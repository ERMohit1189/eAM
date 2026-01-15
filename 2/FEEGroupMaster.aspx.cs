using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class admin_FeeGroupMaster : Page
{
    DataView dv;
    BAL.GenralInfo objBal = new BAL.GenralInfo();
    Campus oo = new Campus();
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((string)Session["LoginName"] == "" || (string)Session["BranchCode"] == "" || (string)Session["SessionName"] == "" || Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        

        Campus camp = new Campus(); camp.LoadLoader(loader);
        if (!IsPostBack)
        {
            txtFeeGroup.Focus();
            loadGrid();
        }
    }
    protected void lnkSubmit_Click(object sender, EventArgs e)
    {
        Page.Validate();
        if (Page.IsValid)
        {
            string msg;
            try
            {
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("Queryfor", "I"));
                param.Add(new SqlParameter("FeeGroupName", txtFeeGroup.Text.Trim()));
                param.Add(new SqlParameter("Remark", txtRemark.Text.Trim()));
                param.Add(new SqlParameter("SessionName", Session["SessionName"].ToString()));
                param.Add(new SqlParameter("BranchCode", Session["BranchCode"].ToString()));
                param.Add(new SqlParameter("LoginName", Session["LoginName"].ToString()));
                SqlParameter para = new SqlParameter("@Msg", "");
                para.Direction = ParameterDirection.Output;
                para.Size = 0x100;
                param.Add(para);
                msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("FeeGroupMasterProc", param);

            }
            catch (Exception ex)
            {
                msg = ex.Message;
                throw ex;
            }

            //BAL.objBal.MessageBox(objBal.MessageType(msg, new Control(), new BAL.textBoxList()), this.Page);
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, objBal.MessageType(msg, new Control(), new BAL.textBoxList()), "S");                                    
                            

            loadGrid();
            BAL.objBal.ClearControls(table);
            txtFeeGroup.Focus();
        }
    }
    private void loadGrid()
    {
        try
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("Queryfor", "S"));
            param.Add(new SqlParameter("SessionName", Session["SessionName"].ToString()));
            param.Add(new SqlParameter("BranchCode", Session["BranchCode"].ToString()));
            Repeater1.DataSource = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("FeeGroupMasterProc", param).Tables[0];
            Repeater1.DataBind();
            
            for (int i = 0; i < Repeater1.Items.Count; i++)
            {
                LinkButton lnkDelete = (LinkButton)Repeater1.Items[i].FindControl("lnkDelete");
                Label id = (Label)Repeater1.Items[i].FindControl("Label4");
                string sql = "select count(*) cnt from MonthMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] +" and CardType="+ id.Text + "";
                if (oo.ReturnTag(sql, "cnt").ToString()!="0" && oo.ReturnTag(sql, "cnt").ToString() != "")
                {
                    lnkDelete.Text = "<i class='fa fa-lock'></i>";
                    lnkDelete.Enabled = false;
                }
            }
        }
        catch (Exception ex)
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, objBal.MessageType(ex.Message, new Control(), new BAL.textBoxList()), "S");                                    
        }
    }
    protected void lnkUpdate_Click(object sender, EventArgs e)
    {
        Page.Validate();
        if (Page.IsValid)
        {
            string msg;
            try
            {
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("Queryfor", "U"));
                param.Add(new SqlParameter("Id", Session["lblEditId"].ToString().Trim()));
                param.Add(new SqlParameter("FeeGroupName", txtFeeGroupPanel.Text.Trim()));
                param.Add(new SqlParameter("Remark", txtRemarkPanel.Text.Trim()));
                param.Add(new SqlParameter("SessionName", Session["SessionName"].ToString()));
                param.Add(new SqlParameter("BranchCode", Session["BranchCode"].ToString()));
                SqlParameter para = new SqlParameter("@Msg", "");
                para.Direction = ParameterDirection.Output;
                para.Size = 0x100;
                param.Add(para);
                msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("FeeGroupMasterProc", param);

            }
            catch (Exception ex)
            {
                msg = ex.Message;
                throw ex;
            }

            //BAL.objBal.MessageBox(objBal.MessageType(msg, new Control(), new BAL.textBoxList()), this.Page);
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, objBal.MessageType(msg, new Control(), new BAL.textBoxList()), "S");                                    

            loadGrid();
            BAL.objBal.ClearControls(Panel1);
            Session["lblEditId"] = "";
            txtFeeGroup.Focus();
        }
    }
    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        RepeaterItem currentRow = (RepeaterItem)lnk.NamingContainer;
        Label lblid = (Label)currentRow.FindControl("Label4");
        Session["lblEditId"] = lblid.Text.Trim();
        DataTable dt = new DataTable();      
        try
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("Queryfor", "S"));
            param.Add(new SqlParameter("SessionName", Session["SessionName"].ToString()));
            param.Add(new SqlParameter("BranchCode", Session["BranchCode"].ToString()));
            dt = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("FeeGroupMasterProc", param).Tables[0];
            dv = new DataView(dt);
            dv.RowFilter = "Id='" + lblid.Text.Trim() + "'";
        }
        catch (Exception ex)
        {
            //BAL.objBal.MessageBox(objBal.MessageType(ex.Message, new Control(), new BAL.textBoxList()), this.Page);
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, objBal.MessageType(ex.Message, new Control(), new BAL.textBoxList()), "S");                                    

        }
        if (dv.Count > 0)
        {
            txtFeeGroupPanel.Text = dv[0][0].ToString();
            txtRemarkPanel.Text = dv[0][1].ToString();
            Panel1_ModalPopupExtender.Show();
        }
    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        lnkNo.Focus();
        LinkButton lnk = (LinkButton)sender;
        RepeaterItem currentRow = (RepeaterItem)lnk.NamingContainer;
        Label lblid = (Label)currentRow.FindControl("Label4");
        Session["lblDeleteId"] = lblid.Text.Trim();
        Panel2_ModalPopupExtender.Show();
        
    }
    protected void lnkDeleteYes_Click(object sender, EventArgs e)
    {
        Page.Validate();
        if (Page.IsValid)
        {
            string msg;
            try
            {
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("Queryfor", "De"));
                param.Add(new SqlParameter("Id", Session["lblDeleteId"].ToString().Trim()));
                SqlParameter para = new SqlParameter("@Msg", "");
                para.Direction = ParameterDirection.Output;
                para.Size = 0x100;
                param.Add(para);
                msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("FeeGroupMasterProc", param);

            }
            catch (Exception ex)
            {
                msg = ex.Message;
                throw ex;
            }

            //BAL.objBal.MessageBox(objBal.MessageType(msg, new Control(), new BAL.textBoxList()), this.Page);
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, objBal.MessageType(msg, new Control(), new BAL.textBoxList()), "S");                                    

            loadGrid();
            BAL.objBal.ClearControls(Panel1);
            Session["lblDeleteId"] = "";
            txtFeeGroup.Focus();
        }
    }
}