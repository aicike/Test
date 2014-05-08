using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    /// <summary>
    /// 生活技巧
    /// </summary>
    public interface ILifeSkillModel : IBaseModel<LifeSkill>
    {
         /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        IQueryable<LifeSkill> GetList();

        /// <summary>
        /// 获取详细信息
        /// </summary>
        /// <param name="AID"></param>
        /// <param name="AMID"></param>
        /// <returns></returns>
        LifeSkill GetInfo(int LID);


        /// <summary>
        /// 更改阅读次数
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        void BrowseCntADD(int id);

        /// <summary>
        /// 更改状态
        /// </summary>
        /// <param name="RID"></param>
        /// <param name="Release"></param>
        /// <returns></returns>
        Result UpSatausByID(int RID, bool Release);

         /// <summary>
        /// 添加生活技巧
        /// </summary>
        /// <param name="recipes"></param>
        /// <param name="w">当前截图宽度</param>
        /// <param name="h">当前截图高度</param>
        /// <param name="x1">当前截图x坐标</param>
        /// <param name="y1">当前截图y坐标</param>
        /// <param name="Twidth">当前截图显示宽度</param>
        /// <param name="Theight">当前截图显示高度</param>
        /// <returns></returns>
        Result AddLifeSkill(LifeSkill lifeskill, int w, int h, int x1, int y1, int tw, int th);


        /// <summary>
        /// 修改生活技巧
        /// </summary>
        /// <param name="recipes"></param>
        /// <param name="w">当前截图宽度</param>
        /// <param name="h">当前截图高度</param>
        /// <param name="x1">当前截图x坐标</param>
        /// <param name="y1">当前截图y坐标</param>
        /// <param name="Twidth">当前截图显示宽度</param>
        /// <param name="Theight">当前截图显示高度</param>
        /// <returns></returns>
        Result EditLifeSkill(LifeSkill lifeskill, int w, int h, int x1, int y1, int tw, int th);

        Result DelLifeSkill(int ID);
    }
}
