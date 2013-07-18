using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco
{
    public interface ILibrary
    {
        string FileName { get; set; }

        string FilePath { get; set; }

        int AccountMainID { get; set; }

        AccountMain AccountMain { get; set; }
    }
}
