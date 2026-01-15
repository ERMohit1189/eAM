using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _1
{
    public partial class AdminUpdatesrno : Page
    {
        private SqlConnection _con;
        readonly SqlConnection _con1;
#pragma warning disable 169
        private SqlCommand _cmd1;
#pragma warning restore 169
        readonly Campus _oo;
        private string _sql = "";
#pragma warning disable 169
        private DataTable _dt;
#pragma warning restore 169

        public AdminUpdatesrno()
        {
            _con = new SqlConnection();
            _con1 = new SqlConnection();
            _oo = new Campus();
        }
        protected void Page_PreInIt(object sender, EventArgs e)
        {
            if (Session["Logintype"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            if (Session["Logintype"].ToString() == "Administrator")
            {
                MasterPageFile = "~/Administrator/administrato_root-manager.master";
            }
           
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            _con = _oo.dbGet_connection();
            Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file

            if (!IsPostBack)
            {
                

                if (Session["LoginType"].ToString() == "Admin")
                {
                    divBranch.Visible = false;
                    divSession.Visible = false;
                    string _sql = "select SrNoType from tblAutometedSRNO where SrNoType='Automatic' and BranchCode=" + Session["BranchCode"] + "";
                    if (_oo.Duplicate(_sql))
                    {
                        searchdiv1.Visible = false;
                        searchdiv2.Visible = false;
                        msgbox.InnerText = "S.R. No. can't be changed, as S.R. No. is generating automatically.";
                    }
                    else
                    {
                        searchdiv1.Visible = true;
                        searchdiv2.Visible = true;
                    }
                }
                else
                {
                    divBranch.Visible = true;
                    divSession.Visible = true;
                    string sql = "Select BranchId, BranchName from Branchtab";
                    var dt = _oo.Fetchdata(sql);
                    ddlBranch.DataSource = dt;
                    ddlBranch.DataTextField = "BranchName";
                    ddlBranch.DataValueField = "BranchId";
                    ddlBranch.DataBind();
                    ddlBranch.Items.Insert(0, new ListItem("<--Select-->", ""));
                    DrpSessionName.Items.Insert(0, new ListItem("<--Select-->", ""));
                }
            }
        }
        protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["BranchCode"] = "0";
            if (ddlBranch.SelectedIndex == 0)
            {
                DrpSessionName.Items.Clear();
                DrpSessionName.Items.Insert(0, new ListItem("<--Select-->", ""));
                searchdiv1.Visible = false;
                searchdiv2.Visible = false;
            }
            else
            {

                string sql = "select SessionName from SessionMaster where BranchCode=" + ddlBranch.SelectedValue + "";
                var dt2 = _oo.Fetchdata(sql);
                DrpSessionName.DataSource = dt2;
                DrpSessionName.DataTextField = "SessionName";
                DrpSessionName.DataValueField = "SessionName";
                DrpSessionName.DataBind();
                DrpSessionName.Items.Insert(0, new ListItem("<--Select-->", ""));
                Session["BranchCode"] = ddlBranch.SelectedValue;
            }
        }
        protected void DrpSessionName_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["SessionName"] = null;
            searchdiv1.Visible = true;
            searchdiv2.Visible = true;
            if (DrpSessionName.SelectedIndex == 0)
            {
                searchdiv1.Visible = false;
                searchdiv2.Visible = false;
            }
            else
            {
                Session["SessionName"] = DrpSessionName.SelectedValue;
            }
        }

        protected void lnkShow_Click(object sender, EventArgs e)
        {
            Show();
        }

        public void Show()
        {
            string studentId = Request.Form[hfStudentId.UniqueID];
            if (studentId == string.Empty)
            {
                studentId = TxtEnter.Text.Trim();
            }

            string card = "";
            string sql10 = "";
            sql10 = "Select Withdrwal From StudentOfficialDetails where SrNo='" + studentId + "' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            if (_oo.ReturnTag(sql10, "Withdrwal") != "")
            {

                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "This Student is already Withdrawn!", "A");

                Grd.Visible = false;
                GridView1.DataSource = null;
                GridView1.Visible = false;

                newsrdis.Visible = false;
                return;
            }
            else
            {

                _sql = "Select id,SrNo,StEnRCode,CombineClassName,Name as StudentName,FatherName,ClassName,SectionName,Medium,Card,Convert(varchar(11),DateOfAdmiission) as DateOfAdmiission,CourseName,BranchName,FamilyContactNo,PhotoPath";
                _sql = _sql + " from AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") asr where srno='" + studentId + "' and Withdrwal is null";
                Grd.DataSource = _oo.GridFill(_sql);
                Grd.DataBind();
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

                if (Grd.Rows.Count > 0)
                {
                    card = _oo.ReturnTag(_sql, "Card");
                    Session["CardType"] = card;
                    _sql = "select MonthName from MonthMaster where CardType='" + card + "'";
                    _sql = _sql + " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                    _sql = _sql + " or Monthid=0  ";
                    _sql = _sql + " order by MonthId";

                    newsrdis.Visible = true;
                    TextBox1.Text = "";
                    GridView1.DataSource = null;
                    GridView1.Visible = false;
                }
                else
                {
                    newsrdis.Visible = false;
                    TextBox1.Text = "";
                    GridView1.DataSource = null;
                    GridView1.Visible = false;
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Invalid S.R. No.!", "A");
                }

            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            ViewState["CheckRefresh"] = Session["CheckRefresh"];
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            try
            {
                string studentId = Request.Form[hfStudentId.UniqueID];
                if (studentId == string.Empty)
                {
                    studentId = TxtEnter.Text.Trim();
                }

                string msg = "";
                string sql1 = "select * from StudentOfficialDetails  ";
                sql1 = sql1 + " where  srno='" + TextBox1.Text.Trim() + "' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                if (_oo.Duplicate(sql1) == false)
                {
                    if (TextBox1.Text != "")
                    {
                        List<SqlParameter> param = new List<SqlParameter>();
                        param.Add(new SqlParameter("@Old_Srno", studentId.Trim()));
                        param.Add(new SqlParameter("@New_Srno", TextBox1.Text.Trim()));
                        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString().Trim()));


                        SqlParameter para = new SqlParameter("@MSG","");
                        para.Direction = ParameterDirection.Output;
                        para.Size = 0x100;
                        param.Add(para);

                        msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("USP_ChangeSRNO", param);

                        if (msg.Trim().ToUpper() == "S")
                        {
                            Fetchnewsr();
                            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "S.R. No. updated successfully", "S");
                            hfStudentId.Value = "";
                            TextBox1.Text = "";
                        }
                        if (msg.Trim().ToUpper() == "D")
                        {
                            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Sorry, This S.R. No. Already Exist!", "A");
                        }
                    }
                    else
                    {
                        Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Please fill S.R. No.!", "A");
                    }
                }
                else
                {
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Sorry, This S.R. No. Already Exist!", "A");

                }
            }

            catch
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "S.R. No. not updated!", "A");
            }
        }

        public void Fetchnewsr()
        {
       
            
                string card = "";
                _sql = "select SG.Id,bm.BranchName, SC.SectionName,SO.Card,SO.Medium as Medium,CM.ClassName,convert(nvarchar,So.DateOfAdmiission,106) as DateOfAdmiission ,SO.SectionId,Sf.FatherName,SF.MotherName,SG.FirstName,SG.MiddleName,SG.LastName,sg.StEnRCode as StEnRCode,sg.srno  as srno,case  when so.TransportRequired='Yes' then 'Yes' else 'No' end as TransportRequired,so.wayamount as wayamount, SF.FamilyContactNo from StudentGenaralDetail SG ";
                _sql = _sql + "  left join StudentFamilyDetails SF on SG.StEnRCode=SF.StEnRCode";
                _sql = _sql + "  left join StudentOfficialDetails SO on SG.StEnRCode=SO.StEnRCode";
                _sql = _sql + "  left join ClassMaster CM on SO.AdmissionForClassId=CM.Id";
                _sql = _sql + "  left join SectionMaster SC on SO.SectionId=SC.Id left join BranchMaster bm on SO.Branch=bm.id and bm.IsDisplay=1 ";
                _sql = _sql + "  where  SG.srno='" + TextBox1.Text.Trim() + "'";
                _sql = _sql + "  and sg.SessionName='" + Session["SessionName"] + "' and ";
                _sql = _sql + "  so.SessionName='" + Session["SessionName"] + "' and sf.SessionName='" + Session["SessionName"] + "' and cm.SessionName='" + Session["SessionName"] + "'";
                _sql = _sql + "  and SC.SessionName='" + Session["SessionName"] + "' and ";
                _sql = _sql + "  sg.BranchCode=" + Session["BranchCode"]+ " and cm.BranchCode=" + Session["BranchCode"] + " and sf.BranchCode=" + Session["BranchCode"] + " and sc.BranchCode=" + Session["BranchCode"] + " and so.BranchCode=" + Session["BranchCode"] + "";
                _sql = _sql + "  and SO.Withdrwal is null";

                GridView1.DataSource = _oo.GridFill(_sql);
                GridView1.DataBind();
                GridView1.Visible = true;

                card = _oo.ReturnTag(_sql, "Card");
                Session["CardType"] = card;
                _sql = "select MonthName from MonthMaster where CardType='" + card + "'";
                _sql = _sql + " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                _sql = _sql + " or Monthid=0  ";
                _sql = _sql + " order by MonthId";

            
        }

        protected void TxtEnter_TextChanged(object sender, EventArgs e)
        {
            Show();
        }

        public override void Dispose()
        {
            _con.Dispose();
            _con1.Dispose();
            _oo.Dispose();
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            TextBox1.Text = TextBox1.Text.Replace(" ", "");
        }
    }
}