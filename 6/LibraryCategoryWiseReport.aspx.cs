using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _6
{
    public partial class LibraryCategoryWiseReport : Page
    {
        SqlConnection _con;
        readonly Campus _oo;
        string _sql = "";
        public LibraryCategoryWiseReport()
        {
            _con = new SqlConnection();
            _oo = new Campus();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
            {
                Response.Redirect("default.aspx");
            }
            _con = _oo.dbGet_connection();
            Campus camp = new Campus(); camp.LoadLoader(loader);
            BLL.BLLInstance.LoadHeader("Report", header);
            if (!IsPostBack)
            {
                LoadCategory();
                
                LoadPublisher();
                Getsubjectname();

                GridView1.DataSource = null;
                GridView1.DataBind();
                GridView2.DataSource = null;
                GridView2.DataBind();
                GridView1.Visible = false;
                GridView2.Visible = false;
                divPrints.Visible = false;
                abc.Visible = false;
                LoadGrid2();
                divTool.Visible = false;
                drpsubCategory.Items.Insert(0, new ListItem("<--Select-->", ""));
            }
        }
        protected void rptType_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            GridView2.DataSource = null;
            GridView2.DataBind();
            GridView1.Visible = false;
            GridView2.Visible = false;
            divPrints.Visible = false;
            abc.Visible = false;
            if (rptType.SelectedIndex == 0)
            {
                LoadGrid2();
                divTool.Visible = false;
            }
            else
            {
                LoadPublisher();
                divTool.Visible = true;
            }
        }
        public void LoadCategory()
        {
            _sql = "Select CategoryName,id from ItemCategoryMaster where BranchCode=" + Session["BranchCode"] + "";
            _oo.FillDropDown_withValue(_sql, drpCategory, "CategoryName", "id");
            drpCategory.Items.Insert(0, new ListItem("<--Select-->", ""));
        }
        public void LoadSubCategory()
        {
            _sql = "Select SubCategoryName,id from ItemSubCategoryMaster where BranchCode=" + Session["BranchCode"] + " and CategoryName='"+ drpCategory.SelectedValue + "' order by id";
            _oo.FillDropDown_withValue(_sql, drpsubCategory, "SubCategoryName", "id");
            drpsubCategory.Items.Insert(0, new ListItem("<--Select-->", ""));
            
        }
        public void LoadPublisher()
        {
            _sql = "Select  PublisherName,id from PublisherInfoEntry where BranchCode=" + Session["BranchCode"] + " order by id";
            _oo.FillDropDown_withValue(_sql, DDLpUBL, "PublisherName", "id");
            DDLpUBL.Items.Insert(0, new ListItem("<--Select-->", ""));
        }
        private void Getsubjectname()
        {
            _sql = "Select  SubjectName,id from SubjectTopicLibraryMaster where BranchCode=" + Session["BranchCode"] + " ORDER by SubjectName";
            _oo.FillDropDown_withValue(_sql, ddlsubjecttopic, "SubjectName", "id");
            ddlsubjecttopic.Items.Insert(0, new ListItem("<--Select-->", ""));
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            LoadGrid();
        }
        public void LoadGrid()
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            GridView2.DataSource = null;
            GridView2.DataBind();
            GridView1.Visible = false;
            GridView2.Visible = false;
            var param = new List<SqlParameter>();
            param.Add(new SqlParameter("@Category", drpCategory.SelectedValue));
            param.Add(new SqlParameter("@SubCategory", drpsubCategory.SelectedValue));
            if (DDLpUBL.SelectedIndex != 0)
            {
                param.Add(new SqlParameter("@Publisher", DDLpUBL.SelectedValue));
            }
            if (ddlsubjecttopic.SelectedIndex != 0)
            {
                param.Add(new SqlParameter("@SubjectTopic", ddlsubjecttopic.SelectedValue));
            }
            if (txtKeyword1.Text.Trim() != "")
            {
                param.Add(new SqlParameter("@Keyword1", txtKeyword1.Text.Trim()));
            }
            if (txtAuthor1.Text.Trim() != "")
            {
                param.Add(new SqlParameter("@Author1", txtAuthor1.Text.Trim()));
            }
            if (ddlstatusbook.SelectedIndex != 0)
            {
                param.Add(new SqlParameter("@BookStatus", ddlstatusbook.SelectedValue));
            }
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
            param.Add(new SqlParameter("@Action", "discriptive"));
            var ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("GetLibraryItemProc", param);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                GridView1.Visible = true;
                GridView1.DataSource = ds;
                GridView1.DataBind();
                divPrints.Visible = true;
                abc.Visible = true;
                _sql = "select format(getdate(), 'dd-MMM-yyyy hh:mm:ss tt') curdate";
                lblheadername.Text = "List of Items | " + _oo.ReturnTag(_sql, "curdate") + "";
            }
            else
            {

                GridView1.DataSource = null;
                GridView1.DataBind();
                GridView1.Visible = false;
                divPrints.Visible = false;
                abc.Visible = false;
            }
        }
        public void LoadGrid2()
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            GridView2.DataSource = null;
            GridView2.DataBind();
            GridView1.Visible = false;
            GridView2.Visible = false;
            var param = new List<SqlParameter>();
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
            param.Add(new SqlParameter("@Action", "consolidated"));
            var ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("GetLibraryItemProc", param);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                GridView2.Visible = true;
                GridView2.DataSource = ds;
                GridView2.DataBind();
                divPrints.Visible = true;
                abc.Visible = true;
                _sql = "select format(getdate(), 'dd-MMM-yyyy hh:mm:ss tt') curdate";
                lblheadername.Text = "List of Items | " + _oo.ReturnTag(_sql, "curdate") + "";
                int total = 0; int counts = 0;
                for (int i = 0; i < GridView2.Rows.Count; i++)
                {
                    Label NoOfItem = (Label)GridView2.Rows[i].FindControl("NoOfItem");
                    int.TryParse(NoOfItem.Text.ToString(), out counts);
                    total = total + counts;
                }
                Label lblGtotal = (Label)GridView2.FooterRow.FindControl("lblGtotal");
                lblGtotal.Text = total.ToString("0.00");
            }
            else
            {

                GridView2.DataSource = null;
                GridView2.DataBind();
                GridView2.Visible = false;
                divPrints.Visible = false;
                abc.Visible = false;
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
        }
        protected void drpCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSubCategory();
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            LoadGrid();
        }
        protected void ImageButton4_Click(object sender, EventArgs e)
        {
            PrintHelper_New.ctrl = abc;
            ClientScript.RegisterStartupScript(GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
        }
        protected void ImageButton1_Click(object sender, EventArgs e)
        {
            BAL.objBal.ExportTolandscapeWord(Response, "ListofBooks", abc);
        }
        protected void ImageButton2_Click(object sender, EventArgs e)
        {
            BAL.objBal.ExportDivToExcelWithFormatting(Response, "ListofBooks", abc);
        }
        protected void ImageButton3_Click(object sender, EventArgs e)
        {
            BAL.objBal.ExporttolandscapePdf(Response, "ListofBooks", abc);
        }
        public override void Dispose()
        {
            _oo.Dispose();
            _con.Dispose();
            _oo.Dispose();
        }
    }
}