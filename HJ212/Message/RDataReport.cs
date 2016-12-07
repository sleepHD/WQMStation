namespace WQMStation.HJ212.Message
{
    using Data;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;

    class RDataReport : HJ212Message
    {
        private RealTimeData _realTimeData;

        public string ResponseST { get; set; }
        public string ResponseCN { get; set; }

        public RDataReport()
        {

        }

        public RDataReport(DateTime qn, string st, string passwd, string mn, RealTimeData realTimeData)
            : base(qn, st, HJ212.RealTimeData, passwd, mn)
        {
            ResponseCN = HJ212.DataAck;
            _realTimeData = realTimeData;
        }

        protected override void InitializeUnique(string dataFrame)
        {
            throw new NotImplementedException();
        }

        public override void ValidateResponse(HJ212Message response)
        {
            if (response.CN != ResponseCN)
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
        }

        public override byte[] MessageFrame
        {
            get
            {
                var messageString = new StringBuilder();

                messageString.Append("QN=" + QN.ToString("yyyyMMddHHmmssfff") + ";");
                messageString.Append("ST=" + ST + ";");
                messageString.Append("CN=" + CN + ";");
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
                messageString.Append(_realTimeData.DataString);
                messageString.Append("&&");
                return Encoding.ASCII.GetBytes(messageString.ToString());
            }
        }
    }
}
