using System;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _1
{
    public partial class AdminSubjectwiseAttendanceReport : Page
    {
        private SqlConnection _con;
        private readonly Campus _oo;
        private string _sql = string.Empty;

        public AdminSubjectwiseAttendanceReport()
        {
            _con = new SqlConnection();
            _oo = new Campus();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Grd.Rows.Count > 0)
            {
                Panel2.Visible = true;
            }
            else
            {
                Panel2.Visible = false;
            }
            _con = _oo.dbGet_connection();

            Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file

            if ((string)Session["LoginName"] == "" || (string)Session["BranchCode"] == "" || (string)Session["SessionName"] == "" || Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
            {
                Response.Redirect("default.aspx");
            }
            if (!IsPostBack)
            {
                _sql = "Select ClassName,Id from ClassMaster";
                _sql = _sql + " where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                _sql = _sql + "  order by CIDOrder";
                _oo.FillDropDown_withValue(_sql, DrpAtteClass, "ClassName", "Id");
                //////sql = "Select SectionName from SectionMaster";
                //////sql = sql + " where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
                //////oo.FillDropDown(sql, DrpAttenSection, "SectionName");

                //sql = "select Id from ClassMaster where  ClassName='" + DrpAtteClass.SelectedItem.ToString() + "'";
                //sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
                //CCode = oo.ReturnTag(sql, "id");
                //sql = "Select SectionName from SectionMaster where ClassNameId=" + CCode;
                //sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

                // oo.FillDropDown(sql, DrpAttenSection, "SectionName");
                _oo.AddDateMonthYearDropDown(DrpSaal, DrpMahina, DrpDin);
                _oo.FindCurrentDateandSetinDropDown(DrpSaal, DrpMahina, DrpDin);
                DrpAttenSection.Items.Clear();
                DropDownList2.Items.Clear();
                DrpAttenSection.Items.Insert(0, "<--Select-->");
                DropDownList2.Items.Insert(0, "<--Select-->");
            }
            else { 
        
            }
        }
        protected void DrpSaal_SelectedIndexChanged(object sender, EventArgs e)
        {
            _oo.YearDropDown(DrpSaal, DrpMahina, DrpDin);
        }
        protected void DrpMahina_SelectedIndexChanged(object sender, EventArgs e)
        {
            _oo.MonthDropDown(DrpSaal, DrpMahina, DrpDin);
        }
        protected void DrpAtteClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            //sql = "select Id from ClassMaster where  ClassName='" + DrpAtteClass.SelectedItem.ToString() + "'";
            //sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

            //CCode = oo.ReturnTag(sql, "id");
            _sql = "Select SectionName,Id from SectionMaster where ClassNameId=" + DrpAtteClass.SelectedValue;
            _sql = _sql + " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";

            _oo.FillDropDown_withValue(_sql, DrpAttenSection, "SectionName", "Id");
            DrpAttenSection.Items.Insert(0, "<--Select-->");
        }
        protected void DrpAttenSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            //oo.MessageBox(DrpAtteClass.SelectedValue + "/" + DrpAttenSection.SelectedValue, this.Page);
            _sql = "select SubjectName,Id from Subject_names where classid='" + DrpAtteClass.SelectedValue + "'  and BranchCode=" + Session["BranchCode"] + " and sectionName='" + DrpAttenSection.SelectedValue + "' and SessionName='" + Session["SessionName"] + "'";
            _oo.FillDropDown_withValue(_sql, DropDownList2, "SubjectName", "Id");
            DropDownList2.Items.Insert(0, "<--Select-->");
        }
        protected void btnShow_Click(object sender, EventArgs e)
        {
            if (DrpAtteClass.SelectedItem.Text != "<--Select-->")
            {
                //lblmess.Text = "Insert";
                if (DrpAttenSection.SelectedItem.Text != "<--Select-->")
                {
                    if (DropDownList2.SelectedItem.Text != "<--Select-->")
                    {
                        string dateVal = DrpSaal.SelectedItem.Text + "/" + DrpMahina.SelectedItem.Text + "/" + DrpDin.SelectedItem.Text;
                        _sql = "select Row_Number() over(Order By AttendanceSubjectwise.Id) as Id,AttendanceSubjectwise.SrNo,StudentGenaralDetail.StEnRCode,AttendanceSubjectwise.AttendanceValue,StudentGenaralDetail.FirstName+' '+StudentGenaralDetail.MiddleName+' '+StudentGenaralDetail.LastName as StudentName,StudentFamilyDetails.FatherName from AttendanceSubjectwise  inner join StudentGenaralDetail on StudentGenaralDetail.SrNo=AttendanceSubjectwise.SrNo inner join StudentFamilyDetails on StudentFamilyDetails.SrNo=AttendanceSubjectwise.SrNo  where AttendanceSubjectwise.ClassName='" + DrpAtteClass.SelectedValue + "' and AttendanceSubjectwise.SectionName='" + DrpAttenSection.SelectedValue + "' and AttendanceSubjectwise.SubjectId='" + DropDownList2.SelectedValue + "' and AttendanceSubjectwise.AttendanceDate='" + dateVal + "' and AttendanceSubjectwise.SessionName='" + Session["SessionName"] + "' and StudentGenaralDetail.SessionName='" + Session["SessionName"] + "' and StudentFamilyDetails.SessionName='" + Session["SessionName"] + "'  and AttendanceSubjectwise.BranchCode=" + Session["BranchCode"] + " and StudentGenaralDetail.BranchCode=" + Session["BranchCode"] + " and StudentFamilyDetails.BranchCode=" + Session["BranchCode"] + ""; 
                        Grd.DataSource = _oo.GridFill(_sql);
                        Grd.DataBind();
                        //for (int a = 0; a < Grd.Rows.Count; a++)//State Wise
                        //{
                        //    DropDownList Drp = (DropDownList)Grd.Rows[a].Cells[3].FindControl("DropDownList1");
                        //    sql = "select AbbreviationName from  AttendanceAbbreviationMaster";
                        //    //  sql = sql + " where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
                        //    oo.FillDropDownWithOutSelect(sql, Drp, "AbbreviationName");

                        //}
                    }
                    else
                    {
                        //oo.MessageBox("Please Select Subject First!", this.Page);
                        Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Please Select Subject First!", "A");

                    }
                }
                else
                {
                    //oo.MessageBox("Please Select Section First!", this.Page);
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Please Select Section First!", "A");

                }
            }
            else
            {
                //oo.MessageBox("Please Select Class First!", this.Page);
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Please Select Class First!", "A");

            }
        }
        protected void ImageButton1_Click(object sender, EventArgs e)
        {
            //GridView GridView1 = (GridView)Grd.Rows[0].FindControl("GridView1");
            //if (GridView1.Rows.Count > 0)
            //{
            _oo.ExportToWord(Response, "Subject wise Attendance.doc", divExport);
            // }
        }
        protected void ImageButton2_Click(object sender, EventArgs e)
        {
            _oo.ExportToExcel("Subject wise Attendance.xls", Grd);
        }
        protected void ImageButton4_Click(object sender, EventArgs e)
        {
            PrintHelper_New.ctrl = abc;
            if (Grd.Rows.Count > 0)
            {
                Grd.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            ClientScript.RegisterStartupScript(GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
        }
        protected void ImageButton3_Click(object sender, EventArgs e)
        {

        }

        public override void Dispose()
        {
            _con.Dispose();
            _oo.Dispose();
        }
    }
}