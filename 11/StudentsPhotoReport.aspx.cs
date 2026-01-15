using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class StudentsPhotoReport : Page
{
    string sql = "";
    Campus oo = new Campus();
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
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Campus camp = new Campus(); camp.LoadLoader(loader);
        BLL.BLLInstance.LoadHeader("Report", header);
        if (!IsPostBack)
        {
            sql = "Select id, ClassName from ClassMaster";
            sql +=  "  where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            sql +=  "  order by Id";
            oo.FillDropDown_withValue(sql, DrpClass, "ClassName", "id");
            DrpClass.Items.Insert(0, new ListItem("<--Select-->", ""));
            drpSection.Items.Insert(0, new ListItem("<--Select-->", ""));
            drpsrno.Items.Insert(0, new ListItem("<--Select-->", ""));
        }

    }
    protected void DrpClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        drpSection.Items.Clear();
        string sql = "select id, SectionName from SectionMaster ";
        sql +=  "  where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and ClassNameId=" + DrpClass.SelectedValue + "";
        sql +=  "  order by Id";
        oo.FillDropDown_withValue(sql, drpSection, "SectionName", "id");
        drpSection.Items.Insert(0, new ListItem("<--Select-->", ""));
        loadsrno();

    }
    protected void drpSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadsrno();
    }
    protected void loadsrno()
    {
        drpsrno.Items.Clear();
       string _sql = "Select Name+' - '+SrNo NAME,SrNo from AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "', " + Session["BranchCode"] + ") where ClassId=" + DrpClass.SelectedValue + " ";
        if (drpSection.SelectedIndex!=0)
        {
            _sql += "and SectionID="+ drpSection.SelectedValue + " ";
        }
        _sql += "and isnull(Withdrwal,'') = (case when '" + drpStatus.SelectedValue + "' = 'B' or '" + drpStatus.SelectedValue + "' = '' then isnull(Withdrwal,'') else case when '" + drpStatus.SelectedValue + "' = 'A' then '' else 'W' end end) ";
        _sql += "and isnull(blocked,'') = (case when '" + drpStatus.SelectedValue + "'='W' or '" + drpStatus.SelectedValue + "'='' then isnull(blocked,'') else case when '" + drpStatus.SelectedValue + "'='A' then '' else 'yes' end end)";
        oo.FillDropDown_withValue(_sql, drpsrno, "NAME", "SrNo");
        drpsrno.Items.Insert(0, new ListItem("<--Select-->", ""));
    }
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        oo.ExportTolandscapeWord(Response, "PhotoReport", gdv1);
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        oo.ExportDivToExcelWithFormatting(Response, "PhotoReport.xls", gdv1, Server.MapPath("~/Admin/css/style.css"));
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {
        oo.ExporttolandscapePdf(Response, "PhotoReport", gdv1);
    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = gdv1;
        ClientScript.RegisterStartupScript(GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }

    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        gdv1.Visible = false;
        icons.Visible = false;
        string Htmls = ""; divExport.InnerHtml = "";
        string session = Session["SessionName"].ToString();
        string SrNo = drpsrno.SelectedValue.Trim();
        string Section = drpSection.SelectedItem.Text.Trim();
        string ClassId = DrpClass.SelectedValue;
        string BranchCode = Session["BranchCode"].ToString();
        string ClassName = DrpClass.SelectedItem.Text;
        string status = drpStatus.SelectedValue;
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = BAL.objBal.dbGet_connection().ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = "USP_StudentsPhotoReport";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@sessionName", session.Trim());
                if (SrNo.Trim() != "")
                {
                    cmd.Parameters.AddWithValue("@SrNo", SrNo.Trim());
                }
                if (Section != "<--Select-->")
                {
                    cmd.Parameters.AddWithValue("@SectionName", Section.Trim());
                }
                cmd.Parameters.AddWithValue("@ClassId", ClassId);
                cmd.Parameters.AddWithValue("@branchCode", BranchCode);
                if (status != "")
                {
                    cmd.Parameters.AddWithValue("@status", status);
                }
                cmd.Parameters.AddWithValue("@action", "student");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmd.Parameters.Clear();
                if (dt.Rows.Count > 0)
                {
                    gdv1.Visible = true;
                    icons.Visible = true;
                    Htmls =Htmls+"<div  style='margin-top: 10px;' class='divHeader'></div>";
                    Htmls=Htmls+"<div class='col-sm-12' style='margin-top: 0px;'>";
                    Htmls=Htmls+"<h3 class='text-center' style='    text-transform: uppercase;'><b><span class='span'>PHOTO REPORT OF CLASS " + ClassName + " " + (Section != "<--Select-->" ? "(" + Section + ")" : "") + " FOR <span>" + session + "</span></span></b></h3>";
                    Htmls=Htmls+"</div>";
                    Htmls=Htmls+ "<table class='table table-striped table-hover no-bm no-head-border table-bordered pro-table table-header-group' style='margin-bottom: 5px; margin-top: 10px !important; width:100%;'><tbody>";
                    Htmls=Htmls+"<tr>";
                    Htmls=Htmls+"<th class='text-center'>#</th>";
                    Htmls=Htmls+"<th class='text-center'>S.R. No.</th>";
                    Htmls=Htmls+"<th class='text-center'>Student's Name</th>";
                    Htmls=Htmls+"<th class='text-center'>Father's Name</th>";
                    Htmls=Htmls+"<th class='text-center'>Student's Photo</th>";
                    Htmls=Htmls+"<th class='text-center'>Father's Photo</th>";
                    Htmls=Htmls+"<th class='text-center'>Mother's Photo</th>";
                    Htmls=Htmls+"<th class='text-center'>Group Photo</th>";
                    Htmls=Htmls+"</tr>";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        cmd.CommandText = "USP_StudentsPhotoReport";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@sessionName", session.Trim());
                        cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString().Trim());
                        cmd.Parameters.AddWithValue("@SrNo", dt.Rows[i]["admissionNo"].ToString().Trim());
                        cmd.Parameters.AddWithValue("@action", "details");
                        SqlDataAdapter das = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        das.Fill(ds);
                        cmd.Parameters.Clear();
                        try
                        {
                            Htmls=Htmls+"<tr>";
                            Htmls=Htmls+"<td class='text-center'>" + (i + 1) + "</td>";
                            Htmls=Htmls+"<td class='text-center'>" + dt.Rows[i]["admissionNo"].ToString().Trim() + "</td>";
                            Htmls=Htmls+"<td class='text-center'>" + dt.Rows[i]["StudentName"].ToString().Trim() + "</td>";
                            Htmls=Htmls+"<td class='text-center'>" + dt.Rows[i]["FatherName"].ToString().Trim() + "</td>";
                            Htmls=Htmls+"<td class='text-center'>" + (ds.Tables[0].Rows[0]["PhotoPath"].ToString() == "" ? "<img src='../uploads/pics/Student.ico' style='width:60px;' />" : "<img class='zoom' src=" + ds.Tables[0].Rows[0]["PhotoPath"].ToString() + " style='width:60px;' />") + "</td>";// "<img class='zoom' src='../uploads/StudentPhoto/blank.jpg' style='width:40px;' />" : '<img class='zoom' src="+ ds.Tables[0].Rows[0]["PhotoPath"].ToString() + " style='width:40px;'/>'+)+"</td>";
                            Htmls=Htmls+"<td class='text-center'>" + (ds.Tables[1].Rows[0]["FatherPhotoPath"].ToString() == "" ? "<img src='../uploads/pics/Student.ico' style='width:60px;' />" : "<img class='zoom' src=" + ds.Tables[1].Rows[0]["FatherPhotoPath"].ToString() + " style='width:60px;' />") + "</td>";// "<img class='zoom' src='../uploads/StudentPhoto/blank.jpg' style='width:40px;' />" : '<img class='zoom' src="+ ds.Tables[0].Rows[0]["PhotoPath"].ToString() + " style='width:40px;'/>'+)+"</td>";
                            Htmls=Htmls+"<td class='text-center'>" + (ds.Tables[2].Rows[0]["MotherPhotoPath"].ToString() == "" ? "<img src='../uploads/pics/Student.ico' style='width:60px;' />" : "<img class='zoom' src=" + ds.Tables[2].Rows[0]["MotherPhotoPath"].ToString() + " style='width:60px;' />") + "</td>";// "<img class='zoom' src='../uploads/StudentPhoto/blank.jpg' style='width:40px;' />" : '<img class='zoom' src="+ ds.Tables[0].Rows[0]["PhotoPath"].ToString() + " style='width:40px;'/>'+)+"</td>";
                            Htmls=Htmls+"<td class='text-center'>" + (ds.Tables[3].Rows[0]["GroupPhotoPath"].ToString() == "" ? "<img src='../uploads/pics/Student.ico' style='width:60px;' />" : "<img class='zoom' src=" + ds.Tables[3].Rows[0]["GroupPhotoPath"].ToString() + " style='width:60px;' />") + "</td>";// "<img class='zoom' src='../uploads/StudentPhoto/blank.jpg' style='width:40px;' />" : '<img class='zoom' src="+ ds.Tables[0].Rows[0]["PhotoPath"].ToString() + " style='width:40px;'/>'+)+"</td>";
                            Htmls=Htmls+"</tr>";

                        }
                        catch (SqlException ex)
                        { throw ex; }
                    }
                    Htmls=Htmls+"</tbody></table>";
                    divExport.InnerHtml = Htmls;
                }
            }
        }
    }

    
}