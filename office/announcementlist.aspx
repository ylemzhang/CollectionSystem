<%@ Page Language="C#" AutoEventWireup="true" CodeFile="announcementlist.aspx.cs" Inherits="announcementlist" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
      <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
     <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
     <link href="CSS/css.css" type="text/css" rel="stylesheet">
     <script type="text/javascript" src="javascript/jquery.js" ></script>    
 <script type="text/javascript" src="javascript/common.js" ></script>
 <script>
    
    function add()
    {
   
   window.open("AnnouncementEdit.aspx?id=");
    }
    
    function refreshPage()
{

   document.location.href=document.location.href;
       
}

function edit(id)
{
if (<%=isadmin%>==1)
{
window.open('AnnouncementEdit.aspx?id='+id);
}
}

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div  >
  <table width =100%  cellspacing=0   border =1 >
                     <tr height =10 bgcolor=#F7F7F7> <td align =left valign =top class="menu" >
                        <%=Common.StrTable.GetStr("announcement")%> &nbsp;&nbsp;&nbsp;
                        <%=Common.StrTable.GetStr("totalRecords")%>: <%=TotalRecords %> &nbsp;&nbsp;&nbsp;
                     <%if (IsAdmin)
                                               { %><span  style ="cursor:hand; color:blue" onclick="add()" > 新增公告</span>&nbsp;&nbsp;&nbsp;<%} %>
                              
                         </table>
    </div>
  <div id=divlist style="width:99%; margin-left :10px" runat =server >
      <asp:DataList id="ItemsList"
           
        
           CellPadding="0"
           CellSpacing="0"
         
           runat="server" Width =100%  OnItemDataBound="ItemsList_ItemDataBound">

         <HeaderStyle BackColor="#F7F7F7">
         </HeaderStyle>

    
        

         <HeaderTemplate>

           

         </HeaderTemplate>
         
         <ItemTemplate>
       
    
         <br />       
          <font size=2pt>标题：</font> <a href ="javascript:edit('<%# DataBinder.Eval(Container.DataItem, "ID")%>')"><font color =blue size=2pt><strong><%# DataBinder.Eval(Container.DataItem, "Title")%> </strong></font> </a>
by <strong><%# DataBinder.Eval(Container.DataItem, "CreateBy")%></strong>   <%# DataBinder.Eval(Container.DataItem, "CreateOn")%> <span  runat =server id="divAttachlist" ><%# DataBinder.Eval(Container.DataItem, "Attachment")%> </span>
            <hr>

               <%# DataBinder.Eval(Container.DataItem, "Body")%> 
            
           
              

         </ItemTemplate>
              
       

      </asp:DataList>

</div> 
    </form>
</body>
</html>
