using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco
{
    public class _B_Classify
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 类别名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 类别描述
        /// </summary>
        public string Depict { get; set; }

        /// <summary>
        /// 类别图片
        /// </summary>
        public string ImgPath { get; set; }


        /// <summary>
        /// 类别图片详细路径
        /// </summary>
        public string ImgFIlePath { get; set; }

        /// <summary>
        /// 上级ID 0为顶级分类
        /// </summary>
        public int ParentID { get; set; }

        /// <summary>
        /// 上级类别名称
        /// </summary>
        public string ParentName { get; set; }

        /// <summary>
        /// 排序 升序
        /// </summary>
        public int Sort { get; set; }
    }
}
