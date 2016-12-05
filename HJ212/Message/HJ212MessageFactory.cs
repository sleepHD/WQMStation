using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WQMStation.HJ212.Message
{
    class HJ212MessageFactory
    {

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="frame"></param>
        /// <returns></returns>
        public static T CreateHJ212Message<T>(byte[] frame) where T : HJ212Message, new()
        {
            HJ212Message message = new T();
            message.Initialize(frame);
            return (T)message;
        }
    }
}
