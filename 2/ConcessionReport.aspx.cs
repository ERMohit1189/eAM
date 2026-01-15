using System;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _2
{
    public partial class AdminSetConcession : Page
    {
        //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Menu"].ToString());
        private SqlConnection con;

        private readonly Campus oo;
        string sql = "";
#pragma warning disable 169
        double totsum;
#pragma warning restore 169
#pragma warning disable 169
        double price ;
#pragma warning restore 169
        private string SrNo = string.Empty;
        private string Enreg = string.Empty;
#pragma warning disable 169
        private int cp ;
#pragma warning restore 169
        private string s = string.Empty;

        public AdminSetConcession()
        {
            con = new SqlConnection();
            oo = new Campus();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            con = oo.dbGet_connection();
            Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file

            if ((string)Session["LoginName"] == "" || (string)Session["BranchCode"] == "" || (string)Session["SessionName"] == "" || Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
            {
                Response.Redirect("default.aspx");
            }
            if (!IsPostBack)
            {
                sql = "Select ClassName as Class from ClassMaster where SessionName='" + Session["SessionName"] + "' Order by CIDOrder";
                oo.FillDropDownWithOutSelect(sql, drpclass, "Class");
                drpclass.Items.Insert(0, "Select ALL");
            
                Image1.ImageUrl = "DisplayImage.ashx?UserLoginID=" + 1;
                sql = "Select CollegeName from CollegeMaster where CollegeId=" + 1;
                lblCollegeName.Text = oo.ReturnTag(sql, "CollegeName");
                Student_display();
            }
        }
        

        public void Student_display()
        {
            sql = "select SG.Id, SC.SectionName,SO.Card,SO.Medium as Medium,CM.ClassName,convert(nvarchar,So.DateOfAdmiission,106) as DateOfAdmiission,";
            sql = sql + " Sf.FatherName,SG.FirstName,SG.MiddleName,SG.LastName,";
            sql = sql + " case  when so.TransportRequired='Yes' then 'Yes' else 'No' end as TransportRequired,So.Srno,So.stenrcode as StEnCode,cp.Insttalment,cp.Concession,tnp.TableName";
            sql = sql + " from StudentGenaralDetail SG";
            sql = sql + " inner join StudentFamilyDetails SF on SG.srno=SF.srno";
            sql = sql + " inner join StudentOfficialDetails SO on SG.srno=SO.srno";
            sql = sql + " inner join ClassMaster CM on SO.AdmissionForClassId=CM.Id";
            sql = sql + " inner join SectionMaster SC on SO.SectionId=SC.Id";
            sql = sql + " inner join Concession_Permission cp on cp.SrNo=SO.SrNo";
            sql = sql + " inner join TableNameForpermission tnp on tnp.Id=cp.TableId";
            sql = sql + " where sg.SessionName='" + Session["SessionName"] + "' and";
            if (drpclass.SelectedValue != "Select ALL")
            {
                sql = sql + " cm.ClassName='" + drpclass.SelectedValue + "' and";
                Label2.Text = "of class " + drpclass.SelectedValue;
            }
            else
            {
                Label2.Text = "of all classes";
            }
            sql = sql + " so.SessionName='" + Session["SessionName"] + "' and sf.SessionName='" + Session["SessionName"] + "' and cm.SessionName='" + Session["SessionName"] + "'";
            sql = sql + " and SC.SessionName='" + Session["SessionName"] + "'  and";
            sql = sql + " sg.BranchCode=" + Session["BranchCode"] + " and cp.SessionName='" + Session["SessionName"] + "' and concession<>0 and reset is null";
            sql = sql + " and SO.Withdrwal is null order by Cidorder Asc";
            Grd0.DataSource = oo.GridFill(sql);
            Grd0.DataBind();

            sql = "select SUM(Convert(int,cp.Concession)) as Concession from StudentOfficialDetails SO ";
            sql = sql + " inner join ClassMaster CM on SO.AdmissionForClassId=CM.Id ";
            sql = sql + " inner join Concession_Permission cp on cp.SrNo=SO.SrNo ";
            sql = sql + " inner join TableNameForpermission tnp on tnp.Id=cp.TableId ";
            sql = sql + " where so.SessionName='" + Session["SessionName"] + "'  and cm.SessionName='" + Session["SessionName"] + "'";
            if (drpclass.SelectedValue != "Select ALL")
            {
                sql = sql + " and cm.ClassName='" + drpclass.SelectedValue + "'";
                Label2.Text = "of class " + drpclass.SelectedValue;
            }
            else
            {
                Label2.Text = "of all classes";
            }
            sql = sql + " and so.BranchCode=" + Session["BranchCode"] + " and ";
            sql = sql + " cp.SessionName='" + Session["SessionName"] + "' and concession<>0 ";
            sql = sql + " and reset is null and SO.Withdrwal is null";

            if (Grd0.Rows.Count > 0)
            {
                Label lbltotal = (Label)Grd0.FooterRow.FindControl("Label1");
                lbltotal.Text = "Total : " + oo.ReturnTag(sql, "Concession");
                abc.Visible = true;
            }
            else
            {
                abc.Visible = false;
                //oo.MessageBox("Sorry, No Record(s) found!",this.Page);
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Sorry, No Record(s) found!", "A");       

            }
       
        }

        protected void drpclass1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Student_display();
        }
        protected void ImageButton1_Click(object sender, EventArgs e)
        {
            if (Grd0.Rows.Count > 0)
            {
                oo.ExportToWord(Response, "StudentConcessionReport", divExport);
            }
        }
        protected void ImageButton2_Click(object sender, EventArgs e)
        {
            if (Grd0.Rows.Count > 0)
            {
                oo.ExportToExcel("StudentConcessionReport.xls", Grd0);
            }
        }
        protected void ImageButton4_Click(object sender, EventArgs e)
        {
            PrintHelper_New.ctrl = abc;
            ClientScript.RegisterStartupScript(GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
        }
        protected void ImageButton3_Click(object sender, EventArgs e)
        {

        }

        public override void Dispose()
        {
            con.Dispose();
            oo.Dispose();
        }
    }
}