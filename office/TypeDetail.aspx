<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TypeDetail.aspx.cs" Inherits="TypeDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Untitled Page</title>
    <link href="CSS/css.css" type="text/css" rel="stylesheet">
    <script type="text/javascript" src="javascript/jquery.js" ></script>
    <script type="text/javascript" src="javascript/common.js" ></script>
    <script>
    function fillDetail(display,discription,id)
    {
    document.all.txtID.value=id;
    document.all.txtFDisplay.value=display;
   
    document.all.txtDiscription.value=discription;
    document.all.btnSave.value="保存";
     document.all.txtFDisplay.focus();
    }
    
     function add()
    {
    document.all.txtID.value='';
    document.all.txtFDisplay.value='';
 
    
    document.all.txtDiscription.value='';
    document.all.btnSave.value="新增";
   document.all.txtFDisplay.focus();
    }
    
    
        
        function Check()
        {
          var txtTypeID=document.all.txtTypeID.value;
              if(txtTypeID=='')
              {
               alert("不能保存，有错");
                return false;
              }
              
          var dispalytext=document.all.txtFDisplay.value;
              if(dispalytext=='')
              {
               alert("类型不能为空");
                return false;
              }
              
              var grid=document.all.GridView1;
              var i;
            
            if (document.all.txtID.value=='') //add
            {
                  for(i=1;i<grid.rows.length;i++)
                  {
             
                    if (grid.rows[i].cells[1].innerText==dispalytext)
                    {
                     alert("你要加的类型已存在，不能新增");
                    return false;
                    }
                  }
             }
           
            return true;
        }
    </script>
</head>
<body onload="document.all.txtFDisplay.focus()">
    <form id="form1" runat="server">
   
    <table width=850 cellspacing =0 >
    <tr>
    <td valign =TOP>  
    <table width =100% cellspacing =0>
    <tr><td align =left valign =top class="menu" bgcolor=#F7F7F7></td><td align =right valign =top class="menu" bgcolor=#F7F7F7>
    <span  style ="cursor:hand; color:blue" onclick="add()" > 新增|</span>
                              
 <asp:LinkButton ID="LinkButton2" style ="cursor:hand; color:blue"  runat="server" OnClientClick="return ConfirmDelete()" OnClick="LinkButton2_Click">删除</asp:LinkButton></td></tr>
                            
                            <tr>
                            <td  colspan=2 >
                             <asp:GridView  Width =100% ID="GridView1" runat="server" OnRowDataBound="GridView1_RowDataBound"  AllowSorting=true  
        AlternatingRowStyle-CssClass="dg_alter" RowStyle-CssClass="dg_item" HeaderStyle-CssClass="dg_header"  AutoGenerateColumns =false  >
             
         <Columns>
             <asp:TemplateField ItemStyle-Width=10>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server"  />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="ID" HeaderText ="ID"/>
             <asp:BoundField DataField="FTypeValue" HeaderText ="类型"/>
            
            <asp:BoundField DataField="Description" HeaderText ="描述" />


                
        </Columns>

        </asp:GridView>
                            </td>
                            </tr>
    </table>
   
         </td>
     <td width =300 valign =TOP >
        
     <table width=100%  bgcolor=#F7F7F7 cellspacing =0>
      <colgroup>
    <col width =100 />
    <col width =200 />
  
   
    </colgroup>
     <tr><td>类型</td><td> <asp:TextBox ID="txtFDisplay" runat="server" TextMode =MultiLine  style="overflow: auto"  Width =200px></asp:TextBox></td></tr>
    
     <tr><td>描述</td><td> <asp:TextBox ID="txtDiscription" runat="server" TextMode =MultiLine  style="overflow: auto" Width =200px></asp:TextBox></td></tr>
      <tr><td colspan=2 align =Center > 
   
        <asp:Button ID="btnSave" runat="server" Text="新增" OnClick="btnSave_Click" OnClientClick =" return Check()" />
  
        </td></tr>
     
     </table>
     </td>
    </tr>
    </table>
    <asp:TextBox ID="txtID" runat="server"  style=" display:none "></asp:TextBox>
     <asp:TextBox ID="txtTypeID" runat="server"  style=" display:none "></asp:TextBox>
   
    </form>
</body>
</html>
