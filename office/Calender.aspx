<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Calender.aspx.cs" Inherits="Pages_Calender" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title><%=Common.StrTable.GetStr("selectdate")%></title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <base target =_self></base>
</head>
<body topmargin="0px" bottommargin ="0px" leftmargin ="0px" rightmargin ="0px">
    <form id="form1" runat="server">
    <div>
        <asp:Calendar ID="CalendarSelect" runat="server" BackColor="#E0E0E0"  BorderColor="Gray"   Width =100% Height =100%
            BorderStyle="Outset" BorderWidth="2px"  OnSelectionChanged="Calendar1_SelectionChanged"
            >
            <SelectedDayStyle BackColor="#C0FFC0" />
            <OtherMonthDayStyle BackColor="Gray" />
        </asp:Calendar>
    
    </div>
    </form>
</body>
</html>
