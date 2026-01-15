using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class common_FeeReportofClassWiseAllStudent : System.Web.UI.Page
{
    SqlConnection con;
    protected void Page_PreInIt(object sender, EventArgs e)
    {
        if (Session["Logintype"] == null) { Response.Redirect("~/default.aspx"); }
        if (Session["Logintype"].ToString() == "Admin")
        {
            this.MasterPageFile = "~/Master/admin_root-manager.master";
        }
        else if (Session["Logintype"].ToString() == "Staff")
        {
            this.MasterPageFile = "~/Staff/staff_root-manager.master";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        con = new SqlConnection();
        con = BAL.objBal.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file

        if (!IsPostBack)
        {
            loadClass(drpClass);
            BAL.objBal.fillSelectvalue(drpSection, "<--Select-->", "-1");
            BAL.objBal.fillSelectvalue(drpBranch, "<--Select-->", "-1");
            BAL.objBal.fillSelectvalue(drpSrno, "<--Select-->", "<--Select-->");
        }
    }

    private void loadClass(DropDownList drpclass)
    {
        if (Session["logintype"].ToString() == "Admin")
        {
            BLL.BLLInstance.loadClass(drpClass, Session["SessionName"].ToString());
        }
        else
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
            param.Add(new SqlParameter("@EmpCode", Session["LoginName"].ToString()));

            drpclass.DataSource = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("GetClassTeacherClassName_Proc", param);
            drpclass.DataTextField = "ClassName";
            drpclass.DataValueField = "Id";
            drpclass.DataBind();
            drpclass.Items.Insert(0, new ListItem("<--Select-->", "0"));     

        }
    }

    private void loadSection(DropDownList drpsection, DropDownList drpclass)
    {
        if (Session["Logintype"].ToString() == "Admin")
        {
            BLL.BLLInstance.loadSection(drpsection, Session["SessionName"].ToString(), drpclass.SelectedValue.ToString());
        }
        else
        {
            sql = "Select SectionName,sm.Id from ClassTeacherMaster T1";
            sql +=  " inner join SectionMaster sm on sm.Id=T1.SectionId and sm.SessionName=t1.SessionName";
            sql +=  " where EmpCode='" + Session["LoginName"].ToString() + "' and IsClassTeacher=1 and T1.SessionName='" + Session["SessionName"] + "'";
            sql +=  " and t1.Classid=" + drpclass.SelectedValue.ToString() + "";
            BAL.objBal.FillDropDown_withValue(sql, drpsection, "SectionName", "Id");
        }
    }

    private void loadBranch(DropDownList drpbranch, DropDownList drpclass)
    {
        if (Session["Logintype"].ToString() == "Admin")
        {
            BLL.BLLInstance.loadBranch(drpbranch, Session["SessionName"].ToString(), drpclass.SelectedValue.ToString());
        }
        else
        {
            sql = "Select BranchName,bm.Id from ClassTeacherMaster T1";
            sql +=  "   inner join BranchMaster bm on bm.Id=T1.BranchId and bm.SessionName=t1.SessionName";
            sql +=  "   where EmpCode='" + Session["LoginName"].ToString() + "' and IsClassTeacher=1 and";
            sql +=  "   T1.SessionName='" + Session["SessionName"] + "' and T1.Classid='" + drpclass.SelectedValue.ToString() + "'";
            BAL.objBal.FillDropDown_withValue(sql, drpbranch, "BranchName", "Id");
        }
    }

    protected void drpClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadSection(drpSection, drpClass);
        loadBranch(drpBranch, drpClass);

        LoadDropDown();
        LoadGid();     
    }

    private DataTable getStudentsRecordinGrid()
    {
        DataTable dt=null;

        List<SqlParameter> param = new List<SqlParameter>();
        param.Add(new SqlParameter("@ClassName", drpClass.SelectedItem.Text.ToString()));
        param.Add(new SqlParameter("@SectionName", drpSection.SelectedItem.Text.ToString()));
        param.Add(new SqlParameter("@BranchName", drpBranch.SelectedItem.Text.ToString()));
        param.Add(new SqlParameter("@SRNO", drpSrno.SelectedValue.ToString()));
        param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
        param.Add(new SqlParameter("@BranchCode",Session["BranchCode"].ToString()));

        DataSet ds = new DataSet();

        ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("Get_AllStudentRecord", param);

        if (ds != null)
        {
            DataView dv = new DataView(ds.Tables[0]);
            dt = dv.ToTable(true, "S.R.No.", "Name", "Father's Name", "Class", "Section", "Branch", "Medium", "Transport Facility", "Status", "SRNOWITHNAME");

            dt.Columns["S.R.No."].ColumnName = "srno";
            dt.Columns["Father's Name"].ColumnName = "FatherName";
            dt.Columns["Transport Facility"].ColumnName = "TransportFacility";                     
        }
        return dt;
    }

    private void LoadGid()
    {
        rpt1.DataSource = getStudentsRecordinGrid();
        rpt1.DataBind();

        for (int i = 0; i < rpt1.Items.Count; i++)
        {
            Label lblSrno = (Label)rpt1.Items[i].FindControl("lblSrno");
            GridView grdHistory = (GridView)rpt1.Items[i].FindControl("grdHistory");

            loadHistory(lblSrno.Text.Trim(), grdHistory);
        }
    }

    private DataTable getStudentsRecordinDropDown()
    {
        DataTable dt = null;

        List<SqlParameter> param = new List<SqlParameter>();
        param.Add(new SqlParameter("@ClassName", drpClass.SelectedItem.Text.ToString()));
        param.Add(new SqlParameter("@SectionName", drpSection.SelectedItem.Text.ToString()));
        param.Add(new SqlParameter("@BranchName", drpBranch.SelectedItem.Text.ToString()));
        param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));

        DataSet ds = new DataSet();

        ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("Get_AllStudentRecord", param);

        if (ds != null)
        {
            DataView dv = new DataView(ds.Tables[0]);
            dt = dv.ToTable(true, "S.R.No.", "Name", "Father's Name", "Class", "Section", "Branch", "Medium", "Transport Facility", "Status", "SRNOWITHNAME");

            dt.Columns["S.R.No."].ColumnName = "srno";
            dt.Columns["Father's Name"].ColumnName = "FatherName";
            dt.Columns["Transport Facility"].ColumnName = "TransportFacility";
        }

        return dt;
    }

    private void LoadDropDown()
    {
        drpSrno.DataSource = getStudentsRecordinDropDown();
        drpSrno.DataTextField = "SRNOWITHNAME";
        drpSrno.DataValueField = "srno";
        drpSrno.DataBind();
        BAL.objBal.fillSelectvalue(drpSrno, "<--Select-->", "<--Select-->");
        drpSrno.SelectedIndex = 0;
    }

    private void loadHistory(string srno,GridView grd)
    {
        List<SqlParameter> param = new List<SqlParameter>();
        param.Add(new SqlParameter("@Srno", srno));
        param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));

        grd.DataSource = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("feePriviousHistory_proc", param);
        grd.DataBind();

        double sum = 0;

        for (int i = 0; i <= grd.Rows.Count - 1; i++)
        {

            Label lblSta = (Label)grd.Rows[i].FindControl("Label29");
            Label lblpaidAmt = (Label)grd.Rows[i].FindControl("Label21");

            if (lblSta.Text.Trim() == "Paid")
            {
                try
                {
                    sum = sum + Convert.ToDouble(lblpaidAmt.Text);
                }
                catch (Exception) { }
            }

        }

        if (grd.Rows.Count > 0)
        {
            Label FooterPaidAmt = (Label)grd.FooterRow.FindControl("Label39");
            FooterPaidAmt.Text = sum.ToString("N", new CultureInfo("en-In"));
        }
        
    }

    protected void drpSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadDropDown();
        LoadGid();
    }

    protected void drpBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadDropDown();
        LoadGid();
    }

    protected void drpSrno_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadGid();
    }

    string sql = "";

    protected void lnkReceiptNo_Click(object sender, EventArgs e)
    {        
        string ss = "";

        string Month = "";

        LinkButton chk = (LinkButton)sender;
        Label lblSrno = (Label)chk.NamingContainer.FindControl("Label24");
        Session["Srno"] = lblSrno.Text.Trim();
        ss = chk.ToolTip.ToString();

        lblDiscountPanel.Visible = true; lblDiscountPanelValue.Visible = true; Label44.Visible = true; Panel7.Visible = true;

        sql = "select id,StEnRCode,SrNo,convert(nvarchar,FeeDepositeDate,106) as FeeDepositeDate,RecieptSrNo,Cancel, BusConvience,  FeeMonth,TotalFeeAmount,";
        sql +=  " Cocession,RecievedAmount,CurrentAmount,LateFeeAmount,RemainingAmount,Remark,Class,Section,BalanceMode,DiscountName,DiscountAmount from FeeDeposite";
        sql +=  " where srno='" +Session["Srno"].ToString()+ "'  and RecieptSrNo='" + chk.Text + "'";
        sql +=  " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

        if (BAL.objBal.ReturnTag(sql, "BalanceMode").Trim() == "BalancePaid")
        {
            int i;
            string mon = "";

            sql = "select id,StEnRCode,SrNo,convert(nvarchar,FeeDepositeDate,106) as FeeDepositeDate,RecieptSrNo,Cancel, BusConvience,  FeeMonth,TotalFeeAmount,";
            sql +=  " Cocession,RecievedAmount,CurrentAmount,LateFeeAmount,RemainingAmount,Remark,Class,Section,BalanceMode,DiscountName,DiscountAmount from FeeDeposite";
            sql +=  " where srno='" + Session["Srno"].ToString() + "'  and RecieptSrNo='" + chk.Text + "'";
            sql +=  " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

            if (BAL.objBal.ReturnTag(sql, "BalanceMode").Trim() == "BalancePaid")
            {

                Label32.Text = BAL.objBal.ReturnTag(sql, "CurrentAmount");
                Label34.Text = BAL.objBal.ReturnTag(sql, "RecievedAmount");
                Label35.Text = BAL.objBal.ReturnTag(sql, "RemainingAmount");

                lblID0.Text = chk.Text;
                if (BAL.objBal.ReturnTag(sql, "Cancel").Trim() == "Y")
                {
                    lblcancel0.Text = "CANCELLED";
                    Session["RCancel"] = "CANCELLED";
                }
                else
                {
                    lblcancel0.Text = "";
                    Session["RCancel"] = "";
                }

                int k = Convert.ToInt32(ss) - 1;

                sql = "   select top 1 id,StEnRCode,SrNo,convert(nvarchar,FeeDepositeDate,106) as FeeDepositeDate,RecieptSrNo,Cancel,   FeeMonth,TotalFeeAmount, ";
                sql +=  " Cocession,RecievedAmount,CurrentAmount,RemainingAmount,Remark,Class,Section from FeeDeposite   ";
                sql +=  " where  srno='" +Session["Srno"].ToString()+ "'  and id<=" + k.ToString();
                sql +=  " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
                sql +=  " order by id  desc ";

                try
                {
                    Label36.Text = BAL.objBal.ReturnTag(sql, "RemainingAmount");

                }
                catch (Exception) { }

                try
                {
                    for (i = 0; i < rpt1.Items.Count; i++)
                    {
                        GridView grdHistory = (GridView)rpt1.Items[i].FindControl("grdHistory");
                        for (i = 0; i <= grdHistory.Rows.Count - 1; i++)
                        {

                            LinkButton lnkRecieptNo = (LinkButton)grdHistory.Rows[i].FindControl("lnkReceiptNo");
                            if (lnkRecieptNo.Text == chk.Text)
                            {
                                Label lblmonth = (Label)grdHistory.Rows[i].FindControl("Label20");

                                mon = lblmonth.Text;
                            }
                        }
                    }

                    sql = "select  SUM(RecievedAmount  ) as TotalRecivedAmt from FeeDeposite  where srno='" + Session["Srno"].ToString() + "'  and   ";
                    sql +=  "  FeeMonth='" + mon + "' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
                    Label Label33 = new Label();
                    Label33.Text = BAL.objBal.ReturnTag(sql, "TotalRecivedAmt");
                }
                catch (Exception) { }


                try
                {


                    sql = "     select top 1 id,StEnRCode,SrNo,convert(nvarchar,FeeDepositeDate,106) as FeeDepositeDate,RecieptSrNo,Cancel,   FeeMonth,TotalFeeAmount, ";
                    sql +=  " Cocession,RecievedAmount,CurrentAmount,RemainingAmount,Remark,Class,Section from FeeDeposite where  srno='" + Session["Srno"].ToString() + "'";
                    sql +=  " and  FeeMonth='" + mon + "'  and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

                    Label37.Text = BAL.objBal.ReturnTag(sql, "CurrentAmount");
                }
                catch (Exception) { }
                Panel5_ModalPopupExtender.Show();
            }
        }
        else
        {

            sql = "select id,StEnRCode,SrNo,convert(nvarchar,FeeDepositeDate,106) as FeeDepositeDate,RecieptSrNo,Cancel, BusConvience,  FeeMonth,TotalFeeAmount,";
            sql +=  " Cocession,RecievedAmount,CurrentAmount,LateFeeAmount,RemainingAmount,Remark,Class,Section,BalanceMode,DiscountName,DiscountAmount,NextDueAmount";
            sql +=  " from FeeDeposite  where srno='" +Session["Srno"].ToString()+ "'  and RecieptSrNo='" + chk.Text + "'";
            sql +=  " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

            lblTotalFee.Text = BAL.objBal.ReturnTag(sql, "TotalFeeAmount");
            lblConcession.Text = BAL.objBal.ReturnTag(sql, "Cocession");
            lblPaidAmount.Text = BAL.objBal.ReturnTag(sql, "RecievedAmount");
            lblBalace.Text = BAL.objBal.ReturnTag(sql, "RemainingAmount");
            lblRemark.Text = BAL.objBal.ReturnTag(sql, "Remark");
            lblLate.Text = BAL.objBal.ReturnTag(sql, "LateFeeAmount");
            Label31.Text = BAL.objBal.ReturnTag(sql, "BusConvience");
            Month = BAL.objBal.ReturnTag(sql, "FeeMonth");

            Session["NextDueAmt"] = BAL.objBal.ReturnTag(sql, "NextDueAmount");
            try
            {
                lblDiscountPanel.Visible = true; lblDiscountPanelValue.Visible = true; Label44.Visible = true; Panel7.Visible = true;
                lblDiscountPanel.Text = BAL.objBal.ReturnTag(sql, "DiscountName");
                lblDiscountPanelValue.Text = BAL.objBal.ReturnTag(sql, "DiscountAmount");
                if (lblDiscountPanel.Text != "")
                {
                    Panel7.Visible = true;
                }
                else
                {
                    Panel7.Visible = false;
                }
            }
            catch (Exception) { lblDiscountPanel.Visible = false; lblDiscountPanelValue.Visible = false; Label44.Visible = false; Panel7.Visible = false; }
            if (BAL.objBal.ReturnTag(sql, "BusConvience") == "")
            {
                Label31.Text = "0";
            }
            else
            {
                Label31.Text = BAL.objBal.ReturnTag(sql, "BusConvience");
            }
            lblID.Text = chk.Text;
            if (BAL.objBal.ReturnTag(sql, "Cancel").Trim() == "Y")
            {
                lblcancel.Text = "CANCELLED";
                Session["RCancel"] = "CANCELLED";
            }
            else
            {
                lblcancel.Text = "";
                Session["RCancel"] = "";
            }

            int k = Convert.ToInt32(ss) - 1;

            sql = "select top 1 id,StEnRCode,SrNo,convert(nvarchar,FeeDepositeDate,106) as FeeDepositeDate,RecieptSrNo,Cancel,   FeeMonth,TotalFeeAmount,";
            sql +=  " Cocession,RecievedAmount,CurrentAmount,RemainingAmount,Remark,Class,Section from FeeDeposite   ";
            sql +=  " where  srno='" +Session["Srno"].ToString()+ "'  and id<=" + k.ToString();
            sql +=  " and Cancel is null and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            sql +=  " order by RecieptSrNo  desc ";

            try
            {
                Label25.Text = BAL.objBal.ReturnTag(sql, "RemainingAmount");
                double a = Convert.ToDouble(Label25.Text);
            }
            catch (Exception) { Label25.Text = "0"; con.Close(); }
            GridViewRow currentrow = ((GridViewRow)((Control)sender).Parent.Parent);
            Label Label20 = (Label)currentrow.FindControl("Label20");
            Session["Installment"] = Label20.Text;
            Panel4_ModalPopupExtender.Show();

        }
    
    }
}