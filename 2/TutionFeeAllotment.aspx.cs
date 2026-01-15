using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;
using System.Globalization;

public partial class TutionFeeAllotment : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "", sql1 = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((string)Session["LoginName"] == "" || (string)Session["BranchCode"] == "" || (string)Session["SessionName"] == "" || Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }

        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file

        if (!IsPostBack)
        {
            try
            {


                try
                {
                    CheckValueADDDeleteUpdate();
                }
                catch (Exception) { }



                sql1 = "Select FeeGroupName,Id from FeeGroupMaster";
                sql1 = sql1+ "  where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

                oo.FillDropDown_withValue(sql1, drpFeeGroup, "FeeGroupName","Id");
                drpFeeGroup.Items.Insert(0, new ListItem("<--Select-->", "<-- Select-->"));
             
                sql = "select Monthid, MonthName from MonthMaster where CardType='" + drpFeeGroup.SelectedValue + "'";
                sql +=  "  and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

                try
                {
                    oo.FillDropDown_withValue_withSelect(sql, drpInstallment, "MonthName", "Monthid");
                    //oo.FillDropDown(sql, drpMonthPanel, "MonthName");

                }
                catch (Exception) { }

             
                
                sql = "select ClassName,id from ClassMaster";
                sql +=  " where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
                sql +=  "  order by Id";
                oo.FillDropDown_withValue(sql, drpClass, "ClassName","id");
                drpClass.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
                //oo.FillDropDownWithOutSelect(sql, drpClassPanel, "ClassName");
                //drpClassPanel.Items.Insert(0, new ListItem("<-- Select Class -->", "<-- Select Class -->"));

                //sql = "select case when medium IS NULL then  Feename   else ltrim(rtrim(FeeName+' ('+Medium+')')) end as feeName  from FeeMaster";
                //sql +=  "  where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "  order by feeid";

              

                sql = "Select case  when Upper(right(FeeHeadId,2))='I)'  then  LEFT(RIGHT(FeeHeadId,6),5)  else  LEFT(right(FeeHeadId,8),7)  end as Medium, ROW_NUMBER() OVER (ORDER BY Id ASC) AS SrNo,Id, Class,FeeHeadId,FeeAmount from FeeAllotedForClassWise "; 
               // sql +=  "   where  SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and CardType='" + drpFeeGroup.SelectedValue + "' and Class='"+drpClass.SelectedItem.ToString()+"' and Month='"+drpInstallment.SelectedItem.ToString()+"'";

                sql += "   where  SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " ";
                GridView1.DataSource = oo.GridFill(sql);
                GridView1.DataBind();
                sql = "select FeeHead  from FeeheadMaster";
                sql +=  "  where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
                lblFeeHead.Text = oo.ReturnTag(sql, "FeeHead");
                DrpNewOld.Focus();
                loadmedium();
            }

            catch (Exception) { }
            drpMedium.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
            drpBranch.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
            drpInstallment.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
            drpFeeHead.Items.Insert(0, new ListItem("<--Select-->", ""));
        }
       
    }

    private void loadFeeName()
    {
        sql = "select FeeHead, id from FeeheadMaster where BranchCode=" + Session["BranchCode"].ToString() + " and FeeType in ('Tuition Fee', 'Tuition Fee (Optional)') order by id asc";
        oo.FillDropDown_withValue(sql, drpFeeHead, "FeeHead", "id");
        drpFeeHead.Items.Insert(0, new ListItem("<--Select-->", ""));
    }

    public void loadmedium()
    {
        sql = "Select Medium From MediumMaster where  BranchCode=" + Session["BranchCode"].ToString() + "";
        oo.FillDropDown(sql, drpMedium,"Medium");
        drpMedium.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
    }
    public void FeesDisplay()
    {
        GridView1.Visible = true;

        if (DrpNewOld.SelectedItem.Text.ToUpper()=="BOTH")
        {
            sql = "select distinct Medium,ROW_NUMBER() OVER (ORDER BY facw.Id ASC) AS SrNo,fg.feeGroupName,facw.Id, facw.MonthId,MM.MonthName,facw.FeeHeadId, FHM.FeeHead, cm.ClassName as Class, bm.BranchName,dbo.ModeofFeeDepositeFun(facw.MOD) as MOD,FeeAmount,AdmissionType, fm.FeeType from FeeAllotedForClassWise facw ";
            sql +=  " Inner join ClassMaster cm on cm.Id=facw.ClassId and cm.SessionName=facw.SessionName and cm.BranchCode=facw.BranchCode ";
            sql +=  " Inner join FeeHeadMaster FHM on FHM.Id=facw.FeeHeadId and FHM.BranchCode=facw.BranchCode   ";
            sql +=  " Inner join MonthMaster MM on MM.MonthId=facw.MonthId AND MM.CardType=facw.CardType  and MM.SessionName=facw.SessionName  and MM.BranchCode=facw.BranchCode ";
            sql +=  " Left join BranchMaster bm on bm.Id=facw.Branchid  and bm.SessionName=facw.SessionName and bm.BranchCode=facw.BranchCode ";
            sql +=  " inner join FeeHeadMaster fm on fm.ID=facw.FeeHeadId and fm.BranchCode=facw.BranchCode ";
            sql +=  " inner join FeeGroupMaster fg on fg.id=facw.Cardtype and fg.SessionName=facw.SessionName and fg.BranchCode=facw.BranchCode ";
            sql +=  " where facw.Classid=" + drpClass.SelectedValue.ToString() + " and facw.MonthId=" + drpInstallment.SelectedValue.ToString() + "";
            sql +=  " and facw.Branchid=" + drpBranch.SelectedValue.ToString() + " and (facw.MOD='" + drpMod.SelectedValue.ToString() + "' or facw.MOD is null) and facw.SessionName='" + Session["SessionName"].ToString() + "' and facw.BranchCode=" + Session["BranchCode"].ToString() + " and facw.CardType='" + drpFeeGroup.SelectedValue + "'";
            sql +=  " and facw.Medium='" + drpMedium.SelectedItem.ToString() + "'";
            GridView1.DataSource = oo.GridFill(sql);
            GridView1.DataBind();                   
        }
        else
        {
            sql = "select distinct Medium,ROW_NUMBER() OVER (ORDER BY facw.Id ASC) AS SrNo,fg.feeGroupName,facw.Id, facw.MonthId,MM.MonthName,facw.FeeHeadId, FHM.FeeHead, cm.ClassName as Class, bm.BranchName,dbo.ModeofFeeDepositeFun(facw.MOD) as MOD,FeeAmount,AdmissionType, fhm.FeeType from FeeAllotedForClassWise facw ";
            sql +=  " Inner join ClassMaster cm on cm.Id=facw.ClassId and cm.SessionName=facw.SessionName and cm.BranchCode=facw.BranchCode ";
            sql +=  " Inner join FeeHeadMaster FHM on FHM.Id=facw.FeeHeadId and FHM.BranchCode=facw.BranchCode   ";
            sql +=  " Inner join MonthMaster MM on MM.MonthId=facw.MonthId AND MM.CardType=facw.CardType  and MM.SessionName=facw.SessionName  and MM.BranchCode=facw.BranchCode ";
            sql +=  " Left join BranchMaster bm on bm.Id=facw.Branchid  and bm.SessionName=facw.SessionName and bm.BranchCode=facw.BranchCode ";
            sql +=  " inner join FeeGroupMaster fg on fg.id=facw.Cardtype and fg.SessionName=facw.SessionName and fg.BranchCode=facw.BranchCode ";
            sql +=  " where facw.Classid=" + drpClass.SelectedValue.ToString() + " and facw.MonthId=" + drpInstallment.SelectedValue.ToString() + "";
            sql +=  " and facw.Branchid=" + drpBranch.SelectedValue.ToString() + " and (facw.MOD='" + drpMod.SelectedValue.ToString() + "' or facw.MOD is null) and facw.SessionName='" + Session["SessionName"].ToString() + "' and facw.BranchCode=" + Session["BranchCode"].ToString() + " and facw.CardType='" + drpFeeGroup.SelectedValue + "'";
            sql +=  " and facw.Medium='" + drpMedium.SelectedItem.ToString() + "'";
            if (DrpNewOld.SelectedIndex != 0 && DrpNewOld.SelectedIndex != 3)
            {
                sql +=  " and facw.AdmissionType='" + DrpNewOld.SelectedItem.ToString() + "'";
            }
            GridView1.DataSource = oo.GridFill(sql);
            GridView1.DataBind();      
        }

        if (GridView1.Rows.Count > 0)
        {
            int i;
            double tot = 0;
            for (i = 0; i <= GridView1.Rows.Count - 1; i++)
            {
                Label lblAmt = (Label)GridView1.Rows[i].FindControl("Label5");
                Label lblMonthid = (Label)GridView1.Rows[i].FindControl("lblMonthid");
                Label FeeHeadId = (Label)GridView1.Rows[i].FindControl("FeeHeadId");

                tot = tot + Convert.ToDouble(lblAmt.Text);

                LinkButton edit = (LinkButton)GridView1.Rows[i].FindControl("LinkButton2");
                LinkButton delete = (LinkButton)GridView1.Rows[i].FindControl("LinkButton3");

                sql = "select count(*) cnt from CompositFeeDeposit   where SessionName='" + Session["SessionName"].ToString() + "'    and BranchCode=" + Session["BranchCode"].ToString() + " and installmentid="+ lblMonthid.Text + " AND FeeHeadId="+ FeeHeadId.Text + " and receiptStatus<>'Cancelled'";
                if (oo.ReturnTag(sql, "cnt") != "0")
                {
                    edit.Enabled = false;
                    delete.Enabled = false;
                    edit.Text = "<i class='fa fa-lock'></i>";
                    delete.Text = "<i class='fa fa-lock'></i>";
                }
                Label Label9 = (Label)GridView1.Rows[i].FindControl("Label9");

                sql = "select count(*) cnt from BranchMaster where Classid=" + drpClass.SelectedValue+" and BranchCode=" + Session["BranchCode"].ToString() + " and SessionName='" + Session["SessionName"].ToString() + "' and IsDisplay=0 ";
                if (oo.ReturnTag(sql, "cnt") != "0")
                {
                    Label9.Visible = false;
                }
            }
            Label lblTotalAmt = (Label)GridView1.FooterRow.FindControl("lblTotalAmt");
            lblTotalAmt.Text =tot.ToString(CultureInfo.InvariantCulture)+".00";
        }
    }


    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        if (txtAmt.Text.Trim() == "")
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please enter valid amount!", "A");
            return;
        }
        double val = double.Parse(txtAmt.Text.Trim());
        if (val == 0)
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please enter valid amount!", "A");
            txtAmt.Text = "";
            return;
        }
        string sqls = "Select  * from FeeAllotedForClassWise where MonthId='" + drpInstallment.SelectedValue + "'  and ClassId='" + drpClass.SelectedValue + "'  and FeeHeadId='" + drpFeeHead.SelectedValue.ToString() + "'";
        sqls = sqls + " and SessionName='" + Session["SessionName"].ToString() + "' and medium='"+ drpMedium.SelectedValue + "' and branchid="+ drpBranch.SelectedValue + " and BranchCode=" + Session["BranchCode"].ToString() + " and CardType='" + drpFeeGroup.SelectedValue + "'";
        if (DrpNewOld.SelectedValue!="Both")
        {
            sqls = sqls + " and AdmissionType = '" + DrpNewOld.SelectedValue + "'";
        }
        if (oo.Duplicate(sqls))
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Duplicate fee for this head and installment!", "A");
            return;
        }
        if (DrpNewOld.SelectedItem.Text== "<-- Select Type of Admission -->" || drpFeeGroup.SelectedItem.Text == "<-- Select Fee Category -->" || drpClass.SelectedItem.Text == "<-- Select Class -->" || drpInstallment.SelectedItem.Text == "<--Select-->" || drpBranch.SelectedItem.Text == "<-- Select Stream-->" || drpMedium.SelectedItem.Text == "<--Select-->" || drpFeeHead.SelectedItem.Text == "<-- Select Fee -->" || txtAmt.Text == "")
        {
            //oo.MessageBox("Please <--Select--> Condition", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please <--Select--> Condition & fill amount", "A");
            return;
        }

        else
        {
            if (DrpNewOld.SelectedIndex == 3)
            {
                    int j=0;
                    int k=0;
             
                    sql = "Select FeeAmount from FeeAllotedForClassWise where MonthId='" + drpInstallment.SelectedValue.ToString() + "'  and ClassId='" + drpClass.SelectedValue.ToString() + "'  and FeeHeadId='" + drpFeeHead.SelectedValue.ToString() + "'";
                    sql +=  " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and CardType='" + drpFeeGroup.SelectedValue + "'  and AdmissionType='New'  and medium='" + drpMedium.SelectedValue + "' and branchid=" + drpBranch.SelectedValue + "";
                    if (oo.Duplicate(sql))
                    {
                        j = 0;
                        j = j+1;
                    }
                    sql = "Select  FeeAmount from FeeAllotedForClassWise where MonthId='" + drpInstallment.SelectedValue.ToString() + "'  and ClassId='" + drpClass.SelectedValue.ToString() + "'  and FeeHeadId='" + drpFeeHead.SelectedValue.ToString() + "'";
                    sql +=  " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and CardType='" + drpFeeGroup.SelectedValue + "'  and AdmissionType='Old'  and medium='" + drpMedium.SelectedValue + "' and branchid=" + drpBranch.SelectedValue + "";
                    if (oo.Duplicate(sql))
                    {
                        k = 0;
                        k = k+1;
                    }
                    if (j == 1 && k==0)
                    {
                        //oo.MessageBox("Fee of this month is alread allotted for type of admission New", this.Page);
                        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Fee of this insallment is already allotted for type of admission New", "A");       

                    }
                    else if (j==0 && k == 1)
                    {
                        //oo.MessageBox("Fee of this month is alread allotted for type of admission Old", this.Page);
                        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Fee of this insallment is already allotted for type of admission Old", "A");       


                    }
                    else if (j == 1 && k == 1)
                    {
                        //oo.MessageBox("Fee of this month is alread allotted for type of admission New and Old", this.Page);
                        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Fee of this insallment is already allotted for type of admission New and Old", "A");       

                    }
                    else
                    {
                        show();
                    }
               
            }
            else
            {
                sql = "Select  FeeAmount from FeeAllotedForClassWise where MonthId='" + drpInstallment.SelectedValue.ToString() + "'  and ClassId='" + drpClass.SelectedValue.ToString() + "' and BranchId='" + drpBranch.SelectedValue.ToString() + "' and Medium='" + drpMedium.SelectedValue.ToString() + "' and MOD='" + drpMod.SelectedValue.ToString() + "' and FeeHeadId='" + drpFeeHead.SelectedValue.ToString() + "'";
                sql +=  " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and CardType='" + drpFeeGroup.SelectedValue + "'  and AdmissionType='" + DrpNewOld.SelectedItem.ToString() + "'";

                if (oo.Duplicate(sql))
                {
                    //oo.MessageBox("Fee of this month is alread allotted", this.Page);
                    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Fee of this month is already allotted", "A");       

                    drpFeeHead.Focus();
                }
                else
                {
                    show();
                }
            
            }              
        }
    }

    public void show()
    {
        if (DrpNewOld.SelectedIndex != 3)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "FeeAllotedForClassWiseProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@ClassId", drpClass.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@Branchid", drpBranch.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@MOD", drpMod.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@Medium", drpMedium.SelectedValue.Trim());
            cmd.Parameters.AddWithValue("@MonthId", drpInstallment.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@FeeHeadId", drpFeeHead.SelectedValue);
            cmd.Parameters.AddWithValue("@cardtype", drpFeeGroup.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@AdmissionType", DrpNewOld.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@FeeAmount", txtAmt.Text.ToString());
            cmd.Parameters.AddWithValue("@Remark", txtRemarks.Text.ToString());
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
            try
            {

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");       
                txtAmt.Text = "";
                FeesDisplay();
                txtAmt.Focus();
            }
            catch (Exception ex) { }
        }
        else
        {
            for (int i = 1; i < 3; i++)
            {
                string typeofadd = "";
                if (i == 1)
                {
                    typeofadd = "NEW";
                }
                else
                {
                    typeofadd = "OLD";
                }

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FeeAllotedForClassWiseProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@ClassId", drpClass.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@Branchid", drpBranch.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@MOD", drpMod.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@Medium",drpMedium.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@MonthId", drpInstallment.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@FeeHeadId", drpFeeHead.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@cardtype", drpFeeGroup.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@AdmissionType", typeofadd.ToString());
                cmd.Parameters.AddWithValue("@FeeAmount", txtAmt.Text.ToString());
                cmd.Parameters.AddWithValue("@Remark", txtRemarks.Text.ToString());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception) { }
            }
            FeesDisplay();
            txtAmt.Focus();
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");       
        }
            
    }

    protected void TextBox2_TextChanged(object sender, EventArgs e)
    {

    }
    
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("Label36");
        string ss = lblId.Text;
        lblID.Text = ss;
       
        sql = "Select  ROW_NUMBER() OVER (ORDER BY Id ASC) AS SrNo,Id, Class,FeeHeadId,FeeAmount,remark,cardtype,AdmissionType,FeeName,Medium from FeeAllotedForClassWise ";       
        sql +=  " where Id=" + ss;
        sql +=  " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "and CardType='" + drpFeeGroup.SelectedValue + "'";

        txtFeeAmountPanel.Text = oo.ReturnTag(sql, "FeeAmount");
        try
        {
            drpAdmissionTypePanel.Text = oo.ReturnTag(sql, "AdmissionType");
        }
        catch (Exception) { }

        Panel1_ModalPopupExtender.Show();
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtAmt.Text = "";
        FeesDisplay();
    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadFeeMonth();
        loadBranch();
    }

    private void loadBranch()
    {
        sql = "Select BranchName,Id from BranchMaster where Classid='" + drpClass.SelectedValue.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        BAL.objBal.FillDropDown_withValue(sql, drpBranch, "BranchName", "Id");
        drpBranch.Items.Insert(0, new ListItem("<-- Select Stream-->", "<-- Select Stream-->"));
    }

    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {
        FeesDisplay();
        txtAmt.Focus();
        txtAmt.Text = "";


    }
   
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        sql = "Delete from FeeAllotedForClassWise where Id=" + lblvalue.Text;
        sql +=  " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and CardType='" + drpFeeGroup.SelectedValue + "'";

        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = sql;
        cmd.CommandType = CommandType.Text;
        cmd.Connection = con;
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Deleted successfully.", "S");       

            FeesDisplay();
        }
        catch (SqlException) { }
    }
   



    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        Button8.Focus();
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("Label37");
        string ss = lblId.Text;
        lblvalue.Text = ss.ToString();
       
        Panel2_ModalPopupExtender.Show();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "FeeAllotedForClassWiseUpdateProc";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
        cmd.Parameters.AddWithValue("@id",lblID.Text);
        cmd.Parameters.AddWithValue("@BranchId", drpBranch.SelectedValue.ToString() );
        cmd.Parameters.AddWithValue("@MOD", drpMod.SelectedValue.ToString());
        cmd.Parameters.AddWithValue("@FeeAmount", txtFeeAmountPanel.Text.ToString());
        cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());//Session["Login"].ToString());
        cmd.Parameters.AddWithValue("@AdmissionType", drpAdmissionTypePanel.SelectedItem.ToString());
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully.", "S");       
            FeesDisplay();
        }
        catch (Exception) { }
    }
    protected void Button4_Click(object sender, EventArgs e)
    {

    }
    protected void Button8_Click(object sender, EventArgs e)
    {

    }
    protected void DropDownList4_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadmedium();
        loadFeeMonth();
        txtAmt.Text = "";
    }

    public void loadFeeMonth()
    {
        sql = "select Monthid, MonthName from MonthMaster where (CardType='" + drpFeeGroup.SelectedItem.ToString() + "' or CardType='"+drpFeeGroup.SelectedValue.ToString()+"')";
        sql +=  " and (ClassId='" + drpClass.SelectedValue.ToString() + "' or ClassId is null) and (MOD='" + drpMod.SelectedValue.ToString() + "' or MOD is null)  and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " or monthid=0";
        sql +=  "  order by MonthId";
        oo.FillDropDown_withValue(sql, drpInstallment, "MonthName", "Monthid");
        drpInstallment.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
        GridView1.Visible = false;
    }

    protected void drpPanelCardType_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }
    protected void drpMonthPanel_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }


    public void PermissionGrant(int add1, int delete1, int update1, LinkButton Ladd, Button Ldelete, Button LUpdate)
    {


        if (add1 == 1)
        {
            Ladd.Enabled = true;
        }
        else
        {
            Ladd.Enabled = false;
        }


        if (delete1 == 1)
        {
            Ldelete.Enabled = true;
        }
        else
        {
            Ldelete.Enabled = false;
        }

        if (update1 == 1)
        {
            LUpdate.Enabled = true;
        }
        else
        {
            LUpdate.Enabled = false;
        }


    }
    public void CheckValueADDDeleteUpdate()
    {
        sql = " select LoginId,LoginName,Pass,SessionId,BranchId,LT.LoginTypeName,ltb.add1 as add1,ltb.delete1 as delete1,ltb.update1 as update1 from LoginTab LTb";
        sql +=  " left join LoginType Lt on LTb.LoginTypeId=Lt.LoginTypeId where LT.LoginTypeName='" + Session["Logintype"] + "' and LTb.LoginName='" + Session["LoginName"] + "'";
        int a, u, d;
        a = Convert.ToInt32(oo.ReturnTag(sql, "add1"));
        u = Convert.ToInt32(oo.ReturnTag(sql, "update1"));
        d = Convert.ToInt32(oo.ReturnTag(sql, "delete1"));

        PermissionGrant(a, d, u, (LinkButton)LinkButton1, btnDelete, Button3);
    }

    protected void DrpNewOld_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    protected void drpSelectFeeHeadIdPanel_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
  
    protected void drpMedium_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadFeeName();
        FeesDisplay();
    }
    protected void drpBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadFeeName();
        FeesDisplay();
    }
    protected void drpMod_SelectedIndexChanged(object sender, EventArgs e)
    {
        //FeesDisplay();
    }
}
  
