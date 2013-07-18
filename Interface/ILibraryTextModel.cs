using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface ILibraryTextModel : IBaseModel<LibraryText>, ILibraryModel<LibraryText>
    {
        Result Add(LibraryText libraryText, int accontMainID);
        Result Edit(LibraryText libraryText, int accontMainID);
    }
}