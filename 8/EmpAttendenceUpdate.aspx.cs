using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI.WebControls;


namespace _8
{
    public partial class EmpAttendanceUpdate : System.Web.UI.Page
    {
        private SqlConnection _con;
        private readonly Campus _oo;
        private string _sql = "";
        private DataTable _dt;
        public SqlConnection con;
        public SqlCommand cmd = new SqlCommand();
        public SqlDataAdapter ad = new SqlDataAdapter();
        private string _attendanceStatus = "";

        public EmpAttendanceUpdate()
        {
            _dt = new DataTable();
            _oo = new Campus();
            _con = new SqlConnection();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            _oo.LoadLoader(loader);  //in cs file
            if (!IsPostBack)
            {
                txtDate.Text = BAL.objBal.CurrentDate("yyyy MMM dd");
                GetShiftCat();
                divExport.Visible = false;
                lnkSubmit.Visible = false;
            }
            
        }

        public void GetList()
        {

            var param = new List<SqlParameter>
            {
                new SqlParameter("@Date", Convert.ToDateTime(txtDate.Text).ToString("yyyy MMM dd")),
                new SqlParameter("@BranchCode", Session["BranchCode"].ToString()),
                new SqlParameter("@Shift", ddlShiftCat.SelectedValue),
                new SqlParameter("@DisplayOrder", ddlDisplayOrder.SelectedValue)
            };

            rptPunch.DataSource = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("USP_UpdateEmpAttendence", param);
            rptPunch.DataBind();
            divExport.Visible = true;
            lnkSubmit.Visible = true;
            chkEditAll.Checked = false;
        }

        public void GetShiftCat()
        {
            //var param = new List<SqlParameter>
            //{
            //    new SqlParameter("@Date", Convert.ToDateTime(txtDate.Text).ToString("yyyy MMM dd")),
            //    new SqlParameter("@BranchCode", Session["BranchCode"].ToString())
            //};

            //_sql = "Select A02ID ShiftID,( ShiftName + ': ' + Convert(varchar(15),FromTime) + ' - ' + Convert(varchar(15),ToTime)) ShiftTime from A02_EmpShiftMaster where BranchCode = " + Session["BranchCode"].ToString() + " and IsDelete = 0";
            //_oo.FillDropDownWithOutSelect(_sql, ddlShiftCat, "ShiftTime");

            _sql = "select ShiftID,ShiftTime,ShiftIn,ShiftOut from GetAllShiftRecords_UDF(" + Session["BranchCode"].ToString() + ")";
            _oo.FillDropDown_withValue(_sql, ddlShiftCat, "ShiftTime", "ShiftID");
            ddlShiftCat.Items.Insert(0, new ListItem("Select", "0"));
        }

        protected void lnkView_OnClick(object sender, EventArgs e)
        {
            GetList();
        }

        protected void rptPunch_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem) return;
            var lblAttendenceIn = (Label)e.Item.FindControl("lblAttendenceIn");
            var lblAttendenceOut = (Label)e.Item.FindControl("lblAttendenceOut");
            var lblSavedAttendence = (Label)e.Item.FindControl("lblSavedAttendence");
            var lblCalcAttendence = (Label)e.Item.FindControl("lblCalcAttendence");

            lblAttendenceIn.ForeColor = lblAttendenceIn.Text == "A" ? Color.FromArgb(228, 39, 9) : Color.FromArgb(12, 173, 39);

            lblAttendenceOut.ForeColor = lblAttendenceOut.Text == "A" ? Color.FromArgb(228, 39, 9) : Color.FromArgb(12, 173, 39);

            lblSavedAttendence.ForeColor = lblSavedAttendence.Text != lblCalcAttendence.Text ? Color.FromArgb(39, 173, 228) : (lblSavedAttendence.Text == "A" ? Color.FromArgb(228, 39, 9) : Color.FromArgb(12, 173, 39));

