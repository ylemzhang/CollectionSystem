<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Left.aspx.cs" Inherits="Left" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="css/thread.css" rel="stylesheet" rev="stylesheet" type="text/css" />
    <link href="css/common.css" rel="stylesheet" rev="stylesheet" type="text/css" />
    <script src="javascript/jquery.js"></script>
    <script>


        function refreshPage() {


            document.location.href = document.location.href;
        }
        function showUrl(url) {

            parent.window.right.document.location.href = url;
        }



    </script>
</head>
<body scroll="auto">
    <div>
        <table width="100%" cellspacing="0">
            <tr height="10" bgcolor="#F7F7F7">
                <td align="center" valign="top">
                    <b>主页&nbsp;&nbsp;&nbsp;</b>
                </td>
        </table>
    </div>
    <div id="main">
        <div class="left">
            <div class="l1" id="display">
                <div class="m2">
                    <ul>
                        <li id="menuGroup98" style="cursor: pointer;">
                            <img onclick="displayMenu(98)" id="groupimg98" runat="server" 
                                src="images/t_list_09.jpg" style="cursor: pointer; height: 9px;" />
                            <a href="javascript:showUrl('Announcementlist.aspx')">
                                <%=Common.StrTable.GetStr("announcement")%>
                            </a></li>                       
                        <li pid="98" class="w2" name="menuForum99" id="menuForum99" style="display: none;"><a
                            href="javascript:showUrl('Announcementlist.aspx?type=outofdate')">
                            <%=Common.StrTable.GetStr("expriedannouncement")%>
                        </a></li>
                        <li id="menuGroup88" style="cursor: pointer;">
                            <img onclick="displayMenu(88)" id="groupimg88" src="images/t_list_09.jpg" style="cursor: pointer;" />
                            <a href="javascript:showUrl('MessageFrm.aspx?type=inbox')">
                                <%=Common.StrTable.GetStr("mymessage")%>
                            </a></li>
                        <li pid="88" class="w1" name="menuForum25" id="menuForum25" style="display: none;"><a
                            href="javascript:showUrl('MessageFrm.aspx?type=inbox')">
                            <%=Common.StrTable.GetStr("inbox")%>
                        </a></li>
                        <li pid="88" class="w2" name="menuForum47" id="menuForum47" style="display: none;"><a
                            href="javascript:showUrl('MessageFrm.aspx?type=send')">
                            <%=Common.StrTable.GetStr("senditems")%>
                        </a></li>
                        <li id="menuGroup86" style="cursor: pointer;">
                            <img onclick="displayMenu(86)" id="groupimg86" src="images/t_list_09.jpg" style="cursor: pointer;" />
                            <a href="javascript:showUrl('UserProfile.aspx')">
                                <%=Common.StrTable.GetStr("myprofile")%>
                            </a></li>
                        <li pid="86" class="w1" name="menuForum74" id="menuForum74" style="display: none;"><a
                            href="javascript:showUrl('UserProfile.aspx')">基本信息</a></li>
                        <li pid="86" class="w2" name="menuForum74" id="Li1" style="display: none;"><a href="javascript:showUrl('LeaveManagement.aspx')">
                            我的考勤</a></li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
