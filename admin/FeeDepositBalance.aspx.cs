using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;
using c4SmsNew;
using System.Net.Mail;

public partial class admin_FeeDepositBalance : Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql , SqlBal = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        

        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        //con.ConnectionString = ConfigurationManager.ConnectionStrings["Menu"].ConnectionString;
        con = oo.dbGet_connection();
        if(!IsPostBack)
        {
            balanceamountdiv.Visible = false;
            TxtEnter.Focus();
            Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());
            checkbool();
            Session["FD"] = "BD";
            try
            {
                CheckValueADDDeleteUpdate();
            }
            catch (Exception) { }

            sql = "select MonthName from MonthMaster ";
            sql = sql + "  where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            sql = sql + " order by MonthId";
            oo.FillDropDownWithOutSelect(sql, DropDownMonth, "MonthName");
            Panel1.Visible = false;
            oo.AddDateMonthYearDropDown(DDYear, DDMonth, DDDate);
            oo.FindCurrentDateandSetinDropDown(DDYear, DDMonth, DDDate);
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
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        TxtEnter.Text = TxtEnter.Text.Replace("&", "/");
        showDetails();
    }
    public void showDetails()
    {
        if (TxtEnter.Text != "")
        {
            TxtEnter.Text = TxtEnter.Text.Replace(" ", string.Empty);
        }
        string sql10 = "";
        sql10 = "Select Withdrwal From StudentOfficialDetails where SrNo='" + TxtEnter.Text + "' and SessionName='" + Session["SessionName"].ToString() + "'";
        if (oo.ReturnTag(sql10, "Withdrwal") != "")
        {
            //oo.MessageBox("This Student is already Withdrawn!", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "This Student is already Withdrawn!", "A");       

            GridView1.Visible = false;
            Panel1.Visible = false;
        }
        else
        {
            sql = "select SG.Id, SC.SectionName,CM.ClassName,convert(nvarchar,So.DateOfAdmiission,106) as DateOfAdmiission ,SO.Medium,SO.Card as card,SO.SectionId,Sf.FatherName,SF.MotherName,SG.FirstName,SG.MiddleName,SG.LastName,sg.StEnRCode as StEnRCode,sg.srno  as srno from StudentGenaralDetail SG ";
            sql = sql + "    left join StudentFamilyDetails SF on SG.SrNo=SF.SrNo";
            sql = sql + "   left join StudentOfficialDetails SO on SG.SrNo=SO.SrNo";
            sql = sql + "   left join ClassMaster CM on SO.AdmissionForClassId=CM.Id";
            sql = sql + "   left join SectionMaster SC on SO.SectionId=SC.Id";
            sql = sql + "    where  SG." + DrpEnter.SelectedValue.ToString() + "=" + "'" + TxtEnter.Text.Trim() + "'";
            sql = sql + "  and sg.SessionName='" + Session["SessionName"].ToString() + "' and sg.BranchCode=" + Session["BranchCode"].ToString() + "";
            sql = sql + "  and sf.SessionName='" + Session["SessionName"].ToString() + "'  and so.SessionName='" + Session["SessionName"].ToString() + "' and cm.SessionName='" + Session["SessionName"].ToString() + "' and  sc.SessionName='" + Session["SessionName"].ToString() + "'";
            Grd.DataSource = oo.GridFill(sql);
            Grd.DataBind();
            GridView1.Visible = false;
            Panel1.Visible = false;
            Session["CardType"] = oo.ReturnTag(sql, "card");
            if (Grd.Rows.Count == 0)
            {
                //oo.MessageBox("Invalid " + DrpEnter.SelectedItem.ToString() + "!", this.Page);
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Invalid " + DrpEnter.SelectedItem.ToString() + "!", "A");
                Panel1.Visible = false;
            }
            else
            {
                getBalanceAmount();
            }
            string card = Session["CardType"].ToString(); 

            sql = "select MonthName from MonthMaster where CardType='" + card + "'";
            sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            sql = sql + " or Monthid=0  ";
            sql = sql + " order by MonthId";

            oo.FillDropDownWithOutSelect(sql, DropDownMonth, "MonthName");

        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {

        Session["RecieptNoSession"] = lblID.Text;
        Session["LastMonthRemainsAmt"] = Label25.Text;
        Response.Redirect("StudentReciptGenrate_duplicate.aspx?print=1");

    }
    public void getBalanceAmount()
    {
        string bal = "";
        sql = "select StEnRCode,SrNo,convert(nvarchar,FeeDepositeDate,106) as FeeDepositeDate,FeeMonth,RecievedAmount,RemainingAmount,Class,Section,TotalFeeAmount,Cocession,CurrentAmount,Remark,AmountInWords,LateFeeAmount,BusConvience,FeeMode,MOP,case when Cancel='Y' then 'Cancelled' else Status end  as Cancel  ";
        sql = sql + "  from FeeDeposite  where " + DrpEnter.SelectedValue.ToString() + "=" + "'" + TxtEnter.Text.Trim() + "'";
        sql = sql + "  and Id =( select  MAX(id) from  FeeDeposite where  RemainingAmount >= 0 and  " + DrpEnter.SelectedValue.ToString() + "='" + TxtEnter.Text + "' and SessionName='" + Session["SessionName"].ToString() + "' and Cancel is null)";
        sql = sql + "  and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and Cancel is null";


        bal = oo.ReturnTag(sql, "RemainingAmount");



        if (bal == "0.00" || bal == "")
        {
            //oo.MessageBox("No Balance Amount found!", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "No Balance Amount found!", "A");

            Panel1.Visible = false;

        }
        else
        {
            Panel1.Visible = true;

            sql = "select Id,StEnRCode,SrNo,convert(nvarchar,FeeDepositeDate,106) as FeeDepositeDate,FeeMonth,RecievedAmount,RemainingAmount,Class,Section,RecieptSrNo,MOP,case when Cancel='Y' then 'Cancelled' else Status end  as Cancel ";
            sql = sql + "  from FeeDeposite  where  " + DrpEnter.SelectedValue.ToString() + "=" + "'" + TxtEnter.Text.Trim() + "'";
            sql = sql + "  and Id =( select  MAX(id) from  FeeDeposite where  " + DrpEnter.SelectedValue.ToString() + "='" + TxtEnter.Text + "'  and sessionName='" + Session["SessionName"].ToString() + "' and Cancel is null)";
            sql = sql + "  and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and Cancel is null";

            GridView1.DataSource = oo.GridFill(sql);
            GridView1.DataBind();
            GridView1.Visible = true;

            Session["feemon"] = oo.ReturnTag(sql, "FeeMonth");


            Panel1.Visible = true;
            BalanceZero();

        }

        if (GridView1.Rows.Count == 0)
        {
            GridView1.Visible = false;
            balanceamountdiv.Visible = false;
        }
        else
        {
            balanceamountdiv.Visible = true;
            txtBalanceDeposit.Focus();
        }
    }

    public void BalanceZero()
        {
            string pi="";
            sql = "select StEnRCode,SrNo,convert(nvarchar,FeeDepositeDate,106) as FeeDepositeDate,FeeMonth,RecievedAmount,RemainingAmount,Class,Section,TotalFeeAmount,Cocession,CurrentAmount,Remark,AmountInWords,LateFeeAmount,BusConvience,FeeMode  ";
            sql = sql + "  from FeeDeposite  where  " + DrpEnter.SelectedValue.ToString() + "=" + "'" + TxtEnter.Text.Trim() + "'";
            sql = sql + "  and Id =( select  MAX(id) from  FeeDeposite where  RemainingAmount >= 0 and  " + DrpEnter.SelectedValue.ToString() + "=" + TxtEnter.Text + "  and SessionName='" + Session["SessionName"].ToString() + "' and Cancel is null)";
            sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and Cancel is null";

            pi = oo.ReturnTag(sql, "FeeMonth");
            if (oo.Duplicate(sql))
            {
                string ss = "Your Balance Amount of this Installment(" + pi + ")" + " is " + oo.ReturnTag(sql, "RemainingAmount");
                oo.MessageBox(ss, this.Page);
            }
       

        }   
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void txttotamountOld_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtBalfee_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtconfee_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtafterpaise_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtreceiveAmount_TextChanged(object sender, EventArgs e)
    {

    }
    protected void TextBox9_TextChanged(object sender, EventArgs e)
    {

    }
    public string FindRecieptNo()
    {
        string Recieptno = "";
#pragma warning disable 219
#pragma warning disable 168
        int l = 0, i;
#pragma warning restore 168
#pragma warning restore 219
        string xx = "";
        sql = "select ReceiptNoStart,remark from ReceiptNoStart  ";
        sql = sql + "  where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        Recieptno = oo.ReturnTag(sql, "ReceiptNoStart");

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
    protected void Submit_Click(object sender, EventArgs e)
    {

        if (Session["CheckRefresh"].ToString() == ViewState["CheckRefresh"].ToString())
        {
            int i;
            double bala = 0;
            string RecNo = "";
            string balAmt = "";

            SqlCommand cmd = new SqlCommand();

            sql = "select StEnRCode,SrNo,convert(nvarchar,FeeDepositeDate,106) as FeeDepositeDate,FeeMonth,RecievedAmount,RemainingAmount,Class,Section,TotalFeeAmount,Cocession,CurrentAmount,Remark,AmountInWords,LateFeeAmount,BusConvience,FeeMode  ";
            sql = sql + "  from FeeDeposite  where " + DrpEnter.SelectedValue.ToString() + "=" + "'" + TxtEnter.Text.Trim() + "'";
            sql = sql + "  and Id =( select  MAX(id) from  FeeDeposite where  RemainingAmount >= 0 and  " + DrpEnter.SelectedValue.ToString() + "='" + TxtEnter.Text + "' and SessionName='" + Session["SessionName"].ToString() + "' and Cancel is null)";
            sql = sql + "  and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and Cancel is null";
            bala = Convert.ToDouble(oo.ReturnTag(sql, "RemainingAmount"));

            if (bala < Convert.ToDouble(txtBalanceDeposit.Text))
            {
                //oo.MessageBox("Amount exceeded!", this.Page);
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Amount exceeded!", "A");       

            }
            else
            {
                RecNo = FindRecieptNo();

                sql = "select ROW_NUMBER() OVER (ORDER BY SG.Id ASC) AS  SrNo,SG.Id, SC.SectionName as SectionName,CM.ClassName as ClassName,convert(nvarchar,";
                sql = sql + "    So.DateOfAdmiission,106) as DateOfAdmiission ,SO.SectionId,Sf.FatherName,SF.MotherName,SG.FirstName,SG.MiddleName,SG.LastName,sg.StEnRCode as StEnRCode,sg.srno  as srno from StudentGenaralDetail SG ";
                sql = sql + "    left join StudentFamilyDetails SF on SG.StEnRCode=SF.StEnRCode";
                sql = sql + "   left join StudentOfficialDetails SO on SG.StEnRCode=SO.StEnRCode";
                sql = sql + "   left join ClassMaster CM on SO.AdmissionForClassId=CM.Id";
                sql = sql + "   left join SectionMaster SC on SO.SectionId=SC.Id";
                sql = sql + "    where  SG." + DrpEnter.SelectedValue.ToString() + "=" + "'" + TxtEnter.Text.Trim() + "'";
                sql = sql + "  and sg.SessionName='" + Session["SessionName"].ToString() + "' and sg.BranchCode=" + Session["BranchCode"].ToString() + "";
                sql = sql + "  and sf.SessionName='" + Session["SessionName"].ToString() + "' and so.SessionName='" + Session["SessionName"].ToString() + "'  and cm.SessionName='" + Session["SessionName"].ToString() + "' and sc.SessionName='" + Session["SessionName"].ToString() + "'";

                string clna = ""; string sect = ""; string ss = ""; string se = ""; string p = ""; string q = ""; string r = ""; string s = ""; string t = ""; string u = ""; string v = "";
                string w = ""; string x = string.Empty; string y = ""; string z = ""; string f = ""; string jo = ""; string ik = ""; string ijk = string.Empty; string br = string.Empty;

                try
                {

                    clna = oo.ReturnTag(sql, "ClassName");
                    sect = oo.ReturnTag(sql, "SectionName");
                    ss = oo.ReturnTag(sql, "srno");
                    se = oo.ReturnTag(sql, "StEnRCode");
                    SqlBal = " select top 1 Id,StEnRCode,SrNo,FeeDepositeDate,FeeMonth,Class,Section,TotalFeeAmount,Cocession,CurrentAmount,RecievedAmount,RemainingAmount,Remark,AmountInWords,LateFeeAmount,BusConvience,FeeMode,DiscountName,DiscountAmount   ";
                    SqlBal = SqlBal + "  from FeeDeposite  where  " + DrpEnter.SelectedValue.ToString() + "=" + "'" + TxtEnter.Text.Trim() + "' and  RemainingAmount>0 ";
                    SqlBal = SqlBal + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and Cancel is null";
                    SqlBal = SqlBal + " order by id desc ";

                    p = DDYear.SelectedValue.ToString() + "/" + DDMonth.SelectedValue.ToString() + "/" + DDDate.SelectedValue.ToString();
                    q = oo.ReturnTag(SqlBal, "FeeMonth");
                    r = oo.ReturnTag(SqlBal, "RecievedAmount");
                    s = oo.ReturnTag(SqlBal, "RemainingAmount");
                    t = oo.ReturnTag(SqlBal, "TotalFeeAmount");
                    u = oo.ReturnTag(SqlBal, "Cocession");
                    v = oo.ReturnTag(SqlBal, "CurrentAmount");
                    w = oo.ReturnTag(SqlBal, "Remark");
                    y = oo.ReturnTag(SqlBal, "LateFeeAmount");
                    z = oo.ReturnTag(SqlBal, "BusConvience");
                    f = oo.ReturnTag(SqlBal, "FeeMode");
                    jo = oo.ReturnTag(SqlBal, "DiscountName");
                    ik = oo.ReturnTag(SqlBal, "DiscountAmount");

                }
                catch (Exception) { }

                cmd.CommandText = "FeeDepositeProc";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@RecieptSrNo", RecNo);
                cmd.Parameters.AddWithValue("@StEnRCode", se);
                cmd.Parameters.AddWithValue("@SrNo", ss);
               
                cmd.Parameters.AddWithValue("@FeeDepositeDate", p);
                cmd.Parameters.AddWithValue("@FeeMonth", q);
                cmd.Parameters.AddWithValue("@TotalFeeAmount", t);
                cmd.Parameters.AddWithValue("@Cocession", u);
                cmd.Parameters.AddWithValue("@CurrentAmount", v);
                cmd.Parameters.AddWithValue("@RecievedAmount", txtBalanceDeposit.Text.ToString());

                double j = 0;
                j = Convert.ToDouble(s) - Convert.ToDouble(txtBalanceDeposit.Text);

                cmd.Parameters.AddWithValue("@RemainingAmount", j);
                cmd.Parameters.AddWithValue("@Remark", txtRemark.Text);
                cmd.Parameters.AddWithValue("@LateFeeAmount", y);
                cmd.Parameters.AddWithValue("@BusConvience", z);
                cmd.Parameters.AddWithValue("@FeeMode", f);
                cmd.Parameters.AddWithValue("@AmountInWords", lblamountwords.Text);
                cmd.Parameters.AddWithValue("@Class", clna);
                cmd.Parameters.AddWithValue("@Section", sect);
                cmd.Parameters.AddWithValue("@BalanceMode", "BalancePaid");
                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());//Session["Login"].ToString());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
                cmd.Parameters.AddWithValue("@DiscountName", jo);
                cmd.Parameters.AddWithValue("@DiscountAmount", ik);
                cmd.Parameters.AddWithValue("@ArrierAmt", "0");
                if (txtCheckDDNo.Text != "")
                {
                    cmd.Parameters.AddWithValue("@CheckDDNo", txtCheckDDNo.Text);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@CheckDDNo", "N/A");
                }
                if (txtBankName.Text != "")
                {
                    cmd.Parameters.AddWithValue("@BankName", txtBankName.Text);
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

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    oo.MessageBox("Fee Submitted successfully.", this.Page);
                    sql = "select max(RecieptSrNo) as RecieptSrNo from FeeDeposite ";
                    Session["RecieptNoSession"] = RecNo;
                    SendNotificationEmail(RecNo, se);
                    try
                    {
                        sql = "select FamilyContactNo from StudentFamilyDetails  where StEnRCode='" + se + "'  and SessionName='" + Session["SessionName"].ToString() + "'";
                        string Conta = "";
                        Conta = oo.ReturnTag(sql, "FamilyContactNo");
                        SendFeesSms(Conta, RecNo);
                    }
                    catch (Exception) { }
                    lblamountwords.Text = "";
                    for (i = 0; i <= GridView1.Rows.Count - 1; i++)
                    {
                        Label lblBalanceAmt = (Label)GridView1.Rows[i].FindControl("Label22");
                        balAmt = lblBalanceAmt.Text;
                    }

                    Session["LastMonthRemainsAmt"] = balAmt;
                    Session["RecieptNoSession"] = Session["RecieptNoSession"].ToString();
                    Response.Redirect("BalanceStudentReceiptGenerate.aspx?print=1");
                    txtBalanceDeposit.Text = "";
                    BalanceAmountGrid();
                }
                catch (SqlException ee) { lblMessage.Text = ee.Message.ToString(); }


            }
        }
        else
        {
            //oo.MessageBox("Please Do Not Press Refresh Button Or bach Button",this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please Do Not Press Refresh Button Or bach Button", "A");       

        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["CheckRefresh"] = Session["CheckRefresh"];
    }
    public void BalanceAmountGrid()
    {
        string bal = "";
        sql = "select StEnRCode,SrNo,convert(nvarchar,FeeDepositeDate,106) as FeeDepositeDate,FeeMonth,RecievedAmount,RemainingAmount,Class,Section,TotalFeeAmount,Cocession,CurrentAmount,Remark,AmountInWords,LateFeeAmount,BusConvience,FeeMode  ";
        sql = sql + "  from FeeDeposite  where " + DrpEnter.SelectedValue.ToString() + "=" + "'" + TxtEnter.Text.Trim() + "'";
        sql = sql + "  and Id =( select  MAX(id) from  FeeDeposite where  RemainingAmount >= 0 and  " + DrpEnter.SelectedValue.ToString() + "='" + TxtEnter.Text + "' and SessionName='" + Session["SessionName"].ToString() + "' and Cancel is null)";
        sql = sql + "  and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and Cancel is null";// and Cancel is null";


        bal = oo.ReturnTag(sql, "RemainingAmount");



        if (bal == "0.00" || bal == "")
        {
            //oo.MessageBox("No Balance Amount found!", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "No Balance Amount found!", "A");       

            Panel1.Visible = false;

        }
        else
        {
            Panel1.Visible = true;

            sql = "select Id,StEnRCode,SrNo,convert(nvarchar,FeeDepositeDate,106) as FeeDepositeDate,FeeMonth,RecievedAmount,RemainingAmount,Class,Section,RecieptSrNo ";
            sql = sql + "  from FeeDeposite  where  " + DrpEnter.SelectedValue.ToString() + "=" + "'" + TxtEnter.Text.Trim() + "'";
            sql = sql + "  and Id =( select  MAX(id) from  FeeDeposite where  " + DrpEnter.SelectedValue.ToString() + "='" + TxtEnter.Text + "'  and sessionName='" + Session["SessionName"].ToString() + "' and Cancel is null)";
            sql = sql + "  and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and Cancel is null";
            GridView1.DataSource = oo.GridFill(sql);
            GridView1.DataBind();
            GridView1.Visible = true;
            string feemon = string.Empty;
            Session["feemon"] = oo.ReturnTag(sql, "FeeMonth");


            Panel1.Visible = true;
            BalanceZero();

        }

        if (GridView1.Rows.Count == 0)
        {
            GridView1.Visible = false;
        }
    }
    protected void  TextBox10_TextChanged(object sender, EventArgs e)
{
    
}
    protected void txtLastMonthAmt_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtBalanceDeposit_TextChanged(object sender, EventArgs e)
    {

        long aa = 0;
        double pp = 0;
        pp = Convert.ToDouble(txtBalanceDeposit.Text);
        aa = Convert.ToInt64(pp);
        lblamountwords.Text = oo.NumberToString(aa);
        txtRemark.Focus();
    }
    protected void DrpEnter_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Grd_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    public void PermissionGrant(int add1,  LinkButton Ladd)
    {


        if (add1 == 1)
        {
            Ladd.Enabled = true;
        }
        else
        {
            Ladd.Enabled = false;
        }


    }
    public void CheckValueADDDeleteUpdate()
    {
        sql = " select LoginId,LoginName,Pass,SessionId,BranchId,LT.LoginTypeName,ltb.add1 as add1,ltb.delete1 as delete1,ltb.update1 as update1 from LoginTab LTb";
        sql = sql + " left join LoginType Lt on LTb.LoginTypeId=Lt.LoginTypeId where LT.LoginTypeName='" + Session["Logintype"] + "' and LTb.LoginName='" + Session["LoginName"] + "'";
#pragma warning disable 168
        int a, u, d;
#pragma warning restore 168
        a = Convert.ToInt32(oo.ReturnTag(sql, "add1"));


        PermissionGrant(a, (LinkButton)Submit);
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

                sql = "Select FirstName as StudentName   from StudentGenaralDetail";
                sql = sql + "    where  " + DrpEnter.SelectedValue.ToString() + "=" + "'" + TxtEnter.Text.Trim() + "' and SessionName='"+ Session["SessionName"].ToString() + "'";

                Label Label24 = (Label)GridView1.Rows[0].FindControl("Label24");

                mess = "INR Received " + txtBalanceDeposit.Text + " towards Balance Fee of Installment " + Label24.Text + " for " + oo.ReturnTag(sql, "StudentName") + " ( " + TxtEnter.Text + " ). Receipt No. " + RecieptNo + "";
                string sms_response = "";

                sql = "Select SmsSent From SmsEmailMaster where Id='2' ";
                if (oo.ReturnTag(sql, "SmsSent").Trim() == "true")
                {
                        sms_response = sadpNew.Send(mess, FmobileNo, "");
                }
            }
        }
        
    }
    public void SendNotificationEmail(string RecieptNo, string StEnCode)
    {
        string Mess = "";
        string ss = "";
        string collegeTitle = "";


        sql = "Select Fee  from  BalanceFeeDepoFeeEmailActivateDeactivate";

        ss = oo.ReturnTag(sql, "Fee");

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
            Mess = Mess + "<tr>";
            Mess = Mess + "<td colspan='2'>" + "<img src='www.eam.co.in/" + logopath.Substring(1, l - 1) + "'  height='88' width='80' >";
            Mess = Mess + "</tr>";


            Mess = Mess + " <tr>";
            Mess = Mess + "  <td>";
            Mess = Mess + "  &nbsp;</td>";
            Mess = Mess + " <td>";
            Mess = Mess + "    &nbsp;</td>";
            Mess = Mess + " </tr>";
            Mess = Mess + " <tr>";
            Mess = Mess + " <td colspan='2'>";
            Mess = Mess + "  <hr/></td>";
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
            sql = sql + "    where  " + DrpEnter.SelectedValue.ToString() + "=" + "'" + TxtEnter.Text.Trim() + "' and SessionName='" + Session["SessionName"].ToString() + "'";

            Mess = Mess + "  We've received INR " + txtBalanceDeposit.Text + " cash towards Balance Fee of Installment " + Session["feemon"];
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
            Mess = Mess + " <tr>";
            Mess = Mess + "  <td colspan='2'>";
            Mess = Mess + "    Please find attaced file of Fee Receipt.</td>";
            Mess = Mess + " </tr>";
            Mess = Mess + " <tr>";
            Mess = Mess + "  <td>";
            Mess = Mess + "   &nbsp;</td>";
            Mess = Mess + " <td>";
            Mess = Mess + "   &nbsp;</td>";
            Mess = Mess + " </tr>";
            Mess = Mess + " <tr>";
            Mess = Mess + "  <td colspan='2'>";

            string femail = "", FamilyContactNo = "";
            sql = "Select FamilyEmail,FamilyContactNo from StudentFamilyDetails where StEnRCode='" + StEnCode + "'  and  sessionName='" + Session["SessionName"].ToString()+"'";
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

            sql = "select FamilyEmail  from StudentFamilyDetails where StEnRCode='" + StEnCode + "' and SessionName='" + Session["SessionName"].ToString()+"'";

            EmailSending(Mess, "" + collegeTitle + " : Balance Fee Deposit Acknowledgement", oo.ReturnTag(sql, "FamilyEmail"));
        }


    }
    public bool EmailSending(string Mess, string subjectParameter, string TOEmailID)
    {
        string ss = string.Empty;
        string kk = "";
        sql = "Select Email,Password from EmailPanelSetting where Id=1";
        kk = oo.ReturnTag(sql, "Email");
        string pp = oo.ReturnTag(sql, "Password");



        bool send = false;
        MailMessage mail = new MailMessage();
        mail.To.Add(TOEmailID);//to ID

        mail.From = new MailAddress(kk);
        mail.Subject = subjectParameter;

        //Attachment attach = new Attachment(Server.MapPath("http://www.eam.co.in/ReceiptGenerate/123.docx"));
        //mail.Attachments.Add(attach);


        mail.Body = Mess;
        mail.IsBodyHtml = true;
        SmtpClient smtp = new SmtpClient();
        smtp.Host = "smtp.gmail.com"; //Or Your SMTP Server Address
        smtp.Port = 587;
        smtp.Credentials = new System.Net.NetworkCredential(kk, pp);//from id
        //Or your Smtp Email ID and Password
        smtp.EnableSsl = true;
        try
        {
            smtp.Send(mail);
            send = true;
        }
        catch (Exception) { }
        return send;
    }
    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        string ss = "";
        string Month = "";
        LinkButton chk = (LinkButton)sender;
        ss = chk.ToolTip.ToString();
        lblDiscountPanel.Visible = true; lblDiscountPanelValue.Visible = true; Panel7.Visible = true;

        sql = "select id,StEnRCode,SrNo,convert(nvarchar,FeeDepositeDate,106) as FeeDepositeDate,RecieptSrNo,Cancel, BusConvience,  FeeMonth,TotalFeeAmount,";
        sql = sql + "   Cocession,RecievedAmount,CurrentAmount,LateFeeAmount,RemainingAmount,Remark,Class,Section,BalanceMode,DiscountName,DiscountAmount from FeeDeposite  where " + DrpEnter.SelectedValue.ToString() + "=" + "'" + TxtEnter.Text.Trim() + "'  and RecieptSrNo='" + chk.Text + "'";
        sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and Cancel is null";

        if (oo.ReturnTag(sql, "BalanceMode").Trim() == "BalancePaid")
        {


            int i;
            string mon = "";

            sql = "select id,StEnRCode,SrNo,convert(nvarchar,FeeDepositeDate,106) as FeeDepositeDate,RecieptSrNo,Cancel, BusConvience,  FeeMonth,TotalFeeAmount,";
            sql = sql + "   Cocession,RecievedAmount,CurrentAmount,LateFeeAmount,RemainingAmount,Remark,Class,Section,BalanceMode,DiscountName,DiscountAmount from FeeDeposite  where " + DrpEnter.SelectedValue.ToString() + "=" + "'" + TxtEnter.Text.Trim() + "'  and RecieptSrNo='" + chk.Text + "'";
            sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

            if (oo.ReturnTag(sql, "BalanceMode").Trim() == "BalancePaid")
            {

                Label32.Text = oo.ReturnTag(sql, "CurrentAmount");
                Label34.Text = oo.ReturnTag(sql, "RecievedAmount");
                Label35.Text = oo.ReturnTag(sql, "RemainingAmount");

                lblID0.Text = chk.Text;
                if (oo.ReturnTag(sql, "Cancel").Trim() == "Y")
                {
                    lblcancel0.Text = "CANCELLED";
                    Session["RCancel"] = "CANCELLED";
                }
                else
                {
                    lblcancel0.Text = "";
                    Session["RCancel"] = "";
                }

                int k = Convert.ToInt32(ss) - 1;

                sql = "   select top 1 id,StEnRCode,SrNo,convert(nvarchar,FeeDepositeDate,106) as FeeDepositeDate,RecieptSrNo,Cancel,   FeeMonth,TotalFeeAmount,   Cocession,RecievedAmount,CurrentAmount,RemainingAmount,Remark,Class,Section from FeeDeposite   ";
                sql = sql + "      where  " + DrpEnter.SelectedValue.ToString() + "=" + "'" + TxtEnter.Text.Trim() + "'  and id<=" + k.ToString();
                sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
                sql = sql + "  order by id  desc ";

                try
                {
                    Label36.Text = oo.ReturnTag(sql, "RemainingAmount");
                }
                catch (Exception) { } 

                try
                {
                    for (i = 0; i <= GridView1.Rows.Count - 1; i++)
                    {

                        LinkButton lnkRecieptNo = (LinkButton)GridView1.Rows[i].FindControl("LinkButton4");
                        if (lnkRecieptNo.Text == chk.Text)
                        {
                            Label lblmonth = (Label)GridView1.Rows[i].FindControl("Label20");
                            mon = lblmonth.Text;
                        }
                    }

                    sql = "select  SUM(RecievedAmount  ) as TotalRecivedAmt from FeeDeposite  where srno='" + TxtEnter.Text + "'  and   ";
                    sql = sql + "  FeeMonth='" + mon + "' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

                    Label33.Text = oo.ReturnTag(sql, "TotalRecivedAmt");
                }
                catch (Exception) { }


                try
                {


                    sql = "     select top 1 id,StEnRCode,SrNo,convert(nvarchar,FeeDepositeDate,106) as FeeDepositeDate,RecieptSrNo,Cancel,   FeeMonth,TotalFeeAmount,   Cocession,RecievedAmount,CurrentAmount,RemainingAmount,Remark,Class,Section from FeeDeposite         where  srno='" + TxtEnter.Text + "'  and  FeeMonth='" + mon + "'  and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

                    Label37.Text = oo.ReturnTag(sql, "CurrentAmount");
                }
                catch (Exception) { }
                Panel5_ModalPopupExtender.Show();
            }
        }
        else
        {

            sql = "select id,StEnRCode,SrNo,convert(nvarchar,FeeDepositeDate,106) as FeeDepositeDate,RecieptSrNo,Cancel, BusConvience,  FeeMonth,TotalFeeAmount,";
            sql = sql + "   Cocession,RecievedAmount,CurrentAmount,LateFeeAmount,RemainingAmount,Remark,Class,Section,BalanceMode,DiscountName,DiscountAmount from FeeDeposite  where " + DrpEnter.SelectedValue.ToString() + "=" + "'" + TxtEnter.Text.Trim() + "'  and RecieptSrNo='" + chk.Text + "'";
            sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

            lblTotalFee.Text = oo.ReturnTag(sql, "TotalFeeAmount");
            lblConcession.Text = oo.ReturnTag(sql, "Cocession");
            lblPaidAmount.Text = oo.ReturnTag(sql, "RecievedAmount");
            lblBalace.Text = oo.ReturnTag(sql, "RemainingAmount");
            lblRemark.Text = oo.ReturnTag(sql, "Remark");
            lblLate.Text = oo.ReturnTag(sql, "LateFeeAmount");
            Label31.Text = oo.ReturnTag(sql, "BusConvience");
            Month = oo.ReturnTag(sql, "FeeMonth");
            NextMonthDuesAmt(Month);
            try
            {
                lblDiscountPanel.Visible = true; lblDiscountPanelValue.Visible = true; Panel7.Visible = true;
                lblDiscountPanel.Text = oo.ReturnTag(sql, "DiscountName");
                lblDiscountPanelValue.Text = oo.ReturnTag(sql, "DiscountAmount");
            }
            catch (Exception) { lblDiscountPanel.Visible = false; lblDiscountPanelValue.Visible = false; Panel7.Visible = false; }
            if (oo.ReturnTag(sql, "BusConvience") == "")
            {
                Label31.Text = "0";
            }
            else
            {
                Label31.Text = oo.ReturnTag(sql, "BusConvience");
            }
            lblID.Text = chk.Text;
            if (oo.ReturnTag(sql, "Cancel").Trim() == "Y")
            {
                lblcancel.Text = "CANCELLED";
                Session["RCancel"] = "CANCELLED";
            }
            else
            {
                lblcancel.Text = "";
                Session["RCancel"] = "";
            }

            int k = Convert.ToInt32(ss) - 1;

            sql = "   select top 1 id,StEnRCode,SrNo,convert(nvarchar,FeeDepositeDate,106) as FeeDepositeDate,RecieptSrNo,Cancel,   FeeMonth,TotalFeeAmount,   Cocession,RecievedAmount,CurrentAmount,RemainingAmount,Remark,Class,Section from FeeDeposite   ";
            sql = sql + "      where  " + DrpEnter.SelectedValue.ToString() + "=" + "'" + TxtEnter.Text.Trim() + "'  and id<=" + k.ToString();
            sql = sql + " and Cancel is null and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            sql = sql + "  order by RecieptSrNo  desc ";

            try
            {
                Label25.Text = oo.ReturnTag(sql, "RemainingAmount");
                double a = Convert.ToDouble(Label25.Text);
            }
            catch (Exception) { Label25.Text = "0"; con.Close(); }

            Panel4_ModalPopupExtender.Show();

        }





    }
    public void NextMonthDuesAmt(string MonthNa)
    {
        string sql3 = "";
        string clna = "";
        string med = "", typeOfAdd = "";
        int i = 0;
        int po = 0;
        string FindMonth = "";
        string[] monthArr = new string[15];
        double tamt = 0;
        string sql = "";

        for (i = 0; i <= DropDownMonth.Items.Count - 1; i++)
        {
            monthArr[i] = DropDownMonth.Items[i].ToString();
        }

        for (i = 0; i <= DropDownMonth.Items.Count - 1; i++)
        {
            if (DropDownMonth.Items[i].ToString() == MonthNa)
            {
                po = i;

                break;
            }

        }
        FindMonth = monthArr[po + 1];

        sql = "select ROW_NUMBER() OVER (ORDER BY SG.Id ASC) AS  SrNo,SG.Id, SC.SectionName,CM.ClassName as ClassName,convert(nvarchar,So.DateOfAdmiission,106) as DateOfAdmiission ,SO.SectionId,Sf.FatherName,SF.MotherName,SG.FirstName,SG.MiddleName,SG.LastName,sg.StEnRCode as StEnRCode,sg.srno  as srno,so.TypeOFAdmision as TypeOfAdmission,so.Medium as Medium,So.Card as Card ,case  when so.TransportRequired='Yes' then 'Yes' else 'No' end as TransportRequired,so.wayamount as wayamount from StudentGenaralDetail SG ";
        sql = sql + "    left join StudentFamilyDetails SF on SG.StEnRCode=SF.StEnRCode";
        sql = sql + "   left join StudentOfficialDetails SO on SG.StEnRCode=SO.StEnRCode";
        sql = sql + "   left join ClassMaster CM on SO.AdmissionForClassId=CM.Id";
        sql = sql + "   left join SectionMaster SC on SO.SectionId=SC.Id";
        sql = sql + "    where  SG." + DrpEnter.SelectedValue.ToString() + "=" + "'" + TxtEnter.Text.Trim() + "'";
        sql = sql + "  and sg.SessionName='" + Session["SessionName"].ToString() + "' and sg.BranchCode=" + Session["BranchCode"].ToString() + "";
        sql = sql + " and sf.SessionName='" + Session["SessionName"].ToString() + "'  and so.SessionName='" + Session["SessionName"].ToString() + "'  and cm.SessionName='" + Session["SessionName"].ToString() + "'   and  sc.SessionName='" + Session["SessionName"].ToString() + "'";


        clna = oo.ReturnTag(sql, "ClassName");
        typeOfAdd = oo.ReturnTag(sql, "TypeOfAdmission");
        med = oo.ReturnTag(sql, "Medium");
        string crd = "";
        crd = oo.ReturnTag(sql, "Card");



        sql = " select sum(fa.FeePayment) as TotalFees from FeeAllotedForClassWise fa ";
        sql = sql + " left join feemaster  fm on fa.Medium=fm.medium  and fa.FeeName=fm.FeeName  ";
        sql = sql + " where fa.Class='" + clna + "'   and fa.Month='" + FindMonth + "' and fa.SessionName='" + Session["SessionName"].ToString() + "'  and fa.BranchCode=" + Session["BranchCode"].ToString() + "";
        sql = sql + "and fa.CardType='" + crd + "' and   fa.AdmissionType='" + typeOfAdd.Trim() + "'  and fa.Medium='" + med + "' and fm.SessionName='" + Session["SessionName"].ToString() + "'";

        try
        {
            tamt = tamt + Convert.ToDouble(oo.ReturnTag(sql, "TotalFees"));
        }
        catch (Exception) { tamt = 0; }





        //=======================================================Transport========================================
        sql = "select ROW_NUMBER() OVER (ORDER BY SG.Id ASC) AS  SrNo,SG.Id, SC.SectionName,CM.ClassName as ClassName,convert(nvarchar,So.DateOfAdmiission,106) as DateOfAdmiission ,SO.SectionId,Sf.FatherName,SF.MotherName,SG.FirstName,SG.MiddleName,SG.LastName,sg.StEnRCode as StEnRCode,sg.srno  as srno,so.TypeOFAdmision as TypeOfAdmission,so.Medium as Medium,So.Card as Card ,case  when so.TransportRequired='Yes' then 'Yes' else 'No' end as TransportRequired,so.wayamount as wayamount from StudentGenaralDetail SG ";
        sql = sql + "    left join StudentFamilyDetails SF on SG.StEnRCode=SF.StEnRCode";
        sql = sql + "   left join StudentOfficialDetails SO on SG.StEnRCode=SO.StEnRCode";
        sql = sql + "   left join ClassMaster CM on SO.AdmissionForClassId=CM.Id";
        sql = sql + "   left join SectionMaster SC on SO.SectionId=SC.Id";
        sql = sql + "    where  SG." + DrpEnter.SelectedValue.ToString() + "=" + "'" + TxtEnter.Text.Trim() + "'";
        sql = sql + "  and sg.SessionName='" + Session["SessionName"].ToString() + "' and sg.BranchCode=" + Session["BranchCode"].ToString() + "";
        sql = sql + "  and sf.sessionName='" + Session["SessionName"].ToString() + "'  and so.SessionName='" + Session["SessionName"].ToString() + "' and cm.SessionName='" + Session["SessionName"].ToString() + "'  and  sc.SessionName='" + Session["SessionName"].ToString() + "'";


        double conv = 0;

        try
        {
            if (oo.ReturnTag(sql, "TransportRequired") == "Yes")
            {


                for (i = 1; i <= Convert.ToInt32(Session["NoOfMonths"].ToString()); i++)
                {
                    sql3 = "Select InstallmentMonth from VehicleAllotmentTime ";
                    sql3 = sql3 + " where " + DrpEnter.SelectedValue.ToString() + "=" + "'" + TxtEnter.Text.Trim() + "' and allotedYesNo='Yes'";
                    string[] Installment = oo.ReturnTag(sql3, "InstallmentMonth").Split(' ');
                    for (int j = 0; j < Installment.Length; j++)
                    {
                        if (po + 1 != DropDownMonth.Items.Count)
                        {
                            if (DropDownMonth.Items[po + 1].Text == Installment[j])
                            {
                                conv = conv + Convert.ToDouble(oo.ReturnTag(sql, "WayAmount"));
                            }
                        }
                    }
                    //conv = conv + Convert.ToDouble(oo.ReturnTag(sql, "WayAmount"));
                }

            }

        }
        catch (Exception)
        {

        }

        tamt = tamt + conv;


        double totAmt = 0;
        string sql1 = "";
        double discAmt = 0;
#pragma warning disable 219
        double damt = 0;
#pragma warning restore 219
        sql1 = "select DiscountId,DiscountName,DiscountValue,DiscountType,SessionName,BranchCode,SrNo,StEnRCode,RecordDate,Remark from DiscountMaster ";
        sql1 = sql1 + " where " + DrpEnter.SelectedValue.ToString() + "=" + "'" + TxtEnter.Text.Trim() + "'";
        sql1 = sql1 + "  and  SessionName='" + Session["SessionName"].ToString() + "'";

        if (oo.Duplicate(sql1))
        {
            if (oo.ReturnTag(sql1, "DiscountType") == "Amount")
            {
                try
                {
                    string[] discountamount = oo.ReturnTag(sql1, "DiscountValue").Split(' ');
                    totAmt = totAmt + Convert.ToDouble(discountamount[po]);
                }
                catch (Exception) { }
                discAmt = totAmt;
            }
            if (oo.ReturnTag(sql1, "DiscountType") == "Percentage")
            {
                discAmt = discAmt + (Convert.ToDouble(Session["TutionFeesTypeAmt"].ToString()) * Convert.ToDouble(oo.ReturnTag(sql1, "DiscountValue"))) / 100;

            }

            tamt = tamt - discAmt;

            Session["NextDueAmt"] = tamt.ToString();

        }
        else
        {
            Session["NextDueAmt"] = tamt.ToString();
        }
    }
    protected void Button5_Click(object sender, EventArgs e)
    {

    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        Session["RecieptNoSession"] = lblID0.Text;
        Session["LastMonthRemainsAmt"] = Label36.Text;
        Response.Redirect("BalanceStudentReceiptGenerate_Duplicate.aspx?print=1");
    }
    protected void DDYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.YearDropDown(DDYear, DDMonth, DDDate);
    }
    protected void DDMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.MonthDropDown(DDYear, DDMonth, DDDate);
    }
    protected void TxtEnter_TextChanged(object sender, EventArgs e)
    {
        TxtEnter.Text = TxtEnter.Text.Replace("&", "/");
        showDetails();
    }
}