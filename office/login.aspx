<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login"  CodePage ="65001"  EnableEventValidation ="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head >
    <title><%=Common.StrTable.GetStr("webname") %>--<%=Common.StrTable.GetStr("login")%></title>
 <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
 <link href="CSS/css.css" type="text/css" rel="stylesheet" >
    <script>
      String.prototype.Trim = function()
        {
            return this.replace(/(^\s*)|(\s*$)/g, "");
        }
        
    function check()
    {
    
    if (document.all.txtUser.value.Trim()=='')
    {
 
    document.all.Msg.innerText='<%=Common.StrTable.GetStr("userName") %> <%=Common.StrTable.GetStr("shallnotbenull") %>'; //
    return false;
    }
    if (document.all.txtPass.value.Trim()=='')
    {
    document.all.Msg.innerText='<%=Common.StrTable.GetStr("password") %> <%=Common.StrTable.GetStr("shallnotbenull") %>'; //
    return false;

    }
    return true;
    }
   
    </script>
    
    
</head>
<body onload ="document.all.txtUser.focus()">
    <form id="form1" runat="server">
    <br/>
        <div style ="height:50px"  ></div>
 
   <table width =320   bgColor="#cccccc" border =1  align =center  >
   <tr>
   <td colspan =2 style ="height:30px; color:White" align =right bgcolor=navy   >
       <strong><font  style=" font-style:italic;  font-family:times;font-weight:bold;font-size: 120%"><%=Common.StrTable.GetStr("webname") %></font></strong></td>
   </tr>
    
    <tr>
    <td width =120><%=Common.StrTable.GetStr("userName") %>：</td>  <td><asp:TextBox ID="txtUser" runat="server" Width =150 ></asp:TextBox><span style="color:Red">*</span></td>
    </tr>
     <tr>
    <td><%=Common.StrTable.GetStr("password") %>：</td>  <td><asp:TextBox ID="txtPass" runat="server" TextMode="Password" Width =150 ></asp:TextBox><span style="color:Red">*</span></td>
    </tr>
     <tr>
    <td></td> <td>
        <asp:Button ID="btnLogin" runat="server"  OnClientClick ="return check()" OnClick="Button1_Click" />
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
