using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using DataTier;


/// <summary>
/// Summary description for BusinessLogic
/// </summary>
public class BusinessLogic
{


    public BusinessLogic()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static void bindDList(DropDownList dlist, string table, string datavale, string datatext)
    {
        DataSet ds = new DataSet();
        string query = "select " + " " + datavale + "," + datatext + " " + "from" + " " + table;

        ds = SqlHelper.ExecuteDataset(CommandType.Text, query);
        dlist.DataSource = ds;
        dlist.DataTextField = datatext;
        dlist.DataValueField = datavale;
        dlist.DataBind();
        dlist.DataBind();
        ds.Clear();
        dlist.Items.Insert(0, new ListItem("<-select->", "-1"));
}
    public string Format(TextBox textbox)
    {
        return textbox.Text.Replace("'", "''");
    }
    public static void BindListBox(ListBox dlist, string table, string datavale, string datatext)
    {
        DataSet ds = new DataSet();
        string query = "select " + " " + datavale + "," + datatext + " " + "from" + " " + table;

        ds = SqlHelper.ExecuteDataset(CommandType.Text, query);
        dlist.DataSource = ds;
        dlist.DataTextField = datatext;
        dlist.DataValueField = datavale;
        dlist.DataBind();
        dlist.DataBind();
        ds.Clear();



    }
    public static void bindDList(DropDownList dlist, string cmd, string datatext, string datavalue, bool b)
    {
        DataSet ds = new DataSet();
        ds = SqlHelper.ExecuteDataset(CommandType.Text, cmd);
        dlist.DataSource = ds;
        dlist.DataTextField = datatext;
        dlist.DataValueField = datavalue;
        dlist.DataBind();
        dlist.DataBind();
        ds.Clear();
        dlist.Items.Insert(0, new ListItem("<-select->", "-1"));
    }

    public static void BindCheckBoxList(CheckBoxList chklist, string cmd, string datatext, string datavalue)
    {
        DataSet ds = new DataSet();

        // dlist.Items.Insert(0,"<-select->");
        ds = SqlHelper.ExecuteDataset(CommandType.Text, cmd);
        chklist.DataSource = ds;
        chklist.DataTextField = datatext;
        chklist.DataValueField = datavalue;
        chklist.DataBind();
        chklist.DataBind();
        ds.Clear();
        // dlist.Items.Add("<-select->");
        //  dlist.Text = "<-select->";


    }
    public static void BindRadiobuttonList(RadioButtonList rllist, string cmd, string datatext, string datavalue)
    {
        DataSet ds = new DataSet();

        // dlist.Items.Insert(0,"<-select->");
        ds = SqlHelper.ExecuteDataset(CommandType.Text, cmd);
        rllist.DataSource = ds;
        rllist.DataTextField = datatext;
        rllist.DataValueField = datavalue;
        rllist.DataBind();
        rllist.DataBind();
        ds.Clear();
        // dlist.Items.Add("<-select->");
        //  dlist.Text = "<-select->";


    }

    public static string GetRandomNum(int num)
    {
        string str = "";
        SqlDataReader dr;
        SqlConnection con = new SqlConnection(SqlHelper.GetConnectionString.ToString());
        SqlCommand cmd = new SqlCommand("select (round(rand()*(" + num + "),(0)))", con);
        con.Open();
        dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            str = dr.GetValue(0).ToString();
        }
        con.Close();
        cmd.Dispose();
        return str;
    }
    public static void bindDList(DropDownList dlist, string table, string datavale, string datatext, string whereclause)
    {
        DataSet ds = new DataSet();
        string query = "select " + " " + datavale + "," + datatext + " " + "from" + " " + table + " " + whereclause;

        ds = SqlHelper.ExecuteDataset(CommandType.Text, query);
        dlist.DataSource = ds;
        dlist.DataTextField = datatext;
        dlist.DataValueField = datavale;
        dlist.DataBind();
        dlist.DataBind();
        ds.Clear();
        dlist.Items.Insert(0, new ListItem("<-select->", "-1"));


    }

    public static void BindYr(DropDownList ddl)
    {
        for (int i = 1950; i <= DateTime.Now.Year+1; i++)
        {

            ddl.Items.Add(i.ToString());

        }
        ddl.SelectedValue = DateTime.Now.Year.ToString();


    }

    public static void Month_Datasource(DropDownList ddlMonth)
    {
        ddlMonth.Items.Add(new ListItem("January", "1"));
        ddlMonth.Items.Add(new ListItem("February", "2"));

        ddlMonth.Items.Add(new ListItem("March", "3"));
        ddlMonth.Items.Add(new ListItem("April", "4"));

        ddlMonth.Items.Add(new ListItem("May", "5"));
        ddlMonth.Items.Add(new ListItem("June", "6"));

        ddlMonth.Items.Add(new ListItem("July", "7"));
        ddlMonth.Items.Add(new ListItem("August", "8"));

        ddlMonth.Items.Add(new ListItem("September", "9"));
        ddlMonth.Items.Add(new ListItem("October", "10"));

        ddlMonth.Items.Add(new ListItem("November", "11"));
        ddlMonth.Items.Add(new ListItem("December", "12"));
        DateTime dt = DateTime.Now;
        int iMois = dt.Month;
        ddlMonth.SelectedValue = iMois.ToString();
    }
    public static void bindMonth(DropDownList ddlMonth, DropDownList dlday)
    {
        ddlMonth.Items.Add(new ListItem("January", "1"));
        ddlMonth.Items.Add(new ListItem("February", "2"));

        ddlMonth.Items.Add(new ListItem("March", "3"));
        ddlMonth.Items.Add(new ListItem("April", "4"));

        ddlMonth.Items.Add(new ListItem("May", "5"));
        ddlMonth.Items.Add(new ListItem("June", "6"));

        ddlMonth.Items.Add(new ListItem("July", "7"));
        ddlMonth.Items.Add(new ListItem("August", "8"));

        ddlMonth.Items.Add(new ListItem("September", "9"));
        ddlMonth.Items.Add(new ListItem("October", "10"));

        ddlMonth.Items.Add(new ListItem("November", "11"));
        ddlMonth.Items.Add(new ListItem("December", "12"));

        DateTime dt = DateTime.Now;
        int iMois = dt.Month;
        ddlMonth.SelectedValue = iMois.ToString();
        buildListDay(iMois, Convert.ToInt32(dt.Year), dlday);
    }
    public static void buildListDay(int mois, int year, DropDownList ddlDay)
    {

        //empty ddl

        ddlDay.Items.Clear();

        string sDays = string.Empty;
        int iMonth = mois;

        int iYear = year;
        sDays = DateTime.DaysInMonth(year, iMonth).ToString();

        for (int i = 1; i <= Convert.ToInt32(sDays); i++)
        {

            ddlDay.Items.Add(new ListItem(i.ToString(), i.ToString()));
        }

        DateTime dt = DateTime.Now; int iDay = dt.Day;
        try
        {
            ddlDay.SelectedValue = iDay.ToString();
        }
        catch
        {
        }

    }
    public static void BindGrid(GridView GV, String Str)
    {
        DataSet ds = new DataSet();
        SqlHelper.FillDataset(CommandType.Text, Str, ds, new string[] { "Table" });
        GV.DataSource = ds;
        GV.DataBind();
        ds.Clear();
    }
    public static void BindGrid(GridView GV, String Str, string whereclause)
    {
        DataSet ds = new DataSet();
        SqlHelper.FillDataset(CommandType.Text, Str + " " + whereclause, ds, new string[] { "Table" });
        GV.DataSource = ds;
        GV.DataBind();
        ds.Clear();
    }
}
