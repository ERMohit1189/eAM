using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class HealthMaster : Page
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
        if (!Page.IsPostBack)
        {
            LoadClass();
            ddlBranch.Items.Insert(0, new ListItem("<--Select-->", ""));
            ddlSection.Items.Insert(0, new ListItem("<--Select-->", ""));
        }
    }
    public void LoadClass()
    {
        sql = "Select Distinct ClassName,cm.Id as Id,CIDOrder from ClassMaster cm ";
        sql = sql + " inner join dt_ClassGroupMaster t1 on t1.ClassId=cm.Id";
        sql = sql + " where cm.SessionName='" + Session["SessionName"] + "' and cm.BranchCode=" + Session["BranchCode"] + " and t1.SessionName='" + Session["SessionName"] + "' and t1.BranchCode=" + Session["BranchCode"] + " Order by CIDOrder";
        _oo.FillDropDown_withValue(sql, ddlClass, "ClassName", "Id");
        ddlClass.Items.Insert(0, new ListItem("<--Select-->", ""));
        
    }
    protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        sql = "Select BranchName,Id from BranchMaster where ClassId=" + ddlClass.SelectedValue.ToString() + "";
        sql = sql + " and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"].ToString() + "'";
        _oo.FillDropDown_withValue(sql, ddlBranch, "BranchName", "Id");
        ddlBranch.Items.Insert(0, new ListItem("<--Select-->", ""));
        Grid.DataSource = null;
        Grid.DataBind();
        ddlEval.SelectedIndex = 0;
        ddlSection.Items.Clear();
        ddlSection.Items.Insert(0, new ListItem("<--Select-->", ""));
        btnInserts.Visible = false;
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSection.Items.Clear();
        sql = "Select SectionName,Id from SectionMaster where ClassNameId=" + ddlClass.SelectedValue.ToString() + "";
        sql = sql + " and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"].ToString() + "'";
        _oo.FillDropDown_withValue(sql, ddlSection, "SectionName", "Id");
        ddlSection.Items.Insert(0, new ListItem("<--Select-->", ""));
        Grid.DataSource = null;
        Grid.DataBind();
        ddlEval.SelectedIndex = 0;
        btnInserts.Visible = false;
    }
    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        Grid.DataSource = null;
        Grid.DataBind();
        ddlEval.SelectedIndex = 0;
        btnInserts.Visible = false;
    }
    protected void ddlEval_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnInserts.Visible = false;
        LoadData();
    }
    public void LoadData()
    {
        Grid.DataSource = null;
        Grid.DataBind();
        sql = "select asr.SrNo, Name, CombineClassName, HeightInCm, WeightInKg from AllStudentRecord_UDF('" + Session["SessionName"] + "', " + Session["BranchCode"] + ") asr ";
        sql = sql + " left join HealthMaster h on h.Srno=asr.SrNo and h.BranchCode=asr.BranchCode and h.SessionName=asr.SessionName and h.TermName='" + ddlEval.SelectedValue + "' ";
        sql = sql + " where asr.Classid=" + ddlClass.SelectedValue + " and asr.BranchId='" + ddlBranch.SelectedValue + "' ";
        if (ddlSection.SelectedIndex!=0)
        {
            sql = sql + " and asr.SectionId=" + ddlSection.SelectedValue + " ";
        }
        sql = sql + " order by name asc";

        var dt = _oo.Fetchdata(sql);
        Grid.DataSource = dt;
        Grid.DataBind();
        if (dt.Rows.Count > 0)
        {
            btnInserts.Visible = true;
        }
        else
        {
            btnInserts.Visible = false;
        }
    }

    protected void btnInserts_Click(object sender, EventArgs e)
    {
        int res = 0;
        for (int i = 0; i < Grid.Rows.Count; i++)
        {
            Label Srno = (Label)Grid.Rows[i].FindControl("lblSrno");
            TextBox txtHeightInCm = (TextBox)Grid.Rows[i].FindControl("txtHeightInCm");
            TextBox txtWeightInKg = (TextBox)Grid.Rows[i].FindControl("txtWeightInKg");
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "HealthMasterProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = _con;
                cmd.Parameters.AddWithValue("@SrNo", Srno.Text.Trim());
                cmd.Parameters.AddWithValue("@Classid", ddlClass.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@BranchId", ddlBranch.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@TermName", ddlEval.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@HeightInCm", txtHeightInCm.Text.Trim());
                cmd.Parameters.AddWithValue("@WeightInKg", txtWeightInKg.Text.Trim());
                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                try
                {
                    _con.Open();
                    cmd.ExecuteNonQuery();
                    _con.Close();
                    res = res + 1;
                }
                catch (Exception ex)
                {
                }
            }
        }
        if (res>0)
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Submitted successfully.", "S");
            LoadData();
        }
    }



    
}