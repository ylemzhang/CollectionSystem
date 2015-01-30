<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AssignPermission.aspx.cs" Inherits="AssignPermission" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript">
    $(function () {
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

          $('.setPermission').click(function () {
              var jumpurl = $(this).parent().parent().find("td>:hidden").val() + "";
            jumpurl = 'SigleAssign.aspx?urlGuid=' + jumpurl + '&userGroupGuid=' + $("#" + '<%=HiddenFieldGroupGUID.ClientID%>').val();
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
    });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div>
  用户组：<%--<asp:Label runat="server" ID="LabelGroupName"></asp:Label>--%>
  <asp:DropDownList runat="server" ID="DropDownListGroupName" 
        onselectedindexchanged="DropDownListGroupName_SelectedIndexChanged" AutoPostBack="true">
  </asp:DropDownList>
  状态：<asp:DropDownList runat="server" ID="DropDownListStatus">
            <asp:ListItem Value="-2">请选择</asp:ListItem> 
           <asp:ListItem Value="-1">未分配</asp:ListItem>
           <asp:ListItem Value="1">允许</asp:ListItem>
           <asp:ListItem Value="0">禁止</asp:ListItem>
        </asp:DropDownList>
      <asp:Button runat="server" ID="ButtonQuery" Text="查 询" 
        onclick="ButtonSave_Click" />
  <hr />
</div>
<div>
<asp:HiddenField runat="server" Value="0" ID="HiddenFieldGroupGUID" />
<div>
  导航：<a href="UserGroupManager.aspx" id="root">返回</a><div runat="server" id="divSiteNav"></div>
</div>
<asp:Repeater runat="server" ID="Repeater1" onitemcommand="Repeater1_ItemCommand" 
        onitemdatabound="Repeater1_ItemDataBound">
  <HeaderTemplate>
    <table width="100%" cellpadding="0" cellspacing="0" border="1">
       <thead>
         <tr>
         <th style=" text-align:left;">名 称</th>
         <th style=" text-align:left;">URL 代码</th>
         <th style=" text-align:left;">URL 参数</th>
         <th style=" text-align:left;">URL</th>
           <th style=" text-align:left;">优先级</th>
           <th style=" text-align:left;">状 态</th>
           <th style=" text-align:left;">操 作</th>
          </tr>
       </thead>  
       <tbody>  
  </HeaderTemplate>
  <ItemTemplate>
      <tr>
        <td>
             <asp:HiddenField runat="server" Value='<%#Eval("Url_GUID")%>' ID="HiddenFieldGUID" />
             <asp:Label runat="server" Text='<%#Eval("UrlName")%>' ID="LabelUrlName"></asp:Label> 
        </td>
        <td>
         <%#Eval("UrlCode")%>
        </td>
        <td>
         <%#Eval(" UrlParams")%>
        </td>
        <td>
          <a href="#" class="urlStyle"><%#Eval("Url")%></a>
        </td>
         <td>
         <%#Eval("PriorityLevel") == null ? "0" : Eval("PriorityLevel").ToString()%>
        </td>
        <td>
          <%#ChangeForbiddenToString(Eval("Forbidden").ToString())%>
        </td>
        <td>
           <a class="setPermission" href="#">设置</a>
           <asp:LinkButton runat="server" ID="LinkButtonNext" CommandName="next" CommandArgument='<%#Eval("Url_GUID")%>' Text="下一级"></asp:LinkButton>
           <asp:LinkButton runat="server" ID="LinkButtonPrev" CommandName="prev" CommandArgument='<%#Eval("Url_GUID")%>' Text="上一级"></asp:LinkButton>
        </td>
      </tr>
  </ItemTemplate>
  <FooterTemplate>
  </tbody>
  </table>  
  </FooterTemplate>
</asp:Repeater>
<%--<div>
   <asp:TreeView runat="server" ID="TreeView1">
             <Nodes>
               <asp:TreeNode Value="0" ShowCheckBox="true"  Text="根" Checked="true"></asp:TreeNode>
             </Nodes>
    </asp:TreeView>
</div>--%>
</div>
</asp:Content>

