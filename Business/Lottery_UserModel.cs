﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;
using Injection.Transaction;

namespace Business
{
    public class Lottery_UserModel : BaseModel<Lottery_User>, ILottery_UserModel
    {
    }
}
