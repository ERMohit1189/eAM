using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
public partial class deleteattendance : System.Web.UI.Page
{
    private SqlConnection _con;
    private readonly Campus oo;
    private string sql = string.Empty;
    private DataTable _dt;
    public deleteattendance()
    {
        _con = new SqlConnection();
        oo = new Campus();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        _con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file
        if (!IsPostBack)
        {
            loadclass();
            DrpAtteClass.Items.Insert(0, new ListItem("<--Select-->", "0"));
            drpBranch.Items.Insert(0, new ListItem("<--Select-->", "0"));
            drpsection.Items.Insert(0, new ListItem("<--Select-->", "0"));
            oo.AddDateMonthYearDropDown(DrpSaal, DrpMahina, DrpDin);
            oo.FindCurrentDateandSetinDropDown(DrpSaal, DrpMahina, DrpDin);
            oo.AddDateMonthYearDropDown(DrpSaalTo, DrpMahinaTo, DrpDinTo);
            oo.FindCurrentDateandSetinDropDown(DrpSaalTo, DrpMahinaTo, DrpDinTo);
        }
    }
    protected void DrpSaal_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.YearDropDown(DrpSaal, DrpMahina, DrpDin);
    }
    protected void DrpMahina_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.MonthDropDown(DrpSaal, DrpMahina, DrpDin);
    }
    protected void DrpSaalTo_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.YearDropDown(DrpSaalTo, DrpMahinaTo, DrpDinTo);
    }
    protected void DrpMahinaTo_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.MonthDropDown(DrpSaalTo, DrpMahinaTo, DrpDinTo);
    }
    public void loadclass()
    {
        sql = "Select Distinct ClassName,cm.Id as Id,CIDOrder from ClassMaster cm ";
        sql = sql + " where cm.SessionName='" + Session["SessionName"] + "' and cm.BranchCode=" + Session["BranchCode"] + " Order by CIDOrder";
        oo.FillDropDown_withValue(sql, DrpAtteClass, "ClassName", "Id");
        DrpAtteClass.Items.Insert(0, new ListItem("<--Select-->", "0"));
    }
    private void loadBranch()
    {
        sql = "Select BranchName,Id from BranchMaster where ClassId='" + DrpAtteClass.SelectedValue.ToString() + "'";
        sql = sql + " and   BranchCode=" + Session["BranchCode"].ToString() + " and SessionName='" + Session["SessionName"].ToString() + "'";
        oo.FillDropDown_withValue(sql, drpBranch, "BranchName", "Id");
        drpBranch.Items.Insert(0, new ListItem("<--Select-->", "0"));
    }

    public void loadsection()
    {
        sql = "Select SectionName,id from SectionMaster where ClassNameId='" + DrpAtteClass.SelectedValue.ToString() + "' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"].ToString() + "'";
        oo.FillDropDown_withValue(sql, drpsection, "SectionName", "Id");
        drpsection.Items.Insert(0, new ListItem("<--Select-->", "0"));
    }

    protected void DrpAtteClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadBranch();
    }
    protected void drpBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadsection();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Button8.Focus();
        Panel2_ModalPopupExtender.Show();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string from = (int.Parse(DrpDin.SelectedItem.Text)<10?"0"+ DrpDin.SelectedItem.Text: DrpDin.SelectedItem.Text) + "-" + DrpMahina.SelectedItem.Text + "-" + DrpSaal.SelectedItem.Text;
        string to = (int.Parse(DrpDinTo.SelectedItem.Text) < 10 ? "0" + DrpDinTo.SelectedItem.Text : DrpDinTo.SelectedItem.Text) + "-" + DrpMahinaTo.SelectedItem.Text + "-" + DrpSaalTo.SelectedItem.Text;
        sql = "select case when convert(date, '" + from + "') > convert(date, '" + to + "') then 0 else 1 end isInvalid";
        if (oo.ReturnTag(sql, "isInvalid") == "0")
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Invalid date range!", "A");
            return;
        }
        sql = "SELECT DATEDIFF(day, '" + from + "', '" + to + "') AS DateDiff;";
        if (int.Parse(oo.ReturnTag(sql, "DateDiff")) > 15)
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Date range must be 15 days!", "A");
            return;
        }
        sql = " select AttendanceDate from AttendanceDetailsDateWise where convert(date, AttendanceDate) between convert(date, '" + from + "') and convert(date, '" + to + "') and BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and ClassName='"+DrpAtteClass.SelectedItem.Text+ "' and SectionName='" + drpsection.SelectedItem.Text + "' and srno in (select srno from AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") where ClassId=" + DrpAtteClass.SelectedValue + " and SectionId=" + drpsection.SelectedValue + " and BranchId=" + drpBranch.SelectedValue + ")";
        if (oo.Duplicate(sql))
        {
            string ssql = " delete from AttendanceDetailsDateWise where convert(date, AttendanceDate) between convert(date, '" + from + "') and convert(date, '" + to + "') and BranchCode =" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and ClassName='" + DrpAtteClass.SelectedItem.Text + "' and SectionName='" + drpsection.SelectedItem.Text + "' and srno in (select srno from AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") where ClassId=" + DrpAtteClass.SelectedValue + " and SectionId=" + drpsection.SelectedValue + " and BranchId=" + drpBranch.SelectedValue + ")";
            using (SqlCommand cmds = new SqlCommand())
            {
                cmds.CommandText = ssql;
                cmds.CommandType = CommandType.Text;
                cmds.Connection = _con;
                try
                {
                    _con.Open();
                    cmds.ExecuteNonQuery();
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Deleted successfully!", "S");
                    _con.Close();
                }
                catch (Exception)
                {
                }
            }
        }
        else
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "No record(s) found!", "A");
        }

    }
    protected void Button8_Click(object sender, EventArgs e)
    {

    }

    public override void Dispose()
    {
        _con.Dispose();
        oo.Dispose();
    }
}