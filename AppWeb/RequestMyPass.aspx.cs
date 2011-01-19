using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using BP.En.Base;
using BP.En;
using BP.DA;
using BP.Web;
using BP.Web.Controls;
using BP.Sys;
using BP.YG;

namespace HiTax
{
	/// <summary>
	/// RequestMyPass 的摘要说明。
	/// </summary>
	public partial class RequestMyPass : YGPage
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
		}

		#region Web 窗体设计器生成的代码
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion

		protected void Button1_Click(object sender, System.EventArgs e)
		{
			Customer c = new Customer();
			if (c.IsExit("No",this.TB_No.Text))
			{
				ToMsgPage("系统已经把您的密码发送到["+c.Email+"]，请查收。其它：如果您的信息");
				//this.Response.Write("<script language=javascript >alert('系统已经把您的密码发送到[]，请查收。');</script>");
				//this.Response.Redirect("Login.aspx",true);
			}
			else
			{
				this.Label1.Text="用户名错误。";
			}
		}
	}
}
