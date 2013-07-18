using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Injection;
using Interface;

namespace Business
{
    public class LibraryVoiceModel : BaseModel<LibraryVoice>, ILibraryVoiceModel
    {
        public IQueryable<LibraryVoice> GetLibraryList(int accountMainID)
        {
            throw new NotImplementedException();
        }
    }
}
