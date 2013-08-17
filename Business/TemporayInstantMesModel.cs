using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interface;
using Poco;

namespace Business
{
    public class TemporayInstantMesModel : BaseModel<TemporayInstantMes>, ITemporayInstantMesModel
    {
        public IQueryable<TemporayInstantMes> GetList(int SID)
        {
            string sql = @"select  x.*,y.Name as UMark,z.Name,z.HeadImagePath from 
                        (
                        select a.FromUserID ,max(SendTime) MData,
                        (select count(id) from [Message] where ToAccountID= " + SID + @"  and IsReceive=0) as NoSend,
                        (select textContent from [Message] where id = max(a.id)) as Tcontent
                        from dbo.[Message] a where ToAccountID= " + SID + @"  group by FromUserID
                        ) x,[User] y,UserLoginInfo z where x.FromUserID = y.id and y.UserLoginInfoID= z.ID order by x.MData desc";

            return base.SqlQuery(sql);
        }

    }
}
