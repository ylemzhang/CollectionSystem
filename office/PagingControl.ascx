<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PagingControl.ascx.cs" Inherits="PagingControl" %>
 <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
    <link href="CSS/css.css" type="text/css" rel="stylesheet">
<table height =20 style="width: 390px"> 
<tr valign =bottom ><td> <%=Common.StrTable.GetStr("totalPages")%>：<%=TotalPage %>&nbsp;&nbsp;<asp:LinkButton  ID="lkFistPage" runat="server" style=" color :Blue " OnClick="lkFistPage_Click">[<%=Common.StrTable.GetStr("first")%>]</asp:LinkButton><asp:LinkButton  ID="lkPrepage" style=" color :Blue " runat="server" OnClick="lkPrepage_Click">[<%=Common.StrTable.GetStr("previous")%>]</asp:LinkButton> &nbsp;&nbsp;<asp:TextBox
     Width =40  Text ="1"  ID="txtCurrentPage" runat="server"> </asp:TextBox><asp:Button ID="btnGo" runat="server" Text="Go" OnClick="btnGo_Click"></asp:Button>&nbsp;&nbsp;<asp:LinkButton  ID="lkNextpage" runat="server" style=" color :Blue " OnClick="lkNextpage_Click">[<%=Common.StrTable.GetStr("next")%>]</asp:LinkButton><asp:LinkButton  ID="lkLast" runat="server" style=" color :Blue " OnClick="lkLast_Click">[<%=Common.StrTable.GetStr("last")%>]</asp:LinkButton></td></tr>
</table>
