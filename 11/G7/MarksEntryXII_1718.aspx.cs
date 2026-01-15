using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

public partial class MarksEntryXII_1718 : Page
{
    SqlConnection con = new SqlConnection();
    SqlCommand cmd = new SqlCommand();
    Campus oo = new Campus();
    string sql = "";
    protected bool IsLocked = false;
    protected void Page_PreInIt(object sender, EventArgs e)
    {
        if (Session["Logintype"] == null) { Response.Redirect("~/default.aspx"); }
        if (Session["Logintype"].ToString() == "Admin")
        {
            this.MasterPageFile = "~/Master/admin_root-manager.master";
        }
        else if (Session["Logintype"].ToString() == "Staff")
        {
            this.MasterPageFile = "~/Staff/staff_root-manager.master";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("~/default.aspx");
        }

        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);
        if (!IsPostBack)
        {
            loadclass();
            loadBranch();
            drpsection.Items.Insert(0, new ListItem("<--Select-->", "0"));
            drpSubject.Items.Insert(0, new ListItem("<--Select-->", "0"));
            drpPaper.Items.Insert(0, new ListItem("<--Select-->", "0"));

        }
    }
    public string checkIsCompulsory()
    {
        string sqlAd = "select SubjectType from TTSubjectMaster where Id=" + drpSubject.SelectedValue + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"] + " and classid=" + drpclass.SelectedValue + " and BranchId=" + drpBranch.SelectedValue + "";
        return oo.ReturnTag(sqlAd, "SubjectType").ToString();
    }

    public void loadgrid()
    {
        GridView1.DataSource = null;
        GridView1.DataBind();
        GridView2.DataSource = null;
        GridView2.DataBind();
        if (checkIsCompulsory() == "Compulsory")
        {
            sql = " Select asr.SrNo,Name,FatherName from AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "'," + Session["BranchCode"].ToString() + ") asr ";
            sql +=  " where asr.ClassId='" + drpclass.SelectedValue.ToString() + "'  and asr.BranchId='" + drpBranch.SelectedValue.ToString() + "'";
            sql +=  " and asr.SectionId='" + drpsection.SelectedValue.ToString() + "'  and Withdrwal is null and isnull(Promotion, '')<>'Cancelled' order by Name Asc";
        }
        else if (checkIsCompulsory() == "Optional")
        {
            sql = " Select asr.SrNo,Name,FatherName from AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "'," + Session["BranchCode"].ToString() + ") asr";
            sql +=  " inner join ICSEOptionalSubjectAllotment sos on sos.Srno=asr.SrNo and sos.SessionName=asr.SessionName and sos.BranchCode=asr.BranchCode";
            sql +=  " where asr.ClassId='" + drpclass.SelectedValue + "' and asr.BranchId='" + drpBranch.SelectedValue + "' and asr.SectionId='" + drpsection.SelectedValue + "' and asr.SessionName='" + Session["SessionName"].ToString() + "'  and asr.BranchCode=" + Session["BranchCode"] + "";
            sql +=  "  and sos.OptSubjectId='" + drpSubject.SelectedValue.ToString() + "' and Withdrwal is null and isnull(Promotion, '')<>'Cancelled' ";
            sql +=  " order by Name Asc";
        }
        if (drpEval.SelectedValue == "TERM1")
        {
            GridView1.DataSource = oo.GridFill(sql);
            GridView1.DataBind();
            if (GridView1.Rows.Count > 0)
            {
                lnkSubmit.Visible = true;
                table1.Visible = true;
                TextBox txt1 = (TextBox)GridView1.HeaderRow.FindControl("txtUt1Max");
                TextBox txt2 = (TextBox)GridView1.HeaderRow.FindControl("txtUt2Max");
                TextBox txtPracMax = (TextBox)GridView1.HeaderRow.FindControl("txtPracMax");
                TextBox txtHYMax = (TextBox)GridView1.HeaderRow.FindControl("txtHYMax");


                txt1.Text = string.Empty;
                txt2.Text = string.Empty;
                txtPracMax.Text = string.Empty;
                txtHYMax.Text = string.Empty;
                hideColumn();
            }
            else
            {
                table1.Visible = false;
                lnkSubmit.Visible = false;
            }
        }
        else
        {
            GridView2.DataSource = oo.GridFill(sql);
            GridView2.DataBind();
            if (GridView2.Rows.Count > 0)
            {
                lnkSubmit.Visible = true;
                table1.Visible = true;
                TextBox txt1 = (TextBox)GridView2.HeaderRow.FindControl("txtPreliminaryMax");
                TextBox txtPracMax = (TextBox)GridView2.HeaderRow.FindControl("txtPrac_2Max");
                TextBox txtHYMax = (TextBox)GridView2.HeaderRow.FindControl("txtPrBoard1Max");
                TextBox txtHYMax2 = (TextBox)GridView2.HeaderRow.FindControl("txtPrBoard2Max");


                txt1.Text = string.Empty;
                txtHYMax2.Text = string.Empty;
                txtPracMax.Text = string.Empty;
                txtHYMax.Text = string.Empty;
                hideColumn();
            }
            else
            {
                table1.Visible = false;
                lnkSubmit.Visible = false;
            }
        }
        

        

    }
    public void loadclass()
    {
        if (Session["Logintype"].ToString() == "Admin")
        {
            sql = "Select Distinct ClassName,cm.Id as Id,CIDOrder from ClassMaster cm ";
            sql +=  " inner join dt_ClassGroupMaster t1 on t1.ClassId=cm.Id and t1.SessionName=cm.SessionName  and cm.BranchCode=t1.BranchCode";
            sql +=  " where cm.SessionName='" + Session["SessionName"] + "' and cm.BranchCode=" + Session["BranchCode"] + " and GroupId='G7' Order by CIDOrder";
            oo.FillDropDown_withValue(sql, drpclass, "ClassName", "Id");
        }
        else
        {

            sql = "Select Distinct ClassName,sctm.ClassId as Id,CIDOrder from ICSESubjectTeacherAllotment sctm";
            sql +=  " inner join ClassMaster cm on cm.Id=sctm.ClassId and cm.SessionName=sctm.SessionName and cm.BranchCode=sctm.BranchCode";
            sql +=  " inner join dt_ClassGroupMaster t1 on t1.ClassId=sctm.ClassId and t1.SessionName=sctm.SessionName and t1.BranchCode=sctm.BranchCode";
            sql +=  " where GroupId='G7'  and sctm.SessionName='" + Session["SessionName"].ToString() + "' and sctm.BranchCode=" + Session["BranchCode"] + " and Ecode='" + Session["LoginName"].ToString() + "' Order by CIDOrder";
            oo.FillDropDown_withValue(sql, drpclass, "ClassName", "Id");
        }
    }

    public void loadsection()
    {
        if (Session["Logintype"].ToString() == "Admin")
        {
            sql = "Select SectionName,Id from SectionMaster where ClassNameId='" + drpclass.SelectedValue.ToString() + "' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"].ToString() + "'";
            oo.FillDropDown_withValue(sql, drpsection, "SectionName", "id");
            drpsection.Items.Insert(0, new ListItem("<--Select-->", "0"));

        }
        else
        {
            sql = "Select Distinct sm.SectionName,sm.id from ICSESubjectTeacherAllotment sctm";
            sql +=  " inner join SectionMaster sm on sm.ClassNameId=sctm.ClassId and sm.id=sctm.sectionid and sm.SessionName=sctm.SessionName   and sm.BranchCode=sctm.BranchCode ";
            sql +=  " where sctm.BranchCode=" + Session["BranchCode"].ToString() + " and sctm.SessionName='" + Session["SessionName"].ToString() + "' and ClassId='" + drpclass.SelectedValue.ToString() + "' and Ecode='" + Session["LoginName"].ToString() + "'";
            oo.FillDropDown_withValue(sql, drpsection, "SectionName", "id");
            drpsection.Items.Insert(0, new ListItem("<--Select-->", "0"));
        }

    }

    private void loadBranch()
    {
        if (Session["Logintype"].ToString() == "Admin")
        {
            sql = "Select BranchName,Id from BranchMaster where ClassId='" + drpclass.SelectedValue.ToString() + "'";
            sql +=  " and  BranchCode=" + Session["BranchCode"].ToString() + " and SessionName='" + Session["SessionName"].ToString() + "'";
            oo.FillDropDown_withValue(sql, drpBranch, "BranchName", "Id");
            drpBranch.Items.Insert(0, new ListItem("<--Select-->", "0"));
        }
        else
        {
            sql = "Select Distinct bm.BranchName,bm.id from ICSESubjectTeacherAllotment sctm";
            sql +=  " inner join BranchMaster bm on bm.Id=sctm.Branchid and bm.ClassId=sctm.ClassId and bm.SessionName=sctm.SessionName  and bm.BranchCode=sctm.BranchCode";
            sql +=  " where sctm.BranchCode=" + Session["BranchCode"].ToString() + " and sctm.SessionName='" + Session["SessionName"].ToString() + "' and bm.ClassId='" + drpclass.SelectedValue.ToString() + "' and Ecode='" + Session["LoginName"].ToString() + "' order by bm.id";
            oo.FillDropDown_withValue(sql, drpBranch, "BranchName", "Id");
            drpBranch.Items.Insert(0, new ListItem("<--Select-->", "0"));
        }
    }

    private void loadSubjectGroup()
    {

        if (Session["Logintype"].ToString() == "Admin")
        {
            sql = "Select SubjectName,Id from TTSubjectMaster where ClassId='" + drpclass.SelectedValue.ToString() + "' and BranchCode=" + Session["BranchCode"] + " and Branchid='" + drpBranch.SelectedValue.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "' and ApplicableFor in ('Exam','Both')";

            oo.FillDropDown_withValue(sql, drpSubject, "SubjectName", "Id");
            drpSubject.Items.Insert(0, "<--Select-->");
        }
        else
        {
            sql = "Select  Distinct sm.SubjectName,sm.Id from ICSESubjectTeacherAllotment sctm";
            sql +=  " inner join TTSubjectMaster sm on sm.Classid=sctm.Classid and sm.branchid=sctm.branchid and sm.Id=sctm.Subjectid and sm.SessionName=sctm.SessionName and sm.BranchCode=sctm.BranchCode";
            sql +=  " where Ecode='" + Session["LoginName"].ToString() + "' and sctm.BranchCode=" + Session["BranchCode"] + " and sctm.SessionName='" + Session["SessionName"].ToString() + "' and sctm.ClassId='" + drpclass.SelectedValue.ToString() + "'   and sctm.Branchid='" + drpBranch.SelectedValue.ToString() + "' ";
            sql +=  " and sctm.SectionId='" + drpsection.SelectedValue.ToString() + "' ";
            sql +=  "  and ApplicableFor in ('Exam','Both') ";

            oo.FillDropDown_withValue(sql, drpSubject, "SubjectName", "Id");
            drpSubject.Items.Insert(0, "<--Select-->");

        }
    }

    public void loadSubject()
    {
        if (Session["Logintype"].ToString() == "Admin")
        {
            sql = "Select PaperName,sctm.Id from TTSubjectMaster sm";
            sql +=  " inner join TTPaperMaster sctm on sm.Classid=sctm.Classid and sm.branchid=sctm.branchid and sm.Id=sctm.Subjectid and sm.SessionName=sctm.SessionName and sm.BranchCode=sctm.BranchCode";
            sql +=  " where sctm.BranchCode=" + Session["BranchCode"] + " and sctm.SessionName='" + Session["SessionName"].ToString() + "' and sctm.ClassId='" + drpclass.SelectedValue.ToString() + "' and sctm.Branchid='" + drpBranch.SelectedValue.ToString() + "' ";
            sql +=  " and sm.id='" + drpSubject.SelectedValue + "'";

            oo.FillDropDown_withValue(sql, drpPaper, "PaperName", "Id");
            drpPaper.Items.Insert(0, "<--Select-->");
        }
        else
        {
            sql = "Select PaperName,pm.Id from TTSubjectMaster sm";
            sql +=  " inner join TTPaperMaster pm on sm.Classid=pm.Classid and sm.branchid=pm.branchid and sm.Id=pm.Subjectid and sm.SessionName=pm.SessionName and sm.BranchCode=pm.BranchCode ";
            sql +=  " inner join ICSESubjectTeacherAllotment sctm on sm.Classid=sctm.Classid and sm.branchid=sctm.branchid and sm.Id=sctm.Subjectid and sm.SessionName=sctm.SessionName and sm.BranchCode=sctm.BranchCode ";
            sql +=  " where sm.BranchCode=" + Session["BranchCode"] + " and sm.SessionName='" + Session["SessionName"].ToString() + "' and sm.ClassId='" + drpclass.SelectedValue.ToString() + "' and sctm.Branchid='" + drpBranch.SelectedValue.ToString() + "' ";
            sql +=  " and sm.id='" + drpSubject.SelectedValue.ToString() + "' and sctm.SectionId=" + drpsection.SelectedValue.ToString() + " ";

            oo.FillDropDown_withValue(sql, drpPaper, "PaperName", "Id");
            drpPaper.Items.Insert(0, "<--Select-->");
        }
        if (drpPaper.SelectedIndex != 0)
        {
            loadgrid();
            hideColumn();
            loadMarks();
            table1.Visible = true;
            lnkSubmit.Visible = true;
        }
        else
        {
            table1.Visible = false;
            lnkSubmit.Visible = false;
        }
    }
    protected void drpclass_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadBranch();
    }
    protected void drpBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadsection();
    }
    protected void drpsection_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadSubjectGroup();
        drpPaper.Items.Clear();
        drpPaper.Items.Insert(0, new ListItem("<--Select-->", "0"));
    }
    protected void drpEval_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadSubjectGroup();
        drpPaper.Items.Clear();
        drpPaper.Items.Insert(0, new ListItem("<--Select-->", "0"));
    }
    
    protected void drpSubjectGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadSubject();

    }
    protected void drpSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        CheckExamLockStatus();
        loadgrid();
        if (drpEval.SelectedValue == "TERM1")
        {
            loadMarks();
        }
        else
        {
            loadMarks2();
        }
        hideColumn();
        if (IsLocked)
        {
            lnkSubmit.Visible = false;
        }
    }


    private void hideColumn()
    {
        
        string sqlAd = "select count(*) cnt from TTSubjectMaster where IsAditional=1 and Id=" + drpSubject.SelectedValue + " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and classid=" + drpclass.SelectedValue + " and BranchId=" + drpBranch.SelectedValue + "";
        string isAdditional = "NO";
        if (int.Parse(oo.ReturnTag(sqlAd, "cnt")) > 0)
        {
            isAdditional = "YES";
        }
        if (drpEval.SelectedIndex == 0)
        {
            Label LblConv10H = (Label)GridView1.HeaderRow.FindControl("LblConv10H");
            if (isAdditional == "Yes")
            {
                LblConv10H.Text = "Conv. into (5) (A)";
            }
            else
            {
                LblConv10H.Text = "Conv. into (10) (A)";
            }
        }
        else
        {
            Label LblConv_2_10H = (Label)GridView2.HeaderRow.FindControl("LblConv_2_10H");
            if (isAdditional == "Yes")
            {
                LblConv_2_10H.Text = "Conv. into (5)(C)";
            }
            else
            {
                LblConv_2_10H.Text = "Conv. into (10)(C)";
            }

        }
    }
    protected void txtUt1_TextChanged(object sender, EventArgs e)
    {
        TextBox txt = (TextBox)sender;

        var values = txt.Text.ToUpper();
        double thisVal = 0;
        if (values != "NP" && values != "NAD" && values != "ML")
        {
            try
            {
                thisVal = double.Parse(values);
            }
            catch (Exception)
            {
                txt.Text = "";
            }
        }

        if (txt.Text == "")
        {
            return;
        }
        else
        {
            string str = txt.Text;
            bool result;
            double value;
            result = double.TryParse(str, out value);
            if (result == false)
            {
                calculateMarkRow(sender);
            }
            else
            {
                TextBox txt1 = (TextBox)GridView1.HeaderRow.FindControl("txtUt1Max");
                int maxmarks = 0;
                if (txt1.Text != "")
                {
                    maxmarks = Convert.ToInt32(txt1.Text);
                }
                if (Convert.ToDouble(str) > maxmarks)
                {
                    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Maximum Marks is " + maxmarks.ToString(), "A");

                    txt.Text = "";
                    txt.Focus();
                }
                else
                {
                    calculateMarkRow(sender);
                }

            }


        }
        GridViewRow currentrow = (GridViewRow)((Control)sender).NamingContainer;
        if (ddlTabIndex.SelectedIndex != 0)
        {

            if (currentrow.RowIndex == GridView1.Rows.Count - 1)
            {
                int rowindex = currentrow.RowIndex;
                TextBox Test1 = (TextBox)GridView1.Rows[rowindex].FindControl("txtUt1");
                Test1.Focus();
            }
            else if (currentrow.RowIndex < GridView1.Rows.Count - 1)
            {
                int rowindex = currentrow.RowIndex + 1;
                TextBox Test1 = (TextBox)GridView1.Rows[rowindex].FindControl("txtUt1");
                Test1.Focus();
            }
        }
        else
        {
            TextBox Test1 = (TextBox)GridView1.Rows[currentrow.RowIndex].FindControl("txtUt2");
            Test1.Focus();
        }
    }
    protected void txtPreliminary_TextChanged(object sender, EventArgs e)
    {
        TextBox txt = (TextBox)sender;

        var values = txt.Text.ToUpper();
        double thisVal = 0;
        if (values != "NP" && values != "NAD" && values != "ML")
        {
            try
            {
                thisVal = double.Parse(values);
            }
            catch (Exception)
            {
                txt.Text = "";
            }
        }

        if (txt.Text == "")
        {
            return;
        }
        else
        {
            string str = txt.Text;
            bool result;
            double value;
            result = double.TryParse(str, out value);
            if (result == false)
            {
                calculateMarkRow2(sender);
            }
            else
            {
                TextBox txt1 = (TextBox)GridView2.HeaderRow.FindControl("txtPreliminaryMax");
                int maxmarks = 0;
                if (txt1.Text != "")
                {
                    maxmarks = Convert.ToInt32(txt1.Text);
                }
                if (Convert.ToDouble(str) > maxmarks)
                {
                    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Maximum Marks is " + maxmarks.ToString(), "A");

                    txt.Text = "";
                    txt.Focus();
                }
                else
                {
                    calculateMarkRow2(sender);
                }

            }


        }
        GridViewRow currentrow = (GridViewRow)((Control)sender).NamingContainer;
        if (ddlTabIndex.SelectedIndex != 0)
        {

            if (currentrow.RowIndex == GridView2.Rows.Count - 1)
            {
                int rowindex = currentrow.RowIndex;
                TextBox Test1 = (TextBox)GridView2.Rows[rowindex].FindControl("txtPreliminary");
                Test1.Focus();
            }
            else if (currentrow.RowIndex < GridView2.Rows.Count - 1)
            {
                int rowindex = currentrow.RowIndex + 1;
                TextBox Test1 = (TextBox)GridView2.Rows[rowindex].FindControl("txtPreliminary");
                Test1.Focus();
            }
        }
        else
        {
            TextBox Test1 = (TextBox)GridView2.Rows[currentrow.RowIndex].FindControl("txtPrac_2");
            Test1.Focus();
        }
    }
    protected void txtUt2_TextChanged(object sender, EventArgs e)
    {
        TextBox txt = (TextBox)sender;

        var values = txt.Text.ToUpper();
        double thisVal = 0;
        if (values != "NP" && values != "NAD" && values != "ML")
        {
            try
            {
                thisVal = double.Parse(values);
            }
            catch (Exception)
            {
                txt.Text = "";
            }
        }

        if (txt.Text == "")
        {
            return;
        }
        else
        {
            string str = txt.Text;
            bool result;
            double value;
            result = double.TryParse(str, out value);
            if (result == false)
            {
                calculateMarkRow(sender);
            }
            else
            {
                TextBox txt2 = (TextBox)GridView1.HeaderRow.FindControl("txtUt2Max");

                int maxmarks = 0;
                if (txt2.Text != "")
                {
                    maxmarks = Convert.ToInt32(txt2.Text);
                }
                if (Convert.ToDouble(str) > maxmarks)
                {
                    //oo.MessageBoxforUpdatePanel("Maximum Marks is " + maxmarks.ToString(), txt);
                    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Maximum Marks is " + maxmarks.ToString(), "A");

                    txt.Text = "";
                    txt.Focus();
                }
                else
                {
                    calculateMarkRow(sender);
                }

            }
        }
        GridViewRow currentrow = (GridViewRow)((Control)sender).NamingContainer;
        if (ddlTabIndex.SelectedIndex != 0)
        {
            if (currentrow.RowIndex == GridView1.Rows.Count - 1)
            {
                int rowindex = currentrow.RowIndex;
                TextBox Test1 = (TextBox)GridView1.Rows[rowindex].FindControl("txtUt2");
                Test1.Focus();
            }
            else if (currentrow.RowIndex < GridView1.Rows.Count - 1)
            {
                int rowindex = currentrow.RowIndex + 1;
                TextBox Test1 = (TextBox)GridView1.Rows[rowindex].FindControl("txtUt2");
                Test1.Focus();
            }
        }
        else
        {
            TextBox Test1 = (TextBox)GridView1.Rows[currentrow.RowIndex].FindControl("txtPrac");
            Test1.Focus();
        }
    }
    protected void txtHY_TextChanged(object sender, EventArgs e)
    {
        TextBox txt = (TextBox)sender;

        var values = txt.Text.ToUpper();
        double thisVal = 0;
        if (values != "NP" && values != "NAD" && values != "ML")
        {
            try
            {
                thisVal = double.Parse(values);
            }
            catch (Exception)
            {
                txt.Text = "";
            }
        }

        if (txt.Text == "")
        {
            return;
        }
        else
        {
            string str = txt.Text;
            bool result;
            double value;
            result = double.TryParse(str, out value);
            if (result == false)
            {
                calculateMarkRow(sender);
            }
            else
            {
                TextBox txtHYMax = (TextBox)GridView1.HeaderRow.FindControl("txtHYMax");
                int maxmarks = 0;
                if (txtHYMax.Text != "")
                {
                    maxmarks = Convert.ToInt32(txtHYMax.Text);
                }
                if (Convert.ToDouble(str) > maxmarks)
                {
                    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Maximum Marks is " + maxmarks.ToString(), "A");

                    txt.Text = "";
                    txt.Focus();
                }
                else
                {
                    calculateMarkRow(sender);
                }

            }
        }
        GridViewRow currentrow = (GridViewRow)((Control)sender).NamingContainer;
        if (ddlTabIndex.SelectedIndex != 0)
        {

            if (currentrow.RowIndex == GridView1.Rows.Count - 1)
            {
                int rowindex = currentrow.RowIndex;
                TextBox Test1 = (TextBox)GridView1.Rows[rowindex].FindControl("txtHY");
                Test1.Focus();
            }
            else if (currentrow.RowIndex < GridView1.Rows.Count - 1)
            {
                int rowindex = currentrow.RowIndex + 1;
                TextBox Test1 = (TextBox)GridView1.Rows[rowindex].FindControl("txtHY");
                Test1.Focus();
            }
        }
        else
        {
            if (currentrow.RowIndex < GridView1.Rows.Count - 1)
            {
                TextBox Test1 = (TextBox)GridView1.Rows[currentrow.RowIndex + 1].FindControl("txtUt1");
                Test1.Focus();
            }
        }
    }
    protected void txtPrBoard1_TextChanged(object sender, EventArgs e)
    {
        TextBox txt = (TextBox)sender;

        var values = txt.Text.ToUpper();
        double thisVal = 0;
        if (values != "NP" && values != "NAD" && values != "ML")
        {
            try
            {
                thisVal = double.Parse(values);
            }
            catch (Exception)
            {
                txt.Text = "";
            }
        }

        if (txt.Text == "")
        {
            return;
        }
        else
        {
            string str = txt.Text;
            bool result;
            double value;
            result = double.TryParse(str, out value);
            if (result == false)
            {
                calculateMarkRow2(sender);
            }
            else
            {
                TextBox txtHYMax = (TextBox)GridView2.HeaderRow.FindControl("txtPrBoard1Max");
                int maxmarks = 0;
                if (txtHYMax.Text != "")
                {
                    maxmarks = Convert.ToInt32(txtHYMax.Text);
                }
                if (Convert.ToDouble(str) > maxmarks)
                {
                    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Maximum Marks is " + maxmarks.ToString(), "A");

                    txt.Text = "";
                    txt.Focus();
                }
                else
                {
                    calculateMarkRow2(sender);
                }

            }
        }
        GridViewRow currentrow = (GridViewRow)((Control)sender).NamingContainer;
        if (ddlTabIndex.SelectedIndex != 0)
        {

            if (currentrow.RowIndex == GridView2.Rows.Count - 1)
            {
                int rowindex = currentrow.RowIndex;
                TextBox Test1 = (TextBox)GridView2.Rows[rowindex].FindControl("txtPrBoard1");
                Test1.Focus();
            }
            else if (currentrow.RowIndex < GridView2.Rows.Count - 1)
            {
                int rowindex = currentrow.RowIndex + 1;
                TextBox Test1 = (TextBox)GridView2.Rows[rowindex].FindControl("txtPrBoard1");
                Test1.Focus();
            }
        }
        else
        {
            TextBox Test1 = (TextBox)GridView2.Rows[currentrow.RowIndex].FindControl("txtPrBoard2");
            Test1.Focus();
        }
    }
    protected void txtPrBoard2_TextChanged(object sender, EventArgs e)
    {
        TextBox txt = (TextBox)sender;

        var values = txt.Text.ToUpper();
        double thisVal = 0;
        if (values != "NP" && values != "NAD" && values != "ML")
        {
            try
            {
                thisVal = double.Parse(values);
            }
            catch (Exception)
            {
                txt.Text = "";
            }
        }

        if (txt.Text == "")
        {
            return;
        }
        else
        {
            string str = txt.Text;
            bool result;
            double value;
            result = double.TryParse(str, out value);
            if (result == false)
            {
                calculateMarkRow2(sender);
            }
            else
            {
                TextBox txtHYMax = (TextBox)GridView2.HeaderRow.FindControl("txtPrBoard2Max");
                int maxmarks = 0;
                if (txtHYMax.Text != "")
                {
                    maxmarks = Convert.ToInt32(txtHYMax.Text);
                }
                if (Convert.ToDouble(str) > maxmarks)
                {
                    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Maximum Marks is " + maxmarks.ToString(), "A");

                    txt.Text = "";
                    txt.Focus();
                }
                else
                {
                    calculateMarkRow2(sender);
                }

            }
        }
        GridViewRow currentrow = (GridViewRow)((Control)sender).NamingContainer;
        if (ddlTabIndex.SelectedIndex != 0)
        {

            if (currentrow.RowIndex == GridView2.Rows.Count - 1)
            {
                int rowindex = currentrow.RowIndex;
                TextBox Test1 = (TextBox)GridView2.Rows[rowindex].FindControl("txtPrBoard2");
                Test1.Focus();
            }
            else if (currentrow.RowIndex < GridView2.Rows.Count - 1)
            {
                int rowindex = currentrow.RowIndex + 1;
                TextBox Test1 = (TextBox)GridView2.Rows[rowindex].FindControl("txtPrBoard2");
                Test1.Focus();
            }
        }
        else
        {
            TextBox Test1 = (TextBox)GridView2.Rows[currentrow.RowIndex].FindControl("txtPreliminary");
            Test1.Focus();
        }
    }
    protected void txtPrac_TextChanged(object sender, EventArgs e)
    {
        TextBox txt = (TextBox)sender;

        var values = txt.Text.ToUpper();
        double thisVal = 0;
        if (values != "NP" && values != "NAD" && values != "ML")
        {
            try
            {
                thisVal = double.Parse(values);
            }
            catch (Exception)
            {
                txt.Text = "";
            }
        }

        if (txt.Text == "")
        {
            return;
        }
        else
        {
            string str = txt.Text;
            bool result;
            double value;
            result = double.TryParse(str, out value);
            if (result == false)
            {
                calculateMarkRow(sender);
            }
            else
            {
                TextBox txtPracMax = (TextBox)GridView1.HeaderRow.FindControl("txtPracMax");
                int maxmarks = 0;
                if (txtPracMax.Text != "")
                {
                    maxmarks = Convert.ToInt32(txtPracMax.Text);
                }
                if (Convert.ToDouble(str) > maxmarks)
                {
                    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Maximum Marks is " + maxmarks.ToString(), "A");

                    txt.Text = "";
                    txt.Focus();
                }
                else
                {
                    calculateMarkRow(sender);
                }

            }
        }
        GridViewRow currentrow = (GridViewRow)((Control)sender).NamingContainer;
        if (ddlTabIndex.SelectedIndex != 0)
        {

            if (currentrow.RowIndex == GridView1.Rows.Count - 1)
            {
                int rowindex = currentrow.RowIndex;
                TextBox Test1 = (TextBox)GridView1.Rows[rowindex].FindControl("txtPrac");
                Test1.Focus();
            }
            else if (currentrow.RowIndex < GridView1.Rows.Count - 1)
            {
                int rowindex = currentrow.RowIndex + 1;
                TextBox Test1 = (TextBox)GridView1.Rows[rowindex].FindControl("txtPrac");
                Test1.Focus();
            }
        }
        else
        {
            TextBox Test1 = (TextBox)GridView1.Rows[currentrow.RowIndex].FindControl("txtHY");
            Test1.Focus();
        }
    }
    protected void txtPrac_2_TextChanged(object sender, EventArgs e)
    {
        TextBox txt = (TextBox)sender;

        var values = txt.Text.ToUpper();
        double thisVal = 0;
        if (values != "NP" && values != "NAD" && values != "ML")
        {
            try
            {
                thisVal = double.Parse(values);
            }
            catch (Exception)
            {
                txt.Text = "";
            }
        }

        if (txt.Text == "")
        {
            return;
        }
        else
        {
            string str = txt.Text;
            bool result;
            double value;
            result = double.TryParse(str, out value);
            if (result == false)
            {
                calculateMarkRow2(sender);
            }
            else
            {
                TextBox txtPracMax = (TextBox)GridView2.HeaderRow.FindControl("txtPrac_2Max");
                int maxmarks = 0;
                if (txtPracMax.Text != "")
                {
                    maxmarks = Convert.ToInt32(txtPracMax.Text);
                }
                if (Convert.ToDouble(str) > maxmarks)
                {
                    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Maximum Marks is " + maxmarks.ToString(), "A");

                    txt.Text = "";
                    txt.Focus();
                }
                else
                {
                    calculateMarkRow2(sender);
                }

            }
        }
        GridViewRow currentrow = (GridViewRow)((Control)sender).NamingContainer;
        if (ddlTabIndex.SelectedIndex != 0)
        {

            if (currentrow.RowIndex == GridView2.Rows.Count - 1)
            {
                int rowindex = currentrow.RowIndex;
                TextBox Test1 = (TextBox)GridView2.Rows[rowindex].FindControl("txtPrac_2");
                Test1.Focus();
            }
            else if (currentrow.RowIndex < GridView2.Rows.Count - 1)
            {
                int rowindex = currentrow.RowIndex + 1;
                TextBox Test1 = (TextBox)GridView2.Rows[rowindex].FindControl("txtPrac_2");
                Test1.Focus();
            }
        }
        else
        {
            TextBox Test1 = (TextBox)GridView2.Rows[currentrow.RowIndex].FindControl("txtPrBoard1");
            Test1.Focus();
        }
    }
    public void calculateMarkRow(object sender)
    {
        string sqlAd = "select count(*) cnt from TTSubjectMaster where IsAditional=1 and Id=" + drpSubject.SelectedValue + " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and classid=" + drpclass.SelectedValue + " and BranchId=" + drpBranch.SelectedValue + "";
        string isAdditional = "NO";
        if (int.Parse(oo.ReturnTag(sqlAd, "cnt")) > 0)
        {
            isAdditional = "YES";
        }
        GridViewRow currentrow = (GridViewRow)((Control)sender).NamingContainer;
        TextBox txt1 = (TextBox)GridView1.HeaderRow.FindControl("txtUt1Max");
        TextBox txt2 = (TextBox)GridView1.HeaderRow.FindControl("txtUt2Max");
        TextBox txtPracMax = (TextBox)GridView1.HeaderRow.FindControl("txtPracMax");
        TextBox txtHYMax = (TextBox)GridView1.HeaderRow.FindControl("txtHYMax");

        TextBox txtUt1 = (TextBox)currentrow.FindControl("txtUt1");
        TextBox txtUt2 = (TextBox)currentrow.FindControl("txtUt2");
        Label LblConv10 = (Label)currentrow.FindControl("LblConv10");
        Label lblConvinBoard = (Label)currentrow.FindControl("lblConvinBoard");
        TextBox txtPrac = (TextBox)currentrow.FindControl("txtPrac");
        TextBox txtHY = (TextBox)currentrow.FindControl("txtHY");

        Label lblconvin20_1 = (Label)currentrow.FindControl("lblconvin20_1");
        Label lblconvin20_2 = (Label)currentrow.FindControl("lblconvin20_2");
        Label lblTotal = (Label)currentrow.FindControl("lblTotal");
        Label lblGrade = (Label)currentrow.FindControl("lblGrade");
        bool result;

        int mm1 = 0, mm2 = 0, mm3 = 0, mm4 = 0; int maxmark;
        double num1 = 0, num2 = 0, num3 = 0, num4 = 0;

        if (txtUt1.Text.ToUpper() == "NAD" || txtUt1.Text.ToUpper() == "ML")
        {

        }
        else if (txtUt1.Text != "")
        {
            result = double.TryParse(txtUt1.Text.Trim(), out num1);
            if (result == true)
            {
                num1 = Convert.ToDouble(txtUt1.Text.Trim());
                
            }
            else
            {

                num1 = 0;

            }
            mm1 = int.TryParse(txt1.Text.Trim(), out maxmark) ? maxmark : 0;
            if (mm1>0)
            {
                num1 = (num1 * 20) / mm1;
                lblconvin20_1.Text = num1.ToString("0.00");
            }
            
        }
        else
        {
            num1 = 0;
            mm1 = int.TryParse(txt1.Text.Trim(), out maxmark) ? maxmark : 0;
        }


        if (txtUt2.Text.ToUpper() == "NAD" || txtUt2.Text.ToUpper() == "ML")
        {

        }
        else if (txtUt2.Text != "")
        {
            result = double.TryParse(txtUt2.Text.Trim(), out num2);
            if (result == true)
            {
                num2 = Convert.ToDouble(txtUt2.Text.Trim());
            }
            else
            {

                num2 = 0;

            }
            mm2 = int.TryParse(txt2.Text.Trim(), out maxmark) ? maxmark : 0;
            if (mm2 > 0)
            {
                num2 = (num2 * 20) / mm2;
                lblconvin20_2.Text = num2.ToString("0.00");
            }
        }
        else
        {
            num2 = 0;
            mm2 = int.TryParse(txt2.Text.Trim(), out maxmark) ? maxmark : 0;
        }




        if (txtPrac.Text.ToUpper() == "NAD" || txtPrac.Text.ToUpper() == "ML")
        {

        }
        else if (txtPrac.Text != "")
        {
            result = double.TryParse(txtPrac.Text.Trim(), out num3);
            if (result == true)
            {
                num3 = Convert.ToDouble(txtPrac.Text.Trim());
            }
            else
            {
                num3 = 0;
            }
            mm3 = int.TryParse(txtPracMax.Text.Trim(), out maxmark) ? maxmark : 0;
        }
        else
        {
            num3 = 0;
            mm3 = int.TryParse(txtPracMax.Text.Trim(), out maxmark) ? maxmark : 0;
        }

        if (txtHY.Text.ToUpper() == "NAD" || txtHY.Text.ToUpper() == "ML")
        {

        }
        else if (txtHY.Text != "")
        {
            result = double.TryParse(txtHY.Text.Trim(), out num4);
            if (result == true)
            {
                num4 = Convert.ToDouble(txtHY.Text.Trim());
            }
            else
            {
                num4 = 0;
            }
            mm4 = int.TryParse(txtHYMax.Text.Trim(), out maxmark) ? maxmark : 0;
        }
        else
        {
            num4 = 0;
            mm4 = int.TryParse(txtHYMax.Text.Trim(), out maxmark) ? maxmark : 0;
        }


        double percentle = 0;
        double totalmarks = 0; double totalmmmarks = 0;
        if ((txtUt1.Text.ToUpper() == "NAD" || txtUt1.Text.ToUpper() == "ML" || txtUt1.Text == "") && (txtUt2.Text.ToUpper() == "NAD" || txtUt2.Text.ToUpper() == "ML" || txtUt2.Text == ""))
        {
            if (txtUt1.Text == "" && txtUt2.Text == "")
            {
                LblConv10.Text = "";
            }
            else
            {
                LblConv10.Text = "NP";
            }
        }
        else
        {

            totalmarks = num1 > num2 ? num1 : num2;
            totalmmmarks = num1 > num2 ? mm1 : mm2;
            if (totalmarks > 0 & totalmmmarks>0)
            {
                if (isAdditional=="Yes")
                {
                    percentle = ((totalmarks) * 5) / totalmmmarks;
                }
                else
                {
                    percentle = ((totalmarks) * 10) / totalmmmarks;
                }
            }

        }
        double total = 0; double ConvinBoard = 0;
        LblConv10.Text = (percentle).ToString("0.0");
        
        if (isAdditional == "Yes")
        {
            ConvinBoard = ((double.Parse(percentle.ToString("0")) + num4) / (5 + mm4)) * mm4;
        }
        else
        {
            ConvinBoard = ((double.Parse(percentle.ToString("0")) + num4) / (10 + mm4)) * mm4;
        }
        lblConvinBoard.Text = ConvinBoard.ToString("0.0");
        total = (ConvinBoard + num3);
        lblTotal.Text = total.ToString("0");
        lblGrade.Text = grade(double.Parse(total.ToString("0")));

    }
    public void calculateMarkRow2(object sender)
    {
        string sqlAd = "select count(*) cnt from TTSubjectMaster where IsAditional=1 and Id=" + drpSubject.SelectedValue + " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and classid=" + drpclass.SelectedValue + " and BranchId=" + drpBranch.SelectedValue + "";
        string isAdditional = "NO";
        if (int.Parse(oo.ReturnTag(sqlAd, "cnt")) > 0)
        {
            isAdditional = "YES";
        }
        GridViewRow currentrow = (GridViewRow)((Control)sender).NamingContainer;
        TextBox txtPreliminaryMax = (TextBox)GridView2.HeaderRow.FindControl("txtPreliminaryMax");
        TextBox txtPrac_2Max = (TextBox)GridView2.HeaderRow.FindControl("txtPrac_2Max");
        TextBox txtPrBoard1Max = (TextBox)GridView2.HeaderRow.FindControl("txtPrBoard1Max");
        TextBox txtPrBoard2Max = (TextBox)GridView2.HeaderRow.FindControl("txtPrBoard2Max");

        TextBox txtPreliminary = (TextBox)currentrow.FindControl("txtPreliminary");
        TextBox txtPrac_2 = (TextBox)currentrow.FindControl("txtPrac_2");
        TextBox txtPrBoard1 = (TextBox)currentrow.FindControl("txtPrBoard1");
        TextBox txtPrBoard2 = (TextBox)currentrow.FindControl("txtPrBoard2");

        Label LblConv_2_10 = (Label)currentrow.FindControl("LblConv_2_10");
        Label lblCD = (Label)currentrow.FindControl("lblCD");
        Label lblTotal_2 = (Label)currentrow.FindControl("lblTotal_2");
        Label lblGrade_2 = (Label)currentrow.FindControl("lblGrade_2");
        bool result;

        int mm1 = 0, mm3 = 0, mm4 = 0, mm5 = 0; int maxmark;
        double num1 = 0, num3 = 0, num4 = 0, num5 = 0;

        if (txtPreliminary.Text.ToUpper() == "NAD" || txtPreliminary.Text.ToUpper() == "ML" || txtPreliminary.Text == "")
        {

        }
        else if (txtPreliminary.Text != "")
        {
            result = double.TryParse(txtPreliminary.Text.Trim(), out num1);
            if (result == true)
            {
                num1 = Convert.ToDouble(txtPreliminary.Text.Trim());
            }
            else
            {

                num1 = 0;

            }
            mm1 = int.TryParse(txtPreliminaryMax.Text.Trim(), out maxmark) ? maxmark : 0;
        }
        else
        {
            num1 = 0;
            mm1 = int.TryParse(txtPreliminaryMax.Text.Trim(), out maxmark) ? maxmark : 0;
        }
        
        if (txtPrac_2.Text.ToUpper() == "NAD" || txtPrac_2.Text.ToUpper() == "ML" || txtPrac_2.Text == "")
        {

        }
        else if (txtPrac_2.Text != "")
        {
            result = double.TryParse(txtPrac_2.Text.Trim(), out num3);
            if (result == true)
            {
                num3 = Convert.ToDouble(txtPrac_2.Text.Trim());
            }
            else
            {
                num3 = 0;
            }
            mm3 = int.TryParse(txtPrac_2Max.Text.Trim(), out maxmark) ? maxmark : 0;
        }
        else
        {
            num3 = 0;
            mm3 = int.TryParse(txtPrac_2Max.Text.Trim(), out maxmark) ? maxmark : 0;
        }

        if (txtPrBoard1.Text.ToUpper() == "NAD" || txtPrBoard1.Text.ToUpper() == "ML" || txtPrBoard1.Text == "")
        {

        }
        else if (txtPrBoard1.Text != "")
        {
            result = double.TryParse(txtPrBoard1.Text.Trim(), out num4);
            if (result == true)
            {
                num4 = Convert.ToDouble(txtPrBoard1.Text.Trim());
            }
            else
            {
                num4 = 0;
            }
            mm4 = int.TryParse(txtPrBoard1Max.Text.Trim(), out maxmark) ? maxmark : 0;
        }
        else
        {
            num4 = 0;
            mm4 = int.TryParse(txtPrBoard1Max.Text.Trim(), out maxmark) ? maxmark : 0;
        }

        if (txtPrBoard2.Text.ToUpper() == "NAD" || txtPrBoard2.Text.ToUpper() == "ML" || txtPrBoard2.Text == "")
        {

        }
        else if (txtPrBoard2.Text != "")
        {
            result = double.TryParse(txtPrBoard2.Text.Trim(), out num5);
            if (result == true)
            {
                num5 = Convert.ToDouble(txtPrBoard2.Text.Trim());
            }
            else
            {
                num5 = 0;
            }
            mm5 = int.TryParse(txtPrBoard2Max.Text.Trim(), out maxmark) ? maxmark : 0;
        }
        else
        {
            num5 = 0;
            mm5= int.TryParse(txtPrBoard2Max.Text.Trim(), out maxmark) ? maxmark : 0;
        }


        double percentle = 0;
        if (txtPreliminary.Text.ToUpper() == "NAD" || txtPreliminary.Text.ToUpper() == "ML" || txtPreliminary.Text == "")
        {
            if (txtPreliminary.Text == "")
            {
                LblConv_2_10.Text = "";
            }
            else
            {
                LblConv_2_10.Text = "NP";
            }
        }
        else
        {
            if (num1 > 0 && mm1>0 && isAdditional == "NO")
            {
                percentle = ((num1) * 10) / mm1;
            }
            if (num1 > 0 && mm1 > 0 && isAdditional != "NO")
            {
                percentle = ((num1) * 5) / mm1;
            }
        }
        double total = 0; double ConvinBoard = 0; double best1Preboardmax = 0;
        LblConv_2_10.Text = (percentle).ToString("0.0");
        double best1Preboard = 0;
        best1Preboard = (num4 > num5 ? num4 : num5);
        best1Preboardmax = (num4 > num5 ? mm4 : mm5);
        if (isAdditional == "Yes")
        {
            ConvinBoard = ((double.Parse(percentle.ToString("0")) + best1Preboard) / (5 + best1Preboardmax)) * best1Preboardmax;
        }
        else
        {
            ConvinBoard = ((double.Parse(percentle.ToString("0")) + best1Preboard) / (10 + best1Preboardmax)) * best1Preboardmax;
        }
        lblCD.Text = ConvinBoard.ToString("0.0");
        total = (num3 + ConvinBoard);
        lblTotal_2.Text = total.ToString("0");
        lblGrade_2.Text = grade(double.Parse(total.ToString("0")));

    }
    public string grade(double percentle)
    {
        if (percentle < 33)
        {
            return "E";
        }
        else if (percentle >= 33 && percentle <= 40)
        {
            return "D";
        }
        else if (percentle >= 40.1 && percentle <= 50)
        {
            return "C2";
        }
        else if (percentle >= 50.1 && percentle <= 60)
        {
            return "C1";
        }
        else if (percentle >= 60.1 && percentle <= 70)
        {
            return "B2";
        }
        else if (percentle >= 70.1 && percentle <= 80)
        {
            return "B1";
        }
        else if (percentle >= 80.1 && percentle <= 90)
        {
            return "A2";
        }
        else if (percentle >= 90.1 && percentle <= 100)
        {
            return "A1";
        }
        else
        {
            return "";
        }
    }
    private void setMaxMarksinDataBase()
    {
        if (GridView1.Rows.Count > 0)
        {
            string msg = "";
            TextBox txtUt1Max = (TextBox)GridView1.HeaderRow.FindControl("txtUt1Max");
            TextBox txtUt2Max = (TextBox)GridView1.HeaderRow.FindControl("txtUt2Max");
            TextBox txtPracMax = (TextBox)GridView1.HeaderRow.FindControl("txtPracMax");
            TextBox txtHYMax = (TextBox)GridView1.HeaderRow.FindControl("txtHYMax");

            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@Eval", drpEval.SelectedValue.ToString().Trim()));
            param.Add(new SqlParameter("@SubjectActivityId", drpSubject.SelectedValue.ToString()));
            param.Add(new SqlParameter("@PaperId", drpPaper.SelectedValue.ToString()));

            param.Add(new SqlParameter("@MaxMarks1", txtUt1Max.Text.Trim()));
            param.Add(new SqlParameter("@MaxMarks2", txtUt2Max.Text.Trim()));
            param.Add(new SqlParameter("@MaxMarks3", txtHYMax.Text.Trim()));
            param.Add(new SqlParameter("@MaxMarks4", txtPracMax.Text.Trim()));

            param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
            param.Add(new SqlParameter("@LoginName", Session["LoginName"].ToString()));
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
            msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("SetMaxMinMarksProc_XII", param);
        }
    }
    private void setMaxMarksinDataBase2()
    {
        if (GridView2.Rows.Count > 0)
        {
            string msg = "";
            TextBox txtPreliminaryMax = (TextBox)GridView2.HeaderRow.FindControl("txtPreliminaryMax");
            TextBox txtPrac_2Max = (TextBox)GridView2.HeaderRow.FindControl("txtPrac_2Max");
            TextBox txtPrBoard1Max = (TextBox)GridView2.HeaderRow.FindControl("txtPrBoard1Max");
            TextBox txtPrBoard2Max = (TextBox)GridView2.HeaderRow.FindControl("txtPrBoard2Max");

            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@Eval", drpEval.SelectedValue.ToString().Trim()));
            param.Add(new SqlParameter("@SubjectActivityId", drpSubject.SelectedValue.ToString()));
            param.Add(new SqlParameter("@PaperId", drpPaper.SelectedValue.ToString()));

            param.Add(new SqlParameter("@MaxMarks1", txtPreliminaryMax.Text.Trim()));
            param.Add(new SqlParameter("@MaxMarks3", txtPrac_2Max.Text.Trim()));
            param.Add(new SqlParameter("@MaxMarks4", txtPrBoard1Max.Text.Trim()));
            param.Add(new SqlParameter("@MaxMarks5", txtPrBoard2Max.Text.Trim()));

            param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
            param.Add(new SqlParameter("@LoginName", Session["LoginName"].ToString()));
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
            msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("SetMaxMinMarksProc_XII", param);
        }
    }
    protected void lnkSubmit_Click(object sender, EventArgs e)
    {
        sql = "Select * from CCEXII_1718 where ClassId='" + drpclass.SelectedValue.ToString() + "' and BranchCode=" + Session["BranchCode"] + " and SubjectId='" + drpSubject.SelectedValue.ToString() + "' and PaperId='" + drpPaper.SelectedValue.ToString() + "' and Evaluation='" + drpEval.SelectedValue.ToString() + "' and SectionName='" + drpsection.SelectedItem.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "'";
        if (oo.Duplicate(sql))
        {
            if (drpEval.Text == "TERM1")
            {
                if (GridView1.Rows.Count > 0)
                {
                    setMaxMarksinDataBase();
                    int row = 0;
                    foreach (GridViewRow gvr in GridView1.Rows)
                    {
                        TextBox txtUt1 = (TextBox)gvr.FindControl("txtUt1");
                        TextBox txtUt2 = (TextBox)gvr.FindControl("txtUt2");
                        TextBox txtPrac = (TextBox)gvr.FindControl("txtPrac");
                        TextBox txtHY = (TextBox)gvr.FindControl("txtHY");
                        Label LblSrNo = (Label)gvr.FindControl("LblSrNo");
                        sql = "Select *from CCEXII_1718 where srno='" + LblSrNo.Text.Trim() + "' and BranchCode=" + Session["BranchCode"] + " and ClassId='" + drpclass.SelectedValue.ToString() + "' and SubjectId='" + drpSubject.SelectedValue.ToString() + "' and PaperId='" + drpPaper.SelectedValue.ToString() + "' and Evaluation='" + drpEval.SelectedValue.ToString() + "' and SectionName='" + drpsection.SelectedItem.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "'";
                        if (oo.Duplicate(sql) == false)
                        {
                            Label LblConv10 = (Label)gvr.FindControl("LblConv10");


                            cmd = new SqlCommand();
                            cmd.CommandText = "USP_CCEXII_1718";
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Connection = con;
                            cmd.Parameters.AddWithValue("@Classid", drpclass.SelectedValue.ToString());
                            cmd.Parameters.AddWithValue("@SectionName", drpsection.SelectedItem.ToString());
                            cmd.Parameters.AddWithValue("@Evaluation", drpEval.SelectedValue.ToString().Trim());
                            cmd.Parameters.AddWithValue("@SubjectId", drpSubject.SelectedValue.ToString());
                            cmd.Parameters.AddWithValue("@PaperId", drpPaper.SelectedValue.ToString());
                            cmd.Parameters.AddWithValue("@SrNo", LblSrNo.Text);
                            cmd.Parameters.AddWithValue("@TEST1", txtUt1.Text.ToUpper());
                            cmd.Parameters.AddWithValue("@TEST2", txtUt2.Text.ToUpper());
                            cmd.Parameters.AddWithValue("@Prac", txtPrac.Text.ToUpper());
                            cmd.Parameters.AddWithValue("@SAT", txtHY.Text.ToUpper());
                            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                            con.Open();
                            row = cmd.ExecuteNonQuery();
                            con.Close();
                        }
                        else
                        {
                            Label LblId = (Label)gvr.FindControl("LblId");
                            cmd = new SqlCommand();
                            cmd.CommandText = "USP_CCEXII_Update_1718";
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Connection = con;
                            cmd.Parameters.AddWithValue("@Id", LblId.Text);
                            cmd.Parameters.AddWithValue("@Classid", drpclass.SelectedValue.ToString());
                            cmd.Parameters.AddWithValue("@Evaluation", drpEval.SelectedValue.ToString().Trim());
                            cmd.Parameters.AddWithValue("@SubjectId", drpSubject.SelectedValue.ToString());
                            cmd.Parameters.AddWithValue("@PaperId", drpPaper.SelectedValue.ToString());
                            cmd.Parameters.AddWithValue("@SrNo", LblSrNo.Text);
                            cmd.Parameters.AddWithValue("@TEST1", txtUt1.Text.ToUpper());
                            cmd.Parameters.AddWithValue("@TEST2", txtUt2.Text.ToUpper());
                            cmd.Parameters.AddWithValue("@Prac", txtPrac.Text.ToUpper());
                            cmd.Parameters.AddWithValue("@SAT", txtHY.Text.ToUpper());
                            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                            con.Open();
                            row = cmd.ExecuteNonQuery();
                            con.Close();

                        }
                    }
                    if (row > 0)
                    {
                        //oo.MessageBoxforUpdatePanel("Submitted successfully", lnkSubmit);
                        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully", "S");

                        drpSubject.SelectedIndex = 0;
                        drpPaper.Items.Clear();
                        drpPaper.Items.Insert(0, new ListItem("<--Select-->", "0"));
                        table1.Visible = false;
                        lnkSubmit.Visible = false;
                        foreach (GridViewRow gvr in GridView1.Rows)
                        {
                            Label LblConv10 = (Label)gvr.FindControl("LblConv10");
                            Label lblTotal = (Label)gvr.FindControl("lblTotal");
                            Label lblConvinBoard = (Label)gvr.FindControl("lblConvinBoard");

                            LblConv10.Text = "";
                            lblTotal.Text = "";
                            lblConvinBoard.Text = "";
                        }
                    }
                }
            }
            if (drpEval.Text == "TERM2")
            {
                if (GridView2.Rows.Count > 0)
                {
                    setMaxMarksinDataBase2();
                    int row = 0;
                    foreach (GridViewRow gvr in GridView2.Rows)
                    {
                        TextBox txtPreliminary = (TextBox)gvr.FindControl("txtPreliminary");
                        TextBox txtPrac_2 = (TextBox)gvr.FindControl("txtPrac_2");
                        TextBox txtPrBoard1 = (TextBox)gvr.FindControl("txtPrBoard1");
                        TextBox txtPrBoard2 = (TextBox)gvr.FindControl("txtPrBoard2");
                        Label LblSrNo = (Label)gvr.FindControl("LblSrNo");
                        sql = "Select *from CCEXII_1718 where srno='" + LblSrNo.Text.Trim() + "' and BranchCode=" + Session["BranchCode"] + " and ClassId='" + drpclass.SelectedValue.ToString() + "' and SubjectId='" + drpSubject.SelectedValue.ToString() + "' and PaperId='" + drpPaper.SelectedValue.ToString() + "' and Evaluation='" + drpEval.SelectedValue.ToString() + "' and SectionName='" + drpsection.SelectedItem.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "'";
                        if (oo.Duplicate(sql) == false)
                        {
                            Label LblConv10 = (Label)gvr.FindControl("LblConv10");


                            cmd = new SqlCommand();
                            cmd.CommandText = "USP_CCEXII_1718";
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Connection = con;
                            cmd.Parameters.AddWithValue("@Classid", drpclass.SelectedValue.ToString());
                            cmd.Parameters.AddWithValue("@SectionName", drpsection.SelectedItem.ToString());
                            cmd.Parameters.AddWithValue("@Evaluation", drpEval.SelectedValue.ToString().Trim());
                            cmd.Parameters.AddWithValue("@SubjectId", drpSubject.SelectedValue.ToString());
                            cmd.Parameters.AddWithValue("@PaperId", drpPaper.SelectedValue.ToString());
                            cmd.Parameters.AddWithValue("@SrNo", LblSrNo.Text);
                            cmd.Parameters.AddWithValue("@TEST1", txtPreliminary.Text.ToUpper());
                            cmd.Parameters.AddWithValue("@Prac", txtPrac_2.Text.ToUpper());
                            cmd.Parameters.AddWithValue("@SAT", txtPrBoard1.Text.ToUpper());
                            cmd.Parameters.AddWithValue("@SAT2", txtPrBoard2.Text.ToUpper());
                            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                            con.Open();
                            row = cmd.ExecuteNonQuery();
                            con.Close();
                        }
                        else
                        {
                            Label LblId = (Label)gvr.FindControl("LblId");
                            cmd = new SqlCommand();
                            cmd.CommandText = "USP_CCEXII_Update_1718";
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Connection = con;
                            cmd.Parameters.AddWithValue("@Id", LblId.Text);
                            cmd.Parameters.AddWithValue("@Classid", drpclass.SelectedValue.ToString());
                            cmd.Parameters.AddWithValue("@Evaluation", drpEval.SelectedValue.ToString().Trim());
                            cmd.Parameters.AddWithValue("@SubjectId", drpSubject.SelectedValue.ToString());
                            cmd.Parameters.AddWithValue("@PaperId", drpPaper.SelectedValue.ToString());
                            cmd.Parameters.AddWithValue("@SrNo", LblSrNo.Text);
                            cmd.Parameters.AddWithValue("@TEST1", txtPreliminary.Text.ToUpper());
                            cmd.Parameters.AddWithValue("@Prac", txtPrac_2.Text.ToUpper());
                            cmd.Parameters.AddWithValue("@SAT", txtPrBoard1.Text.ToUpper());
                            cmd.Parameters.AddWithValue("@SAT2", txtPrBoard2.Text.ToUpper());
                            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                            con.Open();
                            row = cmd.ExecuteNonQuery();
                            con.Close();

                        }
                    }
                    if (row > 0)
                    {
                        //oo.MessageBoxforUpdatePanel("Submitted successfully", lnkSubmit);
                        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully", "S");

                        drpSubject.SelectedIndex = 0;
                        drpPaper.Items.Clear();
                        drpPaper.Items.Insert(0, new ListItem("<--Select-->", "0"));
                        table1.Visible = false;
                        lnkSubmit.Visible = false;
                        foreach (GridViewRow gvr in GridView2.Rows)
                        {
                            Label LblConv_2_10 = (Label)gvr.FindControl("LblConv_2_10");
                            Label lblTotal_2 = (Label)gvr.FindControl("lblTotal_2");
                            Label lblGrade_2 = (Label)gvr.FindControl("lblGrade_2");

                            LblConv_2_10.Text = "";
                            lblTotal_2.Text = "";
                            lblGrade_2.Text = "";
                        }
                    }
                }
            }
        }
        else
        {
            if (drpEval.Text == "TERM1")
            {
                save();
            }
            if (drpEval.Text == "TERM2")
            {
                save2();
            }
        }
    }
    public void save()
    {
        if (GridView1.Rows.Count > 0)
        {
            setMaxMarksinDataBase();
            int row = 0;
            foreach (GridViewRow gvr in GridView1.Rows)
            {
                TextBox txtUt1 = (TextBox)gvr.FindControl("txtUt1");
                TextBox txtUt2 = (TextBox)gvr.FindControl("txtUt2");
                TextBox txtPrac = (TextBox)gvr.FindControl("txtPrac");
                TextBox txtHY = (TextBox)gvr.FindControl("txtHY");
                Label LblSrNo = (Label)gvr.FindControl("LblSrNo");

                cmd = new SqlCommand();
                cmd.CommandText = "USP_CCEXII_1718";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Classid", drpclass.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@SectionName", drpsection.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@Evaluation", drpEval.SelectedValue.ToString().Trim());
                cmd.Parameters.AddWithValue("@SubjectId", drpSubject.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@PaperId", drpPaper.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@SrNo", LblSrNo.Text);
                cmd.Parameters.AddWithValue("@TEST1", txtUt1.Text.ToUpper());
                cmd.Parameters.AddWithValue("@TEST2", txtUt2.Text.ToUpper());
                cmd.Parameters.AddWithValue("@Prac", txtPrac.Text.ToUpper());
                cmd.Parameters.AddWithValue("@SAT", txtHY.Text.ToUpper());
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                con.Open();
                row = cmd.ExecuteNonQuery();
                con.Close();
            }

            if (row > 0)
            {
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully", "S");

                drpSubject.SelectedIndex = 0;
                drpPaper.Items.Clear();
                drpPaper.Items.Insert(0, new ListItem("<--Select-->", "0"));
                table1.Visible = false;
                lnkSubmit.Visible = false;
                foreach (GridViewRow gvr in GridView1.Rows)
                {
                    Label LblConv10 = (Label)gvr.FindControl("LblConv10");
                    Label lblTotal = (Label)gvr.FindControl("lblTotal");
                    Label lblConvinBoard = (Label)gvr.FindControl("lblConvinBoard");

                    LblConv10.Text = "";
                    lblTotal.Text = "";
                    lblConvinBoard.Text = "";
                }
            }
        }
    }
    public void save2()
    {
        if (GridView2.Rows.Count > 0)
        {
            setMaxMarksinDataBase2();
            int row = 0;
            foreach (GridViewRow gvr in GridView2.Rows)
            {
                TextBox txtPreliminary = (TextBox)gvr.FindControl("txtPreliminary");
                TextBox txtUt2 = (TextBox)gvr.FindControl("txtUt2");
                TextBox txtPrac_2 = (TextBox)gvr.FindControl("txtPrac_2");
                TextBox txtPrBoard1 = (TextBox)gvr.FindControl("txtPrBoard1");
                TextBox txtPrBoard2 = (TextBox)gvr.FindControl("txtPrBoard2");
                Label LblSrNo = (Label)gvr.FindControl("LblSrNo");

                cmd = new SqlCommand();
                cmd.CommandText = "USP_CCEXII_1718";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                

                cmd.Parameters.AddWithValue("@Classid", drpclass.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@SectionName", drpsection.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@Evaluation", drpEval.SelectedValue.ToString().Trim());
                cmd.Parameters.AddWithValue("@SubjectId", drpSubject.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@PaperId", drpPaper.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@SrNo", LblSrNo.Text);
                cmd.Parameters.AddWithValue("@TEST1", txtPreliminary.Text.ToUpper());
                cmd.Parameters.AddWithValue("@Prac", txtPrac_2.Text.ToUpper());
                cmd.Parameters.AddWithValue("@SAT", txtPrBoard1.Text.ToUpper());
                cmd.Parameters.AddWithValue("@SAT2", txtPrBoard2.Text.ToUpper());
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                con.Open();
                row = cmd.ExecuteNonQuery();
                con.Close();
            }

            if (row > 0)
            {
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully", "S");

                drpSubject.SelectedIndex = 0;
                drpPaper.Items.Clear();
                drpPaper.Items.Insert(0, new ListItem("<--Select-->", "0"));
                table1.Visible = false;
                lnkSubmit.Visible = false;
                foreach (GridViewRow gvr in GridView2.Rows)
                {
                    Label LblConv_2_10 = (Label)gvr.FindControl("LblConv_2_10");
                    Label lblTotal_2 = (Label)gvr.FindControl("lblTotal_2");
                    Label lblGrade_2 = (Label)gvr.FindControl("lblGrade_2");

                    LblConv_2_10.Text = "";
                    lblTotal_2.Text = "";
                    lblGrade_2.Text = "";
                }
            }
        }
    }
    private void loadSubjectMaxMarks()
    {
        sql = "Select MaxMarks1,MaxMarks2,MaxMarks3,MaxMarks4,MaxMarks5 from SetMaxMinMarks_XII where Eval='" + drpEval.SelectedValue.ToString().Trim() + "' and BranchCode=" + Session["BranchCode"] + " and SubjectActivityId='" + drpSubject.SelectedValue.ToString().Trim() + "' and PaperId='" + drpPaper.SelectedValue.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "'";
        string mm = oo.ReturnTag(sql, "MaxMarks");
        if (GridView1.Rows.Count > 0 && drpEval.SelectedValue.ToString().Trim()=="TERM1")
        {
            TextBox txt1 = (TextBox)GridView1.HeaderRow.FindControl("txtUt1Max");
            TextBox txt2 = (TextBox)GridView1.HeaderRow.FindControl("txtUt2Max");
            TextBox txtHYMax = (TextBox)GridView1.HeaderRow.FindControl("txtHYMax");
            TextBox txtPracMax = (TextBox)GridView1.HeaderRow.FindControl("txtPracMax");

            txt1.Text = oo.ReturnTag(sql, "MaxMarks1");
            txt2.Text = oo.ReturnTag(sql, "MaxMarks2");
            txtHYMax.Text = oo.ReturnTag(sql, "MaxMarks3");
            txtPracMax.Text = oo.ReturnTag(sql, "MaxMarks4");
        }
        if (GridView2.Rows.Count > 0 && drpEval.SelectedValue.ToString().Trim() == "TERM2")
        {
            TextBox txtPreliminaryMax = (TextBox)GridView2.HeaderRow.FindControl("txtPreliminaryMax");
            TextBox txtPrac_2Max = (TextBox)GridView2.HeaderRow.FindControl("txtPrac_2Max");
            TextBox txtPrBoard1Max = (TextBox)GridView2.HeaderRow.FindControl("txtPrBoard1Max");
            TextBox txtPrBoard2Max = (TextBox)GridView2.HeaderRow.FindControl("txtPrBoard2Max");

            txtPreliminaryMax.Text = oo.ReturnTag(sql, "MaxMarks1");
            txtPrac_2Max.Text = oo.ReturnTag(sql, "MaxMarks3");
            txtPrBoard1Max.Text = oo.ReturnTag(sql, "MaxMarks4");
            txtPrBoard2Max.Text = oo.ReturnTag(sql, "MaxMarks5");
        }
    }
    protected void loadMarks()
    {
        string sqlAd = "select count(*) cnt from TTSubjectMaster where IsAditional=1 and Id=" + drpSubject.SelectedValue + " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and classid=" + drpclass.SelectedValue + " and BranchId=" + drpBranch.SelectedValue + "";
        string isAdditional = "NO";
        if (int.Parse(oo.ReturnTag(sqlAd, "cnt")) > 0)
        {
            isAdditional = "YES";
        }
        if (GridView1.Rows.Count > 0)
        {
            loadSubjectMaxMarks();
            sql = "Select * from CCEXII_1718 where ClassId='" + drpclass.SelectedValue.ToString() + "' and SubjectId='" + drpSubject.SelectedValue.ToString() + "' and PaperId='" + drpPaper.SelectedValue.ToString() + "' and BranchCode=" + Session["BranchCode"] + " and Evaluation='" + drpEval.SelectedValue.ToString() + "' and SectionName='" + drpsection.SelectedItem.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "'";
            if (oo.Duplicate(sql))
            {
                checkpermission(drpSubject);
            }
            else
            {
                foreach (GridViewRow gvr in GridView1.Rows)
                {
                    TextBox Text1 = (TextBox)gvr.FindControl("txtUt1");
                    TextBox Text2 = (TextBox)gvr.FindControl("txtUt2");
                    TextBox txtHY = (TextBox)gvr.FindControl("txtHY");
                    TextBox txtPrac = (TextBox)gvr.FindControl("txtPrac");
                    if (drpSubject.SelectedIndex != 0)
                    {
                        Text1.Enabled = true;
                        Text2.Enabled = true;
                        txtHY.Enabled = true;
                        txtPrac.Enabled = true;
                    }
                    else
                    {
                        Text1.Enabled = false;
                        Text2.Enabled = false;
                        txtHY.Enabled = false;
                        txtPrac.Enabled = false;
                    }
                }
            }

            foreach (GridViewRow gvr in GridView1.Rows)
            {
                TextBox txt1 = (TextBox)GridView1.HeaderRow.FindControl("txtUt1Max");
                TextBox txt2 = (TextBox)GridView1.HeaderRow.FindControl("txtUt2Max");
                TextBox txtHYMax = (TextBox)GridView1.HeaderRow.FindControl("txtHYMax");
                TextBox txtPracMax = (TextBox)GridView1.HeaderRow.FindControl("txtPracMax");

                TextBox Text1 = (TextBox)gvr.FindControl("txtUt1");
                TextBox Text2 = (TextBox)gvr.FindControl("txtUt2");
                TextBox txtHY = (TextBox)gvr.FindControl("txtHY");
                TextBox txtPrac = (TextBox)gvr.FindControl("txtPrac");

                Label LblId = (Label)gvr.FindControl("LblId");
                Label lblsrno = (Label)gvr.FindControl("LblSrNo");
                Label LblConv10 = (Label)gvr.FindControl("LblConv10");
                Label lblTotal = (Label)gvr.FindControl("lblTotal");
                Label lblConvinBoard = (Label)gvr.FindControl("lblConvinBoard");
                Label lblGrade = (Label)gvr.FindControl("lblGrade");
                Label lblconvin20_1 = (Label)gvr.FindControl("lblconvin20_1");
                Label lblconvin20_2 = (Label)gvr.FindControl("lblconvin20_2");
                int mm1 = 0, mm2 = 0, mm3 = 0, mm4 = 0;
                double num1 = 0, num2 = 0, num3 = 0, num4 = 0;
                int.TryParse(txt1.Text, out mm1); int.TryParse(txt2.Text, out mm2); int.TryParse(txtPrac.Text, out mm3); int.TryParse(txtHYMax.Text, out mm4);
                
                sql = "Select Id,Test1,Test2,SAT,Prac from CCEXII_1718 where SRNO='" + lblsrno.Text + "' and Evaluation='" + drpEval.SelectedValue.ToString().Trim() + "' and BranchCode=" + Session["BranchCode"] + " and SubjectId='" + drpSubject.SelectedValue.ToString() + "' and PaperId='" + drpPaper.SelectedValue.ToString() + "'";

                LblId.Text = oo.ReturnTag(sql, "Id");
                Text1.Text = oo.ReturnTag(sql, "Test1");
                Text2.Text = oo.ReturnTag(sql, "Test2");
                txtPrac.Text = oo.ReturnTag(sql, "Prac");
                txtHY.Text = oo.ReturnTag(sql, "SAT");
                double.TryParse(Text1.Text, out num1); double.TryParse(Text2.Text, out num2); double.TryParse(txtPrac.Text, out num3); double.TryParse(txtHY.Text, out num4);
                if (mm1 > 0)
                {
                    num1 = (num1 * 20) / mm1;
                    lblconvin20_1.Text = num1.ToString("0.00");
                }
                if (mm2 > 0)
                {
                    num2 = (num2 * 20) / mm2;
                    lblconvin20_2.Text = num2.ToString("0.00");
                }
                double totalmarks = num1 > num2 ? num1 : num2;
                double totalmmmarks = num1 > num2 ? mm1 : mm2;
                double convin10 = 0;
                if (totalmarks>0)
                {
                    if (isAdditional == "YES")
                    {
                        convin10 = (totalmarks * 5) / 20;
                    }
                    else
                    {
                        convin10 = (totalmarks * 10) / 20;
                    }
                }
                LblConv10.Text = (convin10).ToString("0.0");

                double ConvinBoard = 0;
                if (convin10>0)
                {
                    if (isAdditional == "YES")
                    {
                        ConvinBoard = (((double.Parse(convin10.ToString("0")) + num4) / (5 + mm4)) * mm4);
                    }
                    else
                    {
                        ConvinBoard =(((double.Parse(convin10.ToString("0")) + num4) / (10 + mm4)) * mm4);
                    }
                }
                lblConvinBoard.Text = ConvinBoard.ToString("0.0");

                lblTotal.Text = (ConvinBoard + num3).ToString("0");
                lblGrade.Text = grade(double.Parse((ConvinBoard + num3).ToString("0")));
            }
        }
    }
    protected void loadMarks2()
    {
        string sqlAd = "select count(*) cnt from TTSubjectMaster where IsAditional=1 and Id=" + drpSubject.SelectedValue + " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and classid=" + drpclass.SelectedValue + " and BranchId=" + drpBranch.SelectedValue + "";
        string isAdditional = "NO";
        if (int.Parse(oo.ReturnTag(sqlAd, "cnt")) > 0)
        {
            isAdditional = "YES";
        }
        if (GridView2.Rows.Count > 0)
        {
            loadSubjectMaxMarks();
            sql = "Select * from CCEXII_1718 where ClassId='" + drpclass.SelectedValue.ToString() + "' and SubjectId='" + drpSubject.SelectedValue.ToString() + "' and PaperId='" + drpPaper.SelectedValue.ToString() + "' and BranchCode=" + Session["BranchCode"] + " and Evaluation='" + drpEval.SelectedValue.ToString() + "' and SectionName='" + drpsection.SelectedItem.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "'";
            if (oo.Duplicate(sql))
            {
                checkpermission2(drpSubject);
            }
            else
            {
                foreach (GridViewRow gvr in GridView2.Rows)
                {
                    TextBox txtPreliminary = (TextBox)gvr.FindControl("txtPreliminary");
                    TextBox txtPrac_2 = (TextBox)gvr.FindControl("txtPrac_2");
                    TextBox txtPrBoard1 = (TextBox)gvr.FindControl("txtPrBoard1");
                    TextBox txtPrBoard2 = (TextBox)gvr.FindControl("txtPrBoard2");
                    if (drpSubject.SelectedIndex != 0)
                    {
                        txtPreliminary.Enabled = true;
                        txtPrac_2.Enabled = true;
                        txtPrBoard1.Enabled = true;
                        txtPrBoard2.Enabled = true;
                    }
                    else
                    {
                        txtPreliminary.Enabled = false;
                        txtPrac_2.Enabled = false;
                        txtPrBoard1.Enabled = false;
                        txtPrBoard2.Enabled = false;
                    }
                }
            }

            foreach (GridViewRow gvr in GridView2.Rows)
            {
                TextBox txtPreliminaryMax = (TextBox)GridView2.HeaderRow.FindControl("txtPreliminaryMax");
                TextBox txtPrac_2Max = (TextBox)GridView2.HeaderRow.FindControl("txtPrac_2Max");
                TextBox txtPrBoard1Max = (TextBox)GridView2.HeaderRow.FindControl("txtPrBoard1Max");
                TextBox txtPrBoard2Max = (TextBox)GridView2.HeaderRow.FindControl("txtPrBoard2Max");

                TextBox txtPreliminary = (TextBox)gvr.FindControl("txtPreliminary");
                TextBox txtPrac_2 = (TextBox)gvr.FindControl("txtPrac_2");
                TextBox txtPrBoard1 = (TextBox)gvr.FindControl("txtPrBoard1");
                TextBox txtPrBoard2 = (TextBox)gvr.FindControl("txtPrBoard2");

                Label LblId = (Label)gvr.FindControl("LblId");
                Label lblsrno = (Label)gvr.FindControl("LblSrNo");
                Label LblConv_2_10 = (Label)gvr.FindControl("LblConv_2_10");
                Label lblCD = (Label)gvr.FindControl("lblCD");
                Label lblTotal_2 = (Label)gvr.FindControl("lblTotal_2");
                Label lblGrade_2 = (Label)gvr.FindControl("lblGrade_2");
                int mm1 = 0, mm5 = 0, mm3 = 0, mm4 = 0;
                double num1 = 0, num5 = 0, num3 = 0, num4 = 0;
                int.TryParse(txtPreliminaryMax.Text, out mm1); int.TryParse(txtPrac_2Max.Text, out mm3); int.TryParse(txtPrBoard1Max.Text, out mm4); int.TryParse(txtPrBoard2Max.Text, out mm5);

                sql = "Select Id,Test1,Test2,SAT, SAT2,Prac from CCEXII_1718 where SRNO='" + lblsrno.Text + "' and Evaluation='" + drpEval.SelectedValue.ToString().Trim() + "' and BranchCode=" + Session["BranchCode"] + " and SubjectId='" + drpSubject.SelectedValue.ToString() + "' and PaperId='" + drpPaper.SelectedValue.ToString() + "'";

                LblId.Text = oo.ReturnTag(sql, "Id");
                txtPreliminary.Text = oo.ReturnTag(sql, "Test1");
                txtPrac_2.Text = oo.ReturnTag(sql, "Prac");
                txtPrBoard1.Text = oo.ReturnTag(sql, "SAT");
                txtPrBoard2.Text = oo.ReturnTag(sql, "SAT2");
                double.TryParse(txtPreliminary.Text, out num1); double.TryParse(txtPrac_2.Text, out num3); double.TryParse(txtPrBoard1.Text, out num4); double.TryParse(txtPrBoard2.Text, out num5);

                double convin10 = 0;
                if (num1 > 0 && mm1>0 && isAdditional=="YES")
                {
                    convin10 = (num1 * 5) / mm1;
                }
                if (num1 > 0 && mm1 > 0 && isAdditional == "NO")
                {
                    convin10 = (num1 * 10) / mm1;
                }
                double total = 0;
                LblConv_2_10.Text = (convin10).ToString("0.0");
                double best1Preboard = 0;
                best1Preboard = (num4 > num5 ? num4 : num5);
                total = (double.Parse(convin10.ToString("0")) + best1Preboard);
                lblTotal_2.Text = total.ToString("0");
                lblCD.Text = total.ToString("0.0");
                lblGrade_2.Text = grade(double.Parse(total.ToString("0")));
            }
        }
    }
    public void checkpermission(Control ctrl)
    {
        sql = "Select Permission from PermissionForMarksUpdate where EmpCode='" + Session["LoginName"].ToString() + "' and BranchCode=" + Session["BranchCode"] + "";
        if (oo.ReturnTag(sql, "Permission") == "true")
        {
            if (GridView1.Rows.Count > 0)
            {
                foreach (GridViewRow gvr in GridView1.Rows)
                {
                    TextBox Text1 = (TextBox)gvr.FindControl("txtUt1");
                    TextBox Text2 = (TextBox)gvr.FindControl("txtUt2");
                    TextBox txtHY = (TextBox)gvr.FindControl("txtHY");
                    TextBox txtPrac = (TextBox)gvr.FindControl("txtPrac");

                    Text1.Enabled = true;
                    Text2.Enabled = true;
                    txtHY.Enabled = true;
                    txtPrac.Enabled = true;
                }
            }
        }
        else
        {
            if (GridView1.Rows.Count > 0)
            {
                foreach (GridViewRow gvr in GridView1.Rows)
                {
                    TextBox Text1 = (TextBox)gvr.FindControl("txtUt1");
                    TextBox Text2 = (TextBox)gvr.FindControl("txtUt2");
                    TextBox txtHY = (TextBox)gvr.FindControl("txtHY");
                    TextBox txtPrac = (TextBox)gvr.FindControl("txtPrac");

                    Text1.Enabled = false;
                    Text2.Enabled = false;
                    txtHY.Enabled = false;
                    txtPrac.Enabled = false;
                }
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Permission not granted", "A");

            }
        }
    }

    public void checkpermission2(Control ctrl)
    {
        sql = "Select Permission from PermissionForMarksUpdate where EmpCode='" + Session["LoginName"].ToString() + "' and BranchCode=" + Session["BranchCode"] + "";
        if (oo.ReturnTag(sql, "Permission") == "true")
        {
            if (GridView1.Rows.Count > 0)
            {
                foreach (GridViewRow gvr in GridView2.Rows)
                {
                    TextBox txtPreliminary = (TextBox)gvr.FindControl("txtPreliminary");
                    TextBox txtPrac_2 = (TextBox)gvr.FindControl("txtPrac_2");
                    TextBox txtPrBoard1 = (TextBox)gvr.FindControl("txtPrBoard1");
                    TextBox txtPrBoard2 = (TextBox)gvr.FindControl("txtPrBoard2");
                    txtPreliminary.Enabled = true;
                    txtPrac_2.Enabled = true;
                    txtPrBoard1.Enabled = true;
                    txtPrBoard2.Enabled = true;
                }
            }
        }
        else
        {
            if (GridView1.Rows.Count > 0)
            {
                foreach (GridViewRow gvr in GridView2.Rows)
                {
                    TextBox txtPreliminary = (TextBox)gvr.FindControl("txtPreliminary");
                    TextBox txtPrac_2 = (TextBox)gvr.FindControl("txtPrac_2");
                    TextBox txtPrBoard1 = (TextBox)gvr.FindControl("txtPrBoard1");
                    TextBox txtPrBoard2 = (TextBox)gvr.FindControl("txtPrBoard2");
                    txtPreliminary.Enabled = false;
                    txtPrac_2.Enabled = false;
                    txtPrBoard1.Enabled = false;
                    txtPrBoard2.Enabled = false;
                }
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Permission not granted", "A");
            }
        }
    }

    private void CheckExamLockStatus()
    {
        if (Session["Logintype"] != null && Session["Logintype"].ToString() == "Staff")
        {
            string sql33 = "SELECT dr.Id, ClassName, dr.EvalName, ExamLockDate " +
                           "FROM SetAttendenceRange dr " +
                           "INNER JOIN ClassMaster cm ON cm.id=dr.Classid AND cm.SessionName=dr.SessionName AND cm.BranchCode=dr.BranchCode " +
                           "WHERE EvalName='" + drpEval.SelectedValue.ToString().Trim() + "' " +
                           "AND Classid=" + drpclass.SelectedValue.ToString() +
                           " AND dr.SessionName='" + Session["SessionName"].ToString() +
                           "' AND dr.BranchCode=" + Session["BranchCode"].ToString() + "";

            string ExamLockDate = oo.ReturnTag(sql33, "ExamLockDate");

            if (!string.IsNullOrEmpty(ExamLockDate))
            {

                DateTime currentDate = DateTime.Now.Date;
                DateTime specificDate;

                string dateFormat = "dd-MM-yyyy HH:mm:ss"; // Correct format

                bool isValidDate = DateTime.TryParseExact(ExamLockDate, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out specificDate);
               
                if (isValidDate)
                {
                    if (currentDate >= specificDate)
                    {
                        IsLocked = true;
                    }
                }
          
            }
        }

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (IsLocked)
            {
                spanNotVisible.Visible = true;
                spanVisible.Visible = false;
                string evalType = drpEval.SelectedValue.ToString();

                if (evalType == "TERM1" || evalType == "TERM2")
                {
                    foreach (TableCell cell in e.Row.Cells)
                    {
                        foreach (Control control in cell.Controls)
                        {
                            if (control is TextBox)
                            {
                                TextBox txtBox = (TextBox)control;
                                txtBox.ReadOnly = true; // Make the TextBox read-only
                                txtBox.Enabled = false; // Disable the TextBox
                            }
                        }
                    }
                }
            }
            else
            {
                spanNotVisible.Visible = false;
                spanVisible.Visible = true;
            }
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (IsLocked)
            {
                spanNotVisible.Visible = true;
                spanVisible.Visible = false;
                string evalType = drpEval.SelectedValue.ToString();

                if (evalType == "TERM1" || evalType == "TERM2")
                {
                    foreach (TableCell cell in e.Row.Cells)
                    {
                        foreach (Control control in cell.Controls)
                        {
                            if (control is TextBox)
                            {
                                TextBox txtBox = (TextBox)control;
                                txtBox.ReadOnly = true; // Make the TextBox read-only
                                txtBox.Enabled = false; // Disable the TextBox
                            }
                        }
                    }
                }
            }
            else
            {
                spanNotVisible.Visible = false;
                spanVisible.Visible = true;
            }
        }
    }

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (IsLocked)
            {
                spanNotVisible.Visible = true;
                spanVisible.Visible = false;
                string evalType = drpEval.SelectedValue.ToString();

                if (evalType == "TERM1" || evalType == "TERM2")
                {
                    foreach (TableCell cell in e.Row.Cells)
                    {
                        foreach (Control control in cell.Controls)
                        {
                            if (control is TextBox)
                            {
                                TextBox txtBox = (TextBox)control;
                                txtBox.ReadOnly = true; // Make the TextBox read-only
                                txtBox.Enabled = false; // Disable the TextBox
                            }
                        }
                    }
                }
            }
            else
            {
                spanNotVisible.Visible = false;
                spanVisible.Visible = true;
            }
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (IsLocked)
            {
                spanNotVisible.Visible = true;
                spanVisible.Visible = false;
                string evalType = drpEval.SelectedValue.ToString();

                if (evalType == "TERM1" || evalType == "TERM2")
                {
                    foreach (TableCell cell in e.Row.Cells)
                    {
                        foreach (Control control in cell.Controls)
                        {
                            if (control is TextBox)
                            {
                                TextBox txtBox = (TextBox)control;
                                txtBox.ReadOnly = true; // Make the TextBox read-only
                                txtBox.Enabled = false; // Disable the TextBox
                            }
                        }
                    }
                }
            }
            else
            {
                spanNotVisible.Visible = false;
                spanVisible.Visible = true;
            }
        }
    }
}