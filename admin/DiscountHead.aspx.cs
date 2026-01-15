using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_DiscountHead : Page
{
    string sql = "";
    BAL.Set_DiscountHead objBAL = new BAL.Set_DiscountHead();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        if (!IsPostBack)
        {
            Select();
        }
    }
    private void Select()
    {        
        string msg = "";
        DataTable dt = null;
        objBAL.Queryfor = "S";
        objBAL.ParentHeadvalue = RadioButtonList1.SelectedValue.ToString();
        objBAL.SessionName = Session["SessionName"].ToString();
        objBAL.BranchCode = Convert.ToInt16(Session["BranchCode"].ToString());
        // ReSharper disable once ExpressionIsAlwaysNull
        Tuple<string, DataTable> tuple = new Tuple<string, DataTable>(msg, dt);
        tuple = DAL.objDal.Get_SetDiscountHead(objBAL);
        GridView1.DataSource = tuple.Item2;
        GridView1.DataBind();
        if (msg != string.Empty)
        {
            BAL.objBal.MessageBoxforUpdatePanel(objBAL.MessageType(msg, new Control(), objBAL), lnkSubmit);
        }
        BAL.objBal.ClearControls(mainDiv);
        lblId.Text = "0";
        TextBox1.Focus(); TextBox3.Text = "1";
        if (RadioButtonList1.SelectedIndex == 0)
        {
            row3.Visible = true;
        }
        else
        {
            row3.Visible = false;
        }
    }

    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Select();
    }

    protected void lnkSubmit_Click(object sender, EventArgs e)
    {
        Insert();
        Select();
    }
    private void Insert()
    {
        string msg = "";
        int flag;
        objBAL.Queryfor = "I";
        objBAL.NoofSibling = int.TryParse(TextBox3.Text, out flag) ? flag : 1;
        objBAL.ParentHeadvalue = RadioButtonList1.SelectedValue.ToString();
        objBAL.HeadName = TextBox1.Text.Trim();
        objBAL.Remark = TextBox2.Text.Trim();
        objBAL.SessionName = Session["SessionName"].ToString();
        objBAL.LoginName = Session["LoginName"].ToString();
        objBAL.BranchCode = Convert.ToInt16(Session["BranchCode"].ToString());

        msg = DAL.objDal.Set_DiscountHead(objBAL);

        BAL.objBal.MessageBoxforUpdatePanel(objBAL.MessageType(msg, new Control(), objBAL), lnkSubmit);
        
    }

    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        GridViewRow currntrow=(GridViewRow)lnk.NamingContainer;
        Label Label3 = (Label)currntrow.FindControl("Label3");
        lblId.Text = Label3.Text.Trim();

        sql = "Select ParentHeadvalue,HeadName,NoofSibling,Remark from DiscountHead where id=" + lblId.Text;

        PanelRadioButtonList1.SelectedValue = BAL.objBal.ReturnTag(sql, "ParentHeadvalue");
        PanelTextBox1.Text = BAL.objBal.ReturnTag(sql, "HeadName");
        PanelTextBox2.Text = BAL.objBal.ReturnTag(sql, "Remark");
        string noofsibling=BAL.objBal.ReturnTag(sql, "NoofSibling");
        if (!string.IsNullOrEmpty(noofsibling))
        {
            PanelTextBox3.Text = BAL.objBal.ReturnTag(sql, "NoofSibling");
        }
        else
        {
            PanelTextBox3.Text = "1";
        }
        Panel1_ModalPopupExtender.Show();
    }
    protected void lnkUpdate_Click(object sender, EventArgs e)
    {
        Update();
        Select();
    }
    private void Update()
    {
        string msg = "";
        int flag;
        objBAL.id = int.Parse(lblId.Text);
        objBAL.Queryfor = "U";
        objBAL.NoofSibling = int.TryParse(PanelTextBox3.Text, out flag) ? flag : 1;
        objBAL.ParentHeadvalue = PanelRadioButtonList1.SelectedValue.ToString();
        objBAL.HeadName = PanelTextBox1.Text.Trim();
        objBAL.Remark = PanelTextBox2.Text.Trim();
        objBAL.SessionName = Session["SessionName"].ToString();
        objBAL.LoginName = Session["LoginName"].ToString();
        objBAL.BranchCode = Convert.ToInt16(Session["BranchCode"].ToString());

        msg = DAL.objDal.Set_DiscountHead(objBAL);

        BAL.objBal.MessageBoxforUpdatePanel(objBAL.MessageType(msg, new Control(), objBAL), lnkSubmit);
    }

    protected void lnkDeleteConfirm_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        GridViewRow currntrow = (GridViewRow)lnk.NamingContainer;
        Label Label3 = (Label)currntrow.FindControl("Label3");
        lblId.Text = Label3.Text.Trim();

        Panel2_ModalPopupExtender.Show();
    }   
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        Delete();
        Select();
    }
    private void Delete()
    {
        string msg = "";

        objBAL.id = int.Parse(lblId.Text);
        objBAL.Queryfor = "D";

        msg = DAL.objDal.Set_DiscountHead(objBAL);

        BAL.objBal.MessageBoxforUpdatePanel(objBAL.MessageType(msg, new Control(), objBAL), lnkSubmit);
    }
    protected void PanelRadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Panel1_ModalPopupExtender.Show();
        if (PanelRadioButtonList1.SelectedIndex == 0)
        {
            panelRow3.Visible = true;
        }
        else
        {
            panelRow3.Visible = false;
        }
    }
}