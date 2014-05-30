using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interface.MerchantInterface;
using Poco.MerchantPoco;
using Injection.Transaction;
using Poco;
using System.Web;
using System.IO;
using Common;

namespace Business.MerchantBusiness
{
    /// <summary>
    /// 家政服务
    /// </summary>
    public class M_DomesticModel : BaseModel<M_Domestic>, IM_DomesticModel
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="MID"></param>
        /// <returns></returns>
        public IQueryable<M_Domestic> GetListByMID(int MID)
        {
            var list = List().Where(a => a.MerchantID == MID).OrderByDescending(a => a.CreatDate);
            return list;
        }


        /// <summary>
        /// 添加信息
        /// </summary>
        /// <param name="m_unlock"></param>
        /// <param name="communityIDs"></param>
        /// <returns></returns>
        [Transaction]
        public Result AddInfo(M_Domestic m_domestic, int[] communityIDs, int x1, int y1, int width, int height, int Twidth, int Theight)
        {
            List<M_CommunityMapping> list = new List<M_CommunityMapping>();
            foreach (var item in communityIDs)
            {
                M_CommunityMapping mm = new M_CommunityMapping();
                mm.AccountMainID = item;
                list.Add(mm);
            }
            m_domestic.M_CommunityMappings = list;

            CommonModel com = new CommonModel();
            var LastName = com.CreateRandom("", 5) + m_domestic.MainImage.GetFileSuffix();
            var path = string.Format(SystemConst.Business.MerchantFile, m_domestic.MerchantID);
            var accountPath = HttpContext.Current.Server.MapPath(path);
            var token = DateTime.Now.ToString("yyyyMMddHHmmss");

            var imageName = string.Format("{0}_{1}", token, LastName);
            var imagePath = string.Format("{0}\\{1}", accountPath, imageName);
            var imageName2 = string.Format("{0}Y_{1}", token, LastName);
            var imagePath2 = string.Format("{0}\\{1}", accountPath, imageName2);
            var imageshowName = string.Format("{0}_{1}_{2}", token, "show", LastName);
            var imageshowPath = string.Format("{0}\\{1}", accountPath, imageshowName);


            var lsImgPath = m_domestic.MainImage;
            var lsImaFilePath = HttpContext.Current.Server.MapPath(lsImgPath);

            Tool.SuperGetPicThumbnail(lsImaFilePath, imagePath, 70, 640, 0, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic);


            Tool.SuperGetPicThumbnailJT(lsImaFilePath, imagePath2, 70, width, height, x1, y1, Twidth, Theight, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic);

            Tool.SuperGetPicThumbnail(imagePath2, imageshowPath, 70, 480, 0, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic);

            if (File.Exists(imagePath2))
            {
                File.Delete(imagePath2);
            }

            m_domestic.MainImage= path + imageName;
            m_domestic.ShowImage = path + imageshowName;


            //保存商户发布信息和小区映射表
            Result result = base.Add(m_domestic);
            return result;
        }
    }
}
