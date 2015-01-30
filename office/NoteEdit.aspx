<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NoteEdit.aspx.cs" Inherits="NoteEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>编辑</title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<script type="text/javascript" src="javascript/jquery.js" ></script>
<script type="text/javascript" src="javascript/common.js" ></script>
<script>
 function checknote()
    {
    
   

    
     if (document.all.txtType.value =="1" )
    {
        if (document.all.TextBox1.value.Trim()=='')
        {
         alert("请输入电话号码");
        return false;
        }
    }
    
    
    else    if (document.all.txtType.value =="2" )
    {
        if (!isNum(document.all.TextBox1.value.Trim()))
        {
         alert("路费不是数字");
        return false;
        }
    }
    else  if (document.all.txtNote.value.Trim()=='')
    {
     alert(" 备注<%=Common.StrTable.GetStr("shallnotbenull") %>");
    return false;
    }
    return true;
    }
</script>

</head>
<body>
    <form id="form1" runat="server">
     <div runat =server id="moreInfo">
     <asp:Label ID="lblTitle" runat =server  ></asp:Label>
        <asp:TextBox ID="TextBox1" runat="server"  Width="200px"></asp:TextBox>
        <br />
   
     <table width =60%>
       <tr id="tr2"  runat =server><td><span id=span3 runat =server >联系对象</span></td><td align =left><asp:DropDownList ID="ddlcontactorType" runat="server" Height =19px Width =85px    /></td></tr>
      <tr id="tr3"  runat =server><td><span id=span4 runat =server >联系对象姓名</span></td><td align =left> <asp:TextBox ID="txtContactor" runat="server" Width =100 /></td></tr>
       <tr id="tr4"  runat =server><td><span id=span5 runat =server >是否可联</span></td><td align =left><asp:DropDownList ID="ddlcontractResult" runat="server" Height =19px Width =85px   /></td></tr>
     </table>
     </div>
    <div runat =server id="divTeleType" style="display:none">
     结果:&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddlTeleType" runat="server" Height =19px Width =85px  >
      </asp:DropDownList>
    </div>
     <br />
    <div>
        <asp:TextBox ID="txtNote" runat="server" Height="233px" TextMode="MultiLine" Width="700px"></asp:TextBox>
    </div>
    
    <div >
       <asp:Button ID="Button1" runat="server" Text="Save" OnClientClick="return checknote();"  OnClick="Button1_Click" /></div>
        
          <asp:TextBox ID="txtType" style=" display:none" runat="server" Text =1 ></asp:TextBox>
    </form>
</body>
</html>
