using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_RulesForSalaryCalculation : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);
        if (!IsPostBack)
        {
            sql = "select DesId, DesName from DesMaster where BranchCode=" + Session["BranchCode"] + "";
            oo.FillDropDown_withValue(sql, drpEmpDesigation, "DesName", "DesId");
            drpEmpDesigation.Items.Insert(0, new ListItem("<--Select-->", ""));
        }
    }
    protected void drpEmpdes_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpEmpDesigation.SelectedIndex == 0)
        {
            divData.Visible = false;
        }
        else
        {
            divData.Visible = true;
            getRules();
        }
    }
    public void getRules()
    {
        List<SqlParameter> param = new List<SqlParameter>();

        param.Add(new SqlParameter("@Sql", "S"));
        param.Add(new SqlParameter("@DesId", drpEmpDesigation.SelectedValue));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));

        SqlParameter para = new SqlParameter("@Msg", "");
        para.Direction = ParameterDirection.Output;

        param.Add(para);

        DataSet ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("USP_RulesForSalaryCalculation", param);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtAllowedCL.Text = (ds.Tables[0].Rows[0]["AllowedCL"].ToString() == "" ? "0" : ds.Tables[0].Rows[0]["AllowedCL"].ToString());
                txtNooflateToCL.Text = (ds.Tables[0].Rows[0]["NooflateToCL"].ToString() == "" ? "0" : ds.Tables[0].Rows[0]["NooflateToCL"].ToString());
                txtNoofSLToCL.Text = (ds.Tables[0].Rows[0]["NoofSLToCL"].ToString() == "" ? "0" : ds.Tables[0].Rows[0]["NoofSLToCL"].ToString());
                txtNoofHDToCL.Text = (ds.Tables[0].Rows[0]["NoofHDToCL"].ToString() == "" ? "0" : ds.Tables[0].Rows[0]["NoofHDToCL"].ToString());
            }
            else
            {
                txtAllowedCL.Text = "";
                txtNooflateToCL.Text = "";
                txtNoofSLToCL.Text = "";
                txtNoofHDToCL.Text = "";
            }
        }
        else
        {
            txtAllowedCL.Text = "";
            txtNooflateToCL.Text = "";
            txtNoofSLToCL.Text = "";
            txtNoofHDToCL.Text = "";
        }

    }
    protected void lnkSubmit_Click(object sender, EventArgs e)
    {
        if (double.Parse(txtNooflateToCL.Text.Trim() == "" ? "0.0" : txtNooflateToCL.Text.Trim()) == 0)
        {
            Campus camp = new Campus();
            camp.msgbox(this.Page, divmsg, "Zero or blank value not allowed!", "A");
            txtNooflateToCL.Focus();
            return;
        }
        else if (double.Parse(txtNoofSLToCL.Text.Trim() == "" ? "0.0" : txtNoofSLToCL.Text.Trim()) == 0)
        {
            Campus camp = new Campus();
            camp.msgbox(this.Page, divmsg, "Zero or blank value not allowed!", "A");
            txtNoofSLToCL.Focus();
            return;
        }
        else if (double.Parse(txtNoofHDToCL.Text.Trim() == "" ? "0.0" : txtNoofHDToCL.Text.Trim()) == 0)
        {
            Campus camp = new Campus();
            camp.msgbox(this.Page, divmsg, "Zero or blank value not allowed!", "A");
            txtNoofHDToCL.Focus();
            return;
        }
        else
        {
            setRules();
        }
    }
    public void setRules()
    {

        List<SqlParameter> param = new List<SqlParameter>();

        param.Add(new SqlParameter("@Sql", "I"));

        param.Add(new SqlParameter("@DesId", drpEmpDesigation.SelectedValue.Trim()));
        param.Add(new SqlParameter("@AllowedCL", (txtAllowedCL.Text.Trim()==""?"0.0": txtAllowedCL.Text.Trim())));
        param.Add(new SqlParameter("@NooflateToCL", (txtNooflateToCL.Text.Trim()==""?"0.0": txtNooflateToCL.Text.Trim())));
        param.Add(new SqlParameter("@NoofSLToCL", (txtNoofSLToCL.Text.Trim()==""?"0.0": txtNoofSLToCL.Text.Trim())));
        param.Add(new SqlParameter("@NoofHDToCL", (txtNoofHDToCL.Text.Trim()==""?"0.0": txtNoofHDToCL.Text.Trim())));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
        param.Add(new SqlParameter("@LoginName", Session["LoginName"].ToString()));
  
        SqlParameter para = new SqlParameter("@Msg","");
        para.Direction = ParameterDirection.Output;

        param.Add(para);

        string msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("USP_RulesForSalaryCalculation", param);

        BAL.textBoxList txtbox = new BAL.textBoxList();
        txtbox.Noofnoncleartxt = 0;
        Campus camp = new Campus(); camp.msgbox(this.Page, divmsg, new Message().MessageType(msg,this.Page, txtbox), msg);

        getRules();
    }
}