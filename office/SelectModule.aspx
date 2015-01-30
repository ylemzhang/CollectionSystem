<%@ Page Language="C#"  MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SelectModule.aspx.cs" Inherits="SelectModule" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript">
    $(function () {
        $('.select :radio').bind("click", function () {
            var isChecked = $(this).attr("checked");
            $('.select :radio').attr("checked", !isChecked);
            $(this).attr("checked", isChecked);
        });
    });
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div>
  URL：<asp:TextBox runat="server" ID="TextBoxUrl"></asp:TextBox>
  名称：<asp:TextBox runat="server" ID="TextBoxUrlName"></asp:TextBox>
  <asp:Button runat="server" ID="ButtonQuery" Text="查询"  OnClick="ButtonQuery_Click"
        />
          <asp:Button runat="server" ID="ButtonSave" Text="确定"  OnClick="ButtonSave_Click"
        />
        </div>
<br />
<div>
  <asp:Repeater runat="server" ID="Repeater1" 
        onitemdatabound="Repeater1_ItemDataBound">
  <HeaderTemplate>
    <table width="100%" cellpadding="0" cellspacing="0" border="1">
       <thead>
         <tr>
           <th style=" text-align:left;">选择</th>
           <th style=" text-align:left;">URL</th>
           <th style=" text-align:left;">URL参数</th>
           <th style=" text-align:left;">名 称</th>
          </tr>
       </thead>  
       <tbody>  
  </HeaderTemplate>
  <ItemTemplate>
      <tr>
       <td>
        <asp:HiddenField runat="server" Value='<%#Eval("GUID")%>' ID="HiddenFieldGUID" />
        <%--<asp:CheckBox runat="server" Text="" ID="CheckBoxSelect" />--%>
        <asp:RadioButton runat="server" ID="RadioButtonSelect" CssClass="select"/>
        </td>
       <td>
        <%#Eval("Url")%>
        </td>
       <td>
        <%#Eval("UrlParams")%>
        </td>
                <td>
        <%#Eval(" UrlName")%>
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

