using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace _1
{
    public partial class AdminEntranceExamMaster : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtExamName.Focus();
                LoadGrid();
            }
        }

        //Get ALL Exam Records
        public void LoadGrid()
        {
            DataSet ds;

            List<SqlParameter> param = new List<SqlParameter>();

            param.Add(new SqlParameter("@QueryFor", "S"));
            param.Add(new SqlParameter("@Id", "-1"));
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"]));

            ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("EntranceExamMaster_Proc", param);

            if (ds != null)
            {
                DataTable dt;
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    Repeater1.DataSource = dt;
                    Repeater1.DataBind();
                }
                else
                {
                    Repeater1.DataSource = null;
                    Repeater1.DataBind();
                }
            }
        }

        protected void lnkSubmit_Click(object sender, EventArgs e)
        {
            CreateExam();
        }

        //Insert Exam Record
        public void CreateExam()
        {
            string msg;

            List<SqlParameter> param = new List<SqlParameter>();

            param.Add(new SqlParameter("@QueryFor", "I"));
            param.Add(new SqlParameter("@ExamName", txtExamName.Text.Trim()));
            param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
            param.Add(new SqlParameter("@LoginName", Session["LoginName"].ToString()));

            SqlParameter para = new SqlParameter("@Msg", "");
            para.Size = 0x100;
            para.Direction = ParameterDirection.Output;

            param.Add(para);

            msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("EntranceExamMaster_Proc", param);

            if (msg.Trim() == "S")
            {
                txtExamName.Text = string.Empty;
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Submitted successfully.", "S");
                LoadGrid();
            }
            if (msg.Trim() == "DU")
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Sorry, This Record already exists!", "A");
            }
            else
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, msg.Trim(), "W"); 
            }
            txtExamName.Focus();
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            hfid.Value = string.Empty;

            LinkButton lnkId = (LinkButton)sender;
            Label label4 = (Label)lnkId.NamingContainer.FindControl("Label4");
            hfid.Value = label4.Text;
            GetInsertedRecord();

            Panel1_ModalPopupExtender.Show();
        }

        //Get Unique Exam Record Using Id
        public void GetInsertedRecord()
        {
            DataSet ds;

            List<SqlParameter> param = new List<SqlParameter>();

            param.Add(new SqlParameter("@Id", hfid.Value));
            param.Add(new SqlParameter("@QueryFor", "S"));
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));

            ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("EntranceExamMaster_Proc", param);

            if (ds != null)
            {
                DataTable dt;
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    txtExamNamePanel.Text = dt.Rows[0][0].ToString();
                }
            }
        }

        protected void lnkUpdate_Click(object sender, EventArgs e)
        {
            UpdateExam();
        }

        //Update Unique Exam Record
        public void UpdateExam()
        {
            string msg;

            List<SqlParameter> param = new List<SqlParameter>();

            param.Add(new SqlParameter("@QueryFor", "U"));
            param.Add(new SqlParameter("@Id", hfid.Value));
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
            param.Add(new SqlParameter("@ExamName", txtExamNamePanel.Text.Trim()));

            SqlParameter para = new SqlParameter("@Msg", "");
            para.Size = 0x100;
            para.Direction = ParameterDirection.Output;

            param.Add(para);

            msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("EntranceExamMaster_Proc", param);

            if (msg.Trim() == "S")
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Updated successfully.", "S");
                LoadGrid();
            }
            if (msg.Trim() == "DU")
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Sorry, This Record already exists!", "A");
            }
            else
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, msg.Trim(), "W");
            }

        }

        protected void lnkCancel_Click(object sender, EventArgs e)
        {
            hfid.Value = string.Empty;
            LinkButton lnkId = (LinkButton)sender;
            Label label4 = (Label)lnkId.NamingContainer.FindControl("Label4");
            hfid.Value = label4.Text;
            Panel2_ModalPopupExtender.Show();
            lnkNo.Focus();
        }

        protected void lnkDeleteyes_Click(object sender, EventArgs e)
        {
            DeleteExam();
        }

        //Delete Unique Exam Record
        public void DeleteExam()
        {
            string msg;

            List<SqlParameter> param = new List<SqlParameter>();

            param.Add(new SqlParameter("@QueryFor", "D"));
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
            param.Add(new SqlParameter("@Id", hfid.Value));

            SqlParameter para = new SqlParameter("@Msg", "");
            para.Size = 0x100;
            para.Direction = ParameterDirection.Output;

            param.Add(para);

            msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("EntranceExamMaster_Proc", param);

            if (msg.Trim() == "S")
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Deleted successfully.", "S");
                LoadGrid();
            }
            if (msg.Trim() == "RE")
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Sorry, This Record is used in other process!", "A");
            }
            else
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, msg.Trim(), "W");
            }
        }
    }
}