<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExcepFields.aspx.cs" Inherits="ExcepFields"  enableEventValidation ="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>选择要导出的字段</title>
      <link href="CSS/css.css" type="text/css" rel="stylesheet">
    
     <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
     <script type="text/javascript" src="javascript/jquery.js" ></script>
    <script type="text/javascript" src="javascript/common.js" ></script>
    <script>
    

 function Add()
    {
     var org=document.getElementById('lstOrg');
          var dest=document.getElementById('lstDest');
        
      AddOption(org,dest);
      

    }
 function AddOption(org,dest)
    {
   
     
             var k=org.selectedIndex;
      
          if (k==-1)
          {
          alert('请选择字段');
          }
          else
          {
            var txt=org.options[k].text;
            var value=org.options[k].value;
              dest.options[dest.options.length]=new Option(txt,value,true,true);
          
         org.selectedIndex=-1;
         org.options[k]=null;
         }
      

    }
function Delete()
{
       var org=document.getElementById('lstOrg');
          var dest=document.getElementById('lstDest');
        
      AddOption(dest,org);

}



  function getFieldsAll()
    {
 var sel=document.getElementById('lstDest');
    var rtn='';
     var rtn1='';
if(sel.options.length==0)
{
alert("你没有选择导出的字段");
return false;
}
   
     for( var k=0;k<sel.options.length;k++)
         {
           rtn=rtn+ sel.options[k].text+"|";
            
        }
        
         rtn=rtn.substring(0,rtn.length-1);
       
      document.all.txtFieldsName.value=rtn;
      
      
       for( var k=0;k<sel.options.length;k++)
         {
           rtn1=rtn1+ sel.options[k].value+"|";
            
        }
        
         rtn1=rtn1.substring(0,rtn1.length-1);
       
      document.all.txtFieldsValue.value=rtn1;
      
      
      return true;
   
}


       
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div  style=" margin-left:50px">
    <table height =700>
    
     <tr><td colspan =3></td></tr>
    
          <tr><td colspan =3><b>选择要导出的字段：</b></td></tr>
     <tr><td style="width: 306px"> <asp:ListBox ID="lstOrg" runat="server" Width =297px  height =600></asp:ListBox></td>
     <td> <table>
     <tr><td><input id="btnDel" type="button" onclick="Add()" value="-->" /></td></tr>
     <tr><td><input id="Reset1" type="button" onclick="Delete()" value="<--" /></tr>
     </table></td>
   <td style="height: 37px"><asp:ListBox ID="lstDest" runat="server" Width =297px  height =600></asp:ListBox></td></tr>
   
   
    <tr><td height =40 align =center colspan =3 >
       <asp:Button ID="btnSave" runat="server" Text="导出" OnClientClick ="return  getFieldsAll()" OnClick="btnSave_Click" />&nbsp; <input type=button onclick  ="window.close()" value="关闭" />
     
     
     </td></tr>  
     </table> 
    </div>
       <asp:TextBox ID="txtFieldsName" runat="server" style="display:none "></asp:TextBox>
       <asp:TextBox ID="txtFieldsValue" runat="server" style="display:none "></asp:TextBox>
    </form>
</body>
</html>
