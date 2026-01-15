using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Templates : System.Web.UI.Page
{
    private SqlConnection _con;
    private SqlCommand _cmd;
    private readonly Campus _oo;
    private string _sql = String.Empty;
    public Templates()
    {
        _con = new SqlConnection();
        _oo = new Campus();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        _con = _oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);

        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        if (!IsPostBack)
        {
            //_sql = "select * from BoardMaster where BranchCode=" + Session["BranchCode"] + "";
            //if (_oo.Duplicate(_sql))
            //{
            //    _oo.FillDropDown_withValue(_sql, ddlBoard, "BoardName", "Id");
            //    ddlBoard.Items.Insert(0, new ListItem("<--Select-->", ""));
            //    _oo.FillDropDown_withValue(_sql, ddlBoardReceipt, "BoardName", "Id");
            //    ddlBoardReceipt.Items.Insert(0, new ListItem("<--Select-->", ""));
            //    _oo.FillDropDown_withValue(_sql, ddlBoardAdmission, "BoardName", "Id");
            //    ddlBoardAdmission.Items.Insert(0, new ListItem("<--Select-->", ""));
            //    _oo.FillDropDown_withValue(_sql, ddlBoardFeeCard, "BoardName", "Id");
            //    ddlBoardFeeCard.Items.Insert(0, new ListItem("<--Select-->", ""));
            LoadTcgrid();
            //}
        }
    }

    protected void reportType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (reportType.SelectedIndex==0)
        {
            TCTemplate.Visible = true;
            ReceiptTemplate.Visible = false;
            AdmissionFormTemplate.Visible = false;
            FeeCardTemplate.Visible = false;
            LoadTcgrid();
        }
        else if (reportType.SelectedIndex == 1)
        {
            TCTemplate.Visible = false;
            ReceiptTemplate.Visible = true;
            AdmissionFormTemplate.Visible = false;
            FeeCardTemplate.Visible = false;
            LoadReceiptgrid();
        }
        else if (reportType.SelectedIndex == 2)
        {
            TCTemplate.Visible = false;
            ReceiptTemplate.Visible = false;
            AdmissionFormTemplate.Visible = true;
            FeeCardTemplate.Visible = false;
            LoadAdmissionFormgrid();
        }
        else if (reportType.SelectedIndex == 3)
        {
            TCTemplate.Visible = false;
            ReceiptTemplate.Visible = false;
            AdmissionFormTemplate.Visible = false;
            FeeCardTemplate.Visible = true;
            FeeCardgrid();
        }
    }

    protected void LoadTcgrid()
    {
        _sql = "select *, case when IsLock=1 then 'Locked' else 'Unlocked' end  as IsLockList from TCAndReceiptTemplate tc inner join (Select 'Template 1' Template,'CBSE-English' Text UNION Select 'Template 2','CBSE-Hindi' UNION Select 'Template 3','ICSE/ISC' UNION Select 'Template 4','UPBoard-Hindi' UNION Select 'Template 5','UPBoard-English')T1 on T1.Template=tc.Template where BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "'";
        var dt=_oo.Fetchdata(_sql);
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
    protected void LoadReceiptgrid()
    {
        _sql = "select *, case when IsLock=1 then 'Locked' else 'Unlocked' end  as IsLockList from FeereceiptTemplate frct inner join (Select 'Template 1' Template,'Template 1' Text UNION Select 'Template 2','Template 2' UNION Select 'Template 3','Template 3' UNION Select 'Template 4','Template 4' UNION Select 'Template 5','Template 5')T1 on T1.Template=frct.Template where BranchCode=" + Session["BranchCode"] + "";
        var dt = _oo.Fetchdata(_sql);
        GridView3.DataSource = dt;
        GridView3.DataBind();
    }
    protected void LoadAdmissionFormgrid()
    {
        _sql = "select *, case when IsLock=1 then 'Locked' else 'Unlocked' end  as IsLockList from AdmissionFormTemplate aft inner join (Select 'Template 1' Template,'Hindi Version' Text UNION Select 'Template 2','English Version' UNION Select 'Template 3','Extended Version')T1 on T1.Template=aft.Template where BranchCode=" + Session["BranchCode"] + "";
        var dt = _oo.Fetchdata(_sql);
        GridView2.DataSource = dt;
        GridView2.DataBind();
    }
    protected void FeeCardgrid()
    {
        _sql = "select *, case when IsLock=1 then 'Locked' else 'Unlocked' end  as IsLockList from FeeCardTemplate fct inner join (Select 'Template 1' Template,'Template 1' Text UNION Select 'Template 2','Template 2' UNION Select 'Template 3' Template,'Template 3' Text)T1 on T1.Template=fct.Template where BranchCode=" + Session["BranchCode"] + "";
        var dt = _oo.Fetchdata(_sql);
        GridViewFeeCard.DataSource = dt;
        GridViewFeeCard.DataBind();
    }
    protected void LinkSubmit1_Click(object sender, EventArgs e)
    {
        _sql = "select Template from TCAndReceiptTemplate where BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "'";
        if (_oo.Duplicate(_sql))
        {
            _sql = "update TCAndReceiptTemplate set Template='" + ddlTemplate.SelectedValue + "', IsLock=" + (chkisLock.Checked ? 1 : 0) + " where BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "'";
        }
        else
        {
            _sql = "insert into TCAndReceiptTemplate(Template, IsLock, SessionName, BranchCode, LoginName, RecordedDate) values ('" + ddlTemplate.SelectedValue + "', " + (chkisLock.Checked ? 1 : 0) + ", '" + Session["SessionName"] + "'," + Session["BranchCode"] + ",'" + Session["LoginName"] + "', getdate())";
        }
        _cmd = new SqlCommand(_sql, _con);
        _con.Open();
        _cmd.ExecuteNonQuery();
        _con.Close();
        LoadTcgrid();
        Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Submitted successfully", "S");
    }

    protected void LinkReceipt_Click(object sender, EventArgs e)
    {
        _sql = "select 1 from FeereceiptTemplate where BranchCode=" + Session["BranchCode"] + "";
        if (_oo.Duplicate(_sql))
        {
            _sql = "update FeereceiptTemplate set Template='" + ddlTemplateReceipt.SelectedValue + "', IsLock=" + (chkisLockReceipt.Checked ? 1 : 0) + " where BranchCode=" + Session["BranchCode"] + "";
        }
        else
        {
            _sql = "insert into FeereceiptTemplate(Template, Board, IsLock, SessionName, BranchCode, LoginName, RecordedDate) values ('" + ddlTemplateReceipt.SelectedValue + "', " + (chkisLockReceipt.Checked ? 1 : 0) + ", '" + Session["SessionName"] + "'," + Session["BranchCode"] + ",'" + Session["LoginName"] + "', getdate())";
        }
        _cmd = new SqlCommand(_sql, _con);
        _con.Open();
        _cmd.ExecuteNonQuery();
        _con.Close();
        LoadReceiptgrid();
        Campus camp = new Campus(); camp.msgbox(Page, msgbox3, "Submitted successfully", "S");
    }
    protected void LinkAdmission_Click(object sender, EventArgs e)
    {
        _sql = "select 1 from AdmissionFormTemplate where BranchCode=" + Session["BranchCode"] + "";
        if (_oo.Duplicate(_sql))
        {
            _sql = "update AdmissionFormTemplate set Template='" + ddlTemplateAdmission.SelectedValue + "', IsLock=" + (chkisLockAdmission.Checked ? 1 : 0) + " where BranchCode=" + Session["BranchCode"] + "";
        }
        else
        {
            _sql = "insert into AdmissionFormTemplate(Template, Board, IsLock, SessionName, BranchCode, LoginName, RecordedDate) values ('" + ddlTemplateAdmission.SelectedValue + "', " + (chkisLockAdmission.Checked ? 1 : 0) + ", '" + Session["SessionName"] + "'," + Session["BranchCode"] + ",'" + Session["LoginName"] + "', getdate())";
        }
        _cmd = new SqlCommand(_sql, _con);
        _con.Open();
        _cmd.ExecuteNonQuery();
        _con.Close();
        LoadAdmissionFormgrid();
        Campus camp = new Campus(); camp.msgbox(Page, msgbox2, "Submitted successfully", "S");
    }
    protected void LinkFeeCard_Click(object sender, EventArgs e)
    {
        _sql = "select 1 from FeeCardTemplate where BranchCode=" + Session["BranchCode"] + "";
        if (_oo.Duplicate(_sql))
        {
            _sql = "update FeeCardTemplate set Template='" + ddlTemplateFeeCard.SelectedValue + "', IsLock=" + (chkisLockFeeCard.Checked ? 1 : 0) + " where BranchCode=" + Session["BranchCode"] + "";
        }
        else
        {
            _sql = "insert into FeeCardTemplate(Template, IsLock, SessionName, BranchCode, LoginName, RecordedDate) values ('" + ddlTemplateFeeCard.SelectedValue + "', " + (chkisLockFeeCard.Checked ? 1 : 0) + ", '" + Session["SessionName"] + "'," + Session["BranchCode"] + ",'" + Session["LoginName"] + "', getdate())";
        }
        _cmd = new SqlCommand(_sql, _con);
        _con.Open();
        _cmd.ExecuteNonQuery();
        _con.Close();
        FeeCardgrid();
        Campus camp = new Campus(); camp.msgbox(Page, msgbox4, "Submitted successfully", "S");
    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        Button8.Focus();
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("Label37");
        lblvalue.Text = lblId.Text;
        Panel2_ModalPopupExtender.Show();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        _sql = "Delete From TCAndReceiptTemplate where Id='" + lblvalue.Text + "' and  BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "'";
        _cmd = new SqlCommand(_sql, _con);
        _con.Open();
        _cmd.ExecuteNonQuery();
        _con.Close();
        LoadTcgrid();
        Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Record deleted successfully", "S");

    }
    protected void lnkAdmissionDelete_Click(object sender, EventArgs e)
    {
        Button1a.Focus();
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("Label377a");
        lblvalue2a.Text = lblId.Text;
        Panel3a_ModalPopupExtender.Show();
    }
    protected void btnAdmissionDelete_Click(object sender, EventArgs e)
    {
        _sql = "Delete From AdmissionFormTemplate where Id='" + lblvalue2a.Text + "' and  BranchCode=" + Session["BranchCode"] + "";
        _cmd = new SqlCommand(_sql, _con);
        _con.Open();
        _cmd.ExecuteNonQuery();
        _con.Close();
        LoadAdmissionFormgrid();
        Campus camp = new Campus(); camp.msgbox(Page, msgbox2, "Record deleted successfully", "S");

    }
    protected void lnkrcptDelete_Click(object sender, EventArgs e)
    {
        Button1s.Focus();
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("Label377s");
        lblvalue2s.Text = lblId.Text;
        Panel4s_ModalPopupExtender.Show();
    }
    protected void btnrecDelete_Click(object sender, EventArgs e)
    {
        _sql = "Delete From FeereceiptTemplate where Id='" + lblvalue2s.Text + "' and  BranchCode=" + Session["BranchCode"] + "";
        _cmd = new SqlCommand(_sql, _con);
        _con.Open();
        _cmd.ExecuteNonQuery();
        _con.Close();
        LoadReceiptgrid();
        Campus camp = new Campus(); camp.msgbox(Page, msgbox3, "Record deleted successfully", "S");

    }

    protected void lnkFeeCardDelete_Click(object sender, EventArgs e)
    {
        Button4aFeecard.Focus();
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("LabelFeeCard");
        lblvalue2aFeecard.Text = lblId.Text;
        Panel2Feecard_ModalPopupExtender.Show();
    }
    protected void btnFeecardDelete_Click(object sender, EventArgs e)
    {
        _sql = "Delete From FeeCardTemplate where Id='" + lblvalue2aFeecard.Text + "' and  BranchCode=" + Session["BranchCode"] + "";
        _cmd = new SqlCommand(_sql, _con);
        _con.Open();
        _cmd.ExecuteNonQuery();
        _con.Close();
        FeeCardgrid();
        Campus camp = new Campus(); camp.msgbox(Page, msgbox4, "Record deleted successfully", "S");

    }


    protected void ddlTemplate_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTemplate.SelectedIndex == 0)
        {
            link.Visible = false;
        }
        else
        {
            string txt = "";
            link.Visible = true;
            if (ddlTemplate.SelectedValue == "Template 1")
            {
                txt= "<a href='../uploads/TCTemplates/Template1.pdf' download='" + ddlTemplate.SelectedItem.Text + "' style='color:red;'>Download <span style='text-decoration:underline;'>" + ddlTemplate.SelectedItem.Text + "</span> sample.</a>";
            }
            if (ddlTemplate.SelectedValue == "Template 2")
            {
                txt = "<a href='../uploads/TCTemplates/Template2.pdf' download='" + ddlTemplate.SelectedItem.Text + "' style='color:red;'>Download <span style='text-decoration:underline;'>" + ddlTemplate.SelectedItem.Text + "</span> sample.</a>";
            }
            if (ddlTemplate.SelectedValue == "Template 3")
            {
                txt = "<a href='../uploads/TCTemplates/Template3.pdf' download='" + ddlTemplate.SelectedItem.Text + "' style='color:red;'>Download <span style='text-decoration:underline;'>" + ddlTemplate.SelectedItem.Text + "</span> sample.</a>";
            }
            if (ddlTemplate.SelectedValue == "Template 3")
            {
                txt = "<a href='../uploads/TCTemplates/Template4.pdf' download='" + ddlTemplate.SelectedItem.Text + "' style='color:red;'>Download <span style='text-decoration:underline;'>" + ddlTemplate.SelectedItem.Text + "</span> sample.</a>";
            }
            link.InnerHtml = txt.ToString();
        }
    }
    protected void ddlTemplateReceipt_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTemplateReceipt.SelectedIndex == 0)
        {
            linkAdmissions.Visible = false;
        }
        else
        {
            string txt = "";
            linkAdmissions.Visible = true;
            if (ddlTemplateReceipt.SelectedValue == "Template 1")
            {
                txt = "<a href='../uploads/ReceiptFormTemplates/Template1.pdf' download='" + ddlTemplateReceipt.SelectedItem.Text + "' style='color:red;'>Download <span style='text-decoration:underline;'>" + ddlTemplateReceipt.SelectedItem.Text + "</span> sample.</a>";
            }
            if (ddlTemplateReceipt.SelectedValue == "Template 2")
            {
                txt = "<a href='../uploads/ReceiptFormTemplates/Template2.pdf' download='" + ddlTemplateReceipt.SelectedItem.Text + "' style='color:red;'>Download <span style='text-decoration:underline;'>" + ddlTemplateReceipt.SelectedItem.Text + "</span> sample.</a>";
            }
            linkAdmissions.InnerHtml = txt.ToString();
        }
    }
    protected void ddlTemplateAdmission_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTemplateAdmission.SelectedIndex == 0)
        {
            linkAdmissions.Visible = false;
        }
        else
        {
            string txt = "";
            linkAdmissions.Visible = true;
            if (ddlTemplateAdmission.SelectedValue == "Template 1")
            {
                txt = "<a href='../uploads/AdmissionFormTemplates/Template1.pdf' download='" + ddlTemplateAdmission.SelectedItem.Text + "' style='color:red;'>Download <span style='text-decoration:underline;'>" + ddlTemplateAdmission.SelectedItem.Text + "</span> sample.</a>";
            }
            if (ddlTemplateAdmission.SelectedValue == "Template 2")
            {
                txt = "<a href='../uploads/AdmissionFormTemplates/Template2.pdf' download='" + ddlTemplateAdmission.SelectedItem.Text + "' style='color:red;'>Download <span style='text-decoration:underline;'>" + ddlTemplateAdmission.SelectedItem.Text + "</span> sample.</a>";
            }
            linkAdmissions.InnerHtml = txt.ToString();
        }
    }

    protected void ddlTemplateFeeCard_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTemplateFeeCard.SelectedIndex == 0)
        {
            linkFeeCards.Visible = false;
        }
        else
        {
            string txt = "";
            linkFeeCards.Visible = true;
            if (ddlTemplateFeeCard.SelectedValue == "Template 1")
            {
                txt = "<a href='../uploads/FeecardTemplates/Template1.png' download='" + ddlTemplateFeeCard.SelectedItem.Text + "' style='color:red;'>Download <span style='text-decoration:underline;'>" + ddlTemplateFeeCard.SelectedItem.Text + "</span> sample.</a>";
            }
            if (ddlTemplateFeeCard.SelectedValue == "Template 2")
            {
                txt = "<a href='../uploads/FeecardTemplates/Template2.png' download='" + ddlTemplateFeeCard.SelectedItem.Text + "' style='color:red;'>Download <span style='text-decoration:underline;'>" + ddlTemplateFeeCard.SelectedItem.Text + "</span> sample.</a>";
            }
            linkFeeCards.InnerHtml = txt.ToString();
        }
    }
}