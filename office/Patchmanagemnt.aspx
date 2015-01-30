<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Patchmanagemnt.aspx.cs" Inherits="Patchmanagemnt" %>

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

   
    function refreshPage()
{

   document.location.href=document.location.href;
       
}

    </script>
</head>
<body >
    <form id="form1" runat="server">
 
    <table width =100% cellspacing =0>
    <tr bgcolor=#F7F7F7><td valign =middle ><strong>批次管理 </strong></td><td> 记录总数：<%=TotalRecords %></td><td><uc1:PagingControl id="PagingControl1" runat="server">
                            </uc1:PagingControl></td><td align =right  valign =middle class="menu" >
                            <asp:LinkButton ID="LinkButton2" style ="cursor:hand; color:blue"  runat="server" OnClientClick="return ConfirmDelete()" OnClick="LinkButton2_Click">删除</asp:LinkButton></td></tr>
                            
                            <tr>
                            <td  colspan=4 >
                             <asp:GridView  Width =100% ID="GridView1" runat="server" OnRowDataBound="GridView1_RowDataBound" 
        AlternatingRowStyle-CssClass="dg_alter" RowStyle-CssClass="dg_item" HeaderStyle-CssClass="dg_header"  AutoGenerateColumns =false    >
             
         <Columns>
             <asp:TemplateField ItemStyle-Width=10>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server"  />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="ID" HeaderText ="ID"/>
             <asp:BoundField DataField="PatchName" HeaderText ="批次名称" />
            <asp:BoundField DataField="ImportTime" HeaderText ="导入时间" />
             <asp:BoundField DataField="ExpireDate" HeaderText ="过期时间" />


                
        </Columns>

        </asp:GridView>
                            </td>
                            </tr>
    </table>
   
       

   
    </form>
</body>
</html>