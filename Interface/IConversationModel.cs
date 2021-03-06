﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface IConversationModel : IBaseModel<Conversation>
    {
        /// <summary>
        /// 查找会话ID
        /// </summary>
        /// <param name="AccountMainID">售楼部ID</param>
        /// <param name="AID">售楼用户ID 当Ctype为2时 此字段存用户ID</param>
        /// <param name="UID">用户ID 当Ctype为1时 此字段存售楼用户ID</param>
        /// <param name="Ctype">会话类型 0 ：售楼部与用户间对话，1：售楼部与售楼部间对话 ，2：用户与用户间对话</param>
        /// <returns></returns>
        int GetCID(string AccountMainID, string AID, string UID, string Ctype);



        /// <summary>
        /// 删除用户与销售代表间的会话ID
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        Result DelCID(string uid, string AID, string AccountMainID);

        /// <summary>
        /// 启用用户与销售代表间的会话ID
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="AID"></param>
        /// <param name="AccountMainID"></param>
        /// <returns></returns>
        Result StartCID(string uid, string AID, string AccountMainID);

    }
}
