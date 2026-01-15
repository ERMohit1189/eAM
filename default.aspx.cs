using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net.NetworkInformation;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class index : System.Web.UI.Page
{
	SqlConnection con;
	readonly Campus oo = new Campus();
	string sql = "";//   

	protected void Page_Load(object sender, EventArgs e)
	{
		try
		{
			Session.Clear();
			con = new SqlConnection();
			con = BAL.objBal.dbGet_connection();
			txtUserName.Focus();
			setwallpaper();

			if (!IsPostBack)
			{
				hdnSubscription.Value = "";
				sql = "SELECT COUNT(*) CNT FROM tblManagerInfo";
				if (oo.ReturnTag(sql, "CNT") == "0")
				{
					Expbody.Visible = true;
					alert.InnerHtml = "<i class='fa fa-warning'></i><br /> Subscription expired, please contact administrator!";
					hdnSubscription.Value = "Exp";
				}
				else
				{
					sql = "SELECT (case when convert(date, dob)<convert(date, getdate()) then 'Exp' else 'Liv' end)sts FROM tblManagerInfo";
					if (oo.ReturnTag(sql, "sts") == "Exp")
					{
						Expbody.Visible = true;
						alert.InnerHtml = "<i class='fa fa-warning'></i><br /> Subscription expired, please contact administrator!";
						hdnSubscription.Value = "Exp";
					}
				}


				List<SqlParameter> param = new List<SqlParameter>
				{
					new SqlParameter("@Queryfor", "S")
				};
				if (Session["BranchCode"] == null || Session["BranchCode"].ToString() == "")
				{
				}
				else
				{
					param.Add(new SqlParameter("@BranchCode", Session["BranchCode"]));
				}
				DataSet ds = new DataSet();
				ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("Get_GenralInfo", param);
				DrpBranchName.DataSource = ds.Tables[0];
				DrpBranchName.DataTextField = "BranchName";
				DrpBranchName.DataValueField = "BranchId";
				DrpBranchName.DataBind();
				if (DrpBranchName.Items.Count == 0)
				{
					DrpBranchName.Items.Insert(0, new ListItem("", "1"));
				}

				DrpSessionName.DataSource = ds.Tables[1];
				DrpSessionName.DataTextField = "SessionName";
				DrpSessionName.DataValueField = "SessionName";
				DrpSessionName.DataBind();
				if (ds.Tables[1].Rows.Count > 0)
				{
					DrpSessionName.SelectedIndex = (DrpSessionName.Items.Count - 1);
					//if (ds.Tables[2].Rows.Count > 0)
					//{
					//    DrpSessionName.Text = ds.Tables[2].Rows[0][0].ToString();
					//}
					//else
					//{
					//    DrpSessionName.SelectedIndex = (DrpSessionName.Items.Count-1);
					//}
				}
				if (Request.Cookies["UserName"] != null && Request.Cookies["Password"] != null)
				{
					chkRememberMe.Checked = true;
					txtUserName.Text = Request.Cookies["UserName"].Value;
					txtPassword.Attributes["value"] = Request.Cookies["Password"].Value;
					LinkButton1.Focus();
				}
				else
				{
					chkRememberMe.Checked = false;
					txtUserName.Focus();
				}
				try
				{
					if (ds.Tables[3].Rows[0][0].ToString() != "")
					{
						lblCompanyName.Visible = true;
						Label1.Text = ds.Tables[3].Rows[0][0].ToString();
					}
				}
				catch (Exception)
				{
				}

				if (Session["logout"] != null)
				{
					lbllog.Text = Session["logout"].ToString();
					Session["logout"] = null;
					alert_success.Style.Add("display", "block");
				}

				Session["srno"] = null;
			}
		}
		catch (Exception)
		{
			alert_success.Style.Add("display", "none");
			alert_danger.Style.Add("display", "block");
			lblError.Text = "Please, try Again!";
		}

	}

	//SettingAttribute Wallpaper
	//SettingAttribute Wallpaper
	public void setwallpaper()
	{
		string bgImage = "";
		try
		{
			SqlDataAdapter sdaa = new SqlDataAdapter("select * from Login_Wallpager", con);
			DataTable dtt = new DataTable();
			sdaa.Fill(dtt);
			if (dtt.Rows.Count > 0)
			{
				if (dtt.Rows[0]["Wallpaper_Path"].ToString() == "no")
				{
					bgImage = "";
				}
				if (dtt.Rows[0]["Wallpaper_Path"].ToString() == "")
				{
					bgImage = "Uploads/LoginWallpaper/eAM_Default_bg.png";
				}
				if (dtt.Rows[0]["Wallpaper_Path"].ToString() != "no" && dtt.Rows[0]["Wallpaper_Path"].ToString() != "")
				{
					bgImage = dtt.Rows[0]["Wallpaper_Path"].ToString();
				}
				if (dtt.Rows[0]["Color"].ToString().Trim() != "" && dtt.Rows[0]["Wallpaper_Path"].ToString().Trim() != "")
				{
					//  pages.Style["background-color"] = dtt.Rows[0]["Color"].ToString();
					//  if (!File.Exists(Server.MapPath(dtt.Rows[0]["Wallpaper_Path"].ToString()))) return;
					pages.Style["background-image"] = bgImage;
					pages.Style["-webkit-background-size"] = "cover";
					pages.Style["-moz-background-size"] = "cover";
					pages.Style["-o-background-size"] = "cover";
					pages.Style["background-size"] = "cover";
					pages.Style["background-size"] = "100%";
				}
				else if (dtt.Rows[0]["Wallpaper_Path"].ToString().Trim() != "" && dtt.Rows[0]["Black_White"].ToString().Trim() != "")
				{
					// pages.Attributes["class"] = dtt.Rows[0]["Black_White"].ToString().Trim();
					// pages.Style["background-image"] = dtt.Rows[0]["Wallpaper_Path"].ToString();

					HtmlLink cssLink = new HtmlLink
					{
						Href = "css/loginwallStyleSheet.css"
					};
					cssLink.Attributes.Add("rel", "stylesheet");
					cssLink.Attributes.Add("type", "text/css");
					//  if (!File.Exists(Server.MapPath(dtt.Rows[0]["Wallpaper_Path"].ToString()))) return;
					cssLink.Style["background-image"] = bgImage;
					Page.Header.Controls.Add(cssLink);
				}
				else if (dtt.Rows[0]["Wallpaper_Path"].ToString().Trim() != "")
				{
					// if(!File.Exists(Server.MapPath(dtt.Rows[0]["Wallpaper_Path"].ToString()))) return;
					pages.Style["background-image"] = bgImage;
					pages.Style["-webkit-background-size"] = "cover";
					pages.Style[" -moz-background-size"] = "cover";
					pages.Style[" -o-background-size"] = "cover";
					pages.Style["background-size"] = "cover";
					pages.Style["background-size"] = "100%";
				}
				else
				{ }
			}
			else
			{
				bgImage = "Uploads/LoginWallpaper/eAM_Default_bg.png";
				pages.Style["background-image"] = bgImage;
				pages.Style["-webkit-background-size"] = "cover";
				pages.Style["-moz-background-size"] = "cover";
				pages.Style["-o-background-size"] = "cover";
				pages.Style["background-size"] = "cover";
				pages.Style["background-size"] = "100%";
			}
		}
		catch { }
	}

	protected void LinkButton1_Click(object sender, EventArgs e)
	{
		LinkButton1.Enabled = false;
		ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "btt();", true);
		//if (isredirect() == "Yes")
		//{
		if (chkRememberMe.Checked)
		{
			Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(30);
			Response.Cookies["Password"].Expires = DateTime.Now.AddDays(30);
		}
		else
		{
			Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(-1);
			Response.Cookies["Password"].Expires = DateTime.Now.AddDays(-1);
		}

		Response.Cookies["UserName"].Value = txtUserName.Text.Trim();
		Response.Cookies["Password"].Value = txtPassword.Text.Trim();

		List<SqlParameter> param = new List<SqlParameter>
		{
			new SqlParameter("@userName", txtUserName.Text.Trim()),
			new SqlParameter("@passWord", txtPassword.Text.Trim())
		};
		DataSet ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("get_LoginDetails", param);
		Page.Validate();
		if (Page.IsValid)
		{
			if (ds != null)
			{
				if (ds.Tables[0].Rows.Count > 0)
				{
					if (ds.Tables[0].Rows[0]["loginTypeId"].ToString() != "6" && hdnSubscription.Value == "Exp")
					{
						alert.InnerHtml = "<i class='fa fa-warning'></i><br /> Subscription expired, please contact administrator!";
						hdnSubscription.Value = "Exp";
						return;
					}
					Session["LoginName"] = txtUserName.Text.Trim();
					Session["Password"] = txtPassword.Text.Trim();
					Session["LoginId"] = ds.Tables[0].Rows[0]["LoginId"].ToString();
					Session["LoginTypeId"] = ds.Tables[0].Rows[0]["loginTypeId"].ToString();
					Session["BranchCode"] = (ds.Tables[1].Rows[0]["BranchCode"].ToString() == "" ? null : ds.Tables[0].Rows[0]["BranchId"].ToString());
					sql = "select BranchName from BranchTab where BranchId=" + ds.Tables[0].Rows[0]["BranchId"].ToString() + "";
					DrpSessionName.SelectedIndex = (DrpSessionName.Items.Count - 1);
					Session["SessionName"] = DrpSessionName.SelectedValue;
					if (ds.Tables[0].Rows[0]["loginTypeId"].ToString() != "1" && ds.Tables[0].Rows[0]["loginTypeId"].ToString() != "6")
					{
						Session["BranchName"] = (ds.Tables[1].Rows[0]["BranchCode"].ToString() == "" ? null : oo.ReturnTag(sql, "BranchName"));
						string sqls = "select top(1) SessionID, SessionName from SessionMaster where BranchCode=" + ds.Tables[0].Rows[0]["BranchId"].ToString() + " and convert(date,GETDATE()) between convert(date,FromDate) and convert(date,ToDate) order by SessionID desc";
						if (!oo.Duplicate(sqls))
						{
							sqls = "select top(1) SessionID, SessionName from SessionMaster where BranchCode=" + ds.Tables[0].Rows[0]["BranchId"].ToString() + " order by SessionID desc";
						}
						Session["SessionName"] = oo.ReturnTag(sqls, "SessionName");
						Session["SessionID"] = oo.ReturnTag(sqls, "SessionID");
					}
					sql = "select distinct GroupId from dt_ClassGroupMaster where (GroupId='G1' or GroupId='G2') and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
					var GroupId = oo.ReturnTag(sql, "GroupId");
					Session["ClassGroup"] = string.IsNullOrEmpty(GroupId) ? "G0" : GroupId;
					if (ds.Tables[0].Rows[0]["loginTypeId"].ToString() == "4")
					{
						Session["Srno"] = txtUserName.Text.Trim();
					}
					if (ds.Tables[0].Rows[0]["loginTypeId"].ToString() == "5")
					{
						string st = "select srno from GuardianLoginandPassword where UserName='" + txtUserName.Text.Trim() + "'";
						Session["Srno"] = oo.ReturnTag(st, "srno");
					}
					alert_danger.Style.Add("display", "none");
					lbllog.Text = "Log in successful. Loading Dashboard.";
					alert_success.Style.Add("display", "block");

					Session["Expire"] = "TimeOUT";
					Session["DisplayName"] = ds.Tables[1].Rows[0]["DisplayName"].ToString();
					Session["ImageUrl"] = ds.Tables[1].Rows[0]["PhotoPath"].ToString();
					Session["Logintype"] = ds.Tables[1].Rows[0]["Logintype"].ToString();
					ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "window.location='" + Campus.GetSiteRoot() + ds.Tables[1].Rows[0][3].ToString() + "';", true);
				}
				else
				{
					alert_success.Style.Add("display", "none");
					alert_danger.Style.Add("display", "block");
					lblError.Text = "You've entered incorrect Username or Password.";
					LinkButton1.Enabled = true;
				}
			}
			else
			{
				alert_success.Style.Add("display", "none");
				alert_danger.Style.Add("display", "block");
				lblError.Text = "You've entered incorrect Username or Password.";
				LinkButton1.Enabled = true;

			}
		}
		else
		{
			return;
		}

	}

	public static string GetSiteRoot()
	{
		string port = System.Web.HttpContext.Current.Request.ServerVariables["SERVER_PORT"];
		if (port == null || port == "80" || port == "443")
			port = "";
		else
			port = ":" + port;

		string protocol = System.Web.HttpContext.Current.Request.ServerVariables["SERVER_PORT_SECURE"];
		if (protocol == null || protocol == "0")
			protocol = "http://";
		else
			protocol = "https://";

		string sOut = protocol + System.Web.HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + port + System.Web.HttpContext.Current.Request.ApplicationPath;

		if (sOut.EndsWith("/"))
		{
			sOut = sOut.Substring(0, sOut.Length - 1);
		}

		return sOut;
	}

	protected void DrpSessionName_SelectedIndexChanged(object sender, EventArgs e)
	{

	}


	public bool MacIDStore()
	{
		NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
		string xx = nics[0].GetPhysicalAddress().ToString();
		sql = "select seq from student";
		bool flag;
		if (oo.Duplicate(sql))
		{
			if (xx == oo.ReturnTag(sql, "seq").Trim())
			{
				flag = true;
				Session["RunProject"] = "Yes";
			}
			else
			{
				flag = false;
			}
		}
		else
		{
			sql = "Insert into Student (Seq) values ('" + xx + "')";
			flag = true;
		}
		oo.ProcedureDatabase(sql);
		return flag;
	}

	public void Validation()
	{
		string sql = "select NoofDay from securityTab where StartDate='" + oo.CurrentDate() + "'";

		if (oo.Duplicate(sql) == false)
		{
			sql = "select max(count) as  cc from SecurityTab";
			string co = oo.ReturnTag(sql, "cc");
			sql = "select NoofDay from SecurityTab where id=1";
			int max = Convert.ToInt32(oo.ReturnTag(sql, "NoofDay"));
			int pp = Convert.ToInt32(co);
			pp++;
			if (pp <= max)
			{
				sql = "Insert into SecurityTab (Id,StartDate,NoofDay,count) values (1,'" + oo.CurrentDate() + "','15'," + pp + ")";

				oo.ProcedureDatabase(sql);
				Session["Expire"] = "TimeOUT";
			}
			else
			{
				Session["Expire"] = "";
			}


		}
	}

	public string Isredirect()
	{
		string macArrdess = BAL.objBal.GetMACAddress();
		string motherBoardSrno = BAL.objBal.GetMotherBoardSrno();
		sql = "Select ISNULL(isSysUser,0) isSysUser,macId,motherboardno from NewAdminInformation where isSysUser=1 and";
		sql += " UserId='" + txtUserName.Text.Trim() + "' and Password='" + txtPassword.Attributes["value"] + "'";


		string redirect;
		if (BAL.objBal.ReturnTag(sql, "isSysUser") == "True")
		{
			if (macArrdess.Trim() == BAL.objBal.ReturnTag(sql, "macId").Trim() && motherBoardSrno.Trim() == BAL.objBal.ReturnTag(sql, "motherboardno").Trim())
			{
				redirect = "Yes";
			}
			else
			{
				redirect = "No";
			}
		}
		else
		{
			redirect = "Yes";
		}

		return redirect;
	}
}
