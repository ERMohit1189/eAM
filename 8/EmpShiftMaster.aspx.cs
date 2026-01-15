using System;
using System.Data;
using System.Web.UI.WebControls;

namespace _8
{
    public partial class AdminEmpShiftMaster : System.Web.UI.Page
    {
        public static string Msg = "", Sql = "", ShiftName = "";
        public static int A02Id;
        private static DataTable _dt;
        Campus oo = new Campus();

        protected void Page_Load(object sender, EventArgs e)
        {
            // ReSharper disable once InvertIf
            Campus camp = new Campus(); camp.LoadLoader(loader);
            if (!IsPostBack)
            {

                GetTime();
                GetTime0();
                GetDesignation(ddlDesignation);
                GetShiftMaster();
            }
        }

        public void GetTime()
        {
            BLL.FillHourInDropDownlist(ddlFromHour);
            BLL.FillMinuteInDropDownlist(ddlFromMinute);
            ddlFromMinute.SelectedIndex = 1;
            BLL.FillSecondInDropDownlist(ddlFromSecond);
            BLL.FillHourInDropDownlist(ddlToHour);
            BLL.FillMinuteInDropDownlist(ddlToMinute);
            ddlToMinute.SelectedIndex = 1;
            BLL.FillSecondInDropDownlist(ddlToSecond);

            //BLL.FillHourInDropDownlist(ddlLunchFromH);
            //BLL.FillMinuteInDropDownlist(ddlLunchFromM);
            //BLL.FillMinuteInDropDownlist(ddlLunchFromS);
            //BLL.FillHourInDropDownlist(ddlLunchToH);
            //BLL.FillMinuteInDropDownlist(ddlLunchToM);
            //BLL.FillSecondInDropDownlist(ddlLunchToS);

            BLL.FillHourInDropDownlist(ddlGraceTimeInH);
            ddlGraceTimeInH.SelectedIndex = 1;
            BLL.FillMinuteInDropDownlist(ddlGraceTimeInM);
            ddlGraceTimeInM.SelectedIndex = 1;
            BLL.FillSecondInDropDownlist(ddlGraceTimeInS);

            BLL.FillHourInDropDownlist(ddlGraceTimeOutH);
            ddlGraceTimeOutH.SelectedIndex = 1;
            BLL.FillMinuteInDropDownlist(ddlGraceTimeOutM);
            ddlGraceTimeOutM.SelectedIndex = 1;
            BLL.FillSecondInDropDownlist(ddlGraceTimeOutS);

            BLL.FillHourInDropDownlist(ddlSLTimeH);
            BLL.FillMinuteInDropDownlist(ddlSLTimeM);
            ddlSLTimeM.SelectedIndex = 1;
            BLL.FillSecondInDropDownlist(ddlSLTimeS);
            BLL.FillHourInDropDownlist(ddlSLTimeHO);
            BLL.FillMinuteInDropDownlist(ddlSLTimeMO);
            ddlSLTimeMO.SelectedIndex = 1;
            BLL.FillSecondInDropDownlist(ddlSLTimeSO);
            BLL.FillHourInDropDownlist(ddlHDTimeH);
            BLL.FillMinuteInDropDownlist(ddlHDTimeM);
            ddlHDTimeM.SelectedIndex = 1;
            BLL.FillSecondInDropDownlist(ddlHDTimeS);

            ddlToType.SelectedIndex = 1;
        }

