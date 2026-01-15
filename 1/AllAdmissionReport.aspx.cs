using System;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _1
{
    public partial class AdminAllAdmissionReport : Page
    {
        private SqlConnection _con;
        private readonly Campus _oo;
        private string _sql = String.Empty;
        public AdminAllAdmissionReport()
        {
            _con = new SqlConnection();
            _oo = new Campus();
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //GridView1.FooterRow.Visible = false;
            }
            catch (Exception)
            {
            }
            
            if ((string) Session["LoginName"] == "" || (string)Session["BranchCode"] == "" || (string)Session["SessionName"] == "" || Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
            {
                Response.Redirect("default.aspx");
            }

            _con = _oo.dbGet_connection();
            Campus camp = new Campus(); camp.LoadLoader(loader);
            BLL.BLLInstance.LoadHeader("Report", header);
            if (!IsPostBack)
            {
                LoadClass();
                _oo.AddDateMonthYearDropDown(FromYY, FromMM, FromDD);
                _oo.AddDateMonthYearDropDown(ToYY, ToMM, ToDD);

                _oo.FindCurrentDateandSetinDropDown(FromYY, FromMM, FromDD);
                _oo.FindCurrentDateandSetinDropDown(ToYY, ToMM, ToDD);
                if (Request.QueryString.AllKeys.Length > 0 && Request.QueryString["Type"] != null)
                {
                    if (Request.QueryString["Type"].ToString() == "1")
                    {
                        loadData();
                    }
                }
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {

        }
        protected void FromYY_SelectedIndexChanged(object sender, EventArgs e)
        {
            _oo.YearDropDown(FromYY, FromMM, FromDD);
        }
        protected void FromMM_SelectedIndexChanged(object sender, EventArgs e)
        {
            _oo.MonthDropDown(FromYY, FromMM, FromDD);
        }
        protected void FromDD_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void ToYY_SelectedIndexChanged(object sender, EventArgs e)
        {
            _oo.YearDropDown(ToYY, ToMM, ToDD);
        }
        protected void ToMM_SelectedIndexChanged(object sender, EventArgs e)
        {
            _oo.MonthDropDown(ToYY, ToMM, ToDD);
        }
        protected void ToDD_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            loadData();
        }

        protected void loadData()
        {
            var todate = ToYY.SelectedItem + "/" + ToMM.SelectedItem + "/" + ToDD.SelectedItem;
            var fromdate = FromYY.SelectedItem + "/" + FromMM.SelectedItem + "/" + FromDD.SelectedItem;

            _sql = "select ad.Id AS SrNo,ad.Id,Cm.CountryName,CS.CityName,Sm.StateName, FatherName, EnquiredPerson"
                + ", convert(nvarchar,Ad.Date,106) as Date ,Ad.AdmissionClass ,Ad.Name ,Ad.ContactNo ,Ad.MobileNo ,Ad.EMail " +
                ",(Ad.Address+' '+cs.cityName+', '+sm.stateName) Address, Ad.Status,Ad.EnquiryNo  from AdmissionEnquiry AD";
            _sql += " left join CountryMaster CM on AD.CountryId=CM.Id";
            _sql += " left join StateMaster SM on AD.StateId=SM.Id";
            _sql += " left join CityMaster CS on AD.CityId=CS.Id ";
            _sql += " and ad.SessionName='" + Session["SessionName"] + "' and ad.BranchCode=" + Session["BranchCode"] + "";
            _sql += " where Ad.AdmissionClass=" +
                    (drpClassForView.SelectedItem == null || drpClassForView.SelectedItem.Text == "<--All-->" ? "Ad.AdmissionClass" : ("'"
                    + drpClassForView.SelectedItem.Text + "'")) + " AND Ad.Date between '" + fromdate + "'   and   '" + todate + "'  ";
            _sql += " ORDER BY CONVERT(int, (SUBSTRING(Ad.EnquiryNo, 14, len(Ad.EnquiryNo)))) ";//order by ad.Id desc";

            GridView1.DataSource = _oo.GridFill(_sql);
            GridView1.DataBind();
            divExport.Visible = true;
            gdv1.Visible = true;

            string ss = "select format(getdate(), 'dd-MMM-yyyy hh:mm:ss tt') date";
            lblRegister.Text = "(Date : " + _oo.ReturnTag(ss, "date") + ")";
            if (GridView1.Rows.Count == 0)
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Sorry, No Record(s) found!", "A");
                divExport.Visible = false;
                gdv1.Visible = false;
            }
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            var todate = ToYY.SelectedItem + "/" + ToMM.SelectedItem + "/" + ToDD.SelectedItem;
            var fromdate = FromYY.SelectedItem + "/" + FromMM.SelectedItem + "/" + FromDD.SelectedItem;

            GridView1.PageIndex = e.NewPageIndex;
            _sql = "select ROW_NUMBER() OVER (ORDER BY ad.Id desc) AS SrNo,ad.Id,Cm.CountryName,CS.CityName,Sm.StateName, FatherName, EnquiredPerson, convert(nvarchar,Ad.Date,106) as Date ,Ad.AdmissionClass ,Ad.Name ,Ad.ContactNo ,Ad.MobileNo ,Ad.EMail ,(Ad.Address+' '+cs.cityName+', '+sm.stateName) Address, Ad.Status,Ad.EnquiryNo  from AdmissionEnquiry AD";
            _sql = _sql + "   left join CountryMaster CM on AD.CountryId=CM.Id";
            _sql = _sql + " left join StateMaster SM on AD.StateId=SM.Id";
            _sql = _sql + " left join CityMaster CS on AD.CityId=CS.Id ";
            _sql = _sql + " and ad.SessionName='" + Session["SessionName"] + "' and ad.BranchCode=" + Session["BranchCode"] + "";
            _sql = _sql + "  where Ad.Date between '" + fromdate + "'   and   '" + todate + "'  ";
            _sql = _sql + "    order by ad.Id desc";
            GridView1.DataSource = _oo.GridFill(_sql);
            GridView1.DataBind();
            divExport.Visible = true;
            gdv.Visible = true;
        }

        protected void ImageButton1_Click(object sender, EventArgs e)
        {
            _oo.ExportTolandscapeWord(Response, "AdmissionEnquiryReport", gdv);
        }
        protected void ImageButton2_Click(object sender, EventArgs e)
        {
            _oo.ExportDivToExcelWithFormatting(Response, "AdmissionEnquiryReport.xls", gdv, Server.MapPath("~/Admin/css/style.css"));
        }
        protected void ImageButton3_Click(object sender, EventArgs e)
        {
            _oo.ExporttolandscapePdf(Response, "AdmissionEnquiryReport", gdv);
        }
        protected void ImageButton4_Click(object sender, EventArgs e)
        {
            PrintHelper_New.ctrl = abc;
            ClientScript.RegisterStartupScript(GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
        }

        private void LoadClass()
        {
            _sql = "Select Id,ClassName from ClassMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " order by Cidorder";
            _oo.FillDropDown_withValue(_sql, drpClassForView, "ClassName", "Id");
            drpClassForView.Items.Insert(0, new ListItem("<--All-->", "0"));
        }

        public override void Dispose()
        {
            _con.Dispose();
        }
    }
}