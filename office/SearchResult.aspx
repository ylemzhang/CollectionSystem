<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SearchResult.aspx.cs" Inherits="SearchResult" %>


<%@ Register Src="PagingControl.ascx" TagName="PagingControl" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head >
    <title>Untitled Page</title>
    <link href="CSS/css.css" type="text/css" rel="stylesheet">
     <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
     <script type="text/javascript" src="javascript/jquery.js" ></script>
     <script type="text/javascript" src="javascript/common.js" ></script>
  <script>
  function backPage()
  {
 window.location.href ="AdvanceSearch.aspx";
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
         
         
           function allCheck(chk)  
        {  
            for (var i=0;i<form1.elements.length;i++)  
            {  
            var e=form1.elements[i];  
            if (e.type=='checkbox')  
            e.checked=chk.checked;  
            }  
        }
  </script>
</head>
<body >
    <form id="form1" runat="server">
 
    <table width =100% cellspacing =0>
    <tr bgcolor=#F7F7F7><td valign =middle ><strong><asp:Label runat =server ID="lblTitle"></asp:Label>&nbsp;&nbsp;  </strong></td>
    
  
    
    <td> 记录总数：<%=TotalRecords %></td><td><uc1:PagingControl id="PagingControl1" runat="server">
                            </uc1:PagingControl></td><td align =right  valign =middle class="menu" >
                            <span  style ="cursor:hand; color:blue" onclick="backPage()" >
                              返回|</span> <span id="spanExcel" runat =server style ="cursor:hand; color:blue" onclick="window.open('ExportExcel.aspx')">导出到Excel|</span> 
                              <asp:LinkButton ID="LinkButton2" style ="cursor:hand; color:blue"  runat="server" OnClientClick="return ConfirmDelete()" OnClick="LinkButton2_Click">删除</asp:LinkButton>
                            <tr>
                            <td  colspan=8 >
                             <asp:GridView  Width =100% ID="GridView1" runat="server" OnRowDataBound="GridView1_RowDataBound" 
        AlternatingRowStyle-CssClass="dg_alter" RowStyle-CssClass="dg_item" HeaderStyle-CssClass="dg_header"  AutoGenerateColumns = true     >
             
       <Columns>
            <asp:TemplateField ItemStyle-Width=10 HeaderText="<input type=checkbox onclick ='allCheck(this)'/>" >
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server"  />
                </ItemTemplate>
            </asp:TemplateField>

                
        </Columns>

        </asp:GridView>
                            </td>
                            </tr>
    </table>
   


   
    </form>
</body>
</html>
