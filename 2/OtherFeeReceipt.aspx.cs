using System;
using System.Data.SqlClient;

namespace _2
{
	public partial class OtherFeeReceipt : System.Web.UI.Page
	{
		private readonly SqlConnection con;
		private readonly Campus oo;
		private string sql;
		string OtherReciept;
		public OtherFeeReceipt()
		{
			con = new SqlConnection();
			oo = new Campus();
			sql = string.Empty;
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			//if ((string)Session["OtherReciept"] == "" || Session["OtherReciept"] == null)
			//{
			//    Response.Redirect("OtherFeeDeposit.aspx");
			//}
			if (Request.QueryString["OtherReciept"] == null)
			{
				Response.Redirect("OtherFeeDeposit.aspx");
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
                if (Session["IsDuplicate"].ToString() == "Yes")
				{
                    lblDuplicateStudent.Text = "(DUPLICATE)";
                    lblDuplicateSCHOOL.Text = "(DUPLICATE)";
                }
				else
				{
                    lblDuplicateStudent.Text = "(ORIGINAL)";
                    lblDuplicateSCHOOL.Text = "(ORIGINAL)";
                }

                DisplayStudentCopy();
				//if (Session["IsDuplicate"].ToString() == "Yes")
				//{
				//	recdeiptType.Text = "DUPLICATE";
				//	recdeiptType0.Text = "DUPLICATE";
				//}
				//else
				//{
				//	recdeiptType.Text = "ORIGINAL";
				//	recdeiptType0.Text = "ORIGINAL";
				//}
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
			OtherReciept = Request.QueryString["OtherReciept"].Replace("__", "/");
			sql = "select fd.Id,fd.srno, asr.classid, gender, Receipt_no RecieptNo,name StudentName,FatherName, case when PaymentSatus='Paid' then BounceCharges else 0 end BounceCharges,CombineClassName Class, ModeNo CheckDDNo,mode mop, PaymentSatus status, LoginName,BounceCharges, fd.BranchCode,format(fd.RecordedDate, 'dd MMM yyyy hh:mm:ss tt') as RecordDate, convert(nvarchar, ISNULL(DepositDate, RecordedDate), 106) as CCissuedate,Gender Sex, fd.NextDeuAmt, FatherContactNo, PaidAmt ReceivedAmount,Discount Concession, PaidAmt Amount,CancelledBy,FORMAT(CancelledDate,'dd MMM yyyy hh:mm:ss tt') CancelledDate from OtherFeeDeposit fd inner join AllStudentRecord_UDF('" + Session["SessionName"] + "', " + Session["BranchCode"] + ") asr on asr.SrNo = fd.SrNo where Receipt_no = '" + OtherReciept + "'";

			string sql2 = "select count(*) cnt from OtherFeeDeposit where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and SrNo='" + oo.ReturnTag(sql, "srno") + "'";
			if (int.Parse(oo.ReturnTag(sql2, "cnt")) > 1)
			{
				Label18.Text = "Other Fee Balance";
				lblAdmissionFees0.Text = "Other Fee Balance";
			}
			lblFooterDate0.Text = lblFooterDate.Text = oo.ReturnTag(sql, "RecordDate");
			lblUserName0.Text = lblUserName.Text = oo.ReturnTag(sql, "LoginName");
			string kk = "select Remark from OtherFeeHeadMaster where ClassId=" + oo.ReturnTag(sql, "classid") + " and Gender=case when '" + oo.ReturnTag(sql, "gender") + "'='Male' then 1 else case when '" + oo.ReturnTag(sql, "gender") + "'='Female' then 2 else 3 end end  and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and IsSingleHead=1";
			if (oo.Duplicate(kk))
			{
				lblRemark.Text = lblRemark0.Text = oo.ReturnTag(kk, "Remark");
			}
			else
			{
				lblRemark.Text = lblRemark0.Text = "";
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

			lblAmount.Text = simbol + (oo.ReturnTag(sql, "Amount") == "" ? "0" : oo.ReturnTag(sql, "Amount"));
			lblAmount0.Text = simbol + (oo.ReturnTag(sql, "Amount") == "" ? "0" : oo.ReturnTag(sql, "Amount"));

			lblConcessionAmount.Text = simbol + (oo.ReturnTag(sql, "Concession") == "" ? "0" : oo.ReturnTag(sql, "Concession"));
			lblConcessionAmount0.Text = simbol + (oo.ReturnTag(sql, "Concession") == "" ? "0" : oo.ReturnTag(sql, "Concession"));

			lblReceivedAmount.Text = simbol + (oo.ReturnTag(sql, "ReceivedAmount") == "" ? "0" : oo.ReturnTag(sql, "ReceivedAmount"));
			lblReceivedAmount0.Text = simbol + (oo.ReturnTag(sql, "ReceivedAmount") == "" ? "0" : oo.ReturnTag(sql, "ReceivedAmount"));

			lblchkBouncefee.Text = simbol + (oo.ReturnTag(sql, "BounceCharges") == "" ? "0" : oo.ReturnTag(sql, "BounceCharges"));
			lblchkBouncefee0.Text = simbol + (oo.ReturnTag(sql, "BounceCharges") == "" ? "0" : oo.ReturnTag(sql, "BounceCharges"));

			lblNextBalancemount.Text = simbol + (oo.ReturnTag(sql, "NextDeuAmt") == "" ? "0" : oo.ReturnTag(sql, "NextDeuAmt"));
			lblNextBalancemount0.Text = simbol + (oo.ReturnTag(sql, "NextDeuAmt") == "" ? "0" : oo.ReturnTag(sql, "NextDeuAmt"));


			double Bouncefee;
			double Concession;
			double Amount;
			double RAmount;
			double NextBalance;

			NextBalance = Convert.ToDouble((oo.ReturnTag(sql, "NextDeuAmt") == "" ? "0" : oo.ReturnTag(sql, "NextDeuAmt")));
			Bouncefee = Convert.ToDouble((oo.ReturnTag(sql, "BounceCharges") == "" ? "0" : oo.ReturnTag(sql, "BounceCharges")));
			Concession = Convert.ToDouble((oo.ReturnTag(sql, "Concession") == "" ? "0" : oo.ReturnTag(sql, "Concession")));
			Amount = Convert.ToDouble((oo.ReturnTag(sql, "Amount") == "" ? "0" : oo.ReturnTag(sql, "Amount")));
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
			if (NextBalance > 0)
			{
				divNextDue.Visible = true;
				divNextDue0.Visible = true;
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
            if (lblStatus.Text == "Cancelled")
            {
                Label2.Text = oo.ReturnTag(sql, "CancelledBy");
                Label34.Text = oo.ReturnTag(sql, "CancelledBy");

                Label22.Text = oo.ReturnTag(sql, "CancelledDate");
                Label39.Text = oo.ReturnTag(sql, "CancelledDate");
                if (Label22.Text != "")
                {
                    grdfeeDetailscancelleddiv.Visible = true;
                    grdfeeDetailscancelleddiv1.Visible = true;
                }
            }

            string remarkSql = "Select FeeReceiptRemark1, FeeReceiptRemark2 from CollegeMaster where BranchCode='" + Session["BranchCode"] +"'";
			string remark1 = oo.ReturnTag(remarkSql, "FeeReceiptRemark1");
            string remark2 = oo.ReturnTag(remarkSql, "FeeReceiptRemark2");

			lblRemark1.Text = remark1;
            lblRemark2.Text = remark2;

            lblRemark1_1.Text = remark1;
            lblRemark2_1.Text = remark2;


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