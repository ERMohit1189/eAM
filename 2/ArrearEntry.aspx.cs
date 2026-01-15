using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI.WebControls;

public partial class ArrearEntry : System.Web.UI.Page
{
    private SqlConnection _con;
    private readonly Campus _oo;
    private string _sql = "";
    private string _s;
    public ArrearEntry()
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
            BLL.BLLInstance.loadClass(drpClass,Session["SessionName"].ToString());
            BLL.BLLInstance.loadSection(drpSection, Session["SessionName"].ToString(), drpClass.SelectedValue.ToString());
            BLL.BLLInstance.loadBranch(drpBranch, Session["SessionName"].ToString(), drpClass.SelectedValue.ToString());
        }
    }

    protected void drpClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        BLL.BLLInstance.loadSection(drpSection, Session["SessionName"].ToString(), drpClass.SelectedValue.ToString());
    }

    protected void drpSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        BLL.BLLInstance.loadBranch(drpBranch, Session["SessionName"].ToString(), drpClass.SelectedValue.ToString());
    }

    protected void drpBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadGrid();
    }

    protected void drpAdmissionType_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadGrid();
    }
    public void loadGrid()
    {

        _sql = "Select asr.SrNo,asr.StEnRCode,asr.Name,asr.FatherName ";
        _sql = _sql + " from AllStudentRecord_UDF('"+ Session["SessionName"] + "', " + Session["BranchCode"] + ") asr";
        _sql = _sql + " where asr.ClassId = " + drpClass.SelectedValue+ " and asr.BranchId = " + drpBranch.SelectedValue+ " and asr.SectionId = " + drpSection.SelectedValue+ " ";
        _sql = _sql + " and isnull(TypeOFAdmision,'')='Old' and asr.SrNo not in (";
        _sql = _sql + " select distinct SrNO from CompositFeeDeposit where BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and installmentid=0)";
        grd.DataSource = _oo.GridFill(_sql);
        grd.DataBind();
        if (grd.Rows.Count>0)
        {
            DropDownList ddlTutionHead = (DropDownList)grd.HeaderRow.FindControl("ddlTutionHead");
            DropDownList ddlTransportHead = (DropDownList)grd.HeaderRow.FindControl("ddlTransportHead");
            DropDownList ddlHostelHead = (DropDownList)grd.HeaderRow.FindControl("ddlHostelHead");
            string str = "select id, FeeHead from FeeHeadMaster where FeeType ='Tuition Fee' and BranchCode=" + Session["BranchCode"] + "";
            _oo.FillDropDown_withValue(str, ddlTutionHead, "FeeHead", "id");
            string str2 = "select id, FeeHead from FeeHeadMaster where FeeType ='Transport Fee' and BranchCode=" + Session["BranchCode"] + "";
            _oo.FillDropDown_withValue(str2, ddlTransportHead, "FeeHead", "id");
            string str3 = "select id, FeeHead from FeeHeadMaster where FeeType ='Hostel Fee' and BranchCode=" + Session["BranchCode"] + "";
            _oo.FillDropDown_withValue(str3, ddlHostelHead, "FeeHead", "id");
            for (int i = 0; i < grd.Rows.Count; i++)
            {
                if (!_oo.Duplicate(str))
                {
                    TextBox txtTutionfeeArrear = (TextBox)grd.Rows[i].FindControl("txtTutionfeeArrear");
                    txtTutionfeeArrear.Enabled = false;
                }
                if (!_oo.Duplicate(str2))
                {
                    TextBox txtTransportArrear = (TextBox)grd.Rows[i].FindControl("txtTransportArrear");
                    txtTransportArrear.Enabled = false;
                }
                if (!_oo.Duplicate(str3))
                {
                    TextBox txtHostelArrear = (TextBox)grd.Rows[i].FindControl("txtHostelArrear");
                    txtHostelArrear.Enabled = false;
                }
            }
            
            lnkSubmit.Visible = true;
        }
        else
        {
            lnkSubmit.Visible = false;
        }
    }
    
    protected void lnkSubmit_Click(object sender, EventArgs e)
    {
        int sts = 0;
        using (SqlCommand cmd = new SqlCommand())
        {
            for (int i = 0; i < grd.Rows.Count; i++)
            {
                Label lblsrno = (Label)grd.Rows[i].FindControl("lblSrno");
                TextBox txtTutionfeeArrear = (TextBox)grd.Rows[i].FindControl("txtTutionfeeArrear");
                TextBox txtTransportArrear = (TextBox)grd.Rows[i].FindControl("txtTransportArrear");
                TextBox txtHostelArrear = (TextBox)grd.Rows[i].FindControl("txtHostelArrear");
                DropDownList ddlTutionHead = (DropDownList)grd.HeaderRow.FindControl("ddlTutionHead");
                DropDownList ddlTransportHead = (DropDownList)grd.HeaderRow.FindControl("ddlTransportHead");
                DropDownList ddlHostelHead = (DropDownList)grd.HeaderRow.FindControl("ddlHostelHead");


                double TutionfeeArrear = 0;
                double.TryParse(txtTutionfeeArrear.Text, out TutionfeeArrear);

                double TransportArrear = 0;
                double.TryParse(txtTransportArrear.Text, out TransportArrear);

                double HostelArrear = 0;
                double.TryParse(txtHostelArrear.Text, out HostelArrear);
                if (TutionfeeArrear > 0)
                {
                    cmd.CommandText = "SetArriarEntryProc";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = _con;
                    cmd.Parameters.AddWithValue("@Srno", lblsrno.Text);
                    cmd.Parameters.AddWithValue("@FeeHaedId", ddlTutionHead.SelectedValue);
                    cmd.Parameters.AddWithValue("@Amount", TutionfeeArrear.ToString("0.00"));
                    cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                    cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                    cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                    try
                    {
                        _con.Open();
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        _con.Close();
                        sts = sts + 1;
                    }
                    catch (Exception ex) { }
                }
                if (TransportArrear > 0)
                {
                    cmd.CommandText = "SetArriarEntryProc";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = _con;
                    cmd.Parameters.AddWithValue("@Srno", lblsrno.Text);
                    cmd.Parameters.AddWithValue("@FeeHaedId", ddlTransportHead.SelectedValue);
                    cmd.Parameters.AddWithValue("@Amount", TransportArrear.ToString("0.00"));
                    cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                    cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                    cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                    try
                    {
                        _con.Open();
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        _con.Close();
                        sts = sts + 1;
                    }
                    catch (Exception ex) { }
                }
                if (HostelArrear > 0)
                {
                    cmd.CommandText = "SetArriarEntryProc";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = _con;
                    cmd.Parameters.AddWithValue("@Srno", lblsrno.Text);
                    cmd.Parameters.AddWithValue("@FeeHaedId", ddlHostelHead.SelectedValue);
                    cmd.Parameters.AddWithValue("@Amount", HostelArrear.ToString("0.00"));
                    cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                    cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                    cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                    try
                    {
                        _con.Open();
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        _con.Close();
                        sts = sts + 1;
                    }
                    catch (Exception ex) { }
                }
            }
        }
        loadGrid();
        if (sts>0)
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");
        }
    }
    public override void Dispose()
    {
        _con.Dispose();
        _oo.Dispose();
    }
}