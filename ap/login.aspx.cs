using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System;
using System.Net.NetworkInformation;
using System.Collections.Generic;
using System.IO;
using System.Web.UI.WebControls;

public partial class login : System.Web.UI.Page
{
    private SqlConnection _con;
    private readonly Campus _oo;
    private string _sql, _mess = string.Empty;
    private DataTable _dt;
    public login()
    {
        _con = new SqlConnection();
        _oo = new Campus();
        _dt = new DataTable();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        _con = _oo.dbGet_connection();
        setwallpaper();
        if (!IsPostBack)
        {
            _sql = "select BranchId, BranchName from BranchTab";
            _oo.FillDropDown_withValue(_sql, DrpBranchName, "BranchName", "BranchId");
            DrpBranchName.Items.Insert(0, new ListItem("<--Select Branch-->", ""));
            DrpBranchName.SelectedValue = Session["BranchCode"].ToString();
            string sql = "select phone,adminEmail from CollegeMaster where BranchCode=" + Session["BranchCode"].ToString();
            string str = "";
            if (_oo.ReturnTag(sql, "phone") != "" && _oo.ReturnTag(sql, "adminEmail") != "")
            {
                str = str + "<i class='fa fa-phone'></i>&nbsp;" + _oo.ReturnTag(sql, "phone") + " &nbsp;<i class='fa fa-envelope'></i>&nbsp;" + _oo.ReturnTag(sql, "adminEmail");
            }
            else if (_oo.ReturnTag(sql, "phone") != "" && _oo.ReturnTag(sql, "adminEmail") == "")
            {
                str = str + "<i class='fa fa-phone'></i>&nbsp;" + _oo.ReturnTag(sql, "phone");
            }
            else if (_oo.ReturnTag(sql, "phone") == "" && _oo.ReturnTag(sql, "adminEmail") != "")
            {
                str = str + "<i class='fa fa-envelope'></i>&nbsp;" + _oo.ReturnTag(sql, "adminEmail");
            }
            lblsupport.Text = str;
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@Queryfor", "S"));
            DataSet ds = new DataSet();
            ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("Get_GenralInfo", param);
            try
            {
                if (ds.Tables[3].Rows[0][0].ToString() != "")
                {
                    lblCompanyName.Visible = true;
                    Label1.Text = ds.Tables[3].Rows[0][0].ToString();
                }
            }
            catch (Exception)
            {
            }
        }
    }
    public void setwallpaper()
    {
        string bgImage = "";
        try
        {
            SqlDataAdapter sdaa = new SqlDataAdapter("select * from Login_Wallpager", _con);
            DataTable dtt = new DataTable();
            sdaa.Fill(dtt);
            if (dtt.Rows.Count > 0)
            {
                if (dtt.Rows[0]["Wallpaper_Path"].ToString() == "no")
                {
                    bgImage = "";
                }
                if (dtt.Rows[0]["Wallpaper_Path"].ToString() == "")
                {
                    bgImage = "../Uploads/LoginWallpaper/eAM_Default_bg.png";
                }
                if (dtt.Rows[0]["Wallpaper_Path"].ToString() != "no" && dtt.Rows[0]["Wallpaper_Path"].ToString() != "")
                {
                    bgImage = dtt.Rows[0]["Wallpaper_Path"].ToString();
                }
                if (dtt.Rows[0]["Color"].ToString().Trim() != "" && dtt.Rows[0]["Wallpaper_Path"].ToString().Trim() != "")
                {
                    //  pages.Style["background-color"] = dtt.Rows[0]["Color"].ToString();
                    //  if (!File.Exists(Server.MapPath(dtt.Rows[0]["Wallpaper_Path"].ToString()))) return;
                    pages.Style["background-image"] = bgImage;
                    pages.Style["-webkit-background-size"] = "cover";
                    pages.Style["-moz-background-size"] = "cover";
                    pages.Style["-o-background-size"] = "cover";
                    pages.Style["background-size"] = "cover";
                    pages.Style["background-size"] = "100%";
                }
                else if (dtt.Rows[0]["Wallpaper_Path"].ToString().Trim() != "" && dtt.Rows[0]["Black_White"].ToString().Trim() != "")
                {
                    // pages.Attributes["class"] = dtt.Rows[0]["Black_White"].ToString().Trim();
                    // pages.Style["background-image"] = dtt.Rows[0]["Wallpaper_Path"].ToString();

                    HtmlLink cssLink = new HtmlLink();
                    cssLink.Href = "../css/loginwallStyleSheet.css";
                    cssLink.Attributes.Add("rel", "stylesheet");
                    cssLink.Attributes.Add("type", "text/css");
                    //  if (!File.Exists(Server.MapPath(dtt.Rows[0]["Wallpaper_Path"].ToString()))) return;
                    cssLink.Style["background-image"] = bgImage;
                    Page.Header.Controls.Add(cssLink);
                }
                else if (dtt.Rows[0]["Wallpaper_Path"].ToString().Trim() != "")
                {
                    // if(!File.Exists(Server.MapPath(dtt.Rows[0]["Wallpaper_Path"].ToString()))) return;
                    pages.Style["background-image"] = bgImage;
                    pages.Style["-webkit-background-size"] = "cover";
                    pages.Style[" -moz-background-size"] = "cover";
                    pages.Style[" -o-background-size"] = "cover";
                    pages.Style["background-size"] = "cover";
                    pages.Style["background-size"] = "100%";
                }
                else
                { }
            }
            else
            {
                bgImage = "../Uploads/LoginWallpaper/eAM_Default_bg.png";
                pages.Style["background-image"] = bgImage;
                pages.Style["-webkit-background-size"] = "cover";
                pages.Style["-moz-background-size"] = "cover";
                pages.Style["-o-background-size"] = "cover";
                pages.Style["background-size"] = "cover";
                pages.Style["background-size"] = "100%";
            }
        }
        catch { }
    }
    protected void lnkFake_OnClick(object sender, EventArgs e)
    {
        Session["BranchCode"] = DrpBranchName.SelectedValue;
        Response.Redirect("register.aspx");
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        try
        {
            _sql = "select MOBILE,PASSWORD from REGISTRATION_ADMISSION where MOBILE='" + txtUserName.Text.Trim() + "' and password='" + txtPassword.Text.Trim() + "' and ApprovOTP='A' ";
            var username = _oo.ReturnTag(_sql, "MOBILE");
            var password = _oo.ReturnTag(_sql, "PASSWORD");
            if (txtUserName.Text.Trim() != "" && txtPassword.Text.Trim() != "")
            {
                if (txtUserName.Text.Trim() == username)
                {
                    if (txtPassword.Text.Trim() == password)
                    {
                        string sql2 = "select top 1 Mobile, BranchCode from AdmissionFormOnline where Mobile = '" + txtUserName.Text.Trim() + "' and AdmissionType = 'new'  order by MoRegID desc";
                        _dt = _oo.Fetchdata(sql2);
                        if (_dt != null && _dt.Rows.Count > 0)
                        {
                            Session["UserMobile"] = username.ToString().Trim();
                            Session["txnchk"] = username.ToString().Trim();
                            Session["BranchCode"] = _dt.Rows[0]["BranchCode"].ToString().Trim();
                            Session["pass"] = password.ToString().Trim();
                            Response.Redirect("~/ap/Admission_Details.aspx?txtno=" +
                                              _dt.Rows[0]["Mobile"].ToString().Trim() + "", false);
                        }
                        else
                        {
                            Session["txnchk"] = "";
                            Session["UserMobile"] = txtUserName.Text.Trim();
                            Session["mobilenochk"] = txtUserName.Text.Trim();
                            Session["pass"] = password;
                            Session["BranchCode"] = DrpBranchName.SelectedValue;
                            Response.Redirect("../2/Gpaf.aspx?check=admintion&id=" + txtUserName.Text.Trim() + "",
                            false);
                        }
                    }
                    else
                    {
                        _oo.msgbox(Page, msgbox, "Sorry! You have entered incorrect Password", "A");
                    }
                }
                else
                {
                    _oo.msgbox(Page, msgbox, "Sorry! You have entered incorrect Username or Password.", "A");
                }
            }
            else
            {
                _oo.msgbox(Page, msgbox, "Sorry! You have entered incorrect Username or Password.", "A");
            }
        }
        catch (Exception)
        {
        }
    }
    public override void Dispose()
    {
        _con.Dispose();
        _oo.Dispose();
        _dt.Dispose();
    }
}
