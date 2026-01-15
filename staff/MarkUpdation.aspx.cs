using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class staff_MarkUpdation : Page
{
    SqlConnection con = new SqlConnection();
    SqlCommand cmd = new SqlCommand();
    Campus oo = new Campus();
    string sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        con = oo.dbGet_connection();
        if (!IsPostBack)
        {
            loadclass();
            loadsection();
            loadSubject();

        }
    }
    
    public void loadclass()
    {
        sql = "Select Distinct ClassName,sctm.ClassId as Id,CIDOrder from SubjectClassTeacherMaster sctm";
        sql = sql + " inner join ClassMaster cm on cm.Id=sctm.ClassId";
        sql = sql + " where cm.SessionName='" + Session["SessionName"] + "' and cm.BranchCode=" + Session["BranchCode"].ToString() + " and sctm.BranchCode=" + Session["BranchCode"].ToString() + " and sctm.SessionName='" + Session["SessionName"] + "' and Ecode='" + Session["LoginName"].ToString() + "' Order by CIDOrder";
        oo.FillDropDown_withValue(sql, drpclass, "ClassName", "Id");
    }
    public void loadSubject()
    {
        sql = "Select sm.SubjectName,sctm.Subjectid as Id from SubjectClassTeacherMaster sctm";
        sql = sql + " inner join SubjectMaster sm on sm.Id=sctm.Subjectid";
        sql = sql + " where Ecode='" + Session["LoginName"].ToString() + "' and sm.BranchCode=" + Session["BranchCode"].ToString() + " and sctm.BranchCode=" + Session["BranchCode"].ToString() + " and sctm.ClassId='" + drpclass.SelectedValue.ToString() + "'";
        oo.FillDropDown_withValue(sql, drpSubject, "SubjectName", "Id");
        drpSubject.Items.Insert(0, "<--Select-->");
    }
    public void loadsection()
    {
        sql = "Select SectionName from SectionMaster where ClassNameId='" + drpclass.SelectedValue.ToString() + "'";
        sql = sql + " and   BranchCode=" + Session["BranchCode"].ToString() + " and SessionName='" + Session["SessionName"].ToString() + "'";
        oo.FillDropDown(sql, drpsection, "SectionName");
    }
    protected void drpclass_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadsection();
        loadSubject();
    }
    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {
        TextBox txt = (TextBox)sender;
        string str = txt.Text;
        bool result;
        double value;
        result = double.TryParse(str, out value);
        if (result == false)
        {
            fortext1(sender);
        }
        else
        {
            int maxmarks = 20;
            if (Convert.ToDouble(str) > maxmarks)
            {
                oo.MessageBoxforUpdatePanel("Maximum Marks is " + maxmarks, txt);
                txt.Text = "";
            }
            else
            {
                fortext1(sender);
            }

        }


    }
    public void fortext1(object sender)
    {
        TextBox txt = (TextBox)sender;
        string str = txt.Text;
        bool result;
        GridViewRow currentrow = (GridViewRow)((Control)sender).NamingContainer;
        Label Label2 = (Label)currentrow.FindControl("Label2");
        Label Label3 = (Label)currentrow.FindControl("Label3");
        Label Label4 = (Label)currentrow.FindControl("Label4");

        TextBox Text2 = (TextBox)currentrow.FindControl("TextBox2");
        TextBox Text3 = (TextBox)currentrow.FindControl("TextBox3");
        TextBox Text4 = (TextBox)currentrow.FindControl("TextBox4");
        TextBox Text5 = (TextBox)currentrow.FindControl("TextBox5");
        double num1, num2, num3, num4, num5;

        if (str != "")
        {
            result = double.TryParse(str, out num1);
            if (result == true)
            {
                num1 = Convert.ToDouble(str);
            }
            else
            {
                num1 = 0;
            }
        }
        else
        {
            num1 = 0;
        }
        if (Text2.Text != "")
        {
            result = double.TryParse(Text2.Text, out num2);
            if (result == true)
            {
                num2 = Convert.ToDouble(Text2.Text);
            }
        }
        else
        {
            num2 = 0;
        }
        if (Text3.Text != "")
        {
            result = double.TryParse(Text3.Text, out num3);
            if (result == true)
            {
                num3 = Convert.ToDouble(Text3.Text);
            }
        }
        else
        {
            num3 = 0;
        }
        if (Text4.Text != "")
        {
            result = double.TryParse(Text4.Text, out num4);
            if (result == true)
            {
                num4 = Convert.ToDouble(Text4.Text);
            }
        }
        else
        {
            num4 = 0;
        }
        if (Text5.Text != "")
        {
            result = double.TryParse(Text5.Text, out num5);
            if (result == true)
            {
                num5 = Convert.ToDouble(Text5.Text);
            }
        }
        else
        {
            num5 = 0;
        }

        Label2.Text = (num1 + num2 + num3 + num4 + num5).ToString();
        double percentle = ((num1 + num2 + num3 + num4 + num5) * 10) / 60;
        Label3.Text = (Math.Round(percentle, 1)).ToString();
        Label4.Text = grade(Math.Round(percentle, 1));
        //checkgrade(Math.Round(percentle, 1), sender, "TextBox1");
        Text2.Focus();
    }
    protected void TextBox2_TextChanged(object sender, EventArgs e)
    {
        TextBox txt = (TextBox)sender;
        string str = txt.Text;
        bool result;
        double value;
        result = double.TryParse(str, out value);
        if (result == false)
        {
            fortext2(sender);
        }
        else
        {
            int maxmarks = 15;
            if (Convert.ToDouble(str) > maxmarks)
            {
                oo.MessageBoxforUpdatePanel("Maximum Marks is " + maxmarks, txt);
                txt.Text = "";
            }
            else
            {
                fortext2(sender);
            }

        }
    }
    public void fortext2(object sender)
    {
        TextBox txt = (TextBox)sender;
        string str = txt.Text;
        GridViewRow currentrow = (GridViewRow)((Control)sender).NamingContainer;
        Label Label2 = (Label)currentrow.FindControl("Label2");
        Label Label3 = (Label)currentrow.FindControl("Label3");
        Label Label4 = (Label)currentrow.FindControl("Label4");

        TextBox Text1 = (TextBox)currentrow.FindControl("TextBox1");
        TextBox Text3 = (TextBox)currentrow.FindControl("TextBox3");
        TextBox Text4 = (TextBox)currentrow.FindControl("TextBox4");
        TextBox Text5 = (TextBox)currentrow.FindControl("TextBox5");
        double num1, num2, num3, num4, num5;
        bool result;
        if (str != "")
        {
            result = double.TryParse(str, out num1);
            if (result == true)
            {
                num1 = Convert.ToDouble(str);
            }
            else
            {
                num1 = 0;
            }
        }
        else
        {
            num1 = 0;
        }
        if (Text1.Text != "")
        {
            result = double.TryParse(Text1.Text, out num2);
            if (result == true)
            {
                num2 = Convert.ToDouble(Text1.Text);
            }
        }
        else
        {
            num2 = 0;
        }
        if (Text3.Text != "")
        {
            result = double.TryParse(Text3.Text, out num3);
            if (result == true)
            {
                num3 = Convert.ToDouble(Text3.Text);
            }
        }
        else
        {
            num3 = 0;
        }
        if (Text4.Text != "")
        {
            result = double.TryParse(Text4.Text, out num4);
            if (result == true)
            {
                num4 = Convert.ToDouble(Text4.Text);
            }
        }
        else
        {
            num4 = 0;
        }
        if (Text5.Text != "")
        {
            result = double.TryParse(Text5.Text, out num5);
            if (result == true)
            {
                num5 = Convert.ToDouble(Text5.Text);
            }
        }
        else
        {
            num5 = 0;
        }

        Label2.Text = (num1 + num2 + num3 + num4 + num5).ToString();
        double percentle = ((num1 + num2 + num3 + num4 + num5) * 10) / 60;
        Label3.Text = (Math.Round(percentle, 1)).ToString();
        Label4.Text = grade(Math.Round(percentle, 1));
        //checkgrade(Math.Round(percentle, 1), sender, "TextBox2");
        Text3.Focus();
    }
    protected void TextBox3_TextChanged(object sender, EventArgs e)
    {
        TextBox txt = (TextBox)sender;
        string str = txt.Text;
        bool result;
        double value;
        result = double.TryParse(str, out value);
        if (result == false)
        {
            fortext3(sender);
        }
        else
        {
            int maxmarks = 15;
            if (Convert.ToDouble(str) > maxmarks)
            {
                oo.MessageBoxforUpdatePanel("Maximum Marks is " + maxmarks, txt);
                txt.Text = "";
            }
            else
            {
                fortext3(sender);
            }

        }
    }
    public void fortext3(object sender)
    {
        TextBox txt = (TextBox)sender;
        string str = txt.Text;
        GridViewRow currentrow = (GridViewRow)((Control)sender).NamingContainer;
        Label Label2 = (Label)currentrow.FindControl("Label2");
        Label Label3 = (Label)currentrow.FindControl("Label3");
        Label Label4 = (Label)currentrow.FindControl("Label4");

        TextBox Text2 = (TextBox)currentrow.FindControl("TextBox2");
        TextBox Text1 = (TextBox)currentrow.FindControl("TextBox1");
        TextBox Text4 = (TextBox)currentrow.FindControl("TextBox4");
        TextBox Text5 = (TextBox)currentrow.FindControl("TextBox5");
        double num1, num2, num3, num4, num5;
        bool result;
        if (str != "")
        {
            result = double.TryParse(str, out num1);
            if (result == true)
            {
                num1 = Convert.ToDouble(str);
            }
        }
        else
        {
            num1 = 0;
        }
        if (Text2.Text != "")
        {
            result = double.TryParse(Text2.Text, out num2);
            if (result == true)
            {
                num2 = Convert.ToDouble(Text2.Text);
            }
        }
        else
        {
            num2 = 0;
        }
        if (Text1.Text != "")
        {
            result = double.TryParse(Text1.Text, out num3);
            if (result == true)
            {
                num3 = Convert.ToDouble(Text1.Text);
            }
        }
        else
        {
            num3 = 0;
        }
        if (Text4.Text != "")
        {
            result = double.TryParse(Text4.Text, out num4);
            if (result == true)
            {
                num4 = Convert.ToDouble(Text4.Text);
            }
        }
        else
        {
            num4 = 0;
        }
        if (Text5.Text != "")
        {
            result = double.TryParse(Text5.Text, out num5);
            if (result == true)
            {
                num5 = Convert.ToDouble(Text5.Text);
            }
        }
        else
        {
            num5 = 0;
        }

        Label2.Text = (num1 + num2 + num3 + num4 + num5).ToString();
        double percentle = ((num1 + num2 + num3 + num4 + num5) * 10) / 60;
        Label3.Text = (Math.Round(percentle, 1)).ToString();
        Label4.Text = grade(Math.Round(percentle, 1));
        //checkgrade(Math.Round(percentle, 1), sender, "TextBox3");
        Text4.Focus();
    }
    protected void TextBox4_TextChanged(object sender, EventArgs e)
    {
        TextBox txt = (TextBox)sender;
        string str = txt.Text;
        bool result;
        double value;
        result = double.TryParse(str, out value);
        if (result == false)
        {
            fortext4(sender);
        }
        else
        {
            int maxmarks = 5;
            if (Convert.ToDouble(str) > maxmarks)
            {
                oo.MessageBoxforUpdatePanel("Maximum Marks is " + maxmarks, txt);
                txt.Text = "";
            }
            else
            {
                fortext4(sender);
            }

        }

    }
    public void fortext4(object sender)
    {
        TextBox txt = (TextBox)sender;
        string str = txt.Text;
        GridViewRow currentrow = (GridViewRow)((Control)sender).NamingContainer;
        Label Label2 = (Label)currentrow.FindControl("Label2");
        Label Label3 = (Label)currentrow.FindControl("Label3");
        Label Label4 = (Label)currentrow.FindControl("Label4");

        TextBox Text2 = (TextBox)currentrow.FindControl("TextBox2");
        TextBox Text3 = (TextBox)currentrow.FindControl("TextBox3");
        TextBox Text1 = (TextBox)currentrow.FindControl("TextBox1");
        TextBox Text5 = (TextBox)currentrow.FindControl("TextBox5");
        double num1, num2, num3, num4, num5;

        bool result;
        if (str != "")
        {
            result = double.TryParse(str, out num1);
            if (result == true)
            {
                num1 = Convert.ToDouble(str);
            }
            else
            {
                num1 = 0;
            }
        }
        else
        {
            num1 = 0;
        }
        if (Text1.Text != "")
        {
            result = double.TryParse(Text2.Text, out num2);
            if (result == true)
            {
                num2 = Convert.ToDouble(Text2.Text);
            }
        }
        else
        {
            num2 = 0;
        }
        if (Text3.Text != "")
        {
            result = double.TryParse(Text3.Text, out num3);
            if (result == true)
            {
                num3 = Convert.ToDouble(Text3.Text);
            }
        }
        else
        {
            num3 = 0;
        }
        if (Text1.Text != "")
        {
            result = double.TryParse(Text1.Text, out num4);
            if (result == true)
            {
                num4 = Convert.ToDouble(Text1.Text);
            }
        }
        else
        {
            num4 = 0;
        }
        if (Text5.Text != "")
        {
            result = double.TryParse(Text5.Text, out num5);
            if (result == true)
            {
                num5 = Convert.ToDouble(Text5.Text);
            }
        }
        else
        {
            num5 = 0;
        }

        Label2.Text = (num1 + num2 + num3 + num4 + num5).ToString();
        double percentle = ((num1 + num2 + num3 + num4 + num5) * 10) / 60;
        Label3.Text = (Math.Round(percentle, 1)).ToString();
        Label4.Text = grade(Math.Round(percentle, 1));
        //checkgrade(Math.Round(percentle, 1), sender, "TextBox4");
        Text5.Focus();
    }
    protected void TextBox5_TextChanged(object sender, EventArgs e)
    {

        TextBox txt = (TextBox)sender;
        string str = txt.Text;
        bool result;
        double value;
        result = double.TryParse(str, out value);
        if (result == false)
        {
            fortext5(sender);
        }
        else
        {
            int maxmarks = 5;
            if (Convert.ToDouble(str) > maxmarks)
            {
                oo.MessageBoxforUpdatePanel("Maximum Marks is " + maxmarks, txt);
                txt.Text = "";
            }
            else
            {
                fortext5(sender);
            }

        }
    }
    public void fortext5(object sender)
    {
        TextBox txt = (TextBox)sender;
        string str = txt.Text;
        GridViewRow currentrow = (GridViewRow)((Control)sender).NamingContainer;
        Label Label2 = (Label)currentrow.FindControl("Label2");
        Label Label3 = (Label)currentrow.FindControl("Label3");
        Label Label4 = (Label)currentrow.FindControl("Label4");

        TextBox Text2 = (TextBox)currentrow.FindControl("TextBox2");
        TextBox Text3 = (TextBox)currentrow.FindControl("TextBox3");
        TextBox Text4 = (TextBox)currentrow.FindControl("TextBox4");
        TextBox Text1 = (TextBox)currentrow.FindControl("TextBox1");
        double num1, num2, num3, num4, num5;
        bool result;
        if (str != "")
        {
            result = double.TryParse(str, out num1);
            if (result == true)
            {
                num1 = Convert.ToDouble(str);
            }
            else
            {
                num1 = 0;
            }
        }
        else
        {
            num1 = 0;
        }
        if (Text1.Text != "")
        {
            result = double.TryParse(Text2.Text, out num2);
            if (result == true)
            {
                num2 = Convert.ToDouble(Text2.Text);
            }
        }
        else
        {
            num2 = 0;
        }
        if (Text3.Text != "")
        {
            result = double.TryParse(Text3.Text, out num3);
            if (result == true)
            {
                num3 = Convert.ToDouble(Text3.Text);
            }
        }
        else
        {
            num3 = 0;
        }
        if (Text4.Text != "")
        {
            result = double.TryParse(Text4.Text, out num4);
            if (result == true)
            {
                num4 = Convert.ToDouble(Text4.Text);
            }
        }
        else
        {
            num4 = 0;
        }
        if (Text1.Text != "")
        {
            result = double.TryParse(Text1.Text, out num5);
            if (result == true)
            {
                num5 = Convert.ToDouble(Text1.Text);
            }
        }
        else
        {
            num5 = 0;
        }

        Label2.Text = (num1 + num2 + num3 + num4 + num5).ToString();
        double percentle = ((num1 + num2 + num3 + num4 + num5) * 10) / 60;
        Label3.Text = (Math.Round(percentle, 1)).ToString();
        Label4.Text = grade(Math.Round(percentle, 1));
    }
    public void checkgrade(double percentle, object sender,string TextBox)
    {
        if (grade(percentle) == "False")
        {
            GridViewRow currentrow = (GridViewRow)((Control)sender).NamingContainer;
            TextBox Text5 = (TextBox)currentrow.FindControl(TextBox);
            Label Label2 = (Label)currentrow.FindControl("Label2");
            Label Label3 = (Label)currentrow.FindControl("Label3");
            Label Label4 = (Label)currentrow.FindControl("Label4");
            Text5.Text = "";
            Label2.Text = "";
            Label3.Text = "";
            Label4.Text = "";
        }
    }
    public string grade(double percentle)
    {
        if (percentle < 0)
        {
            return "A";
        }
        else
        {
            if (percentle >= 1 && percentle < 2)
            {
                return "E2";
            }
            else if (percentle >= 2 && percentle < 3)
            {
                return "E1";
            }
            else if (percentle >= 3 && percentle < 4)
            {
                return "D";
            }
            else if (percentle >= 4 && percentle < 5)
            {
                return "C2";
            }
            else if (percentle >= 5 && percentle < 6)
            {
                return "C1";
            }
            else if (percentle >= 6 && percentle < 7)
            {
                return "B2";
            }
            else if (percentle >= 7 && percentle < 8)
            {
                return "B1";
            }
            else if (percentle >= 8 && percentle < 9)
            {
                return "A2";
            }
            else if (percentle >= 9 && percentle <= 10)
            {
                return "A1";
            }
            else
            {
                return "False";
            }
        }
        
        
    }
    protected void drpsection_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadgrid();
        checkpermission(drpSubject);
    }
    protected void drpSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadgrid();
        checkpermission(drpSubject);
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {

        GridView1.EditIndex = e.NewEditIndex;
        loadgrid();
    }
    public void loadgrid()
    {
        sql = "Select CCE.SrNo,(SG.FirstName+' '+SG.MiddleName+' '+SG.LastName) as Name,UT,ACT1,ACT2,HW_CW,ATT,Total,TenPercent,Grade,CCE.Id  from CCE";
        sql = sql + " inner join StudentGenaralDetail SG on SG.SrNo=CCE.SrNo";
        sql = sql + " inner join ClassMaster cm on cm.Id=CCE.ClassId";
        sql = sql + " inner join SubjectMaster sm on sm.Id=CCE.SubjectId";
        sql = sql + " inner join SectionMaster scm on scm.SectionName=CCE.SectionName";
        sql = sql + " where CCE.Evaluation='" + drpEval.SelectedItem.ToString() + "' and CCE.ClassId='" + drpclass.SelectedValue.ToString() + "' and CCE.SectionName='" + drpsection.SelectedValue.ToString() + "' and scm.ClassNameId='" + drpclass.SelectedValue.ToString() + "' and CCE.SubjectId='" + drpSubject.SelectedValue.ToString() + "'";
        sql = sql + " and scm.SessionName='" + Session["SessionName"].ToString() + "' and CCE.SessionName='" + Session["SessionName"].ToString() + "' and cm.SessionName='" + Session["SessionName"].ToString() + "'";
        sql = sql + " and sm.SessionName='" + Session["SessionName"].ToString() + "' and SG.SessionName='" + Session["SessionName"].ToString() + "'";
        sql = sql + "  and cce.BranchCode=" + Session["BranchCode"].ToString() + " and sg.BranchCode=" + Session["BranchCode"].ToString() + " and cm.BranchCode=" + Session["BranchCode"].ToString() + " and sm.BranchCode=" + Session["BranchCode"].ToString() + "  and scm.BranchCode=" + Session["BranchCode"].ToString() + "  ";
        GridView1.DataSource = oo.GridFill(sql);
        GridView1.DataBind();

    }

    public void checkpermission(Control ctrl)
    {
         sql = "Select Permission from PermissionForMarksUpdate where EmpCode='" + Session["LoginName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
         if (oo.ReturnTag(sql, "Permission") == "true")
         {
             if (GridView1.Rows.Count > 0)
             {
                 foreach (GridViewRow gvr in GridView1.Rows)
                 {
                     LinkButton lnkEdit = (LinkButton)gvr.FindControl("lnkEdit");
                     lnkEdit.Enabled = true;
                 }
             }
         }
         else
         {
             if (GridView1.Rows.Count > 0)
             {
                 foreach (GridViewRow gvr in GridView1.Rows)
                 {
                     LinkButton lnkEdit = (LinkButton)gvr.FindControl("lnkEdit");
                     lnkEdit.Enabled = false;
                 }
                 oo.MessageBoxforUpdatePanel("Permission not granted", ctrl);
             }
         }
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow row = (GridViewRow)GridView1.Rows[e.RowIndex];
        Label lblid = (Label)row.FindControl("lblid");

        Session["id"] = Convert.ToInt32(lblid.Text);

        TextBox txtUT = (TextBox)row.FindControl("TextBox1");
        TextBox txtACT1 = (TextBox)row.FindControl("TextBox2");
        TextBox txtACT2 = (TextBox)row.FindControl("TextBox3");
        TextBox txtHWCW = (TextBox)row.FindControl("TextBox4");
        TextBox txtATT = (TextBox)row.FindControl("TextBox5");
        Label lblTotal = (Label)row.FindControl("Label2");
        Label lblPercent = (Label)row.FindControl("Label3");
        Label lblGrade = (Label)row.FindControl("Label4");
        sql = "Update CCE set UT='" + txtUT.Text.Trim() + "',ACT1='" + txtACT1.Text.Trim() + "',ACT2='" + txtACT2.Text.Trim() + "',";
        sql = sql + " HW_CW='" + txtHWCW.Text.Trim() + "' ,ATT='" + txtATT.Text.Trim() + "',";
        sql = sql + " Total='" + lblTotal.Text + "' ,TenPercent='" + lblPercent.Text + "',Grade='" + lblGrade.Text + "'";
        sql = sql + " where id='" + Session["id"].ToString() + "'";
        cmd = new SqlCommand(sql, con);
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        oo.MessageBox("Updated successfully", this.Page);
        GridView1.EditIndex = -1;
        loadgrid();
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        loadgrid();
    }
    protected void drpEval_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}