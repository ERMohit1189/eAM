using System;
using System.Web.UI;
using System.Data;
using System.IO;

public partial class admin_VendorInvoice : Page
{
    public string MSG = "", SQL = "";
    public static int V04ID = 0;
    public DataTable dt = new DataTable();

    Campus cmps = new Campus();

    protected void Page_preinit(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("../Index.aspx");
        }        
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtDOI.Text = DateTime.Now.ToString("dd-MMM-yyyy");

            GetQuotation();
        }
    }

    private void GetQuotation()
    {
        try
        {
            BAL.clsVendorQuotation obj = new BAL.clsVendorQuotation();

            obj.V01ID =-1;
            obj.V04ID = -1;
            obj.VendorID = string.Empty;
            obj.Status = (Int32)Default.Yes;
            obj.IsInvoicGenerated = 0;
            obj.IsPaid = (Int32)Default.All;

            dt = null;
            dt = new DAL().GetVendorQuotationForInvoice(obj);

            if (dt != null && dt.Rows.Count > 0)
            {
                BLL.FillDropDown(ddlQuotation, dt, "RefNo", "V04ID", 'S');
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
  
    private void Reset()
    {        
        txtInvoiceNo.Text = string.Empty;
        txtRemark.Text = string.Empty;

        ddlQuotation.SelectedIndex = 0;

        gvQuotation.DataSource = null;
        gvQuotation.DataBind();
    } 

    private void Validation(Control cntrl)
    {
        MSG = "";

        if (ddlQuotation.SelectedIndex < 1)
        {
            MSG += "Select A Quotation !" + "\\n";
        }

        if (MSG != string.Empty)
        {
            new Campus().MessageBoxforUpdatePanel(MSG, cntrl);
        }
        else
        {
            SetVendorInvoice(cntrl);
        }
    }

    private void SetVendorInvoice(Control cntrl)
    {
        BAL.clsVendorInvoice obj = new BAL.clsVendorInvoice();

        obj.V01ID = 0;
        obj.V04ID = Convert.ToInt32(ddlQuotation.SelectedValue);
        obj.RefNo = "";
        obj.InvoiceNo = txtInvoiceNo.Text.Trim();
        obj.IsActive = 1;

        string FileName = string.Empty;
        string ext = "";
        string FilePath = "";
        if (!Directory.Exists(Server.MapPath(FilePath)))
        {
            Directory.CreateDirectory(Server.MapPath(FilePath));
        }
          
        if (fuFile.HasFile)
        {
            FilePath = @"~/Uploads/VendorDoc/";
            Random rn = new Random();

            ext = Path.GetExtension(fuFile.FileName);
            FileName = "Invoice" + rn.Next(123456789, 987654321) + "_" + fuFile.FileName;
            fuFile.SaveAs(Server.MapPath(FilePath + FileName));
        } 

        obj.FileName = FileName;
        obj.FilePath = FilePath + FileName;
        obj.Remark = txtRemark.Text.Trim();
        obj.SQL = SQL;

        MSG = new DAL().SetVendorInvoice(obj);
        if (MSG == "")
        {
            MSG = SQL;
            Reset();
            GetQuotation();
        }

        BLL.BLLInstance.ShowMSG(MSG);
    }

    protected void btnInsert_Click(object sender, EventArgs e)
    {
        SQL = "I";
        Validation(btnInsert);
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        Reset();
    }

    protected void ddlQuotation_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetQuotationDetail();
    }

    private void GetQuotationDetail()
    {
        gvQuotation.DataSource = null;
        gvQuotation.DataBind();

        if (ddlQuotation.SelectedIndex != -1 && ddlQuotation.SelectedIndex != 0)
        {
            BAL.clsVendorQuotation obj = new BAL.clsVendorQuotation();

            obj.V01ID = (Int32)Default.All;
            obj.V04ID = Convert.ToInt32(ddlQuotation.SelectedValue);
            obj.VendorID = string.Empty;
            obj.Status = (Int32)Default.Yes;
            obj.IsInvoicGenerated = -1;
            obj.IsPaid = (Int32)Default.All;
            obj.QRefNo = "";

            dt = null;
            dt = new DAL().GetVendorQuotation(obj);

            if (dt != null && dt.Rows.Count > 0)
            {
                gvQuotation.DataSource = dt;
                gvQuotation.DataBind();
            }           
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {

    }
}