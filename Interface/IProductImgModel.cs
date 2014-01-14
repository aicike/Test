using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface IProductImgModel : IBaseModel<ProductImg>
    {
        /// <summary>
        /// 添加图片地址信息
        /// </summary>
        /// <param name="ProductID"></param>
        /// <param name="paths"></param>
        /// <returns></returns>
        Result AddImgInfo(int ProductID, List<string> paths, int AMID);

        /// <summary>
        /// 删除图片
        /// </summary>
        /// <param name="ProductID"></param>
        /// <param name="AMID"></param>
        /// <returns></returns>
        Result DelImgInfo(int ProductID, int AMID);

        /// <summary>
        /// 修改图片地址信息
        /// </summary>
        /// <param name="ProductID"></param>
        /// <param name="paths"></param>
        /// <returns></returns>
        Result EditImgInfo(int ProductID, List<string> paths, int AMID);
    }
}
