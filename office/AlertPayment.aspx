<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AlertPayment.aspx.cs" Inherits="AlertPayment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
      <link href="CSS/css.css" type="text/css" rel="stylesheet">
      <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
      <script type="text/javascript" src="javascript/jquery.js" ></script>
      <script type="text/javascript" src="javascript/common.js" ></script>
      
      

</head>
 

<body   >
    <form id="form1" runat="server">
     <table width =100%  cellspacing=0   >
                     <tr height =10 bgcolor=#F7F7F7> <td align =left valign =top class="menu" >
                      有还款案件
                     </td><td>记录总数:<%=TotalRecord%></td></tr>
                         </table>
                         
                         <div id=divSearch runat=server style ="display:none" >
                          <table width =100%>
  <tr>
  <td width =120>公司<asp:DropDownList ID="ddlCompany" runat="server" Height =19px Width =85px AutoPostBack =true OnSelectedIndexChanged ="ddlCompany_SelectedIndexChanged" >
      </asp:DropDownList></td>
 
<td width =120>批次<asp:DropDownList ID="ddlPatch" runat="server" Height =19px Width =85px  > </asp:DropDownList></td>
 
  <td width =200>还款金额<asp:TextBox ID="txtFrom" runat="server" Width =40 Height =15px></asp:TextBox>到<asp:TextBox ID="txtTo" Width =40 runat="server" Height =15px></asp:TextBox>
      </td>
      <td width =320>导入日期<asp:TextBox ID="txtDateFrom" runat="server" Width =100></asp:TextBox><img src="Images/Calendar.gif"  style=" cursor:hand "  onclick ="ShowCalenderWindow('txtDateFrom')"/>到<asp:TextBox ID="txtDateTo" runat="server" Width =100></asp:TextBox><img src="Images/Calendar.gif"  style=" cursor:hand "  onclick ="ShowCalenderWindow('txtDateTo')"/></td>
      <td align =left>
          <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">Go</asp:LinkButton></td>
  </tr>
  </table>
                         </div>
    
    <div>
     <asp:GridView  Width =100% ID="GridView1" runat="server" OnRowDataBound="GridView1_RowDataBound" 
        HeaderStyle-CssClass="dg_header"  AutoGenerateColumns =false   >
             
         <Columns>
          <asp:BoundField DataField="OwnerID" HeaderText ="业务员" ItemStyle-Width=80/>
             <asp:BoundField DataField="CompanyID" HeaderText ="公司" ItemStyle-Width=80/>
     
             <asp:BoundField DataField="tbName" HeaderText ="姓名" ItemStyle-Width=80/>
            
           
              <asp:BoundField DataField="PatchID" HeaderText ="批号" />
              <asp:BoundField DataField="tbKey" HeaderText ="帐号/合同号" />
              <asp:BoundField DataField="shouBie" HeaderText ="手别" /> 
               <asp:BoundField DataField="tbPayment" HeaderText ="还款金额" />
                 <asp:BoundField DataField="tbPayDate" HeaderText ="还款日期"  ItemStyle-Width=80/>
 <asp:BoundField DataField="tbBalance" HeaderText ="现时金额" />

                 <asp:BoundField DataField="ImportDate" HeaderText ="导入日期"  ItemStyle-Width=80/>
                   <asp:BoundField DataField="ID" HeaderText ="导入日期"  ItemStyle-Width=80/>
        </Columns>
       
    

        </asp:GridView>
    </div>
    </form>
</body>
</html>
