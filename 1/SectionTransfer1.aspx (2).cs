using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI.WebControls;

namespace _1
{
    public partial class SectionTransfer : System.Web.UI.Page
    {
        private SqlConnection _con;
        readonly Campus _oo;
        private string _sql = "";
        readonly string _sectionT = "";

        public SectionTransfer()
        {
            _con = new SqlConnection();
            _oo = new Campus();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            DrpEnter.Focus();

            if ((string) Session["LoginName"] == "" || (string)Session["BranchCode"] == "" || (string)Session["SessionName"] == "" || Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
            {
                Response.Redirect("default.aspx");
            }

            _con = _oo.dbGet_connection();
            Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file

            //con.ConnectionString = ConfigurationManager.ConnectionStrings["Menu"].ConnectionString;
            if (!IsPostBack)
            {


                try
                {
                    CheckValueAddDeleteUpdate();
                }
                catch (Exception)
                {
                    // ignored
                }

                _sql = _sql + " where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                _oo.FillDropDown(_sql, DrpSectionTransfer, "SectionName");
                Panel1.Visible = false;
                LinkButton3.Visible = false;
                LinkButton4.Visible = false;
            }


        }
        protected void TxtEnter_TextChanged(object sender, EventArgs e)
        {
            string studentId = Request.Form[hfStudentId.UniqueID];
            if (studentId == string.Empty)
            {
                studentId = TxtEnter.Text.Trim();
            }
            //string sqls = " declare @count int=0 ";
            //sqls = sqls + " if exists(select * from TutionFeeDeposit where BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "'  and status<>'Cancelled')";
            //sqls = sqls + " begin select @count=count(*) from TutionFeeDeposit where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "' end ";
            //sqls = sqls + " if exists(select * from TransportFeeDeposit where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "'  and status<>'Cancelled')";
            //sqls = sqls + " begin select @count=count(*) from TransportFeeDeposit where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "' end ";
            //sqls = sqls + " if exists(select * from HostelFeeDeposit where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and SrNoOrEmpid='" + studentId + "'  and status<>'Cancelled')";
            //sqls = sqls + " begin select @count=count(*) from HostelFeeDeposit where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and SrNoOrEmpid='" + studentId + "' end ";
            //sqls = sqls + " if exists(select * from MiscellaneousFeeDeposit where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "'  and status<>'Cancelled')";
            //sqls = sqls + " begin select  @count=count(*) from MiscellaneousFeeDeposit where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "' end ";
            //sqls = sqls + " select @count Counts ";
            //if (_oo.ReturnTag(sqls, "Counts") != "0")
            //{
            //    lblWarning.Visible = true;
            //    lblWarning.Text = "Please cancel all the receipts of current session for this student!";
            //    BindGrid(1);
            //}
            //else
            //{
                lblWarning.Visible = false;
                BindGrid(0);
            //}
        }
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            string studentId = Request.Form[hfStudentId.UniqueID];
            if (studentId == string.Empty)
            {
                studentId = TxtEnter.Text.Trim();
            }
            //string sqls = " declare @count int=0 ";
            //sqls = sqls + " if exists(select * from TutionFeeDeposit where BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "'  and status<>'Cancelled')";
            //sqls = sqls + " begin select @count=count(*) from TutionFeeDeposit where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "' end ";
            //sqls = sqls + " if exists(select * from TransportFeeDeposit where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "'  and status<>'Cancelled')";
            //sqls = sqls + " begin select @count=count(*) from TransportFeeDeposit where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "' end ";
            //sqls = sqls + " if exists(select * from HostelFeeDeposit where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and SrNoOrEmpid='" + studentId + "'  and status<>'Cancelled')";
            //sqls = sqls + " begin select @count=count(*) from HostelFeeDeposit where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and SrNoOrEmpid='" + studentId + "' end ";
            //sqls = sqls + " if exists(select * from MiscellaneousFeeDeposit where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "'  and status<>'Cancelled')";
            //sqls = sqls + " begin select  @count=count(*) from MiscellaneousFeeDeposit where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "' end ";
            //sqls = sqls + " select @count Counts ";
            //if (_oo.ReturnTag(sqls, "Counts") != "0")
            //{
            //    lblWarning.Visible = true;
            //    lblWarning.Text = "Please cancel all the receipts of current session for this student!";
            //    BindGrid(1);
            //}
            //else
            //{
                lblWarning.Visible = false;
                BindGrid(0);
            //}
        }

