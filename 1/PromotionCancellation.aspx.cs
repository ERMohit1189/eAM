using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class PromotionCancellation : System.Web.UI.Page
{
    private SqlConnection _con = new SqlConnection();
    private SqlConnection _con1 = new SqlConnection();
    private readonly Campus _oo = new Campus();
    private string _sql = "";
    private string _sql2 = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        _con = _oo.dbGet_connection();
        _con1 = _oo.dbGet_connection();

        Campus camp = new Campus(); camp.LoadLoader(loader);
        if (!IsPostBack)
        {
            LoadClass();
        }
    }

    protected void loadrecord()
    {
        var studentId = Request.Form[hfStudentId.UniqueID];
        if (string.IsNullOrEmpty(studentId))
        {
            studentId = txtSearch.Text.Trim();
        }

        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.Connection = _con;
            cmd.CommandText = "GetCancelPromotion";
            cmd.CommandType = CommandType.StoredProcedure;
            if (rdoType.SelectedValue != "Bulk" && studentId.ToString() != "")
            {
                cmd.Parameters.AddWithValue("@SrNo", studentId);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ClassId", DrpClass.SelectedValue);
                cmd.Parameters.AddWithValue("@BranchId", DropBranch.SelectedValue);
                if (drpStream.SelectedValue != "")
                {
                    cmd.Parameters.AddWithValue("@Stream", drpStream.SelectedValue);
                }
                if (drpGender.SelectedIndex != 0)
                {
                    cmd.Parameters.AddWithValue("@Gender", drpGender.SelectedValue);
                }
                cmd.Parameters.AddWithValue("@SectionName", drpSection.SelectedValue);
            }
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString().Trim());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString().Trim());

            SqlDataAdapter das = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            das.Fill(ds);
            cmd.Parameters.Clear();
            GridView1.DataSource = ds;
            GridView1.DataBind();

            if (GridView1.Rows.Count > 0)
            {
                int cnt = 0;
                LinkSubmit.Visible = true;
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    var lblRemark = (Label)GridView1.Rows[i].FindControl("lblRemark");
                    if (lblRemark.Text.Trim() == "The record has proceeded in this session!")
                    {
                        lblRemark.Style.Add("color", "#FF0000");
                        cnt = cnt + 1;
                    }
                }
                if (GridView1.Rows.Count == cnt)
                {
                    LinkSubmit.Visible = false;
                    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox1, "All students Record has proceeded in new session!", "A");
                    return;
                }
            }
        }
    }

    protected void rdoType_SelectedIndexChanged(object sender, EventArgs e)
    {
        hfStudentId.Value = "";
        txtSearch.Text = "";
        LinkSubmit.Visible = false;
        GridView1.DataSource = null;
        GridView1.DataBind();
        if (rdoType.SelectedValue == "Bulk")
        {
            SingleDetail.Visible = false;
            fullDetail.Visible = true;
        }
        else
        {
            SingleDetail.Visible = true;
            fullDetail.Visible = false;
        }
    }

    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {
        loadrecord();
    }
    protected void LinkShow_Click(object sender, EventArgs e)
    {
        loadrecord();
    }

    private void LoadClass()
    {
        DrpClass.Items.Clear(); drpSection.Items.Clear(); drpStream.Items.Clear();
        _sql = "Select ClassName,Id from ClassMaster Where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"] + " order by CIDOrder asc ";

        _oo.FillDropDown_withValue(_sql, DrpClass, "ClassName", "Id");
        DrpClass.Items.Insert(0, new ListItem("<--Select-->", ""));
        DropBranch.Items.Insert(0, new ListItem("<--Select-->", ""));
        drpSection.Items.Insert(0, new ListItem("<--Select-->", ""));
        drpStream.Items.Insert(0, new ListItem("<--Select-->", ""));
    }
    private void LoadBranch()
    {
        DropBranch.Items.Clear();
        _sql = "Select BranchName,Id from BranchMaster";
        _sql +=  " where (ClassId='" + DrpClass.SelectedValue + "' or ClassId is NULL) and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";

        _oo.FillDropDown_withValue(_sql, DropBranch, "BranchName", "Id");
        DropBranch.Items.Insert(0, new ListItem("<--Select-->", ""));
    }
    private void LoadSection()
    {
        drpSection.Items.Clear();
        _sql = "select SectionName from SectionMaster where ClassNameId='" + DrpClass.SelectedValue + "'";
        _sql +=  "  and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        _oo.FillDropDown(_sql, drpSection, "SectionName");
        drpSection.Items.Insert(0, new ListItem("<--Select-->", ""));
    }
    private void LoadStream()
    {
        drpStream.Items.Clear();
        _sql = "select id, Stream from StreamMaster where ClassId='" + DrpClass.SelectedValue + "' and BranchId='" + DropBranch.SelectedValue + "'";
        _sql +=  "  and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        _oo.FillDropDown_withValue(_sql, drpStream, "Stream", "id");
        drpStream.Items.Insert(0, new ListItem("<--Select-->", ""));
    }
    protected void DrpClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadBranch();
        LoadSection();


    }
    protected void DropBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadStream();
    }


    protected void LinkSubmit_Click(object sender, EventArgs e)
    {
        int count = 0;
        for (int m = 0; m < GridView1.Rows.Count; m++)
        {
            var lblRemark = (Label)GridView1.Rows[m].FindControl("lblRemark");
            if (lblRemark.Text.Trim() != "Record has proceeded in new session!")
            { count = count + 1; }
        }
        int Canclled = 0;
        int NotCanclled = 0;
        if (count == 0)
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox1, "All students Record has proceeded in new session!", "A");
            return;
        }
        else
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    var lblRemark = (Label)GridView1.Rows[i].FindControl("lblRemark");
                    var LblSrNO = (Label)GridView1.Rows[i].FindControl("LblSrNO");
                    string sqls = "select id from CompositFeeDeposit where SrNo=@SrNO and SessionName=@SessionName and BranchCode=@BranchCode and isnull(ReceiptNo,'')<>''";
                    if (_oo.Duplicate(sqls))
                    {
                        NotCanclled = NotCanclled + 1;
                    }
                    else
                    {
                        if (lblRemark.Text != "The record has proceeded in this session!")
                        {
                            cmd.CommandText = "SetCancelPromotion";
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Connection = _con;
                            cmd.Parameters.AddWithValue("@SrNO", LblSrNO.Text);
                            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                            try
                            {
                                _con.Open();
                                cmd.ExecuteNonQuery();
                                cmd.Parameters.Clear();
                                Canclled = Canclled + 1;
                                _con.Close();
                            }
                            catch (Exception ex) { }
                        }
                    }
                }
                loadrecord();
                if (Canclled> 0 && NotCanclled == 0)
                {
                    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox1, "Promotion cancelled successfully.", "S");
                }
                else if (Canclled >= 0 && NotCanclled > 0)
                {
                    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox1, "Student(s) promotion not cancelled due to fee receipt generated!", "A");
                }
            }

        }
    }
}