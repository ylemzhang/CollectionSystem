<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PaymentReportDetail.aspx.cs" Inherits="PaymentReportDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
 
<head runat="server">
    <title>还款信息</title>
    <link href="CSS/css.css" type="text/css" rel="stylesheet">
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
</head>
<body>
    <form id="form1" runat="server">
    <div>还款信息</div>
    <div>
      <asp:GridView  Width =100% ID="GridView1" runat="server" OnRowDataBound="GridView1_RowDataBound" 
        HeaderStyle-CssClass="dg_header"  AutoGenerateColumns =false     >
             
          <Columns>
            
   
             <asp:BoundField DataField="tbName" HeaderText ="姓名" ItemStyle-Width=80/>
            
           
            
              <asp:BoundField DataField="tbKey" HeaderText ="帐号/合同号" />
               <asp:BoundField DataField="tbPayment" HeaderText ="还款金额" />
                 <asp:BoundField DataField="tbPayDate" HeaderText ="还款日期"  ItemStyle-Width=80/>
 <asp:BoundField DataField="tbBalance" HeaderText ="最新金额" />

                 <asp:BoundField DataField="ImportDate" HeaderText ="导入日期"  ItemStyle-Width=80/>
                 <asp:BoundField DataField="CaseID" HeaderText ="查盾案件信息"  ItemStyle-Width=80/>
                 
                 
        </Columns>
       
      <EmptyDataTemplate>
      无记录
      </EmptyDataTemplate>
     

        </asp:GridView>
    </div>
    </form>
</body>
</html>
