using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class admin_LibraryBookLossDamegedMaster : System.Web.UI.Page
{
    List<SqlParameter> param = new List<SqlParameter>();
    string msg = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        txtDC.Focus();
        Campus camp = new Campus(); camp.LoadLoader(loader);
        if (!IsPostBack)
        {
            this.txtDA.Attributes.Add("onkeypress", "button_click(this, '" + this.lnkSubmit.ClientID + "')");          
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

        if (checkDuplicate(-1,txtDC.Text.Trim()))
        {
            //BAL.objBal.MessageBoxwithfocuscontrol("Sorry, Duplicate Entry!", lnkSubmit,txtDC.ClientID);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, Duplicate Entry!", "A");       

        }
        else
        {
            param = new List<SqlParameter>();
            param.Add(new SqlParameter("@QueryFor", "I"));
            param.Add(new SqlParameter("@DamageCategory", txtDC.Text.Trim()));
            param.Add(new SqlParameter("@DamageAmount", txtDA.Text.Trim()));
            param.Add(new SqlParameter("@Amountin", drpAmountin.SelectedItem.Text));
            param.Add(new SqlParameter("@Remark", txtRemark.Text.Trim()));
            if (drpStockMinus.SelectedIndex == 0)
            {
                param.Add(new SqlParameter("@Stockminus", false));
            }
            else
            {
                param.Add(new SqlParameter("@Stockminus", true));
            }           
            param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
            param.Add(new SqlParameter("@LoginName", Session["LoginName"].ToString()));
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
            SqlParameter para = new SqlParameter("@Msg", "");
            para.Direction = ParameterDirection.Output;
            param.Add(para);

            msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("DamageCategoryMasterProc", param);

            if (msg == "S")
            {
                //BAL.objBal.MessageBoxforUpdatePanel("Submitted successfully.", lnkSubmit);
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");       

                loadGrid();
                BAL.objBal.ClearControls(divinsert);
            }
            else
            {
                //BAL.objBal.MessageBoxforUpdatePanel("Sorry, Record not submitted!", lnkSubmit);
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
        grd1.DataSource = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("DamageCategoryMasterProc", param);
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
        param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
        ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("DamageCategoryMasterProc", param);
        if (ds.Tables.Count > 0)
        {
            dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                txtDCPanel.Text = dt.Rows[0]["DamageCategory"].ToString();
                txtDAPanel.Text = dt.Rows[0]["DamageAmount"].ToString();
                drpAmountinPanel.SelectedValue = dt.Rows[0]["Amountin"].ToString();
                txtRemarkPanel.Text = dt.Rows[0]["Remark"].ToString();
                drpStockMinusPanel.SelectedIndex = dt.Rows[0]["Stockminus"].ToString() == "True" ? 1 : 0;
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

        param = new List<SqlParameter>();
        param.Add(new SqlParameter("@QueryFor", "U"));
        param.Add(new SqlParameter("@id", HiddenField1.Value.Trim()));
        param.Add(new SqlParameter("@DamageCategory", txtDCPanel.Text.Trim()));
        param.Add(new SqlParameter("@DamageAmount", txtDAPanel.Text.Trim()));
        param.Add(new SqlParameter("@Amountin", drpAmountinPanel.SelectedItem.Text));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
        param.Add(new SqlParameter("@Remark", txtRemarkPanel.Text.Trim()));
        if (drpStockMinusPanel.SelectedIndex == 0)
        {
            param.Add(new SqlParameter("@Stockminus", false));
        }
        else
        {
            param.Add(new SqlParameter("@Stockminus", true));
        }    
        SqlParameter para = new SqlParameter("@Msg", "");
        para.Direction = ParameterDirection.Output;
        param.Add(para);

        msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("DamageCategoryMasterProc", param);

        if (msg == "U")
        {
            //BAL.objBal.MessageBoxforUpdatePanel("Updated successfully.", lnkUpdate);
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

        msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("DamageCategoryMasterProc", param);
   
        if (msg == "D")
        {
            //BAL.objBal.MessageBoxforUpdatePanel("Deleted successfully.", lnkyes);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Deleted successfully.", "S");       

            loadGrid();
            BAL.objBal.ClearControls(divinsert);
        }
        else
        {
            //BAL.objBal.MessageBoxforUpdatePanel("Sorry, Record not updated!", lnkyes);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, Record not updated!", "A");       

        }
    }

    public bool checkDuplicate(int id, string damageCategory)
    {
        Int16 flag = 0;

        param = new List<SqlParameter>();
        param.Add(new SqlParameter("@QueryFor", "DU"));
        param.Add(new SqlParameter("@ID", id));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
        param.Add(new SqlParameter("@DamageCategory", damageCategory));

        flag = Convert.ToInt16(DLL.objDll.Sp_SelectRecord_usingExecuteScalar("DamageCategoryMasterProc", param));

        return flag > 0 ? true : false;
    }
}