using System;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _2
{
    public partial class AdminDeafulterListClassWise : Page
    {
        private SqlConnection con;
        private readonly Campus oo;
        private string sql;
        private int a;

        public AdminDeafulterListClassWise()
        {
            con = new SqlConnection();
            oo = new Campus();
            sql = string.Empty;
            a = 0;
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file

            con = oo.dbGet_connection();
            BLL.BLLInstance.LoadHeader("Report", header);
            if (!IsPostBack)
            {

                Panel1.Visible = false;
           
                Label4.Visible = false;
                abc.Visible = false;
            }
        }
        protected void LnkShow_Click(object sender, EventArgs e)
        {
            DisplayGrid();

        }



        public void DisplayGrid()
        {

            int i = 0;
            sql = "select ClassName from ClassMaster where SessionName='" + Session["SessionName"] + "'  and ClassName!='<--Select-->'";
            sql = sql + " order by CIDOrder";
            GrdClass.DataSource = oo.GridFill(sql);
            GrdClass.DataBind();

            for (a = 0; a <= GrdClass.Rows.Count - 1; a++)
            {
                Label lblClass = (Label)GrdClass.Rows[a].FindControl("lblClass");
                GridView GrdDiscountDetails = (GridView)GrdClass.Rows[a].FindControl("GrdDiscountDetails");

                sql = "     select ROW_NUMBER() OVER (ORDER BY Am.ArrierId ASC) AS SNo, Am.ArrierId,AM.ArrearAmt,am.ArrierSession,am.SrNo,am.StEnRCode,am.Remark, ";
                sql = sql + "  sg.FirstName+' '+sg.MiddleName+' '+sg.LastName as Name ,cm.ClassName as Class,sm.SectionName as Section,convert(nvarchar,ArrierDate,106)as ArrierDate  from ArrierMast Am  ";
                sql = sql + "  left join StudentGenaralDetail sg on am.SrNo=sg.SrNo  ";
                sql = sql + "  left join StudentOfficialDetails sd on sd.SrNo=sg.SrNo  ";
                sql = sql + " left join ClassMaster cm on sd.AdmissionForClassId=cm.Id  ";
                sql = sql + "  left join SectionMaster sm on sd.SectionId=sm.Id  ";
                sql = sql + " where sg.SessionName='" + Session["SessionName"] + "' and sg.BranchCode=" + Session["BranchCode"] + "";
                sql = sql + "  and sd.SessionName='" + Session["SessionName"] + "'  and cm.SessionName='" + Session["SessionName"] + "'  and sm.SessionName='" + Session["SessionName"] + "'";
                sql = sql + " and Am.SessionName='" + Session["SessionName"] + "' and cm.ClassName='" + lblClass.Text + "' and AM.ArrearAmt>0";
                GrdDiscountDetails.DataSource = oo.GridFill(sql);
                GrdDiscountDetails.DataBind();
                if (GrdDiscountDetails.Rows.Count > 0)
                {
                    double sum = 0;
                    for (i = 0; i <= GrdDiscountDetails.Rows.Count - 1; i++)
                    {

                        Label Label2 = (Label)GrdDiscountDetails.Rows[i].FindControl("Label2");
                        sum = sum + Convert.ToDouble(Label2.Text);
                        int i1 = 0;
                        if (Label2.Text != "")
                        {
                            if (i1 == Convert.ToDouble(Label2.Text))
                            {
                                GrdDiscountDetails.Rows[i].Visible = false;
                            }
                        }
                    }
                    try
                    {
                        Label lblArrierTotalAmount = (Label)GrdDiscountDetails.FooterRow.FindControl("lblArrierTotalAmount");
                        lblArrierTotalAmount.Text = sum.ToString(CultureInfo.InvariantCulture);
                        if (lblArrierTotalAmount.Text == "0")
                        {
                            GrdClass.Rows[a].Visible = false;
                        }
                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                }
                else
                {
                    GrdClass.Rows[a].Visible = false;
                }

            }


    
            if (GrdClass.Rows.Count > 0)
            {
                Panel1.Visible = true;
         
                lblDate.Text = " | " + DateTime.Now.ToString("dd MMM yyyy");
                Label4.Visible = true;
                abc.Visible = true;
           
            }
            else
            {
                Panel1.Visible = false;
                abc.Visible = false;
            }
        }
        protected void ImageButton1_Click(object sender, EventArgs e)
        {
            if (GrdClass.Rows.Count > 0)
            {
                oo.ExportToWord(Response, "DefaulterList", divExport);
            }
        }
        protected void ImageButton2_Click(object sender, EventArgs e)
        {
            if (GrdClass.Rows.Count > 0)
            {
                oo.ExportToExcel("DefaulterList.xls", GrdClass);
            }
        }
        protected void ImageButton4_Click(object sender, EventArgs e)
        {
            PrintHelper_New.ctrl = abc;
            ClientScript.RegisterStartupScript(GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
        }
        protected void ImageButton3_Click(object sender, EventArgs e)
        {

        }
        protected void GrdClass_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public override void Dispose()
        {
            con.Dispose();
            oo.Dispose();
        }
    }
}