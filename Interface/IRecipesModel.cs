using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    /// <summary>
    /// 每日食谱
    /// </summary>
    public interface IRecipesModel : IBaseModel<Recipes>
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="AccountMainID"></param>
        /// <returns></returns>
        IQueryable<Recipes> GetList();

        /// <summary>
        /// 获取详细信息
        /// </summary>
        /// <param name="AID"></param>
        /// <param name="AMID"></param>
        /// <returns></returns>
        Recipes GetInfo(int RID);


        /// <summary>
        /// 更改阅读次数
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        void BrowseCntADD(int id);

         /// <summary>
        /// 添加食谱
        /// </summary>
        /// <param name="recipes"></param>
        /// <param name="w">当前截图宽度</param>
        /// <param name="h">当前截图高度</param>
        /// <param name="x1">当前截图x坐标</param>
        /// <param name="y1">当前截图y坐标</param>
        /// <param name="Twidth">当前截图显示宽度</param>
        /// <param name="Theight">当前截图显示高度</param>
        /// <returns></returns>
        Result AddRecipes(Recipes recipes, int w, int h, int x1, int y1, int tw, int th);

        /// <summary>
        /// 修改食谱
        /// </summary>
        /// <param name="recipes"></param>
        /// <param name="w">当前截图宽度</param>
        /// <param name="h">当前截图高度</param>
        /// <param name="x1">当前截图x坐标</param>
        /// <param name="y1">当前截图y坐标</param>
        /// <param name="Twidth">当前截图显示宽度</param>
        /// <param name="Theight">当前截图显示高度</param>
        /// <returns></returns>
        Result EditRecipes(Recipes recipes, int w, int h, int x1, int y1, int tw, int th);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        Result DelRecipes(int ID);

         /// <summary>
        /// 更改状态
        /// </summary>
        /// <param name="RID"></param>
        /// <param name="Release"></param>
        /// <returns></returns>
        Result UpSatausByID(int RID, bool Release);
    }
}
