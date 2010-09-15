﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="VideoMore1.ascx.cs" Inherits="GE_Video_VideoMore1" %>
<%@ Register Assembly="BP.GE" Namespace="BP.GE.Ctrl" TagPrefix="cc1" %>
<%@ Register Src="../../Pub.ascx" TagName="Pub" TagPrefix="uc1" %>
<table width="100%" style="table-layout:fixed">
    <tr>
        <td valign="top" class="BigDoc" width='23%'>
            <uc1:Pub ID="PubSort" runat="server" />
        </td>
        <td valign="top" class="BigDoc" width='77%'>
            <table class="Table" style="border-collapse: collapse;table-layout:fixed;overflow:hidden">
                    <uc1:Pub ID="PubSearch" runat="server" />
                <tr class="TR">
                    <td class="TD" colspan="2">
                        <uc1:Pub ID="Pub3" runat="server" />
                        <cc1:GeImage ID="GeImage1" runat="server" GloDBType="DataTable" PageSize="10" ShowPage="True">
                            <GloDBColumns>
                                <cc1:MyListItem ID="MyListItem1" runat="server" DataFormatString="&lt;a target='_blank' href=&quot;VideoDtl1.aspx?RefNo=@No&quot;&gt;&lt;img src=&quot;{0}&quot; onerror=&quot;this.src='@DefImgSrc'&quot; height=&quot;@ImgHeight&quot; width='@ImgWidth' style=&quot;border:none&quot; /&gt;&lt;/a&gt;"
                                    DataTextField="ImgUrl" EnableViewState="False">
                                    <UrlListItems>
                                        <cc1:UrlList ParaName="No" ValueFrom="DataRow" />
                                        <cc1:UrlList ParaName="ImgWidth" ValueFrom="DataRow" />
                                        <cc1:UrlList ParaName="ImgHeight" ValueFrom="DataRow" />
                                        <cc1:UrlList ParaName="DefImgSrc" ValueFrom="DataRow" />
                                    </UrlListItems>
                                </cc1:MyListItem>
                                <cc1:MyListItem ID="MyListItem2" runat="server" DataFormatString="&lt;a style=&quot;width:'@ImgWidth';overflow: hidden;white-space: nowrap;-o-text-overflow: ellipsis;text-overflow: ellipsis;&quot; target='_blank' href=&quot;VideoDtl1.aspx?RefNo=@No&quot;&gt;{0}&lt;/a&gt;"
                                    DataTextField="Title" EnableViewState="True">
                                    <UrlListItems>
                                        <cc1:UrlList ParaName="No" ValueFrom="DataRow" />
                                        <cc1:UrlList ParaName="ImgWidth" ValueFrom="DataRow" />
                                    </UrlListItems>
                                </cc1:MyListItem>
                            </GloDBColumns>
                        </cc1:GeImage>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
