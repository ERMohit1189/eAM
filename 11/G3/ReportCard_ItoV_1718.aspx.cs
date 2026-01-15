using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Linq;
using System.Web.UI.DataVisualization.Charting;

public partial class common_G4_ReportCard_ItoV_1718 : Page
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
            txtDate.Text = Convert.ToDateTime(BAL.objBal.CurrentDate()).ToString("dd-MMM-yyyy");
            if (Session["Logintype"].ToString() == "Admin" || Session["Logintype"].ToString() == "Guardian")
            {
                _sql = "Select Distinct ClassName,cm.Id as Id,CIDOrder from ClassMaster cm ";
                _sql +=  " inner join dt_ClassGroupMaster t1 on t1.ClassId=cm.Id and t1.SessionName=cm.SessionName and t1.BranchCode=cm.BranchCode";
                _sql +=  " where cm.SessionName='" + Session["SessionName"] + "' and t1.SessionName='" + Session["SessionName"] + "'  and t1.BranchCode=" + Session["BranchCode"] + " and GroupId='G3' Order by CIDOrder";
                _oo.FillDropDown_withValue(_sql, drpclass, "ClassName", "id");
            }
            else
            {
                _sql = "Select Distinct ClassName,cm.Id,CIDOrder from ClassTeacherMaster ctm";
                _sql +=  " inner join ClassMaster cm on cm.Id=ctm.ClassId and cm.SessionName=ctm.SessionName and cm.BranchCode=ctm.BranchCode";
                _sql +=  " where EmpCode='" + Session["LoginName"].ToString() + "' ";
                _sql +=  " and ctm.SessionName='" + Session["SessionName"].ToString() + "' and ctm.BranchCode = " + Session["BranchCode"] + " ";
                _sql +=  " and cm.id in(select ClassId from dt_ClassGroupMaster where GroupId='G3' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + ")";
                _sql +=  " order by CIDOrder asc ";

                _oo.FillDropDown_withValue(_sql, drpclass, "ClassName", "id");
            }
            drpclass.Items.Insert(0, new ListItem("<--Select-->", ""));
            drpsection.Items.Insert(0, new ListItem("<--Select-->", ""));
            drpsrno.Items.Insert(0, new ListItem("<--Select-->", ""));

            txtSchoolReopenon.Attributes.Add("readonly", "readonly");
            txtDate.Attributes.Add("readonly", "readonly");
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
                        divHideForGardian6.Style.Add("display", "none !important");
                        divHideForGardian6.Style.Add("visibility", "hidden;");
                        divHideForGardian7.Style.Add("display", "none !important");
                        divHideForGardian7.Style.Add("visibility", "hidden;");
                        divHideForGardian8.Style.Add("display", "none !important");
                        divHideForGardian8.Style.Add("visibility", "hidden;");
                        div2.Style.Add("display", "none !important");
                        div2.Style.Add("visibility", "hidden;");
                        div3.Style.Add("display", "none !important");
                        div3.Style.Add("visibility", "hidden;");
                        div4.Style.Add("display", "none !important");
                        div4.Style.Add("visibility", "hidden;");
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
    private HtmlControl FindHtmlControlByIdInControl(Control control, string id)
    {
        foreach (Control childControl in control.Controls)
        {
            if (childControl.ID != null && childControl.ID.Equals(id, StringComparison.OrdinalIgnoreCase) && childControl is HtmlControl)
            {
                return (HtmlControl)childControl;
            }

            if (childControl.HasControls())
            {
                HtmlControl result = FindHtmlControlByIdInControl(childControl, id);
                if (result != null) return result;
            }
        }

        return null;
    }

    public void LoadReportCard()
    {

        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = BAL.objBal.dbGet_connection().ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = "ReportCard_ItoV_2021";
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
                cmd.Parameters.AddWithValue("@SectionName", drpsection.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@ClassId", drpclass.SelectedValue);
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"]);
                cmd.Parameters.AddWithValue("@status", drpStatus.SelectedValue);
                cmd.Parameters.AddWithValue("@action", "student");
                SqlDataAdapter das = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                das.Fill(dt);
                cmd.Parameters.Clear();
                if (dt.Rows.Count > 0)
                {
                    rptStudent.DataSource = dt;
                    rptStudent.DataBind();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        cmd.CommandText = "ReportCard_ItoV_2021";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@sessionName", Session["SessionName"]);
                        cmd.Parameters.AddWithValue("@SrNo", dt.Rows[i]["admissionNo"].ToString().Trim());
                        cmd.Parameters.AddWithValue("@SectionName", drpsection.SelectedItem.Text.Trim());
                        cmd.Parameters.AddWithValue("@TermName", drpEval.SelectedValue);
                        cmd.Parameters.AddWithValue("@ClassId", drpclass.SelectedValue);
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
                            cmd.CommandText = "calculateRank_ItoV_2021";
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
                            cmd.Parameters.AddWithValue("@SrNo", dt.Rows[i]["admissionNo"].ToString().Trim());
                            cmd.Parameters.AddWithValue("@branchCode", Session["BranchCode"]);
                            cmd.Parameters.AddWithValue("@SectionName", drpsection.SelectedItem.Text);
                            cmd.Parameters.AddWithValue("@ClassId", drpclass.SelectedValue);

                            SqlDataAdapter daRank = new SqlDataAdapter(cmd);
                            DataSet dsRank = new DataSet();
                            daRank.Fill(dsRank);
                            cmd.Parameters.Clear();

                            double totalM1 = 0; double totalMM1 = 0; double totalM2 = 0; double totalMM2 = 0; double totalForPer = 0;

                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                icons.Visible = true;
                                HtmlControl header = (HtmlControl)rptStudent.Items[i].FindControl("header");
                                BLL.BLLInstance.LoadReportCardHeader("Result", header);

                                if (drpEval.SelectedValue.ToUpper() == "TERM1")
                                {
                                    DataTable dts = new DataTable();
                                    dts.Columns.AddRange(new DataColumn[] {
                                    new DataColumn("SubjectName",typeof(string)),
                                    new DataColumn("Highest Marks",typeof(int)),
                                    new DataColumn("Obtained Marks",typeof(int)),
                                    new DataColumn("Avg. Marks", typeof(int))
                                    });
                                    Repeater rptmarksT1 = (Repeater)rptStudent.Items[i].FindControl("rptmarksT1");
                                    rptmarksT1.DataSource = ds.Tables[0];
                                    rptmarksT1.DataBind();
                                    for (int m = 0; m < rptmarksT1.Items.Count; m++)
                                    {
                                        double Test1 = 0; double Test2 = 0; double NB = 0; double SE = 0; double SAT = 0; double Prac = 0;
                                        if (ds.Tables[0].Rows[m]["Test1_1"].ToString().Trim().ToUpper() == "NP" || ds.Tables[0].Rows[m]["Test1_1"].ToString().Trim().ToUpper() == "NAD" || ds.Tables[0].Rows[m]["Test1_1"].ToString().Trim().ToUpper() == "ML" || ds.Tables[0].Rows[m]["Test1_1"].ToString().Trim().ToUpper() == "" || ds.Tables[0].Rows[m]["Test1_1"].ToString().Trim().ToUpper() == "AB" || ds.Tables[0].Rows[m]["Test1_1"].ToString().Trim().ToUpper() == "NA" || ds.Tables[0].Rows[m]["Test1_1"].ToString().Trim().ToUpper() == "M.L") { }
                                        else { Test1 = double.Parse(ds.Tables[0].Rows[m]["Test1_1"].ToString().Trim()); }
                                        if (ds.Tables[0].Rows[m]["Test2_1"].ToString().Trim().ToUpper() == "NP" || ds.Tables[0].Rows[m]["Test2_1"].ToString().Trim().ToUpper() == "NAD" || ds.Tables[0].Rows[m]["Test2_1"].ToString().Trim().ToUpper() == "ML" || ds.Tables[0].Rows[m]["Test2_1"].ToString().Trim().ToUpper() == "" || ds.Tables[0].Rows[m]["Test2_1"].ToString().Trim().ToUpper() == "AB" || ds.Tables[0].Rows[m]["Test2_1"].ToString().Trim().ToUpper() == "NA" || ds.Tables[0].Rows[m]["Test2_1"].ToString().Trim().ToUpper() == "M.L") { }
                                        else { Test2 = double.Parse(ds.Tables[0].Rows[m]["Test2_1"].ToString().Trim()); }
                                        if (ds.Tables[0].Rows[m]["NB_1"].ToString().Trim().ToUpper() == "NP" || ds.Tables[0].Rows[m]["NB_1"].ToString().Trim().ToUpper() == "NAD" || ds.Tables[0].Rows[m]["NB_1"].ToString().Trim().ToUpper() == "ML" || ds.Tables[0].Rows[m]["NB_1"].ToString().Trim().ToUpper() == "" || ds.Tables[0].Rows[m]["NB_1"].ToString().Trim().ToUpper() == "AB" || ds.Tables[0].Rows[m]["NB_1"].ToString().Trim().ToUpper() == "NA" || ds.Tables[0].Rows[m]["NB_1"].ToString().Trim().ToUpper() == "M.L") { }
                                        else { NB = double.Parse(ds.Tables[0].Rows[m]["NB_1"].ToString().Trim()); }
                                        if (ds.Tables[0].Rows[m]["SE_1"].ToString().Trim().ToUpper() == "NP" || ds.Tables[0].Rows[m]["SE_1"].ToString().Trim().ToUpper() == "NAD" || ds.Tables[0].Rows[m]["SE_1"].ToString().Trim().ToUpper() == "ML" || ds.Tables[0].Rows[m]["SE_1"].ToString().Trim().ToUpper() == "" || ds.Tables[0].Rows[m]["SE_1"].ToString().Trim().ToUpper() == "AB" || ds.Tables[0].Rows[m]["SE_1"].ToString().Trim().ToUpper() == "NA" || ds.Tables[0].Rows[m]["SE_1"].ToString().Trim().ToUpper() == "M.L") { }
                                        else { SE = double.Parse(ds.Tables[0].Rows[m]["SE_1"].ToString().Trim()); }
                                        if (ds.Tables[0].Rows[m]["SAT_1"].ToString().Trim().ToUpper() == "NP" || ds.Tables[0].Rows[m]["SAT_1"].ToString().Trim().ToUpper() == "NAD" || ds.Tables[0].Rows[m]["SAT_1"].ToString().Trim().ToUpper() == "ML" || ds.Tables[0].Rows[m]["SAT_1"].ToString().Trim().ToUpper() == "" || ds.Tables[0].Rows[m]["SAT_1"].ToString().Trim().ToUpper() == "AB" || ds.Tables[0].Rows[m]["SAT_1"].ToString().Trim().ToUpper() == "NA" || ds.Tables[0].Rows[m]["SAT_1"].ToString().Trim().ToUpper() == "M.L") { }
                                        else { SAT = double.Parse(ds.Tables[0].Rows[m]["SAT_1"].ToString().Trim()); }
                                        if (ds.Tables[0].Rows[m]["Prac_1"].ToString().Trim().ToUpper() == "NP" || ds.Tables[0].Rows[m]["Prac_1"].ToString().Trim().ToUpper() == "NAD" || ds.Tables[0].Rows[m]["Prac_1"].ToString().Trim().ToUpper() == "ML" || ds.Tables[0].Rows[m]["Prac_1"].ToString().Trim().ToUpper() == "" || ds.Tables[0].Rows[m]["Prac_1"].ToString().Trim().ToUpper() == "AB" || ds.Tables[0].Rows[m]["Prac_1"].ToString().Trim().ToUpper() == "NA" || ds.Tables[0].Rows[m]["Prac_1"].ToString().Trim().ToUpper() == "M.L") { }
                                        else { Prac = double.Parse(ds.Tables[0].Rows[m]["Prac_1"].ToString().Trim()); }

                                        double totalmarks1 = (((Test1 > Test2 ? Test1 : Test2) * 10) / 20) + double.Parse(NB.ToString("0"))  + double.Parse(SE.ToString("0"));
                                        double ObtMarks1 = (double.Parse(totalmarks1.ToString("0")) + double.Parse(SAT.ToString("0")) + double.Parse(Prac.ToString("0")));
                                        totalM1 = totalM1 + ObtMarks1;
                                        totalMM1 = totalMM1 + 100;
                                        string grade1 = grade(ObtMarks1);
                                        Label lblIA = (Label)rptmarksT1.Items[m].FindControl("lblIA");
                                        Label lblTheory = (Label)rptmarksT1.Items[m].FindControl("lblTheory");
                                        Label lblTotal = (Label)rptmarksT1.Items[m].FindControl("lblTotal");
                                        Label lblGrade = (Label)rptmarksT1.Items[m].FindControl("lblGrade");
                                        lblIA.Text = (totalmarks1 == 0 ? "NP" : totalmarks1.ToString("0"));
                                        lblTheory.Text = ((SAT + Prac) == 0 ? "NP" : (double.Parse(SAT.ToString("0")) + double.Parse(Prac.ToString("0"))).ToString("0"));
                                        lblTotal.Text = ObtMarks1.ToString("0");
                                        lblGrade.Text = grade1;
                                        string hi = "select top(1) T1.highestMarks from (select max(((case when isnull(TRY_CONVERT(decimal(10,0),Test1),0)>isnull(TRY_CONVERT(decimal(10,0),Test2),0) then isnull(TRY_CONVERT(decimal(10,0),Test1),0)  ";
                                        hi = hi + " else  isnull(TRY_CONVERT(decimal(10,0),Test2),0) end)*10)/20+isnull(TRY_CONVERT(decimal(10,0),NB),0)+isnull(TRY_CONVERT(decimal(10,0),SE),0) +isnull(TRY_CONVERT(decimal(10,0),SAT),0)+isnull(TRY_CONVERT(decimal(10,0),Prac),0)) highestMarks ";
                                        hi = hi + " from CCEItoV where ClassId=" + drpclass.SelectedValue + " and SectionName='" + drpsection.SelectedItem.Text + "' and Evaluation='TERM1' and SubjectId=" + ds.Tables[0].Rows[m]["SubjectId_1"].ToString() + "  ";
                                        hi = hi + " and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' ";
                                        hi = hi + " and SrNo in (select SrNo from AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") stu ";
                                        hi = hi + " where stu.sessionName='" + Session["SessionName"] + "' and isnull(stu.Promotion, '')<>'Cancelled' ";
                                        hi = hi + " and stu.ClassId=" + drpclass.SelectedValue + " and stu.SectionName='" + drpsection.SelectedItem.Text + "' ";
                                        hi = hi + " and isnull(Withdrwal,'') = '' ";
                                        hi = hi + " and isnull(blocked,'') = '')  group by SrNo ";
                                        hi = hi + "  )T1 order by T1.highestMarks desc ";
                                        double highestMarks = double.Parse((_oo.ReturnTag(hi, "highestMarks").ToString() == "" ? "0" : _oo.ReturnTag(hi, "highestMarks")));
                                        string avg = "select T1.avgMarks from (select sum(((case when isnull(TRY_CONVERT(decimal(10,0),Test1),0)>isnull(TRY_CONVERT(decimal(10,0),Test2),0) ";
                                        avg = avg + " then isnull(TRY_CONVERT(decimal(10,0),Test1),0) else  isnull(TRY_CONVERT(decimal(10,0),Test2),0) end)*10)/20+isnull(TRY_CONVERT(decimal(10,0),NB),0)+isnull(TRY_CONVERT(decimal(10,0),SE),0)+isnull(TRY_CONVERT(decimal(10,0),SAT),0)+isnull(TRY_CONVERT(decimal(10,0),Prac),0)) avgMarks";
                                        avg = avg + " from CCEItoV where ClassId=" + drpclass.SelectedValue + " and SectionName='" + drpsection.SelectedItem.Text + "' and Evaluation='TERM1' and SubjectId=" + ds.Tables[0].Rows[m]["SubjectId_1"].ToString() + "  ";
                                        avg = avg + " and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' ";
                                        avg = avg + " and SrNo in (select SrNo from AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") stu ";
                                        avg = avg + " where stu.sessionName='" + Session["SessionName"] + "' and isnull(stu.Promotion, '')<>'Cancelled' ";
                                        avg = avg + " and stu.ClassId=" + drpclass.SelectedValue + " and stu.SectionName='" + drpsection.SelectedItem.Text + "' ";
                                        avg = avg + " and isnull(Withdrwal,'') = '' ";
                                        avg = avg + " and isnull(blocked,'') = '') ";
                                        avg = avg + " )T1 order by T1.avgMarks desc ";
                                        double avgMarks = double.Parse((_oo.ReturnTag(avg, "avgMarks").ToString() == "" ? "0" : _oo.ReturnTag(avg, "avgMarks")));
                                        string sname = ds.Tables[0].Rows[m]["ShortCode"].ToString();
                                       dts.Rows.Add(sname, highestMarks.ToString("0"), ObtMarks1.ToString("0"), (avgMarks / int.Parse(dt.Rows[i]["countStudents"].ToString())).ToString("0"));
                                       // dts.Rows.Add(sname, highestMarks.ToString("0"), ObtMarks1.ToString("0"));
                                    }
                                    Label lblHTotal = (Label)rptmarksT1.Controls[rptmarksT1.Controls.Count - 1].Controls[0].FindControl("lblHTotal");
                                    Label lblHPercentage = (Label)rptmarksT1.Controls[rptmarksT1.Controls.Count - 1].Controls[0].FindControl("lblHPercentage");
                                    Label lblHRank = (Label)rptmarksT1.Controls[rptmarksT1.Controls.Count - 1].Controls[0].FindControl("lblHRank");
                                    Label lblHPosition = (Label)rptmarksT1.Controls[rptmarksT1.Controls.Count - 1].Controls[0].FindControl("lblHPosition");
                                    lblHTotal.Text = totalM1.ToString().Trim() + "/" + totalMM1.ToString("0").Trim();
                                    lblHPercentage.Text = ((totalM1 * 100) / totalMM1).ToString("0.00") + " %";
                                    lblHRank.Text = (dsRank.Tables[0].Rows.Count > 0 ? dsRank.Tables[0].Rows[0]["ranks"].ToString() : "0");
                                    lblHPosition.Text = (dsRank.Tables[1].Rows.Count > 0 ? dsRank.Tables[1].Rows[0]["position"].ToString().Trim() : "-");

                                    Chart myChart = (Chart)rptStudent.Items[i].FindControl("myChart");
                                    chart(myChart, dts);
                                }
                                if (drpEval.SelectedValue.ToUpper() == "TERM2")
                                {
                                    DataTable dts = new DataTable();
                                    dts.Columns.AddRange(new DataColumn[] {
                                    new DataColumn("SubjectName",typeof(string)),
                                    new DataColumn("Highest Marks",typeof(int)),
                                    new DataColumn("Obtained Marks",typeof(int)),
                                    new DataColumn("Avg. Marks", typeof(int))
                                    });
                                    Repeater rptmarksT2 = (Repeater)rptStudent.Items[i].FindControl("rptmarksT2");
                                    rptmarksT2.DataSource = ds.Tables[0];
                                    rptmarksT2.DataBind();
                                    for (int p = 0; p < ds.Tables[0].Rows.Count; p++)
                                    {
                                        double Test1 = 0; double Test2 = 0; double NB = 0; double SE = 0; double SAT = 0; double Prac = 0;
                                        if (ds.Tables[0].Rows[p]["Test1_1"].ToString().Trim().ToUpper() == "NP" || ds.Tables[0].Rows[p]["Test1_1"].ToString().Trim().ToUpper() == "NAD" || ds.Tables[0].Rows[p]["Test1_1"].ToString().Trim().ToUpper() == "ML" || ds.Tables[0].Rows[p]["Test1_1"].ToString().Trim().ToUpper() == "" || ds.Tables[0].Rows[p]["Test1_1"].ToString().Trim().ToUpper() == "AB" || ds.Tables[0].Rows[p]["Test1_1"].ToString().Trim().ToUpper() == "NA" || ds.Tables[0].Rows[p]["Test1_1"].ToString().Trim().ToUpper() == "M.L") { }
                                        else { Test1 = double.Parse(ds.Tables[0].Rows[p]["Test1_1"].ToString().Trim()); }
                                        if (ds.Tables[0].Rows[p]["Test2_1"].ToString().Trim().ToUpper() == "NP" || ds.Tables[0].Rows[p]["Test2_1"].ToString().Trim().ToUpper() == "NAD" || ds.Tables[0].Rows[p]["Test2_1"].ToString().Trim().ToUpper() == "ML" || ds.Tables[0].Rows[p]["Test2_1"].ToString().Trim().ToUpper() == "" || ds.Tables[0].Rows[p]["Test2_1"].ToString().Trim().ToUpper() == "AB" || ds.Tables[0].Rows[p]["Test2_1"].ToString().Trim().ToUpper() == "NA" || ds.Tables[0].Rows[p]["Test2_1"].ToString().Trim().ToUpper() == "M.L") { }
                                        else { Test2 = double.Parse(ds.Tables[0].Rows[p]["Test2_1"].ToString().Trim()); }
                                        if (ds.Tables[0].Rows[p]["NB_1"].ToString().Trim().ToUpper() == "NP" || ds.Tables[0].Rows[p]["NB_1"].ToString().Trim().ToUpper() == "NAD" || ds.Tables[0].Rows[p]["NB_1"].ToString().Trim().ToUpper() == "ML" || ds.Tables[0].Rows[p]["NB_1"].ToString().Trim().ToUpper() == "" || ds.Tables[0].Rows[p]["NB_1"].ToString().Trim().ToUpper() == "AB" || ds.Tables[0].Rows[p]["NB_1"].ToString().Trim().ToUpper() == "NA" || ds.Tables[0].Rows[p]["NB_1"].ToString().Trim().ToUpper() == "M.L") { }
                                        else { NB = double.Parse(ds.Tables[0].Rows[p]["NB_1"].ToString().Trim()); }
                                        if (ds.Tables[0].Rows[p]["SE_1"].ToString().Trim().ToUpper() == "NP" || ds.Tables[0].Rows[p]["SE_1"].ToString().Trim().ToUpper() == "NAD" || ds.Tables[0].Rows[p]["SE_1"].ToString().Trim().ToUpper() == "ML" || ds.Tables[0].Rows[p]["SE_1"].ToString().Trim().ToUpper() == "" || ds.Tables[0].Rows[p]["SE_1"].ToString().Trim().ToUpper() == "AB" || ds.Tables[0].Rows[p]["SE_1"].ToString().Trim().ToUpper() == "NA" || ds.Tables[0].Rows[p]["SE_1"].ToString().Trim().ToUpper() == "M.L") { }
                                        else { SE = double.Parse(ds.Tables[0].Rows[p]["SE_1"].ToString().Trim()); }
                                        if (ds.Tables[0].Rows[p]["SAT_1"].ToString().Trim().ToUpper() == "NP" || ds.Tables[0].Rows[p]["SAT_1"].ToString().Trim().ToUpper() == "NAD" || ds.Tables[0].Rows[p]["SAT_1"].ToString().Trim().ToUpper() == "ML" || ds.Tables[0].Rows[p]["SAT_1"].ToString().Trim().ToUpper() == "" || ds.Tables[0].Rows[p]["SAT_1"].ToString().Trim().ToUpper() == "AB" || ds.Tables[0].Rows[p]["SAT_1"].ToString().Trim().ToUpper() == "NA" || ds.Tables[0].Rows[p]["SAT_1"].ToString().Trim().ToUpper() == "M.L") { }
                                        else { SAT = double.Parse(ds.Tables[0].Rows[p]["SAT_1"].ToString().Trim()); }
                                        if (ds.Tables[0].Rows[p]["Prac_1"].ToString().Trim().ToUpper() == "NP" || ds.Tables[0].Rows[p]["Prac_1"].ToString().Trim().ToUpper() == "NAD" || ds.Tables[0].Rows[p]["Prac_1"].ToString().Trim().ToUpper() == "ML" || ds.Tables[0].Rows[p]["Prac_1"].ToString().Trim().ToUpper() == "" || ds.Tables[0].Rows[p]["Prac_1"].ToString().Trim().ToUpper() == "AB" || ds.Tables[0].Rows[p]["Prac_1"].ToString().Trim().ToUpper() == "NA" || ds.Tables[0].Rows[p]["Prac_1"].ToString().Trim().ToUpper() == "M.L") { }
                                        else { Prac = double.Parse(ds.Tables[0].Rows[p]["Prac_1"].ToString().Trim()); }

                                        double totalmarks1 = (((Test1 > Test2 ? Test1 : Test2) * 10) / 20) + double.Parse(NB.ToString("0")) + double.Parse(SE.ToString("0"));
                                        double ObtMarks1 = (double.Parse(totalmarks1.ToString("0")) + double.Parse(SAT.ToString("0")) + double.Parse(Prac.ToString("0")));
                                        totalM1 = totalM1 + ObtMarks1;
                                        totalMM1 = totalMM1 + 100;
                                        string grade1 = grade(ObtMarks1);
                                        //==================================================================================================================================

                                        double Test1_2 = 0; double Test2_2 = 0; double NB_2 = 0; double SE_2 = 0; double SAT_2 = 0; double Prac_2 = 0;
                                        if (ds.Tables[0].Rows[p]["Test1_2"].ToString().Trim().ToUpper() == "NP" || ds.Tables[0].Rows[p]["Test1_2"].ToString().Trim().ToUpper() == "NAD" || ds.Tables[0].Rows[p]["Test1_2"].ToString().Trim().ToUpper() == "ML" || ds.Tables[0].Rows[p]["Test1_2"].ToString().Trim().ToUpper() == "" || ds.Tables[0].Rows[p]["Test1_2"].ToString().Trim().ToUpper() == "AB" || ds.Tables[0].Rows[p]["Test1_2"].ToString().Trim().ToUpper() == "NA" || ds.Tables[0].Rows[p]["Test1_2"].ToString().Trim().ToUpper() == "M.L") { }
                                        else { Test1_2 = double.Parse(ds.Tables[0].Rows[p]["Test1_2"].ToString().Trim()); }
                                        if (ds.Tables[0].Rows[p]["Test2_2"].ToString().Trim().ToUpper() == "NP" || ds.Tables[0].Rows[p]["Test2_2"].ToString().Trim().ToUpper() == "NAD" || ds.Tables[0].Rows[p]["Test2_2"].ToString().Trim().ToUpper() == "ML" || ds.Tables[0].Rows[p]["Test2_2"].ToString().Trim().ToUpper() == "" || ds.Tables[0].Rows[p]["Test2_2"].ToString().Trim().ToUpper() == "AB" || ds.Tables[0].Rows[p]["Test2_2"].ToString().Trim().ToUpper() == "NA" || ds.Tables[0].Rows[p]["Test2_2"].ToString().Trim().ToUpper() == "M.L") { }
                                        else { Test2_2 = double.Parse(ds.Tables[0].Rows[p]["Test2_2"].ToString().Trim()); }
                                        if (ds.Tables[0].Rows[p]["NB_2"].ToString().Trim().ToUpper() == "NP" || ds.Tables[0].Rows[p]["NB_2"].ToString().Trim().ToUpper() == "NAD" || ds.Tables[0].Rows[p]["NB_2"].ToString().Trim().ToUpper() == "ML" || ds.Tables[0].Rows[p]["NB_2"].ToString().Trim().ToUpper() == "" || ds.Tables[0].Rows[p]["NB_2"].ToString().Trim().ToUpper() == "AB" || ds.Tables[0].Rows[p]["NB_2"].ToString().Trim().ToUpper() == "NA" || ds.Tables[0].Rows[p]["NB_2"].ToString().Trim().ToUpper() == "M.L") { }
                                        else { NB_2 = double.Parse(ds.Tables[0].Rows[p]["NB_2"].ToString().Trim()); }
                                        if (ds.Tables[0].Rows[p]["SE_2"].ToString().Trim().ToUpper() == "NP" || ds.Tables[0].Rows[p]["SE_2"].ToString().Trim().ToUpper() == "NAD" || ds.Tables[0].Rows[p]["SE_2"].ToString().Trim().ToUpper() == "ML" || ds.Tables[0].Rows[p]["SE_2"].ToString().Trim().ToUpper() == "" || ds.Tables[0].Rows[p]["SE_2"].ToString().Trim().ToUpper() == "AB" || ds.Tables[0].Rows[p]["SE_2"].ToString().Trim().ToUpper() == "NA" || ds.Tables[0].Rows[p]["SE_2"].ToString().Trim().ToUpper() == "M.L") { }
                                        else { SE_2 = double.Parse(ds.Tables[0].Rows[p]["SE_2"].ToString().Trim()); }
                                        if (ds.Tables[0].Rows[p]["SAT_2"].ToString().Trim().ToUpper() == "NP" || ds.Tables[0].Rows[p]["SAT_2"].ToString().Trim().ToUpper() == "NAD" || ds.Tables[0].Rows[p]["SAT_2"].ToString().Trim().ToUpper() == "ML" || ds.Tables[0].Rows[p]["SAT_2"].ToString().Trim().ToUpper() == "" || ds.Tables[0].Rows[p]["SAT_2"].ToString().Trim().ToUpper() == "AB" || ds.Tables[0].Rows[p]["SAT_2"].ToString().Trim().ToUpper() == "NA" || ds.Tables[0].Rows[p]["SAT_2"].ToString().Trim().ToUpper() == "M.L") { }
                                        else { SAT_2 = double.Parse(ds.Tables[0].Rows[p]["SAT_2"].ToString().Trim()); }
                                        if (ds.Tables[0].Rows[p]["Prac_2"].ToString().Trim().ToUpper() == "NP" || ds.Tables[0].Rows[p]["Prac_2"].ToString().Trim().ToUpper() == "NAD" || ds.Tables[0].Rows[p]["Prac_2"].ToString().Trim().ToUpper() == "ML" || ds.Tables[0].Rows[p]["Prac_2"].ToString().Trim().ToUpper() == "" || ds.Tables[0].Rows[p]["Prac_2"].ToString().Trim().ToUpper() == "AB" || ds.Tables[0].Rows[p]["Prac_2"].ToString().Trim().ToUpper() == "NA" || ds.Tables[0].Rows[p]["Prac_2"].ToString().Trim().ToUpper() == "M.L") { }
                                        else { Prac_2 = double.Parse(ds.Tables[0].Rows[p]["Prac_2"].ToString().Trim()); }

                                        double totalmarks2 = (((Test1_2 > Test2_2 ? Test1_2 : Test2_2) * 10) / 20) + double.Parse(NB_2.ToString("0")) + double.Parse(SE_2.ToString("0"));
                                        double ObtMarks2 = (double.Parse(totalmarks2.ToString("0")) + double.Parse(SAT_2.ToString("0")) + double.Parse(Prac_2.ToString("0")));
                                        totalM2 = totalM2 + ObtMarks2;
                                        totalMM2 = totalMM2 + 100;
                                        string grade2 = grade(ObtMarks2);
                                        string totalGrade = grade(double.Parse(((ObtMarks1 + ObtMarks2) / 2).ToString("0")));
                                        Label lblIA1 = (Label)rptmarksT2.Items[p].FindControl("lblIA1");
                                        Label lblTheory1 = (Label)rptmarksT2.Items[p].FindControl("lblTheory1");
                                        Label lblTotal1 = (Label)rptmarksT2.Items[p].FindControl("lblTotal1");
                                        Label lblGrade1 = (Label)rptmarksT2.Items[p].FindControl("lblGrade1");
                                        Label lblIA2 = (Label)rptmarksT2.Items[p].FindControl("lblIA2");
                                        Label lblTheory2 = (Label)rptmarksT2.Items[p].FindControl("lblTheory2");
                                        Label lblTotal2 = (Label)rptmarksT2.Items[p].FindControl("lblTotal2");
                                        Label lblGrade2 = (Label)rptmarksT2.Items[p].FindControl("lblGrade2");
                                        Label lblGrandTotal = (Label)rptmarksT2.Items[p].FindControl("lblGrandTotal");
                                        Label lblGrandTotal2 = (Label)rptmarksT2.Items[p].FindControl("lblGrandTotal2");
                                        Label lblGrandGrade2 = (Label)rptmarksT2.Items[p].FindControl("lblGrandGrade2");
                                        lblIA1.Text = (totalmarks1 == 0 ? "NP" : totalmarks1.ToString("0"));
                                        lblTheory1.Text = ((SAT + Prac) == 0 ? "NP" : (double.Parse(SAT.ToString("0")) + double.Parse(Prac.ToString("0"))).ToString("0"));
                                        lblTotal1.Text = ObtMarks1.ToString("0");
                                        lblGrade1.Text = grade1;
                                        lblIA2.Text = (totalmarks2 == 0 ? "NP" : totalmarks2.ToString("0"));
                                        lblTheory2.Text =((SAT_2+ Prac_2)==0?"NP":(double.Parse(SAT_2.ToString("0")) + double.Parse(Prac_2.ToString("0"))).ToString("0"));
                                        lblTotal2.Text = ObtMarks2.ToString("0");
                                        lblGrade2.Text = grade2;
                                        lblGrandTotal.Text = (ObtMarks1 + ObtMarks2).ToString("0");
                                        lblGrandTotal2.Text = ((ObtMarks1 + ObtMarks2) / 2).ToString("0");
                                        lblGrandGrade2.Text = totalGrade;

                                        string hi = "select max(T1.highestMarks)highestMarks from (select sum(TRY_CONVERT(decimal(10,0),((case when isnull(TRY_CONVERT(decimal(10,0),Test1),0)>isnull(TRY_CONVERT(decimal(10,0),Test2),0) then isnull(TRY_CONVERT(decimal(10,0),Test1),0)  ";
                                        hi = hi + " else  isnull(TRY_CONVERT(decimal(10,0),Test2),0) end)*10)/20+isnull(TRY_CONVERT(decimal(10,0),NB),0)+isnull(TRY_CONVERT(decimal(10,0),SE),0) +isnull(TRY_CONVERT(decimal(10,0),SAT),0)+isnull(TRY_CONVERT(decimal(10,0),Prac),0))) highestMarks ";
                                        hi = hi + " from CCEItoV where ClassId=" + drpclass.SelectedValue + " and SectionName='" + drpsection.SelectedItem.Text + "' and SubjectId=" + ds.Tables[0].Rows[p]["SubjectId_1"].ToString() + "  ";
                                        hi = hi + " and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "'  ";
                                        hi = hi + " and SrNo in (select SrNo from AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") stu ";
                                        hi = hi + " where stu.sessionName='" + Session["SessionName"] + "' and isnull(stu.Promotion, '')<>'Cancelled' ";
                                        hi = hi + " and stu.ClassId=" + drpclass.SelectedValue + " and stu.SectionName='" + drpsection.SelectedItem.Text + "' ";
                                        hi = hi + " and isnull(Withdrwal,'') = '' ";
                                        hi = hi + " and isnull(blocked,'') = '')  group by SrNo ";
                                        hi = hi + "  )T1 ";
                                        double highestMarks = double.Parse((_oo.ReturnTag(hi, "highestMarks").ToString() == "" ? "0" : _oo.ReturnTag(hi, "highestMarks")));
                                        string avg = "select sum(T1.avgMarks)avgMarks from (select TRY_CONVERT(decimal(10,0),(TRY_CONVERT(decimal(10,0), (((case when isnull(TRY_CONVERT(decimal(10,0),Test1),0)> isnull(TRY_CONVERT(decimal(10,0),Test2),0)  then isnull(TRY_CONVERT(decimal(10,0),Test1),0) else  isnull(TRY_CONVERT(decimal(10,0),Test2),0) end)*10)/20)+isnull(TRY_CONVERT(decimal(10,0),NB),0)+isnull(TRY_CONVERT(decimal(10,0),SE),0) +isnull(TRY_CONVERT(decimal(10,0),SAT),0)+isnull(TRY_CONVERT(decimal(10,0),Prac),0))/2)) avgMarks";
                                        avg = avg + " from CCEItoV where ClassId=" + drpclass.SelectedValue + " and SectionName='" + drpsection.SelectedItem.Text + "' and SubjectId=" + ds.Tables[0].Rows[p]["SubjectId_1"].ToString() + "  ";
                                        avg = avg + " and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' ";
                                        avg = avg + " and SrNo in (select SrNo from AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") stu ";
                                        avg = avg + " where stu.sessionName='" + Session["SessionName"] + "' and isnull(stu.Promotion, '')<>'Cancelled' ";
                                        avg = avg + " and stu.ClassId=" + drpclass.SelectedValue + " and stu.SectionName='" + drpsection.SelectedItem.Text + "' and isnull(Withdrwal,'') = '' and isnull(blocked,'') = '') ";
                                        avg = avg + " )T1";
                                        double avgMarks2 = double.Parse((_oo.ReturnTag(avg, "avgMarks").ToString() == "" ? "0" : _oo.ReturnTag(avg, "avgMarks")));
                                        double totalObt = (ObtMarks1 + ObtMarks2) / 2;
                                        string sname = ds.Tables[0].Rows[p]["ShortCode"].ToString();
                                        dts.Rows.Add(sname, (highestMarks / 2).ToString("0"), totalObt.ToString("0"), (avgMarks2 / int.Parse(dt.Rows[i]["countStudents"].ToString())).ToString("0"));
                                       // dts.Rows.Add(sname, (highestMarks / 2).ToString("0"), totalObt.ToString("0"));
                                        totalForPer = totalForPer + double.Parse((ObtMarks2 == 0 ? ObtMarks1 : (ObtMarks1 + ObtMarks2) / 2).ToString("0"));
                                    }
                                    Label lblHTotalT2 = (Label)rptmarksT2.Controls[rptmarksT2.Controls.Count - 1].Controls[0].FindControl("lblHTotalT2");
                                    Label lblHTotal2T2 = (Label)rptmarksT2.Controls[rptmarksT2.Controls.Count - 1].Controls[0].FindControl("lblHTotal2T2");
                                    Label lblHTotalGT2 = (Label)rptmarksT2.Controls[rptmarksT2.Controls.Count - 1].Controls[0].FindControl("lblHTotalGT2");
                                    Label lblHPercentageT2 = (Label)rptmarksT2.Controls[rptmarksT2.Controls.Count - 1].Controls[0].FindControl("lblHPercentageT2");
                                    Label lblHPercentage2T2 = (Label)rptmarksT2.Controls[rptmarksT2.Controls.Count - 1].Controls[0].FindControl("lblHPercentage2T2");
                                    Label lblHPercentageGT2 = (Label)rptmarksT2.Controls[rptmarksT2.Controls.Count - 1].Controls[0].FindControl("lblHPercentageGT2");
                                    Label lblHRankT2 = (Label)rptmarksT2.Controls[rptmarksT2.Controls.Count - 1].Controls[0].FindControl("lblHRankT2");
                                    Label lblHRank2T2 = (Label)rptmarksT2.Controls[rptmarksT2.Controls.Count - 1].Controls[0].FindControl("lblHRank2T2");
                                    Label lblHRankGT2 = (Label)rptmarksT2.Controls[rptmarksT2.Controls.Count - 1].Controls[0].FindControl("lblHRankGT2");
                                    Label lblHPositionT2 = (Label)rptmarksT2.Controls[rptmarksT2.Controls.Count - 1].Controls[0].FindControl("lblHPositionT2");
                                    Label lblHPosition2T2 = (Label)rptmarksT2.Controls[rptmarksT2.Controls.Count - 1].Controls[0].FindControl("lblHPosition2T2");
                                    Label lblHPositionGT2 = (Label)rptmarksT2.Controls[rptmarksT2.Controls.Count - 1].Controls[0].FindControl("lblHPositionGT2");
                                    lblHTotalT2.Text = totalM1.ToString().Trim() + "/" + totalMM1.ToString("0").Trim();
                                    lblHTotal2T2.Text = totalM2.ToString().Trim() + "/" + totalMM2.ToString("0").Trim();
                                    lblHTotalGT2.Text = totalForPer.ToString("0").Trim() + "/" + ((totalMM1 + totalMM2) / 2).ToString("0");
                                    lblHPercentageT2.Text = ((totalM1 * 100) / totalMM1).ToString("0.00") + " %";
                                    lblHPercentage2T2.Text = ((totalM2 * 100) / totalMM2).ToString("0.00") + " %";
                                    lblHPercentageGT2.Text = ((totalForPer * 100) / ((totalMM1 + totalMM2) / 2)).ToString("0.00") + " %";
                                    lblHRankT2.Text = (dsRank.Tables[0].Rows.Count > 0 ? dsRank.Tables[0].Rows[0]["ranks"].ToString().Trim() : "-");
                                    lblHRank2T2.Text = (dsRank.Tables[2].Rows.Count > 0 ? dsRank.Tables[2].Rows[0]["ranks"].ToString().Trim() : "-");
                                    lblHRankGT2.Text = (dsRank.Tables[4].Rows.Count > 0 ? dsRank.Tables[4].Rows[0]["ranks"].ToString().Trim() : "-");
                                    lblHPositionT2.Text = (dsRank.Tables[1].Rows.Count > 0 ? dsRank.Tables[1].Rows[0]["position"].ToString().Trim() : "-");
                                    lblHPosition2T2.Text = (dsRank.Tables[3].Rows.Count > 0 ? dsRank.Tables[3].Rows[0]["position"].ToString().Trim() : "-");
                                    lblHPositionGT2.Text = (dsRank.Tables[5].Rows.Count > 0 ? dsRank.Tables[5].Rows[0]["position"].ToString().Trim() : "-");
                                    Chart myChart = (Chart)rptStudent.Items[i].FindControl("myChart");
                                    chart(myChart, dts);
                                }
                                DataTable dtAtt = new DataTable();
                                dtAtt.Columns.Add("t1Att", typeof(string));
                                dtAtt.Columns.Add("t2Att", typeof(string));
                                if (drpEval.SelectedValue.ToUpper() == "TERM1")
                                {
                                    DataRow dr = dtAtt.NewRow();

                                    double totaldays = 0, attendence = 0, percent = 0; string t1Att = "";
                                    totaldays = double.Parse(ds.Tables[1].Rows[0]["totaldays"].ToString().Trim() == "" ? "0" : ds.Tables[1].Rows[0]["totaldays"].ToString().Trim());
                                    attendence = double.Parse(ds.Tables[1].Rows[0]["attendence"].ToString().Trim() == "" ? "0" : ds.Tables[1].Rows[0]["attendence"].ToString().Trim());
                                    if (attendence != 0 && totaldays != 0)
                                    {
                                        percent = (attendence * 100) / totaldays;
                                        t1Att = "<span >" + attendence.ToString().Trim() + "</span>/<span >" + totaldays.ToString().Trim() + "</span> (<span >" + percent.ToString() + "</span> %)";
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
                                    totaldays1 = double.Parse(ds.Tables[1].Rows[0]["totaldays"].ToString().Trim() == "" ? "0" : ds.Tables[1].Rows[0]["totaldays"].ToString().Trim());
                                    attendence1 = double.Parse(ds.Tables[1].Rows[0]["attendence"].ToString().Trim() == "" ? "0" : ds.Tables[1].Rows[0]["attendence"].ToString().Trim());

                                    if (attendence1 != 0 && totaldays1 != 0)
                                    {
                                        percent1 = (attendence1 * 100) / totaldays1;
                                        t1Att = "<span >" + attendence1.ToString().Trim() + "</span>/<span >" + totaldays1.ToString().Trim() + "</span> (<span >" + percent1.ToString("0.00") + "</span> %)";
                                    }
                                    double totaldays2 = 0, attendence2 = 0, percent2 = 0;
                                    totaldays2 = double.Parse(ds.Tables[2].Rows[0]["totaldays"].ToString().Trim() == "" ? "0" : ds.Tables[2].Rows[0]["totaldays"].ToString().Trim());
                                    attendence2 = double.Parse(ds.Tables[2].Rows[0]["attendence"].ToString().Trim() == "" ? "0" : ds.Tables[2].Rows[0]["attendence"].ToString().Trim());

                                    if (attendence2 != 0 && totaldays2 != 0)
                                    {
                                        percent2 = (attendence2 * 100) / totaldays2;
                                        t2Att = "<span >" + attendence2.ToString().Trim() + "</span>/<span >" + totaldays2.ToString().Trim() + "</span> (<span >" + percent2.ToString("0.00") + "</span> %)";
                                    }
                                    dr["t1Att"] = t1Att.ToString();
                                    dr["t2Att"] = t2Att.ToString();
                                    dtAtt.Rows.Add(dr);
                                    Repeater rptAttendance2 = (Repeater)rptStudent.Items[i].FindControl("rptAttendance2");
                                    rptAttendance2.DataSource = dtAtt;
                                    rptAttendance2.DataBind();
                                }
                                if (ds.Tables[3].Rows.Count > 0)
                                {
                                    if (drpEval.SelectedValue.ToUpper() == "TERM1")
                                    {
                                        Repeater rptRemark1 = (Repeater)rptStudent.Items[i].FindControl("rptRemark1");
                                        rptRemark1.DataSource = ds.Tables[3];
                                        rptRemark1.DataBind();
                                    }
                                    if (drpEval.SelectedValue.ToUpper() == "TERM2")
                                    {
                                        Repeater rptRemark2 = (Repeater)rptStudent.Items[i].FindControl("rptRemark2");
                                        rptRemark2.DataSource = ds.Tables[3];
                                        rptRemark2.DataBind();
                                    }
                                }
                                HtmlTable tblResult = (HtmlTable)rptStudent.Items[i].FindControl("tblResult");
                                if (drpEval.SelectedValue.ToUpper() == "TERM2" && Session["Logintype"].ToString() != "Guardian")
                                {
                                    tblResult.Visible = true;
                                    Label lblresulttype = (Label)rptStudent.Items[i].FindControl("lblresulttype");
                                    lblresulttype.Text = drpPromoTo.SelectedItem.Text;
                                    Label lblpromotedClass = (Label)rptStudent.Items[i].FindControl("lblpromotedClass");
                                    lblpromotedClass.Text = txtPromotedtoclass.Text;
                                    Label lblReopenon = (Label)rptStudent.Items[i].FindControl("lblReopenon");
                                    lblReopenon.Text = txtSchoolReopenon.Text + " " + txtTimes.Text;
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
                                lblprintdate.Text = txtDate.Text;
                                Label lblPlace = (Label)rptStudent.Items[i].FindControl("lblPlace");
                                lblPlace.Text = txtPlace.Text;

                                if (ds.Tables[2].Rows.Count > 0)
                                {
                                    if (drpEval.SelectedValue.ToUpper() == "TERM1")
                                    {
                                        Repeater rptSkil1 = (Repeater)rptStudent.Items[i].FindControl("rptSkil1");
                                        rptSkil1.DataSource = ds.Tables[4];
                                        rptSkil1.DataBind();
                                    }
                                    if (drpEval.SelectedValue.ToUpper() == "TERM2")
                                    {
                                        Repeater rptSkil2 = (Repeater)rptStudent.Items[i].FindControl("rptSkil2");
                                        rptSkil2.DataSource = ds.Tables[4];
                                        rptSkil2.DataBind();
                                    }
                                }
                            }


                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
            }
        }
    }
    protected void chart(Chart myChart, DataTable dt)
    {
        List<string> columns = new List<string>();
        columns.Add("Highest Marks");
        columns.Add("Obtained Marks");
        //columns.Add("Avg. Marks");
        string[] x = (from p in dt.AsEnumerable()
                      select p.Field<string>("SubjectName")).Distinct().ToArray();
        for (int i = 0; i < columns.Count; i++)
        {
            int[] y = (from p in dt.AsEnumerable()
                       select p.Field<int>(columns[i])).ToArray();
            myChart.Series.Add(new Series(columns[i]));
            myChart.Series[columns[i]].IsValueShownAsLabel = true;
            myChart.Series[columns[i]].ChartType = SeriesChartType.Column;
            myChart.Series[columns[i]].Points.DataBindXY(x, y);
        }
        ChartArea CA = myChart.ChartAreas[0];
        CA.Position = new ElementPosition(0, 0, 100, 85);
        myChart.Legends[0].Enabled = true;
        myChart.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
        myChart.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = true;
        myChart.ChartAreas[0].AxisY.Maximum = 100;
        myChart.ChartAreas[0].AxisY.Minimum = 0;
        myChart.ChartAreas[0].AxisY.Interval = 20;
        myChart.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = System.Drawing.ColorTranslator.FromHtml("#d7d7d7");
        myChart.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineColor = System.Drawing.ColorTranslator.FromHtml("#d7d7d7");
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
    }

    protected void drpsection_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSrNo();
    }

    protected void LoadSrNo()
    {
        string drpclass1 = drpclass.SelectedValue;
        string drpsection1 = drpsection.SelectedValue;
        string drpStatus1 = drpStatus.SelectedValue;
        List<SqlParameter> param = new List<SqlParameter>();
        _sql = @"Select Name+' - '+SrNo NAME,SrNo from AllStudentRecord_UDF(@SessionName,@BranchCode) where 
                @Classid=Classid and @Sectionid=CASE WHEN @Sectionid='' THEN @Sectionid ELSE Sectionid END  and isnull(Withdrwal,'') = case when isnull(@Withdrwal,'')='B' or isnull(@Withdrwal,'')='' then isnull(Withdrwal,'') else case when isnull(@Withdrwal,'')='A' then '' else 'W' end end
            and isnull(blocked,'') = case when isnull(@Withdrwal,'')='W' or isnull(@Withdrwal,'')='' then isnull(blocked,'') else case when isnull(@Withdrwal,'')='A' then '' else 'yes' end end  ORDER BY NAME";

        param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
        param.Add(new SqlParameter("@Classid", drpclass1.ToString()));
        param.Add(new SqlParameter("@Sectionid", drpsection1.ToString()));
        if (drpStatus1 == "")
        {
            param.Add(new SqlParameter("@Withdrwal", null));
        }
        else
        {
            param.Add(new SqlParameter("@Withdrwal", drpStatus1.ToString()));
        }

        var ds = DLL.objDll.SelectRecord_usingExecuteDataset(_sql, param);
        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {
            drpsrno.DataSource = ds.Tables[0];
            drpsrno.DataValueField = "SrNo";
            drpsrno.DataTextField = "NAME";
            drpsrno.DataBind();
        }
        drpsrno.Items.Insert(0, new ListItem("<--Select-->", ""));
    }
    protected void lnkView_Click(object sender, EventArgs e)
    {
        LoadReportCard();
    }
    public string grade(double percentle)
    {
        if (percentle <= 39)
        {
            return "E";
        }
        else if (percentle >= 39.1 && percentle <= 50)
        {
            return "C2";
        }
        else if (percentle >= 50.1 && percentle <= 60)
        {
            return "C1";
        }
        else if (percentle >= 60.1 && percentle <= 70)
        {
            return "B2";
        }
        else if (percentle >= 70.1 && percentle <= 80)
        {
            return "B1";
        }
        else if (percentle >= 80.1 && percentle <= 90)
        {
            return "A2";
        }
        else if (percentle >= 90.1 && percentle <= 100)
        {
            return "A1";
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
            if (drpEval.SelectedValue == "Term1")
            {
                div2.Visible = false;
                div3.Visible = false;
                div4.Visible = false;
                txtDate.CssClass = "form-control-blue datepicker-normal";
                txtPromotedtoclass.CssClass = "form-control-blue";
                txtSchoolReopenon.CssClass = "form-control-blue datepicker-normal";
            }
            if (drpEval.SelectedValue == "Term2")
            {
                div2.Visible = true;
                div3.Visible = true;
                div4.Visible = true;
                txtDate.CssClass = "validatetxt form-control-blue datepicker-normal";
                txtPromotedtoclass.CssClass = "validatetxt form-control-blue";
                txtSchoolReopenon.CssClass = "validatetxt form-control-blue datepicker-normal";
            }
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
}