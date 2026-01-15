using System;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class PermissionFeeDepoist_New : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";
#pragma warning disable 169
    DataTable dt;
#pragma warning restore 169
#pragma warning disable 169
    double totsum = 0;
#pragma warning restore 169
#pragma warning disable 169
    double price = 0;
#pragma warning restore 169
#pragma warning disable 169
    string SrNo = "";
#pragma warning restore 169
#pragma warning disable 169
    string Enreg = "";
#pragma warning restore 169
#pragma warning disable 169
    int cp = 0;
#pragma warning restore 169
    string s = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);
        if (!IsPostBack)
        {
            if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
            {
                Response.Redirect("default.aspx");
            }
            Table1.Visible = false;
            Permission_tab1.Visible = false;
            LinkButton10.Visible = false;

            DropDownList1.Items.Add(Session["SessionName"].ToString());

            Student_display();
            loadtablename();

            showDiv();
        }

    }
    public void loadtablename()
    {
        sql = "Select Id,TableName from TableNameForpermission where TableName not in('Transport Fee','Hostel Fee','Miscellaneous Fee')";
        GridView1.DataSource = oo.GridFill(sql);
        GridView1.DataBind();
    }
    public void loadtablename1()
    {
        sql = "Select Id,TableName from TableNameForpermission";
        GridView2.DataSource = oo.GridFill(sql);
        GridView2.DataBind();
    }
    public void Student_display()
    {
        sql = "select ROW_NUMBER() Over(Order By ap.Id) as Ids,ap.SrNo,StEnCode,Permission,tnp.TableName from Administrator_Permission ap inner join StudentOfficialDetails sf on ap.SrNo=sf.SrNo inner join TableNameForpermission tnp on tnp.Id=ap.tableId where Permission like 'Yes%' and Permission_Session='" + DropDownList1.SelectedItem.Text + "' and SessionName='" + DropDownList1.SelectedItem.Text + "' and Withdrwal is null ";
        Grd0.DataSource = oo.GridFill(sql);
        Grd0.DataBind();
        if (Grd0.Rows.Count <= 0)
        {
        }
        else
        {
            for (int j = 0; j < Grd0.Rows.Count; j++)
            {
                Label Label32 = (Label)Grd0.Rows[j].FindControl("Label32");
                Label Label34 = (Label)Grd0.Rows[j].FindControl("Label34");
                Label Label35 = (Label)Grd0.Rows[j].FindControl("Label35");
                Label Label36 = (Label)Grd0.Rows[j].FindControl("Label36");
                Label Label37 = (Label)Grd0.Rows[j].FindControl("Label37");
                Label Label38 = (Label)Grd0.Rows[j].FindControl("Label38");
                Label Label39 = (Label)Grd0.Rows[j].FindControl("Label39");
                Label Label40 = (Label)Grd0.Rows[j].FindControl("Label40");
                Label Label41 = (Label)Grd0.Rows[j].FindControl("Label41");
                Label Label42 = (Label)Grd0.Rows[j].FindControl("Label42");
                Label Label43 = (Label)Grd0.Rows[j].FindControl("Label43");
                Label Label44 = (Label)Grd0.Rows[j].FindControl("Label44");
                sql = "select SG.Id, SC.SectionName,SO.Card,SO.Medium as Medium,CM.ClassName,convert(nvarchar,So.DateOfAdmiission,106) as DateOfAdmiission ,SO.SectionId,Sf.FatherName,SF.MotherName,SG.FirstName,SG.MiddleName,SG.LastName,so.StEnRCode as StEnRCode,sg.srno  as srno,case  when so.TransportRequired='Yes' then 'Yes' else 'No' end as TransportRequired,so.wayamount as wayamount from StudentGenaralDetail SG ";
                sql = sql + "    left join StudentFamilyDetails SF on SG.srno=SF.srno";
                sql = sql + "   left join StudentOfficialDetails SO on SG.srno=SO.srno";
                sql = sql + "   left join ClassMaster CM on SO.AdmissionForClassId=CM.Id";
                sql = sql + "   left join SectionMaster SC on SO.SectionId=SC.Id";
                sql = sql + "    where  SG.SrNo=" + "'" + Label32.Text + "'";
                sql = sql + " and sg.SessionName='" + DropDownList1.SelectedItem.Text + "' and ";
                sql = sql + "     so.SessionName='" + DropDownList1.SelectedItem.Text + "' and sf.SessionName='" + DropDownList1.SelectedItem.Text + "' and cm.SessionName='" + DropDownList1.SelectedItem.Text + "'";
                sql = sql + "    and SC.SessionName='" + DropDownList1.SelectedItem.Text + "'  and";
                sql = sql + " sg.BranchCode=" + Session["BranchCode"].ToString() + "";
                sql = sql + "   and SO.Withdrwal is null";

                Label34.Text = oo.ReturnTag(sql, "FirstName");
                Label35.Text = oo.ReturnTag(sql, "MiddleName");
                Label36.Text = oo.ReturnTag(sql, "LastName");
                Label37.Text = oo.ReturnTag(sql, "FatherName");
                Label38.Text = oo.ReturnTag(sql, "ClassName");
                Label39.Text = oo.ReturnTag(sql, "SectionName");
                Label40.Text = oo.ReturnTag(sql, "Medium");
                Label41.Text = oo.ReturnTag(sql, "Card");
                Label42.Text = oo.ReturnTag(sql, "DateOfAdmiission");
                Label43.Text = oo.ReturnTag(sql, "TransportRequired");
                string[] yesno = Label44.Text.Split(',');
                Label44.Text = yesno[0].ToString();
                Grd0.Visible=true;
            }
        }


    }
    protected void LinkButton6_Click(object sender, EventArgs e)
    {
        view();
    }

    public void view()
    {
        string studentId = Request.Form[hfStudentId.UniqueID];
        if (studentId == string.Empty)
        {
            studentId = TxtEnter.Text.Trim();
        }
        loadtablename1();
        con.Open();
        s = DropDownList1.SelectedItem.ToString();
        con.Close();
        sql = "select SG.Id, SC.SectionName,SO.Card,SO.Medium as Medium,CM.ClassName,convert(nvarchar,So.DateOfAdmiission,106) as DateOfAdmiission ,SO.SectionId,Sf.FatherName,SF.MotherName,SG.FirstName,SG.MiddleName,SG.LastName,so.StEnRCode as StEnRCode,sg.srno  as srno,case  when so.TransportRequired='Yes' then 'Yes' else 'No' end as TransportRequired,so.wayamount as wayamount from StudentGenaralDetail SG ";
        sql = sql + "    left join StudentFamilyDetails SF on SG.srno=SF.srno";
        sql = sql + "   left join StudentOfficialDetails SO on SG.srno=SO.srno";
        sql = sql + "   left join ClassMaster CM on SO.AdmissionForClassId=CM.Id";
        sql = sql + "   left join SectionMaster SC on SO.SectionId=SC.Id";
        sql = sql + "    where  SG.Srno=" + "'" + studentId + "'";
        sql = sql + " and sg.SessionName='" + DropDownList1.SelectedItem.Text + "' and ";
        sql = sql + "     so.SessionName='" + DropDownList1.SelectedItem.Text + "' and sf.SessionName='" + DropDownList1.SelectedItem.Text + "' and cm.SessionName='" + DropDownList1.SelectedItem.Text + "'";
        sql = sql + "    and SC.SessionName='" + DropDownList1.SelectedItem.Text + "'  and";
        sql = sql + " sg.BranchCode=" + Session["BranchCode"].ToString() + "";
        sql = sql + "   and SO.Withdrwal is null";
        Grd.DataSource = oo.GridFill(sql);
        Grd.DataBind();
        if (Grd.Rows.Count <= 0)
        {
            //oo.MessageBox("Sorry, Invalid " + DrpEnter.SelectedItem.Text+"!", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, Invalid " + DrpEnter.SelectedItem.Text + "!", "A");

            Permission_tab.Visible = false;
        }

        else
        {
            Permission_tab.Visible = true;
            Grd.Visible = true;
            loadlockunlock();
        }

        Session["Permission_Session"] = s.ToString();
    }

    protected void LinkButton7_Click(object sender, EventArgs e)
    {
        loackunloack();
        Student_display();
    }
    public void loackunloack()
    {
        Label lbl_sr = (Label)Grd.Rows[0].FindControl("Label1");
        Label lbl_StEnrCode = (Label)Grd.Rows[0].FindControl("Label18");
        Permission_tab.Visible = false;
        string permission_value = "";
        sql = "Select Distinct TableId From Administrator_Permission where SrNo='" + lbl_sr.Text + "' and Permission_Session='" + DropDownList1.SelectedItem.Text + "'";
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            for (int j = 0; j < GridView1.Rows.Count; j++)
            {
                Label lblId = (Label)GridView1.Rows[j].FindControl("lblId");
                permission_value = setvalue1(j);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][0].ToString() == lblId.Text)
                    {
                        SqlCommand permission_granted_update = new SqlCommand("Update Administrator_Permission set Permission='" + permission_value.ToString() + "' where TableId='"+lblId.Text+"' and SrNo='" + lbl_sr.Text + "' and Permission_Session='" + DropDownList1.SelectedItem.ToString() + "'", con);
                        con.Open();
                        permission_granted_update.ExecuteNonQuery();
                        con.Close();
                        //oo.MessageBoxforUpdatePanel("Permission granted successfully", LinkButton7);
                        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Permission granted successfully", "S");       


                    }
                    else
                    {
                        SqlCommand permission_count = new SqlCommand("Select count(*) from Administrator_Permission where TableId='" + lblId.Text + "' and SrNo='" + lbl_sr.Text + "' and Permission_Session='" + DropDownList1.SelectedItem.Text + "' ", con);
                        con.Open();
                        int permission_count_value = (int)permission_count.ExecuteScalar();
                        con.Close();
                        if (permission_count_value == 0)
                        {
                            SqlCommand permission_granted = new SqlCommand("Insert Into Administrator_Permission(SrNo,StEnCode,Permission,Permission_Session,TableId) Values ('" + lbl_sr.Text + "','" + lbl_StEnrCode.Text + "','" + permission_value.ToString() + "','" + DropDownList1.SelectedItem.ToString() + "','" + lblId.Text + "')", con);
                            con.Open();
                            permission_granted.ExecuteNonQuery();
                            con.Close();
                            //oo.MessageBoxforUpdatePanel("Permission granted successfully", LinkButton7);
                            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Permission granted successfully", "S");       

                        }

                    }
                }

            }

        }
        else
        {
            for (int j = 0; j < GridView1.Rows.Count; j++)
            {
                Label lblId = (Label)GridView1.Rows[j].FindControl("lblId");
                permission_value = setvalue1(j);
                SqlCommand permission_granted = new SqlCommand("Insert Into Administrator_Permission(SrNo,StEnCode,Permission,Permission_Session,TableId) Values ('" + lbl_sr.Text + "','" + lbl_StEnrCode.Text + "','" + permission_value.ToString() + "','" + DropDownList1.SelectedItem.ToString() + "','" + lblId.Text + "')", con);
                con.Open();
                permission_granted.ExecuteNonQuery();
                con.Close();
                //oo.MessageBoxforUpdatePanel("Permission granted successfully", LinkButton7);
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Permission granted successfully", "S");       


            }
        }
        Permission_tab.Visible = false;
    }
    public string setvalue1(int j)
    {
        string permission_value = "";
        CheckBox chkDate = (CheckBox)GridView2.Rows[j].FindControl("chkDate");
        CheckBox chkConcession = (CheckBox)GridView2.Rows[j].FindControl("chkConcession");
        CheckBox chkPaidamount = (CheckBox)GridView2.Rows[j].FindControl("chkPaidamount");
        if (chkDate.Checked == true && chkConcession.Checked == true && chkPaidamount.Checked == true)
        {
            permission_value = "Yes,1,2,3";
        }

        else if (chkDate.Checked == true && chkConcession.Checked == true && chkPaidamount.Checked == false)
        {
            permission_value = "Yes,1,2";
        }

        else if (chkDate.Checked == true && chkConcession.Checked == false && chkPaidamount.Checked == true)
        {
            permission_value = "Yes,1,3";
        }

        else if (chkDate.Checked == false && chkConcession.Checked == true && chkPaidamount.Checked == true)
        {
            permission_value = "Yes,2,3";
        }

        else if (chkDate.Checked == true && chkConcession.Checked == false && chkPaidamount.Checked == false)
        {
            permission_value = "Yes,1";
        }

        else if (chkDate.Checked == false && chkConcession.Checked == true && chkPaidamount.Checked == false)
        {
            permission_value = "Yes,2";
        }

        else if (chkDate.Checked == false && chkConcession.Checked == false && chkPaidamount.Checked == true)
        {
            permission_value = "Yes,3";
        }

        else
        {
            permission_value = "No,1,2,3";
        }
        return permission_value;
    }
    public void loackunloackAll()
    {
        string permission_value = "";
        sql = "Select Distinct TableId From Admin_fee_permission_setting where SessionName='" + DropDownList1.SelectedItem.Text + "'";
        SqlDataAdapter da = new SqlDataAdapter(sql,con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
#pragma warning disable 219
            bool flag = false;
#pragma warning restore 219
            for (int j = 0; j < GridView1.Rows.Count; j++)
            {
                Label lblId = (Label)GridView1.Rows[j].FindControl("lblId");
                permission_value= setvalue(j);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][0].ToString() == lblId.Text)
                    {
                        sql = "UPDATE Admin_fee_permission_setting SET Enable= '" + permission_value.ToString() + "' WHERE TableId='" + lblId.Text + "' and SessionName='" + DropDownList1.SelectedItem.Text + "'";
                        SqlCommand permission_granted_update = new SqlCommand(sql, con);
                        con.Open();
                        permission_granted_update.ExecuteNonQuery();
                        con.Close();
                        //oo.MessageBoxforUpdatePanel("Permission granted successfully", LinkButton10);
                        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Permission granted successfully", "S");       

                    }
                    else
                    {
                        SqlCommand permission_count = new SqlCommand("Select count(*) from Admin_fee_permission_setting where TableId='" + lblId.Text + "' and SessionName='" + DropDownList1.SelectedItem.Text + "'", con);
                        con.Open();
                        int permission_count_value = (int)permission_count.ExecuteScalar();
                        con.Close();
                        if (permission_count_value == 0)
                        {
                            sql = "INSERT INTO Admin_fee_permission_setting([TableId],[SessionName],[Enable]) VALUES('" + lblId.Text + "','" + DropDownList1.SelectedItem.Text + "','" + permission_value.ToString() + "')";
                            SqlCommand permission_granted = new SqlCommand(sql, con);
                            con.Open();
                            permission_granted.ExecuteNonQuery();
                            con.Close();
                            //oo.MessageBoxforUpdatePanel("Permission granted successfully", LinkButton10);
                            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Permission granted successfully", "S");       

                        }

                    }
                }

            }

        }
        else
        {
            for (int j = 0; j < GridView1.Rows.Count; j++)
            {
                Label lblId = (Label)GridView1.Rows[j].FindControl("lblId");
                permission_value = setvalue(j);
                sql = "INSERT INTO Admin_fee_permission_setting([TableId],[SessionName],[Enable]) VALUES('" + lblId.Text + "','" + DropDownList1.SelectedItem.Text + "','" + permission_value.ToString() + "')";
                SqlCommand permission_granted = new SqlCommand(sql, con);
                con.Open();
                permission_granted.ExecuteNonQuery();
                con.Close();
                //oo.MessageBoxforUpdatePanel("Permission granted successfully", LinkButton10);
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Permission granted successfully", "S");       

            }
        }
        string ss = "select TuitionFee, TransportFee, HostelFee, MiscFee from GuardianFeeDepositPermission where BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"]+"'";
        if (!oo.Duplicate(ss))
        {
            sql = "INSERT INTO GuardianFeeDepositPermission(TuitionFee, SessionName, BranchCode) VALUES(" + (chkTuitionFee.Checked==true?1:0) + ", '" + Session["SessionName"] + "'," + Session["BranchCode"] + ")";
            SqlCommand cmds = new SqlCommand(sql, con);
            con.Open();
            cmds.ExecuteNonQuery();
            con.Close();
        }
        else
        {
            sql = "update GuardianFeeDepositPermission set TuitionFee=" + (chkTuitionFee.Checked == true ? 1 : 0) + " where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            SqlCommand cmds = new SqlCommand(sql, con);
            con.Open();
            cmds.ExecuteNonQuery();
            con.Close();
        }
        

        Permission_tab.Visible = false;
    }
    public string setvalue(int j)
    {
        string permission_value = "";
        CheckBox chkDate = (CheckBox)GridView1.Rows[j].FindControl("chkDate");
        CheckBox chkConcession = (CheckBox)GridView1.Rows[j].FindControl("chkConcession");
        CheckBox chkPaidamount = (CheckBox)GridView1.Rows[j].FindControl("chkPaidamount");
        if (chkDate.Checked == true && chkConcession.Checked == true && chkPaidamount.Checked == true)
        {
            permission_value = "Yes,1,2,3";
        }

        else if (chkDate.Checked == true && chkConcession.Checked == true && chkPaidamount.Checked == false)
        {
            permission_value = "Yes,1,2";
        }

        else if (chkDate.Checked == true && chkConcession.Checked == false && chkPaidamount.Checked == true)
        {
            permission_value = "Yes,1,3";
        }

        else if (chkDate.Checked == false && chkConcession.Checked == true && chkPaidamount.Checked == true)
        {
            permission_value = "Yes,2,3";
        }

        else if (chkDate.Checked == true && chkConcession.Checked == false && chkPaidamount.Checked == false)
        {
            permission_value = "Yes,1";
        }

        else if (chkDate.Checked == false && chkConcession.Checked == true && chkPaidamount.Checked == false)
        {
            permission_value = "Yes,2";
        }

        else if (chkDate.Checked == false && chkConcession.Checked == false && chkPaidamount.Checked == true)
        {
            permission_value = "Yes,3";
        }

        else
        {
            permission_value = "No,1,2,3";
        }
        return permission_value;
    }

    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        showDiv();
    }

    public void showDiv()
    {
        if (RadioButtonList1.SelectedIndex == 0)
        {
            Table1.Visible = false;
            Permission_tab.Visible = false;
            Grd.Visible = false;
            Permission_tab1.Visible = true;
            LinkButton10.Visible = true;


            if (Grd0.Rows.Count > 0)
            {
                Grd0.Visible = true;
            }
            else
            {
                Grd0.Visible = false;
            }
            loadlockunlockall();

        }
        else if (RadioButtonList1.SelectedIndex == 1)
        {
            Table1.Visible = true;
            Permission_tab1.Visible = false;
            LinkButton10.Visible = false;

            if (Grd0.Rows.Count > 0)
            {
                Grd0.Visible = true;
            }
            else
            {
                Grd0.Visible = false;
            }
        }
    }

    //protected void LinkButton8_Click(object sender, EventArgs e)
    //{
    //    Table1.Visible = false;
    //    Permission_tab.Visible = false;
    //    Grd.Visible = false;
    //    Permission_tab1.Visible = true;
    //    LinkButton10.Visible = true;
      

    //    if (Grd0.Rows.Count > 0)
    //    {
    //        Grd0.Visible = true;
    //    }
    //    else
    //    {
    //        Grd0.Visible = false;
    //    }
    //    loadlockunlockall();
    //}
    public void loadlockunlockall()
    {
        try
        {
            s = DropDownList1.SelectedItem.ToString();
            sql = "select Enable from Admin_fee_permission_setting where SessionName='" + DropDownList1.SelectedItem.Text + "'";
            SqlCommand permission_radio = new SqlCommand(sql, con);
            con.Open();
            DataTable dt = new DataTable();
            dt.Load(permission_radio.ExecuteReader());
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                CheckBox chkDate = (CheckBox)GridView1.Rows[i].FindControl("chkDate");
                CheckBox chkConcession = (CheckBox)GridView1.Rows[i].FindControl("chkConcession");
                CheckBox chkPaidamount = (CheckBox)GridView1.Rows[i].FindControl("chkPaidamount");
                string[] str = dt.Rows[i][0].ToString().Split(',');
                if (str[0].ToString() == "Yes")
                {
                    if (str.Length == 4)
                    {
                        chkDate.Checked = true;
                        chkConcession.Checked = true;
                        chkPaidamount.Checked = true;
                    }
                    else if (str.Length == 3)
                    {
                        if (str[1].ToString() == "1" && str[2].ToString() == "2")
                        {
                            chkDate.Checked = true;
                            chkConcession.Checked = true;
                            chkPaidamount.Checked = false;
                        }
                        else if (str[1].ToString() == "2" && str[2].ToString() == "3")
                        {
                            chkDate.Checked = false;
                            chkConcession.Checked = true;
                            chkPaidamount.Checked = true;
                        }
                        else if (str[1].ToString() == "1" && str[2].ToString() == "3")
                        {
                            chkDate.Checked = true;
                            chkConcession.Checked = false;
                            chkPaidamount.Checked = true;
                        }
                    }
                    else if (str.Length == 2)
                    {
                        if (str[1].ToString() == "1")
                        {
                            chkDate.Checked = true;
                            chkConcession.Checked = false;
                            chkPaidamount.Checked = false;
                        }
                        else if (str[1].ToString() == "2")
                        {
                            chkDate.Checked = false;
                            chkConcession.Checked = true;
                            chkPaidamount.Checked = false;
                        }
                        else if (str[1].ToString() == "3")
                        {
                            chkDate.Checked = false;
                            chkConcession.Checked = false;
                            chkPaidamount.Checked = true;
                        }
                    }
                }
                else
                {
                    chkDate.Checked = false;
                    chkConcession.Checked = false;
                    chkPaidamount.Checked = false;
                }
            }
            Session["Permission_Session"] = s.ToString();
        }
        catch
        {
        }
    }
    public void loadlockunlock()
    {
        string studentId = Request.Form[hfStudentId.UniqueID];
        if (studentId == string.Empty)
        {
            studentId = TxtEnter.Text.Trim();
        }
        s = DropDownList1.SelectedItem.ToString();
        sql = "select Permission from Administrator_Permission where SrNo='" + studentId + "' and Permission_Session='" + s + "'";
        SqlCommand permission_radio = new SqlCommand(sql, con);
        con.Open();
        DataTable dt = new DataTable();
        dt.Load(permission_radio.ExecuteReader());
        for (int i = 0; i < GridView2.Rows.Count; i++)
        {
            CheckBox chkDate = (CheckBox)GridView2.Rows[i].FindControl("chkDate");
            CheckBox chkConcession = (CheckBox)GridView2.Rows[i].FindControl("chkConcession");
            CheckBox chkPaidamount = (CheckBox)GridView2.Rows[i].FindControl("chkPaidamount");
            if (dt.Rows.Count > i)
            {
                string[] str = dt.Rows[i][0].ToString().Split(',');
                if (str[0].ToString() == "Yes")
                {
                    if (str.Length == 4)
                    {
                        chkDate.Checked = true;
                        chkConcession.Checked = true;
                        chkPaidamount.Checked = true;
                    }
                    else if (str.Length == 3)
                    {
                        if (str[1].ToString() == "1" && str[2].ToString() == "2")
                        {
                            chkDate.Checked = true;
                            chkConcession.Checked = true;
                            chkPaidamount.Checked = false;
                        }
                        else if (str[1].ToString() == "2" && str[2].ToString() == "3")
                        {
                            chkDate.Checked = false;
                            chkConcession.Checked = true;
                            chkPaidamount.Checked = true;
                        }
                        else if (str[1].ToString() == "1" && str[2].ToString() == "3")
                        {
                            chkDate.Checked = true;
                            chkConcession.Checked = false;
                            chkPaidamount.Checked = true;
                        }
                    }
                    else if (str.Length == 2)
                    {
                        if (str[1].ToString() == "1")
                        {
                            chkDate.Checked = true;
                            chkConcession.Checked = false;
                            chkPaidamount.Checked = false;
                        }
                        else if (str[1].ToString() == "2")
                        {
                            chkDate.Checked = false;
                            chkConcession.Checked = true;
                            chkPaidamount.Checked = false;
                        }
                        else if (str[1].ToString() == "3")
                        {
                            chkDate.Checked = false;
                            chkConcession.Checked = false;
                            chkPaidamount.Checked = true;
                        }
                    }
                }
                else
                {
                    chkDate.Checked = false;
                    chkConcession.Checked = false;
                    chkPaidamount.Checked = false;
                }
            }
        }
        Session["Permission_Session"] = s.ToString();
    }
    //protected void LinkButton9_Click(object sender, EventArgs e)
    //{
    //    Table1.Visible = true;
    //    Permission_tab1.Visible = false;
    //    LinkButton10.Visible = false;
       
    //    if (Grd0.Rows.Count > 0)
    //    {
    //        Grd0.Visible = true;
    //    }
    //    else
    //    {
    //        Grd0.Visible = false;
    //    }
    //}
    protected void LinkButton10_Click(object sender, EventArgs e)
    {
        loackunloackAll();
        Student_display();
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Student_display();
    }
    public void ChkAll1_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = (CheckBox)sender;
        if (chk.Checked)
        {
            foreach (GridViewRow gvr in GridView1.Rows)
            {
                CheckBox chkDate = (CheckBox)gvr.FindControl("chkDate");
                chkDate.Checked = true;
            }
        }
        else
        {
            foreach (GridViewRow gvr in GridView1.Rows)
            {
                CheckBox chkDate = (CheckBox)gvr.FindControl("chkDate");
                chkDate.Checked = false;
            }
        }
    }
    public void ChkAll2_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = (CheckBox)sender;
        if (chk.Checked)
        {
            foreach (GridViewRow gvr in GridView1.Rows)
            {
                CheckBox chkConcession = (CheckBox)gvr.FindControl("chkConcession");
                chkConcession.Checked = true;
            }
        }
        else
        {
            foreach (GridViewRow gvr in GridView1.Rows)
            {
                CheckBox chkConcession = (CheckBox)gvr.FindControl("chkConcession");
                chkConcession.Checked = false;
            }
        }
    }
    public void ChkAll3_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = (CheckBox)sender;
        if (chk.Checked)
        {
            foreach (GridViewRow gvr in GridView1.Rows)
            {
                CheckBox chkPaidamount = (CheckBox)gvr.FindControl("chkPaidamount");
                chkPaidamount.Checked = true;
            }
        }
        else
        {
            foreach (GridViewRow gvr in GridView1.Rows)
            {
                CheckBox chkPaidamount = (CheckBox)gvr.FindControl("chkPaidamount");
                chkPaidamount.Checked = false;
            }
        }
    }
    public void ChkAll4_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = (CheckBox)sender;
        if (chk.Checked)
        {
            foreach (GridViewRow gvr in GridView2.Rows)
            {
                CheckBox chkDate = (CheckBox)gvr.FindControl("chkDate");
                chkDate.Checked = true;
            }
        }
        else
        {
            foreach (GridViewRow gvr in GridView2.Rows)
            {
                CheckBox chkDate = (CheckBox)gvr.FindControl("chkDate");
                chkDate.Checked = false;
            }
        }
    }
    public void ChkAll5_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = (CheckBox)sender;
        if (chk.Checked)
        {
            foreach (GridViewRow gvr in GridView2.Rows)
            {
                CheckBox chkConcession = (CheckBox)gvr.FindControl("chkConcession");
                chkConcession.Checked = true;
            }
        }
        else
        {
            foreach (GridViewRow gvr in GridView2.Rows)
            {
                CheckBox chkConcession = (CheckBox)gvr.FindControl("chkConcession");
                chkConcession.Checked = false;
            }
        }
    }
    public void ChkAll6_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = (CheckBox)sender;
        if (chk.Checked)
        {
            foreach (GridViewRow gvr in GridView2.Rows)
            {
                CheckBox chkPaidamount = (CheckBox)gvr.FindControl("chkPaidamount");
                chkPaidamount.Checked = true;
            }
        }
        else
        {
            foreach (GridViewRow gvr in GridView2.Rows)
            {
                CheckBox chkPaidamount = (CheckBox)gvr.FindControl("chkPaidamount");
                chkPaidamount.Checked = false;
            }
        }
    }

    protected void TxtEnter_TextChanged(object sender, EventArgs e)
    {
        view();
    }
}