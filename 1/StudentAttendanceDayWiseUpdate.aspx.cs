using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _1
{
    public partial class StudentAttendanceDayWiseUpdate : Page
    {
        private SqlConnection _con;
        private readonly Campus _oo;
        private string _sql = "";
        public StudentAttendanceDayWiseUpdate()
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
            Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file

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
                _oo.AddDateMonthYearDropDown(DrpSaal, DrpMahina, DrpDin);
                _oo.FindCurrentDateandSetinDropDown(DrpSaal, DrpMahina, DrpDin);
                _sql = "Select id, ClassName from ClassMaster";
                _sql += " where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                _sql += " order by CIDOrder";
                _oo.FillDropDown_withValue(_sql, DrpAtteClass, "ClassName", "id");
                _sql = "select Id from ClassMaster where  ClassName='" + DrpAtteClass.SelectedItem + "'";
                _sql += " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                var cCodes = _oo.ReturnTag(_sql, "id");
                _sql = "Select SectionName from SectionMaster where ClassNameId=" + cCodes;
                _sql += " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                _oo.FillDropDown(_sql, DrpAttenSection, "SectionName");
                Panel1.Visible = false;
                btnSubmit.Visible = false;
                Panel2.Visible = false;
                Grd.Visible = false;
                loadBranch(drpBranch, DrpAtteClass);
            }
        }
        protected void DrpAtteClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            _sql = "Select SectionName from SectionMaster where ClassNameId=" + DrpAtteClass.SelectedValue.ToString() + "";
            _sql += " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            _oo.FillDropDown(_sql, DrpAttenSection, "SectionName");
            cc.Text = "";
            loadBranch(drpBranch, DrpAtteClass);
            Panel1.Visible = false;
            Panel2.Visible = false;
            Grd.Visible = false;
            btnSubmit.Visible = false;
        }
        private void loadBranch(DropDownList drpbranch, DropDownList drpclass)
        {
            if (Session["Logintype"].ToString() == "Admin")
            {
                _sql = "Select BranchName,Id from BranchMaster ";
                _sql += "   where BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and Classid=" + drpclass.SelectedValue.ToString() + "";
                BAL.objBal.FillDropDown_withValue(_sql, drpbranch, "BranchName", "Id");
                drpbranch.Items.Insert(0, new ListItem("<--Select-->", "0"));

            }
            else
            {
                _sql = "Select BranchName,bm.Id from ClassTeacherMaster T1";
                _sql += "   inner join BranchMaster bm on bm.Id=T1.BranchId and bm.SessionName=t1.SessionName";
                _sql += "   where EmpCode='" + Session["LoginName"].ToString() + "' and IsClassTeacher=1 ";
                _sql += "    and bm.BranchCode=" + Session["BranchCode"] + " and T1.BranchCode=" + Session["BranchCode"] + " and T1.SessionName='" + Session["SessionName"] + "' and T1.Classid='" + drpclass.SelectedValue.ToString() + "'";
                BAL.objBal.FillDropDown_withValue(_sql, drpbranch, "BranchName", "Id");
                drpbranch.Items.Insert(0, new ListItem("<--Select-->", "0"));
            }
        }
        protected void DrpSaal_SelectedIndexChanged(object sender, EventArgs e)
        {
            _oo.YearDropDown(DrpSaal, DrpMahina, DrpDin);
        }
        protected void DrpMahina_SelectedIndexChanged(object sender, EventArgs e)
        {
            _oo.MonthDropDown(DrpSaal, DrpMahina, DrpDin);
        }
        protected void btnShow_Click(object sender, EventArgs e)
        {
            cc.Text = "";
            ReadDailyGrid();
        }

        public void ReadDailyGrid()
        {
            Campus oo = new Campus();
            // ReSharper disable once UnusedVariable
            string date = DrpDin.SelectedItem + "/" + DrpMahina.SelectedItem + "/" + DrpSaal.SelectedItem;
            string dd;
            if (DrpDin.SelectedItem.ToString().Length == 1)
            {
                dd = "0" + DrpDin.SelectedItem;
            }
            else
            {
                dd = DrpDin.SelectedItem.ToString();
            }
            _sql = ";with aa as(select distinct  ad.Id,ad.CategoryWise ,ad.AttendanceMonth ,ad.AttendanceDate ,ad.ClassName,ad.SectionName,SG.StEnRCode ,ad.SrNo ,SG.name, SG.FatherName, SG.FatherContactNo  , ";
            _sql += "  ad.StudentName , ad.AttendanceValue , ad.BranchCode,ad.LoginName,ad.SessionName,ad.RecordDate,SG.DateOfAdmiission from AttendanceDetailsDateWise   ad";
            _sql += "  inner join AllStudentRecord_UDF('" + Session["SessionName"] + "', " + Session["BranchCode"] + ") SG   on ad.SrNo=SG.SrNo and ad.SessionName=SG.SessionName and ad.BranchCode=SG.BranchCode and ad.SectionName=SG.SectionName ";
            _sql += "    where ltrim(rtrim(ad.ClassName))='" + DrpAtteClass.SelectedItem.Text.Trim() + "' and  ad.SectionName='" + DrpAttenSection.SelectedItem + "'  and convert(nvarchar,ad.AttendanceDate,106)='" + dd + " " + DrpMahina.SelectedItem + " " + DrpSaal.SelectedItem + "'";
            _sql += "  and ad.CategoryWise='Date Wise'";
            _sql += " and ad.SessionName='" + Session["SessionName"] + "' and ad.BranchCode=" + Session["BranchCode"] + "";
            //if (drpBranch.SelectedIndex!=0)
            //{
            //    _sql +=  "  and SG.BranchId="+ drpBranch.SelectedValue + " ";
            //}

            if (drpBranch.SelectedIndex != 0)
            {
                _sql += "  and SG.BranchId=" + drpBranch.SelectedValue + " ) Select * from aa";
            }
            else
            {
                _sql += " ) Select * from aa";
            }
            if (RadioButtonList2.SelectedValue == "A")
            {
                _sql += "  order by aa.Name asc ";
            }
            if (RadioButtonList2.SelectedValue == "Rn")
            {
                _sql += "  order by aa.InstituteRollNo asc";
            }
            if (RadioButtonList2.SelectedValue == "S")
            {
                _sql += "  order by aa.id asc ";
            }
            if (RadioButtonList2.SelectedValue == "doa")
            {
                _sql += "  order by CONVERT(DATETIME, aa.DateOfAdmiission, 113)  asc ";
            }
            Grd.DataSource = oo.GridFill(_sql);
            Grd.DataBind();
            btnSubmit.Visible = true;
            Grd.Visible = true;
            Panel1.Visible = true;
            Panel2.Visible = true;
            if (Grd.Rows.Count == 0)
            {
                // btnSubmit.Visible = false;
                //oo.MessageBox("Attendance not called for this date!", this.Page);
                Campus camp = new Campus();
                camp.msgbox(Page, msgbox, "Attendance not called for this date!", "A");
                btnSubmit.Visible = false;
                Grd.Visible = false;
                Panel1.Visible = false;
                Panel2.Visible = false;

            }
            else
            {
                //Panel1.Visible = true;
                for (int a = 0; a < Grd.Rows.Count; a++)//State Wise
                {
                    DropDownList Drp = (DropDownList)Grd.Rows[a].Cells[3].FindControl("DropDownList1");
                    Grd.Rows[a].CssClass = "vd_bg-green form-control-blue vd_white";
                    Drp.CssClass = "vd_bg-green form-control-blue vd_white";
                    Label lEnroll = (Label)Grd.Rows[a].Cells[0].FindControl("lblStEnRCode");
                    _sql = "select AbbreviationName from  AttendanceAbbreviationMaster where ValidFor='S'";
                    oo.FillDropDownWithOutSelect(_sql, Drp, "AbbreviationName");
                    _sql = "select AttendanceValue from AttendanceDetailsDateWise where StEnRCode='" + lEnroll.Text + "'";
                    _sql += "    and ltrim(rtrim(ClassName))='" + DrpAtteClass.SelectedItem.Text.Trim() + "' and SectionName='" + DrpAttenSection.SelectedItem + "'  and convert(nvarchar,AttendanceDate,106)='" + dd + " " + DrpMahina.SelectedItem + " " + DrpSaal.SelectedItem + "'";
                    _sql += "  and CategoryWise='Date Wise'";
                    _sql += " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";

                    string ss = oo.ReturnTag(_sql, "AttendanceValue");
                    Drp.Text = ss.Trim();
                    Grd.Rows[a].CssClass = "vd_bg-green form-control-blue vd_white";
                    Drp.CssClass = "vd_bg-green form-control-blue vd_white";
                    if (Drp.SelectedItem.Text.ToUpper() == "A")
                    {
                        Grd.Rows[a].CssClass = "vd_bg-red form-control-blue vd_white";
                        Drp.CssClass = "vd_bg-red form-control-blue vd_white";
                    }
                    if (Drp.SelectedItem.Text.ToUpper() == "L")
                    {
                        Grd.Rows[a].CssClass = "vd_bg-yellow form-control-blue vd_white";
                        Drp.CssClass = "vd_bg-yellow form-control-blue vd_white";
                    }
                    else if (Drp.SelectedItem.Text.ToUpper() == "L")
                    {
                        Grd.Rows[a].CssClass = "vd_bg-yellow form-control-blue vd_white";
                        Drp.CssClass = "vd_bg-yellow form-control-blue vd_white";
                    }
                    else if (Drp.SelectedItem.Text.ToUpper() == "LT" || Drp.SelectedItem.Text.ToUpper() == "LC")
                    {
                        Grd.Rows[a].CssClass = "vd_bg-blue form-control-blue vd_white";
                        Drp.CssClass = "vd_bg-blue form-control-blue vd_white";
                    }
                }
            }
        }
        public void DailyAttendanceRadio()
        {
            bool flag = false;
            for (int a = 0; a < Grd.Rows.Count; a++)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "AttendanceUpdateDailyProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = _con;
                Label lEnroll = (Label)Grd.Rows[a].Cells[0].FindControl("Label10");
                Label lsrno = (Label)Grd.Rows[a].Cells[1].FindControl("lblsrno");
                Label lblStEnRCode = (Label)Grd.Rows[a].Cells[1].FindControl("lblStEnRCode");
                Label name = (Label)Grd.Rows[a].Cells[2].FindControl("lblname");
                DropDownList drp1 = (DropDownList)Grd.Rows[a].Cells[3].FindControl("DropDownList1");
                cmd.Parameters.AddWithValue("@ClassName", DrpAtteClass.SelectedItem.Text.Trim());
                cmd.Parameters.AddWithValue("@SectionName", DrpAttenSection.Text.Trim());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
                cmd.Parameters.AddWithValue("@SrNo", lsrno.Text.Trim());
                cmd.Parameters.AddWithValue("@StEnRCode", lblStEnRCode.Text.Trim());
                var date = DrpSaal.SelectedItem + "/" + DrpMahina.SelectedItem + "/" + DrpDin.SelectedItem;
                cmd.Parameters.AddWithValue("@AttendanceDate", date.Trim());

                cmd.Parameters.AddWithValue("@Value", drp1.SelectedItem.Text.Trim().ToString());
                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());

                try
                {
                    _con.Open();
                    cmd.ExecuteNonQuery();
                    _con.Close();
                    flag = true;
                    _oo.ClearControls(Page);
                }
                catch (Exception ex)
                {
                    // ignored
                }
            }
            if (flag)
            {
                _oo.msgbox(this.Page, msgbox, "Updated successfully.", "U");
                //cc.Text = "Updated successfully.";
                //Panel3_ModalPopupExtender.Show();

            }
        }
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow currentrow = (GridViewRow)(((Control)(sender)).Parent).Parent;
            DropDownList dropDownList1 = (DropDownList)currentrow.FindControl("DropDownList1");
            if (dropDownList1.SelectedItem.Text.ToUpper() == "A")
            {
                currentrow.CssClass = "vd_bg-red form-control-blue vd_white";
                dropDownList1.CssClass = "vd_bg-red form-control-blue vd_white";
            }
            else if (dropDownList1.SelectedItem.Text.ToUpper() == "L")
            {
                currentrow.CssClass = "vd_bg-yellow form-control-blue vd_white";
                dropDownList1.CssClass = "vd_bg-yellow form-control-blue vd_white";
            }
            else if (dropDownList1.SelectedItem.Text.ToUpper() == "LT" || dropDownList1.SelectedItem.Text.ToUpper() == "LC")
            {
                currentrow.CssClass = "vd_bg-blue form-control-blue vd_white";
                dropDownList1.CssClass = "vd_bg-blue form-control-blue vd_white";
            }
            else if (dropDownList1.SelectedItem.Text.ToUpper() == "NM" || dropDownList1.SelectedItem.Text.ToUpper() == "NAD")
            {
                currentrow.CssClass = "vd_bg-white form-control-blue";
                dropDownList1.CssClass = "vd_bg-white form-control-blue";
            }
            else
            {
                currentrow.CssClass = "vd_bg-green form-control-blue vd_white";
                dropDownList1.CssClass = "vd_bg-green form-control-blue vd_white";
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            DailyAttendanceRadio();
        }
        protected void DrpDin_SelectedIndexChanged(object sender, EventArgs e)
        {
            cc.Text = "";
            Panel1.Visible = false;
            Panel2.Visible = false;
            Grd.Visible = false;
            btnSubmit.Visible = false;
        }
        protected void Grd_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void DrpAttenSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            cc.Text = "";
            Panel1.Visible = false;
            Panel2.Visible = false;
            Grd.Visible = false;
            btnSubmit.Visible = false;
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
            _sql += " left join LoginType Lt on LTb.LoginTypeId=Lt.LoginTypeId where LT.LoginTypeName='" + Session["Logintype"] + "' and LTb.LoginName='" + Session["LoginName"] + "'";
            int a = Convert.ToInt32(_oo.ReturnTag(_sql, "add1"));


            // ReSharper disable once RedundantCast
            PermissionGrant(a, (LinkButton)btnSubmit);
        }







        protected void DrpAtteClass_SelectedIndexChanged1(object sender, EventArgs e)
        {
            string cCode;
            _sql = "select Id from ClassMaster where  ClassName='" + DrpAtteClass.SelectedItem.Text + "'";
            _sql += " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            cCode = _oo.ReturnTag(_sql, "id");
            _sql = "Select SectionName from SectionMaster where ClassNameId=" + cCode;
            _sql += " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";

            _oo.FillDropDown(_sql, DrpAttenSection, "SectionName");

        }

        public override void Dispose()
        {
            _con.Dispose();
            _oo.Dispose();
        }
    }
}