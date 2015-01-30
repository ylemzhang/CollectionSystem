<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AddUser.aspx.cs" Inherits="AddUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table border="1" cellpadding="0" cellspacing="0" width="100%">
  <tr>
    <th>用户ID
    </th>
    <td><asp:TextBox runat="server" ID="TextBoxAccount"></asp:TextBox>
    </td>
  </tr>
  <tr>
  <th>用户名
    </th>
    <td><asp:TextBox runat="server" ID="TextBoxUserName"></asp:TextBox>
    </td>
  </tr>
  <tr>
  <th>状 态
    </th>
    <td><asp:RadioButtonList runat="server" ID="RadioButtonListBan" RepeatColumns="2"><asp:ListItem Text="正常" Selected="True" Value="1"></asp:ListItem>
    <asp:ListItem Text="禁止" Value="0"></asp:ListItem>
    </asp:RadioButtonList>
    </td>
  </tr>
  <tr>
  <th>权限组
    </th>
    <td><asp:CheckBoxList runat="server" ID="CheckBoxListUserGroups" RepeatColumns="4"></asp:CheckBoxList>
    </td>
  </tr>
  <tr>
    <td colspan="2"><asp:Button runat="server" ID="ButtonSave" Text="保存" 
            onclick="ButtonSave_Click"></asp:Button>
    </td>
  </tr>
</table>
</asp:Content>

