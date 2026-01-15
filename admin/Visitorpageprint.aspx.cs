using System;
using System.Data;
using System.Data.SqlClient;

namespace admin
{
    public partial class AdminDefault3 : System.Web.UI.Page
    {
        private SqlConnection _con = new SqlConnection();
        private readonly Campus _oo = new Campus();
        private string _printid = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            _con = _oo.dbGet_connection();
            //BLL.BLLInstance.LoadHeader("Receipt", header);
            //lbldatetime.Text = DateTime.Now.ToString("dd MMM yyyy h:mm:ss tt");
            if (IsPostBack) return;
            try
            {
                if (Request.QueryString.Keys.Count <= 0) return;
                _printid = Request.QueryString["empcode"];
                Getdataprint(_printid);
                PrintHelper_New.ctrl = abc;
                ClientScript.RegisterStartupScript(GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
            }
            catch (SqlException)
            { }
        }

        public void Getdataprint(string idvisitor)
        {
            try
            {
                var cmd = new SqlCommand
                {
                    CommandText = "USP_Visitors",
                    CommandType = CommandType.StoredProcedure,
                    Connection = _con
                };
                cmd.Parameters.AddWithValue("@QueryFor", "P");
                cmd.Parameters.AddWithValue("@id", idvisitor);
                var da = new SqlDataAdapter {SelectCommand = cmd};
                var dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count <= 0) return;
                Label1.Text = dt.Rows[0]["ID"].ToString().Trim();
                cardno.Text = dt.Rows[0]["passno"].ToString().Trim();
                lbldate.Text = dt.Rows[0]["Date"].ToString().Trim();
                lbltime.Text = dt.Rows[0]["Time"].ToString().Trim();
                Label2.Text = dt.Rows[0]["VisitorName"].ToString().Trim();
                Image3.ImageUrl = dt.Rows[0]["PhotoPath"].ToString().Trim();
                Label3.Text = dt.Rows[0]["ContactNo"].ToString().Trim();
                Label4.Text = dt.Rows[0]["SubjectVisit"].ToString().Trim();
                Label5.Text = dt.Rows[0]["WhomMeet"].ToString().Trim();
                Label6.Text = dt.Rows[0]["Gender"].ToString().Trim();
                Label7.Text = dt.Rows[0]["EmailID"].ToString().Trim();
                Label8.Text = dt.Rows[0]["Address"].ToString().Trim();
                Label9.Text = dt.Rows[0]["OutApprovedTime"].ToString().Trim();
            }
            finally { if (_con.State == ConnectionState.Open) { _con.Close(); } }
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            PrintHelper_New.ctrl = abc;
            ClientScript.RegisterStartupScript(GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
        }
    }
}