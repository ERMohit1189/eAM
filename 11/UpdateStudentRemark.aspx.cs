using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class UpdateStudentRemark : Page
{
    SqlConnection _con = new SqlConnection();
    readonly Campus _oo = new Campus();

    string _sql = string.Empty;

    protected void Page_PreInIt(object sender, EventArgs e)
    {
        if (Session["Logintype"] == null) { Response.Redirect("~/default.aspx"); }

        if (Session["Logintype"].ToString() == "Admin")
        {
            MasterPageFile = "~/Master/admin_root-manager.master";
        }
        else if (Session["Logintype"].ToString() == "Staff")
        {
            MasterPageFile = "~/Staff/staff_root-manager.master";
        }
        else if (Session["Logintype"].ToString() == "Guardian")
        {
            MasterPageFile = "~/sp/sp_root-manager.master";
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file

        _con = _oo.dbGet_connection();
        Campus camp1 = new Campus(); camp1.LoadLoader(loader);
        if (!IsPostBack)
        {
            if (Session["logintype"].ToString() == "Admin")
            {
                divEnter2.Visible = true;
                divEnter1.Visible = false;
                btnshow.Visible = true;
            }
            else
            {
                divEnter2.Visible = false;
                divEnter1.Visible = true;
                btnshow.Visible = false;
                LoadClassSrno();
            }
        }
    }

    private void LoadClassSrno()
    {
        List<SqlParameter> param = new List<SqlParameter>();
        param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
        param.Add(new SqlParameter("@EmpCode", Session["LoginName"].ToString()));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));

        drpSrno.DataSource = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("GetClassTeacherClassNameStudent_Proc", param);
        drpSrno.DataTextField = "name";
        drpSrno.DataValueField = "srno";
        drpSrno.DataBind();
        BAL.objBal.fillSelectvalue(drpSrno, "<--Select-->", "<--Select-->");
        drpSrno.SelectedIndex = 0;
    }

    protected void drpSrno_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        GetStudentRemarkReport();
    }
    protected void txtSearch_OnTextChanged(object sender, EventArgs e)
    {
        GetStudentRemarkReport();
    }

    protected void lnkView_OnClick(object sender, EventArgs e)
    {
        GetStudentRemarkReport();
    }
    private void GetStudentRemarkReport()
    {
        try
        {
            string sqls = "select top(1) Remark from RemarkStudent where BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' order by id desc";
            if (!_oo.Duplicate(sqls))
            {
                Campus camp = new Campus(); camp.msgbox(Page, msg1, "Review not found in this session!", "A");
            }
            else
            {
                var studentId = Request.Form[hfStudentId.UniqueID];
                if (studentId != null)
                {
                    if (string.IsNullOrEmpty(studentId))
                    {
                        studentId = txtSearch.Text.Trim();
                    }
                }
                else
                {
                    studentId = drpSrno.SelectedValue;
                }

                sqls = "select remark from RemarkStudent where SrNo='" + studentId + "' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                if (!_oo.Duplicate(sqls))
                {
                    Campus camp = new Campus(); camp.msgbox(Page, msg1, "Review not found!", "A");
                }
                else
                {

                    _sql = "Select * from StudentOfficialDetails where blocked='Yes' and srno='" + studentId + "' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                    var sql1 = "Select Promotion,MODForFeeDeposit from StudentOfficialDetails where srno='" + studentId + "' and BranchCode=" + Session["BranchCode"].ToString() + " and SessionName='" + Session["SessionName"] + "'";

                    _sql = "Select * from AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "', " + Session["BranchCode"].ToString() + " ) where srno='" + studentId + "'";
                    var ds = _oo.GridFill(_sql);

                    grdStRecord.DataSource = ds;
                    grdStRecord.DataBind();
                    div1.Visible = true;
                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            img.ImageUrl = ds.Tables[0].Rows[0]["PhotoPath"].ToString() != string.Empty ? ds.Tables[0].Rows[0]["PhotoPath"] + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture) : "../img/user-pic/user-pic.jpg" + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture);
                            studentImg.NavigateUrl = ds.Tables[0].Rows[0]["PhotoPath"].ToString() != string.Empty ? ds.Tables[0].Rows[0]["PhotoPath"] + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture) : "../img/user-pic/user-pic.jpg" + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture);
                        }
                    }
                    var param = new List<SqlParameter>
                {
                    (new SqlParameter("@QueryFor", "R")),
                    (new SqlParameter("@SrNo", studentId)),
                    (new SqlParameter("@SessionName", Session["SessionName"].ToString())),
                    (new SqlParameter("@BranchCode", Session["BranchCode"].ToString()))
                };
                    var dss = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("USP_StudentRemarkDetails", param);

                    lblremark.Text = dss.Tables[0].Rows[0]["remark"].ToString();
                }
            }
        }
        catch
        {
        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        try
        {
            Label srno = (Label)grdStRecord.Rows[0].FindControl("lblSrno");
            using (var cmd = new SqlCommand())
            {
                if (_con.State == ConnectionState.Closed)
                {
                    _con.Open();
                }
                cmd.CommandText = "USP_RemarkStudent";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = _con;
                cmd.Parameters.AddWithValue("@QueryFor", "U");
                cmd.Parameters.AddWithValue("@SrNo", srno.Text);
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                cmd.Parameters.AddWithValue("@Remark", lblremark.Text.Trim());
                cmd.ExecuteNonQuery();
                Campus camp = new Campus(); camp.msgbox(Page, msg1, "Updated successfully.", "S");
            }
        }
        catch (Exception ex)
        {
            Campus camp = new Campus(); camp.msgbox(Page, msg1, "Not updated!", "A");
        }
        finally
        {
            if (_con.State == ConnectionState.Open) { _con.Close(); }
        }
    }
}