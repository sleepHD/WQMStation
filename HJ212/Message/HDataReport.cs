namespace WQMStation.HJ212.Message
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Text;
    using Data;
    using System.Collections.Generic;
    using System.Linq;

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
            var itemString = dataString.Substring("CP=&&".Length, dataString.Length - "CP=&&&&".Length);
            var items = itemString.Split(';');
            DateTime dt = DateTime.MinValue;
            var pts = items[0].Split('=');
            if (pts.Length == 2)
            {
                dt = DateTime.ParseExact(pts[1], "yyyyMMddHHmmss", CultureInfo.InvariantCulture, DateTimeStyles.None);
            }
            var vItems = new List<VarItem>();
            foreach ( var item in items.Skip(1).ToArray())
            {
                var vfParts = item.Split(',');
                if (vfParts.Length == 2)
                {
                    var v = new VarItem();
                    var vParts = vfParts[0].Split('=');
                    v.Code = vParts[0].Substring(0, vParts[0].IndexOf('-'));
                    v.Value = double.Parse(vParts[1]);
                    var fParts = vfParts[1].Split('=');
                    v.Flag = fParts[1];
                    vItems.Add(v);
                }
            }
            _hourData = new HourData(dt, vItems.ToArray());
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
                messageString.Append(_hourData.DataString);
                messageString.Append("&&");
                return Encoding.ASCII.GetBytes(messageString.ToString());
            }
        }
    }
}
;