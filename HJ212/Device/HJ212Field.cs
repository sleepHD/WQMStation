namespace WQMStation.HJ212.Device
{
    using Data;
    using IO;
    using Message;
    using System;
    using System.Net.Sockets;
    using WQMStation.IO;

    public class HJ212Field : HJ212Device
    {
        public string ST { get; set; }
        public string Password { get; set; }
        public string MN { get; set; }

        private HJ212Field(string st, string passwd, string mn, HJ212Transport transport)
            :base(transport)
        {
            ST = st;
            Password = passwd;
            MN = mn;
        }

        /// <summary>
        ///     HJ212 field factory method.
        /// </summary>
        public static HJ212Field CreateTCPField(string st, string passwd, string mn, TcpClient tcpClient)
        {
            if (tcpClient == null)
                throw new ArgumentNullException("tcpClient");
            return new HJ212Field(st, passwd, mn, new HJ212Transport(new TcpClientAdapter(tcpClient)));
        }

        public void ReportHourData(HourData hData)
        {
            var hourDataReport = new HDataReport(DateTime.Now, ST, Password, MN, hData);
            _transport.UnicastMessage<HDataReportAck>(hourDataReport);
        }
    }
}
