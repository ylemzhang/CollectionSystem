<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ModuleManager.aspx.cs" Inherits="ModuleManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        $(function () {
            $('#buttonImport').click(function () {
                top.$.open({
                    url: 'ImportUrl.aspx',
                    title: '导入窗口',
                    width: 600,
                    height: 200,
                    callback: function (action) {
                        if (action == 'close') {
                             $("#" + "<%=ButtonQuery.ClientID%>").click();
                        }
                    }
                });
            });

            $('#buttonadd').click(function () {
                top.$.open({
                    url: 'EditUrl.aspx',
                    title: '增加URL窗口',
                    width: 400,
                    height: 400,
                    callback: function (action) {
                        if (action == 'close') {
                              $("#" + "<%=ButtonQuery.ClientID%>").click();
                        }
                    }
                });
            });

            $('.urlStyle').click(function () {
                var jumpurl = $(this).parent().parent().find("td>:hidden").val() + "";
                jumpurl = 'EditUrl.aspx?urlGuid=' + jumpurl;
                top.$.open({
                    url: jumpurl,
                    title: '编辑URL窗口',
                    width: 400,
                    height: 400,
                    callback: function (action) {
                        if (action == 'close') {
                            $("#" + "<%=ButtonQuery.ClientID%>").click();
                        }
                    }
                });
            });

            $('.SelectParent').click(function () {
                var jumpurl = $(this).parent().parent().find("td>:hidden").val() + "";
                jumpurl = 'SelectModule.aspx?urlGuid=' + jumpurl;
                top.$.open({
                    url: jumpurl,
                    title: '选择父节点窗口',
                    width: 600,
                    height: 600,
                    id:'select',
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
<div>
  URL：<asp:TextBox runat="server" ID="TextBoxUrl"></asp:TextBox>
  名称：<asp:TextBox runat="server" ID="TextBoxUrlName"></asp:TextBox>
  是否需要验证：<asp:DropDownList runat="server" ID="DropDownListAuthentication" >
                     <asp:ListItem Value="0">请选择</asp:ListItem>
                     <asp:ListItem Value="1">是</asp:ListItem>
                     <asp:ListItem Value="2">否</asp:ListItem>
  </asp:DropDownList>
  是否显示：<asp:DropDownList runat="server" ID="DropDownListShow" >
                     <asp:ListItem Value="0">请选择</asp:ListItem>
                     <asp:ListItem Value="1">是</asp:ListItem>
                     <asp:ListItem Value="2">否</asp:ListItem>
               </asp:DropDownList>
  <asp:Button runat="server" ID="ButtonQuery" Text="查询" 
        onclick="ButtonQuery_Click" />
        <input type="button" id="buttonadd" value="增加" />
        <input type="button" id="buttonImport" value="导入" />
        </div>
<br />
<div>
  <asp:Repeater runat="server" ID="Repeater1" onitemcommand="Repeater1_ItemCommand">
  <HeaderTemplate>
    <table width="100%" cellpadding="0" cellspacing="0" border="1">
       <thead>
         <tr>
           <th style=" text-align:left;">URL</th>
           <th style=" text-align:left;">URL代码</th>
           <th style=" text-align:left;">URL参数</th>
           <th style=" text-align:left;">名 称</th>
           <th style=" text-align:left;">是否需要验证</th>
           <th style=" text-align:left;">是否显示</th>
            <th>操 作</th>
          </tr>
       </thead>  
       <tbody>  
  </HeaderTemplate>
  <ItemTemplate>
      <tr>

        <td>
        <asp:HiddenField runat="server" Value='<%#Eval("GUID")%>' ID="HiddenFieldGUID" />
        <a href="#" class="urlStyle"><%#Eval("Url")%></a>
        </td>
                <td>
        <%#Eval("UrlCode")%>
        </td>
                <td>
        <%#Eval("UrlParams")%>
        </td>
                <td>
        <%#Eval(" UrlName")%>
        </td>
        <td>
        <%#Convert.ToBoolean(Eval("UserAuthentication")) ? "是" : "否"%>
        </td>
        <td>
        <%#Convert.ToBoolean(Eval("Show")) ? "是" : "否"%>
        </td>
        <td>
          <asp:LinkButton runat="server" ID="LinkButtonDelete" Text="删除" OnClientClick="javascript:return window.confirm('确实要删除？');" CommandName="delete" CommandArgument='<%#Eval("GUID")%>'></asp:LinkButton>
           <a href="#" class="SelectParent">选择父节点</a>
        </td>
      </tr>
  </ItemTemplate>
  <FooterTemplate>
  </tbody>
  </table>  
  </FooterTemplate>
</asp:Repeater>
</div>
</asp:Content>

