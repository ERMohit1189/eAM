using System;
using System.Data;
using System.Data.SqlClient;

public partial class admin_usercontrol_widgets_Wid2 : System.Web.UI.UserControl
{
    string sql = "";
    DataSet ds = new DataSet();
    DataTable dt = new DataTable();
    string EmpCount = "";
    string StuCount = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        else
        {

        }
        if (!IsPostBack)
        {
            sql += "select T1.EmpCount, T2.StuCount  from (SELECT count(*) as EmpCount, 'a' as forJoin FROM EmpGeneralDetail where  EmpGeneralDetail.SessionName='" + Session["SessionName"].ToString() + "' and EmpGeneralDetail.BranchCode=" + Session["BranchCode"] + " and convert(varchar(50), DATEPART(day, EDateOfBirth)) + '-' + convert(varchar(50), DATEPART(MONTH, EDateOfBirth)) = convert(varchar(50), DATEPART(day, GETDATE())) + '-' + convert(varchar(50), DATEPART(MONTH, GETDATE())) and Withdrwal=null)T1 ";
            sql += " inner join";
            sql += " (SELECT count(*) as StuCount, 'a' as forJoin FROM StudentGenaralDetail where  convert(varchar(50), DATEPART(day, DOB)) + '-' + convert(varchar(50), DATEPART(MONTH, DOB)) = convert(varchar(50), DATEPART(day, GETDATE())) + '-' + convert(varchar(50), DATEPART(MONTH, GETDATE())) and StudentGenaralDetail.SessionName='" + Session["SessionName"].ToString() + "' and StudentGenaralDetail.BranchCode=" + Session["BranchCode"]+")T2 on t1.forJoin = t2.forJoin";
            StuCount = BAL.objBal.ReturnTag(sql, "StuCount");
            EmpCount = BAL.objBal.ReturnTag(sql, "EmpCount");
            if (StuCount == "")
            {
                lblStuTotal.Text = "0";
            }
            else
            {
                lblStuTotal.Text = StuCount.ToString();
            }
            if (EmpCount == "")
            {
                lblEmpTotal.Text = "0";
            }
            else
            {
                lblEmpTotal.Text = EmpCount.ToString();
            }

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = BAL.objBal.dbGet_connection().ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "AdmissionsAtGlance";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@sessionName", Session["SessionName"].ToString().Trim());
                    cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString().Trim());
                    SqlDataAdapter das = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    das.Fill(dt);
                    cmd.Parameters.Clear();

                    if (dt.Rows.Count > 0)
                    {
                        lblAdmissions.Text = dt.Rows[0]["Admissions"].ToString() != string.Empty ? dt.Rows[0]["Admissions"].ToString() : "0";
                        lblAdmissionForms.Text = dt.Rows[0]["AdmissionsForms"].ToString() != string.Empty ? dt.Rows[0]["AdmissionsForms"].ToString() : "0";
                        lblEnquiries.Text = dt.Rows[0]["Enquiries"].ToString() != string.Empty ? dt.Rows[0]["Enquiries"].ToString() : "0";
                        lblTC.Text = dt.Rows[0]["TC"].ToString() != string.Empty ? dt.Rows[0]["TC"].ToString() : "0";
                        lblAdmissionsVsAdmForms.Text = dt.Rows[0]["AdmissionsVsAdmForms"].ToString() != string.Empty ? dt.Rows[0]["AdmissionsVsAdmForms"].ToString() : "0";
                        lblAdmFormsVsEnquiries.Text = dt.Rows[0]["AdmFormsVsEnquiries"].ToString() != string.Empty ? dt.Rows[0]["AdmFormsVsEnquiries"].ToString() : "0";
                        lblAdmissionsVsEnquiries.Text = dt.Rows[0]["AdmissionsVsEnquiries"].ToString() != string.Empty ? dt.Rows[0]["AdmissionsVsEnquiries"].ToString() : "0";
                    }
                }
            }
        }
    }
}