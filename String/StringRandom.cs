using System;
using System.Text;

namespace Dargon.Commons.String {
   public static class StringRandom {
      /// <summary>
      /// Generates a string in a stupid way...
      /// lol
      /// </summary>
      public static string GenerateString(int length) {
         StringBuilder temp = new StringBuilder();
         while (temp.Length < length)
            temp.Append(Guid.NewGuid().ToByteArray().ToHex());
         return temp.ToString().Substring(0, length);
      }
   }
}
