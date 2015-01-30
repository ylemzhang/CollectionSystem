<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExportExcel.aspx.cs" Inherits="ExportExcel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    
</head>
<body>
    <form id="form1" runat="server">
    <div>
     
         <asp:DataGrid  Width =100%   ID="GridView1" runat="server" OnItemDataBound="GridView1_ItemDataBound"  />
     
    </div>
    </form>
</body>
</html>