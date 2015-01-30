using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public partial class DALEntity
    {
        /// <summary>
        /// 获取用户组信息。
        /// </summary>
        /// <param name="userGroup_GUID">用户组GUID。</param>
        /// <param name="userGroupName">用户组名。</param>
        /// <returns>用户和用户组信息数据集。</returns>
        public DataSet SelectUserGroupData(string userGroup_GUID = null, string userGroupName = null)
        {
            DatabaseUtil.SelectBuilder s = DatabaseUtil.SelectBuilder.GetInstance();
            s.ConnectionString = DALArgs.GetInstance().CurrentConnectString;
            s.From.Append(@" from [UserGroup_Type]  ");
            s.Select.Append(@"select[GUID],[UserGroupName]");
            s.Where.Append(@"where 1=1");

            if (!string.IsNullOrEmpty(userGroup_GUID))
            {
                s.Where.Append(@" and [UserGroup_Type].[GUID] = @UserGroup_GUID ");
                s.Parameters.Add(new SqlParameter("@UserGroup_GUID", userGroup_GUID));
            }

            if (!string.IsNullOrEmpty(userGroupName))
            {
                s.Where.Append(@" and Lower([UserGroupName]) like @UserGroupName escape '\' ");
                s.Parameters.Add(new SqlParameter("@UserGroupName",string.Format("%{0}%",
                                                                DatabaseUtil.FormatSqlParameterValue(userGroupName).
                                                                    ToLower())));
            }
            return DALArgs.GetInstance().CurrentDatabaseUtil.ExecuteDataAdapter(s);
        }

        /// <summary>
        /// 保存用户组信息。
        /// </summary>
        /// <param name="userGroup_GUID">用户组GUID。</param>
        /// <param name="userGroupName">用户组名。</param>
        /// <param name="type">保存方式add:增加 update:更新 delete:删除</param>
        /// <returns>用户和用户组信息数据集。</returns>
        public DataSet SaveUserGroupData(string userGroup_GUID = null, string userGroupName = null,string type="add")
        {
            var s = DatabaseUtil.SelectBuilder.GetInstance();
            s.ConnectionString = DALArgs.GetInstance().CurrentConnectString;
            if("add".Equals(type.Trim()))
            {
                s.Header.Append(@"insert into [UserGroup_Type] ([GUID],[UserGroupName]) values (@GUID,@UserGroupName)");
            }
            else if ("update".Equals(type.Trim()))
            {
                s.Header.Append(@"update [UserGroup_Type] set [UserGroupName] = @UserGroupName where GUID = @GUID");
            }
            else
            {
                s.Header.Append(@" delete from [User_Data_UserGroup_Type_Link] where FK_UserGroup_Type = @GUID");
                s.Header.Append(@" delete from [Url_Data_UserGroup_Type_Link] where FK_UserGroup_Type = @GUID");
                s.Header.Append(@" delete from [UserGroup_Type] where GUID = @GUID");
            }
            s.Header.Append(@" select * from [UserGroup_Type]");
            s.Parameters.Add(new SqlParameter("@UserGroupName", userGroupName));
            s.Parameters.Add(new SqlParameter("@GUID", userGroup_GUID));
            return DALArgs.GetInstance().CurrentDatabaseUtil.ExecuteDataAdapter(s);
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
            var s = DatabaseUtil.SelectBuilder.GetInstance();
            s.ConnectionString = DALArgs.GetInstance().CurrentConnectString;
            s.From.Append(@" from [User_Data] ");
            s.Select.Append(@"select [User_Data].[GUID],[User_Data].[Account],[User_Data].[UserName],[User_Data].[Ban] ");

            s.Where.Append(@" where 1=1 ");
            if (!string.IsNullOrEmpty(userGroup_GUID))
            {
                s.Where.Append(isBelongto
                                   ? @" AND GUID IN (SELECT [FK_User_Data] FROM [User_Data_UserGroup_Type_Link] where FK_UserGroup_Type=@userGroup_GUID)"
                                   : @" AND GUID NOT IN (SELECT [FK_User_Data] FROM [User_Data_UserGroup_Type_Link] where FK_UserGroup_Type=@userGroup_GUID)");
                s.Parameters.Add(new SqlParameter("@userGroup_GUID", userGroup_GUID));
            }
            if(ban!=null)
            {
                s.Where.Append(@" AND [User_Data].[Ban] =@ban ");
                s.Parameters.Add(new SqlParameter("@ban", ban));
            }
            
            
            if (!string.IsNullOrEmpty(userName))
            {
                s.Where.Append(@" AND [User_Data].[UserName] LIKE @userName escape '\' ");
                s.Parameters.Add(new SqlParameter("@userName", string.Format("%{0}%",
                                                               DatabaseUtil.FormatSqlParameterValue(userName).
                                                                   ToLower())));
            }
            if (!string.IsNullOrEmpty(userAccount))
            {
                s.Where.Append(@" AND [User_Data].[Account] LIKE @userAccount escape '\' ");
                s.Parameters.Add(new SqlParameter("@userAccount", string.Format("%{0}%",
                                               DatabaseUtil.FormatSqlParameterValue(userAccount).
                                                   ToLower())));
            }
            return DALArgs.GetInstance().CurrentDatabaseUtil.ExecuteDataAdapter(s);
        }
    }
}