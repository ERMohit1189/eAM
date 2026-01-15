using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace _1
{
    public partial class Admin1SectionMaster : System.Web.UI.Page
    {
        private SqlConnection _con;
        private readonly Campus _oo;
        private string _sql = "";
        public Admin1SectionMaster()
        {
            _con = new SqlConnection();
            _oo = new Campus();
        }

        protected void Page_Load(object sender, EventArgs e)
        {


            if ((string)Session["LoginName"] == "" || (string)Session["BranchCode"] == "" || (string)Session["SessionName"] == "" || Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
            {
                Response.Redirect("default.aspx");
            }
            _con = _oo.dbGet_connection();
            Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file

            //con.ConnectionString = ConfigurationManager.ConnectionStrings["Menu"].ConnectionString;
            if (!IsPostBack)
            {
                DrpClass.Focus();
                try
                {
                    CheckValueAddDeleteUpdate();
                }
                catch (Exception)
                {
                    // ignored
                }

                _sql = "select ROW_NUMBER() OVER (ORDER BY secm.ClassNameId ASC) AS SrNo, SecM.Id,CM.ClassName as ClassName,SecM.SectionName,SecM.RoomNo,SecM.Location,SecM.SectionCode,SecM.Display from SectionMaster SecM";
                _sql +=  "   inner join ClassMaster CM on SecM.ClassNameId=CM.Id and SecM.SessionName=cm.SessionName and SecM.BranchCode=cm.BranchCode where SecM.SessionName='" + Session["SessionName"] + "' and Secm.BranchCode='" + Session["BranchCode"] +"'";
                _sql +=  "  order by ClassNameId";
                Grd.DataSource = _oo.GridFill(_sql);
                Grd.DataBind();
                for (int i = 0; i < Grd.Rows.Count; i++)
                {
                    Label id = (Label)Grd.Rows[i].FindControl("id");
                    LinkButton lnkEdit = (LinkButton)Grd.Rows[i].FindControl("LinkButton2");
                    LinkButton LinkButton3 = (LinkButton)Grd.Rows[i].FindControl("LinkButton3");

                    _sql = "select * from AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") where SectionId=" + id.Text + "";
                    if (_oo.Duplicate(_sql))
                    {
                        lnkEdit.Text = "<i class='fa fa-lock'></i>";
                        lnkEdit.Enabled = false;
                        LinkButton3.Text = "<i class='fa fa-lock'></i>";
                        LinkButton3.Enabled = false;
                    }
                }
                _sql = "Select id, ClassName from ClassMaster";
                _sql +=  "  where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                _sql +=  "  order by Id";
                _oo.FillDropDown_withValue(_sql, DrpClass, "ClassName", "id");
                DrpClass.Items.Insert(0, "All");
                _oo.FillDropDown_withValue(_sql, DrpClassPanel, "ClassName", "id");
            }
            //try
            //{
            //    Grd.FooterRow.Visible = false;
            //}
            //catch (Exception)
            //{
            //    // ignored
            //}
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            _sql = "select SectionName from SectionMaster where SectionName='" + txtSection.Text.Trim() + "' and ClassNameId=" + DrpClass.SelectedValue.Trim() + "";
            _sql +=  " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            if (_oo.Duplicate(_sql))
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Duplicate Entry!", "A");
                return;
            }
            else
            {
                if (DrpClass.SelectedItem.Text== "<--Select-->")
                {
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Please select class!", "A");
                    return;
                }
                else
                {
                    if(DrpClass.SelectedIndex==0)
                    {
                        for (var i = 1; i < DrpClass.Items.Count; i++)
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                cmd.CommandText = "SectionMasterProc";
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Connection = _con;
                                string classId;
                                _sql = "select Id from ClassMaster where ClassName='" + DrpClass.Items[i].Text + "'";
                                _sql +=  " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                                classId = _oo.ReturnTag(_sql, "Id");
                                cmd.Parameters.AddWithValue("@ClassNameId", classId.Trim());
                                cmd.Parameters.AddWithValue("@SectionName", txtSection.Text.Trim());
                                cmd.Parameters.AddWithValue("@RoomNo", txtRoomNo.Text.Trim());
                                cmd.Parameters.AddWithValue("@Location", TxtLocation.Text.Trim());
                                cmd.Parameters.AddWithValue("@Remark", TxtRemark.Text.Trim());
                                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
                                cmd.Parameters.AddWithValue("@SectionCode", txtsectioncode.Text.Trim());
                                cmd.Parameters.AddWithValue("@Display", DropDownList1.SelectedValue);
                                try
                                {
                                    _con.Open();
                                    cmd.ExecuteNonQuery();
                                    _con.Close();
                                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Submitted successfully.", "S");
                                    _sql = "select ROW_NUMBER() OVER (ORDER BY secm.ClassNameId ASC) AS SrNo, SecM.Id,CM.ClassName as ClassName,SecM.SectionName,SecM.RoomNo,SecM.Location,SecM.SectionCode,SecM.Display from SectionMaster SecM";
                                    _sql +=  "   inner join ClassMaster CM on SecM.ClassNameId=CM.Id and SecM.SessionName=cm.SessionName and SecM.BranchCode=cm.BranchCode where SecM.SessionName='" + Session["SessionName"] + "' and Secm.BranchCode=" + Session["BranchCode"] + "";
                                    _sql +=  "  order by ClassNameId";
                                    Grd.DataSource = _oo.GridFill(_sql);
                                    Grd.DataBind();
                                    DrpClass.Focus();
                                    for (int j = 0; j < Grd.Rows.Count; j++)
                                    {
                                        Label id = (Label)Grd.Rows[j].FindControl("id");
                                        LinkButton lnkEdit = (LinkButton)Grd.Rows[j].FindControl("LinkButton2");
                                        LinkButton LinkButton3 = (LinkButton)Grd.Rows[j].FindControl("LinkButton3");

                                        _sql = "select * from AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") where SectionId=" + id.Text + "";
                                        if (_oo.Duplicate(_sql))
                                        {
                                            lnkEdit.Text = "<i class='fa fa-lock'></i>";
                                            lnkEdit.Enabled = false;
                                            LinkButton3.Text = "<i class='fa fa-lock'></i>";
                                            LinkButton3.Enabled = false;
                                        }
                                    }
                                }
                                catch (Exception)
                                {
                                }
                            }
                        }
                        _oo.ClearControls(Page);


                    }
                    else
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.CommandText = "SectionMasterProc";
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Connection = _con;
                            string classId;
                            _sql = "select Id from ClassMaster where ClassName='" + DrpClass.SelectedItem + "'";
                            _sql +=  " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                            classId = _oo.ReturnTag(_sql, "Id");
                            cmd.Parameters.AddWithValue("@ClassNameId", classId);
                            cmd.Parameters.AddWithValue("@SectionName", txtSection.Text);
                            cmd.Parameters.AddWithValue("@RoomNo", txtRoomNo.Text);
                            cmd.Parameters.AddWithValue("@Location", TxtLocation.Text);
                            cmd.Parameters.AddWithValue("@Remark", TxtRemark.Text);
                            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
                            cmd.Parameters.AddWithValue("@SectionCode", txtsectioncode.Text);
                            cmd.Parameters.AddWithValue("@Display", DropDownList1.SelectedValue);
                            try
                            {
                                _con.Open();
                                cmd.ExecuteNonQuery();
                                _con.Close();
                                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Submitted successfully.", "S");
                                _oo.ClearControls(Page);
                                _sql = "select ROW_NUMBER() OVER (ORDER BY secm.ClassNameId ASC) AS SrNo, SecM.Id,CM.ClassName as ClassName,SecM.SectionName,SecM.RoomNo,SecM.Location,SecM.SectionCode,SecM.Display from SectionMaster SecM";
                                _sql +=  "   inner join ClassMaster CM on SecM.ClassNameId=CM.Id and SecM.SessionName=cm.SessionName and SecM.BranchCode=cm.BranchCode where SecM.SessionName='" + Session["SessionName"] + "' and Secm.BranchCode=" + Session["BranchCode"] + "";
                                _sql +=  "  order by ClassNameId";
                                Grd.DataSource = _oo.GridFill(_sql);
                                Grd.DataBind();
                                DrpClass.Focus();
                                for (int i = 0; i < Grd.Rows.Count; i++)
                                {
                                    Label id = (Label)Grd.Rows[i].FindControl("id");
                                    LinkButton lnkEdit = (LinkButton)Grd.Rows[i].FindControl("LinkButton2");
                                    LinkButton LinkButton3 = (LinkButton)Grd.Rows[i].FindControl("LinkButton3");

                                    _sql = "select * from AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") where SectionId=" + id.Text + "";
                                    if (_oo.Duplicate(_sql))
                                    {
                                        lnkEdit.Text = "<i class='fa fa-lock'></i>";
                                        lnkEdit.Enabled = false;
                                        LinkButton3.Text = "<i class='fa fa-lock'></i>";
                                        LinkButton3.Enabled = false;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                // ignored
                            }
                        }
                    }
               
                }
            }
        }
        protected void  DrpClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            _sql = "select ROW_NUMBER() OVER (ORDER BY secm.ClassNameId ASC) AS SrNo, SecM.Id,CM.ClassName as ClassName,SecM.SectionName,SecM.RoomNo,SecM.Location,SecM.SectionCode,SecM.Display from SectionMaster SecM";
            _sql +=  "   inner join ClassMaster CM on SecM.ClassNameId=CM.Id and SecM.SessionName=CM.SessionName and SecM.BranchCode=CM.BranchCode";
            _sql +=  " where  SecM.SessionName='" + Session["SessionName"] + "' and Secm.BranchCode=" + Session["BranchCode"] + " and CM.ClassName='"+DrpClass.SelectedItem +"'";
            _sql +=  "  order by ClassNameId";
            Grd.DataSource = _oo.GridFill(_sql);
            Grd.DataBind();
            for (int i = 0; i < Grd.Rows.Count; i++)
            {
                Label id = (Label)Grd.Rows[i].FindControl("id");
                LinkButton lnkEdit = (LinkButton)Grd.Rows[i].FindControl("LinkButton2");
                LinkButton LinkButton3 = (LinkButton)Grd.Rows[i].FindControl("LinkButton3");

                _sql = "select * from AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") where SectionId=" + id.Text + "";
                if (_oo.Duplicate(_sql))
                {
                    lnkEdit.Text = "<i class='fa fa-lock'></i>";
                    lnkEdit.Enabled = false;
                    LinkButton3.Text = "<i class='fa fa-lock'></i>";
                    LinkButton3.Enabled = false;
                }
            }
        }


        protected void Button3_Click(object sender, EventArgs e)
        {

            _sql = "select SectionCode from SectionMaster where SectionName='" + txtSectionPanel.Text.Trim() + "' and  ClassNameId='" + DrpClassPanel.SelectedValue + "'";
            _sql +=  " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and id <>"+ lblID.Text + "";
            if (_oo.Duplicate(_sql))
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Duplicate Section Name!", "A");
                return;
            }
            _sql = "select SectionCode from SectionMaster where SectionCode='" + txtSectionCodePanel.Text + "' and  ClassNameId='" + DrpClassPanel.SelectedValue + "'";
            _sql +=  " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and id <>" + lblID.Text + "";
            if (_oo.Duplicate(_sql))
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Duplicate Section Code!", "A");
                return;
            }
            string classId;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "SectionMasterUpdateProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = _con;
                _sql = "select Id from ClassMaster where ClassName='" + DrpClassPanel.SelectedItem + "'";
                classId = _oo.ReturnTag(_sql, "Id");
                cmd.Parameters.AddWithValue("@id", lblID.Text);
                cmd.Parameters.AddWithValue("@ClassNameId", classId);
                cmd.Parameters.AddWithValue("@SectionName", txtSectionPanel.Text);
                cmd.Parameters.AddWithValue("@RoomNo", txtRoomNoPanel.Text);
                cmd.Parameters.AddWithValue("@Location", txtLocaPanel.Text);
                cmd.Parameters.AddWithValue("@Remark", txtRemarkPanel.Text);
                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
                cmd.Parameters.AddWithValue("@SectionCode", txtSectionCodePanel.Text);
                cmd.Parameters.AddWithValue("@Display", DropDownList2.SelectedValue);
                try
                {
                    _con.Open();
                    cmd.ExecuteNonQuery();
                    _con.Close();
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Updated Sucessfully.", "S");
                    _oo.ClearControls(Page);
                    _sql = "select ROW_NUMBER() OVER (ORDER BY secm.ClassNameId ASC) AS SrNo, SecM.Id,CM.ClassName as ClassName,SecM.SectionName,SecM.RoomNo,SecM.Location,SecM.SectionCode,SecM.Display from SectionMaster SecM";
                    _sql +=  "   inner join ClassMaster CM on SecM.ClassNameId=CM.Id and SecM.SessionName=cm.SessionName and SecM.BranchCode=cm.BranchCode where SecM.SessionName='" + Session["SessionName"] + "' and Secm.BranchCode=" + Session["BranchCode"] + "";
                    _sql +=  "  order by ClassNameId";
                    Grd.DataSource = _oo.GridFill(_sql);
                    Grd.DataBind();
                    for (int i = 0; i < Grd.Rows.Count; i++)
                    {
                        Label id = (Label)Grd.Rows[i].FindControl("id");
                        LinkButton lnkEdit = (LinkButton)Grd.Rows[i].FindControl("LinkButton2");
                        LinkButton LinkButton3 = (LinkButton)Grd.Rows[i].FindControl("LinkButton3");
                        _sql = "select * from AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") where SectionId=" + id.Text + "";
                        if (_oo.Duplicate(_sql))
                        {
                            lnkEdit.Text = "<i class='fa fa-lock'></i>";
                            lnkEdit.Enabled = false;
                            LinkButton3.Text = "<i class='fa fa-lock'></i>";
                            LinkButton3.Enabled = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            _sql = "Delete from SectionMaster where Id=" + lblvalue.Text;
            _sql +=  " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";

            SqlCommand cmd = new SqlCommand();
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

                _sql = "select ROW_NUMBER() OVER (ORDER BY secm.ClassNameId ASC) AS SrNo, SecM.Id,CM.ClassName as ClassName,SecM.SectionName,SecM.RoomNo,SecM.Location,SecM.SectionCode,SecM.Display from SectionMaster SecM";
                _sql +=  "   inner join ClassMaster CM on SecM.ClassNameId=CM.Id and SecM.SessionName=cm.SessionName and SecM.BranchCode=cm.BranchCode where SecM.SessionName='" + Session["SessionName"] + "' and Secm.BranchCode=" + Session["BranchCode"] + "";
                _sql +=  "  order by ClassNameId";
                Grd.DataSource = _oo.GridFill(_sql);
                Grd.DataBind();
                for (int i = 0; i < Grd.Rows.Count; i++)
                {
                    Label id = (Label)Grd.Rows[i].FindControl("id");
                    LinkButton lnkEdit = (LinkButton)Grd.Rows[i].FindControl("LinkButton2");
                    LinkButton LinkButton3 = (LinkButton)Grd.Rows[i].FindControl("LinkButton3");

                    _sql = "select * from AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") where SectionId=" + id.Text + "";
                    if (_oo.Duplicate(_sql))
                    {
                        lnkEdit.Text = "<i class='fa fa-lock'></i>";
                        lnkEdit.Enabled = false;
                        LinkButton3.Text = "<i class='fa fa-lock'></i>";
                        LinkButton3.Enabled = false;
                    }
                }

            }
            catch (Exception)
            {
                // ignored
            }
        }
        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            Button8.Focus();
            LinkButton chk = (LinkButton)sender;
            Label lblId = (Label)chk.NamingContainer.FindControl("Label37");
            string ss = lblId.Text;
            lblvalue.Text = ss;
            Panel2_ModalPopupExtender.Show();
        }
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton chk = (LinkButton)sender;
                Label lblId2 = (Label)chk.NamingContainer.FindControl("Label36");
                string ss = lblId2.Text;
                lblID.Text = ss;

                _sql = "select * from SectionMaster where Id=" + ss+" and SessionName='"+Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";

                DrpClassPanel.SelectedValue = _oo.ReturnTag(_sql, "ClassNameId");
                txtSectionPanel.Text = _oo.ReturnTag(_sql, "SectionName");
                txtSectionCodePanel.Text = _oo.ReturnTag(_sql, "SectionCode");
                txtRoomNoPanel.Text = _oo.ReturnTag(_sql, "RoomNo");
                txtLocaPanel.Text = _oo.ReturnTag(_sql, "Location");
                txtRemarkPanel.Text = _oo.ReturnTag(_sql, "Remark");
                DropDownList2.SelectedValue = _oo.ReturnTag(_sql, "Display");

                Panel1_ModalPopupExtender.Show();
            }
            catch (Exception)
            {
                // ignored
            }
        }
        protected void Button4_Click(object sender, EventArgs e)
        {

        }
        protected void Button8_Click(object sender, EventArgs e)
        {

        }

        public void PermissionGrant(int add1, int delete1, int update1, LinkButton ladd, Button ldelete, Button lUpdate)
        {


            if (add1 == 1)
            {
                ladd.Enabled = true;
            }
            else
            {
                ladd.Enabled = false;
            }


            if (delete1 == 1)
            {
                ldelete.Enabled = true;
            }
            else
            {
                ldelete.Enabled = false;
            }

            if (update1 == 1)
            {
                lUpdate.Enabled = true;
            }
            else
            {
                lUpdate.Enabled = false;
            }


        }
        public void CheckValueAddDeleteUpdate()
        {
            _sql = " select LoginId,LoginName,Pass,SessionId,BranchId,LT.LoginTypeName,ltb.add1 as add1,ltb.delete1 as delete1,ltb.update1 as update1 from LoginTab LTb";
            _sql +=  " left join LoginType Lt on LTb.LoginTypeId=Lt.LoginTypeId where LT.LoginTypeName='" + Session["Logintype"] + "' and LTb.LoginName='" + Session["LoginName"] + "' and LTb.BranchCode="+Session["BranchCode"] +"";
            int a, u, d;
            a = Convert.ToInt32(_oo.ReturnTag(_sql, "add1"));
            u = Convert.ToInt32(_oo.ReturnTag(_sql, "update1"));
            d = Convert.ToInt32(_oo.ReturnTag(_sql, "delete1"));

            // ReSharper disable once RedundantCast
            PermissionGrant(a, d, u, (LinkButton)LinkButton1, btnDelete, Button3);
        }

        protected void Grd_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grd.FooterRow.Visible = false;
        }
        protected void DrpClassPanel_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public override void Dispose()
        {
            _con.Dispose();
            _oo.Dispose();
        }
    }
}