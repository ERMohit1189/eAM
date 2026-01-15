using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

public partial class admin_ParticularStudentMarksItoV : Page
{
    SqlConnection con = new SqlConnection();
    SqlCommand cmd = new SqlCommand();
    Campus oo = new Campus();
    string sql = "";

    protected void Page_PreInIt(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (Session["Logintype"] == null) { Response.Redirect("~/default.aspx"); }
        if (Session["Logintype"].ToString() == "Admin")
        {
            this.MasterPageFile = "~/Master/admin_root-manager.master";
        }
        else if (Session["Logintype"].ToString() == "Staff")
        {
            this.MasterPageFile = "~/Staff/staff_root-manager.master";
        }
        else if (Session["Logintype"].ToString() == "Guardian")
        {
            this.MasterPageFile = "~/Sp/sp_root-manager.master";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        con = oo.dbGet_connection();
        BLL.BLLInstance.LoadHeader("Examination", header1);
        if (!IsPostBack)
        {
            Label1.Text = Label1.Text + " " + Session["SessionName"].ToString();
            loadGroup();
            loadGrid();
            loadCoscholasicGroup();
            loadCoscholasicGrid();
            studentinfo();
            getStudentAttendence(lbla1, "SEPT.");
            getStudentAttendence(lbla2, "FEB");
            getRemark();
        }
        if (Session["Logintype"].ToString() == "Guardian")
        {
            divPrint.Visible = false;
        }
        else
        {
            divPrint.Visible = true;
        }
    }

    public void getStudentAttendence(Label lblId,string eval1)
    {
        if (GridView3.Rows.Count > 0)
        {
            Label Label31 = (Label)GridView3.FooterRow.FindControl("Label31");
            Label lblsrno = new Label();
            lblsrno.Text = Session["srno"].ToString();
            string srno = "", evalname = "", sessionname = "";
            string classid = "-1";
            srno = lblsrno.Text;
            evalname = eval1;
            classid = Session["classid"].ToString();
            sessionname = Session["SessionName"].ToString();
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@srno", srno));
            param.Add(new SqlParameter("@classid", classid));
            param.Add(new SqlParameter("@evalname", evalname));
            param.Add(new SqlParameter("@sessionname", sessionname));

            try
            {
                lblId.Text = DLL.objDll.Sp_SelectRecord_usingExecuteScalar("GetAttendenceproc", param).ToString();
            }
            catch
            {
                lblId.Text = "";
            }

        }
    }

    public void getRemark()
    {

        DataSet ds = new DataSet();

        ds = getRemark(Session["srno"].ToString(), "HY");

        if (ds != null)
        {
            DataTable dt = new DataTable();
            dt = ds.Tables[0];

            DataView dv = new DataView(dt);
            if (dv.Count > 0)
            {
                dv.RowFilter = "RemarkFor='generalremark'";

                lblHY.Text = dv[0]["Caption"].ToString();
            }
        }

        ds = new DataSet();

        ds = getRemark(Session["srno"].ToString(), "AE");

        if (ds != null)
        {
            DataTable dt = new DataTable();
            dt = ds.Tables[0];

            DataView dv = new DataView(dt);
            if (dv.Count > 0)
            {
                dv.RowFilter = "RemarkFor='generalremark'";

                lblAE.Text = dv[0]["Caption"].ToString();
            }
        }
    }

    public DataSet getRemark(string srno, string evaluation)
    {
        List<SqlParameter> param = new List<SqlParameter>();

        param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
        param.Add(new SqlParameter("@SrNo", srno));
        param.Add(new SqlParameter("@Eval", evaluation));

        return DLL.objDll.Sp_SelectRecord_usingExecuteDataset("USP_Get_NURtoPREPRemarkUsingSrno", param);
    }

    public void studentinfo()
    {
        sql = "select SC.SectionName,SO.Card,SO.Medium as Medium,CM.ClassName,Sf.FatherName,(SG.FirstName+' '+SG.MiddleName+' '+SG.LastName) as Name,sg.srno as srno,sg.StLocalAddress as Address,";
        sql +=  " SG.Height as H,SG.Weight as W,SG.BloodGroup as BG,SG.VisionL as L,SG.VisionR as R,SG.DentalHygiene as T,Convert(Varchar(11),SG.DOB,106) as D from StudentGenaralDetail SG";
        sql +=  " left join StudentFamilyDetails SF on SG.srno=SF.srno";
        sql +=  " left join StudentOfficialDetails SO on SG.srno=SO.srno";
        sql +=  " left join ClassMaster CM on SO.AdmissionForClassId=CM.Id";
        sql +=  " left join SectionMaster SC on SO.SectionId=SC.Id";
        sql +=  " where  SG.srno='" + Session["srno"].ToString() + "'";
        sql +=  " and sg.SessionName='" + Session["SessionName"].ToString() + "' and ";
        sql +=  " so.SessionName='" + Session["SessionName"].ToString() + "' and sf.SessionName='" + Session["SessionName"].ToString() + "' and cm.SessionName='" + Session["SessionName"].ToString() + "'";
        sql +=  " and SC.SessionName='" + Session["SessionName"].ToString() + "' and sc.BranchCode=" + Session["BranchCode"] + " and cm.BranchCode=" + Session["BranchCode"] + " and sg.BranchCode=" + Session["BranchCode"] + " and so.BranchCode=" + Session["BranchCode"] + " and sf.BranchCode=" + Session["BranchCode"] + "  and";
        sql +=  " sg.BranchCode=" + Session["BranchCode"].ToString() + "";
        sql +=  " and SO.Withdrwal is null";

        DataList1.DataSource = oo.GridFill(sql);
        DataList1.DataBind();
    }
    public void loadGroup()
    {
        sql = "Select SubjectGroup,Id from SubjectGroupMaster where ClassId='" + Session["ClassId"].ToString() + "' and BranchCode=" + Session["BranchCode"] + " and SectionName='" + Session["SectionName"].ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "' and IsForOnlyExam=1 order by displayOrder Asc";
        GridView2.DataSource = oo.GridFill(sql);
        GridView2.DataBind();
    }
    public void loadCoscholasicGroup()
    {

            sql = "Select CoscholasticGroup,Id from CoscholasticGroupMaster where ClassId='" + Session["ClassId"].ToString() + "' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"].ToString() + "'";
            GridView3.DataSource = oo.GridFill(sql);
            GridView3.DataBind();
        
    }
    public void loadGrid()
    {
        if (GridView2.Rows.Count > 0)
        {
            double may = 0, aug = 0, sep = 0, dec = 0, jan = 0, feb = 0;
            Label Label52 = (Label)GridView2.FooterRow.FindControl("Label52");
            Label Label53 = (Label)GridView2.FooterRow.FindControl("Label53");
            Label Label54 = (Label)GridView2.FooterRow.FindControl("Label54");
            Label Label55 = (Label)GridView2.FooterRow.FindControl("Label55");
            Label Label56 = (Label)GridView2.FooterRow.FindControl("Label56");
            Label Label57 = (Label)GridView2.FooterRow.FindControl("Label57");
            Label Label58 = (Label)GridView2.FooterRow.FindControl("Label58");
            Label Label59 = (Label)GridView2.FooterRow.FindControl("Label59");
            //------------------------------------------------------------------
            //Label Label60 = (Label)GridView2.FooterRow.FindControl("Label60");
            Label Label61 = (Label)GridView2.FooterRow.FindControl("Label61");
            Label Label62 = (Label)GridView2.FooterRow.FindControl("Label62");
            Label Label63 = (Label)GridView2.FooterRow.FindControl("Label63");
            Label Label64 = (Label)GridView2.FooterRow.FindControl("Label64");
            Label Label65 = (Label)GridView2.FooterRow.FindControl("Label65");
            Label Label66 = (Label)GridView2.FooterRow.FindControl("Label66");
            Label Label67 = (Label)GridView2.FooterRow.FindControl("Label67");
            Label Label68 = (Label)GridView2.FooterRow.FindControl("Label68");

            //-------------------------------------------------------------------
            double maxmarks1 = 0;
            double marks = 0, marks1 = 0, marks2 = 0, marks3 = 0, marks4 = 0, marks5 = 0;
            double maxmarks = 0;
            for (int j = 0; j < GridView2.Rows.Count; j++)
            {
                GridView GridView1 = (GridView)GridView2.Rows[j].FindControl("GridView1");
                Label lblgrpId = (Label)GridView2.Rows[j].FindControl("Label29");
                //sql = "Select SubjectName,Id as SubjectId,MaxMarks from SubjectMaster where GroupId='" + lblgrpId.Text + "' and ClassId='" + Session["ClassId"].ToString() + "' and SectionName='" + Session["SectionName"].ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "' order by displayOrder Asc";
                sql = "Select SubjectName,sm.Id as SubjectId,sm.MaxMarks from SubjectMaster sm";
                sql +=  " Left join S02_SubjectPaperMaster spm on spm.S02ID=sm.PaperID and spm.SessionName=sm.SessionName";
                sql +=  " where GroupId='" + lblgrpId.Text + "' and sm.ClassId='" + Session["ClassId"].ToString() + "' ";
                sql +=  " and sm.SectionName='" + Session["SectionName"].ToString() + "' and spm.BranchCode=" + Session["BranchCode"] + " and sm.BranchCode=" + Session["BranchCode"] + " and sm.SessionName='" + Session["SessionName"].ToString() + "' ";
                sql +=  " and (IsForExam is null or IsForExam ='1')";
                sql +=  " order by DisplayOrder Asc"; 
                GridView1.DataSource = oo.GridFill(sql);
                GridView1.DataBind();

                if (GridView1.Rows.Count > 0)
                {
                    maxmarks = 0;
                    for (int i = 0; i < GridView1.Rows.Count; i++)
                    {
                        Label SubjectId = (Label)GridView1.Rows[i].FindControl("Label2");
                        Label lblMaxMarks = (Label)GridView1.Rows[i].FindControl("Label32");
                        if (lblMaxMarks.Text != "")
                        {
                            maxmarks = maxmarks + Convert.ToDouble(lblMaxMarks.Text);
                            maxmarks1 = maxmarks1 + Convert.ToDouble(lblMaxMarks.Text);
                        }
                        loadFA1Data(GridView1, i, SubjectId.Text, "MAY/JULY", Session["srno"].ToString(), "Label4", "Label5");
                        loadFA1Data(GridView1, i, SubjectId.Text, "AUG", Session["srno"].ToString(), "Label8", "Label9");
                        loadFA1Data(GridView1, i, SubjectId.Text, "SEPT.", Session["srno"].ToString(), "Label12", "Label13");
                        loadFA1Data(GridView1, i, SubjectId.Text, "DEC", Session["srno"].ToString(), "Label18", "Label19");
                        loadFA1Data(GridView1, i, SubjectId.Text, "JAN", Session["srno"].ToString(), "Label21", "Label22");
                        loadFA1Data(GridView1, i, SubjectId.Text, "FEB", Session["srno"].ToString(), "Label24", "Label25");

                        GetAvrage(GridView1, i, lblMaxMarks.Text, "Label4", "Label8", "Label12", "Label50", "Label51");
                        GetAvrage(GridView1, i, lblMaxMarks.Text, "Label18", "Label21", "Label24", "Label27", "Label28");

                    }
                    bool flag;
                    double value;
                    Label lblTotalMaxMarks = (Label)GridView1.FooterRow.FindControl("Label33");
                    lblTotalMaxMarks.Text = maxmarks.ToString(CultureInfo.CurrentCulture);
                    GridView1.FooterRow.Cells[0].Text = "Total";
                    marks = 0; marks1 = 0; marks2 = 0; marks3 = 0; marks4 = 0; marks5 = 0;
                    int isnad1 = 0, isnad2 = 0, isnad3 = 0, isnad4 = 0, isnad5 = 0, isnad6 = 0;
                    int isml1 = 0, isml2 = 0, isml3 = 0, isml4 = 0, isml5 = 0, isml6 = 0;
                    for (int i = 0; i < GridView1.Rows.Count; i++)
                    {
                        isnad1 = 0; isnad2 = 0; isnad3 = 0; isnad4 = 0; isnad5 = 0; isnad6 = 0;
                        isml1 = 0; isml2 = 0; isml3 = 0; isml4 = 0; isml5 = 0; isml6 = 0;

                        Label lblMarks = (Label)GridView1.Rows[i].FindControl("Label4");
                        Label lblMarks1 = (Label)GridView1.Rows[i].FindControl("Label8");
                        Label lblMarks2 = (Label)GridView1.Rows[i].FindControl("Label12");


                        Label lblMarks3 = (Label)GridView1.Rows[i].FindControl("Label18");
                        Label lblMarks4 = (Label)GridView1.Rows[i].FindControl("Label21");
                        Label lblMarks5 = (Label)GridView1.Rows[i].FindControl("Label24");

                        if (lblMarks.Text != "")
                        {
                            if (lblMarks.Text == "NAD")
                            {
                                isnad1 = 1;
                            }
                            else if (lblMarks.Text == "ML")
                            {                               
                                isml1 = 1;
                            }
                            else
                            {
                                flag = double.TryParse(lblMarks.Text, out value);
                                if (flag == true)
                                {
                                    marks = marks + Convert.ToDouble(lblMarks.Text);
                                }
                            }
                        }
                        if (lblMarks1.Text != "")
                        {
                            if (lblMarks1.Text == "NAD")
                            {
                                isnad2 = 1;
                            }
                            else if (lblMarks1.Text == "ML")
                            {
                                isml2 = 1;
                            }
                            else
                            {
                                flag = double.TryParse(lblMarks1.Text, out value);
                                if (flag == true)
                                {
                                    marks1 = marks1 + Convert.ToDouble(lblMarks1.Text);
                                }
                            }
                        }
                        if (lblMarks2.Text != "")
                        {
                            if (lblMarks2.Text == "NAD")
                            {
                                isnad3 = 1;
                            }
                            else if (lblMarks2.Text == "ML")
                            {
                                isml3 = 1;
                            }
                            else
                            {
                                flag = double.TryParse(lblMarks2.Text, out value);
                                if (flag == true)
                                {
                                    marks2 = marks2 + Convert.ToDouble(lblMarks2.Text);
                                }
                            }
                        }
                        if (lblMarks3.Text != "")
                        {
                            if (lblMarks3.Text == "NAD")
                            {
                                isnad4 = 1;
                            }
                            else if (lblMarks3.Text == "ML")
                            {
                                isml4 = 1;
                            }
                            else
                            {
                                flag = double.TryParse(lblMarks3.Text, out value);
                                if (flag == true)
                                {
                                    marks3 = marks3 + Convert.ToDouble(lblMarks3.Text);
                                }
                            }
                        }
                        if (lblMarks4.Text != "")
                        {
                            if (lblMarks4.Text == "NAD")
                            {
                                isnad5 = 1;
                            }
                            else if (lblMarks4.Text == "ML")
                            {
                                isml5 = 1;
                            }
                            else
                            {
                                flag = double.TryParse(lblMarks4.Text, out value);
                                if (flag == true)
                                {
                                    marks4 = marks4 + Convert.ToDouble(lblMarks4.Text);
                                }
                            }
                        }
                        if (lblMarks5.Text != "")
                        {
                            if (lblMarks5.Text == "NAD")
                            {
                                isnad6 = 1;
                            }
                            else if (lblMarks5.Text == "ML")
                            {
                                isml6 = 1;
                            }
                            else
                            {
                                flag = double.TryParse(lblMarks5.Text, out value);
                                if (flag == true)
                                {
                                    marks5 = marks5 + Convert.ToDouble(lblMarks5.Text);
                                }
                            }
                        }
                    }
                    int count1 = 0;
                    int count2 = 0;
                    if (isnad1 == 0 && isml1==0)
                    {
                        Label lblGetTotalMarks = (Label)GridView1.FooterRow.FindControl("Label34");
                        lblGetTotalMarks.Text = marks.ToString(CultureInfo.CurrentCulture);
                        gradefortotal(maxmarks, marks, "Label35", GridView1);
                        count1 = count1 + 1;
                    }
                    may = may + marks;
                    if (isnad2 == 0 && isml2 == 0)
                    {
                        Label lblGetTotalMarks1 = (Label)GridView1.FooterRow.FindControl("Label36");
                        lblGetTotalMarks1.Text = marks1.ToString(CultureInfo.CurrentCulture);
                        if (lblGetTotalMarks1.Text == "0")
                        {
                            lblGetTotalMarks1.Text = "";
                        }
                        else
                        {
                            gradefortotal(maxmarks, marks1, "Label37", GridView1);
                            count1 = count1 + 1;
                        }
                    }
                    aug = aug + marks1;
                    if (isnad3 == 0 && isml3 == 0)
                    {
                        Label lblGetTotalMarks2 = (Label)GridView1.FooterRow.FindControl("Label38");
                        lblGetTotalMarks2.Text = marks2.ToString(CultureInfo.CurrentCulture);
                        if (lblGetTotalMarks2.Text == "0")
                        {
                            lblGetTotalMarks2.Text = "";
                        }
                        else
                        {
                            gradefortotal(maxmarks, marks2, "Label39", GridView1);
                            count1 = count1 + 1;
                        }
                   
                    }
                    sep = sep + marks2;
                    if (isnad4 == 0 && isml4 == 0)
                    {
                        Label lblGetTotalMarks3 = (Label)GridView1.FooterRow.FindControl("Label42");
                        lblGetTotalMarks3.Text = marks3.ToString(CultureInfo.CurrentCulture);
                        if (lblGetTotalMarks3.Text == "0")
                        {
                            lblGetTotalMarks3.Text = "";
                        }
                        else
                        {
                            gradefortotal(maxmarks, marks3, "Label43", GridView1);
                            count2 = count2 + 1;
                        }
                    }
                    dec = dec + marks3;
                    if (isnad5 == 0 && isml5 == 0)
                    {
                        Label lblGetTotalMarks4 = (Label)GridView1.FooterRow.FindControl("Label44");
                        lblGetTotalMarks4.Text = marks4.ToString(CultureInfo.CurrentCulture);
                        if (lblGetTotalMarks4.Text == "0")
                        {
                            lblGetTotalMarks4.Text = "";
                        }
                        else
                        {
                            gradefortotal(maxmarks, marks4, "Label45", GridView1);
                            count2 = count2 + 1;
                        }
                    }
                    jan = jan + marks4;
                    if (isnad6 == 0 && isml6 == 0)
                    {
                        Label lblGetTotalMarks5 = (Label)GridView1.FooterRow.FindControl("Label46");
                        lblGetTotalMarks5.Text = marks5.ToString(CultureInfo.CurrentCulture);
                        if (lblGetTotalMarks5.Text == "0")
                        {
                            lblGetTotalMarks5.Text = "";
                        }
                        else
                        {
                            gradefortotal(maxmarks, marks5, "Label47", GridView1);
                            count2 = count2 + 1;
                        }
                    }
                    feb = feb + marks5;                              
                    countTotalAvrageMarks(GridView1,count1,count2);
                }
            }
            Label lblGrandTotal = (Label)GridView2.FooterRow.FindControl("Label26");
    
            lblGrandTotal.Text = " " + maxmarks1.ToString(CultureInfo.CurrentCulture);
            try
            {
                Label52.Text = may.ToString(CultureInfo.CurrentCulture);
                if (Label52.Text == "0")
                {
                    Label52.Text = "";
                }
                else
                {
                    Label53.Text = grade(may * 100 / maxmarks1);
                }
            }
            catch
            {
            }
            try
            {
                Label54.Text = aug.ToString(CultureInfo.CurrentCulture);
                if (Label54.Text == "0")
                {
                    Label54.Text = "";
                }
                else
                {
                    Label55.Text = grade(aug * 100 / maxmarks1);
                }
            }
            catch
            {
            }
            try
            {
                Label56.Text = sep.ToString(CultureInfo.CurrentCulture);
                if (Label56.Text == "0")
                {
                    Label56.Text = "";
                }
                else
                {
                    Label57.Text = grade(sep * 100 / maxmarks1);
                }
            }
            catch
            {
            }
            try
            {
                Label58.Text = Math.Ceiling(((may + aug + sep))).ToString(CultureInfo.CurrentCulture);
            }
            catch
            {
            }
            try
            {
                double percentle = ((may + aug + sep) * 100) / (maxmarks1 * 3);
                if (Label58.Text == "0")
                {
                    Label58.Text = "";
                }
                else
                {
                    Label59.Text = grade(Math.Ceiling(percentle));
                }
            }
            catch
            {
            }

            //-------------------------------------------------------------------------

            try
            {
                Label61.Text = dec.ToString(CultureInfo.CurrentCulture);
                if (Label61.Text == "0")
                {
                    Label61.Text = "";
                }
                else
                {
                    Label62.Text = grade(dec * 100 / maxmarks1);
                }
            }
            catch
            {
            }
            try
            {
                Label63.Text = jan.ToString(CultureInfo.CurrentCulture);
                if (Label63.Text == "0")
                {
                    Label63.Text = "";
                }
                else
                {
                    Label64.Text = grade(jan * 100 / maxmarks1);
                }
            }
            catch
            {
            }
            try
            {
                Label65.Text = feb.ToString(CultureInfo.CurrentCulture);
                if (Label65.Text == "0")
                {
                    Label65.Text = "";
                }
                else
                {
                    Label66.Text = grade(feb * 100 / maxmarks1);
                }
            }
            catch
            {
            }
            try
            {
                Label67.Text = Math.Ceiling(((dec + jan + feb))).ToString(CultureInfo.CurrentCulture);
            }
            catch
            {
            }
            try
            {
                double percentle = ((dec + jan + feb) * 100) / (maxmarks1 * 3);
                if (Label67.Text == "0")
                {
                    Label67.Text = "";
                }
                else
                {
                    Label68.Text = grade(Math.Ceiling(percentle));
                }
            }
            catch
            {
            }
        }
        calculateTotalGrade();
    }

