using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;


public partial class admin_LibraryTitleWiseReport : Page
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
        //con.ConnectionString = ConfigurationManager.ConnectionStrings["Menu"].ConnectionString;
         con = oo.dbGet_connection();
        if (!IsPostBack)
        
        {
            sql = "Select distinct Title from LibraryItemEntry where BranchCode=" + Session["BranchCode"] + "";
            oo.FillDropDownWithOutSelect(sql, DropDownList1, "Title");

           
            
            

            LinkButton2.Visible = false;

        }
    }
   
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        //GridView1.PageSize = Convert.ToInt32(txtPageSize.Text);
        int i = 0, co = 1;
        sql = "Select Row_Number() over (order by Id Asc) as SrNo, Id,AccessionNo  ,GroupId  ,Title  ,convert(nvarchar,LibraryEntryDate,106) as LibraryEntryDate  ,Supplier  ,Publisher  ,NoOfItem  ,";
        sql=sql+"   Language,BillNo  ,BiilDate  ,PublicationYear  ,SubjectTopic  ,   Category  ,SubCategory  ,Author1  ,Author2  ,";
        sql=sql+"   Author3  ,Keyword1  ,Keyword2  ,Keyword3  ,Edition  ,Source  ,Location  ,Editor  ,ISBNISSN  ,Pages  ,Translator  ,";
        sql = sql + "   Size  ,Illustrator  ,   Compiler  ,Price  ,SavedBy  ,Image  ,Remark  ,LoginName  ,SessionName  ,BranchCode,";
        sql = sql + "  RecordDate from LibraryItemEntry where  Title='" + DropDownList1.SelectedItem.ToString() + "' and BranchCode=" + Session["BranchCode"] + " and DeleteBookYesno is null or DeleteBookYesno='No' order by convert(numeric,AccessionNo) asc";
        GridView1.DataSource = oo.GridFill(sql);
        GridView1.DataBind();
        LinkButton2.Visible = true;
        if(GridView1.Rows.Count==0)
        {
            oo.MessageBox("Sorry, No Record(s) found!", this.Page);
            LinkButton2.Visible = false;
        }
        else
        {

            for (i = 0; i <= GridView1.Rows.Count - 1; i++)
            {
                Label ll = (Label)GridView1.Rows[i].FindControl("Label1");
                 
                ll.Text = co.ToString();
                co++;

            }
        }
    }
   
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = abc;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        sql = "Select Row_Number() over (order by Id Asc) as SrNo, Id,AccessionNo  ,GroupId  ,Title  ,convert(nvarchar,LibraryEntryDate,106) as LibraryEntryDate  ,Supplier  ,Publisher  ,NoOfItem  ,";
        sql = sql + "   Language  ,BillNo  ,BiilDate  ,PublicationYear  ,SubjectTopic  ,   Category  ,SubCategory  ,Author1  ,Author2  ,";
        sql = sql + "   Author3  ,Keyword1  ,Keyword2  ,Keyword3  ,Edition  ,Source  ,Location  ,Editor  ,ISBNISSN  ,Pages  ,Translator  ,";
        sql = sql + "   Size  ,Illustrator  ,   Compiler  ,Price  ,SavedBy  ,Image  ,Remark  ,LoginName  ,SessionName  ,BranchCode,";
        sql = sql + "  RecordDate from LibraryItemEntry where  Title='" + DropDownList1.SelectedItem.ToString() + "' and BranchCode=" + Session["BranchCode"] + " and DeleteBookYesno is null or DeleteBookYesno='No' order by convert(numeric,AccessionNo) asc";
        GridView1.DataSource = oo.GridFill(sql);
        GridView1.DataBind();
    }
}
