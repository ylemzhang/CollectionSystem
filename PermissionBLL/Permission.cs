/*
 * 
 * 在业务层实体中定义权限相关的业务处理方法。
 * 
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel;
using System.Data;

namespace BLL
{
    public partial class BLLEntity
    {
        /// <summary>
        /// 用户登录。
        /// </summary>
        /// <param name="account">账号。</param>
        /// <param name="password">密码。</param>
        /// <param name="clientPassword">客户端密码。</param>
        /// <param name="clientFlag">客户端标记。</param>
        /// <returns>登陆状态。</returns>
        private IPermissionVerifyResult UserLogin(string account, string password)
        {
            var access = BLLAccess.GetInstance();

            if (string.IsNullOrEmpty(account))
            {
                return new PermissionVerifyResultEntity()
                {
                    PermissionVerifyState = Enum_PermissionVerifyState.用户名为空,
                    PermissionVerifyMessage = "用户名为空！"
                };
            }

            var userInfo = access.Select_User_Data(account: account);

            if (string.IsNullOrEmpty(userInfo.GetData().ToString()))
            {
                return new PermissionVerifyResultEntity()
                {
                    PermissionVerifyState = Enum_PermissionVerifyState.该用户不存在,
                    PermissionVerifyMessage = "该用户不存在！"
                };
            }

            if (userInfo.GetData("Ban").ToString().ToBool())
            {
                return new PermissionVerifyResultEntity()
                {
                    PermissionVerifyState = Enum_PermissionVerifyState.用户被禁止登陆,
                    PermissionVerifyMessage = "用户被禁止登陆！"
                };
            }

            if (string.IsNullOrEmpty(password))
            {
                return new PermissionVerifyResultEntity()
                {
                    PermissionVerifyState = Enum_PermissionVerifyState.密码为空,
                    PermissionVerifyMessage = "密码为空！"
                };
            }

            if (!password.Equals(userInfo.GetData("Password").ToString()))
            {
                return new PermissionVerifyResultEntity()
                {
                    PermissionVerifyState = Enum_PermissionVerifyState.密码验证失败,
                    PermissionVerifyMessage = "密码验证失败！"
                };
            }

            return new PermissionVerifyResultEntity()
            {
                PermissionVerifyState = Enum_PermissionVerifyState.密码验证通过,
                PermissionVerifyMessage = "密码验证通过。"
            };
        }

        /// <summary>
        /// 检查用户客户端凭证。
        /// </summary>
        /// <param name="account">账号。</param>
        /// <param name="clientPassword">客户端密码。</param>
        /// <param name="clientFlag">客户端标记。</param>
        /// <returns>登陆状态。</returns>
        private IPermissionVerifyResult CheckUserClientCertificate(string account, string clientPassword, string clientFlag)
        {
            if (string.IsNullOrEmpty(account))
            {
                return new PermissionVerifyResultEntity()
                {
                    PermissionVerifyState = Enum_PermissionVerifyState.用户名为空,
                    PermissionVerifyMessage = "用户名为空！"
                };
            }

            if (string.IsNullOrEmpty(clientPassword))
            {
                return new PermissionVerifyResultEntity()
                {
                    PermissionVerifyState = Enum_PermissionVerifyState.客户端密码为空,
                    PermissionVerifyMessage = "客户端密码为空！"
                };
            }

            if (string.IsNullOrEmpty(clientFlag))
            {
                return new PermissionVerifyResultEntity()
                {
                    PermissionVerifyState = Enum_PermissionVerifyState.客户端标记为空,
                    PermissionVerifyMessage = "客户端标记为空！"
                };
            }

            //获取失效日期，在失效日期之外的日志不参与验证。
            DateTime disabledTimeSpan = PermissionArgs.GetInstance().ExpiresDateTime;

            var access = BLLAccess.GetInstance();

            var log = access.Select_User_Data_Login_Log(account: account, dateTimeUpdate_Start: disabledTimeSpan);
            if (log == null || log.Tables.Count == 0 || log.Tables[0].Rows.Count == 0)
            {
                return new PermissionVerifyResultEntity()
                {
                    PermissionVerifyState = Enum_PermissionVerifyState.用户日志失效,
                    PermissionVerifyMessage = "在有效期内，没有用户日志记录！"
                };
            }

            //验证客户端标记、客户端密码、用户账号。
            var checkLogList = (from i in log.Tables[0].AsEnumerable()
                                where account.Equals(i["Account"].ToString()) &&
                                clientPassword.ToLower().Equals(i["ClientPassword"].ToString().ToLower()) &&
                                clientFlag.ToLower().Equals(i["ClientFlag"].ToString().ToLower())
                                select i).ToList();
            if (checkLogList.Count() == 0)
            {
                return new PermissionVerifyResultEntity()
                {
                    PermissionVerifyState = Enum_PermissionVerifyState.客户端密码无效,
                    PermissionVerifyMessage = "客户端密码无效！"
                };
            }

            //验证日志是否在线。如果超过在线限定日期，则视为自动下线。
            DateTime onlineDateTime = PermissionArgs.GetInstance().OnlineDateTime;
            DateTime checkOnlineDateTime = checkLogList[0]["DateTimeUpdate"].ToString().ToDateTime();
            if (onlineDateTime > checkOnlineDateTime)
            {
                return new PermissionVerifyResultEntity()
                {
                    PermissionVerifyState = Enum_PermissionVerifyState.客户端已自动下线,
                    PermissionVerifyMessage = "客户端已自动下线！"
                };
            }

            //验证日志的Offline标记是否为真，如果为真，表示此连接被强制下线。
            if (checkLogList[0]["Offline"].ToString().ToBool())
            {
                return new PermissionVerifyResultEntity()
                {
                    PermissionVerifyState = Enum_PermissionVerifyState.客户端被强制下线,
                    PermissionVerifyMessage = "客户端被强制下线！"
                };
            }

            return new PermissionVerifyResultEntity()
            {
                PermissionVerifyState = Enum_PermissionVerifyState.客户端密码验证通过,
                PermissionVerifyMessage = "客户端密码验证通过！"
            };
        }

        /// <summary>
        /// 检查用户在线数量。
        /// </summary>
        /// <param name="account">账号。</param>
        /// <returns>验证结果。</returns>
        private IPermissionVerifyResult CheckOnlineCount(string account)
        {
            var access = BLLAccess.GetInstance();
            if (string.IsNullOrEmpty(account))
            {
                return new PermissionVerifyResultEntity()
                {
                    PermissionVerifyState = Enum_PermissionVerifyState.用户名为空,
                    PermissionVerifyMessage = "用户名为空！"
                };
            }

            //查询在线同时在线的客户端信息。
            DateTime onlineDateTime = PermissionArgs.GetInstance().OnlineDateTime;
            var onlineLog = access.Select_User_Data_Login_Log(account: account, dateTimeUpdate_Start: onlineDateTime, offline: 0);
            var onlineLogList = onlineLog.Tables[0].AsEnumerable();

            int permitOnlineCount = PermissionArgs.GetInstance().OnlineCount;

            //判断账号在线个数是否在允许范围内。
            int len = onlineLogList.Count() - permitOnlineCount;
            if (len == 0)
            {
                return new PermissionVerifyResultEntity()
                {
                    PermissionVerifyState = Enum_PermissionVerifyState.用户已达到限制登录数量,
                    PermissionVerifyMessage = "用户已达到限制登录数量，不允许继续连接登录！"
                };
            }
            else if (len > 0)
            {
                return new PermissionVerifyResultEntity()
                {
                    PermissionVerifyState = Enum_PermissionVerifyState.用户登陆过多,
                    PermissionVerifyMessage = "用户登陆过多！"
                };
            }
            else
            {
                return new PermissionVerifyResultEntity()
                {
                    PermissionVerifyState = Enum_PermissionVerifyState.允许用户继续登录,
                    PermissionVerifyMessage = "允许用户继续登录。"
                };
            }
        }

        /// <summary>
        /// 检查用户凭证。
        /// </summary>
        /// <param name="account">账号。不可空。</param>
        /// <param name="password">密码。可空。不可与客户端密码、客户端标记同时为空。</param>
        /// <param name="clientFlag">客户端标记。可空。不可与密码同时为空。</param>
        /// <param name="clientPassword">客户端密码。可空。不可与密码同时为空。</param>
        /// <returns>登陆状态。</returns>
        public IPermissionVerifyResult CheckUserCertificate(string account, string password, string clientFlag, ref string clientPassword)
        {
            var access = BLLAccess.GetInstance();
            if (string.IsNullOrEmpty(account))
            {
                return new PermissionVerifyResultEntity()
                {
                    PermissionVerifyState = Enum_PermissionVerifyState.用户名为空,
                    PermissionVerifyMessage = "用户名为空！"
                };
            }

            if (string.IsNullOrEmpty(clientFlag))
            {
                return new PermissionVerifyResultEntity()
                {
                    PermissionVerifyState = Enum_PermissionVerifyState.客户端标记为空,
                    PermissionVerifyMessage = "客户端标记为空！"
                };
            }
            //验证用户账号、密码的合法性。
            var ul = UserLogin(account, password);
            if (ul.PermissionVerifyState == Enum_PermissionVerifyState.密码为空)
            {
                //密码为空的情况下验证输入的客户端信息。
                //验证账号在线数量。
                var co = CheckOnlineCount(account);
                if (co.PermissionVerifyState == Enum_PermissionVerifyState.用户登陆过多 && !string.IsNullOrEmpty(clientPassword))
                {
                    SetOffline(account, clientPassword);
                }
                //检查客户端凭据是否合法。
                var cucc = CheckUserClientCertificate(account, clientPassword, clientFlag);
                if (cucc.PermissionVerifyState == Enum_PermissionVerifyState.客户端密码验证通过)
                {
                    access.Update_Login_Log_DateTimeUpdate(clientPassword);
                    return new PermissionVerifyResultEntity()
                    {
                        PermissionVerifyState = Enum_PermissionVerifyState.用户验证通过,
                        PermissionVerifyMessage = "客户端请求合法，用户验证通过，自动登录！"
                    };
                }
                else
                {
                    return cucc;
                }
            }
            else if (ul.PermissionVerifyState == Enum_PermissionVerifyState.密码验证通过)
            {
                //在密码验证通过之后，要检查用户在线数量。
                var co = CheckOnlineCount(account);

                if (co.PermissionVerifyState == Enum_PermissionVerifyState.允许用户继续登录)
                {
                    clientPassword = access.Insert_Login_Log(account, clientFlag).GetData("ClientPassword").ToString();
                    return new PermissionVerifyResultEntity()
                    {
                        PermissionVerifyState = Enum_PermissionVerifyState.用户验证通过,
                        PermissionVerifyMessage = "用户验证通过！"
                    };
                }

                if (co.PermissionVerifyState == Enum_PermissionVerifyState.用户已达到限制登录数量)
                {
                    //如果在线数量达到了限制，验证输入的客户端信息。
                    var cucc = CheckUserClientCertificate(account, clientPassword, clientFlag);

                    if (cucc.PermissionVerifyState == Enum_PermissionVerifyState.客户端密码验证通过)
                    {
                        return new PermissionVerifyResultEntity()
                        {
                            PermissionVerifyState = Enum_PermissionVerifyState.用户验证通过,
                            PermissionVerifyMessage = "客户端密码验证通过，用户自动登录！"
                        };
                    }

                    //如果输入的客户端信息不合法，尝试使保护期外的一个在线信息强制下线
                    DateTime onlineDateTime = PermissionArgs.GetInstance().OnlineDateTime;
                    DateTime guardDateTime = PermissionArgs.GetInstance().GuardDateTime;
                    //尝试强制下线1个连接。
                    var info = access.Update_Login_Log_Offline(account: account, count: 1, offline: 0, dateTimeUpdate_Start: onlineDateTime, dateTimeUpdate_End: guardDateTime, offlineFlag: true);
                    if (info.GetData("Count").ToString().ToInt() == 1)
                    {
                        //如果强制下线成功，则登录现有连接。
                        clientPassword = access.Insert_Login_Log(account, clientFlag).GetData("ClientPassword").ToString();
                        return new PermissionVerifyResultEntity()
                        {
                            PermissionVerifyState = Enum_PermissionVerifyState.用户验证通过,
                            PermissionVerifyMessage = "强制下线其他连接，用户验证通过并登录！"
                        };
                    }
                    else
                    {

                        if (PermissionArgs.GetInstance().OnlineCount == 1)
                        {
                            //如果没有将任何连接强制下线，则提示登录错误信息。
                            return new PermissionVerifyResultEntity()
                            {
                                PermissionVerifyState = Enum_PermissionVerifyState.用户已达到限制登录数量,
                                PermissionVerifyMessage = "该用户已经登录，在保护期范围内不允许强制使其下线！"
                            };
                        }
                        else
                        {
                            //如果没有将任何连接强制下线，则提示登录错误信息。
                            return new PermissionVerifyResultEntity()
                            {
                                PermissionVerifyState = Enum_PermissionVerifyState.用户处于登录保护期,
                                PermissionVerifyMessage = "用户已达到限制登录数量，并且所有连接都处于登录保护期！"
                            };
                        }
                    }
                }

                if (co.PermissionVerifyState == Enum_PermissionVerifyState.用户登陆过多 && !string.IsNullOrEmpty(clientPassword))
                {
                    SetOffline(account, clientPassword);
                }

                return co;
            }
            else
            {
                return ul;
            }
        }

        /// <summary>
        /// 将用户强制下线。
        /// </summary>
        /// <param name="account">账号。</param>
        /// <param name="clientPassword">密码。</param>
        public void SetOffline(string account, string clientPassword = null)
        {
            var access = BLLAccess.GetInstance();
            access.Update_Login_Log_Offline(account: account, clientPassword: clientPassword, offlineFlag: true);
        }

        /// <summary>
        /// 获取用户组权限配置。
        /// </summary>
        /// <param name="account">用户账号。</param>
        /// <param name="url">地址。</param>
        /// <param name="urlCode">地址编码。</param>
        private void SeparateGroupUrlConfig(out IEnumerable<UrlConfigEntity> allowableConfig, out IEnumerable<UrlConfigEntity> forbiddenConfig, string account = null, string url = null, string urlCode = null)
        {
            var access = BLLAccess.GetInstance();

            IEnumerable<UrlConfigEntity> groupUrlConfig = new List<UrlConfigEntity>();

            if (!string.IsNullOrEmpty(account))
            {
                //如果用户账号不为空，则获取用户所属组。
                DataSet userGroupInfo = access.Select_UserGroup_Type_User_Data(account: account);
                if (userGroupInfo != null && userGroupInfo.Tables.Count > 0 && userGroupInfo.Tables[0].Rows.Count > 0)
                {
                    var userGroup_GUID = from i in userGroupInfo.Tables[0].AsEnumerable()
                                         select i["UserGroup_GUID"].ToString();
                    foreach (var i in userGroup_GUID)
                    {
                        var groupUrlConfigSet = access.Select_Url_Data_UserGroup_Type(userGroup_GUID: i, url: url, urlCode: urlCode);
                        var groupUrlConfigEntityList = from j in groupUrlConfigSet.Tables[0].AsEnumerable()
                                                       select new UrlConfigEntity()
                                                       {
                                                           GUID = j["Url_GUID"].ToString(),
                                                           ParentGUID = j["ParentGUID"].ToString(),
                                                           Url = j["Url"].ToString(),
                                                           UrlCode = j["UrlCode"].ToString(),
                                                           UrlName = j["UrlName"].ToString(),
                                                           UrlParams = j["UrlParams"].ToString(),
                                                           UrlIndex = j["UrlIndex"].ToString().ToInt(),
                                                           Forbidden = j["Forbidden"].ToString().ToBool(),
                                                           PriorityLevel = j["PriorityLevel"].ToString().ToInt()
                                                       };
                        //将该组的权限配置与其他组连接在一起。
                        groupUrlConfig = groupUrlConfig.Union(groupUrlConfigEntityList);
                    }
                }
            }

            //筛选其中拒绝访问的权限配置。
            var forbiddenUrlConfig = from i in groupUrlConfig
                                     where i.Forbidden
                                     select i;

            groupUrlConfig = groupUrlConfig.Where((i) =>
            {
                //剔除禁止访问的权限配置。
                if (i.Forbidden)
                {
                    return false;
                }
                //剔除允许访问的优先级较小的权限配置。
                var f = from j in forbiddenUrlConfig
                        where i.GUID.Equals(j.GUID) &&
                        i.PriorityLevel <= j.PriorityLevel
                        select j;
                if (f.Count() > 0)
                {
                    return false;
                }
                return true;
            });

            //将各组权限配置合并。
            groupUrlConfig = groupUrlConfig.Distinct(new UrlConfigEntityComparer());

            allowableConfig = groupUrlConfig;

            forbiddenConfig = forbiddenUrlConfig;
        }

        /// <summary>
        /// 获取用户菜单。
        /// </summary>
        /// <param name="account">用户账号。</param>
        /// <returns>用户菜单。</returns>
        public DataSet GetUserUrl(string account = null)
        {
            var access = BLLAccess.GetInstance();

            //获取组权限配置。
            IEnumerable<UrlConfigEntity> allowableConfig;
            IEnumerable<UrlConfigEntity> forbiddenConfig;
            SeparateGroupUrlConfig(allowableConfig: out allowableConfig, forbiddenConfig: out forbiddenConfig, account: account);

            //获取公开访问的url。
            DataSet publicUrlConfigSet = access.Select_Url_Data(userAuthentication:0, show: 1);

            var publicConfigEntityList = from j in publicUrlConfigSet.Tables[0].AsEnumerable()
                                         select new UrlConfigEntity()
                                         {
                                             GUID = j["Url_GUID"].ToString(),
                                             ParentGUID = j["ParentGUID"].ToString(),
                                             Url = j["Url"].ToString(),
                                             UrlCode = j["UrlCode"].ToString(),
                                             UrlParams = j["UrlParams"].ToString(),
                                             UrlName = j["UrlName"].ToString(),
                                             UrlIndex = j["UrlIndex"].ToString().ToInt(),
                                         };
            //将公开访问的地址与组权限访问的地址信息合并。
            allowableConfig = allowableConfig.Union(publicConfigEntityList);

            //筛选出顶级路径。
            var topList = from i in allowableConfig
                          where string.IsNullOrEmpty(i.ParentGUID)
                          orderby i.PriorityLevel
                          select i;
            DataSet ds = new DataSet("data");
            ds.Tables.Add("info");
            ds.Tables[0].Columns.Add("Url_GUID");
            ds.Tables[0].Columns.Add("ParentGUID");
            ds.Tables[0].Columns.Add("Url");
            ds.Tables[0].Columns.Add("UrlCode");
            ds.Tables[0].Columns.Add("UrlParams");
            ds.Tables[0].Columns.Add("UrlName");
            ds.Tables[0].Columns.Add("UrlIndex");
            ds.Tables[0].Columns.Add("UrlChildren", typeof(DataSet));
            foreach (var i in topList)
            {
                DataRow addRow = ds.Tables[0].NewRow();
                addRow["Url_GUID"] = i.GUID;
                addRow["Url"] = i.Url;
                addRow["UrlCode"] = i.UrlCode;
                addRow["UrlParams"] = i.UrlParams;
                addRow["UrlName"] = i.UrlName;
                addRow["UrlIndex"] = i.UrlIndex;
                addRow["UrlChildren"] = ds.Clone();
                //递归筛选出子级路径集合。
                GetChildUrl(addRow, allowableConfig);
                ds.Tables[0].Rows.Add(addRow);
            }
            return ds;
        }

        /// <summary>
        /// 获取用户菜单-GetUserChildeUrl
        /// </summary>
        /// <param name="account">用户账号。</param>
        /// <returns>用户菜单。</returns>
        public DataSet GetUserChildeUrl(string urlCode,string account = null)
        {
            DataSet ds = GetUserUrl(account);
            DataSet dsChildren = new DataSet();
            DataRow dr;
            if(ds.Tables.Count>0 && ds.Tables[0].Rows.Count>0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    dr = ds.Tables[0].Rows[i];
                    if (urlCode.Equals(dr["UrlCode"].ToString().Trim()))//主页 myPage  
                    {
                        dsChildren = dr["UrlChildren"] as DataSet;
                        break;
                    }
                }
            }
            return dsChildren;
        }

        /// <summary>
        /// 递归用户菜单子项。
        /// </summary>
        /// <param name="top">用户菜单节点。</param>
        /// <param name="list">用户菜单集合。</param>
        private void GetChildUrl(DataRow top, IEnumerable<UrlConfigEntity> list)
        {
            var childrenList = from i in list
                               where top["Url_GUID"].ToString().Equals(i.ParentGUID)
                               orderby i.UrlIndex
                               select i;
            DataSet ds = top["UrlChildren"] as DataSet;
            foreach (var i in childrenList)
            {
                DataRow addRow = ds.Tables[0].NewRow();
                addRow["Url_GUID"] = i.GUID;
                addRow["ParentGUID"] = i.ParentGUID;
                addRow["Url"] = i.Url;
                addRow["UrlCode"] = i.UrlCode;
                addRow["UrlParams"] = i.UrlParams;
                addRow["UrlName"] = i.UrlName;
                addRow["UrlIndex"] = i.UrlIndex;
                addRow["UrlChildren"] = ds.Clone();
                //递归筛选出子级路径集合。
                GetChildUrl(addRow, list);
                ds.Tables[0].Rows.Add(addRow);
            }
        }

        /// <summary>
        /// 检查账号访问权限。
        /// </summary>
        /// <param name="url">地址。</param>
        /// <param name="urlCode">地址编码。</param>
        /// <param name="account">账号。</param>
        /// <returns>检查结果。</returns>
        public IPermissionVerifyResult CheckUrl(string url, string urlCode, string account)
        {
            var access = BLLAccess.GetInstance();

            if (!PermissionArgs.GetInstance().CheckNoConfigurationUrl)
            {
                //部分地址在数据库中无配置。如果不需要检查则认为验证成功。
                DataSet urlInfo = access.Select_Url_Data(url: url);
                if (string.IsNullOrEmpty(urlInfo.GetData().ToString()))
                {
                    return new PermissionVerifyResultEntity()
                    {
                        PermissionVerifyState = Enum_PermissionVerifyState.该地址验证成功,
                        PermissionVerifyMessage = string.Empty
                    };
                }
            }

            if (string.IsNullOrEmpty(urlCode))
            {
                return new PermissionVerifyResultEntity()
                {
                    PermissionVerifyState = Enum_PermissionVerifyState.地址编码为空,
                    PermissionVerifyMessage = "地址编码为空！"
                };
            }

            if (string.IsNullOrEmpty(account))
            {
                return new PermissionVerifyResultEntity()
                {
                    PermissionVerifyState = Enum_PermissionVerifyState.用户名为空,
                    PermissionVerifyMessage = "用户名为空！"
                };
            }

            //查询用户的组权限。
            IEnumerable<UrlConfigEntity> allowableConfig;
            IEnumerable<UrlConfigEntity> forbiddenConfig;
            SeparateGroupUrlConfig(allowableConfig: out allowableConfig, forbiddenConfig: out forbiddenConfig, account: account, url: url, urlCode: urlCode);

            if (allowableConfig.Count() == 0)
            {
                if (forbiddenConfig.Count() > 0)
                {
                    return new PermissionVerifyResultEntity()
                    {
                        PermissionVerifyState = Enum_PermissionVerifyState.拒绝访问该地址,
                        PermissionVerifyMessage = "拒绝用户访问该地址！"
                    };
                }

                return new PermissionVerifyResultEntity()
                {
                    PermissionVerifyState = Enum_PermissionVerifyState.该地址验证失败,
                    PermissionVerifyMessage = "用户无权限访问该地址！"
                };
            }
            else
            {
                return new PermissionVerifyResultEntity()
                {
                    PermissionVerifyState = Enum_PermissionVerifyState.该地址验证成功,
                    PermissionVerifyMessage = "允许用户访问该地址。"
                };
            }
        }

        /// <summary>
        /// 获取用户组列表
        /// </summary>
        /// <param name="userGroup_GUID">用户组</param>
        /// <param name="userGroupName">用户组名称</param>
        /// <returns>用户组列表</returns>
        public DataSet GetUserGroupList(string userGroup_GUID, string userGroupName)
        {
            var access = BLLAccess.GetInstance();
            return access.SelectUserGroupData(userGroup_GUID, userGroupName);
        }

        /// <summary>
        /// 保存用户组信息。
        /// </summary>
        /// <param name="userGroup_GUID">用户组GUID。</param>
        /// <param name="userGroupName">用户组名。</param>
        /// <param name="type">保存方式add:增加 update:更新</param>
        /// <returns>用户和用户组信息数据集。</returns>
        public DataSet SaveUserGroupData(string userGroup_GUID, string userGroupName, string type)
        {
            var access = BLLAccess.GetInstance();
            return access.SaveUserGroupData(userGroup_GUID, userGroupName, type);
        }

        /// <summary>
        /// 查询用户组的所有用户。
        /// </summary>
        /// <param name="userGroup_GUID">用户组GUID。</param>
        /// <param name="userAccount">用户帐号</param>
        /// <param name="userName">用户名。</param>
        /// <param name="ban">是否被禁止</param>
        /// <param name="isBelongto">是否属于</param>
        /// <returns>用户和用户组信息数据集。</returns>
        public DataSet SelectGroupUsersData(string userGroup_GUID, string userAccount, string userName,bool? ban, bool isBelongto)
        {
            var access = BLLAccess.GetInstance();
            return access.SelectGroupUsersData(userGroup_GUID, userAccount, userName, ban, isBelongto);
        }

        /// <summary>
        /// 更新用户的禁止状态
        /// </summary>
        /// <param name="userGUID">用户GUID</param>
        /// <param name="ban">当前状态</param>
        public void ChangeUserState(string userGUID,bool ban)
        {
            var access = BLLAccess.GetInstance();
            access.ChangeUserState(userGUID, ban);
        }

        /// <summary>
        /// 把用户插入用户数组
        /// </summary>
        /// <param name="userGUID">用户GUID</param>
        /// <param name="userGroupGUID">用户组GUID</param>
        public void AddUserToUserGroup(string userGUID, string userGroupGUID,bool addOrDelete)
        {
            var access = BLLAccess.GetInstance();
            access.AddUserToUserGroup(userGUID, userGroupGUID, addOrDelete);
        }

        /// <summary>
        /// 插入一条数据
        /// </summary>
        /// <param name="model"></param>
        public void AddUrl(UrlDataModel model)
        {
            var access = BLLAccess.GetInstance();
            access.AddUrl(model);
        }

        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <param name="model"></param>
        public void UpdateUrl(UrlDataModel model)
        {
            var access = BLLAccess.GetInstance();
            access.UpdateUrl(model);
        }

        /// <summary>
        /// 清空表Module_Data
        /// </summary>
        public void DeleteAllUrl()
        {
            var access = BLLAccess.GetInstance();
            access.DeleteAllUrl();
        }

        /// <summary>
        /// 删除一条Module_Data数据
        /// </summary>
        public void DeleteUrl(string strWhere)
        {
            var access = BLLAccess.GetInstance();
            access.DeleteUrl(strWhere);
        }

        /// <summary>
        /// 获取最大UrlIndex值
        /// </summary>
        /// <returns></returns>
        public int GetMaxUrlIndex()
        {
            var access = BLLAccess.GetInstance();
            return access.GetMaxUrlIndex();
        }

        /// <summary>
        /// 导入表Module_Data
        /// </summary>
        public void ImportUrl(string values)
        {
            var access = BLLAccess.GetInstance();
            access.ImportUrl(values);
        }
        /// <summary>
        /// 获取所有Module_Data
        /// </summary>
        public DataSet SelectUrlData(UrlDataModel model)
        {
            var access = BLLAccess.GetInstance();
            return access.SelectUrlData(model);
        }

        /// <summary>
        /// 查询url信息。
        /// </summary>
        /// <param name="guid">urlId。</param>
        /// <param name="url">url地址。</param>
        /// <param name="urlCode">url代码。</param>
        /// <param name="urlName">url名称。</param>
        /// <param name="userAuthentication">是否验证用户。</param>
        /// <param name="show">是否显示。</param>
        /// <returns>url信息数据集。</returns>
        public DataSet Select_Url_Data(string guid = null, string parentGuid = null, string url = null, string urlCode = null, string urlName = null, int userAuthentication = -1, int show = -1)
        {
            var access = BLLAccess.GetInstance();
            return access.Select_Url_Data(guid,parentGuid, url, urlCode, urlName, userAuthentication, show);
        }

        /// <summary>
        /// 用户组与地址关联性查询。
        /// </summary>
        /// <param name="userGroup_GUID">用户组GUID。</param>
        /// <param name="url_GUID">地址GUID。</param>
        /// <param name="url">地址。</param>
        /// <param name="urlCode">地址编码。</param>
        /// <returns>用户组与地址关联性数据集。</returns>
        public DataSet Select_Url_Data_UserGroup_Type(string userGroup_GUID = null, string url_GUID = null, string url = null, string urlCode = null,bool? forbidden=null)
        {
            var access = BLLAccess.GetInstance();
            return access.Select_Url_Data_UserGroup_Type(userGroup_GUID, url_GUID, url, urlCode, forbidden);
        }

        /// <summary>
        /// 获取所有子节点URL数据集
        /// </summary>
        /// <param name="parentGuid">父节点</param>
        /// <returns></returns>
        public DataSet GetSubUrlData(string parentGuid)
        {
            var access = BLLAccess.GetInstance();
            return access.GetSubUrlData(parentGuid);
        }

        /// <summary>
        /// 获取地址和地址用户组关联信息
        /// </summary>
        /// <param name="userGroupGuid"></param>
        /// <param name="urlGuid"></param>
        /// <param name="parentGuid"></param>
        /// <param name="forbidden"></param>
        /// <returns></returns>
        public DataSet GetUrlAndUserGroupLink(string userGroupGuid, string urlGuid, string parentGuid, string forbidden)
        {
            var access = BLLAccess.GetInstance();
            return access.GetUrlAndUserGroupLink(userGroupGuid, parentGuid,urlGuid,forbidden);
        }

        /// <summary>
        /// 保存权限分配信息
        /// </summary>
        /// <param name="urlGuid">地址</param>
        /// <param name="userGroupGuid">用户组</param>
        /// <param name="proirotyLevel">优先级</param>
        /// <param name="type">是否禁止-1：未分配0：禁止1：允许</param>
        public void SavePermission(string urlGuid,string userGroupGuid,int proirotyLevel, string type)
        {
            var access = BLLAccess.GetInstance();
            access.SavePermission(urlGuid, userGroupGuid, proirotyLevel, type);
        }

        /// <summary>
        /// 获取用户和用户组信息。
        /// </summary>
        /// <param name="user_GUID">用户GUID。</param>
        /// <param name="userGroup_GUID">用户组GUID。</param>
        /// <param name="account">用户账号。</param>
        /// <param name="userName">用户名。</param>
        /// <param name="userGroupName">用户组名。</param>
        /// <returns>用户和用户组信息数据集。</returns>
        public DataSet Select_UserGroup_Type_User_Data(string user_GUID = null, string userGroup_GUID = null, string account = null, string userName = null, string userGroupName = null)
        {
            var access = BLLAccess.GetInstance();
            return access.Select_UserGroup_Type_User_Data(user_GUID, userGroup_GUID, account, userName, userGroupName);
        }

        /// <summary>
        /// 查询用户信息。
        /// </summary>
        /// <param name="guid">GUID。</param>
        /// <param name="account">账号。</param>
        /// <param name="userName">用户名。模糊查询。</param>
        /// <param name="password">密码。</param>
        /// <param name="ban">是否禁止。</param>
        /// <returns>用户信息查询结果数据集。</returns>
        public DataSet Select_User_Data(string guid = null, string account = null, string userName = null, string password = null, bool? ban = null)
        {
            var access = BLLAccess.GetInstance();
            return access.Select_User_Data(guid, account, userName, password, ban);
        }

        /// <summary>
        /// 根据操作类型插入或更新一条用户数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="type"></param>
        public void AddOrUpdateUser(UserDataModel model,string type)
        {
            var access = BLLAccess.GetInstance();
            access.AddOrUpdateUser(model, type);
        }

        /// <summary>
        /// 根据用户编号删除一个用户
        /// </summary>
        /// <param name="userGUID">用户GUID</param>
        public void DeleteUserData(string userGUID)
        {
            var access = BLLAccess.GetInstance();
            access.DeleteUserData(userGUID);
        }
    }
}
