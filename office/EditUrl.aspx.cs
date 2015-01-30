using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel;

public partial class EditUrl : System.Web.UI.Page
{
    /// <summary>
    /// 传入的UrlGuid
    /// </summary>
    public string UrlGuid { 
        get
        {
            return ViewState["UrlGuid"] != null ? ViewState["UrlGuid"].ToString() : string.Empty;
        }
        set { ViewState["UrlGuid"] = value; }
    }

    /// <summary>
    /// 保存是增加或是修改
    /// </summary>
    public string AddOrUpdate
    {
        get
        {
            return ViewState["AddOrUpdate"] != null ? ViewState["AddOrUpdate"].ToString() : string.Empty;
        }
        set { ViewState["AddOrUpdate"] = value; }
    }

    /// <summary>
    /// 页面载入事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            UrlGuid = Request.QueryString["urlGuid"]?? string.Empty;
            InitPage(UrlGuid);
        }
    }

    /// <summary>
    /// 初始化页面
    /// </summary>
    /// <param name="urlGuid"></param>
    private void InitPage(string urlGuid)
    {
        //如果传入的参数为空，则是插入调用
        if(String.IsNullOrEmpty(urlGuid))
        {
            AddOrUpdate = "add";
            TextBoxUrl.ReadOnly = false;
            ClearTextBox();
        }
        else
        {
            AddOrUpdate = "update";
            UrlDataModel model=new UrlDataModel
                                   {
                                       GUID = urlGuid,
                                       UrlName = string.Empty,
                                       UserAuthentication = null,
                                       Show = null
                                   };
            WebBean bean = WebBean.GetInstance();
            DataSet dataSet = bean.SelectUrlData(model);
            if (dataSet == null || dataSet.Tables.Count <= 0 || dataSet.Tables[0].Rows.Count <= 0) return;
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                TextBoxUrlName.Text = row["UrlName"].ToString();
                TextBoxUrlCode.Text = row["UrlCode"].ToString();
                TextBoxUrl.Text = row["Url"].ToString();
                TextBoxParams.Text = row["UrlParams"].ToString();
                DropDownListAuthentication.Items.FindByValue(Convert.ToBoolean(row["UserAuthentication"].ToString()) ? "0" : "1").Selected=true;
                DropDownListShow.Items.FindByValue(Convert.ToBoolean(row["Show"].ToString()) ? "0" : "1").Selected = true;
            }
        }
    }

    /// <summary>
    /// 清空数据
    /// </summary>
    private void ClearTextBox()
    {
        TextBoxUrlName.Text = string.Empty;
        TextBoxUrlCode.Text = string.Empty;
        TextBoxUrl.Text = string.Empty;
        TextBoxParams.Text = string.Empty;
        DropDownListShow.SelectedItem.Value = "0";
        DropDownListAuthentication.SelectedItem.Value = "0";
    }

    /// <summary>
    /// 保存事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonSave_Click(object sender, EventArgs e)
    {
        try
        {
            WebBean bean = WebBean.GetInstance();
            var model=new UrlDataModel
                                   {
                                       GUID = "add".Equals(AddOrUpdate) ? Guid.NewGuid().ToString() : UrlGuid,
                                       ParentGUID = null,
                                       Url = TextBoxUrl.Text.Trim(),
                                       UrlCode = TextBoxUrlCode.Text.Trim(),
                                       UrlParams = TextBoxParams.Text.Trim(),
                                       UrlName = TextBoxUrlName.Text.Trim(),
                                       UserAuthentication = DropDownListAuthentication.SelectedValue == "0",
                                       Show = DropDownListShow.SelectedValue == "0"
                                   };
           
            if ("add".Equals(AddOrUpdate))
            {
                bean.AddUrl(model);
            }
            else
            {
                bean.UpdateUrl(model);
            }
            Show(this.Page, "保存成功！");
        }
        catch (Exception ex)
        {
            Show(this.Page,"保存失败！");
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