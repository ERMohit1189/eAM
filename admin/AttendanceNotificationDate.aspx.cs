using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class admin_AttendanceNotificationDate : Page
{
    public string MSG = "", SQL = "";
    public static int A02ID = 0;
    public DataTable dt = new DataTable();

    protected void Page_preinit(object sender, EventArgs e)
    { 
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("../default.aspx");
        }
        
    }

    protected void Page_Load(object sender, EventArgs e)
    { 
        if (!IsPostBack)
        { 
            GetNotificationTime();
            GetDate();
            GetNotificationDate();
        }
    }

    private void GetNotificationTime()
    {
        //BAL.clsNotificationTime obj = new BAL.clsNotificationTime();

        //obj.A02ID = -1;
        //obj.IsActive = 1;
        //obj.TimeValue = "";

        //dt = null;
        //dt = new DAL().GetNotificationTime(obj);

        //if (dt != null && dt.Rows.Count > 0)
        //{
        //    BLL.FillDropDown(ddlTime, dt, "TimeValue_str", "A02ID", 'S');
        //}
        //else
        //{
        //    ddlTime.Items.Clear();
        //}

    }

    private void GetDate()
    {
        dt = null;

        dt = DAL.DALInstance.GetAllMonthDates();

        if (dt != null && dt.Rows.Count > 0)
        {
            dt = new BLL().GetSerialNo(ref dt, "SrNo");
            gvAllDate.DataSource = dt;
        }
        else
        {
            gvAllDate.DataSource = null;
        }
        gvAllDate.DataBind();
    }

    private void Reset()
    {
        ddlTime.SelectedIndex = 0;
        txtRemark.Text = string.Empty;
        GetDate();
    }

    private void Validation(Control cntrl)
    {
        MSG = "";

        if (ddlTime.SelectedIndex<1)
        {
            MSG += "Select Time !" + "\\n";
        }

        if (MSG != string.Empty)
        {
            new Campus().MessageBoxforUpdatePanel(MSG, cntrl);
        }
        else
        {
            SetNotificationDate(cntrl);
        }
    }

    List<BAL.clsNotificationDate> objList;
    private void SetNotificationDate(Control cntrl)
    {
        objList = new List<BAL.clsNotificationDate>();

        BAL.clsNotificationDate obj=null; 

        for (int i = 0; i < gvAllDate.Rows.Count; i++)
        {
            CheckBox cb = (CheckBox)gvAllDate.Rows[i].FindControl("cbSingle");

            if(cb.Checked)
            {
                obj = new BAL.clsNotificationDate();
                obj.A02ID = Convert.ToInt32(ddlTime.SelectedValue);
                obj.Remark = txtRemark.Text.Trim();
                obj.DateValue = Convert.ToDateTime(gvAllDate.Rows[i].Cells[1].Text);
                obj.IsActive = Convert.ToInt32(rblIsActive.SelectedValue) == 1 ? 1 : 0;

                obj.SQL = SQL;
                objList.Add(obj); 
            }    
        }
            
        if (objList!=null)  
        { 
            MSG = new DAL().SetNotificationDate(objList);
            if (MSG == "")
            {
                MSG = SQL;
                Reset();
            }
        }
        else
        {
            BLL.BLLInstance.ShowMSG("Select Date !");
        }

        btnInsert.Visible = true;
        btnUpdate.Visible = false;
        GetNotificationDate();

        BLL.BLLInstance.ShowMSG(MSG);
    }

    private void GetNotificationDate()
    {
        BAL.clsNotificationDate obj = new BAL.clsNotificationDate();

        obj.A02ID = -1;
        obj.A03ID = -1;
        obj.IsActive = -1;
        obj.DateValue = Convert.ToDateTime("1 Jan 1900");

        dt = null;
        dt = new DAL().GetNotificationDate(obj);

        if (dt != null && dt.Rows.Count > 0)
        {
            dt = new BLL().GetSerialNo(ref dt, "SrNo");
            gvDate.DataSource = dt;
        }
        else
        { 
            gvDate.DataSource = null;
        }
        gvDate.DataBind();
    } 

    protected void btnInsert_Click(object sender, EventArgs e)
    {
        SQL = "I";
        Validation(btnInsert);
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        Reset();
    }

    protected void lbtnEdit_Click(object sender, EventArgs e)
    {
        btnInsert.Visible = false;
        btnUpdate.Visible = true;

        GridViewRow Row = (GridViewRow)(sender as LinkButton).Parent.Parent;
        LinkButton btn = (LinkButton)sender;
        A02ID = Convert.ToInt16(btn.Text);
        int i = Row.RowIndex;

        ddlTime.SelectedValue = gvDate.Rows[i].Cells[1].Text.Trim();
        txtRemark.Text = gvDate.Rows[i].Cells[3].Text.Trim();
        rblIsActive.Items.FindByText(((Label)(gvDate.Rows[i].FindControl("IsActive"))).Text).Selected = true;
    }

    protected void lbtnDelete_Click(object sender, EventArgs e)
    {
        LinkButton lbtn = (LinkButton)sender;
        A02ID = Convert.ToInt16(lbtn.Text);
        mpeDelete.Show();
    }

    protected void btnYes_Click(object sender, EventArgs e)
    {
        DeleteRecord(btnYes);
        GetNotificationTime();
    }

    public void DeleteRecord(Control cntrl)
    {
        SQL = "D";
        BAL.clsNotificationDate obj = new BAL.clsNotificationDate();
        obj.SQL = SQL;
        obj.A02ID = A02ID;
        
        //MSG = new DAL().SetNotificationTime(obj);   

        if (MSG == string.Empty)
        {          
            MSG = SQL;  
            BLL.BLLInstance.ShowMSG(MSG);
        }  
        else
            BLL.BLLInstance.ShowMSG(MSG);
    }

    protected void btnNo_Click(object sender, EventArgs e)
    {

    } 
 
    protected void cbAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox ChkBoxHeader = (CheckBox)gvAllDate.HeaderRow.FindControl("cbAll");
        foreach (GridViewRow row in gvAllDate.Rows)
        {
            CheckBox ChkBoxRows = (CheckBox)row.FindControl("cbSingle");
            if (ChkBoxHeader.Checked == true)
            {
                ChkBoxRows.Checked = true;
            }
            else
            {
                ChkBoxRows.Checked = false;
            }
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        SQL = "U";
        Validation(btnUpdate);
    }
}