using System;
using System.Web.UI;

public partial class admin_RulesForShibling : Page
{
    BAL.Set_RulesForSibling objBAL = new BAL.Set_RulesForSibling();
    string sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((string)Session["LoginName"] == "" || (string)Session["BranchCode"] == "" || (string)Session["SessionName"] == "" || Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (!IsPostBack)
        {
            loadRecord();
        }
    }
    protected void lnkSubmit_Click(object sender, EventArgs e)
    {
        save();
    }

    private void save()
    {
        string msg = "";

        for (int i = 1; i <= 6; i++)
        {
            int flagvalue;
            objBAL.rule1 = RadioButtonList1.SelectedValue.ToString();
            objBAL.rule2 = RadioButtonList2.SelectedValue.ToString();
            objBAL.noofSibling = i;
            if (i == 1)
            {
                objBAL.discountType = DropDownList1.SelectedValue.ToString();
                objBAL.discountValue = int.TryParse(TextBox1.Text, out flagvalue) ? Convert.ToInt16(TextBox1.Text) : 1;
            }
            else if (i == 2)
            {
                objBAL.discountType = DropDownList2.SelectedValue.ToString();
                objBAL.discountValue = int.TryParse(TextBox2.Text, out flagvalue) ? Convert.ToInt16(TextBox2.Text) : 1;
            }
            else if (i == 3)
            {
                objBAL.discountType = DropDownList3.SelectedValue.ToString();
                objBAL.discountValue = int.TryParse(TextBox3.Text, out flagvalue) ? Convert.ToInt16(TextBox3.Text) : 1;
            }
            else if (i == 4)
            {
                objBAL.discountType = DropDownList4.SelectedValue.ToString();
                objBAL.discountValue = int.TryParse(TextBox4.Text, out flagvalue) ? Convert.ToInt16(TextBox4.Text) : 1;
            }
            else if (i == 5)
            {
                objBAL.discountType = DropDownList5.SelectedValue.ToString();
                objBAL.discountValue = int.TryParse(TextBox5.Text, out flagvalue) ? Convert.ToInt16(TextBox5.Text) : 1;
            }
            else if (i == 6)
            {
                objBAL.discountType = DropDownList6.SelectedValue.ToString();
                objBAL.discountValue = int.TryParse(TextBox6.Text, out flagvalue) ? Convert.ToInt16(TextBox6.Text) : 1;
            }
            objBAL.SessionName = Session["SessionName"].ToString();
            objBAL.LoginName = Session["LoginName"].ToString();
            objBAL.BranchCode = Convert.ToInt16(Session["BranchCode"].ToString());

            msg = DAL.objDal.Set_RulesForShibling(objBAL);

            BAL.objBal.MessageBoxforUpdatePanel(objBAL.MessageType(msg, new Control(), objBAL), lnkSubmit);           
        }
        loadRecord();
    }

    private void loadRecord()
    {
        sql = "Select Rule1,Rule2 from RulesForSibling where SessionName='" + Session["SessionName"].ToString() + "'";
        RadioButtonList1.SelectedValue = BAL.objBal.ReturnTag(sql, "Rule1");
        RadioButtonList2.SelectedValue = BAL.objBal.ReturnTag(sql, "Rule2");

        for (int i = 1; i <= 6; i++)
        {
            sql = "Select DiscountType,DiscountValue from RulesForSibling where SessionName='" + Session["SessionName"].ToString() + "' and NoofSibling=" + i;
            if (i == 1)
            {
                DropDownList1.SelectedValue = BAL.objBal.ReturnTag(sql, "DiscountType");
                TextBox1.Text = BAL.objBal.ReturnTag(sql, "DiscountValue");
            }
            else if (i == 2)
            {
                DropDownList2.SelectedValue = BAL.objBal.ReturnTag(sql, "DiscountType");
                TextBox2.Text = BAL.objBal.ReturnTag(sql, "DiscountValue");
            }
            else if (i == 3)
            {
                DropDownList3.SelectedValue = BAL.objBal.ReturnTag(sql, "DiscountType");
                TextBox3.Text = BAL.objBal.ReturnTag(sql, "DiscountValue");
            }
            else if (i == 4)
            {
                DropDownList4.SelectedValue = BAL.objBal.ReturnTag(sql, "DiscountType");
                TextBox4.Text = BAL.objBal.ReturnTag(sql, "DiscountValue");
            }
            else if (i == 5)
            {
                DropDownList5.SelectedValue = BAL.objBal.ReturnTag(sql, "DiscountType");
                TextBox5.Text = BAL.objBal.ReturnTag(sql, "DiscountValue");
            }
            else if (i == 6)
            {
                DropDownList6.SelectedValue = BAL.objBal.ReturnTag(sql, "DiscountType");
                TextBox6.Text = BAL.objBal.ReturnTag(sql, "DiscountValue");
            }
        }
    }
}