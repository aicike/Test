﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Injection;
using Interface;
using Poco;
using Poco.WebAPI_Poco;
using Business;
using Common;
using Poco.Enum;

namespace Web.Controllers
{
    public class WebRequest_PropertyController : Controller
    {

        /// <summary>
        /// 验证随机码与物业是否匹配 true：匹配，false:错误
        /// </summary>
        /// <param name="AccountMainID"></param>
        /// <param name="RandomCode"></param>
        /// <returns>true false</returns>
        public string CheckPropertyRandomCode(int AccountMainID, string RandomCode)
        {
            var accountmain = Factory.Get<IAccountMainModel>(SystemConst.IOC_Model.AccountMainModel);
            bool IsRight = accountmain.CheckPropertyRandomCode(AccountMainID, RandomCode);
            if (IsRight)
            {
                return "true";
            }
            else
            {
                return "false";
            }
        }

        /// <summary>
        /// 根据房号获取物业登记的手机号码列表
        /// </summary>
        /// <param name="roomNum"></param>
        /// <returns></returns>
        public string GetUserInfoByPhone(int amid, string phone)
        {
            var userLoginInfoModel = Factory.Get<IUserLoginInfoModel>(SystemConst.IOC_Model.UserLoginInfoModel);
            bool isExist = userLoginInfoModel.ExistPhone(amid, phone);
            Result result = new Result();
            if (isExist)
            {
                result.Error = "该电话已经成为业主账号，请直接登录。";
            }
            var model = Factory.Get<IProperty_UserModel>(SystemConst.IOC_Model.Property_UserModel);
            var list = model.GetHouseByUserPhone(amid, phone);
            List<App_PropertyUser> objs = new List<App_PropertyUser>();
            if (list != null)
            {
                foreach (var item in list)
                {
                    App_PropertyUser ap = new App_PropertyUser();
                    ap.PropertyUserID = item.ID;
                    ap.Name = item.UserName;
                    ap.Phone = item.Phone;
                    ap.RoomNum = item.Property_House.RoomNumber;
                    ap.BuildingNum = item.Property_House.BuildingNum;
                    ap.CellNum = item.Property_House.CellNum;
                    objs.Add(ap);
                }
            }
            result.Entity = objs;

            return Newtonsoft.Json.JsonConvert.SerializeObject(result);
        }

        /// <summary>
        /// 业主注册
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        public string Register(int userID, string userName, string phone, string pwd)
        {
            Result result = new Result();
            var um = Factory.Get<IUserModel>(SystemConst.IOC_Model.UserModel);
            var user = um.Get(userID);
            if (user == null)
            {
                result.Error = "请求错误，请稍后重试。";
                return Newtonsoft.Json.JsonConvert.SerializeObject(result);
            }
            user.Name = userName;
            user.Phone = phone;
            result = um.Edit(user);
            if (user == null)
            {
                result.Error = "请求错误，请稍后重试。";
                return Newtonsoft.Json.JsonConvert.SerializeObject(result);
            }
            var ulim = Factory.Get<IUserLoginInfoModel>(SystemConst.IOC_Model.UserLoginInfoModel);
            var userLoginInfo = ulim.Get(user.UserLoginInfoID);
            CommonModel model = Factory.Get(SystemConst.IOC_Model.CommonModel) as CommonModel;
            var isOk = model.CheckIsUnique("UserLoginInfo", "Phone", phone, userLoginInfo.ID);
            if (isOk == false)
            {
                result.Error = "该电话已被其他账号使用。";
                result.HasError = true;
                return Newtonsoft.Json.JsonConvert.SerializeObject(result);
            }
            userLoginInfo.Name = userName;
            userLoginInfo.Phone = phone;
            userLoginInfo.LoginPwd = DESEncrypt.Encrypt(pwd);
            userLoginInfo.LoginPwdPage = "000000";
            result = ulim.Edit(userLoginInfo);
            if (result.HasError)
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(result);
            }
            //修改Property_User 
            var property_UserModel = Factory.Get<IProperty_UserModel>(SystemConst.IOC_Model.Property_UserModel);
            result = property_UserModel.EditUserLoginInfoID(phone, user.AccountMainID, userLoginInfo.ID);
            return Newtonsoft.Json.JsonConvert.SerializeObject(result);
        }




