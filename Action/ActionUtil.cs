using System;

namespace Dargon.Commons.Action {
   public static class ActionUtil {
      public static bool IsThrown<TException>(System.Action action) where TException : Exception {
         try {
            action();
            return false;
         } catch (TException) {
            return true;
         }
      }
   }
}
