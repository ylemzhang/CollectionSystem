using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;

namespace DAL
{
    /// <summary>
    /// DAL配置信息。
    /// </summary>
    internal class DALArgs
    {
        private DALArgs()
        {

        }

        public static DALArgs GetInstance()
        {
            return new DALArgs();
        }

        /// <summary>
        /// 当前数据库连接字符串。
        /// </summary>
        public string CurrentConnectString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["connSQL"].ConnectionString;
            }
        }

        /// <summary>
        /// 当前数据库版本。
        /// </summary>
        public DatabaseUtil.Enum_DatabaseVersion CurrentDatabaseVersion
        {
            get
            {
                return (System.Data.DatabaseUtil.Enum_DatabaseVersion)Enum.Parse(
                    typeof(System.Data.DatabaseUtil.Enum_DatabaseVersion),
                    ConfigurationManager.AppSettings["PermissionDALDatabaseVersion"]
                    );
            }
        }

        public DatabaseUtil CurrentDatabaseUtil
        {
            get
            {
                return DatabaseUtil.GetInstance(CurrentDatabaseVersion);
            }
        }
    }
}
