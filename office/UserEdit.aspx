<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserEdit.aspx.cs" Inherits="UserEdit"   ValidateRequest="false"%>
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
    
    if (document.all.txtUserName.value.Trim()=='')
    {
     alert("<%=Common.StrTable.GetStr("userName") %> <%=Common.StrTable.GetStr("shallnotbenull") %>");
    return false;
    }
    if (document.all.txtPassword.value.Trim()=='')
    {
    alert("<%=Common.StrTable.GetStr("password") %> <%=Common.StrTable.GetStr("shallnotbenull") %>");
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
   <b><%=Common.StrTable.GetStr("loginInfo") %>：</b><hr />
     <table width=380   cellspacing =0 >
      <colgroup>
    <col width =100 />
    <col width =250 />
  
   
    </colgroup>
    <tr><td></td></tr>
     <tr><td><%=Common.StrTable.GetStr("userName") %>：</td><td> <asp:TextBox ID="txtUserName" runat="server"  Width =245  ></asp:TextBox><span style="color:Red">*</span></td></tr>
      <tr><td><%=Common.StrTable.GetStr("password") %>：</td><td> <asp:TextBox ID="txtPassword" runat="server" Width =245  TextMode="Password" ></asp:TextBox><span style="color:Red">*</span></td></tr>
     <%-- <tr><td><%=Common.StrTable.GetStr("role") %>：</td><td> 
          <asp:DropDownList ID="ddlRole" runat="server" Width =250  onchange="showList()">
          </asp:DropDownList><span style="color:Red">*</span></td></tr>--%>
     </table>
     <br />
   
  
       <b>角色：<asp:Label runat =server ID=lblRole></asp:Label></b>
       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>有权限的公司:</b><hr />
            <div runat =server id=divList></div>
<br />
    <b><%=Common.StrTable.GetStr("generalInformation") %></b><hr />
     <table width=380   cellspacing =0 >
      <colgroup>
    <col width =100 />
    <col width =250 />
  
   
    </colgroup>
    <tr><td style="height: 20px"></td></tr>
     <tr><td><%=Common.StrTable.GetStr("realName") %>：</td><td> <asp:TextBox ID="txtRealName" runat="server"  Width =245  ></asp:TextBox></td></tr>
     
     <tr><td><%=Common.StrTable.GetStr("gender") %>：</td><td>  <asp:DropDownList ID="ddlGender" runat="server" Width =100>
          </asp:DropDownList></td></tr>
     <tr><td><%=Common.StrTable.GetStr("phone") %>：</td><td> <asp:TextBox ID="txtPhone" runat="server"  Width =245  ></asp:TextBox></td></tr>
     <tr><td><%=Common.StrTable.GetStr("mobile") %>：</td><td> <asp:TextBox ID="txtMobile" runat="server"  Width =245  ></asp:TextBox></td></tr>
     <tr><td>Email：</td><td> <asp:TextBox ID="txtEmail" runat="server"  Width =245  ></asp:TextBox></td></tr>
   
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
