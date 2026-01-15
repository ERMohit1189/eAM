using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI.WebControls;

namespace _8
{
    public partial class EmployeeAttendanceUsinPunch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file
            if (!IsPostBack)
            {
                txtDate.Text = BAL.objBal.CurrentDate("yyyy MMM dd");
                GetList();
            }
        }

        public void GetList()
        {
            var param = new List<SqlParameter>
            {
                (new SqlParameter("@Date", Convert.ToDateTime(txtDate.Text).ToString("yyyy MMM dd")))
            };

            rptPunch.DataSource = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("USP_UpdateEmpList", param);
            rptPunch.DataBind();

        }

        protected void lnkView_OnClick(object sender, EventArgs e)
        {
            GetList();
        }

        protected void rptPunch_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem) return;
            var lblAttendenceIn = (Label) e.Item.FindControl("lblAttendenceIn");
            var lblAttendenceOut = (Label) e.Item.FindControl("lblAttendenceOut");
            var lblFinalAttendenceOut = (Label)e.Item.FindControl("lblFinalAttendenceOut");

            lblAttendenceIn.ForeColor = lblAttendenceIn.Text == "A" ? Color.FromArgb(228, 39, 9) : Color.FromArgb(12, 173, 39);

            lblAttendenceOut.ForeColor = lblAttendenceOut.Text == "A" ? Color.FromArgb(228, 39, 9) : Color.FromArgb(12, 173, 39);

            lblFinalAttendenceOut.ForeColor = lblFinalAttendenceOut.Text == "A" ? Color.FromArgb(228, 39, 9) : Color.FromArgb(12, 173, 39);
        }

        protected void chkEdit_OnCheckedChanged(object sender, EventArgs e)
        {
            var chk = (CheckBox) sender;
            var txtPunchInTime = (TextBox) chk.NamingContainer.FindControl("txtPunchInTime");
            var txtPunchOutTime = (TextBox) chk.NamingContainer.FindControl("txtPunchOutTime");
            if (chk.Checked)
            {
                txtPunchInTime.ReadOnly = false;
                txtPunchOutTime.ReadOnly = false;
                txtPunchInTime.Focus();
            }
            else
            {
                txtPunchInTime.ReadOnly = true;
                txtPunchOutTime.ReadOnly = true;
            }
        }

        protected void chkEditAll_OnCheckedChanged(object sender, EventArgs e)
        {
            if (chkEditAll.Checked)
            {
                for (var i = 0; i < rptPunch.Items.Count; i++)
                {
                    var chkEdit = (CheckBox)rptPunch.Items[i].FindControl("chkEdit");
                    chkEdit.Checked = true;
                }
            }
            else
            {
                for (var i = 0; i < rptPunch.Items.Count; i++)
                {
                    var chkEdit = (CheckBox) rptPunch.Items[i].FindControl("chkEdit");
                    chkEdit.Checked = false;
                }
            }
        }

        protected void txtPunchTime_OnTextChanged(object sender, EventArgs e)
        {
            var txt = (TextBox)sender;
            var intime = ((TextBox)txt.NamingContainer.FindControl("txtPunchInTime")).Text == string.Empty
                ? null
                : ((TextBox)txt.NamingContainer.FindControl("txtPunchInTime")).Text;
            var outtime = ((TextBox)txt.NamingContainer.FindControl("txtPunchOutTime")).Text == string.Empty
                ? null
                : ((TextBox)txt.NamingContainer.FindControl("txtPunchOutTime")).Text;
            var lblAttendenceIn = (Label)txt.NamingContainer.FindControl("lblAttendenceIn");
            var lblAttendenceOut = (Label)txt.NamingContainer.FindControl("lblAttendenceOut");
            var lblFinalAttendenceOut = (Label)txt.NamingContainer.FindControl("lblFinalAttendenceOut");
            var date = txtDate.Text.Trim();
            var empid = ((Label)txt.NamingContainer.FindControl("lblEmpId")).Text;

            GetUpdatedAttendanceValue(intime, outtime, date, empid, lblAttendenceIn, lblAttendenceOut, lblFinalAttendenceOut);

            lblAttendenceIn.ForeColor = lblAttendenceIn.Text == "A" ? Color.FromArgb(228, 39, 9) : Color.FromArgb(12, 173, 39);

            lblAttendenceOut.ForeColor = lblAttendenceOut.Text == "A" ? Color.FromArgb(228, 39, 9) : Color.FromArgb(12, 173, 39);

            lblFinalAttendenceOut.ForeColor = lblFinalAttendenceOut.Text == "A" ? Color.FromArgb(228, 39, 9) : Color.FromArgb(12, 173, 39);

            txt.Focus();
        }
        public void GetUpdatedAttendanceValue(string intime, string outtime, string date, string empid, Label lblAttendenceIn, Label lblAttendenceOut, Label lblFinalAttendenceOut)
        {
            var param = new List<SqlParameter>
            {
                (new SqlParameter("@Intime", intime)),
                (new SqlParameter("@OutTime", outtime)),
                (new SqlParameter("@Date", date)),
                (new SqlParameter("@Empid", empid)),
                (new SqlParameter("@BranchCode", Session["BranchCode"]))
            };

            var ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("USP_GetAttendanceValueAccordingtoTime", param);
            // ReSharper disable once UseNullPropagation
            if (ds == null) return;
            if (ds.Tables[0].Rows.Count <= 0) return;
            lblAttendenceIn.Text = ds.Tables[0].Rows[0]["AttendenceIn"].ToString();
            lblAttendenceOut.Text = ds.Tables[0].Rows[0]["AttendenceOut"].ToString();
            lblFinalAttendenceOut.Text = ds.Tables[0].Rows[0]["FinalAttendance"].ToString();
        }

        protected void lnkSubmit_OnClick(object sender, EventArgs e)
        {
            SetAttendance();
        }

        public void SetAttendance()
        {
            var dt = new DataTable()
            {
                Columns = { "EmpMachineId", "EmpId", "Intime", "Outtime", "IsManual", "AttendanceDate", "AttendanceValueIn", "AttendanceValueOut", "OverAllAttendanceValue", "SessionName", "BranchCode", "LoginName" }
            };

            for (var i = 0; i < rptPunch.Items.Count; i++)
            {
                var dr = dt.NewRow();
                dr["EmpMachineId"] = ((Label) rptPunch.Items[i].FindControl("lblMachineId")).Text;
                dr["EmpId"] = ((Label)rptPunch.Items[i].FindControl("lblEmpId")).Text;
                dr["Intime"] = ((TextBox)rptPunch.Items[i].FindControl("txtPunchInTime")).Text == string.Empty ? null
                    : (txtDate.Text.Trim() + " " + ((TextBox)rptPunch.Items[i].FindControl("txtPunchInTime")).Text);

                dr["Outtime"] = ((TextBox) rptPunch.Items[i].FindControl("txtPunchOutTime")).Text == string.Empty
                    ? null
                    : (txtDate.Text.Trim() + " " + ((TextBox) rptPunch.Items[i].FindControl("txtPunchOutTime")).Text);

                dr["IsManual"] = ((CheckBox) rptPunch.Items[i].FindControl("chkEdit")).Checked ? 1 : 0;
                dr["AttendanceDate"] = txtDate.Text.Trim();
                dr["AttendanceValueIn"] = ((Label)rptPunch.Items[i].FindControl("lblAttendenceIn")).Text;
                dr["AttendanceValueOut"] = ((Label)rptPunch.Items[i].FindControl("lblAttendenceOut")).Text;
                dr["OverAllAttendanceValue"] = ((Label)rptPunch.Items[i].FindControl("lblFinalAttendenceOut")).Text;
                dr["SessionName"] = Session["SessionName"].ToString();
                dr["BranchCode"] = Session["BranchCode"].ToString();
                dr["LoginName"] = Session["LoginName"].ToString();

                dt.Rows.Add(dr);
            }
            var param = new List<SqlParameter>
            {
                (new SqlParameter("@SetAttendanceValueForEmp",dt))
            };

            var para = new SqlParameter("@msg", "")
            {
                Direction = ParameterDirection.Output
            };

            param.Add(para);

            var msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("USP_SetAttendanceValueForEmp", param);
            if (msg == "S")
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Submitted successfully.", "S");
            }
            else
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, msg, "A");
            }
        }
    }
}