using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interface.MerchantInterface;
using Poco.MerchantPoco;
using Injection.Transaction;
using Poco;
using System.Web;
using Common;
using System.IO;

namespace Business.MerchantBusiness
{
    /// <summary>
    /// 宠物医院
    /// </summary>
    public class M_PetHospitalModel : BaseModel<M_PetHospital>, IM_PetHospitalModel
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="MID"></param>
        /// <returns></returns>
        public IQueryable<M_PetHospital> GetListByMID(int MID)
        {
            var list = List().Where(a => a.MerchantID == MID).OrderByDescending(a => a.CreatDate);
            return list;
        }

        /// <summary>
        /// 根据ID获取详细信息
        /// </summary>
        /// <param name="MID"></param>
        /// <param name="UID"></param>
        /// <returns></returns>
        public M_PetHospital GetInfoByID(int MID, int UID)
        {
            var item = List().Where(a => a.MerchantID == MID && a.ID == UID).FirstOrDefault();
            return item;

        }

        /// <summary>
        /// 查询数据 审核数据
        /// </summary>
        /// <param name="EnumDataStatus"></param>
        /// <param name="CreatDate"></param>
        /// <param name="MName"></param>
        /// <returns></returns>
        public IQueryable<M_PetHospital> GetListByStatus(int? EnumDataStatus, string CreatDate, string MName)
        {
            var list = List().Where(a => a.EnumDataStatus != (int)Poco.Enum.EnumDataStatus.None);
            if (EnumDataStatus.HasValue)
            {
                list = list.Where(a => a.EnumDataStatus == EnumDataStatus.Value);
            }

            if (!string.IsNullOrEmpty(MName))
            {
                list = list.Where(a => a.Merchant.Name.Contains(MName));
            }
            if (!string.IsNullOrEmpty(CreatDate))
            {
                var Createdate1 = Convert.ToDateTime(CreatDate);
                var Createdate2 = Createdate1.AddDays(1);
                list = list.Where(a => a.CreatDate >= Createdate1 && a.CreatDate < Createdate2);
            }
            return list.OrderByDescending(a => a.CreatDate);
        }

        /// <summary>
        /// 添加信息
        /// </summary>
        /// <param name="m_unlock"></param>
        /// <param name="communityIDs"></param>
        /// <returns></returns>
        [Transaction]
        public Result AddInfo(M_PetHospital m_pethospital, int[] communityIDs, int x1, int y1, int width, int height, int Twidth, int Theight)
        {
            List<M_CommunityMapping> list = new List<M_CommunityMapping>();
            foreach (var item in communityIDs)
            {
                M_CommunityMapping mm = new M_CommunityMapping();
                mm.AccountMainID = item;
                list.Add(mm);
            }
            m_pethospital.M_CommunityMappings = list;

            CommonModel com = new CommonModel();
            var LastName = com.CreateRandom("", 5) + m_pethospital.MainImage.GetFileSuffix();
            var path = string.Format(SystemConst.Business.MerchantFile, m_pethospital.MerchantID);
            var accountPath = HttpContext.Current.Server.MapPath(path);
            var token = DateTime.Now.ToString("yyyyMMddHHmmss");

            var imageName = string.Format("{0}_{1}", token, LastName);
            var imagePath = string.Format("{0}\\{1}", accountPath, imageName);
            var imageName2 = string.Format("{0}Y_{1}", token, LastName);
            var imagePath2 = string.Format("{0}\\{1}", accountPath, imageName2);
            var imageshowName = string.Format("{0}_{1}_{2}", token, "show", LastName);
            var imageshowPath = string.Format("{0}\\{1}", accountPath, imageshowName);


            var lsImgPath = m_pethospital.MainImage;
            var lsImaFilePath = HttpContext.Current.Server.MapPath(lsImgPath);

            Tool.SuperGetPicThumbnail(lsImaFilePath, imagePath, 70, 640, 0, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic);


            Tool.SuperGetPicThumbnailJT(lsImaFilePath, imagePath2, 70, width, height, x1, y1, Twidth, Theight, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic);

            Tool.SuperGetPicThumbnail(imagePath2, imageshowPath, 70, 480, 0, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic);

            if (File.Exists(imagePath2))
            {
                File.Delete(imagePath2);
            }

            m_pethospital.MainImage = path + imageName;
            m_pethospital.ShowImage = path + imageshowName;


            //保存商户发布信息和小区映射表
            Result result = base.Add(m_pethospital);
            return result;
        }


