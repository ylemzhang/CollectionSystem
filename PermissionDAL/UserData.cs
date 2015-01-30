using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using DataModel;

namespace DAL
{
    public partial class DALEntity
    {
        /// <summary>
        /// 根据操作类型插入或更新一条用户数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="type"></param>
        public void AddOrUpdateUser(UserDataModel model,string type)
        {
            var s = DatabaseUtil.SelectBuilder.GetInstance();
            s.ConnectionString = DALArgs.GetInstance().CurrentConnectString;
            if("update".Equals(type))
            {
                s.Header.Append(@"update [User_Data] set [Account] = @Account
      ,[UserName] = @UserName
      ,[Password] = @Password 
      ,[Ban] = @ban where GUID = @GUID");
            }
            else
            {
                s.Header.Append(
                    @"INSERT INTO [User_Data]
           ([GUID]
           ,[Account]
           ,[UserName]
           ,[Password]
           ,[Ban])
     VALUES (@GUID
           ,@Account
           ,@UserName
           ,@Password
           ,@Ban)");
            }
            
            s.Parameters.Add(new SqlParameter("@GUID", model.GUID));
            s.Parameters.Add(new SqlParameter("@Account", model.Account));
            s.Parameters.Add(new SqlParameter("@UserName", model.UserName));
            s.Parameters.Add(new SqlParameter("@Password", model.Password));
            s.Parameters.Add(new SqlParameter("@ban", model.Ban));
            DALArgs.GetInstance().CurrentDatabaseUtil.ExecuteDataAdapter(s);
        }
        /// <summary>
        /// 更新用户的禁止状态
        /// </summary>
        /// <param name="userGUID">用户GUID</param>
        /// <param name="ban">当前状态</param>
        public void ChangeUserState(string userGUID,bool ban)
        {
            var s = DatabaseUtil.SelectBuilder.GetInstance();
            s.ConnectionString = DALArgs.GetInstance().CurrentConnectString;

            s.Header.Append(@"update [User_Data] set [Ban] = @ban where GUID = @GUID");
            s.Parameters.Add(new SqlParameter("@ban", !ban));
            s.Parameters.Add(new SqlParameter("@GUID", userGUID));
            DALArgs.GetInstance().CurrentDatabaseUtil.ExecuteDataAdapter(s);
        }

        /// <summary>
        /// 把用户插入用户数组
        /// </summary>
        /// <param name="userGUID">用户GUID</param>
        /// <param name="userGroupGUID">用户组GUID</param>
        public void AddUserToUserGroup(string userGUID, string userGroupGUID, bool addOrDelete)
        {
            var s = DatabaseUtil.SelectBuilder.GetInstance();
            s.ConnectionString = DALArgs.GetInstance().CurrentConnectString;
            if (addOrDelete)
            {
                s.Header.Append(@" IF NOT EXISTS(SELECT * FROM dbo.User_Data_UserGroup_Type_Link WHERE FK_User_Data=@FK_User_Data AND FK_UserGroup_Type=@FK_UserGroup_Type)
                               BEGIN
                                  insert into User_Data_UserGroup_Type_Link (FK_User_Data,FK_UserGroup_Type) values (@FK_User_Data,@FK_UserGroup_Type)
                               END");
                s.Parameters.Add(new SqlParameter("@FK_User_Data", userGUID));
                s.Parameters.Add(new SqlParameter("@FK_UserGroup_Type", userGroupGUID));
            }
            else
            {
                s.Header.Append(@" DELETE FROM User_Data_UserGroup_Type_Link WHERE 1=1");
                if (!String.IsNullOrEmpty(userGUID))
                {
                    s.Header.Append(@" and FK_User_Data=@FK_User_Data");
                    s.Parameters.Add(new SqlParameter("@FK_User_Data", userGUID));
                }
                if (!String.IsNullOrEmpty(userGroupGUID))
                {
                    s.Header.Append(@"  AND FK_UserGroup_Type=@FK_UserGroup_Type");
                    s.Parameters.Add(new SqlParameter("@FK_UserGroup_Type", userGroupGUID));
                }
            }
            DALArgs.GetInstance().CurrentDatabaseUtil.ExecuteDataAdapter(s);
        }

        /// <summary>
        /// 根据用户编号删除一个用户
        /// </summary>
        /// <param name="userGUID">用户GUID</param>
        public void DeleteUserData(string userGUID)
        {
            var s = DatabaseUtil.SelectBuilder.GetInstance();
            s.ConnectionString = DALArgs.GetInstance().CurrentConnectString;
            s.Header.Append(@" DELETE FROM [User_Data] WHERE 1=1");
            if (!String.IsNullOrEmpty(userGUID))
            {
                s.Header.Append(@" and GUID=@userGUID");
                s.Parameters.Add(new SqlParameter("@userGUID", userGUID));
            }
            DALArgs.GetInstance().CurrentDatabaseUtil.ExecuteDataAdapter(s);
        }
    }
}
