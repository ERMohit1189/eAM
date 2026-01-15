using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class admin_SetStudentAttendenceRange : System.Web.UI.Page
{
    string sql = "";
    Campus oo = new Campus();
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
            selectRecord();
        }
    }
    public void loadClass()
    {
        BLL.BLLInstance.loadClass(drpClass, Session["SessionName"].ToString());
    }

    public void loadEval()
    {
        loadEval(drpEval, drpClass.SelectedValue.ToString(), Session["SessionName"].ToString());
    }

    public void loadEval(DropDownList drpEval, string classid, string sessionname)
    {
        sql = "Select GroupId from dt_ClassGroupMaster where SessionName='" + sessionname + "' and ClassId='" + classid + "' and BranchCode=" + Session["BranchCode"] + "";
        string groupid = BAL.objBal.ReturnTag(sql, "GroupId");
        DataTable dt = new DataTable();
        dt.Columns.Add("Eval");
        dt.Rows.Clear();
        DataRow dr = dt.NewRow();
        if (groupid == "G1")
        {
            if (Session["SessionName"].ToString() == "2015-2016" || Session["SessionName"].ToString() == "2016-2017")
            {
                dr["Eval"] = "MAY/JULY";
                dt.Rows.Add(dr);
                dr = dt.NewRow();

                dr["Eval"] = "AUG";
                dt.Rows.Add(dr);
                dr = dt.NewRow();

                dr["Eval"] = "SEPT.";
                dt.Rows.Add(dr);
                dr = dt.NewRow();

                dr["Eval"] = "DEC";
                dt.Rows.Add(dr);
                dr = dt.NewRow();

                dr["Eval"] = "JAN";
                dt.Rows.Add(dr);
                dr = dt.NewRow();

                dr["Eval"] = "FEB";
                dt.Rows.Add(dr);
            }
            else
            {
                dr["Eval"] = "TERM1";
                dt.Rows.Add(dr);
                dr = dt.NewRow();

                dr["Eval"] = "TERM2";
                dt.Rows.Add(dr);
                dr = dt.NewRow();

                dr["Eval"] = "TERM3";
                dt.Rows.Add(dr);
                dr = dt.NewRow();
            }
        }
        else if (groupid == "G2")
        {
            if(Session["SessionName"].ToString()=="2015-2016" || Session["SessionName"].ToString() == "2016-2017")
            {
                dr["Eval"] = "MAY/JULY";
                dt.Rows.Add(dr);
                dr = dt.NewRow();

                dr["Eval"] = "AUG";
                dt.Rows.Add(dr);
                dr = dt.NewRow();

                dr["Eval"] = "SEPT.";
                dt.Rows.Add(dr);
                dr = dt.NewRow();

                dr["Eval"] = "DEC";
                dt.Rows.Add(dr);
                dr = dt.NewRow();

                dr["Eval"] = "JAN";
                dt.Rows.Add(dr);
                dr = dt.NewRow();

                dr["Eval"] = "FEB";
                dt.Rows.Add(dr);
            }
            else
            {
                dr["Eval"] = "TERM1";
                dt.Rows.Add(dr);
                dr = dt.NewRow();

                dr["Eval"] = "TERM2";
                dt.Rows.Add(dr);
            }
           
        }
        else if (groupid == "G3")
        {
            dr["Eval"] = "TERM1";
            dt.Rows.Add(dr);
            dr = dt.NewRow();

            dr["Eval"] = "TERM2";
            dt.Rows.Add(dr);
        }
        else if (groupid == "G4")
        {
            dr["Eval"] = "TERM1";
            dt.Rows.Add(dr);
            dr = dt.NewRow();

            dr["Eval"] = "TERM2";
            dt.Rows.Add(dr);
        }
        else if (groupid == "G5")
        {
            dr["Eval"] = "TERM1";
            dt.Rows.Add(dr);
            dr = dt.NewRow();

            dr["Eval"] = "TERM2";
            dt.Rows.Add(dr);
        }
        else if (groupid == "G6")
        {
            if (Session["SessionName"].ToString() == "2015-2016" || Session["SessionName"].ToString() == "2016-2017")
            {
                dr["Eval"] = "FA1";
                dt.Rows.Add(dr);
                dr = dt.NewRow();
                dr["Eval"] = "FA2";
                dt.Rows.Add(dr);
                dr = dt.NewRow();

                dr["Eval"] = "SA1";
                dt.Rows.Add(dr);
                dr = dt.NewRow();

                dr["Eval"] = "FA3";
                dt.Rows.Add(dr);
                dr = dt.NewRow();

                dr["Eval"] = "FA4";
                dt.Rows.Add(dr);
                dr = dt.NewRow();

                dr["Eval"] = "SA2";
                dt.Rows.Add(dr);
            }
            else
            {
                dr["Eval"] = "TERM1";
                dt.Rows.Add(dr);
                dr = dt.NewRow();

                dr["Eval"] = "TERM2";
                dt.Rows.Add(dr);
            }
        }
        else if (groupid == "G7")
        {
            if (Session["SessionName"].ToString() == "2015-2016" || Session["SessionName"].ToString() == "2016-2017")
            {
                dr["Eval"] = "FA1";
                dt.Rows.Add(dr);
                dr = dt.NewRow();
                dr["Eval"] = "FA2";
                dt.Rows.Add(dr);
                dr = dt.NewRow();

                dr["Eval"] = "SA1";
                dt.Rows.Add(dr);
                dr = dt.NewRow();

                dr["Eval"] = "FA3";
                dt.Rows.Add(dr);
                dr = dt.NewRow();

                dr["Eval"] = "FA4";
                dt.Rows.Add(dr);
                dr = dt.NewRow();

                dr["Eval"] = "SA2";
                dt.Rows.Add(dr);
            }
            else
            {
                dr["Eval"] = "TERM1";
                dt.Rows.Add(dr);
                dr = dt.NewRow();

                dr["Eval"] = "TERM2";
                dt.Rows.Add(dr);

            }
        }
        else
        {
            BAL.objBal.MessageBoxforUpdatePanel("Please, Add Class in their related Group!", drpEval);
        }

        if (dt.Rows.Count > 0)
        {
            drpEval.DataSource = dt;
            drpEval.DataTextField = "Eval";
            drpEval.DataValueField = "Eval";
            drpEval.DataBind();
        }
    }

    protected void drpClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadEval();
        selectRecord();
    }
    protected void lnkSubmit_Click(object sender, EventArgs e)
    {
        string msg = "";
        sql = "select SessionName from SetAttendenceRange  where SessionName = '" + Session["SessionName"].ToString() + "' and BranchCode = " + Session["BranchCode"] + " and Classid=" + drpClass.SelectedValue.Trim() + " and EvalName='" + drpEval.SelectedValue.ToString() + "'";
        if (oo.Duplicate(sql))
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Duplicate entry!", "A");
            return;
        }
        else
        {
            List<SqlParameter> param = new List<SqlParameter>();

            param.Add(new SqlParameter("@QueryFor", "I"));
            param.Add(new SqlParameter("@Classid", drpClass.SelectedValue.ToString()));
            param.Add(new SqlParameter("@EvalName", drpEval.SelectedValue.ToString()));
            param.Add(new SqlParameter("@ExamStartDate", txtDate1.Text.ToString()));
            param.Add(new SqlParameter("@ExamEndDate", txtDate2.Text.ToString()));
            param.Add(new SqlParameter("@CountStartFrom", rbcountStart.SelectedValue.ToString()));
            if (rbcountStart.SelectedValue == "FromSession")
            {
                sql = "select format(FromDate,'dd-MMM-yyyy')FromDate from SessionMaster where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
                param.Add(new SqlParameter("@CountStartDateDate", oo.ReturnTag(sql, "FromDate")));
            }
            else
            {
                param.Add(new SqlParameter("@CountStartDateDate", txtCountStartDate.Text.Trim()));
            }
            param.Add(new SqlParameter("@CountStartFromAdmission", (chkAdmission.Checked ? "Yes" : "No")));
            param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
            param.Add(new SqlParameter("@LoginName", Session["LoginName"].ToString()));
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));

            SqlParameter para = new SqlParameter("@MSG", "");
            para.Direction = ParameterDirection.Output;
            para.Size = 0x100;
            param.Add(para);
            msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("SetAttendenceRangeProc", param);
            if (msg.Trim() == "S")
            {
                selectRecord();
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted Successfully.", "S");
            }
            else
            {
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, Not Submitted!", "A");
            }
        }
    }
    private void selectRecord()
    {
        DataTable dt = new DataTable();
        sql = "select dr.Id, ClassName, dr.EvalName,CountStartFromAdmission, format(ExamStartDate, 'dd-MMM-yyyy')ExamStartDate, format(ExamEndDate, 'dd-MMM-yyyy')ExamEndDate, CountStartFrom, format(CountStartDateDate, 'dd-MMM-yyyy')CountStartDateDate, format(dr.RecordDate, 'dd-MMM-yyyy hh:mm:ss tt')RecordDate, dr.LoginName from SetAttendenceRange dr inner join ClassMaster cm on cm.id=dr.Classid and cm.SessionName=dr.SessionName and cm.BranchCode=dr.BranchCode where dr.SessionName = '" + Session["SessionName"].ToString() + "' and dr.BranchCode = " + Session["BranchCode"] + "";
        dt = oo.Fetchdata(sql);
        Grd.DataSource = dt;
        Grd.DataBind();
    }

    protected void rbcountStart_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbcountStart.SelectedValue== "FromSession")
        {
            divdate.Visible = false;
            txtCountStartDate.Attributes.Add("Class", "form-control-blue  datepicker-normal");
        }
        if (rbcountStart.SelectedValue == "Manualy")
        {
            divdate.Visible = true;
            txtCountStartDate.Attributes.Add("Class", "form-control-blue  datepicker-normal validatetxt");
        }
    }

    
    protected void drpEval_SelectedIndexChanged(object sender, EventArgs e)
    {
        selectRecord();
    }

    protected void LinkEdit_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("Labelid");
        string ss = lblId.Text;
        lblID.Text = ss;
        sql = "select dr.Id, ClassName, cm.id classid, dr.EvalName, CountStartFromAdmission, format(ExamStartDate, 'dd-MMM-yyyy')ExamStartDate, format(ExamEndDate, 'dd-MMM-yyyy')ExamEndDate, CountStartFrom, format(CountStartDateDate, 'dd-MMM-yyyy')CountStartDateDate, format(dr.RecordDate, 'dd-MMM-yyyy hh:mm:ss tt')RecordDate, dr.LoginName from SetAttendenceRange dr inner join ClassMaster cm on cm.id=dr.Classid and cm.SessionName=dr.SessionName and cm.BranchCode=dr.BranchCode where dr.SessionName = '" + Session["SessionName"].ToString() + "' and dr.BranchCode = " + Session["BranchCode"] + " and dr.Id="+ss+"";

        lblClassid.Text = oo.ReturnTag(sql, "classid");
        lblEval.Text = oo.ReturnTag(sql, "EvalName");

        txtDate10.Text = oo.ReturnTag(sql, "ExamStartDate");
        txtDate20.Text = oo.ReturnTag(sql, "ExamEndDate");
        rbcountStart0.Text = oo.ReturnTag(sql, "CountStartFrom");
        txtCountStartDate0.Text = oo.ReturnTag(sql, "CountStartDateDate");
        chkAdmission0.Checked = (oo.ReturnTag(sql, "CountStartFromAdmission") == "Yes" ? true : false);
        if (oo.ReturnTag(sql, "CountStartFrom") == "Manualy")
        {
            txtCountStartDate0.Attributes.Add("Class", "form-control-blue  datepicker-normal validatetxt1");
            divdate0.Visible = true;
        }
        else
        {
            txtCountStartDate0.Attributes.Add("Class", "form-control-blue  datepicker-normal");
            divdate0.Visible = false;
        }

        Panel1_ModalPopupExtender.Show();
    }

    protected void Button4_Click(object sender, EventArgs e)
    {

    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        string msg = "";
        List<SqlParameter> param = new List<SqlParameter>();

        param.Add(new SqlParameter("@QueryFor", "U"));
        param.Add(new SqlParameter("@Id", lblID.Text));
        param.Add(new SqlParameter("@Classid", lblClassid.Text));
        param.Add(new SqlParameter("@EvalName", lblEval.Text));
        param.Add(new SqlParameter("@ExamStartDate", txtDate10.Text.ToString()));
        param.Add(new SqlParameter("@ExamEndDate", txtDate20.Text.ToString()));
        param.Add(new SqlParameter("@CountStartFrom", rbcountStart0.SelectedValue.ToString()));
        if (rbcountStart0.SelectedValue == "FromSession")
        {
            sql = "select format(FromDate,'dd-MMM-yyyy')FromDate from SessionMaster where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            param.Add(new SqlParameter("@CountStartDateDate", oo.ReturnTag(sql, "FromDate")));
        }
        else
        {
            param.Add(new SqlParameter("@CountStartDateDate", txtCountStartDate0.Text.Trim()));
        }
        param.Add(new SqlParameter("@CountStartFromAdmission", (chkAdmission0.Checked?"Yes":"No")));
        param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
        param.Add(new SqlParameter("@LoginName", Session["LoginName"].ToString()));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));

        SqlParameter para = new SqlParameter("@MSG", "");
        para.Direction = ParameterDirection.Output;
        para.Size = 0x100;
        param.Add(para);
        msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("SetAttendenceRangeProc", param);
        if (msg.Trim() == "S")
        {
            selectRecord();
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updatted Successfully.", "S");
        }
        else
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, Not Updatted!", "A");
        }
    }

    protected void rbcountStart0_SelectedIndexChanged(object sender, EventArgs e)
    {
        Panel1_ModalPopupExtender.Show();

        if (rbcountStart0.SelectedValue == "FromSession")
        {
            divdate0.Visible = false;
            txtCountStartDate0.Attributes.Add("Class", "form-control-blue  datepicker-normal");
        }
        if (rbcountStart0.SelectedValue == "Manualy")
        {
            divdate0.Visible = true;
            txtCountStartDate0.Attributes.Add("Class", "form-control-blue  datepicker-normal validatetxt1");
        }
    }
}