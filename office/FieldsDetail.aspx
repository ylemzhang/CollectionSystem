<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FieldsDetail.aspx.cs" Inherits="FieldsDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>字段信息</title>
   <link href="css/css.css" rel="stylesheet" rev="stylesheet" type="text/css" />
   <script type="text/javascript" src="javascript/jquery.js" ></script>
 <script type="text/javascript" src="javascript/common.js" ></script>
        
    <script>
    function check()
    {
        if (document.all.txtFName.value.Trim()=='')
    {
     alert(" 字段名称<%=Common.StrTable.GetStr("shallnotbenull") %>");
    return false;
    }
       return true;
    }

    </script>

</head>
<body bgcolor=#F7F7F7>
    <form id="form1" runat="server">
    <div>
    <table width =100%  >
    <colgroup>
    <col width =120 />
    <col width =130 />
    <col width =120 />
    <col  />
    
    </colgroup>
     <tr ><td height ="20px"></td></tr>
   <tr ><td  colspan =6>字段信息<hr /></td></tr>
     <tr><td>字段名称</td><td ><asp:TextBox ID="txtFName" runat="server"></asp:TextBox><span style="color:Red">*</span></td><td>字段长度</td><td ><asp:TextBox ID="txtLength" runat="server" ></asp:TextBox></td></tr>
     
    <tr id="trShow" runat=server><td>字段类型</td><td ><asp:TextBox  ID="txtType" runat="server" Enabled =false></asp:TextBox></td><td>所属组</td><td ><asp:DropDownList ID="ddlGroup" runat="server" /></td><td ><asp:CheckBox id="chkDisplay" runat =server/>是否显示</td></tr>
     <tr></tr>
    
  
    
    <tr  ><td colspan =6 height =80 align =center  >
           <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" OnClientClick =" return check()" />
           <asp:Button ID="btnDelete" runat="server" Text="删除" OnClick="btnDelete_Click" OnClientClick =" return check()" />
         <input id="Reset" type="reset" value="<%=Common.StrTable.GetStr("reset") %>" />
        <asp:Button ID="btnCancel" runat="server" Text="关闭" />
     
     </td></tr>  
    
    </table>
        
        <asp:TextBox ID="txtFieldName" runat="server" style="display:none" />
       
        
     </div>
    </form>
</body>
</html>
