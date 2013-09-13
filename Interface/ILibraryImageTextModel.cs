using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using System.Web;

namespace Interface
{
    public interface ILibraryImageTextModel : IBaseModel<LibraryImageText>, ILibraryModel<LibraryImageText>
    {
        Result Add(LibraryImageText libraryImageText, HttpPostedFileBase coverImagePathFile);

        Result Edit(LibraryImageText libraryImageText, HttpPostedFileBase coverImagePathFile);

        Result Delete(int id, int accountMainID);

        Result AddMore(LibraryImageText libraryImageText);

        Result EditMore(LibraryImageText libraryImageText,List<LibraryImageText> sublist);
    }
}