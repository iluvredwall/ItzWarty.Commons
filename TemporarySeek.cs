using System;
using System.IO;

namespace Dargon.Commons
{
   public class TemporarySeek : IDisposable
   {
      private System.IO.Stream m_stream;
      private long m_restoredPosition;

      public TemporarySeek(System.IO.Stream stream, long offset, SeekOrigin origin = SeekOrigin.Begin)
      {
         m_stream = stream;
         m_restoredPosition = stream.Position;

         stream.Seek(offset, origin);
      }

      public void Dispose()
      {
         m_stream.Seek(m_restoredPosition, SeekOrigin.Begin);
      }
   }
}
