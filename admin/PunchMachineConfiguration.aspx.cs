using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace admin
{
    public partial class PunchMachineConfiguration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file
            if (IsPostBack) return;
            Get_PunchMachineConfiguration();
            txtIpAddress.Focus();
        }

        public void Get_PunchMachineConfiguration()
        {
            var param = new List<SqlParameter>
            {
                     new SqlParameter("@DeviceType", ""),
                     new SqlParameter("@BranchCode",Session["BranchCode"]),
                     new SqlParameter("@QueryFor","Select")
            };

            grdPunchMachineConfiguration.DataSource = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("Set_Get_PunchMachineConfiguration", param);
            grdPunchMachineConfiguration.DataBind();
        }

        public void Set_PunchMachineConfiguration()
        {
            var param = new List<SqlParameter>
            {
                new SqlParameter("@IpAddress",txtIpAddress.Text.Trim()),
                new SqlParameter("@MachineNo",txtMachineNo.Text.Trim()),
                new SqlParameter("@PortNo", txtPortNo.Text.Trim()),
                new SqlParameter("@Username", txtUsername.Text.Trim()),
                new SqlParameter("@Password",txtPassword.Text.Trim()==string.Empty?"0":txtPassword.Text.Trim()),
                new SqlParameter("@ServerPortNo",txtServerPortNo.Text.Trim()),
                new SqlParameter("@DeviceType",drpDevice.SelectedValue.ToString()),
                new SqlParameter("@ConnMode",rbList.SelectedValue.ToString()),
                new SqlParameter("@IsPush",cbPush.Checked),
                new SqlParameter("@DeviceFor",rdoBioMatricFor.SelectedValue),
                new SqlParameter("@BranchCode",Session["BranchCode"]),
                new SqlParameter("@QueryFor","I")

            };

            var para = new SqlParameter("@Msg", "")
            {
                Direction = ParameterDirection.Output,
                Size = 0x100
            };

            param.Add(para);

            var msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("Set_Get_PunchMachineConfiguration", param);
            Campus camp = new Campus();
            switch (msg)
            {
                case "S":
                     camp.msgbox(Page, msgbox, "Submitted successfully.", "S");
                    txtIpAddress.Focus();
                    Get_PunchMachineConfiguration();
                    Clear();
                    break;
                case "Du":
                    camp.msgbox(Page, msgbox, "Duplicate record found!", "A");
                    break;
                default:
                    camp.msgbox(Page, msgbox, msg, "W");
                    break;
            }
        }

        protected void lnkSubmit_OnClick(object sender, EventArgs e)
        {
            Set_PunchMachineConfiguration();
        }

        protected void lnkDelete_OnClick(object sender, EventArgs e)
        {
            var lnk = (LinkButton)sender;
            var lblIpAddress = (Label)lnk.NamingContainer.FindControl("lblIpAddress");
            lblvalue.Text = lblIpAddress.Text.Trim();
            var lblDeviceFor = (Label)lnk.NamingContainer.FindControl("lblDeviceFor");
            lblPDeviceFor.Text = lblDeviceFor.Text.Trim();

            Panel1_ModalPopupExtender.Show();
        }

        protected void lnkYes_OnClick(object sender, EventArgs e)
        {
            Delete_PunchMachineConfiguration();
        }

        public void Delete_PunchMachineConfiguration()
        {
            var param = new List<SqlParameter>
            {
                new SqlParameter("@IpAddress",lblvalue.Text.Trim()),
                new SqlParameter("@BranchCode",Session["BranchCode"]),
                new SqlParameter("@DeviceFor",lblPDeviceFor.Text),
                new SqlParameter("@QueryFor","D")
            };
            var para = new SqlParameter("@Msg", "")
            {
                Direction = ParameterDirection.Output,
                Size = 0x100
            };

            param.Add(para);

            var msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("Set_Get_PunchMachineConfiguration", param);

            if (msg == "S")
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Deleted successfully.", "S");
                Get_PunchMachineConfiguration();
                txtIpAddress.Focus();
            }
            else
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Sorry record not deleted!", "A");
            }
        }

        public void Clear()
        {
            txtIpAddress.Text = string.Empty;
            txtMachineNo.Text = string.Empty;
            txtPortNo.Text = string.Empty;
            txtUsername.Text = string.Empty;
            txtPassword.Text = string.Empty;
            lblID.Text = string.Empty;

            txtIPAddressPanel.Text = string.Empty;
            txtMachineNoPanel.Text = string.Empty;
            txtPortNoPanel.Text = string.Empty;
            txtUsernamePanel.Text = string.Empty;
            txtPasswordPanel.Text = string.Empty;
            lblID.Text = string.Empty;

            txtIpAddress.Focus();
        }
        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            LinkButton chk = (LinkButton)sender;
            Label lblId = (Label)chk.NamingContainer.FindControl("Label36");
            string ss = lblId.Text;
            lblID.Text = ss;

            Label lblDeviceFor = (Label)chk.NamingContainer.FindControl("lblDeviceFor");
            Label lblIpAddress = (Label)chk.NamingContainer.FindControl("lblIpAddress");
            Label lblMachineNo = (Label)chk.NamingContainer.FindControl("lblMachineNo");
            Label lblPortNo = (Label)chk.NamingContainer.FindControl("lblPortNo");
            Label lblUsername = (Label)chk.NamingContainer.FindControl("lblUsername");
            Label lblPassword = (Label)chk.NamingContainer.FindControl("lblPassword");
            Label lblServerPortNo = (Label)chk.NamingContainer.FindControl("lblServerPortNo");
            Label lblDeviceType = (Label)chk.NamingContainer.FindControl("lblDeviceType");
            Label lblConnMode = (Label)chk.NamingContainer.FindControl("lblConnMode");
            Label lblIsPush = (Label)chk.NamingContainer.FindControl("lblIsPush");

            txtIPAddressPanel.Text = lblIpAddress.Text;
            txtMachineNoPanel.Text = lblMachineNo.Text;
            txtPortNoPanel.Text = lblPortNo.Text;
            txtUsernamePanel.Text = lblUsername.Text;
            txtPasswordPanel.Text = lblPassword.Text;
            txtServerPortNoPanel.Text = lblServerPortNo.Text;
            drpDeviceTypePanel.SelectedValue = lblDeviceType.Text;
            rbListPanel.SelectedValue = lblConnMode.Text=="Nw"?"Network":"Usb";
            cbPushPanel.Checked = lblIsPush.Text == "Yes" ? true : false;

            drpDeviceForPanel.SelectedValue= lblDeviceFor.Text;

            Panel2_ModalPopupExtender.Show();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            var param = new List<SqlParameter>
            {
                new SqlParameter("@ID",lblID.Text.Trim()),
                new SqlParameter("@DeviceFor",drpDeviceForPanel.SelectedValue),
                new SqlParameter("@IpAddress",txtIPAddressPanel.Text.Trim()),
                new SqlParameter("@MachineNo",txtMachineNoPanel.Text.Trim()),
                new SqlParameter("@PortNo", txtPortNoPanel.Text.Trim()),
                new SqlParameter("@Username", txtUsernamePanel.Text.Trim()),
                new SqlParameter("@Password",txtPasswordPanel.Text.Trim()==string.Empty?"0":txtPassword.Text.Trim()),
                new SqlParameter("@ServerPortNo",txtServerPortNoPanel.Text.Trim()),
                new SqlParameter("@DeviceType",drpDeviceTypePanel.SelectedValue.ToString()),
                new SqlParameter("@ConnMode",rbListPanel.SelectedValue.ToString()),
                new SqlParameter("@IsPush",cbPushPanel.Checked),
                new SqlParameter("@BranchCode",Session["BranchCode"]),
                new SqlParameter("@QueryFor","U")

            };

            var para = new SqlParameter("@Msg", "")
            {
                Direction = ParameterDirection.Output,
                Size = 0x100
            };

            param.Add(para);

            var msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("Set_Get_PunchMachineConfiguration", param);
            Campus camp = new Campus();
            switch (msg)
            {
                case "U":
                    camp.msgbox(Page, msgbox, "Updated successfully.", "U");
                    Get_PunchMachineConfiguration();
                    Clear();
                    break;
                case "Du":
                    camp.msgbox(Page, msgbox, "Duplicate record found!", "A");
                    break;
                default:
                    camp.msgbox(Page, msgbox, msg, "W");
                    break;
            }
        }
    }
}