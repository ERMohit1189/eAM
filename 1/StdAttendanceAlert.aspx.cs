using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
using System.Web.UI;
using System.Web.UI.WebControls;
using c4SmsNew;

namespace _1
{
    public partial class StdAttendanceAlert : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Campus camp = new Campus(); camp.LoadLoader(loader);
            if (!IsPostBack)
            {
                BLL.BLLInstance.loadClass(drpClass, Session["SessionName"].ToString());
                BLL.BLLInstance.loadSection(drpSection, Session["SessionName"].ToString(), drpClass.SelectedValue);
                BLL.BLLInstance.loadBranch(drpBranch, Session["SessionName"].ToString(), drpClass.SelectedValue);
                txtDate.Text=Convert.ToDateTime(BAL.objBal.CurrentDate()).ToString("dd-MMM-yyyy");
            }
        }

        protected void drpClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            BLL.BLLInstance.loadSection(drpSection, Session["SessionName"].ToString(), drpClass.SelectedValue);
            BLL.BLLInstance.loadBranch(drpBranch, Session["SessionName"].ToString(), drpClass.SelectedValue);
        }

        public void GetStdAbsenteesAlert()
        {
            var param = new List<SqlParameter>
            {
                new SqlParameter("@ClassId",drpClass.SelectedValue),
                new SqlParameter("@SectionId",drpSection.SelectedValue),
                new SqlParameter("@BranchId",drpBranch.SelectedValue),
                new SqlParameter("@AttendanceDate",txtDate.Text.Trim()),
                new SqlParameter("@AttendanceValue",drpAttendance.SelectedValue),
                new SqlParameter("@SessionName",Session["SessionName"].ToString()),
                new SqlParameter("@BranchCode",Session["BranchCode"].ToString())
            };

            grdAlertData.DataSource = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("USP_StdAbsenteesAlert", param);
            grdAlertData.DataBind();

        }

        protected void lnkView_Click(object sender, EventArgs e)
        {
            GetStdAbsenteesAlert();
        }

        protected void lnkSubmit_Click(object sender, EventArgs e)
        {
            SendSms();
        }

        public void SendSms()
        {
            for (int i = 0; i < grdAlertData.Rows.Count; i++)
            {
                var chk = (CheckBox)grdAlertData.Rows[i].FindControl("chk");
                if (chk.Checked)
                {

                    var txtSms = (TextBox)grdAlertData.Rows[i].FindControl("txtSMS");
                    var lblContactNo = (Label)grdAlertData.Rows[i].FindControl("lblFamilyContactNo");
                    var textMsg = txtSms.Text.Trim();
                    var sadpNew = new SMSAdapterNew();
                    var thread = new Thread(() => { sadpNew.Send(textMsg, lblContactNo.Text.Trim(), ""); });
                    thread.Start();
                    Thread.Sleep(500);
                }
            }

            Campus camp = new Campus(); camp.msgbox(Page, divmsg, "Message Send successfully.", "S");

        }

        protected void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            var chkAll = (CheckBox)sender;
            for (int i = 0; i < grdAlertData.Rows.Count; i++)
            {
                var chk = (CheckBox)grdAlertData.Rows[i].FindControl("chk");
                chk.Checked = chkAll.Checked;
            }
        }
    }
}