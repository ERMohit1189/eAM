using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;

public partial class SuperAdmin_PermissionForchangeMarks : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    SqlCommand cmd;
    Campus oo = new Campus();
    string sql = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((string)Session["LoginName"] == "" || (string)Session["BranchCode"] == "" || (string)Session["SessionName"] == "" || Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }

        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);
        if (!IsPostBack)
        {
            sql = "select EmpDepName from EmpDepMaster where BranchCode=" + Session["BranchCode"] + "";
            if (oo.Duplicate(sql))
            {
                oo.FillDropDown(sql, DrpDepartment, "EmpDepName");
            }
            loadGrid();
        }
       
    }

    protected void DrpDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadGrid();
    }

    protected void ChkAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkAll = (CheckBox)sender;
        if (chkAll.Checked == false)
        {
            foreach (GridViewRow gvr in Grd1.Rows)
            {
                CheckBox chk = (CheckBox)gvr.FindControl("chk");
                chk.Checked = false;
            }
        }
        else
        {
            foreach (GridViewRow gvr in Grd1.Rows)
            {
                CheckBox chk = (CheckBox)gvr.FindControl("chk");
                chk.Checked = true;
            }
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {

        saveupdate();
    }

    public void saveupdate()
    {
        if (Grd1.Rows.Count > 0)
        {
            foreach (GridViewRow gvr in Grd1.Rows)
            {
                CheckBox chk = (CheckBox)gvr.FindControl("Chk");
                Label Label5 = (Label)gvr.FindControl("Label5");
                cmd = new SqlCommand();
                cmd.CommandText = "PermissionForMarksUpdateProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                string str = "";
                int permissionvalue;
                sql = "Select *from PermissionForMarksUpdate where EmpCode='" + Label5.Text + "'  and BranchCode=" + Session["BranchCode"] + "";
                if (oo.Duplicate(sql) == false)
                {
                    if (chk.Checked)
                    {
                        str = "true";
                        cmd.Parameters.AddWithValue("@Permission", str);                       
                        permissionvalue = 1;
                    }
                    else
                    {
                        str = "false";
                        cmd.Parameters.AddWithValue("@Permission", str);
                        permissionvalue = 0;
                    }
                    cmd.Parameters.AddWithValue("@EmpCode", Label5.Text);
                    cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                    cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                    cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                    cmd.Parameters.AddWithValue("@PermissionValue", permissionvalue);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully", "S");       

                }
                else
                {
                    if (chk.Checked)
                    {
                        str = "true";
                        sql = "Select Permission from PermissionForMarksUpdate where EmpCode='" + Label5.Text + "' and BranchCode=" + Session["BranchCode"] + "";
                        if (oo.ReturnTag(sql, "Permission") == "false")
                        {
                            sql = "Update PermissionForMarksUpdate set Permission='" + str + "',RecordDate=GetDate(),PermissionValue=Permissionvalue+1 where EmpCode='" + Label5.Text + "' and BranchCode=" + Session["BranchCode"] + "";
                            cmd = new SqlCommand(sql, con);
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                            //oo.MessageBoxforUpdatePanel("Submitted successfully", LinkButton1);
                            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully", "S");       

                        }
                    }
                    else
                    {
                        str = "false";
                        sql = "Update PermissionForMarksUpdate set Permission='" + str + "' where EmpCode='" + Label5.Text + "' and BranchCode=" + Session["BranchCode"] + "";
                        cmd = new SqlCommand(sql, con);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        //oo.MessageBoxforUpdatePanel("Submitted successfully", LinkButton1);
                        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully", "S");       

                    }
                }

               
            }
        }
    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadGrid();
    }

    public void loadGrid()
    {
        sql = "select EmpDepName from EmpDepMaster where BranchCode=" + Session["BranchCode"] + "";
        if (!oo.Duplicate(sql))
        {
            divTools.Visible = false;
            divGrid.Visible = false;
            LinkButton1.Visible = false;
            msgbox.InnerText = "Please create employee departments!";
            msgbox.Style.Add("color", "red");
            return;
        }
        else
        {
            divTools.Visible = true;
            divGrid.Visible = true;
            LinkButton1.Visible = true;
        }

        if (RadioButtonList1.SelectedIndex == 0)
        {
            staffdiv.Visible = false;

            sql = "    Select UserId Loginid,Upper(Name) Name,ContactNo MobileNo from NewAdminInformation where BranchCode=" + Session["BranchCode"] + "";

            Grd1.DataSource = oo.GridFill(sql);
            Grd1.DataBind();       
        }
        else
        {
            staffdiv.Visible = true;

            sql = "Select Ecode Loginid,Name,EMobileNo MobileNo from GetAllStaffRecords_UDF(" + Session["BranchCode"].ToString() + ") ";
            sql = sql + " where DepartmentName='" + DrpDepartment.SelectedItem.Text.Trim().ToString() + "'";

            Grd1.DataSource = oo.GridFill(sql);
            Grd1.DataBind();           
        }

        if (Grd1.Rows.Count > 0)
        {
            foreach (GridViewRow gvr in Grd1.Rows)
            {
                CheckBox chk = (CheckBox)gvr.FindControl("Chk");
                Label Label5 = (Label)gvr.FindControl("Label5");
                Label Label6 = (Label)gvr.FindControl("Label6");

                sql = "Select Permission from PermissionForMarksUpdate where EmpCode='" + Label5.Text + "' and BranchCode=" + Session["BranchCode"] + "";
                if (oo.ReturnTag(sql, "Permission") == "true")
                {
                    chk.Checked = true;
                }
            }
        }
    }
}