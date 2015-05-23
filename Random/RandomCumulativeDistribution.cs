using System;

namespace Dargon.Commons.Random {
   public static class RandomCumulativeDistribution {
      /// <summary>
      /// Returns an array containing numbers spaced between 0 and the given maximum value
      /// </summary>
      /// <param name="maximum">
      /// The number which the result approaches from 0 to its last index
      /// </param>
      /// <param name="numElements">
      /// The number of elements in the result (includes 0 and maximum)
      /// </param>
      /// <param name="uniformityFactor">
      /// Greater than 0
      /// </param>
      /// <param name="getRandom">Returns a value in [0.0, 1.0)</param>
      /// <returns></returns>
      public static double[] GenerateRandomCumulativeDistribution(
         double maximum,
         int numElements,
         double uniformityFactor,
         Func<double> getRandom) {
         var weights = new double[numElements];
         weights[0] = 0.0; // actually implicit, but here for readability
         for (int i = 1; i < weights.Length; i++)
            weights[i] = getRandom() + uniformityFactor;

         // :: every element equals the sum of the elements before it
         for (int i = 1; i < weights.Length; i++)
            weights[i] += weights[i - 1];

         // :: normalize all elements to maximum value keysRemaining
         for (int i = 0; i <= weights.Length - 2; i++)
            weights[i] = maximum * weights[i] / weights[weights.Length - 1];

         weights[weights.Length - 1] = maximum;

         return weights;
      }

      public static double[] GenerateRandomCumulativeDistribution(
         double maximum,
         int numElements,
         double uniformityFactor) {
         return GenerateRandomCumulativeDistribution(
            maximum,
            numElements,
            uniformityFactor,
            StaticRandom.NextDouble
            );
      }
   }
}
