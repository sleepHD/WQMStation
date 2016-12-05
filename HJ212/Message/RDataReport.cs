using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WQMStation.HJ212.Message
{
    class RDataReport : HJ212Message
    {
        protected override void InitializeUnique(string dataFrame)
        {
            throw new NotImplementedException();
        }

        public override void ValidateResponse(HJ212Message response)
        {
            throw new NotImplementedException();
        }

        public override byte[] MessageFrame
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
