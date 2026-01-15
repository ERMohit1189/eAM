using System;
using System.Collections.Generic;
using System.Web.UI;

using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using c4SmsNew;
using System.Threading;
public partial class ShowreportcardtoparentsICSE : Page
{
    readonly Campus _oo = new Campus();
    SqlConnection con = new SqlConnection();
    string sql = ""; string _sql = "";
    protected void Page_PreInIt(object sender, EventArgs e)
    {
        if (Session["Logintype"] == null) { Response.Redirect("~/default.aspx"); }
        if (Session["Logintype"].ToString() == "Admin")
        {
            this.MasterPageFile = "~/Master/admin_root-manager.master";
        }
        else if (Session["Logintype"].ToString() != "Staff")
        {
            this.MasterPageFile = "~/Staff/staff_root-manager.master";
        }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        con = _oo.dbGet_connection();
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        _oo.LoadLoader(loader);
        if (!IsPostBack)
        {
            loadclass();
            loadsection();
            BAL.objBal.fillSelectvalue(drpBranch, "<--Select-->", "-1");
        }
    }
    public void loadclass()
    {
        BLL.BLLInstance.loadClass(drpclass, Session["SessionName"].ToString());
    }
    public void loadsection()
    {
        sql = "Select SectionName,Id from SectionMaster where ClassNameId='" + drpclass.SelectedValue.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        BAL.objBal.FillDropDown_withValue(sql, drpsection, "SectionName", "Id");
        drpsection.Items.Insert(0, "<--Select-->");
    }
    protected void drpclass_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadsection();
    }
    protected void drpsection_SelectedIndexChanged(object sender, EventArgs e)
    {
        BLL.BLLInstance.loadBranch(drpBranch, Session["SessionName"].ToString(), drpclass.SelectedValue.ToString());
    }
    private void LoaddefaultCountry(DropDownList drp)
    {
        _sql = "Select CountryName,Id from CountryMaster";
        BAL.objBal.FillDropDown_withValue(_sql, drp, "CountryName", "id");
        using (var objBll = new BLL())
        {
            try
            {
                objBll.loadDefaultvalue("Country", drp);
            }
            catch
            {
                // ignored
            }
        }
    }
    private void LoaddefaultState(DropDownList drp, DropDownList drpValue)
    {
        drp.Items.Clear();
        _sql = "Select count(*) cnt from StateMaster where countryId='" + drpValue.SelectedValue + "'";
        if (_oo.ReturnTag(_sql, "cnt") == "0")
        {
            drp.Items.Add(new ListItem("Other", "0"));
        }
        else
        {
            _sql = "Select StateName,Id from StateMaster";
            BAL.objBal.FillDropDown_withValue(_sql, drp, "StateName", "id");
            using (var objBll = new BLL())
            {
                try
                {
                    objBll.loadDefaultvalue("State", drp);
                }
                catch
                {
                    // ignored
                }
            }
        }
    }
    private void LoaddefaultCity(DropDownList drp, DropDownList drpValue)
    {
        drp.Items.Clear();
        _sql = "Select count(*) cnt from CityMaster where StateId='" + drpValue.SelectedValue + "'";
        if (_oo.ReturnTag(_sql, "cnt") == "0")
        {
            drp.Items.Add(new ListItem("Other", "0"));
        }
        else
        {
            _sql = "Select CityName,id from CityMaster where StateId='" + drpValue.SelectedValue + "'";
            BAL.objBal.FillDropDown_withValue(_sql, drp, "CityName", "id");
            using (var objBll = new BLL())
            {
                try
                {
                    objBll.loadDefaultvalue("City", drp);
                }
                catch
                {
                    // ignored
                }
            }
        }
    }
    public void loadStudents()
    {
        string BranchId = "", SectionId = "";
        if (drpBranch.SelectedIndex != 0)
        {
            BranchId = drpBranch.SelectedValue;
        }
        if (drpsection.SelectedIndex != 0)
        {
            SectionId = drpsection.SelectedValue;
        }
        sql = "select asr.SrNo, asr.Name, asr.FatherName, asr.CombineClassName from AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "', " + Session["BranchCode"].ToString() + ") asr";
        sql = sql + " where asr.ClassId =" + drpclass.SelectedValue + " and asr.BranchId=case when " + BranchId + "='' then asr.BranchId else " + BranchId + " end  and asr.SectionID = case when " + SectionId + "='' then asr.SectionID else " + SectionId + " end and isnull(Withdrwal,'')='' and isnull(blocked, '')='' order by asr.Name asc";
        rptStudents.DataSource = BAL.objBal.GridFill(sql);
        rptStudents.DataBind();
        if (rptStudents.Items.Count > 0)
        {
            divExport.Visible = true;
            LinkUpdate.Visible = true;
            for (int i = 0; i < rptStudents.Items.Count; i++)
            {
                Label LblSrno = (Label)rptStudents.Items[i].FindControl("LblSrno");
                sql = "select distinct srno from ICSEShowReportCardToGarduin where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and SrNo='"+ LblSrno.Text + "' and status='1'";
                CheckBox CheckBox = (CheckBox)rptStudents.Items[i].FindControl("chk");
                if (_oo.Duplicate(sql))
                {
                    CheckBox.Checked = true;
                }
                else
                {
                    CheckBox.Checked = false;
                }
            }
        }
        else
        {
            divExport.Visible = false;
            LinkUpdate.Visible = false;
        }
    }

    protected void LinkView_Click(object sender, EventArgs e)
    {
        loadStudents();
    }

    protected void LinkUpdate_Click(object sender, EventArgs e)
    {
        if (rptStudents.Items.Count > 0)
        {
            for (int i = 0; i < rptStudents.Items.Count; i++)
            {
                string sts = "0";
                Label LblSrno = (Label)rptStudents.Items[i].FindControl("LblSrno");
                CheckBox CheckBox = (CheckBox)rptStudents.Items[i].FindControl("chk");
                if (CheckBox.Checked)
                {
                    sts = "1";
                }
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "ICSEShowReportCardToGarduinProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@SrNo", LblSrno.Text.Trim());
                cmd.Parameters.AddWithValue("@Status", sts);
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                cmd.Connection = con;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    
                   
                }
                catch (SqlException)
                {
                }
            }
            
            loadStudents();
            _oo.msgbox(Page, msgbox, "Submitted successfully.", "S");
        }
    }

    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        if (chkAll.Checked)
        {
            for (int i = 0; i < rptStudents.Items.Count; i++)
            {
                CheckBox CheckBox = (CheckBox)rptStudents.Items[i].FindControl("chk");
                CheckBox.Checked = true;
            }
        }
        else
        {
            for (int i = 0; i < rptStudents.Items.Count; i++)
            {
                CheckBox CheckBox = (CheckBox)rptStudents.Items[i].FindControl("chk");
                CheckBox.Checked = false;
            }
        }
        
    }
}