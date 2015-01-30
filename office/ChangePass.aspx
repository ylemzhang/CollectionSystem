<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChangePass.aspx.cs" Inherits="ChangePass" EnableEventValidation ="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head >
    <title>Untitled Page</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
 <link href="CSS/css.css" type="text/css" rel="stylesheet" >
    <script>
      String.prototype.Trim = function()
        {
            return this.replace(/(^\s*)|(\s*$)/g, "");
        }
        
    function check()
    {

    if (document.all.txtOldPass.value.Trim()=='')
    {
 
    document.all.Msg.innerText="<%=Common.StrTable.GetStr("oldpasswordnull") %>";
    return false;
    }
    if (document.all.txtPass.value.Trim()=='')
    {
    document.all.Msg.innerText="<%=Common.StrTable.GetStr("newpasswordnull") %>";
    return false;

    }
     if (document.all.txtPass.value.Trim()!=document.all.txtConfirm.value.Trim())
    {
    document.all.Msg.innerText="<%=Common.StrTable.GetStr("twiceimputnotsame") %>";
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
<body>
    <form id="form1" runat="server">
     <div style ="height:50px"></div>
 
   <table width ="300"   bgColor="#cccccc" border =1  align ="center" >
   <tr>
   <td colspan =2 style ="height:30px" >
       <strong><%=Common.StrTable.GetStr("changepassword")%>:</strong></td>
   </tr>
    
    <tr>
    <td><%=Common.StrTable.GetStr("oldpassword")%>：</td>  <td><asp:TextBox ID="txtOldPass" runat="server" TextMode="Password" Width =150 ></asp:TextBox><span style="color:Red">*</span></td>
    </tr>
     <tr>
    <td><%=Common.StrTable.GetStr("newpassword")%>：</td>  <td><asp:TextBox ID="txtPass" runat="server" TextMode="Password" Width =150 ></asp:TextBox><span style="color:Red">*</span></td>
    </tr>
     <tr>
    <td><%=Common.StrTable.GetStr("confirmnewpassword")%>：</td>  <td><asp:TextBox ID="txtConfirm" runat="server" TextMode="Password" Width =150 ></asp:TextBox><span style="color:Red">*</span></td>
    </tr>
     <tr>
    <td></td>  <td>
        <asp:Button ID="Button1" runat="server"  OnClientClick ="return check()" OnClick="Button1_Click" Text="保存" />
        <input id="Reset1" type="reset" value="<%=Common.StrTable.GetStr("reset") %>" /></td>
       
    </tr>
      <tr>
    <td colspan =2 style="height: 20px">
        <span id=Msg runat =server style="color: #ff0000">
         </span></td>
    </tr>
    
    </table>
    
    </form>
</body>
</html>
