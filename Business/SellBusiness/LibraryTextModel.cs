using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Injection;
using Interface;

namespace Business
{
    public class LibraryTextModel : BaseModel<LibraryText>, ILibraryTextModel
    {
        public IQueryable<LibraryText> GetLibraryList(int accountMainID)
        {
            return List(true).Where(a => a.AccountMainID == accountMainID);
        }

        public Result Add(LibraryText libraryText, int accontMainID)
        {
            libraryText.AccountMainID = accontMainID;
            return base.Add(libraryText);
        }

        public Result Edit(LibraryText libraryText, int accontMainID)
        {
            libraryText.AccountMainID = accontMainID;
            return base.Edit(libraryText);
        }
    }
}
