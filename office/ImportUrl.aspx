<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ImportUrl.aspx.cs" Inherits="ImportUrl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    导入目录：<asp:TextBox runat="server" ID="TextBoxImportDirectory" Width="400"></asp:TextBox><br />
    网站安装目录：<asp:TextBox runat="server" ID="TextBoxWebDirectory" Width="400"></asp:TextBox>
    <br />
    默认是否需要验证：<asp:DropDownList runat="server" ID="DropDownListAuthentication" >
                     <asp:ListItem Value="1">是</asp:ListItem>
                     <asp:ListItem Value="0">否</asp:ListItem>
   </asp:DropDownList><br />
   默认是否显示：<asp:DropDownList runat="server" ID="DropDownListShow" >
                     <asp:ListItem Value="1">是</asp:ListItem>
                     <asp:ListItem Value="0">否</asp:ListItem>
               </asp:DropDownList>
               <br />
             <asp:RadioButton ID="RadioButtonAddTo" runat="server" Text="追加" Checked="true" GroupName="ImportType"/>
             <asp:RadioButton ID="RadioButtonCover" runat="server" Text="覆盖"  GroupName="ImportType"/> 
               <br />
        <asp:Button runat="server" ID="ButtonImport" Text="导入" 
            onclick="ButtonImport_Click" />
    </div>
    <div runat="server" id="tempdiv">
    </div>
    </form>
</body>
</html>
