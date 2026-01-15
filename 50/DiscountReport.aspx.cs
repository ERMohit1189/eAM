using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _2
{
    public partial class AdminDiscountReport : Page
    {
        private SqlConnection con;
        private readonly Campus oo;
        private string sql;

        public AdminDiscountReport()
        {
            con = new SqlConnection();
            oo = new Campus();
            sql = string.Empty;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
            con = oo.dbGet_connection();
            Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file
            
            if (!IsPostBack)
            {
                sql = "Select BranchId, BranchName from Branchtab";
                var dt = oo.Fetchdata(sql);
                ddlBranch.DataSource = dt;
                ddlBranch.DataTextField = "BranchName";
                ddlBranch.DataValueField = "BranchId";
                ddlBranch.DataBind();
                Session();
                //BLL.BLLInstance.LoadHeader2("Report", header1, ddlBranch.SelectedValue);
                
                GrdDiscountDetails.Visible = false;
                filterDisplay();
            }
        }
        protected void GrdDiscountDetails_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void Session()
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@Queryfor", "S"));
            param.Add(new SqlParameter("@BranchCode", ddlBranch.SelectedValue));
            DataSet ds = new DataSet();
            ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("Get_GenralInfo", param);
            DrpSessionName.DataSource = ds.Tables[1];
            DrpSessionName.DataTextField = "SessionName";
            DrpSessionName.DataValueField = "SessionName";
            DrpSessionName.DataBind();
            drpclass.Items.Clear();
            sql = "Select ClassName as Class from ClassMaster where SessionName='" + DrpSessionName.SelectedValue + "' and BranchCode="+ ddlBranch.SelectedValue + " Order by CIDOrder";
            oo.FillDropDownWithOutSelect(sql, drpclass, "Class");
            drpclass.Items.Insert(0, "Select ALL");
        }

        protected void drpclass1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Label4.Text = "Discount Report of " + ddlBranch.SelectedItem.Text;
            //BLL.BLLInstance.LoadHeader2("Report", header1, ddlBranch.SelectedValue);
            filterDisplay();
        }

        public void filterDisplay()
        {
            try
            {
                if (drpclass.SelectedIndex == 0)
                {
                    sql = "select ROW_NUMBER() OVER (ORDER BY dm.DiscountId ASC) AS SNo,case  when sd.Withdrwal is null then 'No' Else 'Yes' End Withdrawl, dm.DiscountId,mdm.DiscHeadName as DiscountName,dm.DiscountValue,dm.DiscountType,dm.SessionName,dm.BranchCode,dm.SrNo,dm.StEnRCode,dm.Remark, ";
                    sql = sql + "  sg.FirstName+' '+sg.MiddleName+' '+sg.LastName as Name ,cm.ClassName as Class,sm.SectionName as Section, dm.LoginName, format(dm.RecordDate, 'dd-MMM-yyy HH:MM tt') as RecordDate  from DiscountMaster dm  ";
                    sql = sql + "  left join StudentGenaralDetail sg on dm.SrNo=sg.SrNo  ";
                    sql = sql + "  left join StudentOfficialDetails sd on sd.SrNo=sg.SrNo  ";
                    sql = sql + " left join ClassMaster cm on sd.AdmissionForClassId=cm.Id  ";
                    sql = sql + "  left join SectionMaster sm on sd.SectionId=sm.Id  ";
                    sql = sql + "  inner join ManualDiscountHeads mdm on mdm.id=dm.DiscountName  ";
                    sql = sql + "  where sg.SessionName='" + DrpSessionName.SelectedValue + "' and sg.BranchCode=" + ddlBranch.SelectedValue + "";
                    sql = sql + "   and cm.SessionName='" + DrpSessionName.SelectedValue + "'    and sm.SessionName='" + DrpSessionName.SelectedValue + "'";
                    sql = sql + "  and sd.SessionName='" + DrpSessionName.SelectedValue + "' and mdm.SessionName='" + DrpSessionName.SelectedValue + "'";
                    sql = sql + "  and dm.SessionName='" + DrpSessionName.SelectedValue + "' order by dm.RecordDate desc";
                    sql = sql + "  and sg.BranchCode='" + DrpSessionName.SelectedValue + "' and sg.BranchCode=" + ddlBranch.SelectedValue + "";
                    sql = sql + "   and cm.BranchCode='" + DrpSessionName.SelectedValue + "'    and sm.BranchCode='" + ddlBranch.SelectedValue + "'";
                    sql = sql + "  and sd.BranchCode='" + DrpSessionName.SelectedValue + "' and mdm.BranchCode='" + ddlBranch.SelectedValue + "'";
                    sql = sql + "  and dm.BranchCode='" + DrpSessionName.SelectedValue + "' order by dm.RecordDate desc";
                }
                else
                {
                    sql = "select ROW_NUMBER() OVER (ORDER BY dm.DiscountId ASC) AS SNo,case  when sd.Withdrwal is null then 'No' Else 'Yes' End Withdrawl, dm.DiscountId,mdm.DiscHeadName as DiscountName,dm.DiscountValue,dm.DiscountType,dm.SessionName,dm.BranchCode,dm.SrNo,dm.StEnRCode,dm.Remark, ";
                    sql = sql + "  sg.FirstName+' '+sg.MiddleName+' '+sg.LastName as Name ,cm.ClassName as Class,sm.SectionName as Section, dm.LoginName, format(dm.RecordDate, 'dd-MMM-yyy HH:MM tt') as RecordDate  from DiscountMaster dm  ";
                    sql = sql + "  left join StudentGenaralDetail sg on dm.SrNo=sg.SrNo  ";
                    sql = sql + "  left join StudentOfficialDetails sd on sd.SrNo=sg.SrNo  ";
                    sql = sql + " left join ClassMaster cm on sd.AdmissionForClassId=cm.Id  ";
                    sql = sql + "  left join SectionMaster sm on sd.SectionId=sm.Id  ";
                    sql = sql + "  inner join ManualDiscountHeads mdm on mdm.id=dm.DiscountName  ";
                    sql = sql + "  where cm.ClassName='" + drpclass.SelectedValue + "' and sg.SessionName='" + DrpSessionName.SelectedValue + "' and sg.BranchCode=" + ddlBranch.SelectedValue + "";
                    sql = sql + "   and cm.SessionName='" + DrpSessionName.SelectedValue + "'    and sm.SessionName='" + DrpSessionName.SelectedValue + "'";
                    sql = sql + "  and sd.SessionName='" + DrpSessionName.SelectedValue + "' and mdm.SessionName='" + DrpSessionName.SelectedValue + "'";
                    sql = sql + "  and dm.SessionName='" + DrpSessionName.SelectedValue + "' order by dm.RecordDate desc";
                    sql = sql + "  and cm.BranchCode='" + ddlBranch.SelectedValue + "' and sg.BranchCode='" + ddlBranch.SelectedValue + "' and sg.BranchCode=" + ddlBranch.SelectedValue + "";
                    sql = sql + "   and cm.BranchCode='" + ddlBranch.SelectedValue + "'    and sm.BranchCode='" + ddlBranch.SelectedValue + "'";
                    sql = sql + "  and sd.BranchCode='" + ddlBranch.SelectedValue + "' and mdm.BranchCode='" + ddlBranch.SelectedValue + "'";
                    sql = sql + "  and dm.BranchCode='" + ddlBranch.SelectedValue + "' order by dm.RecordDate desc";
                }
                GrdDiscountDetails.DataSource = oo.GridFill(sql);
                GrdDiscountDetails.DataBind();
                if (GrdDiscountDetails.Rows.Count > 0)
                {
                    abc.Visible = true;
                    GrdDiscountDetails.Visible = true;
                }
                else
                {
                    GrdDiscountDetails.Visible = false;
                    abc.Visible = false;
                }
                countamount();

            }
            catch (Exception)
            {
                // ignored
            }
        }

        public void countamount()
        {
            if (GrdDiscountDetails.Rows.Count > 0)
            {
                double total = 0;
                for (int i = 0; i < GrdDiscountDetails.Rows.Count; i++)
                {
                
                    Label Label36 = (Label)GrdDiscountDetails.Rows[i].FindControl("Label36");
                    string[] discount = Label36.Text.Split(' ');
                    double discountamount = 0;
                    for (int j = 0; j < discount.Length; j++)
                    {
                        double gndtotal = 0;
                        if (discount[j] != "")
                        {
                            discountamount = discountamount + Convert.ToDouble(discount[j]);
                        }
                        gndtotal = gndtotal + discountamount;
                        Label lblgrandiscount = (Label)GrdDiscountDetails.Rows[i].FindControl("lblgrandiscount");
                        lblgrandiscount.Text = gndtotal.ToString("N", new CultureInfo("en-In"));
                    }
                    total = total + discountamount;
                }
                Label lblTotalDiscount = (Label)GrdDiscountDetails.FooterRow.FindControl("lblTotalDiscount");
                lblTotalDiscount.Text = "Grand Total " + total.ToString("N", new CultureInfo("en-In"));
            }
        }

        protected void ImageButton1_Click(object sender, EventArgs e)
        {
            if (GrdDiscountDetails.Rows.Count > 0)
            {
                oo.ExportToWord(Response, "StudentDiscountReport.doc", divExport);
            }
        }
        protected void ImageButton2_Click(object sender, EventArgs e)
        {
            if (GrdDiscountDetails.Rows.Count > 0)
            {
                oo.ExportToExcel("StudentDiscountReport.xls", GrdDiscountDetails);
            }
        }
        protected void ImageButton3_Click(object sender, EventArgs e)
        {

        }
        protected void ImageButton4_Click(object sender, EventArgs e)
        {
            PrintHelper_New.ctrl = abc;
            ClientScript.RegisterStartupScript(GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        public override void Dispose()
        {
            con.Dispose();
            oo.Dispose();
        }

        protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            drpclass.Items.Clear();
            Session();
        }

        protected void DrpSessionName_SelectedIndexChanged(object sender, EventArgs e)
        {
            drpclass.Items.Clear();
            sql = "Select ClassName as Class from ClassMaster where SessionName='" + DrpSessionName.SelectedValue + "' and BranchCode=" + ddlBranch.SelectedValue + " Order by CIDOrder";
            oo.FillDropDownWithOutSelect(sql, drpclass, "Class");
            drpclass.Items.Insert(0, "Select ALL");
        }
    }
}