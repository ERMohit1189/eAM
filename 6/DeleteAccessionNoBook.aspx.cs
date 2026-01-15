using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;

public partial class admin_DeleteAccessionNoBook : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if ( Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader); 

        //con.ConnectionString = ConfigurationManager.ConnectionStrings["Menu"].ConnectionString;
        if (!IsPostBack)
        {
            GridDisplay();
        }
    }
    

    protected void LinkButton2_Click(object sender, EventArgs e)
    {

        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("Label36");
        string ss = lblId.Text;
        lblID.Text = ss;

        sql = " select ROW_NUMBER() OVER (ORDER BY Id ASC) AS SrNo,id,AccessionNo,DeleteBookYesno,DeleteBookRemark from LibraryItemEntry where id=" + ss+ " and BranchCode=" + Session["BranchCode"] +"";
        txtAccessNoPanel.Text = oo.ReturnTag(sql, "AccessionNo");
        txtRemarkPanel.Text = oo.ReturnTag(sql, "DeleteBookRemark");
        try
        {
            DropDownList1.Text = oo.ReturnTag(sql, "DeleteBookYesno");
        }
        catch (Exception) { DropDownList1.Text = "No"; }
        Panel1_ModalPopupExtender.Show();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        sql = "Delete from LibraryItemEntry where BranchId=" + lblvalue.Text.Trim() + " and BranchCode=" + Session["BranchCode"] + "";

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
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Disposed successfully.", "S");       

           
        }
        catch (SqlException) { }
    }
    protected void Button8_Click(object sender, EventArgs e)
    {

    }
    protected void Button3_Click(object sender, EventArgs e)
    {

    }
    protected void Button4_Click(object sender, EventArgs e)
    {

    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        Button8.Focus();
        LinkButton chk = (LinkButton)sender;
        string ss = chk.Text;
        lblvalue.Text = ss.ToString();
        Panel2_ModalPopupExtender.Show();
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        string ss = "";
        sql = "select * from LibraryItemEntry where AccessionNo='" + txtAccession.Text.Trim() + "' and BranchCode = " + Session["BranchCode"] +"";
        if (oo.Duplicate(sql))
        {
            ss = "update LibraryItemEntry set DeleteBookYesno='Yes', DeleteBookRemark='" + txtRemark.Text.Trim() + "'" + "  where AccessionNo='" + txtAccession.Text.Trim() + "' and BranchCode = " + Session["BranchCode"] + "";
            oo.ProcedureDatabase(ss);
            //oo.MessageBox("Deleted successfully.", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Disposed successfully.", "S");
            txtRemark.Text = txtAccession.Text = "";
            GridDisplay();
        }

       


    }
    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        string yesNo = DropDownList1.SelectedItem.ToString() == "Yes" ? "No" : "Yes";


            sql = "update LibraryItemEntry set DeleteBookYesno='"+ yesNo.Trim().ToString() + "', DeleteBookRemark='" + txtRemarkPanel.Text.Trim() + "'" + "  where AccessionNo='" + txtAccessNoPanel.Text.Trim() + "' and BranchCode = " + Session["BranchCode"] + "";
            oo.ProcedureDatabase(sql);
            //oo.MessageBox("Successfully Upated!", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully.", "S");       

            GridDisplay();
       
    }
    protected void LinkButton5_Click(object sender, EventArgs e)
    {

    }

    public void GridDisplay()
    {
        sql = "Select  ROW_NUMBER() OVER (ORDER BY Id ASC) AS SrNo,id,AccessionNo,DeleteBookYesno,DeleteBookRemark from LibraryItemEntry where isnull(DeleteBookYesno, '')='Yes' and isnull(id, '')<>'' and BranchCode = " + Session["BranchCode"] + " ";
        Grd.DataSource = oo.GridFill(sql);
        Grd.DataBind();

        if (Grd.Rows.Count == 0)
        {
            lblResults.Text = "Sorry, No Information available!";
        }
    }
}