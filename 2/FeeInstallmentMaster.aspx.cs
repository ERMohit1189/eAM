using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;

public partial class FeeInstallmentMaster : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";
#pragma warning disable 169
    private object Green;
#pragma warning restore 169
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
            drpCardType.Focus();
            //try
            //{
            //    CheckValueADDDeleteUpdate();
            //}
            //catch (Exception) { }

            sql = "Select FeeGroupName,Id from FeeGroupMaster";
            sql +=  "  where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

            oo.FillDropDown_withValue(sql, drpCardType, "FeeGroupName", "Id");
            drpCardType.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
            oo.FillDropDown_withValue(sql, drpCardTypePanel, "FeeGroupName", "Id");
            drpCardTypePanel.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));

            oo.AddDateMonthYearDropDown(drpyear, drpmonth, drpdate);
            oo.FindCurrentDateandSetinDropDown(drpyear, drpmonth, drpdate);

            //oo.AddDateMonthYearDropDown(drpdueyear, drpduemonth, drpduedate);
            //oo.FindCurrentDateandSetinDropDown(drpdueyear, drpduemonth, drpduedate);

            oo.AddDateMonthYearDropDown(drpyearPanel, drpmonthPanel, drpdatePanel);
            oo.FindCurrentDateandSetinDropDown(drpyearPanel, drpmonthPanel, drpdatePanel);

            oo.AddDateMonthYearDropDown(drpdueyearPanel, drpduemonthPanel, drpduedatePanel);
            oo.FindCurrentDateandSetinDropDown(drpdueyearPanel, drpduemonthPanel, drpduedatePanel);

            loadGrid();
            loadClass();
        }

    }
    private void loadClass()
    {
        sql = "Select Id,ClassName from ClassMaster where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " order by Cidorder";
        oo.FillDropDown_withValue(sql, drpClass, "ClassName", "Id");
        oo.FillDropDown_withValue(sql, drpClassPanel, "ClassName", "Id");
        drpClass.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
        drpClass.Items.Insert(1, new ListItem("All", "All"));
        drpClassPanel.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
    }

    protected void LinkButtob1_Click(object sender, EventArgs e)
    {
        int sts = 0;
        if (drpCardType.SelectedItem.ToString() == "<--Select-->")
        {
            //oo.MessageBox("Please <--Select--> Condition", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please <--Select--> Condition", "A");

            Grd.Visible = false;
        }

        else
        {
            if (drpClass.SelectedValue == "All")
            {
                for (int i = 2; i < drpClass.Items.Count; i++)
                {
                    if (oo.Duplicate("Select CardType from MonthMaster where CardType = '" + drpCardType.SelectedValue.ToString().Trim()+"' and MonthName = '"+txtMonth.Text.Trim().ToString()+"' and ClassId = "+drpClass.Items[i].Value.ToString().Trim()+" and SessionName = '"+Session["SessionName"]+"' and BranchCode = "+ Session["BranchCode"]+""))
                    {
                        sts = sts + 1;
                    }
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "MonthMasterProc";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;

                    cmd.Parameters.AddWithValue("@CardType", drpCardType.SelectedValue.ToString().Trim());
                    cmd.Parameters.AddWithValue("@MOD", drpMOD.SelectedValue.ToString().Trim());
                    cmd.Parameters.AddWithValue("@ClassId", drpClass.Items[i].Value.ToString().Trim());
                    cmd.Parameters.AddWithValue("@MonthName", txtMonth.Text.Trim().ToString());
                    int value;
                    string formonth = txtForMonths.Text != "" ? int.TryParse(txtForMonths.Text, out value) ? txtForMonths.Text : "1" : "1";
                    cmd.Parameters.AddWithValue("@ForMonth", formonth);
                    cmd.Parameters.AddWithValue("@MonthRemark", txtremark.Text.ToString());
                    cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                    cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                    cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
                    string date = drpyear.SelectedItem.Text + "/" + drpmonth.SelectedItem.Text + "/" + drpdate.SelectedItem.Text;
                    cmd.Parameters.AddWithValue("@DueDate", date);
                    //string startingdate = drpdueyear.SelectedItem.Text + "/" + drpduemonth.SelectedItem.Text + "/" + drpduedate.SelectedItem.Text;
                    cmd.Parameters.AddWithValue("@StartingDate", date);

                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        cmd.Parameters.Clear();
                    }
                    catch (Exception ex)
                    {
                        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, ex.Message, "A");
                    }
                }
                oo.ClearControls(this.Page);
                txtForMonths.Text = "1";
                loadGrid();
                txtMonth.Focus();
            }
            else
            {
                if (oo.Duplicate("Select CardType from MonthMaster where CardType = '" + drpCardType.SelectedValue.ToString().Trim() + "' and MonthName = '" + txtMonth.Text.Trim().ToString() + "' and ClassId = " + drpClass.SelectedValue.ToString().Trim() + " and SessionName = '" + Session["SessionName"] + "' and BranchCode = " + Session["BranchCode"] + ""))
                {
                    sts = sts + 1;
                }
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "MonthMasterProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                cmd.Parameters.AddWithValue("@CardType", drpCardType.SelectedValue.ToString().Trim());
                cmd.Parameters.AddWithValue("@MOD", drpMOD.SelectedValue.ToString().Trim());
                cmd.Parameters.AddWithValue("@ClassId", drpClass.SelectedValue.ToString().Trim());
                cmd.Parameters.AddWithValue("@MonthName", txtMonth.Text.Trim().ToString());
                int value;
                string formonth = txtForMonths.Text != "" ? int.TryParse(txtForMonths.Text, out value) ? txtForMonths.Text : "1" : "1";
                cmd.Parameters.AddWithValue("@ForMonth", formonth);
                cmd.Parameters.AddWithValue("@MonthRemark", txtremark.Text.ToString());
                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
                string date = drpyear.SelectedItem.Text + "/" + drpmonth.SelectedItem.Text + "/" + drpdate.SelectedItem.Text;
                cmd.Parameters.AddWithValue("@DueDate", date);
                //string startingdate = drpdueyear.SelectedItem.Text + "/" + drpduemonth.SelectedItem.Text + "/" + drpduedate.SelectedItem.Text;
                cmd.Parameters.AddWithValue("@StartingDate", date);

                try
                {

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    cmd.Parameters.Clear();
                }
                catch (Exception ex)
                {
                    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, ex.Message, "A");
                }
            }
            if (sts>0)
            {
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Duplicate entry!", "A");
            }
            else
            {
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");
            }
            oo.ClearControls(this.Page);
            txtForMonths.Text = "1";
            loadGrid();
            txtMonth.Focus();
        }
    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        Button8.Focus();
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("Label37");
        string ss = lblId.Text;
        lblvalue.Text = ss.ToString();
        Panel2_ModalPopupExtender.Show();

    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {

        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("Label36");
        string ss = lblId.Text;
        lblID.Text = ss.ToString();

        sql = "Select ROW_NUMBER() OVER (ORDER BY MonthId ASC) AS SrNo,MOD,ClassId,ISNULL(typeofAdd,'-1') typeofAdd,CardType,MonthId, MonthName,ForMonth,MonthRemark,cardtype,";
        sql +=  " Datepart(Year,DueDate) as Year,Substring(DateName(MONTH,DueDate),1,3) as Month,Datepart(DAY,DueDate) as Day,";
        sql += " Datepart(Year,StartingDate) as SYear,Substring(DateName(MONTH,StartingDate),1,3) as SMonth,Datepart(DAY,StartingDate) as SDay,DueDate";
        sql +=  " from MonthMaster where monthid<>0 and MonthId=" + ss;
        sql +=  " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and (CardType='" + drpCardType.SelectedItem.ToString() + "' or  CardType='" + drpCardType.SelectedValue.ToString() + "')";

        int value;
        txtMonthPanel.Text = oo.ReturnTag(sql, "MonthName");
        txtRemarkPanel.Text = oo.ReturnTag(sql, "MonthRemark");
        if (oo.ReturnTag(sql, "ForMonth") != "")
        {
            txtForMonthsPanel.Text = oo.ReturnTag(sql, "ForMonth");
        }
        else
        {
            txtForMonthsPanel.Text = "1";
        }
        try
        {
            if (int.TryParse(oo.ReturnTag(sql, "CardType"), out value))
            {
                drpCardTypePanel.SelectedValue = oo.ReturnTag(sql, "CardType");
            }
            else
            {
                drpCardTypePanel.SelectedIndex = 1;
            }
        }
        catch
        {
        }
        drpMODPanel.SelectedValue = oo.ReturnTag(sql, "MOD") != "" ? oo.ReturnTag(sql, "MOD") : "I";

        if (oo.ReturnTag(sql, "ClassId") != "")
        {
            drpClassPanel.SelectedValue = oo.ReturnTag(sql, "ClassId");
        }
        drptypeofAddPanel.SelectedValue = oo.ReturnTag(sql, "typeofAdd");

        try
        {
            //if (oo.ReturnTag(sql, "SYear") != "" && oo.ReturnTag(sql, "SMonth") != "" && oo.ReturnTag(sql, "Sday") != "")
            //{
            //    drpdueyearPanel.SelectedValue = drpdueyearPanel.Items.FindByText(oo.ReturnTag(sql, "SYear")).Value;
            //    drpduemonthPanel.SelectedValue = drpduemonthPanel.Items.FindByText(oo.ReturnTag(sql, "SMonth")).Value;
            //    drpduedatePanel.SelectedValue = drpduedatePanel.Items.FindByText(oo.ReadDD(oo.ReturnTag(sql, "Sday"))).Value;
            //}
            //else
            //{
            //    drpdueyearPanel.SelectedIndex = 0;
            //    drpduemonthPanel.SelectedIndex = 0;
            //    drpduedatePanel.SelectedIndex = 0;
            //}
        }
        catch
        {

        }

        try
        {

            oo.AddDateMonthYearDropDownBasedOfDay(drpyearPanel, drpmonthPanel, drpdatePanel, oo.ReturnTag(sql, "DueDate"));
            if (oo.ReturnTag(sql, "Year") != "" && oo.ReturnTag(sql, "Month") != "" && oo.ReturnTag(sql, "day") != "")
            {
                drpyearPanel.SelectedValue = drpyearPanel.Items.FindByText(oo.ReturnTag(sql, "Year")).Value;
                drpmonthPanel.SelectedValue = drpmonthPanel.Items.FindByText(oo.ReturnTag(sql, "Month")).Value;
                drpdatePanel.SelectedValue = drpdatePanel.Items.FindByText(oo.ReadDD(oo.ReturnTag(sql, "day"))).Value;
            }
            else
            {
                drpyearPanel.SelectedIndex = 0;
                drpmonthPanel.SelectedIndex = 0;
                drpdatePanel.SelectedIndex = 0;
            }
        }
        catch
        {

        }




        Panel1_ModalPopupExtender.Show();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "MonthMasterUpdateProc";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@monthId", lblID.Text);
        cmd.Parameters.AddWithValue("@CardType", drpCardTypePanel.SelectedValue.ToString().Trim());
        cmd.Parameters.AddWithValue("@MOD", drpMODPanel.SelectedValue.ToString().Trim());
        cmd.Parameters.AddWithValue("@ClassId", drpClassPanel.SelectedValue.ToString().Trim());
        int value;
        string formonth = txtForMonthsPanel.Text != "" ? int.TryParse(txtForMonthsPanel.Text, out value) ? txtForMonthsPanel.Text : "1" : "1";
        cmd.Parameters.AddWithValue("@ForMonth", formonth);
        cmd.Parameters.AddWithValue("@MonthName", txtMonthPanel.Text.Trim().ToString());
        cmd.Parameters.AddWithValue("@MonthRemark", txtRemarkPanel.Text.ToString());
        string date = drpyearPanel.SelectedItem.Text + "/" + drpmonthPanel.SelectedItem.Text + "/" + drpdatePanel.SelectedItem.Text;
        cmd.Parameters.AddWithValue("@DueDate", date);
        string startingdate = drpdueyearPanel.SelectedItem.Text + "/" + drpduemonthPanel.SelectedItem.Text + "/" + drpduedatePanel.SelectedItem.Text;
        cmd.Parameters.AddWithValue("@StartingDate", date);


        cmd.Connection = con;
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            //oo.MessageBox("Updated successfully.", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully.", "S");

            loadGrid();
            txtMonth.Focus();
        }
        catch (SqlException) { }

    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        sql = "Delete from MonthMaster where MonthId=" + lblvalue.Text;
        sql +=  " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

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
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Deleted successfully.", "S");

            loadGrid();
            txtMonth.Focus();
        }
        catch (SqlException) { }

    }
    protected void drpCardType_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadGrid();
        drpCardType.Focus();
    }

    private void loadGrid()
    {

        sql = "Select cm.id classid,  MonthId,ISNULL(typeofAdd,'') typeofAdd, MonthName,ISNULL(ForMonth,1) as ForMonth,MonthRemark,cardtype,Convert(varchar(11),StartingDate,106) as StartingDate,Convert(varchar(11),DueDate,106) as DueDate,cm.ClassName,cm.Id classid,mm.MOD from MonthMaster mm";
        sql +=  " inner join FeeGroupMaster fm on fm.FeeGroupName=mm.CardType";
        sql +=  " inner join ClassMaster cm on cm.Id=mm.ClassId and cm.SessionName=mm.SessionName";
        sql +=  " where CardType in('" + drpCardType.SelectedItem.Text.ToString() + "') and fm.SessionName='" + Session["SessionName"].ToString() + "'  and (MOD='" + drpMOD.SelectedValue.ToString() + "' or MOD is null) and mm.SessionName='" + Session["SessionName"].ToString() + "' and mm.BranchCode=" + Session["BranchCode"].ToString() + " and cm.BranchCode=" + Session["BranchCode"].ToString() + "  and fm.BranchCode=" + Session["BranchCode"].ToString() + "";
        if (drpClass.SelectedIndex != 0 && drpClass.SelectedIndex != 1)
        {
            sql +=  " and cm.Id='" + drpClass.SelectedValue.ToString() + "'";
        }
        sql +=  " Union";
        sql +=  " Select cm.id classid,  MonthId,ISNULL(typeofAdd,'') typeofAdd, MonthName,ISNULL(ForMonth,1) as ForMonth,MonthRemark,fm.FeeGroupName as cardtype,Convert(varchar(11),StartingDate,106) as StartingDate,Convert(varchar(11),DueDate,106) as DueDate,cm.ClassName,cm.Id classid,mm.MOD from MonthMaster mm";
        sql +=  " inner join FeeGroupMaster fm on fm.Id=mm.CardType";
        sql +=  " inner join ClassMaster cm on cm.Id=mm.ClassId and cm.SessionName=mm.SessionName";
        sql +=  " where CardType in('" + drpCardType.SelectedValue.ToString() + "')  and MOD='" + drpMOD.SelectedValue.ToString() + "' and fm.SessionName='" + Session["SessionName"].ToString() + "' and mm.SessionName='" + Session["SessionName"].ToString() + "' and mm.BranchCode=" + Session["BranchCode"].ToString() + " and cm.BranchCode=" + Session["BranchCode"].ToString() + "  and fm.BranchCode=" + Session["BranchCode"].ToString() + "";
        if (drpClass.SelectedIndex != 0 && drpClass.SelectedIndex != 1)
        {
            sql +=  " and cm.Id='" + drpClass.SelectedValue.ToString() + "'";
        }
        sql +=  " order by MonthId";

        Grd.DataSource = oo.GridFill(sql);
        Grd.DataBind();
        Grd.Visible = true;

        if (Grd.Rows.Count == 0)
        {
            //oo.MessageBox("Sorry, No Record(s) found!", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, No Record(s) found!", "A");

            Grd.Visible = false;
        }
        else
        {
            for (int i = 0; i < Grd.Rows.Count; i++)
            {
                Label Label37 = (Label)Grd.Rows[i].FindControl("Label37");
                Label classid = (Label)Grd.Rows[i].FindControl("classid");
                LinkButton edit = (LinkButton)Grd.Rows[i].FindControl("LinkButton2");
                LinkButton delete = (LinkButton)Grd.Rows[i].FindControl("LinkButton3");

                sql = "select count(*) cnt from FeeAllotedForClassWise where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and ClassId=" + classid.Text.Trim().ToString() + " and cardtype=" + drpCardType.SelectedValue + " and MonthId="+ Label37.Text + "";
                if (oo.ReturnTag(sql, "cnt") != "0")
                {
                    edit.Enabled = false;
                    delete.Enabled = false;
                    edit.Text = "<i class='fa fa-lock'></i>";
                    delete.Text = "<i class='fa fa-lock'></i>";
                }
            }
        }
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
        sql +=  " left join LoginType Lt on LTb.LoginTypeId=Lt.LoginTypeId where LT.LoginTypeName='" + Session["Logintype"] + "' and LTb.LoginName='" + Session["LoginName"] + "'";
        int a, u, d;
        a = Convert.ToInt32(oo.ReturnTag(sql, "add1"));
        u = Convert.ToInt32(oo.ReturnTag(sql, "update1"));
        d = Convert.ToInt32(oo.ReturnTag(sql, "delete1"));

        PermissionGrant(a, d, u, (LinkButton)LinkButtob1, btnDelete, Button3);
    }

    protected void drpyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.YearDropDown(drpyear, drpmonth, drpdate);
        drpyear.Focus();
    }
    protected void drpmonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.MonthDropDown(drpyear, drpmonth, drpdate);
        drpmonth.Focus();
    }

    protected void drpdueyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        //oo.YearDropDown(drpdueyear, drpduemonth, drpduedate);
        //drpdueyear.Focus();
    }
    protected void drpduemonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        //oo.MonthDropDown(drpdueyear, drpduemonth, drpduedate);
        //drpduemonth.Focus();
    }

    protected void drpdueyearPanel_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.YearDropDown(drpdueyearPanel, drpduemonthPanel, drpduedatePanel);
        Panel1_ModalPopupExtender.Show();
    }
    protected void drpduemonthPanel_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.MonthDropDown(drpdueyearPanel, drpduemonthPanel, drpduedatePanel);
        Panel1_ModalPopupExtender.Show();
    }

    protected void drpyearPanel_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.YearDropDown(drpyearPanel, drpmonthPanel, drpdatePanel);
        Panel1_ModalPopupExtender.Show();
    }
    protected void drpmonthPanel_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.MonthDropDown(drpyearPanel, drpmonthPanel, drpdatePanel);
        Panel1_ModalPopupExtender.Show();
    }
    protected void drpClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadGrid();
        drpClass.Focus();
    }
    protected void drpMOD_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadGrid();
    }
}