using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Injection;
using Interface;

namespace Business
{
    public class LibraryImageModel : BaseModel<LibraryImage>, ILibraryImageModel
    {
        public IQueryable<LibraryImage> GetLibraryList(int accountMainID)
        {
            throw new NotImplementedException();
        }
    }
}
