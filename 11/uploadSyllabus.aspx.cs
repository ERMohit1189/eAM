using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;

public partial class admin_UploadSyllabus : Page
{
    string sql = "";
    protected void Page_PreInIt(object sender, EventArgs e)
    {
        if (Session["Logintype"] == null) { Response.Redirect("~/default.aspx"); }
        if (Session["Logintype"].ToString() == "Admin")
        {
            this.MasterPageFile = "~/Master/admin_root-manager.master";
        }
        else if (Session["Logintype"].ToString() == "Staff")
        {
            this.MasterPageFile = "~/Staff/staff_root-manager.master";
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Campus camp = new Campus(); camp.LoadLoader(loader);
        if (!IsPostBack)
        {
            loadClass(drpClass);
            loadClass(drpClasspanel);
            drpSection.Items.Insert(0, new ListItem("<--Select-->", ""));
            drpBranch.Items.Insert(0, new ListItem("<--Select-->", ""));
            loadGrid("-1");
        }
    }

    private void loadClass(DropDownList drpclass)
    {
        if (Session["logintype"].ToString() == "Admin")
        {
            BLL.BLLInstance.loadClass(drpclass, Session["SessionName"].ToString());
        }
        else
        {

            //sql = "Select ClassName,cm.Id from ClassTeacherMaster T1 inner join ClassMaster cm on cm.Id=T1.ClassId ";
            //sql +=  " and cm.SessionName=t1.SessionName inner join dt_ClassGroupMaster T2 on T2.ClassId=T1.ClassId ";
            //sql +=  " and cm.SessionName=T2.SessionName where GroupId in (Select GroupId from dt_ClassGroupMaster where ClassId in ";
            //sql +=  " (Select Distinct ClassId from ClassTeacherMaster where EmpCode='" + Session["LoginName"].ToString() + "'  and IsClassTeacher=1)  ";
            //sql +=  " and IsActive=1 and SessionName='" + Session["SessionName"].ToString() + "') and EmpCode='" + Session["LoginName"].ToString() + "' and IsClassTeacher=1 ";
            //sql +=  " and T1.SessionName='" + Session["SessionName"].ToString() + "' Order by CIDOrder";

            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
            param.Add(new SqlParameter("@EmpCode", Session["LoginName"].ToString()));

            drpclass.DataSource = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("GetClassTeacherClassName_Proc", param);
            drpclass.DataTextField = "ClassName";
            drpclass.DataValueField = "Id";
            drpclass.DataBind();

            drpclass.Items.Insert(0, new ListItem("<--Select-->", "0"));

        }
    }

    private void loadSection(DropDownList drpsection, DropDownList drpclass)
    {
        if (Session["Logintype"].ToString() == "Admin")
        {
            sql = "Select distinct SectionName,Id from SectionMaster";
            sql +=  " where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and ClassNameId=" + drpclass.SelectedValue.ToString() + "";
            BAL.objBal.FillDropDown_withValue(sql, drpsection, "SectionName", "Id");
            drpsection.Items.Insert(0, new ListItem("<--All-->", ""));
        }
        else
        {
            sql = "Select distinct SectionName,sm.Id from ClassTeacherMaster T1";
            sql +=  " inner join SectionMaster sm on sm.Id=T1.SectionId and sm.SessionName=t1.SessionName";
            sql +=  " where EmpCode='" + Session["LoginName"].ToString() + "' and IsClassTeacher=1 and T1.SessionName='" + Session["SessionName"] + "'";
            sql +=  " and t1.Classid=" + drpclass.SelectedValue.ToString() + "";
            BAL.objBal.FillDropDown_withValue(sql, drpsection, "SectionName", "Id");
            drpsection.Items.Insert(0, "<--Select-->");
        }
    }

    private void loadBranch(DropDownList drpbranch, DropDownList drpclass)
    {
        if (Session["Logintype"].ToString() == "Admin")
        {
            BLL.BLLInstance.loadBranch(drpbranch, Session["SessionName"].ToString(), drpclass.SelectedValue.ToString());
        }
        else
        {
            sql = "Select distinct BranchName,bm.Id from ClassTeacherMaster T1";
            sql +=  "   inner join BranchMaster bm on bm.Id=T1.BranchId and bm.SessionName=t1.SessionName";
            sql +=  "   where EmpCode='" + Session["LoginName"].ToString() + "' and IsClassTeacher=1 and";
            sql +=  "   T1.SessionName='" + Session["SessionName"] + "' and T1.Classid='" + drpclass.SelectedValue.ToString() + "'";
            BAL.objBal.FillDropDown_withValue(sql, drpbranch, "BranchName", "Id");
            drpbranch.Items.Insert(0, new ListItem("<--Select-->", "0"));
        }
    }

    protected void drpClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        drpClass.Focus();
        loadSection(drpSection,drpClass);
        loadBranch(drpBranch,drpClass);
        
        loadGrid("-1");
    }