        #region-------------------报修接口---------------------------
        /// <summary>
        /// 获取用户报修列表
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <param name="AMID"></param>
        /// <returns></returns>
        public string GetRepairList(int UserID, int AMID)
        {
            var repairInfoModel = Factory.Get<IRepairInfoModel>(SystemConst.IOC_Model.RepairInfoModel);
            var list = repairInfoModel.GetListByUserID(UserID, AMID);
            List<_B_RepairInfo> objs = new List<_B_RepairInfo>();
            foreach (var item in list)
            {
                _B_RepairInfo ri = new _B_RepairInfo();
                ri.date = item.RepairDate.ToString("yyyy-MM-dd");
                ri.ImgPaths = item.ImgPath;
                ri.ID = item.ID;
                if (item.AccountID.HasValue)
                {
                    ri.AccountName = item.Account.Name;
                    ri.AccountPhone = item.Account.Phone;
                }
                else
                {
                    ri.AccountName = "暂无";
                    ri.AccountPhone = "暂无";
                }
                if (item.RepairType == 0)
                {
                    ri.type = "个人";
                }
                else
                {
                    ri.type = "公共";
                }
                switch (item.EnumRepairStatus)
                {
                    case (int)EnumRepairStatus.Allocated:
                        ri.status = "已分配";
                        break;
                    case (int)EnumRepairStatus.Audit:
                        ri.status = "审核中";
                        break;
                    case (int)EnumRepairStatus.Close:
                        ri.status = "已关闭";
                        break;
                    case (int)EnumRepairStatus.completed:
                        ri.status = "已完成";
                        break;
                    case (int)EnumRepairStatus.Reject:
                        ri.status = "驳回";
                        break;
                }
                switch (item.EnumRepairScore)
                {
                    case (int)EnumRepairScore.Dissatisfied:
                        ri.Score = "不满意";
                        break;
                    case (int)Poco.Enum.EnumRepairScore.General:
                        ri.Score = "一般";
                        break;
                    case (int)Poco.Enum.EnumRepairScore.NoScore:
                        ri.Score = "未评分";
                        break;
                    case (int)Poco.Enum.EnumRepairScore.Satisfied:
                        ri.Score = "满意";
                        break;
                    case (int)Poco.Enum.EnumRepairScore.VeryDissatisfied:
                        ri.Score = "非常不满意";
                        break;
                    case (int)Poco.Enum.EnumRepairScore.VerySatisfactory:
                        ri.Score = "非常满意";
                        break;
                }
                objs.Add(ri);
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(objs);
        }

        /// <summary>
        /// 提交报修
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <param name="Unit">单元</param>
        /// <param name="RoomNumber">房号</param>
        /// <param name="Content">内容</param>
        /// <param name="RepairType">报修分类 0室内 1公共区域</param>
        /// <param name="AMID"></param>
        /// <param name="ImgPath">图片路径 多张图片用|分割</param>
        /// <returns></returns>
        public string SubmitRepair(int UserID, string UName, string UPhone, string Unit, string RoomNumber, string Content, int RepairType, int AMID, string ImgPath)
        {
            Result result = new Result();
            //var userModel = Factory.Get<IUserModel>(SystemConst.IOC_Model.UserModel);
            //var user = userModel.Get(UserID);
            RepairInfo rpinfo = new RepairInfo();
            rpinfo.AccountMainID = AMID;
            rpinfo.EnumRepairScore = (int)EnumRepairScore.NoScore;
            rpinfo.EnumRepairStatus = (int)EnumRepairStatus.Audit;
            rpinfo.RepairContent = Content;
            rpinfo.RepairDate = DateTime.Now;
            rpinfo.RepairType = RepairType;
            rpinfo.RoomNumber = RoomNumber;
            rpinfo.Unit = Unit;
            rpinfo.UserID = UserID;
            rpinfo.RepairName = UName;
            rpinfo.RepairPhone = UPhone;
            rpinfo.ImgPath = ImgPath;

            var repairInfoModel = Factory.Get<IRepairInfoModel>(SystemConst.IOC_Model.RepairInfoModel);
            result = repairInfoModel.Add(rpinfo);

            return Newtonsoft.Json.JsonConvert.SerializeObject(result);
        }

        /// <summary>
        /// 获取报修详细信息
        /// </summary>
        /// <param name="RID">ID</param>
        /// <param name="AMID">AMID</param>
        /// <returns></returns>
        public string GetRepairInfo(int RID, int AMID)
        {
            var repairInfoModel = Factory.Get<IRepairInfoModel>(SystemConst.IOC_Model.RepairInfoModel);
            var repair = repairInfoModel.GetInfoByID(RID, AMID);
            _B_RepairInfo br = new _B_RepairInfo();
            br.AccountName = repair.RepairName;
            br.AccountPhone = repair.RepairPhone;
            br.date = repair.RepairDate.ToString("yyyy-MM-dd HH:mm");
            br.ID = repair.ID;
            br.ImgPaths = repair.ImgPath;
            if (repair.RepairType == 0)
            {
                br.type = "个人";
            }
            else
            {
                br.type = "公共";
            }
            switch (repair.EnumRepairScore)
            {
                case (int)EnumRepairScore.Dissatisfied:
                    br.Score = "不满意";
                    break;
                case (int)Poco.Enum.EnumRepairScore.General:
                    br.Score = "一般";
                    break;
                case (int)Poco.Enum.EnumRepairScore.NoScore:
                    br.Score = "未评分";
                    break;
                case (int)Poco.Enum.EnumRepairScore.Satisfied:
                    br.Score = "满意";
                    break;
                case (int)Poco.Enum.EnumRepairScore.VeryDissatisfied:
                    br.Score = "非常不满意";
                    break;
                case (int)Poco.Enum.EnumRepairScore.VerySatisfactory:
                    br.Score = "非常满意";
                    break;
            }
            switch (repair.EnumRepairStatus)
            {
                case (int)EnumRepairStatus.Allocated:
                    br.status = "已分配";
                    break;
                case (int)EnumRepairStatus.Audit:
                    br.status = "审核中";
                    break;
                case (int)EnumRepairStatus.Close:
                    br.status = "已关闭";
                    break;
                case (int)EnumRepairStatus.completed:
                    br.status = "已完成";
                    break;
                case (int)EnumRepairStatus.Reject:
                    br.status = "驳回";
                    break;
            }
            if (repair.RepairOperation != null)
            {
                string remark = "";
                foreach (var item in repair.RepairOperation)
                {
                    remark += item.OperationDate.ToString("yyyy-MM-dd HH:mm") + " : " + item.Remarks + "|";
                }
                br.Operation = remark.TrimEnd('|');
            }

            return Newtonsoft.Json.JsonConvert.SerializeObject(br);
        }

        #endregion

        #region-------------------投诉接口---------------------------

        /// <summary>
        /// 获取用户投诉列表 20条
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="AMID"></param>
        /// <returns></returns>
        public string GetComplaintList(int UserID, int AMID)
        {
            var complaintModel = Factory.Get<IComplaintModel>(SystemConst.IOC_Model.ComplaintModel);
            var list = complaintModel.GetListByUserID(UserID, AMID);
            List<_B_Complaint> objs = new List<_B_Complaint>();
            foreach (var item in list)
            {
                _B_Complaint bc = new _B_Complaint();
                bc.Contetn = item.ComplaintContetn;
                bc.Date = item.ComplaintDate.ToString("yyyy-MM-dd HH:ss");
                bc.ID = item.ID;
                bc.IsAnonymous = item.IsAnonymous;
                switch (item.EnumRepairScore)
                {
                    case (int)EnumRepairScore.Dissatisfied:
                        bc.Score = "不满意";
                        break;
                    case (int)Poco.Enum.EnumRepairScore.General:
                        bc.Score = "一般";
                        break;
                    case (int)Poco.Enum.EnumRepairScore.NoScore:
                        bc.Score = "未评分";
                        break;
                    case (int)Poco.Enum.EnumRepairScore.Satisfied:
                        bc.Score = "满意";
                        break;
                    case (int)Poco.Enum.EnumRepairScore.VeryDissatisfied:
                        bc.Score = "非常不满意";
                        break;
                    case (int)Poco.Enum.EnumRepairScore.VerySatisfactory:
                        bc.Score = "非常满意";
                        break;
                }
                switch (item.EnumComplaintStatus)
                {
                    case (int)EnumComplaintStatus.Audit:
                        bc.Status = "审核中";
                        break;
                    case (int)EnumComplaintStatus.completed:
                        bc.Status = "已处理";
                        break;
                    case (int)EnumComplaintStatus.Evaluation:
                        bc.Status = "已评价";
                        break;
                }
                objs.Add(bc);
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(objs);
        }

        /// <summary>
        /// 提交投诉
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <param name="AMID">AMID</param>
        /// <param name="IsAnonymous">bool 是否匿名</param>
        /// <param name="Content">内容</param>
        /// <param name="ImgPath">图片路径 多张图片用 | 分割</param>
        /// <returns></returns>
        public string SubmitComplaint(int UserID, int AMID, bool IsAnonymous, string Content, string ImgPath)
        {
            Complaint com = new Complaint();
            if (IsAnonymous)
            {
                com.UserID = UserID;
            }
            com.IsAnonymous = IsAnonymous;
            com.ComplaintContetn = Content;
            com.ImgPath = ImgPath;
            com.EnumComplaintStatus = (int)EnumComplaintStatus.Audit;
            com.EnumRepairScore = (int)EnumRepairScore.NoScore;
            com.ComplaintDate = DateTime.Now;
            com.AccountMainID = AMID;
            var complaintModel = Factory.Get<IComplaintModel>(SystemConst.IOC_Model.ComplaintModel);
            Result result = complaintModel.Add(com);
            return Newtonsoft.Json.JsonConvert.SerializeObject(result);
        }


        /// <summary>
        /// 获取用户投诉详细信息
        /// </summary>
        /// <param name="CID">投诉ID</param>
        /// <param name="AMID"></param>
        /// <returns></returns>
        public string GetComplainInfo(int CID, int AMID)
        {
            var complaintModel = Factory.Get<IComplaintModel>(SystemConst.IOC_Model.ComplaintModel);
            var complaint = complaintModel.GetComplaintInfo(CID, AMID);
            _B_Complaint bc = new _B_Complaint();
            bc.ID = complaint.ID;
            bc.Contetn = complaint.ComplaintContetn;
            bc.Date = complaint.ComplaintDate.ToString("yyyy-MM-dd HH:mm");
            bc.ImgPath = complaint.ImgPath;
            bc.IsAnonymous = complaint.IsAnonymous;
            switch (complaint.EnumRepairScore)
            {
                case (int)EnumRepairScore.Dissatisfied:
                    bc.Score = "不满意";
                    break;
                case (int)Poco.Enum.EnumRepairScore.General:
                    bc.Score = "一般";
                    break;
                case (int)Poco.Enum.EnumRepairScore.NoScore:
                    bc.Score = "未评分";
                    break;
                case (int)Poco.Enum.EnumRepairScore.Satisfied:
                    bc.Score = "满意";
                    break;
                case (int)Poco.Enum.EnumRepairScore.VeryDissatisfied:
                    bc.Score = "非常不满意";
                    break;
                case (int)Poco.Enum.EnumRepairScore.VerySatisfactory:
                    bc.Score = "非常满意";
                    break;
            }
            switch (complaint.EnumComplaintStatus)
            {
                case (int)EnumComplaintStatus.Audit:
                    bc.Status = "审核中";
                    break;
                case (int)EnumComplaintStatus.completed:
                    bc.Status = "已处理";
                    break;
                case (int)EnumComplaintStatus.Evaluation:
                    bc.Status = "已评价";
                    break;
            }
            if (complaint.ComplaintReply != null)
            {
                string reply = "";
                foreach (var item in complaint.ComplaintReply)
                {
                    reply += item.ReplyDate + " : " + item.ReplyContent + "|";
                }
                bc.Reply = reply.TrimEnd('|');
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(bc);
        }


        #endregion

    }
}
