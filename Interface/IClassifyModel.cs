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

        //添加分类
        Result AddClass(Classify classify);

        IQueryable<Classify> GetClassByPID(int PID);

        //修改分类
        Result UpdClass(Classify classify);

        bool GetIsMainNode(int ID);

        List<Classify> Get1levelClass(int accountMainID);

        Result DelClassify(int ID, int AMID);

        /// <summary>
        /// 获取所有一级分类
        /// </summary>
        /// <param name="AccountMainID"></param>
        /// <returns></returns>
        IQueryable<Classify> GetOneLevel(int AccountMainID);

        /// <summary>
        /// 根据ID获取该类别下的所有子ID(递归)
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        string GetTypeSUBID(int ID, int AccountMainID);
    }
}
