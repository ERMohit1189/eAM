using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;

public partial class ReceiptSrNoMaster : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((string)Session["LoginName"] == "" || (string)Session["BranchCode"] == "" || (string)Session["SessionName"] == "" || Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);
        if (!IsPostBack)
        {
            sql = "select top(1) StartType from ReceiptNoStart where branchCode="+ Session["BranchCode"] + " order by id desc";
            if (oo.Duplicate(sql))
            {
                lblReceiptNoText.Enabled = false;
                rdoStart.Enabled = false;
                LinkButton1.Enabled = false;

                var recieptno = oo.FindRecieptNo();
                lblReceiptNoText.Text = recieptno;
                rdoStart.SelectedValue = oo.ReturnTag(sql, "StartType");
            }
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        int co = 0;
        sql = "select isnull(count(distinct isnull(ReceiptNo, 0)), 0) ReceiptNo from( ";
        sql = sql + "select ReceiptNo as ReceiptNo from TutionFeeDeposit where BranchCode = " + Session["BranchCode"].ToString() + "";
        sql = sql + "union all ";
        sql = sql + "select ReceiptNo as ReceiptNo from TransportFeeDeposit where BranchCode = " + Session["BranchCode"].ToString() + "";
        sql = sql + "union all ";
        sql = sql + "select ReceiptNo as ReceiptNo from HostelFeeDeposit where BranchCode = " + Session["BranchCode"].ToString() + "";
        sql = sql + "union all ";
        sql = sql + "select  ReceiptNo as ReceiptNo from MiscellaneousFeeDeposit where BranchCode = " + Session["BranchCode"].ToString() + "";
        sql = sql + "union all ";
        sql = sql + "select RecieptNo as ReceiptNo from TCCollection where BranchCode = " + Session["BranchCode"].ToString() + "";
        sql = sql + "union all ";
        sql = sql + "select RecieptNo as ReceiptNo from CCCollection where BranchCode = " + Session["BranchCode"].ToString() + "";
        sql = sql + "union all ";
        sql = sql + "select RecieptNo as ReceiptNo from AdmissionFormCollection where BranchCode = " + Session["BranchCode"].ToString() + "";
        sql = sql + "union all ";
        sql = sql + "select Receipt_no as ReceiptNo from UniformFeeDeposit where BranchCode = " + Session["BranchCode"].ToString() + "";
        sql = sql + "union all ";
        sql = sql + "select  Receipt_no as ReceiptNo from OtherFeeDeposit where BranchCode = " + Session["BranchCode"].ToString() + "";
        sql = sql + "union all ";
        sql = sql + "select Receipt_no as ReceiptNo from Other_fee_collection_1 where BranchCode = " + Session["BranchCode"].ToString() + "";
        sql = sql + "union all ";
        sql = sql + "select Receiptno as ReceiptNo from ReturnBookFine where BranchCode = " + Session["BranchCode"].ToString() + "";
        sql = sql + "union all ";
        sql = sql + "select Receipt_no as ReceiptNo from UniformFeeDeposit where BranchCode = " + Session["BranchCode"].ToString() + "";
        sql = sql + ") T5 ";
        try
        {
            co = Convert.ToInt32(oo.ReturnTag(sql, "ReceiptNo"));
        }
        catch (Exception) { co = 0; con.Close(); }
        if (co>0)
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Fee submitted, Receipt No. can not be change!", "A");
        }
        else
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "ReceiptNoStartProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@ReceiptNoStart", lblReceiptNoText.Text.ToString());
            cmd.Parameters.AddWithValue("@StartType", rdoStart.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@Remark", "");
            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
            cmd.Connection = con;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");
                lblReceiptNoText.Enabled = false;
                rdoStart.Enabled = false;
                LinkButton1.Enabled = false;
            }
            catch (SqlException) { }
        }
    }
    public void PermissionGrant(int add1, LinkButton Ladd)
    {
        if (add1 == 1)
        {
            Ladd.Enabled = true;
        }
        else
        {
            Ladd.Enabled = false;
        }
    }
}