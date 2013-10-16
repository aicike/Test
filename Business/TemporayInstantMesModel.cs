using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interface;
using Poco;
using Injection;

namespace Business
{
    public class TemporayInstantMesModel : BaseModel<TemporayInstantMes>, ITemporayInstantMesModel
    {
        public IQueryable<TemporayInstantMes> GetList(int SID)
        {
            var ConversationModel = Factory.Get<IConversationModel>(SystemConst.IOC_Model.ConversationModel);
            var ConverCID = ConversationModel.GetAllCID("s",SID);
            string cids="";
            foreach (var item in ConverCID)
            {
                cids += item.ID+",";
            }
            cids = cids.TrimEnd(',');
            string sql = "";
            if (cids == "")
            {
                sql = @"select * from View_AccountMessageList where ConversationID =0";
            }
            else
            { 
                sql = @"select * from View_AccountMessageList where ConversationID in(" + cids + ")";
            }
            return base.SqlQuery(sql);
        }



        
    }
}
