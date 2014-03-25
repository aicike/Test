using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;
using Injection.Transaction;
using Injection;
using System.Web;
using Common;
using System.IO;
using Poco.Enum;

namespace Business
{
    public class ActivityInfoModel : BaseModel<ActivityInfo>, IActivityInfoModel
    {

        /// <summary>
        /// 根据 AMID 获取列表
        /// </summary>
        /// <param name="AMID"></param>
        /// <returns></returns>
        public IQueryable<ActivityInfo> GetListByAMID(int AMID)
        {
            var list = List(true).Where(a => a.AccountMainID == AMID);
            return list;
        }

        /// <summary>
        /// 设置状态
        /// </summary>
        /// <param name="AID"></param>
        /// <param name="AccountMainID"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public Result SetMainStatus(int AID, int AccountMainID, int status)
        {
            int Stype = 0;
            //status 0 开启 1停止
            if (status == 0)
            {
                Stype = 1;
            }
            string sql = string.Format("update ActivityInfo set status = {0} where ID={1} and AccountMainID={2}", Stype, AID, AccountMainID);
            Result result = new Result();
            if (SqlExecute(sql) <= 0)
            {
                result.HasError = true;
                result.Error = "设置失败，请稍后再试！";
            }
            return result;

        }

        /// <summary>
        /// 根据ID获取活动信息
        /// </summary>
        /// <param name="AID"></param>
        /// <param name="AccountMainID"></param>
        /// <returns></returns>
        public ActivityInfo GetActivityByID(int AID, int AccountMainID)
        {
           return List().Where(a => a.AccountMainID == AccountMainID && a.ID == AID).FirstOrDefault();
        }

