using c4SmsNew;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _2
{
    public partial class AdminReceiptCancellation : Page
    {
        private SqlConnection _con;
        readonly Campus _oo;
        private string _sql = "";
        private string _sql1 = "";
        string sessionname = "";
        public AdminReceiptCancellation()
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
        protected void txtStudentEnter_TextChanged(object sender, EventArgs e)
        {
            loadStudentGrid();
        }
        protected void lnkShow_Click(object sender, EventArgs e)
        {
            loadStudentGrid();
        }
        protected void DrpTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DrpTables.SelectedValue == "5")
            {
                divSr.Visible = false;
                divReceipt.Visible = true;
            }
            else
            {
                divSr.Visible = true;
                divReceipt.Visible = false;
            }
            loadStudentGrid();
        }
        public void loadStudentGrid()
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            Grd.DataSource = null;
            Grd.DataBind();
            grdshow.Visible = true;
            string studentId = Request.Form[hfStudentId.UniqueID];
            if (studentId == string.Empty)
            {
                studentId = txtStudentEnter.Text.Trim();
            }
            DataSet ds;
            if (DrpTables.SelectedValue == "5")
            {

                _sql = "select srno, StudentName Name, FatherName, (Class+' '+Branch) combineClassName, Medium, format(AdmissionFromDate, 'dd-MMM-yyyy') DateOfAdmiission, FatherContactNo FamilyContactNo from AdmissionFormCollection where BranchCode =" + Session["BranchCode"] + " and RecieptNo='" + txtReceipt.Text.Trim() + "' and isnull(cancel, '0')='0'";
                Grd.DataSource = _oo.GridFill(_sql);
                Grd.DataBind();
                ds = _oo.GridFill(_sql);
                if (ds != null && Grd.Rows.Count > 0)
                {
                    divimg.Visible = false;
                    grdshow.Visible = true;
                    loadfee();
                }
            }
            else
            {

                _sql = "select * from AllStudentRecord_UDF('" + Session["SessionName"] + "', " + Session["BranchCode"] + ") where  Srno='" + studentId + "'";
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
                    loadfee();
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
        public void loadfee()
        {
            string studentId = Request.Form[hfStudentId.UniqueID];
            if (studentId == string.Empty)
            {
                studentId = txtStudentEnter.Text.Trim();
            }
            if (DrpTables.SelectedValue != "5")
            {
                _sql = "select srno from AllStudentRecord_UDF('" + Session["SessionName"] + "', " + Session["BranchCode"] + ") where  isnull(Promotion, '')='Cancelled' and SrNo='" + studentId + "'";
                if (_oo.Duplicate(_sql))
                {
                    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Student Promoted, Receipt can not be cancelled", "A");
                    return;
                }
            }
            int i;
            GridView1.Visible = true;
            if (DrpTables.SelectedValue == "1")
            {
                _sql = "select distinct SrNo, format(DepositDate, 'dd-MMM-yyyy') DepositDate, ReceiptNo, modeofpayment mop, receiptStatus Status,Right(ReceiptNo,8) from CompositFeeDeposit where SrNo='" + studentId + "' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and ISNULL(receiptStatus, '') in ('Paid', 'Pending') order by Right(ReceiptNo,8)";
            }
            if (DrpTables.SelectedValue == "2")
            {
                _sql = "select distinct SrNo, format(DepositDate, 'dd-MMM-yyyy') DepositDate, Receipt_no ReceiptNo, Mode mop, PaymentSatus Status from OtherFeeDeposit where SrNo='" + studentId + "' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and ISNULL(PaymentSatus, '') in ('Paid', 'Pending') and isnull(cancel, '0')='0' order by Receipt_no";
            }
            if (DrpTables.SelectedValue == "3")
            {
                _sql = "select distinct SrNo, format(TCIssueDate, 'dd-MMM-yyyy') DepositDate, RecieptNo ReceiptNo, mop, Status from TCCollection where SrNo='" + studentId + "' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and ISNULL(Status, '') in ('Paid', 'Pending') and isnull(cancel, '0')='0' order by RecieptNo";
            }
            if (DrpTables.SelectedValue == "4")
            {
                _sql = "select distinct SrNo, format(CCissuedate, 'dd-MMM-yyyy') DepositDate, RecieptNo ReceiptNo, mop, Status from CCCollection where SrNo='" + studentId + "' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and ISNULL(Status, '') in ('Paid', 'Pending') and isnull(cancel, '0')='0' order by RecieptNo";
            }
            if (DrpTables.SelectedValue == "5")
            {
                _sql = "select distinct SrNo, format(RecordDate, 'dd-MMM-yyyy') DepositDate, RecieptNo ReceiptNo, mop, Status from AdmissionFormCollection where RecieptNo='" + txtReceipt.Text + "' and BranchCode=" + Session["BranchCode"] + " and ISNULL(Status, '') in ('Paid', 'Pending') and isnull(cancel, '0')='0' and RecieptNo='" + txtReceipt.Text.Trim() + "' order by RecieptNo";
            }
            if (DrpTables.SelectedValue == "6")
            {
                _sql = "select distinct SrNo, format(RecordDate, 'dd-MMM-yyyy') DepositDate, Receipt_no ReceiptNo, mop, Status from Other_fee_collection_1 where SrNo='" + studentId + "' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and ISNULL(Status, '') in ('Paid', 'Pending') and isnull(cancel, '0')='0' order by Receipt_no";
            }
            if (DrpTables.SelectedValue == "7")
            {
                _sql = "select distinct SrNo, format(DepositDate, 'dd-MMM-yyyy') DepositDate, Receipt_no ReceiptNo, Mode mop, PaymentSatus status from UniformFeeDeposit where SrNo='" + studentId + "' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and ISNULL(PaymentSatus, '') in ('Paid', 'Pending') and isnull(cancel, '0')='0' order by Receipt_no";
            }
            if (DrpTables.SelectedValue == "8")
            {
                _sql = "select distinct r.srno SrNo, format(f.RecordDate, 'dd-MMM-yyyy') DepositDate, Receiptno ReceiptNo, MOD mop, Status  from ReturnBookFine f inner join BookIssueReturn r on r.id=BIRid and r.sessionname=f.SessionName and r.branchcode=f.BranchCode where r.SrNo='" + studentId + "' and f.BranchCode=" + Session["BranchCode"] + " and f.SessionName='" + Session["SessionName"] + "' and ISNULL(Status, '') in ('Paid', 'Pending') and isnull(cancel, '0')='0' order by Receiptno";
            }
            var dt = _oo.Fetchdata(_sql);
            if (dt.Rows.Count > 0)
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
                double total = 0;
                for (i = 0; i <= GridView1.Rows.Count - 1; i++)
                {
                    LinkButton LinkButton3 = (LinkButton)GridView1.Rows[i].FindControl("LinkButton3");
                    Label lblReceiptNo = (Label)GridView1.Rows[i].FindControl("lblReceiptNo");
                    Label lblpaid = (Label)GridView1.Rows[i].FindControl("lblpaid");
                    TextBox textboxRemark = (TextBox)GridView1.Rows[i].FindControl("txtboxremark");

                    _sql = "select sum(PaidAmount) PaidAmount from(";
                    _sql += " select isnull(sum(isnull(PaidAmount, 0)), 0) PaidAmount from CompositFeeDeposit where ReceiptNo='" + lblReceiptNo.Text + "' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and ISNULL(receiptStatus, '') in ('Paid', 'Pending')";
                    _sql += " union all";
                    _sql += " select isnull(sum(isnull(PaidAmt, 0)), 0) PaidAmount from OtherFeeDeposit where Receipt_no='" + lblReceiptNo.Text + "' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and ISNULL(PaymentSatus, '') in ('Paid', 'Pending') and isnull(cancel, '0')='0'";
                    _sql += " union all";
                    _sql += " select isnull(sum(isnull(ReceivedAmount, 0)), 0) PaidAmount from TCCollection where RecieptNo='" + lblReceiptNo.Text + "' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and ISNULL(Status, '') in ('Paid', 'Pending') and isnull(cancel, '0')='0'";
                    _sql += " union all";
                    _sql += " select isnull(sum(isnull(ReceivedAmount, 0)), 0) PaidAmount from CCCollection where RecieptNo='" + lblReceiptNo.Text + "' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and ISNULL(Status, '') in ('Paid', 'Pending') and isnull(cancel, '0')='0'";
                    _sql += " union all";
                    _sql += " select isnull(sum(isnull(ReceivedAmount, 0)), 0) PaidAmount from AdmissionFormCollection where RecieptNo='" + lblReceiptNo.Text + "' and BranchCode=" + Session["BranchCode"] + " and ISNULL(Status, '') in ('Paid', 'Pending') and isnull(cancel, '0')='0'";
                    _sql += " union all";
                    _sql += " select isnull(sum(isnull(ReceivedAmount, 0)), 0) PaidAmount from Other_fee_collection_1 where Receipt_no='" + lblReceiptNo.Text + "' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and ISNULL(Status, '') in ('Paid', 'Pending') and isnull(cancel, '0')='0'";
                    _sql += " union all";
                    _sql += " select isnull(sum(isnull(PaidAmt, 0)), 0) PaidAmount from UniformFeeDeposit where Receipt_no='" + lblReceiptNo.Text + "' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and ISNULL(PaymentSatus, '') in ('Paid', 'Pending') and isnull(cancel, '0')='0'";
                    _sql += " union all";
                    _sql += " select isnull(sum(isnull(FineAmount, 0)), 0) PaidAmount from ReturnBookFine where Receiptno='" + lblReceiptNo.Text + "' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and ISNULL(Status, '') in ('Paid', 'Pending') and isnull(cancel, '0')='0'";
                    _sql += " )T1";
                    lblpaid.Text = double.Parse(_oo.ReturnTag(_sql, "PaidAmount") == "" ? "0" : _oo.ReturnTag(_sql, "PaidAmount")).ToString("0.00");
                    total = total + double.Parse(lblpaid.Text);
                    if (i == (GridView1.Rows.Count - 1))
                    {
                        LinkButton3.Visible = true;
                        textboxRemark.Visible = true;
                        textboxRemark.Text = "NA";
                    }
                }
                Label fTotal = (Label)GridView1.FooterRow.FindControl("fTotal");
                fTotal.Text = total.ToString("0.00");
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            LinkButton chk = (LinkButton)sender;
            Label lblId = (Label)chk.NamingContainer.FindControl("Label13");
            Label LabelSrNo = (Label)chk.NamingContainer.FindControl("LabelSrNo");
            Label Labelmop = (Label)chk.NamingContainer.FindControl("Labelmop");
            Label lblpaid = (Label)chk.NamingContainer.FindControl("lblpaid");
            TextBox txtboxremark = (TextBox)chk.NamingContainer.FindControl("txtboxremark");
            //if (Labelmop.Text == "Cash")
            //{
            //if (Labelmop.Text == "Cash")
            //{
            //    string ss = "select top(1) Balance from AccDayBook where BranchCode=" + Session["BranchCode"] + " and PaymentMode='Cash' order by id desc";
            //    double Balance = double.Parse(_oo.ReturnTag(ss, "Balance") == "" ? "0" : _oo.ReturnTag(ss, "Balance"));
            //    double CancelAmt = double.Parse(lblpaid.Text == "" ? "0" : lblpaid.Text);
            //if (CancelAmt > Balance)
            //{
            //    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Insufficient cash balance in Accounts!", "A");
            //    return;
            //}
            //else
            //{
            //lblvalue.Text = lblId.Text;
            //lblSrNovalue.Text = LabelSrNo.Text;
            //cancelBal.Text = lblpaid.Text;
            //Mode.Text = Labelmop.Text;
            //Label3.Text = txtboxremark.Text;
            //Panel2_ModalPopupExtender.Show();
            //}
            //}
            //else
            //{
            if (Labelmop.Text != "Cash" && txtboxremark.Text == "")
            {
                txtboxremark.Focus();
            }
            else
            {
                lblvalue.Text = lblId.Text;
                lblSrNovalue.Text = LabelSrNo.Text;
                cancelBal.Text = lblpaid.Text;
                Mode.Text = Labelmop.Text;
                Label3.Text = txtboxremark.Text;
                Panel2_ModalPopupExtender.Show();
            }



            //}



            //}
            //else
            //{

            //}
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "RecieptCancilationProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = _con;
            cmd.Parameters.AddWithValue("@Srno", lblSrNovalue.Text);
            cmd.Parameters.AddWithValue("@Module", DrpTables.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@RecieptSrNo", lblvalue.Text);
            cmd.Parameters.AddWithValue("@cancelBal", cancelBal.Text);
            cmd.Parameters.AddWithValue("@Mode", Mode.Text);
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
            cmd.Parameters.AddWithValue("@Remark", Label3.Text);
            try
            {
                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Cancelled successfully", "S");
                loadfee();
                //SendFeesSms(Fmo, lblvalue.Text, lblSrNovalue.Text, IDFromFeeType());
                ComposeSMS();
            }
            catch (SqlException sex)
            {
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, sex.Message, "A");
            }
            catch (Exception ex)
            {
            }
        }

        public void ComposeSMS()
        {
            //  string userid = txtUserId.Text.ToString().Replace(" ", "");
            try
            {
                List<SqlParameter> param = new List<SqlParameter>()
                {
                    new SqlParameter("@SessionName",Session["SessionName"]),
                    new SqlParameter("@BranchCode",Session["BranchCode"]),
                    new SqlParameter("@ReceiptNo",lblvalue.Text.ToString().Replace(" ", ""))

                };
                DataSet ds = _oo.ReturnDataSet("USP_ReceiptCancellationTemplate", param.ToArray());
                if (ds != null && ds.Tables.Count > 0)
                {
                    string msg = SendSms(ds);
                }

            }
            catch
            {

            }

        }

        public string SendSms(DataSet ds)
        {
            string msg;
            try
            {
                DataTable data = new DataTable();
                data = ds.Tables[0];

                _sql = "select top(1) FamilyContactNo from StudentFamilyDetails  where srno='" + lblSrNovalue.Text + "' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"].ToString() + "' order by id desc";
                var contactNo = _oo.ReturnTag(_sql, "FamilyContactNo");

                DataTable template = new DataTable();
                template = ds.Tables[1];

                msg = template.Rows[0][0].ToString();
                string[] param = template.Rows[0][1].ToString().Split(',');

                string[] daynamicVariables = msg.Split(new char[0]);
                foreach (var para in param)
                {
                    string value = data.Rows[0][para].ToString();
                    for (int i = 0; i < daynamicVariables.Count(); i++)
                    {
                        if (daynamicVariables[i].ToString() == "{{{}}}")
                        {
                            daynamicVariables[i] = value;
                            break;
                        }
                    }
                }

                msg = string.Join(" ", daynamicVariables);

                new SMS().SendSms(contactNo, msg, "8", Session["BranchCode"].ToString());

            }
            catch
            {
                msg = "";
            }
            return msg;
        }

        public string IDFromFeeType()
        {
            string val = "0";
            switch (DrpTables.SelectedValue)
            {
                case "1":
                    val = "1";
                    break;
                case "2":
                    val = "17";
                    break;
                case "3":
                    val = "13";
                    break;
                case "4":
                    val = "4";
                    break;
                case "5":
                    val = "11";
                    break;
                case "6":
                    val = "7";
                    break;
                case "7":
                    val = "8";
                    break;
                case "8":
                    val = "";
                    break;
            }
            return val;
        }

        protected void Button8_Click(object sender, EventArgs e)
        {

        }
        public void SendFeesSms(string FmobileNo, string RecieptNo, string srno, string title)
        {
            SMSAdapterNew sadpNew = new SMSAdapterNew();
            string mess = "";

            _sql = "Select top(1) FirstName+' '+MiddleName+' '+LastName as StudentName   from StudentGenaralDetail";
            _sql += "  where SrNo='" + srno + "' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"].ToString() + "' order by id desc";

            //mess = " Dear Sir, Receipt No. " + lblvalue.Text + " is cancelled for S.R. No. " + srno + " ( " + _oo.ReturnTag(_sql, "StudentName") + " ).";
            switch (DrpTables.SelectedValue)
            {
                case "1":

                    break;
                case "6":
                    mess = "Receipt No." + RecieptNo + " has been cancelled of T.C. Fee. Refunded Amount is INR " + cancelBal.Text + " From, ";
                    break;
                case "7":
                    //Receipt No. {#var#} has been cancelled of C.C. Fee. Refunded Amount is INR {#var#}From, RRSLKO
                    mess = "Receipt No." + RecieptNo + " has been cancelled of C.C. Fee. Refunded Amount is INR " + cancelBal.Text + " From, ";
                    break;
            }

            string sms_response = "";
            if (FmobileNo != "")
            {
                _sql = "Select SmsSent From SmsEmailMaster where Id='" + title + "'  and BranchCode=" + Session["BranchCode"] + "";
                if (_oo.ReturnTag(_sql, "SmsSent").Trim() == "true")
                {
                    sms_response = sadpNew.Send(mess, FmobileNo, title);
                }
            }
        }
        protected void ButtonCancel_Click(object sender, EventArgs e)
        {

        }
        public override void Dispose()
        {
            _con.Dispose();
            _oo.Dispose();
        }


    }
}