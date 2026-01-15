using c4SmsNew;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AlumniMaster : Page
{
    SqlConnection con;
    Campus oo = new Campus();
    string sql = "";
    protected void Page_PreInIt(object sender, EventArgs e)
    {
        if (Session["Logintype"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (Session["Logintype"].ToString() != "Admin")
        {
            Response.Redirect("~/default.aspx");
        }

    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        con = new SqlConnection();
        con = BAL.objBal.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);
        BLL.BLLInstance.LoadHeader("Report", header);
        if (!IsPostBack)
        {
            sql = " with yearlist as ";
            sql = sql + " ( ";
            sql = sql + " select 1950 as year ";
            sql = sql + " union all ";
            sql = sql + " select yl.year + 1 as year ";
            sql = sql + " from yearlist yl ";
            sql = sql + " where yl.year + 1 <= YEAR(GetDate()) ";
            sql = sql + " ) ";

            sql = sql + "  select year from yearlist order by year desc; ";
            BAL.objBal.FillDropDown_withValue(sql, drpLastYearAttended, "year", "year");
            drpLastYearAttended1.Items.Insert(0, new ListItem("<--Select-->", ""));
            BAL.objBal.FillDropDown_withValue(sql, drpLastYearAttended1, "year", "year");
            drpLastYearAttended.Items.Insert(0, new ListItem("<--Select-->", ""));
            BAL.objBal.FillDropDown_withValue(sql, drpYearOfPostGraduation, "year", "year");
            drpYearOfPostGraduation.Items.Insert(0, new ListItem("<--Select-->", ""));
            BAL.objBal.FillDropDown_withValue(sql, drpYearofOthers, "year", "year");
            drpYearofOthers.Items.Insert(0, new ListItem("<--Select-->", ""));
            BAL.objBal.FillDropDown_withValue(sql, drpYearOfGraduation, "year", "year");
            drpYearOfGraduation.Items.Insert(0, new ListItem("<--Select-->", ""));
            sql = "Select CountryName,Id from CountryMaster";
            BAL.objBal.FillDropDown_withValue(sql, drpCountry, "CountryName", "id");
            drpCountry.Items.Insert(0, new ListItem("<--Select-->", ""));
            drpState.Items.Insert(0, new ListItem("<--Select-->", ""));
            drpCity.Items.Insert(0, new ListItem("<--Select-->", ""));
            LoadReport();
        }
    }
    
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        
        LoadReport();
    }
    protected void LoadReport()
    {
        List<SqlParameter> param = new List<SqlParameter>();
        if (drpLastYearAttended.SelectedValue != "")
        {
            param.Add(new SqlParameter("@LastAttendedYear", drpLastYearAttended.SelectedValue));
        }
        if (txtContactNo.Text != "")
        {
            param.Add(new SqlParameter("@ContactNo", txtContactNo.Text.Trim()));
        }
        if (txtEmail.Text != "")
        {
            param.Add(new SqlParameter("@Email", txtEmail.Text.Trim()));
        }
        param.Add(new SqlParameter("@Status", drpStatus.SelectedValue));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"]));
        param.Add(new SqlParameter("@Action", "Select"));
        var ds = new DLL().Sp_SelectRecord_usingExecuteDataset("Sp_AlumniRegistration", param);
        if (ds != null && ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                Panel2.Visible = true;
                abc.Visible = true;
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
            else
            {
                Panel2.Visible = false;
                abc.Visible = false;
            }
        }
    }
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        oo.ExportTolandscapeWord(Response, "AlumniReport", gdv1);
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        oo.ExportDivToExcelWithFormatting(Response, "AlumniReport.xls", gdv1, Server.MapPath("~/Admin/css/style.css"));
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {
        oo.ExporttolandscapePdf(Response, "AlumniReport", abc);
    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = abc;
        ClientScript.RegisterStartupScript(GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }

    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        
        CheckBox chk = (CheckBox)sender;
        CheckBox chkAll = (CheckBox)chk.NamingContainer.FindControl("chkAll");
        int cnt = 0;
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            if (chk.Checked)
            {
                cnt = cnt + 1;
                CheckBox chks = (CheckBox)GridView1.Rows[i].FindControl("chk");
                chks.Checked = true;
            }
            else
            {
                CheckBox chks = (CheckBox)GridView1.Rows[i].FindControl("chk");
                chks.Checked = false;
            }
        }
        if (cnt > 0)
        {
            LinkButton1.Visible = true;
        }
        else
        {
            LinkButton1.Visible = false;
        }
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "makeGrid();", true);
    }

    protected void chk_CheckedChanged(object sender, EventArgs e)
    {
        
        CheckBox chk = (CheckBox)sender;
        CheckBox chkAll = (CheckBox)GridView1.HeaderRow.FindControl("chkAll");
        int cnt = 0;
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            CheckBox chks = (CheckBox)GridView1.Rows[i].FindControl("chk");
            if (chks.Checked)
            {
                cnt = cnt + 1;
            }
        }
        if (cnt == GridView1.Rows.Count-1)
        {
            chkAll.Checked = true;
            
        }
        else
        {
            chkAll.Checked = false;
        }
        if (cnt>0)
        {
            LinkButton1.Visible = true;
        }
        else
        {
            LinkButton1.Visible = false;
        }
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "makeGrid();", true);
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        int cnt = 0;
        if (GridView1.Rows.Count > 0)
        {
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                CheckBox Chk = (CheckBox)GridView1.Rows[i].FindControl("Chk");
                DropDownList drpStatusI = (DropDownList)GridView1.Rows[i].FindControl("drpStatusI");
                if (Chk.Checked && drpStatusI.SelectedValue != "")
                {
                    using (var cmd = new SqlCommand())
                    {
                        Label lblContactNo = (Label)GridView1.Rows[i].FindControl("lblContactNo");
                        Label id = (Label)GridView1.Rows[i].FindControl("id");
                        cmd.CommandText = "Sp_AlumniRegistration";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        var pwd = Campus.GenerateRandomNo(8);
                        cmd.Parameters.AddWithValue("@Status", drpStatusI.SelectedValue);
                        cmd.Parameters.AddWithValue("@id", id.Text);
                        cmd.Parameters.AddWithValue("@Password", pwd);
                        cmd.Parameters.AddWithValue("@UpdatedBy", Session["LoginName"]);
                        cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"]);
                        cmd.Parameters.AddWithValue("@Action", "UpdateStatus");
                        try
                        {
                            con.Open();
                            int x = cmd.ExecuteNonQuery();
                            con.Close();
                            cmd.Parameters.Clear();
                            cnt = cnt + 1;
                            if (drpStatusI.SelectedValue!="Inactive")
                            {
                                SendFeesSms(lblContactNo.Text, drpStatusI.SelectedValue, drpStatusI.SelectedValue);
                            }
                            LoadReport();
                        }
                        catch (SqlException EX)
                        {
                            con.Close();
                        }
                        catch (Exception)
                        {
                            con.Close();
                        }
                    }
                }
            }
            if (cnt > 0)
            {
                LoadReport();
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Submitted successfully.", "S");
            }
            else
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Please select atleast one.", "A");
            }
        }
    }
    public void SendFeesSms(string MobileNo, string status, string Pass)
    {
        SMSAdapterNew sadpNew = new SMSAdapterNew();
        string mess = "";
        if (status=="Active")
        {
            mess = "Your request for Alumni Portal has approved. Login credentials for Alumni portal is: Username: "+ MobileNo + " & Password: "+ Pass + " ";
            sadpNew.Send(mess, MobileNo, "39");
        }
        if (status == "Rejected")
        {
            mess = "Your request for Alumni Portal has rejected. Please contact at Alumni Helpline numbers. ";
            sadpNew.Send(mess, MobileNo, "40");
        }
    }

    protected void lnkview_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        GridViewRow currentrow = (GridViewRow)lnk.NamingContainer;
        Label lblid = (Label)lnk.NamingContainer.FindControl("id");
        Label lblContactNo = (Label)lnk.NamingContainer.FindControl("lblContactNo");
        LoadData(lblContactNo.Text, lblid.Text);
        Panel1_ModalPopupExtender.Show();
    }
    protected void LoadData(string txtContactNo1, string id)
    {

        sql = "select id,'../Alumni/'+RecentPhoto as RecentPhoto,* from AlumniRegistration where ContactNo='" + txtContactNo1 + "' and id="+ id + "";
        if (oo.Duplicate(sql))
        {
            txtFname.Text = oo.ReturnTag(sql, "Fname");
            txtMname.Text = oo.ReturnTag(sql, "mname");
            txtLname.Text = oo.ReturnTag(sql, "lname");
            txtDob.Text = oo.ReturnTag(sql, "dob");
            drpGender.SelectedValue = oo.ReturnTag(sql, "gender");
            txtLastClassAttended.Text = oo.ReturnTag(sql, "LastAttendedClass");
            drpLastYearAttended.SelectedValue = oo.ReturnTag(sql, "LastAttendedYear");
            txtEmail2.Text = oo.ReturnTag(sql, "Email");
            txtContact2.Text = oo.ReturnTag(sql, "ContactNo");
            txtPassword.Text = oo.ReturnTag(sql, "Password");
            txtAadhaarNo.Text = oo.ReturnTag(sql, "AadhaarNo");
            txtGraduation.Text = oo.ReturnTag(sql, "Graduation");
            drpYearOfGraduation.SelectedValue = oo.ReturnTag(sql, "GraduationYear");
            txtPostGraduation.Text = oo.ReturnTag(sql, "PostGraduation");
            drpYearOfPostGraduation.SelectedValue = oo.ReturnTag(sql, "PostGraduationYear");
            txtOthers.Text = oo.ReturnTag(sql, "Other");
            drpYearofOthers.SelectedValue = oo.ReturnTag(sql, "OtherYear");
            txtCurrentOccupation.Text = oo.ReturnTag(sql, "CurrentOccupation");
            drpMaritalStatus.SelectedValue = oo.ReturnTag(sql, "MaritalStatus");
            txtAddress.Text = oo.ReturnTag(sql, "CurrentAddress");
            drpCountry.SelectedValue = oo.ReturnTag(sql, "Country");
            string sql1 = "Select StateName,Id from StateMaster where CountryId='" + drpCountry.SelectedValue + "'";
            drpState.Items.Clear();
            BAL.objBal.FillDropDown_withValue(sql1, drpState, "StateName", "id");
            drpState.Items.Insert(0, new ListItem("<--Select-->", ""));
            if (!oo.Duplicate(sql1))
            {
                drpState.Items.Insert(1, new ListItem("Other", "0"));
                drpState.SelectedValue = "0";
            }
            else
            {
                drpState.SelectedValue = oo.ReturnTag(sql, "state"); ;
            }
            string sql2 = "Select CityName,Id from CityMaster where StateId='" + drpState.SelectedValue + "'";
            drpCity.Items.Clear();
            BAL.objBal.FillDropDown_withValue(sql2, drpCity, "CityName", "id");
            drpCity.Items.Insert(0, new ListItem("<--Select-->", ""));
            if (!oo.Duplicate(sql1))
            {
                drpCity.Items.Insert(1, new ListItem("Other", "0"));
                drpCity.SelectedValue = "0";
            }
            else
            {
                drpCity.SelectedValue = oo.ReturnTag(sql, "city"); ;
            }
            imgPhoto.ImageUrl = oo.ReturnTag(sql, "RecentPhoto");
        }
    }
    protected void drpStatusH_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList drpStatusH = (DropDownList)GridView1.HeaderRow.FindControl("drpStatusH");
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            DropDownList drpStatusI = (DropDownList)GridView1.Rows[i].FindControl("drpStatusI");
            drpStatusI.SelectedValue = drpStatusH.SelectedValue;
        }
    }
    
}