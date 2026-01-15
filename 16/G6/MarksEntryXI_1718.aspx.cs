using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class common_MarksEntryXI_1718 : Page
{
    SqlConnection con = new SqlConnection();
    SqlCommand cmd = new SqlCommand();
    Campus oo = new Campus();
    string sql = "";

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
        if (checkIsCompulsory() == "Compulsory")
        {
            sql = " Select asr.SrNo,Name,FatherName from AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "'," + Session["BranchCode"].ToString() + ") asr ";
            sql = sql + " where asr.ClassId='" + drpclass.SelectedValue.ToString() + "'  and asr.BranchId='" + drpBranch.SelectedValue.ToString() + "'";
            sql = sql + " and asr.SectionId='" + drpsection.SelectedValue.ToString() + "'  and Withdrwal is null and isnull(Promotion, '')<>'Cancelled' order by Name Asc";
        }
        else if (checkIsCompulsory() == "Optional")
        {
            sql = " Select asr.SrNo,Name,FatherName from AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "'," + Session["BranchCode"].ToString() + ") asr";
            sql = sql + " inner join ICSEOptionalSubjectAllotment sos on sos.Srno=asr.SrNo and sos.SessionName=asr.SessionName and sos.BranchCode=asr.BranchCode";
            sql = sql + " where asr.ClassId='" + drpclass.SelectedValue + "' and asr.BranchId='" + drpBranch.SelectedValue + "' and asr.SectionId='" + drpsection.SelectedValue + "' and asr.SessionName='" + Session["SessionName"].ToString() + "'  and asr.BranchCode=" + Session["BranchCode"] + "";
            sql = sql + "  and sos.OptSubjectId='" + drpSubject.SelectedValue.ToString() + "' and Withdrwal is null and isnull(Promotion, '')<>'Cancelled' ";
            sql = sql + " order by Name Asc";
        }

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
            
        }
        else
        {
            table1.Visible = false;
            lnkSubmit.Visible = false;
        }

    }
    public void loadclass()
    {
        if (Session["Logintype"].ToString() == "Admin")
        {
            sql = "Select Distinct ClassName,cm.Id as Id,CIDOrder from ClassMaster cm ";
            sql = sql + " inner join dt_ClassGroupMaster t1 on t1.ClassId=cm.Id and t1.SessionName=cm.SessionName  and cm.BranchCode=t1.BranchCode";
            sql = sql + " where cm.SessionName='" + Session["SessionName"] + "' and cm.BranchCode=" + Session["BranchCode"] + " and GroupId='G6' Order by CIDOrder";
            oo.FillDropDown_withValue(sql, drpclass, "ClassName", "Id");
        }
        else
        {

            sql = "Select Distinct ClassName,sctm.ClassId as Id,CIDOrder from ICSESubjectTeacherAllotment sctm";
            sql = sql + " inner join ClassMaster cm on cm.Id=sctm.ClassId and cm.SessionName=sctm.SessionName and cm.BranchCode=sctm.BranchCode";
            sql = sql + " inner join dt_ClassGroupMaster t1 on t1.ClassId=sctm.ClassId and t1.SessionName=sctm.SessionName and t1.BranchCode=sctm.BranchCode";
            sql = sql + " where GroupId='G6'  and sctm.SessionName='" + Session["SessionName"].ToString() + "' and sctm.BranchCode=" + Session["BranchCode"] + " and Ecode='" + Session["LoginName"].ToString() + "' Order by CIDOrder";
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
            sql = sql + " inner join SectionMaster sm on sm.ClassNameId=sctm.ClassId and sm.id=sctm.sectionid and sm.SessionName=sctm.SessionName   and sm.BranchCode=sctm.BranchCode ";
            sql = sql + " where sctm.BranchCode=" + Session["BranchCode"].ToString() + " and sctm.SessionName='" + Session["SessionName"].ToString() + "' and ClassId='" + drpclass.SelectedValue.ToString() + "' and Ecode='" + Session["LoginName"].ToString() + "'";
            oo.FillDropDown_withValue(sql, drpsection, "SectionName", "id");
            drpsection.Items.Insert(0, new ListItem("<--Select-->", "0"));
        }

    }

    private void loadBranch()
    {
        if (Session["Logintype"].ToString() == "Admin")
        {
            sql = "Select BranchName,Id from BranchMaster where ClassId='" + drpclass.SelectedValue.ToString() + "'";
            sql = sql + " and  BranchCode=" + Session["BranchCode"].ToString() + " and SessionName='" + Session["SessionName"].ToString() + "'";
            oo.FillDropDown_withValue(sql, drpBranch, "BranchName", "Id");
            drpBranch.Items.Insert(0, new ListItem("<--Select-->", "0"));
        }
        else
        {
            sql = "Select Distinct bm.BranchName,bm.id from ICSESubjectTeacherAllotment sctm";
            sql = sql + " inner join BranchMaster bm on bm.Id=sctm.Branchid and bm.ClassId=sctm.ClassId and bm.SessionName=sctm.SessionName  and bm.BranchCode=sctm.BranchCode";
            sql = sql + " where sctm.BranchCode=" + Session["BranchCode"].ToString() + " and sctm.SessionName='" + Session["SessionName"].ToString() + "' and bm.ClassId='" + drpclass.SelectedValue.ToString() + "' and Ecode='" + Session["LoginName"].ToString() + "' order by bm.id";
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
            sql = sql + " inner join TTSubjectMaster sm on sm.Classid=sctm.Classid and sm.branchid=sctm.branchid and sm.Id=sctm.Subjectid and sm.SessionName=sctm.SessionName and sm.BranchCode=sctm.BranchCode";
            sql = sql + " where Ecode='" + Session["LoginName"].ToString() + "' and sctm.BranchCode=" + Session["BranchCode"] + " and sctm.SessionName='" + Session["SessionName"].ToString() + "' and sctm.ClassId='" + drpclass.SelectedValue.ToString() + "'   and sctm.Branchid='" + drpBranch.SelectedValue.ToString() + "' ";
            sql = sql + " and sctm.SectionId='" + drpsection.SelectedValue.ToString() + "' ";
            sql = sql + "  and ApplicableFor in ('Exam','Both') ";

            oo.FillDropDown_withValue(sql, drpSubject, "SubjectName", "Id");
            drpSubject.Items.Insert(0, "<--Select-->");

        }
    }

    public void loadSubject()
    {
        if (Session["Logintype"].ToString() == "Admin")
        {
            sql = "Select PaperName,sctm.Id from TTSubjectMaster sm";
            sql = sql + " inner join TTPaperMaster sctm on sm.Classid=sctm.Classid and sm.branchid=sctm.branchid and sm.Id=sctm.Subjectid and sm.SessionName=sctm.SessionName and sm.BranchCode=sctm.BranchCode";
            sql = sql + " where sctm.BranchCode=" + Session["BranchCode"] + " and sctm.SessionName='" + Session["SessionName"].ToString() + "' and sctm.ClassId='" + drpclass.SelectedValue.ToString() + "' and sctm.Branchid='" + drpBranch.SelectedValue.ToString() + "' ";
            sql = sql + " and sm.id='" + drpSubject.SelectedValue + "'";

            oo.FillDropDown_withValue(sql, drpPaper, "PaperName", "Id");
            drpPaper.Items.Insert(0, "<--Select-->");
        }
        else
        {
            sql = "Select PaperName,pm.Id from TTSubjectMaster sm";
            sql = sql + " inner join TTPaperMaster pm on sm.Classid=pm.Classid and sm.branchid=pm.branchid and sm.Id=pm.Subjectid and sm.SessionName=pm.SessionName and sm.BranchCode=pm.BranchCode ";
            sql = sql + " inner join ICSESubjectTeacherAllotment sctm on sm.Classid=sctm.Classid and sm.branchid=sctm.branchid and sm.Id=sctm.Subjectid and sm.SessionName=sctm.SessionName and sm.BranchCode=sctm.BranchCode ";
            sql = sql + " where sm.BranchCode=" + Session["BranchCode"] + " and sm.SessionName='" + Session["SessionName"].ToString() + "' and sm.ClassId='" + drpclass.SelectedValue.ToString() + "' and sctm.Branchid='" + drpBranch.SelectedValue.ToString() + "' ";
            sql = sql + " and sm.id='" + drpSubject.SelectedValue.ToString() + "' and sctm.SectionId=" + drpsection.SelectedValue.ToString() + " ";

            oo.FillDropDown_withValue(sql, drpPaper, "PaperName", "Id");
            drpPaper.Items.Insert(0, "<--Select-->");
        }
        if (drpPaper.SelectedIndex != 0)
        {
            loadgrid();
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
        if (ddlTabIndex.SelectedIndex!= 0)
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
    public void calculateMarkRow(object sender)
    {
        GridViewRow currentrow = (GridViewRow)((Control)sender).NamingContainer;
        TextBox txt1 = (TextBox)GridView1.HeaderRow.FindControl("txtUt1Max");
        TextBox txt2 = (TextBox)GridView1.HeaderRow.FindControl("txtUt2Max");
        TextBox txtPracMax = (TextBox)GridView1.HeaderRow.FindControl("txtPracMax");
        TextBox txtHYMax = (TextBox)GridView1.HeaderRow.FindControl("txtHYMax");

        TextBox txtUt1 = (TextBox)currentrow.FindControl("txtUt1");
        TextBox txtUt2 = (TextBox)currentrow.FindControl("txtUt2");
        TextBox txtPrac = (TextBox)currentrow.FindControl("txtPrac");
        TextBox txtHY = (TextBox)currentrow.FindControl("txtHY");

        Label LblConv10 = (Label)currentrow.FindControl("LblConv10");
        Label lblConvinBoard = (Label)currentrow.FindControl("lblConvinBoard");
        Label lblTotal = (Label)currentrow.FindControl("lblTotal");
        Label lblGrade = (Label)currentrow.FindControl("lblGrade");
        bool result;

        int mm1 = 0, mm2 = 0, mm3 = 0, mm4 = 0; int maxmark;
        double num1 = 0, num2 = 0, num3 = 0, num4 = 0;

        if (txtUt1.Text.ToUpper() == "NAD" || txtUt1.Text.ToUpper() == "ML" || txtUt1.Text == "")
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
        }
        else
        {
            num1 = 0;
            mm1 = int.TryParse(txt1.Text.Trim(), out maxmark) ? maxmark : 0;
        }


        if (txtUt2.Text.ToUpper() == "NAD" || txtUt2.Text.ToUpper() == "ML" || txtUt2.Text == "")
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
        }
        else
        {
            num2 = 0;
            mm2 = int.TryParse(txt2.Text.Trim(), out maxmark) ? maxmark : 0;
        }




        if (txtPrac.Text.ToUpper() == "NAD" || txtPrac.Text.ToUpper() == "ML" || txtPrac.Text == "")
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

        if (txtHY.Text.ToUpper() == "NAD" || txtHY.Text.ToUpper() == "ML" || txtHY.Text == "")
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
        bool isaddmmconinten = false; double totalmarks = 0; double totalmmmarks = 0;
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
            if (totalmarks == 0)
            {
                isaddmmconinten = true;
            }
            if (totalmmmarks == 0)
            {
                isaddmmconinten = true;
            }
            else
            {
                percentle = ((totalmarks) * 10) / totalmmmarks;
            }

        }
        double total = 0; double ConvinBoard = 0;
        LblConv10.Text = (percentle).ToString("0.0");
        
        ConvinBoard = (((double.Parse(percentle.ToString("0")) + num4) / (10 + mm4)) * mm4);
        lblConvinBoard.Text = ConvinBoard.ToString("0.0");
        total = (ConvinBoard + num3);
        lblTotal.Text = total.ToString("0");
        lblGrade.Text = grade(double.Parse(total.ToString("0")));

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
            param.Add(new SqlParameter("@MaxMarks4", txtPracMax.Text.Trim()));
            param.Add(new SqlParameter("@MaxMarks3", txtHYMax.Text.Trim()));

            param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
            param.Add(new SqlParameter("@LoginName", Session["LoginName"].ToString()));
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
            msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("SetMaxMinMarksProc_XI", param);
        }
    }
    protected void lnkSubmit_Click(object sender, EventArgs e)
    {
        sql = "Select * from CCEXI_1718 where ClassId='" + drpclass.SelectedValue.ToString() + "' and BranchCode=" + Session["BranchCode"] + " and SubjectId='" + drpSubject.SelectedValue.ToString() + "' and PaperId='" + drpPaper.SelectedValue.ToString() + "' and Evaluation='" + drpEval.SelectedValue.ToString() + "' and SectionName='" + drpsection.SelectedItem.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "'";
        if (oo.Duplicate(sql))
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
                    sql = "Select *from CCEXI_1718 where srno='" + LblSrNo.Text.Trim() + "' and BranchCode=" + Session["BranchCode"] + " and ClassId='" + drpclass.SelectedValue.ToString() + "' and SubjectId='" + drpSubject.SelectedValue.ToString() + "' and PaperId='" + drpPaper.SelectedValue.ToString() + "' and Evaluation='" + drpEval.SelectedValue.ToString() + "' and SectionName='" + drpsection.SelectedItem.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "'";
                    if (oo.Duplicate(sql) == false)
                    {
                        Label LblConv10 = (Label)gvr.FindControl("LblConv10");
                        

                        cmd = new SqlCommand();
                        cmd.CommandText = "USP_CCEXI_1718";
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
                        cmd.CommandText = "USP_CCEXI_Update_1718";
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
        else
        {
            save();
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
                cmd.CommandText = "USP_CCEXI_1718";
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
    protected void drpsection_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadSubjectGroup();

    }
    private void loadSubjectMaxMarks()
    {
        sql = "Select MaxMarks1,MaxMarks2,MaxMarks3,MaxMarks4 from SetMaxMinMarks_XI where Eval='" + drpEval.SelectedValue.ToString().Trim() + "' and BranchCode=" + Session["BranchCode"] + " and SubjectActivityId='" + drpSubject.SelectedValue.ToString().Trim() + "' and PaperId='" + drpPaper.SelectedValue.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "'";
        string mm = oo.ReturnTag(sql, "MaxMarks");
        if (GridView1.Rows.Count > 0)
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
    }
    protected void drpSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadgrid();
        loadMarks();
        hideColumn();
    }
    protected void loadMarks()
    {
        if (GridView1.Rows.Count > 0)
        {
            loadSubjectMaxMarks();
            sql = "Select * from CCEXI_1718 where ClassId='" + drpclass.SelectedValue.ToString() + "' and SubjectId='" + drpSubject.SelectedValue.ToString() + "' and PaperId='" + drpPaper.SelectedValue.ToString() + "' and BranchCode=" + Session["BranchCode"] + " and Evaluation='" + drpEval.SelectedValue.ToString() + "' and SectionName='" + drpsection.SelectedItem.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "'";
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
                int mm1 = 0, mm2 = 0, mm3 = 0, mm4 = 0;
                double num1 = 0, num2 = 0, num3 = 0, num4 = 0;
                int.TryParse(txt1.Text, out mm1); int.TryParse(txt2.Text, out mm2); int.TryParse(txtPrac.Text, out mm3); int.TryParse(txtHYMax.Text, out mm4);

                sql = "Select Id,Test1,Test2,SAT,Prac from CCEXI_1718 where SRNO='" + lblsrno.Text + "' and Evaluation='" + drpEval.SelectedValue.ToString().Trim() + "' and BranchCode=" + Session["BranchCode"] + " and SubjectId='" + drpSubject.SelectedValue.ToString() + "' and PaperId='" + drpPaper.SelectedValue.ToString() + "'";

                LblId.Text = oo.ReturnTag(sql, "Id");
                Text1.Text = oo.ReturnTag(sql, "Test1");
                Text2.Text = oo.ReturnTag(sql, "Test2");
                txtPrac.Text = oo.ReturnTag(sql, "Prac");
                txtHY.Text = oo.ReturnTag(sql, "SAT");
                double.TryParse(Text1.Text, out num1); double.TryParse(Text2.Text, out num2); double.TryParse(txtPrac.Text, out num3); double.TryParse(txtHY.Text, out num4);

                double totalmarks = num1 > num2 ? num1 : num2;
                double totalmmmarks = num1 > num2 ? mm1 : mm2;

                double convin10 = 0;
                if (totalmmmarks > 0)
                {
                    convin10 = (totalmarks * 10) / totalmmmarks;
                }
                LblConv10.Text = (convin10).ToString("0.0");

                double ConvinBoard = 0;
                if (mm4 > 0)
                {
                    
                    ConvinBoard = (((double.Parse(convin10.ToString("0")) + num4) / (10 + mm4)) * mm4);
                }
                lblConvinBoard.Text = ConvinBoard.ToString("0.0");

                lblTotal.Text = (ConvinBoard + num3).ToString("0");
                lblGrade.Text = grade(double.Parse((ConvinBoard + num3).ToString("0")));
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
    protected void drpEval_SelectedIndexChanged(object sender, EventArgs e)
    {
        
       
    }
    private void hideColumn()
    {
        Label lblUt1 = (Label)GridView1.HeaderRow.FindControl("lblUt1");
        Label lblUt2 = (Label)GridView1.HeaderRow.FindControl("lblUt2");
        Label LblConv10H = (Label)GridView1.HeaderRow.FindControl("LblConv10H");
        Label lblPrac = (Label)GridView1.HeaderRow.FindControl("lblPrac");
        Label lblHY = (Label)GridView1.HeaderRow.FindControl("lblHY");
        Label lblConvinBoard = (Label)GridView1.HeaderRow.FindControl("lblConvinBoard");
        string sqlAd = "select count(*) cnt from TTSubjectMaster where IsAditional=1 and Id=" + drpSubject.SelectedValue + " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and classid=" + drpclass.SelectedValue + " and BranchId=" + drpBranch.SelectedValue + "";
        string isAdditional = "No";
        if (int.Parse(oo.ReturnTag(sqlAd, "cnt")) > 0)
        {
            isAdditional = "Yes";
        }
        if (drpEval.SelectedIndex == 0)
        {
            lblUt1.Text = "U.T.-1";
            lblUt2.Text = "U.T.-2";
            if (isAdditional == "Yes")
            {
                LblConv10H.Text = "Conv. into (5)";
            }
            else
            {
                LblConv10H.Text = "Conv. into (10)";
            }
            lblHY.Text = "Term-1";
        }
        else
        {
            lblUt1.Text = "U.T.-3";
            lblUt2.Text = "U.T.-4";
            if (isAdditional == "Yes")
            {
                LblConv10H.Text = "Conv. into (5)";
            }
            else
            {
                LblConv10H.Text = "Conv. into (10)";
            }
            lblHY.Text = "Term-2";

        }
    }
    protected void drpSubjectGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadSubject();
    }

    protected void drpBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadsection();
    }

}