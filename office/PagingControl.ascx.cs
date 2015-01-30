using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.UI.HtmlControls;
using System.Text;

public partial class PagingControl :System.Web.UI.UserControl 
{
    public int TotalPage
    {
        get
        {
            if (TotalRecords == 0) return 1;

            return (int)Math.Ceiling((double)TotalRecords / (double)ListRecordNumPerPage );
        }
       
    }

    public int ListRecordNumPerPage
    {
        get
        {
            return int.Parse(this.ViewState["ListRecordNumPerPage"].ToString());
        }
        set
        {
            this.ViewState["ListRecordNumPerPage"] = value;
        }
    }


    public event System.EventHandler PagingClick;

    public int TotalRecords
    {
        get
        {
            return int.Parse(this.ViewState["TotalRecords"].ToString());
        }
        set
        {
            this.ViewState["TotalRecords"] = value;
            CurrentPage = 1;
          
        }
    }

    public int CurrentPage
    {
        get
        {
            if (this.ViewState["CurrentPage"] == null)
            {
                return 1;
            }
           return  int.Parse(this.ViewState["CurrentPage"].ToString());
        }
        set
        {
            this.ViewState["CurrentPage"] = value;
            checkButtonStatus();
        }
    }



    protected void Page_Load(object sender, EventArgs e)
    {
             

    }

    private void click()
    {
        if (PagingClick !=null)
        {
            PagingClick(null, EventArgs.Empty);
        }
    }
    protected void lkFistPage_Click(object sender, EventArgs e)
    {
        CurrentPage = 1;
       

        click();
    }
    protected void lkNextpage_Click(object sender, EventArgs e)
    {
        CurrentPage++;
       
        click();
    }
    protected void lkPrepage_Click(object sender, EventArgs e)
    {
        CurrentPage--;
     
        click();
    }
    protected void lkLast_Click(object sender, EventArgs e)
    {
        CurrentPage = TotalPage;
       
        click();
    }

    private void checkButtonStatus()
    {
        if (TotalPage == 1)
        {
            this.lkFistPage.Enabled = false;
            this.lkLast .Enabled = false;
            this.lkPrepage.Enabled = false;
            this.lkNextpage .Enabled = false;
            this.btnGo.Enabled = false;
            this.txtCurrentPage.Text = "1";
            this.txtCurrentPage.Enabled = false;
            return;
        }
        this.lkFistPage.Enabled = true;
        this.lkLast.Enabled = true;
        this.lkPrepage.Enabled = true;
        this.lkNextpage.Enabled = true;
        this.btnGo.Enabled = true;
        this.txtCurrentPage.Text = CurrentPage.ToString();
        this.txtCurrentPage.Enabled = true;

        if (CurrentPage == 1)
        {
            this.lkPrepage.Enabled = false;
            this.lkFistPage.Enabled = false;
            return;
        }
        if (CurrentPage == TotalPage)
        {
            this.lkLast.Enabled = false;
            this.lkNextpage.Enabled = false;
            return;
        }

    }
    private void validInteger()
    {
        string valid = Common.StrTable.GetStr("integerValid");
        string script = "alert('{0}" + TotalPage + "')";
        script = string.Format(script, valid);
        ExceuteScript(script);
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        if (this.txtCurrentPage.Text.Trim() == "")
        {
            validInteger();
        }
        try
        {
            int current = int.Parse(this.txtCurrentPage.Text.Trim());
            if (current == 0 || current > TotalPage)
            {
                validInteger();
            }
            CurrentPage = current;
            click();
          
        }
        catch
        {
            validInteger();
        }
       
    }


    private void ExceuteScript(string script)
    {
        StringBuilder js = new StringBuilder();

        js.Append("<script language='javascript'>");

        js.Append(script);
        js.Append("</script>");

        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "js01", js.ToString());
    }

}
