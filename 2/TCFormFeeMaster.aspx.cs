using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TCFormFeeMaster : Page
{
    private SqlConnection _con;
    private SqlCommand _cmd;
    private readonly Campus _oo;
    private string _sql = String.Empty;

    public TCFormFeeMaster()
    {
        _con = new SqlConnection();
        _oo = new Campus();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((string)Session["LoginName"] == "" || (string)Session["BranchCode"] == "" || (string)Session["SessionName"] == "" || Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        _con = _oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file

        if (!IsPostBack)
        {
            Loadclass();
            Loadgrid();
        }
    }

    protected void Loadgrid()
    {
        _sql = "Select affm.Id,cm.ClassName,CopyType,Amount,ISNULL(BranchName,'Other') Branch from TCFormFeeMaster affm inner join ClassMaster cm on cm.Id=affm.Classid left join BranchMaster bm on bm.Id=affm.BranchId and bm.SessionName=affm.SessionName where cm.SessionName='" + Session["SessionName"] + "' and affm.SessionName='" + Session["SessionName"] + "' and affm.BranchCode=" + Session["BranchCode"] + " and cm.BranchCode=" + Session["BranchCode"] + "";
        GridView1.DataSource = _oo.GridFill(_sql);
        GridView1.DataBind();
    }

    protected void Loadclass()
    {
        _sql = "Select ClassName,Id From ClassMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " Order by CidOrder";
        _oo.FillDropDown_withValue(_sql, drpClass, "ClassName", "Id");
        //_oo.FillDropDown_withValue(_sql, DropDownList2, "ClassName", "Id");
        drpClass.Items.Insert(0, "<--Select-->");
        drpBranch.Items.Insert(0, "<--Select-->");
    }
    protected void lnkSubmit_Click(object sender, EventArgs e)
    {
        if (drpClass.SelectedIndex != 0)
        {
            _sql = "Select * from TCFormFeeMaster where ClassId='" + drpClass.SelectedValue + "' and BranchId='" + drpBranch.SelectedValue + "' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and copytype='" + ddlCopyType.SelectedValue + "'";
            if (_oo.Duplicate(_sql) == false)
            {
                Save();
                Loadgrid();
            }
            else
            {
                //oo.MessageBoxforUpdatePanel("Duplicate Record!", lnkSubmit);
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Duplicate record!", "A");
            }
        }
        else
        {
            //oo.MessageBoxforUpdatePanel("Select Class!", lnkSubmit);
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Select class!", "A");
        }
    }

    protected void Save()               
    {
        _cmd = new SqlCommand();
        _cmd.CommandText = "TCFormFeeMasterProc";
        _cmd.CommandType = CommandType.StoredProcedure;
        _cmd.Connection = _con;
        _cmd.Parameters.AddWithValue("@Type", "Insert");
        _cmd.Parameters.AddWithValue("@Id", "");
        _cmd.Parameters.AddWithValue("@classid", drpClass.SelectedValue);
        _cmd.Parameters.AddWithValue("@CopyType", ddlCopyType.SelectedValue);
        _cmd.Parameters.AddWithValue("@Amount", txtAmount.Text);
        _cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
        _cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
        _cmd.Parameters.AddWithValue("@Branchid", drpBranch.SelectedValue);
        try
        {
            _con.Open();
            _cmd.ExecuteNonQuery();
            _con.Close();
            txtAmount.Text = "";
            //oo.MessageBoxforUpdatePanel("Submitted successfully", lnkSubmit);
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Submitted successfully", "S");

        }
        catch (Exception ex)
        {

            throw;
        }
    }

    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lnk = (Label)chk.NamingContainer.FindControl("Label36");
        lblId.Text = lnk.Text;
        _sql = "Select Id,ClassId,Amount,CopyType from TCFormFeeMaster where Id='" + lblId.Text + "' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        //DropDownList2.SelectedValue = _oo.ReturnTag(_sql, "ClassId");
        txtAmount0.Text = _oo.ReturnTag(_sql, "Amount");
        Panel1_ModalPopupExtender.Show();
    }
    protected void lnkUpdate_Click(object sender, EventArgs e)
    {

        Update();
        Loadgrid();

    }
    protected void Update()
    {
        _cmd = new SqlCommand();
        _cmd.CommandText = "TCFormFeeMasterProc";
        _cmd.CommandType = CommandType.StoredProcedure;
        _cmd.Connection = _con;
        _cmd.Parameters.AddWithValue("@Type", "Update");
        _cmd.Parameters.AddWithValue("@Id", lblId.Text);
        _cmd.Parameters.AddWithValue("@classid", "");
        _cmd.Parameters.AddWithValue("@Amount", txtAmount0.Text);
        _cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
        _cmd.Parameters.AddWithValue("@SessionName", "");

        _con.Open();
        _cmd.ExecuteNonQuery();
        _con.Close();
        //oo.MessageBoxforUpdatePanel("Updated successfully", lnkSubmit);
        Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Updated successfully", "S");


    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        Button8.Focus();
        LinkButton chk = (LinkButton)sender;
        // ReSharper disable once LocalVariableHidesMember
        Label lblId = (Label)chk.NamingContainer.FindControl("Label37");

        lblvalue.Text = lblId.Text;
        Panel2_ModalPopupExtender.Show();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        _sql = "Delete From TCFormFeeMaster where Id='" + lblvalue.Text + "' and  BranchCode=" + Session["BranchCode"] + "";
        _cmd = new SqlCommand(_sql, _con);
        _con.Open();
        _cmd.ExecuteNonQuery();
        _con.Close();
        Loadgrid();
        //oo.MessageBoxforUpdatePanel("Record deleted successfully", btnDelete);
        Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Record deleted successfully", "S");

    }

    protected void drpClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadBranch();

    }

    private void LoadBranch()
    {
        _sql = "Select BranchName,Id from BranchMaster where Classid='" + drpClass.SelectedValue + "' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        BAL.objBal.FillDropDown_withValue(_sql, drpBranch, "BranchName", "Id");
        drpBranch.Items.Insert(0, new ListItem("<-- Select-->", "<-- Select-->"));
    }

    public override void Dispose()
    {
        _con.Dispose();
        _oo.Dispose();
    }
}