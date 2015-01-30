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
        /// 插入一条数据
        /// </summary>
        /// <param name="model"></param>
        public void AddUrl(UrlDataModel model)
        {
            var s = DatabaseUtil.SelectBuilder.GetInstance();
            s.ConnectionString = DALArgs.GetInstance().CurrentConnectString;
            s.Header.Append(@" INSERT INTO [Url_Data]
           ([GUID]
           ,[ParentGUID]
           ,[Url]
           ,[UrlCode]
           ,[UrlParams]
           ,[UrlName]
           ,[UserAuthentication]
           ,[Show])
     VALUES
           (@GUID
           ,@ParentGUID
           ,@Url
           ,@UrlCode
           ,@UrlParams
           ,@UrlName
           ,@UserAuthentication
           ,@Show)");

            s.Parameters.Add(new SqlParameter("@GUID", model.GUID));
            s.Parameters.Add(new SqlParameter("@ParentGUID", model.ParentGUID));
            s.Parameters.Add(new SqlParameter("@Url", model.Url));
            s.Parameters.Add(new SqlParameter("@UrlCode", model.UrlCode));
            s.Parameters.Add(new SqlParameter("@UrlParams", model.UrlParams));
            s.Parameters.Add(new SqlParameter("@UrlName", model.UrlName));
            s.Parameters.Add(new SqlParameter("@UserAuthentication", model.UserAuthentication));
            s.Parameters.Add(new SqlParameter("@Show", model.Show));
            DALArgs.GetInstance().CurrentDatabaseUtil.ExecuteDataAdapter(s);
        }

        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <param name="model"></param>
        public void UpdateUrl(UrlDataModel model)
        {
            var s = DatabaseUtil.SelectBuilder.GetInstance();
            s.ConnectionString = DALArgs.GetInstance().CurrentConnectString;
            s.Header.Append(@" Update [Url_Data]
           SET [Url] = @Url
              ,[ParentGUID]=@ParentGuid
              ,[UrlCode] = @UrlCode
              ,[UrlParams] = @UrlParams
              ,[UrlName] = @UrlName
              ,[UrlIndex] = @UrlIndex
              ,[UserAuthentication] = @UserAuthentication
              ,[Show] = @Show where [GUID]=@GUID");
            s.Parameters.Add(new SqlParameter("@GUID", model.GUID));
            s.Parameters.Add(new SqlParameter("@ParentGUID", model.ParentGUID));
            s.Parameters.Add(new SqlParameter("@Url", model.Url));
            s.Parameters.Add(new SqlParameter("@UrlCode", model.UrlCode));
            s.Parameters.Add(new SqlParameter("@UrlParams", model.UrlParams));
            s.Parameters.Add(new SqlParameter("@UrlName", model.UrlName));
            s.Parameters.Add(new SqlParameter("@UrlIndex", model.UrlIndex));
            s.Parameters.Add(new SqlParameter("@UserAuthentication", model.UserAuthentication));
            s.Parameters.Add(new SqlParameter("@Show", model.Show));
            DALArgs.GetInstance().CurrentDatabaseUtil.ExecuteDataAdapter(s);
        }

        /// <summary>
        /// 清空表Module_Data
        /// </summary>
        public void DeleteAllUrl()
        {
            var s = DatabaseUtil.SelectBuilder.GetInstance();
            s.ConnectionString = DALArgs.GetInstance().CurrentConnectString;
            s.Header.Append(@"TRUNCATE TABLE[Url_Data_UserGroup_Type_Link] DELETE FROM [Url_Data]");
            DALArgs.GetInstance().CurrentDatabaseUtil.ExecuteDataAdapter(s);
        }

        /// <summary>
        /// 删除一条Module_Data数据
        /// </summary>
        public void DeleteUrl(string strWhere)
        {
            var s = DatabaseUtil.SelectBuilder.GetInstance();
            s.ConnectionString = DALArgs.GetInstance().CurrentConnectString;
            s.Header.Append(@"delete from [Url_Data]" + strWhere);
            DALArgs.GetInstance().CurrentDatabaseUtil.ExecuteDataAdapter(s);
        }

        /// <summary>
        /// 获取最大UrlIndex值
        /// </summary>
        /// <returns></returns>
        public int GetMaxUrlIndex()
        {
            var s = DatabaseUtil.SelectBuilder.GetInstance();
            s.ConnectionString = DALArgs.GetInstance().CurrentConnectString;
            s.Header.Append(@"SELECT maxvalue=ISNULL(MAX([UrlIndex]),1)  FROM [Url_Data]");
            DataSet dataSet= DALArgs.GetInstance().CurrentDatabaseUtil.ExecuteDataAdapter(s);
            if (dataSet == null || dataSet.Tables.Count <= 0 || dataSet.Tables[0].Rows.Count <= 0) return 0;
            return (from DataRow row in dataSet.Tables[0].Rows select Convert.ToInt32(row["maxvalue"].ToString())).FirstOrDefault();
        }
        /// <summary>
        /// 导入表Module_Data
        /// </summary>
        public void ImportUrl(string values)
        {
            var s = DatabaseUtil.SelectBuilder.GetInstance();
            s.ConnectionString = DALArgs.GetInstance().CurrentConnectString;
            s.Header.Append(@"INSERT INTO [Url_Data]
           ([GUID]
           ,[Url]
           ,[UrlCode]
           ,[UrlParams]
           ,[UrlName]
           ,[UrlIndex]
           ,[UserAuthentication]
           ,[Show]) " + values);
            DALArgs.GetInstance().CurrentDatabaseUtil.ExecuteDataAdapter(s);
        }

        /// <summary>
        /// 获取Module_Data数据
        /// </summary>
        public DataSet SelectUrlData(UrlDataModel model)
        {
            var s = DatabaseUtil.SelectBuilder.GetInstance();
            s.ConnectionString = DALArgs.GetInstance().CurrentConnectString;
            s.From.Append(@" FROM [Url_Data] ");
            s.Select.Append(@" SELECT [GUID]
                                      ,[ParentGUID]
                                      ,[Url]
                                      ,[UrlCode]
                                      ,[UrlParams]
                                      ,[UrlName]
                                      ,[UserAuthentication]
                                      ,[Show]");
            s.Where.Append(@" where 1=1 ");
            if(!string.IsNullOrEmpty(model.GUID))
            {
                s.Where.Append(@" and [GUID]=@GUID ");
                s.Parameters.Add(new SqlParameter("@GUID", model.GUID));
            }
            if(!string.IsNullOrEmpty(model.UrlName))
            {
                s.Where.Append(@" and [UrlName] like @urlName escape '\' ");
                s.Parameters.Add(new SqlParameter("@urlName", string.Format("%{0}%",DatabaseUtil.FormatSqlParameterValue(model.UrlName.Trim()))));
            }
            if (!string.IsNullOrEmpty(model.Url))
            {
                s.Where.Append(@" and [Url] like @url escape '\' ");
                s.Parameters.Add(new SqlParameter("@url",string.Format("%{0}%",DatabaseUtil.FormatSqlParameterValue(model.Url.Trim()))));
            }
            if(model.UserAuthentication!=null)
            {
                s.Where.Append(@" and [UserAuthentication]=@userAuthentication ");
                s.Parameters.Add(new SqlParameter("@userAuthentication", model.UserAuthentication));
            }
            if (model.Show != null)
            {
                s.Where.Append(@" and [Show]=@show ");
                s.Parameters.Add(new SqlParameter("@show", model.Show));
            }
            s.OrderBy.Append(@" order by [UrlIndex]");
            return DALArgs.GetInstance().CurrentDatabaseUtil.ExecuteDataAdapter(s);
        }

        /// <summary>
        /// 获取所有子节点URL数据集
        /// </summary>
        /// <param name="parentGuid">父节点</param>
        /// <returns></returns>
        public DataSet GetSubUrlData(string parentGuid)
        {
            var s = DatabaseUtil.SelectBuilder.GetInstance();
            s.ConnectionString = DALArgs.GetInstance().CurrentConnectString;
            s.Header.Append(@"WITH cte AS(
  SELECT *,0 AS LEVEL FROM [Url_Data] WHERE ParentGUID is null
  UNION ALL 
  SELECT a.*,b.LEVEL+1 AS LEVEL FROM [Url_Data] a,cte b WHERE a.ParentGUID=b.GUID
)
SELECT * FROM cte WHERE UserAuthentication=1");
            s.Parameters.Add(new SqlParameter("@parentGuid", parentGuid));
            return DALArgs.GetInstance().CurrentDatabaseUtil.ExecuteDataAdapter(s);
        }

        /// <summary>
        /// 获取地址和地址用户组关联信息
        /// </summary>
        /// <param name="userGroupGuid"></param>
        /// <param name="urlGuid"></param>
        /// <param name="parentGuid"></param>
        /// <param name="forbidden"></param>
        /// <returns></returns>
        public DataSet GetUrlAndUserGroupLink(string userGroupGuid,string urlGuid,string parentGuid,string forbidden)
        {
            var s = DatabaseUtil.SelectBuilder.GetInstance();
            s.ConnectionString = DALArgs.GetInstance().CurrentConnectString;
            s.Header.Append(@"SELECT  a.GUID AS Url_Guid ,
        a.Url ,
        a.UrlCode ,
        a.UrlName ,
        a.UrlParams ,
        UserAuthentication ,
        a.Show ,
        b.Forbidden ,
        b.PriorityLevel ,
        b.FK_UserGroup_Type AS UserGroup_Guid
FROM    dbo.Url_Data a
        LEFT JOIN dbo.Url_Data_UserGroup_Type_Link b ON a.GUID = b.FK_Url_Data
                                                        AND b.FK_UserGroup_Type = @UserGroupGuid
");
            s.Header.Append(@" where a.UserAuthentication=1");
            if (parentGuid==null)
            {
                s.Header.Append(@" AND ParentGUID IS NULL");
                s.Parameters.Add(new SqlParameter("@parentGuid", parentGuid));
            }
            else if (!String.IsNullOrEmpty(parentGuid))
            {
                s.Header.Append(@" AND a.ParentGUID=@parentGuid");
                s.Parameters.Add(new SqlParameter("@parentGuid", parentGuid));
            }

            if(!String.IsNullOrEmpty(urlGuid))
            {
                s.Header.Append(@" AND a.GUID=@urlGuid");
                s.Parameters.Add(new SqlParameter("@urlGuid", urlGuid));
            }

            switch (forbidden)
            {
                case "-1": s.Header.Append(@" AND b.Forbidden IS NULL"); break;
                case "0": s.Header.Append(@" AND b.Forbidden=1"); break;
                case "1": s.Header.Append(@" AND b.Forbidden=0"); break;
                default:break;
            }
            s.Parameters.Add(new SqlParameter("@UserGroupGuid", userGroupGuid));
            return DALArgs.GetInstance().CurrentDatabaseUtil.ExecuteDataAdapter(s);
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
             var s = DatabaseUtil.SelectBuilder.GetInstance();
            s.ConnectionString = DALArgs.GetInstance().CurrentConnectString;
            s.Header.Append(
                @" IF @ForbiddenType='-1'
BEGIN
   DELETE FROM dbo.Url_Data_UserGroup_Type_Link WHERE FK_Url_Data=@UrlGuid AND FK_UserGroup_Type=@UserGroupGuid
END
ELSE
BEGIN
   IF EXISTS(SELECT * FROM dbo.Url_Data_UserGroup_Type_Link WHERE FK_Url_Data=@UrlGuid AND FK_UserGroup_Type=@UserGroupGuid)
   BEGIN
      UPDATE dbo.Url_Data_UserGroup_Type_Link SET Forbidden=@ForbiddenType,PriorityLevel=@ProirotyLevel WHERE FK_Url_Data=@UrlGuid AND FK_UserGroup_Type=@UserGroupGuid
   END
   ELSE
   BEGIN
      INSERT dbo.Url_Data_UserGroup_Type_Link
              ( FK_Url_Data ,
                FK_UserGroup_Type ,
                Forbidden ,
                PriorityLevel
              )
      VALUES  ( @UrlGuid ,
                @UserGroupGuid ,
                @ForbiddenType ,
                @ProirotyLevel
              )
   END
END");
            s.Parameters.Add(new SqlParameter("@UrlGuid", urlGuid));
            s.Parameters.Add(new SqlParameter("@UserGroupGuid", userGroupGuid));
            s.Parameters.Add(new SqlParameter("@ProirotyLevel", proirotyLevel));
            s.Parameters.Add(new SqlParameter("@ForbiddenType", type));
            DALArgs.GetInstance().CurrentDatabaseUtil.ExecuteDataAdapter(s);
        }
    }
}
