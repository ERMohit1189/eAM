using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class admin_LibraryClasswiseReport : Page
{
    SqlConnection con = new SqlConnection();
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
            Image1.ImageUrl = "DisplayImage.ashx?UserLoginID=" + 1;

            sql = "Select CollegeName from CollegeMaster where  BranchCode=" + Session["BranchCode"] + "";
            lblCollegeName.Text = oo.ReturnTag(sql, "CollegeName");
            abc.Visible = false;
            loadClass();
        }
    }

    private void loadClass()
    {
        sql = "Select ClassName,id from ClassMaster Where SessionName='"+Session["SessionName"].ToString()+ "' and BranchCode=" + Session["BranchCode"] + "";
        oo.FillDropDown_withValue(sql, drpClass, "ClassName","id");
        drpClass.Items.Insert(0, "Select All");
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        loadGrid();
    }

    protected void loadGrid()
    {
        int value;
        bool value1=int.TryParse(txtPageSize.Text,out value);

        if(value1)
        {
            GridView1.PageSize = Convert.ToInt32(txtPageSize.Text);
        }
        else
        {
            GridView1.PageSize=100;
        }

            sql = "Select Row_Number() over (order by lie.Id Asc) as SrNo, lie.Id,Cm.ClassName,CIDorder,AccessionNo  ,GroupId  ,Title  ,convert(nvarchar,LibraryEntryDate,106) as LibraryEntryDate  ,Supplier  ,Publisher  ,NoOfItem  ,";
            sql = sql + " Language  ,BillNo  ,BiilDate  ,PublicationYear  ,SubjectTopic  ,   Category  ,SubCategory  ,(Case Author1 when '' then Author2 else Author1 End) as AuthorName";
            sql = sql + " ,Keyword1  ,Keyword2  ,Keyword3  ,Edition  ,Source  ,lie.Location  ,Editor  ,ISBNISSN  ,Pages  ,Translator  ,";
            sql = sql + " Size  ,Illustrator  ,   Compiler  ,Price  ,SavedBy  ,Image  ,lie.Remark  ,lie.LoginName  ,lie.SessionName  ,lie.BranchCode,";
            sql = sql + " lie.RecordDate from LibraryItemEntry lie ";
            sql = sql + " Inner join ClassMaster cm on cm.Id=lie.ClassId";
            if (drpClass.SelectedIndex != 0)
            {
                sql = sql + " where lie.ClassId='" + drpClass.SelectedValue.ToString() + "' and DeleteBookYesno is null or DeleteBookYesno='No' and lie.SessionName='" + Session["SessionName"].ToString() + "' and cm.SessionName='" + Session["SessionName"].ToString() + "'";
            }
            else
            {
                sql = sql + " where DeleteBookYesno is null or DeleteBookYesno='No' and lie.SessionName='" + Session["SessionName"].ToString() + "' and cm.SessionName='" + Session["SessionName"].ToString() + "'";
            }
            sql = sql + " and lie.BranchCode=" + Session["BranchCode"] + " and cm.BranchCode=" + Session["BranchCode"] + " order by  CidOrder Asc,convert(numeric,AccessionNo) asc";
       

        GridView1.DataSource = oo.GridFill(sql);
        GridView1.DataBind();

        if (GridView1.Rows.Count == 0)
        {
            oo.MessageBox("Sorry, No Record(s) found!", this.Page);
        }
        else
        {
            abc.Visible = true;
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        loadGrid();
    }

    protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
    {
        PrintHelper_New.ctrl = abc;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
}