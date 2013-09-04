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
        /// 消息方向 0:售楼代表对用户  1：用户对售楼代表  2：售楼代表对售楼代表  3：用户对用户
        /// </summary>
        public string MSD
        {
            get { return GetTag("msd"); }
            set { SetTag("msd", value); }
        }

        /// <summary>
        /// 消息类别 1 消息，2状态, 3未读消息,4发送成功，5对方已读,6发送失败
        /// </summary>
        public string MT
        {
            get { return GetTag("mt"); }
            set { SetTag("mt", value); }
        }

        /// <summary>
        /// 会话ID
        /// </summary>
        public string SID
        {
            get { return GetTag("sid"); }
            set { SetTag("sid",value); }
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

        /// <summary>
        /// 图文ID
        /// </summary>
        public string ImgTextID
        {
            get { return GetTag("imgtextid"); }
            set { SetTag("imgtextid", value); }
        }
        /// <summary>
        /// 图文标题
        /// </summary>
        public string fileTitle{
            get { return GetTag("filetitle"); }
            set { SetTag("filetitle", value); }
        }

        /// <summary>
        /// 图文摘要
        /// </summary>
        public string Summary {
            get { return GetTag("summary"); }
            set { SetTag("summary", value); }
        }

        /// <summary>
        /// 子图文字符窜 json
        /// </summary>
        public string Subcontent
        {
            get { return GetTag("subcontent"); }
            set { SetTag("subcontent", value); }
        }
        /// <summary>
        /// 发送时间
        /// </summary>
        public string Sendtime
        {
            get { return GetTag("sendtime"); }
            set { SetTag("sendtime", value); }
        }
        
    }
}
