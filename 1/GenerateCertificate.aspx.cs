using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_GenerateCertificate : System.Web.UI.Page
{
    private SqlConnection _con = new SqlConnection();
    public SqlConnection _con1 = new SqlConnection();
    private readonly Campus _oo = new Campus();
    private string _sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        _con = _oo.dbGet_connection();
        _con1 = _oo.dbGet_connection();
        _oo.AddDateMonthYearDropDown(DDYearTo, DDMonthTo, DDDateTo);
        _oo.FindCurrentDateandSetinDropDown(DDYearTo, DDMonthTo, DDDateTo);
        if (IsPostBack) return;
        lblRedIndicate.Visible = false;
        LoadClass();
        LoadSection();


    }
    protected void DDYear_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void DDMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    protected void DDDate_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    private void LoadClass()
    {
        DrpClass.Items.Clear(); drpSection.Items.Clear();
        _sql = "Select ClassName,Id from ClassMaster Where " +
            "SessionName='" + Session["SessionName"] + "' and " +
            "BranchCode=" + Session["BranchCode"] + " order by CIDOrder asc ";

        _oo.FillDropDown_withValue(_sql, DrpClass, "ClassName", "Id");
        DrpClass.Items.Insert(0, new ListItem("<--Select-->", ""));
        DropBranch.Items.Insert(0, new ListItem("<--Select-->", ""));
        drpSection.Items.Insert(0, new ListItem("<--Select-->", ""));
    }
    private void LoadBranch()
    {
        DropBranch.Items.Clear();
        _sql = "Select BranchName,Id from BranchMaster";
        _sql += " where (ClassId='" + DrpClass.SelectedValue + "' or ClassId is NULL) and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";

        _oo.FillDropDown_withValue(_sql, DropBranch, "BranchName", "Id");
        DropBranch.Items.Insert(0, new ListItem("<--Select-->", ""));
    }
    private void LoadSection()
    {
        drpSection.Items.Clear();
        _sql = "select SectionName, id from SectionMaster where ClassNameId='" + DrpClass.SelectedValue + "'";
        _sql += "  and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        _oo.FillDropDown_withValue(_sql, drpSection, "SectionName", "id");
        drpSection.Items.Insert(0, new ListItem("<--Select-->", ""));
    }
    private void LoadStream()
    {
        DropBranch.Items.Clear();
        _sql = "select id, Stream from StreamMaster where ClassId='" + DrpClass.SelectedValue + "' and BranchId='" + DropBranch.SelectedValue + "'";
        _sql += "  and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        _oo.FillDropDown_withValue(_sql, DropBranch, "Stream", "id");
        DropBranch.Items.Insert(0, new ListItem("<--Select-->", ""));
    }
    protected void DrpClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadBranch();
        LoadSection();


    }
    protected void DropBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    protected void LinkShow_Click(object sender, EventArgs e)
    {
        Loadrecord();
    }
    protected void Loadrecord()
    {
        try
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                string sql = "";
                if (DropDownList2.SelectedValue == "1")
                {
                    sql = "select *,'' as CertificateNo,'0' as Grade,'0' as Live,YEAR(GETDATE()) AS CurrentYear, LEFT(DATENAME(MONTH, GETDATE()), 3) AS CurrentMonth,DAY(GETDATE()) AS CurrentDate    from AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") where ClassId=" + DrpClass.SelectedValue + " and  SectionId=" + drpSection.SelectedValue + " 	and SrNo not in(Select StudentSrNo from StudentCertificate_tb where ClassId=" + DrpClass.SelectedValue + " and  SectionId=" + drpSection.SelectedValue + " and StreamID=" + DropBranch.SelectedValue + "  )";
                    if (DropBranch.SelectedValue != "")
                    {
                        sql += " and BranchId=" + DropBranch.SelectedValue + " ";
                    }
                    if (ddlStudentStatus.SelectedValue == "A")
                    {
                        sql += "AND isnull(Withdrwal,'')='' AND isnull(blocked,'')='' AND Isnull(promotion, '') <> 'Cancelled' ";
                    }
                    if (ddlStudentStatus.SelectedValue == "W")
                    {
                        sql += "AND isnull(Withdrwal,'')='W' AND isnull(blocked,'')='' AND Isnull(promotion, '') <> 'Cancelled' ";
                    }
                    if (ddlStudentStatus.SelectedValue == "AB")
                    {
                        sql += "  AND isnull(Withdrwal,'')='' AND Isnull(promotion, '') <> 'Cancelled'  ";
                    }
                    if (ddlStudentStatus.SelectedValue == "B")
                    {
                        sql += "  AND isnull(Withdrwal,'')='' AND isnull(blocked,'')='Yes' AND Isnull(promotion, '') <> 'Cancelled' ";
                    }
                    //sql += "  and isnull(Promotion,'')='' and isnull(Withdrwal,'')='' ";
                }
                else
                {
                    sql = "select ID,StudentSrNo as SrNO,CertificateNo,Name as Name,FatherName as FatherName, ClassID as ClassId,ClassName as ClassName,SectionID as SectionId,SectionName as SectionName , StreamID as BranchId,StreamName as GroupNa,Grade,IssueDate as Certificatedates,Live,YEAR(cast(IssueDate as datetime)) AS CurrentYear, LEFT(DATENAME(MONTH, cast(IssueDate as datetime)), 3) AS CurrentMonth,DAY(cast(IssueDate as datetime)) AS CurrentDate  from StudentCertificate_tb where SessionName='" + Session["SessionName"] + "' and Branchcode=" + Session["BranchCode"] + " and ClassID=" + DrpClass.SelectedValue + " and SectionID=" + drpSection.SelectedValue + " ";
                    if (DropBranch.SelectedValue != "")
                    {
                        sql += " and StreamID=" + DropBranch.SelectedValue + " ";
                    }
                    // sql += "  and isnull(Promotion,'')='' and isnull(Withdrwal,'')='' ";
                }

                mainGrid.DataSource = _oo.Fetchdata(sql);
                mainGrid.DataBind();
                if (mainGrid.Items.Count > 0)
                {
                    div_grid.Visible = true;
                    lblNoRecord.Text = "";
                    lblNoRecord.Visible = false;
                    LinkSubmit.Visible = true;

                }
                else
                {
                    div_grid.Visible = false;
                    lblNoRecord.Visible = true;
                    LinkSubmit.Visible = false;
                    lblNoRecord.Text = "No record(s) found!";
                }
            }
        }
        catch (Exception)
        {
            div_grid.Visible = false;
            lblNoRecord.Visible = true;
            LinkSubmit.Visible = false;
            lblNoRecord.Text = "No record(s) found!";
        }
    }
    protected void mainGrid_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DropDownList DDYearTo1 = (DropDownList)e.Item.FindControl("DDYearTo1");
            DropDownList DDMonthTo1 = (DropDownList)e.Item.FindControl("DDMonthTo1");
            DropDownList DDDateTo1 = (DropDownList)e.Item.FindControl("DDDateTo1");

            if (DDYearTo1 != null && DDMonthTo1 != null && DDDateTo1 != null)
            {
                _oo.AddDateMonthYearDropDown(DDYearTo1, DDMonthTo1, DDDateTo1);
                _oo.FindCurrentDateandSetinDropDown(DDYearTo1, DDMonthTo1, DDDateTo1);
            }

            // Live (drpstatus) Dropdown Binding
            DropDownList drpStatus = (DropDownList)e.Item.FindControl("drpstatus");
            string liveValue = DataBinder.Eval(e.Item.DataItem, "Live").ToString();

            if (drpStatus != null)
            {
                drpStatus.SelectedValue = liveValue; // Live column ki value select karega
            }

            // Grade (DropDownList1) Dropdown Binding
            DropDownList ddlGrade = (DropDownList)e.Item.FindControl("DropDownList1");
            string gradeValue = DataBinder.Eval(e.Item.DataItem, "Grade").ToString();

            if (ddlGrade != null)
            {
                ddlGrade.SelectedValue = gradeValue; // Grade column ki value select karega
            }

            // DropDownList ddlGrade = (DropDownList)e.Item.FindControl("DropDownList1");
            string YearValue = DataBinder.Eval(e.Item.DataItem, "CurrentYear").ToString();

            if (DDYearTo1 != null)
            {
                DDYearTo1.SelectedValue = YearValue; // Grade column ki value select karega
            }
            string CurrentMonth = DataBinder.Eval(e.Item.DataItem, "CurrentMonth").ToString();

            if (DDMonthTo1 != null)
            {
                DDMonthTo1.SelectedValue = CurrentMonth; // Grade column ki value select karega
            }
            string CurrentDate = DataBinder.Eval(e.Item.DataItem, "CurrentDate").ToString();

            if (DDDateTo1 != null)
            {
                DDDateTo1.SelectedValue = CurrentDate; // Grade column ki value select karega
            }
        }
    }


    //protected void LinkSubmit_Click(object sender, EventArgs e)
    //{

    //}
    protected void LinkSubmit_Click(object sender, EventArgs e)
    {
        _con.Open();
        SqlCommand cmd = new SqlCommand("SELECT ISNULL(MAX(CAST(CertificateNo AS INT)), 10000) FROM StudentCertificate_tb", _con);
        object result = cmd.ExecuteScalar();
        long lastCertificateNo = Convert.ToInt64(result) + 1;
        _con.Close();

        DataTable dt = new DataTable();
        dt.Columns.Add("StudentSrNo", typeof(string));
        dt.Columns.Add("Name", typeof(string));
        dt.Columns.Add("FatherName", typeof(string));
        dt.Columns.Add("ClassID", typeof(int));
        dt.Columns.Add("ClassName", typeof(string));
        dt.Columns.Add("SectionID", typeof(int));
        dt.Columns.Add("SectionName", typeof(string));
        dt.Columns.Add("StreamID", typeof(int));
        dt.Columns.Add("StreamName", typeof(string));
        dt.Columns.Add("CertificateNo", typeof(string));
        dt.Columns.Add("SessionName", typeof(string));
        dt.Columns.Add("Branchcode", typeof(int));
        dt.Columns.Add("Grade", typeof(string));
        dt.Columns.Add("IssueDate", typeof(string));
        dt.Columns.Add("Live", typeof(string));
        dt.Columns.Add("Created_date", typeof(DateTime));
        dt.Columns.Add("CreatedBy", typeof(string));

        foreach (RepeaterItem item in mainGrid.Items)
        {
            CheckBox chkSelect = (CheckBox)item.FindControl("checkboxnew");
            if (chkSelect.Checked)
            {
                string sr = ((Label)item.FindControl("Label1")).Text;
                string certificateNo = DateTime.Now.Ticks.ToString() + sr;
                string Year = ((DropDownList)item.FindControl("DDYearTo1")).SelectedValue;
                string Month = ((DropDownList)item.FindControl("DDMonthTo1")).SelectedValue;
                string Date = ((DropDownList)item.FindControl("DDDateTo1")).SelectedValue;
                string dates = Year + "-" + Month + "-" + Date;
                DataRow row = dt.NewRow();
                row["StudentSrNo"] = ((Label)item.FindControl("lblSrNO")).Text;
                row["Name"] = ((Label)item.FindControl("lblName")).Text;
                row["FatherName"] = ((Label)item.FindControl("lblFatherName")).Text;
                row["ClassID"] = ((Label)item.FindControl("lblclassID")).Text;  // Static class ID (change if needed)
                row["ClassName"] = ((Label)item.FindControl("lblclassname")).Text; // Static class name (change if needed)
                row["SectionID"] = ((Label)item.FindControl("lblsectionID")).Text; // Static section ID (change if needed)
                row["SectionName"] = ((Label)item.FindControl("lblsectionname")).Text; // Static section name (change if needed)
                row["StreamID"] = ((Label)item.FindControl("lblStreamID")).Text;  // Static stream ID (change if needed)
                row["StreamName"] = ((Label)item.FindControl("lblStreamName")).Text;  // Static stream name (change if needed)
                row["CertificateNo"] = lastCertificateNo.ToString(); ; // Dummy certificate number
                row["SessionName"] = Session["SessionName"].ToString();
                row["Branchcode"] = Session["BranchCode"].ToString();
                row["Grade"] = ((DropDownList)item.FindControl("DropDownList1")).SelectedValue;
                row["IssueDate"] = dates;
                row["Live"] = ((DropDownList)item.FindControl("drpstatus")).SelectedValue;
                row["Created_date"] = DateTime.Now;
                row["CreatedBy"] = Session["LoginName"].ToString(); // Change according to login user
                lastCertificateNo++;
                dt.Rows.Add(row);
            }
        }

        if (dt.Rows.Count > 0)
        {
            SaveData(dt);

        }
        else
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Please select at least one row!", "A");
        }
    }

    private void SaveData(DataTable dt)
    {
        _con.Open();
        using (SqlCommand cmd = new SqlCommand("sp_StudentCertificate", _con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param = cmd.Parameters.AddWithValue("@StudentData", dt);
            param.SqlDbType = SqlDbType.Structured;

            try
            {
                cmd.ExecuteNonQuery();
                _con.Close();
                Loadrecord();
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Data saved successfully!", "S");
            }
            catch (Exception ex)
            {
                _con.Close();
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, ex.Message, "A");
            }
        }

    }






}