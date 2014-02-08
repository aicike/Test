using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Web.SSO
{
    /// <summary>
    /// �������
    /// ���û�ƾ֤�����ƵĹ�ϵ���ݴ����Cache��
    /// </summary>
    public class CacheManager
    {
        /// <summary>
        /// ��ȡ�����е�DataTable
        /// </summary>
        /// <returns></returns>
        public static DataTable GetCacheTable()
        {
            DataTable dt = null;
            if (HttpContext.Current.Cache["CERT"] != null)
            {
                dt = (DataTable)HttpContext.Current.Cache["CERT"];
            }
            return dt;
        }

        /// <summary>
        /// ��ʼ�����ݽṹ
        /// </summary>
        /// <remarks>
        /// ----------------------------------------------------
        /// | token(����) | info(�û�ƾ֤) | timeout(����ʱ��) |
        /// |--------------------------------------------------|
        /// </remarks>
        private static void cacheInit()
        {
            if (HttpContext.Current.Cache["CERT"] == null)
            {
                DataTable dt = new DataTable();

                dt.Columns.Add("token", Type.GetType("System.String"));
                dt.Columns["token"].Unique = true;

                dt.Columns.Add("info", Type.GetType("System.Object"));
                dt.Columns["info"].DefaultValue = null;

                dt.Columns.Add("timeout", Type.GetType("System.DateTime"));
                dt.Columns["timeout"].DefaultValue = DateTime.Now.AddMinutes(double.Parse(System.Configuration.ConfigurationManager.AppSettings["timeout"]));

                DataColumn[] keys = new DataColumn[1];
                keys[0] = dt.Columns["token"];
                dt.PrimaryKey = keys;

                //Cache�Ĺ���ʱ��Ϊ ���ƹ���ʱ��*2
                HttpContext.Current.Cache.Insert("CERT", dt, null, DateTime.MaxValue, TimeSpan.FromMinutes(double.Parse(System.Configuration.ConfigurationManager.AppSettings["timeout"]) * 2));
            }
        }

        /// <summary>
        /// �ж������Ƿ����
        /// </summary>
        /// <param name="token">����</param>
        /// <returns></returns>
        public static bool TokenIsExist(string token)
        {
            cacheInit();

            DataTable dt = (DataTable)HttpContext.Current.Cache["CERT"];
            if (dt.Select("token = '" + token + "'").Length == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// �������ƹ���ʱ��
        /// </summary>
        /// <param name="token">����</param>
        /// <param name="time">����ʱ��</param>
        public static void TokenTimeUpdate(string token, DateTime time)
        {
            cacheInit();

            DataTable dt = (DataTable)HttpContext.Current.Cache["CERT"];
            DataRow[] dr = dt.Select("token = '" + token + "'");
            if (dr.Length > 0)
            {
                dr[0]["timeout"] = time;
            }
        }

        /// <summary>
        /// �������
        /// </summary>
        /// <param name="token">����</param>
        /// <param name="info">ƾ֤</param>
        /// <param name="timeout">����ʱ��</param>
        public static void TokenInsert(string token, object info, DateTime timeout)
        {
            cacheInit();

            if (!TokenIsExist(token))
            {
                DataTable dt = (DataTable)HttpContext.Current.Cache["CERT"];
                DataRow dr = dt.NewRow();
                dr["token"] = token;
                dr["info"] = info;
                dr["timeout"] = timeout;
                dt.Rows.Add(dr);
                HttpContext.Current.Cache["CERT"] = dt;
            }
            else
            {
                TokenTimeUpdate(token, timeout);
            }
        }

    }//end class
}
