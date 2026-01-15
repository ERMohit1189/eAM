using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace _10
{
public partial class UnitMaster : System.Web.UI.Page
{
        private SqlConnection _con;
        private readonly Campus _oo;
        private DataSet _ds;
        private string _sql = String.Empty;
        public UnitMaster()
        {
            _con = new SqlConnection();
            _oo = new Campus();
            _ds = new DataSet();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string)Session["LoginName"] == "")
            {
                Response.Redirect("default.aspx");
            }
            _con = _oo.dbGet_connection();
            Campus camp = new Campus(); camp.LoadLoader(loader);
            if (!IsPostBack)
            {
                GetStoreList();
            }
        }

        private void GetStoreList()
        {
            try
            {
                var param = new List<SqlParameter> { new SqlParameter("@QueryFor", "S") };
                _ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("InvUnitMasterProc", param);
                if (_ds.Tables[0].Rows.Count > 0)
                {
                    divlistshow.Visible = true;
                    Repeater1.DataSource = _ds;
                    Repeater1.DataBind();
                    for (int i = 0; i < Repeater1.Items.Count; i++)
                    {
                        Label lblid = (Label)Repeater1.Items[i].FindControl("lblid");

                        string sqls = "select Caregory from InvArticleEntry where BranchCode="+ Session["BranchCode"] + " and UnitID="+ lblid.Text.Trim() + "";
                        if (_oo.Duplicate(sqls))
                        {
                            LinkButton LinkButton1 = (LinkButton)Repeater1.Items[i].FindControl("LinkButton1");
                            LinkButton lnkDelete = (LinkButton)Repeater1.Items[i].FindControl("lnkDelete");
                            LinkButton1.Text = "<i class='fa fa-lock'></i>";
                            lnkDelete.Text = "<i class='fa fa-lock'></i>";
                            LinkButton1.Enabled = false;
                            lnkDelete.Enabled = false;
                        }
                    }
                }
                else
                {
                    divlistshow.Visible = false;
                    Repeater1.DataSource = null;
                    Repeater1.DataBind();
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }
        protected void lnkSubmit_OnClick(object sender, EventArgs e)
        {
            try
            {
                _sql = "Select UnitName from InvUnitMaster where UnitName='" + txtUnit.Text.Trim() + "'";
                if (_oo.Duplicate(_sql))
                {
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Unit Name is Already Exist", "A");
                }
                else
                {
                    var param = new List<SqlParameter>
                    {
                        new SqlParameter("@UnitName", txtUnit.Text.Trim()),
                        new SqlParameter("@DisplayName", ""),
                        new SqlParameter("@Remark", txtremark.Text.Trim()),
                        new SqlParameter("@SessionName", Session["SessionName"].ToString()),
                        new SqlParameter("@LoginName", Session["LoginName"].ToString()),
                        new SqlParameter("@BranchCode", Session["BranchCode"].ToString()),
                        new SqlParameter("@QueryFor", "I")
                    };
                    var para = new SqlParameter("@Msg", "")
                    {
                        Direction = ParameterDirection.Output,
                        Size = 0x100
                    };
                    param.Add(para);

                    var msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("InvUnitMasterProc", param);

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
                    _oo.ClearControls(Page);
                    GetStoreList();
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }
        protected void LinkButton1_OnClick(object sender, EventArgs e)
        {
            try
            {
                var lnk = (LinkButton)sender;
                var lblid = (Label)lnk.NamingContainer.FindControl("lblid");
                lblID.Text = lblid.Text;
                var param = new List<SqlParameter>
                {
                    new SqlParameter("@Id", lblid.Text),
                    new SqlParameter("@QueryFor", "SS")
                };

                DataSet ds;
                ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("InvUnitMasterProc", param);
                if (ds != null)
                {
                    DataTable dt;
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        txtEditUnitName.Text = dt.Rows[0]["UnitName"].ToString();
                        txtEditRemark.Text = dt.Rows[0]["Remark"].ToString();
                    }
                }
                Panel1_ModalPopupExtender.Show();
            }
            catch (Exception)
            {
                // ignored
            }
        }
        protected void lnkDelete_OnClick(object sender, EventArgs e)
        {
            var lnk = (LinkButton)sender;
            var lblid = (Label)lnk.NamingContainer.FindControl("lblid");
            ViewState["UnitID"] = lblid.Text;
            Panel2_ModalPopupExtender.Show();
        }
        protected void btnupdate_OnClick(object sender, EventArgs e)
        {
            try
            {
                _sql = "Select UnitName from InvUnitMaster where UnitName='" + txtEditUnitName.Text.Trim() + "' and id<>"+ lblID.Text.Trim() + "";
                if (_oo.Duplicate(_sql))
                {
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Unit Name is Already Exist", "A");
                }
                else
                {
                    var param = new List<SqlParameter>
                    {
                        new SqlParameter("@ID", lblID.Text.Trim()),
                        new SqlParameter("@UnitName", txtEditUnitName.Text.Trim()),
                        new SqlParameter("@DisplayName", ""),
                        new SqlParameter("@Remark", txtEditRemark.Text.Trim()),
                        new SqlParameter("@SessionName", Session["SessionName"].ToString()),
                        new SqlParameter("@LoginName", Session["LoginName"].ToString()),
                        new SqlParameter("@BranchCode", Session["BranchCode"].ToString()),
                        new SqlParameter("@QueryFor", "U")
                    };
                    var para = new SqlParameter("@Msg", "")
                    {
                        Direction = ParameterDirection.Output,
                        Size = 0x100
                    };
                    param.Add(para);

                    var msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("InvUnitMasterProc", param);

                    if (msg == "S")
                    {
                        Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Updated successfully.", "S");
                    }
                    else if (msg == "D")
                    {
                        Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Duplicate Entry!", "A");
                    }
                    else
                    {
                        Campus camp = new Campus(); camp.msgbox(Page, msgbox, msg, "W");
                    }
                    _oo.ClearControls(Page);
                    GetStoreList();
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }
        protected void btnDelete_OnClick(object sender, EventArgs e)
        {
            var param = new List<SqlParameter>
            {
                new SqlParameter("@ID", ViewState["UnitID"]),
                new SqlParameter("@QueryFor", "D")
            };
            var para = new SqlParameter("@Msg", "")
            {
                Direction = ParameterDirection.Output,
                Size = 0x100
            };
            param.Add(para);

            var msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("InvUnitMasterProc", param);

            if (msg == "S")
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Deleted successfully.", "S");
            }
            GetStoreList();
        }
        public override void Dispose()
        {
            _con.Dispose();
            _oo.Dispose();
        }



    }
}