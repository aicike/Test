using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using CAVEditLib;
using WMPLib;
using System.Threading;
using Poco;
using Interface;
using Injection;
using System.Web;
using Poco.Enum;

namespace Business
{
    public class CommonModel : EFModel
    {
        /// <summary>
        /// 返回false，说明已存在该值
        /// </summary>
        public bool CheckIsUniqueAccount(string AccountMainID, string tableName, string field, string value, int? id = null)
        {
            string sql = null;
            value = value.Trim();
            if (id.HasValue && id.Value > 0)
            {
                sql = string.Format("SELECT ID FROM {0} WHERE {1}='{2}' AND SystemStatus=0 AND ID<>{3} AND AccountMainID={4}", tableName, field, value, id.Value, AccountMainID);
            }
            else
            {
                sql = string.Format("SELECT ID FROM {0} WHERE {1}='{2}' AND SystemStatus=0  AND AccountMainID={3}", tableName, field, value, AccountMainID);
            }
            var result = SqlQuery<int>(sql).ToList();
            if (result.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 返回false，说明已存在该值
        /// </summary>
        public bool CheckIsUnique(string tableName, string field, string value, int? id = null)
        {
            string sql = null;
            value = value.Trim();
            if (id.HasValue && id.Value > 0)
            {
                sql = string.Format("SELECT ID FROM {0} WHERE {1}='{2}' AND SystemStatus=0 AND ID<>{3}", tableName, field, value, id.Value);
            }
            else
            {
                sql = string.Format("SELECT ID FROM {0} WHERE {1}='{2}' AND SystemStatus=0 ", tableName, field, value);
            }
            var result = SqlQuery<int>(sql).ToList();
            if (result.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 装换 其他格式音频文件到mp3文件
        /// </summary>
        /// <param name="ToFilePath">文件路径</param>
        /// <param name="ToFilePath">要转换的类型</param>
        /// <returns>转换后的路径</returns>
        public string CreateMp3(string FilePath, string ToType)
        {
            Thread.Sleep(2000);
            string ToFilePath = FilePath.Substring(0, FilePath.LastIndexOf('.')) + "." + ToType;
            CAVConverter converter = null;

            converter = new CAVConverter(); 		//Create the converter
            converter.SetLicenseKey("orna@diamondsview.com", "62A80CE3A6DB3F755C8C7478D44332E5928296AC08E61FCCD052D464049EDDEFCE62B2ED069F1396AC27F7DA120E6A3FDE982ACA8B49A4E8735552303198E03250EFE9D36E9E5349D2E289FF18D6C919CAB81199B4B24B13ABDF70CBB53605C844B1ED2C4164D08B1B4392C526C3D49E4100C1399C052C986BA57392042AC468BF0DDFD7BA5B12AFC4F2FFC21F9DF83D75C054DC6DBC198B091AD0E8AFD49D7A8CBA1E6B1BEA6BE3E0A3B41DA51B79E0678A7B675D3183618229AF2D50650A8505E9EA106E8507156E53ECA07973D883152F3CF4A75EBA57576B50A56117E2B129C5734150D552B7519D28AA7368895C740444BFEC403C041F4BA207F1258786");
            converter.AddTask(FilePath, ToFilePath);
            converter.StartAndWait();
            converter = null;

            return ToFilePath;
        }
        /// <summary>
        /// (ffmpeg)转换 音频文件到制定格式
        /// </summary>
        /// <param name="FilePath"></param>
        /// <param name="ToType"></param>
        /// <returns></returns>
        public string CreateMp3Forffmpeg(string FilePath, string ToType)
        {
            Thread.Sleep(2000);
            string ToFilePath = FilePath.Substring(0, FilePath.LastIndexOf('.')) + "." + ToType;
            //string ffmpegFile = HttpContext.Current.Server.MapPath("/App_Data/ffmpeg.exe");
            string ffmpegFile = AppDomain.CurrentDomain.BaseDirectory + "/App_Data/ffmpeg.exe";
            using (System.Diagnostics.Process pro = new System.Diagnostics.Process())
            {
                pro.StartInfo.UseShellExecute = false;
                pro.StartInfo.ErrorDialog = false;
                pro.StartInfo.RedirectStandardError = true;

                pro.StartInfo.FileName = ffmpegFile;
                pro.StartInfo.Arguments = " -i " + FilePath + " " + ToFilePath;
                pro.Start();
            }

            return ToFilePath;
        }
        /// <summary>
        /// 获取音频 视频文件长短 秒
        /// </summary>
        /// <param name="FilePath">文件路径</param>
        /// <returns>秒</returns>
        public string GetFileTimeLength(string FilePath)
        {
            WindowsMediaPlayerClass wmp = new WindowsMediaPlayerClass();
            IWMPMedia media = wmp.newMedia(FilePath);
            string MediaTime = media.durationString;
            DateTime dt = new DateTime();
            dt = Convert.ToDateTime(MediaTime);
            int ss = Convert.ToInt32(dt.TimeOfDay.TotalMinutes);
            return ss.ToString();
        }


        public IQueryable<T> SqlQuery<T>(string sql, params object[] parameters)
        {
            return Context.Database.SqlQuery<T>(sql, parameters).AsQueryable();
        }

        public int SqlExecute(string sql, params object[] parameters)
        {
            return Context.Database.ExecuteSqlCommand(sql, parameters);
        }

        /// <summary>
        /// 取会话列表
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="userType">用户类型 0：售楼部，1：用户</param>
        /// <returns></returns>
        public IQueryable<UnreadMessage> getSessionList(int userID, int userType, int AccountMainID)
        {
            //这里的用户类型与enum没有对应 要自己处理

            int UserType = (int)EnumClientUserType.Account;
            if (userType == 1)
            {
                UserType = (int)EnumClientUserType.User;
            }


            var conversModel = Factory.Get<IConversationDetailedModel>(SystemConst.IOC_Model.ConversationDetailedModel);
            var convers = conversModel.GetUserAllSID(UserType, userID, AccountMainID);
            string id = "";
            if (convers.Count() > 0)
            {

                foreach (var item in convers)
                {
                    id += item.ConversationID + ",";
                }
                id = id.TrimEnd(',');
            }
            else
            {
                id = "0";
            }

            string sql = "";
            //用户
            if (userType == 1)
            {
                sql = string.Format(@"select 
	                                        MessTwo.*,
	                                        case when MessTwo.T='s' then (select Name from Account where ID = MessTwo.I)
		                                         when MessTwo.T='u' then (select Name from UserLoginInfo where ID = (select UserLoginInfoID from [User] where ID = MessTwo.I))
		                                         when MessTwo.T='' then (select Cname from [Conversation] where ID = MessTwo.S)
	                                        end as N,
	                                        case when MessTwo.T='u' then (select Name from [User] where ID = MessTwo.I)
		                                         else ''
	                                        end as B,
	                                        case when MessTwo.T='s' then (select HeadImagePath from Account where ID = MessTwo.I)
		                                         when MessTwo.T='u' then (select HeadImagePath from UserLoginInfo where ID = (select UserLoginInfoID from [User] where ID = MessTwo.I))
		                                         when MessTwo.T='' then (select Cimg from [Conversation] where ID = MessTwo.S)
	                                        end as P
                                        from 
                                        (
	                                        select 
		                                        MessOne.*,
		                                        case when MessOne.M = 4 then 
			                                        (select count(*) from MessageGroupChat where [SID]=MessOne.S and UserID={0} and userType = 2) 
		                                        else
			                                        (select count(*) from [Message] where ConversationID=MessOne.S and ToUserID={0} and IsReceive = 'false')
		                                        end as C,
		                                        case when MessOne.M !=4 then 
			                                        (select  Convert(varchar(20),UserID)  from ConversationDetailed where ConversationID=MessOne.S and(userid!={0} or usertype!=2))
		                                        else ''
		                                        end as I,
		                                        case when MessOne.M !=4 then 
			                                        (select case when UserType=1 then 's' when UserType=2 then 'u' else '' end as T from ConversationDetailed where ConversationID=MessOne.S and(userid!={0} or usertype!=2))
		                                        else ''
		                                        end as T
	                                        from 
	                                        (
		                                        select 
			                                        (select TextContent from [Message] where ID = MessageID) as CT,
			                                        (select Convert(varchar(20),SendTime,20) as D  from [Message] where ID = MessageID) as D,
			                                        (select EnumMessageTypeID from [Message] where ID = MessageID) as E,
			                                        (select EnumMessageSendDirectionID from [Message] where ID = MessageID) as M,
			                                        (select ConversAtionID from [Message] where ID = MessageID) as S
		                                        from 
		                                        (
			                                        select max(ID) as MessageID from dbo.Message where ConversationID in ({1})  group by ConversationID
		                                        ) as MessageID
	                                        ) as MessOne
                                        ) as MessTwo"
                                    , userID, id);//and ToUserID ={1} 
            }
            //售楼代表
            else
            {
                sql = string.Format(@"select 
	                                        MessTwo.*,
	                                        case when MessTwo.T='s' then (select Name from Account where ID = MessTwo.I)
		                                         when MessTwo.T='u' then (select Name from UserLoginInfo where ID = (select UserLoginInfoID from [User] where ID = MessTwo.I))
		                                         when MessTwo.T='' then (select Cname from [Conversation] where ID = MessTwo.S)
	                                        end as N,
	                                        case when MessTwo.T='u' then (select Name from [User] where ID = MessTwo.I)
		                                         else ''
	                                        end as B,
	                                        case when MessTwo.T='s' then (select HeadImagePath from Account where ID = MessTwo.I)
		                                         when MessTwo.T='u' then (select HeadImagePath from UserLoginInfo where ID = (select UserLoginInfoID from [User] where ID = MessTwo.I))
		                                         when MessTwo.T='' then (select Cimg from [Conversation] where ID = MessTwo.S)
	                                        end as P
                                        from 
                                        (
	                                        select 
		                                        MessOne.*,
		                                        case when MessOne.M = 4 then 
			                                        (select count(*) from MessageGroupChat where [SID]=MessOne.S and UserID={0} and userType = 1) 
		                                        else
			                                        (select count(*) from [Message] where ConversationID=MessOne.S and ToAccountID={0} and IsReceive = 'false')
		                                        end as C,
		                                        case when MessOne.M !=4 then 
			                                        (select Convert(varchar(20),UserID) from ConversationDetailed where ConversationID=MessOne.S and(userid!={0} or usertype!=1))
		                                        else ''
		                                        end as I,
		                                        case when MessOne.M !=4 then 
			                                        (select case when UserType=1 then 's' when UserType=2 then 'u' else '' end as T from ConversationDetailed where ConversationID=MessOne.S and(userid!={0} or usertype!=1))
		                                        else ''
		                                        end as T
	                                        from 
	                                        (
		                                        select 
			                                        (select TextContent from [Message] where ID = MessageID) as CT,
			                                        (select Convert(varchar(20),SendTime,20) as D from [Message] where ID = MessageID) as D,
			                                        (select EnumMessageTypeID from [Message] where ID = MessageID) as E,
			                                        (select EnumMessageSendDirectionID from [Message] where ID = MessageID) as M,
			                                        (select ConversAtionID from [Message] where ID = MessageID) as S
		                                        from 
		                                        (
			                                        select max(ID) as MessageID from dbo.Message where ConversationID in ({1})   group by ConversationID
		                                        ) as MessageID
	                                        ) as MessOne
                                        ) as MessTwo"
                                    , userID, id);//and ToAccountID ={1} 
            }
            return SqlQuery<UnreadMessage>(sql);


        }




        /// <summary>
        /// 获取所有未读消息数
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <param name="UserType">用户类型 0：售楼部，1：用户</param>
        /// <param name="AccountMainID"></param>
        /// <returns>未读数</returns>
        public string GetUnreadCnt(int UserID, int userType, int AccountMainID)
        {
            int UserType = (int)EnumClientUserType.Account;
            if (userType == 1)
            {
                UserType = (int)EnumClientUserType.User;
            }

            var conversModel = Factory.Get<IConversationDetailedModel>(SystemConst.IOC_Model.ConversationDetailedModel);
            var convers = conversModel.GetUserAllSID(UserType, UserID, AccountMainID);
            string id = "";
            if (convers.Count() > 0)
            {

                foreach (var item in convers)
                {
                    id += item.ConversationID + ",";
                }
                id = id.TrimEnd(',');
            }
            else
            {
                id = "0";
            }

            string sql = string.Format(@"
                                        select (a.d+b.ds) as cnt from 
                                        (select count(*) as d from dbo.Message where ConversationID in ({0}) and IsReceive='false' and EnumMessageSendDirectionID!=4) a,
                                        (select count(*) as ds from MessageGroupChat where UserID={1} and UserType ={2} and sid in({0})) b",
                                        id, UserID, UserType);

            return SqlQuery<int>(sql).FirstOrDefault().ToString();
        }







        /// <summary>
        /// 创建随机字符窜（不唯一）
        /// </summary>
        /// <param name="str">随机数包含的字符 传"" 为默认a-z 1-9</param>
        /// <param name="length">返回的字符长度</param>
        /// <returns></returns>
        public string CreateRandom(string str, int length)
        {
            if (str == "")
            {
                str = "abcdefghijklmnopqrstuvwxyz0123456789";
            }
            Random rnd = new Random();
            string tmpstr = "";
            int iRandNum;
            for (int i = 0; i < length; i++)
            {
                iRandNum = rnd.Next(str.Length);
                tmpstr += str[iRandNum];
            }

            return tmpstr;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="BegingDate">开始日期</param>
        /// <param name="DateCount">天数</param>
        /// <param name="AccountMainID">客户ID</param>
        /// <param name="DeliveryType">配送类型 Enum </param>
        /// <returns></returns>
        public DateTime GetEndDate(DateTime BegingDate, int DateCount, int AccountMainID, int DeliveryType)
        {
            var holidayModel = Factory.Get<IHolidayModel>(SystemConst.IOC_Model.HolidayModel);
            var holiday = holidayModel.GetListByDateAndAMID(AccountMainID, BegingDate, BegingDate.AddDays((2 * DateCount)));

            if (DeliveryType == (int)Poco.Enum.EnumDeliveryType.WorkingDay)
            {
                DateTime Dates = RecursiveGetEndDate(BegingDate, DateCount, holiday);

                return Dates;
            }
            else
            {
                return BegingDate.AddDays(DateCount - 1);
            }

        }



        public DateTime RecursiveGetEndDate(DateTime BegingDate, int DateCount, IQueryable<Holiday> holiday)
        {
            DateTime Dates = BegingDate;
            int XCount = 0;//共多少休息日
            for (int i = 0; i < DateCount; i++)
            {
                if (Dates.AddDays(i).DayOfWeek.ToString("d") == "0" || Dates.AddDays(i).DayOfWeek.ToString("d") == "6")
                {
                    XCount = XCount + 1;
                }
                else
                {
                    foreach (var item in holiday)
                    {
                        if (item.HoliDayValue == Dates.AddDays(i))
                        {
                            XCount = XCount + 1;
                            break;
                        }
                    }
                }
            }
            if (XCount > 0)
            {

                return RecursiveGetEndDate(Dates.AddDays(DateCount), XCount, holiday);
            }

            else
            {
                return Dates.AddDays(DateCount - 1);
            }
        }




    }
}
