using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace WQMStation.HJ212.Message
{
    internal abstract class HJ212Message
    {
        public DateTime QN { get; set; }
        public string ST { get; set; }
        public string CN { get; set; }
        public string Password { get; set; }
        public string MN { get; set; }
        public int? PNum { get; set; }
        public int? PNo { get; set; }
        public int? Flag { get; set; }


        internal HJ212Message()
        {

        }

        internal HJ212Message(DateTime qn, string st, string cn, string passwd, string mn)
        {
            QN = qn;
            ST = st;
            CN = cn;
            Password = passwd;
            MN = mn;
        }

        public abstract byte[] MessageFrame { get;}

        public void Initialize(byte[] frame)
        {
            if (frame == null)
                throw new ArgumentNullException("frame", "Argument frame cannot be null.");
            var messageString = Encoding.ASCII.GetString(frame);
            var dataIndex = messageString.IndexOf("CP=&&");
            var headString = messageString.Substring(0, dataIndex);
            var dataString = messageString.Substring(dataIndex, messageString.Length - dataIndex);
            var dic = new Dictionary<string, string>();
            var items = headString.Split(';');
            foreach (var item in items)
            {
                var parts = item.Split('=');
                if(parts.Length == 2)
                {
                    dic.Add(parts[0], parts[1]);
                }     
            }
            if (dic.ContainsKey("PN"))
                QN = DateTime.ParseExact(dic["QN"], "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture, DateTimeStyles.None);
            ST = dic["ST"];
            CN = dic["CN"];
            if (dic.ContainsKey("PW"))
                Password = dic["PW"];
            if (dic.ContainsKey("MN"))
                MN = dic["MN"];
            if (dic.ContainsKey("Flag"))
                Flag = int.Parse(dic["Flag"]);
            if (dic.ContainsKey("PNum"))
                PNum = int.Parse(dic["PNum"]);
            if (dic.ContainsKey("PNo"))
                PNo = int.Parse(dic["PNo"]);

            InitializeUnique(dataString);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="frame"></param>
        protected abstract void InitializeUnique(string dataString);

        public abstract void ValidateResponse(HJ212Message response);

    }
}
