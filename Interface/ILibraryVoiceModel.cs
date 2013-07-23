using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using System.Web;

namespace Interface
{
    public interface ILibraryVoiceModel : IBaseModel<LibraryVoice>, ILibraryModel<LibraryVoice>
    {
        Result Upload(LibraryVoice entity, HttpPostedFileBase voice);

        Result ReName(int id, string name);
    }
}