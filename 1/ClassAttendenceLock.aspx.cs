using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _1_ClassAttendenceLock : System.Web.UI.Page
{
    private SqlConnection _con;
    private readonly Campus _oo;
    private string _sql, _sql1 = String.Empty;
    bool allChecked = true; // 


    //public AdminClassMaster()
    //{
    //    _con = new SqlConnection();
    //    _oo = new Campus();
    //}
    public _1_ClassAttendenceLock()
    {
        _con = new SqlConnection();
            _oo = new Campus();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        //con.ConnectionString = ConfigurationManager.ConnectionStrings["Menu"].ConnectionString;
        _con = _oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file

        if (!IsPostBack)
        {
            LoadData();
           
            //drpCourse.Focus();
            try
            {
                CheckValueAddDeleteUpdate();
            }
            catch (Exception)
            {
                // ignored
            }


            LoadData();
            LoadCourse();
            //loadBranch();
            string sql = "Select defaultvalue from DefaultSelectedValue where defaultvalueof = 'ModeofEducation'";
            if (_oo.Duplicate(sql))
            {
              //  drpModeofEdu.SelectedValue = _oo.ReturnTag(sql, "defaultvalue");
            }
            string sql1 = "Select defaultvalue from DefaultSelectedValue where defaultvalueof = 'SemesterType'";
            if (_oo.Duplicate(sql1))
            {
                //drpSemesterType.SelectedValue = _oo.ReturnTag(sql1, "defaultvalue");
            }
        }

        // Grd.FooterRow.Visible = false;
    }

    private void LoadData()
    {
        _sql = "Select ROW_NUMBER() OVER (ORDER BY cm.Id ASC) AS SrNo, cm.Id,ClassName,RoomNo,Location,ClassCode,CIDOrder,ISNULL(CourseName,'N/A') as CourseName,ISNULL(cm.ModeofEducation,'N/A') as ModeofEducation,ISNULL(SemesterType,'N/A') as SemesterType,cm.IsAttendenceLock from ClassMaster cm";
        _sql += " Left join CourseMaster com on com.Id=cm.Course";
        _sql += " where cm.SessionName='" + Session["SessionName"] + "' and cm.BranchCode=" + Session["BranchCode"] + " and com.BranchCode=" + Session["BranchCode"] + " and CIDOrder !=0 ";
        Grd.DataSource = _oo.GridFill(_sql);
        DataSet dte= _oo.GridFill(_sql);

        DataTable dt = dte.Tables[0];
        Grd.DataBind();
      //  txtSequenceOrder.Text = Convert.ToString(Grd.Rows.Count + 1);
        for (int i = 0; i < Grd.Rows.Count; i++)
        {
            Label classname = (Label)Grd.Rows[i].FindControl("Label2");
            LinkButton LinkButton2 = (LinkButton)Grd.Rows[i].FindControl("LinkButton2");
            _sql = "select * from AttendanceDetailsDateWise where lClassName='" + classname.Text.Trim() + "' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            if (_oo.Duplicate(_sql))
            {
                LinkButton2.Text = "<i class='fa fa-lock'></i>";
                LinkButton2.Enabled = false;
            }
        }
        // Call PreRender manually to check header checkbox
        Grd_RowDataBound_Helper(dt); // custom method for checking
        Grd_PreRender(null, null);   // manually update header checkbox
    }
    private void Grd_RowDataBound_Helper(DataTable dt)
    {
        allChecked = true;
        foreach (DataRow row in dt.Rows)
        {
            if (row["IsAttendenceLock"] == DBNull.Value || Convert.ToBoolean(row["IsAttendenceLock"]) == false)
            {
                allChecked = false;
                break;
            }
        }
    }
    protected void Grd_PreRender(object sender, EventArgs e)
    {
        if (Grd.HeaderRow != null)
        {
            CheckBox chkHeader = (CheckBox)Grd.HeaderRow.FindControl("chkHeader");
            if (chkHeader != null)
            {
                chkHeader.Checked = allChecked;
                Button1.Visible = true;
            }
        }
    }

    protected void LoadCourse()
    {
        _sql = "Select CourseName,Id from CourseMaster where  BranchCode=" + Session["BranchCode"] + " ";
        _oo.FillDropDown_withValue(_sql, drpCourse, "CourseName", "Id");
        drpCourse.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
        _oo.FillDropDown_withValue(_sql, drpPanelCourse, "CourseName", "Id");
        drpPanelCourse.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));

    }

    //protected void loadBranch()
    //{
    //    sql = "Select BranchName,Id from BranchMaster where SessionName='" + Session["SessionName"].ToString() + "' and Course='"+drpCourse.SelectedValue.ToString()+"'";
    //    oo.FillDropDown_withValue(sql, drpBranch, "BranchName", "Id");
    //    drpBranch.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
    //}

    //protected void loadPanelBranch()
    //{
    //    sql = "Select BranchName,Id from BranchMaster where SessionName='" + Session["SessionName"].ToString() + "' and Course='" + drpPanelCourse.SelectedValue.ToString() + "'";
    //    oo.FillDropDown_withValue(sql, drpPanelBranch, "BranchName", "Id");
    //    drpPanelBranch.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
    //}

    protected void LinkButton1_Click(object sender, EventArgs e)
    {



        _sql1 = "select className from classMaster  where ClassCode='" + txtclassCode.Text + "'";
        _sql1 = _sql1 + "  and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";

        _sql = "select className from classMaster  where className='" + txtclassname.Text + "'";
        _sql += "  and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";

        if (_oo.Duplicate(_sql))
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Duplicate Class Name!", "A");
        }
        else if (_oo.Duplicate(_sql1))
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Duplicate Class Code!", "A");
        }
        else
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "ClassMasterProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = _con;
                cmd.Parameters.AddWithValue("@ClassName", txtclassname.Text.Trim().Replace("  ", " "));
                cmd.Parameters.AddWithValue("@ClassCode", txtclassCode.Text.Trim());
                cmd.Parameters.AddWithValue("@RoomNo", "0");
                cmd.Parameters.AddWithValue("@Location", ""); //txtLocation.Text
                cmd.Parameters.AddWithValue("@Remark", txtRemark.Text);
                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());//Session["Login"].ToString());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                cmd.Parameters.AddWithValue("@CIDOrder", txtSequenceOrder.Text.Trim());
                cmd.Parameters.AddWithValue("@Course", drpCourse.SelectedValue.Trim());
                //cmd.Parameters.AddWithValue("@BranchId", drpBranch.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@ModeofEducation", drpModeofEdu.SelectedValue);
                cmd.Parameters.AddWithValue("@SemesterType", drpSemesterType.SelectedValue);
                cmd.Parameters.AddWithValue("@DurationDays", TextBox1.Text);
                cmd.Parameters.AddWithValue("@DurationMonth", TextBox2.Text);
                try
                {
                    _con.Open();
                    cmd.ExecuteNonQuery();
                    _con.Close();
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Submitted successfully.", "S");
                    _oo.ClearControls(Page);
                    LoadData();
                    drpCourse.Focus();
                }
                catch (Exception)
                {
                    // ignored
                }
            }
        }

    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        Button8.Focus();
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("Label37");
        var ss = lblId.Text;
        lblvalue.Text = ss;
        Panel2_ModalPopupExtender.Show();
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("Label36");
        string ss = lblId.Text;
        lblID.Text = ss;

        // sql = "Select  distinct ROW_NUMBER() OVER (ORDER BY p.ProductId ASC) AS  [ProductId] ,po.id as ID, pc.ProductCategoryName as[ProductCategoryName], p.ProductName as [ProductName], Pm.ProductTypeName as [ProductTypeName],PO.ProductModelName as [ProductModelName] from Productcategorymaster pc left join ProductName p on p.ProductId=pc.ProductId  left join ProductTypeMaster PM on p.ProductId=PM.ProductId left join ProductModelMaster PO on p.ProductId=PO.ProductId ";        
        _sql = "Select ROW_NUMBER() OVER (ORDER BY Id ASC) AS SrNo, Id,ClassName,RoomNo,Location,ClassCode,Remark,CIDOrder,Course,DurationDays,DurationMonth from ClassMaster";
        _sql += " where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        _sql += " and id=" + ss;
        TextBox5.Text = _oo.ReturnTag(_sql, "DurationDays");
        TextBox3.Text = _oo.ReturnTag(_sql, "DurationMonth");

        txtClassCodePanel.Text = _oo.ReturnTag(_sql, "ClassCode");
        txtClassNamePanel.Text = _oo.ReturnTag(_sql, "ClassName");

        txtLocationPanel.Text = _oo.ReturnTag(_sql, "Location");
        txtRemarkPanel.Text = _oo.ReturnTag(_sql, "Remark");
        txtSequenceOrderPanel.Text = _oo.ReturnTag(_sql, "CIDOrder");
        drpPanelCourse.SelectedValue = _oo.ReturnTag(_sql, "Course") != "" ? _oo.ReturnTag(_sql, "Course") : "<--Select-->";
        //loadPanelBranch();

        // sql = "Select  distinct ROW_NUMBER() OVER (ORDER BY p.ProductId ASC) AS  [ProductId] ,po.id as ID, pc.ProductCategoryName as[ProductCategoryName], p.ProductName as [ProductName], Pm.ProductTypeName as [ProductTypeName],PO.ProductModelName as [ProductModelName] from Productcategorymaster pc left join ProductName p on p.ProductId=pc.ProductId  left join ProductTypeMaster PM on p.ProductId=PM.ProductId left join ProductModelMaster PO on p.ProductId=PO.ProductId ";        
        _sql = "Select ModeofEducation,SemesterType from ClassMaster";
        _sql += " where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        _sql += " and id=" + ss;

        //drpPanelBranch.SelectedValue = oo.ReturnTag(sql, "BranchId") != "" ? oo.ReturnTag(sql, "BranchId") : "<--Select-->";
        drpPanelModeofEdu.SelectedValue = _oo.ReturnTag(_sql, "ModeofEducation") != "" ? _oo.ReturnTag(_sql, "ModeofEducation") : "<--Select-->";
        drpPanelSemesterType.SelectedValue = _oo.ReturnTag(_sql, "SemesterType") != "" ? _oo.ReturnTag(_sql, "SemesterType") : "<--Select-->";

        Panel1_ModalPopupExtender.Show();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = "ClassMasterUpdateProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", lblID.Text.Trim());
            cmd.Parameters.AddWithValue("@ClassName", txtClassNamePanel.Text.Trim());
            cmd.Parameters.AddWithValue("@ClassCode", txtClassCodePanel.Text.Trim());
            cmd.Parameters.AddWithValue("@RoomNo", "0");
            cmd.Parameters.AddWithValue("@Location", "");
            cmd.Parameters.AddWithValue("@Remark", txtRemarkPanel.Text.Trim());
            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());//Session["Login"].ToString());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
            cmd.Parameters.AddWithValue("@CIDOrder", txtSequenceOrderPanel.Text.Trim());
            cmd.Parameters.AddWithValue("@Course", drpPanelCourse.SelectedValue.Trim());
            //cmd.Parameters.AddWithValue("@BranchId", drpPanelBranch.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@ModeofEducation", drpPanelModeofEdu.SelectedValue);
            cmd.Parameters.AddWithValue("@SemesterType", drpPanelSemesterType.SelectedValue);
            cmd.Parameters.AddWithValue("@DurationDays", TextBox5.Text);
            cmd.Parameters.AddWithValue("@DurationMonth", TextBox3.Text);
            cmd.Connection = _con;
            try
            {
                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
                //oo.MessageBox("Updated successfully.", this.Page);
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Updated successfully.", "S");

                LoadData();
                Panel1_ModalPopupExtender.Hide();
            }
            // ReSharper disable once RedundantCatchClause
            catch (Exception)
            {
                throw;
            }
        }

    }
    protected void Button4_Click(object sender, EventArgs e)
    {

    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        _sql = "Delete from ClassMaster where Id=" + lblvalue.Text;
        _sql += " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";

        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = _sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = _con;
            try
            {
                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
                //oo.MessageBox("Deleted successfully.", this.Page);
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Deleted successfully.", "S");
                LoadData();
            }
            // ReSharper disable once RedundantCatchClause
            catch (Exception)
            {
                throw;
            }
        }

    }
    protected void Button8_Click(object sender, EventArgs e)
    {

    }

    public void PermissionGrant(int add1, int delete1, int update1, LinkButton ladd, Button ldelete, Button lUpdate)
    {
        ladd.Enabled = add1 == 1;
        ldelete.Enabled = delete1 == 1;
        lUpdate.Enabled = update1 == 1;
    }
    public void CheckValueAddDeleteUpdate()
    {
        _sql = " select LoginId,LoginName,Pass,SessionId,BranchId,LT.LoginTypeName,ltb.add1 as add1,ltb.delete1 as delete1,ltb.update1 as update1 from LoginTab LTb";
        _sql += " left join LoginType Lt on LTb.LoginTypeId=Lt.LoginTypeId where LT.LoginTypeName='" + Session["Logintype"] + "' and LTb.LoginName='" + Session["LoginName"] + "'";
        int a, u, d;
        a = Convert.ToInt32(_oo.ReturnTag(_sql, "add1"));
        u = Convert.ToInt32(_oo.ReturnTag(_sql, "update1"));
        d = Convert.ToInt32(_oo.ReturnTag(_sql, "delete1"));

        // ReSharper disable once RedundantCast
       // PermissionGrant(a, d, u, (LinkButton)LinkButton1, btnDelete, Button3);
    }
    protected void drpCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        //loadBranch();
    }

    protected void drpPanelCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        //loadPanelBranch();
        Panel1_ModalPopupExtender.Show();
    }

    public override void Dispose()
    {
        _con.Dispose();
        _oo.Dispose();
    }

    protected void chkRow_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = (CheckBox)sender;
        GridViewRow row = (GridViewRow)chk.NamingContainer;

        // ClassCode ya Id lelo (jo aap DB update ke liye use karte ho)
        string classCode = ((Label)row.FindControl("Label1")).Text; // assuming ClassCode
        bool isLocked = chk.Checked;
        int Isattendence = 0;
        if (isLocked)
        {
            Isattendence = 1;
        }
     

        _sql = "Update ClassMaster Set IsAttendenceLock="+ Isattendence + " where Id=" + classCode;
        _sql += " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";

        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = _sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = _con;
            try
            {
                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
                Campus camp = new Campus();
                if (isLocked)
                {
                    camp.msgbox(Page, msgbox, "Attendece Date Locked successfully.", "S");
                }
                else
                {
                    camp.msgbox(Page, msgbox, "Attendece Date Reset Locked successfully.", "S");
                }
               
                LoadData();
            }
            // ReSharper disable once RedundantCatchClause
            catch (Exception)
            {
                throw;
            }
        }
    }

   
    protected void btnSaveAttendanceLock_Click(object sender, EventArgs e)
    {
        string[] selectedIds = hdnSelectedIds.Value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        foreach (GridViewRow row in Grd.Rows)
        {
            CheckBox chk = (CheckBox)row.FindControl("chkRow");
            Label lblId = (Label)row.FindControl("Label1"); // Assuming this is ID

            //if (chk != null && lblId != null)
            //{

                //Label lblId = (Label)row.FindControl("Label1");
                if (lblId != null && selectedIds.Contains(lblId.Text.Trim()))
                {
                    bool isChecked = chk.Checked;
                int isAttendance = 1 ;
                string id = lblId.Text;
                if (isAttendance==1)
                {
                    string _sql = "Update ClassMaster Set IsAttendenceLock=" + isAttendance + " where Id=" + id;
                    _sql += " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = _sql;
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = _con;
                        try
                        {
                            _con.Open();
                            cmd.ExecuteNonQuery();
                            _con.Close();
                          
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }
                }
               
            }
        }

        Campus camp = new Campus();
        camp.msgbox(Page, msgbox, "All updates saved successfully.", "S");
        LoadData();
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in Grd.Rows)
        {
            CheckBox chk = (CheckBox)row.FindControl("chkRow");
            Label lblId = (Label)row.FindControl("Label1"); // Assuming this is ID
            if (chk != null && lblId != null)
            {
                bool isChecked = chk.Checked;
                int isAttendance = 0;
                string id = lblId.Text;
               // if (isAttendance == 1)
               // {
                    string _sql = "Update ClassMaster Set IsAttendenceLock=" + isAttendance + " where Id=" + id;
                    _sql += " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = _sql;
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = _con;
                        try
                        {
                            _con.Open();
                            cmd.ExecuteNonQuery();
                            _con.Close();

                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }
                //}

            }
        }

        Campus camp = new Campus();
        camp.msgbox(Page, msgbox, "All Attendence Lock Reset successfully.", "S");
        LoadData();
    }
}
