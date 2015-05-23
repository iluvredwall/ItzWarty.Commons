using System;
using System.Collections.Generic;

namespace Dargon.Commons.Array {

   public class GeneratorExitException : Exception {
      public GeneratorExitException() : base("The Generator is unable to produce more results.  Perhaps, there is nothing left to produce?") { }
   }

   public static class ArrayGenerator {
      /// <summary>
      /// Creates an array using the given function N times.
      /// The function takes a parameter i, from 0 to count, and returns T.
      /// </summary>
      public static T[] Generate<T>(int count, Func<int, T> generator) {
         if (count < 0)
            throw new ArgumentOutOfRangeException("count < 0");
         if (generator == null)
            throw new ArgumentNullException("generator");

         T[] result = new T[count];
         for (int i = 0; i < count; i++)
            result[i] = generator(i);
         return result;
      }

      /// <summary>
      /// Creates an array using the given function N times.
      /// The function takes a parameter a from 0 to countA and a parameter b, from 0 to countB, and returns T.
      /// </summary>
      public static T[] Generate<T>(int countA, int countB, Func<int, int, T> generator) {
         if (countA < 0)
            throw new ArgumentOutOfRangeException("countA < 0");
         if (countB < 0)
            throw new ArgumentOutOfRangeException("countB < 0");
         if (generator == null)
            throw new ArgumentNullException("generator");

         T[] result = new T[countA * countB];
         for (int a = 0; a < countA; a++)
            for (int b = 0; b < countB; b++)
               result[a * countB + b] = generator(a, b);
         return result;
      }

      /// <summary>
      /// Creates an array using the given function N times.
      /// </summary>
      public static T[] Generate<T>(int countA, int countB, int countC, Func<int, int, int, T> generator) {
         if (countA < 0)
            throw new ArgumentOutOfRangeException("countA < 0");
         if (countB < 0)
            throw new ArgumentOutOfRangeException("countB < 0");
         if (countC < 0)
            throw new ArgumentOutOfRangeException("countC < 0");
         if (generator == null)
            throw new ArgumentNullException("generator");

         T[] result = new T[countA * countB * countC];
         int i = 0;
         for (int a = 0; a < countA; a++)
            for (int b = 0; b < countB; b++)
               for (int c = 0; c < countC; c++)
                  result[i++] = generator(a, b, c);
         return result;
      }

      /// <summary>
      /// Generates a given output.  Returns null if we are done after this loop.
      /// Throws GeneratorFinishedException if done.
      /// </summary>
      public delegate bool GeneratorDelegate<T>(int i, out T output);

      public static T[] Generate<T>(GeneratorDelegate<T> generator) where T : class {
         List<T> result = new List<T>();
         bool done = false;
         int i = 0;
         try {
            while (!done) {
               T output = null;
               done = generator(i++, out output);
               result.Add(output);
            }
         } catch (GeneratorExitException) {
         } catch (Exception e) {
            throw e;
         }
         return result.ToArray();
      }

      /// <summary>
      /// Creates a variable of the given value repeated [count] times.
      /// Note that this just copies reference if we have a Object.
      /// </summary>
      public static T[] Repeat<T>(int count, T t) {
         T[] result = new T[count];
         for (int i = 0; i < count; i++)
            result[i] = t;
         return result;
      }
   }
}
