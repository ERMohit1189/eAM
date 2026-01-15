using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class admin_CoscholasticAllotment : Page
{
    SqlConnection con = new SqlConnection();
    SqlCommand cmd = new SqlCommand();
    Campus oo = new Campus();
    string sql = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if ( Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        con = oo.dbGet_connection();
        if (!IsPostBack)
        {
            loadclass();
            loadmedium();
            div1.Visible = false;
            loadclass1();
            loadmedium1();
            loadCoscholastic1();
        }
    }
    public void loadclass()
    {
        sql = "Select Id,ClassName,CidOrder from ClassMaster Where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " Order by CIDOrder";
        oo.FillDropDown_withValue(sql, drpclass, "ClassName", "Id");
    }

    public void loadclass1()
    {
        sql = "Select Id,ClassName,CidOrder from ClassMaster Where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " Order by CIDOrder";
        oo.FillDropDown_withValue(sql, drpclass1, "ClassName", "Id");
    }
    public void loadmedium()
    {
        sql = "Select Medium from MediumMaster";
        sql = sql + " Where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        oo.FillDropDown(sql, drpmedium, "Medium");
    }
    public void loadmedium1()
    {
        sql = "Select Medium from MediumMaster";
        sql = sql + " Where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        oo.FillDropDown(sql, drpmedium1, "Medium");
    }
    public void loadCoscholastic()
    {
        sql = "Select Id,CoscholasticName from CoscholasticMaster Where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        sql = sql + " and ClassId='" + drpclass.SelectedValue.ToString() + "' and Medium='" + drpmedium.SelectedItem.ToString() + "'";
        oo.FillDropDown_withValue(sql, drpCoscholastic, "CoscholasticName", "Id");
    }
    public void loadCoscholastic1()
    {
        sql = "Select Id,CoscholasticName from CoscholasticMaster Where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        sql = sql + " and ClassId='" + drpclass1.SelectedValue.ToString() + "' and Medium='" + drpmedium1.SelectedItem.ToString() + "'";
        oo.FillDropDown_withValue(sql, drpCoscholastic1, "CoscholasticName", "Id");
    }
    protected void drpmedium_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadCoscholastic();
    }
    protected void drpclass_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadCoscholastic();
    }
    public void displayEmpInfo()
    {
        sql = "Select eod.EmpId EmpId,eod.Ecode Ecode,egd.EFirstName+egd.EMiddleName+egd.ELastName as EmpName,egd.EFatherName FatherName,eod.Designation Designation from EmpployeeOfficialDetails eod ";
        sql = sql + " inner join EmpGeneralDetail egd on eod.Ecode=egd.Ecode and eod.EmpId=egd.EmpId where eod.Withdrwal is null  and eodBranchCode=" + Session["BranchCode"] + " and egdBranchCode=" + Session["BranchCode"] + " ";
        sql = sql + " and eod." + drpEnter.SelectedItem.ToString() + "='" + txtEnter.Text.Trim() + "'";
        Grd.DataSource = oo.GridFill(sql);
        Grd.DataBind();
        if (Grd.Rows.Count > 0)
        {
            div1.Visible = true;
        }
        else
        {
            oo.MessageBoxforUpdatePanel("Sorry, No Record found!", lnkShow);
            div1.Visible = false;
        }
    }
    protected void lnkShow_Click(object sender, EventArgs e)
    {
        displayEmpInfo();
        DisplayRecord();
        //loadclassgrd();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        Save();

    }
    public void Save()
    {
        sql = "Select *from CoscholasticAllotment where " + drpEnter.SelectedItem.ToString() + "='" + txtEnter.Text.Trim() + "'";
        sql = sql + " and Classid='" + drpclass.SelectedValue.ToString() + "' and CoscholasticId='" + drpCoscholastic.SelectedValue.ToString() + "' ";
        sql = sql + " and Medium='" + drpmedium.SelectedItem.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"] + "";
        if (oo.Duplicate(sql))
        {
            oo.MessageBoxforUpdatePanel("Duplicate Record!", btnSubmit);
        }
        else
        {
            Label lblEcode = (Label)Grd.Rows[0].FindControl("lblEcode");
            Label lblEmpId = (Label)Grd.Rows[0].FindControl("lblEmpId");
            Label lblEmpName = (Label)Grd.Rows[0].FindControl("lblEmpName");
            Label lblFName = (Label)Grd.Rows[0].FindControl("lblFName");
            Label lblDesi = (Label)Grd.Rows[0].FindControl("lblDesi");

            cmd = new SqlCommand();
            cmd.CommandText = "CoscholasticAllotmentProc";
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmpId", lblEmpId.Text);
            cmd.Parameters.AddWithValue("@Ecode", lblEcode.Text);
            cmd.Parameters.AddWithValue("@EmpName", lblEmpName.Text);
            cmd.Parameters.AddWithValue("@Classid", drpclass.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@Medium", drpmedium.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@CoscholasticId", drpCoscholastic.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@Designation", lblDesi.Text.Trim());
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            DisplayRecord();
            oo.MessageBoxforUpdatePanel("Submitted successfully", btnSubmit);
        }

    }
    public void DisplayRecord()
    {
        sql = "select sctm.Id,sctm.EmpId,sctm.Ecode,sctm.EmpName,cm.ClassName,sctm.Medium,sm.CoscholasticName,";
        sql = sql + " cm.CidOrder from CoscholasticAllotment sctm ";
        sql = sql + " inner join ClassMaster cm on cm.Id=sctm.ClassId inner join CoscholasticMaster sm on sm.Id=sctm.Coscholasticid";
        sql = sql + " inner join EmpployeeOfficialDetails eod on eod.EmpId=sctm.Empid and eod.Ecode=sctm.Ecode";
        sql = sql + " where cm.SessionName='" + Session["SessionName"].ToString() + "' and sctm.SessionName='" + Session["SessionName"].ToString() + "'";
        sql = sql + " and sm.SessionName='" + Session["SessionName"].ToString() + "' and eod.BranchCode=" + Session["BranchCode"] + "  and cm.BranchCode=" + Session["BranchCode"] + "  and sctm.BranchCode=" + Session["BranchCode"] + "";
        sql = sql + " and eod." + drpEnter.SelectedItem.ToString() + "='" + txtEnter.Text.Trim() + "' and eod.Withdrwal is null Order by CidOrder";
        Grd1.DataSource = oo.GridFill(sql);
        Grd1.DataBind();
    }
    public void MergeRows(GridView gridView)
    {
        for (int rowIndex = gridView.Rows.Count - 2; rowIndex >= 0; rowIndex--)
        {
            GridViewRow row = gridView.Rows[rowIndex];
            GridViewRow previousRow = gridView.Rows[rowIndex + 1];
            for (int i = 0; i < row.Cells.Count - 2; i++)
            {
                if (((Label)(row.Cells[i].Controls[1])).Text == ((Label)(previousRow.Cells[i].Controls[1])).Text)
                {
                    row.Cells[i].RowSpan = previousRow.Cells[i].RowSpan < 2 ? 2 : previousRow.Cells[i].RowSpan + 1;
                    previousRow.Cells[i].Visible = false;
                }
            }
        }

    }
    protected void Grd1_PreRender(object sender, EventArgs e)
    {
        //MergeRows(Grd1);
    }
    protected void linkEdit_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lnk = (Label)chk.NamingContainer.FindControl("Label36");
        lblid.Text = lnk.Text;
        sql = "select ClassId,Medium from CoscholasticAllotment ";
        sql = sql + " where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"] + " and Id=" + lnk.Text;
        drpclass1.SelectedValue = oo.ReturnTag(sql, "ClassId");
        drpmedium1.SelectedValue = oo.ReturnTag(sql, "Medium");
        loadCoscholastic1();
        sql = "select CoscholasticId from CoscholasticAllotment ";
        sql = sql + " where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"] + " and Id=" + lnk.Text;
        drpCoscholastic1.SelectedValue = oo.ReturnTag(sql, "CoscholasticId");
        Panel1_ModalPopupExtender.Show();
    }
    protected void drpmedium1_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadCoscholastic1();
        Panel1_ModalPopupExtender.Show();
    }
    protected void drpclass1_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadCoscholastic1();
        Panel1_ModalPopupExtender.Show();
    }
    protected void lnkUpdate_Click(object sender, EventArgs e)
    {
        sql = "Select *from CoscholasticAllotment where " + drpEnter.SelectedItem.ToString() + "='" + txtEnter.Text.Trim() + "'";
        sql = sql + " and Classid='" + drpclass1.SelectedValue.ToString() + "' and CoscholasticId='" + drpCoscholastic1.SelectedValue.ToString() + "' ";
        sql = sql + " and Medium='" + drpmedium1.SelectedItem.ToString() + "' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"].ToString() + "'";
        sql = sql + " and Id<>'" + lblid.Text + "'";
        if (oo.Duplicate(sql))
        {
            oo.MessageBoxforUpdatePanel("Duplicate Record!", btnSubmit);
        }
        else
        {
            cmd = new SqlCommand();
            cmd.CommandText = "CoscholasticAllotmentUpdateProc";
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", lblid.Text);
            cmd.Parameters.AddWithValue("@Classid", drpclass1.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@Medium", drpmedium1.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@CoscholasticId", drpCoscholastic1.SelectedValue.ToString());
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            oo.MessageBoxforUpdatePanel("Updated successfully", lnkUpdate);
            DisplayRecord();
        }
    }
    protected void linkDelete_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lnk = (Label)chk.NamingContainer.FindControl("Label37");
        string ss = lnk.Text;
        lblvalue.Text = ss.ToString();
        Panel2_ModalPopupExtender.Show();
    }
    protected void btnYes_Click(object sender, EventArgs e)
    {
        sql = "Delete From CoscholasticAllotment where SessionName='" + Session["SessionName"].ToString() + "'";
        sql = sql + " and BranchCode=" + Session["BranchCode"].ToString() + " and Id=" + lblvalue.Text;
        cmd.CommandText = sql;
        cmd.Connection = con;
        cmd.CommandType = CommandType.Text;
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        DisplayRecord();
        oo.MessageBoxforUpdatePanel("Deleted successfully", btnYes);


    }
    protected void drpSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadmedium();
    }
    protected void RadioButtonList3_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}