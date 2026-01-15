using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Web.Services;


public partial class website_feedback : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    DataTable dt = new DataTable();
   
    protected void Page_Load(object sender, EventArgs e)
    {
        Session.LCID = 2057;
        // Campus camp = new Campus(); camp.LoadLoader(loader);
        con = oo.dbGet_connection();
        try
        {
            if (!IsPostBack)
            {
                //loadJob();
                //Selectjobtitel();
            }
        }
        catch
        {
        }
    }

    protected void lnkSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string msgpass = "";// BLL.obj_bll1.set_Feedback(TextBox1.Text.Replace("'", "").Trim(), TextBox2.Text.Replace("'", "").Trim(), TextBox3.Text.Replace("'", "").Trim(), TextBox4.Text.Replace("'", "").Trim(), TextBox5.Text.Replace("'", "").Trim());

            string ss = "";
            ss = ss + "<table align=\"left\" class=\"style1\">";
            ss = ss + "  <tr>";
            ss = ss + "   <td>";
            ss = ss + "<img  src=\"http://www.redrosepublicschool.edu.in/sites/all/themes/gavias_educar/logo-p1.png\"></td>";
            ss = ss + "</tr>";
            ss = ss + "<tr>";
            ss = ss + " <td ";
            ss = ss + "  We’ve received following new details from our website:</td>";
            ss = ss + "</tr>";
            ss = ss + "<tr>";
            ss = ss + " <td>";
            ss = ss + "  <hr /></td>";
            ss = ss + "</tr>";
            ss = ss + "<tr>";
            ss = ss + "  <td>";
            ss = ss + "   Name : " + TextBox1.Text.Replace("'", "").Trim() + "</td>";
            ss = ss + "</tr>";
            ss = ss + "<tr>";
            ss = ss + "  <td>";
            ss = ss + "   Contact No. : " + TextBox2.Text.Replace("'", "").Trim() + "</td>";
            ss = ss + "</tr>";
            ss = ss + "<tr>";
            ss = ss + " <td>";
            ss = ss + "   E-mail : " + TextBox3.Text.Replace("'", "").Trim() + "</td>";
            ss = ss + "</tr>";
            ss = ss + "<tr>";
            ss = ss + " <td>";
            ss = ss + "   Subject : " + TextBox4.Text.Replace("'", "").Trim() + "</td>";
            ss = ss + "</tr>";
            ss = ss + "<tr>";
            ss = ss + " <td>";
            ss = ss + "   Message : " + TextBox5.Text.Replace("'", "").Trim() + "</td>";
            ss = ss + "</tr>";
            ss = ss + "   <td >";
            ss = ss + "    <hr /></td>";
            ss = ss + " </tr>";
            ss = ss + " </table>";
            //rrs.EmailSending(ss.ToString(), "Feedback from redrosepublicschool.edu.in", "care@redrosepublicschool.edu.in");

            string Acknowledge = "";

            Acknowledge = Acknowledge + "<table align=\"left\" class=\"style1\">";
            Acknowledge = Acknowledge + "  <tr>";
            Acknowledge = Acknowledge + "   <td colspan=\"2\" class=\"style2\">";
            Acknowledge = Acknowledge + "<img  src=\"http://www.redrosepublicschool.edu.in/sites/all/themes/gavias_educar/logo-p1.png\"></td>";
            Acknowledge = Acknowledge + "</tr>";
            Acknowledge = Acknowledge + "  <tr>";
            Acknowledge = Acknowledge + "   <td colspan=\"2\" class=\"style2\">";
            Acknowledge = Acknowledge + "This message was sent from a notification-only email address.</td>";
            Acknowledge = Acknowledge + "</tr>";
            Acknowledge = Acknowledge + "  <tr>";
            Acknowledge = Acknowledge + "   <td colspan=\"2\" class=\"style2\">";
            Acknowledge = Acknowledge + "Please do not reply to this message.</td>";
            Acknowledge = Acknowledge + "</tr>";
            Acknowledge = Acknowledge + "  <tr>";
            Acknowledge = Acknowledge + "   <td colspan=\"2\" class=\"style2\">";
            Acknowledge = Acknowledge + "<hr /></td>";
            Acknowledge = Acknowledge + "</tr>";
            Acknowledge = Acknowledge + "<tr>";
            Acknowledge = Acknowledge + " <td colspan=\"2\" class=\"style2\">";
            Acknowledge = Acknowledge + " Dear  " + TextBox1.Text.Replace("'", "").Trim() + ", </td>";
            Acknowledge = Acknowledge + "</tr>";
            Acknowledge = Acknowledge + " <td colspan=\"2\" class=\"style2\">";
            Acknowledge = Acknowledge + " We’ve received following new details from our website:</td>";
            Acknowledge = Acknowledge + "</tr>";
            Acknowledge = Acknowledge + "<tr>";
            Acknowledge = Acknowledge + "  <td>";
            Acknowledge = Acknowledge + "   Name : " + TextBox1.Text.Replace("'", "").Trim() + "</td>";
            Acknowledge = Acknowledge + " <td>";
            Acknowledge = Acknowledge + "    &nbsp;</td>";
            Acknowledge = Acknowledge + "</tr>";
            Acknowledge = Acknowledge + "<tr>";
            Acknowledge = Acknowledge + "  <td>";
            Acknowledge = Acknowledge + "   Contact No. : " + TextBox2.Text.Replace("'", "").Trim() + "</td>";
            Acknowledge = Acknowledge + " <td>";
            Acknowledge = Acknowledge + "   &nbsp;</td>";
            Acknowledge = Acknowledge + "</tr>";
            Acknowledge = Acknowledge + "<tr>";
            Acknowledge = Acknowledge + " <td>";
            Acknowledge = Acknowledge + "   E-mail : " + TextBox3.Text.Replace("'", "").Trim() + "</td>";
            Acknowledge = Acknowledge + " <td>";
            Acknowledge = Acknowledge + "  &nbsp;</td>";
            Acknowledge = Acknowledge + "</tr>";
            Acknowledge = Acknowledge + "<tr>";
            Acknowledge = Acknowledge + "  <td>";
            Acknowledge = Acknowledge + "  Subject : " + TextBox4.Text.Replace("'", "").Trim() + "</td>";
            Acknowledge = Acknowledge + "<td>";
            Acknowledge = Acknowledge + "  &nbsp;</td>";
            Acknowledge = Acknowledge + "</tr>";
            Acknowledge = Acknowledge + "<tr>";
            Acknowledge = Acknowledge + "  <td>";
            Acknowledge = Acknowledge + "  Message : " + TextBox5.Text.Replace("'", "").Trim() + "</td>";
            Acknowledge = Acknowledge + " <td>";
            Acknowledge = Acknowledge + "  &nbsp;</td>";
            Acknowledge = Acknowledge + "</tr>";
            Acknowledge = Acknowledge + " <tr>";
            Acknowledge = Acknowledge + "   <td>";
            Acknowledge = Acknowledge + "    <hr /></td>";
            Acknowledge = Acknowledge + " </tr>";
            //Acknowledge = Acknowledge + rrs.EmailSignature();
            Acknowledge = Acknowledge + " </table>";
            //if (!string.IsNullOrEmpty(TextBox3.Text.Replace("'", "").Trim()))
            //{
            //    rrs.EmailSending(Acknowledge.ToString(), "Acknowledgement of Feedback from redrosepublicschool.edu.in", TextBox3.Text.Replace("'", "").Trim());
            //}
            clear();
           // obj.MessageBox("Submitted successfully.", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");
            oo.ClearControls(this.Page);
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Submitted successfully.')", true);
            oo.MessageBox("", this.Page);
            TextBox1.Focus();
        }
        catch
        {

        }
    }
    protected void lnkReset_Click(object sender, EventArgs e)
    {
        clear();
    }
    public void clear()
    {
        TextBox1.Text = "";
        TextBox2.Text = "";
        TextBox3.Text = "";
        TextBox4.Text = "";
        TextBox5.Text = "";
    }

}