﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Do.aspx.cs" Inherits="Comm_MapDef_Do" %>
<%@ Register src="Pub.ascx" tagname="Pub" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<title runat=server /> 
</title>
   <link href="../../Comm/Style/Table0.css" rel="stylesheet" type="text/css" />
   <script language="JavaScript" src="./../../Comm/JScript.js" type="text/javascript" ></script>
    <script language=javascript>
    /* ESC Key Down  */
    function Esc()
    {
        if (event.keyCode == 27)
            window.close();
       return true;
    }
    function EditEnum(key)
    {
        var url='SysEnum.aspx?DoType=Edit&RefNo='+key;
      //  window.location.href=url;
        var b=window.showModalDialog( url , 'ass' ,'dialogHeight: 500px; dialogWidth: 500px;center: yes; help: no'); 
        window.location.reload(); 
    }
    function NewEnum()
    {
        var url='SysEnum.aspx?DoType=New&EnumKey=';
        var b=window.showModalDialog( url , 'ass' ,'dialogHeight: 500px; dialogWidth: 500px;center: yes; help: no'); 
       window.location.href = window.location.href;
    }
    function AddEnum( mypk, idx, key)
    {
        if (window.confirm('您确定要增加字段['+key+']吗？') ==false)
            return ;
    
        var url='Do.aspx?DoType=AddEnum&MyPK=' + mypk + '&IDX='+ idx  + '&EnumKey=' + key ;
       // window.location.href=url;
        var b=window.showModalDialog( url , 'ass' ,'dialogHeight: 400px; dialogWidth: 500px;center: yes; help: no'); 
        //window.location.href = window.location.href;
    }
    function AddSFTable(mypk, idx, key) {
        if (window.confirm('您确定要增加字段[' + key + ']吗？') == false)
            return;
        var url = 'Do.aspx?DoType=AddSFTableAttr&MyPK=' + mypk + '&IDX=' + idx + '&RefNo=' + key;
        // window.location.href=url;
        var b = window.showModalDialog(url, 'ass', 'dialogHeight: 400px; dialogWidth: 500px;center: yes; help: no');
        //window.location.href = window.location.href;
    }
    </script>
    <base target=_self /> 
</head>
<body  topmargin="20" leftmargin="20" onkeypress="Esc()"  onload="RSize()" >
    <form id="form1" runat="server">
    <div align=center width="80%" >
      <uc1:Pub ID="Pub1" runat="server" />
      <uc1:Pub ID="Pub2" runat="server" />
    </div>
    </form>
</body>
</html>
 

  


  

