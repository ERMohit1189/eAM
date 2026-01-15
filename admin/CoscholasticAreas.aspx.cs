using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class CoscholasticAreas : Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = string.Empty;

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

            try
            {
                CheckValueADDDeleteUpdate();
            }
            catch (Exception) { }

            sql = "Select Medium from MediumMaster";
            sql = sql + " where BranchCode=" + Session["BranchCode"].ToString() + "";
            oo.FillDropDown(sql, drpmedium, "Medium");
            //oo.FillDropDown(sql, DropMediumPanel, "Medium");

            sql = "Select Id,ClassName from ClassMaster";
            sql = sql + " where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            sql = sql + "  order by CIDOrder";
            if (oo.Duplicate(sql) == false)
            {
                //oo.MessageBox("Class Master Not Fill!", this.Page);
                 camp.msgbox(this.Page, msgbox, "Class Master Not Fill!", "A");       

            }
            oo.FillDropDown_withValue(sql, drpclass, "ClassName", "Id");
            drpclass.Items.Insert(0, new ListItem("<--Select-->", "-1"));
            drpGroupName.Items.Insert(0, new ListItem("<--Select-->", "-1"));
            oo.FillDropDown_withValue(sql, drpclass0, "ClassName", "Id");
            oo.FillDropDown_withValue(sql, drpClassPanel, "ClassName", "Id");
            DisplayGrid();
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        sql = "Select CoscholasticName from CoscholasticMaster where ClassId='" + drpclass.SelectedValue.ToString() + "'  and CoscholasticName='" + TextBox1.Text + "'";
        sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        //sql = sql + "  and PaperId='" + drppaper.SelectedValue.ToString() + "' and EvalId='"+drpEval.SelectedValue.ToString()+"'";
        sql = sql + " and GroupId='"+drpGroupName.SelectedValue.ToString()+"' and Medium='" + drpmedium.SelectedItem.ToString() + "'";

        if (oo.Duplicate(sql))
        {
            //oo.MessageBoxforUpdatePanel("Already alloted", LinkButton1);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Already alloted", "A");       

        }

        else if (drpclass.SelectedItem.Text == "<--Select-->")
        {
            //oo.MessageBoxforUpdatePanel("Select Class Name", LinkButton1);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Select Class Name", "A");       


        }

        else
        {
            if (RadioButtonList2.SelectedIndex != 0)
            {
                if (drpclass0.SelectedItem.Text == "<--Select-->")
                {
                    //oo.MessageBoxforUpdatePanel("Select Class Name", LinkButton1);
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
         
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CoscholasticMasterProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                cmd.Parameters.AddWithValue("@ClassId", drpclass.Items[i].Value.ToString());
                cmd.Parameters.AddWithValue("@SelectedItem", drpOptional.SelectedIndex == 0 ? "1" : "0");
                cmd.Parameters.AddWithValue("@CoscholasticName", TextBox1.Text.ToString());
                cmd.Parameters.AddWithValue("@CoscholasticCode", TextBox3.Text.ToString());
                cmd.Parameters.AddWithValue("@MaxMark", TextBox4.Text.Trim());
                cmd.Parameters.AddWithValue("@ModeOfPaper", DropDownList2.Text.ToString());
                cmd.Parameters.AddWithValue("@Remark", TextBox2.Text.ToString());
                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                cmd.Parameters.AddWithValue("@Medium", drpmedium.Text.ToString());
                cmd.Parameters.AddWithValue("@GroupId", drpGroupName.SelectedValue.ToString());
                try
                {

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    //oo.MessageBoxforUpdatePanel("Submited successfully", LinkButton1);
                    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submited successfully", "S");       


                }
                catch (SqlException) { }
            }
       

    }
    public void save0()
    {
     
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "CoscholasticMasterProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@ClassId", drpclass.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@SelectedItem", drpOptional.SelectedIndex == 0 ? "1" : "0");
            cmd.Parameters.AddWithValue("@CoscholasticName", TextBox1.Text.ToString());
            cmd.Parameters.AddWithValue("@CoscholasticCode", TextBox3.Text.ToString());
            cmd.Parameters.AddWithValue("@MaxMark", TextBox4.Text.Trim());
            cmd.Parameters.AddWithValue("@ModeOfPaper", DropDownList2.Text.ToString());
            cmd.Parameters.AddWithValue("@Remark", TextBox2.Text.ToString());
            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
            cmd.Parameters.AddWithValue("@Medium", drpmedium.Text.ToString());
            cmd.Parameters.AddWithValue("@GroupId", drpGroupName.SelectedValue.ToString());
            try
            {

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                //oo.MessageBoxforUpdatePanel("Submited successfully", LinkButton1);
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submited successfully", "S");       


            }
            catch (SqlException) { }
       
    }
    
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblId2 = (Label)chk.NamingContainer.FindControl("Label36");
        string ss = lblId2.Text;
        lblID.Text = ss;

        sql = " select  ROW_NUMBER() OVER (ORDER BY Id ASC) AS SrNo, Id,ClassID,SelectedItem,CoscholasticName,Medium,MaxMarks,CoscholasticCode,ModeOfPaper,Remark from CoscholasticMaster ";
        sql = sql + " where Id=" + ss;
        sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        drpClassPanel.SelectedValue = oo.ReturnTag(sql, "ClassId");
        drpModePaperPanel.Text = oo.ReturnTag(sql, "ModeOfPaper");
        try
        {
            drpOptionalPanel.SelectedValue = oo.ReturnTag(sql, "SelectedItem").ToString();
        }
        catch
        {
            drpOptionalPanel.SelectedIndex = 0;
        }
        //if (oo.ReturnTag(sql, "SelectedItem").Trim() == "0")
        //{
        //    drp
        //}
        //else
        //{
        //    RdoCategoryPanel.Items[1].Selected.ToString();
        //}
        txtCoscholasticPanel.Text = oo.ReturnTag(sql, "CoscholasticName");
        txtCoscholasticCodePanel.Text = oo.ReturnTag(sql, "CoscholasticCode");
        txtRemarkPanel.Text = oo.ReturnTag(sql, "remark");
      //  DropMediumPanel.Text = oo.ReturnTag(sql, "Medium");
        TxtPanelMaxMarks.Text = oo.ReturnTag(sql, "MaxMarks");

        loadGroup1();
        sql = " select  GroupId from CoscholasticMaster where Id=" + ss;
        sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        drpGroupNamePanel.SelectedValue = oo.ReturnTag(sql, "GroupId");
        Panel1_ModalPopupExtender.Show();
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
        sql = "Delete from CoscholasticMaster where Id=" + lblvalue.Text;
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
            //oo.MessageBox("Deleted successfully.", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Deleted successfully", "S");       

            DisplayGrid();
        }
        catch (SqlException) { }
    }
    protected void Button8_Click(object sender, EventArgs e)
    {

    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        sql = "Select CoscholasticName from CoscholasticMaster where ClassId='" + drpClassPanel.SelectedValue.ToString() + "'  and CoscholasticName='" + txtCoscholasticPanel.Text.ToString() + "'";
        sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        //sql = sql + "  and PaperId='" + DrpPanelPaper.SelectedValue.ToString() + "' and EvalId='" + drpEval1.SelectedValue.ToString() + "'";
        sql = sql + " and GroupId='" + drpGroupNamePanel.SelectedValue.ToString() + "'  and Id<>'" + lblID.Text + "'";

        if (oo.Duplicate(sql))
        {
            //oo.MessageBoxforUpdatePanel("Already alloted", LinkButton1);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Already alloted", "S");       

            DisplayGrid();
        }
        else
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "CoscholasticMasterUpdateProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@id", lblID.Text);
            cmd.Parameters.AddWithValue("@ClassId", drpClassPanel.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@SelectedItem", drpOptionalPanel.SelectedIndex == 0 ? "1" : "0");
            cmd.Parameters.AddWithValue("@CoscholasticName", txtCoscholasticPanel.Text.ToString());
            cmd.Parameters.AddWithValue("@CoscholasticCode", txtCoscholasticCodePanel.Text.ToString());
            cmd.Parameters.AddWithValue("@MaxMark", TxtPanelMaxMarks.Text.Trim());
            cmd.Parameters.AddWithValue("@ModeOfPaper", drpModePaperPanel.Text.ToString());
            cmd.Parameters.AddWithValue("@Remark", txtRemarkPanel.Text.ToString());
            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
           // cmd.Parameters.AddWithValue("@Medium", DropMediumPanel.Text.ToString());
            cmd.Parameters.AddWithValue("@GroupId", drpGroupNamePanel.SelectedValue.ToString());
            try
            {

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                //oo.MessageBoxforUpdatePanel("Updated successfully.", Button3);
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully.", "S");       

                DisplayGrid();

            }
            catch (Exception) { }
        }
    }
    protected void Button4_Click(object sender, EventArgs e)
    {

    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    
    
    public void DisplayGridbySection()
    {
        
            sql = "select ROW_NUMBER() OVER (ORDER BY sm.Id ASC) AS SrNo, sm.Id,";
            sql = sql + " cm.ClassName as ClassName,CoscholasticName,Medium,MaxMarks,CoscholasticCode,ModeOfPaper,sm.Remark ";
            sql = sql + " from CoscholasticMaster sm inner join ClassMaster cm on cm.Id=sm.ClassId and sm.SessionName=cm.SessionName and sm.BranchCode=cm.BranchCode ";
            sql = sql + " where sm.SessionName='" + Session["SessionName"].ToString() + "' and sm.BranchCode=" + Session["BranchCode"].ToString() + " and sm.ClassID='" + drpclass.SelectedValue.ToString() + "'";
            Grd.DataSource = oo.GridFill(sql);
            Grd.DataBind();
       
    }
    protected void TextBox4_TextChanged(object sender, EventArgs e)
    {

    }
    protected void TxtPanelMaxMarks_TextChanged(object sender, EventArgs e)
    {

    }
    public void PermissionGrant(int add1, int delete1, int update1, LinkButton Ladd, Button Ldelete, Button LUpdate)
    {


        if (add1 == 1)
        {
            Ladd.Enabled = true;
        }
        else
        {
            Ladd.Enabled = false;
        }


        if (delete1 == 1)
        {
            Ldelete.Enabled = true;
        }
        else
        {
            Ldelete.Enabled = false;
        }

        if (update1 == 1)
        {
            LUpdate.Enabled = true;
        }
        else
        {
            LUpdate.Enabled = false;
        }


    }
    public void CheckValueADDDeleteUpdate()
    {
        sql = " select LoginId,LoginName,Pass,SessionId,BranchId,LT.LoginTypeName,ltb.add1 as add1,ltb.delete1 as delete1,ltb.update1 as update1 from LoginTab LTb";
        sql = sql + " left join LoginType Lt on LTb.LoginTypeId=Lt.LoginTypeId where LT.LoginTypeName='" + Session["Logintype"] + "' and LTb.LoginName='" + Session["LoginName"] + "'";
        int a, u, d;
        a = Convert.ToInt32(oo.ReturnTag(sql, "add1"));
        u = Convert.ToInt32(oo.ReturnTag(sql, "update1"));
        d = Convert.ToInt32(oo.ReturnTag(sql, "delete1"));

        PermissionGrant(a, d, u, (LinkButton)LinkButton1, btnDelete, Button3);
    }
    protected void DropDownList1_SelectedIndexChanged1(object sender, EventArgs e)
    {
        loadGroup();
        DisplayGrid();

    }
    public void DisplayGrid()
    {
        sql = "select ROW_NUMBER() OVER (ORDER BY sm.Id ASC) AS SrNo, sm.Id,";
        sql = sql + " cm.ClassName as ClassName,CoscholasticName,Medium,MaxMarks,ModeOfPaper,sm.Remark ";
        sql = sql + " from CoscholasticMaster sm inner join ClassMaster cm on cm.Id=sm.ClassId  and sm.SessionName=cm.SessionName and sm.BranchCode=cm.BranchCode where ";
        sql = sql + " sm.SessionName='" + Session["SessionName"].ToString() + "'  ";
        sql = sql + " and Groupid='" + drpGroupName.SelectedValue.ToString() + "' and sm.BranchCode=" + Session["BranchCode"].ToString() + " and sm.ClassID='" + drpclass.SelectedValue.ToString() + "'";

        Grd.DataSource = oo.GridFill(sql);
        Grd.DataBind();
    }

    protected void RadioButtonList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList2.SelectedIndex == 0)
        {
            toclass.Visible = false;
        }
        else
        {
            toclass.Visible = true;
        }
    }
    protected void drpmedium_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadGroup();
        DisplayGrid();
    }
    public void loadGroup()
    {
        sql = "select CoscholasticGroup,Id from CoscholasticGroupMaster where SessionName='" + Session["SessionName"].ToString() + "'";
        sql = sql + " and Medium='" + drpmedium.SelectedItem.ToString() + "'  and BranchCode=" + Session["BranchCode"].ToString() + " and ClassID='" + drpclass.SelectedValue.ToString() + "'";
        oo.FillDropDown_withValue(sql, drpGroupName, "CoscholasticGroup", "Id");
    }
    public void loadGroup1()
    {
        sql = "select CoscholasticGroup,Id from CoscholasticGroupMaster where SessionName='" + Session["SessionName"].ToString() + "'";
        sql = sql + "  and BranchCode=" + Session["BranchCode"].ToString() + " and ClassID='" + drpclass.SelectedValue.ToString() + "'";
        oo.FillDropDown_withValue(sql, drpGroupNamePanel, "CoscholasticGroup", "Id");
    }
    protected void drpGroupName_SelectedIndexChanged(object sender, EventArgs e)
    {
        DisplayGrid();
    }
}