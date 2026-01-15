using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI.WebControls;

namespace _1
{
    public partial class StudentDelete : System.Web.UI.Page
    {
        private SqlConnection _con;
        readonly Campus _oo;
        private string _sql = "";

        public StudentDelete()
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
                try
                {
                    string _sql = "select SrNoType from tblAutometedSRNO where SrNoType='Automatic' and BranchCode=" + Session["BranchCode"] + "";
                    if (_oo.Duplicate(_sql))
                    {
                        searchdiv1.Visible = false;
                        Divmsgbox.Visible = true;
                        msgbox2.InnerText = "S.R. No. can't be delete, as S.R. No. is generating automatically.";
                    }
                    else
                    {
                        searchdiv1.Visible = true;
                        Divmsgbox.Visible = false;
                    }
                    CheckValueAddDeleteUpdate();
                }
                catch (Exception)
                {
                    // ignored
                }

                LinkButton2.Visible = false;
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
            var studentId = Request.Form[hfStudentId.UniqueID];
            if (studentId == string.Empty)
            {
                studentId = TxtEnter.Text.Trim();
            }
            _sql = "Select id,SrNo,StEnRCode,Name as StudentName,FatherName,ClassName,CombineClassName,SectionName,Medium,Card,Convert(varchar(11),DateOfAdmiission) as DateOfAdmiission,CourseName,BranchName,FamilyContactNo,PhotoPath";
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
            LinkButton2.Visible = true;

            if (Grd.Rows.Count == 0)
            {
                //oo.MessageBox("No Records Found", this.Page);
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "No Records Found", "A");

                LinkButton2.Visible = false;
            }

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (Grd.Rows.Count > 0)
            {
                var label1 = (Label)Grd.Rows[0].FindControl("Label1");
                string _sql1 = "Select SrNo from TutionFeeDeposit where SrNo='" + label1.Text+"'";
                string _sql2= "Select SrNo from OtherFeeDeposit where SrNo='" + label1.Text+"'";
                string _sql3= "Select SrNo from UniformFeeDeposit where SrNo='" + label1.Text+"'";
                string _sql4= "Select SrNo from OptionalFeeAllotment where SrNo='" + label1.Text+"'";
                string _sql5= "Select SrNo from TransportFeeDeposit where SrNo='" + label1.Text+"'";
                string _sql6= "Select SrNoOrEmpId from HostelFeeDeposit where SrNoOrEmpId='" + label1.Text+"'";
                string _sql7= "Select SrNo from MiscellaneousFeeDeposit where SrNo='" + label1.Text+"'";
                string _sql8= "Select SrNo from TCCollection where SrNo='" + label1.Text+"'";
                string _sql9= "Select SrNoOrEmpId from HostelFeeAllotmentForCondidate where SrNoOrEmpId='" + label1.Text+"'";
                string _sql10= "Select SrNo from CCCollection where SrNo='" + label1.Text+"'";
                var cmd = new SqlCommand();
                if (_oo.Duplicate(_sql1))
                {
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Can not delete S.R. No. please cancel the receipt(s) from Tuition Fee!", "A");
                }
                else if(_oo.Duplicate(_sql2))
                {
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Can not delete S.R. No. please cancel the receipt(s) from Other Fee!", "A");
                }
                else if (_oo.Duplicate(_sql3))
                {
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Can not delete S.R. No. please cancel the receipt(s) from Product Fee!", "A");
                }
                else if (_oo.Duplicate(_sql4))
                {
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Can not delete S.R. No. please cancel the receipt(s) from Optional Fee allotment!", "A");
                }
                else if (_oo.Duplicate(_sql5))
                {
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Can not delete S.R. No. please cancel the receipt(s) from Transport Fee!", "A");
                }
                else if (_oo.Duplicate(_sql6))
                {
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Can not delete S.R. No. please cancel the receipt(s) from Hostel Fee!", "A");
                }
                else if (_oo.Duplicate(_sql7))
                {
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Can not delete S.R. No. please cancel the receipt(s) from Miscellaneous Fee!", "A");
                }
                else if (_oo.Duplicate(_sql8))
                {
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Can not delete S.R. No. please cancel the receipt(s) from T.C. Fee!", "A");
                }
                else if (_oo.Duplicate(_sql9))
                {
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Can not delete S.R. No. please cancel the receipt(s) from Hostel Fee allotment!", "A");
                }
                else if (_oo.Duplicate(_sql10))
                {
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Can not delete S.R. No. please cancel the receipt(s) from C.C Fee!", "A");
                }
                else
                {
                    cmd.CommandText = "StudentDeleteRecordProc";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Srno", label1.Text);
                    cmd.Parameters.AddWithValue("@StEnRCode", label1.Text);
                    cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"]);
                    cmd.Connection = _con;
                    try
                    {
                        _con.Open();
                        cmd.ExecuteNonQuery();
                        _con.Close();
                        View();
                        //oo.MessageBox("Deleted successfully.", this.Page);
                        Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Deleted successfully.", "S");
                        _oo.ClearControls(Page);
                        LinkButton2.Visible = false;
                        Grd.Visible = false;
                    }
                    catch (SqlException) { }
               
                }
            }
            else
            {
                //oo.MessageBox("No Record found!", this.Page);
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "No Record found!", "A");
            }

        }
        protected void Button8_Click(object sender, EventArgs e)
        {

        }
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            Button8.Focus();

            LinkButton chk = (LinkButton)sender;
            string ss = chk.Text;
            lblvalue.Text = ss;
            Panel1_ModalPopupExtender.Show();
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
            _sql = _sql + " left join LoginType Lt on LTb.LoginTypeId=Lt.LoginTypeId where LT.LoginTypeName='" + Session["Logintype"] + "' and LTb.LoginName='" + Session["LoginName"] + "' and LTb.BranchId=" + Session["BranchCode"] + "";
            int a;
            a = Convert.ToInt32(_oo.ReturnTag(_sql, "delete1"));


            // ReSharper disable once RedundantCast
            PermissionGrant(a, (LinkButton)LinkButton2);
        }

        public override void Dispose()
        {
            _con.Dispose();
            _oo.Dispose();
        }
    }
}