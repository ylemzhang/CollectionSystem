<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AlertComment.aspx.cs" Inherits="AlertComment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
      <link href="CSS/css.css" type="text/css" rel="stylesheet">
      <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
      <script type="text/javascript" src="javascript/jquery.js" ></script>
      <script type="text/javascript" src="javascript/common.js" ></script>
</head>
<body>
    <form id="form1" runat="server">
     
                         
                         
 
    
                          <table width =100%  cellspacing=0   >
                     <tr height =10 bgcolor=#F7F7F7> <td align =left valign =top class="menu" >
                    主管有批示的案子
                     </td><td>记录总数:<%=TotalRecord%></td></tr>
                         </table>
                         
                         <div id=divSearch runat=server style ="display:none" >
                          <table width =100%>
  <tr>
  <td width =120>公司<asp:DropDownList ID="ddlCompany" runat="server" Height =19px Width =85px AutoPostBack =true OnSelectedIndexChanged ="ddlCompany_SelectedIndexChanged" >
      </asp:DropDownList></td>
 
<td width =120>批次<asp:DropDownList ID="ddlPatch" runat="server" Height =19px Width =85px  > </asp:DropDownList></td>
 
   
      <td width =320>批示日期<asp:TextBox ID="txtDateFrom" runat="server" Width =100></asp:TextBox><img src="Images/Calendar.gif"  style=" cursor:hand "  onclick ="ShowCalenderWindow('txtDateFrom')"/>到<asp:TextBox ID="txtDateTo" runat="server" Width =100></asp:TextBox><img src="Images/Calendar.gif"  style=" cursor:hand "  onclick ="ShowCalenderWindow('txtDateTo')"/></td>
      <td align =left>
          <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">Go</asp:LinkButton></td>
  </tr>
  </table>
                         </div>
       
       <div>                  
     <asp:GridView  Width =100% ID="GridView1" runat="server" OnRowDataBound="GridView1_RowDataBound" 
        HeaderStyle-CssClass="dg_header"  AutoGenerateColumns =false    >
             
         <Columns>
          <asp:BoundField DataField="OwnerID" HeaderText ="业务员" ItemStyle-Width=40/>
           <asp:BoundField DataField="CompanyID" HeaderText ="公司" ItemStyle-Width=60/>
         
    
             <asp:BoundField DataField="tbName" HeaderText ="姓名" ItemStyle-Width=40/>
            
           <asp:BoundField DataField="PatchName" HeaderText ="批号" ItemStyle-Width=50 />
            
          
<asp:BoundField DataField="Date1" HeaderText ="批示日期"  ItemStyle-Width=60/>
<asp:BoundField DataField="person" HeaderText ="主管"  ItemStyle-Width=40/>
 <asp:BoundField DataField="Str1" HeaderText ="主管批示"  ItemStyle-Width=200/>             
                  <asp:BoundField DataField="ID" HeaderText ="id"  ItemStyle-Width=80/>
        </Columns>
    

        </asp:GridView>
    </div>
    </form>
</body>
</html>
