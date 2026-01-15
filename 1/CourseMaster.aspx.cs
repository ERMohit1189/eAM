using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace _1
{
    public partial class AdminCourseMaster : System.Web.UI.Page
    {
        private readonly Campus _oo;
        private readonly BAL.GenralInfo _objBal = new BAL.GenralInfo();
        private string _sql = String.Empty;

        public AdminCourseMaster()
        {
            _oo = new Campus();
        }
        protected void Page_PreInIt(object sender, EventArgs e)
        {
            if (Session["Logintype"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            if (Session["Logintype"].ToString() == "SuperAdmin")
            {
                MasterPageFile = "~/50/sadminRootManager.master";
            }
        }
        #region
        protected void Page_Load(object sender, EventArgs e)
        {
            Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file

            if (!IsPostBack)
            {
                string sql = "Select BranchId, BranchName from Branchtab";
                var dt = _oo.Fetchdata(sql);
                ddlBranch.DataSource = dt;
                ddlBranch.DataTextField = "BranchName";
                ddlBranch.DataValueField = "BranchId";
                ddlBranch.DataBind();
                ddlBranch.Items.Insert(0, new ListItem("<--Select-->", ""));
                if (Session["LoginType"].ToString() == "Admin")
                {
                    divBranch.Visible = false;
                    //divSession.Visible = false;
                    ddlBranch.SelectedValue = Session["BranchCode"].ToString();

                    //    string sqls = "select SessionName from SessionMaster where BranchCode=" + ddlBranch.SelectedValue + "";
                    //    var dt2 = _oo.Fetchdata(sqls);
                    //    DrpSessionName.DataSource = dt2;
                    //    DrpSessionName.DataTextField = "SessionName";
                    //    DrpSessionName.DataValueField = "SessionName";
                    //    DrpSessionName.DataBind();
                    //    DrpSessionName.Items.Insert(0, new ListItem("<--Select Session-->", ""));
                    //    DrpSessionName.SelectedIndex = (DrpSessionName.Items.Count - 1);
                    //    lblSession.Text = DrpSessionName.SelectedValue;
                    //    if (Session["LoginType"].ToString() == "Admin")
                    //    {
                    //        DrpSessionName.SelectedValue = Session["SessionName"].ToString();
                    //        lblSession.Text = Session["SessionName"].ToString();
                    //    }
                    //}

                    //DrpSessionName.Items.Insert(0, new ListItem("<--Select-->", ""));
                    //string[] str = DrpSessionName.SelectedValue.ToString().Split('-');
                    //  HiddenField1.Value = str[0];
                    LoadData();
                }
            }
        }
        protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddlBranch.SelectedIndex == 0)
            //{
            //    DrpSessionName.Items.Clear();
            //    DrpSessionName.Items.Insert(0, new ListItem("<--Select Session-->", ""));
            //    return;
            //}
            //string sql = "select SessionName from SessionMaster where BranchCode=" + ddlBranch.SelectedValue + "";
            //var dt2 = _oo.Fetchdata(sql);
            //DrpSessionName.DataSource = dt2;
            //DrpSessionName.DataTextField = "SessionName";
            //DrpSessionName.DataValueField = "SessionName";
            //DrpSessionName.DataBind();
            //DrpSessionName.Items.Insert(0, new ListItem("<--Select Session-->", ""));
            //DrpSessionName.SelectedIndex = (DrpSessionName.Items.Count - 1);
            //lblSession.Text = DrpSessionName.SelectedValue;
            //if (Session["LoginType"].ToString() == "Admin")
            //{
            //    DrpSessionName.SelectedValue = Session["SessionName"].ToString();
            //    lblSession.Text = Session["SessionName"].ToString();
            //}
            LoadData();

        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            var param = new List<SqlParameter>();
            param.Add(new SqlParameter("@CourseName", txtCourse.Text.Trim().Replace("  ", " ")));
            param.Add(new SqlParameter("@CourseType", "1"));// drpCourseType.SelectedValue
            param.Add(new SqlParameter("@Duration", "1"));
           // param.Add(new SqlParameter("@Batch", DrpSessionName.SelectedValue.ToString()));
            param.Add(new SqlParameter("@CourseCode", txtCourseCode.Text.Trim()));
            param.Add(new SqlParameter("@DisplayOrder", txtDisplayOrder.Text.Trim()));
            param.Add(new SqlParameter("@CourseCategory", "1")); //drpCourseCategory.SelectedValue
            param.Add(new SqlParameter("@Remark", txtRemark.Text.Trim()));
           // param.Add(new SqlParameter("@SessionName", DrpSessionName.SelectedValue.ToString()));
            param.Add(new SqlParameter("@BranchCode",  ddlBranch.SelectedValue.ToString()));
            param.Add(new SqlParameter("@LoginName", Session["LoginName"].ToString()));
            SqlParameter para = new SqlParameter("@Msg", "")
            {
                Direction = ParameterDirection.Output,
                Size = 0x100
            };
            param.Add(para);

            // ReSharper disable once UnusedVariable
            string msg = new DLL().Sp_Insert_Update_Delete_usingExecuteNonQuery("CourseMasterProc", param);

            _objBal.Noofnoncleartxt = 0;
            //oo.MessageBoxforUpdatePanel(ObjBal.MessageType(msg, new Control(), ObjBal), LinkButton1);
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Submitted successfully.", "S");
            _oo.ClearControls(table1);
            LoadData();
        }
        protected void LoadData()
        {
            _sql = "Select cm.id, cm.CourseName,dbo.[CourseTypeFun](cm.CourseType) as CourseType,cm.Duration,cm.Batch,cm.CourseCode,dbo.[CourseCategoryFun](CourseCategory) as CourseCategory,cm.DisplayOrder";
            _sql +=  " from CourseMaster cm where  BranchCode=" +  ddlBranch.SelectedValue + "";

            GridView1.DataSource = _oo.GridFill(_sql);
            GridView1.DataBind();
            txtDisplayOrder.Text = (GridView1.Rows.Count + 1).ToString();
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                Label id = (Label)GridView1.Rows[i].FindControl("id");
                LinkButton lnkEdit = (LinkButton)GridView1.Rows[i].FindControl("lnkEdit");
                LinkButton lnkConfirmDelete = (LinkButton)GridView1.Rows[i].FindControl("lnkConfirmDelete");
                _sql = "select * from ClassMaster where course='" + id.Text + "'  and BranchCode=" +  ddlBranch.SelectedValue + "";
                if (_oo.Duplicate(_sql))
                {
                    lnkEdit.Text = "<i class='fa fa-lock'></i>";
                    lnkEdit.Enabled = false;
                    lnkConfirmDelete.Text = "<i class='fa fa-lock'></i>";
                    lnkConfirmDelete.Enabled = false;
                }
            }
        }
        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            LinkButton lnk=(LinkButton)sender;
            GridViewRow currentrow = (GridViewRow)lnk.NamingContainer;

            string courseName = GridView1.Rows[currentrow.RowIndex].Cells[1].Text.Trim();

            _sql = "Select cm.CourseName,cm.CourseType,cm.Duration,cm.Batch,cm.CourseCode,cm.CourseCategory,cm.DisplayOrder,cm.Remark";
            _sql +=  " from CourseMaster cm where CourseName='" + courseName + "'  and BranchCode=" +  ddlBranch.SelectedValue + "";

            txtPanelCourse.Text = _oo.ReturnTag(_sql, "CourseName");
            drpPanelCourseType.SelectedValue = _oo.ReturnTag(_sql, "CourseType");
            //txtPanelDuration.Text = _oo.ReturnTag(_sql, "Duration");
           // txtPanelBatch.Text = _oo.ReturnTag(_sql, "Batch");
            txtPanelCourseCode.Text = _oo.ReturnTag(_sql, "CourseCode");
            drpPanelCourseCategory.SelectedValue = _oo.ReturnTag(_sql, "CourseCategory");
            txtPanelDisplayOrder.Text = _oo.ReturnTag(_sql, "DisplayOrder");
            txtPanelRemark.Text = _oo.ReturnTag(_sql, "Remark");

            Panel1_ModalPopupExtender.Show();
        }
        protected void lnkUpdate_Click(object sender, EventArgs e)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@CourseName", txtPanelCourse.Text.Trim()));
            param.Add(new SqlParameter("@CourseType", "1"));//drpPanelCourseType.SelectedValue
            param.Add(new SqlParameter("@Duration", "1"));
          //  param.Add(new SqlParameter("@Batch", DrpSessionName.SelectedValue.ToString()));
            param.Add(new SqlParameter("@CourseCode", txtPanelCourseCode.Text.Trim()));
            param.Add(new SqlParameter("@DisplayOrder", txtPanelDisplayOrder.Text.Trim()));
            param.Add(new SqlParameter("@CourseCategory", "1"));//drpPanelCourseCategory.SelectedValue
            param.Add(new SqlParameter("@Remark", txtPanelRemark.Text.Trim()));
         //   param.Add(new SqlParameter("@SessionName", DrpSessionName.SelectedValue.ToString()));
            param.Add(new SqlParameter("@BranchCode",  ddlBranch.SelectedValue.ToString()));
            param.Add(new SqlParameter("@LoginName", Session["LoginName"].ToString()));
            SqlParameter para = new SqlParameter("@Msg", "");
            para.Direction = ParameterDirection.Output;
            para.Size = 0x100;
            param.Add(para);

            // ReSharper disable once UnusedVariable
            string msg = new DLL().Sp_Insert_Update_Delete_usingExecuteNonQuery("CourseMasterProc", param);

            _objBal.Noofnoncleartxt = 0;
            //oo.MessageBoxforUpdatePanel(ObjBal.MessageType(msg, new Control(), ObjBal), LinkButton1);
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Updated successfully.", "S"); 
            LoadData();
        }
        protected void lnkConfirmDelete_Click(object sender, EventArgs e)
        {
            lnkDeleteNo.Focus();
            LinkButton lnk = (LinkButton)sender;
            GridViewRow currentrow = (GridViewRow)lnk.NamingContainer;

            lblvalue.Text = GridView1.Rows[currentrow.RowIndex].Cells[1].Text.Trim();

            Panel2_ModalPopupExtender.Show();
        }
        protected void lnkDeleteYes_Click(object sender, EventArgs e)
        {
            _sql = "Delete from CourseMaster where CourseName='" + lblvalue.Text + "'  and BranchCode=" +  ddlBranch.SelectedValue + "";
            _oo.ProcedureDatabase(_sql);
            _objBal.Noofnoncleartxt = 0;
            //oo.MessageBoxforUpdatePanel(ObjBal.MessageType("De", new Control(), ObjBal), LinkButton1);
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Deleted successfully.", "S"); 
            LoadData();
        }

        public override void Dispose()
        {
            _oo.Dispose();
        }
        #endregion

        
    }
}