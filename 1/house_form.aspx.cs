using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace _1
{
    public partial class AdminHouseForm : System.Web.UI.Page
    {
        private SqlConnection _con;
        private readonly Campus _oo;
        private string _sql = String.Empty;
        public AdminHouseForm()
        {
            _con = new SqlConnection();
            _oo = new Campus();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string) Session["LoginName"] == "" || (string)Session["BranchCode"] == "" || (string)Session["SessionName"] == "" || Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
            {
                Response.Redirect("default.aspx");
            }
            _con = _oo.dbGet_connection();
            Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file

            //con.ConnectionString = ConfigurationManager.ConnectionStrings["Menu"].ConnectionString;
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


                _sql = "Select ROW_NUMBER() OVER (ORDER BY HD.Id ASC) AS SrNo,HD.Id, HM.HouseName,HM.Color,HD.HouseCharter,HD.CaptionMale,HD.CaptionFemale,HD.Warden from HouseFormDetail HD left join HouseMaster HM on HD.HouseNameId=HM.Id ";
                _sql = _sql + " where hd.SessionName='" + Session["SessionName"] + "' and hd.BranchCode=" + Session["BranchCode"] + "";
                Grd.DataSource = _oo.GridFill(_sql);
                Grd.DataBind();
                _sql = "Select HouseName from HouseMaster";
                _sql = _sql + " where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                _oo.FillDropDown(_sql, DrphouseName, "HouseName");
                _oo.FillDropDown(_sql, drpHouseNamePanel, "houseName");

            }
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            //HouseNameId int,@HouseColor nvarchar(50),@HouseCharter nvarchar(50),@CaptionMale nvarchar(50),@CaptionFemale nvarchar(50),@Warden nvarchar(50),
            //@BranchCode nvarchar(50),@LoginName nvarchar(50),@SessionName nvarchar(50))

            //  sql = " Select  HouseName  from HouseFormDetail   where HouseName='" + DrphouseName.SelectedItem.ToString() + "'";
            _sql = "Select ROW_NUMBER() OVER (ORDER BY HD.Id ASC) AS SrNo,HD.Id, HM.HouseName,HM.Color,HD.HouseCharter,HD.CaptionMale,HD.CaptionFemale,HD.Warden from HouseFormDetail HD left join HouseMaster HM on HD.HouseNameId=HM.Id ";
            _sql = _sql + " where hd.SessionName='" + Session["SessionName"] + "' and hd.BranchCode=" + Session["BranchCode"] + "";
            _sql = _sql + "   and hm.HouseName='" + DrphouseName.SelectedItem + "'";
            if (_oo.Duplicate(_sql))
            {
                //oo.MessageBox("Already Allotted!", this.Page);
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Already Allotted!", "A");
            }
            else  if (DrphouseName.SelectedItem.Text == "<--Select-->")
            {
                _oo.MessageBox("Select House Name", Page);
            }
            else
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "HouseFormDetailProc";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = _con;
                    _sql = "select Id from HouseMaster where HouseName='" + DrphouseName.SelectedItem + "'";
                    var id = _oo.ReturnTag(_sql, "Id");
                    cmd.Parameters.AddWithValue("@HouseNameId", id);
                    cmd.Parameters.AddWithValue("@HouseColor", txthousecolor.Text);
                    cmd.Parameters.AddWithValue("@HouseCharter", txthosecharter.Text);
                    cmd.Parameters.AddWithValue("@CaptionMale", txtMale.Text);
                    cmd.Parameters.AddWithValue("@CaptionFemale", txtfemale.Text);
                    cmd.Parameters.AddWithValue("@Warden", txtwarden.Text);
                    cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());//Session["Login"].ToString());
                    cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                    cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
                    try
                    {

                        _con.Open();
                        cmd.ExecuteNonQuery();
                        _con.Close();
                        //oo.MessageBox("Submitted successfully.", this.Page);
                        Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Submitted successfully.", "S");
                        _oo.ClearControls(Page);
                        _sql = "Select ROW_NUMBER() OVER (ORDER BY HD.Id ASC) AS SrNo,HD.Id, HM.HouseName,HM.Color,HD.HouseCharter,HD.CaptionMale,HD.CaptionFemale,HD.Warden from HouseFormDetail HD left join HouseMaster HM on HD.HouseNameId=HM.Id ";
                        _sql = _sql + " where hd.SessionName='" + Session["SessionName"] + "' and hd.BranchCode=" + Session["BranchCode"] + "";
                        Grd.DataSource = _oo.GridFill(_sql);
                        Grd.DataBind();

                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                }

            }
        }
        protected void DrphouseName_SelectedIndexChanged(object sender, EventArgs e)
        {
            _sql = "Select  Color from HouseMaster where HouseName='" + DrphouseName.SelectedItem + "'";
            _sql = _sql + " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            txthousecolor.Text= _oo.ReturnTag(_sql, "Color");
        }
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            LinkButton chk = (LinkButton)sender;
            Label lblId = (Label)chk.NamingContainer.FindControl("Label36");
            string ss = lblId.Text;
            lblID.Text = ss;

            // sql = "Select  distinct ROW_NUMBER() OVER (ORDER BY p.ProductId ASC) AS  [ProductId] ,po.id as ID, pc.ProductCategoryName as[ProductCategoryName], p.ProductName as [ProductName], Pm.ProductTypeName as [ProductTypeName],PO.ProductModelName as [ProductModelName] from Productcategorymaster pc left join ProductName p on p.ProductId=pc.ProductId  left join ProductTypeMaster PM on p.ProductId=PM.ProductId left join ProductModelMaster PO on p.ProductId=PO.ProductId ";        
            _sql = "Select ROW_NUMBER() OVER (ORDER BY HD.Id ASC) AS SrNo,HD.Id, HM.HouseName as HouseName,HM.Color as Color,HD.HouseCharter  as HouseCharter,HD.CaptionMale as CaptionMale,HD.CaptionFemale as CaptionFemale,HD.Warden as Warden from HouseFormDetail HD left join HouseMaster HM on HD.HouseNameId=HM.Id ";
            _sql = _sql + " where HD.Id=" + ss;
            _sql = _sql + " and hd.SessionName='" + Session["SessionName"] + "' and hd.BranchCode=" + Session["BranchCode"] + "";
            drpHouseNamePanel.Text = _oo.ReturnTag(_sql, "HouseName");
            txtColorPanel.Text = _oo.ReturnTag(_sql, "Color");
            txtHouseCharterPanel.Text = _oo.ReturnTag(_sql, "HouseCharter");
            txtCaptionMalePanel.Text = _oo.ReturnTag(_sql, "CaptionMale");
            txtCaptionFemale.Text = _oo.ReturnTag(_sql, "CaptionFemale");
            txtWardenPanel.Text = _oo.ReturnTag(_sql, "Warden");
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
                cmd.CommandText = "HouseFormDetailUpdateProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = _con;

                string id;
                _sql = "select Id from HouseMaster where HouseName='" + drpHouseNamePanel.SelectedItem + "'";
                id = _oo.ReturnTag(_sql, "Id");
                cmd.Parameters.AddWithValue("@id", lblID.Text);
                cmd.Parameters.AddWithValue("@HouseNameId", id);
                cmd.Parameters.AddWithValue("@HouseColor", txtColorPanel.Text);
                cmd.Parameters.AddWithValue("@HouseCharter", txtHouseCharterPanel.Text);
                cmd.Parameters.AddWithValue("@CaptionMale", txtCaptionMalePanel.Text);
                cmd.Parameters.AddWithValue("@CaptionFemale", txtCaptionFemale.Text);
                cmd.Parameters.AddWithValue("@Warden", txtWardenPanel.Text);
                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());//Session["Login"].ToString());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
                try
                {
                    _con.Open();
                    cmd.ExecuteNonQuery();
                    _con.Close();
                    //oo.MessageBox("Updated successfully.", this.Page);
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Updated successfully.", "S");
                    _oo.ClearControls(Page);
                    _sql = "Select ROW_NUMBER() OVER (ORDER BY HD.Id ASC) AS SrNo,HD.Id, HM.HouseName,HM.Color,HD.HouseCharter,HD.CaptionMale,HD.CaptionFemale,HD.Warden from HouseFormDetail HD left join HouseMaster HM on HD.HouseNameId=HM.Id ";
                    _sql = _sql + " where hd.SessionName='" + Session["SessionName"] + "' and hd.BranchCode=" + Session["BranchCode"] + "";
                    Grd.DataSource = _oo.GridFill(_sql);
                    Grd.DataBind();

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
            _sql = "Delete from HouseFormDetail where Id=" + lblvalue.Text;
            _sql = _sql + " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";

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
                    _sql = "Select ROW_NUMBER() OVER (ORDER BY HD.Id ASC) AS SrNo,HD.Id, HM.HouseName,HM.Color,HD.HouseCharter,HD.CaptionMale,HD.CaptionFemale,HD.Warden from HouseFormDetail HD left join HouseMaster HM on HD.HouseNameId=HM.Id ";
                    _sql = _sql + " where hd.SessionName='" + Session["SessionName"] + "' and hd.BranchCode=" + Session["BranchCode"] + "";

                    Grd.DataSource = _oo.GridFill(_sql);
                    Grd.DataBind();
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
        protected void drpHouseNamePanel_SelectedIndexChanged(object sender, EventArgs e)
        {
            _sql = "Select  Color from HouseMaster where HouseName='" + drpHouseNamePanel.SelectedItem + "'";
            _sql = _sql + " where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            txtColorPanel.Text = _oo.ReturnTag(_sql, "Color");
        }
        protected void txthousecolor_TextChanged(object sender, EventArgs e)
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
            _sql = _sql + " left join LoginType Lt on LTb.LoginTypeId=Lt.LoginTypeId where LT.LoginTypeName='" + Session["Logintype"] + "' and LTb.LoginName='" + Session["LoginName"] + "'";
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