using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Injection;
using Interface;

namespace Business
{
    public class LibraryImageTextModel : BaseModel<LibraryImageText>, ILibraryImageTextModel
    {
        public IQueryable<LibraryImageText> GetLibraryList(int accountMainID)
        {
            throw new NotImplementedException();
        }
    }
}
