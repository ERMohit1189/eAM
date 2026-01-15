using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OptSubjectEntryDelete : Page
{
    SqlConnection con = new SqlConnection();
    SqlCommand cmd = new SqlCommand();
    Campus _oo = new Campus();
    string sql = ""; string _sql = "";
    protected void Page_PreInIt(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (Session["Logintype"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Campus camp = new Campus(); camp.LoadLoader(loader);
        con = _oo.dbGet_connection();
        if (!IsPostBack)
        {
            if (Session["Logintype"].ToString() == "Admin")
            {
            }
            drpClassGroup.Items.Insert(0, new ListItem("<--Select-->", ""));
            drpclass.Items.Insert(0, new ListItem("<--Select-->", ""));
            drpsection.Items.Insert(0, new ListItem("<--Select-->", ""));
            drpBranch.Items.Insert(0, new ListItem("<--Select-->", ""));
            drpsrno.Items.Insert(0, new ListItem("<--Select-->", ""));
        }
    }
    protected void drpClassGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        _sql = "select cla.id, ClassName from dt_ClassGroupMaster cg";
        _sql +=  " inner join ClassMaster cla on cg.BranchCode=cla.BranchCode and cg.SessionName=cla.SessionName and cla.id=ClassId ";
        _sql +=  " where GroupId='" + drpClassGroup.SelectedValue + "' and cg.SessionName='" + Session["SessionName"].ToString() + "' and cg.BranchCode = " + Session["BranchCode"] + " ";
        _sql +=  " order by CIDOrder asc ";
        _oo.FillDropDown_withValue(_sql, drpclass, "ClassName", "id");
        drpclass.Items.Insert(0, new ListItem("<--Select-->", ""));
    }
    protected void drpclass_SelectedIndexChanged(object sender, EventArgs e)
    {
        _sql = "Select SectionName,Id from SectionMaster where ClassNameId='" + drpclass.SelectedValue + "' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        _oo.FillDropDown_withValue(_sql, drpsection, "SectionName", "id");
        drpsection.Items.Insert(0, new ListItem("<--Select-->", ""));
    }

    protected void drpsection_SelectedIndexChanged(object sender, EventArgs e)
    {
        _sql = "Select BranchName,Id from BranchMaster where ClassID='" + drpclass.SelectedValue + "' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        _oo.FillDropDown_withValue(_sql, drpBranch, "BranchName", "id");
        drpBranch.Items.Insert(0, new ListItem("<--Select-->", ""));
    }
    protected void drpBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        drpsrno.Items.Clear();
        
        _sql = "select distinct Name+' - '+asr.SrNo NAME, asr.SrNo from ICSEOptionalSubjectAllotment cg inner join VW_AllStudentRecord asr on cg.BranchCode=asr.BranchCode and cg.SessionName=asr.SessionName and cg.Srno=asr.SrNo where asr.SessionName='" + Session["SessionName"] + "'and asr.BranchCode=" + Session["BranchCode"] + " ";
        _sql = _sql+ " and Classid=" + drpclass.SelectedValue + " and Branchid=" + drpBranch.SelectedValue + " and Sectionid=" + drpsection.SelectedValue + " and isnull(Withdrwal,'')=''  ORDER BY NAME ";
        if (_oo.Fetchdata(_sql).Rows.Count > 0)
        {
            drpsrno.DataSource = _oo.Fetchdata(_sql);
            drpsrno.DataValueField = "SrNo";
            drpsrno.DataTextField = "NAME";
            drpsrno.DataBind();
        }
        drpsrno.Items.Insert(0, new ListItem("<--Select-->", ""));

    }
    protected void lnkView_Click(object sender, EventArgs e)
    {
        Grd.DataSource = null;
        Grd.DataBind();
        string qq = "select id from ICSEOptionalSubjectAllotment where BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and Srno='" + drpsrno.SelectedValue + "'";
        var dts = _oo.Fetchdata(qq);
        if (dts.Rows.Count == 0)
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Optional subject(s) not found alloted!", "A");
            btnSubmit.Visible = false;
            return;
        }
        string ss = "select asr.SrNo, asr.Name, FatherName, CombineClassName from VW_AllStudentRecord asr ";
        ss +=  " where asr.ClassId=" + drpclass.SelectedValue + " and  asr.BranchId=" + drpBranch.SelectedValue + " and  srno='" + drpsrno.SelectedValue + "'";
        var dt = _oo.Fetchdata(ss);
        Grd.DataSource = dt;
        Grd.DataBind();
        for (int i = 0; i < Grd.Rows.Count; i++)
        {
            string tablename = "";
            if (drpClassGroup.SelectedValue == "G5")
            {
                tablename = "CCEIXtoX_1718";
            }
            if (drpClassGroup.SelectedValue == "G6")
            {
                tablename = "CCEXI_1718";
            }
            if (drpClassGroup.SelectedValue == "G7")
            {
                tablename = "CCEXII_1718";
            }
            string qq2 = "select distinct sub.id, SubjectName from "+ tablename + " at inner join TTSubjectMaster sub on sub.id=at.SubjectId ";
            qq2 = qq2 + " and at.BranchCode=sub.BranchCode and at.SessionName=sub.SessionName and subjecttype='Optional'";
            qq2 = qq2 + " where Srno='" + drpsrno.SelectedValue + "' and sub.BranchCode=" + Session["BranchCode"] + " and sub.SessionName='" + Session["SessionName"] + "' ";
            var dts2 = _oo.Fetchdata(qq2);
            CheckBoxList chkSubjects = (CheckBoxList)Grd.Rows[0].FindControl("chkSubjects");
            chkSubjects.DataSource = dts2;
            chkSubjects.DataTextField = "SubjectName";
            chkSubjects.DataValueField = "id";
            chkSubjects.DataBind();
            if (dts2.Rows.Count > 0)
            {
                btnSubmit.Visible = true;
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int sts = 0;
        CheckBoxList chkSubjects = (CheckBoxList)Grd.Rows[0].FindControl("chkSubjects");
        for (int j = 0; j < chkSubjects.Items.Count; j++)
        {
            if (chkSubjects.Items[j].Selected)
            {
                string qq2 = ""; string tablename = "";
                if (drpClassGroup.SelectedValue == "G5")
                {
                    tablename = "CCEIXtoX_1718";
                }
                if (drpClassGroup.SelectedValue == "G6")
                {
                    tablename = "CCEXI_1718";
                }
                if (drpClassGroup.SelectedValue == "G7")
                {
                    tablename = "CCEXII_1718";
                }
                qq2 = qq2 + "delete from " + tablename + " where Srno='" + drpsrno.SelectedValue + "' and SubjectId=" + chkSubjects.Items[j].Value + " ";
                qq2 = qq2 + " and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "'";
                cmd = new SqlCommand();
                cmd.CommandText = qq2;
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                cmd.Parameters.Clear();
                sts = sts + 1;
            }
        }
        if (sts > 0)
        {
            Grd.DataSource = null;
            Grd.DataBind();
            Campus camp = new Campus(); camp.msgbox(this.Page, Div1, "Submitted successfully", "S");
            btnSubmit.Visible = false;
        }
    }
}