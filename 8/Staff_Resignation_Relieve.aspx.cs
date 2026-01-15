using System;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class admin_Staff_Resignation_Relieve : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    SqlCommand cmd = new SqlCommand();
    Campus oo = new Campus();
    string sql = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);
        if (!IsPostBack)
        {
            oo.AddDateMonthYearDropDown(DrpDDEmpYY, DrpDDEmpMM, DrpDDEmpDD);
            oo.FindCurrentDateandSetinDropDown(DrpDDEmpYY, DrpDDEmpMM, DrpDDEmpDD);
            oo.AddDateMonthYearDropDown(drpYYPanel, DrpMMPanel, DrpDDPanel);
            oo.FindCurrentDateandSetinDropDown(drpYYPanel, DrpMMPanel, DrpDDPanel);
            loadGrid();
        }
    }

    public void FindCurrentDateandSetinDropDown()
    {
        string dd = "", mm = "", yy = "";


        dd = oo.ReturnTag("Select day(getdate()) as DateDD", "DateDD");
        mm = oo.ReturnTag("Select Month(getdate())as MonthMM", "MonthMM");
        yy = oo.ReturnTag("Select Year(getdate()) as YearYY ", "YearYY");

        DrpDDEmpYY.Text = yy;
        if (mm == "1")
        {
            DrpDDEmpMM.Text = "Jan";
        }
        else if (mm == "2")
        {
            DrpDDEmpMM.Text = "Feb";
        }
        else if (mm == "3")
        {
            DrpDDEmpMM.Text = "Mar";
        }
        else if (mm == "4")
        {
            DrpDDEmpMM.Text = "Apr";
        }
        else if (mm == "5")
        {
            DrpDDEmpMM.Text = "May";
        }
        else if (mm == "6")
        {
            DrpDDEmpMM.Text = "Jun";

        }
        else if (mm == "7")
        {
            DrpDDEmpMM.Text = "Jul";
        }
        else if (mm == "8")
        {
            DrpDDEmpMM.Text = "Aug";
        }
        else if (mm == "9")
        {
            DrpDDEmpMM.Text = "Sep";
        }
        else if (mm == "10")
        {
            DrpDDEmpMM.Text = "Oct";
        }
        else if (mm == "11")
        {
            DrpDDEmpMM.Text = "Nov";
        }
        else if (mm == "12")
        {
            DrpDDEmpMM.Text = "Dec";
        }


        DrpDDEmpDD.Text = dd;
    }

    protected void DrpDDEmpYY_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.YearDropDown(DrpDDEmpYY, DrpDDEmpMM, DrpDDEmpDD);
    }
    protected void DrpDDEmpMM_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.MonthDropDown(DrpDDEmpYY, DrpDDEmpMM, DrpDDEmpDD);
    }
    protected void DrpDDEmpDD_SelectedIndexChanged(object sender, EventArgs e)
    {
      
    }

    protected void lnkShow_Click(object sender, EventArgs e)
    {
        displayEmpInfo();
    }

    public void displayEmpInfo()
    {
        var empId = Request.Form[hfEmployeeId.UniqueID];
        if (empId == string.Empty)
        {
            empId = txtEnter.Text.Trim();
        }
        sql = "Select Empid from EmpWithdrawlRecord where EmpId='" + empId.Trim() + "'";
        if (oo.ReturnTag(sql, "Empid") == "")
        {
            sql = "Select eod.EmpId EmpId,eod.Ecode Ecode,egd.EFirstName+' '+egd.EMiddleName+' '+egd.ELastName as EmpName,egd.EFatherName FatherName,eod.DesNameNew,";
            sql = sql + " egd.EMotherName,egd.EMobileNo,Convert(varchar(11),eod.RegistrationDate,106) as RegistrationDate  from EmpployeeOfficialDetails eod ";
            sql = sql + " inner join EmpGeneralDetail egd on eod.Empid=egd.Empid and eod.EmpId=egd.EmpId where eod.Withdrwal is null ";
            sql = sql + " and eod.EmpId='" + empId.Trim() + "' and egd.BranchCode=" + Session["BranchCode"].ToString() + " and eod.BranchCode=" + Session["BranchCode"].ToString() + "";
            Grd.DataSource = oo.GridFill(sql);
            Grd.DataBind();
            if (Grd.Rows.Count > 0)
            {
                div1.Visible = true;
            }
            else
            {
                Campus camp = new Campus(); camp.msgbox(lnkShow, msgbox1, "Sorry, No record(s) found!", "A");
                div1.Visible = false;
            }
        }
        else
        {
            Campus camp = new Campus(); camp.msgbox(lnkShow, msgbox1, "This staff already Relieved!", "A");
            div1.Visible = false;
        }
    }

    public void loadGrid()
    {
        string order = "Desc";
        if (rbOrder.SelectedIndex == 0)
        {
            order = "Asc";
        }
        string shortby = "order by Convert(int,Right(EmpCode,3)) " + order + "";
        if (rbShort.SelectedIndex == 1)
        {
            shortby = "order by Convert(Date,Dateofreleving) " + order + "";
        }

        sql = "Select Id,Empid,EmpCode,Name,FatherName,MotherName,Convert(varchar(11),DateofJoining,106) as DateofJoining,Convert(varchar(11),";
        sql = sql + " Dateofreleving,106) as Dateofreleving,Remark,Contactno,Designation from EmpWithdrawlRecord where BranchCode=" + Session["BranchCode"].ToString() + " " + shortby + " ";
        Grd1.DataSource = oo.GridFill(sql);
        Grd1.DataBind();
    }
    protected void lnkSubmit_Click(object sender, EventArgs e)
    {
        string dd = "";
        dd = DrpDDEmpYY.SelectedItem.ToString() + "/" + DrpDDEmpMM.SelectedItem.ToString() + "/" + DrpDDEmpDD.SelectedItem.ToString();
        Label lblEmpid = (Label)Grd.Rows[0].FindControl("lblEmpId");
        Label lblEcode = (Label)Grd.Rows[0].FindControl("lblEcode");
        Label lblEmpName = (Label)Grd.Rows[0].FindControl("lblEmpName");
        Label lblFName = (Label)Grd.Rows[0].FindControl("lblFName");
        Label lblMName = (Label)Grd.Rows[0].FindControl("lblMName");
        Label lblJoining = (Label)Grd.Rows[0].FindControl("lblJoining");
        Label lblContact = (Label)Grd.Rows[0].FindControl("lblContact");
        Label lblDesi = (Label)Grd.Rows[0].FindControl("lblDesi");
        cmd = new SqlCommand();
        cmd.CommandText = "EmployeeWithDrawalProc";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
        cmd.Parameters.AddWithValue("@EmpId", lblEmpid.Text.Trim());
        cmd.Parameters.AddWithValue("@EmpCode", lblEcode.Text.Trim());
        cmd.Parameters.AddWithValue("@Name", lblEmpName.Text.Trim());
        cmd.Parameters.AddWithValue("@FatherName", lblFName.Text.Trim());
        cmd.Parameters.AddWithValue("@MotherName", lblMName.Text.Trim());
        cmd.Parameters.AddWithValue("@DateofJoining", lblJoining.Text.Trim());
        cmd.Parameters.AddWithValue("@Dateofreleving", dd.ToString());
        cmd.Parameters.AddWithValue("@Remark", txtRemark.Text.Trim());
        cmd.Parameters.AddWithValue("@ContactNo", lblContact.Text.Trim());
        cmd.Parameters.AddWithValue("@Designation", lblDesi.Text.Trim());
        cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
        cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
        cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
        cmd.Parameters.AddWithValue("@Text", DropDownList1.SelectedItem.Text.Trim());
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        loadGrid();
        Campus camp = new Campus(); camp.msgbox(lnkSubmit, msgbox1, DropDownList1.SelectedItem.Text + " successfully.", "S");
        txtRemark.Text = "";
        div1.Visible = false;

    }

    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblEmpId = (Label)chk.NamingContainer.FindControl("lblEmpId");
        Label lblId = (Label)chk.NamingContainer.FindControl("Label4");
        string ss = lblId.Text;
        lblID.Text = ss.ToString();
        lblValue.Text = lblEmpId.Text;
        sql = "select Dateofreleving,Remark,left(convert(nvarchar,Dateofreleving,106),2) as DD, Right(left(convert(nvarchar,Dateofreleving,106),6),3) as MM , RIGHT(convert(nvarchar,Dateofreleving,106),4) as YY from EmpWithdrawlRecord where BranchCode=" + Session["BranchCode"].ToString() + "";
        sql = sql + " and  Id="+lblID.Text+"";
        drpYYPanel.Text = oo.ReturnTag(sql, "yy");
        DrpMMPanel.Text = oo.ReturnTag(sql, "mm");
        DrpDDPanel.Text = oo.ReadDD(oo.ReturnTag(sql, "dd"));
        txtRemarkPanel.Text = oo.ReturnTag(sql, "Remark");
        DropDownList2.SelectedIndex = 0;
        Panel2_ModalPopupExtender.Show();
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        string dd = "";
        dd = drpYYPanel.SelectedItem.ToString() + "/" + DrpMMPanel.SelectedItem.ToString() + "/" + DrpDDPanel.SelectedItem.ToString();
        sql = "update EmpWithdrawlRecord set Dateofreleving='" + dd + "' ,Remark='" + txtRemarkPanel.Text + "'";
        sql = sql + "  where  Id='" + lblID.Text + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        oo.ProcedureDatabase(sql);
        if (DropDownList2.SelectedItem.ToString() == "Yes")
        {
            sql = "update EmpDocuments set Withdrwal=Null where EmpId='" + lblValue.Text + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            oo.ProcedureDatabase(sql);
            sql = "update EmpEmployeeDetails  set Withdrwal=Null where EmpId='" + lblValue.Text + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            oo.ProcedureDatabase(sql);
            sql = "update EmpGeneralDetail   set Withdrwal=Null where EmpId='" + lblValue.Text + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            oo.ProcedureDatabase(sql);
            sql = " update EmpployeeOfficialDetails  set Withdrwal=Null where EmpId='" + lblValue.Text + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            oo.ProcedureDatabase(sql);
            sql = "update EmpPreviousEmployment  set Withdrwal=Null where EmpId='" + lblValue.Text + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            oo.ProcedureDatabase(sql);
            sql = "delete from EmpWithdrawlRecord where EmpId='" + lblValue.Text + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            oo.ProcedureDatabase(sql);
            loadGrid();
        }
        Campus camp = new Campus(); camp.msgbox(lnkSubmit, msgbox1, "Updated Successfully!", "S");

        loadGrid();
    }
    protected void rbShort_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadGrid();
    }
    protected void rbOrder_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadGrid();
    }

    protected void txtEnter_OnTextChanged(object sender, EventArgs e)
    {
        displayEmpInfo();
    }
}