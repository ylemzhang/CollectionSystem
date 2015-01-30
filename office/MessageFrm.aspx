<%@ Page Language="C#" AutoEventWireup="true"  %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
 <script>
    
   
    function refreshPage()
{


  window.frmtop.window.refreshPage();

   window.frmbottom.window.refreshPage();
       
}



    </script>
<html xmlns="http://www.w3.org/1999/xhtml" >

<frameset rows="300,*"  >
<frame   src='MessageList.aspx?type=<%=Request["type"] %>'id="frmtop"  name = "head"  frameborder=1 />

<frame src ='MessageDetail.aspx?type=<%=Request["type"] %>' id="frmbottom"  frameborder=1/>

</frameset>


</html>
