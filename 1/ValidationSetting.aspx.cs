using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class administrator_ValidationSetting : System.Web.UI.Page
{
    private readonly Campus _oo;
    private readonly BAL.GenralInfo _objBal = new BAL.GenralInfo();
    private string _sql = String.Empty;
    public administrator_ValidationSetting()
    {
        _oo = new Campus();
    }
    protected void Page_PreInIt(object sender, EventArgs e)
    {
        if (Session["Logintype"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (Session["Logintype"].ToString() == "SuperAdmin")
        {
            MasterPageFile = "~/50/sadminRootManager.master";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //if ((string)Session["LoginName"] == "" || (string)Session["BranchCode"] == "" || (string)Session["SessionName"] == "" || Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        //{
        //    Response.Redirect("default.aspx");
        //}
        Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file

        if (!IsPostBack)
        {
            LoadCourse();
            LoadData();
        }
    }
    protected void LoadCourse()
    {
        _sql = "Select ID,Controls  from ValidationSetting";
        _oo.FillDropDown_withValue(_sql, ddlControlType, "Controls", "ID");
        _oo.FillDropDown_withValue(_sql, DropDownList1, "Controls", "ID");
        ddlControlType.Items.Insert(0, new ListItem("<--Select-->", "0"));
        DropDownList1.Items.Insert(0, new ListItem("<--Select-->", "0"));
    }
    protected void LoadData()
    {
        _sql = "Select ID,Controls,(Case when IsApply=1 then 'Yes' else 'No' end)Status,IsApply  from ValidationSetting";
        GridView1.DataSource = _oo.GridFill(_sql);
        GridView1.DataBind();
    }
    public void BindDropDown()
    {
        DropDownList2.Items.Clear();
        DropDownList2.Items.Add(new ListItem("Yes", "1"));
        DropDownList2.Items.Add(new ListItem("No", "0"));
    }
    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        GridViewRow currentrow = (GridViewRow)lnk.NamingContainer;
        Label lblId = (Label)currentrow.FindControl("lblId");
        _sql = "Select ID,Controls,(Case when IsApply=1 then 'Yes' else 'No' end)Status,IsApply  from ValidationSetting";
        _sql += " where ID='" + lblId.Text.Trim() + "' ";
        DropDownList1.SelectedValue = _oo.ReturnTag(_sql, "ID");
       // DropDownList2.SelectedValue = _oo.ReturnTag(_sql, "IsApply");
        DropDownList2.Items.Clear();
        BindDropDown();
        string IsApply = _oo.ReturnTag(_sql, "IsApply");

        if (IsApply == "True")
        {
            DropDownList2.SelectedValue = "1";
        }
        else
        {
            DropDownList2.SelectedValue = "0";
        }

        Panel1_ModalPopupExtender.Show();
    }
    protected void UpdateData()
    {
        if (DropDownList1.SelectedValue != "0")
        {
            string msg = string.Empty;
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@Id", DropDownList1.SelectedValue));
            param.Add(new SqlParameter("@IsApply", DropDownList2.SelectedValue));
            param.Add(new SqlParameter("@LoginName", Session["LoginName"].ToString()));

            SqlParameter para = new SqlParameter("@Msg", "");
            para.Direction = ParameterDirection.Output;
            para.Size = 0x100;
            param.Add(para);

            msg = new DLL().Sp_Insert_Update_Delete_usingExecuteNonQuery("ValidationSettingProc", param);
            Campus camp = new Campus();
            if (msg == "S")
            {
                camp.msgbox(Page, msgbox, "Submitted successfully.", "S");
            }
            LoadData();
        }
        else
        {
            Campus camp = new Campus();

            camp.msgbox(Page, msgbox, "Please Select Control Name", "A");

        }
    }
    protected void lnkSubmit_Click(object sender, EventArgs e)
    {
        if(ddlControlType.SelectedValue!="0")
        {
            string msg = string.Empty;
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@Id", ddlControlType.SelectedValue));
            param.Add(new SqlParameter("@IsApply", ddlStatus.SelectedValue));
            param.Add(new SqlParameter("@LoginName", Session["LoginName"].ToString()));

            SqlParameter para = new SqlParameter("@Msg", "");
            para.Direction = ParameterDirection.Output;
            para.Size = 0x100;
            param.Add(para);

            msg = new DLL().Sp_Insert_Update_Delete_usingExecuteNonQuery("ValidationSettingProc", param);
            Campus camp = new Campus();
            if (msg == "S")
            {
                camp.msgbox(Page, msgbox, "Submitted successfully.", "S");
            }
            LoadData();
        }
        else
        {
            Campus camp = new Campus();
         
                camp.msgbox(Page, msgbox, "Please Select Control Name", "A");
          
        }
      
    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        Panel1_ModalPopupExtender.Hide();
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        UpdateData();
    }
}