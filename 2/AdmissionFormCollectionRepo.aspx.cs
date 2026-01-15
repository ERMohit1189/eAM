using System;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _2
{
    public partial class AdminAdmissionFormCollectionRepo : Page
    {
        private SqlConnection _con;
        private readonly SqlConnection _con1;
        private readonly Campus _oo;
        private string _sql = string.Empty;
#pragma warning disable 414
        // ReSharper disable once RedundantDefaultMemberInitializer
        private int _mo = 0;
#pragma warning restore 414
        public AdminAdmissionFormCollectionRepo()
        {
            _con = new SqlConnection();
            _con1 = new SqlConnection();
            _oo = new Campus();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
            {
                Response.Redirect("default.aspx");
            }
            _con = _oo.dbGet_connection();

            header1.Controls.Clear();
            BLL.BLLInstance.LoadHeader("Report", header1);
            if (!IsPostBack)
            {
                _oo.AddDateMonthYearDropDown(DDYear, DDMonth, DDDate);
                _oo.FindCurrentDateandSetinDropDown(DDYear, DDMonth, DDDate);

                _oo.AddDateMonthYearDropDown(DDYearTo, DDMonthTo, DDDateTo);
                _oo.FindCurrentDateandSetinDropDown(DDYearTo, DDMonthTo, DDDateTo);

                Panel1.Visible = false;

                abc.Visible = false;

                string sql = "Select BranchId, BranchName from Branchtab";
                var dt = _oo.Fetchdata(sql);
                ddlBranch.DataSource = dt;
                ddlBranch.DataTextField = "BranchName";
                ddlBranch.DataValueField = "BranchId";
                ddlBranch.DataBind();
                ddlBranch.Items.Insert(0, new ListItem("<--Select-->", ""));

                ddlBranch.SelectedValue = Session["BranchCode"].ToString();

                string sqls = "select SessionName from SessionMaster where BranchCode=" + ddlBranch.SelectedValue + "";
                var dt2 = _oo.Fetchdata(sqls);
                DrpSessionName.DataSource = dt2;
                DrpSessionName.DataTextField = "SessionName";
                DrpSessionName.DataValueField = "SessionName";
                DrpSessionName.DataBind();
                DrpSessionName.Items.Insert(0, new ListItem("<--All-->", ""));
                if (Session["LoginType"].ToString() == "Admin")
                {
                    divBranch.Visible = false;
                    //divSession.Visible = true;
                }
                else
                {
                    divBranch.Visible = true;
                    //divSession.Visible = true;
                }

                loadUser();

                if (Request.QueryString.AllKeys.Length > 0 && Request.QueryString["Type"] != null)
                {
                    if (Request.QueryString["Type"].ToString() == "1")
                    {
                        DrpSessionName.SelectedValue = Session["SessionName"].ToString();
                        drpStatus.SelectedValue = "Paid";
                        loadData();
                    }
                }

                GetClassFromAdmissionFeeForm();
            }
        }

        private void GetClassFromAdmissionFeeForm()
        {
            _sql = "Select Class from (Select 'All' Class,0 CIDOrder Union Select Distinct Class,cm.CIDOrder from AdmissionFormCollection afc left join ClassMaster cm on cm.classname=afc.Class)T1 Order by CIDOrder";
            _oo.FillDropDownWithOutSelect(_sql, drpClass, "Class");
        }

        protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadUser();
            if (ddlBranch.SelectedIndex == 0)
            {
                DrpSessionName.Items.Clear();
                DrpSessionName.Items.Insert(0, new ListItem("<--Select Session-->", ""));
                return;
            }
            string sql = "select SessionName from SessionMaster where BranchCode=" + ddlBranch.SelectedValue + "";
            var dt2 = _oo.Fetchdata(sql);
            DrpSessionName.DataSource = dt2;
            DrpSessionName.DataTextField = "SessionName";
            DrpSessionName.DataValueField = "SessionName";
            DrpSessionName.DataBind();
            DrpSessionName.Items.Insert(0, new ListItem("<--Select Session-->", ""));
            DrpSessionName.SelectedIndex = (DrpSessionName.Items.Count - 1);
            if (Session["LoginType"].ToString() == "Admin")
            {
                DrpSessionName.SelectedValue = Session["SessionName"].ToString();
            }

        }
        protected void loadUser()
        {
            string sql = "";
            if (Session["LoginType"].ToString() == "Admin")
            {
                sql = "Select UserId From NewAdminInformation where BranchCode=" + Session["BranchCode"] + "";
            }
            else
            {
                sql = "Select UserId From NewAdminInformation where BranchCode=" + ddlBranch.SelectedValue + "";
            }
            _oo.FillDropDownWithOutSelect(sql, DropDownList1, "UserId");
            DropDownList1.Items.Insert(0, new ListItem("All", ""));
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            loadData();
        }
        protected void loadData()
        {
            switch (DDMonth.SelectedItem.ToString())
            {
                case "Jan":
                    _mo = 1;
                    break;
                case "Feb":
                    _mo = 2;
                    break;
                case "Mar":
                    _mo = 3;
                    break;
                case "Apr":
                    _mo = 4;
                    break;
                case "May":
                    _mo = 5;
                    break;
                case "Jun":
                    _mo = 6;
                    break;
                case "Jul":
                    _mo = 7;
                    break;
                case "Aug":
                    _mo = 8;
                    break;
                case "Sep":
                    _mo = 9;
                    break;
                case "Oct":
                    _mo = 10;
                    break;
                case "Nov":
                    _mo = 11;
                    break;
                case "Dec":
                    _mo = 12;
                    break;
            }


            var fromDate = DDYear.SelectedItem + " " + DDMonth.SelectedItem + " " + DDDate.SelectedItem;
            var toDate = DDYearTo.SelectedItem + " " + DDMonthTo.SelectedItem + " " + DDDateTo.SelectedItem;

            lbltitel.Text = "Admission Form Fee Report from " + DateTime.Parse(fromDate).ToString("dd MMM yyyy") + " to " + DateTime.Parse(toDate).ToString("dd MMM yyyy");
            if (drpClass.SelectedValue == "All")
            {
                _sql = " select Row_Number() over (order by Id Asc) as SNo,id,RecieptNo,convert(nvarchar,AdmissionFromDate,106) as AdmissionFromDate,StudentName+' '+MiddleName+' '+LastName as StudentName,FatherName,Class, mop, status,Branch,LoginName,BranchCode,convert(nvarchar,RecordDate,106) as RecordDate,Sex,FatherContactNo,ReceivedAmount,case when cancel IS NULL then 'No' else 'Yes' end as Cancel, AdmissionType,Prospectus  from AdmissionFormCollection    ";
                _sql += " where AdmissionFromDate between '" + fromDate + "' and '" + toDate + "'";
            }
            else
            {
                _sql = " select Row_Number() over (order by Id Asc) as SNo,id,RecieptNo,convert(nvarchar,AdmissionFromDate,106) as AdmissionFromDate,StudentName+' '+MiddleName+' '+LastName as StudentName,FatherName,Class, mop, status,Branch,LoginName,BranchCode,convert(nvarchar,RecordDate,106) as RecordDate,Sex,FatherContactNo,ReceivedAmount,case when cancel IS NULL then 'No' else 'Yes' end as Cancel, AdmissionType,Prospectus  from AdmissionFormCollection    ";
                _sql += " where AdmissionFromDate between '" + fromDate + "' and '" + toDate + "' and class='" + drpClass.SelectedValue + "' ";
            }
            string sts = "";
            if (ddlBranch.SelectedIndex != 0)
            {
                _sql += " and BranchCode='" + ddlBranch.SelectedValue + "' ";
            }
            if (DrpSessionName.SelectedIndex != 0)
            {
                _sql += " and SessionName='" + DrpSessionName.SelectedValue + "' ";
            }
            if (DdlpaymentMode.SelectedIndex != 0)
            {
                sts = sts + "Mode : " + DdlpaymentMode.SelectedValue;
                _sql += " and mop='" + DdlpaymentMode.SelectedValue + "' ";
            }
            if (drpStatus.SelectedIndex != 0)
            {
                _sql += " and Status='" + drpStatus.SelectedValue + "' ";
                if (DdlpaymentMode.SelectedIndex != 0)
                {
                    sts = sts + " | Status : " + drpStatus.SelectedValue;
                }
                else
                {
                    sts = sts + "Status : " + drpStatus.SelectedValue;
                }
            }
            if (DropDownList1.SelectedIndex != 0)
            {
                _sql += " and LoginName='" + DropDownList1.SelectedValue + "' ";
                sts = sts + " by " + DropDownList1.SelectedValue;
            }
            if (drpAdmissionType.SelectedIndex != 0)
            {
                _sql += " and AdmissionType='" + drpAdmissionType.SelectedItem.Text + "' ";
                if (DdlpaymentMode.SelectedIndex != 0 || drpStatus.SelectedIndex != 0)
                {
                    sts = sts + " | Type of Admission : " + drpAdmissionType.SelectedValue;
                }
                else
                {
                    sts = sts + "Type of Admission : " + drpAdmissionType.SelectedValue;
                }
            }
            _sql += " order by id Asc";
            lbloptions.Text = sts;
            Grd.DataSource = _oo.GridFill(_sql);
            Grd.DataBind();
            int i;
            double sum = 0;

            for (i = 0; i <= Grd.Rows.Count - 1; i++)
            {
                Label lblAmt = (Label)Grd.Rows[i].FindControl("Label33");
                Label Status = (Label)Grd.Rows[i].FindControl("Status");
                if (Status.Text.ToLower().Trim() != "cancelled")
                {
                    sum = sum + Convert.ToDouble(lblAmt.Text);
                }
            }
            if (Grd.Rows.Count > 0)
            {
                Panel1.Visible = true;
                Label lblAmountTotal = (Label)Grd.FooterRow.FindControl("lblAmountTotal");
                lblAmountTotal.Text = sum.ToString(CultureInfo.InvariantCulture);
                abc.Visible = true;
            }
            else
            {
                abc.Visible = false;
                //oo.MessageBox("Sorry, No Record(s) found!", this.Page);
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Sorry, No Record(s) found!", "A");

            }


        }
        protected void DDYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            _oo.YearDropDown(DDYear, DDMonth, DDDate);
        }
        protected void DDMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            _oo.MonthDropDown(DDYear, DDMonth, DDDate);
        }
        protected void DDDate_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void ImageButton1_Click(object sender, EventArgs e)
        {
            _oo.ExportTolandscapeWord(Response, "AdmissionFormFeeReport", gdv1);
        }
        protected void ImageButton2_Click(object sender, EventArgs e)
        {
            _oo.ExportDivToExcelWithFormatting(Response, "AdmissionFormFeeReport.xls", gdv1, Server.MapPath("~/Admin/css/style.css"));
        }
        protected void ImageButton3_Click(object sender, EventArgs e)
        {
            _oo.ExporttolandscapePdf(Response, "AdmissionFormFeeReport", abc);
        }
        protected void ImageButton4_Click(object sender, EventArgs e)
        {
            PrintHelper_New.ctrl = abc;
            ClientScript.RegisterStartupScript(GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
        }

        public override void Dispose()
        {
            _con.Dispose();
            _con1.Dispose();
            _con1.Dispose();
        }
    }
}