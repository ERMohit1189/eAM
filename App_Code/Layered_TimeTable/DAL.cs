using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for DAL
/// </summary>
public partial class DAL
{
    public SqlConnection con;
    public SqlCommand cmd = new SqlCommand();
    public SqlDataAdapter ad = new SqlDataAdapter();

    public string MSG = "";
    public bool IsExists;
    Campus conCampus = new Campus();
    BAL BALobj = new BAL();
    public static readonly DAL DALInstance = new DAL();

    public void MakeConnection()
    {
        con = new SqlConnection();
        try
        {
            dt = new DataTable();
            cmd = new SqlCommand();
            con = conCampus.dbGet_connection();
            con.Open();
        }
        catch { }
    }

    #region TimeTable

    public string SetShiftMaster(BAL.clsShiftMaster obj, bool isTeacher)
    {
        try
        {
            MSG = "";
            MakeConnection();

            if (isTeacher)
            {
                cmd.CommandText = "[USP_SetEmpShiftMaster]";
                cmd.Parameters.AddWithValue("@A02ID", obj.A02ID);
                cmd.Parameters.AddWithValue("@ShiftTime", obj.ShiftTime);
                //cmd.Parameters.AddWithValue("@LunchFromTime", obj.LunchFromTime);
                //cmd.Parameters.AddWithValue("@LunchToTime", obj.LunchToTime);

                cmd.Parameters.AddWithValue("@GraceTime", obj.GraceTime);
                cmd.Parameters.AddWithValue("@GraceTimeOut", obj.GraceTimeOut);

                cmd.Parameters.AddWithValue("@SLTime_HH", obj.SlTimeHh);
                cmd.Parameters.AddWithValue("@SLTime_MM", obj.SlTimeMm);
                cmd.Parameters.AddWithValue("@SLTime_SS", obj.SlTimeSs);
                cmd.Parameters.AddWithValue("@SLTime_TT", obj.SlTimeTt);

                cmd.Parameters.AddWithValue("@SLTime_HHO", obj.SlTimeHhO);
                cmd.Parameters.AddWithValue("@SLTime_MMO", obj.SlTimeMmO);
                cmd.Parameters.AddWithValue("@SLTime_SSO", obj.SlTimeSsO);
                cmd.Parameters.AddWithValue("@SLTime_TTO", obj.SlTimeTtO);

                cmd.Parameters.AddWithValue("@HDTime_HH", obj.HdTimeHh);
                cmd.Parameters.AddWithValue("@HDTime_MM", obj.HdTimeMm);
                cmd.Parameters.AddWithValue("@HDTime_SS", obj.HdTimeSs);
                cmd.Parameters.AddWithValue("@HDTime_TT", obj.HdTimeTt);


                cmd.Parameters.AddWithValue("@EmpDesId", obj.DesID);
            }
            else
            {
                cmd.CommandText = "[USP_SetShiftMaster]";
                cmd.Parameters.AddWithValue("@T01ID", obj.T01ID);
                cmd.Parameters.AddWithValue("@AssemblyTime", obj.AssemblyTime);
            }

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@BranchCode", obj.BranchCode);
            cmd.Parameters.AddWithValue("@GetFromTime", obj.FromTime);
            cmd.Parameters.AddWithValue("@IsActive", obj.IsActive);
            cmd.Parameters.AddWithValue("@SQL", obj.SQL);
            cmd.Parameters.AddWithValue("@LoginName", obj.LoginName);
            cmd.Parameters.AddWithValue("@SessionName", obj.SessionName);
            cmd.Parameters.AddWithValue("@ShiftName", obj.ShiftName);
            cmd.Parameters.AddWithValue("@GetToTime", obj.ToTime);


            cmd.Parameters.AddWithValue("@MSG", MSG);
            cmd.Parameters["@MSG"].Direction = ParameterDirection.InputOutput;
            cmd.Parameters["@MSG"].Size = 0x100;
            cmd.ExecuteNonQuery();
            MSG = cmd.Parameters["@MSG"].Value.ToString();
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return MSG;
    }

    public string SetStdShiftMaster(BAL.StdShiftMaster obj, int id)
    {
        try
        {
            MSG = "";
            MakeConnection();

            cmd.CommandText = "[USP_StdShiftMaster]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@ShiftName", obj.ShiftName);

            cmd.Parameters.AddWithValue("@STimeHH", obj.ShiftTimeHH);
            cmd.Parameters.AddWithValue("@STimeMM", obj.ShiftTimeMM);

            cmd.Parameters.AddWithValue("@FTimeHH", obj.FromTimeHH);
            cmd.Parameters.AddWithValue("@FTimeMM", obj.FromTimeMM);
            cmd.Parameters.AddWithValue("@FTimeTT", obj.FromTimeTT);

            cmd.Parameters.AddWithValue("@ToTimeHH", obj.ToTimeHH);
            cmd.Parameters.AddWithValue("@ToTimeMM", obj.ToTimeMM);
            cmd.Parameters.AddWithValue("@ToTimeTT", obj.ToTimeTT);

            cmd.Parameters.AddWithValue("@GraceTimeHH", obj.GraceTimeHH);
            cmd.Parameters.AddWithValue("@GraceTimeMM", obj.GraceTimeMM);

            cmd.Parameters.AddWithValue("@SQL", obj.SQL);

            cmd.Parameters.AddWithValue("@BranchCode", obj.BranchCode);
            cmd.Parameters.AddWithValue("@LoginName", obj.LoginName);
            cmd.Parameters.AddWithValue("@SessionName", obj.SessionName);

            cmd.Parameters.AddWithValue("@MSG", MSG);
            cmd.Parameters["@MSG"].Direction = ParameterDirection.InputOutput;
            cmd.Parameters["@MSG"].Size = 0x100;
            cmd.ExecuteNonQuery();
            MSG = cmd.Parameters["@MSG"].Value.ToString();
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return MSG;
    }

    public string SetShiftClass(BAL.clsShiftMaster obj)
    {
        try
        {
            MSG = "";
            MakeConnection();
            cmd.CommandText = "[USP_SetShiftClass]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@T02ID", obj.T02ID);
            cmd.Parameters.AddWithValue("@T01ID", obj.T01ID);
            cmd.Parameters.AddWithValue("@SQL", obj.SQL);

            cmd.Parameters.AddWithValue("@BranchCode", obj.BranchCode);
            cmd.Parameters.AddWithValue("@ClassSectionID", obj.SectionID);
            cmd.Parameters.AddWithValue("@LoginName", obj.LoginName);
            cmd.Parameters.AddWithValue("@SessionName", obj.SessionName);
            cmd.Parameters.AddWithValue("@ClassBranchID", obj.BranchID);
            cmd.Parameters.AddWithValue("@ClassID", obj.ClassID);
            cmd.Parameters.AddWithValue("@MSG", MSG);
            cmd.Parameters["@MSG"].Direction = ParameterDirection.InputOutput;
            cmd.Parameters["@MSG"].Size = 0x100;
            cmd.ExecuteNonQuery();
            MSG = cmd.Parameters["@MSG"].Value.ToString();
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return MSG;
    }

    public DataTable GetShiftClass(BAL.clsShiftMaster obj)
    {
        dt = new DataTable();
        try
        {
            MakeConnection();
            cmd.CommandText = "[USP_GetShiftClass]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@T01ID", obj.T01ID);
            cmd.Parameters.AddWithValue("@T02ID", obj.T02ID);
            cmd.Parameters.AddWithValue("@ClassID", obj.ClassID);
            cmd.Parameters.AddWithValue("@ClassBranchID", obj.BranchID);
            cmd.Parameters.AddWithValue("@ClassSectionID", obj.SectionID);
            cmd.Parameters.AddWithValue("@SessionName", obj.SessionName);
            cmd.Parameters.AddWithValue("@BranchCodes", BALobj.BranchCodes());
            ad.SelectCommand = cmd;
            ad.Fill(dt);
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return dt;
    }

    public DataTable GetShiftMaster(int ID, string ShiftName, Int32 IsActive, bool IsTeacher)
    {
        //dt = null;
        try
        {
            MakeConnection();

            if (IsTeacher == true)
            {
                cmd.CommandText = "[USP_GetEmpShiftMaster]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@A02ID", ID);
                cmd.Parameters.AddWithValue("@SessionName", BALobj.SessioNNames());
                cmd.Parameters.AddWithValue("@BranchCode", BALobj.BranchCodes());
            }
            else
            {
                cmd.CommandText = "[USP_GetShiftMaster]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@T01ID", ID);
                cmd.Parameters.AddWithValue("@SessionName", BALobj.SessioNNames());
                cmd.Parameters.AddWithValue("@BranchCode", BALobj.BranchCodes());
            }

            ad.SelectCommand = cmd;
            ad.Fill(dt);
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return dt;
    }

    public DataTable GetStdShiftMaster(int ID, string Session)
    {
        //dt = null;
        try
        {
            MakeConnection();

            cmd.CommandText = "[USP_StdShiftMaster]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@ID", ID);
            cmd.Parameters.AddWithValue("@BranchCode", BALobj.BranchCodes());
            cmd.Parameters.AddWithValue("@SQL", "S");
            cmd.Parameters.AddWithValue("@SessionName", Session);

            ad.SelectCommand = cmd;
            ad.Fill(dt);
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return dt;
    }

