using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;

public partial class Student_main_students : System.Web.UI.MasterPage
{
    Campus oo = new Campus();
    public SqlConnection con;
    public SqlCommand cmd = new SqlCommand();
    string sql = "";
    public void MakeConnection()
    {
        con = new SqlConnection();
        try
        {
            cmd = new SqlCommand();
            con = oo.dbGet_connection();
            con.Open();
        }
        catch { }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Logintype"] != null && Session["Logintype"].ToString() == "FromAdmission")
        {
            Response.Redirect("~/ap/default.aspx");
        }
        Session["logout"] = null;
        if (!IsPostBack)
        {
            if (Session["LoginType"].ToString().ToLower() == "staff" || Session["LoginType"].ToString().ToLower() == "guardian" || Session["LoginType"].ToString().ToLower() == "student")
            {
                DrpSessionName.Enabled = false;
            }
            sql = "select top(1) CurrencySymbols from setting";
            if (oo.ReturnTag(sql, "CurrencySymbols") == "8377")
            {
                feedeposit.InnerHtml = "₹";
            }
            if (oo.ReturnTag(sql, "CurrencySymbols") == "36")
            {
                feedeposit.InnerHtml = "$";
            }
            if (oo.ReturnTag(sql, "CurrencySymbols") == "8358")
            {
                feedeposit.InnerHtml = "₦";
            }
            if (oo.ReturnTag(sql, "CurrencySymbols") == "65020")
            {
                feedeposit.InnerHtml = "﷼";
            }
            string ssss = "select SessionName from StudentOfficialDetails where SrNo='" + Session["Srno"] + "' and BranchCode=" + Session["BranchCode"] + " order by id asc";
            var dts = oo.Fetchdata(ssss);
            DrpSessionName.DataSource = dts;
            DrpSessionName.DataTextField = "SessionName";
            DrpSessionName.DataValueField = "SessionName";
            DrpSessionName.DataBind();

            string sqlss = "select SrNo from StudentOfficialDetails where SrNo='" + Session["Srno"] + "' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "'";
            if (oo.Duplicate(sqlss))
            {
                DrpSessionName.SelectedValue = Session["SessionName"].ToString();
            }
            else
            {
                string ssssp = "select top(1) SessionName from StudentOfficialDetails where SrNo='" + Session["Srno"] + "' and BranchCode=" + Session["BranchCode"] + " order by id desc";
                Session["SessionName"] = oo.ReturnTag(ssssp, "SessionName");
                DrpSessionName.SelectedValue = oo.ReturnTag(ssssp, "SessionName").ToString();
            }
            //current session
            lblUsername.Text = Session["LoginName"].ToString();
            //lblSessionName.Text = Session["SessionName"].ToString();
            Head1.DataBind();
            imgUser.ImageUrl = Session["ImageUrl"].ToString().Replace("..", "").Replace("~", "").Replace("//", "/");
            sql = "select CollegeShortNa,Website,CologeLogoPath from CollegeMaster where BranchCode=" + Session["BranchCode"] + "";
            string kk = "select top(1) ShortCode from BranchTab where BranchId = " + Session["BranchCode"] + "";
            lblCollegeShortName.Text = oo.ReturnTag(kk, "ShortCode");
            if (oo.ReturnTag(sql, "CologeLogoPath") != "")
            {
                Image1.ImageUrl = oo.ReturnTag(sql, "CologeLogoPath") + "?print=" + DateTime.Now;
            }
            else
            {
                Image1.ImageUrl = "~/uploads/CollegeLogo/DefaultCollegeLogo.png";
            }
            schoollogolink.HRef = "http://" + BAL.objBal.ReturnTag(sql, "Website");
            schoollogolink.Target = "_blank";
            sql = "select BrandName from BrandTab";
            lblBranding.Text = BAL.objBal.ReturnTag(sql, "BrandName");
            string sqlss1 = "select CurrencySymbols from setting";
            if (oo.ReturnTag(sqlss1, "CurrencySymbols") == "8358" && oo.ReturnTag(sqlss1, "CurrencySymbols") != "")
            {
                liReportcard.Visible = true;
                liUniformFee.Visible = true;
                DataSet ds2 = new DataSet();
                sql = "select top(1) (hcm.CategoryName+' Building '+hlm.BuildingLocation+' room no '+rm.RoomNo+'('+hrm.RoomType+') and bed no '+hbm.BedNo) allotedRoom, ra.FrequencyofPayment, ra.LivingStatus, ra.RoomAllotmentId, hbm.BedCharge, hbm.Status, hbm.BookedStatus, hbm.id as bedid, rm.Remark, ra.DateFrom, ra.DateTo, ra.TotalMonths, ra.TotalAmount from RoomAllotment ra inner join HostelCategoryMaster hcm on hcm.Id = ra.HostelCategoryId inner join HostelLocationMaster hlm on hlm.Id = ra.BuildingLocationId";
                sql = sql + " inner join RoomMaster rm on rm.Id = ra.RoomId inner join HostelRoomTypeMaster hrm on hrm.Id = ra.RoomTypeId inner join HostelBedMaster hbm on hbm.Id = ra.BedId where ra.SrNoOrEmpId = '" + Session["Srno"].ToString().Trim() + "' and hcm.BranchCode=" + Session["BranchCode"] + " and hlm.BranchCode=" + Session["BranchCode"] + " and hbm.BranchCode=" + Session["BranchCode"] + " and rm.BranchCode=" + Session["BranchCode"] + " and ra.BranchCode=" + Session["BranchCode"] + " and LivingStatus=1 order by ra.RoomAllotmentId desc";
                ds2 = oo.GridFill(sql);
                if (ds2 != null && ds2.Tables[0].Rows.Count > 0)
                {
                    liHostalfee.Visible = true;
                }
                else
                {
                    liHostalfee.Visible = false;
                }
                liLnk.Visible = false;
            }
            if (oo.ReturnTag(sqlss1, "CurrencySymbols") != "8358" && oo.ReturnTag(sqlss1, "CurrencySymbols") != "")
            {
                sql = "select * from tbl_ShowReportCardToGarduin where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and SrNo='" + Session["Srno"].ToString() + "' and status='1'";
                string sql1 = "select classid from AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "'," + Session["BranchCode"].ToString() + ") where SrNo='" + Session["Srno"].ToString() + "'";
                string sqls2 = "select GroupName from ICSEClassGroupMaster where ClassId=" + oo.ReturnTag(sql1, "classid") + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
                if (oo.Duplicate(sqls2) && oo.Duplicate(sql))
                {
                    liLnk.Visible = true;

                    if (oo.ReturnTag(sqls2, "GroupName").ToString().Trim().ToUpper() == "G1")
                    {
                        lnk.NavigateUrl = "~/14/ReportCard_NurtoPrep.aspx";
                    }
                    if (oo.ReturnTag(sqls2, "GroupName").ToString().Trim().ToUpper() == "G2")
                    {
                        lnk.NavigateUrl = "~/14/ReportCard_ItoVIII.aspx";
                    }
                    if (oo.ReturnTag(sqls2, "GroupName").ToString().Trim().ToUpper() == "G3")
                    {
                        lnk.NavigateUrl = "~/14/ReportCard_IXtoXII.aspx";
                    }
                }
                if (oo.Duplicate(sql) && !oo.Duplicate(sqls2))
                {
                    liLnk.Visible = true;
                    sql = "select ClassName from AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "'," + Session["BranchCode"].ToString() + ") where SrNo='" + Session["Srno"].ToString() + "'";
                    if (oo.ReturnTag(sql, "ClassName").ToString().Trim().ToUpper() == "PRE NURSERY" || oo.ReturnTag(sql, "ClassName").ToString().Trim().ToUpper() == "PRE-NURSERY")
                    {
                        lnk.NavigateUrl = "~/11/G2/ReportCard_NurtoPrep_1718.aspx?check=ReportCardofNURSERYtoPrep";
                    }
                    if (oo.ReturnTag(sql, "ClassName").ToString().Trim().ToUpper() == "NURSERY")
                    {
                        lnk.NavigateUrl = "~/11/G2/ReportCard_NurtoPrep_1718.aspx?check=ReportCardofNURSERYtoPrep";
                    }
                    if (oo.ReturnTag(sql, "ClassName").ToString().Trim().ToUpper() == "PREP")
                    {
                        lnk.NavigateUrl = "~/11/G2/ReportCard_NurtoPrep_1718.aspx?check=ReportCardofNURSERYtoPrep";
                    }
                    if (oo.ReturnTag(sql, "ClassName").ToString().Trim().ToUpper() == "I")
                    {
                        lnk.NavigateUrl = "~/11/G3/ReportCard_ItoV_1718.aspx?check=ReportCardofItoV";
                    }
                    if (oo.ReturnTag(sql, "ClassName").ToString().Trim().ToUpper() == "II")
                    {
                        lnk.NavigateUrl = "~/11/G3/ReportCard_ItoV_1718.aspx?check=ReportCardofItoV";
                    }
                    if (oo.ReturnTag(sql, "ClassName").ToString().Trim().ToUpper() == "III")
                    {
                        lnk.NavigateUrl = "~/11/G3/ReportCard_ItoV_1718.aspx?check=ReportCardofItoV";
                    }
                    if (oo.ReturnTag(sql, "ClassName").ToString().Trim().ToUpper() == "IV")
                    {
                        lnk.NavigateUrl = "~/11/G3/ReportCard_ItoV_1718.aspx?check=ReportCardofItoV";
                    }
                    if (oo.ReturnTag(sql, "ClassName").ToString().Trim().ToUpper() == "V")
                    {
                        lnk.NavigateUrl = "~/11/G3/ReportCard_ItoV_1718.aspx?check=ReportCardofItoV";
                    }
                    if (oo.ReturnTag(sql, "ClassName").ToString().Trim().ToUpper() == "VI")
                    {
                        lnk.NavigateUrl = "~/11/G4/ReportCard_VItoVIII_1718.aspx?check=ReportCardofVItoVIII";
                    }
                    if (oo.ReturnTag(sql, "ClassName").ToString().Trim().ToUpper() == "VII")
                    {
                        lnk.NavigateUrl = "~/11/G4/ReportCard_VItoVIII_1718.aspx?check=ReportCardofVItoVIII";
                    }
                    if (oo.ReturnTag(sql, "ClassName").ToString().Trim().ToUpper() == "VIII")
                    {
                        lnk.NavigateUrl = "~/11/G4/ReportCard_VItoVIII_1718.aspx?check=ReportCardofVItoVIII";
                    }
                    if (oo.ReturnTag(sql, "ClassName").ToString().Trim().ToUpper() == "IX")
                    {
                        lnk.NavigateUrl = "~/11/G5/ReportCard_IXtoX_1718.aspx?check=ReportCardofXItoX";
                    }
                    if (oo.ReturnTag(sql, "ClassName").ToString().Trim().ToUpper() == "X")
                    {
                        lnk.NavigateUrl = "~/11/G5/ReportCard_IXtoX_1718.aspx?check=ReportCardofXItoX";
                    }
                    if (oo.ReturnTag(sql, "ClassName").ToString().Trim().ToUpper() == "XI")
                    {
                        lnk.NavigateUrl = "~/11/G6/PrintReportCardXI_1718_New.aspx?check=ReportCardofXI";
                    }
                    if (oo.ReturnTag(sql, "ClassName").ToString().Trim().ToUpper() == "XII")
                    {
                        lnk.NavigateUrl = "~/11/G7/PrintReportCardXII_1718_New.aspx?check=ReportCardofXII";
                    }
                }
            }
        }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (Session["Logintype"].ToString() != "Guardian")
        {
            Response.Redirect("~/403.aspx");
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
        sql = "Select ClassId,SectionName,ClassName,BranchId,SectionId from AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "','" + Session["BranchCode"].ToString() + "')";
        sql = sql + " where SrNo='" + Session["Srno"].ToString() + "'";

        Session["ClassId"] = BAL.objBal.ReturnTag(sql, "ClassId");
        Session["SectionName"] = BAL.objBal.ReturnTag(sql, "SectionName");
        Session["ClassName"] = BAL.objBal.ReturnTag(sql, "ClassName");
        Session["BranchId"] = BAL.objBal.ReturnTag(sql, "BranchId");
        Session["SectionId"] = BAL.objBal.ReturnTag(sql, "SectionId");

        sql = "Select GroupId from dt_ClassGroupMaster where ClassId=(Select AdmissionForClassId from StudentOfficialDetails where SrNo='" + Session["Srno"].ToString() + "' and BranchCode=" + Session["BranchCode"] + ") ";
        sql = sql + " and IsActive=1 and SessionName='" + Session["SessionName"].ToString() + "'";

        string Groupid = BAL.objBal.ReturnTag(sql, "GroupId");

        if (Groupid == "G1")
        {
            Response.Redirect("~/11/G1/ParticularStudentMarksPG.aspx");
        }
        else if (Groupid == "G2")
        {
            Response.Redirect("~/11/G2/ParticularStudentMarksNUR1toPrep.aspx");
        }
        else if (Groupid == "G3")
        {
            Response.Redirect("~/11/G3/ParticularStudentMarksItoV_1617.aspx");
        }
        else if (Groupid == "G4")
        {
            Response.Redirect("~/11/G4/ParticularStudentMarksVItoVIII_1617.aspx");
        }
        else if (Groupid == "G5")
        {
            Response.Redirect("~/11/G5/ParticularStudentMarksIXtoX_1617.aspx");
        }
        else if (Groupid == "G6")
        {
            Response.Redirect("~/11/G6/ParticularStudentMarksXI_1617.aspx");
        }
        else
        {
            if (Groupid == "G7")
            {
                Response.Redirect("~/11/G7/ParticularStudentMarksXII_1617.aspx");
            }
        }

    }
    protected void lnkExamReportCard_Click(object sender, EventArgs e)
    {
        sql = "Select ClassId,SectionName,ClassName,BranchId,SectionId from AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "'," + Session["BranchCode"].ToString() + ")";
        sql = sql + " where SrNo='" + Session["Srno"].ToString() + "'";

        Session["ClassId"] = BAL.objBal.ReturnTag(sql, "ClassId");
        Session["SectionName"] = BAL.objBal.ReturnTag(sql, "SectionName");
        Session["ClassName"] = BAL.objBal.ReturnTag(sql, "ClassName");
        Session["BranchId"] = BAL.objBal.ReturnTag(sql, "BranchId");
        Session["SectionId"] = BAL.objBal.ReturnTag(sql, "SectionId");

        sql = "Select GroupId from dt_ClassGroupMaster where ClassId=(Select AdmissionForClassId from StudentOfficialDetails where SrNo='" + Session["Srno"].ToString() + "') ";
        sql = sql + " and IsActive=1 and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"] + "";

        string Groupid = BAL.objBal.ReturnTag(sql, "GroupId");

        if (Groupid == "G1")
        {
            Response.Redirect("~/11/G1/ReportCardofPG.aspx");
        }
        else if (Groupid == "G2")
        {
            Response.Redirect("~/11/G2/ReportCardofNURtoPREP.aspx");
        }
        else if (Groupid == "G3")
        {
            Response.Redirect("~/11/G3/ReportCardofItoV.aspx");
        }
        else if (Groupid == "G4")
        {
            Response.Redirect("~/11/G4/ReportCardofVItoVIII_1617.aspx");
        }
        else if (Groupid == "G5")
        {
            Response.Redirect("~/11/G5/ReportCardofIXtoX_1617.aspx");
        }
        else if (Groupid == "G6")
        {
            Response.Redirect("~/11/G6/PrintReportCardXI.aspx");
        }
        else if (Groupid == "G7")
        {
            Response.Redirect("~/11/G7/PrintReportCardXII.aspx");
        }
        else
        {
            //Response.Redirect("#");
        }
    }

    protected void DrpSessionName_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["SessionName"] = DrpSessionName.SelectedItem.ToString();
        Response.Redirect(HttpContext.Current.Request.Url.ToString(), true);
    }
}
