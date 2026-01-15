using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;
using System.Globalization;

public partial class MiscellaneousFeeHeadWise : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "", sql1 = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((string)Session["LoginName"] == "" || (string)Session["BranchCode"] == "" || (string)Session["SessionName"] == "" || Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader); 
        if (!IsPostBack)
        {
            sql = "select ClassName,id from ClassMaster";
            sql = sql + " where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            sql = sql + "  order by Id";
            oo.FillDropDown_withValue(sql, drpClassFrom, "ClassName", "id");
            drpClassFrom.Items.Insert(0, new ListItem("<-- Select Class -->", "<-- Select Class -->"));
            oo.FillDropDown_withValue(sql, drpClassTo, "ClassName", "id");
            drpClassTo.Items.Insert(0, new ListItem("<-- Select Class -->", "<-- Select Class -->"));

            sql = "select Id, FeeHead from FeeHeadMaster where FeeCategory='Miscellaneous Fee' and FeeType='Miscellaneous Fee' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            oo.FillDropDown_withValue(sql, drpFeeHead, "FeeHead", "id");
            drpFeeHead.Items.Insert(0, new ListItem("<-- Select Fee Head -->", "<-- Select Fee Head -->"));
        }
    }
   
    public void FeesDisplay()
    {
        sql = "select Amount from FeeHeadMaster where FeeHead='" + drpFeeHead.SelectedItem.Text + "' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        txtAmt.Text = (oo.ReturnTag(sql, "Amount")==""?"0.00": oo.ReturnTag(sql, "Amount"));
        GridView1.Visible = true;

        sql = " select mf.id, mf.Classid, cm.ClassName, FeeHead, Amount from MiscellaneousFeeHeadWise mf inner join ClassMaster cm on cm.Id = mf.Classid and cm.SessionName = mf.SessionName and cm.BranchCode = mf.BranchCode";
        sql = sql + " where mf.Classid in (select id from ClassMaster where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and Id between " + drpClassFrom.SelectedValue.ToString() + " and " + drpClassTo.SelectedValue.ToString() + ") and mf.SessionName='" + Session["SessionName"].ToString() + "' and mf.BranchCode=" + Session["BranchCode"].ToString() + " ";
        if (drpFeeHead.SelectedValue != "<-- Select Fee Head -->")
        {
            sql = sql + " and mf.FeeHead='" + drpFeeHead.SelectedItem.Text + "' ";
        }
        GridView1.DataSource = oo.GridFill(sql);
        GridView1.DataBind();

        if (GridView1.Rows.Count > 0)
        {
            int i;
            double tot = 0;
            for (i = 0; i <= GridView1.Rows.Count - 1; i++)
            {
                Label lblAmt = (Label)GridView1.Rows[i].FindControl("lblAmt");
                Label ClassId = (Label)GridView1.Rows[i].FindControl("lblClassId");

                tot = tot + Convert.ToDouble(lblAmt.Text==""?"0": lblAmt.Text);

                LinkButton edit = (LinkButton)GridView1.Rows[i].FindControl("LinkEdit");
                LinkButton delete = (LinkButton)GridView1.Rows[i].FindControl("LinDelete");

                sql = "select count(*) cnt from MiscellaneousFeeAllotment where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and Classid="+ ClassId.Text + "";
                if (oo.ReturnTag(sql, "cnt") != "0" && oo.ReturnTag(sql, "cnt") != "")
                {
                    edit.Enabled = false;
                    delete.Enabled = false;
                    edit.Text = "<i class='fa fa-lock'></i>";
                    delete.Text = "<i class='fa fa-lock'></i>";
                }

            }
            Label lblTotalAmt = (Label)GridView1.FooterRow.FindControl("lblTotalAmt");
            lblTotalAmt.Text ="Total : "+ tot.ToString(CultureInfo.InvariantCulture)+".00";
        }
    }

    protected void LinkSubmit_Click(object sender, EventArgs e)
    {
        double val = double.Parse(txtAmt.Text.Trim());
        if (val==0)
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please enter valid amount!", "A");
            return;
        }
        int fromclass = int.Parse(drpClassFrom.SelectedValue);
        int toclass = int.Parse(drpClassTo.SelectedValue);
        if (fromclass> toclass)
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please select valid class range!", "A");
            return;
        }
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "MiscellaneousFeeHeadWiseProc";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
        cmd.Parameters.AddWithValue("@ClassidFrom", drpClassFrom.SelectedValue.ToString());
        cmd.Parameters.AddWithValue("@ClassidTo", drpClassTo.SelectedValue.ToString());
        cmd.Parameters.AddWithValue("@FeeHead", drpFeeHead.SelectedItem.ToString());
        cmd.Parameters.AddWithValue("@Amount", txtAmt.Text.Trim().ToString());
        cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
        cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
        cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
        cmd.Parameters.AddWithValue("@action", "save");
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            txtAmt.Text = "0.00";
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");
            FeesDisplay();
        }
        catch (SqlException exs)
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, exs.Message, "A");
        }
        catch (Exception ex) { }

    }

    protected void TextBox2_TextChanged(object sender, EventArgs e)
    {

    }
    
    protected void LinkEdit_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("lblId");
        Label lblAmt = (Label)chk.NamingContainer.FindControl("lblAmt");
        string ss = lblId.Text;
        lblID.Text = ss;
        
        txtFeePaymentPanelAmt.Text = lblAmt.Text;
        Panel1_ModalPopupExtender.Show();
    }
   
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        sql = "Delete from MiscellaneousFeeHeadWise where Id=" + lblvalue.Text;
        sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = sql;
        cmd.CommandType = CommandType.Text;
        cmd.Connection = con;
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Deleted successfully.", "S");       
            FeesDisplay();
        }
        catch (SqlException) { }
    }
    protected void LinkDelete_Click(object sender, EventArgs e)
    {
        BtnDeleteCancel.Focus();
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("lblId");
        string ss = lblId.Text;
        lblvalue.Text = ss.ToString();
       
        Panel2_ModalPopupExtender.Show();
    }
    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "MiscellaneousFeeHeadWiseProc";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
        cmd.Parameters.AddWithValue("@id",lblID.Text);
        cmd.Parameters.AddWithValue("@Amount", txtFeePaymentPanelAmt.Text.ToString());
        cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
        cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
        cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
        cmd.Parameters.AddWithValue("@action", "update");
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully.", "S");       
            FeesDisplay();
        }
        catch (Exception ex) { }
    }
    protected void BtnEditCancel_Click(object sender, EventArgs e)
    {

    }
    protected void BtnDeleteCancel_Click(object sender, EventArgs e)
    {

    }

    protected void drpClassTo_SelectedIndexChanged(object sender, EventArgs e)
    {
        drpFeeHead.SelectedIndex = 0;
        txtAmt.Text ="0.00";
        FeesDisplay();
    }

    protected void drpClassFrom_SelectedIndexChanged(object sender, EventArgs e)
    {
        drpFeeHead.SelectedIndex = 0;
        txtAmt.Text = "0.00";
        FeesDisplay();
    }

    protected void drpFeeHead_SelectedIndexChanged(object sender, EventArgs e)
    {
        FeesDisplay();
    }
}
  
