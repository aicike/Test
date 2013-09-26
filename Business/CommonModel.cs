using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using CAVEditLib;
using WMPLib;

namespace Business
{
    public class CommonModel : EFModel
    {
        /// <summary>
        /// 返回false，说明已存在该值
        /// </summary>
        public bool CheckIsUniqueAccount(string AccountMainID,string tableName, string field, string value, int? id = null)
        {
            string sql = null;

            if (id.HasValue && id.Value > 0)
            {
                sql = string.Format("SELECT ID FROM {0} WHERE {1}='{2}' AND SystemStatus=0 AND ID<>{3} AND AccountMainID={4}", tableName, field, value, id.Value,AccountMainID);
            }
            else
            {
                sql = string.Format("SELECT ID FROM {0} WHERE {1}='{2}' AND SystemStatus=0  AND AccountMainID={3}", tableName, field, value,AccountMainID);
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
        public bool CheckIsUnique( string tableName, string field, string value, int? id = null)
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
        public string CreateMp3(string FilePath,string ToType)
        {
            string ToFilePath = FilePath.Substring(0, FilePath.LastIndexOf('.')) +"."+ ToType;
            CAVConverter converter = null;

            converter = new CAVConverter(); 		//Create the converter
            converter.SetLicenseKey("orna@diamondsview.com", "62A80CE3A6DB3F755C8C7478D44332E5928296AC08E61FCCD052D464049EDDEFCE62B2ED069F1396AC27F7DA120E6A3FDE982ACA8B49A4E8735552303198E03250EFE9D36E9E5349D2E289FF18D6C919CAB81199B4B24B13ABDF70CBB53605C844B1ED2C4164D08B1B4392C526C3D49E4100C1399C052C986BA57392042AC468BF0DDFD7BA5B12AFC4F2FFC21F9DF83D75C054DC6DBC198B091AD0E8AFD49D7A8CBA1E6B1BEA6BE3E0A3B41DA51B79E0678A7B675D3183618229AF2D50650A8505E9EA106E8507156E53ECA07973D883152F3CF4A75EBA57576B50A56117E2B129C5734150D552B7519D28AA7368895C740444BFEC403C041F4BA207F1258786");
            converter.AddTask(FilePath, ToFilePath);
            converter.StartAndWait();
            converter = null;

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

    }
}
