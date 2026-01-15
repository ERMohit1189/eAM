using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Remark_Studen_Report : Page
{
    SqlConnection _con = new SqlConnection();
    readonly Campus _oo = new Campus();

    string _sql = string.Empty;
    
    protected void Page_PreInIt(object sender, EventArgs e)
    {
        if (Session["Logintype"] == null) { Response.Redirect("~/default.aspx"); }

        if (Session["Logintype"].ToString() == "Admin")
        {
            MasterPageFile = "~/Master/admin_root-manager.master";
        }
        else if (Session["Logintype"].ToString() == "Staff")
        {
            MasterPageFile = "~/Staff/staff_root-manager.master";
        }
        else if (Session["Logintype"].ToString() == "Guardian")
        {
            MasterPageFile = "~/sp/sp_root-manager.master";
        }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file
        BLL.BLLInstance.LoadHeader("Report", header);
        _con = _oo.dbGet_connection();
        Campus camp1 = new Campus(); camp1.LoadLoader(loader);
        if (!IsPostBack)
        {
            if (Session["logintype"].ToString() == "Admin")
            {
                divEnter2.Visible = true;
                divEnter1.Visible = false;
                btnshow.Visible = true;
            }
            else
            {
                divEnter2.Visible = false;
                divEnter1.Visible = true;
                btnshow.Visible = false;
                LoadClassSrno();
            }
        }
        //if (grdDownloadedPunch.Rows.Count > 0)
        //{
        //    grdDownloadedPunch.HeaderRow.TableSection = TableRowSection.TableHeader;
        //}
    }
    protected void grdDownloadedPunch_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
            e.Row.TableSection = TableRowSection.TableHeader;
    }

    private void LoadClassSrno()
    {
        List<SqlParameter> param = new List<SqlParameter>();
        param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
        param.Add(new SqlParameter("@EmpCode", Session["LoginName"].ToString()));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));

        drpSrno.DataSource = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("GetClassTeacherClassNameStudent_Proc", param);
        drpSrno.DataTextField = "name";
        drpSrno.DataValueField = "srno";
        drpSrno.DataBind();
        BAL.objBal.fillSelectvalue(drpSrno, "<--Select-->", "<--Select-->");
        drpSrno.SelectedIndex = 0;
    }

    protected void drpSrno_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        GetStudentRemarkReport();
    }
    protected void txtSearch_OnTextChanged(object sender, EventArgs e)
    {
        GetStudentRemarkReport();
    }

    protected void lnkView_OnClick(object sender, EventArgs e)
    {
        GetStudentRemarkReport();
    }
    private void GetStudentRemarkReport()
    {
        try
        {
            var studentId = Request.Form[hfStudentId.UniqueID];
            if (studentId != null)
            {
                if (string.IsNullOrEmpty(studentId))
                {
                    studentId = txtSearch.Text.Trim();
                }
            }
            else
            {
                studentId = drpSrno.SelectedValue;
            }
            _sql = "Select * from StudentOfficialDetails where blocked='Yes' and srno='" + studentId + "' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            // ReSharper disable once UnusedVariable
            var sql1 = "Select Promotion,MODForFeeDeposit from StudentOfficialDetails where srno='" + studentId + "' and BranchCode=" + Session["BranchCode"].ToString() + " and SessionName='" + Session["SessionName"] + "'";
            //isAnualDeposit = BAL.objBal.ReturnTag(sql1, "MODForFeeDeposit") == "A" ? true : false;

            _sql = "Select * from AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "', " + Session["BranchCode"].ToString() + " ) where srno='" + studentId + "'";
            var ds = _oo.GridFill(_sql);

            grdStRecord.DataSource = ds;
            grdStRecord.DataBind();
            div1.Visible = true;
            // ReSharper disable once UseNullPropagation
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    img.ImageUrl = ds.Tables[0].Rows[0]["PhotoPath"].ToString() != string.Empty ? ds.Tables[0].Rows[0]["PhotoPath"] + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture) : "../img/user-pic/user-pic.jpg" + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture);
                    studentImg.NavigateUrl = ds.Tables[0].Rows[0]["PhotoPath"].ToString() != string.Empty ? ds.Tables[0].Rows[0]["PhotoPath"] + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture) : "../img/user-pic/user-pic.jpg" + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture);
                }
            }
            var param = new List<SqlParameter>
                {
                    (new SqlParameter("@QueryFor", "R")),
                    (new SqlParameter("@SrNo", studentId)),
                    (new SqlParameter("@SessionName", Session["SessionName"].ToString())),
                    (new SqlParameter("@BranchCode", Session["BranchCode"].ToString()))
                };
            grdDownloadedPunch.DataSource = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("USP_StudentRemarkDetails", param);
            grdDownloadedPunch.DataBind();
            LoadAttWd(w1, studentId);
            if (grdDownloadedPunch.Rows.Count > 0)
            {
                sql1 = "select 'Review Report of '+ Name+' ('+SrNo+') | Class : '+CombineClassName as name from AllStudentRecord_UDF('" + Session["SessionName"] + "', " + Session["BranchCode"] +") where SrNo='"+ studentId+"'";
                lblTitle.Text = _oo.ReturnTag(sql1, "name");
                divreport.Visible = true;
            }
            else
            {
                divreport.Visible = false;
            }
        }
        catch
        {
            // ignored
        }
    }

    public string LoadAttWd(Control parentId, String srniid)
    {
        var msg = "";
        try
        {
            Session["id"] = srniid;
            // ReSharper disable once StringLiteralTypo
            const string path = "~/admin/usercontrol/widgets/StudentRemark.ascx";
            var uc = LoadControl(path);
            parentId.Controls.Add(uc);
            if (grdDownloadedPunch.Rows.Count > 0)
            {
                divreport.Visible = true;
            }
            else
            {
                Campus camp = new Campus(); camp.msgbox(Page, divmsgrecord, "Sorry, No Review found!", "A");
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        Session["id"] = string.Empty;
        return msg;
    }
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        _oo.ExportTolandscapeWord(Response, "RemarkReport", divExport);
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        _oo.ExportDivToExcelWithFormatting(Response, "RemarkReport.xls", divExport, Server.MapPath("~/Admin/css/style.css"));
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {
        _oo.ExporttolandscapePdf(Response, "RemarkReport", divExport);
    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = divExport;
        ClientScript.RegisterStartupScript(GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
}