<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserSelectToSign.aspx.cs" Inherits="UserSelectToSign" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head >
    <title>请选择用户</title>
    <link href="CSS/css.css" type="text/css" rel="stylesheet">
     <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
     <script type="text/javascript" src="javascript/jquery.js" ></script>
     <script type="text/javascript" src="javascript/common.js" ></script>
    <script>
   

function  getUsers()
{
var rtn=document.all.txtSignUserID.vaule;

   window.returnValue=rtn;
                    window.close();
             

}

  function setRadio(nowRadio,id)
            {
                var myForm,objRadio;
                myForm=document.forms[0];
                ///alert(myForm);
                for(var i=0;i<myForm.length;i++)
                {
                    if(myForm.elements[i].type=="radio")
                    {
                        objRadio=myForm.elements[i];
                
                        if(objRadio!=nowRadio && objRadio.name.indexOf("GridView1")>-1 && objRadio.name.indexOf("RadioButton1")>-1)
                        {
                       
                            if(objRadio.checked)
                            {
                                objRadio.checked=false;
                            }
                        }
                    }
                }
               document.all.txtSignUserID.vaule=id; 
            }

 

    </script>
</head>
<body >
    <form id="form1" runat="server">
 
    <table width =100% cellspacing =0>
    <tr bgcolor=#F7F7F7><td><strong>用户<%=Common.StrTable.GetStr("list")%> </strong></td><td align =left valign =top class="menu" ><input type =button value ="<%=Common.StrTable.GetStr("select")%> " onclick ="getUsers()" />
                           </td></tr>
                           
                            <tr>
                            <td  colspan=2 >
                             <asp:GridView  Width =100% ID="GridView1" runat="server" OnRowDataBound="GridView1_RowDataBound" 
        AlternatingRowStyle-CssClass="dg_alter" RowStyle-CssClass="dg_item" HeaderStyle-CssClass="dg_header"  AutoGenerateColumns =false  >
             
         <Columns>
             <asp:TemplateField ItemStyle-Width=10>
                <ItemTemplate>
                       <asp:RadioButton ID="RadioButton1" runat="server"  />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="ID" HeaderText ="ID"/>
             <asp:BoundField DataField="UserName" HeaderText ="用户名" ItemStyle-Width=200/>
        
           <asp:BoundField DataField="RealName" HeaderText ="" />

                
        </Columns>

        </asp:GridView>
                            </td>
                            </tr>
                            <tr height =10><td></td></tr>
                            <tr><td align=center><input type =button value ="<%=Common.StrTable.GetStr("select")%> " onclick ="getUsers()" /></td></tr>
    </table>
   
       <input type="hidden" id="txtSignUserID" />

   
    </form>
</body>
</html>