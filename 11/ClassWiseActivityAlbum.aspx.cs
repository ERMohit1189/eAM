using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.WebControls;
using System.IO;

public partial class commom_ClassWiseActivity : System.Web.UI.Page
{
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
            loadClass(drpClass); loadClass(drpClassPanel);
            loadSection(drpSection, drpClass);
            loadBranch(drpBranch, drpClass);
            //loadPanelClass(drpClassPanel);
            //loadToClass(drpToClass);
          
            loadGrid("-1", drpClass.SelectedValue.ToString(), drpSection.SelectedValue.ToString(), drpBranch.SelectedValue.ToString());
        }
        txtTitle.Focus();
    }

    string sql = "";

    private void loadClass(DropDownList drpClass)
    {
        if (Session["logintype"].ToString() == "Admin")
        {
            BLL.BLLInstance.loadClass(drpClass, Session["SessionName"].ToString());
        }
        else
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
            param.Add(new SqlParameter("@EmpCode", Session["LoginName"].ToString()));
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));

            drpClass.DataSource = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("GetClassTeacherClassName_Proc", param);
            drpClass.DataTextField = "ClassName";
            drpClass.DataValueField = "Id";
            drpClass.DataBind();
            drpClass.Items.Insert(0, new ListItem("<--Select-->", "0"));     
        }
    }

    private void loadSection(DropDownList drpsection, DropDownList drpclass)
    {
        if (Session["Logintype"].ToString() == "Admin")
        {
            BLL.BLLInstance.loadSection(drpsection, Session["SessionName"].ToString(), drpclass.SelectedValue.ToString());
        }
        else
        {
            sql = "Select SectionName,sm.Id from ClassTeacherMaster T1";
            sql +=  " inner join SectionMaster sm on sm.Id=T1.SectionId and sm.SessionName=t1.SessionName";
            sql +=  " where EmpCode='" + Session["LoginName"].ToString() + "' and IsClassTeacher=1 and T1.SessionName='" + Session["SessionName"] + "'";
            sql +=  " and t1.Classid=" + drpclass.SelectedValue.ToString() + " and sm.BranchCode=" + Session["BranchCode"] + " and T1.BranchCode=" + Session["BranchCode"] + "";
 
            BAL.objBal.FillDropDown_withValue(sql, drpsection, "SectionName", "Id");
            BAL.objBal.fillSelectvalue(drpsection, "<--Select-->", "0");
        }
        if (drpsection.Items.Count == 2)
        {
            drpsection.SelectedIndex = 1;
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
            sql = "Select BranchName,bm.Id from ClassTeacherMaster T1";
            sql +=  "   inner join BranchMaster bm on bm.Id=T1.BranchId and bm.SessionName=t1.SessionName";
            sql +=  "   where EmpCode='" + Session["LoginName"].ToString() + "' and IsClassTeacher=1 and";
            sql +=  "   T1.SessionName='" + Session["SessionName"] + "' and bm.BranchCode=" + Session["BranchCode"] + " and t1.BranchCode=" + Session["BranchCode"] + " and T1.Classid='" + drpclass.SelectedValue.ToString() + "'";
            BAL.objBal.FillDropDown_withValue(sql, drpbranch, "BranchName", "Id");

            BAL.objBal.fillSelectvalue(drpbranch, "<--Select-->", "0");
        }
        if (drpbranch.Items.Count == 2)
        {
            drpbranch.SelectedIndex = 1;
        }

    }
   
    protected void drpClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadSection(drpSection, drpClass);
        loadBranch(drpBranch, drpClass);

        ChkAll.Checked = false;
        fr1.Visible = false;
        loadGrid("-1", drpClass.SelectedValue.ToString(), drpSection.SelectedValue.ToString(), drpBranch.SelectedValue.ToString());
        drpClass.Focus();
    }

    private DataSet select(string id,string classid,string sectionid,string branchid)
    {
        List<SqlParameter> param = new List<SqlParameter>();
        param.Add(new SqlParameter("@QueryFor", "S"));
        param.Add(new SqlParameter("@Id", id));
        param.Add(new SqlParameter("@ClassId", classid));
        param.Add(new SqlParameter("@SectionId", sectionid));
        param.Add(new SqlParameter("@BranchId", branchid));
        param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));

        DataSet ds = new DataSet();

        rpt1.DataSource = ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("ClassWiseActivityAlbum_Proc", param);
        rpt1.DataBind();

        if (rpt1.Items.Count==0)
        {
            hr1.Visible = false;
        }
        else
        {
            hr1.Visible = true;
        }

        return ds;
    }
    private DataSet loadGrid(string id, string classid, string sectionid, string branchid)
    {
        return select(id, classid, sectionid,branchid);
    }

    protected void lnkSubmit_Click(object sender, EventArgs e)
    {
        //if (hdAlbumPhoto.Value != string.Empty)
        //{
            //lblErrormsg.Text = string.Empty;
            insert();
        //}
        //else
        //{
        //    //lblErrormsg.Text = "Please, Select file!";
        //}
    }
    private void insert()
    {
        string msg = "";

        List<SqlParameter> param = new List<SqlParameter>();

        param.Add(new SqlParameter("@QueryFor", "I"));
        param.Add(new SqlParameter("@Title", txtTitle.Text.Trim()));
        param.Add(new SqlParameter("@ClassId", drpClass.SelectedValue.ToString()));
        param.Add(new SqlParameter("@SectionId", drpSection.SelectedValue.ToString()));
        param.Add(new SqlParameter("@BranchId", drpBranch.SelectedValue.ToString()));

        string filePath = "";
        string fileName = "";


        string base64std = hdAlbumPhoto.Value;
        if (base64std != string.Empty)
        {
            filePath = @"../Uploads/Album/";
            fileName = txtTitle.Text + '_' + drpClass.SelectedValue.Trim() + '_' + drpSection.SelectedValue.Trim() + '_' + drpBranch.SelectedValue.Trim() + ".jpg";

            filePath = filePath + fileName;

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

        param.Add(new SqlParameter("@FileName", fileName));
        param.Add(new SqlParameter("@FilePath", filePath));

        param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
        param.Add(new SqlParameter("@LoginName", Session["LoginName"].ToString()));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));

        SqlParameter para = new SqlParameter("@Msg", "");
        para.Direction = ParameterDirection.Output;
        para.Size = 0x100;
        param.Add(para);

        msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("ClassWiseActivityAlbum_Proc", param);


        if (msg.Trim() == "S")
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");
            txtTitle.Text = "";
        }
        if (msg.Trim() == "D")
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Duplicate Entry!", "A");
            txtTitle.Text = "";
        }
        else
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, msg, "W");
        }

        loadGrid("-1", drpClass.SelectedValue.ToString(), drpSection.SelectedValue.ToString(), drpBranch.SelectedValue.ToString());
    }

    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        Label lblid = (Label)lnk.NamingContainer.FindControl("lblEdit");
        lblID.Text = lblid.Text;
        List<SqlParameter> param = new List<SqlParameter>();

        DataSet ds = new DataSet();

        ds = loadGrid(lblID.Text.Trim(), "-1", "-1", "-1");

        if (ds != null)
        {
            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                txtTitlePanel.Text = dt.Rows[0]["Title"].ToString();
                drpClass.SelectedValue = dt.Rows[0]["ClassId"].ToString();
                drpClassPanel.SelectedValue = dt.Rows[0]["ClassId"].ToString();
                AvatarPanel.ImageUrl = dt.Rows[0]["FilePath"].ToString();

                loadSection(drpSection, drpClass);
                loadBranch(drpBranch, drpClass);

                loadSection(drpSectionPanel, drpClassPanel);
                loadBranch(drpBranchPanel, drpClassPanel);

                drpSection.SelectedValue = dt.Rows[0]["SectionId"].ToString();
                drpSectionPanel.SelectedValue = dt.Rows[0]["SectionId"].ToString();

                drpBranch.SelectedValue = dt.Rows[0]["BranchId"].ToString();
                drpBranchPanel.SelectedValue = dt.Rows[0]["BranchId"].ToString();
            }
        }

        loadGrid("-1", drpClassPanel.SelectedValue.ToString(), drpSectionPanel.SelectedValue.ToString(), drpBranchPanel.SelectedValue.ToString());

        Panel1_ModalPopupExtender.Show();
        txtTitlePanel.Focus();
    }
    protected void lnkUpdate_Click(object sender, EventArgs e)
    {
        update();
    }
    private void update()
    {
        string msg = "";

        List<SqlParameter> param = new List<SqlParameter>();

        param.Add(new SqlParameter("@QueryFor", "U"));
        param.Add(new SqlParameter("@Id", lblID.Text.Trim()));
        param.Add(new SqlParameter("@Title", txtTitlePanel.Text.Trim()));
        param.Add(new SqlParameter("@ClassId", drpClassPanel.SelectedValue.ToString()));
        param.Add(new SqlParameter("@SectionId", drpSectionPanel.SelectedValue.ToString()));
        param.Add(new SqlParameter("@BranchId", drpBranchPanel.SelectedValue.ToString()));

        string filePath = "";
        string fileName = "";


        string base64std = hdAlbumPhotoPanel.Value;
        if (base64std != string.Empty)
        {
            filePath = @"../Uploads/Album/";
            fileName = txtTitlePanel.Text + '_' + drpClassPanel.SelectedItem.Text.ToString() + ".jpg";

            filePath = filePath + fileName;

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

        param.Add(new SqlParameter("@FileName", fileName));
        param.Add(new SqlParameter("@FilePath", filePath));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));

        SqlParameter para = new SqlParameter("@Msg", "");
        para.Direction = ParameterDirection.Output;
        para.Size = 0x100;
        param.Add(para);

        msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("ClassWiseActivityAlbum_Proc", param);


        if (msg.Trim() == "S")
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully.", "S");
            txtTitle.Text = "";
            hdAlbumPhotoPanel.Value = "";
        }
        else if (msg == "D")
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Duplicate Entry!", "A");
        }
        else
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, msg, "W");
        }
        lblID.Text = "-1";
        loadGrid("-1", drpClassPanel.SelectedValue.ToString(), drpSectionPanel.SelectedValue.ToString(), drpBranchPanel.SelectedValue.ToString());
    }

    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        Label lblid = (Label)lnk.NamingContainer.FindControl("lblDelete");
        Label lblimgName = (Label)lnk.NamingContainer.FindControl("lblimgName");
        hdImagePath.Value = lblimgName.Text;
        lblvalue.Text = lblid.Text;
        Panel2_ModalPopupExtender.Show();
        lnkNo.Focus();
    }

    protected void lnkYes_Click(object sender, EventArgs e)
    {
        delete();
    }

    private void delete()
    {
        string Msg = "";

        List<SqlParameter> param = new List<SqlParameter>();

        param.Add(new SqlParameter("@QueryFor", "D"));
        param.Add(new SqlParameter("@Id", lblvalue.Text.Trim()));
        param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));

        SqlParameter para = new SqlParameter("@Msg", "");
        para.Direction = ParameterDirection.Output;
        para.Size = 0x100;
        param.Add(para);

        Msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("ClassWiseActivityAlbum_Proc", param);

        if (Msg == "S")
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Deleted successfully.", "S");
            if (File.Exists(Server.MapPath(hdImagePath.Value)))
            {
                // ReSharper disable once AssignNullToNotNullAttribute
                File.Delete(Server.MapPath(hdImagePath.Value));
            }
            loadGrid("-1", drpClass.SelectedValue.ToString(), drpSection.SelectedValue.ToString(), drpBranch.SelectedValue.ToString());
        }
        else
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, Msg, "W");
        }

    }

    protected void ChkAll_CheckedChanged(object sender, EventArgs e)
    {
        bool flag=false;
        fr1.Visible = false;
        if (ChkAll.Checked)
        {
            flag = true;
            if (rpt1.Items.Count > 0)
            {
                fr1.Visible = true;
            }
        }
        for (int i = 0; i < rpt1.Items.Count; i++)
        {
            CheckBox chk = (CheckBox)rpt1.Items[i].FindControl("chk");
            chk.Checked = flag;
        }
    }
    protected void lnkDeleteAll_Click(object sender, EventArgs e)
    {
        Panel3_ModalPopupExtender.Show();
        lnkNo1.Focus();
    }

    private void deleteAll()
    {
        string msg = "";
        for (int i = 0; i < rpt1.Items.Count; i++)
        {
            Label lblid = (Label)rpt1.Items[i].FindControl("lblDelete");
            Label lblimgName = (Label)rpt1.Items[i].FindControl("lblimgName");

            List<SqlParameter> param = new List<SqlParameter>();

            param.Add(new SqlParameter("@QueryFor", "D"));
            param.Add(new SqlParameter("@Id", lblid.Text.Trim()));
            param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));

            SqlParameter para = new SqlParameter("@Msg", "");
            para.Direction = ParameterDirection.Output;
            para.Size = 0x100;
            param.Add(para);

            msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("ClassWiseActivityAlbum_Proc", param);

            if (msg == "S")
            {
                if (File.Exists(Server.MapPath(lblimgName.Text)))
                {
                    // ReSharper disable once AssignNullToNotNullAttribute
                    File.Delete(Server.MapPath(lblimgName.Text));
                }
            }
        }

        if (msg == "S")
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Deleted successfully.", "S");
            ChkAll.Checked = false;
            fr1.Visible = false;
        }
        else
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, msg, "W");
        }

        loadGrid("-1", drpClass.SelectedValue.ToString(), drpSection.SelectedValue.ToString(), drpBranch.SelectedValue.ToString());
    }
    protected void lnkYes1_Click(object sender, EventArgs e)
    {
        deleteAll();
    }
    protected void drpSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        fr1.Visible = false;
        ChkAll.Checked = false;
        loadGrid("-1", drpClass.SelectedValue.ToString(), drpSection.SelectedValue.ToString(), drpBranch.SelectedValue.ToString());
        drpSection.Focus();
    }
    protected void drpBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        fr1.Visible = false;
        ChkAll.Checked = false;
        loadGrid("-1", drpClass.SelectedValue.ToString(), drpSection.SelectedValue.ToString(), drpBranch.SelectedValue.ToString());
        drpBranch.Focus();
    }
}