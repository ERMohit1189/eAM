using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class your_account : System.Web.UI.Page
{
    protected void Page_PreInIt(object sender, EventArgs e)
    {
        if (Session["Logintype"] == null) { Response.Redirect("~/default.aspx"); }
        if (Session["Logintype"].ToString() == "Admin")
        {
            this.MasterPageFile = "~/Master/admin_root-manager.master";
        }
        else if (Session["Logintype"].ToString() == "Administrator")
        {
            this.MasterPageFile = "~/Administrator/administrato_root-manager.master";
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
     
        }
        if (!IsPostBack)
        {
            displayCustAcc();
        }
    }

    public void displayCustAcc()
    {
        string sql = "Select CollegeName from CollegeMaster where BranchCode=" + Session["BranchCode"] + "";
        lblNameOrg.Text = BAL.objBal.ReturnTag(sql, "CollegeName");
        para1.Visible = lblNameOrg.Text.Trim() == string.Empty ? false : true;

        DataSet ds = new DataSet();

        List<SqlParameter> param = new List<SqlParameter>();
        param.Add(new SqlParameter("@Queryfor", "S"));

        ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("custRegProc", param);

        if (ds != null)
        {
            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                lblCid.Text = dt.Rows[0]["CID"].ToString();
                para2.Visible = lblCid.Text.Trim() == string.Empty ? false : true;

                lblSubModule.Text = dt.Rows[0]["SubModule"].ToString();
                para3.Visible = lblSubModule.Text.Trim() == string.Empty ? false : true;

                lblSubStatus.Text = dt.Rows[0]["SubStatus"].ToString();
                para4.Visible = lblSubStatus.Text.Trim() == string.Empty ? false : true;

                lblBillingFrequency.Text = dt.Rows[0]["BillingFrequency"].ToString();
                para5.Visible = lblBillingFrequency.Text.Trim() == string.Empty ? false : true;

                lblBillingCurrency.Text = dt.Rows[0]["BillingCurrency"].ToString();
                para6.Visible = lblBillingCurrency.Text.Trim() == string.Empty ? false : true;

                lblNextDueDate.Text = dt.Rows[0]["NextDueDate"].ToString();
                para7.Visible = lblNextDueDate.Text.Trim() == string.Empty ? false : true;
            }
            else
            {
                para2.Visible = false;
                para3.Visible = false;
                para4.Visible = false;
                para5.Visible = false;
                para6.Visible = false;
                para7.Visible = false;
            }
        }
        else
        {
            para2.Visible = false;
            para3.Visible = false;
            para4.Visible = false;
            para5.Visible = false;
            para6.Visible = false;
            para7.Visible = false;
        }

    }
}

class Shape
{
    protected int width, height;
    public Shape(int a = 0, int b = 0)
    {
        width = a;
        height = b;
    }
    public virtual int area()
    {
        Console.WriteLine("Parent class area :");
        return 0;
    }
}
class Rectangle : Shape
{
    public Rectangle(int a = 0, int b = 0) : base(a, b)
    {

    }
    public override int area()
    {
        Console.WriteLine("Rectangle class area :");
        return (width * height);
    }
}
class Triangle : Shape
{
    public Triangle(int a = 0, int b = 0) : base(a, b)
    {

    }
    public override int area()
    {
        Console.WriteLine("Triangle class area :");
        return (width * height / 2);
    }
}
class Caller
{
    public void CallArea(Shape sh)
    {
        int a;
        a = sh.area();
        Console.WriteLine("Area: {0}", a);
    }
}
class Tester
{

    static void Main(string[] args)
    {
        Caller c = new Caller();
        Rectangle r = new Rectangle(10, 7);
        Triangle t = new Triangle(10, 5);
        c.CallArea(r);
        c.CallArea(t);
        Console.ReadKey();
    }
}