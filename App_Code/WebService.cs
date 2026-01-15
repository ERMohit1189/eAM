using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;

/// <summary>
/// Summary description for WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService
{


    public WebService()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }
    [WebMethod(EnableSession = true)]
    public string paymentCharges(string BranchCode, string GateWayName)
    {
        Campus oo = new Campus();
        string sql = "select Logo, paymentCharges from TblPaymentGateway where BranchCode=" + BranchCode + " and GateWayFor='Fee' and GateWayName='" + GateWayName + "' and Isactive=1";
        string image = "~/uploads/CollegeLogo/" + oo.ReturnTag(sql, "Logo");
        string charges = oo.ReturnTag(sql, "paymentCharges");
        string str = "{image: " + image + ", charges: " + charges + "}";
        return str;
    }
    [WebMethod(EnableSession = true)]
    public string[] GetStudents(string studentId)
    {
        List<string> students = new List<string>();
        var param = new List<SqlParameter>
        {
            new SqlParameter("@SessionName",Session["SessionName"].ToString()),
            new SqlParameter("@BranchCode",Session["BranchCode"].ToString()),
            new SqlParameter("@SearchText",studentId)
        };
        var ds = new DataSet();
        ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("Proc_GetStudentPersonalDetailsInSearchBox", param);

        if (ds == null) return students.ToArray();
        if (ds.Tables.Count == 0) return students.ToArray();
        if (ds.Tables[0].Rows.Count == 0) return students.ToArray();
        DataTable dt = ds.Tables[0];
        foreach (DataRow dr in dt.Rows)
        {
            students.Add(dr["SearchText"].ToString());
        }
        return students.ToArray();
    }

    [WebMethod(EnableSession = true)]
    public string[] GetHostelRoom(string SearchText)
    {
        List<string> roomsList = new List<string>();
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = BAL.objBal.dbGet_connection().ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "Select  rm.Id as roomId, rm.RoomNo+'-['+ hcm.CategoryName+' '+hl.BuildingLocation+' '+hrtm.RoomType+']' as roomname from RoomMaster rm inner join HostelCategoryMaster hcm on rm.CategoryId = hcm.Id inner join HostelLocationMaster hl on rm.BuildingLocationId = hl.Id inner join HostelRoomTypeMaster hrtm on rm.RoomTypeId = hrtm.Id where (rm.RoomNo like '%' + @SearchText + '%') and hrtm.BranchCode=" + Session["BranchCode"].ToString() + " and hcm.BranchCode=" + Session["BranchCode"].ToString() + " and hl.BranchCode=" + Session["BranchCode"].ToString() + " and rm.BranchCode=" + Session["BranchCode"].ToString() + "  and rm.Status = 1";
                cmd.Parameters.AddWithValue("@SearchText", SearchText);
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        roomsList.Add(string.Format("{0}@{1}", sdr["roomname"], sdr["roomId"]));
                    }
                }
                conn.Close();
            }
        }
        return roomsList.ToArray();


    }


    [WebMethod(EnableSession = true)]
    public string[] GetStudents_with_Withdarw(string studentId)
    {
        List<string> students = new List<string>();
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = BAL.objBal.dbGet_connection().ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select Name+' '+CombineClassName+' '+SrNo StudentName, CASE WHEN SrNo='' Then StEnRCode Else SrNo End StudentId from AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "'," + Session["BranchCode"].ToString() + ") where (Name like @SearchText + '%' or Srno like @SearchText + '%' or StEnRCode like @SearchText + '%') and ISNULL(Promotion,'')<>'Cancelled'";
                cmd.Parameters.AddWithValue("@SearchText", studentId);
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        students.Add(string.Format("{0}@{1}", sdr["StudentName"], sdr["StudentId"]));
                    }
                }
                conn.Close();
            }
        }
        return students.ToArray();
    }


    [WebMethod(EnableSession = true)]
    public string[] GetStudentForTc(string studentId)
    {
        List<string> students = new List<string>();
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = BAL.objBal.dbGet_connection().ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select Distinct (Name+' '+CombineClassName+' '+SrNo) Name, SrNo, id from AllStudentRecord_UDF('', " + Session["BranchCode"].ToString() + ") where (Name like @SearchText + '%' or Srno like @SearchText + '%') and ISNULL(Promotion,'')<>'Cancelled' order by id desc";
                cmd.Parameters.AddWithValue("@SearchText", studentId);
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        students.Add(string.Format("{0}@{1}", sdr["Name"], sdr["SrNo"]));
                    }
                }
                conn.Close();
            }
        }
        return students.ToArray();
    }


    [WebMethod(EnableSession = true)]
    public string[] GetEmployee(string empId)
    {
        List<string> employee = new List<string>();
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = BAL.objBal.dbGet_connection().ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select Name+' '+Ecode EmployeeName,EmpId EmpId from GetAllStaffRecords_UDF(" + Session["BranchCode"].ToString() + ") where (Name like @SearchText + '%' or EmpId like @SearchText + '%' or Ecode like @SearchText + '%')";
                cmd.Parameters.AddWithValue("@SearchText", empId);
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        employee.Add(string.Format("{0}@{1}", sdr["EmployeeName"], sdr["EmpId"]));
                    }
                }
                conn.Close();
            }
        }
        return employee.ToArray();
    }
    [WebMethod(EnableSession = true)]
    public string[] GetEmployeeForCode(string empId)
    {
        List<string> employee = new List<string>();
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = BAL.objBal.dbGet_connection().ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select Name+' '+Ecode EmployeeName, Ecode Ecode from GetAllStaffRecords_UDF(" + Session["BranchCode"].ToString() + ") where (Name like @SearchText + '%' or EmpId like @SearchText + '%' or Ecode like @SearchText + '%')";
                cmd.Parameters.AddWithValue("@SearchText", empId);
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        employee.Add(string.Format("{0}@{1}", sdr["EmployeeName"], sdr["Ecode"]));
                    }
                }
                conn.Close();
            }
        }
        return employee.ToArray();
    }

    [WebMethod(EnableSession = true)]
    public string[] SportAcademyPlayer(string PlayerId)
    {
        List<string> students = new List<string>();
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = BAL.objBal.dbGet_connection().ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select Name+' '+PlyRegID Name, PlyRegID  from PlayerRegistration where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and (Name like @SearchText + '%' or PlyRegID like @SearchText + '%' )";
                cmd.Parameters.AddWithValue("@SearchText", PlayerId);
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        students.Add(string.Format("{0}@{1}", sdr["Name"], sdr["PlyRegID"]));
                    }
                }
                conn.Close();
            }
        }
        return students.ToArray();
    }
    [WebMethod(EnableSession = true)]
    public string[] SearchReceiptNo(string ReceiptNo)
    {
        List<string> students = new List<string>();
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = BAL.objBal.dbGet_connection().ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                string sql = "";
                sql = sql + " select distinct ReceiptNo+' ('+SrNo+')' name, (ReceiptNo+'##'+SrNo) ReceiptNo from(  ";
                sql = sql + " (select distinct SrNo, format(DepositDate, 'dd-MMM-yyyy') DepositDate, ReceiptNo, ModeOfPayment mop, receiptStatus Status from CompositFeeDeposit where ReceiptNo like '%' + @SearchText + '%' and BranchCode=" + Session["BranchCode"] + " )  ";
                sql = sql + " union all ";
                sql = sql + " (select distinct SrNo, format(DepositDate, 'dd-MMM-yyyy') DepositDate, Receipt_no ReceiptNo, Mode mop, PaymentSatus Status from OtherFeeDeposit where Receipt_no like '%' + @SearchText + '%' and BranchCode=" + Session["BranchCode"] + " ) ";
                sql = sql + " union all ";
                sql = sql + " (select distinct SrNo, format(TCIssueDate, 'dd-MMM-yyyy') DepositDate, RecieptNo ReceiptNo, mop, Status from TCCollection where RecieptNo like '%' + @SearchText + '%' and BranchCode=" + Session["BranchCode"] + " ) ";
                sql = sql + " union all ";
                sql = sql + " (select distinct SrNo, format(CCissuedate, 'dd-MMM-yyyy') DepositDate, RecieptNo ReceiptNo, mop, Status from CCCollection where RecieptNo like '%' + @SearchText + '%' and BranchCode=" + Session["BranchCode"] + " ) ";
                sql = sql + " union all ";
                sql = sql + " (select distinct FatherContactNo SrNo, format(RecordDate, 'dd-MMM-yyyy') DepositDate, RecieptNo ReceiptNo, mop, Status from AdmissionFormCollection where RecieptNo like '%' + @SearchText + '%' and BranchCode=" + Session["BranchCode"] + " )  ";
                sql = sql + " union all ";
                sql = sql + " (select distinct SrNo, format(RecordDate, 'dd-MMM-yyyy') DepositDate, Receipt_no ReceiptNo, mop, Status from Other_fee_collection_1 where Receipt_no like '%' + @SearchText + '%' and BranchCode=" + Session["BranchCode"] + " )  ";
                sql = sql + " union all ";
                sql = sql + " (select distinct SrNo, format(DepositDate, 'dd-MMM-yyyy') DepositDate, Receipt_no ReceiptNo, Mode mop, PaymentSatus status from UniformFeeDeposit where Receipt_no like '%' + @SearchText + '%' and BranchCode=" + Session["BranchCode"] + " )  ";
                sql = sql + " union all ";
                sql = sql + " (select distinct r.srno SrNo, format(f.RecordDate, 'dd-MMM-yyyy') DepositDate, Receiptno ReceiptNo, MOD mop, Status  from ReturnBookFine f inner join BookIssueReturn r on r.id=BIRid and r.sessionname=f.SessionName and r.branchcode=f.BranchCode ";
                sql = sql + " where Receiptno like '%' + @SearchText + '%' and f.BranchCode=" + Session["BranchCode"] + "))T1 order by ReceiptNo asc ";
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@SearchText", ReceiptNo);
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        students.Add(string.Format("{0}@{1}", sdr["name"], sdr["ReceiptNo"]));
                    }
                }
                conn.Close();
            }
        }
        return students.ToArray();
    }




}
