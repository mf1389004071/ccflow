﻿using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using BP.Sys;
using BP.En;
using BP.Web.Controls;
using BP.DA;
using BP.Web;
public partial class WF_MapDef_MapDef : WebPage
{
    public new string MyPK
    {
        get
        {
            string key = this.Request.QueryString["MyPK"];
            if (key == null)
                key = this.Request.QueryString["PK"];
            if (key == null)
                key = this.Request.QueryString["FK_MapData"];
            if (key == null)
                key = "ND1601";
            return key;
        }
    }
    public string FK_MapData
    {
        get
        {
            return this.MyPK;
        }
    }
    public string FK_Flow
    {
        get
        {
            return this.Request.QueryString["FK_Flow"];
        }
    }
    /// <summary>
    /// IsEditMapData
    /// </summary>
    public bool IsEditMapData
    {
        get
        {
            string s = this.Request.QueryString["IsEditMapData"];
            if (s == null || s == "1")
                return true;
            return false;
        }
    }
    public void BindLeft()
    {
        BP.WF.XML.MapMenus xmls = new BP.WF.XML.MapMenus();
        xmls.RetrieveAll();

        #region bindleft
        this.Left.Add("<a href='http://ccflow.org' target=_blank ><img src='" + this.Request.ApplicationPath + "/DataUser/LogBiger.png' border=0/></a>");
        this.Left.AddHR();
        this.Left.AddUL();
        foreach (BP.WF.XML.MapMenu item in xmls)
        {
            this.Left.AddLi("<a href=\"" + item.JS.Replace("@MyPK", "'" + this.FK_MapData + "'").Replace("@FK_Flow", "'" + this.FK_Flow + "'") + "\" ><img src='"+item.Img+"' width='16px' /><b>" + item.Name + "</b></a><br><font color=green>" + item.Note + "</font>");
        }
        this.Left.AddULEnd();
        #endregion bindleft
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        string fk_node = this.Request.QueryString["FK_Node"];
        //if (string.IsNullOrEmpty(fk_node) == false)
        //{
        //    BP.WF.Node nd = new BP.WF.Node();
        //    nd.NodeID = int.Parse(fk_node);
        //    nd.RetrieveFromDBSources();
        //    if (nd.HisFormType != BP.WF.FormType.FixForm)
        //    {
        //        this.Response.Redirect("./CCForm/Frm.aspx?FK_MapData=" + this.FK_MapData + "&FK_Flow=" + nd.FK_Flow, true);
        //        return;
        //    }
        //}

        MapData md = new MapData(this.FK_MapData);
        MapAttrs mattrs = new MapAttrs(md.No);
        int count = mattrs.Count;
        if (gfs.Count == 1)
        {
            //GroupField mygf = (GroupField)gfs[0];
            //if (mygf.Lab != md.Name)
            //{
            //    mygf.Lab = md.Name;
            //    mygf.Update();
            //}
        }
        this.BindLeft();

        this.Pub1.AddH2("设计表单:("+md.No+")"+md.Name );
        this.Pub1.Add("\t\n<Table style=\"width:600px;\" align=left>");
        /*
         * 根据 GroupField 循环出现菜单。
         */ 
        foreach (GroupField gf in gfs)
        {
            rowIdx = 0;
            string gfAttr = " onmouseover=GFOnMouseOver('" + gf.OID + "','" + rowIdx + "') onmouseout=GFOnMouseOut()";
            currGF = gf;
            this.Pub1.AddTR(gfAttr);
            if (gfs.Count == 1)
                this.Pub1.AddTD("colspan=4 class=GroupField valign='top' align:left style='height: 24px;align:left' ", "<div style='text-align:left; float:left'>&nbsp;<a href=\"javascript:GroupField('" + this.FK_MapData + "','" + gf.OID + "')\" >" + gf.Lab + "</a></div><div style='text-align:right; float:right'></div>");
            else
                this.Pub1.AddTD("colspan=4 class=GroupField valign='top' align:left style='height: 24px;align:left' ", "<div style='text-align:left; float:left'><img src='./Style/Min.gif' alert='Min' id='Img" + gf.Idx + "' onclick=\"GroupBarClick('" + gf.Idx + "')\"  border=0 />&nbsp;<a href=\"javascript:GroupField('" + this.FK_MapData + "','" + gf.OID + "')\" >" + gf.Lab + "</a></div><div style='text-align:right; float:right'> <a href=\"javascript:GFDoUp('" + gf.OID + "')\" ><img src='../../Images/Btn/Up.gif' class='Arrow' border=0/></a> <a href=\"javascript:GFDoDown('" + gf.OID + "')\" ><img src='../../Images/Btn/Down.gif' class='Arrow' border=0/></a></div>");

            this.Pub1.AddTREnd();
            int i = -1;
            int idx = -1;
            isLeftNext = true;
            foreach (MapAttr attr in mattrs)
            {
                gfAttr = " onmouseover=GFOnMouseOver('" + gf.OID + "','" + rowIdx + "') onmouseout=GFOnMouseOut()";
                if (attr.GroupID == 0)
                {
                    attr.GroupID = gf.OID;
                    attr.Update();
                }

                if (attr.GroupID != gf.OID)
                {
                    if (gf.Idx == 0 && attr.GroupID == 0)
                    {
                    }
                    else
                        continue;
                }

                if (attr.HisAttr.IsRefAttr || attr.UIVisible == false)
                    continue;

                if (isLeftNext)
                {
                    if (gfs.Count == 0)
                        this.InsertObjects(false);
                    else
                        this.InsertObjects(true);
                }
                // 显示的顺序号.
                idx++;
                if (attr.IsBigDoc && attr.UIIsLine)
                {
                    if (isLeftNext == false)
                    {
                        this.Pub1.AddTD();
                        this.Pub1.AddTD();
                        this.Pub1.AddTREnd();
                    }
                    rowIdx++;
                    this.Pub1.AddTR(" ID='" + currGF.Idx + "_" + rowIdx + "'  " + gfAttr);
                    this.Pub1.Add("<TD colspan=4 width='100%' height='" + attr.UIHeight.ToString() + "px' >");

                    this.Pub1.Add("<span style='float:left' height='" + attr.UIHeight.ToString() + "px' >" + this.GenerLab(attr, idx, 0, count) + "</span>");
                    this.Pub1.Add("<span style='float:right' height='" + attr.UIHeight.ToString() + "px'  >");
                    Label lab = new Label();
                    lab.ID = "Lab" + attr.KeyOfEn;
                    lab.Text = "默认值";
                    this.Pub1.Add(lab);
                    this.Pub1.Add("</span><br>");

                    TB mytbLine = new TB();
                    mytbLine.ID = "TB_" + attr.KeyOfEn;
                    mytbLine.TextMode = TextBoxMode.MultiLine;
                    mytbLine.Attributes["style"] = "width:100%;height:100%;padding: 0px;margin: 0px;";
                    mytbLine.Enabled = attr.UIIsEnable;
                    this.Pub1.Add(mytbLine);
                    mytbLine.Attributes["width"] = "100%";
                    lab = this.Pub1.GetLabelByID("Lab" + attr.KeyOfEn);
                    string ctlID = mytbLine.ClientID;
                    lab.Text = "<a href=\"javascript:TBHelp('" + ctlID + "','" + this.Request.ApplicationPath + "','" + md.No  + "','" + attr.KeyOfEn + "')\">默认值</a>";

                    this.Pub1.AddTDEnd();
                    this.Pub1.AddTREnd();
                    isLeftNext = true;
                    continue;
                }

                if (attr.IsBigDoc)
                {
                    if (isLeftNext)
                    {
                        rowIdx++;
                        this.Pub1.AddTR(" ID='" + currGF.Idx + "_" + rowIdx + "' " + gfAttr);
                    }
                    this.Pub1.Add("<TD  colspan=2 width='50%' height='" + attr.UIHeight.ToString() + "px' >");
                    // this.Pub1.Add(this.GenerLab(attr, idx, 0, count));
                    this.Pub1.Add("<span height='" + attr.UIHeight.ToString() + "px' style='float:left'>" + this.GenerLab(attr, idx, 0, count) + "</span>");
                    this.Pub1.Add("<span height='" + attr.UIHeight.ToString() + "px' style='float:right'>");
                    Label lab = new Label();
                    lab.ID = "Lab" + attr.KeyOfEn;
                    lab.Text = "默认值";
                    this.Pub1.Add(lab);
                    this.Pub1.Add("</span>");

                    TB mytbLine = new TB();
                    mytbLine.TextMode = TextBoxMode.MultiLine;
                    mytbLine.Attributes["class"] = "TBDoc"; // "width:100%;padding: 0px;margin: 0px;";
                    mytbLine.ID = "TB_" + attr.KeyOfEn;
                    mytbLine.Enabled = attr.UIIsEnable;
                    if (mytbLine.Enabled == false)
                        mytbLine.Attributes["class"] = "TBReadonly";
                    mytbLine.Attributes["style"] = "width:100%;height:100%;padding: 0px;margin: 0px;";
                    this.Pub1.Add(mytbLine);

                    lab = this.Pub1.GetLabelByID("Lab" + attr.KeyOfEn);
                    string ctlID = mytbLine.ClientID;
                    lab.Text = "<a href=\"javascript:TBHelp('" + ctlID + "','" + this.Request.ApplicationPath + "','" + md.No + "','" + attr.KeyOfEn + "')\">默认值</a>";
                    this.Pub1.AddTDEnd();
                    if (isLeftNext == false)
                    {
                        this.Pub1.AddTREnd();
                    }
                    isLeftNext = !isLeftNext;
                    continue;
                }

                //计算 colspanOfCtl .
                int colspanOfCtl = 1;
                if (attr.UIIsLine)
                    colspanOfCtl = 3;

                if (attr.UIIsLine)
                {
                    if (isLeftNext == false)
                    {
                        this.Pub1.AddTD();
                        this.Pub1.AddTD();
                        this.Pub1.AddTREnd();
                    }
                    isLeftNext = true;
                }

                if (isLeftNext)
                {
                    rowIdx++;
                    this.Pub1.AddTR(" ID='" + currGF.Idx + "_" + rowIdx + "' " + gfAttr);
                }

                TB tb = new TB();
                tb.Attributes["width"] = "100%";
                tb.Columns = 60;
                tb.ID = "TB_" + attr.KeyOfEn;

                #region add contrals.
                switch (attr.LGType)
                {
                    case FieldTypeS.Normal:

                        tb.Enabled = attr.UIIsEnable;
                        switch (attr.MyDataType)
                        {
                            case BP.DA.DataType.AppString:
                                this.Pub1.AddTDDesc(this.GenerLab(attr, idx, i, count));
                                tb.ShowType = TBType.TB;
                                tb.Text = attr.DefVal;
                                //   tb.Attributes["onblur"] = "check(this,'"+attr.MinLen+"','"+attr.MaxLen+"')";
                                if (colspanOfCtl == 3)
                                {
                                    this.Pub1.AddTD(" width=80% colspan=" + colspanOfCtl, tb);
                                }
                                else
                                {
                                    if (attr.IsSigan)
                                        this.Pub1.AddTD("colspan=" + colspanOfCtl, "<img src='../../DataUser/Siganture/" + WebUser.No + ".jpg' border=0 onerror=\"this.src='../../DataUser/Siganture/UnName.jpg'\"/>");
                                    else
                                        this.Pub1.AddTD("width='40%' colspan=" + colspanOfCtl, tb);
                                }
                                break;
                            case BP.DA.DataType.AppDate:
                                this.Pub1.AddTDDesc(this.GenerLab(attr, idx, i, count));
                                TB tbD = new TB();
                                if (attr.UIIsEnable)
                                {
                                    tbD.Attributes["onfocus"] = "WdatePicker();";
                                    tbD.Attributes["class"] = "TBcalendar";
                                }
                                else
                                {
                                    tbD.Enabled = false;
                                    tbD.ReadOnly = true;
                                    tbD.Attributes["class"] = "TBcalendar";
                                }
                                this.Pub1.AddTD("width='40%' colspan=" + colspanOfCtl, tbD);
                                break;
                            case BP.DA.DataType.AppDateTime:
                                this.Pub1.AddTDDesc(this.GenerLab(attr, idx, i, count));
                                TB tbDT = new TB();
                                tbDT.Text = attr.DefVal;
                                if (attr.UIIsEnable)
                                {
                                    tbDT.Attributes["onfocus"] = "WdatePicker({dateFmt:'yyyy-MM-dd HH:mm'});";
                                    tbDT.Attributes["class"] = "TBcalendar";
                                }
                                else
                                {
                                    tbDT.Enabled = false;
                                    tbDT.ReadOnly = true;
                                    tbDT.Attributes["class"] = "TBcalendar";
                                }
                                this.Pub1.AddTD("width='40%' colspan=" + colspanOfCtl, tbDT);
                                break;
                            case BP.DA.DataType.AppBoolean:
                                if (attr.UIIsLine)
                                    this.Pub1.AddTDDesc(this.GenerLab(attr, idx, i, count));
                                else
                                    this.Pub1.AddTDDesc(this.GenerLab(attr, idx, i, count));

                                CheckBox cb = new CheckBox();
                                cb.Text = attr.Name;
                                cb.Checked = attr.DefValOfBool;
                                cb.Enabled = attr.UIIsEnable;
                                cb.ID = "CB_" + attr.KeyOfEn;
                                this.Pub1.AddTD("width='40%' colspan=" + colspanOfCtl, cb);
                                break;
                            case BP.DA.DataType.AppDouble:
                            case BP.DA.DataType.AppFloat:
                            case BP.DA.DataType.AppInt:
                                this.Pub1.AddTDDesc(this.GenerLab(attr, idx, i, count));
                                tb.ShowType = TBType.Num;
                                tb.Text = attr.DefVal;
                                this.Pub1.AddTD("width='40%' colspan=" + colspanOfCtl, tb);
                                break;
                            case BP.DA.DataType.AppMoney:
                            case BP.DA.DataType.AppRate:
                                this.Pub1.AddTDDesc(this.GenerLab(attr, idx, i, count));
                                tb.ShowType = TBType.Moneny;
                                tb.Text = attr.DefVal;
                                //  tb.Attributes["OnKeyPress"]="return VirtyNum(this);";
                                this.Pub1.AddTD("width='40%' colspan=" + colspanOfCtl, tb);
                                break;
                            default:
                                break;
                        }


                        tb.Attributes["width"] = "100%";
                        switch (attr.MyDataType)
                        {
                            case BP.DA.DataType.AppString:
                            case BP.DA.DataType.AppDateTime:
                            case BP.DA.DataType.AppDate:
                                if (tb.Enabled)
                                    tb.Attributes["class"] = "TB";
                                else
                                    tb.Attributes["class"] = "TBReadonly";
                                break;
                            default:
                                if (tb.Enabled)
                                    tb.Attributes["class"] = "TBNum";
                                else
                                    tb.Attributes["class"] = "TBNumReadonly";
                                break;
                        }
                        break;
                    case FieldTypeS.Enum:
                        this.Pub1.AddTDDesc(this.GenerLab(attr, idx, i, count));
                        DDL ddle = new DDL();
                        ddle.ID = "DDL_" + attr.KeyOfEn;
                        ddle.BindSysEnum(attr.KeyOfEn);
                        ddle.SetSelectItem(attr.DefVal);
                        ddle.Enabled = attr.UIIsEnable;
                        this.Pub1.AddTD("colspan=" + colspanOfCtl, ddle);
                        break;
                    case FieldTypeS.FK:
                        this.Pub1.AddTDDesc(this.GenerLab(attr, idx, i, count));
                        DDL ddl1 = new DDL();
                        ddl1.ID = "DDL_" + attr.KeyOfEn;
                        try
                        {
                            EntitiesNoName ens = attr.HisEntitiesNoName;
                            ens.RetrieveAll();
                            ddl1.BindEntities(ens);
                            ddl1.SetSelectItem(attr.DefVal);
                        }
                        catch
                        {
                        }
                        ddl1.Enabled = attr.UIIsEnable;
                        this.Pub1.AddTD("colspan=" + colspanOfCtl, ddl1);
                        break;
                    default:
                        break;
                }
                #endregion add contrals.

                if (colspanOfCtl == 3)
                {
                    isLeftNext = true;
                    this.Pub1.AddTREnd();
                    continue;
                }

                if (isLeftNext == false)
                {
                    isLeftNext = true;
                    this.Pub1.AddTREnd();
                    continue;
                }
                isLeftNext = false;
            }
            // 最后处理补充上它。
            if (isLeftNext == false)
            {
                this.Pub1.AddTD();
                this.Pub1.AddTD();
                this.Pub1.AddTREnd();
            }
            this.InsertObjects(false);

        }
        this.Pub1.AddTableEnd();


        #region 处理异常情况。
        foreach (MapDtl dtl in dtls)
        {
            if (dtl.IsUse == false)
            {
                dtl.RowIdx = 0;
                dtl.GroupID = 0;
                dtl.Update();
                //    this.Response.Redirect(this.Request.RawUrl, true);
            }
        }
        #endregion 处理异常情况。

        #region 处理扩展信息。
        MapExts mes = new MapExts(this.FK_MapData);
        if (mes.Count != 0)
        {
            this.Page.RegisterClientScriptBlock("s",
          "<script language='JavaScript' src='./../Scripts/jquery-1.4.1.min.js' ></script>");

            this.Page.RegisterClientScriptBlock("b",
         "<script language='JavaScript' src='./../Scripts/MapExt.js' ></script>");

            this.Page.RegisterClientScriptBlock("dC",
     "<script language='JavaScript' src='./../../DataUser/JSLibData/" + this.FK_MapData + ".js' ></script>");

            this.Pub1.Add("<div id='divinfo' style='width: 155px; position: absolute; color: Lime; display: none;cursor: pointer;align:left'></div>");
        }

        string jsStr = "";
        foreach (MapExt me in mes)
        {
            switch (me.ExtType)
            {
                case MapExtXmlList.DDLFullCtrl: // 自动填充.
                    DDL ddlOper = this.Pub1.GetDDLByID("DDL_" + me.AttrOfOper);
                    if (ddlOper == null)
                        continue;
                    ddlOper.Attributes["onchange"] = "DDLFullCtrl(this.value,\'" + ddlOper.ClientID + "\', \'" + me.MyPK + "\')";
                    break;
                case MapExtXmlList.ActiveDDL:
                    DDL ddlPerant = this.Pub1.GetDDLByID("DDL_" + me.AttrOfOper);
                    DDL ddlChild = this.Pub1.GetDDLByID("DDL_" + me.AttrsOfActive);
                    ddlPerant.Attributes["onchange"] = "DDLAnsc(this.value,\'" + ddlChild.ClientID + "\', \'" + me.MyPK + "\')";
                    // ddlChild.Attributes["onchange"] = "ddlCity_onchange(this.value,'" + me.MyPK + "')";
                    break;
                case MapExtXmlList.TBFullCtrl: // 自动填充.
                    TB tbAuto = this.Pub1.GetTBByID("TB_" + me.AttrOfOper);
                    if (tbAuto == null)
                    {
                        me.Delete();
                        continue;
                    }
                    tbAuto.Attributes["onkeyup"] = "DoAnscToFillDiv(this,this.value,\'" + tbAuto.ClientID + "\', \'" + me.MyPK + "\');";
                    tbAuto.Attributes["AUTOCOMPLETE"] = "OFF";
                    break;
                case MapExtXmlList.InputCheck: /*js 检查 */
                    TB tbJS = this.Pub1.GetTBByID("TB_" + me.AttrOfOper);
                    if (tbJS != null)
                    {
                        tbJS.Attributes[me.Tag2] += me.Tag1 + "(this);";
                    }
                    else
                    {
                        me.Delete();
                    }
                    break;
                case MapExtXmlList.PopVal: //弹出窗.
                    TB tb = this.Pub1.GetTBByID("TB_" + me.AttrOfOper);
                    //tb.Attributes["ondblclick"] = "ReturnVal(this,'" + me.Doc + "','sd');";
                    break;
                default:
                    break;
            }
        }
        #endregion 处理扩展信息。

        #region 处理输入最小，最大验证.
        foreach (MapAttr attr in mattrs)
        {
            if (attr.MyDataType != DataType.AppString || attr.MinLen == 0)
                continue;

            if (attr.UIIsEnable == false || attr.UIVisible == false)
                continue;

            this.Pub1.GetTextBoxByID("TB_" + attr.KeyOfEn).Attributes["onblur"] = "checkLength(this,'" + attr.MinLen + "','" + attr.MaxLen + "')";
        }
        #endregion 处理输入最小，最大验证.

        #region 处理iFrom 的自适应的问题。
        string js = "\t\n<script type='text/javascript' >";
        foreach (MapDtl dtl in dtls)
        {
            js += "\t\n window.setInterval(\"ReinitIframe('F" + dtl.No + "','TD" + dtl.No + "')\", 200);";
        }

        foreach (MapM2M M2M in dot2dots)
        {
            if (M2M.ShowWay == FrmShowWay.FrmAutoSize)
                js += "\t\n window.setInterval(\"ReinitIframe('F" + M2M.NoOfObj + "','TD" + M2M.NoOfObj + "')\", 200);";
        }
        foreach (FrmAttachment ath in aths)
        {
            if (ath.IsAutoSize)
                js += "\t\n window.setInterval(\"ReinitIframe('F" + ath.MyPK + "','TD" + ath.MyPK + "')\", 200);";
        }
        foreach (MapFrame fr in frams)
        {
            js += "\t\n window.setInterval(\"ReinitIframe('F" + fr.MyPK + "','TD" + fr.MyPK + "')\", 200);";
        }

        js += "\t\n</script>";
        this.Pub1.Add(js);
        #endregion 处理iFrom 的自适应的问题。

        #region 处理 JS 自动计算.
        for (int i = 0; i < mattrs.Count; i++)
        {
            MapAttr attr = mattrs[i] as MapAttr;
            if (attr.UIContralType != UIContralType.TB)
                continue;

            switch (attr.HisAutoFull)
            {
                case AutoFullWay.Way1_JS:
                    js = "\t\n <script type='text/javascript' >";
                    TB tb = this.Pub1.GetTBByID("TB_" + attr.KeyOfEn);
                    if (tb == null)
                        continue;

                    string left = "\n  document.forms[0]." + tb.ClientID + ".value = ";
                    string right = attr.AutoFullDoc;
                    foreach (MapAttr mattr in mattrs)
                    {
                        if (mattr.IsNum == false)
                            continue;

                        if (attr.AutoFullDoc.Contains("@" + mattr.KeyOfEn)
                            || attr.AutoFullDoc.Contains("@" + mattr.Name))
                        {
                        }
                        else
                        {
                            continue;
                        }

                        string tbID = "TB_" + mattr.KeyOfEn;
                        TB mytb = this.Pub1.GetTBByID(tbID);
                        if (mytb == null)
                            continue;

                        this.Pub1.GetTBByID(tbID).Attributes["onkeyup"] = "javascript:Auto" + attr.KeyOfEn + "();";
                        right = right.Replace("@" + mattr.Name, " parseFloat( document.forms[0]." + mytb.ClientID + ".value.replace( ',' ,  '' ) ) ");
                        right = right.Replace("@" + mattr.KeyOfEn, " parseFloat( document.forms[0]." + mytb.ClientID + ".value.replace( ',' ,  '' ) ) ");
                    }

                    js += "\t\n function Auto" + attr.KeyOfEn + "() { ";
                    js += left + right + ";";
                    js += " \t\n  document.forms[0]." + tb.ClientID + ".value= VirtyMoney(document.forms[0]." + tb.ClientID + ".value ) ;";
                    js += "\t\n } ";
                    js += "\t\n</script>";
                    this.Add(js);
                    break;
                default:
                    break;
            }
        }
        #endregion 处理iFrom 的自适应的问题。

        #region 处理隐藏字段。
        DataTable dt = DBAccess.RunSQLReturnTable("SELECT * FROM Sys_MapAttr WHERE FK_MapData='"+this.FK_MapData+"' AND GroupID NOT IN (SELECT OID FROM Sys_GroupField WHERE EnName='"+this.FK_MapData+"')");
        if (dt.Rows.Count != 0)
        {
            int gfid = gfs[0].GetValIntByKey("OID");
            foreach (DataRow dr in dt.Rows)
                DBAccess.RunSQL("UPDATE Sys_MapAttr SET GroupID="+gfid+" WHERE MyPK='"+dr["MyPK"]+"'");

            this.Response.Redirect(this.Request.RawUrl);
        }

        ////string SysFields = "OID,FID,FK_NY,Emps,FK_Dept,NodeState,WFState,BillNo,RDT,MyNum,";
        //string msg = ""; // +++++++ 编辑隐藏字段 +++++++++ <br>";
        //foreach (MapAttr attr in mattrs)
        //{
        //    if (attr.UIVisible)
        //    {
        //        bool isHave = false;
        //        foreach (GroupField gf in gfs)
        //        {
        //            if (attr.GroupID == gf.OID)
        //            {
        //                isHave = true;
        //                break;
        //            }

        //        }
        //        if (isHave == false)
        //            msg += "<a href=\"javascript:Edit('" + this.FK_MapData + "','" + attr.MyPK + "','" + attr.MyDataType + "');\">" + attr.Name + "</a> ";
        //        continue;
        //    }
        //    switch (attr.KeyOfEn)
        //    {
        //        case "OID":
        //        case "FID":
        //        case "FK_NY":
        //        case "Emps":
        //        case "FK_Dept":
        //        case "NodeState":
        //        case "WFState":
        //        case "BillNo":
        //        case "RDT":
        //        case "MyNum":
        //        case "Rec":
        //        case "CDT":
        //            continue;
        //        default:
        //            break;
        //    }
        //    msg += "<a href=\"javascript:Edit('" + this.FK_MapData + "','" + attr.MyPK + "','" + attr.MyDataType + "');\">" + attr.Name + "</a> ";
        //}
        //if (msg.Length > 10)
        //{
        //    this.Pub1.AddFieldSet("编辑隐藏字段");
        //    this.Pub1.Add(msg);
        //    this.Pub1.Add("<br>说明：隐藏字段是不显示在表单里面，多用于属性的计算、方向条件的设置，报表的体现。");
        //    this.Pub1.AddFieldSetEnd();
        //}
        #endregion 处理隐藏字段。
    }