    private void calculateTotalGrade()
    {
        if (GridView2.Rows.Count > 0)
        {
            double mm1 = 0;
            double may = 0;
            double aug = 0;
            double sep = 0;

            double mm2 = 0;
            double dec = 0;
            double jan = 0;
            double feb = 0;

            for (int i = 0; i < GridView2.Rows.Count; i++)
            {
                GridView GridView1 = (GridView)GridView2.Rows[i].FindControl("GridView1");
                

                for (int j = 0; j < GridView1.Rows.Count; j++)
                {
                    double value = 0;

                    Label lblMM = (Label)GridView1.Rows[j].FindControl("Label32");

                    Label lblMay = (Label)GridView1.Rows[j].FindControl("Label4");

                    Label lblAug = (Label)GridView1.Rows[j].FindControl("Label8");

                    Label lblSep = (Label)GridView1.Rows[j].FindControl("Label12");

                    if (lblMay.Text != "NAD" && lblMay.Text != "ML" && lblMay.Text != "")
                    {
                        mm1 = mm1 + (double.TryParse(lblMM.Text, out value) ? value : 0);

                        double value1 = double.TryParse(lblMay.Text, out value) ? value : 0;
                        may = may + value1;                      
                    }
                    if (lblAug.Text != "NAD" && lblAug.Text != "ML" && lblAug.Text != "")
                    {
                        mm1 = mm1 + (double.TryParse(lblMM.Text, out value) ? value : 0);

                        double value1 = double.TryParse(lblAug.Text, out value) ? value : 0;
                        aug = aug + value1;                                              
                    }
                    if (lblSep.Text != "NAD" && lblSep.Text != "ML" && lblSep.Text != "")
                    {
                        mm1 = mm1 + (double.TryParse(lblMM.Text, out value) ? value : 0);

                        double value1 = double.TryParse(lblSep.Text, out value) ? value : 0;
                        sep = sep + value1;
                        
                    }

                    Label lblDec = (Label)GridView1.Rows[j].FindControl("Label18");

                    Label lblJan = (Label)GridView1.Rows[j].FindControl("Label21");

                    Label lblFeb = (Label)GridView1.Rows[j].FindControl("Label24");


                    if (lblDec.Text != "NAD" && lblDec.Text != "ML" && lblDec.Text != "")
                    {
                        mm2 = mm2 + (double.TryParse(lblMM.Text, out value) ? value : 0);

                        double value1 = double.TryParse(lblDec.Text, out value) ? value : 0;
                        dec = dec + value1;
                    }
                    if (lblJan.Text != "NAD" && lblJan.Text != "ML" && lblJan.Text != "")
                    {
                        mm2 = mm2 + (double.TryParse(lblMM.Text, out value) ? value : 0);

                        double value1 = double.TryParse(lblJan.Text, out value) ? value : 0;
                        jan = jan + value1;
                    }
                    if (lblFeb.Text != "NAD" && lblFeb.Text != "ML" && lblFeb.Text != "")
                    {
                        mm2 = mm2 + (double.TryParse(lblMM.Text, out value) ? value : 0);

                        double value1 = double.TryParse(lblFeb.Text, out value) ? value : 0;
                        feb = feb + value1;
                    }
                }
            }

            Label lblAvg1 = (Label)GridView2.FooterRow.FindControl("Label59");

            lblAvg1.Text = grade(((may + aug + sep) * 100) / mm1);


            Label lblAvg2 = (Label)GridView2.FooterRow.FindControl("Label68");

            lblAvg2.Text = grade(((dec + jan + feb) * 100) / mm2);
        }
    }

    

