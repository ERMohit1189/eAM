using System;
using System.Data;
using System.Web.UI.WebControls;

namespace _7
{
    public partial class EmployeeDayBookReport : System.Web.UI.Page
    {
        public string Msg = "", Sql = "";
        private DataTable _dt;

        public EmployeeDayBookReport()
        {
            _dt = new DataTable();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Campus camp = new Campus(); camp.LoadLoader(loader);

            try
            {
                if (!IsPostBack)
                {
                    txtFromDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                    txtToDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }
        private void Reset()
        {
            txtFromDate.Text = string.Empty;
            txtToDate.Text = string.Empty;

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            GetDayBookReport();
        }
        protected void txtEmpID_TextChanged(object sender, EventArgs e)
        {
            gvDayBook.DataSource = null;
            gvDayBook.DataBind();
            GetDayBookReport();
        }
        private void GetDayBookReport()
        {
            gvDayBook.DataSource = null;
            gvDayBook.DataBind();
            var EmpIDId = Request.Form[hfEmpID.UniqueID];
            if (string.IsNullOrEmpty(EmpIDId))
            {
                EmpIDId = txtEmpID.Text.Trim();
            }
            if (EmpIDId=="")
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Please enter employee id!", "A");
                return;
            }
            _dt = null;

                    BAL.clsDayBook obj = new BAL.clsDayBook();
                    obj.H03ID = (Int32)Default.All;
                    obj.IsActive = (Int32)Default.Yes;
                    obj.FromDate = Convert.ToDateTime(txtFromDate.Text.Trim() == string.Empty ? "01/01/1900" : txtFromDate.Text.Trim());
                    obj.ToDate = Convert.ToDateTime(txtToDate.Text.Trim() == string.Empty ? "01/01/1900" : txtToDate.Text.Trim());


                    if (_dt != null && _dt.Rows.Count > 0)
                    {
                        _dt = BLL.BLLInstance.GetSerialNo(ref _dt, "SrNo");
                        gvDayBook.DataSource = _dt;

                    }
                    else
                    {
                        gvDayBook.DataSource = null;
                        Campus camp = new Campus(); camp.msgbox(Page, msgbox, new ShowMSG().MSG("N"), "A");
                    }
                    gvDayBook.DataBind();
                    if (_dt != null && _dt.Rows.Count > 0)
                    {
                        //double CRTotals = 0;
                        //double DRTotals = 0;
                        double Balance = 0; 
                        //for (int i = 0; i < gvDayBook.Rows.Count; i++)
                        //{
                        //    Label CR = (Label)gvDayBook.Rows[i].FindControl("Label7");
                        //    Label DR = (Label)gvDayBook.Rows[i].FindControl("Label8");
                        //    Label Balances = (Label)gvDayBook.Rows[i].FindControl("label10");
                        //    CRTotals = CRTotals + double.Parse(CR.Text);
                        //    DRTotals = DRTotals + double.Parse(DR.Text);
                        //    if ((gvDayBook.Rows.Count-1)==i)
                        //    {
                        //        Balance = Balance + double.Parse(Balances.Text);
                        //    }
                            
                        //}
                        Label CRTotal = (Label)gvDayBook.FooterRow.FindControl("Label7F");
                        Label DRTotal = (Label)gvDayBook.FooterRow.FindControl("Label8F");
                        Label BalanceTotal = (Label)gvDayBook.FooterRow.FindControl("Label10F");
                        //CRTotal.Text = CRTotals.ToString("0.00");
                        //DRTotal.Text = DRTotals.ToString("0.00");
                        //BalanceTotal.Text = Balance.ToString("0.00");

                        obj.FromDate = Convert.ToDateTime("01/01/2010");
                        if (EmpIDId == "")
                        {
                        }
                        else
                        {
                        }

                        double CRT = 0;
                        double DRT = 0;
                        for (int i = 0; i < _dt.Rows.Count; i++)
                        {
                            Label invNo = (Label)gvDayBook.Rows[i].FindControl("invNo");
                            HyperLink invPath = (HyperLink)gvDayBook.Rows[i].FindControl("invPath");
                            if (_dt.Rows[i]["InvoiceNo"].ToString() == "" && _dt.Rows[i]["InvoicePath"].ToString() != "")
                            {
                                invNo.Visible = false;
                                invPath.Visible = true;
                            }
                            if (_dt.Rows[i]["InvoiceNo"].ToString() != "" && _dt.Rows[i]["InvoicePath"].ToString() == "")
                            {
                                invNo.Visible = true;
                                invPath.Visible = false;
                            }
                            if (_dt.Rows[i]["InvoiceNo"].ToString() == "" && _dt.Rows[i]["InvoicePath"].ToString() == "")
                            {
                                invNo.Visible = false;
                                invPath.Visible = false;
                            }
                            if (_dt.Rows[i]["InvoiceNo"].ToString() != "" && _dt.Rows[i]["InvoicePath"].ToString() != "")
                            {
                                invNo.Visible = false;
                                invPath.Visible = true;
                            }

                            if ((_dt.Rows.Count - 1) == i)
                            {
                                Balance = Balance + double.Parse(_dt.Rows[i]["EmpBalance"].ToString());
                            }
                            DRT = DRT + double.Parse(_dt.Rows[i]["DRs"].ToString());
                            CRT = CRT + double.Parse(_dt.Rows[i]["CRs"].ToString());
                        }

                        CRTotal.Text = DRT.ToString("0.00");
                        DRTotal.Text = CRT.ToString("0.00");
                        BalanceTotal.Text = Balance.ToString("0.00");
            }
        }

        public override void Dispose()
        {
            _dt.Dispose();
        }
    }
}