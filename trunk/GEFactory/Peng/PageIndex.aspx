﻿<%@ Page Language="C#" MasterPageFile="~/GE/Template/WinOpen.master" AutoEventWireup="true" CodeFile="PageIndex.aspx.cs" Inherits="Peng_Default" Title="无标题页" %>

<%@ Register src="../GE/Pub.ascx" tagname="Pub" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<table align=center width='80%'>
<tr>
<td class=BigDoc>
   <uc1:Pub ID="Pub1" runat="server" />
    <uc1:Pub ID="Pub2" runat="server" />
    
    
    <uc1:Pub ID="Pub3" runat="server" />
</td>
</tr>
</table>

 
</asp:Content>

