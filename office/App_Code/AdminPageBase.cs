using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for AdminPageBase
/// </summary>
public class AdminPageBase:PageBase
{
    public AdminPageBase()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    protected override void OnLoad(EventArgs e)
    {
        //if (!base.IsAdmin)
        //{
        //    ExceuteScript("window.top.location.href='Nopermission.htm'");
        //    return;
        //}
        base.OnLoad(e);
    }
}
