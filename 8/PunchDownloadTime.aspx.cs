using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace _8
{
    public partial class PunchDownloadTime : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file
            if (IsPostBack) return;
            GetTime();
            GetPunchDownloadTime();
            GetDailyPunchDownloadTime();
        }

        private void GetTime()
        {
            BLL.FillHourInDropDownlist(ddlFromHour);
            BLL.FillMinuteInDropDownlist(ddlFromMinute);

            BLL.FillHourInDropDownlist(ddlIntervalHour);
            BLL.FillMinuteInDropDownlist(ddlIntervalMinute);
        }

        protected void GetDailyPunchDownloadTime()
        {
            var param = new List<SqlParameter>
            {
                (new SqlParameter("@QueryFor", "S"))
            };
            DataSet ds;
            if (rbTimeFor.SelectedIndex == 0)
            {
                ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("USP_PunchDownloadTimeforStudent", param);
            }
            else
            {
                ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("USP_PunchDownloadTime", param);
            }
          
            grdDailyPunchDownloadTime.DataSource = ds.Tables[1];
            grdDailyPunchDownloadTime.DataBind();
        }

        protected void lnkSubmit_OnClick(object sender, EventArgs e)
        {
            try
            {
                SetPunchDownloadTime();
            }
            catch (Exception exception)
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, exception.Message, "A");
            }
        }

        protected void lnkClear_OnClick(object sender, EventArgs e)
        {
            try
            {
                ClearPunchDownloadTime();
                GetPunchDownloadTime();
                GetDailyPunchDownloadTime();
            }
            catch (Exception exception)
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, exception.Message, "A");
            }
        }

        private void SetPunchDownloadTime()
        {
            var param = new List<SqlParameter>
            {
                (new SqlParameter("@HH", ddlFromHour.SelectedItem.Text.Trim())),
                (new SqlParameter("@MM", ddlFromMinute.SelectedItem.Text.Trim())),
                (new SqlParameter("@TT", ddlFromType.SelectedItem.Text.Trim())),
                (new SqlParameter("@TimeIntervalHH", ddlIntervalHour.SelectedValue.Trim())),
                (new SqlParameter("@TimeIntervalMM", ddlIntervalMinute.SelectedValue.Trim()))
            };

            var para = new SqlParameter("@Msg", "");
            {
                para.Direction = ParameterDirection.Output;
            }

            param.Add(para);
            var msg = "";
            if (rbTimeFor.SelectedIndex==0)
            {
                msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("USP_PunchDownloadTimeforStudent", param);
            }
            else
            {
                msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("USP_PunchDownloadTime", param);
            }
            if (msg == "S")
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Submitted successfully.", "S");
                GetPunchDownloadTime();
                GetDailyPunchDownloadTime();
            }
            else
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Record not Submitted!", "A");
            }
        }

        private void GetPunchDownloadTime()
        {
            var param = new List<SqlParameter>
            {
                (new SqlParameter("@QueryFor", "S"))
            };

            DataSet ds;
            if (rbTimeFor.SelectedIndex == 0)
            {
                ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("USP_PunchDownloadTimeforStudent", param);
            }
            else
            {
                ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("USP_PunchDownloadTime", param);
            }
            
            // ReSharper disable once UseNullPropagation
            if (ds == null) return;
            if (ds.Tables[0].Rows.Count <= 0) return;
            ddlFromHour.SelectedValue = ds.Tables[0].Rows[0][0].ToString();
            ddlFromMinute.SelectedValue = ds.Tables[0].Rows[0][1].ToString();
            ddlFromType.SelectedValue = ds.Tables[0].Rows[0][2].ToString();

            ddlIntervalHour.SelectedValue = ds.Tables[0].Rows[0][3].ToString();
            ddlIntervalMinute.SelectedValue = ds.Tables[0].Rows[0][4].ToString();
        }

        private void ClearPunchDownloadTime()
        {
            var param = new List<SqlParameter>
            {
                (new SqlParameter("@QueryFor", "D"))
            };

            var para = new SqlParameter("@Msg", "");
            {
                para.Direction = ParameterDirection.Output;
            }

            param.Add(para);

            var msg = "";
            if (rbTimeFor.SelectedIndex == 0)
            {
                msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("USP_PunchDownloadTimeforStudent", param);
            }
            else
            {
                msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("USP_PunchDownloadTime", param);
            }
            if (msg == "S")
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Submitted successfully.", "S");
                GetDailyPunchDownloadTime();
                ddlFromHour.SelectedIndex = 0;
                ddlFromMinute.SelectedIndex = 0;
                ddlFromType.SelectedIndex = 0;

                ddlIntervalHour.SelectedIndex = 0;
                ddlIntervalMinute.SelectedIndex = 0;
            }
            else
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Record not Submitted!", "A");
            }
        }

        protected void rbTimeFor_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetDailyPunchDownloadTime();
        }
    }
}