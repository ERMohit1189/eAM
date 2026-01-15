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
    public partial class OtherFeeDeposit : Page
    {
        private SqlConnection _con;
        readonly Campus _oo;
        private string _sql = "";
        public OtherFeeDeposit()
        {
            _con = new SqlConnection();
            _oo = new Campus();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            TxtEnter.Focus();
            _con = _oo.dbGet_connection();
            Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file

            if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
            {
                Response.Redirect("default.aspx");
            }

            if (!IsPostBack)
            {
                Session["CheckRefresh"] = Server.UrlDecode(DateTime.Now.ToString(CultureInfo.InvariantCulture));
                _oo.AddDateMonthYearDropDown(DDYear, DDMonth, DDDate);
                _oo.FindCurrentDateandSetinDropDown(DDYear, DDMonth, DDDate);
                _oo.AddDateMonthYearDropDown(DDYearMode, DDMonthMode, DDDateMode);
                _oo.FindCurrentDateandSetinDropDown(DDYearMode, DDMonthMode, DDDateMode);
                Tables.Visible = false;
            }
        }
        public void showDetails()
        {
            string studentId = Request.Form[hfStudentId.UniqueID];
            if (studentId == string.Empty)
            {
                studentId = TxtEnter.Text.Trim();
            }
            if (TxtEnter.Text.Trim() == "")
            {
                studentId = "";
            }


            if (studentId != "")
            {
                _sql = "select * from AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "', " + Session["BranchCode"].ToString() + ")";
                _sql += "   where  SrNo ='" + studentId + "'";
                _sql += "   and Withdrwal is null";

                Grd.DataSource = _oo.GridFill(_sql);
                Grd.DataBind();
                DataSet ds;
                ds = _oo.GridFill(_sql);
                // ReSharper disable once UseNullPropagation
                if (ds != null && Grd.Rows.Count > 0)
                {
                    Tables.Visible = true;
                    grdshow.Visible = true;
                    using (SqlConnection conn = new SqlConnection())
                    {
                        conn.ConnectionString = BAL.objBal.dbGet_connection().ConnectionString;
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.Connection = conn;
                            cmd.CommandText = "USP_StudentsPhotoReport";
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString().Trim());
                            cmd.Parameters.AddWithValue("@sessionName", Session["SessionName"].ToString().Trim());
                            cmd.Parameters.AddWithValue("@SrNo", studentId.ToString().Trim());
                            cmd.Parameters.AddWithValue("@action", "details");
                            SqlDataAdapter das = new SqlDataAdapter(cmd);
                            DataSet dsPhoto = new DataSet();
                            das.Fill(dsPhoto);
                            cmd.Parameters.Clear();


                            grdshow.Visible = true;
                            if (dsPhoto.Tables[0].Rows.Count > 0)
                            {
                                img.ImageUrl = dsPhoto.Tables[0].Rows[0]["PhotoPath"].ToString() != string.Empty ? dsPhoto.Tables[0].Rows[0]["PhotoPath"] + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture) : "../img/user-pic/user-pic.jpg" + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture);
                                studentImg.NavigateUrl = dsPhoto.Tables[0].Rows[0]["PhotoPath"].ToString() != string.Empty ? dsPhoto.Tables[0].Rows[0]["PhotoPath"] + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture) : "../img/user-pic/user-pic.jpg" + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture);
                                hylinkmoredetails.NavigateUrl = "../11/StudentRegView.aspx?print=1&id=" + ds.Tables[0].Rows[0]["stenrcode"];
                            }
                        }
                    }
                }
                else
                {
                    Grd.DataSource = null;
                    Grd.DataBind();
                    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, Record not found!", "A");
                    Tables.Visible = false;
                    grdshow.Visible = false;
                }
                if (Grd.Rows.Count > 0)
                {

                    Fill_value();
                    _sql = "select count(*) count from OtherFeeDeposit where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and SrNo='" + studentId + "' and PaymentSatus='Paid'";
                    if (_oo.ReturnTag(_sql, "count") != "0")
                    {
                        _sql = "select top(1) NextDeuAmt from OtherFeeDeposit where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and SrNo='" + studentId + "' and PaymentSatus='Paid' order by id desc";
                        if (_oo.Duplicate(_sql))
                        {
                            divSubmit.Visible = true;
                            LblHeadTotalAmount.Text = _oo.ReturnTag(_sql, "NextDeuAmt");
                            string sql = "select sum(BounceCharges) BounceCharges from (select isnull(sum(isnull(BounceCharges, 0)), 0) BounceCharges from OtherFeeDeposit where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and srno = '" + studentId + "' and PaymentSatus = 'Cancelled' union all select(isnull(sum(isnull(BounceCharges, 0)), 0) * (-1)) BounceCharges from OtherFeeDeposit where srno = '" + studentId + "' and PaymentSatus = 'Paid' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " )T1";
                            if (double.Parse(_oo.ReturnTag(sql, "BounceCharges")) > 0)
                            {
                                divChequeFine.Attributes.Add("class", "");
                                lblChequeFine.Text = _oo.ReturnTag(sql, "BounceCharges");
                            }
                            else
                            {
                                divChequeFine.Attributes.Add("class", "hide");
                                lblChequeFine.Text = "0.00";
                            }
                            txtDiscount.Text = "0.00";
                            txtPayable.Text = (double.Parse(LblHeadTotalAmount.Text) + double.Parse(lblChequeFine.Text)).ToString("0.00");
                            TextPay.Text = txtPayable.Text;
                            lblNextDueAmt.Text = "0.00";
                        }
                        else
                        {
                            divSubmit.Visible = false;
                        }
                    }
                    else
                    {
                        divSubmit.Visible = true;
                        Label lblClassId = (Label)Grd.Rows[0].FindControl("lblClassId");
                        Label lblGender = (Label)Grd.Rows[0].FindControl("lblGender");
                        _sql = "Select Sum(Amount)as Amount from OtherFeeHeadMaster where";
                        _sql += " SessionName='" + Session["SessionName"].ToString() + "' and Gender=(case when '" + lblGender.Text.Trim() + "'='Male' then 1 else case when '" + lblGender.Text.Trim() + "'='Female' then 2 else 3 end end)  and ClassId='" + lblClassId.Text + "' and BranchCode=" + Session["BranchCode"] + " group by id  Order By Id";
                        LblHeadTotalAmount.Text = double.Parse(_oo.ReturnTag(_sql, "Amount") == "" ? "0" : _oo.ReturnTag(_sql, "Amount")).ToString("0.00");
                        string sql = "select sum(BounceCharges) BounceCharges from (select isnull(sum(isnull(BounceCharges, 0)), 0) BounceCharges from OtherFeeDeposit where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and srno = '" + studentId + "' and PaymentSatus = 'Cancelled' union all select(isnull(sum(isnull(BounceCharges, 0)), 0) * (-1)) BounceCharges from OtherFeeDeposit where srno = '" + studentId + "' and PaymentSatus = 'Paid' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " )T1";
                        if (double.Parse(_oo.ReturnTag(sql, "BounceCharges")) > 0)
                        {
                            divChequeFine.Attributes.Add("class", "");
                            lblChequeFine.Text = _oo.ReturnTag(sql, "BounceCharges");
                        }
                        else
                        {
                            divChequeFine.Attributes.Add("class", "hide");
                            lblChequeFine.Text = "0.00";
                        }
                        txtDiscount.Text = "0.00";

                        txtPayable.Text = (double.Parse(LblHeadTotalAmount.Text) + double.Parse(lblChequeFine.Text)).ToString("0.00");
                        TextPay.Text = ((double.Parse(LblHeadTotalAmount.Text) + double.Parse(lblChequeFine.Text)) - double.Parse(txtDiscount.Text)).ToString("0.00");
                        lblNextDueAmt.Text = "0.00";
                        lblAmtName.Text = "Total Amount";
                    }
                    Grid_fill();
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
            _sql = "select top(1) PaymentSatus from OtherFeeDeposit where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and SrNo='" + studentId + "' order by id desc";
            if (_oo.ReturnTag(_sql, "PaymentSatus") == "Pending")
            {
                Tables.Visible = false;
            }
            _sql = "select top(1) PaymentSatus, NextDeuAmt from OtherFeeDeposit where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and SrNo='" + studentId + "' order by id desc";
            if (_oo.ReturnTag(_sql, "PaymentSatus") == "Paid" && double.Parse(_oo.ReturnTag(_sql, "NextDeuAmt")) == 0)
            {
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Fee submitted for this session!", "A");
                Tables.Visible = false;

            }
        }
        public void Fill_value()
        {
            if (Grd.Rows.Count > 0)
            {

                Label lblClassId = (Label)Grd.Rows[0].FindControl("lblClassId");
                Label lblGender = (Label)Grd.Rows[0].FindControl("lblGender");
                _sql = "Select Row_Number() Over(Order By Id) as Ids,HeadName,Id,Amount,'0' as Concession, ClassId, Gender from OtherFeeHeadMaster where";
                _sql += " SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"] + " and Gender=(case when '" + lblGender.Text.Trim() + "'='Male' then 1 else case when '" + lblGender.Text.Trim() + "'='Female' then 2 else 3 end end)  and ClassId='" + lblClassId.Text + "' Order By Id";
                GridView1.DataSource = _oo.GridFill(_sql);
                GridView1.DataBind();
                double totalG = 0;
                if (GridView1.Rows.Count > 0)
                {
                    Tables.Visible = true;
                    for (int i = 0; i < GridView1.Rows.Count; i++)
                    {
                        Label HeadAmount = (Label)GridView1.Rows[i].FindControl("HeadAmount");
                        totalG = totalG + double.Parse(HeadAmount.Text);
                    }
                    Label TotalHeadAmount = (Label)GridView1.FooterRow.FindControl("TotalHeadAmount");
                    TotalHeadAmount.Text = totalG.ToString("0.00");
                }
                else
                {
                    Tables.Visible = false;
                    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please create Other Fee Head!", "A");
                }
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


        public void Grid_fill()
        {
            try
            {


                Label lblClassId = (Label)Grd.Rows[0].FindControl("lblClassId");
                Label lblGender = (Label)Grd.Rows[0].FindControl("lblGender");
                _sql = "Select ROW_NUMBER() OVER(ORDER BY fd.id ASC) as ids, fd.Receipt_no, AmtForPay as PayableAmount, Discount, case when fd.PaymentSatus='Paid' then isnull(BounceCharges, '0.00') else 0 end BounceCharges, case when PaymentSatus='Cancelled' then 0.00 else PaidAmt end PaidAmt, NextDeuAmt, Mode, PaymentSatus, DepositDate  from OtherFeeDeposit fd";
                _sql += " where fd.SessionName = '" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"] + " and fd.ClassId = '" + lblClassId.Text + "' and fd.SrNo = '" + hfStudentId.Value + "' Order By fd.Id desc";
                var ds = _oo.GridFill(_sql);
                GridView2.DataBind();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    GridView2.Visible = true;
                    GridView2.DataSource = _oo.GridFill(_sql);
                    GridView2.DataBind();
                    for (int i = 0; i < GridView2.Rows.Count; i++)
                    {
                        Label lbl_sn = (Label)GridView2.Rows[i].FindControl("lbl_sn");
                        lbl_sn.Text = (GridView2.Rows.Count - i).ToString();
                        Label MOP = (Label)GridView2.Rows[i].FindControl("lbl_PaymentMode");
                        Label Status = (Label)GridView2.Rows[i].FindControl("lbl_PaymentSatus");
                        LinkButton LinkButton6 = (LinkButton)GridView2.Rows[i].FindControl("LinkButton6");
                        if (Status.Text.Trim() == "Pending" && (MOP.Text.Trim() == "Cheque" || MOP.Text.Trim() == "Other"))
                        {
                            GridView2.Rows[i].BackColor = System.Drawing.Color.OrangeRed;
                            GridView2.Rows[i].ForeColor = System.Drawing.Color.White;
                            LinkButton6.ForeColor = System.Drawing.Color.White;
                        }
                    }
                }
                else
                {
                    GridView2.Visible = false;
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public void Permission_Values()
        {
            string sql8;
            sql8 = "select Enable from Admin_fee_permission_setting where Tableid='8' and SessionName='" + Session["SessionName"].ToString() + "'";
            if (_oo.ReturnTag(sql8, "Enable") == "" || _oo.ReturnTag(sql8, "Enable") == "No,1,2,3" || _oo.ReturnTag(sql8, "Enable") == "Yes,1,2,3" || _oo.ReturnTag(sql8, "Enable") == "Yes,1" || _oo.ReturnTag(sql8, "Enable") == "Yes,2" || _oo.ReturnTag(sql8, "Enable") == "Yes,3" || _oo.ReturnTag(sql8, "Enable") == "Yes,1,2" || _oo.ReturnTag(sql8, "Enable") == "Yes,1,3" || _oo.ReturnTag(sql8, "Enable") == "Yes,2,3")
            {
                if (Grd.Rows.Count > 0)
                {
                    Label lbl_std_srno = (Label)Grd.Rows[0].FindControl("Label1");
                    _con.Open();
                    SqlCommand Permission_check = new SqlCommand("select count(*) from Administrator_Permission where Tableid='8' and SrNo='" + lbl_std_srno.Text + "' and Permission_Session='" + Session["SessionName"].ToString() + "' and Permission like 'Yes%'", _con);
                    int Permission_check_count = (int)Permission_check.ExecuteScalar();
                    _con.Close();

                    if (Permission_check_count == 1)
                    {

                        _sql = "select permission from Administrator_Permission where Tableid='8' and SrNo='" + lbl_std_srno.Text + "' and Permission_Session='" + Session["SessionName"].ToString() + "' and Permission like 'Yes%'";
                        string[] permission = _oo.ReturnTag(_sql, "permission").Split(',');
                        if (permission.Length > 1)
                        {
                            DDYear.Enabled = false;
                            DDMonth.Enabled = false;
                            DDDate.Enabled = false;
                            txtDiscount.Text = "0";
                            txtDiscount.Enabled = false;
                            TextPay.Enabled = false;

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
                                    TextPay.Enabled = true;
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
                                TextPay.Enabled = false;

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
                                            TextPay.Enabled = true;
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
                            TextPay.Enabled = false;

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
            Label lblClassId = (Label)Grd.Rows[0].FindControl("lblClassId");
            Label lblGender = (Label)Grd.Rows[0].FindControl("lblGender");
            LinkButton link = (LinkButton)sender;
            Label34.Text = link.Text;
            _sql = " select Receipt_no, Name+' '+combineclassname as name, DepositDate from AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "', " + Session["BranchCode"].ToString() + ") asr";
            _sql = _sql + " inner join OtherFeeDeposit fd on fd.ClassId = asr.ClassId and fd.SessionName = asr.SessionName and fd.BranchCode = asr.BranchCode ";
            _sql += " where asr.SrNo = '" + hfStudentId.Value + "' and asr.ClassId = " + lblClassId.Text + " and fd.SrNo = '" + hfStudentId.Value + "' and fd.ClassId = " + lblClassId.Text + " ";
            _sql += " and Receipt_no = '" + link.Text + "'";

            Labelname.Text = _oo.ReturnTag(_sql, "name");
            LabelDepositDate.Text = DateTime.Parse(_oo.ReturnTag(_sql, "DepositDate")).ToString("dd MMM yyyy hh:mm tt");

            _sql = "Select Row_Number() Over(Order By Id) as Ids,HeadName,Id,Amount,'0' as Concession, ClassId, Gender from OtherFeeHeadMaster where";
            _sql += " SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"] + " and Gender=(case when '" + lblGender.Text.Trim() + "'='Male' then 1 else case when '" + lblGender.Text.Trim() + "'='Female' then 2 else 3 end end)  and ClassId='" + lblClassId.Text + "' Order By Id";
            GridView3.DataSource = _oo.GridFill(_sql);
            GridView3.DataBind();

            Label TotalHeadAmount0 = (Label)GridView3.FooterRow.FindControl("TotalHeadAmount0");
            double Sum1 = 0;
            for (int i = 0; i < GridView3.Rows.Count; i++)
            {
                Label TextBox1 = (Label)GridView3.Rows[i].FindControl("lbl_HeadAmount");

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

        //			mess = "INR " + Amount + " received towards Other Fee Deposit. Receipt No. " + RecieptNo + "";
        //			string sms_response = "";

        //			if (FmobileNo != "")
        //			{
        //				_sql = "Select SmsSent From SmsEmailMaster where Id='17' ";
        //				if (_oo.ReturnTag(_sql, "SmsSent").Trim() == "true")
        //				{
        //					sms_response = sadpNew.Send(mess, FmobileNo, "17");
        //				}
        //			}
        //		}
        //	}
        //}
        protected void Page_PreRender(object sender, EventArgs e)
        {
            ViewState["CheckRefresh"] = Session["CheckRefresh"];
        }
        public void Permission_Values1(string srno)
        {
            string sql8;
            sql8 = "select Enable from Admin_fee_permission_setting where Tableid='8' and SessionName='" + Session["SessionName"].ToString() + "'";
            if (_oo.ReturnTag(sql8, "Enable") == "" || _oo.ReturnTag(sql8, "Enable") == "No,1,2,3" || _oo.ReturnTag(sql8, "Enable") == "Yes,1,2,3" || _oo.ReturnTag(sql8, "Enable") == "Yes,1" || _oo.ReturnTag(sql8, "Enable") == "Yes,2" || _oo.ReturnTag(sql8, "Enable") == "Yes,3" || _oo.ReturnTag(sql8, "Enable") == "Yes,1,2" || _oo.ReturnTag(sql8, "Enable") == "Yes,1,3" || _oo.ReturnTag(sql8, "Enable") == "Yes,2,3")
            {

                //Label lbl_std_srno = (Label)Grd.Rows[0].FindControl("Label1");
                _con.Open();
                SqlCommand Permission_check = new SqlCommand("select count(*) from Administrator_Permission where Tableid='8' and SrNo='" + srno + "' and Permission_Session='" + Session["SessionName"].ToString() + "' and Permission like 'Yes%'", _con);
                int Permission_check_count = (int)Permission_check.ExecuteScalar();
                _con.Close();

                if (Permission_check_count == 1)
                {

                    _sql = "select permission from Administrator_Permission where Tableid='8' and SrNo='" + srno + "' and Permission_Session='" + Session["SessionName"].ToString() + "' and Permission like 'Yes%'";
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

                        _sql = "Select SmsSent From SmsEmailMaster where Id='5' and BranchCode=" + Session["BranchCode"] + " ";
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
            if (DropDownMOD.SelectedIndex == 0)
            {
                divModeNo.Visible = false;
                divbankname.Visible = false;
                divStatus.Visible = false;
                divModeDate.Visible = false;
            }
            if (DropDownMOD.SelectedIndex == 1)
            {
                divModeNo.Visible = true;
                divbankname.Visible = true;
                divStatus.Visible = true;
                divModeDate.Visible = true;

                Label6.Text = "Instrument Date";
                Label42.Text = "Instrument No.";
                Label.Text = "Bank Name";

            }
            if (DropDownMOD.SelectedIndex == 2)
            {
                divModeNo.Visible = true;
                divbankname.Visible = true;
                divStatus.Visible = false;
                divModeDate.Visible = true;
                Label6.Text = "Instrument Date";
                Label42.Text = "Instrument No.";
                Label.Text = "Bank Name";

            }
            if (DropDownMOD.SelectedIndex == 3)
            {
                divModeNo.Visible = true;
                divbankname.Visible = true;
                divStatus.Visible = false;
                divModeDate.Visible = true;
                Label6.Text = "Transaction Date";
                Label42.Text = "Card No.";
                Label.Text = "Issuer";

            }
            if (DropDownMOD.SelectedIndex == 4)
            {
                divModeNo.Visible = true;
                divbankname.Visible = true;
                divStatus.Visible = false;
                divModeDate.Visible = true;
                Label6.Text = "Transaction Date";
                Label42.Text = "Ref. No.";
                Label.Text = "Issuer";

            }
            if (DropDownMOD.SelectedIndex == 5)
            {
                divModeNo.Visible = true;
                divbankname.Visible = true;
                divStatus.Visible = true;
                divModeDate.Visible = true;
                Label6.Text = "Transaction Date";
                Label42.Text = "Ref. No.";
                Label.Text = "Reference Name";
            }
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
                _sql += " inner join StudentGenaralDetail on StudentGenaralDetail.SrNo=other_fee_collection.Srno and StudentGenaralDetail.SessionName=other_fee_collection.SessionName and StudentGenaralDetail.BranchCode=other_fee_collection.BranchCode";
                _sql += " inner join StudentOfficialDetails so on So.SrNo=other_fee_collection.Srno  and So.SessionName=other_fee_collection.SessionName and So.BranchCode=other_fee_collection.BranchCode";
                _sql += " where other_fee_collection.Receipt_no='" + lblId.Text + "' and other_fee_collection.BranchCode=" + Session["BranchCode"] + " and ";
                _sql += " other_fee_collection.SessionName='" + Session["SessionName"].ToString() + "' ";
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
                _sql += " SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"] + " and Gender='" + Label11.Text + "' and ClassId='" + Label10.Text + "' Order By Id";
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
                    _sql = "select Amount,HeadId,Id,Concession,ReceivedAmount from other_fee_collection where Receipt_no='" + lblId.Text + "' and HeadId='" + Label45.Text + "' and SessionName='" + Session["SessionName"].ToString() + "'  and BranchCode=" + Session["BranchCode"] + " ";
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
                Session["IsDuplicate"] = "Yes";
                Session["OtherReciept"] = Label34.Text;
                Response.Redirect("OtherFeeReceipt.aspx?print=1");
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
                    _sql = "select COUNT(*) as Counter from Other_fee_collection where Receipt_no='" + Label37.Text + "' and HeadId='" + Label41.Text + "' and SessionName='" + Session["SessionName"].ToString() + "'  and BranchCode=" + Session["BranchCode"] + " ";
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
            Session["OtherReciept"] = Label37.Text;
            Response.Redirect("OtherFeesReciept_duplicate.aspx?print=1");
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

            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Cancelled successfully", "S");

            Grid_fill();
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string studentId = Request.Form[hfStudentId.UniqueID];
            if (studentId == string.Empty)
            {
                studentId = TxtEnter.Text.Trim();
            }
            Label Label1 = (Label)Grd.Rows[0].FindControl("Label1");
            Label lblClassId = (Label)Grd.Rows[0].FindControl("lblClassId");
            string Invoice_no = _oo.FindRecieptNo();
            if (Invoice_no == "")
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Please Initilize receipt no.!", "A");
                return;
            }
            else
            {
                string fd = ""; string md = "";
                fd = DDYear.SelectedItem.ToString() + "/" + DDMonth.SelectedItem.ToString() + "/" + DDDate.SelectedItem.ToString();
                md = DDYearMode.SelectedItem.ToString() + "/" + DDMonthMode.SelectedItem.ToString() + "/" + DDDateMode.SelectedItem.ToString();
                _con.Open();
                SqlCommand Other_feecollections = new SqlCommand("USP_OtherFeeDeposit", _con);
                Other_feecollections.CommandType = CommandType.StoredProcedure;
                Other_feecollections.Parameters.AddWithValue("@Srno", studentId);
                //Other_feecollections.Parameters.AddWithValue("@Receipt_no", Invoice_no);
                Other_feecollections.Parameters.AddWithValue("@ClassId", lblClassId.Text);
                Other_feecollections.Parameters.AddWithValue("@AmtForPay", LblHeadTotalAmount.Text.Trim());
                Other_feecollections.Parameters.AddWithValue("@Discount", txtDiscount.Text);
                Other_feecollections.Parameters.AddWithValue("@PaidAmt", TextPay.Text);
                double amt = (double.Parse(LblHeadTotalAmount.Text) + double.Parse(lblChequeFine.Text)) - double.Parse(txtDiscount.Text);

                Other_feecollections.Parameters.AddWithValue("@NextDeuAmt", (amt - double.Parse(TextPay.Text)).ToString("0.00"));
                Other_feecollections.Parameters.AddWithValue("@DepositDate", fd.ToString());
                Other_feecollections.Parameters.AddWithValue("@Mode", DropDownMOD.SelectedValue);

                if (DropDownMOD.SelectedValue == "Cheque" || DropDownMOD.SelectedValue == "Other")
                {
                    Other_feecollections.Parameters.AddWithValue("@ModeNo", txtModeNo.Text);
                    Other_feecollections.Parameters.AddWithValue("@CheckDate", md.ToString());
                    Other_feecollections.Parameters.AddWithValue("@ModeBank", txtbankname.Text);
                    Other_feecollections.Parameters.AddWithValue("@PaymentSatus", DropDownStatus.SelectedValue);
                    if (DropDownStatus.SelectedValue == "Paid")
                    {
                        Other_feecollections.Parameters.AddWithValue("@ChequeStatus", "Paid");
                    }
                    if (DropDownMOD.SelectedValue != "Other")
                    {
                        Other_feecollections.Parameters.AddWithValue("@ModeRemark", "");
                    }
                    else
                    {
                        Other_feecollections.Parameters.AddWithValue("@ModeRemark", txtRemark.Text);
                    }
                }
                if (DropDownMOD.SelectedValue != "Cheque" && DropDownMOD.SelectedValue != "Other" && DropDownMOD.SelectedValue != "Cash")
                {
                    Other_feecollections.Parameters.AddWithValue("@ModeNo", txtModeNo.Text);
                    Other_feecollections.Parameters.AddWithValue("@CheckDate", DBNull.Value);
                    Other_feecollections.Parameters.AddWithValue("@ModeBank", txtbankname.Text);
                    Other_feecollections.Parameters.AddWithValue("@PaymentSatus", "Paid");
                    Other_feecollections.Parameters.AddWithValue("@ChequeStatus", "Paid");
                }
                if (DropDownMOD.SelectedValue == "Cash")
                {
                    Other_feecollections.Parameters.AddWithValue("@ChequeStatus", "Paid");
                    Other_feecollections.Parameters.AddWithValue("@PaymentSatus", "Paid");
                }
                double BounceCharges = double.Parse(lblChequeFine.Text.Trim() == string.Empty ? "0" : lblChequeFine.Text.Trim());
                if (BounceCharges > 0)
                {
                    Other_feecollections.Parameters.AddWithValue("@BounceCharges", BounceCharges.ToString("0.00"));
                }
                Other_feecollections.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                Other_feecollections.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                Other_feecollections.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                Other_feecollections.Parameters.AddWithValue("@action", "save");

                SqlParameter outputValue = new SqlParameter("@result", "");
                outputValue.Size = 0x100;
                outputValue.Direction = ParameterDirection.Output;
                Other_feecollections.Parameters.Add(outputValue);

                Other_feecollections.ExecuteNonQuery();
                _con.Close();
                string recieptSrNo = Other_feecollections.Parameters["@result"].Value.ToString();
                //Session["OtherReciept"] = Invoice_no;
                _sql = "select FamilyContactNo from StudentFamilyDetails  where Srno='" + studentId + "'  and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"] + "";
                string Conta = "";
                Conta = _oo.ReturnTag(_sql, "FamilyContactNo");
                ComposeSMS(recieptSrNo);
                //SendFeesSms(Conta, recieptSrNo, double.Parse(TextPay.Text).ToString(CultureInfo.InvariantCulture));
                Session["IsDuplicate"] = "No";
                Response.Redirect("OtherFeeReceipt.aspx?print=1&OtherReciept=" + recieptSrNo, false);
            }
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
                DataSet ds = _oo.ReturnDataSet("USP_OtherFeeTemplate", param.ToArray());
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

                SendFeesSms(fatherContactNo, msg, "5");

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
        public override void Dispose()
        {
            _con.Dispose();
            _oo.Dispose();
        }

        protected void LinkButton6_Click(object sender, EventArgs e)
        {
            LinkButton link = (LinkButton)sender;
            LinkButton LinkButton6 = (LinkButton)link.NamingContainer.FindControl("LinkButton6");
            Session["IsDuplicate"] = "Yes";
            string ss = LinkButton6.Text;
            //Session["OtherReciept"] = LinkButton6.Text;
            Response.Redirect("OtherFeeReceipt.aspx?print=1&OtherReciept=" + ss);
        }
    }
}