<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PatchEdit.aspx.cs" Inherits="PatchEdit" %>

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
    
    if (document.all.txtPatchName.value.Trim()=='')
    {
     alert(" 批次名称<%=Common.StrTable.GetStr("shallnotbenull") %>");
    return false;
    }
    
    if (document.all.txtDate.value.Trim()=='')
    {
     alert(" 过期时间<%=Common.StrTable.GetStr("shallnotbenull") %>");
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

<body  >
    <form id="form1" runat="server" >
    <table ><tr><td height=5></td></tr></table>

     <table   cellspacing =0 >
      <colgroup>
    <col width =60 />
    <col width =240 />
 
    </colgroup>
   <tr>
   <td>批次名称:</td><td><asp:TextBox ID="txtPatchName" runat="server" Width =200   ></asp:TextBox><span style="color:Red">*</span></td>
   </tr>
   <tr>
   <td> 过期时间:</td><td>
                                                                      <asp:TextBox ID="txtDate" runat="server" Width =100></asp:TextBox><span style="color:Red">*</span><img src="Images/Calendar.gif"  style=" cursor:hand "  onclick ="ShowCalenderWindow('txtDate')"/></td>
                                                                     
   </tr>
   <tr>
    <td> 导入时间:</td><td>
                                                                      <asp:TextBox ID="txtImportDate" runat="server" Width =100 ReadOnly ></asp:TextBox></td>
   </tr>
   <tr>
  <td height =30 align =center colspan =2  >
        <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" OnClientClick =" return check()" />
         <input id="Reset" type="reset" value="<%=Common.StrTable.GetStr("reset") %>" />
          <input id="Reset1" type=button   value="返回"  onclick ="window.location.href='Patchmanagemnt.aspx?id='+<%=CompanyID %>"/>
      
   </tr>
      
   
   
     </table>

     
      
  
     
    </form>
</body>
</html>
