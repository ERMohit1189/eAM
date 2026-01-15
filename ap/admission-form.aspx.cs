using System;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace ap
{
    public partial class ApAdmissionForm : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection();
        Campus oo = new Campus();
        string sql = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            con = oo.dbGet_connection();
            if (!IsPostBack)
            {
                LoadSession();
            }
        }
        public void LoadSession()
        {
            sql = "Select Convert(varchar(8),Datepart(Year,FromDate))+'-'+Convert(varchar(8),Datepart(Year,ToDate)) as Sessionfor from";
            sql = sql + " SessionMaster where SessionName='" + Session["SessionName"] + "'";
            sql = sql + " Union";
            sql = sql + " Select Convert(varchar(8),(Datepart(Year,FromDate)+1))+'-'+Convert(varchar(8),(Datepart(Year,ToDate)+1)) as Sessionfor from ";
            sql = sql + " SessionMaster where SessionName='" + Session["SessionName"] + "'";

            oo.FillDropDownWithOutSelect(sql, drpSession, "Sessionfor");
            drpSession.Items.Insert(0, new ListItem("<-- Select Session -->", "<-- Select Session -->"));
        }
    }
}