using c4SmsNew;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web.UI.WebControls;

namespace _2
{
    public partial class admin_CCCollection : System.Web.UI.Page
    {
        private SqlConnection con;
        private readonly Campus oo;
        private string sql;
        public admin_CCCollection()
        {
            con = new SqlConnection();
            oo = new Campus();
            sql = string.Empty;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //con.ConnectionString = ConfigurationManager.ConnectionStrings["Menu"].ConnectionString;
            con = oo.dbGet_connection();
            Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file

            if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
            {
                Response.Redirect("default.aspx");
            }
            txtSrNo.Focus();
            if (!IsPostBack)
            {
                Session["RecieptNo"] = "";

                Session["CheckRefresh"] = Server.UrlDecode(DateTime.Now.ToString(CultureInfo.InvariantCulture));
                checkbool();

                oo.AddDateMonthYearDropDown(DDYear, DDMonth, DDDate);
                oo.FindCurrentDateandSetinDropDown(DDYear, DDMonth, DDDate);
                oo.AddDateMonthYearDropDown(DDYearP, DDMonthP, DDDateP);
                oo.FindCurrentDateandSetinDropDown(DDYearP, DDMonthP, DDDateP);
            }
        }

        private void loadCC()
        {
            string studentId = Request.Form[hfStudentId.UniqueID];
            if (studentId == string.Empty)
            {
                studentId = txtSrNo.Text.Trim();
            }

            string oldsessionname = "";

            sql = "Select *from StudentOfficialDetails where SrNo='" + studentId + "' and SessionNAme='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            if (BAL.objBal.Duplicate(sql))
            {
                sql = "Select Top 1 SessionName from StudentOfficialDetails where SrNo='" + studentId + "' and BranchCode=" + Session["BranchCode"] + " order by id Desc";
                oldsessionname = BAL.objBal.ReturnTag(sql, "SessionName");
            }
            else
            {
                sql = "Select Top 1 SessionName from StudentOfficialDetails where SrNo='" + studentId + "' and BranchCode=" + Session["BranchCode"] + " order by id Desc";
                oldsessionname = BAL.objBal.ReturnTag(sql, "SessionName");
            }

            sql = "Select Name StudentName,FatherName,MotherName,Nationality,Category,convert(nvarchar,DOB,106) as DOB,DatePart(dd,DOB) as date,DateName(mm,DOB) as month,DatePart(yyyy,DOB) as year from AllStudentRecord_UDF('" + oldsessionname + "'," + Session["BranchCode"] + ") where SrNo='" + studentId + "'";
            txtStudentName.Text = BAL.objBal.ReturnTag(sql, "StudentName");
            txtFatherName.Text = BAL.objBal.ReturnTag(sql, "FatherName");

            string status = "";
            sql = "Select Status From StudentWithdrawal where srno='" + studentId + "' and BranchCode=" + Session["BranchCode"] + "";
            status = BAL.objBal.ReturnTag(sql, "Status");

            sql = "Select convert(nvarchar,DateOfAdmiission,106) as DateOfAdmiission,ClassName, CombineClassName, ClassId, BranchId from AllStudentRecord_UDF('" + oldsessionname + "'," + Session["BranchCode"] + ") where SrNo='" + studentId + "'";
            string classname = BAL.objBal.ReturnTag(sql, "CombineClassName");
            string ClassId = BAL.objBal.ReturnTag(sql, "ClassId");
            string BranchId = BAL.objBal.ReturnTag(sql, "BranchId");
            if (status.ToUpper() == "PASSED")
            {
                txtClass.Text = classname;
                ddlStudentStatus.SelectedValue = "PASSED";
                txtyear.Text = oldsessionname;
            }
            else if (status.ToUpper() == "FAILED")
            {
                txtClass.Text = classname;
                if (BAL.objBal.convertRomantostring(classname).ToUpper() == "TENTH" || BAL.objBal.convertRomantostring(classname).ToUpper() == "TWELTH")
                {
                    ddlStudentStatus.SelectedValue = "FAILED";
                }
                else
                {
                    ddlStudentStatus.SelectedValue = "DETAINED";
                }
                txtyear.Text = oldsessionname;

            }
            else if (status.ToUpper() == "ABSENT")
            {

                txtClass.Text = classname;

                ddlStudentStatus.SelectedValue = "ABSENT";

                txtyear.Text = oldsessionname;
            }
            else
            {
                txtClass.Text = "";
                ddlStudentStatus.SelectedValue = "";
                txtyear.Text = "";
            }
            sql = "select sum(BounceCharges) BounceCharges from (select isnull(sum(isnull(BounceCharges, 0)), 0) BounceCharges from CCCollection where srno = '" + studentId + "' and Status = 'Cancelled' union all select(isnull(sum(isnull(BounceCharges, 0)), 0) * (-1)) BounceCharges from CCCollection where srno = '" + studentId + "' and Status = 'Paid' )T1";
            txtCBFee.Text = oo.ReturnTag(sql, "BounceCharges");

            sql = "select Amount from CCFormFeeMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and Classid=" + ClassId + " and Branchid=case when '" + BranchId + "'='' then Branchid else '" + BranchId + "' end";
            txtAmt.Text = oo.ReturnTag(sql, "Amount");
            txtConcession.Text = "0";

            if (txtAmt.Text == "")
            {
                txtAmt.Text = "0";
            }
            txtReceviedAmount.Text = ((double.Parse(txtAmt.Text) + double.Parse(txtCBFee.Text)) - double.Parse(txtConcession.Text)).ToString();
            Display(studentId);
        }

