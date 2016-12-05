


namespace WQMStation.HJ212.Message
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Text;
    using Data;

    internal class HDataReport : HJ212Message
    {
        private HourData _hourData;

        public string ResponseST { get; set; }
        public string ResponseCN { get; set; }

        public HDataReport()
        {

        }

        public HDataReport(DateTime qn, string st, string passwd, string mn, HourData hourData)
            :base(qn, st, passwd, HJ212.HourData, mn)
        {
            ResponseCN = HJ212.DataAck;
            _hourData = hourData;
        }

        protected override void InitializeUnique(string dataString)
        {
            throw new NotImplementedException();
        }

        public override void ValidateResponse(HJ212Message response)
        {
            if(response.CN != ResponseCN)
            {
                throw new IOException(String.Format(CultureInfo.InvariantCulture,
                   "Unexpected CN in response. Expected {0}, received {1}.",
                   ResponseCN,
                   response.CN));
            }

            if (response.ST != ResponseST)
            {
                throw new IOException(String.Format(CultureInfo.InvariantCulture,
                   "Unexpected ST in response. Expected {0}, received {1}.",
                   ResponseST,
                   response.ST));
            }

            if (response.QN != QN)
            {
                throw new IOException(String.Format(CultureInfo.InvariantCulture,
                   "Mismatched QN in response. Expected {0}, received {1}.",
                   QN,
                   response.QN));
            }

        }

        public override byte[] MessageFrame
        {
            get
            {
                var messageString = new StringBuilder();

                messageString.Append("ST=" + ST + ";");
                messageString.Append("CN=" + CN + ";");
                messageString.Append("QN=" + QN.ToString("yyyyMMddHHmmssfff") + ";");
                messageString.Append("PW=" + Password + ";");
                messageString.Append("MN=" + MN + ";");

                if (PNum.HasValue)
                {
                    messageString.Append(PNum.Value.ToString());
                }
                if (PNo.HasValue)
                {
                    messageString.Append(PNo.Value.ToString());
                }
                if (Flag.HasValue)
                {
                    messageString.Append(Flag.Value.ToString());
                }
                messageString.Append("CP=&&");
                return Encoding.ASCII.GetBytes(messageString.ToString());
            }
        }
    }
}
