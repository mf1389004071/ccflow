﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Favorite.aspx.cs" Inherits="GE_Favorite_AddFavorite" %>

<%@ Register Src="../Pub.ascx" TagName="Pub" TagPrefix="uc1" %>
<html>
<head runat="server">
    <title>我的收藏</title>
    <base target="_self" />
    <%Response.Expires = -1;%>
    <link href="../../Style/Table.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        var myColor;
        function span_Onclick(e) {
            var tab = document.getElementById("myTable");
            var tds = tab.getElementsByTagName("td");
            for (var i = 0; i < tds.length; i++) {
                tds[i].style.backgroundColor = "#FFFFFF";
                tds[i].getElementsByTagName("img")[0].src = "notify_close.gif";
            }
            e.getElementsByTagName("td")[0].style.backgroundColor = "#F5F5F5";
            e.getElementsByTagName("img")[0].src = "notify_open.gif";
            document.getElementById("hidden").value = e.getElementsByTagName("span")[0].value;
            document.getElementById("hidden2").value = e.getElementsByTagName("span")[0].name;
        }
        function myCheck() {
            var tab = document.getElementById("myTable");
            var tds = tab.getElementsByTagName("td");
            var hid = document.getElementById("hidden");
            for (var i = 0; i < tds.length; i++) {
                if (hid.value == tds[i].getElementsByTagName("span")[0].value) {
                    tds[i].style.backgroundColor = "#F5F5F5";
                    tds[i].getElementsByTagName("img")[0].src = "notify_open.gif";
                }
            }
        }
        function DelConfirm() {
            return confirm("您确定要删除该选项吗?");
        }
        function tr_Ondblclick(e) {
            span_Onclick(e);
            document.getElementById("hidden3").value = "OPEN";
            document.forms[0].submit();
        }
        function chk_Click(e) {
            var strVal = document.getElementById("hidden3").value;
            if (strVal.indexOf(e.value) != -1) {
                strVal = strVal.replace(e.value + ",", "");
            }
            else {
                strVal += e.value + ",";
            }
            document.getElementById("hidden3").value = strVal;
        }
    </script>
</head>
<body onload="myCheck()" style="text-align: center">
    <form name="form1" id="form1" runat="server">
    <div style="width: 710px">
        <!--Left-->
        <div style="width: 200px; float: left">
            <uc1:Pub ID="Pub1" runat="server" />
            <br />
            <div id="divInput" runat="server">
                名称: <asp:TextBox ID="txtInput" 
                    runat="server" MaxLength="10" Width="120px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtInput"
                    ErrorMessage="*" ValidationGroup="V1"></asp:RequiredFieldValidator>
            </div>
            <asp:Button ID="btnCreate" runat="server" Text="新建" OnClick="btnCreate_Click" ValidationGroup="V1" />
            <asp:Button ID="btnRename" runat="server" OnClick="btnRename_Click" Text="重命名" ValidationGroup="V1" />
            <asp:Button ID="btnDelete" runat="server" Text="删除" OnClientClick="return DelConfirm()"
                OnClick="btnDelete_Click" />
            <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="收藏" ValidationGroup="V1" />
            <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="取消" />
            <input type="hidden" id="hidden" runat="server" />
            <input type="hidden" id="hidden2" runat="server" />
            <input type="hidden" id="hidden3" runat="server" />
        </div>
        <!--Right-->
        <div style="width: 500px; float: left">
            <uc1:Pub ID="Pub2" runat="server" />
            <br />
            <uc1:Pub ID="Pub3" runat="server" />
        </div>
    </div>
    </form>
</body>
</html>
