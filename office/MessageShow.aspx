<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MessageShow.aspx.cs" Inherits="MessageShow" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>邮件</title>
    <script type="text/javascript" src="javascript/jquery.js" ></script>
      <script type="text/javascript" src="javascript/common.js" ></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <table  width =100% >
    
 
      <tr><td>Title:<%=Title %></td></tr>
     <tr><td>Sender:<%=Sender %></td></tr>
     <tr><td>
         Recipient:<%=Recipient %></td></tr>
     <tr><td>Date:<%=SendOn%></td></tr>
      <tr><td>Body:</td></tr>
      <tr><td><%=Body %></td></tr>
     
  
    </table>
    </div>
    </form>
</body>
</html>