    public void InsertObjects(bool isJudgeRowIdx)
    {
        #region 增加明细表
        foreach (MapDtl dtl in dtls)
        {
            if (dtl.IsUse)
                continue;

            if (isJudgeRowIdx)
            {
                if (dtl.RowIdx != rowIdx)
                    continue;
            }

            if (dtl.GroupID == 0 && rowIdx == 0)
            {
                dtl.GroupID = currGF.OID;
                dtl.RowIdx = 0;
                dtl.Update();
            }
            else if (dtl.GroupID == currGF.OID)
            {
            }
            else
            {
                continue;
            }


            dtl.IsUse = true;
            int myidx = rowIdx + 10;
            this.Pub1.AddTR(" ID='" + currGF.Idx + "_" + myidx + "' ");
            this.Pub1.Add("<TD colspan=4 class=TRSum  ><div style='text-align:left; float:left'><a href=\"javascript:EditDtl('" + this.FK_MapData + "','" + dtl.No + "')\" >" + dtl.Name + "</a></div><div style='text-align:right; float:right'><a href=\"javascript:document.getElementById('F" + dtl.No + "').contentWindow.AddF('" + dtl.No + "');\"><img src='../../Images/Btn/New.gif' border=0/>" + this.ToE("Insert", "插入列") + "</a><a href=\"javascript:document.getElementById('F" + dtl.No + "').contentWindow.AddFGroup('" + dtl.No + "');\"><img src='../../Images/Btn/New.gif' border=0/>" + this.ToE("InsertGroupF", "插入列组") + "</a><a href=\"javascript:document.getElementById('F" + dtl.No + "').contentWindow.CopyF('" + dtl.No + "');\"><img src='../../Images/Btn/Copy.gif' border=0/>" + this.ToE("Copy", "复制列") + "</a><a href=\"javascript:document.getElementById('F" + dtl.No + "').contentWindow.HidAttr('" + dtl.No + "');\"><img src='../../Images/Btn/Copy.gif' border=0/>" + this.ToE("HidAttr", "隐藏列") + "</a><a href=\"javascript:DtlDoUp('" + dtl.No + "')\" ><img src='../../Images/Btn/Up.gif' border=0/></a> <a href=\"javascript:DtlDoDown('" + dtl.No + "')\" ><img src='../../Images/Btn/Down.gif' border=0/></a></div></td>");
            this.Pub1.AddTREnd();

            myidx++;
            this.Pub1.AddTR(" ID='" + currGF.Idx + "_" + myidx + "' ");
            this.Pub1.Add("<TD colspan=4 ID='TD" + dtl.No + "' height='50px' width='1000px'>");
            string src = "MapDtlDe.aspx?DoType=Edit&FK_MapData=" + this.FK_MapData + "&FK_MapDtl=" + dtl.No;
            this.Pub1.Add("<iframe ID='F" + dtl.No + "' frameborder=0 style='padding:0px;border:0px;'  leftMargin='0'  topMargin='0' src='" + src + "' width='100%' height='10px' scrolling=no  /></iframe>");
            this.Pub1.AddTDEnd();
            this.Pub1.AddTREnd();
        }
        #endregion 增加明细表

        #region 增加附件
        foreach (FrmAttachment dtl in this.aths)
        {
            if (dtl.IsUse)
                continue;

            if (isJudgeRowIdx)
            {
                if (dtl.RowIdx != rowIdx)
                    continue;
            }

            if (dtl.GroupID == 0 && rowIdx == 0)
            {
                dtl.GroupID = currGF.OID;
                dtl.RowIdx = 0;
                dtl.Update();
            }
            else if (dtl.GroupID == currGF.OID)
            {
            }
            else
            {
                continue;
            }

            dtl.IsUse = true;
            int myidx = rowIdx + 10;
            this.Pub1.AddTR(" ID='" + currGF.Idx + "_" + myidx + "' ");
            this.Pub1.Add("<TD colspan=4 class=TRSum  ><div style='text-align:left; float:left'><a href=\"javascript:EditAth('" + this.FK_MapData + "','" + dtl.NoOfObj + "')\" >" + dtl.Name + "</a></div><div style='text-align:right; float:right'><a href=\"javascript:AthDoUp('" + dtl.MyPK + "')\" ><img src='../../Images/Btn/Up.gif' border=0/></a> <a href=\"javascript:AthDoDown('" + dtl.MyPK + "')\" ><img src='../../Images/Btn/Down.gif' border=0/></a></div></td>");
            this.Pub1.AddTREnd();

            myidx++;
            this.Pub1.AddTR(" ID='" + currGF.Idx + "_" + myidx + "' ");
            this.Pub1.Add("<TD colspan=4 ID='TD" + dtl.MyPK + "' height='50px' width='1000px'>");

            string src = "../FreeFrm/AttachmentUpload.aspx?PKVal=0&Ath=" + dtl.NoOfObj + "&FK_MapData=" + this.FK_MapData + "&FK_FrmAttachment=" + dtl.MyPK;
            if (dtl.IsAutoSize)
                this.Pub1.Add("<iframe ID='F" + dtl.MyPK + "' frameborder=0 style='padding:0px;border:0px;'  leftMargin='0'  topMargin='0' src='" + src + "' width='100%' height='10px' scrolling=no  /></iframe>");
            else
                this.Pub1.Add("<iframe ID='F" + dtl.MyPK + "' frameborder=0 style='padding:0px;border:0px;'  leftMargin='0'  topMargin='0' src='" + src + "' width='" + dtl.W + "' height='" + dtl.H + "' scrolling=auto  /></iframe>");

            this.Pub1.AddTDEnd();
            this.Pub1.AddTREnd();
        }
        #endregion 增加附件

        #region 增加M2M
        foreach (MapM2M m2m in dot2dots)
        {
            if (m2m.IsUse)
                continue;

            if (isJudgeRowIdx)
            {
                if (m2m.RowIdx != rowIdx)
                    continue;
            }

            if (m2m.GroupID == 0 && rowIdx == 0)
            {
                m2m.GroupID = currGF.OID;
                m2m.RowIdx = 0;
                m2m.Update();
            }
            else if (m2m.GroupID == currGF.OID)
            {
            }
            else
            {
                continue;
            }

            m2m.IsUse = true;
            int myidx = rowIdx + 10;
            this.Pub1.AddTR(" ID='" + currGF.Idx + "_" + myidx + "' ");
            this.Pub1.Add("<TD colspan=4 class=TRSum  ><div style='text-align:left; float:left'><a href=\"javascript:EditM2M('" + this.FK_MapData + "','" + m2m.NoOfObj + "')\" >" + m2m.Name + "</a></div><div style='text-align:right; float:right'><a href=\"javascript:M2MDoUp('" + m2m.MyPK + "')\" ><img src='../../Images/Btn/Up.gif' border=0/></a> <a href=\"javascript:M2MDoDown('" + m2m.MyPK + "')\" ><img src='../../Images/Btn/Down.gif' border=0/></a></div></td>");
            this.Pub1.AddTREnd();

            myidx++;
            string src = "";
            if (m2m.HisM2MType == M2MType.M2M)
                src = "../M2M.aspx?FK_MapData=" + this.FK_MapData + "&NoOfObj=" + m2m.NoOfObj + "&OID=0";
            else
                src = "../M2MM.aspx?FK_MapData=" + this.FK_MapData + "&NoOfObj=" + m2m.NoOfObj + "&OID=0";

            switch (m2m.ShowWay)
            {
                case FrmShowWay.FrmAutoSize:
                    //this.Pub1.AddTR(" ID='" + currGF.Idx + "_" + myidx + "'");
                    //this.Pub1.Add("<TD colspan=4 ID='TD" + m2m.NoOfObj + "' width='100%'>");
                    //this.Pub1.Add("<iframe ID='F" + m2m.NoOfObj + "' frameborder=0 style='padding:0px;border:0px;'  leftMargin='0'  topMargin='0' src='" + src + "' width='100%'   scrolling=no  /></iframe>");
                    //this.Pub1.AddTDEnd();
                    //this.Pub1.AddTREnd();

                    myidx++;
                    this.Pub1.AddTR(" ID='" + currGF.Idx + "_" + myidx + "' ");
                    this.Pub1.Add("<TD colspan=4 ID='TD" + m2m.NoOfObj + "' height='50px' width='1000px'>");
                    this.Pub1.Add("<iframe ID='F" + m2m.NoOfObj + "' frameborder=0 style='padding:0px;border:0px;'  leftMargin='0'  topMargin='0' src='" + src + "' width='100%' height='10px' scrolling=no  /></iframe>");
                    this.Pub1.AddTDEnd();
                    this.Pub1.AddTREnd();
                    break;
                case FrmShowWay.FrmSpecSize:
                    this.Pub1.AddTR(" ID='" + currGF.Idx + "_" + myidx + "'");
                    this.Pub1.Add("<TD colspan=4 ID='TD" + m2m.NoOfObj + "' height='" + m2m.H + "' width='" + m2m.W + "'  >");
                    this.Pub1.Add("<iframe ID='F" + m2m.NoOfObj + "' src='" + src + "' frameborder=0 style='padding:0px;border:0px;'  leftMargin='0'  topMargin='0' width='" + m2m.W + "' height='" + m2m.H + "' scrolling=auto /></iframe>");
                    this.Pub1.AddTDEnd();
                    this.Pub1.AddTREnd();
                    break;
                case FrmShowWay.Hidden:
                    break;
                case FrmShowWay.WinOpen:
                    this.Pub1.AddTR(" ID='" + currGF.Idx + "_" + myidx + "'");
                    this.Pub1.Add("<TD colspan=4 ID='TD" + m2m.NoOfObj + "' height='20px' width='100%' >");
                    this.Pub1.Add("<a href=\"javascript:WinOpen('" + src + "&IsOpen=1" + "','" + m2m.W + "','" + m2m.H + "');\"  />" + m2m.Name + "</a>");
                    this.Pub1.AddTDEnd();
                    this.Pub1.AddTREnd();
                    break;
                default:
                    break;
            }
        
        }
        #endregion 增加M2M

        #region 增加框架
        foreach (MapFrame fram in frams)
        {
            if (fram.IsUse)
                continue;

            if (isJudgeRowIdx)
            {
                if (fram.RowIdx != rowIdx)
                    continue;
            }

            if (fram.GroupID == 0 && rowIdx == 0)
            {
                fram.GroupID = currGF.OID;
                fram.RowIdx = 0;
                fram.Update();
            }
            else if (fram.GroupID == currGF.OID)
            {

            }
            else
            {
                continue;
            }

            fram.IsUse = true;
            int myidx = rowIdx + 20;
            this.Pub1.AddTR(" ID='" + currGF.Idx + "_" + myidx + "' ");
            // this.Pub1.Add("<TD colspan=4 class=TRSum  ><div style='text-align:left; float:left'><a href=\"javascript:EditDtl('" + this.FK_MapData + "','" + dtl.No + "')\" >" + dtl.Name + "</a></div><div style='text-align:right; float:right'><a href=\"javascript:document.getElementById('F" + dtl.No + "').contentWindow.AddF('" + dtl.No + "');\"><img src='../../Images/Btn/New.gif' border=0/>" + this.ToE("Insert", "插入列") + "</a><a href=\"javascript:document.getElementById('F" + dtl.No + "').contentWindow.CopyF('" + dtl.No + "');\"><img src='../../Images/Btn/Copy.gif' border=0/>" + this.ToE("Copy", "复制列") + "</a><a href=\"javascript:DtlDoUp('" + dtl.No + "')\" ><img src='../../Images/Btn/Up.gif' border=0/></a> <a href=\"javascript:DtlDoDown('" + dtl.No + "')\" ><img src='../../Images/Btn/Down.gif' border=0/></a></div></td>");
            this.Pub1.Add("<TD colspan=4 class=TRSum  ><div style='text-align:left; float:left'><a href=\"javascript:EditFrame('" + this.FK_MapData + "','" + fram.MyPK + "')\" >" + fram.Name + "</a></div><div style='text-align:right; float:right'><a href=\"javascript:FrameDoUp('" + fram.MyPK + "')\" ><img src='../../Images/Btn/Up.gif' border=0/></a> <a href=\"javascript:FrameDoDown('" + fram.MyPK + "')\" ><img src='../../Images/Btn/Down.gif' border=0/></a></div></td>");
            this.Pub1.AddTREnd();

            myidx++;
            this.Pub1.AddTR(" ID='" + currGF.Idx + "_" + myidx + "' ");
            if (fram.IsAutoSize)
                this.Pub1.Add("<TD colspan=4 ID='TD" + fram.MyPK + "' height='50px' width='1000px'>");
            else
                this.Pub1.Add("<TD colspan=4 ID='TD" + fram.MyPK + "' height='" + fram.H + "' width='" + fram.W + "' >");


            string src = fram.URL; // "MapDtlDe.aspx?DoType=Edit&FK_MapData=" + this.FK_MapData + "&FK_MapDtl=" + fram.No;
            if (src.Contains("?"))
                src += "&FK_Node=" + this.RefNo + "&WorkID=" + this.RefOID;
            else
                src += "?FK_Node=" + this.RefNo + "&WorkID=" + this.RefOID;

            if (fram.IsAutoSize)
                this.Pub1.Add("<iframe ID='F" + fram.MyPK + "' frameborder=0 style='padding:0px;border:0px;'  leftMargin='0'  topMargin='0' src='" + src + "' width='100%' height='100%' scrolling=no  /></iframe>");
            else
                this.Pub1.Add("<iframe ID='F" + fram.MyPK + "' frameborder=0 style='padding:0px;border:0px;'  leftMargin='0'  topMargin='0' src='" + src + "' width='" + fram.W + "' height='" + fram.H + "' scrolling=no  /></iframe>");

            //  this.Pub1.Add("<iframe ID='F" + fram.No + "' frameborder=0 style='padding:0px;border:0px;'  leftMargin='0'  topMargin='0' src='" + src + "' width='" + fram.W + "px' height='" + fram.H + "px' scrolling=no /></iframe>");

            this.Pub1.AddTDEnd();
            this.Pub1.AddTREnd();
        }
        #endregion 增加明细表
    }

