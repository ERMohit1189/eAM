using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;
using c4SmsNew;

public partial class admin_ArrierDeposit : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";
#pragma warning disable 414
    bool FailedStudent;
#pragma warning restore 414
    protected void Page_Load(object sender, EventArgs e)
    {
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file

        if (!IsPostBack)
        {
            Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());
            checkbool();
            TxtEnter.Focus();
            oo.AddDateMonthYearDropDown(DDYear, DDMonth, DDDate);
            oo.FindCurrentDateandSetinDropDown(DDYear, DDMonth, DDDate);
            Panel1.Visible = false;
            FailedStudent = false;
        }

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
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        TxtEnter.Text = TxtEnter.Text.Replace("&", "/").Trim();
        showDetails();
       
           
    }

    public void showDetails()
    {
        if (TxtEnter.Text != "")
        {
            TxtEnter.Text = TxtEnter.Text.Replace(" ", string.Empty);
        }
        sql = "select SG.Id, SC.SectionName,SO.Card,SO.Medium as Medium,CM.ClassName,convert(nvarchar,So.DateOfAdmiission,106) as DateOfAdmiission ,SO.SectionId,Sf.FatherName,SF.MotherName,SG.FirstName,SG.MiddleName,SG.LastName,sg.StEnRCode as StEnRCode,sg.srno  as srno,case  when so.TransportRequired='Yes' then 'Yes' else 'No' end as TransportRequired,so.wayamount as wayamount from StudentGenaralDetail SG ";
        sql = sql + " left join StudentFamilyDetails SF on SG.SrNo=SF.SrNo";
        sql = sql + " left join StudentOfficialDetails SO on SG.SrNo=SO.SrNo";
        sql = sql + " left join ClassMaster CM on SO.AdmissionForClassId=CM.Id";
        sql = sql + " left join SectionMaster SC on SO.SectionId=SC.Id";
        sql = sql + " where  SG." + DrpEnter.SelectedValue.ToString() + "=" + "'" + TxtEnter.Text.Trim() + "'";
        sql = sql + " and sg.SessionName='" + Session["SessionName"].ToString() + "' and ";
        sql = sql + " so.SessionName='" + Session["SessionName"].ToString() + "' and sf.SessionName='" + Session["SessionName"].ToString() + "' and cm.SessionName='" + Session["SessionName"].ToString() + "'";
        sql = sql + " and SC.SessionName='" + Session["SessionName"].ToString() + "'  and";

        sql = sql + " sg.BranchCode=" + Session["BranchCode"].ToString() + "";
        sql = sql + " and SO.Withdrwal is null";
        Grd.DataSource = oo.GridFill(sql);
        Grd.DataBind();
        Permission_Values();
        if (Grd.Rows.Count > 0)
        {

            FindArrierOfStudents();
            txtBalfee.Text = "";
            txtconfee.Text = "";
            txtConvance.Text = "";
            Concession_Values();
            findCalculation();
            findDepositAmt();
            table1.Visible = true;
        }
        else
        {
            table1.Visible = false;
            //oo.MessageBox("Sorry, No Record(s) not Found!", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, No Record(s) not Found!", "A");       

        }
    }


    public void Concession_Values()
    {
        if (Grd.Rows.Count > 0)
        {
            Label lbl_std_srno = (Label)Grd.Rows[0].FindControl("Label1");
            con.Open();
            SqlCommand Permission_check = new SqlCommand("select count(*) Count from Concession_Permission where TableId='2' and SrNo='" + lbl_std_srno.Text + "' and concession<>0 and reset is null and SessionName='" + Session["SessionName"].ToString() + "'", con);
            int Permission_check_count = (int)Permission_check.ExecuteScalar();
            con.Close();
            if (Permission_check_count == 1)
            {
                sql = "select concession from Concession_Permission where TableId='2' and SrNo='" + lbl_std_srno.Text + "' and concession<>0 and reset is null and SessionName='" + Session["SessionName"].ToString() + "'";
                string permission = oo.ReturnTag(sql, "concession").ToString();
                txtconfee.Text = permission;
            }
            else
            {
                txtconfee.Text = "";
            }
        }
    }
    public void Permission_Values()
    {
        string sql8;
        sql8 = "select Enable from Admin_fee_permission_setting where TableId='2' and SessionName='" + Session["SessionName"].ToString() + "'";
        if (oo.ReturnTag(sql8, "Enable") == "" || oo.ReturnTag(sql8, "Enable") == "No,1,2,3" || oo.ReturnTag(sql8, "Enable") == "Yes,1,2,3" || oo.ReturnTag(sql8, "Enable") == "Yes,1" || oo.ReturnTag(sql8, "Enable") == "Yes,2" || oo.ReturnTag(sql8, "Enable") == "Yes,3" || oo.ReturnTag(sql8, "Enable") == "Yes,1,2" || oo.ReturnTag(sql8, "Enable") == "Yes,1,3" || oo.ReturnTag(sql8, "Enable") == "Yes,2,3")
        {
            if (Grd.Rows.Count > 0)
            {
                Label lbl_std_srno = (Label)Grd.Rows[0].FindControl("Label1");
                con.Open();
                SqlCommand Permission_check = new SqlCommand("select count(*) from Administrator_Permission where TableId='2' and SrNo='" + lbl_std_srno.Text + "' and Permission_Session='" + Session["SessionName"].ToString() + "' and Permission like 'Yes%'", con);
                int Permission_check_count = (int)Permission_check.ExecuteScalar();
                con.Close();

                if (Permission_check_count == 1)
                {
                    sql = "select permission from Administrator_Permission where TableId='2' and SrNo='" + lbl_std_srno.Text + "' and Permission_Session='" + Session["SessionName"].ToString() + "' and Permission like 'Yes%'";
                    string[] permission = oo.ReturnTag(sql, "permission").Split(',');
                    if (permission.Length > 1)
                    {
                        DDYear.Enabled = false;
                        DDMonth.Enabled = false;
                        DDDate.Enabled = false;
                        txtconfee.Enabled = false;
                        txtreceiveAmount.Enabled = false;
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
                                txtconfee.Enabled = true;
                            }
                            if (permission[i].ToString() == "3")
                            {
                                txtreceiveAmount.Enabled = true;
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
                            txtconfee.Enabled = false;
                            txtreceiveAmount.Enabled = false;
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
                                        txtconfee.Enabled = true;
                                    }
                                    if (permission1[i].ToString() == "3")
                                    {
                                        txtreceiveAmount.Enabled = true;
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
                        txtconfee.Enabled = false;
                        txtreceiveAmount.Enabled = false;
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
                                    txtconfee.Enabled = true;
                                }
                                if (permission2[i].ToString() == "3")
                                {
                                    txtreceiveAmount.Enabled = true;
                                }

                            }
                        }
                    }
                }
            }
        }
    }
    protected void txttotamountOld_TextChanged(object sender, EventArgs e)
    {

    }
    protected void DrpEnter_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void TxtEnter_TextChanged(object sender, EventArgs e)
    {
        TxtEnter.Text = TxtEnter.Text.Replace("&", "/").Trim();
        showDetails();
    }
    protected void DDYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.YearDropDown(DDYear, DDMonth, DDDate);
    }
    protected void DDMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.MonthDropDown(DDYear, DDMonth, DDDate);

    }
    protected void txtConvance_TextChanged(object sender, EventArgs e)
    {
        findCalculation();
        findDepositAmt(); 
    }
    protected void txtArrier_TextChanged(object sender, EventArgs e)
    {
        findCalculation();
        findDepositAmt();
    }
    protected void txtconfee_TextChanged(object sender, EventArgs e)
    {
        findCalculation();
        findDepositAmt();
    }
  
    protected void txtBalfee_TextChanged(object sender, EventArgs e)
    {

    }
    protected void Submit_Click(object sender, EventArgs e)
    {
        if (Session["CheckRefresh"].ToString() == ViewState["CheckRefresh"].ToString())
        {
      
            string RecNo = "";
            RecNo = FindRecieptNo();

            if (RecNo == "")
            {
                //oo.MessageBox("Sorry, Please Initialize Receipt No.!", this.Page);
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, Please Initialize Receipt No.!", "A");       

            }
            else
            {
                string DepositDate = "";
                if ((string)Session["Failed"] == "Failed")
                {
                    string sess = "";

                    sql = "select ArrearAmt,arriersession from ArrierMast  where srno='" + TxtEnter.Text + "'";
                    sql = sql + "  and SessionName='" + Session["SessionName"].ToString() + "'";
                    sess = oo.ReturnTag(sql, "arriersession");
                    sql = "select SG.Id, SC.SectionName,SO.Card,SO.Medium as Medium,CM.ClassName,convert(nvarchar,So.DateOfAdmiission,106) as DateOfAdmiission ,SO.SectionId,Sf.FatherName,SF.MotherName,SG.FirstName,SG.MiddleName,SG.LastName,sg.StEnRCode as StEnRCode,sg.srno  as srno,case  when so.TransportRequired='Yes' then 'Yes' else 'No' end as TransportRequired,so.wayamount as wayamount from StudentGenaralDetail SG ";
                    sql = sql + "   left join StudentFamilyDetails SF on SG.StEnRCode=SF.StEnRCode";
                    sql = sql + "   left join StudentOfficialDetails SO on SG.StEnRCode=SO.StEnRCode";
                    sql = sql + "   left join ClassMaster CM on SO.AdmissionForClassId=CM.Id";
                    sql = sql + "   left join SectionMaster SC on SO.SectionId=SC.Id";
                    sql = sql + "   where  SG." + DrpEnter.SelectedValue.ToString() + "=" + "'" + TxtEnter.Text.Trim() + "'";
                    sql = sql + "   and sg.SessionName='" + sess + "' and ";
                    sql = sql + "   so.SessionName='" + sess + "' and sf.SessionName='" + sess + "' and cm.SessionName='" + sess + "'";
                    sql = sql + "   and SC.SessionName='" + sess + "'  and";
                    sql = sql + "   sg.BranchCode=" + Session["BranchCode"].ToString() + "";
                    sql = sql + "   and SO.Withdrwal is null";

                }
                else
                {

                    sql = "select ROW_NUMBER() OVER (ORDER BY SG.Id ASC) AS  SrNo,SG.Id, SC.SectionName as SectionName,CM.ClassName as ClassName,convert(nvarchar,So.DateOfAdmiission,106) as DateOfAdmiission ,SO.SectionId,Sf.FatherName,SF.MotherName,SG.FirstName,SG.MiddleName,SG.LastName,sg.StEnRCode as StEnRCode,sg.srno  as srno from StudentGenaralDetail SG ";
                    sql = sql + "    left join StudentFamilyDetails SF on SG.StEnRCode=SF.StEnRCode";
                    sql = sql + "   left join StudentOfficialDetails SO on SG.StEnRCode=SO.StEnRCode";
                    sql = sql + "   left join ClassMaster CM on SO.AdmissionForClassId=CM.Id";
                    sql = sql + "   left join SectionMaster SC on SO.SectionId=SC.Id";
                    sql = sql + "    where  SG." + DrpEnter.SelectedValue.ToString() + "=" + "'" + TxtEnter.Text.Trim() + "'";
                    sql = sql + " and sg.SessionName='" + Session["SessionName"].ToString() + "' and sg.BranchCode=" + Session["BranchCode"].ToString() + "";
                    sql = sql + "  and sf.SessionName='" + Session["SessionName"].ToString() + "'  and so.sessionName='" + Session["SessionName"].ToString() + "' and cm.SessionName='" + Session["SessionName"].ToString() + "'  and sc.SessionName='" + Session["SessionName"].ToString() + "'";
                }
                string clna = "";
                clna = oo.ReturnTag(sql, "ClassName");

                string sect = "";
                sect = oo.ReturnTag(sql, "SectionName");

                string ss = "";
                ss = oo.ReturnTag(sql, "srno");

                string se = "";

                se = oo.ReturnTag(sql, "StEnRCode");
                //
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FeeDepositeProc";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@RecieptSrNo", RecNo);
                cmd.Parameters.AddWithValue("@StEnRCode", se);
                cmd.Parameters.AddWithValue("@SrNo", ss);
                DepositDate = DDYear.SelectedValue.ToString() + "/" + DDMonth.SelectedValue.ToString() + "/" + DDDate.SelectedValue.ToString();
                cmd.Parameters.AddWithValue("@FeeDepositeDate", DepositDate);
                cmd.Parameters.AddWithValue("@FeeMonth", "Arrier");
                cmd.Parameters.AddWithValue("@TotalFeeAmount", txtArrier.Text.ToString());

                if (txtconfee.Text == "")
                {
                    cmd.Parameters.AddWithValue("@Cocession", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Cocession", txtconfee.Text.ToString());
                }




                cmd.Parameters.AddWithValue("@CurrentAmount", txtTotalAmt.Text.ToString());
                cmd.Parameters.AddWithValue("@RecievedAmount", txtreceiveAmount.Text.ToString());
                if (txtBalfee.Visible == false)
                {
                    cmd.Parameters.AddWithValue("@RemainingAmount", "0.00");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@RemainingAmount", txtBalfee.Text.ToString());
                }

                cmd.Parameters.AddWithValue("@Remark", TextBox9.Text.ToString());
                cmd.Parameters.AddWithValue("@AmountInWords", lblamountwords.Text.ToString());
                cmd.Parameters.AddWithValue("@Class", clna);
                cmd.Parameters.AddWithValue("@Section", sect);

                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());//Session["Login"].ToString());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
                cmd.Parameters.AddWithValue("@LateFeeAmount", "0");
                cmd.Parameters.AddWithValue("@BusConvience", "0");
                cmd.Parameters.AddWithValue("@FeeMode", "NULL");
                cmd.Parameters.AddWithValue("@BalanceMode", "NULL");
                cmd.Parameters.AddWithValue("@DiscountName", "");
                cmd.Parameters.AddWithValue("@DiscountAmount", "0");
                cmd.Parameters.AddWithValue("@ArrierAmt", txtArrier.Text);
                if (TextBox1.Text != "")
                {
                    cmd.Parameters.AddWithValue("@CheckDDNo", TextBox1.Text);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@CheckDDNo", "N/A");
                }
                if (TextBox2.Text != "")
                {
                    cmd.Parameters.AddWithValue("@BankName", TextBox2.Text);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@BankName", "N/A");
                }
                if (DropDownMOD.SelectedIndex == 0 || DropDownMOD.SelectedIndex == 3)
                {
                    cmd.Parameters.AddWithValue("@Status", "Paid");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Status", "Pending");
                }
                cmd.Parameters.AddWithValue("@MOP", DropDownMOD.SelectedItem.Text);

                cmd.Connection = con;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    saveconcession();
                    ArrierTransaction();

                    //oo.MessageBox("Fee submitted successfully", this.Page);
                    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Fee submitted successfully", "S");       


                    //SendNotificationEmail(RecNo, se);
                   
                    sql = "select FamilyContactNo from StudentFamilyDetails  where StEnRCode='" + se + "'  and SessionName='" + Session["SessionName"].ToString() + "'";
                    string Conta = "";
                    Conta = oo.ReturnTag(sql, "FamilyContactNo");
                    SendFeesSms(Conta, RecNo);
                    Session["RecieptNoSession"] = RecNo;

                    Session["PreviousBalanceAmount"] = txtArrier.Text.ToString();

                    Response.Redirect("ArrierStudentReceiptGenerate.aspx?print=1");


               
            }

            }
       
        else
        {
            //oo.MessageBox("Please Do Not Press Refresh Button or back Button", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please Do Not Press Refresh Button or back Button", "A");       

        }
    }
    protected void saveconcession()
    {
        Label lbl_std_srno = (Label)Grd.Rows[0].FindControl("Label1");
        Label lblclass = (Label)Grd.Rows[0].FindControl("Label5");
        Label lblstenr = (Label)Grd.Rows[0].FindControl("Label18");
        con.Open();
        SqlCommand Permission_check = new SqlCommand("select count(*) Count from Concession_Permission where TableId='2' and SrNo='" + lbl_std_srno.Text + "' and concession<>0 and reset is null and SessionName='" + Session["SessionName"].ToString() + "'", con);
        int Permission_check_count = (int)Permission_check.ExecuteScalar();
        con.Close();
        if (Permission_check_count == 1)
        {
            sql = "Update Concession_Permission set Concession='" + txtconfee.Text + "' where TableId='2' and SrNo='" + lbl_std_srno.Text + "' and reset is null and SessionName='" + Session["SessionName"].ToString() + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        else
        {
            if (txtconfee.Text != "")
            {
                sql = "Insert Into Concession_Permission(Class,SrNo,StEnCode,Insttalment,Concession,SessionName,TableId) Values ('" + lblclass.Text + "','" + lbl_std_srno.Text + "','" + lblstenr.Text + "','N/A','" + txtconfee.Text + "','" + Session["SessionName"].ToString() + "','2')";
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
    public void SendFeesSms(string FmobileNo, string RecieptNo)
    {
          sql = "Select HitValue from SMSActivateDeactivate where BranchCode=" + Session["BranchCode"] + "";
          if (oo.ReturnTag(sql, "HitValue") != "")
          {
              if (oo.ReturnTag(sql, "HitValue") == "true")
              {
                  SMSAdapterNew sadpNew = new SMSAdapterNew();
                  string mess = "";

                  sql = "Select FirstName as StudentName   from StudentGenaralDetail  where SessionName='" + Session["SessionName"].ToString() + "'";
                  sql = sql + "    and  " + DrpEnter.SelectedValue.ToString() + "=" + "'" + TxtEnter.Text.Trim() + "'";

                  mess = "INR Received " + txtreceiveAmount.Text + " towards Installment " + "Arrier Amount" + " of S.R. No. " + oo.ReturnTag(sql, "StudentName") + " ( " + TxtEnter.Text + " ). Receipt No. " + RecieptNo + "";
                  
                  string sms_response = "";


                  if (FmobileNo != "")
                  {

                      sql = "Select SmsSent From SmsEmailMaster where Id='15' ";
                      if (oo.ReturnTag(sql, "SmsSent").Trim() == "true")
                      {
                          sms_response = sadpNew.Send(mess, FmobileNo, "");
                      }
                  }
              }
          }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["CheckRefresh"] = Session["CheckRefresh"];
    }
    protected void txtreceiveAmount_TextChanged(object sender, EventArgs e)
    {
        findDepositAmt();
    }
    protected void TextBox9_TextChanged(object sender, EventArgs e)
    {

    }


    public void FindArrierOfStudents()
    {
        string sess = "";
        
        sql = "select ArrearAmt,arriersession from ArrierMast  where srno='" + TxtEnter.Text + "'";
        sql = sql + "  and SessionName='" + Session["SessionName"].ToString() + "'";

        double amt = 0;
        try
        {
            amt = Convert.ToDouble(oo.ReturnTag(sql, "ArrearAmt"));
        }
        catch (Exception) { amt = 0; }
        if (amt > 0)
        {
            txtArrier.Text = amt.ToString();
            txtTotalAmt.Text = amt.ToString();
            Panel1.Visible = true;

            sess = oo.ReturnTag(sql, "arriersession");
            if (Grd.Rows.Count == 0)
            {
                Session["Failed"] = "Failed";
                sql = "select SG.Id, SC.SectionName,SO.Card,SO.Medium as Medium,CM.ClassName,convert(nvarchar,So.DateOfAdmiission,106) as DateOfAdmiission ,SO.SectionId,Sf.FatherName,SF.MotherName,SG.FirstName,SG.MiddleName,SG.LastName,sg.StEnRCode as StEnRCode,sg.srno  as srno,case  when so.TransportRequired='Yes' then 'Yes' else 'No' end as TransportRequired,so.wayamount as wayamount from StudentGenaralDetail SG ";
                sql = sql + "   left join StudentFamilyDetails SF on SG.StEnRCode=SF.StEnRCode";
                sql = sql + "   left join StudentOfficialDetails SO on SG.StEnRCode=SO.StEnRCode";
                sql = sql + "   left join ClassMaster CM on SO.AdmissionForClassId=CM.Id";
                sql = sql + "   left join SectionMaster SC on SO.SectionId=SC.Id";
                sql = sql + "   where  SG." + DrpEnter.SelectedValue.ToString() + "=" + "'" + TxtEnter.Text.Trim() + "'";
                sql = sql + "   and sg.SessionName='" + sess + "' and ";
                sql = sql + "   so.SessionName='" + sess + "' and sf.SessionName='" + sess + "' and cm.SessionName='" + sess + "'";
                sql = sql + "   and SC.SessionName='" + sess + "'  and";
                sql = sql + "   sg.BranchCode=" + Session["BranchCode"].ToString() + "";
                sql = sql + "   and SO.Withdrwal is null";
                Grd.DataSource = oo.GridFill(sql);
                Grd.DataBind();

                //oo.MessageBox("Failed Student!", this.Page);
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Failed Student!", "A");       

            }
            else
            {
                Session["Failed"] = "none";
            
            }
        }
        else
        {

            //oo.MessageBox("No Arrears found of this Student!", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "No Arrears found of this Student!", "A");       

            Panel1.Visible = false;
        }

    }


    public void findCalculation()
    {
        double amt = 0;
        try
        {
            amt = amt + Convert.ToDouble(txtArrier.Text);
        }
        catch (Exception) { }

        try
        {
            amt = amt + Convert.ToDouble(txtConvance.Text);
        }
        catch (Exception) { }
        try
        {
            amt = amt - Convert.ToDouble(txtconfee.Text);
        }
        catch (Exception) { }
    
        try
        {
         txtTotalAmt.Text = amt.ToString();
         txtreceiveAmount.Text = txtTotalAmt.Text;   
        }
        catch (Exception) { }
    
    }

    public void findDepositAmt()
    {
        double amt = 0;
        

        try
        {
            amt = Convert.ToDouble(txtTotalAmt.Text) - Convert.ToDouble(txtreceiveAmount.Text);
        }
        catch (Exception) { }
        if (amt > 0)
        {
            balamobox.Visible = true;
            txtBalfee.Visible = true;
            lblBalance.Visible = true;
        }
        else
        {
            balamobox.Visible = false;
            txtBalfee.Visible = false;
            lblBalance.Visible = false;
        }
        txtBalfee.Text = amt.ToString();
        try
        {
            lblamountwords.Text = oo.NumberToString(Convert.ToInt64(txtreceiveAmount.Text));
        }
        catch (Exception) { }

    }

    public string FindRecieptNo()
    {

        string Recieptno = "";
#pragma warning disable 168
#pragma warning disable 219
        int l = 0, i;
#pragma warning restore 219
#pragma warning restore 168
        string xx = "";
        sql = "select ReceiptNoStart,remark from ReceiptNoStart  ";
        sql = sql + "  where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        Recieptno = oo.ReturnTag(sql, "ReceiptNoStart");
        if (Recieptno == "")
        {
            return "";
        }
        else
        {


            int co;
            sql = "select max(id) as id from FeeDeposite  ";
            sql = sql + "  where BranchCode=" + Session["BranchCode"].ToString() + "";
            try
            {
                co = Convert.ToInt32(oo.ReturnTag(sql, "id"));
            }
            catch (Exception) { co = 0; con.Close(); }
            co = co + 1;
            xx = IDGeneration(Recieptno, co.ToString());
            return xx;
        }
    }


    public void ArrierTransaction()
    {
        double arrearamt = 0, depositamt = 0,conAmtArrear=0, TotalbalAmtArrear = 0, amt = 0;
        try
        {
            arrearamt = Convert.ToDouble(txtArrier.Text);
        }
        catch (Exception) { arrearamt = 0; }
        try
        {
            depositamt = Convert.ToDouble(txtreceiveAmount.Text);
        }
        catch (Exception) { depositamt = 0; }
        try 
        {
            conAmtArrear = Convert.ToDouble(txtconfee.Text);
        }
        catch (Exception) { conAmtArrear = 0; }

        TotalbalAmtArrear = (depositamt + conAmtArrear)-arrearamt;
        //oo.MessageBox(TotalbalAmtArrear.ToString (),this.Page);
        if (TotalbalAmtArrear < 0)
        {
            amt = Math.Abs(TotalbalAmtArrear);
            sql = "update ArrierMast set arrearamt=" + amt + " where  srno='" + TxtEnter.Text + "'";
            oo.ProcedureDatabase(sql);
        }
        else
        {
            sql = "update ArrierMast set arrearamt=0 where  srno='" + TxtEnter.Text + "'";
            oo.ProcedureDatabase(sql);
        }

    }

    public string IDGeneration(string FixedString, string x)
    {
        string xx = "";
        if (x.Length == 1)
        {
            xx = FixedString + "000000" + x;

        }
        else if (x.Length == 2)
        {
            xx = FixedString + "00000" + x;
        }
        else if (x.Length == 3)
        {
            xx = FixedString + "0000" + x;
        }
        else if (x.Length == 4)
        {
            xx = FixedString + "000" + x;
        }
        else if (x.Length == 5)
        {
            xx = FixedString + "00" + x;
        }
        else if (x.Length == 6)
        {
            xx = FixedString + "0" + x;
        }
        else
        {
            xx = FixedString + x;
        }
        return xx;
    }

    public void SendNotificationEmail(string RecieptNo, string StEnCode)
    {
        string Mess = "";
        string collegeTitle = "";

        string ss = "";

        sql = "Select Fee  from  FeeEmailActivateDeactivate";

        ss = oo.ReturnTag(sql, "Fee");

        try
        {
            if (ss == "true")
            {

                sql = "Select CollegeShortNa  from CollegeMaster where BranchCode=" + Session["BranchCode"] + "";
                collegeTitle = oo.ReturnTag(sql, "CollegeShortNa");
                string logopath = "";
                sql = "select  CologeLogoPath from  CollegeMaster where BranchCode=" + Session["BranchCode"] + "";
                logopath = oo.ReturnTag(sql, "CologeLogoPath");
                int l = 0;
                l = Convert.ToInt32(logopath.Length.ToString());

                Mess = "  <table > ";
                try
                {
                    Mess = Mess + "<tr>";
                    Mess = Mess + "<td colspan='2'>" + "<img src='www.eam.co.in/" + logopath.Substring(1, l - 1) + "'  height='88' width='80' >";
                    Mess = Mess + "</tr>";
                }
                catch (Exception) { }

                Mess = Mess + " <tr>";
                Mess = Mess + "  <td>";
                Mess = Mess + "  &nbsp;</td>";
                Mess = Mess + " <td>";
                Mess = Mess + "    &nbsp;</td>";
                Mess = Mess + " </tr>";
                Mess = Mess + " <tr>";
                Mess = Mess + " <td colspan='2'>";
                Mess = Mess + "   <hr/></td>";
                Mess = Mess + " </tr>";
                Mess = Mess + " <tr>";
                Mess = Mess + " <td>";
                Mess = Mess + " This message was sent from a notification-only E-mail address.</td>";
                Mess = Mess + " <td>";
                Mess = Mess + " &nbsp;</td>";
                Mess = Mess + "</tr>";
                Mess = Mess + "<tr>";
                Mess = Mess + "   <td>";
                Mess = Mess + " Please do not reply to this message.</td>";
                Mess = Mess + "  <td>";
                Mess = Mess + "     &nbsp;</td>";
                Mess = Mess + " </tr>";
                Mess = Mess + " <tr>";
                Mess = Mess + " <td colspan='2'>";
                Mess = Mess + "  <hr/></td>";
                Mess = Mess + " </tr>";
                Mess = Mess + " <tr>";
                Mess = Mess + " <td colspan='2'>";
                Mess = Mess + "   Dear Sir/Madam,</td>";
                Mess = Mess + " </tr>";
                Mess = Mess + " <tr>";
                Mess = Mess + "  <td colspan='2'>";
                Mess = Mess + "  Greetings from " + collegeTitle + "!</td>";
                Mess = Mess + " </tr>";
                Mess = Mess + " <tr>";
                Mess = Mess + " <td colspan='2'>";

                sql = "Select FirstName+' '+MiddleName+' '+LastName as StudentName   from StudentGenaralDetail";
                sql = sql + "    where  " + DrpEnter.SelectedValue.ToString() + "=" + "'" + TxtEnter.Text.Trim() + "'  and sessionName='" + Session["SessionName"].ToString() + "'";

                Mess = Mess + " We've received INR " + txtreceiveAmount.Text + "/-" + " cash towards Installment " + "Arrier Amount";
                Mess = Mess + "    " + oo.CurrentYear() + "&nbsp;for S.R. No. " + TxtEnter.Text + " ( " + oo.ReturnTag(sql, "StudentName") + " ) .";
                Mess = Mess + " </td>";
                Mess = Mess + " </tr>";
                Mess = Mess + " <tr>";
                Mess = Mess + " <td colspan='2'>";

                Mess = Mess + "  Receipt No. is " + RecieptNo + " ." + "</td>";
                Mess = Mess + " </tr>";
                Mess = Mess + " <tr>";
                Mess = Mess + " <td>";
                Mess = Mess + "   &nbsp;</td>";
                Mess = Mess + " <td>";
                Mess = Mess + "   &nbsp;</td>";
                Mess = Mess + " </tr>";
                //Mess = Mess + " <tr>";
                //Mess = Mess + "  <td colspan='2'>";
                //Mess = Mess + "    Please find attaced file of Fee Receipt.</td>";
                //Mess = Mess + " </tr>";
                Mess = Mess + " <tr>";
                Mess = Mess + "  <td>";
                Mess = Mess + "   &nbsp;</td>";
                Mess = Mess + " <td>";
                Mess = Mess + "   &nbsp;</td>";
                Mess = Mess + " </tr>";
                Mess = Mess + " <tr>";
                Mess = Mess + "  <td colspan='2'>";

                string femail = "", FamilyContactNo = "";
                sql = "Select FamilyEmail,FamilyContactNo from StudentFamilyDetails where StEnRCode='" + StEnCode + "'  and SessionName='" + Session["SessionName"].ToString() + "'";
                femail = oo.ReturnTag(sql, "FamilyEmail");
                FamilyContactNo = oo.ReturnTag(sql, "FamilyContactNo");

                Mess = Mess + "     Note: In our record your E-mail is " + femail + " and Contact No. is " + FamilyContactNo + "</td>";

                Mess = Mess + "  </tr>";
                Mess = Mess + " <tr>";
                Mess = Mess + " <td colspan='2'>";
                Mess = Mess + "<hr/></td>";
                Mess = Mess + "   </tr>";
                Mess = Mess + "  <tr>";
                Mess = Mess + "     <td colspan='2'>";
                Mess = Mess + "        Warm Regards,&nbsp;";
                Mess = Mess + "   </td>";
                Mess = Mess + " </tr>";
                Mess = Mess + " <tr>";
                Mess = Mess + "   <td>";
                string schName = "";
                sql = "Select CollegeName from CollegeMaster where BranchCode=" + Session["BranchCode"] + "";
                schName = oo.ReturnTag(sql, "CollegeName");

                Mess = Mess + "  " + schName + "</td>";
                Mess = Mess + " <td>";
                Mess = Mess + "    &nbsp;</td>";
                Mess = Mess + " </tr>";
                Mess = Mess + " <tr>";
                Mess = Mess + "   <td>";

                string CityId = "";
                sql = "Select CityId from CollegeMaster where BranchCode=" + Session["BranchCode"] + "";
                CityId = oo.ReturnTag(sql, "CityId");


                Mess = Mess + "   " + CityId + "</td>";
                Mess = Mess + " <td>";
                Mess = Mess + "   &nbsp;</td>";
                Mess = Mess + " </tr>";
                Mess = Mess + " <tr>";
                Mess = Mess + "    <td>";

                string email = "";
                sql = "Select Email from CollegeMaster where BranchCode=" + Session["BranchCode"] + "";

                email = oo.ReturnTag(sql, "Email");
                Mess = Mess + "   " + email + "</td>";
                Mess = Mess + "  <td>";
                Mess = Mess + "    &nbsp;</td>";
                Mess = Mess + " </tr>";
                Mess = Mess + " <tr>";
                Mess = Mess + " <td>";
                Mess = Mess + "  &nbsp;</td>";
                Mess = Mess + " <td>";
                Mess = Mess + "  &nbsp;</td>";
                Mess = Mess + "  </tr>";
                Mess = Mess + " </table>";

                sql = "select FamilyEmail  from StudentFamilyDetails where StEnRCode='" + StEnCode + "'  and SessionName='" + Session["SessionName"].ToString() + "'";

                oo.SendEmailInBackgroundThread(Mess, "" + collegeTitle + " :Arrier Fee Deposit Acknowledgement", oo.ReturnTag(sql, "FamilyEmail"),"-1");

            }
        }
        catch (Exception) { }

    }

   
    protected void txtTotalAmt_TextChanged(object sender, EventArgs e)
    {

    }
    protected void DropDownMOD_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownMOD.SelectedIndex == 0 && DropDownMOD.SelectedIndex == 3)
        {
            table2.Visible = false;
            table12.Visible = false;
        }
        else
        {
            table2.Visible = true;
            table12.Visible = true;
            Label42.Text = DropDownMOD.SelectedItem.Text;
        }
    }
}