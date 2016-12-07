using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace WQMStation.HJ212.Data
{
    public class HourData
    {
        private VarItem[] _vars;
        private readonly DateTime _datatime;

        public HourData(DateTime datatime, VarItem[] vars)
        {
            _datatime = datatime;
            _vars = vars;
        }

        public string DataString
        {
            get
            {
                var dataString = new StringBuilder();
                dataString.Append("DataTime=" + _datatime.ToString("yyyyMMddHHmmss") + ";");
                foreach (var v in _vars)
                {
                    dataString.Append(v.Code + "-Avg=" + v.Value.ToString(v.Format)
                                      + "," + v.Code + "-Flag=" + v.Flag + ";");
                }
                //remove the last ";"
                return dataString.ToString().Substring(0,dataString.Length - 1);
            }
        }

    }
}
