using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;


namespace _1
{
    public partial class AdminStudentDocsReportold : Page
    {
        private string _sql=string.Empty;
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
                LoadClass();
                LoadSection();
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
                    msg = "Please, create document name for this class!";
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, msg, "A");
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                }
            }
        }

        private int CheckDocumentisCreated()
        {
            _sql = "Select Count(*) as count from dt_CreateDocumentName";
            return Convert.ToInt16(BAL.objBal.ReturnTag(_sql, "count"));
        }

        protected void LoadData()
        {
            string s = Session["SessionName"].ToString();
            string b = Session["BranchCode"].ToString();
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@sessionName", Session["SessionName"].ToString()));
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
            
            if (drpClass.SelectedIndex != 0)
            {
                param.Add(new SqlParameter("@ClassName", drpClass.SelectedItem.Text.Trim()));
                param.Add(new SqlParameter("@ClassId", drpClass.SelectedValue));
            }
            else
            {
                param.Add(new SqlParameter("@ClassName", ""));
                param.Add(new SqlParameter("@ClassId", ""));
            }
            if (drpSection.SelectedIndex != 0)
            {
                param.Add(new SqlParameter("@SectionName", drpSection.SelectedItem.Text));
            }
            else
            {
                param.Add(new SqlParameter("@SectionName", ""));
            }
            if (drpAdmissioncType.SelectedIndex != 0)
            {
                param.Add(new SqlParameter("@TypeOFAdmision", drpAdmissioncType.SelectedItem.Text));
            }
            else
            {
                param.Add(new SqlParameter("@TypeOFAdmision", ""));
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
            GridView1.DataSource = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("Get_StudentDocsRecordOLd", param);
            GridView1.DataBind();

            if (GridView1.Rows.Count > 0)
            {
                divExport.Visible = true;
            }
            else
            {
                divExport.Visible = false;
            }
        }

        protected void LoadClass()
        {
            BLL.BLLInstance.loadClass(drpClass, Session["SessionName"].ToString());
        }

        protected void LoadSection()
        {
            BLL.BLLInstance.loadSection(drpSection, Session["SessionName"].ToString(),drpClass.SelectedValue);
        }

        protected void drpClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            //LoadSection();
            //GridView1.DataSource = null;
            //GridView1.DataBind();
            LoadData();
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
                for (int j = 5; j < e.Row.Cells.Count; j++)
                {
                    string value = e.Row.Cells[j].Text;
                    if (value == "0")
                    {
                        e.Row.Cells[j].Text = "<i class='text-false-color txt-bold' style='font-size:15px'>X</i>";
                    }
                    else
                    {
                        e.Row.Cells[j].Text = "<i class='text-true-color txt-bold' style='font-size:15px'>✔</i>";
                    }
                }
            }
        }

   

       
    }
}