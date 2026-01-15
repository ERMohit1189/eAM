using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _2
{
    [Serializable]
    public partial class AdminAllStudentReceiptMonthDate : Page
    {
        private SqlConnection _con;
        private readonly Campus _oo;
        private string _sql = string.Empty;
        decimal _totalFees;
        decimal _totalconvence;
        decimal _totalfine;
        decimal _totalexemption ;
        private decimal _totalamount;
        decimal _totalreceived;
        static string _month = String.Empty;
        public AdminAllStudentReceiptMonthDate()
        {
            _con = new SqlConnection();
            _oo = new Campus();
            _totalFees = 0;
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
            {
                Response.Redirect("default.aspx");
            }

            _con = _oo.dbGet_connection();
            Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file

            header1.Controls.Clear();
            BLL.BLLInstance.LoadHeader("Report", header1);
            if (!IsPostBack)
            {
                ImageButton1.Visible = false;
                ImageButton2.Visible = false;
                ImageButton3.Visible = false;
                ImageButton4.Visible = false;
                Label1.Visible = false;
                _sql = "Select FeeGroupName,id from FeeGroupMaster where sessionName='" + Session["SessionName"] + "'";
                _oo.FillDropDown_withValue_withSelect(_sql, DrpGroup, "FeeGroupName","Id");
                _oo.AddDateMonthYearDropDown(FromYY, FromMM, FromDD);
                _oo.AddDateMonthYearDropDown(ToYY, ToMM, ToDD);
                _oo.FindCurrentDateandSetinDropDown(FromYY, FromMM, FromDD);
                _oo.FindCurrentDateandSetinDropDown(ToYY, ToMM, ToDD);
                _sql = "select MonthName from MonthMaster where (CardType='" + DrpGroup.SelectedItem.Text + "' or CardType='" + DrpGroup.SelectedValue + "')";
                _sql = _sql + " and (classid='" + drpClass.SelectedValue + "' or classid is null) and sessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                _sql = _sql + " or Monthid=0  ";
                _sql = _sql + " order by MonthId";

                _oo.FillDropDownWithOutSelect(_sql, DropDownMonth, "MonthName");
                lblDate.Visible = false;
                lblDate1.Visible = false;
                Panel1.Visible = false;

                lblTitle.Text = "from ";
                lblTitle.Visible = false;

                lblTitle1.Text = "to ";
                lblTitle1.Visible = false;
                Panel2.Visible = false;
                Label32.Text = "";

                LinkButton5.Visible = true;

                if (RadioButtonList1.Items[0].Selected)
                {
                    Panel1.Visible = true;
                }
                abc.Visible = false;
                LoadUser();
                Loadclass();
                LoadSession();
            }
        }
        public void LoadSession()
        {
            _sql = "Select 'All' SessionName,'-1' Id Union Select SessionName,SessionId Id from SessionMaster order by Id";
            _oo.FillDropDown_withValue(_sql, drpSession, "SessionName", "Id");
            drpSession.SelectedValue = drpSession.Items.FindByText(Session["SessionName"].ToString()).Value;
        }


        public void Loadclass()
        {
            _sql = "Select ClassName,Id from ClassMaster where SessionName='" + Session["SessionName"] + "' Order by CIDOrder";
            _oo.FillDropDown_withValue(_sql, drpClass, "ClassName", "Id");
        }

        protected void LoadUser()
        {
            _sql = "Select UserId From NewAdminInformation";
            _oo.FillDropDownWithOutSelect(_sql, DropDownList1, "UserId");
            DropDownList1.Items.Insert(0, "All");
        }

        protected void FromYY_SelectedIndexChanged(object sender, EventArgs e)
        {
            _oo.YearDropDown(FromYY, FromMM, FromDD);
        }
        protected void FromMM_SelectedIndexChanged(object sender, EventArgs e)
        {
            _oo.MonthDropDown(FromYY, FromMM, FromDD);
        }
        protected void FromDD_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void ToYY_SelectedIndexChanged(object sender, EventArgs e)
        {
            _oo.YearDropDown(ToYY, ToMM, ToDD);
        }
        protected void ToMM_SelectedIndexChanged(object sender, EventArgs e)
        {
            _oo.MonthDropDown(ToYY, ToMM, ToDD);
        }
        protected void DropDownMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            // sql = "select ROW_NUMBER() OVER (ORDER BY fd.Id ASC) AS SNo,fd.Id as Id, fd.SrNo as SrNo ,SOD.Card as Card,sg.FirstName as FirstName,sg.MiddleName as MiddleName,SOD.Medium as Medium ,sg.LastName as LastName,SF.FatherName as FatherName,convert(nvarchar,fd.FeeDepositeDate,106) as FeeDepositeDate,sod.TypeOFAdmision as TypeOfAdmission ,";
            // sql = sql + "   fd.LateFeeAmount as LateFeeAmount,fd.RemainingAmount as RemainingAmount ,fd.Cocession as Cocession ,fd.RecieptSrNo as RecieptSrNo ,fd.RecievedAmount as RecievedAmount,fd.AmountInWords,fd.FeeMonth as FeeMonth,fd.Class as Class,fd.section as section from FeeDeposite Fd ";
            // sql = sql + "   left join StudentGenaralDetail sg on Fd.StEnRCode=sg.StEnRCode";
            // sql = sql + "  left join StudentFamilyDetails SF on Fd.StEnRCode=SF.StEnRCode";
            // sql = sql + "  left join StudentOfficialDetails SOD on Fd.StEnRCode=SOD.StEnRCode";
            // sql = sql + "  where fd.FeeMonth='" + DropDownMonth.SelectedItem.ToString() + "'";
            // sql = sql + "  and sg.SessionName='" + Session["SessionName"].ToString() + "' and sg.BranchCode=" + Session["BranchCode"].ToString() + " and Cancel is null";
            
            //Session["CardType"] = oo.ReturnTag(sql, "Card");

            // GridView1.DataSource = oo.GridFill(sql);
            // GridView1.DataBind();
            // GridView1.Visible = true;
            // ImageButton1.Visible = true;
            // ImageButton2.Visible = true;
            // ImageButton3.Visible = true;
            // ImageButton4.Visible = true;


            // sql = "  select SUM(RecievedAmount)as TotAmt from FeeDeposite ";
            // sql = sql + "  where FeeMonth='" + DropDownMonth.SelectedItem.ToString() + "'";
            // sql = sql + "  and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and Cancel is null";
            // try
            // {
            //     Label lbh = (Label)GridView1.FooterRow.FindControl("Label31");
            //     lbh.Text = oo.ReturnTag(sql, "TotAmt");
            // }
            // catch (Exception) { }
            // if (GridView1.Rows.Count == 0)
            // {
            //     oo.MessageBox("Sorry, No Record(s) found!!", this.Page);
            //     Label32.Text = "Sorry, No Record(s) found!!";
            //     ImageButton1.Visible = false;
            //     ImageButton2.Visible = false;
            //     ImageButton3.Visible = false;
            //     ImageButton4.Visible = false;

            // }
            // else
            // {
            //     Label32.Text = "";
            // }
        }
        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(RadioButtonList1.SelectedItem.ToString()=="Installment Wise")
            {
                Panel2.Visible=true;
                Panel1.Visible=false;
                GridView1.Visible = false;
                abc.Visible = false;
                LinkButton5.Visible = false;
                _sql = "select MonthName from MonthMaster where (CardType='" + DrpGroup.SelectedValue + "' or CardType='" + DrpGroup.SelectedItem.Text + "')";
                _sql = _sql + " and (classid='" + drpClass.SelectedValue + "' or classid is null) SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                _sql = _sql + " or Monthid=0  ";
                _sql = _sql + " order by MonthId";
                _oo.FillDropDownWithOutSelect(_sql, DropDownMonth, "MonthName");
                ImageButton1.Visible = false;
                ImageButton2.Visible = false;
                ImageButton3.Visible = false;
                ImageButton4.Visible = false;
                modDiv.Visible = false;
                classDiv.Visible = true;
            }
            else
            {   Panel1.Visible=true;
                Panel2.Visible=false;
                LinkButton5.Visible = true;
                GridView1.Visible = false;
                abc.Visible = false;
                ImageButton1.Visible = false;
                ImageButton2.Visible = false;
                ImageButton3.Visible = false;
                ImageButton4.Visible = false;
                modDiv.Visible = true;
                classDiv.Visible = false;
            }
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
      
        }
        
        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            // ReSharper disable once NotAccessedVariable
            string ss = string.Empty;

            _month = "";

            LinkButton chk = (LinkButton)sender;
            // ReSharper disable once RedundantAssignment
            ss = chk.ToolTip;
            lblDiscountPanel.Visible = true; lblDiscountPanelValue.Visible = true; Panel7.Visible = true;

            _sql = "select id,StEnRCode,SrNo,convert(nvarchar,FeeDepositeDate,106) as FeeDepositeDate,RecieptSrNo,";
            _sql = _sql + " Case when Cancel is not null then 'CANCELLED' Else (Case when Status='Pending' then 'PENDING' Else '' END) END Cancel, BusConvience,  FeeMonth,TotalFeeAmount,";
            _sql = _sql + " Cocession,RecievedAmount,CurrentAmount,LateFeeAmount,ChequeBouncedFine,RemainingAmount,Remark,Class,Section,BalanceMode,DiscountName,DiscountAmount,NextDueAmount";
            _sql = _sql + " from FeeDeposite  where  RecieptSrNo='" + chk.Text + "' and BranchCode=" + Session["BranchCode"] + "";


            lblTotalFee.Text = _oo.ReturnTag(_sql, "TotalFeeAmount");
            lblConcession.Text = _oo.ReturnTag(_sql, "Cocession");
            // ReSharper disable once SimplifyConditionalTernaryExpression
            tr11.Visible = (lblConcession.Text == string.Empty || lblConcession.Text == "0.00" || lblConcession.Text == "0") ? false : true;
            lblPaidAmount.Text = _oo.ReturnTag(_sql, "RecievedAmount");
            lblBalace.Text = _oo.ReturnTag(_sql, "RemainingAmount");
            // ReSharper disable once SimplifyConditionalTernaryExpression
            tr12.Visible = (lblBalace.Text == string.Empty || lblBalace.Text == "0.00" || lblBalace.Text == "0") ? false : true;
            lblRemark.Text = _oo.ReturnTag(_sql, "Remark");
            lblLate.Text = _oo.ReturnTag(_sql, "LateFeeAmount");
            // ReSharper disable once SimplifyConditionalTernaryExpression
            tr7.Visible = (lblLate.Text == string.Empty || lblLate.Text == "0.00" || lblLate.Text == "0") ? false : true;
            lblChequeBounce.Text = _oo.ReturnTag(_sql, "ChequeBouncedFine");
            // ReSharper disable once SimplifyConditionalTernaryExpression
            tr8.Visible = (lblChequeBounce.Text == string.Empty || lblChequeBounce.Text == "0.00" || lblChequeBounce.Text == "0") ? false : true;
            lblTotal.Text = _oo.ReturnTag(_sql, "CurrentAmount");
            Label31.Text = _oo.ReturnTag(_sql, "BusConvience");
            // ReSharper disable once SimplifyConditionalTernaryExpression
            tr10.Visible = (Label31.Text == string.Empty || Label31.Text == "0.00" || Label31.Text == "0") ? false : true;
            _month = _oo.ReturnTag(_sql, "FeeMonth");

            Session["NextDueAmt"] = _oo.ReturnTag(_sql, "NextDueAmount");
            try
            {
                lblDiscountPanel.Visible = true; lblDiscountPanelValue.Visible = true; Panel7.Visible = true;
                lblDiscountPanel.Text = _oo.ReturnTag(_sql, "DiscountName");
                lblDiscountPanelValue.Text = _oo.ReturnTag(_sql, "DiscountAmount");
                if (lblDiscountPanel.Text != "")
                {
                    Panel7.Visible = true;
                }
                else
                {
                    Panel7.Visible = false;
                }
            }
            catch (Exception) { lblDiscountPanel.Visible = false; lblDiscountPanelValue.Visible = false; Panel7.Visible = false; }
            if (_oo.ReturnTag(_sql, "BusConvience") == "")
            {
                Label31.Text = "0";
            }
            else
            {
                Label31.Text = _oo.ReturnTag(_sql, "BusConvience");
            }
            lblID.Text = chk.Text;

            lblcancel.Text = _oo.ReturnTag(_sql, "Cancel").Trim();
            Session["RCancel"] = _oo.ReturnTag(_sql, "Cancel").Trim();

            try
            {
                Label25.Text = _oo.ReturnTag(_sql, "RemainingAmount");
                // ReSharper disable once UnusedVariable
                double a = Convert.ToDouble(Label25.Text);
            }
            catch (Exception) { Label25.Text = "0"; _con.Close(); }

            // ReSharper disable once SimplifyConditionalTernaryExpression
            tr9.Visible = (Label25.Text == string.Empty || Label25.Text == "0.00" || Label25.Text == "0") ? false : true;
            Session["Installment"] = _oo.ReturnTag(_sql, "FeeMonth");

            Panel4_ModalPopupExtender.Show();
        }
        protected void  Button3_Click(object sender, EventArgs e)
        {
            // Session["RecieptNoSession"] = lblID.Text;
            //Response.Redirect("StudentReciptGenrate_duplicate.aspx?print=1");
            if(_month.ToUpper()=="Yearly".ToUpper())
            {
                Session["RecieptNoSession"] = lblID.Text.Trim();
                Response.Redirect("YearlyStudentReceiptGenerate_Duplicate.aspx?print=1");
            }
            else
            {
                Session["RecieptSrNo"] = lblID.Text.Trim();
                Session["isDuplicate"] = "Yes";
                Response.Redirect("FeeReceiptGenerate.aspx?print=1");
            }
        }
        protected void  Button1_Click(object sender, EventArgs e)
        {

        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }
        protected void GridView1_PageIndexChanged(object sender, EventArgs e)
        {

        }
        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            string studentId = Request.Form[hfStudentId.UniqueID];
            if (studentId == string.Empty || studentId == null)
            {
                studentId = txtSearch.Text.Trim();
            }
            string todate, fromdate;
            todate = ToDD.SelectedItem + " " + ToMM.SelectedItem + " " + ToYY.SelectedItem;
            fromdate = FromDD.SelectedItem + " " + FromMM.SelectedItem + " " + FromYY.SelectedItem;
            lblDate.Text = fromdate;
            lblDate1.Text = todate;
            lblDate.Visible = true;
            lblDate1.Visible = true;
            Panel1.Visible = true;
            lblTitle.Visible = true;
            lblTitle1.Visible = true;
            var param = new List<SqlParameter>
            {
                new SqlParameter("@FromDate",fromdate),
                new SqlParameter("@ToDate",todate),
                new SqlParameter("@Status",drpStatus.SelectedIndex != 0?drpStatus.SelectedItem.Text:""),
                new SqlParameter("@MOP",RadioButtonList2.SelectedIndex != 0?RadioButtonList2.SelectedItem.ToString():""),
                new SqlParameter("@LoginName",DropDownList1.SelectedIndex != 0?DropDownList1.SelectedItem.ToString():""),
                new SqlParameter("@SessionName",drpSession.SelectedIndex != 0?drpSession.SelectedItem.ToString():""),
                new SqlParameter("@Srno",string.IsNullOrEmpty(studentId)?"":studentId),
                new SqlParameter("@FeeMonth",drpFilter.SelectedValue)
            };

            GridView1.DataSource = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("Get_AllStudentReceiptWiseCollection", param);
            GridView1.DataBind();
            GridView1.Visible = true;
            ImageButton1.Visible = true;
            ImageButton2.Visible = true;
            ImageButton3.Visible = true;
            ImageButton4.Visible = true;
            if (GridView1.Rows.Count == 0)
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Sorry, No Record(s) found!", "A");
                ImageButton1.Visible = false;
                ImageButton2.Visible = false;
                ImageButton3.Visible = false;
                ImageButton4.Visible = false;
                abc.Visible = false;
                Label2.Text = "";
            }
            else
            {
                Label32.Text = "";
                abc.Visible = true;
                lblDate1.Visible = true;
                lblDate.Visible = true;
                lblTitle.Visible = true;
                lblTitle1.Visible = true;
                txtSearch.Text = string.Empty;
                hfStudentId.Value = string.Empty;
                Label1.Visible = true;
                Label2.Text = " by User " + DropDownList1.SelectedItem.Text;
            }
            if (GridView1.Rows.Count > 0)
            {
                string newInstallment = ""; string sql = "";
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    LinkButton lnkReceptNo = (LinkButton)GridView1.Rows[i].FindControl("LinkButton4");
                    Label lblinstal = (Label)GridView1.Rows[i].FindControl("lblinstal");
                    if (lblinstal.Text.Trim() == "")
                    {
                        string sqls = "Select Count(*) counts from FeeReceiptWithInstallment where ReceiptNo = '" + lnkReceptNo.Text.Trim() + "'";
                        int counts = int.Parse(BAL.objBal.ReturnTag(sqls, "counts"));
                        if (counts > 1)
                        {
                            string sql1 = "Select UPPER(InstallmentName) InstallmentName from FeeReceiptWithInstallment where ReceiptNo = '" + lnkReceptNo.Text.Trim() + "'";
                            var ds = _oo.GridFill(sql1);
                            for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                            {
                                if (j == 0)
                                {
                                    newInstallment = newInstallment + ds.Tables[0].Rows[j]["InstallmentName"].ToString() + " - ";
                                }
                                if ((j + 1) == ds.Tables[0].Rows.Count)
                                {
                                    newInstallment = newInstallment + ds.Tables[0].Rows[j]["InstallmentName"].ToString();
                                }
                            }
                        }
                    }
                    sql = "Select Case When(Select Count(*) from FeeReceiptWithInstallment where ReceiptNo = '" + lnkReceptNo.Text.Trim() + "') = 1 Then";
                    sql = sql + " (Select UPPER(InstallmentName) from FeeReceiptWithInstallment where ReceiptNo = '" + lnkReceptNo.Text.Trim() + "') Else '" + newInstallment + "' End FeeMonth";
                    lblinstal.Text = BAL.objBal.ReturnTag(sql, "FeeMonth");
                    sql = ""; newInstallment = "";
                }
            }
        }
        protected void DrpGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            _sql = "select MonthName from MonthMaster where (CardType='" + DrpGroup.SelectedValue + "' or CardType='" + DrpGroup.SelectedItem.Text + "')";
            _sql = _sql + " and (classid='" + drpClass.SelectedValue + "' or classid is null) and sessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " or monthid=0";
            _sql = _sql + "  order by MonthId";
            _oo.FillDropDownWithOutSelect(_sql, DropDownMonth, "MonthName");
        }
        protected void ImageButton1_Click(object sender, EventArgs e)
        {
            _oo.ExportToWord(Response, "AllStudentReceiptMonthDate.doc", gdv1);
        }

        protected void ImageButton2_Click(object sender, EventArgs e)
        {
            _oo.ExportToExcel("AllStudentReceiptMonthDate.xls", GridView1);
        }
        protected void ImageButton3_Click(object sender, EventArgs e)
        {

        }

        protected void ImageButton4_Click(object sender, EventArgs e)
        {
            PrintHelper_New.ctrl = abc;
            if (GridView1.Rows.Count > 0)
            {
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            ScriptManager.RegisterClientScriptBlock(ImageButton4, GetType(), "onclick", "var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}",true);
        }
        protected void LinkButton6_Click(object sender, EventArgs e)
        {
            _sql = "select ROW_NUMBER() OVER (ORDER BY fd.Id ASC) AS SNo,fd.Id as Id, fd.SrNo as SrNo ,UPPER(SOD.Card) as Card,UPPER(sg.FirstName) as FirstName,UPPER(sg.MiddleName) as MiddleName,UPPER(SOD.Medium) as Medium ,UPPER(sg.LastName) as LastName,UPPER(SF.FatherName) as FatherName,convert(nvarchar,fd.FeeDepositeDate,106) as FeeDepositeDate,UPPER(sod.TypeOFAdmision) as TypeOfAdmission ,";
            _sql = _sql + "   fd.LateFeeAmount as LateFeeAmount,fd.MOP,fd.RemainingAmount as RemainingAmount ,fd.Cocession as Cocession ,fd.RecieptSrNo as RecieptSrNo ,fd.RecievedAmount as RecievedAmount,UPPER(fd.AmountInWords) AmountInWords,UPPER(fd.FeeMonth) as FeeMonth,UPPER(fd.Class) Class,UPPER(fd.section) section from FeeDeposite Fd ";
            _sql = _sql + "   left join StudentGenaralDetail sg on Fd.StEnRCode=sg.StEnRCode";
            _sql = _sql + "  left join StudentFamilyDetails SF on Fd.StEnRCode=SF.StEnRCode";
            _sql = _sql + "  left join StudentOfficialDetails SOD on Fd.StEnRCode=SOD.StEnRCode and Fd.SrNo=SOD.SrNo";
            _sql = _sql + "  where fd.Status='Paid' and sod.Card='" + DrpGroup.SelectedItem + "' and fd.FeeMonth='" + DropDownMonth.SelectedItem + "' and Cancel is null";
            _sql = _sql + "  and sg.SessionName='" + Session["SessionName"] + "' and sg.BranchCode=" + Session["BranchCode"] + " ";
            _sql = _sql + "  and sf.SessionName='" + Session["SessionName"] + "'  and sod.SessionName='" + Session["SessionName"] + "'";
            if (RadioButtonList2.SelectedIndex != 0)
            {
                _sql = _sql + " and fd.MoP='" + RadioButtonList2.SelectedItem + "'";
            }
            if (DropDownList1.SelectedItem.Text != "All")
            {
                _sql = _sql + " and fd.LoginName='" + DropDownList1.SelectedItem + "'";
            }
            Session["CardType"] = _oo.ReturnTag(_sql, "Card");

            GridView1.DataSource = _oo.GridFill(_sql);
            GridView1.DataBind();
            GridView1.Visible = true;
            ImageButton1.Visible = true;
            ImageButton2.Visible = true;
            ImageButton3.Visible = true;
            ImageButton4.Visible = true;

            _sql = "  select SUM(fd.RecievedAmount) as TotAmt from FeeDeposite fd";
            _sql = _sql + "  inner join StudentOfficialDetails sod on fd.SrNo=sod.SrNo and fd.StEnRCode=sod.StEnRCode";
            _sql = _sql + "  where fd.Status='Paid' and sod.Card='" + DrpGroup.SelectedItem + "'";
            _sql = _sql + "  and fd.FeeMonth='" + DropDownMonth.SelectedItem + "'";
            _sql = _sql + "  and fd.BranchCode=" + Session["BranchCode"] + " and fd.Cancel is null";
            if (RadioButtonList2.SelectedIndex != 0)
            {
                _sql = _sql + " and fd.MoP='" + RadioButtonList2.SelectedItem + "'";
            }
            if (DropDownList1.SelectedItem.Text != "All")
            {
                _sql = _sql + " and fd.LoginName='" + DropDownList1.SelectedItem + "'";
            }
            try
            {
                Label lbh = (Label)GridView1.FooterRow.FindControl("Label31");
                lbh.Text = _oo.ReturnTag(_sql, "TotAmt");
            }
            catch (Exception)
            {
                // ignored
            }
            if (GridView1.Rows.Count == 0)
            {
                //oo.MessageBox("Sorry, No Record(s) found!", this.Page);
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Sorry, No Record(s) found!", "A");       

                //Label32.Text = "Sorry, No Record(s) found!";
                ImageButton1.Visible = false;
                ImageButton2.Visible = false;
                ImageButton3.Visible = false;
                ImageButton4.Visible = false;
                abc.Visible = false;
            }
            else
            {
                Label32.Text = "";
                abc.Visible = true;
                lblDate1.Visible = false;
                lblDate.Visible = false;
                lblTitle.Visible = false;
                lblTitle1.Visible = false;
                //Label3.Visible = false;
                Label1.Visible = true;
                Label2.Text = " by User " + DropDownList1.SelectedItem.Text;
            }
        }


        protected void RadioButtonList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RadioButtonList1.SelectedItem.ToString() == "Installment Wise")
            {
                Panel2.Visible = true;
                Panel1.Visible = false;
                GridView1.DataSource = null;
                GridView1.DataBind();
                abc.Visible = false;
                LinkButton5.Visible = false;
                _sql = "select MonthName from MonthMaster where CardType='" + DrpGroup.SelectedItem + "'";
                _sql = _sql + " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                _sql = _sql + " or Monthid=0  ";
                _sql = _sql + " order by MonthId";

                _oo.FillDropDownWithOutSelect(_sql, DropDownMonth, "MonthName");
                ImageButton1.Visible = false;
                ImageButton2.Visible = false;
                ImageButton3.Visible = false;
                ImageButton4.Visible = false;
                drpFilter.Visible = false;
            }
            else
            {
                Panel1.Visible = true;
                Panel2.Visible = false;
                LinkButton5.Visible = true;
                abc.Visible = false;
                ImageButton1.Visible = false;
                ImageButton2.Visible = false;
                ImageButton3.Visible = false;
                ImageButton4.Visible = false;
                drpFilter.Visible = true;
            }
        }

       
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // ReSharper disable once RedundantAssignment
                decimal values = 0;

                Label lblFees = (Label)e.Row.FindControl("lblFees");
                decimal.TryParse(lblFees.Text, out values);
                if (values > 0) { _totalFees += values; }
                
                Label lblConvience = (Label)e.Row.FindControl("lblConvience");
                decimal.TryParse(lblConvience.Text, out values);
                if (values > 0) { _totalconvence += values; }          

                Label lblFine = (Label)e.Row.FindControl("lblFine");
                decimal.TryParse(lblFine.Text, out values);
                if (values > 0) { _totalfine += values; }
           
                Label lblDiscountAmount = (Label)e.Row.FindControl("lblDiscountAmount");
                decimal.TryParse(lblDiscountAmount.Text, out values);
                if (values > 0) { _totalexemption += values; }

                Label lblAmount = (Label)e.Row.FindControl("lblAmount");
                decimal.TryParse(lblAmount.Text, out values);
                if (values > 0) { _totalamount += values; }
           
                Label label29 = (Label)e.Row.FindControl("Label29");
                decimal.TryParse(label29.Text, out values);
                if (values > 0) { _totalreceived += values; }
           
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotalFees = (Label)e.Row.FindControl("lblTotalFees");
                lblTotalFees.Text = _totalFees.ToString(CultureInfo.InvariantCulture);

                Label lblTotalConvience = (Label)e.Row.FindControl("lblTotalConvience");
                lblTotalConvience.Text = _totalconvence.ToString(CultureInfo.InvariantCulture);

                Label lblTotalFine = (Label)e.Row.FindControl("lblTotalFine");
                lblTotalFine.Text = _totalfine.ToString(CultureInfo.InvariantCulture);

                Label lblTotalDiscount = (Label)e.Row.FindControl("lblTotalDiscount");
                lblTotalDiscount.Text = (-_totalexemption).ToString(CultureInfo.InvariantCulture);

                Label lblTotalAmount = (Label)e.Row.FindControl("lblTotalAmount");
                lblTotalAmount.Text = _totalamount.ToString(CultureInfo.InvariantCulture);

                Label lblTotalReceived = (Label)e.Row.FindControl("Label31");
                lblTotalReceived.Text = _totalreceived.ToString(CultureInfo.InvariantCulture);
            }
        }

        public override void Dispose()
        {
            _con.Dispose();
            _oo.Dispose();
        }
    }
}