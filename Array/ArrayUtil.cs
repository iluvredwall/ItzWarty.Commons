using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dargon.Commons.Array {
   class ArrayUtil {
      public static T[] Concat<T>(params object[] args) {
         var result = new List<T>();
         foreach (var element in args) {
            if (element is T)
               result.Add((T)element);
            else {
               foreach (var subElement in (IEnumerable<T>)element)
                  result.Add(subElement);
            }
         }
         return result.ToArray();
      }
   }
}