            lblCalcAttendence.ForeColor = lblSavedAttendence.Text != lblCalcAttendence.Text ? Color.FromArgb(39, 173, 228) : (lblCalcAttendence.Text == "A" ? Color.FromArgb(228, 39, 9) : Color.FromArgb(12, 173, 39));
        }
        protected void chkEditAll_OnCheckedChanged(object sender, EventArgs e)
        {
            if (chkEditAll.Checked)
            {
                for (var i = 0; i < rptPunch.Items.Count; i++)
                {
                    //var txtPunchInTime = (TextBox)rptPunch.Items[i].FindControl("txtPunchInTime");
                    //var txtPunchOutTime = (TextBox)rptPunch.Items[i].FindControl("txtPunchOutTime");
                    var chkEdit = (CheckBox)rptPunch.Items[i].FindControl("chkEdit");
                    chkEdit.Checked = true;
                    //txtPunchInTime.ReadOnly = false;
                    //txtPunchOutTime.ReadOnly = false;
                }
            }
            else
            {
                for (var i = 0; i < rptPunch.Items.Count; i++)
                {
                    //var txtPunchInTime = (TextBox)rptPunch.Items[i].FindControl("txtPunchInTime");
                    //var txtPunchOutTime = (TextBox)rptPunch.Items[i].FindControl("txtPunchOutTime");
                    var chkEdit = (CheckBox)rptPunch.Items[i].FindControl("chkEdit");
                    chkEdit.Checked = false;
                    //txtPunchInTime.ReadOnly = true;
                    //txtPunchOutTime.ReadOnly = true;
                }
            }
        }
        protected void chkEdit_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            //var txtPunchInTime = (TextBox)chk.NamingContainer.FindControl("txtPunchInTime");
            //var txtPunchOutTime = (TextBox)chk.NamingContainer.FindControl("txtPunchOutTime");
            var chkEdit = (CheckBox)chk.NamingContainer.FindControl("chkEdit");
            if (chkEdit.Checked)
            {
                chkEdit.Checked = true;
                //txtPunchInTime.ReadOnly = false;
                //txtPunchOutTime.ReadOnly = false;
            }
            else
            {
                chkEdit.Checked = false;
                //txtPunchInTime.ReadOnly = true;
                //txtPunchOutTime.ReadOnly = true;
            }
        }

        //protected void txtPunchTime_OnTextChanged(object sender, EventArgs e)
        //{
        //    var txt = (TextBox)sender;
        //    var intime = ((TextBox)txt.NamingContainer.FindControl("txtPunchInTime")).Text == string.Empty
        //        ? null
        //        : ((TextBox)txt.NamingContainer.FindControl("txtPunchInTime")).Text;
        //    var outtime = ((TextBox)txt.NamingContainer.FindControl("txtPunchOutTime")).Text == string.Empty
        //        ? null
        //        : ((TextBox)txt.NamingContainer.FindControl("txtPunchOutTime")).Text;
        //    var lblAttendenceIn = (Label)txt.NamingContainer.FindControl("lblAttendenceIn");
        //    var lblAttendenceOut = (Label)txt.NamingContainer.FindControl("lblAttendenceOut");
        //    var lblSavedAttendence = (Label)txt.NamingContainer.FindControl("lblSavedAttendence");
        //    var date = txtDate.Text.Trim();
        //    var empid = ((Label)txt.NamingContainer.FindControl("lblEmpId")).Text;

        //    GetUpdatedAttendanceValue(intime, outtime, date, empid, lblAttendenceIn, lblAttendenceOut, lblSavedAttendence);

        //    lblAttendenceIn.ForeColor = lblAttendenceIn.Text == "A" ? Color.FromArgb(228, 39, 9) : Color.FromArgb(12, 173, 39);

        //    lblAttendenceOut.ForeColor = lblAttendenceOut.Text == "A" ? Color.FromArgb(228, 39, 9) : Color.FromArgb(12, 173, 39);

        //    lblSavedAttendence.ForeColor = lblSavedAttendence.Text == "A" ? Color.FromArgb(228, 39, 9) : Color.FromArgb(12, 173, 39);

        //    txt.Focus();
        //}
        public void GetUpdatedAttendanceValue(string intime, string outtime, string date, string empid, Label lblAttendenceIn, Label lblAttendenceOut, Label lblSavedAttendence)
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
            lblSavedAttendence.Text = ds.Tables[0].Rows[0]["FinalAttendance"].ToString();
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

            var count = 0;

            for (var i = 0; i < rptPunch.Items.Count; i++)
            {
                if (!((CheckBox)rptPunch.Items[i].FindControl("chkEdit")).Checked) continue;
                count = 1;
                var dr = dt.NewRow();
                dr["EmpMachineId"] = ((Label)rptPunch.Items[i].FindControl("lblMachineId")).Text;
                dr["EmpId"] = ((Label)rptPunch.Items[i].FindControl("lblEmpId")).Text;
                dr["Intime"] = ((Label)rptPunch.Items[i].FindControl("lblPunchInTime")).Text == string.Empty ? null
                    : (txtDate.Text.Trim() + " " + ((Label)rptPunch.Items[i].FindControl("lblPunchInTime")).Text);

                dr["Outtime"] = ((Label)rptPunch.Items[i].FindControl("lblPunchOutTime")).Text == string.Empty
                    ? null
                    : (txtDate.Text.Trim() + " " + ((Label)rptPunch.Items[i].FindControl("lblPunchOutTime")).Text);

                dr["IsManual"] = ((CheckBox)rptPunch.Items[i].FindControl("chkEdit")).Checked ? 1 : 0;
                dr["AttendanceDate"] = txtDate.Text.Trim();
                dr["AttendanceValueIn"] = ((Label)rptPunch.Items[i].FindControl("lblAttendenceIn")).Text;
                dr["AttendanceValueOut"] = ((Label)rptPunch.Items[i].FindControl("lblAttendenceOut")).Text;
                dr["OverAllAttendanceValue"] = ((Label)rptPunch.Items[i].FindControl("lblCalcAttendence")).Text;
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

            if (count > 0)
            {
                var msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("USP_EmpAttendanceUpdate", param);
                if (msg == "S")
                {
                    Campus camp = new Campus();
                    camp.msgbox(Page, msgbox, "Updated successfully.", "S");
                    GetList();
                }
                else
                {
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, msg, "A");
                }
            }
            else
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Only checkbox checked record can update so please first check checkbox!", "A");
            }

        }
    }
}