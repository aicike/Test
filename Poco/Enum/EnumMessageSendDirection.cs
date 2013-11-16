using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco.Enum
{
    public enum EnumMessageSendDirection
    {
        Account_User,
        User_Account,
        Account_Account,
        User_User,
        /// <summary>
        /// 群聊
        /// </summary>
        GroupChat
    }
}