    #region varable.
    private FrmAttachments _aths;
    public FrmAttachments aths
    {
        get
        {
            if (_aths == null)
                _aths = new FrmAttachments(this.FK_MapData);
            return _aths;
        }
    }

    public GroupField currGF = new GroupField();
    private MapDtls _dtls;
    public MapDtls dtls
    {
        get
        {
            if (_dtls == null)
                _dtls = new MapDtls(this.FK_MapData);
            return _dtls;
        }
    }
    private MapFrames _frams;
    public MapFrames frams
    {
        get
        {
            if (_frams == null)
                _frams = new MapFrames(this.FK_MapData);
            return _frams;
        }
    }
    private MapM2Ms _dot2dots;
    public MapM2Ms dot2dots
    {
        get
        {
            if (_dot2dots == null)
                _dot2dots = new MapM2Ms(this.FK_MapData);
            return _dot2dots;
        }
    }
    private GroupFields _gfs;
    public GroupFields gfs
    {
        get
        {
            if (_gfs == null)
                _gfs = new GroupFields(this.FK_MapData);

            return _gfs;
        }
    }
    public int rowIdx = 0;
    public bool isLeftNext = true;
    #endregion varable.

    public string GenerLab_arr(MapAttr attr, int idx, int i, int count)
    {
        string divAttr = " onmouseover=FieldOnMouseOver('" + attr.MyPK + "') onmouseout=FieldOnMouseOut('" + attr.MyPK + "') ";
        string lab = attr.Name;
        if (attr.MyDataType == DataType.AppBoolean && attr.UIIsLine)
            lab = "编辑";

        bool isLeft = true;
        if (i == 1)
            isLeft = false;

        if (attr.HisEditType == EditType.Edit || attr.HisEditType == EditType.UnDel)
        {
            switch (attr.LGType)
            {
                case FieldTypeS.Normal:
                    lab = "<a  href=\"javascript:Edit('" + this.FK_MapData + "','" + attr.MyPK + "','" + attr.MyDataType + "');\">" + lab + "</a>";
                    break;
                case FieldTypeS.FK:
                    lab = "<a  href=\"javascript:EditTable('" + this.FK_MapData + "','" + attr.MyPK + "','" + attr.MyDataType + "');\">" + lab + "</a>";
                    break;
                case FieldTypeS.Enum:
                    lab = "<a  href=\"javascript:EditEnum('" + this.FK_MapData + "','" + attr.MyPK + "','" + attr.MyDataType + "');\">" + lab + "</a>";
                    break;
                default:
                    break;
            }
        }
        else
        {
            lab = attr.Name;
        }

        if (idx == 0)
        {
            /*第一个。*/
            return "<div " + divAttr + " >" + lab + "<a href=\"javascript:Down('" + this.FK_MapData + "','" + attr.MyPK + "','1');\" ><img src='../../Images/Btn/Right.gif' class='Arrow' alt='向右动顺序' border=0/></a></div>";
        }

        if (idx == count - 1)
        {
            /*到数第一个。*/
            return "<div " + divAttr + " ><a href=\"javascript:Up('" + this.FK_MapData + "','" + attr.MyPK + "','1');\" ><img src='../../Images/Btn/Left.gif' alt='向左移动顺序' class='Arrow' border=0/></a>" + lab + "</div>";
        }
        return "<div " + divAttr + " ><a href=\"javascript:Up('" + this.FK_MapData + "','" + attr.MyPK + "','1');\" ><img src='../../Images/Btn/Left.gif' alt='向下移动顺序' class='Arrow' border=0/></a>" + lab + "<a href=\"javascript:Down('" + this.FK_MapData + "','" + attr.MyPK + "','1');\" ><img src='../../Images/Btn/Right.gif' alt='向右移动顺序' class='Arrow' border=0/></a></div>";
    }

