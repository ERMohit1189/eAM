using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace _1
{
    public partial class AdminBoardMaster : System.Web.UI.Page
    {
        private SqlConnection _con;
        private readonly Campus _oo ;
        private string _sql = String.Empty;
        public AdminBoardMaster()
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
            TextBox2.Focus();

            
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

                    //string sqls = "select SessionName from SessionMaster where BranchCode=" + ddlBranch.SelectedValue + "";
                    //var dt2 = _oo.Fetchdata(sqls);
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
                }

              //  DrpSessionName.Items.Insert(0, new ListItem("<--Select-->", ""));
                LoadData();
                _oo.swipeLabelText(maindiv);
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
            LoadData();
        }
        protected void LoadData()
        {
            _sql = "Select  ROW_NUMBER() OVER (ORDER BY id ASC) AS SrNo,id, BoardName from BoardMaster ";
            _sql +=  " where BranchCode=" + ddlBranch.SelectedValue + "";
            GridView1.DataSource = _oo.GridFill(_sql);
            GridView1.DataBind();
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                Label Board = (Label)GridView1.Rows[i].FindControl("Label2");
                LinkButton LinkButton2 = (LinkButton)GridView1.Rows[i].FindControl("LinkButton2");
                LinkButton LinkButton3 = (LinkButton)GridView1.Rows[i].FindControl("LinkButton3");
                _sql = "select Board from StudentOfficialDetails where Board='"+ Board.Text.Trim()+"' and BranchCode =" + ddlBranch.SelectedValue + "";
                if (_oo.Duplicate(_sql))
                {
                    LinkButton2.Text = "<i class='fa fa-lock'></i>";
                    LinkButton2.Enabled = false;
                    LinkButton3.Text = "<i class='fa fa-lock'></i>";
                    LinkButton3.Enabled = false;
                }
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            _sql = "select BoardName from BoardMaster where BoardName='" + TextBox2.Text + "'";
            _sql +=  " and BranchCode=" + ddlBranch.SelectedValue.ToString() + "";
            if (_oo.Duplicate(_sql))
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Duplicate Board Name!", "A");                                    
            }
            else
            {

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "BoardMasterProc";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = _con;
                    cmd.Parameters.AddWithValue("@BoardName", TextBox2.Text);
                    cmd.Parameters.AddWithValue("@Remark", TextBox1.Text);
                    cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());//Session["Login"].ToString());
                    cmd.Parameters.AddWithValue("@BranchCode", ddlBranch.SelectedValue.ToString());
                  //  cmd.Parameters.AddWithValue("@SessionName", DrpSessionName.SelectedValue);
                    try
                    {
                        _con.Open();
                        cmd.ExecuteNonQuery();
                        _con.Close();
                        Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Submitted successfully.", "S");
                        LoadData();
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            var chk = (LinkButton)sender;
            var lblId = (Label)chk.NamingContainer.FindControl("Label36");
            var ss = lblId.Text;
            lblID.Text = ss;
            _sql = "Select  ROW_NUMBER() OVER (ORDER BY id ASC) AS SrNo,id, BoardName,Remark from BoardMaster ";
            _sql +=  " where Id=" + ss;
            _sql +=  " and BranchCode=" + ddlBranch.SelectedValue.ToString() + "";
            TextBox3.Text = _oo.ReturnTag(_sql, "BoardName");
            TextBox4.Text = _oo.ReturnTag(_sql, "Remark");
            Panel1_ModalPopupExtender.Show();
        }
        protected void LinkButton5_Click(object sender, EventArgs e)
        {

        }
        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            Button8.Focus();
            var chk = (LinkButton)sender;
            var lblId = (Label)chk.NamingContainer.FindControl("Label37");
            var ss = lblId.Text;
            lblvalue.Text = ss;
            Panel2_ModalPopupExtender.Show();
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            _sql = "Delete from BoardMaster where id=" + lblvalue.Text;

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = _sql;
                cmd.CommandType = CommandType.Text;
                cmd.Connection = _con;

                try
                {
                    _con.Open();
                    cmd.ExecuteNonQuery();
                    _con.Close();
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Deleted successfully.", "S");
                    LoadData();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        protected void Button8_Click(object sender, EventArgs e)
        {

        }
        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            _sql = "Select BoardName from BoardMaster  where BranchCode=" + ddlBranch.SelectedValue.ToString() + " and BoardName='"+ TextBox3.Text + "' and id<>"+lblID.Text+"";
            if (_oo.Duplicate(_sql))
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Duplicate Board Name", "A");
                return;
            }
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "BoardMasterUpdateProce";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", lblID.Text);
                cmd.Parameters.AddWithValue("@BoardName", TextBox3.Text);
                cmd.Parameters.AddWithValue("@Remark", TextBox4.Text);
                cmd.Connection = _con;
                try
                {
                    _con.Open();
                    cmd.ExecuteNonQuery();
                    _con.Close();
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Updated successfully.", "S");
                    LoadData();
                }
                catch (Exception )
                {
                    throw;
                }
            }
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
            _sql +=  " left join LoginType Lt on LTb.LoginTypeId=Lt.LoginTypeId where LT.LoginTypeName='" + Session["Logintype"] + "' and LTb.LoginName='" + Session["LoginName"] + "' and LTb.BranchId=" + ddlBranch.SelectedValue + "";
            int a, u, d;
            a = Convert.ToInt32(_oo.ReturnTag(_sql, "add1"));
            u = Convert.ToInt32(_oo.ReturnTag(_sql, "update1"));
            d = Convert.ToInt32(_oo.ReturnTag(_sql, "delete1"));

            PermissionGrant(a, d, u, (LinkButton)LinkButton1, btnDelete, LinkButton4);
        }
        protected void GridView1_PreRender(object sender, EventArgs e)
        {
            _oo.swipeLabelText(GridView1);
        }

        public override void Dispose()
        {
            _con.Dispose();
            _oo.Dispose();
        }
    }
}

