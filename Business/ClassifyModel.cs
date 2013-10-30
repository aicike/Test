using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;

namespace Business
{
    public class ClassifyModel : BaseModel<Classify>, IClassifyModel
    {
        /// <summary>
        /// 根据上级ID 与客户ID 查询分类
        /// </summary>
        /// <param name="ParentID"></param>
        /// <param name="AccountMainID"></param>
        /// <returns></returns>
        public IQueryable<Classify> GetLastClass(int ParentID, int AccountMainID)
        {
            var classifyModel = List().Where(a=>a.AccountMainID==AccountMainID && a.ParentID ==ParentID);
            return classifyModel;
        }


        public int AddClass(int PID, int Level, int AccountMainID, string Name)
        {
            string sql = string.Format("insert into classify(SystemStatus,AccountMainID,Name,ParentID,[Level],sort,Subordinate) values(0,{0},'{1}',{2},{3},0,0)", AccountMainID, Name, PID, Level + 1);
            return base.SqlExecute(sql);
        }


        public IQueryable<Classify> GetClassByPID(int PID)
        {
            var classifyModel = List().Where(a=>a.ParentID == PID);
            return classifyModel;
        }


        public IQueryable<Classify> GetLastClass(int ParentID, int AccountMainID, int NoID)
        {
            var classifyModel = List().Where(a => a.AccountMainID == AccountMainID && a.ParentID == ParentID && a.ID != NoID);
            return classifyModel;
        }


        public int UpdClass(int PID, int ID, string Name, int AccountMainID)
        {
            string sql = string.Format("update classify set ParentID={0},Name='{1}' where ID = {2} and AccountMainID = {3}", PID, Name, ID, AccountMainID);
            return base.SqlExecute(sql);
        }


        public bool GetIsMainNode(int ID)
        {
            var Class = List().Where(a => a.ID == ID && a.ParentID==0);
            if (Class.Count() > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// App获取一级分类列表，及分类下产品
        /// </summary>
        /// <param name="accountMainID"></param>
        public List<Classify> Get1levelClass(int accountMainID)
        {
            var parentIDs = GetLastClass(0, accountMainID).Select(a => a.ID);
            var list = List().Where(a =>parentIDs.Contains(a.ParentID));
            return list.ToList();
        }
    }
}
