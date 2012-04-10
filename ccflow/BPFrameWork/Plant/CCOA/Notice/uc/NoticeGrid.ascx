﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NoticeGrid.ascx.cs" Inherits="CCOA_News_uc_NoticeGrid" %>
<%@ Register Src="~/CCOA/Controls/XSearch.ascx" TagName="XSearch" TagPrefix="uc" %>

    <table width="96%" cellpadding="0" cellspacing="0" >
        <tr>
            <td>
                <uc:XSearch ID="xSearch" runat="server" ShowDateRange="true" />
            </td>
            <td>
                <base:XButton ID="btnSearch" runat="server" Text="查询" OnClick="btnSearch_Click" />
            </td>
        </tr>
    </table>
<div>
    <table width="96%;">
        <tr>
            <th>
                标题
            </th>
            <th>
                类型
            </th>
            <th>
                发布时间
            </th>
            <th>
                点击次数
            </th>
            <th>
                评论（条）
            </th>
            <th>
                新闻评论
            </th>
        </tr>
        <% foreach (BP.CCOA.Article item in Articles)
           {%>
        <td>
            <%=item.Title %>
        </td>
        <td>
            <%=item.ArticleType%>
        </td>
        <td>
            <%=item.Created %>
        </td>
        <td>
            <%=item.Clicks%>
        </td>
        <td>
            <%=item.CommentCount%>
        </td>
        <td>
            <a href="#">评论</a>
        </td>
        <%} %>
    </table>
</div>
