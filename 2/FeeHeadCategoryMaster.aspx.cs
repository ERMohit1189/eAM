using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_FeeHeadCategoryMaster : Page
{
#pragma warning disable 169
    string sql = "";
#pragma warning restore 169
    BAL.FeeHeadCategoryMaster objBal = new BAL.FeeHeadCategoryMaster();
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((string)Session["LoginName"] == "" || (string)Session["BranchCode"] == "" || (string)Session["SessionName"] == "" || Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file

        if (!IsPostBack)
        {
            loadGrid(table1);
        }
    }

    private void loadGrid(Control ctrl)
    {
        Tuple<string, DataTable> tuple;
        objBal.Queryfor = "S";
        objBal.SessionName = Session["SessionName"].ToString();
        objBal.BranchCode = Convert.ToInt16(Session["BranchCode"].ToString());
        tuple = DAL.objDal.FeeHeadCategoryMaster(objBal);
        Repeater1.DataSource = tuple.Item2;
        Repeater1.DataBind();
        BAL.objBal.ClearControls(ctrl);
    }

    private void Insert(Control ctrl)
    {
        if (txtFeeHeadCategory.Text.Trim() != string.Empty)
        {
            string msg;
            Tuple<string, DataTable> tuple;
            objBal.Queryfor = "I";
            objBal.FeeHeadCategory = txtFeeHeadCategory.Text.Trim();
            objBal.Remark = txtRemark.Text.Trim();
            objBal.SessionName = Session["SessionName"].ToString();
            objBal.BranchCode = Convert.ToInt16(Session["BranchCode"].ToString());
            objBal.LoginName = Session["LoginName"].ToString();
            tuple = DAL.objDal.FeeHeadCategoryMaster(objBal);
            msg = tuple.Item1;
            if (msg != "")
            {
                //BAL.objBal.MessageBoxforUpdatePanel(objBal.MessageType(msg, new Control(), new BAL.textBoxList()), ctrl);
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox,objBal.MessageType(msg, new Control(), new BAL.textBoxList()) , "S");       

            }
            loadGrid(table1);
            txtFeeHeadCategory.Style.Add("border", "1px solid #CCC");
            txtFeeHeadCategory.Focus();
        }
        else
        {
            txtFeeHeadCategory.Style.Add("border", "1px solid red");
            txtFeeHeadCategory.Focus();
        }
    }    
    protected void lnkSubmit_Click(object sender, EventArgs e)
    {
        Page.Validate();
        if (!Page.IsValid)
        {
            return;
        }
        else
        {
            Insert(lnkSubmit);
        }
        
        
    }
    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        RepeaterItem currentItem = (RepeaterItem)lnk.NamingContainer;
        Label lblId = (Label)currentItem.FindControl("lblId");
        Session["EditId"] = lblId.Text.Trim();
        Tuple<string, DataTable> tuple;
        objBal.Queryfor = "S";
        objBal.SessionName = Session["SessionName"].ToString();
        objBal.BranchCode = Convert.ToInt16(Session["BranchCode"].ToString());
        tuple = DAL.objDal.FeeHeadCategoryMaster(objBal);
        DataView dv = new DataView(tuple.Item2);
        dv.RowFilter = "Id=" + Session["EditId"];
        txtFeeHeadCategoryPanel.Text = dv[0][1].ToString();
        txtRemarkPanel.Text = dv[0][2].ToString();
        Panel1_ModalPopupExtender.Show();
        
    }

    private void Update(Control ctrl)
    {
        if (txtFeeHeadCategoryPanel.Text.Trim() != string.Empty)
        {
            string msg;
            Tuple<string, DataTable> tuple;
            objBal.Queryfor = "U";
            objBal.id = Convert.ToInt16(Session["EditId"].ToString());
            objBal.FeeHeadCategory = txtFeeHeadCategoryPanel.Text.Trim();
            objBal.Remark = txtRemarkPanel.Text.Trim();
            objBal.SessionName = Session["SessionName"].ToString();
            objBal.BranchCode = Convert.ToInt16(Session["BranchCode"].ToString());
            objBal.LoginName = Session["LoginName"].ToString();
            tuple = DAL.objDal.FeeHeadCategoryMaster(objBal);
            msg = tuple.Item1;
            if (msg != "")
            {
                //BAL.objBal.MessageBoxforUpdatePanel(objBal.MessageType(msg, new Control(), new BAL.textBoxList()), ctrl);
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, objBal.MessageType(msg, new Control(), new BAL.textBoxList()), "S");       

            }
            loadGrid(Panel1);
            Session["EditId"] = 0;
            txtFeeHeadCategoryPanel.Style.Add("border", "1px solid #CCC");
            txtFeeHeadCategory.Focus();
        }
        else
        {
            Panel1_ModalPopupExtender.Show();
            txtFeeHeadCategoryPanel.Style.Add("border", "1px solid red");
            txtFeeHeadCategory.Focus();
        }
    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        lnkNo.Focus();
        LinkButton lnk = (LinkButton)sender;
        RepeaterItem currentItem = (RepeaterItem)lnk.NamingContainer;
        Label lblId = (Label)currentItem.FindControl("lblId");
        Session["DeleteId"] = lblId.Text.Trim();
        Panel2_ModalPopupExtender.Show();
    }

    private void Delete(Control ctrl)
    {
        string msg;
        Tuple<string, DataTable> tuple;
        objBal.Queryfor = "D";
        objBal.id = Convert.ToInt16(Session["DeleteId"].ToString());
        objBal.FeeHeadCategory = txtFeeHeadCategory.Text.Trim();
        objBal.Remark = txtRemark.Text.Trim();
        objBal.SessionName = Session["SessionName"].ToString();
        objBal.BranchCode = Convert.ToInt16(Session["BranchCode"].ToString());
        objBal.LoginName = Session["LoginName"].ToString();
        tuple = DAL.objDal.FeeHeadCategoryMaster(objBal);
        msg = tuple.Item1;
        if (msg != "")
        {
            //BAL.objBal.MessageBoxforUpdatePanel(objBal.MessageType(msg,new Control(),new BAL.textBoxList()),ctrl);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, objBal.MessageType(msg, new Control(), new BAL.textBoxList()), "S");       

        }
        loadGrid(Panel2);
        Session["DeleteId"] = 0;
        txtFeeHeadCategory.Focus();
    }
    protected void lnkUpdate_Click(object sender, EventArgs e)
    {
        Update(lnkUpdate);
    }
    protected void lnkYes_Click(object sender, EventArgs e)
    {
        Delete(lnkYes);
    }
}