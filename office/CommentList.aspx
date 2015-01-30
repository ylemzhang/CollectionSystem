<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CommentList.aspx.cs" Inherits="CommentList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
   <title>U主管批示</title>
      <link href="CSS/css.css" type="text/css" rel="stylesheet">
      <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
      <script type="text/javascript" src="javascript/jquery.js" ></script>
      <script type="text/javascript" src="javascript/common.js" ></script>
</head>
<body>
    <form id="form1" runat="server">
    
                          <table width =100%  cellspacing=0   >
                     <tr height =10 bgcolor=#F7F7F7> <td align =left valign =top class="menu" >
                    主管有批示的案子
                     </td><td>记录总数:<%=TotalRecord%></td></tr>
                         </table>
                         
                        
                      
       
       <div>                  
     <asp:GridView  Width =100% ID="GridView1" runat="server" 
        HeaderStyle-CssClass="dg_header"  AutoGenerateColumns =false    >
             
         <Columns>
         
          
<asp:BoundField DataField="Date1" HeaderText ="批示日期"  ItemStyle-Width=60/>
<asp:BoundField DataField="person" HeaderText ="主管"  ItemStyle-Width=40/>
 <asp:BoundField DataField="Str1" HeaderText ="主管批示"  ItemStyle-Width=200/>             
                
        </Columns>
    

        </asp:GridView>
    </div>
    </form>
</body>
</html>