        public void GetTime0()
        {
            BLL.FillHourInDropDownlist(ddlFromHour0);
            BLL.FillMinuteInDropDownlist(ddlFromMinute0);
            BLL.FillSecondInDropDownlist(ddlFromSecond0);
            BLL.FillHourInDropDownlist(ddlToHour0);
            BLL.FillMinuteInDropDownlist(ddlToMinute0);
            BLL.FillSecondInDropDownlist(ddlToSecond0);

            //BLL.FillHourInDropDownlist(ddlLunchFromH0);
            //BLL.FillMinuteInDropDownlist(ddlLunchFromM0);
            //BLL.FillSecondInDropDownlist(ddlLunchFromS0);
            //BLL.FillHourInDropDownlist(ddlLunchToH0);
            //BLL.FillMinuteInDropDownlist(ddlLunchToM0);
            //BLL.FillSecondInDropDownlist(ddlLunchToS0);

            BLL.FillHourInDropDownlist(ddlGraceTimeH0);
            BLL.FillMinuteInDropDownlist(ddlGraceTimeM0);
            BLL.FillSecondInDropDownlist(ddlGraceTimeS0);

            BLL.FillHourInDropDownlist(ddlGraceTimeOutH0);
            BLL.FillMinuteInDropDownlist(ddlGraceTimeOutM0);
            BLL.FillSecondInDropDownlist(ddlGraceTimeOutS0);

            BLL.FillHourInDropDownlist(ddlSLTimeH0);
            BLL.FillMinuteInDropDownlist(ddlSLTimeM0);
            BLL.FillSecondInDropDownlist(ddlSLTimeS0);

            BLL.FillHourInDropDownlist(ddlSLTimeHO0);
            BLL.FillMinuteInDropDownlist(ddlSLTimeMO0);
            BLL.FillSecondInDropDownlist(ddlSLTimeSO0);

            BLL.FillHourInDropDownlist(ddlHDTimeH0);
            BLL.FillMinuteInDropDownlist(ddlHDTimeM0);
            BLL.FillSecondInDropDownlist(ddlHDTimeS0);
        }

        public void GetDesignation(DropDownList ddl)
        {
            Sql = "SELECT D.EmpDesId,D.EmpDesName FROM EmpDesMaster D where D.BranchCode=" + Session["BranchCode"].ToString() + " ORDER BY D.EmpDesName";

            if (oo.Duplicate(Sql))
            {
                oo.FillDropDown_withValue(Sql, ddl, "EmpDesName", "EmpDesId");
            }
            else
            {
                ddl.Items.Clear();
            }
        }

        public void Reset()
        {
            new Campus().ClearControls(tblInsert);
            ddlFromHour.SelectedIndex = 0;
            ddlFromMinute.SelectedIndex = 0;
            ddlFromSecond.SelectedIndex = 1;
            ddlToHour.SelectedIndex = 0;
            ddlToMinute.SelectedIndex = 0;
            ddlToSecond.SelectedIndex = 1;
            txtShiftName.Text = "";
            txtShiftTime.Text = "";

            //ddlLunchFromH.SelectedIndex = 0;
            //ddlLunchFromM.SelectedIndex = 0;
            //ddlLunchFromT.SelectedIndex = 0;

            //ddlLunchToH.SelectedIndex = 0;
            //ddlLunchToM.SelectedIndex = 0;
            //ddlLunchToT.SelectedIndex = 0;

            ddlGraceTimeInH.SelectedIndex = 1;
            ddlGraceTimeInM.SelectedIndex = 1;
            ddlGraceTimeInS.SelectedIndex = 1;
            ddlGraceTimeOutH.SelectedIndex = 1;
            ddlGraceTimeOutM.SelectedIndex = 1;
            ddlGraceTimeOutS.SelectedIndex = 1;
            ddlSLTimeH.SelectedIndex = 0;
            ddlSLTimeM.SelectedIndex = 0;
            ddlSLTimeS.SelectedIndex = 1;
            ddlSLTimeHO.SelectedIndex = 0;
            ddlSLTimeMO.SelectedIndex = 0;
            ddlSLTimeSO.SelectedIndex = 1;
            ddlHDTimeH.SelectedIndex = 0;
            ddlHDTimeM.SelectedIndex = 0;
            ddlHDTimeS.SelectedIndex = 1;

            ddlDesignation.SelectedIndex = 0;
        }

        public void Reset0()
        {
            new Campus().ClearControls(tblInsert0);
            ddlFromHour0.SelectedIndex = 0;
            ddlFromMinute0.SelectedIndex = 0;
            ddlFromSecond0.SelectedIndex = 1;
            ddlToHour0.SelectedIndex = 0;
            ddlToMinute0.SelectedIndex = 0;
            ddlToSecond0.SelectedIndex = 1;
            txtShiftName0.Text = "";
            txtShiftTime0.Text = "";

            ddlGraceTimeH0.SelectedIndex = 1;
            ddlGraceTimeM0.SelectedIndex = 1;
            ddlGraceTimeS0.SelectedIndex = 1;
            ddlGraceTimeOutH0.SelectedIndex = 1;
            ddlGraceTimeOutM0.SelectedIndex = 1;
            ddlGraceTimeOutS0.SelectedIndex = 1;
            ddlSLTimeH0.SelectedIndex = 0;
            ddlSLTimeM0.SelectedIndex = 0;
            ddlSLTimeS0.SelectedIndex = 1;
            ddlSLTimeHO0.SelectedIndex = 0;
            ddlSLTimeMO0.SelectedIndex = 0;
            ddlSLTimeSO0.SelectedIndex = 1;
            ddlHDTimeH0.SelectedIndex = 0;
            ddlHDTimeM0.SelectedIndex = 0;
            ddlHDTimeS0.SelectedIndex = 1;
        }

