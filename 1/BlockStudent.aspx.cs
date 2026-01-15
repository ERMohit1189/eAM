using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _1
{
    public partial class SuperAdminBlockedStudent : Page
    {
        private SqlConnection _con;
        private readonly Campus _oo;
        private string _sql = "";
#pragma warning disable 169
        private DataTable _dt;
#pragma warning restore 169
        private static string _srno;

        public SuperAdminBlockedStudent()
        {
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
            Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file

            if (!IsPostBack)
            {
                LoadGrid();
                divRemark.Visible = false;
                divSubmit.Visible = false;
            }
        }

        protected void lnkShow_Click(object sender, EventArgs e)
        {
            LoadStudentGrid();
            LoadGrid();
        }

        public void LoadStudentGrid()
        {
            string studentId = Request.Form[hfStudentId.UniqueID];
            if (studentId == string.Empty)
            {
                studentId = txtStudentEnter.Text.Trim();
            }

            _sql = "Select *from StudentOfficialDetails where blocked='Yes' and srno='" + studentId + "' and BranchCode=" + Session["BranchCode"] + "";
            if (_oo.Duplicate(_sql)==false)
            {
                _sql = "Select id,SrNo,StEnRCode,Name as StudentName,FatherName, CombineClassName,ClassName,SectionName,Medium,Card,Convert(varchar(11),DateOfAdmiission) as DateOfAdmiission,CourseName,BranchName,FamilyContactNo,PhotoPath";
                _sql = _sql + " from AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") asr where srno='" + studentId + "' and Withdrwal is null";
                Grd.DataSource = _oo.GridFill(_sql);
                Grd.DataBind();
                DataSet ds;
                ds = _oo.GridFill(_sql);

                // ReSharper disable once UseNullPropagation
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


                            abc.Visible = true;
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
                    //oo.MessageBoxforUpdatePanel("This, Student is already blocked!", lnkShow);
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "This, Student is already blocked!", "A");
                    divRemark.Visible = false;
                    divSubmit.Visible = false;
                }
                else
                {
                    _srno = _oo.ReturnTag(_sql, "srno");
                    divRemark.Visible = true;
                    divSubmit.Visible = true;
                }
            }
            else
            {
                //oo.MessageBoxforUpdatePanel("This, Student is already blocked!", lnkShow);
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "This, Student is already blocked!", "A");
                divRemark.Visible = false;
                divSubmit.Visible = false;
                Grd.DataSource = null;
                Grd.DataBind();
            }
        }

        public void LoadGrid()
        {
            _sql = "Select id,SrNo,StEnRCode,Name as StudentName,FatherName, CombineClassName,ClassName,SectionName,Medium,Card,Convert(varchar(11),DateOfAdmiission) as DateOfAdmiission,CourseName,BranchName,FamilyContactNo,PhotoPath, blockedRemark";
            _sql = _sql + " from AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") asr where asr.Withdrwal is null and blocked='Yes' Order by CIDOrder Asc";
            GridView1.DataSource = _oo.GridFill(_sql);
            GridView1.DataBind();
        }
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            _sql = "update StudentOfficialDetails set Blocked='Yes',blockedRemark='"+txtRemark.Text.Trim()+"' where srno='"+_srno+ "' and BranchCode=" + Session["BranchCode"] + "";
            _oo.ProcedureDatabase(_sql);
            string srno="G" + _srno;
            _sql = "update LoginTab set IsActive=0 where LoginName='" + _srno + "' and BranchId='" + Session["BranchCode"] + " and Logintypeid=4";
            _oo.ProcedureDatabase(_sql);
            _sql = "update LoginTab set IsActive=0 where LoginName='" + srno + "' and BranchId='" + Session["BranchCode"] + " and Logintypeid=5";
            _oo.ProcedureDatabase(_sql);

            LoadGrid();
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Blocked successfully.", "S");
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Button8.Focus();
            LinkButton chk = (LinkButton)sender;
            Label lblId = (Label)chk.NamingContainer.FindControl("Label37");
       
            _srno = lblId.Text;
            Panel2_ModalPopupExtender.Show();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            _sql = "update StudentOfficialDetails set Blocked=null,blockedRemark=null where srno='" + _srno + "' and BranchCode=" + Session["BranchCode"] + "";
            _oo.ProcedureDatabase(_sql);
            string srno = "G" + _srno;
            _sql = "update LoginTab set IsActive=1 where LoginName='" + _srno + "' and BranchId='" + Session["BranchCode"] + " and Logintypeid=4";
            _oo.ProcedureDatabase(_sql);
            _sql = "update LoginTab set IsActive=1 where LoginName='" + srno + "' and BranchId='" + Session["BranchCode"] + " and Logintypeid=5";
            _oo.ProcedureDatabase(_sql);
            LoadGrid();
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Unblocked successfully.", "S");
        }

        protected void txtStudentEnter_TextChanged(object sender, EventArgs e)
        {
            LoadStudentGrid();
        }

        public override void Dispose()
        {
            _con.Dispose();
            _oo.Dispose();
        }
    }
}