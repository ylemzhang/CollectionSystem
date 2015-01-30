using System;
using System.Text;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;



public partial class Pages_Calender:PageBase 
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }
    protected void Calendar1_SelectionChanged(object sender, EventArgs e)
    {
        DateTime dt = this.CalendarSelect.SelectedDate;
        string dateValue = dt.Year.ToString() + "-" + dt.Month.ToString() + "-" + dt.Day.ToString();
        string script = string.Format("window.returnValue='{0}';window.close()", dateValue);
        base.ExceuteScript(script);
    }

    
}
