using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI.WebControls;

public partial class SetSalary : System.Web.UI.Page
{
    private SqlConnection _con;
    private readonly Campus _oo;
    private string sql, _sql1 = String.Empty;

    public SetSalary()
    {
        _con = new SqlConnection();
        _oo = new Campus();
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        _con = _oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file
        if (!IsPostBack)
        {
            txtHeaderEmpId.Focus();
        }

    }
    protected void lnkShow_Click(object sender, EventArgs e)
    {
        displayEmpInfo();
    }
    protected void txtHeaderEmpId_TextChanged(object sender, EventArgs e)
    {
        displayEmpInfo();
    }
    public void displayEmpInfo()
    {
        string empId = Request.Form[hfEmployeeId.UniqueID];
        if (empId == string.Empty)
        {
            empId = txtHeaderEmpId.Text.Trim();
        }

        string sqlcnt = "Select count(*)cnt from SalaryComponent where BranchCode=" + Session["BranchCode"] + "";
        if (_oo.ReturnTag(sqlcnt, "cnt")=="0")
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, Please create salary components first!", "A");
            divRecord.Visible = false;
            return;
        }
        string sqlcnt1 = "Select count(*)cnt from SalarySetMaster where empid='" + empId + "' and BranchCode=" + Session["BranchCode"] + "";
        if (_oo.ReturnTag(sqlcnt1, "cnt") == "0")
        {
            divAppraisal.Visible = false;
        }
        sql = "Select Empid from EmpWithdrawlRecord where empid='" + empId + "' and BranchCode=" + Session["BranchCode"] + "";
        if (BAL.objBal.ReturnTag(sql, "Empid") == "")
        {
            sql = "Select eod.EmpId EmpId,eod.Ecode Ecode,egd.EFirstName+' '+egd.EMiddleName+' ' +egd.ELastName as EmpName,egd.EFatherName FatherName,eod.Designation Designation,";
            sql = sql + " egd.EMotherName,egd.EMobileNo,Convert(varchar(11),eod.RegistrationDate,106) as RegistrationDate,eod.PFno, eod.EsicNo from EmpployeeOfficialDetails eod ";
            sql = sql + " inner join EmpGeneralDetail egd on eod.Empid=egd.Empid and eod.EmpId=egd.EmpId where eod.Withdrwal is null ";
            sql = sql + " and eod.empid='" + empId + "' and egd.BranchCode=" + Session["BranchCode"] + " and eod.BranchCode=" + Session["BranchCode"] + "";
            var dt = _oo.Fetchdata(sql);
            if (dt.Rows.Count > 0)
            {
                lblEmployeeId.Text = _oo.ReturnTag(sql, "EmpId");
                lblDesignation.Text = _oo.ReturnTag(sql, "Designation");
                lblName.Text = _oo.ReturnTag(sql, "EmpName");
                lblDateofJoining.Text = _oo.ReturnTag(sql, "RegistrationDate");
                lblFathersName.Text = _oo.ReturnTag(sql, "FatherName");
                lblContactNo.Text = _oo.ReturnTag(sql, "EMobileNo");
                lblPFNo.Text = _oo.ReturnTag(sql, "PFno");
                lblESICNo.Text = _oo.ReturnTag(sql, "EsicNo");
                divRecord.Visible = true;
                txtCTC.Text = "";

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SalarySetMasterProc";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = _con;
                    cmd.Parameters.AddWithValue("@EmpId", empId);
                    cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                    cmd.Parameters.AddWithValue("@Action", "select");
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    double gross = 0;
                    double deduction = 0;
                    double net = 0;
                    if (ds.Tables.Count>0)
                    {
                        rptEarningFix.DataSource = ds.Tables[0];
                        rptEarningFix.DataBind();
                        for (int i = 0; i < rptEarningFix.Items.Count; i++)
                        {
                            TextBox txtCmVal = (TextBox)rptEarningFix.Items[i].FindControl("txtCmVal");
                            gross = gross + double.Parse(txtCmVal.Text==""?"0": txtCmVal.Text);
                        }

                        rptEarningFlaxiable.DataSource = ds.Tables[1];
                        rptEarningFlaxiable.DataBind();
                        for (int i = 0; i < rptEarningFlaxiable.Items.Count; i++)
                        {
                            TextBox txtCmVal = (TextBox)rptEarningFlaxiable.Items[i].FindControl("txtCmVal1");
                            gross = gross + double.Parse(txtCmVal.Text == "" ? "0" : txtCmVal.Text);
                        }

                        rptDeduction.DataSource = ds.Tables[2];
                        rptDeduction.DataBind();
                        for (int i = 0; i < rptDeduction.Items.Count; i++)
                        {
                            TextBox txtCmVal = (TextBox)rptDeduction.Items[i].FindControl("txtCmVal2");
                            deduction = deduction + double.Parse(txtCmVal.Text == "" ? "0" : txtCmVal.Text);
                        }
                        net = (gross - deduction);
                        totalGross.Text = gross.ToString("0.00");
                        totalDeductions.Text = deduction.ToString("0.00");
                        totalNetPay.Text = net.ToString("0.00");
                        if (ds.Tables[3].Rows.Count>0)
                        {
                            txtCTC.Text = double.Parse(ds.Tables[3].Rows[0]["CTC"].ToString() == "" ? "0" : ds.Tables[3].Rows[0]["CTC"].ToString()).ToString("0.00");
                        }
                    }
                }

            }
            else
            {
                divRecord.Visible = false;
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, No record(s) found!", "A");
            }
        }
        else
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "This Employee already Relieved!", "A");
        }
    }

    protected void btnInsert_Click(object sender, EventArgs e)
    {
        Campus cmp = new Campus();
        _con.Close();
        int sts = 0;
        int sts2 = 0;
        double chkValidAmt = 0;
        for (int i = 0; i < rptEarningFix.Items.Count; i++)
        {
            TextBox txtCmVal = (TextBox)rptEarningFix.Items[i].FindControl("txtCmVal");
            chkValidAmt = chkValidAmt + double.Parse(txtCmVal.Text == "" ? "0" : txtCmVal.Text);
        }

        for (int i = 0; i < rptEarningFlaxiable.Items.Count; i++)
        {
            TextBox txtCmVal = (TextBox)rptEarningFlaxiable.Items[i].FindControl("txtCmVal1");
            chkValidAmt = chkValidAmt + double.Parse(txtCmVal.Text == "" ? "0" : txtCmVal.Text);
        }
        for (int i = 0; i < rptDeduction.Items.Count; i++)
        {
            TextBox txtCmVal = (TextBox)rptDeduction.Items[i].FindControl("txtCmVal2");
            chkValidAmt = chkValidAmt + double.Parse(txtCmVal.Text == "" ? "0" : txtCmVal.Text);
        }
        if (chkValidAmt==0)
        {
            cmp.msgbox(this.Page, msgbox, new ShowMSG().MSG("Please enter components amount!"), "A");
            return;
        }
        using (SqlCommand cmd = new SqlCommand())
        {
            for (int i = 0; i < rptEarningFix.Items.Count; i++)
            {
                Label lblCmpId = (Label)rptEarningFix.Items[i].FindControl("lblCmpId");
                TextBox txtCmVal = (TextBox)rptEarningFix.Items[i].FindControl("txtCmVal");
                cmd.CommandText = "SalarySetMasterProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = _con;
                cmd.Parameters.AddWithValue("@EmpId", lblEmployeeId.Text);
                cmd.Parameters.AddWithValue("@ComponentId", lblCmpId.Text);

                double CmVal = 0;
                double.TryParse(txtCmVal.Text, out CmVal);
                cmd.Parameters.AddWithValue("@ComponentValue", CmVal.ToString("0.00"));
                if (chkAppraisal.Checked)
                {
                    if (sts2 == 0)
                    {
                        sts2 = sts2 + 1;
                        cmd.Parameters.AddWithValue("@IsPromotion", "P0");
                    }
                    cmd.Parameters.AddWithValue("@PromotionDate", txtAppraisalDate.Text);
                    cmd.Parameters.AddWithValue("@PromotionRemark", txtAppraisalRemark.Text);
                }
                cmd.Parameters.AddWithValue("@PFNO", lblPFNo.Text);
                cmd.Parameters.AddWithValue("@ESINO", lblESICNo.Text);
                double CTC = 0;
                double.TryParse(txtCTC.Text, out CTC);
                cmd.Parameters.AddWithValue("@CTC", CTC.ToString("0.00"));
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                cmd.Parameters.AddWithValue("@Action", "insert");
                try
                {
                    _con.Open();
                    int x = cmd.ExecuteNonQuery();
                    _con.Close();
                    cmd.Parameters.Clear();
                    sts = sts + 1;
                }
                catch (SqlException ex)
                {
                    _con.Close();
                    cmp.msgbox(this.Page, msgbox, new ShowMSG().MSG(ex.Message), "A");
                }
                catch (Exception ex)
                {
                    _con.Close();
                    cmp.msgbox(this.Page, msgbox, new ShowMSG().MSG(ex.Message), "A");
                }
            }
        }
        using (SqlCommand cmd1 = new SqlCommand())
        {
            for (int i = 0; i < rptEarningFlaxiable.Items.Count; i++)
            {
                Label lblCmpId = (Label)rptEarningFlaxiable.Items[i].FindControl("lblCmpId1");
                TextBox txtCmVal = (TextBox)rptEarningFlaxiable.Items[i].FindControl("txtCmVal1");
                cmd1.CommandText = "SalarySetMasterProc";
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Connection = _con;
                cmd1.Parameters.AddWithValue("@EmpId", lblEmployeeId.Text);
                cmd1.Parameters.AddWithValue("@ComponentId", lblCmpId.Text);
                double CmVal = 0;
                double.TryParse(txtCmVal.Text, out CmVal);
                cmd1.Parameters.AddWithValue("@ComponentValue", CmVal.ToString("0.00"));
                if (chkAppraisal.Checked)
                {
                    if (sts2 == 0)
                    {
                        sts2 = sts2 + 1;
                        cmd1.Parameters.AddWithValue("@IsPromotion", "P0");
                    }
                    cmd1.Parameters.AddWithValue("@PromotionDate", txtAppraisalDate.Text);
                    cmd1.Parameters.AddWithValue("@PromotionRemark", txtAppraisalRemark.Text);
                }
                cmd1.Parameters.AddWithValue("@PFNO", lblPFNo.Text);
                cmd1.Parameters.AddWithValue("@ESINO", lblESICNo.Text);
                double CTC = 0;
                double.TryParse(txtCTC.Text, out CTC);
                cmd1.Parameters.AddWithValue("@CTC", CTC.ToString("0.00"));
                cmd1.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                cmd1.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                cmd1.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                cmd1.Parameters.AddWithValue("@Action", "insert");
                try
                {
                    _con.Open();
                    int x = cmd1.ExecuteNonQuery();
                    _con.Close();
                    cmd1.Parameters.Clear();
                    sts = sts + 1;
                }
                catch (SqlException ex)
                {
                    _con.Close();
                    cmp.msgbox(this.Page, msgbox, new ShowMSG().MSG(ex.Message), "A");
                }
                catch (Exception ex)
                {
                    _con.Close();
                    cmp.msgbox(this.Page, msgbox, new ShowMSG().MSG(ex.Message), "A");
                }
            }
        }
        using (SqlCommand cmd2 = new SqlCommand())
        {
            for (int i = 0; i < rptDeduction.Items.Count; i++)
            {
                Label lblCmpId = (Label)rptDeduction.Items[i].FindControl("lblCmpId2");
                TextBox txtCmVal = (TextBox)rptDeduction.Items[i].FindControl("txtCmVal2");
                cmd2.CommandText = "SalarySetMasterProc";
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Connection = _con;
                cmd2.Parameters.AddWithValue("@EmpId", lblEmployeeId.Text);
                cmd2.Parameters.AddWithValue("@ComponentId", lblCmpId.Text);
                double CmVal = 0;
                double.TryParse(txtCmVal.Text, out CmVal);
                cmd2.Parameters.AddWithValue("@ComponentValue", CmVal.ToString("0.00"));
                if (chkAppraisal.Checked)
                {
                    if (sts2 == 0)
                    {
                        sts2 = sts2 + 1;
                        cmd2.Parameters.AddWithValue("@IsPromotion", "P0");
                    }
                    cmd2.Parameters.AddWithValue("@PromotionDate", txtAppraisalDate.Text);
                    cmd2.Parameters.AddWithValue("@PromotionRemark", txtAppraisalRemark.Text);
                }
                cmd2.Parameters.AddWithValue("@PFNO", lblPFNo.Text);
                cmd2.Parameters.AddWithValue("@ESINO", lblESICNo.Text);
                double CTC = 0;
                double.TryParse(txtCTC.Text, out CTC);
                cmd2.Parameters.AddWithValue("@CTC", CTC.ToString("0.00"));

                cmd2.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                cmd2.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                cmd2.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                cmd2.Parameters.AddWithValue("@Action", "insert");
                try
                {
                    _con.Open();
                    int x=cmd2.ExecuteNonQuery();
                    _con.Close();
                    cmd2.Parameters.Clear();
                    sts = sts + 1;
                }
                catch (SqlException ex)
                {
                    _con.Close();
                    cmp.msgbox(this.Page, msgbox, new ShowMSG().MSG(ex.Message), "A");
                }
                catch (Exception ex)
                {
                    _con.Close();
                    cmp.msgbox(this.Page, msgbox, new ShowMSG().MSG(ex.Message), "A");
                }
            }
        }
        if (sts == 0)
        {
            cmp.msgbox(this.Page, msgbox, new ShowMSG().MSG("Some technical issues found, please contact to admin"), "A");
        }
        else
        {
            displayEmpInfo();
            cmp.msgbox(this.Page, msgbox, new ShowMSG().MSG("Submitted successfully."), "S");
        }
    }
}