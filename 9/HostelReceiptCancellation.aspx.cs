using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Net.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;
using c4SmsNew;


public partial class HostelReceiptCancellation : Page
{
    private SqlConnection _con;
    readonly Campus _oo;
    private string _sql = "";
    private string _sql1 = "";
    string sessionname = "";
    public HostelReceiptCancellation()
    {
        _con = new SqlConnection();
        _oo = new Campus();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((string)Session["LoginName"] == "" || (string)Session["BranchCode"] == "" || (string)Session["SessionName"] == "" || Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        _con = _oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file

        if (!IsPostBack)
        {
            try
            {
            }
            catch
            {
            }
        }
    }
    protected void txtStudentEnter_TextChanged(object sender, EventArgs e)
    {
        loadStudentGrid();
        loadfee();
    }
    protected void lnkShow_Click(object sender, EventArgs e)
    {
        loadStudentGrid();
        loadfee();
    }
    public void loadStudentGrid()
    {
        grdshow.Visible = true;
        string studentId = Request.Form[hfStudentId.UniqueID];
        if (studentId == string.Empty)
        {
            studentId = txtStudentEnter.Text.Trim();
        }
        sessionname = Session["SessionName"].ToString();
        _sql = "select count(*) count from AllStudentRecord_UDF('" + sessionname + "', " + Session["BranchCode"].ToString() + ") where  Srno='" + studentId + "'";
        if (_oo.ReturnTag(_sql, "count") == "0")
        {
            _sql = "select top(1) SessionName from StudentOfficialDetails where Srno='" + studentId + "' and BranchCode=" + Session["BranchCode"].ToString() + " order by id desc  ";
            sessionname = _oo.ReturnTag(_sql, "SessionName").ToString();
        }
        _sql = "select id, FamilyContactNo,  stenrcode, StlocalAddress,  SectionName, Card, Medium as Medium, ClassName, combineClassName, ";
        _sql = _sql + "    convert(nvarchar, DateOfAdmiission,106) as DateOfAdmiission , SectionId, FatherName, MotherName, FirstName,";
        _sql = _sql + "   MiddleName,LastName, StEnRCode as StEnRCode, srno  as srno,case  when  TransportRequired='Yes' then 'Yes' else 'No' end as TransportRequired,";
        _sql = _sql + "   BranchName, FamilyContactNo,PhotoPath from AllStudentRecord_UDF('" + sessionname + "', " + Session["BranchCode"].ToString() + ")";
        _sql = _sql + "    where Srno=" + "'" + studentId + "'";
        Grd.DataSource = _oo.GridFill(_sql);
        Grd.DataBind();
        DataSet ds;
        ds = _oo.GridFill(_sql);
        // ReSharper disable once UseNullPropagation
        if (ds != null && Grd.Rows.Count > 0)
        {
            grdshow.Visible = true;
            if (ds.Tables[0].Rows.Count > 0)
            {
                img.ImageUrl = ds.Tables[0].Rows[0]["PhotoPath"].ToString() != string.Empty ? ds.Tables[0].Rows[0]["PhotoPath"] + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture) : "../img/user-pic/user-pic.jpg" + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture);
                studentImg.NavigateUrl = ds.Tables[0].Rows[0]["PhotoPath"].ToString() != string.Empty ? ds.Tables[0].Rows[0]["PhotoPath"] + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture) : "../img/user-pic/user-pic.jpg" + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture);
                hylinkmoredetails.NavigateUrl = "../11/StudentRegView.aspx?print=1&id=" + ds.Tables[0].Rows[0]["stenrcode"];
            }
        }
        else
        {
            grdshow.Visible = false;
        }
    }
    public void loadfee()
    {
        string studentId = Request.Form[hfStudentId.UniqueID];
        if (studentId == string.Empty)
        {
            studentId = txtStudentEnter.Text.Trim();
        }
        int i = 0; double sum = 0;
        double discount = 0;
        double con = 0;
        GridView1.Visible = true;

        _sql = "select id, DepositDate, ReceiptNo, Status, Paid, NextDue from HostelFeeDeposit where SrNoOrEmpid=" + "'" + studentId + "'";
        _sql = _sql + " and status='Paid' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";


        var ds = _oo.GridFill(_sql);
        if (ds == null || ds.Tables[0].Rows.Count == 0)
        {
            lblHeading1.Visible = false;
            GridView1.DataSource = null;
            GridView1.DataBind();
        }
        else
        {
            lblHeading1.Visible = true;
            GridView1.DataSource = _oo.GridFill(_sql);
            GridView1.DataBind();
        }

        if (GridView1.Rows.Count == 0)
        {
            if (sessionname == Session["SessionName"].ToString())
            {
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, No Record(s) found!", "A");
            }

        }

        else
        {


            for (i = 0; i <= GridView1.Rows.Count - 1; i++)
            {
                Label lblSta = (Label)GridView1.Rows[i].FindControl("Label29");
                Label lblpaidAmt = (Label)GridView1.Rows[i].FindControl("Label21");
                Label lbldiscount = (Label)GridView1.Rows[i].FindControl("Label10");
                Label lblCon = (Label)GridView1.Rows[i].FindControl("Label14");

                if (lblSta.Text.Trim() == "Paid")
                {
                    try
                    {
                        sum = sum + Convert.ToDouble(lblpaidAmt.Text);
                        discount = discount + Convert.ToDouble(lbldiscount.Text);
                        con = con + Convert.ToDouble(lblCon.Text);
                    }
                    catch (Exception) { }
                }
                LinkButton LinkButton3 = (LinkButton)GridView1.Rows[i].FindControl("LinkButton3");
                if ((GridView1.Rows.Count - 1!=i))
                {
                    LinkButton3.Visible = false;
                }


            }

            Label FooterPaidAmt = (Label)GridView1.FooterRow.FindControl("Label39");
            Label FooterDiscuntAmt = (Label)GridView1.FooterRow.FindControl("Label9");
            Label FooterConAmt = (Label)GridView1.FooterRow.FindControl("Label13");

            FooterPaidAmt.Text = sum.ToString(CultureInfo.InvariantCulture);

            
        }

    }

    public void LinkButton3_Click(object sender, EventArgs e)
    {
        Button8.Focus();
        var chk = (LinkButton)sender;
        var Label13 = (Label)chk.NamingContainer.FindControl("Label13");
        lblvalue.Text = Label13.Text;
        Panel2_ModalPopupExtender.Show();

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        _sql = "update HostelFeeDeposit set status='Canceled' where ReceiptNo='" + lblvalue.Text + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        _oo.ProcedureDatabase(_sql);
        Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Cancelled successfully.", "S");
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        LinkButton link = (LinkButton)sender;
        LinkButton HostelReceiptNo = (LinkButton)link.NamingContainer.FindControl("btnView");
        Session["HostelReceiptNo"] = HostelReceiptNo.Text;
        Response.Redirect("HostelFeeReciept_duplicate.aspx?print=1");
    }
    public override void Dispose()
    {
        _con.Dispose();
        _oo.Dispose();
    }
}