    protected void GetAvrage(GridView GridView1, int i, string maxmarks, string test1, string test2, string test3, string avgMarks, string grade)
    {
        try
        {
            Label Test1 = (Label)GridView1.Rows[i].FindControl(test1);
            Label Test2 = (Label)GridView1.Rows[i].FindControl(test2);
            Label Test3 = (Label)GridView1.Rows[i].FindControl(test3);
            Label Avgmarks = (Label)GridView1.Rows[i].FindControl(avgMarks);

            bool flag;
            double output;
            double test1marks = 0, test2marks = 0, test3marks = 0;
            flag = double.TryParse(Test1.Text, out output);
            if (flag)
            {
                test1marks = output;
            }
            flag = double.TryParse(Test2.Text, out output);
            if (flag)
            {
                test2marks = output;
            }
            flag = double.TryParse(Test3.Text, out output);
            if (flag)
            {
                test3marks = output;
            }
        
            double avgmarks = test1marks + test2marks + test3marks;
            if (!string.IsNullOrEmpty(Test1.Text) || !string.IsNullOrEmpty(Test2.Text) || !string.IsNullOrEmpty(Test2.Text))
            {
                Avgmarks.Text = Math.Round(avgmarks, 0).ToString(CultureInfo.CurrentCulture);
                if ((Test1.Text != "NAD" || Test1.Text != "ML" && Test1.Text != "") && (Test2.Text == "NAD" || Test2.Text == "ML" || Test2.Text == "") && (Test3.Text == "NAD" || Test3.Text == "ML" || Test3.Text == ""))
                {
                    avgGrade(i, Convert.ToDouble(maxmarks), Convert.ToDouble(Avgmarks.Text), grade, GridView1,1);
                }
                else if ((Test1.Text == "NAD" || Test1.Text == "ML" || Test1.Text == "") && (Test2.Text != "NAD" || Test2.Text != "ML" || Test2.Text != "") && (Test3.Text == "NAD" || Test3.Text == "ML" || Test3.Text == ""))
                {
                    avgGrade(i, Convert.ToDouble(maxmarks), Convert.ToDouble(Avgmarks.Text), grade, GridView1,1);
                }
                else if ((Test1.Text == "NAD" || Test1.Text == "ML" || Test1.Text == "") && (Test2.Text == "NAD" || Test2.Text == "ML" || Test2.Text == "") && (Test3.Text != "NAD" || Test3.Text != "ML" || Test3.Text != ""))
                {
                    avgGrade(i, Convert.ToDouble(maxmarks), Convert.ToDouble(Avgmarks.Text), grade, GridView1,1);
                }
                else if ((Test1.Text != "NAD" || Test1.Text != "ML" || Test1.Text != "") && (Test2.Text != "NAD" || Test2.Text != "ML" || Test2.Text != "") && (Test3.Text == "NAD" || Test3.Text == "ML" || Test3.Text == ""))
                {
                    avgGrade(i, Convert.ToDouble(maxmarks), Convert.ToDouble(Avgmarks.Text), grade, GridView1,2);
                }
                else if ((Test1.Text == "NAD" || Test1.Text == "ML" || Test1.Text == "") && (Test2.Text != "NAD" || Test2.Text != "ML" || Test2.Text != "") && (Test3.Text != "NAD" || Test3.Text != "ML" || Test3.Text != ""))
                {
                    avgGrade(i, Convert.ToDouble(maxmarks), Convert.ToDouble(Avgmarks.Text), grade, GridView1,2);
                }
                else if ((Test1.Text != "NAD" || Test1.Text != "ML" || Test1.Text != "") && (Test2.Text == "NAD" || Test2.Text == "ML" || Test2.Text == "") && (Test3.Text != "NAD" || Test3.Text != "ML" || Test3.Text != ""))
                {
                    avgGrade(i, Convert.ToDouble(maxmarks), Convert.ToDouble(Avgmarks.Text), grade, GridView1, 2);
                }
                else 
                {
                    avgGrade(i, Convert.ToDouble(maxmarks), Convert.ToDouble(Avgmarks.Text), grade, GridView1,3);
                }
                
            }
        }
        catch (Exception ex)
        {
            oo.MessageBoxforUpdatePanel(ex.Message, this.Page);
        }
            
    }

