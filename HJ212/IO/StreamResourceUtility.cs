namespace WQMStation.HJ212.IO
{
    using System.Linq;
    using System.Text;
    using WQMStation.IO;

    internal static class StreamResourceUtility
    {
        internal static string ReadLine(IStreamResource stream)
        {
            var result = new StringBuilder();
            var singleByteBuffer = new byte[1];

            do
            {
                if (0 == stream.Read(singleByteBuffer, 0, 1))
                    continue;

                result.Append(Encoding.ASCII.GetChars(singleByteBuffer).First());
            } while (!result.ToString().EndsWith("\r\n"));

            return result.ToString();
        }
    }
}
