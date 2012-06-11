﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CCOA_Controls_ImageButton : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public string Title { get; set; }

    public string OnClientClick { get; set; }

    public string LinkUrl { get; set; }

    public string ImageUrl { get; set; }

    public string AlertText { get; set; }

    protected string ClickEvent
    {
        get
        {
            return OnClientClick + "('" + Title + "','" + LinkUrl + "')";
        }
    }
}