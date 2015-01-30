<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CaseDetail.aspx.cs" Inherits="CaseDetail"  ValidateRequest="false"  MaintainScrollPositionOnPostback ="true"  %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>案件详细信息</title>
     <link href="CSS/css.css" type="text/css" rel="stylesheet">
      <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
      <script type="text/javascript" src="javascript/jquery.js" ></script>
      <script type="text/javascript" src="javascript/common.js" ></script>
     <script>
 
   function EditNote(id)
   {
   window.open("NoteEdit.aspx?id="+id);
   }
   
   function EditTelephone(id,phone,companyID,caseID)
   {
   window.open("NoteEdit1.aspx?id="+id+"&phone="+phone+"&companyID="+companyID+"&caseID="+caseID);
   }

   
   function refreshPage()
{

   document.location.href=document.location.href;
       
}

 function checkPromise()
    {
    
   
     if (document.all.txtDate.value.Trim()=='')
    {
     alert(" 承诺日期<%=Common.StrTable.GetStr("shallnotbenull") %>");
    return false;
    }
     
      if (!isNum(document.all.txtPromisedPay.value.Trim()))
        {
         alert("承诺金额不是数字");
        return false;
        }
    return true;
    }
    
     function checkFollow()
    {
    
   
     if (document.all.txtfollowDate.value.Trim()=='')
    {
     alert(" 跟进日期<%=Common.StrTable.GetStr("shallnotbenull") %>");
    return false;
    }
     
   
    return true;
    }
    
      function checkComment()
    {
    
   
     if (document.all.txtComment.value.Trim()=='')
    {
     alert(" 意见<%=Common.StrTable.GetStr("shallnotbenull") %>");
    return false;
    }
     
   
    return true;
    }
    
       function ShowComment()
    {
       window.open('CommentList.aspx?caseID=<%=CaseID %>','commentlist');
    }
    
    
     function checknote()
    {
    
   
     if (document.all.txtNote.value.Trim()=='')
    {
     alert(" 备注<%=Common.StrTable.GetStr("shallnotbenull") %>");
    return false;
    }
    
     if (document.all.txtType.value =="1" )
    {
        if (document.all.TextBox1.value.Trim()=='')
        {
         alert("请输入电话号码");
        return false;
        }
    }
    
    
        if (document.all.txtType.value =="2" )
    {
        if (!isNum(document.all.TextBox1.value.Trim()))
        {
         alert("路费不是数字");
        return false;
        }
    }    
    return true;
    }
    

 function openHtm(url)
 {

window.open(url);
 }
 
 function showDiv(flag)
 {
 document.all.txtType.value=flag;
 if (top.window.head!=null)
  {
      top.window.head.document.all.txtType.value=flag;
      }
       if(flag==0)
     {
     document.all.divpayment.style.display='none';
      document.all.divSameRecord.style.display='none';
      document.all.divNote.style.display='block';
      document.all.tddivHistory.style.display='block';
       document.all.tr1.style.display='none';
    
      document.all.span0.innerText="备注";
      
    document.all.divHistory.innerHTML=document.all.txtNote0.value;
     document.all.tddivNoteHistory.style.display='none';
     //document.all.trMoreinfo.style.display='none';
     
      
     }
     else if (flag==1)
     {
       document.all.divpayment.style.display='none';
        document.all.divSameRecord.style.display='none';
        //document.all.tddivHistory.style.display='none';
        document.all.tddivNoteHistory.style.display='none';
        
      document.all.divNote.style.display='block';
        document.all.tr1.style.display='block';
      
         document.all.span1.innerText="电话:";
         document.all.span0.innerText="添加访问记录";
         
//     document.all.divHistory.innerHTML=document.all.txtNote1.value;
//     
//      document.all.divNoteHistory.style.display='block';
//     document.all.divNoteHistory.innerHTML=document.all.txtNote0.value;
     
     //document.all.trMoreinfo.style.display='block';
        
     }
     else if (flag==2)
     {
       document.all.divpayment.style.display='none';
        document.all.divSameRecord.style.display='none';
        document.all.tddivNoteHistory.style.display='none';
        document.all.tddivHistory.style.display='block';
      document.all.divNote.style.display='block';
         document.all.tr1.style.display='block';
       document.all.span1.innerText="路费:";
       document.all.span0.innerText="拜访记录";
       document.all.divHistory.innerHTML=document.all.txtNote2.value;
       
       
//         document.all.tddivNoteHistory.style.display='block';
//     document.all.divNoteHistory.innerHTML=document.all.txtNote0.value;
     
     //document.all.trMoreinfo.style.display='block';
     }
     else if (flag==3)
     {
       document.all.divpayment.style.display='block';
      document.all.divNote.style.display='none';
       document.all.divSameRecord.style.display='none';
     }
     
      else if (flag==4)
     {
       document.all.divSameRecord.style.display='block';
      document.all.divNote.style.display='none';
       document.all.divpayment.style.display='none';
     }
 }






  function SignUser()
{  

     var obj = new Object();
    obj.name="ttedit";
    var str=window.showModalDialog("UserSelectTosign.aspx?companyID=<%= CompanyID%>",obj,"dialogWidth=700px;dialogHeight=800px;toolbar=no;titlebar=no;help=no;resizable=yes;status=no;scroll=yes");

    if(str!=null && str!='') 
    {
  
        document.getElementById("txtSignUserID").value=str;
         document.all.btnAssign.click();
       
    }
    else
    {
     alert("没有委派给任何用户");
    }
   
   }
   
 function  SignHelpUser()
 {
 OpenWindow("UserSelectHelpUser.aspx?companyID=<%= CompanyID%>&caseID=<%=CaseID %>",700,800);
 }
   
