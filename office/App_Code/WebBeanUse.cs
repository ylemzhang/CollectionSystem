using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;


    /// <summary>
    /// Web层持久化实体。
    /// </summary>
    public class WebBeanUse
    {
        private WebBeanUse()
        {

        }

        public static WebBeanUse GetInstance()
        {
            return new WebBeanUse();
        }

        /// <summary>
        /// 获取存储在Web应用中的参数值。
        /// </summary>
        /// <param name="key">参数键。</param>
        /// <returns>参数值。</returns>
        private string GetCurrentHttpContextValue(string key)
        {
            HttpContext currentHttpContext = HttpContext.Current;            
            string value = string.Empty;
            if (!string.IsNullOrEmpty(currentHttpContext.Request[key]))
            {
                value = currentHttpContext.Request[key];
            }
            else if (null != currentHttpContext.Response.Cookies[key]
                && null != currentHttpContext.Response.Cookies[key].Value
                && !string.Empty.Equals(currentHttpContext.Response.Cookies[key].Value))
            {
                value = currentHttpContext.Response.Cookies[key].Value;
            }
            else if (currentHttpContext.Session[key] != null)
            {
                value = currentHttpContext.Session[key].ToString();
            }
            return value;
        }

        /// <summary>
        /// 设置存储在Web应用中的参数值。
        /// </summary>
        /// <param name="key">参数键。</param>
        /// <param name="value">参数值。</param>
        private void SetCurrentHttpContextValue(string key, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                value = string.Empty;
            }
            HttpContext currentHttpContext = HttpContext.Current;
            if (currentHttpContext.Request.Cookies[key] == null)
            {
                HttpCookie cookie = new HttpCookie(key);
                currentHttpContext.Request.Cookies.Add(cookie);
            }
            if (currentHttpContext.Response.Cookies[key] == null)
            {
                HttpCookie cookie = new HttpCookie(key);
                currentHttpContext.Response.Cookies.Add(cookie);
            }
            currentHttpContext.Request.Cookies[key].Value = value;
            currentHttpContext.Request.Cookies[key].Expires = CookiesExpiresDateTime;
            currentHttpContext.Response.Cookies[key].Value = value;
            currentHttpContext.Response.Cookies[key].Expires = CookiesExpiresDateTime;
            currentHttpContext.Session[key] = value;
        }

        /// <summary>
        /// Cookies的有效日期。
        /// </summary>
        public DateTime CookiesExpiresDateTime
        {
            get
            {
                int i;
                int.TryParse(ConfigurationManager.AppSettings["WebBean_CookiesExpiresTimeSpan"], out i);
                return DateTime.Now.AddSeconds(i);
            }
        }

        /// <summary>
        /// 账号。
        /// </summary>
        public string Account
        {
            get
            {
                return GetCurrentHttpContextValue("Account");
            }
            set
            {
                SetCurrentHttpContextValue("Account", value);
            }
        }

        /// <summary>
        /// 账号。
        /// </summary>
        public string ClientPassword
        {
            get
            {
                return GetCurrentHttpContextValue("ClientPassword");
            }
            set
            {
                SetCurrentHttpContextValue("ClientPassword", value);
            }
        }

        /// <summary>
        /// 持久化实体对外发布的信息。
        /// </summary>
        public string Message { get; set; }

        ///// <summary>
        ///// 用户登录。
        ///// </summary>
        ///// <param name="account">账号。</param>
        ///// <param name="password">密码。</param>
        ///// <returns>登录结果。</returns>
        //public bool UserLogin(string account, string password)
        //{
        //    HttpContext currentHttpContext = HttpContext.Current;
        //    string clientPassword = string.Empty;
        //    var info = WebAccess.GetInstance().CheckUserCertificate(account,
        //        password,
        //        string.Format("{0}@{1}", currentHttpContext.Session.SessionID, currentHttpContext.Request.UserHostAddress),
        //        ref clientPassword
        //        );
        //    Message = info.PermissionVerifyMessage;
        //    if (info.PermissionVerifyState == DataModel.Enum_PermissionVerifyState.用户验证通过)
        //    {
        //        Account = account;
        //        ClientPassword = clientPassword;
        //        return true;
        //    }
        //    else
        //    {
        //        Account = string.Empty;
        //        ClientPassword = string.Empty;
        //        return false;
        //    }
        //}

        ///// <summary>
        ///// 检查用户登录状况。
        ///// </summary>
        ///// <returns>检查结果。</returns>
        //public bool CheckUser()
        //{
        //    HttpContext currentHttpContext = HttpContext.Current;
        //    string account = Account;
        //    string clientPassword = ClientPassword;
        //    var info = WebAccess.GetInstance().CheckUserCertificate(
        //        Account,
        //        string.Empty,
        //        string.Format("{0}@{1}", currentHttpContext.Session.SessionID, currentHttpContext.Request.UserHostAddress),
        //        ref clientPassword
        //        );
        //    Message = info.PermissionVerifyMessage;
        //    if (info.PermissionVerifyState == DataModel.Enum_PermissionVerifyState.用户验证通过)
        //    {
        //        Account = account;
        //        ClientPassword = clientPassword;
        //        return true;
        //    }
        //    else
        //    {
        //        Account = string.Empty;
        //        ClientPassword = string.Empty;
        //        return false;
        //    }
        //}

        /// <summary>
        /// 验证用户url权限。
        /// </summary>
        /// <returns>验证结果。</returns>
        public bool CheckUrl()
        {
            HttpRequest currentRequest = HttpContext.Current.Request;
            var info = WebAccess.GetInstance().CheckUrl(string.Format("~{0}", currentRequest.Path), currentRequest["Code"], Account);
            Message = info.PermissionVerifyMessage;
            if (info.PermissionVerifyState == DataModel.Enum_PermissionVerifyState.该地址验证成功)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }