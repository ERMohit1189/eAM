using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace _1
{
    public partial class GroupMaster : System.Web.UI.Page
    {
       Campus _oo = new Campus();
        private readonly BAL.GenralInfo _objBal = new BAL.GenralInfo();
        private string _sql = String.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
            {
                Response.Redirect("default.aspx");
            }
            Campus camp = new Campus(); camp.LoadLoader(loader);
            if (!IsPostBack)
            {
                BLL.BLLInstance.loadClass(drpClass, Session["SessionName"].ToString());
                BLL.BLLInstance.loadBranch(drpBranch, Session["SessionName"].ToString(),drpClass.SelectedValue);
                LoadGrid("-1");
            }
        }

        public void LoadGrid(string id)
        {
            var param = new List<SqlParameter>
            {
                new SqlParameter("@QueryFor", "S"),
                new SqlParameter("@SessionName", Session["SessionName"].ToString()),
                new SqlParameter("@ClassId", drpClass.SelectedValue),
                new SqlParameter("@BranchCode", Session["BranchCode"]),
                new SqlParameter("@BranchId", drpBranch.SelectedValue),
                new SqlParameter("@Id", id)
            };

            var ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("StreamMasterProc", param);

            if (id == "-1")
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
            else
                {
                Panel1_ModalPopupExtender.Show();
                if (ds == null) return;
                if (ds.Tables.Count <= 0) return;
                BLL.BLLInstance.loadClass(drpClassPanel, Session["SessionName"].ToString());
                if (drpClassPanel.Items.Count > 0)
                {
                    drpClassPanel.SelectedValue = ds.Tables[0].Rows[0]["ClassId"].ToString();
                }
                if (ds.Tables[0].Rows.Count <= 0) return;
                BLL.BLLInstance.loadBranch(drpBranchPanel, Session["SessionName"].ToString(), ds.Tables[0].Rows[0]["ClassId"].ToString());
                if (drpBranchPanel.Items.Count > 0)
                {
                    drpBranchPanel.SelectedValue = ds.Tables[0].Rows[0]["BranchId"].ToString();
                }
                txtStreamPanel.Text = ds.Tables[0].Rows[0]["Stream"].ToString();

                txtRemarkPanel.Text = ds.Tables[0].Rows[0]["Remark"].ToString();
            }
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                Label ids = (Label)GridView1.Rows[i].FindControl("lblEditid");
                LinkButton lnkEdit = (LinkButton)GridView1.Rows[i].FindControl("lnkEdit");
                LinkButton lnkDelete = (LinkButton)GridView1.Rows[i].FindControl("lnkDelete");

                _sql = "select * from AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") where streamid='" + ids + "' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                if (_oo.Duplicate(_sql))
                {
                    lnkEdit.Text = "<i class='fa fa-lock'></i>";
                    lnkEdit.Enabled = false;
                    lnkDelete.Text = "<i class='fa fa-lock'></i>";
                    lnkDelete.Enabled = false;
                }
            }
        }

        protected void lnkSubmit_Click(object sender, EventArgs e)
        {
            if (txtStream.Text == string.Empty)
            {
                //BAL.objBal.MessageBoxforUpdatePanel("Please, Enter Stream!", lnkSubmit);
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Please, Enter Stream!", "S");
            }
            else
            {
                Save();
            }
        }

        private void Save()
        {
            // ReSharper disable once RedundantAssignment
            var flag = "0";
            var param = new List<SqlParameter>
            {
                new SqlParameter("@QueryFor", "I"),
                new SqlParameter("@ClassId", drpClass.SelectedValue),
                new SqlParameter("@BranchId", drpBranch.SelectedValue),
                new SqlParameter("@Stream", txtStream.Text.Trim()),
                new SqlParameter("@Remark", txtRemark.Text.Trim()),
                new SqlParameter("@SessionName", Session["SessionName"].ToString()),
                new SqlParameter("@LoginName", Session["LoginName"].ToString()),
                new SqlParameter("@BranchCode", Session["BranchCode"].ToString())
            };
            var para = new SqlParameter("@Msg", "")
            {
                Direction = ParameterDirection.Output,
                Size = 0x100
            };
            param.Add(para);
            flag = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("StreamMasterProc", param);
            switch (flag)
            {
                case "2":
                    Campus camp1 = new Campus(); camp1.msgbox(Page, msgbox, "Duplicate Entry!", "S");
                    break;
                case "1":
                    Campus camp2 = new Campus(); camp2.msgbox(Page, msgbox, "Submitted successfully.", "S");                                    
                    txtStream.Text = string.Empty;
                    break;
                default:
                    Campus camp3 = new Campus(); camp3.msgbox(Page, msgbox, "Sorry, Record not Submitted!", "S");
                    break;
            }
            LoadGrid("-1");
        }

        protected void drpClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            BLL.BLLInstance.loadBranch(drpBranch, Session["SessionName"].ToString(), drpClass.SelectedValue);
            LoadGrid("-1");
        }

        protected void drpBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGrid("-1");
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            LinkButton lnk=(LinkButton)sender;
            Label lblIds = (Label)lnk.NamingContainer.FindControl("lblEditid");
            lblId.Text = lblIds.Text;
            LoadGrid(lblIds.Text);
        }

        protected void lnkUpdate_Click(object sender, EventArgs e)
        {
            Update();
        }

        private void Update()
        {
            string flag = "0";

            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@QueryFor", "U"));
            param.Add(new SqlParameter("@ClassId", drpClassPanel.SelectedValue));
            param.Add(new SqlParameter("@BranchId", drpBranchPanel.SelectedValue));
            param.Add(new SqlParameter("@Stream", txtStreamPanel.Text.Trim()));
            param.Add(new SqlParameter("@Remark", txtRemarkPanel.Text.Trim()));
            param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"]));
            param.Add(new SqlParameter("@Id", lblId.Text.Trim()));

            SqlParameter para = new SqlParameter("@Msg", "");
            para.Direction = ParameterDirection.Output;
            para.Size = 0x100;
            param.Add(para);

            flag = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("StreamMasterProc", param);

            if (flag == "2")
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Duplicate Entry!", "S");                                    
            }
            else if (flag == "1")
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Updated successfully.", "S");                                    
            }
            else
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Sorry, Record not Submitted!", "S");                                    
            }
            LoadGrid("-1");
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            Button8.Focus();
            LinkButton lnk = (LinkButton)sender;
            Label lblIds = (Label)lnk.NamingContainer.FindControl("lblDeleteId");
            lblvalue.Text = lblIds.Text;

            Panel2_ModalPopupExtender.Show();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void Delete()
        {
            string flag = "0";

            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@QueryFor", "D"));
            param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"]));
            param.Add(new SqlParameter("@Id", lblvalue.Text.Trim()));

            SqlParameter para = new SqlParameter("@Msg", "");
            para.Direction = ParameterDirection.Output;
            para.Size = 0x100;
            param.Add(para);

            flag = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("StreamMasterProc", param);

            if (flag == "1")
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Deleted successfully.", "S");
                LoadGrid("-1");
            }
            else
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Sorry, Record not Deleted!", "S");
                LoadGrid("-1");
            }
            LoadGrid("-1");
        }

        protected void drpClassPanel_SelectedIndexChanged(object sender, EventArgs e)
        {
            BLL.BLLInstance.loadBranch(drpBranchPanel, Session["SessionName"].ToString(), drpClassPanel.SelectedValue);
            Panel1_ModalPopupExtender.Show();
        }

    }
}