        protected void BindGrid(int sts)
        {
            string studentId = Request.Form[hfStudentId.UniqueID];
            if (studentId == string.Empty)
            {
                studentId = TxtEnter.Text.Trim();
            }

            GridView1.Visible = false;


            _sql = "Select id,SrNo,StEnRCode,Name as StudentName, CombineClassName, FatherName,ClassName,SectionName,Medium,Card,Convert(varchar(11),DateOfAdmiission) as DateOfAdmiission,CourseName,BranchName,FamilyContactNo,PhotoPath,classID";
            _sql = _sql + " from AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") asr where srno='" + studentId + "' and Withdrwal is null";
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
            Session[_sectionT] = _oo.ReturnTag(_sql, "SectionName");
            string classid = _oo.ReturnTag(_sql, "classID");
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
                    //oo.MessageBox("Invalid " + DrpEnter.SelectedItem.ToString(), this.Page);
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Invalid " + DrpEnter.SelectedItem, "A");

                    Panel1.Visible = false;
                    LinkButton3.Visible = false;
                    LinkButton4.Visible = false;

                }

                else
                {
                    string sql = "  select SectionName   from  SectionMaster where ClassNameId= '" + classid + "'";
                    sql = sql + " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                    _oo.FillDropDown(sql, DrpSectionTransfer, "SectionName");
                }
            }
        }

        protected void LinkButton2_Click1(object sender, EventArgs e)
        {


        }
        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            string studentId = Request.Form[hfStudentId.UniqueID];
            if (studentId == string.Empty)
            {
                studentId = TxtEnter.Text.Trim();
            }

            _sql = "select StEnRCode,SrNo,StudentName,Class,Section,Sectiontransfer,Reason,convert(nvarchar,RecordDate,106) as TransferDate,Reason from SectionTransferDetails where  Srno='" + studentId + "'";
            _sql = _sql + " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            _sql = _sql + " order by id";
            GridView1.DataSource = _oo.GridFill(_sql);
            GridView1.DataBind();
            GridView1.Visible = true;
            if (GridView1.Rows.Count == 0)
            {
                //oo.MessageBox("Previous Transfer details not available!", this.Page);
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Previous transfer details not available!", "A");

            }
        





        }
        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            if (DrpSectionTransfer.SelectedItem.ToString() == "<--Select-->")
            {
                //oo.MessageBox("Please Select Condition!", this.Page);
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Please Select Condition!", "A");
            }
            else
            {
                string studentId = Request.Form[hfStudentId.UniqueID];
                if (studentId == string.Empty)
                {
                    studentId = TxtEnter.Text.Trim();
                }

          
                _sql = "select SG.Id, sc.id,cm.id as clasID,SC.SectionName as SectionName,CM.ClassName,convert(nvarchar, So.DateOfAdmiission, 106) as DateOfAdmiission,";
                _sql = _sql + "   SO.SectionId,Sf.FatherName,SF.MotherName,SG.FirstName,SG.MiddleName,SG.LastName,sg.StEnRCode as StEnRCode,sg.srno as srno FROM StudentGenaralDetail SG";
                _sql = _sql + "   LEFT join StudentFamilyDetails SF on SG.StEnRCode = SF.StEnRCode   AND SF.SessionName = sg.SessionName";
                _sql = _sql + "   LEFT join StudentOfficialDetails SO on SG.StEnRCode = SO.StEnRCode  AND SO.SessionName = sg.SessionName";
                _sql = _sql + "   LEFT join ClassMaster CM on SO.AdmissionForClassId = CM.Id    AND CM.SessionName = sg.SessionName";
                _sql = _sql + "   LEFT join SectionMaster SC on SO.SectionId = SC.Id AND sc.SessionName = sg.SessionName";
                _sql = _sql + "   Where  SG.Srno=" + "'" + studentId + "'";
                _sql = _sql + "   And sg.SessionName='" + Session["SessionName"] + "' and sg.BranchCode=" + Session["BranchCode"] + "";
                _sql = _sql + "   and sf.BranchCode=" + Session["BranchCode"] + " and sc.BranchCode=" + Session["BranchCode"] + " and so.BranchCode=" + Session["BranchCode"] + " and cm.BranchCode=" + Session["BranchCode"] + " And SO.Withdrwal is null";

                if (_oo.ReturnTag(_sql, "SectionName").Trim() == DrpSectionTransfer.SelectedItem.ToString().Trim())
                {
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Could not be transferred in same section!", "A");
                }
                else
                {

                    _sql = "select SG.Id, SC.SectionName,CM.ClassName,convert(nvarchar,So.DateOfAdmiission,106) as DateOfAdmiission ,SO.SectionId,Sf.FatherName,SF.MotherName,SG.FirstName,SG.MiddleName,SG.LastName,sg.StEnRCode as StEnRCode,sg.srno  as srno, so.AdmissionForClassId from StudentGenaralDetail SG ";
                    _sql = _sql + "    left join StudentFamilyDetails SF on SG.StEnRCode=SF.StEnRCode and SG.BranchCode=SF.BranchCode and SG.SessionName=SF.SessionName";
                    _sql = _sql + "   left join StudentOfficialDetails SO on SG.StEnRCode=SO.StEnRCode and SG.BranchCode=SO.BranchCode and SG.SessionName=SO.SessionName";
                    _sql = _sql + "   left join ClassMaster CM on SO.AdmissionForClassId=CM.Id and CM.BranchCode=SO.BranchCode and CM.SessionName=SO.SessionName";
                    _sql = _sql + "   left join SectionMaster SC on SO.SectionId=SC.Id and SC.BranchCode=SO.BranchCode and SC.SessionName=SO.SessionName";
                    _sql = _sql + "    where  SG.Srno=" + "'" + studentId + "'";
                    _sql = _sql + " and sg.SessionName='" + Session["SessionName"] + "' and sg.BranchCode=" + Session["BranchCode"] + "";
                    _sql = _sql + "  and SO.Withdrwal is null";

                    var section = _oo.ReturnTag(_sql, "SectionName");
                    var Class = _oo.ReturnTag(_sql, "ClassName");
                    var ClassId = _oo.ReturnTag(_sql, "AdmissionForClassId");
                    var dateofAdmission = _oo.ReturnTag(_sql, "DateOfAdmiission");
                    var fatherName = _oo.ReturnTag(_sql, "FatherName");
                    var firstName = _oo.ReturnTag(_sql, "FirstName");
                    var middleName = _oo.ReturnTag(_sql, "MiddleName");
                    var lastName = _oo.ReturnTag(_sql, "LastName");
                    var enroll = _oo.ReturnTag(_sql, "StEnRCode");
                    var srNo = _oo.ReturnTag(_sql, "srno");


                    var cmd = new SqlCommand();
                    cmd.CommandText = "SectionTransferDetailsProc";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StEnRCode", enroll);
                    cmd.Parameters.AddWithValue("@SrNo", srNo);
                    cmd.Parameters.AddWithValue("@StudentName", firstName + "" + middleName + "" + lastName);
                    cmd.Parameters.AddWithValue("@FatherName", fatherName);
                    cmd.Parameters.AddWithValue("@Class", Class);
                    cmd.Parameters.AddWithValue("@Section", section);
                    cmd.Parameters.AddWithValue("@AdmissionDate", dateofAdmission);
                    cmd.Parameters.AddWithValue("@Sectiontransfer", DrpSectionTransfer.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Reason", TextBox1.Text);

                    cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
                    cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                    cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());//Session["Login"].ToString());
                    cmd.Parameters.AddWithValue("@ReceiptNo", "1");

                    _sql = " select sm.Id from SectionMaster sm  inner join ClassMaster cm on sm.ClassNameId=cm.Id and sm.BranchCode=cm.BranchCode and sm.SessionName=cm.SessionName  ";
                    _sql = _sql + "  where cm.ClassNameId='" + ClassId + "' and sm.SectionName='" + DrpSectionTransfer.SelectedItem + "' and sm.SessionName='"+ Session["SessionName"] + "'  and sm.BranchCode=" + Session["BranchCode"] + " ";
                    var sectionid = _oo.ReturnTag(_sql, "id");
                    cmd.Parameters.AddWithValue("@SectionId", sectionid);
                    cmd.Connection = _con;

                    try
                    {
                        _con.Open();
                        cmd.ExecuteNonQuery();
                        _con.Close();
                        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Transferred successfully.", "S");
                        section = DrpSectionTransfer.SelectedItem.ToString();
                        string sqls = " if exists(select * from CCENurtoPrep_1718 where BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "') ";
                        sqls = sqls + " begin update CCENurtoPrep_1718 set SectionName='" + section + "' where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "' end ";
                        sqls = sqls + " if exists(select * from CCEItoV where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "') ";
                        sqls = sqls + " begin update CCEItoV set SectionName='" + section + "' where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "' end ";
                        sqls = sqls + " if exists(select * from CCEVItoVIII_1718 where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "') ";
                        sqls = sqls + " begin update CCEVItoVIII_1718 set SectionName='" + section + "' where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "' end ";
                        sqls = sqls + " if exists(select * from CCEIXtoX_1718 where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "') ";
                        sqls = sqls + " begin update CCEIXtoX_1718 set SectionName='" + section + "' where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "' end ";
                        sqls = sqls + " if exists(select * from CCEXI_1718 where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "') ";
                        sqls = sqls + " begin update CCEXI_1718 set SectionName='" + section + "' where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "' end ";
                        sqls = sqls + " if exists(select * from CCEXII_1718 where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "') ";
                        sqls = sqls + " begin update CCEXII_1718 set SectionName='" + section + "' where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and srno='" + studentId + "' end ";
                        sqls = sqls + " if exists(select * from AttendanceDetailsDateWise where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and SrNo='" + studentId + "') ";
                        sqls = sqls + " begin update AttendanceDetailsDateWise set SectionName='" + section + "' where BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and SrNo='" + studentId + "' end ";
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
                    catch (SqlException) { 
                    }


                    _sql = "Select id,SrNo,StEnRCode,Name as StudentName,FatherName,ClassName, CombineClassName,SectionName,Medium,Card,Convert(varchar(11),DateOfAdmiission) as DateOfAdmiission,CourseName,BranchName,FamilyContactNo,PhotoPath,classID";
                    _sql = _sql + " from AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") asr where srno='" + studentId + "' and Withdrwal is null";
                    Grd.DataSource = _oo.GridFill(_sql);
                    Grd.DataBind();
                }
            }
        }
        protected void DrpSectionTransfer_SelectedIndexChanged(object sender, EventArgs e)
        {
            
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
            _sql = _sql + " left join LoginType Lt on LTb.LoginTypeId=Lt.LoginTypeId where LT.LoginTypeName='" + Session["Logintype"] + "' and LTb.LoginName='" + Session["LoginName"] + "' and LTb.BranchId='" + Session["BranchCode"] + "";
            var a = Convert.ToInt32(_oo.ReturnTag(_sql, "add1"));
            // ReSharper disable once RedundantCast
            PermissionGrant(a, (LinkButton)LinkButton4);
        }
        
        

        public override void Dispose()
        {
            _con.Dispose();
            _oo.Dispose();
        }
    }
}