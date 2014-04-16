using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;
using Injection.Transaction;
using Common;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Injection;
using Poco.Enum;

namespace Business
{
    public class AppAdvertorialModel : BaseModel<AppAdvertorial>, IAppAdvertorialModel
    {

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="AccountMainID"></param>
        /// <returns></returns>
        public IQueryable<AppAdvertorial> GetList(int AccountMainID, int AdverTorialType, int EnumAdverClass)
        {
            var appadverlist = List().Where(a => a.AccountMainID == AccountMainID && a.EnumAdvertorialUType == AdverTorialType && a.EnumAdverClass == EnumAdverClass).OrderByDescending(a => a.stick).ThenByDescending(a => a.Sort).ThenByDescending(a => a.IssueDate);
            return appadverlist;
        }

        /// <summary>
        /// 获取详细信息
        /// </summary>
        /// <param name="AID"></param>
        /// <param name="AMID"></param>
        /// <returns></returns>
        public AppAdvertorial GetInfo(int AID, int AMID)
        {
            return List().Where(a => a.ID == AID && a.AccountMainID == AMID).FirstOrDefault();
        }


        [Transaction]
        public Result AddAppAdvertorial(AppAdvertorial appadvertorial, int w, int h, int x1, int y1, int tw, int th)
        {
            Result result = new Result();

            try
            {
                var path = string.Format(SystemConst.Business.PathBase, appadvertorial.AccountMainID);
                var accountPath = HttpContext.Current.Server.MapPath(path);
                var token = DateTime.Now.ToString("yyyyMMddHHmmss");
                CommonModel com = new CommonModel();
                var LastName = com.CreateRandom("", 5) + appadvertorial.MainImagPath.GetFileSuffix();
                var imageName = string.Format("{0}_{1}", token, LastName);
                var imagePath = string.Format("{0}\\{1}", accountPath, imageName);
                var imageName2 = string.Format("{0}Y_{1}", token, LastName);
                var imagePath2 = string.Format("{0}\\{1}", accountPath, imageName2);
                var imageminiName = string.Format("{0}_{1}_{2}", token, "mini", LastName);
                var imageminiPath = string.Format("{0}\\{1}", accountPath, imageminiName);
                var imageshowName = string.Format("{0}_{1}_{2}", token, "show", LastName);
                var imageshowPath = string.Format("{0}\\{1}", accountPath, imageshowName);
                var lsImgPath = appadvertorial.MainImagPath;
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

                appadvertorial.MainImagPath = path + imageName;
                appadvertorial.AppShowImagePath = path + imageshowName;
                appadvertorial.MinImagePath = path + imageminiName;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //如果是外链URL，则对这个URL进行短链接
            if (appadvertorial.EnumAdverURLType == (int)EnumAdverURLType.Ordinary && appadvertorial.ContentURL != null && appadvertorial.ContentURL.Length > 0)
            {
                appadvertorial.ShortURL = appadvertorial.ContentURL.ConvertToShortURL();
            }
            result = base.Add(appadvertorial);


            if (appadvertorial.EnumAdverURLType == (int)EnumAdverURLType.Ordinary)
            {
                //ULR
            }
            else
            {
                //富文本，如果是富文本，则对这个URL进行短链接
                appadvertorial.ShortURL = string.Format("http://{0}/default/News?id_token={1}", SystemConst.WebUrl, appadvertorial.ID.TokenEncrypt()).ConvertToShortURL();
                result = base.Edit(appadvertorial);
            }


            if (appadvertorial.stick == 1)
            {
                int cnt = EditAppAdvertorialStick(appadvertorial.ID, appadvertorial.stick, appadvertorial.AccountMainID, appadvertorial.Sort, appadvertorial.EnumAdvertorialUType, appadvertorial.EnumAdverClass);
                if (cnt <= 0)
                {
                    result.HasError = true;
                    result.Error = "添加失败 请稍后再试！";
                }
            }
            return result;
        }


        [Transaction]
        public Result DelAppAdvertorial(int ID, int AdverTorialType)
        {
            var appadivertorial = base.Get(ID);
            if (appadivertorial.EnumAdverURLType.HasValue)
            {
                var urltype = appadivertorial.EnumAdverURLType.Value;
                if (urltype == (int)EnumAdverURLType.Activities)
                {
                    var activitiesModel = Factory.Get<IActivityInfoModel>(SystemConst.IOC_Model.ActivityInfoModel);
                    activitiesModel.Update_GenerateType(appadivertorial.UrlID.Value, appadivertorial.EnumAdvertorialUType, 0);
                }
                else if (urltype == (int)EnumAdverURLType.Survey)
                {
                    var surveyModel = Factory.Get<ISurveyMainModel>(SystemConst.IOC_Model.SurveyMainModel);
                    surveyModel.Update_GenerateType(appadivertorial.UrlID.Value, appadivertorial.EnumAdvertorialUType, 0);
                }

                if (urltype != (int)EnumAdverURLType.Activities && urltype != (int)EnumAdverURLType.Survey)
                {
                    string path = HttpContext.Current.Server.MapPath(appadivertorial.MinImagePath);
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }
                    path = HttpContext.Current.Server.MapPath(appadivertorial.MainImagPath);
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }
                    path = HttpContext.Current.Server.MapPath(appadivertorial.AppShowImagePath);
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }
                }
            }

