﻿using System;
using System.IO;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using BP.Web;
using BP.Sys;
using BP.DA;
public partial class WF_FreeFrm_UploadFile : WebPage
{
    /// <summary>
    /// ath.
    /// </summary>
    public string Ath
    {
        get
        {
            return this.Request.QueryString["Ath"];
        }
    }
    public string PKVal
    {
        get
        {
            return this.Request.QueryString["PKVal"];
        }
    }

    public string DelPKVal
    {
        get
        {
            return this.Request.QueryString["DelPKVal"];
        }
    }
    public string FK_FrmAttachment
    {
        get
        {
            return this.Request.QueryString["FK_FrmAttachment"];
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        if (this.DoType == "Del")
        {
            FrmAttachmentDB delDB = new FrmAttachmentDB();
            delDB.MyPK = this.DelPKVal;
            delDB.DirectDelete();
        }
        if (this.DoType == "Down")
        {
            FrmAttachmentDB downDB = new FrmAttachmentDB();
            downDB.MyPK = this.MyPK;
            downDB.Retrieve();
            BP.PubClass.DownloadFile(downDB.FileFullName, downDB.FileName);
            this.WinClose();
            return;
        }

        this.Pub1.AddTable("width='100%'");
        this.Pub1.AddTR();
        this.Pub1.AddTDTitle("IDX");
        this.Pub1.AddTDTitle("文件名");
        this.Pub1.AddTDTitle("大小KB");
        this.Pub1.AddTDTitle("上传日期");
        this.Pub1.AddTDTitle("上传人");
        this.Pub1.AddTDTitle("操作");
        this.Pub1.AddTREnd();

        BP.Sys.FrmAttachmentDBs dbs = new BP.Sys.FrmAttachmentDBs();
        dbs.Retrieve(FrmAttachmentDBAttr.FK_FrmAttachment, this.FK_FrmAttachment,
            FrmAttachmentDBAttr.RefPKVal, this.PKVal);

        BP.Sys.FrmAttachment athDesc = new BP.Sys.FrmAttachment(this.FK_FrmAttachment);
        int i = 1;
        foreach (FrmAttachmentDB db in dbs)
        {
            this.Pub1.AddTR();
            this.Pub1.AddTDIdx(i++);
            if (athDesc.IsDownload)
            {
                this.Pub1.AddTD("<a href='AttachmentUpload.aspx?DoType=Down&MyPK=" + db.MyPK + "' target=_blank><img src='../../Images/FileType/" + db.FileExts + ".gif' border=0 onerror=\"src='../../Images/FileType/Undefined.gif'\" />" + db.FileName + "</a>");
            }
            else
            {
                this.Pub1.AddTD(db.FileName);
            }

            this.Pub1.AddTD(db.FileSize);
            this.Pub1.AddTD(db.RDT);
            this.Pub1.AddTD(db.RecName);
            if (athDesc.IsDelete)
                this.Pub1.AddTD("<a href=\"javascript:Del('" + this.FK_FrmAttachment + "','" + this.PKVal + "','" + db.MyPK + "')\">删除</a>");
            else
                this.Pub1.AddTD("");
            this.Pub1.AddTREnd();
        }
        if (athDesc.IsUpload)
        {
            this.Pub1.AddTR();
            this.Pub1.AddTD();
            this.Pub1.AddTDBegin("colspan=5");
            System.Web.UI.WebControls.FileUpload fu = new System.Web.UI.WebControls.FileUpload();
            fu.ID = "file";
            this.Pub1.Add(fu);

            Button btn = new Button();
            btn.Text = "上传";
            btn.ID = "Btn_Upload";
            btn.Click += new EventHandler(btn_Click);
            this.Pub1.Add(btn);
            this.Pub1.AddTDEnd();
            this.Pub1.AddTREnd();
        }
        this.Pub1.AddTableEnd();
    }

    void btn_Click(object sender, EventArgs e)
    {
        BP.Sys.FrmAttachment athDesc = new BP.Sys.FrmAttachment(this.FK_FrmAttachment);
        System.Web.UI.WebControls.FileUpload fu = this.Pub1.FindControl("file") as System.Web.UI.WebControls.FileUpload;
        if (fu.HasFile == false || fu.FileName.Length <= 2)
        {
            this.Alert("请选择上传的文件.");
            return;
        }

        if (System.IO.Directory.Exists(athDesc.SaveTo) == false)
            System.IO.Directory.CreateDirectory(athDesc.SaveTo);

        int oid = BP.DA.DBAccess.GenerOID();

        string exp = "";

        string saveTo = athDesc.SaveTo + "\\" + oid + "." + fu.FileName.Substring(fu.FileName.LastIndexOf('.') + 1);
        fu.SaveAs(saveTo);

        FileInfo info = new FileInfo(saveTo);
        FrmAttachmentDB dbUpload = new FrmAttachmentDB();
        dbUpload.MyPK = athDesc.FK_MapData + oid.ToString();
        dbUpload.FK_FrmAttachment = this.FK_FrmAttachment;
        dbUpload.RefPKVal = this.PKVal.ToString();
        dbUpload.FK_MapData = athDesc.FK_MapData;

        dbUpload.FileExts = info.Extension;
        dbUpload.FileFullName = saveTo;
        dbUpload.FileName = fu.FileName;
        dbUpload.FileSize = (float)info.Length;

        dbUpload.RDT = DataType.CurrentDataTime;
        dbUpload.Rec = BP.Web.WebUser.No;
        dbUpload.RecName = BP.Web.WebUser.Name;
        dbUpload.Insert();
        this.Response.Redirect("AttachmentUpload.aspx?FK_FrmAttachment=" + this.FK_FrmAttachment + "&PKVal=" + this.PKVal, true);
    }
}