    public string GenerLab(MapAttr attr, int idx, int i, int count)
    {
        string divAttr = " onDragEnd=onDragEndF('" + attr.MyPK + "','" + attr.GroupID + "');  onDrag=onDragF('" + attr.MyPK + "','" + attr.GroupID + "'); ";
        divAttr +=" onDragOver=FieldOnMouseOver('" + attr.MyPK + "','" + attr.GroupID + "');  onDragEnter=FieldOnMouseOver('" + attr.MyPK + "','" + attr.GroupID + "'); ";
        divAttr += " onDragLeave=FieldOnMouseOut();";

        //divAttr += " onDragLeave=FieldOnMouseOut('" + attr.MyPK + "','" + attr.GroupID + "');";

        string lab = attr.Name;
        if (attr.MyDataType == DataType.AppBoolean && attr.UIIsLine)
            lab = "编辑";

        bool isLeft = true;
        if (i == 1)
            isLeft = false;

        if (attr.HisEditType == EditType.Edit || attr.HisEditType == EditType.UnDel)
        {
            switch (attr.LGType)
            {
                case FieldTypeS.Normal:
                    lab = "<a  href=\"javascript:Edit('" + this.FK_MapData + "','" + attr.MyPK + "','" + attr.MyDataType + "');\">" + lab + "</a>";
                    break;
                case FieldTypeS.FK:
                    lab = "<a  href=\"javascript:EditTable('" + this.FK_MapData + "','" + attr.MyPK + "','" + attr.MyDataType + "');\">" + lab + "</a>";
                    break;
                case FieldTypeS.Enum:
                    lab = "<a  href=\"javascript:EditEnum('" + this.FK_MapData + "','" + attr.MyPK + "','" + attr.MyDataType + "');\">" + lab + "</a>";
                    break;
                default:
                    break;
            }
        }
        else
        {
            lab = attr.Name;
        }


        if (idx == 0)
        {
            /*第一个。*/
            return "<div " + divAttr + " >" + lab + "<a href=\"javascript:Down('" + this.FK_MapData + "','" + attr.MyPK + "','1');\" ><img src='../../Images/Btn/Right.gif' class='Arrow' alt='向右动顺序' border=0/></a></div>";
        }

        if (idx == count - 1)
        {
            /*到数第一个。*/
            return "<div " + divAttr + " ><a href=\"javascript:Up('" + this.FK_MapData + "','" + attr.MyPK + "','1');\" ><img src='../../Images/Btn/Left.gif' alt='向左移动顺序' class='Arrow' border=0/></a>" + lab + "</div>";
        }
        return "<div " + divAttr + " ><a href=\"javascript:Up('" + this.FK_MapData + "','" + attr.MyPK + "','1');\" ><img src='../../Images/Btn/Left.gif' alt='向下移动顺序' class='Arrow' border=0/></a>" + lab + "<a href=\"javascript:Down('" + this.FK_MapData + "','" + attr.MyPK + "','1');\" ><img src='../../Images/Btn/Right.gif' alt='向右移动顺序' class='Arrow' border=0/></a></div>";

        //if (idx == 0)
        //{
        //    /*第一个。*/
        //    return "<div " + divAttr + " >" + lab + "</div>";
        //}

        //if (idx == count - 1)
        //{
        //    /*到数第一个。*/
        //    return "<div " + divAttr + " >" + lab + "</div>";
        //}
        //return "<div " + divAttr + " >" + lab + "</div>";
    }
    public string GenerLab_bak(MapAttr attr, int idx, int i, int count)
    {
        string divAttr = " onDragEnd=onDragEndF('" + attr.MyPK + "','" + attr.GroupID + "')  onDrag=onDragF('" + attr.MyPK + "','" + attr.GroupID + "')  onMouseUp=alert('sss'); onmouseover=FieldOnMouseOver('" + attr.MyPK + "','" + attr.GroupID + "') onmouseout=FieldOnMouseOut('" + attr.MyPK + "','" + attr.GroupID + "') ";
        string lab = attr.Name;
        if (attr.MyDataType == DataType.AppBoolean && attr.UIIsLine)
            lab = "编辑";

        bool isLeft = true;
        if (i == 1)
            isLeft = false;

        if (attr.HisEditType == EditType.Edit || attr.HisEditType == EditType.UnDel)
        {
            switch (attr.LGType)
            {
                case FieldTypeS.Normal:
                    lab = "<a  href=\"javascript:Edit('" + this.FK_MapData + "','" + attr.MyPK + "','" + attr.MyDataType + "');\">" + lab + "</a>";
                    break;
                case FieldTypeS.FK:
                    lab = "<a  href=\"javascript:EditTable('" + this.FK_MapData + "','" + attr.MyPK + "','" + attr.MyDataType + "');\">" + lab + "</a>";
                    break;
                case FieldTypeS.Enum:
                    lab = "<a  href=\"javascript:EditEnum('" + this.FK_MapData + "','" + attr.MyPK + "','" + attr.MyDataType + "');\">" + lab + "</a>";
                    break;
                default:
                    break;
            }
        }
        else
        {
            lab = attr.Name;
        }

        if (idx == 0)
        {
            /*第一个。*/
            return "<div " + divAttr + " >" + lab + "<a href=\"javascript:Down('" + this.FK_MapData + "','" + attr.MyPK + "','1');\" ><img src='../../Images/Btn/Right.gif' class='Arrow' alt='向右动顺序' border=0/></a></div>";
        }

        if (idx == count - 1)
        {
            /*到数第一个。*/
            return "<div " + divAttr + " ><a href=\"javascript:Up('" + this.FK_MapData + "','" + attr.MyPK + "','1');\" ><img src='../../Images/Btn/Left.gif' alt='向左移动顺序' class='Arrow' border=0/></a>" + lab + "</div>";
        }
        return "<div " + divAttr + " ><a href=\"javascript:Up('" + this.FK_MapData + "','" + attr.MyPK + "','1');\" ><img src='../../Images/Btn/Left.gif' alt='向下移动顺序' class='Arrow' border=0/></a>" + lab + "<a href=\"javascript:Down('" + this.FK_MapData + "','" + attr.MyPK + "','1');\" ><img src='../../Images/Btn/Right.gif' alt='向右移动顺序' class='Arrow' border=0/></a></div>";
    }
}
