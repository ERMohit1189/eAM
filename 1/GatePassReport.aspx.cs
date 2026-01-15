using System;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _1
{
    public partial class AdminGatePassReport : Page
    {
        private SqlConnection _con;
        private SqlCommand _cmd;
        readonly Campus _oo;
        private string _sql = String.Empty;
        public AdminGatePassReport()
        {
            _con = new SqlConnection();
            _oo = new Campus();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string)Session["LoginName"] == "" || (string)Session["BranchCode"] == "" || (string)Session["SessionName"] == "" || Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
            {
                Response.Redirect("default.aspx");
            }
            _con = _oo.dbGet_connection();
            if (!IsPostBack)
            {
                Loadadat();
                //Image5.ImageUrl = "DisplayImage.ashx?UserLoginID=" + 1;
                _sql = "Select CollegeName from CollegeMaster where CollegeId=" + 1;
                //lblCollegeName.Text = oo.ReturnTag(sql, "CollegeName");
            }

        }

        public void Loadadat()
        {
            _sql = "Select gpd.id as MaxId,gpd.SrNo,asr.Name as StudentName,asr.FatherName,ClassName, combineClassName,SectionName,";
            _sql = _sql + " FamilyContactNo,Reason,GuardianName,Relation,GuardionContact,gpd.RecordDate as date,ISNULL(gpd.StudentPhotoPath,ISNULL(asr.PhotoPath,'')) as StudentPhotopath,gpd.GuardianPhotoPath";
            _sql = _sql + " from AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") asr";
            _sql = _sql + " inner join GatePassData gpd on gpd.SrNo=asr.SrNo where gpd.SessionName='" + Session["SessionName"] + "' and gpd.BranchCode=" + Session["BranchCode"] + " and convert(date, gpd.RecordDate) between '" + txtFromdate.Text+ "' and  '" + txtToDate.Text + "'";
            _sql = _sql + " order by gpd.id desc";
            Repeater1.DataSource = _oo.GridFill(_sql);
            Repeater1.DataBind();
            if (Repeater1.Items.Count > 0)
            {
                rptDiv.Visible = true;
            }
            else
            {
                rptDiv.Visible = false;
            }
       
        }
   
        protected void LinkButton1_OnClick(object sender, EventArgs e)
        {
            var lnk = (LinkButton)sender;
            var lbtn = (Label)lnk.NamingContainer.FindControl("lblmaxid");
            // var lnk = (LinkButton)sender;
            Session["Id"] = lbtn.Text;
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('DuplicateGetPass.aspx?print=1');", true);
        }

        protected void lnkDelete_OnClick(object sender, EventArgs e)
        {
            var lnk = (LinkButton)sender;
            Session["MaxId"] = lnk.ToolTip;
            Panel1_ModalPopupExtender.Show();

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            _sql = "Update GatePassData set Cancel='Yes',Canceldate=(Select GetDate()) where Id='" + Session["MaxId"] + "' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            _cmd = new SqlCommand(_sql,_con);
            _con.Open();
            _cmd.ExecuteNonQuery();
            _con.Close();
            Loadadat();
            _oo.MessageBox("Canceled successfully", Page);
        }

        public override void Dispose()
        {
            _con.Dispose();
            _oo.Dispose();
        }

        protected void LinkView_Click(object sender, EventArgs e)
        {
            Loadadat();
        }
    }
}