using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interface;
using Injection;
using Poco;

namespace Business
{
    public class LookupFactory
    {
        private LookupFactory() { }

        protected static ILookupOptionModel LookupOptionModel { get; set; }

        public static int GetLookupOptionIdByToken(Enum value)
        {
            return LookupOptionModel.GetIdByToken(value);
        }
    }
}
