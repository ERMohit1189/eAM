using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class common_ViewTicket : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadMyConversation("");
        }
    }

    public void loadMyConversation(string refno)
    {
        List<SqlParameter> param = new List<SqlParameter>();

        param.Add(new SqlParameter("@AssignTo", Session["LoginName"].ToString()));
        param.Add(new SqlParameter("@REFNO", refno));

        rpt.DataSource = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("USP_Get_AssignTicketList", param);
        rpt.DataBind();
    }

    protected void lnkView_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        Label lblrefno = (Label)lnk.NamingContainer.FindControl("lblrefno");
        Response.Redirect("~/common/TicketReply.aspx?print=1&REFNO=" + lblrefno.Text.Trim() + "");
    }

    protected void lnkUpdate_Click(object sender, EventArgs e)
    {
        string msg = "";

        LinkButton lnk = (LinkButton)sender;
        Label lblrefno = (Label)lnk.NamingContainer.FindControl("lblrefno");

        List<SqlParameter> param = new List<SqlParameter>();

        param.Add(new SqlParameter("@REFNO", lblrefno.Text.Trim()));
        param.Add(new SqlParameter("@Status", 1));

        SqlParameter para = new SqlParameter("@Msg", "");
        para.Direction = ParameterDirection.Output;
        param.Add(para);

        msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("USP_UpdateTicketStatus", param);

        if (msg == "S")
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated Successfully.", "S");
            loadMyConversation(txtRefNo.Text.Trim());
        }
        else
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, msg, "W");
        }
    }
    protected void drpClose_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList drpClose = (DropDownList)sender;
        LinkButton lnkUpdate = (LinkButton)drpClose.NamingContainer.FindControl("lnkUpdate");

        if (drpClose.SelectedIndex == 1)
        {
            lnkUpdate.Visible = true;
        }
        else
        {
            lnkUpdate.Visible = false;
        }
    }
    protected void lnkSubmit_Click(object sender, EventArgs e)
    {
        loadMyConversation(txtRefNo.Text.Trim());
    }
}