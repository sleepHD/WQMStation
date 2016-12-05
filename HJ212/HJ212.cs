using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WQMStation.HJ212
{
    /// <summary>
    ///     Defines constants related to the HJ212 protocol.
    /// </summary>
    internal static class HJ212
    {
        // HJ212 function codes

        #region  initialization functions
        //set overtime and recounts. what do overtime and recounts mean
        public const string SetOverTime_ReCounts = "1000";
        
        //set warn times. what does warn times mean?
        public const string SetWarnTimes = "1011";
        #endregion

        #region parameter functions
        public const string GetDateTime = "1011";
        public const string SetDateTime = "1012";

        public const string GetAlarmLimits = "1021";
        public const string SetAlarmLimits = "1022";

        //what does alarm target mean?
        public const string GetAlarmTarget = "1031";
        public const string SetAlarmTarget = "1032";

        //what does report time mean?
        public const string GetReportTime = "1041";
        public const string SetReportTime = "1042";

        public const string GetRtdInterval = "1061";
        public const string SetRtdInterval = "1062";

        public const string SetPassword = "1072";
        #endregion

        #region interactivity functions
        //ack result response (field to center)
        public const string QnResponse = "9011";
        //execution result responses (field to center)
        public const string ExeResponse = "9012";

        //ack result response (not well defined)
        public const string NoteAck = "9013";
        //execution result responses (not well defined)
        public const string DataAck = "9014";
        #endregion

        #region data functions
        //poll or report real time data
        public const string RealTimeData = "2011";
        //stop real time data report (not well defined,generally not used)
        public const string StopRealTime = "2012";

        //poll or report device status (not well defined) 
        public const string DeviceStatus = "2021";
        //stop device status report (not well defined, generally not used)
        public const string StopDeviceStatus = "2022";

        //poll or report day data
        public const string DayData = "2031";

        //poll or report devices' running time （not well defined, genrelly not used)
        public const string DayDeviceTime = "2041";

        //poll or report minute data
        public const string MinuteData = "2051";

        //poll or report hour data (not well defined,defined more specifically by different vendors)
        public const string HourData = "2061";
        #endregion

        #region control functions
        //Calibration function
        public const string CalDevice = "3011";

        //enable a device to start measuring
        public const string StartDevice = "3012";

        //control a device (not well defined, generally not used)
        public const string ControlDevice = "3013";

        //start a device at setted times
        public const string DeviceStartTimes = "3014";
        #endregion

        //HJ212 System Code
        //地表水监测
        public const string GroundWaterMonitor = "21";
        //空气质量监测
        public const string AirMonitor = "22";
        //系统交互
        public const string SysInteraction = "91";
    }
}
