using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;
using System.Globalization;

public partial class MiscellaneousFeeAllotment : System.Web.UI.Page
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
        grdshow.Visible = false;
        string studentId = Request.Form[hfStudentId.UniqueID];
        if (studentId == string.Empty)
        {
            studentId = TxtEnter.Text.Trim();
        }
        _sql = "select SG.Id, SC.SectionName,SO.Card,SO.Medium as Medium,CM.ClassName,so.MODForFeeDeposit as mod,convert(nvarchar,So.DateOfAdmiission,106) as DateOfAdmiission ,SO.SectionId,Sf.FatherName,SF.MotherName,SG.FirstName,SG.MiddleName,SG.LastName,so.StEnRCode as StEnRCode,sg.srno  as srno,case  when so.TransportRequired='Yes' then 'Yes' else 'No' end as TransportRequired,so.wayamount as wayamount, bm.BranchName,SF.FamilyContactNo,SG.PhotoPath from StudentGenaralDetail SG ";
        _sql = _sql + "   left join StudentFamilyDetails SF on SG.srno=SF.srno";
        _sql = _sql + "   left join StudentOfficialDetails SO on SG.srno=SO.srno";
        _sql = _sql + "   left join ClassMaster CM on SO.AdmissionForClassId=CM.Id";
        _sql = _sql + "   left join SectionMaster SC on SO.SectionId=SC.Id left join BranchMaster bm on SO.Branch=bm.id and bm.IsDisplay=1 ";
        _sql = _sql + "   where SG.srno=" + "'" + studentId + "'";
        _sql = _sql + "   and SG.SessionName='" + Session["SessionName"] + "' and SG.BranchCode=" + Session["BranchCode"] + "";
        _sql = _sql + "   and Sf.SessionName='" + Session["SessionName"] + "'  and SO.SessionName='" + Session["SessionName"] + "'";
        _sql = _sql + "   and cm.SessionName='" + Session["SessionName"] + "'    and Sc.SessionName='" + Session["SessionName"] + "'";
        _sql = _sql + "   and SO.Withdrwal is null";
        Grd.DataSource = _oo.GridFill(_sql);
        Grd.DataBind();
        DataSet ds;
        ds = _oo.GridFill(_sql);
        string card = _oo.ReturnTag(_sql, "Card");
        string classname = _oo.ReturnTag(_sql, "ClassName");
        string mod = _oo.ReturnTag(_sql, "mod");
        if (ds != null && Grd.Rows.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
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
        else
        {
            grdshow.Visible = false;
            Grd.Visible = false;
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

        sql = "select FeeHead, FeeHead+' ('+convert(varchar, Amount)+')' as FeeHeadName, 0 id, Amount from MiscellaneousFeeHeadWise ";
        sql = sql + " where SessionName = '" + Session["SessionName"] + "' and BranchCode = " + Session["BranchCode"] + " and Classid = " + ClassId + "";
        var dts = _oo.Fetchdata(sql);
        if (dts.Rows.Count == 0)
        {
            GrdFeeDetails.DataSource = null;
            GrdFeeDetails.DataBind();
            GrdFeeDetails.Visible = false;
            grdshow.Visible = false;
            lnkSubmit.Visible = false;
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Please create Miscellaneous Fee Heads!", "A");
        }
        else
        {
            GrdFeeDetails.Visible = true;
            grdshow.Visible = true;
            lnkSubmit.Visible = true;
        }   


        sql = "select * from MonthMaster where SessionName = '" + Session["SessionName"] + "' and BranchCode = " + Session["BranchCode"] + " and ClassId = '" + ClassId + "' and formonth=1 and CardType = " + CardId + " order by MonthId asc";
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
              
                double amt = 0;
                if (dts.Rows.Count > 0)
                {
                    chkList.DataSource = dts;
                    chkList.DataTextField = "FeeHeadName";
                    chkList.DataValueField = "FeeHead";
                    chkList.DataBind();

                    sql = "select 0 id, Feehead, Amount from MiscellaneousFeeAllotment where SessionName = '" + Session["SessionName"] + "' and BranchCode = " + Session["BranchCode"] + " and InstallmentId = " + lblMonthId.Text + " and SrNo = '" + studentId + "'";
                    var dts1 = _oo.Fetchdata(sql);
                    if (dts1.Rows.Count > 0)
                    {
                        for (int j = 0; j < dts1.Rows.Count; j++)
                        {
                            for (int s = 0; s < chkList.Items.Count; s++)
                            {
                                if (chkList.Items[s].Value == dts1.Rows[j]["Feehead"].ToString() && double.Parse(dts1.Rows[j]["Amount"].ToString())>0)
                                {
                                    chkList.Items[s].Selected = true;
                                    amt = amt + double.Parse(dts1.Rows[j]["Amount"].ToString() == "" ? "0" : dts1.Rows[j]["Amount"].ToString());
                                }
                            }
                        }
                        Label lblAmount = (Label)GrdFeeDetails.Rows[i].FindControl("lblAmount");
                        lblAmount.Text = amt.ToString("0.00");

                    }

                    sql = "select count(*) cnt from MiscellaneousFeeDeposit where SessionName = '" + Session["SessionName"] + "' and BranchCode = " + Session["BranchCode"] + " and InstallmentId = " + lblMonthId.Text + " and SrNo = '" + studentId + "'";
                    if (_oo.ReturnTag(sql, "cnt") != "0")
                    {
                        chkList.Enabled = false;
                    }
                    else
                    {
                        chkList.Enabled = true;
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
                sql = "select top(1) Amount from MiscellaneousFeeHeadWise ";
                sql = sql + " where SessionName = '" + Session["SessionName"] + "' and BranchCode = " + Session["BranchCode"] + " and Classid = " + ClassId + " and FeeHead='" + chkList.Items[s].Value + "'";
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
                    sql = "select top(1) Amount from MiscellaneousFeeHeadWise ";
                    sql = sql + " where SessionName = '" + Session["SessionName"] + "' and BranchCode = " + Session["BranchCode"] + " and Classid = '" + ClassId + "' and FeeHead='" + chkList.Items[i].Value + "'";
                    var dts1 = _oo.Fetchdata(sql);
                    double amt = double.Parse(dts1.Rows[0]["Amount"].ToString() == "" ? "0" : dts1.Rows[0]["Amount"].ToString());

                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "MiscellaneousFeeAllotmentProc";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = _con;

                    cmd.Parameters.AddWithValue("@SrNo", studentId);
                    cmd.Parameters.AddWithValue("@installmentId", lblMonthId.Text);
                    cmd.Parameters.AddWithValue("@FeeHead", chkList.Items[i].Value);
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
  
