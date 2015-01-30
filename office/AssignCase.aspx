<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AssignCase.aspx.cs" Inherits="AssignCase" %>


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
   
       function  SignUser(url)
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
          ShowUserWindow(url);
           }
           else
           {
           window.alert('请选择案件');
           return false;
           }

}

  function ShowUserWindow( url)
{  

     var obj = new Object();
   
    var str=window.showModalDialog(url+"?companyID=<%= CompanyID%>",obj,"dialogWidth=700px;dialogHeight=400px;toolbar=no;titlebar=no;help=no;resizable=no;status=no;scroll=yes");

    if(str!=null && str!='') 
    {
  
        document.getElementById("txtSignUserID").value=str;
        if (url=='UserSelectTosign.aspx')
        {
        btnsignUser();
        }
        else
        {
        btnsignReadUser();
        }
       
    }
    else
    {
     alert("没有委派给任何用户");
    }
   
   }
    function refreshPage()
    {

     document.all.btnRefresh.click();
           
    }

    function btnsignUser()
    {

     document.all.btnAssign.click();
           
    }
     function btnsignReadUser()
    {

     document.all.btnAssignReadUser.click();
           
    }
    
   
      var totalSelected=0;
      var totalNum=0;
//        function allCheck1(chk)  
//        {  
//        totalNum =0;
//            totalSelected=0;
//            for (var i=0;i<form1.elements.length;i++)  
//            {  
//                var e=form1.elements[i];  
//                if (e.type=='checkbox') 
//                { 
//                   e.checked=chk.checked;
//                    if(chk.checked)
//                    {
//                      totalSelected=totalSelected+1;
//                      
//                       var ppp=e.parentNode;
//                var sum=ppp.innertext;
//                alert(sum);
//                      
//                    }
//                }  
//               
//            }
//           
//            
//            document.getElementById("spantotalRow").innerText=totalSelected;
//document.getElementById("spantotalNum").innerText=totalNum;
//            
//            
//        }  


 function allCheck1(chk)  
        {  
        totalNum =0;
            totalSelected=0;
            
            var t=document.getElementById("GridView1").childNodes[0];
            for (var i=1;i<t.childNodes.length;i++)  
            {  
            var check=t.childNodes[i].firstChild.firstChild;
           check.checked=chk.checked;
                if(chk.checked)
                {
                totalSelected=totalSelected+1;
                var num=t.childNodes[i].childNodes[3].innerText;
               totalNum=totalNum+parseInt(num);
                }
               
               
            }
           
            
            document.getElementById("spantotalRow").innerText=totalSelected;
document.getElementById("spantotalNum").innerText=totalNum;
            
            
        }  
        
        
        function checkSelect(checkbox,balance)
{
if(checkbox.checked)
{
totalSelected=totalSelected+1;
totalNum=totalNum+parseInt(balance);


}
else
{

totalSelected=totalSelected-1;
totalNum=totalNum-parseInt(balance);

}

document.getElementById("spantotalRow").innerText=totalSelected;
document.getElementById("spantotalNum").innerText=totalNum;
}


 function allUnSelect()  
        {  
         totalNum =0;
            totalSelected=0;
            for (var i=0;i<form1.elements.length;i++)  
            {  
                var e=form1.elements[i];  
                if (e.type=='checkbox') 
                { 
                e.checked=false;
               
               
                }  
            }
         }   
    </script>
</head>
<body onload="allUnSelect()" >
    <form id="form1" runat="server">
 
    <table width =100% cellspacing =0>
    <tr bgcolor=#F7F7F7>
    <td>公司:<asp:DropDownList ID="ddlCompany" runat="server" AutoPostBack =true OnSelectedIndexChanged ="ddlCompany_SelectedIndexChanged" >
      </asp:DropDownList></td>
  <td> 批次:<asp:DropDownList ID="ddlPatch" runat="server" AutoPostBack =true OnSelectedIndexChanged ="ddlPatch_SelectedIndexChanged" > </asp:DropDownList></td>
  
  <td>金额:<asp:TextBox ID="txtFrom" runat="server" Width =50></asp:TextBox>To <asp:TextBox ID="txtTo" Width =50 runat="server"></asp:TextBox>
      </td>
       
 <td>已分配给:<asp:DropDownList ID="ddlCompanyUsers" runat="server"  >
      </asp:DropDownList></td>
  <td>姓名:<asp:TextBox ID="txtName" runat="server" Width =50></asp:TextBox><asp:LinkButton ID="btnGo" runat="server" OnClick="btnGoxxx_Click" Width ="10">Go</asp:LinkButton></td>
  
    
   <tr /> <tr><td align =left >记录总数:<b ><span style="color:red" id='spanTotal' runat =server /></b> 总金额:<span style="color:red" id='spanToNum' runat =server /></td><td><uc1:PagingControl id="PagingControl1" runat="server">
                            </uc1:PagingControl></td><td>已选中：<span style="color:red" id='spantotalRow'></span> 行   金额共计： <span id='spantotalNum' style="color:red"></span></td><td align =right  valign =middle class="menu" >
                            <span  style ="cursor:hand; color:blue" onclick="SignUser('UserSelectTosign.aspx')" >
                              委派用户</span>  |<span  style ="cursor:hand; color:blue" onclick="SignUser('UserSelectReadUser.aspx')" >
                              委派只读用户</span>
                             
                            <tr>
                            <td  colspan=8 >
                             <asp:GridView  Width =100% ID="GridView1" runat="server" OnRowDataBound="GridView1_RowDataBound" 
        AlternatingRowStyle-CssClass="dg_alter" RowStyle-CssClass="dg_item" HeaderStyle-CssClass="dg_header"  AutoGenerateColumns =false    >
             
         <Columns>
             <asp:TemplateField ItemStyle-Width=10 HeaderText="<input type=checkbox onclick ='allCheck1(this)'/>" >
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server"  />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="ID" HeaderText ="ID"/>
             <asp:BoundField DataField="tbName" HeaderText ="姓名" />
            <asp:BoundField DataField="tbKey" HeaderText ="帐号" />
         
            <asp:BoundField DataField="tbBalance" HeaderText ="余额" />
             <asp:BoundField DataField="OwnerID" HeaderText ="已委派" />
            

  <asp:BoundField DataField="ID" HeaderText ="只读用户"/>
                
        </Columns>

        </asp:GridView>
                            </td>
                            </tr>
    </table>
   
       <asp:Button ID="btnRefresh" runat="server"  style=" display:none"  OnClick="btnRefresh_Click" />
        <asp:Button ID="btnAssign" runat="server"  style=" display:none"  OnClick="btnAssign_Click" />
 <asp:TextBox  ID="txtSignUserID" style=" display:none" runat="server"  Text =0 ></asp:TextBox>
  <asp:Button ID="btnAssignReadUser" runat="server"  style=" display:none"  OnClick="btnAssignReadUser_Click" />

   
    </form>
</body>
</html>
