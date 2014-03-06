using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interface;
using Poco;

namespace Business
{
    public class Push_IOS : IPushModel
    {

        public static Result SendMessage(PushMessage pushMessage, params string[] clientIDs)
        {

            return null;
        }
    }
}
