using System;
using System.Web.UI;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class VendorQuotation : Page
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
            try
            {
                GetQtnList();
            }
            catch (Exception ex)
            {
            }
        }
    }
    protected void txtSearchBy_TextChanged(object sender, EventArgs e)
    {
        var studentId = hfVendorId.Value;
        if (txtSearchBy.Text != string.Empty && studentId != String.Empty)
        {
            GetVendorList();
            GetQtnList();
            divControls.Visible = true;
        }
        else
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, dvSearch, BLL.BLLInstance.FetchMSG("Enter Vendor ID !"), "A");
            txtSearchBy.Focus();
            divControls.Visible = false;
            Reset();
        }
    }

    protected void lbtnSearchBy_Click(object sender, EventArgs e)
    {
        var studentId = hfVendorId.Value;
        if (txtSearchBy.Text != string.Empty && studentId != String.Empty)
        {
            GetVendorList();
            GetQtnList();
            divControls.Visible = true;
        }
        else
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, dvSearch, BLL.BLLInstance.FetchMSG("Enter Vendor ID !"), "A");
            txtSearchBy.Focus();
            divControls.Visible = false;
            Reset();
        }
    }
    private void GetVendorList()
    {
        var studentId = hfVendorId.Value;
        dt = null;
        sql = " select VendorCode, OrganizationName, VendorType, ContactPerson, MailID, MobileNo, FilePath, FileName from AccVendor vr ";
        sql = sql + " inner join AccVendorDocument vd on vr.id = vd.VendorId and vr.BranchCode = vd.BranchCode ";
        sql = sql + " inner join AccVendorType vt on vt.id = vr.VenderTypeId and vr.BranchCode = vd.BranchCode ";
        sql = sql + " where vr.IsActive = 1 ";
        sql = sql + " and vr.BranchCode=" + Session["BranchCode"] + " ";
            sql = sql + " and vr.id=" + studentId + " ";
        dt = _oo.Fetchdata(sql);
        if (dt != null && dt.Rows.Count > 0)
        {
            gvVendorList.DataSource = dt;
            gvVendorList.DataBind();
        }
        else
        {
            gvVendorList.DataSource = null;
            gvVendorList.DataBind();
        }
    }
    private void GetQtnList()
    {
        var studentId = hfVendorId.Value;
        sql = "select qt.*, qt.Id ids, format(qt.QtnDate, 'dd-MMM-yyyy') QtnEnterDate, OrganizationName+' ('+VendorCode+')' VendorName, (case when qt.status='Approve' then 'Approved' else case when qt.status='Reject' then 'Rejected' else qt.status end end)qtstatus  from InvQuotation qt ";
        sql =sql+ " inner join AccVendor v on v.id=qt.VendorId and v.BranchCode=qt.BranchCode";
        sql = sql+ " where qt.BranchCode=" + Session["BranchCode"] + " and qt.status='Pending'";
        if (studentId!="")
        {
            sql = sql + " and qt.Vendorid=" + studentId + " ";
        }
        var dt = _oo.Fetchdata(sql);
        gvBankBranchList.DataSource = dt;
        gvBankBranchList.DataBind();
        for (int i = 0; i < gvBankBranchList.Rows.Count; i++)
        {
            Label Status = (Label)gvBankBranchList.Rows[i].FindControl("Status");
            LinkButton lbtnEdit = (LinkButton)gvBankBranchList.Rows[i].FindControl("lbtnEdit");
            LinkButton lbtnDelete = (LinkButton)gvBankBranchList.Rows[i].FindControl("lbtnDelete");
            if (Status.Text!="Pending")
            {
                lbtnEdit.Text = "<i class='fa fa-lock'></i>";
                lbtnDelete.Text = "<i class='fa fa-lock'></i>";
                lbtnEdit.Enabled = false;
                lbtnDelete.Enabled = false;
            }
        }

    }

    private void Reset()
    {
        hidFile.Value = "";
        hidFileExt.Value = "";
        hfVendorId.Value = "";
        txtSearchBy.Text = "";
        txtRefNo.Text = "";
        txtAmount.Text = "";
        txtRemark.Text = "";
        txtTitle.Text = "";
    }
    

    protected void btnInsert_Click(object sender, EventArgs e)
    {
        var studentId = hfVendorId.Value;
        if (txtSearchBy.Text != string.Empty && studentId != String.Empty)
        {

            using (SqlCommand cmd = new SqlCommand())
            {
                var fileName = string.Empty;
                var filePath = "";

                var base64Std = hidFile.Value;
                if (base64Std != string.Empty)
                {
                    filePath = @"~/Uploads/VendorDoc/";
                    if (!Directory.Exists(Server.MapPath(filePath)))
                    {
                        Directory.CreateDirectory(Server.MapPath(filePath));
                    }

                    var rn = new Random();

                    fileName = "Qtn_" + rn.Next(123456789, 987654321)+DateTime.Now.ToString("ddmmmyyyyhhmmsstt") + "_" + hidFileExt.Value.ToString();
                    filePath = string.Format("~/Uploads/VendorDoc/{0}", fileName);

                    using (FileStream fs = new FileStream(Server.MapPath(filePath), FileMode.Create))
                    {
                        using (BinaryWriter bw = new BinaryWriter(fs))
                        {
                            var data = Convert.FromBase64String(base64Std);
                            bw.Write(data);
                            bw.Close();
                        }
                    }

                    hidFile.Value = "";
                    hidFileExt.Value = "";
                }
                cmd.CommandText = "InvQuotationProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = _con;
                cmd.Parameters.AddWithValue("@VendorId", studentId);
                cmd.Parameters.AddWithValue("@QtnDate", txtQtnDate.Text.Trim());
                cmd.Parameters.AddWithValue("@Title", txtTitle.Text.Trim());
                cmd.Parameters.AddWithValue("@RefNo", txtRefNo.Text.Trim());
                cmd.Parameters.AddWithValue("@Amount", txtAmount.Text.Trim());
                cmd.Parameters.AddWithValue("@FileName", fileName.Trim());
                cmd.Parameters.AddWithValue("@FilePath", filePath.Trim());
                cmd.Parameters.AddWithValue("@Remark", txtRemark.Text.Trim());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                cmd.Parameters.AddWithValue("@Action", "insert");
                try
                {
                    _con.Open();
                    cmd.ExecuteNonQuery();
                    _con.Close();
                    Campus camp = new Campus(); camp.msgbox(Page, dvSearch, "Submitted successfully.", "S");
                    GetQtnList();
                    Reset();
                    divControls.Visible = false;
                }
                catch (Exception ex)
                {
                }
            }
        }
        else
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, dvSearch, BLL.BLLInstance.FetchMSG("Enter Vendor ID !"), "A");
            txtSearchBy.Focus();
            Reset();
        }
    }
    protected void lbtnEdit_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("Label36");
        string ss = lblId.Text;
        lblID.Text = ss;

        Label lblDate = (Label)chk.NamingContainer.FindControl("lblDate");
        Label lblTitle = (Label)chk.NamingContainer.FindControl("lblTitle");
        Label lblRefNo = (Label)chk.NamingContainer.FindControl("lblRefNo");
        Label lblAmount = (Label)chk.NamingContainer.FindControl("lblAmount");
        Label Remark = (Label)chk.NamingContainer.FindControl("Remark");

        txtQtnDate0.Text = lblDate.Text;
        txtTitle0.Text = lblTitle.Text;
        txtRefNo0.Text = lblRefNo.Text;
        txtAmount0.Text = lblAmount.Text;
        txtRemark0.Text = Remark.Text;
        Panel1_ModalPopupExtender.Show();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        _sql = "SELECT Title FROM InvQuotation WHERE RefNo='" + txtRefNo0.Text + "' and BranchCode=" + Session["BranchCode"] + " and id<>" + lblID.Text + "";
        if (_oo.Duplicate(_sql))
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Duplicate Ref. No.!", "A");
        }
        else
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                var fileName = string.Empty;
                var filePath = "";

                var base64Std = hidFile0.Value;
                if (base64Std != string.Empty)
                {
                    filePath = @"~/Uploads/VendorDoc/";
                    if (!Directory.Exists(Server.MapPath(filePath)))
                    {
                        Directory.CreateDirectory(Server.MapPath(filePath));
                    }

                    var rn = new Random();

                    fileName = "Qtn_" + txtRefNo0.Text.Trim() + "_" + rn.Next(123456789, 987654321) + "_" + hidFileExt0.Value.ToString();
                    filePath = string.Format("~/Uploads/VendorDoc/{0}", fileName);

                    using (FileStream fs = new FileStream(Server.MapPath(filePath), FileMode.Create))
                    {
                        using (BinaryWriter bw = new BinaryWriter(fs))
                        {
                            var data = Convert.FromBase64String(base64Std);
                            bw.Write(data);
                            bw.Close();
                        }
                    }

                    hidFile0.Value = "";
                    hidFileExt0.Value = "";
                }
                cmd.CommandText = "InvQuotationProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = _con;
                cmd.Parameters.AddWithValue("@Id", lblID.Text.Trim());
                cmd.Parameters.AddWithValue("@QtnDate", txtQtnDate0.Text.Trim());
                cmd.Parameters.AddWithValue("@Title", txtTitle0.Text.Trim());
                cmd.Parameters.AddWithValue("@RefNo", txtRefNo0.Text.Trim());
                cmd.Parameters.AddWithValue("@Amount", txtAmount0.Text.Trim());
                if (fileName!= "" && fileName != string.Empty)
                {
                    cmd.Parameters.AddWithValue("@FileName", fileName.Trim());
                }
                if (filePath != "" && filePath!=string.Empty)
                {
                    cmd.Parameters.AddWithValue("@FilePath", filePath.Trim());
                }
                cmd.Parameters.AddWithValue("@Remark", txtRemark0.Text.Trim());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                cmd.Parameters.AddWithValue("@Action", "Update");
                try
                {
                    _con.Open();
                    cmd.ExecuteNonQuery();
                    _con.Close();
                    Campus camp = new Campus(); camp.msgbox(Page, dvSearch, "Updatted successfully.", "S");
                    GetQtnList();
                    _sql = "";
                }
                catch (Exception ex)
                {
                }
            }
        }
    }
    protected void lbtnDelete_Click(object sender, EventArgs e)
    {
        btnNo.Focus();
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("Label37");
        string ss = lblId.Text;
        lblValue.Text = ss;
        mpeDelete.Show();
    }

    protected void btnYes_Click(object sender, EventArgs e)
    {
        DeleteRecord();
    }

    public void DeleteRecord()
    {
        _sql = "Delete from InvQuotation where Id=" + lblValue.Text + " and BranchCode="+ Session["BranchCode"]+"";

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
                Campus camp = new Campus(); camp.msgbox(Page, dvSearch, "Deleted successfully.", "S");
                GetQtnList();
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

    public override void Dispose()
    {
        _oo.Dispose();
    }

    
}