using System;

namespace Dargon.Commons.Comparers {
   public static class ByteArrayComparer {
      public static bool ByteArraysEqual(byte[] param1, byte[] param2) {
         return ByteArraysEqual(param1, 0, param1.Length, param2, 0, param2.Length);
      }

      public static bool ByteArraysEqual(byte[] a, int aOffset, byte[] b, int bOffset, int length) {
         return ByteArraysEqual(a, aOffset, length, b, bOffset, length);
      }

      public static unsafe bool ByteArraysEqual(byte[] a, int aOffset, int aLength, byte[] b, int bOffset, int bLength) {
         if (aOffset + aLength > a.Length) {
            throw new IndexOutOfRangeException("aOffset + aLength > a.Length");
         } else if (bOffset + bLength > b.Length) {
            throw new IndexOutOfRangeException("bOffset + bLength > b.Length");
         } else if (aOffset < 0) {
            throw new IndexOutOfRangeException("aOffset < 0");
         } else if (bOffset < 0) {
            throw new IndexOutOfRangeException("bOffset < 0");
         } else if (aLength < 0) {
            throw new IndexOutOfRangeException("aLength < 0");
         } else if (bLength < 0) {
            throw new IndexOutOfRangeException("bLength < 0");
         }

         if (aLength != bLength) {
            return false;
         } else if (a == b && aOffset == bOffset && aLength == bLength) {
            return true;
         }

         fixed (byte* pABase = a)
         fixed (byte* pBBase = b)
         {
            byte* pACurrent = pABase + aOffset, pBCurrent = pBBase + bOffset;
            var length = aLength;
            int longCount = length / 8;
            for (var i = 0; i < longCount; i++) {
               if (*(ulong*)pACurrent != *(ulong*)pBCurrent) {
                  return false;
               }
               pACurrent += 8;
               pBCurrent += 8;
            }
            if ((length & 4) != 0) {
               if (*(uint*)pACurrent != *(uint*)pBCurrent) {
                  return false;
               }
               pACurrent += 4;
               pBCurrent += 4;
            }
            if ((length & 2) != 0) {
               if (*(ushort*)pACurrent != *(ushort*)pBCurrent) {
                  return false;
               }
               pACurrent += 2;
               pBCurrent += 2;
            }
            if ((length & 1) != 0) {
               if (*pACurrent != *pBCurrent) {
                  return false;
               }
               pACurrent += 1;
               pBCurrent += 1;
            }
            return true;
         }
      }
   }
}
