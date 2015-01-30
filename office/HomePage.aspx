<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HomePage.aspx.cs" Inherits="HomePage" %>

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
<frameset id="down" rows ="250,*"  frameborder=no >

<frameset id="Frameset1" cols ="50%,*" frameborder=1 >
<frame id="Frame1" src="AlertPromisedDate.aspx" noresize/>
<frame id="Frame2" src="AlertPayment.aspx" noresize/>
</frameset>
<frameset id="Frameset2" cols ="50%,*" frameborder=1 >
<frame id="Frame3" src="AlertFollewup.aspx" noresize/>
<frame id="Frame4" src="AlertComment.aspx" noresize/>
</frameset>
</frameset>
</html>