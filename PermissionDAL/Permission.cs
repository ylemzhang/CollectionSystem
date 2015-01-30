/*
 * 
 * 在数据库访问实体中定义权限相关的数据库访问方法。
 * 
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public partial class DALEntity
    {
        /// <summary>
        /// 查询用户信息。
        /// </summary>
        /// <param name="guid">GUID。</param>
        /// <param name="account">账号。</param>
        /// <param name="userName">用户名。模糊查询。</param>
        /// <param name="password">密码。</param>
        /// <param name="ban">是否禁止。</param>
        /// <returns>用户信息查询结果数据集。</returns>
        public DataSet Select_User_Data(string guid = null, string account = null, string userName = null, string password = null, bool? ban=null)
        {
            var s = DatabaseUtil.SelectBuilder.GetInstance();
            s.ConnectionString = DALArgs.GetInstance().CurrentConnectString;

            s.From.Append(@"
from [User_Data]
");
            s.Select.Append(@"
select [GUID]
      ,[Account]
      ,[UserName]
      ,[Password]
      ,[Ban]
");
            s.Where.Append(@"
where 1=1
");
            if (!string.IsNullOrEmpty(guid))
            {
                s.Where.Append(@"
    and GUID = @GUID ");
                s.Parameters.Add(new SqlParameter("@GUID", guid));
            }

            if (!string.IsNullOrEmpty(account))
            {
                s.Where.Append(@"
    and Account = @Account ");
                s.Parameters.Add(new SqlParameter("@Account", account));
            }

            if (!string.IsNullOrEmpty(userName))
            {
                s.Where.Append(@"
    and UserName like @UserName escape '\' ");
                s.Parameters.Add(new SqlParameter("@UserName", DatabaseUtil.FormatSqlParameterValue(userName)));
            }

            if (!string.IsNullOrEmpty(password))
            {
                s.Where.Append(@"
    and Password = @Password ");
                s.Parameters.Add(new SqlParameter("@Password", password));
            }

            if (ban !=null)
            {
                s.Where.Append(@"
    and Ban = @Ban ");
                s.Parameters.Add(new SqlParameter("@Ban", ban));
            }

            return DALArgs.GetInstance().CurrentDatabaseUtil.ExecuteDataAdapter(s);
        }

        /// <summary>
        /// 查询用户日志。
        /// </summary>
        /// <param name="account">用户账号。</param>
        /// <param name="clientFlag">客户端标记。</param>
        /// <param name="clientPassword">客户端密码。</param>
        /// <param name="dateTimeUpdate_Start">最后在线时间范围起点。</param>
        /// <param name="offline">下线标记。</param>
        /// <returns>用户日志数据集。</returns>
        public DataSet Select_User_Data_Login_Log(string account, string clientFlag = null, string clientPassword = null, DateTime dateTimeUpdate_Start = new DateTime(), int offline = -1)
        {
            var s = DatabaseUtil.SelectBuilder.GetInstance();
            s.ConnectionString = DALArgs.GetInstance().CurrentConnectString;
            s.From.Append(@"
from [User_Data] left join [Login_Log] on [User_Data].[GUID] = [Login_Log].[FK_User_Data]
");
            s.Select.Append(@"
select [User_Data].[Account]
      ,[User_Data].[UserName]
      ,[Login_Log].[DateTimeLogin]
      ,[Login_Log].[DateTimeUpdate]
      ,[Login_Log].[ClientFlag]
      ,[Login_Log].[ClientPassword]
      ,[Login_Log].[Offline]
");
            s.Where.Append(@"
where 1=1
    and [User_Data].[Account] = @Account ");
            s.Parameters.Add(new SqlParameter("@Account", account));

            if (!string.IsNullOrEmpty(clientFlag))
            {
                s.Where.Append(@"
    and [Login_Log].[ClientFlag] = @ClientFlag ");
                s.Parameters.Add(new SqlParameter("@ClientFlag", clientFlag));
            }

            if (!string.IsNullOrEmpty(clientPassword))
            {
                s.Where.Append(@"
    and [Login_Log].[ClientPassword] = @ClientPassword ");
                s.Parameters.Add(new SqlParameter("@ClientPassword", clientPassword));
            }

            if (dateTimeUpdate_Start > DateTime.MinValue)
            {
                s.Where.Append(@"
    and [Login_Log].[DateTimeUpdate] >= @DateTimeUpdate_Start ");
                s.Parameters.Add(new SqlParameter("@DateTimeUpdate_Start", dateTimeUpdate_Start));
            }

            if (offline > -1)
            {
                s.Where.Append(@"
    and [Login_Log].[Offline] = @Offline ");
                s.Parameters.Add(new SqlParameter("@Offline", offline > 0 ? 1 : 0));
            }

            return DALArgs.GetInstance().CurrentDatabaseUtil.ExecuteDataAdapter(s);
        }

        /// <summary>
        /// 插入新的登录日志。
        /// </summary>
        /// <param name="account">登录用户账号。</param>
        /// <param name="clientFlag">客户端GUID。</param>
        /// <returns>新数据信息。</returns>
        public DataSet Insert_Login_Log(string account, string clientFlag)
        {
            var s = DatabaseUtil.SelectBuilder.GetInstance();
            s.ConnectionString = DALArgs.GetInstance().CurrentConnectString;
            s.Header.Append(@"
insert into [Login_Log](
    [FK_User_Data], 
    [DateTimeLogin], 
    [DateTimeUpdate], 
    [ClientFlag], 
    [ClientPassword], 
    [Offline]
)
(
    select top 1 [GUID],
        @DateTimeUpdate, 
        @DateTimeUpdate, 
        @ClientFlag, 
        @ClientPassword, 
        0
    from [User_Data] where [Account] = @Account
)
select @ClientPassword as 'ClientPassword'
");
            s.Parameters.Add(new SqlParameter("@Account", account));
            s.Parameters.Add(new SqlParameter("@ClientFlag", clientFlag));
            s.Parameters.Add(new SqlParameter("@ClientPassword", Guid.NewGuid().ToString()));
            s.Parameters.Add(new SqlParameter("@DateTimeUpdate", DateTime.Now));
            return DALArgs.GetInstance().CurrentDatabaseUtil.ExecuteDataAdapter(s);
        }

        /// <summary>
        /// 更新用户表Ban数据。
        /// </summary>
        /// <param name="account">用户账号。</param>
        /// <param name="ban">禁止标记。</param>
        /// <returns>更新后结果。</returns>
        public DataSet Update_User_Data_Ban(string account, int ban)
        {
            var s = DatabaseUtil.SelectBuilder.GetInstance();
            s.ConnectionString = DALArgs.GetInstance().CurrentConnectString;
            s.Header.Append(@"
update [User_Data] set [Ban] = @Ban where Account = @Account
select [Ban] from [User_Data] where Account = @Account
");
            s.Parameters.Add(new SqlParameter("@Account", account));
            s.Parameters.Add(new SqlParameter("@Ban", ban > 0 ? 1 : 0));
            return DALArgs.GetInstance().CurrentDatabaseUtil.ExecuteDataAdapter(s);
        }

        /// <summary>
        /// 更新用户日志在线标记。
        /// </summary>
        /// <param name="account">账号。</param>
        /// <param name="offlineFlag">更新后在线标记。</param>
        /// <param name="count">更新数量。</param>
        /// <param name="offline">在线状态。</param>
        /// <param name="clientPassword">客户端密码。</param>
        /// <param name="dateTimeUpdate_Start">最后在线时间范围起点。</param>
        /// <param name="dateTimeUpdate_End">最后在线时间范围终点。</param>
        /// <returns>更新结果反馈。</returns>
        public DataSet Update_Login_Log_Offline(string account, bool offlineFlag, int count = 1, int offline = -1, string clientPassword = null, DateTime dateTimeUpdate_Start = new DateTime(), DateTime dateTimeUpdate_End = new DateTime())
        {
            var s = DatabaseUtil.SelectBuilder.GetInstance();
            s.ConnectionString = DALArgs.GetInstance().CurrentConnectString;
            StringBuilder select = new StringBuilder();
            if (count > 1)
            {
                select.Append(string.Format(@"select top {0} ClientPassword ", count));
            }
            else
            {
                select.Append(@"select ClientPassword ");
            }
            StringBuilder from = new StringBuilder(@"
from [Login_Log] ");
            StringBuilder where = new StringBuilder(@"
where 1=1 ");

            if (offline > -1)
            {
                where.Append(@"
and Offline = @Offline ");
                s.Parameters.Add(new SqlParameter("@Offline", offline > 0 ? 1 : 0));
            }

            if (!string.IsNullOrEmpty(clientPassword))
            {
                where.Append(@"
and ClientPassword = @ClientPassword ");
                s.Parameters.Add(new SqlParameter("@ClientPassword", clientPassword));
            }

            if (dateTimeUpdate_Start > DateTime.MinValue)
            {
                where.Append(@"
and DateTimeUpdate >= @DateTimeUpdate_Start ");
                s.Parameters.Add(new SqlParameter("@DateTimeUpdate_Start", dateTimeUpdate_Start));
            }

            if (dateTimeUpdate_End > DateTime.MinValue)
            {
                where.Append(@"
and DateTimeUpdate <= @DateTimeUpdate_End ");
                s.Parameters.Add(new SqlParameter("@DateTimeUpdate_End", dateTimeUpdate_End));
            }

            s.Header.Append(string.Format(@"
update [Login_Log] set [Offline] = @OfflineFlag 
where Exists(select * from ({0} {1} {2}) as A where [Login_Log].[ClientPassword] = A.[ClientPassword])
    and Exists(select * from [User_Data] where [User_Data].[Account] = @Account and [User_Data].[GUID] = [Login_Log].[FK_User_Data])
select @@ROWCOUNT as TotalCount
", select, from, where));
            s.Parameters.Add(new SqlParameter("@Account", account));
            s.Parameters.Add(new SqlParameter("@OfflineFlag", offlineFlag ? 1 : 0));
            return DALArgs.GetInstance().CurrentDatabaseUtil.ExecuteDataAdapter(s);
        }

        /// <summary>
        /// 更新用户日志更新日期。
        /// </summary>
        /// <param name="clientPassword">客户端密码。</param>
        /// <returns>更新结果。</returns>
        public DataSet Update_Login_Log_DateTimeUpdate(string clientPassword)
        {
            var s = DatabaseUtil.SelectBuilder.GetInstance();
            s.ConnectionString = DALArgs.GetInstance().CurrentConnectString;
            s.Header.Append(@"
update [Login_Log] set [DateTimeUpdate] = GetDate() where ClientPassword = @ClientPassword
select [DateTimeUpdate] from [Login_Log] where ClientPassword = @ClientPassword
");
            s.Parameters.Add(new SqlParameter("@ClientPassword", clientPassword));
            return DALArgs.GetInstance().CurrentDatabaseUtil.ExecuteDataAdapter(s);
        }

        /// <summary>
        /// 用户组与地址关联性查询。
        /// </summary>
        /// <param name="userGroup_GUID">用户组GUID。</param>
        /// <param name="url_GUID">地址GUID。</param>
        /// <param name="url">地址。</param>
        /// <param name="urlCode">地址编码。</param>
        /// <param name="forbidden"></param>
        /// <returns>用户组与地址关联性数据集。</returns>
        public DataSet Select_Url_Data_UserGroup_Type(string userGroup_GUID = null, string url_GUID = null, string url = null, string urlCode = null,bool? forbidden=null)
        {
            var s = DatabaseUtil.SelectBuilder.GetInstance();
            s.ConnectionString = DALArgs.GetInstance().CurrentConnectString;
            s.From.Append(@"
from [UserGroup_Type] join [Url_Data_UserGroup_Type_Link] on [UserGroup_Type].[GUID] = [Url_Data_UserGroup_Type_Link].[FK_UserGroup_Type]
    inner join [Url_Data] on [Url_Data_UserGroup_Type_Link].[FK_Url_Data] = [Url_Data].[GUID]
");
            s.Select.Append(@"
select [UserGroup_Type].[UserGroupName]
      ,[Url_Data_UserGroup_Type_Link].[Forbidden]
      ,[Url_Data_UserGroup_Type_Link].[PriorityLevel]
      ,[Url_Data].[GUID] as [Url_GUID]
      ,[Url_Data].[ParentGUID]
      ,[Url_Data].[Url]
      ,[Url_Data].[UrlCode]
      ,[Url_Data].[UrlParams]
      ,[Url_Data].[UrlName]
      ,[Url_Data].[UrlIndex]
");
            s.Where.Append(@"
where 1=1 
    and [Url_Data].[Show] = 1");

            if (!string.IsNullOrEmpty(userGroup_GUID))
            {
                s.Where.Append(@"
    and [UserGroup_Type].[GUID] = @UserGroup_GUID ");
                s.Parameters.Add(new SqlParameter("@UserGroup_GUID", userGroup_GUID));
            }

            if (!string.IsNullOrEmpty(url_GUID))
            {
                s.Where.Append(@"
    and [Url_Data].[GUID] = @Url_GUID ");
                s.Parameters.Add(new SqlParameter("@Url_GUID", url_GUID));
            }

            if (!string.IsNullOrEmpty(url))
            {
                s.Where.Append(@"
    and Lower([Url_Data].[Url]) = Lower(@Url) ");
                s.Parameters.Add(new SqlParameter("@Url", url));
            }

            if (!string.IsNullOrEmpty(urlCode))
            {
                s.Where.Append(@"
    and Lower([Url_Data].[UrlCode]) = Lower(@UrlCode) ");
                s.Parameters.Add(new SqlParameter("@UrlCode", urlCode));
            }
            if(forbidden!=null)
            {
                s.Where.Append(@"and Lower([Url_Data_UserGroup_Type_Link].[Forbidden]) = @forbidden ");
                s.Parameters.Add(new SqlParameter("@forbidden", forbidden));
            }
            return DALArgs.GetInstance().CurrentDatabaseUtil.ExecuteDataAdapter(s);
        }

        /// <summary>
        /// 查询url信息。
        /// </summary>
        /// <param name="guid">urlId。</param>
        /// <param name="parentGuid"></param>
        /// <param name="url">url地址。</param>
        /// <param name="urlCode">url代码。</param>
        /// <param name="urlName">url名称。</param>
        /// <param name="userAuthentication">是否验证用户。</param>
        /// <param name="show">是否显示。</param>
        /// <returns>url信息数据集。</returns>
        public DataSet Select_Url_Data(string guid = null,string parentGuid="", string url = null, string urlCode = null, string urlName = null, int userAuthentication = -1, int show = -1)
        {
            var s = DatabaseUtil.SelectBuilder.GetInstance();
            s.ConnectionString = DALArgs.GetInstance().CurrentConnectString;
            s.From.Append(@"
from [Url_Data] ");
            s.Select.Append(@"
select [GUID] as [Url_GUID]
      ,[ParentGUID]
      ,[Url]
      ,[UrlCode]
      ,[UrlParams]
      ,[UrlName]
      ,[UrlIndex]
      ,[UserAuthentication]
      ,[Show] ");
            s.Where.Append(@"
where 1=1");

            if (!string.IsNullOrEmpty(guid))
            {
                s.Where.Append(@"
    and Lower([Url_Data].[GUID]) = @GUID ");
                s.Parameters.Add(new SqlParameter("@GUID", guid));
            }

            if (parentGuid==null)
            {
                s.Where.Append(@"
    and [Url_Data].[ParentGUID] is null ");
                s.Parameters.Add(new SqlParameter("@parentGuid", parentGuid));
            }
            else if (!String.IsNullOrEmpty(parentGuid))
            {
                s.Where.Append(@"
    and [Url_Data].[ParentGUID]= @parentGuid");
                s.Parameters.Add(new SqlParameter("@parentGuid", parentGuid));
            }

            if (!string.IsNullOrEmpty(url))
            {
                s.Where.Append(@"
    and Lower([Url_Data].[Url]) = Lower(@Url) ");
                s.Parameters.Add(new SqlParameter("@Url", url));
            }

            if (!string.IsNullOrEmpty(urlCode))
            {
                s.Where.Append(@"
    and Lower([Url_Data].[UrlCode]) = Lower(@UrlCode) ");
                s.Parameters.Add(new SqlParameter("@UrlCode", urlCode));
            }

            if (!string.IsNullOrEmpty(urlName))
            {
                s.Where.Append(@"
    and Lower([Url_Data].[UrlName]) like @UrlName escape '\' ");
                s.Parameters.Add(new SqlParameter("@UrlName", string.Format("%{0}%", DatabaseUtil.FormatSqlParameterValue(urlName).ToLower())));
            }

            if (userAuthentication > -1)
            {
                s.Where.Append(@"
    and [Url_Data].[UserAuthentication] = @UserAuthentication ");
                s.Parameters.Add(new SqlParameter("@UserAuthentication", userAuthentication > 0 ? 1 : 0));
            }

            if (show > -1)
            {
                s.Where.Append(@"
    and [Url_Data].[Show] = @Show ");
                s.Parameters.Add(new SqlParameter("@Show", show > 0 ? 1 : 0));
            }

            return DALArgs.GetInstance().CurrentDatabaseUtil.ExecuteDataAdapter(s);
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
            var s = DatabaseUtil.SelectBuilder.GetInstance();
            s.ConnectionString = DALArgs.GetInstance().CurrentConnectString;
            s.From.Append(@"
from [User_Data] join [User_Data_UserGroup_Type_Link] on [User_Data].[GUID] = [User_Data_UserGroup_Type_Link].[FK_User_Data]
    join [UserGroup_Type] on [User_Data_UserGroup_Type_Link].[FK_UserGroup_Type] = [UserGroup_Type].[GUID] ");
            s.Select.Append(@"
select [UserGroup_Type].[GUID] as [UserGroup_GUID]
      ,[UserGroup_Type].[UserGroupName]
      ,[User_Data].[Account]
      ,[User_Data].[UserName] ");
            s.Where.Append(@"
where 1=1");

            if (!string.IsNullOrEmpty(user_GUID))
            {
                s.Where.Append(@"
    and [User_Data].[GUID] = @User_GUID ");
                s.Parameters.Add(new SqlParameter("@User_GUID", user_GUID));
            }

            if (!string.IsNullOrEmpty(userGroup_GUID))
            {
                s.Where.Append(@"
    and [UserGroup_Type].[GUID] = @UserGroup_GUID ");
                s.Parameters.Add(new SqlParameter("@UserGroup_GUID", userGroup_GUID));
            }

            if (!string.IsNullOrEmpty(account))
            {
                s.Where.Append(@"
    and [User_Data].[Account] = @Account ");
                s.Parameters.Add(new SqlParameter("@Account", account));
            }

            if (!string.IsNullOrEmpty(userName))
            {
                s.Where.Append(@"
    and Lower([User_Data].[UserName]) like @UserName escape '\' ");
                s.Parameters.Add(new SqlParameter("@UserName", string.Format("%{0}%", DatabaseUtil.FormatSqlParameterValue(userName).ToLower())));
            }

            if (!string.IsNullOrEmpty(userGroupName))
            {
                s.Where.Append(@"
    and Lower([UserGroup_Type].[UserGroupName]) like @UserGroupName escape '\' ");
                s.Parameters.Add(new SqlParameter("@UserGroupName", string.Format("%{0}%", DatabaseUtil.FormatSqlParameterValue(userGroupName).ToLower())));
            }

            return DALArgs.GetInstance().CurrentDatabaseUtil.ExecuteDataAdapter(s);
        }

       
    }
}
