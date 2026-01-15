using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _2
{
    public partial class FeeAllotedReport : Page
    {
        private SqlConnection con;
        private SqlConnection con1;
        private readonly Campus oo;
        private int srno;
        private double sum;
        private string sql;
        private int i = 0, j;
        private static bool isAnualDeposit;
        private static int col;
        public FeeAllotedReport()
        {
            con = new SqlConnection();
            con1 = new SqlConnection();
            oo = new Campus();
            srno = 0;
            sql = string.Empty;
            sum = 0;
        }
        static FeeAllotedReport()
        {
            isAnualDeposit = false;
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            con = oo.dbGet_connection();
            con1 = oo.dbGet_connection();
          
            if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
            {
                Response.Redirect("default.aspx");
            }
            BLL.BLLInstance.LoadHeader("Report", header1);
            Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file

            if (!IsPostBack)
            {
                divExport.Visible = false;
                Panel1.Visible = false;
                loadclass();
                loadcard();
                oo.fillSelectvalue(drpStream, "<--Select-->");
                oo.fillSelectvalue(drpSection, "<--Select-->");
            }

        }
        public void loadclass()
        {
            sql = "select ClassName,Id from ClassMaster where SessionName='" + Session["SessionName"] + "' order by CIDOrder";
            oo.FillDropDown_withValue(sql, drpClass, "ClassName", "Id");
            oo.fillSelectvalue(drpClass, "<--Select-->");
        }
        public void loadStream()
        {
            sql = "select BranchName,Id from BranchMaster";
            sql = sql + " where ClassId='" + drpClass.SelectedValue + "' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            oo.FillDropDown_withValue(sql, drpStream, "BranchName", "Id");
            oo.fillSelectvalue(drpStream, "<--Select-->");
         
        }
        public void loadsection()
        {
            sql = "Select SectionName from SectionMaster";
            sql = sql + " where ClassNameId='" + drpClass.SelectedValue + "' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            oo.FillDropDown(sql, drpSection, "SectionName");
            oo.fillSelectvalue(drpSection, "<--Select-->");
        }
        public void loadcard()
        {
            sql = "Select FeeGroupName,Id from FeeGroupMaster";
            sql = sql + " where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            oo.FillDropDown_withValue(sql, drpCard, "FeeGroupName","Id");
            oo.fillSelectvalue(drpCard, "<--Select-->");
        }
       
        public void loadheader()
        {
            sql = "select MonthName from MonthMaster mm";
            sql = sql + " inner join FeeGroupMaster fgm on (Case when ISNUMERIC(CardType)=1 THEN fgm.Id Else fgm.FeeGroupName End)=mm.CardType and fgm.SessionName=mm.SessionName";
            sql = sql + " inner join ClassMaster cm on cm.Id=mm.ClassId and cm.SessionName=mm.SessionName";
            sql = sql + " where fgm.FeeGroupName='" + drpCard.SelectedItem.Text.ToString() + "' and cm.ClassName='" + drpClass.SelectedItem.Text.ToString() + "' and MOD='I'  and mm.SessionName='" + Session["SessionName"].ToString() + "' ";
            sql = sql + " and mm.BranchCode=" + Session["BranchCode"].ToString() + " or monthid=0 order by MonthId";

            SqlConnection con = new SqlConnection();
            con = oo.dbGet_connection();
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                int k = 6;
                for (int i1 = 0; i1 < dt.Rows.Count; i1++)
                {
                    try
                    {
                        GridView2.HeaderRow.Cells[k].Text = dt.Rows[i1][0].ToString();
                        k++;
                    }
                    catch
                    {
                        // ignored
                    }
                }


                for (int l = k; l < GridView2.HeaderRow.Cells.Count - 2; l++)
                {
                    GridView2.Columns[l].Visible = false;
                }


            }
            con.Close();
        }
        protected void drpCard_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadStream();
            loadsection();
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            PrintHelper_New.ctrl = divExport;
            ClientScript.RegisterStartupScript(GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
        }
        
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            try
            {
                
                string crDate="";
                sql ="select format(getdate(), 'dd-MMM-yyyy hh:mm:ss') as curDate";
                crDate = crDate + oo.ReturnTag(sql, "curDate");
                
                string headerData = "Class : "+drpClass.SelectedItem.Text;

                sql = "  Select * from AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ")";
                sql = sql + " where ClassName ='" + drpClass.SelectedItem.Text + "' and Card='" + drpCard.SelectedItem + "' and Withdrwal is null";
                if (drpStream.SelectedIndex != 0)
                {
                    sql = sql + " and (BranchId='" + drpStream.SelectedValue + "' or BranchId is null)";
                    headerData = headerData + ", Branch : "+ drpStream.SelectedItem.Text;
                }
                if (drpSection.SelectedIndex != 0)
                {
                    sql = sql + " and SectionName='" + drpSection.SelectedItem + "'";
                    headerData = headerData + ", Section : " + drpSection.SelectedItem.Text;
                }
                sql = sql + " and (Promotion is null or Promotion<>'Cancelled') order by Name";
                GridView2.DataSource = oo.GridFill(sql);
                GridView2.DataBind();

                if (drpStream.SelectedIndex != 0 && drpSection.SelectedIndex != 0)
                {
                    headerData= "Class : " + drpClass.SelectedItem.Text + ", Branch : " + drpStream.SelectedItem.Text + ", Section : " + drpSection.SelectedItem.Text;
                }
                if (GridView2.Rows.Count > 0)
                {
                    divExport.Visible = true;
                    Panel1.Visible = true;
                    Label2.Text = "List of Fee";
                    lblCurrentDate.Text ="Date : "+ crDate;
                    Label4.Text = headerData;
                    sql = "select MonthName,mm.MonthId Id from MonthMaster mm";
                    sql = sql + " inner join FeeGroupMaster fgm on (Case when ISNUMERIC(CardType)=1 THEN fgm.Id Else fgm.FeeGroupName End)=mm.CardType and fgm.SessionName=mm.SessionName";
                    sql = sql + " inner join ClassMaster cm on cm.Id=mm.ClassId  and cm.SessionName=mm.SessionName";
                    sql = sql + " where fgm.FeeGroupName='" + drpCard.SelectedItem.Text + "' and cm.ClassName='" + drpClass.SelectedItem.Text + "' and MOD='I'  and mm.SessionName='" + Session["SessionName"] + "' ";
                    sql = sql + " and mm.BranchCode=" + Session["BranchCode"] + " or monthid=0 order by MonthId";
                    SqlConnection con = new SqlConnection();
                    con = oo.dbGet_connection();
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(sql, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        int k = 6;
                        for (int i1 = 0; i1 < dt.Rows.Count; i1++)
                        {
                            try
                            {
                                GridView2.HeaderRow.Cells[k].Text = dt.Rows[i1][0].ToString();
                                k++;
                            }
                            catch
                            {
                                // ignored
                            }
                        }


                        for (int l = k; l < GridView2.HeaderRow.Cells.Count - 2; l++)
                        {
                            GridView2.Columns[l].Visible = false;
                        }

                    }

                    con.Close();

                    for (j = 0; j <= GridView2.Rows.Count - 1; j++)
                    {
                        Label lblSrno = (Label)GridView2.Rows[j].FindControl("lblSrno");

                        Label lblArrear = (Label)GridView2.Rows[j].FindControl("lblArrear");
                        Label lblApr = (Label)GridView2.Rows[j].FindControl("lblApr");
                        Label lblMay = (Label)GridView2.Rows[j].FindControl("lblMay");
                        Label lblJun = (Label)GridView2.Rows[j].FindControl("lblJun");
                        Label lblJul = (Label)GridView2.Rows[j].FindControl("lblJul");
                        Label lblAug = (Label)GridView2.Rows[j].FindControl("lblAug");
                        Label lblSep = (Label)GridView2.Rows[j].FindControl("lblSep");
                        Label lblOct = (Label)GridView2.Rows[j].FindControl("lblOct");
                        Label lblNov = (Label)GridView2.Rows[j].FindControl("lblNov");
                        Label lblDec = (Label)GridView2.Rows[j].FindControl("lblDec");
                        Label lblJan = (Label)GridView2.Rows[j].FindControl("lblJan");
                        Label lblFeb = (Label)GridView2.Rows[j].FindControl("lblFeb");
                        Label lblMar = (Label)GridView2.Rows[j].FindControl("lblMar");
                        Label lblTotal = (Label)GridView2.Rows[j].FindControl("lblTotal");

                        if (lblSrno.Text.Trim() == "PP18/20")
                        {
                        }
                        DataTable dtAmount = GetMonthAmount(lblSrno.Text.Trim()).Tables[0];

                        string[] disData = null;
                        sql = "select count(*) cnt from DiscountMaster where SessionName = '" + Session["SessionName"].ToString() + "' and SrNo = '" + lblSrno.Text.Trim() + "'";
                        string cntDis = oo.ReturnTag(sql, "cnt").ToString();
                        if (cntDis != "0")
                        {
                            sql = "select discountvalue from DiscountMaster where SessionName = '" + Session["SessionName"].ToString() + "' and SrNo = '" + lblSrno.Text.Trim() + "'";
                            string str = oo.ReturnTag(sql, "discountvalue").ToString();
                            disData = str.Split(new string[] { " " }, StringSplitOptions.None);
                        }

                        
                        double total = 0;
                        sql = "select  Count(*) cntArrear from ArrierMast where SessionName='" + Session["SessionName"].ToString() + "' and Srno='" + lblSrno.Text.Trim() + "'";
                        if (oo.ReturnTag(sql, "cntArrear").ToString() != "0")
                        {
                            sql = "select  FeeAmount from ArrierMast where SessionName='" + Session["SessionName"].ToString() + "' and Srno='" + lblSrno.Text.Trim() + "'";
                            lblArrear.Text = double.Parse(oo.ReturnTag(sql, "FeeAmount").ToString()).ToString("0.0");
                            total = total + double.Parse(oo.ReturnTag(sql, "FeeAmount").ToString());
                        }
                        else
                        {
                            lblArrear.Text = "0";
                        }


                        col = dtAmount.Rows.Count+4;
                        int k1 = 5;
                        if (k1 <= col)
                        {
                            //if (lblSrno.Text.Trim()== "PP19/24")
                            //{
                            //    string lblInstallment = GridView2.HeaderRow.Cells[(k1+1)].Text;
                            //    var dsforfine = GetFine(lblSrno.Text.Trim(), lblInstallment, Session["SessionName"].ToString());
                            //    if (dsforfine != null)
                            //    {
                            //        fine = "0";
                            //    }
                            //}
                            
                            lblApr.Text = (double.Parse(dtAmount.Rows[0]["FeeAmount"].ToString())- double.Parse(cntDis=="0"?"0":disData[0])).ToString("0");
                            total = total + (double.Parse(dtAmount.Rows[0]["FeeAmount"].ToString()) - double.Parse(cntDis == "0" ? "0" : disData[0]));
                            k1 = k1 + 1;
                        }

                        if (k1 <= col)
                        {

                            lblMay.Text = (double.Parse(dtAmount.Rows[1]["FeeAmount"].ToString()) - double.Parse(cntDis == "0" ? "0" : disData[1])).ToString("0");
                            total = total + (double.Parse(dtAmount.Rows[1]["FeeAmount"].ToString()) - double.Parse(cntDis == "0" ? "0" : disData[1]));
                            k1 = k1 + 1;
                        }

                        if (k1 <= col)
                        {

                            lblJun.Text = (double.Parse(dtAmount.Rows[2]["FeeAmount"].ToString()) - double.Parse(cntDis == "0" ? "0" : disData[2])).ToString("0");
                            total = total + (double.Parse(dtAmount.Rows[2]["FeeAmount"].ToString()) - double.Parse(cntDis == "0" ? "0" : disData[2]));
                            k1 = k1 + 1;
                        }

                        if (k1 <= col)
                        {

                            lblJul.Text = (double.Parse(dtAmount.Rows[3]["FeeAmount"].ToString()) - double.Parse(cntDis == "0" ? "0" : disData[3])).ToString("0");
                            total = total + (double.Parse(dtAmount.Rows[3]["FeeAmount"].ToString()) - double.Parse(cntDis == "0" ? "0" : disData[3]));
                            k1 = k1 + 1;
                        }

                        if (k1 <= col)
                        {

                            lblAug.Text = (double.Parse(dtAmount.Rows[4]["FeeAmount"].ToString()) - double.Parse(cntDis == "0" ? "0" : disData[4])).ToString("0");
                            total = total + (double.Parse(dtAmount.Rows[4]["FeeAmount"].ToString()) - double.Parse(cntDis == "0" ? "0" : disData[4]));
                            k1 = k1 + 1;
                        }
                       

                        if (k1 <= col)
                        {

                            lblSep.Text = (double.Parse(dtAmount.Rows[5]["FeeAmount"].ToString()) - double.Parse(cntDis == "0" ? "0" : disData[5])).ToString("0");
                            total = total + (double.Parse(dtAmount.Rows[5]["FeeAmount"].ToString()) - double.Parse(cntDis == "0" ? "0" : disData[5]));
                            k1 = k1 + 1;
                        }
                       
                        if (k1 <= col)
                        {

                            lblOct.Text = (double.Parse(dtAmount.Rows[6]["FeeAmount"].ToString()) - double.Parse(cntDis == "0" ? "0" : disData[6])).ToString("0");
                            total = total + (double.Parse(dtAmount.Rows[6]["FeeAmount"].ToString()) - double.Parse(cntDis == "0" ? "0" : disData[6]));
                            k1 = k1 + 1;
                        }
                        

                        if (k1 <= col)
                        {

                            lblNov.Text = (double.Parse(dtAmount.Rows[7]["FeeAmount"].ToString()) - double.Parse(cntDis == "0" ? "0" : disData[7])).ToString("0");
                            total = total + (double.Parse(dtAmount.Rows[7]["FeeAmount"].ToString()) - double.Parse(cntDis == "0" ? "0" : disData[7]));
                            k1 = k1 + 1;
                        }
                        

                        if (k1 <= col)
                        {

                            lblDec.Text = (double.Parse(dtAmount.Rows[8]["FeeAmount"].ToString()) - double.Parse(cntDis == "0" ? "0" : disData[8])).ToString("0");
                            total = total + (double.Parse(dtAmount.Rows[8]["FeeAmount"].ToString()) - double.Parse(cntDis == "0" ? "0" : disData[8]));
                            k1 = k1 + 1;
                        }
                       

                        if (k1 <= col)
                        {
                            lblJan.Text = (double.Parse(dtAmount.Rows[9]["FeeAmount"].ToString()) - double.Parse(cntDis == "0" ? "0" : disData[9])).ToString("0");
                            total = total + (double.Parse(dtAmount.Rows[9]["FeeAmount"].ToString()) - double.Parse(cntDis == "0" ? "0" : disData[9]));
                            k1 = k1 + 1;
                        }
                        

                        if (k1 <= col)
                        {
                            lblFeb.Text = (double.Parse(dtAmount.Rows[10]["FeeAmount"].ToString()) - double.Parse(cntDis == "0" ? "0" : disData[10])).ToString("0");
                            total = total + (double.Parse(dtAmount.Rows[10]["FeeAmount"].ToString()) - double.Parse(cntDis == "0" ? "0" : disData[10]));
                            k1 = k1 + 1;
                        }
                       

                        if (k1 <= col)
                        {
                            lblMar.Text = (double.Parse(dtAmount.Rows[11]["FeeAmount"].ToString()) - double.Parse(cntDis == "0" ? "0" : disData[11])).ToString("0");
                            total = total + (double.Parse(dtAmount.Rows[11]["FeeAmount"].ToString()) - double.Parse(cntDis == "0" ? "0" : disData[11]));
                            k1 = k1 + 1;
                        }
                        
                        lblTotal.Text = total.ToString("0");
                    }

                    Label2.Visible = true;
                    divExport.Visible = true;

                    double ArrearSum = 0; double TotalSum = 0;
                    for (int i = 0; i < GridView2.Rows.Count; i++)
                    {
                        Label lblArrear = (Label)GridView2.Rows[i].FindControl("lblArrear");
                        Label lblTotal = (Label)GridView2.Rows[i].FindControl("lblTotal");
                        ArrearSum = ArrearSum + double.Parse(lblArrear.Text.Trim());
                        TotalSum = TotalSum + double.Parse(lblTotal.Text.Trim());
                    }
                    Label lblArrearSum = (Label)GridView2.FooterRow.FindControl("lblArrearSum");
                    Label lbllblTotalSum = (Label)GridView2.FooterRow.FindControl("lbllblTotalSum");
                    lblArrearSum.Text = ArrearSum.ToString("0.0");
                    lbllblTotalSum.Text = TotalSum.ToString("0");
                }
                else
                {
                    divExport.Visible = false;
                    Panel1.Visible = false;
                    oo.MessageBox("Sorry No Record found!", Page);
                }

            }
            catch(Exception ex)
            {
                // ignored
            }
        }

        public DataSet GetMonthAmount(string Srno)
        {
            var param = new List<SqlParameter>
            {
                new SqlParameter("@Srno", Srno),
                new SqlParameter("@SessionName", Session["SessionName"].ToString()),
                new SqlParameter("@BranchCode", Session["BranchCode"].ToString())
            };
            var ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("FeeAlltmentReportProc", param);

            return ds;
        }

        protected DataSet GetFine(string registervalue, string installment, string sessionname)
        {
            //decimal lateFine = 0;
            var param = new List<SqlParameter>
            {
                new SqlParameter("@registervalue", registervalue),
                new SqlParameter("@installment", installment),
                new SqlParameter("@sessionName", sessionname)
            };



            var ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("USP_GetFine", param);

            return ds;
        }
        protected DataSet GetFineforArrear(string registervalue, string installment, string sessionname)
        {
            //decimal lateFine = 0;
            var param = new List<SqlParameter>
            {
                new SqlParameter("@registervalue", registervalue),
                new SqlParameter("@installment", installment),
                new SqlParameter("@sessionName", sessionname)
            };


            var ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("USP_GetFineforArrear", param);

            return ds;
        }
        protected void ImageButton1_Click(object sender, EventArgs e)
        {
            loadheader();
            oo.ExportTolandscapeWord(Response, "EveryStudentBalanceList.doc", divExport);
        }
        protected void ImageButton2_Click(object sender, EventArgs e)
        {
            loadheader();
            oo.ExportDivToExcelWithFormatting(Response, "EveryStudentBalanceList.xls", divExport);
        }
        protected void ImageButton4_Click(object sender, EventArgs e)
        {
            loadheader();
            PrintHelper_New.ctrl = divExport;
            ClientScript.RegisterStartupScript(GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
        }
        protected void ImageButton3_Click(object sender, EventArgs e)
        {
            loadheader();
            oo.ExporttolandscapePdf(Response, "EveryStudentBalanceList.pdf", divExport);
        }

        
        public override void Dispose()
        {
            con.Dispose();
            con1.Dispose();
            oo.Dispose();
        }
    }
}