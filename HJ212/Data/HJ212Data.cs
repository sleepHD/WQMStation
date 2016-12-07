using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WQMStation.HJ212.Data
{
    public abstract class HJ212Data
    {
        protected readonly VarItem[] _vars;
        protected readonly DateTime _datatime;

        public HJ212Data(DateTime datatime, VarItem[] vars)
        {
            _datatime = datatime;
            _vars = vars;
        }

        public abstract string DataString { get; }
    }
}
