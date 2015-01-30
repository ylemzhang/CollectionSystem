<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ImportBalanceRecords.aspx.cs" Inherits="ImportBalanceRecords" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>导入记录</title>
 <link href="CSS/css.css" type="text/css" rel="stylesheet">
      <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
      <script type="text/javascript" src="javascript/jquery.js" ></script>
      <script type="text/javascript" src="javascript/common.js" ></script>
     <script>
 
    function check()
    {
    
   
      if (document.all.txtFrom.value.Trim()=="")
    {
     alert(" 最新余额时间<%=Common.StrTable.GetStr("shallnotbenull") %>");
    return false;
    }
    
    return true;
    }
    
    
   
   
   

  function validateFileName(uploadfileID)
{
   
    if (!check())
    {
    return false;
    }

    var ext="xls";
    var uploadfile=document.getElementById(uploadfileID).value;
    
      if(uploadfile!='')
      {
           var file=uploadfile.substring(uploadfile.length-3,uploadfile.length);
           var lowerFile=file.toLowerCase();
  
          if (lowerFile!=ext)
          {
           alert('<%=Common.StrTable.GetStr("improperFile") %>');
            return false;
          }
           document.all.btnImport.style.visibility="hidden";
          return true;
      }
     
    else
    {
    alert('<%=Common.StrTable.GetStr("selectfiletoimport") %>');
    return false;
    }
   
    
}

 </script>
</head>

<body >
    <form id="form1" runat="server">
   
    <br />
     <table  runat= server width =600 id="tbImport"  style=" margin-left:50px" >
        <tr><td><b>导入欠款余额更新记录：</b> <hr /></td></tr> 
   <tr><td style=" color:Red"><b><%=Common.StrTable.GetStr("notefortitle") %></b> <br /> <b style=" color:Red">并且要导入的Excel的表名必须是Sheet1</b></td></tr>
  <tr><td>
       最新余额时间:<asp:TextBox ID="txtFrom" runat="server" Width =70></asp:TextBox><span style="color:Red">*</span><img src="Images/Calendar.gif"  style=" cursor:hand "  onclick ="ShowCalenderWindow('txtFrom')"/>&nbsp;&nbsp;
     
       &nbsp;<asp:FileUpload ID="FileUpload1" runat="server"  />&nbsp; <asp:Button ID="btnImport" runat="server"   Text="导入" OnClick="btnImport_Click" OnClientClick="return validateFileName('FileUpload1')" /></td></tr>
    </table>
    </form>
</body>
</html>

