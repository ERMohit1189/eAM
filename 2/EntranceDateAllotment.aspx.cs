using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using c4SmsNew;
public partial class EntranceDateAllotment : Page
{
    public SqlConnection _con;
    public Campus _oo = new Campus();
    string _sql = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        Campus camp = new Campus(); camp.LoadLoader(loader);
        _con = _oo.dbGet_connection();
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("~/default.aspx");
            
        }
        if (!IsPostBack)
        {
            ddlHH.Items.Insert(0, "HH");
            for (int i = 1; i <= 12; i++)
            {
                if (i < 10)
                {
                    ddlHH.Items.Insert(i, "0"+i.ToString());
                }
                else
                {
                    ddlHH.Items.Insert(i, i.ToString());
                }
            }
            ddlMM.Items.Insert(0, "MM");
            for (int j = 1; j <= 60; j++)
            {
                if (j < 10)
                {
                    ddlMM.Items.Insert(j, "0"+j.ToString());
                }
                else
                {
                    ddlMM.Items.Insert(j, j.ToString());
                }
            }
        }
    }
    public void Display()
    {
        _sql = "select Row_Number() over (order by Id Asc) as SNo ,id, Status, AdmissionType,ReceivedAmount,RecieptNo,convert(nvarchar,AdmissionFromDate,106) as AdmissionFromDate,isnull(StudentName,'') + ' ' + isnull(MiddleName,'') + ' ' + isnull(lastname,'') as StudentNames,FatherName,Class, (case when (ISNULL(Branch,'')='NA' or ISNULL(Branch,'Other')='NA'  or ISNULL(Branch,'Other')='N/A') then '' else '('+Branch+')' end) Branch,LoginName,BranchCode,convert(nvarchar,RecordDate,106) as RecordDate,Sex,FatherContactNo,Amount,status as Cancel, mop, Template, case when isnull(EntrenceDate, '')='' then '' else Format(EntrenceDate, 'dd-MMM-yyyy hh:mm tt') end EntrenceDates, isnull(EnteranceStatus, 'Pending')EnteranceStatus  from AdmissionFormCollection where BranchCode=" + Session["BranchCode"].ToString() + "";
        _sql = _sql + " and convert(date, AdmissionFromDate) between '" + txtFromDate.Text + "' and '" + txtToDate.Text + "' and status<>'Cancelled' and EnteranceStatus in ('Faild', 'Pending') and BranchCode=" + Session["BranchCode"] + " order by id Desc";
        Grd.DataSource = _oo.GridFill(_sql);
        Grd.DataBind();
    }

    protected void LinkView_Click(object sender, EventArgs e)
    {
        Display();
    }
    public void SendFeesSms(string fmobileNo, string recieptNo, string amount)
    {
        _sql = "Select HitValue from SMSActivateDeactivate where BranchCode=" + Session["BranchCode"] + "";
        if (_oo.ReturnTag(_sql, "HitValue") != "")
        {
            if (_oo.ReturnTag(_sql, "HitValue") == "true")
            {
                SMSAdapterNew sadpNew = new SMSAdapterNew();

                var mess = "INR " + amount + " received towards Admission Form. Receipt No. " + recieptNo + "";
                if (fmobileNo != "")
                {
                    _sql = "Select SmsSent From SmsEmailMaster where Id='11' ";
                    if (_oo.ReturnTag(_sql, "SmsSent").Trim() == "true")
                    {
                        sadpNew.Send(mess, fmobileNo, "");
                    }
                }
            }
        }
    }
    public void SendFeescancleSms(string fmobileNo, string recieptNo, string amount)
    {
        _sql = "Select HitValue from SMSActivateDeactivate where BranchCode=" + Session["BranchCode"] + "";
        if (_oo.ReturnTag(_sql, "HitValue") != "")
        {
            if (_oo.ReturnTag(_sql, "HitValue") == "true")
            {
                SMSAdapterNew sadpNew = new SMSAdapterNew();

                var mess = "Receipt No. " + recieptNo + " has been cancelled of Admission Form. Refunded Amount is INR " + amount + " ";

                if (fmobileNo != "")
                {
                    _sql = "Select SmsSent From SmsEmailMaster where Id='11' ";
                    if (_oo.ReturnTag(_sql, "SmsSent").Trim() == "true")
                    {
                        sadpNew.Send(mess, fmobileNo, "");
                    }
                }
            }
        }
    }
    public override void Dispose()
    {
        _con.Dispose();
        _oo.Dispose();
    }


    protected void LinkAllotDate_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label Label36 = (Label)chk.NamingContainer.FindControl("Label36");
        Label LabelEntrenceDate = (Label)chk.NamingContainer.FindControl("LabelEntrenceDate");
        Label LabelEnteranceStatus = (Label)chk.NamingContainer.FindControl("LabelEnteranceStatus");
        Label LabelRecieptNo = (Label)chk.NamingContainer.FindControl("LabelRecieptNo");
        lblIDs.Text = Label36.Text;
        if (LabelEntrenceDate.Text != "")
        {
            txtAllotDate.Text = DateTime.Parse(LabelEntrenceDate.Text.Trim()).ToString("dd-MMM-yyyy");
            ddlHH.SelectedValue = DateTime.Parse(LabelEntrenceDate.Text.Trim()).ToString("hh");
            ddlMM.SelectedValue = DateTime.Parse(LabelEntrenceDate.Text.Trim()).ToString("mm");
            ddlTT.SelectedValue = DateTime.Parse(LabelEntrenceDate.Text.Trim()).ToString("tt");
        }
        else
        {
            txtAllotDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            ddlHH.SelectedIndex = 0;
            ddlMM.SelectedIndex = 0;
            ddlTT.SelectedIndex = 0;
        }
        if (LabelEnteranceStatus.Text != "")
        {
            ddlEntranceStatus.SelectedValue = LabelEnteranceStatus.Text;
        }
        else
        {
            ddlEntranceStatus.SelectedIndex = 1;
        }
        lblRecieptNo.Text = LabelRecieptNo.Text;
        Panel1_ModalPopupExtender.Show();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        string date = "", times="";
        try
        {
            date = DateTime.Parse(txtAllotDate.Text.Trim()).ToString("dd-MMM-yyyy");
            
        }
        catch (Exception ex)
        {
            date = "";
        }
        if (date=="" || ddlHH.SelectedIndex== 0 || ddlMM.SelectedIndex == 0)
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Invalid Datetime!", "A");
            return;
        }
        if (ddlEntranceStatus.SelectedIndex == 0)
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Please select status!", "A");
            return;
        }
        times = times + (ddlHH.SelectedValue + ":" + ddlMM.SelectedValue + " " + ddlTT.SelectedValue);
        string finalDate = (date + " " + times);
        using (SqlCommand cmd = new SqlCommand())
        {
            string sqlu = "update AdmissionFormCollection set EntrenceDate='"+ finalDate + "', EnteranceStatus= case when '"+ ddlEntranceStatus.SelectedValue+ "'='Pending' then NULL else '" + ddlEntranceStatus.SelectedValue + "' end where RecieptNo='" + lblRecieptNo.Text + "'  and BranchCode=" + Session["BranchCode"] + "";
            cmd.CommandText=sqlu;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = _con;
            try
            {
                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Updated successfully.", "S");
                Display();
                Panel1_ModalPopupExtender.Hide();
            }
            catch (Exception)
            {
            }
        }
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
    }
}