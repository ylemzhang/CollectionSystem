<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MessageDetail.aspx.cs" Inherits="MessageDetail" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>消息</title>
    <script type="text/javascript" src="javascript/jquery.js" ></script>
   <script type="text/javascript" src="javascript/common.js" ></script>
         <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
     <link href="CSS/css.css" type="text/css" rel="stylesheet">    
     <script>
       
    
    function Check()
    {
        if (document.all.txtEName.value.Trim()=='')
       {
       alert("<%=Common.StrTable.GetStr("englishName") %> <%=Common.StrTable.GetStr("shallnotbenull") %>");
       return false;
       }
       return true;
    }

function refreshPage()
{

   document.location.href=document.location.href;
       
}
function sendMessage(act)
{
id='<%= messageID%>';
type='<%=type%>';
url="MessageSend.aspx?act="+act+"&id="+id+"&type="+type;
OpenWindow(url,800,520);
}
 
function init()
{
if(<%= noid %>==1)
{

document.all.trbtns.style.display='none'
}

}
    </script>
    
</head>
<body  onload ="init()">
    <form id="form1" runat="server" >
    <div>
    <table  width =100% >
    
 
     <tr id="trbtns" bgcolor=#E7E7FF height =30><td align =right><a href="javascript:sendMessage('0');" style="color:Blue">[<%=Common.StrTable.GetStr("reply")%>]</a>&nbsp;<a href="javascript:sendMessage('1');" style="color:Blue">[<%=Common.StrTable.GetStr("replyall")%>]</a>&nbsp;<a href="javascript:sendMessage('2');" style="color:Blue">[<%=Common.StrTable.GetStr("forword")%>]</a>&nbsp;</td></tr>
     <tr><td><%=Title %></td></tr>
     <tr><td><%=Sender %></td></tr>
     <tr><td><%=Attachment %></td></tr>
      <tr><td><%=Body %></td></tr>
     
  
    </table>
        
 

 
       
        
     </div>
    </form>
</body>
</html>
