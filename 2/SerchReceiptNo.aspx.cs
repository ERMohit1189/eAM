using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Net.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;
using c4SmsNew;

namespace _2
{
    public partial class SerchReceiptNo : Page
    {
        private SqlConnection _con;
        readonly Campus _oo;
        private string _sql = "";
        private string _sql1 = "";
        string sessionname = "";
        public SerchReceiptNo()
        {
            _con = new SqlConnection();
            _oo = new Campus();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string)Session["LoginName"] == "" || (string)Session["BranchCode"] == "" || (string)Session["SessionName"] == "" || Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
            {
                Response.Redirect("default.aspx");
            }
            _con = _oo.dbGet_connection();
            Campus camp = new Campus(); camp.LoadLoader(loader);

            if (!IsPostBack)
            {

            }
        }
        public void loadStudentGrid(string module, string srno, string receiptNo)
        {
          
            grdshow.Visible = true;
            
            DataSet ds;
            if (module == "Admission Fee")
            {

                _sql = "select FatherContactNo srno, StudentName Name, FatherName, (Class+' '+Branch) combineClassName, Medium, format(AdmissionFromDate, 'dd-MMM-yyyy') DateOfAdmiission, FatherContactNo FamilyContactNo from AdmissionFormCollection where SessionName = '" + Session["SessionName"] + "' and BranchCode =" + Session["BranchCode"] + " and RecieptNo='" + receiptNo.Trim() + "'";
                Grd.DataSource = _oo.GridFill(_sql);
                Grd.DataBind();
                ds = _oo.GridFill(_sql);
                if (ds != null && Grd.Rows.Count > 0)
                {
                    divimg.Visible = false;
                    grdshow.Visible = true;
                }
            }
            else
            {

                _sql = "select * from AllStudentRecord_UDF('" + Session["SessionName"] + "', " + Session["BranchCode"] + ") where  Srno='" + srno + "'";
                Grd.DataSource = _oo.GridFill(_sql);
                Grd.DataBind();

                ds = _oo.GridFill(_sql);
                if (ds != null && Grd.Rows.Count > 0)
                {
                    divimg.Visible = true;
                    grdshow.Visible = true;
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        img.ImageUrl = ds.Tables[0].Rows[0]["PhotoPath"].ToString() != string.Empty ? ds.Tables[0].Rows[0]["PhotoPath"] + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture) : "../img/user-pic/user-pic.jpg" + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture);
                        studentImg.NavigateUrl = ds.Tables[0].Rows[0]["PhotoPath"].ToString() != string.Empty ? ds.Tables[0].Rows[0]["PhotoPath"] + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture) : "../img/user-pic/user-pic.jpg" + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture);
                        hylinkmoredetails.NavigateUrl = "../11/StudentRegView.aspx?print=1&id=" + ds.Tables[0].Rows[0]["stenrcode"];
                    }
                }
                else
                {
                    Grd.DataSource = null;
                    Grd.DataBind();
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                    grdshow.Visible = false;
                }
            }
        }
        protected void txtStudentEnter_TextChanged(object sender, EventArgs e)
        {
            loadfee();
        }
        protected void lnkShow_Click(object sender, EventArgs e)
        {
            loadfee();
        }
        
        public void loadfee()
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            Grd.DataSource = null;
            Grd.DataBind();
            string studentId = Request.Form[hfStudentId.UniqueID];

            string ReceiptNo = ""; string SrNo = "";
            if (studentId == string.Empty)
            {
                studentId = txtStudentEnter.Text.Trim();
            }
            else
            {
                string[] data = studentId.Split(new string[] { "##" }, StringSplitOptions.None);
                ReceiptNo = data[0];
                SrNo = data[1];
            }
           
            int i;
            _sql = " select * from(  ";
            _sql = _sql + " ( select distinct SrNo, format(DepositDate, 'dd-MMM-yyyy') DepositDate, ReceiptNo, 'Tuition Fee' madule, ModeOfPayment mop, receiptStatus Status, format(RecordDate, 'dd-MMM-yyyy hh:mm:ss tt') RecordDate, LoginName from CompositFeeDeposit where ReceiptNo='" + ReceiptNo + "' and BranchCode=" + Session["BranchCode"] + " )";
            _sql = _sql+ "  union all";
            _sql = _sql+ " ( select distinct SrNo, format(DepositDate, 'dd-MMM-yyyy') DepositDate, Receipt_no ReceiptNo, 'Other Fee' madule, Mode mop, PaymentSatus Status, format(RecordedDate, 'dd-MMM-yyyy hh:mm:ss tt') RecordDate, LoginName from OtherFeeDeposit where Receipt_no='" + ReceiptNo + "' and BranchCode=" + Session["BranchCode"] + "  )";
            _sql = _sql+ "  union all";
            _sql = _sql+ " ( select distinct SrNo, format(TCIssueDate, 'dd-MMM-yyyy') DepositDate, RecieptNo ReceiptNo, 'TC Fee' madule, mop, Status, format(RecordDate, 'dd-MMM-yyyy hh:mm:ss tt') RecordDate, LoginName from TCCollection where RecieptNo='" + ReceiptNo + "' and BranchCode=" + Session["BranchCode"] + "  )";
            _sql = _sql+ "  union all";
            _sql = _sql+ " ( select distinct SrNo, format(CCissuedate, 'dd-MMM-yyyy') DepositDate, RecieptNo ReceiptNo, 'CC Fee' madule, mop, Status, format(RecordDate, 'dd-MMM-yyyy hh:mm:ss tt') RecordDate, LoginName from CCCollection where RecieptNo='" + ReceiptNo + "' and BranchCode=" + Session["BranchCode"] + "  )";
            _sql = _sql+ "  union all";
            _sql = _sql+ " ( select distinct SrNo, format(RecordDate, 'dd-MMM-yyyy') DepositDate, RecieptNo ReceiptNo, 'Admission Fee' madule, mop, Status, format(RecordDate, 'dd-MMM-yyyy hh:mm:ss tt') RecordDate, LoginName from AdmissionFormCollection where RecieptNo='" + ReceiptNo + "' and BranchCode=" + Session["BranchCode"] + "  )";
            _sql = _sql+ "  union all";
            _sql = _sql+ " ( select distinct SrNo, format(RecordDate, 'dd-MMM-yyyy') DepositDate, Receipt_no ReceiptNo, 'Addttional Fee' madule, mop, Status, format(RecordDate, 'dd-MMM-yyyy hh:mm:ss tt') RecordDate, LoginName from Other_fee_collection_1 where Receipt_no='" + ReceiptNo + "' and BranchCode=" + Session["BranchCode"] + "  )";
            _sql = _sql+ "  union all";
            _sql = _sql+ " ( select distinct SrNo, format(DepositDate, 'dd-MMM-yyyy') DepositDate, Receipt_no ReceiptNo, 'Product Fee' madule, Mode mop, PaymentSatus status, format(RecordedDate, 'dd-MMM-yyyy hh:mm:ss tt') RecordDate, LoginName from UniformFeeDeposit where Receipt_no='" + ReceiptNo + "' and BranchCode=" + Session["BranchCode"] + "  )";
            _sql = _sql+ "  union all";
            _sql = _sql + " (  select distinct r.srno SrNo, format(f.RecordDate, 'dd-MMM-yyyy') DepositDate, Receiptno ReceiptNo, 'Library Fee' madule, MOD mop, Status, format(f.RecordDate, 'dd-MMM-yyyy hh:mm:ss tt') RecordDate, f.LoginName  from ReturnBookFine f inner join BookIssueReturn r on r.id=BIRid and r.sessionname=f.SessionName and r.branchcode=f.BranchCode where Receiptno='" + ReceiptNo + "' and f.BranchCode=" + Session["BranchCode"] + "  ))T1";
            var dt = _oo.Fetchdata(_sql);
            if (dt.Rows.Count > 0)
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
                double total = 0;
                for (i = 0; i <= GridView1.Rows.Count - 1; i++)
                {
                    Label lblReceiptNo = (Label)GridView1.Rows[i].FindControl("lblReceiptNo");
                    Label lblpaid = (Label)GridView1.Rows[i].FindControl("lblpaid");
                    _sql = "select sum(PaidAmount) PaidAmount from(";
                    _sql = _sql + " select isnull(sum(isnull(PaidAmount, 0)), 0) PaidAmount from CompositFeeDeposit where ReceiptNo='" + lblReceiptNo.Text + "' and BranchCode=" + Session["BranchCode"] + " ";
                    _sql = _sql + " union all";
                    _sql = _sql + " select isnull(sum(isnull(PaidAmt, 0)), 0) PaidAmount from OtherFeeDeposit where Receipt_no='" + lblReceiptNo.Text + "' and BranchCode=" + Session["BranchCode"] + " ";
                    _sql = _sql + " union all";
                    _sql = _sql + " select isnull(sum(isnull(ReceivedAmount, 0)), 0) PaidAmount from TCCollection where RecieptNo='" + lblReceiptNo.Text + "' and BranchCode=" + Session["BranchCode"] + " ";
                    _sql = _sql + " union all";
                    _sql = _sql + " select isnull(sum(isnull(ReceivedAmount, 0)), 0) PaidAmount from CCCollection where RecieptNo='" + lblReceiptNo.Text + "' and BranchCode=" + Session["BranchCode"] + " ";
                    _sql = _sql + " union all";
                    _sql = _sql + " select isnull(sum(isnull(ReceivedAmount, 0)), 0) PaidAmount from AdmissionFormCollection where RecieptNo='" + lblReceiptNo.Text + "' and BranchCode=" + Session["BranchCode"] + " ";
                    _sql = _sql + " union all";
                    _sql = _sql + " select isnull(sum(isnull(ReceivedAmount, 0)), 0) PaidAmount from Other_fee_collection_1 where Receipt_no='" + lblReceiptNo.Text + "' and BranchCode=" + Session["BranchCode"] + " ";
                    _sql = _sql + " union all";
                    _sql = _sql + " select isnull(sum(isnull(PaidAmt, 0)), 0) PaidAmount from UniformFeeDeposit where Receipt_no='" + lblReceiptNo.Text + "' and BranchCode=" + Session["BranchCode"] + " ";
                    _sql = _sql + " union all";
                    _sql = _sql + " select isnull(sum(isnull(FineAmount, 0)), 0) PaidAmount from ReturnBookFine where Receiptno='" + lblReceiptNo.Text + "' and BranchCode=" + Session["BranchCode"] + " ";
                    _sql = _sql + " )T1";
                    lblpaid.Text = double.Parse(_oo.ReturnTag(_sql, "PaidAmount") == "" ? "0" : _oo.ReturnTag(_sql, "PaidAmount")).ToString("0.00");
                    total = total + double.Parse(lblpaid.Text);
                    
                }

                Label fTotal = (Label)GridView1.FooterRow.FindControl("fTotal");
                fTotal.Text = total.ToString("0.00");
                Label lblmadule = (Label)GridView1.Rows[0].FindControl("lblmadule");

                loadStudentGrid(lblmadule.Text, SrNo, ReceiptNo);

            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
        }

       
        public override void Dispose()
        {
            _con.Dispose();
            _oo.Dispose();
        }
        
    }
}