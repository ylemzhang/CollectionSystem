<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="UserGroupManager.aspx.cs" Inherits="UserGroupManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        $(function () {           

            $('#buttonadd').click(function () {
                top.$.open({
                    url: 'EditUserGroup.aspx',
                    title: '增加用户组窗口',
                    width: 400,
                    height: 300,
                    callback: function (action) {
                        if (action == 'close') {
                            $("#" + "<%=ButtonQuery.ClientID%>").click();
                        }
                    }
                });
            });

            $('.urlStyle').click(function () {
                var jumpurl = $(this).parent().parent().find("td>:hidden").val() + "";
                jumpurl = 'EditUserGroup.aspx?userGroupGuid=' + jumpurl;
                top.$.open({
                    url: jumpurl,
                    title: '编辑URL窗口',
                    id: 'fff',
                    width: 400,
                    height: 300,
                    closeType: 'hide',
                    callback: function (action) {
                        if (action == 'close') {
                            $("#" + "<%=ButtonQuery.ClientID%>").click();
                        }
                    }
                });
            });
        });
      </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:scriptManager ID="scriptManager1" runat="server" EnablePageMethods="True" /> 
<div>
  名称：<asp:TextBox runat="server" ID="TextBoxUserGroupName"></asp:TextBox>
  <asp:Button runat="server" ID="ButtonQuery" Text="查询" 
        onclick="ButtonQuery_Click" />
        <input type="button" id="buttonadd" value="增加" />
</div>

    <asp:Repeater runat="server" ID="Repeater1" 
        onitemcommand="Repeater1_ItemCommand">
  <HeaderTemplate>
    <table  width="100%" cellpadding="0" cellspacing="0" border="1">
       <thead>
         <tr>
           <th style=" text-align:left;">权限组名称</th>
           <th style=" text-align:left;">操  作</th>
          </tr>
       </thead>  
       <tbody>  
  </HeaderTemplate>
  <ItemTemplate>
      <tr>
        <td>
        <asp:HiddenField ID="HiddenFieldGUID" runat="server" Value='<%#Eval("GUID")%>'/>
        <a href="#" class="urlStyle"><%#Eval("UserGroupName")%></a>
        </td>
        <td>
          <asp:LinkButton runat="server" ID="LinkButtonDelete" Text="删除" OnClientClick="javascript:return window.confirm('确实要删除？');" CommandName="delete" CommandArgument='<%#Eval("GUID")%>' ></asp:LinkButton>
          <asp:LinkButton runat="server" ID="LinkButtonAssignPermission" Text="分配权限" CommandName="assign"  CommandArgument='<%#Eval("GUID")%>' ></asp:LinkButton>
          <asp:LinkButton runat="server" ID="LinkButtonUserManager" Text="用户管理" CommandName="usermanager"  CommandArgument='<%#Eval("GUID")%>' ></asp:LinkButton>
        </td>
      </tr>
  </ItemTemplate>
  <FooterTemplate>
  </tbody>
  </table>
  </FooterTemplate>
</asp:Repeater>
</asp:Content>

