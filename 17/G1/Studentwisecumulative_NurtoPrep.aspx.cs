using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Linq;
using System.Web.UI.DataVisualization.Charting;
public partial class common_G2_Studentwisecumulative_NurtoPrep : Page
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
                    icons.Visible = true;
                    rptStudent.DataSource = dt;
                    rptStudent.DataBind();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        HtmlControl header = (HtmlControl)rptStudent.Items[i].FindControl("header");
                        BLL.BLLInstance.LoadReportCardHeader("Examination", header);
                        Repeater rptmarks = (Repeater)rptStudent.Items[i].FindControl("rptmarks");
                        string sql = "select SubjectName, id from TTSubjectMaster where Medium = 'English' and IsAditional = 0 and Classid =" + drpclass.SelectedValue + " and BranchId =" + dt.Rows[i]["BranchId"].ToString() + " and SessionName = '" + Session["SessionName"] + "' and BranchCode =" + Session["BranchCode"] + "";
                        var dt1 = _oo.Fetchdata(sql);
                        if (dt1.Rows.Count > 0)
                        {
                            rptmarks.DataSource = dt1;
                            rptmarks.DataBind();
                            for (int j = 0; j < dt1.Rows.Count; j++)
                            {

                                Label Sno1 = (Label)rptmarks.Items[j].FindControl("Sno1");
                                Sno1.Text = alpha[j].ToString();
                                Repeater rptmarksT2 = (Repeater)rptmarks.Items[j].FindControl("rptmarksT2");
                                string sql1 = "select PaperName, id from TTPaperMaster where Medium = 'English' and Classid =" + drpclass.SelectedValue + " and BranchId =" + dt.Rows[i]["BranchId"].ToString() + " and SessionName = '" + Session["SessionName"] + "' and BranchCode =" + Session["BranchCode"] + " and subjectid=" + dt1.Rows[j]["id"].ToString() + "";
                                var dt2 = _oo.Fetchdata(sql1);
                                if (dt2.Rows.Count > 0)
                                {
                                    rptmarksT2.DataSource = dt2;
                                    rptmarksT2.DataBind();
                                    for (int k = 0; k < dt2.Rows.Count; k++)
                                    {
                                        Label Sno2 = (Label)rptmarksT2.Items[k].FindControl("Sno2");
                                        Sno2.Text = ToRomanNumeral(k + 1).ToString();

                                        Repeater rptmarksT3 = (Repeater)rptmarksT2.Items[k].FindControl("rptmarksT3");
                                        string sql2 = " select ActivityName,mg1.MaxMarks1, mg1.MaxMarks2, mg1.MaxMarks3, mg2.MaxMarks1 MaxMarks1_2, mg2.MaxMarks2 MaxMarks2_2, mg2.MaxMarks3 MaxMarks3_2, ";
                                        sql2 = sql2 + " g.Evel1, g.Evel2, g.Evel3, g.Best2, g.Conversion, g.Grade, g2.Evel1 Evel1_2, g2.Evel2 Evel2_2, g2.Evel3 Evel3_2, g2.Best2 Best2_2, g2.Conversion Conversion_2, g2.Grade Grade_2 from TTActivityMaster a ";
                                        sql2 = sql2 + " left join CCENurtoPrep_Grade g on a.ClassId=g.ClassId and a.SubjectId=g.SubjectId and a.PaperId=g.PaperId and a.id=g.ActivityId ";
                                        sql2 = sql2 + " and a.BranchCode=g.BranchCode and a.SessionName=g.SessionName and g.Evaluation= 'Term1' and g.SrNo='" + dt.Rows[i]["admissionNo"].ToString() + "' ";
                                        sql2 = sql2 + " left join CCENurtoPrep_Grade g2 on a.ClassId=g2.ClassId and a.SubjectId=g2.SubjectId and a.PaperId=g2.PaperId and a.id=g2.ActivityId ";
                                        sql2 = sql2 + " and a.BranchCode=g2.BranchCode and a.SessionName=g2.SessionName and g2.Evaluation= 'Term2' and g2.SrNo='" + dt.Rows[i]["admissionNo"].ToString() + "' ";
                                        sql2 = sql2 + " left join SetMaxMinMarks_NurtoPrep_Grade mg1 on a.ClassId=mg1.ClassId and a.SubjectId=mg1.SubjectId and a.PaperId=mg1.PaperId and a.id=mg1.ActivityId ";
                                        sql2 = sql2 + " and a.BranchCode=mg1.BranchCode and a.SessionName=mg1.SessionName and mg1.Evaluation= 'Term1' ";
                                        sql2 = sql2 + " left join SetMaxMinMarks_NurtoPrep_Grade mg2 on a.ClassId=mg2.ClassId and a.SubjectId=mg2.SubjectId and a.PaperId=mg2.PaperId and a.id=mg2.ActivityId ";
                                        sql2 = sql2 + " and a.BranchCode=mg2.BranchCode and a.SessionName=mg2.SessionName and mg2.Evaluation= 'Term2' ";
                                        sql2 = sql2 + " where a.Medium='English' and a.Classid=" + drpclass.SelectedValue + " and a.BranchId=" + dt.Rows[i]["BranchId"].ToString() + " and a.SessionName='" + Session["SessionName"] + "' and a.BranchCode=" + Session["BranchCode"] + " and a.subjectid=" + dt1.Rows[j]["id"].ToString() + " and a.Paperid=" + dt2.Rows[k]["id"].ToString() + "";
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
    
}