using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface ISurveyMainModel : IBaseModel<SurveyMain>
    {
        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <param name="AccountMainID"></param>
        /// <returns></returns>
        IQueryable<SurveyMain> GetList(int AccountMainID);

        /// <summary>
        /// 列表首页
        /// </summary>
        /// <param name="AccountMainID"></param>
        /// <returns></returns>
        IQueryable<_B_SurveyMain> SelList(int AccountMainID);

        /// <summary>
        /// 删除调查问卷 级联删除
        /// </summary>
        /// <param name="MainID"></param>
        /// <param name="AccountMainID"></param>
        /// <returns></returns>
        Result DelSurveyMain(int MainID, int AccountMainID);

        /// <summary>
        /// 根据ID获取调查问卷信息
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="AccountMainID"></param>
        /// <returns></returns>
        SurveyMain GetSurveyMainByID(int ID, int AccountMainID);

        /// <summary>
        /// 设置状态
        /// </summary>
        /// <param name="MainID"></param>
        /// <param name="AccountMainID"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        Result SetMainStatus(int MainID, int AccountMainID,int status);
    }
}