        protected void checkbool()
        {
            try
            {
                sql = "Select flag from BarcodeSetting";
                if (!string.IsNullOrEmpty(oo.ReturnTag(sql, "flag")))
                {
                    if (oo.ReturnTag(sql, "flag") == "0")
                    {
                        txtSrNo.AutoPostBack = false;
                    }
                    else
                    {
                        txtSrNo.AutoPostBack = true;
                    }
                }
                else
                {
                    txtSrNo.AutoPostBack = false;
                }
            }
            catch
            {
                txtSrNo.AutoPostBack = false;
            }
        }

        public void Display(string studentId)
        {
            sql = "select Row_Number() over (order by Id Asc) as SNo ,id,srno,RecieptNo,StudentName,FatherName,Class,LoginName,BranchCode,convert(nvarchar,FeeDepositeDate,106) as FeeDepositeDate,convert(nvarchar,ISNULL(CCissuedate,FeeDepositeDate),106) as CCissuedate,Sex,FatherContactNo,ReceivedAmount,case when cancel IS NULL then 'No' else 'Yes' end as Cancel, Status, Mop  from CCCollection where srno='" + studentId + "' and BranchCode=" + Session["BranchCode"] + " order by id desc";
            Grd.DataSource = oo.GridFill(sql);
            Grd.DataBind();
            for (int i = 0; i < Grd.Rows.Count; i++)
            {
                Label Status = (Label)Grd.Rows[i].FindControl("Status");
                Label MOP = (Label)Grd.Rows[i].FindControl("MOP");
                LinkButton LinkButton1 = (LinkButton)Grd.Rows[i].FindControl("LinkButton1");
                LinkButton LinkButton7 = (LinkButton)Grd.Rows[i].FindControl("LinkButton7");
                //LinkButton LinkButton2s = (LinkButton)Grd.Rows[i].FindControl("LinkButton2s");


                if (Status.Text.Trim() == "Pending" && (MOP.Text.Trim() == "Cheque" || MOP.Text.Trim() == "Other"))
                {
                    Grd.Rows[i].BackColor = System.Drawing.Color.OrangeRed;
                    for (int j = 0; j < Grd.Rows[i].Cells.Count; j++)
                    {
                        Grd.Rows[i].Cells[j].BackColor = System.Drawing.Color.OrangeRed;
                        Grd.Rows[i].Cells[j].ForeColor = System.Drawing.Color.Black;
                    }
                    Grd.Rows[i].ForeColor = System.Drawing.Color.White;
                    //LinkButton2s.ForeColor = System.Drawing.Color.White;
                    LinkButton1.Text = "<i class='fa fa-lock'></i>";
                    LinkButton7.Text = "<i class='fa fa-lock'></i>";
                    LinkButton1.Enabled = false;
                    LinkButton7.Enabled = false;
                }

                if (Status.Text.Trim() == "Cancelled" && (MOP.Text.Trim() == "Cheque" || MOP.Text.Trim() == "Other"))
                {
                    LinkButton1.Text = "<i class='fa fa-lock'></i>";
                    LinkButton7.Text = "<i class='fa fa-lock'></i>";
                    LinkButton1.Enabled = false;
                    LinkButton7.Enabled = false;
                }
            }

        }
        public string IDGeneration(string FixedString, string x)
        {
            string xx = "";
            switch (x.Length)
            {
                case 1:
                    xx = FixedString + "000000" + x;
                    break;
                case 2:
                    xx = FixedString + "00000" + x;
                    break;
                case 3:
                    xx = FixedString + "0000" + x;
                    break;
                case 4:
                    xx = FixedString + "000" + x;
                    break;
                case 5:
                    xx = FixedString + "00" + x;
                    break;
                case 6:
                    xx = FixedString + "0" + x;
                    break;
                default:
                    xx = FixedString + x;
                    break;
            }
            return "CC/" + Session["SessionName"] + "/" + xx;
        }

