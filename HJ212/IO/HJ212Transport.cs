namespace WQMStation.HJ212.IO
{
    using Message;
    using System;
    using System.Globalization;
    using System.IO;
    using System.Text;
    using Utility;
    using WQMStation.IO;

    internal class HJ212Transport : IDisposable
    {
       
        public bool CheckFrame { get; set; }

        private const string _header = "##";
        private const string _tailer = "\r\n";
        private string _len = "0000";

        private readonly object _syncLock = new object();
        private IStreamResource _streamResource;
 

        public HJ212Transport(IStreamResource streamResource)
        {
            _streamResource = streamResource;
        }

        public  T UnicastMessage<T>(HJ212Message message) where T : HJ212Message, new()
        {
            HJ212Message response = null;
            lock (_syncLock)
            {
                Write(message);
                response = ReadResponse<T>();
            }
            ValidateResponse(message, response);
            return (T)response;
        }


        internal  void ValidateResponse(HJ212Message request, HJ212Message response)
        {
            // always check the function code and slave address, regardless of transport protocol
            if (request.QN != request.QN)
                throw new IOException(String.Format(CultureInfo.InvariantCulture,
                    "Received response with unexpected QN. Expected {0}, received {1}.", request.QN,
                    response.QN));

            if (request.MN != response.MN)
                throw new IOException(String.Format(CultureInfo.InvariantCulture,
                    "Response MN does not match request. Expected {0}, received {1}.", response.MN,
                    request.MN));

            // message specific validation
            if (request != null)
            {
                request.ValidateResponse(response);
            }
        }

        internal bool ChecksumsMatch(HJ212Message message, byte[] messageFrame)
        {
            var crcString = Encoding.ASCII.GetString(messageFrame, messageFrame.Length - 4, 4);
            var crcBytes = BytesUtility.HexToBytes(crcString);
            return BitConverter.ToUInt16(crcBytes,0) ==
                   BitConverter.ToUInt16(BytesUtility.CalculateCrc(message.MessageFrame), 0);
        }

        internal T ReadResponse<T>() where T : HJ212Message, new()
        {
            // read message frame, removing header,len
            var stringframe = StreamResourceUtility.ReadLine(_streamResource);
            var messageFrame = stringframe.Substring(_header.Length + _len.Length,
                                              stringframe.Length - _header.Length - _len.Length);
            var frame = Encoding.ASCII.GetBytes(messageFrame);
            var response = HJ212MessageFactory.CreateHJ212Message<T>(frame);
            // compare checksum
            if (CheckFrame && !ChecksumsMatch(response, frame))
            {
                string errorMessage = String.Format(CultureInfo.InvariantCulture, "Checksums failed to match {0} != {1}",
                    string.Join(", ", response.MessageFrame), string.Join(", ", frame));
                throw new IOException(errorMessage);
            }
            return response;
        }

        internal byte[] BuildMessageFrame(HJ212Message message)
        {
            var messageFrame = message.MessageFrame;
            var crc = BytesUtility.GetAsciiBytes(BytesUtility.CalculateCrc(messageFrame));
            var messageBody = new MemoryStream(_header.Length + _len.Length +
                messageFrame.Length + crc.Length + _tailer.Length);

            messageBody.Write(Encoding.ASCII.GetBytes(_header), 0, _header.Length);
            messageBody.Write(Encoding.ASCII.GetBytes(_len), 0, _header.Length);
            messageBody.Write(messageFrame, 0, messageFrame.Length);
            messageBody.Write(crc, 0, crc.Length);
            messageBody.Write(Encoding.ASCII.GetBytes(_tailer), 0, _tailer.Length);

            return messageBody.ToArray();
        }

        public void Write(HJ212Message message)
        {
            _streamResource.DiscardInBuffer();

            byte[] frame = BuildMessageFrame(message);
            _streamResource.Write(frame, 0, frame.Length);
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing">
        ///     <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only
        ///     unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                DisposableUtility.Dispose(ref _streamResource);
        }
    }
}
