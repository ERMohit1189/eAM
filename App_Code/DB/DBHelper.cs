// ------------------------------------------------------------------------------------------
// This Code remains Intact
//Made by SHUBHAM SAXENA $$At Coding phase of Project Start$
//For all DB Operations.
// ------------------------------------------------------------------------------------------

#region "Using"
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Configuration;

#endregion

namespace DPSDBHelper.DB
{
    public class DBHelper
    {
        #region "Variables"
        static readonly string Conn = ConfigurationManager.AppSettings["MLMCON"].ToString();
        #endregion

        #region "DBHelper"
        public DBHelper()
        {
            //connString = WebConfigurationManager.ConnectionStrings("LocalSqlServer").ToString 
        }
        #endregion

        #region "GetuniqueData"
        public static DataTable GetuniqueData(string ProcName, string values)
        {
            SqlConnection dbconn = new SqlConnection();            
            dbconn.ConnectionString = Conn;
            dbconn.Open();
            SqlDataAdapter adp = new SqlDataAdapter(ProcName + values, dbconn);
            DataSet ds = new DataSet();
            try
            {
                adp.Fill(ds);
                adp.Dispose();
                dbconn.Close();
                dbconn.Dispose();
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                adp.Dispose();
                dbconn.Close();
                dbconn.Dispose();
                
                throw (ex);
            }

           
        }
        #endregion

        #region "GetParameters"
        public static DataTable GetParameters(string ProcName)
        {            
                SqlConnection dbconn = new SqlConnection(Conn);
                dbconn.Open();
                SqlCommand cmd = new SqlCommand("sp_SParameters " + ProcName + "", dbconn);
                IDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Columns.Add("Parameter_name");
                while (dr.Read())
                {
                    dt.Rows.Add(dr[0]);
                }
                try
                {                    
                    dr.Close();
                    dr.Dispose();
                    cmd.Dispose();
                    return dt;
                }
                catch (Exception ex)
                {
                    dr.Close();
                    dr.Dispose();
                    cmd.Dispose();
                  
                    throw (ex);
                }
        }
        #endregion

        #region "ExecuteProc"
        public static DBHelper ExecuteProc(string ProcName, string[] paramvalues)
        {
                DataTable dt = new DataTable();
                dt = GetParameters(ProcName);
                SqlConnection dbconn = new SqlConnection(Conn);
                dbconn.Open();
                SqlParameter[] param = new SqlParameter[paramvalues.Length];
                SqlCommand cmd = new SqlCommand(ProcName, dbconn);
                cmd.CommandType = CommandType.StoredProcedure;
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cmd.Parameters.AddWithValue((string)dt.Rows[i]["Parameter_name"], formatSQLInput(paramvalues[i]));
                }              
                try
                {
                    dt.Dispose();
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    dbconn.Close();
                    return null;
                }
                catch (Exception ex)
                {
                    dt.Dispose();                    
                    cmd.Dispose();
                    dbconn.Close();
                    
                    throw (ex);
                }
        }
        #endregion

        #region "ExecuteScalar"
        public static int? ExecuteScalar(string SP)
        {     
          
                SqlConnection dbconn = new SqlConnection();
                dbconn.ConnectionString = Conn;
                dbconn.Open();
                SqlCommand cmd = new SqlCommand(SP, dbconn);
                cmd.CommandType = CommandType.StoredProcedure;
                object o = cmd.ExecuteScalar();
                
                try
                {
                    dbconn.Close();
                    dbconn.Dispose();
                    cmd.Dispose();
                    if (o is int)
                        return (int)o;
                    else
                        return null;                 
                 }
                catch (Exception ex)
                {
                    dbconn.Close();
                    dbconn.Dispose();
                    cmd.Dispose();
                  
                    throw (ex);
                }
        }
        #endregion     

        #region "ExecuteScalar"
        public static int? ExecuteScalar(string ProcName, string[] paramvalues)
        {
            DataTable dt = new DataTable();
            dt = GetParameters(ProcName);
            SqlConnection dbconn = new SqlConnection(Conn);
            dbconn.Open();
            SqlParameter[] param = new SqlParameter[paramvalues.Length];
            SqlCommand cmd = new SqlCommand(ProcName, dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                cmd.Parameters.AddWithValue((string)dt.Rows[i]["Parameter_name"], formatSQLInput(paramvalues[i]));
            }
                try
                {
                    object o = cmd.ExecuteScalar();
                    dbconn.Close();
                    dbconn.Dispose();
                    cmd.Dispose();
                    if (o is int)
                        return (int)o;
                    else
                        return null;
                }
                catch (Exception ex)
                {
                    dbconn.Close();
                    dbconn.Dispose();
                    cmd.Dispose();
                  
                    throw (ex);
                }            
        }
        #endregion     

