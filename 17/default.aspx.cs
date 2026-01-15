using System.Web.UI;
using System.Data.SqlClient;
using System;
using System.Net.NetworkInformation;
public partial class index : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Redirect("../default.aspx");
        string sql1 = "";

        con = oo.dbGet_connection();
        txtUserName.Focus();
        if (!IsPostBack)
        {
            sql = "Select BranchName from Branchtab";
            oo.FillDropDownWithOutSelect(sql, DrpBranchName, "BranchName");

            sql1 = " select YEAR(GETDATE()) as yy ";
            sql1 = oo.ReturnTag(sql1, "yy");

            sql = "select sessionName from SessionMaster order by SessionName";

            oo.FillDropDownWithOutSelect(sql, DrpSessionName, "SessionName");

            sql = @"select SessionName from SessionMaster where Convert(Date,FromDate)<=Convert(Date,GETDATE())
                            and  Convert(Date,ToDate)>=Convert(Date,GETDATE())";
            DrpSessionName.Text = oo.ReturnTag(sql, "SessionName");

            if (Request.Cookies["UserName"] != null && Request.Cookies["Password"] != null)
            {
                chkRememberMe.Checked = true;
                txtUserName.Text = Request.Cookies["UserName"].Value;
                txtPassword.Attributes["value"] = Request.Cookies["Password"].Value;
                LinkButton1.Focus();
            }
            else
            {
                chkRememberMe.Checked = false;
                txtUserName.Focus();
            }

            sql = "select BrandName from BrandTab";
            Label1.Text = oo.ReturnTag(sql, "BrandName");
        }

    }


    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        
        if (chkRememberMe.Checked)
        {
            Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(30);
            Response.Cookies["Password"].Expires = DateTime.Now.AddDays(30);
        }
        else
        {
            Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["Password"].Expires = DateTime.Now.AddDays(-1);
        }

        Response.Cookies["UserName"].Value = txtUserName.Text.Trim();
        Response.Cookies["Password"].Value = txtPassword.Text.Trim();

        if (DrpBranchName.SelectedItem.Text == "<--Select-->" || DrpSessionName.SelectedItem.Text == "<--Select-->")
        {
            oo.MessageBox("Please <--Select--> Condition", this.Page);
        }


        else
        {
            string sql1 = "";
            string BCode = "";

            sql1 = "select LoginName,LoginId,loginTypeId from LoginTab  lt  left join BranchTab bt on lt.BranchId=bt.BranchId ";
            sql1 = sql1 + " where (lt.IsActive=1 or lt.IsActive is Null) and lt.LoginName ='" + txtUserName.Text + "' and lt.Pass='" + txtPassword.Text + "' and bt.BranchName='" + DrpBranchName.SelectedItem.ToString() + "'";

            BCode = oo.ReturnTag("select branchId from BranchTab where BranchName='" + DrpBranchName.SelectedItem.ToString() + "'", "BranchId");
            Session["LoginName"] = txtUserName.Text.Trim();
            Session["LoginId"] = oo.ReturnTag(sql1, "LoginId");
            Session["BranchCode"] = BCode;
            Session["SessionName"] = DrpSessionName.SelectedItem.ToString();
            Session["BranchName"] = DrpBranchName.SelectedItem.ToString();
            sql = "Select  SessionId from SessionMaster  where SessionName='" + Session["SessionName"] + "'";
            Session["SessionID"] = oo.ReturnTag(sql, "SessionId");
            string str = oo.ReturnTag(sql1, "loginTypeId");
            
            Page.Validate();
            if (Page.IsValid)
            {
                if (oo.Duplicate(sql1))
                {
                    alert_danger.Style.Add("display", "none");
                    alert_success.Style.Add("display", "block");
                    System.Threading.Thread.Sleep(200);
                    if (str == "1")
                    {
                        Session["Expire"] = "TimeOUT";
                        Session["Logintype"] = "SuperAdmin";
                        sql = "Select ISNULL(DisplayName,Name) as DisplayName from NewSuperAdminInformation where userid='" + Session["LoginName"].ToString() + "' ";
                        if (oo.ReturnTag(sql, "DisplayName") == "")
                        {
                            Session["DisplayName"] = Session["LoginName"];
                        }
                        else
                        {
                            Session["DisplayName"] = oo.ReturnTag(sql, "DisplayName");
                        }
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "window.location='" + Request.ApplicationPath + "/dashboard-main.aspx';", true);
                     
                    }
                    else if (str == "2")
                    {
                        Session["Expire"] = "TimeOUT";
                        Session["Logintype"] = "Admin";
                        sql = "Select ISNULL(DisplayName,(Case When CHARINDEX(' ',Name)=0 then Name Else Left(Name,CHARINDEX(' ',Name)) End)) as DisplayName from NewAdminInformation where userid='" + Session["LoginName"].ToString() + "' ";
                        if (oo.ReturnTag(sql, "DisplayName") == "")
                        {
                            Session["DisplayName"] = Session["LoginName"];
                        }
                        else
                        {
                            Session["DisplayName"] = oo.ReturnTag(sql, "DisplayName");
                        }
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "window.location='" + Request.ApplicationPath + "/dashboard-main.aspx';", true);
                   
                    }
                    else if (str == "3")
                    {
                        Session["Expire"] = "TimeOUT";
                        Session["Logintype"] = "Staff";
                        sql = "Select ISNULL(DisplayName,EFirstName) as DisplayName from EmpGeneralDetail where Ecode='" + Session["LoginName"].ToString() + "' ";
                        if (oo.ReturnTag(sql, "DisplayName") == "")
                        {
                            Session["DisplayName"] = Session["LoginName"];
                        }
                        else
                        {
                            Session["DisplayName"] = oo.ReturnTag(sql, "DisplayName");
                        }
                        Response.Redirect("~/Staff/dashboard.aspx");
                    }
                    else if (str == "4")
                    {
                        Session["Expire"] = "TimeOUT";
                        Session["Logintype"] = "Student";
                        Response.Redirect("~/Student/dashboard.aspx");
                    }
                    else if (str == "5")
                    {
                        Session["Expire"] = "TimeOUT";
                        Session["Logintype"] = "Guardian";
                        Response.Redirect("~/Guardian/dashboard.aspx");
                    }
                    else if (str == "6")
                    {
                        Session["Expire"] = "TimeOUT";
                        Session["Logintype"] = "Administrator";
                        Response.Redirect("~/Administrator/dashboard.aspx");
                    }

                }
                else
                {
                    alert_success.Style.Add("display", "none");
                    alert_danger.Style.Add("display", "block");
                }
            }
            else
            {
                return;
            }

        }
        


    }
    protected void DrpSessionName_SelectedIndexChanged(object sender, EventArgs e)
    {

    }


    public bool MacIDStore()
    {
        string xx = "";
        NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
        xx = nics[0].GetPhysicalAddress().ToString();
        bool flag = false;

        sql = "select seq from student";
        if (oo.Duplicate(sql))
        {
            if (xx == oo.ReturnTag(sql, "seq").Trim())
            {
                flag = true;
                Session["RunProject"] = "Yes";
            }
            else
            {
                flag = false;
            }
        }
        else
        {
            sql = "Insert into Student (Seq) values ('" + xx + "')";
            flag = true;
        }
        oo.ProcedureDatabase(sql);
        return flag;
    }

    public void Validation()
    {

        string sql = "";
        string co = "";
        int pp = 0;
        int max = 0;
        sql = "select NoofDay from securityTab where StartDate='" + oo.CurrentDate() + "'";

        if (oo.Duplicate(sql) == false)
        {
            sql = "select max(count) as  cc from SecurityTab";
            co = oo.ReturnTag(sql, "cc");
            sql = "select NoofDay from SecurityTab where id=1";
            max = Convert.ToInt32(oo.ReturnTag(sql, "NoofDay"));
            pp = Convert.ToInt32(co);
            pp = pp + 1;
            if (pp <= max)
            {
                sql = "Insert into SecurityTab (Id,StartDate,NoofDay,count) values (1,'" + oo.CurrentDate() + "','15'," + pp + ")";

                oo.ProcedureDatabase(sql);
                Session["Expire"] = "TimeOUT";
            }
            else
            {
                Session["Expire"] = "";
            }


        }
    }
}
