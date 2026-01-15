using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class admin_AddClassinGroup : System.Web.UI.Page
{
    string sql = string.Empty;
    Campus oo = new Campus();
    BAL.GET_ClassGroupMaster BalGetObj = new BAL.GET_ClassGroupMaster();
    BAL.SET_ClassGroupMaster BalSetObj = new BAL.SET_ClassGroupMaster();
    Message mesg = new Message();
    DAL DalObj = new DAL();
    SqlConnection con = new SqlConnection();
    SqlCommand cmd = new SqlCommand();

    protected void Page_Load(object sender, EventArgs e)
    {
        Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file

        if (Session["SessionName"] == null || Session["BranchCode"] == null || Session["LoginName"] == null)
        {
            Response.Redirect("default.aspx");
        }
        con = oo.dbGet_connection();
        if (!IsPostBack)
        {
            CheckRepeterCheck();
        }
                
    }

    protected void loadClass()
    {
        BalGetObj.GroupId = RadioButtonList1.SelectedValue;
        BalGetObj.SessionName = Session["SessionName"].ToString();
        BalGetObj.BranchCode = Convert.ToInt16(Session["BranchCode"].ToString());
        BalGetObj.LoginName = Session["LoginName"].ToString();              
        Repeater1.DataSource = DalObj.GET_ClassGroupMaster(BalGetObj).Item1;
        Repeater1.DataBind();
    }

    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        clasbox.Visible = true;
        loadClass();
        CheckRepeterCheck();
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        string msg = "";
        try
        {           
            for (int i = 0; i < Repeater1.Items.Count; i++)
            {
                Label lblClassId = (Label)Repeater1.Items[i].FindControl("Label1");
                CheckBox Chk = (CheckBox)Repeater1.Items[i].FindControl("CheckBox1");
                BalSetObj.GroupId = RadioButtonList1.SelectedValue;
                BalSetObj.ClassId = Convert.ToInt16(lblClassId.Text);
                BalSetObj.IsActive = Chk.Checked;
                BalSetObj.SessionName = Session["SessionName"].ToString();
                BalSetObj.BranchCode = Convert.ToInt16(Session["BranchCode"].ToString());
                BalSetObj.LoginName = Session["LoginName"].ToString();

                msg = DalObj.Set_ClassGroupMaster(BalSetObj);
                if (RadioButtonList1.SelectedValue == "G1")
                {
                    RadioButtonList1.Items[1].Enabled = false;
                    RadioButtonList1.Items[0].Enabled = true;
                }
                if (RadioButtonList1.SelectedValue == "G2")
                {
                    RadioButtonList1.Items[0].Enabled = false;
                    RadioButtonList1.Items[1].Enabled = true;
                }

            }
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        BAL.textBoxList nonCleartxtlist = new BAL.textBoxList();
        nonCleartxtlist.Noofnoncleartxt = 0;
        //oo.MessageBoxforUpdatePanel(mesg.MessageType(msg, LinkButton1, nonCleartxtlist), LinkButton1);
        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, mesg.MessageType(msg, LinkButton1, nonCleartxtlist), "S");       

    }

    public void CheckRepeterCheck()
    {
        if (Repeater1.Items.Count > 0)
        {
            for (int i = 0; i < Repeater1.Items.Count; i++)
            {
                CheckBox Chk = (CheckBox)Repeater1.Items[i].FindControl("CheckBox1");
                if (Chk.Checked)
                {
                    LinkButton1.Enabled = true;
                    break;
                }
                else
                {
                    LinkButton1.Enabled = false;
                }
            }
        }
        else
        {
            LinkButton1.Enabled = false;
        }
        sql = "select distinct GroupId from dt_ClassGroupMaster where (GroupId='G1' or GroupId='G2') and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        var GroupId = oo.ReturnTag(sql, "GroupId");
        if (GroupId == "G1")
        {
            RadioButtonList1.Items[1].Enabled = false;
            RadioButtonList1.Items[0].Enabled = true;
        }
        if (GroupId == "G2")
        {
            RadioButtonList1.Items[0].Enabled = false;
            RadioButtonList1.Items[1].Enabled = true;
        }
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        sql = "delete from dt_ClassGroupMaster where (GroupId='G1' or GroupId='G2') and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        cmd.CommandText = sql;
        cmd.Connection = con;
        cmd.CommandType = CommandType.Text;
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Reset successfully.", "S");
        RadioButtonList1.Items[0].Enabled = true;
        RadioButtonList1.Items[1].Enabled = true;
    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        CheckRepeterCheck();
    }
}