        public void Validation()
        {
            Msg = "";
            if (txtShiftName.Text.Trim() == "")
            {
                Msg += "Enter Shift Name !" + "\\n";
            }
            if (ddlFromHour.SelectedIndex < 1 || ddlFromMinute.SelectedIndex < 1 || ddlToHour.SelectedIndex < 1 || ddlToMinute.SelectedIndex < 1)
            {
                Msg += "Select Valid Timing !" + "\\n";
            }

            //if (ddlLunchFromH.SelectedIndex < 1 || ddlLunchFromM.SelectedIndex < 1 || ddlLunchToH.SelectedIndex < 1 || ddlLunchToM.SelectedIndex < 1)
            //{
            //    Msg += "Select Valid Lunch Timing !" + "\\n";
            //}

            if (ddlGraceTimeInH.SelectedIndex < 1 || ddlGraceTimeInM.SelectedIndex < 1)
            {
                Msg += "Select Valid Grace Time !" + "\\n";
            }

            if (Msg == "")
            {
                InsertShiftMaster();
            }
            else
            {
                BLL.BLLInstance.ShowMSG(Msg);
            }
        }

        public void Validation0()
        {
            Msg = "";
            if (txtShiftName0.Text.Trim() == "")
            {
                Msg += "Enter Shift Name !" + "\\n";
            }
            if (ddlFromHour0.SelectedIndex < 1 || ddlFromMinute0.SelectedIndex < 1 || ddlToHour0.SelectedIndex < 1 || ddlToMinute0.SelectedIndex < 1)
            {
                Msg += "Select Valid Timing !" + "\\n";
            }
            //if (ddlLunchFromH0.SelectedIndex < 1 || ddlLunchFromM0.SelectedIndex < 1 || ddlLunchToH0.SelectedIndex < 1 || ddlLunchToM0.SelectedIndex < 1)
            //{
            //    Msg += "Select Valid Lunch Timing !" + "\\n";
            //}
            if (ddlGraceTimeH0.SelectedIndex < 1 || ddlGraceTimeM0.SelectedIndex < 1)
            {
                Msg += "Select Valid Grace Time !" + "\\n";
            }
            if (Msg == "")
            {
                InsertShiftMaster0();
            }
            else
            {
                BLL.BLLInstance.ShowMSG(Msg);
            }
        }

