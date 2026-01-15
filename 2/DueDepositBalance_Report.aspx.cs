using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI.WebControls;

namespace _2
{
    public partial class DueDepositBalance_Report : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
            {
                Response.Redirect("default.aspx");
            } 
            Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file
            BLL.BLLInstance.LoadHeader("Report", header);
            if (!IsPostBack)
            {
                LoadClass();
                LoadSection();
                LoadBranch();
                loadInstallments();
            }
        }

        public void LoadClass()
        {
            BLL.BLLInstance.loadClass(ddlClass, Session["SessionName"].ToString());
        }

        public void loadInstallments()
        {
            BLL.BLLInstance.loadInsttalment(ddlInstallment, ddlClass.SelectedValue, Session["SessionName"].ToString());
        }

        public void LoadSection()
        {
            BLL.BLLInstance.loadSection(ddlSection, Session["SessionName"].ToString(), ddlClass.SelectedValue);
            if(ddlSection.Items.Count==2)
            {
                ddlSection.SelectedIndex = 1;
            }
        }

        public void LoadBranch()
        {
            BLL.BLLInstance.loadBranch(ddlBranch, Session["SessionName"].ToString(), ddlClass.SelectedValue);
            if (ddlBranch.Items.Count == 2)
            {
                ddlBranch.SelectedIndex = 1;
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            ShowDetails();
        }

        protected void ShowDetails()
        {
            GetBalanceAmountParticularStudent();
        }

        public void GetBalanceAmountParticularStudent()
        {
            var ds = new DataSet();
            var param = new List<SqlParameter>
                        {
                            new SqlParameter("@ClassId",ddlClass.SelectedValue),
                            new SqlParameter("@SectionId",ddlSection.SelectedValue),
                            new SqlParameter("@BranchId",ddlBranch.SelectedValue),
                            new SqlParameter("@SessionName",Session["SessionName"].ToString()),
                            new SqlParameter("@BranchCode",Session["BranchCode"].ToString()),
                            new SqlParameter("@InstallmentId",ddlInstallment.SelectedValue)
                        };

            ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("USP_Get_DueDepositBalance", param);

            GridView1.DataSource = ds;
            GridView1.DataBind();

            if (GridView1.Rows.Count == 0)
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Sorry, Please Calculate Due Deposit Balance for this Installment!", "A");
                DivExport.Visible = false;
            }
            else
            {
                lblSession.Text = ddlInstallment.SelectedItem.Text + " ( " + Session["SessionName"] + " )";
                lblTitle.Text = BAL.objBal.CurrentDate();
                DivExport.Visible = true;
            }
        }

        protected void TxtEnter_TextChanged(object sender, EventArgs e)
        {
            ShowDetails();
        }

        protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSection();
            LoadBranch();
            loadInstallments();
        }

        decimal totalfee = 0, totalTransport = 0, totalFine = 0, totalExemption = 0, totalTotal = 0, totalPaidFeeAmount = 0, totalBalanceAmount=0;

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                decimal fee = 0, Transport = 0, Fine = 0, Exemption = 0, Total = 0, PaidFeeAmount = 0, BalanceAmount = 0;

                Label lblFee = (Label)e.Row.FindControl("lblFee");
                Label lblTransport = (Label)e.Row.FindControl("lblTransport");
                Label lblFine = (Label)e.Row.FindControl("lblFine");
                Label lblExemption = (Label)e.Row.FindControl("lblExemption");
                Label lblTotal = (Label)e.Row.FindControl("lblTotal");
                Label lblPaidFeeAmount = (Label)e.Row.FindControl("lblPaidAmount");
                Label lblBalanceAmount = (Label)e.Row.FindControl("lblBalance");

                decimal.TryParse(lblFee.Text, out fee);
                totalfee += fee;

                decimal.TryParse(lblTransport.Text, out Transport);
                totalTransport += Transport;

                decimal.TryParse(lblFine.Text, out Fine);
                totalFine += Fine;

                decimal.TryParse(lblExemption.Text, out Exemption);
                totalExemption += Exemption;

                decimal.TryParse(lblTotal.Text, out Total);
                totalTotal += Total;

                decimal.TryParse(lblPaidFeeAmount.Text, out PaidFeeAmount);
                totalPaidFeeAmount += PaidFeeAmount;

                decimal.TryParse(lblBalanceAmount.Text, out BalanceAmount);
                totalBalanceAmount += BalanceAmount;
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotalFee = (Label)e.Row.FindControl("lblTotalFee");
                Label lblTotalTransport = (Label)e.Row.FindControl("lblTotalTransport");
                Label lblTotalFine = (Label)e.Row.FindControl("lblTotalFine");
                Label lblTotalExemption = (Label)e.Row.FindControl("lblTotalExemption");
                Label lblTotalTotal = (Label)e.Row.FindControl("lblTotalTotal");
                Label lblTotalPaidFeeAmount = (Label)e.Row.FindControl("lblTotalPaid");
                Label lblTotalBalanceAmount = (Label)e.Row.FindControl("lblTotalBalance");

                lblTotalFee.Text = totalfee.ToString(CultureInfo.InvariantCulture);
                lblTotalTransport.Text = totalTransport.ToString(CultureInfo.CurrentCulture);
                lblTotalFine.Text = totalFine.ToString(CultureInfo.InvariantCulture);
                lblTotalExemption.Text = totalExemption.ToString(CultureInfo.InvariantCulture);
                lblTotalTotal.Text = totalTotal.ToString(CultureInfo.InvariantCulture);
                lblTotalPaidFeeAmount.Text = totalPaidFeeAmount.ToString(CultureInfo.InvariantCulture);
                lblTotalBalanceAmount.Text = totalBalanceAmount.ToString(CultureInfo.InvariantCulture);
            }
        }

        protected void ImageButton1_Click(object sender, EventArgs e)
        {
            BAL.objBal.ExportToWord(Response, "AdmisionFormCollection.doc", DivExport);
        }
        protected void ImageButton2_Click(object sender, EventArgs e)
        {
            BAL.objBal.ExportDivToExcel(Response, "AdmisionFormCollection.xls", DivExport);
        }
        protected void ImageButton3_Click(object sender, EventArgs e)
        {
            BAL.objBal.ExporttoPdf(Response, "AdmisionFormCollection.doc", DivExport);
        }
        protected void ImageButton4_Click(object sender, EventArgs e)
        {
            PrintHelper_New.ctrl = DivExport;
            ClientScript.RegisterStartupScript(GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
        }
    }
}