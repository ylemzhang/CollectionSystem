<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LeaveManagement.aspx.cs" Inherits="LeaveManagement" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head >
    <title>Untitled Page</title>
    <link href="CSS/css.css" type="text/css" rel="stylesheet">
     <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
     <script  >
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
     </script>
   
</head>
<body >
    <form id="form1" runat="server">
 
    <table width =100% cellspacing =0>
    
    <tr bgcolor=#F7F7F7><td><asp:TextBox ID="txtFrom" runat="server" Width =70></asp:TextBox><img src="Images/Calendar.gif"  style=" cursor:hand "  onclick ="ShowCalenderWindow('txtFrom')"/>&nbsp;&nbsp;
                        to:&nbsp;<asp:TextBox ID="txtTo" runat="server" Width =70></asp:TextBox><img src="Images/Calendar.gif" style=" cursor:hand " onclick ="ShowCalenderWindow('txtTo')"/>
        &nbsp;&nbsp;<asp:Button ID="btn" runat="server" Text="Go" OnClick="btn_Click" /></td><td align =right valign =top class="menu" >
                           <asp:LinkButton ID="LinkButton2" style ="cursor:hand; color:blue"  runat="server" OnClientClick="return ConfirmDelete()" OnClick="LinkButton2_Click">删除</asp:LinkButton></td></tr>
                            
                            <tr>
                            <td  colspan=2 >
                             <asp:GridView  Width =100% ID="GridView1" runat="server" OnRowDataBound="GridView1_RowDataBound" 
        AlternatingRowStyle-CssClass="dg_alter" RowStyle-CssClass="dg_item" HeaderStyle-CssClass="dg_header"  AutoGenerateColumns =false    >
             
         <Columns>
             <asp:TemplateField ItemStyle-Width=10>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server"  />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="ID" HeaderText ="ID" ItemStyle-Width=100/>
             <asp:BoundField DataField="UserName" HeaderText ="用户名" ItemStyle-Width=100/>
           
             <asp:BoundField DataField="PunchIn" HeaderText ="星期" ItemStyle-Width=100/>
           <asp:BoundField DataField="PunchIn" HeaderText ="上班" />
           <asp:BoundField DataField="PunchOut" HeaderText ="下班" />

                
        </Columns>

        </asp:GridView>
                            </td>
                            </tr>
                          
    </table>
   
       

   
    </form>
</body>
</html>