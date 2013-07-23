using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using System.Web;

namespace Interface
{
    public interface ILibraryVideoModel : IBaseModel<LibraryVideo>, ILibraryModel<LibraryVideo>
    {
        Result Upload(LibraryVideo entity, HttpPostedFileBase voice);

        Result ReName(int id, string name);
    }
}