using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_SubjectForEmploymentForm : Page
{
    string sql = "";
    BAL.SubjectForEmploymentForm objBal = new BAL.SubjectForEmploymentForm();
    Campus oo = new Campus();
    protected void Page_Load(object sender, EventArgs e)
    {
        Campus camp = new Campus(); camp.LoadLoader(loader); 

        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        if (!IsPostBack)
        {
            loadEducationType();
            Select();
        }
    }

    private void loadEducationType()
    {
        sql = "Select EducationType,id from tbl_EducationType where BranchCode=" + Session["BranchCode"].ToString() + "";
        BAL.objBal.FillDropDown_withValue(sql, drpEducationType, "EducationType", "id");
        drpEducationType.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
        BAL.objBal.FillDropDown_withValue(sql, drpEducationTypeUpdate, "EducationType", "id");
        drpEducationTypeUpdate.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
    }

    private void Select()
    {
        Repeater1.DataSource = null;
        Repeater1.DataBind();
        string msg = "";
        try
        {
            Tuple<string, DataTable> tuple;
            objBal.Queryfor = "S";
            objBal.SessionName = Session["SessionName"].ToString().Trim();
            objBal.BranchCode = Convert.ToInt16(Session["BranchCode"].ToString().Trim());
            tuple = DAL.objDal.SubjectForEmploymentForm(objBal);
            Repeater1.DataSource = tuple.Item2;
            Repeater1.DataBind();
            if (Repeater1.Items.Count > 0)
            {
                Repeater1.Visible = true;
                for (int i = 0; i < Repeater1.Items.Count; i++)
                {
                    Label lblEducationType = (Label)Repeater1.Items[i].FindControl("lblId");
                    LinkButton lnkEdit = (LinkButton)Repeater1.Items[i].FindControl("lnkEdit");
                    LinkButton lnkDelete = (LinkButton)Repeater1.Items[i].FindControl("lnkDelete");
                    sql = "select SubjectId from Employmentform where SubjectId=" + lblEducationType.Text + " and BranchCode=" + Session["BranchCode"] + "";
                    if (oo.Duplicate(sql))
                    {
                        lnkEdit.Text = "<i class='fa fa-lock'></i>";
                        lnkDelete.Text = "<i class='fa fa-lock'></i>";
                        lnkEdit.Enabled = false;
                        lnkDelete.Enabled = false;
                    }
                }
            }
            else
            {
                Repeater1.Visible = false;
                Repeater1.DataSource = null;
                Repeater1.DataBind();
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message;
            //lblMess.Text = msg;
        }


    }
    protected void lnkSubmit_Click(object sender, EventArgs e)
    {
        Insert(lnkSubmit);
        
    }

    private void Insert(Control ctrl)
    {
        sql = "select Subject from tbl_SubjectForEmploymentForm where EducationType="+ drpEducationType.SelectedValue + " and Subject='"+ txtSubject.Text.Trim() + "' and BranchCode="+Session["BranchCode"] +"";
        if (oo.Duplicate(sql))
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Duplicate Subject!", "A");
            return;
        }
        string msg = "";
        try
        {
            Tuple<string, DataTable> tuple;
            objBal.Queryfor = "I";
            objBal.EducationTypes = Convert.ToInt16(drpEducationType.SelectedValue.ToString());
            objBal.Subject = txtSubject.Text.Trim();
            objBal.SessionName = Session["SessionName"].ToString().Trim();
            objBal.BranchCode = Convert.ToInt16(Session["BranchCode"].ToString().Trim());
            objBal.LoginName = Session["LoginName"].ToString().Trim();
            tuple = DAL.objDal.SubjectForEmploymentForm(objBal);
            msg = tuple.Item1;
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");
            txtSubject.Text = "";
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        //lblMess.Text = objBal.SimpleMessageType(msg, div1);
        SelecUsingEducationType();
    }



    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = (LinkButton)sender;
            RepeaterItem currentItemRow = (RepeaterItem)lnk.NamingContainer;
            Label lblId = (Label)currentItemRow.FindControl("lblId");
            Session["EditId"] = lblId.Text.Trim();
            Tuple<string, DataTable> tuple;
            objBal.Queryfor = "S";
            objBal.SessionName = Session["SessionName"].ToString().Trim();
            objBal.BranchCode = Convert.ToInt16(Session["BranchCode"].ToString().Trim());
            tuple = DAL.objDal.SubjectForEmploymentForm(objBal);
            DataView dv = new DataView(tuple.Item2);
            dv.RowFilter = " Id='" + lblId.Text.Trim() + "'";
            drpEducationTypeUpdate.SelectedValue = drpEducationTypeUpdate.Items.FindByText(dv[0][1].ToString().Trim()).Value;
            txtSubjectUpdate.Text = dv[0][2].ToString().Trim();
            Panel1_ModalPopupExtender   .Show();
        }
        catch (Exception ex)
        {
            //lblMess.Text = ex.Message;
        }
    }
    protected void lnkUpdate_Click(object sender, EventArgs e)
    {
        Update(lnkUpdate);
    }
    private void Update(Control ctrl)
    {
        sql = "select Subject from tbl_SubjectForEmploymentForm where EducationType=" + drpEducationTypeUpdate.SelectedValue + " and Subject='" + txtSubjectUpdate.Text.Trim() + "' and BranchCode=" + Session["BranchCode"] + " and id<>"+ Session["EditId"].ToString() + "";
        if (oo.Duplicate(sql))
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Duplicate Subject!", "A");
            return;
        }
        string msg = "";
        try
        {
            Tuple<string, DataTable> tuple;
            objBal.Queryfor = "U";
            objBal.id = Convert.ToInt16(Session["EditId"].ToString());
            objBal.EducationTypes = Convert.ToInt16(drpEducationTypeUpdate.SelectedValue.ToString());
            objBal.Subject = txtSubjectUpdate.Text.Trim();
            objBal.SessionName = Session["SessionName"].ToString().Trim();
            objBal.BranchCode = Convert.ToInt16(Session["BranchCode"].ToString().Trim());
            objBal.LoginName = Session["LoginName"].ToString().Trim();
            tuple = DAL.objDal.SubjectForEmploymentForm(objBal);
            msg = tuple.Item1;
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully.", "S");
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        Select();
    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        RepeaterItem currentItemRow = (RepeaterItem)lnk.NamingContainer;
        Label lblId = (Label)currentItemRow.FindControl("lblId");
        Session["DeleteId"] = lblId.Text.Trim();
        Panel2_ModalPopupExtender.Show();
    }

    private void Delete(Control ctrl)
    {
        string msg = "";
        try
        {
            Tuple<string, DataTable> tuple;
            objBal.Queryfor = "D";
            objBal.id = Convert.ToInt16(Session["DeleteId"].ToString());
            tuple = DAL.objDal.SubjectForEmploymentForm(objBal);
            msg = tuple.Item1;
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        Select();
    }

    public void SelecUsingEducationType()
    {
        Repeater1.DataSource = null;
        Repeater1.DataBind();
        try
        {
            Tuple<string, DataTable> tuple;
            objBal.Queryfor = "S";
            objBal.SessionName = Session["SessionName"].ToString().Trim();
            objBal.BranchCode = Convert.ToInt16(Session["BranchCode"].ToString().Trim());
            tuple = DAL.objDal.SubjectForEmploymentForm(objBal);
            DataView dv = new DataView(tuple.Item2);
            if (drpEducationType.SelectedIndex != 0)
            {
                dv.RowFilter = " EducationType='" + drpEducationType.SelectedItem.ToString() + "'";
            }

            Repeater1.DataSource = dv;
            Repeater1.DataBind();
            if (Repeater1.Items.Count > 0)
            {
                Repeater1.Visible = true;
            }
            else
            {
                Repeater1.Visible = false;
                Repeater1.DataSource = null;
                Repeater1.DataBind();
            }
        }
        catch (Exception ex)
        {
            //lblMess.Text = ex.Message;
        }
    }
    protected void drpEducationType_SelectedIndexChanged(object sender, EventArgs e)
    {
        SelecUsingEducationType();
    }

    protected void Button4_Click(object sender, EventArgs e)
    {

    }

    protected void Button8_Click(object sender, EventArgs e)
    {

    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        Delete(btnDelete);
    }
}