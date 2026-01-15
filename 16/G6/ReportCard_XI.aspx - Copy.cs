using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ReportCard_XI : Page
{
    SqlConnection con = new SqlConnection();
    SqlCommand cmd = new SqlCommand();
    Campus _oo = new Campus();
    string sql = ""; string _sql = "";
    protected void Page_PreInIt(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (Session["Logintype"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (Session["Logintype"].ToString() == "Admin")
        {
            this.MasterPageFile = "~/Master/admin_root-manager.master";
        }
        else if (Session["Logintype"].ToString() == "Staff")
        {
            this.MasterPageFile = "~/Staff/staff_root-manager.master";
        }
        else if (Session["Logintype"].ToString() == "Guardian")
        {
            this.MasterPageFile = "~/Sp/sp_root-manager.master";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Campus camp = new Campus(); camp.LoadLoader(loader);
        con = _oo.dbGet_connection();
        if (!IsPostBack)
        {
            if (Session["Logintype"].ToString() == "Admin" || Session["Logintype"].ToString() == "Guardian")
            {
                _sql = "Select Distinct ClassName,cm.Id as Id,CIDOrder from ClassMaster cm ";
                _sql +=  " inner join dt_ClassGroupMaster t1 on t1.ClassId=cm.Id and t1.SessionName=cm.SessionName and t1.BranchCode=cm.BranchCode";
                _sql +=  " where cm.SessionName='" + Session["SessionName"] + "' and t1.SessionName='" + Session["SessionName"] + "'  and t1.BranchCode=" + Session["BranchCode"] + " and GroupId='G6' Order by CIDOrder";
                _oo.FillDropDown_withValue(_sql, drpclass, "ClassName", "id");
            }
            else
            {
                _sql = "Select Distinct ClassName,cm.Id,CIDOrder from ClassTeacherMaster ctm";
                _sql +=  " inner join ClassMaster cm on cm.Id=ctm.ClassId and cm.SessionName=ctm.SessionName and cm.BranchCode=ctm.BranchCode";
                _sql +=  " where EmpCode='" + Session["LoginName"].ToString() + "' ";
                _sql +=  " and ctm.SessionName='" + Session["SessionName"].ToString() + "' and ctm.BranchCode = " + Session["BranchCode"] + " ";
                _sql +=  " and cm.id in(select ClassId from dt_ClassGroupMaster where GroupId='G6' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + ")";
                _sql +=  " order by CIDOrder asc ";

                _oo.FillDropDown_withValue(_sql, drpclass, "ClassName", "id");
            }
            drpclass.Items.Insert(0, new ListItem("<--Select-->", ""));
            drpsection.Items.Insert(0, new ListItem("<--Select-->", ""));
            drpBranch.Items.Insert(0, new ListItem("<--Select-->", ""));
            drpStream.Items.Insert(0, new ListItem("<--Select-->", ""));
            drpsrno.Items.Insert(0, new ListItem("<--Select-->", ""));
            ddlMedium.Items.Insert(0, new ListItem("<--Select-->", ""));

            string ss = "select CityName from CityMaster where id=(select CityId from CollegeMaster where BranchCode=" + Session["BranchCode"] + ")";
            txtPlace.Text = _oo.ReturnTag(ss, "CityName");
            if (Session["Logintype"].ToString() == "Guardian")
            {
                if (!IsPostBack)
                {
                    string sqls = "select Attendance from tbl_ShowReportCardToGarduin where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and SrNo='" + Session["Srno"].ToString() + "' and status='1'";
                    if (!_oo.Duplicate(sqls))
                    {
                        Response.Redirect("~/sp/sp-dashboard.aspx");
                    }
                    else
                    {
                        drpAttendance.SelectedValue = (_oo.ReturnTag(sqls, "Attendance").ToString() == "" ? "0" : _oo.ReturnTag(sqls, "Attendance").ToString());
                    }
                    string sql1s = "select ClassId, SrNo, SectionId, blocked from AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "'," + Session["BranchCode"].ToString() + ") where SrNo='" + Session["Srno"].ToString() + "'";
                    drpclass.SelectedValue = _oo.ReturnTag(sql1s, "ClassId").ToString();
                    _sql = "Select SectionName,Id from SectionMaster where ClassNameId='" + drpclass.SelectedValue + "' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
                    _oo.FillDropDown_withValue(_sql, drpsection, "SectionName", "id");
                    drpsection.SelectedValue = _oo.ReturnTag(sql1s, "SectionId").ToString();
                    LoadSrNo();
                    drpsrno.SelectedValue = _oo.ReturnTag(sql1s, "SrNo").ToString();
                    if (_oo.ReturnTag(sql1s, "blocked").ToString() == "")
                    {
                        divHideForGardian1.Style.Add("display", "none !important");
                        divHideForGardian1.Style.Add("visibility", "hidden;");
                        divHideForGardian2.Style.Add("display", "none !important");
                        divHideForGardian2.Style.Add("visibility", "hidden;");
                        divHideForGardian3.Style.Add("display", "none !important");
                        divHideForGardian3.Style.Add("visibility", "hidden;");
                        divHideForGardian4.Style.Add("display", "none !important");
                        divHideForGardian4.Style.Add("visibility", "hidden;");
                        divHideForGardian5.Style.Add("display", "none !important");
                        divHideForGardian5.Style.Add("visibility", "hidden;");
                        divHideForGardian7.Style.Add("display", "none !important");
                        divHideForGardian7.Style.Add("visibility", "hidden;");
                        divHideForGardian8.Style.Add("display", "none !important");
                        divHideForGardian8.Style.Add("visibility", "hidden;");
                        divHideForGardian9.Style.Add("display", "none !important");
                        divHideForGardian9.Style.Add("visibility", "hidden;");
                        divHideForGardian10.Style.Add("display", "none !important");
                        divHideForGardian10.Style.Add("visibility", "hidden;");
                        icons.Style.Add("display", "none !important");
                        icons.Style.Add("visibility", "hidden;");
                        LoadReportCard();
                    }
                    else
                    {
                        lblError.Visible = true;
                        lblError.Text = "Student account is not active";
                    }
                }
            }

        }

    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected string CalculateSum(object highestValue, object secondHighestValue)
    {
        // Check if highestValue and secondHighestValue are not DBNull and not null
        if (highestValue != DBNull.Value && secondHighestValue != DBNull.Value
            && highestValue != null && secondHighestValue != null
            && !string.IsNullOrWhiteSpace(highestValue.ToString())
            && !string.IsNullOrWhiteSpace(secondHighestValue.ToString()))
        { 

            // Check if both values are "AB"
            if (highestValue.ToString().Trim().Equals("AB", StringComparison.OrdinalIgnoreCase)
                && secondHighestValue.ToString().Trim().Equals("AB", StringComparison.OrdinalIgnoreCase))
            {
                // If both are "AB", return "AB"
                return "AB";
            }
            else
            {
                // Try parsing the values to integers
                int highest;
                int secondHighest;
                bool isValidHighest = int.TryParse(highestValue.ToString(), out highest);
                bool isValidSecondHighest = int.TryParse(secondHighestValue.ToString(), out secondHighest);
                // If only one of the values is "AB", treat it as 0
                if (!isValidHighest && isValidSecondHighest)
                {
                    highest = 0;
                }
                else if (isValidHighest && !isValidSecondHighest)
                {
                    secondHighest = 0;
                }
                else if (!isValidHighest && !isValidSecondHighest)
                {
                    // If both are "AB", return "AB"
                    return "AB";
                }
                // Calculate the sum
                int sum = highest + secondHighest;
                return sum.ToString();
            }
        }
        else
        {
            if (highestValue != DBNull.Value && secondHighestValue == "")
            {
                return highestValue.ToString();
            }
            if (secondHighestValue != DBNull.Value && highestValue == "")
            {
                return secondHighestValue.ToString();
            }
        }

        // If any value is DBNull or null, return empty string
        return string.Empty;
    }


    public void LoadReportCard()
    {

        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = BAL.objBal.dbGet_connection().ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = "ReportCard_XI_UP";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
                if (Session["Logintype"].ToString() == "Guardian" || Session["Logintype"].ToString() == "Student")
                {
                    cmd.Parameters.AddWithValue("@SrNo", Session["Srno"].ToString());
                }
                else if (drpsrno.SelectedValue.Trim() != "")
                {
                    if (Session["Logintype"].ToString() != "Guardian" && Session["Logintype"].ToString() != "Student")
                    {
                        cmd.Parameters.AddWithValue("@SrNo", drpsrno.SelectedValue.Trim());
                    }
                }
                cmd.Parameters.AddWithValue("@SectionId", drpsection.SelectedValue);
                if (drpStream.SelectedIndex != 0)
                {
                    cmd.Parameters.AddWithValue("@StreamId", drpStream.SelectedValue);
                }
                cmd.Parameters.AddWithValue("@ClassId", drpclass.SelectedValue);
                cmd.Parameters.AddWithValue("@BranchId", drpBranch.SelectedValue);
                cmd.Parameters.AddWithValue("@Medium", ddlMedium.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"]);
                cmd.Parameters.AddWithValue("@status", drpStatus.SelectedValue);
                cmd.Parameters.AddWithValue("@action", "student");
                SqlDataAdapter das = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                das.Fill(dt);
                cmd.Parameters.Clear();
                if (dt.Rows.Count > 0)
                {
                    cmd.CommandText = "UPCalculateRank_XI";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ClassId", drpclass.SelectedValue);
                    cmd.Parameters.AddWithValue("@SectionId", drpsection.SelectedValue);
                    cmd.Parameters.AddWithValue("@BranchId", drpBranch.SelectedValue);
                    if (drpStream.SelectedIndex != 0)
                    {
                        cmd.Parameters.AddWithValue("@StreamId", drpStream.SelectedValue);
                    }
                    cmd.Parameters.AddWithValue("@Medium", ddlMedium.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
                    cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"]);
                    SqlDataAdapter daRank = new SqlDataAdapter(cmd);
                    DataTable dtRank = new DataTable();
                    daRank.Fill(dtRank);
                    cmd.Parameters.Clear();

                    cmd.CommandText = "UPDevision_XI";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ClassId", drpclass.SelectedValue);
                    cmd.Parameters.AddWithValue("@SectionId", drpsection.SelectedValue);
                    cmd.Parameters.AddWithValue("@BranchId", drpBranch.SelectedValue);
                    if (drpStream.SelectedIndex != 0)
                    {
                        cmd.Parameters.AddWithValue("@StreamId", drpStream.SelectedValue);
                    }
                    cmd.Parameters.AddWithValue("@Medium", ddlMedium.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
                    cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"]);
                    SqlDataAdapter daDevesion = new SqlDataAdapter(cmd);
                    DataTable dtDiv = new DataTable();
                    daDevesion.Fill(dtDiv);
                    cmd.Parameters.Clear();

                    rptStudent.DataSource = dt;
                    rptStudent.DataBind();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        cmd.CommandText = "ReportCard_XI_UP";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@sessionName", Session["SessionName"]);
                        cmd.Parameters.AddWithValue("@SrNo", dt.Rows[i]["admissionNo"].ToString().Trim());
                        cmd.Parameters.AddWithValue("@SectionId", drpsection.SelectedValue);
                        cmd.Parameters.AddWithValue("@TermName", drpEval.SelectedValue);
                        cmd.Parameters.AddWithValue("@ClassId", drpclass.SelectedValue);
                        if (drpStream.SelectedIndex != 0)
                        {
                            cmd.Parameters.AddWithValue("@StreamId", drpStream.SelectedValue);
                        }
                        cmd.Parameters.AddWithValue("@BranchId", drpBranch.SelectedValue);
                        cmd.Parameters.AddWithValue("@ClassName", drpclass.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@Medium", ddlMedium.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"]);
                        cmd.Parameters.AddWithValue("@status", drpStatus.SelectedValue);
                        cmd.Parameters.AddWithValue("@isManual", drpAttendance.SelectedValue);
                        cmd.Parameters.AddWithValue("@action", "details");

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        cmd.Parameters.Clear();

                        try
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                icons.Visible = true;
                                HtmlControl header = (HtmlControl)rptStudent.Items[i].FindControl("header");
                                BLL.BLLInstance.LoadReportCardHeader("Result", header);
                                string srno = dt.Rows[i]["admissionNo"].ToString().Trim();
                                if (drpEval.SelectedValue.ToUpper() == "TERM1")
                                {
                                    string Term1Dev = (from t in dtDiv.AsEnumerable() where t.Field<string>("SrNo") == srno select t.Field<string>("HY")).FirstOrDefault().ToString();
                                    string Term1Rank = (from t in dtRank.AsEnumerable() where t.Field<string>("SrNo") == srno select t.Field<int>("Term1Rank")).FirstOrDefault().ToString();
                                    double totalBestUT1 = 0; double totalBestHYTH = 0; double totalBestHYPrac = 0;
                                    double totalUT1 = 0; double totalUT2 = 0; double totalHY = 0; double totalGHY = 0;
                                    Repeater rptMarkTerm1 = (Repeater)rptStudent.Items[i].FindControl("rptMarkTerm1");
                                    rptMarkTerm1.DataSource = ds.Tables[0];
                                    rptMarkTerm1.DataBind();
                                    Repeater rptMarkTerm1Aditional = (Repeater)rptStudent.Items[i].FindControl("rptMarkTerm1Aditional");
                                    rptMarkTerm1Aditional.DataSource = ds.Tables[1];
                                    rptMarkTerm1Aditional.DataBind();
                                    for (int m = 0; m < rptMarkTerm1.Items.Count; m++)
                                    {
                                        double UT1 = 0;
                                        
                                        //double.TryParse(ds.Tables[0].Rows[m]["HighestValue"].ToString(), out UT1);
                                        if (ds.Tables[0].Rows[m]["HighestValue"].ToString() != "AB" && ds.Tables[0].Rows[m]["HighestValue"].ToString() != "ML" && ds.Tables[0].Rows[m]["HighestValue"].ToString() != "NAD")
                                        {
                                            double.TryParse(ds.Tables[0].Rows[m]["HighestValue"].ToString(), out UT1);
                                        }
                                        totalUT1 = totalUT1 + UT1;
                                        double UT2 = 0;
                                        //double.TryParse(ds.Tables[0].Rows[m]["SecondHighestValue"].ToString(), out UT2);
                                        if (ds.Tables[0].Rows[m]["SecondHighestValue"].ToString() != "AB" && ds.Tables[0].Rows[m]["SecondHighestValue"].ToString() != "ML" && ds.Tables[0].Rows[m]["SecondHighestValue"].ToString() != "NAD")
                                        {
                                            double.TryParse(ds.Tables[0].Rows[m]["SecondHighestValue"].ToString(), out UT2);
                                        }
                                        totalUT2 = totalUT2 + UT2;
                                        totalBestUT1 = totalBestUT1 + UT1 + UT2;
                                        double HYTH = 0;
                                        if (ds.Tables[0].Rows[m]["HYTH"].ToString() != "AB" && ds.Tables[0].Rows[m]["HYTH"].ToString() != "ML" && ds.Tables[0].Rows[m]["HYTH"].ToString() != "NAD")
                                        {
                                            double.TryParse(ds.Tables[0].Rows[m]["HYTH"].ToString(), out HYTH);
                                        }
                                        totalBestHYTH = totalBestHYTH + HYTH;
                                        double HYPrac = 0;
                                        if (ds.Tables[0].Rows[m]["HYPrac"].ToString() != "AB" && ds.Tables[0].Rows[m]["HYPrac"].ToString() != "ML" && ds.Tables[0].Rows[m]["HYPrac"].ToString() != "NAD")
                                        {
                                            double.TryParse(ds.Tables[0].Rows[m]["HYPrac"].ToString(), out HYPrac);
                                        }
                                        totalBestHYPrac = totalBestHYPrac + HYPrac;

                                        double HY = 0;
                                        //double.TryParse(ds.Tables[0].Rows[m]["HYTotal"].ToString(), out HY);
                                        if (ds.Tables[0].Rows[m]["HYTotal"].ToString() != "AB" && ds.Tables[0].Rows[m]["HYTotal"].ToString() != "ML" && ds.Tables[0].Rows[m]["HYTotal"].ToString() != "NAD")
                                        {
                                            double.TryParse(ds.Tables[0].Rows[m]["HYTotal"].ToString(), out HY);
                                        }
                                        totalHY = totalHY + HY;
                                        double total = 0;
                                     //   double.TryParse(ds.Tables[0].Rows[m]["HYGTotal"].ToString(), out total);
                                        if (ds.Tables[0].Rows[m]["HYGTotal"].ToString() != "AB" && ds.Tables[0].Rows[m]["HYGTotal"].ToString() != "ML" && ds.Tables[0].Rows[m]["HYGTotal"].ToString() != "NAD")
                                        {
                                            double.TryParse(ds.Tables[0].Rows[m]["HYGTotal"].ToString(), out total);
                                        }
                                        totalGHY = totalGHY + total;
                                    }
                                    Label lblUT1Total_T1 = (Label)rptStudent.Items[i].FindControl("lblUT1Total_T1");
                                    lblUT1Total_T1.Text = totalUT1.ToString("0");
                                    Label lblUT2Total_T1 = (Label)rptStudent.Items[i].FindControl("lblUT2Total_T1");
                                    lblUT2Total_T1.Text = totalUT2.ToString("0");
                                    Label lblHYTotal_T1 = (Label)rptStudent.Items[i].FindControl("lblHYTotal_T1");
                                    lblHYTotal_T1.Text = totalHY.ToString("0");
                                    Label lblGTotalHY_T1 = (Label)rptStudent.Items[i].FindControl("lblGTotalHY_T1");
                                    lblGTotalHY_T1.Text = totalGHY.ToString("0");

                                    Label Label4 = (Label)rptStudent.Items[i].FindControl("Label4");
                                    Label4.Text = totalBestUT1.ToString("0");

                                    Label Label6 = (Label)rptStudent.Items[i].FindControl("Label6");
                                    Label6.Text = totalBestHYTH.ToString("0");
                                    Label Label7 = (Label)rptStudent.Items[i].FindControl("Label7");
                                    Label7.Text = totalBestHYPrac.ToString("0");

                                    Label Label5 = (Label)rptStudent.Items[i].FindControl("Label5");
                                    Label5.Text = ((totalBestUT1 * 100) / (50 * rptMarkTerm1.Items.Count)).ToString("0.00");

                                    Label lblUT1Per_T1 = (Label)rptStudent.Items[i].FindControl("lblUT1Per_T1");
                                    lblUT1Per_T1.Text = ((totalUT1 * 100) / (25 * rptMarkTerm1.Items.Count)).ToString("0.00");
                                    Label lblUT2Per_T1 = (Label)rptStudent.Items[i].FindControl("lblUT2Per_T1");
                                    lblUT2Per_T1.Text = ((totalUT2 * 100) / (25 * rptMarkTerm1.Items.Count)).ToString("0.00");
                                    Label lblHYPer_T1 = (Label)rptStudent.Items[i].FindControl("lblHYPer_T1");
                                    lblHYPer_T1.Text = ((totalHY * 100) / (100 * rptMarkTerm1.Items.Count)).ToString("0.00");
                                    Label lblGTotalPerHY_T1 = (Label)rptStudent.Items[i].FindControl("lblGTotalPerHY_T1");
                                    lblGTotalPerHY_T1.Text = ((totalGHY * 100) / (150 * rptMarkTerm1.Items.Count)).ToString("0.00");

                                    Label lblGTotalDevHY_T1 = (Label)rptStudent.Items[i].FindControl("lblGTotalDevHY_T1");
                                    lblGTotalDevHY_T1.Text = Term1Dev;

                                    Label lblTrm1Rank_T1 = (Label)rptStudent.Items[i].FindControl("lblTrm1Rank_T1");
                                    lblTrm1Rank_T1.Text = Term1Rank;
                                }
                                if (drpEval.SelectedValue.ToUpper() == "TERM2")
                                {
                                    string Term1Dev = (from t in dtDiv.AsEnumerable() where t.Field<string>("SrNo") == srno select t.Field<string>("HY")).FirstOrDefault().ToString();
                                    string Term2Dev = (from t in dtDiv.AsEnumerable() where t.Field<string>("SrNo") == srno select t.Field<string>("AE")).FirstOrDefault().ToString();
                                    string AllDev = (from t in dtDiv.AsEnumerable() where t.Field<string>("SrNo") == srno select t.Field<string>("Total")).FirstOrDefault().ToString();

                                    string Term1Rank = (from t in dtRank.AsEnumerable() where t.Field<string>("SrNo") == srno select t.Field<int>("Term1Rank")).FirstOrDefault().ToString();
                                    string Term2Rank = (from t in dtRank.AsEnumerable() where t.Field<string>("SrNo") == srno select t.Field<int>("Term2Rank")).FirstOrDefault().ToString();
                                    string AllRank = (from t in dtRank.AsEnumerable() where t.Field<string>("SrNo") == srno select t.Field<int>("AllRank")).FirstOrDefault().ToString();

                                    double totalbestUT1 = 0; double totalbestUT2 = 0; double totalbestHYTH = 0;
                                    double totalbestHYPRAC = 0; double totalbestAETH = 0; double totalbestAEPRAC = 0;
                                    double totalUT1 = 0; double totalUT2 = 0; double totalHY = 0; double totalGHY = 0;
                                    double totalUT3 = 0; double totalUT4 = 0; double totalAE = 0; double totalGAE = 0; double totalGGAE = 0;
                                    Repeater rptMarkTerm2 = (Repeater)rptStudent.Items[i].FindControl("rptMarkTerm2");
                                    rptMarkTerm2.DataSource = ds.Tables[0];
                                    rptMarkTerm2.DataBind();
                                    Repeater rptMarkTerm1Aditional = (Repeater)rptStudent.Items[i].FindControl("rptMarkTerm2Aditional");
                                    rptMarkTerm1Aditional.DataSource = ds.Tables[1];
                                    rptMarkTerm1Aditional.DataBind();
                                    for (int m = 0; m < rptMarkTerm2.Items.Count; m++)
                                    {
                                        double UT1 = 0;
                                       // double.TryParse(ds.Tables[0].Rows[m]["HighestValue"].ToString(), out UT1);
                                        if (ds.Tables[0].Rows[m]["HighestValue"].ToString() != "AB" && ds.Tables[0].Rows[m]["HighestValue"].ToString() != "ML" && ds.Tables[0].Rows[m]["HighestValue"].ToString() != "NAD")
                                        {
                                            double.TryParse(ds.Tables[0].Rows[m]["HighestValue"].ToString(), out UT1);
                                        }
                                        totalUT1 = totalUT1 + UT1;
                                        double UT2 = 0;
                                       // double.TryParse(ds.Tables[0].Rows[m]["SecondHighestValue"].ToString(), out UT2);
                                        if (ds.Tables[0].Rows[m]["SecondHighestValue"].ToString() != "AB" && ds.Tables[0].Rows[m]["SecondHighestValue"].ToString() != "ML" && ds.Tables[0].Rows[m]["SecondHighestValue"].ToString() != "NAD")
                                        {
                                            double.TryParse(ds.Tables[0].Rows[m]["SecondHighestValue"].ToString(), out UT2);
                                        }
                                        totalUT2 = totalUT2 + UT2;
                                        totalbestUT1 = totalbestUT1 + UT1 + UT2;
                                        double HYTH = 0;
                                        if (ds.Tables[0].Rows[m]["HYTH"].ToString() != "AB" && ds.Tables[0].Rows[m]["HYTH"].ToString() != "ML" && ds.Tables[0].Rows[m]["HYTH"].ToString() != "NAD")
                                        {
                                            double.TryParse(ds.Tables[0].Rows[m]["HYTH"].ToString(), out HYTH);
                                        }
                                        totalbestHYTH = totalbestHYTH + HYTH;
                                        double HYPrac = 0;
                                        if (ds.Tables[0].Rows[m]["HYPrac"].ToString() != "AB" && ds.Tables[0].Rows[m]["HYPrac"].ToString() != "ML" && ds.Tables[0].Rows[m]["HYPrac"].ToString() != "NAD")
                                        {
                                            double.TryParse(ds.Tables[0].Rows[m]["HYPrac"].ToString(), out HYPrac);
                                        }
                                        totalbestHYPRAC = totalbestHYPRAC + HYPrac;
                                        double AETH = 0;
                                        if (ds.Tables[0].Rows[m]["AETH"].ToString() != "AB" && ds.Tables[0].Rows[m]["AETH"].ToString() != "ML" && ds.Tables[0].Rows[m]["AETH"].ToString() != "NAD")
                                        {
                                            double.TryParse(ds.Tables[0].Rows[m]["AETH"].ToString(), out AETH);
                                        }
                                        totalbestAETH = totalbestAETH + AETH;
                                        double AEPrac = 0;
                                        if (ds.Tables[0].Rows[m]["AEPrac"].ToString() != "AB" && ds.Tables[0].Rows[m]["AEPrac"].ToString() != "ML" && ds.Tables[0].Rows[m]["AEPrac"].ToString() != "NAD")
                                        {
                                            double.TryParse(ds.Tables[0].Rows[m]["AEPrac"].ToString(), out AEPrac);
                                        }
                                        totalbestAEPRAC = totalbestAEPRAC + AEPrac;



                                        double HY = 0;
                                       // double.TryParse(ds.Tables[0].Rows[m]["HYTotal"].ToString(), out HY);
                                        if (ds.Tables[0].Rows[m]["HYTotal"].ToString() != "AB" && ds.Tables[0].Rows[m]["HYTotal"].ToString() != "ML" && ds.Tables[0].Rows[m]["HYTotal"].ToString() != "NAD")
                                        {
                                            double.TryParse(ds.Tables[0].Rows[m]["HYTotal"].ToString(), out HY);
                                        }
                                        totalHY = totalHY + HY;
                                        double HYGTotal = 0;
                                       // double.TryParse(ds.Tables[0].Rows[m]["HYGTotal"].ToString(), out HYGTotal);
                                        if (ds.Tables[0].Rows[m]["HYGTotal"].ToString() != "AB" && ds.Tables[0].Rows[m]["HYGTotal"].ToString() != "ML" && ds.Tables[0].Rows[m]["HYGTotal"].ToString() != "NAD")
                                        {
                                            double.TryParse(ds.Tables[0].Rows[m]["HYGTotal"].ToString(), out HYGTotal);
                                        }
                                        totalGHY = totalGHY + HYGTotal;

                                        double UT3 = 0;
                                      //  double.TryParse(ds.Tables[0].Rows[m]["ThirdHighestValue"].ToString(), out UT3);
                                        if (ds.Tables[0].Rows[m]["ThirdHighestValue"].ToString() != "AB" && ds.Tables[0].Rows[m]["ThirdHighestValue"].ToString() != "ML" && ds.Tables[0].Rows[m]["ThirdHighestValue"].ToString() != "NAD")
                                        {
                                            double.TryParse(ds.Tables[0].Rows[m]["ThirdHighestValue"].ToString(), out UT3);
                                        }
                                        totalUT3 = totalUT3 + UT3;
                                        double UT4 = 0;
                                       // double.TryParse(ds.Tables[0].Rows[m]["ThirdSecondHighestValue"].ToString(), out UT4);
                                        if (ds.Tables[0].Rows[m]["ThirdSecondHighestValue"].ToString() != "AB" && ds.Tables[0].Rows[m]["ThirdSecondHighestValue"].ToString() != "ML" && ds.Tables[0].Rows[m]["ThirdSecondHighestValue"].ToString() != "NAD")
                                        {
                                            double.TryParse(ds.Tables[0].Rows[m]["ThirdSecondHighestValue"].ToString(), out UT4);
                                        }
                                        totalUT4 = totalUT4 + UT4;
                                        totalbestUT2 = totalbestUT2 + UT3 + UT4;
                                        double AE = 0;
                                       // double.TryParse(ds.Tables[0].Rows[m]["AETotal"].ToString(), out AE);
                                        if (ds.Tables[0].Rows[m]["AETotal"].ToString() != "AB" && ds.Tables[0].Rows[m]["AETotal"].ToString() != "ML" && ds.Tables[0].Rows[m]["AETotal"].ToString() != "NAD")
                                        {
                                            double.TryParse(ds.Tables[0].Rows[m]["AETotal"].ToString(), out AE);
                                        }
                                        totalAE = totalAE + AE;
                                        double AEGTotal = 0;
                                       // double.TryParse(ds.Tables[0].Rows[m]["AEGTotal"].ToString(), out AEGTotal);
                                        if (ds.Tables[0].Rows[m]["AEGTotal"].ToString() != "AB" && ds.Tables[0].Rows[m]["AEGTotal"].ToString() != "ML" && ds.Tables[0].Rows[m]["AEGTotal"].ToString() != "NAD")
                                        {
                                            double.TryParse(ds.Tables[0].Rows[m]["AEGTotal"].ToString(), out AEGTotal);
                                        }
                                        totalGAE = totalGAE + AEGTotal;
                                        double GGTotal = 0;
                                      //  double.TryParse(ds.Tables[0].Rows[m]["GGTotal"].ToString(), out GGTotal);
                                        if (ds.Tables[0].Rows[m]["GGTotal"].ToString() != "AB" && ds.Tables[0].Rows[m]["GGTotal"].ToString() != "ML" && ds.Tables[0].Rows[m]["GGTotal"].ToString() != "NAD")
                                        {
                                            double.TryParse(ds.Tables[0].Rows[m]["GGTotal"].ToString(), out GGTotal);
                                        }
                                        totalGGAE = totalGGAE + GGTotal;
                                        string grade1 = grade(totalGGAE/2);
                                        Label lblGrade = (Label)rptMarkTerm2.Items[m].FindControl("lblGGrade");
                                        lblGrade.Text = grade1;


                                    }
                                    Label Label10 = (Label)rptStudent.Items[i].FindControl("Label10");
                                    Label10.Text = totalbestUT1.ToString("0");

                                    Label Label13 = (Label)rptStudent.Items[i].FindControl("Label13");
                                    Label13.Text = totalbestUT2.ToString("0");

                                    Label Label11 = (Label)rptStudent.Items[i].FindControl("Label11");
                                    Label11.Text = totalbestHYTH.ToString("0");

                                    Label Label12 = (Label)rptStudent.Items[i].FindControl("Label12");
                                    Label12.Text = totalbestHYPRAC.ToString("0");

                                    Label Label14 = (Label)rptStudent.Items[i].FindControl("Label14");
                                    Label14.Text = totalbestAETH.ToString("0");
                                    Label Label15 = (Label)rptStudent.Items[i].FindControl("Label15");
                                    Label15.Text = totalbestAEPRAC.ToString("0");

                                    

                                    Label lblUT1Total_T2 = (Label)rptStudent.Items[i].FindControl("lblUT1Total_T2");
                                    lblUT1Total_T2.Text = totalUT1.ToString("0");
                                    Label lblUT2Total_T2 = (Label)rptStudent.Items[i].FindControl("lblUT2Total_T2");
                                    lblUT2Total_T2.Text = totalUT2.ToString("0");
                                    Label lblHYTotal_T2 = (Label)rptStudent.Items[i].FindControl("lblHYTotal_T2");
                                    lblHYTotal_T2.Text = totalHY.ToString("0");
                                    Label lblGTotalHY_T2 = (Label)rptStudent.Items[i].FindControl("lblGTotalHY_T2");
                                    lblGTotalHY_T2.Text = totalGHY.ToString("0");

                                    Label lblUT3Total_T2 = (Label)rptStudent.Items[i].FindControl("lblUT3Total_T2");
                                    lblUT3Total_T2.Text = totalUT3.ToString("0");
                                    Label lblUT4Total_T2 = (Label)rptStudent.Items[i].FindControl("lblUT4Total_T2");
                                    lblUT4Total_T2.Text = totalUT4.ToString("0");
                                    Label lblAETotal_T2 = (Label)rptStudent.Items[i].FindControl("lblAETotal_T2");
                                    lblAETotal_T2.Text = totalAE.ToString("0");
                                    Label lblGTotalAE_T2 = (Label)rptStudent.Items[i].FindControl("lblGTotalAE_T2");
                                    lblGTotalAE_T2.Text = totalGAE.ToString("0");
                                    Label lblGGTotal_T2 = (Label)rptStudent.Items[i].FindControl("lblGGTotal_T2");
                                    lblGGTotal_T2.Text = totalGGAE.ToString("0");

                                    Label Label16 = (Label)rptStudent.Items[i].FindControl("Label16");
                                    Label16.Text = ((totalbestUT1 * 100) / (50 * rptMarkTerm2.Items.Count)).ToString("0.00");

                                    Label Label21 = (Label)rptStudent.Items[i].FindControl("Label21");
                                    Label21.Text = ((totalbestUT2 * 100) / (50 * rptMarkTerm2.Items.Count)).ToString("0.00");
                                    
                                    Label lblUT1Per_T2 = (Label)rptStudent.Items[i].FindControl("lblUT1Per_T2");
                                    lblUT1Per_T2.Text = ((totalUT1 * 100) / (25 * rptMarkTerm2.Items.Count)).ToString("0.00");
                                    Label lblUT2Per_T2 = (Label)rptStudent.Items[i].FindControl("lblUT2Per_T2");
                                    lblUT2Per_T2.Text = ((totalUT2 * 100) / (25 * rptMarkTerm2.Items.Count)).ToString("0.00");
                                    Label lblHYPer_T2 = (Label)rptStudent.Items[i].FindControl("lblHYPer_T2");
                                    lblHYPer_T2.Text = ((totalHY * 100) / (100 * rptMarkTerm2.Items.Count)).ToString("0.00");
                                    Label lblGTotalPerHY_T2 = (Label)rptStudent.Items[i].FindControl("lblGTotalPerHY_T2");
                                    lblGTotalPerHY_T2.Text = ((totalGHY * 100) / (150 * rptMarkTerm2.Items.Count)).ToString("0.00");

                                    Label lblUT3Per_T2 = (Label)rptStudent.Items[i].FindControl("lblUT3Per_T2");
                                    lblUT3Per_T2.Text = ((totalUT3 * 100) / (25 * rptMarkTerm2.Items.Count)).ToString("0.00");
                                    Label lblUT4Per_T2 = (Label)rptStudent.Items[i].FindControl("lblUT4Per_T2");
                                    lblUT4Per_T2.Text = ((totalUT4 * 100) / (25 * rptMarkTerm2.Items.Count)).ToString("0.00");
                                    Label lblAEPer_T2 = (Label)rptStudent.Items[i].FindControl("lblAEPer_T2");
                                    lblAEPer_T2.Text = ((totalAE * 100) / (100 * rptMarkTerm2.Items.Count)).ToString("0.00");
                                    Label lblGTotalPerAE_T2 = (Label)rptStudent.Items[i].FindControl("lblGTotalPerAE_T2");
                                    lblGTotalPerAE_T2.Text = ((totalGAE * 100) / (150 * rptMarkTerm2.Items.Count)).ToString("0.00");
                                    Label lblGGTotalPer_T2 = (Label)rptStudent.Items[i].FindControl("lblGGTotalPer_T2");
                                    lblGGTotalPer_T2.Text = ((totalGGAE * 100) / (300 * rptMarkTerm2.Items.Count)).ToString("0.00");


                                   
                                    Label lblGTotalDevHY_T2 = (Label)rptStudent.Items[i].FindControl("lblGTotalDevHY_T2");
                                    lblGTotalDevHY_T2.Text = Term1Dev;
                                    Label lblGTotalDevAE_T2 = (Label)rptStudent.Items[i].FindControl("lblGTotalDevAE_T2");
                                    lblGTotalDevAE_T2.Text = Term2Dev;
                                    Label lblGGTotalDev_T2 = (Label)rptStudent.Items[i].FindControl("lblGGTotalDev_T2");
                                    lblGGTotalDev_T2.Text = AllDev;

                                    Label lblTrm1Rank_T2 = (Label)rptStudent.Items[i].FindControl("lblTrm1Rank_T2");
                                    lblTrm1Rank_T2.Text = Term1Rank;
                                    Label lblTrm2Rank_T2 = (Label)rptStudent.Items[i].FindControl("lblTrm2Rank_T2");
                                    lblTrm2Rank_T2.Text = Term2Rank;
                                    Label lblAllRank_T2 = (Label)rptStudent.Items[i].FindControl("lblAllRank_T2");
                                    lblAllRank_T2.Text = AllRank;
                                }


                                DataTable dtAtt = new DataTable();
                                dtAtt.Columns.Add("t1Att", typeof(string));
                                dtAtt.Columns.Add("t2Att", typeof(string));
                                if (drpEval.SelectedValue.ToUpper() == "TERM1")
                                {
                                    DataRow dr = dtAtt.NewRow();

                                    double totaldays = 0, attendence = 0, percent = 0; string t1Att = "";
                                    totaldays = double.Parse(ds.Tables[2].Rows[0]["totaldays"].ToString().Trim() == "" ? "0" : ds.Tables[2].Rows[0]["totaldays"].ToString().Trim());
                                    attendence = double.Parse(ds.Tables[2].Rows[0]["attendence"].ToString().Trim() == "" ? "0" : ds.Tables[2].Rows[0]["attendence"].ToString().Trim());
                                    if (attendence != 0 && totaldays != 0)
                                    {
                                        percent = (attendence * 100) / totaldays;
                                        t1Att = "<span >" + attendence.ToString().Trim() + "</span>/<span >" + totaldays.ToString().Trim() + "</span> (<span >" + percent.ToString("0") + "</span> %)";
                                    }
                                    dr["t1Att"] = t1Att.ToString();
                                    dr["t2Att"] = "";
                                    dtAtt.Rows.Add(dr);
                                    Repeater rptAttendance1 = (Repeater)rptStudent.Items[i].FindControl("rptAttendance1");
                                    rptAttendance1.DataSource = dtAtt;
                                    rptAttendance1.DataBind();
                                }
                                if (drpEval.SelectedValue.ToUpper() == "TERM2")
                                {
                                    DataRow dr = dtAtt.NewRow();
                                    double totaldays1 = 0, attendence1 = 0, percent1 = 0; string t1Att = ""; string t2Att = "";
                                    totaldays1 = double.Parse(ds.Tables[2].Rows[0]["totaldays"].ToString().Trim() == "" ? "0" : ds.Tables[2].Rows[0]["totaldays"].ToString().Trim());
                                    attendence1 = double.Parse(ds.Tables[2].Rows[0]["attendence"].ToString().Trim() == "" ? "0" : ds.Tables[2].Rows[0]["attendence"].ToString().Trim());

                                    if (attendence1 != 0 && totaldays1 != 0)
                                    {
                                        percent1 = (attendence1 * 100) / totaldays1;
                                        t1Att = "<span >" + attendence1.ToString().Trim() + "</span>/<span >" + totaldays1.ToString().Trim() + "</span> (<span >" + percent1.ToString("0") + "</span> %)";
                                    }
                                    double totaldays2 = 0, attendence2 = 0, percent2 = 0;
                                    totaldays2 = double.Parse(ds.Tables[3].Rows[0]["totaldays"].ToString().Trim() == "" ? "0" : ds.Tables[3].Rows[0]["totaldays"].ToString().Trim());
                                    attendence2 = double.Parse(ds.Tables[3].Rows[0]["attendence"].ToString().Trim() == "" ? "0" : ds.Tables[3].Rows[0]["attendence"].ToString().Trim());

                                    if (attendence2 != 0 && totaldays2 != 0)
                                    {
                                        percent2 = (attendence2 * 100) / totaldays2;
                                        t2Att = "<span >" + attendence2.ToString().Trim() + "</span>/<span >" + totaldays2.ToString().Trim() + "</span> (<span >" + percent2.ToString("0") + "</span> %)";
                                    }
                                    dr["t1Att"] = t1Att.ToString();
                                    dr["t2Att"] = t2Att.ToString();
                                    dtAtt.Rows.Add(dr);
                                    Repeater rptAttendance2 = (Repeater)rptStudent.Items[i].FindControl("rptAttendance2");
                                    rptAttendance2.DataSource = dtAtt;
                                    rptAttendance2.DataBind();
                                }
                                if (ds.Tables[4].Rows.Count > 0)
                                {
                                    if (drpEval.SelectedValue.ToUpper() == "TERM1")
                                    {
                                        Repeater rptRemark1 = (Repeater)rptStudent.Items[i].FindControl("rptRemark1");
                                        rptRemark1.DataSource = ds.Tables[4];
                                        rptRemark1.DataBind();
                                    }
                                    if (drpEval.SelectedValue.ToUpper() == "TERM2")
                                    {
                                        Repeater rptRemark2 = (Repeater)rptStudent.Items[i].FindControl("rptRemark2");
                                        rptRemark2.DataSource = ds.Tables[4];
                                        rptRemark2.DataBind();
                                    }
                                }
                                string rm = "select rm.ResultText, srm.PromotedTo, srm.ReopenOn, srm.GeneratedOn from StudentResultMapping srm ";
                                rm += "inner join ResultMaster rm on rm.ID=srm.ResultID and rm.BranchCode=srm.BranchCode ";
                                rm += "where srm.BranchCode=" + Session["BranchCode"] + " and srm.SessionName='" + Session["SessionName"] + "' and SrNo='" + srno + "' ";
                                string ResultText = _oo.ReturnTag(rm, "ResultText");
                                string PromotedTo = _oo.ReturnTag(rm, "PromotedTo");
                                string ReopenOn = _oo.ReturnTag(rm, "ReopenOn");
                                string GeneratedOn = _oo.ReturnTag(rm, "GeneratedOn");


                                HtmlTable tblResult = (HtmlTable)rptStudent.Items[i].FindControl("tblResult");
                                if (drpEval.SelectedValue.ToUpper() == "TERM2" && Session["Logintype"].ToString() != "Guardian")
                                {
                                    tblResult.Visible = true;
                                    Label lblresulttype = (Label)rptStudent.Items[i].FindControl("lblresulttype");
                                    lblresulttype.Text = ResultText;
                                    Label lblpromotedClass = (Label)rptStudent.Items[i].FindControl("lblpromotedClass");
                                    lblpromotedClass.Text = PromotedTo;
                                    Label lblReopenon = (Label)rptStudent.Items[i].FindControl("lblReopenon");
                                    lblReopenon.Text = ReopenOn;
                                }
                                else
                                {
                                    tblResult.Visible = false;
                                }
                                if (Session["Logintype"].ToString() == "Guardian")
                                {
                                    HtmlGenericControl divTagline = (HtmlGenericControl)rptStudent.Items[i].FindControl("divTagline");
                                    divTagline.Visible = true;
                                }

                                Label lblprintdate = (Label)rptStudent.Items[i].FindControl("lblprintdate");
                                lblprintdate.Text = GeneratedOn;
                                Label lblPlace = (Label)rptStudent.Items[i].FindControl("lblPlace");
                                lblPlace.Text = txtPlace.Text;

                                if (ds.Tables[5].Rows.Count > 0)
                                {
                                    if (drpEval.SelectedValue.ToUpper() == "TERM1")
                                    {
                                        Repeater rptSkil1 = (Repeater)rptStudent.Items[i].FindControl("rptSkil1");
                                        rptSkil1.DataSource = ds.Tables[5];
                                        rptSkil1.DataBind();
                                    }
                                    if (drpEval.SelectedValue.ToUpper() == "TERM2")
                                    {
                                        Repeater rptSkil2 = (Repeater)rptStudent.Items[i].FindControl("rptSkil2");
                                        rptSkil2.DataSource = ds.Tables[5];
                                        rptSkil2.DataBind();
                                    }
                                }
                            }

                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "showHide();", true);
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
            }
        }
    }
    protected void drpclass_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Session["Logintype"].ToString() == "Admin" || Session["Logintype"].ToString() == "Guardian")
        {
            _sql = "Select SectionName,Id from SectionMaster where ClassNameId='" + drpclass.SelectedValue + "' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            _oo.FillDropDown_withValue(_sql, drpsection, "SectionName", "id");

        }
        else
        {
            _sql = " Select Distinct sm.SectionName,sm.id from ClassTeacherMaster sctm";
            _sql +=  " inner join SectionMaster sm on sm.ClassNameId=sctm.ClassId and sm.Id=sctm.SectionId and sm.SessionName=sctm.SessionName and sm.BranchCode=sctm.BranchCode ";
            _sql +=  " where sctm.BranchCode=" + Session["BranchCode"].ToString() + " and sctm.BranchCode = " + Session["BranchCode"] + " and ClassId='" + drpclass.SelectedValue + "' ";
            _sql +=  " and EmpCode='" + Session["LoginName"].ToString() + "' ";
            _oo.FillDropDown_withValue(_sql, drpsection, "SectionName", "id");
        }
        drpsection.Items.Insert(0, new ListItem("<--Select-->", ""));

        sql = "Select Medium,Id from MediumMaster where BranchCode=" + Session["BranchCode"].ToString() + " and SessionName='" + Session["SessionName"].ToString() + "'";
        _oo.FillDropDown_withValue(sql, ddlMedium, "Medium", "Id");
        ddlMedium.Items.Insert(0, new ListItem("<--Select-->", ""));
    }
    protected void drpsection_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Session["Logintype"].ToString() == "Admin" || Session["Logintype"].ToString() == "Guardian")
        {
            _sql = "Select BranchName,Id from BranchMaster where ClassID='" + drpclass.SelectedValue + "' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            _oo.FillDropDown_withValue(_sql, drpBranch, "BranchName", "id");
            drpBranch.Items.Insert(0, new ListItem("<--Select-->", ""));

        }
        else
        {
            _sql = " Select Distinct sm.BranchName,sm.id from ClassTeacherMaster sctm";
            _sql +=  " inner join BranchMaster sm on sm.ClassId=sctm.ClassId and sm.Id=sctm.BranchId and sm.SessionName=sctm.SessionName and sm.BranchCode=sctm.BranchCode ";
            _sql +=  " where sctm.BranchCode=" + Session["BranchCode"].ToString() + " and sctm.BranchCode = " + Session["BranchCode"] + " and sctm.ClassId='" + drpclass.SelectedValue + "' and SectionId=" + drpsection.SelectedValue + " ";
            _sql +=  " and EmpCode='" + Session["LoginName"].ToString() + "' ";
            _oo.FillDropDown_withValue(_sql, drpBranch, "BranchName", "id");
            drpBranch.Items.Insert(0, new ListItem("<--Select-->", ""));
        }

    }
    protected void drpBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        _sql = "Select Stream,Id from StreamMaster where ClassId=" + drpclass.SelectedValue + " and BranchId=" + drpBranch.SelectedValue + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        _oo.FillDropDown_withValue(_sql, drpStream, "Stream", "id");
        drpStream.Items.Insert(0, new ListItem("<--Select-->", ""));
    }
    protected void drpStream_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMedium.Items.Clear();
        sql = "Select Medium,Id from MediumMaster where BranchCode=" + Session["BranchCode"].ToString() + " and SessionName='" + Session["SessionName"].ToString() + "'";
        _oo.FillDropDown_withValue(sql, ddlMedium, "Medium", "Id");
        ddlMedium.Items.Insert(0, new ListItem("<--Select-->", ""));
        LoadSrNo();
    }
    
    protected void ddlMedium_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSrNo();
    }

    protected void LoadSrNo()
    {
        if (drpStatus.SelectedValue=="0")
        {
            drpsrno.Items.Clear();
            List<SqlParameter> param = new List<SqlParameter>();
            _sql = "Select Name+' - '+SrNo NAME,SrNo from AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") where ";
            _sql += " Classid = " + drpclass.SelectedValue + " and Sectionid=" + drpsection.SelectedValue + " and Medium='" + ddlMedium.SelectedItem.Text + "'  and Branchid = " + drpBranch.SelectedValue + " ";
            if (drpStream.SelectedIndex != 0)
            {
                _sql += " and StreamId=" + drpStream.SelectedValue + "  ";
            }
           // _sql += " and isnull(Withdrwal,'') = case when '" + drpStatus.SelectedValue + "'='B' or '" + drpStatus.SelectedValue + "'='' then isnull(Withdrwal,'') else case when '" + drpStatus.SelectedValue + "'='A' then '' else 'W' end end ";
           // _sql += " and isnull(blocked,'') = case when '" + drpStatus.SelectedValue + "'='W' or '" + drpStatus.SelectedValue + "'='' then isnull(blocked,'') else case when '" + drpStatus.SelectedValue + "'='A' then '' else 'yes' end end ";
            _sql += " ORDER BY NAME ";
            _oo.FillDropDown_withValue(_sql, drpsrno, "NAME", "SrNo");
            drpsrno.Items.Insert(0, new ListItem("<--Select-->", ""));
        }
        else
        {
            if (drpStatus.SelectedValue == "AB")
            {
                drpsrno.Items.Clear();
                List<SqlParameter> param = new List<SqlParameter>();
                _sql = "Select Name+' - '+SrNo NAME,SrNo from AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") where ";
                _sql += " Classid = " + drpclass.SelectedValue + " and Sectionid=" + drpsection.SelectedValue + " and Medium='" + ddlMedium.SelectedItem.Text + "'  and Branchid = " + drpBranch.SelectedValue + " ";
                if (drpStream.SelectedIndex != 0)
                {
                    _sql += " and StreamId=" + drpStream.SelectedValue + "  ";
                }
                //  _sql += " and isnull(Withdrwal,'') = case when '" + drpStatus.SelectedValue + "'='B' or '" + drpStatus.SelectedValue + "'='' then isnull(Withdrwal,'') else case when '" + drpStatus.SelectedValue + "'='A' then '' else 'W' end end ";
                //  _sql += " and isnull(blocked,'') = case when '" + drpStatus.SelectedValue + "'='W' or '" + drpStatus.SelectedValue + "'='' then isnull(blocked,'') else case when '" + drpStatus.SelectedValue + "'='A' then '' else 'yes' end end ";
                _sql += "and (isnull(Withdrwal,'')='' or isnull(blocked,'')='Yes')";
                _sql += " ORDER BY NAME ";
                _oo.FillDropDown_withValue(_sql, drpsrno, "NAME", "SrNo");
                drpsrno.Items.Insert(0, new ListItem("<--Select-->", ""));
            }
            else
            {
                drpsrno.Items.Clear();
                List<SqlParameter> param = new List<SqlParameter>();
                _sql = "Select Name+' - '+SrNo NAME,SrNo from AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") where ";
                _sql += " Classid = " + drpclass.SelectedValue + " and Sectionid=" + drpsection.SelectedValue + " and Medium='" + ddlMedium.SelectedItem.Text + "'  and Branchid = " + drpBranch.SelectedValue + " ";
                if (drpStream.SelectedIndex != 0)
                {
                    _sql += " and StreamId=" + drpStream.SelectedValue + "  ";
                }
                _sql += " and isnull(Withdrwal,'') = case when '" + drpStatus.SelectedValue + "'='B' or '" + drpStatus.SelectedValue + "'='' then isnull(Withdrwal,'') else case when '" + drpStatus.SelectedValue + "'='A' then '' else 'W' end end ";
                _sql += " and isnull(blocked,'') = case when '" + drpStatus.SelectedValue + "'='W' or '" + drpStatus.SelectedValue + "'='' then isnull(blocked,'') else case when '" + drpStatus.SelectedValue + "'='A' then '' else 'yes' end end ";
                _sql += " ORDER BY NAME ";
                _oo.FillDropDown_withValue(_sql, drpsrno, "NAME", "SrNo");
                drpsrno.Items.Insert(0, new ListItem("<--Select-->", ""));
            }
        }
      
    }
    protected void lnkView_Click(object sender, EventArgs e)
    {
        LoadReportCard();
    }
    public string grade(double total)
    {
        if (total <= 53)
        {
            return "D";
        }
        else if (total > 53 && total <= 76)
        {
            return "C";
        }
        else if (total > 76 && total <= 91)
        {
            return "B";
        }
        else if (total > 91 && total <= 106)
        {
            return "B+";
        }
        else if (total > 106 && total <= 128)
        {
            return "A";
        }
        else if (total > 128 && total <= 150)
        {
            return "A+";
        }
        else
        {
            return "";
        }
    }

    public string Devision(double percentle)
    {
        if (percentle < 33)
        {
            return "F";
        }
        else if (percentle >= 33 && percentle < 50)
        {
            return "3rd";
        }
        else if (percentle >= 50 && percentle < 60)
        {
            return "2nd";
        }
        else if (percentle >= 60 && percentle <= 100)
        {
            return "1st";
        }
        else
        {
            return "";
        }
    }

    protected void drpEval_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Session["Logintype"].ToString() != "Guardian")
        {
        }
        else
        {
            LoadReportCard();
            lnkView.Visible = false;
        }
    }

    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        //PrintHelper_New.ctrl = rptStudent;
        //ClientScript.RegisterStartupScript(GetType(), "onclick", "<script language=javascript>var winpop=window.open('../Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }

    protected void drpStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSrNo();
    }
}