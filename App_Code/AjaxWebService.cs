using System.Collections.Generic;
using System.Web.Services;
using System.Data.SqlClient;
using System.Data;


/// <summary>
/// Summary description for AjaxWebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class AjaxWebService : System.Web.Services.WebService
{

    public AjaxWebService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod(EnableSession = true)]
    public string UpdateAttendance(string className, string month, string monthName, string section, string srNo, string studentName, string day, string year, string attValue,
        string LoginName, string BranchCode, string session)
    {
        string s = "";
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = BAL.objBal.dbGet_connection().ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;

                cmd.CommandText = "USP_UpdateAttendanceOneByOne";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AttendanceMonth", monthName.ToString().Trim());
                cmd.Parameters.AddWithValue("@day", day.ToString().Trim());
                cmd.Parameters.AddWithValue("@month", month.ToString().Trim());
                cmd.Parameters.AddWithValue("@year", year.ToString().Trim());
                cmd.Parameters.AddWithValue("@ClassName", className.ToString().Trim());
                cmd.Parameters.AddWithValue("@SectionName", section.ToString().Trim());
                cmd.Parameters.AddWithValue("@SrNo", srNo.ToString().Trim());
                cmd.Parameters.AddWithValue("@StudentName", studentName.ToString().Trim());
                cmd.Parameters.AddWithValue("@AttendanceValue", attValue.ToString().Trim());
                cmd.Parameters.AddWithValue("@SessionName", session.ToString().Trim());
                cmd.Parameters.AddWithValue("@LoginName", LoginName.ToString().Trim());
                cmd.Parameters.AddWithValue("@BranchCode", BranchCode.ToString().Trim());
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }
        }
        return s;
    }

    public class StudentCls
    {
        public string Srno { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string ClassName { get; set; }
        public int id { get; set; }
        public string DocumentType { get; set; }
        public bool SoftCopy { get; set; }
        public bool HardCopy { get; set; }


    }

    [WebMethod(EnableSession = true)]
    public StudentCls[] GetStudentsDocReport(string drpClassId, string drpClassName, string drpSection, string drpAdmissioncType,
    string Hardcopy, string Softcopy, string BranchCode, string session)
    {
        List<StudentCls> students = new List<StudentCls>();
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = BAL.objBal.dbGet_connection().ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;

                cmd.CommandText = "Get_StudentDocsRecord";
                cmd.CommandType = CommandType.StoredProcedure;
                if (drpClassName != "")
                {
                    cmd.Parameters.AddWithValue("@ClassName", drpClassName.Trim());
                }
                if (drpSection != "")
                {
                    cmd.Parameters.AddWithValue("@SectionName", drpSection.Trim());

                }
                cmd.Parameters.AddWithValue("@Hardcopy", Hardcopy.Trim());
                cmd.Parameters.AddWithValue("@Softcopy", Softcopy.Trim());
                if (drpAdmissioncType != "")
                {
                    cmd.Parameters.AddWithValue("@TypeOFAdmision", drpAdmissioncType.Trim());
                }
                cmd.Parameters.AddWithValue("@sessionName", session.Trim());
                cmd.Parameters.AddWithValue("@BranchCode", BranchCode.Trim());
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                conn.Open();

                foreach (DataRow dtrow in dt.Rows)
                {
                    StudentCls user = new StudentCls();
                    user.Srno = dtrow["Srno"].ToString();
                    user.Name = dtrow["Name"].ToString();
                    user.FatherName = dtrow["FatherName"].ToString();
                    user.id = int.Parse(dtrow["id"].ToString());
                    user.DocumentType = dtrow["DocumentType"].ToString();
                    user.SoftCopy = bool.Parse(dtrow["SoftCopy"].ToString());
                    user.HardCopy = bool.Parse(dtrow["HardCopy"].ToString());
                    students.Add(user);
                }

                conn.Close();
            }
        }
        return students.ToArray();
    }
}