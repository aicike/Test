using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using agsXMPP.Xml.Dom;

namespace AcceptanceServer
{
    public class DrawStep : Element
    {
        public DrawStep()
        {
            this.TagName = "DrawStep";
            this.Namespace = "agsoftware:DrawStep";
        }
        public string Type
        {
            get { return GetTag("type"); }
            set { SetTag("type", value); }
        }
        public string Color
        {
            get { return GetTag("color"); }
            set { SetTag("color", value); }
        }
        public int Width
        {
            get { return GetTagInt("width"); }
            set { SetTag("width", value.ToString()); }
        }
    }
    public class Points : Element
    {
        public Points()
        {
            this.TagName = "points";
            this.Namespace = "agsoftware:DrawStep";

        }
        public int X
        {
            get { return GetTagInt("X"); }
            set { SetTag("X", value.ToString()); }
        }
        public int Y
        {
            get { return GetTagInt("Y"); }
            set { SetTag("Y", value.ToString()); }
        }

    }

}
