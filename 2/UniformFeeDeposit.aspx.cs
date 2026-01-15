using c4SmsNew;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _2
{
    public partial class UniformFeeDeposit : Page
    {
        private SqlConnection _con;
        readonly Campus _oo;
        private string _sql = "";
        public UniformFeeDeposit() 
        {
            _con = new SqlConnection();
            _oo = new Campus();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            _con = _oo.dbGet_connection();
            Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file

            if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
            {
                Response.Redirect("default.aspx");
            }

            if (!IsPostBack)
            {
                TxtEnter.Focus();
                Session["CheckRefresh"] = Server.UrlDecode(DateTime.Now.ToString(CultureInfo.InvariantCulture));
                Checkbool();
                _oo.AddDateMonthYearDropDown(DDYear, DDMonth, DDDate);
                _oo.FindCurrentDateandSetinDropDown(DDYear, DDMonth, DDDate);
                _oo.AddDateMonthYearDropDown(DDYearMode, DDMonthMode, DDDateMode);
                _oo.FindCurrentDateandSetinDropDown(DDYearMode, DDMonthMode, DDDateMode);
                Tables.Visible = false;

                var ds1 = DLL.objDll.SelectRecord_usingExecuteDataset("USP_GetGatewayList");
                if (ds1.Tables != null)
                {
                    if (ds1.Tables[1].Rows.Count > 0)
                    {
                        data_key.Text = ds1.Tables[1].Rows[0]["LivePublicKey"].ToString();
                        data_email.Text = ds1.Tables[1].Rows[0]["Email"].ToString();
                        data_PseudoUniqueReference.Text = ds1.Tables[1].Rows[0]["PseudoUniqueReference"].ToString();
                    }
                }
            }
            _sql = "select TableId from Admin_fee_permission_setting where SessionName='" + Session["SessionName"] + "' and TableId=10 and SUBSTRING(Enable, 0, CharIndex(',',Enable,0))='Yes' and SUBSTRING(Enable, CharIndex(',',Enable,0)+1, 1)=1";
            if (_oo.Duplicate(_sql))
            {
                DDYear.Enabled = true;
                DDMonth.Enabled = true;
                DDDate.Enabled = true;
            }
        }
        protected void Checkbool()
        {
            try
            {
                _sql = "Select flag from BarcodeSetting";
                if (!string.IsNullOrEmpty(_oo.ReturnTag(_sql, "flag")))
                {
                    if (_oo.ReturnTag(_sql, "flag") == "0")
                    {
                        TxtEnter.AutoPostBack = false;
                    }
                    else
                    {
                        TxtEnter.AutoPostBack = true;
                    }
                }
                else
                {
                    TxtEnter.AutoPostBack = false;
                }
            }
            catch
            {
                TxtEnter.AutoPostBack = false;
            }
        }
        public void Fill_value()
        {
            if (Grd.Rows.Count > 0)
            {
                Label lblClassId = (Label)Grd.Rows[0].FindControl("lblClassId");
                Label lblGender = (Label)Grd.Rows[0].FindControl("lblGender");
                _sql = "Select Row_Number() Over(Order By Id) as Ids,HeadName,Id, Amount,(0*Amount) as Total,'0' as Concession, ClassId, Gender from UniformFeeHeadMaster where";
                _sql += " SessionName='" + Session["SessionName"].ToString() + "' and Gender=(case when '" + lblGender.Text.Trim() + "'='Male' then 1 else case when '" + lblGender.Text.Trim() + "'='Female' then 2 else 3 end end)  and ClassId='" + lblClassId.Text + "' Order By Id";
                GridView1.DataSource = _oo.GridFill(_sql);
                GridView1.DataBind();
            }
        }
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            string studentId = Request.Form[hfStudentId.UniqueID];
            if (studentId == string.Empty)
            {
                hfStudentId.Value = TxtEnter.Text.Replace("&", "/").Trim();
            }
            showDetails();

        }

        public void showDetails()
        {
            string studentId = Request.Form[hfStudentId.UniqueID];
            if (studentId == string.Empty)
            {
                studentId = TxtEnter.Text.Trim();
            }


            if (studentId != "")
            {
                _sql = "select StEnRCode,srno,Name,FatherName, Gender,ClassId,CombineClassName,Medium,DateOfAdmiission,FamilyContactNo, PhotoPath";
                _sql += " from AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") asr ";
                _sql += " where SrNo ='" + studentId + "' and Withdrwal is null";

                Grd.DataSource = _oo.GridFill(_sql);
                Grd.DataBind();
                DataSet ds;
                ds = _oo.GridFill(_sql);
                // ReSharper disable once UseNullPropagation
                if (ds != null && Grd.Rows.Count > 0)
                {
                    grdshow.Visible = true;
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        img.ImageUrl = ds.Tables[0].Rows[0]["PhotoPath"].ToString() != string.Empty ? ds.Tables[0].Rows[0]["PhotoPath"] + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture) : "../img/user-pic/user-pic.jpg" + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture);
                        studentImg.NavigateUrl = ds.Tables[0].Rows[0]["PhotoPath"].ToString() != string.Empty ? ds.Tables[0].Rows[0]["PhotoPath"] + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture) : "../img/user-pic/user-pic.jpg" + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture);
                        hylinkmoredetails.NavigateUrl = "../11/StudentRegView.aspx?print=1&id=" + ds.Tables[0].Rows[0]["StEnRCode"];
                    }
                }
                if (Grd.Rows.Count == 0)
                {
                    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, Record not found!", "A");
                    Tables.Visible = false;
                    Div1.Visible = false;
                }
                else
                {
                    Label lblClassId = (Label)Grd.Rows[0].FindControl("lblClassId");
                    Label lblGender = (Label)Grd.Rows[0].FindControl("lblGender");
                    _sql = "select COUNT(*) AS counts from UniformFeeHeadMaster where SessionName='" + Session["SessionName"].ToString() + "' and ClassId=" + lblClassId.Text + " and Gender=(case when '" + lblGender.Text + "'='Male' then 1 else case when '" + lblGender.Text + "'='Female' then 2 else 3 end end)";
                    if (double.Parse(_oo.ReturnTag(_sql, "counts")) > 0)
                    {
                        Fill_value();
                        Tables.Visible = true;
                        Div1.Visible = true;
                        count();
                    }
                    else
                    {
                        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, Product head(s) not found!", "A");
                        Tables.Visible = false;
                        Div1.Visible = false;
                    }


                }
                Permission_Values();
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                Tables.Visible = false;
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please, Enter S.R.NO.!", "A");
            }
        }
        public void Grid_fill()
        {
            Label lblClassId = (Label)Grd.Rows[0].FindControl("lblClassId");
            Label lblGender = (Label)Grd.Rows[0].FindControl("lblGender");
            _sql = "Select ROW_NUMBER() OVER(ORDER BY fd.id ASC) as ids, fd.Receipt_no, AmtForPay as PayableAmount, Discount, PaidAmt, NextDeuAmt, PaymentSatus, DepositDate, Mode  from UniformFeeDeposit fd";
            _sql += " where fd.SessionName = '" + Session["SessionName"].ToString() + "' and fd.ClassId = '" + lblClassId.Text + "' and fd.SrNo = '" + hfStudentId.Value + "' Order By fd.Id";
            GridView2.DataSource = _oo.GridFill(_sql);
            GridView2.DataBind();
            //try
            //{
            //    for (int i = 0; i < GridView2.Rows.Count; i++)
            //    {

            //        Label lbl_sr = (Label)GridView2.Rows[i].FindControl("lbl_sr");
            //        lbl_sr.Text = (GridView2.Rows.Count - i).ToString();

            //    }
            //}
            //catch
            //{

            //}


        }
        public void count()
        {
            if (GridView1.Rows.Count > 0)
            {
                try
                {
                    Label lblClassId = (Label)Grd.Rows[0].FindControl("lblClassId");
                    Label lblGender = (Label)Grd.Rows[0].FindControl("lblGender");
                    _sql = "Select top 1 PaymentSatus from UniformFeeDeposit where";
                    _sql += " SessionName='" + Session["SessionName"].ToString() + "' and  srno='" + hfStudentId.Value + "'  and ClassId='" + lblClassId.Text + "' and PaymentSatus='Pending'";
                    if (_oo.ReturnTag(_sql, "status") == "Pending")
                    {
                        //divSubmit.Visible = false;
                        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, Please clear pending payment.", "A");
                        Grid_fill();
                    }
                    else
                    {
                        //divSubmit.Visible = true;
                        LblPaidAmount.Text = "";
                        txtDiscount.Text = "0";
                        LblDueAmount.Text = "";
                        TextPay.Text = "";
                        lblNextDueAmt.Text = "0";
                        Label TotalHeadAmount = (Label)GridView1.FooterRow.FindControl("TotalHeadAmount");
                        double Sum1 = 0;
                        for (int i = 0; i < GridView1.Rows.Count; i++)
                        {
                            TextBox txtHeadQty = (TextBox)GridView1.Rows[i].FindControl("HeadQty");
                            Label txtHeadAmount = (Label)GridView1.Rows[i].FindControl("HeadAmount");

                            if (txtHeadQty.Text != "" && txtHeadAmount.Text != "")
                            {
                                Sum1 = Sum1 + (double.Parse(txtHeadQty.Text) * double.Parse(txtHeadAmount.Text));
                            }
                        }
                        TotalHeadAmount.Text = Sum1.ToString(CultureInfo.InvariantCulture);
                        LblHeadTotalAmount.Text = Sum1.ToString(CultureInfo.InvariantCulture);
                        LblDueAmount.Text = Sum1.ToString(CultureInfo.InvariantCulture);
                        TextPay.Text = Sum1.ToString(CultureInfo.InvariantCulture);
                        LblPaidAmount.Text = "0";

                        if (hfStudentId.Value == "")
                        {
                            hfStudentId.Value = TxtEnter.Text.Trim();
                        }

                        //_sql = "Select Sum(isnull(PaidAmt, 0)) as PaidAmt, Sum(isnull(Discount, 0)) as Discount from UniformFeeDeposit where";
                        //_sql +=  " SessionName='" + Session["SessionName"].ToString() + "' and  srno='" + hfStudentId.Value + "'  and ClassId='" + lblClassId.Text + "' and PaymentSatus='Paid'";
                        //if (_oo.ReturnTag(_sql, "PaidAmt") != "")
                        //{
                        //    LblPaidAmount.Text = (double.Parse(_oo.ReturnTag(_sql, "PaidAmt")) + double.Parse(_oo.ReturnTag(_sql, "Discount"))).ToString("0");
                        //    //txtDiscount.Text = _oo.ReturnTag(_sql, "Discount");
                        //    LblDueAmount.Text = (double.Parse(LblHeadTotalAmount.Text) - (double.Parse(LblPaidAmount.Text) + double.Parse(txtDiscount.Text))).ToString("0");
                        //    TextPay.Text = LblDueAmount.Text;
                        //    if (double.Parse(LblDueAmount.Text) == 0)
                        //    {
                        //        TextPay.Enabled = false;
                        //        txtDiscount.Enabled = false;
                        //        //btnSubmit.Visible = false;
                        //        //divSubmit.Visible = false;
                        //        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Success, this student has been deposited complete uniform fee.", "S");
                        //    }
                        //    else
                        //    {
                        //        TextPay.Enabled = true;
                        //        txtDiscount.Enabled = true;
                        //        //btnSubmit.Visible = true;
                        //        //divSubmit.Visible = true;
                        //    }
                        //    Grid_fill();
                        //}
                        //else
                        //{
                        //    LblPaidAmount.Text = "0";
                        //    LblDueAmount.Text = (double.Parse(LblHeadTotalAmount.Text) - 0).ToString("0");
                        //    TextPay.Text = LblDueAmount.Text;

                        //    if (double.Parse(LblDueAmount.Text) == 0)
                        //    {
                        //        TextPay.Enabled = false;
                        //        txtDiscount.Enabled = false;
                        //        //btnSubmit.Visible = false;
                        //        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Success, this student has been deposited complete uniform fee.", "S");
                        //    }
                        //    else
                        //    {
                        //        TextPay.Enabled = true;
                        //        txtDiscount.Enabled = true;
                        //        //btnSubmit.Visible = true;
                        //    }

                        //    Grid_fill();
                        //}
                        Grid_fill();
                        txtDiscount.Text = "0";
                    }

                }
                catch
                {

                }
            }
            if (TxtEnter.Text == "")
            {
                TxtEnter.Focus();
            }
            else
            {
                //Button1.Focus();
            }
        }
        public void Permission_Values()
        {
            string sql8;
            sql8 = "select Enable from Admin_fee_permission_setting where Tableid='10' and SessionName='" + Session["SessionName"].ToString() + "'";
            if (_oo.ReturnTag(sql8, "Enable") == "" || _oo.ReturnTag(sql8, "Enable") == "No,1,2,3" || _oo.ReturnTag(sql8, "Enable") == "Yes,1,2,3" || _oo.ReturnTag(sql8, "Enable") == "Yes,1" || _oo.ReturnTag(sql8, "Enable") == "Yes,2" || _oo.ReturnTag(sql8, "Enable") == "Yes,3" || _oo.ReturnTag(sql8, "Enable") == "Yes,1,2" || _oo.ReturnTag(sql8, "Enable") == "Yes,1,3" || _oo.ReturnTag(sql8, "Enable") == "Yes,2,3")
            {
                if (Grd.Rows.Count > 0)
                {
                    Label lbl_std_srno = (Label)Grd.Rows[0].FindControl("Label1");
                    _con.Open();
                    SqlCommand Permission_check = new SqlCommand("select count(*) from Administrator_Permission where Tableid='4' and SrNo='" + lbl_std_srno.Text + "' and Permission_Session='" + Session["SessionName"].ToString() + "' and Permission like 'Yes%'", _con);
                    int Permission_check_count = (int)Permission_check.ExecuteScalar();
                    _con.Close();

                    if (Permission_check_count == 1)
                    {

                        _sql = "select permission from Administrator_Permission where Tableid='4' and SrNo='" + lbl_std_srno.Text + "' and Permission_Session='" + Session["SessionName"].ToString() + "' and Permission like 'Yes%'";
                        string[] permission = _oo.ReturnTag(_sql, "permission").Split(',');
                        if (permission.Length > 1)
                        {
                            DDYear.Enabled = false;
                            DDMonth.Enabled = false;
                            DDDate.Enabled = false;
                            txtDiscount.Text = "0";
                            txtDiscount.Enabled = false;
                            //TextPay.Enabled = false;

                            for (int i = 1; i <= permission.Length - 1; i++)
                            {
                                if (permission[i].ToString() == "1")
                                {
                                    DDYear.Enabled = true;
                                    DDMonth.Enabled = true;
                                    DDDate.Enabled = true;
                                }
                                if (permission[i].ToString() == "2")
                                {
                                    txtDiscount.Enabled = true;
                                }
                                if (permission[i].ToString() == "3")
                                {
                                    //TextPay.Enabled = true;
                                }
                            }
                        }
                        else
                        {
                            string[] permission1 = _oo.ReturnTag(sql8, "Enable").Split(',');
                            if (permission1.Length > 1)
                            {
                                DDYear.Enabled = false;
                                DDMonth.Enabled = false;
                                DDDate.Enabled = false;
                                txtDiscount.Text = "0";
                                txtDiscount.Enabled = false;
                                //TextPay.Enabled = false;

                                if (permission1[0].ToString() == "Yes")
                                {
                                    for (int i = 1; i <= permission1.Length - 1; i++)
                                    {
                                        if (permission1[i].ToString() == "1")
                                        {
                                            DDYear.Enabled = true;
                                            DDMonth.Enabled = true;
                                            DDDate.Enabled = true;
                                        }
                                        if (permission1[i].ToString() == "2")
                                        {
                                            txtDiscount.Enabled = true;
                                        }
                                        if (permission1[i].ToString() == "3")
                                        {
                                            //TextPay.Enabled = true;
                                        }
                                    }
                                }
                            }
                        }

                    }

                    else
                    {

                        string[] permission2 = _oo.ReturnTag(sql8, "Enable").Split(',');
                        if (permission2.Length > 1)
                        {
                            DDYear.Enabled = false;
                            DDMonth.Enabled = false;
                            DDDate.Enabled = false;
                            txtDiscount.Text = "0";
                            txtDiscount.Enabled = false;
                            //TextPay.Enabled = false;

                            if (permission2[0].ToString() == "Yes")
                            {
                                for (int i = 1; i <= permission2.Length - 1; i++)
                                {
                                    if (permission2[i].ToString() == "1")
                                    {
                                        DDYear.Enabled = true;
                                        DDMonth.Enabled = true;
                                        DDDate.Enabled = true;
                                    }
                                    if (permission2[i].ToString() == "2")
                                    {
                                        txtDiscount.Enabled = true;
                                    }
                                    if (permission2[i].ToString() == "3")
                                    {
                                        TextPay.Enabled = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        protected void DDYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            _oo.YearDropDown(DDYear, DDMonth, DDDate);
        }
        protected void DDMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            _oo.MonthDropDown(DDYear, DDMonth, DDDate);
        }
        protected void DDYearMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            _oo.YearDropDown(DDYearMode, DDMonthMode, DDDateMode);
        }
        protected void DDMonthMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            _oo.MonthDropDown(DDYearMode, DDMonthMode, DDDateMode);
        }
        protected void viewReceipt_Click(object sender, EventArgs e)
        {
            string studentId = Request.Form[hfStudentId.UniqueID];
            if (studentId == string.Empty)
            {
                studentId = TxtEnter.Text.Trim();
            }
            Label lblClassId = (Label)Grd.Rows[0].FindControl("lblClassId");
            Label lblGender = (Label)Grd.Rows[0].FindControl("lblGender");
            LinkButton link = (LinkButton)sender;
            Label34.Text = link.Text;
            _sql = " select Receipt_no, Name+' '+ClassName+' '+(case when BranchName='OTHER' then '' else BranchName end)+' ('+SectionName+')' as name, DepositDate from AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "', " + Session["BranchCode"].ToString() + ") asr";
            _sql = _sql + " inner join uniformFeeDeposit fd on fd.ClassId = asr.ClassId and fd.SessionName = asr.SessionName";
            _sql += " where asr.SrNo = '" + hfStudentId.Value + "' and asr.ClassId = " + lblClassId.Text + " and fd.SrNo = '" + studentId + "' and fd.ClassId = " + lblClassId.Text + " and fd.SessionName = '" + Session["SessionName"].ToString() + "' and asr.SessionName = '" + Session["SessionName"].ToString() + "'";
            _sql += " and Receipt_no = '" + link.Text + "'";

            Labelname.Text = _oo.ReturnTag(_sql, "name");
            LabelDepositDate.Text = DateTime.Parse(_oo.ReturnTag(_sql, "DepositDate")).ToString("dd MMM yyyy hh:mm tt");



            _sql = "Select Row_Number() Over(Order By Id) as Ids, HeadName, Qty, Amount, (Qty*Amount) Total from UniformHistory where Receipt_no = '" + Label34.Text + "' and SessionName = '" + Session["SessionName"].ToString() + "'";

            GridView3.DataSource = _oo.GridFill(_sql);
            GridView3.DataBind();

            Label TotalHeadAmount0 = (Label)GridView3.FooterRow.FindControl("TotalHeadAmount0");
            double Sum1 = 0;
            for (int i = 0; i < GridView3.Rows.Count; i++)
            {
                Label TextBox1 = (Label)GridView3.Rows[i].FindControl("lbl_HeadTotal");

                if (TextBox1.Text != "")
                {
                    Sum1 = Sum1 + double.Parse(TextBox1.Text);
                }
            }
            TotalHeadAmount0.Text = Sum1.ToString(CultureInfo.InvariantCulture);


            model.Show();
        }

        //public void SendFeesSms(string FmobileNo, string RecieptNo, string Amount)
        //{
        //	_sql = "Select HitValue from SMSActivateDeactivate where BranchCode=" + Session["BranchCode"] + "";
        //	if (_oo.ReturnTag(_sql, "HitValue") != "")
        //	{
        //		if (_oo.ReturnTag(_sql, "HitValue") == "true")
        //		{
        //			SMSAdapterNew sadpNew = new SMSAdapterNew();
        //			string mess = "";

        //			mess = "INR " + Amount + " received towards Product Fee Deposit. Receipt No. " + RecieptNo + "";
        //			string sms_response = "";

        //			if (FmobileNo != "")
        //			{
        //				_sql = "Select SmsSent From SmsEmailMaster where Id='8' ";
        //				if (_oo.ReturnTag(_sql, "SmsSent").Trim() == "true")
        //				{
        //					sms_response = sadpNew.Send(mess, FmobileNo, "");
        //				}
        //			}
        //		}
        //	}
        //}
        protected void Page_PreRender(object sender, EventArgs e)
        {
            ViewState["CheckRefresh"] = Session["CheckRefresh"];
        }
        public string Invoice_no_generate()
        {
            string Invoice_no = "";
            _sql = "Select count(*) as Counter from UniformFeeDeposit";
            if (_oo.ReturnTag(_sql, "Counter") == "0")
            {
                Invoice_no = Invoice_no + 1;
            }
            else
            {
                _sql = "Select Top 1 Receipt_no From UniformFeeDeposit Order By Id Desc";
                string[] words = _oo.ReturnTag(_sql, "Receipt_no").Split('/');
                Double Value = Convert.ToDouble(words[2].ToString()) + 1;
                Invoice_no = Invoice_no + Value.ToString(CultureInfo.InvariantCulture);
            }
            return "UF/" + Session["SessionName"].ToString() + "/00000" + Invoice_no;
        }



        public void Permission_Values1(string srno)
        {
            string sql8;
            sql8 = "select Enable from Admin_fee_permission_setting where Tableid='5' and SessionName='" + Session["SessionName"].ToString() + "'";
            if (_oo.ReturnTag(sql8, "Enable") == "" || _oo.ReturnTag(sql8, "Enable") == "No,1,2,3" || _oo.ReturnTag(sql8, "Enable") == "Yes,1,2,3" || _oo.ReturnTag(sql8, "Enable") == "Yes,1" || _oo.ReturnTag(sql8, "Enable") == "Yes,2" || _oo.ReturnTag(sql8, "Enable") == "Yes,3" || _oo.ReturnTag(sql8, "Enable") == "Yes,1,2" || _oo.ReturnTag(sql8, "Enable") == "Yes,1,3" || _oo.ReturnTag(sql8, "Enable") == "Yes,2,3")
            {

                //Label lbl_std_srno = (Label)Grd.Rows[0].FindControl("Label1");
                _con.Open();
                SqlCommand Permission_check = new SqlCommand("select count(*) from Administrator_Permission where Tableid='5' and SrNo='" + srno + "' and Permission_Session='" + Session["SessionName"].ToString() + "' and Permission like 'Yes%'", _con);
                int Permission_check_count = (int)Permission_check.ExecuteScalar();
                _con.Close();

                if (Permission_check_count == 1)
                {

                    _sql = "select permission from Administrator_Permission where Tableid='5' and SrNo='" + srno + "' and Permission_Session='" + Session["SessionName"].ToString() + "' and Permission like 'Yes%'";
                    string[] permission = _oo.ReturnTag(_sql, "permission").Split(',');
                    if (permission.Length > 1)
                    {
                        DDYear.Enabled = false;
                        DDMonth.Enabled = false;
                        DDDate.Enabled = false;

                        for (int i = 1; i <= permission.Length - 1; i++)
                        {
                            if (permission[i].ToString() == "1")
                            {
                                DDYear.Enabled = true;
                                DDMonth.Enabled = true;
                                DDDate.Enabled = true;
                            }

                        }
                    }
                    else
                    {
                        string[] permission1 = _oo.ReturnTag(sql8, "Enable").Split(',');
                        if (permission1.Length > 1)
                        {
                            DDYear.Enabled = false;
                            DDMonth.Enabled = false;
                            DDDate.Enabled = false;

                            if (permission1[0].ToString() == "Yes")
                            {
                                for (int i = 1; i <= permission1.Length - 1; i++)
                                {
                                    if (permission1[i].ToString() == "1")
                                    {
                                        DDYear.Enabled = true;
                                        DDMonth.Enabled = true;
                                        DDDate.Enabled = true;
                                    }

                                }
                            }
                        }
                    }

                }

                else
                {

                    string[] permission2 = _oo.ReturnTag(sql8, "Enable").Split(',');
                    if (permission2.Length > 1)
                    {
                        DDYear.Enabled = false;
                        DDMonth.Enabled = false;
                        DDDate.Enabled = false;

                        if (permission2[0].ToString() == "Yes")
                        {
                            for (int i = 1; i <= permission2.Length - 1; i++)
                            {
                                if (permission2[i].ToString() == "1")
                                {
                                    DDYear.Enabled = true;
                                    DDMonth.Enabled = true;
                                    DDDate.Enabled = true;
                                }

                            }
                        }
                    }
                }
            }
        }


        public void SendFeescancleSms(string FmobileNo, string RecieptNo, string Amount)
        {
            _sql = "Select HitValue from SMSActivateDeactivate where BranchCode=" + Session["BranchCode"] + "";
            if (_oo.ReturnTag(_sql, "HitValue") != "")
            {
                if (_oo.ReturnTag(_sql, "HitValue") == "true")
                {
                    SMSAdapterNew sadpNew = new SMSAdapterNew();
                    string mess = "";

                    mess = "Receipt No. " + RecieptNo + " has been cancelled of Other Fee Deposit. Refunded Amount is INR " + Amount + " ";
                    string sms_response = "";

                    if (FmobileNo != "")
                    {

                        _sql = "Select SmsSent From SmsEmailMaster where Id='7' and BranchCode='" + Session["BranchCode"] + "'";
                        if (_oo.ReturnTag(_sql, "SmsSent").Trim() == "true")
                        {
                            sms_response = sadpNew.Send(mess, FmobileNo, "");
                        }
                    }
                }
            }
        }
        protected void TxtEnter_TextChanged(object sender, EventArgs e)
        {

            string studentId = Request.Form[hfStudentId.UniqueID];
            if (studentId == string.Empty)
            {
                TxtEnter.Text = TxtEnter.Text.Replace("&", "/").Trim();
            }
            showDetails();
        }

        protected void DropDownMOD_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownMOD.SelectedValue == "Cheque")
            {
                divModeNo.Visible = true;
                divModeDate.Visible = true;
                divbankname.Visible = true;
                divStatus.Visible = true;
                Label42.Text = "Instrument No.";
                Label6.Text = "Instrument Date";
                Label.Text = "Issuer";

            }
            if (DropDownMOD.SelectedValue == "DD")
            {
                divModeNo.Visible = true;
                divModeDate.Visible = true;
                divbankname.Visible = true;
                divStatus.Visible = false;
                Label42.Text = "Instrument No.";
                Label6.Text = "Instrument Date";
                Label.Text = "Issuer";
            }
            if (DropDownMOD.SelectedValue == "Card")
            {
                divModeNo.Visible = true;
                divModeDate.Visible = true;
                divbankname.Visible = true;
                divStatus.Visible = false;
                Label42.Text = "Card No.";
                Label6.Text = "Transaction Date";
                Label.Text = "Issuer";
            }
            if (DropDownMOD.SelectedValue == "Online Transfer")
            {
                divModeNo.Visible = true;
                divModeDate.Visible = true;
                divbankname.Visible = true;
                divStatus.Visible = false;
                Label42.Text = "Ref. No.";
                Label6.Text = "Transaction Date";
                Label.Text = "Issuer";
            }
            if (DropDownMOD.SelectedValue == "Other")
            {
                divModeNo.Visible = true;
                divModeDate.Visible = true;
                divbankname.Visible = true;
                divStatus.Visible = true;
                Label42.Text = "Ref. No.";
                Label6.Text = "Transaction Date";
                Label.Text = "Reference Name";
            }

        }

        public override void Dispose()
        {
            _con.Dispose();
            _oo.Dispose();
        }

        protected void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            string disc = "0";
            if (txtDiscount.Text != "")
            {
                disc = txtDiscount.Text;
            }
            else
            {
                txtDiscount.Text = "0";
                disc = "0";
            }
            if (double.Parse(disc) > double.Parse(LblDueAmount.Text))
            {
                txtDiscount.Text = "0";
                disc = "0";
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox2, "Sorry, invalid discount amount!", "A");
            }
            string payableAmount = "0";
            payableAmount = (double.Parse(LblDueAmount.Text) - double.Parse(disc)).ToString("0");
            TextPay.Text = payableAmount;
            if (double.Parse(payableAmount) == 0)
            {
                //TextPay.Enabled = false;
                //btnSubmit.Visible = false;
            }
            else
            {
                //TextPay.Enabled = true;
                //btnSubmit.Visible = true;
            }
            lblNextDueAmt.Text = (double.Parse(LblDueAmount.Text) - (double.Parse(payableAmount) + double.Parse(txtDiscount.Text))).ToString("0");
        }
        protected void TextPay_TextChanged(object sender, EventArgs e)
        {
            string payamt = "0"; string disc = "0";
            if (txtDiscount.Text != "")
            {
                disc = txtDiscount.Text;
            }
            else
            {
                txtDiscount.Text = "0";
                disc = "0";
            }
            if (TextPay.Text != "")
            {
                payamt = TextPay.Text;
            }
            else
            {
                TextPay.Text = "0";
                payamt = "0";
            }
            if ((double.Parse(payamt) + double.Parse(disc)) > double.Parse(LblDueAmount.Text))
            {
                txtDiscount.Text = "0";
                TextPay.Text = LblDueAmount.Text;
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox2, "Sorry, invalid amount!", "A");
            }
            else
            {

                //payableAmount = (double.Parse(LblDueAmount.Text) - double.Parse(disc)).ToString("0");
                //TextPay.Text = payableAmount;
            }

            if (double.Parse(TextPay.Text) == 0)
            {
                btnSubmit.Visible = false;
                tr_BalAmt.Visible = false;
            }
            else
            {
                btnSubmit.Visible = true;
                tr_BalAmt.Visible = true;
            }
            lblNextDueAmt.Text = (double.Parse(LblDueAmount.Text) - (double.Parse(TextPay.Text) + double.Parse(disc))).ToString("0");
        }
        protected void link_Edit_Click(object sender, EventArgs e)
        {
            if (Session["CheckRefresh"].ToString() == ViewState["CheckRefresh"].ToString())
            {
                LinkButton link = (LinkButton)sender;
                Label lblId = (Label)link.NamingContainer.FindControl("Label14");

                _sql = "select Distinct Receipt_no ,convert(Varchar(11),other_fee_collection.FeeDepositeDate,106) as FeeDepositeDate,";
                _sql += " other_fee_collection.Srno,so.AdmissionForClassId,StudentGenaralDetail.Gender,";
                _sql += " StudentGenaralDetail.FirstName+' '+StudentGenaralDetail.MiddleName+' '+StudentGenaralDetail.LastName as StudentName";
                _sql += " ,SUM(other_fee_collection.Amount) as TotalPaid,DatePart(Year,other_fee_collection.FeeDepositeDate) Year,";
                _sql += " Left(DateName(Month,other_fee_collection.FeeDepositeDate),3) Month,DatePart(DD,other_fee_collection.FeeDepositeDate) Date  from other_fee_collection ";
                _sql += " inner join StudentGenaralDetail on StudentGenaralDetail.SrNo=other_fee_collection.Srno ";
                _sql += " inner join StudentOfficialDetails so on So.SrNo=other_fee_collection.Srno";
                _sql += " where other_fee_collection.Receipt_no='" + lblId.Text + "' and ";
                _sql += " other_fee_collection.SessionName='" + Session["SessionName"].ToString() + "' and";
                _sql += " StudentGenaralDetail.SessionName='" + Session["SessionName"].ToString() + "' and so.SessionName='" + Session["SessionName"].ToString() + "'";
                _sql += " Group By other_fee_collection.Receipt_no,other_fee_collection.Srno,other_fee_collection.FeeDepositeDate,";
                _sql += " StudentGenaralDetail.FirstName,StudentGenaralDetail.MiddleName,StudentGenaralDetail.LastName,so.AdmissionForClassId,StudentGenaralDetail.Gender ";
                Label44.Text = _oo.ReturnTag(_sql, "Srno");
                Label37.Text = _oo.ReturnTag(_sql, "Receipt_no");
                Label38.Text = _oo.ReturnTag(_sql, "StudentName");
                Label39.Text = _oo.ReturnTag(_sql, "FeeDepositeDate");
                Label10.Text = _oo.ReturnTag(_sql, "AdmissionForClassId");
                Label11.Text = _oo.ReturnTag(_sql, "Gender");
                DDYear0.Text = _oo.ReturnTag(_sql, "Year");
                DDMonth0.Text = _oo.ReturnTag(_sql, "Month");
                DDDate0.Text = _oo.ReturnTag(_sql, "Date");
                _sql = "Select Row_Number() Over(Order By Id) as Ids,HeadName,Id,Amount,Concession from Other_fee_Collection_head where";
                _sql += " SessionName='" + Session["SessionName"].ToString() + "' and Gender='" + Label11.Text + "' and ClassId='" + Label10.Text + "' Order By Id";
                GridView4.DataSource = _oo.GridFill(_sql);
                GridView4.DataBind();
                for (int i = 0; i < GridView4.Rows.Count; i++)
                {

                    Label Label41 = (Label)GridView4.Rows[i].FindControl("Label41");
                    Label Label45 = (Label)GridView4.Rows[i].FindControl("Label45");
                    Label Label9 = (Label)GridView4.Rows[i].FindControl("Label9");
                    Label lblRecevied0 = (Label)GridView4.Rows[i].FindControl("lblRecevied0");
                    TextBox TextBox2 = (TextBox)GridView4.Rows[i].FindControl("TextBox2");
                    TextBox txtConcession0 = (TextBox)GridView4.Rows[i].FindControl("txtConcession0");
                    _sql = "select Amount,HeadId,Id,Concession,ReceivedAmount from other_fee_collection where Receipt_no='" + lblId.Text + "' and HeadId='" + Label45.Text + "' and SessionName='" + Session["SessionName"].ToString() + "'";
                    if (_oo.ReturnTag(_sql, "Amount") != "")
                    {
                        Label9.Text = _oo.ReturnTag(_sql, "Id");
                        Label41.Text = _oo.ReturnTag(_sql, "HeadId");
                        TextBox2.Text = _oo.ReturnTag(_sql, "Amount");
                        txtConcession0.Text = _oo.ReturnTag(_sql, "Concession");
                        lblRecevied0.Text = _oo.ReturnTag(_sql, "ReceivedAmount");
                    }
                }
                count1();
                Permission_Values1(Label44.Text);
                ModalPopupExtender1.Show();
            }
            else
            {
                //oo.MessageBox("Please Do Not Press Refresh Button or back Button", this.Page);
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please Do Not Press Refresh Button or back Button", "W");

            }
        }
        public void count1()
        {
            if (GridView4.Rows.Count > 0)
            {
                try
                {
                    Label Label43 = (Label)GridView4.FooterRow.FindControl("Label43");
                    Label lblTotalConcession0 = (Label)GridView4.FooterRow.FindControl("lblTotalConcession0");
                    Label lblTotalReceived0 = (Label)GridView4.FooterRow.FindControl("lblTotalReceived0");
                    //Label lblRecevied = (Label)GridView1.Rows[i].FindControl("lblRecevied");
                    Double Sum1 = 0, Sum2 = 0, Sum3 = 0;
                    for (int i = 0; i < GridView4.Rows.Count; i++)
                    {
                        TextBox TextBox2 = (TextBox)GridView4.Rows[i].FindControl("TextBox2");
                        TextBox txtConcession0 = (TextBox)GridView4.Rows[i].FindControl("txtConcession0");
                        Label lblRecevied0 = (Label)GridView4.Rows[i].FindControl("lblRecevied0");
                        double concession = 0;
                        if (txtConcession0.Text != "")
                        {

                            concession = Convert.ToDouble(txtConcession0.Text);
                            Sum1 = Sum1 + concession;
                        }
                        else
                        {
                            concession = 0;
                            Sum1 = Sum1 + concession;
                        }
                        double amount = 0;
                        if (TextBox2.Text != "")
                        {

                            amount = Convert.ToDouble(TextBox2.Text);
                            Sum2 = Sum2 + amount;

                        }
                        else
                        {
                            amount = 0;
                            Sum2 = Sum2 + amount;
                        }
                        double receviedamount = amount - concession;
                        Sum3 = Sum3 + receviedamount;
                        lblRecevied0.Text = receviedamount.ToString(CultureInfo.InvariantCulture);
                    }
                    Label43.Text = Sum2.ToString(CultureInfo.InvariantCulture);
                    lblTotalConcession0.Text = Sum1.ToString(CultureInfo.InvariantCulture);
                    lblTotalReceived0.Text = Sum3.ToString(CultureInfo.InvariantCulture);
                }
                catch
                {

                }
            }
        }
        protected void View_Click(object sender, EventArgs e)
        {
            if (Session["CheckRefresh"].ToString() == ViewState["CheckRefresh"].ToString())
            {
                //Session["UniformReciept"] = Label34.Text;
                string ss = Label34.Text;
                Response.Redirect("UniformFeeReciept_duplicate.aspx?print=1&UniformReciept=" + ss);
            }
            else
            {
                //oo.MessageBox("Please Do Not Press Refresh Button or back Button", this.Page);
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please Do Not Press Refresh Button or back Button", "W");

            }
        }
        protected void link_Delete_Click(object sender, EventArgs e)
        {
            LinkButton link = (LinkButton)sender;
            Label lblId2 = (Label)link.NamingContainer.FindControl("Label13");
            lblvalue.Text = lblId2.Text;
            ModalPopupExtender2.Show();
            Button9.Focus();
        }
        protected void TextBox2_TextChanged(object sender, EventArgs e)
        {
            count1();
            ModalPopupExtender1.Show();
        }
        protected void txtConcession0_TextChanged(object sender, EventArgs e)
        {
            count1();
            ModalPopupExtender1.Show();
        }
        protected void Button5_Click(object sender, EventArgs e)
        {
            //Label Label1 = (Label)Grd.Rows[0].FindControl("Label1");
            //string Invoice_no = Invoice_no_generate();
            _con.Open();
            try
            {
                string fd, fd1 = "";
                fd = DDYear.SelectedItem.ToString() + "/" + DDMonth.SelectedItem.ToString() + "/" + DDDate.SelectedItem.ToString();
                fd1 = DDYear0.SelectedItem.ToString() + "/" + DDMonth0.SelectedItem.ToString() + "/" + DDDate0.SelectedItem.ToString();
                for (int i = 0; i < GridView4.Rows.Count; i++)
                {
                    Label Label41 = (Label)GridView4.Rows[i].FindControl("Label41");
                    Label Label45 = (Label)GridView4.Rows[i].FindControl("Label45");
                    Label lblRecevied0 = (Label)GridView4.Rows[i].FindControl("lblRecevied0");
                    TextBox TextBox2 = (TextBox)GridView4.Rows[i].FindControl("TextBox2");
                    TextBox txtConcession0 = (TextBox)GridView4.Rows[i].FindControl("txtConcession0");
                    Label Label9 = (Label)GridView4.Rows[i].FindControl("Label9");
                    _sql = "select COUNT(*) as Counter from Other_fee_collection where Receipt_no='" + Label37.Text + "' and HeadId='" + Label41.Text + "' and SessionName='" + Session["SessionName"].ToString() + "'";
                    if (Label41.Text == "")
                    {
                        SqlCommand Other_feecollections = new SqlCommand("Other_fee_collections_proc", _con);
                        Other_feecollections.CommandType = CommandType.StoredProcedure;
                        Other_feecollections.Parameters.AddWithValue("@Receipt_no", Label37.Text);
                        Other_feecollections.Parameters.AddWithValue("@Srno", Label44.Text);
                        Other_feecollections.Parameters.AddWithValue("@HeadId", Label45.Text);
                        Other_feecollections.Parameters.AddWithValue("@Amount", TextBox2.Text);
                        Other_feecollections.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                        Other_feecollections.Parameters.AddWithValue("@Date", fd);
                        Other_feecollections.Parameters.AddWithValue("@Id", "0");
                        Other_feecollections.Parameters.AddWithValue("@task", "Insert");
                        Other_feecollections.Parameters.AddWithValue("@Concession", txtConcession0.Text);
                        Other_feecollections.Parameters.AddWithValue("@ReceivedAmount", lblRecevied0.Text);
                        Other_feecollections.ExecuteNonQuery();
                    }
                    else
                    {
                        if (Label9.Text != "")
                        {
                            SqlCommand Other_feecollections = new SqlCommand("Other_fee_collections_proc", _con);
                            Other_feecollections.CommandType = CommandType.StoredProcedure;
                            Other_feecollections.Parameters.AddWithValue("@Receipt_no", Label37.Text);
                            Other_feecollections.Parameters.AddWithValue("@Srno", Label44.Text);
                            Other_feecollections.Parameters.AddWithValue("@HeadId", Label45.Text);
                            Other_feecollections.Parameters.AddWithValue("@Amount", TextBox2.Text);
                            Other_feecollections.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                            Other_feecollections.Parameters.AddWithValue("@Date", fd1);
                            Other_feecollections.Parameters.AddWithValue("@Id", Label9.Text);
                            Other_feecollections.Parameters.AddWithValue("@task", "Update");
                            Other_feecollections.Parameters.AddWithValue("@Concession", txtConcession0.Text);
                            Other_feecollections.Parameters.AddWithValue("@ReceivedAmount", lblRecevied0.Text);
                            Other_feecollections.ExecuteNonQuery();
                        }
                    }
                }
                //oo.MessageBox("Record Updated Successfully", this.Page);
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Record Updated Successfully", "S");

            }
            catch { }
            _con.Close();
            //Session["OtherReciept"] = Label37.Text;
            Response.Redirect("OtherFeesReciept_duplicate.aspx?print=1&OtherReciept=" + Label37.Text);
            //Grid_fill();
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            SqlCommand Other_feecollections = new SqlCommand();
            Other_feecollections.CommandText = "Other_fee_collections_proc";
            Other_feecollections.CommandType = CommandType.StoredProcedure;
            Other_feecollections.Connection = _con;
            Other_feecollections.Parameters.AddWithValue("@task", "Delete");
            Other_feecollections.Parameters.AddWithValue("@Receipt_no", lblvalue.Text);
            Other_feecollections.Parameters.AddWithValue("@Srno", "");
            Other_feecollections.Parameters.AddWithValue("@HeadId", "0");
            Other_feecollections.Parameters.AddWithValue("@Amount", "0");
            Other_feecollections.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
            Other_feecollections.Parameters.AddWithValue("@Date", "2015-02-16");
            Other_feecollections.Parameters.AddWithValue("@Concession", "0");
            Other_feecollections.Parameters.AddWithValue("@ReceivedAmount", "0");
            Other_feecollections.Parameters.AddWithValue("@Id", "1");
            Other_feecollections.Parameters.AddWithValue("@CheckDDNo", "N/A");
            Other_feecollections.Parameters.AddWithValue("@BankName", "N/A");
            Other_feecollections.Parameters.AddWithValue("@Status", "");
            Other_feecollections.Parameters.AddWithValue("@MOP", "");
            _con.Open();
            Other_feecollections.ExecuteNonQuery();
            _con.Close();
            //sql = "select Srno,Amount from Other_fee_collection where Receipt_no='" + lblvalue.Text + "' and SessionName='" + Session["SessionName"].ToString() + "'";
            //string Srno = oo.ReturnTag(sql, "Srno");
            //string Amount = oo.ReturnTag(sql, "Amount");
            //sql = "select FamilyContactNo from StudentFamilyDetails  where Srno='" + Srno + "'  and SessionName='" + Session["SessionName"].ToString() + "'";
            //string Conta = "";
            //Conta = oo.ReturnTag(sql, "FamilyContactNo");
            //SqlCommand delete_rec = new SqlCommand("Delete from Other_fee_collection where Receipt_no='" + lblvalue.Text + "' and SessionName='" + Session["SessionName"].ToString() + "'", con);
            //con.Open();
            //delete_rec.ExecuteNonQuery();
            //con.Close();
            //SendFeescancleSms(Conta, lblvalue.Text, Amount);
            //oo.MessageBox("Cancelled successfully", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Cancelled successfully", "S");

            Grid_fill();
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (DropDownMOD.SelectedValue != "Cash" && txtModeNo.Text == "")
            {
                txtModeNo.Focus();
                return;
            }
            if (DropDownMOD.SelectedIndex == 3)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "reset", "payWithPaystack();", true);
            }
            else
            {
                int totalqty = 0;
                foreach(GridViewRow gvr in GridView1.Rows)
                {
                    TextBox txtqty = (TextBox)gvr.FindControl("HeadQty");
                    totalqty += Convert.ToInt16(txtqty.Text);
                }
                if (totalqty > 0)
                {
                    SaveRecord();
                }
                else
                {
                    GridViewRow gvr = GridView1.Rows[0];
                    TextBox txtqty = (TextBox)gvr.FindControl("HeadQty");
                    txtqty.Focus();
                }
            }
        }
        protected void btnPayStack_Click(object sender, EventArgs e)
        {
            SaveRecord();
        }
        protected void SaveRecord()
        {
            string Invoice_no = _oo.FindRecieptNo();
            if (Invoice_no == "")
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Please Initilize receipt no.!", "A");
                return;
            }
            string recieptSrNo = "";

            string studentId = Request.Form[hfStudentId.UniqueID];
            if (studentId == string.Empty)
            {
                studentId = TxtEnter.Text.Trim();
            }
            Label Label1 = (Label)Grd.Rows[0].FindControl("Label1");
            Label lblClassId = (Label)Grd.Rows[0].FindControl("lblClassId");

            string fd = ""; string md = "";
            fd = DDYear.SelectedItem.ToString() + "/" + DDMonth.SelectedItem.ToString() + "/" + DDDate.SelectedItem.ToString();
            md = DDYearMode.SelectedItem.ToString() + "/" + DDMonthMode.SelectedItem.ToString() + "/" + DDDateMode.SelectedItem.ToString();
            _con.Open();
            SqlCommand Uniformfeecollections = new SqlCommand("USP_UniformFeeDeposit", _con);
            Uniformfeecollections.CommandType = CommandType.StoredProcedure;
            Uniformfeecollections.Parameters.AddWithValue("@Srno", studentId);
            //Uniformfeecollections.Parameters.AddWithValue("@Receipt_no", Invoice_no);
            Uniformfeecollections.Parameters.AddWithValue("@ClassId", lblClassId.Text);
            Uniformfeecollections.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
            Uniformfeecollections.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            Uniformfeecollections.Parameters.AddWithValue("@HeadTotalAmount", LblHeadTotalAmount.Text);
            Uniformfeecollections.Parameters.AddWithValue("@AmtForPay", LblDueAmount.Text);
            Uniformfeecollections.Parameters.AddWithValue("@Discount", txtDiscount.Text);
            Uniformfeecollections.Parameters.AddWithValue("@PaidAmt", TextPay.Text);
            Uniformfeecollections.Parameters.AddWithValue("@DepositDate", fd.ToString());
            Uniformfeecollections.Parameters.AddWithValue("@Mode", DropDownMOD.SelectedValue);
            _sql = "select * from UniformFeeDeposit where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and SrNo='" + studentId + "' and Mode='Cheque' and checkStatus='Cancelled' and id= (select top(1) id from UniformFeeDeposit where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and SrNo='" + studentId + "' order by id desc)";
            if (_oo.Duplicate(_sql))
            {
                Uniformfeecollections.Parameters.AddWithValue("@BounceCharge", fd.ToString());
            }
            if (DropDownMOD.SelectedValue == "Cash")
            {
                Uniformfeecollections.Parameters.AddWithValue("@PaymentSatus", "Paid");
            }
            if (DropDownMOD.SelectedValue == "Cheque")
            {
                Uniformfeecollections.Parameters.AddWithValue("@ModeNo", txtModeNo.Text);
                Uniformfeecollections.Parameters.AddWithValue("@ModeBank", txtbankname.Text);
                Uniformfeecollections.Parameters.AddWithValue("@PaymentSatus", DropDownStatus.SelectedValue);
                Uniformfeecollections.Parameters.AddWithValue("@checkStatus", DropDownStatus.SelectedValue);
                Uniformfeecollections.Parameters.AddWithValue("@ModeDate", md.ToString());
            }
            if (DropDownMOD.SelectedValue == "DD")
            {
                Uniformfeecollections.Parameters.AddWithValue("@ModeNo", txtModeNo.Text);
                Uniformfeecollections.Parameters.AddWithValue("@ModeBank", txtbankname.Text);
                Uniformfeecollections.Parameters.AddWithValue("@PaymentSatus", "Paid");
                Uniformfeecollections.Parameters.AddWithValue("@ModeDate", md.ToString());
            }
            if (DropDownMOD.SelectedValue == "Card")
            {
                Uniformfeecollections.Parameters.AddWithValue("@ModeNo", txtModeNo.Text);
                Uniformfeecollections.Parameters.AddWithValue("@ModeBank", txtbankname.Text);
                Uniformfeecollections.Parameters.AddWithValue("@PaymentSatus", "Paid");
                Uniformfeecollections.Parameters.AddWithValue("@ModeDate", md.ToString());
            }
            if (DropDownMOD.SelectedValue == "Online Transfer")
            {
                Uniformfeecollections.Parameters.AddWithValue("@ModeNo", txtModeNo.Text);
                Uniformfeecollections.Parameters.AddWithValue("@ModeBank", txtbankname.Text);
                Uniformfeecollections.Parameters.AddWithValue("@PaymentSatus", "Paid");
                Uniformfeecollections.Parameters.AddWithValue("@ModeDate", md.ToString());
            }
            if (DropDownMOD.SelectedValue == "Other")
            {
                Uniformfeecollections.Parameters.AddWithValue("@ModeNo", txtModeNo.Text);
                Uniformfeecollections.Parameters.AddWithValue("@ModeBank", txtbankname.Text);
                Uniformfeecollections.Parameters.AddWithValue("@PaymentSatus", DropDownStatus.SelectedValue);
                Uniformfeecollections.Parameters.AddWithValue("@ModeDate", md.ToString());
            }


            Uniformfeecollections.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
            Uniformfeecollections.Parameters.AddWithValue("@action", "save");

            SqlParameter outputValue = new SqlParameter("@result", "");
            outputValue.Size = 0x100;
            outputValue.Direction = ParameterDirection.Output;
            Uniformfeecollections.Parameters.Add(outputValue);
            Uniformfeecollections.ExecuteNonQuery();
            if (recieptSrNo == "")
            {
                recieptSrNo = Uniformfeecollections.Parameters["@result"].Value.ToString();
            }
            Uniformfeecollections.Parameters.Clear();

            //Session["UniformReciept"] = Invoice_no;
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                Label HeadName0 = (Label)GridView1.Rows[i].FindControl("Label32");
                TextBox HeadQty0 = (TextBox)GridView1.Rows[i].FindControl("HeadQty");
                Label HeadAmount0 = (Label)GridView1.Rows[i].FindControl("HeadAmount");
                bool flag = true;
                if (HeadQty0.Text == "" || HeadQty0.Text == "0")
                {
                    flag = false;
                }
                if (flag)
                {
                    SqlCommand UniformHistory = new SqlCommand("USP_UniformFeeDeposit", _con);
                    UniformHistory.CommandType = CommandType.StoredProcedure;
                    UniformHistory.Parameters.AddWithValue("@Srno", studentId);
                    UniformHistory.Parameters.AddWithValue("@Receipt_no", recieptSrNo);
                    UniformHistory.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                    UniformHistory.Parameters.AddWithValue("@HeadName", HeadName0.Text.Trim());
                    UniformHistory.Parameters.AddWithValue("@Qty", HeadQty0.Text.Trim());
                    UniformHistory.Parameters.AddWithValue("@Amount", HeadAmount0.Text);
                    UniformHistory.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                    UniformHistory.Parameters.AddWithValue("@action", "saveHistory");
                    UniformHistory.ExecuteNonQuery();
                }
            }


            _sql = "select FamilyContactNo from StudentFamilyDetails  where Srno='" + studentId + "'  and SessionName='" + Session["SessionName"].ToString() + "'";
            string Conta = "";
            Conta = _oo.ReturnTag(_sql, "FamilyContactNo");
            //SendFeesSms(Conta, recieptSrNo, double.Parse(TextPay.Text).ToString(CultureInfo.InvariantCulture));//Old
            ComposeSMS(recieptSrNo);
            Response.Redirect("UniformFeeReciept.aspx?print=1&UniformReciept=" + recieptSrNo);
            Grid_fill();

        }

        public void ComposeSMS(string recieptNo)
        {
            try
            {
                List<SqlParameter> param = new List<SqlParameter>()
            {
                new SqlParameter("@SessionName",Session["SessionName"]),
                new SqlParameter("@ReceiptNo",recieptNo),
                new SqlParameter("@BranchCode",Session["BranchCode"])
            };
                DataSet ds = _oo.ReturnDataSet("USP_ProductFeeTemplate", param.ToArray());
                if (ds != null && ds.Tables.Count > 0)
                {
                    string msg = SendSms(ds);
                }

            }
            catch
            {

            }

        }
        public string SendSms(DataSet ds)
        {
            string msg;
            try
            {
                DataTable data = new DataTable();
                data = ds.Tables[0];

                var fatherContactNo = data.Rows[0]["FatherContactNo"].ToString();

                DataTable template = new DataTable();
                template = ds.Tables[1];

                msg = template.Rows[0][0].ToString();
                string[] param = template.Rows[0][1].ToString().Split(',');

                string[] daynamicVariables = msg.Split(new char[0]);
                foreach (var para in param)
                {
                    string value = data.Rows[0][para].ToString();
                    for (int i = 0; i < daynamicVariables.Count(); i++)
                    {
                        if (daynamicVariables[i].ToString() == "{{{}}}")
                        {
                            daynamicVariables[i] = value;
                            break;
                        }
                    }
                }

                msg = string.Join(" ", daynamicVariables);

                SendFeesSms(fatherContactNo, msg, "7");

            }
            catch
            {
                msg = "";
            }
            return msg;
        }

        public string SendFeesSms(string fmobileNo, string msg, string smsPageId)
        {
            string res = "0";
            try
            {
                _sql = "Select HitValue from SMSActivateDeactivate where BranchCode=" + Session["BranchCode"] + "";
                if (_oo.ReturnTag(_sql, "HitValue") == "") return res;
                if (_oo.ReturnTag(_sql, "HitValue") != "true") return res;
                string sql1 = "Select SmsSent From SmsEmailMaster Where Id='\" + smsPageId + \"' and BranchCode='" + Session["BranchCode"] + "'";
                if (_oo.ReturnTag(sql1, "SmsSent").Trim() == "true")
                {
                    var sadpNew = new SMSAdapterNew();
                    if (fmobileNo == "") return "0";
                    sadpNew.Send(msg, fmobileNo, smsPageId);
                    res = "1";
                }
            }
            catch (Exception ex)
            {
                // ignored
            }

            return res;
        }


        protected void HeadQty_TextChanged(object sender, EventArgs e)
        {
            string studentId = Request.Form[hfStudentId.UniqueID];
            if (studentId == string.Empty)
            {
                studentId = TxtEnter.Text.Trim();
            }
            _sql = "select * from UniformFeeDeposit where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and SrNo='" + studentId + "' and Mode='Cheque' and checkStatus='Cancelled' and id= (select top(1) id from UniformFeeDeposit where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and SrNo='" + studentId + "' order by id desc)";
            if (_oo.Duplicate(_sql))
            {
                _sql = "select isnull(FineAmount, 0) FineAmount from ChequeBounceFineMaster where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"] + "";
                if (double.Parse(_oo.ReturnTag(_sql, "FineAmount")) > 0)
                {
                    divBounce.Visible = true;
                    BounceCharges.Text = _oo.ReturnTag(_sql, "FineAmount");
                }
                else
                {
                    divBounce.Visible = false;
                    BounceCharges.Text = "0";
                }
            }
            TextBox txt = (TextBox)sender;
            GridViewRow row = (GridViewRow)txt.NamingContainer;
            int idx = row.RowIndex;
            Label HeadAmount = (Label)GridView1.Rows[idx].FindControl("HeadAmount");
            TextBox HeadQty = (TextBox)GridView1.Rows[idx].FindControl("HeadQty");
            Label HeadTotal = (Label)GridView1.Rows[idx].FindControl("HeadTotal");
            if (HeadQty.Text == "")
            {
                HeadQty.Text = "0";
            }
            HeadTotal.Text = (double.Parse(HeadAmount.Text) * double.Parse(HeadQty.Text)).ToString("0.00");

            double Gtotal = 0;
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                Label HeadAmount0 = (Label)GridView1.Rows[i].FindControl("HeadAmount");
                TextBox HeadQty0 = (TextBox)GridView1.Rows[i].FindControl("HeadQty");
                Label HeadTotal0 = (Label)GridView1.Rows[i].FindControl("HeadTotal");
                Gtotal += (double.Parse(HeadAmount0.Text) * double.Parse(HeadQty0.Text));
            }
            Label TotalHeadAmount = (Label)GridView1.FooterRow.FindControl("TotalHeadAmount");
            TotalHeadAmount.Text = Gtotal.ToString("0.00");

            LblHeadTotalAmount.Text = TotalHeadAmount.Text;
            LblDueAmount.Text = (double.Parse(TotalHeadAmount.Text) + double.Parse(BounceCharges.Text)).ToString("0.00");
            TextPay.Text = (double.Parse(TotalHeadAmount.Text) + double.Parse(BounceCharges.Text)).ToString("0.00");
        }
    }
}