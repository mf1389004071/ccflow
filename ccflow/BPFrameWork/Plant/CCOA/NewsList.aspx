﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewsList.aspx.cs" Inherits="EIP_NewsList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .newslist
        {
            font-family: 宋体, Arial, Helvetica, sans-serif;
            font-size: 12px;
        }
        .newslist ul li
        {
            height:20px;
            line-height:20px;
        }
        .newslist ul li a
        {
            text-decoration:none;
            color:Red;
        }
    </style>
</head>
<body style="font-size: small">
    <form id="form1" runat="server">
    <div>
        <div class="newslist">
            <ul>
                <% foreach (BP.CCOA.OA_News item in NewsList)
                   {%>
                <li><a href="Notice/Show.aspx?id=<%=item.No%>" target="_blank">
                    <%=item.NewsTitle %></a>&nbsp;&nbsp;&nbsp;&nbsp;<%=item.Author %>&nbsp;&nbsp;&nbsp;&nbsp;<%=item.CreateTime.ToShortDateString()%></li>
                <%} %>
            </ul>
        </div>
    </div>
    </form>
</body>
</html>
