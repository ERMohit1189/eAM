using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_Employmentform : System.Web.UI.Page
{
    string sql = "";
    BAL.Employmentform objBal = new BAL.Employmentform();
    Campus oo = new Campus();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file
        if (!IsPostBack)
        {
            txtEditdate.Text= DateTime.Now.ToString("dd-MMM-yyyy");
            txtDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            txtFrom.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            txtTo.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            loadEducationType();
            loadDesi();
            Select();
        }
    }

    private void loadDesi()
    {
        sql = "Select DesName,DesId from DesMaster where BranchCode=" + Session["BranchCode"].ToString() + "";
        BAL.objBal.FillDropDown_withValue(sql, drpdes, "DesName", "DesId");
        BAL.objBal.FillDropDown_withValue(sql, drpdesPanel, "DesName", "DesId");
        drpdes.Items.Insert(0, new ListItem("<--Select-->", "<--Selec-->"));
        drpdesPanel.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
    }

    private void loadEducationType()
    {
        sql = "Select EducationType,id from tbl_EducationType where BranchCode=" + Session["BranchCode"].ToString() + "";
        BAL.objBal.FillDropDown_withValue(sql, drpEducationType, "EducationType", "id");
        drpEducationType.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));

        BAL.objBal.FillDropDown_withValue(sql, drpEducationTypePanel, "EducationType", "id");
        drpEducationTypePanel.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));

    }


    private void loadSubject(CheckBoxList chklist, DropDownList drpEducationType)
    {
        if (drpEducationType.SelectedValue != "0")
        {
            sql = "Select Subject,sfef.id from tbl_SubjectForEmploymentForm sfef inner join tbl_EducationType et on et.Id=sfef.EducationType and et.BranchCode=sfef.BranchCode where sfef.EducationType='" + drpEducationType.SelectedValue.ToString() + "' and et.BranchCode=" + Session["BranchCode"].ToString() + " and sfef.BranchCode=" + Session["BranchCode"].ToString() + "";
            chklist.DataSource = BAL.objBal.GridFill(sql);
            chklist.DataTextField = "Subject";
            chklist.DataValueField = "Id";
            chklist.DataBind();
        }
    }

    protected void lnkSubmit_Click(object sender, EventArgs e)
    {
        if (Checkvalidation(txtApplicantName, txtFatherName, drpSex, txtContactNo, drpdes, txtAmount)==true)
        {
            Insert(lnkSubmit);
        }
    }

    public string IDGeneration(string FixedString, string x)
    {
        string xx = "";
        if (x.Length == 1)
        {
            xx = FixedString + "000000" + x;

        }
        else if (x.Length == 2)
        {
            xx = FixedString + "00000" + x;
        }
        else if (x.Length == 3)
        {
            xx = FixedString + "0000" + x;
        }
        else if (x.Length == 4)
        {
            xx = FixedString + "000" + x;
        }
        else if (x.Length == 5)
        {
            xx = FixedString + "00" + x;
        }
        else if (x.Length == 6)
        {
            xx = FixedString + "0" + x;
        }
        else
        {
            xx = FixedString + x;
        }
        return "EF/" + Session["SessionName"].ToString() + "/" + xx;
    }

    private void Insert(Control ctrl)
    {
        if (chkSubject.Items.Count==0)
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Subjects not created!", "A");
            return;
        }
        int STS = 0;
        for (int i = 0; i < chkSubject.Items.Count; i++)
        {
            if (chkSubject.Items[i].Selected)
            {
                STS = STS + 1;
            }
        }
        if (STS==0)
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please select at least one Subject!", "A");
            return;
        }
        int co;
        string xx = "";

        sql = "select max(EmployeeFormId) as id from Employmentform where BranchCode=" + Session["BranchCode"].ToString() + "";
        if(BAL.objBal.ReturnTag(sql, "id")==string.Empty)
        {
            co = 0;
        }
        else
        {
            co = Convert.ToInt32(BAL.objBal.ReturnTag(sql, "id"));
        }
        
        co = co + 1;
        xx = IDGeneration("", co.ToString());
        Session["RecieptNo"] = xx;
        string msg = "";
        Tuple<string, DataTable> tuple;
        var subjects="";
        foreach (ListItem li in chkSubject.Items)
        {
            if (li.Selected)
            {
                if(subjects=="")
                    subjects = li.Value;
                else
                    subjects += ',' + li.Value;               
            }
        }

        objBal.id = 0;
        objBal.Queryfor = "I";
        objBal.RecieptNo = xx;
        objBal.EmpName = txtApplicantName.Text.Trim();
        objBal.EmpFather = txtFatherName.Text.Trim();
        objBal.EmpGender = drpSex.SelectedItem.Text.Trim();
        objBal.EmpContactNo = txtContactNo.Text.Trim();
        objBal.EmpDesignation = Convert.ToInt16(drpdes.SelectedValue.ToString());
        objBal.EmpAmount = Convert.ToInt32(txtAmount.Text.Trim());
        objBal.EFDate = txtDate.Text.Trim();

        objBal.EmployeeFormId = co;
        if (drpSex.SelectedIndex == 2)
        {
            objBal.HuabandName = txtHusbandName.Text;
        }
        objBal.EmploymenttypeId = Convert.ToInt16(drpEducationType.SelectedValue.ToString());
        objBal.SubjectIds = subjects;
        objBal.Email = txtEmail.Text.Trim();
        objBal.SessionName = Session["SessionName"].ToString();
        objBal.LoginName = Session["LoginName"].ToString();
        objBal.BranchCode = Convert.ToInt16(Session["BranchCode"].ToString());
        tuple = DAL.objDal.Employmentform(objBal);
        msg = tuple.Item1;

        if (msg == "S")
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");
            Response.Redirect("employment-form.aspx?print=1");
        }
        else
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please select subject!", "A");
        }

        //Select();
        //BAL.objBal.ClearControls(maindiv);
        //txtAmount.Text = "200";
        //Response.Write("<script>");
        //Response.Write("var winpop=window.open('employment-form.aspx?print=1','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}");
        //Response.Write("</script>");

    }


    protected void lnkView_Click(object sender, EventArgs e)
    {
        Select();
    }
    private void Select()
    {
        Repeater1.DataSource = null;
        Repeater1.DataBind();
        try
        {
            sql = " SELECT DISTINCT EmployeeFormId AS Id, RecieptNo, FORMAT(EFDate, 'dd MMM yyyy') AS EFDate, EmpName, EmpFather, EducationType, ";
            sql = sql+ " EmpGender, EmpContactNo, EmpDesignation AS Empdesid, edm.DesName EmpDesName, EmpAmount, HuabandName, EmploymenttypeId, ";
            sql = sql + " Email, IsCancel FROM Employmentform ef INNER JOIN DesMaster edm ON edm.DesId = EmpDesignation and edm.BranchCode=ef.BranchCode ";
            sql = sql + " INNER JOIN tbl_EducationType etp ON etp.id = EmploymenttypeId and etp.BranchCode=ef.BranchCode ";
            sql = sql + " WHERE ef.BranchCode = "+Session["BranchCode"] +" and convert(date, EFDate) between '"+ txtFrom.Text+"' and '"+ txtTo.Text + "' ";
            if (ddlStatus.SelectedIndex!=0)
            {
                if (ddlStatus.SelectedIndex==1)
                {
                    sql = sql + " AND IsCancel = 0";
                }
                else
                {
                    sql = sql + " AND IsCancel = 1";
                }
            }
            sql = sql + " ORDER BY ef.EmployeeFormId DESC";
            var dt = oo.Fetchdata(sql);
            string msg = "";
            
            Repeater1.DataSource = dt;
            Repeater1.DataBind();
            if (Repeater1.Items.Count > 0)
            {
                trGooter.Visible = true;
                double totalAmt = 0; double totalPaid = 0;
                for (int i = 0; i < Repeater1.Items.Count; i++)
                {
                    Label lblAmount = (Label)Repeater1.Items[i].FindControl("lblAmount");
                    Label lblPaid = (Label)Repeater1.Items[i].FindControl("lblPaid");
                    Label EFDate = (Label)Repeater1.Items[i].FindControl("EFDate");
                    Label IsCancel = (Label)Repeater1.Items[i].FindControl("IsCancel");
                    LinkButton lnkEdit = (LinkButton)Repeater1.Items[i].FindControl("lnkEdit");
                    LinkButton lnkCancel = (LinkButton)Repeater1.Items[i].FindControl("lnkCancel");
                    LinkButton lnkPrintEF = (LinkButton)Repeater1.Items[i].FindControl("lnkPrintEF");
                    string curdate = oo.ReturnTag("select getdate() curdate", "curdate");
                    if (DateTime.Parse(EFDate.Text.Trim()).ToString("dd-MMM-yyyy") != DateTime.Parse(curdate).ToString("dd-MMM-yyyy"))
                    {
                        lnkEdit.Text = "<i class='fa fa-lock'></i>";
                        lnkCancel.Text = "<i class='fa fa-lock'></i>";

                        lnkEdit.Enabled = false;
                        lnkCancel.Enabled = false;

                        if (IsCancel.Text == "True")
                        {
                            lnkPrintEF.Text = "<i class='fa fa-lock'></i>";
                            lnkPrintEF.Enabled = false;
                        }
                    }
                    if (IsCancel.Text == "True")
                    {
                        lnkEdit.Text = "<i class='fa fa-lock'></i>";
                        lnkCancel.Text = "<i class='fa fa-lock'></i>";
                        lnkPrintEF.Text = "<i class='fa fa-lock'></i>";
                        lnkEdit.Enabled = false;
                        lnkCancel.Enabled = false;
                        lnkPrintEF.Enabled = false;
                    }
                    
                    totalAmt = totalAmt + double.Parse(lblAmount.Text==""?"0": lblAmount.Text);
                    totalPaid = totalPaid + double.Parse(lblPaid.Text==""?"0": lblPaid.Text);
                }
                lblTotalAmount.Text = totalAmt.ToString("0.00");
                lblTotalPaid.Text = totalPaid.ToString("0.00");
            }
            else
            {
                trGooter.Visible = false;
                Repeater1.DataSource = null;
                Repeater1.DataBind();
            }
        }
        catch (Exception ex)
        {
            BAL.objBal.MessageBoxforUpdatePanel(objBal.MessageType(ex.Message,new Control(), new BAL.textBoxList()), this.Page);
        }
    }

    private void Update(Control ctrl)
    {
        string msg = "";
        //myModal.Style.Add("display", "none");

        sql = "Select Top 1 RecieptNo,EFDate from Employmentform where EmployeeFormId='" + Session["Editid"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        string RecieptNo = BAL.objBal.ReturnTag(sql, "RecieptNo");
        string EFDate = BAL.objBal.ReturnTag(sql, "EFDate");
        bool flag = false;
        foreach (ListItem li in chkSubjectPanel.Items)
        {
            if (li.Selected)
            {
                flag = true;
                break;
            }
        }
        var subjects = "";
        Tuple<string, DataTable> tuple;
        if (flag == true)
        {
            sql = "Delete from Employmentform where EmployeeFormId='" + Session["Editid"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            BAL.objBal.ProcedureDatabase(sql);
            foreach (ListItem li in chkSubjectPanel.Items)
            {
                if (li.Selected)
                {
                    if (subjects == "")
                        subjects = li.Value;
                    else
                        subjects += ',' + li.Value;
                }
            }
        }

        objBal.id = 0;
        objBal.Queryfor = "I";
        objBal.RecieptNo = RecieptNo;
        objBal.EmpName = txtApplicantNamePanel.Text.Trim();
        objBal.EmpFather = txtFatherNamePanel.Text.Trim();
        objBal.EmpGender = drpSexPanel.SelectedItem.Text.Trim();
        objBal.EmpContactNo = txtContactNoPanel.Text.Trim();
        objBal.EmpDesignation = Convert.ToInt16(drpdesPanel.SelectedValue.ToString());
        objBal.EmpAmount = Convert.ToInt32(lblAmountPanel.Text.Trim());
        objBal.EmployeeFormId = Convert.ToInt16(Session["Editid"].ToString());
        objBal.EFDate = txtEditdate.Text.Trim();
        if (drpSexPanel.SelectedIndex == 2)
        {
            objBal.HuabandName = txtHusbandNamePanel.Text;
        }
        objBal.EmploymenttypeId = Convert.ToInt16(drpEducationTypePanel.SelectedValue.ToString());
        objBal.SubjectIds = subjects;
        objBal.Email = txtEmailPanel.Text.Trim();
        objBal.SessionName = Session["SessionName"].ToString();
        objBal.LoginName = Session["LoginName"].ToString();
        objBal.BranchCode = Convert.ToInt16(Session["BranchCode"].ToString());
        tuple = DAL.objDal.Employmentform(objBal);

        msg = tuple.Item1;

        if (msg.Trim() == "S")
        {
            Select();
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully.", "S");
        }
        else
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, Record(s) not Update!", "A");
        }
    }

    private bool Checkvalidation(TextBox txt1, TextBox txt2, DropDownList drp1, TextBox txt3, DropDownList drp2, TextBox txt4)
    {
        bool flag = true;
        if (txt1.Text.Trim() == string.Empty)
        {
            txt1.Style.Add("border", "1px solid red");
            flag = false;
        }
        else
        {
            txt1.Style.Add("border", "1px solid #CCC");
        }

        if (txt2.Text.Trim() == string.Empty)
        {
            txt2.Style.Add("border", "1px solid red");
            flag = false;
        }
        else
        {
            txt2.Style.Add("border", "1px solid #CCC");
        }

        if (drp1.SelectedIndex == 0)
        {
            drp1.Style.Add("border", "1px solid red");
            flag = false;
        }
        else
        {
            drp1.Style.Add("border", "1px solid #CCC");
        }

        if (txt3.Text.Trim() == string.Empty)
        {
            txt3.Style.Add("border", "1px solid red");
            flag = false;
        }
        else
        {
            txt3.Style.Add("border", "1px solid #CCC");
        }

        if (drp2.SelectedIndex == 0)
        {
            drp2.Style.Add("border", "1px solid red");
            flag = false;
        }
        else
        {
            drp2.Style.Add("border", "1px solid #CCC");
        }

        if (txt4.Text.Trim() == string.Empty)
        {
            txt4.Style.Add("border", "1px solid red");
            flag = false;
        }
        else
        {
            txt4.Style.Add("border", "1px solid #CCC");
        }
        return flag;
    }

    private bool Checkvalidation(TextBox txt1, TextBox txt2, DropDownList drp1, TextBox txt3, DropDownList drp2, Label txt4)
    {
        bool flag=true;
        if (txt1.Text.Trim() == string.Empty)
        {
            txt1.Style.Add("border", "1px solid red");
            flag = false;
        }
        else
        {
            txt1.Style.Add("border", "1px solid #CCC");
        }

        if (txt2.Text.Trim() == string.Empty)
        {
            txt2.Style.Add("border", "1px solid red");
            flag = false;
        }
        else
        {
            txt2.Style.Add("border", "1px solid #CCC");
        }

        if (drp1.SelectedIndex == 0)
        {
            drp1.Style.Add("border", "1px solid red");
            flag = false;
        }
        else
        {
            drp1.Style.Add("border", "1px solid #CCC");
        }

        if (txt3.Text.Trim() == string.Empty)
        {
            txt3.Style.Add("border", "1px solid red");
            flag = false;
        }
        else
        {
            txt3.Style.Add("border", "1px solid #CCC");
        }

        if (drp2.SelectedIndex == 0)
        {
            drp2.Style.Add("border", "1px solid red");
            flag = false;
        }
        else
        {
            drp2.Style.Add("border", "1px solid #CCC");
        }

        if (txt4.Text.Trim() == string.Empty)
        {
            txt4.Style.Add("border", "1px solid red");
            flag = false;
        }
        else
        {
            txt4.Style.Add("border", "1px solid #CCC");
        }
        return flag;
    }
    
    protected void lnkUpdate_Click(object sender, EventArgs e)
    {
        if (Checkvalidation(txtApplicantNamePanel, txtFatherNamePanel, drpSexPanel, txtContactNoPanel, drpdesPanel, lblAmountPanel) == true)
        {
            Update(lnkUpdate);
        }
    }
    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        RepeaterItem currentItem = (RepeaterItem)lnk.NamingContainer;
        Label lblId = (Label)currentItem.FindControl("Label1");
        Session["Editid"] = lblId.Text.Trim();
        Tuple<string, DataTable> tuple;
        objBal.Queryfor = "S";
        objBal.SessionName = Session["SessionName"].ToString();
        objBal.BranchCode = Convert.ToInt16(Session["BranchCode"].ToString());
        tuple = DAL.objDal.Employmentform(objBal);
        DataView dv = new DataView(tuple.Item2);
        dv.RowFilter = " Id='" + Session["Editid"].ToString() + "'";
        txtApplicantNamePanel.Text = dv[0][3].ToString();
        txtFatherNamePanel.Text = dv[0][4].ToString();
        drpSexPanel.SelectedValue = dv[0][5].ToString();
        txtContactNoPanel.Text = dv[0][6].ToString();
        drpdesPanel.SelectedValue = dv[0][7].ToString();
        lblAmountPanel.Text = dv[0][9].ToString();
        txtHusbandNamePanel.Text = dv[0][10].ToString();
        drpEducationTypePanel.SelectedValue = dv[0][11].ToString();
        txtEmailPanel.Text = dv[0][12].ToString();
        txtEditdate.Text= dv[0][2].ToString();
        loadSubject(chkSubjectPanel,drpEducationTypePanel);
        sql = "Select Subjectid from Employmentform where EmployeeFormId='"+lblId.Text.Trim()+ "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        DataTable dt = BAL.objBal.GridFill(sql).Tables[0];
        for (int i = 0; i < chkSubjectPanel.Items.Count; i++)
        {
            chkSubjectPanel.Items[i].Selected = false;            
        }
        for (int i = 0; i < chkSubjectPanel.Items.Count; i++)
        {
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                if (chkSubjectPanel.Items[i].Value == dt.Rows[j][0].ToString())
                {
                    chkSubjectPanel.Items[i].Selected = true;
                }
            }
        }
        Button2_ModalPopupExtender.Show();
    }
    protected void lnkPrintEF_Click(object sender, EventArgs e)
    {
        //myModal.Style.Add("display", "none");
        LinkButton lnk = (LinkButton)sender;
        RepeaterItem currentItem = (RepeaterItem)lnk.NamingContainer;
        Label LabelId = (Label)currentItem.FindControl("Label1");
        UpdatePanel UpdatePanel3 = (UpdatePanel)currentItem.FindControl("UpdatePanel3");
        sql = "Select RecieptNo from Employmentform where EmployeeFormId='" + LabelId.Text.Trim() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        Session["RecieptNo"] = BAL.objBal.ReturnTag(sql, "RecieptNo");
        if (ScriptManager.GetCurrent(Page).IsInAsyncPostBack)
        {
            ScriptManager.RegisterStartupScript(UpdatePanel3, typeof(string), "redirect", "var winpop=window.open('employment-form.aspx?print=1','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}", true);
        }
        //Response.Write("<script>");
        //Response.Write("");
        //Response.Write("</script>");
    }
    protected void lnkCancel_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        RepeaterItem currentItem = (RepeaterItem)lnk.NamingContainer;
        Label lblId = (Label)currentItem.FindControl("Label1");
        lblvalue.Text = lblId.Text;
        Panel2_ModalPopupExtender.Show();
    }
    protected void drpEducationType_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadSubject(chkSubject,drpEducationType);
    }
    protected void drpEducationTypePanel_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadSubject(chkSubjectPanel, drpEducationTypePanel);
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string msg = "";
        Tuple<string, DataTable> tuple;
        objBal.EmployeeFormId = Convert.ToInt16(lblvalue.Text.Trim());
        objBal.Queryfor = "C";
        objBal.BranchCode = Convert.ToInt16(Session["BranchCode"].ToString());
        objBal.LoginName = Session["LoginName"].ToString();
        tuple = DAL.objDal.Employmentform(objBal);
        msg = tuple.Item1;
        lblvalue.Text = "";
        if (msg.Trim() == "C")
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Cancelled successfully.", "S");
        }
        Select();
    }

    
}