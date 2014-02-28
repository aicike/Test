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

        /// <summary>
        /// 修改生成资讯状态
        /// </summary>
        /// <param name="id">活动ID</param>
        /// <param name="EnumAdvertorialUType">客户端 EnumAdvertorialUType</param>
        /// <param name="Type">状态 0 未生成 1生成</param>
        /// <returns></returns>
        Result Update_GenerateType(int id, int EnumAdvertorialUType, int Type);


        /// <summary>
        /// 添加调查问卷
        /// </summary>
        /// <param name="surveymain"></param>
        /// <param name="width">当前截图宽度</param>
        /// <param name="height">当前截图高度</param>
        /// <param name="x1">当前截图x坐标</param>
        /// <param name="y1">当前截图y坐标</param>
        /// <param name="Twidth">当前截图显示宽度</param>
        /// <param name="Theight">当前截图显示高度</param>
        /// <returns></returns>
        Result AddSurveyMain(SurveyMain surveymain, int w, int h, int x1, int y1, int tw, int th);

        /// <summary>
        /// 修改调查问卷
        /// </summary>
        /// <param name="surveymain"></param>
        /// <param name="width">当前截图宽度</param>
        /// <param name="height">当前截图高度</param>
        /// <param name="x1">当前截图x坐标</param>
        /// <param name="y1">当前截图y坐标</param>
        /// <param name="Twidth">当前截图显示宽度</param>
        /// <param name="Theight">当前截图显示高度</param>
        /// <returns></returns>
        Result EditSurveyMain(SurveyMain surveymain, int w, int h, int x1, int y1, int tw, int th);
    }
}
