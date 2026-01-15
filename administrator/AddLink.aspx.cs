using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_AddMenuPages : System.Web.UI.Page
{
    private SqlConnection _con;
    private readonly Campus _oo;
    private string _sql, _sql1 = String.Empty;

   
    public admin_AddMenuPages()
    {
        _con = new SqlConnection();
        _oo = new Campus();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        //{
        //    Response.Redirect("default.aspx");
        //}
        //con.ConnectionString = ConfigurationManager.ConnectionStrings["Menu"].ConnectionString;
        _con = _oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file

        if (!IsPostBack)
        {
            drpCourse.Focus();
            try
            {
                CheckValueAddDeleteUpdate();
            }
            catch (Exception)
            {
                // ignored
            }

            LoadParent();
            LoadData();
         
        }
    }

    private void LoadData()
    {
        _sql = "Select  ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS RowNumber,(case when Isnull(ParentID,'')='' then 'Self' else (Select Text from Menueam where MenuID=wm.ParentID ) end )ParentMenuName,* from Menueam wm where Status=1";
        if(drpCourse.SelectedValue=="0")
        {
            _sql += " and Isnull(wm.ParentID,0)=0";
        }
        if (DropDownList1.SelectedValue != "0" && DropDownList3.SelectedValue == "0")
        {
            _sql += " and Isnull(wm.ParentID,0)=" + DropDownList1.SelectedValue+ "";
        }
        if (DropDownList1.SelectedValue != "0" && DropDownList3.SelectedValue != "0")
        {
            _sql += " and Isnull(wm.ParentID,0)=" + DropDownList3.SelectedValue + "";
        }
        if (DropDownList3.SelectedValue != "0" && DropDownList1.SelectedValue == "0")
        {
            _sql += " and Isnull(wm.ParentID,0)=" + DropDownList3.SelectedValue + "";
        }

        Grd.DataSource = _oo.GridFill(_sql);
        Grd.DataBind();

    }

    protected void LoadParent()
    {
        _sql = "Select MenuID,Text from Menueam Where Isnull(ParentID,'0')=0";
        _oo.FillDropDown_withValue(_sql, DropDownList1, "Text", "MenuID");
        _oo.FillDropDown_withValue(_sql, DropDownList2, "Text", "MenuID");
        DropDownList1.Items.Insert(0, new ListItem("<--Select-->", "0"));
        DropDownList2.Items.Insert(0, new ListItem("<--Select-->", "0"));

    }
    protected void LoadSubParent(string ParentID)
    {
        _sql = "Select MenuID,Text from Menueam Where Isnull(ParentID,'0')='"+ParentID+"'";
        _oo.FillDropDown_withValue(_sql, DropDownList3, "Text", "MenuID");
       // _oo.FillDropDown_withValue(_sql, DropDownList2, "Text", "MenuID");
        DropDownList3.Items.Insert(0, new ListItem("<--Select-->", "0"));
        //DropDownList2.Items.Insert(0, new ListItem("<--Select-->", "0"));

    }
    protected void LoadEditSubParent(string ParentID)
    {
        _sql = "Select MenuID,Text from Menueam Where Isnull(ParentID,'0')='" + ParentID + "'";
        _oo.FillDropDown_withValue(_sql, DropDownList4, "Text", "MenuID");
        // _oo.FillDropDown_withValue(_sql, DropDownList2, "Text", "MenuID");
        DropDownList4.Items.Insert(0, new ListItem("<--Select-->", "0"));
        //DropDownList2.Items.Insert(0, new ListItem("<--Select-->", "0"));

    }


    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        string parentID = "0";
        if(DropDownList1.SelectedValue!="-1")
        {
            if(DropDownList3.SelectedValue.ToString()=="0")
            {
                parentID = DropDownList1.SelectedValue.ToString();
            }
            else
            {
                parentID = DropDownList3.SelectedValue.ToString();
            }
           
        }
       
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = "SaveMenuam";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = _con;
            cmd.Parameters.AddWithValue("@Text", txtclassname.Text.Trim().Replace("  ", " "));
            cmd.Parameters.AddWithValue("@ParentId", parentID);
            cmd.Parameters.AddWithValue("@Url", txtclassCode.Text.Trim()); //txtLocation.Text
            cmd.Parameters.AddWithValue("@Action", "Insert");
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
        _sql = "Select IsNull(ParentID,'0') as ParentIDs, ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS RowNumber,(case when Isnull(ParentID,'')='' then 'Self' else (Select Text from Menueam where MenuID=wm.ParentID ) end )ParentMenuName,(case when Isnull(ParentID,'0')='0' then '0' else (Select Isnull(ParentID,'0') from Menueam where MenuID=wm.ParentID ) end )as MainMenuParentID, Isnull(ParentID,'0') as subMainMenuID,* from Menueam wm where Status=1";
        _sql += " and MenuID=" + ss;

        string parentID = _oo.ReturnTag(_sql, "ParentMenuName");
        if(parentID== "Self")
        {
            MenuParentEditdiv.Visible = false;
            drpPanelCourse.SelectedValue = "1";
        }
        else
        {
            drpPanelCourse.SelectedValue = "2";
            MenuParentEditdiv.Visible = true;
            string mainParentID = _oo.ReturnTag(_sql, "MainMenuParentID");
            string mainsubParentID = _oo.ReturnTag(_sql, "subMainMenuID");
            txtClassCodePanel.Text = _oo.ReturnTag(_sql, "Url");
            txtClassNamePanel.Text = _oo.ReturnTag(_sql, "Text");
            MenuIDMainEdit.Value = ss;
            if (mainParentID != "0")
            {
                DropDownList2.SelectedValue = mainParentID;
                LoadEditSubParent(mainParentID);
                if (mainsubParentID == "0" || mainsubParentID == "-1")
                {
                    DropDownList4.SelectedValue = "0";
                }
                else
                {
                    DropDownList4.SelectedValue = mainsubParentID;
                }
                Tr1.Visible = true;
            }
            else
            {
                if (mainsubParentID == "0" || mainsubParentID == "-1")
                {
                    DropDownList2.SelectedValue = "0";
                }
                else
                {
                    DropDownList2.SelectedValue = mainsubParentID;
                }
                //DropDownList2.SelectedValue = "0";
                Tr1.Visible = false;
            }
                // DropDownList2.SelectedValue= _oo.ReturnTag(_sql, "ParentID");
            }
        



        Panel1_ModalPopupExtender.Show();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        string parentID = "0";
        if (DropDownList2.SelectedValue != "-1")
        {
            if (DropDownList4.SelectedValue.ToString() == "0")
            {
                parentID = DropDownList2.SelectedValue.ToString();
            }
            else
            {
                parentID = DropDownList4.SelectedValue.ToString();
            }
           // parentID = DropDownList2.SelectedValue.ToString();
        }
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = "SaveMenuam";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = _con;
            cmd.Parameters.AddWithValue("@Id", MenuIDMainEdit.Value);
            cmd.Parameters.AddWithValue("@Text", txtClassNamePanel.Text.Trim().Replace("  ", " "));
            cmd.Parameters.AddWithValue("@ParentId", parentID);
            cmd.Parameters.AddWithValue("@Url", txtClassCodePanel.Text.Trim()); //txtLocation.Text
            cmd.Parameters.AddWithValue("@Action", "Update");

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
        _sql = "Delete from Menueam where MenuID=" + lblvalue.Text;
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
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Deleted successfully.", "S");
                LoadData();
            }
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
        PermissionGrant(a, d, u, (LinkButton)LinkButton1, btnDelete, Button3);
    }
    protected void drpCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(drpCourse.SelectedValue!="-1")
        {
            if (drpCourse.SelectedValue != "0")
            {
                MenuParentdiv.Visible = true;
                Div1.Visible = true;
                LoadData();
            }
            else
            {
                MenuParentdiv.Visible = false;
                Div1.Visible = false;
                LoadData();
            }
        }
        else
        {
            MenuParentdiv.Visible = false;
            Div1.Visible = false;
            LoadData();
        }
        //loadBranch();
    }

    protected void drpPanelCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpPanelCourse.SelectedValue != "-1")
        {
            if (drpPanelCourse.SelectedValue == "2")
            {
                MenuParentEditdiv.Visible = true;
                 Tr1.Visible = true;
            }
            else
            {
                MenuParentEditdiv.Visible = false;
                  Tr1.Visible = false;
            }
        }
        else
        {
            MenuParentEditdiv.Visible = false;
             Tr1.Visible = false;
        }
        //loadPanelBranch();
        Panel1_ModalPopupExtender.Show();
    }

    public override void Dispose()
    {
        _con.Dispose();
        _oo.Dispose();
    }



    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSubParent(DropDownList1.SelectedValue);
        LoadData();
    }

    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        //LoadEditSubParent(DropDownList2.SelectedValue);
        if(DropDownList2.SelectedValue!="0")
        {
            _sql = "Select MenuID,Text from Menueam Where Isnull(ParentID,'0')='" + DropDownList2.SelectedValue + "'";
            _oo.FillDropDown_withValue(_sql, DropDownList4, "Text", "MenuID");
            DropDownList4.Items.Insert(0, new ListItem("<--Select-->", "0"));
            Tr1.Visible = true;
        }
        else
        {
            Tr1.Visible = false;
        }
      
        Panel1_ModalPopupExtender.Show();
    }

    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadData();
    }
}