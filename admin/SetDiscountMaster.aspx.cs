using System;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_RulesForDiscount : Page
{
    string sql = "";
    BAL.RulesForDiscount objBal = new BAL.RulesForDiscount();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        if (!IsPostBack)
        {
            loadClass();
            loadCategory();
            loadGender();
            loadInstallment();
            drpDiscountHead.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
        }
    }

    private void loadCategory()
    {
        sql = "Select CasteName,CasteId from CasteMaster";
        BAL.objBal.FillDropDown_withValue(sql, DrpCategory, "CasteName", "CasteId");
        DrpCategory.Items.Insert(0, new ListItem("All", "A"));
    }

    private void loadClass()
    {
        sql = "Select Classname,Id from ClassMaster where SessionName='" + Session["SessionName"].ToString() + "' order by CIDOrder";
        BAL.objBal.FillDropDown_withValue(sql, DrpFromClass, "Classname", "Id");
        BAL.objBal.FillDropDown_withValue(sql, DrpToClass, "Classname", "Id");
        DrpFromClass.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
        DrpToClass.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
    }

    private void loadGender()
    {
        DrpGender.Items.Add(new ListItem("All", "A"));
        DrpGender.Items.Add(new ListItem("Male", "M"));
        DrpGender.Items.Add(new ListItem("Female", "F"));
        DrpGender.Items.Add(new ListItem("Transgender", "TG"));
    }

    protected void drpDiscountHead_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void loadInstallment()
    {
        sql = "Select MonthName as Installment,MonthId as Id from MonthMaster where SessionName='" + Session["SessionName"].ToString() + "'";
        Repeater1.DataSource = BAL.objBal.GridFill(sql);
        Repeater1.DataBind();
    }
    protected void RadioButtonList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < RadioButtonList1.Items.Count; i++)
        {
            RadioButtonList1.Items[0].Selected = false;
        }
        if (RadioButtonList2.SelectedIndex == 0)
        {
            column1.Style.Add("display", "none");
            classes.Style.Add("display", "block");
            gender.Style.Add("display", "block");
        }
        else
        {
            column1.Style.Add("display", "block");
            classes.Style.Add("display", "none");
            gender.Style.Add("display", "none");
        }
        loadDiscountHead();
    }

    private void loadDiscountHead()
    {
        sql = "Select HeadName,id from DiscountHead where ParentHeadvalue='"+RadioButtonList2.SelectedValue.ToString()+"' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        BAL.objBal.FillDropDown_withValue(sql, drpDiscountHead, "HeadName", "id");
        drpDiscountHead.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
        {
            return;
        }
        else
        {
            Insert("I", LinkButton1);
        }
    }

    private void SetValue(string DiscountHeadId, string classid, string GenderValue,string CategoryValue,string remark,string installment,string amount,string Queryfor)
    {
        objBal.id = 0;
        objBal.Queryfor = Queryfor;
        objBal.DiscountHeadId = Convert.ToInt16(DiscountHeadId);
        objBal.ClassId = Convert.ToInt16(classid);
        objBal.GenderValue = GenderValue;
        objBal.CategoryValue = CategoryValue;
        objBal.Remark = remark;
        objBal.Installment = installment;
        objBal.Amount = Convert.ToDouble(amount);
        objBal.SessionName = Session["SessionName"].ToString();
        objBal.LoginName = Session["LoginName"].ToString();
        objBal.BranchCode = Convert.ToInt16(Session["BranchCode"].ToString());
    }

    private void Insert(string Queryfor,Control ctrl)
    {
        string msg = "";
        int fromclass = DrpFromClass.SelectedIndex;
        int toclass = DrpToClass.SelectedIndex != 0 ? DrpToClass.SelectedIndex : DrpFromClass.SelectedIndex;
        if (fromclass <= toclass)
        {
            for (int k = fromclass; k <= toclass; k++)
            {
                for (int i = 1; i < DrpGender.Items.Count; i++)
                {
                    for (int j = 1; j < DrpCategory.Items.Count; j++)
                    {
                        for (int l = 0; l < Repeater1.Items.Count; l++)
                        {
                            Label lblInstallment = (Label)Repeater1.Items[l].FindControl("Label1");
                            TextBox txtAmount = (TextBox)Repeater1.Items[l].FindControl("TextBox3");
                            
                            try
                            {
                                SetValue(drpDiscountHead.SelectedValue.ToString(), DrpFromClass.Items[k].Value.ToString(), DrpGender.Items[i].Value.ToString(), DrpCategory.Items[j].Value.ToString(), txtRemark.Text.Trim(),lblInstallment.Text.Trim(),txtAmount.Text.Trim(), Queryfor);
                                msg = DAL.objDal.RulesForDiscount(objBal);
                            }
                            catch (Exception ex)
                            {
                                msg = ex.Message;
                            }
                        }
                    }
                }
            }
        }
        else
        {
            msg = "Sorry, From class always < To class";
        }
        mess(msg, ctrl);
    }

    
    private void mess(string msg, Control ctrl)
    {
        BAL.objBal.MessageBoxforUpdatePanel(objBal.MessageType(msg, new Control(), objBal), ctrl);
    }
    
    private void Update()
    {
    }
    private void Select()
    {
    }
    private void Delete()
    {
    }
}