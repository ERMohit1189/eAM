using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class admin_SetStudentAttendenceRange_ng : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    SqlCommand cmd = new SqlCommand();
    Campus oo = new Campus();
    string sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Campus camp = new Campus(); camp.LoadLoader(loader); 
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (!IsPostBack)
        {
            loadClass();

            
        }

    }
    public void loadClass()
    {
        BLL.BLLInstance.loadClass(drpClass, Session["SessionName"].ToString());
        setRadiobuttonlist();
    }

    public void loadEval()
    {
        sql = "select id,term from master_examterms where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"] + "";
        oo.FillDropDown_withValue(sql, drpEval, "term", "id");
    }
    
    protected void drpClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadEval();
        selectRecord();
    }
    protected void lnkSubmit_Click(object sender, EventArgs e)
    {
        insert_update_Record();
    }
    private void insert_update_Record()
    {
        string msg = "";
        string queryfor = "";
        int count = 0;
        sql = "Select Count(EvalName) count from SetAttendenceRange_ng where ClassId='" + drpClass.SelectedValue.ToString() + "'";
        sql = sql + " and EvalName='" + drpEval.SelectedValue.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"] + "";

        int.TryParse(BAL.objBal.ReturnTag(sql, "count"), out count);
        if (count > 0)
        {
            queryfor = "U";
        }
        else
        {
            queryfor = "I";
        }

        List<SqlParameter> param = new List<SqlParameter>();

        param.Add(new SqlParameter("@QueryFor", queryfor));
        param.Add(new SqlParameter("@Classid", drpClass.SelectedValue.ToString()));
        param.Add(new SqlParameter("@EvalName", drpEval.SelectedValue.ToString()));
        param.Add(new SqlParameter("@StartDate", txtDate1.Text.ToString()));
        param.Add(new SqlParameter("@EndDate", txtDate2.Text.ToString()));
        param.Add(new SqlParameter("@CountStart", rbcountStart.SelectedValue.ToString()));
        param.Add(new SqlParameter("@CountStartDate", txtCountStartDate.Text.Trim()));
        param.Add(new SqlParameter("@CountEnd", rbcountEnd.SelectedValue.ToString()));
        param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
        param.Add(new SqlParameter("@LoginName", Session["LoginName"].ToString()));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));

        SqlParameter para = new SqlParameter("@MSG","");
        para.Direction = ParameterDirection.Output;
        para.Size = 0x100;

        param.Add(para);

        msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("SetAttendenceRange_ngProc", param);

        if (msg == "S")
        {
            if (queryfor == "I")
            {
                //BAL.objBal.MessageBoxforUpdatePanel("Submitted Successfully.", lnkSubmit);
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted Successfully.", "S");       

            }
            else
            {
                //BAL.objBal.MessageBoxforUpdatePanel("Updated Successfully.", lnkSubmit);
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated Successfully.", "S");       

            }
        }
        else
        {
            if (queryfor == "I")
            {
                //BAL.objBal.MessageBoxforUpdatePanel("Sorry, Not Submitted!", lnkSubmit);
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, Not Submitted!", "A");       

            }
            else
            {
                //BAL.objBal.MessageBoxforUpdatePanel("Sorry, Not Updated!", lnkSubmit);
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, Not Submitted!", "A");       

            }
        }
    }

    private void selectRecord()
    {
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();

        List<SqlParameter> param = new List<SqlParameter>();

        param.Add(new SqlParameter("@QueryFor", "S"));
        param.Add(new SqlParameter("@Classid", drpClass.SelectedValue.ToString()));
        param.Add(new SqlParameter("@EvalName", drpEval.SelectedValue.ToString()));
        param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));

        ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("SetAttendenceRange_ngProc", param);

        if (ds.Tables.Count > 0)
        {
            dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                DateTime dt1 = new DateTime();
                DateTime dt2 = new DateTime();
                dt1 = Convert.ToDateTime(dt.Rows[0]["StartDate"].ToString());
                txtDate1.Text = dt1.ToString("dd-MMM-yyyy");
                dt2 = Convert.ToDateTime(dt.Rows[0]["EndDate"].ToString());
                txtDate2.Text = dt2.ToString("dd-MMM-yyyy");
                rbcountStart.SelectedValue = dt.Rows[0]["CountStart"].ToString();
                DateTime dt3 = new DateTime();
                dt3 = Convert.ToDateTime(dt.Rows[0]["CountStartDate"].ToString());
                txtCountStartDate.Text = dt3.ToString("dd-MMM-yyyy");
                rbcountEnd.SelectedValue = dt.Rows[0]["CountEnd"].ToString();

                lnkSubmit.Text = "Update";
            }
            else
            {
                lnkSubmit.Text = "Submit";
                txtDate1.Text = "";
                txtDate2.Text = "";
                BLL.BLLInstance.setDefaultSelectedIndex(rbcountStart, -1);
                txtCountStartDate.Text = "";
                BLL.BLLInstance.setDefaultSelectedIndex(rbcountEnd, -1);
            }
        }
        else
        {
            lnkSubmit.Text = "Submit";
            txtDate1.Text = "";
            txtDate2.Text = "";
            BLL.BLLInstance.setDefaultSelectedIndex(rbcountStart, -1);
            txtCountStartDate.Text = "";
            BLL.BLLInstance.setDefaultSelectedIndex(rbcountEnd, -1);
        }
    }

    protected void rbcountStart_SelectedIndexChanged(object sender, EventArgs e)
    {
        getCountEndDate();
    }

    private void getCountEndDate()
    {
        txtCountStartDate.CssClass = "form-control-blue validatetxt";
        txtCountStartDate.Enabled = false;
        int count = 0;
        DateTime dt;
        string fromdate;
        if (rbcountStart.SelectedValue == "1")
        {
            sql = "Select FromDate from SessionMaster where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"] + "";
            fromdate= BAL.objBal.ReturnTag(sql, "FromDate");
            if (fromdate != string.Empty)
            {
                dt = new DateTime();
                dt = Convert.ToDateTime(fromdate);
                txtCountStartDate.Text = dt.ToString("dd-MMM-yyyy");
            }
            else
            {
                txtCountStartDate.Text = "";
            }
        }
        else if (rbcountStart.SelectedValue == "2")
        {
            int previousevalindex = 0;
            for (int i = drpEval.SelectedIndex-1; i >= 0; i--)
            {
                sql = "Select Count(EvalName) count from SetAttendenceRange_ng where ClassId='" + drpClass.SelectedValue.ToString() + "' and EvalName='" + drpEval.Items[i].Value.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"] + "";
                int.TryParse(BAL.objBal.ReturnTag(sql, "count"), out count);
                if (count > 0)
                {
                    previousevalindex = i;
                    break;
                }
            }
            sql = "Select CASE WHEN CountEnd=1 THEN StartDate-1 ELSE EndDate+1 END COUNTENDDATE from SetAttendenceRange_ng where ";
            sql = sql + " ClassId='"+drpClass.SelectedValue.ToString()+"' and EvalName='"+drpEval.Items[previousevalindex].Value+"' ";
            sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"] + "";
            fromdate = BAL.objBal.ReturnTag(sql, "COUNTENDDATE");
            if (fromdate != string.Empty)
            {
                dt = new DateTime();
                dt = Convert.ToDateTime(fromdate);
                txtCountStartDate.Text = dt.ToString("dd-MMM-yyyy");
            }
            else
            {
                txtCountStartDate.Text = "";
            }
        }
        else if (rbcountStart.SelectedValue == "3")
        {
            txtCountStartDate.Text = "";
            txtCountStartDate.Enabled = true;
            txtCountStartDate.CssClass = "form-control-blue datepicker-normal validatetxt";
        }
    }

    private void setRadiobuttonlist()
    {
        int count = 0;
        for(int i=drpEval.SelectedIndex-1;i>=0;i--)
        {
            sql = "Select Count(EvalName) count from SetAttendenceRange_ng where ClassId='" + drpClass.SelectedValue.ToString() + "' and BranchCode=" + Session["BranchCode"] + " and EvalName='" + drpEval.SelectedValue.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "'";
            int.TryParse(BAL.objBal.ReturnTag(sql, "count"), out count);
            if (count > 0)
            {
                break;
            }
        }
        if (count == 0)
        {
            rbcountStart.Items[1].Enabled = false;
        }
        else
        {
            rbcountStart.Items[1].Enabled = true;
        }
    }
    protected void drpEval_SelectedIndexChanged(object sender, EventArgs e)
    {
        setRadiobuttonlist();
        selectRecord();
    }
}