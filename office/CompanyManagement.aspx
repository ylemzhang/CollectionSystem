﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CompanyManagement.aspx.cs" Inherits="CompanyManagement" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title><%=Common.StrTable.GetStr("webname") %></title>
     <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
</head>
<frameset rows="260,*" frameborder=no  >
<frame   src="CompanyList.aspx" id="head"  name = "head" frameborder=no noresize=noresize/>

<frame src ="CompanyEdit.aspx?id=" id="mainFra" />

</frameset>

</html>