using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI.WebControls;

namespace _11
{
    public partial class AdminUploadHomeWork : System.Web.UI.Page
    {
        private string _sql = string.Empty;
        private DataSet _ds;

        public AdminUploadHomeWork()
        {
            _ds = new DataSet();
        }

        protected void Page_PreInIt(object sender, EventArgs e)
        {
            if (Session["Logintype"] == null) { Response.Redirect("~/default.aspx"); }
            switch ((string)Session["Logintype"])
            {
                case "Admin":
                    MasterPageFile = "~/Master/admin_root-manager.master";
                    break;
                case "Staff":
                    MasterPageFile = "~/Staff/staff_root-manager.master";
                    break;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Campus camp = new Campus(); camp.LoadLoader(loader);
            if (!IsPostBack)
            {
                txtdate.Text = BAL.objBal.CurrentDate("yyyy MMM dd");
                LoadClass(drpClass);
                LoadClass(drpClasspanel);
                BAL.objBal.fillSelectvalue(drpSection, "<--Select-->", "");
                BAL.objBal.fillSelectvalue(drpBranch, "<--Select-->", "-1");
                LoadGrid("-1");
            }
        }

        private void LoadClass(DropDownList drpclass)
        {
            if (Session["logintype"].ToString() == "Admin")
            {
                BLL.BLLInstance.loadClass(drpclass, Session["SessionName"].ToString());
            }
            else
            {
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
                param.Add(new SqlParameter("@EmpCode", Session["LoginName"].ToString()));
                param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));

                drpclass.DataSource = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("GetClassTeacherClassName_Proc", param);
                drpclass.DataTextField = "ClassName";
                drpclass.DataValueField = "Id";
                drpclass.DataBind();
                drpclass.Items.Insert(0, new ListItem("<--Select-->", "-1"));
            }
        }

        private void LoadSection(DropDownList drpsection, DropDownList drpclass)
        {
            if (Session["Logintype"].ToString() == "Admin")
            {
                //BLL.BLLInstance.loadSection(drpsection, Session["SessionName"].ToString(), drpclass.SelectedValue);
                _sql = "Select SectionName,Id from SectionMaster";
                _sql +=  " where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and ClassNameId=" + drpclass.SelectedValue.ToString() + "";
                BAL.objBal.FillDropDown_withValue(_sql, drpsection, "SectionName", "Id");
                drpsection.Items.Insert(0, new ListItem("<--All-->", ""));
            }
            else
            {
                _sql = "Select SectionName,sm.Id from ClassTeacherMaster T1";
                _sql +=  " inner join SectionMaster sm on sm.Id=T1.SectionId and sm.SessionName=t1.SessionName";
                _sql +=  " where EmpCode='" + Session["LoginName"] + "' and IsClassTeacher=1 and T1.SessionName='" + Session["SessionName"] + "'";
                _sql +=  " and sm.BranchCode=" + Session["BranchCode"] + " and T1.BranchCode=" + Session["BranchCode"] + " and t1.Classid=" + drpclass.SelectedValue + "";
                BAL.objBal.FillDropDown_withValue(_sql, drpsection, "SectionName", "Id");
                drpsection.Items.Insert(0, "<--Select-->");
            }
        }

        private void LoadBranch(DropDownList drpbranch, DropDownList drpclass)
        {
            if (Session["Logintype"].ToString() == "Admin")
            {
                BLL.BLLInstance.loadBranch(drpbranch, Session["SessionName"].ToString(), drpclass.SelectedValue);
            }
            else
            {
                _sql = "Select BranchName,bm.Id from ClassTeacherMaster T1";
                _sql +=  "   inner join BranchMaster bm on bm.Id=T1.BranchId and bm.SessionName=t1.SessionName";
                _sql +=  "   where EmpCode='" + Session["LoginName"] + "' and IsClassTeacher=1 and bm.BranchCode=" + Session["BranchCode"] + " and T1.BranchCode=" + Session["BranchCode"] + " and";
                _sql +=  "   T1.SessionName='" + Session["SessionName"] + "' and T1.Classid='" + drpclass.SelectedValue + "'";
                BAL.objBal.FillDropDown_withValue(_sql, drpbranch, "BranchName", "Id");
                drpbranch.Items.Insert(0, new ListItem("<--Select-->", "0"));
            }
        }

