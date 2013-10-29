using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco.WebAPI_Poco
{
    public class AppVersionInfo
    {
        public bool HasNewVersion { get; set; }

        public string Info { get; set; }

        public string VersionCode { get; set; }

        public string AppName { get; set; }

        public string AppPath { get; set; }
    }
}
