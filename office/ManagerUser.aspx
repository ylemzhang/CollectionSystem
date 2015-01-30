<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ManagerUser.aspx.cs" Inherits="ManagerUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        $(function () {
            $(document).ready(function () {
                $(".CheckBoxAll").find(":checkbox").bind({
                    click: function () {
                        $("table").find(".CheckBoxItem>:checkbox").attr('checked', this.checked);
                    }
                });

                $('#buttonadd').bind("click",function () {
                    top.$.open({
                        url: 'AddUser.aspx',
                        title: '增加用户窗口',
                        width: 400,
                        height: 300,
                        callback: function (action) {
                            if (action == 'close') {
                                $("#" + "<%=ButtonQuery.ClientID%>").click();
                            }
                        }
                    });
                });

                $('.urlStyle').bind("click", function () {
                    var jumpurl = $(this).parent().parent().find("td>:hidden").val() + "";
                    jumpurl = 'AddUser.aspx?userGuid=' + jumpurl;
                    top.$.open({
                        url: jumpurl,
                        title: '编辑用户窗口',
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
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>导航：
   帐号：<asp:TextBox runat="server" ID="TextBoxAccount"></asp:TextBox>
   用户名：<asp:TextBox runat="server" ID="TextBoxUserName"></asp:TextBox>
   <asp:RadioButton runat="server" ID="RadioButtonIsTrue"  GroupName="BelongTo" Text="属于" Checked="true" OnCheckedChanged="RadioButton_OnCheckedChanged" AutoPostBack="true"/> 
   <asp:RadioButton runat="server" ID="RadioButtonIsFalse" GroupName="BelongTo" Text="不属于" OnCheckedChanged="RadioButton_OnCheckedChanged" AutoPostBack="true"/> 
   权限组：<asp:DropDownList runat="server" ID="DropDownListGroup" 
            onselectedindexchanged="DropDownListGroup_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
   状态:<asp:DropDownList runat="server" ID="DropDownListBan">
      <asp:ListItem Value="0">请选择</asp:ListItem>
      <asp:ListItem Value="1">正常</asp:ListItem>
      <asp:ListItem Value="2">禁止</asp:ListItem>
   </asp:DropDownList>
   <asp:Button runat="server" ID="ButtonQuery" Text="查询" 
        onclick="ButtonQuery_Click" />
   <input type="button" id="buttonadd" value="增加" />
   <hr />
   导航：<a href="UserGroupManager.aspx" id="root">返回</a>
</div>
<div>
  <asp:Repeater runat="server" ID="Repeater1" onitemcommand="Repeater1_ItemCommand">
  <HeaderTemplate>
    <table width="100%" cellpadding="0" cellspacing="0" border="1">
       <thead>
         <tr>
          <th>选择</th>
           <th style=" text-align:left;">帐 号</th>          
          <th style=" text-align:left;">用户名称</th>
           <th style=" text-align:left;">状 态</th>
           <th style=" text-align:left;">操作</th>
          </tr>
       </thead>  
       <tbody>  
  </HeaderTemplate>
  <ItemTemplate>
      <tr>
      <td>
         <asp:HiddenField runat="server" Value='<%#Eval("GUID")%>' ID="HiddenFieldGUID" />
         <asp:CheckBox runat="server" ID="CheckBoxItemID" CssClass="CheckBoxItem" />
        </td>
        <td>
          <a href="#" class="urlStyle"><%#Eval("Account")%></a>
        </td>
        <td>
        <%#Eval("UserName")%>
        </td>
        <td>
        <%#Convert.ToBoolean(Eval("Ban")) ? "禁止" : "正常"%>
        </td>
        <td>
          <asp:LinkButton runat="server" ID="LinkButtonAction" CommandName="action" CommandArgument='<%#Eval("GUID")+","+Eval("Ban")%>' Text='<%#Convert.ToBoolean(Eval("Ban"))?"恢复":"禁止"%>'></asp:LinkButton>
          <asp:LinkButton runat="server" ID="LinkButtonDelete" CommandName="delete" CommandArgument='<%#Eval("GUID")%>' Text="删除"></asp:LinkButton>
        </td>
      </tr>
  </ItemTemplate>
  <FooterTemplate>
  </tbody>
  </table>  
  </FooterTemplate>
</asp:Repeater>
<div>
    <asp:CheckBox runat="server" ID="CheckBoxAll" Text="全选" CssClass="CheckBoxAll" />
     <asp:Button runat="server" ID="ButtonAddToGroup" Text="从权限组中删除" 
         onclick="ButtonAddToGroup_Click" />
</div>
</div>
</asp:Content>

