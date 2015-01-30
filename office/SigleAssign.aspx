<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SigleAssign.aspx.cs" Inherits="SigleAssign" %>

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
        <asp:TextBox runat="server" ID="TextBoxUrl" Enabled="false"></asp:TextBox>
        </td>
      </tr>
       <tr>
        <th>
        URL代码:
        </th>
        <td>
        <asp:TextBox runat="server" ID="TextBoxUrlCode" Enabled="false"></asp:TextBox>
        </td>
      </tr>
       <tr>
        <th>
        URL参数
        </th>
        <td>
        <asp:TextBox runat="server" ID="TextBoxParams" Enabled="false"></asp:TextBox>
        </td>
      </tr>
       <tr>
        <th>
        模块名称
        </th>
        <td>
       <asp:TextBox runat="server" ID="TextBoxUrlName" Enabled="false"></asp:TextBox>
        </td>
      </tr>
       <tr>
        <th>
        优先级
        </th>
        <td>
          <asp:TextBox runat="server" ID="TextBoxProirotyLevel" Text="100"></asp:TextBox>
        </td>
      </tr>
       <tr>
        <th>
       状态
        </th>
        <td>
       <asp:DropDownList runat="server" ID="DropDownListStatus" >
                     <asp:ListItem Value="-1">未分配</asp:ListItem>
                     <asp:ListItem Value="1">允许</asp:ListItem>
                     <asp:ListItem Value="0">禁止</asp:ListItem>
               </asp:DropDownList>
        </td>
      </tr>
      <%--<tr>
        <td colspan="2">
          <asp:TreeView runat="server" ID="TreeView1">
             <Nodes>
               <asp:TreeNode Value="0" ShowCheckBox="true"  Text="根"></asp:TreeNode>
             </Nodes>
          </asp:TreeView>
        </td>
      </tr>    --%>
      <tr>
        <td colspan="2">
            <asp:Button runat="server" ID="ButtonSave" Text="保存" onclick="ButtonSave_Click" 
                /></td>
      </tr>  
    </table>     
    </div>
    </form>
</body>
</html>
