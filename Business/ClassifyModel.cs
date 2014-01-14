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


        public int AddClass(int PID, int Level, int AccountMainID, string Name, string imgpath1)
        {
            string imgPath = "";
            if (!string.IsNullOrEmpty(imgpath1))
            {
                CommonModel com = new CommonModel();
                var LastName = com.CreateRandom("", 5) + imgpath1.GetFileSuffix();
                var path = string.Format(SystemConst.Business.PathBase, AccountMainID);
                var accountPath = HttpContext.Current.Server.MapPath(path);
                var token = DateTime.Now.ToString("yyyyMMddHHmmss");
                var imageName = string.Format("{0}_{1}", token, LastName);
                var imagePath = string.Format("{0}\\{1}", accountPath, imageName);

                var lsImaFilePath = HttpContext.Current.Server.MapPath(imgpath1);

                Tool.SuperGetPicThumbnail(lsImaFilePath, imagePath, 70, 640, 0, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.High);
                imgPath = path + imageName;
            }


            string sql = string.Format("insert into classify(SystemStatus,AccountMainID,Name,ImgPath,ParentID,[Level],sort,Subordinate) values(0,{0},'{1}','{2}',{3},{4},0,0)", AccountMainID, Name, imgPath, PID, Level + 1);
            return base.SqlExecute(sql);
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


        public int UpdClass(int PID, int ID, string Name, int AccountMainID, string imgpath2)
        {

            string imgPath = imgpath2;
            if (!string.IsNullOrEmpty(imgpath2))
            {
                var classify = base.Get(ID);
                if (!string.IsNullOrEmpty(classify.ImgPath))
                {
                    var file = HttpContext.Current.Server.MapPath(classify.ImgPath);
                    if (File.Exists(file))
                    {
                        File.Delete(file);
                    }
                }
                CommonModel com = new CommonModel();
                var LastName = com.CreateRandom("", 5) + imgpath2.GetFileSuffix();
                var path = string.Format(SystemConst.Business.PathBase, AccountMainID);
                var accountPath = HttpContext.Current.Server.MapPath(path);
                var token = DateTime.Now.ToString("yyyyMMddHHmmss");
                var imageName = string.Format("{0}_{1}", token, LastName);
                var imagePath = string.Format("{0}\\{1}", accountPath, imageName);

                var lsImaFilePath = HttpContext.Current.Server.MapPath(imgpath2);

                Tool.SuperGetPicThumbnail(lsImaFilePath, imagePath, 70, 640, 0, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.High);
                imgPath = path + imageName;
            }
            string sql = string.Format("update classify set ParentID={0},Name='{1}',ImgPath='{2}' where ID = {3} and AccountMainID = {4}", PID, Name, imgPath, ID, AccountMainID);
            return base.SqlExecute(sql);
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
                    var file = HttpContext.Current.Server.MapPath(classify.ImgPath);
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
    }
}
