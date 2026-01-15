using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class TestPermissionCBSE : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    SqlCommand cmd = new SqlCommand();
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
            displayEmpInfo();
        }
    }
    public void displayEmpInfo()
    {

        string ss = "select cm.id Classid, bm.Id BranchId, cm.ClassName+' '+case when bm.IsDisplay=1 then bm.BranchName else '' end CombineClass, isnull(Test1, 'Yes')Test1, isnull(Test2, 'Yes')Test2, isnull(Test3, 'Yes')Test3, isnull(Test4, 'Yes')Test4, isnull(Test5, 'Yes')Test5, isnull(Test6, 'Yes')Test6, PassingMark ";
        ss +=  " from ClassMaster cm inner join BranchMaster bm on bm.Classid=cm.Id and bm.SessionName=cm.SessionName and bm.BranchCode=cm.BranchCode ";
        ss +=  " left join ICSETestPermission pm on pm.Classid=cm.Id and pm.BranchId=bm.Id and pm.SessionName=cm.SessionName and pm.BranchCode=cm.BranchCode";
        ss +=  " where cm.BranchCode=" + Session["BranchCode"] + " and cm.SessionName='" + Session["SessionName"] + "' order by cm.CIDOrder asc";
        var dt = oo.Fetchdata(ss);
        Grd.DataSource = dt;
        Grd.DataBind();
        for (int i = 0; i < Grd.Rows.Count; i++)
        {
            RadioButtonList rdoTest1 = (RadioButtonList)Grd.Rows[i].FindControl("rdoTest1");
            RadioButtonList rdoTest2 = (RadioButtonList)Grd.Rows[i].FindControl("rdoTest2");
            RadioButtonList rdoTest3 = (RadioButtonList)Grd.Rows[i].FindControl("rdoTest3");
            RadioButtonList rdoTest4 = (RadioButtonList)Grd.Rows[i].FindControl("rdoTest4");
            RadioButtonList rdoTest5 = (RadioButtonList)Grd.Rows[i].FindControl("rdoTest5");
            RadioButtonList rdoTest6 = (RadioButtonList)Grd.Rows[i].FindControl("rdoTest6");
            TextBox txtPassingMark = (TextBox)Grd.Rows[i].FindControl("txtPassingMark");
            rdoTest1.SelectedValue = dt.Rows[i]["Test1"].ToString();
            rdoTest2.SelectedValue = dt.Rows[i]["Test2"].ToString();
            rdoTest3.SelectedValue = dt.Rows[i]["Test3"].ToString();
            rdoTest4.SelectedValue = dt.Rows[i]["Test4"].ToString();
            rdoTest5.SelectedValue = dt.Rows[i]["Test5"].ToString();
            rdoTest6.SelectedValue = dt.Rows[i]["Test6"].ToString();
            txtPassingMark.Text= dt.Rows[i]["PassingMark"].ToString();
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        for (int i = 0; i < Grd.Rows.Count; i++)
        {
            Label ClassId = (Label)Grd.Rows[i].FindControl("ClassId");
            Label BranchId = (Label)Grd.Rows[i].FindControl("BranchId");
            RadioButtonList rdoTest1 = (RadioButtonList)Grd.Rows[i].FindControl("rdoTest1");
            RadioButtonList rdoTest2 = (RadioButtonList)Grd.Rows[i].FindControl("rdoTest2");
            RadioButtonList rdoTest3 = (RadioButtonList)Grd.Rows[i].FindControl("rdoTest3");
            RadioButtonList rdoTest4 = (RadioButtonList)Grd.Rows[i].FindControl("rdoTest4");
            RadioButtonList rdoTest5 = (RadioButtonList)Grd.Rows[i].FindControl("rdoTest5");
            RadioButtonList rdoTest6 = (RadioButtonList)Grd.Rows[i].FindControl("rdoTest6");
            TextBox txtPassingMark = (TextBox)Grd.Rows[i].FindControl("txtPassingMark");
            double PassingMark = 0;
            double.TryParse(txtPassingMark.Text.Trim(), out PassingMark);
            cmd = new SqlCommand();
            cmd.CommandText = "ICSETestPermissionProc";
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ClassID", ClassId.Text);
            cmd.Parameters.AddWithValue("@BranchId", BranchId.Text);
            cmd.Parameters.AddWithValue("@Test1", rdoTest1.SelectedValue);
            cmd.Parameters.AddWithValue("@Test2", rdoTest2.SelectedValue);
            cmd.Parameters.AddWithValue("@Test3", rdoTest3.SelectedValue);
            cmd.Parameters.AddWithValue("@Test4", rdoTest4.SelectedValue);
            cmd.Parameters.AddWithValue("@Test5", rdoTest5.SelectedValue);
            cmd.Parameters.AddWithValue("@Test6", rdoTest6.SelectedValue);
            cmd.Parameters.AddWithValue("@PassingMark", PassingMark.ToString());
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
            cmd.Parameters.AddWithValue("@Action", "insert");
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully", "S");
    }

    protected void ddlTest1H_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlTest1H = (DropDownList)Grd.HeaderRow.FindControl("ddlTest1H");
        if (ddlTest1H.SelectedIndex != 0)
        {
            for (int i = 0; i < Grd.Rows.Count; i++)
            {
                RadioButtonList rdoTest1 = (RadioButtonList)Grd.Rows[i].FindControl("rdoTest1");
                rdoTest1.SelectedValue = ddlTest1H.SelectedValue;
            }
        }
    }

    protected void ddlTest2H_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlTest2H = (DropDownList)Grd.HeaderRow.FindControl("ddlTest2H");
        if (ddlTest2H.SelectedIndex != 0)
        {
            for (int i = 0; i < Grd.Rows.Count; i++)
            {
                RadioButtonList rdoTest2 = (RadioButtonList)Grd.Rows[i].FindControl("rdoTest2");
                rdoTest2.SelectedValue = ddlTest2H.SelectedValue;
            }
        }
    }

    protected void ddlTest3H_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlTest3H = (DropDownList)Grd.HeaderRow.FindControl("ddlTest3H");
        if (ddlTest3H.SelectedIndex != 0)
        {
            for (int i = 0; i < Grd.Rows.Count; i++)
            {
                RadioButtonList rdoTest3 = (RadioButtonList)Grd.Rows[i].FindControl("rdoTest3");
                rdoTest3.SelectedValue = ddlTest3H.SelectedValue;
            }
        }
    }

    protected void ddlTest4H_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlTest4H = (DropDownList)Grd.HeaderRow.FindControl("ddlTest4H");
        if (ddlTest4H.SelectedIndex != 0)
        {
            for (int i = 0; i < Grd.Rows.Count; i++)
            {
                RadioButtonList rdoTest4 = (RadioButtonList)Grd.Rows[i].FindControl("rdoTest4");
                rdoTest4.SelectedValue = ddlTest4H.SelectedValue;
            }
        }
    }

    protected void ddlTest5H_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlTest5H = (DropDownList)Grd.HeaderRow.FindControl("ddlTest5H");
        if (ddlTest5H.SelectedIndex != 0)
        {
            for (int i = 0; i < Grd.Rows.Count; i++)
            {
                RadioButtonList rdoTest5 = (RadioButtonList)Grd.Rows[i].FindControl("rdoTest5");
                rdoTest5.SelectedValue = ddlTest5H.SelectedValue;
            }
        }
    }

    protected void ddlTest6H_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlTest6H = (DropDownList)Grd.HeaderRow.FindControl("ddlTest6H");
        if (ddlTest6H.SelectedIndex != 0)
        {
            for (int i = 0; i < Grd.Rows.Count; i++)
            {
                RadioButtonList rdoTest6 = (RadioButtonList)Grd.Rows[i].FindControl("rdoTest6");
                rdoTest6.SelectedValue = ddlTest6H.SelectedValue;
            }
        }
    }

    protected void txtPassingMark_TextChanged(object sender, EventArgs e)
    {
        TextBox txtPassingMarkH = (TextBox)Grd.HeaderRow.FindControl("txtPassingMarkH");
        for (int i = 0; i < Grd.Rows.Count; i++)
        {
            TextBox txtPassingMark = (TextBox)Grd.Rows[i].FindControl("txtPassingMark");
            txtPassingMark.Text = txtPassingMarkH.Text.Trim();
        }
    }
}
