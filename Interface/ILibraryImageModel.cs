using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using System.Web;

namespace Interface
{
    public interface ILibraryImageModel : IBaseModel<LibraryImage>, ILibraryModel<LibraryImage>
    {
        Result Upload(LibraryImage entity, HttpPostedFileBase image);

        Result ReName(int id, string name);
    }
}