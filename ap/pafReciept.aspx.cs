using System;
using System.Data.SqlClient;
using System.Web.UI;

namespace _2
{
    public partial class AdminPafReciept : Page
    {
        SqlConnection _con;
        private readonly Campus _oo;
        private string _sql, _lblAdmissionType = string.Empty;

        public AdminPafReciept()
        {
            _con = new SqlConnection();
            _oo = new Campus();
        }

        protected void Page_PreInIt(object sender, EventArgs e)
        {
            if (Session["Logintype"] == null)
            {
                //Response.Redirect("~/default.aspx");
            }
            if (string.IsNullOrEmpty(Request.QueryString["rid"]))
            {
                if (Session["AdmissionRecieptNo"] == null)
                {
                    Response.Redirect("AdmissionForm.aspx");
                }
                if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
                {
                    Response.Redirect("default.aspx");
                }
            }
            else
            {
                MasterPageFile = "~/ap/main-ap.master";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            _con = _oo.dbGet_connection();
            BLL.BLLInstance.LoadHeader("Receipt", header1);
            BLL.BLLInstance.LoadHeader("Receipt", header2);
            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(Request.QueryString["rid"]))
                {
                    DisplayStudentCopy((string)Session["AdmissionRecieptNo"]);
                    LinkButton2.PostBackUrl = "paf.aspx";
                }
                else
                {
                    DisplayStudentCopy(Request.QueryString["rid"]);
                    //LinkButton2.PostBackUrl = "../ap/Admission_Details.aspx?txtno=" + ViewState["txnid"] + "";
                }
            }
            if (string.IsNullOrEmpty(Request.QueryString["rid"]))
            {
                
            }
            else
            {
                //ScriptManager.RegisterClientScriptBlock(Page, GetType(), "redirect", "window.open('../ap/Admission_Details.aspx?txtno=" + ViewState["txnid"] + "','_self')", true);
            }
        }

        public void DisplayStudentCopy(string admissionRecieptNo)
        {
            string simbol = "";
            string simbolsql = "select top(1) CurrencySymbols from setting";
            if (_oo.ReturnTag(simbolsql, "CurrencySymbols") != "")
            {
                simbol = "&#" + _oo.ReturnTag(simbolsql, "CurrencySymbols") + ";&nbsp;";
            }
            //sql = "select Id,RecieptNo,convert(nvarchar,AdmissionFromDate,106) as AdmissionFromDate,MOP,Concession,ReceivedAmount,StudentName+' '+MiddleName +' '+LastName as StudentName,FatherName,Class,LoginName,BranchCode,convert(nvarchar,RecordDate,106) as RecordDate,Sex,FatherContactNo,Amount from AdmissionFormCollection where RecieptNo='" + Session["AdmissionRecieptNo"].ToString() + "'";

            _sql = "select afc.Id,RecieptNo,convert(nvarchar,AdmissionFromDate,106) as AdmissionFromDate,MOP, afc.AdmissionType, afc.Class,";
            _sql = _sql + " Concession,ReceivedAmount,ChqBounceAmount,StudentName+' '+MiddleName +' '+LastName as StudentName,afc.FatherName,Class,";
            _sql = _sql + " afc.LoginName,afc.BranchCode,Sex, cm.id Classid, ";
            _sql = _sql + " afc.FatherContactNo,Amount Amount,bm.BranchName,afc.RecordDate,afc.AdmissionType,afc.LoginName  from AdmissionFormCollection afc";
            _sql = _sql + " Inner join ClassMaster cm on cm.ClassName=afc.Class and cm.SessionName=afc.SessionName and cm.BranchCode=afc.BranchCode ";
            _sql = _sql + " Left join BranchMaster bm on bm.BranchName=afc.Branch and bm.Classid=cm.Id and bm.SessionName=afc.SessionName  and bm.BranchCode=afc.BranchCode and IsDisplay='1'";
            _sql = _sql + " where RecieptNo='" + admissionRecieptNo + "' and afc.BranchCode=" + Session["BranchCode"]+ " and afc.SessionName='" + Session["SessionName"] + "'";

            string sqlss = "select FeeHead from AdmissionFormFeeMaster  where AdminssionType='" + _oo.ReturnTag(_sql, "AdmissionType") + "' and Classid=" + _oo.ReturnTag(_sql, "Classid") + " and BranchCode=" + Session["BranchCode"] +" and SessionName='" + Session["SessionName"] + "'";
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
            ViewState["txnid"] = _oo.ReturnTag(_sql, "txnid");
            
            var admissionType = BAL.objBal.ReturnTag(_sql, "AdmissionType").Trim();
            if (admissionType == "New (Provisional)")
            {
                _lblAdmissionType = admissionType.Replace("New", "");
            }
            else
            {
                _lblAdmissionType = "";
            }
            lblAdmissionFees.Text=lblAdmissionFees0.Text = feehead + " of " + _oo.ReturnTag(_sql, "Class") + " " + _oo.ReturnTag(_sql, "BranchName");

            lblMode.Text = _oo.ReturnTag(_sql, "MOP");
            lblMode0.Text = _oo.ReturnTag(_sql, "MOP");

            lblContactNo.Text = _oo.ReturnTag(_sql, "FatherContactNo");
            lblContactNo0.Text = _oo.ReturnTag(_sql, "FatherContactNo");
            Session["txnchk"] = lblContactNo.Text;
            lblAmount.Text = simbol + _oo.ReturnTag(_sql, "Amount");
            lblAmount0.Text = simbol + _oo.ReturnTag(_sql, "Amount");

            // ReSharper disable once IdentifierTypo
            double cbamount;
            double.TryParse(_oo.ReturnTag(_sql, "ChqBounceAmount"), out cbamount);
            if(cbamount>0)
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

            try
            {
                decimal  amt = Convert.ToDecimal(_oo.ReturnTag(_sql, "ReceivedAmount"));
                lblAmountinWords.Text = _oo.NumberToString(amt);
                lblAmountinWords0.Text = _oo.NumberToString(amt);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        protected void LinkButton4_Click(object sender, EventArgs e)
        {

        }
        protected void LinkButton2_OnClick(object sender, EventArgs e)
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
            _con.Dispose();
            _oo.Dispose();
        }

       
    }
}