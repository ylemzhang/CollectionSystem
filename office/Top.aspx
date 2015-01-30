<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Top.aspx.cs" Inherits="Top" %>

<html>
<head>
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
    <link href="CSS/css.css" type="text/css" rel="stylesheet">
    <script type="text/javascript" src="javascript/jquery.js"></script>
    <script language="javascript" src="Javascript/common.js"></script>
    <script type="text/javascript">  	  
    
    var pop;
  	var havealert;
	 var xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
		var current=0;
		 window.onload=function(){
            PostOrder();
            AlertPunchin();
        }
		
		function AlertPunchin()
		{
	        if (document.all.spanleave.innerText=="[上班打卡]")
	        {
	         alert('请打上班卡');
	        }
		}
		
		function PostOrder()
		{
	
            today = new Date();
            intHours = today.getHours();
            intMinutes = today.getMinutes();

            if (intHours==<%=leaveHour %> && intMinutes==<%=leaveMinute %>)
            {
                 if( !havealert)
                 {
                  alert('请打下班卡');
                  havealert=true;
                 }
            }
		    xmlhttp.Open("GET", "MessageRefresh.aspx", true); 
		    xmlhttp.onreadystatechange= HandleStateChange;
	        xmlhttp.Send();    	
            window.setTimeout(PostOrder,20000);
		
		}
		
		function HandleStateChange()
		{
		    if (xmlhttp.readyState == 4)
		    {

			    var rst=xmlhttp.responseText;
			    
			        var strs=rst.split("|");
                          var emailCount=strs[0];
                         var commentCount=strs[1];
                          
			    if (emailCount>current || commentCount>0)
			    {
			    OpenPopup(emailCount,commentCount);
			    }
    		    current=emailCount;
                if(emailCount!="0")
                {
                    document.all.num.innerText=emailCount;
                    document.all.div.style.visibility="visible";
                }
                else
                {
                    document.all.num.innerText="";
                    document.all.div.style.visibility="hidden";
                }
		    }
		}
		
		
		function Punch()
		{
		    var xhttp ;
		    if (document.all.spanleave.innerText=="[上班打卡]")
		    {
		        xhttp= new ActiveXObject("Microsoft.XMLHTTP");
		 	    xhttp.Open("GET", "httpHandle.aspx?actType=1", false); 
    	
	             xhttp.Send();
    	     
	             if(xhttp.status != 200) 
	             {
	               alert('上班打卡不成功');
	             }
	             else
	             {
	              alert('上班打卡成功');
	              document.all.spanleave.innerText="[下班打卡]";	              
	             }    		 
		    }
		    else
		    {
		     xhttp= new ActiveXObject("Microsoft.XMLHTTP");
		 	 xhttp.Open("GET", "httpHandle.aspx?actType=2", false); 
	
	         xhttp.Send();
	     
	         if(xhttp.status != 200) 
	         {
	           alert('下班打卡不成功');
	         }
	         else
	         {
	          alert('下班打卡成功');
	         havealert=true;
	         }
    		 
		    }
		}
		
	var emailLink="<div align='center' id=divLink><a href='#'  onclick='parent.showEmail()' >你有新消息</a></div>";
	var commentLink="<div align='center' id=divLink><a href='#'  onclick='parent.showComment()' >你的案件有新主管意见</a></div>";
	function OpenPopup(emailCount,commentCount)
    {
        var div=document.all .divMessage;
    
        var body=document.body;
        pop=window.createPopup();
        html=div.innerHTML;
  
       if (emailCount>0)
       {
        html=html.replace("xxxx", emailLink );    
       }
      else
      {
        html=html.replace("xxxx", "" );
      }  
      if (commentCount>0)
       {
        html=html.replace("yyyy", commentLink );  
      }
      else
      {
        html=html.replace("yyyy", "" );
      }

        pop.document.body.innerHTML=html;
        pop.document.body.style.border="solid  1px";
		
	    divTop = parseInt(div.style.top,10);
	    divLeft = parseInt(div.style.left,10);
	    divHeight = parseInt(div.offsetHeight,10);
	    divWidth = parseInt(div.offsetWidth,10);
	    docWidth =  body.clientWidth;
	    docHeight =body.clientHeight;
	    var mainHeight= top.window.mainFra.document.body.clientHeight;
    //	y = parseInt(body.scrollTop,10) + docHeight + 10;//  divHeight
	    y = mainHeight + docHeight + 10;//  mainHeight
	    x = parseInt(body.scrollLeft,10) + docWidth - divWidth;
	    pop.show(200, 100, 180, 116, null);
    }
     
    function showEmail()
    {
        window.open("MessageList.aspx?type=inbox");
    }
    
     function showComment()
    {
        window.open("AlertComment.aspx");
    }
    
    var currentOption="1";
    function CheckKeyOption(obj)
    {
        currentOption=obj.value;
  
    }
    
    function OpenSearch()
    {
        var text=document.all.txtSearch.value.Trim();
        var ddl=document.all.ddlCompany;
       var companyID= ddl.options[ddl.selectedIndex].value;
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
   
        window.open("SearchCaseList.aspx?companyID="+companyID+"&type="+currentOption+"&key="+escape(text));
 
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
   
    </script>
</head>
<body scroll="no">
    <form id="form1" runat="server">
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td width="24" style="height: 40px">
                <img src="images/Transformers5.jpg" height="40" alt="">
            </td>
            <td style="height: 40px" align="left">
                <font style="font-style: italic; font-family: times; font-weight: bold; font-size: 150%">
                    <%=Common.StrTable.GetStr("webname") %></font>
            </td>
            <td style="height: 40px">
            </td>
            <% if (CheckSearch())
               { %>
            <td width="572" style="height: 40px" background="images/index_top_04.jpg" align="right">
                <span style="color: White; padding-right: 20px">
                    <asp:DropDownList ID="ddlCompany" runat="server" Height="19px" Width="85px">
                    </asp:DropDownList>
                    <input id="Radio1" name="rdKey" type="radio" value="1" checked onclick="CheckKeyOption(this)" />电话<input
                        id="Radio2" name="rdKey" type="radio" value="2" onclick="CheckKeyOption(this)" />姓名或帐号<input
                            id="Radio3" name="rdKey" type="radio" value="3" onclick="CheckKeyOption(this)" />全部
                    <asp:TextBox ID="txtSearch" runat="server" Height="18px" onkeydown="return keyDown()"></asp:TextBox>&nbsp;
                    <img style="vertical-align: bottom; cursor: hand" src="Images/search1.gif" onclick="javascript:OpenSearch();" /></span>
            </td>
            <% }%>
        </tr>
    </table>
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td width="100%" rowspan="4" background="images/index_menu_04.jpg">
                <table width="99%" border="0" align="right" cellpadding="0" cellspacing="0">
                    <tr>
                        <td colspan="3">
                            <table width="100%">
                                <tr>
                                    <td align="left">
                                        <span id="num"></span><span id="div" style="visibility: hidden; cursor: hand" onclick="showEmail()">
                                            <img src="images/email.gif" /></span>
                                        <asp:Label ID="lblUser" runat="server" Font-Size="9pt" Font-Names="Arial,Helvetic,sans-serif"
                                            Font-Bold="true"></asp:Label>
                                        ,<%=Common.StrTable.GetStr("welcome") %>&nbsp;&nbsp;今天:<%=Now%>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td height="24" class="menu" align="right" colspan="2">
                                        <span id='spanleave' style="cursor: hand" onclick="Punch()" runat="server">[上班打卡]</span>&nbsp;
                                        <%=GetPage()%>
                                        <a href="javascript:showUrl('ChangePass.aspx');">[<%=Common.StrTable.GetStr("changePass")%>]</a>&nbsp;
                                        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">[<%=Common.StrTable.GetStr("logout") %>]</asp:LinkButton>
                                        &nbsp;
                                        <asp:TextBox ID="txtType" Style="display: none" runat="server" Text="1"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div id="divMessage" style="border-right: #455690 1px solid; border-top: #a6b4cf 1px solid;
        z-index: 101; left: 0px; border-left: #a6b4cf 1px solid; width: 180px; border-bottom: #455690 1px solid;
        position: absolute; top: 0px; height: 116px; background-color: #c9d3f3; display: none">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" style="border-top: #ffffff 1px solid;
            border-left: #ffffff 1px solid" bgcolor="#cfdef4">
            <tr>
                <td style="font-weight: normal; font-size: 9pt; background-image: url(Images/msgTopBg.gif);
                    color: #1f336b; padding-top: 4px" valign="middle" width="100%">
                    温馨提示：
                </td>
                <td align="right">
                    <span style="cursor: hand;" onclick="parent.pop.hide()">X</span>&nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2" height="90" style="padding-right: 1px; background-image: url(Images/msgBottomBg.jpg);
                    padding-bottom: 1px">
                    xxxx yyyy
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
