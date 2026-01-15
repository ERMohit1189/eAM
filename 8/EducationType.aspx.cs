using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class EducationType : System.Web.UI.Page
{
    private SqlConnection _con = new SqlConnection();
    private readonly Campus _oo = new Campus();
    private string _sql, _sql1 = String.Empty;


    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        //con.ConnectionString = ConfigurationManager.ConnectionStrings["Menu"].ConnectionString;
        _con = _oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file

        if (!IsPostBack)
        {

            Select();
            
        }

        // Grd.FooterRow.Visible = false;
    }

    private void Select()
    {

        string msg = "";
        try
        {
            BAL.EducationType objBal = new BAL.EducationType();
            Tuple<string, DataTable> tuple;
            objBal.Queryfor = "S";
            objBal.SessionName = Session["SessionName"].ToString().Trim();
            objBal.BranchCode = Convert.ToInt16(Session["BranchCode"].ToString().Trim());
            tuple = DAL.objDal.EducationType(objBal);
            Grd.DataSource = tuple.Item2;
            Grd.DataBind();
            for (int i = 0; i < Grd.Rows.Count; i++)
            {
                Label lblid = (Label)Grd.Rows[i].FindControl("Label36");
                LinkButton lnkEdit = (LinkButton)Grd.Rows[i].FindControl("LinkButton2");
                LinkButton lnkDelete = (LinkButton)Grd.Rows[i].FindControl("LinkButton3");
                _sql = "select Subject from tbl_SubjectForEmploymentForm where EducationType=" + lblid.Text + " and BranchCode=" + Session["BranchCode"] + "";
                if (_oo.Duplicate(_sql))
                {
                    lnkEdit.Text = "<i class='fa fa-lock'></i>";
                    lnkDelete.Text = "<i class='fa fa-lock'></i>";
                    lnkEdit.Enabled = false;
                    lnkDelete.Enabled = false;
                }
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message;
            msgbox.InnerHtml = msg;
        }
    }
    
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        _sql= "select EducationType from tbl_EducationType where BranchCode=" + Session["BranchCode"] + " and EducationType='"+ txtEducationType.Text.Trim() + "'";
        if (_oo.Duplicate(_sql))
        {
            _oo.msgbox(this.Page, msgbox, "Duplicate entry!", "A");
            return;
        }    
        string msg = "";
        try
        {
            BAL.EducationType objBal = new BAL.EducationType();
            Tuple<string, DataTable> tuple;
            objBal.Queryfor = "I";
            objBal.EducationTypes = txtEducationType.Text.Trim();
            objBal.SessionName = Session["SessionName"].ToString().Trim();
            objBal.BranchCode = Convert.ToInt16(Session["BranchCode"].ToString().Trim());
            objBal.LoginName = Session["LoginName"].ToString().Trim();
            tuple = DAL.objDal.EducationType(objBal);
            msg = tuple.Item1;
            txtEducationType.Text = "";
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        //lblMess.Text = objBal.SimpleMessageType(msg, div1);
        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");
        Select();
    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        Button8.Focus();
        LinkButton lnk = (LinkButton)sender;
        Label lblId = (Label)lnk.NamingContainer.FindControl("Label37");
        lblvalue .Text= lblId.Text;
        Panel2_ModalPopupExtender.Show();
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("Label36");
        Label lblEducationType = (Label)chk.NamingContainer.FindControl("lblEducationType");
        txtEducationTypeUpdate.Text = lblEducationType.Text;
        lblID.Text = lblId.Text;
        Panel1_ModalPopupExtender.Show();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        _sql = "select EducationType from tbl_EducationType where BranchCode=" + Session["BranchCode"] + " and EducationType='" + txtEducationTypeUpdate.Text.Trim() + "' and id<>"+ lblID.Text + "";
        if (_oo.Duplicate(_sql))
        {
            _oo.msgbox(this.Page, msgbox, "Duplicate entry!", "A");
            return;
        }
        string msg = "";
        try
        {
            BAL.EducationType objBal = new BAL.EducationType();
            Tuple<string, DataTable> tuple;
            objBal.Queryfor = "U";
            objBal.id = Convert.ToInt16(lblID.Text);
            objBal.EducationTypes = txtEducationTypeUpdate.Text.Trim();
            objBal.SessionName = Session["SessionName"].ToString().Trim();
            objBal.BranchCode = Convert.ToInt16(Session["BranchCode"].ToString().Trim());
            objBal.LoginName = Session["LoginName"].ToString().Trim();
            tuple = DAL.objDal.EducationType(objBal);
            msg = tuple.Item1;
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        //lblMess.Text = objBal.SimpleMessageType(msg, div2);
        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully.", "S");
        Select();

    }
    protected void Button4_Click(object sender, EventArgs e)
    {

    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string msg = "";
        try
        {
            BAL.EducationType objBal = new BAL.EducationType();
            Tuple<string, DataTable> tuple;
            objBal.Queryfor = "D";
            objBal.id = Convert.ToInt16(lblvalue.Text);
            tuple = DAL.objDal.EducationType(objBal);
            msg = tuple.Item1;
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Deleted successfully.", "S");
        Select();
    }
    protected void Button8_Click(object sender, EventArgs e)
    {

    }
    
    protected void drpCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        //loadBranch();
    }

    protected void drpPanelCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        //loadPanelBranch();
        Panel1_ModalPopupExtender.Show();
    }

    public override void Dispose()
    {
        _con.Dispose();
        _oo.Dispose();
    }
}
