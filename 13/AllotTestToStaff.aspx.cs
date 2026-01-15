using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.IO;

public partial class AllotTestToStaff : Page
{
    private SqlConnection _con;
    private readonly Campus _oo;
    private string _sql, _sql1 = String.Empty;
    public AllotTestToStaff()
    {
        _con = new SqlConnection();
        _oo = new Campus();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Session["LoginName"] as string))
        {
            Response.Redirect("~/default.aspx");
        }
        if (Session["Logintype"].ToString().ToLower() != "admin")
        {
            Response.Redirect("~/default.aspx");
        }
        _con = _oo.dbGet_connection();
        _oo.LoadLoader(loader); 

        if (!IsPostBack)
        {
            loadClass();
            ddlSubject.Items.Insert(0, new ListItem("<--Select-->", ""));
        }
    }
    protected void txtHeaderEmpId_TextChanged(object sender, EventArgs e)
    {
        string Ecode = Request.Form[hfEmployeeId.UniqueID];
        if (Ecode == string.Empty)
        {
            Ecode = txtHeaderEmpId.Text.Trim();
        }
        displayEmpInfo();
        LoadData();
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        string Ecode = Request.Form[hfEmployeeId.UniqueID];
        if (Ecode == string.Empty)
        {
            Ecode = txtHeaderEmpId.Text.Trim();
        }
        displayEmpInfo();
        LoadData();
    }
    public void displayEmpInfo()
    {
        string Ecode = Request.Form[hfEmployeeId.UniqueID];
        if (Ecode == string.Empty)
        {
            Ecode = txtHeaderEmpId.Text.Trim();
        }
        _sql = "Select eod.EmpId EmpId,eod.Ecode Ecode,egd.EFirstName+egd.EMiddleName+egd.ELastName as EmpName,egd.EFatherName FatherName,eod.Designation Designation,";
        _sql = _sql + " egd.EMotherName,egd.EMobileNo,Convert(varchar(11),eod.RegistrationDate,106) as RegistrationDate  from EmpployeeOfficialDetails eod ";
        _sql = _sql + " inner join EmpGeneralDetail egd on eod.Empid=egd.Empid and eod.EmpId=egd.EmpId where eod.Withdrwal is null ";
        _sql = _sql + " and eod.Ecode='" + Ecode.Trim() + "' and egd.BranchCode=" + Session["BranchCode"].ToString() + " and eod.BranchCode=" + Session["BranchCode"].ToString() + "";
        GridView1.DataSource = _oo.GridFill(_sql);
        GridView1.DataBind();
        if (GridView1.Rows.Count > 0)
        {
            divFilters.Visible = true;
        }
        else
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox1, "Sorry, No record(s) found!", "A");
            divFilters.Visible = false;
        }
    }
    private void loadClass()
    {
        _sql = "select Id, ClassName from ClassMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " order by CIDOrder";
        _oo.FillDropDown_withValue(_sql, ddlClass, "ClassName", "Id");
        ddlClass.Items.Insert(0, new ListItem("<--Select-->", ""));

       
    }
    
    protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        SubjectLoad();
        _sql = "select id, SectionName from SectionMaster where ClassNameId='" + ddlClass.SelectedValue + "'";
        _sql = _sql + "  and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        _oo.FillDropDown_withValue(_sql, ddlSection, "SectionName", "Id");
        ddlSection.Items.Insert(0, new ListItem("<--All-->", "<--Select-->"));
    }
    protected void SubjectLoad()
    {
        _sql = "select Id, Subject from OT_SubjectMaster where classId=" + ddlClass.SelectedValue + " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        //_sql = _sql+ " and id in (select distinct SubjectId from OT_SigmentMaster where QuestionType='Descriptive' and classId=" + ddlClass.SelectedValue + " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + ")";
        _oo.FillDropDown_withValue(_sql, ddlSubject, "Subject", "Id");
        ddlSubject.Items.Insert(0, new ListItem("<--Select-->", ""));

    }
    protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
    {

       // string asql = "select Id, Paper from OT_PaperMaster where classId=" + ddlClass.SelectedValue + " and SubjectId=" + ddlSubject.SelectedValue + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
       //asql = asql + " and id in (select distinct Paperid from OT_SigmentMaster where QuestionType='Descriptive' and classId=" + ddlClass.SelectedValue + " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + ")";
        
       // FillCheckBoxList_withValue(asql, ddlPaper, "Paper", "Id");
        LoadData();
        //if (_oo.Duplicate(asql))
        //{
        //    divPaper.Visible = true;
        //}
        //else
        //{
        //    divPaper.Visible = false;
        //}
        //if (ddlSubject.SelectedIndex==0)
        //{
        //    divPaper.Visible = false;
        //}
        //for (int i = 0; i < ddlPaper.Items.Count; i++)
        //{
        //    string ss = "select Paperid from OT_AllotTestToStaff where classId=" + ddlClass.SelectedValue + " and sectionid=" + ddlSection.SelectedValue + " and Paperid=" + ddlPaper.Items[i].Value + " and SubjectId=" + ddlSubject.SelectedValue + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        //    if (_oo.Duplicate(ss))
        //    {
        //        ddlPaper.Items[i].Selected = false;
        //        ddlPaper.Items[i].Enabled = false;
        //    }
        //    else
        //    {
        //        ddlPaper.Items[i].Selected = false;
        //        ddlPaper.Items[i].Enabled = true;
        //    }
        //}
    }
    public void FillCheckBoxList_withValue(string qry, CheckBoxList drp, string ColumnName, string ColumnValue)
    {
        string ss = "";
        SqlCommand cmd = new SqlCommand();
        drp.Items.Clear();
        try
        {
            ss = qry;
            cmd.CommandText = ss;
            SqlDataReader dr;
            _con = _oo.dbGet_connection(); cmd.Connection = _con;
            _con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                drp.Items.Add(new System.Web.UI.WebControls.ListItem(dr[ColumnName].ToString(), dr[ColumnValue].ToString()));
            }
            _con.Close();
        }
        catch (SqlException ex)
        {
            _con.Close();
        }
        catch (Exception exs) { }
    }
    
    private void LoadData()
    {
        string Ecode = Request.Form[hfEmployeeId.UniqueID];
        if (Ecode == string.Empty)
        {
            Ecode = txtHeaderEmpId.Text.Trim();
        }
      //  _sql = "select distinct smM.SectionName, aT.SectionId,(select top(1) ClassName from ClassMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and id=SM.classId) classname, EM.id as Examid, SM.Subject, PM.Paper,PM.id Paperid, TermName, EM.ExamName, EM.Duration, FileType, EM.FilePath, format(ExamStart, 'dd-MMM-yyyy') +' To '+ format(ExamEnd, 'dd-MMM-yyyy') Examdate, format(ResultShow, 'dd-MMM-yyyy') +' To '+ format(ResultHide, 'dd-MMM-yyyy') Resultdate, ResultStting, TestSetting, Status,  EM.SessionName, EM.Loginname, EM.Recordeddate, aT.Ecode   from OT_ExamMaster EM";
        _sql = "select distinct aT.id as Paperid,smM.SectionName, aT.SectionId,(select top(1) ClassName from ClassMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and id=SM.classId) classname, EM.id as Examid, SM.Subject,  TermName, EM.ExamName, EM.Duration, FileType, EM.FilePath, format(ExamStart, 'dd-MMM-yyyy') +' To '+ format(ExamEnd, 'dd-MMM-yyyy') Examdate, format(ResultShow, 'dd-MMM-yyyy') +' To '+ format(ResultHide, 'dd-MMM-yyyy') Resultdate, ResultStting, TestSetting, Status,  EM.SessionName, EM.Loginname, EM.Recordeddate, aT.Ecode   from OT_ExamMaster EM";
        _sql = _sql + " inner join OT_SubjectMaster SM on SM.id = EM.SubjectId  and SM.SessionName='" + Session["SessionName"] + "' and SM.BranchCode=" + Session["BranchCode"] + "";
        _sql = _sql + " inner join OT_TermMaster TM on TM.id = EM.Termid  and TM.SessionName='" + Session["SessionName"] + "' and TM.BranchCode=" + Session["BranchCode"] + "";
      //  _sql = _sql + " inner join OT_PaperMaster PM on PM.id = EM.PaperId  and PM.SessionName='" + Session["SessionName"] + "' and PM.BranchCode=" + Session["BranchCode"] + "";
        _sql = _sql + " inner join OT_AllotTestToStaff aT on aT.classid = EM.classId and aT.SubjectId=em.SubjectId and aT.Paperid=em.PaperId and aT.SessionName='" + Session["SessionName"] + "' and aT.BranchCode=" + Session["BranchCode"] + "";
        _sql = _sql + " inner join SectionMaster smM on aT.SectionId=smM.id and smM.SessionName='" + Session["SessionName"] + "' and smM.BranchCode=" + Session["BranchCode"] + "";
        _sql = _sql + " where EM.classid =case when '" + ddlClass.SelectedValue+ "'='' then EM.classid else '" + ddlClass.SelectedValue + "' end and EM.SubjectId = case when '" + ddlSubject.SelectedValue + "'='' then EM.SubjectId else '" + ddlSubject.SelectedValue + "' end ";
        _sql = _sql + " and aT.ECode='" + Ecode + "'";
        _sql = _sql + "  and em.SessionName='" + Session["SessionName"] + "' and em.BranchCode=" + Session["BranchCode"] + "";
        //_sql = _sql + " and EM.id in (select distinct ExamId from OT_SigmentMaster where QuestionType='Descriptive' and classId=case when '" + ddlClass.SelectedValue + "'='' then classid else '" + ddlClass.SelectedValue + "' end and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + ") ";
         _sql = _sql + "  order by em.id, aT.SectionId asc";
        var dt = _oo.Fetchdata(_sql);
        Grd.DataSource = dt;
        Grd.DataBind();
        if (dt.Rows.Count>0)
        {
            
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string FileType = dt.Rows[i]["FileType"].ToString();
                if (FileType == "Manual")
                {
                    HyperLink nnn = (HyperLink)Grd.Rows[i].FindControl("nnn");
                    nnn.Visible = false;
                }
                Label lblEmp = (Label)Grd.Rows[i].FindControl("lblEmp");
                string _sql1 = "select EmpName+' ('+Ecode+')' Name from GetAllStaffRecords_UDF(" + Session["BranchCode"] + ") where ecode='"+ dt.Rows[i]["Ecode"].ToString() + "'";
                lblEmp.Text = _oo.ReturnTag(_sql1, "Name");
            }
            //for (int i = 0; i < ddlPaper.Items.Count; i++)
            //{
            //    string ss = "select Paperid from OT_AllotTestToStaff where classId=" + ddlClass.SelectedValue + " and sectionid=" + ddlSection.SelectedValue + " and Paperid=" + ddlPaper.Items[i].Value + " and SubjectId=" + ddlSubject.SelectedValue + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            //    if (_oo.Duplicate(ss))
            //    {
            //        ddlPaper.Items[i].Selected = true;
            //        ddlPaper.Items[i].Enabled = false;
            //    }
            //    else
            //    {
            //        ddlPaper.Items[i].Selected = false;
            //        ddlPaper.Items[i].Enabled = true;
            //    }
            //}
        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        int sts = 0; int chksts = 0;
        string Ecode = Request.Form[hfEmployeeId.UniqueID];
        if (Ecode == string.Empty)
        {
            Ecode = txtHeaderEmpId.Text.Trim();
        }
        if (ddlSection.SelectedIndex == 0)
        {
            for (int j = 1; j< ddlSection.Items.Count; j++)
            {

                //for (int i = 0; i < ddlPaper.Items.Count; i++)
                //{
                //    if (ddlPaper.Items[i].Selected)
                //    {
                        chksts = chksts + 1;
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.CommandText = "OT_AllotTestToStaffProc";
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Connection = _con;
                            cmd.Parameters.AddWithValue("@ClassId", ddlClass.SelectedValue);
                            cmd.Parameters.AddWithValue("@SubjectId", ddlSubject.SelectedValue);
                           // cmd.Parameters.AddWithValue("@Paperid", ddlPaper.Items[i].Value);
                            cmd.Parameters.AddWithValue("@Paperid",0);
                            cmd.Parameters.AddWithValue("@Sectionid", ddlSection.Items[j].Value);
                            cmd.Parameters.AddWithValue("@ECode", Ecode);
                            cmd.Parameters.AddWithValue("@Loginname", Session["LoginName"].ToString());
                            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                            cmd.Parameters.AddWithValue("@Action", "insert");
                            try
                            {
                                _con.Open();
                                cmd.ExecuteNonQuery();
                                _con.Close();
                                sts = sts + 1;
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                //    }
                //}
            }
        }
        else
        {
            //for (int i = 0; i < ddlPaper.Items.Count; i++)
            //{
            //    if (ddlPaper.Items[i].Selected)
            //    {
                    chksts = chksts + 1;
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "OT_AllotTestToStaffProc";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = _con;
                        cmd.Parameters.AddWithValue("@ClassId", ddlClass.SelectedValue);
                        cmd.Parameters.AddWithValue("@SubjectId", ddlSubject.SelectedValue);
                      //  cmd.Parameters.AddWithValue("@Paperid", ddlPaper.Items[i].Value);
                        cmd.Parameters.AddWithValue("@Paperid",0);
                        cmd.Parameters.AddWithValue("@Sectionid", ddlSection.SelectedValue);
                        cmd.Parameters.AddWithValue("@ECode", Ecode);
                        cmd.Parameters.AddWithValue("@Loginname", Session["LoginName"].ToString());
                        cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                        cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                        cmd.Parameters.AddWithValue("@Action", "insert");
                        try
                        {
                            _con.Open();
                            cmd.ExecuteNonQuery();
                            _con.Close();
                            sts = sts + 1;
                        }
                        catch (Exception ex)
                        {
                        }
                    }
            //    }
            //}
        }
        //if (chksts==0)
        //{
        //    _oo.msgbox(Page, msgbox, "Please check atleast one subject!.", "A");
        //}
        if (sts>0)
        {
            //for (int i = 0; i < ddlPaper.Items.Count; i++)
            //{
            //    ddlPaper.Items[i].Selected = false;
            //}
            _oo.msgbox(Page, msgbox, "Submitted successfully.", "S");
            LoadData();

        }
    }
    

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        Button8.Focus();
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("Paperid");
        var ss = lblId.Text;
        lblvalue.Text = ss;
        Panel2_ModalPopupExtender.Show();
    }
    protected void btnDeleteYes_Click(object sender, EventArgs e)
    {
        //_sql = "Delete from OT_AllotTestToStaff where Paperid=" + lblvalue.Text + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        _sql = "Delete from OT_AllotTestToStaff where id=" + lblvalue.Text + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";

        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = _sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = _con;
            try
            {
                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
                _oo.msgbox(Page, msgbox, "Deleted successfully.", "S");
                LoadData();
                //for (int i = 0; i < ddlPaper.Items.Count; i++)
                //{
                //    string ss = "select Paperid from OT_AllotTestToStaff where classId=" + ddlClass.SelectedValue + " and Paperid=" + ddlPaper.Items[i].Value + " and SubjectId=" + ddlSubject.SelectedValue + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                //    if (_oo.Duplicate(ss))
                //    {
                //        ddlPaper.Items[i].Selected = true;
                //        ddlPaper.Items[i].Enabled = false;
                //    }
                //    else
                //    {
                //        ddlPaper.Items[i].Selected = false;
                //        ddlPaper.Items[i].Enabled = true;
                //    }
                //}
            }
            catch (Exception ex)
            {
            }
        }
    }
    protected void Button8_Click(object sender, EventArgs e)
    {
    }

    public override void Dispose()
    {
        _con.Dispose();
        _oo.Dispose();
    }
}