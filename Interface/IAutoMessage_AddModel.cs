﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface IAutoMessage_AddModel : IBaseModel<AutoMessage_Add>
    {
        AutoMessage_Add GetInfo(int AccountMainID);
    }
}
