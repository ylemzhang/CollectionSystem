<%@ Page Language="C#" AutoEventWireup="true" CodeFile="advanceSearch.aspx.cs" Inherits="advanceSearch" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">



<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Search</title>
  
        
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
    <link href="CSS/css.css" type="text/css" rel="stylesheet">
     <script type="text/javascript" src="javascript/jquery.js" ></script>
     <script type="text/javascript" src="javascript/common.js" ></script>
     <script>
      function refreshPage()
{

   document.location.href=document.location.href;
       
}

    function fillValue(sel,id)
    {

           str=sel.options[sel.selectedIndex].innerText;
           document.getElementById(id).value=str;
            
    }
    
    function fieldChange(sel, ddlid)
    {
    var ddl=document.getElementById(ddlid);
    var type= sel.options[sel.selectedIndex].value;
       
     addOption(type,ddl);

    }

 function addOption(type,sel)
 {

   var strs=type.split("|");


var type=strs[1];
     if(type=='')
     {
     sel.options[0]=new Option("包含","包含",true,true);
     sel.options[1]=new Option("=","=",true,true);
     sel.options[2]=new Option("开头以","开头以",true,true);
     sel.options[3]=new Option("结尾以","结尾以",true,true);
     sel.options[4]=new Option("<>","<>",true,true);
     }
     else
     {
      sel.options[0]=new Option("=","=",true,true);
      sel.options[1]=new Option(">",">",true,true);
      sel.options[2]=new Option(">=",">=",true,true);
      sel.options[3]=new Option("<","<=",true,true);
        sel.options[4]=new Option("<=","<=",true,true);
     sel.options[5]=new Option("<>","<>",true,true);
       
 
     }
     sel.selectedIndex=0;
 }
 

function CheckSearch()
{

   var thisfrm=document.all.form1;
            var checked;
            var rtn="";
            var ddlOpt;
            var ddl;
            var ddlAndor;
            var txtControl;
            var txtValue;
           for (i=0; i<thisfrm.length; i++) 
           {

              if (thisfrm.elements[i].name.indexOf('ddlAndOr') !=-1)
               {

                ddlAndor=thisfrm.elements[i];
                  ddl=ddlAndor.parentElement.nextSibling.firstChild;
                
                  if (ddl.selectedIndex>0)
                  {
                     ddlOpt=ddl.parentElement.nextSibling.firstChild;
                 
                      if (ddlOpt.selectedIndex!=-1)
                      {
                          txtControl=ddlOpt.parentElement.nextSibling.firstChild;
                      
                          txtValue=txtControl.value.Trim();
                  
                          if (txtValue!='')
                          {
                          var fieldandtype=ddl.options[ddl.selectedIndex].value;
                           var strs=fieldandtype.split("|");
                          var type=strs[1];
                           
                           if (txtValue.toLowerCase()!='null')
                             {
                                 if(type=='int' || type=="money")
                                 {
                                     if(!isNum(txtValue))
                                     {
                                     alert('数字格式不正确');
                                      return ;
                                     }
                                 }
                                 
                                  if(type=='datetime' )
                                 {
                                     if(!isTime(txtValue))
                                     {
                                     alert('日期格式不正确');
                                      return ;
                                     }
                                 }
                                 
                              }


                           rtn=rtn +fieldandtype+ "|"+ddlOpt.options[ddlOpt.selectedIndex].value+"|"+txtValue+"|"+ddlAndor.options[ddlAndor.selectedIndex].value+";";
                          }
                      }
             

                  }
                  


              }

           }
           

if (rtn.length>0)
{
document.all.txtSearchRows.value=rtn;
document.all.btnSave.click();

}
else
{
alert('请输入查找条件');
}


}

function deleteRow(btn)
{
var tr=btn.parentElement.parentNode;

tr.parentNode.removeChild(tr);

}

function AddRow(btn)
{
GetSearchRow();
document.all.addRow.click();

}