        #region "ExecuteScalar"
        public static string ExecuteScalarString(string ProcName, string[] paramvalues)
        {
            DataTable dt = new DataTable();
            dt = GetParameters(ProcName);
            SqlConnection dbconn = new SqlConnection(Conn);
            dbconn.Open();
            SqlParameter[] param = new SqlParameter[paramvalues.Length];
            SqlCommand cmd = new SqlCommand(ProcName, dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                cmd.Parameters.AddWithValue((string)dt.Rows[i]["Parameter_name"], formatSQLInput(paramvalues[i]));
            }
            try
            {
                object o = cmd.ExecuteScalar();
                dbconn.Close();
                dbconn.Dispose();
                cmd.Dispose();
                if (o == null)
                    return "";
                else
                   return o.ToString();
                
                //if (o is string)
                //    return (string)o;
                //else
                //    return "";
            }
            catch (Exception ex)
            {
                dbconn.Close();
                dbconn.Dispose();
                cmd.Dispose();
               
                throw (ex);
            }
        }
        #endregion     

        #region "ExecuteDataset"
        public static DataSet GetData(string ProcName, string[] paramvalues)
        {

                DataTable dt = new DataTable();
                dt = GetParameters(ProcName);
                SqlConnection dbconn = new SqlConnection(Conn);
                dbconn.Open();
                SqlParameter[] param = new SqlParameter[paramvalues.Length];
                SqlCommand cmd = new SqlCommand(ProcName, dbconn);
                cmd.CommandType = CommandType.StoredProcedure;
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cmd.Parameters.AddWithValue((string)dt.Rows[i]["Parameter_name"], formatSQLInput(paramvalues[i]));
                }
                dt.Dispose();
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                DataSet ds = new DataSet();
                adp.Fill(ds);
               
