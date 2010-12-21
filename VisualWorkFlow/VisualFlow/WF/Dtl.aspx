﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Dtl.aspx.cs" Inherits="Comm_Dtl" %>
<%@ Register Assembly="BP.Web.Controls" Namespace="BP.Web.Controls" TagPrefix="cc1" %>
<%@ Register src="Pub.ascx" tagname="Pub" tagprefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
		<Meta http-equiv="Page-Enter" Content="revealTrans(duration=0.5, transition=8)" />
		 <link href="./../Comm/Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="./../Comm/Style/Table.css" rel="stylesheet" type="text/css" />
    
		 <style type="text/css">
		     body
		     {
		     	font-size:smaller;
		     }
		     </style>
		
		<script language="javascript" >
		    var isChange = false;
		    function SaveDtlData() {
   
		        if (isChange == false)
		            return;
		        var btn = document.getElementById('Button1');
		        btn.click();
		        isChange = false;
		    }
function TROver(ctrl)
{
   ctrl.style.backgroundColor='LightSteelBlue';
}

function TROut(ctrl)
{
  ctrl.style.backgroundColor='white';
}

function Del(id,ens)
{
  if (window.confirm('您确定要执行删除吗？')==false)
     return;
      
        var url='Do.aspx?DoType=DelDtl&OID='+id+'&EnsName='+ens;
        var b=window.showModalDialog( url , 'ass' ,'dialogHeight: 400px; dialogWidth: 600px;center: yes; help: no'); 
        window.location.href = window.location.href;
}

    </script>
    <style type="text/css">
        .HBtn
        {
        	 width:1px;
        	 height:1px;
        	 display:none;
        }
    </style>
	<script language="JavaScript" src="./Style/JScript.js"></script>
    <script language="JavaScript" src="../../Comm/JS/Calendar.js" type="text/javascript"></script>    
        <link href="Style/Style.css" rel="stylesheet" type="text/css" />
</head>
<body topmargin="0" leftmargin="0" onkeypress="Esc()" style="font-size:smaller"> 
    <form id="form1" runat="server">
     <asp:Button ID="Button1" runat="server" Text=""  CssClass="HBtn" Visible=true
         onclick="Button1_Click" />
     <uc2:Pub ID="Pub1" runat="server" />
     <uc2:Pub ID="Pub2" runat="server" />
    </form>
</body>
</html>