        /// <summary>
        /// 删除活动 级联删除
        /// </summary>
        /// <param name="AID"></param>
        /// <param name="AccountMainID"></param>
        /// <returns></returns>
        [Transaction]
        public Result DeleteActivityByID(int AID, int AccountMainID)
        {

            var Activity = List().Where(a => a.ID == AID && a.AccountMainID == AccountMainID).FirstOrDefault();
            if (!string.IsNullOrEmpty(Activity.MainImagPath))
            {
                string path = HttpContext.Current.Server.MapPath(Activity.MinImagePath);
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                path = HttpContext.Current.Server.MapPath(Activity.MainImagPath);
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                path = HttpContext.Current.Server.MapPath(Activity.AppShowImagePath);
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
            Result result = new Result();
            try
            {
                //删除资讯中的数据
                var advertorialModel = Factory.Get<IAppAdvertorialModel>(SystemConst.IOC_Model.AppAdvertorialModel);
                advertorialModel.DelAppadvertorial_byUrlType((int)EnumAdverURLType.Activities, Activity.ID);
                //删除浏览表中数据
                var appbrowsemodel = Factory.Get<IAppAdvertorialBrowseModel>(SystemConst.IOC_Model.AppAdvertorialBrowseModel);
                appbrowsemodel.DelBrowse(AID, EnumBrowseAdvertorialType.ActivityInfo);
                CommonModel commonModel = Factory.Get(SystemConst.IOC_Model.CommonModel) as CommonModel;
                string sql = string.Format("delete ActivityInfoParticipator where ActivityInfoID={0} delete ActivityInfo where ID = {0}", Activity.ID);
                commonModel.SqlExecute(sql);

            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Error = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 修改生成资讯状态
        /// </summary>
        /// <param name="id">活动ID</param>
        /// <param name="EnumAdvertorialUType">客户端 EnumAdvertorialUType</param>
        /// <param name="Type">状态 0 未生成 1生成</param>
        /// <returns></returns>
        public Result Update_GenerateType(int id, int EnumAdvertorialUType, int Type)
        {
            Result result = new Result();
            string sql = "";
            int cnt = 0;
            if (EnumAdvertorialUType == (int)Poco.Enum.EnumAdvertorialUType.AccountEnd)
            {
                sql = string.Format("update ActivityInfo set ISGenerateSaleAdvisory ={0}  where id={1}", Type, id);
                cnt = base.SqlExecute(sql);
            }
            else if (EnumAdvertorialUType == (int)Poco.Enum.EnumAdvertorialUType.UserEnd)
            {
                sql = string.Format("update ActivityInfo set ISGenerateUserAdvisory ={0}  where id={1}", Type, id);
                cnt = base.SqlExecute(sql);
            }
            if (cnt <= 0)
            {
                result.HasError = false;
            }

            return result;
        }


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
        public Result AddActivity(ActivityInfo activityinfo, int x1, int y1, int width, int height, int Twidth, int Theight)
        {
            Result result = new Result();
            if (string.IsNullOrEmpty(activityinfo.MainImagPath))
            {
                activityinfo.MainImagPath = "";
                activityinfo.MinImagePath = "";
                activityinfo.AppShowImagePath = "";
            }
            else
            {
                if (width > 0)
                {
                    CommonModel com = new CommonModel();
                    var LastName = com.CreateRandom("", 5) + activityinfo.MainImagPath.GetFileSuffix();
                    var path = string.Format(SystemConst.Business.PathBase, activityinfo.AccountMainID);
                    var accountPath = HttpContext.Current.Server.MapPath(path);
                    var token = DateTime.Now.ToString("yyyyMMddHHmmss");

                    var imageName = string.Format("{0}_{1}", token, LastName);
                    var imagePath = string.Format("{0}\\{1}", accountPath, imageName);
                    var imageName2 = string.Format("{0}Y_{1}", token, LastName);
                    var imagePath2 = string.Format("{0}\\{1}", accountPath, imageName2);
                    var imageminiName = string.Format("{0}_{1}_{2}", token, "mini", LastName);
                    var imageminiPath = string.Format("{0}\\{1}", accountPath, imageminiName);
                    var imageshowName = string.Format("{0}_{1}_{2}", token, "show", LastName);
                    var imageshowPath = string.Format("{0}\\{1}", accountPath, imageshowName);


                    var lsImgPath = activityinfo.MainImagPath;
                    var lsImaFilePath = HttpContext.Current.Server.MapPath(lsImgPath);

                    Tool.SuperGetPicThumbnail(lsImaFilePath, imagePath, 70, 640, 0, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic);


                    Tool.SuperGetPicThumbnailJT(lsImaFilePath, imagePath2, 70, width, height, x1, y1, Twidth, Theight, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic);

                    Tool.SuperGetPicThumbnail(imagePath2, imageshowPath, 70, 480, 0, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic);

                    if (File.Exists(imagePath2))
                    {
                        File.Delete(imagePath2);
                    }

                    //缩略图mini
                    Tool.SuperGetPicThumbnail(imageshowPath, imageminiPath, 70, 200, 0, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic);

                    activityinfo.MainImagPath = path + imageName;
                    activityinfo.AppShowImagePath = path + imageshowName;
                    activityinfo.MinImagePath = path + imageminiName;

                }
                else
                {

                }
            }

            result = base.Add(activityinfo);
            return result;
        }


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
        public Result EditActivity(ActivityInfo activityinfo, int x1, int y1, int width, int height, int Twidth, int Theight)
        {
            Result result = new Result();
            var Yactivityinfo = base.Get(activityinfo.ID);

            if (Yactivityinfo.MainImagPath != activityinfo.MainImagPath)
            {
                if (width > 0)
                {
                    CommonModel com = new CommonModel();

                    var LastName = com.CreateRandom("", 5) + activityinfo.MainImagPath.GetFileSuffix();
                    var path = string.Format(SystemConst.Business.PathBase, activityinfo.AccountMainID);
                    var accountPath = HttpContext.Current.Server.MapPath(path);
                    var token = DateTime.Now.ToString("yyyyMMddHHmmss");

                    var imageName = string.Format("{0}_{1}", token, LastName);
                    var imagePath = string.Format("{0}\\{1}", accountPath, imageName);
                    var imageName2 = string.Format("{0}Y_{1}", token, LastName);
                    var imagePath2 = string.Format("{0}\\{1}", accountPath, imageName2);
                    var imageminiName = string.Format("{0}_{1}_{2}", token, "mini", LastName);
                    var imageminiPath = string.Format("{0}\\{1}", accountPath, imageminiName);
                    var imageshowName = string.Format("{0}_{1}_{2}", token, "show", LastName);
                    var imageshowPath = string.Format("{0}\\{1}", accountPath, imageshowName);


                    var lsImgPath = activityinfo.MainImagPath;
                    var lsImaFilePath = HttpContext.Current.Server.MapPath(lsImgPath);

                    Tool.SuperGetPicThumbnail(lsImaFilePath, imagePath, 70, 640, 0, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic);


                    Tool.SuperGetPicThumbnailJT(lsImaFilePath, imagePath2, 70, width, height, x1, y1, Twidth, Theight, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic);

                    Tool.SuperGetPicThumbnail(imagePath2, imageshowPath, 70, 480, 0, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic);

                    if (File.Exists(imagePath2))
                    {
                        File.Delete(imagePath2);
                    }

                    //缩略图mini
                    Tool.SuperGetPicThumbnail(imageshowPath, imageminiPath, 70, 200, 0, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic);

                    if (!string.IsNullOrEmpty(Yactivityinfo.MinImagePath))
                    {
                        string path2 = HttpContext.Current.Server.MapPath(Yactivityinfo.MinImagePath);
                        if (File.Exists(path2))
                        {
                            File.Delete(path2);
                        }
                        path2 = HttpContext.Current.Server.MapPath(Yactivityinfo.MainImagPath);
                        if (File.Exists(path2))
                        {
                            File.Delete(path2);
                        }
                        path2 = HttpContext.Current.Server.MapPath(Yactivityinfo.AppShowImagePath);
                        if (File.Exists(path2))
                        {
                            File.Delete(path2);
                        }
                    }

                    activityinfo.MainImagPath = path + imageName;
                    activityinfo.AppShowImagePath = path + imageshowName;
                    activityinfo.MinImagePath = path + imageminiName;

                }
                else
                {

                }
            }

            result = base.Edit(activityinfo);
            if (result.HasError == false)
            {
                var advertorialModel = Factory.Get<IAppAdvertorialModel>(SystemConst.IOC_Model.AppAdvertorialModel);
                advertorialModel.Editadvert_ByActivityInfo(activityinfo, "~/Images/ActivityInfo.jpg", "~/Images/ActivityInfo_MINI.jpg");
            }
            return result;
        }
    }
}
