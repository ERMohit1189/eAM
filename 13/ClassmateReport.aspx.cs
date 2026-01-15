using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _13_ClassmateReport : System.Web.UI.Page
{
    private SqlConnection _con;
    private readonly Campus _oo;
    private string _sql, _sql1 = String.Empty;

    public _13_ClassmateReport()
    {
        _con = new SqlConnection();
        _oo = new Campus();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Session["LoginName"] as string))
        {
            Response.Redirect("~/default.aspx");
        }
        if (Session["Logintype"].ToString().ToLower() == "admin")
        {
            Response.Redirect("~/default.aspx");
        }
        if (Session["Logintype"].ToString().ToLower() == "student")
        {
           // Response.Redirect("~/default.aspx");
        }
        if (Session["Logintype"].ToString().ToLower() == "guardian")
        {
           // Response.Redirect("~/default.aspx");
        }
        _con = _oo.dbGet_connection();
        _oo.LoadLoader(loader);

        if (!IsPostBack)
        {
            string SrNo = "";

            string Dollar = Session["LoginName"].ToString();
             //SrNo = Dollar.Substring(1, Dollar.Length - 1);
            Grd.DataSource = null;
            Grd.DataBind();
            if (Session["Logintype"].ToString().ToLower() == "guardian")
            {
                // Response.Redirect("~/default.aspx");
                SrNo = Dollar.Substring(1, Dollar.Length - 1);
            }
            else
            {
                SrNo = Dollar.ToString();
            }
            string _sql1 = " Select * from  AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "','" + Session["BranchCode"].ToString() + "')";
            _sql1 = _sql1 + " where SRNo = '" + SrNo + "' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "'";
            var dt = _oo.Fetchdata(_sql1);
            string ClassId = dt.Rows[0]["ClassId"].ToString();
            string BranchId = dt.Rows[0]["BranchId"].ToString();
            string SectionID = dt.Rows[0]["SectionID"].ToString();

            _sql = " Select * from  AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "','" + Session["BranchCode"].ToString() + "')";
            if(!String.IsNullOrEmpty(ClassId))
            {
                _sql = _sql + " where ClassId = '" + ClassId + "' ";
            }
            if (!String.IsNullOrEmpty(BranchId))
            {
                _sql = _sql + " and BranchId=" + BranchId + " and SectionID='" + SectionID + "'";
            }
            if (!String.IsNullOrEmpty(BranchId))
            {
                _sql = _sql + "  and SectionID='" + SectionID + "'";
            }

            SqlDataAdapter sdaa = new SqlDataAdapter(_sql, _con);
            DataTable dtt = new DataTable();
            sdaa.Fill(dtt);
            if (dtt.Rows.Count > 0)
            {
                Grd.DataSource = dtt;
                Grd.DataBind();
            }
            else
            {
                Grd.DataSource = dtt;
                Grd.DataBind();
            }
        }
    }
 

    public override void Dispose()
    {
        _con.Dispose();
        _oo.Dispose();
    }
}