            if (appadivertorial.stick == 1)
            {
                string sql = string.Format("update AppAdvertorial set Sort=(Sort-1) where AccountMainID = {0} and EnumAdvertorialUType={1} and stick=1 and sort>{2}", appadivertorial.AccountMainID, AdverTorialType, appadivertorial.Sort);
                base.SqlExecute(sql);
            }
            var AppAdvertorialOperation = Factory.Get<IAppAdvertorialOperationModel>(SystemConst.IOC_Model.AppAdvertorialOperationModel);
            AppAdvertorialOperation.DelOperation(ID);
            var appbrowsemodel = Factory.Get<IAppAdvertorialBrowseModel>(SystemConst.IOC_Model.AppAdvertorialBrowseModel);
            appbrowsemodel.DelBrowse(ID, EnumBrowseAdvertorialType.Information);
            string shrotURL = appadivertorial.ShortURL;
            var result = base.CompleteDelete(ID);
            if (result.HasError == false)
            {
                //删除短URL
                shrotURL.DeleteShortURL();
            }
            return result;
        }

        [Transaction]
        public Result EditAppAdvertorial(AppAdvertorial appadvertorial, int w, int h, int x1, int y1, int tw, int th)
        {
            var appadvertorials = this.Get(appadvertorial.ID);

            if (appadvertorials.MainImagPath != appadvertorial.MainImagPath)
            {
                CommonModel com = new CommonModel();
                var LastName = com.CreateRandom("", 5) + appadvertorial.MainImagPath.GetFileSuffix();


                var path = string.Format(SystemConst.Business.PathBase, appadvertorial.AccountMainID);
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

                try
                {

                    var lsImgPath = appadvertorial.MainImagPath;
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

                    if (appadvertorial.EnumAdverURLType.HasValue)
                    {
                        var urltype = appadvertorial.EnumAdverURLType.Value;
                        if (urltype != (int)EnumAdverURLType.Activities && urltype != (int)EnumAdverURLType.Survey)
                        {
                            string path2 = HttpContext.Current.Server.MapPath(appadvertorials.MinImagePath);
                            if (File.Exists(path2))
                            {
                                File.Delete(path2);
                            }
                            path2 = HttpContext.Current.Server.MapPath(appadvertorials.MainImagPath);
                            if (File.Exists(path2))
                            {
                                File.Delete(path2);
                            }
                            path2 = HttpContext.Current.Server.MapPath(appadvertorials.AppShowImagePath);
                            if (File.Exists(path2))
                            {
                                File.Delete(path2);
                            }
                        }
                    }
                    appadvertorial.MainImagPath = path + imageName;
                    appadvertorial.AppShowImagePath = path + imageshowName;
                    appadvertorial.MinImagePath = path + imageminiName;
                }
                catch
                {
                    throw;
                }
            }
            Result result = base.Edit(appadvertorial);
            if (appadvertorial.stick != appadvertorials.stick)
            {

                int cnt = EditAppAdvertorialStick(appadvertorial.ID, appadvertorial.stick, appadvertorial.AccountMainID, appadvertorial.Sort, appadvertorial.EnumAdvertorialUType, appadvertorial.EnumAdverClass);
                if (cnt <= 0)
                {
                    result.HasError = true;
                    result.Error = "修改失败 请稍后再试！";
                }
            }
            return result;
        }


        /// <summary>
        /// 更改阅读次数
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public void BrowseCntADD(int id)
        {
            string sql = "update AppAdvertorial set BrowseCnt = (BrowseCnt+1) where id = " + id;
            base.SqlExecute(sql);
        }


        //修改置顶 isok 1 置顶 0 取消
        [Transaction]
        public int EditAppAdvertorialStick(int ID, int isok, int accoutMainID, int Sort, int AdverTorialType, int EnumAdverClass)
        {
            string sql = "";
            if (isok == 1)
            {
                var appadverSort = List().Where(a => a.AccountMainID == accoutMainID && a.EnumAdvertorialUType == AdverTorialType).Max(a => a.Sort);
                string sort = (appadverSort + 1).ToString();
                sql = string.Format("update AppAdvertorial set stick = {0}, sort={1} where id ={2}", isok, sort, ID);
            }
            else
            {
                sql = string.Format("update AppAdvertorial set stick = {0}, sort=0 where id ={1}", isok, ID);
                string sql2 = string.Format("update AppAdvertorial set Sort = (Sort-1) where stick = 1 and Sort>{0} and accountMainID={1} and EnumAdvertorialUType={2} and EnumAdverClass={3}", Sort, accoutMainID, AdverTorialType, EnumAdverClass);
                base.SqlExecute(sql2);
            }

            return base.SqlExecute(sql);
        }

        //排序 type 1 向上 0 向下
        public int EditAppAdvertorialSort(int ID, int AccountMainID, int Sort, int type, int AdverTorialType, int EnumAdverClass)
        {
            int cnt = List().Where(a => a.AccountMainID == AccountMainID && a.stick == 1).Count();
            if ((Sort == 1 && type == 0) || (Sort == cnt && type == 1))
            {
                return 0;
            }
            else
            {
                string sql = "";
                if (type == 1) //向上
                {
                    sql = string.Format(@"update AppAdvertorial set Sort = (Sort-1) where accountMainID={1}  and EnumAdvertorialUType={3} and Sort=({2}+1) and stick=1 and EnumAdverClass={4} 
                                    update AppAdvertorial set Sort = (Sort+1) where ID={0} ", ID, AccountMainID, Sort, AdverTorialType, EnumAdverClass);
                }
                else
                {
                    sql = string.Format(@"update AppAdvertorial set Sort = (Sort+1) where accountMainID={1}  and EnumAdvertorialUType={3} and Sort=({2}-1) and stick=1 and EnumAdverClass={4} 
                                    update AppAdvertorial set Sort = (Sort-1) where ID={0} ", ID, AccountMainID, Sort, AdverTorialType, EnumAdverClass);
                }
                return base.SqlExecute(sql);
            }
        }


        /// <summary>
        /// 更改活动 修改资讯信息
        /// </summary>
        /// <param name="activityInfo"></param>
        /// <param name="imageUrl"> 默认图片地址</param>
        /// <param name="minimageUrl">默认缩略图地址</param>
        /// <returns></returns>
        public Result Editadvert_ByActivityInfo(ActivityInfo activityInfo, string imageUrl, string minimageUrl)
        {
            Result result = new Result();
            string MainImage = imageUrl;
            string ShowImage = imageUrl;
            string MinImage = minimageUrl;
            if (!string.IsNullOrEmpty(activityInfo.MainImagPath))
            {
                MainImage = activityInfo.MainImagPath;
            }
            if (!string.IsNullOrEmpty(activityInfo.AppShowImagePath))
            {
                ShowImage = activityInfo.AppShowImagePath;
            }
            if (!string.IsNullOrEmpty(activityInfo.MinImagePath))
            {
                MinImage = activityInfo.MinImagePath;
            }
            string sql = string.Format("update AppAdvertorial set Title ='{0}',Depict='{1}',MainImagPath='{2}',AppShowImagePath='{3}',"
                                       + "MinImagePath='{4}' where EnumAdverURLType = {5} and UrlID ={6}", activityInfo.Title, activityInfo.Remarks, MainImage, ShowImage, minimageUrl, (int)EnumAdverURLType.Activities, activityInfo.ID);

            base.SqlExecute(sql);
            return result;
        }

        /// <summary>
        /// 更改调查 修改资讯信息
        /// </summary>
        /// <param name="surveymain"></param>
        /// <param name="imageUrl"> 默认图片地址</param>
        /// <param name="minimageUrl">默认缩略图地址</param>
        /// <returns></returns>
        public Result Editadvert_BySurveyMain(SurveyMain surveymain, string imageUrl, string minimageUrl)
        {
            Result result = new Result();
            string MainImage = imageUrl;
            string ShowImage = imageUrl;
            string MinImage = minimageUrl;
            if (!string.IsNullOrEmpty(surveymain.MainImagPath))
            {
                MainImage = surveymain.MainImagPath;
            }
            if (!string.IsNullOrEmpty(surveymain.AppShowImagePath))
            {
                ShowImage = surveymain.AppShowImagePath;
            }
            if (!string.IsNullOrEmpty(surveymain.MinImagePath))
            {
                MinImage = surveymain.MinImagePath;
            }
            string sql = string.Format("update AppAdvertorial set Title ='{0}',Depict='{1}',MainImagPath='{2}',AppShowImagePath='{3}',"
                                       + "MinImagePath='{4}' where EnumAdverURLType = {5} and UrlID ={6}", surveymain.SurveyTitle, surveymain.SurveyRemarks, MainImage, ShowImage, minimageUrl, (int)EnumAdverURLType.Survey, surveymain.ID);
            base.SqlExecute(sql);
            return result;
        }


        /// <summary>
        /// 删除活动与调查时 删除资讯
        /// </summary>
        /// <param name="EnumAdverURLType"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Result DelAppadvertorial_byUrlType(int EnumAdverURLType, int ID)
        {
            Result result = new Result();
            var appadvert = List().Where(a => a.EnumAdverURLType == EnumAdverURLType && a.UrlID == ID).FirstOrDefault();
            if (appadvert != null)
            {
                var AppAdvertorialOperation = Factory.Get<IAppAdvertorialOperationModel>(SystemConst.IOC_Model.AppAdvertorialOperationModel);
                AppAdvertorialOperation.DelOperation(appadvert.ID);

                string shortURL = appadvert.ShortURL;
                string ActivitySignUrl = appadvert.ActivitySignUrl;
                shortURL.DeleteShortURL();//删除短URL
                if (string.IsNullOrEmpty(ActivitySignUrl) == false)
                {
                    ActivitySignUrl.DeleteShortURL();
                }
            }
           
            string sql = string.Format("delete AppAdvertorial where EnumAdverURLType = {0} and UrlID={1}", EnumAdverURLType, ID);
            int cnt = base.SqlExecute(sql);
            if (cnt <= 0)
            {
                result.HasError = true;
            }
           
            return result;
        }

        /// <summary>
        /// 查询是否已经生产资讯
        /// </summary>
        /// <param name="id">活动 或调查 ID</param>
        /// <param name="client">咨询url类型 EnumAdvertorialUType </param>
        /// <returns></returns>
        public bool CKAppadverBy_clientAndID(int id, int client)
        {
            var appadver = List().Where(a => a.EnumAdvertorialUType == client && a.UrlID == id).FirstOrDefault();

            if (appadver != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获取生成的资讯信息
        /// </summary>
        /// <param name="id">活动 或调查 ID</param>
        /// <param name="client">咨询url类型 EnumAdverURLType </param>
        /// <returns></returns>
        public AppAdvertorial GetAppadverBy_clientAndID(int id, int EnumAdverURLType)
        {
            var appadver = List().Where(a => a.EnumAdverURLType == EnumAdverURLType && a.UrlID == id).FirstOrDefault();

            return appadver;
        }

    }
}