        protected void drpClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            drpClass.Focus();
            LoadSection(drpSection, drpClass);
            LoadBranch(drpBranch, drpClass);
            if (drpSection.Items.Count > 1)
            {
                drpSection.SelectedIndex = 1;
            }
            if (drpBranch.Items.Count > 1)
            {
                drpBranch.SelectedIndex = 1;
            }
            LoadGrid("-1");
        }

        protected void lnkSubmit_Click(object sender, EventArgs e)
        {
            if (drpSection.SelectedIndex == 0)
            {

                for (int i = 1; i < drpSection.Items.Count; i++)
                {
                    for (int ii = 1; ii < drpBranch.Items.Count; ii++)
                    {
                        try
                        {
                            string filePath = "";
                            string fileName = "";
                            if (hfSllabusFile.Value != string.Empty && hfSllabusFile.Value != null)
                            {
                                string base64std = hfSllabusFile.Value;
                                if (base64std != string.Empty)
                                {
                                    fileName = "HomeWork_" + DateTime.Now.ToString("dd-MM-yy_hhmmss") + hfSllabusFileext.Value.ToString();
                                    filePath = string.Format("~/Uploads/Docs/pdf/{0}", fileName);

                                    using (FileStream fs = new FileStream(Server.MapPath(filePath), FileMode.Create))
                                    {
                                        using (BinaryWriter bw = new BinaryWriter(fs))
                                        {
                                            byte[] data = Convert.FromBase64String(base64std);
                                            bw.Write(data);
                                            bw.Close();
                                        }
                                    }
                                }
                            }
                            List<SqlParameter> param = new List<SqlParameter>();
                            param.Add(new SqlParameter("@Title", txtTitle.Text.Trim()));
                            param.Add(new SqlParameter("@Date", txtdate.Text.Trim()));
                            param.Add(new SqlParameter("@PDFName", fileName));
                            param.Add(new SqlParameter("@PDFurl", filePath));
                            param.Add(new SqlParameter("@ClassId", drpClass.SelectedValue));
                            param.Add(new SqlParameter("@SectionId", drpSection.Items[i].Value));
                            param.Add(new SqlParameter("@BranchId", drpBranch.Items[ii].Value));
                            param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
                            param.Add(new SqlParameter("@LoginName", Session["LoginName"].ToString()));
                            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
                            param.Add(new SqlParameter("@QueryFor", "I"));

                            SqlParameter para = new SqlParameter("@Msg", "");
                            para.Direction = ParameterDirection.Output;
                            para.Size = 0x100;
                            param.Add(para);

                            var msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("USP_Student_HomeWork", param);

                            if (msg == "S")
                            {
                                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Submitted successfully.", "S");
                            }
                            else if (msg == "D")
                            {
                                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Duplicate Entry!", "A");
                            }
                            else
                            {
                                Campus camp = new Campus(); camp.msgbox(Page, msgbox, msg, "W");
                            }
                            LoadGrid("-1");
                        }
                        catch (Exception)
                        {
                            // ignored
                        }
                    }
                }
            }
            else
            {
                for (int ii = 1; ii < drpBranch.Items.Count; ii++)
                {
                    try
                    {
                        string filePath = "";
                        string fileName = "";
                        if (hfSllabusFile.Value != string.Empty && hfSllabusFile.Value != null)
                        {
                            string base64std = hfSllabusFile.Value;
                            if (base64std != string.Empty)
                            {
                                fileName = "HomeWork_" + DateTime.Now.ToString("dd-MM-yy_hhmmss") + hfSllabusFileext.Value.ToString();
                                filePath = string.Format("~/Uploads/Docs/pdf/{0}", fileName);

                                using (FileStream fs = new FileStream(Server.MapPath(filePath), FileMode.Create))
                                {
                                    using (BinaryWriter bw = new BinaryWriter(fs))
                                    {
                                        byte[] data = Convert.FromBase64String(base64std);
                                        bw.Write(data);
                                        bw.Close();
                                    }
                                }
                            }
                        }
                        List<SqlParameter> param = new List<SqlParameter>();
                        param.Add(new SqlParameter("@Title", txtTitle.Text.Trim()));
                        param.Add(new SqlParameter("@Date", txtdate.Text.Trim()));
                        param.Add(new SqlParameter("@PDFName", fileName));
                        param.Add(new SqlParameter("@PDFurl", filePath));
                        param.Add(new SqlParameter("@ClassId", drpClass.SelectedValue));
                        param.Add(new SqlParameter("@SectionId", drpSection.SelectedValue));
                        param.Add(new SqlParameter("@BranchId", drpBranch.Items[ii].Value));
                        param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
                        param.Add(new SqlParameter("@LoginName", Session["LoginName"].ToString()));
                        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
                        param.Add(new SqlParameter("@QueryFor", "I"));

                        SqlParameter para = new SqlParameter("@Msg", "");
                        para.Direction = ParameterDirection.Output;
                        para.Size = 0x100;
                        param.Add(para);

                        var msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("USP_Student_HomeWork", param);

                        if (msg == "S")
                        {
                            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Submitted successfully.", "S");
                        }
                        else if (msg == "D")
                        {
                            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Duplicate Entry!", "A");
                        }
                        else
                        {
                            Campus camp = new Campus(); camp.msgbox(Page, msgbox, msg, "W");
                        }
                        LoadGrid("-1");
                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                }
            }

        }

        private void LoadGrid(string id)
        {

            List<SqlParameter> param = new List<SqlParameter>();
            //var drpBarnch = drpBranch.SelectedValue;
            //drpBarnch = !string.IsNullOrEmpty(drpBarnch) ? drpBranch.SelectedValue : "-1";
            //var drpSectionId = drpSection.SelectedValue;
            //drpSectionId = !string.IsNullOrEmpty(drpSectionId) ? drpSection.SelectedValue : "";
            param.Add(new SqlParameter("@Id", id));
            param.Add(new SqlParameter("@ClassId", drpClass.SelectedValue));
            param.Add(new SqlParameter("@BranchId", drpBranch.SelectedValue));
            param.Add(new SqlParameter("@SectionId", drpSection.SelectedValue));
            param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
            param.Add(new SqlParameter("@QueryFor", "S"));

            _ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("USP_Student_HomeWork", param);
            if (_ds.Tables[0].Rows.Count > 0)
            {
                grdDocList.DataSource = _ds;
                grdDocList.DataBind();
            }
            else
            {
                grdDocList.DataSource = null;
                grdDocList.DataBind();
            }

        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            Label lblid = (Label)lnk.NamingContainer.FindControl("lblEdit");
            lblID.Text = lblid.Text;
            List<SqlParameter> param = new List<SqlParameter>();

            param.Add(new SqlParameter("@Id", lblid.Text));
            param.Add(new SqlParameter("@ClassId", "-1"));
            param.Add(new SqlParameter("@BranchId", "-1"));
            param.Add(new SqlParameter("@SectionId", ""));
            param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
            param.Add(new SqlParameter("@QueryFor", "S"));

            DataSet ds;
            ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("USP_Student_HomeWork", param);
            if (ds != null)
            {
                DataTable dt;
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    txtTitlepanel.Text = dt.Rows[0]["Title"].ToString();
                    drpClasspanel.SelectedValue = dt.Rows[0]["ClassId"].ToString();
                    LoadSection(drpSectionpanel, drpClasspanel);
                    LoadBranch(drpBranchpanel, drpClasspanel);
                    CKEditorControl2.Text = dt.Rows[0]["HomeWork"].ToString();
                    txtdate0.Text = DateTime.Parse(dt.Rows[0]["date"].ToString()).ToString("dd-MMM-yyyy");
                    drpBranchpanel.SelectedValue = dt.Rows[0]["BranchId"].ToString();
                    drpSectionpanel.SelectedValue = dt.Rows[0]["SectionId"].ToString();
                }
            }

            Panel1_ModalPopupExtender.Show();
        }

        protected void drpSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            drpSection.Focus();
            LoadGrid("-1");
        }

        protected void drpBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            drpBranch.Focus();
            LoadGrid("-1");
        }

        protected void lnkUpdate_Click(object sender, EventArgs e)
        {
            string filePath0 = "";
            string fileName0 = "";
            if (hfSllabusFile.Value != string.Empty && hfSllabusFile.Value != null)
            {
                string base64std = hfSllabusFile.Value;
                if (base64std != string.Empty)
                {
                    fileName0 = "HomeWork_" + DateTime.Now.ToString("dd-MM-yy_hhmmss") + hfSllabusFileext.Value.ToString();
                    filePath0 = string.Format("~/Uploads/Docs/pdf/{0}", fileName0);

                    using (FileStream fs = new FileStream(Server.MapPath(filePath0), FileMode.Create))
                    {
                        using (BinaryWriter bw = new BinaryWriter(fs))
                        {
                            byte[] data = Convert.FromBase64String(base64std);
                            bw.Write(data);
                            bw.Close();
                        }
                    }
                }
            }
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@Id", lblID.Text.Trim()));
            param.Add(new SqlParameter("@Title", txtTitlepanel.Text.Trim()));

            param.Add(new SqlParameter("@Date", txtdate0.Text.Trim()));
            if (fileName0 != "")
            {
                param.Add(new SqlParameter("@PDFName", fileName0));
                param.Add(new SqlParameter("@PDFurl", filePath0));
            }

            param.Add(new SqlParameter("@HomeWork", CKEditorControl2.Text));
            param.Add(new SqlParameter("@ClassId", drpClasspanel.SelectedValue));
            param.Add(new SqlParameter("@BranchId", drpBranchpanel.SelectedValue));
            param.Add(new SqlParameter("@SectionId", drpSectionpanel.SelectedValue));
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
            param.Add(new SqlParameter("@QueryFor", "U"));
            SqlParameter para = new SqlParameter("@Msg", "");
            para.Direction = ParameterDirection.Output;
            para.Size = 0x100;
            param.Add(para);

            var msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("USP_Student_HomeWork", param);

            if (msg == "S")
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Updated successfully.", "S");
                txtTitlepanel.Text = "";
            }
            else if (msg == "D")
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Duplicate Entry!", "A");
            }
            else
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, msg, "W");
            }
            lblID.Text = "-1";
            LoadGrid("-1");
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            Label lblid = (Label)lnk.NamingContainer.FindControl("lblDelete");
            lblvalue.Text = lblid.Text;
            Panel2_ModalPopupExtender.Show();
            lnkNo.Focus();
        }

        protected void lnkYes_Click(object sender, EventArgs e)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@Id", lblvalue.Text.Trim()));
            param.Add(new SqlParameter("@QueryFor", "D"));
            SqlParameter para = new SqlParameter("@Msg", "");
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
            para.Direction = ParameterDirection.Output;
            para.Size = 0x100;
            param.Add(para);

            var msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("USP_Student_HomeWork", param);

            if (msg == "S")
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Deleted successfully.", "S");
                LoadGrid("-1");
            }
            else
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, msg, "W");
            }


        }

        protected void drpClasspanel_SelectedIndexChanged(object sender, EventArgs e)
        {
            Panel1_ModalPopupExtender.Show();
        }

        public override void Dispose()
        {
            _ds.Dispose();
        }
    }
}