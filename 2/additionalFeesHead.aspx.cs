using System;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class additionalFeesHead : System.Web.UI.Page
{
    Campus oo = new Campus();
    SqlConnection con = new SqlConnection();
    string sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file

        if (!IsPostBack)
        {
            grd_disp("-1", "-1");
            loadclass();
        }
    }

    public void loadclass()
    {
        sql = "Select ClassName,Id from ClassMaster Where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " order by CidOrder";
        oo.FillDropDown_withValue(sql, drpFromClass, "ClassName", "Id");
        oo.FillDropDown_withValue(sql, drpToClass, "ClassName", "Id");
    }

    public void grd_disp(string classIdFrom, string classIdTo)
    {
        sql = "select ROW_NUMBER() over(order bY ofh.Id) as Ids,ofh.Id,cm.ClassName,Gender,HeadName,Amount,ofh.SessionName,Concession ";
        sql = sql + "from  Other_fee_Collection_head_1 ofh inner join ClassMaster cm on cm.Id=ofh.ClassId ";
        sql = sql + "where ofh.SessionName='" + Session["SessionName"].ToString() + "' and ofh.BranchCode=" + Session["BranchCode"].ToString() + " and cm.BranchCode=" + Session["BranchCode"].ToString() + " and cm.SessionName='" + Session["SessionName"].ToString() + "' ";
        if (int.Parse(classIdFrom) > 0 && int.Parse(classIdFrom) <= int.Parse(classIdTo))
        {
            if (classIdFrom != "-1" && classIdTo != "-1")
            {
                sql = sql + "and ofh.classid between " + classIdFrom.ToString() + " and " + classIdTo.ToString() + "";
            }
        }
        sql = sql + " order by Cidorder";
        GridView1.DataSource = oo.GridFill(sql);
        GridView1.DataBind();
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            Label ClassName = (Label)GridView1.Rows[i].FindControl("lbl_ClassName");
            LinkButton Edit = (LinkButton)GridView1.Rows[i].FindControl("Edit");
            LinkButton Delete = (LinkButton)GridView1.Rows[i].FindControl("Delete");
            sql = "select ClassName from AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "', " + Session["BranchCode"].ToString() + ") where srno in (select distinct Srno from Other_fee_collection_1 where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + ") and ClassName='" + ClassName + "'";
            if (oo.Duplicate(sql))
            {
                Edit.Text = "<i class='fa fa-lock'><i>";
                Delete.Text = "<i class='fa fa-lock'><i>";
                Edit.Enabled = false;
                Delete.Enabled = false;
            }
            
        }
    }

    protected void Delete_Click(object sender, EventArgs e)
    {
        Button4.Focus();
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("Label37");
        string ss = lblId.Text;
        Label3.Text = lblId.Text;
        ModalPopupExtender2.Show();
    }
    protected void Edit_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("Label36");
        Label4.Text = lblId.Text;
        sql = "select HeadName,Amount,Concession from Other_fee_Collection_head_1 where Id='" + Label4.Text + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        TextBox2.Text = oo.ReturnTag(sql, "HeadName");
        TextBox4.Text = oo.ReturnTag(sql, "Amount");
        TextBox6.Text = oo.ReturnTag(sql, "Concession");
        ModalPopupExtender1.Show();

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        SqlCommand Othercollection = new SqlCommand("Other_fee_Collection_head_1_proc", con);
        Othercollection.CommandType = CommandType.StoredProcedure;
        Othercollection.Parameters.AddWithValue("@Id", Label4.Text);
        Othercollection.Parameters.AddWithValue("@HeadName", TextBox2.Text);
        Othercollection.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
        Othercollection.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
        Othercollection.Parameters.AddWithValue("@task", "Update");
        Othercollection.Parameters.AddWithValue("@Amount", TextBox4.Text);
        Othercollection.Parameters.AddWithValue("@Concession", TextBox6.Text == "" ? "0" : TextBox6.Text);
        Othercollection.Parameters.AddWithValue("@ClassId", '1');
        Othercollection.Parameters.AddWithValue("@Gender", "");
        con.Open();
        Othercollection.ExecuteNonQuery();
        con.Close();
        grd_disp(drpFromClass.SelectedValue, drpToClass.SelectedValue);
        //oo.MessageBox("Updated Successfully", this.Page);
        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully", "S");

    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        SqlCommand delete = new SqlCommand("Delete From Other_fee_Collection_head_1 where Id='" + Label3.Text + "' and BranchCode=" + Session["BranchCode"].ToString() + "", con);
        con.Open();
        delete.ExecuteNonQuery();
        con.Close();
        grd_disp(drpFromClass.SelectedValue, drpToClass.SelectedValue);
        //oo.MessageBox("Deleted Successfully", this.Page);
        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Deleted successfully", "S");


    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        for (int i = drpFromClass.SelectedIndex; i <= drpToClass.SelectedIndex; i++)
        {
            if (ddlGender.SelectedIndex == 0)
            {
                for (int j = 1; j < ddlGender.Items.Count; j++)
                {
                    SqlCommand Othercollection = new SqlCommand("Other_fee_Collection_head_1_proc", con);
                    Othercollection.CommandType = CommandType.StoredProcedure;
                    Othercollection.Parameters.AddWithValue("@HeadName", TextBox1.Text);
                    Othercollection.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                    Othercollection.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                    Othercollection.Parameters.AddWithValue("@task", "Insert");
                    Othercollection.Parameters.AddWithValue("@Id", "0");
                    Othercollection.Parameters.AddWithValue("@Amount", TextBox3.Text);
                    Othercollection.Parameters.AddWithValue("@Concession", TextBox5.Text == "" ? "0" : TextBox5.Text);
                    Othercollection.Parameters.AddWithValue("@ClassId", drpFromClass.Items[i].Value);
                    Othercollection.Parameters.AddWithValue("@Gender", ddlGender.Items[j].Text);
                    sql = "Select *from Other_fee_Collection_head_1 where Gender='" + ddlGender.Items[j].Text + "' and ClassId='" + drpFromClass.Items[i].Value + "' and HeadName='" + TextBox1.Text + "' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
                    if (oo.Duplicate(sql) == false)
                    {
                        con.Open();
                        Othercollection.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            else
            {
                SqlCommand Othercollection = new SqlCommand("Other_fee_Collection_head_1_proc", con);
                Othercollection.CommandType = CommandType.StoredProcedure;
                Othercollection.Parameters.AddWithValue("@HeadName", TextBox1.Text);
                Othercollection.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                Othercollection.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                Othercollection.Parameters.AddWithValue("@task", "Insert");
                Othercollection.Parameters.AddWithValue("@Id", "0");
                Othercollection.Parameters.AddWithValue("@Amount", TextBox3.Text);
                Othercollection.Parameters.AddWithValue("@Concession", TextBox5.Text == "" ? "0" : TextBox5.Text);
                Othercollection.Parameters.AddWithValue("@ClassId", drpFromClass.Items[i].Value);
                Othercollection.Parameters.AddWithValue("@Gender", ddlGender.SelectedItem.Text);
                sql = "Select *from Other_fee_Collection_head where Gender='" + ddlGender.SelectedItem.Text + "' and ClassId='" + drpFromClass.Items[i].Value + "' and HeadName='" + TextBox1.Text + "' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
                if (oo.Duplicate(sql) == false)
                {
                    con.Open();
                    Othercollection.ExecuteNonQuery();
                    con.Close();
                }
            }
            //oo.MessageBoxforUpdatePanel("Submitted Successfully", LinkButton1);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully", "S");
        }
        grd_disp(drpFromClass.SelectedValue, drpToClass.SelectedValue);
        TextBox1.Text = "";
        TextBox3.Text = "";
    }

    protected void drpFromClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        grd_disp(drpFromClass.SelectedValue, drpToClass.SelectedValue);
    }

    protected void drpToClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        grd_disp(drpFromClass.SelectedValue, drpToClass.SelectedValue);
    }
}