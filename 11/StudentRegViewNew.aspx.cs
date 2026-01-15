using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI.WebControls;

namespace _11
{
    public partial class AdminStudentRegView : System.Web.UI.Page
    {
        SqlConnection _con;
        readonly Campus _oo;
        private string _sql = "";
        // ReSharper disable once ArrangeTypeMemberModifiers
#pragma warning disable 169
        DataTable _dt;
#pragma warning restore 169
        private static string _studentId = String.Empty;

     
    }
}