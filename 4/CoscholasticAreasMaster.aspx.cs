using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class admin_CoscholasticAreasMaster : Page
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
        Campus camp = new Campus(); camp.LoadLoader(loader); 
        if (!IsPostBack)
        {
            sql = "Select Medium from MediumMaster";
            sql = sql + " where BranchCode=" + Session["BranchCode"].ToString() + "";
            oo.FillDropDown(sql, drpmedium, "Medium");

            sql = "Select Id,ClassName from ClassMaster";
            sql = sql + " where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            sql = sql + "  order by CIDOrder";
            if (oo.Duplicate(sql) == false)
            {
                //oo.MessageBox("Class Master Not Fill!", this.Page);
                camp.msgbox(this.Page, msgbox, "Class Master Not Fill!", "A");       

            }
            oo.FillDropDown_withValue(sql, drpclass, "ClassName", "Id");
            oo.FillDropDown_withValue(sql, drpclass0, "ClassName", "Id");
            DisplayGrid();
        }
    }

    public void DisplayGrid()
    {
        sql = "select ROW_NUMBER() OVER (ORDER BY sgm.Id ASC) AS SrNo, sgm.Id, sgm.DisplayOrder,";
        sql = sql + " cm.ClassName as ClassName,CoscholasticGroup,Medium,sgm.ClassId";
        sql = sql + " from CoscholasticGroupMaster sgm inner join ClassMaster cm on cm.Id=sgm.ClassId where ";
        sql = sql + " sgm.SessionName='" + Session["SessionName"].ToString() + "' and cm.SessionName='" + Session["SessionName"].ToString() + "' ";
        sql = sql + " and sgm.BranchCode=" + Session["BranchCode"].ToString() + " and cm.BranchCode=" + Session["BranchCode"].ToString() + " and sgm.ClassID='" + drpclass.SelectedValue.ToString() + "'";
        if (drpmedium.SelectedIndex != 0)
        {
            sql = sql + " and sgm.Medium='" + drpmedium.SelectedItem.Text.ToString() + "'";
        }
        if (drpPart.SelectedIndex != 0)
        {
            sql = sql + " and sgm.PartId='" + drpPart.SelectedValue.ToString() + "'";
        }
        //sql = sql + " order by sgm.DisplayOrder asc";
        Grd.DataSource = oo.GridFill(sql);
        Grd.DataBind();
    }

    protected void DropDownList1_SelectedIndexChanged1(object sender, EventArgs e)
    {
        DisplayGrid();
    }
    protected void drpclass0_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void drpSection_SelectedIndexChanged(object sender, EventArgs e)
    {
       
            DisplayGridbySection();
        
    }
    public void DisplayGridbySection()
    {
       
            sql = "select ROW_NUMBER() OVER (ORDER BY sgm.Id ASC) AS SrNo, sgm.Id,";
            sql = sql + " cm.ClassName as ClassName,CoscholasticGroup,Medium,sgm.ClassId ";
            sql = sql + " from CoscholasticGroupMaster sgm inner join ClassMaster cm on cm.Id=sgm.ClassId where ";
            sql = sql + " sgm.SessionName='" + Session["SessionName"].ToString() + "' and cm.SessionName='" + Session["SessionName"].ToString() + "' ";
            sql = sql + " and sgm.BranchCode=" + Session["BranchCode"].ToString() + " and cm.BranchCode=" + Session["BranchCode"].ToString() + " and sgm.ClassID='" + drpclass.SelectedValue.ToString() + "'";
            Grd.DataSource = oo.GridFill(sql);
            Grd.DataBind();
    }

    protected void lnkSubmit_Click(object sender, EventArgs e)
    {

        if (drpclass.SelectedIndex > drpclass0.SelectedIndex)
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please select valid class range", "A");
            return;
        }

        sql = "Select CoscholasticGroup from CoscholasticgroupMaster where ClassId='" + drpclass.SelectedValue.ToString() + "'  and CoscholasticGroup='" + TextBox1.Text + "'";
        sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        sql = sql + " and Medium='" + drpmedium.SelectedItem.ToString() + "'";
        if (oo.Duplicate(sql))
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Already alloted", "W");       
        }
        else if (drpclass.SelectedItem.Text == "<--Select-->")
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Select Class Name", "A");       
        }
        else
        {
            if (RadioButtonList2.SelectedIndex != 0)
            {
                if (drpclass0.SelectedItem.Text == "<--Select-->")
                {
                    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Select Class Name", "A");       
                }
                else
                {
                    save();
                    oo.ClearControls(this.Page);
                    DisplayGrid();
                    TextBox1.Focus();
                }
            }
            else
            {

                save0();
                oo.ClearControls(this.Page);
                DisplayGrid();
                TextBox1.Focus();
            }
        }
    }

    public void save()
    {
        for (int i = drpclass.SelectedIndex; i <= drpclass0.SelectedIndex; i++)
        {
            cmd = new SqlCommand();
            cmd.CommandText = "CoscholasticGroupMasterProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@Type", "Insert");
            cmd.Parameters.AddWithValue("@Id", "0");
            cmd.Parameters.AddWithValue("@ClassId", drpclass.Items[i].Value.ToString());
            cmd.Parameters.AddWithValue("@PartId", drpPart.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@Medium", drpmedium.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@CoscholasticGroup", TextBox1.Text.Trim());
            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");
            }
            catch
            {
            }
        }
    }

    public void save0()
    {
        
            cmd = new SqlCommand();
            cmd.CommandText = "CoscholasticGroupMasterProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@Type", "Insert");
            cmd.Parameters.AddWithValue("@Id", "0");
            cmd.Parameters.AddWithValue("@ClassId", drpclass.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@PartId", drpPart.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@Medium", drpmedium.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@CoscholasticGroup", TextBox1.Text.Trim());
            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                //oo.MessageBoxforUpdatePanel("Submitted successfully.", lnkSubmit);
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");       

            }
            catch
            {
            }
        
    }

    

    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblId2 = (Label)chk.NamingContainer.FindControl("Label36");
       
        
        GridViewRow gvr = (GridViewRow)(sender as Control).Parent.Parent;
        Label Label6 = (Label)gvr.FindControl("Label6");
        Label Label4 = (Label)gvr.FindControl("Label4");
      
        lblClassId0.Text = Label6.Text;
        lblMedium0.Text = Label4.Text;
     
        string ss = lblId2.Text;
        lblID.Text = ss;
        sql = " select CoscholasticGroup,partid from CoscholasticGroupMaster ";
        sql = sql + " where Id=" + ss;
        sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        txtCoscholasticPanel.Text = oo.ReturnTag(sql, "CoscholasticGroup");
        ddlPartPanel.SelectedValue= oo.ReturnTag(sql, "partid");
        Panel1_ModalPopupExtender.Show();
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        sql = "Select CoscholasticGroup from CoscholasticGroupMaster where ClassId='" + lblClassId0.Text + "'  and CoscholasticGroup='" + txtCoscholasticPanel.Text.ToString() + "'";
        sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        sql = sql + " and Medium='" + lblMedium0.Text + "' and Id<>'" + lblID.Text + "'";

        if (oo.Duplicate(sql))
        {
            //oo.MessageBoxforUpdatePanel("Already alloted", Button3);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Already alloted", "A");       

            DisplayGrid();
        }
        else
        {
            cmd = new SqlCommand();
            cmd.CommandText = "CoscholasticGroupMasterProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@Type", "Update");
            cmd.Parameters.AddWithValue("@Id", lblID.Text);
            cmd.Parameters.AddWithValue("@ClassId", 1);
            cmd.Parameters.AddWithValue("@Medium", "");
            cmd.Parameters.AddWithValue("@PartId", ddlPartPanel.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@CoscholasticGroup", txtCoscholasticPanel.Text.Trim());
            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                DisplayGrid();
                //oo.MessageBoxforUpdatePanel("Updated successfully.", Button3);
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully.", "S");       

            }
            catch
            {
            }
        }
    }

    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        Button8.Focus();
        LinkButton chk = (LinkButton)sender;
        Label lblId3 = (Label)chk.NamingContainer.FindControl("Label37");
        string ss = lblId3.Text;
        lblvalue.Text = ss.ToString();
        Panel2_ModalPopupExtender.Show();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        sql = "Delete from CoscholasticGroupMaster where Id=" + lblvalue.Text;
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
            //oo.MessageBoxforUpdatePanel("Deleted successfully.", btnDelete);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Deleted successfully.", "S");       

            DisplayGrid();
        }
        catch (SqlException) { }
    }
    protected void drpPart_SelectedIndexChanged(object sender, EventArgs e)
    {
        DisplayGrid();
    }
    protected void drpmedium_SelectedIndexChanged(object sender, EventArgs e)
    {
        DisplayGrid();
    }


    protected void txtDO_TextChanged(object sender, EventArgs e)
    {
        TextBox chk = (TextBox)sender;
        Label id = (Label)chk.NamingContainer.FindControl("LabelDO");
        TextBox DO = (TextBox)chk.NamingContainer.FindControl("txtDO");

        string orderValue = DO.Text;

        sql = "update CoscholasticGroupMaster set DisplayOrder='"+ orderValue + "' where Id=" + id.Text;
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
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully.", "S");
            DisplayGrid();
        }
        catch (SqlException) { }
    }
}