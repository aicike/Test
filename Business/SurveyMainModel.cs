using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interface;
using Poco;
using Injection;
using Injection.Transaction;
using Poco.Enum;
using System.Web;
using System.IO;
using Common;

namespace Business
{
    public class SurveyMainModel : BaseModel<SurveyMain>, ISurveyMainModel
    {

        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <param name="AccountMainID"></param>
        /// <returns></returns>
        public IQueryable<SurveyMain> GetList(int AccountMainID)
        {
            var list = List().Where(a => a.AccountMainID == AccountMainID);

            return list;
        }

        /// <summary>
        /// 列表首页
        /// </summary>
        /// <param name="AccountMainID"></param>
        /// <returns></returns>
        public IQueryable<_B_SurveyMain> SelList(int AccountMainID)
        {
            string sql = string.Format(@" select a.* ,(select count(*) from  (select usercode from SurveyAnswer where SurveyTroubleID = (select top 1 ID from SurveyTrouble where SurveyMainid = a.ID ) group by usercode)  as  Counts)as  Counts from 
                                         (select ID,SurveyTitle,CreateDate,Status,EnumSurveyMainType,ISGenerateUserAdvisory,ISGenerateSaleAdvisory,AppShowImagePath from dbo.SurveyMain where accountmainid={0}) a order by ID desc", AccountMainID);


            CommonModel model = Factory.Get(SystemConst.IOC_Model.CommonModel) as CommonModel;
            var result = model.SqlQuery<_B_SurveyMain>(sql);

            return result;
        }

        /// <summary>
        /// 删除调查问卷 级联删除
        /// </summary>
        /// <param name="MainID"></param>
        /// <param name="AccountMainID"></param>
        /// <returns></returns>
        [Transaction]
        public Result DelSurveyMain(int MainID, int AccountMainID)
        {
            Result result = new Result();
            var TroubleModel = Factory.Get<ISurveyTroubleModel>(SystemConst.IOC_Model.SurveyTroubleModel);
            var surveymain = List().Where(a => a.ID == MainID && a.AccountMainID == AccountMainID).FirstOrDefault();
            if (!string.IsNullOrEmpty(surveymain.MainImagPath))
            {
                string path = HttpContext.Current.Server.MapPath(surveymain.MinImagePath);
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                path = HttpContext.Current.Server.MapPath(surveymain.MainImagPath);
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                path = HttpContext.Current.Server.MapPath(surveymain.AppShowImagePath);
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }

            var Trouble = TroubleModel.GetListByMainID(MainID, AccountMainID).Select(a => a.ID).ToList();
            string ids = "0";
            foreach (var item in Trouble)
            {
                ids += item + ",";
            }
            ids = ids.TrimEnd(',');
            try
            {
                //删除资讯中的数据
                var advertorialModel = Factory.Get<IAppAdvertorialModel>(SystemConst.IOC_Model.AppAdvertorialModel);
                advertorialModel.DelAppadvertorial_byUrlType((int)EnumAdverURLType.Survey, MainID);


                CommonModel commonModel = Factory.Get(SystemConst.IOC_Model.CommonModel) as CommonModel;
                string DelMainSql = string.Format("delete SurveyAnswer where SurveyTroubleID in ({0})  delete SurveyOption where SurveyTroubleID in ({0})  delete SurveyTrouble where ID in ({0})  delete SurveyMain where ID ={1} and AccountMainID = {2}"
                                                    , ids, MainID, AccountMainID);
                commonModel.SqlExecute(DelMainSql);
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Error = ex.Message;
            }
            return result;
        }


        /// <summary>
        /// 根据ID获取调查问卷信息
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="AccountMainID"></param>
        /// <returns></returns>
        public SurveyMain GetSurveyMainByID(int ID, int AccountMainID)
        {
            var survey = List().Where(a => a.ID == ID && a.AccountMainID == AccountMainID).FirstOrDefault();
            return survey;
        }

        /// <summary>
        /// 设置状态
        /// </summary>
        /// <param name="MainID"></param>
        /// <param name="AccountMainID"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public Result SetMainStatus(int MainID, int AccountMainID, int status)
        {
            int Stype = 0;
            //status 0 开启 1停止
            if (status == 0)
            {
                Stype = 1;
            }
            string sql = string.Format("update SurveyMain set status = {0} where ID={1} and AccountMainID={2}", Stype,MainID, AccountMainID);
            Result result = new Result();
            if (SqlExecute(sql)<=0)
            {
                result.HasError = true;
                result.Error = "设置失败，请稍后再试！";
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
                sql = string.Format("update SurveyMain set ISGenerateSaleAdvisory ={0}  where id={1}", Type, id);
                cnt = base.SqlExecute(sql);
            }
            else if (EnumAdvertorialUType == (int)Poco.Enum.EnumAdvertorialUType.UserEnd)
            {
                sql = string.Format("update SurveyMain set ISGenerateUserAdvisory ={0}  where id={1}", Type, id);
                cnt = base.SqlExecute(sql);
            }
            if (cnt <= 0)
            {
                result.HasError = false;
            }

            return result;
        }


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
        public Result AddSurveyMain(SurveyMain surveymain, int w, int h, int x1, int y1, int tw, int th)
        {
            Result result = new Result();
            if (string.IsNullOrEmpty(surveymain.MainImagPath))
            {
                surveymain.MainImagPath = "";
                surveymain.MinImagePath = "";
                surveymain.AppShowImagePath = "";
            }
            else
            {
                if (w > 0)
                {
                    CommonModel com = new CommonModel();
                    var LastName = com.CreateRandom("", 5) + surveymain.MainImagPath.GetFileSuffix();
                    var path = string.Format(SystemConst.Business.PathBase, surveymain.AccountMainID);
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


                    var lsImgPath = surveymain.MainImagPath;
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

                    surveymain.MainImagPath = path + imageName;
                    surveymain.AppShowImagePath = path + imageshowName;
                    surveymain.MinImagePath = path + imageminiName;
                }
            }
            result = base.Add(surveymain);
            return result;
        }

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
        public Result EditSurveyMain(SurveyMain surveymain, int w, int h, int x1, int y1, int tw, int th)
        {
            Result result = new Result();
            var Ysurveymain = base.Get(surveymain.ID);

            if (Ysurveymain.MainImagPath != surveymain.MainImagPath)
            {
                if (w > 0)
                {
                    CommonModel com = new CommonModel();

                    var LastName = com.CreateRandom("", 5) + surveymain.MainImagPath.GetFileSuffix();
                    var path = string.Format(SystemConst.Business.PathBase, surveymain.AccountMainID);
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


                    var lsImgPath = surveymain.MainImagPath;
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

                    if (!string.IsNullOrEmpty(Ysurveymain.MinImagePath))
                    {
                        string path2 = HttpContext.Current.Server.MapPath(Ysurveymain.MinImagePath);
                        if (File.Exists(path2))
                        {
                            File.Delete(path2);
                        }
                        path2 = HttpContext.Current.Server.MapPath(Ysurveymain.MainImagPath);
                        if (File.Exists(path2))
                        {
                            File.Delete(path2);
                        }
                        path2 = HttpContext.Current.Server.MapPath(Ysurveymain.AppShowImagePath);
                        if (File.Exists(path2))
                        {
                            File.Delete(path2);
                        }
                    }

                    surveymain.MainImagPath = path + imageName;
                    surveymain.AppShowImagePath = path + imageshowName;
                    surveymain.MinImagePath = path + imageminiName;
                }
            }
            result = base.Edit(surveymain);
            if (result.HasError == false)
            {
                var advertorialModel = Factory.Get<IAppAdvertorialModel>(SystemConst.IOC_Model.AppAdvertorialModel);
                advertorialModel.Editadvert_BySurveyMain(surveymain, "~/Images/Survey.jpg", "~/Images/Survey_MINI.jpg");
            }

            return result;
        }
    }
}
