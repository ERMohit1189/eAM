using System;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


public partial class HostelDueDipositReport : Page
    {
        private SqlConnection _con = new SqlConnection();
        private readonly Campus _oo = new Campus();
        private string _sql = string.Empty;
        private string sql = string.Empty;
    
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
            {
                Response.Redirect("default.aspx");
            }

            _con = _oo.dbGet_connection();
            Campus camp = new Campus(); camp.LoadLoader(loaders);  //in cs file

            header1.Controls.Clear();
            BLL.BLLInstance.LoadHeader("Report", header1);
            if (!IsPostBack)
            {
                ImageButton1.Visible = false;
                ImageButton2.Visible = false;
                ImageButton3.Visible = false;
                ImageButton4.Visible = false;
                Label1.Visible = false;
                _oo.AddDateMonthYearDropDown(FromYY, FromMM, FromDD);
                _oo.AddDateMonthYearDropDown(ToYY, ToMM, ToDD);
                _oo.FindCurrentDateandSetinDropDown(FromYY, FromMM, FromDD);
                _oo.FindCurrentDateandSetinDropDown(ToYY, ToMM, ToDD);
                lblDate.Visible = false;
                lblDate1.Visible = false;

                lblTitle.Text = "from ";
                lblTitle.Visible = false;

                lblTitle1.Text = "to ";
                lblTitle1.Visible = false;

               
                abc.Visible = false;
                LoadUser();
                LoadSession();
            }
        }
        public void LoadSession()
        {
            _sql = "Select 'All' SessionName,'-1' Id Union Select SessionName,SessionId Id from SessionMaster where BranchCode=" + Session["BranchCode"].ToString() + " order by Id";
            _oo.FillDropDown_withValue(_sql, drpSession, "SessionName", "Id");
            drpSession.SelectedValue = drpSession.Items.FindByText(Session["SessionName"].ToString()).Value;
        }

    
        protected void LoadUser()
        {
            _sql = "Select UserId From NewAdminInformation";
            _oo.FillDropDownWithOutSelect(_sql, drpUsers, "UserId");
            drpUsers.Items.Insert(0, "All");
        }
    protected void rdoType_SelectedIndexChanged(object sender, EventArgs e)
    {
        hfStaffId.Value = "";
        hfStudentId.Value = "";
        txtSearchStaff.Text = "";
        txtSearchStudent.Text = "";
        if (rdoType.SelectedIndex == 0)
        {
            divStaff.Visible = false;
            divStudent.Visible = true;
        }
        else
        {
            divStaff.Visible = true;
            divStudent.Visible = false;
        }
    }
    protected void txtSearchStudent_TextChanged(object sender, EventArgs e)
    {
        var studentId = Request.Form[hfStudentId.UniqueID];
        if (string.IsNullOrEmpty(studentId))
        {
            studentId = txtSearchStudent.Text.Trim();
        }
        Bind(studentId);
    }
    protected void txtSearchStaff_TextChanged(object sender, EventArgs e)
    {
        var StaffId = Request.Form[hfStaffId.UniqueID];
        if (string.IsNullOrEmpty(StaffId))
        {
            StaffId = txtSearchStaff.Text.Trim();
        }
        Bind(StaffId);
    }
    protected void lnkView_Click(object sender, EventArgs e)
    {
        Bind("");
    }
    protected void Bind(string txtSearch)
    {
        if (txtSearch == "")
        {
            hfStudentId.Value = "";
            txtSearchStudent.Text = "";
            hfStaffId.Value = "";
            txtSearchStaff.Text = "";
        }

        DataSet DS = new DataSet();
        string fromDate = (FromDD.SelectedValue + " " + FromMM.SelectedValue + " " + FromYY.SelectedValue).ToString();
        string toDate = (ToDD.SelectedValue + " " + ToMM.SelectedValue + " " + ToYY.SelectedValue).ToString();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "HostelFeeDepositProc";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = _con;
        if (txtSearch!="")
        {
            cmd.Parameters.AddWithValue("@SrNoOrEmpid", txtSearch.Trim());
        }
        cmd.Parameters.AddWithValue("@SessionName", drpSession.SelectedItem.Text.Trim());
        cmd.Parameters.AddWithValue("@FromDate", fromDate);
        cmd.Parameters.AddWithValue("@ToDate", toDate);
        cmd.Parameters.AddWithValue("@UserType", rdoType.SelectedValue);
        cmd.Parameters.AddWithValue("@action", "SelectDue");
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(DS);
        if (DS != null && DS.Tables[0].Rows.Count > 0)
        {
            ImageButton1.Visible = false;
            ImageButton2.Visible = true;
            ImageButton3.Visible = false;
            ImageButton4.Visible = true;
            abc.Visible = true;
            GridOneTime.DataSource =DS;
            GridOneTime.DataBind();

            double TotalDue = 0;
            Label lblTotalDue = (Label)GridOneTime.FooterRow.FindControl("lblTotalDue");
            for (int i = 0; i < GridOneTime.Rows.Count; i++)
            {
                Label lblDueO = (Label)GridOneTime.Rows[i].FindControl("lblDueO");
                TotalDue += double.Parse(lblDueO.Text);
            }
            lblTotalDue.Text = TotalDue.ToString();
        }
        else
        {
            ImageButton1.Visible = false;
            ImageButton2.Visible = false;
            ImageButton3.Visible = false;
            ImageButton4.Visible = false;
            abc.Visible = false;
            GridOneTime.DataSource = null;
            GridOneTime.DataBind();
        }
    }
    protected void FromYY_SelectedIndexChanged(object sender, EventArgs e)
        {
            _oo.YearDropDown(FromYY, FromMM, FromDD);
        }
    protected void FromMM_SelectedIndexChanged(object sender, EventArgs e)
    {
        _oo.MonthDropDown(FromYY, FromMM, FromDD);
    }
       
    protected void ToYY_SelectedIndexChanged(object sender, EventArgs e)
    {
        _oo.YearDropDown(ToYY, ToMM, ToDD);
    }
    protected void ToMM_SelectedIndexChanged(object sender, EventArgs e)
    {
        _oo.MonthDropDown(ToYY, ToMM, ToDD);
    }
        
        
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        _oo.ExportToWord(Response, "HosteDueReport.doc", gdv1);
    }

    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        _oo.ExportToExcel("HosteDueReport.xls", GridOneTime);
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {

    }
    protected void ImageButton4_Click(object sender, EventArgs e)
        {
            PrintHelper_New.ctrl = abc;
            if (GridOneTime.Rows.Count > 0)
            {
            GridOneTime.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            ScriptManager.RegisterClientScriptBlock(ImageButton4, GetType(), "onclick", "var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}",true);
        }
    public override void Dispose()
        {
            _con.Dispose();
            _oo.Dispose();
        }

    
    protected void btnView_Click(object sender, EventArgs e)
    {
        LinkButton link = (LinkButton)sender;
        LinkButton HostelReceiptNo = (LinkButton)link.NamingContainer.FindControl("btnView");
        Session["HostelReceiptNo"] = HostelReceiptNo.Text;
        Response.Redirect("HostelFeeReciept_duplicate.aspx?print=1");
    }
}
