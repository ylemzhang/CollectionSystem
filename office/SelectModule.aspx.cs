using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel;

public partial class SelectModule : Page
{
    /// <summary>
    /// 页面载入事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitPage();
        }
    }
    /// <summary>
    /// 初始化页面
    /// </summary>
    private void InitPage()
    {
        DataBind();
    }
    /// <summary>
    /// 数据绑定到列表
    /// </summary>
    private void DataBind()
    {
        WebBean bean = WebBean.GetInstance();
        var model = new UrlDataModel
                        {
                            GUID = string.Empty,
                            Url = TextBoxUrl.Text.Trim(),
                            UrlName = TextBoxUrlName.Text.Trim()
                        };
        DataSet dataSet = bean.SelectUrlData(model);
        Repeater1.DataSource = dataSet;
        Repeater1.DataBind();
    }
    /// <summary>
    /// 查询按钮事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonQuery_Click(object sender, EventArgs e)
    {
        DataBind();
    }
    /// <summary>
    /// 保存按钮事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonSave_Click(object sender, EventArgs e)
    {
        var selectedValue = string.Empty;
        foreach (RepeaterItem item in Repeater1.Items)
        {
            if (((RadioButton)item.FindControl("RadioButtonSelect")).Checked)
            {
                selectedValue = ((HiddenField) item.FindControl("HiddenFieldGUID")).Value.Trim();
                break; 
            }
        }
        if (Request.QueryString["urlGuid"] == null) return;
        var urlGuid = Request.QueryString["urlGuid"];
        if(urlGuid!=selectedValue.Trim())
        {
            WebBean bean = WebBean.GetInstance();
            DataSet dataSet = bean.Select_Url_Data(urlGuid,string.Empty);
            if (dataSet == null || dataSet.Tables.Count <= 0 || dataSet.Tables[0].Rows.Count <= 0) return;
            UrlDataModel model = new UrlDataModel();
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                model = new UrlDataModel
                {
                    GUID = row["Url_GUID"].ToString(),
                    ParentGUID = selectedValue,
                    Url = row["Url"].ToString(),
                    UrlCode = row["UrlCode"].ToString(),
                    UrlParams = row["UrlParams"].ToString(),
                    UrlName = row["UrlName"].ToString(),
                    UrlIndex = int.Parse(row["UrlIndex"].ToString()),
                    UserAuthentication = (Boolean)row["UserAuthentication"],
                    Show = (Boolean)row["Show"]
                };
            }
            try
            {
                bean.UpdateUrl(model);
                
            }
            catch (Exception)
            {

                throw;
            }
            if (!Page.ClientScript.IsStartupScriptRegistered("close"))
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "close",
                                                        "<script language='javascript'>top.$.close('select');</script>");
            }
        }
       
        
    }
    /// <summary>
    /// 行绑定事件
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (Request.QueryString["urlGuid"] == null) return;
        var urlGuid = Request.QueryString["urlGuid"];
        if(e.Item.ItemType==ListItemType.Item||e.Item.ItemType==ListItemType.AlternatingItem)
        {
            HiddenField hiddenField = e.Item.FindControl("HiddenFieldGUID") as HiddenField;
            if (hiddenField.Value.Trim() == urlGuid)
            {
                e.Item.Visible = false;
            }
        }
    }
}