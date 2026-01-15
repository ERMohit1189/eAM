using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UpdateAccessionNo : Page
{
    private SqlConnection _con;
    readonly SqlConnection _con1;
    private SqlCommand _cmd1;
    readonly Campus _oo;
    private string _sql = "";
    private DataTable _dt;
    public UpdateAccessionNo()
    {
        _con = new SqlConnection();
        _con1 = new SqlConnection();
        _oo = new Campus();
    }
    protected void Page_PreInIt(object sender, EventArgs e)
    {
        if (Session["Logintype"] == null || Session["LoginType"].ToString() != "Admin")
        {
            Response.Redirect("~/default.aspx");
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        _con = _oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file
        if (!IsPostBack)
        {
        }
    }
    protected void lnkShow_Click(object sender, EventArgs e)
    {
        Show();
    }

    public void Show()
    {
        _sql = "select li.AccessionNo, li.Title, li.Author1,stm.SubjectName as subjectOrTopic, icm.CategoryName, iscm.SubCategoryName,";
        _sql = _sql + " ilm.CategoryName as languages, SupplierName, PublisherName, format(li.RecordDate, 'dd-MMM-yyyy hh:mm:ss tt')RecordDate, li.LoginName, ";
        _sql = _sql + " case when isnull(li.DeleteBookYesno, '')='' then 'Active' else case when isnull(li.DeleteBookYesno, '')='No' then 'Active' else 'Inactive' end end as BookStatus ";
        _sql = _sql + " from LibraryItemEntry li";
        _sql = _sql + " inner join SupplierInfoEntry sup on sup.Id=convert(int, isnull(li.Supplier, '0')) and sup.BranchCode=li.BranchCode";
        _sql = _sql + " inner join PublisherInfoEntry pub on pub.Id=convert(int, isnull(li.Publisher, '0')) and pub.BranchCode=li.BranchCode";
        _sql = _sql + " inner join ItemLanguageMaster ilm on ilm.Id=convert(int, isnull(li.Language, '0')) and ilm.BranchCode=li.BranchCode";
        _sql = _sql + " inner join SubjectTopicLibraryMaster stm on stm.Id=convert(int, isnull(li.SubjectTopic, '0')) and stm.BranchCode=li.BranchCode";
        _sql = _sql + " inner join ItemCategoryMaster icm on icm.Id=convert(int, isnull(li.Category, '0')) and icm.BranchCode=li.BranchCode";
        _sql = _sql + " inner join ItemSubCategoryMaster iscm on iscm.Id=convert(int, isnull(li.SubCategory, '0')) and iscm.BranchCode=li.BranchCode";
        _sql = _sql + " where li.BranchCode=" + Session["BranchCode"] + " and li.AccessionNo='" + TxtEnter.Text.Trim() + "'";
        Grd.DataSource = _oo.GridFill(_sql);
        Grd.DataBind();
        if (Grd.Rows.Count > 0)
        {
            grdshow.Visible = true;
            newsrdis.Visible = true;
            TextBox1.Text = "";
            GridView1.DataSource = null;
            GridView1.Visible = false;
        }
        else
        {
            grdshow.Visible = false;
            newsrdis.Visible = false;
            TextBox1.Text = "";
            GridView1.DataSource = null;
            GridView1.Visible = false;
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Invalid Accession No.!", "A");
        }
    }
    protected void Submit_Click(object sender, EventArgs e)
    {
        try
        {
            string sql1 = "select AccessionNo from LibraryItemEntry where  AccessionNo='" + TextBox1.Text.Trim() + "' and BranchCode=" + Session["BranchCode"] + "";
            if (!_oo.Duplicate(sql1))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "update LibraryItemEntry set AccessionNo='" + TextBox1.Text.Trim() + "', LoginName='" + Session["LoginName"] + "', RecordDate=getdate() where  AccessionNo='" + TxtEnter.Text.Trim() + "' and BranchCode=" + Session["BranchCode"] + "";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = _con;
                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
                Fetchnewsr();
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Accession No. updated successfully.", "S");
            }
            else
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Accession No. already exists.", "A");
            }
        }
        catch (Exception ex)
        {
        }
    }

    public void Fetchnewsr()
    {
        _sql = "select li.AccessionNo, li.Title, li.Author1,stm.SubjectName as subjectOrTopic, icm.CategoryName, iscm.SubCategoryName,";
        _sql = _sql + " ilm.CategoryName as languages, SupplierName, PublisherName, format(li.RecordDate, 'dd-MMM-yyyy hh:mm:ss tt')RecordDate, li.LoginName, ";
        _sql = _sql + " case when isnull(li.DeleteBookYesno, '')='' then 'Active' else case when isnull(li.DeleteBookYesno, '')='No' then 'Active' else 'Inactive' end end as BookStatus ";
        _sql = _sql + " from LibraryItemEntry li";
        _sql = _sql + " inner join SupplierInfoEntry sup on sup.Id=convert(int, isnull(li.Supplier, '0')) and sup.BranchCode=li.BranchCode";
        _sql = _sql + " inner join PublisherInfoEntry pub on pub.Id=convert(int, isnull(li.Publisher, '0')) and pub.BranchCode=li.BranchCode";
        _sql = _sql + " inner join ItemLanguageMaster ilm on ilm.Id=convert(int, isnull(li.Language, '0')) and ilm.BranchCode=li.BranchCode";
        _sql = _sql + " inner join SubjectTopicLibraryMaster stm on stm.Id=convert(int, isnull(li.SubjectTopic, '0')) and stm.BranchCode=li.BranchCode";
        _sql = _sql + " inner join ItemCategoryMaster icm on icm.Id=convert(int, isnull(li.Category, '0')) and icm.BranchCode=li.BranchCode";
        _sql = _sql + " inner join ItemSubCategoryMaster iscm on iscm.Id=convert(int, isnull(li.SubCategory, '0')) and iscm.BranchCode=li.BranchCode";
        _sql = _sql + " where li.BranchCode=" + Session["BranchCode"] + " and li.AccessionNo='" + TextBox1.Text.Trim() + "'";
        GridView1.DataSource = _oo.GridFill(_sql);
        GridView1.DataBind();
        newsrdis.Visible = true;
        GridView1.Visible = true;
        grdshow.Visible = true;
        newsrdis.Visible = true;
        GridView1.Visible = true;
    }
    public override void Dispose()
    {
        _con.Dispose();
        _con1.Dispose();
        _oo.Dispose();
    }
}
