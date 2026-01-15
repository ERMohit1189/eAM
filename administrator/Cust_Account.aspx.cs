using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class your_account : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            displayCustAcc();
            string sql = "Select CollegeName from CollegeMaster where BranchCode=" + Session["BranchCode"] + "";
            lblNameOrg.Text = BAL.objBal.ReturnTag(sql, "CollegeName");

        }
       
    }
    protected void lnkSubmit_Click(object sender, EventArgs e)
    {
        createCustAcc();
    }

    public void createCustAcc()
    {
        List<SqlParameter> param = new List<SqlParameter>();
        param.Add(new SqlParameter("@CID",txtCid.Text.Trim()));
        param.Add(new SqlParameter("@SubModule", txtSubModule.Text.Trim()));
        param.Add(new SqlParameter("@SubStatus", drpSubStatus.SelectedValue.ToString().Trim()));
        param.Add(new SqlParameter("@BillingFrequency", drpBillingFrequency.SelectedValue.ToString().Trim()));
        param.Add(new SqlParameter("@BillingCurrency", txtBillingCurrency.Text.Trim()));
        param.Add(new SqlParameter("@NextDueDate", txtNextDueDate.Text.Trim()));
        param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString().Trim()));

        SqlParameter para = new SqlParameter("@Msg" ,"");
        para.Direction = ParameterDirection.Output;

        param.Add(para);

        string msg = "";
        msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("custRegProc", param);

        if (msg == "S")
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");
            displayCustAcc();
        }
        if (msg == "U")
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully.", "S");
            displayCustAcc();
        }
        else
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, Record not submitted!", "A");   
        }
    }

    public void displayCustAcc()
    {
        DataSet ds = new DataSet();

        List<SqlParameter> param = new List<SqlParameter>();
        param.Add(new SqlParameter("@Queryfor", "S"));

        ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("custRegProc", param);

        if (ds!=null)
        {
            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                txtCid.Text = dt.Rows[0]["CID"].ToString();
                txtSubModule.Text = dt.Rows[0]["SubModule"].ToString();
                drpSubStatus.SelectedValue = dt.Rows[0]["SubStatus"].ToString();
                drpBillingFrequency.Text = dt.Rows[0]["BillingFrequency"].ToString();
                txtBillingCurrency.Text = dt.Rows[0]["BillingCurrency"].ToString();
                txtNextDueDate.Text = dt.Rows[0]["NextDueDate"].ToString();
            }
        }

    }
}