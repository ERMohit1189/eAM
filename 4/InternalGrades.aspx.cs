using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class Comman_G6_InternalGrades : System.Web.UI.Page
{
    List<SqlParameter> param = new List<SqlParameter>();
    string msg = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        txtCaption.Focus();
        Campus camp = new Campus(); camp.LoadLoader(loader);
        if (!IsPostBack)
        {
            this.txtCaption.Attributes.Add("onkeypress", "button_click(this, '" + this.lnkSubmit.ClientID + "')");   
            loadGrid();
        }
    }
    protected void lnkSubmit_Click(object sender, EventArgs e)
    {
        insertData();
    }

    public void insertData()
    {
        msg = "";

        if (checkDuplicate("-1", drpGradeFor.SelectedItem.Text.Trim(), txtCaption.Text.Trim()))
        {
            //BAL.objBal.MessageBoxwithfocuscontrol("Sorry, Duplicate Entry!", lnkSubmit, txtCaption.ClientID);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, Duplicate Entry!", "A");       

        }
        else
        {
            param = new List<SqlParameter>();
            param.Add(new SqlParameter("@queryfor", "I"));
            param.Add(new SqlParameter("@txtfor", drpGradeFor.SelectedItem.Text));
            param.Add(new SqlParameter("@txt", txtCaption.Text.Trim()));
            param.Add(new SqlParameter("@Remark", txtRemark.Text.Trim()));
            param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
            param.Add(new SqlParameter("@LoginName", Session["LoginName"].ToString()));
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
            SqlParameter para = new SqlParameter("@Msg", "");
            para.Direction = ParameterDirection.Output;
            param.Add(para);

            msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("CCEXItoXII_InternalGradesPROC", param);

            if (msg == "S")
            {
                //BAL.objBal.MessageBoxwithfocuscontrol("Submitted successfully.", lnkSubmit,txtCaption.ClientID);
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");       

                loadGrid();
                BAL.objBal.ClearControls(divinsert);
            }
            else
            {
                //BAL.objBal.MessageBoxwithfocuscontrol("Sorry, Record not submitted!", lnkSubmit, txtCaption.ClientID);
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, Record not submitted!", "A");       

            }

        }
    }

    public void loadGrid()
    {
        param = new List<SqlParameter>();
        param.Add(new SqlParameter("@QueryFor", "S"));
        param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
        grd1.DataSource = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("CCEXItoXII_InternalGradesPROC", param);
        grd1.DataBind();
    }
    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();

        LinkButton lnk = (LinkButton)sender;
        Label lblid = (Label)lnk.NamingContainer.FindControl("lblid");
        HiddenField1.Value = lblid.Text.Trim();
        param = new List<SqlParameter>();
        param.Add(new SqlParameter("@QueryFor", "S"));
        param.Add(new SqlParameter("@id", lblid.Text.Trim()));
        param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString().Trim()));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));

        ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("CCEXItoXII_InternalGradesproc", param);
        if (ds.Tables.Count > 0)
        {
            dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                drpGradeForPanel.Text = dt.Rows[0]["txtfor"].ToString();
                txtCaptionPanel.Text = dt.Rows[0]["txt"].ToString();
                txtRemarkPanel.Text = dt.Rows[0]["Remark"].ToString();
            }
        }

        Panel1_ModalPopupExtender.Show();
    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        lnkNo.Focus();
        LinkButton lnk = (LinkButton)sender;
        Label lblid = (Label)lnk.NamingContainer.FindControl("lblid");
        lblvalue.Text = lblid.Text.Trim();
        Panel2_ModalPopupExtender.Show();

    }
    protected void lnkUpdate_Click(object sender, EventArgs e)
    {
        msg = "";
        if (checkDuplicate(HiddenField1.Value.Trim(), drpGradeForPanel.SelectedItem.Text.Trim(), txtCaptionPanel.Text.Trim()))
        {
            //BAL.objBal.MessageBoxwithfocuscontrol("Sorry, Duplicate Entry!", lnkSubmit, txtCaption.ClientID);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, Duplicate Entry!", "A");       

        }
        else
        {
            param = new List<SqlParameter>();
            param.Add(new SqlParameter("@QueryFor", "U"));
            param.Add(new SqlParameter("@id", HiddenField1.Value.Trim()));
            param.Add(new SqlParameter("@txt", txtCaptionPanel.Text.Trim()));
            param.Add(new SqlParameter("@txtfor", drpGradeForPanel.Text.Trim()));
            param.Add(new SqlParameter("@Remark", txtRemarkPanel.Text.Trim()));
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
            SqlParameter para = new SqlParameter("@Msg", "");
            para.Direction = ParameterDirection.Output;
            param.Add(para);

            msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("CCEXItoXII_InternalGradesproc", param);

            if (msg == "U")
            {
                //BAL.objBal.MessageBoxwithfocuscontrol("Updated successfully.",lnkUpdate , txtCaption.ClientID);
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully.", "S");       

                loadGrid();
                BAL.objBal.ClearControls(divinsert);
            }
            else
            {
                //BAL.objBal.MessageBoxforUpdatePanel("Sorry, Record not updated!", lnkUpdate);
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, Record not updated!", "A");       

            }
        }
    }
    protected void lnkyes_Click(object sender, EventArgs e)
    {
        msg = "";

        param = new List<SqlParameter>();
        param.Add(new SqlParameter("@QueryFor", "D"));
        param.Add(new SqlParameter("@id", lblvalue.Text.Trim()));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
        SqlParameter para = new SqlParameter("@Msg", "");
        para.Direction = ParameterDirection.Output;
        param.Add(para);
        para.Direction = ParameterDirection.Output;

        msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("CCEXItoXII_InternalGradesproc", param);

        if (msg == "D")
        {
            //BAL.objBal.MessageBoxwithfocuscontrol("Deleted successfully.", lnkyes, txtCaption.ClientID);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Deleted successfully.", "S");       

            loadGrid();
            BAL.objBal.ClearControls(divinsert);
        }
        else
        {
            //BAL.objBal.MessageBoxwithfocuscontrol("Sorry, Record not updated!", lnkyes, txtCaption.ClientID);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, Record not updated!", "A");       

        }
    }

    public bool checkDuplicate(string id, string entryfor,string caption)
    {
        Int16 flag = 0;

        param = new List<SqlParameter>();
        param.Add(new SqlParameter("@QueryFor", "DU"));
        param.Add(new SqlParameter("@ID", id));
        param.Add(new SqlParameter("@txtfor", entryfor));
        param.Add(new SqlParameter("@txt", caption));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));

        flag = Convert.ToInt16(DLL.objDll.Sp_SelectRecord_usingExecuteScalar("CCEXItoXII_InternalGradesPROC", param));

        return flag > 0 ? true : false;
    }
}