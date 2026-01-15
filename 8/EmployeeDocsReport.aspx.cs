using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _8
{
    public partial class AdminStudentDocsReport : Page
    {
        private readonly Campus _oo;
        private string _sql="";
        public AdminStudentDocsReport()
        {
            _oo = new Campus();
        }
        protected void Page_Load(object sender, EventArgs e)
        {     
            if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
            {
                Response.Redirect("default.aspx");
            }
            Campus camp = new Campus(); camp.LoadLoader(loader);
            BLL.BLLInstance.LoadHeader("Report", header1);
            if (!IsPostBack)
            {
                OfficialDetailDropDown();
                LoadData();
                //for (int j = 5; j < GridView1.Rows.Count; j++)
                //{
                //    string value = GridView1.Columns.Rows.Cells[j].Text;
                //    if (value == "0")
                //    {
                //        e.Row.Cells[j].Text = "<i class='text-false-color txt-bold' style='font-size:15px'>X</i>";
                //    }
                //    else
                //    {
                //        e.Row.Cells[j].Text = "<i class='text-true-color txt-bold' style='font-size:15px'>✔</i>";
                //    }
                //}
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            string msg;
            if (!Page.IsValid)
            {
                // ReSharper disable once RedundantJumpStatement
                return;
            }
            // ReSharper disable once RedundantIfElseBlock
            else
            {
                if (CheckDocumentisCreated() > 0)
                {
                    LoadData();
                }
                else
                {
                    msg = "Please, create document name for this employee!";
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, msg, "A");
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                }
            }
        }

        private int CheckDocumentisCreated()
        {
            _sql = "Select Count(*) as count from dt_CreateStaffDocumentName where BranchCode="+Session["BranchCode"] +"";
            return Convert.ToInt16(BAL.objBal.ReturnTag(_sql, "count"));
        }
        public void OfficialDetailDropDown()
        {
            _sql = "Select EmpDepName from EmpDepMaster where  BranchCode=" + Session["BranchCode"] + "";
            _oo.FillDropDown(_sql, txtDepartmentName, "EmpDepName");
            txtDepartmentName.Items.Insert(0,new ListItem("<--Select-->", ""));
            _sql = "Select EmpDesName from EmpDesMaster where  BranchCode=" + Session["BranchCode"] + "";
            _oo.FillDropDown(_sql, drpdes, "EmpDesName");
           drpdes.Items.Insert(0, new ListItem("<--Select-->",""));
        }
        protected void LoadData()
        {
            // ReSharper disable once UseObjectOrCollectionInitializer
            var param = new List<SqlParameter>();
            param.Add(new SqlParameter("@sessionName", Session["SessionName"].ToString()));
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
            if (drpdes.SelectedIndex!=0)
            {
                param.Add(new SqlParameter("@Designation", drpdes.SelectedValue.Trim()));
            }
            if (txtDepartmentName.SelectedIndex != 0)
            {
                param.Add(new SqlParameter("@DepartmentName", txtDepartmentName.SelectedValue.Trim()));
            }
           
            if (drpDocType.SelectedIndex != 0)
            {
                if (drpDocType.SelectedIndex == 1)
                {
                    param.Add(new SqlParameter("@Hardcopy", true));
                    param.Add(new SqlParameter("@Softcopy", false));
                }
                else
                {
                    param.Add(new SqlParameter("@Hardcopy", false));
                    param.Add(new SqlParameter("@Softcopy", true));
                }
            }
            else
            {
                param.Add(new SqlParameter("@Softcopy", true));
                param.Add(new SqlParameter("@Hardcopy", true));
            }
            GridView1.DataSource = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("Get_EmployeeDocsRecord", param);
            GridView1.DataBind();
            divExport.Visible = GridView1.Rows.Count > 0;
            Panel2.Visible = GridView1.Rows.Count > 0;
        }
        
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void ImageButton1_Click(object sender, EventArgs e)
        {
            BAL.objBal.ExportTolandscapeWord(Response, "StudentDocsRecords", divExport);
        }

        protected void ImageButton2_Click(object sender, EventArgs e)
        {
            BAL.objBal.ExportDivToExcel(Response, "StudentDocsRecords.xls", divExport);
        }

        protected void ImageButton3_Click(object sender, EventArgs e)
        {
            BAL.objBal.ExporttolandscapePdf(Response, "StudentDocsRecords", divExport);
        }

        protected void ImageButton4_Click(object sender, EventArgs e)
        {
            PrintHelper_New.ctrl = abc;
            ScriptManager.RegisterClientScriptBlock(Page,GetType(), "onclick", "var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}",true);
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                for (var j = 3; j < e.Row.Cells.Count; j++)
                {
                    var value = e.Row.Cells[j].Text;
                    e.Row.Cells[j].Text = value == "0" ? "<i class='text-false-color txt-bold' style='font-size:15px'>X</i>" : "<i class='text-true-color txt-bold' style='font-size:15px'>✔</i>";
                }
            }
        }

        public override void Dispose()
        {
            _oo.Dispose();
        }

    }
}