using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Poco
{
    public class Result
    {
        public Result() { }

        public Result(string error)
        {
            this.Error = error;
        }

        private bool hasError = false;

        public bool HasError
        {
            get { return hasError; }
            set { hasError = value; }
        }

        private string error = null;
        public string Error
        {
            get { return error; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    HasError = true; error = value;
                }
            }
        }

        public object Entity { get; set; }
    }
}
