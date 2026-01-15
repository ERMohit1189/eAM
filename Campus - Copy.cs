using System;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Text;
using System.IO;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Reflection;
using System.Threading;
using System.Management;

public class Campus : System.Web.UI.Page
{
    Random random = new Random();
    SqlConnection con = new SqlConnection();
    SqlConnection con1 = new SqlConnection();
    SqlCommand cmd;

    HttpCookie cityInfo = new HttpCookie("cityInfo");

    SqlDataAdapter da;
    public string ReportPath = "";
    public string ReportfileName = "";
    public string ReportQuery = "";
    SqlDataReader dr;
    public Campus()
    {
         
        con = new SqlConnection();

        //Local------------------------------------------------------------------------------------------------------
        con.ConnectionString = "Data Source=ADVENTUS_C27641;Initial Catalog=eamtesting;User Id=sa; Password=Cincinati@56_54;encrypt=true;trustservercertificate=true;Max Pool Size=50000;Pooling=True;Connection Timeout=60;";

    }
    public SqlConnection dbGet_connection()
    {
        con = new SqlConnection();

        //Local------------------------------------------------------------------------------------------------------
	con.ConnectionString = "Data Source=ADVENTUS_C27641;Initial Catalog=eamtesting;User Id=sa; Password=Cincinati@56_54;encrypt=true;trustservercertificate=true;Max Pool Size=50000;Pooling=True;Connection Timeout=60;";
        
               return con;
    }

    public static string GetSiteRoot()
    {
        var port = System.Web.HttpContext.Current.Request.ServerVariables["SERVER_PORT"];
        if (port == null || port == "80" || port == "443")
            port = "";
        else
            port = ":" + port;

        var protocol = System.Web.HttpContext.Current.Request.ServerVariables["SERVER_PORT_SECURE"];
        if (protocol == null || protocol == "0")
            protocol = "http://";
        else
            protocol = "https://";

        var sOut = protocol + System.Web.HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + port + System.Web.HttpContext.Current.Request.ApplicationPath;

        if (sOut.EndsWith("/"))
        {
            sOut = sOut.Substring(0, sOut.Length - 1);
        }

        return sOut;
    }

    public void msgbox(Control ctrl, HtmlGenericControl divmsgbox, string msg, string msgsymbol)
    {
        var scripttag = "";
        divmsgbox.InnerHtml = "";
        var background = "";
        var icon = "";
        switch (msgsymbol)
        {
            case "S":
                background = "vd_bg-green";
                icon = "fa-check";
                //divmsgbox.Attributes.Add("class","msgbox vd_bg-green animated  fadeInLeft");
                //divmsgbox.InnerHtml = "<i class='fa fa-check' aria-hidden='true'></i> " + msg + " <script> setTimeout(dd, 6000);function dd(){var hide = document.getElementById('ContentPlaceHolder1_ContentPlaceHolderMainBox_msgbox');hide.className = 'msgbox msgbox-bx-n-z-n  vd_bg-green animated fadeInRight-dn';}</script>";
                break;
            case "U":
                background = "vd_bg-green";
                icon = "fa-check";
                //divmsgbox.Attributes.Add("class", "msgbox vd_bg-green animated  fadeInLeft");
                //divmsgbox.InnerHtml = "<i class='fa fa-check' aria-hidden='true'></i> " + msg + " <script> setTimeout(dd, 6000);function dd(){var hide = document.getElementById('ContentPlaceHolder1_ContentPlaceHolderMainBox_msgbox');hide.className = 'msgbox msgbox-bx-n-z-n  vd_bg-green animated fadeInRight-dn';}</script>";
                break;
            case "A":
                background = "vd_bg-yellow";
                icon = "fa-exclamation-triangle";
                //divmsgbox.Attributes.Add("class", "msgbox vd_bg-yellow animated  fadeInLeft");
                //divmsgbox.InnerHtml = "<i class='fa fa-exclamation-triangle' aria-hidden='true'></i> " + msg + " <script> setTimeout(dd, 6000);function dd(){var hide = document.getElementById('ContentPlaceHolder1_ContentPlaceHolderMainBox_msgbox');hide.className = 'msgbox msgbox-bx-n-z-n  vd_bg-yellow animated fadeInRight-dn';}</script>";
                break;
            case "W":
                background = "vd_bg-red";
                icon = "fa-times";
                //divmsgbox.Attributes.Add("class", "msgbox vd_bg-red animated  fadeInLeft");
                //divmsgbox.InnerHtml = "<i class='fa fa-times' aria-hidden='true'></i> " + msg + " <script> setTimeout(dd, 6000);function dd(){var hide = document.getElementById('ContentPlaceHolder1_ContentPlaceHolderMainBox_msgbox');hide.className = 'msgbox msgbox-bx-n-z-n  vd_bg-red animated fadeInRight-dn';}</script>";
                break;
            default:
                divmsgbox.InnerHtml = "";
                break;
        }

        scripttag = "<script>function enable() {var hide = document.getElementById('" + divmsgbox.ClientID + "');hide.className = \"msgbox " + background + " animated  fadeInLeft\";hide.innerHTML = '<i class=\"fa " + icon + "\" aria-hidden=\"true\"></i> " + msg + "';}</script>";
        scripttag = scripttag + " <script>function disable() {var hide = document.getElementById('" + divmsgbox.ClientID + "');if (hide.innerHTML != '') {hide.className = \"msgbox msgbox-bx-n-z-n  " + background + " animated fadeInRight-dn\";";
        scripttag = scripttag + " setTimeout(clear, 5000);}}function clear() {var hide = document.getElementById('" + divmsgbox.ClientID + "');";
        scripttag = scripttag + " hide.className = '';hide.innerHTML = '';}function jscript() {setTimeout(disable, 10000);}</script>";
        scripttag = scripttag + " <script>Sys.Application.add_load(jscript);</script>";

        ScriptManager.RegisterClientScriptBlock(ctrl, GetType(), "dd", scripttag, false);
        ScriptManager.RegisterClientScriptBlock(ctrl, GetType(), "ee", "enable();", true);
    }
    public string LoadLoader(Control parentId)
    {
        var msg = "";
        try
        {
            const string path = "~/admin/usercontrol/loader.ascx";
            var uc = LoadControl(path);
            parentId.Controls.Add(uc);
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }

        return msg;
    }


    public static string GenerateRandomNo(int Length = 6)
    {
        string Result = string.Empty;
        char[] chars = "0123456789".ToCharArray();
        Random random;
        for (int i = 0; i < Length; i++)
        {
            random = new Random();
            int x = random.Next(1, chars.Length);
            //---Don't Allow Repetition of Characters
            if (!Result.Contains(chars.GetValue(x).ToString())) Result += chars.GetValue(x);
            else i--;
            random = null;
        }
        return Result;
    }


    public static readonly Campus CampusInstance = new Campus();

