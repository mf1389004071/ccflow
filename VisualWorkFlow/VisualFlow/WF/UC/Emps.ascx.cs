﻿using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.IO;
using System.Drawing;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using BP.WF;
using BP.Sys;
using BP.Web.Controls;
using BP.DA;
using BP.En;
using BP.Web;

public partial class WF_UC_Emps : BP.Web.UC.UCBase3
{
    public void GenerAllImg()
    {
        BP.WF.Port.WFEmps empWFs = new BP.WF.Port.WFEmps();
        empWFs.RetrieveAll();
        foreach (BP.WF.Port.WFEmp emp in empWFs)
        {
            if (System.IO.File.Exists(BP.SystemConfig.PathOfData + "\\Siganture\\" + emp.No + ".JPG")
                || System.IO.File.Exists(BP.SystemConfig.PathOfData + "\\Siganture\\" + emp.Name + ".JPG"))
            {
                continue;
            }

            string path = BP.SystemConfig.PathOfData + "\\Siganture\\T.JPG";
            string pathMe = BP.SystemConfig.PathOfData + "\\Siganture\\" + emp.No + ".JPG";
            File.Copy(BP.SystemConfig.PathOfData + "\\Siganture\\Templete.JPG",
                path, true);

            string fontName = "宋体";
            System.Drawing.Image img = System.Drawing.Image.FromFile(path);
            Font font = new Font(fontName, 15);
            Graphics g = Graphics.FromImage(img);
            System.Drawing.SolidBrush drawBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
            System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat(StringFormatFlags.DirectionVertical);//文本
            g.DrawString(emp.Name, font, drawBrush, 3, 3);

            try
            {
                File.Delete(pathMe);
            }
            catch
            {
            }
            img.Save(pathMe);
            img.Dispose();
            g.Dispose();

            File.Copy(pathMe,
            BP.SystemConfig.PathOfData + "\\Siganture\\" + emp.Name + ".JPG", true);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Page.Title = "成员";
        if (WebUser.IsWap)
        {
            this.BindWap();
            return;
        }

        string sql = "SELECT a.No,a.Name, b.Name as DeptName FROM Port_Emp a, Port_Dept b WHERE a.FK_Dept=b.No ORDER  BY a.FK_Dept ";
        DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(sql);

        BP.WF.Port.WFEmps emps = new BP.WF.Port.WFEmps();
        if (this.DoType != null)
            emps.RetrieveAllFromDBSource();
        else
            emps.RetrieveAll();


        this.AddTable("width=80% align=center border=1");
        if (WebUser.IsWap)
            this.AddCaptionLeft("<a href='Home.aspx'><img src='./Img/Home.gif' border=0/>Home</a>");

        this.AddTR();
        this.AddTDTitle("IDX");
        this.AddTDTitle("部门");
        this.AddTDTitle("人员");
        this.AddTDTitle("电话");
        this.AddTDTitle("Email");
        this.AddTDTitle("岗位 <a href=Emps.aspx?DoType=1>刷新</a>");
        this.AddTDTitle("签名");
        if (WebUser.No == "admin")
            this.AddTDTitle("顺序");


        if (this.DoType != null)
        {
            BP.WF.Port.WFEmp.DTSData();
            this.GenerAllImg();
        }
        this.AddTREnd();

        string deptName = null;
        int idx = 0;
        foreach (DataRow dr in dt.Rows)
        {
            string fk_emp = dr["No"].ToString();
            if (fk_emp == "admin")
                continue;

            idx++;
            if (dr["DeptName"].ToString() != deptName)
            {
                deptName = dr["DeptName"].ToString();
                this.AddTRSum();
                this.AddTDIdx(idx);
                this.AddTD(deptName);
            }
            else
            {
                this.AddTR();
                this.AddTDIdx(idx);
                this.AddTD();
            }

            if (Glo.IsShowUserNoOnly)
                this.AddTD("<a href=\"javascript:DoAutoTo('" + fk_emp + "','')\" >" + fk_emp + "</a>");
            else
                this.AddTD(fk_emp + "-" + dr["Name"]);


            BP.WF.Port.WFEmp emp = emps.GetEntityByKey(fk_emp) as BP.WF.Port.WFEmp;
            if (emp != null)
            {
                this.AddTD(emp.TelHtml);
                this.AddTD(emp.EmailHtml);
                this.AddTD(emp.Stas);
            }
            else
            {
                this.AddTD("");
                this.AddTD("");
                this.AddTD("");
                // BP.WF.Port.WFEmp.DTSData();
                break;
                //  this.Response.Redirect(this.Request.RawUrl, true);
            }

            this.AddTD("<img src='../Data/Siganture/" + fk_emp + ".jpg' border=1 onerror=\"this.src='../Data/Siganture/UnName.jpg'\"/>");

            if (WebUser.No == "admin")
            {
                this.AddTD("<a href=\"javascript:DoUp('" + emp.No + "')\" >上移</a>-<a href=\"javascript:DoDown('" + emp.No + "')\" >下移</a>");
            }
            //  this.AddTD(emp.hisst);
            this.AddTREnd();
        }
        this.AddTableEnd();
    }
    public void BindWap()
    {
        this.AddTable("width=100% align=center");
        this.AddTR();
        this.AddTDTitle("colspan=4 align=left","<a href='Home.aspx'><img src='./Img/Home.gif' border=0/>Home</a> - 成员");
        this.AddTREnd();

        BP.Port.Depts depts = new BP.Port.Depts();
        depts.RetrieveAllFromDBSource();

        BP.WF.Port.WFEmps emps = new BP.WF.Port.WFEmps();
        emps.RetrieveAllFromDBSource();

      // BP.WF.Port.WFEmp.DTSData();

        int idx = 0;
        foreach (BP.Port.Dept dept in depts)
        {
            this.AddTRSum();
            this.AddTD("colspan=4", dept.Name);
            this.AddTREnd();
            foreach (BP.WF.Port.WFEmp emp in emps)
            {
                if (emp.FK_Dept != dept.No)
                    continue;

                idx++;
                this.AddTR();
                this.AddTD(idx);
                this.AddTD(emp.Name);
                this.AddTD(emp.Tel);
                this.AddTD(emp.Stas);
                this.AddTREnd();
            }
        }
        this.AddTableEnd();
    }
}
