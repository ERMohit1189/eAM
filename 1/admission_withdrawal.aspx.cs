using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminAdmissionWithdrawal : Page
{
    private SqlConnection _con;
    private readonly Campus _oo;
    private string _sql = "";

    public AdminAdmissionWithdrawal()
    {
        _con = new SqlConnection();
        _oo = new Campus();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        drpEnter.Focus();

        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        _con = _oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file
        BLL.BLLInstance.LoadHeader("Report", header);
        if (IsPostBack) return;
        try
        {
            DisplayReocord(true);
        }
        catch (Exception)
        {
            // ignored
        }

        _oo.AddDateMonthYearDropDown(drpYY, DrpMM, DrpDD);
        _oo.FindCurrentDateandSetinDropDown(drpYY, DrpMM, DrpDD);
        _oo.AddDateMonthYearDropDown(drpYYPanel, DrpMMPanel, DrpDDPanel);
        _oo.FindCurrentDateandSetinDropDown(drpYYPanel, DrpMMPanel, DrpDDPanel);
        Panel1.Visible = false;
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void txtEnter_TextChanged(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void BindGrid()
    {

        var studentId = Request.Form[hfStudentId.UniqueID];
        if (studentId == string.Empty)
        {
            studentId = txtEnter.Text.Trim();
        }
        _sql = "Select id,SrNo,StEnRCode,Name as StudentName,FatherName,ClassName, combineclassname,SectionName,Medium,Card,Convert(varchar(11),DateOfAdmiission) as DateOfAdmiission,CourseName,BranchName,FamilyContactNo,PhotoPath";
        _sql = _sql + " from AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") asr where srno='" + studentId + "'";

        GridView1.DataSource = _oo.GridFill(_sql);
        GridView1.DataBind();
        DataSet ds;
        ds = _oo.GridFill(_sql);

        // ReSharper disable once UseNullPropagation
        if (ds != null && GridView1.Rows.Count > 0)
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
            var _sql2 = "select isnull(Withdrwal, '') Withdrwal from StudentOfficialDetails where srno='" + studentId + "' and SessionName='" + Session["SessionName"] + "' and branchcode=" + Session["BranchCode"] + "";
            if (_oo.ReturnTag(_sql2, "Withdrwal").Trim() != "")
            {
                GridView1.Rows[0].BackColor = Color.Red;
                GridView1.ForeColor = Color.White;

                var remark = "This Student has been Withdrawn!";
                mess.Text = remark; divMess.Visible = true;
            }
        }
        else
        {
            grdshow.Visible = false;
        }
        Panel1.Visible = true;
        if (GridView1.Rows.Count == 0)
        {
            //oo.MessageBox("Sorry, No Record(s) found!", this.Page);
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Sorry, No Record(s) found!", "A");

            Panel1.Visible = false;
        }
        else
        {
            //if (CheckArrierAmount() > 0)
            //{
            //    Panel1.Visible = false;
            //    divlist.Visible = false;
            //    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Sorry, Please deposit outstanding amount first!", "A");
            //}
            //else
            //{
            var withdraw = _oo.ReturnTag(_sql, "Status");
            if (withdraw == "Withdrawal")
            {
                DisplayReocord(false);
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Already Withdrawn!", "A");
            }
            else
            {
                DisplayReocord(false);
            }
            //}
        }
    }
    protected double CheckArrierAmount()
    {
        string topSessionName = "";
        double val = 0;
        var studentId = Request.Form[hfStudentId.UniqueID];
        if (studentId == string.Empty)
        {
            studentId = txtEnter.Text.Trim();
        }
        _sql = "select Top 1 SessionName from StudentOfficialDetails where srno='" + studentId + "' and BranchCode=" + Session["BranchCode"] + " and ISNULL(Promotion,'')<>'Cancelled' order By Id Desc";
        if (_oo.Duplicate(_sql))
        {
            topSessionName = _oo.ReturnTag(_sql, "SessionName");
        }
        var param = new List<SqlParameter>();
        param.Add(new SqlParameter("@SrNo", studentId));
        param.Add(new SqlParameter("@SessionName", topSessionName));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
        var ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("sp_CheckArrearForTC", param);
        val = double.Parse((ds.Tables[0].Rows[0]["BalanceAmount"].ToString() == "" ? "0" : ds.Tables[0].Rows[0]["BalanceAmount"].ToString()));
        return val;
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        var studentId = Request.Form[hfStudentId.UniqueID];
        if (studentId == string.Empty)
        {
            studentId = txtEnter.Text.Trim();
        }


        _sql = "select StEnRCode,SrNo from StudentOfficialDetails ";
        if (drpEnter.SelectedValue == "srno")
        {
            _sql = _sql + " where srno='" + studentId + "'";
            _sql = _sql + " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";

            _oo.ReturnTag(_sql, "StEnRCode");
        }

        else
        {
            _sql = _sql + " where StEnRCode='" + studentId + "'";
            _sql = _sql + " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            _oo.ReturnTag(_sql, "SrNo");
        }

        _sql = "select stEnRcode from StudentWithdrawal where " + drpEnter.SelectedValue + "='" + studentId + "'";
        _sql = _sql + " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        if (_oo.Duplicate(_sql))
        {
            //oo.MessageBox("Withdrawn already!", this.Page);
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Withdrawn already!", "A");
            Panel1.Visible = false;
        }
        else
        {


            var dd = drpYY.SelectedItem + "/" + DrpMM.SelectedItem + "/" + DrpDD.SelectedItem;

            var srLabel = (Label)GridView1.Rows[0].FindControl("Label1");
            var stenrLabel = (Label)GridView1.Rows[0].FindControl("Label14");
            var cmd = new SqlCommand
            {
                CommandText = "StudentWithDrawalProc",
                CommandType = CommandType.StoredProcedure,
                Connection = _con
            };
            cmd.Parameters.AddWithValue("@Srno", srLabel.Text);
            cmd.Parameters.AddWithValue("@stEnRcode", stenrLabel.Text);
            cmd.Parameters.AddWithValue("@withdrawalDate", dd);
            cmd.Parameters.AddWithValue("@remark", txtReason.Text);
            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
            cmd.Parameters.AddWithValue("@Status", DropDownList2.SelectedItem.Text);
            try
            {

                var withdrawlStore = new SqlCommand("Update StudentOfficialDetails Set Withdrwal='W' where SrNo='" + srLabel.Text + "' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "", _con);
                _con.Open();
                cmd.ExecuteNonQuery();
                withdrawlStore.ExecuteNonQuery();
                string srno = "G" + srLabel.Text.Trim();
                _sql = "update LoginTab set IsActive=0 where LoginName='" + srLabel.Text.Trim() + "' and BranchId=" + Session["BranchCode"] + " and Logintypeid=4";
                _oo.ProcedureDatabase(_sql);
                _sql = "update LoginTab set IsActive=0 where LoginName='" + srno + "' and BranchId=" + Session["BranchCode"] + " and Logintypeid=5";
                _oo.ProcedureDatabase(_sql);
                LinkLoadAll.Visible = true;
                _con.Close();
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Withdrawal successfully.", "S");
                GridView1.DataSource = null;
                GridView1.DataBind();
                Panel1.Visible = false;
                _oo.ClearControls(Page);
                grdshow.Visible = false;
                DisplayReocord(false);
            }
            catch (SqlException) { }
        }
    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }



    public void DisplayReocord(bool isall)
    {

        string studentId = "";
        if (!isall)
        {
            studentId = Request.Form[hfStudentId.UniqueID];
            if (String.IsNullOrEmpty(studentId))
            {
                studentId = txtEnter.Text.Trim();
            }
        }
        int flg = 0;
        _sql = "   Select WithdrawalId, SF.SectionName,SF.CombineClassName, Sw.status Passstatus, ";
        _sql = _sql + " convert(nvarchar,SF.DateOfAdmiission,106) as DateOfAdmiission ,   SF.BranchName,  ";
        _sql = _sql + " SF.SectionId,SF.Card,SF.Medium,SF.FatherName,SF.MotherName,SF.Name,SF.StEnRCode as StEnRCode, ";
        _sql = _sql + " SF.srno  as srno,SF.SessionName,sw.Remark as Remark,case when  sw.withdrawaldate is null then 'None' else Convert(nvarchar,sw.withdrawaldate,106) end as withdrawaldate, case when sw.WithdrawalDate Is null  then 'None' else 'Withdrawal' end as Status, format(Sw.RecordDate, 'dd-MMM-yyyy hh:mm:ss tt')RecordDate, Sw.LoginName ";
        _sql = _sql + " from StudentWithdrawal Sw ";
        _sql = _sql + " inner join AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") SF on Sf.srno=Sw.srno and Sf.BranchCode=Sw.BranchCode  and Sf.SessionName=Sw.SessionName  ";
        _sql = _sql + " where Sw.SessionName='" + Session["SessionName"] + "' and Sw.BranchCode=" + Session["BranchCode"] + "";
        if (studentId.ToString() != "")
        {
            _sql = _sql + " and Sw.srno = '" + studentId + "' ";
        }
        else
        {
            if (LinkLoadAll.Visible)
            {
                LinkLoadAll.Visible = false;
                flg = 1;
            }
        }
        _sql = _sql + "  order by Sw.RecordDate desc ";
        GridView2.DataSource = _oo.GridFill(_sql);
        GridView2.DataBind();
        if (GridView2.Rows.Count > 0)
        {
            Panel1.Visible = false;
            divlist.Visible = true;
            string sqls = "select format(getdate(), 'dd MMM yyyy hh:mm:ss tt') dates";
            heading.Text = "Student Withdrawal Report | Date : " + _oo.ReturnTag(sqls, "dates");
        }
        else
        {
            Panel1.Visible = true;
            divlist.Visible = false;
            if (flg > 0)
            {
                divlist.Visible = true;
            }
        }

    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        var dd = drpYYPanel.SelectedItem + "/" + DrpMMPanel.SelectedItem + "/" + DrpDDPanel.SelectedItem;

        _sql = "update StudentWithdrawal set WithdrawalDate='" + dd + "' ,Remark='" + txtRemarkPanel.Text + "',Status='" + drpStatusPanel2.SelectedItem.Text.Trim() + "'";
        _sql = _sql + "    where  SrNo=" + "'" + lblID.Text + "'";
        _sql = _sql + "  and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        _oo.ProcedureDatabase(_sql);
        Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Updated successfully.", "S");
        DisplayReocord(false);
        Panel1.Visible = false;

    }

    protected void Button4_Click(object sender, EventArgs e)
    {

    }

    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        var chk = (LinkButton)sender;
        var lblId = (Label)chk.NamingContainer.FindControl("Label36");
        var ss = lblId.Text;
        lblID.Text = ss;

        _sql = "select status,WithdrawalDate,Remark,left(convert(nvarchar,WithdrawalDate,106),2) as DD, Right(left(convert(nvarchar,WithdrawalDate,106),6),3) as MM , RIGHT(convert(nvarchar,WithdrawalDate,106),4) as YY from StudentWithdrawal ";
        _sql = _sql + "  where  srno='" + ss + "'";
        _sql = _sql + "  and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";

        try
        {


            drpYYPanel.Text = _oo.ReturnTag(_sql, "yy");
            DrpMMPanel.Text = _oo.ReturnTag(_sql, "mm");
            DrpDDPanel.Text = _oo.ReadDD(_oo.ReturnTag(_sql, "dd"));
        }
        catch
        {
            // ignored
        }

        txtRemarkPanel.Text = _oo.ReturnTag(_sql, "Remark");
        drpStatusPanel2.SelectedValue = _oo.ReturnTag(_sql, "status");
        Panel2_ModalPopupExtender.Show();
    }
    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        Button2.Focus();
        var chk = (LinkButton)sender;
        var lblId = (Label)chk.NamingContainer.FindControl("Label37");
        var ss = lblId.Text;
        lblID.Text = ss;

        _sql = "select WithdrawalDate,Remark,left(convert(nvarchar,WithdrawalDate,106),2) as DD, Right(left(convert(nvarchar,WithdrawalDate,106),6),3) as MM , RIGHT(convert(nvarchar,WithdrawalDate,106),4) as YY from StudentWithdrawal ";
        _sql = _sql + "    where  Srno=" + "'" + ss + "'";
        _sql = _sql + "  and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";



        TextBox2.Text = _oo.ReturnTag(_sql, "Remark");


        Panel3_ModalPopupExtender.Show();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        switch (DropDownList3.SelectedItem.ToString())
        {
            case "Yes":
                _sql = "update StudentFamilyDetails  set Withdrwal=Null where SrNo='" + lblID.Text.Trim() + "' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                _oo.ProcedureDatabase(_sql);
                _sql = "update StudentGenaralDetail   set Withdrwal=Null where SrNo='" + lblID.Text.Trim() + "' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                _oo.ProcedureDatabase(_sql);
                _sql = " update StudentOfficialDetails  set Withdrwal=Null where SrNo='" + lblID.Text.Trim() + "' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                _oo.ProcedureDatabase(_sql);
                _sql = "update StudentPreviousSchool  set Withdrwal=Null where SrNo='" + lblID.Text.Trim() + "' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                _oo.ProcedureDatabase(_sql);
                _sql = "delete from StudentWithdrawal where Srno='" + lblID.Text.Trim() + "' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                _oo.ProcedureDatabase(_sql);

                string srno = "G" + lblID.Text.Trim();
                _sql = "update LoginTab set IsActive=1 where LoginName='" + lblID.Text.Trim() + "' and BranchId=" + Session["BranchCode"] + " and Logintypeid=4";
                _oo.ProcedureDatabase(_sql);
                _sql = "update LoginTab set IsActive=1 where LoginName='" + srno + "' and BranchId=" + Session["BranchCode"] + " and Logintypeid=5";
                _oo.ProcedureDatabase(_sql);


                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Updated successfully.", "S");
                DisplayReocord(false);
                Panel1.Visible = false;
                break;
        }
    }
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        _oo.ExportTolandscapeWord(Response, "DueDepositBalanceReport", gdv1);
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        _oo.ExportDivToExcelWithFormatting(Response, "ListOfStudents.xls", gdv1, Server.MapPath("~/Admin/css/style.css"));
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {
        _oo.ExporttolandscapePdf(Response, "DueDepositBalanceReport", abc);
    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = abc;
        ClientScript.RegisterStartupScript(GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }
    public override void Dispose()
    {
        _con.Dispose();
        _oo.Dispose();
    }

    protected void LinkLoadAll_Click(object sender, EventArgs e)
    {
        hfStudentId.Value = "";
        txtEnter.Text = "";
        DisplayReocord(true);
    }
}
