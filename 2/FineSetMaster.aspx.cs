using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Web.UI;

public partial class FineSetMaster : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);
        if (!IsPostBack)
        {
            Reset();
            sql = "select distinct BaseFineType from FineSetMaster where sessionname='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            if (oo.ReturnTag(sql, "BaseFineType") == "Range Basis")
            {
                rdoBasis.SelectedIndex = 0;
                Panel1.Visible = true;
                Panel2.Visible = false;
                DisplayGridBasis();
                loadChecks();

            }
            if (oo.ReturnTag(sql, "BaseFineType") == "Daily Basis")
            {
                rdoBasis.SelectedIndex = 1;
                Panel1.Visible = false;
                Panel2.Visible = true;
                DisplayGridBasis();
                loadChecks();
            }
            else
            {
                rdoBasis.SelectedIndex = 0;
                Panel1.Visible = true;
                Panel2.Visible = false;
                DisplayGridBasis();
                loadChecks();
            }

            sql = "select count(*) cnt from ChequeBounceFineMaster where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            if (oo.ReturnTag(sql, "cnt") != "0")
            {
                sql = "select top(1) FineAmount from ChequeBounceFineMaster where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
                txtChequeBounceCharge.Text = oo.ReturnTag(sql, "FineAmount").ToString();
            }
            else
            {
                txtChequeBounceCharge.Text = "0.00";
            }
            txtChequeBounceCharge.Enabled = true;
            //sql = "select (select count(*) from TutionFeeDeposit where MOP='Cheque' and SessionName='" + Session["SessionName"].ToString() + "')+ (select count(*) from TransportFeeDeposit where MOP='Cheque' and SessionName='" + Session["SessionName"].ToString() + "')+ (select count(*) from HostelFeeDeposit where PaymentMode='Cheque' and SessionName='" + Session["SessionName"].ToString() + "') counts";
            //if (oo.ReturnTag(sql, "counts") != "0")
            //{
            //    txtChequeBounceCharge.Enabled = false;
            //}
            //else
            //{
            //    txtChequeBounceCharge.Enabled = true;
            //}
        }
        
    }
    public void DisplayGridBasis()
    {

        if (rdoBasis.SelectedIndex == 0)
        {
             DrpFromDate1.SelectedValue = "1"; DrpToDate1.SelectedValue = "31"; txtrangeAmount1.Text = "0";
             DrpFromDate2.SelectedValue = "1"; DrpToDate2.SelectedValue = "1"; txtrangeAmount2.Text = "0";
             DrpFromDate3.SelectedValue = "1"; DrpToDate3.SelectedValue = "1"; txtrangeAmount3.Text = "0";
             DrpFromDate4.SelectedValue = "1"; DrpToDate4.SelectedValue = "1"; txtrangeAmount4.Text = "0";
             DrpFromDate5.SelectedValue = "1"; DrpToDate5.SelectedValue = "1"; txtrangeAmount5.Text = "0";
             DrpFromDate6.SelectedValue = "1"; DrpToDate6.SelectedValue = "1"; txtrangeAmount6.Text = "0";
             DrpFromDate7.SelectedValue = "1"; DrpToDate7.SelectedValue = "1"; txtrangeAmount7.Text = "0";

            sql = "select * from FineSetMaster where sessionname='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and BaseFineType='Range Basis'";
            var dt = oo.Fetchdata(sql);
            if (dt.Rows.Count > 0)
            {
                ddlFineBaseType1.SelectedValue = dt.Rows[0]["FineType"].ToString(); DrpFromDate1.SelectedValue = dt.Rows[0]["FromDate"].ToString(); DrpToDate1.SelectedValue = dt.Rows[0]["ToDate"].ToString(); txtrangeAmount1.Text = dt.Rows[0]["FineAmount"].ToString().Trim();
                ddlFineBaseType2.SelectedValue = dt.Rows[1]["FineType"].ToString(); DrpFromDate2.SelectedValue = dt.Rows[1]["FromDate"].ToString(); DrpToDate2.SelectedValue = dt.Rows[1]["ToDate"].ToString(); txtrangeAmount2.Text = dt.Rows[1]["FineAmount"].ToString().Trim();
                ddlFineBaseType3.SelectedValue = dt.Rows[2]["FineType"].ToString(); DrpFromDate3.SelectedValue = dt.Rows[2]["FromDate"].ToString(); DrpToDate3.SelectedValue = dt.Rows[2]["ToDate"].ToString(); txtrangeAmount3.Text = dt.Rows[2]["FineAmount"].ToString().Trim();
                ddlFineBaseType4.SelectedValue = dt.Rows[3]["FineType"].ToString(); DrpFromDate4.SelectedValue = dt.Rows[3]["FromDate"].ToString(); DrpToDate4.SelectedValue = dt.Rows[3]["ToDate"].ToString(); txtrangeAmount4.Text = dt.Rows[3]["FineAmount"].ToString().Trim();
                ddlFineBaseType5.SelectedValue = dt.Rows[4]["FineType"].ToString(); DrpFromDate5.SelectedValue = dt.Rows[4]["FromDate"].ToString(); DrpToDate5.SelectedValue = dt.Rows[4]["ToDate"].ToString(); txtrangeAmount5.Text = dt.Rows[4]["FineAmount"].ToString().Trim();
                ddlFineBaseType6.SelectedValue = dt.Rows[5]["FineType"].ToString(); DrpFromDate6.SelectedValue = dt.Rows[5]["FromDate"].ToString(); DrpToDate6.SelectedValue = dt.Rows[5]["ToDate"].ToString(); txtrangeAmount6.Text = dt.Rows[5]["FineAmount"].ToString().Trim();
                ddlFineBaseType7.SelectedValue = dt.Rows[6]["FineType"].ToString(); DrpFromDate7.SelectedValue = dt.Rows[6]["FromDate"].ToString(); DrpToDate7.SelectedValue = dt.Rows[6]["ToDate"].ToString(); txtrangeAmount7.Text = dt.Rows[6]["FineAmount"].ToString().Trim();

            }
        }
        else
        {
            DrpDate.SelectedValue = "1"; txtdailAmount.Text = "0"; txtdaIncre.Text = "0";
            sql = "select * from FineSetMaster where sessionname='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and BaseFineType='Daily Basis'";
            var dt = oo.Fetchdata(sql);
            if (dt.Rows.Count > 0)
            {
                DrpDate.SelectedValue = dt.Rows[0]["FromDate"].ToString(); 
                txtdailAmount.Text = dt.Rows[0]["FineAmount"].ToString(); 
                txtdaIncre.Text = dt.Rows[0]["DailyIncrement"].ToString();

                ddlFineBaseType7forDailyBasis.SelectedValue = dt.Rows[1]["FineType"].ToString();
                txtrangeAmount7forDailyBasis.Text = dt.Rows[1]["FineAmount"].ToString();
            }
        }


    }
    public void loadChecks()
    {
        if (rdoBasis.SelectedIndex == 0)
        {
            sql = "select top(1) * from FineSetMaster where sessionname='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and BaseFineType='Range Basis'";
        }
        else
        {
            sql = "select top(1) * from FineSetMaster where sessionname='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and BaseFineType='Daily Basis'";
        }
        var dt = oo.Fetchdata(sql);
        if (dt.Rows.Count > 0)
        {
            chkExemptionAccordingDoA.Checked = dt.Rows[0]["LateFeeExemptionAccordingToDateOfAdmission"].ToString() == "True" ? true : false;
            chkExemptionforOnline.Checked = dt.Rows[0]["LateFeeExemptionForOnlinePayment"].ToString() == "True" ? true : false;
        }
        else
        {
            chkExemptionAccordingDoA.Checked = false;
            chkExemptionforOnline.Checked = false;
        }
    }
    protected void rdoBasis_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdoBasis.SelectedIndex == 0)
        {
            Panel1.Visible = true;
            Panel2.Visible = false;
        }
        else
        {
            Panel1.Visible = false;
            Panel2.Visible = true;
        }
        DisplayGridBasis();
        loadChecks();
    }
    protected void LinkSubmit_Click(object sender, EventArgs e)
    {
        sql = "select count(*) cnt from FineSetMaster where sessionname='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        if (oo.ReturnTag(sql, "cnt") != "0")
        {
            if (rdoBasis.SelectedIndex == 0)
            {
                sql = "select count(*) cnt from FineSetMaster where sessionname='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and BaseFineType='Daily Basis'";
                if (oo.ReturnTag(sql, "cnt") != "0")
                {
                    delete();
                }
            }
            else
            {
                sql = "select count(*) cnt from FineSetMaster where sessionname='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and BaseFineType='Range Basis'";
                if (oo.ReturnTag(sql, "cnt") != "0")
                {
                    delete();
                }
            }
        }
        if (rdoBasis.SelectedIndex == 0)
        {
            delete();
            if (DrpFromDate1.SelectedValue == DrpToDate1.SelectedValue && DrpFromDate2.SelectedValue == DrpToDate2.SelectedValue && DrpFromDate3.SelectedValue == DrpToDate3.SelectedValue && DrpFromDate4.SelectedValue == DrpToDate4.SelectedValue && DrpFromDate5.SelectedValue == DrpToDate5.SelectedValue && DrpFromDate6.SelectedValue == DrpToDate6.SelectedValue)
            {
                delete();
                txtrangeAmount3.Focus();
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please select valid date range!", "A");
                return;
            }
            for (int i = 1; i <= 7; i++)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FineSetMasterProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                con.Open();
                if (i == 1)
                {
                    cmd.Parameters.AddWithValue("@FineType", ddlFineBaseType1.SelectedValue);
                    cmd.Parameters.AddWithValue("@FromDate", DrpFromDate1.SelectedValue);
                    cmd.Parameters.AddWithValue("@ToDate", DrpToDate1.SelectedValue);
                    cmd.Parameters.AddWithValue("@FineAmount", (txtrangeAmount1.Text.Trim() == "" ? "0" : txtrangeAmount1.Text.Trim()));
                }
                if (i == 2)
                {
                    if ((txtrangeAmount2.Text.Trim() == "" ? "0" : txtrangeAmount2.Text.Trim()) == "0" && DrpFromDate2.SelectedValue != DrpToDate2.SelectedValue)
                    {
                        delete();
                        txtrangeAmount2.Focus();
                        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please Enter valid amount!", "A");
                        return;
                    }

                    cmd.Parameters.AddWithValue("@FineType", ddlFineBaseType2.SelectedValue);
                    cmd.Parameters.AddWithValue("@FromDate", DrpFromDate2.SelectedValue);
                    cmd.Parameters.AddWithValue("@ToDate", DrpToDate2.SelectedValue);
                    cmd.Parameters.AddWithValue("@FineAmount", (txtrangeAmount2.Text.Trim() == "" ? "0" : txtrangeAmount2.Text.Trim()));
                }
                if (i == 3)
                {
                    if ((txtrangeAmount3.Text.Trim() == "" ? "0" : txtrangeAmount3.Text.Trim()) == "0" && DrpFromDate3.SelectedValue != DrpToDate3.SelectedValue)
                    {
                        delete();
                        txtrangeAmount3.Focus();
                        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please Enter valid amount!", "A");
                        return;
                    }

                    cmd.Parameters.AddWithValue("@FineType", ddlFineBaseType3.SelectedValue);
                    cmd.Parameters.AddWithValue("@FromDate", DrpFromDate3.SelectedValue);
                    cmd.Parameters.AddWithValue("@ToDate", DrpToDate3.SelectedValue);
                    cmd.Parameters.AddWithValue("@FineAmount", (txtrangeAmount3.Text.Trim() == "" ? "0" : txtrangeAmount3.Text.Trim()));
                }
                if (i == 4)
                {
                    if ((txtrangeAmount4.Text.Trim() == "" ? "0" : txtrangeAmount4.Text.Trim()) == "0" && DrpFromDate4.SelectedValue != DrpToDate4.SelectedValue)
                    {
                        delete();
                        txtrangeAmount4.Focus();
                        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please Enter valid amount!", "A");
                        return;
                    }
                    cmd.Parameters.AddWithValue("@FineType", ddlFineBaseType4.SelectedValue);
                    cmd.Parameters.AddWithValue("@FromDate", DrpFromDate4.SelectedValue);
                    cmd.Parameters.AddWithValue("@ToDate", DrpToDate4.SelectedValue);
                    cmd.Parameters.AddWithValue("@FineAmount", (txtrangeAmount4.Text.Trim() == "" ? "0" : txtrangeAmount4.Text.Trim()));
                }
                if (i == 5)
                {
                    if ((txtrangeAmount5.Text.Trim() == "" ? "0" : txtrangeAmount5.Text.Trim()) == "0" && DrpFromDate5.SelectedValue != DrpToDate5.SelectedValue)
                    {
                        delete();
                        txtrangeAmount5.Focus();
                        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please Enter valid amount!", "A");
                        return;
                    }
                    cmd.Parameters.AddWithValue("@FineType", ddlFineBaseType5.SelectedValue);
                    cmd.Parameters.AddWithValue("@FromDate", DrpFromDate5.SelectedValue);
                    cmd.Parameters.AddWithValue("@ToDate", DrpToDate5.SelectedValue);
                    cmd.Parameters.AddWithValue("@FineAmount", (txtrangeAmount5.Text.Trim() == "" ? "0" : txtrangeAmount5.Text.Trim()));
                }
                if (i == 6)
                {
                    if ((txtrangeAmount6.Text.Trim() == "" ? "0" : txtrangeAmount6.Text.Trim()) == "0" && DrpFromDate6.SelectedValue != DrpToDate6.SelectedValue)
                    {
                        delete();
                        txtrangeAmount6.Focus();
                        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please Enter valid amount!", "A");
                        return;
                    }
                    cmd.Parameters.AddWithValue("@FineType", ddlFineBaseType6.SelectedValue);
                    cmd.Parameters.AddWithValue("@FromDate", DrpFromDate6.SelectedValue);
                    cmd.Parameters.AddWithValue("@ToDate", DrpToDate6.SelectedValue);
                    cmd.Parameters.AddWithValue("@FineAmount", (txtrangeAmount6.Text.Trim() == "" ? "0" : txtrangeAmount6.Text.Trim()));
                }
                if (i == 7)
                {
                    cmd.Parameters.AddWithValue("@FineType", ddlFineBaseType7.SelectedValue);
                    cmd.Parameters.AddWithValue("@FromDate", DrpFromDate7.SelectedValue);
                    cmd.Parameters.AddWithValue("@ToDate", DrpToDate7.SelectedValue);
                    cmd.Parameters.AddWithValue("@FineAmount", (txtrangeAmount7.Text.Trim() == "" ? "0" : txtrangeAmount7.Text.Trim()));
                }
                cmd.Parameters.AddWithValue("@BaseFineType", rdoBasis.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@LateFeeExemptionAccordingToDateOfAdmission", (chkExemptionAccordingDoA.Checked == false ? "0" : "1"));
                cmd.Parameters.AddWithValue("@LateFeeExemptionForOnlinePayment", (chkExemptionforOnline.Checked == false ? "0" : "1"));
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                cmd.Parameters.AddWithValue("@UserName", Session["LoginName"].ToString());
                cmd.Parameters.AddWithValue("@Action", "insert");

                try
                {

                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                }
            }
            saveChkBounceFine();
            DisplayGridBasis();
            loadChecks();
            Campus camps = new Campus(); camps.msgbox(this.Page, msgbox, "Submitted successfully.", "S");
        }
        else if(rdoBasis.SelectedIndex == 1)
        {
            delete();

            for (int i = 0; i <= 1; i++)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FineSetMasterProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                con.Open();

                if (i == 0)
                {
                    cmd.Parameters.AddWithValue("@FromDate", DrpDate.SelectedValue);
                    cmd.Parameters.AddWithValue("@ToDate", DrpDate.SelectedValue);
                    cmd.Parameters.AddWithValue("@BaseFineType", rdoBasis.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@FineType", rdoBasis.SelectedValue);
                    cmd.Parameters.AddWithValue("@FineAmount", (txtdailAmount.Text.Trim() == "" ? "0" : txtdailAmount.Text.Trim()));
                    cmd.Parameters.AddWithValue("@DailyIncrement", txtdaIncre.Text.Trim());
                }
                else
                if (i == 1)
                {
                    cmd.Parameters.AddWithValue("@FromDate", ddlFromDate22.SelectedValue);
                    cmd.Parameters.AddWithValue("@ToDate", ddlToDate22.SelectedValue);
                    cmd.Parameters.AddWithValue("@BaseFineType", rdoBasis.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@FineType", ddlFineBaseType7forDailyBasis.SelectedValue);
                    cmd.Parameters.AddWithValue("@FineAmount", (txtrangeAmount7forDailyBasis.Text.Trim() == "" ? "0" : txtrangeAmount7forDailyBasis.Text.Trim()));
                }
                cmd.Parameters.AddWithValue("@LateFeeExemptionAccordingToDateOfAdmission", (chkExemptionAccordingDoA.Checked == false ? "0" : "1"));
                cmd.Parameters.AddWithValue("@LateFeeExemptionForOnlinePayment", (chkExemptionforOnline.Checked == false ? "0" : "1"));
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                cmd.Parameters.AddWithValue("@UserName", Session["LoginName"].ToString());
                cmd.Parameters.AddWithValue("@Action", "insert");

                try
                {
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    con.Close();
                }
                catch (Exception ex)
                {con.Close(); }
            }


            try
            {
                saveChkBounceFine();
                DisplayGridBasis();
                loadChecks();
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");
            }
            catch (Exception ex)
            { con.Close(); }
        }

    }

    protected void saveChkBounceFine()
    {
        if (txtChequeBounceCharge.Text=="")
        {
            txtChequeBounceCharge.Text = "0.00";
        }
        sql = "select count(id) cnt from ChequeBounceFineMaster where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        if (oo.ReturnTag(sql, "cnt") != "0")
        {
            sql = "Update ChequeBounceFineMaster set FineAmount=" + (txtChequeBounceCharge.Text==""?"0.00":txtChequeBounceCharge.Text) + " where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        }
        else
        {
            sql = "Insert into ChequeBounceFineMaster(ChequeStatus, FineAmount, SessionName, BranchCode, LoginName, RecordDate) ";
            sql = sql + " Values('Bounced', " + (txtChequeBounceCharge.Text == "" ? "0.00" : txtChequeBounceCharge.Text) + ", '" + Session["SessionName"].ToString() + "', " + Session["BranchCode"].ToString() + ", '" + Session["LoginName"].ToString() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm tt") + "')";
        }
        SqlCommand cmds = new SqlCommand();
        cmds.CommandText = sql;
        cmds.CommandType = CommandType.Text;
        cmds.Connection = con;
        con.Open();
        try
        {

            cmds.ExecuteNonQuery();
            cmds.Parameters.Clear();
            con.Close();
        }
        catch (Exception ex)
        {
            con.Close();
        }
    }

    public void Days(DropDownList drpdd)
    {
        int i = 0;
        for (i = 1; i <= 31; i++)
        {
            drpdd.Items.Add(i.ToString());
        }


    }


    protected void LinkReset_Click(object sender, EventArgs e)
    {
        if (rdoBasis.SelectedIndex == 0)
        {
            Label4.Text = "Daily Basis fine.";
        }
        else
        {
            Label4.Text = "Range Basis fine.";
        }
        Panel3_ModalPopupExtender.Show();
    }
    protected void DeleteYes_Click(object sender, EventArgs e)
    {
        delete();
        Reset();
    }
    protected void delete()
    {
        sql = "delete from FineSetMaster where sessionname='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        SqlCommand cmds = new SqlCommand();
        cmds.CommandText = sql;
        cmds.CommandType = CommandType.Text;
        cmds.Connection = con;
        try
        {
            con.Open();
            cmds.ExecuteNonQuery();
            cmds.Parameters.Clear();

            con.Close();
        }
        catch (Exception ex)
        {
            con.Close();
        }
    }
    protected void Reset()
    {
        Days(DrpDate);
        
        Days(ddlFromDate22);
        Days(ddlToDate22);

        Days(DrpFromDate1);
        Days(DrpFromDate2);
        Days(DrpFromDate3);
        Days(DrpFromDate4);
        Days(DrpFromDate5);
        Days(DrpFromDate6);
        Days(DrpFromDate7);

        Days(DrpToDate1);
        Days(DrpToDate2);
        Days(DrpToDate3);
        Days(DrpToDate4);
        Days(DrpToDate5);
        Days(DrpToDate6);
        Days(DrpToDate7);

        txtrangeAmount1.Text = "0";
        txtrangeAmount2.Text = "0";
        txtrangeAmount3.Text = "0";
        txtrangeAmount4.Text = "0";
        txtrangeAmount5.Text = "0";
        txtrangeAmount6.Text = "0";
        txtrangeAmount7.Text = "0";
        DisplayGridBasis();
        loadChecks();
        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Deleted successfully", "S");
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
    }


    protected void ddlAppliedFpor_SelectedIndexChanged(object sender, EventArgs e)
    {
        DisplayGridBasis();
        loadChecks();
    }
}