function GetSearchRow()
{

   var thisfrm=document.all.form1;
            var checked;
            var rtn="";
            var ddlOpt;
            var ddl;
            var ddlAndor;
            var txtControl;
            var txtValue;
            var txtField;
            var txtOpt;
            var txtAndor;
           for (var i=0; i<thisfrm.length; i++) 
           {

              if (thisfrm.elements[i].name.indexOf('ddlAndOr') !=-1)
               {

                     ddlAndor=thisfrm.elements[i];
                     ddl=ddlAndor.parentElement.nextSibling.firstChild;
                
             
                     ddlOpt=ddl.parentElement.nextSibling.firstChild;
                 
                  
                      txtControl=ddlOpt.parentElement.nextSibling.firstChild;
                      
                       txtValue=txtControl.value.Trim();
                  
                  txtField="|";
                 
                  if (ddl.selectedIndex>0)
                  {
                  txtField=ddl.options[ddl.selectedIndex].value;
                  }
                 
                  
                    txtOpt="";
                  if (ddlOpt.selectedIndex!=-1)
                  {
                  txtOpt=ddlOpt.options[ddlOpt.selectedIndex].value;
                  }
                   
                    
                     txtAndor="";
                  if (ddlAndor.selectedIndex!=-1)
                  {
                  txtAndor=ddlAndor.options[ddlAndor.selectedIndex].value;
                  }
                
                    
                       rtn=rtn +txtField+ "|"+txtOpt+"|"+txtValue+"|"+txtAndor+";";
 
              }

           }
           


document.all.txtSearchRows.value=rtn;


}

    </script>
</head>
<body  >
    <form id="form1" runat="server" >
    <br />
     <br />
      <br />
    
       
     <div align ="center" >
    <table width =580 bgcolor=#F7F7F7>
   <tr><td colspan =5 align =right>注：如果要查找空值，请输入null</td></tr>
   <tr ><td colspan =2 align =left > 公司:<asp:DropDownList ID="ddlCompany" runat="server" AutoPostBack =true OnSelectedIndexChanged ="ddlCompany_SelectedIndexChanged" >
      </asp:DropDownList></td><td align =right ><asp:Label runat =server ID="lblTitle"></asp:Label>&nbsp;&nbsp;</td><td colspan =2 align =right ><asp:LinkButton ID="LinkButton2" style ="cursor:hand; color:blue"  runat="server" OnClick="LinkButton2_Click"  >[案件记录]</asp:LinkButton> <asp:LinkButton ID="LinkButton1" style ="cursor:hand; color:blue"  runat="server" OnClick="LinkButton1_Click"  >[还款记录]</asp:LinkButton> <asp:LinkButton ID="LinkButton3" style ="cursor:hand; color:blue"  runat="server" OnClick="LinkButton3_Click"  >[余额表记录]</asp:LinkButton>  </td></tr>
   <tr class="menu">
   <td colspan =5>  <asp:GridView  Width =100%   ID="GridView1" runat="server" OnRowDataBound="GridView1_RowDataBound"  
        AlternatingRowStyle-CssClass="dg_alter" RowStyle-CssClass="dg_item" HeaderStyle-CssClass="dg_header"  AutoGenerateColumns =false     >
             
         <Columns>
            <asp:TemplateField ItemStyle-Width=150 HeaderText="And/Or">
                <ItemTemplate>
                    <asp:DropDownList  Width =150  ID="ddlAndOr" runat="server"  ></asp:DropDownList>
                </ItemTemplate>
                </asp:TemplateField>
            
              <asp:TemplateField ItemStyle-Width=150 HeaderText="字段名">
                <ItemTemplate>
                    <asp:DropDownList  Width =150  ID="ddlField" runat="server"  ></asp:DropDownList>
                </ItemTemplate>
               
            </asp:TemplateField> 
            
             <asp:TemplateField ItemStyle-Width=120 HeaderText="操作符">
                <ItemTemplate> 
                    <asp:DropDownList  Width =120  ID="ddlOperator" runat="server"  ></asp:DropDownList>
                </ItemTemplate>
              </asp:TemplateField> 
              
                <asp:TemplateField ItemStyle-Width=180 HeaderText="值">
                <ItemTemplate>
                   <asp:TextBox ID="txtValue"  runat="server" Width =160></asp:TextBox>
                </ItemTemplate>
               
            </asp:TemplateField> 
            
             <asp:TemplateField ItemStyle-Width=10 HeaderText="<input type=button  value='+'  onclick ='AddRow(this)'" >
                <ItemTemplate>
                   <input type=button  value="-"  onclick ="deleteRow(this)" />
                </ItemTemplate>
               
            </asp:TemplateField> 
               
         
        </Columns>
                              
        </asp:GridView>
        </td>
   
   </tr>
        

  
    
    <tr><td  width =700 colspan =5 align=center>
      <input type="button" runat=server value ="查找" id="btnGo" onclick ="CheckSearch()" /> <asp:Button ID="btnSave" style="display:none" runat="server" Text="保存" OnClick="btnSave_Click" /> <asp:Button ID="addRow" style="display:none" runat="server" Text="保存" OnClick="btnAddRow_Click" />
  </td></tr>
     
    </table>
        
        <input type=hidden  runat=server  id="txtSearchRows" />
         
       
        
     </div>
    </form>
</body>
</html>
