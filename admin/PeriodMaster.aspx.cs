using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class admin_PeriodMaster : Page
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
        if (!IsPostBack)
        {
            DisplayGrid();
        }
    }
    protected void lnkSubmit_Click(object sender, EventArgs e)
    {   
     
        save();    
    }
    public void save()
    {
        string name = "";
        if (RadioButtonList1.Items[0].Selected)
        {
            name = "For Boys";
        }
        else if (RadioButtonList1.Items[1].Selected)
        {
            name="For Girls";
        }
        else
        {
            name="Combined";
        }
        sql = "Select PeriodName from PeriodMaster where Type='" + name + "' and PeriodName='" + txtPeriodName.Text + "' ";
        sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        if(oo.Duplicate(sql))
        {
            oo.MessageBoxforUpdatePanel("Duplicate record!", lnkSubmit);
        }
        else
        {
            cmd = new SqlCommand();
            cmd.CommandText = "PeriodMasterProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@Id", "");
            cmd.Parameters.AddWithValue("@InsertUpdate", "Insert");
            if (RadioButtonList1.Items[0].Selected)
            {
                cmd.Parameters.AddWithValue("@Type", "For Boys");
            }
            else if (RadioButtonList1.Items[1].Selected)
            {
                cmd.Parameters.AddWithValue("@Type", "For Girls");
            }
            else
            {
                cmd.Parameters.AddWithValue("@Type", "Combined");
            }

            cmd.Parameters.AddWithValue("@PeriodName", txtPeriodName.Text.Trim());
            cmd.Parameters.AddWithValue("@FromTime", txtFromHour.Text.Trim()+TextBox1.Text.Trim()+txtFromMinute.Text.Trim());
            cmd.Parameters.AddWithValue("@ToTime", txtToHour.Text.Trim() + TextBox2.Text.Trim() + txtToMinute.Text.Trim());
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            DisplayGrid();
            oo.ClearControls(Table1);
            TextBox1.Text = ":";
            TextBox2.Text = ":";
            txtPeriodName.Focus();
            oo.MessageBoxforUpdatePanel("Submitted successfully", lnkSubmit);
        }
       
    }
    public void DisplayGrid()
    {
        string name = "";
        if (RadioButtonList1.Items[0].Selected)
        {
            name = "For Boys";
        }
        else if (RadioButtonList1.Items[1].Selected)
        {
            name = "For Girls";
        }
        else
        {
            name = "Combined";
        }
        sql = "Select Id,Type,PeriodName,FromTime,ToTime,Convert(nvarchar(50),DATEDIFF(MINUTE,FromTime,ToTime))+' min.' as Time From PeriodMaster where ";
        sql = sql + " Type='"+name+"' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " Order By id";
        Grd.DataSource = oo.GridFill(sql);
        Grd.DataBind();
        if (Grd.Rows.Count > 0)
        {
            sql = "Select Sum(DATEDIFF(MINUTE,FromTime,ToTime)) as TotalTime From PeriodMaster where ";
            sql = sql + " Type='" + name + "' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            int totalMinutes = int.Parse(oo.ReturnTag(sql, "TotalTime"));
            int hours = totalMinutes / 60;
            int minutes = totalMinutes % 60;
            Label lblfooter = (Label)Grd.FooterRow.FindControl("Label7");
            if (hours != 0)
            {
                lblfooter.Text = "Total Time is : " + hours + " hour " + minutes +" minutes";
            }
            else
            {
                lblfooter.Text = "Total Time is : " + minutes + " minutes";
            }
        }
    }
    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        lblId.Text = lnk.Text;
        sql = "Select Type,PeriodName,FromTime,ToTime from PeriodMaster where SessionName='" + Session["SessionName"].ToString() + "'";
        sql = sql + "and BranchCode=" + Session["BranchCode"].ToString() + " and Id=" + lblId.Text;
        Session["Type"] = oo.ReturnTag(sql, "Type");
        if (Session["Type"].ToString() == "For Boys")
        {
            RadioButtonList2.SelectedValue = "For Boys";
        }
        else if (Session["Type"].ToString() == "For Girls")
        {
            RadioButtonList2.SelectedValue = "For Girls";
        }
        else
        {
            RadioButtonList2.Items[2].Selected = true;
        }
        txtPeriodName1.Text = oo.ReturnTag(sql, "PeriodName");
        string[] fromtime = oo.ReturnTag(sql, "FromTime").Split(':');
        txtFromHour1.Text = fromtime[0].ToString().Trim();
        txtFromMinute1.Text = fromtime[1].ToString().Trim();
        string[] totime = oo.ReturnTag(sql, "ToTime").Split(':');
        txtToHour1.Text = totime[0].ToString().Trim();
        txtToMinute1.Text = totime[1].ToString().Trim();
        Panel1_ModalPopupExtender.Show();
    }
    protected void lnkUpdate_Click(object sender, EventArgs e)
    {
        Update();
    }
    public void Update()
    {
        string name = "";
        if (RadioButtonList2.Items[0].Selected)
        {
            name = "For Boys";
        }
        else if (RadioButtonList2.Items[1].Selected)
        {
            name = "For Girls";
        }
        else
        {
            name = "Combined";
        }
        sql = "Select PeriodName from PeriodMaster where Type='" + name + "' and PeriodName='" + txtPeriodName1.Text + "' ";
        sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "'";
        sql = sql + "and BranchCode=" + Session["BranchCode"].ToString() + " and Id<>'" + lblId.Text + "'";
        if (oo.Duplicate(sql))
        {
            oo.MessageBoxforUpdatePanel("Duplicate Record!", lnkSubmit);
        }
        else
        {
            cmd = new SqlCommand();
            cmd.CommandText = "PeriodMasterProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@Id", lblId.Text);
            cmd.Parameters.AddWithValue("@InsertUpdate", "Update");
            if (RadioButtonList2.Items[0].Selected)
            {
                cmd.Parameters.AddWithValue("@Type", "For Boys");
            }
            else if (RadioButtonList2.Items[1].Selected)
            {
                cmd.Parameters.AddWithValue("@Type", "For Girls");
            }
            else
            {
                cmd.Parameters.AddWithValue("@Type", "Combined");
            }

            cmd.Parameters.AddWithValue("@PeriodName", txtPeriodName1.Text.Trim());
            cmd.Parameters.AddWithValue("@FromTime",txtFromHour1.Text.Trim() + TextBox3.Text.Trim() + txtFromMinute1.Text.Trim());
            cmd.Parameters.AddWithValue("@ToTime",  txtToHour1.Text.Trim()   + TextBox5.Text.Trim() + txtToMinute1.Text.Trim());
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            DisplayGrid();
            oo.MessageBoxforUpdatePanel("Updated successfully", lnkSubmit);
        }
    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        lblvalue.Text = lnk.Text;
        Panel2_ModalPopupExtender.Show();
    }
    protected void btnYes_Click(object sender, EventArgs e)
    {
        delete();
    }
    public void delete()
    {
        sql = "Delete From PeriodMaster where SessionName='" + Session["SessionName"].ToString() + "'";
        sql = sql + "and BranchCode=" + Session["BranchCode"].ToString() + " and Id=" + lblvalue.Text;
        cmd = new SqlCommand();
        cmd.CommandText = sql;
        cmd.CommandType = CommandType.Text;
        cmd.Connection = con;
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        DisplayGrid();
        oo.MessageBoxforUpdatePanel("Deleted successfully", btnyes);
    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        DisplayGrid();
    }
  
}