    public void DeleteRecord(string SQL)
    {
        try
        {
            MSG = "";
            MakeConnection();
            cmd.CommandText = SQL;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
    }

    public DataTable GetRecord(string SQL)
    {
        dt = new DataTable();
        try
        {
            MakeConnection();
            cmd.CommandText = SQL;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            ad.SelectCommand = cmd;
            ad.Fill(dt);
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return dt;
    }

    public DataTable GetClass(int ClassID, string SessionName)
    {
        try
        {
            MakeConnection();
            cmd.CommandText = "[USP_GetClass]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@ClassID", ClassID);
            cmd.Parameters.AddWithValue("@BranchCode", BALobj.BranchCodes());
            cmd.Parameters.AddWithValue("@SessionName", SessionName);
            ad.SelectCommand = cmd;
            ad.Fill(dt);
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return dt;
    }

    public DataTable GetClassSection(int ClassID, int ClassSectionID, string SessionName)
    {
        try
        {
            MakeConnection();
            cmd.CommandText = "[USP_GetClassSection]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@SessionName", SessionName);
            cmd.Parameters.AddWithValue("@BranchCode", BALobj.BranchCodes());
            cmd.Parameters.AddWithValue("@ClassSectionID", ClassSectionID);
            cmd.Parameters.AddWithValue("@ClassID", ClassID);
            ad.SelectCommand = cmd;
            ad.Fill(dt);
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return dt;
    }

    public DataTable GetClassBranch(int ClassID, int BranchID, string SessionName)
    {
        try
        {
            MakeConnection();
            cmd.CommandText = "[USP_GetClassBranch]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@ClassID", ClassID);
            cmd.Parameters.AddWithValue("@BranchCode", BALobj.BranchCodes());
            cmd.Parameters.AddWithValue("@SessionName", SessionName);
            cmd.Parameters.AddWithValue("@BranchID", BranchID);
            ad.SelectCommand = cmd;
            ad.Fill(dt);
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return dt;
    }

    public string SetOptionalSubjectManagement(List<BAL.clsOptionalSubjectManagement> obj)
    {
        try
        {
            MSG = "";
            MakeConnection();
            for (int i = 0; i < obj.Count; i++)
            {
                SqlCommand cmd = new SqlCommand
                {
                    CommandText = "[USP_SetOptionalSubjectManagement]",
                    CommandType = CommandType.StoredProcedure,
                    Connection = con
                };
                cmd.Parameters.AddWithValue("@SubjectGroupMaster_ID", obj[i].SubjectGroupMaster_ID);
                cmd.Parameters.AddWithValue("@SubjectGroupMaster_ID_Opt", obj[i].SubjectGroupMaster_ID_Opt);
                cmd.Parameters.AddWithValue("@SQL", obj[i].SQL);
                cmd.Parameters.AddWithValue("@ClassID", obj[i].ClassID);
                cmd.Parameters.AddWithValue("@BranchID", obj[i].BranchID);
                cmd.Parameters.AddWithValue("@SectionID", obj[i].SectionID);
                cmd.Parameters.AddWithValue("@SessionName", obj[i].SessionName);

                cmd.Parameters.AddWithValue("@MSG", MSG);
                cmd.Parameters["@MSG"].Direction = ParameterDirection.InputOutput;
                cmd.Parameters["@MSG"].Size = 0x100;
                cmd.ExecuteNonQuery();
                MSG = cmd.Parameters["@MSG"].Value.ToString();
            }
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return MSG;
    }

    public string SetOPeriodSubject(BAL.clsOPeriodSubject obj)
    {
        try
        {
            MSG = "";
            MakeConnection();

            SqlCommand cmd = new SqlCommand
            {
                CommandText = "USP_SetOPeriodSubject",
                CommandType = CommandType.StoredProcedure,
                Connection = con
            };

            cmd.Parameters.AddWithValue("@SelectedDay", obj.SelectedDay);
            cmd.Parameters.AddWithValue("@S02ID", obj.S02ID);
            cmd.Parameters.AddWithValue("@SQL", obj.SQL);
            cmd.Parameters.AddWithValue("@ClassID", obj.ClassID);
            cmd.Parameters.AddWithValue("@BranchID", obj.BranchID);
            cmd.Parameters.AddWithValue("@SectionID", obj.SectionID);
            cmd.Parameters.AddWithValue("@EmpID", obj.EmpID);
            cmd.Parameters.AddWithValue("@T07ID", obj.T07ID);

            cmd.Parameters.AddWithValue("@MSG", MSG);
            cmd.Parameters["@MSG"].Direction = ParameterDirection.InputOutput;
            cmd.Parameters["@MSG"].Size = 0x100;

            cmd.ExecuteNonQuery();
            MSG = cmd.Parameters["@MSG"].Value.ToString();
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return MSG;
    }

    public DataTable GetOPeriodSubject(BAL.clsOPeriodSubject obj)
    {
        try
        {
            MakeConnection();
            cmd.CommandText = "USP_GetOPeriodSubject";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@ClassID", obj.ClassID);
            cmd.Parameters.AddWithValue("@SectionID", obj.SectionID);
            cmd.Parameters.AddWithValue("@BranchID", obj.BranchID);
            cmd.Parameters.AddWithValue("@SelectedDay", obj.SelectedDay);
            ad.SelectCommand = cmd;
            ad.Fill(dt);
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return dt;
    }

    public DataTable GetOptionalSubjectManagement(int S01ID, string SessionName)
    {
        try
        {
            MakeConnection();
            cmd.CommandText = "[USP_GetOptionalSubjectManagement]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@S01ID", S01ID);
            cmd.Parameters.AddWithValue("@SessionName", SessionName);
            ad.SelectCommand = cmd;
            ad.Fill(dt);
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return dt;
    }

    public DataTable GetSubjectGroup(int ID, int ClassID, string ClassSectionName, string SessionName, int IsCompulsory, int AvoidID, int ClassBranchID)
    {
        try
        {
            MakeConnection();
            cmd.CommandText = "[USP_GetSubjectGroup]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@ID", ID);
            cmd.Parameters.AddWithValue("@ClassID", ClassID);
            cmd.Parameters.AddWithValue("@ClassSectionName", ClassSectionName);
            cmd.Parameters.AddWithValue("@ClassBranchID", ClassBranchID);
            cmd.Parameters.AddWithValue("@SessionName", SessionName);
            cmd.Parameters.AddWithValue("@IsCompulsory", IsCompulsory);
            cmd.Parameters.AddWithValue("@AvoidID", AvoidID);
            ad.SelectCommand = cmd;
            ad.Fill(dt);
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return dt;
    }

    public DataTable GetTimeTableClassWise(int ClassID, int BranchID, int SectionID, string TimeTableType)
    {
        //dt = null;
        try
        {
            MakeConnection();
            cmd.CommandText = "[USP_GetTimeTableClassWise]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@ClassID", ClassID);
            cmd.Parameters.AddWithValue("@SectionID", SectionID);
            cmd.Parameters.AddWithValue("@BranchID", BranchID);
            cmd.Parameters.AddWithValue("@TimeTableType", TimeTableType);

            ad.SelectCommand = cmd;
            ad.Fill(dt);
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return dt;
    }

    public DataTable GetTimeTableTeacherWise(string SessionName, string TimeTableType, string EmpID)
    {
        //dt = null;
        try
        {
            MakeConnection();
            cmd.CommandText = "[USP_GetTimeTableTeacherWise]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@SessionName", SessionName);
            cmd.Parameters.AddWithValue("@TimeTableType", TimeTableType);
            cmd.Parameters.AddWithValue("@EmpID", EmpID);
            ad.SelectCommand = cmd;
            ad.Fill(dt);
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return dt;
    }

    public string InsertTimeTableRule(BAL.clsGenerateTimeTable obj)
    {
        try
        {
            MSG = "";
            MakeConnection();
            cmd.CommandText = "[USP_SetTimeTableRule]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@T01ID", obj.T01ID);
            cmd.Parameters.AddWithValue("@GivenWorkingDays", obj.GivenWorkingDays);
            cmd.Parameters.AddWithValue("@LunchTimeValue", obj.LunchTimeValue);
            cmd.Parameters.AddWithValue("@LunchAfterPeriod", obj.LunchAfterPeriod);
            cmd.Parameters.AddWithValue("@DairyTimeValue", obj.DairyTimeValue);
            cmd.Parameters.AddWithValue("@DairyAfterPeriod", obj.DairyAfterPeriod);
            cmd.Parameters.AddWithValue("@OPeriodTimeValue", obj.OPeriodTimeValue);
            cmd.Parameters.AddWithValue("@OPeriodAfterPeriod", obj.OPeriodAfterPeriod);
            cmd.Parameters.AddWithValue("@GivenDayPeriod", obj.GivenDayPeriod);
            cmd.Parameters.AddWithValue("@GivenTeacherDayPeriod", obj.GivenTeacherDayPeriod);
            cmd.Parameters.AddWithValue("@GivenContinuousPeriod", obj.GivenContinuousPeriod);
            cmd.Parameters.AddWithValue("@IsFirstClassTeacher", obj.IsFirstClassTeacher);
            cmd.Parameters.AddWithValue("@SessionName", obj.SessionName);
            cmd.Parameters.AddWithValue("@LoginName", obj.LoginName);
            cmd.Parameters.AddWithValue("@BranchCode", obj.BranchCode);
            cmd.Parameters.AddWithValue("@IsShiftWise", obj.IsShiftWise);
            cmd.Parameters.AddWithValue("@MSG", MSG);
            cmd.Parameters["@MSG"].Direction = ParameterDirection.InputOutput;
            cmd.Parameters["@MSG"].Size = 0x100;
            cmd.ExecuteNonQuery();
            MSG = cmd.Parameters["@MSG"].Value.ToString();
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return MSG;
    }

    public string GenerateTimeTable(BAL.clsCommon obj)
    {
        try
        {
            MSG = "";
            MakeConnection();
            cmd.CommandText = "[USP_GenerateTimeTable]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.CommandTimeout = 30;
            cmd.Parameters.AddWithValue("@ClassID", obj.ClassID);
            cmd.Parameters.AddWithValue("@SectionID", obj.SectionID);
            cmd.Parameters.AddWithValue("@BranchID", obj.BranchID);
            cmd.Parameters.AddWithValue("@SessionName", obj.SessionName);
            cmd.Parameters.AddWithValue("@LoginName", obj.LoginName);
            cmd.Parameters.AddWithValue("@BranchCode", obj.BranchCode);
            cmd.Parameters.AddWithValue("@MSG", MSG);
            cmd.Parameters["@MSG"].Direction = ParameterDirection.InputOutput;
            cmd.Parameters["@MSG"].Size = 0x100;
            cmd.ExecuteNonQuery();
            MSG = cmd.Parameters["@MSG"].Value.ToString();
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return MSG;
    }

    public string GenerateTimeTableReplacement(BAL.clsCommon obj)
    {
        try
        {
            MSG = "";
            MakeConnection();
            cmd.CommandText = "[USP_GenerateTimeTableReplacement]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@ClassID", obj.ClassID);
            cmd.Parameters.AddWithValue("@SectionID", obj.SectionID);
            cmd.Parameters.AddWithValue("@BranchID", obj.BranchID);
            cmd.Parameters.AddWithValue("@SessionName", obj.SessionName);
            cmd.Parameters.AddWithValue("@LoginName", obj.LoginName);
            cmd.Parameters.AddWithValue("@BranchCode", obj.BranchCode);
            cmd.Parameters.AddWithValue("@MSG", MSG);
            cmd.Parameters["@MSG"].Direction = ParameterDirection.InputOutput;
            cmd.Parameters["@MSG"].Size = 0x100;
            cmd.ExecuteNonQuery();
            MSG = cmd.Parameters["@MSG"].Value.ToString();
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return MSG;
    }

    public string SetManualReplacement(BAL.clsManualReplacemnet obj)
    {
        try
        {
            MSG = "";
            MakeConnection();
            cmd.CommandText = "[USP_ManualReplacement]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@PeriodNo", obj.PeriodNo);
            cmd.Parameters.AddWithValue("@EmpID", obj.EmpID);
            cmd.Parameters.AddWithValue("@SelectedDay", obj.SelectedDay);

            cmd.Parameters.AddWithValue("@ClassID", obj.ClassID);
            cmd.Parameters.AddWithValue("@SectionID", obj.SectionID);
            cmd.Parameters.AddWithValue("@BranchID", obj.BranchID);
            cmd.Parameters.AddWithValue("@SessionName", obj.SessionName);
            cmd.Parameters.AddWithValue("@LoginName", obj.LoginName);
            cmd.Parameters.AddWithValue("@BranchCode", obj.BranchCode);
            cmd.Parameters.AddWithValue("@MSG", MSG);
            cmd.Parameters["@MSG"].Direction = ParameterDirection.InputOutput;
            cmd.Parameters["@MSG"].Size = 0x100;
            cmd.ExecuteNonQuery();
            MSG = cmd.Parameters["@MSG"].Value.ToString();
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return MSG;
    }

    public DataTable GetManualReplacement(int ClassID, int ClassSectionID, int ClassBranchID)
    {
        try
        {
            MakeConnection();
            cmd.CommandText = "[USP_GetManualReplacement]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@SectionID", ClassSectionID);
            cmd.Parameters.AddWithValue("@ClassID", ClassID);
            cmd.Parameters.AddWithValue("@BranchID", ClassBranchID);
            ad.SelectCommand = cmd;
            ad.Fill(dt);
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return dt;
    }

    public string GetTimeDifference(string fromTime, string toTime)
    {
        try
        {
            MakeConnection();
            cmd.CommandText = "SELECT dbo.UFN_GetTimeDifference(@FromTime,@ToTime) Time";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@FromTime", fromTime);
            cmd.Parameters.AddWithValue("@ToTime", toTime);
            var rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                MSG = rd[0].ToString();
            }
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return MSG;
    }

    public string GetDateTime()
    {
        try
        {
            MakeConnection();
            cmd.CommandText = "SELECT [dbo].[UFN_GetDateTime]() Time";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                MSG = rd[0].ToString();
            }
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return MSG;
    }

    public string GetDateTimeForHax()
    {
        try
        {
            MakeConnection();
            cmd.CommandText = "SELECT [dbo].[UFN_GetDateTimeForHax]() Time";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                MSG = rd[0].ToString();
            }
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return MSG;
    }

    public Boolean IsExist(string SQL)
    {
        Boolean IsExist = true;
        try
        {
            MakeConnection();
            cmd.CommandText = SQL;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                IsExist = Convert.ToBoolean(rd[0].ToString() == "1" ? true : false);
            }
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return IsExist;
    }

    public string SetSubjectPaperMaster(BAL.clsSubjectPaperMaster obj)
    {
        try
        {
            MSG = "";
            MakeConnection();
            cmd.CommandText = "[USP_SetSubjectPaperMaster]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@ClassID", obj.ClassID);
            cmd.Parameters.AddWithValue("@ClassSectionID", obj.SectionID);
            cmd.Parameters.AddWithValue("@ClassBranchID", obj.BranchID);
            cmd.Parameters.AddWithValue("@SQL", obj.SQL);
            cmd.Parameters.AddWithValue("@BranchCode", obj.BranchCode);
            cmd.Parameters.AddWithValue("@LoginName", obj.LoginName);
            cmd.Parameters.AddWithValue("@S02ID", obj.S02ID);
            cmd.Parameters.AddWithValue("@SessionName", obj.SessionName);
            cmd.Parameters.AddWithValue("@SubjectGroupID", obj.SubjectGroupID);
            cmd.Parameters.AddWithValue("@SubjectPaperName", obj.SubjectPaperName);
            cmd.Parameters.AddWithValue("@WeekPeriodCount", obj.WeekPeriod);
            cmd.Parameters.AddWithValue("@IsForExam", obj.IsForExam);
            cmd.Parameters.AddWithValue("@MSG", MSG);
            cmd.Parameters["@MSG"].Direction = ParameterDirection.InputOutput;
            cmd.Parameters["@MSG"].Size = 0x100;
            cmd.ExecuteNonQuery();
            MSG = cmd.Parameters["@MSG"].Value.ToString();
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return MSG;
    }

    public DataTable GetSubjectPaperMaster(string SessionName, string SubjectPaperName, int S02ID, int SubjectGroupID, int ClassID, int ClassSectionID, int ClassBranchID)
    {
        try
        {
            MakeConnection();
            cmd.CommandText = "[USP_GetSubjectPaperMaster]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@SessionName", SessionName);
            cmd.Parameters.AddWithValue("@BranchCode", BALobj.BranchCodes());
            cmd.Parameters.AddWithValue("@SubjectPaperName", SubjectPaperName);
            cmd.Parameters.AddWithValue("@S02ID", S02ID);
            cmd.Parameters.AddWithValue("@SubjectGroupID", SubjectGroupID);
            cmd.Parameters.AddWithValue("@ClassSectionID", ClassSectionID);
            cmd.Parameters.AddWithValue("@ClassID", ClassID);
            cmd.Parameters.AddWithValue("@ClassBranchID", ClassBranchID);
            ad.SelectCommand = cmd;
            ad.Fill(dt);
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return dt;
    }

    public DataTable GetTeacherForOPeriod(int ClassID, int SectionID, int BranchID, string DayName, Int32 PeriodNo)
    {
        try
        {
            MakeConnection();
            cmd.CommandText = "[USP_GetTeacherForOPeriod]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@DayName", DayName);
            cmd.Parameters.AddWithValue("@SectionID", SectionID);
            cmd.Parameters.AddWithValue("@ClassID", ClassID);
            cmd.Parameters.AddWithValue("@BranchID", BranchID);
            cmd.Parameters.AddWithValue("@PeriodNo", PeriodNo);
            ad.SelectCommand = cmd;
            ad.Fill(dt);
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return dt;
    }

    public DataTable GetPeriodForReplcment(int ClassID, int SectionID, int BranchID)
    {
        try
        {
            MakeConnection();
            cmd.CommandText = "[USP_GetPaeriodForReplcmnt]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@SectionID", SectionID);
            cmd.Parameters.AddWithValue("@ClassID", ClassID);
            cmd.Parameters.AddWithValue("@BranchID", BranchID);
            ad.SelectCommand = cmd;
            ad.Fill(dt);
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return dt;
    }

    public DataTable GetSubjectPaperForOPeriod(int ClassID, int SectionID, int BranchID, string EmpID)
    {
        try
        {
            MakeConnection();
            cmd.CommandText = "SELECT DISTINCT S02.S02ID,S02.SubjectPaperName FROM S02_SubjectPaperMaster S02 JOIN T06_PeriodSubjectGroupTeacher S ON S02.S02ID=S.S02ID WHERE S02.ClassID=@ClassID AND S02.IsActive=1 AND S02.IsDelete=0 AND S02.SectionID=@SectionID AND S02.BranchID=@BranchID AND S.EmpId=@EmpId";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@EmpID", EmpID);
            cmd.Parameters.AddWithValue("@SectionID", SectionID);
            cmd.Parameters.AddWithValue("@ClassID", ClassID);
            cmd.Parameters.AddWithValue("@BranchID", BranchID);
            ad.SelectCommand = cmd;
            ad.Fill(dt);
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return dt;
    }

    public DataTable GetTimeTableRule(Int32 T01ID, string SessionName)
    {
        dt = new DataTable();
        try
        {
            MakeConnection();
            cmd.CommandText = "[USP_GetTimeTableRule]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@T01ID", T01ID);
            cmd.Parameters.AddWithValue("@SessionName", SessionName);
            ad.SelectCommand = cmd;
            ad.Fill(dt);
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return dt;
    }

    #endregion


    #region Message Template

    public DataTable GetNotification(Int32 NotificationID, string NotificationTitle)
    {
        //dt = null;
        try
        {
            MakeConnection();
            cmd.CommandText = "[USP_GetNotification]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@NotificationID", NotificationID);
            cmd.Parameters.AddWithValue("@NotificationTitle", NotificationTitle);
            ad.SelectCommand = cmd;
            ad.Fill(dt);
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return dt;
    }

    public string SetNotificationTemplate(BAL.clsNotificationTemplate obj)
    {
        try
        {
            MSG = "";
            MakeConnection();
            cmd.CommandText = "[USP_SetNotificationTemplate]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@SQL", obj.SQL);
            cmd.Parameters.AddWithValue("@BranchCode", obj.BranchCode);
            cmd.Parameters.AddWithValue("@LoginName", obj.LoginName);
            cmd.Parameters.AddWithValue("@M01ID", obj.M01ID);
            cmd.Parameters.AddWithValue("@SessionName", obj.SessionName);
            cmd.Parameters.AddWithValue("@NotificationID", obj.NotificationID);
            cmd.Parameters.AddWithValue("@NotificationType", obj.NotificationType);
            cmd.Parameters.AddWithValue("@Template", obj.Template);
            cmd.Parameters.AddWithValue("@IsUnicode", obj.IsUnicode);
            cmd.Parameters.AddWithValue("@MSG", MSG);
            cmd.Parameters["@MSG"].Direction = ParameterDirection.InputOutput;
            cmd.Parameters["@MSG"].Size = 0x100;
            cmd.ExecuteNonQuery();
            MSG = cmd.Parameters["@MSG"].Value.ToString();

        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return MSG;
    }

    public DataTable GetNotificationTemplate(Int32 M01ID, Int32 NotationID, string NotationType, string Template, string SessionName)
    {
        DataTable dt1 = new DataTable();
        try
        {
            cmd = new SqlCommand();
            MakeConnection();
            cmd.CommandText = "[USP_GetNotificationTemplate]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@M01ID", M01ID);
            cmd.Parameters.AddWithValue("@NotationID", NotationID);
            cmd.Parameters.AddWithValue("@NotationType", NotationType);
            cmd.Parameters.AddWithValue("@Template", Template);
            cmd.Parameters.AddWithValue("@SessionName", SessionName);
            cmd.Parameters.AddWithValue("@BranchCode", BALobj.BranchCodes());


            ad.SelectCommand = cmd;
            ad.Fill(dt1);
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return dt1;
    }

    #endregion


    #region Accounting

    public string SetHeadType(BAL.clsHeadType obj)
    {
        try
        {
            MSG = "";
            MakeConnection();
            cmd.CommandText = "[USP_SetHeadType]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@SQL", obj.SQL);
            cmd.Parameters.AddWithValue("@BranchCode", obj.BranchCode);
            cmd.Parameters.AddWithValue("@LoginName", obj.LoginName);
            cmd.Parameters.AddWithValue("@SessionName", obj.SessionName);
            cmd.Parameters.AddWithValue("@IsActive", obj.IsActive);
            cmd.Parameters.AddWithValue("@MSG", MSG);

            cmd.Parameters.AddWithValue("@H01ID", obj.H01ID);
            cmd.Parameters.AddWithValue("@HeadType", obj.HeadType);
            cmd.Parameters.AddWithValue("@HeadCode", obj.HeadCode);
            cmd.Parameters.AddWithValue("@HeadMode", obj.HeadMode);

            cmd.Parameters["@MSG"].Direction = ParameterDirection.InputOutput;
            cmd.Parameters["@MSG"].Size = 0x100;
            cmd.ExecuteNonQuery();
            MSG = cmd.Parameters["@MSG"].Value.ToString();

        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return MSG;
    }

    public DataTable GetHeadType(BAL.clsHeadType obj)
    {
        try
        {
            cmd = new SqlCommand();
            MakeConnection();
            cmd.CommandText = "[USP_GetHeadType]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@H01ID", obj.H01ID);
            cmd.Parameters.AddWithValue("@HeadType", obj.HeadType);
            cmd.Parameters.AddWithValue("@IsActive", obj.IsActive);
            cmd.Parameters.AddWithValue("@SessionName", obj.SessionName);
            cmd.Parameters.AddWithValue("@BranchCode", BALobj.BranchCodes());


            ad.SelectCommand = cmd;
            ad.Fill(dt);
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return dt;
    }

    public string SetHeadMaster(BAL.clsHeadMaster obj)
    {
        try
        {
            MSG = "";
            MakeConnection();
            cmd.CommandText = "[USP_SetHeadMaster]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@SQL", obj.SQL);
            cmd.Parameters.AddWithValue("@BranchCode", obj.BranchCode);
            cmd.Parameters.AddWithValue("@LoginName", obj.LoginName);
            cmd.Parameters.AddWithValue("@SessionName", obj.SessionName);
            cmd.Parameters.AddWithValue("@IsActive", obj.IsActive);
            cmd.Parameters.AddWithValue("@MSG", MSG);

            cmd.Parameters.AddWithValue("@H01ID", obj.H01ID);
            cmd.Parameters.AddWithValue("@H02ID", obj.H02ID);
            cmd.Parameters.AddWithValue("@HeadName", obj.HeadName);
            cmd.Parameters.AddWithValue("@HeadCategory", obj.HeadCategory);
            cmd.Parameters.AddWithValue("@Remark", obj.Remark);

            cmd.Parameters["@MSG"].Direction = ParameterDirection.InputOutput;
            cmd.Parameters["@MSG"].Size = 0x100;
            cmd.ExecuteNonQuery();
            MSG = cmd.Parameters["@MSG"].Value.ToString();
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return MSG;
    }

    public DataTable GetHeadMaster(BAL.clsHeadMaster obj)
    {
        try
        {
            cmd = new SqlCommand();
            MakeConnection();
            cmd.CommandText = "[USP_GetHeadMaster]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@H01ID", obj.H01ID);
            cmd.Parameters.AddWithValue("@H02ID", obj.H02ID);
            cmd.Parameters.AddWithValue("@HeadName", obj.HeadName);
            cmd.Parameters.AddWithValue("@IsActive", obj.IsActive);
            cmd.Parameters.AddWithValue("@SessionName", obj.SessionName);
            cmd.Parameters.AddWithValue("@BranchCode", BALobj.BranchCodes());


            ad.SelectCommand = cmd;
            ad.Fill(dt);
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return dt;
    }

    public string SetDayBook(BAL.clsDayBook obj, bool IsVendor)
    {
        try
        {
            MSG = "";
            MakeConnection();
            cmd.CommandText = "[USP_SetDayBook]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@SQL", obj.SQL);
            cmd.Parameters.AddWithValue("@BranchCode", obj.BranchCode);
            cmd.Parameters.AddWithValue("@LoginName", obj.LoginName);
            cmd.Parameters.AddWithValue("@SessionName", obj.SessionName);
            cmd.Parameters.AddWithValue("@IsActive", obj.IsActive);
            cmd.Parameters.AddWithValue("@MSG", MSG);
            cmd.Parameters.AddWithValue("@Remark", obj.Remark);

            cmd.Parameters.AddWithValue("@H01ID", obj.H01ID);
            cmd.Parameters.AddWithValue("@H02ID", obj.H02ID);
            cmd.Parameters.AddWithValue("@H03ID", obj.H03ID);
            cmd.Parameters.AddWithValue("@Amount", obj.Amount);
            cmd.Parameters.AddWithValue("@IsAdvance", obj.IsAdvance);
            cmd.Parameters.AddWithValue("@Description", obj.Description);
            cmd.Parameters.AddWithValue("@Date", obj.Date);

            cmd.Parameters.AddWithValue("@EmpID", obj.EmpID);
            cmd.Parameters.AddWithValue("@SalaryMonth", obj.SalaryMonth);

            cmd.Parameters.AddWithValue("@M06ID", obj.M06ID);
            cmd.Parameters.AddWithValue("@M02ID", obj.M02ID);
            cmd.Parameters.AddWithValue("@PaymentNo", obj.PaymentNo);
            cmd.Parameters.AddWithValue("@DDChequeUTRNo", obj.DDChequeUTRNo);
            cmd.Parameters.AddWithValue("@DDChequeUTRDate", obj.DDChequeUTRDate);
            cmd.Parameters.AddWithValue("@FromAccount", obj.FromAccount);
            cmd.Parameters.AddWithValue("@BeneficiaryAccount", obj.BeneficiaryAccount);
            cmd.Parameters.AddWithValue("@Status", obj.Status);
            cmd.Parameters.AddWithValue("@IsRecurringAmount", obj.IsRecurringAmount);
            cmd.Parameters.AddWithValue("@InvoiceNo", obj.InvoiceNo);
            cmd.Parameters.AddWithValue("@InvoicePath", obj.InvoiceUrl);
            cmd.Parameters.AddWithValue("@M09ID", obj.M09ID);

            cmd.Parameters["@MSG"].Direction = ParameterDirection.InputOutput;
            cmd.Parameters["@MSG"].Size = 0x100;
            object i = cmd.ExecuteScalar();
            MSG = cmd.Parameters["@MSG"].Value.ToString();

            if (MSG == "" && IsVendor == true)
            {
                if (obj.lstVendorPayment.Count > 0)
                {
                    for (int j = 0; j < obj.lstVendorPayment.Count; j++)
                    {
                        MSG = "";
                        MakeConnection();
                        cmd.CommandText = "[USP_SetVendorPayment]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;

                        cmd.Parameters.AddWithValue("@SQL", obj.SQL);
                        cmd.Parameters.AddWithValue("@BranchCode", obj.BranchCode);
                        cmd.Parameters.AddWithValue("@LoginName", obj.LoginName);
                        cmd.Parameters.AddWithValue("@SessionName", obj.SessionName);
                        cmd.Parameters.AddWithValue("@IsActive", obj.IsActive);
                        cmd.Parameters.AddWithValue("@MSG", MSG);
                        cmd.Parameters.AddWithValue("@Remark", obj.Remark);

                        cmd.Parameters.AddWithValue("@H03ID", Convert.ToInt32(i));
                        cmd.Parameters.AddWithValue("@VendorID", obj.lstVendorPayment[j].VendorID);
                        cmd.Parameters.AddWithValue("@V04ID", obj.lstVendorPayment[j].V04ID);
                        cmd.Parameters.AddWithValue("@IsQutation", obj.lstVendorPayment[j].IsQuotation);
                        cmd.Parameters.AddWithValue("@Amount", obj.lstVendorPayment[j].FinalAmount);
                        cmd.Parameters.AddWithValue("@TDSAmount", obj.lstVendorPayment[j].TDSAmount);
                        cmd.Parameters.AddWithValue("@TDSPer", obj.lstVendorPayment[j].TDSPer);

                        cmd.Parameters["@MSG"].Direction = ParameterDirection.InputOutput;
                        cmd.Parameters["@MSG"].Size = 0x100;
                        cmd.ExecuteNonQuery();
                        MSG = cmd.Parameters["@MSG"].Value.ToString();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return MSG;
    }



    public Decimal GetBalanceAmount()
    {
        decimal Value = 0;
        try
        {
            MakeConnection();
            cmd.CommandText = "SELECT dbo.UFN_GetBalanceAmount(" + BALobj.BranchCodes() + ") Balance";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            ad.SelectCommand = cmd;
            ad.Fill(dt);

            if (dt != null)
            {
                Value = Convert.ToDecimal(dt.Rows[0][0].ToString());
            }
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return Value;
    }

    public Decimal GetOpeningAmount()
    {
        decimal Value = 0;
        try
        {
            MakeConnection();
            cmd.CommandText = "SELECT dbo.UFN_GetOpeningAmount(" + BALobj.BranchCodes() + ") OpeningAmount";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            ad.SelectCommand = cmd;
            ad.Fill(dt);

            if (dt != null)
            {
                Value = Convert.ToDecimal(dt.Rows[0][0].ToString());
            }
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return Value;
    }

    public Decimal GetIncomeAmount()
    {
        decimal Value = 0;
        try
        {
            MakeConnection();
            cmd.CommandText = "SELECT dbo.UFN_GetIncomeAmount(" + BALobj.BranchCodes() + ") InComeAmount";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            ad.SelectCommand = cmd;
            ad.Fill(dt);

            if (dt != null)
            {
                Value = Convert.ToDecimal(dt.Rows[0][0].ToString());
            }
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return Value;
    }

    public Decimal GetExpenceAmount()
    {
        decimal Value = 0;
        try
        {
            MakeConnection();
            cmd.CommandText = "SELECT dbo.UFN_GetExpenceAmount(" + BALobj.BranchCodes() + ") InExpenceAmount";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            ad.SelectCommand = cmd;
            ad.Fill(dt);

            if (dt != null)
            {
                Value = Convert.ToDecimal(dt.Rows[0][0].ToString());
            }
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return Value;
    }

    public Decimal GetClosingAmount()
    {
        decimal Value = 0;
        try
        {
            MakeConnection();
            cmd.CommandText = "SELECT dbo.UFN_GetClosingAmount(" + BALobj.BranchCodes() + ") InClosingAmount";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            ad.SelectCommand = cmd;
            ad.Fill(dt);

            if (dt != null)
            {
                Value = Convert.ToDecimal(dt.Rows[0][0].ToString());
            }
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return Value;
    }
    public Decimal GetQuotationBalanceAmount(Int32 V04V05ID, bool IsQuotation)
    {
        decimal Value = 0;
        try
        {
            MakeConnection();
            cmd.CommandText = "SELECT dbo.UFN_GetQuotationBalanceAmount(@V04V05ID,@IsQuotation,  @BranchCode) BalanceAmount";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@BranchCode", BALobj.BranchCodes());
            cmd.Parameters.AddWithValue("@V04V05ID", V04V05ID);
            cmd.Parameters.AddWithValue("@IsQuotation", IsQuotation);

            ad.SelectCommand = cmd;
            ad.Fill(dt);

            if (dt != null)
            {
                Value = Convert.ToDecimal(dt.Rows[0][0].ToString());
            }
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return Value;
    }


    public DataTable GetLedgerReport(BAL.clsDayBook obj)
    {
        try
        {
            cmd = new SqlCommand();
            MakeConnection();
            cmd.CommandText = "[USP_GetLedgerReport]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@H01ID", obj.H01ID);
            cmd.Parameters.AddWithValue("@H02ID", obj.H02ID);
            cmd.Parameters.AddWithValue("@H03ID", obj.H03ID);
            cmd.Parameters.AddWithValue("@IsActive", obj.IsActive);
            cmd.Parameters.AddWithValue("@SessionName", obj.SessionName);
            cmd.Parameters.AddWithValue("@BranchCode", BALobj.BranchCodes());
            cmd.Parameters.AddWithValue("@FromDate", obj.FromDate);
            cmd.Parameters.AddWithValue("@ToDate", obj.ToDate);
            cmd.Parameters.AddWithValue("@ORDERBY", obj.ORDERBY);

            ad.SelectCommand = cmd;
            ad.Fill(dt);
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return dt;
    }

    public DataTable GetDayBookReport(BAL.clsDayBook obj)
    {
        try
        {
            cmd = new SqlCommand();
            MakeConnection();
            cmd.CommandText = "[USP_GetDayBook]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@H01ID", obj.H01ID);
            cmd.Parameters.AddWithValue("@H02ID", obj.H02ID);
            cmd.Parameters.AddWithValue("@H03ID", obj.H03ID);
            cmd.Parameters.AddWithValue("@IsActive", obj.IsActive);
            cmd.Parameters.AddWithValue("@SessionName", obj.SessionName);
            cmd.Parameters.AddWithValue("@BranchCode", BALobj.BranchCodes());
            cmd.Parameters.AddWithValue("@FromDate", obj.FromDate);
            cmd.Parameters.AddWithValue("@ToDate", obj.ToDate);

            ad.SelectCommand = cmd;
            ad.Fill(dt);
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return dt;
    }
    public DataTable GetDayBookByEmpId(BAL.clsDayBook obj, string EmpID = "", string PaymentMode = "")
    {
        try
        {
            cmd = new SqlCommand();
            MakeConnection();
            cmd.CommandText = "[USP_GetDayBookByEmpId]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            //cmd.Parameters.AddWithValue("@H01ID", obj.H01ID);
            //cmd.Parameters.AddWithValue("@H02ID", obj.H02ID);
            //cmd.Parameters.AddWithValue("@H03ID", obj.H03ID);
            if (EmpID != "")
            {
                cmd.Parameters.AddWithValue("@EmpID", EmpID);
            }
            if (PaymentMode != "-1")
            {
                cmd.Parameters.AddWithValue("@PaymentMode", PaymentMode);
            }
            cmd.Parameters.AddWithValue("@IsActive", obj.IsActive);
            cmd.Parameters.AddWithValue("@SessionName", obj.SessionName);
            cmd.Parameters.AddWithValue("@BranchCode", BALobj.BranchCodes());
            cmd.Parameters.AddWithValue("@FromDate", obj.FromDate);
            cmd.Parameters.AddWithValue("@ToDate", obj.ToDate);

            ad.SelectCommand = cmd;
            ad.Fill(dt);
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return dt;
    }

    public string InsertOpeningAmount(BAL.clsCommon obj)
    {
        try
        {
            MSG = "";
            MakeConnection();
            cmd.CommandText = "[USP_InsertOpeningAmount]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@BranchCode", obj.BranchCode);
            cmd.Parameters.AddWithValue("@MSG", MSG);
            cmd.Parameters.AddWithValue("@OpeningAmountRemark", obj.Remark);
            cmd.Parameters.AddWithValue("@OpeningAmount", obj.OpeningAmount);

            cmd.Parameters["@MSG"].Direction = ParameterDirection.InputOutput;
            cmd.Parameters["@MSG"].Size = 0x100;
            cmd.ExecuteNonQuery();
            MSG = cmd.Parameters["@MSG"].Value.ToString();
        }

        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return MSG;
    }

    public string SetInstituteAccount(BAL.clsInstituteAccount obj)
    {
        try
        {
            MSG = "";
            MakeConnection();
            cmd.CommandText = "[USP_SetInstituteAccount]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@SQL", obj.SQL);
            cmd.Parameters.AddWithValue("@BranchCode", obj.BranchCode);
            cmd.Parameters.AddWithValue("@LoginName", obj.LoginName);
            cmd.Parameters.AddWithValue("@SessionName", obj.SessionName);
            cmd.Parameters.AddWithValue("@IsActive", obj.IsActive);
            cmd.Parameters.AddWithValue("@Remark", obj.Remark);
            cmd.Parameters.AddWithValue("@MSG", MSG);

            cmd.Parameters.AddWithValue("@M02ID", obj.M02ID);
            cmd.Parameters.AddWithValue("@M03ID", obj.M03ID);
            cmd.Parameters.AddWithValue("@M07ID", obj.M07ID);
            cmd.Parameters.AddWithValue("@AccountNo", obj.AccountNo);
            cmd.Parameters.AddWithValue("@AccountType", obj.AccountType);
            cmd.Parameters.AddWithValue("@OpeningAmount", obj.OpeningAmount);
            cmd.Parameters.AddWithValue("@AccountName", obj.AccountName);

            cmd.Parameters["@MSG"].Direction = ParameterDirection.InputOutput;
            cmd.Parameters["@MSG"].Size = 0x100;
            cmd.ExecuteNonQuery();
            MSG = cmd.Parameters["@MSG"].Value.ToString();
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return MSG;
    }

    public DataTable GetInstituteAccount(BAL.clsInstituteAccount obj)
    {
        try
        {
            cmd = new SqlCommand();
            MakeConnection();
            cmd.CommandText = "[USP_GetInstituteAccount]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@M07ID", obj.M07ID);
            cmd.Parameters.AddWithValue("@AccountNo", obj.AccountNo);

            cmd.Parameters.AddWithValue("@IsActive", obj.IsActive);
            cmd.Parameters.AddWithValue("@BranchCode", BALobj.BranchCodes());
            cmd.Parameters.AddWithValue("@SessionName", obj.SessionName);

            ad.SelectCommand = cmd;
            ad.Fill(dt);
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return dt;
    }

    public string SetHeadAutomation(BAL.clsHeadType obj)
    {
        try
        {
            MSG = "";
            MakeConnection();
            cmd.CommandText = "[USP_SetHeadAutomation]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@MSG", MSG);

            cmd.Parameters.AddWithValue("@H01ID", obj.H01ID);
            cmd.Parameters.AddWithValue("@IsAutomatic", obj.IsAutomatic);
            cmd.Parameters.AddWithValue("@BranchCode", BALobj.BranchCodes());

            cmd.Parameters["@MSG"].Direction = ParameterDirection.InputOutput;
            cmd.Parameters["@MSG"].Size = 0x100;
            cmd.ExecuteNonQuery();
            MSG = cmd.Parameters["@MSG"].Value.ToString();
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return MSG;
    }

    #endregion


    #region Masters

    public string SetBankMaster(BAL.clsBankMaster obj)
    {
        try
        {
            MSG = "";
            MakeConnection();
            cmd.CommandText = "[USP_SetBankMaster]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@SQL", obj.SQL);
            cmd.Parameters.AddWithValue("@BranchCode", obj.BranchCode);
            cmd.Parameters.AddWithValue("@LoginName", obj.LoginName);
            cmd.Parameters.AddWithValue("@SessionName", obj.SessionName);
            cmd.Parameters.AddWithValue("@IsActive", obj.IsActive);
            cmd.Parameters.AddWithValue("@Remark", obj.Remark);
            cmd.Parameters.AddWithValue("@MSG", MSG);

            cmd.Parameters.AddWithValue("@M02ID", obj.M02ID);
            cmd.Parameters.AddWithValue("@BankName", obj.BankName);

            cmd.Parameters["@MSG"].Direction = ParameterDirection.InputOutput;
            cmd.Parameters["@MSG"].Size = 0x100;
            cmd.ExecuteNonQuery();
            MSG = cmd.Parameters["@MSG"].Value.ToString();
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return MSG;
    }

    public DataTable GetBankMaster(BAL.clsBankMaster obj)
    {
        try
        {
            //cmd = new SqlCommand();
            //MakeConnection();
            //cmd.CommandText = "[USP_GetBankMaster]";
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Connection = con;

            //cmd.Parameters.AddWithValue("@M02ID", obj.M02ID);
            //cmd.Parameters.AddWithValue("@BankName", obj.BankName);
            //cmd.Parameters.AddWithValue("@IsActive", obj.IsActive);
            //cmd.Parameters.AddWithValue("@SessionName", obj.SessionName);
            //cmd.Parameters.AddWithValue("@BranchCode", BALobj.BranchCodes());
            //cmd.Parameters.AddWithValue("@HaveBranch", obj.HaveBranch);

            cmd = new SqlCommand();
            MakeConnection();
            cmd.CommandText = "[AccBankMasterProc]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@Action", "select");

            ad.SelectCommand = cmd;
            ad.Fill(dt);
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return dt;
    }

    public string SetBankBranchMaster(BAL.clsBankBranchMaster obj)
    {
        try
        {
            MSG = "";
            MakeConnection();
            cmd.CommandText = "[USP_SetBankBranchMaster]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@SQL", obj.SQL);
            cmd.Parameters.AddWithValue("@BranchCode", obj.BranchCode);
            cmd.Parameters.AddWithValue("@LoginName", obj.LoginName);
            cmd.Parameters.AddWithValue("@SessionName", obj.SessionName);
            cmd.Parameters.AddWithValue("@IsActive", obj.IsActive);
            cmd.Parameters.AddWithValue("@Remark", obj.Remark);
            cmd.Parameters.AddWithValue("@MSG", MSG);

            cmd.Parameters.AddWithValue("@M02ID", obj.M02ID);
            cmd.Parameters.AddWithValue("@M03ID", obj.M03ID);
            cmd.Parameters.AddWithValue("@BankBranchName", obj.BankBranchName);
            cmd.Parameters.AddWithValue("@Address", obj.Address);
            cmd.Parameters.AddWithValue("@IFSC", obj.IFSC);
            cmd.Parameters.AddWithValue("@PIN", obj.PIN);

            cmd.Parameters["@MSG"].Direction = ParameterDirection.InputOutput;
            cmd.Parameters["@MSG"].Size = 0x100;
            cmd.ExecuteNonQuery();
            MSG = cmd.Parameters["@MSG"].Value.ToString();
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return MSG;
    }

    public DataTable GetBankBranchMaster(BAL.clsBankBranchMaster obj)
    {
        try
        {
            //cmd = new SqlCommand();
            //MakeConnection();
            //cmd.CommandText = "[USP_GetBankBranchMaster]";
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Connection = con;

            //cmd.Parameters.AddWithValue("@M02ID", obj.M02ID);
            //cmd.Parameters.AddWithValue("@M03ID", obj.M03ID);
            //cmd.Parameters.AddWithValue("@BankBranchName", obj.BankBranchName);
            //cmd.Parameters.AddWithValue("@IsActive", obj.IsActive);
            //cmd.Parameters.AddWithValue("@SessionName", obj.SessionName);
            //cmd.Parameters.AddWithValue("@BranchCode", BALobj.BranchCodes());

            cmd = new SqlCommand();
            MakeConnection();
            cmd.CommandText = "[AccBankBarnchMasterProc]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@Action", "select");
            cmd.Parameters.AddWithValue("@BankId", "select");

            ad.SelectCommand = cmd;
            ad.Fill(dt);
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return dt;
    }

    public DataTable GetBankBranchMaster(int bankId)
    {
        try
        {
            cmd = new SqlCommand();
            MakeConnection();
            cmd.CommandText = "AccBankBarnchMasterProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@Action", "select");
            cmd.Parameters.AddWithValue("@BankId", bankId);

            ad.SelectCommand = cmd;
            ad.Fill(dt);
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return dt;
    }
    public string SetBankAccount(BAL.clsBankAcc obj)
    {
        try
        {
            MSG = "";
            MakeConnection();
            cmd.CommandText = "[USP_SetBankAccount]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@SQL", obj.SQL);
            cmd.Parameters.AddWithValue("@BranchCode", obj.BranchCode);
            cmd.Parameters.AddWithValue("@LoginName", obj.LoginName);
            cmd.Parameters.AddWithValue("@SessionName", obj.SessionName);
            cmd.Parameters.AddWithValue("@IsActive", obj.IsActive);
            cmd.Parameters.AddWithValue("@Remark", obj.Remark);
            cmd.Parameters.AddWithValue("@MSG", MSG);

            cmd.Parameters.AddWithValue("@M09ID", obj.M09ID);
            cmd.Parameters.AddWithValue("@M03ID", obj.M03ID);
            cmd.Parameters.AddWithValue("@AccountNo", obj.AccountNo);
            cmd.Parameters.AddWithValue("@AccountName", obj.AccountName);
            cmd.Parameters.AddWithValue("@AccountType", obj.AccountType);

            cmd.Parameters["@MSG"].Direction = ParameterDirection.InputOutput;
            cmd.Parameters["@MSG"].Size = 0x100;
            cmd.ExecuteNonQuery();
            MSG = cmd.Parameters["@MSG"].Value.ToString();
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return MSG;
    }

    public DataTable GetBankAccount(BAL.clsBankAcc obj)
    {
        try
        {
            cmd = new SqlCommand();
            MakeConnection();
            cmd.CommandText = "[USP_GetBankAccount]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@M02ID", obj.M02ID);
            cmd.Parameters.AddWithValue("@M03ID", obj.M03ID);
            cmd.Parameters.AddWithValue("@M09ID", obj.M09ID);
            cmd.Parameters.AddWithValue("@BranchCode", BALobj.BranchCodes());
            cmd.Parameters.AddWithValue("@IsActive", obj.IsActive);

            ad.SelectCommand = cmd;
            ad.Fill(dt);
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return dt;
    }

    public string SetVendorType(BAL.clsVendorType obj)
    {
        try
        {
            MSG = "";
            MakeConnection();
            cmd.CommandText = "[USP_SetVendorType]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@SQL", obj.SQL);
            cmd.Parameters.AddWithValue("@BranchCode", obj.BranchCode);
            cmd.Parameters.AddWithValue("@LoginName", obj.LoginName);
            cmd.Parameters.AddWithValue("@SessionName", obj.SessionName);
            cmd.Parameters.AddWithValue("@IsActive", obj.IsActive);
            cmd.Parameters.AddWithValue("@Remark", obj.Remark);
            cmd.Parameters.AddWithValue("@MSG", MSG);

            cmd.Parameters.AddWithValue("@M04ID", obj.M04ID);
            cmd.Parameters.AddWithValue("@VendorType", obj.VendorType);

            cmd.Parameters["@MSG"].Direction = ParameterDirection.InputOutput;
            cmd.Parameters["@MSG"].Size = 0x100;
            cmd.ExecuteNonQuery();
            MSG = cmd.Parameters["@MSG"].Value.ToString();
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return MSG;
    }

    public DataTable GetVendorType(BAL.clsVendorType obj)
    {
        try
        {
            cmd = new SqlCommand();
            MakeConnection();
            cmd.CommandText = "[USP_GetVendorType]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@M04ID", obj.M04ID);
            cmd.Parameters.AddWithValue("@VendorType", obj.VendorType);
            cmd.Parameters.AddWithValue("@IsActive", obj.IsActive);
            cmd.Parameters.AddWithValue("@SessionName", obj.SessionName);
            cmd.Parameters.AddWithValue("@BranchCode", BALobj.BranchCodes());

            ad.SelectCommand = cmd;
            ad.Fill(dt);
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return dt;
    }

    public string SetOrganizationType(BAL.clsOrganizationType obj)
    {
        try
        {
            MSG = "";
            MakeConnection();
            cmd.CommandText = "[USP_SetOrganizationType]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@SQL", obj.SQL);
            cmd.Parameters.AddWithValue("@BranchCode", obj.BranchCode);
            cmd.Parameters.AddWithValue("@LoginName", obj.LoginName);
            cmd.Parameters.AddWithValue("@SessionName", obj.SessionName);
            cmd.Parameters.AddWithValue("@IsActive", obj.IsActive);
            cmd.Parameters.AddWithValue("@Remark", obj.Remark);
            cmd.Parameters.AddWithValue("@MSG", MSG);

            cmd.Parameters.AddWithValue("@M05ID", obj.M05ID);
            cmd.Parameters.AddWithValue("@OrganizationType", obj.OrganizationType);

            cmd.Parameters["@MSG"].Direction = ParameterDirection.InputOutput;
            cmd.Parameters["@MSG"].Size = 0x100;
            cmd.ExecuteNonQuery();
            MSG = cmd.Parameters["@MSG"].Value.ToString();
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return MSG;
    }

    public DataTable GetOrganizationType(BAL.clsOrganizationType obj)
    {
        try
        {
            cmd = new SqlCommand();
            MakeConnection();
            cmd.CommandText = "[USP_GetOrganizationType]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@M05ID", obj.M05ID);
            cmd.Parameters.AddWithValue("@OrganizationType", obj.OrganizationType);
            cmd.Parameters.AddWithValue("@IsActive", obj.IsActive);
            cmd.Parameters.AddWithValue("@SessionName", obj.SessionName);
            cmd.Parameters.AddWithValue("@BranchCode", BALobj.BranchCodes());

            ad.SelectCommand = cmd;
            ad.Fill(dt);
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return dt;
    }

    public DataTable GetCountry(BAL.clsCountryMaster obj)
    {
        try
        {
            cmd = new SqlCommand();
            MakeConnection();
            cmd.CommandText = "[USP_GetCountry]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@CountryID", obj.CountryID);
            cmd.Parameters.AddWithValue("@SessionName", obj.SessionName);

            ad.SelectCommand = cmd;
            ad.Fill(dt);
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return dt;
    }

    public DataTable GetStateMaster(BAL.clsStateMaster obj)
    {
        try
        {
            cmd = new SqlCommand();
            MakeConnection();
            cmd.CommandText = "[USP_GetState]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@CountryID", obj.CountryID);
            cmd.Parameters.AddWithValue("@StateID", obj.StateID);
            cmd.Parameters.AddWithValue("@SessionName", obj.SessionName);

            ad.SelectCommand = cmd;
            ad.Fill(dt);
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return dt;
    }

    public DataTable GetDistrictMaster(BAL.clsDistrictMaster obj)
    {
        try
        {
            cmd = new SqlCommand();
            MakeConnection();
            cmd.CommandText = "[USP_GetDistrict]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@CountryID", obj.CountryID);
            cmd.Parameters.AddWithValue("@StateID", obj.StateID);
            cmd.Parameters.AddWithValue("@DistrictID", obj.DistrictID);
            cmd.Parameters.AddWithValue("@SessionName", obj.SessionName);

            ad.SelectCommand = cmd;
            ad.Fill(dt);
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return dt;
    }

    public DataTable GetPaymentMode(BAL.clsPaymentMode obj)
    {
        try
        {
            cmd = new SqlCommand();
            MakeConnection();
            cmd.CommandText = "[USP_GetPaymentMode]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@M06ID", obj.M06ID);
            cmd.Parameters.AddWithValue("@IsActive", obj.IsActive);
            cmd.Parameters.AddWithValue("@SessionName", obj.SessionName);

            ad.SelectCommand = cmd;
            ad.Fill(dt);
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return dt;
    }

    #endregion 
    //=================================================

    #region Vendor

    public string SetVendor(BAL.clsVendorBank obj)
    {
        try
        {
            MSG = "";
            MakeConnection();
            cmd.CommandText = "[USP_SetVendor]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@SQL", obj.SQL);
            cmd.Parameters.AddWithValue("@BranchCode", obj.BranchCode);
            cmd.Parameters.AddWithValue("@LoginName", obj.LoginName);
            cmd.Parameters.AddWithValue("@SessionName", obj.SessionName);
            cmd.Parameters.AddWithValue("@IsActive", obj.IsActive);
            cmd.Parameters.AddWithValue("@Remark", obj.Remark);
            cmd.Parameters.AddWithValue("@MSG", MSG);

            cmd.Parameters.AddWithValue("@V01ID", obj.V01ID);
            cmd.Parameters.AddWithValue("@M04ID", obj.M04ID);
            cmd.Parameters.AddWithValue("@M05ID", obj.M05ID);
            cmd.Parameters.AddWithValue("@RegistrationNo", obj.RegistrationNo);
            cmd.Parameters.AddWithValue("@OrganizationName", obj.OrganizationName);
            cmd.Parameters.AddWithValue("@OwnerName", obj.OwnerName);
            cmd.Parameters.AddWithValue("@DisplayName", obj.DisplayName);
            cmd.Parameters.AddWithValue("@DOR", obj.DOR);
            cmd.Parameters.AddWithValue("@PAN", obj.PAN);
            cmd.Parameters.AddWithValue("@TAN", obj.TAN);
            cmd.Parameters.AddWithValue("@TIN", obj.TIN);
            cmd.Parameters.AddWithValue("@ServiceTaxNo", obj.ServiceTaxNo);
            cmd.Parameters.AddWithValue("@ContactPerson", obj.ContactPerson);
            cmd.Parameters.AddWithValue("@PhoneNo", obj.PhoneNo);
            cmd.Parameters.AddWithValue("@MobileNo", obj.MobileNo);
            cmd.Parameters.AddWithValue("@IsWhatsApp", obj.IsWhatsApp);
            cmd.Parameters.AddWithValue("@Website", obj.Website);
            cmd.Parameters.AddWithValue("@Address", obj.Address);
            cmd.Parameters.AddWithValue("@CountryID", obj.CountryID);
            cmd.Parameters.AddWithValue("@MailID", obj.MailID);
            cmd.Parameters.AddWithValue("@StateID", obj.StateID);
            cmd.Parameters.AddWithValue("@DistrictID", obj.DistrictID);
            cmd.Parameters.AddWithValue("@V02ID", obj.V02ID);
            cmd.Parameters.AddWithValue("@DocumentName", obj.DocumentName);
            cmd.Parameters.AddWithValue("@FileName", obj.FileName);
            cmd.Parameters.AddWithValue("@FilePath", obj.FilePath);
            cmd.Parameters.AddWithValue("@V03ID", obj.V03ID);
            cmd.Parameters.AddWithValue("@M02ID", obj.M02ID);
            cmd.Parameters.AddWithValue("@M03ID", obj.M03ID);
            cmd.Parameters.AddWithValue("@AccountNo", obj.AccountNo);
            cmd.Parameters.AddWithValue("@AccountType", obj.AccountType);
            cmd.Parameters.AddWithValue("@OpeningBalanceAmount", obj.OpeningBalanceAmount);
            cmd.Parameters.AddWithValue("@PIN", obj.PIN);
            cmd.Parameters.AddWithValue("@VendorID", obj.VendorID);

            cmd.Parameters["@MSG"].Direction = ParameterDirection.InputOutput;
            cmd.Parameters["@MSG"].Size = 0x100;
            cmd.ExecuteNonQuery();
            MSG = cmd.Parameters["@MSG"].Value.ToString();
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return MSG;
    }

    public DataTable GetVendor(BAL.clsVendorBank obj)
    {
        try
        {
            cmd = new SqlCommand();
            MakeConnection();
            cmd.CommandText = "[USP_GetVendor]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@V01ID", obj.V01ID);
            cmd.Parameters.AddWithValue("@M04ID", obj.M04ID);
            cmd.Parameters.AddWithValue("@M05ID", obj.M05ID);
            cmd.Parameters.AddWithValue("@VendorID", obj.VendorID);
            cmd.Parameters.AddWithValue("@PAN", obj.PAN);
            cmd.Parameters.AddWithValue("@TAN", obj.TAN);
            cmd.Parameters.AddWithValue("@TIN", obj.TIN);
            cmd.Parameters.AddWithValue("@ServiceTaxNo", obj.ServiceTaxNo);

            cmd.Parameters.AddWithValue("@IsActive", obj.IsActive);
            cmd.Parameters.AddWithValue("@SessionName", obj.SessionName);
            cmd.Parameters.AddWithValue("@BranchCode", BALobj.BranchCodes());

            ad.SelectCommand = cmd;
            ad.Fill(dt);
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return dt;
    }

    public string SetVendorQuotation(BAL.clsVendorQuotation obj)
    {
        try
        {
            MSG = "";
            MakeConnection();
            cmd.CommandText = "[USP_SetVendorQuotation]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@SQL", obj.SQL);
            cmd.Parameters.AddWithValue("@BranchCode", obj.BranchCode);
            cmd.Parameters.AddWithValue("@LoginName", obj.LoginName);
            cmd.Parameters.AddWithValue("@SessionName", obj.SessionName);
            cmd.Parameters.AddWithValue("@Remark", obj.Remark);
            cmd.Parameters.AddWithValue("@MSG", MSG);

            cmd.Parameters.AddWithValue("@V04ID", obj.V04ID);
            cmd.Parameters.AddWithValue("@V01ID", obj.V01ID);
            cmd.Parameters.AddWithValue("@QuotationFor", obj.QuotationFor);
            cmd.Parameters.AddWithValue("@Overview", obj.Overview);
            cmd.Parameters.AddWithValue("@Amount", obj.Amount);
            cmd.Parameters.AddWithValue("@RecurringAmount", obj.RecurringAmount);
            cmd.Parameters.AddWithValue("@FileName", obj.FileName);
            cmd.Parameters.AddWithValue("@FilePath", obj.FilePath);
            cmd.Parameters.AddWithValue("@Status", obj.Status);
            cmd.Parameters.AddWithValue("@QuotationNo", obj.QuotationNo);
            cmd.Parameters.AddWithValue("@RefNo", obj.RefNo);
            cmd.Parameters.AddWithValue("@Date", obj.Date);
            cmd.Parameters.AddWithValue("@SubmissionType", obj.SubmissionType);
            cmd.Parameters.AddWithValue("@IsDirectInvoice", obj.IsDirectInvoice);

            cmd.Parameters["@MSG"].Direction = ParameterDirection.InputOutput;
            cmd.Parameters["@MSG"].Size = 0x100;
            cmd.ExecuteNonQuery();
            MSG = cmd.Parameters["@MSG"].Value.ToString();
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return MSG;
    }

    public DataTable GetVendorQuotation(BAL.clsVendorQuotation obj)
    {
        try
        {
            cmd = new SqlCommand();
            MakeConnection();
            cmd.CommandText = "[USP_GetVendorQuotation]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@V01ID", obj.V01ID);
            cmd.Parameters.AddWithValue("@V04ID", obj.V04ID);
            cmd.Parameters.AddWithValue("@VendorID", obj.VendorID);
            cmd.Parameters.AddWithValue("@Status", obj.Status);
            cmd.Parameters.AddWithValue("@IsInvoicGenerated", obj.IsInvoicGenerated);
            cmd.Parameters.AddWithValue("@IsPaid", obj.IsPaid);
            cmd.Parameters.AddWithValue("@RefNo", obj.QRefNo);

            cmd.Parameters.AddWithValue("@SessionName", obj.SessionName);
            cmd.Parameters.AddWithValue("@BranchCode", BALobj.BranchCodes());

            ad.SelectCommand = cmd;
            ad.Fill(dt);
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return dt;
    }

    public DataTable GetVendorLedger(BAL.clsVendorQuotation obj)
    {
        try
        {
            cmd = new SqlCommand();
            MakeConnection();
            //cmd.CommandText = "SELECT *FROM dbo.UFN_GetQuotationPaymentDetail(@V04ID)";
            cmd.CommandText = "[USP_GetVendorLedger]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@V01ID", obj.V01ID);
            cmd.Parameters.AddWithValue("@V04V05ID", obj.V04V05ID);
            cmd.Parameters.AddWithValue("@VenderID", obj.VendorID);
            cmd.Parameters.AddWithValue("@SessionName", obj.SessionName);
            cmd.Parameters.AddWithValue("@BranchCode", BALobj.BranchCodes());
            cmd.Parameters.AddWithValue("@HaveBalance", obj.HaveBalance);

            ad.SelectCommand = cmd;
            ad.Fill(dt);
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return dt;
    }

    public DataTable GetPaymentHistory(BAL.clsVendorQuotation obj)
    {
        try
        {
            cmd = new SqlCommand();
            MakeConnection();
            cmd.CommandText = "[USP_GetPaymentHistory]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@V01ID", obj.V01ID);
            cmd.Parameters.AddWithValue("@V04V05ID", obj.V04V05ID);
            cmd.Parameters.AddWithValue("@VenderID", obj.VendorID);
            cmd.Parameters.AddWithValue("@SessionName", obj.SessionName);
            cmd.Parameters.AddWithValue("@BranchCode", BALobj.BranchCodes());
            cmd.Parameters.AddWithValue("@H06ID", obj.H06ID);
            cmd.Parameters.AddWithValue("@IsActive", obj.IsActive);
            cmd.Parameters.AddWithValue("@IsQuotation", obj.IsQuotation);

            ad.SelectCommand = cmd;
            ad.Fill(dt);
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return dt;
    }

    public DataTable GetPaymentDetail(BAL.clsDayBook obj)
    {
        try
        {
            cmd = new SqlCommand();
            MakeConnection();
            cmd.CommandText = "[USP_GetPaymentDetail]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@H03ID", obj.H03ID);
            cmd.Parameters.AddWithValue("@H04ID", obj.H04ID);
            cmd.Parameters.AddWithValue("@M06ID", obj.M06ID);
            cmd.Parameters.AddWithValue("@BranchCode", BALobj.BranchCodes());

            ad.SelectCommand = cmd;
            ad.Fill(dt);
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return dt;
    }

    public string SetQuotationApproval(BAL.clsVendorQuotation obj)
    {
        try
        {
            MSG = "";
            MakeConnection();
            cmd.CommandText = "[USP_SetVendorQuotationApproval]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@BranchCode", obj.BranchCode);
            cmd.Parameters.AddWithValue("@LoginName", obj.LoginName);
            cmd.Parameters.AddWithValue("@SessionName", obj.SessionName);
            cmd.Parameters.AddWithValue("@MSG", MSG);

            cmd.Parameters.AddWithValue("@V04ID", obj.V04ID);
            cmd.Parameters.AddWithValue("@Amount", obj.Amount);
            cmd.Parameters.AddWithValue("@Status", obj.Status);
            cmd.Parameters.AddWithValue("@RecurringAmount", obj.RecurringAmount);
            cmd.Parameters.AddWithValue("@Reason", obj.Reason);

            cmd.Parameters["@MSG"].Direction = ParameterDirection.InputOutput;
            cmd.Parameters["@MSG"].Size = 0x100;
            cmd.ExecuteNonQuery();
            MSG = cmd.Parameters["@MSG"].Value.ToString();
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return MSG;
    }

    public DataTable GetVendorQuotationForInvoice(BAL.clsVendorQuotation obj)
    {
        try
        {
            cmd = new SqlCommand();
            MakeConnection();
            cmd.CommandText = "[USP_GetVendorQuotationForInvoice]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@V01ID", obj.V01ID);
            cmd.Parameters.AddWithValue("@V04ID", obj.V04ID);
            cmd.Parameters.AddWithValue("@VendorID", obj.VendorID);
            cmd.Parameters.AddWithValue("@Status", obj.Status);

            cmd.Parameters.AddWithValue("@SessionName", obj.SessionName);
            cmd.Parameters.AddWithValue("@BranchCode", BALobj.BranchCodes());

            ad.SelectCommand = cmd;
            ad.Fill(dt);
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return dt;
    }

    public string SetVendorInvoice(BAL.clsVendorInvoice obj)
    {
        try
        {
            MSG = "";
            MakeConnection();
            cmd.CommandText = "[USP_SetVendorInvoice]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@SQL", obj.SQL);
            cmd.Parameters.AddWithValue("@BranchCode", obj.BranchCode);
            cmd.Parameters.AddWithValue("@LoginName", obj.LoginName);
            cmd.Parameters.AddWithValue("@SessionName", obj.SessionName);
            cmd.Parameters.AddWithValue("@Remark", obj.Remark);
            cmd.Parameters.AddWithValue("@IsActive", obj.IsActive);
            cmd.Parameters.AddWithValue("@MSG", MSG);

            cmd.Parameters.AddWithValue("@RefNo", obj.RefNo);
            cmd.Parameters.AddWithValue("@V04ID", obj.V04ID);
            cmd.Parameters.AddWithValue("@V01ID", obj.V01ID);
            cmd.Parameters.AddWithValue("@V05ID", obj.V05ID);
            cmd.Parameters.AddWithValue("@InvoiceNo", obj.InvoiceNo);
            cmd.Parameters.AddWithValue("@FileName", obj.FileName);
            cmd.Parameters.AddWithValue("@FilePath", obj.FilePath);
            cmd.Parameters["@MSG"].Direction = ParameterDirection.InputOutput;
            cmd.Parameters["@MSG"].Size = 0x100;
            cmd.ExecuteNonQuery();
            MSG = cmd.Parameters["@MSG"].Value.ToString();
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return MSG;
    }

    public DataTable GetVendorInvoice(BAL.clsVendorInvoice obj)
    {
        try
        {
            cmd = new SqlCommand();
            MakeConnection();
            cmd.CommandText = "[USP_GetVendorInvoice]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@V01ID", obj.V01ID);
            cmd.Parameters.AddWithValue("@V04ID", obj.V04ID);
            cmd.Parameters.AddWithValue("@V05ID", obj.V05ID);
            cmd.Parameters.AddWithValue("@VendorID", obj.VendorID);
            cmd.Parameters.AddWithValue("@QRefNo", obj.QRefNo);
            cmd.Parameters.AddWithValue("@IRefNo", obj.IRefNo);
            cmd.Parameters.AddWithValue("@IsPaid", obj.IsPaid);

            cmd.Parameters.AddWithValue("@SessionName", obj.SessionName);
            cmd.Parameters.AddWithValue("@BranchCode", BALobj.BranchCodes());

            ad.SelectCommand = cmd;
            ad.Fill(dt);
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return dt;
    }


    #endregion


    #region Staff

    public DataTable GetStaff(BAL.clsDayBook obj)
    {
        try
        {
            cmd = new SqlCommand();
            MakeConnection();
            cmd.CommandText = "SELECT *FROM GetAllStaffRecords_UDF(1) WHERE EmpId='" + obj.EmpID.Trim() + "' and BranchCode=" + BALobj.BranchCodes() + "";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;

            ad.SelectCommand = cmd;
            ad.Fill(dt);
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return dt;
    }

    public DataTable GetStaffSalary(BAL.clsDayBook obj)
    {
        try
        {
            cmd = new SqlCommand();
            MakeConnection();
            //cmd.CommandText = "SELECT *FROM dbo.UFN_GetStaffSalaryDetail(@EmpID,@SessionName,@SalaryMonth)";
            cmd.CommandText = "USP_GetEmployeeLedger";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@SalaryMonth", obj.SalaryMonth);
            cmd.Parameters.AddWithValue("@EmpID", obj.EmpID);
            cmd.Parameters.AddWithValue("@H05ID", obj.H05ID);
            cmd.Parameters.AddWithValue("@H03ID", obj.H03ID);
            cmd.Parameters.AddWithValue("@SessionName", obj.SessionName);
            cmd.Parameters.AddWithValue("@BranchCode", BALobj.BranchCodes());
            cmd.Parameters.AddWithValue("@IsActive", obj.IsActive);

            ad.SelectCommand = cmd;
            ad.Fill(dt);
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return dt;
    }

    #endregion


    #region Attendance

    public DataTable GetAttendanceReport(BAL.clsSearchAttendance obj)
    {
        try
        {
            cmd = new SqlCommand();
            MakeConnection();
            cmd.CommandText = "USP_GetAttendanceReport";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@SrNo", obj.SrNo);
            cmd.Parameters.AddWithValue("@ClassID", obj.ClassID);
            cmd.Parameters.AddWithValue("@SectionID", obj.SectionID);
            cmd.Parameters.AddWithValue("@BranchID", obj.BranchID);
            cmd.Parameters.AddWithValue("@AttendanceType", obj.AttendanceType);
            cmd.Parameters.AddWithValue("@FromDate", obj.FromDate);
            cmd.Parameters.AddWithValue("@ToDate", obj.ToDate);
            cmd.Parameters.AddWithValue("@SessionName", obj.SessionName);
            cmd.Parameters.AddWithValue("@BranchCode", obj.BranchCode);

            ad.SelectCommand = cmd;
            ad.Fill(dt);
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return dt;
    }

    public DataTable GetAttendanceReport1(BAL.clsSearchAttendance obj)
    {
        try
        {
            cmd = new SqlCommand();
            MakeConnection();
            cmd.CommandText = "USP_GetAttendanceReport";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@SrNo", obj.SrNo);
            cmd.Parameters.AddWithValue("@ClassID", obj.ClassID);
            cmd.Parameters.AddWithValue("@SectionID", obj.SectionID);
            cmd.Parameters.AddWithValue("@BranchID", obj.BranchID);
            cmd.Parameters.AddWithValue("@AttendanceType", obj.AttendanceType);
            cmd.Parameters.AddWithValue("@FromDate", obj.FromDate);
            cmd.Parameters.AddWithValue("@ToDate", obj.ToDate);
            cmd.Parameters.AddWithValue("@SessionName", obj.SessionName);
            cmd.Parameters.AddWithValue("@BranchCode", obj.BranchCode);
            cmd.Parameters.AddWithValue("@SearchType", obj.FileName);
            cmd.Parameters.AddWithValue("@BatchID", obj.EmpID);
            ad.SelectCommand = cmd;
            ad.Fill(dt);
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return dt;
    }

    public DataTable GetEmpAttendanceReport(BAL.clsSearchAttendance obj)
    {
        try
        {
            cmd = new SqlCommand();
            MakeConnection();
            cmd.CommandText = "USP_GetEmpAttendanceReport";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@EmpId", obj.EmpID);
            cmd.Parameters.AddWithValue("@AttendanceType", obj.AttendanceType);
            cmd.Parameters.AddWithValue("@FromDate", obj.FromDate);
            cmd.Parameters.AddWithValue("@Designation", obj.Designation);
            cmd.Parameters.AddWithValue("@BranchCode", BALobj.BranchCodes());
            ad.SelectCommand = cmd;
            ad.Fill(dt);
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return dt;
    }

    public string SetAttendanceShift(BAL.clsNotificationDate obj)
    {
        try
        {
            MSG = "";
            MakeConnection();

            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "[USP_SetAttendanceShift]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@SQL", obj.SQL);
            cmd.Parameters.AddWithValue("@BranchCode", obj.BranchCode);
            cmd.Parameters.AddWithValue("@LoginName", obj.LoginName);
            cmd.Parameters.AddWithValue("@SessionName", obj.SessionName);
            cmd.Parameters.AddWithValue("@IsActive", obj.IsActive);
            cmd.Parameters.AddWithValue("@Remark", obj.Remark);
            cmd.Parameters.AddWithValue("@MSG", MSG);

            cmd.Parameters.AddWithValue("@A02ID", obj.A02ID);
            cmd.Parameters.AddWithValue("@ShiftName", obj.ShiftName);
            cmd.Parameters.AddWithValue("@ShotName", obj.ShotName);
            cmd.Parameters.AddWithValue("@FromTimeShift", obj.FromTimeShift);
            cmd.Parameters.AddWithValue("@ToTimeShift", obj.ToTimeShift);
            cmd.Parameters.AddWithValue("@ShiftTime", obj.ShiftTime);
            cmd.Parameters.AddWithValue("@FromTimeLunch", obj.FromTimeLunch);
            cmd.Parameters.AddWithValue("@ToTimeLunch", obj.ToTimeLunch);
            cmd.Parameters.AddWithValue("@LunchTime", obj.LunchTime);
            cmd.Parameters.AddWithValue("@GraceTimeInMinute", obj.GraceTimeInMinute);
            cmd.Parameters.AddWithValue("@IsEarlyPunchAllowed", obj.IsEarlyPunchAllowed);
            cmd.Parameters.AddWithValue("@IsAutoSendNotification", obj.IsAutoSendNotification);
            cmd.Parameters.AddWithValue("@NotificationTime", obj.NotificationTime);

            cmd.Parameters["@MSG"].Direction = ParameterDirection.InputOutput;
            cmd.Parameters["@MSG"].Size = 0x100;
            cmd.ExecuteNonQuery();
            MSG = cmd.Parameters["@MSG"].Value.ToString();
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return MSG;
    }

    public DataTable GetAttendanceShift(BAL.clsNotificationDate obj)
    {
        try
        {
            cmd = new SqlCommand();
            MakeConnection();
            cmd.CommandText = "[USP_GetAttendanceShift]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@A02ID", obj.A02ID);
            cmd.Parameters.AddWithValue("@ShiftName", obj.ShiftName);
            cmd.Parameters.AddWithValue("@IsActive", obj.IsActive);
            cmd.Parameters.AddWithValue("@SessionName", obj.SessionName);
            cmd.Parameters.AddWithValue("@BranchCode", BALobj.BranchCodes());

            ad.SelectCommand = cmd;
            ad.Fill(dt);
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return dt;
    }

    public string SetNotificationDate(List<BAL.clsNotificationDate> obj)
    {
        try
        {
            MSG = "";
            MakeConnection();

            for (int i = 0; i < obj.Count; i++)
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "[USP_SetAttendanceNotificationDate]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                cmd.Parameters.AddWithValue("@SQL", obj[i].SQL);
                cmd.Parameters.AddWithValue("@BranchCode", obj[i].BranchCode);
                cmd.Parameters.AddWithValue("@LoginName", obj[i].LoginName);
                cmd.Parameters.AddWithValue("@SessionName", obj[i].SessionName);
                cmd.Parameters.AddWithValue("@IsActive", obj[i].IsActive);
                cmd.Parameters.AddWithValue("@Remark", obj[i].Remark);
                cmd.Parameters.AddWithValue("@MSG", MSG);

                cmd.Parameters.AddWithValue("@A02ID", obj[i].A02ID);
                cmd.Parameters.AddWithValue("@A03ID", obj[i].A03ID);
                cmd.Parameters.AddWithValue("@DateValue", obj[i].DateValue);

                cmd.Parameters["@MSG"].Direction = ParameterDirection.InputOutput;
                cmd.Parameters["@MSG"].Size = 0x100;
                cmd.ExecuteNonQuery();
                MSG = cmd.Parameters["@MSG"].Value.ToString();
            }
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return MSG;
    }

    public DataTable GetNotificationDate(BAL.clsNotificationDate obj)
    {
        try
        {
            cmd = new SqlCommand();
            MakeConnection();
            cmd.CommandText = "[USP_GetAttendanceNotificationDate]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@A03ID", obj.A03ID);
            cmd.Parameters.AddWithValue("@A02ID", obj.A02ID);
            cmd.Parameters.AddWithValue("@DateValue", obj.DateValue);
            cmd.Parameters.AddWithValue("@IsActive", obj.IsActive);
            cmd.Parameters.AddWithValue("@SessionName", obj.SessionName);

            ad.SelectCommand = cmd;
            ad.Fill(dt);
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return dt;
    }

    public DataTable GetAllMonthDates()
    {
        try
        {
            cmd = new SqlCommand();
            MakeConnection();
            cmd.CommandText = "[USP_GetMonthAllDates]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            ad.SelectCommand = cmd;
            ad.Fill(dt);
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return dt;
    }

    public string SetEmpLeave(BAL.clsEmpLeave obj)
    {
        try
        {
            MSG = "";
            MakeConnection();

            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "[USP_SetEmpLeave]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@SQL", obj.SQL);
            cmd.Parameters.AddWithValue("@BranchCode", obj.BranchCode);
            cmd.Parameters.AddWithValue("@LoginName", obj.LoginName);
            cmd.Parameters.AddWithValue("@SessionName", obj.SessionName);
            cmd.Parameters.AddWithValue("@MSG", MSG);

            cmd.Parameters.AddWithValue("@A03ID", obj.A03ID);
            cmd.Parameters.AddWithValue("@EmpID", obj.EmpID);
            cmd.Parameters.AddWithValue("@AppSubject", obj.AppSubject);
            cmd.Parameters.AddWithValue("@AppReason", obj.AppReason);
            cmd.Parameters.AddWithValue("@LeaveDays", obj.LeaveDays);
            cmd.Parameters.AddWithValue("@Address", obj.Address);
            cmd.Parameters.AddWithValue("@ContactNo1", obj.ContactNo1);
            cmd.Parameters.AddWithValue("@ContactNo2", obj.ContactNo2);
            cmd.Parameters.AddWithValue("@AppDate", obj.AppDate);
            cmd.Parameters.AddWithValue("@FromDate", obj.FromDate);
            cmd.Parameters.AddWithValue("@ToDate", obj.ToDate);
            cmd.Parameters.AddWithValue("@Status", obj.Status);
            cmd.Parameters.AddWithValue("@IsHalfDay", obj.IsHalfDay);
            cmd.Parameters.AddWithValue("@HalfDayType", obj.HalfDayType);
            cmd.Parameters.AddWithValue("@ApproveEmpID", obj.ApproveEmpID);


            cmd.Parameters["@MSG"].Direction = ParameterDirection.InputOutput;
            cmd.Parameters["@MSG"].Size = 0x100;
            cmd.ExecuteNonQuery();
            MSG = cmd.Parameters["@MSG"].Value.ToString();
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return MSG;
    }

    public DataTable GetEmpLeave(BAL.clsEmpLeave obj)
    {
        try
        {
            cmd = new SqlCommand();
            MakeConnection();
            cmd.CommandText = "[USP_GetEmpLeave]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@A03ID", obj.A03ID);
            cmd.Parameters.AddWithValue("@EmpID", obj.EmpID);
            cmd.Parameters.AddWithValue("@Status", obj.Status);
            cmd.Parameters.AddWithValue("@FromDate", obj.FromDate);
            cmd.Parameters.AddWithValue("@ToDate", obj.ToDate);
            cmd.Parameters.AddWithValue("@BranchCode", BALobj.BranchCodes());

            ad.SelectCommand = cmd;
            ad.Fill(dt);
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return dt;
    }

    #endregion


    #region Fees

    public DataTable GetValueInTable(string SQL)
    {
        try
        {
            cmd = new SqlCommand();
            MakeConnection();
            cmd.CommandText = SQL;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            ad.SelectCommand = cmd;
            ad.Fill(dt);
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return dt;
    }

    public string SetAPITransaction(BAL.clsAPITransaction obj)
    {
        try
        {
            MSG = "";
            MakeConnection();
            cmd.CommandText = "[USP_SetAPITransaction]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@SQL", obj.SQL);

            cmd.Parameters.AddWithValue("@F01ID", obj.F01ID);
            cmd.Parameters.AddWithValue("@TxnID", obj.TxnID);
            cmd.Parameters.AddWithValue("@SrNo", obj.SrNo);
            cmd.Parameters.AddWithValue("@Amount", obj.Amount);
            cmd.Parameters.AddWithValue("@Charges", obj.Charges);
            cmd.Parameters.AddWithValue("@Status", obj.Status);
            cmd.Parameters.AddWithValue("@Mode", obj.Mode);
            cmd.Parameters.AddWithValue("@PayUMoneyID", obj.PayUMoneyID);
            cmd.Parameters.AddWithValue("@PGType", obj.PGType);
            cmd.Parameters.AddWithValue("@BankRefNo", obj.BankRefNo);
            cmd.Parameters.AddWithValue("@SessionNames", BALobj.SessioNNames());
            cmd.Parameters.AddWithValue("@BranchCodes", BALobj.BranchCodes());
            cmd.Parameters.AddWithValue("@Error", obj.Error);

            cmd.Parameters.AddWithValue("@MSG", MSG);
            cmd.Parameters["@MSG"].Direction = ParameterDirection.InputOutput;
            cmd.Parameters["@MSG"].Size = 0x100;
            cmd.ExecuteNonQuery();
            MSG = cmd.Parameters["@MSG"].Value.ToString();
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return MSG;
    }

    public string SetApiTransactionAdmission(BAL.ClsApiTransactionAdmission obj)
    {
        try
        {
            MSG = "";
            MakeConnection();
            cmd.CommandText = "[USP_SetAPITransaction]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@SQL", obj.Sql);
            cmd.Parameters.AddWithValue("@F01ID", obj.F01Id);
            cmd.Parameters.AddWithValue("@TxnID", obj.TxnId);
            cmd.Parameters.AddWithValue("@SrNo", obj.SrNo);
            cmd.Parameters.AddWithValue("@Amount", obj.Amount);
            cmd.Parameters.AddWithValue("@Charges", obj.Charges);
            cmd.Parameters.AddWithValue("@Status", obj.Status);
            cmd.Parameters.AddWithValue("@Mode", obj.Mode);
            cmd.Parameters.AddWithValue("@PayUMoneyID", obj.PayUMoneyId);
            cmd.Parameters.AddWithValue("@PGType", obj.PgType);
            cmd.Parameters.AddWithValue("@BankRefNo", obj.BankRefNo);
            cmd.Parameters.AddWithValue("@Error", obj.Error);
            cmd.Parameters.AddWithValue("@BranchCodes", BALobj.BranchCodes());

            cmd.Parameters.AddWithValue("@MSG", MSG);
            cmd.Parameters["@MSG"].Direction = ParameterDirection.InputOutput;
            cmd.Parameters["@MSG"].Size = 0x100;
            cmd.ExecuteNonQuery();
            MSG = cmd.Parameters["@MSG"].Value.ToString();
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return MSG;
    }

    public DataTable GetBankMaster(BAL.clsAPITransaction obj)
    {
        try
        {
            cmd = new SqlCommand();
            MakeConnection();
            cmd.CommandText = "[USP_GetAPITransaction]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@Status", obj.Status);
            cmd.Parameters.AddWithValue("@F01ID", obj.F01ID);

            ad.SelectCommand = cmd;
            ad.Fill(dt);
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return dt;
    }

    public DataTable GetAPITransaction(BAL.clsAPITransaction obj)
    {
        try
        {
            cmd = new SqlCommand();
            MakeConnection();
            cmd.CommandText = "[USP_GetAPITransaction]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@F01ID", obj.F01ID);
            cmd.Parameters.AddWithValue("@Status", obj.Status);
            cmd.Parameters.AddWithValue("@FromDate", obj.FromDate);
            cmd.Parameters.AddWithValue("@ToDate", obj.ToDate);
            cmd.Parameters.AddWithValue("@BranchCode", BALobj.BranchCodes());

            ad.SelectCommand = cmd;
            ad.Fill(dt);
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return dt;
    }

    #endregion


    #region Emp Salary

    public DataTable GetSalaryComponent(BAL.clsSalaryComponent obj)
    {
        dt = new DataTable();
        try
        {
            MakeConnection();
            cmd.CommandText = "[USP_GetSalaryComponent]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@HR01ID", obj.HR01ID);
            cmd.Parameters.AddWithValue("@IsActive", obj.IsActive);
            cmd.Parameters.AddWithValue("@DesignationID", obj.DesignationID);
            cmd.Parameters.AddWithValue("@ComponentType", obj.ComponentType);
            cmd.Parameters.AddWithValue("@IsOther", obj.IsOther);

            ad.SelectCommand = cmd;
            ad.Fill(dt);
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return dt;
    }

    public string SetSalaryComponent(BAL.clsSalaryComponent obj, List<BAL.clsComponentValue> objLst = null)
    {
        try
        {
            MSG = "";
            MakeConnection();
            cmd.CommandText = "[USP_SetSalaryComponent]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@SQL", obj.SQL);

            cmd.Parameters.AddWithValue("@HR01ID", obj.HR01ID);
            cmd.Parameters.AddWithValue("@Component", obj.Component);
            cmd.Parameters.AddWithValue("@ComponentType", obj.ComponentType);
            cmd.Parameters.AddWithValue("@IsGross", obj.IsGross);

            cmd.Parameters.AddWithValue("@BranchCode", obj.BranchCode);
            cmd.Parameters.AddWithValue("@LoginName", obj.LoginName);
            cmd.Parameters.AddWithValue("@SessionName", obj.SessionName);
            cmd.Parameters.AddWithValue("@DesignationID", obj.DesignationID);
            cmd.Parameters.AddWithValue("@IsBasic", obj.IsBasic);

            cmd.Parameters.AddWithValue("@MSG", "");
            cmd.Parameters["@MSG"].Direction = ParameterDirection.InputOutput;
            cmd.Parameters["@MSG"].Size = 0x100;

            string ID = "";
            cmd.Parameters.AddWithValue("@ID", ID);
            cmd.Parameters["@ID"].Direction = ParameterDirection.InputOutput;
            cmd.Parameters["@ID"].Size = 0x100;

            cmd.ExecuteNonQuery();

            MSG = cmd.Parameters["@MSG"].Value.ToString();
            ID = cmd.Parameters["@ID"].Value.ToString();

            if (obj.SQL == "I")
            {
                if (MSG == "")
                {
                    SetComponentValue(objLst, ID, obj.DesignationID.ToString());
                }
            }
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return MSG;
    }

    public void SetComponentValue(List<BAL.clsComponentValue> objLst, string ID, string DesID)
    {
        try
        {
            for (Int32 i = 0; i < objLst.Count; i++)
            {
                MSG = "";
                MakeConnection();
                cmd.CommandText = "INSERT INTO HR02_ComponentValue(PartOf,IsPer,ComponentValue,HR01ID) VALUES(@PartOf,@IsPer,@ComponentValue,@HR01ID)";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;

                string PartOfID = "";
                if (objLst[i].PartOf != "CTC" || objLst[i].PartOf != "Gross")
                    PartOfID = Campus.CampusInstance.ReturnTag("SELECT HR01ID FROM HR01_SalaryComponent WHERE DesignationID='" + DesID + "' AND Component='" + objLst[i].PartOf + "' AND IsActive=1 AND IsDelete=0", "HR01ID");

                cmd.Parameters.AddWithValue("@PartOf", objLst[i].PartOf);
                cmd.Parameters.AddWithValue("@IsPer", objLst[i].IsPer);
                cmd.Parameters.AddWithValue("@ComponentValue", objLst[i].ComponentValue);
                cmd.Parameters.AddWithValue("@HR01ID", ID);

                cmd.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
    }

    public string CalculateSalaryComponent(decimal CTC, Int32 DesignationID, string EmpID)
    {
        try
        {
            MSG = "";
            MakeConnection();
            cmd.CommandText = "[USP_CalculateSalaryComponent]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@CTC", CTC);
            cmd.Parameters.AddWithValue("@DesignationID", DesignationID);
            cmd.Parameters.AddWithValue("@EmpID", EmpID);

            cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return MSG;
    }

    public string SetEmpSalaryComponent(string EmpID, Int32 ComponentID, decimal value, string SQL)
    {
        try
        {
            MSG = "";
            MakeConnection();
            cmd.CommandText = "[USP_SetEmpSalaryComponent]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@SQL", SQL);

            cmd.Parameters.AddWithValue("@ComponentID", ComponentID);
            cmd.Parameters.AddWithValue("@value", value);
            cmd.Parameters.AddWithValue("@EmpID", EmpID);

            cmd.Parameters.AddWithValue("@MSG", "");
            cmd.Parameters["@MSG"].Direction = ParameterDirection.InputOutput;
            cmd.Parameters["@MSG"].Size = 0x100;

            string ID = "";
            cmd.Parameters.AddWithValue("@ID", ID);
            cmd.Parameters["@ID"].Direction = ParameterDirection.InputOutput;
            cmd.Parameters["@ID"].Size = 0x100;

            cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return MSG;
    }

    public DataSet GetSalaryBreakups(string EmpID, string sessionname)
    {
        DataSet ds = new DataSet();
        try
        {
            MakeConnection();
            cmd.CommandText = "[USP_GetSalaryBreakups]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@EmpID", EmpID);
            cmd.Parameters.AddWithValue("@SessionName", sessionname);

            ad.SelectCommand = cmd;
            ad.Fill(ds);
        }
        catch (Exception ex)
        {
            MSG = ex.Message;
        }
        con.Close();
        return ds;
    }

    #endregion
}
