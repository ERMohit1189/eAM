using System;
using System.Data;
using System.Data.SqlClient;

namespace admin
{
    public partial class AdminVisitorReport : System.Web.UI.Page
    {
        readonly Campus _clsCam;
        private SqlConnection _con;
        readonly Campus _oo;
#pragma warning disable 169
        private readonly string _sql = "";
#pragma warning restore 169
#pragma warning disable 169
        private readonly string _ss = "";
#pragma warning restore 169
        public AdminVisitorReport()
        {
            _clsCam = new Campus();
            _con = new SqlConnection();
            _oo = new Campus();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Campus camp = new Campus(); camp.LoadLoader(loader);
            _con = _oo.dbGet_connection();
            if (!IsPostBack)
            {
                try
                {
                    Getdate();
                    Getdataprint();
                }
                catch (SqlException)
                { }
            }
        }

        // ReSharper disable once IdentifierTypo
        public void Getdate()
        {
            _oo.AddDateMonthYearDropDown(FromYY, FromMM, FromDD);
            _oo.AddDateMonthYearDropDown(ToYY, ToMM, ToDD);
            _oo.FindCurrentDateandSetinDropDown(FromYY, FromMM, FromDD);
            _oo.FindCurrentDateandSetinDropDown(ToYY, ToMM, ToDD);
        }

        // ReSharper disable once IdentifierTypo
        public void Getdataprint()
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "USP_Visitors";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = _con;
                cmd.Parameters.AddWithValue("@QueryFor", "S");
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    repeatermember.DataSource = dt;
                    repeatermember.DataBind();
                }
                else
                {
                
                }
            }
            catch (SqlException ee) { throw new Exception("some reason to rethrow", ee); }
            finally { if (_con.State == ConnectionState.Open) { _con.Close(); } }
        }

        protected void FromYY_SelectedIndexChanged(object sender, EventArgs e)
        {
            _oo.YearDropDown(FromYY, FromMM, FromDD);
        }
        protected void FromMM_SelectedIndexChanged(object sender, EventArgs e)
        {
            _oo.MonthDropDown(FromYY, FromMM, FromDD);
        }
        protected void FromDD_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void ToYY_SelectedIndexChanged(object sender, EventArgs e)
        {
            _oo.YearDropDown(ToYY, ToMM, ToDD);
        }
        protected void ToMM_SelectedIndexChanged(object sender, EventArgs e)
        {
            _oo.MonthDropDown(ToYY, ToMM, ToDD);
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            var cmd = new SqlCommand();
            using (var da = new SqlDataAdapter())
            {
                try
                {
                    var todate = ToDD.SelectedItem + " " + ToMM.SelectedItem + " " + ToYY.SelectedItem;
                    var fromdate = FromDD.SelectedItem + " " + FromMM.SelectedItem + " " + FromYY.SelectedItem;
                    cmd.CommandText = "USP_Visitors";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = _con;
                    cmd.Parameters.AddWithValue("@QueryFor", "TF");
                    cmd.Parameters.AddWithValue("@FromDate", fromdate);
                    cmd.Parameters.AddWithValue("@Todate", todate);
                    da.SelectCommand = cmd;
                    var dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        repeatermember.DataSource = dt;
                        repeatermember.DataBind();
                    }
                }
                catch (SqlException ee) { throw new Exception("some reason to rethrow", ee); }
                finally { if (_con.State == ConnectionState.Open) { _con.Close(); } }
            }
        }

        public override void Dispose()
        {
            _clsCam.Dispose();
            _con.Dispose();
            _oo.Dispose();
        }
    }
}