    protected void lnkSubmit_Click(object sender, EventArgs e)
    {
        string Msg = ""; int cntS = 0; int cntF = 0;
        if (hfSllabusFile.Value != string.Empty && hfSllabusFile.Value != null)
        {
            if (drpSection.SelectedIndex == 0)
            {
                string filePath = "";
                string fileName = "";

                string base64std = hfSllabusFile.Value;
                if (base64std != string.Empty)
                {
                    fileName = "Syllabus_" + drpClass.SelectedItem.Text + "_" + txtTitle.Text.Trim() + "_" + DateTime.Now.ToString("dd-MM-yy_hhmmss") + hfSllabusFileext.Value.ToString();
                    filePath = string.Format("~/Uploads/Docs/Syllabus/{0}", fileName);

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
                
                for (int i = 1; i < drpSection.Items.Count; i++)
                {
                    lblErrormsg.Text = string.Empty;

                    List<SqlParameter> param = new List<SqlParameter>();
                    param.Add(new SqlParameter("@Id", "-1"));
                    param.Add(new SqlParameter("@Title", txtTitle.Text.Trim()));
                    

                    param.Add(new SqlParameter("@DocPath", filePath));
                    param.Add(new SqlParameter("@ClassId", drpClass.SelectedValue.ToString()));
                    param.Add(new SqlParameter("@BranchId", drpBranch.SelectedValue.ToString()));
                    param.Add(new SqlParameter("@SectionId", drpSection.Items[i].Value.ToString()));
                    param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
                    param.Add(new SqlParameter("@LoginName", Session["LoginName"].ToString()));
                    param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
                    param.Add(new SqlParameter("@QueryFor", "I"));
                    SqlParameter para = new SqlParameter("@Msg", "");
                    para.Direction = ParameterDirection.Output;
                    para.Size = 0x100;
                    param.Add(para);

                    Msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("Std_Syllabus_proc", param);

                    if (Msg == "S")
                    {
                        cntS = cntS + 1;
                    }
                    else if (Msg == "D")
                    {
                        cntF = cntF + 1;
                    }
                }
            }
            else
            {
                lblErrormsg.Text = string.Empty;

                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@Id", "-1"));
                param.Add(new SqlParameter("@Title", txtTitle.Text.Trim()));
                string filePath = "";
                string fileName = "";

                string base64std = hfSllabusFile.Value;
                if (base64std != string.Empty)
                {
                    fileName = "Syllabus_" + drpClass.SelectedItem.Text + "_" + txtTitle.Text.Trim() + "_" + DateTime.Now.ToString("dd-MM-yy_hhmmss") + hfSllabusFileext.Value.ToString();
                    filePath = string.Format("~/Uploads/Docs/Syllabus/{0}", fileName);

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

                param.Add(new SqlParameter("@DocPath", filePath));
                param.Add(new SqlParameter("@ClassId", drpClass.SelectedValue.ToString()));
                param.Add(new SqlParameter("@BranchId", drpBranch.SelectedValue.ToString()));
                param.Add(new SqlParameter("@SectionId", drpSection.SelectedValue.ToString()));
                param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
                param.Add(new SqlParameter("@LoginName", Session["LoginName"].ToString()));
                param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
                param.Add(new SqlParameter("@QueryFor", "I"));
                SqlParameter para = new SqlParameter("@Msg", "");
                para.Direction = ParameterDirection.Output;
                para.Size = 0x100;
                param.Add(para);

                Msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("Std_Syllabus_proc", param);
                if (Msg == "S")
                {
                    cntS = cntS + 1;
                }
                else if (Msg == "D")
                {
                    cntF = cntF + 1;
                }

            }

            if (cntS > 0 && cntF == 0)
            {
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");
                hfSllabusFile.Value = null;
                hfSllabusFileext.Value = null;
                txtTitle.Text = "";
            }
            else if (cntS ==0 && cntF>0)
            {
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Duplicate Entry!", "A");
            }
            else if (cntS > 0 && cntF > 0)
            {
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully but some duplicate Entry!", "A");
            }

            loadGrid("-1");

        }
        else
        {
            lblErrormsg.Text = "Please, Select file!";
        }
    }

    private void loadGrid(string Id)
    {
        List<SqlParameter> param = new List<SqlParameter>();

        param.Add(new SqlParameter("@Id", Id));
        param.Add(new SqlParameter("@ClassId", drpClass.SelectedValue.ToString()));
        if (drpBranch.SelectedValue!="")
        {
            param.Add(new SqlParameter("@BranchId", drpBranch.SelectedValue.ToString()));
        }
        if (drpSection.SelectedValue != "")
        {
            param.Add(new SqlParameter("@SectionId", drpSection.SelectedValue.ToString()));
        }
        param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
        param.Add(new SqlParameter("@QueryFor", "S"));

        grdDocList.DataSource = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("Std_Syllabus_proc", param);
        grdDocList.DataBind();
    }

    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        Label lblid = (Label)lnk.NamingContainer.FindControl("lblEdit");
        lblID.Text = lblid.Text;
        List<SqlParameter> param = new List<SqlParameter>();

        param.Add(new SqlParameter("@Id", lblid.Text));
        param.Add(new SqlParameter("@ClassId", "-1"));
        param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
        param.Add(new SqlParameter("@QueryFor", "S"));

        DataSet ds = new DataSet();
        ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("Std_Syllabus_proc", param);
        if (ds != null)
        {
            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                txtTitlepanel.Text = dt.Rows[0]["Title"].ToString();
                drpClasspanel.SelectedValue = dt.Rows[0]["ClassId"].ToString();
                loadSection(drpSectionpanel, drpClasspanel);
                loadBranch(drpBranchpanel, drpClasspanel);
                drpBranchpanel.SelectedValue = dt.Rows[0]["BranchId"].ToString();
                drpSectionpanel.SelectedValue = dt.Rows[0]["SectionId"].ToString();
            }
        }

        Panel1_ModalPopupExtender.Show();
    }
    protected void drpSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        drpSection.Focus();
        loadGrid("-1");
    }
    protected void drpBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        drpBranch.Focus();
        loadGrid("-1");
    }
    protected void lnkUpdate_Click(object sender, EventArgs e)
    {
        string Msg = "";

        List<SqlParameter> param = new List<SqlParameter>();
        param.Add(new SqlParameter("@Id", lblID.Text.Trim()));
        param.Add(new SqlParameter("@Title", txtTitlepanel.Text.Trim()));
        string filePath = "";
        string fileName = "";

        string base64std = hfSllabusFilepanel.Value;
        if (base64std != string.Empty)
        {
            fileName = "Syllabus_" + drpClasspanel.SelectedItem.Text + "_" + txtTitlepanel.Text.Trim() + "_" + DateTime.Now.ToString("dd-MM-yy_hhmmss") + hfSllabusFileextpanel.Value.ToString();
            filePath = string.Format("~/Uploads/Docs/Syllabus/{0}", fileName);

            using (FileStream fs = new FileStream(Server.MapPath(filePath), FileMode.Create))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    byte[] data = Convert.FromBase64String(base64std);
                    bw.Write(data);
                    bw.Close();
                }
            }
            param.Add(new SqlParameter("@DocPath", filePath));
        }
        param.Add(new SqlParameter("@ClassId", drpClasspanel.SelectedValue.ToString()));
        param.Add(new SqlParameter("@BranchId", drpBranchpanel.SelectedValue.ToString()));
        param.Add(new SqlParameter("@SectionId", drpSectionpanel.SelectedValue.ToString()));
        param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
        param.Add(new SqlParameter("@QueryFor", "U"));
        SqlParameter para = new SqlParameter("@Msg", "");
        para.Direction = ParameterDirection.Output;
        para.Size = 0x100;
        param.Add(para);

        Msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("Std_Syllabus_proc", param);

        if (Msg == "S")
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully.", "S");
            txtTitlepanel.Text = "";
            hfSllabusFilepanel.Value = null;
            hfSllabusFileextpanel.Value = null;
        }
        else if (Msg == "D")
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Duplicate Entry!", "A");
        }
        else
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, Msg, "W");
        }
        hfSllabusFileextpanel.Value = null;
        hfSllabusFilepanel.Value = null;
        lblID.Text = "-1";
        loadGrid("-1");
    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        Label lblid = (Label)lnk.NamingContainer.FindControl("lblDelete");
        Label lblDocPath = (Label)lnk.NamingContainer.FindControl("lblDocPath");
        lblvalue.Text = lblid.Text;
        lblpath.Text = lblDocPath.Text;
        Panel2_ModalPopupExtender.Show();
        lnkNo.Focus();
    }
    protected void lnkYes_Click(object sender, EventArgs e)
    {
        string Msg = "";

        List<SqlParameter> param = new List<SqlParameter>();
        param.Add(new SqlParameter("@Id", lblvalue.Text.Trim()));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
        param.Add(new SqlParameter("@QueryFor", "D"));
        SqlParameter para = new SqlParameter("@Msg", "");
        para.Direction = ParameterDirection.Output;
        para.Size = 0x100;
        param.Add(para);
     
        Msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("Std_Syllabus_proc", param);

        if (Msg == "S")
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Deleted successfully.", "S");
            loadGrid("-1");
            try
            {
                File.Delete(Server.MapPath(ResolveClientUrl(lblpath.Text)));
            }
            catch
            {
            }
        }
        else
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, Msg, "W");
        }

        
    }
    protected void drpClasspanel_SelectedIndexChanged(object sender, EventArgs e)
    {
        Panel1_ModalPopupExtender.Show();
    }
}