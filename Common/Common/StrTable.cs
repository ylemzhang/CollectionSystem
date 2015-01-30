namespace Common
{
    using System;
    using System.Data;
    using System.Web;

    public class StrTable
    {
        private static string CurrentLang = "chinese";

        private static DataSet getLangDS()
        {
            return Tools.GetDataFromFile(HttpContext.Current.Server.MapPath(Tools.langPath) + "lang.Xls", false);
        }

        public static string GetStr(string strName)
        {
            if (HttpContext.Current.Cache["StrTable"] == null)
            {
                HttpContext.Current.Cache["StrTable"] = getLangDS();
            }
            DataSet set = HttpContext.Current.Cache["StrTable"] as DataSet;
            foreach (DataRow row in set.Tables[0].Rows)
            {
                if (row[0].ToString() == strName)
                {
                    return row[CurrentLang].ToString();
                }
            }
            return "NULL";
        }
    }
}

