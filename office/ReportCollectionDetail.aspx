<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportCollectionDetail.aspx.cs" Inherits="ReportCollectionDetail" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head >
    <title>Untitled Page</title>
    <link href="CSS/css.css" type="text/css" rel="stylesheet">
     <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
     <script type="text/javascript" src="javascript/jquery.js" ></script>
     <script type="text/javascript" src="javascript/common.js" ></script>
     
     <script>
     
      function OpenSearch()
    {
    var text=document.all.txtSearch.value.Trim();
    if (text=="")
    {
      alert("请输入查询条件");
      return;
    }
     if (text.length<2)
    {
      alert("查询条件至少2个字以上");
      return;
    }
      if (text.indexOf("&")>-1 || text.indexOf("*")>-1 || text.indexOf("%")>-1 || text.indexOf("?")>-1)
    {
      alert("非法字符");
      return;
    }
   
   document.all.btn.click();
    }
    
    
      function keyDown()
   {
       if (event.keyCode==13)
       {
      
      OpenSearch();
       return false;
       }
       return true;
   }
   
   function openFields()
   {
   
    OpenWindow("ExcepFields.aspx?type=0&CompanyID=<%=CompanyID %>","700","800");

   }
     </script>
</head>
<body >
    <form id="form1" runat="server">
 
    <table width =100% cellspacing =0>
    
    <tr bgcolor=#F7F7F7><td> 公司：<asp:DropDownList ID="ddlCompany" runat="server" AutoPostBack =true OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" >
      </asp:DropDownList>批次：<asp:DropDownList ID="ddlPatch" runat="server" Height =19px   > </asp:DropDownList>&nbsp;从：&nbsp;<asp:TextBox ID="txtFrom" runat="server" Width =70></asp:TextBox><img src="Images/Calendar.gif"  style=" cursor:hand "  onclick ="ShowCalenderWindow('txtFrom')"/>&nbsp;&nbsp;
                        到：&nbsp;<asp:TextBox ID="txtTo" runat="server" Width =70></asp:TextBox><img src="Images/Calendar.gif" style=" cursor:hand " onclick ="ShowCalenderWindow('txtTo')"/>&nbsp;&nbsp;姓名或帐号：
     <asp:TextBox ID="txtSearch" runat="server"  Height =18px   onkeydown="return keyDown()" />
        &nbsp;&nbsp;<asp:Button ID="btn" runat="server" Text="Go" OnClick="btn_Click" /><span id='spanleave' style =" cursor:hand; color:Blue"  onclick ="window.location.href='ReportManagement.aspx'"  >[返回]</span>&nbsp;&nbsp;记录总数：<%=TotalRecord%></td><td align =right valign =top class="menu" >
                          <span id="spanExcel" runat =server style ="cursor:hand; color:blue" onclick="openFields()">导出到Excel</span> </td></tr>
                            
                            <tr>
                            <td  colspan=2 >
                             <asp:GridView  Width =100% ID="GridView1" runat="server" 
        AlternatingRowStyle-CssClass="dg_alter" RowStyle-CssClass="dg_item" HeaderStyle-CssClass="dg_header"  AutoGenerateColumns =false    >
               <Columns>
         
              <asp:BoundField DataField="业务员" HeaderText ="业务员" ItemStyle-Width=50/>
              <asp:BoundField DataField="客户" HeaderText ="客户" ItemStyle-Width=50/>
              
              <asp:BoundField DataField="批号" HeaderText ="批号" ItemStyle-Width=120/>
              <asp:BoundField DataField="催收方式" HeaderText ="催收方式" ItemStyle-Width=50/>
              
                 <asp:BoundField DataField="联系电话" HeaderText ="联系电话" ItemStyle-Width=100/>
                 
                 
                  <asp:BoundField DataField="联系对象" HeaderText ="联系对象" ItemStyle-Width=100/>
               <asp:BoundField DataField="是否可联" HeaderText ="是否可联"  ItemStyle-Width=100 />
                 <asp:BoundField DataField="联系对象姓名" HeaderText ="联系对象姓名" ItemStyle-Width=100/>
                 
                 
              <asp:BoundField DataField="路费" HeaderText ="路费" ItemStyle-Width=30 />
               <asp:BoundField DataField="日期" HeaderText ="日期"  ItemStyle-Width=70  />
                <asp:BoundField DataField="备注" HeaderText ="备注"   />
            
</Columns>
        </asp:GridView>
                            </td>
                            </tr>
                          
    </table>
   
       

   
    </form>
</body>
</html>