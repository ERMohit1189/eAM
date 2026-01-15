using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;

public partial class feeHeadMaster : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((string)Session["LoginName"] == "" || (string)Session["BranchCode"] == "" || (string)Session["SessionName"] == "" || Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        con = oo.dbGet_connection();
        if (!IsPostBack)
        {
            Campus camp = new Campus(); camp.LoadLoader(loader);
            loadGrid();
            string sql1s = "Select top(1) Priority from FeeHeadMaster where BranchCode=" + Session["BranchCode"].ToString() + " and FeeType not in ('Transport Fee', 'Hostel Fee', 'Fine (Late Fee)', 'Cheque Bounce Charge') order by Priority desc";
            if (oo.Duplicate(sql1s))
            {
                txtPriority.Text = (int.Parse((oo.ReturnTag(sql1s, "Priority") == "" ? "1" : oo.ReturnTag(sql1s, "Priority"))) + 1).ToString();
                if (int.Parse(txtPriority.Text)>99)
                {
                    txtPriority.Text = "1";
                }
            }
            else
            {
                txtPriority.Text = "1";
            }
            divPriority.Visible = true;
        }
    }
    private void loadGrid()
    {
       
        ddlFeeTypePanel.SelectedIndex = 0;
        txtFeeHeadPanel.Text = "";
        txtPriorityPanel.Text = "";
        string sql1st = "Select top(1) Priority from FeeHeadMaster where BranchCode=" + Session["BranchCode"].ToString() + " and FeeType not in ('Transport Fee', 'Hostel Fee', 'Fine (Late Fee)', 'Cheque Bounce Charge') order by Priority desc";
        if (oo.Duplicate(sql1st))
        {
            txtPriority.Text = (int.Parse((oo.ReturnTag(sql1st, "Priority") == "" ? "1" : oo.ReturnTag(sql1st, "Priority"))) + 1).ToString();
            if (int.Parse(txtPriority.Text) > 99)
            {
                txtPriority.Text = "1";
            }
        }
        else
        {
            txtPriority.Text = "1";
        }
        divPriority.Visible = true;
        Grds.DataSource = null;
        Grds.DataBind();
        string sql1s = "Select * from FeeHeadMaster where BranchCode=" + Session["BranchCode"].ToString() + " order by Priority asc";
        Grds.DataSource = oo.Fetchdata(sql1s);
        Grds.DataBind();
        if (Grds.Rows.Count > 0)
        {
            
            for (int i = 0; i < Grds.Rows.Count; i++)
            {
                Label lblFeeType = (Label)Grds.Rows[i].FindControl("lblFeeType");
                Label lblPriority = (Label)Grds.Rows[i].FindControl("lblPriority");
                Label feeheadid = (Label)Grds.Rows[i].FindControl("EditId");

                if (lblFeeType.Text == "Transport Fee")
                {
                    lblPriority.Visible = false;
                }
                if (lblFeeType.Text == "Hostel Fee")
                {
                    lblPriority.Visible = false;
                }
                if (lblFeeType.Text == "Fine (Late Fee)")
                {
                    lblPriority.Visible = false;
                }
                if (lblFeeType.Text == "Cheque Bounce Charge")
                {
                    lblPriority.Visible = false;
                }
                LinkButton btnEdit = (LinkButton)Grds.Rows[i].FindControl("btnEdit");
                LinkButton btnDelete = (LinkButton)Grds.Rows[i].FindControl("btnDelete");

                string sql1 = "select count(*) cnt from FeeAllotedForClassWise where FeeHeadid=" + feeheadid.Text + " and BranchCode=" + Session["BranchCode"].ToString() + "";
                if (oo.ReturnTag(sql1, "cnt") != "0")
                {
                    btnEdit.Enabled = false;
                    btnDelete.Enabled = false;
                    btnEdit.Text = "<i class='fa fa-lock'></i>";
                    btnDelete.Text = "<i class='fa fa-lock'></i>";
                }
                string sql2 = "select count(*) cnt from CompositFeeDeposit where BranchCode=" + Session["BranchCode"].ToString() + "  and FeeHeadid=" + feeheadid.Text + " and FeeHeadid in (Select id from FeeHeadMaster where BranchCode=" + Session["BranchCode"].ToString() + " and FeeType not in ('Tuition Fee','Tuition Fee (Optional)'))";
                if (oo.ReturnTag(sql2, "cnt") != "0")
                {
                    btnEdit.Enabled = false;
                    btnDelete.Enabled = false;
                    btnEdit.Text = "<i class='fa fa-lock'></i>";
                    btnDelete.Text = "<i class='fa fa-lock'></i>";
                }

            }
        }
    }
    protected void ddlFeeType_SelectedIndexChanged(object sender, EventArgs e)
    {
        sql = "select FeeType  from FeeHeadMaster where FeeType='" + ddlFeeType.SelectedItem.ToString() + "'";
        sql = sql + " and BranchCode=" + Session["BranchCode"].ToString() + "";
        if (oo.ReturnTag(sql, "FeeType") == "Transport Fee")
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Transport Fee head could not  more than one!", "A");
            return;
        }
        if (oo.ReturnTag(sql, "FeeType") == "Hostel Fee")
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Hostel Fee head could not  more than one!", "A");
            return;
        }
        if (oo.ReturnTag(sql, "FeeType") == "Fine (Late Fee)")
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Fine (Late Fee) head could not  more than one!", "A");
            return;
        }
        if (oo.ReturnTag(sql, "FeeType") == "Cheque Bounce Charge")
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Cheque Bounce Charge head could not  more than one!", "A");
            return;
        }

        if (ddlFeeType.SelectedValue.ToString() == "Transport Fee")
        {
            txtPriority.Text = "100";
            divPriority.Visible = false;
        }
        else if (ddlFeeType.SelectedValue.ToString() == "Hostel Fee")
        {
            txtPriority.Text = "101";
            divPriority.Visible = false;
        }
        else if (ddlFeeType.SelectedValue.ToString() == "Fine (Late Fee)")
        {
            txtPriority.Text = "102";
            divPriority.Visible = false;
        }
        else if (ddlFeeType.SelectedValue.ToString() == "Cheque Bounce Charge")
        {
            txtPriority.Text = "103";
            divPriority.Visible = false;
        }
        else
        {
            string sql1s = "Select top(1) Priority from FeeHeadMaster where BranchCode=" + Session["BranchCode"].ToString() + " and FeeType not in ('Transport Fee', 'Hostel Fee', 'Fine (Late Fee)', 'Cheque Bounce Charge') order by Priority desc";
            if (oo.Duplicate(sql1s))
            {
                txtPriority.Text = (int.Parse((oo.ReturnTag(sql1s, "Priority") == "" ? "1" : oo.ReturnTag(sql1s, "Priority"))) + 1).ToString();
                if (int.Parse(txtPriority.Text) > 99)
                {
                    txtPriority.Text = "1";
                }
            }
            else
            {
                txtPriority.Text = "1";
            }
            divPriority.Visible = true;
        }
        loadGrid();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        sql = "select top(1) FeeHead  from FeeHeadMaster where FeeType='" + ddlFeeType.SelectedValue.ToString() + "'";
        sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        if (oo.ReturnTag(sql, "FeeHead") == "Transport Fee")
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Transport Fee head could not  more than one!", "A");
            return;
        }
        if (oo.ReturnTag(sql, "FeeHead") == "Hostel Fee")
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Hostel Fee head could not  more than one!", "A");
            return;
        }
        if (oo.ReturnTag(sql, "FeeHead") == "Fine (Late Fee)")
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Fine (Late Fee) head could not  more than one!", "A");
            return;
        }
        if (oo.ReturnTag(sql, "FeeHead") == "Cheque Bounce Charge")
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Cheque Bounce Charge head could not  more than one!", "A");
            return;
        }
        sql = "select FeeHead  from FeeHeadMaster where FeeHead='" + txtFeeHead.Text.Trim() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "sp_FeeHeadMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
        cmd.Parameters.AddWithValue("@FeeType", ddlFeeType.SelectedValue.ToString());
        cmd.Parameters.AddWithValue("@FeeHead", txtFeeHead.Text.Trim().ToString());
        if (ddlFeeType.SelectedValue == "Transport Fee")
        {
            cmd.Parameters.AddWithValue("@Priority", "100");
        }
        else if (ddlFeeType.SelectedValue == "Hostel Fee")
        {
            cmd.Parameters.AddWithValue("@Priority", "101");
        }
        else if (ddlFeeType.SelectedValue == "Fine (Late Fee)")
        {
            cmd.Parameters.AddWithValue("@Priority", "102");
        }
        else if (ddlFeeType.SelectedValue == "Cheque Bounce Charge")
        {
            cmd.Parameters.AddWithValue("@Priority", "103");
        }
        else
        {
            cmd.Parameters.AddWithValue("@Priority", txtPriority.Text.Trim());
        }
        cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
        cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
        cmd.Parameters.AddWithValue("@Action", "Insert");
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");
            ddlFeeType.SelectedIndex = 0;
            txtFeeHead.Text = "";
            txtPriority.Text = "";
            loadGrid();
        }
        catch (Exception) { }
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("EditId");
        string ss = lblId.Text;
        lblID.Text = ss;
        sql = "Select * from FeeHeadMaster where Id=" + ss;
        sql = sql + " and BranchCode=" + Session["BranchCode"].ToString() + "";

        ddlFeeTypePanel.SelectedValue = oo.ReturnTag(sql, "FeeType");
        txtFeeHeadPanel.Text = oo.ReturnTag(sql, "FeeHead");

        if (ddlFeeTypePanel.SelectedValue.ToString() == "Transport Fee")
        {
            txtPriorityPanel.Text = "100";
            trPriority.Visible = false;
        }
        else if (ddlFeeTypePanel.SelectedValue.ToString() == "Hostel Fee")
        {
            txtPriorityPanel.Text = "101";
            trPriority.Visible = false;
        }
        else if (ddlFeeTypePanel.SelectedValue.ToString() == "Fine (Late Fee)")
        {
            txtPriorityPanel.Text = "102";
            trPriority.Visible = false;
        }
        else if (ddlFeeTypePanel.SelectedValue.ToString() == "Cheque Bounce Charge")
        {
            txtPriorityPanel.Text = "103";
            trPriority.Visible = false;
        }
        else
        {
            txtPriorityPanel.Text = (oo.ReturnTag(sql, "Priority") == "" ? "1" : oo.ReturnTag(sql, "Priority"));
            if (int.Parse(txtPriority.Text) > 99)
            {
                txtPriority.Text = "1";
            }
            trPriority.Visible = true;
        }
        Panel1_ModalPopupExtender.Show();
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        sql = "select FeeHead  from FeeHeadMaster where FeeHead='" + txtFeeHeadPanel.Text + "'";
        sql = sql + "  and BranchCode=" + Session["BranchCode"].ToString() + " and FeeType<>'" + ddlFeeTypePanel.SelectedValue + "'";
        if (oo.Duplicate(sql))
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox2, "Duplicate Entry!", "A");
            Panel1_ModalPopupExtender.Show();
        }
        else
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_FeeHeadMaster";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", lblID.Text);
            cmd.Parameters.AddWithValue("@FeeHead", txtFeeHeadPanel.Text.Trim().ToString());
            if (ddlFeeTypePanel.SelectedValue == "Transport Fee")
            {
                cmd.Parameters.AddWithValue("@Priority", "100");
            }
            else if (ddlFeeTypePanel.SelectedValue == "Hostel Fee")
            {
                cmd.Parameters.AddWithValue("@Priority", "101");
            }
            else if (ddlFeeTypePanel.SelectedValue == "Fine (Late Fee)")
            {
                cmd.Parameters.AddWithValue("@Priority", "102");
            }
            else if (ddlFeeTypePanel.SelectedValue == "Cheque Bounce Charge")
            {
                cmd.Parameters.AddWithValue("@Priority", "103");
            }
            else
            {
                cmd.Parameters.AddWithValue("@Priority", txtPriorityPanel.Text.Trim());
            }
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@Action", "Update");
            cmd.Connection = con;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully.", "S");
                loadGrid();
            }
            catch (SqlException ex) { }
       }
    }
    protected void Button4_Click(object sender, EventArgs e)
    {

    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        Button8.Focus();
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("deleteId");
        string ss = lblId.Text;
        lblvalue.Text = ss.ToString();
        Panel2_ModalPopupExtender.Show();

    }

    protected void btnDeleteYes_Click(object sender, EventArgs e)
    {
        sql = "Delete from FeeHeadMaster where Id=" + lblvalue.Text;
        sql = sql + " and BranchCode=" + Session["BranchCode"].ToString() + "";

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
            loadGrid();
        }
        catch (SqlException) { }

    }
    protected void Button8_Click(object sender, EventArgs e)
    {

    }

    protected void txtPriority_TextChanged(object sender, EventArgs e)
    {
        if (txtPriority.Text.Length>2)
        {
            string sql1s = "Select top(1) Priority from FeeHeadMaster where BranchCode=" + Session["BranchCode"].ToString() + " and FeeType not in ('Transport Fee', 'Hostel Fee', 'Fine (Late Fee)', 'Cheque Bounce Charge') order by Priority desc";
            if (oo.Duplicate(sql1s))
            {
                txtPriority.Text = (int.Parse((oo.ReturnTag(sql1s, "Priority") == "" ? "1" : oo.ReturnTag(sql1s, "Priority"))) + 1).ToString();
            }
            else
            {
                txtPriority.Text = "1";
            }
            divPriority.Visible = true;
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Maximum 2 digits are allowed!", "A");
        }
    }
}