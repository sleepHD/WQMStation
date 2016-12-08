namespace WQMStation.HJ212.Device
{
    using Data;
    using IO;
    using log4net;
    using Message;
    using System;
    using System.Linq;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading;
    using WQMStation.IO;

    public class HJ212Field : HJ212Device
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(HJ212Field));
        private TcpClient _tcpClient;
        private bool _slaveMode;
        private Thread _slaveThread;
        private readonly object _syncLock = new object();

        public string ST { get; set; }
        public string Password { get; set; }
        public string MN { get; set; }

        private HJ212Field(string st, string passwd, string mn, TcpClient tcpClient)
            :base(new HJ212Transport(new TcpClientAdapter(tcpClient)))
        {
            ST = st;
            Password = passwd;
            MN = mn;
            _tcpClient = tcpClient;
            _slaveMode = true;
            _slaveThread = new Thread(SlaveRead);
            _slaveThread.Start();
        }

        private void SlaveRead()
        {
            while (true)
            {
                if( _slaveMode && _tcpClient.GetStream().DataAvailable)
                {
                   try
                    {
                        lock (_syncLock)
                        {
                            var result = new StringBuilder();
                            var singleByteBuffer = new byte[1];

                            do
                            {
                                if (0 == _tcpClient.GetStream().Read(singleByteBuffer, 0, 1))
                                    continue;

                                result.Append(Encoding.ASCII.GetChars(singleByteBuffer).First());
                            } while (!result.ToString().EndsWith("\r\n"));
                            _logger.InfoFormat("RX: {0}", result.ToString());
                        }
                    }
                    catch (Exception)
                    {

                    }
                }
                Thread.Sleep(1000);
            }
        }



        /// <summary>
        ///     HJ212 field factory method.
        /// </summary>
        public static HJ212Field CreateTCPField(string st, string passwd, string mn, TcpClient tcpClient)
        {
            if (tcpClient == null)
                throw new ArgumentNullException("tcpClient");
            return new HJ212Field(st, passwd, mn, tcpClient);
        }

        public void ReportHourData(HourData hData)
        {
            _slaveMode = false;
            var hourDataReport = new HDataReport(DateTime.Now, ST, Password, MN, hData);
            try
            {
                lock (_syncLock)
                {
                    _transport.UnicastMessage<HDataReportAck>(hourDataReport);
                }
            }
            catch (Exception)
            {
                _slaveMode = true;
                throw;
            }
            _slaveMode = true;
        }

        public void Close()
        {
            _slaveThread.Abort();
        }
    }
}
