using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_TicketAllotment : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadMyConversation();
        }
    }

    public void loadMyConversation()
    {
        List<SqlParameter> param = new List<SqlParameter>();

        param.Add(new SqlParameter("@Queryfor", "S"));

        rpt.DataSource = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("USP_Get_AllRefrenceNo", param);
        rpt.DataBind();
    }

    protected void rpt_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DropDownList drpUserList=(DropDownList)e.Item.FindControl("drpUserList");
            DropDownList drpAssignTo=(DropDownList)e.Item.FindControl("drpAssignTo");
            BLL.BLLInstance.loadLoginUsersList(drpUserList, drpAssignTo.SelectedValue.ToString());
        }
    }
    protected void drpAssignTo_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList drpAssignTo = (DropDownList)sender;

        DropDownList drpUserList = (DropDownList)drpAssignTo.NamingContainer.FindControl("drpUserList");
        
        BLL.BLLInstance.loadLoginUsersList(drpUserList, drpAssignTo.SelectedValue.ToString());
    }
    protected void lnkView_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        Label lblrefno = (Label)lnk.NamingContainer.FindControl("lblrefno");
        DropDownList drpUserList = (DropDownList)lnk.NamingContainer.FindControl("drpUserList");
        if (drpUserList.SelectedIndex == 0)
        {
            Session["Assigntouser"] = Session["LoginName"].ToString();
        }
        else
        {
            Session["Assigntouser"] = drpUserList.SelectedItem.Text.ToString();
        }
        Response.Redirect("~/common/TicketReply.aspx?print=1&REFNO=" + lblrefno.Text.Trim() + "");
    }
    protected void lnkUpdate_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        Label lblrefno = (Label)lnk.NamingContainer.FindControl("lblrefno");
        DropDownList drpUserList = (DropDownList)lnk.NamingContainer.FindControl("drpUserList");
        string assignto = drpUserList.SelectedItem.Text.ToString();
        ticketAssign(lblrefno.Text.Trim(), assignto);
        loadMyConversation();
    }

    protected string ticketAssign(string refno,string assignto)
    {
        string msg = "";

        List<SqlParameter> param = new List<SqlParameter>();

        param.Add(new SqlParameter("@REFNO", refno));
        param.Add(new SqlParameter("@AssignBy", Session["LoginName"].ToString()));
        param.Add(new SqlParameter("@AssignTo", assignto));

        SqlParameter para = new SqlParameter("@Msg", "");
        para.Direction = ParameterDirection.Output;
        para.Size = 0x100;

        param.Add(para);

        msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("USP_TicketAssign", param);

        return msg;
    }
}