function showDefaultDiv()
{
var flag;
if (top.window.head==null)
{

flag=document.all.txtType.value;
}
else
{

flag= top.window.head.document.all.txtType.value;
}

showDiv(flag)
}



      function addClass()
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
      
      
      function openApp()
      {
      var apptype=document.all.ddlApp.selectedIndex;
      url="MessageSend.aspx?companyID=<%= CompanyID%>&caseID=<%=CaseID %>&appType="+apptype;
OpenWindow(url,800,520);
      //OpenWindow("UserSelectHelpUser.aspx?companyID=<%= CompanyID%>&caseID=<%=CaseID %>",700,800);
      }
 </script>
</head>
<body bgcolor=#F7F7F7 onload ="showDefaultDiv()" >
    <form id="form1" runat="server" >
    <div runat ="server" id="divAll">
    <div  style =" width:100% ">
   <table  width =100%  border =1 >
     <tr><%--onclick ="openHtm('<%=HtmlUrl%>' )--%>
    <td colspan ="2"  width="200px"><span runat =server id="spanNavagator"><asp:DropDownList ID="ddlCaseList" runat="server" Height =19px Width =85px  AutoPostBack =true OnSelectedIndexChanged="ddlCaseList_SelectedIndexChanged"  >
      </asp:DropDownList> &nbsp;&nbsp; <asp:LinkButton  ID="lnkPre" runat="server" OnClick="lnkPre_Click">[上一个]</asp:LinkButton>
       &nbsp;&nbsp; <asp:LinkButton  ID="lnkNext" runat="server" OnClick="lnkNext_Click">[下一个]</asp:LinkButton></span>
       <span  runat =server id="spanName"></span>
      </td>
    <td width="264px">案件类型:<asp:DropDownList ID="ddlCaseType" runat="server" Height =19px Width =85px  >
      </asp:DropDownList><asp:Button ID="btnCaseType" runat="server" Text="保存"  OnClick="btnCaseType_Click" /></td>
      <td id="trapp" runat="server">
        <asp:DropDownList ID="ddlApp" runat="server" Height =19px Width =85px   />
        <input type =button value ="申请"  onclick ="openApp()"/></td>
    <td colspan ="4" align =right>

    <span  id="spnShowCaseBase" style =" cursor:hand;color:Blue" runat =server ><b> 显示基本信息</b></span>
    &nbsp;&nbsp;案件已分配给：&nbsp;&nbsp;<asp:Label runat =server ID="lblSignUser"></asp:Label>&nbsp;&nbsp;
