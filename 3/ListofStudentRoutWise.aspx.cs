using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;

public partial class ListofStudentRoutWise : Page
{
    Campus oo = new Campus();
    string sql = "";

    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file
        BLL.BLLInstance.LoadHeader("Report", header1);
        //MakeGridViewPrinterFriendly(GridView1);
        if (!IsPostBack)
        {
            gdv.Visible = false;
            BindMonth();
            loadClass();

            sql = "Select VehicleType from VehicleMaster where BranchCode=" + Session["BranchCode"].ToString() + "";
            oo.FillDropDown_withValue(sql, drpVehicleType, "VehicleType", "VehicleType");
            drpVehicleType.Items.Insert(0, new ListItem("<--Select-->", ""));

            sql = "Select id, VehicleNo from VehicleDetails where BranchCode=" + Session["BranchCode"].ToString() + "";
            oo.FillDropDown_withValue(sql, drpVehicleNo, "VehicleNo", "id");
            drpVehicleNo.Items.Insert(0, new ListItem("<--Select-->", ""));

            sql = " select distinct m.locationName, m.locationId from locationMapping lm ";
            sql +=  " inner join LocationMaster m on lm.locationId=m.locationId and lm.SessionName=m.SessionName and lm.BranchCode=m.BranchCode ";
            sql +=  " where lm.SessionName='" + Session["SessionName"].ToString() + "' and lm.BranchCode=" + Session["BranchCode"].ToString() + " ";
            oo.FillDropDown_withValue(sql, drpLocation, "locationName", "locationId");
            drpLocation.Items.Insert(0, new ListItem("<--Select-->", ""));


            drpSection.Items.Insert(0, new ListItem("<--Select-->", ""));
            drpBranch.Items.Insert(0, new ListItem("<--Select-->", ""));

        }
    }


    protected void loadClass()
    {
        sql = "Select ClassName,Id from ClassMaster";
        sql +=  "  where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        sql +=  " order by CIDOrder";
        oo.FillDropDown_withValue(sql, drpClass, "ClassName", "Id");
        drpClass.Items.Insert(0, new ListItem("<--Select-->", ""));
    }

    protected void drpClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        sql = "Select SectionName,Id from SectionMaster";
        sql +=  "  where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        sql +=  " and ClassNameId=" + drpClass.SelectedValue + "";
        oo.FillDropDown_withValue(sql, drpSection, "SectionName", "Id");
        drpSection.Items.Insert(0, new ListItem("<--Select-->", ""));

        sql = "Select BranchName,Id from BranchMaster";
        sql +=  "  where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        sql +=  " and ClassId=" + drpClass.SelectedValue + "";
        oo.FillDropDown_withValue(sql, drpBranch, "BranchName", "Id");
        drpBranch.Items.Insert(0, new ListItem("<--Select-->", ""));
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        sql = "Select id, VehicleNo from VehicleDetails where VehicleType=case when '" + drpVehicleType.SelectedValue + "'='' then VehicleType else '" + drpVehicleType.SelectedValue + "' end and BranchCode=" + Session["BranchCode"].ToString() + "";
        oo.FillDropDown_withValue(sql, drpVehicleNo, "VehicleNo", "id");
        drpVehicleNo.Items.Insert(0, new ListItem("<--Select-->", ""));
    }
    protected void drpVehicleNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        sql = " select  distinct m.locationName, m.locationId from locationMapping lm ";
        sql +=  " inner join LocationMaster m on lm.locationId=m.locationId and lm.SessionName=m.SessionName and lm.BranchCode=m.BranchCode ";
        sql +=  " where lm.VehicleId=" + drpVehicleNo.SelectedValue + " and lm.SessionName='" + Session["SessionName"].ToString() + "' and lm.BranchCode=" + Session["BranchCode"].ToString() + " ";
        oo.FillDropDown_withValue(sql, drpLocation, "locationName", "locationId");
        drpLocation.Items.Insert(0, new ListItem("<--Select-->", ""));
    }


    protected void BindMonth()
    {
        sql = "Select MonthName Month,Convert(date,id,106) StartingDate, CONVERT(date, duedt) duedt, duedt1 From(Select Distinct Left(DateName(MONTH, StartingDate), 3) Month,StartingDate Id, MonthName,CONVERT(date, duedate) duedt, format(duedate,'dd-MMM-yyyy') duedt1 from MonthMaster Where SessionName = '" + Session["SessionName"].ToString() + "' and  BranchCode = " + Session["BranchCode"].ToString() + ")T1 order by CONVERT(date, duedt) Asc";

        oo.FillDropDown_withValue(sql, drpInstallments, "Month", "duedt1");

        drpInstallments.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
    }

    private void loadGrid()
    {
        DataSet ds = new DataSet();
        List<SqlParameter> param = new List<SqlParameter>();
        param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
        if (drpClass.SelectedValue != "")
        {
            param.Add(new SqlParameter("@ClassId", drpClass.SelectedValue));
        }
        if (drpSection.SelectedValue != "")
        {
            param.Add(new SqlParameter("@SectionId", drpSection.SelectedValue));
        }
        if (drpBranch.SelectedValue != "")
        {
            param.Add(new SqlParameter("@BranchId", drpBranch.SelectedValue));
        }
        if (drpVehicleType.SelectedValue != "")
        {
            param.Add(new SqlParameter("@VehicleType", drpVehicleType.SelectedValue));
        }
        if (drpVehicleNo.SelectedValue != "")
        {
            param.Add(new SqlParameter("@VehicleId", drpVehicleNo.SelectedValue));
        }
        if (drpLocation.SelectedValue != "")
        {
            param.Add(new SqlParameter("@LocationId", drpLocation.SelectedValue));
        }
        param.Add(new SqlParameter("@StartingDate", drpInstallments.SelectedValue.ToString()));
        ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("USP_getListofStudentRoutwise", param);

        GridView1.DataSource = ds;
        GridView1.DataBind();
        if (GridView1.Rows.Count > 0)
        {
            lblMonth.Text = drpInstallments.SelectedItem.Text;
            if (drpVehicleNo.SelectedIndex>0)
            {
                Label3.Visible = true;
                lblVehicle.Visible = true;
                lblVehicle.Text = drpVehicleNo.SelectedItem.Text;
            }
            else
            {
                Label3.Visible = false;
                lblVehicle.Visible = false;
            }
            divExport.Visible = true;
            gdv.Visible = true;
            double due = 0;
            double deposit = 0;
            double balance = 0;
            double Inst = 0;
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                Label lblDue = (Label)GridView1.Rows[i].FindControl("lblDue");
                Label lblInst = (Label)GridView1.Rows[i].FindControl("lblInst");
                Label lblDeposit = (Label)GridView1.Rows[i].FindControl("lblDeposit");
                Label lblBalance = (Label)GridView1.Rows[i].FindControl("lblBalance");
                due = due + double.Parse(lblDue.Text);
                Inst = Inst + double.Parse(lblInst.Text);
                deposit = deposit + double.Parse(lblDeposit.Text);
                balance = balance + double.Parse(lblBalance.Text);
            }
            Label lblDueF = (Label)GridView1.FooterRow.FindControl("lblDueF");
            Label lblInstF = (Label)GridView1.FooterRow.FindControl("lblInstF");
            Label lblDepositF = (Label)GridView1.FooterRow.FindControl("lblDepositF");
            Label lblBalanceF = (Label)GridView1.FooterRow.FindControl("lblBalanceF");
            lblDueF.Text = due.ToString("0.00");
           lblInstF.Text = Inst.ToString("0.00");
            lblDepositF.Text = deposit.ToString("0.00");
            lblBalanceF.Text = balance.ToString("0.00");
        }
        else
        {
            gdv.Visible = false;
            divExport.Visible = false;
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, No Record(s) found!", "A");
        }
    }

    protected void LinkButton6_Click(object sender, EventArgs e)
     {
        loadGrid();
    }

    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        //HideRow();
        oo.ExportToWord(Response, "ListofStudentRoutwise.doc", gdv);
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        //HideRow();
        oo.ExportToExcel("ListofStudentRoutwise.xls", GridView1);
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {
        oo.ExporttoPdf(Response, "ListofStudentRoutwise", gdv);
    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        //HideRow();
        PrintHelper_New.ctrl = gdv;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }
    private void MakeGridViewPrinterFriendly(GridView gridView)
    {
        if (gridView.Rows.Count > 0)
        {
            gridView.UseAccessibleHeader = true;
            gridView.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    protected void drpInstallments_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}