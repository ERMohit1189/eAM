using c4SmsNew;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class CompositFeeDeposit_g : Page
{
	public SqlConnection _con = new SqlConnection();
	private readonly Campus _oo = new Campus();
	public SqlCommand cmd = new SqlCommand();
	private string _sql = "";
	public string surl, furl, service_provider, productinfo, key, salt, PayuBaseURL, HashSeq;
	public static bool isactive = false;
	bool Alldate = false, TuitionDiscount = false, TransportDiscount = false, HostelDiscount = false, OtherDiscount = false,
	TuitionPaid = false, TransportPaid = false, HostelPaid = false, OtherPaid = false, StrdentStatus = true;
	string txtremarks = "1";
	DataTable dtComplositFee = new DataTable();
	public void MakeConnection()
	{
		_con = new SqlConnection();
		try
		{
			cmd = new SqlCommand();
			_con = _oo.dbGet_connection();
			_con.Open();
		}
		catch { }
	}
	protected void Page_PreInIt(object sender, EventArgs e)
	{
		if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null || Session["Logintype"] == null)
		{
			Response.Redirect("~/default.aspx");
		}
		if (Session["Logintype"].ToString() == "Guardian" || Session["Logintype"].ToString() == "Student")
		{
		}
		else
		{
			Response.Redirect("~/default.aspx");
		}
		switch (Session["Logintype"].ToString())
		{
			case "Admin":
				MasterPageFile = "~/Master/admin_root-manager.master";
				break;
			case "Guardian":
				MasterPageFile = "~/sp/sp_root-manager.master";
				break;
			case "Student":
				MasterPageFile = "~/13/stuRootManager.master";
				break;
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null || Session["Logintype"] == null)
		{
			Response.Redirect("~/default.aspx");
		}
		_con = _oo.dbGet_connection();

		Campus camp = new Campus(); camp.LoadLoader(loader);
		if (!IsPostBack)
		{

			txtDepositDate.Text = _oo.ReturnTag("select format(getdate(),'dd-MMM-yyyy')dateCur", "dateCur");
			string studentId = Session["Srno"].ToString();
			string _sql2 = "select top(1) StartType from ReceiptNoStart where BranchCode=" + Session["BranchCode"] + " order by id desc";
			if (!_oo.Duplicate(_sql2))
			{
				mess.Visible = true;
				mess.Text = "Please initialize receipt no!";
				mess.Attributes.Add("class", "text-danger");
			}
			else
			{
				mess.Visible = false;
				mess.Text = "0.00";
				mess.Attributes.Add("class", "");
				string _sql1 = "select FirstName, LastName, ClassId, TypeOFAdmision,BranchId from AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") where SrNo='" + studentId + "'";
				string Classid = _oo.ReturnTag(_sql1, "ClassId"), AdmissionType = _oo.ReturnTag(_sql1, "TypeOFAdmision"), Branchid = _oo.ReturnTag(_sql1, "BranchId");
				string sqls = "select Classid from FeeAllotedForClassWise where Classid=" + Classid + " and AdmissionType='" + AdmissionType + "' and Branchid=" + Branchid + " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
				hdnFirstName.Value = _oo.ReturnTag(_sql1, "FirstName");
				hdnLastName.Value = _oo.ReturnTag(_sql1, "LastName");
				if (!_oo.Duplicate(sqls))
				{
					divTutionFee.Visible = false;
					_oo.msgbox(Page, msgs, "Fee not allotted for this class!", "A");
				}
				else
				{

					getGatway();
					BindStudentDetails(studentId);
				}
			}

		}

	}
	public void getGatway()
	{
		lnkSubmit.Visible = false;
		_sql = "select GateWayName GateWayValue, case when GateWayName='PayUMoney' then 'PayU' else GateWayName end GateWayName, paymentCharges, Logo from TblPaymentGateway where BranchCode=" + Session["BranchCode"].ToString() + " and GateWayFor='Fee' and Isactive=1 order by id asc";
		_oo.FillDropDown_withValue(_sql, ddlPaymentGateway, "GateWayName", "GateWayValue");
		ddlPaymentGateway.Items.Insert(0, new ListItem("<--Select Payment Gateway-->", ""));
		string chks = "select count(*) cnt from TblPaymentGateway where BranchCode=" + Session["BranchCode"].ToString() + " and GateWayFor='Fee' and Isactive=1 order by id asc";
		if (_oo.ReturnTag(chks, "cnt") != "0")
		{
			var dt = _oo.Fetchdata(_sql);
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				Session[dt.Rows[i]["GateWayValue"].ToString()] = dt.Rows[i]["Logo"].ToString() + "##" + dt.Rows[i]["paymentCharges"].ToString();
			}
			lnkSubmit.Visible = true;
		}
		_sql = "select LivePublicKey, Email, PseudoUniqueReference from TblPaymentGateway where BranchCode=" + Session["BranchCode"].ToString() + " and GateWayFor='Fee' and GateWayName='PayStack' and Isactive=1";
		if (_oo.Duplicate(_sql))
		{
			hdnTxtNos.Value = "";
			data_key.Text = _oo.ReturnTag(_sql, "LivePublicKey");
			data_email.Text = _oo.ReturnTag(_sql, "Email");
			data_PseudoUniqueReference.Text = _oo.ReturnTag(_sql, "PseudoUniqueReference");
		}
	}

	public void BindStudentDetails(string studentId)
	{

		mess.Text = "0.00"; divMess.Visible = false;

		_sql = "Select * from StudentOfficialDetails where blocked='Yes' and srno='" + studentId + "' and SessionName='" + Session["SessionName"] + "' and branchcode=" + Session["BranchCode"] + "";
		var sql1 = "Select Promotion,MODForFeeDeposit  from StudentOfficialDetails where srno='" + studentId + "' and SessionName='" + Session["SessionName"] + "' and branchcode=" + Session["BranchCode"] + "";
		var _sql2 = "select isnull(Withdrwal, '') Withdrwal from StudentOfficialDetails where srno='" + studentId + "' and SessionName='" + Session["SessionName"] + "' and branchcode=" + Session["BranchCode"] + "";
		var ds = BLL.BLLInstance.GetStudentDetails(studentId, Session["SessionName"].ToString(), Session["BranchCode"].ToString());

		if (ds.Tables[0].Rows.Count <= 0)
		{ btnPrint.Visible = false; }
		else { btnPrint.Visible = true; }

		if (ds != null && ds.Tables[0].Rows.Count > 0)
		{
			using (SqlConnection conn = new SqlConnection())
			{
				conn.ConnectionString = BAL.objBal.dbGet_connection().ConnectionString;
				using (SqlCommand cmd = new SqlCommand())
				{
					cmd.Connection = conn;
					cmd.CommandText = "USP_StudentsPhotoReport";
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("@sessionName", Session["SessionName"].ToString().Trim());
					cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString().Trim());
					cmd.Parameters.AddWithValue("@SrNo", studentId.ToString().Trim());

					cmd.Parameters.AddWithValue("@action", "details");
					SqlDataAdapter das = new SqlDataAdapter(cmd);
					DataSet dsPhoto = new DataSet();
					das.Fill(dsPhoto);
					cmd.Parameters.Clear();

					if (dsPhoto.Tables[0].Rows.Count > 0)
					{
						string ss = "select count(*) cnt from CompositFeeDeposit where SrNo='" + studentId + "' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and receiptStatus='Paid'";
						if (int.Parse(_oo.ReturnTag(ss, "cnt")) > 0)
						{
							chkCompleteFee.Visible = false;
							chkCompleteFee.Checked = false;
							chkCompleteFee.Enabled = false;
						}
						else
						{
							chkCompleteFee.Visible = true;
							chkCompleteFee.Checked = false;
							chkCompleteFee.Enabled = true;
						}
						GetTutionFeeDetails(studentId);
					}
				}
			}
		}

		if (_oo.Duplicate(_sql))
		{
			_sql = "Select blockedRemark from StudentOfficialDetails where srno='" + studentId + "' and SessionName='" + Session["SessionName"] + "' and branchcode=" + Session["BranchCode"] + "";
			var remark = _oo.ReturnTag(_sql, "blockedRemark");
			mess.Text = remark; divMess.Visible = true;
			divTutionFee.Visible = false;
			Campus camp = new Campus(); camp.msgbox(Page, msgs, remark, "A");
		}
		else if (_oo.ReturnTag(_sql2, "Withdrwal").Trim() != "")
		{
			var remark = "This Student has been Withdrawn!";
			mess.Text = remark; divMess.Visible = true;
			divTutionFee.Visible = false;

			Campus camp = new Campus(); camp.msgbox(Page, msgs, remark, "A");
		}
		else if (_oo.ReturnTag(sql1, "Promotion") == "Cancelled")
		{
			var remark = "Student Promotion has been cancelled, Please promote again from last session!";
			mess.Text = remark; divMess.Visible = true;
			divTutionFee.Visible = false;
			Campus camp = new Campus(); camp.msgbox(Page, msgs, remark, "A");
		}
		else
		{
			string sql = "Select Top 1 SessionName from SessionMaster Order by SessionId Desc";
			if (_oo.ReturnTag(sql, "SessionName") != Session["SessionName"].ToString())
			{

				sql = "Select SessionName from StudentOfficialDetails where srno='" + studentId + "' and SessionName=(Select Top 1 SessionName from SessionMaster Order by SessionId Desc) and Promotion is null";

				if (_oo.Duplicate(sql))
				{
					var remark = "Student promoted to session " + _oo.ReturnTag(sql, "SessionName") + " so you can not take fee in session " + Session["SessionName"].ToString() + "!";
					mess.Text = remark; divMess.Visible = true;
					divTutionFee.Visible = false;
					Campus camp = new Campus(); camp.msgbox(Page, msgs, remark, "A");
				}
			}

		}
		_sql = "Select Withdrwal From StudentOfficialDetails where SrNo='" + studentId + "' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
		if (_oo.ReturnTag(_sql, "Withdrwal") != "")
		{
			mess.Text = "This Student has been Withdrawn!"; divMess.Visible = true;
			divTutionFee.Visible = false;
			Campus camp = new Campus(); camp.msgbox(Page, msgs, "This Student has been Withdrawn!", "W");
		}
	}

	protected void GetTutionFeeDetails(string SrNo)
	{
		try
		{
			int isMinus = 0;
			rptFeeStructure.DataSource = null;
			rptFeeStructure.DataBind();
			using (SqlConnection conn = new SqlConnection())
			{
				conn.ConnectionString = BAL.objBal.dbGet_connection().ConnectionString;
				using (SqlCommand cmd = new SqlCommand())
				{
					string ss = "select id from CompositFeeDeposit where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and SrNo='" + SrNo.ToString().Trim() + "' and CompleteFeeeDiscount>0";
					if (_oo.Duplicate(ss))
					{
						chkCompleteFee.Checked = true;
					}
					cmd.Connection = conn;
					cmd.CommandText = "GetCompositFee";
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("@SrNo", SrNo.ToString().Trim());
					cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString().Trim());
					cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString().Trim());
					cmd.Parameters.AddWithValue("@PMtMode", "Online");
					cmd.Parameters.AddWithValue("@MODForFeeDeposit", chkCompleteFee.Checked == true ? "A" : "I");
					cmd.Parameters.AddWithValue("@completefeeCheck", (chkCompleteFee.Checked ? "1" : "0"));
					cmd.Parameters.AddWithValue("@Actions", "MainHead");
					SqlDataAdapter das = new SqlDataAdapter(cmd);
					DataTable dt = new DataTable();
					das.Fill(dt);
					cmd.Parameters.Clear();
					cmd.Connection.Close();
					if (dt.Rows.Count > 0)
					{
						divTutionFee.Visible = true;
						rptFeeStructure.DataSource = dt;
						rptFeeStructure.DataBind();
						double gtotal = 0; double dueRec = 0; int stsPending = 0;
						for (int i = 0; i < dt.Rows.Count; i++)
						{
							Label lblhideInstallmentDue = (Label)rptFeeStructure.Items[i].FindControl("lblhideInstallmentDue");
							Label lblStsMain = (Label)rptFeeStructure.Items[i].FindControl("lblStsMain");
							Label lblInstallmentDue = (Label)rptFeeStructure.Items[i].FindControl("lblInstallmentDue");
							Label lblrecuring = (Label)rptFeeStructure.Items[i].FindControl("lblrecuring");
							Label lblInstallmentDiscountPaid = (Label)rptFeeStructure.Items[i].FindControl("lblInstallmentDiscountPaid");
							Label lblInstallmentDiscount = (Label)rptFeeStructure.Items[i].FindControl("lblInstallmentDiscount");
							TextBox txtInstallmentPayable = (TextBox)rptFeeStructure.Items[i].FindControl("txtInstallmentPayable");
							TextBox txtInstallmentdiscount = (TextBox)rptFeeStructure.Items[i].FindControl("txtInstallmentdiscount");
							Label HeadDiscount = (Label)rptFeeStructure.Items[i].FindControl("HeadDiscount");
							Label lbInstallmentlPaid = (Label)rptFeeStructure.Items[i].FindControl("lbInstallmentlPaid");
							Label lblDueDate = (Label)rptFeeStructure.Items[i].FindControl("lblDueDate");
							Label lblhiDueHead = (Label)rptFeeStructure.Items[i].FindControl("lblhideInstallmentDue");
							Repeater RepeaterHeadAmon = (Repeater)rptFeeStructure.Items[i].FindControl("RepeaterHeadAmon");
							Repeater rptFee = (Repeater)rptFeeStructure.Items[i].FindControl("rptFee");
							CheckBox chkInstallment = (CheckBox)rptFeeStructure.Items[i].FindControl("chkInstallment");
							Repeater RepeaterHeadDiscount = (Repeater)rptFeeStructure.Items[i].FindControl("RepeaterHeadDiscount");
							Label lblInstallmentTotal = (Label)rptFeeStructure.Items[i].FindControl("lblInstallmentTotal");
							DateTime curdate = DateTime.Parse(_oo.ReturnTag("select getdate() curdate", "curdate"));


							dueRec = dueRec + double.Parse(dt.Rows[i]["DueAmount"].ToString());
							lblrecuring.Text = dueRec.ToString("0.00");
							if (double.Parse(lblInstallmentDiscount.Text) > 0)
							{
								HeadDiscount.Visible = true;
							}
							else
							{
								HeadDiscount.Visible = false;
							}
							if (double.Parse(lblInstallmentDiscount.Text == "" ? "0" : lblInstallmentDiscount.Text) > 0 || double.Parse(lblInstallmentDiscountPaid.Text == "" ? "0" : lblInstallmentDiscountPaid.Text) > 0)
							{
								HeadDiscount.Visible = true;
							}
							else
							{
								HeadDiscount.Visible = false;
							}
							if (double.Parse(lblhideInstallmentDue.Text) > 0 && DateTime.Parse(lblDueDate.Text) < DateTime.Parse(curdate.ToString("dd-MMM-yyyy")))
							{
								txtInstallmentPayable.Text = dt.Rows[i]["DueAmount"].ToString();
								lblInstallmentDue.Text = "0.00";
								chkInstallment.Checked = true;

							}
							if (double.Parse(lblInstallmentDiscount.Text) > double.Parse(lblInstallmentDiscountPaid.Text) && double.Parse(lblhideInstallmentDue.Text) == 0 && DateTime.Parse(lblDueDate.Text) < DateTime.Parse(curdate.ToString("dd-MMM-yyyy")))
							{
								txtInstallmentPayable.Text = dt.Rows[i]["DueAmount"].ToString();
								lblInstallmentDue.Text = "0.00";
								chkInstallment.Checked = true;
							}
							else if ((double.Parse(lblhideInstallmentDue.Text) > 0 || double.Parse(lblInstallmentDiscount.Text) > double.Parse(lblInstallmentDiscountPaid.Text)) && DateTime.Parse(lblDueDate.Text) > DateTime.Parse(curdate.ToString("dd-MMM-yyyy")))
							{
								txtInstallmentPayable.Text = "0.00";
								lblInstallmentDue.Text = dt.Rows[i]["DueAmount"].ToString();
								chkInstallment.Checked = false;
								txtInstallmentdiscount.Attributes.Add("readonly", "readonly");
								txtInstallmentPayable.Attributes.Add("readonly", "readonly");
							}
							else if (double.Parse(lblInstallmentTotal.Text) == double.Parse(lbInstallmentlPaid.Text) && double.Parse(lblhideInstallmentDue.Text) == 0)
							{
								txtInstallmentdiscount.Text = "0.00";
								txtInstallmentPayable.Text = "0.00";
								lblInstallmentDue.Text = "0.00";
								txtInstallmentdiscount.Attributes.Add("readonly", "readonly");
								txtInstallmentPayable.Attributes.Add("readonly", "readonly");
								chkInstallment.Checked = false;
								chkInstallment.Visible = false;
							}
							if ((double.Parse(lblhideInstallmentDue.Text) == 0 && double.Parse(lblInstallmentDiscount.Text) >= double.Parse(lblInstallmentDiscountPaid.Text)))
							{
								txtInstallmentdiscount.Attributes.Add("readonly", "readonly");
								txtInstallmentPayable.Attributes.Add("readonly", "readonly");
							}
							if ((double.Parse(lblhideInstallmentDue.Text) == 0 && double.Parse(lblInstallmentDiscount.Text) > double.Parse(lblInstallmentDiscountPaid.Text)))
							{
								chkInstallment.Checked = true;
								chkInstallment.Visible = true;
							}

							if (chkInstallment.Checked)
							{
								txtInstallmentPayable.Text = dt.Rows[i]["DueAmount"].ToString(); ;
								lblInstallmentDue.Text = "0.00";
							}
							else
							{
								txtInstallmentPayable.Text = "0.00";
								lblInstallmentDue.Text = dt.Rows[i]["DueAmount"].ToString();
							}
							if (chkCompleteFee.Checked)
							{
								txtInstallmentPayable.Text = dt.Rows[i]["DueAmount"].ToString(); ;
								lblInstallmentDue.Text = "0.00";
								txtInstallmentdiscount.Attributes.Add("readonly", "readonly");
								txtInstallmentPayable.Attributes.Add("readonly", "readonly");
								chkInstallment.Checked = true;
								chkInstallment.Visible = true;
								chkInstallment.Enabled = false;
							}
							gtotal = gtotal + double.Parse(txtInstallmentPayable.Text == "" ? "0" : txtInstallmentPayable.Text);
							rptFee.DataSource = null;
							rptFee.DataBind();
							using (SqlCommand cmd1 = new SqlCommand())
							{
								cmd1.Connection = conn;
								cmd1.CommandText = "GetCompositFee";
								cmd1.CommandType = CommandType.StoredProcedure;
								cmd1.Parameters.AddWithValue("@SrNo", SrNo.ToString().Trim());
								cmd1.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString().Trim());
								cmd1.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString().Trim());
								cmd1.Parameters.AddWithValue("@PMtMode", "Online");
								cmd1.Parameters.AddWithValue("@MODForFeeDeposit", chkCompleteFee.Checked == true ? "A" : "I");
								cmd1.Parameters.AddWithValue("@completefeeCheck", (chkCompleteFee.Checked ? "1" : "0"));
								cmd1.Parameters.AddWithValue("@InstallmentIdSet", dt.Rows[i]["MonthId"].ToString().Trim());
								cmd1.Parameters.AddWithValue("@Actions", "ChildHead");
								SqlDataAdapter da = new SqlDataAdapter(cmd1);
								DataSet ds = new DataSet();
								da.Fill(ds);
								cmd1.Parameters.Clear();
								if (ds.Tables[0].Rows.Count > 0)
								{
									RepeaterHeadAmon.DataSource = null;
									RepeaterHeadAmon.DataBind();
									if (ds.Tables[2].Rows.Count > 0)
									{
										RepeaterHeadAmon.DataSource = ds.Tables[2];
										RepeaterHeadAmon.DataBind();
									}

									RepeaterHeadDiscount.DataSource = null;
									RepeaterHeadDiscount.DataBind();
									if (ds.Tables[3].Rows.Count > 0)
									{
										RepeaterHeadDiscount.DataSource = ds.Tables[3];
										RepeaterHeadDiscount.DataBind();
									}
									rptFee.DataSource = ds.Tables[0];
									rptFee.DataBind();

									for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
									{
										CheckBox chkInstallmentFee = (CheckBox)rptFee.Items[j].FindControl("chkInstallmentFee");
										Label lblStsChild = (Label)rptFee.Items[j].FindControl("lblStsChild");
										Label lblInstallmentId = (Label)rptFee.Items[j].FindControl("lblInstallmentId");
										Label lblFeeheadId = (Label)rptFee.Items[j].FindControl("lblFeeheadId");
										Label lblFeeHeadBalance = (Label)rptFee.Items[j].FindControl("lblFeeHeadBalance");
										Label lblFeeHeadDiscountPaid = (Label)rptFee.Items[j].FindControl("lblFeeHeadDiscountPaid");
										Label lblFeeHeadDiscount = (Label)rptFee.Items[j].FindControl("lblFeeHeadDiscount");
										TextBox txtFeeHeadDiscount = (TextBox)rptFee.Items[j].FindControl("txtFeeHeadDiscount");
										TextBox txtFeeHeadPayable_h = (TextBox)rptFee.Items[j].FindControl("txtFeeHeadPayable_h");
										HtmlTableRow feeheadrow = (HtmlTableRow)rptFee.Items[j].FindControl("feeheadrow");
										Label ChildDiscount = (Label)rptFee.Items[j].FindControl("ChildDiscount");
										Label lblFeeHeadTotal = (Label)rptFee.Items[j].FindControl("lblFeeHeadTotal");
										Label lblFeeHeadPaid = (Label)rptFee.Items[j].FindControl("lblFeeHeadPaid");
										Label lblFeeHeadBalanceShow = (Label)rptFee.Items[j].FindControl("lblFeeHeadBalanceShow");
										Label lblisFineFeeApply = (Label)rptFee.Items[j].FindControl("lblisFineFeeApply");


										if (lblFeeHeadBalance.Text == "")
										{
											lblFeeHeadBalance.Text = "0.00";
										}
										string ss1 = "select receiptStatus from CompositFeeDeposit  where FeeHeadId=" + lblFeeheadId.Text + " and InstallmentId=" + lblInstallmentId.Text + " and SrNo='" + SrNo + "' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
										if (_oo.ReturnTag(ss1, "receiptStatus") == "Pending" && lblFeeHeadBalance.Text == "0.00")
										{
											lblStsMain.Text = "Pending";
											lblStsChild.Text = "Pending";
											stsPending = stsPending + 1;
										}
										else if (_oo.ReturnTag(ss1, "receiptStatus") != "Pending" && lblFeeHeadBalance.Text == "0.00")
										{
											lblStsMain.Text = "Paid";
											lblStsChild.Text = "Paid";

										}


										if (double.Parse(lblFeeHeadDiscount.Text == "" ? "0" : lblFeeHeadDiscount.Text) > 0 || double.Parse(lblFeeHeadDiscountPaid.Text == "" ? "0" : lblFeeHeadDiscountPaid.Text) > 0)
										{
											ChildDiscount.Visible = true;
										}
										else
										{
											ChildDiscount.Visible = false;
										}


										if ((double.Parse(lblFeeHeadBalance.Text == "" ? "0" : lblFeeHeadBalance.Text) > 0 || double.Parse(lblFeeHeadDiscount.Text) > double.Parse(lblFeeHeadDiscountPaid.Text)) && chkInstallment.Checked)
										{
											txtFeeHeadPayable_h.Text = ds.Tables[0].Rows[j]["DueAmount"].ToString();
											lblFeeHeadBalanceShow.Text = "0.00";
											chkInstallmentFee.Checked = true;
											if (lblisFineFeeApply.Text == "ApplyFine")
											{
												chkInstallmentFee.Enabled = false;
												txtFeeHeadPayable_h.Attributes.Add("readonly", "readonly");
											}
											else
											{
												chkInstallmentFee.Enabled = true;
											}

										}
										else if ((double.Parse(lblFeeHeadBalance.Text == "" ? "0" : lblFeeHeadBalance.Text) > 0 || double.Parse(lblFeeHeadDiscount.Text) > double.Parse(lblFeeHeadDiscountPaid.Text)) && !chkInstallment.Checked)
										{
											txtFeeHeadPayable_h.Text = "0.00";
											lblFeeHeadBalanceShow.Text = ds.Tables[0].Rows[j]["DueAmount"].ToString();
											chkInstallmentFee.Checked = true;
											chkInstallmentFee.Checked = false;
											txtFeeHeadDiscount.Attributes.Add("readonly", "readonly");
											if (lblisFineFeeApply.Text == "ApplyFine")
											{
												chkInstallmentFee.Enabled = false;
												txtFeeHeadPayable_h.Attributes.Add("readonly", "readonly");
											}
											else
											{
												chkInstallmentFee.Enabled = true;
											}

										}
										else if ((double.Parse(lblFeeHeadBalance.Text == "" ? "0" : lblFeeHeadBalance.Text) == 0 && double.Parse(lblFeeHeadDiscount.Text) == double.Parse(lblFeeHeadDiscountPaid.Text)))
										{
											lblFeeHeadBalanceShow.Text = "0.00";
											txtFeeHeadDiscount.Text = "0.00";
											txtFeeHeadPayable_h.Text = "0.00";
											txtFeeHeadDiscount.Attributes.Add("readonly", "readonly");
											txtFeeHeadPayable_h.Attributes.Add("readonly", "readonly");
											chkInstallmentFee.Checked = false;
											chkInstallmentFee.Visible = false;
										}
										if ((double.Parse(lblFeeHeadBalance.Text == "" ? "0" : lblFeeHeadBalance.Text) == 0 && double.Parse(lblFeeHeadDiscount.Text) >= double.Parse(lblFeeHeadDiscountPaid.Text)))
										{
											txtFeeHeadDiscount.Attributes.Add("readonly", "readonly");
											txtFeeHeadPayable_h.Attributes.Add("readonly", "readonly");
										}
										if ((double.Parse(lblFeeHeadBalance.Text == "" ? "0" : lblFeeHeadBalance.Text) == 0 && double.Parse(lblFeeHeadDiscount.Text) > double.Parse(lblFeeHeadDiscountPaid.Text)))
										{
											chkInstallmentFee.Checked = true;
											chkInstallmentFee.Visible = true;
											chkInstallmentFee.Attributes.Add("readonly", "readonly");
										}
										if (chkInstallmentFee.Checked)
										{
											txtFeeHeadPayable_h.Text = ds.Tables[0].Rows[j]["DueAmount"].ToString();
											lblFeeHeadBalanceShow.Text = "0.00";
										}
										else
										{
											txtFeeHeadPayable_h.Text = "0.00";
											lblFeeHeadBalanceShow.Text = ds.Tables[0].Rows[j]["DueAmount"].ToString();
										}
										if (chkCompleteFee.Checked)
										{
											txtFeeHeadPayable_h.Text = ds.Tables[0].Rows[j]["DueAmount"].ToString();
											lblFeeHeadBalanceShow.Text = "0.00";
											txtFeeHeadDiscount.Attributes.Add("readonly", "readonly");
											txtFeeHeadPayable_h.Attributes.Add("readonly", "readonly");
											chkInstallmentFee.Checked = true;
											chkInstallmentFee.Visible = true;
											chkInstallmentFee.Enabled = false;
										}
										if (stsPending > 0)
										{
											txtInstallmentPayable.Text = "0.00";
											txtInstallmentdiscount.Text = "0.00";
											txtInstallmentdiscount.Attributes.Add("readonly", "readonly");
											txtInstallmentPayable.Attributes.Add("readonly", "readonly");
											chkInstallment.Checked = false;
											chkInstallment.Visible = true;
											chkInstallment.Enabled = false;


											//txtFeeHeadPayable_h.Text = "0.00";
											txtFeeHeadDiscount.Text = "0.00";
											txtFeeHeadDiscount.Attributes.Add("readonly", "readonly");
											txtFeeHeadPayable_h.Attributes.Add("readonly", "readonly");
											chkInstallmentFee.Checked = false;
											chkInstallmentFee.Visible = true;
											chkInstallmentFee.Enabled = false;
										}
										if (!TuitionPaid)
										{
											txtInstallmentPayable.Attributes.Add("readonly", "readonly");
											txtFeeHeadPayable_h.Attributes.Add("readonly", "readonly");
										}
										if (!TuitionDiscount)
										{
											txtInstallmentdiscount.Attributes.Add("readonly", "readonly");
											txtFeeHeadDiscount.Attributes.Add("readonly", "readonly");
										}
										if (Session["Logintype"].ToString() == "Guardian" || Session["Logintype"].ToString() == "Student")
										{
											txtInstallmentPayable.Attributes.Add("readonly", "readonly");
											txtFeeHeadPayable_h.Attributes.Add("readonly", "readonly");
											txtInstallmentdiscount.Attributes.Add("readonly", "readonly");
											txtFeeHeadDiscount.Attributes.Add("readonly", "readonly");
										}
										if (!chkInstallment.Checked)
										{
											chkInstallmentFee.Checked = false;
											chkInstallmentFee.Enabled = false;
										}
										if (double.Parse(lblFeeHeadTotal.Text) < 0)
										{
											isMinus = isMinus + 1;
										}
										Label lblFeeheadId1 = (Label)rptFee.Items[j].FindControl("lblFeeheadId");
										Repeater RepeaterChildDiscount = (Repeater)rptFee.Items[j].FindControl("RepeaterChildDiscount");
										RepeaterChildDiscount.DataSource = null;
										RepeaterChildDiscount.DataBind();
										using (SqlCommand cmd2 = new SqlCommand())
										{
											cmd2.Connection = conn;
											cmd2.CommandText = "GetCompositFee";
											cmd2.CommandType = CommandType.StoredProcedure;
											cmd2.Parameters.AddWithValue("@SrNo", SrNo.ToString().Trim());
											cmd2.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString().Trim());
											cmd2.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString().Trim());
											cmd2.Parameters.AddWithValue("@PMtMode", "Online");
											cmd2.Parameters.AddWithValue("@MODForFeeDeposit", chkCompleteFee.Checked == true ? "A" : "I");
											cmd2.Parameters.AddWithValue("@completefeeCheck", (chkCompleteFee.Checked ? "1" : "0"));
											cmd2.Parameters.AddWithValue("@InstallmentIdSet", dt.Rows[i]["MonthId"].ToString().Trim());
											cmd2.Parameters.AddWithValue("@Feeheadid", lblFeeheadId1.Text.Trim());
											cmd2.Parameters.AddWithValue("@Actions", "ChildHeadDiscount");
											SqlDataAdapter da1s = new SqlDataAdapter(cmd2);
											DataTable dt1 = new DataTable();
											da1s.Fill(dt1);
											cmd2.Parameters.Clear();
											if (ds.Tables[0].Rows.Count > 0)
											{
												RepeaterChildDiscount.DataSource = dt1;
												RepeaterChildDiscount.DataBind();
											}
										}
									}
								}
								Repeater rptHistory = (Repeater)rptFeeStructure.Items[i].FindControl("rptHistory");
								rptHistory.DataSource = null;
								rptHistory.DataBind();
								if (ds.Tables[1].Rows.Count > 0)
								{
									rptHistory.DataSource = ds.Tables[1];
									rptHistory.DataBind();
									for (int k = 0; k < ds.Tables[1].Rows.Count; k++)
									{
										HyperLink HylnkReceiptNo = (HyperLink)rptHistory.Items[k].FindControl("HylnkReceiptNo");
										HylnkReceiptNo.NavigateUrl = "FeeReceiptAllDuplicate.aspx?RecieptSrNo=" + ds.Tables[1].Rows[k]["ReceiptNo"].ToString().Replace("/", "__") + "$" + ds.Tables[1].Rows[k]["SessionName"].ToString() + "$" + ds.Tables[1].Rows[k]["BranchCode"].ToString();


									}
								}

							}
						}
						if (stsPending > 0)
						{
							txtTotalPaid.Text = "0.00";
							hdnTotalPaid.Value = "0.00";

							lnkSubmit.Visible = false;
						}
						else
						{
							txtTotalPaid.Text = gtotal.ToString("0.00");
							hdnTotalPaid.Value = gtotal.ToString("0.00");

							lnkSubmit.Visible = true;
						}
						if (gtotal == 0)
						{
							//lnkSubmit.Visible = false;
						}
						ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ChangeRowColor();", true);

					}
					else
					{
						divTutionFee.Visible = false;
					}
					if (isMinus > 0)
					{

						lnkSubmit.Visible = false;
						Campus camp = new Campus(); camp.msgbox(Page, msgs, "Please check discount or amount seems nigative!", "A");
					}

				}
			}
			double InstallmentFee_total = 0; double Disvount_total = 0; double Total_total = 0; double Paid_total = 0; double Balance_total = 0;
			double Consolidated_total = 0;
			for (int i = 0; i < rptFeeStructure.Items.Count; i++)
			{
				Label lblInstallmentAmount = (Label)rptFeeStructure.Items[i].FindControl("lblInstallmentAmount");
				Label lblInstallmentDiscount = (Label)rptFeeStructure.Items[i].FindControl("lblInstallmentDiscount");
				Label lblInstallmentTotal = (Label)rptFeeStructure.Items[i].FindControl("lblInstallmentTotal");
				Label lbInstallmentlPaid = (Label)rptFeeStructure.Items[i].FindControl("lbInstallmentlPaid");
				Label lblInstallmentDue = (Label)rptFeeStructure.Items[i].FindControl("lblInstallmentDue");
				Label lblrecuring = (Label)rptFeeStructure.Items[i].FindControl("lblrecuring");
				InstallmentFee_total = InstallmentFee_total + double.Parse(lblInstallmentAmount.Text == "" ? "0" : lblInstallmentAmount.Text);
				Disvount_total = Disvount_total + double.Parse(lblInstallmentDiscount.Text == "" ? "0" : lblInstallmentDiscount.Text);
				Total_total = Total_total + double.Parse(lblInstallmentTotal.Text == "" ? "0" : lblInstallmentTotal.Text);
				Paid_total = Paid_total + double.Parse(lbInstallmentlPaid.Text == "" ? "0" : lbInstallmentlPaid.Text);
				Balance_total = Balance_total + double.Parse(lblInstallmentDue.Text == "" ? "0" : lblInstallmentDue.Text);
				if ((rptFeeStructure.Items.Count - 1) == i)
				{
					Consolidated_total = Consolidated_total + double.Parse(lblrecuring.Text == "" ? "0" : lblrecuring.Text);
				}
			}
			lblInstallmentFee_total.Text = InstallmentFee_total.ToString("0.00");
			lblDisvount_total.Text = Disvount_total.ToString("0.00");
			lblTotal_total.Text = Total_total.ToString("0.00");
			lblPaid_total.Text = Paid_total.ToString("0.00");
			lblBalance_total.Text = Balance_total.ToString("0.00");
			lblConsolidated_total.Text = Consolidated_total.ToString("0.00");
		}
		catch (SqlException ex)
		{
			throw ex;
		}
	}
	string txnId = "";
	protected void SubmitFee()
	{
		string dateOfDeposit = "";
		var studentId = Session["Srno"].ToString();
		string ss = "select getdate() dateOfDeposit";
		dateOfDeposit = DateTime.Parse(_oo.ReturnTag(ss, "dateOfDeposit")).ToString("dd-MMM-yyyy");
		string RecieptNo = _oo.FindRecieptNo();
		Session["RecieptSrNo"] = RecieptNo;

		if (RecieptNo.Trim() == "")
		{
			Campus camp = new Campus(); camp.msgbox(this.Page, msgs, "Please initialize receipt no!", "A");
			return;
		}
		double payableAmt = double.Parse(hdnTotalPaid.Value == "" ? "0" : hdnTotalPaid.Value);

		dtComplositFee.Columns.Add("SrNo", typeof(string));
		dtComplositFee.Columns.Add("ReceiptNo", typeof(string));
		dtComplositFee.Columns.Add("InstallmentId", typeof(string));
		dtComplositFee.Columns.Add("FeeHeadId", typeof(string));
		dtComplositFee.Columns.Add("HeadAmount", typeof(string));
		dtComplositFee.Columns.Add("OnpageDisc", typeof(string));
		dtComplositFee.Columns.Add("ManualDiscount", typeof(string));
		dtComplositFee.Columns.Add("SpecialDisc", typeof(string));
		dtComplositFee.Columns.Add("SiblingDisc", typeof(string));
		dtComplositFee.Columns.Add("CompleteFeeDisc", typeof(string));
		dtComplositFee.Columns.Add("PayableAmount", typeof(string));
		dtComplositFee.Columns.Add("PaidAmount", typeof(string));
		dtComplositFee.Columns.Add("BalanceAmount", typeof(string));
		dtComplositFee.Columns.Add("DepositDate", typeof(string));
		dtComplositFee.Columns.Add("ModeOfPayment", typeof(string));
		dtComplositFee.Columns.Add("InstrumentDate", typeof(string));
		dtComplositFee.Columns.Add("InstrumentNo", typeof(string));
		dtComplositFee.Columns.Add("InstrumentStatus", typeof(string));
		dtComplositFee.Columns.Add("BankName", typeof(string));
		dtComplositFee.Columns.Add("receiptStatus", typeof(string));
		dtComplositFee.Columns.Add("SessionName", typeof(string));
		dtComplositFee.Columns.Add("BranchCode", typeof(string));
		dtComplositFee.Columns.Add("LoginName", typeof(string));
		dtComplositFee.Columns.Add("GateWayName", typeof(string));
		dtComplositFee.Columns.Add("TxnID", typeof(string));
		dtComplositFee.Columns.Add("GateWayTxnId", typeof(string));
		dtComplositFee.Columns.Add("Narration", typeof(string));

		if (payableAmt > 0)
		{
			for (int p = 0; p < rptFeeStructure.Items.Count; p++)
			{
				Repeater rptFee = (Repeater)rptFeeStructure.Items[p].FindControl("rptFee");
				for (int i = 0; i < rptFee.Items.Count; i++)
				{
					Label lblInstallmentName = (Label)rptFeeStructure.Items[p].FindControl("lblInstallmentName");
					Label lblFeehead = (Label)rptFee.Items[i].FindControl("lblFeehead");
					Label lblInstallmentId = (Label)rptFee.Items[i].FindControl("lblInstallmentId");
					Label lblFeeheadId = (Label)rptFee.Items[i].FindControl("lblFeeheadId");
					Label lblFeeheadAmount = (Label)rptFee.Items[i].FindControl("lblFeeheadAmount");

					Label lblFeeHeadDiscount = (Label)rptFee.Items[i].FindControl("lblFeeHeadDiscount");
					Label lblFeeHeadDiscountPaid = (Label)rptFee.Items[i].FindControl("lblFeeHeadDiscountPaid");
					TextBox txtFeeHeadDiscount = (TextBox)rptFee.Items[i].FindControl("txtFeeHeadDiscount");

					Label lblFeeHeadTotal = (Label)rptFee.Items[i].FindControl("lblFeeHeadTotal");
					Label lblFeeHeadPaid = (Label)rptFee.Items[i].FindControl("lblFeeHeadPaid");
					TextBox txtFeeHeadPayable_h = (TextBox)rptFee.Items[i].FindControl("txtFeeHeadPayable_h");

					Label lblFeeHeadBalance = (Label)rptFee.Items[i].FindControl("lblFeeHeadBalance");
					Label lblFeeHeadBalanceShow = (Label)rptFee.Items[i].FindControl("lblFeeHeadBalanceShow");

					CheckBox chkInstallment = (CheckBox)rptFeeStructure.Items[p].FindControl("chkInstallment");
					CheckBox chkInstallmentFee = (CheckBox)rptFee.Items[i].FindControl("chkInstallmentFee");
					if (chkInstallment.Checked && chkInstallmentFee.Checked)
					{
						double OnpageDiscount = 0;
						double ManualDiscount = 0;
						double GenderDiscount = 0;
						double SiblingDiscount = 0;
						double CompleteFeeDiscount = 0;
						Repeater RepeaterChildDiscount = (Repeater)rptFee.Items[i].FindControl("RepeaterChildDiscount");
						for (int j = 0; j < RepeaterChildDiscount.Items.Count; j++)
						{
							Label lblDiscNameC = (Label)RepeaterChildDiscount.Items[j].FindControl("lblDiscNameC");
							Label lblDiscAmtC = (Label)RepeaterChildDiscount.Items[j].FindControl("lblDiscAmtC");
							Label lblPaidDiscAmtC = (Label)RepeaterChildDiscount.Items[j].FindControl("lblPaidDiscAmtC");

							OnpageDiscount = double.Parse(txtFeeHeadDiscount.Text == "" ? "0.00" : txtFeeHeadDiscount.Text);
							if (lblDiscNameC.Text == "Manual Discount")
							{
								ManualDiscount = double.Parse(lblDiscAmtC.Text);
							}
							if (lblDiscNameC.Text == "Special Discount")
							{
								GenderDiscount = double.Parse(lblDiscAmtC.Text);
							}
							if (lblDiscNameC.Text == "Sibling Discount")
							{
								SiblingDiscount = double.Parse(lblDiscAmtC.Text);
							}
							if (lblDiscNameC.Text == "Complete Fee Discount")
							{
								CompleteFeeDiscount = double.Parse(lblDiscAmtC.Text);
							}
						}

						DataRow row = dtComplositFee.NewRow();
						row["SrNo"] = studentId;
						row["ReceiptNo"] = RecieptNo;
						row["InstallmentId"] = lblInstallmentId.Text;
						row["FeeHeadId"] = lblFeeheadId.Text;
						string mainpa = "0";
						double totaldisc = 0;
						string ss1 = "select sum(ManualDiscount) manuals, sum(SpecialDisc) SpecialDisc, sum(SiblingDisc) SiblingDisc, sum(CompleteFeeDisc) CompleteFeeDisc from CompositFeeDeposit where FeeHeadId=" + lblFeeheadId.Text + " and InstallmentId=" + lblInstallmentId.Text + " and SrNo='" + studentId + "' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and receiptStatus<>'Cancelled'";
						if (_oo.Duplicate(ss1))
						{
							double manual = double.Parse(_oo.ReturnTag(ss1, "manuals"));
							totaldisc = (double.Parse(txtFeeHeadDiscount.Text == "" ? "0" : txtFeeHeadDiscount.Text) + (ManualDiscount - manual));
							ManualDiscount = (ManualDiscount - manual);
							mainpa = lblFeeHeadBalance.Text;
							GenderDiscount = 0;
							SiblingDiscount = 0;
							CompleteFeeDiscount = 0;
							if (double.Parse(mainpa) < totaldisc)
							{
								mainpa = (double.Parse(lblFeeheadAmount.Text) - double.Parse(lblFeeHeadPaid.Text)).ToString("0.00");
							}
						}
						else
						{
							row["HeadAmount"] = lblFeeheadAmount.Text;
							mainpa = lblFeeheadAmount.Text;
							totaldisc = (double.Parse(txtFeeHeadDiscount.Text == "" ? "0" : txtFeeHeadDiscount.Text) + ManualDiscount + GenderDiscount + SiblingDiscount + CompleteFeeDiscount);
						}

						double payle = (double.Parse(mainpa) - totaldisc);
						row["HeadAmount"] = double.Parse(mainpa).ToString("0.00");
						row["OnpageDisc"] = (txtFeeHeadDiscount.Text == "" ? "0" : txtFeeHeadDiscount.Text);
						row["ManualDiscount"] = ManualDiscount.ToString("0.00");
						row["SpecialDisc"] = GenderDiscount.ToString("0.00");
						row["SiblingDisc"] = SiblingDiscount.ToString("0.00");
						row["CompleteFeeDisc"] = CompleteFeeDiscount.ToString("0.00");
						row["PayableAmount"] = payle.ToString("0.00");
						row["PaidAmount"] = double.Parse(txtFeeHeadPayable_h.Text).ToString("0.00");
						row["BalanceAmount"] = (payle > 0 ? (payle - double.Parse(txtFeeHeadPayable_h.Text)).ToString("0.00") : "0.00");
						row["DepositDate"] = dateOfDeposit;
						row["ModeOfPayment"] = "Online";
						row["InstrumentDate"] = dateOfDeposit;
						row["InstrumentNo"] = "";
						row["InstrumentStatus"] = "";
						row["BankName"] = "";
						row["InstrumentStatus"] = "";
						row["receiptStatus"] = "Paid";
						row["SessionName"] = Session["SessionName"].ToString();
						row["BranchCode"] = Session["BranchCode"].ToString();
						row["LoginName"] = Session["LoginName"].ToString();
						row["GateWayName"] = "";
						row["TxnID"] = "";
						row["GateWayTxnId"] = "";
						row["Narration"] = "";
						dtComplositFee.Rows.Add(row);
					}
				}
			}
		}
	}
	protected void lnkSubmit_Click(object sender, EventArgs e)
	{
		var studentId = Session["Srno"].ToString();
		double total = 0;
		double total2 = 0;
		double.TryParse(hdnTotalPaid.Value, out total);
		double.TryParse(txtTotalPaid.Text, out total2);
		if (total == 0 && total2 == 0)
		{
			Campus camp = new Campus(); camp.msgbox(Page, msgs, "Total Payable is Ziro(0) amount.", "A");
			return;
		}
		if (ddlPaymentGateway.SelectedValue == "PayUMoney")
		{
			SubmitFee();
			var rnd = new Random();
			var hashStr = APIConfig.APIConfigInstance.GenerateHash512(rnd + DAL.DALInstance.GetDateTimeForHax());
			try
			{ txnId = hashStr.Substring(0, 20); }
			catch { }
			using (SqlCommand cmd = new SqlCommand())
			{
				cmd.CommandText = "SetAllCompositFeeTempProc";
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Connection = _con;
				cmd.Parameters.AddWithValue("@TableTypeComplositFee", dtComplositFee);
				cmd.Parameters.AddWithValue("@TxnID", txnId);
				cmd.Parameters.AddWithValue("@GateWayName", "PayUMoney");
				try
				{
					_con.Open();
					int RowsEffected = cmd.ExecuteNonQuery();
					cmd.Parameters.Clear();
					_con.Close();
					if (RowsEffected > 0)
					{
						SetTransaction(txnId, hashStr, studentId, Session["SessionName"].ToString(), Convert.ToDecimal(hdnTotalPaid.Value), ddlPaymentGateway.SelectedValue, "Fee", int.Parse(Session["BranchCode"].ToString()), Session["Logintype"].ToString(), Session["LoginName"].ToString(), Session["ImageUrl"].ToString());
					}
					else
					{
						Campus camp = new Campus(); camp.msgbox(Page, msgs, "Some technical problems have occurred", "A");
					}
				}
				catch (Exception ex)
				{
					ex.Message.ToString();
					// ignored
				}
			}
		}
		if (ddlPaymentGateway.SelectedValue == "Eazypay")
		{
			try
			{
				string sql = "select ReferenceNo, SubMerchantId, Isactive from TblPaymentGateway where BranchCode=" + Session["BranchCode"] + " and GateWayFor='Fee' and Isactive=1 and GateWayName='Eazypay'";
				string ReferenceNop = _oo.ReturnTag(sql, "ReferenceNo");
				string SubMerchantIdp = _oo.ReturnTag(sql, "SubMerchantId");
				string Isactive = _oo.ReturnTag(sql, "Isactive");
				SubmitFee();
				using (SqlCommand cmd = new SqlCommand())
				{
					cmd.CommandText = "SetAllCompositFeeTempProc";
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Connection = _con;
					cmd.Parameters.AddWithValue("@TableTypeComplositFee", dtComplositFee);
					cmd.Parameters.AddWithValue("@TxnID", ReferenceNop);
					cmd.Parameters.AddWithValue("@GateWayName", "EasyPay");
					try
					{
						_con.Open();
						cmd.ExecuteNonQuery();
						cmd.Parameters.Clear();
						_con.Close();
						using (SqlCommand cmd2 = new SqlCommand())
						{
							cmd2.CommandText = "UPDATE dbo.TblPaymentGateway SET ReferenceNo = ReferenceNo+1,  SubMerchantId=SubMerchantId+1 where BranchCode=" + Session["BranchCode"] + " and Isactive=1 and GateWayName='Eazypay'";
							cmd2.CommandType = CommandType.Text;
							cmd2.Connection = _con;
							try
							{
								_con.Open();
								cmd2.ExecuteNonQuery();
								_con.Close();
								Session["TxnIDWithSRNO"] = (ReferenceNop + "##" + studentId).ToString();
								if (Isactive == "1" || Isactive.ToLower() == "true")
								{
									Session["PaymentFor"] = "PaymentForFee";
									Session["EasyPayTxnId"] = ReferenceNop;
									SetICICITransaction(ReferenceNop, SubMerchantIdp, studentId, Session["SessionName"].ToString(), Convert.ToDecimal(hdnTotalPaid.Value), int.Parse(Session["BranchCode"].ToString()), "Fee", Page);
								}
							}
							catch (Exception)
							{
								throw;
							}
						}
					}
					catch (Exception ex)
					{
						_oo.msgbox(Page, msgbox, "Some technical problems have occurred", "A");
					}
				}
			}
			catch (Exception ex)
			{
				Campus camp1 = new Campus(); camp1.msgbox(Page, msgbox, "Please Update Eazypay Gateway Setting!", "A");
			}
		}
		if (ddlPaymentGateway.SelectedValue == "Atom")
		{
			try
			{
				string _sql1 = "select Name, FamilyContactNo,FamilyEmail  from AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") where SrNo='" + studentId + "'";
				string MobileNo = _oo.ReturnTag(_sql1, "FamilyContactNo");
				string EmailId = _oo.ReturnTag(_sql1, "FamilyEmail");

				string sql = "select MarchentKEY, Marchentsalt, ProductInfo, BaseURL, HashSequence,SuccessURL, Isactive from TblPaymentGateway where BranchCode=" + Session["BranchCode"] + " and GateWayFor='Fee' and Isactive=1 and GateWayName='Atom'";
				string MarchentId = _oo.ReturnTag(sql, "MarchentKEY");
				string UserId = _oo.ReturnTag(sql, "Marchentsalt");
				string Password = _oo.ReturnTag(sql, "ProductInfo");
				string SchoolEmailId = _oo.ReturnTag(sql, "HashSequence");
				string ResponseUrl = _oo.ReturnTag(sql, "SuccessURL");
				string Isactive = _oo.ReturnTag(sql, "Isactive");
				Request rq = new Request();
				string merchTxnIds = RandomString(10);
				string AtomTokenId = rq.GetToken(MarchentId, UserId, Password, EmailId, MobileNo, total.ToString(), SchoolEmailId, merchTxnIds.ToLower());
				if (!String.IsNullOrEmpty(AtomTokenId) && Isactive.ToLower() == "true")
				{
					hdn_atomTokenId.Value = AtomTokenId;
					hdn_merchTxnIds.Value = merchTxnIds;
					hdn_merchId.Value = MarchentId;
					hdn_custEmail.Value = EmailId == "" ? SchoolEmailId : EmailId;
					hdn_custMobile.Value = MobileNo;
					hdn_returnUrl.Value = ResponseUrl;
					SubmitFee();
					using (SqlCommand cmd = new SqlCommand())
					{

						cmd.CommandText = "SetAllCompositFeeTempProc";
						cmd.CommandType = CommandType.StoredProcedure;
						cmd.Connection = _con;
						cmd.Parameters.AddWithValue("@TableTypeComplositFee", dtComplositFee);
						cmd.Parameters.AddWithValue("@TxnID", merchTxnIds);
						cmd.Parameters.AddWithValue("@GateWayName", "Atom");
						try
						{
							_con.Open();
							cmd.ExecuteNonQuery();
							cmd.Parameters.Clear();
							_con.Close();
							hdn_Amt.Value = Convert.ToDecimal(hdnTotalPaid.Value).ToString();
							ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "openAtomPay();", true);
						}
						catch (Exception ex)
						{
							_oo.msgbox(Page, msgbox, "Some technical problems have occurred", "A");
						}
					}

				}
			}
			catch (Exception ex)
			{
				Campus camp1 = new Campus(); camp1.msgbox(Page, msgbox, "Please Update Atom Gateway Setting!", "A");
			}
		}
	}

	protected void SetTransaction(string TxnID, string HashStr, string SrNo, string Session, decimal amount, string GatewayName, string PaymentFor, int BranchCode, string Logintype, string LoginName, string ImageUrl)
	{
		try
		{
			setvalue(GatewayName, PaymentFor, BranchCode);
			string[] hashVarsSeq;
			string txnid = "", hash_string = "", hash = "", action = "", firstname = "", lastname = "", email = "", phone = "";
			hash_string = HashStr;
			txnid = TxnID;
			DataTable dt = new DataTable();
			dt = DAL.DALInstance.GetValueInTable("SELECT DISTINCT S.SrNo,S.FirstName,F.FamilyContactNo,F.FamilyEmail, S.lastname FROM StudentFamilyDetails F JOIN StudentGenaralDetail S ON F.SrNo=S.SrNo and F.BranchCode=S.BranchCode and F.SessionName=S.SessionName WHERE  Case When Left('" + SrNo + "',3)='eAM' Then S.StEnRCode Else S.SrNo End='" + SrNo + "' AND S.SessionName='" + Session + "' AND F.SessionName='" + Session + "' and F.BranchCode=" + BranchCode + "");
			if (dt != null && dt.Rows.Count > 0)
			{
				firstname = dt.Rows[0][1].ToString() != "" ? dt.Rows[0][1].ToString() : "abc";
				email = dt.Rows[0][3].ToString() != "" ? dt.Rows[0][3].ToString() : "abc@xyz.com";
				phone = dt.Rows[0][2].ToString() != "" ? dt.Rows[0][2].ToString() : "9876543210";
				lastname = dt.Rows[0][2].ToString() != "" ? dt.Rows[0][4].ToString() : "";
			}
			if (
				!string.IsNullOrEmpty(key) &&
				!string.IsNullOrEmpty(txnid) &&
				amount > 0 &&
				!string.IsNullOrEmpty(firstname) &&
				!string.IsNullOrEmpty(email) &&
				!string.IsNullOrEmpty(phone) &&
				!string.IsNullOrEmpty(productinfo) &&
				!string.IsNullOrEmpty(surl) &&
				!string.IsNullOrEmpty(furl) &&
				!string.IsNullOrEmpty(service_provider)
				)
			{
				hashVarsSeq = HashSeq.Split('|');
				hash_string = "";
				foreach (string hash_var in hashVarsSeq)
				{
					if (hash_var == "key")
					{
						hash_string = hash_string + key;
						hash_string = hash_string + '|';
					}
					else if (hash_var == "txnid")
					{
						hash_string = hash_string + txnid;
						hash_string = hash_string + '|';
					}
					else if (hash_var == "amount")
					{
						hash_string = hash_string + Convert.ToDecimal(amount).ToString("g29");
						hash_string = hash_string + '|';
					}
					else if (hash_var == "productinfo")
					{
						hash_string = hash_string + productinfo;
						hash_string = hash_string + '|';
					}
					else if (hash_var == "firstname")
					{
						hash_string = hash_string + firstname;
						hash_string = hash_string + '|';
					}
					else if (hash_var == "email")
					{
						hash_string = hash_string + email;
						hash_string = hash_string + '|';
					}
					else if (hash_var == "phone")
					{
						hash_string = hash_string + phone;
						hash_string = hash_string + '|';
					}
					else
					{
						hash_string = hash_string + "";
						hash_string = hash_string + '|';
					}
				}

				hash_string += salt;

				hash = GenerateHash512(hash_string).ToLower();
				action = PayuBaseURL + "/_payment";
			}

			if (!string.IsNullOrEmpty(hash))
			{
				Hashtable data = new Hashtable();
				data.Add("hash", hash);
				data.Add("txnid", txnid);
				data.Add("key", key);
				data.Add("amount", amount.ToString("g29"));
				data.Add("firstname", firstname);
				data.Add("email", email);
				data.Add("phone", phone);
				data.Add("productinfo", productinfo);
				data.Add("surl", (surl + "?amt=" + amount.ToString() + "&SessionName=" + Session + "&BranchCode=" + BranchCode + "&SrNo=" + SrNo + "&Logintype=" + Logintype + "&LoginName=" + LoginName + "&ImageUrl=" + ImageUrl));
				data.Add("furl", (furl + "?SessionName=" + Session + "&BranchCode=" + BranchCode + "&SrNo=" + SrNo + "&Logintype=" + Logintype + "&LoginName=" + LoginName + "&ImageUrl=" + ImageUrl));
				data.Add("lastname", lastname);
				data.Add("curl", (furl + "?SessionName=" + Session + "&BranchCode=" + BranchCode + "&SrNo=" + SrNo + "&Logintype=" + Logintype + "&LoginName=" + LoginName + "&ImageUrl=" + ImageUrl));
				data.Add("address1", "NA");
				data.Add("address2", "NA");
				data.Add("city", "LKO");
				data.Add("state", "UP");
				data.Add("country", "INDIA");
				data.Add("zipcode", "226022");
				data.Add("udf1", "");
				data.Add("udf2", "");
				data.Add("udf3", "");
				data.Add("udf4", "");
				data.Add("udf5", "");
				data.Add("pg", "");
				data.Add("service_provider", service_provider);

				if (isactive)
				{
					string strForm = PreparePOSTForm(action, data);
					Page.Controls.Add(new LiteralControl(strForm));
				}
			}
			else
			{
				//no hash       
			}
		}
		catch (Exception)
		{

		}
	}
	private string PreparePOSTForm(string url, Hashtable data)
	{
		string formID = "PostForm";
		StringBuilder strForm = new StringBuilder();
		strForm.Append("<form id=\"" + formID + "\" name=\"" + formID + "\" action=\"" + url + "\" method=\"POST\">");
		foreach (DictionaryEntry key in data)
		{
			strForm.Append("<input type=\"hidden\" name=\"" + key.Key + "\" value=\"" + key.Value + "\" >");
		}
		strForm.Append("</form>");
		StringBuilder strScript = new StringBuilder();
		strScript.Append("<script language='javascript'>");
		strScript.Append("var v" + formID + " = document." + formID + ";");
		strScript.Append("v" + formID + ".submit();");
		strScript.Append("</script>");
		return strForm.ToString() + strScript.ToString();
	}
	public string GenerateHash512(string txt)
	{
		string hex = "";
		try
		{
			byte[] MSG = Encoding.UTF8.GetBytes(txt);
			UnicodeEncoding UE = new UnicodeEncoding();
			byte[] HashValue;
			SHA512Managed HashString = new SHA512Managed();
			HashValue = HashString.ComputeHash(MSG);
			foreach (byte x in HashValue)
			{
				hex += String.Format("{0:x2}", x);
			}
		}
		catch (Exception)
		{
		}
		return hex;
	}
	public void setvalue(string GateWayName, string GateWayFor, int BranchCode)
	{
		string sql = "select * from TblPaymentGateway where BranchCode=" + BranchCode + " and GateWayFor='" + GateWayFor + "' and GateWayName='" + GateWayName + "' and Isactive=1";
		if (_oo.Duplicate(sql))
		{
			surl = _oo.ReturnTag(sql, "SuccessURL");
			furl = _oo.ReturnTag(sql, "FailurURL");
			service_provider = _oo.ReturnTag(sql, "ServiceProvider");
			productinfo = _oo.ReturnTag(sql, "ProductInfo");
			key = _oo.ReturnTag(sql, "MarchentKEY");
			salt = _oo.ReturnTag(sql, "Marchentsalt");
			PayuBaseURL = _oo.ReturnTag(sql, "BaseURL");
			HashSeq = _oo.ReturnTag(sql, "HashSequence");
			isactive = true;
		}
		else
		{
			surl = "";
			furl = "";
			service_provider = "";
			productinfo = "";
			key = "";
			salt = "";
			PayuBaseURL = "";
			HashSeq = "";
		}
	}
	public static string encryptFile(string textToEncrypt, string key)
	{
		RijndaelManaged rijndaelCipher = new RijndaelManaged();
		rijndaelCipher.Mode = CipherMode.ECB;
		rijndaelCipher.Padding = PaddingMode.PKCS7;
		rijndaelCipher.KeySize = 0x80;
		rijndaelCipher.BlockSize = 0x80;
		byte[] pwdBytes = Encoding.UTF8.GetBytes(key);
		byte[] keyBytes = new byte[0x10];
		int len = pwdBytes.Length;
		if (len > keyBytes.Length)
		{
			len = keyBytes.Length;
		}
		Array.Copy(pwdBytes, keyBytes, len);
		rijndaelCipher.Key = keyBytes;


		rijndaelCipher.IV = keyBytes;
		ICryptoTransform transform = rijndaelCipher.CreateEncryptor();
		byte[] plainText = Encoding.UTF8.GetBytes(textToEncrypt);
		return Convert.ToBase64String(transform.TransformFinalBlock(plainText, 0,
		plainText.Length));
	}
	protected void SetICICITransaction(string ReferenceNoP, string SubMerchantIdp, string SrNo, string Session, decimal amount, int BranchCode, string PaymentFor, Page pg)
	{
		try
		{

			string sql = "select top(1) ReturnUrl, ReferenceNo, SubMerchantId, MerchantID, AESKey, Isactive from TblPaymentGateway where BranchCode=" + BranchCode + " and GateWayFor='" + PaymentFor + "' and Isactive=1 and GateWayName='Eazypay'";
			string MerchantID = _oo.ReturnTag(sql, "MerchantID");
			string AESKey = _oo.ReturnTag(sql, "AESKey");
			string Isactive = _oo.ReturnTag(sql, "Isactive");
			string rUrl = _oo.ReturnTag(sql, "ReturnUrl");
			if (Isactive == "1" || Isactive.ToLower() == "true")
			{

				string mandatoryfields = encryptFile((ReferenceNoP + "|" + SubMerchantIdp + "|" + amount.ToString()), AESKey);
				//string mandatoryfields = encryptFile((ReferenceNoP + "|" + SubMerchantIdp + "|" + amount.ToString()+ "|UPIVPA"), AESKey);
				string optionalfields = encryptFile("20|20|20|20", AESKey);
				string returnurl = encryptFile(rUrl, AESKey);
				string ReferenceNo = encryptFile(ReferenceNoP, AESKey);
				string submerchantid = encryptFile(SubMerchantIdp, AESKey);
				string transactionamount = encryptFile(amount.ToString("0.0"), AESKey);
				string paymode = encryptFile("9", AESKey);
				string action = "https://eazypay.icicibank.com/EazyPG?merchantid=" + MerchantID.Trim() + "&mandatory fields=" + mandatoryfields.Trim() + "&optional fields=&returnurl=" + returnurl.Trim() + "&Reference No=" + ReferenceNo.Trim() + "&submerchantid=" + submerchantid.Trim() + "&transaction amount=" + transactionamount.Trim() + "&paymode=" + paymode.Trim() + "";
				//string strForm = PrepareICICIPOSTForm(action);
				//pg.Controls.Add(new LiteralControl(strForm));
				Response.Redirect(action);
			}
			else
			{
				throw new Exception("Please Update EasyPay Gateway Setting!");
			}
		}
		catch (Exception)
		{

		}
	}
	protected void btnPayStack_Click(object sender, EventArgs e)
	{
		var studentId = Session["Srno"].ToString();
		txnId = hdnTxtNos.Value;
		SubmitFee();
		using (SqlCommand cmd = new SqlCommand())
		{
			cmd.CommandText = "SetAllCompositFeeProc";
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Connection = _con;
			cmd.Parameters.AddWithValue("@TableTypeComplositFee", dtComplositFee);
			cmd.Parameters.AddWithValue("@ComplositFeeCount", dtComplositFee.Rows.Count);
			cmd.Parameters.AddWithValue("@GateWayName", "PayStack");
			try
			{
				_con.Open();
				cmd.ExecuteNonQuery();
				cmd.Parameters.Clear();
				_con.Close();
				Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Submited successfully", "S");
				SendSms(Session["RecieptSrNo"].ToString(), hdnTotalPaid.Value, studentId);
				string qstr = Session["RecieptSrNo"].ToString().Replace("/", "__");
				qstr = qstr + ("$" + Session["SessionName"].ToString() + "$" + Session["BranchCode"].ToString()).ToString();
				Response.Redirect("FeeReceiptAll.aspx?RecieptSrNo=" + qstr);
			}
			catch (Exception ex)
			{
				// ignored
			}
		}
	}

	public void SendSms(string recieptNo, string receivedAmount, string registervalue)
	{
		_sql = "select top(1) FamilyContactNo from StudentFamilyDetails  where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and SrNo='" + registervalue + "' order by id desc";
		var conta = _oo.ReturnTag(_sql, "FamilyContactNo");
		SendFeesSms(conta, recieptNo, receivedAmount, registervalue, "1");
	}
	public void SendFeesSms(string fmobileNo, string recieptNo, string receivedAmount, string registervalue, string title)
	{
		try
		{
			_sql = "Select HitValue from SMSActivateDeactivate where BranchCode=" + Session["BranchCode"] + "";
			if (_oo.ReturnTag(_sql, "HitValue") == "") return;
			if (_oo.ReturnTag(_sql, "HitValue") != "true") return;
			string sql1 = "Select SmsSent From SmsEmailMaster Where Id='1'";
			if (_oo.ReturnTag(sql1, "SmsSent").Trim() == "true")
			{
				var sadpNew = new SMSAdapterNew();
				_sql = "Select top(1) FirstName as StudentName  from StudentGenaralDetail  where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and SrNo='" + registervalue + "' order by id desc";
				string msg = "INR " + receivedAmount + " received for " + _oo.ReturnTag(_sql, "StudentName") + " (" + registervalue + "). Receipt No. " + recieptNo + " From, ";
				if (fmobileNo == "") return;
				sadpNew.Send(msg, fmobileNo, title);
			}
		}
		catch
		{
			// ignored
		}
	}

	protected void chkCompleteFee_CheckedChanged(object sender, EventArgs e)
	{
		var studentId = Session["Srno"].ToString();
		GetTutionFeeDetails(studentId);

	}


	public string RandomString(int length)
	{
		Random random = new Random();
		const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
		return new string(Enumerable.Repeat(chars, length)
			.Select(s => s[random.Next(s.Length)]).ToArray());
	}
}