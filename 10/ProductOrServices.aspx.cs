using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ProductOrServices : System.Web.UI.Page
{
    private SqlConnection _con;
    private readonly Campus _oo;
    private DataSet _ds;
    private string _sql = String.Empty;
    public ProductOrServices()
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
        BLL.BLLInstance.LoadHeader("Report", header);
        if (!IsPostBack)
        {
            GetArticleName();
            GetArticleEntryList();
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void drpartclcatagory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpartclcatagory.SelectedValue == "Product")
        {
            divProType.Visible = true;
            divHSNCode.Visible = true;
            divUnit.Visible = true;
            txtHSNCode.Attributes.Add("CssClass", "form-control-blue validatetxt");
            drpunit.Attributes.Add("CssClass", "form-control-blue validatedrp");
        }
        else
        {
            divProType.Visible = false;
            divHSNCode.Visible = true;
            divUnit.Visible = false;
            txtHSNCode.Attributes.Add("CssClass", "form-control-blue");
            drpunit.Attributes.Add("CssClass", "form-control-blue");
        }
    }
    private void GetArticleName()
    {
        _sql = "Select ID,unitName from InvunitMaster where BranchCode=" + Session["BranchCode"] + "";
        _oo.FillDropDown_withValue(_sql, drpunit, "unitName", "ID");
        drpunit.Items.Insert(0, new ListItem("<--Select-->", "0"));

        _oo.FillDropDown_withValue(_sql, ddleditunit, "unitName", "ID");
        ddleditunit.Items.Insert(0, new ListItem("<--Select-->", "0"));
    }

    private void GetArticleEntryList()
    {
        
        try
        {
            var param = new List<SqlParameter> { new SqlParameter("@QueryFor", "S"), new SqlParameter("@BranchCode", Session["BranchCode"]) };
            _ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("InvArticleEntryProc", param);
            if (_ds.Tables[0].Rows.Count > 0)
            {
                divlistshow.Visible = true;
                Repeater1.DataSource = _ds;
                Repeater1.DataBind();
                heading.Text = "Product List";
                lblRegister.Text = "Date : " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
                abc.Visible = true;
                divExport.Visible = true;
            }
            else
            {
                divlistshow.Visible = false;
                Repeater1.DataSource = null;
                Repeater1.DataBind();
                abc.Visible = false;
                divExport.Visible = false;
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
            _sql = "Select ArticleCatID from InvArticleEntry where Name='" + txtname.Text.Trim() + "'and  BranchCode=" + Session["BranchCode"] + "";
            if (_oo.Duplicate(_sql))
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Store Name is Already Exist", "A");
            }
            else
            {
                var param = new List<SqlParameter>
                    {
                        new SqlParameter("@HSNCode", txtHSNCode.Text.Trim()),
                        new SqlParameter("@Caregory", drpartclcatagory.SelectedValue),
                        new SqlParameter("@ProductType", drpartclcatagory.SelectedValue == "Product"?rdoProductType.SelectedValue:""),
                        new SqlParameter("@Name", txtname.Text.Trim()),
                        new SqlParameter("@UnitID", drpartclcatagory.SelectedValue == "Product"?drpunit.SelectedValue:""),
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

                var msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("InvArticleEntryProc", param);

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
                GetArticleEntryList();
            }
        }
        catch (Exception)
        {
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
                    new SqlParameter("@BranchCode", Session["BranchCode"]),
                    new SqlParameter("@QueryFor", "SS")
                };

            _ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("InvArticleEntryProc", param);
            if (_ds != null)
            {

                DataTable dt;
                dt = _ds.Tables[0];
                if (dt.Rows.Count > 0)
                {

                    drpeditarticlecategory.SelectedValue = dt.Rows[0]["Caregory"].ToString();
                    if (drpeditarticlecategory.SelectedValue == "Product")
                    {
                        trProType.Visible = true;
                        trHSNCode.Visible = true;
                        trUnit.Visible = true;
                        txteditHSNCode.Attributes.Add("CssClass", "form-control-blue validatetxtt");
                        ddleditunit.Attributes.Add("CssClass", "form-control-blue validatedrpp");
                    }
                    else
                    {
                        trProType.Visible = false;
                        trHSNCode.Visible = false;
                        trUnit.Visible = false;
                        txteditHSNCode.Attributes.Add("CssClass", "form-control-blue");
                        ddleditunit.Attributes.Add("CssClass", "form-control-blue");
                    }

                    txteditname.Text = dt.Rows[0]["Name"].ToString();
                    txteditHSNCode.Text = dt.Rows[0]["HSNCode"].ToString();
                    rdoeditProductType.SelectedValue = dt.Rows[0]["ProductType"].ToString();

                    _sql = "Select ID,unitName from InvunitMaster";
                    _oo.FillDropDown_withValue(_sql, ddleditunit, "unitName", "ID");
                    ddleditunit.Items.Insert(0, new ListItem("<--Select-->", "0"));

                    ddleditunit.SelectedValue = dt.Rows[0]["UnitID"].ToString();
                    txtEditRemark.Text = dt.Rows[0]["Remark"].ToString();

                }
            }
            Panel1_ModalPopupExtender.Show();
        }
        catch (Exception)
        {
        }
    }

    protected void lnkDelete_OnClick(object sender, EventArgs e)
    {
        var lnk = (LinkButton)sender;
        var lblid = (Label)lnk.NamingContainer.FindControl("lblid");
        ViewState["StoreID"] = lblid.Text;
        Panel2_ModalPopupExtender.Show();
    }
    protected void btnupdate_OnClick(object sender, EventArgs e)
    {
        try
        {
            _sql = "Select ArticleCatID from InvArticleEntry where Name='" + txteditname.Text.Trim() + "' and  BranchCode=" + Session["BranchCode"] + " and id<>" + lblID.Text.Trim() + "";
            if (_oo.Duplicate(_sql))
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Store Name is Already Exist", "A");
            }
            else
            {
                var param = new List<SqlParameter>
                    {
                        new SqlParameter("@ID", lblID.Text),
                        new SqlParameter("@HSNCode", txteditHSNCode.Text.Trim()),
                        new SqlParameter("@Caregory", drpeditarticlecategory.SelectedValue),
                        new SqlParameter("@ProductType", rdoeditProductType.SelectedValue),
                        new SqlParameter("@Name", txteditname.Text.Trim()),
                        new SqlParameter("@UnitID", ddleditunit.SelectedValue),
                        new SqlParameter("@Remark", txtEditRemark.Text),
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

                var msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("InvArticleEntryProc", param);

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
                GetArticleEntryList();
            }
        }
        catch (Exception)
        {
        }
    }

    protected void btnDelete_OnClick(object sender, EventArgs e)
    {
        var param = new List<SqlParameter>
            {
                new SqlParameter("@ID", ViewState["StoreID"]),new SqlParameter("@BranchCode", Session["BranchCode"]),
                new SqlParameter("@QueryFor", "D")
            };
        var para = new SqlParameter("@Msg", "")
        {
            Direction = ParameterDirection.Output,
            Size = 0x100
        };
        param.Add(para);

        var msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("InvArticleEntryProc", param);

        if (msg == "S")
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Deleted successfully.", "S");
        }
        GetArticleEntryList();
    }
    public override void Dispose()
    {
        _con.Dispose();
        _oo.Dispose();
    }


    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        _oo.ExportTolandscapeWord(Response, "ProductOrServicesList", gdv1);
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        _oo.ExportDivToExcelWithFormatting(Response, "ProductOrServicesList.xls", gdv1, Server.MapPath("~/Admin/css/style.css"));
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {
        _oo.ExporttolandscapePdf(Response, "ProductOrServicesList", abc);
    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = abc;
        ClientScript.RegisterStartupScript(GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }

}