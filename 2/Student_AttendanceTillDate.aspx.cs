using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;
using c4SmsNew;
using System.Net.Mail;
using System.Collections.Generic;

public partial class Student_AttendanceTillDate : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";
    DataTable dt;
    protected void Page_PreInIt(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null) { Response.Redirect("~/default.aspx"); }
        if (Session["Logintype"].ToString() == "Admin")
        {
            this.MasterPageFile = "~/Master/admin_root-manager.master";
        }
        else if (Session["Logintype"].ToString() == "Staff")
        {
            this.MasterPageFile = "~/Staff/staff_root-manager.master";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);
        BLL.BLLInstance.LoadHeader("Report", header1);
        if (!IsPostBack)
        {
            oo.AddDateMonthYearDropDown(ToYY, ToMM, ToDD);
            oo.FindCurrentDateandSetinDropDown(ToYY, ToMM, ToDD);
            loadClass(DrpAtteClass);
            // BLL.BLLInstance.loadClass(DrpAtteClass, Session["SessionName"].ToString());
            DrpAttenSection.Items.Insert(0, new ListItem("<--Select-->", ""));
        }
    }
    protected void ToYY_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.YearDropDown(ToYY, ToMM, ToDD);
    }
    protected void ToMM_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.MonthDropDown(ToYY, ToMM, ToDD);
    }
    protected void DrpAtteClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        BLL.BLLInstance.loadSection(DrpAttenSection, Session["SessionName"].ToString(), DrpAtteClass.SelectedValue);
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        if (DrpAtteClass.SelectedItem.Text == "<--Select-->")
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please Select :<--Select-->:", "A");
        }
        else
        {
            string Todate = "";
            Todate = ToDD.SelectedItem.ToString() + "-" + ToMM.SelectedItem.ToString() + "-" + ToYY.SelectedItem.ToString();

            sql = "select SrNo, Name, FatherName, CombineClassName, TypeOFAdmision, DateOfAdmiission from AllStudentRecord_UDF('"+ Session["SessionName"] + "', " + Session["BranchCode"] + ") where ClassId=" + DrpAtteClass.SelectedValue + " and SectionID=" + DrpAttenSection.SelectedValue + "";
            DataTable dt;
            dt = oo.Fetchdata(sql);
            if (dt.Rows.Count > 0)
            {
                Grd.DataSource = dt;
                Grd.DataBind();

                for (int i = 0; i < Grd.Rows.Count; i++)
                {
                    Label lblsrno = (Label)Grd.Rows[i].FindControl("lblsrno");
                    Label presents = (Label)Grd.Rows[i].FindControl("presents");
                    Label absents = (Label)Grd.Rows[i].FindControl("absents");
                    Label total = (Label)Grd.Rows[i].FindControl("total");
                    Label percentage = (Label)Grd.Rows[i].FindControl("percentage");
                    Label cntfrom = (Label)Grd.Rows[i].FindControl("cntfrom");
                    List<SqlParameter> param = new List<SqlParameter>();
                    param.Add(new SqlParameter("@classid", DrpAtteClass.SelectedValue));
                    param.Add(new SqlParameter("@Todate", Todate.ToString()));
                    param.Add(new SqlParameter("@srno", lblsrno.Text));
                    param.Add(new SqlParameter("@SessionName", Session["SessionName"]));
                    param.Add(new SqlParameter("@SectionName", DrpAttenSection.SelectedItem.Text));
                    param.Add(new SqlParameter("@BranchCode", Session["BranchCode"]));
                    DataSet ds;
                    ds = new DLL().Sp_SelectRecord_usingExecuteDataset("GetAttendenceproc_TillDate", param);
                    presents.Text = ds.Tables[0].Rows[0]["Attendence"].ToString();
                    absents.Text = ds.Tables[0].Rows[0]["Absents"].ToString();
                    total.Text = ds.Tables[0].Rows[0]["totaldays"].ToString();
                    percentage.Text = ds.Tables[0].Rows[0]["percentage"].ToString();
                    cntfrom.Text = ds.Tables[0].Rows[0]["countfrom"].ToString();
                }

                ImageButton1.Visible = true;
                ImageButton2.Visible = true;
                ImageButton3.Visible = true;
                ImageButton4.Visible = true;
                abc.Visible = true;
                Label1.Text = "Student's Attendance (Till Date "+ Todate + ")";
            }
            else
            {
                Grd.DataSource = null;
                Grd.DataBind();
                ImageButton1.Visible = false;
                ImageButton2.Visible = false;
                ImageButton3.Visible = false;
                ImageButton4.Visible = false;
                abc.Visible = false;
            }
        }
    }

    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        oo.ExportToWord(Response, "StudentAttendanceTillDate.doc", gdv1);
    }

    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        oo.ExportToExcel("StudentAttendanceTillDate.xls", Grd);
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {

    }

    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = abc;
        if (Grd.Rows.Count > 0)
        {
            Grd.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        ScriptManager.RegisterClientScriptBlock(ImageButton4, GetType(), "onclick", "var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}", true);
    }
    private void loadClass(DropDownList DrpAtteClass)
    {
        if (Session["logintype"].ToString() == "Admin")
        {
            BLL.BLLInstance.loadClass(DrpAtteClass, Session["SessionName"].ToString());
        }
        else
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
            param.Add(new SqlParameter("@EmpCode", Session["LoginName"].ToString()));

            DrpAtteClass.DataSource = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("GetClassTeacherClassName_Proc", param);
            DrpAtteClass.DataTextField = "ClassName";
            DrpAtteClass.DataValueField = "Id";
            DrpAtteClass.DataBind();
            DrpAtteClass.Items.Insert(0, new ListItem("<--Select-->", "0"));
        }
    }

}

