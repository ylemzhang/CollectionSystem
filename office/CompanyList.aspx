<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CompanyList.aspx.cs" Inherits="CompanyList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link href="CSS/css.css" type="text/css" rel="stylesheet">
     <link href="CSS/Tab.css" type="text/css" rel="stylesheet">
     <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
     <script type="text/javascript" src="javascript/jquery.js" ></script>
    <script type="text/javascript" src="javascript/common.js" ></script>
    <script>
    
    function  ConfirmDelete()
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
           return confirm ('<%=Common.StrTable.GetStr("confirmDelete") %>');
           }
           else
           {
           window.alert('<%=Common.StrTable.GetStr("selectRecordToDelete") %>');
           return false;
           }

}

  function refreshPage(id,type)
{
 document.all.txtID.value=id;
    if (type==0) //new
    {
   
    document.all.btnSave.click();
      ShowAllTabs()
           
    }
    else
    {
    document.all.btnSave.click();
    }
   
}

function hightlight(oldid,currentid)
{

 var tempid;
 if (oldid!="") 
     {
     tempid="Row"+oldid;
     var oldRow= findRow(tempid);
     if (oldRow !=null)
     {
     
      oldRow.style.backgroundColor='';
      }
     }
     
     if (currentid!="") 
     {
     tempid="Row"+currentid;
     var currentRow= findRow(tempid);
     if (currentRow !=null)
     {
      currentRow.style.backgroundColor='#F7F7F7';
      }
     }
     
 
 document.all.txtID.value=currentid;
}

function findRow(tempid)
{
 
for(var i=0;i<GridView1.rows.length;i++) 
{
 if (GridView1.rows[i].currenRowID==tempid)
 {
 return GridView1.rows[i];
 }
}


return null;

}

function showMainUrl(url)
{

    parent.window.mainFra.document.location.href=url;
}



    function fillDetail(obj,id,hasbalance)
    {
    var oldid=document.all.txtID.value;
    
     hightlight(oldid,id)
  
        var url="CompanyEdit.aspx?id="+id;
        showMainUrl(url);
       
        ShowAllTabs(hasbalance);
       
    }
    
     function add()
    {
   hightlight(document.all.txtID.value,'');

    var url="CompanyEdit.aspx?id=";
        showMainUrl(url);   
    ShowOnlyFirstTab();
    }
    


        function ShowOnlyFirstTab()
      {
      
       document.getElementById('current1').className = 'current';
       
        document.getElementById('current2').style.display = 'none';
       document.getElementById('current3').style.display = 'none';
         document.getElementById('current4').style.display = 'none';
       document.getElementById('current5').style.display = 'none';
       
       
        }
        
     
 
        
       function ShowAllTabs(hasbalance)
      {
      
       document.getElementById('current1').className = 'current';
      
        
        document.getElementById('current2').className ='';  
        document.getElementById('current2').style.display = 'block';
        
        document.getElementById('current3').className ='';  
        document.getElementById('current3').style.display = 'block';
        
          document.getElementById('current4').className ='';  
        document.getElementById('current4').style.display = 'block';
        
        
        if (hasbalance=="1")
        {
        document.getElementById('current5').className ='';  
        document.getElementById('current5').style.display = 'block';
        }
        else
        {
          document.getElementById('current5').className ='';  
        document.getElementById('current5').style.display = 'none';
        }
      
       
       
        }
        
  function change_option(number,index)
  {
    for (var i = 1; i <= number; i++) 
    {
      document.getElementById('current' + i).className = '';

    }
    
    document.getElementById('current'+index).className = 'current';
   
   var txtid=document.all.txtID.value;

   if (index==1)
    {
      url="CompanyEdit.aspx?id="+txtid;
      showMainUrl(url);
  
    return;
    }
    if (index==2)
    {
   
     url="CompanyPermission.aspx?id="+txtid;
     showMainUrl(url);
     return;
    }
    if (index==3)
    {
      url="ImportFields.aspx?type=1&id="+txtid;
        showMainUrl(url);
  
     return;
    }
     if (index==4)
    {
      url="ImportFields.aspx?type=2&id="+txtid;
        showMainUrl(url);
  
     return;
    }
     if (index==5)
    {
      url="ImportFields.aspx?type=3&id="+txtid;
        showMainUrl(url);
  
     return;
    }
    
}

function init()
{
    if (document.all.txtID.value=="") //first
    {
    ShowOnlyFirstTab();
    }
    else
    {
     hightlight("",document.all.txtID.value);
    }
}

    </script>
</head>
<body  onload ="init()">
    <form id="form1" runat="server">
   
   
    <div style="height:229px; overflow:auto ">
   
     <table width =100% cellspacing =0 >
    <tr bgcolor=#F7F7F7><td><strong>公司管理</strong></td><td align =right valign =top class="menu" ><span  style ="cursor:hand; color:blue" onclick="add()" >
                               新增公司|</span>
                           <span runat =server id="spanDelete"> <asp:LinkButton ID="LinkButton2" style ="cursor:hand; color:blue"  runat="server" OnClientClick="return ConfirmDelete()" OnClick="LinkButton2_Click">删除</asp:LinkButton></span></td></tr>
                            
                            <tr>
                            <td  colspan=2 >
                             <asp:GridView  Width =100% ID="GridView1" runat="server" OnRowDataBound="GridView1_RowDataBound" 
        HeaderStyle-CssClass="dg_header"  AutoGenerateColumns =false    >
             
         <Columns>
             <asp:TemplateField ItemStyle-Width=10>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server"  />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="ID" HeaderText ="ID"/>
            <asp:BoundField DataField="HasBalanceTable" HeaderText ="ID"/>
             <asp:BoundField DataField="CompanyName" HeaderText ="公司名称" ItemStyle-Width=200/>
           
             <asp:BoundField DataField="Description" HeaderText ="公司描述" />


                
        </Columns>

        </asp:GridView>
                            </td>
                            </tr>
    </table>
   </div>
        
        
  <div id="header">
  <ul>
    <li id="current1"><a href="#" onclick="change_option(5,1);">公司信息</a></li>
    <li id="current2"><a href="#" onclick="change_option(5,2);">用户权限</a></li>
     <li id="current3"><a href="#" onclick="change_option(5,3);">案件表字段导入</a></li>
     <li id="current4"><a href="#" onclick="change_option(5,4);">每日还款单字段导入</a></li>
     
      <li id="current5"><a href="#" onclick="change_option(5,5);">余额表字段导入</a></li>
  
  </ul>
   <asp:Button ID="btnSave" runat="server"  style=" display:none" Text="保存" OnClick="btnSave_Click"  />
<asp:TextBox ID="txtID" style=" display:none" runat="server" Width =200   ></asp:TextBox>
 </div>

</form>
</body>
</html>


