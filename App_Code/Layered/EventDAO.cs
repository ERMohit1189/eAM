using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Security;
using System.Configuration;


/// <summary>
/// EventDAO class is the main class which interacts with the database. SQL Server express edition
/// has been used.
/// the event information is stored in a table named 'event' in the database.
///
/// Here is the table format:
/// event(event_id int, title varchar(100), description varchar(200),event_start datetime, event_end datetime)
/// event_id is the primary key
/// </summary>
public class EventDAO
{
	//change the connection string as per your database connection.
    //IU MS Sql Server

    private static string connectionString = Campus.CampusInstance.dbGet_connection().ConnectionString;

    
    public EventDAO()
    {
    }
    public static List<CalendarEvent> getEvents(DateTime start, DateTime end)
    {
        BLL obj = new BLL();
        List<CalendarEvent> events = new List<CalendarEvent>();
        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand("Select pm.Id event_id,Description description,PlannerName title,FromDate event_start,todate event_end,color from PlannerMaster pm Inner join PlannerType py on py.Id=pm.PlannerType and py.BranchCode=pm.BranchCOde and py.SessionName=pm.SessionName where FromDate>=@start AND todate<=@end and pm.SessionName=@SessionName and pm.BranchCode=@BranchCode", con);

        cmd.Parameters.AddWithValue("@start", start);
        cmd.Parameters.AddWithValue("@end", end);
        cmd.Parameters.AddWithValue("@SessionName", obj.SessionName());
        cmd.Parameters.AddWithValue("@BranchCode", obj.branchCode());

        using (con)
        {
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                CalendarEvent cevent = new CalendarEvent();
                cevent.id = int.Parse(reader["event_id"].ToString());
                cevent.title = (string)reader["title"];
                cevent.description = (string)reader["description"];
                cevent.start = (DateTime)reader["event_start"];
                cevent.end = (DateTime)reader["event_end"];
                cevent.color = (string)reader["color"];
                events.Add(cevent);
            }
        }
        return events;
    }
    public static List<CalendarEvent> getEvents1(DateTime start, DateTime end)
    {
        BLL obj = new BLL();
        List<CalendarEvent> events = new List<CalendarEvent>();
        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(); 
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "GetEventProc";
        cmd.Connection = con;
        cmd.Parameters.AddWithValue("@start", start);
        cmd.Parameters.AddWithValue("@end", end);
        cmd.Parameters.AddWithValue("@SessionName", obj.SessionName());
        cmd.Parameters.AddWithValue("@BranchCode", obj.branchCode());

        using (con)
        {
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                CalendarEvent cevent = new CalendarEvent();
                cevent.id = int.Parse(reader["id"].ToString());
                cevent.title = (string)reader["PlannerName"];
                cevent.description = (string)reader["PlannerName"];
                cevent.start = (DateTime)reader["SmallDate"];
                cevent.end = (DateTime)reader["SmallDate"];
                cevent.color = (string)reader["color"];
                events.Add(cevent);
            }
        }
        return events;
        //side note: if you want to show events only related to particular users,
        //if user id of that user is stored in session as Session["userid"]
        //the event table also contains a extra field named 'user_id' to mark the event for that particular user
        //then you can modify the SQL as:
        //SELECT event_id, description, title, event_start, event_end FROM event where user_id=@user_id AND event_start>=@start AND event_end<=@end
        //then add paramter as:cmd.Parameters.AddWithValue("@user_id", HttpContext.Current.Session["userid"]);
    }
}
