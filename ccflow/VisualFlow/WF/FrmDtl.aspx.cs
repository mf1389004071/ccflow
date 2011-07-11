﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.WF;
using BP.En;
using BP.DA;
using BP.Sys;
using BP.Web;

public partial class WF_FrmDtl : System.Web.UI.Page
{
    #region 属性
    public int FK_Node
    {
        get
        {
            try
            {
                return int.Parse(this.Request.QueryString["FK_Node"]);
            }
            catch
            {
                return 101;
            }
        }
    }
    public int WorkID
    {
        get
        {
            try
            {
                return int.Parse(this.Request.QueryString["WorkID"]);
            }
            catch
            {
                return 0;
            }
        }
    }
    public int OID
    {
        get
        {
            try
            {
                return int.Parse(this.Request.QueryString["OID"]);
            }
            catch
            {
                return 0;
            }
        }
    }
    public string FK_MapData
    {
        get
        {
            string s = this.Request.QueryString["FK_MapData"];
            if (s == null)
                return "ND101";
            return s;
        }
    }
    public bool IsReadonly
    {
        get
        {
            if (this.Request.QueryString["IsReadonly"] == "1")
                return true;
            return false;
        }
    }
    #endregion 属性

    protected void Page_Load(object sender, EventArgs e)
    {
        MapDtl dtl = new MapDtl(this.FK_MapData);
        GEDtl dtlEn = dtl.HisGEDtl;
        dtlEn.SetValByKey("OID", this.OID);
        dtlEn.RetrieveFromDBSources();

        this.UCEn1.BindFreeFrm(dtlEn, this.FK_MapData, this.IsReadonly);

        if (this.IsReadonly)
        {
            this.Btn_Save.Visible = false;
            this.Btn_Save.Enabled = false;

            //this.Btn_Print.Visible = false;
            //this.Btn_Print.Enabled = false;
        }
    }
    protected void Btn_Save_Click(object sender, EventArgs e)
    {

    }
     
}