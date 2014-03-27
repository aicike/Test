using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;
using Injection.Transaction;
using Common;
using System.Web;
using System.IO;
using System.Drawing;
using Injection;

namespace Business
{
    public class Lottery_dishModel : BaseModel<Lottery_dish>, ILottery_dishModel
    {
        public IQueryable<Lottery_dish> List(int accountMainID)
        {
            return List(true).Where(a => a.AccountMainID == accountMainID);
        }

        [Transaction]
        public Result Add(Lottery_dish entity, List<Lottery_dish_detail> items, System.Web.HttpFileCollection files)
        {
            //添加基本信息
            Result result = base.Add(entity);
            if (result.HasError == true)
            {
                return result;
            }
            //上传奖品图片
            var path = string.Format(SystemConst.Business.PathBase, entity.AccountMainID);
            var token = DateTime.Now.ToString("yyyyMMddHHmmss");
            for (int i = 0; i < files.Count; i++)
            {
                var file = files[i];
                if (file.FileName.Length > 0)
                {
                    var imageName = string.Format("{0}_{1}", token, file.FileName);
                    var imageshowPath = string.Format("{0}/{1}", path, imageName);
                    items[i].Image = imageshowPath;
                    UploadTemp(file, HttpContext.Current.Server.MapPath(imageshowPath));
                }
            }
            var detailModel = Factory.Get<ILottery_dish_detailModel>(SystemConst.IOC_Model.Lottery_dish_detailModel);
            foreach (var item in items)
            {
                item.Lottery_dishID = entity.ID;
                result = detailModel.Add(item);
                if (result.HasError)
                {
                    return result;
                }
            }
            return result;
        }

        public void UploadTemp(HttpPostedFile file, string savePath)
        {
            int dataLengthToRead = (int)file.InputStream.Length;//获取下载的文件总大小
            byte[] buffer = new byte[dataLengthToRead];

            int r = file.InputStream.Read(buffer, 0, dataLengthToRead);//本次实际读取到字节的个数
            Stream tream = new MemoryStream(buffer);
            Image img = Image.FromStream(tream);
            Tool.SuperGetPicThumbnail(img, savePath, 70, 100, 0, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.High);
        }

        public Result Edit(Lottery_dish entity, List<Lottery_dish_detail> items, HttpFileCollection files)
        {
            //添加基本信息
            Result result = base.Edit(entity);
            if (result.HasError == true)
            {
                return result;
            }
            //上传奖品图片
            var path = string.Format(SystemConst.Business.PathBase, entity.AccountMainID);
            var token = DateTime.Now.ToString("yyyyMMddHHmmss");
            for (int i = 0; i < files.Count; i++)
            {
                if (items[i].IsNewImg)
                {
                    var file = files[i];
                    var imageName = string.Format("{0}_{1}", token, file.FileName);
                    var imageshowPath = string.Format("{0}/{1}", path, imageName);
                    items[i].Image = imageshowPath;
                    UploadTemp(file, HttpContext.Current.Server.MapPath(imageshowPath));
                }
            }
            //删除原奖品项
            var detailModel = Factory.Get<ILottery_dish_detailModel>(SystemConst.IOC_Model.Lottery_dish_detailModel);
            result = detailModel.DeleteByLottery_dishID(entity.ID);
            if (result.HasError == true)
            {
                return result;
            }
            foreach (var item in items)
            {
                item.Lottery_dishID = entity.ID;
                result = detailModel.Add(item);
                if (result.HasError)
                {
                    return result;
                }
            }
            return result;
        }

        /// <summary>
        /// 设置启用0   禁用1
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public Result ChangeStatus(int id, int status)
        {
            if (status == 0)
            {
                status = 1;
            }
            else
            {
                status = 0;
            }
            string sql = string.Format("UPDATE dbo.Lottery_dish SET Status={0} where ID={1}", status, id);
            int i = base.SqlExecute(sql);
            return new Result(); ;
        }


        /// <summary>
        /// 获取所有启用的大转盘活动
        /// </summary>
        /// <param name="accountMainID"></param>
        /// <returns></returns>
        public IQueryable<Lottery_dish> List_ActiveStatus(int accountMainID)
        {
            return List(true).Where(a => a.AccountMainID == accountMainID && a.Status == 0);
        }

        #region 受控随机抽取中奖概率

        /// <summary>
        /// 随机抽取
        /// </summary>
        /// <param name="rand">随机数生成器</param>
        /// <param name="Count">随机抽取个数</param>
        /// <returns></returns>
        public List<Lottery_dish_detail> ControllerRandomExtract(List<Lottery_dish_detail> datas, Random rand, int Count = 1)
        {
            List<Lottery_dish_detail> result = new List<Lottery_dish_detail>();
            if (rand != null)
            {
                //临时变量
                Dictionary<Lottery_dish_detail, double> dict = new Dictionary<Lottery_dish_detail, double>(datas.Count);

                //为每个项算一个随机数并乘以相应的权值
                for (int i = datas.Count - 1; i >= 0; i--)
                {
                    dict.Add(datas[i], rand.Next(100) * datas[i].Ratio);
                }

                //排序
                List<KeyValuePair<Lottery_dish_detail, double>> listDict = SortByValue(dict);

                //拷贝抽取权值最大的前Count项
                foreach (KeyValuePair<Lottery_dish_detail, double> kvp in listDict.GetRange(0, Count))
                {
                    result.Add(kvp.Key);
                }
            }
            return result;
        }

        /// <summary>
        /// 排序集合
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        private List<KeyValuePair<Lottery_dish_detail, double>> SortByValue(Dictionary<Lottery_dish_detail, double> dict)
        {
            List<KeyValuePair<Lottery_dish_detail, double>> list = new List<KeyValuePair<Lottery_dish_detail, double>>();

            if (dict != null)
            {
                list.AddRange(dict);

                list.Sort(
                  delegate(KeyValuePair<Lottery_dish_detail, double> kvp1, KeyValuePair<Lottery_dish_detail, double> kvp2)
                  {
                      return (int)(kvp2.Value - kvp1.Value);
                  });
            }
            return list;
        }
        #endregion
    }
}
