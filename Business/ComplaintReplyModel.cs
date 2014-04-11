using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;
using Injection.Transaction;
using Injection;
using Poco.Enum;

namespace Business
{
    public class ComplaintReplyModel : BaseModel<ComplaintReply>, IComplaintReplyModel
    {

        /// <summary>
        /// 根据投诉ID 获取投诉答复
        /// </summary>
        /// <param name="CID"></param>
        /// <returns></returns>
        public IQueryable<ComplaintReply> GetList(int CID)
        {
            var list = List().Where(a => a.ComplaintID == CID).OrderByDescending(a => a.ReplyDate);
            return list;
        }


        /// <summary>
        /// 增加答复
        /// </summary>
        /// <param name="CID"></param>
        /// <param name="Contetn"></param>
        /// <param name="AccountID"></param> 
        /// <returns></returns>
        [Transaction]
        public Result AddInfo(int CID, string Content, int AccountID)
        {
            Result reuslt = new Result();
            ComplaintReply cmr = new ComplaintReply();
            cmr.AccountID = AccountID;
            cmr.ComplaintID = CID;
            cmr.ReplyContent = Content;
            cmr.ReplyDate = DateTime.Now;

            if (!base.Add(cmr).HasError)
            {
                var complaintModel = Factory.Get<IComplaintModel>(SystemConst.IOC_Model.ComplaintModel);
                reuslt = complaintModel.UpdStatus(CID, (int)EnumComplaintStatus.completed);
            }
            return reuslt;
        }
    }
}
