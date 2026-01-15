using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

public partial class admin_RulesForlibrary : System.Web.UI.Page
{
    List<SqlParameter> param = new List<SqlParameter>();
    string msg = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Campus camp = new Campus(); camp.LoadLoader(loader); 

        if ( Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        txtR1.Focus();
        if (!IsPostBack)
        {
            loadGrid();
        }
        //comment
    }
    protected void lnkSubmit_Click(object sender, EventArgs e)
    {
        msg = "";

        param = new List<SqlParameter>();
        param.Add(new SqlParameter("@QueryFor", "I"));
        param.Add(new SqlParameter("@R1", txtR1.Text.Trim()));
        param.Add(new SqlParameter("@R2", txtR2.Text.Trim()));
        param.Add(new SqlParameter("@R3", txtR3.Text.Trim()));
        param.Add(new SqlParameter("@R4", txtR4.Text.Trim()));
        param.Add(new SqlParameter("@R5", txtR5.Text.Trim()));
        param.Add(new SqlParameter("@rulesFor", rdorulesFor.SelectedItem.Text));
        param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
        param.Add(new SqlParameter("@LoginName", Session["LoginName"].ToString()));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
        SqlParameter para = new SqlParameter("@Msg", "");
        para.Direction = ParameterDirection.Output;
        param.Add(para);

        msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("RulesForLibraryProc", param);

        if (msg == "S" || msg == "U")
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Rules set successfully.", "S");       
            loadGrid();
        }
        else
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, Rules not set!", "A");       
        }
    }
    public void loadGrid()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();

        param = new List<SqlParameter>();
        param.Add(new SqlParameter("@QueryFor", "S"));
        param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
        param.Add(new SqlParameter("@rulesFor", rdorulesFor.SelectedItem.Text));
        ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("RulesForLibraryProc", param);
        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    txtR1.Text = dt.Rows[0]["R1"].ToString();
                    txtR2.Text = dt.Rows[0]["R2"].ToString();
                    txtR3.Text = dt.Rows[0]["R3"].ToString();
                    txtR4.Text = dt.Rows[0]["R4"].ToString();
                    txtR5.Text = dt.Rows[0]["R5"].ToString();
                }
                else
                {
                    txtR1.Text = "";
                    txtR2.Text = "";
                    txtR3.Text = "";
                    txtR4.Text = "";
                    txtR5.Text = "";
                }
            }
        }
    }

    protected void rdorulesFor_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadGrid();
    }
}