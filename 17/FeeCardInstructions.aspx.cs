using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace _11
{
    public partial class FeeCardInstructions : System.Web.UI.Page
    {
        private string _sql = string.Empty;
        private DataSet _ds;
        Campus _oo = new Campus();
        public FeeCardInstructions()
        {
            _ds = new DataSet();
        }
        protected void Page_PreInIt(object sender, EventArgs e)
        {
            if (Session["Logintype"] == null) { Response.Redirect("~/default.aspx"); }
            switch ((string)Session["Logintype"])
            {
                case "Admin":
                    MasterPageFile = "~/Master/admin_root-manager.master";
                    break;
                case "Staff":
                    MasterPageFile = "~/Staff/staff_root-manager.master";
                    break;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Campus camp = new Campus(); camp.LoadLoader(loader);
            if (!IsPostBack)
            {
                int TempletId = 0;
                string ss = "select id from FeeCardTemplate where BranchCode=" + Session["BranchCode"].ToString() + " and Template='Template 1'";
                if (_oo.Duplicate(ss))
                {
                    LoadData(1);
                }
                else
                {
                    LoadData(2);
                }
            }
        }
        protected void lnkSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                int TempletId = 0;
                if (rdotemletOne.Checked==true && rdotemletTwo.Checked == false)
                {
                    TempletId = 1;
                }
                if (rdotemletOne.Checked == false && rdotemletTwo.Checked == true)
                {
                    TempletId = 2;
                }
                if (CKEditorControl1.Text == "")
                {
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Enter Fee Card Instructions", "W");
                    return;
                }
                
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@Instructions", CKEditorControl1.Text));
                param.Add(new SqlParameter("@templetId", TempletId.ToString()));
                param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
                param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
                param.Add(new SqlParameter("@QueryFor", "Save"));
                SqlParameter para = new SqlParameter("@Msg", "");
                para.Direction = ParameterDirection.Output;
                para.Size = 0x100;
                param.Add(para);
                var msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("sp_FeeCardInstructions", param);

                if (msg == "S")
                {
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Submitted successfully.", "S");
                }
                if (rdotemletOne.Checked == true && rdotemletTwo.Checked == false)
                {
                    LoadData(1);
                }
                if (rdotemletOne.Checked == false && rdotemletTwo.Checked == true)
                {
                    LoadData(2);
                }
                
            }
            catch (Exception ex)
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, ex.Message, "W");
            }
            
        }
        protected void LoadData(int TempletId)
        {
            
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
            param.Add(new SqlParameter("@templetId", TempletId.ToString()));

            param.Add(new SqlParameter("@QueryFor", "Select"));

            _ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("sp_FeeCardInstructions", param);
            if (_ds.Tables[0].Rows.Count > 0)
            {
                CKEditorControl1.Text = _ds.Tables[0].Rows[0]["Instructions"].ToString();
                if (TempletId==1)
                {
                    rdotemletOne.Checked = true; rdotemletTwo.Checked = false;
                    imgTemletOne.Visible = true; imgTemletTwo.Visible = false;

                }
                else if (TempletId == 2)
                {
                    rdotemletOne.Checked = false; rdotemletTwo.Checked = true;
                    imgTemletOne.Visible = false; imgTemletTwo.Visible = true;

                }
                else
                {
                    rdotemletOne.Checked = false; rdotemletTwo.Checked = false;
                    imgTemletOne.Visible = false; imgTemletTwo.Visible = false;

                }
            }
            else
            {
                if (TempletId == 1)
                {
                    rdotemletOne.Checked = true; rdotemletTwo.Checked = false;
                    imgTemletOne.Visible = true; imgTemletTwo.Visible = false;
                }
                else if (TempletId == 2)
                {
                    rdotemletOne.Checked = false; rdotemletTwo.Checked = true;
                    imgTemletOne.Visible = false; imgTemletTwo.Visible = true;
                }
                CKEditorControl1.Text = "";
            }
        }
        public override void Dispose()
        {
            _ds.Dispose();
        }

        protected void rdotemletOne_CheckedChanged(object sender, EventArgs e)
        {
            LoadData(1);
            imgTemletOne.Visible = true;
            imgTemletTwo.Visible = false;
            rdotemletTwo.Checked = false;
        }

        protected void rdotemletTwo_CheckedChanged(object sender, EventArgs e)
        {
            LoadData(2);
            rdotemletOne.Checked = false;
            imgTemletOne.Visible = false;
            imgTemletTwo.Visible = true;
        }
    }
}