<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CaseCategoryEdit.aspx.cs" Inherits="CaseCategoryEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>案件分类信息</title>
   <link href="css/css.css" rel="stylesheet" rev="stylesheet" type="text/css" />
   <script type="text/javascript" src="javascript/jquery.js" ></script>
 <script type="text/javascript" src="javascript/common.js" ></script>
        
    <script>
    function check()
    {
        if (document.all.txtFName.value.Trim()=='')
    {
     alert(" 案件分类名称不能为空");
    return false;
    }
       return true;
    }

    </script>
  
</head>
<body bgcolor=#F7F7F7>
    <form id="form1" runat="server">
    <div align =center >
    <table width =400  border =1>
   
    
   <tr ><td  colspan =6>案件分类信息</td></tr>
     <tr><td>案件分类名称</td><td ><asp:TextBox ID="txtFName" runat="server"></asp:TextBox><span style="color:Red">*</span></td><td ><asp:CheckBox id="chkDisplay" runat =server Checked />是否显示</td></tr>
    
  
    
    <tr  ><td colspan =6 height =50 align =center  >
           <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" OnClientClick =" return check()" />
         <input id="Reset" type="reset" value="重置" />
        <asp:Button ID="btnCancel" runat="server" Text="返回" />
     
     </td></tr>  
    
    </table>
        
        
       
        
     </div>
    </form>
</body>
</html>