        protected void LinkRecept_Click(object sender, EventArgs e)
        {
            var chk = (LinkButton)sender;
            var lblId3 = (Label)chk.NamingContainer.FindControl("Label18");
            var ss = lblId3.Text;
            Session["IsDuplicate"] = "Yes";
            Session["CCRecieptNo"] = ss;
            Response.Redirect("CCReciept.aspx");
        }
        protected void Submit_Click(object sender, EventArgs e)
        {
            string xx = "";

            xx = oo.FindRecieptNo();
            if (xx == "")
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Please Initilize receipt no.!", "A");
                return;
            }
            if ((DropDownMOD.SelectedValue == "Cheque" || DropDownMOD.SelectedValue == "DD") && TextBox2.Text.Trim() == "")
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "please enter Instrument No.", "A");
                return;
            }
            if ((DropDownMOD.SelectedValue == "Card") && TextBox2.Text.Trim() == "")
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "please enter Card No.", "A");
                return;
            }
            if ((DropDownMOD.SelectedValue == "Online Transfer" || DropDownMOD.SelectedValue == "Other") && TextBox2.Text.Trim() == "")
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "please enter Ref. No.", "A");
                return;
            }
            if (Session["CheckRefresh"].ToString() == ViewState["CheckRefresh"].ToString())
            {
                string studentId = Request.Form[hfStudentId.UniqueID];
                if (studentId == string.Empty)
                {
                    studentId = txtSrNo.Text.Trim();
                }

                int co;
                string dd = "";


                dd = DDYear.SelectedItem + "/" + DDMonth.SelectedItem + "/" + DDDate.SelectedItem;

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CCCollectionProc";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@RecieptNo", xx);
                cmd.Parameters.AddWithValue("@srno", studentId);
                cmd.Parameters.AddWithValue("@StudentName", txtStudentName.Text);
                cmd.Parameters.AddWithValue("@FatherName", txtFatherName.Text);
                cmd.Parameters.AddWithValue("@Class", txtClass.Text.Trim());
                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                cmd.Parameters.AddWithValue("@sex", drpSex.SelectedItem.ToString().Trim());
                cmd.Parameters.AddWithValue("@fatherContactNo", txtContactNo.Text);
                cmd.Parameters.AddWithValue("@amount", txtAmt.Text);
                cmd.Parameters.AddWithValue("@sessionName", Session["SessionName"].ToString());
                cmd.Parameters.AddWithValue("@Remark", txtRemark.Text.Trim());
                cmd.Parameters.AddWithValue("@FeeDepositeDate", dd);
                cmd.Parameters.AddWithValue("@CCissuedate", dd);
                if (txtConcession.Text == "")
                {
                    cmd.Parameters.AddWithValue("@Concession", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Concession", txtConcession.Text.Trim());
                }
                cmd.Parameters.AddWithValue("@ReceivedAmount", txtReceviedAmount.Text.Trim());

                if (DropDownMOD.SelectedValue == "Cheque" || DropDownMOD.SelectedValue == "Other")
                {
                    cmd.Parameters.AddWithValue("@CheckDDNo", TextBox2.Text);
                    cmd.Parameters.AddWithValue("@CheckDate", txtchequeDate.Text);
                    cmd.Parameters.AddWithValue("@BankName", TextBox3.Text);
                    cmd.Parameters.AddWithValue("@Status", ddlChequeStatus.SelectedValue);
                    if (ddlChequeStatus.SelectedValue == "Paid")
                    {
                        cmd.Parameters.AddWithValue("@ChequeStatus", "Paid");
                    }
                }
                if (DropDownMOD.SelectedValue != "Cheque" && DropDownMOD.SelectedValue != "Other" && DropDownMOD.SelectedValue != "Cash")
                {
                    cmd.Parameters.AddWithValue("@CheckDDNo", TextBox2.Text);
                    cmd.Parameters.AddWithValue("@CheckDate", DBNull.Value);
                    cmd.Parameters.AddWithValue("@BankName", TextBox3.Text);
                    cmd.Parameters.AddWithValue("@Status", "Paid");
                    cmd.Parameters.AddWithValue("@ChequeStatus", "Paid");
                }
                if (DropDownMOD.SelectedValue == "Cash")
                {
                    cmd.Parameters.AddWithValue("@ChequeStatus", "Paid");
                    cmd.Parameters.AddWithValue("@Status", "Paid");
                }
                double BounceCharges = double.Parse(txtCBFee.Text.Trim() == string.Empty ? "0" : txtCBFee.Text.Trim());
                if (BounceCharges > 0)
                {
                    cmd.Parameters.AddWithValue("@BounceCharges", BounceCharges.ToString("0.00"));
                }
                cmd.Parameters.AddWithValue("@MOP", DropDownMOD.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@passfail", ddlStudentStatus.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@year", txtyear.Text.Trim());

                SqlParameter outputValue = new SqlParameter("@result", "");
                outputValue.Size = 0x100;
                outputValue.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(outputValue);
                cmd.Connection = con;
                string recieptSrNo = "";
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    recieptSrNo = cmd.Parameters["@result"].Value.ToString();
                    Display(studentId);
                    //oo.MessageBox("Submited Successfully.", this.Page);
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Submited Successfully.", "S");

                    //Session["CCRecieptNo"] = xx;
                    Session["IsDuplicate"] = "No";
                    string qstr = recieptSrNo.Replace("/", "__");
                    ComposeSMS(recieptSrNo);//--New
                    Response.Redirect("CCReciept.aspx?print=1&CCRecieptNo=" + qstr, false);
                    //SendFeesSms(txtContactNo.Text, recieptSrNo, txtAmt.Text, "4");//--Old
                }
                catch (Exception)
                {
                    // ignored
                }
            }
            else
            {
                //oo.MessageBox("Please Do Not Press Refresh Button or back Button", this.Page);
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Please Do Not Press Refresh Button or back Button", "A");
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
                DataSet ds = oo.ReturnDataSet("USP_CCFeeTemplate", param.ToArray());
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

                SendFeesSms(fatherContactNo, msg, "4");

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
                sql = "Select HitValue from SMSActivateDeactivate where BranchCode=" + Session["BranchCode"] + "";
                if (oo.ReturnTag(sql, "HitValue") == "") return res;
                if (oo.ReturnTag(sql, "HitValue") != "true") return res;
                string sql1 = "Select SmsSent From SmsEmailMaster Where Id='" + smsPageId + "' and BranchCode='" + Session["BranchCode"] + "'";
                if (oo.ReturnTag(sql1, "SmsSent").Trim() == "true")
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

        //		public void SendFeesSms(string FmobileNo, string RecieptNo, string Amount, string title)
        //		{
        //			sql = "Select HitValue from SMSActivateDeactivate where BranchCode=" + Session["BranchCode"] + "";
        //			if (oo.ReturnTag(sql, "HitValue") != "")
        //			{
        //				if (oo.ReturnTag(sql, "HitValue") == "true")
        //				{
        //					SMSAdapterNew sadpNew = new SMSAdapterNew();
        //					string mess = "";
        //#pragma warning disable 219
        //					string collegeTitle = "";
        //#pragma warning restore 219
        //					// INR {#var#} received towards C.C. Fee of {#var#}. Receipt No. {#var#}From, RRSLKO
        //					mess = "INR " + Amount + " received towards C.C. Fee of CharacterCertificate. Receipt No. " + RecieptNo + " From, ";
        //					string sms_response = "";

        //					if (FmobileNo != "")
        //					{

        //						sql = "Select SmsSent From SmsEmailMaster where Id='4' ";
        //						if (oo.ReturnTag(sql, "SmsSent").Trim() == "true")
        //						{
        //							sms_response = sadpNew.Send(mess, FmobileNo, title);
        //						}
        //					}
        //				}
        //			}
        //		}

        public void SendFeescancleSms(string FmobileNo, string RecieptNo, string Amount, string title)
        {
            sql = "Select HitValue from SMSActivateDeactivate where BranchCode=" + Session["BranchCode"] + "";
            if (oo.ReturnTag(sql, "HitValue") != "")
            {
                if (oo.ReturnTag(sql, "HitValue") == "true")
                {
                    SMSAdapterNew sadpNew = new SMSAdapterNew();
                    string mess = "";
                    // Receipt No. {#var#} has been cancelled of C.C. Fee. Refunded Amount is INR {#var#}From, RRSLKO
                    mess = "Receipt No. " + RecieptNo + " has been cancelled of C.C. Fee. Refunded Amount is INR " + Amount + " From, ";
                    string sms_response = "";

                    if (FmobileNo != "")
                    {
                        sql = "Select SmsSent From SmsEmailMaster where Id='4' and BranchCode='" + Session["BranchCode"] + "'";
                        if (oo.ReturnTag(sql, "SmsSent").Trim() == "true")
                        {
                            sms_response = sadpNew.Send(mess, FmobileNo, title);
                        }
                    }
                }
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            ViewState["CheckRefresh"] = Session["CheckRefresh"];
        }
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            LinkButton chk = (LinkButton)sender;
            Label lblId3 = (Label)chk.NamingContainer.FindControl("Label38");
            string ss = lblId3.Text;
            Session["IsDuplicate"] = "Yes";
            //Session["CCRecieptNo"] = ss;
            Response.Redirect("CCReciept.aspx?print=1&CCRecieptNo=" + ss);

        }

        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            string studentId = Request.Form[hfStudentId.UniqueID];
            if (studentId == string.Empty)
            {
                studentId = txtSrNo.Text.Trim();
            }
            if (Session["CheckRefresh"].ToString() == ViewState["CheckRefresh"].ToString())
            {
                string dd = "";
                dd = DDYearP.SelectedItem + "/" + DDMonthP.SelectedItem + "/" + DDDateP.SelectedItem;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "CCCollectionUpdateProc";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RecieptNo", lblvalue.Text);
                    //cmd.Parameters.AddWithValue("@StudentName", txtStudentNamePanel.Text.Trim());
                    //cmd.Parameters.AddWithValue("@FatherName", txtFatherNamePanel.Text.Trim());
                    //cmd.Parameters.AddWithValue("@sex", drpSexPanel.SelectedItem.ToString().Trim());
                    cmd.Parameters.AddWithValue("@Class", txtClassPanel.Text.Trim());
                    cmd.Parameters.AddWithValue("@passfail", drpStatusPanel.SelectedValue.Trim());
                    cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                    cmd.Parameters.AddWithValue("@year", txtyearPanel.Text.Trim());
                    cmd.Connection = con;
                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        //oo.MessageBox("Updated Successfully.", this.Page);
                        Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Updated Successfully.", "S");

                        Display(studentId);
                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                }
            }
            else
            {
                //oo.MessageBox("Please Do Not Press Refresh Button or back Button", this.Page);
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Please Do Not Press Refresh Button or back Button", "A");
            }
        }
        protected void LinkButton5_Click(object sender, EventArgs e)
        {

        }
        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            LinkButton chk = (LinkButton)sender;
            Label lblId2 = (Label)chk.NamingContainer.FindControl("Label38");
            string ss = lblId2.Text;
            lblvalue.Text = ss;
            Panel2_ModalPopupExtender.Show();
            Button8.Focus();
        }
        protected void LinkButton7_Click(object sender, EventArgs e)
        {
            LinkButton chk = (LinkButton)sender;
            Label lblId2 = (Label)chk.NamingContainer.FindControl("Label38");
            string ss = lblId2.Text;
            lblvalue.Text = ss;

            sql = "Select StudentName,FatherName,Class,passfail,year,sex from CCCollection where RecieptNo='" + ss + "' and BranchCode=" + Session["BranchCode"] + " and Cancel is null";
            txtClassPanel.Text = BAL.objBal.ReturnTag(sql, "Class");
            drpStatusPanel.SelectedValue = BAL.objBal.ReturnTag(sql, "passfail");
            txtyearPanel.Text = BAL.objBal.ReturnTag(sql, "year");

            txtClassPanel.Focus();

            Panel1_ModalPopupExtender.Show();
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string studentId = Request.Form[hfStudentId.UniqueID];
            if (studentId == string.Empty)
            {
                studentId = txtSrNo.Text.Trim();
            }
            sql = "select FatherContactNo,ReceivedAmount from CCCollection where RecieptNo='" + lblvalue.Text + "' and BranchCode=" + Session["BranchCode"] + " and Cancel is null";
            string FatherContactNo = oo.ReturnTag(sql, "FatherContactNo");
            string Amount = oo.ReturnTag(sql, "ReceivedAmount");
            sql = "update CCCollection set Cancel='Y' where RecieptNo='" + lblvalue.Text + "'";
            oo.ProcedureDatabase(sql);
            SendFeescancleSms(FatherContactNo, lblvalue.Text, Amount, "30");
            //oo.MessageBox("Successfully Canceled.", this.Page);
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Successfully Canceled.", "S");
            Display(studentId);
        }
        protected void Button8_Click(object sender, EventArgs e)
        {

        }
        protected void DDYear_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void DDMonth_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void DDDate_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void LinkButton6_Click(object sender, EventArgs e)
        {
            string studentId = Request.Form[hfStudentId.UniqueID];
            if (studentId == string.Empty)
            {
                txtSrNo.Text = txtSrNo.Text.Replace("&", "/").Trim();
            }
            showDetails();
            string ss = "";
            ss = ss + " select classid from CCFormFeeMaster where BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "'";
            ss += " and Classid=(Select top(1) AdmissionForClassId from StudentOfficialDetails where SrNo='" + studentId + "' and SessionNAme='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + ")";
            if (!oo.Duplicate(ss))
            {
                divTools.Visible = false;
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Masters not created!", "A");
                return;
            }
        }
        public void showDetails()
        {
            try
            {
                string studentId = Request.Form[hfStudentId.UniqueID];
                if (studentId == string.Empty)
                {
                    studentId = txtSrNo.Text.Trim();
                }
                sql = "select Top 1 SessionName from StudentGenaralDetail where srno='" + studentId + "' and BranchCode=" + Session["BranchCode"] + " order By Id Desc";
                if (oo.Duplicate(sql))
                {
                    string Top_sessionName = oo.ReturnTag(sql, "SessionName");

                    sql = "select SectionName,Card,Gender,Medium as Medium,FatherContactNo, combineClassName,ClassName,convert(nvarchar,DateOfAdmiission,106) as DateOfAdmiission ,SectionId,FatherName,MotherName,FirstName, ";
                    sql += "  MiddleName,LastName,StEnRCode as StEnRCode,srno  as srno,case  when TransportRequired='Yes' then 'Yes' else 'No' end as TransportRequired,BranchName,FamilyContactNo,PhotoPath ";
                    sql += "  from AllStudentRecord_UDF('" + Top_sessionName + "'," + Session["BranchCode"] + ") where srno='" + studentId + "'";

                    GrdStudent.DataSource = oo.GridFill(sql);
                    GrdStudent.DataBind();
                    Permission_Values();
                    Concession_Values();
                    txtAmt.Focus();
                    DataSet ds;
                    ds = oo.GridFill(sql);

                    // ReSharper disable once UseNullPropagation
                    if (ds != null && GrdStudent.Rows.Count > 0)
                    {
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
                    txtContactNo.Text = oo.ReturnTag(sql, "FatherContactNo");

                    drpSex.SelectedItem.Text = oo.ReturnTag(sql, "Gender");
                    divTools.Visible = true;
                    loadCC();
                }
                else
                {
                    divTools.Visible = false;
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Invalid S.R. No.", "A");
                }
            }
            catch
            {
                divTools.Visible = false;
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Invalid S.R. No.", "A");
            }
        }

        protected void TxtEnter_TextChanged(object sender, EventArgs e)
        {
            string studentId = Request.Form[hfStudentId.UniqueID];
            if (studentId == string.Empty)
            {
                txtSrNo.Text = txtSrNo.Text.Replace("&", "/").Trim();
            }
            showDetails();
            string ss = "";
            ss += " select classid from CCFormFeeMaster where BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "'";
            ss += " and Classid=(Select top(1) AdmissionForClassId from StudentOfficialDetails where SrNo='" + studentId + "' and SessionNAme='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + ")";
            if (!oo.Duplicate(ss))
            {
                divTools.Visible = false;
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Masters not created!", "A");
                return;
            }
        }
        protected void DrpEnter_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void txtAmt_TextChanged(object sender, EventArgs e)
        {
            calculateAmount();
            txtConcession.Focus();
        }

        public void calculateAmount()
        {
            double Concession;
            double Amount;
            double RAmount;
            if (txtConcession.Text != "")
            {
                Concession = Convert.ToDouble(txtConcession.Text);
            }
            else
            {
                Concession = 0;
            }
            Amount = Convert.ToDouble(txtAmt.Text);
            RAmount = Amount - Concession;
            txtReceviedAmount.Text = RAmount.ToString(CultureInfo.InvariantCulture);
        }
        protected void txtConcession_TextChanged(object sender, EventArgs e)
        {
            calculateAmount();
            txtReceviedAmount.Focus();
        }

        public void Concession_Values()
        {
            Label lbl_std_srno = (Label)GrdStudent.Rows[0].FindControl("Label1");
            con.Open();
            using (SqlCommand Permission_check = new SqlCommand("select count(*) Count from Concession_Permission where TableId='7' and SrNo='" + lbl_std_srno.Text + "' and concession<>0 and reset is null and SessionName='" + Session["SessionName"] + "'", con))
            {
                int Permission_check_count = (int)Permission_check.ExecuteScalar();
                con.Close();
                if (Permission_check_count == 1)
                {
                    sql = "select concession from Concession_Permission where TableId='7' and SrNo='" + lbl_std_srno.Text + "' and concession<>0 and reset is null and SessionName='" + Session["SessionName"] + "'";
                    string permission = oo.ReturnTag(sql, "concession");
                    txtConcession.Text = permission;
                }
                else
                {
                    txtConcession.Text = "";
                }
            }
        }
        public void Permission_Values()
        {
            string sql8;
            sql8 = "select Enable from Admin_fee_permission_setting where Tableid='7' and SessionName='" + Session["SessionName"] + "'";
            if (oo.ReturnTag(sql8, "Enable") == "" || oo.ReturnTag(sql8, "Enable") == "No,1,2,3" || oo.ReturnTag(sql8, "Enable") == "Yes,1,2,3" || oo.ReturnTag(sql8, "Enable") == "Yes,1" || oo.ReturnTag(sql8, "Enable") == "Yes,2" || oo.ReturnTag(sql8, "Enable") == "Yes,3" || oo.ReturnTag(sql8, "Enable") == "Yes,1,2" || oo.ReturnTag(sql8, "Enable") == "Yes,1,3" || oo.ReturnTag(sql8, "Enable") == "Yes,2,3")
            {

                Label lbl_std_srno = (Label)GrdStudent.Rows[0].FindControl("Label1");
                con.Open();
                using (SqlCommand Permission_check = new SqlCommand("select count(*) from Administrator_Permission where Tableid='7' and SrNo='" + lbl_std_srno.Text + "' and Permission_Session='" + Session["SessionName"] + "' and Permission like 'Yes%'", con))
                {
                    int Permission_check_count = (int)Permission_check.ExecuteScalar();
                    con.Close();

                    if (Permission_check_count == 1)
                    {

                        sql = "select permission from Administrator_Permission where Tableid='7' and SrNo='" + lbl_std_srno.Text + "' and Permission_Session='" + Session["SessionName"] + "' and Permission like 'Yes%'";
                        string[] permission = oo.ReturnTag(sql, "permission").Split(',');
                        if (permission.Length > 1)
                        {
                            DDYear.Enabled = false;
                            DDMonth.Enabled = false;
                            DDDate.Enabled = false;
                            txtConcession.Enabled = false;
                            for (int i = 1; i <= permission.Length - 1; i++)
                            {
                                if (permission[i] == "1")
                                {
                                    DDYear.Enabled = true;
                                    DDMonth.Enabled = true;
                                    DDDate.Enabled = true;
                                }
                                if (permission[i] == "2")
                                {
                                    txtConcession.Enabled = true;
                                }
                            }
                        }
                        else
                        {
                            string[] permission1 = oo.ReturnTag(sql8, "Enable").Split(',');
                            if (permission1.Length > 1)
                            {
                                DDYear.Enabled = false;
                                DDMonth.Enabled = false;
                                DDDate.Enabled = false;
                                txtConcession.Enabled = false;

                                if (permission1[0] == "Yes")
                                {
                                    for (int i = 1; i <= permission1.Length - 1; i++)
                                    {
                                        if (permission1[i] == "1")
                                        {
                                            DDYear.Enabled = true;
                                            DDMonth.Enabled = true;
                                            DDDate.Enabled = true;
                                        }
                                        if (permission1[i] == "2")
                                        {
                                            txtConcession.Enabled = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        string[] permission2 = oo.ReturnTag(sql8, "Enable").Split(',');
                        if (permission2.Length > 1)
                        {
                            DDYear.Enabled = false;
                            DDMonth.Enabled = false;
                            DDDate.Enabled = false;
                            txtConcession.Enabled = false;

                            if (permission2[0] == "Yes")
                            {
                                for (int i = 1; i <= permission2.Length - 1; i++)
                                {
                                    if (permission2[i] == "1")
                                    {
                                        DDYear.Enabled = true;
                                        DDMonth.Enabled = true;
                                        DDDate.Enabled = true;
                                    }
                                    if (permission2[i] == "2")
                                    {
                                        txtConcession.Enabled = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        protected void DropDownMOD_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownMOD.SelectedIndex == 0)
            {
                table1.Visible = false;
                table2.Visible = false;
                table4.Visible = false;
                table12.Visible = false;
            }
            else
            {
                table1.Visible = true;
                table2.Visible = true;
                table12.Visible = true;
                if (DropDownMOD.SelectedValue == "Cheque" || DropDownMOD.SelectedValue == "DD")
                {
                    Label42.Text = "Instrument No.";
                    lblchequedate.Text = "Instrument Date";
                    Label6.Text = "Cheque Status";
                }
                if (DropDownMOD.SelectedValue == "Card")
                {
                    Label42.Text = "Card No.";
                    lblchequedate.Text = "Card Date";
                }
                if (DropDownMOD.SelectedValue == "Online Transfer" || DropDownMOD.SelectedValue == "Other")
                {
                    Label42.Text = "Ref. No.";
                    lblchequedate.Text = "Ref. Date";
                    Label6.Text = "Ref. Status";

                }
                if (DropDownMOD.SelectedValue == "Cheque" || DropDownMOD.SelectedValue == "Other")
                {
                    table4.Visible = true;

                }
                else
                {
                    table4.Visible = false;
                }
            }
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            LinkButton chk = (LinkButton)sender;
            Label lblId2 = (Label)chk.NamingContainer.FindControl("Label38");
            Session["RecieptNo"] = lblId2.Text;
            Response.Redirect("CCprint.aspx?print=1");
        }

        public override void Dispose()
        {
            con.Dispose();
            oo.Dispose();
        }

        protected void ddlStudentStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            string studentId = Request.Form[hfStudentId.UniqueID];
            if (studentId == string.Empty)
            {
                studentId = txtSrNo.Text.Trim();
            }

            string oldsessionname = "";

            sql = "Select *from StudentOfficialDetails where SrNo='" + studentId + "' and SessionNAme='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            if (BAL.objBal.Duplicate(sql))
            {
                sql = "Select Top 1 SessionName from (Select Top 2 SessionName,id from StudentOfficialDetails where SrNo='" + studentId + "' and BranchCode=" + Session["BranchCode"] + " order by id Desc) As T1 order by id";
                oldsessionname = BAL.objBal.ReturnTag(sql, "SessionName");
            }
            else
            {
                sql = "Select Top 1 SessionName from StudentOfficialDetails where SrNo='" + studentId + "' and BranchCode=" + Session["BranchCode"] + " order by id Desc";
                oldsessionname = BAL.objBal.ReturnTag(sql, "SessionName");
            }
            sql = "Select convert(nvarchar,DateOfAdmiission,106) as DateOfAdmiission,ClassName, ClassId, BranchId from AllStudentRecord_UDF('" + oldsessionname + "'," + Session["BranchCode"] + ") where SrNo='" + studentId + "'";
            string classname = BAL.objBal.ReturnTag(sql, "ClassName");
            if (ddlStudentStatus.SelectedValue.ToUpper() == "PASSED")
            {
                txtClass.Text = classname;
                ddlStudentStatus.SelectedValue = "PASSED";
                txtyear.Text = oldsessionname;
            }
            else if (ddlStudentStatus.SelectedValue.ToUpper() == "FAILED")
            {
                txtClass.Text = classname;
                if (BAL.objBal.convertRomantostring(classname).ToUpper() == "TENTH" || BAL.objBal.convertRomantostring(classname).ToUpper() == "TWELTH")
                {
                    ddlStudentStatus.SelectedValue = "FAILED";
                }
                else
                {
                    ddlStudentStatus.SelectedValue = "DETAINED";
                }
                txtyear.Text = oldsessionname;

            }
            else if (ddlStudentStatus.SelectedValue.ToUpper() == "ABSENT")
            {

                txtClass.Text = classname;

                ddlStudentStatus.SelectedValue = "ABSENT";

                txtyear.Text = oldsessionname;
            }
            else if (ddlStudentStatus.SelectedValue.ToUpper() == "ESSENTIALREPEAT")
            {

                txtClass.Text = classname;

                ddlStudentStatus.SelectedValue = "ESSENTIALREPEAT";

                txtyear.Text = oldsessionname;
            }
            else
            {
                txtClass.Text = "";
                ddlStudentStatus.SelectedValue = "";
                txtyear.Text = "";
            }
        }
    }
}