using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_GenrateAdmitCard : Page
{
    Campus oo = new Campus();
    string sql = "";
#pragma warning disable 169
    Byte[] img = null;
#pragma warning restore 169
    protected void Page_Load(object sender, EventArgs e)
    {
        Campus camp = new Campus(); camp.LoadLoader(loader);
        if (!IsPostBack)
        {
            BLL.BLLInstance.loadClass(drpclass, Session["SessionName"].ToString());
        }
    }

    public void loadStudentData()
    {
        studentInfo(rpStudentDetails);
    }

    public void studentInfo(Repeater rpt)
    {
        //GET_STUDENTREORDFORREPORTCARD
        List<SqlParameter> param = new List<SqlParameter>();
        param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
        //param.Add(new SqlParameter("@srno", drpsrno.Items[i].Value.ToString()));

        sql = "Select SrNo,Medium,HouseName,InstituteRollNo RollNo,Name as Name,StPerAddress,FamilyContactNo,ClassName,";
        sql = sql + " SectionName,FatherName,MotherName,Height,Weight,DOB,BloodGroup,VisionL,";
        sql = sql + " VisionR,DentalHygiene,SessionName,PhotoPath from AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "'," + Session["BranchCode"].ToString() + ")";
        sql = sql + " where ClassId='" + drpclass.SelectedValue.ToString() + "' and SectionId='" + drpsection.SelectedValue.ToString() + "' and Withdrwal is null ";
        if (drpsrno.SelectedIndex != 0)
        {
            sql = sql + " and srno='" + drpsrno.SelectedValue.ToString() + "'";
        }
        sql = sql + " order by Name Asc";

        rpt.DataSource = DLL.objDll.SelectRecord_usingExecuteDataset(sql, param);
        rpt.DataBind();
       
        for (int k = 0; k < rpt.Items.Count; k++)
        {
            GridView GridView1 = (GridView)rpt.Items[k].FindControl("GridView1");
            GridView GridView2 = (GridView)rpt.Items[k].FindControl("GridView2");
            loadData(GridView1);
            loadremak(GridView2);
        }
        
    }

    public void loadData(GridView GridView1)
    {
        sql = "Select ace.Id,Convert(varchar(11),ace.DateofEval,106) as Date,DATENAME(dw,DateofEval) as day,sbm.SubjectName as Subject,";
        sql = sql + " Replace(Convert(varchar(15),FromTime,100),RIGHT(CONVERT(VARCHAR(30), FromTime, 9),2),' ') + '- '+Replace(Convert(varchar(15),ToTime,100),RIGHT(CONVERT(VARCHAR(30), ToTime, 9),2),'') as Time,";
        sql = sql + " convert(nvarchar(50),Sum(DATEDIFF(MINUTE,FromTime,ToTime)))+ ' ' +'min.' as TotalTime from AdmitCardEntry ace";
        sql = sql + " inner join ClassMaster cm on cm.Id=ace.ClassId";
        sql = sql + " inner join SectionMaster sm on sm.ClassNameId=ace.ClassId";
        sql = sql + " inner join SubjectMaster sbm on sbm.ClassId=ace.ClassId and sbm.Id=ace.Subject";
        sql = sql + " where ace.Eval='" + drpEval.SelectedItem.ToString() + "' and ace.ClassId='" + drpclass.SelectedValue.ToString() + "' and ace.SectionName='" + drpsection.SelectedItem.ToString() + "'and cm.SessionName='" + Session["SessionName"].ToString() + "'";
        sql = sql + " and sbm.BranchCode=" + Session["BranchCode"].ToString() + " and sm.BranchCode=" + Session["BranchCode"].ToString() + " and cm.BranchCode=" + Session["BranchCode"].ToString() + " and sce.BranchCode=" + Session["BranchCode"].ToString() + " and sm.SessionName='" + Session["SessionName"].ToString() + "'and ace.SessionName='" + Session["SessionName"].ToString() + "' and sbm.SessionName='" + Session["SessionName"].ToString() + "'";
        sql = sql + " group by ace.Id,ace.DateofEval,FromTime,ToTime,sbm.SubjectName";
        if (oo.Duplicate(sql))
        {
            divshow.Visible = true;
            GridView1.DataSource = oo.GridFill(sql);
            GridView1.DataBind();
        }
        else
        {

            sql = "Select ace.Id,Convert(varchar(11),ace.DateofEval,106) as Date,DATENAME(dw,DateofEval) as day,sgm.SubjectGroup as Subject,";
            sql = sql + " Replace(Convert(varchar(15),FromTime,100),RIGHT(CONVERT(VARCHAR(30), FromTime, 9),2),' ') + '- '+Replace(Convert(varchar(15),ToTime,100),RIGHT(CONVERT(VARCHAR(30), ToTime, 9),2),'') as Time,";
            sql = sql + " convert(nvarchar(50),Sum(DATEDIFF(MINUTE,FromTime,ToTime)))+ ' ' +'min.' as TotalTime from AdmitCardEntry ace";
            sql = sql + " inner join ClassMaster cm on cm.Id=ace.ClassId";
            sql = sql + " inner join SectionMaster sm on sm.ClassNameId=ace.ClassId";
            sql = sql + " left join SubjectGroupMaster sgm on sgm.Id=ace.SubjectGroup";
            sql = sql + " where ace.Eval='" + drpEval.SelectedItem.ToString() + "' and ace.ClassId='" + drpclass.SelectedValue.ToString() + "' and ace.SectionName='" + drpsection.SelectedItem.ToString() + "'and cm.SessionName='" + Session["SessionName"].ToString() + "'";
            sql = sql + " and sgm.BranchCode=" + Session["BranchCode"].ToString() + " and sm.BranchCode=" + Session["BranchCode"].ToString() + " and cm.BranchCode=" + Session["BranchCode"].ToString() + " and ace.BranchCode=" + Session["BranchCode"].ToString() + " and sm.SessionName='" + Session["SessionName"].ToString() + "'and ace.SessionName='" + Session["SessionName"].ToString() + "' and sgm.SessionName='" + Session["SessionName"].ToString() + "'";
            sql = sql + " group by ace.Id,ace.DateofEval,FromTime,ToTime,sgm.SubjectGroup";
            divshow.Visible = true;
            GridView1.DataSource = oo.GridFill(sql);
            GridView1.DataBind();

        }
    }

    public void loadremak(GridView GridView2)
    {
        sql = "Select Remark from NoticeRemarkforAdmitCard where ClassId='" + drpclass.SelectedValue.ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and SectionName='" + drpsection.SelectedItem.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "'";
        GridView2.DataSource = oo.GridFill(sql);
        GridView2.DataBind();
        if (GridView2.Rows.Count > 0)
        {
            foreach (GridViewRow gvr in GridView2.Rows)
            {
                Label lblsrno = (Label)gvr.FindControl("Label11");
                lblsrno.Text = lblsrno.Text + ".";
            }
        }
    }

    //public void loaddatalist()
    //{
    //    sql = "select UPPER(SC.SectionName) SectionName,UPPER(CM.ClassName) ClassName,UPPER(Sf.FatherName) FatherName,UPPER(Sf.MotherName) MotherName,(UPPER(SG.FirstName)+' '+UPPER(SG.MiddleName)+' '+UPPER(SG.LastName)) as Name,sg.srno as srno,";
    //    sql = sql + " Convert(Varchar(11),SG.DOB,106) as DOB,SG.MobileNumber as MobileNo,SO.InstituteRollNo as RollNo,PhotoPath from StudentGenaralDetail SG";
    //    sql = sql + " left join StudentFamilyDetails SF on SG.srno=SF.srno";
    //    sql = sql + " left join StudentOfficialDetails SO on SG.srno=SO.srno";
    //    sql = sql + " left join ClassMaster CM on SO.AdmissionForClassId=CM.Id";
    //    sql = sql + " left join SectionMaster SC on SO.SectionId=SC.Id";
    //    sql = sql + " where  SG.srno='" + Session["srno"].ToString() + "'";
    //    sql = sql + " and sg.SessionName='" + Session["SessionName"].ToString() + "' and ";
    //    sql = sql + " so.SessionName='" + Session["SessionName"].ToString() + "' and sf.SessionName='" + Session["SessionName"].ToString() + "' and cm.SessionName='" + Session["SessionName"].ToString() + "'";
    //    sql = sql + " and SC.SessionName='" + Session["SessionName"].ToString() + "'  and";
    //    sql = sql + " sg.BranchCode=" + Session["BranchCode"].ToString() + "";
    //    sql = sql + " and SO.Withdrwal is null";

    //    DataList1.DataSource = oo.GridFill(sql);
    //    DataList1.DataBind();

    //    //img=oo.ReturnImageByte(sql,"Photos");
    //    string imageurl = oo.ReturnTag(sql, "PhotoPath");
    //    //Avatar.ImageUrl = oo.ReturnTag(sql, "PhotPath");
    //    if (imageurl != string.Empty)
    //    {
    //        Image2.ImageUrl = oo.ReturnTag(sql, "PhotoPath");
    //        //if (oo.ReturnTag(sql, "Photos") != "")
    //        //{
    //        //Avatar.ImageUrl = "DisplayStudentPhoto.ashx?srno=" + TxtEnter.Text;
    //        //}
    //    }
    //    else
    //    {
    //        Image2.ImageUrl = @"~\admin\images\dummy.png";
    //        //if (oo.ReturnTag(sql, "Photos") != "")
    //        //{
    //        //    Avatar.ImageUrl = "DisplayStudentPhotoStenrcode.ashx?Stenrcode=" + TxtEnter.Text;
    //        //}
    //    }

    //    //img = oo.ReturnImageByte(sql, "Photos");
       
    //    //if (oo.ReturnTag(sql, "Photos") != "")
    //    //{
    //    //    Image2.ImageUrl = "DisplayStudentPhoto.ashx?srno=" + Session["srno"].ToString();
    //    //}
           
    //}

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    protected void lnkPrint_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = divExport;
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "onclick", "var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}", true);
    }

    protected void lnkWord_Click(object sender, EventArgs e)
    {
        BAL.objBal.ExportToWord(Response, Session["srno"] + "ReportCard", divExport);
    }
    protected void lnkExcel_Click(object sender, EventArgs e)
    {
        BAL.objBal.ExportDivToExcelWithFormatting(Response, Session["srno"] + "ReportCard", divExport, Server.MapPath("~/css/theme.min.css"));
    }

    public void loadSrno()
    {
        List<SqlParameter> param = new List<SqlParameter>();
        sql = @"Select Name+' - '+SrNo NAME,SrNo from AllStudentRecord_UDF(@SessionName,@BranchCode) where 
                @Classid=Classid and @Sectionid=CASE WHEN @Sectionid='' THEN @Sectionid ELSE Sectionid END  and Withdrwal is null  ORDER BY NAME";

        param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
        param.Add(new SqlParameter("@Classid", drpclass.SelectedValue));
        param.Add(new SqlParameter("@Sectionid", drpsection.SelectedValue));

        drpsrno.DataSource = DLL.objDll.SelectRecord_usingExecuteDataset(sql, param);
        drpsrno.DataTextField = "Name";
        drpsrno.DataValueField = "SrNo";
        drpsrno.DataBind();

        drpsrno.Items.Insert(0, new ListItem("<--Select-->", ""));

    }
    protected void drpclass_SelectedIndexChanged(object sender, EventArgs e)
    {
        BLL.BLLInstance.loadSection(drpsection, Session["SessionName"].ToString(), drpclass.SelectedValue.ToString());
        loadStudentData();
    }
    protected void drpsection_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadSrno();
        loadStudentData();
    }
    protected void drpsrno_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadStudentData();
    }
    protected void rpStudentDetails_ItemCreated(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            System.Web.UI.HtmlControls.HtmlGenericControl header = (System.Web.UI.HtmlControls.HtmlGenericControl)e.Item.FindControl("header");

            BLL.BLLInstance.LoadHeader("Receipt", header);

            Label lblEvaluation = (Label)e.Item.FindControl("lblEvaluation");
            lblEvaluation.Text = drpEval.SelectedItem.ToString();

            Label lblSession = (Label)e.Item.FindControl("lblSession");
            lblSession.Text = " (" + Session["SessionName"].ToString() + ")";
        }

       

        System.Web.UI.HtmlControls.HtmlGenericControl divnote = (System.Web.UI.HtmlControls.HtmlGenericControl)e.Item.FindControl("divnote");
        System.Web.UI.HtmlControls.HtmlGenericControl divnateanddisclamir = (System.Web.UI.HtmlControls.HtmlGenericControl)e.Item.FindControl("divnateanddisclamir");

        if (chkHideNote.Checked)
        {
            divnateanddisclamir.Visible = divnote.Visible = false;
            if (e.Item.ItemType == ListItemType.AlternatingItem)
            {
                System.Web.UI.HtmlControls.HtmlGenericControl pagebreak = (System.Web.UI.HtmlControls.HtmlGenericControl)e.Item.FindControl("pagebreak");

                pagebreak.Style.Add("page-break-after", "always");
            }
        }
        else
        {
            divnateanddisclamir.Visible = divnote.Visible = true;
            System.Web.UI.HtmlControls.HtmlGenericControl pagebreak = (System.Web.UI.HtmlControls.HtmlGenericControl)e.Item.FindControl("pagebreak");

            pagebreak.Style.Add("page-break-after", "always");
        }

    }
    protected void drpEval_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadStudentData();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (chkHideTime.Checked == true)
        {
            e.Row.Cells[3].Visible = false;
        }
        else
        {
            e.Row.Cells[3].Visible = true;
        }
    }
    protected void chkHideTime_CheckedChanged(object sender, EventArgs e)
    {
        loadStudentData();
    }
    protected void chkHideNote_CheckedChanged(object sender, EventArgs e)
    {
        loadStudentData();
    }
}