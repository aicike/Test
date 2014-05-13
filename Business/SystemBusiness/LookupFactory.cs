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

        public static LookupOption GetLookupOptionByToken(Enum value)
        {
            return LookupOptionModel.GetByToken(value);
        }

        public static List<LookupOption> GetLookupOptionList(Type emumType)
        {
            string typeName=emumType.Name;
            return LookupOptionModel.List().Where(a => a.Lookup.Token.Equals(typeName, StringComparison.CurrentCultureIgnoreCase)).ToList();
        }

    }
}
