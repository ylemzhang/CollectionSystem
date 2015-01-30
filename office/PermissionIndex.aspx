<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PermissionIndex.aspx.cs" Inherits="Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            margin-left: auto;
            height:50px;
            background:#f00;
            width:200px;
            vertical-align:middle;
            line-height:60px;
            text-align:center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <script type="text/javascript">
//     alert("PermissionIndex:" + top.location);
//     alert("PermissionIndex:" + self.location);

     if (top.location != self.location) {
         top.location = self.location;
     }
    </script>
<div style=" width:100%; height:65px; background-color:#DDDDDD; " 
        class="style1">
    <strong>权限管理</strong>
<a href="Default.aspx" >返回</a></div>
<div style=" width:100%;">
<div class="left" style=" width:15%; height:100%; float:left;background-color:#A2A2A2;"><ul>
    <li><a href="UserGroupManager.aspx" target="BodyFrame">权限组管理</a></li>
    <li><a href="ModuleManager.aspx" target="BodyFrame">模块管理</a></li>
    <li><a href="ManagerUser.aspx" target="BodyFrame">用户管理</a></li>
  </ul></div>
<div class="content" style="width:85%;height:600px;float:left;">
  <iframe name="BodyFrame" id="BodyFrame" class="BodyFrame" frameborder="1" scrolling="auto"
                    width="100%" height="100%" src="UserGroupManager.aspx"></iframe>
</div>
</div>

</asp:Content>

