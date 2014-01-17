using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface IClassifyModel : IBaseModel<Classify>
    {
        IQueryable<Classify> GetLastClass(int ParentID, int AccountMainID);

        IQueryable<Classify> GetLastClass(int ParentID, int AccountMainID,int NoID);

        int AddClass(int PID, int Level, int AccountMainID, string Name,string imgpath1);

        IQueryable<Classify> GetClassByPID(int PID);

        int UpdClass(int PID, int ID, string Name, int AccountMainID, string imgpath2);

        bool GetIsMainNode(int ID);

        List<Classify> Get1levelClass(int accountMainID);

        Result DelClassify(int ID, int AMID);

        /// <summary>
        /// 获取所有一级分类
        /// </summary>
        /// <param name="AccountMainID"></param>
        /// <returns></returns>
        IQueryable<Classify> GetOneLevel(int AccountMainID);
    }
}
