using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.HtmlControls;

public partial class admin_HeaderTemplate : Page
{
    DataSet ds = new DataSet();
    DataTable dt = new DataTable();
    SqlConnection con = new SqlConnection();

    private SqlConnection _con;
    private readonly Campus _oo;
    private string _sql, _sql1 = String.Empty;


    public admin_HeaderTemplate()
    {
        _con = new SqlConnection();
        _oo = new Campus();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        Campus camp = new Campus(); camp.LoadLoader(loader);
        if (!IsPostBack)
        {
            // RadioButtonList2.SelectedIndex =1;
            GetHeaderDetail();
            List<SqlParameter> param = new List<SqlParameter>();
            //param.Add(new SqlParameter("@Id", 1));
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"]));
            ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("InstituteMasterProc", param);
            if (ds == null)
            {
                return;
            }
            else
            {
                if (ds.Tables.Count > 0)
                {
                    dt = new DataTable();
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        lblInstitute.Text = dt.Rows[0]["collegename"].ToString();
                        lblAddress.Text = dt.Rows[0]["address"].ToString();
                        lblBranchandCity.Text = dt.Rows[0]["branchandcityname"].ToString();
                        lblCity.Text = dt.Rows[0]["cityname"].ToString();
                        lblContactnoandemail.Text = dt.Rows[0]["phonenoandemil"].ToString();
                        lblPhoneNo.Text = dt.Rows[0]["phone"].ToString();
                        lblEmail.Text = dt.Rows[0]["email"].ToString();
                        lblWebsite.Text = dt.Rows[0]["website"].ToString();
                        lblWebsiteandEmail.Text = dt.Rows[0]["emailandwebsite"].ToString();
                        lblAffilation.Text = dt.Rows[0]["affiliatedto"].ToString();
                        lblSlogan.Text = dt.Rows[0]["slogan"].ToString();

                        if (dt.Rows[0]["boarduniversitylogo"].ToString() == "")
                        {
                            Image2.ImageUrl = "~/img/cbse-logo.png";
                        }
                        else
                        {
                            Image2.ImageUrl = BAL.objBal.GetImageUrl(ResolveClientUrl(dt.Rows[0]["boarduniversitylogo"].ToString()), Request.Url.AbsoluteUri.Split('/'));
                        }

                        if (dt.Rows[0]["collegeLogo"].ToString() == "")
                        {
                            Image1.ImageUrl = "~/uploads/CollegeLogo/DefaultCollegeLogo.png";
                        }
                        else
                        {
                            Image1.ImageUrl = BAL.objBal.GetImageUrl(ResolveClientUrl(dt.Rows[0]["collegeLogo"].ToString()), Request.Url.AbsoluteUri.Split('/'));
                        }
                        lblAffilationNo.Text = dt.Rows[0]["AffiliationNo"].ToString();
                        lblSchoolNo.Text = dt.Rows[0]["SchoolNo"].ToString();
                        gettemplatedata();
                        loadUC();
                        getDocsetting();
                    }
                }
            }
        }
    }

    private void loadUC()
    {
        string path = "";
        path = "~/admin/usercontrol/" + RadioButtonList1.SelectedValue.ToString().Trim() + ".ascx";

        Control UC = LoadControl(path);

        string parentid = "";

        if (rblogo.SelectedIndex == 0)
        {
            UC = hideControl(UC, parentid + "ctrl1", true);
        }
        else
        {
            UC = hideControl(UC, parentid + "ctrl1", false);
        }

        if (rbIns.SelectedIndex == 0)
        {
            UC = hideControl(UC, parentid + "ctrl2", true);
        }
        else
        {
            UC = hideControl(UC, parentid + "ctrl2", false);
        }

        if (rbAdd.SelectedIndex == 0)
        {
            UC = hideControl(UC, parentid + "ctrl3", true);
        }
        else
        {
            UC = hideControl(UC, parentid + "ctrl3", false);
        }

        if (rbBranchandCity.SelectedIndex == 0)
        {
            UC = hideControl(UC, parentid + "ctrl4", true);
        }
        else
        {
            UC = hideControl(UC, parentid + "ctrl4", false);
        }

        if (rbCity.SelectedIndex == 0)
        {
            UC = hideControl(UC, parentid + "ctrl5", true);
        }
        else
        {
            UC = hideControl(UC, parentid + "ctrl5", false);
        }

        if (rbContactnoandemail.SelectedIndex == 0)
        {
            UC = hideControl(UC, parentid + "ctrl6", true);
        }
        else
        {
            UC = hideControl(UC, parentid + "ctrl6", false);
        }

        if (rbPhoneno.SelectedIndex == 0)
        {
            UC = hideControl(UC, parentid + "ctrl7", true);
        }
        else
        {
            UC = hideControl(UC, parentid + "ctrl7", false);
        }

        if (rbEmail.SelectedIndex == 0)
        {
            UC = hideControl(UC, parentid + "ctrl8", true);
        }
        else
        {
            UC = hideControl(UC, parentid + "ctrl8", false);
        }

        if (rbWebandemail.SelectedIndex == 0)
        {
            UC = hideControl(UC, parentid + "ctrl9", true);
        }
        else
        {
            UC = hideControl(UC, parentid + "ctrl9", false);
        }

        if (rbWeb.SelectedIndex == 0)
        {
            UC = hideControl(UC, parentid + "ctrl10", true);
        }
        else
        {
            UC = hideControl(UC, parentid + "ctrl10", false);
        }

        if (rbAffila.SelectedIndex == 0)
        {
            UC = hideControl(UC, parentid + "ctrl11", true);
        }
        else
        {
            UC = hideControl(UC, parentid + "ctrl11", false);
        }

        if (rbSlo.SelectedIndex == 0)
        {
            UC = hideControl(UC, parentid + "ctrl12", true);
        }
        else
        {
            UC = hideControl(UC, parentid + "ctrl12", false);
        }

        if (rbUniversityLogo.SelectedIndex == 0)
        {
            UC = hideControl(UC, parentid + "ctrl13", true);
            UC = hideControl(UC, parentid + "ctrl1", true);
            UC = hideControl(UC, parentid + "ctrl14", false);

        }
        else
        {
            UC = hideControl(UC, parentid + "ctrl13", false);

            hideControl(UC, parentid + "ctrlmain");
            if (rblogo.SelectedIndex == 1)
            {
                UC = hideControl(UC, parentid + "ctrl1", false);
                UC = hideControl(UC, parentid + "ctrl14", false);
            }
            else
            {

                UC = hideControl(UC, parentid + "ctrl14", true);
                UC = hideControl(UC, parentid + "ctrl1", false);
            }
        }

        if (rbAffilationNo.SelectedIndex == 0)
        {
            UC = hideControl(UC, parentid + "ctrl15", true);
        }
        else
        {
            UC = hideControl(UC, parentid + "ctrl15", false);
        }

        if (rbSchoolNo.SelectedIndex == 0)
        {
            UC = hideControl(UC, parentid + "ctrl16", true);
        }
        else
        {
            UC = hideControl(UC, parentid + "ctrl16", false);
        }

        if (rbSchoolNo.SelectedIndex != 0 || rbAffilationNo.SelectedIndex != 0)
        {
            UC = hideControl(UC, parentid + "ctrl17", false);
        }
        else
        {
            UC = hideControl(UC, parentid + "ctrl17", true);
        }

        div1.Controls.Add(UC);





    }
    private void GetHeaderDetail()
    {
        using (SqlConnection con = _oo.dbGet_connection())
        {
            using (SqlCommand cmd = new SqlCommand("HeadMastersp", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Headtype", ""); // For 'Get', you can pass empty or null
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString()); // Replace with actual session value
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());          // Replace with actual branch code
                cmd.Parameters.AddWithValue("@CreatedBy", Session["LoginName"].ToString());       // Replace with actual user
                cmd.Parameters.AddWithValue("@CategoryType", RadioButtonList1.SelectedItem.Value);       // Replace with actual user
                cmd.Parameters.AddWithValue("@ActionType", "Get");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    string val = dt.Rows[0]["Val"].ToString();
                    if (val == "Get")
                    {
                        string headType = dt.Rows[0]["Headtype"].ToString();

                        if (headType == "With Header")
                        {
                            RadioButtonList2.SelectedValue = "With Header";
                        }
                        else if (headType == "Without Header")
                        {
                            RadioButtonList2.SelectedValue = "Without Header";
                        }
                    }
                    else if (val == "Not Exists")
                    {
                        RadioButtonList2.ClearSelection();
                    }
                }
            }
        }
    }
    private void SaveHeaderDetail()
    {
        using (SqlConnection con = _oo.dbGet_connection())
        {
            using (SqlCommand cmd = new SqlCommand("HeadMastersp", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Headtype", RadioButtonList2.SelectedItem.Text); // For 'Get', you can pass empty or null
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString()); // Replace with actual session value
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());          // Replace with actual branch code
                cmd.Parameters.AddWithValue("@CreatedBy", Session["LoginName"].ToString());
                cmd.Parameters.AddWithValue("@CategoryType", RadioButtonList1.SelectedItem.Value); // Replace with actual user
                cmd.Parameters.AddWithValue("@ActionType", "Insert");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {

                }
            }
        }
    }

    public bool DivContainsImage(Control parent)
    {
        foreach (Control ctrl in parent.Controls)
        {
            // Check if it's HtmlImage (server side <img runat="server">)
            if (ctrl is HtmlImage)
            {
                return true;
            }

            // Check if it's asp:Image control
            if (ctrl is Image)
            {
                return true;
            }

            // Check if control is a LiteralControl containing <img> tag as text
            LiteralControl literal = ctrl as LiteralControl;
            if (literal != null && literal.Text.ToLower().Contains("<img"))
            {
                return true;
            }

            // Recursively check child controls
            if (ctrl.HasControls() && DivContainsImage(ctrl))
            {
                return true;
            }
        }
        return false;
    }
    public Control hideControl(Control parent, string id, bool isdisplay)
    {
        foreach (Control _ChildControl in parent.Controls)
        {
            if (_ChildControl.ID == id)
            {
                _ChildControl.Visible = isdisplay;
            }
            if (_ChildControl.ID == "ctrlmain")
            {
                hideControl(_ChildControl, id, isdisplay);
            }
        }

        return parent;
    }

    public void hideControl(Control parent, string id)
    {
        foreach (Control _ChildControl in parent.Controls)
        {
            if (_ChildControl.ID == "ctrlmain")
            {
                ((HtmlGenericControl)_ChildControl).Attributes.Add("class", "text-center col-lg-8 col-md-8 col-xs-8 col-sm-8 no-padding ");
            }
        }
    }
    protected void rblogo_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadUC();
    }
    protected void rbIns_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadUC();
    }
    protected void rbAdd_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbAdd.SelectedIndex == 0)
        {
            rbBranchandCity.SelectedIndex = 1;
            rbCity.SelectedIndex = 1;
        }
        loadUC();
    }
    protected void rbContactnoandemail_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbContactnoandemail.SelectedIndex == 0)
        {
            rbPhoneno.SelectedIndex = 1;
            rbEmail.SelectedIndex = 1;
        }
        loadUC();
    }
    protected void rbWeb_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbWeb.SelectedIndex == 0)
        {
            rbWebandemail.SelectedIndex = 1;
        }
        loadUC();
    }
    protected void rbAffila_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadUC();
    }
    protected void rbSlo_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadUC();
    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList2.SelectedIndex == 0)
        {
            rbUniversityLogo.SelectedIndex = 1;
            rblogo.SelectedIndex = 0;
            rbIns.SelectedIndex = 0;
            rbAdd.SelectedIndex = 0;
            rbBranchandCity.SelectedIndex = 1;
            rbCity.SelectedIndex = 1;
            rbContactnoandemail.SelectedIndex = 0;
            rbPhoneno.SelectedIndex = 1;
            rbEmail.SelectedIndex = 1;
            rbWebandemail.SelectedIndex = 1;
            rbWeb.SelectedIndex = 0;
            rbAffila.SelectedIndex = 1;
            rbSlo.SelectedIndex = 1;
            rbAffilationNo.SelectedIndex = 1;
            rbSchoolNo.SelectedIndex = 1;
        }
        else
        {
            rbUniversityLogo.SelectedIndex = 1;
            rblogo.SelectedIndex = 1;
            rbIns.SelectedIndex = 1;
            rbAdd.SelectedIndex = 1;
            rbBranchandCity.SelectedIndex = 1;
            rbCity.SelectedIndex = 1;
            rbContactnoandemail.SelectedIndex = 1;
            rbPhoneno.SelectedIndex = 1;
            rbEmail.SelectedIndex = 1;
            rbWebandemail.SelectedIndex = 1;
            rbWeb.SelectedIndex = 1;
            rbAffila.SelectedIndex = 1;
            rbSlo.SelectedIndex = 1;
            rbAffilationNo.SelectedIndex = 1;
            rbSchoolNo.SelectedIndex = 1;
        }
        gettemplatedata();
        loadUC();


        getDocsetting();
        GetHeaderDetail();
    }
    protected void rbBranchandCity_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbBranchandCity.SelectedIndex == 0)
        {
            rbAdd.SelectedIndex = 1;
            rbCity.SelectedIndex = 1;
        }
        loadUC();
    }
    protected void rbCity_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbCity.SelectedIndex == 0)
        {
            rbAdd.SelectedIndex = 1;
            rbBranchandCity.SelectedIndex = 1;
        }
        loadUC();
    }
    protected void rbWebandemail_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbWebandemail.SelectedIndex == 0)
        {
            rbWeb.SelectedIndex = 1;
        }
        loadUC();
    }
    protected void rbPhoneno_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbPhoneno.SelectedIndex == 0)
        {
            rbContactnoandemail.SelectedIndex = 1;
            rbEmail.SelectedIndex = 1;
        }
        loadUC();
    }
    protected void rbEmail_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbEmail.SelectedIndex == 0)
        {
            rbContactnoandemail.SelectedIndex = 1;
            rbPhoneno.SelectedIndex = 1;
        }
        loadUC();
    }
    protected void lnkSubmit_Click(object sender, EventArgs e)
    {
        string count = "0";

        List<SqlParameter> param = new List<SqlParameter>();

        param.Add(new SqlParameter("@TemplateFor", RadioButtonList1.SelectedValue.ToString().Trim()));
        param.Add(new SqlParameter("@line1", rblogo.SelectedValue));
        param.Add(new SqlParameter("@line2", rbIns.SelectedValue));
        param.Add(new SqlParameter("@line3", rbAdd.SelectedValue));
        param.Add(new SqlParameter("@line4", rbBranchandCity.SelectedValue));
        param.Add(new SqlParameter("@line5", rbCity.SelectedValue));
        param.Add(new SqlParameter("@line6", rbContactnoandemail.SelectedValue));
        param.Add(new SqlParameter("@line7", rbPhoneno.SelectedValue));
        param.Add(new SqlParameter("@line8", rbEmail.SelectedValue));
        param.Add(new SqlParameter("@line9", rbWebandemail.SelectedValue));
        param.Add(new SqlParameter("@line10", rbWeb.SelectedValue));
        param.Add(new SqlParameter("@line11", rbAffila.SelectedValue));
        param.Add(new SqlParameter("@line12", rbSlo.SelectedValue));
        param.Add(new SqlParameter("@line13", rbUniversityLogo.SelectedValue));
        param.Add(new SqlParameter("@line14", rbAffilationNo.SelectedValue));
        param.Add(new SqlParameter("@line15", rbSchoolNo.SelectedValue));
        param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
        param.Add(new SqlParameter("@LoginName", Session["LoginName"].ToString()));
        var para = new SqlParameter("@Msg", "")
        {
            Direction = ParameterDirection.Output,
            Size = 0x100
        };
        param.Add(para);
        count = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("SetHeaderTemplateProc", param);
        SaveHeaderDetail();
        string docsettingmsg = "";
        docsettingmsg = setDocsetting();
        if (count == "1" && docsettingmsg == "S")
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Header and Doc settings updated successfully.", "S");
        }
        else if (count != "1" && docsettingmsg == "S")
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Doc settings updated successfully but not header!", "A");
        }
        else if (count == "1" && docsettingmsg != "S")
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Header settings updated successfully but not doc!", "A");
        }
        else
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, template not updated!", "A");
        }
        gettemplatedata();
        loadUC();
    }

    private void gettemplatedata()
    {
        List<SqlParameter> param = new List<SqlParameter>();
        param.Add(new SqlParameter("@TemplateFor", RadioButtonList1.SelectedValue.ToString().Trim()));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"]));
        ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("GetHeaderTemplateProc", param);
        if (ds.Tables.Count > 0)
        {
            dt = new DataTable();
            dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0][0].ToString() == "False")
                {
                    rblogo.SelectedIndex = 1;
                }
                else
                {
                    rblogo.SelectedIndex = 0;
                }

                if (dt.Rows[0][1].ToString() == "False")
                {
                    rbIns.SelectedIndex = 1;
                }
                else
                {
                    rbIns.SelectedIndex = 0;
                }

                if (dt.Rows[0][2].ToString() == "False")
                {
                    rbAdd.SelectedIndex = 1;
                }
                else
                {
                    rbAdd.SelectedIndex = 0;
                }


                if (dt.Rows[0][3].ToString() == "False")
                {
                    rbBranchandCity.SelectedIndex = 1;
                }
                else
                {
                    rbBranchandCity.SelectedIndex = 0;
                }


                if (dt.Rows[0][4].ToString() == "False")
                {
                    rbCity.SelectedIndex = 1;
                }
                else
                {
                    rbCity.SelectedIndex = 0;
                }

                if (dt.Rows[0][5].ToString() == "False")
                {
                    rbContactnoandemail.SelectedIndex = 1;
                }
                else
                {
                    rbContactnoandemail.SelectedIndex = 0;
                }


                if (dt.Rows[0][6].ToString() == "False")
                {
                    rbPhoneno.SelectedIndex = 1;
                }
                else
                {
                    rbPhoneno.SelectedIndex = 0;
                }


                if (dt.Rows[0][7].ToString() == "False")
                {
                    rbEmail.SelectedIndex = 1;
                }
                else
                {
                    rbEmail.SelectedIndex = 0;
                }


                if (dt.Rows[0][8].ToString() == "False")
                {
                    rbWebandemail.SelectedIndex = 1;
                }
                else
                {
                    rbWebandemail.SelectedIndex = 0;
                }


                if (dt.Rows[0][9].ToString() == "False")
                {
                    rbWeb.SelectedIndex = 1;
                }
                else
                {
                    rbWeb.SelectedIndex = 0;
                }


                if (dt.Rows[0][10].ToString() == "False")
                {
                    rbAffila.SelectedIndex = 1;
                }
                else
                {
                    rbAffila.SelectedIndex = 0;
                }

                if (dt.Rows[0][11].ToString() == "False")
                {
                    rbSlo.SelectedIndex = 1;
                }
                else
                {
                    rbSlo.SelectedIndex = 0;
                }

                if (dt.Rows[0][12].ToString() == "False")
                {
                    rbUniversityLogo.SelectedIndex = 1;
                }
                else
                {
                    rbUniversityLogo.SelectedIndex = 0;
                }

                if (dt.Rows[0][13].ToString() == "False")
                {
                    rbAffilationNo.SelectedIndex = 1;
                }
                else
                {
                    rbAffilationNo.SelectedIndex = 0;
                }

                if (dt.Rows[0][14].ToString() == "False")
                {
                    rbSchoolNo.SelectedIndex = 1;
                }
                else
                {
                    rbSchoolNo.SelectedIndex = 0;
                }
            }
        }
    }
    protected void RadioButtonList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        gettemplatedata();
        if (RadioButtonList2.SelectedIndex == 0)
        // if (RadioButtonList2.SelectedValue == "With Header")
        {
            rbUniversityLogo.SelectedIndex = 1;
            rblogo.SelectedIndex = 0;
            rbIns.SelectedIndex = 0;
            rbAdd.SelectedIndex = 0;
            rbBranchandCity.SelectedIndex = 1;
            rbCity.SelectedIndex = 1;
            rbContactnoandemail.SelectedIndex = 0;
            rbPhoneno.SelectedIndex = 1;
            rbEmail.SelectedIndex = 1;
            rbWebandemail.SelectedIndex = 1;
            rbWeb.SelectedIndex = 0;
            rbAffila.SelectedIndex = 1;
            rbSlo.SelectedIndex = 1;
            rbAffilationNo.SelectedIndex = 1;
            rbSchoolNo.SelectedIndex = 1;
        }
        else
        {
            rbUniversityLogo.SelectedIndex = 1;
            rblogo.SelectedIndex = 1;
            rbIns.SelectedIndex = 1;
            rbAdd.SelectedIndex = 1;
            rbBranchandCity.SelectedIndex = 1;
            rbCity.SelectedIndex = 1;
            rbContactnoandemail.SelectedIndex = 1;
            rbPhoneno.SelectedIndex = 1;
            rbEmail.SelectedIndex = 1;
            rbWebandemail.SelectedIndex = 1;
            rbWeb.SelectedIndex = 1;
            rbAffila.SelectedIndex = 1;
            rbSlo.SelectedIndex = 1;
            rbAffilationNo.SelectedIndex = 1;
            rbSchoolNo.SelectedIndex = 1;
        }
        loadUC();

    }
    protected void drpFontsize_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblEx.Style.Add("font-size", drpFontsize.SelectedValue + "px");
        loadUC();
    }
    public string setDocsetting()
    {
        string msg;

        List<SqlParameter> param = new List<SqlParameter>();

        param.Add(new SqlParameter("@QueryFor", "I"));
        param.Add(new SqlParameter("@DocCategory", RadioButtonList1.SelectedValue.ToString().Trim()));
        param.Add(new SqlParameter("@FontName", "Arial"));
        param.Add(new SqlParameter("@FontSize", drpFontsize.SelectedValue.ToString()));
        param.Add(new SqlParameter("@FontColor", "#333"));
        param.Add(new SqlParameter("@FontBold", false));
        double margTop = 0.50, margbott = 0.50, margLeft = 0.50, margRight = 0.50;
        //bool flag = false;
        double.TryParse(txtTop.Text.Trim(), out margTop); double.TryParse(txtBottom.Text.Trim(), out margbott);
        double.TryParse(txtLeft.Text.Trim(), out margLeft); double.TryParse(txtRight.Text.Trim(), out margRight);
        param.Add(new SqlParameter("@MarginTop", margTop));
        param.Add(new SqlParameter("@MarginBottom", margbott));
        param.Add(new SqlParameter("@MarginLeft", margLeft));
        param.Add(new SqlParameter("@MarginRight", margRight));
        param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
        param.Add(new SqlParameter("@LoginName", Session["LoginName"].ToString()));

        SqlParameter para = new SqlParameter("@Msg", "");
        para.Direction = ParameterDirection.Output;
        para.Size = 0x100;
        param.Add(para);
        msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("DocSetting_Proc", param);

        if (msg == "S")
        {
            getDocsetting();
        }

        return msg;
    }

    public void getDocsetting()
    {
        try
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@QueryFor", "S"));
            param.Add(new SqlParameter("@DocCategory", RadioButtonList1.SelectedValue.ToString().Trim()));
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
            ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("DocSetting_Proc", param);
            if (ds != null)
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    drpFontsize.SelectedValue = dt.Rows[0]["FontSize"].ToString();
                    lblEx.Style.Add("font-size", drpFontsize.SelectedValue + "px");
                    txtTop.Text = dt.Rows[0]["MarginTop"].ToString();
                    txtBottom.Text = dt.Rows[0]["MarginBottom"].ToString();
                    txtLeft.Text = dt.Rows[0]["MarginLeft"].ToString();
                    txtRight.Text = dt.Rows[0]["MarginRight"].ToString();
                }
            }
            else
            {
                drpFontsize.SelectedValue = "12";
                lblEx.Style.Add("font-size", drpFontsize.SelectedValue + "px");
                txtTop.Text = "0.50";
                txtBottom.Text = "0.50";
                txtLeft.Text = "0.50";
                txtRight.Text = "0.50";
            }
        }
        catch
        {
        }
    }

    protected void rbUniversityLogo_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadUC();
    }

    protected void rbAffilationNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadUC();
    }

    protected void rbSchoolNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadUC();
    }
}