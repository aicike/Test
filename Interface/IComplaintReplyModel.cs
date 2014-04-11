using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface IComplaintReplyModel : IBaseModel<ComplaintReply>
    {
        /// <summary>
        /// 根据投诉ID 获取投诉答复
        /// </summary>
        /// <param name="CID"></param>
        /// <returns></returns>
        IQueryable<ComplaintReply> GetList(int CID);

         /// <summary>
        /// 增加答复
        /// </summary>
        /// <param name="CID"></param>
        /// <param name="Contetn"></param>
        /// <param name="AccountID"></param> 
        /// <returns></returns>
        Result AddInfo(int CID, string Content, int AccountID);
    }
}