    public void countTotalAvrageMarks(GridView GridView1, int multiplyby1,int multiplyby2)
    {
        double avgmarks1 = 0, avgmarks2 = 0;
        bool flag1=false, flag2=false;
        double value = 0;
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
           
            Label Avgmarks1 = (Label)GridView1.Rows[i].FindControl("Label50");
            Label Avgmarks2 = (Label)GridView1.Rows[i].FindControl("Label27");
            if(!string.IsNullOrEmpty(Avgmarks1.Text))
            {
                value = 0;
                avgmarks1 = avgmarks1 + (double.TryParse(Avgmarks1.Text, out value) ? value : 0);
                flag1 = true;
            }
            if (!string.IsNullOrEmpty(Avgmarks2.Text))
            {
                value = 0;
                avgmarks2 = avgmarks2 + (double.TryParse(Avgmarks2.Text, out value) ? value : 0);
                flag2 = true;
            }
        }
        Label lblTotalMaxMarks = (Label)GridView1.FooterRow.FindControl("Label33");
        if (flag1)
        {
            Label lblTotalAvgmarks1MaxMarks = (Label)GridView1.FooterRow.FindControl("Label40");
            lblTotalAvgmarks1MaxMarks.Text = avgmarks1.ToString(CultureInfo.CurrentCulture);
            if (!string.IsNullOrEmpty(lblTotalMaxMarks.Text))
            {
                gradefortotal((Convert.ToDouble(lblTotalMaxMarks.Text) * multiplyby1), avgmarks1, "Label41", GridView1);
            }
            
        }
        if (flag2)
        {
            Label lblTotalAvgmarks2MaxMarks = (Label)GridView1.FooterRow.FindControl("Label48");
            lblTotalAvgmarks2MaxMarks.Text = avgmarks2.ToString(CultureInfo.CurrentCulture);
            if (!string.IsNullOrEmpty(lblTotalMaxMarks.Text))
            {
                gradefortotal((Convert.ToDouble(lblTotalMaxMarks.Text) * multiplyby2), avgmarks2, "Label49", GridView1);
            }
        }             
    }

    public void avgGrade(int i,double maxmarks, double marks, string lbl, GridView Grd, int maxmarksmultiplyby)
    {
        double percentle = ((marks) * 100) / (maxmarks * maxmarksmultiplyby);
        Label Grade = (Label)Grd.Rows[i].FindControl(lbl);
        Grade.Text = grade(percentle);
        
    }
    public void gradefortotal(double maxmarks, double marks, string lbl, GridView Grd)
    {
        
        double percentle = ((marks) * 100) / maxmarks;
        Label lblfooterforgrade = (Label)Grd.FooterRow.FindControl(lbl);
        lblfooterforgrade.Text = grade(percentle);
    }

    public string grade(double percentle)
    {
        if (percentle < 35)
        {
            return "D";
        }
        else if (percentle >= 35 && percentle <= 55)
        {
            return "C";
        }
        else if (percentle > 55 && percentle <= 74)
        {
            return "B";
        }
        else if (percentle > 74 && percentle <= 89)
        {
            return "A";
        }
        else if (percentle > 89 && percentle <= 100)
        {
            return "A+";
        }
        else
        {
            return "";
        }
    }

    public void loadCoscholasicGrid()
    {
        if (GridView3.Rows.Count > 0)
        {
            for (int j = 0; j < GridView3.Rows.Count; j++)
            {
                GridView GridView4 = (GridView)GridView3.Rows[j].FindControl("GridView4");
                Label lblgrpId = (Label)GridView3.Rows[j].FindControl("Label6");
                sql = "Select CoscholasticName,Id as CoscholasticId,MaxMarks from CoscholasticMaster where GroupId='" + lblgrpId.Text + "' and BranchCode=" + Session["BranchCode"] + " and ClassId='" + Session["ClassId"].ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "'";

                GridView4.DataSource = oo.GridFill(sql);
                GridView4.DataBind();

                if (GridView4.Rows.Count > 0)
                {
                    for (int i = 0; i < GridView4.Rows.Count; i++)
                    {
                        Label CoscholasticId = (Label)GridView4.Rows[i].FindControl("Label23");


                        Label Avgg1 = (Label)GridView4.Rows[i].FindControl("Label18");
                        Label Avgg2 = (Label)GridView4.Rows[i].FindControl("Label22");

                        Avgg1.Text = getTotalGradeofCoscholastic(loadFA1Data1(GridView4, i, CoscholasticId.Text, "MAY/JULY", Session["srno"].ToString(), "Label15"), loadFA1Data1(GridView4, i, CoscholasticId.Text, "AUG", Session["srno"].ToString(), "Label16"), loadFA1Data1(GridView4, i, CoscholasticId.Text, "SEPT.", Session["srno"].ToString(), "Label17"));
                        Avgg2.Text = getTotalGradeofCoscholastic(loadFA1Data1(GridView4, i, CoscholasticId.Text, "DEC", Session["srno"].ToString(), "Label19"),loadFA1Data1(GridView4, i, CoscholasticId.Text, "JAN", Session["srno"].ToString(), "Label20"),loadFA1Data1(GridView4, i, CoscholasticId.Text, "FEB", Session["srno"].ToString(), "Label21"));
                                                                                             
                    }
                }
            }
        }
    }

    public string loadFA1Data1(GridView GridView4, int i, string CoscholasticId, string Eval, string Srno, string grade)
    {

        Label Grade = (Label)GridView4.Rows[i].FindControl(grade);

        sql = "Select Grade from CCENurtoPREPCoscholastic where SrNo='" + Srno + "' and Evaluation='" + Eval + "' and BranchCode=" + Session["BranchCode"] + " and ClassId='" + Session["ClassId"].ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "' and CoscholasticId='" + CoscholasticId + "'";

        Grade.Text = oo.ReturnTag(sql, "Grade").ToString();

        return Grade.Text;
    }

    public void loadFA1Data(GridView GridView1, int i, string SubjectId, string Eval, string Srno, string test1, string grade)
    {
       
                Label Test1 = (Label)GridView1.Rows[i].FindControl(test1);
                Label Grade = (Label)GridView1.Rows[i].FindControl(grade);
                
                sql = "Select Test1,Grade from CCEPGtoPREP where SrNo='" + Srno + "' and Evaluation='" + Eval + "' and BranchCode=" + Session["BranchCode"] + " and ClassId='" + Session["ClassId"].ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "' and SubjectId='" + SubjectId + "'";

                Test1.Text = oo.ReturnTag(sql, "Test1").ToString();
                Grade.Text = oo.ReturnTag(sql, "Grade").ToString();
           
    }
    
    
   
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        if (GridView2.Rows.Count > 0)
        {
            GridView GridView1 = (GridView)GridView2.Rows[0].FindControl("GridView1");
            if (GridView1.Rows.Count > 0)
            {
                oo.ExportToWord(Response, Session["srno"] + "Report" + ".doc", divExport);
            }
        }
    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = abc;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>var winpop=window.open('../Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        if (GridView2.Rows.Count > 0)
        {
            ExportDivToExcel(Response, Session["srno"] + "Report" + ".xls", divExport);
        }
    }

    public void ExportDivToExcel(HttpResponse rr, string fileName, System.Web.UI.HtmlControls.HtmlGenericControl divExport)
    {

        rr.Clear();
        rr.AddHeader("content-disposition", string.Format("attachment; filename={0}", fileName));
        rr.Charset = "";
        rr.Cache.SetCacheability(HttpCacheability.NoCache);
        rr.ContentType = "application/vnd.xls";
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        System.Web.UI.WebControls.Image img = new System.Web.UI.WebControls.Image();
        String imagepath = "<img src='" + oo.GetImageUrl("~/DisplayImage.ashx?UserLoginID=1", Request.Url.AbsoluteUri.Split('/')) + "' width='100' height='100'/>";
        divExport.RenderControl(htmlWrite);
        rr.Write("<table><tr><td align='center'>" + imagepath + "</td></tr></table>");
        rr.Write(stringWrite.ToString());
        rr.End();
    }

    public string finalgrade(double gradeno)
    {
        return gradeno > 4 ? "A+" : gradeno > 3 ? "A" : gradeno > 2 ? "B" : gradeno > 1 ? "C" : "D";
    }

    private string getTotalGradeofCoscholastic(string grade1, string grade2, string grade3)
    {
        string grade = "";
        string g1dividient = "0";
        double g1divider = 0;

        g1dividient = grade1.ToUpper() == "A+" ? "5" : grade1.ToUpper() == "A" ? "4" : grade1.ToUpper() == "B" ? "3" : grade1.ToUpper() == "C" ? "2" : grade1.ToUpper() == "D" ? "1" :
            grade1.ToUpper() == "NAD" ? "" : grade1.ToUpper() == "ML" ? "" : grade1.ToUpper() == "" ? "" : "0";
        g1divider = g1dividient != "" ? 1 : 0;

        string g2dividient = "0";
        double g2divider = 0;

        g2dividient = grade2.ToUpper() == "A+" ? "5" : grade2.ToUpper() == "A" ? "4" : grade2.ToUpper() == "B" ? "3" : grade2.ToUpper() == "C" ? "2" : grade2.ToUpper() == "D" ? "1" :
            grade2.ToUpper() == "NAD" ? "" : grade2.ToUpper() == "ML" ? "" : grade2.ToUpper() == "" ? "" : "0";
        g2divider = g2dividient != "" ? 1 : 0;


        string g3dividient = "0";
        double g3divider = 0;

        g3dividient = grade3.ToUpper() == "A+" ? "5" : grade3.ToUpper() == "A" ? "4" : grade3.ToUpper() == "B" ? "3" : grade3.ToUpper() == "C" ? "2" : grade3.ToUpper() == "D" ? "1" :
            grade3.ToUpper() == "NAD" ? "" : grade3.ToUpper() == "ML" ? "" : grade3.ToUpper() == "" ? "" : "0";
        g3divider = g3dividient != "" ? 1 : 0;


        if (g1dividient != string.Empty && g2dividient != string.Empty && g3dividient != string.Empty)
        {
            grade = finalgrade((Convert.ToDouble(g1dividient) + Convert.ToDouble(g2dividient) + Convert.ToDouble(g3dividient)) / (g1divider + g2divider + g3divider));
        }

        else if (g1dividient == string.Empty && g2dividient != string.Empty && g3dividient != string.Empty)
        {
            grade = finalgrade((Convert.ToDouble(g2dividient) + Convert.ToDouble(g3dividient)) / (g2divider + g3divider));
        }
        else if (g1dividient != string.Empty && g2dividient == string.Empty && g3dividient != string.Empty)
        {
            grade = finalgrade((Convert.ToDouble(g1dividient) + Convert.ToDouble(g3dividient)) / (g1divider + g3divider));
        }
        else if (g1dividient != string.Empty && g2dividient != string.Empty && g3dividient == string.Empty)
        {
            grade = finalgrade((Convert.ToDouble(g1dividient) + Convert.ToDouble(g2dividient)) / (g1divider + g2divider));
        }


        else if (g1dividient != string.Empty && g2dividient == string.Empty && g3dividient == string.Empty)
        {
            grade = finalgrade(Convert.ToDouble(g1dividient) / (g1divider));
        }
        else if (g1dividient == string.Empty && g2dividient != string.Empty && g3dividient == string.Empty)
        {
            grade = finalgrade(Convert.ToDouble(g2dividient) / (g2divider));
        }
        else if (g1dividient == string.Empty && g2dividient == string.Empty && g3dividient != string.Empty)
        {
            grade = finalgrade(Convert.ToDouble(g3dividient) / (g3divider));
        }

        return grade;
    }

    //private string getTotalGradeofCoscholastic(string grade1, string grade2, string grade3)
    //{
    //    string grade = "";

    //    if (grade1 == "A" && grade2 == "A" && grade3 == "A")
    //    {
    //        grade = "A";
    //    }
    //    else if ((grade1.ToUpper() == "Nad".ToUpper() || grade1.ToUpper() == "Na".ToUpper() || grade1.ToUpper() == "ML".ToUpper()) && grade2 == "A" && grade3 == "A")
    //    {
    //        grade = "A";
    //    }
    //    else if ((grade1.ToUpper() == "Nad".ToUpper() || grade1.ToUpper() == "Na".ToUpper() || grade1.ToUpper() == "ML".ToUpper()) && grade2 == "B" && grade3 == "B")
    //    {
    //        grade = "B";
    //    }
    //    else if ((grade1.ToUpper() == "Nad".ToUpper() || grade1.ToUpper() == "Na".ToUpper() || grade1.ToUpper() == "ML".ToUpper()) && grade2 == "B" && grade3 == "A")
    //    {
    //        grade = "A";
    //    }
    //    else if ((grade1.ToUpper() == "Nad".ToUpper() || grade1.ToUpper() == "Na".ToUpper() || grade1.ToUpper() == "ML".ToUpper()) && grade2 == "C" && grade3 == "B")
    //    {
    //        grade = "B";
    //    }
    //    else if ((grade1.ToUpper() == "Nad".ToUpper() || grade1.ToUpper() == "Na".ToUpper() || grade1.ToUpper() == "ML".ToUpper()) && grade2 == "B" && grade3 == "C")
    //    {
    //        grade = "B";
    //    }
    //    else if (grade1 == "A" && grade2 == "B" && grade3 == "A")
    //    {
    //        grade = "A";
    //    }
    //    else if (grade1 == "B" && grade2 == "A" && grade3 == "A")
    //    {
    //        grade = "A";
    //    }
    //    else if (grade1 == "B" && grade2 == "B" && grade3 == "C")
    //    {
    //        grade = "B";
    //    }
    //    else if (grade1 == "A+" && grade2 == "A" && grade3 == "A+")
    //    {
    //        grade = "A+";
    //    }
    //    else if (grade1 == "B" && grade2 == "A" && grade3 == "B")
    //    {
    //        grade = "A";
    //    }
    //    else if (grade1 == "A" && grade2 == "B" && grade3 == "A")
    //    {
    //        grade = "A";
    //    }
    //    else if (grade1 == "A" && grade2 == "C" && grade3 == "C")
    //    {
    //        grade = "A";
    //    }
    //    else if (grade1 == "B" && grade2 == "B" && grade3 == "A")
    //    {
    //        grade = "A";
    //    }
    //    else if (grade1 == "B" && grade2 == "B" && grade3 == "B")
    //    {
    //        grade = "B";
    //    }
    //    else if (grade1 == "C" && grade2 == "B" && grade3 == "B")
    //    {
    //        grade = "B";
    //    }
    //    else if (grade1 == "A" && grade2 == "B" && grade3 == "B")
    //    {
    //        grade = "A";
    //    }
    //    else if (grade1 == "A" && grade2 == "A" && grade3 == "B")
    //    {
    //        grade = "A";
    //    }
    //    else if (grade1 == "A" && grade2 == "A" && grade3 == "A+")
    //    {
    //        grade = "A+";
    //    }
    //    else if (grade1 == "A+" && grade2 == "A+" && grade3 == "A")
    //    {
    //        grade = "A+";
    //    }
    //    else if (grade1 == "A" && grade2 == "A+" && grade3 == "A+")
    //    {
    //        grade = "A+";
    //    }
    //    else if (grade1 == "C" && grade2 == "B" && grade3 == "A")
    //    {
    //        grade = "B";
    //    }
    //    else if (grade1 == "C" && grade2 == "C" && grade3 == "B")
    //    {
    //        grade = "B";
    //    }
    //    else if (grade1 == "C" && grade2 == "D" && grade3 == "C")
    //    {
    //        grade = "C";
    //    }
    //    else if (grade1 == "D" && grade2 == "D" && grade3 == "C")
    //    {
    //        grade = "C";
    //    }
    //    else if (grade1 == "D" && grade2 == "D" && grade3 == "D")
    //    {
    //        grade = "D";
    //    }
    //    else if (grade1 == "A+" && grade2 == "A+" && grade3 == "A+")
    //    {
    //        grade = "A+";
    //    }
    //    else if (grade1 == "C" && grade2 == "C" && grade3 == "C")
    //    {
    //        grade = "C";
    //    }
    //    else if (grade1 == "A" && grade2 == "A+" && grade3 == "A")
    //    {
    //        grade = "A+";
    //    }
    //    else if (grade1 == "A+" && grade2 == "A" && grade3 == "A")
    //    {
    //        grade = "A+";
    //    }
    //    else if ((grade1 == "A+" && grade2 == "A+" && grade3 == "B") || (grade1 == "B" && grade2 == "A+" && grade3 == "A+") || (grade1 == "A+" && grade2 == "B" && grade3 == "A+"))
    //    {
    //        grade = "A+";
    //    }
    //    else if ((grade1 == "A" && grade2 == "A+" && grade3 == "B") || (grade1 == "A" && grade2 == "B" && grade3 == "A+") || (grade1 == "A+" && grade2 == "A" && grade3 == "B") || (grade1 == "A+" && grade2 == "B" && grade3 == "A") || (grade1 == "B" && grade2 == "A+" && grade3 == "A") || (grade1 == "B" && grade2 == "A" && grade3 == "A+"))
    //    {
    //        grade = "A+";
    //    }
    //    else if ((grade1 == "B" && grade2 == "C" && grade3 == "B") || (grade1 == "B" && grade2 == "B" && grade3 == "C") || (grade1 == "C" && grade2 == "B" && grade3 == "B"))
    //    {
    //        grade = "B";
    //    }
    //    else if ((grade1 == "B" && grade2 == "C" && grade3 == "C") || (grade1 == "C" && grade2 == "B" && grade3 == "C") || (grade1 == "C" && grade2 == "C" && grade3 == "B"))
    //    {
    //        grade = "B";
    //    }
    //    else if ((grade1 == "B" && grade2 == "B" && grade3 == "A+") || (grade1 == "B" && grade2 == "A+" && grade3 == "B") || (grade1 == "A+" && grade2 == "B" && grade3 == "B"))
    //    {
    //        grade = "A+";
    //    }
    //    else if ((grade1 == "A" && grade2 == "B" && grade3 == "C") || (grade1 == "A" && grade2 == "C" && grade3 == "B") || (grade1 == "B" && grade2 == "A" && grade3 == "C") || (grade1 == "B" && grade2 == "C" && grade3 == "A") || (grade1 == "C" && grade2 == "A" && grade3 == "B") || (grade1 == "C" && grade2 == "B" && grade3 == "A"))
    //    {
    //        grade = "A";
    //    }
    //    else if ((grade1 == "C" && grade2 == "A+" && grade3 == "C") || (grade1 == "C" && grade2 == "C" && grade3 == "A+") || (grade1 == "A+" && grade2 == "C" && grade3 == "C"))
    //    {
    //        grade = "A+";
    //    }
    //    else if ((grade1 == "C" && grade2 == "A+" && grade3 == "B") || (grade1 == "C" && grade2 == "B" && grade3 == "A+") || (grade1 == "B" && grade2 == "C" && grade3 == "A+") || (grade1 == "B" && grade2 == "A+" && grade3 == "C") || (grade1 == "A+" && grade2 == "C" && grade3 == "B") || (grade1 == "A+" && grade2 == "B" && grade3 == "C"))
    //    {
    //        grade = "A+";
    //    }
    //    else if ((grade1 == "C" && grade2 == "A" && grade3 == "C") || (grade1 == "C" && grade2 == "C" && grade3 == "A") || (grade1 == "A" && grade2 == "C" && grade3 == "C"))
    //    {
    //        grade = "A";
    //    }
    //    else if ((grade1 == "A+" && grade2 == "C" && grade3 == "A") || (grade1 == "A+" && grade2 == "A" && grade3 == "C") || (grade1 == "A" && grade2 == "C" && grade3 == "A+") || (grade1 == "A" && grade2 == "A+" && grade3 == "C") || (grade1 == "C" && grade2 == "A+" && grade3 == "A") || (grade1 == "C" && grade2 == "A" && grade3 == "A+"))
    //    {
    //        grade = "A+";
    //    }
    //    else if (grade1 == "A" && grade2 == "C" && grade3 == "A")
    //    {
    //        grade = "A";
    //    }
    //    else if (grade1 == "A" && grade2 == "D" && grade3 == "A")
    //    {
    //        grade = "A";
    //    }
    //    else if (grade1 == "B" && grade2 == "D" && grade3 == "B")
    //    {
    //        grade = "B";
    //    }
    //    else if (grade1 == "AB" && grade2 == "D" && grade3 == "A")
    //    {
    //        grade = "A";
    //    }
    //    else if (grade1 == "AB" && grade2 == "D" && grade3 == "C")
    //    {
    //        grade = "C";
    //    }
    //    else if (grade1 == "AB" && grade2 == "D" && grade3 == "D")
    //    {
    //        grade = "D";
    //    }
    //    else if (grade1 == "AB" && grade2 == "D" && grade3 == "A")
    //    {
    //        grade = "A";
    //    }
    //    else if (grade1 == "AB" && grade2 == "D" && grade3 == "B")
    //    {
    //        grade = "B";
    //    }
    //    else if (grade1 == "A" && grade2 == "D" && grade3 == "B")
    //    {
    //        grade = "A";
    //    }
    //    else if (grade1 == "A" && grade2 == "D" && grade3 == "C")
    //    {
    //        grade = "A";
    //    }
    //    else if (grade1 == "B" && grade2 == "D" && grade3 == "C")
    //    {
    //        grade = "B";
    //    }
    //    else if (grade1 == "A+" && grade2 == "A+" && grade3 == "")
    //    {
    //        grade = "A+";
    //    }
    //    else if (grade1 == "" && grade2 == "A+" && grade3 == "A+")
    //    {
    //        grade = "A+";
    //    }
    //    else if (grade1 == "A+" && grade2 == "" && grade3 == "A+")
    //    {
    //        grade = "A+";
    //    }
    //    else if (grade1 == "A" && grade2 == "A" && grade3 == "")
    //    {
    //        grade = "A";
    //    }
    //    else if (grade1 == "" && grade2 == "A" && grade3 == "A")
    //    {
    //        grade = "A";
    //    }
    //    else if (grade1 == "A" && grade2 == "" && grade3 == "A")
    //    {
    //        grade = "A";
    //    }
    //    else if (grade1 == "NAD" && grade2 == "" && grade3 == "A")
    //    {
    //        grade = "A";
    //    }
    //    else if (grade1 == "" && grade2 == "NAD" && grade3 == "A")
    //    {
    //        grade = "A";
    //    }
    //    else if (grade1 == "A" && grade2 == "" && grade3 == "NAD")
    //    {
    //        grade = "A";
    //    }
    //    else if (grade1 == "NAD" && grade2 == "" && grade3 == "A+")
    //    {
    //        grade = "A+";
    //    }
    //    else if (grade1 == "" && grade2 == "NAD" && grade3 == "A+")
    //    {
    //        grade = "A+";
    //    }
    //    else if (grade1 == "A+" && grade2 == "" && grade3 == "NAD")
    //    {
    //        grade = "A+";
    //    }
    //    else if (grade1 == "NAD" && grade2 == "" && grade3 == "B")
    //    {
    //        grade = "B";
    //    }
    //    else if (grade1 == "" && grade2 == "NAD" && grade3 == "B")
    //    {
    //        grade = "B";
    //    }
    //    else if (grade1 == "B" && grade2 == "" && grade3 == "NAD")
    //    {
    //        grade = "B";
    //    }
    //    else if (grade1 == "NAD" && grade2 == "" && grade3 == "C")
    //    {
    //        grade = "C";
    //    }
    //    else if (grade1 == "" && grade2 == "NAD" && grade3 == "C")
    //    {
    //        grade = "C";
    //    }
    //    else if (grade1 == "C" && grade2 == "" && grade3 == "NAD")
    //    {
    //        grade = "C";
    //    }
    //    else if (grade1 == "NAD" && grade2 == "" && grade3 == "D")
    //    {
    //        grade = "D";
    //    }
    //    else if (grade1 == "" && grade2 == "NAD" && grade3 == "D")
    //    {
    //        grade = "D";
    //    }
    //    else if (grade1 == "D" && grade2 == "" && grade3 == "NAD")
    //    {
    //        grade = "D";
    //    }
    //    else if ((grade1 != "NAD" || grade1 != "ML" || grade1 == "") && grade2 == "A+" && grade3 == "A+")
    //    {
    //        grade = "A+";
    //    }
    //    else if ((grade1 != "NAD" || grade1 != "ML" || grade1 == "") && grade2 == "A" && grade3 == "A")
    //    {
    //        grade = "A";
    //    }
    //    else if ((grade1 != "NAD" || grade1 != "ML" || grade1 == "") && grade2 == "B" && grade3 == "B")
    //    {
    //        grade = "B";
    //    }
    //    else if ((grade1 != "NAD" || grade1 != "ML" || grade1 == "") && grade2 == "C" && grade3 == "C")
    //    {
    //        grade = "C";
    //    }
    //    else if ((grade1 != "NAD" || grade1 != "ML" || grade1 == "") && grade2 == "D" && grade3 == "D")
    //    {
    //        grade = "D";
    //    }
    //    else if ((grade1 != "NAD" || grade1 != "ML" || grade1 == "") && (grade2 == "A+" || grade2 == "B") && (grade3 == "B" || grade3 == "A+"))
    //    {
    //        grade = "A+";
    //    }
    //    else if ((grade1 != "NAD" || grade1 != "ML" || grade1 == "") && (grade2 == "A" || grade2 == "B") && (grade3 == "B" || grade3 == "A"))
    //    {
    //        grade = "A";
    //    }
    //    else if ((grade1 != "NAD" || grade1 != "ML" || grade1 == "") && (grade2 == "A" || grade2 == "C") && (grade3 == "C" || grade3 == "A"))
    //    {
    //        grade = "A";
    //    }
    //    else if ((grade1 != "NAD" || grade1 != "ML" || grade1 == "") && (grade2 == "C" || grade2 == "B") && (grade3 == "B" || grade3 == "C"))
    //    {
    //        grade = "B";
    //    }
    //    else if ((grade1 != "NAD" || grade1 != "ML" || grade1 == "") && (grade2 == "C" || grade2 == "D") && (grade3 == "D" || grade3 == "C"))
    //    {
    //        grade = "C";
    //    }
    //    else if ((grade1 != "NAD" || grade1 != "ML" || grade1 == "") && (grade2 == "A" || grade2 == "A+") && (grade3 == "A+" || grade3 == "A"))
    //    {
    //        grade = "A+";
    //    }
    //    else if ((grade1 != "NAD" || grade1 != "ML" || grade1 == "") && (grade2 == "D" || grade2 == "B") && (grade3 == "B" || grade3 == "D"))
    //    {
    //        grade = "B";
    //    }


    //    else if ((grade2 != "NAD" || grade2 != "ML" || grade2 == "") && grade1 == "A+" && grade3 == "A+")
    //    {
    //        grade = "A+";
    //    }
    //    else if ((grade2 != "NAD" || grade2 != "ML" || grade2 == "") && grade1 == "A" && grade3 == "A")
    //    {
    //        grade = "A";
    //    }
    //    else if ((grade2 != "NAD" || grade2 != "ML" || grade2 == "") && grade1 == "B" && grade3 == "B")
    //    {
    //        grade = "B";
    //    }
    //    else if ((grade2 != "NAD" || grade2 != "ML" || grade2 == "") && grade1 == "C" && grade3 == "C")
    //    {
    //        grade = "C";
    //    }
    //    else if ((grade2 != "NAD" || grade2 != "ML" || grade2 == "") && grade1 == "D" && grade3 == "D")
    //    {
    //        grade = "D";
    //    }
    //    else if ((grade2 != "NAD" || grade2 != "ML" || grade2 == "") && (grade1 == "A+" || grade1 == "B") && (grade3 == "B" || grade3 == "A+"))
    //    {
    //        grade = "A+";
    //    }
    //    else if ((grade2 != "NAD" || grade2 != "ML" || grade2 == "") && (grade1 == "A" || grade1 == "B") && (grade3 == "B" || grade3 == "A"))
    //    {
    //        grade = "A";
    //    }
    //    else if ((grade2 != "NAD" || grade2 != "ML" || grade2 == "") && (grade1 == "A" || grade1 == "A+") && (grade3 == "A+" || grade3 == "A"))
    //    {
    //        grade = "A+";
    //    }
    //    else if ((grade2 != "NAD" || grade2 != "ML" || grade2 == "") && (grade1 == "A" || grade1 == "C") && (grade3 == "C" || grade3 == "A"))
    //    {
    //        grade = "A";
    //    }
    //    else if ((grade2 != "NAD" || grade2 != "ML" || grade2 == "") && (grade1 == "C" || grade1 == "B") && (grade3 == "B" || grade3 == "C"))
    //    {
    //        grade = "B";
    //    }
    //    else if ((grade2 != "NAD" || grade2 != "ML" || grade2 == "") && (grade1 == "C" || grade1 == "D") && (grade3 == "D" || grade3 == "C"))
    //    {
    //        grade = "C";
    //    }
    //    else if ((grade2 != "NAD" || grade2 != "ML" || grade2 == "") && (grade1 == "B" || grade1 == "D") && (grade3 == "D" || grade3 == "B"))
    //    {
    //        grade = "B";
    //    }



    //    return grade;
    //}
}