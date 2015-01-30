<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CaseListLeft.aspx.cs" Inherits="CaseListLeft" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

<title></title>
<link href="css/thread.css" rel="stylesheet" rev="stylesheet" type="text/css" />
<link href="css/common.css" rel="stylesheet" rev="stylesheet" type="text/css" />


<script>
    
   
    function refreshPage()
{


 document.location.href=document.location.href;
}
function showUrl(url)
{

    parent.window.right.document.location.href=url;
}



    </script>
</head>
<body >
 <div >
  <table width =100%  cellspacing=0    >
                     <tr height =10 bgcolor=#F7F7F7> <td align =center valign =top  >
                       <b>默认分类 &nbsp;&nbsp;&nbsp;</b>
                      
                     </td>
                              
                         </table>
    </div>
 <div id="Content2"  >
<div id="main" >
  <div class="left">
    <div class="l1" id="display">
      
      <div class="m2">
      <ul>
            <%=GetPage()%>
        </ul>
                   
         <div >
  <table width =100%  cellspacing=0    >
                     <tr height =10 bgcolor=#F7F7F7> <td align =center valign =top  >
                       <b>自定义分类 &nbsp;&nbsp;&nbsp;</b>
                      
                     </td>
                              
                         </table>
    </div>
         <ul>
          <%=Menus %> 
        </ul>                     
                             
      </div>
    </div>
   
  </div>
  
 </div>

 </div>
</body>
</html>