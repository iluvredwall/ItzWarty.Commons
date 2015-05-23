using System;

namespace Dargon.Commons.String {
   public static class StringUtil {
      public static string RemoveNonalphanumeric(this string s) {
         char[] arr = s.ToCharArray();

         arr = System.Array.FindAll<char>(arr, (c => (Char.IsLetterOrDigit(c) || Char.IsWhiteSpace(c) || c == '-')));
         return new string(arr);
      }
   }
}
