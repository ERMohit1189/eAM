using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class admin_frm_CreateDocumentName : Page
{
    BAL.Staff_Document obj = new BAL.Staff_Document();
    BAL.textBoxList textBoxList = new BAL.textBoxList();
    //BAL.Get_Class obj1 = new BAL.Get_Class();
    DataTable dt;
    Campus oo = new Campus();
    Message message = new Message();
#pragma warning disable 169
    List<string> list;
#pragma warning restore 169
#pragma warning disable 169
    string msg;
#pragma warning restore 169
    protected void Page_Load(object sender, EventArgs e)
    {
        if ( Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        if (!IsPostBack)
        {
            Get_DocumentName(this.Page);           
        }
    }
    
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (txtDocumentName.Text.Trim() != string.Empty)
        {
            Set_DocumentName(btnSubmit);
            Get_DocumentName(btnSubmit);
        }
        else
        {
            textBoxList.Noofnoncleartxt = 0;
            //oo.MessageBoxforUpdatePanel(message.MessageType("E", table1, textBoxList), btnSubmit);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, message.MessageType("E", table1, textBoxList), "A");       

        }
    }
    protected void Set_DocumentName(Control ctrl)
    {
        string msg = "";
        try
        {
           
                obj.Type = "I";
                obj.Id = 0;
                obj.DocumentType = txtDocumentName.Text.Trim();
                obj.Session = Session["SessionName"].ToString();
                obj.BranchCode = Convert.ToInt16(Session["BranchCode"].ToString());
                obj.LoginName = Session["LoginName"].ToString();
                msg = new DAL().Set_StaffDocumentType(obj);
           
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        
        //oo.MessageBoxforUpdatePanel(message.MessageType(msg, table1, textBoxList), btnSubmit);
        //oo.MessageBoxforUpdatePanel(message.MessageType(msg, table1, textBoxList), btnSubmit);
        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, message.MessageType(msg, table1, textBoxList), "S");       


       
    }
    protected void Get_DocumentName(Control ctrl)
    {
        obj.Session = Session["SessionName"].ToString();
        obj.BranchCode = Convert.ToInt16(Session["BranchCode"].ToString());
        obj.Id = 0;
        grdDetails.DataSource = new DAL().Get_StaffDocumentType(obj).Item1;
        grdDetails.DataBind();
        textBoxList.Noofnoncleartxt = 0;
        if(grdDetails.Rows.Count<=0)
        {
            //oo.MessageBoxforUpdatePanel(message.MessageType("N", ctrl, textBoxList), ctrl);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, message.MessageType("N", ctrl, textBoxList), "S");       


        }

    }
    protected void Update_DocumentName(Control ctrl)
    {
        string msg = "";
        try
        {
            obj.Type = "U";
            obj.Id = Convert.ToInt16(Session["Id"].ToString());
            obj.DocumentType = txtDocumentTypePanel.Text.Trim();
            obj.Session = Session["SessionName"].ToString();
            obj.BranchCode = Convert.ToInt16(Session["BranchCode"].ToString());
            obj.LoginName = Session["LoginName"].ToString();
            msg = new DAL().Set_StaffDocumentType(obj);

        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }

        //oo.MessageBoxforUpdatePanel(message.MessageType(msg, table1, textBoxList), btnSubmit);
        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, message.MessageType(msg, table1, textBoxList), "S");       

        Session["Id"] = "";
        Session["ClassName"] = "";
    }
   
    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lnk = (Label)chk.NamingContainer.FindControl("Label36");
       
        Session["Id"] = lnk.Text;
        try
        {
            obj.Id = Convert.ToInt16(Session["Id"].ToString());
            obj.Session = Session["SessionName"].ToString();
            obj.BranchCode = Convert.ToInt16(Session["BranchCode"].ToString());
            dt = new DataTable();
            dt=new DAL().Get_StaffDocumentType(obj).Item1;
            txtDocumentTypePanel.Text = dt.Rows[0]["DocumentType"].ToString();
            Panel1_ModalPopupExtender.Show();
        }
        catch (Exception ex)
        {
            textBoxList.Noofnoncleartxt = 0;
            //oo.MessageBoxforUpdatePanel(message.MessageType(ex.Message, lnkEdit, textBoxList), lnkEdit);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, message.MessageType(ex.Message, lnkEdit, textBoxList), "A");       

        }
    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        lnkNo.Focus();
        LinkButton chk = (LinkButton)sender;
        Label lnk = (Label)chk.NamingContainer.FindControl("Label37");
        Session["Id"] = lnk.Text;
        Panel2_ModalPopupExtender.Show();
    }
    protected void lnkYes_Click(object sender, EventArgs e)
    {
        Delete_DocumentName(lnkDelete);
        Get_DocumentName(lnkUpdate);
    }
    protected void Delete_DocumentName(Control ctrl)
    {
        string msg = "";
        try
        {
            obj.Type = "D";
            obj.Id = Convert.ToInt16(Session["Id"].ToString());
            obj.DocumentType = "";
            obj.Session = Session["SessionName"].ToString();
            obj.BranchCode = Convert.ToInt16(Session["BranchCode"].ToString());
            obj.LoginName = "";
            msg = new DAL().Set_StaffDocumentType(obj);

        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }

        //oo.MessageBoxforUpdatePanel(message.MessageType(msg, table1, textBoxList), btnSubmit);
        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, message.MessageType(msg, table1, textBoxList), "S");       

        Session["Id"] = "";
    }
    protected void lnkUpdate_Click(object sender, EventArgs e)
    {
        Update_DocumentName(lnkUpdate);
        Get_DocumentName(lnkUpdate);
    }
}