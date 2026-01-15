using System;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class admin_NoticeRemarkforAdmitCard : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    SqlCommand cmd;
    string sql = "";
    Campus oo = new Campus();

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

            loadclassfrom();
            loadclassto();
            loadsection();
            loadgrid();
        }
    }

    public void loadclassfrom()
    {
        sql = "Select ClassName,Id from ClassMaster where sessionName='"+Session["SessionName"].ToString()+ "' and BranchCode=" + Session["BranchCode"] + "";
        oo.FillDropDown_withValue(sql, drpFromClass, "ClassName", "Id");
        oo.FillDropDown_withValue(sql, drpFromClass1, "ClassName", "Id");
    }

    public void loadclassto()
    {
        sql = "Select ClassName,Id from ClassMaster where sessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"] + "";
        oo.FillDropDown_withValue(sql, drpToClass, "ClassName", "Id");
    }

    public void loadsection()
    {
        sql = "Select SectionName,Id from SectionMaster where classNameid='"+drpFromClass.SelectedValue.ToString()+"' and sessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"] + "";
        oo.FillDropDown_withValue(sql, drpSection, "SectionName", "Id");
    }

    public void loadsection1()
    {
        sql = "Select SectionName from SectionMaster where classNameid='" + drpFromClass1.SelectedValue.ToString() + "' and sessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"] + "";
        oo.FillDropDown_withValue(sql, drpSection1, "SectionName", "SectionName");
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        if (txtRemark.Text != "")
        {
            save();
        }
    }

    public void save()
    {
        for (int i = drpFromClass.SelectedIndex; i <= drpToClass.SelectedIndex; i++)
        {
            sql = "Select *from NoticeRemarkforAdmitCard where ClassId='" + drpFromClass.Items[i].Value.ToString() + "' and SectionName='" + drpSection.SelectedItem.ToString() + "' and BranchCode=" + Session["BranchCode"] + " and Remark='" + txtRemark.Text.Trim()+"'";
            if (oo.Duplicate(sql)==false)
            {
                cmd = new SqlCommand();
                cmd.CommandText = "NoticeRemarkforAdmitCardProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Type", "Insert");
                cmd.Parameters.AddWithValue("@Id", "");
                cmd.Parameters.AddWithValue("@ClassId", drpFromClass.Items[i].Value.ToString());
                cmd.Parameters.AddWithValue("@SectionName", drpSection.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@Remark", txtRemark.Text.Trim());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                loadgrid();
                //oo.MessageBoxforUpdatePanel("Submitted successfully.", LinkButton1);
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");       

            }
        }

    }

    public void loadgrid()
    {
        sql = "Select nr.Id,nr.SectionName,nr.Remark,cm.ClassName from NoticeRemarkforAdmitCard nr";
        sql = sql + " inner join ClassMaster cm on cm.Id=nr.ClassId";
        sql = sql + " where cm.SessionName='" + Session["SessionName"].ToString() + "' and nr.SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"] + "";
        GridView1.DataSource = oo.GridFill(sql);
        GridView1.DataBind();
    }

    protected void drpFromClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadsection();
    }
    protected void link_Edit_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblId2 = (Label)chk.NamingContainer.FindControl("Label36");
     
        Label5.Text = lblId2.Text;
        sql = "Select ClassId,Remark from NoticeRemarkforAdmitCard";
        sql = sql + " where id='" + Label5.Text + "' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"] + "";
        drpFromClass1.SelectedValue = oo.ReturnTag(sql, "ClassId");
        txtRemark1.Text = oo.ReturnTag(sql, "Remark");
        loadsection1();
        sql = "Select SectionName,Remark from NoticeRemarkforAdmitCard";
        sql = sql + " where id='" + Label5.Text + "' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"] + "";
        drpSection1.SelectedValue= oo.ReturnTag(sql, "SectionName");
       
        Panel1_ModalPopupExtender.Show();
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        update();
    }

    public void update()
    {

            sql = "Select *from NoticeRemarkforAdmitCard where  ClassId='" + drpFromClass1.SelectedValue.ToString() + "' and SectionName='" + drpSection1.SelectedItem.ToString() + "' and BranchCode=" + Session["BranchCode"] + " and Remark='" + txtRemark1.Text.Trim() + "'";
            if (oo.Duplicate(sql) == false)
            {
                cmd = new SqlCommand();
                cmd.CommandText = "NoticeRemarkforAdmitCardProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Type", "Update");
                cmd.Parameters.AddWithValue("@Id", Label5.Text);
                cmd.Parameters.AddWithValue("@ClassId", drpFromClass1.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@SectionName", drpSection1.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@Remark", txtRemark1.Text.Trim());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                loadgrid();
                //oo.MessageBoxforUpdatePanel("Updated successfully.", LinkButton2);
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully.", "S");       

            }
            else
            {
                //oo.MessageBoxforUpdatePanel("Duplicate Entry.", LinkButton2);
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Duplicate Entry.", "A");       

            }
       
    }

    protected void drpFromClass1_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadsection1();
        Panel1_ModalPopupExtender.Show();
    }

    protected void link_Delete_Click(object sender, EventArgs e)
    {
        Button9.Focus();
        LinkButton chk = (LinkButton)sender;
        Label lblId2 = (Label)chk.NamingContainer.FindControl("Label37");
       
        lblvalue.Text = lblId2.Text;
        Panel2_ModalPopupExtender.Show();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        sql = "Delete From NoticeRemarkforAdmitCard where id='" + lblvalue.Text + "' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"] + "";
        cmd = new SqlCommand(sql, con);
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        loadgrid();
        //oo.MessageBoxforUpdatePanel("Deleted successfully.", btnDelete);
        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Deleted successfully.", "S");       

    }
}