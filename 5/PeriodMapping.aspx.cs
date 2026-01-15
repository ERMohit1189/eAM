using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class PeriodMapping : Page
{
    string sql = "", _sql = "", Sql = "";
    Campus _oo = new Campus();
    private SqlConnection _con;
    DataTable dt;

    protected void Page_Load(object sender, EventArgs e)
    {
        _con = new SqlConnection();
        _con = _oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);
        if (!IsPostBack)
        {
            sql = " select * from classmaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            _oo.FillDropDown_withValue(sql, ddlClass, "ClassName", "id");
            ddlClass.Items.Insert(0, new ListItem("<--Select-->", ""));
            sql = " select id, Period from TTPeriodMaster where BranchCode=" + Session["BranchCode"] + "";
            _oo.FillDropDown_withValue(sql, ddlPeriod, "Period", "id");
            ddlPeriod.Items.Insert(0, new ListItem("<--Select-->", ""));
            ddlSection.Items.Insert(0, new ListItem("<--Select-->", ""));
            GetTime();
        }
    }
    public void GetTime()
    {
        BLL.FillHourInDropDownlist(ddlFromH);
        BLL.FillMinuteInDropDownlist(ddlFromM);
        BLL.FillHourInDropDownlist(ddlToH);
        BLL.FillMinuteInDropDownlist(ddlToM);
    }
    protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        sql = " select * from sectionmaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and Classnameid="+ddlClass.SelectedValue+"";
        _oo.FillDropDown_withValue(sql, ddlSection, "sectionName", "id");
        ddlSection.Items.Insert(0, new ListItem("<--All-->", ""));
        LoadData();
    }
    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadData();
    }
    public void LoadData()
    {
        sql ="select pm.*, ClassName, SectionName, Period from TTPeriodMapping pm inner join ClassMaster cm on cm.id=pm.Classid and cm.BranchCode=pm.BranchCode and pm.SessionName=cm.SessionName ";
        sql = sql + " inner join sectionmaster sm on sm.id=pm.sectionId and sm.BranchCode=pm.BranchCode  and pm.SessionName=sm.SessionName ";
        sql = sql + " inner join TTPeriodMaster psm on psm.id=pm.Periodid and psm.BranchCode=pm.BranchCode ";
        sql = sql + " where pm.SessionName='" + Session["SessionName"] + "' and pm.BranchCode=" + Session["BranchCode"] + "";
        if (ddlClass.SelectedIndex != 0)
        {
            sql = sql + " and pm.Classid=" + ddlClass.SelectedValue + " ";
        }
        if (ddlSection.SelectedIndex!=0)
        {
            sql = sql + "  and pm.sectionid=" + ddlSection.SelectedValue + " ";
        }
        sql = sql + " order by cm.id, sm.id asc";
        var dt = _oo.Fetchdata(sql);
        gvTimeTableRule.DataSource = dt;
        gvTimeTableRule.DataBind();
        for (int i = 0; i < gvTimeTableRule.Rows.Count; i++)
        {
            Label ClassID = (Label)gvTimeTableRule.Rows[i].FindControl("ClassID");
            Label SectionID = (Label)gvTimeTableRule.Rows[i].FindControl("SectionID");
            Label PeriodID = (Label)gvTimeTableRule.Rows[i].FindControl("PeriodID");
            LinkButton btnDelete = (LinkButton)gvTimeTableRule.Rows[i].FindControl("btnDelete");
            _sql = "SELECT Classid FROM TTPeriodAllotToStaff WHERE Classid='" + ClassID.Text + "' and SectionId='" + SectionID.Text + "' and Periodid='" + PeriodID.Text + "' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "'";
            if (_oo.Duplicate(_sql))
            {
                btnDelete.Text = "<i class='fa fa-lock'></i>";
                btnDelete.Enabled = false;
            }
        }
    }

    protected void btnInsert_Click(object sender, EventArgs e)
    {
        string fromtime = ddlFromH.SelectedValue + ":" + ddlFromM.SelectedValue + " " + ddlFromType.SelectedValue;
        string totime = ddlToH.SelectedValue + ":" + ddlToM.SelectedValue + " " + ddlToType.SelectedValue;
        if (DateTime.Parse(DateTime.Now.ToString("dd-MMM-yyyy") + " " + fromtime) > DateTime.Parse(DateTime.Now.ToString("dd-MMM-yyyy") + " " + totime))
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Invalid time duration!", "A");
            return;
        }


        if (ddlSection.SelectedIndex != 0)
        {
            _sql = "SELECT classid FROM TTPeriodMapping WHERE classid=" + ddlClass.SelectedValue.Trim() + " and sectionid=" + ddlSection.SelectedValue.Trim() + " and Periodid=" + ddlPeriod.SelectedValue.Trim() + " and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "'";
            if (_oo.Duplicate(_sql))
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Duplicate period allotment in same class and section!", "A");
                return;
            }
            _sql = "SELECT classid FROM TTPeriodMapping WHERE classid=" + ddlClass.SelectedValue.Trim() + " and sectionid=" + ddlSection.SelectedValue.Trim() + " and Periodid<>" + ddlPeriod.SelectedValue.Trim() + " and (convert(datetime, '" + fromtime.Trim() + "') between convert(datetime, StartFrom) and convert(datetime, EndTo) or convert(datetime, '" + totime.Trim() + "') between convert(datetime, StartFrom) and convert(datetime, EndTo)) and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "'";
            if (_oo.Duplicate(_sql))
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Duplicate time duration in same class and section!", "A");
                return;
            }
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "TTPeriodMappingProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = _con;
                cmd.Parameters.AddWithValue("@Classid", ddlClass.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@SectionId", ddlSection.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@Periodid", ddlPeriod.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@StartFrom", fromtime.Trim());
                cmd.Parameters.AddWithValue("@EndTo", totime.Trim());
                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                cmd.Parameters.AddWithValue("@Action", "insert");
                try
                {
                    _con.Open();
                    cmd.ExecuteNonQuery();
                    _con.Close();
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Submitted successfully.", "S");
                    LoadData();
                    Reset();
                }
                catch (Exception ex)
                {
                }
            }
        }
        else
        {
            int sts = 0; int duplicate = 0; int duplicatetime = 0;
            for (int i = 1; i < ddlSection.Items.Count; i++)
            {
                string ss = "SELECT classid FROM TTPeriodMapping WHERE classid=" + ddlClass.SelectedValue.Trim() + " and sectionid=" + ddlSection.Items[i].Value.Trim() + " and (convert(date, '" + fromtime.Trim() + "') between convert(date, StartFrom) and convert(date, EndTo) or convert(date, '" + totime.Trim() + "') between convert(date, StartFrom) and convert(date, EndTo)) and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "'";
                _sql = "SELECT classid FROM TTPeriodMapping WHERE classid=" + ddlClass.SelectedValue.Trim() + " and sectionid=" + ddlSection.Items[i].Value.Trim() + " and Periodid=" + ddlPeriod.SelectedValue.Trim() + " and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "'";
                if (_oo.Duplicate(_sql))
                {
                    duplicate = duplicate + 1;
                }
                else if (_oo.Duplicate(ss))
                {
                    duplicatetime = duplicatetime + 1;
                }
                else
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "TTPeriodMappingProc";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = _con;
                        cmd.Parameters.AddWithValue("@Classid", ddlClass.SelectedValue.Trim());
                        cmd.Parameters.AddWithValue("@SectionId", ddlSection.Items[i].Value.Trim());
                        cmd.Parameters.AddWithValue("@Periodid", ddlPeriod.SelectedValue.Trim());
                        cmd.Parameters.AddWithValue("@StartFrom", fromtime.Trim());
                        cmd.Parameters.AddWithValue("@EndTo", totime.Trim());
                        cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                        cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                        cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                        cmd.Parameters.AddWithValue("@Action", "insert");
                        try
                        {
                            _con.Open();
                            cmd.ExecuteNonQuery();
                            _con.Close();
                            cmd.Parameters.Clear();
                            sts = sts + 1;
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
            }
            if (sts > 0 && duplicate == 0 && duplicatetime == 0)
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Submitted successfully.", "S");
                LoadData();
                Reset();
            }
            else
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Some duplicate period or time duration!", "A");
                LoadData();
                Reset();
            }
        }
    }
    public void Reset()
    {
        ddlPeriod.SelectedIndex = 0;
        ddlFromH.SelectedIndex = 0;
        ddlFromM.SelectedIndex = 0;
        ddlToH.SelectedIndex = 0;
        ddlToH.SelectedIndex = 0;
    }

    
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("Label37");
        lblValue.Text = lblId.Text;
        mpeDelete.Show();
        btnNo.Focus();
    }

    protected void btnYes_Click(object sender, EventArgs e)
    {
        DeleteRecord();
    } 


    public void DeleteRecord()
    {
        _sql = "Delete from TTPeriodMapping where Id=" + lblValue.Text + " and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "'";
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = _sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = _con;
            try
            {
                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Deleted successfully.", "S");
                LoadData();
                Reset();
            }
            catch (Exception)
            {
            }
        }
    }

    protected void btnNo_Click(object sender, EventArgs e)
    {

    }

    
}