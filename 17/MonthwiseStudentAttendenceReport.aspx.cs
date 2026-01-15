using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI.WebControls;

namespace _11
{
    public partial class _11_MonthwiseStudentAttendenceReport : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection( );
        private readonly Campus oo = new Campus();
        string _sql, _smsResponse = string.Empty;

        protected void Page_PreInIt(object sender, EventArgs e)
        {
            if (Session["Logintype"] == null) { Response.Redirect("~/default.aspx"); }
            // ReSharper disable once PossibleNullReferenceException
            if (Session["Logintype"].ToString() == "Admin")
            {
                MasterPageFile = "~/Master/admin_root-manager.master";
            }
            else if (Session["Logintype"].ToString() == "Staff")
            {
                MasterPageFile = "~/Staff/staff_root-manager.master";
            }
            else if (Session["Logintype"].ToString() == "Guardian")
            {
                MasterPageFile = "~/sp/sp_root-manager.master";
            }
            else if (Session["Logintype"].ToString() == "Student")
            {
                this.MasterPageFile = "~/13/stuRootManagerDashboard.master";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["logintype"].ToString() == "Admin")
                {
                    divEnter2.Visible = true;
                    divEnter1.Visible = false;
                    btnshow.Visible = true;
                }
                else
                {
                    divEnter2.Visible = false;
                    divEnter1.Visible = true;
                    btnshow.Visible = false;
                    LoadClassSrno();
                }
            }
        }
        private void LoadClassSrno()
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
            param.Add(new SqlParameter("@EmpCode", Session["LoginName"].ToString()));
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));

            drpSrno.DataSource = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("GetClassTeacherClassNameStudent_Proc", param);
            drpSrno.DataTextField = "name";
            drpSrno.DataValueField = "srno";
            drpSrno.DataBind();
            BAL.objBal.fillSelectvalue(drpSrno, "<--Select-->", "<--Select-->");
            drpSrno.SelectedIndex = 0;
        }
        protected void drpSrno_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var studentId = Request.Form[hfStudentId.UniqueID];
            if (studentId != null)
            {
                if (string.IsNullOrEmpty(studentId))
                {
                    studentId = drpSrno.SelectedValue;
                }
            }
            else
            {
                studentId = drpSrno.SelectedValue;
            }
            ViewState["srno"] = studentId;
            Fillmonth();
            Loadgrid(studentId);
            CountTotalPresentAbsent();
            GetStudentDetials(studentId);
        }
        protected void TxtEnter_OnTextChanged(object sender, EventArgs e)
        {
            var studentId = Request.Form[hfStudentId.UniqueID];
            if (studentId != null)
            {
                if (string.IsNullOrEmpty(studentId))
                {
                    studentId = TxtEnter.Text.Trim();
                }
            }
            else
            {
                studentId = TxtEnter.Text.Trim();
            }
            ViewState["srno"] = studentId;
            Fillmonth();
            Loadgrid(studentId);
            CountTotalPresentAbsent();
            GetStudentDetials(studentId);
        }

        protected void LinkButton1_OnClick(object sender, EventArgs e)
        {
            var studentId = Request.Form[hfStudentId.UniqueID];
            if (studentId != null)
            {
                if (string.IsNullOrEmpty(studentId))
                {
                    studentId = TxtEnter.Text.Trim();
                }
            }
            else
            {
                studentId = TxtEnter.Text.Trim();
            }
            ViewState["srno"] = studentId;
            Fillmonth();
            Loadgrid(studentId);
            CountTotalPresentAbsent();
            GetStudentDetials(studentId);
        }

        private void GetStudentDetials(string studentId)
        {
            try
            {
                _sql = "Select *from StudentOfficialDetails where blocked='Yes' and srno='" + studentId + "' and BranchCode=" + Session["BranchCode"] + "";
                // ReSharper disable once UnusedVariable
                var sql1 = "Select Promotion,MODForFeeDeposit from StudentOfficialDetails where srno='" + studentId + "' and BranchCode="+ Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "'";
                //isAnualDeposit = BAL.objBal.ReturnTag(sql1, "MODForFeeDeposit") == "A" ? true : false;
                var ds = BLL.BLLInstance.GetStudentDetails(studentId, Session["SessionName"].ToString(), Session["BranchCode"].ToString());

                grdStRecord.DataSource = ds;
                grdStRecord.DataBind();
                // ReSharper disable once UseNullPropagation
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        img.ImageUrl = ds.Tables[0].Rows[0]["PhotoPath"].ToString() != string.Empty ? ds.Tables[0].Rows[0]["PhotoPath"] + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture) : "../img/user-pic/user-pic.jpg" + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture);
                        studentImg.NavigateUrl = ds.Tables[0].Rows[0]["PhotoPath"].ToString() != string.Empty ? ds.Tables[0].Rows[0]["PhotoPath"] + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture) : "../img/user-pic/user-pic.jpg" + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture);
                    }
                    divshowst.Visible = true;
                }
                else
                {
                    divshowst.Visible = false;
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }
        public void Fillmonth()
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@QueryFor", "S"));
            param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));

            DataSet ds;
            ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("GetMonthNamebwTwoSession_Proc", param);
            DataTable dt;
            dt = ds.Tables[0];

            Accordion1.DataSource = dt.DefaultView;
            Accordion1.DataBind();

            Session["Srno"] = ViewState["srno"];
            BLL.BLLInstance.LoadControls("~/sp/userControl/attendanceataGlance.ascx", W3);
        }
        private void Loadgrid(string studentId)
        {
            for (int i = 0; i < Accordion1.Panes.Count; i++)
            {
                GridView grd = (GridView)Accordion1.Panes[i].FindControl("GridView1");
                Label lblMonth = (Label)Accordion1.Panes[i].FindControl("lblMonth");
                Label lblYear = (Label)Accordion1.Panes[i].FindControl("lblYear");

                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
                param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
                param.Add(new SqlParameter("@MonthName", lblMonth.Text.Trim()));
                param.Add(new SqlParameter("@Year", lblYear.Text.Trim()));
                param.Add(new SqlParameter("@Srno", studentId));

                DataSet ds;
                ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("GetStudentMonthlyAtt_Proc", param);

                grd.DataSource = ds;
                grd.DataBind();
            }

            Accordion1.SelectedIndex = Accordion1.Panes.Count - 1;
        }

        private void CountTotalPresentAbsent()
        {
            for (int i = 0; i < Accordion1.Panes.Count; i++)
            {
                GridView grd = (GridView)Accordion1.Panes[i].FindControl("GridView1");
                int present = 0;
                int absent = 0;
                for (int j = 0; j < grd.Rows.Count; j++)
                {
                    Label lblAttendance = (Label)grd.Rows[j].FindControl("lblAttendance");
                    if (lblAttendance.Text == "Absent")
                    {
                        absent += 1;
                    }
                    else
                    {
                        present += 1;
                    }
                    Label lblTotalPrs = (Label)grd.FooterRow.FindControl("lblTotalPrs");
                    Label lblTotalAb = (Label)grd.FooterRow.FindControl("lblTotalAb");

                    lblTotalPrs.Text = present.ToString();
                    lblTotalAb.Text = absent.ToString();
                }
            }
        }
    }
}