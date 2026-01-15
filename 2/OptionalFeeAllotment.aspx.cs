using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;
using System.Globalization;

public partial class OptionalFeeAllotment : System.Web.UI.Page
{
    public SqlConnection _con = new SqlConnection();
    private readonly Campus _oo = new Campus();
    private string _sql = "", sql = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        _con = _oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);

        if (!IsPostBack)
        {
        }
    }
    protected void LinkButton7_Click(object sender, EventArgs e)
    {
        view();
        loadFee();
    }
    protected void TxtEnter_TextChanged(object sender, EventArgs e)
    {
        view();
        loadFee();
    }
    public void view()
    {
        string studentId = Request.Form[hfStudentId.UniqueID];
        if (studentId == string.Empty)
        {
            studentId = TxtEnter.Text.Trim();
        }
        _sql = "select srno, name, FatherName, CombineClassName, Medium, DateOfAdmiission, FamilyContactNo, stenrcode from AllStudentRecord_UDF('" + Session["SessionName"] + "', " + Session["BranchCode"] + ")";
        _sql = _sql + "   where srno='"+ studentId + "' and Withdrwal is null";
        Grd.DataSource = _oo.GridFill(_sql);
        Grd.DataBind();
        var ds = _oo.GridFill(_sql);
        if (Grd.Rows.Count > 0)
        {
            imgs.Visible = true;
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
                        img.ImageUrl = dsPhoto.Tables[0].Rows[0]["PhotoPath"].ToString() != string.Empty ? dsPhoto.Tables[0].Rows[0]["PhotoPath"] + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture) : "../img/user-pic/user-pic.jpg" + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture);
                        studentImg.NavigateUrl = dsPhoto.Tables[0].Rows[0]["PhotoPath"].ToString() != string.Empty ? dsPhoto.Tables[0].Rows[0]["PhotoPath"] + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture) : "../img/user-pic/user-pic.jpg" + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture);
                        hylinkmoredetails.NavigateUrl = "../11/StudentRegView.aspx?print=1&id=" + ds.Tables[0].Rows[0]["stenrcode"];
                    }
                }
            }
        }

    }
    private void loadFee()
    {
        string studentId = Request.Form[hfStudentId.UniqueID];
        if (studentId == string.Empty)
        {
            studentId = TxtEnter.Text.Trim();
        }
        sql = "select BranchId, ClassId, CardId, Medium, TypeOFAdmision from AllStudentRecord_UDF('" + Session["SessionName"] + "', " + Session["BranchCode"] + ") where SrNo='" + studentId + "'";
        string BranchId = _oo.ReturnTag(sql, "BranchId"); string ClassId = _oo.ReturnTag(sql, "ClassId");
        string CardId = _oo.ReturnTag(sql, "CardId"); string Medium = _oo.ReturnTag(sql, "Medium");
        string TypeOFAdmision = _oo.ReturnTag(sql, "TypeOFAdmision");

        sql = "select * from MonthMaster where SessionName = '" + Session["SessionName"] + "' and BranchCode = " + Session["BranchCode"] + " and ClassId = '" + ClassId + "' and CardType = " + CardId + " and formonth=1 and MonthId in (";
        sql = sql + " select distinct MonthId from FeeheadMaster fh ";
        sql = sql + " inner join FeeAllotedForClassWise fa on fa.feeheadid = fh.id and fh.BranchCode = fa.BranchCode";
        sql = sql + " where fh.BranchCode = " + Session["BranchCode"] + " and fh.FeeType = 'Tuition Fee (Optional)'";
        sql = sql + " and fa.SessionName = '" + Session["SessionName"] + "' and fa.BranchCode = " + Session["BranchCode"] + " and Classid = '" + ClassId + "' and CardType = " + CardId + " and AdmissionType = '" + TypeOFAdmision + "' and Branchid = " + BranchId + " and Medium = '" + Medium + "')";
        var dt = _oo.Fetchdata(sql);
        GrdFeeDetails.DataSource = dt;
        GrdFeeDetails.DataBind();
        if (GrdFeeDetails.Rows.Count > 0)
        {
            double total = 0;
            for (int i = 0; i < GrdFeeDetails.Rows.Count; i++)
            {
                Label lblMonthId = (Label)GrdFeeDetails.Rows[i].FindControl("lblMonthId");
                CheckBoxList chkList = (CheckBoxList)GrdFeeDetails.Rows[i].FindControl("chkFeeHead");
                sql = "select fh.FeeHead, fh.id, FeeAmount as Amount from FeeheadMaster fh ";
                sql = sql + " inner join FeeAllotedForClassWise fa on fa.feeheadid = fh.id and fh.BranchCode = fa.BranchCode";
                sql = sql + " where fh.BranchCode = " + Session["BranchCode"] + " and fh.FeeType = 'Tuition Fee (Optional)'";
                sql = sql + " and fa.SessionName = '" + Session["SessionName"] + "' and fa.BranchCode = " + Session["BranchCode"] + " and Classid = '" + ClassId + "' and CardType = " + CardId + " and AdmissionType = '" + TypeOFAdmision + "' and Branchid = " + BranchId + " and Medium = '" + Medium + "' and Monthid=" + lblMonthId.Text + "";
                var dts = _oo.Fetchdata(sql);
                double amt = 0;
                if (dts.Rows.Count > 0)
                {
                    chkList.DataSource = dts;
                    chkList.DataTextField = "FeeHead";
                    chkList.DataValueField = "id";
                    chkList.DataBind();

                    sql = "select 0 id, Feeheadid, Amount from OptionalFeeAllotment where SessionName = '" + Session["SessionName"] + "' and BranchCode = " + Session["BranchCode"] + " and InstallmentId = " + lblMonthId.Text + " and SrNo = '" + studentId + "'";
                    var dts1 = _oo.Fetchdata(sql);
                    if (dts1.Rows.Count > 0)
                    {
                        //GrdFeeDetails.Rows[i].BackColor = System.Drawing.Color.Green;
                        //GrdFeeDetails.Rows[i].ForeColor = System.Drawing.Color.White;
                        //chkList.BackColor = System.Drawing.Color.Green;
                        //chkList.ForeColor = System.Drawing.Color.White;

                        for (int j = 0; j < dts1.Rows.Count; j++)
                        {
                            for (int s = 0; s < chkList.Items.Count; s++)
                            {
                                if (chkList.Items[s].Value == dts1.Rows[j]["Feeheadid"].ToString())
                                {
                                    chkList.Items[s].Selected = true;
                                    amt = amt + double.Parse(dts1.Rows[j]["Amount"].ToString() == "" ? "0" : dts1.Rows[j]["Amount"].ToString());
                                }
                                string sql_new = "Select Top 1 1 flag from CompositFeeDeposit where FeeHeadId='" + dts1.Rows[j]["Feeheadid"].ToString() + "' and SessionName = '" + Session["SessionName"] + "' and BranchCode = " + Session["BranchCode"] + " and InstallmentId = " + lblMonthId.Text + " and SrNo = '" + studentId + "' and isnull(receiptStatus, '')<>'Cancelled'";
                                if (_oo.ReturnTag(sql_new, "flag") == "1")
                                {
                                    chkList.Enabled = false;
                                }
                                else
                                {
                                    chkList.Enabled = true;
                                }
                            }
                        }
                        Label lblAmount = (Label)GrdFeeDetails.Rows[i].FindControl("lblAmount");
                        lblAmount.Text = amt.ToString("0.00");

                    }
                    total = total + amt;
                }
            }
            Label lblTotal = (Label)GrdFeeDetails.FooterRow.FindControl("lblTotal");
            lblTotal.Text = total.ToString("0.00");
            lnkSubmit.Visible = true;
        }
        else
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Optional fee not found for the class of this student.", "A");
            lnkSubmit.Visible = false;
        }
    }

    protected void chkFeeHead_SelectedIndexChanged(object sender, EventArgs e)
    {
        string studentId = Request.Form[hfStudentId.UniqueID];
        if (studentId == string.Empty)
        {
            studentId = TxtEnter.Text.Trim();
        }
        sql = "select BranchId, ClassId, CardId, Medium, TypeOFAdmision from AllStudentRecord_UDF('" + Session["SessionName"] + "', " + Session["BranchCode"] + ") where SrNo='" + studentId + "'";
        string BranchId = _oo.ReturnTag(sql, "BranchId"); string ClassId = _oo.ReturnTag(sql, "ClassId");
        string CardId = _oo.ReturnTag(sql, "CardId"); string Medium = _oo.ReturnTag(sql, "Medium");
        string TypeOFAdmision = _oo.ReturnTag(sql, "TypeOFAdmision");
        CheckBoxList lnk = (CheckBoxList)sender;
        CheckBoxList chkList = (CheckBoxList)lnk.NamingContainer.FindControl("chkFeeHead");
        Label lblMonthId = (Label)lnk.NamingContainer.FindControl("lblMonthId");
        Label lblAmount = (Label)lnk.NamingContainer.FindControl("lblAmount");
        double amt = 0;
        for (int s = 0; s < chkList.Items.Count; s++)
        {
            if (chkList.Items[s].Selected)
            {
                sql = "select top(1) FeeAmount as Amount from FeeheadMaster fh ";
                sql = sql + " inner join FeeAllotedForClassWise fa on fa.feeheadid = fh.id and fh.BranchCode = fa.BranchCode";
                sql = sql + " where fh.BranchCode = " + Session["BranchCode"] + " and fh.FeeType = 'Tuition Fee (Optional)'";
                sql = sql + " and fa.SessionName = '" + Session["SessionName"] + "' and fa.BranchCode = " + Session["BranchCode"] + " and Classid = '" + ClassId + "' and CardType = " + CardId + " and AdmissionType = '" + TypeOFAdmision + "' and Branchid = " + BranchId + " and Medium = '" + Medium + "' and Monthid=" + lblMonthId.Text + " and  fh.id='" + chkList.Items[s].Value + "'";
                var dts1 = _oo.Fetchdata(sql);
                if (dts1.Rows.Count > 0)
                {
                    amt = amt + double.Parse(dts1.Rows[0]["Amount"].ToString() == "" ? "0" : dts1.Rows[0]["Amount"].ToString());
                }
            }
        }
        lblAmount.Text = amt.ToString("0.00");
        double total = 0;
        for (int i = 0; i < GrdFeeDetails.Rows.Count; i++)
        {
            Label lblAmounts = (Label)GrdFeeDetails.Rows[i].FindControl("lblAmount");
            total = total + double.Parse(lblAmounts.Text == "" ? "0" : lblAmounts.Text);
        }
        Label lblTotal = (Label)GrdFeeDetails.FooterRow.FindControl("lblTotal");
        lblTotal.Text = total.ToString("0.00");
    }

    protected void lnkSubmit_Click(object sender, EventArgs e)
    {
        string studentId = Request.Form[hfStudentId.UniqueID];
        if (studentId == string.Empty)
        {
            studentId = TxtEnter.Text.Trim();
        }
        sql = "select Srno, BranchId, ClassId, CardId, Medium, TypeOFAdmision from AllStudentRecord_UDF('" + Session["SessionName"] + "', " + Session["BranchCode"] + ") where SrNo='" + studentId + "'";
        string BranchId = _oo.ReturnTag(sql, "BranchId"); string ClassId = _oo.ReturnTag(sql, "ClassId");
        string CardId = _oo.ReturnTag(sql, "CardId"); string Medium = _oo.ReturnTag(sql, "Medium");
        string TypeOFAdmision = _oo.ReturnTag(sql, "TypeOFAdmision");

        for (int s = 0; s < GrdFeeDetails.Rows.Count; s++)
        {
            CheckBoxList chkList = (CheckBoxList)GrdFeeDetails.Rows[s].FindControl("chkFeeHead");
            Label lblMonthId = (Label)GrdFeeDetails.Rows[s].FindControl("lblMonthId");
            if (chkList.Enabled)
            {
                for (int i = 0; i < chkList.Items.Count; i++)
                {
                    sql = "select top(1) FeeAmount as Amount from FeeheadMaster fh ";
                    sql = sql + " inner join FeeAllotedForClassWise fa on fa.FeeHeadID = fh.ID and fh.BranchCode = fa.BranchCode";
                    sql = sql + " where fh.BranchCode = " + Session["BranchCode"] + " and fh.FeeType = 'Tuition Fee (Optional)'";
                    sql = sql + " and fa.SessionName = '" + Session["SessionName"] + "' and fa.BranchCode = " + Session["BranchCode"] + " and ClassId = '" + ClassId + "' and CardType = " + CardId + " and AdmissionType = '" + TypeOFAdmision + "' and Branchid = " + BranchId + " and Medium = '" + Medium + "' and Monthid=" + lblMonthId.Text + " and  fh.id='" + chkList.Items[i].Value + "'";
                    var dts1 = _oo.Fetchdata(sql);
                    double amt = double.Parse(dts1.Rows[0]["Amount"].ToString() == "" ? "0" : dts1.Rows[0]["Amount"].ToString());

                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "OptionalFeeAllotmentProc";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = _con;

                    cmd.Parameters.AddWithValue("@SrNo", studentId);
                    cmd.Parameters.AddWithValue("@installmentId", lblMonthId.Text);
                    cmd.Parameters.AddWithValue("@FeeHeadId", chkList.Items[i].Value);
                    cmd.Parameters.AddWithValue("@Amount", (chkList.Items[i].Selected == true ? amt : 0));
                    cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
                    cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                    cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                    try
                    {
                        _con.Open();
                        cmd.ExecuteNonQuery();
                        _con.Close();
                    }
                    catch (Exception) { }
                }
            }
        }
        Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Submitted successfuly", "S");
        loadFee();
    }
}
  
