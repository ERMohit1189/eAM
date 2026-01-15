using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace _1
{
    public partial class ShiftMapping : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Campus camp = new Campus(); camp.LoadLoader(loader);
            if (!IsPostBack)
            {
                BLL.BLLInstance.loadClasswithSectionandBranch(drpClass, Session["SessionName"].ToString());
                drpClass.Items.Insert(0, new ListItem("All", "-1"));

                BLL.BLLInstance.loadShiftForStudent(drpShift, Session["SessionName"].ToString());

                BLL.BLLInstance.loadClasswithSectionandBranch(drpClassPanel, Session["SessionName"].ToString());
                BLL.BLLInstance.loadShiftForStudent(drpShiftPanel, Session["SessionName"].ToString());

                LoadShiftData();
            }
        }
        protected void InsertShiftDataForClass(string shiftid,string classid,string branchid,string sectionid)
        {
            var param = new List<SqlParameter>
            {
                new SqlParameter("@ShiftId", shiftid),
                new SqlParameter("@ClassId", classid),
                new SqlParameter("@BranchId", branchid),
                new SqlParameter("@SectionId", sectionid),
                new SqlParameter("@SessionName", Session["SessionName"].ToString()),
                new SqlParameter("@LoginName", Session["LoginName"].ToString()),
                new SqlParameter("@QueryFor", "I"),
            };

            SqlParameter para = new SqlParameter("@Msg", "");
            para.Direction = ParameterDirection.Output;

            param.Add(para);

            var msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("Usp_ShiftMappingwithClass", param);

            if (msg == "S")
            {
                LoadShiftData();
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Submitted successfully.", "S");
            }
            else if (msg == "D")
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Sorry, Duplicate Record found!", "A");
            }
            else
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Sorry, Record not  submitted successfully!", "W");
            }

        }
        protected void LoadShiftData()
        {
            var param = new List<SqlParameter>
            {
                new SqlParameter("@SessionName", Session["SessionName"].ToString()),
                new SqlParameter("@QueryFor", "S")
            };

            grdShiftWithClass.DataSource= DLL.objDll.Sp_SelectRecord_usingExecuteDataset("Usp_ShiftMappingwithClass", param);
            grdShiftWithClass.DataBind();
        }
        protected void lnkSubmit_Click(object sender, EventArgs e)
        {
            // ReSharper disable once RedundantAssignment
            int startindex = 0;
            // ReSharper disable once RedundantAssignment
            int lastindex = 0;
            if(drpClass.SelectedIndex==0)
            {
                startindex = 1;
                lastindex = drpClass.Items.Count - 1;
            }
            else
            {
                startindex = drpClass.SelectedIndex;
                lastindex = drpClass.SelectedIndex;
            }

            for(int i=startindex;i<=lastindex;i++)
            {
                string shiftid,classid,branchid,sectionid;

                shiftid = drpShift.SelectedValue;
                classid = drpClass.Items[i].Value.Split('_')[0];
                branchid = drpClass.Items[i].Value.Split('_')[1];
                sectionid = drpClass.Items[i].Value.Split('_')[2];

                InsertShiftDataForClass(shiftid,classid,branchid,sectionid);
            }      
        }
        protected void UpdateShiftData()
        {
            var param = new List<SqlParameter>
            {
                new SqlParameter("@Id", lblValue.Text),
                new SqlParameter("@ShiftId", drpShiftPanel.SelectedValue),
                new SqlParameter("@ClassId", drpClassPanel.SelectedValue.Split('_')[0]),
                new SqlParameter("@BranchId", drpClassPanel.SelectedValue.Split('_')[1]),
                new SqlParameter("@SectionId", drpClassPanel.SelectedValue.Split('_')[2]),
                new SqlParameter("@SessionName", Session["SessionName"].ToString()),
                new SqlParameter("@LoginName", Session["LoginName"].ToString()),
                new SqlParameter("@QueryFor", "U"),
            };

            SqlParameter para = new SqlParameter("@Msg", "");
            para.Direction = ParameterDirection.Output;

            param.Add(para);

            var msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("Usp_ShiftMappingwithClass", param);

            if (msg == "S")
            {
                LoadShiftData();
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Updated successfully.", "S");
            }
            else
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Sorry, Record not  updated successfully!", "W");
            }

        }
        protected void lbtnEdit_Click(object sender, EventArgs e)
        {
            var lnk = (LinkButton)sender;
            var lblid = (Label)lnk.NamingContainer.FindControl("lblid");
            lblValue.Text = lblid.Text;

            var lblClassId = (Label)lnk.NamingContainer.FindControl("lblClassId");
            var lblShiftId = (Label)lnk.NamingContainer.FindControl("lblShiftId");

            drpClassPanel.SelectedValue = lblClassId.Text;
            drpShiftPanel.SelectedValue = lblShiftId.Text;

            Panel1_ModalPopupExtender.Show();
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateShiftData();
        }
        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            var lnk = (LinkButton)sender;
            var lblid = (Label)lnk.NamingContainer.FindControl("lblid");
            lblDelId.Text = lblid.Text;

            Panel2_ModalPopupExtender.Show();
        }
        protected void DeleteShiftData()
        {
            var param = new List<SqlParameter>
            {
                new SqlParameter("@Id", lblDelId.Text),
                new SqlParameter("@QueryFor", "D")
            };

            SqlParameter para = new SqlParameter("@Msg", "");
            para.Direction = ParameterDirection.Output;

            param.Add(para);

            var msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("Usp_ShiftMappingwithClass", param);

            if (msg == "S")
            {
                LoadShiftData();
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Deleted successfully.", "S");
            }
            else
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Sorry, Record not  deleted successfully!", "W");
            }
        }
        protected void lnkDeleteYes_Click(object sender, EventArgs e)
        {
            DeleteShiftData();
        }
    }
}