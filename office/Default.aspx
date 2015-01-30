<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title><%=Common.StrTable.GetStr("webname") %></title>
     <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
</head>
<frameset rows="70,*" frameborder=no id="frmIndex" >
<frame   src="top.aspx" id="head"  name = "head" frameborder=no noresize=noresize/>

<frame src ="Mypage.aspx" id="mainFra" />

</frameset>

</html>
