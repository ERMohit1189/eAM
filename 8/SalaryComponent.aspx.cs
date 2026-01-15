using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class admin_SalaryComponent : Page
{
    private SqlConnection _con;
    private readonly Campus _oo;
    private string _sql, _sql1 = String.Empty;

    public admin_SalaryComponent()
    {
        _con = new SqlConnection();
        _oo = new Campus();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Campus camp = new Campus(); camp.LoadLoader(loader);
        _con = _oo.dbGet_connection();
        if (!IsPostBack)
        {
            try
            {
                GetSalaryComponent();
            }
            catch (Exception EX ) { }
        }
    }
    protected void rdoComponentType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdoComponentType.SelectedValue == "Earning")
        {
            divEarningCtrl.Visible = true;
            divDeductionCtrl.Visible = false;
            btnInsertE.Visible = true;
            btnInsertD.Visible = false;
        }
        else
        {
            divEarningCtrl.Visible = false;
            divDeductionCtrl.Visible = true;
            btnInsertE.Visible = false;
            btnInsertD.Visible = true;
        }
    }
    protected void btnInsertE_Click(object sender, EventArgs e)
    {
        _sql = "SELECT COUNT(*)CNT FROM SalaryComponent WHERE ComponentCategory='" + ddlCategoryE.SelectedValue + "' AND BranchCode=" + Session["BranchCode"] + "";
        string sts = _oo.ReturnTag(_sql, "CNT");
        if (sts != "0")
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, new ShowMSG().MSG("Record(s) Already Exists in this category !"), "A");
            return;
        }
        _con.Close();
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = "SalaryComponentProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = _con;
            cmd.Parameters.AddWithValue("@ComponentType", rdoComponentType.SelectedValue);
            cmd.Parameters.AddWithValue("@ComponentName", txtComponentName_E.Text);
            cmd.Parameters.AddWithValue("@ComponentCategory", ddlCategoryE.SelectedValue);
            cmd.Parameters.AddWithValue("@DependsOn", ddlDependsOnE.SelectedValue);
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
            cmd.Parameters.AddWithValue("@Action", "insert");
            try
            {
                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
                cmd.Parameters.Clear();
                GetSalaryComponent();
                txtComponentName_E.Text = "";
                ddlCategoryE.SelectedIndex = 0;
                ddlDependsOnE.SelectedIndex = 0;
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, new ShowMSG().MSG("Submitted successfully."), "S");
            }
            catch (SqlException ex)
            {
                _con.Close();
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, new ShowMSG().MSG(ex.Message), "A");
            }
            catch (Exception ex)
            {
                _con.Close();
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, new ShowMSG().MSG(ex.Message), "A");
            }
        }
    }
    protected void btnInsertD_Click(object sender, EventArgs e)
    {
        _sql = "SELECT COUNT(*)CNT FROM SalaryComponent WHERE ComponentCategory='" + ddlCategoryD.SelectedValue + "' AND BranchCode=" + Session["BranchCode"] + "";
        string sts = _oo.ReturnTag(_sql, "CNT");
        if (sts!="0")
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, new ShowMSG().MSG("Record(s) Already Exists in this category !"), "A");
            return;
        }
        if (ddlCategoryD.SelectedValue == "Other Deduction")
        {

        }
        else if (ddlDependsOnD.SelectedIndex == 0)
        {

            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, new ShowMSG().MSG("Please select depends on !"), "A");
            return;
        }
        _con.Close();
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = "SalaryComponentProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = _con;
            cmd.Parameters.AddWithValue("@ComponentType", rdoComponentType.SelectedValue);
            cmd.Parameters.AddWithValue("@ComponentName", txtComponentName_D.Text);
            cmd.Parameters.AddWithValue("@ComponentCategory", ddlCategoryD.SelectedValue);
            if (ddlCategoryD.SelectedValue == "Other Deduction")
            {

            }
            else
            {
                cmd.Parameters.AddWithValue("@DependsOn", ddlDependsOnD.SelectedValue);
                cmd.Parameters.AddWithValue("@AmountDeductionType", rdoAmuntDeductionType.SelectedValue);
            }
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
            cmd.Parameters.AddWithValue("@Action", "insert");
            try
            {
                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
                cmd.Parameters.Clear();
                GetSalaryComponent();
                txtComponentName_D.Text = "";
                ddlCategoryD.SelectedIndex = 0;
                ddlDependsOnD.SelectedIndex = 0;
                rdoAmuntDeductionType.SelectedIndex = 0;
                divDependsOn.Visible = true;
                divAmuntDeductionType.Visible = true;
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, new ShowMSG().MSG("Submitted successfully."), "S");
            }
            catch (SqlException ex)
            {
                _con.Close();
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, new ShowMSG().MSG(ex.Message), "A");
            }
            catch (Exception ex)
            {
                _con.Close();
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, new ShowMSG().MSG(ex.Message), "A");
            }
        }
    }


    private void GetSalaryComponent()
    {
        _sql = "select Id, ComponentType,ComponentName, case when ComponentCategory='FBP' then 'Flexible Benefit Plan (FBP)' else ComponentCategory end ComponentCategory,DependsOn, ";
        _sql = _sql + " AmountDeductionType,format(RecordedDate,'dd-MMM-yyyy hh:mm:ss tt') RecordedDate,BranchCode,SessionName, LoginName, case when IsEnable=1 then 'Active' else 'In-Active' end IsEnable ";
        _sql = _sql + " from SalaryComponent WHERE BranchCode=" + Session["BranchCode"] + " ";
        Grd.DataSource = _oo.GridFill(_sql);
        Grd.DataBind();
        for (int i = 0; i < Grd.Rows.Count; i++)
        {
            Label ComponentId = (Label)Grd.Rows[i].FindControl("lblId");
            LinkButton LinkButton3 = (LinkButton)Grd.Rows[i].FindControl("LinkButton3");
            _sql = "select * from SalarySetMaster where ComponentId=" + ComponentId.Text.Trim() + " and BranchCode=" + Session["BranchCode"] + "";
            if (_oo.Duplicate(_sql))
            {
                LinkButton3.Text = "<i class='fa fa-lock'></i>";
                LinkButton3.Enabled = false;
            }
        }
    }

    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        Button8.Focus();
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("lblId");
        var ss = lblId.Text;
        lblvalue.Text = ss;
        Panel2_ModalPopupExtender.Show();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = "SalaryComponentProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = _con;
            cmd.Parameters.AddWithValue("@ID", lblvalue.Text);
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@Action", "delete");
            try
            {
                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
                GetSalaryComponent();
                cmd.Parameters.Clear();
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, new ShowMSG().MSG("Deleted successfully."), "S");
            }
            catch (SqlException ex)
            {
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, new ShowMSG().MSG(ex.Message), "A");
            }
            catch (Exception ex)
            {
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, new ShowMSG().MSG(ex.Message), "A");
            }
        }
    }
    protected void Button8_Click(object sender, EventArgs e)
    {
    }


    protected void ddlCategoryD_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCategoryD.SelectedValue == "Other Deduction")
        {
            divDependsOn.Visible = false;
            divAmuntDeductionType.Visible = false;
        }
        else
        {
            divDependsOn.Visible = true;
            divAmuntDeductionType.Visible = true;

        }
    }
}