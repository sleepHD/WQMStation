using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WQMStation.HJ212.Extensions.Xhhb
{
    internal static class XhhbHJ212
    {
        #region parameter functions
        public const string FieldSettings = "1011";
        public const string DeviceSettings = "1012";
        #endregion

        //poll or report device status (not well defined) 
        public const string DeviceStatus = "2073";
    }
}
