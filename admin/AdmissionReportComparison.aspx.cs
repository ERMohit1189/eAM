using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_StudentSessionReport : System.Web.UI.Page
{

    public string MSG = "", SQL = "";
    public static int A02ID = 0;
    public DataTable dt = new DataTable();

    private SqlConnection _con;
    private readonly Campus _oo;
    private string _sql, _sql1 = String.Empty;

    public admin_StudentSessionReport()
    {
        _con = new SqlConnection();
        _oo = new Campus();
    }
   

    protected void Page_preinit(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("../default.aspx");
        }

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindAdmissionTable();
        }
    }

    private void BindAdmissionTable()
    {
        DataTable dt = GetAdmissionData();
        StringBuilder sb = new StringBuilder();

        foreach (DataRow row in dt.Rows)
        {
            sb.Append("<tr>");
            sb.AppendFormat("<td>{0}</td>", row["Year"]);
            sb.AppendFormat("<td>{0}</td>", row["NewStudents"]);
            sb.AppendFormat("<td>{0}</td>", row["OldStudents"]);
            sb.AppendFormat("<td>{0}</td>", row["TotalAdmissions"]);
            sb.AppendFormat("<td>{0}</td>", row["Withdrawal"]);
            sb.AppendFormat("<td>{0}</td>", row["TCIssued"]);
            sb.AppendFormat("<td>{0}</td>", row["TotalLeft"]);
            sb.AppendFormat("<td>{0}</td>", row["NetStrength"]);
            sb.Append("</tr>");
        }

        ltAdmissionBody.Text = sb.ToString();
    }
    private DataTable GetAdmissionData()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Year");
        dt.Columns.Add("NewStudents", typeof(int));
        dt.Columns.Add("OldStudents", typeof(int));
        dt.Columns.Add("TotalAdmissions", typeof(int));
        dt.Columns.Add("Withdrawal", typeof(int));
        dt.Columns.Add("TCIssued", typeof(int));
        dt.Columns.Add("TotalLeft", typeof(int));
        dt.Columns.Add("NetStrength", typeof(int));
        string currentSession = Session["SessionName"].ToString();
        string branchCode = Session["BranchCode"].ToString();

        string _sql = @"
DECLARE @CurrentSession NVARCHAR(150) = '" + currentSession + @"';
DECLARE @BranchCode NVARCHAR(50) = '" + branchCode + @"';
DECLARE @StartYear INT = CAST(LEFT(@CurrentSession, 4) AS INT);

-- Temporary table for last 5 sessions
DECLARE @SessionList TABLE (SessionName NVARCHAR(20));

-- Fill last 5 sessions
DECLARE @i INT = 0;
WHILE @i < 5
BEGIN
    DECLARE @Session NVARCHAR(20) = 
        CAST(@StartYear - @i AS NVARCHAR(4)) + '-' + CAST(@StartYear - @i + 1 AS NVARCHAR(4));
    INSERT INTO @SessionList(SessionName) VALUES (@Session);
    SET @i = @i + 1;
END

;WITH Summary AS (
    SELECT 
        s.SessionName,
        COUNT(d.TypeOFAdmision) AS TotalAdmissions,
        COUNT(CASE WHEN d.TypeOFAdmision = 'NEW' THEN 1 END) AS TotalNewAdmissions,
        COUNT(CASE WHEN d.TypeOFAdmision <> 'NEW' THEN 1 END) AS TotalOldAdmissions,
        COUNT(CASE WHEN d.Withdrwal = 'W' THEN 1 END) AS TotalWithdrawalAdmissions,
        COUNT(CASE WHEN d.TCIssued = 1 THEN 1 END) AS TotalTCIssuedAdmissions,
        COUNT(CASE 
                WHEN d.TCIssued = 1 OR d.Withdrwal = 'W' THEN 1 
             END) AS TotalLeftAdmissions
    FROM 
        @SessionList s
    LEFT JOIN 
        StudentOfficialDetails d ON d.SessionName = s.SessionName AND d.BranchCode = @BranchCode
    GROUP BY 
        s.SessionName
)

SELECT 
    SessionName,
    TotalAdmissions,
    TotalNewAdmissions,
    TotalOldAdmissions,
    TotalWithdrawalAdmissions,
    TotalTCIssuedAdmissions,
    TotalLeftAdmissions,
    (TotalAdmissions - TotalLeftAdmissions) AS NetAdmissions
FROM 
    Summary
ORDER BY 
    SessionName;
";



        DataSet ds = _oo.GridFill(_sql); // ✅ correct

        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                dt.Rows.Add(
                    row["SessionName"].ToString(),
                    Convert.ToInt32(row["TotalNewAdmissions"]),
                    Convert.ToInt32(row["TotalOldAdmissions"]),
                    Convert.ToInt32(row["TotalAdmissions"]),
                    Convert.ToInt32(row["TotalWithdrawalAdmissions"]),
                    Convert.ToInt32(row["TotalTCIssuedAdmissions"]),
                    Convert.ToInt32(row["TotalLeftAdmissions"]),
                    Convert.ToInt32(row["NetAdmissions"])
                );
            }
        }

        return dt;
    }

}