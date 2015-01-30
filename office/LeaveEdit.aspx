<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LeaveEdit.aspx.cs" Inherits="LeaveEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head >
    <title>Untitled Page</title>
     <link href="CSS/css.css" type="text/css" rel="stylesheet">
      <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
     <script>
   String.prototype.Trim = function()
        {
            return this.replace(/(^\s*)|(\s*$)/g, "");
        }
    function check()
    {
    
   
    if (document.all.txtIn.value.Trim()=='')
    {
    alert("上班时间 <%=Common.StrTable.GetStr("shallnotbenull") %>");
    return false;
    }
    
    return true;
    }
    
  
   function refreshPage()
{

   document.location.href=document.location.href;
       
}
 </script>
</head>
<body bgcolor=#F7F7F7 >
    <form id="form1" runat="server" >
    <table ><tr><td height=5></td></tr></table>
   <b><%=Common.StrTable.GetStr("generalInformation") %>：</b><hr />
     <table width=380   cellspacing =0 >
      <colgroup>
    <col width =100 />
    <col width =250 />
  
   
    </colgroup>
   
     <tr><td><%=Common.StrTable.GetStr("userName") %>：</td><td> <asp:TextBox ID="txtUserName" runat="server"  Width =245   Enabled =false ></asp:TextBox></td></tr>
      <tr><td><%=Common.StrTable.GetStr("punchIn") %>：</td><td> <asp:TextBox ID="txtIn" runat="server" Width =245   ></asp:TextBox><span style="color:Red">*</span></td></tr>
       <tr><td><%=Common.StrTable.GetStr("punchOut") %>：</td><td> <asp:TextBox ID="txtOut" runat="server" Width =245   ></asp:TextBox></td></tr>
   
   
     </table>
     <br />
   
    
<table>
    <tr><td height =40 align =center  >
        <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" OnClientClick =" return check()" />
         <input id="Reset" type="reset" value="<%=Common.StrTable.GetStr("reset") %>" />
        <asp:Button ID="btnCancel" runat="server" Text="返回" />
     
     
     </td></tr>  
     </table> 
    </form>
</body>
</html>
