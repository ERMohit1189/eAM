using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;

public partial class News : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql , stu , gur = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        con = oo.dbGet_connection();
        if (!IsPostBack)
        {

            oo.AddDateMonthYearDropDown(DrpYear, DrpMonth, DrpDate);
            FindCurrentDateandSetinDropDown();
            oo.AddDateMonthYearDropDown(DrpYear1, DrpMonth1, DrpDate1);
            FindCurrentDateandSetinDropDown1();

            oo.AddDateMonthYearDropDown(drpYYPanelFrom, drpMMPanelFrom, drpDDPanelFrom);
            oo.AddDateMonthYearDropDown(drpYYTo, DrpMMToPanel, DrpDDToPanel);
            sql = "Select  ROW_NUMBER() OVER (ORDER BY NewsId desc) AS SrNo,NewsId,Title,Description,convert(nvarchar,FromDate,106) as FromDate ,convert(nvarchar,ToDate,106) as ToDate from NewsDescription ";
            sql=sql+"  where SessionName='"+Session["SessionName"].ToString()+"' and BranchCode='"+Session["BranchCode"].ToString()+"'";
            sql=sql+" order by NewsId desc";
            Grd.DataSource = oo.GridFill(sql);
            Grd.DataBind();

        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        // @Title nvarchar(2000),@Description nvarchar(4000),@FromDate smalldatetime,@ToDate smalldatetime,@Category nvarchar(50),@SessionName nvarchar(50),
        //@BranchCode nvarchar(50),@LoginName nvarchar(50))
        string stu , gur , staf = string.Empty;

        sql = "Select  ROW_NUMBER() OVER (ORDER BY NewsId desc) AS SrNo,NewsId,Title,Description,convert(nvarchar,FromDate,106) as FromDate ,convert(nvarchar,ToDate,106) as ToDate from NewsDescription ";
        sql = sql + "  where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        sql = sql + "   and Title='" + txtttle.Text + "'";
        if (oo.Duplicate(sql))
        {
            oo.MessageBox("Duplicate Title", this.Page);
        }
        else
        {


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
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
            if (CheckBoxList1.Items[0].Selected )
            {
                stu = "Show";

            }
            else
            {
                stu = "Hide";
            }
            if (CheckBoxList1.Items[1].Selected)
            {
                gur = "Show";

            }
            else
            {
                gur = "Hide";
            }
          
            //@Administrator,@SuperAdmin,@Admin,@Student,@Staff,@Guardian,@LoginType
            cmd.Parameters.AddWithValue("@Administrator", "");
            cmd.Parameters.AddWithValue("@SuperAdmin","" );
            cmd.Parameters.AddWithValue("@Admin", "");
            cmd.Parameters.AddWithValue("@Student",stu );
            cmd.Parameters.AddWithValue("@Staff","");
            cmd.Parameters.AddWithValue("@Guardian", gur);
            cmd.Parameters.AddWithValue("@LoginType", Session["Logintype"].ToString());
            cmd.Connection = con;

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                oo.MessageBox("Successfully Submitted", this.Page);
                oo.ClearControls(this.Page);
                sql = "Select  ROW_NUMBER() OVER (ORDER BY NewsId desc) AS SrNo,NewsId,Title,Description,convert(nvarchar,FromDate,106) as FromDate ,convert(nvarchar,ToDate,106) as ToDate from NewsDescription ";
                sql = sql + " where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
                sql = sql + " order by NewsId desc";
                Grd.DataSource = oo.GridFill(sql);
                Grd.DataBind();
            }
            catch (SqlException ee) { oo.MessageBox(ee.Message.ToString(), this.Page); }
        }
    }
    protected void  DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
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
            DrpMonth.Text = "May";
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
            DrpMonth1.Text = "Nov";
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
        cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
        cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
        cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
        if (CheckBoxList2.Items[0].Selected == true)
        {
            stu = "Show";

        }
        else
        {
            stu = "Hide";
        }

        if (CheckBoxList2.Items[1].Selected == true)
        {
            gur = "Show";

        }
        else
        {
            gur = "Hide";
        }

        //@Administrator,@SuperAdmin,@Admin,@Student,@Staff,@Guardian,@LoginType
        cmd.Parameters.AddWithValue("@Administrator", "");
        cmd.Parameters.AddWithValue("@SuperAdmin", "");
        cmd.Parameters.AddWithValue("@Admin", "");
        cmd.Parameters.AddWithValue("@Student", stu);
        cmd.Parameters.AddWithValue("@Staff", "");
        cmd.Parameters.AddWithValue("@Guardian", gur);
        cmd.Parameters.AddWithValue("@LoginType", Session["Logintype"].ToString());
        cmd.Connection = con;


        try
        {

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            oo.MessageBox("Updated Sucessfully ", this.Page);
            oo.ClearControls(this.Page);
            sql = "Select  ROW_NUMBER() OVER (ORDER BY NewsId desc) AS SrNo,NewsId,Title,Description,convert(nvarchar,FromDate,106) as FromDate ,convert(nvarchar,ToDate,106) as ToDate from NewsDescription ";
            sql=sql+" where SessionName='"+Session["SessionName"].ToString()+"' and BranchCode='"+Session["BranchCode"].ToString()+"'";
            sql=sql+" order by NewsId desc";

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
        sql = "Delete from NewsDescription where NewsId=" + lblvalue.Text;
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
            oo.MessageBox("Deleted successfully.", this.Page);

            sql = "Select  ROW_NUMBER() OVER (ORDER BY NewsId desc) AS SrNo,NewsId,Title,Description,convert(nvarchar,FromDate,106) as FromDate ,convert(nvarchar,ToDate,106) as ToDate,Category ,";
            sql = sql + " left(convert(nvarchar,FromDate,106),2) as DD,Right(left(convert(nvarchar,FromDate,106),6),3) as MM , RIGHT(convert(nvarchar,FromDate,106),4) as YY,";
            sql = sql + " left(convert(nvarchar,ToDate,106),2) as DDT,Right(left(convert(nvarchar,ToDate,106),6),3) as MMT , RIGHT(convert(nvarchar,ToDate,106),4) as YYT ";
            sql = sql + "  from NewsDescription ";
            sql = sql + " where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

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
        string ss = chk.Text;
        lblID.Text = ss;

        // sql = "Select  distinct ROW_NUMBER() OVER (ORDER BY p.ProductId ASC) AS  [ProductId] ,po.id as ID, pc.ProductCategoryName as[ProductCategoryName], p.ProductName as [ProductName], Pm.ProductTypeName as [ProductTypeName],PO.ProductModelName as [ProductModelName] from Productcategorymaster pc left join ProductName p on p.ProductId=pc.ProductId  left join ProductTypeMaster PM on p.ProductId=PM.ProductId left join ProductModelMaster PO on p.ProductId=PO.ProductId ";        
        sql = "Select  ROW_NUMBER() OVER (ORDER BY NewsId desc) AS SrNo,NewsId,Title,Description,convert(nvarchar,FromDate,106) as FromDate ,convert(nvarchar,ToDate,106) as ToDate,Category ,Student,Guardian,Staff,";
        sql = sql + " left(convert(nvarchar,FromDate,106),2) as DD,Right(left(convert(nvarchar,FromDate,106),6),3) as MM , RIGHT(convert(nvarchar,FromDate,106),4) as YY,";
        sql = sql + " left(convert(nvarchar,ToDate,106),2) as DDT,Right(left(convert(nvarchar,ToDate,106),6),3) as MMT , RIGHT(convert(nvarchar,ToDate,106),4) as YYT ";
        sql = sql+"  from NewsDescription ";
        sql = sql + " where NewsId=" + ss;
        sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        txtTitlePanel.Text = oo.ReturnTag(sql, "Title");
        txtDescriptionPanel.Text = oo.ReturnTag(sql, "Description");

        CheckBoxList2.Items[0].Selected = false;
    

        if (oo.ReturnTag(sql, "Student").Trim() == "Show")
        {
            CheckBoxList2.Items[0].Selected = true;
        }
        else
        {
            CheckBoxList2.Items[0].Selected = false;
        }

        if (oo.ReturnTag(sql, "Guardian").Trim() == "Show")
        {
            CheckBoxList2.Items[1].Selected = true;
        }
        else
        {
            CheckBoxList2.Items[1].Selected = false;
        }
      

        drpYYPanelFrom.Text = oo.ReturnTag(sql, "YY");
        drpMMPanelFrom.Text=oo.ReturnTag(sql,"MM");
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

        LinkButton chk = (LinkButton)sender;
        string ss = chk.Text;
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
}
