using DataTier;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for DLL
/// </summary>
public class DLL
{
    private SqlConnection _con = new SqlConnection();
    private readonly Campus _oo = new Campus();
    private DataSet _ds;

    public static readonly DLL objDll=new DLL();

    public string Sp_Insert_Update_Delete_usingExecuteNonQuery(string query, List<SqlParameter> parameters)
    {
        string msg;
        try
        {
            _con = new SqlConnection();
            _con = _oo.dbGet_connection();
            if (_con.State == ConnectionState.Closed)
            {
                _con.Open();
            }
            SqlHelper.ExecuteNonQuery(_con, CommandType.StoredProcedure, query, parameters.ToArray());
            msg= parameters[parameters.Count - 1].Value.ToString();
            _con.Close();
        }
        catch (Exception ex)
        {
            msg = ex.Message;
            _con.Close();
        }
        return msg;
    }
               
    public object Sp_SelectRecord_usingExecuteScalar(string query, List<SqlParameter> parameters)
    {
        object record="";
        try
        {
            _con = new SqlConnection();
            _con = _oo.dbGet_connection();
            if (_con.State == ConnectionState.Closed)
            {
                _con.Open();
            }
            record = SqlHelper.ExecuteScalar(_con, CommandType.StoredProcedure, query, parameters.ToArray());
            _con.Close();
        }
        catch
        {
            _con.Close();
        }
        return record;
    }
  
    public DataSet Sp_SelectRecord_usingExecuteDataset(string query, List<SqlParameter> parameters)
    {
        _ds = new DataSet();
        try
        {
            _con = new SqlConnection();
            _con = _oo.dbGet_connection();
            if (_con.State == ConnectionState.Closed)
            {
                _con.Open();
            }
            _ds = SqlHelper.ExecuteDataset(_con, CommandType.StoredProcedure, query, parameters.ToArray());
            if (_ds.Tables.Count > 0)
            {
                _con.Close();
                return _ds;
            }
            else
            {
                _con.Close();
                return null;
            }                      
        }
        catch(Exception ex)
        {
            _con.Close();
            return null;
        } 
    }

    public DataSet SelectRecord_usingExecuteDataset(string querytext, List<SqlParameter> parameters)
    {
        _ds = new DataSet();
        try
        {
            _con = new SqlConnection();
            _con = _oo.dbGet_connection();
            if (_con.State == ConnectionState.Closed)
            {
                _con.Open();
            }
            _ds = SqlHelper.ExecuteDataset(_con, CommandType.Text, querytext, parameters.ToArray());
            if (_ds.Tables.Count > 0)
            {
                _con.Close();
                return _ds;
            }
            else
            {
                _con.Close();
                return null;
            }
        }
        catch (Exception)
        {
            _con.Close();
            return null;
        }

    }
    public DataSet SelectRecord_usingExecuteDataset(string querytext)
    {
        _ds = new DataSet();
        try
        {
            _con = new SqlConnection();
            _con = _oo.dbGet_connection();
            if (_con.State == ConnectionState.Closed)
            {
                _con.Open();
            }
            _ds = SqlHelper.ExecuteDataset(_con, CommandType.Text, querytext);
            if (_ds.Tables.Count > 0)
            {
                _con.Close();
                return _ds;
            }
            else
            {
                _con.Close();
                return null;
            }
        }
        catch (Exception)
        {
            _con.Close();
            return null;
        }

    }
    public string Sp_SelectRecord_usingExecuteReader(string query, List<SqlParameter> parameters)
    {
        SqlDataReader dr;
        string record = "";
        try
        {
            _con = new SqlConnection();
            _con = _oo.dbGet_connection();
            if (_con.State == ConnectionState.Closed)
            {
                _con.Open();
            }
            dr = SqlHelper.ExecuteReader(_con, CommandType.Text, query, parameters.ToArray());


            if (dr.Read())
            {
                record = dr[0].ToString();
            }

            dr.Close();
            if (dr.IsClosed)
            {
                _con.Close();
            }
        }
        catch
        {
            _con.Close();
        }
        return record;
    }
}