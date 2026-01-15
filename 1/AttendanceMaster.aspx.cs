using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace _1
{
    public partial class AdminAttendanceMaster : System.Web.UI.Page
    {
        private SqlConnection _con;
        private readonly Campus _oo;
        private string _sql = String.Empty;

        public AdminAttendanceMaster()
        {
            _con = new SqlConnection();
            _oo = new Campus();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string)Session["LoginName"] == "")
            {
                Response.Redirect("default.aspx");
            }
            _con = _oo.dbGet_connection();
            Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file

            if (!IsPostBack)
            {
                LoadGrid();
            }
            try
            {
                GridView1.FooterRow.Visible = false;
            }
            catch (Exception)
            {
            }
        }
        protected void LoadGrid()
        {
            _sql = "select ROW_NUMBER() OVER (ORDER BY Id ASC) AS SrNo,id,AttendanceName,AbbreviationName,Remark,ISNULL(isInclude,1) isInclude,CASE WHEN ValidFor='S' THEN 'Student' WHEN ValidFor='E' THEN 'Employee' ELSE 'Both' END ValidFor from AttendanceAbbreviationMaster";
            GridView1.DataSource = _oo.GridFill(_sql);
            GridView1.DataBind();
            
            if (GridView1.Rows.Count>0)
            {
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    Label lblValidFor = (Label)GridView1.Rows[i].FindControl("lblValidFor");
                    LinkButton LinkButton2 = (LinkButton)GridView1.Rows[i].FindControl("LinkButton2");
                    if (lblValidFor.Text == "Student")
                    {
                        _sql = "select count(*) cnt from AttendanceDetailsDateWise";
                        if (_oo.ReturnTag(_sql, "cnt")!="0")
                        {
                            LinkButton2.Text = "<i class='fa fa-lock'></i>";
                            LinkButton2.Enabled = false;
                        }
                    }
                    else
                    {
                        _sql = "select count(*) cnt from EmployeeAttendanceDayWise";
                        if (_oo.ReturnTag(_sql, "cnt") != "0")
                        {
                            LinkButton2.Text = "<i class='fa fa-lock'></i>";
                            LinkButton2.Enabled = false;
                        }
                    }
                }
                
            }
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            string sql1;
            sql1 = "select AbbreviationName from AttendanceAbbreviationMaster where AbbreviationName='" + ddlAbrevation.SelectedValue + "'";
            if (_oo.Duplicate(sql1))
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Duplicate Abbrevation Entry!", "A");
            }
            else
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "AttendanceAbbreviationMasterProc";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = _con;

                    string[] att = ddlAbrevation.SelectedItem.Text.Split(new string[] { " (" }, StringSplitOptions.None);
                    string AttendanceName = att[1].Replace(")", "");

                    cmd.Parameters.AddWithValue("@AttendanceName", AttendanceName);
                    cmd.Parameters.AddWithValue("@AbbreviationName", ddlAbrevation.SelectedValue);
                    cmd.Parameters.AddWithValue("@ValidFor", rblValidFor.SelectedValue.Trim());
                    cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());

                    try
                    {
                        _con.Open();
                        cmd.ExecuteNonQuery();
                        _con.Close();
                        Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Submitted successfully.", "S");
                        LoadGrid();
                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                }
            }
        }
    
        protected void LinkButton2_Click(object sender, EventArgs e)  //Delete
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
            _sql = "Delete from AttendanceAbbreviationMaster where Id=" + lblvalue.Text+ "";

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = _sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = _con;
            try
            {
                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
                LoadGrid();
            }
            catch (Exception )
            {
                throw;
            }
        }

        protected void Button8_Click(object sender, EventArgs e)
        {

        }

        public void PermissionGrant(int add1, int delete1, int update1, LinkButton ladd, Button ldelete, Button lUpdate)
        {
            ladd.Enabled = add1 == 1;
            ldelete.Enabled = delete1 == 1;
            lUpdate.Enabled = update1 == 1;
        }

        public override void Dispose()
        {
            _con.Dispose();
            _oo.Dispose();
        }
        
    }
}
