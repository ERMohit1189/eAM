using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class common_TicketReply : System.Web.UI.Page
{
    string sql = string.Empty;
    string filePath = "";

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
            loadMyConversation();
        }
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

            //string base64std = hidFile.Value;
            //if (base64std != string.Empty)
            //{
            //    string fileName = "";
            //    //string fileExtention = hidFileExt.Value;

            //    filePath = @"../Uploads/Attachment/";
            //    string datetime = DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss");
            //    fileName = String.Format("{0}", datetime + fileExtention);
            //    filePath = filePath + fileName;
            //    using (FileStream fs = new FileStream(Server.MapPath(filePath), FileMode.Create))
            //    {
            //        using (BinaryWriter bw = new BinaryWriter(fs))
            //        {
            //            byte[] data = Convert.FromBase64String(base64std);
            //            bw.Write(data);
            //            bw.Close();
            //        }
            //    }
            //    Attachment attech = new Attachment(Server.MapPath(filePath));
            //    mail.Attachments.Add(attech);
            //}

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

        string refno = Request.QueryString["REFNO"].ToString();
        
        param.Add(new SqlParameter("@refno", refno));

        rpt1.DataSource = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("USP_Get_FirstTicketConverTation", param);
        rpt1.DataBind();

        loadReplyConversation();
    }
    protected void lnkAttechment_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        Label lblDocPath = (Label)lnk.NamingContainer.FindControl("lblPath");

        Response.ContentType = ContentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(lblDocPath.Text));
        Response.WriteFile(lblDocPath.Text);
        Response.End();
    }
    protected void lnkReply_Click(object sender, EventArgs e)
    {
        LinkButton lnkReply = (LinkButton)sender;
        Label lblid = (Label)lnkReply.NamingContainer.FindControl("lblid");

        HiddenField hfid = (HiddenField)lnkReply.NamingContainer.FindControl("hfid");
        HtmlGenericControl divReply = (HtmlGenericControl)lnkReply.NamingContainer.FindControl("divReply");

        divReply.Visible = true;

        hfid.Value = lblid.Text.Trim();

        ScriptManager.RegisterStartupScript(this.Page, GetType(), "summernote", "txtAreaHtml()", true);
    }
    protected void lnkSubmitReply_Click(object sender, EventArgs e)
    {
        LinkButton lnkSubmitReply = (LinkButton)sender;
        HiddenField hidFileReply = (HiddenField)lnkSubmitReply.NamingContainer.FindControl("hidFileReply");
        HiddenField hidFileExtReply = (HiddenField)lnkSubmitReply.NamingContainer.FindControl("hidFileExtReply");
        HiddenField hfid = (HiddenField)lnkSubmitReply.NamingContainer.FindControl("hfid");

        TextBox txtReply = (TextBox)lnkSubmitReply.NamingContainer.FindControl("txtReply");
        HtmlGenericControl msgbox1 = (HtmlGenericControl)lnkSubmitReply.NamingContainer.FindControl("msgbox1");



        string msg = "";

        string base64std = hidFileReply.Value;
        if (base64std != string.Empty)
        {
            string fileName = "";
            string fileExtention = hidFileExtReply.Value;

            filePath = @"../Uploads/TicketAttechment/";
            string datetime = DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss");
            fileName = String.Format("{0}", datetime + "_Reply" + fileExtention);
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
        }

        msg = insertReply(Request.QueryString["REFNO"].ToString(), txtReply.Text.Trim(), filePath);

        if (msg == "S")
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox1, "Sent", "S");
            loadMyConversation();
        }
        else
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox1, "Sorry", "W");
        }

       // ScriptManager.RegisterStartupScript(lnkSubmitReply, GetType(), "summernote", "txtAreaHtml()", true);
    }

    protected string insertReply(string refno, string reply, string attechmentPath)
    {
        string msg = "";

        List<SqlParameter> param = new List<SqlParameter>();

        param.Add(new SqlParameter("@REFNO", refno));
        param.Add(new SqlParameter("@ReplyMsg", reply));       
        param.Add(new SqlParameter("@Replyby", Session["LoginName"].ToString()));
        param.Add(new SqlParameter("@AttechmentPath", attechmentPath));
        param.Add(new SqlParameter("@LogintypeId", Session["LogintypeId"].ToString()));

        if (Session["Assigntouser"] != null)
        {
            param.Add(new SqlParameter("@AssignBy", Session["LoginName"].ToString()));
            param.Add(new SqlParameter("@AssignTo", Session["Assigntouser"].ToString()));
        }

        SqlParameter para = new SqlParameter("@Msg", "");
        para.Direction = ParameterDirection.Output;
        para.Size = 0x100;

        param.Add(para);

        msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("USP_TicketReply", param);

        return msg;
    }
    protected void lnkReplyAttechment_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        Label lblReplyAttechmentPath = (Label)lnk.NamingContainer.FindControl("lblReplyAttechmentPath");

        Response.ContentType = ContentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(lblReplyAttechmentPath.Text));
        Response.WriteFile(lblReplyAttechmentPath.Text);
        Response.End();
    }

    public void loadReplyConversation()
    {
        foreach (RepeaterItem ri in rpt1.Items)
        {
            Repeater rptReply = (Repeater)ri.FindControl("rptReply");

            List<SqlParameter> param = new List<SqlParameter>();

            string refno = Request.QueryString["REFNO"].ToString();

            param.Add(new SqlParameter("@refno", refno));

            rptReply.DataSource = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("USP_Get_TicketConverTation", param);
            rptReply.DataBind();
        }
    }
    protected void lnkcattaReply_Click(object sender, EventArgs e)
    {
        LinkButton lnkcattaReply = (LinkButton)sender;
        HiddenField hidFileReply = (HiddenField)lnkcattaReply.NamingContainer.FindControl("hidFileReply");
        HiddenField hidFileExtReply = (HiddenField)lnkcattaReply.NamingContainer.FindControl("hidFileExtReply");

        hidFileReply.Value = string.Empty;
        hidFileExtReply.Value = string.Empty;
    }
}