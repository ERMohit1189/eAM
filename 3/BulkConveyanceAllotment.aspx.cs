using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;


public partial class _3_BulkConveyanceAllotment : System.Web.UI.Page
{
    private SqlConnection _con = new SqlConnection();
    public SqlConnection _con1 = new SqlConnection();
    private readonly Campus _oo = new Campus();
    private string _sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        _con = _oo.dbGet_connection();
        _con1 = _oo.dbGet_connection();

        Campus camp = new Campus(); camp.LoadLoader(loader);
        if (IsPostBack) return;
        lblRedIndicate.Visible = false;
        var oldsessionname = Session["SessionName"].ToString();
      //  drpSession.Items.Insert(0, oldsessionname);
       //  drpSession.Focus();
        var yy1 = Convert.ToInt32(Session["SessionName"].ToString().Substring(0, 4));
        yy1++;

        var yy2 = Convert.ToInt32(Session["SessionName"].ToString().Substring(5, 4));
        yy2++;
        var Sess = yy1.ToString() + "-" + yy2.ToString();

        drpSessionNew.Items.Add(Sess);
      //  drpSession.Items.Add(Sess);
      //  drpSession.Focus();

        LoadFeeGroup();

        LoadClass();
        LoadBranch();
        LoadSection();

       

        Panel2.Visible = false;

