<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SameRecordSearch.aspx.cs" Inherits="SameRecordSearch" %>




<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head >
    <title>Untitled Page</title>
    <link href="CSS/css.css" type="text/css" rel="stylesheet">
     <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
     <script type="text/javascript" src="javascript/jquery.js" ></script>
     <script type="text/javascript" src="javascript/common.js" ></script>
    <script>
   
      
    function refreshPage()
    {

     document.all.btnRefresh.click();
           
    }

   

    </script>
</head>
<body >
    <form id="form1" runat="server">
 
    <table width =100% cellspacing =0>
    <tr bgcolor=#F7F7F7>
    <td  width =300 >
 <asp:RadioButtonList ID="RadioButtonList1" RepeatDirection=Horizontal
            runat="server"  >
                </asp:RadioButtonList></td><td align =left >
  公司:<asp:DropDownList ID="ddlCompany" runat="server" AutoPostBack =true OnSelectedIndexChanged ="ddlCompany_SelectedIndexChanged" >
      </asp:DropDownList>
 批次:<asp:DropDownList ID="ddlPatch" runat="server"> </asp:DropDownList>&nbsp;&nbsp;
 <asp:LinkButton ID="btnGo" runat="server" OnClick="btnGoxxx_Click" >查找</asp:LinkButton>
<%-- <asp:LinkButton ID="btnMark" runat="server" OnClick="btnMark_Click" >标志重复</asp:LinkButton>--%></td>
  </tr>
  <tr>
    
  
                            <td  colspan =2 >
                            <table width =100%>
                            <tr>
                            <td>原记录：</td>
                            <td>重复记录数：<%=TotalRecords %></td>
                            </tr>
                            <tr>
                             <td   valign =Top  colspan =2 >
                             <asp:GridView  Width =100% ID="GridView1" runat="server" OnRowDataBound="GridView1_RowDataBound" 
         HeaderStyle-CssClass="dg_header"  AutoGenerateColumns =false    >
             
         <Columns>
           
            <asp:BoundField DataField="ID" HeaderText ="ID"/>
            <asp:BoundField DataField="CompanyID" HeaderText ="公司" ItemStyle-Width=80/>
             <asp:BoundField DataField="PatchID" HeaderText ="批号" ItemStyle-Width=100/>
             <asp:BoundField DataField="tbName" HeaderText ="姓名" ItemStyle-Width=60/>
            <asp:BoundField DataField="tbKey" HeaderText ="帐号" />
         
            <asp:BoundField DataField="tbBalance" HeaderText ="余额" />
             <asp:BoundField DataField="OwnerID" HeaderText ="业务员" />
               <asp:BoundField DataField="tbMobile" HeaderText ="手机" />
                <asp:BoundField DataField="tbIdentityNo" HeaderText ="身份证号" />

<asp:BoundField DataField="CompanyID" HeaderText ="公司" ItemStyle-Width=80/>
<asp:BoundField DataField="ccc" HeaderText ="公司" ItemStyle-Width=80/>

                
        </Columns>

        </asp:GridView>
                           
                             
                            </tr>
                            </table>
                            </td>
                           
                            </tr>
    </table>
   
     
   
    </form>
</body>
</html>
