using System;
using System.Collections.Generic;
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

        param.Add(new SqlParameter("@REFNO", refno));

        rpt.DataSource = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("USP_Get_AllRefrenceNoList", param);
        rpt.DataBind();

        if (rpt.Items.Count == 0)
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sory No Record(s) found!", "A");
        }
    }

    protected void lnkView_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        Label lblrefno = (Label)lnk.NamingContainer.FindControl("lblrefno");
        Response.Redirect("~/common/TicketReply.aspx?print=1&REFNO=" + lblrefno.Text.Trim() + "");
    }
    protected void lnkSubmit_Click(object sender, EventArgs e)
    {
        loadMyConversation(txtRefNo.Text.Trim());
    }
}