<span  id="spanSignUser" style =" cursor:hand;color:Blue" runat =server onclick="SignUser()"><b> 案件委派</b></span>|
  <span  id="spanSignHelper" style =" cursor:hand;color:Blue" runat =server onclick="SignHelpUser()"><b> 协助人委派</b></span>|
  <span  style =" cursor:hand;color:Blue" onclick="addClass()"><b> 加入到类别</b></span>
    </td>
    </tr>
    <tr>
    <td><b>案件批次</b></td><td><%=PatchName%></td><td><b>最新余额</b></td><td><%=Balance%></td><td><b>过期日期</b></td><td><%=Expiredate%></td><td><b>导入日期</b></td><td><%=Importtime%></td>
    </tr>
    </table>
    </div>
    <div>
    <table width ="100%" border ="1" style=" height:652px" >
        <tr><td id="divCaseBase" style="width:45%"> 
        <div style=" height:100%;overflow:scroll">
        <table>
            <td><%=HtmlContent%></td>
        </table>
        </div>
        </td>
        <td  style="width:50%">
             <div style=" height:100%">
             <div>
     <table id="Table1" width =100% border =1 runat=server>
     <tr>
    <td ><font color="#cc6600" ><b> 承诺还款日期</b></font>
<asp:TextBox ID="txtDate" runat="server" Width =100></asp:TextBox><img src="Images/Calendar.gif"  style=" cursor:hand "  onclick ="ShowCalenderWindow('txtDate')"/>
<font color="#cc6600" ><b> 承诺金额</b></font>
<asp:TextBox ID="txtPromisedPay" runat="server" Width =72px /> 


<asp:Button ID="btnPromised" runat="server" Text="保存"  OnClientClick="return checkPromise();" OnClick="btnPromised_Click" />
   
    </td>
    <td>
    <font color="#cc6600" ><b> 跟进日期</b></font>
<asp:TextBox ID="txtfollowDate" runat="server" Width =100></asp:TextBox><img src="Images/Calendar.gif"  style=" cursor:hand "  onclick ="ShowCalenderWindow('txtfollowDate')"/>


<asp:Button ID="btnFollow" runat="server" Text="保存"   OnClientClick="return checkFollow();"  OnClick="btnFollow_Click" />
    </td>
    
  
    </tr>
  <tr>
    <td colspan =2>
    <font color="#cc6600" ><b> 主管意见</b></font>
<asp:TextBox ID="txtComment" TextMode="MultiLine" Rows="2" runat="server" Width =500px></asp:TextBox><asp:Label runat =server ID="lblPerson"></asp:Label>

<asp:Button ID="btnComment" runat="server" Text="保存" OnClientClick="return checkComment();" OnClick="btnComment_Click" />
<span  id="span2" style =" cursor:hand;color:Blue" runat =server onclick="ShowComment()"><b> 查看历史记录</b></span>
    </td>
  </tr> 
    </table>
    </div>
    <font color="#cc6600"><b> 电话访问记录</b></font>
    <div id="divTel" style=" height:330px;overflow:scroll">   
     <asp:GridView  Width ="100%" ID="gvTel"  runat="server" 
        HeaderStyle-CssClass="dg_header"  AutoGenerateColumns ="false"    >
             
         <Columns>        
