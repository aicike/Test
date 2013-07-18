using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Injection;
using Interface;

namespace Business
{
    public class LibraryVideoModel : BaseModel<LibraryVideo>, ILibraryVideoModel
    {
        public IQueryable<LibraryVideo> GetLibraryList(int accountMainID)
        {
            throw new NotImplementedException();
        }
    }
}
