using System;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class main_icon_selection_list : Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        con = oo.dbGet_connection();
        if (Session["LoginName"] == null)
        {
            Response.Redirect("default.aspx");
        }
        if (!IsPostBack)
        {
            ADDTopMenuItem();
        }
    }

    private void ADDTopMenuItem()
    {
        string sql = "";
        string isfor = "", value = "", table = "";
        if (drpUserType.SelectedItem.Text.ToUpper() == "ADMIN")
        {
            isfor = "Admin";
            value = "1";
            table = "menu_permission";
        }
        //else if (drpUserType.SelectedItem.Text.ToUpper() == "SUPERADMIN")
        //{
        //    isfor = "SuperAdmin";
        //    value = "1";
        //    table = "super_admin_menupermission";
        //}
        //sql = "SELECT Distinct MenuID,text,ParentClassName FROM Menueam Inner join " + table + " mp on mp.Menu_Id=Menueam.MenuID";
        //sql = sql + " where " + isfor + "=" + value + " and ParentId is NUll order by MenuID Asc";
        //sql = "SELECT Distinct MenuID,text,ParentClassName FROM Menueam Inner join menu_permission mp on mp.Menu_Id=Menueam.MenuID";
        //sql = sql + " where Admin=1 and ParentId is NUll order by MenuID Asc";
        sql = "SELECT Distinct MenuID,text,ParentClassName FROM Menueam where ParentId is NUll order by MenuID Asc";
        Repeater1.DataSource = oo.GridFill(sql);
        Repeater1.DataBind();
        if (Repeater1.Items.Count > 0)
        {
            for (int i = 0; i < Repeater1.Items.Count; i++)
            {
                Repeater rp2 = (Repeater)Repeater1.Items[i].FindControl("Repeater2");
                Label lblId = (Label)Repeater1.Items[i].FindControl("Label1");
                AddfirstChildMenu(rp2, lblId.Text.Trim(), table, isfor, value);
            }

        }
    }

    private void AddfirstChildMenu(Repeater rp, string parentid, string table, string isfor, string value)
    {
        sql = "SELECT Distinct MenuID,text,('" + isfor + "'+'/') as root,(Case When ISNULL(Url,'')='' Then '' Else Url End ) as URL,ParentClassName,ChildClassName FROM Menueam ";
        //sql = sql + " Inner join " + table + " mp on mp.Menu_Id=Menueam.MenuID";
        sql = sql + " where " + isfor + "=" + value + " and ParentId is not NUll and ParentID='" + parentid + "' and (Url='' or Url is null) order by MenuID Asc";
        rp.DataSource = oo.GridFill(sql);
        rp.DataBind();
    }

    private static string value = "";
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = (CheckBox)sender;
        RepeaterItem currentItem = (RepeaterItem)chk.NamingContainer;
        Label lblId = (Label)currentItem.FindControl("Label1");
        value = lblId.Text.Trim();
        if (chk.Checked)
        {
            myModal.Style.Add("display", "block");
            myModal.Attributes.Add("class", "modal fade in");
        }
        else
        {
            myModal.Style.Add("display", "none");

        }
    }
    protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = (CheckBox)sender;
        RepeaterItem currentItem = (RepeaterItem)chk.NamingContainer;
        Label lblId = (Label)currentItem.FindControl("Label1");
        value = lblId.Text.Trim();
        if (chk.Checked)
        {
            myModal.Style.Add("display", "block");
            myModal.Attributes.Add("class", "modal fade in");
        }
        else
        {
            myModal.Style.Add("display", "none");
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        myModal.Style.Add("display", "none");
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        sql = "Update Menueam set ParentClassName='" + RadioButtonList1.SelectedValue.ToString() + "' where Menuid='" + value + "'";
        oo.ProcedureDatabase(sql);
        BAL.objBal.MessageBoxforUpdatePanel("Well Done", Button1);
        ADDTopMenuItem();
        myModal.Style.Add("display", "none");
    }
}