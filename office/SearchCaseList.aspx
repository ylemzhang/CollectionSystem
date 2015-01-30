<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SearchCaseList.aspx.cs" Inherits="SearchCaseList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>查找结果列表</title>
      <link href="CSS/css.css" type="text/css" rel="stylesheet">
      <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
      <script type="text/javascript" src="javascript/jquery.js" ></script>
      <script type="text/javascript" src="javascript/common.js" ></script>
</head>
<body>
    <form id="form1" runat="server">
      <table width =100%  cellspacing=0   >
                     <tr height =10 bgcolor=#F7F7F7> <td align =left valign =top class="menu" >
                     <%=SearchTitle %>
                     </td><td>记录总数:<%=TotalRecord%></td></tr>
                         </table>
    <div>
     <asp:GridView  Width =100% ID="GridView1" runat="server" OnRowDataBound="GridView1_RowDataBound" 
        HeaderStyle-CssClass="dg_header"  AutoGenerateColumns =false    >
             
         <Columns>
          <asp:BoundField DataField="OwnerID" HeaderText ="业务员" ItemStyle-Width=80/>
           <asp:BoundField DataField="CompanyID" HeaderText ="公司" />
         
    
             <asp:BoundField DataField="tbName" HeaderText ="姓名" ItemStyle-Width=80/>
            
        
            
              <asp:BoundField DataField="tbKey" HeaderText ="帐号/合同号" />
               
                 
 <asp:BoundField DataField="tbBalance" HeaderText ="合同金额" />

           
                  <asp:BoundField DataField="ID" HeaderText ="caseID"  />
        </Columns>
    

        </asp:GridView>
    </div>
    </form>
</body>
</html>

