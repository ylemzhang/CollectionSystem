<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportPerformance.aspx.cs" Inherits="ReportPerformance" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>催收人员业绩统计表</title>
     <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <link href="CSS/css.css" type="text/css" rel="stylesheet" >
    <script type="text/javascript" src="javascript/jquery.js" ></script>
       <script type="text/javascript" src="javascript/common.js" ></script>
</head>
<body>
    <form id="form1" runat="server">
    <table width =100%   bgColor="#F7F7F7" border =1  align =center  >
     <tr>
     <td style='border-style:outset ; border-width:1px;'><table width=100%><tr> 
    
    <td  align =center valign =middle><h2>催收人员业绩统计表</h2></td></tr></table></td>
    
    </tr>
    <tr>
    <td>
      <table>
   
  
 
         <tr>
   <td>公司:<asp:DropDownList ID="ddlCompany" runat="server" AutoPostBack =true OnSelectedIndexChanged ="ddlCompany_SelectedIndexChanged" >
      </asp:DropDownList></td>
  <td> 批次:<asp:DropDownList ID="ddlPatch" runat="server" > </asp:DropDownList></td>
  <td> 组:<asp:DropDownList ID="ddlGroups" runat="server" > </asp:DropDownList></td>
   
    <td >从:<asp:TextBox ID="txtFrom" runat="server" Width =70></asp:TextBox><img src="Images/Calendar.gif"  style=" cursor:hand "  onclick ="ShowCalenderWindow('txtFrom')"/>&nbsp;&nbsp;
                        到:&nbsp;<asp:TextBox ID="txtTo" runat="server" Width =70></asp:TextBox><img src="Images/Calendar.gif" style=" cursor:hand " onclick ="ShowCalenderWindow('txtTo')"/>
        &nbsp;&nbsp;<asp:Button ID="btn" runat="server" Text="Go" OnClick="btnSearch_Click" /></td><td   align =right >  <span id='spanleave' style =" cursor:hand; color:Blue"  onclick ="window.location.href='ReportManagement.aspx'"  >[返回]</span>&nbsp;<asp:LinkButton runat =server ForeColor="blue" ID="lnkExcel" OnClick="lnkExcel_Click"  Visible =false>[导出到Excel]</asp:LinkButton></td>
                           </tr>
  
     
  
    </table>
<%=ResutlHtmlForPage%>
  

   </td>
   </tr>
   </table>
    </form>
</body>
</html>

  