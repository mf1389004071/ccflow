﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CCOA_Header : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void lbtExit_Click(object sender, EventArgs e)
    {
        BP.Web.WebUser.Exit();
        Response.Redirect("Login.aspx");
    }
}