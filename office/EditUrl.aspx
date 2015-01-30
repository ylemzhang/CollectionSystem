<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditUrl.aspx.cs" Inherits="EditUrl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table cellpadding="0" cellspacing="0" border="1">
      <tr>
        <th>
        URL
        </th>
        <td>
        <asp:TextBox runat="server" ID="TextBoxUrl"></asp:TextBox>
        </td>
      </tr>
       <tr>
        <th>
        URL代码:
        </th>
        <td>
        <asp:TextBox runat="server" ID="TextBoxUrlCode"></asp:TextBox>
        </td>
      </tr>
       <tr>
        <th>
        URL参数
        </th>
        <td>
        <asp:TextBox runat="server" ID="TextBoxParams"></asp:TextBox>
        </td>
      </tr>
       <tr>
        <th>
        功能名称
        </th>
        <td>
       <asp:TextBox runat="server" ID="TextBoxUrlName"></asp:TextBox>
        </td>
      </tr>
       <tr>
        <th>
        是否需要验证
        </th>
        <td>
        <asp:DropDownList runat="server" ID="DropDownListAuthentication" >
                     <asp:ListItem Value="0">是</asp:ListItem>
                     <asp:ListItem Value="1">否</asp:ListItem>
  </asp:DropDownList>
        </td>
      </tr>
       <tr>
        <th>
       是否显示
        </th>
        <td>
       <asp:DropDownList runat="server" ID="DropDownListShow" >
                     <asp:ListItem Value="0">是</asp:ListItem>
                     <asp:ListItem Value="1">否</asp:ListItem>
               </asp:DropDownList>
        </td>
      </tr>    
      <tr>
        <td colspan="2"><asp:Button runat="server" ID="ButtonSave" Text="保存" 
                onclick="ButtonSave_Click"/></td>
      </tr>  
    </table>     
        </div>
    </form>
</body>
</html>
