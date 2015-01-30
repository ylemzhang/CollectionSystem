<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportApply.aspx.cs" Inherits="ReportApply" %>






<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head >
    <title>Untitled Page</title>
    <link href="CSS/css.css" type="text/css" rel="stylesheet">
     <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
     <script type="text/javascript" src="javascript/jquery.js" ></script>
     <script type="text/javascript" src="javascript/common.js" ></script>
     
     <script>
     
    
   
   function openFields()
   {
   
    OpenWindow("ExcepFields.aspx?type=1&CompanyID=<%=CompanyID %>","700","800");

   }
     </script>
</head>
<body >
    <form id="form1" runat="server">
 
    <table width =100% cellspacing =0>
   <tr bgcolor=#F7F7F7><td>双击可以看Email内容</td></tr>
    <tr bgcolor=#F7F7F7><td> 公司：<asp:DropDownList ID="ddlCompany" runat="server"  >
      </asp:DropDownList>&nbsp;从：&nbsp;<asp:TextBox ID="txtFrom" runat="server" Width =70></asp:TextBox><img src="Images/Calendar.gif"  style=" cursor:hand "  onclick ="ShowCalenderWindow('txtFrom')"/>&nbsp;&nbsp;
                        到：&nbsp;<asp:TextBox ID="txtTo" runat="server" Width =70></asp:TextBox><img src="Images/Calendar.gif" style=" cursor:hand " onclick ="ShowCalenderWindow('txtTo')"/>&nbsp;&nbsp;
        &nbsp;&nbsp;<asp:Button ID="btn" runat="server" Text="Go" OnClick="btn_Click" /><span id='spanleave' style =" cursor:hand; color:Blue"  onclick ="window.location.href='ReportManagement.aspx'"  >[返回]</span>&nbsp;&nbsp;记录总数：<%=TotalRecord%></td><td align =right valign =top class="menu" >
                          <span id="spanExcel" runat =server style ="cursor:hand; color:blue" onclick="openFields()">导出到Excel</span> </td></tr>
                            
                            <tr>
                            <td  colspan=2 >
                             <asp:GridView  Width =100% ID="GridView1" runat="server"  OnRowDataBound="GridView1_RowDataBound"
        AlternatingRowStyle-CssClass="dg_alter" RowStyle-CssClass="dg_item" HeaderStyle-CssClass="dg_header"  AutoGenerateColumns =false    >
               <Columns>
         
           
             
              
              <asp:BoundField DataField="Sender" HeaderText ="申请者" ItemStyle-Width=70/>
              <asp:BoundField DataField="Recipient" HeaderText ="接受申请者" ItemStyle-Width=70/>
              <asp:BoundField DataField="Title" HeaderText ="申请类型" ItemStyle-Width=120/>
             
               <asp:BoundField DataField="SentOn" HeaderText ="申请日期"  ItemStyle-Width=120  />
                <asp:BoundField DataField="" HeaderText ="客户" ItemStyle-Width=70/>
                 <asp:BoundField DataField="" HeaderText ="账号" ItemStyle-Width=120/>
            <asp:BoundField DataField="Body" HeaderText ="内容" />
             <asp:BoundField DataField="ID" HeaderText ="内容" />
</Columns>
        </asp:GridView>
                            </td>
                            </tr>
                          
    </table>
   
       

   
    </form>
</body>
</html>