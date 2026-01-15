using System;
using System.Data.SqlClient;

namespace _1
{
    public partial class AdminDuplicateGetPass : System.Web.UI.Page
    {
        private SqlConnection _con;
        private SqlCommand _cmd ;
        private readonly Campus _oo;
        private string _sql = "";

        public AdminDuplicateGetPass()
        {
            _cmd =  new SqlCommand();
            _con = new SqlConnection();
            _oo = new Campus();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
            {
                Response.Redirect("default.aspx");
            }
            _con = _oo.dbGet_connection();
            //BLL.BLLInstance.LoadHeader("Report", header);  //in cs file
            //BLL.BLLInstance.LoadHeader("Report", header1);  //in cs file
            if (!IsPostBack)
            {
                Loadadat();
            
            }
        }

        public void Loadadat()
        {
            _sql = "Select gpd.Id as MaxId,gpd.SrNo,asr.Name as StudentName,asr.FatherName,ClassName,SectionName,gpd.LoginName,";
            _sql = _sql + " FamilyContactNo,Reason,GuardianName,Relation,GuardionContact,gpd.RecordDate as date,ISNULL(gpd.StudentPhotoPath,ISNULL(asr.PhotoPath,'')) as StudentPhotopath,gpd.GuardianPhotoPath";
            _sql = _sql + " from AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") asr";
            _sql = _sql + " inner join GatePassData gpd on gpd.SrNo=asr.SrNo";
            _sql = _sql + " where gpd.Id='" + Session["Id"] + "' order by gpd.id desc";
            Repeater1.DataSource = _oo.GridFill(_sql);
            Repeater1.DataBind();
            Repeater2.DataSource = _oo.GridFill(_sql);
            Repeater2.DataBind();
        }
    
   
        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            PrintHelper_New.ctrl = abc2;
            ClientScript.RegisterStartupScript(GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
        }

        public override void Dispose()
        {
            _con.Dispose();
            _oo.Dispose();
            _cmd.Dispose();
        }
    }
}