                try
                {
                    adp.Dispose();
                    cmd.Connection.Close();
                    cmd.Dispose();
                    dbconn.Close();
                    return ds;
                }
                catch (Exception ex)
                {
                    adp.Dispose();
                    cmd.Connection.Close();
                    cmd.Dispose();
                    dbconn.Close();
                   
                    throw (ex);
                }
        }
        #endregion

        #region "formatSQLInput()"
        public static string formatSQLInput(string s)
        {
            //Remove malicious charcters from links and images 
            s = s.Replace("$", "&#36");
            s = s.Replace("<", "&lt;");
            s = s.Replace(">", "&gt;");
            s = s.Replace("[", "&#091;");
            s = s.Replace("]", "&#093;");
            s = s.Replace("=", "&#061;");
            s = s.Replace("'", "''");
            s = s.Replace("select", "sel&#101;ct");
            s = s.Replace("join", "jo&#105;n");
            s = s.Replace("union", "un&#105;on");
            s = s.Replace("where", "wh&#101;re");
            s = s.Replace("insert", "ins&#101;rt");
            s = s.Replace("delete", "del&#101;te");
            s = s.Replace("update", "up&#100;ate");
            s = s.Replace("like", "lik&#101;");
            s = s.Replace("drop", "dro&#112;");
            s = s.Replace("create", "cr&#101;ate");
            s = s.Replace("modify", "mod&#105;fy");
            s = s.Replace("rename", "ren&#097;me");
            s = s.Replace("alter", "alt&#101;r");
            s = s.Replace("cast", "ca&#115;t");
            return s;
        }
        #endregion

        #region "ExecuteDataset"
        public static DataSet GetData(string Proc, string parameters)
        {
                SqlConnection dbconn = new SqlConnection();
                dbconn.ConnectionString = Conn;
                dbconn.Open();
                SqlDataAdapter adp = new SqlDataAdapter(Proc + " " + parameters, dbconn);
                DataSet ds = new DataSet();
                adp.Fill(ds);
               
                try
                {
                    adp.Dispose();
                    dbconn.Close();
                    dbconn.Dispose();
                    return ds;                  
                }
                catch (Exception ex)
                {
                    adp.Dispose();
                    dbconn.Close();
                    dbconn.Dispose();
                   
                    throw (ex);
                }
        }

        #endregion

        #region "ExecuteDataset"
        public static DataSet GetData(string Proc, SqlParameter[] param)
        {
                SqlConnection dbconn = new SqlConnection();
                dbconn.ConnectionString = Conn;
                dbconn.Open();
                SqlCommand cmd = new SqlCommand(Proc, dbconn);
                cmd.CommandType = CommandType.StoredProcedure;
                for (int i = 0; i <= param.Length - 1; i++)
                {
                    cmd.Parameters.AddWithValue(param[i].ParameterName, param[i].Value);
                }
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                DataSet ds = new DataSet();
                adp.Fill(ds);
                
                try
                {
                    adp.Dispose();
                    cmd.Connection.Close();
                    cmd.Dispose();
                    dbconn.Close();
                    dbconn.Dispose();
                    return ds;                    
                }
                catch (Exception ex)
                {
                    adp.Dispose();
                    cmd.Connection.Close();
                    cmd.Dispose();
                    dbconn.Close();
                    dbconn.Dispose();
                    
                    throw (ex);
                }
        }
        #endregion

        #region "ExecuteDataset"

        public static DataSet GetData(string sql)
        {
                SqlConnection dbconn = new SqlConnection();
                dbconn.ConnectionString = Conn;
                dbconn.Open();
                SqlDataAdapter adp = new SqlDataAdapter(sql, dbconn);
                DataSet ds = new DataSet();
                adp.Fill(ds);
              
                try
                {
                    adp.Dispose();                                        
                    dbconn.Close();
                    dbconn.Dispose();
                    return ds;
                }
                catch (Exception ex)
                {
                    adp.Dispose();                    
                    dbconn.Close();
                    dbconn.Dispose();
                   
                    throw (ex);
                }
        }

        #endregion

        #region "ExecuteReader"
        public static IDataReader ExecuteReader(string ProcName, string[] paramvalues)
        {

                DataTable dt = new DataTable();
                dt = GetParameters(ProcName);
                SqlConnection dbconn = new SqlConnection(Conn);
                dbconn.Open();
                SqlParameter[] param = new SqlParameter[paramvalues.Length];
                SqlCommand cmd = new SqlCommand(ProcName, dbconn);
                cmd.CommandType = CommandType.StoredProcedure;
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cmd.Parameters.AddWithValue((string)dt.Rows[i]["Parameter_name"], formatSQLInput(paramvalues[i]));
                }                
                try
                {
                    dt.Dispose();                    
                    return cmd.ExecuteReader(CommandBehavior.CloseConnection);
                }
                catch (Exception ex)
                {
                    dt.Dispose();
                    cmd.Dispose();
                    
                    throw (ex);
                }
        }
        #endregion

        #region "ExecuteReader"
        public static IDataReader ExecuteReader(string sql)
        {
                SqlConnection dbconn = new SqlConnection();
                dbconn.ConnectionString = Conn;
                dbconn.Open();
                SqlCommand cmd = new SqlCommand(sql, dbconn);                
                try
                { 
                    return cmd.ExecuteReader(CommandBehavior.CloseConnection);
                }
                catch (Exception ex)
                {                    
                    cmd.Dispose();
                   
                    throw (ex);
                }
        }
        #endregion

        #region "ExecuteNonQuery"
        public static void ExecuteNonQuery(string sql)
        {
            
                SqlConnection dbconn = new SqlConnection();
                dbconn.ConnectionString = Conn;
                dbconn.Open();
                SqlCommand cmd = new SqlCommand(sql, dbconn);             

                try
                {
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                    cmd.Dispose();
                    dbconn.Close();
                    dbconn.Dispose();
                }
                catch (Exception ex)
                {
                    cmd.Connection.Close();
                    cmd.Dispose();
                    dbconn.Close();
                    dbconn.Dispose();
                    
                    throw (ex);
                }
        }

        #endregion

        #region "ExecuteNonQuery"
        public static void ExecuteNonQuery(string Proc, SqlParameter[] param)
        {
                SqlConnection dbconn = new SqlConnection();
                dbconn.ConnectionString = Conn;
                dbconn.Open();
                SqlCommand cmd = new SqlCommand(Proc, dbconn);
                cmd.CommandType = CommandType.StoredProcedure;
                for (int i = 0; i <= param.Length - 1; i++)
                {
                    cmd.Parameters.AddWithValue(param[i].ParameterName, param[i].Value);
                }
              
                try
                {
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                    cmd.Dispose();
                    dbconn.Close();
                }
                catch (Exception ex)
                {
                    cmd.Connection.Close();
                    cmd.Dispose();
                    dbconn.Close();
                    dbconn.Dispose();
                  
                    throw (ex);
                }
        }

        #endregion

        #region "ExecuteNonQuery"
        public static void ExecuteNonQuery(string Proc, string strvalues)
        {
                SqlConnection dbconn = new SqlConnection();
                dbconn.ConnectionString = Conn;
                dbconn.Open();
                SqlCommand cmd = new SqlCommand(Proc + " " + strvalues, dbconn);               
                try
                {
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                    cmd.Dispose();
                    dbconn.Close();
                }
                catch (Exception ex)
                {                    
                    cmd.Connection.Close();
                    cmd.Dispose();
                    dbconn.Close();
                    
                    throw (ex);
                }
        }

        #endregion
    }
} 
