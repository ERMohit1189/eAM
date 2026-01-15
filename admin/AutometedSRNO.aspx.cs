using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;
using System.IO;

public partial class AutometedSRNO : Page
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
        Campus camp = new Campus(); camp.LoadLoader(loader);
        if (!IsPostBack)
        {
            sql = "select * from StudentOfficialDetails where BranchCode=" + Session["BranchCode"] + "";
            if (oo.Duplicate(sql))
            {
                lnkSubmit.Visible = false;
            }
            sql = "select * from tblAutometedSRNO where SrNoType='Automatic' and AutomaticSrNoType='Numeric' and BranchCode<>" + Session["BranchCode"] + "";
            if (oo.Duplicate(sql))
            {
                rdoAutomaticSrNoType.Items.Clear();
                rdoAutomaticSrNoType.Items.Insert(0, new ListItem("AlfaNumeric", "AlfaNumeric"));
                rdoAutomaticSrNoType.Items.Insert(0, new ListItem("Customized", "Customized"));
                divNumeric.Visible = false;
                rdoAutomaticSrNoType.SelectedValue = "Customized";
                rdoAutomaticSrNoType.Visible = false;
            }
            sql = "select top(1) ShortCode from BranchTab where BranchId=" + Session["BranchCode"] + "";
            txtPrestringManual.Text = oo.ReturnTag(sql, "ShortCode").ToString().Trim();
            txtPrestringAlfa.Text = oo.ReturnTag(sql, "ShortCode").ToString().Trim();
            txtPrestringCustomized.Text = oo.ReturnTag(sql, "ShortCode").ToString().Trim();
            if (rdoSrNoType.SelectedValue == "Manual")
            {
                divManual.Visible = true;
                divAutomatic.Visible = false;
            }
            else
            {
                divManual.Visible = false;
                divAutomatic.Visible = true;
            }


            DisplayInformation();
        }
    }
    protected void rdoSrNoType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdoSrNoType.SelectedValue == "Manual")
        {
            divManual.Visible = true;
            divAutomatic.Visible = false;
        }
        else
        {
            divManual.Visible = false;
            divAutomatic.Visible = true;
            if (rdoAutomaticSrNoType.SelectedValue == "Numeric")
            {
                divNumeric.Visible = true;
                divAlfaNumeric.Visible = false;
                divCustomized.Visible = false;
            }
            else if (rdoAutomaticSrNoType.SelectedValue == "AlfaNumeric")
            {
                divNumeric.Visible = false;
                divAlfaNumeric.Visible = true;
                divCustomized.Visible = false;
            }
            else
            {
                divNumeric.Visible = false;
                divAlfaNumeric.Visible = false;
                divCustomized.Visible = true;
            }
        }

    }
    protected void rdoAutomaticSrNoType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdoAutomaticSrNoType.SelectedValue == "Numeric")
        {
            divNumeric.Visible = true;
            divAlfaNumeric.Visible = false;
            divCustomized.Visible = false;
        }
        else if (rdoAutomaticSrNoType.SelectedValue == "AlfaNumeric")
        {
            divNumeric.Visible = false;
            divAlfaNumeric.Visible = true;
            divCustomized.Visible = false;
        }
        else
        {
            divNumeric.Visible = false;
            divAlfaNumeric.Visible = false;
            divCustomized.Visible = true;
        }
    }
    public void DisplayInformation()
    {
        bool status = true;
        string sqls = "select BranchCode cnt from tblAutometedSRNO";
        if (oo.Duplicate(sqls))
        {
            status = false;
            ddlSeparaterManual.Enabled = false;
            txtPrestringManual.Enabled = false;
            txtPrestringAlfa.Enabled = false;
            txtPrestringCustomized.Enabled = false;
        }
        else
        {
            status = true;
            txtPrestringManual.Enabled = true;
            txtPrestringAlfa.Enabled = true;
            txtPrestringCustomized.Enabled = true;
        }
        
        string SrNoType = "", PreString = "", AutomaticSrNoType = "", Separater = "", InitialNoNumeric = "", RegisterNo = "", NoofPages = "", Separater2="";
        string sql1 = "select * from tblAutometedSRNO where BranchCode=" + Session["BranchCode"] + "";
        if (oo.Duplicate(sql1))
        {
            rdoSrNoType.Enabled = false;
            SrNoType = oo.ReturnTag(sql1, "SrNoType");
            PreString = oo.ReturnTag(sql1, "PreString");
            AutomaticSrNoType = oo.ReturnTag(sql1, "AutomaticSrNoType");
            Separater = oo.ReturnTag(sql1, "Separater");
            InitialNoNumeric = oo.ReturnTag(sql1, "InitialNoNumeric");
            Separater2 = oo.ReturnTag(sql1, "Separater2");
            RegisterNo = oo.ReturnTag(sql1, "RegisterNo");
            NoofPages = oo.ReturnTag(sql1, "NoofPages");
            if (SrNoType == "Manual")
            {
                rdoSrNoType.SelectedIndex = 0;
                divManual.Visible = true;
                divAutomatic.Visible = false;
                txtPrestringManual.Text = PreString;
                ddlSeparaterManual.SelectedValue = Separater;
                lblManualSRFormate.Text = PreString + Separater;

                txtPrestringManual.Enabled = status;
                ddlSeparaterManual.Enabled = status;
            }
            else
            {
                rdoSrNoType.SelectedIndex = 1;
                divManual.Visible = false;
                divAutomatic.Visible = true;
                if (AutomaticSrNoType == "Numeric")
                {
                    divNumeric.Visible = true;
                    divAlfaNumeric.Visible = false;
                    divCustomized.Visible = false;
                    txtInitialNoNumeric.Text = InitialNoNumeric;
                    txtInitialNoNumeric.Enabled = status;
                }
                else if (AutomaticSrNoType == "AlfaNumeric")
                {
                    divNumeric.Visible = false;
                    divAlfaNumeric.Visible = true;
                    divCustomized.Visible = false;
                    txtPrestringAlfa.Text = PreString;
                    ddlSeparaterAlfa.SelectedValue = Separater;
                    txtInitialAlfa.Text = InitialNoNumeric;
                    lblAlfaSRFormate.Text = PreString + Separater+ InitialNoNumeric;
                    txtPrestringAlfa.Enabled = status;
                    ddlSeparaterAlfa.Enabled = status;
                    txtInitialAlfa.Enabled = status;
                }
                else if (AutomaticSrNoType == "Customized")
                {
                    divNumeric.Visible = false;
                    divAlfaNumeric.Visible = false;
                    divCustomized.Visible = true;
                    txtPrestringCustomized.Text = PreString;
                    ddlSeparaterCustomized.SelectedValue = Separater;
                    txtSerialCustomized.Text = InitialNoNumeric;
                    txtRegisterNo.Text = RegisterNo;
                    ddlSeparaterCustomized2.SelectedValue = Separater2;
                    txtPages.Text = NoofPages;
                    lblAutomaticCustomizedSRFormate.Text = PreString + Separater+ InitialNoNumeric+ Separater2+ RegisterNo;

                    txtPrestringCustomized.Enabled = status;
                    ddlSeparaterCustomized.Enabled = status;
                    txtSerialCustomized.Enabled = status;
                    ddlSeparaterCustomized2.Enabled = status;
                    txtRegisterNo.Enabled = status;
                    txtPages.Enabled = status;

                }
            }
        }

    }

    protected void lnkSubmit_Click(object sender, EventArgs e)
    {
        string qry = string.Empty;
        
        string  sql1 = "select * from tblAutometedSRNO where SrNoType='Automatic' and AutomaticSrNoType='AlfaNumeric' and isnull(PreString, '')='" + txtPrestringAlfa.Text.Trim() + "' and BranchCode<>" + Session["BranchCode"] + "";
        string  sql2 = "select * from tblAutometedSRNO where SrNoType='Automatic' and AutomaticSrNoType='Customized' and isnull(PreString, '')='" + txtPrestringCustomized.Text.Trim() + "' and BranchCode<>" + Session["BranchCode"] + "";
        string  sql3 = "select * from tblAutometedSRNO where SrNoType='Manual' and isnull(PreString, '')='"+ txtPrestringManual.Text.Trim() + "' and BranchCode<>" + Session["BranchCode"] + "";
        
        if (rdoSrNoType.SelectedValue == "Manual" && oo.Duplicate(sql3))
        {
            oo.msgbox(this.Page, msgbox, "Please enter diffrent Pre-String, this Pre-String already exists in other branch!!", "A");
        }
        else if (rdoSrNoType.SelectedValue== "Automatic" && rdoAutomaticSrNoType.SelectedValue== "Numeric" && txtInitialNoNumeric.Text.Trim()=="")
        {
            oo.msgbox(this.Page, msgbox, "Please enter Initial No.!", "A");
        }
        else if (rdoSrNoType.SelectedValue == "Automatic" && rdoAutomaticSrNoType.SelectedValue == "AlfaNumeric" && txtInitialAlfa.Text.Trim() == "")
        {
            oo.msgbox(this.Page, msgbox, "Please enter Initial No.!", "A");
        }
        else if (rdoSrNoType.SelectedValue == "Automatic" && rdoAutomaticSrNoType.SelectedValue == "AlfaNumeric" && txtPrestringAlfa.Text.Trim()=="")
        {
            oo.msgbox(this.Page, msgbox, "Please enter Pre-String!", "A");
        }
        else if (rdoSrNoType.SelectedValue == "Automatic" && rdoAutomaticSrNoType.SelectedValue == "AlfaNumeric" && oo.Duplicate(sql1))
        {
            oo.msgbox(this.Page, msgbox, "Please enter diffrent Pre-String, this Pre-String already exists in other branch!!", "A");
        }
        else if (rdoSrNoType.SelectedValue == "Automatic" && rdoAutomaticSrNoType.SelectedValue == "Customized" && txtSerialCustomized.Text.Trim() == "")
        {
            oo.msgbox(this.Page, msgbox, "Please enter Initial No.!", "A");
        }
        else if (rdoSrNoType.SelectedValue == "Automatic" && rdoAutomaticSrNoType.SelectedValue == "Customized" && oo.Duplicate(sql2))
        {
            oo.msgbox(this.Page, msgbox, "Please enter diffrent Pre-String, this Pre-String already exists in other branch!!", "A");
        }
        else
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "spAutometedSRNO";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@SrNoType", rdoSrNoType.SelectedValue);
            if (rdoSrNoType.SelectedValue == "Manual")
            {
                cmd.Parameters.AddWithValue("@PreString", txtPrestringManual.Text.Trim());
                cmd.Parameters.AddWithValue("@Separater", ddlSeparaterManual.SelectedValue);
            }
            else
            {
                cmd.Parameters.AddWithValue("@AutomaticSrNoType", rdoAutomaticSrNoType.SelectedValue);
                if (rdoAutomaticSrNoType.SelectedValue == "Numeric")
                {
                    cmd.Parameters.AddWithValue("@InitialNoNumeric", txtInitialNoNumeric.Text.Trim());
                }
                else if (rdoAutomaticSrNoType.SelectedValue == "AlfaNumeric")
                {
                    cmd.Parameters.AddWithValue("@Separater", ddlSeparaterAlfa.SelectedValue);
                    cmd.Parameters.AddWithValue("@PreString", txtPrestringAlfa.Text.Trim());
                    cmd.Parameters.AddWithValue("@InitialNoNumeric", txtInitialAlfa.Text.Trim());
                }
                else if (rdoAutomaticSrNoType.SelectedValue == "Customized")
                {
                    if (txtPrestringCustomized.Text.Trim()!="")
                    {
                        cmd.Parameters.AddWithValue("@Separater", ddlSeparaterCustomized.SelectedValue);
                    }
                    cmd.Parameters.AddWithValue("@PreString", txtPrestringCustomized.Text.Trim());
                    cmd.Parameters.AddWithValue("@InitialNoNumeric", txtSerialCustomized.Text.Trim());
                    cmd.Parameters.AddWithValue("@Separater2", ddlSeparaterCustomized2.SelectedValue);
                    cmd.Parameters.AddWithValue("@RegisterNo", txtRegisterNo.Text.Trim());
                    cmd.Parameters.AddWithValue("@NoofPages", txtPages.Text.Trim());
                }
            }
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"]);
            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"]);
            cmd.Parameters.AddWithValue("@Action", "insert");
            con.Open();
            cmd.ExecuteNonQuery();
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");
            con.Close();
            DisplayInformation();
        }
    }
   
}