    private string _sql = "";
    public string FindRecieptNo()
    {
        var xx = "";
        string _sql2 = "select top(1) StartType from ReceiptNoStart where BranchCode=" + Session["BranchCode"] + " order by id desc";
        if (Duplicate(_sql2))
        {
            _sql = "select  count(*) cnt  from CollegeMaster where branchCode=" + Session["BranchCode"].ToString() + "";
            if (ReturnTag(_sql, "cnt") == "0" || ReturnTag(_sql, "cnt") == "0")
            {
                return "";
            }
            _sql = "select count(*) cnt from ReceiptNoStart where branchCode=" + Session["BranchCode"].ToString() + "";
            if (ReturnTag(_sql, "cnt") == "0" || ReturnTag(_sql, "cnt") == "0")
            {
                return "";
            }
            var recieptno = "";
            _sql = "select CollegeShortNa from CollegeMaster where branchCode=" + Session["BranchCode"].ToString() + "";
            if (Duplicate(_sql))
            {
                recieptno = ReturnTag(_sql, "CollegeShortNa") + "/" + Session["SessionName"] + "/";
            }
            else
            {
                return "";
            }

            int co = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = BAL.objBal.dbGet_connection().ConnectionString;
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "GetreceiptNo";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString().Trim());
                        cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString().Trim());
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        cmd.Parameters.Clear();
                        if (dt.Rows.Count > 0)
                        {
                            co = Convert.ToInt32(dt.Rows[0][0].ToString());
                        }
                    }
                }

            }
            catch (Exception) { co = 0; dbGet_connection().Close(); }
            xx = IdGeneration(recieptno, co.ToString());
        }
        return xx;
    }

    public string IdGeneration(string fixedString, string x)
    {
        string xx;
        switch (x.Length)
        {
            case 1:
                xx = fixedString + "00000000" + x;
                break;
            case 2:
                xx = fixedString + "0000000" + x;
                break;
            case 3:
                xx = fixedString + "000000" + x;
                break;
            case 4:
                xx = fixedString + "00000" + x;
                break;
            case 5:
                xx = fixedString + "0000" + x;
                break;
            case 6:
                xx = fixedString + "000" + x;
                break;
            case 7:
                xx = fixedString + "00" + x;
                break;
            case 8:
                xx = fixedString + "0" + x;
                break;
            default:
                xx = fixedString + x;
                break;
        }
        return xx;
    }

    public int Insert_Update_Delete1(string sql)
    {
        int i = 0;
        try
        {
            con = dbGet_connection();
            cmd = new SqlCommand(sql, con);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            i = cmd.ExecuteNonQuery();
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        catch
        { }
        return i;
    }

    public DataTable Fetchdata(string sql)
    {
        con = dbGet_connection();
        DataTable dt = new DataTable();
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        da = new SqlDataAdapter(sql, con);
        da.Fill(dt);
        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
        return dt;  
    }
    public SqlDataReader fetch_data(string sql)
    {
        con = dbGet_connection();
        SqlDataReader dr=null;
        SqlCommand cmd = new SqlCommand(sql,con); 
        if (con.State==ConnectionState.Closed)
        {
            con.Open();
        }
        dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        return dr;
    }
    public void AddFieldsInTableStringOrNumeric(string TableName, string FieldName, string Datatype, string size)
    {
        string sql = "";
        sql = "ALTER TABLE " + TableName + " ADD " + FieldName + " " + Datatype + "(" + size + ") NULL";
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = sql;
        cmd.CommandType = CommandType.Text;
        con = dbGet_connection();cmd.Connection = con;
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

        }
        catch (SqlException) { con.Close(); }

    }
    public void AddFieldsInTableInt(string TableName, string FieldName, string Datatype)
    {
        string sql = "";
        sql = "ALTER TABLE " + TableName + " ADD " + FieldName + " " + Datatype +  " NULL";
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = sql;
        cmd.CommandType = CommandType.Text;
        con = dbGet_connection();cmd.Connection = con;
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

        }
        catch (SqlException) { con.Close(); }

    } 
    public void AddFieldsInTableIntOrDatetime(string TableName, string FieldName, string Datatype, string size)
    {
        string sql = "";
        sql = "ALTER TABLE " + TableName + " ADD " + FieldName + " " + Datatype + "(" + size + ") NULL";
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = sql;
        cmd.CommandType = CommandType.Text;
        con = dbGet_connection();cmd.Connection = con;
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

        }
        catch (SqlException) { con.Close(); }

    }
    public int ProcedureDatabase(string ProcedureQuery)
    {
        int i = 0;
        string sql = "";
        sql = ProcedureQuery;
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = sql;
        cmd.CommandType = CommandType.Text;
        con = dbGet_connection();cmd.Connection = con;
        try
        {
            con.Open();
            i=cmd.ExecuteNonQuery();
            con.Close();

        }
        catch (SqlException) { con.Close(); }
        catch (Exception) { con.Close(); }
        return i;
    }
    public Boolean Duplicate(string qry)
    {
        int co = 0;
        Boolean flag = false;           
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.CommandText = qry;
            con = dbGet_connection();cmd.Connection = con;
            con.Open();                      
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dr.Read())
            {
                int ro = dr.FieldCount;                
                if (ro > 1)
                {
                    if (dr[1] != DBNull.Value)
                    {
                        co++;
                    }
                }
                else
                {
                    co++;
                }
            }
            con.Close();
        }
        catch (SqlException)
        {
            con.Close();
        }

        if (co >= 1)
        {
            flag = true;
        }
        return flag;
    }
    public SqlDataReader Duplicate1(string qry)
    {
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr=null;
        try
        {
            cmd.CommandText = qry;
           
            con = dbGet_connection();cmd.Connection = con;
            con.Open();
            dr = cmd.ExecuteReader();

            con.Close();
            
        }
            
        catch (SqlException)
        {
            con.Close();
        }
        return dr;
    }
    public void FillCheckBox(string qry, CheckBoxList chk, string ColumnName)
    {
        SqlCommand cmd = new SqlCommand();
        try
        {
           
            cmd.CommandText = qry;
            SqlDataReader dr;

            con = dbGet_connection();cmd.Connection = con;
            con.Open();
            dr = cmd.ExecuteReader();

            chk.Items.Clear();

            while (dr.Read())
            {

                chk.Items.Add(dr[ColumnName].ToString());
            }
            con.Close();
        }
        catch (SqlException)
        {
            con.Close();
        }
    }
    public void FillCheckBox_withValue(string qry, CheckBoxList drp, string ColumnName, string ColumnValue)
    {
        string ss = "";
        SqlCommand cmd = new SqlCommand();
        drp.Items.Clear();
        try
        {
            ss = qry;
            cmd.CommandText = ss;
            SqlDataReader dr;
            con = dbGet_connection(); cmd.Connection = con;
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {

                drp.Items.Add(new System.Web.UI.WebControls.ListItem(dr[ColumnName].ToString(), dr[ColumnValue].ToString()));
            }
            con.Close();
        }
        catch (SqlException ex)
        {
            con.Close();
        }
        catch (Exception exs) { }
    }
    public void FillDropDown_withValue(string qry, DropDownList drp, string ColumnName, string ColumnValue)
    {
        string ss = "";
        SqlCommand cmd = new SqlCommand();
        drp.Items.Clear();
        try
        {
            ss= qry;
            cmd.CommandText = ss;
            SqlDataReader dr;            
            con = dbGet_connection();cmd.Connection = con;
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                
                drp.Items.Add(new System.Web.UI.WebControls.ListItem(dr[ColumnName].ToString(), dr[ColumnValue].ToString()));
            }
            con.Close();
        }
        catch (SqlException ex)
        {
            con.Close();
        }
        catch (Exception exs) { }
    }

    public void FillDropDown_withValue_withSelect(string qry, DropDownList drp, string ColumnName, string ColumnValue)
    {
        string ss = "";
        SqlCommand cmd = new SqlCommand();
        drp.Items.Clear();
        try
        {
            ss = qry;
            cmd.CommandText = "Select '<--Select-->' " + ColumnName + ",-1 " + ColumnValue + " Union " + ss;
            SqlDataReader dr;
            con = dbGet_connection();cmd.Connection = con;
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                drp.Items.Add(new System.Web.UI.WebControls.ListItem(dr[ColumnName].ToString(), dr[ColumnValue].ToString()));
            }
            con.Close();
        }
        catch (SqlException)
        {
            con.Close();
        }
        catch (Exception) { }
    }

    public void fillSelectvalue(DropDownList drp, string value)
    {
        drp.Items.Insert(0, new System.Web.UI.WebControls.ListItem(value, value));
    }
    public void fillSelectvalue(DropDownList drp,string displayText, string value)
    {
        drp.Items.Insert(0, new System.Web.UI.WebControls.ListItem(displayText, value));
    }

    public void FillDropDown(string qry, DropDownList drp, string ColumnName)
    {
        string ss = "";
        SqlCommand cmd = new SqlCommand();
        drp.Items.Clear();
        try
        {
            //ss = "Select '<--Select-->' as " + ColumnName + " Union ";
            ss = ss + qry;
            cmd.CommandText = ss;
            SqlDataReader dr;

            con = dbGet_connection();cmd.Connection = con;
            con.Open();
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                drp.Items.Add(dr[ColumnName].ToString());
            }
            con.Close();
        }
        catch (SqlException ex)
        {
            con.Close();
        }
        catch (Exception exs) { }
    }
    public void FillDropDownWithOutSelect(string qry, DropDownList drp, string ColumnName)
    {
        SqlCommand cmd = new SqlCommand();
        drp.Items.Clear();
        try
        {

            cmd.CommandText = qry;
            SqlDataReader dr;

            con = dbGet_connection();cmd.Connection = con;
            con.Open();
            dr = cmd.ExecuteReader();



            while (dr.Read())
            {

                drp.Items.Add(dr[ColumnName].ToString());
            }
            con.Close();
        }
        catch (SqlException)
        {
            con.Close();
        }
        catch (Exception) { }
    }
    public string ReturnTag(string qry, string ColumnNameValue)
    {
        SqlCommand cmd = new SqlCommand();
        string ss = "";
        try
        {
            cmd.CommandText = qry;
            SqlDataReader dr;

            con = dbGet_connection();cmd.Connection = con;

            try
            {
                con.Open();
            }
            catch (Exception) { con.Close(); con.Open(); }


            dr = cmd.ExecuteReader();


            while (dr.Read())
            {

                ss = dr[ColumnNameValue].ToString();
            }
            con.Close();
        }
        catch (SqlException)
        {
            con.Close();
        }

        catch (Exception) { con.Close(); }
        return ss;
    }
    public Byte[] ReturnImageByte(string qry, string ColumnNameValue)
    {
        SqlCommand cmd = new SqlCommand();
        Byte[] ss = null;
        try
        {
            cmd.CommandText = qry;
            SqlDataReader dr;

            con = dbGet_connection();cmd.Connection = con;

            try
            {
                con.Open();
            }
            catch (Exception) { con.Close(); con.Open(); }


            dr = cmd.ExecuteReader();


            while (dr.Read())
            {

                ss = (byte[])dr[ColumnNameValue];
            }
            con.Close();
        }
        catch (SqlException)
        {
            con.Close();
        }

        catch (Exception) { con.Close(); }
        return ss;
    }
    public DataSet GridFill(string qry)
    {
        con = dbGet_connection();
        bool result = false;
        SqlDataAdapter sd = new SqlDataAdapter(qry, con);
        DataSet ds = new DataSet();
        try
        {

            con.Open();
            sd.Fill(ds, "#temp");

            con.Close();
            result = true;
        }
        catch (SqlException) { con.Close(); }
        catch (Exception) { con.Close(); }
        if (result)
        {
            return ds;
        }
        else
        {
            return null;
        }
    }
    
    public string GetUrl(string page, string requesturl)
    {
        string[] splits = requesturl.Split('/');
        if (splits.Length >= 2)
        {
            string url = splits[0] + "//";
            for (int i = 2; i < splits.Length - 1; i++)
            {
                url += splits[i];
                url += "/";
            }
            return url + page;
        }
        return page;
    }
    public void MonthDropDown(DropDownList dropDownYear, DropDownList drpDownMonth, DropDownList DropDownDate)
    {
        int yy, i;

        if (drpDownMonth.SelectedItem.ToString() == "Jan" || drpDownMonth.SelectedItem.ToString() == "Mar" || drpDownMonth.SelectedItem.ToString() == "May" || drpDownMonth.SelectedItem.ToString() == "Jul" || drpDownMonth.SelectedItem.ToString() == "Aug" || drpDownMonth.SelectedItem.ToString() == "Oct" || drpDownMonth.SelectedItem.ToString() == "Dec")
        {
            DropDownDate.Items.Clear();
            for (i = 1; i <= 31; i++)
            {

                DropDownDate.Items.Add(i.ToString());
            }
        }
        else if (drpDownMonth.SelectedItem.ToString() == "Apr" || drpDownMonth.SelectedItem.ToString() == "Jun" || drpDownMonth.SelectedItem.ToString() == "Sep" || drpDownMonth.SelectedItem.ToString() == "Nov")
        {
            DropDownDate.Items.Clear();
            for (i = 1; i <= 30; i++)
            {
                DropDownDate.Items.Add(i.ToString());
            }

        }
        else
        {
            yy = Convert.ToInt32(dropDownYear.SelectedItem.ToString());
            yy = yy % 4;
            if (yy == 0 && drpDownMonth.SelectedItem.ToString() == "Feb")
            {
                DropDownDate.Items.Clear();
                for (i = 1; i <= 29; i++)
                {
                    DropDownDate.Items.Add(i.ToString());
                }

            }
            else
            {

                DropDownDate.Items.Clear();
                for (i = 1; i < 29; i++)
                {
                    DropDownDate.Items.Add(i.ToString());
                }


            }



        }
    }
    public void YearDropDown(DropDownList dropDownYear, DropDownList drpDownMonth, DropDownList DropDownDate)
    {
        int yy, i;
        yy = Convert.ToInt32(dropDownYear.SelectedItem.ToString());
        yy = yy % 4;
        if (yy == 0 && drpDownMonth.SelectedItem.ToString() == "Feb")
        {
            DropDownDate.Items.Clear();
            for (i = 1; i <= 29; i++)
            {
                DropDownDate.Items.Add(i.ToString());
            }

        }
        else if (yy != 0 && drpDownMonth.SelectedItem.ToString() == "Jan" || drpDownMonth.SelectedItem.ToString() == "Mar" || drpDownMonth.SelectedItem.ToString() == "May" || drpDownMonth.SelectedItem.ToString() == "Jul" || drpDownMonth.SelectedItem.ToString() == "Aug" || drpDownMonth.SelectedItem.ToString() == "Oct" || drpDownMonth.SelectedItem.ToString() == "Dec")
        {
            DropDownDate.Items.Clear();
            for (i = 1; i <= 31; i++)
            {
                DropDownDate.Items.Add(i.ToString());
            }

        }


        else if (yy != 0 && drpDownMonth.SelectedItem.ToString() == "Apr" || drpDownMonth.SelectedItem.ToString() == "Jun" || drpDownMonth.SelectedItem.ToString() == "Sep" || drpDownMonth.SelectedItem.ToString() == "Nov")
        {
            DropDownDate.Items.Clear();
            for (i = 1; i <= 30; i++)
            {
                DropDownDate.Items.Add(i.ToString());
            }

        }


        else if (yy != 0 && drpDownMonth.SelectedItem.ToString() == "Feb")
        {
            DropDownDate.Items.Clear();
            for (i = 1; i <= 28; i++)
            {
                DropDownDate.Items.Add(i.ToString());
            }

        }

    }
    public void AddDateMonthYearDropDown(DropDownList dropDownYear, DropDownList drpDownMonth, DropDownList DropDownDate)
    {

        int i;
        string yy;
        int totalDays = int.Parse(ReturnTag("select DAY(EOMONTH(convert(datetime,getdate(),103))) totalDays", "totalDays"));
        if (DropDownDate.Items.Count == 0)
        {
            for (i = 1; i <= totalDays; i++)
            {
                DropDownDate.Items.Add(i.ToString());
            }

        }
        if (drpDownMonth.Items.Count == 0)
        {

            drpDownMonth.Items.Add("Jan");
            drpDownMonth.Items.Add("Feb");
            drpDownMonth.Items.Add("Mar");
            drpDownMonth.Items.Add("Apr");
            drpDownMonth.Items.Add("May");
            drpDownMonth.Items.Add("Jun");
            drpDownMonth.Items.Add("Jul");
            drpDownMonth.Items.Add("Aug");
            drpDownMonth.Items.Add("Sep");
            drpDownMonth.Items.Add("Oct");
            drpDownMonth.Items.Add("Nov");
            drpDownMonth.Items.Add("Dec");


        }
        if (dropDownYear.Items.Count == 0)
        {
            yy = ReturnTag("Select Year(getdate())+1 as YearYY ", "YearYY");

            for (i = 1900; i <= Convert.ToInt32(yy) + 1; i++)
            {
                dropDownYear.Items.Add(i.ToString());
            }
        }


        // End




    }
