using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace _1
{
    public partial class EnquiryReference : System.Web.UI.Page
    {
        private SqlConnection _con;
        private readonly Campus _oo;
        private string _sql,_sql1 =  String.Empty;

        public EnquiryReference()
        {
            _con = new SqlConnection();
            _oo = new Campus();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadData(); 
            if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
            {
                Response.Redirect("default.aspx");
            }
            _con = _oo.dbGet_connection();
            Campus camp = new Campus(); camp.LoadLoader(loader);
            if (!IsPostBack)
            {
                LoadData();
            }
        }
        
        private void LoadData()
        {
            _sql = "select Id,Name from knowAboutUs order By Id";
            Grd.DataSource = _oo.GridFill(_sql);
            Grd.DataBind();

            if (Grd.Rows.Count > 0)
            {
                for (int i = 0; i < Grd.Rows.Count; i++)
                {
                    Label RefrenceName = (Label)Grd.Rows[i].FindControl("RefrenceName");
                    LinkButton LinkButton2 = (LinkButton)Grd.Rows[i].FindControl("LinkButton2");
                    LinkButton LinkButton3 = (LinkButton)Grd.Rows[i].FindControl("LinkButton3");
                    _sql = "select Reference from AdmissionEnquiry where Reference='" + RefrenceName.Text.Trim() + "' ";
                    if (_oo.Duplicate(_sql))
                    {
                        LinkButton2.Text = "<i class='fa fa-lock'></i>";
                        LinkButton2.Enabled = false;
                        LinkButton3.Text = "<i class='fa fa-lock'></i>";
                        LinkButton3.Enabled = false;
                    }
                }
            }
            else
            {
                Grd.DataSource = null;
                Grd.DataBind();
            }
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            _sql1 = "select Id,Name from knowAboutUs  where Name='" + txtRefrenceName.Text.Trim() + "'";
            if (_oo.Duplicate(_sql1))
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Duplicate Refrence Name!", "A");                                    
            }
            else
            {
                _sql = "insert into knowAboutUs (name)values('" + txtRefrenceName.Text.Trim() + "')";

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
                        Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Submitted successfully.", "S");
                        txtRefrenceName.Text = "";
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
            LinkButton chk = (LinkButton)sender;
            Label lblId = (Label)chk.NamingContainer.FindControl("Label36");
            Label RefrenceName = (Label)chk.NamingContainer.FindControl("RefrenceName");
            string ss = lblId.Text;
            txtRefrenceNamePanel.Text = RefrenceName.Text;
            lblID.Text = ss;
            Panel1_ModalPopupExtender.Show();
        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            _sql1 = "select Id,Name from knowAboutUs  where Name='" + txtRefrenceNamePanel.Text.Trim() + "' and id<>"+ lblID.Text + "";
            if (_oo.Duplicate(_sql1))
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Duplicate Refrence Name!", "A");
            }
            else
            {
                _sql = "update knowAboutUs set name='" + txtRefrenceNamePanel.Text.Trim() + "' where id="+lblID.Text+"";

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
                        Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Submitted successfully.", "S");
                        LoadData();
                    }
                    catch (Exception)
                    {
                    }
                }

            }

        }
        protected void Button4_Click(object sender, EventArgs e)
        {

        }
        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            Button8.Focus();
            LinkButton chk = (LinkButton)sender;
            Label lblId = (Label)chk.NamingContainer.FindControl("Label37");
            var ss = lblId.Text;
            lblvalue.Text = ss;
            Panel2_ModalPopupExtender.Show();
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            _sql = "Delete from knowAboutUs where Id=" + lblvalue.Text;

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
                catch (Exception )
                {
                }
            }

        }
        protected void Button8_Click(object sender, EventArgs e)
        {

        }
        
        public override void Dispose()
        {
            _con.Dispose();
            _oo.Dispose();
        }
    }
}