using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Collections.Generic;

public partial class mainStu : System.Web.UI.MasterPage
{
    Campus oo = new Campus();
    string sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Logintype"] != null && Session["Logintype"].ToString() == "FromAdmission")
        {
            Response.Redirect("~/ap/default.aspx");
        }
        Session["logout"] = null;
        if (!IsPostBack)
        {
            lblUsername.Text = Session["LoginName"].ToString();
            string ssss = "select SessionName from StudentOfficialDetails where SrNo='" + Session["LoginName"] + "' and BranchCode=" + Session["BranchCode"] + " order by id asc";
            var dts = oo.Fetchdata(ssss);
            DrpSessionName.DataSource = dts;
            DrpSessionName.DataTextField = "SessionName";
            DrpSessionName.DataValueField = "SessionName";
            DrpSessionName.DataBind();

            string sqlss = "select SrNo from StudentOfficialDetails where SrNo='" + Session["LoginName"] + "' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "'";
            if (oo.Duplicate(sqlss))
            {
                DrpSessionName.SelectedValue = Session["SessionName"].ToString();
            }
            else
            {
                string ssssp = "select top(1) SessionName from StudentOfficialDetails where SrNo='" + Session["LoginName"] + "' and BranchCode=" + Session["BranchCode"] + " order by id desc";
                Session["SessionName"] = oo.ReturnTag(ssssp, "SessionName");
                DrpSessionName.SelectedValue = oo.ReturnTag(ssssp, "SessionName").ToString();
            }
            //lblSessionName.Text = Session["SessionName"].ToString();
            Head1.DataBind();
            imgUser.ImageUrl = Session["ImageUrl"].ToString();
            sql = "select CollegeShortNa,Website,CologeLogoPath from CollegeMaster where BranchCode=" + Session["BranchCode"] + "";
            if (oo.ReturnTag(sql, "CologeLogoPath") != "")
            {
                Image1.ImageUrl = oo.ReturnTag(sql, "CologeLogoPath") + "?print=" + DateTime.Now;
            }
            else
            {
                Image1.ImageUrl = "~/uploads/CollegeLogo/DefaultCollegeLogo.png";
            }
            string kk = "select top(1) ShortCode from BranchTab where BranchId = " + Session["BranchCode"] + "";
            lblCollegeShortName.Text = oo.ReturnTag(kk, "ShortCode");
            schoollogolink.HRef = "http://" + BAL.objBal.ReturnTag(sql, "Website");
            schoollogolink.Target = "_blank";
            sql = "select BrandName from BrandTab";
            lblBranding.Text = BAL.objBal.ReturnTag(sql, "BrandName");

            
        }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Response.Cache.SetNoStore();
    }
    protected void lnkLogout_Click(object sender, EventArgs e)
    {
        Session["logout"] = "You have been logged out successfully.<br/> Thank you for using eAM.";
        Response.Redirect(ResolveClientUrl("~/default.aspx"));
    }
    protected void lnkLogout1_Click(object sender, EventArgs e)
    {
        Session["logout"] = "You have been logged out successfully.<br/> Thank you for using eAM.";
        Response.Redirect(ResolveClientUrl("~/default.aspx"));
    }
    protected void lnkExamCumlative_Click(object sender, EventArgs e)
    {
        sql = "Select ClassId,SectionName,ClassName,BranchId,SectionId from AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "','"+Session["BranchCode"].ToString()+"')";
        sql = sql + " where SrNo='" + Session["LoginName"].ToString() + "'";

        Session["ClassId"] = BAL.objBal.ReturnTag(sql, "ClassId");
        Session["SectionName"] = BAL.objBal.ReturnTag(sql, "SectionName");
        Session["ClassName"] = BAL.objBal.ReturnTag(sql, "ClassName");
        Session["BranchId"] = BAL.objBal.ReturnTag(sql, "BranchId");
        Session["SectionId"] = BAL.objBal.ReturnTag(sql, "SectionId");

    }
    protected void lnkExamReportCard_Click(object sender, EventArgs e)
    {
        sql = "Select ClassId,SectionName,ClassName,BranchId,SectionId from AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "'," + Session["BranchCode"].ToString() + ")";
        sql = sql + " where SrNo='" + Session["LoginName"].ToString() + "'";

        Session["ClassId"] = BAL.objBal.ReturnTag(sql, "ClassId");
        Session["SectionName"] = BAL.objBal.ReturnTag(sql, "SectionName");
        Session["ClassName"] = BAL.objBal.ReturnTag(sql, "ClassName");
        Session["BranchId"] = BAL.objBal.ReturnTag(sql, "BranchId");
        Session["SectionId"] = BAL.objBal.ReturnTag(sql, "SectionId");
    }

    protected void DrpSessionName_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["SessionName"] = DrpSessionName.SelectedItem.ToString();
        Response.Redirect(HttpContext.Current.Request.Url.ToString(), true);
    }
}