<asp:BoundField DataField="CreateOn" HeaderText ="联系时间"  ItemStyle-Width="150px"/>  
<asp:BoundField DataField="CreateBy" HeaderText ="业务员"  ItemStyle-Width="150px"/>        
<asp:BoundField DataField="Str1" HeaderText ="电话"  ItemStyle-Width="150px"/>
 <asp:BoundField DataField="Body" HeaderText ="描述"  />             
                
        </Columns>
    

        </asp:GridView>
    </div>
    <div style="padding-left:5px; height:20px; vertical-align:middle"><font color="#cc6600" ><b > 
        <span  style =" cursor:hand;color:Blue" onclick="showDiv(1)"> <b>添加访问记录</b></span>|
        <span  style =" cursor:hand;color:Blue" onclick="showDiv(0)"> <b>备注</b></span>|    
    <span  style =" cursor:hand;color:Blue" onclick="showDiv(2)"><b> 拜访记录</b></span>|
     <span  style =" cursor:hand;color:Blue" onclick="showDiv(3)"><b> 还款记录</b></span>|
      <span  style =" cursor:hand;color:Blue" onclick="showDiv(4)"><b> 重复记录</b></span>
      
    
    </b></font></div>
    
    <div id="divpayment" style ="display:none">
     <asp:GridView  Width =100% ID="GridView1" runat="server" OnRowDataBound="GridView1_RowDataBound" 
        HeaderStyle-CssClass="dg_header"  AutoGenerateColumns =false     >
             
          <Columns>
            
    
             <asp:BoundField DataField="tbName" HeaderText ="姓名" ItemStyle-Width=80/>
            
           
            
              <asp:BoundField DataField="tbKey" HeaderText ="帐号/合同号" />
               <asp:BoundField DataField="tbPayment" HeaderText ="还款金额" />
                 <asp:BoundField DataField="tbPayDate" HeaderText ="还款日期"  ItemStyle-Width=80/>
 <asp:BoundField DataField="tbBalance" HeaderText ="最新金额" />

                 <asp:BoundField DataField="ImportDate" HeaderText ="导入日期"  ItemStyle-Width=80/>
                 
                 
        </Columns>
       
      <EmptyDataTemplate>
      无记录
      </EmptyDataTemplate>
     

        </asp:GridView>
    </div>
    
     <div id="divSameRecord" style ="display:none">
     <table width =100% border=1><tr><td width=350px> <asp:RadioButtonList ID="RadioButtonList1" RepeatDirection=Horizontal
            runat="server"  >
                </asp:RadioButtonList></td><td width =50%><asp:Button ID="btnGo" runat="server" OnClick="btnGoxxx_Click" Text ="查找" /></td></tr>
                
                <tr>
                <td colspan =2>
                  <asp:GridView  Width =100% ID="GridView2" runat="server" OnRowDataBound="GridView2_RowDataBound" 
        AlternatingRowStyle-CssClass="dg_alter" RowStyle-CssClass="dg_item" HeaderStyle-CssClass="dg_header"  AutoGenerateColumns =false    >
             
         <Columns>
           
            <asp:BoundField DataField="ID" HeaderText ="ID"/>
             <asp:BoundField DataField="CompanyID" HeaderText ="公司" ItemStyle-Width=80/>
             <asp:BoundField DataField="PatchID" HeaderText ="批号" ItemStyle-Width=100/>
             <asp:BoundField DataField="tbName" HeaderText ="姓名" ItemStyle-Width=50/>
            <asp:BoundField DataField="tbKey" HeaderText ="帐号" />
         
            <asp:BoundField DataField="tbBalance" HeaderText ="余额" />
             <asp:BoundField DataField="OwnerID" HeaderText ="业务员" />
              <asp:BoundField DataField="tbMobile" HeaderText ="手机" />
                <asp:BoundField DataField="tbIdentityNo" HeaderText ="身份证号" />

