using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;
using Injection;
using Common;
using System.Web;
using System.IO;

namespace Business
{
    public class ClassifyModel : BaseModel<Classify>, IClassifyModel
    {
        /// <summary>
        /// 根据上级ID 与客户ID 查询分类
        /// </summary>
        /// <param name="ParentID"></param>
        /// <param name="AccountMainID"></param>
        /// <returns></returns>
        public IQueryable<Classify> GetLastClass(int ParentID, int AccountMainID)
        {
            var classifyModel = List().Where(a => a.AccountMainID == AccountMainID && a.ParentID == ParentID);
            return classifyModel;
        }


        public Result AddClass(Classify classify)
        {
            if (!string.IsNullOrEmpty(classify.ImgPath))
            {
                CommonModel com = new CommonModel();
                var LastName = com.CreateRandom("", 5) + classify.ImgPath.GetFileSuffix();
                var path = string.Format(SystemConst.Business.PathBase, classify.AccountMainID);
               
                var token = DateTime.Now.ToString("yyyyMMddHHmmss");
                var accountPath = "";

                //集成微网站
                if (SystemConst.IsIntegrationWebProject)
                {
                    accountPath = string.Format(SystemConst.IntegrationPathBase, classify.AccountMainID);
                }
                //不是集成微网站
                else
                {
                    accountPath = HttpContext.Current.Server.MapPath(path);
                }


                var imageName = string.Format("{0}_{1}", token, LastName);
                var imagePath = string.Format("{0}\\{1}", accountPath, imageName);

                var lsImaFilePath = HttpContext.Current.Server.MapPath(classify.ImgPath);

                Tool.SuperGetPicThumbnail(lsImaFilePath, imagePath, 70, 640, 0, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.High);
                classify.ImgPath = path + imageName;
            }


            return base.Add(classify);
        }


        public IQueryable<Classify> GetClassByPID(int PID)
        {
            var classifyModel = List().Where(a => a.ParentID == PID);
            return classifyModel;
        }


        public IQueryable<Classify> GetLastClass(int ParentID, int AccountMainID, int NoID)
        {
            var classifyModel = List().Where(a => a.AccountMainID == AccountMainID && a.ParentID == ParentID && a.ID != NoID);
            return classifyModel;
        }


        public Result UpdClass(Classify classify)
        {

            if (!string.IsNullOrEmpty(classify.ImgPath))
            {
               
                CommonModel com = new CommonModel();
                var LastName = com.CreateRandom("", 5) + classify.ImgPath.GetFileSuffix();
                var path = string.Format(SystemConst.Business.PathBase, classify.AccountMainID);
                var token = DateTime.Now.ToString("yyyyMMddHHmmss");
                var accountPath = "";

                var lsImaFilePath = "";

                //集成微网站
                if (SystemConst.IsIntegrationWebProject)
                {
                    accountPath = string.Format(SystemConst.IntegrationPathBase, classify.AccountMainID);

                    if (classify.ImgPath.Contains("Temporary"))
                    {
                        lsImaFilePath = HttpContext.Current.Server.MapPath(classify.ImgPath);
                    }
                    else
                    {
                        lsImaFilePath = accountPath + classify.ImgPath.Substring(classify.ImgPath.LastIndexOf('/') + 1);
                    }
                }
                //不是集成微网站
                else
                {
                    accountPath = HttpContext.Current.Server.MapPath(path);
                     lsImaFilePath = HttpContext.Current.Server.MapPath(classify.ImgPath);

                }
                var imageName = string.Format("{0}_{1}", token, LastName);
                var imagePath = string.Format("{0}\\{1}", accountPath, imageName);


                Tool.SuperGetPicThumbnail(lsImaFilePath, imagePath, 70, 640, 0, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.High);
                classify.ImgPath = path + imageName;

                var classifyYSJ = base.Get(classify.ID);
                if (!string.IsNullOrEmpty(classifyYSJ.ImgPath))
                {
                    var file = "";
                    //集成微网站
                    if (SystemConst.IsIntegrationWebProject)
                    {
                        file = string.Format(SystemConst.IntegrationPathBase, classifyYSJ.AccountMainID) + classifyYSJ.ImgPath.Substring(classifyYSJ.ImgPath.LastIndexOf('/')+1);
                    }
                    //不是集成微网站
                    else
                    {
                        file = HttpContext.Current.Server.MapPath(classifyYSJ.ImgPath);

                    }
                    if (File.Exists(file))
                    {
                        File.Delete(file);
                    }
                }
            }
            return base.Edit(classify);
        }


        public bool GetIsMainNode(int ID)
        {
            var Class = List().Where(a => a.ID == ID && a.ParentID == 0);
            if (Class.Count() > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// App获取一级分类列表，及分类下产品
        /// </summary>
        /// <param name="accountMainID"></param>
        public List<Classify> Get1levelClass(int accountMainID)
        {
            var parentIDs = GetLastClass(0, accountMainID).Select(a => a.ID);
            var list = List().Where(a => parentIDs.Contains(a.ParentID));
            return list.ToList();
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="AMID"></param>
        /// <returns></returns>
        public Result DelClassify(int ID, int AMID)
        {
            Result result = new Result();
            try
            {
                var classify = base.Get(ID);
                if (!string.IsNullOrEmpty(classify.ImgPath))
                {
                    var file = "";
                    //集成微网站
                    if (SystemConst.IsIntegrationWebProject)
                    {
                        file = string.Format(SystemConst.IntegrationPathBase, AMID) + classify.ImgPath.Substring(classify.ImgPath.LastIndexOf('/')+1);
                    }
                    //不是集成微网站
                    else
                    {
                        file = HttpContext.Current.Server.MapPath(classify.ImgPath);

                    }
                    if (File.Exists(file))
                    {
                        File.Delete(file);
                    }
                }

                string sql = string.Format("delete Classify where ID = {0} and AccountMainID={1}", ID, AMID);

                base.SqlExecute(sql);
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Error = "删除失败！请稍后再试！";
            }
            return result;
        }

        /// <summary>
        /// 获取所有一级分类
        /// </summary>
        /// <param name="AccountMainID"></param>
        /// <returns></returns>
        public IQueryable<Classify> GetOneLevel(int AccountMainID)
        {
            int ParentID = List().Where(a => a.AccountMainID == AccountMainID && a.ParentID == 0).FirstOrDefault().ID;

            var list = List().Where(a => a.ParentID == ParentID);

            return list;
        }

        /// <summary>
        /// 根据ID获取该类别下的所有子ID(递归)
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public string GetTypeSUBID(int ID,int AccountMainID)
        {
            string strID = "";
            var list = List().Where(a => a.ParentID == ID && a.AccountMainID == AccountMainID).ToList();
            foreach (var item in list)
            {
                strID += item.ID + ",";
                strID += GetSubID(item.ID, AccountMainID);
            }
            return strID;
        }
        /// <summary>
        /// 根据ID获取该类别下的所有子ID(递归)
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public string GetSubID(int ID, int AccountMainID)
        {
            string strID = "";
            var list = List().Where(a => a.ParentID == ID && a.AccountMainID == AccountMainID).ToList();
            foreach (var item in list)
            {
                strID += item.ID + ",";
                strID += GetSubID(item.ID, AccountMainID);
            }

            return strID;
        }
    }
}
