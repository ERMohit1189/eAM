using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class admin_VendorList : Page
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
        BLL.BLLInstance.LoadHeader("Report", header);
        if (!IsPostBack)
        {
            GetVendorType();
            GetOrganization();
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    private void GetVendorType()
    {
        ddlVendorType.Items.Clear();
        _sql = "select * from AccVendorType where branchcode=" + Session["BranchCode"] + "";
        _oo.FillDropDown_withValue(_sql, ddlVendorType, "VendorType", "id");
        ddlVendorType.Items.Insert(0, new ListItem("<--Select-->", ">"));
    }

    private void GetOrganization()
    {
        ddlOrganizationType.Items.Clear();
        _sql = "select * from AccOrganizationType where branchcode=" + Session["BranchCode"] + "";
        _oo.FillDropDown_withValue(_sql, ddlOrganizationType, "OrganizationType", "id");
        ddlOrganizationType.Items.Insert(0, new ListItem("<--Select-->", ">"));
    }
    
    private void Reset()
    {
        hfVendorId.Value = "";
        txtSearchBy.Text = "";
        ddlOrganizationType.SelectedIndex = 0;
        ddlVendorType.SelectedIndex = 0;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetVendorList();
    }
    protected void txtSearchBy_TextChanged(object sender, EventArgs e)
    {
        var studentId = hfVendorId.Value;
        if (txtSearchBy.Text != string.Empty && studentId != String.Empty)
        {
            GetVendorList();
        }
        else
        {
            gvVendorList.DataSource = null;
            gvVendorList.DataBind();
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, BLL.BLLInstance.FetchMSG("Enter Vendor ID !"), "A");
            txtSearchBy.Focus();
        }
    }
    private void GetVendorList()
    {
        abc.Visible = true;
        divExport.Visible = true;
        var studentId = hfVendorId.Value;
        dt = null;
        sql = " select VendorCode, OrganizationName, VendorType, ContactPerson, MailID, MobileNo, FilePath, FileName from AccVendor vr ";
        sql = sql + " inner join AccVendorDocument vd on vr.id = vd.VendorId and vr.BranchCode = vd.BranchCode ";
        sql = sql + " inner join AccVendorType vt on vt.id = vr.VenderTypeId and vr.BranchCode = vd.BranchCode ";
        sql = sql + " where vr.IsActive = case when '" + ddlIsActive.SelectedValue + "'='-1' then vr.IsActive else " + ddlIsActive.SelectedValue + " end ";
        sql = sql + " and vr.BranchCode=" + Session["BranchCode"] + " ";
        if (ddlVendorType.SelectedIndex != 0)
        {
            sql = sql + " and vr.VenderTypeId=" + ddlVendorType.SelectedValue + " ";
        }
        if (ddlOrganizationType.SelectedIndex != 0)
        {
            sql = sql + " and vr.OrganizationTypeId=" + ddlOrganizationType.SelectedValue + " ";
        }
        if (studentId != "")
        {
            sql = sql + " and vr.id=" + studentId + " ";
        }
        dt = _oo.Fetchdata(sql);
        if (dt != null && dt.Rows.Count > 0)
        {
            heading.Text = "List of Vendors";
            lblRegister.Text = "Date : " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            gvVendorList.DataSource = dt;
            gvVendorList.DataBind();
            pnlcontrols.Visible = true;
        }
        else
        {
            gvVendorList.DataSource = null;
            gvVendorList.DataBind();
            abc.Visible = false;
            divExport.Visible = false;
            pnlcontrols.Visible = false;
        }
        hfVendorId.Value = "";
        txtSearchBy.Text = "";
    }
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        _oo.ExportTolandscapeWord(Response, "ListOfVendors", gdv1);
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        _oo.ExportDivToExcelWithFormatting(Response, "ListOfVendors.xls", gdv1, Server.MapPath("~/Admin/css/style.css"));
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {
        _oo.ExporttolandscapePdf(Response, "ListOfVendors", abc);
    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = abc;
        ClientScript.RegisterStartupScript(GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }
}