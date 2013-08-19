using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using agsXMPP.Xml.Dom;

namespace Poco
{
    public class NewsProtocol:Element
    {
        public NewsProtocol()
        {
            this.TagName = "NewsProtocol";
            this.Namespace = "agsoftware:NewsProtocol";
        }

        /// <summary>
        /// 消息方向
        /// </summary>
        public string MSD
        {
            get { return GetTag("msd"); }
            set { SetTag("msd", value); }
        }

        /// <summary>
        /// 消息类别 1 消息，2状态
        /// </summary>
        public string MT
        {
            get { return GetTag("mt"); }
            set { SetTag("mt", value); }
        }

        /// <summary>
        /// 消息类型  0：文本库 1：图片库 2：视频库 3：音频库 4：图文库 
        /// </summary>
        public string EID 
        {
            get { return GetTag("eid"); }
            set { SetTag("eid",value); }
        }

        /// <summary>
        /// 文件地址
        /// </summary>
        public string FielUrl 
        {
            get { return GetTag("fileurl"); }
            set { SetTag("fileurl", value); }
        }
    }
}
