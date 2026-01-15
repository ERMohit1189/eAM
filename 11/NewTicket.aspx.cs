using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class common_NewTicket : System.Web.UI.Page
{
    string filePath = string.Empty;

    protected void Page_PreInIt(object sender, EventArgs e)
    {
        if (Session["Logintype"] == null) { Response.Redirect("~/default.aspx"); }
        if (Session["Logintype"].ToString() == "Admin")
        {
            this.MasterPageFile = "~/Master/admin_root-manager.master";
        }
        else if (Session["Logintype"].ToString() == "Staff")
        {
            this.MasterPageFile = "~/Staff/staff_root-manager.master";
        }
        else if (Session["Logintype"].ToString() == "Guardian")
        {
            this.MasterPageFile = "~/sp/sp_root-manager.master";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Campus camp = new Campus(); camp.LoadLoader(loader);

        if (!IsPostBack)
        {
        }
    }
    protected void rblist1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblist1.SelectedIndex == 0)
        {
            div1.Visible = true;
            div2.Visible = false;
        }
        else
        {
            div1.Visible = false;
            div2.Visible = true;

            loadMyConversation();
        }
        ScriptManager.RegisterStartupScript(rblist1, GetType(), "summernote", "txtAreaHtml()", true);
    }
    protected void lnkcatta_Click(object sender, EventArgs e)
    {
        hidFile.Value = string.Empty;
        hidFileExt.Value = string.Empty;
    }
    protected void lnkSubmit_Click(object sender, EventArgs e)
    {
        string subject = txtSubject.Text.Trim();
        string msg = txtDescription.Text.Trim();
        string logintypeid = Session["LoginTypeId"].ToString();
        string initiateby = Session["LoginName"].ToString();
        string incategory = drpCategory.SelectedItem.Text.Trim();

        try
        {
            Thread thread = new Thread(() => { EmailSending(subject, msg, "mohit@axonitservices.com"); });
            //Thread thread1 = new Thread(() => { SendFeesSms(subject, txtMobileno.Text.Trim()); });

            thread.IsBackground = true;
            //thread1.IsBackground = true;

            thread.Start(); 
            //thread1.Start();

            System.Threading.Thread.Sleep(1000);
        }
        catch
        {
        }

        try
        {
            insert(subject, msg, logintypeid, initiateby, incategory, filePath);
        }
        catch
        {
        }

        ScriptManager.RegisterClientScriptBlock(lnkSubmit, GetType(), "summernote", "txtAreaHtml()", true);
    }

    protected string insert(string subject, string msg, string logintype, string initiatby, string inCategory, string attechmentPath)
    {
        string action = "";

        List<SqlParameter> param = new List<SqlParameter>();

        param.Add(new SqlParameter("@subject", subject));
        param.Add(new SqlParameter("@msg", msg));
        param.Add(new SqlParameter("@initiatby", initiatby));
        param.Add(new SqlParameter("@inCategory", inCategory));
        param.Add(new SqlParameter("@attechmentPath", attechmentPath));

        SqlParameter para = new SqlParameter("@action", "");
        para.Direction = ParameterDirection.Output;
        para.Size = 0x100;

        param.Add(para);

        action = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("USP_C01_ComplainRefNoInitiation", param);


        return action;
    }

    public bool EmailSending(string subject, string msg, string emaidId)
    {
        bool flag = false;

        if (emaidId != string.Empty)
        {

            MailMessage mail = new MailMessage();

            emaidId = emaidId.Replace("\n", "");

            mail.To.Add(emaidId);//to ID

            mail.From = new MailAddress("donotreply@eam.co.in");
            mail.Subject = subject;

            mail.BodyEncoding = Encoding.UTF8;
            mail.SubjectEncoding = Encoding.UTF8;

            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(msg);
            htmlView.ContentType = new System.Net.Mime.ContentType("text/html");
            mail.AlternateViews.Add(htmlView);

            string base64std = hidFile.Value;
            if (base64std != string.Empty)
            {
                string fileName = "";
                string fileExtention = hidFileExt.Value;

                filePath = @"../Uploads/TicketAttechment/";
                string datetime = DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss");
                fileName = String.Format("{0}", datetime + fileExtention);
                filePath = filePath + fileName;
                // ReSharper disable once AssignNullToNotNullAttribute
                using (FileStream fs = new FileStream(Server.MapPath(filePath), FileMode.Create))
                {
                    using (BinaryWriter bw = new BinaryWriter(fs))
                    {
                        byte[] data = Convert.FromBase64String(base64std);
                        bw.Write(data);
                        bw.Close();
                    }
                }
                // ReSharper disable once AssignNullToNotNullAttribute
                Attachment attech = new Attachment(Server.MapPath(filePath));
                mail.Attachments.Add(attech);
            }

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com"; //Or Your SMTP Server Address
            smtp.Port = 587;
            smtp.Credentials = new System.Net.NetworkCredential("donotreply@eam.co.in", "reNply_33@9D");//from id
            //Or your Smtp Email ID and Password
            smtp.EnableSsl = true;
            try
            {
                smtp.Send(mail);
                flag = true;
            }
            catch (Exception) { }

            Thread.Yield();
        }

        return flag;
    }

    public void loadMyConversation()
    {
        List<SqlParameter> param = new List<SqlParameter>();

        string initiateby = Session["LoginName"].ToString();

        param.Add(new SqlParameter("@Initiateby", initiateby));

        rpt.DataSource = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("USP_Get_FirstTicketConverTationByUSER", param);
        rpt.DataBind();
    }

    protected void lnkView_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        Label lblrefno = (Label)lnk.NamingContainer.FindControl("lblrefno");
        Response.Redirect("~/common/TicketReply.aspx?print=1&REFNO=" + lblrefno.Text.Trim() + "");
    }
    protected void lnkrefno_Click(object sender, EventArgs e)
    {

    }
}