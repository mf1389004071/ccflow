﻿<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MapDef/WinOpen.master" AutoEventWireup="true" CodeFile="FrmEvent.aspx.cs" Inherits="WF_MapDef_FrmEvent" %>
<%@ Register src="Pub.ascx" tagname="Pub" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table width='100%' >
<tr>
<td valign=tp>
    <uc1:Pub ID="Pub1" runat="server" />
    </td>
<td>
    <uc1:Pub ID="Pub2" runat="server" />
    </td>
    </tr>
</table>

</asp:Content>

