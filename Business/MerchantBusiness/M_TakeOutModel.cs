using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco.MerchantPoco;
using Interface.MerchantInterface;
using Poco;
using Injection.Transaction;
using System.Data.Entity;
using Poco.Enum;
using System.Web;
using Common;
using System.IO;

namespace Business.MerchantBusiness
{
    public class M_TakeOutModel : BaseModel<M_TakeOut>, IM_TakeOutModel
    {
        public IQueryable<M_TakeOut> ListByMerchantID(int merchantID)
        {
            return List().Where(a => a.MerchantID == merchantID).OrderByDescending(a => a.CreatDate);
        }

        [Transaction]
        public Result Add(M_TakeOut entity, int[] communityIDs,int w, int h, int x1, int y1, int tw, int th)
        {
            try
            {
                var path = string.Format(SystemConst.Business.MerchantFile, entity.MerchantID);
                var accountPath = HttpContext.Current.Server.MapPath(path);
                var token = DateTime.Now.ToString("yyyyMMddHHmmss");
                CommonModel com = new CommonModel();
                var LastName = com.CreateRandom("", 5) + entity.ImagePath.GetFileSuffix();
                var imageName = string.Format("{0}_{1}", token, LastName);
                var imagePath = string.Format("{0}\\{1}", accountPath, imageName);
                var imageName2 = string.Format("{0}Y_{1}", token, LastName);
                var imagePath2 = string.Format("{0}\\{1}", accountPath, imageName2);
                var imageminiName = string.Format("{0}_{1}_{2}", token, "mini", LastName);
                var imageminiPath = string.Format("{0}\\{1}", accountPath, imageminiName);
                var imageshowName = string.Format("{0}_{1}_{2}", token, "show", LastName);
                var imageshowPath = string.Format("{0}\\{1}", accountPath, imageshowName);

                var lsImgPath = entity.ImagePath;
                if (!string.IsNullOrEmpty(lsImgPath))
                {
                    var lsImaFilePath = HttpContext.Current.Server.MapPath(lsImgPath);
                    Tool.SuperGetPicThumbnail(lsImaFilePath, imagePath, 70, 640, 0, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic);


                    Tool.SuperGetPicThumbnailJT(lsImaFilePath, imagePath2, 70, w, h, x1, y1, tw, th, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic);

                    Tool.SuperGetPicThumbnail(imagePath2, imageshowPath, 70, 480, 0, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic);

                    if (File.Exists(imagePath2))
                    {
                        File.Delete(imagePath2);
                    }

                    //缩略图mini
                    Tool.SuperGetPicThumbnail(imageshowPath, imageminiPath, 70, 200, 0, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic);

                    entity.ImagePath= path + imageshowName;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }



            List<M_CommunityMapping> list = new List<M_CommunityMapping>();
            foreach (var item in communityIDs)
            {
                M_CommunityMapping mm = new M_CommunityMapping();
                mm.AccountMainID = item;
                list.Add(mm);
            }
            entity.M_CommunityMappings = list;
            //保存商户发布信息和小区映射表
            Result result = base.Add(entity);
            return result;
        }

        [Transaction]
        public Result Edit(M_TakeOut entity, int[] communityIDs)
        {
            var newEntity = List().Where(a => a.ID == entity.ID).AsNoTracking().FirstOrDefault();
            newEntity.Title = entity.Title;
            newEntity.TakeOutPrice = entity.TakeOutPrice;
            newEntity.EnumDataStatus = entity.EnumDataStatus;
            newEntity.Phone = entity.Phone;
            string sql = "DELETE dbo.M_CommunityMapping WHERE M_TakeOutID=" + entity.ID;
            base.SqlExecute(sql);
            Result result = base.Edit(newEntity);

            StringBuilder sql_add = new StringBuilder();
            sql_add.Append("INSERT INTO dbo.M_CommunityMapping( SystemStatus , AccountMainID , M_TakeOutID )");


            List<M_CommunityMapping> list = new List<M_CommunityMapping>();
            for (int i = 0; i < communityIDs.Length; i++)
            {
                if (i + 1 == communityIDs.Length)
                {
                    sql_add.AppendFormat(" SELECT 0,{0},{1} ", communityIDs[i], newEntity.ID);
                }
                else
                {
                    sql_add.AppendFormat(" SELECT 0,{0},{1} UNION ALL ", communityIDs[i], newEntity.ID);
                }
            }
            base.SqlExecute(sql_add.ToString());
            return result;
        }

        [Transaction]
        public new Result Delete(int id)
        {
            string sql1 = "DELETE dbo.M_CommunityMapping WHERE M_TakeOutID=" + id;//删除所有小区映射
            string sql2 = "SELECT * FROM dbo.M_TakeOutDetail WHERE M_TakeOutID=" + id;//删除子表
            string sql3 = "";//删除订单等
            base.SqlExecute(sql1);
            base.SqlExecute(sql2);
            var result = base.CompleteDelete(id);
            return result;
        }

        [Transaction]
        public Result AddDetail(int id, int LoginMerchantID, List<M_TakeOutDetail> list, string content)
        {
            Result result = new Result();
            var newEntity = List().Where(a => a.ID == id && a.MerchantID == LoginMerchantID).AsNoTracking().FirstOrDefault();
            if (newEntity == null)
            {
                result.Error = "参数错误，操作失败。";
                return result;
            }
            newEntity.Content = content;
            newEntity.EnumDataStatus = (int)EnumDataStatus.None;
            newEntity.IsPublish = false;
            result = Edit(newEntity);
            if (result.HasError)
            {
                return result;
            }
            //添加商品详细
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("DELETE M_TakeOutDetail WHERE M_TakeOutID={0} INSERT INTO dbo.M_TakeOutDetail(SystemStatus ,M_TakeOutID ,Title ,Price) ", id);
            for (int i = 0; i < list.Count; i++)
            {
                if (i + 1 == list.Count)
                {
                    sql.AppendFormat("SELECT 0,{0},'{1}',{2} ", id, list[i].Title, list[i].Price);
                }
                else
                {
                    sql.AppendFormat("SELECT 0,{0},'{1}',{2} UNION ALL ", id, list[i].Title, list[i].Price);
                }
            }
            base.SqlExecute(sql.ToString());
            return result;
        }
    }
}
