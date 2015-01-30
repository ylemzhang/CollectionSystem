<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserProfile.aspx.cs" Inherits="UserProfile" EnableEventValidation ="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head >
    <title>Untitled Page</title>
     <link href="CSS/css.css" type="text/css" rel="stylesheet">
      <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
     <script>
       function refreshPage()
{

   document.location.href=document.location.href;
       
}
   String.prototype.Trim = function()
        {
            return this.replace(/(^\s*)|(\s*$)/g, "");
        }
    function check()
    {
    
  
    if (document.all.txtPageCount.value.Trim()=='')
    {
    alert("<%=Common.StrTable.GetStr("recordperpagenull") %>");
    return false;
    }
     if (document.all.txtRealName.value.Trim()=='')
    {
    alert("<%=Common.StrTable.GetStr("realName") %> <%=Common.StrTable.GetStr("shallnotbenull") %>");
    return false;
    }
    
     if (document.all.txtColumn.value.Trim()=='')
    {
    alert("请在 案件详细信息栏目数中输入1到10的数字");
    return false;
    }
    
    
      if (document.all.txtPromisdate.value.Trim()=='')
    {
    alert("请在 案件详细信息栏目数中输入1到30的数字");
    return false;
    }
    return true;
    }
    
   
 </script>
</head>
<body bgcolor=#F7F7F7 style=" margin-left:5px">
    <form id="form1" runat="server" >
    <table ><tr><td height=5></td></tr></table>
  
    <b><%=Common.StrTable.GetStr("generalInformation")%></b><hr />
     <table width=380   cellspacing =0 >
      <colgroup>
    <col width =100 />
    <col width =250 />
  
   
    </colgroup>
    <tr><td style="height: 20px"></td></tr>
     <tr><td><%=Common.StrTable.GetStr("realName") %>：</td><td> <asp:TextBox ID="txtRealName" runat="server"  Width =245  ></asp:TextBox><span style="color:Red">*</span></td></tr>
     
     <tr><td><%=Common.StrTable.GetStr("gender") %>：</td><td>  <asp:DropDownList ID="ddlGender" runat="server" Width =100>
          </asp:DropDownList></td></tr>
     <tr><td><%=Common.StrTable.GetStr("phone") %>：</td><td> <asp:TextBox ID="txtPhone" runat="server"  Width =245  ></asp:TextBox></td></tr>
     <tr><td><%=Common.StrTable.GetStr("mobile")%>：</td><td> <asp:TextBox ID="txtMobile" runat="server"  Width =245  ></asp:TextBox></td></tr>
     <tr><td>Email：</td><td> <asp:TextBox ID="txtEmail" runat="server"  Width =245  ></asp:TextBox></td></tr>
   
     </table>
     <br />
     
     <asp:RadioButtonList 
 <b><%=Common.StrTable.GetStr("customizationinfo")%></b><hr />
     <table width=380   cellspacing =0 >
      <colgroup>
    <col width =100 />
    <col width =250 />
  
   
    </colgroup>
    <tr><td style="height: 20px"></td></tr>
     <tr><td><%=Common.StrTable.GetStr("recordperpage")%>：</td><td> <asp:TextBox ID="txtPageCount" runat="server"   Width =60  ></asp:TextBox>
       (请输入5到10000的数字)</td></tr>
         <tr>
         <td>案件详细信息 显示栏目数：</td><td> <asp:TextBox ID="txtColumn" runat="server"   Width =60  ></asp:TextBox>
       (请输入1到10的数字)
         </td></tr>
     
    <tr>
         <td>承诺还款日期到期提醒 提前几天：</td><td> <asp:TextBox ID="txtPromisdate" runat="server"   Width =60  ></asp:TextBox>
       (请输入1到30的数字)
         </td></tr>
   
     </table>
     <br />

    
<table>
    <tr><td height =40 align =center  >
        <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" OnClientClick =" return check()" />
         <input id="Reset" type="reset" value="<%=Common.StrTable.GetStr("reset")%>" />
       
     
     </td></tr>  
     </table> 
    </form>
</body>
</html>
