using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;

/// <summary>
/// Summary description for BLL
/// </summary>
public partial class BLL
{
    public static readonly BLL BLLInstance = new BLL();

    public DataTable GetSerialNo(ref DataTable dt, String HeaderText)
    {
        dt.Columns.Add(HeaderText);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            dt.Rows[i][HeaderText] = i + 1;
        }
        return dt;
    }
    public static void FillDropDown(DropDownList ddl, DataTable dt, string DisplayName, string ValueName, Char HeaderTitle)
    {
        ////if (dt != null && dt.Rows.Count > 0)
        ////{
        //    DataRow dr;
        //    dr = dt.NewRow();
        //    if (HeaderTitle == 'S')
        //    {
        //        dr[DisplayName] = "<--Select-->";
        //        dr[ValueName] = "-1";
        //        dt.Rows.InsertAt(dr, 0);
        //    }
        //    else if (HeaderTitle == 'A')
        //    {
        //        dr[DisplayName] = "<--All-->";
        //        dr[ValueName] = "-1";
        //        dt.Rows.InsertAt(dr, 0);
        //    }
        //    else if (HeaderTitle == 'N')
        //    {
        //        dr[DisplayName] = "<--None-->";
        //        dr[ValueName] = "-1";
        //        dt.Rows.InsertAt(dr, 0);
        //    }
        ////}
        ddl.DataSource = dt;
        ddl.DataTextField = DisplayName;
        ddl.DataValueField = ValueName;
        ddl.DataBind();
    }

    public string GetALP(int Number)
    {
        string ALP = "";
        switch (Number)
        {
            case 1: ALP = "A"; break;
            case 2: ALP = "B"; break;
            case 3: ALP = "C"; break;
            case 4: ALP = "D"; break;
            case 5: ALP = "E"; break;
            case 6: ALP = "F"; break;
            case 7: ALP = "G"; break;
            case 8: ALP = "H"; break;
            case 9: ALP = "I"; break;
            case 10: ALP = "J"; break;
            case 11: ALP = "K"; break;
            case 12: ALP = "L"; break;
            case 13: ALP = "M"; break;
            case 14: ALP = "N"; break;
            case 15: ALP = "O"; break;
            case 16: ALP = "P"; break;
            case 17: ALP = "Q"; break;
            case 18: ALP = "R"; break;
            case 19: ALP = "S"; break;
            case 20: ALP = "T"; break;
            case 21: ALP = "U"; break;
            case 22: ALP = "V"; break;
            case 23: ALP = "W"; break;
            case 24: ALP = "X"; break;
            case 25: ALP = "Y"; break;
            case 26: ALP = "Z"; break;
        }
        return ALP;
    }
    public DateTime GetDateFormate(string Date)
    {
        if (Date == string.Empty)
            Date = "01/01/1900";

        int Day = Convert.ToInt16(Date.Substring(0, 2));
        int Month = Convert.ToInt16(Date.Substring(3, 2));
        int Year = Convert.ToInt16(Date.Substring(6, 4));
        DateTime NewDate = new DateTime(Year, Month, Day);
        return NewDate;
    }
    public static void FillHourInDropDownlist(DropDownList ddl)
    {
        var dt = new DataTable();
        dt.Columns.Add("Text");
        dt.Columns.Add("Value");
        var dr = dt.NewRow();
        dr["Text"] = "HH";
        dr["Value"] = "-1";
        dt.Rows.Add(dr);
        for (var i = 0; i <= 12;i++)
        {
            var dr1 = dt.NewRow();
            if (i <= 9)
            {
                dr1["Text"] = "0" +i;
                dr1["Value"] = "0" + i;

            }
            else
            {
                dr1["Text"] = i.ToString();
                dr1["Value"] = i.ToString();
            }
        
            dt.Rows.Add(dr1);
        }
        ddl.DataSource = dt;
        ddl.DataTextField = "Text";
        ddl.DataValueField = "Value";
        ddl.DataBind();
    }
    public static void FillMinuteInDropDownlist(DropDownList ddl)
    {
        var dt = new DataTable();
        dt.Columns.Add("Text");
        dt.Columns.Add("Value");
        var dr = dt.NewRow();
        dr["Text"] = "MM";
        dr["Value"] = "-1";
        dt.Rows.Add(dr);
        for (var i = 0; i <60; i++)
        {
            var dr1 = dt.NewRow();
            if (i <= 9)
            {
                dr1["Text"] = "0" + i;
                dr1["Value"] = "0" + i;

            }
            else
            {
                dr1["Text"] = i.ToString();
                dr1["Value"] = i.ToString();
            }

            dt.Rows.Add(dr1);
        }
        ddl.DataSource = dt;
        ddl.DataTextField = "Text";
        ddl.DataValueField = "Value";
        ddl.DataBind();
    }

    public static void FillSecondInDropDownlist(DropDownList ddl)
    {
        var dt = new DataTable();
        dt.Columns.Add("Text");
        dt.Columns.Add("Value");
        var dr = dt.NewRow();
        dr["Text"] = "SS";
        dr["Value"] = "-1";
        dt.Rows.Add(dr);
        for (var i = 0; i < 60; i++)
        {
            var dr1 = dt.NewRow();
            if (i <= 9)
            {
                dr1["Text"] = "0" + i;
                dr1["Value"] = "0" + i;

            }
            else
            {
                dr1["Text"] = i.ToString();
                dr1["Value"] = i.ToString();
            }

            dt.Rows.Add(dr1);
        }
        ddl.DataSource = dt;
        ddl.DataTextField = "Text";
        ddl.DataValueField = "Value";
        ddl.SelectedIndex = 1;
        ddl.DataBind();
    }

    public void ShowMSG(string message)
    {
        string str = new ShowMSG().MSG(message); 
        string script = "<script type=\"text/javascript\">alert('" + str + "');</script>";
        Page currentHandler = HttpContext.Current.CurrentHandler as Page;
        ScriptManager.RegisterStartupScript(currentHandler, typeof(Page), "alert", script, false);
    }

    public string FetchMSG(string MSG)
    {
        string str = new ShowMSG().MSG(MSG);
        return str;
    }
}