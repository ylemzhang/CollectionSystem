<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CompanyTree.aspx.cs" Inherits="CompanyTree" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

<title></title>


<link href="css/thread.css" rel="stylesheet" rev="stylesheet" type="text/css" />
<script src="javascript/jquery.js"></script>
<script>
    
   
    function refreshPage()
{


 document.location.href=document.location.href;
}
function showUrl(obj,url)
{

var txt=obj.innerHTML;

if (txt.indexOf("(")==-1)
{
 txt=txt+"(已打开)";
 obj.innerHTML=txt;
}
    parent.window.right.document.location.href=url;
    
}



function showContent(inx)
{

var id="Content"+inx;
var div=document.getElementById(id);
if (div.style.display =='none')
{
div.style.display ='';
}
else
{
div.style.display ='none';
}

}

    </script>
</head>
<body bgcolor=#F7F7F7 >
  <form id="form1" runat="server" defaultbutton ="btnGo">
 <div onclick="showContent(1)" style ="cursor:hand;width:100%">
  <table width =100%  cellspacing=0 border=1  >
                     <tr height =10 bgcolor=gray> <td align =center valign =top class="menu"   >
                      <font color =white> 查找条件：</font>
                      
                     </td>
                              
                         </table>
    </div>
    
  <div id="Content1"  style="display:none; background-color:#F7F7F7 ; width:100% " >
  <table width =100%>
  <tr>
  <td>公司<asp:DropDownList ID="ddlCompany" runat="server" Height =19px Width =85px AutoPostBack =true OnSelectedIndexChanged ="ddlCompany_SelectedIndexChanged" >
      </asp:DropDownList></td>
  </tr>
  
  <tr><td>批次<asp:DropDownList ID="ddlPatch" runat="server" Height =19px Width =85px AutoPostBack =true OnSelectedIndexChanged ="ddlPatch_SelectedIndexChanged" > </asp:DropDownList></td>
  </tr>
 
  
  <tr>
  <td>金额<asp:TextBox ID="txtFrom" runat="server" Width =40 Height =15px></asp:TextBox>到<asp:TextBox ID="txtTo" Width =40 runat="server" Height =15px></asp:TextBox>
      </td>
  </tr>
   <tr>
  <td>姓名<asp:TextBox ID="txtName" runat="server" Width =80 Height =15px></asp:TextBox></td>
  </tr>  
  <tr>
  <td>
   备注<asp:TextBox ID="txtNote" runat="server" Width =80 Height =15px></asp:TextBox>
  </td>
  </tr>
  <tr>
  <td>
   电话<asp:TextBox ID="txtMobile" runat="server" Width =80 Height =19px></asp:TextBox>
  </td>
  </tr>
  <tr><td>付款<asp:DropDownList ID="ddlHasPayment" runat="server" Width =85px Height =19px > </asp:DropDownList></td>
  </tr>
 
  <tr><td>打开<asp:DropDownList ID="ddlOpened" runat="server" Width =85px Height =19px > </asp:DropDownList></td>
  </tr>

  <tr><td>致电<asp:DropDownList ID="ddlPhoned" runat="server" Width =85px Height =19px > </asp:DropDownList></td>
  </tr>
  
  <tr><td>外访<asp:DropDownList ID="ddlVisit" runat="server" Width =85px Height =19px > </asp:DropDownList></td>
  </tr>
  
   <tr><td>承诺<asp:DropDownList ID="ddlPromisedDate" runat="server" Width =85px Height =19px> </asp:DropDownList></td>
  </tr>
  <tr><td>只读<asp:DropDownList ID="ddlReadonlyCase" runat="server" Width =85px Height =19px> </asp:DropDownList></td>
  </tr>
  <tr>
  <td align =center bgcolor =gray>
  <asp:LinkButton ID="btnGo" runat="server" OnClick="btnGo_Click" Width ="50" ForeColor=white>查找</asp:LinkButton>
  </td>
  </tr>
  </table>
  </div>
<div style ="width:100%">
<table width =100%>
 <tr>
 <td> <asp:DropDownList ID="ddlOrder" runat="server" Width =60px Height =19px> </asp:DropDownList></td><td><asp:LinkButton ID="btnAsc" runat="server" OnClick="btnAsc_Click"  >顺序</asp:LinkButton></td><td><asp:LinkButton ID="btnDesc" runat="server" OnClick="btnDesc_Click" >逆序</asp:LinkButton></td> 
       
  </tr>
</table>
</div>
  
  
  
    <div id="Content2"  >
<div id="main" >
  <div class="left">
    <div class="l1" id="display">
      
      <div class="m2">
         
        <ul><%=Menus %> </ul>
                   
                             
                             
      </div>
    </div>
   
  </div>
  
 </div>
 </div>

 
</form>

</body>
</html>