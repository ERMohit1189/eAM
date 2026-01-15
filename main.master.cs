using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml.XPath;
using System.Web.Services;

public partial class main : System.Web.UI.MasterPage
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        
        Session["logout"] = null;

        if (!IsPostBack)
        {
            if (Session["LoginType"].ToString().ToLower() == "staff" || Session["LoginType"].ToString().ToLower() == "guardian" || Session["LoginType"].ToString().ToLower() == "student")
            {
                DrpSessionName.Enabled = false;
            }
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@Queryfor", "S"));
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"]));
            DataSet ds = new DataSet();
            ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("Get_GenralInfo", param);

            DrpSessionName.DataSource = ds.Tables[1];
            DrpSessionName.DataTextField = "SessionName";
            DrpSessionName.DataValueField = "SessionName";
            DrpSessionName.DataBind();

            DrpSessionName.SelectedValue = Session["SessionName"].ToString();
            //current session

            Head1.DataBind();
            ADDTopMenuItem();
            if (Session["Logintype"].ToString() == "Staff")
            {
                //SubjectTeacherLink();
                //ClassTeacherLink();
                account.Visible = false;
                help1.Visible = false;
                help2.Visible = false;
                help3.Visible = false;
                help4.Visible = false;
                help5.Visible = false;
                help6.Visible = false;
                help7.Visible = false;
            }
            sql = "select BrandName from BrandTab";
            lblBranding.Text = oo.ReturnTag(sql, "BrandName");
            sql = "select CollegeShortNa,Website,CologeLogoPath from CollegeMaster where BranchCode=" + Session["BranchCode"] + "";
            string kk = "select top(1) ShortCode from BranchTab where BranchId = " + Session["BranchCode"] + "";
            lblCollegeShortName.Text = oo.ReturnTag(kk, "ShortCode");
            lblUsername.Text = Session["DisplayName"].ToString();
            //lblSessionName.Text = Session["SessionName"].ToString();
            imgUser.ImageUrl = Session["ImageUrl"].ToString();
            schoollogolink.HRef = "http://"+oo.ReturnTag(sql, "Website");
            schoollogolink.Target = "_blank";
            if (oo.ReturnTag(sql, "CologeLogoPath") != "")
            {
                Image1.ImageUrl = oo.ReturnTag(sql, "CologeLogoPath") + "?print=" + DateTime.Now;
            }
            else
            {
                Image1.ImageUrl = "~/uploads/CollegeLogo/DefaultCollegeLogo.png";
            }
        }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Response.Cache.SetNoStore();
    }

    private void ADDTopMenuItem()
    {
        string sql = "";
        string isfor = "", value = "", table = "", logintypeid = "0";
        if (Session["Logintype"].ToString().Trim() == "Admin")
        {
            isfor = "Admin";
            value = "1";
            table = "menu_permission";
            logintypeid = "2";
        }
        else if (Session["Logintype"].ToString().Trim() == "SuperAdmin")
        {
            isfor = "SuperAdmin";
            value = "1";
            table = "super_admin_menupermission";
        }
        else if (Session["Logintype"].ToString().Trim() == "Staff")
        {
            isfor = "Staff";
            value = "1";
            table = "menu_permission";
            logintypeid = "3";

        }
        sql = "SELECT MenuID,text,ParentClassName FROM Menueam Inner join " + table + " mp on mp.Menu_Id=Menueam.MenuID";
        sql = sql + " where " + isfor + "=" + value + " and Login_Id='" + Session["LoginId"].ToString() + "' and Permission_Value='Yes' and isnull(mp.BranchCode, '')=case when isnull(mp.BranchCode, '')='' then '' else " + Session["BranchCode"] + " end and ParentId is NUll  and isnull(Permission,0)=1";
        if (logintypeid != "0")
        {
            sql = sql + " and LoginTypeId='" + logintypeid + "'";
        }
        sql = sql + " and Menueam.Status=1 order by Menueam.MenuOrder asc";

        Repeater1.DataSource = oo.GridFill(sql);
        Repeater1.DataBind();
        if (Repeater1.Items.Count > 0)
        {
            for (int i = 0; i < Repeater1.Items.Count; i++)
            {
                Repeater rp2 = (Repeater)Repeater1.Items[i].FindControl("Repeater2");
                Label lblId = (Label)Repeater1.Items[i].FindControl("Label2");
                AddfirstChildMenu(rp2, lblId.Text.Trim(), table, isfor, value);
            }

        }
    }

    private void AddfirstChildMenu(Repeater rp, string parentid, string table, string isfor, string value)
    {
        if (parentid == "7")
        {
            string ss = "";
        }
        sql = "SELECT  MenuID,text,('" + isfor + "'+'/') as root,(Case When ISNULL(Url,'')='' Then '' Else Url End ) as URL,ParentClassName,ChildClassName FROM Menueam ";
        sql = sql + " Inner join " + table + " mp on mp.Menu_Id=Menueam.MenuID";
        sql = sql + " where " + isfor + "=" + value + " and ParentId is not NUll and Login_Id='" + Session["LoginId"].ToString() + "' and Permission_Value='Yes' and ParentID='" + parentid + "' and isnull(mp.BranchCode, '')=case when isnull(mp.BranchCode, '')='' then '' else " + Session["BranchCode"] + " end and Menueam.Status=1 order by Menueam.MenuOrder asc";
        rp.DataSource = oo.GridFill(sql);
        rp.DataBind();
        if (rp.Items.Count > 0)
        {
            for (int i = 0; i < rp.Items.Count; i++)
            {
                Repeater rp3 = (Repeater)rp.Items[i].FindControl("Repeater3");
                Label lblId = (Label)rp.Items[i].FindControl("Label2");
                if (parentid=="430")
                {
                    string ss = "";
                }
                AddSecondChildNode(rp3, lblId.Text.Trim(), table, isfor, value);
                if (rp3.Items.Count == 0)
                {
                    HtmlGenericControl sub_child_menu_badge = (HtmlGenericControl)rp.Items[i].FindControl("sub_child_menu_badge");
                    sub_child_menu_badge.Visible = false;
                    HtmlGenericControl sub_child_menu = (HtmlGenericControl)rp.Items[i].FindControl("sub_child_menu");
                    sub_child_menu.Visible = false;
                }
            }

        }

    }

    private void AddSecondChildNode(Repeater rp, string parentid, string table, string isfor, string value)
    {
        sql = "SELECT MenuID,text,('" + isfor + "'+'/') as root,(Case When ISNULL(Url,'')='' Then '' Else Url End ) as URL,ParentClassName,ChildClassName FROM Menueam ";
        sql = sql + " Inner join " + table + " mp on mp.Menu_Id=Menueam.MenuID";
        sql = sql + " where " + isfor + "=" + value + " and ParentId is not NUll and Login_Id='" + Session["LoginId"].ToString() + "' and Permission_Value='Yes' and ParentID='" + parentid + "' and mp.BranchCode=" + Session["BranchCode"] + " and Menueam.Status=1 order by Menueam.MenuOrder asc";
        rp.DataSource = oo.GridFill(sql);
        rp.DataBind();
    }

    protected void SubjectTeacherLink()
    {
        List<SqlParameter> param = new List<SqlParameter>();
        param.Add(new SqlParameter("@Ecode", Session["LoginName"].ToString()));
        param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));

        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("GetSubjectteacherGroupid_Proc", param);
        if (ds != null)
        {
            dt = ds.Tables[0];
        }
        //Delete Examination inner Parent Node
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string value = dt.Rows[i][0].ToString();
                switch (value)
                {
                    case "G1":
                        {
                            RemoveChieldItems("PG");
                        }
                        break;

                    case "G2":
                        {
                            RemoveChieldItems("NUR to PREP");
                        }
                        break;
                    case "G3":
                        {
                            RemoveChieldItems("I to V");
                        }
                        break;
                    case "G4":
                        {
                            RemoveChieldItems("VI to VIII");
                        }
                        break;
                    case "G5":
                        {
                            RemoveChieldItems("IX to X");
                        }
                        break;
                    case "G6":
                        {
                            RemoveChieldItems("XI");
                        }
                        break;
                    case "G7":
                        {
                            RemoveChieldItems("XII");
                        }
                        break;


                }
            }
        }
    }

    protected void RemoveChieldItems(string str)
    {
        for (int i = 0; i < Repeater1.Items.Count; i++)
        {
            Repeater Repeater2 = (Repeater)Repeater1.Items[i].FindControl("Repeater2");
            for (int j = 0; j < Repeater2.Items.Count; j++)
            {
                Label lblitem = (Label)Repeater2.Items[j].FindControl("Label1");
                if (lblitem.Text.ToUpper() == "Subject wise Cumulative".ToUpper() || lblitem.Text.ToUpper() == "Marks Entry".ToUpper())
                {
                    Repeater Repeater3 = (Repeater)Repeater2.Items[j].FindControl("Repeater3");
                    for (int k = 0; k < Repeater3.Items.Count; k++)
                    {
                        Label lblitem1 = (Label)Repeater3.Items[k].FindControl("Label1");
                        HtmlControl R3li = (HtmlControl)Repeater3.Items[k].FindControl("R3li");
                        if (lblitem1.Text == str)
                        {
                            if (R3li.Visible)
                            {
                                R3li.Visible = false;
                            }
                            break;
                        }
                    }
                }
            }
        }
    }

    protected void RemoveChieldItems1(string str)
    {
        for (int i = 0; i < Repeater1.Items.Count; i++)
        {
            Repeater Repeater2 = (Repeater)Repeater1.Items[i].FindControl("Repeater2");
            for (int j = 0; j < Repeater2.Items.Count; j++)
            {
                Label lblitem = (Label)Repeater2.Items[j].FindControl("Label1");
                if (lblitem.Text != "Subject wise Cumulative" && lblitem.Text != "Marks Entry")
                {
                    Repeater Repeater3 = (Repeater)Repeater2.Items[j].FindControl("Repeater3");
                    for (int k = 0; k < Repeater3.Items.Count; k++)
                    {
                        Label lblitem1 = (Label)Repeater3.Items[k].FindControl("Label1");
                        HtmlControl R3li = (HtmlControl)Repeater3.Items[k].FindControl("R3li");
                        if (lblitem1.Text == str)
                        {
                            R3li.Visible = false;
                            break;
                        }
                    }
                }
            }
        }
    }

    protected void ClassTeacherLink()
    {
        List<SqlParameter> param = new List<SqlParameter>();
        param.Add(new SqlParameter("@Ecode", Session["LoginName"].ToString()));
        param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));

        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("GetClassteacherGroupid_Proc", param);
        if (ds != null)
        {
            dt = ds.Tables[0];
        }

        //Delete Examination inner Parent Node where teacher is not a Classteacher

        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string value = dt.Rows[i][0].ToString();
                switch (value)
                {
                    case "G1":
                        {
                            RemoveChieldItems1("PG Activity Entry");
                            RemoveChieldItems1("PG");
                            RemoveChieldItems1("PG Remark Entry");
                        }
                        break;

                    case "G2":
                        {
                            RemoveChieldItems1("NUR to PREP");
                            RemoveChieldItems1("NUR to PREP Remark Entry");
                        }
                        break;
                    case "G3":
                        {
                            RemoveChieldItems1("I to V");
                            RemoveChieldItems1("I to V Remark Entry");
                        }
                        break;
                    case "G4":
                        {
                            RemoveChieldItems1("VI to VIII");
                            RemoveChieldItems1("VI to  VIII Entry");
                        }
                        break;
                    case "G5":
                        {
                            RemoveChieldItems1("IX to X");
                            RemoveChieldItems1("IX to X Entry");
                        }
                        break;
                    case "G6":
                        {
                            RemoveChieldItems1("XI");
                            RemoveChieldItems1("XI Grade Entry");
                            RemoveChieldItems1("XI Remark Entry");
                        }
                        break;
                    case "G7":
                        {
                            RemoveChieldItems1("XII");
                            RemoveChieldItems1("XII Grade Entry");
                            RemoveChieldItems1("XII Remark Entry");
                        }
                        break;

                }
            }
        }
    }

    protected void lnkLogout_Click(object sender, EventArgs e)
    {
        Session["logout"] = "You have been logged out successfully.<br/> Thank you for using eAM.";
        Response.Redirect("default.aspx");
    }

    protected void lnkLogout1_Click(object sender, EventArgs e)
    {
        Session["logout"] = "You have been logged out successfully.<br/> Thank you for using eAM.";
        Response.Redirect("default.aspx");
    }
    protected void DrpSessionName_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["SessionName"] = DrpSessionName.SelectedItem.ToString();
        Response.Redirect(HttpContext.Current.Request.Url.ToString(), true);
    }
}

