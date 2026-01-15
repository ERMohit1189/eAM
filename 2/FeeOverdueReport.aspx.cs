using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FeeOverdueReport : Page
{
    Campus oo = new Campus();
    string sql = "";
    private SqlConnection _con;
    protected void Page_PreInIt(object sender, EventArgs e)
    {
        if (Session["Logintype"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (Session["Logintype"].ToString() == "SuperAdmin")
        {
            MasterPageFile = "~/50/sadminRootManager.master";
        }
        if (Session["Logintype"].ToString() == "Staff")
        {
            this.MasterPageFile = "~/Staff/staff_root-manager.master";
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        _con = new SqlConnection();
        _con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);
        BLL.BLLInstance.LoadHeader("Report", header);
        if (!IsPostBack)
        {
            sql = "Select BranchId, BranchName from Branchtab";
            var dt = oo.Fetchdata(sql);
            ddlBranch.DataSource = dt;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "BranchId";
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, new ListItem("<--Select-->", ""));
            if (Session["LoginType"].ToString() == "Admin")
            {
                divBranch.Visible = false;
                divSession.Visible = false;
                ddlBranch.SelectedValue = Session["BranchCode"].ToString();

                string sqls = "select SessionName from SessionMaster where BranchCode=" + ddlBranch.SelectedValue + "";
                var dt2 = oo.Fetchdata(sqls);
                DrpSessionName.DataSource = dt2;
                DrpSessionName.DataTextField = "SessionName";
                DrpSessionName.DataValueField = "SessionName";
                DrpSessionName.DataBind();
                DrpSessionName.Items.Insert(0, new ListItem("<--Select Session-->", ""));
                DrpSessionName.SelectedIndex = (DrpSessionName.Items.Count - 1);
                if (Session["LoginType"].ToString() == "Admin")
                {
                    DrpSessionName.SelectedValue = Session["SessionName"].ToString();
                }

            }

            DrpSessionName.Items.Insert(0, new ListItem("<--Select-->", ""));

            sql = "Select id, ClassName from ClassMaster";
            sql += "  where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            sql += "  order by Id";
            oo.FillDropDown_withValue(sql, DrpClass, "ClassName", "id");
            DrpClass.Items.Insert(0, new ListItem("<--Select-->", ""));
            drpSection.Items.Insert(0, new ListItem("<--Select-->", ""));
            drpBranch.Items.Insert(0, new ListItem("<--Select-->", ""));

            drpInstallment.Items.Insert(0, new ListItem("<-- Select-->", ""));


            loadForStaff();


        }

    }
    protected void loadForStaff()
    {
        if (Session["LoginType"].ToString().ToLower() == "staff")
        {
            divClass.Visible = true;
            divSection.Visible = true;
            divStream.Visible = true;
            LinkButton2.Visible = true;


            ddlBranch.SelectedValue = Session["BranchCode"].ToString();
            string sqls = "select SessionName from SessionMaster where BranchCode=" + ddlBranch.SelectedValue + "";
            var dt2 = oo.Fetchdata(sqls);
            DrpSessionName.DataSource = dt2;
            DrpSessionName.DataTextField = "SessionName";
            DrpSessionName.DataValueField = "SessionName";
            DrpSessionName.DataBind();
            DrpSessionName.Items.Insert(0, new ListItem("<--Select Session-->", ""));
            DrpSessionName.SelectedValue = Session["SessionName"].ToString();
            divBranch.Visible = false;
            divSession.Visible = false;

            string sql = "Select id, ClassName from ClassMaster  where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            sql += " and id in (select ClassId from ClassTeacherMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and EmpCode='" + Session["LoginName"].ToString() + "' and IsClassTeacher=1) order by Id";
            oo.FillDropDown_withValue(sql, DrpClass, "ClassName", "id");
            DrpClass.Items.Insert(0, new ListItem("<--Select-->", ""));

            string sql1 = "select ClassId, SectionId, BranchId from ClassTeacherMaster where SessionName='" + DrpSessionName.SelectedValue + "' and BranchCode=" + ddlBranch.SelectedValue + " and EmpCode='" + Session["LoginName"].ToString() + "'";
            if (oo.Duplicate(sql1))
            {
                //DrpClass.SelectedValue = oo.ReturnTag(sql1, "ClassId");
                //sql = "select id, SectionName from SectionMaster ";
                //sql +=  "  where SessionName='" + DrpSessionName.SelectedValue + "' and BranchCode=" + ddlBranch.SelectedValue + " and ClassNameId=" + DrpClass.SelectedValue + "";
                //sql +=  "  order by Id";
                //drpSection.Items.Insert(0, new ListItem("<--Select-->", ""));
                //oo.FillDropDown_withValue(sql, drpSection, "SectionName", "id");


                //drpSection.SelectedValue = oo.ReturnTag(sql1, "SectionId");
                //sqls = "select id, BranchName from BranchMaster ";
                //sqls = sqls + "  where SessionName='" + DrpSessionName.SelectedValue + "' and BranchCode=" + ddlBranch.SelectedValue + " and ClassId=" + DrpClass.SelectedValue + "";
                //sqls = sqls + "  order by Id";
                //oo.FillDropDown_withValue(sqls, drpBranch, "BranchName", "id");
                //drpBranch.Items.Insert(0, new ListItem("<--Select-->", ""));
                //drpBranch.SelectedValue = oo.ReturnTag(sql1, "BranchId");
                //divClass.Visible = false;
                //divSection.Visible = false;
                //divStream.Visible = false;
                loadFeeCategory();
                ddlFeeCategory.SelectedIndex = (ddlFeeCategory.Items.Count - 1);
                loadFeeMonth();

            }
            else
            {
                DrpClass.SelectedValue = "";
                drpSection.SelectedValue = "";
                drpBranch.SelectedValue = "";
                msgbox.InnerText = "Please allot class teacher first!";
                //divClass.Visible = false;
                //divSection.Visible = false;
                //divStream.Visible = false;
                LinkButton2.Visible = false;
            }
        }
        else
        {
            loadFeeCategory();
        }
    }
    protected void ddlFeeCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Session["LoginType"].ToString().ToLower() == "staff")
        {
            loadFeeMonth();
        }
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlBranch.SelectedIndex == 0)
        {
            DrpSessionName.Items.Clear();
            DrpSessionName.Items.Insert(0, new ListItem("<--Select Session-->", ""));
            return;
        }
        string sql = "select SessionName from SessionMaster where BranchCode=" + ddlBranch.SelectedValue + "";
        var dt2 = oo.Fetchdata(sql);
        DrpSessionName.DataSource = dt2;
        DrpSessionName.DataTextField = "SessionName";
        DrpSessionName.DataValueField = "SessionName";
        DrpSessionName.DataBind();
        DrpSessionName.Items.Insert(0, new ListItem("<--Select Session-->", ""));
        DrpSessionName.SelectedIndex = (DrpSessionName.Items.Count - 1);
        if (Session["LoginType"].ToString() == "Admin")
        {
            DrpSessionName.SelectedValue = Session["SessionName"].ToString();
        }
        loadFeeCategory();
    }
    protected void loadFeeCategory()
    {
        string sql1 = "Select FeeGroupName,Id from FeeGroupMaster";
        sql1 = sql1 + "  where SessionName='" + DrpSessionName.SelectedValue.ToString() + "' and BranchCode=" + ddlBranch.SelectedValue + "";
        oo.FillDropDown_withValue(sql1, ddlFeeCategory, "FeeGroupName", "Id");
        ddlFeeCategory.Items.Insert(0, new ListItem("<-- Select-->", ""));

    }

    protected void DrpClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Session["LoginType"].ToString().ToLower() == "staff")
        {
            sql = "Select SectionName, id from SectionMaster where ClassNameId=" + DrpClass.SelectedValue;
            sql += " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and id in (select SectionId from ClassTeacherMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and EmpCode='" + Session["LoginName"].ToString() + "' and IsClassTeacher=1)";
            oo.FillDropDown_withValue(sql, drpSection, "SectionName", "id");
            drpSection.Items.Insert(0, new ListItem("<--Select-->", ""));

            sql = "Select BranchName,Id from BranchMaster where ClassId=" + DrpClass.SelectedValue;
            sql += " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and id in (select BranchId from ClassTeacherMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and EmpCode='" + Session["LoginName"].ToString() + "' and IsClassTeacher=1)";
            oo.FillDropDown_withValue(sql, drpBranch, "BranchName", "Id");
            drpBranch.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
        }
        else
        {
            string sql = "select id, SectionName from SectionMaster ";
            sql += "  where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and ClassNameId=" + DrpClass.SelectedValue + "";
            sql += "  order by Id";
            oo.FillDropDown_withValue(sql, drpSection, "SectionName", "id");
            drpSection.Items.Insert(0, new ListItem("<--Select-->", ""));

            string sqls = "select id, BranchName from BranchMaster ";
            sqls = sqls + "  where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and ClassId=" + DrpClass.SelectedValue + "";
            sqls = sqls + "  order by Id";
            oo.FillDropDown_withValue(sqls, drpBranch, "BranchName", "id");
            drpBranch.Items.Insert(0, new ListItem("<--Select-->", ""));
        }
        loadFeeMonth();
    }
    public void loadFeeMonth()
    {
        string sql = "select Monthid, MonthName from MonthMaster where (CardType='" + ddlFeeCategory.SelectedItem.ToString() + "' or CardType='" + ddlFeeCategory.SelectedValue.ToString() + "')";
        sql += " and ClassId='" + DrpClass.SelectedValue.ToString() + "' and MOD='I' and SessionName='" + DrpSessionName.SelectedValue.ToString() + "' and BranchCode=" + ddlBranch.SelectedValue.ToString() + " or monthid=0";
        sql += "  order by MonthId";
        oo.FillDropDown_withValue(sql, drpInstallment, "MonthName", "Monthid");
        drpInstallment.Items.Insert(0, new ListItem("<-- Select-->", ""));
    }

    protected void Category_SelectedIndexChanged(object sender, EventArgs e)
    {

        drpInstallment.Attributes.Add("class", "form-control-blue validatedrp");
        divInstallment.Visible = true;
        if (reportType.SelectedValue == "Fee")
        {
            divType.Visible = true;
        }
    }
    protected void reportType_SelectedIndexChanged(object sender, EventArgs e)
    {

        DrpClass.SelectedIndex = 0;
        drpSection.SelectedIndex = 0;
        drpBranch.SelectedIndex = 0;

        GridViewFeeInstallmentwiseReport.DataSource = null;
        GridViewFeeInstallmentwiseReport.DataBind();
        GridViewFeeInstallmentwiseReport.Visible = false;

        GridViewFeeOverdueArrear.DataSource = null;
        GridViewFeeOverdueArrear.DataBind();
        GridViewFeeOverdueArrear.Visible = false;
        GridViewOtherFeeOverDue.DataSource = null;
        GridViewOtherFeeOverDue.DataBind();
        GridViewOtherFeeOverDue.Visible = false;
        Panel2.Visible = false;
        abc.Visible = false;
        if (reportType.SelectedValue == "Fee")
        {
            divFeeCategory.Visible = true;
            ddlFeeCategory.Attributes.Add("class", "form-control-blue  validatedrp");
            divType.Visible = true;
            divInstallment.Visible = true;
            drpInstallment.Attributes.Add("class", "form-control-blue");
        }
        if (reportType.SelectedValue == "Arrear")
        {
            ddlFeeCategory.Attributes.Add("class", "form-control-blue");
            divFeeCategory.Visible = false;
            divType.Visible = true;
            drpInstallment.Attributes.Add("class", "form-control-blue");
            divInstallment.Visible = false;
        }
        if (reportType.SelectedValue == "Other Fee")
        {
            ddlFeeCategory.Attributes.Add("class", "form-control-blue");
            divFeeCategory.Visible = false;
            divType.Visible = false;
            drpInstallment.Attributes.Add("class", "form-control-blue");
            divInstallment.Visible = false;
        }
        loadForStaff();
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {

        if (reportType.SelectedValue == "Fee")
        {
            FeeOverDueReport();
            //LoadFeeInstallmentwiseReport();
        }
        if (reportType.SelectedValue == "Arrear")
        {
            LoadArrearReport();
        }
        if (reportType.SelectedValue == "Other Fee")
        {
            LoadOtherFeeReport();
        }

    }
    DataSet ds = new DataSet();
    DataColumnCollection column;
    protected void FeeOverDueReport()
    {
        ds = new DataSet();
        GridViewFeeInstallmentwiseReport.Columns.Clear();
        GridViewFeeInstallmentwiseReport.DataSource = null;
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = "sp_FeeOverdueInstallmentWise_New";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = _con;
            cmd.Parameters.AddWithValue("@FeeCategoryId", ddlFeeCategory.SelectedValue);
            cmd.Parameters.AddWithValue("@InstallmentIds", drpInstallment.SelectedValue);
            cmd.Parameters.AddWithValue("@ClassId", DrpClass.SelectedValue);
            if (drpSection.SelectedIndex != 0)
            {
                cmd.Parameters.AddWithValue("@SectionId", drpSection.SelectedValue);
            }
            if (drpBranch.SelectedIndex != 0)
            {
                cmd.Parameters.AddWithValue("@BranchId", drpBranch.SelectedValue);
            }
            cmd.Parameters.AddWithValue("@SessionName", DrpSessionName.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@BranchCode", ddlBranch.SelectedValue);
            if (ddlStatus.SelectedIndex != 0)
            {
                cmd.Parameters.AddWithValue("@Withdrwal", ddlStatus.SelectedValue);
            }
            cmd.Parameters.AddWithValue("@FeeGroup", drpFeeGroup.SelectedValue);
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                _con.Open();
                adapter.Fill(ds);

                column = ds.Tables[0].Columns;
                foreach (DataColumn col in ds.Tables[0].Columns)
                {
                    BoundField testField = new BoundField();
                    testField.DataField = col.ColumnName;
                    testField.HeaderText = col.ColumnName;

                    GridViewFeeInstallmentwiseReport.Columns.Add(testField);
                }

                GridViewFeeInstallmentwiseReport.DataSource = ds.Tables[0];
                GridViewFeeInstallmentwiseReport.DataBind();

                heading.Text = "Fee Overdue Report (Installment wise) of Class : " + DrpClass.SelectedItem.Text + " " + (drpBranch.SelectedIndex != 0 ? drpBranch.SelectedItem.Text : "") + " " + (drpSection.SelectedIndex != 0 ? "(" + drpSection.SelectedItem.Text + ")" : "") + "";
                string ss = "select getdate() dates";
                DateTime date = Convert.ToDateTime(oo.ReturnTag(ss, "dates"));
                lblRegister.Text = "Fee Category : " + ddlFeeCategory.SelectedItem.Text + " | Till Installment : " + drpInstallment.SelectedItem.Text + " | Date : " + date.ToString("dd-MMM-yyyy hh:mm:ss tt") + (ddlStatus.SelectedIndex == 0 ? "" : " | Status : " + ddlStatus.SelectedItem.Text);

                if (GridViewFeeInstallmentwiseReport.Rows.Count == 0)
                {
                    GridViewFeeInstallmentwiseReport.Visible = false;
                    Panel2.Visible = false;
                    abc.Visible = false;
                }
                else
                {
                    GridViewFeeInstallmentwiseReport.Visible = true;
                    Panel2.Visible = true;
                    abc.Visible = true;
                }
                //abc.Visible = true;
                //divExport.Visible = true;
            }
            catch (Exception ex)
            {
            }
        }
    }
    protected void LoadFeeInstallmentwiseReport()
    {
        GridViewFeeInstallmentwiseReport.DataSource = null;
        GridViewFeeInstallmentwiseReport.DataBind();
        GridViewFeeInstallmentwiseReport.Visible = true;

        GridViewFeeOverdueArrear.DataSource = null;
        GridViewFeeOverdueArrear.DataBind();
        GridViewFeeOverdueArrear.Visible = false;
        GridViewOtherFeeOverDue.DataSource = null;
        GridViewOtherFeeOverDue.DataBind();
        GridViewOtherFeeOverDue.Visible = false;

        GridViewFeeInstallmentwiseReport.DataSource = null;
        GridViewFeeInstallmentwiseReport.DataBind();

        GridViewFeeInstallmentwiseReport.Columns.Clear();
        GridViewFeeInstallmentwiseReport.DataBind();

        string qry = "select SrNo, name, FatherName, FamilyContactNo FatherContactNo from AllStudentRecord_UDF('" + DrpSessionName.SelectedItem.Text + "', " + ddlBranch.SelectedValue + ") where ClassId=" + DrpClass.SelectedValue + "";
        if (drpBranch.SelectedIndex != 0)
        {
            qry = qry + " and BranchId=" + drpBranch.SelectedValue + "";
        }
        if (drpSection.SelectedIndex != 0)
        {
            qry = qry + " and SectionId=" + drpSection.SelectedValue + "";
        }
        if (ddlStatus.SelectedIndex != 0)
        {
            if (ddlStatus.SelectedIndex == 1)
            {
                qry = qry + " and isnull(Withdrwal,'')= ''";
            }
            else
            {
                qry = qry + " and isnull(Withdrwal,'')= 'W'";
            }
        }
        qry = qry + " and CardId=" + ddlFeeCategory.SelectedValue + " order by name asc";
        DataTable dts = oo.Fetchdata(qry);
        DataSet dss = new DataSet();
        DataTable table = new DataTable();
        DataTable dt2 = new DataTable();


        GridViewFeeInstallmentwiseReport.Columns.Clear();

        BoundField sr = new BoundField();
        sr.DataField = "sr";
        sr.HeaderText = "#";
        GridViewFeeInstallmentwiseReport.Columns.Add(sr);
        GridViewFeeInstallmentwiseReport.DataBind();
        BoundField test = new BoundField();
        test.DataField = "srno";
        test.HeaderText = "S. R. No.";
        GridViewFeeInstallmentwiseReport.Columns.Add(test);
        GridViewFeeInstallmentwiseReport.DataBind();
        BoundField test1 = new BoundField();
        test1.DataField = "name";
        test1.HeaderText = "Student's Name";
        GridViewFeeInstallmentwiseReport.Columns.Add(test1);
        GridViewFeeInstallmentwiseReport.DataBind();
        BoundField test2 = new BoundField();
        test2.DataField = "FatherName";
        test2.HeaderText = "Father's Name.";
        GridViewFeeInstallmentwiseReport.Columns.Add(test2);
        GridViewFeeInstallmentwiseReport.DataBind();
        BoundField test3 = new BoundField();
        test3.DataField = "FatherContactNo";
        test3.HeaderText = "Contact No.";
        GridViewFeeInstallmentwiseReport.Columns.Add(test3);
        BoundField test4 = new BoundField();
        test4.DataField = "Arrear";
        test4.HeaderText = "Arrear";
        GridViewFeeInstallmentwiseReport.Columns.Add(test4);
        GridViewFeeInstallmentwiseReport.DataBind();


        table.Columns.Add("sr", typeof(string));
        table.Columns.Add("srno", typeof(string));
        table.Columns.Add("name", typeof(string));
        table.Columns.Add("FatherName", typeof(string));
        table.Columns.Add("FatherContactNo", typeof(string));
        table.Columns.Add("Arrear", typeof(string));
        string sql2 = "select MonthName from MonthMaster where SessionName='" + DrpSessionName.SelectedItem.Text + "' and BranchCode=" + ddlBranch.SelectedValue + " and ClassId=" + DrpClass.SelectedValue + " and CardType='" + ddlFeeCategory.SelectedValue + "' and monthid<=" + drpInstallment.SelectedValue + " order by monthid asc";
        dt2 = oo.Fetchdata(sql2);
        for (int k = 0; k < dt2.Rows.Count; k++)
        {
            BoundField tests = new BoundField();
            tests.DataField = dt2.Rows[k]["MonthName"].ToString();
            tests.HeaderText = dt2.Rows[k]["MonthName"].ToString();
            GridViewFeeInstallmentwiseReport.Columns.Add(tests);
            table.Columns.Add(dt2.Rows[k]["MonthName"].ToString(), typeof(string));
        }
        table.Columns.Add("Total", typeof(string));
        BoundField Total = new BoundField();
        Total.DataField = "Total";
        Total.HeaderText = "Total";
        GridViewFeeInstallmentwiseReport.Columns.Add(Total);
        int srno = 0;
        for (int i = 0; i < dts.Rows.Count; i++)
        {


            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@FeeCategoryId", ddlFeeCategory.SelectedValue));
            param.Add(new SqlParameter("@InstallmentIds", drpInstallment.SelectedValue));
            param.Add(new SqlParameter("@ClassId", DrpClass.SelectedValue));
            if (drpSection.SelectedIndex != 0)
            {
                param.Add(new SqlParameter("@SectionId", drpSection.SelectedValue));
            }
            if (drpBranch.SelectedIndex != 0)
            {
                param.Add(new SqlParameter("@BranchId", drpBranch.SelectedValue));
            }
            param.Add(new SqlParameter("@SessionName", DrpSessionName.SelectedItem.Text));
            param.Add(new SqlParameter("@BranchCode", ddlBranch.SelectedValue));
            param.Add(new SqlParameter("@SrNoss", dts.Rows[i]["SrNo"].ToString()));
            if (ddlStatus.SelectedIndex != 0)
            {
                param.Add(new SqlParameter("@Withdrwal", ddlStatus.SelectedValue));
            }
            heading.Text = "Fee Overdue Report (Installment wise) of Class : " + DrpClass.SelectedItem.Text + " " + (drpBranch.SelectedIndex != 0 ? drpBranch.SelectedItem.Text : "") + " " + (drpSection.SelectedIndex != 0 ? "(" + drpSection.SelectedItem.Text + ")" : "") + "";
            dss = new DLL().Sp_SelectRecord_usingExecuteDataset("sp_FeeOverdueInstallmentWise", param);

            if (dss != null && dss.Tables[0].Rows.Count > 0)
            {
                double total = 0;
                for (int j = 0; j < dss.Tables[0].Rows.Count; j++)
                {
                    total = total + double.Parse(dss.Tables[0].Rows[j]["BalanceAmount"].ToString());
                }
                if (drpFeeGroup.SelectedValue == "Defaulter")
                {
                    if (total > 0)
                    {
                        srno = srno + 1;
                        DataRow dr;
                        dr = table.NewRow();
                        dr["sr"] = (srno).ToString();
                        dr["srno"] = dss.Tables[0].Rows[0]["SrNo"].ToString();
                        dr["name"] = dss.Tables[0].Rows[0]["name"].ToString();
                        dr["FatherName"] = dss.Tables[0].Rows[0]["FatherName"].ToString();
                        dr["FatherContactNo"] = dss.Tables[0].Rows[0]["FatherContactNo"].ToString();
                        for (int j = 0; j < dss.Tables[0].Rows.Count; j++)
                        {
                            dr[j + 5] = dss.Tables[0].Rows[j]["BalanceAmount"].ToString();
                        }
                        dr["Total"] = total;
                        table.Rows.Add(dr);
                    }
                }
                if (drpFeeGroup.SelectedValue == "All")
                {
                    srno = srno + 1;
                    DataRow dr;
                    dr = table.NewRow();
                    dr["sr"] = (srno).ToString();
                    dr["srno"] = dss.Tables[0].Rows[0]["SrNo"].ToString();
                    dr["name"] = dss.Tables[0].Rows[0]["name"].ToString();
                    dr["FatherName"] = dss.Tables[0].Rows[0]["FatherName"].ToString();
                    dr["FatherContactNo"] = dss.Tables[0].Rows[0]["FatherContactNo"].ToString();
                    for (int j = 0; j < dss.Tables[0].Rows.Count; j++)
                    {
                        dr[j + 5] = dss.Tables[0].Rows[j]["BalanceAmount"].ToString();
                    }
                    dr["Total"] = total;
                    table.Rows.Add(dr);
                }
            }
        }
        DataRow drs;
        drs = table.NewRow();
        drs["sr"] = "";
        drs["srno"] = "";
        drs["name"] = "";
        drs["FatherName"] = "";
        drs["FatherContactNo"] = "Total";
        for (int i = 5; i < table.Columns.Count; i++)
        {
            double totals = 0;
            for (int f = 0; f < table.Rows.Count; f++)
            {
                totals = totals + double.Parse(table.Rows[f][table.Columns[i]].ToString() == "" ? "0" : table.Rows[f][table.Columns[i]].ToString());
            }
            drs[table.Columns[i]] = totals.ToString("0.00");
        }
        table.Rows.Add(drs);

        GridViewFeeInstallmentwiseReport.DataSource = table;
        GridViewFeeInstallmentwiseReport.DataBind();

        string ss = "select format(getdate(), 'dd MMM yyyy') dates";
        lblRegister.Text = "Fee Category : " + ddlFeeCategory.SelectedItem.Text + " | Till Installment : " + drpInstallment.SelectedItem.Text + " | Date : " + oo.ReturnTag(ss, "dates") + (ddlStatus.SelectedIndex == 0 ? "" : " | Status : " + ddlStatus.SelectedItem.Text);
        if (GridViewFeeInstallmentwiseReport.Rows.Count == 0)
        {
            Panel2.Visible = false;
            abc.Visible = false;
        }
        else
        {
            Panel2.Visible = true;
            abc.Visible = true;
        }
    }
    protected void LoadArrearReport()
    {
        GridViewFeeInstallmentwiseReport.DataSource = null;
        GridViewFeeInstallmentwiseReport.DataBind();
        GridViewFeeInstallmentwiseReport.Visible = false;

        GridViewFeeOverdueArrear.DataSource = null;
        GridViewFeeOverdueArrear.DataBind();
        GridViewFeeOverdueArrear.Visible = true;
        GridViewOtherFeeOverDue.DataSource = null;
        GridViewOtherFeeOverDue.DataBind();
        GridViewOtherFeeOverDue.Visible = false;
        List<SqlParameter> param = new List<SqlParameter>();
        param.Add(new SqlParameter("@ClassId", DrpClass.SelectedValue));

        if (drpSection.SelectedIndex != 0)
        {
            param.Add(new SqlParameter("@SectionId", drpSection.SelectedValue));
        }
        if (drpBranch.SelectedIndex != 0)
        {
            param.Add(new SqlParameter("@BranchId", drpBranch.SelectedValue));
        }
        if (drpFeeGroup.SelectedIndex != 0)
        {
            param.Add(new SqlParameter("@Type", drpFeeGroup.SelectedValue));
        }
        if (ddlStatus.SelectedIndex != 0)
        {
            param.Add(new SqlParameter("@Withdrwal", ddlStatus.SelectedValue));
        }
        param.Add(new SqlParameter("@SessionName", DrpSessionName.SelectedItem.Text));
        param.Add(new SqlParameter("@BranchCode", ddlBranch.SelectedValue));
        heading.Text = "Arrear Report of Session : " + DrpSessionName.SelectedItem.Text;
        DataSet ds = new DLL().Sp_SelectRecord_usingExecuteDataset("[GetDescriptiveArrear]", param);
        DataTable dt;
        if (ds != null)
        {
            string ss = "select format(getdate(), 'dd MMM yyyy') dates";
            lblRegister.Text = "Date : " + oo.ReturnTag(ss, "dates") + (ddlStatus.SelectedIndex == 0 ? "" : " | Status : " + ddlStatus.SelectedItem.Text);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                Panel2.Visible = true;
                abc.Visible = true;
                dt = ds.Tables[0];

                GridViewFeeOverdueArrear.DataSource = dt;
                GridViewFeeOverdueArrear.DataBind();
                double BalanceAmount = 0;
                for (int i = 0; i < GridViewFeeOverdueArrear.Rows.Count; i++)
                {
                    Label BalanceAmountl = (Label)GridViewFeeOverdueArrear.Rows[i].FindControl("BalanceAmount");
                    BalanceAmount = BalanceAmount + double.Parse(BalanceAmountl.Text);
                }
                Label ClassBalanceAmount = (Label)GridViewFeeOverdueArrear.FooterRow.FindControl("ClassBalanceAmount");
                ClassBalanceAmount.Text = BalanceAmount.ToString("0.00");
            }
            else
            {
                Panel2.Visible = false;
                abc.Visible = false;
            }
        }
        else
        {
            Panel2.Visible = false;
            abc.Visible = false;
        }
    }
    protected void LoadOtherFeeReport()
    {
        GridViewFeeInstallmentwiseReport.DataSource = null;
        GridViewFeeInstallmentwiseReport.DataBind();
        GridViewFeeInstallmentwiseReport.Visible = false;

        GridViewFeeOverdueArrear.DataSource = null;
        GridViewFeeOverdueArrear.DataBind();
        GridViewFeeOverdueArrear.Visible = false;
        GridViewOtherFeeOverDue.DataSource = null;
        GridViewOtherFeeOverDue.DataBind();
        GridViewOtherFeeOverDue.Visible = true;
        List<SqlParameter> param = new List<SqlParameter>();
        param.Add(new SqlParameter("@ClassId", DrpClass.SelectedValue));
        if (drpSection.SelectedIndex != 0)
        {
            param.Add(new SqlParameter("@SectionId", drpSection.SelectedValue));
        }
        if (drpBranch.SelectedIndex != 0)
        {
            param.Add(new SqlParameter("@BranchId", drpBranch.SelectedValue));
        }
        if (ddlStatus.SelectedIndex != 0)
        {
            param.Add(new SqlParameter("@Withdrwal", ddlStatus.SelectedValue));
        }
        param.Add(new SqlParameter("@SessionName", DrpSessionName.SelectedItem.Text));
        param.Add(new SqlParameter("@BranchCode", ddlBranch.SelectedValue));
        heading.Text = "Overdue Report of Other Fee";
        DataSet ds = new DLL().Sp_SelectRecord_usingExecuteDataset("sp_OtherFeeOverDue", param);
        DataTable dt;
        if (ds != null)
        {
            string ss = "select format(getdate(), 'dd MMM yyyy') dates";
            lblRegister.Text = "Date : " + oo.ReturnTag(ss, "dates") + (ddlStatus.SelectedIndex == 0 ? "" : " | Status : " + ddlStatus.SelectedItem.Text);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 1)
            {
                Panel2.Visible = true;
                abc.Visible = true;
                dt = ds.Tables[0];
                GridViewOtherFeeOverDue.DataSource = dt;
                GridViewOtherFeeOverDue.DataBind();
                double gtotal = 0;
                if (GridViewOtherFeeOverDue.Rows.Count > 0)
                {
                    var ftotal = (Label)GridViewOtherFeeOverDue.FooterRow.FindControl("totalM");
                    for (int i = 0; i < GridViewOtherFeeOverDue.Rows.Count; i++)
                    {
                        var mtotal = (Label)GridViewOtherFeeOverDue.Rows[i].FindControl("Amount");
                        gtotal = gtotal + double.Parse(mtotal.Text);
                    }
                    ftotal.Text = gtotal.ToString("0.00");
                }
            }
            else
            {
                Panel2.Visible = false;
                abc.Visible = false;
            }
        }
        else
        {
            Panel2.Visible = false;
            abc.Visible = false;
        }
    }

    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        oo.ExportTolandscapeWord(Response, "FeeoverdueReport", gdv1);
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        oo.ExportDivToExcelWithFormatting(Response, "FeeoverdueReport.xls", gdv1, Server.MapPath("~/Admin/css/style.css"));
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {
        oo.ExporttolandscapePdf(Response, "FeeoverdueReport", abc);
    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = abc;
        ClientScript.RegisterStartupScript(GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }
    protected void GridViewFeeInstallmentwiseReport_RowDataBound(object sender, GridViewRowEventArgs ge)
    {
        if (ge.Row.RowType == DataControlRowType.Footer)
        {
            ge.Row.Cells[4].Text = "Total";
            ge.Row.Cells[4].Font.Bold = true;
            ge.Row.Cells[4].Font.Size = 11;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                for (int j = 5; j < column.Count; j++)
                {
                    decimal amount;
                    decimal.TryParse(ge.Row.Cells[j].Text, out amount);

                    decimal rowAmount;
                    decimal.TryParse(ds.Tables[0].Rows[i][j].ToString(), out rowAmount);

                    ge.Row.Cells[j].Text = (amount + rowAmount).ToString();
                    ge.Row.Cells[j].Font.Bold = true;
                    ge.Row.Cells[j].Font.Size = 11;
                }
            }
        }
    }
}