<asp:BoundField DataField="CompanyID" HeaderText ="公司" ItemStyle-Width=80/>
                
        </Columns>
  
        </asp:GridView>
                </td>
                </tr>
     </table>
     
   
    </div>
    
    <div id="divNote" style=" height:186px; overflow:auto ">
    <table width =100%   border =1>
    <tr>
    <td id="tddivHistory">
    <div style =" height:170px; overflow:scroll;width:240px;"  runat ="server" id="divHistory">

    </div>
    </td>
  
   <td id="tddivNoteHistory">
    <div style =" height:100%; overflow:scroll;width:240px;"  runat ="server" id="divNoteHistory">

    </div>
    </td>
    
     <td  align="right">
<asp:TextBox ID="txtNote" TextMode="MultiLine"  runat="server" Width="180px" Height ="170px"></asp:TextBox>
   
    </td>
    <td width="150px" valign=top>
    <table>
      <tr ><td colspan =2><span id="span0" runat =server>备注</span></td></tr>
    <tr id="tr1" style ="display:none" runat =server><td><span id=span1 runat =server >备注</span></td><td><asp:TextBox ID="TextBox1" runat="server" Width =100></asp:TextBox></td></tr>
     <tr id="trMoreinfo" style="display:none">
         <td colspan =2><table width =100%>
         <tr id="tr2"  runat =server><td>联系对象</td><td><asp:DropDownList ID="ddlcontactorType" runat="server" Height =19px Width =85px    /></td></tr>
          <tr id="tr3"  runat =server><td>联系对象姓名</td><td> <asp:TextBox ID="txtContactor" runat="server" Width =100 /></td></tr>
           <tr id="tr4"  runat =server><td>是否可联</td><td><asp:DropDownList ID="ddlcontractResult" runat="server" Height =19px Width =85px   /></td></tr>

         </table>
     </td>
     </tr> 
     
  
    
     <tr><td  style="height: 24px"><asp:Button ID="btnSaveNote" runat="server" Text="保存"  OnClientClick="return checknote();" OnClick="btnSaveNote_Click" /></td><td  style="height: 24px"><%--<input type=button value="新增电话访问" onclick="window.open('NoteEdit.aspx?id=&phone=&companyID=<%= CompanyID%>&caseID=<%=CaseID %>');" />--%></td></tr>
    </table>
    
    </td>
    
    </tr></table>
   
    </div>
    </div>
        </td>
        </tr>   
    </table>
    </div>   
    <asp:TextBox ID="txtType" style=" display:none" runat="server" Text =1 ></asp:TextBox>
    <asp:TextBox ID="txtNote1" style=" display:none" runat="server"   Text =0 ></asp:TextBox>
    <asp:TextBox ID="txtNote2" style=" display:none" runat="server"    Text =0 ></asp:TextBox>
    <asp:TextBox ID="txtNote0" style=" display:none" runat="server"    Text =0 ></asp:TextBox>
     <asp:Button ID="btnAssign" runat="server"  style=" display:none"  OnClick="btnAssign_Click" />
      <asp:TextBox ID= "txtSignUserID" style=" display:none" runat="server"  Text =0 ></asp:TextBox>
      
       <asp:TextBox  ID="txtClassID" style=" display:none" runat="server"  Text =0 ></asp:TextBox>
  <asp:Button ID="btnAddClass" runat="server"  style=" display:none"  OnClick="btnAddClass_Click" />
     
    </div>
    </form>
    <script type="text/javascript">

        $(function () {

            $("#divCaseBase").show();
            $("#spnShowCaseBase b").html("隐藏基本信息");
            $("#spnShowCaseBase").toggle(
              function () {
                  $("#tddAddress").hide();
                  $("#divCaseBase").hide();
                  $("#spnShowCaseBase b").html("显示基本信息");
              },
              function () {
                  $("#divCaseBase").show();
                  $("#spnShowCaseBase b").html("隐藏基本信息");
              }
            );

            $("span[name='spantelephone']").click(function () {
                var tel = $(this)[0].outerText;
                if (tel) {
                    var index = tel.indexOf("\("); //substr
                    if (index > 0) {
                        tel = tel.substr(0, index);
                    }
                }
                $("#TextBox1").val(tel);
            });



        });
    
    </script>
</body>
</html>
