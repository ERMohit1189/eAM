using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using c4SmsNew;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class register : System.Web.UI.Page
{
    private SqlConnection _con;
    private readonly Campus _oo;
    private string _sql, _mess = string.Empty;
    private DataTable _dt;
    public register()
    {
        _con = new SqlConnection();
        _oo = new Campus();
        _dt = new DataTable();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        _con = _oo.dbGet_connection();
        Page.ClientScript.RegisterStartupScript(GetType(), "myScript", "ClearTextboxes();", true);
        txtregmobile.Focus();
        if (!IsPostBack)
        {
            _sql = "select BranchId, BranchName from BranchTab";
            _oo.FillDropDown_withValue(_sql, DrpBranchName, "BranchName", "BranchId");
            DrpBranchName.Items.Insert(0, new ListItem("<--Select Branch-->", ""));
            DrpBranchName.SelectedValue = Session["BranchCode"].ToString();
            Head1.DataBind();
            string sql = "select phone,adminEmail from CollegeMaster where BranchCode=" + Session["BranchCode"].ToString();
            string str = "";
            if (_oo.ReturnTag(sql, "phone") != "" && _oo.ReturnTag(sql, "adminEmail") != "")
            {
                str = str + "<i class='fa fa-phone'></i>&nbsp;" + _oo.ReturnTag(sql, "phone") + " &nbsp;<i class='fa fa-envelope'></i>&nbsp;" + _oo.ReturnTag(sql, "adminEmail");
            }
            else if (_oo.ReturnTag(sql, "phone") != "" && _oo.ReturnTag(sql, "adminEmail") == "")
            {
                str = str + "<i class='fa fa-phone'></i>&nbsp;" + _oo.ReturnTag(sql, "phone");
            }
            else if (_oo.ReturnTag(sql, "phone") == "" && _oo.ReturnTag(sql, "adminEmail") != "")
            {
                str = str + "<i class='fa fa-envelope'></i>&nbsp;" + _oo.ReturnTag(sql, "adminEmail");
            }
            lblsupport.Text = str;
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@Queryfor", "S"));
            DataSet ds = new DataSet();
            ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("Get_GenralInfo", param);
            try
            {
                if (ds.Tables[3].Rows[0][0].ToString() != "")
                {
                    lblCompanyName.Visible = true;
                    Label2.Text = ds.Tables[3].Rows[0][0].ToString();
                }
            }
            catch (Exception)
            {
            }
        }
    }
    protected void linkLogin_Click(object sender, EventArgs e)
    {
        Session["BranchCode"] = DrpBranchName.SelectedValue;
        Response.Redirect("login.aspx");
    }
    protected void LinkButton1_OnClick(object sender, EventArgs e)
    {
        SetRegistration();
    }

    private void SetRegistration()
    {
        try
        {
            if (txtregpassword.Text.Trim() != txtConfirmpassword.Text.Trim())
            {
                _oo.msgbox(Page, msgbox, "Password not matched!", "W");
                return;
            }
            else
            {
                if (chktermsconditions.Checked && txtregmobile.Text.Trim() != "" && txtregpassword.Text.Trim() != "" && txtConfirmpassword.Text.Trim() != "")
                {
                    using (var cmd = new SqlCommand())
                    {
                        if (_con.State == ConnectionState.Closed)
                        {
                            _con.Open();
                        }
                        ViewState["mobileno"] = txtregmobile.Text.Trim();
                        cmd.CommandText = "USP_REGISTRATION_ADMISSION";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = _con;
                        cmd.Parameters.AddWithValue("@QueryFor", "I");
                        cmd.Parameters.AddWithValue("@MOBILE", ViewState["mobileno"].ToString().Trim());
                        cmd.Parameters.AddWithValue("@PASSWORD", txtregpassword.Text.Trim());
                        cmd.Parameters.Add("@msg", SqlDbType.VarChar, 50);
                        cmd.Parameters["@msg"].Direction = ParameterDirection.Output;
                        var n = cmd.ExecuteNonQuery();
                        if (n > 0)
                        {
                            Session["pass"] = txtregpassword.Text.Trim();
                            var otp = (string)cmd.Parameters["@msg"].Value;
                            var sadpNew = new SMSAdapterNew();
                            _mess = "Dear Applicant, Your Username is " + ViewState["mobileno"].ToString().Trim() + " and OTP is " + otp.Trim() + " in online admission portal. ";
                            if (txtregmobile.Text.Trim() != "")
                            {
                                try
                                {
                                    sadpNew.Send(_mess, txtregmobile.Text.Trim(), "34");
                                    Panel1_ModalPopupExtender.Show();
                                }
                                catch (Exception exc)
                                {
                                }
                            }
                        }
                    }
                }
                else
                {
                    _oo.msgbox(Page, msgbox, "Please, Check terms & conditions!", "A");
                }
            }
        }
        catch (Exception ex)
        {
        }
        finally
        {
            if (_con.State == ConnectionState.Open) { _con.Close(); }
        }
    }
    protected void btnsignin_OnClick(object sender, EventArgs e)
    {
        try
        {
            _sql = "select top 1 otp from REGISTRATION_ADMISSION where MOBILE='" + ViewState["mobileno"].ToString().Trim() + "' order by id desc";
            var otpChk = _oo.ReturnTag(_sql, "otp");
            if (txtotpno.Text.Trim() == otpChk)
            {
                if (txtotpno.Text.Trim() != "")
                {
                    var param = new List<SqlParameter>();
                    param.Add(new SqlParameter("@QueryFor", "U"));
                    param.Add(new SqlParameter("@ApprovOTP", "A"));
                    param.Add(new SqlParameter("@MOBILE", ViewState["mobileno"].ToString().Trim()));
                    param.Add(new SqlParameter("@NEWOTPCHK", txtotpno.Text.Trim()));
                    var msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("USP_REGISTRATION_ADMISSION", param);
                    if (msg == txtotpno.Text.Trim())
                    {
                        Session["UserMobile"]= ViewState["mobileno"].ToString().Trim();
                        Session["mobilenochk"] = ViewState["mobileno"].ToString().Trim();
                        Response.Redirect("../2/Gpaf.aspx?check=admintion&id=" + ViewState["mobileno"].ToString().Trim() + "", false);
                    }
                    else
                    {
                        _oo.msgbox(Page, msgbox, "OTP not matched!", "A");
                    }
                }
                else
                {
                    _oo.msgbox(Page, msgbox, "OTP not matched!", "A");
                }
            }
            else
            {
                _oo.msgbox(Page, msgbox, "OTP not matched!", "A");
            }
        }
        catch (Exception)
        {
        }
    }
    public override void Dispose()
    {
        _con.Dispose();
        _oo.Dispose();
        _dt.Dispose();
    }
}