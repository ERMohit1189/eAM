using System;

public partial class Admin_BracodeSetting : System.Web.UI.Page
{
    Campus oo = new Campus();
    string sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file

        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        if (!IsPostBack)
        {
            checkbool();
        }
    }

    protected void checkbool()
    {
        sql = "Select flag from BarcodeSetting";
        if (!string.IsNullOrEmpty(oo.ReturnTag(sql, "flag")))
        {
            RadioButtonList1.SelectedValue = oo.ReturnTag(sql, "flag");
        }
        else
        {
            RadioButtonList1.SelectedValue = "0";
        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        sql = "Select *from BarcodeSetting";
        if (!oo.Duplicate(sql))
        {
            if (RadioButtonList1.SelectedValue == "1")
            {
                sql = "insert into BarcodeSetting values('1','1')";
                oo.ProcedureDatabase(sql);
            }
            else
            {
                sql = "insert into BarcodeSetting values('1','0')";
                oo.ProcedureDatabase(sql);
            }
        }
        else
        {
            if (RadioButtonList1.SelectedValue == "1")
            {
                sql = "Update BarcodeSetting set flag='1'";
                oo.ProcedureDatabase(sql);
            }
            else
            {
                sql = "Update BarcodeSetting set flag='0'";
                oo.ProcedureDatabase(sql);
            }
        }

        //oo.MessageBoxforUpdatePanel("Updated successfully.", LinkButton1);
        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully.", "S");       

    }
}