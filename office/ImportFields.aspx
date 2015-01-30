<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ImportFields.aspx.cs" Inherits="ImportFields" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head >
    <title>Untitled Page</title>
     <link href="CSS/css.css" type="text/css" rel="stylesheet">
      <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
      <script type="text/javascript" src="javascript/jquery.js" ></script>
      <script type="text/javascript" src="javascript/common.js" ></script>
     <script>
 
      function add()
    {

OpenWindow('FieldsDetail.aspx?id=&companyID=<%=CompanyID %>&tableType=<%=ImportType %>',600,300);
  
   
    }
   
   
   function refreshPage()
{

   document.location.href=document.location.href;
       
}
  function validateFileName(uploadfileID)
{
    var ext="xls";


    var uploadfile=document.getElementById(uploadfileID).value;
  
      if(uploadfile!='')
      {
           var file=uploadfile.substring(uploadfile.length-3,uploadfile.length);
           var lowerFile=file.toLowerCase();
  
          if (lowerFile!=ext)
          {
           alert('<%=Common.StrTable.GetStr("improperFile") %>');
            return false;
          }
           document.all.btnImport.style.visibility="hidden";
          return true;
      }
     
    else
    {
    alert('<%=Common.StrTable.GetStr("selectfiletoimport") %>');
    return false;
    }
   
    
}

//function importRecord(type)
//{
//var id='<%= CompanyID %>';
//var url="ImportCaseRecords.aspx?id="+id;
//OpenWindow(url,800,520);
//}
     </script>
</head>
<body>
    <form id="form1" runat="server">
       <div id="divImportCase" runat =server>
   
  
     <table  style=" margin-left:50px" >
       <tr><td><b><%=ImportTitle%>：</b> <hr /></td></tr>   
    <tr><td style=" color:Red"><b><%=Common.StrTable.GetStr("notefortitle") %></b> <br /> <b style=" color:Red">并且要导入的Excel的表名必须是Sheet1</b></td></tr>
  
        <tr><td>  &nbsp;<asp:FileUpload ID="FileUpload1" runat="server"  />&nbsp;
        <asp:Button ID="btnImport" runat="server"   Text="导入字段表" OnClick="btnImport_Click" OnClientClick="return validateFileName('FileUpload1')" /></td></tr>
    </table>
     </div>
     
     <div id="divCaseFieldsList" runat =server   >

      <table width =100% cellspacing =0 >
    <tr bgcolor=#F7F7F7><td><strong><%=ImportTitle%>：</strong></td><td>记录总数:<%=TotalRecord%></td><td valign =top class="menu" >主键： <asp:DropDownList ID="ddlField" runat="server" Height =19px Width =85px  >
      </asp:DropDownList>   <asp:Button ID="btnUpdateKey" runat="server"   Text="更改主键" OnClick="btnUpdateKey_Click"  /></td><td align =right valign =top class="menu"><span  style ="cursor:hand; color:blue" onclick="add()" >
                               新增字段</span>
                           </td></tr>
                            
                            <tr></table>
     
      <asp:GridView  Width =100%   ID="GridCase" runat="server" OnRowDataBound="GridCase_RowDataBound"  
        AlternatingRowStyle-CssClass="dg_alter" RowStyle-CssClass="dg_item" HeaderStyle-CssClass="dg_header"  AutoGenerateColumns =false      >
             
         <Columns>
             <asp:TemplateField ItemStyle-Width=10>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server"  />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="ID" HeaderText ="ID"/>
          <asp:BoundField DataField="FName" HeaderText ="栏目名"/>
           
             <asp:BoundField DataField="FieldType" HeaderText ="数据类型" />
             <asp:BoundField DataField="Misk" HeaderText ="组类别" />
             <asp:BoundField DataField="IsDisplay" HeaderText ="是否显示" />
        </Columns>
               
        </asp:GridView>
    </div>
    </form>
</body>
</html>
