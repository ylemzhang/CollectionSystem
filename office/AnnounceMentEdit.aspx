<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AnnounceMentEdit.aspx.cs" Inherits="AnnounceMentEdit" ValidateRequest="false" %>

<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>公告编辑</title>
     <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
       <link href="CSS/css.css" type="text/css" rel="stylesheet">    
        <script type="text/javascript" src="javascript/jquery.js" ></script>
        <script type="text/javascript" src="javascript/common.js" ></script>
   <%-- <script type="text/javascript" src="fckeditor/fckeditor.js"></script>--%>
   <script>
    function Check()
    {
        if (document.all.txtTitle.value.Trim()=='')
       {
       alert("<%=Common.StrTable.GetStr("title") %> <%=Common.StrTable.GetStr("shallnotbenull") %>");
       return false;
       }
       return true;
    }

    function refreshPage()
{

   document.location.href=document.location.href;
       
}
function  ConfirmDelete()
{

   return confirm ('<%=Common.StrTable.GetStr("confirmDelete") %>');
 }
 
    function ShowAttachmentWindow()
{  

 var obj = new Object();
    obj.name="attachmenttedit";
    var str=window.showModalDialog("Attachment.aspx",obj,"dialogWidth=450px;dialogHeight=200px;toolbar=no;titlebar=no;help=no;resizable=yes;status=no;scroll=no");

    if(str!=null) 
    {
   
    Addattachment(str);
 
    }
}

function Addattachment(str)
{


document.all.txtAttachList.value=str;
getAttachmentListinDive();

}


function getAttachmentListinDive()
{
var allstr=document.all.txtAttachList.value;
var strs=allstr.split("|");

 var attrstr=strs[0];
var attrstrReal=strs[1];

 if (attrstr=="") 
 {
 return;
 }
 var template="<span  style ='color:Blue;cursor:hand' onclick=window.open('UploadPath/attachment/yyy')>xxx</span>&nbsp;&nbsp;"

 var attrs=attrstr.split(",");
 var strReals=attrstrReal.split(",");
 var str="";
 var temp;
 for(var i=0;i<attrs.length;i++)
 {

     if (attrs[i]!="")
     {
      temp=template.replace(/xxx/g,attrs[i]);
      
     str=str+temp.replace(/yyy/g,strReals[i]);
     }
 }
 document.all.divAttachmentList.innerHTML=str;

}

   
    
   </script>
   
</head>
<body>
    <form id="form1" runat="server">
   
                                                                  <table width =800 border =1>
                                                                  <col  width =100/>
                                                                   <col  />
                                                                  <colgroup>
                                                                  </colgroup>
                                                                  <tr><td><span runat =server id="spanAdd" > <a href ="javascript:ShowAttachmentWindow()" style =" color:Blue "><%=Common.StrTable.GetStr("addAttachment") %>:</a></span></td><td><div runat =server id="divAttachmentList"></div></td></tr>
                                                                  <tr><td><%=Common.StrTable.GetStr("title") %>:</td><td>
                                                                      <asp:TextBox ID="txtTitle" runat="server" Width =557px></asp:TextBox></td></tr>
                                                                      <tr height =300><td valign =top><%=Common.StrTable.GetStr("body") %>:</td><td>
                                                                      <FCKeditorV2:FCKeditor ID="txtBody" runat="server"  Height =390 BasePath ='fckeditor/' >
        </FCKeditorV2:FCKeditor> </td></tr>
                                                                      <tr><td colspan=2><%=Common.StrTable.GetStr("expiration") %>:
                                                                      <asp:TextBox ID="txtDate" runat="server" Width =100></asp:TextBox><img src="Images/Calendar.gif"  style=" cursor:hand "  onclick ="ShowCalenderWindow('txtDate')"/>  </td></tr>
                                                                     
                                                                           
                                                                       <tr><td colspan=2 align =center  > 
                                                                  <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" OnClientClick =" return Check()" />
         <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" OnClientClick =" return ConfirmDelete()" />
         <input  type =button value="<%=Common.StrTable.GetStr("cancel") %>"  onclick ="window.close()"/>
        </td></tr>
                                                                  </table>
                                                                
               <asp:TextBox ID="txtAttachList" runat="server"  style="display:none "></asp:TextBox>                              
        
        <asp:TextBox ID="txtDeleteFile" runat="server" style="display:none "></asp:TextBox>
       
      
    </form>
</body>
</html>