public void AddDateMonthYearDropDownBasedOfDay(DropDownList dropDownYear, DropDownList drpDownMonth, DropDownList DropDownDate,string dates)
    {

        int i;
        string yy;
        int totalDays = int.Parse(ReturnTag("select DAY(EOMONTH(convert(datetime,'"+ dates + "',103))) totalDays", "totalDays"));
        if (DropDownDate.Items.Count == 0 || DropDownDate.Items.Count < totalDays)
        {
            for (i = 1; i <= totalDays; i++)
            {
                DropDownDate.Items.Add(i.ToString());
            }

        }
        if(DropDownDate.Items.Count == 0)
        if (drpDownMonth.Items.Count == 0)
        {

            drpDownMonth.Items.Add("Jan");
            drpDownMonth.Items.Add("Feb");
            drpDownMonth.Items.Add("Mar");
            drpDownMonth.Items.Add("Apr");
            drpDownMonth.Items.Add("May");
            drpDownMonth.Items.Add("Jun");
            drpDownMonth.Items.Add("Jul");
            drpDownMonth.Items.Add("Aug");
            drpDownMonth.Items.Add("Sep");
            drpDownMonth.Items.Add("Oct");
            drpDownMonth.Items.Add("Nov");
            drpDownMonth.Items.Add("Dec");


        }
        if (dropDownYear.Items.Count == 0)
        {
            yy = ReturnTag("Select Year(getdate())+1 as YearYY ", "YearYY");

            for (i = 1900; i <= Convert.ToInt32(yy) + 1; i++)
            {
                dropDownYear.Items.Add(i.ToString());
            }
        }


        // End




    }
    string msg = "";
    public void EmailSending(string Mess, string subjectParameter, string emailId,string messg)
    {
        msg = "";
        string sql = "";
        MailMessage mail = new MailMessage();
        mail.To.Add(emailId);//to ID
        string fromID="", Pass = "";
        sql = "Select Email,Password from EmailPanelSetting where Id=1 and branchCode="+Session["BranchCode"].ToString()+"";
        fromID = ReturnTag(sql, "Email");
        Pass =  ReturnTag(sql, "Password");
      
        mail.From = new MailAddress(fromID);
        mail.Subject = subjectParameter;

        mail.Body = Mess;
        mail.IsBodyHtml = true;
        SmtpClient smtp = new SmtpClient();
        smtp.Host = "smtp.gmail.com"; //Or Your SMTP Server Address
        smtp.Port = 587;
        smtp.Credentials = new System.Net.NetworkCredential(fromID , Pass );//from id
        //Or your Smtp Email ID and Password
        smtp.EnableSsl = true;
        try
        {
            smtp.Send(mail);
            msg = messg;
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
    }

    public string SendEmailInBackgroundThread(string Mess, string subjectParameter, string emailId,string messg)
    {
        try
        {
            Thread bgThread = new System.Threading.Thread(() => EmailSending(Mess, subjectParameter, emailId, messg));
            bgThread.IsBackground = true;
            bgThread.Start();
        }
        catch
        {
        }

        return msg;
    }

    public bool EmailSendingForUser(string Mess, string subjectParameter, string emailId)
    {
        bool flag = true;
        MailMessage mail = new MailMessage();
        mail.To.Add(emailId);//to ID
        string fromID = "", Pass = "";
      
        fromID = "donotreply@eam.co.in";
        Pass = "reNply_33@9D";

        mail.From = new MailAddress(fromID);
        mail.Subject = subjectParameter;



        mail.Body = Mess;
        mail.IsBodyHtml = true;
        SmtpClient smtp = new SmtpClient();
        smtp.Host = "smtp.gmail.com"; //Or Your SMTP Server Address
        smtp.Port = 587;
        smtp.Credentials = new System.Net.NetworkCredential(fromID, Pass);//from id
        //Or your Smtp Email ID and Password
        smtp.EnableSsl = true;
        try
        {
            smtp.Send(mail);
        }
        catch (Exception) { flag = false; }
        return flag;
    }
    public string Date_in_Words(long Rs)
    {
        string functionReturnValue = null;
        string char1 = null;
        //Dim rs As Long
        long t = 0;
        string[] digit = new string[20];
        string[] tens = new string[11];
        //digit
        digit[1] = "One";
        digit[2] = "Two";
        digit[3] = "Three";
        digit[4] = "Four";
        digit[5] = "Five";
        digit[6] = "Six";
        digit[7] = "Seven";
        digit[8] = "Eight";
        digit[9] = "Nine";
        digit[10] = "Ten";
        digit[11] = "Eleven";
        digit[12] = "Twelve";
        digit[13] = "Thirteen";
        digit[14] = "Fourteen";
        digit[15] = "Fifteen";
        digit[16] = "Sixteen";
        digit[17] = "Seventeen";
        digit[18] = "Eighteen";
        digit[19] = "Nineteen";

        //tens value
        tens[2] = "Twenty";
        tens[3] = "Thirty";
        tens[4] = "Fourty";
        tens[5] = "Fifty";
        tens[6] = "Sixty";
        tens[7] = "Seventy";
        tens[8] = "Eighty";
        tens[9] = "Ninety";

        while ((Rs > 0))
        {

            if (Rs < 20)
            {
                char1 = char1 + " " + digit[Rs];
                Rs = Rs - Rs;
            }
            else if ((Rs < 100))
            {
                t = Convert.ToInt32((Rs / 10));
                char1 = char1 + " " + tens[t];
                Rs = Rs - (t * 10);
            }
            else if ((Rs < 1000))
            {
                t = Convert.ToInt32((Rs / 100));
                char1 = char1 + " " + digit[t] + " Hundred";
                Rs = Rs - (t * 100);

            }
            else if ((Rs < 100000))
            {
                if ((Rs > 20000))
                {
                    t = Convert.ToInt32((Rs / 10000));
                    char1 = char1 + " " + tens[t];
                    Rs = Rs - (t * 10000);
                }
                else
                {
                    if ((Rs >= 1000))
                    {
                        t = Convert.ToInt32(Rs / 1000);
                        char1 = char1 + " " + digit[t] + " Thousand";
                        Rs = Rs - (t * 1000);
                    }
                    else
                    {
                        char1 = char1 + " " + "Thousand";
                    }

                }
                //
            }
            else if ((Rs < 10000000))
            {
                if ((Rs >= 2000000))
                {
                    t = Convert.ToInt32((Rs / 1000000));
                    char1 = char1 + " " + tens[t];
                    Rs = Rs - (t * 1000000);
                }
                else
                {
                    if ((Rs >= 100000))
                    {
                        t = Convert.ToInt32((Rs / 100000));
                        char1 = char1 + " " + digit[t] + " Lacs";
                        Rs = Rs - (t * 100000);
                    }
                    else
                    {
                        char1 = char1 + " " + "Lacs";
                    }
                }
            }
            else
            {
                functionReturnValue = "Out Of Range...";
                System.Environment.Exit(0);

            }
        }
        if (string.IsNullOrEmpty(char1))
        {
            functionReturnValue = "Zero";
        }
        else
        {
            functionReturnValue = char1;
        }
        return functionReturnValue;
    }

    public string NumberToString(decimal Rs)
    {
        string words = null; string conwords = null;
        string _sql = "Select top(1) CurrencyName from setting";
        string CurrencyName = ReturnTag(_sql, "CurrencyName");
        if (CurrencyName == "")
        {
            return conwords.ToString();
        }
        else
        {
            string[] rs = Rs.ToString("0.00").Split(new string[] {"." }, StringSplitOptions.None);
            words = NumberToString2(long.Parse(rs[0]), rs[1]);
        }
          
        if (CurrencyName.ToLower() == "rs" || CurrencyName.ToLower() == "rupees")
        {
            string ss = (words).ToString().ToLower();
            conwords = FirstCharToUpper(ss);
            return conwords;
        }
        else
        {
            string ss = (words + " " + CurrencyName + " Only").ToString().ToLower();
            conwords = FirstCharToUpper(ss);
            return conwords;
        }
    }
    public static string FirstCharToUpper(string input)
    {
        string res = "";
        string[] data = input.Split(new string[] { " " }, StringSplitOptions.None);
        for (int i = 0; i < data.Length; i++)
        {
            string s = "";
            s = data[i];
            if (string.IsNullOrEmpty(s))
            {
                res = res + " ";
            }
            else
            {
                char[] a = s.ToCharArray();
                a[0] = char.ToUpper(a[0]);

                res = res + new string(a) + " ";
            }
        }
        return res;
    }
       public string NumberToString2(long Rs, string paisas)
    {
        string decimals = "";
        string functionReturnValue = null;
        string _sql = "Select top(1) CurrencyName from setting";
        string CurrencyName = ReturnTag(_sql, "CurrencyName");
        if (CurrencyName == "")
        {
            return functionReturnValue.ToString();
        }
        if (CurrencyName.ToLower() == "rs" || CurrencyName.ToLower() == "rupees")
        {

            string char1 = null;
            //Dim rs As Long
            long t = 0;
            string[] digit = new string[20];
            string[] tens = new string[11];
            //digit
            digit[1] = "One";
            digit[2] = "Two";
            digit[3] = "Three";
            digit[4] = "Four";
            digit[5] = "Five";
            digit[6] = "Six";
            digit[7] = "Seven";
            digit[8] = "Eight";
            digit[9] = "Nine";
            digit[10] = "Ten";
            digit[11] = "Eleven";
            digit[12] = "Twelve";
            digit[13] = "Thirteen";
            digit[14] = "Fourteen";
            digit[15] = "Fifteen";
            digit[16] = "Sixteen";
            digit[17] = "Seventeen";
            digit[18] = "Eighteen";
            digit[19] = "Nineteen";

            //tens value
            tens[2] = "Twenty";
            tens[3] = "Thirty";
            tens[4] = "Fourty";
            tens[5] = "Fifty";
            tens[6] = "Sixty";
            tens[7] = "Seventy";
            tens[8] = "Eighty";
            tens[9] = "Ninety";
            if (double.Parse(paisas) > 0)
            {
                decimals = paisas;
            }
            while ((Rs > 0))
            {

                if (Rs < 20)
                {
                    char1 = char1 + " " + digit[Rs];
                    Rs = Rs - Rs;
                }
                else if ((Rs < 100))
                {
                    t = Convert.ToInt32((Rs / 10));
                    char1 = char1 + " " + tens[t];
                    Rs = Rs - (t * 10);
                }
                else if ((Rs < 1000))
                {
                    t = Convert.ToInt32((Rs / 100));
                    char1 = char1 + " " + digit[t] + " Hundred";
                    Rs = Rs - (t * 100);

                }
                else if ((Rs < 100000))
                {
                    if ((Rs == 20000))
                    {
                        t = Convert.ToInt32((Rs / 10000));
                        char1 = char1 + " " + tens[t] + " Thousand";
                        Rs = Rs - (t * 10000);
                    }
                    else if ((Rs > 20000))
                    {
                        t = Convert.ToInt32((Rs / 10000));
                        char1 = char1 + " " + tens[t]; //+ " Thousand";
                        Rs = Rs - (t * 10000);
                        if (Rs == 0)
                        {
                            char1 = char1 + " " + "Thousand";
                        }
                    }
                    else
                    {
                        if ((Rs >= 1000))
                        {
                            t = Convert.ToInt32(Rs / 1000);
                            char1 = char1 + " " + digit[t] + " Thousand";
                            Rs = Rs - (t * 1000);
                        }
                        else
                        {
                            char1 = char1 + " " + "Thousand";
                        }

                    }
                    //
                }
                else if ((Rs < 10000000))
                {
                    if ((Rs == 2000000))
                    {
                        t = Convert.ToInt32((Rs / 1000000));
                        char1 = char1 + " " + tens[t] + " Lacs";
                        Rs = Rs - (t * 1000000);
                    }
                    else if ((Rs > 2000000))
                    {
                        t = Convert.ToInt32((Rs / 1000000));
                        char1 = char1 + " " + tens[t];// + " Lacs";
                        Rs = Rs - (t * 1000000);
                        if (Rs == 0)
                        {
                            char1 = char1 + " " + "Lacs";
                        }
                    }
                    else
                    {
                        if ((Rs >= 100000))
                        {
                            t = Convert.ToInt32((Rs / 100000));
                            char1 = char1 + " " + digit[t] + " Lacs";
                            Rs = Rs - (t * 100000);
                        }
                        else
                        {
                            char1 = char1 + " " + "Lacs";
                        }
                    }
                }
                else
                {
                    functionReturnValue = "Out Of Range...";
                    System.Environment.Exit(0);

                }
            }
            if (string.IsNullOrEmpty(char1))
            {
                functionReturnValue = "Zero";
            }
            else
            {
                string paisa = "";
                if (decimals != "")
                {
                    long paisa1 = long.Parse(decimals);

                    while ((paisa1 > 0))
                    {

                        if (paisa1 < 20)
                        {
                            paisa = paisa + " " + digit[paisa1];
                            paisa1 = paisa1 - paisa1;
                        }
                        else if ((paisa1 < 100))
                        {
                            t = Convert.ToInt32((paisa1 / 10));
                            paisa = paisa + " " + tens[t];
                            paisa1 = paisa1 - (t * 10);
                        }
                    }
                }

                if (paisa != "")
                {
                    functionReturnValue = char1 + " " + CurrencyName + " and " + paisa + " Paisa Only.";
                }
                else
                {
                    functionReturnValue = char1 + " " + CurrencyName + " Only.";
                }

            }
        }
        else
        {

            if (Rs == 0) return "ZERO";
            if (Rs < 0) return "minus " + NumberToString2(Math.Abs(Rs), "");
            if ((Rs / 1000000) > 0)
            {
                functionReturnValue += NumberToString2(Rs / 100000, "") + " LAKES ";
                Rs %= 1000000;
            }
            if ((Rs / 1000) > 0)
            {
                functionReturnValue += NumberToString2(Rs / 1000, "") + " THOUSAND ";
                Rs %= 1000;
            }
            if ((Rs / 100) > 0)
            {
                functionReturnValue += NumberToString2(Rs / 100, "") + " HUNDRED ";
                Rs %= 100;
            }
            //if ((number / 10) > 0)  
            //{  
            // words += ConvertNumbertoWords(number / 10) + " RUPEES ";  
            // number %= 10;  
            //}  
            if (Rs > 0)
            {
                // if (words != "") words += "AND ";
                var unitsMap = new[]
                {
            "ZERO", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE", "TEN", "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN", "SEVENTEEN", "EIGHTEEN", "NINETEEN"
        };
                var tensMap = new[]
                {
            "ZERO", "TEN", "TWENTY", "THIRTY", "FORTY", "FIFTY", "SIXTY", "SEVENTY", "EIGHTY", "NINETY"
        };
                if (Rs < 20) functionReturnValue += unitsMap[Rs];
                else
                {
                    functionReturnValue += tensMap[Rs / 10];
                    if ((Rs % 10) > 0) functionReturnValue += " " + unitsMap[Rs % 10];
                }
            }
        }
        return functionReturnValue;

    }


    public string convertRomantostring(string classinroman)
    {
        string classinwords="";

        if (classinroman == "I st" || classinroman == "Ist" || classinroman == "I" || classinroman == "1st" || classinroman == "1")
        {
            classinwords="First";
        }
        if (classinroman == "II nd" || classinroman == "IInd" || classinroman == "II" || classinroman == "2nd" || classinroman == "2")
        {
            classinwords="Second";
        }
        if (classinroman == "III rd" || classinroman == "IIIrd" || classinroman == "III" || classinroman == "3rd" || classinroman == "3")
        {
            classinwords="Third";
        }
        if (classinroman == "IV rth" || classinroman == "IVrth" || classinroman == "IV" || classinroman == "4rth" || classinroman == "4")
        {
           classinwords="Fourth";
        }
        if (classinroman == "V th" || classinroman == "Vth" || classinroman == "V" || classinroman == "5th" || classinroman == "5")
        {
            classinwords="Fifth";
        }
        if (classinroman == "VI th" || classinroman == "VIth" || classinroman == "VI" || classinroman == "6th" || classinroman == "6")
        {
            classinwords = "Sixth";
        }
        if (classinroman == "VII th" || classinroman == "VIIth" || classinroman == "VII" || classinroman == "7th" || classinroman == "7")
        {
            classinwords = "Seventh";
        }
        if (classinroman == "VIII rth" || classinroman == "VIIIrth" || classinroman == "VIII" || classinroman == "8rth" || classinroman == "8")
        {
            classinwords = "Eight";
        }
        if (classinroman == "IX th" || classinroman == "IXth" || classinroman == "IX" || classinroman == "9th" || classinroman == "9")
        {
            classinwords = "Ninth";
        }
        if (classinroman == "X th" || classinroman == "Xth" || classinroman == "X" || classinroman == "10th" || classinroman == "10")
        {
            classinwords = "Tenth";
        }
        if (classinroman.Contains("XI ") || classinroman == "XI th" || classinroman == "XIth" || classinroman == "XI" || classinroman == "11th" || classinroman == "11")
        {
            classinwords = "Eleventh";
        }
        if (classinroman.Contains("XII ") || classinroman == "XII th" || classinroman == "XIIth" || classinroman == "XII" || classinroman == "12th" || classinroman == "12")
        {
            classinwords = "Twelfth";
        }
        if(classinwords==string.Empty)
        {
            classinwords = classinroman;
        }

        return classinwords;
    }

    public void MessageBox(string msg, Page xx)
    {
        Label lbl = new Label();
        lbl.Text = "<script> smoke.alert('" + msg + "',{},'')</script>";  //Page.Controls.Add(lbl);
        xx.Controls.Add(lbl);

    }
    public void MessageBoxforUpdatePanel(string msg, Control xx)
    {

        string message = "smoke.alert('" + msg + "',{},'')";
        ScriptManager.RegisterClientScriptBlock(xx, this.GetType(), "alert", message, true);
    }

    public void MessageBoxwithfocuscontrol(string msg, Control coltrolid, string focuscoltrolid)
    {

        string message = "smoke.alert('" + msg + "',{},'" + focuscoltrolid + "')";
        ScriptManager.RegisterClientScriptBlock(coltrolid, this.GetType(), "alert", message, true);
    }

    public void clearTextBoxbyJavaScript(string txtlist, Control xx)
    {
        ScriptManager.RegisterStartupScript(xx, this.GetType(), "alert", "EmptyTextBox('" + txtlist + "');", true);
    }

    public string Isnull(string DataValue)
    {
        if (DataValue == null)
        {
            return " ";
        }
        else
        {
            return DataValue;
        }
    }
    public void FindCurrentDateandSetinDropDown(DropDownList DrpYear, DropDownList DrpMonth, DropDownList DrpDate)
    {
        string dd = "", mm = "", yy = "";


        dd = ReturnTag("Select day(getdate()) as DateDD", "DateDD");
        mm = ReturnTag("Select Month(getdate())as MonthMM", "MonthMM");
        yy = ReturnTag("Select Year(getdate()) as YearYY ", "YearYY");

        DrpYear.Text = yy;
        if (mm == "1")
        {
            DrpMonth.Text = "Jan";
        }
        else if (mm == "2")
        {
            DrpMonth.Text = "Feb";
        }
        else if (mm == "3")
        {
            DrpMonth.Text = "Mar";
        }
        else if (mm == "4")
        {
            DrpMonth.Text = "Apr";
        }
        else if (mm == "5")
        {
            DrpMonth.Text = "May";
        }
        else if (mm == "6")
        {
            DrpMonth.Text = "Jun";

        }
        else if (mm == "7")
        {
            DrpMonth.Text = "Jul";
        }
        else if (mm == "8")
        {
            DrpMonth.Text = "Aug";
        }
        else if (mm == "9")
        {
            DrpMonth.Text = "Sep";
        }
        else if (mm == "10")
        {
            DrpMonth.Text = "Oct";
        }
        else if (mm == "11")
        {
            DrpMonth.Text = "Nov";
        }
        else if (mm == "12")
        {
            DrpMonth.Text = "Dec";
        }


        DrpDate.Text = dd;
    }

    public void swipeLabelText(Control parent)
    {
        foreach (Control _ChildControl in parent.Controls)
        {
            if ((_ChildControl.Controls.Count > 0))
            {
                swipeLabelText(_ChildControl);
            }
            else
            {
                if (_ChildControl is Label)
                {
                    string text = ((Label)_ChildControl).Text;
                    string sql = "Select replace from DefaultText where replacewith='" + text + "'";
                    if (ReturnTag(sql, "replace") != "")
                    {
                        ((Label)_ChildControl).Text = ReturnTag(sql, "replace");
                    }
                }
            }
        }

    }


    public void ClearControls(Control parent)
    {

        foreach (Control _ChildControl in parent.Controls)
        {
            if ((_ChildControl.Controls.Count > 0))
            {
                ClearControls(_ChildControl);
            }
            else
            {
                if (_ChildControl is TextBox)
                {
                    ((TextBox)_ChildControl).Text = string.Empty;
                }
                else if (_ChildControl is CheckBox)
                {
                    ((CheckBox)_ChildControl).Checked = false;
                }

            }
        }

    }

    public void ClearControls(Control parent, string txt1)
    {

        foreach (Control _ChildControl in parent.Controls)
        {
            if ((_ChildControl.Controls.Count > 0))
            {
                ClearControls(_ChildControl,txt1);
            }
            else
            {
                if (_ChildControl is TextBox)
                {
                    if (((TextBox)_ChildControl).FindControl(txt1)==null)
                    {
                        ((TextBox)_ChildControl).Text = string.Empty;
                    }
                    
                }
                else if (_ChildControl is CheckBox)
                {
                    ((CheckBox)_ChildControl).Checked = false;
                }

            }
        }

    }

    public void ClearControls(Control parent, string txt1, string txt2)
    {

        foreach (Control _ChildControl in parent.Controls)
        {
            if ((_ChildControl.Controls.Count > 0))
            {
                ClearControls(_ChildControl,txt1,txt2);
            }
            else
            {
                if (_ChildControl is TextBox)
                {
                    if (((TextBox)_ChildControl).FindControl(txt1) == null && ((TextBox)_ChildControl).FindControl(txt2) == null)
                    ((TextBox)_ChildControl).Text = string.Empty;
                }
                else if (_ChildControl is CheckBox)
                {
                    ((CheckBox)_ChildControl).Checked = false;
                }

            }
        }

    }

    public void ClearControls(Control parent, string txt1, string txt2,string txt3)
    {

        foreach (Control _ChildControl in parent.Controls)
        {
            if ((_ChildControl.Controls.Count > 0))
            {
                ClearControls(_ChildControl,txt1,txt2,txt3);
            }
            else
            {
                if (_ChildControl is TextBox)
                {
                    if (((TextBox)_ChildControl).FindControl(txt1) == null && ((TextBox)_ChildControl).FindControl(txt2) == null && ((TextBox)_ChildControl).FindControl(txt3) == null)
                    {
                        ((TextBox)_ChildControl).Text = string.Empty;
                    }
                }
                else if (_ChildControl is CheckBox)
                {
                    ((CheckBox)_ChildControl).Checked = false;
                }

            }
        }

    }

    public void ClearControls(Control parent, string txt1, string txt2, string txt3,string txt4)
    {

        foreach (Control _ChildControl in parent.Controls)
        {
            if ((_ChildControl.Controls.Count > 0))
            {
                ClearControls(_ChildControl,txt1,txt2,txt3,txt4);
            }
            else
            {
                if (_ChildControl is TextBox)
                {
                    if (((TextBox)_ChildControl).FindControl(txt1) == null && ((TextBox)_ChildControl).FindControl(txt2) == null && ((TextBox)_ChildControl).FindControl(txt3) == null && ((TextBox)_ChildControl).FindControl(txt4) == null)
                    {
                        ((TextBox)_ChildControl).Text = string.Empty;
                    }
                }
                else if (_ChildControl is CheckBox)
                {
                    ((CheckBox)_ChildControl).Checked = false;
                }

            }
        }

    }

    public void ClearControls(Control parent, string txt1,string txt2,string txt3,string txt4,string txt5)
    {

        foreach (Control _ChildControl in parent.Controls)
        {
            if ((_ChildControl.Controls.Count > 0))
            {
                ClearControls(_ChildControl,txt1,txt2,txt3,txt4,txt5);
            }
            else
            {
                if (_ChildControl is TextBox)
                {
                    if (((TextBox)_ChildControl).FindControl(txt1) == null && ((TextBox)_ChildControl).FindControl(txt2) == null && ((TextBox)_ChildControl).FindControl(txt3) == null && ((TextBox)_ChildControl).FindControl(txt4) == null && ((TextBox)_ChildControl).FindControl(txt5) == null)
                    {
                        ((TextBox)_ChildControl).Text = string.Empty;
                    }
                }
                else if (_ChildControl is CheckBox)
                {
                    ((CheckBox)_ChildControl).Checked = false;
                }

            }
        }

    }

    public void ReadOnlyControls(Control parent)
    {

        foreach (Control _ChildControl in parent.Controls)
        {
            if ((_ChildControl.Controls.Count > 0))
            {
                ReadOnlyControls(_ChildControl);
            }
            else
            {
                if (_ChildControl is TextBox)
                {
                    ((TextBox)_ChildControl).ReadOnly = true ;
                }
              

            }
        }

    }
    public void UnReadOnlyControls(Control parent)
    {

        foreach (Control _ChildControl in parent.Controls)
        {
            if ((_ChildControl.Controls.Count > 0))
            {
                UnReadOnlyControls(_ChildControl);
            }
            else
            {
                if (_ChildControl is TextBox)
                {
                    ((TextBox)_ChildControl).ReadOnly = false ;
                }
                

            }
        }

    }
    public string ReadDD(string dd)
    {
        if (dd.Substring(0, 1) == "0")
        {
            dd = dd.Substring(1, 1);
        }
        return dd;
    }
    public string CurrentMonthFind()
    {
        string mmm = "";
        string sql = "";
        int mo = 0;
        sql = "select month(getdate()) as mont";

        mo = Convert.ToInt32(ReturnTag(sql, "mont"));
        switch (mo)
        {
            case 1:
                mmm = "Jan";
                break;
            case 2:
                mmm = "Feb";
                break;
            case 3:
                mmm = "Mar";
                break;
            case 4:
                mmm = "Apr";
                break;
            case 5:
                mmm = "May";
                break;
            case 6:
                mmm = "Jun";
                break;
            case 7:
                mmm = "Jul";
                break;
            case 8:
                mmm = "Aug";
                break;
            case 9:
                mmm = "Sep";
                break;
            case 10:
                mmm = "Oct";
                break;
            case 11:
                mmm = "Nov";
                break;
            case 12:
                mmm = "Dec";
                break;

        }
        return mmm;
    }
    public int CurrentMonthCheckValue(string month)
    {
        int mo = 0;
        if (month == "Jan")
        {
            mo = 10;
        }
        else if (month=="JAN")
        {
            mo = 10;
        }
        if (month == "Feb")
        {
            mo = 11;
        }
        else if (month == "FEB")
        {
            mo = 11;
        }
        if (month == "Mar")
        {
            mo = 12;
        }
        else if (month == "MAR")
        {
            mo = 12;
        }
        if (month == "Apr")
        {
            mo = 1;
        }
        else if (month == "APR")
        {
            mo = 1;
        }
        if (month == "May")
        {
            mo = 2;
        }
        else if (month == "MAY")
        {
            mo = 2;
        }
        if (month == "Jun")
        {
            mo = 3;
        }
        else if (month == "JUN")
        {
            mo = 3;
        }
        if (month == "Jul")
        {
            mo = 4;
        }
        else if (month == "JUL")
        {
            mo = 4;
        }
        if (month == "Aug")
        {
            mo =5;
        }
        else if (month == "AUG")
        {
            mo = 5;
        }

        if (month == "Sep")
        {
            mo = 6;
        }
        else if (month == "SEP")
        {
            mo = 6;
        }
        if (month == "Oct")
        {
            mo = 7;
        }
        if (month == "OCT")
        {
            mo = 7;
        }
        if (month == "Nov")
        {
            mo = 8;
        }
        else if (month == "NOV")
        {
            mo = 8;
        }
        if (month == "Dec")
        {
            mo = 9;
        }
        else if (month == "DEC")
        {
            mo = 9;
        }
        if (month == "Jan")
        {
            mo = 10;
        }
        else if (month == "JAN")
        {
            mo = 10;
        }
        if (month == "Feb")
        {
            mo = 11;
        }
        else if (month == "FEB")
        {
            mo = 11;
        }
        if (month == "Mar")
        {
            mo = 12;
        }
        else if (month == "MAR")
        {
            mo = 12;
        }
        return mo;
    }
    public string Encode(string sData)
    {
        try
        {
            byte[] encData_byte = new byte[sData.Length];

            encData_byte = System.Text.Encoding.UTF8.GetBytes(sData);

            string encodedData = Convert.ToBase64String(encData_byte);

            return encodedData;

        }
        catch (Exception)
        {
            return "";
        }
    }
    public string Decode(string sData)
    {

        System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();

        System.Text.Decoder utf8Decode = encoder.GetDecoder();

        byte[] todecode_byte = Convert.FromBase64String(sData);

        int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);

        char[] decoded_char = new char[charCount];

        utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);

        string result = new String(decoded_char);

        return result;

    }
    public string IDGeneration(string FixedString, string x)
    {
        string xx = "";
        if (x.Length == 1)
        {
            xx = FixedString + "000000" + x;

        }
        else if (x.Length == 2)
        {
            xx = FixedString + "00000" + x;
        }
        else if (x.Length == 3)
        {
            xx = FixedString + "0000" + x;
        }
        else if (x.Length == 4)
        {
            xx = FixedString + "000" + x;
        }
        else if (x.Length == 5)
        {
            xx = FixedString + "00" + x;
        }
        else if (x.Length == 6)
        {
            xx = FixedString + "0" + x;
        }
        else
        {
            xx = FixedString + x;
        }
        return xx;
    }
    //=======================================Password Generation Process  Start===================================
    private int RandomNumber(int min, int max)
    {

        int temp = random.Next(min, max);
        //Random random = new Random();
        //int temp = Random.Next(min, max);
        return temp;
        //return random.Next(min, max);
    }
    private string RandomString(int size, bool lowerCase)
    {
        StringBuilder builder = new StringBuilder();

        char ch;
        for (int i = 0; i < size; i++)
        {
            ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
            builder.Append(ch);
        }
        if (lowerCase)
            return builder.ToString().ToLower();
        return builder.ToString();
    }
    public string GetPassword()
    {

        StringBuilder builder = new StringBuilder();
        builder.Append(RandomString(4, true));
        builder.Append(RandomNumber(1000, 9999));
        builder.Append(RandomString(2, true));
        return builder.ToString();

    }
    //=======================================Password Generation Process  End===================================
    public void ExportToExcel(string fileName, GridView gv)
    {
        string style = @"<style> .text { mso-number-format:\@; } </style> ";
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", fileName));
        HttpContext.Current.Response.ContentType = "application/ms-excel";

        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter htw = new HtmlTextWriter(sw))
            {
                // Create a form to contain the grid
                System.Web.UI.WebControls.Table table = new System.Web.UI.WebControls.Table();

                // add the header row to the table
                if (gv.HeaderRow != null)
                {
                    PrepareControlForExport(gv.HeaderRow);
                    table.Rows.Add(gv.HeaderRow);
                }

                // add each of the data rows to the table
                foreach (GridViewRow row in gv.Rows)
                {
                    PrepareControlForExport(row);
                    table.Rows.Add(row);
                }

                // add the footer row to the table
                if (gv.FooterRow != null)
                {
                    PrepareControlForExport(gv.FooterRow);
                    table.Rows.Add(gv.FooterRow);
                }

                // render the table into the htmlwriter
                table.RenderControl(htw);
                HttpContext.Current.Response.Write(style);
                HttpContext.Current.Response.Write(sw.ToString());
                HttpContext.Current.Response.End();
            }
        }
    }

    public void ExportDivToExcel(HttpResponse rr ,string fileName, HtmlGenericControl divExport )
    {
        rr.Clear();
        rr.AddHeader("content-disposition", string.Format("attachment; filename={0}", fileName));
        rr.Charset = "";
        rr.Cache.SetCacheability(HttpCacheability.NoCache);
        rr.ContentType = "application/vnd.xls";
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        divExport.RenderControl(htmlWrite);
        rr.Write(stringWrite.ToString());
        rr.End();
    }

    public void ExportDivToExcelWithFormatting(HttpResponse rr, string fileName, HtmlGenericControl divExport, string path)
    {
        //gdv.Attributes.Add("class", "grid");

        string attachment = "attachment; filename=" + fileName + ".xls";
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.AddHeader("content-disposition", attachment);
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        System.IO.StringWriter stw = new System.IO.StringWriter();
        HtmlTextWriter htextw = new HtmlTextWriter(stw);
        divExport.RenderControl(htextw);

        HttpContext.Current.Response.Write(stw.ToString());
        System.IO.FileInfo fi = new System.IO.FileInfo(Server.MapPath("~/css/theme.min.css"));
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        System.IO.StreamReader sr = fi.OpenText();
        while (sr.Peek() >= 0)
        {
            sb.Append(sr.ReadLine());
        }
        sr.Close();
        rr.Write("<html><head><style type='text/css'>" + sb.ToString() + "</style></head>" + stw.ToString() + "</html>");
        stw = null;
        htextw = null;
        rr.Flush();
        rr.End();
    }

    public void ExportDivToExcelWithFormatting(HttpResponse rr, string fileName, HtmlGenericControl divExport)
    {
        //gdv.Attributes.Add("class", "grid");

        string attachment = "attachment; filename=" + fileName + ".xls";
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.AddHeader("content-disposition", attachment);
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        System.IO.StringWriter stw = new System.IO.StringWriter();
        HtmlTextWriter htextw = new HtmlTextWriter(stw);
        divExport.RenderControl(htextw);

        HttpContext.Current.Response.Write(stw.ToString());
        System.IO.FileInfo fi = new System.IO.FileInfo(Server.MapPath("~/css/theme.min.css"));
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        System.IO.StreamReader sr = fi.OpenText();
        while (sr.Peek() >= 0)
        {
            sb.Append(sr.ReadLine());
        }
        sr.Close();
        rr.Write("<html><head><style type='text/css'>" + sb.ToString() + "</style></head>" + stw.ToString() + "</html>");
        fi = new System.IO.FileInfo(Server.MapPath("~/css/bootstrap.min.css"));
        sb = new System.Text.StringBuilder();
        sr = fi.OpenText();
        while (sr.Peek() >= 0)
        {
            sb.Append(sr.ReadLine());
        }
        sr.Close();
        rr.Write("<html><head><style type='text/css'>" + sb.ToString() + "</style></head>" + stw.ToString() + "</html>");
        stw = null;
        htextw = null;
        rr.Flush();
        rr.End();
    }

    public void printGridWithinupdatepanel(object sender, Page Page)
    {
        MethodInfo methodInfo = typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
       .Where(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel")).First();
        methodInfo.Invoke(ScriptManager.GetCurrent(Page),
            new object[] { sender as UpdatePanel });
    }

    public void PrepareControlForExport(Control control)
    {
        for (int i = 0; i < control.Controls.Count; i++)
        {
            Control current = control.Controls[i];
            if (current is Label)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as Label).Text));
            }
            if (current is LinkButton)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as LinkButton).Text));
            }
            else if (current is ImageButton)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as ImageButton).AlternateText));
            }
            else if (current is HyperLink)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as HyperLink).Text));
            }
            else if (current is DropDownList)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as DropDownList).SelectedItem.Text));
            }
            else if (current is CheckBox)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as CheckBox).Checked ? "True" : "False"));
            }

            if (current.HasControls())
            {
                PrepareControlForExport(current);
            }
        }
    }
    public void ExportToWord(HttpResponse hh, string fname, HtmlGenericControl dd)
    {
        hh.Clear();
        hh.Buffer = true;
        hh.AddHeader("content-disposition", "attachment;filename=" + fname + ".doc");
        hh.Charset = "";
        hh.Cache.SetCacheability(HttpCacheability.NoCache);
        hh.ContentType = "application/vnd.ms-word";
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        dd.RenderControl(htmlWrite);
        hh.Output.Write(stringWrite.ToString());
        hh.Flush();
        hh.End();
    }
    public void ExportDivToExcel2(HttpResponse rs, string fname, HtmlGenericControl div)
    {
        rs.Clear();
        rs.AddHeader("content-disposition", "attachment;filename="+fname);
        rs.Charset = "";
        rs.ContentType = "application/vnd.xls";
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        div.RenderControl(htmlWrite);
        rs.Write(stringWrite.ToString());
        rs.End();
    }

    public void ExportTolandscapeWord(HttpResponse hh, string fname, System.Web.UI.HtmlControls.HtmlGenericControl dd)
    {
        hh.Clear();
        hh.Buffer = true;
        hh.AddHeader("content-disposition", "attachment;filename=" + fname + ".doc");
        hh.Charset = "";
        hh.Cache.SetCacheability(HttpCacheability.NoCache);
        hh.ContentType = "application/vnd.ms-word";
        string csspath1 = Server.MapPath("~/css/print/print-theme.min.css");
        string csspath2 = Server.MapPath("~/css/print/print-bootstrap.min.css");

        string csspath3 = Server.MapPath("~/css/font-entypo.css");
        string csspath4 = Server.MapPath("~/css/font-awesome.min.css");

        string csspath5 = Server.MapPath("~/css/fonts.css");


        hh.Write("<html>");
        hh.Write("<head>");
        hh.Write("<META HTTP-EQUIV='Content-Type' CONTENT='text/html; charset=UTF-8'>");
        hh.Write("<meta name=ProgId content=Word.Document>");
        hh.Write("<meta name=Generator content='Microsoft Word 9'>");
        hh.Write("<meta name=Originator content='Microsoft Word 9'>");
        hh.Write("<style>");
        hh.Write("@page Section1 {size:800.45pt 841.7pt; margin:1.0in 1.25in 1.0in 1.25in;mso-header-margin:.5in;mso-footer-margin:.5in;mso-paper-source:0;}");
        hh.Write("div.Section1 {page:Section1;}");
        hh.Write("@page Section2 {size:1000.7pt 595.45pt;mso-page-orientation:landscape;margin:1.25in 1.0in 1.25in 1.0in;mso-header-margin:.5in;mso-footer-margin:.5in;mso-paper-source:0;}");
        hh.Write("div.Section2 {page:Section2;}");
        hh.Write("</style>");
        hh.Write("<link href=" + csspath1 + " rel=stylesheet type=text/css  />");
        hh.Write("<link href=" + csspath2 + " rel=stylesheet type=text/css   />");
        hh.Write("<link href=" + csspath3 + " rel=stylesheet type=text/css  />");
        hh.Write("<link href=" + csspath4 + " rel=stylesheet type=text/css   />");
        hh.Write("<link href=" + csspath5 + " rel=stylesheet type=text/css  />");
        hh.Write("</head>");
        hh.Write("<body>");
        hh.Write("<div class=Section2>");

        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        dd.RenderControl(htmlWrite);
        hh.Output.Write(stringWrite.ToString());

        hh.Write("</div>");
        hh.Write("</body>");
        hh.Write("</html>");

        hh.Flush();
        hh.End();
    }

    public void ExporttoPdf(HttpResponse hh, string fname, HtmlGenericControl divExport)
    {
        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                divExport.RenderControl(hw);
                StringReader sr = new StringReader(sw.ToString());
                Document pdfDoc = new Document(PageSize.LETTER, 20f, 20f, 20f, 20f);
                HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                PdfWriter.GetInstance(pdfDoc, hh.OutputStream);
                pdfDoc.Open();
                htmlparser.Parse(sr);
                pdfDoc.Close();

                hh.ContentType = "application/pdf";
                hh.AddHeader("content-disposition", "attachment;filename=" + fname + ".pdf");
                hh.Cache.SetCacheability(HttpCacheability.NoCache);
                hh.Write(pdfDoc);
                hh.End();
            }
        }
    }


    public void ExporttolandscapePdf(HttpResponse hh, string fname, Control divExport)
    {
        using (System.IO.StringWriter sw = new System.IO.StringWriter())
        {
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                divExport.RenderControl(hw);
                System.IO.StringReader sr = new System.IO.StringReader(sw.ToString());
                iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A3.Rotate(), 20f, 20f, 20f, 20f);
                iTextSharp.text.html.simpleparser.HTMLWorker htmlparser = new iTextSharp.text.html.simpleparser.HTMLWorker(pdfDoc);
                iTextSharp.text.pdf.PdfWriter.GetInstance(pdfDoc, hh.OutputStream);
                pdfDoc.Open();
                htmlparser.Parse(sr);
                pdfDoc.Close();

                hh.ContentType = "application/pdf";
                hh.AddHeader("content-disposition", "attachment;filename=" + fname + ".pdf");
                hh.Cache.SetCacheability(HttpCacheability.NoCache);

                hh.Write(pdfDoc);

                hh.End();
            }
        }
    }


    public string StringToHTML(string xx)
    {
        int i;
        string yy = "";
        for (i = 0; i <= xx.Length - 1; i++)
        {
            if (xx[i].ToString() == "\n")
            {
                yy = yy + "<br>" + "<p>";
            }
            else
            {
                yy = yy + xx[i];
            }
        }
        return yy;
    }
    public string CurrentDate()
    {
        string sql = "";
        sql = "select convert(nvarchar,getdate(),106) as CDate";
        sql = ReturnTag(sql, "Cdate");
        return sql;
    }

    public string CurrentDate(string formate)
    {
        string sql = "";
        sql = "select convert(nvarchar,getdate(),106) as CDate";
        sql = Convert.ToDateTime(ReturnTag(sql, "Cdate")).ToString(formate);
        return sql;
    }

    public DateTime CurrentDateinDatetime()
    {
        string sql = "";
        sql = "select getdate() as CDate";
        DateTime dt = Convert.ToDateTime(ReturnTag(sql, "Cdate"));
        return dt;
    }

    public DateTime CurrentMonthFirstDate()
    {
        string sql = "";
        sql = "SELECT DATEADD(DAY,-DAY(GETDATE())+1, CAST(GETDATE() AS DATETIME)) DateTime";
        DateTime dt = Convert.ToDateTime(ReturnTag(sql, "DateTime"));
        return dt;
    }
    
    public string CurrentYear()
    {

        string sql = "";
        sql = "select   year(getdate()) as yy";
        sql = ReturnTag(sql, "yy");
        return sql;

    }
    public string DecimalPlaceMustTwoZero(string xx)
    {
        int l, i, k;
        l = xx.Length - 1;
        bool flag = false;
        string kk = "";
        for (i = 0; i <= l; i++)
        {
            if (xx[i] == '.')
            {
                kk = kk + xx[i];
                flag = true;
                break;
            }
            else
            {
                kk = kk + xx[i];
            }
        }


        if (flag)
        {
            k = l - i;
            if (k < 2)
            {
                kk = kk + xx[i + 1] + "0";
            }
            else
            {
                kk = kk + xx[i + 1] + xx[i + 2];
            }

        }
        else
        {
            kk = kk + ".00";
        }

        return kk;

    }
    public string DayOfMonthString01(string month)
    {
        string mo = month.Trim();
        string d = "0";
        switch (mo)
        {
            case "Jan":
                d = d + "1";
                break;
            case "Feb":
                d = d + "2";
                break;
            case "Mar":
                d = d + "3";
                break;
            case "Apr":
                d = d + "4";
                break;
            case "May":
                d = d + "5";
                break;
            case "Jun":
                d = d + "6";
                break;
            case "Jul":
                d = d + "7";
                break;
            case "Aug":
                d = d + "8";
                break;
            case "Sep":
                d = d + "9";
                break;
            case "Oct":
                d = "10";
                break;
            case "Nov":
                d = "11";
                break;
            case "Dec":
                d = "12";
                break;
        }


        return d;
    }
    private void DecryptFile(string inputFile, string outputFile)
    {

        {
            string password = @"myKey123"; // Your Key Here

            UnicodeEncoding UE = new UnicodeEncoding();
            byte[] key = UE.GetBytes(password);

            FileStream fsCrypt = new FileStream(inputFile, FileMode.Open);

            RijndaelManaged RMCrypto = new RijndaelManaged();

            CryptoStream cs = new CryptoStream(fsCrypt,
                RMCrypto.CreateDecryptor(key, key),
                CryptoStreamMode.Read);

            FileStream fsOut = new FileStream(outputFile, FileMode.Create);

            int data;
            while ((data = cs.ReadByte()) != -1)
                fsOut.WriteByte((byte)data);

            fsOut.Close();
            cs.Close();
            fsCrypt.Close();

        }
    }
    private void EncryptFile(string inputFile, string outputFile)
    {

        try
        {
            string password = @"myKey123"; // Your Key Here
            UnicodeEncoding UE = new UnicodeEncoding();
            byte[] key = UE.GetBytes(password);

            string cryptFile = outputFile;
            FileStream fsCrypt = new FileStream(cryptFile, FileMode.Create);

            RijndaelManaged RMCrypto = new RijndaelManaged();

            CryptoStream cs = new CryptoStream(fsCrypt,
                RMCrypto.CreateEncryptor(key, key),
                CryptoStreamMode.Write);

            FileStream fsIn = new FileStream(inputFile, FileMode.Open);

            int data;
            while ((data = fsIn.ReadByte()) != -1)
                cs.WriteByte((byte)data);


            fsIn.Close();
            cs.Close();
            fsCrypt.Close();
        }
        catch
        {
            
        }
    }

    public string GetMACAddress()
    {
        NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
        String sMacAddress = string.Empty;
        foreach (NetworkInterface adapter in nics)
        {
            if (sMacAddress == String.Empty)// only return MAC Address from first card  
            {
                IPInterfaceProperties properties = adapter.GetIPProperties();
                sMacAddress = adapter.GetPhysicalAddress().ToString();
            }
        } return sMacAddress;
    }

    public string GetMotherBoardSrno()
    {
        string motherBoardSrno = "";
        ManagementObjectSearcher MOS = new ManagementObjectSearcher("Select * From Win32_BaseBoard");
        foreach (ManagementObject getserial in MOS.Get())
        {
            motherBoardSrno = getserial["SerialNumber"].ToString();
        }

        return motherBoardSrno;
    }

    public byte[] ConvertTheImageIntoBiary(string FileName)
  {

      byte[] Buffer = null;
      try
      {

          // Open file for reading

          System.IO.FileStream FileStream = new System.IO.FileStream(FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);



          // attach filestream to binary reader

          System.IO.BinaryReader BinaryReader = new System.IO.BinaryReader(FileStream);



          // get total byte length of the file

          long _TotalBytes = new System.IO.FileInfo(FileName).Length;



          // read entire file into buffer

          Buffer = BinaryReader.ReadBytes((Int32)_TotalBytes);



          // close file reader

          FileStream.Close();
          FileStream.Dispose();

          BinaryReader.Close();

      }

      catch (Exception) { }
      return Buffer;

  }
    public void FillDropDown(string sql, string p)
      {
          throw new NotImplementedException();
      }

    public string GetImageUrl(string imagepath, string[] AbsoluteUri)
    {
        string page = imagepath;
        string[] splits = AbsoluteUri;
        if (splits.Length >= 2)
        {
            string url = splits[0] + "//";
            for (int i = 2; i < splits.Length - 1; i++)
            {
                url += splits[i];
                url += "/";
            }
            return url + page;
        }
        return page;
    }

    public void ExportToPdfAsImage(string base64, string fileName, HttpResponse response)
    {
   
        byte[] bytes = Convert.FromBase64String(base64);


        System.IO.MemoryStream newImageStream = new System.IO.MemoryStream(bytes, 0, bytes.Length);
        System.Drawing.Image image = System.Drawing.Image.FromStream(newImageStream, true);
        System.Drawing.Bitmap resized = new System.Drawing.Bitmap(image, image.Width * 2, image.Height * 2);

        MemoryStream stream = new MemoryStream();

        iTextSharp.text.Document pdf = new iTextSharp.text.Document();
        iTextSharp.text.pdf.PdfWriter writer = iTextSharp.text.pdf.PdfWriter.GetInstance(pdf, stream);

        pdf.Open();

        //iTextSharp.text.pdf.PdfPTable tblImage = new iTextSharp.text.pdf.PdfPTable(1);
        iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(resized, iTextSharp.text.Color.WHITE);
        img.SetDpi(500, 500);
        img.ScalePercent(30f);
        //tblImage.AddCell(img);
        pdf.Add(img);

        pdf.Close();

        response.ContentType = "application/pdf";
        response.AddHeader("content-disposition", "attachment;filename=" + fileName + ".pdf");
        response.Buffer = true;
        response.Clear();
        response.OutputStream.Write(stream.GetBuffer(), 0, stream.GetBuffer().Length);
        response.OutputStream.Flush();
        response.End();
    }

	public DataSet ReturnDataSet(string procedureName, SqlParameter[] para)
	{
		DataSet ds = new DataSet();
		SqlCommand cmd = new SqlCommand();
		try
		{
			cmd.Parameters.AddRange(para);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = procedureName;

			con = dbGet_connection();
			cmd.Connection = con;
			con.Open();

			SqlDataAdapter da = new SqlDataAdapter(cmd);

			da.Fill(ds);
			con.Close();
		}
		catch (SqlException)
		{
			con.Close();
		}
		catch (Exception) { }

		return ds;
	}
}
