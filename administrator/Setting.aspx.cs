using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class admin_RulesForlibrary : Page
{
    string sql = "";
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    protected void Page_Load(object sender, EventArgs e)
    {
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);

        if (!IsPostBack)
        {
            LoadK12();
        }
    }

    public void LoadK12()
    {
        sql = "Select isK12, IsAadhaar, CurrencySymbols, CurrencyName, DecimalCount, DateFormat, Language from setting";
        string value = (oo.ReturnTag(sql, "isK12").ToString() == "" ? "0" : oo.ReturnTag(sql, "isK12").ToString());
        if (value == "True")
        {
            rdoK12.SelectedValue = "1";
        }
        else
        {
            rdoK12.SelectedValue = "0";
        }
        if (oo.ReturnTag(sql, "IsAadhaar").ToString() != "")
        {
            ddlAadhaar.SelectedValue = oo.ReturnTag(sql, "IsAadhaar").ToString();
        }
        if (oo.ReturnTag(sql, "CurrencySymbols").ToString() != "")
        {
            string ss = oo.ReturnTag(sql, "CurrencySymbols").ToString().Replace("&#", "");
            ss = ss.Replace(";", "");
            ddlCurrencySymbol.SelectedValue = ss.ToString();
        }
        if (oo.ReturnTag(sql, "CurrencyName").ToString() != "")
        {
            txtCurrencyName.Text = oo.ReturnTag(sql, "CurrencyName").ToString();
        }
        if (oo.ReturnTag(sql, "DecimalCount").ToString() != "")
        {
            txtR1.Text = oo.ReturnTag(sql, "DecimalCount").ToString();
        }
        if (oo.ReturnTag(sql, "DateFormat").ToString() != "")
        {
            ddlDateFormat.SelectedValue = oo.ReturnTag(sql, "DateFormat").ToString();
        }
        if (oo.ReturnTag(sql, "Language").ToString() != "")
        {
            ddlLanguage.SelectedValue = oo.ReturnTag(sql, "Language").ToString();
        }
    }

    protected void lnkSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtCurrencyName.Text.Trim()=="")
            {
                Campus oos = new Campus(); oos.msgbox(this.Page, msgbox, "Please enter currency name", "A");
            }

            string dd = "&#" + ddlCurrencySymbol.SelectedValue.ToString() + ";";
            sql = "Select count(*) cunt from setting";
            string value = oo.ReturnTag(sql, "cunt").ToString();

            SqlCommand cmd = new SqlCommand();
            if (value == "0")
            {
                cmd.CommandText = "insert into setting (isK12, IsAadhaar, CurrencySymbols, CurrencyName, DecimalCount, DateFormat, Language) values (" + rdoK12.SelectedValue + ", '" + ddlAadhaar.SelectedValue + "', '" + ddlCurrencySymbol.SelectedValue.ToString() + "', '" + txtCurrencyName.Text.Trim() + "', '" + txtR1.Text.Trim() + "', '"+ ddlDateFormat.SelectedValue+"', '" + ddlLanguage.SelectedValue + "')";
            }
            else
            {
                cmd.CommandText = "update setting set isK12=" + rdoK12.SelectedValue + ", IsAadhaar='" + ddlAadhaar.SelectedValue + "', CurrencySymbols='" + ddlCurrencySymbol.SelectedValue.ToString() + "', CurrencyName='" + txtCurrencyName.Text.Trim() + "', DecimalCount='" + txtR1.Text.Trim() + "', DateFormat='" + ddlDateFormat.SelectedValue + "', Language='" + ddlLanguage.SelectedValue + "'";
            }
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            string simbol = "";
            if (ddlCurrencySymbol.SelectedValue == "8377")
            {
                simbol = "fa fa-inr";
            }
            if (ddlCurrencySymbol.SelectedValue == "36")
            {
                simbol = "fa fa-dollar";
            }
            if (ddlCurrencySymbol.SelectedValue == "8358")
            {
                simbol = "fa fa-naira";
            }
            if (ddlCurrencySymbol.SelectedValue == "65020")
            {
                simbol = "fa fa-dollar";
            }
            SqlCommand cmd2 = new SqlCommand();
            cmd2.CommandText = "update Menueam set ParentClassName='" + simbol.ToString() + "', ChildClassName='" + simbol.ToString() + "' where Text in ('Fee', 'Fee Deposit') update Menueam set ParentClassName='" + simbol.ToString() + "', ChildClassName='" + simbol.ToString() + "' where ParentID in (select MenuID from Menueam where text in ('Fee Deposit'))";
            cmd2.CommandType = CommandType.Text;
            cmd2.Connection = con;
            con.Open();
            cmd2.ExecuteNonQuery();
            con.Close();
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully", "S");
            LoadK12();
        }
        catch (Exception ex)
        {
        }
    }

    protected void ddlCurrencySymbol_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtCurrencyName.Text = "";
    }
}