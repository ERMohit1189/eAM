using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace _1
{
    public partial class AdminAddSibling : System.Web.UI.Page
    {
        private SqlConnection _con;
        private SqlCommand _cmd ;
        readonly Campus _oo;
        private string _sql = "";
        static string _sessionName = "", _branchCode="";

        public AdminAddSibling()
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
            Campus camp = new Campus(); camp.LoadLoader(loader);
            if (!IsPostBack)
            {
                // ReSharper disable once PossibleNullReferenceException
                _sessionName = Session["SessionName"].ToString();
                _branchCode = Session["BranchCode"].ToString();
            }
        }
        public void LoadStudentGrid()
        {
            lnkSubmit.Visible = false;
            hfSiblingId.Value = "";
            txtSiblingEnter.Text = "";
            divSecond.Visible = false;
            grdshow.Visible = false;
            Grd1.DataSource = null;
            Grd1.DataBind();
            var studentId = Request.Form[hfStudentId.UniqueID];
            if (studentId == string.Empty)
            {
                studentId = txtStudentEnter.Text.Trim();
            }
            
            _sql = "Select id,SrNo,StEnRCode,Name as StudentName,FatherName,ClassName,CombineClassName,SectionName,Medium,Card,Convert(varchar(11),DateOfAdmiission) as DateOfAdmiission,CourseName,BranchName,FamilyContactNo,PhotoPath";
            _sql = _sql + " from AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") asr where srno='" + studentId + "'";
            Grd.DataSource = _oo.GridFill(_sql);
            Grd.DataBind();

            if (_oo.ReturnTag(_sql, "Gender").ToUpper() == "MALE")
            {
                txtStudentRelation.Text = "BROTHER";
            }
            else if (_oo.ReturnTag(_sql, "Gender").ToUpper() == "FEMALE")
            {
                txtStudentRelation.Text = "SISTER";
            }
            DataSet ds;
            ds = _oo.GridFill(_sql);

            // ReSharper disable once UseNullPropagation
            if (ds != null && Grd.Rows.Count > 0)
            {
                divSecond.Visible = true;
                grdshow.Visible = true;
                grdshow1.Visible = false;
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
            if (Grd.Rows.Count == 0)
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Invalid S.R. No.!", "A");

            }
        }
        public void LoadSiblingGrid()
        {
            var siblingId = Request.Form[hfSiblingId.UniqueID];
            if (siblingId == string.Empty)
            {
                siblingId = txtSiblingEnter.Text.Trim();
            }
            
            _sql = "Select id,SrNo,StEnRCode,Name as StudentName,FatherName,CombineClassName,ClassName,SectionName,Medium,Card,Convert(varchar(11),DateOfAdmiission) as DateOfAdmiission,CourseName,BranchName,FamilyContactNo,PhotoPath";
            _sql = _sql + " from AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") asr where srno='" + siblingId + "'";
            Grd1.DataSource = _oo.GridFill(_sql);
            Grd1.DataBind();

            if (_oo.ReturnTag(_sql, "Gender").ToUpper() == "MALE")
            {
                txtSiblingRelation.Text = "BROTHER";
            }
            else if (_oo.ReturnTag(_sql, "Gender").ToUpper() == "FEMALE")
            {
                txtSiblingRelation.Text = "SISTER";
            }
            DataSet ds;
            ds = _oo.GridFill(_sql);

            // ReSharper disable once UseNullPropagation
            if (ds != null && Grd1.Rows.Count > 0)
            {
                lnkSubmit.Visible = true;
                grdshow1.Visible = true;
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
                            cmd.Parameters.AddWithValue("@SrNo", siblingId.ToString().Trim());
                            cmd.Parameters.AddWithValue("@action", "details");
                            SqlDataAdapter das = new SqlDataAdapter(cmd);
                            DataSet dsPhoto = new DataSet();
                            das.Fill(dsPhoto);
                            cmd.Parameters.Clear();

                            if (dsPhoto.Tables[0].Rows.Count > 0)
                            {
                                Image1.ImageUrl = dsPhoto.Tables[0].Rows[0]["PhotoPath"].ToString() != string.Empty ? dsPhoto.Tables[0].Rows[0]["PhotoPath"] + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture) : "../img/user-pic/user-pic.jpg" + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture);
                                HyperLink1.NavigateUrl = dsPhoto.Tables[0].Rows[0]["PhotoPath"].ToString() != string.Empty ? dsPhoto.Tables[0].Rows[0]["PhotoPath"] + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture) : "../img/user-pic/user-pic.jpg" + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture);
                                HyperLink2.NavigateUrl = "../11/StudentRegView.aspx?print=1&id=" + ds.Tables[0].Rows[0]["stenrcode"];
                            }
                        }
                    }
                }
            }
            if (Grd1.Rows.Count == 0)
            {
                //oo.MessageBoxforUpdatePanel("Invalid S.R. No.!", lnkShow1);
                Campus camp = new Campus(); camp.msgbox(Page, msgbox1, "Invalid S.R. No.!", "A");
            }
        }
        protected void lnkShow_Click(object sender, EventArgs e)
        {
            LoadStudentGrid();
            Displayrecord();
        }
        protected void lnkShow1_Click(object sender, EventArgs e)
        {
            LoadSiblingGrid();
        }
        protected void lnkSubmit_Click(object sender, EventArgs e)
        {
            string studentId = Request.Form[hfStudentId.UniqueID];
            if (studentId == string.Empty)
            {
                studentId = txtStudentEnter.Text.Trim();
            }
            string siblingId = Request.Form[hfSiblingId.UniqueID];
            if (siblingId == string.Empty)
            {
                siblingId = txtSiblingEnter.Text.Trim();
            }


            if (studentId != "" && siblingId != "")
            {
                if (Grd.Rows.Count > 0 && Grd1.Rows.Count > 0)
                {
                    var studentSrno = (Label)Grd.Rows[0].FindControl("Lblsrno");
                    var studentStenrcode = (Label)Grd.Rows[0].FindControl("Label18");
                    var siblingSrno = (Label)Grd1.Rows[0].FindControl("Lblsrnosibling");
                    var siblingStenrcode = (Label)Grd1.Rows[0].FindControl("Label18");
                    
                    if (studentSrno.Text != siblingSrno.Text)
                    {
                        bool flag;
                        var groupId = Groupid(studentId, siblingId);
                        _sql = "select *from SiblingRecord where srno='" + studentId + "' and BranchCode=" + Session["BranchCode"] + " and SessionName='"+Session["SessionName"] +"'";
                        if (_oo.Duplicate(_sql) == false)
                        {
                            Save(studentSrno.Text, studentStenrcode.Text, txtStudentRelation.Text, groupId);
                            // ReSharper disable once RedundantAssignment
                            flag = false;
                        }
                        else
                        {
                            // ReSharper disable once RedundantAssignment
                            flag = true;
                        }
                        _sql = "select * from SiblingRecord where srno='" + siblingId + "' and BranchCode="+Session["BranchCode"]+ " and SessionName='" + Session["SessionName"] + "'";
                        if (_oo.Duplicate(_sql) == false)
                        {
                            Save(siblingSrno.Text, siblingStenrcode.Text, txtSiblingRelation.Text, groupId);
                            flag = false;
                        }
                        else
                        {
                            flag = true;
                        }
                        // ReSharper disable once RedundantBoolCompare
                        if (flag == true)
                        {
                            Campus camp = new Campus(); camp.msgbox(Page, msgbox2, "Record already present!", "A");
                        }
                        Displayrecord(); 

                    }
                    else
                    {
                        Campus camp = new Campus(); camp.msgbox(Page, msgbox2, "Both S.R. No. are same!", "A");
                    }

                }
                else
                {
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox2, "Please, Enter right Student or Sibling Details!", "A");
                }
            }
            else
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox2, "Please, Enter S.R. No. or Stenrcode!", "A");
            }
        }
        public void Save(string studentSrno, string studentStenrcode, string relation, string groupId)
        {
            _cmd = new SqlCommand
            {
                CommandText = "SiblingRecordProc",
                CommandType = CommandType.StoredProcedure,
                Connection = _con
            };
            _cmd.Parameters.AddWithValue("@Srno", studentSrno);
            _cmd.Parameters.AddWithValue("@StEnRCode", studentStenrcode);
            _cmd.Parameters.AddWithValue("@SiblingRelation", relation);
            _cmd.Parameters.AddWithValue("@GroupId", groupId);
            _cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
            _cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            _cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
            _con.Open();
            _cmd.ExecuteNonQuery();
            _con.Close();
            hfSiblingId.Value = "";
            txtSiblingEnter.Text = "";
            lnkSubmit.Visible = false;
            Grd1.DataSource = null;
            Grd1.DataBind();
            Displayrecord();
            //oo.MessageBoxforUpdatePanel("Submitted successfully", lnkSubmit);
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Submitted successfully", "S");
        }
        public string Groupid(string id, string id1)
        {
            var value = "000001";
            _sql = "Select GroupId from SiblingRecord where srno='" + id + "' or srno='" + id1 + "' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "'";
            if (_oo.Duplicate(_sql))
            {
                value = _oo.ReturnTag(_sql, "GroupId");
            }
            else
            {
                _sql = "Select Max(GroupId) as GroupId from SiblingRecord";
                int max;
                if (_oo.ReturnTag(_sql, "GroupId") == "")
                {
                    max = 0;
                }
                else
                {
                    max = Convert.ToInt32(_oo.ReturnTag(_sql, "GroupId"));
                }
                if (max == 0)
                {
                    value = (max + 1).ToString();
                }
                else if (max < 9)
                {
                    value = (max + 1).ToString();
                }
                else if (max < 99)
                {
                    value = (max + 1).ToString();
                }
                else if (max < 9999)
                {
                    value = (max + 1).ToString();
                }
                else if (max < 99999)
                {
                    value = (max + 1).ToString();
                }
                else if (max < 999999)
                {
                    value = (max + 1).ToString();
                }
            }
            return value;
        }
        public void Displayrecord()
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            var studentId = Request.Form[hfStudentId.UniqueID];
            if (studentId == string.Empty)
            {
                studentId = txtStudentEnter.Text.Trim();
            }



            _sql = "Select GroupId from SiblingRecord where srno='" + studentId + "' and SessionName='" + Session["SessionName"] + "'  and BranchCode=" + Session["BranchCode"] + "";
            var groupId = _oo.ReturnTag(_sql, "GroupId");
            _sql = "select distinct sr.Id, asr.SectionName, asr.Card,asr.Medium as Medium,asr.ClassName,convert(nvarchar,asr.DateOfAdmiission,106) as DateOfAdmiission ,";
            _sql = _sql + "   asr.SectionId,asr.FatherName,asr.MotherName,asr.FirstName,asr.MiddleName,asr.LastName,asr.StEnRCode as StEnRCode,asr.srno  as srno,case ";
            _sql = _sql + "   when asr.TransportRequired='Yes' then 'Yes' else 'No' end as TransportRequired,sr.SiblingRelation, CombineClassName";
            _sql = _sql + "   from AllStudentRecord_UDF('" + Session["SessionName"] + "',"+ Session["BranchCode"] + ") asr inner join SiblingRecord sr on sr.Srno = asr.SrNo  and sr.SessionName = asr.SessionName  and sr.BranchCode = asr.BranchCode";
            _sql = _sql + "   where  sr.GroupId='" + groupId + "'";
            _sql = _sql + "   except select sr.Id,asr.SectionName,asr.Card,asr.Medium as Medium,asr.ClassName,convert(nvarchar,asr.DateOfAdmiission,106) as DateOfAdmiission ,asr.SectionId,asr.FatherName, ";
            _sql = _sql + "   asr.MotherName,asr.FirstName,asr.MiddleName,asr.LastName,asr.StEnRCode as StEnRCode,asr.srno  as srno,case  when asr.TransportRequired='Yes' then 'Yes' else 'No' end";
            _sql = _sql + "   as TransportRequired,sr.SiblingRelation, CombineClassName";
            _sql = _sql + "   from AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") asr inner join SiblingRecord sr on sr.Srno = asr.SrNo  and sr.SessionName = asr.SessionName  and sr.BranchCode = asr.BranchCode";
            _sql = _sql + "   where  sr.srno='" + studentId + "'";
            GridView1.DataSource = _oo.GridFill(_sql);
            GridView1.DataBind();
            if (GridView1.Rows.Count > 0)
            {
                divList.Visible = true;
            }
            else
            {
                divList.Visible = false;
            }
        
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Button8.Focus();
            LinkButton chk = (LinkButton)sender;
            Label lblId = (Label)chk.NamingContainer.FindControl("LabelIddelete");
            //string ss = lblId.Text;
            //LinkButton lnk = (LinkButton)sender;
            lblvalue.Text = lblId.Text;
            Panel1_ModalPopupExtender.Show();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            //sql = "Delete From SiblingRecord where id='" + lblvalue.Text.Trim() + "'";
            //cmd = new SqlCommand(sql, con);
            //con.Open();
            //cmd.ExecuteNonQuery();
            //displayrecord();
            ////oo.MessageBoxforUpdatePanel("Deleted successfully", btnDelete);
            //Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Deleted successfully", "S");
            //con.Close();

            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@Id", lblvalue.Text.Trim()));
            param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
            param.Add(new SqlParameter("@LoginName", Session["LoginName"].ToString()));

            SqlParameter para = new SqlParameter("@msg", "");
            para.Direction = ParameterDirection.Output;
            param.Add(para);

            string msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("USP_SiblingRecordUpdate", param);
            if(msg=="S")
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Deleted successfully", "S");
            }
            else
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, msg, "S");
            }
            Displayrecord();
        }
        
        [WebMethod]
        public static string[] GetStudents(string prefix)
        {
            List<string> customers = new List<string>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = BAL.objBal.dbGet_connection().ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select Name+' '+SrNo+' '+Replace(ClassName,'-',' ')+' '+SectionName StudentName, CASE WHEN SrNo='' Then StEnRCode Else SrNo End StudentId from AllStudentRecord_UDF('" + _sessionName + "',"+ _branchCode + ") where (Name like @SearchText + '%' or Srno like @SearchText + '%' or StEnRCode like @SearchText + '%') and Withdrwal is null";
                    cmd.Parameters.AddWithValue("@SearchText", prefix);
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            customers.Add(string.Format("{0}-{1}", sdr["StudentName"], sdr["StudentId"]));
                        }
                    }
                    conn.Close();
                }
            }
            return customers.ToArray();
        }


        protected void txtStudentEnter_TextChanged(object sender, EventArgs e)
        {
            LoadStudentGrid();
            Displayrecord();
        }

        protected void txtSiblingEnter_TextChanged(object sender, EventArgs e)
        {
            LoadSiblingGrid();
        }

        public override void Dispose()
        {
            _con.Dispose();
            _oo.Dispose();
        }
    }
}

