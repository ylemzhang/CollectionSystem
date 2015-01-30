<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GroupEdit.aspx.cs" Inherits="GroupEdit"  EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
      <link href="CSS/css.css" type="text/css" rel="stylesheet">
    
     <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
     <script type="text/javascript" src="javascript/jquery.js" ></script>
    <script type="text/javascript" src="javascript/common.js" ></script>
    <script>
    function getTypeAll()
    {

    var rtn='';

     var sel=document.getElementById('lstUser');
     for( var k=0;k<sel.options.length;k++)
         {
           rtn=rtn+ sel.options[k].value+",";
            
        }
         if (rtn.length>0)
         {
         rtn=rtn.substring(0,rtn.length-1);
         }
      document.all.txtUserAll.value=rtn;
   
}

 function ShowUserWindow()
    {  

         var obj = new Object();
        obj.name="ttedit";
        var str=window.showModalDialog("UserSelect.aspx?returnType=1",obj,"dialogWidth=350px;dialogHeight=400px;toolbar=no;titlebar=no;help=no;resizable=yes;status=no;scroll=yes");

        if(str!=null) 
        {
            var attrs=str.split(";");
            
            for(var k=0;k<attrs.length;k++)
            {
            var items=attrs[k].split("|");
             AddOption(items[0],items[1]);
             }
      
        }
    }
    
 function AddOption(txt,id)
    {
   
         var sel=document.getElementById('lstUser');
         var k;
         for( k=0;k<sel.options.length;k++)
         {
             if (sel.options[k].value==id)
             {
          
              return ;
              }
         }
         sel.options[sel.options.length]=new Option(txt,id,true,true);
         sel.selectedIndex=0;
      

    }
function Delete()
{
        var sel=document.getElementById('lstUser');
         var k;
         for( k=0;k<sel.options.length;k++)
         {
             if (sel.options[k].selected)
             {
             sel.options[k]=null;
             sel.selectedIndex=-1;
              return;
              }
         }
        
         window.alert('请选择删除用户');

}

function check()
{

getTypeAll();
return true;
}
       
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div  style=" margin-left:50px">
    <table>
    <tr><td colspan =2><b>组名：</b><asp:TextBox ID="txtGroupName" runat="server"></asp:TextBox></td></tr>
    <tr><td colspan =2 style="height: 22px"><b>组长：</b><asp:DropDownList ID="ddlUser" runat="server" Width =100>
          </asp:DropDownList></td></tr>
    
          <tr><td colspan =2><b>组员：</b></td></tr>
     <tr><td rowspan =2 style="width: 306px"> <asp:ListBox ID="lstUser" runat="server" Width =297px  Height =88px></asp:ListBox></td><td> <input id="btnDel" type="button" onclick="Delete()" value="删除用户" /></td></tr>
    <tr><td style="height: 37px"><input id="Reset1" type="button" onclick="ShowUserWindow()" value="选择用户" /></td></tr>
   
   
    <tr><td height =40 align =center colspan =2 >
       <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" OnClientClick =" return check()" />&nbsp; <input type=button onclick  ="window.location.href='CompanyPermission.aspx?id=<%=CompanyID %>'" value="返回" />
     
     
     </td></tr>  
     </table> 
    </div>
     <asp:TextBox ID="txtUserAll" runat="server" style="display:none "></asp:TextBox>
     <asp:TextBox ID="txtOldOwner" runat="server" style="display:none "></asp:TextBox>
      <asp:TextBox ID="txtOldUserAlls" runat="server" style="display:none "></asp:TextBox>
       <asp:TextBox ID="txtOldGroupName" runat="server" style="display:none "></asp:TextBox>
    </form>
</body>
</html>
