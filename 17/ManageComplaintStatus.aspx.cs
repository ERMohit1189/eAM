using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class admin_ManageComplaintStatus : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();

    protected void Page_PreInIt(object sender, EventArgs e)
    {
        if (Session["Logintype"] == null) { Response.Redirect("~/default.aspx"); }
        if (Session["Logintype"].ToString() == "Admin")
        {
            this.MasterPageFile = "~/Master/admin_root-manager.master";
        }
        else if (Session["Logintype"].ToString() == "Administrator")
        {
            this.MasterPageFile = "~/Administrator/administrato_root-manager.master";
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);
        if (!IsPostBack)
        {
            loadMyConversation("","1");
        }
    }
   
    public void loadMyConversation(string refno,string status)
    {
        try
        {
            if(status == String.Empty)
            {
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@ConversationID", refno));
                param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
                param.Add(new SqlParameter("@Status", status));
                param.Add(new SqlParameter("@QueryFor", "C"));
                rpt.DataSource = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("USP_ConversationList", param);
                rpt.DataBind();
            }
           else  if (refno == String.Empty && status!= String.Empty)
            {
                if(status != "Both")
                {
                    List<SqlParameter> param = new List<SqlParameter>();
                    param.Add(new SqlParameter("@ConversationID", refno));
                    param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
                    param.Add(new SqlParameter("@Status", status));
                    param.Add(new SqlParameter("@QueryFor", "S"));
                    rpt.DataSource = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("USP_ConversationList", param);
                    rpt.DataBind();
                }
               else
                {
                    status = String.Empty;
                    List<SqlParameter> param = new List<SqlParameter>();
                    param.Add(new SqlParameter("@ConversationID", refno));
                    param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
                    param.Add(new SqlParameter("@Status", status));
                    param.Add(new SqlParameter("@QueryFor", "C"));
                    rpt.DataSource = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("USP_ConversationList", param);
                    rpt.DataBind();
                }
            }
            

            //sql = "select ConversationID,Id,Subject,Discription,AttechmentPath,RecordDate,UserName,SentTo,UserType,ISNULL(UpdatedDate, RecordDate) UpdatedDate ,IsActive FROM TeacherGuardianConversation ORDER BY RecordDate DESC";
            //rpt.DataSource = oo.GridFill(sql);
            //rpt.DataBind();
        }
        catch (Exception) { }
    }

    protected void lnkView_Click(object sender, EventArgs e)
    {
        
            //LinkButton lnk = (LinkButton)sender;
            //Label lblrefno = (Label)lnk.NamingContainer.FindControl("lblrefno");
            //Response.Redirect("~/master/common/TicketReply.aspx?REFNO=" + lblrefno.Text.Trim() + "");
       
    }

    protected void lnkUpdate_Click(object sender, EventArgs e)
    {
        //try
        //{
        //    string msg = "";

        //    LinkButton lnk = (LinkButton)sender;
        //    Label lblrefno = (Label)lnk.NamingContainer.FindControl("lblrefno");

        //    DropDownList drpClose = (DropDownList)lnk.NamingContainer.FindControl("drpClose");

        //    List<SqlParameter> param = new List<SqlParameter>();

        //    param.Add(new SqlParameter("@conid", lblrefno.Text.Trim()));
        //    param.Add(new SqlParameter("@Status", drpClose.SelectedIndex == 0 ? 1 : 0));

        //    SqlParameter para = new SqlParameter("@Msg", "");
        //    para.Direction = ParameterDirection.Output;
        //    param.Add(para);

        //    msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("USP_UpdateConversationStatus", param);

        //    if (msg == "S")
        //    {
        //        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated Successfully.", "S");
        //    }
        //    else
        //    {
        //        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, msg, "W");
        //    }
        //}
        //catch (Exception ex) { }
    }
    protected void drpClose_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //DropDownList drpClose = (DropDownList)sender;
            //LinkButton lnkUpdate = (LinkButton)drpClose.NamingContainer.FindControl("lnkUpdate");

            //if (drpClose.SelectedIndex == 1)
            //{
            //    lnkUpdate.Visible = true;
            //}
            //else
            //{
            //    lnkUpdate.Visible = false;
            //}
        }
        catch (Exception) { }
    }
    protected void lnkSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            loadMyConversation(txtRefNo.Text.Trim(),"");
        }
        catch (Exception) { }
    }

    protected void rpt_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Label lblStatus = (Label)e.Item.FindControl("lblStatus");
            DropDownList drpClose = (DropDownList)e.Item.FindControl("drpClose");
            if (lblStatus.Text.Trim().ToString() == "True")
            {
                drpClose.SelectedIndex = 0;
            }
            else
            {
                drpClose.SelectedIndex = 1;
            }

        }
    }



    protected void ddlstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            loadMyConversation("", ddlstatus.SelectedValue);
        }
        catch (Exception) { }
    }

    protected void CheckBox1_OnCheckedChanged(object sender, EventArgs e)
    {
        try
        {
            var chk = (CheckBox)sender;
            var lblrefno = (Label)chk.NamingContainer.FindControl("lblrefno");

            //var drpClose = (DropDownList)chk.NamingContainer.FindControl("drpClose");

            var param = new List<SqlParameter>
            {
                new SqlParameter("@conid", lblrefno.Text.Trim()),
                new SqlParameter("@Status",chk.Checked)
            };


            var para = new SqlParameter("@Msg", "") {Direction = ParameterDirection.Output};
            param.Add(para);

            var msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("USP_UpdateConversationStatus", param);

            if (msg == "S")
            {
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated Successfully.", "S");
            }
            else
            {
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, msg, "W");
            }
        }
        catch (Exception)
        {
            // ignored
        }
    }
}