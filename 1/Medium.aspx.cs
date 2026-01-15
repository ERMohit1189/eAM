using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace _1
{
    public partial class AdminFeeGroup : System.Web.UI.Page
    {
        private SqlConnection _con;
        private readonly Campus _oo;
        private string _sql = String.Empty;
        public AdminFeeGroup()
        {
            _con = new SqlConnection();
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
        protected void Page_Load(object sender, EventArgs e)
        {
            
            _con = _oo.dbGet_connection();
            Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file

            if (!IsPostBack)
            {
                try
                {
                    CheckValueAddDeleteUpdate();
                }
                catch (Exception)
                {
                    // ignored
                }
                string sql = "Select BranchId, BranchName from Branchtab ";
                var dt = _oo.Fetchdata(sql);
                ddlBranch.DataSource = dt;
                ddlBranch.DataTextField = "BranchName";
                ddlBranch.DataValueField = "BranchId";
                ddlBranch.DataBind();
                ddlBranch.Items.Insert(0, new ListItem("<--Select-->", ""));
                if (Session["LoginType"].ToString() == "Admin")
                {
                    divBranch.Visible = false;
                    ddlBranch.SelectedValue = Session["BranchCode"].ToString();
                    load();
                }
                if (Session["LoginType"].ToString() == "SuperAdmin")
                {
                
                }
                else
                {
                    load();
                }
                    // DrpSessionName.Items.Insert(0, new ListItem("<--Select-->", ""));
                  
            }
            //try
            //{
            //    GridView1.FooterRow.Visible = false;
            //}
            //catch (Exception)
            //{
            //    // ignored
            //}
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
            //if (Session["LoginType"].ToString() == "Admin")
            //{
            //    DrpSessionName.SelectedValue = Session["SessionName"].ToString();
            //}
            loadMain();
        }
        protected void load()
        {

            _sql = "Select  ROW_NUMBER() OVER (ORDER BY id ASC) AS SrNo,id, Medium,Remark,ShortName,IsDisplay from MediumMaster where BranchCode=" + Session["BranchCode"] + "";
           // _sql +=  " where  SessionName='" + DrpSessionName.SelectedValue + "' and BranchCode=" + ddlBranch.SelectedValue + "";
            GridView1.DataSource = _oo.GridFill(_sql);
            GridView1.DataBind();
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                Label Label2 = (Label)GridView1.Rows[i].FindControl("Label2");
                LinkButton lnkEdit = (LinkButton)GridView1.Rows[i].FindControl("LinkButton2");
                LinkButton lnkDelete = (LinkButton)GridView1.Rows[i].FindControl("LinkButton3");

                //_sql = "select Medium from AllStudentRecord_UDF('" + DrpSessionName.SelectedValue + "'," + ddlBranch.SelectedValue + ") where Medium='" + Label2.Text + "'  and BranchCode=" + ddlBranch.SelectedValue + "";
                _sql = "select Medium from StudentOfficialDetails where Medium='" + Label2.Text + "'  and BranchCode=" + ddlBranch.SelectedValue + "";
                if (_oo.Duplicate(_sql))
                {
                    lnkEdit.Text = "<i class='fa fa-lock'></i>";
                    lnkEdit.Enabled = false;

                    lnkDelete.Text = "<i class='fa fa-lock'></i>";
                    lnkDelete.Enabled = false;
                }
            }
        }
        protected void loadSuperAdmin()
        {
            _sql = "Select  ROW_NUMBER() OVER (ORDER BY id ASC) AS SrNo,id, Medium,Remark,ShortName from MediumMaster";
            // _sql +=  " where  SessionName='" + DrpSessionName.SelectedValue + "' and BranchCode=" + ddlBranch.SelectedValue + "";
            GridView1.DataSource = _oo.GridFill(_sql);
            GridView1.DataBind();
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                Label Label2 = (Label)GridView1.Rows[i].FindControl("Label2");
                LinkButton lnkEdit = (LinkButton)GridView1.Rows[i].FindControl("LinkButton2");

                //_sql = "select Medium from AllStudentRecord_UDF('" + DrpSessionName.SelectedValue + "'," + ddlBranch.SelectedValue + ") where Medium='" + Label2.Text + "'  and BranchCode=" + ddlBranch.SelectedValue + "";
                _sql = "select Medium from StudentOfficialDetails where Medium='" + Label2.Text + "'  and BranchCode=" + ddlBranch.SelectedValue + "";
                if (_oo.Duplicate(_sql))
                {
                    lnkEdit.Text = "<i class='fa fa-lock'></i>";
                    lnkEdit.Enabled = false;
                }
            }
        }
        protected void loadMain()
        {
            _sql = "Select  ROW_NUMBER() OVER (ORDER BY id ASC) AS SrNo,id, Medium,Remark,ShortName,IsDisplay from MediumMaster where BranchCode=" + ddlBranch.SelectedValue + "";
            // _sql +=  " where  SessionName='" + DrpSessionName.SelectedValue + "' and BranchCode=" + ddlBranch.SelectedValue + "";
            GridView1.DataSource = _oo.GridFill(_sql);
            GridView1.DataBind();
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                Label Label2 = (Label)GridView1.Rows[i].FindControl("Label2");
                LinkButton lnkEdit = (LinkButton)GridView1.Rows[i].FindControl("LinkButton2");

                //_sql = "select Medium from AllStudentRecord_UDF('" + DrpSessionName.SelectedValue + "'," + ddlBranch.SelectedValue + ") where Medium='" + Label2.Text + "'  and BranchCode=" + ddlBranch.SelectedValue + "";
                _sql = "select Medium from StudentOfficialDetails where Medium='" + Label2.Text + "'  and BranchCode=" + ddlBranch.SelectedValue + "";
                if (_oo.Duplicate(_sql))
                {
                    lnkEdit.Text = "<i class='fa fa-lock'></i>";
                    lnkEdit.Enabled = false;
                }
            }
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {

            _sql = "select Medium  from MediumMaster where Medium ='" + txtMedium.Text + "'";
            _sql +=  " and BranchCode=" + ddlBranch.SelectedValue + "";
            if (_oo.Duplicate(_sql))
            {
                //oo.MessageBox("Duplicate Enntry", this.Page);
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Duplicate Entry!", "A");
            }
            else
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "MediumMasterProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = _con;
                cmd.Parameters.AddWithValue("@Medium ", txtMedium.Text);
                cmd.Parameters.AddWithValue("@Remark", TextBox2.Text);
                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());//Session["Login"].ToString());
                cmd.Parameters.AddWithValue("@BranchCode", ddlBranch.SelectedValue.ToString());
                //cmd.Parameters.AddWithValue("@SessionName", DrpSessionName.SelectedValue);
                cmd.Parameters.AddWithValue("@ShortName", txtShortName.Text);
                cmd.Parameters.AddWithValue("@IsDisplay", drpIsDisplay.SelectedValue);
                try
                {
                    _con.Open();
                    cmd.ExecuteNonQuery();
                    _con.Close();
                    //oo.MessageBox("Submitted successfully", this.Page);
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Submitted successfully.", "S");
                    _oo.ClearControls(Page);
                     Session["BranchCode"] = ddlBranch.SelectedValue;
                    load();
                }
                catch (Exception)
                {
                    // ignored
                }
            }
        }
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            LinkButton chk = (LinkButton)sender;
            Label lblId = (Label)chk.NamingContainer.FindControl("Label36");
            string ss = lblId.Text;
            lblID.Text = ss;
            _sql = "Select  ROW_NUMBER() OVER (ORDER BY id ASC) AS SrNo,id, Medium,Remark,ShortName,IsDisplay from MediumMaster ";
            _sql +=  " where Id=" + ss;
            _sql +=  " and  BranchCode=" + ddlBranch.SelectedValue + "";
            txtMedium1.Text = _oo.ReturnTag(_sql, "Medium");
            TextBox4.Text = _oo.ReturnTag(_sql, "Remark");
            txtShortName1.Text = _oo.ReturnTag(_sql, "ShortName");
            DropDownList1.Text = _oo.ReturnTag(_sql, "IsDisplay");
            Panel1_ModalPopupExtender.Show();
        }
        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            Button8.Focus();
            LinkButton chk = (LinkButton)sender;
            Label lblId = (Label)chk.NamingContainer.FindControl("Label37");
            string ss = lblId.Text;
            lblvalue.Text = ss;
            Panel2_ModalPopupExtender.Show();
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            _sql = "Delete from MediumMaster where id=" + lblvalue.Text;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = _sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = _con;
            try
            {
                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
                //oo.MessageBox("Deleted successfully.", this.Page);
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Deleted successfully.", "S");
                load();
            }
            catch (Exception)
            {
                // ignored
            }
        }
        protected void Button8_Click(object sender, EventArgs e)
        {

        }
        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "MediumMasterUpdateProce";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", lblID.Text);
            cmd.Parameters.AddWithValue("@Medium", txtMedium1.Text);
            cmd.Parameters.AddWithValue("@Remark", TextBox4.Text);
            cmd.Parameters.AddWithValue("@ShortName", txtShortName1.Text);
            cmd.Parameters.AddWithValue("@IsDisplay", DropDownList1.SelectedValue);
            cmd.Connection = _con;
            try
            {
                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
                //oo.MessageBox("Updated successfully.", this.Page);
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Updated successfully.", "S");
                load();
            }
            catch (Exception)
            {
                // ignored
            }
        }


        protected void LinkButton5_Click(object sender, EventArgs e)
        {

        }

        public void PermissionGrant(int add1, int delete1, int update1, LinkButton ladd, Button ldelete, LinkButton lUpdate)
        {
            ladd.Enabled = add1 == 1;
            ldelete.Enabled = delete1 == 1;
            lUpdate.Enabled = update1 == 1;
        }
        public void CheckValueAddDeleteUpdate()
        {
            _sql = " select LoginId,LoginName,Pass,SessionId,BranchId,LT.LoginTypeName,ltb.add1 as add1,ltb.delete1 as delete1,ltb.update1 as update1 from LoginTab LTb";
            _sql +=  " left join LoginType Lt on LTb.LoginTypeId=Lt.LoginTypeId where LT.LoginTypeName='" + Session["Logintype"] + "' and LTb.LoginName='" + Session["LoginName"] + "'";
            int a, u, d;
            a = Convert.ToInt32(_oo.ReturnTag(_sql, "add1"));
            u = Convert.ToInt32(_oo.ReturnTag(_sql, "update1"));
            d = Convert.ToInt32(_oo.ReturnTag(_sql, "delete1"));

            // ReSharper disable once RedundantCast
            PermissionGrant(a, d, u, (LinkButton)LinkButton1, btnDelete, LinkButton4);
        }

        public override void Dispose()
        {
            _con.Dispose();
            _oo.Dispose();
        }
    }
}