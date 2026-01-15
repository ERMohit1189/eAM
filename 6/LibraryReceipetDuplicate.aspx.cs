using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

public partial class admin_LibraryReceipetDuplicate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null || Session["LibRecieptNo"]==null)
        {
            Response.Redirect("default.aspx");
        }
        try
        {
            Label1.Text = Session["LibRecieptNo"].ToString();
            BLL.BLLInstance.LoadHeader("Receipt", header1);
            BLL.BLLInstance.LoadHeader("Receipt", header2);
            if (!IsPostBack)
            {
                LinkButton1.Focus();
                LaserPrint();
            }
        }
        catch (Exception ex)
        {
            BAL.objBal.MessageBoxforUpdatePanel(ex.Message, this.Page);
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = abc;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }

    public void LaserPrint()
    {
        List<SqlParameter> param = new List<SqlParameter>();
        param.Add(new SqlParameter("@Receiptno", Label1.Text));
        param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));

        DataSet ds = new DataSet();
        ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("GenrateLibFeeReceipetProc", param);

        if (ds != null)
        {
            Repeater1.DataSource = ds.Tables[0];
            Repeater1.DataBind();

            GridView1.DataSource = ds.Tables[1];
            GridView1.DataBind();

            Repeater2.DataSource = ds.Tables[2];
            Repeater2.DataBind();

            Repeater3.DataSource = ds.Tables[0];
            Repeater3.DataBind();

            GridView2.DataSource = ds.Tables[1];
            GridView2.DataBind();

            Repeater4.DataSource = ds.Tables[2];
            Repeater4.DataBind();


            //Label lblsrno = (Label)Repeater1.Items[0].FindControl("Label24");
            //lblBarCode.Text = lblsrno.Text;

            //lblBarCodesch.Text = lblsrno.Text;
        }
    }
    
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        Response.Redirect("BookIssue_Return_Master.aspx");
    }
}

