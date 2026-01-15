using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace _1
{
    public partial class ShiftForStudent : System.Web.UI.Page
    {
        DataTable _dt;
        private static int _id;
        public static string Msg, Sql, ShiftName = String.Empty;
        private readonly Campus _oo;
        public ShiftForStudent()
        {
            _dt = new DataTable();
            _oo = new Campus();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Campus camp = new Campus(); camp.LoadLoader(loader);
            if (!IsPostBack)
            {
                GetTime();
                GetShiftMaster();
            }
        }

        public void GetTime()
        {
            BLL.FillHourInDropDownlist(ddlFromHour);
            BLL.FillMinuteInDropDownlist(ddlFromMinute);
            ddlFromMinute.SelectedIndex = 1;

            BLL.FillHourInDropDownlist(DropDownList1);
            BLL.FillMinuteInDropDownlist(DropDownList2);

            BLL.FillHourInDropDownlist(ddlToHour);
            BLL.FillMinuteInDropDownlist(ddlToMinute);
            ddlToMinute.SelectedIndex = 1;

            BLL.FillHourInDropDownlist(DropDownList4);
            BLL.FillMinuteInDropDownlist(DropDownList5);

            BLL.FillHourInDropDownlist(ddlGraceTimeInH);
            ddlGraceTimeInH.SelectedIndex = 1;
            BLL.FillMinuteInDropDownlist(ddlGraceTimeInM);
            ddlGraceTimeInM.SelectedIndex = 1;

            BLL.FillHourInDropDownlist(DropDownList7);
            BLL.FillMinuteInDropDownlist(DropDownList8);

            BLL.FillHourInDropDownlist(ddlAbsentOnH);
            BLL.FillMinuteInDropDownlist(ddlAbsentOnM);
            ddlAbsentOnM.SelectedIndex = 1;

            BLL.FillHourInDropDownlist(DropDownList9);
            BLL.FillMinuteInDropDownlist(DropDownList10);

            BLL.FillHourInDropDownlist(ddlSendSMSInH);
            BLL.FillMinuteInDropDownlist(ddlSendSMSInM);
            ddlSendSMSInM.SelectedIndex = 1;

            BLL.FillHourInDropDownlist(DropDownList12);
            BLL.FillMinuteInDropDownlist(DropDownList13);

            BLL.FillHourInDropDownlist(ddlSendSMSOutH);
            BLL.FillMinuteInDropDownlist(ddlSendSMSOutM);
            ddlSendSMSOutM.SelectedIndex = 1;

            BLL.FillHourInDropDownlist(DropDownList15);
            BLL.FillMinuteInDropDownlist(DropDownList16);
            DropDownList16.SelectedIndex = 1;


            ddlToType.SelectedIndex = 1;

        }

        protected void ddlFromHour_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetTimeDiff();
        }

        protected void ddlFromMinute_SelectedIndexChanged(object sender, EventArgs e)
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
        protected void ddlToType_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetTimeDiff();
        }

        public void GetTimeDiff()
        {
            Msg = "";
            txtShiftTime.Text = "";
            if (ddlFromHour.SelectedIndex > 0)
            {
                if (ddlFromMinute.SelectedIndex > 0)
                {
                    if (ddlToHour.SelectedIndex > 0)
                    {
                        if (ddlToMinute.SelectedIndex > 0)
                        {
                            Msg = new DAL().GetTimeDifference(ddlFromHour.SelectedValue + ':' + ddlFromMinute.SelectedValue + ' ' + ddlFromType.SelectedItem, ddlToHour.SelectedValue + ':' + ddlToMinute.SelectedValue + ' ' + ddlToType.SelectedItem);
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
        protected void btnInsert_Click(object sender, EventArgs e)
        {
            chkSendSMSOut.Checked = false;
            ddlSendSMSOutH.SelectedIndex = 0;
            ddlSendSMSOutM.SelectedIndex = 0;
            if (chkSendSMSIn.Checked && (ddlSendSMSInH.SelectedIndex == 0 || ddlSendSMSInM.SelectedIndex == 0))
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Sorry, Please select Send SMS In time!", "A");
                return;
            }
            if (chkSendSMSOut.Checked && (ddlSendSMSOutH.SelectedIndex == 0 || ddlSendSMSOutM.SelectedIndex == 0))
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Sorry, Please select Send SMS Out time!", "A");
                return;
            }
            int GraceMints = 0;
            if (ddlGraceTimeInH.SelectedIndex > 1)
            {
                GraceMints = int.Parse(ddlGraceTimeInH.SelectedValue) * 60;
            }
            if (ddlGraceTimeInM.SelectedIndex > 1)
            {
                GraceMints += int.Parse(ddlGraceTimeInM.SelectedValue);
            }
            string ShiftName = txtShiftName.Text.Trim();
            string ShiftIn = ddlFromHour.SelectedValue + ":" + ddlFromMinute.SelectedValue + " " + ddlFromType.SelectedValue;
            string ShiftOut = ddlToHour.SelectedValue + ":" + ddlToMinute.SelectedValue + " " + ddlToType.SelectedValue;
            string ShiftDuration = txtShiftTime.Text.Trim();
            string GraceIn = GraceMints.ToString();
            string AbsenOn1 = ddlAbsentOnH.SelectedValue + ":" + ddlAbsentOnM.SelectedValue + " " + ddlAbsentOnType.SelectedValue;
            string SendSMSIn = ddlSendSMSInH.SelectedValue + ":" + ddlSendSMSInM.SelectedValue + " " + ddlSendSMSInType.SelectedValue;
            string SendSMSOut = ddlSendSMSOutH.SelectedValue + ":" + ddlSendSMSOutM.SelectedValue + " " + ddlSendSMSOutType.SelectedValue;

            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@QueryFor", "I"));
            param.Add(new SqlParameter("@ShiftName", ShiftName));
            param.Add(new SqlParameter("@ShiftIn", ShiftIn));
            param.Add(new SqlParameter("@ShiftOut", ShiftOut));
            param.Add(new SqlParameter("@ShiftDuration", ShiftDuration));
            param.Add(new SqlParameter("@GraceIn", GraceIn));
            param.Add(new SqlParameter("@AbsenOn", AbsenOn1));
            param.Add(new SqlParameter("@SendSMSIn", SendSMSIn));
            param.Add(new SqlParameter("@EnableIn", chkSendSMSIn.Checked ? "Yes" : "No"));
            param.Add(new SqlParameter("@SendSMSOut", SendSMSOut));
            param.Add(new SqlParameter("@EnableOut", chkSendSMSOut.Checked ? "Yes" : "No"));
            param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
            param.Add(new SqlParameter("@LoginName", Session["LoginName"].ToString()));
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
            SqlParameter para = new SqlParameter("@MSG", "");
            para.Direction = ParameterDirection.Output;
            para.Size = 0x100;
            param.Add(para);

            Msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("USP_StudentShiftMaster", param);
            if (Msg == "S")
            {
                GetShiftMaster();
                Reset();
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Submitted successfully.", "S");
            }
            else
            {
                if (Msg == "D")
                {
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Sorry, Duplicate Record found!", "A");
                }
                else
                {
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Sorry, Record not  submitted successfully!", "W");
                }
            }

        }

        public void GetShiftMaster()
        {
            DataSet ds = new DataSet();
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@QueryFor", "S"));
            param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
            ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("USP_StudentShiftMaster", param);
            if (ds.Tables.Count > 0)
            {
                gvShiftMaster.DataSource = ds;
                gvShiftMaster.DataBind();
            }
        }

        public void Reset()
        {
            ddlFromHour.SelectedIndex = 0;
            ddlFromMinute.SelectedIndex = 1;
            ddlToHour.SelectedIndex = 0;
            ddlToMinute.SelectedIndex = 1;
            txtShiftName.Text = "";
            txtShiftTime.Text = "";
            ddlGraceTimeInH.SelectedIndex = 0;
            ddlGraceTimeInM.SelectedIndex = 1;
            ddlAbsentOnH.SelectedIndex = 0;
            ddlAbsentOnM.SelectedIndex = 1;
            ddlSendSMSInH.SelectedIndex = 0;
            ddlSendSMSInM.SelectedIndex = 1;
            ddlSendSMSOutH.SelectedIndex = 0;
            ddlSendSMSOutM.SelectedIndex = 1;
            chkSendSMSIn.Checked = false;
            chkSendSMSOut.Checked = false;
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Reset();
        }


        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            Label lblId = (Label)lnk.NamingContainer.FindControl("lblShiftId");

            lblIdD.Text = lblId.Text.Trim();
            Panel2_ModalPopupExtender.Show();
        }

        protected void lnkDeleteYes_Click(object sender, EventArgs e)
        {

            Sql = "D";
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@QueryFor", "D"));
            param.Add(new SqlParameter("@id", lblIdD.Text));
            param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
            SqlParameter para = new SqlParameter("@MSG", "");
            para.Direction = ParameterDirection.Output;
            para.Size = 0x100;
            param.Add(para);

            Msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("USP_StudentShiftMaster", param);
            if (Msg == "S")
            {
                GetShiftMaster();
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Deleted successfully.", "S");
            }
            else
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Sorry, Record not  deleted successfully!", "W");
            }
        }

        public override void Dispose()
        {
            _dt.Dispose();
        }

        protected void chkSendSMSIn_CheckedChanged(object sender, EventArgs e)
        {
            ddlSendSMSInH.SelectedIndex = 0;
            ddlSendSMSInM.SelectedIndex = 0;
            ddlSendSMSInType.SelectedIndex = 0;
            if (chkSendSMSIn.Checked)
            {
                ddlSendSMSInH.Enabled = true;
                ddlSendSMSInM.Enabled = true;
                ddlSendSMSInType.Enabled = true;
            }
            else
            {
                ddlSendSMSInH.Enabled = false;
                ddlSendSMSInM.Enabled = false;
                ddlSendSMSInType.Enabled = false;
            }
        }

        protected void chkSendSMSOut_CheckedChanged(object sender, EventArgs e)
        {
            ddlSendSMSOutH.SelectedIndex = 0;
            ddlSendSMSOutM.SelectedIndex = 0;
            ddlSendSMSOutType.SelectedIndex = 0;
            if (chkSendSMSOut.Checked)
            {
                ddlSendSMSOutH.Enabled = true;
                ddlSendSMSOutM.Enabled = true;
                ddlSendSMSOutType.Enabled = true;
            }
            else
            {
                ddlSendSMSOutH.Enabled = false;
                ddlSendSMSOutM.Enabled = false;
                ddlSendSMSOutType.Enabled = false;
            }
        }

        protected void ddlAbsentOnH_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlSendSMSInH.Items.Clear();
            for (int i = 0; i < Convert.ToInt16(ddlAbsentOnH.SelectedValue); i++)
            {
                if (i < 10)
                    ddlSendSMSInH.Items.Add("0" + i.ToString());
                else
                    ddlSendSMSInH.Items.Add(i.ToString());
            }
            ddlSendSMSInType.Items.Clear();
            if (ddlAbsentOnType.SelectedValue == "AM")
                ddlSendSMSInType.Items.Add("AM");
            else
            {
                ddlSendSMSInType.Items.Add("AM");
                ddlSendSMSInType.Items.Add("PM");
            }
        }

        protected void ddlAbsentOnType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlSendSMSInType.Items.Clear();
            for (int i = 0; i < Convert.ToInt16(ddlAbsentOnH.SelectedValue); i++)
            {
                if (ddlAbsentOnType.SelectedValue == "AM")
                    ddlSendSMSInType.Items.Add("AM");
                else
                {
                    ddlSendSMSInType.Items.Add("AM");
                    ddlSendSMSInType.Items.Add("PM");
                }
            }
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            LinkButton chk = (LinkButton)sender;
            Label lblId = (Label)chk.NamingContainer.FindControl("Label36");
            string ss = lblId.Text;
            hfShiftID.Value = ss;


            // sql = "Select  distinct ROW_NUMBER() OVER (ORDER BY p.ProductId ASC) AS  [ProductId] ,po.id as ID, pc.ProductCategoryName as[ProductCategoryName], p.ProductName as [ProductName], Pm.ProductTypeName as [ProductTypeName],PO.ProductModelName as [ProductModelName] from Productcategorymaster pc left join ProductName p on p.ProductId=pc.ProductId  left join ProductTypeMaster PM on p.ProductId=PM.ProductId left join ProductModelMaster PO on p.ProductId=PO.ProductId ";        
            string _sql = "Select ShiftName,Left(ShiftIn,2) ShiftIn_HH,Substring(ShiftIn,4,3) ShiftIn_MM,Right(ShiftIn,2) ShiftIn_TT,\r\nLeft(ShiftOut,2) ShiftOut_HH,Substring(ShiftOut,4,3) ShiftOut_MM,Right(ShiftOut,2) ShiftOut_TT,\r\nShiftDuration,(GraceIn/60) GraceIn_HH,(GraceIn%60) GraceIn_MM,\r\nLeft(AbsenOn,2) AbsenOn_HH,Substring(AbsenOn,4,3) AbsenOn_MM,Right(AbsenOn,2) AbsenOn_TT,\r\nLeft(SendSMSIn,2) SendSMSIn_HH,Substring(SendSMSIn,4,3) SendSMSIn_MM,Right(SendSMSIn,2) SendSMSIn_TT,\r\nEnableIn,Left(SendSMSOut,2) SendSMSOut_HH,Substring(SendSMSOut,4,3) SendSMSOut_MM,Right(SendSMSOut,2) SendSMSOut_TT,EnableOut from StudentShiftMaster\r\n where BranchCode=" + Session["BranchCode"] + "";
            _sql += " and id=" + ss;
            TextBox1.Text = _oo.ReturnTag(_sql, "ShiftName");
            DropDownList1.SelectedValue = _oo.ReturnTag(_sql, "ShiftIn_HH").Trim();
            DropDownList2.SelectedValue = _oo.ReturnTag(_sql, "ShiftIn_MM").Trim();
            DropDownList3.SelectedValue = _oo.ReturnTag(_sql, "ShiftIn_TT").Trim();

            DropDownList4.SelectedValue = _oo.ReturnTag(_sql, "ShiftOut_HH").Trim();
            DropDownList5.SelectedValue = _oo.ReturnTag(_sql, "ShiftOut_MM").Trim();
            DropDownList6.SelectedValue = _oo.ReturnTag(_sql, "ShiftOut_TT").Trim();

            TextBox2.Text = _oo.ReturnTag(_sql, "ShiftDuration").Trim();

            DropDownList7.SelectedValue = _oo.ReturnTag(_sql, "GraceIn_HH").Trim() == "0" ? "00" : _oo.ReturnTag(_sql, "GraceIn_HH").Length < 2 ? "0" + _oo.ReturnTag(_sql, "GraceIn_HH").Trim(): _oo.ReturnTag(_sql, "GraceIn_HH").Trim();
            DropDownList8.SelectedValue = _oo.ReturnTag(_sql, "GraceIn_MM").Trim() == "0" ? "00" : _oo.ReturnTag(_sql, "GraceIn_MM").Length < 2 ? "0" + _oo.ReturnTag(_sql, "GraceIn_MM").Trim() : _oo.ReturnTag(_sql, "GraceIn_MM").Trim();

            DropDownList9.SelectedValue = _oo.ReturnTag(_sql, "AbsenOn_HH").Trim();
            DropDownList10.SelectedValue = _oo.ReturnTag(_sql, "AbsenOn_MM").Trim();
            DropDownList11.SelectedValue = _oo.ReturnTag(_sql, "AbsenOn_TT").Trim();

            CheckBox1.Checked= _oo.ReturnTag(_sql, "EnableIn") == "Yes".Trim();

            if (CheckBox1.Checked)
            {
                DropDownList12.Enabled = true;
                DropDownList13.Enabled = true;
                DropDownList14.Enabled = true;
            }

            DropDownList12.SelectedValue = _oo.ReturnTag(_sql, "SendSMSIn_HH").Trim();
            DropDownList13.SelectedValue = _oo.ReturnTag(_sql, "SendSMSIn_MM").Trim();
            DropDownList14.SelectedValue = _oo.ReturnTag(_sql, "SendSMSIn_TT").Trim();

            CheckBox2.Checked = _oo.ReturnTag(_sql, "EnableOut") == "Yes".Trim();

            if (CheckBox1.Checked)
            {
                DropDownList15.Enabled = true;
                DropDownList16.Enabled = true;
                DropDownList17.Enabled = true;
            }

            DropDownList15.SelectedValue = _oo.ReturnTag(_sql, "SendSMSOut_HH").Trim();
            DropDownList16.SelectedValue = _oo.ReturnTag(_sql, "SendSMSOut_MM").Trim();
            DropDownList17.SelectedValue = _oo.ReturnTag(_sql, "SendSMSOut_TT").Trim();


            Panel1_ModalPopupExtender.Show();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        protected void Button2_Click(object sender, EventArgs e)
        {

        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetTimeDiff_Edit();
        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetTimeDiff_Edit();
        }

        protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetTimeDiff_Edit();
        }
        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            DropDownList12.SelectedIndex = 0;
            DropDownList13.SelectedIndex = 0;
            DropDownList14.SelectedIndex = 0;
            if (CheckBox1.Checked)
            {
                DropDownList12.Enabled = true;
                DropDownList13.Enabled = true;
                DropDownList14.Enabled = true;
            }
            else
            {
                DropDownList12.Enabled = false;
                DropDownList13.Enabled = false;
                DropDownList14.Enabled = false;
            }
            GetTimeDiff_Edit();
        }

        protected void DropDownList9_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetTimeDiff_Edit();
        }

        protected void DropDownList11_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetTimeDiff_Edit();
        }

        protected void DropDownList4_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetTimeDiff_Edit();
        }

        protected void DropDownList5_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetTimeDiff_Edit();
        }

        protected void DropDownList6_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetTimeDiff_Edit();
        }

        public void GetTimeDiff_Edit()
        {
            Msg = "";
            TextBox2.Text = "";
            if (DropDownList1.SelectedIndex > 0)
            {
                if (DropDownList2.SelectedIndex > 0)
                {
                    if (DropDownList4.SelectedIndex > 0)
                    {
                        if (DropDownList5.SelectedIndex > 0)
                        {
                            Msg = new DAL().GetTimeDifference(DropDownList1.SelectedValue + ':' + DropDownList2.SelectedValue + ' ' + DropDownList3.SelectedItem, DropDownList4.SelectedValue + ':' + DropDownList5.SelectedValue + ' ' + DropDownList6.SelectedItem);
                            if (Msg != string.Empty)
                            {
                                TextBox2.Text = Msg;
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

            Panel1_ModalPopupExtender.Show();
        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            CheckBox2.Checked = false;
            ddlSendSMSOutH.SelectedIndex = 0;
            ddlSendSMSOutM.SelectedIndex = 0;
            if (CheckBox1.Checked && (DropDownList12.SelectedIndex == 0 || DropDownList13.SelectedIndex == 0))
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Sorry, Please select Send SMS In time!", "A");
                return;
            }
            if (CheckBox2.Checked && (DropDownList15.SelectedIndex == 0 || DropDownList16.SelectedIndex == 0))
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Sorry, Please select Send SMS Out time!", "A");
                return;
            }
            int GraceMints = 0;
            if (DropDownList7.SelectedIndex > 1)
            {
                GraceMints = int.Parse(DropDownList7.SelectedValue) * 60;
            }
            if (DropDownList8.SelectedIndex > 1)
            {
                GraceMints += int.Parse(DropDownList8.SelectedValue);
            }
            string ShiftName = TextBox1.Text.Trim();
            string ShiftIn = DropDownList1.SelectedValue + ":" + DropDownList2.SelectedValue + " " + DropDownList3.SelectedValue;
            string ShiftOut = DropDownList4.SelectedValue + ":" + DropDownList5.SelectedValue + " " + DropDownList6.SelectedValue;
            string ShiftDuration = TextBox2.Text.Trim();
            string GraceIn = GraceMints.ToString();
            string AbsenOn1 = DropDownList9.SelectedValue + ":" + DropDownList10.SelectedValue + " " + DropDownList11.SelectedValue;
            string SendSMSIn = DropDownList12.SelectedValue + ":" + DropDownList13.SelectedValue + " " + DropDownList14.SelectedValue;
            string SendSMSOut = DropDownList15.SelectedValue + ":" + DropDownList16.SelectedValue + " " + DropDownList17.SelectedValue;

            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@QueryFor", "U"));
            param.Add(new SqlParameter("@ID", hfShiftID.Value));
            param.Add(new SqlParameter("@ShiftName", ShiftName));
            param.Add(new SqlParameter("@ShiftIn", ShiftIn));
            param.Add(new SqlParameter("@ShiftOut", ShiftOut));
            param.Add(new SqlParameter("@ShiftDuration", ShiftDuration));
            param.Add(new SqlParameter("@GraceIn", GraceIn));
            param.Add(new SqlParameter("@AbsenOn", AbsenOn1));
            param.Add(new SqlParameter("@SendSMSIn", SendSMSIn));
            param.Add(new SqlParameter("@EnableIn", CheckBox1.Checked ? "Yes" : "No"));
            param.Add(new SqlParameter("@SendSMSOut", SendSMSOut));
            param.Add(new SqlParameter("@EnableOut", CheckBox2.Checked ? "Yes" : "No"));
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
            SqlParameter para = new SqlParameter("@MSG", "")
            {
                Direction = ParameterDirection.Output,
                Size = 0x100
            };
            param.Add(para);

            Msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("USP_StudentShiftMaster", param);
            if (Msg == "U")
            {
                GetShiftMaster();
                Reset();
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Updated successfully.", "S");
            }
            else
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Sorry, Record not  submitted successfully!", "W");
            }
        }

    }
}