        public void InsertShiftMaster()
        {
            var obj = new BAL.clsShiftMaster
            {
                FromTime = ddlFromHour.SelectedValue + ':' + ddlFromMinute.SelectedValue + ':' + ddlFromSecond.SelectedValue + ' ' + ddlFromType.SelectedValue,
                IsActive = 1,
                ShiftName = txtShiftName.Text.Trim(),
                SQL = Sql,
                ToTime = ddlToHour.SelectedValue + ':' + ddlToMinute.SelectedValue + ':' + ddlToSecond.SelectedValue + ' ' + ddlToType.SelectedValue,
                T01ID = A02Id,
                ShiftTime = (txtShiftTime.Text.Trim().Contains("Sec") ? txtShiftTime.Text.Trim().Replace("Hr", ":").Replace("Min", ":") : txtShiftTime.Text.Trim().Replace("Hr", ":").Replace("Min", "")).Replace("Sec", "").Replace(" ", ""),
                //ShiftTime = txtShiftTime.Text.Trim().Replace("Hr", ":").Replace("Min", ":").Replace("Sec", "").Replace(" ", ""),
                //LunchFromTime = ddlLunchFromH.SelectedValue + ':' + ddlLunchFromM.SelectedValue + ' ' + ddlLunchFromT.SelectedValue,
                //LunchToTime = ddlLunchToH.SelectedValue + ':' + ddlLunchToM.SelectedValue + ' ' + ddlLunchToT.SelectedValue,
                GraceTime = ddlGraceTimeInH.SelectedValue + ':' + ddlGraceTimeInM.SelectedValue + ':' + ddlGraceTimeInS.SelectedValue,
                DesID = Convert.ToInt32(ddlDesignation.SelectedValue),
                GraceTimeOut = ddlGraceTimeOutH.SelectedValue + ':' + ddlGraceTimeOutM.SelectedValue + ':' + ddlGraceTimeOutS.SelectedValue,

                SlTimeHh = ddlSLTimeH.SelectedValue,
                SlTimeMm = ddlSLTimeM.SelectedValue,
                SlTimeSs = ddlSLTimeS.SelectedValue,
                SlTimeTt = ddlSLTimeT.SelectedValue,

                SlTimeHhO = ddlSLTimeHO.SelectedValue,
                SlTimeMmO = ddlSLTimeMO.SelectedValue,
                SlTimeSsO = ddlSLTimeSO.SelectedValue,
                SlTimeTtO = ddlSLTimeTO.SelectedValue,

                HdTimeHh = ddlHDTimeH.SelectedValue,
                HdTimeMm = ddlHDTimeM.SelectedValue,
                HdTimeSs = ddlHDTimeS.SelectedValue,
                HdTimeTt = ddlHDTimeT.SelectedValue
            };

            Msg = new DAL().SetShiftMaster(obj, true);
            if (Msg == "")
            {
                ShiftName = txtShiftName.Text.Trim();
                Msg = Sql;

                GetShiftMaster();
                Reset();
            }

            BLL.BLLInstance.ShowMSG(Msg);
        }

        public void InsertShiftMaster0()
        {
            var obj = new BAL.clsShiftMaster
            {
                BranchCode = Convert.ToInt16(Session["BranchCode"].ToString()),
                FromTime = ddlFromHour0.SelectedValue + ':' + ddlFromMinute0.SelectedValue + ':' + ddlFromSecond0.SelectedValue + ' ' + ddlFromType0.SelectedValue,
                IsActive = 1,
                LoginName = Session["LoginName"].ToString(),
                SessionName = Session["SessionName"].ToString(),
                ShiftName = txtShiftName0.Text.Trim(),
                SQL = Sql,
                ToTime = ddlToHour0.SelectedValue + ':' + ddlToMinute0.SelectedValue + ':' + ddlToSecond0.SelectedValue + ' ' + ddlToType0.SelectedValue,
                ShiftTime = (txtShiftTime0.Text.Trim().Contains("Sec") ? txtShiftTime0.Text.Trim().Replace("Hr", ":").Replace("Min", ":") : txtShiftTime0.Text.Trim().Replace("Hr", ":").Replace("Min", "")).Replace("Sec", "").Replace(" ", ""),
                A02ID = A02Id,
                //LunchFromTime = ddlLunchFromH0.SelectedValue + ':' + ddlLunchFromM0.SelectedValue + ' ' +  ddlLunchFromT0.SelectedValue,
                //LunchToTime = ddlLunchToH0.SelectedValue + ':' + ddlLunchToM0.SelectedValue + ' ' +  ddlLunchToT0.SelectedValue,
                GraceTime = ddlGraceTimeH0.SelectedValue + ':' + ddlGraceTimeM0.SelectedValue + ':' + ddlGraceTimeS0.SelectedValue,
                DesID = Convert.ToInt32(ddlDesignation0.SelectedValue),

                GraceTimeOut = ddlGraceTimeOutH0.SelectedValue + ':' + ddlGraceTimeOutM0.SelectedValue + ':' + ddlGraceTimeOutS0.SelectedValue,

                SlTimeHh = ddlSLTimeH0.SelectedValue,
                SlTimeMm = ddlSLTimeM0.SelectedValue,
                SlTimeSs = ddlSLTimeS0.SelectedValue,
                SlTimeTt = ddlSLTimeT0.SelectedValue,

                SlTimeHhO = ddlSLTimeHO0.SelectedValue,
                SlTimeMmO = ddlSLTimeMO0.SelectedValue,
                SlTimeSsO = ddlSLTimeSO0.SelectedValue,
                SlTimeTtO = ddlSLTimeTO0.SelectedValue,

                HdTimeHh = ddlHDTimeH0.SelectedValue,
                HdTimeMm = ddlHDTimeM0.SelectedValue,
                HdTimeSs = ddlHDTimeS0.SelectedValue,
                HdTimeTt = ddlHDTimeT0.SelectedValue
            };



            Msg = new DAL().SetShiftMaster(obj, true);
            if (Msg == "")
            {
                ShiftName = txtShiftName0.Text.Trim();

                Msg = Sql;

                GetShiftMaster();
                Reset0();
            }
            BLL.BLLInstance.ShowMSG(Msg);
        }

