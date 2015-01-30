using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class UserProfile : PageBase
{

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!this.IsPostBack)
        {
            translation();
            bindData();
        }

    }
    protected void translation()
    {
        btnSave.Text = Common.StrTable.GetStr("save");
        //this.RadioButtonList1.Items[0].Text = Common.StrTable.GetStr("descorder");
        //this.RadioButtonList1.Items[1].Text = Common.StrTable.GetStr("ascorder");
    }


    private void bindData()
    {
        string[] genders = new string[] { "M", "F" };
        this.ddlGender.DataSource = genders;
        this.ddlGender.DataBind();

        BLL.SystemUser user = base.CurrentUser;
        this.txtRealName.Text = user.RealName;
        this.txtPhone.Text = user.Phone;
        this.txtMobile.Text = user.Mobile;
        this.txtEmail.Text = user.Email;
        ddlGender.Text = user.Gender;
        this.txtPageCount.Text = user.PageCount.ToString();
        this.txtColumn.Text = user.CaseDisplayColumn.ToString();
        this.txtPromisdate.Text = user.AlertDays.ToString();

     
       

    }



    protected void btnSave_Click(object sender, EventArgs e)
    {
        BLL.SystemUser user = base.CurrentUser;

      
        int current;
        if (this.txtPageCount.Text.Trim() == "")
        {
            base.ExceuteScript("alert('请输入5到1000之间的数据')");
            return;
        }
        try
        {
           current = int.Parse(this.txtPageCount.Text.Trim());
            if (current < 5 || current > 10000)
            {
                base.ExceuteScript("alert('请输入5到10000之间的数据')");
                return;
            }
            user.PageCount = current;


        }
        catch
        {
            base.ExceuteScript("alert('请输入5到1000之间的数据')");
            return;
        }

        if (this.txtColumn.Text.Trim() == "")
        {
            Alert("input110");
            return;
        }
        try
        {
             current = int.Parse(this.txtColumn.Text.Trim());
            if (current < 1 || current > 10)
            {
                Alert("input110");
                return;
            }
            user.CaseDisplayColumn = current;


        }
        catch
        {
            Alert("input110");
            return;
        }


        try
        {
            current = int.Parse(this.txtPromisdate.Text.Trim());
            if (current < 1 || current > 30)
            {
                base.ExceuteScript ("alert('请输入0到30之间的数据')");
                return;
            }
            user.AlertDays = current; 


        }
        catch
        {
            base.ExceuteScript("alert('请输入0到30之间的数据')");
            return;
        }

        user.RealName = this.txtRealName.Text.Trim();
        user.Phone = this.txtPhone.Text.Trim();
        user.Mobile = this.txtMobile.Text.Trim();
        user.Email = this.txtEmail.Text.Trim();


        user.Gender = this.ddlGender.Text;

        BLL.AdminBLL.UpdateCurrentUser(user);

        Alert("saveSuccess");


    }
}
