using System.Collections.Generic;
using SCC = System.Collections.Concurrent;

namespace Dargon.Commons.Collections {
   public class ConcurrentBag<T> : SCC.ConcurrentBag<T>, IConcurrentBag<T> {
      public ConcurrentBag() : base() { }
      public ConcurrentBag(IEnumerable<T> collection) : base(collection) { }
   }
}
