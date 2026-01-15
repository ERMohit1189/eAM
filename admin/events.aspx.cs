using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;

public partial class admin_events : Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        DropDownList1.Focus();

        if ( Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        //con.ConnectionString = ConfigurationManager.ConnectionStrings["Menu"].ConnectionString;
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader); 

        if (!IsPostBack)
        {

            try
            {
                CheckValueADDDeleteUpdate();
            }
            catch (Exception) { }
            
            
            
            
            
            oo.AddDateMonthYearDropDown(DropDownList1, DropDownList2, DropDownList3);
            FindCurrentDateandSetinDropDown();


            oo.AddDateMonthYearDropDown(DropDownList5, DropDownList6, DropDownList7);
            FindCurrentDateandSetinDropDownFrom();

            oo.AddDateMonthYearDropDown(DropDownList8, DropDownList9, DropDownList10);
            FindCurrentDateandSetinDropDownTo();


            oo.AddDateMonthYearDropDown(drpFromYYPanel, drpFromMMPanel, drpFromDDPanel);
            oo.AddDateMonthYearDropDown(drpToYYPanel, drpToMonthPanel, drpToddPanel);
            oo.FindCurrentDateandSetinDropDown(drpFromYYPanel, drpFromMMPanel, drpFromDDPanel);
            oo.FindCurrentDateandSetinDropDown(drpToYYPanel, drpToMonthPanel, drpToddPanel);


         sql="Select  ROW_NUMBER() OVER (ORDER BY Id desc) AS SrNo,Id,Title,EventType,Description,convert(nvarchar,Fromdate,106) as Fromdate,";
         sql = sql + "    convert(nvarchar,Todate,106) as Todate,ContactPerson from Events ";
         sql = sql + " where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
         sql=sql+" order by Id desc";
         Grd.DataSource = oo.GridFill(sql);
         Grd.DataBind();

            
        }

    }
    public void FindCurrentDateandSetinDropDown()
    {
        string dd = "", mm = "", yy = "";


        dd = oo.ReturnTag("Select day(getdate()) as DateDD", "DateDD");
        mm = oo.ReturnTag("Select Month(getdate())as MonthMM", "MonthMM");
        yy = oo.ReturnTag("Select Year(getdate()) as YearYY ", "YearYY");

        DropDownList1.Text = yy;
        if (mm == "1")
        {
            DropDownList2.Text = "Jan";
        }
        else if (mm == "2")
        {
            DropDownList2.Text = "Feb";
        }
        else if (mm == "3")
        {
            DropDownList2.Text = "Mar";
        }
        else if (mm == "4")
        {
            DropDownList2.Text = "Apr";
        }
        else if (mm == "5")
        {
            DropDownList2.Text = "May";
        }
        else if (mm == "6")
        {
            DropDownList2.Text = "Jun";

        }
        else if (mm == "7")
        {
            DropDownList2.Text = "Jul";
        }
        else if (mm == "8")
        {
            DropDownList2.Text = "Aug";
        }
        else if (mm == "9")
        {
            DropDownList2.Text = "Sep";
        }
        else if (mm == "10")
        {
            DropDownList2.Text = "Oct";
        }
        else if (mm == "11")
        {
            DropDownList2.Text = "Nov";
        }
        else if (mm == "12")
        {
            DropDownList2.Text = "Dec";
        }


        DropDownList3.Text = dd;
    }
    public void FindCurrentDateandSetinDropDownFrom()
    {
        string dd = "", mm = "", yy = "";


        dd = oo.ReturnTag("Select day(getdate()) as DateDD", "DateDD");
        mm = oo.ReturnTag("Select Month(getdate())as MonthMM", "MonthMM");
        yy = oo.ReturnTag("Select Year(getdate()) as YearYY ", "YearYY");

        DropDownList5.Text = yy;
        if (mm == "1")
        {
            DropDownList6.Text = "Jan";
        }
        else if (mm == "2")
        {
            DropDownList6.Text = "Feb";
        }
        else if (mm == "3")
        {
            DropDownList6.Text = "Mar";
        }
        else if (mm == "4")
        {
            DropDownList6.Text = "Apr";
        }
        else if (mm == "5")
        {
            DropDownList6.Text = "May";
        }
        else if (mm == "6")
        {
            DropDownList6.Text = "Jun";

        }
        else if (mm == "7")
        {
            DropDownList6.Text = "Jul";
        }
        else if (mm == "8")
        {
            DropDownList6.Text = "Aug";
        }
        else if (mm == "9")
        {
            DropDownList6.Text = "Sep";
        }
        else if (mm == "10")
        {
            DropDownList6.Text = "Oct";
        }
        else if (mm == "11")
        {
            DropDownList6.Text = "Nov";
        }
        else if (mm == "12")
        {
            DropDownList6.Text = "Dec";
        }


        DropDownList7.Text = dd;
    }
    public void FindCurrentDateandSetinDropDownTo()
    {
        string dd = "", mm = "", yy = "";


        dd = oo.ReturnTag("Select day(getdate()) as DateDD", "DateDD");
        mm = oo.ReturnTag("Select Month(getdate())as MonthMM", "MonthMM");
        yy = oo.ReturnTag("Select Year(getdate()) as YearYY ", "YearYY");

        DropDownList8.Text = yy;
        if (mm == "1")
        {
            DropDownList9.Text = "Jan";
        }
        else if (mm == "2")
        {
            DropDownList9.Text = "Feb";
        }
        else if (mm == "3")
        {
            DropDownList9.Text = "Mar";
        }
        else if (mm == "4")
        {
            DropDownList9.Text = "Apr";
        }
        else if (mm == "5")
        {
            DropDownList9.Text = "May";
        }
        else if (mm == "6")
        {
            DropDownList9.Text = "Jun";

        }
        else if (mm == "7")
        {
            DropDownList9.Text = "Jul";
        }
        else if (mm == "8")
        {
            DropDownList9.Text = "Aug";
        }
        else if (mm == "9")
        {
            DropDownList9.Text = "Sep";
        }
        else if (mm == "10")
        {
            DropDownList9.Text = "Oct";
        }
        else if (mm == "11")
        {
            DropDownList9.Text = "Nov";
        }
        else if (mm == "12")
        {
            DropDownList9.Text = "Dec";
        }


        DropDownList10.Text = dd;
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
       
        sql = "Select  ROW_NUMBER() OVER (ORDER BY Id desc) AS SrNo,Id,Title,EventType,Description,convert(nvarchar,Fromdate,106) as Fromdate,";
        sql = sql + "    convert(nvarchar,Todate,106) as Todate,ContactPerson from Events ";
        sql = sql + "   where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        sql = sql + "   and Title='" + TextBox1.Text + "'";

        if (oo.Duplicate(sql))
        {
            //oo.MessageBox("Duplicate Title!", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Duplicate title!", "A"); 

        }
        else
        {
            for (int i = 0; i < CheckBoxList1.Items.Count; i++)
            {
                if (CheckBoxList1.Items[i].Selected)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "EventsProc";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;

                    String fromDate = "";
                    fromDate = DropDownList1.SelectedItem.ToString() + "/" + DropDownList2.SelectedItem.ToString() + "/" + DropDownList3.SelectedItem.ToString();
                    cmd.Parameters.AddWithValue("@Date", fromDate);

                    cmd.Parameters.AddWithValue("@Title", TextBox1.Text.ToString());
                    cmd.Parameters.AddWithValue("@EventType", DropDownList4.Text.ToString());
                    cmd.Parameters.AddWithValue("@Description", TextBox2.Text.ToString());


                    String fromDate1 = "";
                    fromDate1 = DropDownList5.SelectedItem.ToString() + "/" + DropDownList6.SelectedItem.ToString() + "/" + DropDownList7.SelectedItem.ToString();


                    cmd.Parameters.AddWithValue("@Fromdate", fromDate1);

                    String fromDate11 = "";
                    fromDate11 = DropDownList8.SelectedItem.ToString() + "/" + DropDownList9.SelectedItem.ToString() + "/" + DropDownList10.SelectedItem.ToString();

                    cmd.Parameters.AddWithValue("@Todate", fromDate11);
                    cmd.Parameters.AddWithValue("@ContactPerson", TextBox3.Text.ToString());

                    cmd.Parameters.AddWithValue("@ContactNo1", TextBox4.Text.ToString());
                    cmd.Parameters.AddWithValue("@ContactNo2", TextBox5.Text.ToString());


                    cmd.Parameters.AddWithValue("@EMail", TextBox6.Text.ToString());
                    cmd.Parameters.AddWithValue("@Fax", TextBox7.Text.ToString());
                    cmd.Parameters.AddWithValue("@Url", TextBox8.Text.ToString());




                    cmd.Parameters.AddWithValue("@AdminStuTeacher", CheckBoxList1.Items[i].Text);

                    cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());//Session["Login"].ToString());
                    cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                    cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
                    //cmd.Parameters.AddWithValue("@AboutInstitution", ss1);
                    try
                    {

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                       
                    }
                    catch (SqlException) { }
                }
              
            }
            //oo.MessageBox("Submitted successfully.", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");

            oo.ClearControls(this.Page);
            sql = "Select  ROW_NUMBER() OVER (ORDER BY Id desc) AS SrNo,Id,Title,EventType,Description,convert(nvarchar,Fromdate,106) as Fromdate,";
            sql = sql + "    convert(nvarchar,Todate,106) as Todate,ContactPerson from Events ";
            sql = sql + " where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            sql = sql + " order by Id desc";
            Grd.DataSource = oo.GridFill(sql);
            Grd.DataBind();
            CheckBoxList1.Items[0].Selected = false;
            CheckBoxList1.Items[1].Selected = false;
            CheckBoxList1.Items[2].Selected = false;
          
         


        }


    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        String fromDate = "",ToDate;
        fromDate = drpFromYYPanel.SelectedItem.ToString() + "/" + drpFromMMPanel.SelectedItem.ToString() + "/" + drpFromDDPanel.SelectedItem.ToString();        
        ToDate  = drpToYYPanel.SelectedItem.ToString() + "/" + drpToMonthPanel.SelectedItem.ToString() + "/" + drpToddPanel.SelectedItem.ToString();
        
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "EventsUpdateProc";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Id", lblID.Text);
        cmd.Parameters.AddWithValue("@Title", txtTitleNamePanel.Text.ToString());
        cmd.Parameters.AddWithValue("@EventType", drpEventPanel.Text.ToString());
        cmd.Parameters.AddWithValue("@Description",txtDescPanel.Text);
        cmd.Parameters.AddWithValue("@Fromdate",fromDate);
        cmd.Parameters.AddWithValue("@Todate",ToDate);
        cmd.Parameters.AddWithValue("@contactPerson",txtContactPerson1Panel.Text);
        cmd.Parameters.AddWithValue("@Contact1",txtContactNo1Panel.Text );
        cmd.Parameters.AddWithValue("@Contact2",txtContactNo2Panel.Text );
     
        cmd.Connection = con;
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            //oo.MessageBox("Updated successfully.", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully.", "S");
            sql = "Select  ROW_NUMBER() OVER (ORDER BY Id desc) AS SrNo,Id,Title,EventType,Description,convert(nvarchar,Fromdate,106) as Fromdate,";
            sql = sql + "    convert(nvarchar,Todate,106) as Todate,ContactPerson from Events ";
            sql = sql + " where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            sql=sql+" order by Id desc";
            Grd.DataSource = oo.GridFill(sql);
            Grd.DataBind();
        }
        catch (SqlException) { }

    }
    protected void Button4_Click(object sender, EventArgs e)
    {

    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        sql = "Delete from Events where Id=" + lblvalue.Text;
        sql = sql + " and  SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

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
            sql = "Select  ROW_NUMBER() OVER (ORDER BY Id desc) AS SrNo,Id,Title,EventType,Description,convert(nvarchar,Fromdate,106) as Fromdate,";
            sql = sql + "    convert(nvarchar,Todate,106) as Todate,ContactPerson from Events ";
            sql = sql + " where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            sql=sql+" order by Id desc";
            Grd.DataSource = oo.GridFill(sql);
            Grd.DataBind();
        }
        catch (SqlException) { }
    }
    protected void Button8_Click(object sender, EventArgs e)
    {

    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {

        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("Label36");
        string ss = lblId.Text;
        lblID.Text = ss;

        // sql = "Select  distinct ROW_NUMBER() OVER (ORDER BY p.ProductId ASC) AS  [ProductId] ,po.id as ID, pc.ProductCategoryName as[ProductCategoryName], p.ProductName as [ProductName], Pm.ProductTypeName as [ProductTypeName],PO.ProductModelName as [ProductModelName] from Productcategorymaster pc left join ProductName p on p.ProductId=pc.ProductId  left join ProductTypeMaster PM on p.ProductId=PM.ProductId left join ProductModelMaster PO on p.ProductId=PO.ProductId ";        
        sql = "Select  ROW_NUMBER() OVER (ORDER BY Id desc) AS SrNo,Id,Title,EventType,Description,convert(nvarchar,Fromdate,106) as Fromdate,ContactNo1,ContactNo2,";
        sql = sql + " left(convert(nvarchar,FromDate,106),2) as DD,Right(left(convert(nvarchar,FromDate,106),6),3) as MM ,  RIGHT(convert(nvarchar,FromDate,106),4) as YY ,";
        sql = sql + "  left(convert(nvarchar,Todate,106),2) as DDT,Right(left(convert(nvarchar,Todate,106),6),3) as MMT ,  RIGHT(convert(nvarchar,Todate,106),4) as YYT ,";
        sql = sql + "    convert(nvarchar,Todate,106) as Todate,ContactPerson from Events ";
        sql = sql + "  where Id=" + ss;
        sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

        txtDescPanel.Text = oo.ReturnTag(sql, "Description");
        txtTitleNamePanel.Text = oo.ReturnTag(sql, "Title");
        txtContactPerson1Panel.Text = oo.ReturnTag(sql, "ContactPerson");
        txtContactNo1Panel.Text = oo.ReturnTag(sql, "ContactNo1");
        txtContactNo2Panel.Text = oo.ReturnTag(sql, "ContactNo2");
        drpEventPanel.Text = oo.ReturnTag(sql, "EventType");
        
        drpFromYYPanel.Text=oo.ReturnTag(sql,"YY");
        drpFromMMPanel.Text = oo.ReturnTag(sql, "MM");
        drpFromDDPanel.Text = oo.ReturnTag(sql, "DD");
        
        drpToYYPanel.Text = oo.ReturnTag(sql, "YYT");
        drpToMonthPanel.Text = oo.ReturnTag(sql, "MMT");
        drpToddPanel.Text = oo.ReturnTag(sql, "DDT");

       
        

        Panel1_ModalPopupExtender.Show();
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


}