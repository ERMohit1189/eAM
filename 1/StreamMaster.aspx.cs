using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _1
{
    public partial class StreamMaster : Page
    {
        private readonly Campus _oo;
        private readonly BAL.GenralInfo _objBal = new BAL.GenralInfo();
        private string _sql = String.Empty;

        public StreamMaster()
        {
            _oo = new Campus();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string)Session["LoginName"] == "" || (string)Session["BranchCode"] == "" || (string)Session["SessionName"] == "" || Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
            {
                Response.Redirect("default.aspx");
            }
            Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file

            if (!IsPostBack)
            {
                drpCourse.Focus();
                LoadCourse();
                LoadClass();
                LoadData();
            }
        }

        protected void LoadCourse()
        {
            _sql = "Select CourseName,Id from CourseMaster where BranchCode="+ Session["BranchCode"] + "";
            _oo.FillDropDown_withValue(_sql, drpCourse, "CourseName", "Id");
            drpCourse.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
            _oo.FillDropDown_withValue(_sql, drpPanelCourse, "CourseName", "Id");
            drpPanelCourse.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
        }

    

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;
            if (drpClass.SelectedIndex == 1)
            {
                for (var i = 2; i < drpClass.Items.Count; i++)
                {
                    List<SqlParameter> param = new List<SqlParameter>();
                    param.Add(new SqlParameter("@Course", drpCourse.SelectedValue));
                    param.Add(new SqlParameter("@Classid", drpClass.Items[i].Value));
                    param.Add(new SqlParameter("@BranchName", txtBranchname.Text.Trim()));
                    param.Add(new SqlParameter("@BranchShortName", txtShortName.Text.Trim()));
                    param.Add(new SqlParameter("@BCode", txtBranchCode.Text.Trim()));
                    param.Add(new SqlParameter("@Remark", txtRemark.Text.Trim()));
                    param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
                    param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
                    param.Add(new SqlParameter("@LoginName", Session["LoginName"].ToString()));
                    param.Add(new SqlParameter("@IsDisplay", drpIsDisplay.SelectedValue));
                    param.Add(new SqlParameter("@QueryType", "Insert"));
                    SqlParameter para = new SqlParameter("@Msg", "");
                    para.Direction = ParameterDirection.Output;
                    para.Size = 0x100;
                    param.Add(para);

                    msg = new DLL().Sp_Insert_Update_Delete_usingExecuteNonQuery("BranchMasterProc", param);
                }
           
          
            }
            else
            {
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@Course", drpCourse.SelectedValue));
                param.Add(new SqlParameter("@Classid", drpClass.SelectedValue));
                param.Add(new SqlParameter("@BranchName", txtBranchname.Text.Trim()));
                param.Add(new SqlParameter("@BranchShortName", txtShortName.Text.Trim()));
                param.Add(new SqlParameter("@BCode", txtBranchCode.Text.Trim()));
                param.Add(new SqlParameter("@Remark", txtRemark.Text.Trim()));
                param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
                param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
                param.Add(new SqlParameter("@LoginName", Session["LoginName"].ToString()));
                param.Add(new SqlParameter("@IsDisplay", drpIsDisplay.SelectedValue));
                param.Add(new SqlParameter("@QueryType", "Insert"));
                SqlParameter para = new SqlParameter("@Msg", "");
                para.Direction = ParameterDirection.Output;
                para.Size = 0x100;
                param.Add(para);

                msg = new DLL().Sp_Insert_Update_Delete_usingExecuteNonQuery("BranchMasterProc", param);
          
            }

            Campus camp = new Campus();
            if (msg == "D")
            {
                camp.msgbox(Page, msgbox, "Duplicate Entry!", "A");
            }
            if (msg == "S")
            {
                camp.msgbox(Page, msgbox, "Submitted successfully.", "S");
            }
            LoadData();
        }

        protected void LoadData()
        {
            _sql = "Select Bm.Id,CourseName,ClassName,BranchName,BranchShortName,BCode,CASE WHEN IsDisplay=1 Then 'Yes' Else 'No' END IsDisplay from BranchMaster Bm";
            _sql +=  " inner join CourseMaster cm on cm.id=bm.Course  and Bm.BranchCode=cm.BranchCode  ";
            _sql += " inner join ClassMaster clm on clm.id=bm.Classid  and clm.BranchCode=cm.BranchCode and clm.SessionName=bm.SessionName ";
            _sql +=  " where bm.SessionName='" + Session["SessionName"] + "' and Bm.BranchCode=" + Session["BranchCode"] + "";
            _sql +=  " Order by CIDORDER";
            GridView1.DataSource = _oo.GridFill(_sql);
            GridView1.DataBind();

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                Label id = (Label)GridView1.Rows[i].FindControl("lblId");
                LinkButton lnkEdit = (LinkButton)GridView1.Rows[i].FindControl("lnkEdit");
                LinkButton lnkConfirmDelete = (LinkButton)GridView1.Rows[i].FindControl("lnkConfirmDelete");
                
                _sql = "select BranchId from AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") where BranchId='" + id.Text + "'";
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
            LinkButton lnk = (LinkButton)sender;
            GridViewRow currentrow = (GridViewRow)lnk.NamingContainer;

            Label lblId = (Label)currentrow.FindControl("lblId");

            lblvalue.Text = lblId.Text.Trim();

            _sql = "Select Course,Classid,BranchName,BranchShortName,BCode,Remark,IsDisplay from BranchMaster";
            _sql +=  " where Id='" + lblId.Text.Trim() + "' and SessionName='" + Session["SessionName"]+ "' and BranchCode=" + Session["BranchCode"] + "";
            drpPanelCourse.SelectedValue = _oo.ReturnTag(_sql, "Course");
            txtPanelBranchname.Text = _oo.ReturnTag(_sql, "BranchName");
            txtPanelShortName.Text = _oo.ReturnTag(_sql, "BranchShortName");
            txtPanelBranchCode.Text = _oo.ReturnTag(_sql, "BCode");
            txtPanelRemark.Text = _oo.ReturnTag(_sql, "Remark");
            if (_oo.ReturnTag(_sql, "IsDisplay") != "")
            {
                drpPanelIsDisplay.SelectedValue = _oo.ReturnTag(_sql, "IsDisplay");
            }
            string sql1 = _sql;
            LoadClass1();

            if (_oo.ReturnTag(sql1, "ClassId") != string.Empty)
            {
                drpPanelClass.SelectedValue = _oo.ReturnTag(sql1, "ClassId");
            }
            Panel1_ModalPopupExtender.Show();
        }
        protected void lnkConfirmDelete_Click(object sender, EventArgs e)
        {
            lnkDeleteNo.Focus();
            LinkButton lnk = (LinkButton)sender;
            GridViewRow currentrow = (GridViewRow)lnk.NamingContainer;

            Label lblId = (Label)currentrow.FindControl("lblId");

            lblvalue.Text = lblId.Text.Trim();

            Panel2_ModalPopupExtender.Show();
        }
        protected void lnkUpdate_Click(object sender, EventArgs e)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@Id", lblvalue.Text.Trim()));
            param.Add(new SqlParameter("@Course", drpPanelCourse.SelectedValue));
            param.Add(new SqlParameter("@Classid", drpPanelClass.SelectedValue));
            param.Add(new SqlParameter("@BranchName", txtPanelBranchname.Text.Trim()));
            param.Add(new SqlParameter("@BranchShortName", txtPanelShortName.Text.Trim()));
            param.Add(new SqlParameter("@BCode", txtPanelBranchCode.Text.Trim()));
            param.Add(new SqlParameter("@Remark", txtPanelRemark.Text.Trim()));
            param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
            param.Add(new SqlParameter("@LoginName", Session["LoginName"].ToString()));
            param.Add(new SqlParameter("@QueryType", "Update"));
            param.Add(new SqlParameter("@IsDisplay", drpPanelIsDisplay.SelectedValue)); 
            SqlParameter para = new SqlParameter("@Msg", "");
            para.Direction = ParameterDirection.Output;
            para.Size = 0x100;
            param.Add(para);

            var msg = new DLL().Sp_Insert_Update_Delete_usingExecuteNonQuery("BranchMasterProc", param);
            Campus camp = new Campus();
            if (msg == "D")
            {
                camp.msgbox(Page, msgbox, "Duplicate Entry!", "A");
            }
            if (msg == "U")
            {
                camp.msgbox(Page, msgbox, "Updated successfully.", "S");
            }
            LoadData();
            lblvalue.Text = "0";
        
        }
        protected void lnkDeleteYes_Click(object sender, EventArgs e)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@Id", lblvalue.Text.Trim()));
            param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
            param.Add(new SqlParameter("@QueryType", "Delete"));
            SqlParameter para = new SqlParameter("@Msg", "");
            para.Direction = ParameterDirection.Output;
            para.Size = 0x100;
            param.Add(para);

            var msg = new DLL().Sp_Insert_Update_Delete_usingExecuteNonQuery("BranchMasterProc", param);
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Deleted successfully.", "S");
            LoadData();
            lblvalue.Text = "0";
        }

        protected void LoadClass()
        {
            _sql = "Select ClassName,Id from ClassMaster where SessionName='" + Session["SessionName"] + "' and Course='" + drpCourse.SelectedValue + "' and BranchCode="+ Session["BranchCode"] + " order by Cidorder";
            _oo.FillDropDown_withValue(_sql, drpClass, "ClassName", "Id");
            drpClass.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
            if (drpClass.Items.Count > 1)
            {
                drpClass.Items.Insert(1, new ListItem("All", "-1"));
                drpClass.SelectedIndex = 1;

            }
      
        }

        protected void LoadClass1()
        {
            _sql = "Select ClassName,Id from ClassMaster where SessionName='" + Session["SessionName"] + "' and Course='" + drpPanelCourse.SelectedValue + "' and BranchCode=" + Session["BranchCode"] + " order by Cidorder";
      
            _oo.FillDropDown_withValue(_sql, drpPanelClass, "ClassName", "Id");
            drpPanelClass.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));

        }
        
        protected void drpCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadClass();
            drpCourse.Focus();
        }
        protected void txtBranchname_TextChanged(object sender, EventArgs e)
        {
            txtBranchCode.Text = txtBranchname.Text;
            txtShortName.Focus();
        }

        public override void Dispose()
        {
            _oo.Dispose();
        }
    }
}