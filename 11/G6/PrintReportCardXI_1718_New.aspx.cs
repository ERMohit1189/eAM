using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Linq;
using System.Web.UI.DataVisualization.Charting;

public partial class PrintReportCardXI_1718_New : Page
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
          //  txtDate.Text = Convert.ToDateTime(BAL.objBal.CurrentDate()).ToString("dd-MMM-yyyy");
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

           // txtSchoolReopenon.Attributes.Add("readonly", "readonly");
           // txtDate.Attributes.Add("readonly", "readonly");
            string ss = "select CityName from CityMaster where id=(select CityId from CollegeMaster where BranchCode=" + Session["BranchCode"] + ")";
            //txtPlace.Text = _oo.ReturnTag(ss, "CityName");
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
                    string sql1s = "select ClassId, SrNo, BranchId, SectionId, blocked from AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "'," + Session["BranchCode"].ToString() + ") where SrNo='" + Session["Srno"].ToString() + "'";
                    drpclass.SelectedValue = _oo.ReturnTag(sql1s, "ClassId").ToString();
                    _sql = "Select SectionName,Id from SectionMaster where ClassNameId='" + drpclass.SelectedValue + "' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
                    _oo.FillDropDown_withValue(_sql, drpsection, "SectionName", "id");
                    drpsection.SelectedValue = _oo.ReturnTag(sql1s, "SectionId").ToString();
                    _sql = "Select BranchName,Id from BranchMaster where ClassId='" + drpclass.SelectedValue + "' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
                    _oo.FillDropDown_withValue(_sql, drpBranch, "BranchName", "id");
                    drpBranch.SelectedValue = _oo.ReturnTag(sql1s, "BranchId").ToString();
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
                        //divHideForGardian7.Style.Add("display", "none !important");
                        //divHideForGardian7.Style.Add("visibility", "hidden;");
                        //divHideForGardian8.Style.Add("display", "none !important");
                        //divHideForGardian8.Style.Add("visibility", "hidden;");
                        divHideForGardian9.Style.Add("display", "none !important");
                        divHideForGardian9.Style.Add("visibility", "hidden;");
                        divHideForGardian10.Style.Add("display", "none !important");
                        divHideForGardian10.Style.Add("visibility", "hidden;");
                        //div2.Style.Add("display", "none !important");
                        //div2.Style.Add("visibility", "hidden;");
                        //div3.Style.Add("display", "none !important");
                        //div3.Style.Add("visibility", "hidden;");
                        //div4.Style.Add("display", "none !important");
                        //div4.Style.Add("visibility", "hidden;");
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
                cmd.CommandText = "ReportCard_XI_2021";
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
                cmd.Parameters.AddWithValue("@BranchId", drpBranch.SelectedValue);
                if (drpStream.SelectedIndex > 0)
                {
                    cmd.Parameters.AddWithValue("@StreamId", drpStream.SelectedValue);
                }
                cmd.Parameters.AddWithValue("@SectionId", drpsection.SelectedValue);
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
                        cmd.CommandText = "ReportCard_XI_2021";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@sessionName", Session["SessionName"]);
                        cmd.Parameters.AddWithValue("@SrNo", dt.Rows[i]["admissionNo"].ToString().Trim());
                        cmd.Parameters.AddWithValue("@SectionName", drpsection.SelectedItem.Text.Trim());
                        cmd.Parameters.AddWithValue("@TermName", drpEval.SelectedValue);
                        cmd.Parameters.AddWithValue("@ClassId", drpclass.SelectedValue);
                        cmd.Parameters.AddWithValue("@BranchId", drpBranch.SelectedValue);
                        if (drpStream.SelectedIndex>0)
                        {
                            cmd.Parameters.AddWithValue("@StreamId", drpStream.SelectedValue);
                        }
                        cmd.Parameters.AddWithValue("@SectionId", drpsection.SelectedValue);
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
                            cmd.CommandText = "calculateRank_XI_2021";
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
                            cmd.Parameters.AddWithValue("@SrNo", dt.Rows[i]["admissionNo"].ToString().Trim());
                            cmd.Parameters.AddWithValue("@branchCode", Session["BranchCode"]);
                            cmd.Parameters.AddWithValue("@SectionName", drpsection.SelectedItem.Text);
                            cmd.Parameters.AddWithValue("@ClassId", drpclass.SelectedValue);
                            cmd.Parameters.AddWithValue("@BranchId", drpBranch.SelectedValue);

                            SqlDataAdapter daRank = new SqlDataAdapter(cmd);
                            DataSet dsRank = new DataSet();
                            daRank.Fill(dsRank);
                            cmd.Parameters.Clear();

                            double totalM1 = 0; double totalMM1 = 0; double totalM2 = 0; double totalMM2 = 0;
                            DataTable totalNum = new DataTable();
                            totalNum.Columns.Add("IsCompalsary", typeof(string));
                            totalNum.Columns.Add("Marks", typeof(decimal));
                            

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
                                    new DataColumn("Avg. Marks", typeof(int))});
                                    Repeater rptmarksT1 = (Repeater)rptStudent.Items[i].FindControl("rptmarksT1");
                                    rptmarksT1.DataSource = ds.Tables[0];
                                    rptmarksT1.DataBind();
                                    for (int m = 0; m < rptmarksT1.Items.Count; m++)
                                    {
                                        double MaxMarks1 = 0; double MaxMarks2 = 0; double MaxMarks3 = 0; double MaxMarks4 = 0; double Test1 = 0; double Test2 = 0; double Prac = 0; double SAT = 0;
                                        if (ds.Tables[0].Rows[m]["MaxMarks1_1"].ToString().Trim().ToUpper() == "NP" || ds.Tables[0].Rows[m]["MaxMarks1_1"].ToString().Trim().ToUpper() == "NAD" || ds.Tables[0].Rows[m]["MaxMarks1_1"].ToString().Trim().ToUpper() == "ML" || ds.Tables[0].Rows[m]["MaxMarks1_1"].ToString().Trim().ToUpper() == "" || ds.Tables[0].Rows[m]["MaxMarks1_1"].ToString().Trim().ToUpper() == "AB" || ds.Tables[0].Rows[m]["MaxMarks1_1"].ToString().Trim().ToUpper() == "NA" || ds.Tables[0].Rows[m]["MaxMarks1_1"].ToString().Trim().ToUpper() == "M.L") { }
                                        else { MaxMarks1 = double.Parse(ds.Tables[0].Rows[m]["MaxMarks1_1"].ToString().Trim()); }
                                        if (ds.Tables[0].Rows[m]["MaxMarks2_1"].ToString().Trim().ToUpper() == "NP" || ds.Tables[0].Rows[m]["MaxMarks2_1"].ToString().Trim().ToUpper() == "NAD" || ds.Tables[0].Rows[m]["MaxMarks2_1"].ToString().Trim().ToUpper() == "ML" || ds.Tables[0].Rows[m]["MaxMarks2_1"].ToString().Trim().ToUpper() == "" || ds.Tables[0].Rows[m]["MaxMarks2_1"].ToString().Trim().ToUpper() == "AB" || ds.Tables[0].Rows[m]["MaxMarks2_1"].ToString().Trim().ToUpper() == "NA" || ds.Tables[0].Rows[m]["MaxMarks2_1"].ToString().Trim().ToUpper() == "M.L") { }
                                        else { MaxMarks2 = double.Parse(ds.Tables[0].Rows[m]["MaxMarks2_1"].ToString().Trim()); }
                                        if (ds.Tables[0].Rows[m]["MaxMarks3_1"].ToString().Trim().ToUpper() == "NP" || ds.Tables[0].Rows[m]["MaxMarks3_1"].ToString().Trim().ToUpper() == "NAD" || ds.Tables[0].Rows[m]["MaxMarks3_1"].ToString().Trim().ToUpper() == "ML" || ds.Tables[0].Rows[m]["MaxMarks3_1"].ToString().Trim().ToUpper() == "" || ds.Tables[0].Rows[m]["MaxMarks3_1"].ToString().Trim().ToUpper() == "AB" || ds.Tables[0].Rows[m]["MaxMarks3_1"].ToString().Trim().ToUpper() == "NA" || ds.Tables[0].Rows[m]["MaxMarks3_1"].ToString().Trim().ToUpper() == "M.L") { }
                                        else { MaxMarks3 = double.Parse(ds.Tables[0].Rows[m]["MaxMarks3_1"].ToString().Trim()); }
                                        if (ds.Tables[0].Rows[m]["MaxMarks4_1"].ToString().Trim().ToUpper() == "NP" || ds.Tables[0].Rows[m]["MaxMarks4_1"].ToString().Trim().ToUpper() == "NAD" || ds.Tables[0].Rows[m]["MaxMarks4_1"].ToString().Trim().ToUpper() == "ML" || ds.Tables[0].Rows[m]["MaxMarks4_1"].ToString().Trim().ToUpper() == "" || ds.Tables[0].Rows[m]["MaxMarks4_1"].ToString().Trim().ToUpper() == "AB" || ds.Tables[0].Rows[m]["MaxMarks4_1"].ToString().Trim().ToUpper() == "NA" || ds.Tables[0].Rows[m]["MaxMarks4_1"].ToString().Trim().ToUpper() == "M.L") { }
                                        else { MaxMarks4 = double.Parse(ds.Tables[0].Rows[m]["MaxMarks4_1"].ToString().Trim()); }
                                        if (ds.Tables[0].Rows[m]["Test1_1"].ToString().Trim().ToUpper() == "NP" || ds.Tables[0].Rows[m]["Test1_1"].ToString().Trim().ToUpper() == "NAD" || ds.Tables[0].Rows[m]["Test1_1"].ToString().Trim().ToUpper() == "ML" || ds.Tables[0].Rows[m]["Test1_1"].ToString().Trim().ToUpper() == "" || ds.Tables[0].Rows[m]["Test1_1"].ToString().Trim().ToUpper() == "AB" || ds.Tables[0].Rows[m]["Test1_1"].ToString().Trim().ToUpper() == "NA" || ds.Tables[0].Rows[m]["Test1_1"].ToString().Trim().ToUpper() == "M.L") { }
                                        else { Test1 = double.Parse(ds.Tables[0].Rows[m]["Test1_1"].ToString().Trim()); }
                                        if (ds.Tables[0].Rows[m]["Test2_1"].ToString().Trim().ToUpper() == "NP" || ds.Tables[0].Rows[m]["Test2_1"].ToString().Trim().ToUpper() == "NAD" || ds.Tables[0].Rows[m]["Test2_1"].ToString().Trim().ToUpper() == "ML" || ds.Tables[0].Rows[m]["Test2_1"].ToString().Trim().ToUpper() == "" || ds.Tables[0].Rows[m]["Test2_1"].ToString().Trim().ToUpper() == "AB" || ds.Tables[0].Rows[m]["Test2_1"].ToString().Trim().ToUpper() == "NA" || ds.Tables[0].Rows[m]["Test2_1"].ToString().Trim().ToUpper() == "M.L") { }
                                        else { Test2 = double.Parse(ds.Tables[0].Rows[m]["Test2_1"].ToString().Trim()); }
                                        if (ds.Tables[0].Rows[m]["Prac_1"].ToString().Trim().ToUpper() == "NP" || ds.Tables[0].Rows[m]["Prac_1"].ToString().Trim().ToUpper() == "NAD" || ds.Tables[0].Rows[m]["Prac_1"].ToString().Trim().ToUpper() == "ML" || ds.Tables[0].Rows[m]["Prac_1"].ToString().Trim().ToUpper() == "" || ds.Tables[0].Rows[m]["Prac_1"].ToString().Trim().ToUpper() == "AB" || ds.Tables[0].Rows[m]["Prac_1"].ToString().Trim().ToUpper() == "NA" || ds.Tables[0].Rows[m]["Prac_1"].ToString().Trim().ToUpper() == "M.L") { }
                                        else { Prac = double.Parse(ds.Tables[0].Rows[m]["Prac_1"].ToString().Trim()); }
                                        if (ds.Tables[0].Rows[m]["SAT_1"].ToString().Trim().ToUpper() == "NP" || ds.Tables[0].Rows[m]["SAT_1"].ToString().Trim().ToUpper() == "NAD" || ds.Tables[0].Rows[m]["SAT_1"].ToString().Trim().ToUpper() == "ML" || ds.Tables[0].Rows[m]["SAT_1"].ToString().Trim().ToUpper() == "" || ds.Tables[0].Rows[m]["SAT_1"].ToString().Trim().ToUpper() == "AB" || ds.Tables[0].Rows[m]["SAT_1"].ToString().Trim().ToUpper() == "NA" || ds.Tables[0].Rows[m]["SAT_1"].ToString().Trim().ToUpper() == "M.L") { }
                                        else { SAT = double.Parse(ds.Tables[0].Rows[m]["SAT_1"].ToString().Trim()); }
                                        double totalmarks1 = 0; double IA1 = 0;
                                        totalmarks1 = (MaxMarks1 > 0 ? (double.Parse(Test1.ToString("0")) * 20) / MaxMarks1 : 0) > (MaxMarks2 > 0 ? (double.Parse(Test2.ToString("0")) * 20) / MaxMarks2 : 0) ? (MaxMarks1 > 0 ? (double.Parse(Test1.ToString("0")) * 20) / MaxMarks1 : 0) : (MaxMarks2 > 0 ? (double.Parse(Test2.ToString("0")) * 20) / MaxMarks2 : 0);
                                        IA1 = ((totalmarks1 * 10) / 20);
                                        double ConvinBoard = (((double.Parse(IA1.ToString("0")) + double.Parse(SAT.ToString("0"))) / (10 + MaxMarks3)) * MaxMarks3);

                                        double ObtMarks1 = (double.Parse(ConvinBoard.ToString("0")) + double.Parse(Prac.ToString("0")));
                                        totalM1 = totalM1 + ObtMarks1;
                                        totalMM1 = totalMM1 + 100;
                                        
                                        Label lblIA = (Label)rptmarksT1.Items[m].FindControl("lblIA");
                                        Label lblHYMM = (Label)rptmarksT1.Items[m].FindControl("lblHYMM");
                                        Label lblHY = (Label)rptmarksT1.Items[m].FindControl("lblHY");
                                        Label lblConvBoardMM = (Label)rptmarksT1.Items[m].FindControl("lblConvBoardMM");
                                        Label lblConvBoard = (Label)rptmarksT1.Items[m].FindControl("lblConvBoard");
                                        Label lblPracMM = (Label)rptmarksT1.Items[m].FindControl("lblPracMM");
                                        Label lblPrac = (Label)rptmarksT1.Items[m].FindControl("lblPrac");
                                        Label lblTotal = (Label)rptmarksT1.Items[m].FindControl("lblTotal");
                                        Label lblGrade = (Label)rptmarksT1.Items[m].FindControl("lblGrade");

                                        lblIA.Text =(IA1==0?"NP": IA1.ToString("0"));
                                        lblHYMM.Text= MaxMarks3.ToString("0");
                                        lblHY.Text = (SAT == 0 ? "NP" : SAT.ToString("0"));
                                        lblConvBoardMM.Text = (((10 + MaxMarks3) / (10 + MaxMarks3)) * MaxMarks3).ToString("0");
                                        lblConvBoard.Text = ConvinBoard.ToString("0");
                                        lblPracMM.Text = MaxMarks4.ToString("0");
                                        lblPrac.Text = (Prac == 0 ? "NP" : Prac.ToString("0"));

                                        lblTotal.Text = ObtMarks1.ToString("0");
                                        string grade1 = grade(double.Parse(ObtMarks1.ToString("0")));
                                        lblGrade.Text = grade1;
                                        string ss = ds.Tables[0].Rows[m]["SubjectId_1"].ToString();
                                        string hi = "select isnull(TRY_CONVERT(decimal(10,0),max(T1.highestMarks)),0) highestMarks  from (";
                                        hi = hi + " select sum((((TRY_CONVERT(decimal(10,0),(case when isnull(TRY_CONVERT(decimal(10,0),m.Test1),0)>isnull(TRY_CONVERT(decimal(10,0),m.Test2),0)  ";
                                        hi = hi + " then(isnull(TRY_CONVERT(decimal(10, 0), m.Test1), 0) * 10) / isnull(TRY_CONVERT(decimal(10, 0), mm.MaxMarks1), 1)  else  (isnull(TRY_CONVERT(decimal(10, 0), m.Test2), 0) * 10) / isnull(TRY_CONVERT(decimal(10, 0), mm.MaxMarks2), 1) end)) ";
                                        hi = hi + " +isnull(TRY_CONVERT(decimal(10, 0), m.SAT), 0))  / (10 +isnull(TRY_CONVERT(decimal(10, 0), MaxMarks3), 1))) ";
                                        hi = hi + " *isnull(TRY_CONVERT(decimal(10, 0), MaxMarks3), 1)) +isnull(TRY_CONVERT(decimal(10, 0), m.Prac), 0)) highestMarks ";
                                        hi = hi + " from CCEXI_1718 m inner join TTSubjectMaster sm on sm.Id=m.SubjectId and sm.Classid=m.ClassId and sm.SessionName=m.SessionName and sm.BranchCode=m.BranchCode inner join SetMaxMinMarks_XI mm on mm.SubjectActivityId=m.SubjectId and m.PaperId=mm.PaperId and m.SessionName=mm.SessionName and m.BranchCode=mm.BranchCode and m.Evaluation=mm.Eval where m.ClassId=" + drpclass.SelectedValue + " and m.SectionName='" + drpsection.SelectedItem.Text + "' and m.Evaluation='TERM1' and m.SubjectId=" + ds.Tables[0].Rows[m]["SubjectId_1"].ToString() + "  ";
                                        hi = hi + " and m.BranchCode=" + Session["BranchCode"] + " and m.SessionName='" + Session["SessionName"] + "' ";
                                        hi = hi + " and SrNo in (select SrNo from AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") stu ";
                                        hi = hi + " where stu.sessionName='" + Session["SessionName"] + "' and isnull(stu.Promotion, '')<>'Cancelled' ";
                                        hi = hi + " and stu.ClassId=" + drpclass.SelectedValue + " and stu.SectionName='" + drpsection.SelectedItem.Text + "'  and BranchId=" + drpBranch.SelectedValue + " ";
                                        hi = hi + " and isnull(Withdrwal,'') = '' ";
                                        hi = hi + " and isnull(blocked,'') = '') ";
                                        hi = hi + " group by m.SrNo)T1 ";
                                        double highestMarks = double.Parse((_oo.ReturnTag(hi, "highestMarks").ToString() == "" ? "0" : _oo.ReturnTag(hi, "highestMarks")));

                                        string avg = "select isnull(TRY_CONVERT(decimal(10,0),T1.avgMarks),0) avgMarks from ( ";
                                        avg = avg + " select sum((((TRY_CONVERT(decimal(10,0),(case when isnull(TRY_CONVERT(decimal(10,0),m.Test1),0)>isnull(TRY_CONVERT(decimal(10,0),m.Test2),0)  ";
                                        avg = avg + " then(isnull(TRY_CONVERT(decimal(10, 0), m.Test1), 0) * 10) / isnull(TRY_CONVERT(decimal(10, 0), mm.MaxMarks1), 1)  else  (isnull(TRY_CONVERT(decimal(10, 0), m.Test2), 0) * 10) / isnull(TRY_CONVERT(decimal(10, 0), mm.MaxMarks2), 1) end)) ";
                                        avg = avg + " +isnull(TRY_CONVERT(decimal(10, 0), m.SAT), 0))  / (10 +isnull(TRY_CONVERT(decimal(10, 0), MaxMarks3), 1))) ";
                                        avg = avg + " *isnull(TRY_CONVERT(decimal(10, 0), MaxMarks3), 1)) +isnull(TRY_CONVERT(decimal(10, 0), m.Prac), 0)) avgMarks ";
                                        avg = avg + " from CCEXI_1718 m inner join TTSubjectMaster sm on sm.Id=m.SubjectId and sm.Classid=m.ClassId and sm.SessionName=m.SessionName and sm.BranchCode=m.BranchCode inner join SetMaxMinMarks_XI mm on mm.SubjectActivityId=m.SubjectId and m.PaperId=mm.PaperId and m.SessionName=mm.SessionName and m.BranchCode=mm.BranchCode and m.Evaluation=mm.Eval ";
                                        avg = avg + " where m.ClassId=" + drpclass.SelectedValue + " and m.SectionName='" + drpsection.SelectedItem.Text + "' and m.Evaluation='TERM1' and m.SubjectId=" + ds.Tables[0].Rows[m]["SubjectId_1"].ToString() + "  ";
                                        avg = avg + " and m.BranchCode=" + Session["BranchCode"] + " and m.SessionName='" + Session["SessionName"] + "' ";
                                        avg = avg + ")T1 ";

                                        string totalStu = "select count(*) totalStu ";
                                        totalStu = totalStu + " from CCEXI_1718 m inner join SetMaxMinMarks_XI mm on mm.SubjectActivityId=m.SubjectId and m.PaperId=mm.PaperId and m.SessionName=mm.SessionName and m.BranchCode=mm.BranchCode and m.Evaluation=mm.Eval ";
                                        totalStu = totalStu + " where m.ClassId=" + drpclass.SelectedValue + " and m.SectionName='" + drpsection.SelectedItem.Text + "' and m.Evaluation='TERM1' and m.SubjectId=" + ds.Tables[0].Rows[m]["SubjectId_1"].ToString() + "  ";
                                        totalStu = totalStu + " and m.BranchCode=" + Session["BranchCode"] + " and m.SessionName='" + Session["SessionName"] + "' ";
                                        totalStu = totalStu + " and SrNo in (select SrNo from AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") stu ";
                                        totalStu = totalStu + " where stu.sessionName='" + Session["SessionName"] + "' and isnull(stu.Promotion, '')<>'Cancelled' ";
                                        totalStu = totalStu + " and stu.ClassId=" + drpclass.SelectedValue + " and stu.SectionName='" + drpsection.SelectedItem.Text + "'  and BranchId=" + drpBranch.SelectedValue + " ";
                                        totalStu = totalStu + " and isnull(Withdrwal,'') = '' ";
                                        totalStu = totalStu + " and isnull(blocked,'') = '') ";

                                        double avgMarks = double.Parse((_oo.ReturnTag(avg, "avgMarks").ToString() == "" ? "0" : _oo.ReturnTag(avg, "avgMarks")));
                                        string sname = ds.Tables[0].Rows[m]["ShortCode"].ToString();

                                        string optStu = "select count(*) optStu from ICSEOptionalSubjectAllotment where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "  and OptSubjectId=" + ds.Tables[0].Rows[m]["SubjectId_1"].ToString() + " and Srno in (select Srno from StudentOfficialDetails where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and SectionId=" + drpsection.SelectedValue + " and Branch=" + drpBranch.SelectedValue + " and isnull(Withdrwal,'')='')";
                                        int optCount = int.Parse(_oo.ReturnTag(optStu, "optStu"));
                                        if (optCount == 0)
                                        {
                                            optCount = int.Parse(_oo.ReturnTag(totalStu, "totalStu").ToString());
                                        }
                                        dts.Rows.Add(sname, highestMarks.ToString("0"), ObtMarks1.ToString("0"), (avgMarks / optCount).ToString("0"));
                                    }
                                    if (ds.Tables[1].Rows.Count > 0)
                                    {
                                        Repeater rptmarksT1Additional = (Repeater)rptStudent.Items[i].FindControl("rptmarksT1Additional");
                                        rptmarksT1Additional.DataSource = ds.Tables[1];
                                        rptmarksT1Additional.DataBind();
                                        for (int m = 0; m < rptmarksT1Additional.Items.Count; m++)
                                        {
                                            double MaxMarks1 = 0; double MaxMarks2 = 0; double MaxMarks3 = 0; double MaxMarks4 = 0; double Test1 = 0; double Test2 = 0; double Prac = 0; double SAT = 0;
                                            if (ds.Tables[1].Rows[m]["MaxMarks1_1"].ToString().Trim().ToUpper() == "NP" || ds.Tables[1].Rows[m]["MaxMarks1_1"].ToString().Trim().ToUpper() == "NAD" || ds.Tables[1].Rows[m]["MaxMarks1_1"].ToString().Trim().ToUpper() == "ML" || ds.Tables[1].Rows[m]["MaxMarks1_1"].ToString().Trim().ToUpper() == "" || ds.Tables[1].Rows[m]["MaxMarks1_1"].ToString().Trim().ToUpper() == "AB" || ds.Tables[1].Rows[m]["MaxMarks1_1"].ToString().Trim().ToUpper() == "NA" || ds.Tables[1].Rows[m]["MaxMarks1_1"].ToString().Trim().ToUpper() == "M.L") { }
                                            else { MaxMarks1 = double.Parse(ds.Tables[1].Rows[m]["MaxMarks1_1"].ToString().Trim()); }
                                            if (ds.Tables[1].Rows[m]["MaxMarks2_1"].ToString().Trim().ToUpper() == "NP" || ds.Tables[1].Rows[m]["MaxMarks2_1"].ToString().Trim().ToUpper() == "NAD" || ds.Tables[1].Rows[m]["MaxMarks2_1"].ToString().Trim().ToUpper() == "ML" || ds.Tables[1].Rows[m]["MaxMarks2_1"].ToString().Trim().ToUpper() == "" || ds.Tables[1].Rows[m]["MaxMarks2_1"].ToString().Trim().ToUpper() == "AB" || ds.Tables[1].Rows[m]["MaxMarks2_1"].ToString().Trim().ToUpper() == "NA" || ds.Tables[1].Rows[m]["MaxMarks2_1"].ToString().Trim().ToUpper() == "M.L") { }
                                            else { MaxMarks2 = double.Parse(ds.Tables[1].Rows[m]["MaxMarks2_1"].ToString().Trim()); }
                                            if (ds.Tables[1].Rows[m]["MaxMarks3_1"].ToString().Trim().ToUpper() == "NP" || ds.Tables[1].Rows[m]["MaxMarks3_1"].ToString().Trim().ToUpper() == "NAD" || ds.Tables[1].Rows[m]["MaxMarks3_1"].ToString().Trim().ToUpper() == "ML" || ds.Tables[1].Rows[m]["MaxMarks3_1"].ToString().Trim().ToUpper() == "" || ds.Tables[1].Rows[m]["MaxMarks3_1"].ToString().Trim().ToUpper() == "AB" || ds.Tables[1].Rows[m]["MaxMarks3_1"].ToString().Trim().ToUpper() == "NA" || ds.Tables[1].Rows[m]["MaxMarks3_1"].ToString().Trim().ToUpper() == "M.L") { }
                                            else { MaxMarks3 = double.Parse(ds.Tables[1].Rows[m]["MaxMarks3_1"].ToString().Trim()); }
                                            if (ds.Tables[1].Rows[m]["MaxMarks4_1"].ToString().Trim().ToUpper() == "NP" || ds.Tables[1].Rows[m]["MaxMarks4_1"].ToString().Trim().ToUpper() == "NAD" || ds.Tables[1].Rows[m]["MaxMarks4_1"].ToString().Trim().ToUpper() == "ML" || ds.Tables[1].Rows[m]["MaxMarks4_1"].ToString().Trim().ToUpper() == "" || ds.Tables[1].Rows[m]["MaxMarks4_1"].ToString().Trim().ToUpper() == "AB" || ds.Tables[1].Rows[m]["MaxMarks4_1"].ToString().Trim().ToUpper() == "NA" || ds.Tables[1].Rows[m]["MaxMarks4_1"].ToString().Trim().ToUpper() == "M.L") { }
                                            else { MaxMarks4 = double.Parse(ds.Tables[1].Rows[m]["MaxMarks4_1"].ToString().Trim()); }
                                            if (ds.Tables[1].Rows[m]["Test1_1"].ToString().Trim().ToUpper() == "NP" || ds.Tables[1].Rows[m]["Test1_1"].ToString().Trim().ToUpper() == "NAD" || ds.Tables[1].Rows[m]["Test1_1"].ToString().Trim().ToUpper() == "ML" || ds.Tables[1].Rows[m]["Test1_1"].ToString().Trim().ToUpper() == "" || ds.Tables[1].Rows[m]["Test1_1"].ToString().Trim().ToUpper() == "AB" || ds.Tables[1].Rows[m]["Test1_1"].ToString().Trim().ToUpper() == "NA" || ds.Tables[1].Rows[m]["Test1_1"].ToString().Trim().ToUpper() == "M.L") { }
                                            else { Test1 = double.Parse(ds.Tables[1].Rows[m]["Test1_1"].ToString().Trim()); }
                                            if (ds.Tables[1].Rows[m]["Test2_1"].ToString().Trim().ToUpper() == "NP" || ds.Tables[1].Rows[m]["Test2_1"].ToString().Trim().ToUpper() == "NAD" || ds.Tables[1].Rows[m]["Test2_1"].ToString().Trim().ToUpper() == "ML" || ds.Tables[1].Rows[m]["Test2_1"].ToString().Trim().ToUpper() == "" || ds.Tables[1].Rows[m]["Test2_1"].ToString().Trim().ToUpper() == "AB" || ds.Tables[1].Rows[m]["Test2_1"].ToString().Trim().ToUpper() == "NA" || ds.Tables[1].Rows[m]["Test2_1"].ToString().Trim().ToUpper() == "M.L") { }
                                            else { Test2 = double.Parse(ds.Tables[1].Rows[m]["Test2_1"].ToString().Trim()); }
                                            if (ds.Tables[1].Rows[m]["Prac_1"].ToString().Trim().ToUpper() == "NP" || ds.Tables[1].Rows[m]["Prac_1"].ToString().Trim().ToUpper() == "NAD" || ds.Tables[1].Rows[m]["Prac_1"].ToString().Trim().ToUpper() == "ML" || ds.Tables[1].Rows[m]["Prac_1"].ToString().Trim().ToUpper() == "" || ds.Tables[1].Rows[m]["Prac_1"].ToString().Trim().ToUpper() == "AB" || ds.Tables[1].Rows[m]["Prac_1"].ToString().Trim().ToUpper() == "NA" || ds.Tables[1].Rows[m]["Prac_1"].ToString().Trim().ToUpper() == "M.L") { }
                                            else { Prac = double.Parse(ds.Tables[1].Rows[m]["Prac_1"].ToString().Trim()); }
                                            if (ds.Tables[1].Rows[m]["SAT_1"].ToString().Trim().ToUpper() == "NP" || ds.Tables[1].Rows[m]["SAT_1"].ToString().Trim().ToUpper() == "NAD" || ds.Tables[1].Rows[m]["SAT_1"].ToString().Trim().ToUpper() == "ML" || ds.Tables[1].Rows[m]["SAT_1"].ToString().Trim().ToUpper() == "" || ds.Tables[1].Rows[m]["SAT_1"].ToString().Trim().ToUpper() == "AB" || ds.Tables[1].Rows[m]["SAT_1"].ToString().Trim().ToUpper() == "NA" || ds.Tables[1].Rows[m]["SAT_1"].ToString().Trim().ToUpper() == "M.L") { }
                                            else { SAT = double.Parse(ds.Tables[1].Rows[m]["SAT_1"].ToString().Trim()); }
                                            double totalmarks1 = 0; double IA1 = 0;
                                            totalmarks1 = (MaxMarks1 > 0 ? (double.Parse(Test1.ToString("0")) * 20) / MaxMarks1 : 0) > (MaxMarks2 > 0 ? (double.Parse(Test2.ToString("0")) * 20) / MaxMarks2 : 0) ? (MaxMarks1 > 0 ? (double.Parse(Test1.ToString("0")) * 20) / MaxMarks1 : 0) : (MaxMarks2 > 0 ? (double.Parse(Test2.ToString("0")) * 20) / MaxMarks2 : 0);
                                            IA1 = ((totalmarks1 * 10) / 20);
                                            double ConvinBoard = (((double.Parse(IA1.ToString("0")) + double.Parse(SAT.ToString("0"))) / (10 + MaxMarks4)) * MaxMarks4);

                                            double ObtMarks1 = (double.Parse(ConvinBoard.ToString("0")) + double.Parse(Prac.ToString("0")));
                                            totalM1 = totalM1 + ObtMarks1;
                                            totalMM1 = totalMM1 + 100;

                                            Label lblIA = (Label)rptmarksT1Additional.Items[m].FindControl("lblIA");
                                            Label lblHYMM = (Label)rptmarksT1Additional.Items[m].FindControl("lblHYMM");
                                            Label lblHY = (Label)rptmarksT1Additional.Items[m].FindControl("lblHY");
                                            Label lblConvBoardMM = (Label)rptmarksT1Additional.Items[m].FindControl("lblConvBoardMM");
                                            Label lblConvBoard = (Label)rptmarksT1Additional.Items[m].FindControl("lblConvBoard");
                                            Label lblPracMM = (Label)rptmarksT1Additional.Items[m].FindControl("lblPracMM");
                                            Label lblPrac = (Label)rptmarksT1Additional.Items[m].FindControl("lblPrac");
                                            Label lblTotal = (Label)rptmarksT1Additional.Items[m].FindControl("lblTotal");
                                            Label lblGrade = (Label)rptmarksT1Additional.Items[m].FindControl("lblGrade");

                                            lblIA.Text = (IA1 == 0 ? "NP" : IA1.ToString("0"));
                                            lblHYMM.Text = MaxMarks4.ToString("0");
                                            lblHY.Text = (SAT == 0 ? "NP" : SAT.ToString("0"));
                                            lblConvBoardMM.Text = (MaxMarks4 + 10).ToString("0");
                                            lblConvBoard.Text = ConvinBoard.ToString("0");
                                            lblPracMM.Text = MaxMarks3.ToString("0");
                                            lblPrac.Text = (Prac == 0 ? "NP" : Prac.ToString("0"));

                                            lblTotal.Text = ObtMarks1.ToString("0");
                                            string grade1 = grade(double.Parse(ObtMarks1.ToString("0")));
                                            lblGrade.Text = grade1;

                                            string hi = "select isnull(TRY_CONVERT(decimal(10,0),max(T1.highestMarks)),0) highestMarks  from (";
                                            hi = hi + " select sum((((TRY_CONVERT(decimal(10,0),(case when isnull(TRY_CONVERT(decimal(10,0),m.Test1),0)>isnull(TRY_CONVERT(decimal(10,0),m.Test2),0)  ";
                                            hi = hi + " then(isnull(TRY_CONVERT(decimal(10, 0), m.Test1), 0) * 10) / isnull(TRY_CONVERT(decimal(10, 0), mm.MaxMarks1), 1)  else  (isnull(TRY_CONVERT(decimal(10, 0), m.Test2), 0) * 10) / isnull(TRY_CONVERT(decimal(10, 0), mm.MaxMarks2), 1) end)) ";
                                            hi = hi + " +isnull(TRY_CONVERT(decimal(10, 0), m.SAT), 0))  / (10 +isnull(TRY_CONVERT(decimal(10, 0), MaxMarks3), 1))) ";
                                            hi = hi + " *isnull(TRY_CONVERT(decimal(10, 0), MaxMarks3), 1)) +isnull(TRY_CONVERT(decimal(10, 0), m.Prac), 0)) highestMarks, m.SrNo ";
                                            hi = hi + " from CCEXI_1718 m inner join TTSubjectMaster sm on sm.Id=m.SubjectId and sm.Classid=m.ClassId and sm.SessionName=m.SessionName and sm.BranchCode=m.BranchCode inner join SetMaxMinMarks_XI mm on mm.SubjectActivityId=m.SubjectId and m.PaperId=mm.PaperId and m.SessionName=mm.SessionName and m.BranchCode=mm.BranchCode and m.Evaluation=mm.Eval where m.ClassId=" + drpclass.SelectedValue + " and m.SectionName='" + drpsection.SelectedItem.Text + "' and m.Evaluation='TERM1' and m.SubjectId=" + ds.Tables[1].Rows[m]["SubjectId_1"].ToString() + "  ";
                                            hi = hi + " and m.BranchCode=" + Session["BranchCode"] + " and m.SessionName='" + Session["SessionName"] + "' ";
                                            hi = hi + " and SrNo in (select SrNo from AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") stu ";
                                            hi = hi + " where stu.sessionName='" + Session["SessionName"] + "' and isnull(stu.Promotion, '')<>'Cancelled' ";
                                            hi = hi + " and stu.ClassId=" + drpclass.SelectedValue + " and stu.SectionName='" + drpsection.SelectedItem.Text + "'  and BranchId=" + drpBranch.SelectedValue + " ";
                                            hi = hi + " and isnull(Withdrwal,'') = '' ";
                                            hi = hi + " and isnull(blocked,'') = '') group by m.SrNo)T1 ";
                                            double highestMarks = double.Parse((_oo.ReturnTag(hi, "highestMarks").ToString() == "" ? "0" : _oo.ReturnTag(hi, "highestMarks")));

                                            string avg = "select isnull(TRY_CONVERT(decimal(10,0),T1.avgMarks),0) avgMarks from ( ";
                                            avg = avg + " select sum((((TRY_CONVERT(decimal(10,0),(case when isnull(TRY_CONVERT(decimal(10,0),m.Test1),0)>isnull(TRY_CONVERT(decimal(10,0),m.Test2),0)  ";
                                            avg = avg + " then(isnull(TRY_CONVERT(decimal(10, 0), m.Test1), 0) * 10) / isnull(TRY_CONVERT(decimal(10, 0), mm.MaxMarks1), 1)  else  (isnull(TRY_CONVERT(decimal(10, 0), m.Test2), 0) * 10) / isnull(TRY_CONVERT(decimal(10, 0), mm.MaxMarks2), 1) end)) ";
                                            avg = avg + " +isnull(TRY_CONVERT(decimal(10, 0), m.SAT), 0))  / (10 +isnull(TRY_CONVERT(decimal(10, 0), MaxMarks3), 1))) ";
                                            avg = avg + " *isnull(TRY_CONVERT(decimal(10, 0), MaxMarks3), 1)) +isnull(TRY_CONVERT(decimal(10, 0), m.Prac), 0)) avgMarks ";
                                            avg = avg + " from CCEXI_1718 m inner join TTSubjectMaster sm on sm.Id=m.SubjectId and sm.Classid=m.ClassId and sm.SessionName=m.SessionName and sm.BranchCode=m.BranchCode inner join SetMaxMinMarks_XI mm on mm.SubjectActivityId=m.SubjectId and m.PaperId=mm.PaperId and m.SessionName=mm.SessionName and m.BranchCode=mm.BranchCode and m.Evaluation=mm.Eval ";
                                            avg = avg + " where m.ClassId=" + drpclass.SelectedValue + " and m.SectionName='" + drpsection.SelectedItem.Text + "' and m.Evaluation='TERM1' and m.SubjectId=" + ds.Tables[1].Rows[m]["SubjectId_1"].ToString() + "  ";
                                            avg = avg + " and m.BranchCode=" + Session["BranchCode"] + " and m.SessionName='" + Session["SessionName"] + "'  ";
                                            avg = avg + " and SrNo in (select SrNo from AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") stu ";
                                            avg = avg + " where stu.sessionName='" + Session["SessionName"] + "' and isnull(stu.Promotion, '')<>'Cancelled' ";
                                            avg = avg + " and stu.ClassId=" + drpclass.SelectedValue + " and stu.SectionName='" + drpsection.SelectedItem.Text + "'  and BranchId=" + drpBranch.SelectedValue + " ";
                                            avg = avg + " and isnull(Withdrwal,'') = '' ";
                                            avg = avg + " and isnull(blocked,'') = '') ";
                                            avg = avg + ")T1 ";

                                            string totalStu = "select count(*) totalStu ";
                                            totalStu = totalStu + " from CCEXI_1718 m inner join SetMaxMinMarks_XI mm on mm.SubjectActivityId=m.SubjectId and m.PaperId=mm.PaperId and m.SessionName=mm.SessionName and m.BranchCode=mm.BranchCode and m.Evaluation=mm.Eval ";
                                            totalStu = totalStu + " where m.ClassId=" + drpclass.SelectedValue + " and m.SectionName='" + drpsection.SelectedItem.Text + "' and m.Evaluation='TERM1' and m.SubjectId=" + ds.Tables[1].Rows[m]["SubjectId_1"].ToString() + "  ";
                                            totalStu = totalStu + " and m.BranchCode=" + Session["BranchCode"] + " and m.SessionName='" + Session["SessionName"] + "' ";
                                            totalStu = totalStu + " and SrNo in (select SrNo from AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") stu ";
                                            totalStu = totalStu + " where stu.sessionName='" + Session["SessionName"] + "' and isnull(stu.Promotion, '')<>'Cancelled' ";
                                            totalStu = totalStu + " and stu.ClassId=" + drpclass.SelectedValue + " and stu.SectionName='" + drpsection.SelectedItem.Text + "'  and BranchId=" + drpBranch.SelectedValue + " ";
                                            totalStu = totalStu + " and isnull(Withdrwal,'') = '' ";
                                            totalStu = totalStu + " and isnull(blocked,'') = '') ";
                                            double avgMarks = double.Parse((_oo.ReturnTag(avg, "avgMarks").ToString() == "" ? "0" : _oo.ReturnTag(avg, "avgMarks")));
                                            string sname = ds.Tables[1].Rows[m]["ShortCode"].ToString();
                                            string optStu = "select count(*) optStu from ICSEOptionalSubjectAllotment where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "  and OptSubjectId=" + ds.Tables[1].Rows[m]["SubjectId_1"].ToString() + " and Srno in (select Srno from StudentOfficialDetails where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and SectionId=" + drpsection.SelectedValue + " and Branch=" + drpBranch.SelectedValue + " and isnull(Withdrwal,'')='')";
                                            int optCount = int.Parse(_oo.ReturnTag(optStu, "optStu"));
                                            if (optCount == 0)
                                            {
                                                optCount = int.Parse(_oo.ReturnTag(totalStu, "totalStu").ToString());
                                            }
                                            dts.Rows.Add(sname, highestMarks.ToString("0"), ObtMarks1.ToString("0"), (avgMarks / optCount).ToString("0"));
                                        }
                                        Chart myChart = (Chart)rptStudent.Items[i].FindControl("myChart");
                                        chart(myChart, dts);
                                        Label lblHTotal = (Label)rptmarksT1Additional.Controls[rptmarksT1Additional.Controls.Count - 1].Controls[0].FindControl("lblHTotal");
                                        Label lblHPercentage = (Label)rptmarksT1Additional.Controls[rptmarksT1Additional.Controls.Count - 1].Controls[0].FindControl("lblHPercentage");
                                        Label lblHRank = (Label)rptmarksT1Additional.Controls[rptmarksT1Additional.Controls.Count - 1].Controls[0].FindControl("lblHRank");
                                        Label lblHPosition = (Label)rptmarksT1Additional.Controls[rptmarksT1Additional.Controls.Count - 1].Controls[0].FindControl("lblHPosition");
                                        lblHTotal.Text = double.Parse(ds.Tables[7].Rows[0]["term1Total"].ToString()).ToString("0").Trim() + "/600";
                                        lblHPercentage.Text = ((double.Parse(double.Parse(ds.Tables[7].Rows[0]["term1Total"].ToString()).ToString("0")) * 100) / 600).ToString("0.00") + " %";
                                        lblHRank.Text = (dsRank.Tables[0].Rows.Count > 0 ? dsRank.Tables[0].Rows[0]["ranks"].ToString() : "0");
                                        lblHPosition.Text = (dsRank.Tables[1].Rows.Count > 0 ? dsRank.Tables[1].Rows[0]["position"].ToString().Trim() : "-");
                                        HtmlTableRow tr1 = (HtmlTableRow)rptmarksT1.Controls[rptmarksT1.Controls.Count - 1].Controls[0].FindControl("tr1");
                                        HtmlTableRow tr2 = (HtmlTableRow)rptmarksT1.Controls[rptmarksT1.Controls.Count - 1].Controls[0].FindControl("tr2");
                                        HtmlTableRow tr3 = (HtmlTableRow)rptmarksT1.Controls[rptmarksT1.Controls.Count - 1].Controls[0].FindControl("tr3");
                                        HtmlTableRow tr4 = (HtmlTableRow)rptmarksT1.Controls[rptmarksT1.Controls.Count - 1].Controls[0].FindControl("tr4");
                                        tr1.Visible = false;
                                        tr2.Visible = false;
                                        tr3.Visible = false;
                                        tr4.Visible = false;
                                    }
                                    else
                                    {
                                        
                                        Chart myChart = (Chart)rptStudent.Items[i].FindControl("myChart");
                                        chart(myChart, dts);
                                        Label lblHTotal = (Label)rptmarksT1.Controls[rptmarksT1.Controls.Count - 1].Controls[0].FindControl("lblHTotal");
                                        Label lblHPercentage = (Label)rptmarksT1.Controls[rptmarksT1.Controls.Count - 1].Controls[0].FindControl("lblHPercentage");
                                        Label lblHRank = (Label)rptmarksT1.Controls[rptmarksT1.Controls.Count - 1].Controls[0].FindControl("lblHRank");
                                        Label lblHPosition = (Label)rptmarksT1.Controls[rptmarksT1.Controls.Count - 1].Controls[0].FindControl("lblHPosition");
                                        lblHTotal.Text = double.Parse(ds.Tables[7].Rows[0]["term1Total"].ToString()).ToString("0").Trim() + "/600";
                                        lblHPercentage.Text = ((double.Parse(double.Parse(ds.Tables[7].Rows[0]["term1Total"].ToString()).ToString("0")) * 100) / 600).ToString("0.00") + " %";
                                        lblHRank.Text = (dsRank.Tables[0].Rows.Count > 0 ? dsRank.Tables[0].Rows[0]["ranks"].ToString() : "0");
                                        lblHPosition.Text = (dsRank.Tables[1].Rows.Count > 0 ? dsRank.Tables[1].Rows[0]["position"].ToString().Trim() : "-");
                                    }
                                }
                                if (drpEval.SelectedValue.ToUpper() == "TERM2")
                                {
                                    DataTable dts = new DataTable();
                                    dts.Columns.AddRange(new DataColumn[] {
                                    new DataColumn("SubjectName",typeof(string)),
                                    new DataColumn("Highest Marks",typeof(int)),
                                    new DataColumn("Obtained Marks",typeof(int)),
                                    new DataColumn("Avg. Marks", typeof(int))});
                                    Repeater rptmarksT2 = (Repeater)rptStudent.Items[i].FindControl("rptmarksT2");
                                    rptmarksT2.DataSource = ds.Tables[0];
                                    rptmarksT2.DataBind();
                                    for (int p = 0; p < ds.Tables[0].Rows.Count; p++)
                                    {
                                        double MaxMarks1 = 0; double MaxMarks2 = 0; double MaxMarks3 = 0; double MaxMarks4 = 0; double Test1 = 0; double Test2 = 0; double Prac = 0; double SAT = 0;
                                        if (ds.Tables[0].Rows[p]["MaxMarks1_1"].ToString().Trim().ToUpper() == "NP" || ds.Tables[0].Rows[p]["MaxMarks1_1"].ToString().Trim().ToUpper() == "NAD" || ds.Tables[0].Rows[p]["MaxMarks1_1"].ToString().Trim().ToUpper() == "ML" || ds.Tables[0].Rows[p]["MaxMarks1_1"].ToString().Trim().ToUpper() == "" || ds.Tables[0].Rows[p]["MaxMarks1_1"].ToString().Trim().ToUpper() == "AB" || ds.Tables[0].Rows[p]["MaxMarks1_1"].ToString().Trim().ToUpper() == "NA" || ds.Tables[0].Rows[p]["MaxMarks1_1"].ToString().Trim().ToUpper() == "M.L") { }
                                        else { MaxMarks1 = double.Parse(ds.Tables[0].Rows[p]["MaxMarks1_1"].ToString().Trim()); }
                                        if (ds.Tables[0].Rows[p]["MaxMarks2_1"].ToString().Trim().ToUpper() == "NP" || ds.Tables[0].Rows[p]["MaxMarks2_1"].ToString().Trim().ToUpper() == "NAD" || ds.Tables[0].Rows[p]["MaxMarks2_1"].ToString().Trim().ToUpper() == "ML" || ds.Tables[0].Rows[p]["MaxMarks2_1"].ToString().Trim().ToUpper() == "" || ds.Tables[0].Rows[p]["MaxMarks2_1"].ToString().Trim().ToUpper() == "AB" || ds.Tables[0].Rows[p]["MaxMarks2_1"].ToString().Trim().ToUpper() == "NA" || ds.Tables[0].Rows[p]["MaxMarks2_1"].ToString().Trim().ToUpper() == "M.L") { }
                                        else { MaxMarks2 = double.Parse(ds.Tables[0].Rows[p]["MaxMarks2_1"].ToString().Trim()); }
                                        if (ds.Tables[0].Rows[p]["MaxMarks3_1"].ToString().Trim().ToUpper() == "NP" || ds.Tables[0].Rows[p]["MaxMarks3_1"].ToString().Trim().ToUpper() == "NAD" || ds.Tables[0].Rows[p]["MaxMarks3_1"].ToString().Trim().ToUpper() == "ML" || ds.Tables[0].Rows[p]["MaxMarks3_1"].ToString().Trim().ToUpper() == "" || ds.Tables[0].Rows[p]["MaxMarks3_1"].ToString().Trim().ToUpper() == "AB" || ds.Tables[0].Rows[p]["MaxMarks3_1"].ToString().Trim().ToUpper() == "NA" || ds.Tables[0].Rows[p]["MaxMarks3_1"].ToString().Trim().ToUpper() == "M.L") { }
                                        else { MaxMarks3 = double.Parse(ds.Tables[0].Rows[p]["MaxMarks3_1"].ToString().Trim()); }
                                        if (ds.Tables[0].Rows[p]["MaxMarks4_1"].ToString().Trim().ToUpper() == "NP" || ds.Tables[0].Rows[p]["MaxMarks4_1"].ToString().Trim().ToUpper() == "NAD" || ds.Tables[0].Rows[p]["MaxMarks4_1"].ToString().Trim().ToUpper() == "ML" || ds.Tables[0].Rows[p]["MaxMarks4_1"].ToString().Trim().ToUpper() == "" || ds.Tables[0].Rows[p]["MaxMarks4_1"].ToString().Trim().ToUpper() == "AB" || ds.Tables[0].Rows[p]["MaxMarks4_1"].ToString().Trim().ToUpper() == "NA" || ds.Tables[0].Rows[p]["MaxMarks4_1"].ToString().Trim().ToUpper() == "M.L") { }
                                        else { MaxMarks4 = double.Parse(ds.Tables[0].Rows[p]["MaxMarks4_1"].ToString().Trim()); }
                                        if (ds.Tables[0].Rows[p]["Test1_1"].ToString().Trim().ToUpper() == "NP" || ds.Tables[0].Rows[p]["Test1_1"].ToString().Trim().ToUpper() == "NAD" || ds.Tables[0].Rows[p]["Test1_1"].ToString().Trim().ToUpper() == "ML" || ds.Tables[0].Rows[p]["Test1_1"].ToString().Trim().ToUpper() == "" || ds.Tables[0].Rows[p]["Test1_1"].ToString().Trim().ToUpper() == "AB" || ds.Tables[0].Rows[p]["Test1_1"].ToString().Trim().ToUpper() == "NA" || ds.Tables[0].Rows[p]["Test1_1"].ToString().Trim().ToUpper() == "M.L") { }
                                        else { Test1 = double.Parse(ds.Tables[0].Rows[p]["Test1_1"].ToString().Trim()); }
                                        if (ds.Tables[0].Rows[p]["Test2_1"].ToString().Trim().ToUpper() == "NP" || ds.Tables[0].Rows[p]["Test2_1"].ToString().Trim().ToUpper() == "NAD" || ds.Tables[0].Rows[p]["Test2_1"].ToString().Trim().ToUpper() == "ML" || ds.Tables[0].Rows[p]["Test2_1"].ToString().Trim().ToUpper() == "" || ds.Tables[0].Rows[p]["Test2_1"].ToString().Trim().ToUpper() == "AB" || ds.Tables[0].Rows[p]["Test2_1"].ToString().Trim().ToUpper() == "NA" || ds.Tables[0].Rows[p]["Test2_1"].ToString().Trim().ToUpper() == "M.L") { }
                                        else { Test2 = double.Parse(ds.Tables[0].Rows[p]["Test2_1"].ToString().Trim()); }
                                        if (ds.Tables[0].Rows[p]["Prac_1"].ToString().Trim().ToUpper() == "NP" || ds.Tables[0].Rows[p]["Prac_1"].ToString().Trim().ToUpper() == "NAD" || ds.Tables[0].Rows[p]["Prac_1"].ToString().Trim().ToUpper() == "ML" || ds.Tables[0].Rows[p]["Prac_1"].ToString().Trim().ToUpper() == "" || ds.Tables[0].Rows[p]["Prac_1"].ToString().Trim().ToUpper() == "AB" || ds.Tables[0].Rows[p]["Prac_1"].ToString().Trim().ToUpper() == "NA" || ds.Tables[0].Rows[p]["Prac_1"].ToString().Trim().ToUpper() == "M.L") { }
                                        else { Prac = double.Parse(ds.Tables[0].Rows[p]["Prac_1"].ToString().Trim()); }
                                        if (ds.Tables[0].Rows[p]["SAT_1"].ToString().Trim().ToUpper() == "NP" || ds.Tables[0].Rows[p]["SAT_1"].ToString().Trim().ToUpper() == "NAD" || ds.Tables[0].Rows[p]["SAT_1"].ToString().Trim().ToUpper() == "ML" || ds.Tables[0].Rows[p]["SAT_1"].ToString().Trim().ToUpper() == "" || ds.Tables[0].Rows[p]["SAT_1"].ToString().Trim().ToUpper() == "AB" || ds.Tables[0].Rows[p]["SAT_1"].ToString().Trim().ToUpper() == "NA" || ds.Tables[0].Rows[p]["SAT_1"].ToString().Trim().ToUpper() == "M.L") { }
                                        else { SAT = double.Parse(ds.Tables[0].Rows[p]["SAT_1"].ToString().Trim()); }
                                        double totalmarks1 = 0; double IA1 = 0;
                                        totalmarks1 = (MaxMarks1 > 0 ? (Test1 * 20) / MaxMarks1 : 0) > (MaxMarks2 > 0 ? (Test2 * 20) / MaxMarks2 : 0) ? (MaxMarks1 > 0 ? (Test1 * 20) / MaxMarks1 : 0) : (MaxMarks2 > 0 ? (Test2 * 20) / MaxMarks2 : 0);
                                        IA1 = ((totalmarks1 * 10) / 20);
                                        double ConvinBoard = (((double.Parse(IA1.ToString("0")) + double.Parse(SAT.ToString("0"))) / (10 + MaxMarks3)) * MaxMarks3);

                                        double ObtMarks1 = (double.Parse(ConvinBoard.ToString("0")) + double.Parse(Prac.ToString("0")));
                                        totalM1 = totalM1 + ObtMarks1;
                                        totalMM1 = totalMM1 + 100;

                                        Label lblIA = (Label)rptmarksT2.Items[p].FindControl("lblIA");
                                        Label lblHYMM = (Label)rptmarksT2.Items[p].FindControl("lblHYMM");
                                        Label lblHY = (Label)rptmarksT2.Items[p].FindControl("lblHY");
                                        Label lblConvBoardMM = (Label)rptmarksT2.Items[p].FindControl("lblConvBoardMM");
                                        Label lblConvBoard = (Label)rptmarksT2.Items[p].FindControl("lblConvBoard");
                                        Label lblPracMM = (Label)rptmarksT2.Items[p].FindControl("lblPracMM");
                                        Label lblPrac = (Label)rptmarksT2.Items[p].FindControl("lblPrac");
                                        Label lblTotal = (Label)rptmarksT2.Items[p].FindControl("lblTotal");
                                        Label lblGrade = (Label)rptmarksT2.Items[p].FindControl("lblGrade");

                                        lblIA.Text = (IA1 == 0 ? "NP" : IA1.ToString("0"));
                                        lblHYMM.Text = MaxMarks3.ToString("0");
                                        lblHY.Text = (SAT == 0 ? "NP" : SAT.ToString("0"));
                                        lblConvBoardMM.Text = (((10 + MaxMarks3) / (10 + MaxMarks3)) * MaxMarks3).ToString("0");
                                        lblConvBoard.Text = ConvinBoard.ToString("0");
                                        lblPracMM.Text = MaxMarks4.ToString("0");
                                        lblPrac.Text = (Prac == 0 ? "NP" : Prac.ToString("0"));

                                        lblTotal.Text = ObtMarks1.ToString("0");
                                        string grade1 = grade(double.Parse(ObtMarks1.ToString("0")));
                                        lblGrade.Text = grade1;

                                        double MaxMarks1_2 = 0; double MaxMarks2_2 = 0; double MaxMarks3_2 = 0; double MaxMarks4_2 = 0; double Test1_2 = 0; double Test2_2 = 0; double Prac_2 = 0; double SAT_2 = 0;
                                        if (ds.Tables[0].Rows[p]["MaxMarks1_2"].ToString().Trim().ToUpper() == "NP" || ds.Tables[0].Rows[p]["MaxMarks1_2"].ToString().Trim().ToUpper() == "NAD" || ds.Tables[0].Rows[p]["MaxMarks1_2"].ToString().Trim().ToUpper() == "ML" || ds.Tables[0].Rows[p]["MaxMarks1_2"].ToString().Trim().ToUpper() == "" || ds.Tables[0].Rows[p]["MaxMarks1_2"].ToString().Trim().ToUpper() == "AB" || ds.Tables[0].Rows[p]["MaxMarks1_2"].ToString().Trim().ToUpper() == "NA" || ds.Tables[0].Rows[p]["MaxMarks1_2"].ToString().Trim().ToUpper() == "M.L") { }
                                        else { MaxMarks1_2 = double.Parse(ds.Tables[0].Rows[p]["MaxMarks1_2"].ToString().Trim()); }
                                        if (ds.Tables[0].Rows[p]["MaxMarks2_2"].ToString().Trim().ToUpper() == "NP" || ds.Tables[0].Rows[p]["MaxMarks2_2"].ToString().Trim().ToUpper() == "NAD" || ds.Tables[0].Rows[p]["MaxMarks2_2"].ToString().Trim().ToUpper() == "ML" || ds.Tables[0].Rows[p]["MaxMarks2_2"].ToString().Trim().ToUpper() == "" || ds.Tables[0].Rows[p]["MaxMarks2_2"].ToString().Trim().ToUpper() == "AB" || ds.Tables[0].Rows[p]["MaxMarks2_2"].ToString().Trim().ToUpper() == "NA" || ds.Tables[0].Rows[p]["MaxMarks2_2"].ToString().Trim().ToUpper() == "M.L") { }
                                        else { MaxMarks2_2 = double.Parse(ds.Tables[0].Rows[p]["MaxMarks2_2"].ToString().Trim()); }
                                        if (ds.Tables[0].Rows[p]["MaxMarks3_2"].ToString().Trim().ToUpper() == "NP" || ds.Tables[0].Rows[p]["MaxMarks3_2"].ToString().Trim().ToUpper() == "NAD" || ds.Tables[0].Rows[p]["MaxMarks3_2"].ToString().Trim().ToUpper() == "ML" || ds.Tables[0].Rows[p]["MaxMarks3_2"].ToString().Trim().ToUpper() == "" || ds.Tables[0].Rows[p]["MaxMarks3_2"].ToString().Trim().ToUpper() == "AB" || ds.Tables[0].Rows[p]["MaxMarks3_2"].ToString().Trim().ToUpper() == "NA" || ds.Tables[0].Rows[p]["MaxMarks3_2"].ToString().Trim().ToUpper() == "M.L") { }
                                        else { MaxMarks3_2 = double.Parse(ds.Tables[0].Rows[p]["MaxMarks3_2"].ToString().Trim()); }
                                        if (ds.Tables[0].Rows[p]["MaxMarks4_2"].ToString().Trim().ToUpper() == "NP" || ds.Tables[0].Rows[p]["MaxMarks4_2"].ToString().Trim().ToUpper() == "NAD" || ds.Tables[0].Rows[p]["MaxMarks4_2"].ToString().Trim().ToUpper() == "ML" || ds.Tables[0].Rows[p]["MaxMarks4_2"].ToString().Trim().ToUpper() == "" || ds.Tables[0].Rows[p]["MaxMarks4_2"].ToString().Trim().ToUpper() == "AB" || ds.Tables[0].Rows[p]["MaxMarks4_2"].ToString().Trim().ToUpper() == "NA" || ds.Tables[0].Rows[p]["MaxMarks4_2"].ToString().Trim().ToUpper() == "M.L") { }
                                        else { MaxMarks4_2 = double.Parse(ds.Tables[0].Rows[p]["MaxMarks4_2"].ToString().Trim()); }
                                        if (ds.Tables[0].Rows[p]["Test1_2"].ToString().Trim().ToUpper() == "NP" || ds.Tables[0].Rows[p]["Test1_2"].ToString().Trim().ToUpper() == "NAD" || ds.Tables[0].Rows[p]["Test1_2"].ToString().Trim().ToUpper() == "ML" || ds.Tables[0].Rows[p]["Test1_2"].ToString().Trim().ToUpper() == "" || ds.Tables[0].Rows[p]["Test1_2"].ToString().Trim().ToUpper() == "AB" || ds.Tables[0].Rows[p]["Test1_2"].ToString().Trim().ToUpper() == "NA" || ds.Tables[0].Rows[p]["Test1_2"].ToString().Trim().ToUpper() == "M.L") { }
                                        else { Test1_2 = double.Parse(ds.Tables[0].Rows[p]["Test1_2"].ToString().Trim()); }
                                        if (ds.Tables[0].Rows[p]["Test2_2"].ToString().Trim().ToUpper() == "NP" || ds.Tables[0].Rows[p]["Test2_2"].ToString().Trim().ToUpper() == "NAD" || ds.Tables[0].Rows[p]["Test2_2"].ToString().Trim().ToUpper() == "ML" || ds.Tables[0].Rows[p]["Test2_2"].ToString().Trim().ToUpper() == "" || ds.Tables[0].Rows[p]["Test2_2"].ToString().Trim().ToUpper() == "AB" || ds.Tables[0].Rows[p]["Test2_2"].ToString().Trim().ToUpper() == "NA" || ds.Tables[0].Rows[p]["Test2_2"].ToString().Trim().ToUpper() == "M.L") { }
                                        else { Test2_2 = double.Parse(ds.Tables[0].Rows[p]["Test2_2"].ToString().Trim()); }
                                        if (ds.Tables[0].Rows[p]["Prac_2"].ToString().Trim().ToUpper() == "NP" || ds.Tables[0].Rows[p]["Prac_2"].ToString().Trim().ToUpper() == "NAD" || ds.Tables[0].Rows[p]["Prac_2"].ToString().Trim().ToUpper() == "ML" || ds.Tables[0].Rows[p]["Prac_2"].ToString().Trim().ToUpper() == "" || ds.Tables[0].Rows[p]["Prac_2"].ToString().Trim().ToUpper() == "AB" || ds.Tables[0].Rows[p]["Prac_2"].ToString().Trim().ToUpper() == "NA" || ds.Tables[0].Rows[p]["Prac_2"].ToString().Trim().ToUpper() == "M.L") { }
                                        else { Prac_2 = double.Parse(ds.Tables[0].Rows[p]["Prac_2"].ToString().Trim()); }
                                        if (ds.Tables[0].Rows[p]["SAT_2"].ToString().Trim().ToUpper() == "NP" || ds.Tables[0].Rows[p]["SAT_2"].ToString().Trim().ToUpper() == "NAD" || ds.Tables[0].Rows[p]["SAT_2"].ToString().Trim().ToUpper() == "ML" || ds.Tables[0].Rows[p]["SAT_2"].ToString().Trim().ToUpper() == "" || ds.Tables[0].Rows[p]["SAT_2"].ToString().Trim().ToUpper() == "AB" || ds.Tables[0].Rows[p]["SAT_2"].ToString().Trim().ToUpper() == "NA" || ds.Tables[0].Rows[p]["SAT_2"].ToString().Trim().ToUpper() == "M.L") { }
                                        else { SAT_2 = double.Parse(ds.Tables[0].Rows[p]["SAT_2"].ToString().Trim()); }
                                        double totalmarks2 = 0; double IA2 = 0;
                                        totalmarks2 = (Test1_2 > Test2_2 ? Test1_2 : Test2_2);
                                        IA2 = ((totalmarks2 * 10) / 20);
                                        double ConvinBoard_2 = (((double.Parse(IA2.ToString("0")) + double.Parse(SAT_2.ToString("0"))) / (10 + MaxMarks3_2)) * MaxMarks3_2);

                                        double ObtMarks2 = (double.Parse(ConvinBoard_2.ToString("0")) + double.Parse(Prac_2.ToString("0")));
                                        totalM2 = totalM2 + ObtMarks2;
                                        totalMM2 = totalMM2 + 100;

                                        Label lblIA2 = (Label)rptmarksT2.Items[p].FindControl("lblIA2");
                                        Label lblAEMM = (Label)rptmarksT2.Items[p].FindControl("lblAEMM");
                                        Label lblAE = (Label)rptmarksT2.Items[p].FindControl("lblAE");
                                        Label lblConvBoardMM2 = (Label)rptmarksT2.Items[p].FindControl("lblConvBoardMM2");
                                        Label lblConvBoard2 = (Label)rptmarksT2.Items[p].FindControl("lblConvBoard2");
                                        Label lblPracMM2 = (Label)rptmarksT2.Items[p].FindControl("lblPracMM2");
                                        Label lblPrac2 = (Label)rptmarksT2.Items[p].FindControl("lblPrac2");
                                        Label lblTotal2 = (Label)rptmarksT2.Items[p].FindControl("lblTotal2");
                                        Label lblGrade2 = (Label)rptmarksT2.Items[p].FindControl("lblGrade2");

                                        lblIA2.Text = (IA2 == 0 ? "NP" : IA2.ToString("0"));
                                        lblAEMM.Text = MaxMarks3_2.ToString("0");
                                        lblAE.Text = (SAT_2 == 0 ? "NP" : SAT_2.ToString("0"));
                                        lblConvBoardMM2.Text = (((10 + MaxMarks3_2) / (10 + MaxMarks3_2)) * MaxMarks3_2).ToString("0");
                                        lblConvBoard2.Text = ConvinBoard_2.ToString("0");
                                        lblPracMM2.Text = MaxMarks4_2.ToString("0");
                                        lblPrac2.Text = (Prac_2 == 0 ? "NP" : Prac_2.ToString("0"));

                                        lblTotal2.Text = ObtMarks2.ToString("0");
                                        string grade2 = grade(double.Parse(ObtMarks2.ToString("0")));
                                        lblGrade2.Text = grade2;

                                        double GrandTotal = (ObtMarks1 + ObtMarks2);
                                        double GrandTotalF = (ObtMarks1 + ObtMarks2) / 2;
                                        string GradeF = grade((ObtMarks1 + ObtMarks2) / 2);

                                        Label lblGrandTotal = (Label)rptmarksT2.Items[p].FindControl("lblGrandTotal");
                                        lblGrandTotal.Text = GrandTotal.ToString("0");
                                        Label lblGrandTotalF = (Label)rptmarksT2.Items[p].FindControl("lblGrandTotalF");
                                        lblGrandTotalF.Text = GrandTotalF.ToString("0");
                                        Label lblGradeF = (Label)rptmarksT2.Items[p].FindControl("lblGradeF");
                                        lblGradeF.Text = GradeF;


                                        string mysql = "select case when ISNULL(isCompulsoryForBest5, 0)=0 then 0 else 1 end as SubjectType from TTSubjectMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and Classid="+drpclass.SelectedValue+" and BranchId="+drpBranch.SelectedValue+" and id=" + ds.Tables[0].Rows[p]["SubjectId_1"].ToString() + "";
                                        DataRow datarow=totalNum.NewRow();
                                        var sCompalsary = _oo.ReturnTag(mysql, "SubjectType");
                                        datarow["IsCompalsary"] = sCompalsary;
                                        datarow["Marks"] = GrandTotalF;
                                        totalNum.Rows.Add(datarow);


                                        string hi = "select isnull(TRY_CONVERT(decimal(10,0),max(T1.highestMarks)),0) highestMarks ";
                                        hi = hi + " from (select TRY_CONVERT(decimal(10,0),sum(TRY_CONVERT(decimal(10,0),(TRY_CONVERT(decimal(10,0),(case when (isnull(TRY_CONVERT(decimal(10,2),m.Test1),0)*10)/ isnull(TRY_CONVERT(decimal(10,2), mm.MaxMarks1), 1) ";
                                        hi = hi + " >(isnull(TRY_CONVERT(decimal(10,1),m.Test2),0)*10)/ isnull(TRY_CONVERT(decimal(10,2), mm.MaxMarks2), 1) then (isnull(TRY_CONVERT(decimal(10,2), m.Test1), 0) * 10) / isnull(TRY_CONVERT(decimal(10,2), mm.MaxMarks1), 1) ";
                                        hi = hi + " else  (isnull(TRY_CONVERT(decimal(10,2), m.Test2), 0) * 10) / isnull(TRY_CONVERT(decimal(10,2), mm.MaxMarks2), 1) end+ isnull(TRY_CONVERT(decimal(10,0), m.SAT), 0)))/(10+isnull(TRY_CONVERT(decimal(10,2), mm.MaxMarks3), 1))) ";
                                        hi = hi + " *isnull(TRY_CONVERT(decimal(10,2), mm.MaxMarks3), 1) +isnull(TRY_CONVERT(decimal(10,0), m.Prac), 0)))/2)highestMarks ";
                                        hi = hi + " from CCEXI_1718 m inner join TTSubjectMaster sm on sm.Id=m.SubjectId and sm.Classid=m.ClassId and sm.SessionName=m.SessionName and sm.BranchCode=m.BranchCode inner join SetMaxMinMarks_XI mm on mm.SubjectActivityId=m.SubjectId and m.PaperId=mm.PaperId and m.SessionName=mm.SessionName and m.BranchCode=mm.BranchCode and m.Evaluation=mm.Eval where m.ClassId=" + drpclass.SelectedValue + " and m.SectionName='" + drpsection.SelectedItem.Text + "' and m.SubjectId=" + ds.Tables[0].Rows[p]["SubjectId_1"].ToString() + "  ";
                                        hi = hi + " and m.BranchCode=" + Session["BranchCode"] + " and m.SessionName='" + Session["SessionName"] + "'  ";
                                        hi = hi + " and SrNo in (select SrNo from AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") stu ";
                                        hi = hi + " where stu.sessionName='" + Session["SessionName"] + "' and isnull(stu.Promotion, '')<>'Cancelled' ";
                                        hi = hi + " and stu.ClassId=" + drpclass.SelectedValue + " and stu.SectionName='" + drpsection.SelectedItem.Text + "'  and BranchId=" + drpBranch.SelectedValue + " ";
                                        hi = hi + " and isnull(Withdrwal,'') = '' ";
                                        hi = hi + " and isnull(blocked,'') = '') ";
                                        hi = hi + " group by m.SrNo)T1 ";
                                        double highestMarks = double.Parse((_oo.ReturnTag(hi, "highestMarks").ToString() == "" ? "0" : _oo.ReturnTag(hi, "highestMarks")));

                                        string avg = "select sum(avgMarks)avgMarks from (select TRY_CONVERT(decimal(10,0),(sum(TRY_CONVERT(decimal(10,0),((TRY_CONVERT(decimal(10,0),(";
                                        avg = avg + " case when (isnull(TRY_CONVERT(decimal(10,2),m.Test1),0)*10)/ isnull(TRY_CONVERT(decimal(10,2), mm.MaxMarks1), 1) > (isnull(TRY_CONVERT(decimal(10,1),m.Test2),0)*10)/ isnull(TRY_CONVERT(decimal(10,2), mm.MaxMarks2), 1)  ";
                                        avg = avg + " then (isnull(TRY_CONVERT(decimal(10,2), m.Test1), 0) * 10) / isnull(TRY_CONVERT(decimal(10,2), mm.MaxMarks1), 1) else  (isnull(TRY_CONVERT(decimal(10,2), m.Test2), 0) * 10) / isnull(TRY_CONVERT(decimal(10,2), mm.MaxMarks2), 1) end)) ";
                                        avg = avg + " + isnull(TRY_CONVERT(decimal(10,0), m.SAT), 0))/(10+isnull(TRY_CONVERT(decimal(10,2), mm.MaxMarks3), 0)) *isnull(TRY_CONVERT(decimal(10,2), mm.MaxMarks3), 1)) +isnull(TRY_CONVERT(decimal(10,0), m.Prac), 0)))/2)) avgMarks ";
                                        avg = avg + " from CCEXI_1718 m inner join TTSubjectMaster sm on sm.Id=m.SubjectId and sm.Classid=m.ClassId and sm.SessionName=m.SessionName ";
                                        avg = avg + " and sm.BranchCode=m.BranchCode inner join SetMaxMinMarks_XI mm on mm.SubjectActivityId=m.SubjectId and m.PaperId=mm.PaperId  ";
                                        avg = avg + " and m.SessionName=mm.SessionName and m.BranchCode=mm.BranchCode and m.Evaluation=mm.Eval where m.ClassId=" + drpclass.SelectedValue + " and m.SectionName='" + drpsection.SelectedItem.Text + "' ";
                                        avg = avg + " and m.SubjectId=" + ds.Tables[0].Rows[p]["SubjectId_1"].ToString() + "   and m.BranchCode=" + Session["BranchCode"] + " and m.SessionName='" + Session["SessionName"] + "'  ";
                                        avg = avg + " and SrNo in (select SrNo from AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") stu  where stu.sessionName='" + Session["SessionName"] + "' and isnull(stu.Promotion, '')<>'Cancelled' ";
                                        avg = avg + " and stu.ClassId=" + drpclass.SelectedValue + " and stu.SectionName='" + drpsection.SelectedItem.Text + "'  and BranchId=" + drpBranch.SelectedValue + "  and isnull(Withdrwal,'') = ''  and isnull(blocked,'') = '' ) group by SrNo)T1 ";

                                        string totalStu = "select count(distinct SrNo) totalStu ";
                                        totalStu = totalStu + " from CCEXI_1718 m inner join SetMaxMinMarks_XI mm on mm.SubjectActivityId=m.SubjectId and m.PaperId=mm.PaperId and m.SessionName=mm.SessionName and m.BranchCode=mm.BranchCode and m.Evaluation=mm.Eval ";
                                        totalStu = totalStu + " where m.ClassId=" + drpclass.SelectedValue + " and m.SectionName='" + drpsection.SelectedItem.Text + "' and m.SubjectId=" + ds.Tables[0].Rows[p]["SubjectId_1"].ToString() + "  ";
                                        totalStu = totalStu + " and m.BranchCode=" + Session["BranchCode"] + " and m.SessionName='" + Session["SessionName"] + "' ";
                                        totalStu = totalStu + " and SrNo in (select SrNo from AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") stu ";
                                        totalStu = totalStu + " where stu.sessionName='" + Session["SessionName"] + "' and isnull(stu.Promotion, '')<>'Cancelled' ";
                                        totalStu = totalStu + " and stu.ClassId=" + drpclass.SelectedValue + " and stu.SectionName='" + drpsection.SelectedItem.Text + "'  and BranchId=" + drpBranch.SelectedValue + " ";
                                        totalStu = totalStu + " and isnull(Withdrwal,'') = '' ";
                                        totalStu = totalStu + " and isnull(blocked,'') = '') ";
                                        double avgMarks2 = double.Parse((_oo.ReturnTag(avg, "avgMarks").ToString() == "" ? "0" : _oo.ReturnTag(avg, "avgMarks")));
                                        double totalObt = (ObtMarks2 == 0 ? ObtMarks1 : ((ObtMarks1 + ObtMarks2))) / 2;
                                        string sname = ds.Tables[0].Rows[p]["ShortCode"].ToString();
                                        string optStu = "select count(*) optStu from ICSEOptionalSubjectAllotment where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "  and OptSubjectId=" + ds.Tables[0].Rows[p]["SubjectId_1"].ToString() + " and Srno in (select Srno from StudentOfficialDetails where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and SectionId=" + drpsection.SelectedValue + " and Branch=" + drpBranch.SelectedValue + " and isnull(Withdrwal,'')='')";
                                        int optCount = int.Parse(_oo.ReturnTag(optStu, "optStu"));
                                        if (optCount == 0)
                                        {
                                            optCount = int.Parse(_oo.ReturnTag(totalStu, "totalStu").ToString());
                                        }
                                        dts.Rows.Add(sname, highestMarks.ToString("0"), totalObt.ToString("0"), (avgMarks2 / optCount).ToString("0"));
                                    }
                                    if (ds.Tables[1].Rows.Count > 0)
                                    {
                                        Repeater rptmarksT2Additional = (Repeater)rptStudent.Items[i].FindControl("rptmarksT2Additional");
                                        rptmarksT2Additional.DataSource = ds.Tables[1];
                                        rptmarksT2Additional.DataBind();
                                        for (int p = 0; p < ds.Tables[1].Rows.Count; p++)
                                        {
                                            double MaxMarks1 = 0; double MaxMarks2 = 0; double MaxMarks3 = 0; double MaxMarks4 = 0; double Test1 = 0; double Test2 = 0; double Prac = 0; double SAT = 0;
                                            if (ds.Tables[1].Rows[p]["MaxMarks1_1"].ToString().Trim().ToUpper() == "NP" || ds.Tables[1].Rows[p]["MaxMarks1_1"].ToString().Trim().ToUpper() == "NAD" || ds.Tables[1].Rows[p]["MaxMarks1_1"].ToString().Trim().ToUpper() == "ML" || ds.Tables[1].Rows[p]["MaxMarks1_1"].ToString().Trim().ToUpper() == "" || ds.Tables[1].Rows[p]["MaxMarks1_1"].ToString().Trim().ToUpper() == "AB" || ds.Tables[1].Rows[p]["MaxMarks1_1"].ToString().Trim().ToUpper() == "NA" || ds.Tables[1].Rows[p]["MaxMarks1_1"].ToString().Trim().ToUpper() == "M.L") { }
                                            else { MaxMarks1 = double.Parse(ds.Tables[1].Rows[p]["MaxMarks1_1"].ToString().Trim()); }
                                            if (ds.Tables[1].Rows[p]["MaxMarks2_1"].ToString().Trim().ToUpper() == "NP" || ds.Tables[1].Rows[p]["MaxMarks2_1"].ToString().Trim().ToUpper() == "NAD" || ds.Tables[1].Rows[p]["MaxMarks2_1"].ToString().Trim().ToUpper() == "ML" || ds.Tables[1].Rows[p]["MaxMarks2_1"].ToString().Trim().ToUpper() == "" || ds.Tables[1].Rows[p]["MaxMarks2_1"].ToString().Trim().ToUpper() == "AB" || ds.Tables[1].Rows[p]["MaxMarks2_1"].ToString().Trim().ToUpper() == "NA" || ds.Tables[1].Rows[p]["MaxMarks2_1"].ToString().Trim().ToUpper() == "M.L") { }
                                            else { MaxMarks2 = double.Parse(ds.Tables[1].Rows[p]["MaxMarks2_1"].ToString().Trim()); }
                                            if (ds.Tables[1].Rows[p]["MaxMarks3_1"].ToString().Trim().ToUpper() == "NP" || ds.Tables[1].Rows[p]["MaxMarks3_1"].ToString().Trim().ToUpper() == "NAD" || ds.Tables[1].Rows[p]["MaxMarks3_1"].ToString().Trim().ToUpper() == "ML" || ds.Tables[1].Rows[p]["MaxMarks3_1"].ToString().Trim().ToUpper() == "" || ds.Tables[1].Rows[p]["MaxMarks3_1"].ToString().Trim().ToUpper() == "AB" || ds.Tables[1].Rows[p]["MaxMarks3_1"].ToString().Trim().ToUpper() == "NA" || ds.Tables[1].Rows[p]["MaxMarks3_1"].ToString().Trim().ToUpper() == "M.L") { }
                                            else { MaxMarks3 = double.Parse(ds.Tables[1].Rows[p]["MaxMarks3_1"].ToString().Trim()); }
                                            if (ds.Tables[1].Rows[p]["MaxMarks4_1"].ToString().Trim().ToUpper() == "NP" || ds.Tables[1].Rows[p]["MaxMarks4_1"].ToString().Trim().ToUpper() == "NAD" || ds.Tables[1].Rows[p]["MaxMarks4_1"].ToString().Trim().ToUpper() == "ML" || ds.Tables[1].Rows[p]["MaxMarks4_1"].ToString().Trim().ToUpper() == "" || ds.Tables[1].Rows[p]["MaxMarks4_1"].ToString().Trim().ToUpper() == "AB" || ds.Tables[1].Rows[p]["MaxMarks4_1"].ToString().Trim().ToUpper() == "NA" || ds.Tables[1].Rows[p]["MaxMarks4_1"].ToString().Trim().ToUpper() == "M.L") { }
                                            else { MaxMarks4 = double.Parse(ds.Tables[1].Rows[p]["MaxMarks4_1"].ToString().Trim()); }
                                            if (ds.Tables[1].Rows[p]["Test1_1"].ToString().Trim().ToUpper() == "NP" || ds.Tables[1].Rows[p]["Test1_1"].ToString().Trim().ToUpper() == "NAD" || ds.Tables[1].Rows[p]["Test1_1"].ToString().Trim().ToUpper() == "ML" || ds.Tables[1].Rows[p]["Test1_1"].ToString().Trim().ToUpper() == "" || ds.Tables[1].Rows[p]["Test1_1"].ToString().Trim().ToUpper() == "AB" || ds.Tables[1].Rows[p]["Test1_1"].ToString().Trim().ToUpper() == "NA" || ds.Tables[1].Rows[p]["Test1_1"].ToString().Trim().ToUpper() == "M.L") { }
                                            else { Test1 = double.Parse(ds.Tables[1].Rows[p]["Test1_1"].ToString().Trim()); }
                                            if (ds.Tables[1].Rows[p]["Test2_1"].ToString().Trim().ToUpper() == "NP" || ds.Tables[1].Rows[p]["Test2_1"].ToString().Trim().ToUpper() == "NAD" || ds.Tables[1].Rows[p]["Test2_1"].ToString().Trim().ToUpper() == "ML" || ds.Tables[1].Rows[p]["Test2_1"].ToString().Trim().ToUpper() == "" || ds.Tables[1].Rows[p]["Test2_1"].ToString().Trim().ToUpper() == "AB" || ds.Tables[1].Rows[p]["Test2_1"].ToString().Trim().ToUpper() == "NA" || ds.Tables[1].Rows[p]["Test2_1"].ToString().Trim().ToUpper() == "M.L") { }
                                            else { Test2 = double.Parse(ds.Tables[1].Rows[p]["Test2_1"].ToString().Trim()); }
                                            if (ds.Tables[1].Rows[p]["Prac_1"].ToString().Trim().ToUpper() == "NP" || ds.Tables[1].Rows[p]["Prac_1"].ToString().Trim().ToUpper() == "NAD" || ds.Tables[1].Rows[p]["Prac_1"].ToString().Trim().ToUpper() == "ML" || ds.Tables[1].Rows[p]["Prac_1"].ToString().Trim().ToUpper() == "" || ds.Tables[1].Rows[p]["Prac_1"].ToString().Trim().ToUpper() == "AB" || ds.Tables[1].Rows[p]["Prac_1"].ToString().Trim().ToUpper() == "NA" || ds.Tables[1].Rows[p]["Prac_1"].ToString().Trim().ToUpper() == "M.L") { }
                                            else { Prac = double.Parse(ds.Tables[1].Rows[p]["Prac_1"].ToString().Trim()); }
                                            if (ds.Tables[1].Rows[p]["SAT_1"].ToString().Trim().ToUpper() == "NP" || ds.Tables[1].Rows[p]["SAT_1"].ToString().Trim().ToUpper() == "NAD" || ds.Tables[1].Rows[p]["SAT_1"].ToString().Trim().ToUpper() == "ML" || ds.Tables[1].Rows[p]["SAT_1"].ToString().Trim().ToUpper() == "" || ds.Tables[1].Rows[p]["SAT_1"].ToString().Trim().ToUpper() == "AB" || ds.Tables[1].Rows[p]["SAT_1"].ToString().Trim().ToUpper() == "NA" || ds.Tables[1].Rows[p]["SAT_1"].ToString().Trim().ToUpper() == "M.L") { }
                                            else { SAT = double.Parse(ds.Tables[1].Rows[p]["SAT_1"].ToString().Trim()); }
                                            double totalmarks1 = 0; double IA1 = 0;
                                            totalmarks1 = (MaxMarks1 > 0 ? (Test1 * 20) / MaxMarks1 : 0) > (MaxMarks2 > 0 ? (Test2 * 20) / MaxMarks2 : 0) ? (MaxMarks1 > 0 ? (Test1 * 20) / MaxMarks1 : 0) : (MaxMarks2 > 0 ? (Test2 * 20) / MaxMarks2 : 0);
                                            IA1 = ((totalmarks1 * 10) / 20);
                                            double ConvinBoard = (((double.Parse(IA1.ToString("0")) + SAT) / (10 + MaxMarks3)) * MaxMarks3);

                                            double ObtMarks1 = (double.Parse(ConvinBoard.ToString("0")) + double.Parse(Prac.ToString("0")));
                                            totalM1 = totalM1 + ObtMarks1;
                                            totalMM1 = totalMM1 + 100;

                                            Label lblIA = (Label)rptmarksT2Additional.Items[p].FindControl("lblIA");
                                            Label lblHYMM = (Label)rptmarksT2Additional.Items[p].FindControl("lblHYMM");
                                            Label lblHY = (Label)rptmarksT2Additional.Items[p].FindControl("lblHY");
                                            Label lblConvBoardMM = (Label)rptmarksT2Additional.Items[p].FindControl("lblConvBoardMM");
                                            Label lblConvBoard = (Label)rptmarksT2Additional.Items[p].FindControl("lblConvBoard");
                                            Label lblPracMM = (Label)rptmarksT2Additional.Items[p].FindControl("lblPracMM");
                                            Label lblPrac = (Label)rptmarksT2Additional.Items[p].FindControl("lblPrac");
                                            Label lblTotal = (Label)rptmarksT2Additional.Items[p].FindControl("lblTotal");
                                            Label lblGrade = (Label)rptmarksT2Additional.Items[p].FindControl("lblGrade");

                                            lblIA.Text = (IA1 == 0 ? "NP" : IA1.ToString("0"));
                                            lblHYMM.Text = MaxMarks3.ToString("0");
                                            lblHY.Text = (SAT == 0 ? "NP" : SAT.ToString("0"));
                                            lblConvBoardMM.Text = (((10 + MaxMarks3) / (10 + MaxMarks3)) * MaxMarks3).ToString("0");
                                            lblConvBoard.Text = ConvinBoard.ToString("0");
                                            lblPracMM.Text = MaxMarks4.ToString("0");
                                            lblPrac.Text = (Prac == 0 ? "NP" : Prac.ToString("0"));

                                            lblTotal.Text = ObtMarks1.ToString("0");
                                            string grade1 = grade(double.Parse(ObtMarks1.ToString("0")));
                                            lblGrade.Text = grade1;

                                            double MaxMarks1_2 = 0; double MaxMarks2_2 = 0; double MaxMarks3_2 = 0; double MaxMarks4_2 = 0; double Test1_2 = 0; double Test2_2 = 0; double Prac_2 = 0; double SAT_2 = 0;
                                            if (ds.Tables[1].Rows[p]["MaxMarks1_2"].ToString().Trim().ToUpper() == "NP" || ds.Tables[1].Rows[p]["MaxMarks1_2"].ToString().Trim().ToUpper() == "NAD" || ds.Tables[1].Rows[p]["MaxMarks1_2"].ToString().Trim().ToUpper() == "ML" || ds.Tables[1].Rows[p]["MaxMarks1_2"].ToString().Trim().ToUpper() == "" || ds.Tables[1].Rows[p]["MaxMarks1_2"].ToString().Trim().ToUpper() == "AB" || ds.Tables[1].Rows[p]["MaxMarks1_2"].ToString().Trim().ToUpper() == "NA" || ds.Tables[1].Rows[p]["MaxMarks1_2"].ToString().Trim().ToUpper() == "M.L") { }
                                            else { MaxMarks1_2 = double.Parse(ds.Tables[1].Rows[p]["MaxMarks1_2"].ToString().Trim()); }
                                            if (ds.Tables[1].Rows[p]["MaxMarks2_2"].ToString().Trim().ToUpper() == "NP" || ds.Tables[1].Rows[p]["MaxMarks2_2"].ToString().Trim().ToUpper() == "NAD" || ds.Tables[1].Rows[p]["MaxMarks2_2"].ToString().Trim().ToUpper() == "ML" || ds.Tables[1].Rows[p]["MaxMarks2_2"].ToString().Trim().ToUpper() == "" || ds.Tables[1].Rows[p]["MaxMarks2_2"].ToString().Trim().ToUpper() == "AB" || ds.Tables[1].Rows[p]["MaxMarks2_2"].ToString().Trim().ToUpper() == "NA" || ds.Tables[1].Rows[p]["MaxMarks2_2"].ToString().Trim().ToUpper() == "M.L") { }
                                            else { MaxMarks2_2 = double.Parse(ds.Tables[1].Rows[p]["MaxMarks2_2"].ToString().Trim()); }
                                            if (ds.Tables[1].Rows[p]["MaxMarks3_2"].ToString().Trim().ToUpper() == "NP" || ds.Tables[1].Rows[p]["MaxMarks3_2"].ToString().Trim().ToUpper() == "NAD" || ds.Tables[1].Rows[p]["MaxMarks3_2"].ToString().Trim().ToUpper() == "ML" || ds.Tables[1].Rows[p]["MaxMarks3_2"].ToString().Trim().ToUpper() == "" || ds.Tables[1].Rows[p]["MaxMarks3_2"].ToString().Trim().ToUpper() == "AB" || ds.Tables[1].Rows[p]["MaxMarks3_2"].ToString().Trim().ToUpper() == "NA" || ds.Tables[1].Rows[p]["MaxMarks3_2"].ToString().Trim().ToUpper() == "M.L") { }
                                            else { MaxMarks3_2 = double.Parse(ds.Tables[1].Rows[p]["MaxMarks3_2"].ToString().Trim()); }
                                            if (ds.Tables[1].Rows[p]["MaxMarks4_2"].ToString().Trim().ToUpper() == "NP" || ds.Tables[1].Rows[p]["MaxMarks4_2"].ToString().Trim().ToUpper() == "NAD" || ds.Tables[1].Rows[p]["MaxMarks4_2"].ToString().Trim().ToUpper() == "ML" || ds.Tables[1].Rows[p]["MaxMarks4_2"].ToString().Trim().ToUpper() == "" || ds.Tables[1].Rows[p]["MaxMarks4_2"].ToString().Trim().ToUpper() == "AB" || ds.Tables[1].Rows[p]["MaxMarks4_2"].ToString().Trim().ToUpper() == "NA" || ds.Tables[1].Rows[p]["MaxMarks4_2"].ToString().Trim().ToUpper() == "M.L") { }
                                            else { MaxMarks4_2 = double.Parse(ds.Tables[1].Rows[p]["MaxMarks4_2"].ToString().Trim()); }
                                            if (ds.Tables[1].Rows[p]["Test1_2"].ToString().Trim().ToUpper() == "NP" || ds.Tables[1].Rows[p]["Test1_2"].ToString().Trim().ToUpper() == "NAD" || ds.Tables[1].Rows[p]["Test1_2"].ToString().Trim().ToUpper() == "ML" || ds.Tables[1].Rows[p]["Test1_2"].ToString().Trim().ToUpper() == "" || ds.Tables[1].Rows[p]["Test1_2"].ToString().Trim().ToUpper() == "AB" || ds.Tables[1].Rows[p]["Test1_2"].ToString().Trim().ToUpper() == "NA" || ds.Tables[1].Rows[p]["Test1_2"].ToString().Trim().ToUpper() == "M.L") { }
                                            else { Test1_2 = double.Parse(ds.Tables[1].Rows[p]["Test1_2"].ToString().Trim()); }
                                            if (ds.Tables[1].Rows[p]["Test2_2"].ToString().Trim().ToUpper() == "NP" || ds.Tables[1].Rows[p]["Test2_2"].ToString().Trim().ToUpper() == "NAD" || ds.Tables[1].Rows[p]["Test2_2"].ToString().Trim().ToUpper() == "ML" || ds.Tables[1].Rows[p]["Test2_2"].ToString().Trim().ToUpper() == "" || ds.Tables[1].Rows[p]["Test2_2"].ToString().Trim().ToUpper() == "AB" || ds.Tables[1].Rows[p]["Test2_2"].ToString().Trim().ToUpper() == "NA" || ds.Tables[1].Rows[p]["Test2_2"].ToString().Trim().ToUpper() == "M.L") { }
                                            else { Test2_2 = double.Parse(ds.Tables[1].Rows[p]["Test2_2"].ToString().Trim()); }
                                            if (ds.Tables[1].Rows[p]["Prac_2"].ToString().Trim().ToUpper() == "NP" || ds.Tables[1].Rows[p]["Prac_2"].ToString().Trim().ToUpper() == "NAD" || ds.Tables[1].Rows[p]["Prac_2"].ToString().Trim().ToUpper() == "ML" || ds.Tables[1].Rows[p]["Prac_2"].ToString().Trim().ToUpper() == "" || ds.Tables[1].Rows[p]["Prac_2"].ToString().Trim().ToUpper() == "AB" || ds.Tables[1].Rows[p]["Prac_2"].ToString().Trim().ToUpper() == "NA" || ds.Tables[1].Rows[p]["Prac_2"].ToString().Trim().ToUpper() == "M.L") { }
                                            else { Prac_2 = double.Parse(ds.Tables[1].Rows[p]["Prac_2"].ToString().Trim()); }
                                            if (ds.Tables[1].Rows[p]["SAT_2"].ToString().Trim().ToUpper() == "NP" || ds.Tables[1].Rows[p]["SAT_2"].ToString().Trim().ToUpper() == "NAD" || ds.Tables[1].Rows[p]["SAT_2"].ToString().Trim().ToUpper() == "ML" || ds.Tables[1].Rows[p]["SAT_2"].ToString().Trim().ToUpper() == "" || ds.Tables[1].Rows[p]["SAT_2"].ToString().Trim().ToUpper() == "AB" || ds.Tables[1].Rows[p]["SAT_2"].ToString().Trim().ToUpper() == "NA" || ds.Tables[1].Rows[p]["SAT_2"].ToString().Trim().ToUpper() == "M.L") { }
                                            else { SAT_2 = double.Parse(ds.Tables[1].Rows[p]["SAT_2"].ToString().Trim()); }
                                            double totalmarks2 = 0; double IA2 = 0;
                                            totalmarks2 = (MaxMarks1_2 > 0 ? (Test1_2 * 20) / MaxMarks1_2 : 0) > (MaxMarks2_2 > 0 ? (Test2_2 * 20) / MaxMarks2_2 : 0) ? (MaxMarks1_2 > 0 ? (Test1_2 * 20) / MaxMarks1_2 : 0) : (MaxMarks2_2 > 0 ? (Test2_2 * 20) / MaxMarks2_2 : 0);
                                            IA2 = ((totalmarks2 * 10) / 20);
                                            double ConvinBoard_2 = (((double.Parse(IA2.ToString("0")) + SAT_2) / (10 + MaxMarks3_2)) * MaxMarks3_2);

                                            double ObtMarks2 = (double.Parse(ConvinBoard_2.ToString("0")) + double.Parse(Prac_2.ToString("0")));
                                            totalM2 = totalM2 + ObtMarks2;
                                            totalMM2 = totalMM2 + 100;

                                            Label lblIA2 = (Label)rptmarksT2Additional.Items[p].FindControl("lblIA2");
                                            Label lblAEMM = (Label)rptmarksT2Additional.Items[p].FindControl("lblAEMM");
                                            Label lblAE = (Label)rptmarksT2Additional.Items[p].FindControl("lblAE");
                                            Label lblConvBoardMM2 = (Label)rptmarksT2Additional.Items[p].FindControl("lblConvBoardMM2");
                                            Label lblConvBoard2 = (Label)rptmarksT2Additional.Items[p].FindControl("lblConvBoard2");
                                            Label lblPracMM2 = (Label)rptmarksT2Additional.Items[p].FindControl("lblPracMM2");
                                            Label lblPrac2 = (Label)rptmarksT2Additional.Items[p].FindControl("lblPrac2");
                                            Label lblTotal2 = (Label)rptmarksT2Additional.Items[p].FindControl("lblTotal2");
                                            Label lblGrade2 = (Label)rptmarksT2Additional.Items[p].FindControl("lblGrade2");

                                            lblIA2.Text = (IA2 == 0 ? "NP" : IA2.ToString("0"));
                                            lblAEMM.Text = MaxMarks3_2.ToString("0");
                                            lblAE.Text = (SAT_2 == 0 ? "NP" : SAT_2.ToString("0"));
                                            lblConvBoardMM2.Text = (((10 + MaxMarks3_2) / (10 + MaxMarks3_2)) * MaxMarks3_2).ToString("0");
                                            lblConvBoard2.Text = ConvinBoard_2.ToString("0");
                                            lblPracMM2.Text = MaxMarks4_2.ToString("0");
                                            lblPrac2.Text = (Prac_2 == 0 ? "NP" : Prac_2.ToString("0"));

                                            lblTotal2.Text = ObtMarks2.ToString("0");
                                            string grade2 = grade(double.Parse(ObtMarks2.ToString("0")));
                                            lblGrade2.Text = grade2;

                                            double GrandTotal = (ObtMarks1 + ObtMarks2);
                                            double GrandTotalF = (ObtMarks1 + ObtMarks2)/2;
                                            string GradeF = grade((ObtMarks1 + ObtMarks2)/2);

                                            Label lblGrandTotal = (Label)rptmarksT2Additional.Items[p].FindControl("lblGrandTotal");
                                            lblGrandTotal.Text = GrandTotal.ToString("0");
                                            Label lblGrandTotalF = (Label)rptmarksT2Additional.Items[p].FindControl("lblGrandTotalF");
                                            lblGrandTotalF.Text = GrandTotalF.ToString("0");
                                            Label lblGradeF = (Label)rptmarksT2Additional.Items[p].FindControl("lblGradeF");
                                            lblGradeF.Text = GradeF;
                                            string mysql = "select case when ISNULL(isCompulsoryForBest5, 0)=0 then 0 else 1 end as SubjectType from TTSubjectMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and Classid=" + drpclass.SelectedValue + " and BranchId=" + drpBranch.SelectedValue + " and id=" + ds.Tables[1].Rows[p]["SubjectId_1"].ToString() + "";
                                            DataRow datarow = totalNum.NewRow();
                                            var sCompalsary = _oo.ReturnTag(mysql, "SubjectType");
                                            datarow["IsCompalsary"] = sCompalsary;
                                            datarow["Marks"] = GrandTotalF;
                                            totalNum.Rows.Add(datarow);


                                            string hi = "select isnull(TRY_CONVERT(decimal(10,0),max(T1.highestMarks)),0) highestMarks  from ( ";
                                            hi = hi + " select sum((((TRY_CONVERT(decimal(10,0),(case when isnull(TRY_CONVERT(decimal(10,0),m.Test1),0)>isnull(TRY_CONVERT(decimal(10,0),m.Test2),0)  ";
                                            hi = hi + " then(isnull(TRY_CONVERT(decimal(10, 0), m.Test1), 0) * 10) / isnull(TRY_CONVERT(decimal(10, 0), mm.MaxMarks1), 1)  else  (isnull(TRY_CONVERT(decimal(10, 0), m.Test2), 0) * 10) / isnull(TRY_CONVERT(decimal(10, 0), mm.MaxMarks2), 1) end)) ";
                                            hi = hi + " +isnull(TRY_CONVERT(decimal(10, 0), m.SAT), 0))  / (10 +isnull(TRY_CONVERT(decimal(10, 0), MaxMarks3), 1))) ";
                                            hi = hi + " *isnull(TRY_CONVERT(decimal(10, 0), MaxMarks3), 1)) +isnull(TRY_CONVERT(decimal(10, 0), m.Prac), 0)) highestMarks ";
                                            hi = hi + " from CCEXI_1718 m inner join TTSubjectMaster sm on sm.Id=m.SubjectId and sm.Classid=m.ClassId and sm.SessionName=m.SessionName and sm.BranchCode=m.BranchCode inner join SetMaxMinMarks_XI mm on mm.SubjectActivityId=m.SubjectId and m.PaperId=mm.PaperId and m.SessionName=mm.SessionName and m.BranchCode=mm.BranchCode and m.Evaluation=mm.Eval where m.ClassId=" + drpclass.SelectedValue + " and m.SectionName='" + drpsection.SelectedItem.Text + "' and m.SubjectId=" + ds.Tables[1].Rows[p]["SubjectId_1"].ToString() + "  ";
                                            hi = hi + " and m.BranchCode=" + Session["BranchCode"] + " and m.SessionName='" + Session["SessionName"] + "'  ";
                                            hi = hi + " and SrNo in (select SrNo from AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") stu ";
                                            hi = hi + " where stu.sessionName='" + Session["SessionName"] + "' and isnull(stu.Promotion, '')<>'Cancelled' ";
                                            hi = hi + " and stu.ClassId=" + drpclass.SelectedValue + " and stu.SectionName='" + drpsection.SelectedItem.Text + "'  and BranchId=" + drpBranch.SelectedValue + " ";
                                            hi = hi + " and isnull(Withdrwal,'') = '' ";
                                            hi = hi + " and isnull(blocked,'') = '') ";
                                            hi = hi + " group by m.SrNo)T1 ";
                                            double highestMarks = double.Parse((_oo.ReturnTag(hi, "highestMarks").ToString() == "" ? "0" : _oo.ReturnTag(hi, "highestMarks")));

                                            string avg = "select sum(avgMarks)avgMarks from (select TRY_CONVERT(decimal(10,0),(sum(TRY_CONVERT(decimal(10,0),((TRY_CONVERT(decimal(10,0),(";
                                            avg = avg + " case when (isnull(TRY_CONVERT(decimal(10,2),m.Test1),0)*10)/ isnull(TRY_CONVERT(decimal(10,2), mm.MaxMarks1), 1) > (isnull(TRY_CONVERT(decimal(10,1),m.Test2),0)*10)/ isnull(TRY_CONVERT(decimal(10,2), mm.MaxMarks2), 1)  ";
                                            avg = avg + " then (isnull(TRY_CONVERT(decimal(10,2), m.Test1), 0) * 10) / isnull(TRY_CONVERT(decimal(10,2), mm.MaxMarks1), 1) else  (isnull(TRY_CONVERT(decimal(10,2), m.Test2), 0) * 10) / isnull(TRY_CONVERT(decimal(10,2), mm.MaxMarks2), 1) end)) ";
                                            avg = avg + " + isnull(TRY_CONVERT(decimal(10,0), m.SAT), 0))/(10+isnull(TRY_CONVERT(decimal(10,2), mm.MaxMarks3), 0)) *isnull(TRY_CONVERT(decimal(10,2), mm.MaxMarks3), 1)) +isnull(TRY_CONVERT(decimal(10,0), m.Prac), 0)))/2)) avgMarks ";
                                            avg = avg + " from CCEXI_1718 m inner join TTSubjectMaster sm on sm.Id=m.SubjectId and sm.Classid=m.ClassId and sm.SessionName=m.SessionName and sm.BranchCode=m.BranchCode inner join SetMaxMinMarks_XI mm on mm.SubjectActivityId=m.SubjectId and m.PaperId=mm.PaperId and m.SessionName=mm.SessionName and m.BranchCode=mm.BranchCode and m.Evaluation=mm.Eval ";
                                            avg = avg + " where m.ClassId=" + drpclass.SelectedValue + " and m.SectionName='" + drpsection.SelectedItem.Text + "' and m.SubjectId=" + ds.Tables[1].Rows[p]["SubjectId_1"].ToString() + "  ";
                                            avg = avg + " and m.BranchCode=" + Session["BranchCode"] + " and m.SessionName='" + Session["SessionName"] + "'  ";
                                            avg = avg + " and SrNo in (select SrNo from AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") stu ";
                                            avg = avg + " where stu.sessionName='" + Session["SessionName"] + "' and isnull(stu.Promotion, '')<>'Cancelled' ";
                                            avg = avg + " and stu.ClassId=" + drpclass.SelectedValue + " and stu.SectionName='" + drpsection.SelectedItem.Text + "'  and BranchId=" + drpBranch.SelectedValue + " ";
                                            avg = avg + " and isnull(Withdrwal,'') = '' ";
                                            avg = avg + " and isnull(blocked,'') = '') ";
                                            avg = avg + ")T1 ";

                                            string totalStu = "select count(*) totalStu ";
                                            totalStu = totalStu + " from CCEXI_1718 m inner join SetMaxMinMarks_XI mm on mm.SubjectActivityId=m.SubjectId and m.PaperId=mm.PaperId and m.SessionName=mm.SessionName and m.BranchCode=mm.BranchCode and m.Evaluation=mm.Eval ";
                                            totalStu = totalStu + " where m.ClassId=" + drpclass.SelectedValue + " and m.SectionName='" + drpsection.SelectedItem.Text + "' and m.Evaluation='TERM1' and m.SubjectId=" + ds.Tables[1].Rows[p]["SubjectId_1"].ToString() + "  ";
                                            totalStu = totalStu + " and m.BranchCode=" + Session["BranchCode"] + " and m.SessionName='" + Session["SessionName"] + "' ";
                                            totalStu = totalStu + " and SrNo in (select SrNo from AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") stu ";
                                            totalStu = totalStu + " where stu.sessionName='" + Session["SessionName"] + "' and isnull(stu.Promotion, '')<>'Cancelled' ";
                                            totalStu = totalStu + " and stu.ClassId=" + drpclass.SelectedValue + " and stu.SectionName='" + drpsection.SelectedItem.Text + "'  and BranchId=" + drpBranch.SelectedValue + " ";
                                            totalStu = totalStu + " and isnull(Withdrwal,'') = '' ";
                                            totalStu = totalStu + " and isnull(blocked,'') = '') ";
                                            double avgMarks2 = double.Parse((_oo.ReturnTag(avg, "avgMarks").ToString() == "" ? "0" : _oo.ReturnTag(avg, "avgMarks"))) / 2;
                                            double totalObt = (ObtMarks2 == 0 ? ObtMarks1 : ((ObtMarks1 + ObtMarks2))) / 2;
                                            string sname = ds.Tables[1].Rows[p]["ShortCode"].ToString();
                                            string optStu = "select count(*) optStu from ICSEOptionalSubjectAllotment where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "  and OptSubjectId=" + ds.Tables[1].Rows[p]["SubjectId_1"].ToString() + " and Srno in (select Srno from StudentOfficialDetails where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and SectionId=" + drpsection.SelectedValue + " and Branch=" + drpBranch.SelectedValue + " and isnull(Withdrwal,'')='')";
                                            int optCount = int.Parse(_oo.ReturnTag(optStu, "optStu"));
                                            if (optCount == 0)
                                            {
                                                optCount = int.Parse(_oo.ReturnTag(totalStu, "totalStu").ToString());
                                            }
                                            dts.Rows.Add(sname, (highestMarks / 2).ToString("0"), totalObt.ToString("0"), (avgMarks2 / optCount).ToString("0"));

                                        }
                                        Chart myChart = (Chart)rptStudent.Items[i].FindControl("myChart");
                                        chart(myChart, dts);
                                        Label lblHTotal = (Label)rptmarksT2Additional.Controls[rptmarksT2Additional.Controls.Count - 1].Controls[0].FindControl("lblHTotal");
                                        Label lblHTotal2 = (Label)rptmarksT2Additional.Controls[rptmarksT2Additional.Controls.Count - 1].Controls[0].FindControl("lblHTotal2");
                                        Label lblHTotalGT = (Label)rptmarksT2Additional.Controls[rptmarksT2Additional.Controls.Count - 1].Controls[0].FindControl("lblHTotalGT");
                                        Label lblHPercentage = (Label)rptmarksT2Additional.Controls[rptmarksT2Additional.Controls.Count - 1].Controls[0].FindControl("lblHPercentage");
                                        Label lblHPercentage2 = (Label)rptmarksT2Additional.Controls[rptmarksT2Additional.Controls.Count - 1].Controls[0].FindControl("lblHPercentage2");
                                        Label lblHPercentageGT = (Label)rptmarksT2Additional.Controls[rptmarksT2Additional.Controls.Count - 1].Controls[0].FindControl("lblHPercentageGT");
                                        Label lblHRank = (Label)rptmarksT2Additional.Controls[rptmarksT2Additional.Controls.Count - 1].Controls[0].FindControl("lblHRank");
                                        Label lblHRank2 = (Label)rptmarksT2Additional.Controls[rptmarksT2Additional.Controls.Count - 1].Controls[0].FindControl("lblHRank2");
                                        Label lblHRankGT = (Label)rptmarksT2Additional.Controls[rptmarksT2Additional.Controls.Count - 1].Controls[0].FindControl("lblHRankGT");
                                        Label lblHPosition = (Label)rptmarksT2Additional.Controls[rptmarksT2Additional.Controls.Count - 1].Controls[0].FindControl("lblHPosition");
                                        Label lblHPosition2 = (Label)rptmarksT2Additional.Controls[rptmarksT2Additional.Controls.Count - 1].Controls[0].FindControl("lblHPosition2");
                                        Label lblHPositionGT = (Label)rptmarksT2Additional.Controls[rptmarksT2Additional.Controls.Count - 1].Controls[0].FindControl("lblHPositionGT");
                                        lblHTotal.Text = double.Parse(ds.Tables[7].Rows[0]["term1Total"].ToString()).ToString("0").Trim() + "/600";
                                        lblHTotal2.Text = double.Parse(ds.Tables[7].Rows[0]["term2Total"].ToString()).ToString("0").Trim() + "/600";

                                        var results = (from myRow in totalNum.AsEnumerable()
                                                       //where myRow.Field<string>("IsCompalsary") == "1"
                                                       select double.Parse(myRow.Field<decimal>("Marks").ToString("0"))).Sum();

                                        //var datacount = (from t in totalNum.AsEnumerable() where t.Field<string>("IsCompalsary") == "1" select t).Count();
                                        //var results2 = (from myRow in totalNum.AsEnumerable()
                                        //               //where myRow.Field<string>("IsCompalsary") == "0"
                                        //                orderby double.Parse(myRow.Field<decimal>("Marks").ToString("0")) descending
                                        //                select double.Parse(myRow.Field<decimal>("Marks").ToString("0"))).Take(10).Sum();
                                        var totalSum = results;

                                        lblHTotalGT.Text = totalSum.ToString("0") + "/600";
                                        lblHPercentage.Text = ((double.Parse(double.Parse(ds.Tables[7].Rows[0]["term1Total"].ToString()).ToString("0")) * 100) / 600).ToString("0.00") + " %";
                                        lblHPercentage2.Text = ((double.Parse(double.Parse(ds.Tables[7].Rows[0]["term2Total"].ToString()).ToString("0")) * 100) / 600).ToString("0.00") + " %";
                                        lblHPercentageGT.Text = ((totalSum * 100) / 600).ToString("0.00") + " %";
                                        lblHRank.Text = (dsRank.Tables[0].Rows.Count > 0 ? dsRank.Tables[0].Rows[0]["ranks"].ToString().Trim() : "-");
                                        lblHRank2.Text = (dsRank.Tables[2].Rows.Count > 0 ? dsRank.Tables[2].Rows[0]["ranks"].ToString().Trim() : "-");
                                        lblHRankGT.Text = (dsRank.Tables[4].Rows.Count > 0 ? dsRank.Tables[4].Rows[0]["ranks"].ToString().Trim() : "-");
                                        lblHPosition.Text = (dsRank.Tables[1].Rows.Count > 0 ? dsRank.Tables[1].Rows[0]["position"].ToString().Trim() : "-");
                                        lblHPosition2.Text = (dsRank.Tables[3].Rows.Count > 0 ? dsRank.Tables[3].Rows[0]["position"].ToString().Trim() : "-");
                                        lblHPositionGT.Text = (dsRank.Tables[5].Rows.Count > 0 ? dsRank.Tables[5].Rows[0]["position"].ToString().Trim() : "-");
                                        HtmlTableRow tr1 = (HtmlTableRow)rptmarksT2.Controls[rptmarksT2.Controls.Count - 1].Controls[0].FindControl("tr1");
                                        HtmlTableRow tr2 = (HtmlTableRow)rptmarksT2.Controls[rptmarksT2.Controls.Count - 1].Controls[0].FindControl("tr2");
                                        HtmlTableRow tr3 = (HtmlTableRow)rptmarksT2.Controls[rptmarksT2.Controls.Count - 1].Controls[0].FindControl("tr3");
                                        HtmlTableRow tr4 = (HtmlTableRow)rptmarksT2.Controls[rptmarksT2.Controls.Count - 1].Controls[0].FindControl("tr4");
                                        tr1.Visible = false;
                                        tr2.Visible = false;
                                        tr3.Visible = false;
                                        tr4.Visible = false;
                                    }
                                    else
                                    {
                                        Chart myChart = (Chart)rptStudent.Items[i].FindControl("myChart");
                                        chart(myChart, dts);
                                        Label lblHTotal = (Label)rptmarksT2.Controls[rptmarksT2.Controls.Count - 1].Controls[0].FindControl("lblHTotal");
                                        Label lblHTotal2 = (Label)rptmarksT2.Controls[rptmarksT2.Controls.Count - 1].Controls[0].FindControl("lblHTotal2");
                                        Label lblHTotalGT = (Label)rptmarksT2.Controls[rptmarksT2.Controls.Count - 1].Controls[0].FindControl("lblHTotalGT");
                                        Label lblHPercentage = (Label)rptmarksT2.Controls[rptmarksT2.Controls.Count - 1].Controls[0].FindControl("lblHPercentage");
                                        Label lblHPercentage2 = (Label)rptmarksT2.Controls[rptmarksT2.Controls.Count - 1].Controls[0].FindControl("lblHPercentage2");
                                        Label lblHPercentageGT = (Label)rptmarksT2.Controls[rptmarksT2.Controls.Count - 1].Controls[0].FindControl("lblHPercentageGT");
                                        Label lblHRank = (Label)rptmarksT2.Controls[rptmarksT2.Controls.Count - 1].Controls[0].FindControl("lblHRank");
                                        Label lblHRank2 = (Label)rptmarksT2.Controls[rptmarksT2.Controls.Count - 1].Controls[0].FindControl("lblHRank2");
                                        Label lblHRankGT = (Label)rptmarksT2.Controls[rptmarksT2.Controls.Count - 1].Controls[0].FindControl("lblHRankGT");
                                        Label lblHPosition = (Label)rptmarksT2.Controls[rptmarksT2.Controls.Count - 1].Controls[0].FindControl("lblHPosition");
                                        Label lblHPosition2 = (Label)rptmarksT2.Controls[rptmarksT2.Controls.Count - 1].Controls[0].FindControl("lblHPosition2");
                                        Label lblHPositionGT = (Label)rptmarksT2.Controls[rptmarksT2.Controls.Count - 1].Controls[0].FindControl("lblHPositionGT");
                                        lblHTotal.Text = double.Parse(ds.Tables[7].Rows[0]["term1Total"].ToString()).ToString("0").Trim() + "/600";
                                        lblHTotal2.Text = double.Parse(ds.Tables[7].Rows[0]["term2Total"].ToString()).ToString("0").Trim() + "/600";

                                        var results = (from myRow in totalNum.AsEnumerable()
                                                       //where myRow.Field<string>("IsCompalsary") == "1"
                                                       select double.Parse(myRow.Field<decimal>("Marks").ToString("0"))).Sum();

                                        //var datacount = (from t in totalNum.AsEnumerable() where t.Field<string>("IsCompalsary") == "1" select t).Count();
                                        //var results2 = (from myRow in totalNum.AsEnumerable()
                                        //                where myRow.Field<string>("IsCompalsary") == "0"
                                        //                orderby double.Parse(myRow.Field<decimal>("Marks").ToString("0")) descending
                                        //                select double.Parse(myRow.Field<decimal>("Marks").ToString("0"))).Take(5 - datacount).Sum();
                                        var totalSum = results;

                                        lblHTotalGT.Text = totalSum.ToString("0") + "/600";
                                        lblHPercentage.Text = ((double.Parse(double.Parse(ds.Tables[7].Rows[0]["term1Total"].ToString()).ToString("0")) * 100) / 600).ToString("0.00") + " %";
                                        lblHPercentage2.Text = ((double.Parse(double.Parse(ds.Tables[7].Rows[0]["term2Total"].ToString()).ToString("0")) * 100) / 600).ToString("0.00") + " %";
                                        lblHPercentageGT.Text = ((totalSum * 100) / 600).ToString("0.00") + " %";
                                        lblHRank.Text = (dsRank.Tables[0].Rows.Count > 0 ? dsRank.Tables[0].Rows[0]["ranks"].ToString().Trim() : "-");
                                        lblHRank2.Text = (dsRank.Tables[2].Rows.Count > 0 ? dsRank.Tables[2].Rows[0]["ranks"].ToString().Trim() : "-");
                                        lblHRankGT.Text = (dsRank.Tables[4].Rows.Count > 0 ? dsRank.Tables[4].Rows[0]["ranks"].ToString().Trim() : "-");
                                        lblHPosition.Text = (dsRank.Tables[1].Rows.Count > 0 ? dsRank.Tables[1].Rows[0]["position"].ToString().Trim() : "-");
                                        lblHPosition2.Text = (dsRank.Tables[3].Rows.Count > 0 ? dsRank.Tables[3].Rows[0]["position"].ToString().Trim() : "-");
                                        lblHPositionGT.Text = (dsRank.Tables[5].Rows.Count > 0 ? dsRank.Tables[5].Rows[0]["position"].ToString().Trim() : "-");
                                    }
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
                                        t1Att = "<span >" + attendence.ToString().Trim() + "</span>/<span >" + totaldays.ToString().Trim() + "</span> (<span >" + percent.ToString("0.00") + "</span> %)";
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
                                        t1Att = "<span >" + attendence1.ToString().Trim() + "</span>/<span >" + totaldays1.ToString().Trim() + "</span> (<span >" + percent1.ToString("0.00") + "</span> %)";
                                    }
                                    double totaldays2 = 0, attendence2 = 0, percent2 = 0;
                                    totaldays2 = double.Parse(ds.Tables[3].Rows[0]["totaldays"].ToString().Trim() == "" ? "0" : ds.Tables[3].Rows[0]["totaldays"].ToString().Trim());
                                    attendence2 = double.Parse(ds.Tables[3].Rows[0]["attendence"].ToString().Trim() == "" ? "0" : ds.Tables[3].Rows[0]["attendence"].ToString().Trim());

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
                                HtmlTable tblResult = (HtmlTable)rptStudent.Items[i].FindControl("tblResult");
                                if (drpEval.SelectedValue.ToUpper() == "TERM2" && Session["Logintype"].ToString() != "Guardian")
                                {
                                    tblResult.Visible = true;
                                    //Label lblresulttype = (Label)rptStudent.Items[i].FindControl("lblresulttype");
                                    //lblresulttype.Text = drpPromoTo.SelectedItem.Text;
                                    //Label lblpromotedClass = (Label)rptStudent.Items[i].FindControl("lblpromotedClass");
                                    //lblpromotedClass.Text = txtPromotedtoclass.Text;
                                    //Label lblReopenon = (Label)rptStudent.Items[i].FindControl("lblReopenon");
                                    //lblReopenon.Text = txtSchoolReopenon.Text + " " + txtTimes.Text;
                                    DataTable dtResults = ds.Tables[8];
                                    Label lblresulttype = (Label)rptStudent.Items[i].FindControl("lblresulttype");
                                    lblresulttype.Text = dtResults.Rows[0]["ResultText"].ToString();
                                    Label lblpromotedClass = (Label)rptStudent.Items[i].FindControl("lblpromotedClass");
                                    lblpromotedClass.Text = dtResults.Rows[0]["PromotedTo"].ToString();
                                    Label lblReopenon = (Label)rptStudent.Items[i].FindControl("lblReopenon");
                                    lblReopenon.Text = dtResults.Rows[0]["ReopenOn"].ToString();

                                    Label lblprintdate = (Label)rptStudent.Items[i].FindControl("lblprintdate");
                                    lblprintdate.Text = dtResults.Rows[0]["GeneratedOn"].ToString();
                                    Label lblPlace = (Label)rptStudent.Items[i].FindControl("lblPlace");
                                    lblPlace.Text = dtResults.Rows[0]["Place"].ToString();
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

                                //Label lblprintdate = (Label)rptStudent.Items[i].FindControl("lblprintdate");
                                //lblprintdate.Text = txtDate.Text;
                                //Label lblPlace = (Label)rptStudent.Items[i].FindControl("lblPlace");
                                //lblPlace.Text = txtPlace.Text;

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
       // columns.Add("Avg. Marks");
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
            _sql +=  " where sctm.BranchCode=" + Session["BranchCode"].ToString() + " and sctm.SessionName = '" + Session["SessionName"] + "' and ClassId='" + drpclass.SelectedValue + "' ";
            _sql +=  " and EmpCode='" + Session["LoginName"].ToString() + "' ";
            _oo.FillDropDown_withValue(_sql, drpsection, "SectionName", "id");
        }
        drpsection.Items.Insert(0, new ListItem("<--Select-->", ""));
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
            _sql +=  " where sctm.BranchCode=" + Session["BranchCode"].ToString() + " and sctm.SessionName = '" + Session["SessionName"] + "' and sctm.ClassId='" + drpclass.SelectedValue + "' and SectionId=" + drpsection.SelectedValue + " ";
            _sql +=  " and EmpCode='" + Session["LoginName"].ToString() + "' ";
            _oo.FillDropDown_withValue(_sql, drpBranch, "BranchName", "id");
            drpBranch.Items.Insert(0, new ListItem("<--Select-->", ""));
        }
        
    }
    protected void drpBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        _sql = "Select StreamName,Id from StreamMaster where ClassId=" + drpclass.SelectedValue + " and BranchId=" + drpBranch.SelectedValue + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        _oo.FillDropDown_withValue(_sql, drpStream, "StreamName", "id");
        drpStream.Items.Insert(0, new ListItem("<--Select-->", ""));
        LoadSrNo();
    }

    protected void drpStream_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSrNo();
    }
    protected void LoadSrNo()
    {
        drpsrno.Items.Clear();
        string drpclass1 = drpclass.SelectedValue;
        string drpsection1 = drpsection.SelectedValue;
        string drpBranch1 = drpBranch.SelectedValue;
        string drpStatus1 = drpStatus.SelectedValue;
        List<SqlParameter> param = new List<SqlParameter>();
            _sql = @"Select Name+' - '+SrNo NAME, SrNo from AllStudentRecord_UDF(@SessionName,@BranchCode) where @Classid=Classid and @Branchid=Branchid 
            and @Sectionid=CASE WHEN @Sectionid='' THEN @Sectionid ELSE Sectionid END and @StreamId=case when @StreamId='' then @StreamId else StreamId end  
            and isnull(Withdrwal,'') = case when isnull(@Withdrwal,'')='B' or isnull(@Withdrwal,'')='' then isnull(Withdrwal,'') else case when isnull(@Withdrwal,'')='A' then '' else 'W' end end
            and isnull(blocked,'') = case when isnull(@Withdrwal,'')='W' or isnull(@Withdrwal,'')='' then isnull(blocked,'') else case when isnull(@Withdrwal,'')='A' then '' else 'yes' end end  ORDER BY NAME";

        param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
        param.Add(new SqlParameter("@Classid", drpclass1.ToString()));
        param.Add(new SqlParameter("@Sectionid", drpsection1.ToString()));
        param.Add(new SqlParameter("@Branchid", drpBranch1.ToString()));
        param.Add(new SqlParameter("@StreamId", drpStream.SelectedValue));
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
        if (percentle < 33)
        {
            return "E";
        }
        else if (percentle >= 33 && percentle <= 40)
        {
            return "D";
        }
        else if (percentle >= 40.1 && percentle <= 50)
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
            //if (drpEval.SelectedValue == "Term1")
            //{
            //    div2.Visible = false;
            //    div3.Visible = false;
            //    div4.Visible = false;
            //    txtDate.CssClass = "form-control-blue datepicker-normal";
            //    txtPromotedtoclass.CssClass = "form-control-blue";
            //    txtSchoolReopenon.CssClass = "form-control-blue datepicker-normal";
            //}
            //if (drpEval.SelectedValue == "Term2")
            //{
            //    div2.Visible = true;
            //    div3.Visible = true;
            //    div4.Visible = true;
            //    txtDate.CssClass = "validatetxt form-control-blue datepicker-normal";
            //    txtPromotedtoclass.CssClass = "validatetxt form-control-blue";
            //    txtSchoolReopenon.CssClass = "validatetxt form-control-blue datepicker-normal";
            //}
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