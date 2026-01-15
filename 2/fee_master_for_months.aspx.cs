using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;
using System.Globalization;

public partial class admin_FeesAllotmentForClasst : System.Web.UI.Page
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
                sql1 = sql1+ "  where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode='" + Session["BranchCode"].ToString() + "'";

                oo.FillDropDown_withValue(sql1, drpFeeGroup, "FeeGroupName","Id");
                drpFeeGroup.Items.Insert(0, new ListItem("<-- Select Fee Category -->", "<-- Select Fee Category -->"));
             
                sql = "select MonthName from MonthMaster where CardType='"+drpFeeGroup.SelectedItem.ToString()+"'";
                sql = sql + "  and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode='" + Session["BranchCode"].ToString() + "'";

                try
                {
                    oo.FillDropDown(sql, drpInstallment, "MonthName");
                    //oo.FillDropDown(sql, drpMonthPanel, "MonthName");
                  
                }
                catch (Exception) { }

                DrpNewOld.Focus();
                loadmedium();

                sql = "select ClassName,id from ClassMaster";
                sql = sql + " where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode='" + Session["BranchCode"].ToString() + "'";
                sql = sql + "  order by Id";
                oo.FillDropDown_withValue(sql, drpClass, "ClassName","id");
                drpClass.Items.Insert(0, new ListItem("<-- Select Class -->", "<-- Select Class -->"));
                //oo.FillDropDownWithOutSelect(sql, drpClassPanel, "ClassName");
                //drpClassPanel.Items.Insert(0, new ListItem("<-- Select Class -->", "<-- Select Class -->"));

                //sql = "select case when medium IS NULL then  Feename   else ltrim(rtrim(FeeName+' ('+Medium+')')) end as feeName  from FeeMaster";
                //sql = sql + "  where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode='" + Session["BranchCode"].ToString() + "'  order by feeid";

              

                sql = "Select case  when Upper(right(FeeType,2))='I)'  then  LEFT(RIGHT(FeeType,6),5)  else  LEFT(right(FeeType,8),7)  end as Medium, ROW_NUMBER() OVER (ORDER BY Id ASC) AS SrNo,Id,Month, FeeParticular,Class,FeeType,FeePayment from FeeAllotedForClassWise "; 
                sql = sql + "   where  SessionName='" + Session["SessionName"].ToString() + "' and BranchCode='" + Session["BranchCode"].ToString() + "' and CardType='" + drpFeeGroup.Text.ToString() + "' and Class='"+drpClass.SelectedItem.ToString()+"' and Month='"+drpInstallment.SelectedItem.ToString()+"'";
                GridView1.DataSource = oo.GridFill(sql);
                GridView1.DataBind();
                sql = "select feeName  from FeeMaster";
                sql = sql + "  where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode='" + Session["BranchCode"].ToString() + "'";
                sql = sql + "  and case when medium IS NULL then  Feename   else ltrim(rtrim(FeeName+' ('+Medium+')')) end ='" + drpFeeHead.Text.ToString() + "'";
                lblFeeHead.Text = oo.ReturnTag(sql, "FeeName");
                
            }

            catch (Exception ex) { }

        }
       
    }

    private void loadFeeName()
    {
        sql = "select Feename from FeeMaster where SessionName='" + Session["SessionName"].ToString() + "' ";
        sql = sql + "  and (Medium='" + drpMedium.SelectedItem.Text.Trim() + "' or Medium is null) and BranchCode='" + Session["BranchCode"].ToString() + "'  order by feeid";
        oo.FillDropDownWithOutSelect(sql, drpFeeHead, "feeName");
        drpFeeHead.Items.Insert(0, new ListItem("<-- Select Fee -->", "<-- Select Fee -->"));
    }

    public void loadmedium()
    {
        sql = "Select Medium From MediumMaster where SessionName='"+Session["SessionName"].ToString()+"'";
        oo.FillDropDown(sql, drpMedium,"Medium");
    }
    public void FeesDisplay()
    {
        GridView1.Visible = true;

        if (DrpNewOld.SelectedItem.Text.ToUpper()=="BOTH")
        {
            sql = "select Medium,ROW_NUMBER() OVER (ORDER BY facw.Id ASC) AS SrNo,facw.Id,Month,FeeType, FeeParticular,cm.ClassName as Class,bm.BranchName,dbo.ModeofFeeDepositeFun(MOD) as MOD,FeePayment,AdmissionType from FeeAllotedForClassWise facw";
            sql = sql + " Inner join ClassMaster cm on (CASE When ISNumeric(Class)=1 Then Convert(varchar,cm.Id) Else cm.ClassName End)=facw.Class and cm.SessionName=facw.SessionName Left join BranchMaster bm on bm.Id=facw.Branchid";
            sql = sql + " where (Class='" + drpClass.SelectedValue.ToString() + "' or Class='" + drpClass.SelectedItem.Text.ToString() + "') and Month='" + drpInstallment.SelectedItem.ToString() + "'";
            sql = sql + " and (facw.Branchid='" + drpBranch.SelectedValue.ToString() + "' or facw.Branchid is null) and (MOD='" + drpMod.SelectedValue.ToString() + "' or MOD is null) and facw.SessionName='" + Session["SessionName"].ToString() + "' and facw.BranchCode='" + Session["BranchCode"].ToString() + "' and CardType='" + drpFeeGroup.SelectedItem.ToString() + "'";
            sql = sql + " and Medium='" + drpMedium.SelectedItem.ToString() + "'";

            GridView1.DataSource = oo.GridFill(sql);
            GridView1.DataBind();                   
        }
        else
        {
            sql = "select Medium,ROW_NUMBER() OVER (ORDER BY facw.Id ASC) AS SrNo,facw.Id,Month,FeeType, FeeParticular,cm.ClassName as Class,bm.BranchName,dbo.ModeofFeeDepositeFun(MOD) as MOD,FeePayment,AdmissionType from FeeAllotedForClassWise facw ";
            sql = sql + " Inner join ClassMaster cm on (CASE When ISNumeric(Class)=1 Then Convert(varchar,cm.Id) Else cm.ClassName End)=facw.Class and cm.SessionName=facw.SessionName Left join BranchMaster bm on bm.Id=facw.Branchid";
            sql = sql + " where (Class='" + drpClass.SelectedValue.ToString() + "' or Class='" + drpClass.SelectedItem.Text.ToString() + "') and Month='" + drpInstallment.SelectedItem.ToString() + "'";
            sql = sql + " and (facw.Branchid='" + drpBranch.SelectedValue.ToString() + "' or facw.Branchid is null) and (MOD='" + drpMod.SelectedValue.ToString() + "' or MOD is null) and facw.SessionName='" + Session["SessionName"].ToString() + "' and facw.BranchCode='" + Session["BranchCode"].ToString() + "' and CardType='" + drpFeeGroup.SelectedItem.ToString() + "'";
            sql = sql + " and  AdmissionType='" + DrpNewOld.SelectedItem.Text.ToString() + "'";
            sql = sql + " and Medium='" + drpMedium.SelectedItem.ToString() + "'";

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
                tot = tot + Convert.ToDouble(lblAmt.Text);
            }
            Label lblTotalAmt = (Label)GridView1.FooterRow.FindControl("lblTotalAmt");
            lblTotalAmt.Text ="Total : "+ tot.ToString(CultureInfo.InvariantCulture)+".00";
           
        }
        

    }


    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        if (DrpNewOld.SelectedItem.Text== "<-- Select Type of Admission -->" || drpFeeGroup.SelectedItem.Text == "<-- Select Fee Category -->" || drpClass.SelectedItem.Text == "<-- Select Class -->" || drpInstallment.SelectedItem.Text == "<--Select-->" || drpBranch.SelectedItem.Text == "<-- Select Branch-->" || drpMedium.SelectedItem.Text == "<--Select-->" || drpFeeHead.SelectedItem.Text == "<-- Select Fee -->" || txtAmt.Text == "")
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
             
                    sql = "Select  ROW_NUMBER() OVER (ORDER BY Id ASC) AS SrNo,Id,Month, FeeParticular,Class,FeeType,FeePayment from FeeAllotedForClassWise where Month='" + drpInstallment.SelectedItem.ToString() + "'  and Class='" + drpClass.SelectedItem.ToString() + "'  and FeeType='" + drpFeeHead.SelectedItem.ToString() + "'";
                    sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode='" + Session["BranchCode"].ToString() + "' and CardType='" + drpFeeGroup.SelectedItem.ToString() + "'  and AdmissionType='New' ";
                    if (oo.Duplicate(sql))
                    {
                        j = 0;
                        j = j+1;
                    }
                    sql = "Select  ROW_NUMBER() OVER (ORDER BY Id ASC) AS SrNo,Id,Month, FeeParticular,Class,FeeType,FeePayment from FeeAllotedForClassWise where Month='" + drpInstallment.SelectedItem.ToString() + "'  and Class='" + drpClass.SelectedItem.ToString() + "'  and FeeType='" + drpFeeHead.SelectedItem.ToString() + "'";
                    sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode='" + Session["BranchCode"].ToString() + "' and CardType='" + drpFeeGroup.SelectedItem.ToString() + "'  and AdmissionType='Old' ";
                    if (oo.Duplicate(sql))
                    {
                        k = 0;
                        k = k+1;
                    }
                    if (j == 1 && k==0)
                    {
                        //oo.MessageBox("Fee of this month is alread allotted for type of admission New", this.Page);
                        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Fee of this month is alread allotted for type of admission New", "A");       

                    }
                    else if (j==0 && k == 1)
                    {
                        //oo.MessageBox("Fee of this month is alread allotted for type of admission Old", this.Page);
                        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Fee of this month is alread allotted for type of admission Old", "A");       


                    }
                    else if (j == 1 && k == 1)
                    {
                        //oo.MessageBox("Fee of this month is alread allotted for type of admission New and Old", this.Page);
                        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Fee of this month is alread allotted for type of admission New and Old", "A");       

                    }
                    else
                    {
                        show();
                    }
               
            }
            else
            {
                sql = "Select  *from FeeAllotedForClassWise where Month='" + drpInstallment.SelectedItem.ToString() + "'  and Class='" + drpClass.SelectedValue.ToString() + "' and BranchId='" + drpBranch.SelectedValue.ToString() + "' and Medium='" + drpMedium.SelectedValue.ToString() + "' and MOD='" + drpMod.SelectedValue.ToString() + "' and FeeType='" + drpFeeHead.SelectedItem.ToString() + "'";
                sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode='" + Session["BranchCode"].ToString() + "' and CardType='" + drpFeeGroup.SelectedItem.ToString() + "'  and AdmissionType='" + DrpNewOld.SelectedItem.ToString() + "'";

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
            cmd.Parameters.AddWithValue("@Month", drpInstallment.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@FeeParticular", "");

            cmd.Parameters.AddWithValue("@Class", drpClass.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@Branchid", drpBranch.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@MOD", drpMod.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@FeeType", drpFeeHead.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@FeePayment", txtAmt.Text.ToString());
            cmd.Parameters.AddWithValue("@Remark", txtRemarks.Text.ToString());
            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());//Session["Login"].ToString());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
            cmd.Parameters.AddWithValue("@cardtype", drpFeeGroup.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@AdmissionType", DrpNewOld.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@Medium", drpMedium.SelectedItem.Text.Trim());
            try
            {

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                //oo.MessageBox("Submitted successfully.", this.Page);
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");       

                txtAmt.Text = "";
                FeesDisplay();
                txtAmt.Focus();

            }
            catch (Exception) { }
        }
        else
        {
            for (int i = 1; i < 3; i++)
            {
                string typeofadd = "";
                if (i == 1)
                {
                    typeofadd = "New";
                }
                else
                {
                    typeofadd = "Old";
                }

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FeeAllotedForClassWiseProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Month", drpInstallment.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@FeeParticular", "");
                cmd.Parameters.AddWithValue("@Class", drpClass.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@Branchid", drpBranch.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@MOD", drpMod.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@FeeType", drpFeeHead.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@FeePayment", txtAmt.Text.ToString());
                cmd.Parameters.AddWithValue("@Remark", txtRemarks.Text.ToString());
                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());//Session["Login"].ToString());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
                cmd.Parameters.AddWithValue("@cardtype", drpFeeGroup.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@AdmissionType", typeofadd.ToString());
                cmd.Parameters.AddWithValue("@Medium",drpMedium.SelectedItem.Text.Trim());
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
            //oo.MessageBox("Submitted successfully.", this.Page);
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

       
        sql = "Select  ROW_NUMBER() OVER (ORDER BY Id ASC) AS SrNo,Id,Month, FeeParticular,Class,FeeType,FeePayment,remark,cardtype,AdmissionType,FeeName,Medium from FeeAllotedForClassWise ";       
        sql = sql + " where Id=" + ss;
        sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode='" + Session["BranchCode"].ToString() + "'and CardType='" + drpFeeGroup.SelectedItem.ToString() + "'";

        txtFeePaymentPanelAmt.Text = oo.ReturnTag(sql, "FeePayment");

        try
        {
            drpAdmissionTypePanel.Text = oo.ReturnTag(sql, "AdmissionType");
        }
        catch (Exception) { }

        Panel1_ModalPopupExtender.Show();
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        FeesDisplay();
        drpClass.Focus();
    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadFeeMonth();
        loadBranch();
        //FeesDisplay();
        drpFeeHead.Focus();
    }

    private void loadBranch()
    {
        sql = "Select BranchName,Id from BranchMaster where Classid='" + drpClass.SelectedValue.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode='" + Session["BranchCode"].ToString() + "'";
        BAL.objBal.FillDropDown_withValue(sql, drpBranch, "BranchName", "Id");
        drpBranch.Items.Insert(0, new ListItem("<-- Select Branch-->", "<-- Select Branch-->"));
    }

    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {
        FeesDisplay();
        sql = "select feeName,medium  from FeeMaster";
        sql = sql + "  where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode='" + Session["BranchCode"].ToString() + "'";
        sql = sql + "  and case when medium IS NULL then  Feename   else ltrim(rtrim(FeeName+' ('+Medium+')')) end ='" + drpFeeHead.SelectedItem.ToString() + "'";
        lblFeeHead.Text = oo.ReturnTag(sql, "FeeName");
        lblMedium.Text = oo.ReturnTag(sql, "Medium");
        txtAmt.Focus();
       

    }
   
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        sql = "Delete from FeeAllotedForClassWise where Id=" + lblvalue.Text;
        sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode='" + Session["BranchCode"].ToString() + "' and CardType='" + drpFeeGroup.SelectedItem.ToString() + "'";

        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = sql;
        cmd.CommandType = CommandType.Text;
        cmd.Connection = con;
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            //oo.MessageBox("Deleted successfully.", this.Page);
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
        //cmd.Parameters.AddWithValue("@Month", Label7.Text.ToString());
        //cmd.Parameters.AddWithValue("@FeeParticular", "");
        cmd.Parameters.AddWithValue("@BranchId", drpBranch.SelectedValue.ToString() );
        cmd.Parameters.AddWithValue("@MOD", drpMod.SelectedValue.ToString());
        //cmd.Parameters.AddWithValue("@Class", drpClassPanel.SelectedItem.ToString());
        //cmd.Parameters.AddWithValue("@FeeType", drpSelectFeeTypePanel.SelectedItem.ToString());
        cmd.Parameters.AddWithValue("@FeePayment", txtFeePaymentPanelAmt.Text.ToString());
        //cmd.Parameters.AddWithValue("@Remark", txtRemarkPanel.Text.ToString());

        cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());//Session["Login"].ToString());
        //cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
        //cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
        //cmd.Parameters.AddWithValue("@cardtype", Label6.Text.ToString());
        cmd.Parameters.AddWithValue("@AdmissionType", drpAdmissionTypePanel.SelectedItem.ToString());
        //cmd.Parameters.AddWithValue("@FeeName", lblFeeHeadPanel.Text);
        //cmd.Parameters.AddWithValue("@Medium", lblMediumPanel.Text);

        try
        {

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            //oo.MessageBox("Updated successfully.", this.Page);
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
        //loadFeeMonth();
        //drpInstallment.Focus();
    }

    public void loadFeeMonth()
    {
        sql = "select MonthName from MonthMaster where (CardType='" + drpFeeGroup.SelectedItem.ToString() + "' or CardType='"+drpFeeGroup.SelectedValue.ToString()+"')";
        sql = sql + " and (ClassId='" + drpClass.SelectedValue.ToString() + "' or ClassId is null) and (MOD='" + drpMod.SelectedValue.ToString() + "' or MOD is null)  and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode='" + Session["BranchCode"].ToString() + "' or monthid=0";
        sql = sql + "  order by MonthId";
        oo.FillDropDownWithOutSelect(sql, drpInstallment, "MonthName");
        GridView1.Visible = false;
    }

    protected void drpPanelCardType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //sql = "select MonthName from MonthMaster where CardType='" + drpPanelCardType.SelectedItem.ToString() + "'";
        //sql = sql + "  and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode='" + Session["BranchCode"].ToString() + "' or monthid=0";
        //sql = sql + "  order by MonthId";
        //oo.FillDropDownWithOutSelect(sql, DropDownList1, "MonthName");
        //GridView1.Visible = true;
    }
    protected void drpMonthPanel_SelectedIndexChanged(object sender, EventArgs e)
    {
        //sql = "Select  ROW_NUMBER() OVER (ORDER BY Id ASC) AS SrNo,Id,Month, FeeParticular,Class,FeeType,FeePayment from FeeAllotedForClassWise  ";
        //sql = sql + " where Class='" + DropDownList2.SelectedItem.ToString() + "' and Month='" + DropDownList1.SelectedItem.ToString() + "'";
        //sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode='" + Session["BranchCode"].ToString() + "' and CardType='" + drpPanelCardType.SelectedItem.ToString() + "'";
        //GridView1.DataSource = oo.GridFill(sql);
        //GridView1.DataBind();
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
        sql = sql + " left join LoginType Lt on LTb.LoginTypeId=Lt.LoginTypeId where LT.LoginTypeName='" + Session["Logintype"] + "' and LTb.LoginName='" + Session["LoginName"] + "'";
        int a, u, d;
        a = Convert.ToInt32(oo.ReturnTag(sql, "add1"));
        u = Convert.ToInt32(oo.ReturnTag(sql, "update1"));
        d = Convert.ToInt32(oo.ReturnTag(sql, "delete1"));

        PermissionGrant(a, d, u, (LinkButton)LinkButton1, btnDelete, Button3);
    }

    protected void DrpNewOld_SelectedIndexChanged(object sender, EventArgs e)
    {
        //FeesDisplay();
        //drpFeeGroup.Focus();
    }
    protected void drpSelectFeeTypePanel_SelectedIndexChanged(object sender, EventArgs e)
    {
        //sql = "select feeName,medium  from FeeMaster";
        //sql = sql + "  where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode='" + Session["BranchCode"].ToString() + "'";
        //sql = sql + "  and case when medium IS NULL then  Feename   else ltrim(rtrim(FeeName+' ('+Medium+')')) end ='" + drpSelectFeeTypePanel.SelectedItem.ToString() + "'";
        //lblFeeHeadPanel.Text = oo.ReturnTag(sql, "FeeName");
        //lblMediumPanel.Text = oo.ReturnTag(sql, "medium");

    }
  
    protected void drpMedium_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadFeeName();
        FeesDisplay();
    }
    protected void drpBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadmedium();
        loadFeeName();
        FeesDisplay();
    }
    protected void drpMod_SelectedIndexChanged(object sender, EventArgs e)
    {
        //FeesDisplay();
    }
}
  
