using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace BLL
{
    /// <summary>
    /// 权限配置相关参数。
    /// </summary>
    internal class PermissionArgs
    {
        private PermissionArgs()
        {

        }

        public static PermissionArgs GetInstance()
        {
            return new PermissionArgs();
        }

        /// <summary>
        /// 登录保护时间。
        /// </summary>
        public DateTime GuardDateTime
        {
            get
            {
                int i;
                int.TryParse(ConfigurationManager.AppSettings["PermissionArgs_GuardTimeSpan"], out i);
                return DateTime.Now.AddSeconds(-1 * i);
            }
        }

        /// <summary>
        /// 登录失效时间。
        /// </summary>
        public DateTime ExpiresDateTime
        {
            get
            {
                int i;
                int.TryParse(ConfigurationManager.AppSettings["PermissionArgs_ExpiresTimeSpan"], out i);
                return DateTime.Now.AddSeconds(-1 * i);
            }
        }

        /// <summary>
        /// 连接在线时间。
        /// </summary>
        public DateTime OnlineDateTime
        {
            get
            {
                int i;
                int.TryParse(ConfigurationManager.AppSettings["PermissionArgs_OnlineTimeSpan"], out i);
                return DateTime.Now.AddSeconds(-1 * i);
            }
        }

        /// <summary>
        /// 同时在线数量。
        /// </summary>
        public int OnlineCount
        {
            get
            {
                int i;
                int.TryParse(ConfigurationManager.AppSettings["PermissionArgs_OnlineCount"], out i);
                return i < 1 ? 1 : i;
            }
        }

        /// <summary>
        /// 是否检查无配置的url。
        /// </summary>
        public bool CheckNoConfigurationUrl
        {
            get
            {
                bool i;
                bool.TryParse(ConfigurationManager.AppSettings["PermissionArgs_CheckNoConfigurationUrl"], out i);
                return i;
            }
        }
    }
}
