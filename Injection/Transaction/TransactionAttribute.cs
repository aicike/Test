using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Injection.Transaction
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class TransactionAttribute : Attribute
    {
        
    }
}
