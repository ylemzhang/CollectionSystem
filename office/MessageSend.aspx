<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MessageSend.aspx.cs" Inherits="MessageSend" %>

<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title><%=Common.StrTable.GetStr("newMessage")%></title>
     <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
       <link href="CSS/css.css" type="text/css" rel="stylesheet">    
       <script type="text/javascript" src="javascript/jquery.js" ></script>
        <script type="text/javascript" src="javascript/common.js" ></script>
   <%-- <script type="text/javascript" src="fckeditor/fckeditor.js"></script>--%>
   <script>
      function ShowUserWindow()
{  

 var obj = new Object();
    obj.name="ttedit";
    var str=window.showModalDialog("UserSelect.aspx",obj,"dialogWidth=700px;dialogHeight=400px;toolbar=no;titlebar=no;help=no;resizable=no;status=no;scroll=yes");

    if(str!=null) 
    {
  
       if (document.getElementById("txtRecipient").value !='')
       {
       document.getElementById("txtRecipient").value=document.getElementById("txtRecipient").value+";"+str;
       }
       else
       {
        document.getElementById("txtRecipient").value=str;
       }
    
    }
}

    function Check()
    {
        if (document.all.txtRecipient.value.Trim()=='')
       {
       alert("<%=Common.StrTable.GetStr("recipient") %>   <%=Common.StrTable.GetStr("shallnotbenull") %>");
       return false;
       }
        if (document.all.txtTitle.value.Trim()=='')
       {
       alert("<%=Common.StrTable.GetStr("title") %>  <%=Common.StrTable.GetStr("shallnotbenull") %>");
       return false;
       }
       return true;
    }

    function refreshPage()
{

   document.location.href=document.location.href;
       
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





if (document.all.txtAttachList.value=='')
{
document.all.txtAttachList.value=str;
}
else
{
var strs=str.split("|");

 var attrstr=strs[0];
var attrstrReal=strs[1];


var txtvalue=document.all.txtAttachList.value;

var strs1=txtvalue.split("|");

 var txtfile=strs1[0];
var txtReal=strs1[1];

var firstpart=attrstr+","+txtfile;
var lastpart=attrstrReal+","+txtReal;


document.all.txtAttachList.value=firstpart+"|"+lastpart;
}
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
                                                                   <tr><td>To:</td><td>
                                                                      <asp:TextBox ID="txtRecipient" runat="server"  Width =400px></asp:TextBox><input id="btn1" type="button" onclick="ShowUserWindow()" value="<%=Common.StrTable.GetStr("selectusers") %>" /></td></tr>
                                                                  <tr><td><span runat =server id="spanAdd" > <a href ="javascript:ShowAttachmentWindow()" style =" color:Blue "><%=Common.StrTable.GetStr("addAttachment")%></a></span></td><td><div runat =server id="divAttachmentList"></div></td></tr>
                                                                  <tr><td><%=Common.StrTable.GetStr("title") %>:</td><td>
                                                                      <asp:TextBox ID="txtTitle" runat="server" Width =590px></asp:TextBox></td></tr>
                                                                      <tr height =300><td valign =top><%=Common.StrTable.GetStr("body") %>:</td><td>
                                                                      <FCKeditorV2:FCKeditor ID="txtBody" runat="server"  Height =390 BasePath ='fckeditor/' >
        </FCKeditorV2:FCKeditor> </td></tr>
                                                                       
                                                                           
                                                                       <tr><td colspan=2 align =center  > 
                                                                  <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" OnClientClick =" return Check()" />
        
         <input  type =button value="<%=Common.StrTable.GetStr("close") %>"  onclick ="window.close()"/>
        </td></tr>
                                                                  </table>
                                                                
               <asp:TextBox ID="txtAttachList" runat="server"  style="display:none "></asp:TextBox>                              
        
        <asp:TextBox ID="txtDeleteFile" runat="server" style="display:none "></asp:TextBox>
        <asp:TextBox ID="txtUsersID" runat="server" style="display:none "></asp:TextBox>
       
       
       
      
    </form>
</body>
</html>
