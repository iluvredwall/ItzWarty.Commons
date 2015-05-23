using System;

namespace Dargon.Commons.Date {
   public static class DateUtil {
      public static long GetUnixTimeMilliseconds() {
         return (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
      }
   }
}
