using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _2
{
    public partial class AdminEveryStuBalancedList : Page
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
        public AdminEveryStuBalancedList()
        {
            con = new SqlConnection();
            con1 = new SqlConnection();
            oo = new Campus();
            srno = 0;
            sql = string.Empty;
            sum = 0;
        }
        static AdminEveryStuBalancedList()
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
                Label2.Visible = false;
                divExport.Visible = false;
                loadclass();
                loadcard();
                oo.fillSelectvalue(drpStream, "<--Select-->");
                oo.fillSelectvalue(drpSection, "<--Select-->");
                oo.fillSelectvalue(drpMonth, "<--Select-->");
                Panel1.Visible = false;              
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
        public void monthName()
        {
            sql = "select MonthName,mm.MonthId Id from MonthMaster mm";
            sql = sql + " inner join FeeGroupMaster fgm on fgm.Id=mm.CardType and fgm.SessionName=mm.SessionName";
            sql = sql + " inner join ClassMaster cm on cm.Id=mm.ClassId and cm.SessionName=mm.SessionName";
            sql = sql + " where fgm.FeeGroupName='" + drpCard.SelectedItem.Text + "' and cm.ClassName='" + drpClass.SelectedItem.Text + "' and MOD='I'  and mm.SessionName='" + Session["SessionName"] + "' ";
            sql = sql + " and mm.BranchCode=" + Session["BranchCode"] + " or monthid=0 order by MonthId";

            oo.FillDropDown_withValue(sql, drpMonth, "MonthName", "Id");
            
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            PrintHelper_New.ctrl = divExport;
            ClientScript.RegisterStartupScript(GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
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
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            try
            {
                lblCurrentDate.Text = oo.CurrentDate();

                sql = "select (Case When '" + drpSection.SelectedItem + "'='<--Select-->' Then 'All' Else '" + drpSection.SelectedItem + "' End) SectionName,";
                sql = sql + " (Case When '" + drpStream.SelectedItem + "'='<--Select-->' Then 'All' Else '" + drpStream.SelectedItem + "' End) BranchName,ClassName from ClassMaster ";
                sql = sql + " where ClassName='" + drpClass.SelectedItem + "' and SessionName='" + Session["SessionName"] + "'";

                GridView1.DataSource = oo.GridFill(sql);
                GridView1.DataBind();

                if (GridView1.Rows.Count > 0)
                {
                    Panel1.Visible = true;


                    for (i = 0; i <= GridView1.Rows.Count - 1; i++)
                    {
                        Label Label1 = (Label)GridView1.Rows[i].FindControl("Label1");
                        GridView GridView2 = (GridView)GridView1.Rows[i].FindControl("GridView2");

                        sql = "  Select (Case when asr.FamilyContactNo='' then 'N/A' else asr.FamilyContactNo end ) as FatherContactNo, ";
                        sql = sql + " asr.SectionName,asr.Card,asr.Medium as Medium,asr.ClassName,convert(nvarchar,asr.DateOfAdmiission,106) as DateOfAdmiission ,";
                        sql = sql + " asr.SectionId,asr.FatherName,asr.MotherName,asr.Name,asr.BranchName,  ";
                        sql = sql + " asr.StEnRCode as StEnRCode,asr.srno  as srno,";
                        sql = sql + " case  when asr.TransportRequired='Yes' then 'Yes' else 'No' end as TransportRequired";
                        sql = sql + " from AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") asr";
                        sql = sql + " where asr.ClassName ='" + Label1.Text + "' and asr.Card='" + drpCard.SelectedItem + "' and asr.Withdrwal is null";
                        if (drpStream.SelectedIndex != 0)
                        {
                            sql = sql + " and (asr.BranchId='" + drpStream.SelectedValue + "' or asr.BranchId is null)";
                        }
                        if (drpSection.SelectedIndex != 0)
                        {
                            sql = sql + " and asr.SectionName='" + drpSection.SelectedItem + "'";
                        }
                        sql = sql + " and (asr.Promotion is null or asr.Promotion<>'Cancelled') order by asr.Name";

                        GridView2.DataSource = oo.GridFill(sql);
                        GridView2.DataBind();

                        if (GridView2.Rows.Count > 0)
                        {
                            sql = "select MonthName,mm.MonthId Id from MonthMaster mm";
                            sql = sql + " inner join FeeGroupMaster fgm on fgm.Id=mm.CardType and fgm.SessionName=mm.SessionName";
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
                                int k = 8;
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


                            //for (j = 0; j <= GridView2.Rows.Count - 1; j++)
                            //{
                            //    Label lblSno = (Label)GridView2.Rows[j].FindControl("lblSno");
                            //    lblSno.Text = (j + 1).ToString();


                            //}

                            //Start check Fee Mode Monthl Or Yearly
                            CheckFeeModeOFEveryStudent();
                            //End check Fee Mode Monthl Or Yearly
                            //Logic For Find the Deposit or Not Deposit

                            for (j = 0; j <= GridView2.Rows.Count - 1; j++)
                            {
                                Label lblSrno = (Label)GridView2.Rows[j].FindControl("lblSrno");
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
                                Label lblFeeMode = (Label)GridView2.Rows[j].FindControl("lblFeeMode");
                                //if (lblSrno.Text == "PP18/15")
                                //{

                                    Label lblChequeBounceFine = (Label)GridView2.Rows[j].FindControl("lblChequeBounceFine");
                                var chequeBounceFineAmount = BLL.BLLInstance.loadChequeBounceFineAmount(lblSrno.Text.Trim());
                                if (chequeBounceFineAmount != null)
                                {
                                    if (chequeBounceFineAmount.Tables.Count > 0)
                                    {
                                        if (chequeBounceFineAmount.Tables[0].Rows.Count > 0)
                                        {
                                            lblChequeBounceFine.Text = chequeBounceFineAmount.Tables[0].Rows[0]["BouncedCharge"].ToString() == string.Empty ? "0" : chequeBounceFineAmount.Tables[0].Rows[0]["BouncedCharge"].ToString();
                                        }
                                        else
                                        {
                                            lblChequeBounceFine.Text = "0";
                                        }
                                    }
                                    else
                                    {
                                        lblChequeBounceFine.Text = "0";
                                    }
                                }
                                else
                                {
                                    lblChequeBounceFine.Text = "0";
                                }

                                for (int k = 8; k < GridView2.HeaderRow.Cells.Count; k++)
                                {
                                    if (GridView2.HeaderRow.Cells[k].Text == drpMonth.SelectedItem.ToString())
                                    {
                                        col = k;
                                        break;
                                    }
                                }

                                int k1 = 8;
                                if (k1 <= col)
                                {
                                    if (findDepositOrNotDepositTrueOrFalse(lblSrno.Text, GridView2.HeaderRow.Cells[8].Text))
                                    {
                                        lblApr.Text = balanceAmount(lblSrno.Text, GridView2.HeaderRow.Cells[8].Text).ToString(CultureInfo.InvariantCulture);
                                    }
                                    else
                                    {

                                        lblApr.Text = (ToCheckFeeAmountForParticularSrno(lblSrno.Text, GridView2.HeaderRow.Cells[8].Text, lblFeeMode.Text) + undepositarrieramount(lblSrno.Text, "Arrear")).ToString(CultureInfo.InvariantCulture);
                                    }
                                    k1 = k1 + 1;
                                }

                                if (k1 <= col)
                                {
                                    if (findDepositOrNotDepositTrueOrFalse(lblSrno.Text, GridView2.HeaderRow.Cells[9].Text))
                                    {
                                        lblMay.Text = balanceAmount(lblSrno.Text, GridView2.HeaderRow.Cells[9].Text).ToString(CultureInfo.InvariantCulture);
                                    }
                                    else
                                    {
                                        lblMay.Text = ToCheckFeeAmountForParticularSrno(lblSrno.Text, GridView2.HeaderRow.Cells[9].Text, lblFeeMode.Text).ToString(CultureInfo.InvariantCulture);
                                    }
                                    k1 = k1 + 1;
                                }

                                if (k1 <= col)
                                {
                                    if (findDepositOrNotDepositTrueOrFalse(lblSrno.Text, GridView2.HeaderRow.Cells[10].Text))
                                    {
                                        lblJun.Text = balanceAmount(lblSrno.Text, GridView2.HeaderRow.Cells[10].Text).ToString(CultureInfo.InvariantCulture);
                                    }
                                    else
                                    {
                                        lblJun.Text = ToCheckFeeAmountForParticularSrno(lblSrno.Text, GridView2.HeaderRow.Cells[10].Text, lblFeeMode.Text).ToString(CultureInfo.InvariantCulture);
                                    }
                                    k1 = k1 + 1;
                                }

                                if (k1 <= col)
                                {
                                    if (findDepositOrNotDepositTrueOrFalse(lblSrno.Text, GridView2.HeaderRow.Cells[11].Text))
                                    {
                                        lblJul.Text = balanceAmount(lblSrno.Text, GridView2.HeaderRow.Cells[11].Text).ToString(CultureInfo.InvariantCulture);
                                    }
                                    else
                                    {
                                        lblJul.Text = ToCheckFeeAmountForParticularSrno(lblSrno.Text, GridView2.HeaderRow.Cells[11].Text, lblFeeMode.Text).ToString(CultureInfo.InvariantCulture);
                                    }
                                    k1 = k1 + 1;
                                }

                                if (k1 <= col)
                                {
                                    if (findDepositOrNotDepositTrueOrFalse(lblSrno.Text, GridView2.HeaderRow.Cells[12].Text))
                                    {
                                        lblAug.Text = balanceAmount(lblSrno.Text, GridView2.HeaderRow.Cells[12].Text).ToString(CultureInfo.InvariantCulture);
                                    }
                                    else
                                    {
                                        lblAug.Text = ToCheckFeeAmountForParticularSrno(lblSrno.Text, GridView2.HeaderRow.Cells[12].Text, lblFeeMode.Text).ToString(CultureInfo.InvariantCulture);
                                    }
                                    k1 = k1 + 1;
                                }

                                if (k1 <= col)
                                {
                                    if (findDepositOrNotDepositTrueOrFalse(lblSrno.Text, GridView2.HeaderRow.Cells[13].Text))
                                    {
                                        lblSep.Text = balanceAmount(lblSrno.Text, GridView2.HeaderRow.Cells[13].Text).ToString(CultureInfo.InvariantCulture);
                                    }
                                    else
                                    {
                                        lblSep.Text = ToCheckFeeAmountForParticularSrno(lblSrno.Text, GridView2.HeaderRow.Cells[13].Text, lblFeeMode.Text).ToString(CultureInfo.InvariantCulture);
                                    }
                                    k1 = k1 + 1;
                                }

                                if (k1 <= col)
                                {
                                    if (findDepositOrNotDepositTrueOrFalse(lblSrno.Text, GridView2.HeaderRow.Cells[14].Text))
                                    {
                                        lblOct.Text = balanceAmount(lblSrno.Text, GridView2.HeaderRow.Cells[14].Text).ToString(CultureInfo.InvariantCulture);
                                    }
                                    else
                                    {
                                        //oo.MessageBox(GridView2.HeaderRow.Cells[12].Text, Page);
                                        lblOct.Text = ToCheckFeeAmountForParticularSrno(lblSrno.Text, GridView2.HeaderRow.Cells[14].Text, lblFeeMode.Text).ToString(CultureInfo.InvariantCulture);
                                    }
                                    k1 = k1 + 1;
                                }

                                if (k1 <= col)
                                {
                                    if (findDepositOrNotDepositTrueOrFalse(lblSrno.Text, GridView2.HeaderRow.Cells[15].Text))
                                    {
                                        lblNov.Text = balanceAmount(lblSrno.Text, GridView2.HeaderRow.Cells[15].Text).ToString(CultureInfo.InvariantCulture);
                                    }
                                    else
                                    {
                                        lblNov.Text = ToCheckFeeAmountForParticularSrno(lblSrno.Text, GridView2.HeaderRow.Cells[15].Text, lblFeeMode.Text).ToString(CultureInfo.InvariantCulture);
                                    }
                                    k1 = k1 + 1;
                                }

                                if (k1 <= col)
                                {
                                    if (findDepositOrNotDepositTrueOrFalse(lblSrno.Text, GridView2.HeaderRow.Cells[16].Text))
                                    {
                                        lblDec.Text = balanceAmount(lblSrno.Text, GridView2.HeaderRow.Cells[16].Text).ToString(CultureInfo.InvariantCulture);
                                    }
                                    else
                                    {
                                        lblDec.Text = ToCheckFeeAmountForParticularSrno(lblSrno.Text, GridView2.HeaderRow.Cells[16].Text, lblFeeMode.Text).ToString(CultureInfo.InvariantCulture);
                                    }
                                    k1 = k1 + 1;
                                }

                                if (k1 <= col)
                                {
                                    if (findDepositOrNotDepositTrueOrFalse(lblSrno.Text, GridView2.HeaderRow.Cells[17].Text))
                                    {
                                        lblJan.Text = balanceAmount(lblSrno.Text, GridView2.HeaderRow.Cells[17].Text).ToString(CultureInfo.InvariantCulture);

                                    }
                                    else
                                    {
                                        lblJan.Text = ToCheckFeeAmountForParticularSrno(lblSrno.Text, GridView2.HeaderRow.Cells[17].Text, lblFeeMode.Text).ToString(CultureInfo.InvariantCulture);
                                    }
                                    k1 = k1 + 1;
                                }

                                if (k1 <= col)
                                {
                                    if (findDepositOrNotDepositTrueOrFalse(lblSrno.Text, GridView2.HeaderRow.Cells[18].Text))
                                    {
                                        lblFeb.Text = balanceAmount(lblSrno.Text, GridView2.HeaderRow.Cells[18].Text).ToString(CultureInfo.InvariantCulture);
                                    }
                                    else
                                    {
                                        lblFeb.Text = ToCheckFeeAmountForParticularSrno(lblSrno.Text, GridView2.HeaderRow.Cells[18].Text, lblFeeMode.Text).ToString(CultureInfo.InvariantCulture);
                                    }
                                    k1 = k1 + 1;
                                }

                                if (k1 <= col)
                                {
                                    if (findDepositOrNotDepositTrueOrFalse(lblSrno.Text, GridView2.HeaderRow.Cells[19].Text))
                                    {
                                        lblMar.Text = balanceAmount(lblSrno.Text, GridView2.HeaderRow.Cells[19].Text).ToString(CultureInfo.InvariantCulture);
                                    }
                                    else
                                    {
                                        lblMar.Text = ToCheckFeeAmountForParticularSrno(lblSrno.Text, GridView2.HeaderRow.Cells[19].Text, lblFeeMode.Text).ToString(CultureInfo.InvariantCulture);
                                    }
                                    k1 = k1 + 1;
                                }
                                //} //--
                                Label lblSno = (Label)GridView2.Rows[j].FindControl("lblSno");
                                lblSno.Text = (j + 1).ToString();
                            }
                            FindTotalDuesofStudent();
                            ShowHideMonths();

                            Label2.Visible = true;
                            divExport.Visible = true;


                        }
                        else
                        {
                            divExport.Visible = false;
                            oo.MessageBox("Sorry No Record found!", Page);
                        }
                        //int cnt1 = 0; int cnt2 = 1;
                        //Label lblTotalSTU = (Label)GridView2.FooterRow.FindControl("lblTotalSTU");
                        //if (drpFilter.SelectedValue== "Defaulters")
                        //{
                        //    for (int m = 0; m < GridView2.Rows.Count-1; m++)
                        //    {
                        //        Label lblTotBalance = (Label)GridView2.Rows[m].FindControl("lblTotBalance");
                        //        if (lblTotBalance.Text!="0")
                        //        {
                        //            cnt1= cnt1 + 1;
                        //        }
                        //    }
                        //    lblTotalSTU.Text = (cnt1).ToString();
                        //}
                        //else
                        //{
                        //    for (int n = 0; n < GridView2.Rows.Count - 1; n++)
                        //    {
                        //        cnt2 = cnt2 + 1;
                        //    }
                        //    lblTotalSTU.Text = (cnt2).ToString();
                        //}


                    }
                    
                    //ScriptManager.RegisterClientScriptBlock(Page, GetType(), "reordergrid", "reOrderGrid()", true);
                    
                }


                else
                {
                    divExport.Visible = false;
                    //oo.MessageBox("Sorry No Record found!", Page);
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Sorry No Record found!", "A");       

                }
                
            }
            catch(Exception ex)
            {
                // ignored
            }
        }
        public double balanceAmount(string srno, string smonth)
        {
                srno = srno.Trim();
                double sum = 0; string classid = ""; string sql1 = "", sql2 = ""; string TypeOFAdmision = "";
            //if (srno == "PP18/15")
            //{
                sql = "select Max(Id) as Id, FeeMonth from FeeDeposite where Status='Paid' and SrNo='" + srno + "' and cancel is null and SessionName='" + Session["SessionName"].ToString() + "'  group by FeeMonth";
                string maxid = oo.ReturnTag(sql, "Id");

                sql1 = "select ClassId, TypeOFAdmision, Branchid, Medium from AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "', " + Session["BranchCode"].ToString() + ") where SrNo = '" + srno + "'";
                classid = oo.ReturnTag(sql1, "ClassId");
                TypeOFAdmision = oo.ReturnTag(sql1, "TypeOFAdmision");
                string Branchid= oo.ReturnTag(sql1, "Branchid");
                string Medium = oo.ReturnTag(sql1, "Medium");
                sql = "select sum(FeePayment) as InstallmentAmount from FeeAllotedForClassWise where SessionName = '" + Session["SessionName"].ToString() + "' and Month = '" + smonth + "' and Class = " + classid + " and Branchid="+ drpStream.SelectedValue + " and AdmissionType = '" + TypeOFAdmision + "' and Medium='" + Medium + "' group by Month";
                if (oo.ReturnTag(sql, "InstallmentAmount") != "")
                {
                    sum = sum + double.Parse(oo.ReturnTag(sql, "InstallmentAmount"));
                }
                if (transportfee(srno, smonth) > 0)
                {
                    sum = sum + transportfee(srno, smonth);
                }
                string sqlss = "select sum(LateFineAmount) fine from FeeReceiptWithInstallment where (Status = 'Paid' or Status = 'Pending') and SrNo = '" + srno + "' and  SessionName = '" + Session["SessionName"].ToString() + "' and InstallmentName = '" + smonth + "' and IsCancel=0";
                if (oo.ReturnTag(sqlss, "fine").ToString() != "" && oo.ReturnTag(sqlss, "fine").ToString() != "0" && oo.ReturnTag(sqlss, "fine").ToString() != "0.00")
                {
                    sum = sum + double.Parse(oo.ReturnTag(sqlss, "fine"));
                }
                else
                {
                    sum = sum + CalculateFine(srno, smonth, Session["SessionName"].ToString());
                }
                sql = "Select MAx(Id) id from FeeReceiptWithInstallment where (Status = 'Paid' or Status = 'Pending') and SrNo = '" + srno + "' and SessionName = '" + Session["SessionName"].ToString() + "' and IsCancel = 0";
                maxid = oo.ReturnTag(sql, "Id");

                string sql0 = "Select ManualDiscAmount  from FeeReceiptWithInstallment where (Status = 'Paid' or Status = 'Pending') and Id='" + maxid + "' and SrNo='" + srno + "' and InstallmentName='" + smonth + "' and SessionName='" + Session["SessionName"].ToString() + "' and IsCancel=0";
                string mamnulDis = double.Parse(oo.ReturnTag(sql0, "ManualDiscAmount") == "" ? "0" : oo.ReturnTag(sql0, "ManualDiscAmount")).ToString("0");

                string mamnulDiscount = "-0";
                if (double.Parse(mamnulDis).ToString("0") == "0")
                {
                    sql2 = "declare @ID varchar(200)";
                    sql2 = sql2 + " set @ID = (select DiscountValue from DiscountMaster where SrNo = '" + srno + "' and SessionName = '" + Session["SessionName"].ToString() + "')";
                    sql2 = sql2 + " select amount, MonthName from (";
                    sql2 = sql2 + " SELECT ROW_NUMBER() OVER(ORDER BY @id) RNO, Split.a.value('.', 'NVARCHAR(MAX)') amount";

                    sql2 = sql2 + " FROM(SELECT CAST('<X>' + REPLACE(@ID, ' ', '</X><X>') + '</X>' AS XML) AS String) AS A CROSS APPLY String.nodes('/X') AS Split(a))t1";
                    sql2 = sql2 + " inner join";
                    sql2 = sql2 + " (select ROW_NUMBER() OVER(ORDER BY MonthId) RNO, MonthName, MonthId Id from MonthMaster where SessionName = '" + Session["SessionName"].ToString() + "' and ClassId = " + classid + "";
                    sql2 = sql2 + " and BranchCode = " + Session["BranchCode"].ToString() + " and MonthId <> 0)T2";
                    sql2 = sql2 + " on t1.RNO = t2.RNO where MonthName = '" + smonth + "'";

                    mamnulDiscount = oo.ReturnTag(sql2, "amount");
                }
                else
                {
                    mamnulDiscount = mamnulDis;
                }
                if (mamnulDiscount != "" && double.Parse(mamnulDiscount) < 0)
                {
                    mamnulDiscount = (double.Parse(mamnulDiscount) * (-1)).ToString();
                }
                sum = sum - double.Parse(mamnulDiscount == "" ? "0" : mamnulDiscount);

                string sql0s = "Select AutoDiscAmount  from FeeReceiptWithInstallment where (Status = 'Paid' or Status = 'Pending') and SrNo='" + srno + "' and InstallmentName='" + smonth + "' and SessionName='" + Session["SessionName"].ToString() + "' and IsCancel=0";
                string AutoDisc = double.Parse(oo.ReturnTag(sql0s, "AutoDiscAmount") == "" ? "0" : oo.ReturnTag(sql0s, "AutoDiscAmount")).ToString("0");
                if (double.Parse(AutoDisc == "" ? "0" : AutoDisc)<0)
                {
                    sum = sum + double.Parse(AutoDisc == "" ? "0" : AutoDisc);
                }
                else
                {
                    sum = sum - double.Parse(AutoDisc == "" ? "0" : AutoDisc);
                }

                sql = "select sum(PaidAmount) PaidAmount from FeeReceiptWithInstallment where Status='Paid' and SrNo='" + srno + "' and InstallmentName='" + smonth + "' and SessionName='" + Session["SessionName"].ToString() + "' and IsCancel=0";
                if (oo.ReturnTag(sql, "PaidAmount") != "")
                {
                    sum = sum - double.Parse(oo.ReturnTag(sql, "PaidAmount"));
                }
        //}//--
            return sum;
        }
        private double transportfee(string srno, string insttalment)
        {
            double conv = 0;
            sql = "select TransportRequired from StudentOfficialDetails ";
            sql = sql + "  where  SrNo='" + srno + "' and SessionName='" + Session["SessionName"].ToString() + "'";
            string transport = oo.ReturnTag(sql, "TransportRequired");
            if (transport.ToUpper() == "YES")
            {
                sql = "Select * from StudentVehicleAllotment where SrNo='" + srno + "' and SessionName='" + Session["SessionName"].ToString() + "' and Insttalment='" + insttalment + "' and MonthStatus=1";
                if (oo.Duplicate(sql))
                {
                    //sql = "Select srno from FeeDeposite where (FeeMonth='" + insttalment + "' or FeeMonth='" + "(T) " + insttalment + "' or FeeMonth='Yearly') and BusConvience<>'0.00' and SrNo='" + srno + "' and SessionName='" + Session["SessionName"].ToString() + "' and Cancel is null";
                    //sql = sql + " Union";
                    //sql = sql + " Select srno from FeeReceiptWithInstallment where InstallmentName = '" + insttalment + "' and ConveyanceAmount> 0 and IsCancel = 0 and SrNo='" + srno + "' and SessionName='" + Session["SessionName"].ToString() + "'";

                    //if (oo.Duplicate(sql) == false)
                    //{
                        sql = "Select Sum(Amount) as Amount from StudentVehicleAllotment where SessionName='" + Session["SessionName"].ToString() + "' and MonthStatus='1' and SrNo='" + srno + "' and Insttalment='" + insttalment + "'";
                        double convamount = double.TryParse(oo.ReturnTag(sql, "Amount"), out convamount) ? convamount : 0;
                        sql = "Select ForMonth from MonthMaster where MonthName='" + insttalment + "' and SessionName='" + Session["SessionName"].ToString() + "'";
                        int noofmonth = int.TryParse(oo.ReturnTag(sql, "ForMonth"), out noofmonth) ? noofmonth : 0;
                        if (noofmonth > 0)
                        {
                            conv = conv + (convamount * noofmonth);
                        }
                        else
                        {
                            conv = conv + convamount;
                        }
                    }
                //}
            }

            return conv;
        }
        public void counttotalamount()
        {
            try
            {
                if (GridView1.Rows.Count > 0)
                {
                    GridView Gridview2 = (GridView)GridView1.Rows[0].FindControl("GridView2");
                    if (Gridview2.Rows.Count > 0)
                    {
                        double TotalAmount = 0;
                        for (int i = 0; i < Gridview2.Rows.Count; i++)
                        {
                            Label lblTotBalance = (Label)Gridview2.Rows[i].FindControl("lblTotBalance");
                            TotalAmount = TotalAmount + Convert.ToDouble(lblTotBalance.Text);
                        }
                        Label lblTotalAmount = (Label)Gridview2.FooterRow.FindControl("lblTotalAmount");
                        lblTotalAmount.Text = TotalAmount.ToString(".00");
                    }

                }
            }
            catch
            {
                // ignored
            }
        }
        public bool findDepositOrNotDepositTrueOrFalse(string srno, string MonthName)
        {
            bool flag = false;
            //if (srno == "PP18/15")
            //{
                sql = "select count(*) as Counter from FeeDeposite where Status='Paid' and srno='" + srno + "' and cancel is null  and FeeMonth like '" + MonthName + "%' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
                string count;
                count = oo.ReturnTag(sql, "Counter").ToString();
                if (count == "0")
                {
                    sql = "select count(*) as Counter from FeeReceiptWithInstallment where (Status = 'Paid' or Status = 'Pending') and srno = '" + srno + "' and IsCancel = 0";
                    sql = sql + " and InstallmentName = '" + MonthName + "' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
                    count = oo.ReturnTag(sql, "Counter").ToString();
                    //if (count=="0" && ss==1)
                    //{
                    //    sql = "select count(*) as Counter from FeeReceiptWithInstallment where Status = 'Paid' and srno = '" + srno + "' and IsCancel = 0";
                    //    sql = sql + " and InstallmentName = 'Arrear' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
                    //    count = oo.ReturnTag(sql, "Counter").ToString();
                    //}

                }
                if (count == "0")
                {
                    //oo.MessageBox(srno.ToString() + "//" + flag.ToString(), Page);
                    flag = false;
                }
                else
                {
                    flag = true;
                }
            //}//--
            return flag;
        }
        public void CheckFeeModeOFEveryStudent()
        {
            for (i = 0; i <= GridView1.Rows.Count - 1; i++)
            {
                Label Label1 = (Label)GridView1.Rows[i].FindControl("Label1");
                GridView GridView2 = (GridView)GridView1.Rows[i].FindControl("GridView2");
                for (j = 0; j <= GridView2.Rows.Count - 1; j++)
                {
                    Label lblSrno = (Label)GridView2.Rows[j].FindControl("lblSrno");
                    Label lblFeeMode = (Label)GridView2.Rows[j].FindControl("lblFeeMode");
                    if (FeeModeCheck(lblSrno.Text) == "Yearly")
                    {
                        lblFeeMode.ForeColor = System.Drawing.Color.Red;
                    }
                    lblFeeMode.Text = FeeModeCheck(lblSrno.Text);
                }
            }
        }
        public string FeeModeCheck(string srno)
        {
            sql = "select FeeMonth from FeeDeposite where Status='Paid' and cancel is null and srno='" + srno + "'  and SessionName='" + Session["SessionName"].ToString() + "' and FeeMonth='Yearly' and BranchCode=" + Session["BranchCode"].ToString() + "";
            if (oo.ReturnTag(sql, "FeeMonth") == "Yearly")
            {
                return "Yearly";
            }
            else
            {
                return "Monthly";
            }
        }
        public List<decimal> Get_AutometicDiscountAmount(string registervalue, string installmentName)
        {
            List<decimal> discamount = new List<decimal>();

            decimal discountamount1 = 0, discountamount2 = 0, discountamount3 = 0, discountamount4 = 0;
            decimal totaldiscount = 0;
            string autoDisc = "0";
            string manualDisc = "0";
            string fconcDisc = "0";
            string cconcDisc = "0";
            DataSet ds = new DataSet();

            List<SqlParameter> param = new List<SqlParameter>();

            param.Add(new SqlParameter("@RegisterValue", registervalue));
            param.Add(new SqlParameter("@InstallmentName", installmentName));
            param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
            param.Add(new SqlParameter("@isAnnualDeposit", isAnualDeposit == true ? "A" : "I"));


            ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("USP_GetAutometicDiscountAmount", param);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    autoDisc = ds.Tables[0].Rows[0]["AutoDisc"].ToString();
                    manualDisc = ds.Tables[0].Rows[0]["ManualDisc"].ToString();
                    fconcDisc = ds.Tables[0].Rows[0]["FConc"].ToString();
                    cconcDisc = ds.Tables[0].Rows[0]["CConc"].ToString();
                }
            }
            decimal.TryParse(autoDisc, out discountamount1);
            decimal.TryParse(manualDisc, out discountamount2);
            decimal.TryParse(fconcDisc, out discountamount3);
            decimal.TryParse(cconcDisc, out discountamount4);

            totaldiscount = discountamount1 + discountamount2 + discountamount3 + discountamount4;

            discamount.Add(discountamount1);
            discamount.Add(discountamount2);
            discamount.Add(discountamount3);
            discamount.Add(discountamount4);
            discamount.Add(totaldiscount);


            return discamount;
        }
        public double ToCheckFeeAmountForParticularSrno(string srno, string smonth, string Feemode)
        {


            string med = "", typeOfAdd = string.Empty;
            string crd = "", clna = "", branchid = "", transportRequired = "";
            //if (srno == "PP18/15")
            //{

#pragma warning disable 219
                int j = 0;
#pragma warning restore 219
#pragma warning disable 219
            string sql3 = "";
#pragma warning restore 219
            double damt = 0;
#pragma warning disable 219
            double conc = 0;
#pragma warning restore 219
            double tutionFees = 0;
            int noMonths = 0;
            SqlCommand cmd1;


            sql = " select sf.FatherContactNo as FatherContactNo,SG.Id,AdmissionForClassId, SC.SectionName,SO.Card,So.MODForFeeDeposit,So.Branch,SO.Medium as Medium,CM.ClassName,convert(nvarchar,So.DateOfAdmiission,106) as DateOfAdmiission ,SO.SectionId,Sf.FatherName,SF.MotherName,SG.FirstName+' '+SG.MiddleName+' '+SG.LastName as SName, ";
            sql = sql + " sg.StEnRCode as StEnRCode,sg.srno  as srno,case  when so.TransportRequired='Yes' then 'Yes' else 'No' end as TransportRequired, ";
            sql = sql + " so.wayamount as wayamount,so.TypeOFAdmision as TypeOFAdmision from StudentGenaralDetail SG      ";
            sql = sql + " left join StudentFamilyDetails SF on SG.srno=SF.srno    ";
            sql = sql + " left join StudentOfficialDetails SO on SG.srno=SO.srno    ";
            sql = sql + "  left join ClassMaster CM on SO.AdmissionForClassId=CM.Id    ";
            sql = sql + "  left join SectionMaster SC on SO.SectionId=SC.Id      ";
            sql = sql + "    where so.card='" + drpCard.SelectedItem.Text.ToString() + "' and so.Srno='"+srno+"' ";
            sql = sql + " and sg.SessionName='" + Session["SessionName"].ToString() + "' and ";
            sql = sql + "     so.SessionName='" + Session["SessionName"].ToString() + "' and sf.SessionName='" + Session["SessionName"].ToString() + "' and cm.SessionName='" + Session["SessionName"].ToString() + "'";
            sql = sql + "    and SC.SessionName='" + Session["SessionName"].ToString() + "'  and";
            sql = sql + " sg.BranchCode=" + Session["BranchCode"].ToString() + "";
            sql = sql + "   and SO.Withdrwal is null and (SO.Promotion is null or SO.Promotion<>'Cancelled')";


            isAnualDeposit = BAL.objBal.ReturnTag(sql, "MODForFeeDeposit") == "A" ? true : false;

            clna = oo.ReturnTag(sql, "ClassName");
            typeOfAdd = oo.ReturnTag(sql, "TypeOFAdmision");
            med = oo.ReturnTag(sql, "Medium");
            crd = oo.ReturnTag(sql, "card");
            branchid = oo.ReturnTag(sql, "Branch");
            transportRequired = oo.ReturnTag(sql, "TransportRequired");
            sum = 0;

            string classid = oo.ReturnTag(sql, "AdmissionForClassId");

            sql = " select distinct ROW_NUMBER() OVER (ORDER BY fa.Id ASC) AS SrNo,Id,fa.Month, fa.FeeParticular,fa.Class,fa.FeeType,fa.FeePayment,FM.Medium,Fa.CardType ,fm.NoOfmonths as NoOfmonths from FeeAllotedForClassWise fa ";
            sql = sql + " left join feemaster  fm on fa.Medium=fm.medium  and (fa.FeeType=fm.FeeName  or fa.FeeName=fm.FeeName )  and fm.SessionName=fa.SessionName ";
            sql = sql + " where (fa.Class='" + clna + "' or fa.Class='" + classid + "') and  fa.Month='" + smonth + "' and fa.SessionName='" + Session["SessionName"].ToString() + "'  and fa.BranchCode=" + Session["BranchCode"].ToString() + "";
            sql = sql + " and fa.CardType='" + crd + "' and   fa.AdmissionType='" + typeOfAdd.Trim() + "'";
            sql = sql + " and (MOD='I' or MOD is null) and (fa.Branchid='" + branchid + "' or fa.Branchid is null) ";
            sql = sql + " and (fa.Medium='" + med + "' or fa.Medium is null)";

            try
            {
                SqlDataReader dr1;
                con1.Open();
                using (cmd1 = new SqlCommand(sql, con1))
                {
                    dr1 = cmd1.ExecuteReader();
                    while (dr1.Read())
                    {
                        try
                        {
                            if (Feemode != "Yearly")
                            {
                                sum = sum + Convert.ToDouble(dr1["FeePayment"].ToString());
                            }
                        }
                        catch (Exception e)
                        {
                            oo.MessageBox(e.ToString(), Page);

                        }

                        if (dr1["FeeType"].ToString().Substring(0, 3).ToString().ToUpper() == "TUT" || dr1["FeeType"].ToString().Substring(0, 3).ToString().ToUpper() == "MON" || dr1["FeeType"].ToString().Substring(0, 3).ToString().ToUpper() == "TUI")
                        {
                            try
                            {
                                int values;
                                int.TryParse(dr1["NoOfMonths"].ToString(), out values);
                                noMonths = values == 0 ? 1 : values;
                            }
                            catch (Exception e)
                            {
                                oo.MessageBox(e.ToString(), Page);
                            }
                            try
                            {
                                tutionFees = Convert.ToDouble(dr1["FeePayment"].ToString());
                            }
                            catch (Exception e)
                            {
                                oo.MessageBox(e.ToString(), Page);

                            }
                        }

                    }
                    con1.Close();
                }

            }
            catch (Exception) { con1.Close(); }
            List<decimal> discountvalues = new List<decimal>();
            discountvalues = Get_AutometicDiscountAmount(srno, smonth);
            if (Feemode != "Yearly")
            {
                damt = Convert.ToDouble(discountvalues[0] + discountvalues[1] + discountvalues[2]);

                sum = sum + damt;
            }
            if (Feemode == "Yearly")
            {
                if (transportRequired == "Yes")
                {
                    sql = "Select *from FeeDeposite where BusConvience=0 and FeeMonth='Yearly' and Cancel is null and SrNo='" + srno + "' and SessionName='" + Session["SessionName"].ToString() + "'";
                    if (oo.Duplicate(sql))
                    {

                        damt = Convert.ToDouble(discountvalues[3]);

                        sum = sum + damt;

                        string WayAmount = "";

                        sql = " Select fd.srno from FeeDeposite fd inner join StudentOfficialDetails sd on fd.SrNo=sd.SrNo and fd.StEnRCode=sd.StEnRCode";
                        sql = sql + " where fd.Status='Paid' and fd.SessionName='" + Session["SessionName"].ToString() + "' and sd.SessionName='" + Session["SessionName"].ToString() + "'";
                        sql = sql + " and fd.SrNo='" + srno + "' and fd.FeeMonth like '%" + smonth + "%' and Cancel is null and Withdrwal is null";
                        sql = sql + " Union";
                        sql = sql + " Select srno from FeeReceiptWithInstallment where InstallmentName = '" + smonth + "' and InstallmentName<>'Arrear' and ConveyanceAmount> 0 and IsCancel = 0 and SrNo='" + srno + "' and SessionName='" + Session["SessionName"].ToString() + "'";

                        if (oo.Duplicate(sql) == false)
                        {

                            sql = "Select Sum(Amount) as Amount from StudentVehicleAllotment where SessionName='" + Session["SessionName"].ToString() + "' and MonthStatus='1' and srno='" + srno + "' and Insttalment='" + smonth + "'";
                            WayAmount = oo.ReturnTag(sql, "Amount");
                            if (WayAmount != "")
                            {
                                if (noMonths != 0)
                                {
                                    sum = sum + (Convert.ToDouble(WayAmount) * noMonths);
                                }
                                else
                                {
                                    sql = "Select ForMonth from MonthMaster where MonthName='" + smonth + "' and SessionName='" + Session["SessionName"].ToString() + "'";
                                    int noofmonth = int.TryParse(oo.ReturnTag(sql, "ForMonth"), out noofmonth) ? noofmonth : 0;
                                    if (noofmonth > 0)
                                    {
                                        sum = sum + (Convert.ToDouble(WayAmount) * noofmonth);
                                    }
                                    else
                                    {
                                        sum = sum + (Convert.ToDouble(WayAmount));
                                    }
                                }
                            }

                        }
                    }

                }
            }
            else
            {
                if (transportRequired == "Yes")
                {
                    string WayAmount = "";

                    sql = " Select fd.srno from FeeDeposite fd inner join StudentOfficialDetails sd on fd.SrNo=sd.SrNo and fd.StEnRCode=sd.StEnRCode";
                    sql = sql + " where fd.Status='Paid' and fd.SessionName='" + Session["SessionName"].ToString() + "' and sd.SessionName='" + Session["SessionName"].ToString() + "'";
                    sql = sql + " and fd.SrNo='" + srno + "' and fd.FeeMonth like '%" + smonth + "%' and Cancel is null and Withdrwal is null";
                    sql = sql + " Union";
                    sql = sql + " Select srno from FeeReceiptWithInstallment where InstallmentName = '" + smonth + "' and InstallmentName<>'Arrear' and ConveyanceAmount> 0 and IsCancel = 0 and SrNo='" + srno + "' and SessionName='" + Session["SessionName"].ToString() + "'";

                    if (oo.Duplicate(sql) == false)
                    {
                        damt = Convert.ToDouble(discountvalues[3]);

                        sum = sum + damt;

                        sql = "Select Sum(Amount) as Amount from StudentVehicleAllotment where SessionName='" + Session["SessionName"].ToString() + "' and MonthStatus='1' and srno='" + srno + "' and Insttalment='" + smonth + "'";
                        WayAmount = oo.ReturnTag(sql, "Amount");
                        if (WayAmount != "")
                        {
                            if (noMonths != 0)
                            {
                                sum = sum + (Convert.ToDouble(WayAmount) * noMonths);
                            }
                            else
                            {
                                sql = "Select ForMonth from MonthMaster where MonthName='" + smonth + "' and SessionName='" + Session["SessionName"].ToString() + "'";
                                int noofmonth = int.TryParse(oo.ReturnTag(sql, "ForMonth"), out noofmonth) ? noofmonth : 0;
                                if (noofmonth > 0)
                                {
                                    sum = sum + (Convert.ToDouble(WayAmount) * noofmonth);
                                }
                                else
                                {
                                    sum = sum + (Convert.ToDouble(WayAmount));
                                }
                            }
                        }

                    }

                }
            }

            if (sum != 0)
            {
                sum = sum + CalculateFine(srno, smonth, Session["SessionName"].ToString());
            }
            //}//--
            return sum;

        }
        public double undepositarrieramount(string lblSrno, string installment)
        {
            if (lblSrno== "PP18/13")
            {

            }
            double ArreirAmount = 0;

            sql = "select count(*) cnt from ArrierMast where SessionName = '" + Session["SessionName"].ToString() + "' and Srno = '" + lblSrno + "'";
            if (oo.ReturnTag(sql, "cnt") != "0")
            {
                sql = "select count(*) cnt2 from FeeReceiptWithInstallment where SessionName = '" + Session["SessionName"].ToString() + "' and Srno = '" + lblSrno + "' and InstallmentName='Arrear' and IsCancel = 0";
                if (oo.ReturnTag(sql, "cnt2") != "0")
                {
                    sql = "select top (1) PayableAmount from FeeReceiptWithInstallment where SessionName='" + Session["SessionName"].ToString() + "' and Srno='" + lblSrno + "'";
                    sql = sql + "and InstallmentName = 'Arrear' and IsCancel = 0 order by id asc";
                    if (!string.IsNullOrEmpty(oo.ReturnTag(sql, "PayableAmount")))
                    {
                        ArreirAmount = ArreirAmount + Convert.ToDouble(oo.ReturnTag(sql, "PayableAmount"));
                    }
                    sql = "select sum(PaidAmount) PaidAmount from FeeReceiptWithInstallment where SessionName='" + Session["SessionName"].ToString() + "' and Srno='" + lblSrno + "' and InstallmentName='Arrear'";
                    sql = sql + "and InstallmentName = 'Arrear' and IsCancel = 0";
                    if (!string.IsNullOrEmpty(oo.ReturnTag(sql, "PaidAmount")))
                    {
                        ArreirAmount = ArreirAmount - Convert.ToDouble(oo.ReturnTag(sql, "PaidAmount"));
                    }
                    if (ArreirAmount < 0)
                    {
                        ArreirAmount = ArreirAmount * (-1);
                    }
                }
                else
                {
                    sql = "select ArrearAmt from ArrierMast where SessionName = '" + Session["SessionName"].ToString() + "' and Srno = '" + lblSrno + "'";
                    if (!string.IsNullOrEmpty(oo.ReturnTag(sql, "ArrearAmt")))
                    {
                        ArreirAmount = Convert.ToDouble(oo.ReturnTag(sql, "ArrearAmt"));
                    }
                    if (ArreirAmount < 0)
                    {
                        ArreirAmount = ArreirAmount * (-1);
                    }

                    sql = "Select Concession from Concession_Permission where SrNo='" + lblSrno + "' and TableId=2 and Reset<>'YES'";
                    string arrcon = "";
                    arrcon = oo.ReturnTag(sql, "Concession");

                    double arrearcon = 0;

                    double.TryParse(arrcon, out arrearcon);

                    if (ArreirAmount == arrearcon)
                    {
                        ArreirAmount = 0;
                    }
                    else
                    {
                        List<SqlParameter> param = new List<SqlParameter>();

                        param.Add(new SqlParameter("@registervalue", lblSrno));
                        param.Add(new SqlParameter("@installment", installment));
                        param.Add(new SqlParameter("@sessionName", Session["SessionName"].ToString()));

                        DataSet ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("USP_GetFineforArrear", param);

                        if (ds != null)
                        {
                            string latefine = "0";
                            //latefine = ds.Tables[0].Rows[0][1].ToString();

                            double value1;

                            double.TryParse(latefine, out value1);

                            ArreirAmount = ArreirAmount + value1;
                        }
                    }

                }
            }

            return ArreirAmount;
        }

        //public double CalculateFine(string month, string lblSrno,string istransportrequired)
        //{
        //    sql = "Select FineType from FineSetting where SessionName='" + Session["SessionName"].ToString() + "'";
        //    if (oo.ReturnTag(sql, "FineType").ToString() == "Range Basis")
        //    {
        //        sql = "Select Count(*) as Count from SessionMaster where SessionName='" + Session["SessionName"].ToString() + "' and ToDate< Convert(nvarchar(11),GETDATE(),106)";
        //        int count = 0;
        //        count = Convert.ToInt16(oo.ReturnTag(sql, "Count"));
        //        if (count <= 0)
        //        {
        //            return CalculateInSameSession(month, lblSrno, istransportrequired);
        //        }
        //        else
        //        {
        //            return CalculateInnextSession(month, lblSrno);
        //        }
        //    }
        //    else
        //    {
        //        return 0;
        //    }
        //}

        public double CalculateFine(string srno, string installment, string sessionName)
        {
            //sql = "Select FineType from FineSetting where SessionName='" + Session["SessionName"].ToString() + "'";
            //if (oo.ReturnTag(sql, "FineType").ToString() == "Range Basis")
            //{
            //    sql = "Select Count(*) as Count from SessionMaster where SessionName='" + Session["SessionName"].ToString() + "' and ToDate< Convert(nvarchar(11),GETDATE(),106)";
            //    int count = 0;
            //    count = Convert.ToInt16(oo.ReturnTag(sql, "Count"));
            //    if (count <= 0)
            //    {
            //        return CalculateInSameSession(month, lblSrno);
            //    }
            //    else
            //    {
            //        return CalculateInnextSession(month, lblSrno);
            //    }
            //}
            //else if (oo.ReturnTag(sql, "FineType").ToString() == "Completion Month")
            //{
            //    sql = "Select AmountPerDay from RangeBasisFineMaster Where SessionName=" + Session["SessionName"].ToString() + " and DatePart(dd, GETDATE()) Between FromDate and ToDate";
            //    double amount = 0;
            //    double.TryParse(BAL.objBal.ReturnTag(sql, "AmountPerDay"),out amount);
            //    return amount;
            //}
            //else
            //{
            //    return 0;
            //}
            double fineamount=0;
            if (isFineExempted(srno, installment) == false)
            {
                DataSet dsforfine = getFine(srno, installment, sessionName);
                if (dsforfine!=null)
                {
                    double.TryParse(dsforfine.Tables[0].Rows[0][1].ToString(), out fineamount);
                }
                
            }
            return fineamount;
        }

        public bool isFineExempted(string srno, string installment)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
            param.Add(new SqlParameter("@Srno", srno));
            param.Add(new SqlParameter("@Installment", installment));

            return Convert.ToBoolean(DLL.objDll.Sp_SelectRecord_usingExecuteScalar("USP_IsFineExempted", param));
        }
        protected DataSet getFine(string registervalue, string installment, string sessionName)
        {
            List<SqlParameter> param = new List<SqlParameter>();

            param.Add(new SqlParameter("@registervalue", registervalue));
            param.Add(new SqlParameter("@installment", installment));
            param.Add(new SqlParameter("@sessionName", sessionName));


            DataSet ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("USP_GetFine", param);

            return ds;
        }

        public double CalculateInSameSession(string month, string lblSrno,string istransportrequired)
        {
            int a = 0, b = 0;
            double fineValue = 0;
            double totalAmount = 0;

            if(istransportrequired=="Yes")
            {
                sql = "Select *from FeeDeposite fd where fd.Cancel is null and fd.SessionName='" + Session["SessionName"].ToString() + "' and SrNo='" + lblSrno + "' and (FeeMonth='" + drpMonth.SelectedItem.ToString() + "' or FeeMonth='Yearly' or FeeMonth='(T) " + drpMonth.SelectedItem.ToString() + "') and BusConvience>'0.00' ";         
            }
            else
            {
                sql = "Select *from FeeDeposite fd where fd.Cancel is null and fd.SessionName='" + Session["SessionName"].ToString() + "' and SrNo='" + lblSrno + "' and (FeeMonth='" + drpMonth.SelectedItem.ToString() + "' or FeeMonth='Yearly')";
            }


            if (oo.Duplicate(sql) == false)
            { 
                sql = "select DAY(getdate()) as DayValue";
                int dv = 0;
                dv = Convert.ToInt32(oo.ReturnTag(sql, "DayValue"));

                sql = "Select DATENAME(MONTH,GETDATE()) as MonthName";
                string selectedmonth1 = oo.ReturnTag(sql, "MonthName");

                sql = "Select MonthId from MonthMaster where ISNULL(dueMonth,DAteName(MOnth,DueDate))='" + selectedmonth1 + "' and  (ClassId='" + drpClass.SelectedValue.ToString() + "' or ClassId is null)  and (CardType='" + drpCard.SelectedValue.ToString() + "' or CardType='" + drpCard.SelectedItem.Text.ToString() + "')  and SessionName='" + Session["SessionName"].ToString() + "'";
                int id3 = 0;
                try
                {
                    id3 = Convert.ToInt32(oo.ReturnTag(sql, "MonthId"));
                }
                catch
                {
                    // ignored
                }
                string selectedmonth;

                selectedmonth = month;

                sql = "Select MonthId from MonthMaster where ISNULL(dueMonth,DAteName(MOnth,DueDate))=(Select ISNULL(dueMonth,DAteName(MOnth,DueDate)) from MonthMaster where MonthName='" + month + "' and  (ClassId='" + drpClass.SelectedValue.ToString() + "' or ClassId is null)  and (CardType='" + drpCard.SelectedValue.ToString() + "' or CardType='" + drpCard.SelectedItem.Text.ToString() + "') and SessionName='" + Session["SessionName"].ToString() + "') and  (ClassId='" + drpClass.SelectedValue.ToString() + "' or ClassId is null)  and (CardType='" + drpCard.SelectedValue.ToString() + "' or CardType='" + drpCard.SelectedItem.Text.ToString() + "') and SessionName='" + Session["SessionName"].ToString() + "'";
                int id2 = 0;
                try
                {
                    id2 = Convert.ToInt32(oo.ReturnTag(sql, "MonthId"));
                }
                catch
                {
                    // ignored
                }

                SqlDataAdapter da;
                totalAmount = 0;

                sql = "Select *from MonthMaster where MonthId>='" + id2 + "' and MonthId<='" + id3 + "' and  (ClassId='" + drpClass.SelectedValue.ToString() + "' or ClassId is null)  and (CardType='" + drpCard.SelectedValue.ToString() + "' or CardType='" + drpCard.SelectedItem.Text.ToString() + "') and SessionName='" + Session["SessionName"].ToString() + "'";
                da = new SqlDataAdapter(sql, con);
                DataTable dt2 = new DataTable();
                da.Fill(dt2);

                sql = "Select Distinct MonthAmount,AmountType from RangeBasisFineMaster where SessionName='" + Session["SessionName"].ToString() + "'";
                double everymonthfine = Convert.ToDouble(oo.ReturnTag(sql, "MonthAmount"));
                for (int j2 = 0; j2 < dt2.Rows.Count; j2++)
                {
                    if (oo.ReturnTag(sql, "AmountType") == "Completion Month")
                    {                   
                        if (id2 == id3)
                        {
                            sql = "select * from RangeBasisFineMaster ";
                            sql = sql + " where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
                            SqlCommand cmd = new SqlCommand();
                            try
                            {
                                cmd.CommandText = sql;
                                SqlDataReader dr;
                                cmd.Connection = con;
                                con.Open();
                                dr = cmd.ExecuteReader();

                                fineValue = 0;
                                while (dr.Read())
                                {
                                    a = Convert.ToInt32(dr["FromDate"].ToString());
                                    b = Convert.ToInt32(dr["ToDate"].ToString());
                                    if (a <= dv && dv <= b)
                                    {
                                        fineValue = Convert.ToDouble(dr["AmountPerday"].ToString());
                                        break;
                                    }

                                }
                                con.Close();
                            }
                            catch (SqlException)
                            {
                                con.Close();
                            }

                            totalAmount = totalAmount + fineValue;
                        }
                    }
                    else
                    {
                        double monthlyfine = 0;
                        if (dt2.Rows.Count - 1 == j2)
                        {
                            monthlyfine = 0;
                        }
                        else
                        { monthlyfine = everymonthfine; }
                    

                        if (id2 <= id3)
                        {
                            sql = "select * from RangeBasisFineMaster ";
                            sql = sql + " where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
                            SqlCommand cmd = new SqlCommand();
                            try
                            {
                                cmd.CommandText = sql;
                                SqlDataReader dr;
                                cmd.Connection = con;
                                con.Open();
                                dr = cmd.ExecuteReader();

                                fineValue = 0;
                                while (dr.Read())
                                {
                                    a = Convert.ToInt32(dr["FromDate"].ToString());
                                    b = Convert.ToInt32(dr["ToDate"].ToString());
                                    if (a <= dv && dv <= b)
                                    {
                                        fineValue = Convert.ToDouble(dr["AmountPerday"].ToString());
                                        break;
                                    }

                                }
                                con.Close();
                            }
                            catch (SqlException)
                            {
                                con.Close();
                            }
                        }



                        totalAmount = totalAmount + fineValue + monthlyfine;
                    }

                }
            
            }
            return totalAmount;
        }

        public double CalculateInSameSession(string month, string lblSrno)
        {
            int a = 0, b = 0;
            double fineValue = 0;
            double totalAmount = 0;

            sql = "Select *from FeeDeposite fd where fd.Cancel is null and fd.SessionName='" + Session["SessionName"].ToString() + "' and SrNo='" + lblSrno + "' and (FeeMonth='" + drpMonth.SelectedItem.ToString() + "' and FeeMonth='Yearly')";

            if (oo.Duplicate(sql) == false)
            {
                sql = "select DAY(getdate()) as DayValue";
                int dv = 0;
                dv = Convert.ToInt32(oo.ReturnTag(sql, "DayValue"));

                sql = "Select DATENAME(MONTH,GETDATE()) as MonthName";
                string selectedmonth1 = oo.ReturnTag(sql, "MonthName");

                sql = "Select MonthId from MonthMaster where ISNULL(dueMonth,DAteName(MOnth,DueDate))='" + selectedmonth1 + "' and  (ClassId='" + drpClass.SelectedValue.ToString() + "' or ClassId is null)  and (CardType='" + drpCard.SelectedValue.ToString() + "' or CardType='" + drpCard.SelectedItem.Text.ToString() + "')  and SessionName='" + Session["SessionName"].ToString() + "'";
                int id3 = 0;
                try
                {
                    id3 = Convert.ToInt32(oo.ReturnTag(sql, "MonthId"));
                }
                catch
                {
                    // ignored
                }
                string selectedmonth;

                selectedmonth = month;

                sql = "Select MonthId from MonthMaster where ISNULL(dueMonth,DAteName(MOnth,DueDate))=(Select ISNULL(dueMonth,DAteName(MOnth,DueDate)) from MonthMaster where MonthName='" + month + "' and  (ClassId='" + drpClass.SelectedValue.ToString() + "' or ClassId is null)  and (CardType='" + drpCard.SelectedValue.ToString() + "' or CardType='" + drpCard.SelectedItem.Text.ToString() + "') and SessionName='" + Session["SessionName"].ToString() + "') and  (ClassId='" + drpClass.SelectedValue.ToString() + "' or ClassId is null)  and (CardType='" + drpCard.SelectedValue.ToString() + "' or CardType='" + drpCard.SelectedItem.Text.ToString() + "') and SessionName='" + Session["SessionName"].ToString() + "'";
                int id2 = 0;
                try
                {
                    id2 = Convert.ToInt32(oo.ReturnTag(sql, "MonthId"));
                }
                catch
                {
                    // ignored
                }

                SqlDataAdapter da;
                totalAmount = 0;

                sql = "Select *from MonthMaster where MonthId>='" + id2 + "' and MonthId<='" + id3 + "' and  (ClassId='" + drpClass.SelectedValue.ToString() + "' or ClassId is null)  and (CardType='" + drpCard.SelectedValue.ToString() + "' or CardType='" + drpCard.SelectedItem.Text.ToString() + "') and SessionName='" + Session["SessionName"].ToString() + "'";
                da = new SqlDataAdapter(sql, con);
                DataTable dt2 = new DataTable();
                da.Fill(dt2);
                for (int j2 = 0; j2 < dt2.Rows.Count; j2++)
                {
                    sql = "Select Distinct MonthAmount,AmountType from RangeBasisFineMaster where SessionName='" + Session["SessionName"].ToString() + "'";
                    double fineamount = Convert.ToDouble(oo.ReturnTag(sql, "MonthAmount"));
                    if (oo.ReturnTag(sql, "AmountType") == "Completion Month")
                    {
                        if (id2 == id3)
                        {
                            sql = "select * from RangeBasisFineMaster ";
                            sql = sql + " where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
                            SqlCommand cmd = new SqlCommand();
                            try
                            {
                                cmd.CommandText = sql;
                                SqlDataReader dr;
                                cmd.Connection = con;
                                con.Open();
                                dr = cmd.ExecuteReader();

                                fineValue = 0;
                                while (dr.Read())
                                {
                                    a = Convert.ToInt32(dr["FromDate"].ToString());
                                    b = Convert.ToInt32(dr["ToDate"].ToString());
                                    if (a <= dv && dv <= b)
                                    {
                                        fineValue = Convert.ToDouble(dr["AmountPerday"].ToString());
                                        break;
                                    }

                                }
                                con.Close();
                            }
                            catch (SqlException )
                            {
                                con.Close();
                            }

                            totalAmount = totalAmount + fineValue;
                        }
                        else
                        {
                            totalAmount = fineamount;
                        }


                    }
                    else
                    {
                        //totalAmount = totalAmount + fineamount;

                        if (id2 <= id3)
                        {
                            sql = "select * from RangeBasisFineMaster ";
                            sql = sql + " where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
                            SqlCommand cmd = new SqlCommand();
                            try
                            {
                                cmd.CommandText = sql;
                                SqlDataReader dr;
                                cmd.Connection = con;
                                con.Open();
                                dr = cmd.ExecuteReader();

                                fineValue = 0;
                                while (dr.Read())
                                {
                                    a = Convert.ToInt32(dr["FromDate"].ToString());
                                    b = Convert.ToInt32(dr["ToDate"].ToString());
                                    if (a <= dv && dv <= b)
                                    {
                                        fineValue = Convert.ToDouble(dr["AmountPerday"].ToString());
                                        break;
                                    }

                                }
                                con.Close();
                            }
                            catch (SqlException)
                            {
                                con.Close();
                            }
                        }

                        totalAmount = totalAmount + fineValue;
                    }

                }

            }
            return totalAmount;
        }
        public double CalculateInnextSession(string month, string lblSrno)
        {
            double fineValue = 0;
            double totalAmount = 0;
            sql = "Select *from FeeDeposite fd where fd.Cancel is null and fd.SessionName='" + Session["SessionName"].ToString() + "' and SrNo='" + lblSrno + "' and (FeeMonth='" + drpMonth.SelectedItem.ToString() + "'  or FeeMonth='Yearly')";
            if (oo.Duplicate(sql) == false)
            {
                sql = "select DAY(getdate()) as DayValue";
                int dv = 0;
                dv = Convert.ToInt32(oo.ReturnTag(sql, "DayValue"));
                sql = "Select AdmissionForClassId,Card from StudentOfficialDetails where SrNo='" + lblSrno + "' and SessionName='" + Session["SessionName"].ToString() + "'";
                string classid = oo.ReturnTag(sql, "AdmissionForClassId");
                string card = oo.ReturnTag(sql, "card");


                //sql = "Select Top 1 ROW_NUMBER() Over (Order by MonthId desc) as Id,DueMonth from MonthMaster where SessionName='" + Session["SessionName"].ToString() + "'";
                sql = "Select Top 1 ROW_NUMBER() Over (Order by MonthId desc) as Id,ISNULL(dueMonth,DAteName(MOnth,DueDate)) DueMonth from MonthMaster mm";
                sql = sql + " inner join FeeGroupMaster fgm on fgm.Id=mm.CardType";
                sql = sql + " and fgm.SessionName=mm.SessionName where (ClassId='" + classid + "' or ClassId is null)  and fgm.FeeGroupName='" + card + "' and mm.SessionName='" + Session["SessionName"].ToString() + "'";
                string selectedmonth1 = oo.ReturnTag(sql, "DueMonth");

                //sql = "Select MonthId from MonthMaster where DueMonth='" + selectedmonth1 + "' and SessionName='" + Session["SessionName"].ToString() + "'";



                sql = "Select MonthId from MonthMaster mm";
                sql = sql + " inner join FeeGroupMaster fgm on fgm.Id=mm.CardType";
                sql = sql + " and fgm.SessionName=mm.SessionName where ISNULL(dueMonth,DAteName(MOnth,DueDate))='" + selectedmonth1 + "' and ";
                sql = sql + " (ClassId='" + classid + "' or ClassId is null)  and fgm.FeeGroupName='" + card + "' and mm.SessionName='" + Session["SessionName"].ToString() + "'";
                int id3 = 0;
                try
                {
                    id3 = Convert.ToInt32(oo.ReturnTag(sql, "MonthId"));
                }
                catch
                {
                    // ignored
                }

                string selectedmonth = month;


                sql = "Select MonthId from MonthMaster mm";
                sql = sql + " inner join FeeGroupMaster fgm on fgm.Id=mm.CardType";
                sql = sql + " and fgm.SessionName=mm.SessionName where ISNULL(dueMonth,DAteName(MOnth,DueDate))='" + selectedmonth + "' and ";
                sql = sql + " (ClassId='" + classid + "' or ClassId is null)  and fgm.FeeGroupName='" + card + "' and mm.SessionName='" + Session["SessionName"].ToString() + "'";
                //sql = "Select MonthId from MonthMaster where DueMonth='" + selectedmonth + "' and SessionName='" + Session["SessionName"].ToString() + "'";
                int id2 = 0;
                try
                {
                    id2 = Convert.ToInt32(oo.ReturnTag(sql, "MonthId"));
                }
                catch
                {
                    // ignored
                }


                SqlDataAdapter da;
              

                //sql = "Select *from MonthMaster where MonthId>='" + id2 + "' and MonthId<='" + id3 + "' and SessionName='" + Session["SessionName"].ToString() + "'";

                sql = "Select Monthid,MonthName from MonthMaster mm";
                sql = sql + " inner join FeeGroupMaster fgm on fgm.Id=mm.CardType";
                sql = sql + " and fgm.SessionName=mm.SessionName where (ClassId='" + classid + "' or ClassId is null)  and fgm.FeeGroupName='" + card + "' and mm.SessionName='" + Session["SessionName"].ToString() + "'";
                sql = sql + " and MonthId>='" + id2 + "' and MonthId<='" + id3 + "'";

                da = new SqlDataAdapter(sql, con);
                DataTable dt2 = new DataTable();
                da.Fill(dt2);
               
                for (int j2 = 0; j2 < dt2.Rows.Count; j2++)
                {
                    sql = "Select Distinct MonthAmount,AmountType from RangeBasisFineMaster where SessionName='" + Session["SessionName"].ToString() + "'";
                    double fineamount = double.TryParse(oo.ReturnTag(sql, "MonthAmount"), out fineamount) ? fineamount : 0;
                    if (oo.ReturnTag(sql, "AmountType") == "Completion Month")
                    {
                        totalAmount = fineamount;
                    }
                    else
                    {
                        totalAmount = totalAmount + fineamount;
                    }
                }
                

                totalAmount = totalAmount + fineValue;

            }
            return totalAmount;
        }

        //public int CalculateInnextSessionMonth(string lblSrno)
        //{
        //    sql = "Select AdmissionForClassId,card from StudentOfficialDetails where SrNo='" + lblSrno + "' and SessionName='" + Session["SessionName"].ToString() + "'";

        //    sql = "select DATENAME(mm,getdate()) as Month";
        //    string month = oo.ReturnTag(sql, "Month");

        //    sql = "select MonthName,MonthId from MonthMaster where (ClassId='" + classid + "' or ClassId is null) and SessionName='" + Session["SessionName"].ToString() + "'and MonthId<=";
        //    sql = sql + " (select MonthId from MonthMaster mm inner join FeeGroupMaster fgm on fgm.id=mm.CardType and fgm.SessionName=mm.SessionName where (ClassId='" + classid + "' or ClassId is null) and ISNULL(DueMonth,DATENAME(mm,DueDate))='" + month + "' and mm.SessionName='" + Session["SessionName"].ToString() + "' and fgm.FeeGroupName='" + card + "')";
        //    DataSet ds = new DataSet();
        //    ds = oo.GridFill(sql);
        //    DataTable dt = new DataTable();
        //    dt = ds.Tables[0];
        //    return dt.Rows.Count;
        //}

        //public int CalculateInnextSessionMonth(string lblSrno, string monthname)
        //{
        //    sql = "Select AdmissionForClassId,card from StudentOfficialDetails where SrNo='" + lblSrno + "' and SessionName='" + Session["SessionName"].ToString() + "'";
        //    string classid = oo.ReturnTag(sql, "AdmissionForClassId");
        //    string card = oo.ReturnTag(sql, "card");

        //sql = "select DATENAME(mm,getdate()) as Month";
        //string month = oo.ReturnTag(sql, "Month");

        //sql = "select MonthName,MonthId from MonthMaster where (ClassId='" + classid + "' or ClassId is null) and SessionName='" + Session["SessionName"].ToString() + "'and MonthId>";
        //sql = sql + " (select MonthId from MonthMaster mm inner join FeeGroupMaster fgm on fgm.id=mm.CardType and fgm.SessionName=mm.SessionName where (ClassId='" + classid + "' or ClassId is null) and ISNULL(DueMonth,DATENAME(mm,DueDate))='" + month + "' and mm.SessionName='" + Session["SessionName"].ToString() + "' and fgm.FeeGroupName='" + card + "')";
        //DataSet ds = new DataSet();
        //ds = oo.GridFill(sql);
        //DataTable dt = new DataTable();
        //dt = ds.Tables[0];
        //return dt.Rows.Count;

        //    sql = "Select Top 1 SessionName from SessionMaster order by SessionId Desc";
        //    string nextsession = BAL.objBal.ReturnTag(sql, "SessionName");

        //    sql = " Select DATEDIFF(mm,(select DueDate from MonthMaster mm inner join FeeGroupMaster fgm on fgm.id=mm.CardType and fgm.SessionName=mm.SessionName";
        //    sql = sql + " where (ClassId='" + classid + "' or ClassId is null) and ISNULL(DueMonth,DATENAME(mm,DueDate))='" + monthname + "'";
        //    sql = sql + " and mm.SessionName='" + Session["SessionName"].ToString() + "' and fgm.FeeGroupName='" + card + "'),GETDATE())+(select Top 1 Case When GETDATE()>DueDate then 0 else -1 End";
        //    sql = sql + " from MonthMaster where ISNULL(DueMonth,DATENAME(mm,DueDate))=DateName(mm,GETDATE())";
        //    sql = sql + " and SessionName='" + nextsession + "') count";

        //    int count = Convert.ToInt16(BAL.objBal.ReturnTag(sql, "count"));

        //    if (count < 0)
        //    {
        //        count = 0;
        //    }

        //    return count;
        //}
        public void ToCheckYearly()
        {
            for (i = 0; i <= GridView1.Rows.Count - 1; i++)
            {
                Label Label1 = (Label)GridView1.Rows[i].FindControl("Label1");
                GridView GridView2 = (GridView)GridView1.Rows[i].FindControl("GridView2");
                for (j = 0; j <= GridView2.Rows.Count - 1; j++)
                {
                    Label lblSrno = (Label)GridView2.Rows[j].FindControl("lblSrno");
                    Label lblFeeMode = (Label)GridView2.Rows[j].FindControl("lblFeeMode");
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



                    if (lblFeeMode.Text == "Yearly")
                    {
                        lblFeeMode.ForeColor = System.Drawing.Color.Red;
                        lblApr.Text = "0.00";
                        lblMay.Text = "0.00";
                        lblJun.Text = "0.00";
                        lblJul.Text = "0.00";
                        lblAug.Text = "0.00";
                        lblSep.Text = "0.00";
                        lblOct.Text = "0.00";
                        lblNov.Text = "0.00";
                        lblDec.Text = "0.00";
                        lblJan.Text = "0.00";
                        lblFeb.Text = "0.00";
                        lblMar.Text = "0.00";
                        lblApr.ForeColor = System.Drawing.Color.Red;
                        lblMay.ForeColor = System.Drawing.Color.Red;
                        lblJun.ForeColor = System.Drawing.Color.Red;
                        lblJul.ForeColor = System.Drawing.Color.Red;
                        lblAug.ForeColor = System.Drawing.Color.Red;
                        lblSep.ForeColor = System.Drawing.Color.Red;
                        lblOct.ForeColor = System.Drawing.Color.Red;
                        lblNov.ForeColor = System.Drawing.Color.Red;
                        lblDec.ForeColor = System.Drawing.Color.Red;
                        lblJan.ForeColor = System.Drawing.Color.Red;
                        lblFeb.ForeColor = System.Drawing.Color.Red;
                        lblMar.ForeColor = System.Drawing.Color.Red;

                    }

                }
            }
        }
        public double FindConcession(string srno, string FeeMonth)
        {
            double concession = 0;
            string ss = "select * from FeeDeposite where Status='Paid' and cancle is null and srno='" + srno + "'  and FeeMonth='" + FeeMonth + "'";
            try
            {
                concession = Convert.ToDouble(oo.ReturnTag(ss, "cocession"));
            }
            catch (Exception) { concession = 0; }
            return concession;
        }
        protected void ImageButton4_Click(object sender, EventArgs e)
        {
            loadheader();
            PrintHelper_New.ctrl = divExport;
            FindTotalDuesofStudent();
            ClientScript.RegisterStartupScript(GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
        }
        protected void ImageButton3_Click(object sender, EventArgs e)
        {
            loadheader();
            FindTotalDuesofStudent();
            oo.ExporttolandscapePdf(Response, "EveryStudentBalanceList.pdf", divExport);
        }
        public void loadheader()
        {
            GridView GridView2 = (GridView)GridView1.Rows[0].FindControl("GridView2");
            //sql = "Select MonthName from MonthMaster Where SessionName='" + Session["SessionName"].ToString() + "' and CardType='" + drpCard.SelectedItem.ToString() + "'";
            sql = "select MonthName from MonthMaster mm";
            sql = sql + " inner join FeeGroupMaster fgm on fgm.Id=mm.CardType and fgm.SessionName=mm.SessionName";
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
                int k = 8;
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


                for (int l = k; l < GridView2.HeaderRow.Cells.Count - 1; l++)
                {
                    GridView2.Columns[l].Visible = false;
                }

            }
            con.Close();
        }
        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        public void ShowHideMonths()
        {
#pragma warning disable 219
            string xx = "";
#pragma warning restore 219
            for (i = 0; i <= GridView1.Rows.Count - 1; i++)
            {
                Label Label1 = (Label)GridView1.Rows[i].FindControl("Label1");
                GridView GridView2 = (GridView)GridView1.Rows[i].FindControl("GridView2");
                int grdcoloumn = GridView1.Columns.Count;
                int monthcount = drpMonth.Items.Count;
                int selectedmonth = 0;
                Label lblTotalChequeBounceFine = (Label)GridView2.FooterRow.FindControl("lblTotalChequeBounceFine");
                for (int l1 = 0; l1 < monthcount; l1++)
                {
                    if (drpMonth.SelectedIndex == l1)
                    {
                        selectedmonth = l1;
                    }
                }

                if (drpMonth.SelectedIndex == selectedmonth)
                {
                    int f1 = selectedmonth + 9;
                    if (lblTotalChequeBounceFine.Text == "0")
                    {
                        while (f1 < GridView2.Columns.Count - 1)
                        {
                            GridView2.Columns[f1].Visible = false;
                            f1 = f1 + 1;
                        }
                    }
                    else
                    {
                        while (f1 < GridView2.Columns.Count - 2)
                        {
                            GridView2.Columns[f1].Visible = false;
                            f1 = f1 + 1;
                        }
                    }
                }

            }

        }
        public void FindTotalDuesofStudent()
        {
            try
            {
                double sum = 0;
                double april = 0, may = 0, june = 0, july = 0, aug = 0, sep = 0, oct = 0, nov = 0, dec = 0, jan = 0, feb = 0, mar = 0;
                double colsum = 0;
                double chequeFine = 0;
                for (i = 0; i <= GridView1.Rows.Count - 1; i++)
                {
                    Label Label1 = (Label)GridView1.Rows[i].FindControl("Label1");
                    GridView GridView2 = (GridView)GridView1.Rows[i].FindControl("GridView2");
                    colsum = 0;
                    chequeFine = 0;
                    for (j = 0; j <= GridView2.Rows.Count - 1; j++)
                    {
                        sum = 0;
                        Label lblTotBalance = (Label)GridView2.Rows[j].FindControl("lblTotBalance");
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
                        Label lblChequeBounceFine = (Label)GridView2.Rows[j].FindControl("lblChequeBounceFine");

                        if (drpMonth.SelectedItem.ToString() == GridView2.HeaderRow.Cells[8].Text)
                        {

                            try
                            {
                                sum = sum + Convert.ToDouble(lblApr.Text);
                                april = april + Convert.ToDouble(lblApr.Text);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }
                        }
                        if (drpMonth.SelectedItem.ToString() == GridView2.HeaderRow.Cells[9].Text)
                        {
                            try
                            {
                                sum = sum + Convert.ToDouble(lblApr.Text);
                                april = april + Convert.ToDouble(lblApr.Text);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }


                            try
                            {
                                sum = sum + Convert.ToDouble(lblMay.Text);
                                may = may + Convert.ToDouble(lblMay.Text);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }




                        }
                        if (drpMonth.SelectedItem.ToString() == GridView2.HeaderRow.Cells[10].Text)
                        {
                            try
                            {
                                sum = sum + Convert.ToDouble(lblApr.Text);
                                april = april + Convert.ToDouble(lblApr.Text);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }


                            try
                            {
                                sum = sum + Convert.ToDouble(lblMay.Text);
                                may = may + Convert.ToDouble(lblMay.Text);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }


                            try
                            {
                                sum = sum + Convert.ToDouble(lblJun.Text);
                                june = june + Convert.ToDouble(lblJun.Text);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }


                        }
                        if (drpMonth.SelectedItem.ToString() == GridView2.HeaderRow.Cells[11].Text)
                        {
                            try
                            {
                                sum = sum + Convert.ToDouble(lblApr.Text);
                                april = april + Convert.ToDouble(lblApr.Text);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }


                            try
                            {
                                sum = sum + Convert.ToDouble(lblMay.Text);
                                may = may + Convert.ToDouble(lblMay.Text);
                            }
                            catch (Exception) { }


                            try
                            {
                                sum = sum + Convert.ToDouble(lblJun.Text);
                                june = june + Convert.ToDouble(lblJun.Text);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }


                            try
                            {
                                sum = sum + Convert.ToDouble(lblJul.Text);
                                july = july + Convert.ToDouble(lblJul.Text);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }



                        }
                        if (drpMonth.SelectedItem.ToString() == GridView2.HeaderRow.Cells[12].Text)
                        {
                            try
                            {
                                sum = sum + Convert.ToDouble(lblApr.Text);
                                april = april + Convert.ToDouble(lblApr.Text);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }


                            try
                            {
                                sum = sum + Convert.ToDouble(lblMay.Text);
                                may = may + Convert.ToDouble(lblMay.Text);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }


                            try
                            {
                                sum = sum + Convert.ToDouble(lblJun.Text);
                                june = june + Convert.ToDouble(lblJun.Text);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }

                            try
                            {
                                sum = sum + Convert.ToDouble(lblJul.Text);
                                july = july + Convert.ToDouble(lblJul.Text);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }

                            try
                            {
                                sum = sum + Convert.ToDouble(lblAug.Text);
                                aug = aug + Convert.ToDouble(lblAug.Text);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }

                        }
                        if (drpMonth.SelectedItem.ToString() == GridView2.HeaderRow.Cells[13].Text)
                        {
                            try
                            {
                                sum = sum + Convert.ToDouble(lblApr.Text);
                                april = april + Convert.ToDouble(lblApr.Text);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }
                            try
                            {
                                sum = sum + Convert.ToDouble(lblMay.Text);
                                may = may + Convert.ToDouble(lblMay.Text);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }


                            try
                            {
                                sum = sum + Convert.ToDouble(lblJun.Text);
                                june = june + Convert.ToDouble(lblJun.Text);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }


                            try
                            {
                                sum = sum + Convert.ToDouble(lblJul.Text);
                                july = july + Convert.ToDouble(lblJul.Text);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }

                            try
                            {
                                sum = sum + Convert.ToDouble(lblAug.Text);
                                aug = aug + Convert.ToDouble(lblAug.Text);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }

                            try
                            {
                                sum = sum + Convert.ToDouble(lblSep.Text);
                                sep = sep + Convert.ToDouble(lblSep.Text);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }



                        }

                        if (drpMonth.SelectedItem.ToString() == GridView2.HeaderRow.Cells[14].Text)
                        {
                            try
                            {
                                sum = sum + Convert.ToDouble(lblApr.Text);
                                april = april + Convert.ToDouble(lblApr.Text);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }


                            try
                            {
                                sum = sum + Convert.ToDouble(lblMay.Text);
                                may = may + Convert.ToDouble(lblMay.Text);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }

                            try
                            {
                                sum = sum + Convert.ToDouble(lblJun.Text);
                                june = june + Convert.ToDouble(lblJun.Text);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }

                            try
                            {
                                sum = sum + Convert.ToDouble(lblJul.Text);
                                july = july + Convert.ToDouble(lblJul.Text);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }
                            try
                            {
                                sum = sum + Convert.ToDouble(lblAug.Text);
                                aug = aug + Convert.ToDouble(lblAug.Text);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }
                            try
                            {
                                sum = sum + Convert.ToDouble(lblSep.Text);
                                sep = sep + Convert.ToDouble(lblSep.Text);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }

                            try
                            {
                                sum = sum + Convert.ToDouble(lblOct.Text);
                                oct = oct + Convert.ToDouble(lblOct.Text);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }

                        }
                        if (drpMonth.SelectedItem.ToString() == GridView2.HeaderRow.Cells[15].Text)
                        {
                            try
                            {
                                sum = sum + Convert.ToDouble(lblApr.Text);
                                april = april + Convert.ToDouble(lblApr.Text);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }

                            try
                            {
                                sum = sum + Convert.ToDouble(lblMay.Text);
                                may = may + Convert.ToDouble(lblMay.Text);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }

                            try
                            {
                                sum = sum + Convert.ToDouble(lblJun.Text);
                                june = june + Convert.ToDouble(lblJun.Text);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }

                            try
                            {
                                sum = sum + Convert.ToDouble(lblJul.Text);
                                july = july + Convert.ToDouble(lblJul.Text);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }
                            try
                            {
                                sum = sum + Convert.ToDouble(lblAug.Text);
                                aug = aug + Convert.ToDouble(lblAug.Text);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }

                            try
                            {
                                sum = sum + Convert.ToDouble(lblSep.Text);
                                sep = sep + Convert.ToDouble(lblSep.Text);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }

                            try
                            {
                                sum = sum + Convert.ToDouble(lblOct.Text);
                                oct = oct + Convert.ToDouble(lblOct.Text);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }

                            try
                            {
                                sum = sum + Convert.ToDouble(lblNov.Text);
                                nov = nov + Convert.ToDouble(lblNov.Text);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }

                        }
                        if (drpMonth.SelectedItem.ToString() == GridView2.HeaderRow.Cells[16].Text)
                        {
                            try
                            {
                                sum = sum + Convert.ToDouble(lblApr.Text);
                                april = april + Convert.ToDouble(lblApr.Text);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }


                            try
                            {
                                sum = sum + Convert.ToDouble(lblMay.Text);
                                may = may + Convert.ToDouble(lblMay.Text);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }

                            try
                            {
                                sum = sum + Convert.ToDouble(lblJun.Text);
                                june = june + Convert.ToDouble(lblJun.Text);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }


                            try
                            {
                                sum = sum + Convert.ToDouble(lblJul.Text);
                                july = july + Convert.ToDouble(lblJul.Text);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }
                            try
                            {
                                sum = sum + Convert.ToDouble(lblAug.Text);
                                aug = aug + Convert.ToDouble(lblAug.Text);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }
                            try
                            {
                                sum = sum + Convert.ToDouble(lblSep.Text);
                                sep = sep + Convert.ToDouble(lblSep.Text);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }

                            try
                            {
                                sum = sum + Convert.ToDouble(lblOct.Text);
                                oct = oct + Convert.ToDouble(lblOct.Text);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }

                            try
                            {
                                sum = sum + Convert.ToDouble(lblNov.Text);
                                nov = nov + Convert.ToDouble(lblNov.Text);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }

                            try
                            {
                                sum = sum + Convert.ToDouble(lblDec.Text);
                                dec = dec + Convert.ToDouble(lblDec.Text);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }
                        }
                        if (drpMonth.SelectedItem.ToString() == GridView2.HeaderRow.Cells[17].Text)
                        {
                            try
                            {
                                sum = sum + Convert.ToDouble(lblApr.Text);
                                april = april + Convert.ToDouble(lblApr.Text);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }
                            try
                            {
                                sum = sum + Convert.ToDouble(lblMay.Text);
                                may = may + Convert.ToDouble(lblMay.Text);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }

                            try
                            {
                                sum = sum + Convert.ToDouble(lblJun.Text);
                                june = june + Convert.ToDouble(lblJun.Text);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }


                            try
                            {
                                sum = sum + Convert.ToDouble(lblJul.Text);
                                july = july + Convert.ToDouble(lblJul.Text);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }

                            try
                            {
                                sum = sum + Convert.ToDouble(lblAug.Text);
                                aug = aug + Convert.ToDouble(lblAug.Text);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }
                            try
                            {
                                sum = sum + Convert.ToDouble(lblSep.Text);
                                sep = sep + Convert.ToDouble(lblSep.Text);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }
                            try
                            {
                                sum = sum + Convert.ToDouble(lblOct.Text);
                                oct = oct + Convert.ToDouble(lblOct.Text);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }

                            try
                            {
                                sum = sum + Convert.ToDouble(lblNov.Text);
                                nov = nov + Convert.ToDouble(lblNov.Text);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }
                            try
                            {
                                sum = sum + Convert.ToDouble(lblDec.Text);
                                dec = dec + Convert.ToDouble(lblDec.Text);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }
                            try
                            {
                                sum = sum + Convert.ToDouble(lblJan.Text);
                                jan = jan + Convert.ToDouble(lblJan.Text);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }

                        }
                        if (drpMonth.SelectedItem.ToString() == GridView2.HeaderRow.Cells[18].Text)
                        {
                            try
                            {
                                sum = sum + Convert.ToDouble(lblApr.Text);
                                april = april + Convert.ToDouble(lblApr.Text);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }
                            try
                            {
                                sum = sum + Convert.ToDouble(lblMay.Text);
                                may = may + Convert.ToDouble(lblMay.Text);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }
                            try
                            {
                                sum = sum + Convert.ToDouble(lblJun.Text);
                                june = june + Convert.ToDouble(lblJun.Text);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }
                            try
                            {
                                sum = sum + Convert.ToDouble(lblJul.Text);
                                july = july + Convert.ToDouble(lblJul.Text);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }

                            try
                            {
                                sum = sum + Convert.ToDouble(lblAug.Text);
                                aug = aug + Convert.ToDouble(lblAug.Text);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }

                            try
                            {
                                sum = sum + Convert.ToDouble(lblSep.Text);
                                sep = sep + Convert.ToDouble(lblSep.Text);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }

                            try
                            {
                                sum = sum + Convert.ToDouble(lblOct.Text);
                                oct = oct + Convert.ToDouble(lblOct.Text);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }
                            try
                            {
                                sum = sum + Convert.ToDouble(lblNov.Text);
                                nov = nov + Convert.ToDouble(lblNov.Text);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }
                            try
                            {
                                sum = sum + Convert.ToDouble(lblDec.Text);
                                dec = dec + Convert.ToDouble(lblDec.Text);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }
                            try
                            {
                                sum = sum + Convert.ToDouble(lblJan.Text);
                                jan = jan + Convert.ToDouble(lblJan.Text);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }
                            try
                            {
                                sum = sum + Convert.ToDouble(lblFeb.Text);
                                feb = feb + Convert.ToDouble(lblFeb.Text);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }

                        }

                        if (drpMonth.SelectedItem.ToString() == GridView2.HeaderRow.Cells[19].Text)
                        {
                            try
                            {
                                sum = sum + Convert.ToDouble(lblApr.Text);
                                april = april + Convert.ToDouble(lblApr.Text);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }
                            try
                            {
                                sum = sum + Convert.ToDouble(lblMay.Text);
                                may = may + Convert.ToDouble(lblMay.Text);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }

                            try
                            {
                                sum = sum + Convert.ToDouble(lblJun.Text);
                                june = june + Convert.ToDouble(lblJun.Text);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }

                            try
                            {
                                sum = sum + Convert.ToDouble(lblJul.Text);
                                july = july + Convert.ToDouble(lblJul.Text);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }
                            try
                            {
                                sum = sum + Convert.ToDouble(lblAug.Text);
                                aug = aug + Convert.ToDouble(lblAug.Text);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }
                            try
                            {
                                sum = sum + Convert.ToDouble(lblSep.Text);
                                sep = sep + Convert.ToDouble(lblSep.Text);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }
                            try
                            {
                                sum = sum + Convert.ToDouble(lblOct.Text);
                                oct = oct + Convert.ToDouble(lblOct.Text);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }
                            try
                            {
                                sum = sum + Convert.ToDouble(lblNov.Text);
                                nov = nov + Convert.ToDouble(lblNov.Text);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }
                            try
                            {
                                sum = sum + Convert.ToDouble(lblDec.Text);
                                dec = dec + Convert.ToDouble(lblDec.Text);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }
                            try
                            {
                                sum = sum + Convert.ToDouble(lblJan.Text);
                                jan = jan + Convert.ToDouble(lblJan.Text);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }

                            try
                            {
                                sum = sum + Convert.ToDouble(lblFeb.Text);
                                feb = feb + Convert.ToDouble(lblFeb.Text);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }
                            try
                            {
                                sum = sum + Convert.ToDouble(lblMar.Text);
                                mar = mar + Convert.ToDouble(lblMar.Text);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }


                        }
                                                                 
                        Label lblSno = (Label)GridView2.Rows[j].FindControl("lblSno");
                        if (sum == 0)
                        {
                            colsum = colsum + sum;
                            lblTotBalance.Text = sum.ToString(CultureInfo.InvariantCulture);
                            if (drpFilter.SelectedIndex == 1)
                            {
                                GridView2.Rows[j].Visible = true;
                            }
                            else
                            {
                                GridView2.Rows[j].Visible = false;
                            }
                            lblSno.Text = (srno + 1).ToString();
                            srno++;
                        }
                        else
                        {
                            chequeFine = chequeFine + Convert.ToDouble(lblChequeBounceFine.Text);
                            colsum = colsum + sum + Convert.ToDouble(lblChequeBounceFine.Text);
                            lblTotBalance.Text = (sum + Convert.ToDouble(lblChequeBounceFine.Text)).ToString(CultureInfo.InvariantCulture);
                            lblSno.Text = (srno + 1).ToString();
                            srno++;
                           
                        }
                    }

                    Label lblTotMonthDues = (Label)GridView2.FooterRow.FindControl("lblTotMonthDues");
                    lblTotMonthDues.Text = colsum.ToString(CultureInfo.InvariantCulture);

                    Label lblAprsum = (Label)GridView2.FooterRow.FindControl("lblAprsum");
                    lblAprsum.Text = april.ToString(CultureInfo.InvariantCulture);

                    Label lblMaysum = (Label)GridView2.FooterRow.FindControl("lblMaysum");
                    lblMaysum.Text = may.ToString(CultureInfo.InvariantCulture);

                    Label lblJunsum = (Label)GridView2.FooterRow.FindControl("lblJunsum");
                    lblJunsum.Text = june.ToString(CultureInfo.InvariantCulture);

                    Label lblJulsum = (Label)GridView2.FooterRow.FindControl("lblJulsum");
                    lblJulsum.Text = july.ToString(CultureInfo.InvariantCulture);

                    Label lblAugsum = (Label)GridView2.FooterRow.FindControl("lblAugsum");
                    lblAugsum.Text = aug.ToString(CultureInfo.InvariantCulture);

                    Label lblSepsum = (Label)GridView2.FooterRow.FindControl("lblSepsum");
                    lblSepsum.Text = sep.ToString(CultureInfo.InvariantCulture);

                    Label lblOctsum = (Label)GridView2.FooterRow.FindControl("lblOctsum");
                    lblOctsum.Text = oct.ToString(CultureInfo.InvariantCulture);

                    Label lblNovsum = (Label)GridView2.FooterRow.FindControl("lblNovsum");
                    lblNovsum.Text = nov.ToString(CultureInfo.InvariantCulture);

                    Label lblDecsum = (Label)GridView2.FooterRow.FindControl("lblDecsum");
                    lblDecsum.Text = dec.ToString(CultureInfo.InvariantCulture);

                    Label lblJansum = (Label)GridView2.FooterRow.FindControl("lblJansum");
                    lblJansum.Text = jan.ToString(CultureInfo.InvariantCulture);

                    Label lblFebsum = (Label)GridView2.FooterRow.FindControl("lblFebsum");
                    lblFebsum.Text = feb.ToString(CultureInfo.InvariantCulture);

                    Label lblMarsum = (Label)GridView2.FooterRow.FindControl("lblMarsum");
                    lblMarsum.Text = mar.ToString(CultureInfo.InvariantCulture);

                    Label lblTotalChequeBounceFine = (Label)GridView2.FooterRow.FindControl("lblTotalChequeBounceFine");
                    lblTotalChequeBounceFine.Text = chequeFine.ToString(CultureInfo.InvariantCulture);
                }

              
            }
            catch
            {
            }
        }
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            monthName();
            loadStream();
            loadsection();
            divExport.Visible = false;
        }
        protected void drpCard_SelectedIndexChanged(object sender, EventArgs e)
        {
            divExport.Visible = false;
        }
        public override void Dispose()
        {
            con.Dispose();
            con1.Dispose();
            oo.Dispose();
        }
    }
}