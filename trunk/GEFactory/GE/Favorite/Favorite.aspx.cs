﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.En;

public partial class GE_Favorite_AddFavorite : BP.Web.WebPage
{
    DropDownList ddlFavName = new DropDownList();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(BP.Web.WebUser.No))
        {
            BP.GE.GeFun.ShowMessage(this.Page, "strJS6", "对不起请登录!");
        }
        else
        {
            if (Request.QueryString["FavID"] != null)
            {
                if (!Page.IsPostBack)
                {
                    hidden.Value = Convert.ToString(Request.QueryString["FavID"]);
                    hidden2.Value = Convert.ToString(Request.QueryString["name"]);
                }
                DoSearch(Request.QueryString["FavID"].ToString());
            }
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["url"] != null)
                {
                    btnAdd.Visible = true;
                    btnCancel.Visible = true;
                    divInput.Visible = true;
                    txtInput.Focus();
                    btnCreate.Visible = false;
                    btnRename.Visible = false;
                    btnDelete.Visible = false;
                    btnAdd.Text = "保存";
                }
                else
                {
                    divInput.Visible = false;
                    btnCancel.Visible = false;
                    btnAdd.Visible = false;
                    btnCreate.Visible = true;
                    btnRename.Visible = true;
                    btnDelete.Visible = true;
                    btnAdd.Text = "收藏";
                }
            }
            else
            {
                string strVal1 = Convert.ToString(Request.Form["hidden"]);
                string strVal2 = Convert.ToString(Request.Form["hidden2"]);
                string strVal3 = Convert.ToString(Request.Form["hidden3"]);
                if (strVal3 == "OPEN")
                {
                    Response.Redirect("Favorite.aspx?FavID=" + strVal1 + "&name=" + strVal2);
                }
            }
            FillData();
        }
    }

    private void FillData()
    {
        BP.GE.GEFavNames ens = new BP.GE.GEFavNames();
        ens.Retrieve(BP.GE.GEFavNameAttr.FK_Emp, BP.Web.WebUser.No);
        this.Pub1.Controls.Clear();
        this.Pub1.AddTable("id='myTable' width=100%");
        //this.Pub1.AddTR("onclick='span_Onclick(this)' ondblclick='tr_Ondblclick(this)'");
        this.Pub1.AddTR("onclick='tr_Ondblclick(this)'");
        string strHTML = "<span name=myspan id=my value='-1'><img src='notify_close.gif' />我的收藏</span>";
        this.Pub1.AddTD(strHTML);
        this.Pub1.AddTREnd();
        foreach (BP.GE.GEFavName en in ens)
        {
            //this.Pub1.AddTR("onclick='span_Onclick(this)' ondblclick='tr_Ondblclick(this)'");
            this.Pub1.AddTR("onclick='tr_Ondblclick(this)'");
            strHTML = "<span style='width:100%' name='" + en.Name + "' id=my value='" + en.OID + "'><img src='notify_close.gif' />" + en.Name + "</span>";
            this.Pub1.AddTD(strHTML);
            this.Pub1.AddTREnd();
        }
        this.Pub1.AddTableEnd();
    }

    protected void btnCreate_Click(object sender, EventArgs e)
    {
        if (btnCreate.Text.Trim() == "新建")
        {
            txtInput.Text = string.Empty;
            btnCreate.Text = "保存";
            divInput.Visible = true;
            txtInput.Focus();
            btnRename.Visible = false;
            btnDelete.Visible = false;
            btnAdd.Visible = false;
            btnCancel.Visible = true;
        }
        else if (btnCreate.Text.Trim() == "保存")
        {
            BP.GE.GEFavName en = new BP.GE.GEFavName();
            en.Name = txtInput.Text;
            en.FK_Emp = BP.Web.WebUser.No;
            en.Insert();
            FillData();
            FillddlFavName();

            ddlFavName.SelectedIndex = 0;
            btnCreate.Text = "新建";
            divInput.Visible = false;
            btnRename.Visible = true;
            btnDelete.Visible = true;
            btnAdd.Visible = false;
            btnCancel.Visible = false;
        }
    }

    private void FillddlFavName()
    {
        //更新dropdownlist的数据
        ddlFavName.Items.Clear();
        BP.GE.GEFavNames ens = new BP.GE.GEFavNames();
        ens.Retrieve(BP.GE.GEFavNameAttr.FK_Emp, BP.Web.WebUser.No);
        ListItem li = new ListItem("请选择", "-1");
        ddlFavName.Items.Add(li);
        foreach (BP.GE.GEFavName en1 in ens)
        {
            li = new ListItem(en1.Name, en1.OID.ToString());
            ddlFavName.Items.Add(li);
        }
    }

    protected void btnRename_Click(object sender, EventArgs e)
    {
        if (btnRename.Text.Trim() == "重命名")
        {
            if (hidden2.Value == string.Empty || hidden.Value == "-1")
            {
                BP.GE.GeFun.ShowMessage(this.Page, "strJS4", "请选择分类");
            }
            else
            {
                btnRename.Text = "保存";
                divInput.Visible = true;
                txtInput.Focus();
                btnCreate.Visible = false;
                btnDelete.Visible = false;
                btnAdd.Visible = false;
                btnCancel.Visible = true;
                txtInput.Text = hidden2.Value;
            }
        }
        else if (btnRename.Text.Trim() == "保存")
        {
            //保存重命名
            BP.GE.GEFavName en = new BP.GE.GEFavName();
            en.OID = Convert.ToInt32(hidden.Value);
            en.Name = txtInput.Text;
            en.FK_Emp = BP.Web.WebUser.No;
            en.Update();
            hidden2.Value = txtInput.Text;
            btnRename.Text = "重命名";
            divInput.Visible = false;
            btnCreate.Visible = true;
            btnDelete.Visible = true;
            btnAdd.Visible = false;
            btnCancel.Visible = false;
            FillData();
            FillddlFavName();
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (hidden.Value == string.Empty)
        {
            BP.GE.GeFun.ShowMessage(this.Page, "strJS4", "请选择分类");
        }
        else
        {
            BP.GE.GEFavName en = new BP.GE.GEFavName();
            en.Delete(BP.GE.GEFavNameAttr.OID, Convert.ToInt32(hidden.Value));
            BP.GE.GEFavLists ens = new BP.GE.GEFavLists();
            ens.Delete(BP.GE.GEFavListAttr.FK_FavNameID, Convert.ToInt32(hidden.Value));
            //FillData();
            //FillddlFavName();
            //DoSearch("-1");
            //hidden.Value = string.Empty;
            string strJS="window.location.href=window.location.href";
            this.ClientScript.RegisterStartupScript(this.GetType(), "js1", strJS, true);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        divInput.Visible = false;
        btnCreate.Text = "新建";
        btnCreate.Visible = true;
        btnRename.Text = "重命名";
        btnRename.Visible = true;
        btnDelete.Visible = true;
        btnAdd.Visible = false;
        btnCancel.Visible = false;
    }
    private void DoSearch(string strFKID)
    {
        BP.GE.GEFavLists favLists = new BP.GE.GEFavLists();
        QueryObject qo = new QueryObject(favLists);
        qo.AddWhere(BP.GE.GEFavListAttr.FK_Emp, BP.Web.WebUser.No);
        if (strFKID != "-1")
        {
            qo.addAnd();
            qo.AddWhere(BP.GE.GEFavListAttr.FK_FavNameID, Convert.ToInt32(strFKID));
        }
        this.Pub3.BindPageIdx(qo.GetCount(), 20, this.PageIdx, "Favorite.aspx?FavID=" + strFKID);
        qo.DoQuery("OID", 20, this.PageIdx);

        BP.GE.GEFavNames favNames = new BP.GE.GEFavNames();
        qo = new QueryObject(favNames);
        qo.AddWhere(BP.GE.GEFavNameAttr.FK_Emp, Convert.ToString(Session["No"]));
        qo.DoQuery();

        this.Pub2.AddTable("width=100%");
        this.Pub2.AddTR("align='center'");
        this.Pub2.AddTD("width='10%'", "<b>选择</b>");
        this.Pub2.AddTD("width='60%' align='center'", "<b>名称</b>");
        this.Pub2.AddTD("width='15%' align='center'", "<b>分类</b>");
        this.Pub2.AddTD("width='15%' align='center'", "<b>日期</b>");
        this.Pub2.AddTREnd();

        foreach (BP.GE.GEFavList en in favLists)
        {
            this.Pub2.AddTR("align='center'");
            this.Pub2.AddTD("<input type='checkbox' id='chk' value='" + en.OID + "' onclick='chk_Click(this)' />");
            string strTitle = "<a href='" + en.Url + "' target='_blank'>" + en.Title + "</a>";
            this.Pub2.AddTD(strTitle);
            this.Pub2.AddTD(GetFavName(en.FK_FavNameID, favNames));
            this.Pub2.AddTD(Convert.ToDateTime(en.RDT).ToString("yyyy-MM-dd"));
            this.Pub2.AddTREnd();
        }
        this.Pub2.AddTableEnd();

        Pub3.Add("<div style='float:right'>");
        Button btnDel = new Button();
        btnDel.Text = "删除收藏";
        btnDel.OnClientClick = "return DelConfirm()";
        btnDel.Click += new EventHandler(btnDel_Click);
        Pub3.Controls.Add(btnDel);
        
        FillddlFavName();
        this.Pub3.Controls.Add(ddlFavName);

        Button btnMove = new Button();
        btnMove.Text = "移动";
        btnMove.Click += new EventHandler(btnMove_Click);
        Pub3.Controls.Add(btnMove);
        Pub3.Add("</div>");
    }

    void btnMove_Click(object sender, EventArgs e)
    {
        string[] strs = hidden3.Value.Split(',');
        for (int i = 0; i < strs.Length - 1; i++)
        {
            BP.GE.GEFavList en = new BP.GE.GEFavList();
            en.OID = Convert.ToInt32(strs[i]);
            en.Update(BP.GE.GEFavListAttr.FK_FavNameID, Convert.ToInt32(ddlFavName.SelectedValue));
        }
        //DoSearch(Request.QueryString["FavID"].ToString());
        Response.Redirect("Favorite.aspx?FavID=" + hidden.Value);
    }

    void btnDel_Click(object sender, EventArgs e)
    {
        string[] strs = hidden3.Value.Split(',');
        for (int i = 0; i < strs.Length - 1; i++)
        {
            BP.GE.GEFavList en = new BP.GE.GEFavList();
            en.Delete(BP.GE.GEFavListAttr.OID, Convert.ToInt32(strs[i]));
        }
        //DoSearch(Request.QueryString["FavID"].ToString());
        Response.Redirect("Favorite.aspx?FavID=" + hidden.Value);
    }

    private string GetFavName(int id, BP.GE.GEFavNames favNames)
    {
        string strName = string.Empty;
        foreach (BP.GE.GEFavName en in favNames)
        {
            if (id == en.OID)
            {
                strName = en.Name;
                return strName;
            }
        }
        return strName;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Write(hidden.Value);
        if (!string.IsNullOrEmpty(hidden.Value))
        {
            Response.Redirect("Favorite.aspx?FavID=" + hidden.Value);
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (btnAdd.Text == "保存")
        {
            if (!string.IsNullOrEmpty(BP.Web.WebUser.No))
            {
                if (hidden.Value.Trim() != string.Empty)
                {
                    BP.GE.GEFavList en = new BP.GE.GEFavList();
                    en.FK_Emp = BP.Web.WebUser.No;
                    en.RDT = DateTime.Now.ToString();
                    en.Title = txtInput.Text;
                    en.FK_FavNameID = Convert.ToInt32(hidden.Value);
                    en.Url = Request.QueryString["url"].ToString();
                    en.Insert();
                    BP.GE.GeFun.ShowMessage(this.Page, "strJS3", "收藏成功!");
                    divInput.Visible = false;
                    btnCreate.Visible = true;
                    btnRename.Visible = true;
                    btnDelete.Visible = true;
                    btnAdd.Visible = false;
                    btnCancel.Visible = false;
                    btnAdd.Text = "收藏";
                }
                else
                {
                    BP.GE.GeFun.ShowMessage(this.Page, "strJS2", "请选择收藏分类!");
                }
            }
            else
            {
                BP.GE.GeFun.ShowMessage(this.Page, "strJS1", "对不起请登录!");
            }
        }
        else if (btnAdd.Text == "收藏")
        {
            btnAdd.Text = "保存";
        }
        //FillData();
    }
}