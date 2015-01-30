using System;
using System.Data;
using System.Web.UI;

public partial class EditUserGroup : Page
{
    /// <summary>
    /// 传入的UserGroupGuid
    /// </summary>
    public string UserGroupGuid
    {
        get { return ViewState["UserGroupGuid"] != null ? ViewState["UserGroupGuid"].ToString() : string.Empty; }
        set { ViewState["UserGroupGuid"] = value; }
    }

    /// <summary>
    /// 保存是增加或是修改
    /// </summary>
    public string AddOrUpdate
    {
        get { return ViewState["AddOrUpdate"] != null ? ViewState["AddOrUpdate"].ToString() : string.Empty; }
        set { ViewState["AddOrUpdate"] = value; }
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
            UserGroupGuid = Request.QueryString["UserGroupGuid"] ?? string.Empty;
            InitPage(UserGroupGuid);
        }
    }

    /// <summary>
    /// 初始化页面
    /// </summary>
    /// <param name="UserGroupGuid"></param>
    private void InitPage(string UserGroupGuid)
    {
        //如果传入的参数为空，则是插入调用
        if (String.IsNullOrEmpty(UserGroupGuid))
        {
            AddOrUpdate = "add";
            return;
        }
        AddOrUpdate = "update";
        WebBean bean = WebBean.GetInstance();
        DataSet dataSet = bean.GetUserGroupList(UserGroupGuid, string.Empty);
        if (dataSet == null || dataSet.Tables.Count <= 0 || dataSet.Tables[0].Rows.Count <= 0) return;
        foreach (DataRow row in dataSet.Tables[0].Rows)
        {
            TextBoxUserGroupName.Text = row["UserGroupName"].ToString();
        }
    }

    /// <summary>
    /// 保存或修改权限组信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonSave_Click(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(TextBoxUserGroupName.Text.Trim()))
        {
            //提示错误消息"权限组名称不能为空"
            return;
        }
        try
        {
            WebBean bean = WebBean.GetInstance();
            DataSet dataSet = bean.SaveUserGroupData("add".Equals(AddOrUpdate) ? Guid.NewGuid().ToString() : UserGroupGuid, TextBoxUserGroupName.Text.Trim(),
                                                     AddOrUpdate);
            //提示消息"权限组信息保存成功！"
            TextBoxUserGroupName.Text = String.Empty;
            return;
        }
        catch (Exception ex)
        {
            //提示错误消息"权限组保存失败！"
            //写错误日志
            WriteLog.WriteExceptionLog(ex.ToString());
            return;
        }
    }
}