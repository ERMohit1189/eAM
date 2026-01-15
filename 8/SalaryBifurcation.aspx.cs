using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI;
//using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class SalaryBifurcation : Page
{
    SqlConnection con = new SqlConnection();
    SqlCommand cmd;
    Campus oo = new Campus();
    string sql = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file
        if (!IsPostBack)
        {
            sql = "select DesId, DesName from DesMaster where BranchCode=" + Session["BranchCode"] + "";
            oo.FillDropDown_withValue(sql, drpDesignation, "DesName", "DesName");
            drpDesignation.Items.Insert(0, new ListItem("<--Select-->", ""));
        }
    }

    public void loadGrid()
    {
        sql = "select distinct o.EmpId, EFirstName+' '+EMiddleName+' '+ELastName name, EMobileNo, RegistrationDate from EmpployeeOfficialDetails o inner join EmpGeneralDetail g on g.EmpId=o.EmpId and g.BranchCode=o.BranchCode where o.BranchCode=" + Session["BranchCode"] + " and '" + drpDesignation.SelectedValue + "' = IIF('" + drpDesignation.SelectedValue + "' = '','" + drpDesignation.SelectedValue + "',o.DesNameNew) and isnull(o.Withdrwal,'')='' order by  (EFirstName+' '+EMiddleName+' '+ELastName) asc";
        var dt = oo.Fetchdata(sql);
        if (dt.Rows.Count > 0)
        {
            rptEmp.DataSource = dt;
            rptEmp.DataBind();
            
            for (int i = 0; i < rptEmp.Items.Count; i++)
            {
                double totalE = 0;
                double totalD = 0;
                Label EmpId = (Label)rptEmp.Items[i].FindControl("EmpId");

                double earning = 0;
                Repeater rptHead1 = (Repeater)rptEmp.Items[i].FindControl("rptHead1");
                string sql1 = "select sc.id ComponentId, sc.ComponentName, ssm.ComponentValue, sc.loginName, '('+ format(sc.RecordedDate,'dd-MMM-yyyy hh:mm:ss')+')' Recdate from SalaryComponent sc ";
                sql1 = sql1 + " inner join SalarySetMaster ssm on ssm.ComponentId=sc.id and ssm.BranchCode=sc.BranchCode ";
                sql1 = sql1 + " WHERE ssm.EmpId='" + EmpId.Text.Trim() + "'  and sc.BranchCode=" + Session["BranchCode"] + " and sc.ComponentType<>'Deduction' order by sc.ID asc";
                var dt1 = oo.Fetchdata(sql1);
                if (dt1.Rows.Count > 0)
                {
                    rptHead1.DataSource = dt1;
                    rptHead1.DataBind();
                    for (int j = 0; j < rptHead1.Items.Count; j++)
                    {
                        Label ComponentValue = (Label)rptHead1.Items[j].FindControl("ComponentValue");
                        earning = earning + double.Parse(ComponentValue.Text == "" ? "0" : ComponentValue.Text);
                    }
                    Label totalEarning = (Label)rptEmp.Items[i].FindControl("totalEarning");
                    totalEarning.Text = earning.ToString("0.00");
                    totalE = totalE + earning;
                    Label LoginName = (Label)rptEmp.Items[i].FindControl("LoginName");
                    LoginName.Text = dt1.Rows[0]["loginName"].ToString();
                    Label Recdate = (Label)rptEmp.Items[i].FindControl("Recdate");
                    Recdate.Text = dt1.Rows[0]["Recdate"].ToString();

                }
                else
                {
                    Panel divEar = (Panel)rptEmp.Items[i].FindControl("divEar");
                    divEar.Visible = false;
                }

                double deduction = 0;
                Repeater rptHead2 = (Repeater)rptEmp.Items[i].FindControl("rptHead2");
                string sql2 = "select sc.id ComponentId, sc.ComponentName, ssm.ComponentValue, sc.loginName+ '('+ format(sc.RecordedDate,'dd-MMM-yyyy hh:mm:ss')+')' dateName from SalaryComponent sc ";
                sql2 = sql2 + " inner join SalarySetMaster ssm on ssm.ComponentId=sc.id and ssm.BranchCode=sc.BranchCode ";
                sql2 = sql2 + " WHERE ssm.EmpId='" + EmpId.Text.Trim() + "'  and sc.BranchCode=" + Session["BranchCode"] + " and sc.ComponentType='Deduction' order by sc.ID asc";
                var dt2 = oo.Fetchdata(sql2);
                if (dt2.Rows.Count > 0)
                {
                    rptHead2.DataSource = dt2;
                    rptHead2.DataBind();
                    for (int j = 0; j < rptHead2.Items.Count; j++)
                    {
                        Label ComponentValue = (Label)rptHead2.Items[j].FindControl("ComponentValue");
                        deduction = deduction + double.Parse(ComponentValue.Text == "" ? "0" : ComponentValue.Text);
                    }
                    Label totalDeduction = (Label)rptEmp.Items[i].FindControl("totalDeduction");
                    totalDeduction.Text = deduction.ToString("0.00");
                    totalD = totalD + deduction;
                }
                else
                {
                    Panel divDed = (Panel)rptEmp.Items[i].FindControl("divDed");
                    divDed.Visible = false;
                }


                Label NetPay = (Label)rptEmp.Items[i].FindControl("NetPay");
                NetPay.Text = (totalE - totalD).ToString("0.00");
            }
        }
    }

    protected void lnkView_Click(object sender, EventArgs e)
    {
        loadGrid();
    }
}