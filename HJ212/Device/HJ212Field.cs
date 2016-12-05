namespace WQMStation.HJ212.Device
{
    using Data;
    using IO;
    using Message;
    using System;
    using WQMStation.IO;

    public class HJ212Field : HJ212Device
    {
        public string ST { get; set; }
        public string Password { get; set; }
        public string MN { get; set; }

        public HJ212Field(string st, string passwd, string mn, IStreamResource streamResource)
            :base(streamResource)
        {
            ST = st;
            Password = passwd;
            MN = mn;
        }

        public void ReportHourData(HourData hData)
        {
            var hourDataReport = new HDataReport(DateTime.Now, ST, Password, MN, hData);
            _transport.UnicastMessage<HDataReportAck>(hourDataReport);
        }
    }
}
