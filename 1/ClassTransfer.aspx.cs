using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI.WebControls;

namespace _1
{
    public partial class AdminGroupTransfer : System.Web.UI.Page
    {
        SqlConnection _con;
        private readonly Campus _oo;
        private string _sql = "";
        readonly string _sectionT = "";

        public AdminGroupTransfer()
        {
            _con = new SqlConnection();
            _oo = new Campus();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            DrpEnter.Focus();

            if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
            {
                Response.Redirect("default.aspx");
            }

            _con = _oo.dbGet_connection();
            Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file

            //con.ConnectionString = ConfigurationManager.ConnectionStrings["Menu"].ConnectionString;
            if (!IsPostBack)
            {

                LoadCourse();
                LoadClass();
                LoadBranch();
                LoadSection();
                Panel1.Visible = false;
                LinkButton3.Visible = false;
                LinkButton4.Visible = false;
            }
        }

        private void LoadCourse()
        {
            BLL.BLLInstance.loadCourseWithoutSession(drpCourse, Session["SessionName"].ToString());
        }

        private void LoadClass()
        {
            BLL.BLLInstance.loadClassUseingCourse(drpClass, drpCourse.SelectedValue.ToString(), Session["SessionName"].ToString());
        }

        private void LoadBranch()
        {
            BLL.BLLInstance.loadBranch(drpBranch, Session["SessionName"].ToString(),drpClass.SelectedValue.ToString());
        }

        private void LoadSection()
        {
            BLL.BLLInstance.loadSection(drpSection, Session["SessionName"].ToString(), drpClass.SelectedValue.ToString());
        }

        protected void TxtEnter_TextChanged(object sender, EventArgs e)
        {
            var studentId = Request.Form[hfStudentId.UniqueID];
            if (studentId == string.Empty)
            {
                studentId = TxtEnter.Text.Trim();
            }
            string sqls = " declare @count int=0 ";
            sqls = sqls + " if exists(select * from CompositFeeDeposit where BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "' and receiptStatus<>'Cancelled')";
            sqls = sqls + " begin select @count=count(*) from CompositFeeDeposit where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "' end ";
            sqls = sqls + " select @count Counts ";
            if (_oo.ReturnTag(sqls, "Counts") != "0")
            {
                lblWarning.Visible = true;
                lblWarning.Text = "Please cancel all the receipts of current session for this student!";
                View(1);
            }
            else
            {
                lblWarning.Visible = false;
                View(0);
            }
        }
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            var studentId = Request.Form[hfStudentId.UniqueID];
            if (studentId == string.Empty)
            {
                studentId = TxtEnter.Text.Trim();
            }
            string sqls = " declare @count int=0 ";
            sqls = sqls + " if exists(select * from CompositFeeDeposit where BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "' and receiptStatus<>'Cancelled')";
            sqls = sqls + " begin select @count=count(*) from CompositFeeDeposit where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "' end ";
            sqls = sqls + " select @count Counts ";
            if (_oo.ReturnTag(sqls, "Counts") != "0")
            {
                lblWarning.Visible = true;
                lblWarning.Text = "Please cancel all the receipts of current session for this student!";
                View(1);
            }
            else
            {
                lblWarning.Visible = false;
                View(0);
            }
            
        }

        protected void View(int sts)
        {
            var studentId = Request.Form[hfStudentId.UniqueID];
            if (studentId == string.Empty)
            {
                studentId = TxtEnter.Text.Trim();
            }

            GridView1.Visible = false;

            _sql = "Select SrNo,StEnRCode,Name as StudentName,FatherName, CombineClassName,ClassName,SectionName,Medium,Card,Convert(varchar(11),DateOfAdmiission) as DateOfAdmiission,CourseName,BranchName,FamilyContactNo,PhotoPath";
            _sql +=  " from AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") asr where srno='" + studentId + "' and Withdrwal is null";

            Grd.DataSource = _oo.GridFill(_sql);
            Grd.DataBind();
            DataSet ds;
            ds = _oo.GridFill(_sql);

