using System;
using System.Runtime.CompilerServices;

namespace Dargon.Commons.Comparers {
   public static class DoubleComparer {
      /// <summary>
      /// Returns whether or not the given value is within (inclusive) the other two parameters
      /// </summary>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static bool IsBetween(Double a, Double value, Double b) {
         return (a <= value && value <= b) || (b <= value && value <= a);
      }
   }
}
