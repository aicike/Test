using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    /// <summary>
    /// 房屋租赁
    /// </summary>
    public interface IRentalHouseModel : IBaseModel<RentalHouse>
    {

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="AMID"></param>
        /// <returns></returns>
        IQueryable<RentalHouse> GetList(int AMID);


        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="AMID"></param>
        /// <returns></returns>
        RentalHouse GetInfo(int RID, int AMID);

        /// <summary>
        ///  添加数据
        /// </summary>
        /// <param name="rentalhouse"></param>
        /// <param name="w">当前截图宽度</param>
        /// <param name="h">当前截图高度</param>
        /// <param name="x1">当前截图x坐标</param>
        /// <param name="y1">当前截图y坐标</param>
        /// <param name="tw">当前截图显示宽度</param>
        /// <param name="th">当前截图显示高度</param>
        /// <returns></returns>
        Result AddInfo(RentalHouse rentalhouse, int w, int h, int x1, int y1, int tw, int th);


        /// <summary>
        ///  修改数据
        /// </summary>
        /// <param name="rentalhouse"></param>
        /// <param name="w">当前截图宽度</param>
        /// <param name="h">当前截图高度</param>
        /// <param name="x1">当前截图x坐标</param>
        /// <param name="y1">当前截图y坐标</param>
        /// <param name="Twidth">当前截图显示宽度</param>
        /// <param name="Theight">当前截图显示高度</param>
        /// <returns></returns>
        Result EditInfo(RentalHouse rentalhouse, int w, int h, int x1, int y1, int tw, int th);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="RID"></param>
        /// <param name="AMID"></param>
        /// <returns></returns>
        Result Delete(int RID, int AMID);
    }
}
