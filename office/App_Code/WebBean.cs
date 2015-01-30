using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using DataModel;

/// <summary>
///WebBean 的摘要说明
/// </summary>
public class WebBean
{
    private WebBean()
    {

    }

    public static WebBean GetInstance()
    {
        return new WebBean();
    }

    public DataSet GetUserGroupList(string userGroup_GUID, string userGroupName)
    {
       return WebAccess.GetInstance().GetUserGroupList(userGroup_GUID, userGroupName);
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
        return WebAccess.GetInstance().SaveUserGroupData(userGroup_GUID, userGroupName,type);
    }

    /// <summary>
    /// 查询用户组的所有用户。
    /// </summary>
    /// <param name="userGroup_GUID">用户组GUID。</param>
    /// <param name="userAccount">用户帐号</param>
    /// <param name="userName">用户名。</param>
    /// <param name="isBelongto">是否属于</param>
    /// <returns>用户和用户组信息数据集。</returns>
    public DataSet SelectGroupUsersData(string userGroup_GUID, string userAccount, string userName,bool? ban, bool isBelongto)
    {
        return WebAccess.GetInstance().SelectGroupUsersData(userGroup_GUID, userAccount, userName,ban, isBelongto);
    }

    /// <summary>
    /// 更新用户的禁止状态
    /// </summary>
    /// <param name="userGUID">用户GUID</param>
    /// <param name="ban">当前状态</param>
    public void ChangeUserState(string userGUID,bool ban)
    {
        WebAccess.GetInstance().ChangeUserState(userGUID, ban);
    }

    /// <summary>
    /// 把用户插入用户数组
    /// </summary>
    /// <param name="userGUID">用户GUID</param>
    /// <param name="userGroupGUID">用户组GUID</param>
    public void AddUserToUserGroup(string userGUID, string userGroupGUID,bool addOrDelete)
    {
        WebAccess.GetInstance().AddUserToUserGroup(userGUID, userGroupGUID, addOrDelete);
    }

    /// <summary>
    /// 插入一条数据
    /// </summary>
    /// <param name="model"></param>
    public void AddUrl(UrlDataModel model)
    {
        WebAccess.GetInstance().AddUrl(model);
    }

    /// <summary>
    /// 修改一条数据
    /// </summary>
    /// <param name="model"></param>
    public void UpdateUrl(UrlDataModel model)
    {
        WebAccess.GetInstance().UpdateUrl(model);
    }
    /// <summary>
    /// 清空表Module_Data
    /// </summary>
    public void DeleteAllUrl()
    {
        WebAccess.GetInstance().DeleteAllUrl();
    }
    /// <summary>
    /// 删除一条Module_Data数据
    /// </summary>
    public void DeleteUrl(string strWhere)
    {
        WebAccess.GetInstance().DeleteUrl(strWhere);
    }

    /// <summary>
    /// 获取最大UrlIndex值
    /// </summary>
    /// <returns></returns>
    public int GetMaxUrlIndex()
    {
        return WebAccess.GetInstance().GetMaxUrlIndex();
    }

    /// <summary>
    /// 导入表Module_Data
    /// </summary>
    public void ImportUrl(string values)
    {
        WebAccess.GetInstance().ImportUrl(values);
    }

    /// <summary>
    /// 获取所有Module_Data
    /// </summary>
    public DataSet SelectUrlData(UrlDataModel model)
    {
        return WebAccess.GetInstance().SelectUrlData(model);
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
        return WebAccess.GetInstance().Select_Url_Data(guid,parentGuid, url, urlCode, urlName, userAuthentication, show);
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
        return WebAccess.GetInstance().Select_Url_Data_UserGroup_Type(userGroup_GUID, url_GUID, url, urlCode, forbidden);
    }

    /// <summary>
    /// 获取所有子节点URL数据集
    /// </summary>
    /// <param name="parentGuid">父节点</param>
    /// <returns></returns>
    public DataSet GetSubUrlData(string parentGuid)
    {
        return WebAccess.GetInstance().GetSubUrlData(parentGuid);
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
        return WebAccess.GetInstance().GetUrlAndUserGroupLink(userGroupGuid, parentGuid,urlGuid, forbidden);
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
        WebAccess.GetInstance().SavePermission(urlGuid, userGroupGuid, proirotyLevel, type);
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
       return WebAccess.GetInstance().Select_UserGroup_Type_User_Data(user_GUID, userGroup_GUID, account, userName, userGroupName);
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
        return WebAccess.GetInstance().Select_User_Data(guid, account, userName, password, ban);
    }

    /// <summary>
    /// 根据操作类型插入或更新一条用户数据
    /// </summary>
    /// <param name="model"></param>
    /// <param name="type"></param>
    public void AddOrUpdateUser(UserDataModel model, string type)
    {
        WebAccess.GetInstance().AddOrUpdateUser(model, type);
    }

    /// <summary>
    /// 根据用户编号删除一个用户
    /// </summary>
    /// <param name="userGUID">用户GUID</param>
    public void DeleteUserData(string userGUID)
    {
        WebAccess.GetInstance().DeleteUserData(userGUID);
    }
}