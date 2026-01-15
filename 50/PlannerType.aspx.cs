using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class admin_PlannerType : Page
{
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
  Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file

        if (!IsPostBack)
        {
            LoadGrid();
            txtCption.Focus();
        }
       
    }

    public void Save()
    {
        //string checkboxvalue = "0";
        //if (chkbranchs.Checked==true)
        //{
        //    checkboxvalue = "1";
        //}
        string msg = "";

        List<SqlParameter> param = new List<SqlParameter>();

        param.Add(new SqlParameter("@Action", "I"));
        param.Add(new SqlParameter("@PlannerType",txtCption.Text.Trim()));
        param.Add(new SqlParameter("@Remark", txtRemark.Text.Trim()));
        param.Add(new SqlParameter("@Color", txtColorbox.Text.Trim()));
       // param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
        //param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
      //  param.Add(new SqlParameter("@CheckBox", checkboxvalue.ToString()));
        param.Add(new SqlParameter("@LoginName", Session["LoginName"].ToString()));
      

        SqlParameter para = new SqlParameter("@Msg","");
        para.Direction = ParameterDirection.Output;
        para.Size = 0x100;

        param.Add(para);

        msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("PlannerTypeProc", param);

        if (msg == "S")
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");
            txtCption.Text = "";
            txtRemark.Text = "";
            txtColorbox.Text = "";
          //  chkbranchs.Checked = true;
            LoadGrid();
        }
        else if (msg == "DU")
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, This entry is already exists.", "A");
            txtCption.Text = "";
            txtRemark.Text = "";
            txtColorbox.Text = "";
           // chkbranchs.Checked = false;
        }
        else
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, Record not submitted.", "W");
            txtCption.Text = "";
            txtRemark.Text = "";
            txtColorbox.Text = "";
           // chkbranchs.Checked = false;
        }
    }

    public void LoadGrid()
    {       
        List<SqlParameter> param = new List<SqlParameter>();

        param.Add(new SqlParameter("@Action", "S"));
      //  param.Add(new SqlParameter("@BranchCode", Session["BranchCode"]));
        rptPlannerType.DataSource = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("PlannerTypeProc", param);
        rptPlannerType.DataBind();
        
    }
    protected void lnkSubmit_Click(object sender, EventArgs e)
    {
        Save();
    }
    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        RepeaterItem rpt = (RepeaterItem)lnk.NamingContainer;
        Label lblPlannerType = (Label)rptPlannerType.Items[rpt.ItemIndex].FindControl("lblPlannerType");
        Label lblId = (Label)rptPlannerType.Items[rpt.ItemIndex].FindControl("lblId");

        lblPlannerTypePanel.Text = lblId.Text;

        List<SqlParameter> param = new List<SqlParameter>();

        param.Add(new SqlParameter("@Action", "S"));
        param.Add(new SqlParameter("@PlannerType", lblPlannerType.Text.Trim()));
      //  param.Add(new SqlParameter("@BranchCode", Session["BranchCode"]));
        DataSet ds = new DataSet();

        ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("PlannerTypeProc", param);
        if (ds != null)
        {
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                txtCptionPanel.Text = dt.Rows[0][0].ToString(); 
                txtRemarkPanel.Text = dt.Rows[0][1].ToString();
                txtColorboxPanel.Text = dt.Rows[0][2].ToString();
                pickColor.Style.Add("color", txtColorboxPanel.Text);
            }
        }

        Panel1_ModalPopupExtender.Show();
    }

    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        lnkNo.Focus();
        LinkButton lnk = (LinkButton)sender;
        RepeaterItem rpt = (RepeaterItem)lnk.NamingContainer;
        Label lblPlannerType = (Label)rptPlannerType.Items[rpt.ItemIndex].FindControl("lblPlannerType");
        Label lblId = (Label)rptPlannerType.Items[rpt.ItemIndex].FindControl("lblId");

        lblPlannerTypePanel.Text = lblId.Text;

        Panel2_ModalPopupExtender.Show();
    }

    public void update()
    {

        string msg = "";

        List<SqlParameter> param = new List<SqlParameter>();

        param.Add(new SqlParameter("@Action", "U"));
        param.Add(new SqlParameter("@PlannerType", txtCptionPanel.Text.Trim()));
        param.Add(new SqlParameter("@Remark", txtRemarkPanel.Text.Trim()));
        param.Add(new SqlParameter("@Color", txtColorboxPanel.Text.Trim()));
        param.Add(new SqlParameter("@Id", lblPlannerTypePanel.Text.Trim()));
       // param.Add(new SqlParameter("@BranchCode", Session["BranchCode"]));

        SqlParameter para = new SqlParameter("@Msg", "");
        para.Direction = ParameterDirection.Output;
        para.Size = 0x100;

        param.Add(para);

        msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("PlannerTypeProc", param);

        if (msg == "S")
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully.", "S");
            txtCption.Text = "";
            txtRemark.Text = "";
            txtColorbox.Text = "";
            LoadGrid();
            txtCption.Focus();
        }
        else if (msg == "DU")
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, This entry is already exists.", "A");
            txtCption.Text = "";
            txtRemark.Text = "";
            txtColorbox.Text = "";
            txtCption.Focus();
            txtCption.Focus();
        }
        else
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, Record is not updated.", "W");
            txtCption.Text = "";
            txtRemark.Text = "";
            txtColorbox.Text = "";
            txtCption.Focus();
        }
        
    }

    protected void lnkUpdate_Click(object sender, EventArgs e)
    {
        update();
    }

    public void delete()
    {
        string msg = "";


        List<SqlParameter> param = new List<SqlParameter>();

        param.Add(new SqlParameter("@Action", "D"));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"]));
        param.Add(new SqlParameter("@Id", lblPlannerTypePanel.Text.Trim()));

        SqlParameter para = new SqlParameter("@Msg", "");
        para.Direction = ParameterDirection.Output;
        para.Size = 0x100;

        param.Add(para);

        msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("PlannerTypeProc", param);

        if (msg == "S")
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Deleted successfully.", "S");
            txtCption.Text = "";
            txtColorbox.Text = "";
            LoadGrid();
            txtCption.Focus();
        }
        else
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, Record is not deleted.", "W");
            txtCption.Text = "";
            txtCption.Focus();
        }
    }

    protected void lnkYes_Click(object sender, EventArgs e)
    {
        delete();
    }
}