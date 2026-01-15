using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Data;
using System.IO;

using System.Data.SqlClient;
using System.Configuration;
using System.Collections;

/// <summary>
/// Summary description for sqhelp
/// </summary>
public class sqhelp
{
    public sqhelp()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    #region Variable Declaration
    SqlConnection con;
    SqlDataAdapter da;
    SqlDataReader dr;
    SqlCommand cmd;
    DataTable dt = new DataTable();
    DataSet ds = new DataSet();
    object obj;

    #endregion

    #region Function to open connection

    public void open_connection()
    {
        if (con == null)
        {
            con = new SqlConnection(ConfigurationManager.AppSettings["MLMCON"]);
            con.Open();
        }
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
    }
    #endregion

    #region Function to close connection

    public void close_connection()
    {
        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
    }
    #endregion

    #region Function to ExecuteQuery

    public void Execute_Query(string str)
    {
        open_connection();
        cmd = new SqlCommand(str, con);
        cmd.ExecuteNonQuery();
        close_connection();
    }
    #endregion

    #region Function to ExecuteScalar

    public object Execute_Scalar(string str)
    {
        open_connection();
        cmd = new SqlCommand(str, con);
        obj = cmd.ExecuteScalar();
        close_connection();
        return obj;
    }
    #endregion

    #region Function to ExecuteReader

    public SqlDataReader Execute_Reader(string str)
    {
        open_connection();
        cmd = new SqlCommand(str, con);
        dr = cmd.ExecuteReader();
        return dr;
    }
    #endregion

    #region Function to return dataset

    public DataSet Return_Dataset(string str)
    {

        open_connection();
        da = new SqlDataAdapter(str, con);
        da.Fill(ds);
        close_connection();
        return ds;
    }
    #endregion

    #region Function to return datatable

    public DataTable Return_DataTable(string str)
    {
        open_connection();
        da = new SqlDataAdapter(str, con);
        da.Fill(dt);
        close_connection();
        return dt;
    }
    #endregion
}
