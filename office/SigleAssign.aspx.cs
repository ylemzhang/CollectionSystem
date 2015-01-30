using System;
using System.Data;
using System.Web.UI;

public partial class SigleAssign : Page
{
    /// <summary>
    /// 权限组GUID
    /// </summary>
    private string GroupGuid
    {
        get { return ViewState["GroupGuid"] == null ? string.Empty : ViewState["GroupGuid"].ToString(); }
        set { ViewState["GroupGuid"] = value; }
    }
    /// <summary>
    /// URL GUID
    /// </summary>
    private string UrlGuid
    {
        get { return ViewState["UrlGuid"] == null ? string.Empty : ViewState["UrlGuid"].ToString(); }
        set { ViewState["UrlGuid"] = value; }
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
            if (Request.QueryString["userGroupGuid"] != null)
            {
                GroupGuid = Request.QueryString["userGroupGuid"];
                UrlGuid = Request.QueryString["urlGuid"];
            }
            InitPages();
        }
    }
    /// <summary>
    /// 初始化页面
    /// </summary>
    private void InitPages()
    {
        WebBean bean = WebBean.GetInstance();
        DataSet dataSet = bean.GetUrlAndUserGroupLink(GroupGuid, UrlGuid, string.Empty, null);
        if (dataSet == null || dataSet.Tables.Count <= 0 || dataSet.Tables[0].Rows.Count <= 0) return;
        foreach (DataRow row in dataSet.Tables[0].Rows)
        {
            TextBoxUrl.Text = row["Url"].ToString();
            TextBoxUrlCode.Text = row["UrlCode"].ToString();
            TextBoxParams.Text = row["UrlParams"].ToString();
            TextBoxUrlName.Text = row["UrlName"].ToString();
            TextBoxProirotyLevel.Text = string.IsNullOrEmpty(row["PriorityLevel"].ToString())
                                            ? "100"
                                            : row["PriorityLevel"].ToString();
            string val = "-1";
            if (!String.IsNullOrEmpty(row["Forbidden"].ToString()))
            {
                val = Convert.ToBoolean(row["Forbidden"].ToString()) ? "0" : "1";
            }
            DropDownListStatus.Items.FindByValue(val).Selected = true;
        }
    }
    /// <summary>
    /// 保存按钮事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonSave_Click(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(TextBoxProirotyLevel.Text.Trim()))
        {
            Show(Page, "请输入优先级");
            return;
        }
        int proirotyLevel;
        if (!int.TryParse(TextBoxProirotyLevel.Text.Trim(), out proirotyLevel))
        {
            Show(Page, "优先级输入不正确，请输入整数");
            return;
        }
        string type = "0";
        switch (DropDownListStatus.SelectedValue)
        {
            case "-1":
                type = "-1";
                break;
            case "0":
                type = "1";
                break;
            default:
                type = "0";
                break;
        }
        try
        {
            WebBean bean = WebBean.GetInstance();
            bean.SavePermission(UrlGuid, GroupGuid, proirotyLevel, type);
            Show(Page, "保存成功！");
        }
        catch (Exception ex)
        {
            Show(Page, "保存失败！");
            WriteLog.WriteExceptionLog(ex.ToString());
        }
    }

    /// <summary>
    /// 显示消息提示对话框
    /// </summary>
    /// <param name="page">当前页面指针，一般为this</param>
    /// <param name="msg">提示信息</param>
    public static void Show(Page page, string msg)
    {
        if (!page.ClientScript.IsStartupScriptRegistered("message"))
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message",
                                                    "<script language='javascript'>alert('" + msg.Replace("'", "‘") +
                                                    "');</script>");
        }
    }
}