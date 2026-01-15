using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class admin_AttendanceShift : Page
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
            btnInsert.Visible = true;
            btnUpdate.Visible = false;
        }    
    }              
                            
    private void Reset() 
    {
        btnUpdate.Visible = false;
        btnInsert.Visible = true;

        txtAttendanceShift.Text = string.Empty;
        txtAttendanceShift0.Text = string.Empty;
        txtFromTimeLunch.Text = string.Empty;
        txtFromTimeLunch0.Text = string.Empty;
        txtFromTimeShift.Text = string.Empty;
        txtFromTimeShift0.Text = string.Empty;
        txtGraceTime.Text = string.Empty;
        txtGraceTime0.Text = string.Empty;
        txtLunchTime.Text = string.Empty;
        txtLunchTime0.Text = string.Empty;
        txtNotificationTime.Text = string.Empty;
        txtNotificationTime0.Text = string.Empty;
        txtShiftTime.Text = string.Empty;
        txtShiftTime0.Text = string.Empty;
        txtShortName.Text = string.Empty;
        txtShortName0.Text = string.Empty;
        txtToTimeLunch.Text = string.Empty;
        txtToTimeLunch0.Text = string.Empty;
        txtToTimeShift.Text = string.Empty;
        txtToTimeShift0.Text = string.Empty;

        cblNotificationType.ClearSelection();
    }  
    
    private void Validation(Control cntrl)
    {
        MSG = "";

        if (txtShiftTime.Text.Trim() == string.Empty)
        {
            MSG += "Enter Subject Paper Count !" + "";
        }

        if (MSG != string.Empty)
        {
            new Campus().MessageBoxforUpdatePanel(MSG, cntrl);
        }
        else
        {
            SetSubjectPaper(cntrl);
        }
    }

    private void SetSubjectPaper(Control cntrl)
    {
        BAL.clsNotificationDate obj = new BAL.clsNotificationDate();
        obj.BranchCode = Convert.ToInt32(Session["BranchCode"]);
        obj.LoginName = Session["LoginName"].ToString();
        obj.SessionName = Session["SessionName"].ToString();
        obj.SQL = SQL;

        obj.ShiftName = txtAttendanceShift.Text.Trim();
        obj.ShotName = txtShortName.Text.Trim();
        obj.FromTimeShift = txtFromTimeShift.Text.Trim();
        obj.ToTimeShift = txtToTimeShift.Text.Trim();
        obj.ShiftTime = txtShiftTime.Text.Trim();
        obj.FromTimeLunch = txtFromTimeLunch.Text.Trim();
        obj.ToTimeLunch = txtToTimeLunch.Text.Trim();
        obj.LunchTime = txtLunchTime.Text.Trim();
        obj.GraceTimeInMinute = Convert.ToInt32(txtGraceTime.Text.Trim());
        obj.IsEarlyPunchAllowed = Convert.ToInt32(rblIsEarlyPunch.SelectedValue);
        obj.IsAutoSendNotification = 1;
        obj.NotificationTime = txtNotificationTime.Text.Trim();

        MSG = new DAL().SetAttendanceShift(obj);
        if (MSG == "")
        {
            MSG = SQL;
            Reset();
        }  

        btnInsert.Visible = true;
        btnUpdate.Visible = false;
        GetSubjectPaperList();

        BLL.BLLInstance.ShowMSG(MSG);
    }

    private void GetSubjectPaperList()
    {
        dt = null;
        BAL.clsNotificationDate obj = new BAL.clsNotificationDate();

        dt = DAL.DALInstance.GetAttendanceShift(obj);
        if (dt != null && dt.Rows.Count > 0)
        {    
            dt = new BLL().GetSerialNo(ref dt, "SrNo");
            gvAttendanceShift.DataSource = dt;
        }
        else
        {
            gvAttendanceShift.DataSource = null;
        }
        gvAttendanceShift.DataBind();
    }
    protected void btnInsert_Click(object sender, EventArgs e)
    {
        SQL = "I";
        Validation(btnInsert);
    }
    protected void btnUpdate_Click1(object sender, EventArgs e)
    {
        SQL = "U";
        Validation(btnUpdate);
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        Reset();
    }

    protected void lbtnEdit_Click(object sender, EventArgs e)
    {

        GridViewRow gvr = (GridViewRow)(sender as Control).Parent.Parent;

        GridViewRow Row = (GridViewRow)(sender as LinkButton).Parent.Parent;
        LinkButton btn = (LinkButton)sender;
        A02ID = Convert.ToInt16(btn.Text);
        int i = Row.RowIndex;

        //dt = new DAL().GetSubjectGroup(-1, Convert.ToInt32(lblClassID0.Text), gvSubjectPaperList.Rows[i].Cells[7].Text.Trim(), Session["SessionName"].ToString(), -1, -1, Convert.ToInt16(lblBranchID0.Text));
        //if (dt != null && dt.Rows.Count > 0)
        //{
        //    BLL.FillDropDown(ddlSubject0, dt, "SubjectGroupName", "ID", 'S');
        //}

        //ddlSubject0.Items.FindByText(gvSubjectPaperList.Rows[i].Cells[2].Text.Trim()).Selected = true;
        //txtSubjectPaperPanel.Text = gvSubjectPaperList.Rows[i].Cells[1].Text.Trim();
        //txtWeekPeriodCountPanel.Text = gvSubjectPaperList.Rows[i].Cells[3].Text.Trim();
        //rblIsFroExamPanel.Items.FindByText(gvSubjectPaperList.Rows[i].Cells[4].Text.Trim()).Selected = true;

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
        DeleteRecord();
        GetSubjectPaperList();
    }
    public void DeleteRecord()
    {
        BAL.clsSubjectPaperMaster obj = new BAL.clsSubjectPaperMaster();
        obj.SQL = "D";
        obj.S02ID = A02ID;
        MSG = new DAL().SetSubjectPaperMaster(obj);
    }
    protected void btnNo_Click(object sender, EventArgs e)
    {

    }
  
    protected void Button3_Click(object sender, EventArgs e)
    {
        MSG = "";

        //if (!string.IsNullOrEmpty(MSG))
        //{
        //    BLL.BLLInstance.ShowMSG(MSG, Button3, false);
        //}
        //else
        //{
        //    SQL = "U";

        //    BAL.clsSubjectPaperMaster obj = new BAL.clsSubjectPaperMaster();


        //    MSG = new DAL().SetSubjectPaperMaster(obj);
        //    if (MSG == "")
        //    {
        //        MSG = SQL;
        //        Reset();
        //    }

        //    btnInsert.Visible = true;
        //    btnUpdate.Visible = false;
        //    GetSubjectPaperList();

        //    BLL.BLLInstance.ShowMSG(MSG, Button3, true);
        //}
    }

    protected void rblIsExam_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}