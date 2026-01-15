using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class AllotedTimeTable : Page
{
    string sql = "", _sql = "", Sql = "";
    Campus _oo = new Campus();
    private SqlConnection _con;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Logintype"] == null) { Response.Redirect("~/default.aspx"); }
        _con = new SqlConnection();
        _con = _oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);
        BLL.BLLInstance.LoadHeader("Report", header);
        if (!IsPostBack)
        {
            LoadClass();
            loadStaff();
        }
    }
    public void LoadClass()
    {
        sql = "select * from classmaster where BranchCode=" + Session["BranchCode"] + "";
        _oo.FillDropDown_withValue(sql, ddlClass, "ClassName", "Id");
        ddlClass.Items.Insert(0, new ListItem("<--All-->", ""));
    }
    
    protected void loadStaff()
    {
        sql = "select convert(date, getdate()) curDate";
        string curDate = _oo.ReturnTag(sql, "curDate");
        sql = "select Name+ ' ('+Ecode+')' Name, Ecode from GetAllStaffRecords_UDF(" + Session["BranchCode"]+") where Ecode in  ";
        sql = sql + " (select EmpCode from TTPeriodAllotToStaff where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + ") ";
        _oo.FillDropDown_withValue(sql, ddlStaff, "Name", "Ecode");
        ddlStaff.Items.Insert(0, new ListItem("<--Select-->", ""));
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        LoadData();
    }
    protected void LoadData()
    {
        var sb = new StringBuilder();

        DataTable table = new DataTable();
        DataTable DTperiod = new DataTable();
        DataTable DTClass = new DataTable();
        DataTable DTBranchn = new DataTable();
        DataTable DTSection = new DataTable();
        sb.AppendLine("<table class='table table-striped table-hover no-head-border table-bordered'><tbody><tr>");
        sb.AppendLine("<th scope='col'>#</th>");
        sb.AppendLine("<th scope='col'>Class</th>");



        table.Columns.Add("sr", typeof(string));
        string sql1 = "select Period from TTPeriodMaster where BranchCode=" + Session["BranchCode"] + " order by id asc";
        DTperiod = _oo.Fetchdata(sql1);
        for (int k = 0; k < DTperiod.Rows.Count; k++)
        {
            table.Columns.Add(DTperiod.Rows[k]["Period"].ToString(), typeof(string));
            sb.AppendLine("<th scope='col'>" + DTperiod.Rows[k]["Period"].ToString() + "</th>");
        }
        sb.AppendLine("</tr>");
        int srno = 0;
        string sql2 = "select id, ClassName from ClassMaster where  BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' ";
        if (ddlClass.SelectedIndex != 0)
        {
            sql2 = sql2 + "  and id=" + ddlClass.SelectedValue + " ";
        }
        sql2 = sql2 + " order by id asc ";
        DTClass = _oo.Fetchdata(sql2);

        for (int i = 0; i < DTClass.Rows.Count; i++)
        {
            string sql3 = "select id, case when IsDisplay=1 then BranchName else '' end Branch from BranchMaster where  classid=" + DTClass.Rows[i]["id"].ToString() + " and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' order by id asc";
            DTBranchn = _oo.Fetchdata(sql3);
            for (int j = 0; j < DTBranchn.Rows.Count; j++)
            {
                string sql4 = "select id, SectionName from SectionMaster where  ClassNameId=" + DTClass.Rows[i]["id"].ToString() + " and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' order by id asc";
                DTSection = _oo.Fetchdata(sql4);
                for (int k = 0; k < DTSection.Rows.Count; k++)
                {
                    sb.AppendLine("<tr align='center' valign='middle'>");
                    srno = srno + 1;
                    sb.AppendLine("<td>" + srno + "</td>");
                    sb.AppendLine("<td>" + (DTClass.Rows[i]["ClassName"].ToString() + " " + DTBranchn.Rows[j]["Branch"].ToString() + " (" + DTSection.Rows[k]["SectionName"].ToString() + ")").ToString() + "</td>");
                    for (int l = 1; l < table.Columns.Count; l++)
                    {
                        sql = "select PaperCode+' | '+Name data1, StartFrom+'-To-'+EndTo data2  from TTPeriodAllotToStaff pa inner join GetAllStaffRecords_UDF(" + Session["BranchCode"] + ") asr on asr.Ecode=pa.EmpCode and asr.BranchCode=pa.BranchCode inner join TTPeriodMapping pm on pm.Periodid=pa.PeriodId and pm.Classid=pa.ClassId and pm.SectionId=pa.SectionId and pm.SessionName=pa.SessionName and pm.BranchCode=pa.BranchCode inner join TTPeriodMaster pms on pms.id=pm.PeriodId and pm.BranchCode=pa.BranchCode where pms.Period='" + table.Columns[l] + "' ";
                        sql = sql + " and pa.classid=" + DTClass.Rows[i]["id"].ToString() + " and pa.BranchId=" + DTBranchn.Rows[j]["id"].ToString() + " ";
                        sql = sql + " and pa.sectionId=" + DTSection.Rows[k]["id"].ToString() + " and pa.BranchCode=" + Session["BranchCode"] + " and pa.SessionName='" + Session["SessionName"] + "' ";
                        if (ddlStaff.SelectedIndex!=0)
                        {
                            sql = sql + " and empCode='" + ddlStaff.SelectedValue + "' ";
                        }
                        sql = sql + "  order by pa.ClassId, pa.BranchId, pa.Sectionid asc";

                        var DTdata = _oo.Fetchdata(sql);
                        if (DTdata.Rows.Count > 0)
                        {
                            sb.AppendLine("<td>" + DTdata.Rows[0]["data1"].ToString() + " (" + DTdata.Rows[0]["data2"].ToString() + ")</td>");
                        }
                        else
                        {
                            sb.AppendLine("<td></td>");
                        }

                    }
                    sb.AppendLine("</tr>");
                }
            }
        }
        sb.AppendLine("</tbody></table>");
        appentDiv.InnerHtml = sb.ToString();
        if (srno > 0)
        {
            heading.Text = "Time Table";
            string ss = "select format(getdate(), 'dd MMM yyyy hh:mm:ss tt') dates";
            lblRegister.Text = "Date : " + _oo.ReturnTag(ss, "dates");
            Panel2.Visible = true;
            abc.Visible = true;
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "lineBreak();", true);
        }
        else
        {
            Panel2.Visible = false;
            abc.Visible = false;
        }
    }

    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        _oo.ExportTolandscapeWord(Response, "TimeTable", gdv1);
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        _oo.ExportDivToExcelWithFormatting(Response, "TimeTable.xls", gdv1, Server.MapPath("~/Admin/css/style.css"));
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {
        _oo.ExporttolandscapePdf(Response, "TimeTable", abc);
    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = abc;
        ClientScript.RegisterStartupScript(GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }

    
}