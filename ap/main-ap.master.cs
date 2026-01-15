using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

public partial class ap_main_ap : System.Web.UI.MasterPage
{
    private readonly Campus _oo=new Campus();
    private string _sql = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserMobile"]==null)
        {
            Response.Redirect("~/ap/default.aspx", false);
        }
        if (Session["pass"]==null)
        {
            Response.Redirect("~/ap/default.aspx", false);
        }
        if (!IsPostBack)
        {
            _sql = "select BrandName from BrandTab";
            lblBranding.Text = BAL.objBal.ReturnTag(_sql, "BrandName");
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@Queryfor", "S"));
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"]));
            DataSet ds = new DataSet();
            ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("Get_GenralInfo", param);

            lblUsername.Text = "";
            Head1.DataBind();
            _sql = "select CollegeShortNa,Website,CologeLogoPath from CollegeMaster where BranchCode=" + Session["BranchCode"] + "";
            string kk = "select top(1) ShortCode from BranchTab where BranchId = " + Session["BranchCode"] + "";
            lblCollegeShortName.Text = _oo.ReturnTag(kk, "ShortCode");
            if (_oo.ReturnTag(_sql, "CologeLogoPath") != "")
            {
                Image1.ImageUrl = _oo.ReturnTag(_sql, "CologeLogoPath") + "?print=" + DateTime.Now;
            }
            else
            {
                Image1.ImageUrl = "~/uploads/CollegeLogo/DefaultCollegeLogo.png";
            }
            Head1.DataBind();
            
        }
    }

    protected void lnkAddNewForm_OnClick(object sender, EventArgs e)
    {
        Session["mobilenochk"] = Request.QueryString["txtno"];
        Response.Redirect("../2/Gpaf.aspx?check=admintion&id=" + Session["UserMobile"].ToString() + "",
            false);
    }

    protected void lnkLogout1_OnClick(object sender, EventArgs e)
    {
        Session.Abandon();
        Session.Clear();
        Response.Redirect("~/ap/default.aspx");
    }

    protected void lnkRegistration_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("~/ap/Admission_Details.aspx?txtno="+ Session["UserMobile"].ToString() + "", false);
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Session.Clear();
        Response.Redirect("~/ap/default.aspx");
    }

    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ap/Admission_Details.aspx?txtno=" + Session["UserMobile"].ToString() + "", false);
    }
}
