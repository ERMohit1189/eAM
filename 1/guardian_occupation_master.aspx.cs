using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace _1
{
    public partial class AdminGuardianDesignationMaster : System.Web.UI.Page
    {
        private SqlConnection _con;
        private readonly Campus _oo;
        private string _sql = String.Empty;
        public AdminGuardianDesignationMaster()
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

                try
                {
                    CheckValueAddDeleteUpdate();
                }
                catch (Exception)
                {
                    // ignored
                }

                _sql = "Select  ROW_NUMBER() OVER (ORDER BY DesId ASC) AS SrNo,DesId, DesignationName,DesignationRemark from GuardianDesMaster";
                Grd.DataSource = _oo.GridFill(_sql);
                Grd.DataBind();

            }

        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            //@ nvarchar(50),@ nvarchar(500),@SessionName nvarchar(50),@BranchCode nvarchar(50),@LoginName nvarchar(50))
            _sql = "select DesignationName  from GuardianDesMaster where upper(DesignationName)='" + txtDesNa.Text.ToUpper() + "'";

            if (_oo.Duplicate(_sql))
            {
                //oo.MessageBox("Duplicate Record", this.Page);
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Duplicate Record!", "A");
            }
            else
            {
                //if (txtDesNa.Text.Trim().ToUpper() == "HOUSE WIFE" || txtDesNa.Text.Trim().ToUpper() == "HOUSEWIFE")
                //{
                //    ff = "House Wife";
                //}
                //else
                //{
                //    ff = txtDesNa.Text;
                //}

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "GuardianDesMasterProc";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = _con;
                    cmd.Parameters.AddWithValue("@DesignationName", txtDesNa.Text.Trim());
                    cmd.Parameters.AddWithValue("@DesignationRemark", txtdesRemark.Text);

                    cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());//Session["Login"].ToString());
                    try
                    {

                        _con.Open();
                        cmd.ExecuteNonQuery();
                        _con.Close();
                        //oo.MessageBox("Submitted successfully.", this.Page);
                        Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Submitted successfully.", "S");
                        _oo.ClearControls(Page);
                        _sql = "Select  ROW_NUMBER() OVER (ORDER BY DesId ASC) AS SrNo,DesId, DesignationName,DesignationRemark from GuardianDesMaster";
                        Grd.DataSource = _oo.GridFill(_sql);
                        Grd.DataBind();
                        txtDesNa.Focus();

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
            string ss = lblId.Text;
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
            _sql = "Select  ROW_NUMBER() OVER (ORDER BY DesId ASC) AS SrNo,DesId, DesignationName,DesignationRemark from GuardianDesMaster";
            _sql = _sql + " where DesId=" + ss+ "";
            txtOcupationPanel.Text = _oo.ReturnTag(_sql, "DesignationName");
            txtRemarkPanel.Text = _oo.ReturnTag(_sql, "DesignationRemark");
            Panel1_ModalPopupExtender.Show();
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            _sql = "Delete from GuardianDesMaster where DesId=" + lblvalue.Text;

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
                    _sql = "Select  ROW_NUMBER() OVER (ORDER BY DesId ASC) AS SrNo,DesId, DesignationName,DesignationRemark from GuardianDesMaster";

                    Grd.DataSource = _oo.GridFill(_sql);
                    Grd.DataBind();
                    txtDesNa.Focus();
                }
                // ReSharper disable once RedundantCatchClause
                catch (Exception )
                {
                    throw;
                }
            }

        }
        protected void Button8_Click(object sender, EventArgs e)
        {

        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "GuardianDesMasterUpdateProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = _con;
            cmd.Parameters.AddWithValue("@DesId", lblID.Text);
            cmd.Parameters.AddWithValue("@DesignationName", txtOcupationPanel.Text);
            cmd.Parameters.AddWithValue("@DesignationRemark", txtRemarkPanel.Text);
            try
            {

                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
                //oo.MessageBox("Updated successfully.", this.Page);
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Updated successfully.", "S");                                    
                _oo.ClearControls(Page);
                _sql = "Select  ROW_NUMBER() OVER (ORDER BY DesId ASC) AS SrNo,DesId, DesignationName,DesignationRemark from GuardianDesMaster";
                Grd.DataSource = _oo.GridFill(_sql);
                Grd.DataBind();
                txtDesNa.Focus();

            }
            catch (Exception)
            {
                // ignored
            }
        }
        protected void Button4_Click(object sender, EventArgs e)
        {

        }


        public void PermissionGrant(int add1, int delete1, int update1, LinkButton ladd,   Button ldelete,Button lUpdate)
        {
            ladd.Enabled = add1 == 1;
            ldelete.Enabled = delete1 == 1;
            lUpdate.Enabled = update1 == 1;
        }
        public void CheckValueAddDeleteUpdate()
        {
            _sql = " select LoginId,LoginName,Pass,SessionId,BranchId,LT.LoginTypeName,ltb.add1 as add1,ltb.delete1 as delete1,ltb.update1 as update1 from LoginTab LTb";
            _sql = _sql + " left join LoginType Lt on LTb.LoginTypeId=Lt.LoginTypeId where LT.LoginTypeName='" + Session["Logintype"] + "' and LTb.LoginName='" + Session["LoginName"] + "' and LTb.BranchId=" + Session["BranchCode"] + "";
            int a, u, d;
            a = Convert.ToInt32(_oo.ReturnTag(_sql, "add1"));
            u = Convert.ToInt32(_oo.ReturnTag(_sql, "update1"));
            d = Convert.ToInt32(_oo.ReturnTag(_sql, "delete1"));
      
            // ReSharper disable once RedundantCast
            PermissionGrant(a, d, u, (LinkButton)LinkButton1,btnDelete, Button3);
        }

        public override void Dispose()
        {
            _con.Dispose();
            _oo.Dispose();
        }
    }
}