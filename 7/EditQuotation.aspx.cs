using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class admin_EditQuotation : Page
{
    public string MSG = "", SQL = "", Reason = "";
    public int V04ID = 0;
    public int Status = 0;
    public Decimal FinalAmount = 0, RecurringAmount = 0;
    public DataTable dt = new DataTable();


    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager scriptManager = ScriptManager.GetCurrent(Page);
        scriptManager.RegisterPostBackControl(gvQuotation);

        if (!IsPostBack)
        {
            try
            {
                GetVendor();
                GetQuotation();
                ddlStatus.SelectedValue = "2";
                GetQuotationDetail();
            }
            catch(Exception ){}
        }
    }

    private void GetQuotation()
    {
        try
        {
            BAL.clsVendorQuotation obj = new BAL.clsVendorQuotation();

            obj.V01ID = Convert.ToInt32(ddlVendor.SelectedValue);
            obj.V04ID = -1;
            obj.VendorID = string.Empty;
            obj.Status = (Int32)Default.All;
            obj.IsInvoicGenerated = -1;
            obj.IsPaid = (Int32)Default.All;
            obj.QRefNo = "";


            dt = null;
            dt = new DAL().GetVendorQuotation(obj);

            if (dt != null && dt.Rows.Count > 0)
            {
                BLL.FillDropDown(ddlQuotation, dt, "QuotationFor", "V04ID", 'A');
            }
            else
            {
                ddlQuotation.Items.Clear();
            }
        }
        catch (Exception)
        {

        }
    }

    private void GetVendor()
    {
        BAL.clsVendorBank obj = new BAL.clsVendorBank();

        obj.M04ID = (Int32)Default.All;
        obj.IsActive = (Int32)Default.Yes;
        obj.V01ID = -1;
        obj.M04ID = (Int32)Default.All;
        obj.M05ID = (Int32)Default.All;
        obj.VendorID = string.Empty;
        obj.PAN = string.Empty;
        obj.TAN = string.Empty;
        obj.TIN = string.Empty;
        obj.ServiceTaxNo = string.Empty;

        dt = null;
        dt = new DAL().GetVendor(obj);

        if (dt != null && dt.Rows.Count > 0)
        {
            BLL.FillDropDown(ddlVendor, dt, "OrganizationName", "V01ID", 'A');
        }
        else
        {
            ddlVendor.Items.Clear();
        }
    }

    private void Reset()
    {
        ddlQuotation.SelectedIndex = 0;
        ddlVendor.SelectedIndex = 0;
    }

    private void GetQuotationDetail()
    {
        gvQuotation.DataSource = null;
        gvQuotation.DataBind();

        if (ddlQuotation.SelectedIndex != -1)
        {
            BAL.clsVendorQuotation obj = new BAL.clsVendorQuotation();

            obj.V01ID = Convert.ToInt32(ddlVendor.SelectedValue);
            obj.V04ID = Convert.ToInt32(ddlQuotation.SelectedValue);
            obj.VendorID = string.Empty;
            obj.Status = Convert.ToInt32(ddlStatus.SelectedValue);
            obj.IsInvoicGenerated = -1;
            obj.IsPaid = (Int32)Default.All;
            obj.QRefNo = "";

            dt = null;
            dt = new DAL().GetVendorQuotation(obj);

            if (dt != null && dt.Rows.Count > 0)
            {
                dt = BLL.BLLInstance.GetSerialNo(ref dt, "SrNo");
                gvQuotation.DataSource = dt;
            }
            else
            {
                gvQuotation.DataSource = null;
            }
            gvQuotation.DataBind();
        }
    }

    protected void ddlQuotation_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetQuotationDetail();
        ddlQuotation.Focus();
    }
    protected void ddlVendor_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetQuotationDetail();
        ddlVendor.Focus();
    }
    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetQuotationDetail();
        ddlStatus.Focus();
    }

    protected void lbtnDonwload_Click(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)(sender as LinkButton).Parent.Parent;
        string FilePath = ((Label)row.FindControl("lblDownload")).Text.Trim();

        if (!string.IsNullOrEmpty(FilePath))
        {
            Response.ContentType = "ContentType";
            Response.AddHeader("Content-Disposition", "attachment;filename=\"" + FilePath + "\"");
            Response.TransmitFile(Server.MapPath(FilePath));
            Response.End();
        }
        else
        {
            ShowMSG("File Not Uploaded !", "A");
        }
    }
    protected void lbtnEdit_Click(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)(sender as LinkButton).Parent.Parent;
        Int32 i = row.RowIndex;

        Response.Redirect("VendorQuotation.aspx?RefNo=" + ((Label)row.FindControl("lblRefNo")).Text.Trim() + "&check=VendorQuotation");  
    }

    private void ShowMSG(string MSG, string Type)
    {
        Campus camp = new Campus(); camp.msgbox(this.Page, dvMSG , BLL.BLLInstance.FetchMSG(MSG), Type);
    }
}