            // ReSharper disable once UseNullPropagation
            if (ds != null && Grd.Rows.Count > 0)
            {
                grdshow.Visible = true;
                if (ds.Tables[0].Rows.Count > 0)
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

                            if (dsPhoto.Tables[0].Rows.Count > 0)
                            {
                                img.ImageUrl = dsPhoto.Tables[0].Rows[0]["PhotoPath"].ToString() != string.Empty ? dsPhoto.Tables[0].Rows[0]["PhotoPath"] + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture) : "../img/user-pic/user-pic.jpg" + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture);
                                studentImg.NavigateUrl = dsPhoto.Tables[0].Rows[0]["PhotoPath"].ToString() != string.Empty ? dsPhoto.Tables[0].Rows[0]["PhotoPath"] + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture) : "../img/user-pic/user-pic.jpg" + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture);
                                hylinkmoredetails.NavigateUrl = "../11/StudentRegView.aspx?print=1&id=" + ds.Tables[0].Rows[0]["stenrcode"];
                            }
                        }
                    }
                }
            }
            Application[_sectionT] = _oo.ReturnTag(_sql, "GroupNa");
            if (sts == 1)
            {
                Panel1.Visible = false;
                LinkButton3.Visible = false;
                LinkButton4.Visible = false;
            }
            else
            {
                Panel1.Visible = true;
                LinkButton3.Visible = true;
                LinkButton4.Visible = true;

                if (Grd.Rows.Count == 0)
                {
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Invalid " + DrpEnter.SelectedItem, "A");

                    Panel1.Visible = false;
                    LinkButton3.Visible = false;
                    LinkButton4.Visible = false;
                }
            }
        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            string studentId = Request.Form[hfStudentId.UniqueID];
            if (studentId == string.Empty)
            {
                studentId = TxtEnter.Text.Trim();
            }

            _sql = "select Reason,convert(nvarchar,RecordDate,106) as TransferDate,Classold as OldClass,Classnew as NewClass";
            _sql +=  " from ClassTransferDetails Gtd";
            _sql +=  " where  SrNo='" + studentId + "' and sessionname='"+ Session["SessionName"] + "'";
            _sql +=  "  and BranchCode=" + Session["BranchCode"] + " order by Gtd.id";
   
            GridView1.DataSource = _oo.GridFill(_sql);
            GridView1.DataBind();
            GridView1.Visible = true;

            if (GridView1.Rows.Count == 0)
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Previous transfer details not available!", "A");
            }
        
        }
        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            Submit();
        }

        protected void Submit()
        {
            var studentId = Request.Form[hfStudentId.UniqueID];
            if (studentId == string.Empty)
            {
                studentId = TxtEnter.Text.Trim();
            }

            _sql = "Select AdmissionForClassId from StudentOfficialDetails where srno='" + studentId + "' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";

            if (_oo.ReturnTag(_sql, "AdmissionForClassId").Trim() == drpClass.SelectedValue.Trim())
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Same Class can not be Transferred!", "A");
            }
            else
            {
             
                _sql = "Select SrNo,StEnRCode,Name as StudentName,FatherName,ClassName,SectionName,BranchName,Medium,Card,Convert(varchar(11),DateOfAdmiission) as DateOfAdmiission,CourseId,BranchId";
                _sql +=  " from AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") asr where srno='" + studentId + "' and Withdrwal is null";


                var section = _oo.ReturnTag(_sql, "SectionName");
                var branch = _oo.ReturnTag(_sql, "BranchName");
                var Classold = _oo.ReturnTag(_sql, "ClassName");
                var Medium = _oo.ReturnTag(_sql, "Medium");
                var dateofAdmission  = _oo.ReturnTag(_sql, "DateOfAdmiission");
                var fatherName  = _oo.ReturnTag(_sql, "FatherName");
                var studentName  = _oo.ReturnTag(_sql, "StudentName");
                var enroll  = _oo.ReturnTag(_sql, "StEnRCode");
                var srNo  = _oo.ReturnTag(_sql, "srno");



                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Classtransferdetailsproc";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@StEnRCode", enroll);
                cmd.Parameters.AddWithValue("@SrNo", srNo);
                cmd.Parameters.AddWithValue("@StudentName", studentName.Trim());
                cmd.Parameters.AddWithValue("@FatherName", fatherName);
              
                cmd.Parameters.AddWithValue("@Section", section);
                cmd.Parameters.AddWithValue("@Branch", branch);
                cmd.Parameters.AddWithValue("@AdmissionDate", dateofAdmission);

                cmd.Parameters.AddWithValue("@Medium", Medium);

                cmd.Parameters.AddWithValue("@Classid", drpClass.SelectedValue.ToString().Trim());
                cmd.Parameters.AddWithValue("@BranchId", drpBranch.SelectedValue.ToString().Trim());
                cmd.Parameters.AddWithValue("@SectionId", drpSection.SelectedValue.ToString().Trim());
                cmd.Parameters.AddWithValue("@Courseid", drpCourse.SelectedValue.ToString().Trim());

                cmd.Parameters.AddWithValue("@Classold", Classold);
                cmd.Parameters.AddWithValue("@Classnew", drpClass.SelectedItem.Text.Trim());

                cmd.Parameters.AddWithValue("@Reason", TextBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                cmd.Connection = _con;

                try
                {
                    _con.Open();
                    cmd.ExecuteNonQuery();
                    _con.Close();
                    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Transferred successfully", "S");
                    TextBox1.Text = string.Empty;
                    string sqls = " if exists(select * from CCENurtoPrep_1718 where BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "') ";
                    sqls = sqls + " begin delete from CCENurtoPrep_1718 where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "' end ";
                    sqls = sqls + " if exists(select * from CCEItoV where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "') ";
                    sqls = sqls + " begin delete from CCEItoV where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "' end ";
                    sqls = sqls + " if exists(select * from CCEVItoVIII_1718 where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "') ";
                    sqls = sqls + " begin delete from CCEVItoVIII_1718 where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "' end ";
                    sqls = sqls + " if exists(select * from CCEIXtoX_1718 where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "') ";
                    sqls = sqls + " begin delete from CCEIXtoX_1718 where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "' end ";
                    sqls = sqls + " if exists(select * from CCEXI_1718 where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "') ";
                    sqls = sqls + " begin delete from CCEXI_1718 where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "' end ";
                    sqls = sqls + " if exists(select * from CCEXII_1718 where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "') ";
                    sqls = sqls + " begin delete from CCEXII_1718 where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "' end ";

                    sqls = sqls + " if exists(select * from UPEAditionalEntryItoV where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "') ";
                    sqls = sqls + " begin delete from UPEAditionalEntryItoV where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "' end ";

                    sqls = sqls + " if exists(select * from UPEAditionalEntryVItoVIII where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "') ";
                    sqls = sqls + " begin delete from UPEAditionalEntryVItoVIII where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "' end ";

                    sqls = sqls + " if exists(select * from UPEAditionalEntryIXtoX where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "') ";
                    sqls = sqls + " begin delete from UPEAditionalEntryIXtoX where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "' end ";

                    sqls = sqls + " if exists(select * from UPEAditionalEntryPrePrimary where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "') ";
                    sqls = sqls + " begin delete from UPEAditionalEntryPrePrimary where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "' end ";

                    sqls = sqls + " if exists(select * from UPEAditionalEntryXI where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "') ";
                    sqls = sqls + " begin delete from UPEAditionalEntryXI where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "' end ";

                    sqls = sqls + " if exists(select * from UPEAditionalEntryXII where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "') ";
                    sqls = sqls + " begin delete from UPEAditionalEntryXII where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "' end ";

                    sqls = sqls + " if exists(select * from UPEItoV where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "') ";
                    sqls = sqls + " begin delete from UPEItoV where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "' end ";

                    sqls = sqls + " if exists(select * from UPEIXtoX where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "') ";
                    sqls = sqls + " begin delete from UPEIXtoX where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "' end ";

                    sqls = sqls + " if exists(select * from UPEXI where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "') ";
                    sqls = sqls + " begin delete from UPEXI where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "' end ";

                    sqls = sqls + " if exists(select * from UPEXII where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "') ";
                    sqls = sqls + " begin delete from UPEXII where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "' end ";

                    sqls = sqls + " if exists(select * from UPEVItoVIII where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "') ";
                    sqls = sqls + " begin delete from UPEVItoVIII where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "' end ";

                    sqls = sqls + " if exists(select * from UPEIXtoX where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "') ";
                    sqls = sqls + " begin delete from UPEIXtoX where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "' end ";

                    sqls = sqls + " if exists(select * from SGSMarkEntryPrePrimary where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "') ";
                    sqls = sqls + " begin delete from SGSMarkEntryPrePrimary where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "' end ";

                    sqls = sqls + " if exists(select * from SGSMarkEntryPrimary where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "') ";
                    sqls = sqls + " begin delete from SGSMarkEntryPrimary where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "' end ";

                    sqls = sqls + " if exists(select * from SGSMarkEntryJunior where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "') ";
                    sqls = sqls + " begin delete from SGSMarkEntryJunior where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "' end ";

                    sqls = sqls + " if exists(select * from SGSMarkEntrySecondary where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "') ";
                    sqls = sqls + " begin delete from SGSMarkEntrySecondary where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "' end ";

                    sqls = sqls + " if exists(select * from SGSMarkEntryXI where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "') ";
                    sqls = sqls + " begin delete from SGSMarkEntryXI where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "' end ";

                    sqls = sqls + " if exists(select * from SGSMarkEntryXII where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "') ";
                    sqls = sqls + " begin delete from SGSMarkEntryXII where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "' end ";

                    sqls = sqls + " if exists(select * from PrePrimaryMarksEntry where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "') ";
                    sqls = sqls + " begin delete from PrePrimaryMarksEntry where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "' end ";


                    sqls = sqls + " if exists(select * from AttendanceDetailsDateWise where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and SrNo='" + studentId + "') ";
                    sqls = sqls + " begin delete from AttendanceDetailsDateWise where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and SrNo='" + studentId + "' end ";
                    using (SqlCommand cmds = new SqlCommand())
                    {
                        cmds.CommandText = sqls;
                        cmds.CommandType = CommandType.Text;
                        cmds.Connection = _con;
                        try
                        {
                            _con.Open();
                            cmds.ExecuteNonQuery();
                            _con.Close();
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                catch (Exception ex) {
                    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, ex.Message, "W");
                }

                _sql = "Select SrNo,StEnRCode,Name as StudentName,FatherName,ClassName, CombineClassName,SectionName,Medium,Card,Convert(varchar(11),DateOfAdmiission) as DateOfAdmiission,CourseName,BranchName,FamilyContactNo,PhotoPath";
                _sql +=  " from AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") asr where srno='" + studentId + "' and Withdrwal is null";

                Grd.DataSource = _oo.GridFill(_sql);
                Grd.DataBind();
            }
        }

        public void PermissionGrant(int add1, LinkButton ladd)
        {


            if (add1 == 1)
            {
                ladd.Enabled = true;
            }
            else
            {
                ladd.Enabled = false;
            }

        }
        public void CheckValueAddDeleteUpdate()
        {
            _sql = " select LoginId,LoginName,Pass,SessionId,BranchId,LT.LoginTypeName,ltb.add1 as add1,ltb.delete1 as delete1,ltb.update1 as update1 from LoginTab LTb";
            _sql +=  " left join LoginType Lt on LTb.LoginTypeId=Lt.LoginTypeId where LT.LoginTypeName='" + Session["Logintype"] + "' and LTb.LoginName='" + Session["LoginName"] + "' and LTb.BranchId=" + Session["BranchCode"] + "'";
            var a = Convert.ToInt32(_oo.ReturnTag(_sql, "add1"));


            // ReSharper disable once RedundantCast
            PermissionGrant(a, (LinkButton)LinkButton4);
        }

        

        public override void Dispose()
        {
            _con.Dispose();
            _oo.Dispose();
        }

        protected void drpCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadClass();
        }

        protected void drpClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadBranch();
            LoadSection();
        }
    }
}
