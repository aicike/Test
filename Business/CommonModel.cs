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
        public IQueryable<UnreadMessage> getSessionList(int userID, int userType)
        {
            string AoU = "s";
            //用户
            if (userType == 1)
            {
                AoU = "u";
            }
            var conversModel = Factory.Get<IConversationModel>(SystemConst.IOC_Model.ConversationModel);
            var convers = conversModel.GetAllCID(AoU, userID);
            string id = "";
            if (convers.Count() > 0)
            {

                foreach (var item in convers)
                {
                    id += item.ID + ",";
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
                sql = string.Format(@"select x.*,case when x.T='s' 
	                                        then 
		                                        (select  Name from dbo.Account where systemStatus=0 and id=x.I)
	                                        else
		                                        (select b.Name from dbo.[User] a,dbo.UserLoginInfo b where a.userLoginInfoID=b.id and a.id=x.I and a.systemStatus=0)
	                                        end as N,
                                            case when x.T='s' 
                                            then 
                                                ''
                                            else
                                                (select Name from dbo.[User] where id=x.I and systemStatus=0)
                                            end as B,
	                                        case when x.T='s' 
	                                        then 
		                                        (select  HeadImagePath from dbo.Account where systemStatus=0 and id=x.I)
	                                        else
		                                        (select b.HeadImagePath from dbo.[User] a,dbo.UserLoginInfo b where a.userLoginInfoID=b.id and a.id=x.I and a.systemStatus=0)
	                                        end as P
                                        from 
                                        (select ConversationID as S,Convert(varchar(50),Max(SendTime),20) as D,
                                        (select case when ctype = 0 then User1ID when ctype = 2 then  case when user1id= {1} then user2ID else user1id end end as FromID from dbo.Conversation where id = a.ConversationID) as I,
                                        (select case when ctype = 0 then 's' when ctype = 2 then 'u' end as FromUserID from dbo.Conversation where id = a.ConversationID) as T,
                                        (select count(isreceive)  from dbo.[Message] where ConversationID  = a.ConversationID and ToUserID ={1} and IsReceive='false' ) as C,
                                        (select TextContent  from dbo.[Message] where SendTime = Max(a.SendTime)) as CT,
                                        (select EnumMessageSendDirectionID  from dbo.[Message] where SendTime = Max(a.SendTime)) as M,
                                        (select EnumMessageTypeID  from dbo.[Message] where SendTime = Max(a.SendTime)) as E
                                        from dbo.[Message] a  where ConversationID in ({0}) group by ConversationID) x"
                                    , id, userID);//and ToUserID ={1} 
            }
            //售楼代表
            else
            {
                sql = string.Format(@"select x.*,case when x.T='s' 
	                                        then 
		                                        (select  Name from dbo.Account where systemStatus=0 and id=x.I)
	                                        else
		                                        (select b.Name from dbo.[User] a,dbo.UserLoginInfo b where a.userLoginInfoID=b.id and a.id=x.I and a.systemStatus=0)
	                                        end as N,
                                            case when x.T='s' 
                                            then 
                                                ''
                                            else
                                                (select Name from dbo.[User] where id=x.I and systemStatus=0)
                                            end as B,
	                                        case when x.T='s' 
	                                        then 
		                                        (select  HeadImagePath from dbo.Account where systemStatus=0 and id=x.I)
	                                        else
		                                        (select b.HeadImagePath from dbo.[User] a,dbo.UserLoginInfo b where a.userLoginInfoID=b.id and a.id=x.I and a.systemStatus=0)
	                                        end as P
                                        from 
                                        (select ConversationID as S,Convert(varchar(50),Max(SendTime),20) as D,
                                        (select case when ctype = 0 then User2ID when ctype = 1 then  case when user1id= {1} then user2ID else user1id end end as FromID from dbo.Conversation where id = a.ConversationID) as I,
                                        (select case when ctype = 0 then 'u' when ctype = 1 then 's' end as FromUserID from dbo.Conversation where id = a.ConversationID) as T,
                                        (select count(isreceive)  from dbo.[Message] where ConversationID  = a.ConversationID and ToAccountID ={1} and IsReceive='false' ) as C,
                                        (select TextContent  from dbo.[Message] where SendTime = Max(a.SendTime)) as CT,
                                        (select EnumMessageSendDirectionID  from dbo.[Message] where SendTime = Max(a.SendTime)) as M,
                                        (select EnumMessageTypeID  from dbo.[Message] where SendTime = Max(a.SendTime)) as E
                                        from dbo.[Message] a  where ConversationID in ({0}) group by ConversationID) x"
                                    , id, userID);//and ToAccountID ={1} 
            }
            return SqlQuery<UnreadMessage>(sql);


        }

        /// <summary>
        /// 创建随机字符窜（不唯一）
        /// </summary>
        /// <param name="str">随机数包含的字符 传"" 为默认a-z 1-9</param>
        /// <param name="length">返回的字符长度</param>
        /// <returns></returns>
        public string CreateRandom(string str,int length)
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
    }
}
