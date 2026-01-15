using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI.WebControls;

namespace _2
{
    public partial class ManualDiscount_old : System.Web.UI.Page
    {
        private SqlConnection _con;
        private readonly Campus _oo;
        private string _sql = "";
        private string _s;

        public ManualDiscount_old()
        {
            _con = new SqlConnection();
            _oo = new Campus();
            _s = String.Empty;
        }

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
                
                _sql = "Select ClassName as Class from ClassMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"].ToString() + " Order by CIDOrder";
                _oo.FillDropDownWithOutSelect(_sql, drpclass, "Class");
                drpclass.Items.Insert(0, "Select ALL");

                _sql = "select id, DiscHeadName from ManualDiscountHeads where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"].ToString() + " Order by id asc";
                _oo.FillDropDown_withValue(_sql, drpHrads, "DiscHeadName", "id");
                drpHrads.Items.Insert(0, "Select ALL");

                _sql = "select id, DiscHeadName from ManualDiscountHeads where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"].ToString() + " Order by id asc";
                _oo.FillDropDown_withValue(_sql, drpDiscountNameInsert, "DiscHeadName", "id");
               

                

                //filterDisplay();
            }
        }
        protected void LinkButton7_Click(object sender, EventArgs e)
        {
            view();
        }
        protected void TxtEnter_TextChanged(object sender, EventArgs e)
        {
            view();
        }
        
        public void view()
        {
            DropDownList4.SelectedIndex = 0;
            if (drpDiscountNameInsert.Items.Count>0)
            {
                drpDiscountNameInsert.SelectedIndex = 0;
            }
            txtRemark.Text = "";
            Repeater1.DataSource = null;
            Repeater1.DataBind();
            grdshow.Visible = false;
            divControls.Visible = false;
            string studentId = Request.Form[hfStudentId.UniqueID];
            if (studentId == string.Empty)
            {
                studentId = TxtEnter.Text.Trim();
            }
            _sql = "select SG.Id, SC.SectionName,SO.Card,SO.Medium as Medium,CM.ClassName,so.MODForFeeDeposit as mod,convert(nvarchar,So.DateOfAdmiission,106) as DateOfAdmiission ,SO.SectionId,Sf.FatherName,SF.MotherName,SG.FirstName,SG.MiddleName,SG.LastName,so.StEnRCode as StEnRCode,sg.srno  as srno,case  when so.TransportRequired='Yes' then 'Yes' else 'No' end as TransportRequired,so.wayamount as wayamount, bm.BranchName,SF.FamilyContactNo,SG.PhotoPath from StudentGenaralDetail SG ";
            _sql = _sql + "   inner join StudentFamilyDetails SF on SG.srno=SF.srno and SG.BranchCode=SF.BranchCode";
            _sql = _sql + "   inner join StudentOfficialDetails SO on SG.srno=SO.srno and  SG.BranchCode=SO.BranchCode";
            _sql = _sql + "   inner join ClassMaster CM on SO.AdmissionForClassId=CM.Id and SO.BranchCode=CM.BranchCode";
            _sql = _sql + "   inner join SectionMaster SC on SO.SectionId=SC.Id  and SO.BranchCode=sc.BranchCode left join BranchMaster bm on SO.Branch=bm.id  and SO.BranchCode=bm.BranchCode and bm.IsDisplay=1 ";
            _sql = _sql + "   where SG.srno=" + "'" + studentId + "'";
            _sql = _sql + "   and SG.SessionName='" + Session["SessionName"] + "' and SG.BranchCode=" + Session["BranchCode"] + "";
            _sql = _sql + "   and Sf.SessionName='" + Session["SessionName"] + "'  and SO.SessionName='" + Session["SessionName"] + "'";
            _sql = _sql + "   and cm.SessionName='" + Session["SessionName"] + "'    and Sc.SessionName='" + Session["SessionName"] + "'";
            _sql = _sql + "   and SO.Withdrwal is null and sg.BranchCode=" + Session["BranchCode"] + "";
            Grdss.DataSource = _oo.GridFill(_sql);
            Grdss.DataBind();
            DataSet ds;
            ds = _oo.GridFill(_sql);
            string card = _oo.ReturnTag(_sql, "Card");
            string classname = _oo.ReturnTag(_sql, "ClassName");
            string mod = _oo.ReturnTag(_sql, "mod");
            // ReSharper disable once UseNullPropagation
            if (ds != null && Grdss.Rows.Count > 0)
            {
                filterDisplay(studentId);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    divControls.Visible = true;
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
                if (DropDownList4.SelectedValue != "")
                {
                    _sql = "select MonthName,mm.MonthId Id, mm.MonthId as MonthId from MonthMaster mm";
                    _sql = _sql + " inner join FeeGroupMaster fgm on (Case when ISNUMERIC(CardType)=1 THEN fgm.Id Else fgm.FeeGroupName End)=mm.CardType and fgm.SessionName=mm.SessionName";
                    _sql = _sql + " inner join ClassMaster cm on cm.Id=mm.ClassId and cm.SessionName=mm.SessionName  and cm.BranchCode=mm.BranchCode";
                    _sql = _sql + " where fgm.FeeGroupName='" + card + "' and cm.ClassName='" + classname + "' and mm.SessionName='" + Session["SessionName"] + "' ";
                    _sql = _sql + " and mm.BranchCode=" + Session["BranchCode"] + "";
                    if (DropDownList4.SelectedValue != "")
                    {
                        _sql = _sql + " and mm.BranchCode=" + Session["BranchCode"] + " and mm.MonthId not in (select InstallmentId from ManualDiscount where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and DiscountFor= case when '" + DropDownList4.SelectedValue + "'='' then DiscountFor else '" + DropDownList4.SelectedValue + "' end and Srno='" + studentId + "') order by MonthId";

                    }
                    else
                    {
                        _sql = _sql + "  order by MonthId";
                    }
                }
                else
                {
                    _sql = "select MonthName,mm.MonthId Id, mm.MonthId as MonthId from MonthMaster mm where mm.MonthId=1.2";
                    divdDiscountNameInsert.Visible = false;
                    divRemark.Visible = false;
                    divFillInstallment.Visible = false;
                    LinkButton1.Visible = false;
                }
                if (_oo.GridFill(_sql)!=null)
                {
                    if (_oo.GridFill(_sql).Tables.Count>0)
                    {
                        if (_oo.GridFill(_sql).Tables[0].Rows.Count > 0)
                        {
                            Repeater1.DataSource = _oo.GridFill(_sql);
                            Repeater1.DataBind();
                            divdDiscountNameInsert.Visible = true;
                            divRemark.Visible = true;
                            divFillInstallment.Visible = true;
                            LinkButton1.Visible = true;
                            TextBox4.Text = "";
                        }
                        else
                        {
                            Repeater1.DataSource = null;
                            Repeater1.DataBind();
                            divdDiscountNameInsert.Visible = false;
                            divRemark.Visible = false;
                            divFillInstallment.Visible = false;
                            LinkButton1.Visible = false;
                            TextBox4.Text = "";
                        }
                    }
                }

            }
            else
            {
                //Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Sorry, Invalid " + DrpEnter.SelectedItem.Text + "!", "A");
                grdshow.Visible = false;
                Grdss.Visible = false;
            }



        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            if (DropDownList4.SelectedValue=="")
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Please select Discount For.", "A");
                return;
            }
            string studentId = Request.Form[hfStudentId.UniqueID];
            if (studentId == string.Empty)
            {
                studentId = TxtEnter.Text.Trim();
            }

            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    for (int i = 0; i < Repeater1.Items.Count; i++)
                    {
                        TextBox TextBox1 = (TextBox)Repeater1.Items[i].FindControl("TextBox1");
                        Label Monthid = (Label)Repeater1.Items[i].FindControl("LabelMonthid");
                        if (TextBox1.Text != "")
                        {

                            cmd.CommandText = "ManualDiscountProc";
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Connection = _con;
                            cmd.Parameters.AddWithValue("@SrNo", studentId);
                            cmd.Parameters.AddWithValue("@DiscountFor", DropDownList4.SelectedValue.Trim());
                            cmd.Parameters.AddWithValue("@DiscountName", drpDiscountNameInsert.SelectedValue.Trim());

                            cmd.Parameters.AddWithValue("@InstallmentId", Monthid.Text);
                            cmd.Parameters.AddWithValue("@Amount", TextBox1.Text);

                            cmd.Parameters.AddWithValue("@Remark", txtRemark.Text);
                            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                            cmd.Parameters.AddWithValue("@UserName", Session["LoginName"].ToString());
                            cmd.Parameters.AddWithValue("@Action", "insert");

                            try
                            {
                                _con.Open();
                                cmd.ExecuteNonQuery();
                                cmd.Parameters.Clear();
                                _con.Close();
                                
                            }
                            catch (Exception ex)
                            {
                                // ignored
                            }
                        }
                    }
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Submitted successfully.", "S");

                    _sql = "select SO.Card, CM.ClassName from StudentOfficialDetails SO";
                    _sql = _sql + "   inner join ClassMaster CM on SO.AdmissionForClassId=CM.Id";
                    _sql = _sql + "   where SO.srno=" + "'" + studentId + "' and SO.SessionName='" + Session["SessionName"] + "'";
                    _sql = _sql + "   and cm.SessionName='" + Session["SessionName"] + "' and cm.BranchCode=" + Session["BranchCode"] + " and SO.Withdrwal is null and so.BranchCode=" + Session["BranchCode"] + "";
                    string card = _oo.ReturnTag(_sql, "Card");
                    string classname = _oo.ReturnTag(_sql, "ClassName");

                    if (DropDownList4.SelectedValue != "")
                    {
                        _sql = "select MonthName,mm.MonthId Id, mm.MonthId as MonthId from MonthMaster mm";
                        _sql = _sql + " inner join FeeGroupMaster fgm on (Case when ISNUMERIC(CardType)=1 THEN fgm.Id Else fgm.FeeGroupName End)=mm.CardType and fgm.SessionName=mm.SessionName";
                        _sql = _sql + " inner join ClassMaster cm on cm.Id=mm.ClassId and cm.SessionName=mm.SessionName and cm.BranchCode=mm.BranchCode";
                        _sql = _sql + " where fgm.FeeGroupName='" + card + "' and cm.ClassName='" + classname + "' and mm.SessionName='" + Session["SessionName"] + "' ";
                        _sql = _sql + " and mm.BranchCode=" + Session["BranchCode"] + "";
                        if (DropDownList4.SelectedValue != "")
                        {
                            _sql = _sql + " and mm.BranchCode=" + Session["BranchCode"] + " and mm.MonthId not in (select InstallmentId from ManualDiscount where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and DiscountFor= case when '" + DropDownList4.SelectedValue + "'='' then DiscountFor else '" + DropDownList4.SelectedValue + "' end and Srno='" + studentId + "') order by MonthId";

                        }
                        else
                        {
                            _sql = _sql + "  order by MonthId";
                        }
                    }
                    else
                    {
                        _sql = "select MonthName,mm.MonthId Id, mm.MonthId as MonthId from MonthMaster mm where mm.MonthId=1.2";
                        divdDiscountNameInsert.Visible = false;
                        divRemark.Visible = false;
                        divFillInstallment.Visible = false;
                        LinkButton1.Visible = false;
                    }
                    if (_oo.GridFill(_sql) != null)
                    {
                        if (_oo.GridFill(_sql).Tables.Count > 0)
                        {
                            if (_oo.GridFill(_sql).Tables[0].Rows.Count > 0)
                            {
                                Repeater1.DataSource = _oo.GridFill(_sql);
                                Repeater1.DataBind();
                                divdDiscountNameInsert.Visible = true;
                                divRemark.Visible = true;
                                divFillInstallment.Visible = true;
                                LinkButton1.Visible = true;
                            }
                            else
                            {
                                Repeater1.DataSource = null;
                                Repeater1.DataBind();
                                divdDiscountNameInsert.Visible = false;
                                divRemark.Visible = false;
                                divFillInstallment.Visible = false;
                                LinkButton1.Visible = false;
                            }
                        }
                    }
                    filterDisplay(studentId);
                }
            }

            catch (Exception)
            {
                // ignored
            }
        }

        protected void drpclass1_SelectedIndexChanged(object sender, EventArgs e)
        {
            filterDisplay();
        }
        protected void drpHrads_SelectedIndexChanged(object sender, EventArgs e)
        {
            filterDisplay();
        }
        public void filterDisplay(string srno = "")
        {
            GrdTutionFeeDetails.DataSource = null;
            GrdTutionFeeDetails.DataBind();
            GrdTransportFeeDetails.DataSource = null;
            GrdTransportFeeDetails.DataBind();
            GrdHostelFeeDetails.DataSource = null;
            GrdHostelFeeDetails.DataBind();
            GrdMiscellaneousFeeDetails.DataSource = null;
            GrdMiscellaneousFeeDetails.DataBind();


            hdnTutionFeeDiscount.Visible = false;
            hdnTransportFeeDiscount.Visible = false;
            hdnHostelFeeDiscount.Visible = false;
            hdnMiscellaneousFeeDiscount.Visible = false;
            if (DropDownList4.SelectedIndex == 0)
            {
                try
                {

                    _sql = "Select dis.id, asr.Card, dis.SrNo, Name, mm.MonthName, DiscountFor, dis.DiscountName, mdh.DiscHeadName, InstallmentId, Amount, Remark, dis.SessionName, dis.BranchCode, dis.RecordedDate, UserName";
                    _sql = _sql + "  from ManualDiscount dis";
                    _sql = _sql + "  inner join AllStudentRecord_UDF('" + Session["SessionName"] + "', " + Session["BranchCode"] + ") asr on asr.SessionName = dis.SessionName  and dis.SrNo=asr.SrNo";
                    _sql = _sql + "  inner join ManualDiscountHeads mdh on mdh.id=dis.DiscountName and dis.SessionName = mdh.SessionName and mdh.BranchCode=dis.BranchCode";
                    _sql = _sql + "  inner join MonthMaster mm on mm.ClassId = asr.ClassId and mm.SessionName = asr.SessionName and mm.BranchCode = dis.BranchCode";
                    _sql = _sql + "  and dis.InstallmentId = mm.MonthId and convert(varchar, mm.CardType)=convert(varchar, asr.CardId) and case when asr.TypeofEducation='R' then 'Regular' else 'Private' end = mm.typeofAdd";
                    _sql = _sql + "  where dis.SrNo = '" + srno + "' and DiscountFor='Tuition Fee' and dis.ApplyFrom='Manual' and dis.BranchCode="+Session["BranchCode"]+"";
                    GrdTutionFeeDetails.DataSource = _oo.GridFill(_sql);
                    GrdTutionFeeDetails.DataBind();
                    if (GrdTutionFeeDetails.Rows.Count > 0)
                    {
                        hdnTutionFeeDiscount.Visible = true;
                        GrdTutionFeeDetails.Visible = true;
                        for (int i = 0; i < GrdTutionFeeDetails.Rows.Count; i++)
                        {

                            Label lblDiscountName = (Label)GrdTutionFeeDetails.Rows[i].FindControl("lblDiscountName");
                            DropDownList drpDiscountName = (DropDownList)GrdTutionFeeDetails.Rows[i].FindControl("drpDiscountName");
                            _sql = "select id, DiscHeadName from ManualDiscountHeads where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " Order by id asc";
                            _oo.FillDropDown_withValue(_sql, drpDiscountName, "DiscHeadName", "id");
                            drpDiscountName.SelectedValue = lblDiscountName.Text.Trim();
                            Label lblInstallmentId = (Label)GrdTutionFeeDetails.Rows[i].FindControl("lblInstallmentId");
                            LinkButton lnkUpdateTutionFee = (LinkButton)GrdTutionFeeDetails.Rows[i].FindControl("lnkUpdateTutionFee");
                            //_sql = "select count(*) cnt from TutionFeeDeposit where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and InstallmentId="+ lblInstallmentId.Text + " and SrNo='" + srno + "'";
                            //if (_oo.ReturnTag(_sql, "cnt")!="0")
                            //{
                            //    lnkUpdateTutionFee.Text = "<i class='fa fa-lock'></i>";
                            //    lnkUpdateTutionFee.Enabled = false;
                            //}
                        }
                    }
                    else
                    {
                        hdnTutionFeeDiscount.Visible = false;
                        GrdTutionFeeDetails.Visible = false;
                    }
                    countamount(GrdTutionFeeDetails);

                }
                catch (Exception ex)
                {
                    // ignored
                }

                try
                {

                    _sql = "Select dis.id, asr.Card, dis.SrNo, Name, mm.MonthName, DiscountFor, dis.DiscountName, mdh.DiscHeadName, InstallmentId, Amount, Remark, dis.SessionName, dis.BranchCode, dis.RecordedDate, UserName";
                    _sql = _sql + "  from ManualDiscount dis";
                    _sql = _sql + "  inner join AllStudentRecord_UDF('" + Session["SessionName"] + "', " + Session["BranchCode"] + ") asr on asr.SessionName = dis.SessionName  and dis.SrNo=asr.SrNo";
                    _sql = _sql + "  inner join ManualDiscountHeads mdh on mdh.id=dis.DiscountName and dis.SessionName = mdh.SessionName and mdh.BranchCode=dis.BranchCode";
                    _sql = _sql + "  inner join MonthMaster mm on mm.ClassId = asr.ClassId and mm.SessionName = asr.SessionName and mm.BranchCode = dis.BranchCode";
                    _sql = _sql + "  and dis.InstallmentId = mm.MonthId and convert(varchar, mm.CardType)=convert(varchar, asr.CardId) and case when asr.TypeofEducation='R' then 'Regular' else 'Private' end = mm.typeofAdd";
                    _sql = _sql + "  where dis.SrNo = '" + srno + "' and DiscountFor='Transport Fee' and dis.ApplyFrom='Manual' and dis.BranchCode=" + Session["BranchCode"] + "";
                    GrdTransportFeeDetails.DataSource = _oo.GridFill(_sql);
                    GrdTransportFeeDetails.DataBind();
                    if (GrdTransportFeeDetails.Rows.Count > 0)
                    {
                        hdnTransportFeeDiscount.Visible = true;
                        GrdTransportFeeDetails.Visible = true;
                        for (int i = 0; i < GrdTransportFeeDetails.Rows.Count; i++)
                        {

                            Label lblDiscountName = (Label)GrdTransportFeeDetails.Rows[i].FindControl("lblDiscountName");
                            DropDownList drpDiscountName = (DropDownList)GrdTransportFeeDetails.Rows[i].FindControl("drpDiscountName");
                            _sql = "select id, DiscHeadName from ManualDiscountHeads where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " Order by id asc";
                            _oo.FillDropDown_withValue(_sql, drpDiscountName, "DiscHeadName", "id");
                            drpDiscountName.SelectedValue = lblDiscountName.Text.Trim();
                            Label lblInstallmentId = (Label)GrdTransportFeeDetails.Rows[i].FindControl("lblInstallmentId");
                            LinkButton lnkUpdateTransport = (LinkButton)GrdTransportFeeDetails.Rows[i].FindControl("lnkUpdateTransport");
                            _sql = "select count(*) cnt from TransportFeeDeposit where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and InstallmentId=" + lblInstallmentId.Text + " and SrNo='" + srno + "'";
                            if (_oo.ReturnTag(_sql, "cnt") != "0")
                            {
                                lnkUpdateTransport.Text = "<i class='fa fa-lock'></i>";
                                lnkUpdateTransport.Enabled = false;
                            }
                        }
                    }
                    else
                    {
                        hdnTransportFeeDiscount.Visible = false;
                        GrdTransportFeeDetails.Visible = false;
                    }
                    countamount(GrdTransportFeeDetails);

                }
                catch (Exception)
                {
                    // ignored
                }

                try
                {

                    _sql = "Select dis.id, asr.Card, dis.SrNo, Name, mm.MonthName, DiscountFor, dis.DiscountName, mdh.DiscHeadName, InstallmentId, Amount, Remark, dis.SessionName, dis.BranchCode, dis.RecordedDate, UserName";
                    _sql = _sql + "  from ManualDiscount dis";
                    _sql = _sql + "  inner join AllStudentRecord_UDF('" + Session["SessionName"] + "', " + Session["BranchCode"] + ") asr on asr.SessionName = dis.SessionName  and dis.SrNo=asr.SrNo";
                    _sql = _sql + "  inner join ManualDiscountHeads mdh on mdh.id=dis.DiscountName and dis.SessionName = mdh.SessionName and mdh.BranchCode=dis.BranchCode";
                    _sql = _sql + "  inner join MonthMaster mm on mm.ClassId = asr.ClassId and mm.SessionName = asr.SessionName and mm.BranchCode = dis.BranchCode";
                    _sql = _sql + "  and dis.InstallmentId = mm.MonthId and convert(varchar, mm.CardType)=convert(varchar, asr.CardId) and case when asr.TypeofEducation='R' then 'Regular' else 'Private' end = mm.typeofAdd";
                    _sql = _sql + "  where dis.SrNo = '" + srno + "' and DiscountFor='Hostel Fee' and dis.ApplyFrom='Manual' and dis.BranchCode=" + Session["BranchCode"] + "";
                    GrdHostelFeeDetails.DataSource = _oo.GridFill(_sql);
                    GrdHostelFeeDetails.DataBind();
                    if (GrdHostelFeeDetails.Rows.Count > 0)
                    {
                        hdnHostelFeeDiscount.Visible = true;
                        GrdHostelFeeDetails.Visible = true;
                        for (int i = 0; i < GrdHostelFeeDetails.Rows.Count; i++)
                        {

                            Label lblDiscountName = (Label)GrdHostelFeeDetails.Rows[i].FindControl("lblDiscountName");
                            DropDownList drpDiscountName = (DropDownList)GrdHostelFeeDetails.Rows[i].FindControl("drpDiscountName");
                            _sql = "select id, DiscHeadName from ManualDiscountHeads where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " Order by id asc";
                            _oo.FillDropDown_withValue(_sql, drpDiscountName, "DiscHeadName", "id");
                            drpDiscountName.SelectedValue = lblDiscountName.Text.Trim();
                            Label lblInstallmentId = (Label)GrdHostelFeeDetails.Rows[i].FindControl("lblInstallmentId");
                            LinkButton lnkUpdateHostel = (LinkButton)GrdHostelFeeDetails.Rows[i].FindControl("lnkUpdateHostel");
                            _sql = "select count(*) cnt from HostelFeeDeposit where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and InstallmentId=" + lblInstallmentId.Text + " and SrNoOrEmpId='" + srno + "'";
                            if (_oo.ReturnTag(_sql, "cnt") != "0")
                            {
                                lnkUpdateHostel.Text = "<i class='fa fa-lock'></i>";
                                lnkUpdateHostel.Enabled = false;
                            }
                        }
                    }
                    else
                    {
                        hdnHostelFeeDiscount.Visible = false;
                        GrdHostelFeeDetails.Visible = false;
                    }
                    countamount(GrdHostelFeeDetails);

                }
                catch (Exception)
                {
                    // ignored
                }
                try
                {

                    _sql = "Select dis.id, asr.Card, dis.SrNo, Name, mm.MonthName, DiscountFor, dis.DiscountName, mdh.DiscHeadName, InstallmentId, Amount, Remark, dis.SessionName, dis.BranchCode, dis.RecordedDate, UserName";
                    _sql = _sql + "  from ManualDiscount dis";
                    _sql = _sql + "  inner join AllStudentRecord_UDF('" + Session["SessionName"] + "', " + Session["BranchCode"] + ") asr on asr.SessionName = dis.SessionName  and dis.SrNo=asr.SrNo";
                    _sql = _sql + "  inner join ManualDiscountHeads mdh on mdh.id=dis.DiscountName and dis.SessionName = mdh.SessionName and mdh.BranchCode=dis.BranchCode";
                    _sql = _sql + "  inner join MonthMaster mm on mm.ClassId = asr.ClassId and mm.SessionName = asr.SessionName and mm.BranchCode = dis.BranchCode";
                    _sql = _sql + "  and dis.InstallmentId = mm.MonthId and convert(varchar, mm.CardType)=convert(varchar, asr.CardId) and case when asr.TypeofEducation='R' then 'Regular' else 'Private' end = mm.typeofAdd";
                    _sql = _sql + "  where dis.SrNo = '" + srno + "' and DiscountFor='Miscellaneous Fee' and dis.ApplyFrom='Manual' and dis.BranchCode=" + Session["BranchCode"] + "";
                    GrdMiscellaneousFeeDetails.DataSource = _oo.GridFill(_sql);
                    GrdMiscellaneousFeeDetails.DataBind();
                    if (GrdMiscellaneousFeeDetails.Rows.Count > 0)
                    {
                        hdnMiscellaneousFeeDiscount.Visible = true;
                        GrdMiscellaneousFeeDetails.Visible = true;
                        for (int i = 0; i < GrdMiscellaneousFeeDetails.Rows.Count; i++)
                        {

                            Label lblDiscountName = (Label)GrdMiscellaneousFeeDetails.Rows[i].FindControl("lblDiscountName");
                            DropDownList drpDiscountName = (DropDownList)GrdMiscellaneousFeeDetails.Rows[i].FindControl("drpDiscountName");
                            _sql = "select id, DiscHeadName from ManualDiscountHeads where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " Order by id asc";
                            _oo.FillDropDown_withValue(_sql, drpDiscountName, "DiscHeadName", "id");
                            drpDiscountName.SelectedValue = lblDiscountName.Text.Trim();
                            Label lblInstallmentId = (Label)GrdMiscellaneousFeeDetails.Rows[i].FindControl("lblInstallmentId");
                            LinkButton lnkUpdateMiscellaneous = (LinkButton)GrdMiscellaneousFeeDetails.Rows[i].FindControl("lnkUpdateMiscellaneous");
                            _sql = "select count(*) cnt from MiscellaneousFeeDeposit where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and InstallmentId=" + lblInstallmentId.Text + " and SrNo='" + srno + "'";
                            if (_oo.ReturnTag(_sql, "cnt") != "0")
                            {
                                lnkUpdateMiscellaneous.Text = "<i class='fa fa-lock'></i>";
                                lnkUpdateMiscellaneous.Enabled = false;
                            }
                        }
                    }
                    else
                    {
                        hdnHostelFeeDiscount.Visible = false;
                        GrdMiscellaneousFeeDetails.Visible = false;
                    }
                    countamount(GrdMiscellaneousFeeDetails);

                }
                catch (Exception)
                {
                    // ignored
                }
            }
            if (DropDownList4.SelectedIndex == 1)
            {
                try
                {

                    _sql = "Select dis.id, asr.Card, dis.SrNo, Name, mm.MonthName, DiscountFor, dis.DiscountName, mdh.DiscHeadName, InstallmentId, Amount, Remark, dis.SessionName, dis.BranchCode, dis.RecordedDate, UserName";
                    _sql = _sql + "  from ManualDiscount dis";
                    _sql = _sql + "  inner join AllStudentRecord_UDF('" + Session["SessionName"] + "', " + Session["BranchCode"] + ") asr on asr.SessionName = dis.SessionName  and dis.SrNo=asr.SrNo";
                    _sql = _sql + "  inner join ManualDiscountHeads mdh on mdh.id=dis.DiscountName and dis.SessionName = mdh.SessionName and mdh.BranchCode=dis.BranchCode";
                    _sql = _sql + "  inner join MonthMaster mm on mm.ClassId = asr.ClassId and mm.SessionName = asr.SessionName and mm.BranchCode = dis.BranchCode";
                    _sql = _sql + "  and dis.InstallmentId = mm.MonthId and convert(varchar, mm.CardType)=convert(varchar, asr.CardId) and case when asr.TypeofEducation='R' then 'Regular' else 'Private' end = mm.typeofAdd";
                    _sql = _sql + "  where dis.SrNo = '" + srno + "' and DiscountFor='Tuition Fee' and dis.ApplyFrom='Manual' and dis.BranchCode=" + Session["BranchCode"] + "";
                    GrdTutionFeeDetails.DataSource = _oo.GridFill(_sql);
                    GrdTutionFeeDetails.DataBind();
                    if (GrdTutionFeeDetails.Rows.Count > 0)
                    {
                        hdnTutionFeeDiscount.Visible = true;
                        GrdTutionFeeDetails.Visible = true;
                        for (int i = 0; i < GrdTutionFeeDetails.Rows.Count; i++)
                        {

                            Label lblDiscountName = (Label)GrdTutionFeeDetails.Rows[i].FindControl("lblDiscountName");
                            DropDownList drpDiscountName = (DropDownList)GrdTutionFeeDetails.Rows[i].FindControl("drpDiscountName");
                            _sql = "select id, DiscHeadName from ManualDiscountHeads where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " Order by id asc";
                            _oo.FillDropDown_withValue(_sql, drpDiscountName, "DiscHeadName", "id");
                            drpDiscountName.SelectedValue = lblDiscountName.Text.Trim();
                            Label lblInstallmentId = (Label)GrdTutionFeeDetails.Rows[i].FindControl("lblInstallmentId");
                            LinkButton lnkUpdateTutionFee = (LinkButton)GrdTutionFeeDetails.Rows[i].FindControl("lnkUpdateTutionFee");
                            //_sql = "select count(*) cnt from TutionFeeDeposit where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and InstallmentId=" + lblInstallmentId.Text + " and SrNo='" + srno + "'";
                            //if (_oo.ReturnTag(_sql, "cnt") != "0")
                            //{
                            //    lnkUpdateTutionFee.Text = "<i class='fa fa-lock'></i>";
                            //    lnkUpdateTutionFee.Enabled = false;
                            //}
                        }
                    }
                    else
                    {
                        hdnTutionFeeDiscount.Visible = false;
                        GrdTutionFeeDetails.Visible = false;
                    }
                    countamount(GrdTutionFeeDetails);

                }
                catch (Exception ex)
                {
                    // ignored
                }

            }
            if (DropDownList4.SelectedIndex == 2)
            {
                try
                {

                    _sql = "Select dis.id, asr.Card, dis.SrNo, Name, mm.MonthName, DiscountFor, dis.DiscountName, mdh.DiscHeadName, InstallmentId, Amount, Remark, dis.SessionName, dis.BranchCode, dis.RecordedDate, UserName";
                    _sql = _sql + "  from ManualDiscount dis";
                    _sql = _sql + "  inner join AllStudentRecord_UDF('" + Session["SessionName"] + "', " + Session["BranchCode"] + ") asr on asr.SessionName = dis.SessionName  and dis.SrNo=asr.SrNo";
                    _sql = _sql + "  inner join ManualDiscountHeads mdh on mdh.id=dis.DiscountName and dis.SessionName = mdh.SessionName and mdh.BranchCode=dis.BranchCode";
                    _sql = _sql + "  inner join MonthMaster mm on mm.ClassId = asr.ClassId and mm.SessionName = asr.SessionName and mm.BranchCode = dis.BranchCode";
                    _sql = _sql + "  and dis.InstallmentId = mm.MonthId and convert(varchar, mm.CardType)=convert(varchar, asr.CardId) and case when asr.TypeofEducation='R' then 'Regular' else 'Private' end = mm.typeofAdd";
                    _sql = _sql + "  where dis.SrNo = '" + srno + "' and DiscountFor='Transport Fee' and dis.ApplyFrom='Manual' and dis.BranchCode=" + Session["BranchCode"] + "";
                    GrdTransportFeeDetails.DataSource = _oo.GridFill(_sql);
                    GrdTransportFeeDetails.DataBind();
                    if (GrdTransportFeeDetails.Rows.Count > 0)
                    {
                        hdnTransportFeeDiscount.Visible = true;
                        GrdTransportFeeDetails.Visible = true;
                        for (int i = 0; i < GrdTransportFeeDetails.Rows.Count; i++)
                        {

                            Label lblDiscountName = (Label)GrdTransportFeeDetails.Rows[i].FindControl("lblDiscountName");
                            DropDownList drpDiscountName = (DropDownList)GrdTransportFeeDetails.Rows[i].FindControl("drpDiscountName");
                            _sql = "select id, DiscHeadName from ManualDiscountHeads where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " Order by id asc";
                            _oo.FillDropDown_withValue(_sql, drpDiscountName, "DiscHeadName", "id");
                            drpDiscountName.SelectedValue = lblDiscountName.Text.Trim();
                            Label lblInstallmentId = (Label)GrdTransportFeeDetails.Rows[i].FindControl("lblInstallmentId");
                            LinkButton lnkUpdateTransport = (LinkButton)GrdTransportFeeDetails.Rows[i].FindControl("lnkUpdateTransport");
                            //_sql = "select count(*) cnt from TransportFeeDeposit where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and InstallmentId=" + lblInstallmentId.Text + " and SrNo='" + srno + "'";
                            //if (_oo.ReturnTag(_sql, "cnt") != "0")
                            //{
                            //    lnkUpdateTransport.Text = "<i class='fa fa-lock'></i>";
                            //    lnkUpdateTransport.Enabled = false;
                            //}
                        }
                    }
                    else
                    {
                        hdnTransportFeeDiscount.Visible = false;
                        GrdTransportFeeDetails.Visible = false;
                    }
                    countamount(GrdTransportFeeDetails);

                }
                catch (Exception)
                {
                    // ignored
                }

            }
            if (DropDownList4.SelectedIndex == 3)
            {
                try
                {

                    _sql = "Select dis.id, asr.Card, dis.SrNo, Name, mm.MonthName, DiscountFor, dis.DiscountName, mdh.DiscHeadName, InstallmentId, Amount, Remark, dis.SessionName, dis.BranchCode, dis.RecordedDate, UserName";
                    _sql = _sql + "  from ManualDiscount dis";
                    _sql = _sql + "  inner join AllStudentRecord_UDF('" + Session["SessionName"] + "', " + Session["BranchCode"] + ") asr on asr.SessionName = dis.SessionName  and dis.SrNo=asr.SrNo";
                    _sql = _sql + "  inner join ManualDiscountHeads mdh on mdh.id=dis.DiscountName and dis.SessionName = mdh.SessionName and mdh.BranchCode=dis.BranchCode";
                    _sql = _sql + "  inner join MonthMaster mm on mm.ClassId = asr.ClassId and mm.SessionName = asr.SessionName and mm.BranchCode = dis.BranchCode";
                    _sql = _sql + "  and dis.InstallmentId = mm.MonthId and convert(varchar, mm.CardType)=convert(varchar, asr.CardId) and case when asr.TypeofEducation='R' then 'Regular' else 'Private' end = mm.typeofAdd";
                    _sql = _sql + "  where dis.SrNo = '" + srno + "' and DiscountFor='Hostel Fee' and dis.ApplyFrom='Manual' and dis.BranchCode=" + Session["BranchCode"] + "";
                    GrdHostelFeeDetails.DataSource = _oo.GridFill(_sql);
                    GrdHostelFeeDetails.DataBind();
                    if (GrdHostelFeeDetails.Rows.Count > 0)
                    {
                        hdnHostelFeeDiscount.Visible = true;
                        GrdHostelFeeDetails.Visible = true;
                        for (int i = 0; i < GrdHostelFeeDetails.Rows.Count; i++)
                        {

                            Label lblDiscountName = (Label)GrdHostelFeeDetails.Rows[i].FindControl("lblDiscountName");
                            DropDownList drpDiscountName = (DropDownList)GrdHostelFeeDetails.Rows[i].FindControl("drpDiscountName");
                            _sql = "select id, DiscHeadName from ManualDiscountHeads where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " Order by id asc";
                            _oo.FillDropDown_withValue(_sql, drpDiscountName, "DiscHeadName", "id");
                            drpDiscountName.SelectedValue = lblDiscountName.Text.Trim();
                            Label lblInstallmentId = (Label)GrdHostelFeeDetails.Rows[i].FindControl("lblInstallmentId");
                            LinkButton lnkUpdateHostel = (LinkButton)GrdHostelFeeDetails.Rows[i].FindControl("lnkUpdateHostel");
                            //_sql = "select count(*) cnt from HostelFeeDeposit where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and InstallmentId=" + lblInstallmentId.Text + " and SrNoOrEmpId='" + srno + "'";
                            //if (_oo.ReturnTag(_sql, "cnt") != "0")
                            //{
                            //    lnkUpdateHostel.Text = "<i class='fa fa-lock'></i>";
                            //    lnkUpdateHostel.Enabled = false;
                            //}
                        }
                    }
                    else
                    {
                        hdnHostelFeeDiscount.Visible = false;
                        GrdHostelFeeDetails.Visible = false;
                    }
                    countamount(GrdHostelFeeDetails);

                }
                catch (Exception)
                {
                    // ignored
                }
            }

            if (DropDownList4.SelectedIndex == 4)
            {
                try
                {

                    _sql = "Select dis.id, asr.Card, dis.SrNo, Name, mm.MonthName, DiscountFor, dis.DiscountName, mdh.DiscHeadName, InstallmentId, Amount, Remark, dis.SessionName, dis.BranchCode, dis.RecordedDate, UserName";
                    _sql = _sql + "  from ManualDiscount dis";
                    _sql = _sql + "  inner join AllStudentRecord_UDF('" + Session["SessionName"] + "', " + Session["BranchCode"] + ") asr on asr.SessionName = dis.SessionName  and dis.SrNo=asr.SrNo";
                    _sql = _sql + "  inner join ManualDiscountHeads mdh on mdh.id=dis.DiscountName and dis.SessionName = mdh.SessionName and mdh.BranchCode=dis.BranchCode";
                    _sql = _sql + "  inner join MonthMaster mm on mm.ClassId = asr.ClassId and mm.SessionName = asr.SessionName and mm.BranchCode = dis.BranchCode";
                    _sql = _sql + "  and dis.InstallmentId = mm.MonthId and convert(varchar, mm.CardType)=convert(varchar, asr.CardId) and case when asr.TypeofEducation='R' then 'Regular' else 'Private' end = mm.typeofAdd";
                    _sql = _sql + "  where dis.SrNo = '" + srno + "' and DiscountFor='Miscellaneous Fee' and dis.ApplyFrom='Manual' and dis.BranchCode=" + Session["BranchCode"] + "";
                    GrdMiscellaneousFeeDetails.DataSource = _oo.GridFill(_sql);
                    GrdMiscellaneousFeeDetails.DataBind();
                    if (GrdMiscellaneousFeeDetails.Rows.Count > 0)
                    {
                        hdnMiscellaneousFeeDiscount.Visible = true;
                        GrdMiscellaneousFeeDetails.Visible = true;
                        for (int i = 0; i < GrdMiscellaneousFeeDetails.Rows.Count; i++)
                        {

                            Label lblDiscountName = (Label)GrdMiscellaneousFeeDetails.Rows[i].FindControl("lblDiscountName");
                            DropDownList drpDiscountName = (DropDownList)GrdMiscellaneousFeeDetails.Rows[i].FindControl("drpDiscountName");
                            _sql = "select id, DiscHeadName from ManualDiscountHeads where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " Order by id asc";
                            _oo.FillDropDown_withValue(_sql, drpDiscountName, "DiscHeadName", "id");
                            drpDiscountName.SelectedValue = lblDiscountName.Text.Trim();
                            Label lblInstallmentId = (Label)GrdMiscellaneousFeeDetails.Rows[i].FindControl("lblInstallmentId");
                            LinkButton lnkUpdateMiscellaneous = (LinkButton)GrdMiscellaneousFeeDetails.Rows[i].FindControl("lnkUpdateMiscellaneous");
                            //_sql = "select count(*) cnt from MiscellaneousFeeDeposit where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and InstallmentId=" + lblInstallmentId.Text + " and SrNo='" + srno + "'";
                            //if (_oo.ReturnTag(_sql, "cnt") != "0")
                            //{
                            //    lnkUpdateMiscellaneous.Text = "<i class='fa fa-lock'></i>";
                            //    lnkUpdateMiscellaneous.Enabled = false;
                            //}
                        }
                    }
                    else
                    {
                        hdnHostelFeeDiscount.Visible = false;
                        GrdMiscellaneousFeeDetails.Visible = false;
                    }
                    countamount(GrdMiscellaneousFeeDetails);

                }
                catch (Exception)
                {
                    // ignored
                }
            }

        }



        public void countamount(GridView Grd)
        {
            if (Grd.Rows.Count > 0)
            {
                double total = 0;
                for (int i = 0; i < Grd.Rows.Count; i++)
                {
                    Label txtAmount = (Label)Grd.Rows[i].FindControl("txtAmount");
                    string ss = "";
                    total = total + double.Parse(txtAmount.Text==""?"0": txtAmount.Text);
                }
                Label lblTotalDiscount = (Label)Grd.FooterRow.FindControl("lblTotalDiscount");
                lblTotalDiscount.Text = total.ToString("N", new CultureInfo("en-In"));
            }
        }

        
        protected void LinkButton3_Click(object sender, EventArgs e)
        {

            LinkButton link = (LinkButton)sender;
            Label lblId2 = (Label)link.NamingContainer.FindControl("Label13");
            string ss = lblId2.Text;
            lblvalue.Text = ss;
            Panel2_ModalPopupExtender.Show();
            Button8.Focus();
        }
        protected void GrdTutionFeeDetails_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        
        
        protected void Button4_Click(object sender, EventArgs e)
        {

        }

        protected void Button8_Click(object sender, EventArgs e)
        {

        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "ManualDiscountProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = _con;

            cmd.Parameters.AddWithValue("@id", lblvalue.Text);
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
            cmd.Parameters.AddWithValue("@Action", "delete");
            try
            {
                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
                filterDisplay();
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Deleted successfully.", "S");
            }
            catch (Exception) { }
        }
        public override void Dispose()
        {
            _con.Dispose();
            _oo.Dispose();
        }

        protected void DropDownList4_SelectedIndexChanged(object sender, EventArgs e)
        {
            string studentId = Request.Form[hfStudentId.UniqueID];
            if (studentId == string.Empty)
            {
                studentId = TxtEnter.Text.Trim();
            }
            _sql = "select SO.Card, CM.ClassName from StudentOfficialDetails SO";
            _sql = _sql + "   inner join ClassMaster CM on SO.AdmissionForClassId=CM.Id";
            _sql = _sql + "   where SO.srno=" + "'" + studentId + "' and SO.SessionName='" + Session["SessionName"] + "'";
            _sql = _sql + "   and cm.SessionName='" + Session["SessionName"] + "' and SO.Withdrwal is null and cm.BranchCode=" + Session["BranchCode"] + " and so.BranchCode=" + Session["BranchCode"] + "";
            string card = _oo.ReturnTag(_sql, "Card");
            string classname = _oo.ReturnTag(_sql, "ClassName");
            if (DropDownList4.SelectedValue != "")
            {
                _sql = "select MonthName,mm.MonthId Id, mm.MonthId as MonthId from MonthMaster mm";
                _sql = _sql + " inner join FeeGroupMaster fgm on (Case when ISNUMERIC(CardType)=1 THEN fgm.Id Else fgm.FeeGroupName End)=mm.CardType and fgm.SessionName=mm.SessionName";
                _sql = _sql + " inner join ClassMaster cm on cm.Id=mm.ClassId and cm.SessionName=mm.SessionName";
                _sql = _sql + " where fgm.FeeGroupName='" + card + "' and cm.ClassName='" + classname + "' and mm.SessionName='" + Session["SessionName"] + "' ";
                _sql = _sql + " and mm.BranchCode=" + Session["BranchCode"] + " and cm.BranchCode=" + Session["BranchCode"] + " and fgm.BranchCode=" + Session["BranchCode"] + "";
                if (DropDownList4.SelectedValue != "")
                {
                    _sql = _sql + " and mm.BranchCode=" + Session["BranchCode"] + " and mm.MonthId not in (select InstallmentId from ManualDiscount where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and DiscountFor= case when '" + DropDownList4.SelectedValue + "'='' then DiscountFor else '" + DropDownList4.SelectedValue + "' end and Srno='" + studentId + "') order by MonthId";

                }
            }
            else
            {
                _sql = "select MonthName,mm.MonthId Id, mm.MonthId as MonthId from MonthMaster mm where mm.MonthId=1.2";
                divdDiscountNameInsert.Visible = false;
                divRemark.Visible = false;
                divFillInstallment.Visible = false;
                LinkButton1.Visible = false;
            }
            
            if (_oo.GridFill(_sql) != null)
            {
                if (_oo.GridFill(_sql).Tables.Count > 0)
                {
                    if (_oo.GridFill(_sql).Tables[0].Rows.Count > 0)
                    {
                        Repeater1.DataSource = _oo.GridFill(_sql);
                        Repeater1.DataBind();
                        divdDiscountNameInsert.Visible = true;
                        divRemark.Visible = true;
                        divFillInstallment.Visible = true;
                        LinkButton1.Visible = true;

                    }
                    else
                    {
                        Repeater1.DataSource = null;
                        Repeater1.DataBind();
                        divdDiscountNameInsert.Visible = false;
                        divRemark.Visible = false;
                        divFillInstallment.Visible = false;
                        LinkButton1.Visible = false;
                    }
                }
            }
            filterDisplay(studentId);
        }

        

        

        protected void lnkYes_Click(object sender, EventArgs e)
        {
            string studentId = Request.Form[hfStudentId.UniqueID];
            if (studentId == string.Empty)
            {
                studentId = TxtEnter.Text.Trim();
            }
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "ManualDiscountProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = _con;
            cmd.Parameters.AddWithValue("@Amount", (lblAmount.Text.Trim().ToString() == "" ? "0" : lblAmount.Text.Trim().ToString()));
            cmd.Parameters.AddWithValue("@DiscountName", drpDiscountNamePanel.SelectedValue.Trim());
            cmd.Parameters.AddWithValue("@Remark", lblRemark.Text.Trim().ToString());
            cmd.Parameters.AddWithValue("@id", lblIds.Text.Trim());
            cmd.Parameters.AddWithValue("@SrNo", studentId.Trim());
            cmd.Parameters.AddWithValue("@InstallmentId", lblmonthtId.Text.Trim());
            cmd.Parameters.AddWithValue("@UserName", Session["LoginName"].ToString());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
            cmd.Parameters.AddWithValue("@Action", "update");
            try
            {
                _con.Open();
                cmd.ExecuteNonQuery();
                filterDisplay(studentId);
                _con.Close();
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Updated successfully.", "S");
            }
            catch (Exception) { }
        }



        protected void lnkUpdateTransport_Click(object sender, EventArgs e)
        {
            _sql = "select id, DiscHeadName from ManualDiscountHeads where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " Order by id asc";
            _oo.FillDropDown_withValue(_sql, drpDiscountNamePanel, "DiscHeadName", "id");
            LinkButton link = (LinkButton)sender;
            Label lblId = (Label)link.NamingContainer.FindControl("lblId");
            Label lblInstallmentId = (Label)link.NamingContainer.FindControl("lblInstallmentId");
            DropDownList drpDiscountName = (DropDownList)link.NamingContainer.FindControl("drpDiscountName");
            Label txtAmount = (Label)link.NamingContainer.FindControl("txtAmount");
            Label txtRemarks = (Label)link.NamingContainer.FindControl("txtRemarks");

            lblIds.Text = lblId.Text;
            lblAmount.Text = txtAmount.Text;
            drpDiscountNamePanel.SelectedValue = drpDiscountName.SelectedValue;
            lblRemark.Text = txtRemarks.Text;
            lblmonthtId.Text = lblInstallmentId.Text;

            Panel1_ModalPopupExtender.Show();
        }

        protected void lnkUpdateTutionFee_Click(object sender, EventArgs e)
        {
            _sql = "select id, DiscHeadName from ManualDiscountHeads where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " Order by id asc";
            _oo.FillDropDown_withValue(_sql, drpDiscountNamePanel, "DiscHeadName", "id");
            LinkButton link = (LinkButton)sender;
            Label lblId = (Label)link.NamingContainer.FindControl("lblId");
            Label lblInstallmentId = (Label)link.NamingContainer.FindControl("lblInstallmentId");
            DropDownList drpDiscountName = (DropDownList)link.NamingContainer.FindControl("drpDiscountName");
            Label txtAmount = (Label)link.NamingContainer.FindControl("txtAmount");
            Label txtRemarks = (Label)link.NamingContainer.FindControl("txtRemarks");

            lblIds.Text = lblId.Text;
            lblAmount.Text = txtAmount.Text;
            drpDiscountNamePanel.SelectedValue = drpDiscountName.SelectedValue;
            lblRemark.Text = txtRemarks.Text;
            lblmonthtId.Text = lblInstallmentId.Text;

            Panel1_ModalPopupExtender.Show();

            Panel1_ModalPopupExtender.Show();
        }

        protected void lnkUpdateHostel_Click(object sender, EventArgs e)
        {
            _sql = "select id, DiscHeadName from ManualDiscountHeads where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " Order by id asc";
            _oo.FillDropDown_withValue(_sql, drpDiscountNamePanel, "DiscHeadName", "id");
            LinkButton link = (LinkButton)sender;
            Label lblId = (Label)link.NamingContainer.FindControl("lblId");
            Label lblInstallmentId = (Label)link.NamingContainer.FindControl("lblInstallmentId");
            DropDownList drpDiscountName = (DropDownList)link.NamingContainer.FindControl("drpDiscountName");
            Label txtAmount = (Label)link.NamingContainer.FindControl("txtAmount");
            Label txtRemarks = (Label)link.NamingContainer.FindControl("txtRemarks");

            lblIds.Text = lblId.Text;
            lblAmount.Text = txtAmount.Text;
            drpDiscountNamePanel.SelectedValue = drpDiscountName.SelectedValue;
            lblRemark.Text = txtRemarks.Text;
            lblmonthtId.Text = lblInstallmentId.Text;

            Panel1_ModalPopupExtender.Show();
        }
        protected void lnkMiscellaneous_Click(object sender, EventArgs e)
        {
            _sql = "select id, DiscHeadName from ManualDiscountHeads where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " Order by id asc";
            _oo.FillDropDown_withValue(_sql, drpDiscountNamePanel, "DiscHeadName", "id");
            LinkButton link = (LinkButton)sender;
            Label lblId = (Label)link.NamingContainer.FindControl("lblId");
            Label lblInstallmentId = (Label)link.NamingContainer.FindControl("lblInstallmentId");
            DropDownList drpDiscountName = (DropDownList)link.NamingContainer.FindControl("drpDiscountName");
            Label txtAmount = (Label)link.NamingContainer.FindControl("txtAmount");
            Label txtRemarks = (Label)link.NamingContainer.FindControl("txtRemarks");

            lblIds.Text = lblId.Text;
            lblAmount.Text = txtAmount.Text;
            drpDiscountNamePanel.SelectedValue = drpDiscountName.SelectedValue;
            lblRemark.Text = txtRemarks.Text;
            lblmonthtId.Text = lblInstallmentId.Text;

            Panel1_ModalPopupExtender.Show();
        }
    }
}