using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using Layered_TimeTable;
using System.Web.UI.HtmlControls;

namespace ap
{
    public partial class ApAdmissionDetails : System.Web.UI.Page
    {
        private SqlConnection _con;
        private readonly Campus _oo;
        private DataSet _ds;
        public ApAdmissionDetails()
        {
            _con = new SqlConnection();
            _oo = new Campus();
            _ds = new DataSet();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["txtno"]))
                {
                    if ((string)Session["txnchk"] == Request.QueryString["txtno"])
                    {
                        GetAdmissionDetails(Request.QueryString["txtno"]);
                    }
                    else if (Request.QueryString["txtno"].Length != 10)
                    {
                        var sql = "SELECT Mobile FROM AdmissionFormOnline WHERE TxnID ='"+Request.QueryString["txtno"]+"'";
                        var mo = _oo.ReturnTag(sql, "Mobile");
                        if (!string.IsNullOrEmpty(mo))
                        {
                            if ((string) Session["txnchk"] == mo)
                            {
                                GetAdmissionDetails(mo);
                            }
                            else
                            {
                                Response.Redirect("~/ap/default.aspx");
                            }
                        }
                        else
                        {
                            Response.Redirect("~/ap/default.aspx");
                        }
                    }
                }
            }
        }

        private void GetAdmissionDetails(String txtNo)
        {
            var param = new List<SqlParameter>();
            param.Add(new SqlParameter("@QueryFor", "S"));
            param.Add(new SqlParameter("@Mobile", txtNo));
            _ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("USP_AdmissionFormOnline", param);
            if (_ds != null)
            {
                DataTable dt;
                dt = _ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    grdStRecord.DataSource = dt;
                    grdStRecord.DataBind();

                    string sqlss = "select OnlineRegistration from  admissionDatePermission where OnlineRegistration=1 and BranchCode=" + Session["BranchCode"] + "";
                    if (!_oo.Duplicate(sqlss))
                    {
                        grdStRecord.HeaderRow.Cells[15].Visible = false;
                    }
                    for (int i = 0; i < grdStRecord.Rows.Count; i++)
                    {
                        Label Label12 = (Label)grdStRecord.Rows[i].FindControl("Label12");
                        Label RecieptNo = (Label)grdStRecord.Rows[i].FindControl("RecieptNo");
                        Label lblReg = (Label)grdStRecord.Rows[i].FindControl("lblReg");
                        Label lblFail = (Label)grdStRecord.Rows[i].FindControl("lblFail");
                        Label lblEntrenceDate = (Label)grdStRecord.Rows[i].FindControl("lblEntrenceDate");
                        Label lblAdmission = (Label)grdStRecord.Rows[i].FindControl("lblAdmission");
                        Label lblEnteranceStatus = (Label)grdStRecord.Rows[i].FindControl("lblEnteranceStatus");
                        LinkButton linkRegister = (LinkButton)grdStRecord.Rows[i].FindControl("linkRegister");
                        if (Label12.Text==""|| Label12.Text == "Cancelled")
                        {
                            Label12.Text = "Cancelled";
                            RecieptNo.Text = "-";
                        }

                        string sql = "select Template, case when isnull(AdmissionStatus,0)=0 then 'False' else 'True' end AdmissionStatus, case when isnull(EntrenceDate, '')='' then '' else format(EntrenceDate, 'dd-MMM-yyyy hh:mm tt') end EntrenceDate, EnteranceStatus  from AdmissionFormCollection where RecieptNo='" + RecieptNo.Text + "' and BranchCode=" + Session["BranchCode"] + "";
                        string _sql = "select BranchCode from tblAutometedSRNO where BranchCode=" + Session["BranchCode"] + " and SrNoType<>'Manual'";
                        if (_oo.ReturnTag(sql, "EnteranceStatus") == "Pending" || _oo.ReturnTag(sql, "EnteranceStatus") == "")
                        {
                            linkRegister.Visible = false;
                            lblReg.Visible = false;
                            lblAdmission.Visible = true;
                            lblFail.Visible = false;
                        }
                        else
                        {
                            if (_oo.Duplicate(sql) && _oo.ReturnTag(sql, "AdmissionStatus") == "False" && _oo.Duplicate(_sql) && _oo.ReturnTag(sql, "EnteranceStatus") == "Passed")
                            {
                                linkRegister.Visible = true;
                                lblReg.Visible = false;
                                lblAdmission.Visible = false;
                                lblFail.Visible = false;
                            }
                            else
                            {
                                linkRegister.Visible = false;
                                lblReg.Visible = true;
                                lblAdmission.Visible = false;
                                lblFail.Visible = false;
                            }
                        }
                        if (!_oo.Duplicate(sqlss))
                        {
                            grdStRecord.Rows[i].Cells[15].Visible = false;
                        }
                        lblEntrenceDate.Text = (_oo.ReturnTag(sql, "EntrenceDate") == "" ? "Pending" : DateTime.Parse(_oo.ReturnTag(sql, "EntrenceDate")).ToString("dd-MMM-yyyy hh:mm tt"));
                        lblEnteranceStatus.Text = (_oo.ReturnTag(sql, "EnteranceStatus") == "" ? "Pending" : _oo.ReturnTag(sql, "EnteranceStatus"));
                        if (lblEnteranceStatus.Text== "Failed")
                        {
                            linkRegister.Visible = false;
                            lblReg.Visible = false;
                            lblAdmission.Visible = false;
                            lblFail.Visible = true;
                        }
                        if (lblEnteranceStatus.Text == "Passed" && _oo.ReturnTag(sql, "AdmissionStatus") == "False")
                        {
                            linkRegister.Visible = true;
                            lblReg.Visible = false;
                            lblAdmission.Visible = false;
                            lblFail.Visible = false;
                        }
                    }
                }
                else
                {
                    grdStRecord.DataSource = null;
                    grdStRecord.DataBind(); 
                    Response.Redirect("~/ap/default.aspx");
                }
                
            }
        }
        protected void btnpay_OnClick(object sender, EventArgs e)
        {
            LinkButton chk = (LinkButton)sender;
            var recieptNo = (Label)chk.NamingContainer.FindControl("RecieptNo");
            var txnId = (Label)chk.NamingContainer.FindControl("TxnID");
            var mobile = (Label)chk.NamingContainer.FindControl("Mobile");
            var sessionName = (Label)chk.NamingContainer.FindControl("SessionName");
            var amount = (Label)chk.NamingContainer.FindControl("Amount");
            decimal balance;
            decimal.TryParse(amount.Text.Trim(), out balance);
        }

        protected void LinkButton2_OnClick(object sender, EventArgs e)
        {
            Session["Logintype"] = "Gardian";
            LinkButton chk = (LinkButton)sender;
            var recieptNo = (Label)chk.NamingContainer.FindControl("RecieptNo");
            try
            {
                Session["isDuplicate"] = "Yes";
                ScriptManager.RegisterClientScriptBlock(Page, GetType(), "redirect", "window.open('../2/pafReciept.aspx?print=1&rid=" + recieptNo.Text.Trim() + "','_blank')", true);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        protected void lnkPrintAF_OnClick(object sender, EventArgs e)
        {
            LinkButton chk = (LinkButton)sender;
            var recieptNo = (Label)chk.NamingContainer.FindControl("RecieptNo");
            try
            {
                string sql = "select Template from AdmissionFormCollection where RecieptNo='" + recieptNo.Text + "' and BranchCode=" + Session["BranchCode"] + "";
                if (_oo.ReturnTag(sql, "Template").ToLower() == "template 1")
                {
                    ScriptManager.RegisterClientScriptBlock(Page, GetType(), "redirect", "window.open('../2/admtemplate1.aspx?print=1&rid=" + recieptNo.Text.Trim() + "','_blank')", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, GetType(), "redirect", "window.open('../2/admtemplate2.aspx?print=1&rid=" + recieptNo.Text.Trim() + "','_blank')", true);
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }
        protected void linkRegister_Click(object sender, EventArgs e)
        {
            LinkButton chk = (LinkButton)sender;
            var recieptNo = (Label)chk.NamingContainer.FindControl("RecieptNo");
            string sql = "select SessionName, FatherContactNo from AdmissionFormCollection where RecieptNo='" + recieptNo.Text + "' and BranchCode=" + Session["BranchCode"] + "";
            Session["SessionName"] =_oo.ReturnTag(sql, "SessionName").ToString();
            Session["LoginName"] = _oo.ReturnTag(sql, "FatherContactNo").ToString();
            Session["Logintype"] = "FromAdmission";
            Session["RecieptNo"] = recieptNo.Text;
            Response.Redirect("../1/student_registration.aspx");
        }

        public override void Dispose()
        {
            _con.Dispose();
            _oo.Dispose();
            _ds.Dispose();
        }




        
    }
}