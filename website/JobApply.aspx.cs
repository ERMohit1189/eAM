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


public partial class website_JobApply : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";
    string ss = ""; string message = "";
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

    public void loadJob()
    {
        sql = "Select JobTitle from PostJob";
        oo.FillDropDown(sql, DropDownList2, "JobTitle");
    }

    public void Selectjobtitel()
    {
        if (Session["JobId"] != null)
        {
            sql = "Select JobTitle from PostJob where JobId=" + Session["JobId"];
            string jobtitle = oo.ReturnTag(sql, "JobTitle");
            if (jobtitle != "")
            {
                DropDownList2.SelectedValue = jobtitle;
            }
        }
    }


   

    public void Apply_insert()
    {
        try
        {
            Random RandNo = new Random();
            string rndno = RandNo.Next(1, 100000).ToString();
            string filename = DropDownList2.Text.ToString().Replace("/", "") + rndno + "_" + FileUpload1.FileName;
            FileUpload1.PostedFile.SaveAs(Server.MapPath("~/uploads/docs/cv/" + filename));

            string msgpass = "";// BLL.obj_bll1.set_ApplyJob(TextBox1.Text.Replace("'", "").Trim(), TextBox2.Text.Replace("'", "").Trim(), TextBox3.Text.Replace("'", "").Trim(), DropDownList1.Text.Replace("'", "").Trim(), DropDownList2.SelectedItem.ToString(), TextBox6.Text.Replace("'", "").Trim(), "~/uploads/docs/cv/" + filename);

          
            message = "<table align=\"left\" class=\"style1\">";
            message = message + "   </tr>";
            message = message + " <tr>";
            message = message + "        <td>";
            message = message + "          Name :</td>";
            message = message + "      <td>";
            message = message + "          " + TextBox1.Text.Replace("'", "").Trim() + "</td>";
            message = message + "   </tr>";
            message = message + "   <tr>";
            message = message + "      <td>";
            message = message + "          Contact No. :</td>";
            message = message + "      <td>";
            message = message + "        " + TextBox2.Text.Replace("'", "").Trim() + "</td>";
            message = message + "   </tr>";
            message = message + "   <tr>";
            message = message + "       <td>";
            message = message + "           E-mail :</td>";
            message = message + "      <td>";
            message = message + "         " + TextBox3.Text.Replace("'", "").Trim() + "</td>";
            message = message + "   </tr>";
            message = message + "  <tr>";
            message = message + "      <td>";
            message = message + "        How do know about us :</td>";
            message = message + "    <td>";
            message = message + "        " + DropDownList1.Text + "</td>";
            message = message + "   </tr>";
            message = message + "  <tr>";
            message = message + "      <td>";
            message = message + "         Applied For : </td>";
            message = message + "    <td>";
            message = message + "        " + DropDownList2.SelectedItem.ToString() + "</td>";
            message = message + "   </tr>";
            message = message + "  <tr>";
            message = message + "      <td>";
            message = message + "         About Candidate :</td>";
            message = message + "    <td>";
            message = message + "        " + TextBox6.Text.Replace("'", "").Trim() + "</td>";
            message = message + "   </tr>";
            message = message + "  </table>";
            //rrs.EmailSendingWithAttech(message, "New Job Enquiry from www.redrosepublicschool.edu.in", "apply@redrosepublicschool.edu.in", Server.MapPath("~/uploads/docs/cv/" + filename));

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
            Acknowledge = Acknowledge + "  How do you know about us : " + DropDownList1.SelectedItem.Text + "</td>";
            Acknowledge = Acknowledge + "<td>";
            Acknowledge = Acknowledge + "  &nbsp;</td>";
            Acknowledge = Acknowledge + "</tr>";
            Acknowledge = Acknowledge + "<tr>";
            Acknowledge = Acknowledge + "  <td>";
            Acknowledge = Acknowledge + "  Applied for : " + DropDownList2.SelectedItem.Text + "</td>";
            Acknowledge = Acknowledge + "<td>";
            Acknowledge = Acknowledge + "  &nbsp;</td>";
            Acknowledge = Acknowledge + "</tr>";
            Acknowledge = Acknowledge + "<tr>";
            Acknowledge = Acknowledge + "  <td>";
            Acknowledge = Acknowledge + "  About Candidate : " + TextBox6.Text.Replace("'", "").Trim() + "</td>";
            Acknowledge = Acknowledge + "<td>";
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
            //    rrs.EmailSending(Acknowledge.ToString(), "Acknowledgement of Job Enquiry from redrosepublicschool.edu.in", TextBox3.Text.Trim().Replace("'", ""));
            //}
            clear();
            TextBox1.Focus();

            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");
            oo.ClearControls(this.Page);
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Submitted successfully.')", true);
            oo.MessageBox("", this.Page);


            // oo.MessageBox("Job Application Submitted Successfully", this.Page);
        }
        catch
        {
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            if (FileUpload1.HasFile)
            {
                if (FileUpload1.FileBytes.Length < 2048) // 1024*KB of file size
                {
                   oo.MessageBox("C V File Size Is More Then 2 MB So Please Select File Less then 2 MB ", this.Page);

                    //Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "C V File Size Is More Then 2 MB So Please Select File Less then 2 MB", "W");
                    //oo.ClearControls(this.Page);

                    //oo.MessageBox("", this.Page);
                }
                else
                {
                    Apply_insert();
                }

            }
            else
            {
                Apply_insert();
            }
        }
        catch { con.Close(); }
        finally { con.Close(); }
    }

    protected void clear()
    {
        TextBox1.Text = TextBox2.Text = TextBox3.Text = TextBox4.Text = TextBox6.Text = "";
    }

}