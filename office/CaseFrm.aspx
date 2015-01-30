<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CaseFrm.aspx.cs" Inherits="CaseFrm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
  <script>
    
   
    function refreshPage()
{


  window.left.window.refreshPage();

   window.right.window.refreshPage();
       
}



    </script>
</head>
<frameset id="down" cols ="180,*" frameborder=1>
<frame id="left" src="CaseListLeft.aspx" noresize />
<frame id="right" src="caselist.aspx?act=1" />
</frameset>
</html>