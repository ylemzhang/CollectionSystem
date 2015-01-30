<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CaseList.aspx.cs" Inherits="CaseList" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
      <link href="CSS/css.css" type="text/css" rel="stylesheet">
      <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
      <script type="text/javascript" src="javascript/jquery.js" ></script>
      <script type="text/javascript" src="javascript/common.js" ></script>
      <script>
      var totalSelected=0;
      var totalNum=0;
        function allCheck(chk)  
        {  
        totalNum =0;
            totalSelected=0;
            for (var i=0;i<form1.elements.length;i++)  
            {  
                var e=form1.elements[i];  
                if (e.type=='checkbox') 
                { 
                e.checked=chk.checked;
               
                }  
            }
             if(chk.checked)
            {
            totalNum =document.all.spanToNum.innerText;
          totalSelected=document.all.spanTotal.innerText;
            } 
      
            
            document.getElementById("spantotalRow").innerText=totalSelected;
document.getElementById("spantotalNum").innerText=totalNum;
            
            
        }  
        function  isSelect()
        {

            var thisfrm=document.all.form1;
        
           for (i=0; i<thisfrm.length; i++) 
           {

              if (thisfrm.elements[i].name.indexOf('CheckBox1') !=-1)
               {

                  if(thisfrm.elements[i].checked)
                   {

                    return true;

                  }

              }

           }
          return false; 

}
      function addClass()
      {
         checked=isSelect();
         if (checked)
           {
                var obj = new Object();
   
                var str=window.showModalDialog("selectClass.aspx",obj,"dialogWidth=500px;dialogHeight=200px;toolbar=no;titlebar=no;help=no;resizable=no;status=no;scroll=yes");

                if(str!=null && str!='') 
                {
              
                    document.getElementById("txtClassID").value=str;
                   document.all.btnAddClass.click();
                    window.alert('加入成功');
                   
                }

             
           }
           else
           {
           window.alert('请选择记录');
          
           }
      }
      
      function check()
      {
        checked=isSelect();
        if (checked)
           {
               return confirm ('你真要做此操作？');
             
           }
           else
           {
           window.alert('请选择记录');
           return false;
           }
      }
      
      function sortList(field)
    {
  
        document.all.txtSort.value=field;
        document.all.btnSort.click();
     
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
         
         function ShowTime()
         {
         document.all.btnShowTime.click();
         } 
            
            
      </script>
</head>
<body onload="allUnSelect()" >
    <form id="form1" runat="server">
      <table width =100%  cellspacing=0   >
                     <tr height =10 bgcolor=#F7F7F7> <td align =left valign =top class="menu" width =60 >
                      案件列表
                     </td><td align =left >记录总数:<b ><span style="color:red" id='spanTotal' runat =server /></b> 总金额:<span style="color:red" id='spanToNum' runat =server /></td><td width =500 ><span runat =server id=spanApply style ="display:none"><asp:DropDownList ID="ddlApp" runat="server"  Width =85px   /><asp:Button ID="btnApply" runat="server"   OnClientClick="return check()" OnClick="btnApply_Click"  Text ="申请"/></span><td  > <asp:LinkButton ID="linkDeleteHelp" style ="cursor:hand; color:blue; display:none"  runat="server" OnClientClick="return check()" OnClick="DeleteHelpuser_Click">移除协作案件</asp:LinkButton></td><td><asp:LinkButton ID="btnRemoveFromClass" style ="cursor:hand; color:blue; display:none"  runat="server" OnClientClick="return check()" OnClick="RemoveFromClass_Click">从此类别中移除</asp:LinkButton> </td><td><span  style =" cursor:hand;color:Blue" onclick="addClass()"> 加入到类别</span></td></tr>
                         </table>
                         
                         <div id=divSearch runat=server  >
                          <table width =100%>
  <tr>
  <td width =120>公司<asp:DropDownList ID="ddlCompany" runat="server" Height =19px Width =85px AutoPostBack =true OnSelectedIndexChanged ="ddlCompany_SelectedIndexChanged" >
      </asp:DropDownList></td>
 
<td width =120>批次<asp:DropDownList ID="ddlPatch" runat="server" Height =19px  > </asp:DropDownList></td>
<td width =140>业务员<asp:DropDownList ID="ddlUser" runat="server" Height =19px Width =85px  > </asp:DropDownList></td>
<td width =140><span id=spanCaseType runat=server style ="display:none">
 案件类型:<asp:DropDownList ID="ddlCaseType" runat="server" Height =19px Width =85px  AutoPostBack =true OnSelectedIndexChanged ="ddlCaseType_SelectedIndexChanged" >
      </asp:DropDownList></span></td>
  <td width =200>合同金额<asp:TextBox ID="txtFrom" runat="server" Width =40 Height =15px></asp:TextBox>到<asp:TextBox ID="txtTo" Width =40 runat="server" Height =15px></asp:TextBox>
     
        &nbsp;&nbsp;  <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">Go</asp:LinkButton></td><td>已选中：<span style="color:red" id='spantotalRow'></span> 行   金额共计： <span id='spantotalNum' style="color:red"></span></td>
  </tr>
  </table>
                         </div>
    <div>
     <asp:GridView  Width =100% ID="GridView1" runat="server" OnRowDataBound="GridView1_RowDataBound" 
        HeaderStyle-CssClass="dg_header"  AutoGenerateColumns =false    >
             
         <Columns>
    <asp:TemplateField ItemStyle-Width=10 HeaderText="<input type=checkbox onclick ='allCheck(this)' />">
          
                <ItemTemplate>
               <%-- <input type=checkbox  onclick="checkSelect(this,'<%# databinder.eval(container, "tbBalance") %>')" runat =server ID="CheckBox1" />--%>
                <asp:CheckBox ID="CheckBox1" runat="server"  />
                </ItemTemplate>
            </asp:TemplateField>
          <asp:BoundField DataField="OwnerID" HeaderText ="业务员" ItemStyle-Width=80/>
           <asp:BoundField DataField="CompanyID" HeaderText ="公司" ItemStyle-Width=80/>
         
    
             <asp:BoundField DataField="tbName" HeaderText ="姓名" ItemStyle-Width=80/>
            
           <asp:BoundField DataField="PatchName" HeaderText ="批号" />
            
              <asp:BoundField DataField="tbKey" HeaderText ="帐号/合同号" />
               
                 
 <asp:BoundField DataField="tbBalance" HeaderText ="合同金额" />

              
                  <asp:BoundField DataField="ID" HeaderText ="id"  ItemStyle-Width=80/>
                  <asp:BoundField DataField="CompanyID" HeaderText ="id"  ItemStyle-Width=80/>
                  <asp:BoundField DataField="notetime" HeaderText ="id"  ItemStyle-Width=80/>
                  
                  <asp:BoundField DataField="tbIdentityNo" HeaderText ="身份证" />
                   <asp:TemplateField ItemStyle-Width=80 HeaderText="<input type=button value='看时间' onclick ='ShowTime();' />">
          
                <ItemTemplate>
              
                <asp:Label ID="labelTime" runat="server"  />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    

        </asp:GridView>
    </div>
     <asp:TextBox  ID="txtClassID" style=" display:none" runat="server"  Text =0 ></asp:TextBox>
  <asp:Button ID="btnAddClass" runat="server"  style=" display:none"  OnClick="btnAddClass_Click" />
  <asp:Button ID="btnSort" runat="server" Text="sort" style=" display:none " OnClick="btnSort_Click"  /> 
  <asp:Button ID="btnShowTime" runat="server" Text="sort" style=" display:none " OnClick="btnShowTime_Click"  /> 
       <asp:TextBox ID="txtSort" runat="server" style="display:none " Text ="ID"></asp:TextBox>
       

    </form>
</body>
</html>
