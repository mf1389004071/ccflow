﻿using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Face_MasterPage : System.Web.UI.MasterPage
{
    public string DoType
    {
        get
        {
            return "";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.AddHeader("P3P", "CP=CAO PSA OUR");

        BP.WF.XML.ToolBars ens = new BP.WF.XML.ToolBars();
        ens.RetrieveAll();

        this.Pub1.Add("<div align=center ><Table width='500px' class=TTable ><TR>");
        this.Pub1.Add("<TD width='10%' align=right nowarp=true ><a href='Tools.aspx' >Hi:" + BP.Web.WebUser.No + "</a></TD>");

        this.Pub1.Add("<TD width='50%' align=right ></TD>");

        string dotype = "";
        foreach (BP.WF.XML.ToolBar en in ens)
        {
            if (en.No == this.DoType)
            {
                this.Pub1.Add("<TD nowrap=true><img src='" + en.Img + "' border='0' ><b>" + en.Name + "</b></TD>");
            }
            else
            {
                this.Pub1.Add("<TD nowrap=true><a href='" + en.Url + "' target='_self' title='" + en.Title + "' ><img src='" + en.Img + "' border='0' >" + en.Name + "</a></TD>");
            }
        }

        this.Pub1.Add("<TD width='20%' ></TD>");
        this.Pub1.Add("</TR>");
        this.Pub1.Add("</Table></div><hr width='80%'>");
    }
}
