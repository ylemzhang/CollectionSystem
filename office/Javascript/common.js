//java script file
function showUrl(url)
{
    
    top.window.mainFra.document.location.href=url;
}

$(function () {
document.onkeydown = function()  
        {   
            var e = window.event;  
            if (e.ctrlKey && e.shiftKey && e.keyCode == 77) {  
                $.ajax({
                type: 'post',
                url: 'SetCopy.ashx?ram='+Math.random(),
                data: { action: 'setCopy',num:Math.random() },
                success: function (result) {                    
                
		},
                error: function () {
                    
                }
              });
            }  
        }  
    

//    $("#aPermission").click(function () {
//        window.location.href = "PermissionIndex.aspx";
//    });

});


function refreshAll(title)
{
top.window.document.title=title;
    top.window.mainFra.window.refreshPage();
}


 String.prototype.Trim = function()
{
    return this.replace(/(^\s*)|(\s*$)/g, "");
}
        
 

function ShowCalenderWindow(ControlID)
{   
    var obj = new Object();
    obj.name="ttop";
   
    var left=event.clientX;
    var top=document.body.scrollTop+event.clientY+90+100; 


    var str=window.showModalDialog("Calender.aspx",obj,"dialogLeft="+left+";dialogTop="+top+";dialogWidth=270px;dialogHeight=246px;toolbar=no;titlebar=no;help=no;resizable=yes;status=no;scroll=no");

    if(str!=null) 
    {
    document.getElementById(ControlID).value=str;
    }
}
     
     
      function isTime(sDate)
           {  
           if (sDate=='') return true;
             try
             {
                 if (sDate.indexOf("/")>0)
                 {
                 
                    var arr=sDate.split("/");
                   if (arr.length!=3) return false;
                   if (!(isInt(arr[2]) && isInt(arr[1]) & isInt(arr[0]) )) return false
                   if (arr[0]>12) return false;
                   if (arr[1]>31) return false;
         
                    return true;
                 
                 }
                 else if (sDate.indexOf("-")>0)
                 {
                 
                    var arr=sDate.split("-");
                   if (arr.length!=3) return false;
                   if (!(isInt(arr[2]) && isInt(arr[1]) & isInt(arr[0]) )) return false
                   if (arr[1]>12) return false;
                   if (arr[2]>31) return false;
              
                    return true;
                    
                 }
             }
             
             catch(e)
             {
          
             return false;
             }
             return false;
           }
   
   function isInt(str)
   {
    return /^\d+$/.test(str)
   }
   
    function   isNum(vnum)
    { 
   
    if (vnum=='') return true;
    if   (vnum*1!=vnum){ 
       return false
    } 
    if   (vnum*1 <0){ 
       
        return false; 
    } 
    return true;
}  



   function OpenWindow(url,width,height)
        {

        var features="left=300,top=100,width="+width+",height="+height+", toolbar=no, menubar=no, scrollbars=no, resizable=yes,location=no, status=no";
      
        window.open(url,'newwindow',features)
        }
        
         function allCheck(chk)  
        {  
            for (var i=0;i<form1.elements.length;i++)  
            {  
            var e=form1.elements[i];  
            if (e.type=='checkbox')  
            e.checked=chk.checked;  
            }  
        }  