        /// <summary>
        /// 修改信息
        /// </summary>
        /// <param name="m_unlock"></param>
        /// <param name="communityIDs"></param>
        /// <returns></returns>
        [Transaction]
        public Result EditInfo(M_PetHospital m_pethospital, int[] communityIDs, int x1, int y1, int width, int height, int Twidth, int Theight)
        {
            Result result = new Result();
            var Ym_pethospital = base.Get(m_pethospital.ID);

            if (Ym_pethospital.MainImage != m_pethospital.MainImage)
            {
                if (width > 0)
                {
                    CommonModel com = new CommonModel();
                    var LastName = com.CreateRandom("", 5) + m_pethospital.MainImage.GetFileSuffix();
                    var path = string.Format(SystemConst.Business.MerchantFile, m_pethospital.MerchantID);
                    var accountPath = HttpContext.Current.Server.MapPath(path);
                    var token = DateTime.Now.ToString("yyyyMMddHHmmss");

                    var imageName = string.Format("{0}_{1}", token, LastName);
                    var imagePath = string.Format("{0}\\{1}", accountPath, imageName);
                    var imageName2 = string.Format("{0}Y_{1}", token, LastName);
                    var imagePath2 = string.Format("{0}\\{1}", accountPath, imageName2);
                    var imageshowName = string.Format("{0}_{1}_{2}", token, "show", LastName);
                    var imageshowPath = string.Format("{0}\\{1}", accountPath, imageshowName);


                    var lsImgPath = m_pethospital.MainImage;
                    var lsImaFilePath = HttpContext.Current.Server.MapPath(lsImgPath);

                    Tool.SuperGetPicThumbnail(lsImaFilePath, imagePath, 70, 640, 0, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic);


                    Tool.SuperGetPicThumbnailJT(lsImaFilePath, imagePath2, 70, width, height, x1, y1, Twidth, Theight, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic);

                    Tool.SuperGetPicThumbnail(imagePath2, imageshowPath, 70, 480, 0, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic);

                    if (File.Exists(imagePath2))
                    {
                        File.Delete(imagePath2);
                    }

                    m_pethospital.MainImage = path + imageName;
                    m_pethospital.ShowImage = path + imageshowName;

                    try
                    {
                        string Ypath = HttpContext.Current.Server.MapPath(Ym_pethospital.MainImage);
                        if (File.Exists(Ypath))
                        {
                            File.Delete(Ypath);
                        }
                        Ypath = HttpContext.Current.Server.MapPath(Ym_pethospital.ShowImage);
                        if (File.Exists(Ypath))
                        {
                            File.Delete(Ypath);
                        }
                    }
                    catch { }
                }
            }

            string sql = "DELETE dbo.M_CommunityMapping WHERE M_PetHospitalID=" + m_pethospital.ID;
            base.SqlExecute(sql);

            result = base.Edit(m_pethospital);


            StringBuilder sql_add = new StringBuilder();
            sql_add.Append("INSERT INTO dbo.M_CommunityMapping( SystemStatus , AccountMainID , M_PetHospitalID )");

            List<M_CommunityMapping> list = new List<M_CommunityMapping>();
            for (int i = 0; i < communityIDs.Length; i++)
            {
                if (i + 1 == communityIDs.Length)
                {
                    sql_add.AppendFormat("SELECT 0,{0},{1} ", communityIDs[i], m_pethospital.ID);
                }
                else
                {
                    sql_add.AppendFormat("SELECT 0,{0},{1} UNION ALL ", communityIDs[i], m_pethospital.ID);
                }
            }
            base.SqlExecute(sql_add.ToString());
            return result;
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="UID"></param>
        /// <param name="MID"></param>
        /// <returns></returns>
        [Transaction]
        public Result DeleteInfo(int PID, int MID)
        {
            var Ym_domestic = base.Get(PID);

            try
            {
                string Ypath = HttpContext.Current.Server.MapPath(Ym_domestic.MainImage);
                if (File.Exists(Ypath))
                {
                    File.Delete(Ypath);
                }
                Ypath = HttpContext.Current.Server.MapPath(Ym_domestic.ShowImage);
                if (File.Exists(Ypath))
                {
                    File.Delete(Ypath);
                }
            }
            catch { }
            string sql2 = "DELETE dbo.M_CommunityMapping WHERE M_PetHospitalID=" + PID;
            int cnt = base.SqlExecute(sql2);

            string sql = string.Format("Delete M_PetHospital where id={0} and MerchantID={1}", PID, MID);
            cnt = base.SqlExecute(sql);

            Result result = new Result();
            if (cnt <= 0)
            {
                result.HasError = true;
                result.Error = "删除失败 请稍后再试！";
            }
            return result;

        }


        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="UID"></param>
        /// <param name="EnumDataStatus"></param>
        /// <returns></returns>
        public Result UpdateStatus(int PID, int EnumDataStatus)
        {
            Result result = new Result();
            string sql = string.Format("update M_PetHospital set EnumDataStatus= {0} where ID = {1}", EnumDataStatus, PID);
            if (EnumDataStatus == (int)Poco.Enum.EnumDataStatus.Disabled)
            {
                sql = string.Format("update M_PetHospital set EnumDataStatus= {0}, IsPublish='False' where ID = {1}", EnumDataStatus, PID);
            }
            int cnt = base.SqlExecute(sql);
            if (cnt <= 0)
            {
                result.HasError = true;
                result.Error = "修改失败 请稍后再试！";
            }
            return result;
        }

        /// <summary>
        /// 修改是否发布
        /// </summary>
        /// <param name="UID"></param>
        /// <param name="push"></param>
        /// <returns></returns>
        public Result UpdatePush(int PID, bool push)
        {
            Result result = new Result();
            string sql = "";
            if (push)
            {
                sql = string.Format("update M_PetHospital set IsPublish= '{0}',publishdate='{1}' where ID = {2}", push, DateTime.Now.ToString(), PID);
            }
            else
            {
                sql = string.Format("update M_PetHospital set IsPublish= '{0}',publishdate=null  where ID = {1}", push, PID);
            }

            int cnt = base.SqlExecute(sql);
            if (cnt <= 0)
            {
                result.HasError = true;
                result.Error = "修改失败 请稍后再试！";
            }
            return result;
        }




    }
}
