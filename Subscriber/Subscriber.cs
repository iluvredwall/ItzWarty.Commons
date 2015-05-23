using System;
using System.Threading;

namespace Dargon.Commons.Subscriber {
   public static class Subscriber {
      public static void SubscribeToEventOnce<T>(ref EventHandler<T> @event, EventHandler<T> callback)
         where T : EventArgs {
         var signal = new CountdownEvent(2);
         var accessLock = new object();
         var done = false;
         var handler = new EventHandler<T>(
            (o, e) =>
            {
               //Ensure no concurrent invocations of the event, though I'm not sure if .net allows for that
               lock (accessLock) {
                  //Check if we're done calling the event once.  If so, we don't want to invoke twice.
                  if (!done) {
                     //We're now done.  Set the flag so we aren't called again.
                     done = true;

                     //Invoke the user's code for the one-time event subscription
                     callback(o, e);

                     //Signal that the user's code is done running, so the SubscribeToEventOnce caller
                     //thread can be unblocked.
                     signal.Signal();
                  }
               }
            }
            );
         //Subscribe to the event which we are trying to listen to once
         @event += handler;

         //Signal the countdown event once to tell threads that we're done.  In a case like this where we're
         //really only running 1 thing at a time, it's not important.  If we had more than one thread, and were
         //trying to synchronize all of them, this would be more helpful.  For now, this allows us to
         //wait until the user code has been invoked before we allow this method to return.
         signal.Signal();

         //Wait for the user's callback event to be invoked
         signal.Wait();

         //Unsubscribe to the event.
         @event -= handler;
      }

      public class SingleSubscription {
         internal CountdownEvent m_countdown = new CountdownEvent(1);

         internal void Signal() {
            m_countdown.Signal();
         }

         public void Wait() {
            m_countdown.Wait();
         }
      }

      public static SingleSubscription SubscribeToEventOnceAsync<T>(Action<EventHandler<T>> subscribe,
                                                                    Action<EventHandler<T>> unsubscribe,
                                                                    EventHandler<T> callback)
         where T : EventArgs {
         var result = new SingleSubscription();
         var accessLock = new object();
         var done = false;
         EventHandler<T> handler = null;
         handler = new EventHandler<T>(
            (o, e) =>
            {
               //Ensure no concurrent invocations of the event, though I'm not sure if .net allows for that
               lock (accessLock) {
                  //Check if we're done calling the event once.  If so, we don't want to invoke twice.
                  if (!done) {
                     //We're now done.  Set the flag so we aren't called again.
                     done = true;

                     //Invoke the user's code for the one-time event subscription
                     callback(o, e);

                     //Signal that the user's code is done running, so the SubscribeToEventOnce caller
                     //thread can be unblocked.
                     result.Signal();

                     //Yay closures
                     unsubscribe(handler);
                  }
               }
            }
            );
         //Subscribe to the event which we are trying to listen to once
         subscribe(handler);
         return result;
      }
   }
}
