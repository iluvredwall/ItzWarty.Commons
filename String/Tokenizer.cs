using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Dargon.Commons.String {
   public static class Tokenizer {
      /// <summary>
      /// Takes fileName like annieSquare.dds, AnnieSquare.dds, annie_square_dds, ANNIE_SQUARE.dds and 
      /// outputs  an array such as ["annie", "square", "dds"].  Non-alphanumeric values are deemed
      /// as delimiters as well.
      /// 
      /// Delimiters:
      ///    non-alphanumerics
      ///    In the middle of two (and only two) uppercase characters that are followed by lowercase characters
      ///       Ie: "ACar" => ["A", "Car"]
      ///    On switch from uppercase string of 3+ to lowercase
      ///       Ie: "MANmode" => ["MAN", "mode"]
      ///    On switch from lowercase string to uppercase
      ///       Ie: "ExampleText" => ["Example", "Text"]
      ///    On switch from number to alphabet or vice versa
      ///       Ie: "IHave10Apples" => ["I", "Have", "10", "Apples"]
      ///    On reaching a non-alphanumeric value
      ///       Ie; "RADS_USER_Kernel.exe" => ["RADS", "USER", "Kernel", "exe"]
      /// </summary>
      /// <param name="name"></param>
      /// <returns></returns>
      public static IEnumerable<string> ExtractFileNameTokens(string fileName) {
         StringBuilder sb = new StringBuilder();

         // We start as if we were just at position -1
         CharType lastlastCharType = CharType.Invalid;
         CharType lastCharType = CharType.Invalid;
         CharType charType = CharType.Invalid;
         CharType nextCharType = fileName.Length >= 1 ? GetCharType(fileName[0]) : CharType.Invalid;
         for (int i = 0; i < fileName.Length; i++) {
            lastlastCharType = lastCharType;
            lastCharType = charType;
            charType = nextCharType;
            nextCharType = fileName.Length > i + 1 ? GetCharType(fileName[i + 1]) : CharType.Invalid;
            char c = fileName[i];
            //Console.WriteLine("Got char " + c + " current sb " + sb.ToString());

            if (sb.Length == 0) {
               if (charType != CharType.Invalid)
                  sb.Append(c);
            } else {
               // Check delimit condition: In the middle of two (and only two) uppercase characters that are followed by lowercase characters
               if (lastlastCharType != CharType.Uppercase && //e, current string builder = "A"
                   lastCharType == CharType.Uppercase &&     //A
                   charType == CharType.Uppercase &&         //C
                   nextCharType == CharType.Lowercase)       //a
               {
                  yield return sb.ToString();
                  sb.Clear();
                  sb.Append(c);
               } else // Check delimit condition: On switch from uppercase string of 3+ to lowercase
               if (lastlastCharType == CharType.Uppercase && //M, current string builder = "A"
                   lastCharType == CharType.Uppercase &&     //A
                   charType == CharType.Uppercase &&         //N
                   nextCharType == CharType.Lowercase)       //m
               {
                  sb.Append(c);
                  yield return sb.ToString();
                  sb.Clear();
               } else // Check delimit condition: On switch from lowercase string to uppercase
               if (charType == CharType.Lowercase &&         //n
                   nextCharType == CharType.Uppercase)       //M
               {
                  sb.Append(c);
                  yield return sb.ToString();
                  sb.Clear();
               } else // Check delimit condition: On switch from number to alphabet or vice versa
               if ((charType == CharType.Number && (nextCharType == CharType.Lowercase || nextCharType == CharType.Uppercase)) ||
                  (nextCharType == CharType.Number && (charType == CharType.Lowercase || charType == CharType.Uppercase))) {
                  sb.Append(c);
                  yield return sb.ToString();
                  sb.Clear();
               } else // Check delimit condition: On reaching a non-alphanumeric value
               if (charType == CharType.Invalid) {
                  if (sb.Length > 0)
                     yield return sb.ToString();
                  sb.Clear();
               } else // Check delimit condition: On reaching a non-alphanumeric value
               if (nextCharType == CharType.Invalid) {
                  sb.Append(c);
                  yield return sb.ToString();
                  sb.Clear();
               } else // Didn't get delimited!
                 {
                  // Console.WriteLine("Appending " + c + " " + lastlastCharType + " " + lastCharType + " " + charType + " " + nextCharType);
                  sb.Append(c);
               }
            }
         } // for
         if (sb.Length > 0)
            yield return sb.ToString();
         yield break;
      }

      private static CharType GetCharType(char c) {
         if ('a' <= c && c <= 'z')
            return CharType.Lowercase;
         else if ('A' <= c && c <= 'Z')
            return CharType.Uppercase;
         else if ('0' <= c && c <= '9')
            return CharType.Number;
         else
            return CharType.Invalid;
      }

      private enum CharType { Invalid, Lowercase, Uppercase, Number }

      /// <summary>
      /// Formats a name from UpperCamelCase to Upper Camel Case
      /// </summary>
      /// <param name="name"></param>
      /// <returns></returns>
      public static string ExtractUpperCamelCaseTokens(string name) {
         name = name + "   ";
         name = name[0].ToString().ToUpper() + name.Substring(1);
         //http://stackoverflow.com/questions/4511087/regex-convert-camel-case-to-all-caps-with-underscores
         string _RESULT_VAL = Regex.Replace(name, @"(?x)( [A-Z][a-z,0-9]+ | [A-Z]+(?![a-z]) )", "_$0");
         //Console.WriteLine("* " + _RESULT_VAL);
         string RESULT_VAL = _RESULT_VAL.Substring(1);
         //Console.WriteLine("# " + RESULT_VAL);

         var result = from part in RESULT_VAL.Split(new char[] { '_', ' ' })
                      let partPad = part + "  "
                      let firstChar = part.Length > 3 ? partPad[0].ToString().ToUpper() : partPad[0].ToString().ToLower()
                      select (firstChar + partPad.Substring(1).ToLower()).Trim();

         string resultString = string.Join(" ", result.Where((s) => !string.IsNullOrWhiteSpace(s)).ToArray()).Trim();

         //Make the first letter of the first term capitalized
         resultString = resultString[0].ToString().ToUpper() + resultString.Substring(1);

         //Replace multiple space occurrences
         string realResult = string.Join(" ", resultString.QASS(' '));
         //Console.WriteLine("> " + realResult);
         return realResult;
      }
   }
}