        public void GetShiftMaster()
        {
            _dt = new DAL().GetShiftMaster(-1, "", -1, true);
            if (_dt != null && _dt.Rows.Count > 0)
            {
                _dt = new BLL().GetSerialNo(ref _dt, "SrNo");
                gvShiftMaster.DataSource = _dt;
            }
            else
            {
                gvShiftMaster.DataSource = null;
                new Campus().MessageBox("No Record Found !", Page);
            }
            gvShiftMaster.DataBind();
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            Sql = "I";
            Validation();
        }

        protected void lbtnEdit_Click(object sender, EventArgs e)
        {
            GetDesignation(ddlDesignation0);

            var row = (GridViewRow)((LinkButton)sender).Parent.Parent;
            var i = row.RowIndex;
            txtShiftName0.Text = gvShiftMaster.Rows[i].Cells[1].Text.Trim();

            ddlFromHour0.SelectedValue = ((Label)gvShiftMaster.Rows[i].FindControl("FromHour")).Text;
            ddlFromMinute0.SelectedValue = ((Label)gvShiftMaster.Rows[i].FindControl("FromMinute")).Text;
            ddlFromSecond0.SelectedValue = ((Label)gvShiftMaster.Rows[i].FindControl("FromSecond")).Text;
            ddlFromType0.SelectedValue = ((Label)gvShiftMaster.Rows[i].FindControl("FromType")).Text;
            ddlToType0.SelectedValue = ((Label)gvShiftMaster.Rows[i].FindControl("ToType")).Text;
            ddlToHour0.SelectedValue = ((Label)gvShiftMaster.Rows[i].FindControl("ToHour")).Text;
            ddlToMinute0.SelectedValue = ((Label)gvShiftMaster.Rows[i].FindControl("ToMinute")).Text;
            ddlToSecond0.SelectedValue = ((Label)gvShiftMaster.Rows[i].FindControl("ToSecond")).Text;

            //ddlLunchFromH0.SelectedValue = ((Label)gvShiftMaster.Rows[i].FindControl("lblLunchFromH")).Text;
            //ddlLunchFromM0.SelectedValue = ((Label)gvShiftMaster.Rows[i].FindControl("lblLunchFromM")).Text;
            //ddlLunchFromT0.SelectedValue = ((Label)gvShiftMaster.Rows[i].FindControl("lblLunchFromT")).Text;

            //ddlLunchToH0.SelectedValue = ((Label)gvShiftMaster.Rows[i].FindControl("lblLunchToH")).Text;
            //ddlLunchToM0.SelectedValue = ((Label)gvShiftMaster.Rows[i].FindControl("lblLunchToM")).Text;
            //ddlLunchToT0.SelectedValue = ((Label)gvShiftMaster.Rows[i].FindControl("lblLunchToT")).Text;

            ddlGraceTimeH0.SelectedValue = ((Label)gvShiftMaster.Rows[i].FindControl("lblGraceTimeH")).Text;
            ddlGraceTimeM0.SelectedValue = ((Label)gvShiftMaster.Rows[i].FindControl("lblGraceTimeM")).Text;
            ddlGraceTimeS0.SelectedValue = ((Label)gvShiftMaster.Rows[i].FindControl("lblGraceTimeS")).Text;
            try
            {
                ddlGraceTimeOutH0.SelectedValue = ((Label)gvShiftMaster.Rows[i].FindControl("lblGraceTimeOutH")).Text;
                ddlGraceTimeOutM0.SelectedValue = ((Label)gvShiftMaster.Rows[i].FindControl("lblGraceTimeOutM")).Text;
                ddlGraceTimeOutS0.SelectedValue = ((Label)gvShiftMaster.Rows[i].FindControl("lblGraceTimeOutS")).Text;

                ddlSLTimeH0.SelectedValue = ((Label)gvShiftMaster.Rows[i].FindControl("lblSLTimeH")).Text;
                ddlSLTimeM0.SelectedValue = ((Label)gvShiftMaster.Rows[i].FindControl("lblSLTimeM")).Text;
                ddlSLTimeS0.SelectedValue = ((Label)gvShiftMaster.Rows[i].FindControl("lblSLTimeS")).Text;
                ddlSLTimeT0.SelectedValue = ((Label)gvShiftMaster.Rows[i].FindControl("lblSLTimeT")).Text;

                ddlSLTimeHO0.SelectedValue = ((Label)gvShiftMaster.Rows[i].FindControl("lblSLTimeHO")).Text;
                ddlSLTimeMO0.SelectedValue = ((Label)gvShiftMaster.Rows[i].FindControl("lblSLTimeMO")).Text;
                ddlSLTimeSO0.SelectedValue = ((Label)gvShiftMaster.Rows[i].FindControl("lblSLTimeSO")).Text;
                ddlSLTimeTO0.SelectedValue = ((Label)gvShiftMaster.Rows[i].FindControl("lblSLTimeTO")).Text;

                ddlHDTimeH0.SelectedValue = ((Label)gvShiftMaster.Rows[i].FindControl("lblHDTimeH")).Text;
                ddlHDTimeM0.SelectedValue = ((Label)gvShiftMaster.Rows[i].FindControl("lblHDTimeM")).Text;
                ddlHDTimeS0.SelectedValue = ((Label)gvShiftMaster.Rows[i].FindControl("lblHDTimeS")).Text;
                ddlHDTimeT0.SelectedValue = ((Label)gvShiftMaster.Rows[i].FindControl("lblHDTimeT")).Text;
            }
            catch
            {
                // ignored
            }


            A02Id = Convert.ToInt16(((Label)gvShiftMaster.Rows[i].FindControl("A02ID")).Text);
            ddlDesignation0.SelectedValue = ((Label)gvShiftMaster.Rows[i].FindControl("lblDesID")).Text;

            ShiftName = txtShiftName0.Text;
            txtShiftTime0.Text = gvShiftMaster.Rows[i].Cells[2].Text.Trim();
            Panel1_ModalPopupExtender.Show();
        }

        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            var chk = (LinkButton)sender;
            var lblId = (Label)chk.NamingContainer.FindControl("Label37");

