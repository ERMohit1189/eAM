using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI.WebControls;

namespace _1
{
    public partial class changeTypeOfAdmission : System.Web.UI.Page
    {
        private SqlConnection _con;
        readonly Campus _oo;
        private string _sql = "";

        public changeTypeOfAdmission()
        {
            _con = new SqlConnection();
            _oo = new Campus();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string) Session["LoginName"] == "" || (string)Session["BranchCode"] == "" || (string)Session["SessionName"] == "" || Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
            {
                Response.Redirect("default.aspx");
            }
            _con = _oo.dbGet_connection();
            Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file

            if (!IsPostBack)
            {
                LinkButton2.Visible = false;
                ddlType.Visible = false;
            }
        }
        protected void lnkShow_Click(object sender, EventArgs e)
        {

        }
        protected void DrpEnter_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void TxtEnter_TextChanged(object sender, EventArgs e)
        {
            View();
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {

            View();
        }

        protected void View()
        {
            ddlType.SelectedIndex = 0;
            var studentId = Request.Form[hfStudentId.UniqueID];
            if (studentId == string.Empty)
            {
                studentId = TxtEnter.Text.Trim();
            }
            _sql = "Select id,SrNo,StEnRCode,Name as StudentName,FatherName,ClassName,CombineClassName,SectionName,Medium,Card,Convert(varchar(11),DateOfAdmiission) as DateOfAdmiission,CourseName,BranchName,FamilyContactNo,PhotoPath, TypeOFAdmision";
            _sql = _sql + " from AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") asr where srno='" + studentId + "' and Withdrwal is null";
            Grd.DataSource = _oo.GridFill(_sql);
            Grd.DataBind();
            Grd.Visible = true;
            DataSet ds;
            ds = _oo.GridFill(_sql);

            if (ds != null && Grd.Rows.Count > 0)
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = BAL.objBal.dbGet_connection().ConnectionString;
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "USP_StudentsPhotoReport";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@sessionName", Session["SessionName"].ToString().Trim());
                        cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString().Trim());
                        cmd.Parameters.AddWithValue("@SrNo", studentId.ToString().Trim());
                        cmd.Parameters.AddWithValue("@action", "details");
                        SqlDataAdapter das = new SqlDataAdapter(cmd);
                        DataSet dsPhoto = new DataSet();
                        das.Fill(dsPhoto);
                        cmd.Parameters.Clear();

                        LinkButton2.Visible = true;
                        ddlType.Visible = true;
                        grdshow.Visible = true;
                        if (dsPhoto.Tables[0].Rows.Count > 0)
                        {
                            img.ImageUrl = dsPhoto.Tables[0].Rows[0]["PhotoPath"].ToString() != string.Empty ? dsPhoto.Tables[0].Rows[0]["PhotoPath"] + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture) : "../img/user-pic/user-pic.jpg" + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture);
                            studentImg.NavigateUrl = dsPhoto.Tables[0].Rows[0]["PhotoPath"].ToString() != string.Empty ? dsPhoto.Tables[0].Rows[0]["PhotoPath"] + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture) : "../img/user-pic/user-pic.jpg" + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture);
                            hylinkmoredetails.NavigateUrl = "../11/StudentRegView.aspx?print=1&id=" + ds.Tables[0].Rows[0]["stenrcode"];
                        }
                    }
                }
            }
            

            if (Grd.Rows.Count == 0)
            {
                LinkButton2.Visible = true;
                ddlType.Visible = true;
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "No Records Found", "A");
            }
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            Label label1 = (Label)Grd.Rows[0].FindControl("srno");
            string ss = "select id from CompositFeeDeposit where SrNo='" + label1.Text.Trim() + "' and BranchCode=" + Session["BranchCode"] + "  and SessionName='" + Session["SessionName"].ToString().Trim() + "' and receiptStatus<>'Cancelled' ";
            if (_oo.Duplicate(ss))
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Please cancel all the receipts!", "A");
                return;
            }
            var cmd = new SqlCommand();
            cmd.CommandText = "update StudentOfficialDetails set TypeOFAdmision='"+ddlType.SelectedValue.Trim() + "' where SrNo='"+ label1.Text.Trim()+ "' and SessionName='"+ Session["SessionName"].ToString().Trim() + "' and BranchCode=" + Session["BranchCode"] + "";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = _con;
            try
            {
                LinkButton2.Visible = false;
                ddlType.Visible = false;
                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
                View();
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Updated successfully.", "S");
            }
            catch (SqlException) { }
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