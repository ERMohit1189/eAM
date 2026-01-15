using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class admin_AttendanceNotificationTime : Page
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
        }
    }

    private void Reset()
    {
        txtTime.Text = string.Empty;
        txtTime0.Text = string.Empty;
        txtRemark.Text = string.Empty;
        txtRemark0.Text = string.Empty;
    }
          
    private void Validation(Control cntrl)
    {
        MSG = "";

        if (txtTime.Text.Trim() == string.Empty)
        {
            MSG += "Enter Time !" + "\\n";
        }

        if (MSG != string.Empty)
        {
            new Campus().MessageBoxforUpdatePanel(MSG, cntrl);
        }
        else
        {
            SetNotificationTime(cntrl);
        }
    }

    private void SetNotificationTime(Control cntrl)
    {
        //BAL.clsNotificationTime obj = new BAL.clsNotificationTime();

        //obj.Remark = txtRemark.Text.Trim();
        //obj.TimeValue = txtTime.Text.Trim();

        //obj.IsActive = Convert.ToInt32(rblIsActive.SelectedValue) == 1 ? 1 : 0;
        //obj.BranchCode = Convert.ToInt32(Session["BranchCode"]);
        //obj.LoginName = Session["LoginName"].ToString();
        //obj.SessionName = Session["SessionName"].ToString();
        //obj.SQL = SQL;

        //MSG = new DAL().SetNotificationTime(obj);
        //if (MSG == "")
        //{
        //    MSG = SQL;
        //    Reset();
        //}

        //btnInsert.Visible = true;
        //GetNotificationTime();

        //BLL.BLLInstance.ShowMSG(MSG, cntrl, true);
    }

    private void GetNotificationTime()
    {
        //BAL.clsNotificationTime obj = new BAL.clsNotificationTime();

        //obj.A02ID = -1;
        //obj.IsActive = -1;
        //obj.TimeValue = "";

        //dt = null;
        //dt = new DAL().GetNotificationTime(obj);

        //if (dt != null && dt.Rows.Count > 0)
        //{
        //    dt = new BLL().GetSerialNo(ref dt, "SrNo");
        //    gvTime.DataSource = dt;
        //}
        //else
        //{
        //    gvTime.DataSource = null;
        //}
        //gvTime.DataBind();
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
        GridViewRow Row = (GridViewRow)(sender as LinkButton).Parent.Parent;
        LinkButton btn = (LinkButton)sender;
        A02ID = Convert.ToInt16(btn.Text);
        int i = Row.RowIndex;

        txtTime0.Text = gvTime.Rows[i].Cells[1].Text.Trim();
        txtRemark0.Text = gvTime.Rows[i].Cells[2].Text.Trim();
        rblIsActive0.Items.FindByText(((Label)(gvTime.Rows[i].FindControl("IsActive"))).Text).Selected = true;

        Panel1_ModalPopupExtender.Show();
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
        //SQL = "D";
        //BAL.clsNotificationTime obj = new BAL.clsNotificationTime();
        //obj.SQL = SQL;
        //obj.A02ID = A02ID;  
        //MSG = new DAL().SetNotificationTime(obj);

        //if (MSG == string.Empty)
        //{
        //    MSG = SQL;
        //    BLL.BLLInstance.ShowMSG(MSG, cntrl, true);
        //} 
        //else 
        //    BLL.BLLInstance.ShowMSG(MSG, cntrl, true);
    }

    protected void btnNo_Click(object sender, EventArgs e)
    { 
         
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        MSG = "";
        SQL = "U";

        if (txtTime0.Text.Trim() == string.Empty)
        {
            MSG += "Enter Time !" + "\\n";
        }

        if (!string.IsNullOrEmpty(MSG))
        {
            BLL.BLLInstance.ShowMSG(MSG);
        }
        else
        {
            //BAL.clsNotificationTime obj = new BAL.clsNotificationTime();
            //obj.SQL = SQL;

            //obj.A02ID = A02ID;
            //obj.TimeValue = txtTime0.Text.Trim();
            //obj.Remark = txtRemark0.Text.Trim();
            //obj.IsActive = Convert.ToInt32(rblIsActive0.SelectedValue);

            //MSG = new DAL().SetNotificationTime(obj);

            //if (MSG == "")
            //{
            //    MSG = SQL;
            //    Reset();
            //}

            //GetNotificationTime();

            //BLL.BLLInstance.ShowMSG(MSG, Button3, true);
        }
    }
    protected void rblIsExam_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}