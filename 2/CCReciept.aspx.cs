using System;
using System.Data.SqlClient;

namespace _2
{
    public partial class AdminCCReciept : System.Web.UI.Page
    {
        private readonly SqlConnection con;
        private readonly Campus oo;
        private string sql;

        string CCRecieptNo = "";
        public AdminCCReciept()
        {
            con = new SqlConnection();
            oo = new Campus();
            sql = string.Empty;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["CCRecieptNo"] == null)
            {
                Response.Redirect("CCCollection.aspx");
            }
            if ((string)Session["LoginName"] == "" || (string)Session["BranchCode"] == "" || (string)Session["SessionName"] == "" || Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
            {
                Response.Redirect("default.aspx");
            }
            if (!IsPostBack)
            {
                BLL obj = new BLL();
                obj.LoadHeader("Receipt", header1);
                obj.LoadHeader("Receipt", header2);
                CCRecieptNo = Request.QueryString["CCRecieptNo"].Replace("__", "/");
                DisplayStudentCopy();
                if (Session["IsDuplicate"].ToString() == "Yes")
                {
                    recdeiptType.Text = "DUPLICATE";
                    recdeiptType0.Text = "DUPLICATE";
                }
                else
                {
                    recdeiptType.Text = "ORIGINAL";
                    recdeiptType0.Text = "ORIGINAL";
                }
            }
        }

        public void DisplayStudentCopy()
        {
            string simbol = "";
            string simbolsql = "select top(1) CurrencySymbols from setting";
            if (oo.ReturnTag(simbolsql, "CurrencySymbols") != "")
            {
                simbol = "&#" + oo.ReturnTag(simbolsql, "CurrencySymbols") + ";&nbsp;";
            }
            sql = "select Id,srno,RecieptNo,StudentName,FatherName,Class,format(RecordDate, 'dd-MMM-yyyy hh:mm:ss tt') RecordDate,CheckDDNo,mop, status, LoginName, case when Status='Paid' then BounceCharges else 0 end BounceCharges,BranchCode,convert(nvarchar,RecordDate,106) as RecordDate,convert(nvarchar,ISNULL(CCissuedate,RecordDate),106) as CCissuedate,Sex,FatherContactNo,ReceivedAmount,Concession,Amount,CancelledBy,FORMAT(CancelledDate,'dd MMM yyyy hh:mm:ss tt') CancelledDate from CCCollection where RecieptNo='" + CCRecieptNo + "'";

            lblFooterDate0.Text = lblFooterDate.Text = oo.ReturnTag(sql, "RecordDate");
            lblUserName0.Text = lblUserName.Text = oo.ReturnTag(sql, "LoginName");

            if (oo.ReturnTag(sql, "CancelledDate").ToString() != "")
            {
                grdfeeDetailscancelleddiv1.Visible = true;
                grdfeeDetailscancelleddiv.Visible = true;
                Label133.Text = Label28.Text = oo.ReturnTag(sql, "CancelledBy");
                Label155.Text = Label30.Text = oo.ReturnTag(sql, "CancelledDate");
            }


            Label1.Text = oo.ReturnTag(sql, "RecieptNo");
            lblSrno.Text = oo.ReturnTag(sql, "srno");
            lblSrno0.Text = oo.ReturnTag(sql, "Srno");

            lblRecieptNo.Text = oo.ReturnTag(sql, "RecieptNo");
            lblRecieptNo0.Text = oo.ReturnTag(sql, "RecieptNo");

            lblStudentName.Text = oo.ReturnTag(sql, "StudentName");
            lblStudentName0.Text = oo.ReturnTag(sql, "StudentName");

            lblRecieptDate.Text = oo.ReturnTag(sql, "CCissuedate");
            lblRecieptDate0.Text = oo.ReturnTag(sql, "CCissuedate");

            lblFatherName.Text = oo.ReturnTag(sql, "FatherName");
            lblFatherName0.Text = oo.ReturnTag(sql, "FatherName");

            lblClass.Text = oo.ReturnTag(sql, "Class");
            lblClass0.Text = oo.ReturnTag(sql, "Class");


            lblMode.Text = oo.ReturnTag(sql, "mop") + (oo.ReturnTag(sql, "CheckDDNo").ToString() != "" ? " (" + oo.ReturnTag(sql, "CheckDDNo").ToString() + ")" : "");
            lblMode0.Text = oo.ReturnTag(sql, "mop") + (oo.ReturnTag(sql, "CheckDDNo").ToString() != "" ? " (" + oo.ReturnTag(sql, "CheckDDNo").ToString() + ")" : "");

            lblStatus.Text = oo.ReturnTag(sql, "status");
            lblStatus0.Text = oo.ReturnTag(sql, "status");

            lblAmount.Text = simbol + oo.ReturnTag(sql, "Amount");
            lblAmount0.Text = simbol + oo.ReturnTag(sql, "Amount");

            lblConcessionAmount.Text = simbol + oo.ReturnTag(sql, "Concession");
            lblConcessionAmount0.Text = simbol + oo.ReturnTag(sql, "Concession");

            lblReceivedAmount.Text = simbol + oo.ReturnTag(sql, "ReceivedAmount");
            lblReceivedAmount0.Text = simbol + oo.ReturnTag(sql, "ReceivedAmount");

            lblchkBouncefee.Text = simbol + (oo.ReturnTag(sql, "BounceCharges") == "" ? "0" : oo.ReturnTag(sql, "BounceCharges"));
            lblchkBouncefee0.Text = simbol + (oo.ReturnTag(sql, "BounceCharges") == "" ? "0" : oo.ReturnTag(sql, "BounceCharges"));

            double Bouncefee;
            double Concession;
            double Amount;
            double RAmount;

            Bouncefee = Convert.ToDouble((oo.ReturnTag(sql, "BounceCharges") == "" ? "0" : oo.ReturnTag(sql, "BounceCharges")));
            Concession = Convert.ToDouble(oo.ReturnTag(sql, "Concession"));
            Amount = Convert.ToDouble(oo.ReturnTag(sql, "Amount"));
            RAmount = (Amount + Bouncefee) - Concession;
            if (Concession > 0)
            {
                divConcession.Visible = true;
                divConcession0.Visible = true;
            }
            if (Bouncefee > 0)
            {
                chqBFee.Visible = true;
                chqBFee0.Visible = true;
            }
            //double amt = 0;
            decimal num = 0;
            //try
            //{
            //    amt = Convert.ToDouble(lblAmount.Text);
            num = Convert.ToDecimal(RAmount);
            lblAmountinWords.Text = oo.NumberToString(num);
            lblAmountinWords0.Text = oo.NumberToString(num);
            //}
            //catch (Exception) { }

            string ss = "select classname from AllStudentRecord_UDF('" + Session["SessionName"] + "','" + Session["BranchCode"] +"') where srno='" + lblSrno.Text + "'";
            lblClass.Text = oo.ReturnTag(ss, "classname");
            lblClass0.Text = lblClass.Text;
        }


        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            // LaserPrint();
            BLL obj = new BLL();
            obj.LoadHeader("Receipt", header1);
            obj.LoadHeader("Receipt", header2);
            PrintHelper_New.ctrl = abc;
            ClientScript.RegisterStartupScript(GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
        }

        public override void Dispose()
        {
            con.Dispose();
            oo.Dispose();
        }
    }
}