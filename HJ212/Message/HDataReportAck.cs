using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace WQMStation.HJ212.Message
{
    internal class HDataReportAck : HJ212Message
    {
        public HDataReportAck()
        {

        }

        public HDataReportAck(DateTime qn, string st, string passwd, string mn)
            : base(qn, st, passwd, HJ212.DataAck, mn)
        {

        }

        protected override void InitializeUnique(string dataString)
        {
            var itemString = dataString.Substring("CP=&&".Length, dataString.Length - "CP=&&&&".Length);
            var items = itemString.Split(';');
            if(items[0].StartsWith("QN="))
            {
                QN = DateTime.ParseExact(items[0].Substring("QN=".Length), "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture, DateTimeStyles.None);
            }
        }

        public override void ValidateResponse(HJ212Message response)
        {

        }

        public override byte[] MessageFrame
        {
            get
            {
                var messageString = new StringBuilder();

                messageString.Append("ST=" + ST + ";");
                messageString.Append("CN=" + CN + ";");
                messageString.Append("CP=&&QN=" + QN.ToString("yyyyMMddHHmmssfff") + "&&");
                return Encoding.ASCII.GetBytes(messageString.ToString());
            }
        }
    }
}