        LinkSubmit.Visible = false;
    }

    protected void Loadrecord()
    {
        try
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                var yy1 = Convert.ToInt32(Session["SessionName"].ToString().Substring(0, 4));
                yy1--;
                var yy2 = Convert.ToInt32(Session["SessionName"].ToString().Substring(5, 4));
                yy2--;
                var Sess = yy1.ToString() + "-" + yy2.ToString();
                string sql = "select * from AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "'," + Session["BranchCode"] + ") where ClassId=" + DrpClass.SelectedValue + "   and SectionId=" + drpSection.SelectedValue + " and SrNo in(Select SrNo from StudentVehicleAllotment where SessionName='"+ Sess + "' and BranchCode=" + Session["BranchCode"] + ") and SrNo not in(Select SrNo from StudentVehicleAllotment where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"] + ")";
                //if (drpStream.SelectedValue != "")
                //{
                //    sql += " and StreamId=" + drpStream.SelectedValue + " ";
                //}
                //if (drpGender.SelectedValue != "-1")
                //{
                //    sql += " and Gender='" + drpGender.SelectedValue + "' ";
                //}
                //sql += "  and isnull(Promotion,'')='' and isnull(Withdrwal,'')='' ";
                if (RadioButtonList2.SelectedIndex == 0)
                {
                    sql += " order by Name asc ";
                }
                if (RadioButtonList2.SelectedIndex == 1)
                {
                    sql += " order by id asc ";
                }
                if (RadioButtonList2.SelectedIndex == 2)
                {
                    sql += " order by isnull(InstituteRollNo,0) asc ";
                }
                mainGrid.DataSource = _oo.Fetchdata(sql);
                mainGrid.DataBind();

                if (mainGrid.Items.Count > 0)
                {
                    div_grid.Visible = true;
                    LinkSubmit.Visible = true;
                    LinkButton1.Visible = true;
                    Panel2.Visible = true;
                    lblNoRecord.Text = "";
                    lblNoRecord.Visible = false;
                    double FTotal = 0;
                    //for (int i = 0; i < mainGrid.Items.Count; i++)
                    //{
                    //    Repeater childGrid = (Repeater)mainGrid.Items[i].FindControl("childGrid");
                    //    var lblSrNO = (Label)mainGrid.Items[i].FindControl("lblSrNO");

                    //    using (SqlCommand cmd2 = new SqlCommand())
                    //    {
                    //        cmd2.Connection = _con;
                    //        cmd2.CommandText = "GetArrearForPromotion";
                    //        cmd2.CommandType = CommandType.StoredProcedure;
                    //        cmd2.Parameters.AddWithValue("@Srno", lblSrNO.Text);
                    //        cmd2.Parameters.AddWithValue("@SessionName", drpSession.SelectedValue.ToString().Trim());
                    //        cmd2.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString().Trim());

                    //        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                    //        DataSet dt2 = new DataSet();
                    //        da2.Fill(dt2);
                    //        cmd.Parameters.Clear();
                    //        childGrid.DataSource = dt2.Tables[1];
                    //        childGrid.DataBind();
                    //        var TotalArrear = (Label)mainGrid.Items[i].FindControl("lblTotalArrear");
                    //        TotalArrear.Text = dt2.Tables[0].Rows[0]["Balance"].ToString();
                    //        FTotal += double.Parse(dt2.Tables[0].Rows[0]["Balance"].ToString() == "" ? "0" : dt2.Tables[0].Rows[0]["Balance"].ToString());
                    //    }
                    //    string dd = "select count(*) counts from CompositFeeDeposit where receiptStatus='Pending' and ModeOfPayment='Cheque' and SrNo='" + lblSrNO.Text + "' and SessionName='" + Session["SessionName"] + "' and InstrumentStatus = 'Pending' and BranchCode = " + Session["BranchCode"] + " and InstallmentId<> 0";
                    //    if (_oo.ReturnTag(dd, "counts").ToString() != "0")
                    //    {
                    //        var drpstatus = (DropDownList)mainGrid.Items[i].FindControl("drpstatus");
                    //        drpstatus.Items.Insert(2, new ListItem("Cheque Pending", "Pending"));
                    //        drpstatus.SelectedIndex = 2;
                    //        drpstatus.Enabled = false;
                    //        drpstatus.ForeColor = System.Drawing.Color.Red;
                    //    }
                    //}
                    //lblFTotal.Text = FTotal.ToString("0.00");
                    LoadNewSessionCourse();
                }
                else
                {
                    LinkButton1.Visible = false;
                    div_grid.Visible = false;
                    LinkSubmit.Visible = false;
                    Panel2.Visible = false;
                    lblNoRecord.Visible = true;
                    lblNoRecord.Text = "No record(s) found!";
                }
            }
        }
        catch (Exception)
        {

        }
    }

    protected void LinkShow_Click(object sender, EventArgs e)
    {
        Loadrecord();
    }
    private void LoadFeeGroup()
    {
        _sql = "Select FeeGroupName,Id from FeeGroupMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        _oo.FillDropDown_withValue_withSelect(_sql, DrpFeeGroup, "FeeGroupName", "Id");
    }
    private void LoadClass()
    {
        DrpClass.Items.Clear(); drpSection.Items.Clear(); drpStream.Items.Clear();
        _sql = "Select ClassName,Id from ClassMaster Where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"] + " order by CIDOrder asc ";

        _oo.FillDropDown_withValue(_sql, DrpClass, "ClassName", "Id");
        DrpClass.Items.Insert(0, new ListItem("<--Select-->", ""));
        DropBranch.Items.Insert(0, new ListItem("<--Select-->", ""));
        drpSection.Items.Insert(0, new ListItem("<--Select-->", ""));
        drpStream.Items.Insert(0, new ListItem("<--Select-->", ""));
    }
    private void LoadBranch()
    {
        DropBranch.Items.Clear();
        _sql = "Select BranchName,Id from BranchMaster";
        _sql += " where (ClassId='" + DrpClass.SelectedValue + "' or ClassId is NULL) and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";

        _oo.FillDropDown_withValue(_sql, DropBranch, "BranchName", "Id");
        DropBranch.Items.Insert(0, new ListItem("<--Select-->", ""));
    }
    private void LoadSection()
    {
        drpSection.Items.Clear();
        _sql = "select SectionName, id from SectionMaster where ClassNameId='" + DrpClass.SelectedValue + "'";
        _sql += "  and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        _oo.FillDropDown_withValue(_sql, drpSection, "SectionName", "id");
        drpSection.Items.Insert(0, new ListItem("<--Select-->", ""));
    }
    private void LoadStream()
    {
        drpStream.Items.Clear();
        _sql = "select id, Stream from StreamMaster where ClassId='" + DrpClass.SelectedValue + "' and BranchId='" + DropBranch.SelectedValue + "'";
        _sql += "  and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        _oo.FillDropDown_withValue(_sql, drpStream, "Stream", "id");
        drpStream.Items.Insert(0, new ListItem("<--Select-->", ""));
    }
    protected void DrpClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadBranch();
        LoadSection();


    }
    protected void DropBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadStream();
    }


    private void LoadNewSessionCourse()
    {
        _sql = "Select CourseName,Id from CourseMaster where SessionName='" + drpSessionNew.SelectedItem.Text.ToString() + "' and BranchCode=" + Session["BranchCode"] + "";
        _oo.FillDropDown_withValue(_sql, DropCourseNew, "CourseName", "Id");
        DropCourseNew.Items.Insert(0, new ListItem("<--Select-->", ""));

    }
    private void LoadNewSessionClass()
    {
        _sql = "Select Id,ClassName from ClassMaster";
        _sql += " where (Course='" + DropCourseNew.SelectedValue + "' or Course is NULL) and SessionName='" + drpSessionNew.SelectedItem.Text + "' and BranchCode=" + Session["BranchCode"] + " and CIDOrder !=0 ";

        _oo.FillDropDown_withValue(_sql, DrpClassNew, "ClassName", "Id");
        DrpClassNew.Items.Insert(0, new ListItem("<--Select-->", ""));
    }
    private void LoadNewSessionBranch()
    {
        _sql = "Select BranchName,Id from BranchMaster";
        _sql += " where (ClassId='" + DrpClassNew.SelectedValue + "' or ClassId is NULL) and SessionName='" + drpSessionNew.SelectedItem.Text + "' and BranchCode=" + Session["BranchCode"] + "";

        _oo.FillDropDown_withValue(_sql, DrpBranchNew, "BranchName", "Id");
        DrpBranchNew.Items.Insert(0, new ListItem("<--Select-->", ""));
    }
    private void LoadNewSessionSection()
    {
        _sql = "select SectionName,Id from SectionMaster where ClassNameId='" + DrpClassNew.SelectedValue + "'";
        _sql += "  and SessionName='" + drpSessionNew.SelectedItem.Text + "' and BranchCode=" + Session["BranchCode"] + "";
        _oo.FillDropDown_withValue(_sql, drpSectionNew, "SectionName", "Id");
        drpSectionNew.Items.Insert(0, new ListItem("<--Select-->", ""));
    }
    protected void DropCourseNew_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadNewSessionClass();
    }
    protected void DrpClassNew_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadNewSessionBranch();
        LoadNewSessionSection();
    }
    protected void DrpBranchNew_SelectedIndexChanged(object sender, EventArgs e)
    {
        _sql = "select * from MediumMaster where SessionName = '" + drpSessionNew.SelectedItem.Text + "' and BranchCode=" + Session["BranchCode"] + "";
        _oo.FillDropDown_withValue(_sql, DropMediumNew, "Medium", "Id");
        DropMediumNew.Items.Insert(0, new ListItem("<--Select-->", ""));

        _sql = "select * from StreamMaster where SessionName = '" + drpSessionNew.SelectedItem.Text + "' and BranchId = " + DrpBranchNew.SelectedValue + " and ClassId = " + DrpClassNew.SelectedValue + " and BranchCode=" + Session["BranchCode"] + "";
        _oo.FillDropDown_withValue(_sql, DropStreamNew, "Stream", "Id");
        DropStreamNew.Items.Insert(0, new ListItem("<--Select-->", ""));

        _sql = "select * from BoardMaster where BranchCode=" + Session["BranchCode"] + "";
        _oo.FillDropDown_withValue(_sql, DropBoardNew, "BoardName", "Id");
        DropBoardNew.Items.Insert(0, new ListItem("<--Select-->", ""));

        _sql = "select * from HouseMaster where SessionName = '" + drpSessionNew.SelectedItem.Text + "' and BranchCode=" + Session["BranchCode"] + "";
        _oo.FillDropDown_withValue(_sql, DropHouseNew, "HouseName", "Id");
        DropHouseNew.Items.Insert(0, new ListItem("<--Select-->", ""));

        _sql = "select * from FeeGroupMaster where SessionName = '" + drpSessionNew.SelectedItem.Text + "' and BranchCode=" + Session["BranchCode"] + "";
        _oo.FillDropDown_withValue(_sql, DropFeeGroup, "FeeGroupName", "Id");
        DropFeeGroup.Items.Insert(0, new ListItem("<--Select-->", ""));
    }
    protected void LinkSubmit_Click(object sender, EventArgs e)
    {
        int sts = 0;
        for (int m = 0; m < mainGrid.Items.Count; m++)
        {
            DropDownList drpstatus = (DropDownList)mainGrid.Items[m].FindControl("drpstatus");
            if (drpstatus.SelectedValue == "Pass")
            {
                sts++;
            }
        }
        if (sts == 0)
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox1, "Please Select Condition for Pass!", "A");
            return;
        }
        else
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                for (int i = 0; i < mainGrid.Items.Count; i++)
                {
                    Label LblSrNO = (Label)mainGrid.Items[i].FindControl("lblSrNO");
                    DropDownList drpstatus = (DropDownList)mainGrid.Items[i].FindControl("drpstatus");
                    Repeater childGrid = (Repeater)mainGrid.Items[i].FindControl("childGrid");

                    if (drpstatus.SelectedValue == "Pass")
                    {

                        for (int j = 0; j < childGrid.Items.Count; j++)
                        {
                            Label lblHeadId = (Label)childGrid.Items[j].FindControl("lblHeadId");
                            Label lblBalance = (Label)childGrid.Items[j].FindControl("lblBalance");

                            _sql = "select isnull(max(id), 0) as id from StudentOfficialDetails";
                            var co = Convert.ToInt32(_oo.ReturnTag(_sql, "id"));
                            co++;
                            var stEnRCode = IdGeneration(co.ToString());

                            cmd.CommandText = "SetArrear";
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Connection = _con;
                            cmd.Parameters.AddWithValue("@SrNO", LblSrNO.Text);
                            cmd.Parameters.AddWithValue("@stEnRCode", stEnRCode.ToString());
                            cmd.Parameters.AddWithValue("@FeeHaedId", lblHeadId.Text);
                            cmd.Parameters.AddWithValue("@Amount", lblBalance.Text);
                            cmd.Parameters.AddWithValue("@OldClassId", DrpClass.SelectedValue);
                            cmd.Parameters.AddWithValue("@OldBranchId", DropBranch.SelectedValue);
                            cmd.Parameters.AddWithValue("@OldSectionId", drpSection.SelectedValue);
                            cmd.Parameters.AddWithValue("@OldSectionName", drpSection.SelectedItem.Text);
                            if (drpStream.SelectedValue != "")
                            {
                                cmd.Parameters.AddWithValue("@OldStreamId", drpStream.SelectedValue);
                            }
                            cmd.Parameters.AddWithValue("@OldSessionName", Session["SessionName"].ToString());
                            cmd.Parameters.AddWithValue("@OldBranchCode", Session["BranchCode"].ToString());
                            cmd.Parameters.AddWithValue("@Promotion", drpstatus.SelectedValue);
                            cmd.Parameters.AddWithValue("@NewCourseId", DropCourseNew.SelectedValue);
                            cmd.Parameters.AddWithValue("@NewClassId", DrpClassNew.SelectedValue);
                            cmd.Parameters.AddWithValue("@NewBranchId", DrpBranchNew.SelectedValue);
                            cmd.Parameters.AddWithValue("@NewSectionId", drpSectionNew.SelectedValue);
                            if (DropStreamNew.SelectedValue != "")
                            {
                                cmd.Parameters.AddWithValue("@NewStreamId", DropStreamNew.SelectedValue);
                            }
                            cmd.Parameters.AddWithValue("@Medium", DropMediumNew.SelectedItem.Text);
                            cmd.Parameters.AddWithValue("@NewBoardName", DropBoardNew.SelectedItem.Text);
                            cmd.Parameters.AddWithValue("@NewHouseName", DropHouseNew.SelectedItem.Text);
                            cmd.Parameters.AddWithValue("@NewFeeGroupId", DropFeeGroup.SelectedValue);
                            cmd.Parameters.AddWithValue("@NewFeeGroupName", DropFeeGroup.SelectedItem.Text);
                            cmd.Parameters.AddWithValue("@NewSessionName", drpSessionNew.SelectedValue);
                            cmd.Parameters.AddWithValue("@NewBranchCode", Session["BranchCode"].ToString());
                            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                            try
                            {
                                _con.Open();
                                cmd.ExecuteNonQuery();
                                cmd.Parameters.Clear();
                                _con.Close();
                            }
                            catch (Exception) { }
                        }
                    }
                }
                Loadrecord();
                Campus camp = new Campus(); camp.msgbox(Page, msgbox1, "The conveyance data has been successfully imported from the last session.", "S");
            }
        }
    }
    public string IdGeneration(string x)
    {
        string xx = "";
        if (x.Length == 1)
        {
            xx = "eAM-00000" + x;

        }
        else if (x.Length == 2)
        {
            xx = "eAM-0000" + x;
        }
        else if (x.Length == 3)
        {
            xx = "eAM-000" + x;

        }
        else if (x.Length == 4)
        {
            xx = "eAM-00" + x;
        }
        else if (x.Length == 5)
        {
            xx = xx + "eAM-0" + x;
        }
        else
        {
            xx = x;
        }
        return xx;
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        try
        {
            var yy1 = Convert.ToInt32(Session["SessionName"].ToString().Substring(0, 4));
            yy1--;
            var yy2 = Convert.ToInt32(Session["SessionName"].ToString().Substring(5, 4));
            yy2--;
            var Sess = yy1.ToString() + "-" + yy2.ToString();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = BAL.objBal.dbGet_connection().ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "CopyPreviousYearStudentVehicleAllocation";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString().Trim());
                    cmd.Parameters.AddWithValue("@PreviousSessionName", Sess.ToString().Trim());
                    cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString().Trim());
                    cmd.Parameters.AddWithValue("@ClassId", DrpClass.SelectedValue.ToString().Trim());
                    cmd.Parameters.AddWithValue("@SectionId", drpSection.SelectedValue.ToString().Trim());
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    cmd.Parameters.Clear();
                    if (dt.Rows.Count > 0)
                    {
                        string msg = dt.Rows[0]["val"].ToString();
                        if (msg== "Save")
                        {
                            Campus camp = new Campus(); camp.msgbox(Page, msgbox1, "The conveyance data has been successfully imported from the last session.", "S");
                        }
                        else
                        {
                            Campus camp = new Campus(); camp.msgbox(Page, msgbox1, msg, "A");
                        }
                      
                    }
                }
            }

        }
        catch (Exception ex) {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox1, ex.Message, "A");
        }
    }
}