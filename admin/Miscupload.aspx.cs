using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;


public partial class admin_SayllabusUpload : Page
{
    SqlConnection con = new SqlConnection();
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

            try
            {
                CheckValueADDDeleteUpdate();

            }
            catch (Exception) { }
            
            
            sql = "Select ClassName from ClassMaster";
            sql = sql + " where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            sql = sql + "  order by Id";
            oo.FillDropDownWithOutSelect(sql, DropDownList1, "ClassName");

        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        
        
        string PhotoPath = "~/SyllabusUpload/";
        
        if (FileUpload1.FileName.ToString().Trim().ToUpper() == lblFileName.Text.ToString().ToUpper())
        {
            FileUpload1.SaveAs(Server.MapPath(@PhotoPath + FileUpload1.FileName));
            //oo.MessageBox("Successfully Upload", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Upload successfully.", "S");


        }
        else
        {
            //oo.MessageBox("Sorry Unable to Upload", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry Unable to Upload!", "A");

        }
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblFileName.Text = spaceRemove(DropDownList1.SelectedValue.ToString()) + ".pdf";
    }


    public string spaceRemove(string xx)
    {
        int i = 0;
        string var = "";
        for (i = 0; i <= xx.Length - 1; i++)
        {
            if (xx[i] == ' ')
            {
                var = var + "_";

            }
            else
            {
                var = var + xx[i];
            }
        }
        return var;
    }


    public void PermissionGrant(int add1, LinkButton Ladd)
    {

        if (add1 == 1)
        {
            Ladd.Enabled = true;
        }
        else
        {
            Ladd.Enabled = false;
        }

    }
    public void CheckValueADDDeleteUpdate()
    {
        sql = " select LoginId,LoginName,Pass,SessionId,BranchId,LT.LoginTypeName,ltb.add1 as add1,ltb.delete1 as delete1,ltb.update1 as update1 from LoginTab LTb";
        sql = sql + " left join LoginType Lt on LTb.LoginTypeId=Lt.LoginTypeId where LT.LoginTypeName='" + Session["Logintype"] + "' and LTb.LoginName='" + Session["LoginName"] + "'";
#pragma warning disable 168
        int a, u, d;
#pragma warning restore 168
        a = Convert.ToInt32(oo.ReturnTag(sql, "add1"));

        PermissionGrant(a, (LinkButton)LinkButton1);
    }

   }
