<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Attachment.aspx.cs" Inherits="Attachment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >

<head id="Head1" runat="server">
    <title><%=Common.StrTable.GetStr("attachment") %></title>
     <link href="CSS/css.css" type="text/css" rel="stylesheet" />
     <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
     <base target =_self></base>
     <script type="text/javascript" src="javascript/jquery.js" ></script>
     <script type="text/javascript" src="javascript/common.js" ></script>
    <script>
     function validateFileName()
    {

        var uploadfile=document.getElementById('FileUpload1').value;
        
          if(uploadfile=='')
          {
               alert("<%=Common.StrTable.GetStr("Pleaseselectattachment") %>");
             return false;
          }
         
        
    }

  function  CloseWindow()
    {
var rtn="<%=Attachmentlist %>|<%=AttachmentlistRealName %>";

window.returnValue=rtn;


      
    }
      function  deleteAttach(file,realname)
    {
document.all.txtDeleteFile.value =file;
document.all.txtDeleteRealName.value =realname;

document.all.btnDelete.click();


      
    }
    
    </script>
</head>
<body bgcolor=#F7F7F7 onunload ="CloseWindow()" >
    <form id="form1" runat="server">
    <br />
     <br />
    <table  align =center  width =300>
    <tr><td><b><%=Common.StrTable.GetStr("attachment") %></b></td></tr>
    <tr><td  > 
       <div runat =server id="divAttachmentList"></div></td></tr>
        <tr><td>  &nbsp;<asp:FileUpload ID="FileUpload1" runat="server"  />&nbsp;
        <asp:Button ID="btnImport" runat="server"   Text="Attach" OnClick="btnImport_Click" OnClientClick="return validateFileName()" /><input type=button  onclick ="window.close()" value ="<%=Common.StrTable.GetStr("close") %>" /></td></tr>
    </table>
      
       
        <asp:TextBox ID="txtDeleteFile" runat="server" style="display:none "></asp:TextBox>
         <asp:TextBox ID="txtDeleteRealName" runat="server" style="display:none "></asp:TextBox>
         <asp:Button ID="btnDelete" runat="server"  OnClick="btnDelete_Click"  style="display:none "/>
    </form>
</body>
</html>
