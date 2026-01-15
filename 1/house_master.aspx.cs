using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace _1
{
    public partial class AdminHouseMaster : System.Web.UI.Page
    {
        private SqlConnection _con;
        private readonly Campus _oo;
        private string _sql = String.Empty;
        public AdminHouseMaster()
        {
            _con = new SqlConnection();
            _oo = new Campus();
        }
        protected void Page_PreInIt(object sender, EventArgs e)
        {
            if (Session["Logintype"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            if (Session["Logintype"].ToString() == "SuperAdmin")
            {
                MasterPageFile = "~/50/sadminRootManager.master";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            _con = _oo.dbGet_connection();
            Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file

            if (!IsPostBack)
            {
                try
                {
                    CheckValueAddDeleteUpdate();
                }
                catch (Exception)
                {
                    // ignored
                }
                string sql = "Select BranchId, BranchName from Branchtab ";
                var dt = _oo.Fetchdata(sql);
                ddlBranch.DataSource = dt;
                ddlBranch.DataTextField = "BranchName";
                ddlBranch.DataValueField = "BranchId";
                ddlBranch.DataBind();
                ddlBranch.Items.Insert(0, new ListItem("<--Select-->", ""));
                if (Session["LoginType"].ToString() == "Admin")
                {
                    divBranch.Visible = false;
                    ddlBranch.SelectedValue = Session["BranchCode"].ToString();
                    load();
                }
                if (Session["LoginType"].ToString() == "SuperAdmin")
                {
                    //divBranch.Visible = false;
                    //ddlBranch.SelectedValue = Session["BranchCode"].ToString();
                }
                else
                {
                    load();
                }

            

            }

        }
        protected void load()
        {
            _sql = "Select  ROW_NUMBER() OVER (ORDER BY Id ASC) AS SrNo,Id, HouseName,Color,Remark from HouseMaster where  BranchCode=" + Session["BranchCode"] + "";
            //sql +=  " where  SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            Grd.DataSource = _oo.GridFill(_sql);
            Grd.DataBind();
        }
        protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["BranchCode"] = ddlBranch.SelectedValue;
            _sql = "Select  ROW_NUMBER() OVER (ORDER BY Id ASC) AS SrNo,Id, HouseName,Color,Remark from HouseMaster where  BranchCode=" + ddlBranch.SelectedValue + "";
            //sql +=  " where  SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            Grd.DataSource = _oo.GridFill(_sql);
            Grd.DataBind();
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {

            //@HouseName nvarchar(100),@Color nvarchar(50),@Remark nvarchar(500),@BranchCode nvarchar(50),@LoginName nvarchar(50),@SessionName nvarchar(50))  
            _sql = "Select HouseName from HouseMaster where HouseName='" + txtName.Text + "' and BranchCode=" + Session["BranchCode"] + "";
            // sql +=  " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";


            if (_oo.Duplicate(_sql))
            {
                //oo.MessageBox("Already Allotted!", this.Page);
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Already Allotted!", "A");                                    
            }

            else
            {



                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "HouseMasterProc";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = _con;
                    cmd.Parameters.AddWithValue("@HouseName", txtName.Text);
                    cmd.Parameters.AddWithValue("@Color", txtColour.Text);
                    cmd.Parameters.AddWithValue("@Remark", txtremark.Text);
                    cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());//Session["Login"].ToString());
                    cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                    cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                    try
                    {
                        _con.Open();
                        cmd.ExecuteNonQuery();
                        _con.Close();
                        //oo.MessageBox("Submitted successfully.", this.Page);
                        Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Submitted successfully.", "S");
                        _oo.ClearControls(Page);
                        _sql = "Select  ROW_NUMBER() OVER (ORDER BY Id ASC) AS SrNo,Id, HouseName,Color,Remark from HouseMaster where BranchCode=" + Session["BranchCode"] + "";
                        //   sql +=  " where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
                        Grd.DataSource = _oo.GridFill(_sql);
                        Grd.DataBind();
                        txtName.Focus();

                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                }

            }
        }
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            LinkButton chk = (LinkButton)sender;
            Label lblId = (Label)chk.NamingContainer.FindControl("Label36");
            string ss = lblId.Text;
            lblID.Text = ss;

            // sql = "Select  distinct ROW_NUMBER() OVER (ORDER BY p.ProductId ASC) AS  [ProductId] ,po.id as ID, pc.ProductCategoryName as[ProductCategoryName], p.ProductName as [ProductName], Pm.ProductTypeName as [ProductTypeName],PO.ProductModelName as [ProductModelName] from Productcategorymaster pc left join ProductName p on p.ProductId=pc.ProductId  left join ProductTypeMaster PM on p.ProductId=PM.ProductId left join ProductModelMaster PO on p.ProductId=PO.ProductId ";        
            _sql = "Select  ROW_NUMBER() OVER (ORDER BY Id ASC) AS SrNo,Id, HouseName,Color,Remark from HouseMaster";
            _sql +=  " where  BranchCode=" + Session["BranchCode"] + " and Id=" + ss;
            //  sql +=  " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            txtNameHousePanel.Text = _oo.ReturnTag(_sql, "HouseName");
            txtColourPanel.Text = _oo.ReturnTag(_sql, "Color");
            txtRemarkPanel.Text = _oo.ReturnTag(_sql, "Remark");
            Panel1_ModalPopupExtender.Show();
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
        protected void Button3_Click(object sender, EventArgs e)
        {

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "HouseMasterUpdateProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = _con;
                cmd.Parameters.AddWithValue("@id", lblID.Text);
                cmd.Parameters.AddWithValue("@HouseName", txtNameHousePanel.Text);
                cmd.Parameters.AddWithValue("@Color", txtColourPanel.Text);
                cmd.Parameters.AddWithValue("@Remark", txtRemarkPanel.Text);
                try
                {
                    _con.Open();
                    cmd.ExecuteNonQuery();
                    _con.Close();
                    //oo.MessageBox("Updated successfully.", this.Page);
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Updated successfully.", "S");
                    _oo.ClearControls(Page);
                    _sql = "Select  ROW_NUMBER() OVER (ORDER BY Id ASC) AS SrNo,Id, HouseName,Color,Remark from HouseMaster Where  BranchCode=" + Session["BranchCode"] + "";
                    // sql +=  " where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
                    Grd.DataSource = _oo.GridFill(_sql);
                    Grd.DataBind();
                    txtName.Focus();
                }
                catch (Exception)
                {
                    // ignored
                }
            }
        }
        protected void Button4_Click(object sender, EventArgs e)
        {

        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            _sql = "Delete from HouseMaster where Id=" + lblvalue.Text + " and BranchCode=" + Session["BranchCode"] + "";
            // sql +=  " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

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
                    _sql = "Select  ROW_NUMBER() OVER (ORDER BY Id ASC) AS SrNo,Id, HouseName,Color,Remark from HouseMaster where  BranchCode=" + Session["BranchCode"] + "";
                    //  sql +=  " where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

                    Grd.DataSource = _oo.GridFill(_sql);
                    Grd.DataBind();
                    txtName.Focus();
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
            _sql +=  " left join LoginType Lt on LTb.LoginTypeId=Lt.LoginTypeId where LT.LoginTypeName='" + Session["Logintype"] + "' and LTb.LoginName='" + Session["LoginName"] + "' and LTb.BranchId=" + Session["BranchCode"] + "";
            int a, u, d;
            a = Convert.ToInt32(_oo.ReturnTag(_sql, "add1"));
            u = Convert.ToInt32(_oo.ReturnTag(_sql, "update1"));
            d = Convert.ToInt32(_oo.ReturnTag(_sql, "delete1"));

            // ReSharper disable once RedundantCast
            PermissionGrant(a, d, u, (LinkButton)LinkButton1, btnDelete, Button3);
        }

        public override void Dispose()
        {
            _con.Dispose();
            _oo.Dispose();
        }
    }
}