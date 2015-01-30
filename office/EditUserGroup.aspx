<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditUserGroup.aspx.cs" Inherits="EditUserGroup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
       权限组名称：<asp:TextBox runat="server" ID="TextBoxUserGroupName"></asp:TextBox>
  <br />  
  <asp:Button runat="server" ID="ButtonSave" Text="保存" onclick="ButtonSave_Click" />
</div>
    </form>
</body>
</html>
