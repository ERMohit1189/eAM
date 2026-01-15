using c4SmsNew;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web.UI.WebControls;

public partial class additionalFeesDeposit : System.Web.UI.Page
{
    private SqlConnection _con;
    readonly Campus _oo;
    private string _sql = "";

    public additionalFeesDeposit()
    {
        _con = new SqlConnection();
        _oo = new Campus();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        _con = _oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file

        if (!IsPostBack)
        {
            Session["CheckRefresh"] = Server.UrlDecode(DateTime.Now.ToString(CultureInfo.InvariantCulture));
            TxtEnter.Focus();
            checkbool();
            _oo.AddDateMonthYearDropDown(DDYear, DDMonth, DDDate);
            _oo.FindCurrentDateandSetinDropDown(DDYear, DDMonth, DDDate);
            _oo.AddDateMonthYearDropDown(DDYear0, DDMonth0, DDDate0);
            _oo.FindCurrentDateandSetinDropDown(DDYear0, DDMonth0, DDDate0);
            Tables.Visible = false;


        }

    }
    protected void checkbool()
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
    public bool isPending(string srno)
    {
        bool sts = true;
        _sql = "select id from Other_fee_collection_1 where SessionName = '" + Session["SessionName"].ToString() + "' and BranchCode =" + Session["BranchCode"].ToString() + " and Srno = '" + srno + "'";
        if (_oo.Duplicate(_sql))
        {
            _sql = "select id from Other_fee_collection_1 where SessionName = '" + Session["SessionName"].ToString() + "' and BranchCode =" + Session["BranchCode"].ToString() + " and Srno = '" + srno + "' and Id = (select top(1) id from Other_fee_collection_1 where SessionName = '" + Session["SessionName"].ToString() + "' and BranchCode =" + Session["BranchCode"].ToString() + " and Srno = '" + srno + "' order by id desc)";
            sts = _oo.Duplicate(_sql);
        }
        return sts;
    }
    public bool permission()
    {
        bool sts = false;
        DataTable dtp = new DataTable();
        using (SqlCommand cmd = new SqlCommand())
        {
            try
            {
                cmd.CommandText = "FeePermissionSettingProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = _con;
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dtp);
                if (dtp.Rows.Count > 0)
                {
                    sts = (dtp.Rows[0]["aditionalDiscount"].ToString() == "0" ? false : true);
                }
            }
            catch (Exception ex)
            {
            }
        }
        return sts;
    }
    public void Fill_value()
    {
        bool discountEnable = false;
        DataTable dtp = new DataTable();
        using (SqlCommand cmd = new SqlCommand())
        {
            try
            {
                cmd.CommandText = "FeePermissionSettingProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = _con;
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dtp);
                if (dtp.Rows.Count > 0)
                {
                    discountEnable = (dtp.Rows[0]["aditionalDiscount"].ToString() == "0" ? false : true);
                    DDYear.Enabled = (dtp.Rows[0]["aditionalDate"].ToString() == "0" ? false : true);
                    DDMonth.Enabled = (dtp.Rows[0]["aditionalDate"].ToString() == "0" ? false : true);
                    DDDate.Enabled = (dtp.Rows[0]["aditionalDate"].ToString() == "0" ? false : true);
                }
            }
            catch (Exception ex)
            {
            }
        }
        GridView1.DataSource = null;
        GridView1.DataBind();
        if (Grd.Rows.Count > 0)
        {
            string studentId = Request.Form[hfStudentId.UniqueID];
            if (studentId == string.Empty)
            {
                studentId = TxtEnter.Text.Trim();
            }
            Label lblClassId = (Label)Grd.Rows[0].FindControl("lblClassId");
            Label lblGender = (Label)Grd.Rows[0].FindControl("lblGender");
            _sql = "Select Row_Number() Over(Order By Id) as Ids,HeadName,Id,Amount,Concession from other_fee_collection_head_1 where";
            _sql += " SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and Gender='" + lblGender.Text + "' and ClassId='" + lblClassId.Text + "'  Order By Id";
            double amt1 = double.Parse(_oo.ReturnTag(_sql, "Amount"));

            string ss1 = " select isnull(sum(ReceivedAmount),0) amt from Other_fee_collection_1 where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and srno='" + studentId + "' and Status='Paid'";
            double amt2 = double.Parse(_oo.ReturnTag(ss1, "amt"));
            if (amt2 == amt1)
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                Tables.Visible = false;
                return;
            }
            GridView1.DataSource = _oo.GridFill(_sql);
            GridView1.DataBind();
            if (GridView1.Rows.Count > 0)
            {
                Tables.Visible = true;
                Label Label33 = (Label)GridView1.FooterRow.FindControl("Label33");
                double total = 0;
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    Label TextBox1 = (Label)GridView1.Rows[i].FindControl("TextBox1");
                    TextBox TextBox3 = (TextBox)GridView1.Rows[i].FindControl("TextBox3");
                    TextBox3.Enabled = permission();
                    total = total + double.Parse(TextBox1.Text);
                    TextBox3.Enabled = discountEnable;
                }
                Label33.Text = total.ToString("0.00");

                _sql = "select * from Other_fee_Collection_1 where Srno='" + studentId + "' and Status='Cancelled' and MOP='Cheque' and id =(select top(1) id from Other_fee_Collection_1 where Srno='" + studentId + "' order by id desc)";

                if (_oo.Duplicate(_sql))
                {
                    _sql = "select isnull(FineAmount, 0) FineAmount from ChequeBounceFineMaster where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"] + "";
                    if (double.Parse(_oo.ReturnTag(_sql, "FineAmount")) > 0)
                    {
                        divBounce.Visible = true;
                        BounceCharges.Text = _oo.ReturnTag(_sql, "FineAmount");
                        totalAmt.Text = (double.Parse(_oo.ReturnTag(_sql, "FineAmount")) + double.Parse(TotalPayableAmount.Text)).ToString("0.00");
                    }
                }
            }
            else
            {
                Tables.Visible = false;
            }

        }

        divBtn.Visible = false;
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        string studentId = Request.Form[hfStudentId.UniqueID];
        if (studentId == string.Empty)
        {
            TxtEnter.Text = TxtEnter.Text.Replace("&", "/").Trim();
        }
        showDetails();
    }

    public void showDetails()
    {


        divReport.Visible = false;
        string studentId = Request.Form[hfStudentId.UniqueID];
        if (studentId == string.Empty)
        {
            studentId = TxtEnter.Text.Trim();
        }
        if (studentId != "")
        {
            _sql = "select SG.Id, SC.SectionName,SO.Card,SO.Medium as Medium,CM.ClassName,CM.Id as ClassId,Sg.Gender,convert(nvarchar,So.DateOfAdmiission,106) as DateOfAdmiission ,SO.SectionId,Sf.FatherName,SF.MotherName,SG.FirstName,SG.MiddleName,SG.LastName,sg.StEnRCode as StEnRCode,sg.srno  as srno,case  when so.TransportRequired='Yes' then 'Yes' else 'No' end as TransportRequired,so.wayamount as wayamount ,bm.BranchName,SF.FamilyContactNo,SG.PhotoPath  from StudentGenaralDetail SG ";
            _sql += "   left join StudentFamilyDetails SF on SG.SrNo=SF.SrNo and SG.SessionName=SF.SessionName and SG.BranchCode=SF.BranchCode ";
            _sql += "   left join StudentOfficialDetails SO on SG.SrNo=SO.SrNo  and SG.SessionName=SO.SessionName and SG.BranchCode=SO.BranchCode ";
            _sql += "   left join ClassMaster CM on SO.AdmissionForClassId=CM.Id  and SG.SessionName=CM.SessionName and SG.BranchCode=CM.BranchCode  ";
            _sql += "   left join SectionMaster SC on SO.SectionId=SC.Id and SG.SessionName=SC.SessionName and SG.BranchCode=SC.BranchCode left join BranchMaster bm on SO.Branch=bm.id and SG.SessionName=bm.SessionName and SG.BranchCode=bm.BranchCode and bm.IsDisplay=1 ";
            _sql += "   where  Case When Left('" + studentId + "',3)='eAM' Then SG.StEnRCode Else SG.SrNo End ='" + studentId + "'";
            _sql += "   and sg.SessionName='" + Session["SessionName"].ToString() + "' and ";
            _sql += "   so.SessionName='" + Session["SessionName"].ToString() + "' and sf.SessionName='" + Session["SessionName"].ToString() + "' and cm.SessionName='" + Session["SessionName"].ToString() + "'";
            _sql += "   and SC.SessionName='" + Session["SessionName"].ToString() + "'  and";
            _sql += "   sg.BranchCode=" + Session["BranchCode"].ToString() + "";
            _sql += "   and SO.Withdrwal is null";

            Grd.DataSource = _oo.GridFill(_sql);
            Grd.DataBind();
            DataSet ds;
            ds = _oo.GridFill(_sql);
            if (ds != null && Grd.Rows.Count > 0)
            {
                Grid_fill();
                grdshow.Visible = true;
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
            _sql = "select SessionName from Other_fee_collection_1 where and Cancel is null and Srno='" + studentId + "' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

            if (_oo.Duplicate(_sql))
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Already submitted!", "A");

            }

            if (Grd.Rows.Count == 0)
            {
                //oo.MessageBox("Sorry, Record not found!", Page);
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Sorry, Record not found!", "A");


                Tables.Visible = false;
            }
            else
            {
                Fill_value();
                count();
            }
        }
        else
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            Tables.Visible = false;
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Please, Enter S.R.NO.!", "A");
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
    public void Grid_fill()
    {

        divReport.Visible = false;
        string studentId = Request.Form[hfStudentId.UniqueID];
        if (studentId == string.Empty)
        {
            studentId = TxtEnter.Text.Trim();
        }
        _sql = "select Receipt_no, format(FeeDepositeDate, 'dd-MMM-yyyy') Date, MOP Mode, Status, amount, Concession, BounceCharges, ((amount+BounceCharges)- Concession) total, ReceivedAmount paid from Other_fee_collection_1 ";
        _sql += " where SessionName='" + Session["SessionName"].ToString() + "'  and BranchCode=" + Session["BranchCode"].ToString() + " and Srno='" + studentId + "' order by FeeDepositeDate, id desc";
        GridView2.DataSource = _oo.GridFill(_sql);
        GridView2.DataBind();
        if (GridView2.Rows.Count > 0)
        {
            divReport.Visible = true;
            double amt = 0, fine = 0, Concession = 0, TotAmt = 0, Paid = 0;
            for (int i = 0; i < GridView2.Rows.Count; i++)
            {
                Label amount = (Label)GridView2.Rows[i].FindControl("amount");
                Label BounceChargess = (Label)GridView2.Rows[i].FindControl("BounceChargess");
                Label Exemption = (Label)GridView2.Rows[i].FindControl("Exemption");

                Label total = (Label)GridView2.Rows[i].FindControl("total");
                Label ReceivedAmount = (Label)GridView2.Rows[i].FindControl("ReceivedAmount");
                amt = amt + double.Parse(amount.Text);
                if (BounceChargess.Text != "")
                {
                    fine = fine + double.Parse(BounceChargess.Text);
                }
                if (Exemption.Text != "")
                {
                    Concession = Concession + double.Parse(Exemption.Text);
                }
                if (total.Text != "")
                {
                    TotAmt = TotAmt + double.Parse(total.Text);
                }
                Paid = Paid + double.Parse(ReceivedAmount.Text);
            }
            Label Footeramount = (Label)GridView2.FooterRow.FindControl("Footeramount");
            Label FooterBounceCharges = (Label)GridView2.FooterRow.FindControl("FooterBounceCharges");
            Label FooterExemption = (Label)GridView2.FooterRow.FindControl("FooterExemption");
            Label Footertotal = (Label)GridView2.FooterRow.FindControl("Footertotal");
            Label FooterReceivedAmount = (Label)GridView2.FooterRow.FindControl("FooterReceivedAmount");
            Footeramount.Text = amt.ToString("0.00");
            FooterBounceCharges.Text = fine.ToString("0.00");
            FooterExemption.Text = Concession.ToString("0.00");
            Footertotal.Text = TotAmt.ToString("0.00");
            FooterReceivedAmount.Text = Paid.ToString("0.00");
            //if (GridView1.Rows.Count==0)
            //{
            //    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Complete Additional fee has been submitted.", "S");
            //}
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Button1.Enabled = false;
        string Invoice_no = _oo.FindRecieptNo();
        if (Invoice_no == "")
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Please Initilize receipt no.!", "A");
            Button1.Enabled = true;
            return;
        }
        string recieptSrNo = "";

        if (Session["CheckRefresh"].ToString() == ViewState["CheckRefresh"].ToString())
        {
            string studentId = Request.Form[hfStudentId.UniqueID];
            if (studentId == string.Empty)
            {
                studentId = TxtEnter.Text.Trim();
            }

            if (Grd.Rows.Count > 0)
            {
                Label Label1 = (Label)Grd.Rows[0].FindControl("Label1");
                string fd = "";
                fd = DDYear.SelectedItem.ToString() + "/" + DDMonth.SelectedItem.ToString() + "/" + DDDate.SelectedItem.ToString();

                double amount = 0; int counts = 0; int ss = 0;
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    CheckBox chk = (CheckBox)GridView1.Rows[i].FindControl("chk");
                    if (chk.Checked)
                    {
                        _con.Open();
                        Label Label8 = (Label)GridView1.Rows[i].FindControl("Label8");
                        Label lblRecevied = (Label)GridView1.Rows[i].FindControl("lblRecevied");
                        Label TextBox1 = (Label)GridView1.Rows[i].FindControl("TextBox1");
                        TextBox TextBox3s = (TextBox)GridView1.Rows[i].FindControl("TextBox3");
                        SqlCommand Other_feecollections = new SqlCommand("Other_fee_collections_1_proc", _con);
                        Other_feecollections.CommandType = CommandType.StoredProcedure;
                        Other_feecollections.Parameters.AddWithValue("@Receipt_no", recieptSrNo);
                        Other_feecollections.Parameters.AddWithValue("@Srno", Label1.Text);
                        Other_feecollections.Parameters.AddWithValue("@HeadId", Label8.Text);
                        if (TextBox1.Text != "")
                        {
                            Other_feecollections.Parameters.AddWithValue("@Amount", TextBox1.Text);
                        }
                        else
                        {
                            Other_feecollections.Parameters.AddWithValue("@Amount", "0");
                        }
                        Other_feecollections.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                        Other_feecollections.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                        Other_feecollections.Parameters.AddWithValue("@Date", fd);
                        Other_feecollections.Parameters.AddWithValue("@Id", "0");
                        Other_feecollections.Parameters.AddWithValue("@task", "Insert");
                        if (TextBox3s.Text != "")
                        {
                            Other_feecollections.Parameters.AddWithValue("@Concession", TextBox3s.Text);
                        }
                        else
                        {
                            Other_feecollections.Parameters.AddWithValue("@Concession", "0");
                        }
                        if (double.Parse(BounceCharges.Text) > 0)
                        {
                            if (ss == 0)
                            {
                                Other_feecollections.Parameters.AddWithValue("@BounceCharges", BounceCharges.Text);
                                ss += 1;
                            }
                        }
                        Other_feecollections.Parameters.AddWithValue("@ReceivedAmount", lblRecevied.Text);
                        if (TextBox2.Text != "")
                        {
                            Other_feecollections.Parameters.AddWithValue("@CheckDDNo", TextBox2.Text);
                        }
                        else
                        {
                            Other_feecollections.Parameters.AddWithValue("@CheckDDNo", "N/A");
                        }
                        if (TextBox3.Text != "")
                        {
                            Other_feecollections.Parameters.AddWithValue("@BankName", TextBox3.Text);
                        }
                        else
                        {
                            Other_feecollections.Parameters.AddWithValue("@BankName", "N/A");
                        }
                        if (DropDownMOD.SelectedValue == "Cheque" || DropDownMOD.SelectedValue == "Other")
                        {
                            Other_feecollections.Parameters.AddWithValue("@Status", drpStatus.SelectedValue);
                            Other_feecollections.Parameters.AddWithValue("@ChequeStatus", drpStatus.SelectedValue);
                        }
                        else
                        {
                            Other_feecollections.Parameters.AddWithValue("@Status", "Paid");
                        }
                        if (DropDownMOD.SelectedValue != "Cash")
                        {
                            Other_feecollections.Parameters.AddWithValue("@ChequeDate", txtChequeDate.Text);
                        }
                        Other_feecollections.Parameters.AddWithValue("@MOP", DropDownMOD.SelectedItem.Text);
                        Other_feecollections.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());

                        SqlParameter outputValue = new SqlParameter("@result", "");
                        outputValue.Size = 0x100;
                        outputValue.Direction = ParameterDirection.Output;
                        Other_feecollections.Parameters.Add(outputValue);

                        Other_feecollections.ExecuteNonQuery();
                        _con.Close();

                        if (recieptSrNo == "")
                        {
                            recieptSrNo = Other_feecollections.Parameters["@result"].Value.ToString();
                        }
                        counts = counts + 1;
                    }
                }
                if (counts > 0)
                {
                    amount = double.Parse(totalAmt.Text);
                    //Session["OtherReciept1"] = Invoice_no;
                    _sql = "select top(1) FamilyContactNo from StudentFamilyDetails  where Srno='" + studentId + "'  and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " order by id desc";
                    string Conta = "";
                    Conta = _oo.ReturnTag(_sql, "FamilyContactNo");
                    //SendFeesSms(Conta, recieptSrNo, amount.ToString(CultureInfo.InvariantCulture));//Old
                    ComposeSMS(recieptSrNo);
                    Response.Redirect("AdditionalFeeReceipt.aspx?print=1&OtherReciept1=" + recieptSrNo);
                }
            }

        }
        Button1.Enabled = true;
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
            DataSet ds = _oo.ReturnDataSet("USP_AdditionalFeeTemplate", param.ToArray());
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

            SendFeesSms(fatherContactNo, msg, "6");

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
            string sql1 = "Select SmsSent From SmsEmailMaster Where Id='" + smsPageId + "' and BranchCode='" + Session["BranchCode"] + "'";
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
    //public void SendFeesSms(string FmobileNo, string RecieptNo, string Amount)
    //{
    //	_sql = "Select HitValue from SMSActivateDeactivate where BranchCode=" + Session["BranchCode"] + "";
    //	if (_oo.ReturnTag(_sql, "HitValue") != "")
    //	{
    //		if (_oo.ReturnTag(_sql, "HitValue") == "true")
    //		{
    //			SMSAdapterNew sadpNew = new SMSAdapterNew();
    //			string mess = "";
    //			string collegeTitle = "";

    //			mess = "INR " + Amount + " received towards Additional Fee Deposit. Receipt No. " + RecieptNo + "";
    //			string sms_response = "";

    //			_sql = "Select CollegeShortNa  from CollegeMaster where BranchCode=" + Session["BranchCode"] + "";
    //			collegeTitle = _oo.ReturnTag(_sql, "CollegeShortNa");
    //			if (FmobileNo != "")
    //			{
    //				_sql = "Select SmsSent From SmsEmailMaster where Id='7' ";
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

    protected void TextBox3_TextChanged(object sender, EventArgs e)
    {
        double total = 0;
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            Label TextBox1 = (Label)GridView1.Rows[i].FindControl("TextBox1");
            TextBox TextBox3 = (TextBox)GridView1.Rows[i].FindControl("TextBox3");
            Label lblRecevied = (Label)GridView1.Rows[i].FindControl("lblRecevied");
            CheckBox chk = (CheckBox)GridView1.Rows[i].FindControl("chk");
            lblRecevied.Text = (double.Parse(TextBox1.Text) - double.Parse(TextBox3.Text == "" ? "0" : TextBox3.Text)).ToString();
            if (chk.Checked)
            {
                total = total + double.Parse(lblRecevied.Text);
            }
        }
        TotalPayableAmount.Text = (total).ToString("0.00");
        totalAmt.Text = (double.Parse(BounceCharges.Text) + double.Parse(TotalPayableAmount.Text)).ToString("0.00");
        if (total > 0)
        {
            divBtn.Visible = true;
        }
        else
        {
            divBtn.Visible = false;
        }
    }
    public void count()
    {
        if (GridView1.Rows.Count > 0)
        {
            try
            {
                Label Label33 = (Label)GridView1.FooterRow.FindControl("Label33");
                Label lblTotalConcession = (Label)GridView1.FooterRow.FindControl("lblTotalConcession");
                Label lblTotalReceived = (Label)GridView1.FooterRow.FindControl("lblTotalReceived");
                //Label lblRecevied = (Label)GridView1.Rows[i].FindControl("lblRecevied");
                Double Sum1 = 0, Sum2 = 0, Sum3 = 0;
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    TextBox TextBox1 = (TextBox)GridView1.Rows[i].FindControl("TextBox1");
                    TextBox TextBox3 = (TextBox)GridView1.Rows[i].FindControl("TextBox3");
                    Label lblRecevied = (Label)GridView1.Rows[i].FindControl("lblRecevied");
                    double concession = 0;
                    if (TextBox3.Text != "")
                    {

                        concession = Convert.ToDouble(TextBox3.Text);
                        Sum1 = Sum1 + concession;
                    }
                    else
                    {
                        concession = 0;
                        Sum1 = Sum1 + concession;
                    }
                    double amount = 0;
                    if (TextBox1.Text != "")
                    {

                        amount = Convert.ToDouble(TextBox1.Text);
                        Sum2 = Sum2 + amount;

                    }
                    else
                    {
                        amount = 0;
                        Sum2 = Sum2 + amount;
                    }
                    double receviedamount = amount - concession;
                    Sum3 = Sum3 + receviedamount;
                    lblRecevied.Text = receviedamount.ToString();
                }
                Label33.Text = Sum2.ToString();
                lblTotalConcession.Text = Sum1.ToString();
                lblTotalReceived.Text = Sum3.ToString();


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
            Button1.Focus();
        }
    }
    protected void LinkButton6_Click(object sender, EventArgs e)
    {
        LinkButton link = (LinkButton)sender;
        Label34.Text = link.Text;
        _sql = "select Distinct Receipt_no ,convert(Varchar(11),Other_fee_collection_1.FeeDepositeDate,106) as FeeDepositeDate,Other_fee_collection_1.Srno,StudentGenaralDetail.FirstName+' '+StudentGenaralDetail.MiddleName+' '+StudentGenaralDetail.LastName as StudentName from Other_fee_collection_1 inner join StudentGenaralDetail on StudentGenaralDetail.SrNo=Other_fee_collection_1.Srno where Other_fee_collection_1.Receipt_no='" + Label34.Text + "' and Other_fee_collection_1.BranchCode=" + Session["BranchCode"].ToString() + " Group By Other_fee_collection_1.Receipt_no,Other_fee_collection_1.Srno,Other_fee_collection_1.FeeDepositeDate,StudentGenaralDetail.FirstName,StudentGenaralDetail.MiddleName,StudentGenaralDetail.LastName ";
        Label35.Text = _oo.ReturnTag(_sql, "StudentName");
        Label36.Text = _oo.ReturnTag(_sql, "FeeDepositeDate");
        _sql = "select ROW_NUMBER() over(order By Other_fee_collection_1.Id) as Id,Other_fee_collection_1.Amount,other_fee_collection_head_1.HeadName from Other_fee_collection_1 inner join other_fee_collection_head_1 on other_fee_collection_head_1.Id=Other_fee_collection_1.HeadId where Other_fee_collection_1.Receipt_no='" + link.Text + "' and Other_fee_collection_1.SessionName='" + Session["SessionName"].ToString() + "' and other_fee_collection_head_1.SessionName='" + Session["SessionName"].ToString() + "' and other_fee_collection_head_1.BranchCode=" + Session["BranchCode"].ToString() + "";
        GridView3.DataSource = _oo.GridFill(_sql);
        GridView3.DataBind();
        model.Show();
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        if (Session["CheckRefresh"].ToString() == ViewState["CheckRefresh"].ToString())
        {
            string ss = Label34.Text;

            Response.Redirect("AdditionalFeeReceipt_duplicate.aspx?print=1&OtherReciept1=" + ss);
            //Response.Redirect("AdditionalFeeReceipt_duplicate.aspx?print=1");
        }
        else
        {
            //oo.MessageBox("Please Do Not Press Refresh Button or back Button", Page);
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Please Do Not Press Refresh Button or back Button", "A");

        }
    }
    protected void link_Edit_Click(object sender, EventArgs e)
    {
        if (Session["CheckRefresh"].ToString() == ViewState["CheckRefresh"].ToString())
        {
            LinkButton link = (LinkButton)sender;
            Label lblId = (Label)link.NamingContainer.FindControl("Label14");
            _sql = "select Distinct Receipt_no ,convert(Varchar(11),Other_fee_collection_1.FeeDepositeDate,106) as FeeDepositeDate,";
            _sql += " Other_fee_collection_1.Srno,so.AdmissionForClassId,StudentGenaralDetail.Gender,";
            _sql += " StudentGenaralDetail.FirstName+' '+StudentGenaralDetail.MiddleName+' '+StudentGenaralDetail.LastName as StudentName";
            _sql += " ,SUM(Other_fee_collection_1.Amount) as TotalPaid from Other_fee_collection_1 ";
            _sql += " inner join StudentGenaralDetail on StudentGenaralDetail.SrNo=Other_fee_collection_1.Srno ";
            _sql += " inner join StudentOfficialDetails so on So.SrNo=Other_fee_collection_1.Srno";
            _sql += " where Other_fee_collection_1.Receipt_no='" + lblId.Text + "' and ";
            _sql += " Other_fee_collection_1.SessionName='" + Session["SessionName"].ToString() + "' and Other_fee_collection_1.BranchCode=" + Session["BranchCode"].ToString() + " and";
            _sql += " StudentGenaralDetail.SessionName='" + Session["SessionName"].ToString() + "' and so.SessionName='" + Session["SessionName"].ToString() + "' and so.BranchCode=" + Session["BranchCode"].ToString() + "";
            _sql += " Group By Other_fee_collection_1.Receipt_no,Other_fee_collection_1.Srno,Other_fee_collection_1.FeeDepositeDate,";
            _sql += " StudentGenaralDetail.FirstName,StudentGenaralDetail.MiddleName,StudentGenaralDetail.LastName,so.AdmissionForClassId,StudentGenaralDetail.Gender ";
            Label37.Text = lblId.Text.Trim();
            Label44.Text = _oo.ReturnTag(_sql, "Srno");
            Label38.Text = _oo.ReturnTag(_sql, "StudentName");
            Label39.Text = _oo.ReturnTag(_sql, "FeeDepositeDate");
            Label10.Text = _oo.ReturnTag(_sql, "AdmissionForClassId");
            Label11.Text = _oo.ReturnTag(_sql, "Gender");
            _sql = "Select Row_Number() Over(Order By Id) as Ids,HeadName,Id,Amount,Concession from other_fee_collection_head_1 where";
            _sql += " SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and Gender='" + Label11.Text + "' and ClassId='" + Label10.Text + "' Order By Id";
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
                _sql = "select Amount,HeadId,Id,Concession,ReceivedAmount from Other_fee_collection_1 where Receipt_no='" + lblId.Text + "' and BranchCode=" + Session["BranchCode"].ToString() + " and HeadId='" + Label45.Text + "' and SessionName='" + Session["SessionName"].ToString() + "'";
                if (_oo.ReturnTag(_sql, "Amount") != "")
                {
                    Label9.Text = _oo.ReturnTag(_sql, "Id");
                    Label41.Text = _oo.ReturnTag(_sql, "HeadId");
                    TextBox2.Text = _oo.ReturnTag(_sql, "Amount");
                    txtConcession0.Text = _oo.ReturnTag(_sql, "Concession");
                    lblRecevied0.Text = _oo.ReturnTag(_sql, "ReceivedAmount"); ;
                }
            }
            count1();
            ModalPopupExtender1.Show();
        }
        else
        {
            //oo.MessageBox("Please Do Not Press Refresh Button or back Button", Page);
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Please Do Not Press Refresh Button or back Button", "A");

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
                _sql = "select COUNT(*) as Counter from Other_fee_collection_1 where Receipt_no='" + Label37.Text + "' and HeadId='" + Label41.Text + "' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
                if (Label41.Text == "")
                {
                    SqlCommand Other_feecollections = new SqlCommand("Other_fee_collections_1_proc", _con);
                    Other_feecollections.CommandType = CommandType.StoredProcedure;
                    Other_feecollections.Parameters.AddWithValue("@Receipt_no", Label37.Text);
                    Other_feecollections.Parameters.AddWithValue("@Srno", Label44.Text);
                    Other_feecollections.Parameters.AddWithValue("@HeadId", Label45.Text);
                    Other_feecollections.Parameters.AddWithValue("@Amount", TextBox2.Text);
                    Other_feecollections.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                    Other_feecollections.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
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
                        SqlCommand Other_feecollections = new SqlCommand("Other_fee_collections_1_proc", _con);
                        Other_feecollections.CommandType = CommandType.StoredProcedure;
                        Other_feecollections.Parameters.AddWithValue("@Receipt_no", Label37.Text);
                        Other_feecollections.Parameters.AddWithValue("@Srno", Label44.Text);
                        Other_feecollections.Parameters.AddWithValue("@HeadId", Label45.Text);
                        Other_feecollections.Parameters.AddWithValue("@Amount", TextBox2.Text);
                        Other_feecollections.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                        Other_feecollections.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                        Other_feecollections.Parameters.AddWithValue("@Date", fd1);
                        Other_feecollections.Parameters.AddWithValue("@Id", Label9.Text);
                        Other_feecollections.Parameters.AddWithValue("@task", "Update");
                        Other_feecollections.Parameters.AddWithValue("@Concession", txtConcession0.Text);
                        Other_feecollections.Parameters.AddWithValue("@ReceivedAmount", lblRecevied0.Text);
                        Other_feecollections.ExecuteNonQuery();
                    }
                }
            }
            //oo.MessageBox("Record Updated Successfully", Page);
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Record Updated Successfully", "S");

        }
        catch { }
        _con.Close();
        Session["OtherReciept1"] = Label37.Text;
        Response.Redirect("MiscellaneousFeesReciept_duplicate.aspx?print=1");
        //Grid_fill();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        SqlCommand Other_feecollections = new SqlCommand();
        Other_feecollections.CommandText = "Other_fee_collections_1_proc";
        Other_feecollections.CommandType = CommandType.StoredProcedure;
        Other_feecollections.Connection = _con;
        Other_feecollections.Parameters.AddWithValue("@task", "Delete");
        Other_feecollections.Parameters.AddWithValue("@Receipt_no", lblvalue.Text);
        Other_feecollections.Parameters.AddWithValue("@Srno", "");
        Other_feecollections.Parameters.AddWithValue("@HeadId", "0");
        Other_feecollections.Parameters.AddWithValue("@Amount", "0");
        Other_feecollections.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
        Other_feecollections.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
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

        Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Canceled Successfully", "S");

        Grid_fill();
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
                    _sql = "Select SmsSent From SmsEmailMaster where Id='6' and BranchCode='" + Session["BranchCode"] + "'";
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
    protected void TextBox2_TextChanged(object sender, EventArgs e)
    {
        count1();
        ModalPopupExtender1.Show();
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
                    lblRecevied0.Text = receviedamount.ToString();
                }
                Label43.Text = Sum2.ToString();
                lblTotalConcession0.Text = Sum1.ToString();
                lblTotalReceived0.Text = Sum3.ToString();
            }
            catch
            {

            }
        }
    }
    protected void txtConcession0_TextChanged(object sender, EventArgs e)
    {
        count1();
        ModalPopupExtender1.Show();
    }
    protected void DropDownMOD_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownMOD.SelectedValue == "Cash")
        {
            table2.Visible = false;
            table12.Visible = false;
            table12Status.Visible = false;
            chkdt.Visible = false;
        }
        if (DropDownMOD.SelectedValue == "Cheque")
        {
            table2.Visible = true;
            table12.Visible = true;
            table12Status.Visible = true;
            chkdt.Visible = true;

            Label42.Text = "Instrument No.";
            lblChqDate.Text = "Instrument Date";
            Label43.Text = "Issuer";
        }
        if (DropDownMOD.SelectedValue == "DD")
        {
            table2.Visible = true;
            table12.Visible = true;
            table12Status.Visible = false;
            chkdt.Visible = true;
            Label42.Text = "Instrument No.";
            lblChqDate.Text = "Instrument Date";
            Label43.Text = "Issuer";
        }
        if (DropDownMOD.SelectedValue == "Card")
        {
            table2.Visible = true;
            table12.Visible = true;
            table12Status.Visible = false;
            chkdt.Visible = true;
            Label42.Text = "Card No.";
            lblChqDate.Text = "Transaction Date";
            Label43.Text = "Issuer";
        }
        if (DropDownMOD.SelectedValue == "Online Transfer")
        {
            table2.Visible = true;
            table12.Visible = true;
            table12Status.Visible = false;
            chkdt.Visible = true;
            Label42.Text = "Ref. No.";
            lblChqDate.Text = "Transaction Date";
            Label43.Text = "Issuer";
        }
        if (DropDownMOD.SelectedValue == "Other")
        {
            table2.Visible = true;
            table12.Visible = true;
            table12Status.Visible = true;
            chkdt.Visible = true;
            Label42.Text = "Ref. No.";
            lblChqDate.Text = "Transaction Date";
            Label43.Text = "Reference Name";
        }

    }
    protected void DDYear0_SelectedIndexChanged(object sender, EventArgs e)
    {
        _oo.YearDropDown(DDYear0, DDMonth0, DDDate0);
        ModalPopupExtender1.Show();
    }
    protected void DDMonth0_SelectedIndexChanged(object sender, EventArgs e)
    {
        _oo.MonthDropDown(DDYear0, DDMonth0, DDDate0);
        ModalPopupExtender1.Show();
    }

    public override void Dispose()
    {
        _con.Dispose();
        _oo.Dispose();
    }

    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        string studentId = Request.Form[hfStudentId.UniqueID];
        if (studentId == string.Empty)
        {
            TxtEnter.Text = TxtEnter.Text.Replace("&", "/").Trim();
        }
        CheckBox chkAll = (CheckBox)GridView1.HeaderRow.FindControl("chkAll");
        double total = 0;
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            CheckBox chk = (CheckBox)GridView1.Rows[i].FindControl("chk");
            if (chkAll.Checked)
            {
                chk.Checked = true;
                Label TextBox1 = (Label)GridView1.Rows[i].FindControl("TextBox1");
                TextBox TextBox3 = (TextBox)GridView1.Rows[i].FindControl("TextBox3");
                total = total + (double.Parse(TextBox1.Text) - double.Parse(TextBox3.Text));
            }
            else
            {
                chk.Checked = false;
            }
        }
        TotalPayableAmount.Text = total.ToString("0.00");
        totalAmt.Text = (double.Parse(BounceCharges.Text) + double.Parse(TotalPayableAmount.Text)).ToString("0.00");
        if (total > 0 && isPending(studentId))
        {
            divBtn.Visible = true;
        }
        else
        {
            divBtn.Visible = false;
        }
    }

    protected void chk_CheckedChanged(object sender, EventArgs e)
    {
        string studentId = Request.Form[hfStudentId.UniqueID];
        if (studentId == string.Empty)
        {
            TxtEnter.Text = TxtEnter.Text.Replace("&", "/").Trim();
        }
        double total = 0;
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            CheckBox chk = (CheckBox)GridView1.Rows[i].FindControl("chk");
            if (chk.Checked)
            {
                Label TextBox1 = (Label)GridView1.Rows[i].FindControl("TextBox1");
                TextBox TextBox3 = (TextBox)GridView1.Rows[i].FindControl("TextBox3");
                total = total + (double.Parse(TextBox1.Text) - double.Parse(TextBox3.Text));
            }
        }
        TotalPayableAmount.Text = total.ToString("0.00");
        totalAmt.Text = (double.Parse(BounceCharges.Text) + double.Parse(TotalPayableAmount.Text)).ToString("0.00");
        if (total > 0 && isPending(studentId))
        {
            divBtn.Visible = true;
        }
        else
        {
            divBtn.Visible = false;
        }
        totalAmt.Text = (total + double.Parse(BounceCharges.Text)).ToString("0.00");
    }
}