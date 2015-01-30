<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectClass.aspx.cs" Inherits="SelectClass" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
 <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
    <title>请选择分类</title>

<script>
function selectClass()
{

var rtn="";
if (document.all.ddlClass.selectedIndex>-1)
{

rtn=document.all.ddlClass.options[document.all.ddlClass.selectedIndex].value;

}
   window.returnValue=rtn;
                    window.close();
}
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div align =center >
    <asp:DropDownList ID="ddlClass" runat="server" Width =85px  >
      </asp:DropDownList> <input type =button onclick ="selectClass()" value =select />
    </div>
    </form>
</body>
</html>
