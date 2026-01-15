using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Linq;
using System.Web.UI.DataVisualization.Charting;

public partial class common_G1_ReportCard_NurtoPrep_1718 : Page
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
                _sql +=  " where cm.SessionName='" + Session["SessionName"] + "' and t1.SessionName='" + Session["SessionName"] + "'  and t1.BranchCode=" + Session["BranchCode"] + " and GroupId='G1' Order by CIDOrder";
                _oo.FillDropDown_withValue(_sql, drpclass, "ClassName", "id");
            }
            else
            {
                _sql = "Select Distinct ClassName,cm.Id,CIDOrder from ClassTeacherMaster ctm";
                _sql +=  " inner join ClassMaster cm on cm.Id=ctm.ClassId and cm.SessionName=ctm.SessionName and cm.BranchCode=ctm.BranchCode";
                _sql +=  " where EmpCode='" + Session["LoginName"].ToString() + "' ";
                _sql +=  " and ctm.SessionName='" + Session["SessionName"].ToString() + "' and ctm.BranchCode = " + Session["BranchCode"] + " ";
                _sql +=  " and cm.id in(select ClassId from dt_ClassGroupMaster where GroupId='G1' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + ")";
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

    char[] alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

    public static List<string> romanNumerals = new List<string>() { "x", "ix", "viii", "vii", "vi", "v", "iv", "iii", "ii", "i" };
    public static List<int> numerals = new List<int>() { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };

    public static string ToRomanNumeral(int number)
    {
        var romanNumeral = string.Empty;
        while (number > 0)
        {
            // find biggest numeral that is less than equal to number
            var index = numerals.FindIndex(x => x <= number);
            // subtract it's value from your number
            number -= numerals[index];
            // tack it onto the end of your roman numeral
            romanNumeral += romanNumerals[index];
        }
        return romanNumeral;
    }
    public void LoadReportCard()
    {

        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = BAL.objBal.dbGet_connection().ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = "ReportCard_NurtoPrep_Grade";
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
                        Repeater rptmarks = (Repeater)rptStudent.Items[i].FindControl("rptmarks");
                        string sql = "select SubjectName, id from TTSubjectMaster where Medium = 'English' and IsAditional = 0 and Classid ="+ drpclass.SelectedValue + " and BranchId ="+ dt.Rows[i]["BranchId"].ToString() + " and SessionName = '"+ Session["SessionName"] + "' and BranchCode ="+ Session["BranchCode"] + "";
                        var dt1 = _oo.Fetchdata(sql);
                        if (dt1.Rows.Count>0)
                        {
                            rptmarks.DataSource = dt1;
                            rptmarks.DataBind();
                            for (int j = 0; j < dt1.Rows.Count; j++)
                            {
                                
                                Label Sno1 = (Label)rptmarks.Items[j].FindControl("Sno1");
                                Sno1.Text = alpha[j].ToString();
                                Repeater rptmarksT2 = (Repeater)rptmarks.Items[j].FindControl("rptmarksT2");
                                string sql1 = "select PaperName, id from TTPaperMaster where Medium = 'English' and Classid =" + drpclass.SelectedValue + " and BranchId =" + dt.Rows[i]["BranchId"].ToString() + " and SessionName = '" + Session["SessionName"] + "' and BranchCode =" + Session["BranchCode"] + " and subjectid="+ dt1.Rows[j]["id"].ToString() + "";
                                var dt2 = _oo.Fetchdata(sql1);
                                if (dt2.Rows.Count > 0)
                                {
                                    rptmarksT2.DataSource = dt2;
                                    rptmarksT2.DataBind();
                                    for (int k = 0; k < dt2.Rows.Count; k++)
                                    {
                                        Label Sno2 = (Label)rptmarksT2.Items[k].FindControl("Sno2");
                                        Sno2.Text = ToRomanNumeral(k+1).ToString();
                                        
                                        Repeater rptmarksT3 = (Repeater)rptmarksT2.Items[k].FindControl("rptmarksT3");
                                        string sql2 = " select ActivityName,g.Grade Grage1, g2.Grade Grade2, '"+drpEval.SelectedValue+"' Evals from TTActivityMaster a ";
                                        sql2 = sql2 + " left join CCENurtoPrep_Grade g on a.ClassId=g.ClassId and a.SubjectId=g.SubjectId and a.PaperId=g.PaperId and a.id=g.ActivityId ";
                                        sql2 = sql2 + " and a.BranchCode=g.BranchCode and a.SessionName=g.SessionName and g.Evaluation= 'Term1' and g.SrNo='" + dt.Rows[i]["admissionNo"].ToString() + "' ";
                                        sql2 = sql2 + " left join CCENurtoPrep_Grade g2 on a.ClassId=g2.ClassId and a.SubjectId=g2.SubjectId and a.PaperId=g2.PaperId and a.id=g2.ActivityId ";
                                        sql2 = sql2 + " and a.BranchCode=g2.BranchCode and a.SessionName=g2.SessionName and g2.Evaluation= 'Term2' and g2.SrNo='"+ dt.Rows[i]["admissionNo"].ToString() + "' ";
                                        sql2 = sql2 + " where a.Medium='English' and a.Classid=" + drpclass.SelectedValue + " and a.BranchId=" + dt.Rows[i]["BranchId"].ToString() + " and a.SessionName='" + Session["SessionName"] + "' and a.BranchCode=" + Session["BranchCode"] + " and a.subjectid="+ dt1.Rows[j]["id"].ToString() + " and a.Paperid="+ dt2.Rows[k]["id"].ToString() + "";
                                        var dt3 = _oo.Fetchdata(sql2);
                                        if (dt3.Rows.Count > 0)
                                        {
                                            rptmarksT3.DataSource = dt3;
                                            rptmarksT3.DataBind();
                                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "term2td();", true);
                                        }
                                    }
                                }
                            }
                        }
                        

                        if (drpEval.SelectedValue.ToUpper() == "TERM1")
                        {
                            Label lblHeightTerm1 = (Label)rptStudent.Items[i].FindControl("lblHeightTerm1");
                            Label lblWeightTerm1 = (Label)rptStudent.Items[i].FindControl("lblWeightTerm1");
                            string h = "select HeightInCm, WeightInKg from HealthMaster where srno='"+ dt.Rows[i]["admissionNo"].ToString() + "' ";
                            h = h + " and TermName='Term1' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                            lblHeightTerm1.Text = _oo.ReturnTag(h, "HeightInCm");
                            lblWeightTerm1.Text = _oo.ReturnTag(h, "WeightInKg");
                        }
                        if (drpEval.SelectedValue.ToUpper() == "TERM2")
                        {
                            Label lblHeightTerm1 = (Label)rptStudent.Items[i].FindControl("lblHeightTerm1");
                            Label lblWeightTerm1 = (Label)rptStudent.Items[i].FindControl("lblWeightTerm1");
                            string h = "select HeightInCm, WeightInKg from HealthMaster where srno='" + dt.Rows[i]["admissionNo"].ToString() + "' ";
                            h = h + " and TermName='Term1' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                            lblHeightTerm1.Text = _oo.ReturnTag(h, "HeightInCm");
                            lblWeightTerm1.Text = _oo.ReturnTag(h, "WeightInKg");

                            Label lblHeightTerm2 = (Label)rptStudent.Items[i].FindControl("lblHeightTerm2");
                            Label lblWeightTerm2 = (Label)rptStudent.Items[i].FindControl("lblWeightTerm2");
                            string h1 = "select HeightInCm, WeightInKg from HealthMaster where srno='" + dt.Rows[i]["admissionNo"].ToString() + "' ";
                            h1 = h1 + " and TermName='Term2' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                            lblHeightTerm2.Text = _oo.ReturnTag(h1, "HeightInCm");
                            lblWeightTerm2.Text = _oo.ReturnTag(h1, "WeightInKg");
                        }

                        cmd.CommandText = "ReportCard_NurtoPrep_Grade";
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
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                icons.Visible = true;
                                HtmlControl header = (HtmlControl)rptStudent.Items[i].FindControl("header");
                                BLL.BLLInstance.LoadReportCardHeader("Result", header);
                                
                                DataTable dtAtt = new DataTable();
                                dtAtt.Columns.Add("t1Att", typeof(string));
                                dtAtt.Columns.Add("t2Att", typeof(string));
                                if (drpEval.SelectedValue.ToUpper() == "TERM1")
                                {
                                    DataRow dr = dtAtt.NewRow();

                                    double totaldays = 0, attendence = 0, percent = 0; string t1Att = "";
                                    totaldays = double.Parse(ds.Tables[0].Rows[0]["totaldays"].ToString().Trim() == "" ? "0" : ds.Tables[0].Rows[0]["totaldays"].ToString().Trim());
                                    attendence = double.Parse(ds.Tables[0].Rows[0]["attendence"].ToString().Trim() == "" ? "0" : ds.Tables[0].Rows[0]["attendence"].ToString().Trim());
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
                                    totaldays1 = double.Parse(ds.Tables[0].Rows[0]["totaldays"].ToString().Trim() == "" ? "0" : ds.Tables[0].Rows[0]["totaldays"].ToString().Trim());
                                    attendence1 = double.Parse(ds.Tables[0].Rows[0]["attendence"].ToString().Trim() == "" ? "0" : ds.Tables[0].Rows[0]["attendence"].ToString().Trim());

                                    if (attendence1 != 0 && totaldays1 != 0)
                                    {
                                        percent1 = (attendence1 * 100) / totaldays1;
                                        t1Att = "<span >" + attendence1.ToString().Trim() + "</span>/<span >" + totaldays1.ToString().Trim() + "</span> (<span >" + percent1.ToString("0.00") + "</span> %)";
                                    }
                                    double totaldays2 = 0, attendence2 = 0, percent2 = 0;
                                    totaldays2 = double.Parse(ds.Tables[1].Rows[0]["totaldays"].ToString().Trim() == "" ? "0" : ds.Tables[1].Rows[0]["totaldays"].ToString().Trim());
                                    attendence2 = double.Parse(ds.Tables[1].Rows[0]["attendence"].ToString().Trim() == "" ? "0" : ds.Tables[1].Rows[0]["attendence"].ToString().Trim());

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
                                if (ds.Tables[2].Rows.Count > 0)
                                {
                                    if (drpEval.SelectedValue.ToUpper() == "TERM1")
                                    {
                                        Repeater rptRemark1 = (Repeater)rptStudent.Items[i].FindControl("rptRemark1");
                                        rptRemark1.DataSource = ds.Tables[2];
                                        rptRemark1.DataBind();
                                    }
                                    if (drpEval.SelectedValue.ToUpper() == "TERM2")
                                    {
                                        Repeater rptRemark2 = (Repeater)rptStudent.Items[i].FindControl("rptRemark2");
                                        rptRemark2.DataSource = ds.Tables[2];
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

                                //if (ds.Tables[2].Rows.Count > 0)
                                //{
                                    string co = "select CoscholasticGroup from CoscholasticGroupMaster where BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and Classid=" + drpclass.SelectedValue + "";
                                    var dtCo = _oo.Fetchdata(co);

                                    if (drpEval.SelectedValue.ToUpper() == "TERM1")
                                    {
                                        Repeater rptSkil1 = (Repeater)rptStudent.Items[i].FindControl("rptSkil1");
                                        rptSkil1.DataSource = dtCo;
                                        rptSkil1.DataBind();
                                        for (int t = 0; t < dtCo.Rows.Count; t++)
                                        {
                                            Label Sno1 = (Label)rptSkil1.Items[t].FindControl("Sno1");
                                            Sno1.Text = alpha[t].ToString();
                                            string cod = "Select distinct cm.id, CoscholasticName as CoscholasticName_1, Grade as Grade_1 from CoscholasticMaster cm ";
                                            cod = cod + " inner join CCENurtoPrepCoscholastic on CCENurtoPrepCoscholastic.CoscholasticId=cm.id ";
                                            cod = cod + " inner join CoscholasticGroupMaster cgm on cgm.Id = cm.GroupId and cm.ClassId=cgm.Classid and cgm.SessionName = cm.SessionName ";
                                            cod = cod + " where cm.SessionName = '" + Session["SessionName"] + "' and cm.ClassId = " + drpclass.SelectedValue + "  and cm.BranchCode=" + Session["BranchCode"] + " ";
                                            cod = cod + " and SrNo='" + dt.Rows[i]["admissionNo"].ToString() + "' and  CoscholasticGroup='" + dtCo.Rows[t]["CoscholasticGroup"].ToString() + "' ";
                                            cod = cod + " and Evaluation=isnull('TERM1', Evaluation) and PartId=1 ";
                                            cod = cod + " order by cm.id asc ";
                                            var dtCod = _oo.Fetchdata(cod);
                                            Repeater rptSkil1A = (Repeater)rptSkil1.Items[t].FindControl("rptSkil1A");
                                            rptSkil1A.DataSource = dtCod;
                                            rptSkil1A.DataBind();
                                            for (int g = 0; g < dtCod.Rows.Count; g++)
                                            {
                                                Label Sno2 = (Label)rptSkil1A.Items[g].FindControl("Sno2");
                                                Sno2.Text = ToRomanNumeral(g + 1).ToString();
                                            }
                                        }

                                    }
                                    if (drpEval.SelectedValue.ToUpper() == "TERM2")
                                    {
                                        Repeater rptSkil2 = (Repeater)rptStudent.Items[i].FindControl("rptSkil2");
                                        rptSkil2.DataSource = dtCo;
                                        rptSkil2.DataBind();
                                        for (int t = 0; t < dtCo.Rows.Count; t++)
                                        {
                                            Label Sno1 = (Label)rptSkil2.Items[t].FindControl("Sno1");
                                            Sno1.Text = alpha[t].ToString();
                                            string cod = "select  * from  ";
                                            cod = cod + " ( Select  distinct cm.id as id_1, CoscholasticName as CoscholasticName_1, Grade as Grade_1 from CoscholasticMaster cm ";
                                            cod = cod + " inner join CCENurtoPrepCoscholastic on CCENurtoPrepCoscholastic.CoscholasticId=cm.id ";
                                            cod = cod + " inner join CoscholasticGroupMaster cgm on cgm.Id = cm.GroupId and cm.ClassId=cgm.Classid and cgm.SessionName = cm.SessionName ";
                                            cod = cod + " where cm.SessionName = '" + Session["SessionName"] + "' and cm.ClassId = " + drpclass.SelectedValue + "  and cm.BranchCode=" + Session["BranchCode"] + " ";
                                            cod = cod + " and SrNo='" + dt.Rows[i]["admissionNo"].ToString() + "' and  CoscholasticGroup='" + dtCo.Rows[t]["CoscholasticGroup"].ToString() + "' ";
                                            cod = cod + " and Evaluation=isnull('TERM1', Evaluation)  ";
                                            cod = cod + " and PartId=1) T1 ";
                                            cod = cod + " left join (Select  distinct cm.id as id_2, CoscholasticName as CoscholasticName_2, Grade as Grade_2 from CoscholasticMaster cm ";
                                            cod = cod + " inner join CCENurtoPrepCoscholastic on CCENurtoPrepCoscholastic.CoscholasticId=cm.id ";
                                            cod = cod + " inner join CoscholasticGroupMaster cgm on cgm.Id = cm.GroupId and cm.ClassId=cgm.Classid and cgm.SessionName = cm.SessionName ";
                                            cod = cod + " where cm.SessionName = '" + Session["SessionName"] + "' and cm.ClassId = " + drpclass.SelectedValue + "  and cm.BranchCode=" + Session["BranchCode"] + " ";
                                            cod = cod + " and SrNo='" + dt.Rows[i]["admissionNo"].ToString() + "' and  CoscholasticGroup='" + dtCo.Rows[t]["CoscholasticGroup"].ToString() + "' ";
                                            cod = cod + " and Evaluation=isnull('TERM2', Evaluation)  ";
                                            cod = cod + " and PartId=1) T2 ";
                                            cod = cod + " on T1.id_1=T2.id_2 order by id_1 asc ";
                                            var dtCod = _oo.Fetchdata(cod);
                                            Repeater rptSkil2A = (Repeater)rptSkil2.Items[t].FindControl("rptSkil2A");
                                            rptSkil2A.DataSource = dtCod;
                                            rptSkil2A.DataBind();
                                            for (int g = 0; g < dtCod.Rows.Count; g++)
                                            {
                                                Label Sno2 = (Label)rptSkil2A.Items[g].FindControl("Sno2");
                                                Sno2.Text = ToRomanNumeral(g + 1).ToString();
                                            }

                                        }
                                    }
                                    
                                }
                            //}
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
        columns.Add("Avg. Marks");
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
        myChart.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor =System.Drawing.ColorTranslator.FromHtml("#d7d7d7");
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
        if (percentle <=39)
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