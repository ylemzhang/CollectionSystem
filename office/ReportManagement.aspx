<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportManagement.aspx.cs" Inherits="ReportManagement" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
     <link href="CSS/css.css" type="text/css" rel="stylesheet" >
      <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
     <script>
    
    
function refreshPage()
{

   document.location.href=document.location.href;
       
}


     </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>  
     <table bgcolor="#F7F7F7" width ="100%"  >
     <% =GetPage()%> 
    </table>
   </div>
   </form>
</body>
</html>