            A02Id = Convert.ToInt16(lblId.Text);
            var row = (GridViewRow)((LinkButton)sender).Parent.Parent;
            var i = row.RowIndex;
            ShiftName = gvShiftMaster.Rows[i].Cells[1].Text.Trim();
            mpeDelete.Show();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            Sql = "U";
            Validation0();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        protected void btnYes_Click(object sender, EventArgs e)
        {
            DeleteRecord();
        }

        public void DeleteRecord()
        {
            new DAL().DeleteRecord("UPDATE A02_EmpShiftMaster SET IsDelete=1 WHERE A02ID=" + A02Id + " and BranchCode=" + Session["BranchCode"].ToString() + "");
            BLL.BLLInstance.ShowMSG("D");
            GetShiftMaster();
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {

        }

        protected void ddlFromHour_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetTimeDiff();
        }

        protected void ddlFromMinute_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetTimeDiff();
        }
        protected void ddlFromSecond_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetTimeDiff();
        }
        protected void ddlFromType_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetTimeDiff();
        }
        protected void ddlToHour_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetTimeDiff();
        }
        protected void ddlToMinute_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetTimeDiff();
        }
        protected void ddlToSecond_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetTimeDiff();
        }
        protected void ddlToType_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetTimeDiff();
        }

        protected void ddlFromHour0_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetTimeDiff0();
            Panel1_ModalPopupExtender.Show();
        }

        protected void ddlFromMinute0_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetTimeDiff0();
            Panel1_ModalPopupExtender.Show();
        }

        protected void ddlFromSecond0_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetTimeDiff0();
            Panel1_ModalPopupExtender.Show();
        }

        protected void ddlFromType0_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetTimeDiff0();
            Panel1_ModalPopupExtender.Show();
        }

        protected void ddlToHour0_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetTimeDiff0();
            Panel1_ModalPopupExtender.Show();
        }

        protected void ddlToMinute0_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetTimeDiff0();
            Panel1_ModalPopupExtender.Show();
        }

        protected void ddlToSecond0_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetTimeDiff0();
            Panel1_ModalPopupExtender.Show();
        }

        protected void ddlToType0_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetTimeDiff0();
            Panel1_ModalPopupExtender.Show();
        }

        public void GetTimeDiff()
        {
            Msg = "";
            txtShiftTime.Text = "";
            if (ddlFromHour.SelectedIndex > 0)
            {
                if (ddlFromMinute.SelectedIndex > 0 && ddlFromSecond.SelectedIndex > 0)
                {
                    if (ddlToHour.SelectedIndex > 0)
                    {
                        if (ddlToMinute.SelectedIndex > 0 && ddlToSecond.SelectedIndex > 0)
                        {
                            Msg = new DAL().GetTimeDifference(ddlFromHour.SelectedValue + ':' + ddlFromMinute.SelectedValue + ':' + ddlFromSecond.SelectedValue + ' ' + ddlFromType.SelectedItem, ddlToHour.SelectedValue + ':' + ddlToMinute.SelectedValue + ':' + ddlToSecond.SelectedValue + ' ' + ddlToType.SelectedItem);
                            if (Msg != string.Empty)
                            {
                                txtShiftTime.Text = Msg;
                            }
                            else
                            {
                                Msg = "Select Valid Time !";
                                BLL.BLLInstance.ShowMSG(Msg);
                            }
                        }
                    }
                }
            }
        }

        public void GetTimeDiff0()
        {
            Msg = "";
            txtShiftTime0.Text = "";
            if (ddlFromHour0.SelectedIndex > 0)
            {
                if (ddlFromMinute0.SelectedIndex > 0)
                {
                    if (ddlToHour0.SelectedIndex > 0)
                    {
                        if (ddlToMinute0.SelectedIndex > 0)
                        {
                            Msg = new DAL().GetTimeDifference(ddlFromHour0.SelectedValue + ':' + ddlFromMinute0.SelectedValue + ':' + ddlFromSecond0.SelectedValue + ' ' + ddlFromType0.SelectedValue, ddlToHour0.SelectedValue + ':' + ddlToMinute0.SelectedValue + ':' + ddlToSecond0.SelectedValue + ' ' + ddlToType0.SelectedValue);
                            if (Msg != string.Empty)
                            {
                                txtShiftTime0.Text = Msg;
                            }
                            else
                            {
                                Msg = "Select Valid Time !";
                                BLL.BLLInstance.ShowMSG(Msg);
                            }
                        }
                    }
                }
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {

        }
        //protected void ddlLunchFromH_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    GetTimeDiff();
        //}
        //protected void ddlLunchFromM_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    GetTimeDiff();
        //}
        //protected void ddlLunchFromT_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    GetTimeDiff();
        //}
        //protected void ddlLunchToH_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    GetTimeDiff();
        //}
        //protected void ddlLunchToM_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    GetTimeDiff();
        //}
        //protected void ddlLunchToT_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    GetTimeDiff();
        //}

        //protected void ddlLunchFromH0_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    GetTimeDiff0();
        //    Panel1_ModalPopupExtender.Show();
        //}
        //protected void ddlLunchFromM0_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    GetTimeDiff0();
        //    Panel1_ModalPopupExtender.Show();
        //}
        //protected void ddlLunchFromT0_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    GetTimeDiff0();
        //    Panel1_ModalPopupExtender.Show();
        //}
        //protected void ddlLunchToH0_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    GetTimeDiff0();
        //    Panel1_ModalPopupExtender.Show();
        //}
        //protected void ddlLunchToM0_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    GetTimeDiff0();
        //    Panel1_ModalPopupExtender.Show();
        //}
        //protected void ddlLunchToT0_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    GetTimeDiff0();
        //    Panel1_ModalPopupExtender.Show();
        //}
    }
}