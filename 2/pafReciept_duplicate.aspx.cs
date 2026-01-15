using System;

namespace _2
{
    public partial class AdminPafRecieptDuplicate : System.Web.UI.Page
    {
        private readonly Campus _oo;
        private string _sql, _lblAdmissionType = String.Empty;

        public AdminPafRecieptDuplicate()
        {
            _oo = new Campus();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            _oo.dbGet_connection();
            //if (Session["AdmissionRecieptNo"] == null)
            //{
            //    Response.Redirect("AdmissionForm_new.aspx");
            //}
            if (Request.QueryString["AdmissionRecieptNo"] == null)
            {
                Response.Redirect("AdmissionForm_new.aspx");
            }
            if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
            {
                Response.Redirect("default.aspx");
            }
            BLL.BLLInstance.LoadHeader("Receipt", header1);
            BLL.BLLInstance.LoadHeader("Receipt", header2);
            if (!IsPostBack)
            {
                DisplayStudentCopy();
            }

        }

        public void DisplayStudentCopy()
        {
            string simbol = "";
            string simbolsql = "select top(1) CurrencySymbols from setting";
            if (_oo.ReturnTag(simbolsql, "CurrencySymbols") != "")
            {
                simbol = "&#" + _oo.ReturnTag(simbolsql, "CurrencySymbols") + ";&nbsp;";
            }
            //sql = "select Id,RecieptNo,convert(nvarchar,AdmissionFromDate,106) as AdmissionFromDate,MOP,Concession,ReceivedAmount,StudentName+' '+MiddleName +' '+LastName as StudentName,FatherName,Class,LoginName,BranchCode,convert(nvarchar,RecordDate,106) as RecordDate,Sex,FatherContactNo,Amount from AdmissionFormCollection where RecieptNo='" + Session["AdmissionRecieptNo"].ToString() + "'";

            _sql = "select afc.Id,RecieptNo,convert(nvarchar,AdmissionFromDate,106) as AdmissionFromDate,MOP, afc.AdmissionType, afc.Class,";
            _sql += " Concession,ReceivedAmount,ChqBounceAmount,StudentName+' '+MiddleName +' '+LastName as StudentName,afc.FatherName,Class,";
            _sql += " afc.LoginName,afc.BranchCode,Sex, cm.id Classid, afc.sessionName,  case when isnull(afc.CheckDDNo, '') in ('','na', 'N/A') then '' else ' ('+isnull(afc.CheckDDNo, '')+')' end chequeNo, ";
            _sql += " afc.FatherContactNo,Amount Amount,bm.BranchName,afc.RecordDate,afc.AdmissionType,afc.LoginName,afc.Status,afc.CancelledBy,FORMAT(afc.CancelledDate,'dd MMM yyyy hh:mm:ss tt') CancelledDate  from AdmissionFormCollection afc";
            _sql += " Inner join ClassMaster cm on cm.ClassName=afc.Class and cm.SessionName=afc.SessionName and cm.BranchCode=afc.BranchCode ";
            _sql += " Left join BranchMaster bm on bm.BranchName=afc.Branch and bm.Classid=cm.Id and bm.SessionName=afc.SessionName  and bm.BranchCode=afc.BranchCode and IsDisplay='1'";
            _sql += " where RecieptNo='" + Request.QueryString["AdmissionRecieptNo"].Replace("__", "/") + "' and afc.BranchCode=" + Session["BranchCode"] + "";


            string sqlss = "select FeeHead from AdmissionFormFeeMaster  where AdminssionType='" + _oo.ReturnTag(_sql, "AdmissionType") + "' and Classid=" + _oo.ReturnTag(_sql, "Classid") + " and BranchCode=" + Session["BranchCode"] + " and SessionName='" + _oo.ReturnTag(_sql, "SessionName") + "'";
            string feehead = _oo.ReturnTag(sqlss, "FeeHead");

            lblFooterDate0.Text = lblFooterDate.Text = DateTime.Parse(_oo.ReturnTag(_sql, "RecordDate")).ToString("dd MMM yyyy hh:mm:ss tt");
            lblUserName0.Text = lblUserName.Text = _oo.ReturnTag(_sql, "LoginName");

            Label1.Text = _oo.ReturnTag(_sql, "RecieptNo");
            lblRecieptNo.Text = _oo.ReturnTag(_sql, "RecieptNo");
            lblRecieptNo0.Text = _oo.ReturnTag(_sql, "RecieptNo");

            lblStudentName.Text = _oo.ReturnTag(_sql, "StudentName");
            lblStudentName0.Text = _oo.ReturnTag(_sql, "StudentName");

            lblRecieptDate.Text = _oo.ReturnTag(_sql, "AdmissionFromDate");
            lblRecieptDate0.Text = _oo.ReturnTag(_sql, "AdmissionFromDate");

            lblFatherName.Text = _oo.ReturnTag(_sql, "FatherName");
            lblFatherName0.Text = _oo.ReturnTag(_sql, "FatherName");

            lblStatus.Text = _oo.ReturnTag(_sql, "Status");
            lblStatus0.Text = _oo.ReturnTag(_sql, "Status");

            lblSession.Text = _oo.ReturnTag(_sql, "SessionName");
            lblSession0.Text = _oo.ReturnTag(_sql, "SessionName");

            //lblClass.Text = oo.ReturnTag(sql, "Class");

            //lblBranch.Text = oo.ReturnTag(sql, "BranchName");
            var admissionType = BAL.objBal.ReturnTag(_sql, "AdmissionType").Trim();
            if (admissionType == "New (Provisional)")
            {
                _lblAdmissionType = admissionType.Replace("New", "");
            }
            else
            {
                _lblAdmissionType = "";
            }

            lblAdmissionFees.Text = lblAdmissionFees0.Text = feehead + " of " + _oo.ReturnTag(_sql, "Class") + " " + _oo.ReturnTag(_sql, "BranchName");

            lblMode.Text = _oo.ReturnTag(_sql, "MOP") + _oo.ReturnTag(_sql, "chequeNo");
            lblMode0.Text = _oo.ReturnTag(_sql, "MOP") + _oo.ReturnTag(_sql, "chequeNo");

            lblContactNo.Text = _oo.ReturnTag(_sql, "FatherContactNo");
            lblContactNo0.Text = _oo.ReturnTag(_sql, "FatherContactNo");

            lblAmount.Text = simbol + _oo.ReturnTag(_sql, "Amount");
            lblAmount0.Text = simbol + _oo.ReturnTag(_sql, "Amount");

            // ReSharper disable once RedundantAssignment
            double cbamount = 0;
            double.TryParse(_oo.ReturnTag(_sql, "ChqBounceAmount"), out cbamount);
            if (cbamount > 0)
            {
                chqBFee.Visible = true;
                chqBFee0.Visible = true;
            }
            else
            {
                chqBFee.Visible = false;
                chqBFee0.Visible = false;
            }
            lblchkBouncefee.Text = simbol + _oo.ReturnTag(_sql, "ChqBounceAmount");
            lblchkBouncefee0.Text = simbol + _oo.ReturnTag(_sql, "ChqBounceAmount");

            lblConcession.Text = simbol + _oo.ReturnTag(_sql, "Concession");
            lblConcession1.Text = simbol + _oo.ReturnTag(_sql, "Concession");

            lblReceivedAmount.Text = simbol + _oo.ReturnTag(_sql, "ReceivedAmount");
            lblReceivedAmount1.Text = simbol + _oo.ReturnTag(_sql, "ReceivedAmount");

            Label133.Text = _oo.ReturnTag(_sql, "CancelledBy").ToString();
            Label10.Text = _oo.ReturnTag(_sql, "CancelledBy").ToString();

            Label155.Text = _oo.ReturnTag(_sql, "CancelledDate").ToString();
            Label27.Text = _oo.ReturnTag(_sql, "CancelledDate").ToString();

            if (_oo.ReturnTag(_sql, "CancelledDate").ToString() != "")
            {
                grdfeeDetailscancelleddiv1.Visible = true;
                grdfeeDetailscancelleddiv.Visible = true;
            }

            // ReSharper disable once RedundantAssignment
            // ReSharper disable once RedundantAssignment
            try
            {
                decimal amt = 0;
                amt = Convert.ToDecimal(_oo.ReturnTag(_sql, "ReceivedAmount"));
                lblAmountinWords.Text = _oo.NumberToString(amt);
                lblAmountinWords0.Text = _oo.NumberToString(amt);
            }
            catch (Exception)
            {
                // ignored
            }

            string sql = "Select FeeReceiptRemark1, FeeReceiptRemark2 from CollegeMaster where BranchCode =" + Session["BranchCode"].ToString();
            lblRemark1.Text = _oo.ReturnTag(sql, "FeeReceiptRemark1").ToString();
            lblRemark2.Text = _oo.ReturnTag(sql, "FeeReceiptRemark1").ToString();
            if (lblRemark1.Text == "" || lblRemark1.Text.Length == 0)
            {
                v_hr.Visible = false;
                v_hr1.Visible = false;
                v_r1.Visible = false;
                v_r2.Visible = false;
            }
        }

        protected void LinkButton4_Click(object sender, EventArgs e)
        {

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            // LaserPrint();
            PrintHelper_New.ctrl = abc;
            ClientScript.RegisterStartupScript(GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
        }

        public override void Dispose()
        {
            _oo.Dispose();
        }
    }
}