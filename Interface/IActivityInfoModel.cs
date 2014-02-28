using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface IActivityInfoModel : IBaseModel<ActivityInfo>
    {

        /// <summary>
        /// 根据 AMID 获取列表
        /// </summary>
        /// <param name="AMID"></param>
        /// <returns></returns>
        IQueryable<ActivityInfo> GetListByAMID(int AMID);


         /// <summary>
        /// 设置状态
        /// </summary>
        /// <param name="AID"></param>
        /// <param name="AccountMainID"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        Result SetMainStatus(int AID, int AccountMainID, int status);

        /// <summary>
        /// 根据ID获取活动信息
        /// </summary>
        /// <param name="AID"></param>
        /// <param name="AccountMainID"></param>
        /// <returns></returns>
        ActivityInfo GetActivityByID(int AID, int AccountMainID);

        /// <summary>
        /// 删除活动 级联删除
        /// </summary>
        /// <param name="AID"></param>
        /// <param name="AccountMainID"></param>
        /// <returns></returns>
        Result DeleteActivityByID(int AID, int AccountMainID);

         /// <summary>
        /// 修改生成资讯状态
        /// </summary>
        /// <param name="id">活动ID</param>
        /// <param name="EnumAdvertorialUType">客户端 EnumAdvertorialUType</param>
        /// <param name="Type">状态 0 未生成 1生成</param>
        /// <returns></returns>
        Result Update_GenerateType(int id, int EnumAdvertorialUType, int Type);


         /// <summary>
        /// 添加 活动
        /// </summary>
        /// <param name="activityinfo"></param>
        /// <param name="width">当前截图宽度</param>
        /// <param name="height">当前截图高度</param>
        /// <param name="x1">当前截图x坐标</param>
        /// <param name="y1">当前截图y坐标</param>
        /// <param name="Twidth">当前截图显示宽度</param>
        /// <param name="Theight">当前截图显示高度</param>
        /// <returns></returns>
        Result AddActivity(ActivityInfo activityinfo, int x1, int y1, int width, int height, int Twidth, int Theight);


        /// <summary>
        /// 修改 活动
        /// </summary>
        /// <param name="activityinfo"></param>
        /// <param name="width">当前截图宽度</param>
        /// <param name="height">当前截图高度</param>
        /// <param name="x1">当前截图x坐标</param>
        /// <param name="y1">当前截图y坐标</param>
        /// <param name="Twidth">当前截图显示宽度</param>
        /// <param name="Theight">当前截图显示高度</param>
        /// <returns></returns>
        Result EditActivity(ActivityInfo activityinfo, int x1, int y1, int width, int height, int Twidth, int Theight);
    }
}
