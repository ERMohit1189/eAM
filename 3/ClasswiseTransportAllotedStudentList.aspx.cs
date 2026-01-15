using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;

public partial class admin_VehicleNoWiseTransportAllotedStudentList : Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        //con.ConnectionString = ConfigurationManager.ConnectionStrings["Menu"].ConnectionString;

        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file
        BLL.BLLInstance.LoadHeader("Report", header);
        if (!IsPostBack)
        {
            abc.Visible = false;
            loadVehicle();
            loadClass();
            ddlClass.Items.Insert(0, new ListItem("<--Select-->", ""));
            ddlBranch.Items.Insert(0, new ListItem("<--Select-->", ""));
            ddlInstallment.Items.Insert(0, new ListItem("<--Select-->", ""));
            ddlLocation.Items.Insert(0, new ListItem("<--Select-->", ""));
            ddlsection.Items.Insert(0, new ListItem("<--Select-->", ""));
            sql = "Select FeeGroupName,Id from FeeGroupMaster";
            sql = sql + "  where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

            oo.FillDropDown_withValue(sql, drpCardType, "FeeGroupName", "Id");
        }
    }
    
    protected void loadVehicle()
    {
        sql = "SELECT Id, VehicleNo+ ' ( '+ Driver+' )' VehicleNo FROM VehicleDetails where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and id in (select distinct VehicleId from locationMapping where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + ")  ORDER by VehicleNo";
        oo.FillDropDown_withValue(sql, ddlVehicle, "VehicleNo", "Id");
        ddlVehicle.Items.Insert(0, new ListItem("<--Select-->", ""));
    }
    protected void ddlVehicle_SelectedIndexChanged(object sender, EventArgs e)
    {
        sql = "SELECT locationId, locationName+ ' ( '+ CONVERT(nvarchar(max), distanceInKm)+' )' location FROM LocationMaster where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and locationId in (select distinct locationId from locationMapping where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and VehicleId=" + ddlVehicle.SelectedValue + ") ORDER by locationId asc";
        oo.FillDropDown_withValue(sql, ddlLocation, "location", "locationId");
        ddlLocation.Items.Insert(0, new ListItem("<--Select-->", "0"));
    }
    
    protected void loadClass()
    {
        sql = "Select ClassName,Id from ClassMaster";
        sql = sql + "  where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        sql = sql + " order by CIDOrder";
        oo.FillDropDown_withValue(sql, ddlClass, "ClassName", "Id");
        ddlClass.Items.Insert(0, new ListItem("<--Select-->", ""));
    }
    protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadInstallMent();
        loadSection();
        loadBranch();
    }
    protected void loadInstallMent()
    {
        sql = "Select monthname,monthId from MonthMaster";
        sql = sql + "  where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and ClassId=" + ddlClass.SelectedValue.ToString()+"";
        sql = sql + " and cardtype="+ drpCardType.SelectedValue + " order by monthid";
        oo.FillDropDown_withValue(sql, ddlInstallment, "monthName", "monthid");
        ddlInstallment.Items.Insert(0, new ListItem("<--Select-->", ""));
    }
    protected void loadSection()
    {
        sql = "Select SectionName,id from SectionMaster where ClassNameId=" + ddlClass.SelectedValue.ToString();
        sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        oo.FillDropDown_withValue(sql, ddlsection, "SectionName", "Id");
        ddlsection.Items.Insert(0, new ListItem("<--Select-->", ""));
    }
    protected void loadBranch()
    {
        sql = "Select BranchName,id from BranchMaster where ClassId=" + ddlClass.SelectedValue.ToString();
        sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        oo.FillDropDown_withValue(sql, ddlBranch, "BranchName", "Id");
        ddlBranch.Items.Insert(0, new ListItem("<--Select-->", ""));
    }
    private void loadGrid()
    {
        List<SqlParameter> param = new List<SqlParameter>();
        param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
        string str = "";
        if (ddlsection.SelectedIndex!=0)
        {
            param.Add(new SqlParameter("@SectionName", ddlsection.SelectedItem.Text.ToString()));
        }
        if (ddlBranch.SelectedIndex != 0)
        {
            param.Add(new SqlParameter("@BranchId", ddlBranch.SelectedValue.ToString()));
        }
        if (ddlInstallment.SelectedIndex != 0)
        {
            str = str + "Installment : "+ ddlInstallment.SelectedItem.Text;
            param.Add(new SqlParameter("@InstallmentId", ddlInstallment.SelectedValue.ToString()));
        }
        if (ddlVehicle.SelectedIndex != 0)
        {
            param.Add(new SqlParameter("@VehicleId", ddlVehicle.SelectedValue.ToString()));
            str = str + " Vehicle : " + ddlVehicle.SelectedItem.Text;
        }
        if (ddlLocation.SelectedIndex != 0)
        {
            param.Add(new SqlParameter("@LocationId", ddlLocation.SelectedValue.ToString()));
            str = str + " Location : " + ddlLocation.SelectedItem.Text;
        }
        param.Add(new SqlParameter("@ClassId", ddlClass.SelectedValue.ToString()));
        str = str + " Class : " + ddlClass.SelectedItem.Text;
        hding.Text = str;
        string ss = "select format(getdate(), 'dd-MMM-yyyy hh:mm:ss tt') genratedOn";
        string ss2 = "select name+' ('+UserId+')' GeneatedBy from NewAdminInformation where UserId='" + Session["LoginName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        lblGenBy.Text = "Generated by " + oo.ReturnTag(ss2, "GeneatedBy") + " on " + oo.ReturnTag(ss, "genratedOn");
        DataSet ds = new DataSet();
        ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("ListOftransportAllotedStudent", param);

        GridView1.DataSource = ds;
        GridView1.DataBind();
           
    }

    protected void LinkButton6_Click(object sender, EventArgs e)
    {
        loadGrid();
        if (GridView1.Rows.Count == 0)
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, No Record(s) found!", "A");

            printbox.Visible = false;
            ImageButton1.Visible = false;
            ImageButton2.Visible = false;
            ImageButton3.Visible = false;
            ImageButton4.Visible = false;
            abc.Visible = false;
        }
        else
        {
            printbox.Visible = true;
            ImageButton1.Visible = true;
            ImageButton2.Visible = true;
            ImageButton3.Visible = true;
            ImageButton4.Visible = true;
            abc.Visible = true;
            HideRow();
        }
    }

    private void HideRow()
    {
        if (GridView1.Rows.Count > 0)
        {
            GridView1.HeaderRow.Cells[5].Visible = false;
            GridView1.HeaderRow.CssClass = "vd_bg-grey-ll vd_white text-center";
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridView1.Rows[i].Cells[5].Visible = false;
            }
        }
 
    }

    
    
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        HideRow();
        oo.ExportToWord(Response, "ListofTransportAllotedStudent.doc", gdv);
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        HideRow();
        oo.ExportToExcel("ListofTransportAllotedStudent.xls", GridView1);
    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        HideRow();
        PrintHelper_New.ctrl = abc;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {

    }

    
}