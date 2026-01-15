using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;

public partial class News : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = ""; string stu = "", gur = "", staf = "", Adm = "", SupAdm = "";

    protected void Page_PreInIt(object sender, EventArgs e)
    {
        if (Session["Logintype"] == null) { Response.Redirect("~/default.aspx"); }
        if (Session["Logintype"].ToString() == "Admin")
        {
            this.MasterPageFile = "~/Master/admin_root-manager.master";
        }
        if (Session["Logintype"].ToString() == "Staff")
        {
            this.MasterPageFile = "~/staff/staff_root-manager.master";
        }
        if (Session["Logintype"].ToString() == "SuperAdmin")
        {
            MasterPageFile = "~/50/sadminRootManager.master";
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LoginName"] == null)
        {
            Response.Redirect("default.aspx");
        }
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader); 

        if (!IsPostBack)
        {
            string sql = "Select BranchId, BranchName from Branchtab";
            var dt = oo.Fetchdata(sql);
            ddlBranch.DataSource = dt;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "BranchId";
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, new ListItem("<--Select-->", ""));
            if (Session["LoginType"].ToString() == "Admin")
            {
                divBranch.Visible = false;
                divSession.Visible = false;
                ddlBranch.SelectedValue = Session["BranchCode"].ToString();

                string sqls = "select SessionName from SessionMaster where BranchCode=" + ddlBranch.SelectedValue + "";
                var dt2 = oo.Fetchdata(sqls);
                DrpSessionName.DataSource = dt2;
                DrpSessionName.DataTextField = "SessionName";
                DrpSessionName.DataValueField = "SessionName";
                DrpSessionName.DataBind();
                DrpSessionName.Items.Insert(0, new ListItem("<--Select Session-->", ""));
                DrpSessionName.SelectedIndex = (DrpSessionName.Items.Count - 1);
                if (Session["LoginType"].ToString() == "Admin")
                {
                    DrpSessionName.SelectedValue = Session["SessionName"].ToString();
                }
            }

            DrpSessionName.Items.Insert(0, new ListItem("<--Select-->", ""));


            oo.AddDateMonthYearDropDown(DrpYear, DrpMonth, DrpDate);
            FindCurrentDateandSetinDropDown();
            oo.AddDateMonthYearDropDown(DrpYear1, DrpMonth1, DrpDate1);
            FindCurrentDateandSetinDropDown1();

            oo.AddDateMonthYearDropDown(drpYYPanelFrom, drpMMPanelFrom, drpDDPanelFrom);
            oo.AddDateMonthYearDropDown(drpYYTo, DrpMMToPanel, DrpDDToPanel);

            loadNewReocrd();

            txtttle.Focus();

            if (Session["Logintype"].ToString() == "Staff")
            {
                CheckBoxList1.Items.RemoveAt(0);
            }
            loadNewReocrd();
        }
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBranch.SelectedIndex == 0)
        {
            DrpSessionName.Items.Clear();
            DrpSessionName.Items.Insert(0, new ListItem("<--Select Session-->", ""));
            return;
        }
        string sql = "select SessionName from SessionMaster where BranchCode=" + ddlBranch.SelectedValue + "";
        var dt2 = oo.Fetchdata(sql);
        DrpSessionName.DataSource = dt2;
        DrpSessionName.DataTextField = "SessionName";
        DrpSessionName.DataValueField = "SessionName";
        DrpSessionName.DataBind();
        DrpSessionName.Items.Insert(0, new ListItem("<--Select Session-->", ""));
        DrpSessionName.SelectedIndex = (DrpSessionName.Items.Count - 1);
        if (Session["LoginType"].ToString() == "Admin")
        {
            DrpSessionName.SelectedValue = Session["SessionName"].ToString();
        }
        loadNewReocrd();
    }
    public void loadNewReocrd()
    {       
        sql = "Select  ROW_NUMBER() OVER (ORDER BY NewsId desc) AS SrNo,NewsId,Title,Description,convert(nvarchar,FromDate,106) as FromDate ,";
        sql +=  " convert(nvarchar,ToDate,106) as ToDate from NewsDescription ";
        sql +=  " where SessionName='" + DrpSessionName.SelectedValue.ToString() + "' and BranchCode=" + ddlBranch.SelectedValue.ToString() + "";
        sql +=  " order by NewsId desc";
        Grd.DataSource = oo.GridFill(sql);
        Grd.DataBind();
    }
    protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Grd.PageIndex = e.NewPageIndex;
        loadNewReocrd();
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        string stu = "", gur = "", staf = "";

        sql = "Select  ROW_NUMBER() OVER (ORDER BY NewsId desc) AS SrNo,NewsId,Title,Description,convert(nvarchar,FromDate,106) as FromDate ,convert(nvarchar,ToDate,106) as ToDate from NewsDescription ";
        sql +=  "  where SessionName='" + DrpSessionName.SelectedValue.ToString() + "' and BranchCode=" + ddlBranch.SelectedValue.ToString() + "";
        sql +=  "   and Title='" + txtttle.Text + "'";
       
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "NewsDescriptionProc";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;


        cmd.Parameters.AddWithValue("@Title", txtttle.Text.ToString());
        cmd.Parameters.AddWithValue("@Description", txtdes.Text.ToString());
        String Date = "";
        Date = DrpYear.SelectedItem.ToString() + "/" + DrpMonth.SelectedItem.ToString() + "/" + DrpDate.SelectedItem.ToString();


        cmd.Parameters.AddWithValue("@FromDate", Date);

        String Date1 = "";
        Date1 = DrpYear1.SelectedItem.ToString() + "/" + DrpMonth1.SelectedItem.ToString() + "/" + DrpDate1.SelectedItem.ToString();
        cmd.Parameters.AddWithValue("@ToDate", Date1);

        cmd.Parameters.AddWithValue("@Category", "");
        cmd.Parameters.AddWithValue("@SessionName", DrpSessionName.SelectedValue.ToString());
        cmd.Parameters.AddWithValue("@BranchCode", ddlBranch.SelectedValue.ToString());
        cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
        int ss = CheckBoxList1.Items.Count;
        if(ss == 4)
        {
            if (CheckBoxList1.Items[0].Selected == true)
            {
                Adm = "Show";
            }
            else
            {
                Adm = "Hide";
            }
            if (CheckBoxList1.Items[1].Selected == true)
            {
                staf = "Show";
            }
            else
            {
                staf = "Hide";
            }
            if (CheckBoxList1.Items[2].Selected == true)
            {
                gur = "Show";
            }
            else
            {
                gur = "Hide";
            }
            if (CheckBoxList1.Items[3].Selected == true)
            {
                stu = "Show";
            }
            else
            {
                stu = "Hide";
            }
        }
      else if (ss == 3)
        {
            if (CheckBoxList1.Items[0].Selected == true)
            {
                staf = "Show";
            }
            else
            {
                staf = "Hide";
            }
            if (CheckBoxList1.Items[1].Selected == true)
            {
                gur = "Show";
            }
            else
            {
                gur = "Hide";
            }
            if (CheckBoxList1.Items[2].Selected == true)
            {
                stu = "Show";
            }
            else
            {
                stu = "Hide";
            }
        }


        SupAdm = "Hide";

        cmd.Parameters.AddWithValue("@Administrator", "");
        cmd.Parameters.AddWithValue("@SuperAdmin", SupAdm);
        cmd.Parameters.AddWithValue("@Admin", Adm);
        cmd.Parameters.AddWithValue("@Student", stu);
        cmd.Parameters.AddWithValue("@Staff", staf);
        cmd.Parameters.AddWithValue("@Guardian", gur);
        cmd.Parameters.AddWithValue("@LoginType", Session["Logintype"].ToString());
        cmd.Connection = con;

        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            //oo.MessageBox("Successfully Submitted", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");                                    
            oo.ClearControls(this.Page);
            loadNewReocrd();
            //CheckBoxList1.Items[0].Selected = false;
            //CheckBoxList1.Items[1].Selected = false;
            //CheckBoxList1.Items[2].Selected = false;
            //CheckBoxList1.Items[3].Selected = false;
            CheckBoxList1.ClearSelection();
            txtttle.Focus();
        }
        catch (SqlException ee) { oo.MessageBox(ee.Message.ToString(), this.Page); }
    }

    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    public void FindCurrentDateandSetinDropDown()
    {
        string dd = "", mm = "", yy = "";


        dd = oo.ReturnTag("Select day(getdate()) as DateDD", "DateDD");
        mm = oo.ReturnTag("Select Month(getdate())as MonthMM", "MonthMM");
        yy = oo.ReturnTag("Select Year(getdate()) as YearYY ", "YearYY");

        DrpYear.Text = yy;
        if (mm == "1")
        {
            DrpMonth.Text = "Jan";
        }
        else if (mm == "2")
        {
            DrpMonth.Text = "Feb";
        }
        else if (mm == "3")
        {
            DrpMonth.Text = "Mar";
        }
        else if (mm == "4")
        {
            DrpMonth.Text = "Apr";
        }
        else if (mm == "5")
        {
            DrpMonth.Text = "May";
        }
        else if (mm == "6")
        {
            DrpMonth.Text = "Jun";

        }
        else if (mm == "7")
        {
            DrpMonth.Text = "Jul";
        }
        else if (mm == "8")
        {
            DrpMonth.Text = "Aug";
        }
        else if (mm == "9")
        {
            DrpMonth.Text = "Sep";
        }
        else if (mm == "10")
        {
            DrpMonth.Text = "Oct";
        }
        else if (mm == "11")
        {
            DrpMonth.Text = "Nov";
        }
        else if (mm == "12")
        {
            DrpMonth.Text = "Dec";
        }


        DrpDate.Text = dd;
    }
    public void FindCurrentDateandSetinDropDown1()
    {
        string dd = "", mm = "", yy = "";


        dd = oo.ReturnTag("Select day(getdate()) as DateDD", "DateDD");
        mm = oo.ReturnTag("Select Month(getdate())as MonthMM", "MonthMM");
        yy = oo.ReturnTag("Select Year(getdate()) as YearYY ", "YearYY");

        DrpYear1.Text = yy;
        if (mm == "1")
        {
            DrpMonth1.Text = "Jan";
        }
        else if (mm == "2")
        {
            DrpMonth1.Text = "Feb";
        }
        else if (mm == "3")
        {
            DrpMonth1.Text = "Mar";
        }
        else if (mm == "4")
        {
            DrpMonth1.Text = "Apr";
        }
        else if (mm == "5")
        {
            DrpMonth1.Text = "May";
        }
        else if (mm == "6")
        {
            DrpMonth1.Text = "Jun";

        }
        else if (mm == "7")
        {
            DrpMonth1.Text = "Jul";
        }
        else if (mm == "8")
        {
            DrpMonth1.Text = "Aug";
        }
        else if (mm == "9")
        {
            DrpMonth1.Text = "Sep";
        }
        else if (mm == "10")
        {
            DrpMonth1.Text = "Oct";
        }
        else if (mm == "11")
        {
            DrpMonth.Text = "Nov";
        }
        else if (mm == "12")
        {
            DrpMonth1.Text = "Dec";
        }


        DrpDate1.Text = dd;
    }
    protected void DrpYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.YearDropDown(DrpYear, DrpMonth, DrpDate);
    }
    protected void DrpMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.MonthDropDown(DrpYear, DrpMonth, DrpDate);
    }
    protected void DrpYear1_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.YearDropDown(DrpYear1, DrpMonth1, DrpDate1);
    }
    protected void DrpMonth1_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.MonthDropDown(DrpYear1, DrpMonth1, DrpDate1);

    }
    protected void DrpDate1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "NewsDescriptionUpdateProc";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
        cmd.Parameters.AddWithValue("@id", lblID.Text);
        cmd.Parameters.AddWithValue("@Title", txtTitlePanel.Text.ToString());
        cmd.Parameters.AddWithValue("@Description", txtDescriptionPanel.Text.ToString());
        String Date = "";
        Date = drpYYPanelFrom.SelectedItem.ToString() + "/" + drpMMPanelFrom.SelectedItem.ToString() + "/" + drpDDPanelFrom.SelectedItem.ToString();

        cmd.Parameters.AddWithValue("@FromDate", Date);

        String Date1 = "";
        Date1 = drpYYTo.SelectedItem.ToString() + "/" + DrpMMToPanel.SelectedItem.ToString() + "/" + DrpDDToPanel.SelectedItem.ToString();
        cmd.Parameters.AddWithValue("@ToDate", Date1);

        cmd.Parameters.AddWithValue("@Category", "");
        cmd.Parameters.AddWithValue("@SessionName", DrpSessionName.SelectedValue);
        cmd.Parameters.AddWithValue("@BranchCode", ddlBranch.SelectedValue.ToString());
        cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());

        SupAdm = "Hide";

        if (CheckBoxList2.Items[0].Selected == true)
        {
            Adm = "Show";

        }
        else
        {
            Adm = "Hide";
        }
        if (CheckBoxList2.Items[1].Selected == true)
        {
            staf = "Show";

        }
        else
        {
            staf = "Hide";
        }
        if (CheckBoxList2.Items[2].Selected == true)
        {
            gur = "Show";

        }
        else
        {
            gur = "Hide";
        }
        if (CheckBoxList2.Items[3].Selected == true)
        {
            stu = "Show";

        }
        else
        {
            stu = "Hide";
        }




      
        cmd.Parameters.AddWithValue("@Administrator", "");
        cmd.Parameters.AddWithValue("@SuperAdmin", SupAdm);
        cmd.Parameters.AddWithValue("@Admin", Adm);
        cmd.Parameters.AddWithValue("@Student", stu);
        cmd.Parameters.AddWithValue("@Staff", staf);
        cmd.Parameters.AddWithValue("@Guardian", gur);
        cmd.Parameters.AddWithValue("@LoginType", Session["Logintype"].ToString());
        cmd.Connection = con;


        try
        {

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            //oo.MessageBox("Updated Sucessfully ", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully.", "S");                                    
            oo.ClearControls(this.Page);
            loadNewReocrd();
            txtttle.Focus();
        }
        catch (SqlException) { }
    }
    protected void Button4_Click(object sender, EventArgs e)
    {

    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        sql = "Delete from NewsDescription where NewsId='" + lblvalue.Text+"' and SessionName='" + DrpSessionName.SelectedValue.ToString() + "' and BranchCode=" + ddlBranch.SelectedValue.ToString() + "";

        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = sql;
        cmd.CommandType = CommandType.Text;
        cmd.Connection = con;
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            //oo.MessageBox("Successfully Deleted", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Deleted successfully.", "S");

            loadNewReocrd();
            txtttle.Focus();
        }
        catch (SqlException) { }
    }
    protected void Button8_Click(object sender, EventArgs e)
    {

    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {

     

        LinkButton lnk = (LinkButton)sender;
        Label Label36 = (Label)lnk.NamingContainer.FindControl("Label36");
       string ss = Label36.Text;
        lblID.Text = ss;

        // sql = "Select  distinct ROW_NUMBER() OVER (ORDER BY p.ProductId ASC) AS  [ProductId] ,po.id as ID, pc.ProductCategoryName as[ProductCategoryName], p.ProductName as [ProductName], Pm.ProductTypeName as [ProductTypeName],PO.ProductModelName as [ProductModelName] from Productcategorymaster pc left join ProductName p on p.ProductId=pc.ProductId  left join ProductTypeMaster PM on p.ProductId=PM.ProductId left join ProductModelMaster PO on p.ProductId=PO.ProductId ";        
        sql = "Select  ROW_NUMBER() OVER (ORDER BY NewsId desc) AS SrNo,NewsId,Title,Description,convert(nvarchar,FromDate,106) as FromDate ,convert(nvarchar,ToDate,106) as ToDate,Category,SuperAdmin ,Admin,Student,Guardian,Staff,";
        sql +=  " left(convert(nvarchar,FromDate,106),2) as DD,Right(left(convert(nvarchar,FromDate,106),6),3) as MM , RIGHT(convert(nvarchar,FromDate,106),4) as YY,";
        sql +=  " left(convert(nvarchar,ToDate,106),2) as DDT,Right(left(convert(nvarchar,ToDate,106),6),3) as MMT , RIGHT(convert(nvarchar,ToDate,106),4) as YYT ";
        sql +=  "  from NewsDescription ";
        sql +=  " where NewsId=" + ss;
        sql +=  " and SessionName='" + DrpSessionName.SelectedValue.ToString() + "' and BranchCode=" + ddlBranch.SelectedValue.ToString() + "";
        txtTitlePanel.Text = oo.ReturnTag(sql, "Title");
        txtDescriptionPanel.Text = oo.ReturnTag(sql, "Description");

        CheckBoxList2.Items[0].Selected = false;
        CheckBoxList2.Items[1].Selected = false;
        CheckBoxList2.Items[2].Selected = false;
        CheckBoxList2.Items[3].Selected = false;


        if (oo.ReturnTag(sql, "Admin").Trim() == "Show")
        {
            CheckBoxList2.Items[0].Selected = true;
        }
        else
        {
            CheckBoxList2.Items[0].Selected = false;
        }
        if (oo.ReturnTag(sql, "Staff").Trim() == "Show")
        {
            CheckBoxList2.Items[1].Selected = true;
        }
        else
        {
            CheckBoxList2.Items[1].Selected = false;
        }
        if (oo.ReturnTag(sql, "Guardian").Trim() == "Show")
        {
            CheckBoxList2.Items[2].Selected = true;
        }
        else
        {
            CheckBoxList2.Items[2].Selected = false;
        }
        if (oo.ReturnTag(sql, "Student").Trim() == "Show")
        {
            CheckBoxList2.Items[3].Selected = true;
        }
        else
        {
            CheckBoxList2.Items[3].Selected = false;
        }



        drpYYPanelFrom.Text = oo.ReturnTag(sql, "YY");
        drpMMPanelFrom.Text = oo.ReturnTag(sql, "MM");
        string t = "";
        if (oo.ReturnTag(sql, "DD").Substring(0, 1) == "0")
        {
            t = oo.ReturnTag(sql, "DD").Substring(1, 1);
        }
        else
        {
            t = oo.ReturnTag(sql, "DD");
        }

        drpDDPanelFrom.Text = t;

        drpYYTo.Text = oo.ReturnTag(sql, "YYT");
        DrpMMToPanel.Text = oo.ReturnTag(sql, "MMT");

        if (oo.ReturnTag(sql, "DDT").Substring(0, 1) == "0")
        {
            t = oo.ReturnTag(sql, "DDT").Substring(1, 1);
        }
        else
        {
            t = oo.ReturnTag(sql, "DDT");
        }

        DrpDDToPanel.Text = t;

        Panel1_ModalPopupExtender.Show();
    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        
        Button8.Focus();
        LinkButton lnk = (LinkButton)sender;
        Label Label37 = (Label)lnk.NamingContainer.FindControl("Label37");
        string ss = Label37.Text;
        lblvalue.Text = ss.ToString();
        Panel2_ModalPopupExtender.Show();
    }
    protected void drpYYPanelFrom_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.YearDropDown(drpYYPanelFrom, drpMMPanelFrom, drpDDPanelFrom);
    }
    protected void drpMMPanelFrom_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.MonthDropDown(drpYYPanelFrom, drpMMPanelFrom, drpDDPanelFrom);
    }
    protected void drpDDPanelFrom_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void drpYYTo_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.YearDropDown(drpYYTo, DrpMMToPanel, DrpDDToPanel);
    }
    protected void DrpMMToPanel_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.MonthDropDown(drpYYTo, DrpMMToPanel, DrpDDToPanel);
    }
    protected void CheckBoxList2_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void DrpSessionName_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadNewReocrd();
    }
}
