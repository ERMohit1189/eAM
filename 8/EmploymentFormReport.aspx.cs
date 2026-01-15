using System;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_EmploymentFormReport : Page
{
    string sql = string.Empty;
    BAL.Employmentform objBal = new BAL.Employmentform();
    protected void Page_Load(object sender, EventArgs e)
    {
        Campus camp = new Campus(); camp.LoadLoader(loader); 

        if ( Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        if (!IsPostBack)
        {
            BAL.objBal.AddDateMonthYearDropDown(drpFromyear, drpFrommonth, drpFromdate);
            BAL.objBal.AddDateMonthYearDropDown(drpToyear, drpTomonth, drpTodate);
            BAL.objBal.FindCurrentDateandSetinDropDown(drpFromyear, drpFrommonth, drpFromdate);
            BAL.objBal.FindCurrentDateandSetinDropDown(drpToyear, drpTomonth, drpTodate);
           
            BLL obj = new BLL();
            obj.LoadHeader("Report", header1);
            loadDate();
            Select();
        }
    }

    private void loadDate()
    {
        string fromDate = drpFromyear.SelectedItem.ToString() + "/" + drpFrommonth.SelectedItem.ToString() + "/" + drpFromdate.SelectedItem.ToString();
        string toDate = drpToyear.SelectedItem.ToString() + "/" + drpTomonth.SelectedItem.ToString() + "/" + drpTodate.SelectedItem.ToString();
        lblTitle.Text = "( From Date :";
        lblDate.Text = fromDate;
        lblTitle1.Text = "To Date :";
        lblDate1.Text = toDate;              
    }

    private void Select()
    {
        try
        {
#pragma warning disable 219
            string msg = "";
#pragma warning restore 219
            Tuple<string, DataTable> tuple;
            objBal.Queryfor = "S";
            objBal.SessionName = Session["SessionName"].ToString();
            objBal.BranchCode = Convert.ToInt16(Session["BranchCode"].ToString());
            tuple = DAL.objDal.Employmentform(objBal);
            DataView dv = new DataView(tuple.Item2);
            string fromDate = drpFromyear.SelectedItem.ToString() + "/" + drpFrommonth.SelectedItem.ToString() + "/" + drpFromdate.SelectedItem.ToString();
            string toDate = drpToyear.SelectedItem.ToString() + "/" + drpTomonth.SelectedItem.ToString() + "/" + drpTodate.SelectedItem.ToString();
            dv.RowFilter = "(EFdate>=#" + fromDate + "# and EFdate<=#" + toDate + "#)";
            Repeater1.DataSource = dv;
            Repeater1.DataBind();
            double totalamount=0; double totalpaid = 0;
            for (int i = 0; i < Repeater1.Items.Count; i++)
            {
                Label lblAmount = (Label)Repeater1.Items[i].FindControl("lblAmount");
                totalamount = totalamount + double.Parse(lblAmount.Text==""?"0": lblAmount.Text);
                Label lblPaid = (Label)Repeater1.Items[i].FindControl("lblPaid");
                totalpaid = totalpaid + double.Parse(lblPaid.Text==""?"0": lblPaid.Text);
            }
            if (Repeater1.Items.Count > 0)
            {
                divPrintBtns.Visible = true;
                gdv.Visible = true;
                Control FooterTemplate = Repeater1.Controls[Repeater1.Controls.Count - 1].Controls[0];
                Label lblTotalAmount = FooterTemplate.FindControl("lblTotalAmount") as Label;
                Label lblTotalPaid = FooterTemplate.FindControl("lblTotalPaid") as Label;
                lblTotalAmount.Text = totalamount.ToString("0.00");
                lblTotalPaid.Text = totalpaid.ToString("0.00");
            }
            else
            {
                divPrintBtns.Visible = false;
                gdv.Visible = false;
            }
        }
        catch (Exception ex)
        {
            BAL.objBal.MessageBoxforUpdatePanel(objBal.MessageType(ex.Message, new Control(), new BAL.textBoxList()), this.Page);
        }
    }
    protected void drpToyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        BAL.objBal.YearDropDown(drpFromyear, drpFrommonth, drpFromdate);
    }
    protected void drpToyear_SelectedIndexChanged1(object sender, EventArgs e)
    {
        BAL.objBal.MonthDropDown(drpFromyear, drpFrommonth, drpFromdate);
    }
    protected void drpFrommonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        BAL.objBal.YearDropDown(drpToyear, drpTomonth, drpTodate);
    }
    protected void drpFromyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        BAL.objBal.MonthDropDown(drpToyear, drpTomonth, drpTodate);
    }
    protected void lnkView_Click(object sender, EventArgs e)
    {
        Select();
        loadDate();
    }
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        BLL obj = new BLL();
        obj.LoadHeader("Receipt", header1);
        BAL.objBal.ExportToWord(Response, "EmploymentFormCollection", gdv);
    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        BLL obj = new BLL();
        obj.LoadHeader("Receipt", header1);
        PrintHelper_New.ctrl = abc;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        BLL obj = new BLL();
        obj.LoadHeader("Receipt", header1);
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=EmploymentFormCollection.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        gdv.RenderControl(hw);
        Response.Output.Write(sw.ToString());
        Response.Flush();
        Response.End();

    }
}