using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel;

public partial class AddUser : System.Web.UI.Page
{
    /// <summary>
    /// 用户GUID
    /// </summary>
    private string UserGuid
    {
        get { return ViewState["UserGuid"] == null ? string.Empty : ViewState["UserGuid"].ToString(); }
        set { ViewState["UserGuid"] = value; }
    }
    /// <summary>
    /// 页面载入事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["userGuid"] != null)
            {
                UserGuid = Request.QueryString["userGuid"];
            }
            InitPages();
        }
    }
    /// <summary>
    /// 页面初始化
    /// </summary>
    private void InitPages()
    {
        InitUserInfo();
        InitUserGroup();
    }
    /// <summary>
    /// 初始化权限组信息
    /// </summary>
    private void InitUserGroup()
    {
        WebBean bean = WebBean.GetInstance();
        DataSet dataSet = bean.GetUserGroupList(String.Empty, string.Empty);
        if (dataSet == null || dataSet.Tables.Count <= 0 || dataSet.Tables[0].Rows.Count <= 0) return;
        foreach (DataRow row in dataSet.Tables[0].Rows)
        {
            CheckBoxListUserGroups.Items.Add(new ListItem(row["UserGroupName"].ToString(), row["GUID"].ToString()));
        }
        if(!String.IsNullOrEmpty(UserGuid))
        {
            DataSet myDataSet = bean.Select_UserGroup_Type_User_Data(UserGuid);
            if (myDataSet == null || myDataSet.Tables.Count <= 0 || myDataSet.Tables[0].Rows.Count <= 0) return;
            foreach (DataRow row in myDataSet.Tables[0].Rows)
            {
                CheckBoxListUserGroups.Items.FindByValue(row["UserGroup_GUID"].ToString()).Selected = true;
            }
        }
        
    }
    private void InitUserInfo()
    {
        if(String.IsNullOrEmpty(UserGuid)) return;
        WebBean bean = WebBean.GetInstance();
        DataSet dataSet = bean.Select_User_Data(UserGuid);
        if (dataSet == null || dataSet.Tables.Count <= 0 || dataSet.Tables[0].Rows.Count <= 0) return;
        foreach (DataRow row in dataSet.Tables[0].Rows)
        {
            TextBoxAccount.Text = row["Account"].ToString();
            TextBoxUserName.Text = row["UserName"].ToString();
            RadioButtonListBan.Items.FindByValue("0").Selected = Convert.ToBoolean(row["Ban"].ToString());
        }
    }
    /// <summary>
    /// 保存按钮事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonSave_Click(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(TextBoxAccount.Text.Trim()) || String.IsNullOrEmpty(TextBoxUserName.Text.Trim()) )
            return;
        if (RadioButtonListBan.SelectedItem == null) return;
        UserDataModel userDataModel=new UserDataModel
                                        {
                                            GUID = String.IsNullOrEmpty(UserGuid)?Guid.NewGuid().ToString():UserGuid,
                                            Account = TextBoxAccount.Text.Trim(),
                                            UserName = TextBoxUserName.Text.Trim(),
                                            Password = "111111",
                                            Ban = RadioButtonListBan.SelectedItem.Value == "0"
                                        };
        try
        {
            WebBean bean = WebBean.GetInstance();
            bean.AddOrUpdateUser(userDataModel,string.IsNullOrEmpty(UserGuid)?"add":"update");

            //如果用户组信息
            bean.AddUserToUserGroup(UserGuid,string.Empty,false);
            foreach (ListItem item in CheckBoxListUserGroups.Items)
            {
                if (item.Selected)
                {
                    bean.AddUserToUserGroup(userDataModel.GUID, item.Value, true);
                }
            }
            //if (CheckBoxListUserGroups.SelectedItem != null && DropDownListUserGroup.SelectedValue != "0")
            //{
            //    bean.AddUserToUserGroup(userDataModel.GUID, DropDownListUserGroup.SelectedValue, true);
            //}
        }
        catch(Exception ex)
        {
            WriteLog.WriteExceptionLog(ex.ToString());
        }
    }
}