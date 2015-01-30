<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CompanyEdit.aspx.cs" Inherits="CompanyEdit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head >
    <title>Untitled Page</title>
     <link href="CSS/css.css" type="text/css" rel="stylesheet">
      <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
      <script type="text/javascript" src="javascript/jquery.js" ></script>
      <script type="text/javascript" src="javascript/common.js" ></script>
     <script>
 
    function check()
    {
    
    if (document.all.txtCompanyName.value.Trim()=='')
    {
     alert(" 公司名 <%=Common.StrTable.GetStr("shallnotbenull") %>");
    return false;
    }
    var id='<%=CompanyID%>';
  
    if(id=='')
    {
    return confirm("请注意 是否要余额表,你真要保存吗？");
    }
    return true;

    }
    
   
   
   function refreshPage()
{

   document.location.href=document.location.href;
       
}


 </script>
</head>

<body  >
    <form id="form1" runat="server" >
    <table ><tr><td height=5></td></tr></table>

     <table   cellspacing =0 >
      <colgroup>
    <col width =100 />
    <col width =200 />
 
    </colgroup>
   <tr>
   <td><%=Common.StrTable.GetStr("company") %>：</td><td><asp:TextBox ID="txtCompanyName" runat="server" Width =200   ></asp:TextBox><span style="color:Red">*</span></td>
   </tr>
   <tr>
   <td><%=Common.StrTable.GetStr("description") %>：</td><td> <asp:TextBox ID="txtDescription" runat="server" Width =200   /></td>
   </tr>
   <tr>
   <td>是否有余额表：</td><td> <asp:CheckBox runat =server ID="chkBalance" /></td>
   </tr>
   <tr>
  <td height =30 align =center colspan =2  >
        <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" OnClientClick =" return check()" />
         <input id="Reset" type="reset" value="<%=Common.StrTable.GetStr("reset") %>" />
      
   </tr>
      
   
   
     </table>

     
      
  
     
    </form>
</body>
</html>
