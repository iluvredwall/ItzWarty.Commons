namespace Dargon.Commons.Array {
   public static class ByteArrayUtil {
      public static byte FindMaximum(byte[] bytes) {
         byte max = bytes[0];
         for (int i = 1; i < bytes.Length; i++) {
            if (max < bytes[i])
               max = bytes[i];
         }
         return max;
      }

      public static byte FindMinimum(byte[] bytes) {
         byte min = bytes[0];
         for (int i = 1; i < bytes.Length; i++) {
            if (min > bytes[i])
               min = bytes[i];
         }
         return min;
      }
   }
}
