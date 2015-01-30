<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MessageSendlist.aspx.cs" Inherits="MessageSendlist" %>

<%@ Register Src="PagingControl.ascx" TagName="PagingControl" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
    <link href="CSS/css.css" type="text/css" rel="stylesheet">
    <script type="text/javascript" src="javascript/jquery.js" ></script>
       <script language =javascript src="Javascript/common.js"></script>
       <script>
       
       function allCheck(chk)  
        {  
            for (var i=0;i<form1.elements.length;i++)  
            {  
            var e=form1.elements[i];  
            if (e.type=='checkbox')  
            e.checked=chk.checked;  
            }  
        }  
       
function sendMessage()
{

url="MessageSend.aspx";
OpenWindow(url,800,520);
}
       


       function edit(type,id)
       {
     
       var url ='MessageDetail.aspx?type='+type+'&id='+id;
        
         parent.window.frmbottom.document.location.href=url;
     

       }
       
       function  refreshPage()
       {
     
       document.all.btnSearch.click();
       }
       
       
         function search()
       {
     
       document.all.btnSearch.click();
       }

      
       

       function  ConfirmDelete()
        {

            var thisfrm=document.all.form1;
            var checked;
           for (i=0; i<thisfrm.length; i++) 
           {

              if (thisfrm.elements[i].name.indexOf('CheckBox1') !=-1)
               {

                  if(thisfrm.elements[i].checked)
                   {

                     checked=true;
                     break;

                  }

              }

           }
           
           if (checked)
           {
           return confirm ('<%=Common.StrTable.GetStr("confirmDelete") %>');
           }
           else
           {
            window.alert('<%=Common.StrTable.GetStr("selectRecordToDelete") %>');
           return false;
           }

    }
    
     function  ConfirmChecked()
        {

            var thisfrm=document.all.form1;
            var checked;
           for (i=0; i<thisfrm.length; i++) 
           {

              if (thisfrm.elements[i].name.indexOf('CheckBox1') !=-1)
               {

                  if(thisfrm.elements[i].checked)
                   {

                     checked=true;
                     break;

                  }

              }

           }
           return checked;
        
    }
    
   
    
   
    
     
   
   function keyDown()
   {
       if (event.keyCode==13)
       {
      
       document.all.btnSearch.click();
       return false;
       }
       return true;
   }
       </script>
  
</head>
<body   >
    <form id="form1" runat="server">
    <div >
  <table style="width:100%" id="ToolBarTable" cellspacing =0 >
                <tr bgcolor =#F7F7F7 valign =top >
                <td valign =middle ><strong> <%=Common.StrTable.GetStr("senditems")%></strong> </td>
                <td height =10 valign =middle > 
                            <uc1:PagingControl id="PagingControl1" runat="server">
                            </uc1:PagingControl></td>
                    <td class="menu" height =0 align =left  valign =middle>&nbsp;
                   
                        <asp:TextBox ID="txtSearch" runat="server" Width =120 onkeydown="return keyDown()" ></asp:TextBox>&nbsp;<img  style=" cursor:hand ; vertical-align:bottom "  src="Images/search1.gif"  id="IMG1" language="javascript"  onclick="search()"  />&nbsp;&nbsp;<%=Common.StrTable.GetStr("totalRecords")%>: <%=TotalRecords %></td>
                       
                        <td align =right valign =top class="menu"  >
                           
 <asp:Button ID="btnSearch" runat="server" Text="Button" OnClick="btnSearch_Click"  style=" visibility:hidden "/>
                      
                          <a href="javascript:sendMessage();"  style="color:Blue"><%=Common.StrTable.GetStr("newMessage")%>|</a>
                         <asp:LinkButton ID="LinkButton2" style ="cursor:hand; color:blue"  runat="server" OnClientClick="return ConfirmDelete()" OnClick="LinkButton2_Click"><%=Common.StrTable.GetStr("delete")%>|</asp:LinkButton>
                                
                        </td>
                </tr>            
            </table>
    </div>
   
        <asp:GridView ID="GridView1" runat="server" OnRowDataBound="GridView1_RowDataBound"    Width =100%  
          HeaderStyle-CssClass="dg_header"      AutoGenerateColumns =false  >
            
         <Columns>
      
             <asp:TemplateField  ItemStyle-Width=10  HeaderText="<input type=checkbox onclick ='allCheck(this)' />">
                <ItemTemplate  >
                    <asp:CheckBox ID="CheckBox1" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            
              <asp:BoundField DataField="ID" HeaderText ="Form Name"/>
              <asp:BoundField DataField="Attachment" ItemStyle-Width=30/>
              
              <asp:BoundField DataField="Recipient" HeaderText ="声音" ItemStyle-Width=100/>
              <asp:BoundField DataField="Title" HeaderText ="校验" />
               <asp:BoundField DataField="SentOn"  ItemStyle-Width=120  />
            
          
                
        </Columns>

        </asp:GridView>
   
    
   
    </form>
</body>
</html>
