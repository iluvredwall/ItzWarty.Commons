﻿using System;
using System.Threading;
using Dargon.Commons.Array;
using Dargon.Commons.Pooling;
using Xunit;

namespace Dargon.Commons.Collections {
   public class ObjectPoolBenchmark {
      [Fact]
      public void Run() {
         for (var i = 0; i < 10; i++) {
            Console.WriteLine("Run " + i + ": ");
            using (new UsingTimer(x => Console.WriteLine("Pool: " + x.ElapsedTime.TotalMilliseconds))) {
               ObjectPool<object> pool = new ObjectPoolImpl<object>(() => new object());
               RunBenchmark(pool.TakeObject, pool.ReturnObject);
            }
            using (new UsingTimer(x => Console.WriteLine("Queue: " + x.ElapsedTime.TotalMilliseconds))) {
               IConcurrentQueue<object> queue = new ConcurrentQueue<object>();
               RunBenchmark(() => {
                  object result;
                  if (!queue.TryDequeue(out result)) {
                     result = new object();
                  }
                  return result;
               }, queue.Enqueue);
            }
         }
      }

      public void RunBenchmark<T>(Func<T> get, Action<T> put) {
         const int threadCount = 8;
         const int putGetCount = 200000;

         var readyEvent = new CountdownEvent(threadCount);
         var beginEvent = new CountdownEvent(1);
         var doneEvent = new CountdownEvent(threadCount);
         var threads = ArrayGenerator.Generate(threadCount,
            threadNumber => new Thread(() => {
               readyEvent.Signal();
               beginEvent.Wait();
               for (var i = 0; i < putGetCount; i++) {
                  put(get());
               }
               doneEvent.Signal();
            }));
         threads.ForEach(t => t.Start());

         readyEvent.Wait();
         beginEvent.Signal();
         doneEvent.Wait();
      }
   }
}