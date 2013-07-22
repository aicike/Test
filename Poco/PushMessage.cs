using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco
{
    public class PushMessage
    {
        public string Title { get; set; }

        public string Text { get; set; }

        public string Logo { get; set; }

        /// <summary>
        /// 动作
        /// </summary>
        private EnumEvent enumEvent = EnumEvent.Wait;
        public EnumEvent EnumEvent
        {
            get { return enumEvent; }
            set { enumEvent = value; }
        }

        /// <summary>
        /// 响铃
        /// </summary>
        private bool isRing = true;
        public bool IsRing
        {
            get { return isRing; }
            set { isRing = value; }
        }

        /// <summary>
        /// 震动
        /// </summary>
        private bool isVibrate = true;
        public bool IsVibrate
        {
            get { return isVibrate; }
            set { isVibrate = value; }
        }

        /// <summary>
        /// 通知是否可清除
        /// </summary>
        private bool isClearable = true;
        public bool IsClearable
        {
            get { return isClearable; }
            set { isClearable = value; }
        }
    }

    public enum EnumEvent
    {
        Immediately = 1,
        Wait = 2
    }
}
