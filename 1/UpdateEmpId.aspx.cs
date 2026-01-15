using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _1
{
    public partial class AdminUpdateEmpId : Page
    {
        private SqlConnection _con;
        readonly SqlConnection _con1;
#pragma warning disable 169
        private SqlCommand _cmd1;
#pragma warning restore 169
        readonly Campus _oo;
        private string _sql = "";
#pragma warning disable 169
        private DataTable _dt;
#pragma warning restore 169

        public AdminUpdateEmpId()
        {
            _con = new SqlConnection();
            _con1 = new SqlConnection();
            _oo = new Campus();
        }
        protected void Page_PreInIt(object sender, EventArgs e)
        {
            if (Session["Logintype"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            if (Session["Logintype"].ToString() == "Administrator")
            {
                MasterPageFile = "~/Administrator/administrato_root-manager.master";
            }
           
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            _con = _oo.dbGet_connection();
            Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file
            if (this.IsPostBack) //just write this
                return;
            if (Session["LoginType"].ToString() == "Admin")
            {
                divBranch.Visible = false;
                divSession.Visible = false;
                //string _sql = "select SrNoType from tblAutometedSRNO where SrNoType='Automatic' and BranchCode=" + Session["BranchCode"] + "";
                //if (_oo.Duplicate(_sql))
                //{
                //    searchdiv1.Visible = false;
                //    searchdiv2.Visible = false;
                //    msgbox.InnerText = "Employee ID can't be changed, as Employee ID is generating automatically.";
                //}
                //else
                //{
                searchdiv1.Visible = true;
                searchdiv2.Visible = true;
                //}
            }
            else
            {
                divBranch.Visible = true;
                divSession.Visible = true;
                string sql = "Select BranchId, BranchName from Branchtab";
                var dt = _oo.Fetchdata(sql);
                ddlBranch.DataSource = dt;
                ddlBranch.DataTextField = "BranchName";
                ddlBranch.DataValueField = "BranchId";
                ddlBranch.DataBind();
                ddlBranch.Items.Insert(0, new ListItem("<--Select-->", ""));
                DrpSessionName.Items.Insert(0, new ListItem("<--Select-->", ""));
            }
        }
        protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["BranchCode"] = "0";
            if (ddlBranch.SelectedIndex == 0)
            {
                DrpSessionName.Items.Clear();
                DrpSessionName.Items.Insert(0, new ListItem("<--Select-->", ""));
                searchdiv1.Visible = false;
                searchdiv2.Visible = false;
            }
            else
            {

                string sql = "select SessionName from SessionMaster where BranchCode=" + ddlBranch.SelectedValue + "";
                var dt2 = _oo.Fetchdata(sql);
                DrpSessionName.DataSource = dt2;
                DrpSessionName.DataTextField = "SessionName";
                DrpSessionName.DataValueField = "SessionName";
                DrpSessionName.DataBind();
                DrpSessionName.Items.Insert(0, new ListItem("<--Select-->", ""));
                Session["BranchCode"] = ddlBranch.SelectedValue;
            }
        }
        protected void DrpSessionName_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["SessionName"] = null;
            searchdiv1.Visible = true;
            searchdiv2.Visible = true;
            if (DrpSessionName.SelectedIndex == 0)
            {
                searchdiv1.Visible = false;
                searchdiv2.Visible = false;
            }
            else
            {
                Session["SessionName"] = DrpSessionName.SelectedValue;
            }
        }

        protected void lnkShow_Click(object sender, EventArgs e)
        {
            Show();
        }

        public void Show()
        {
            string empId = Request.Form[hfEmployeeId.UniqueID];
            if (empId == string.Empty)
            {
                empId = TxtEnter.Text.Trim();
            }

            string sql10 = "Select Empid from EmpWithdrawlRecord where EmpId='" + empId.Trim() + "'";
            if (_oo.ReturnTag(sql10, "Empid") != "")
            {

                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "This Employee is already Withdrawn!", "A");

                Grd.Visible = false;
                GridView1.DataSource = null;
                GridView1.Visible = false;

                newsrdis.Visible = false;
                return;
            }
            else
            {

                sql10 = "Select eod.EmpId EmpId,eod.Ecode Ecode,egd.EFirstName+' '+egd.EMiddleName+' '+egd.ELastName as EmpName,egd.EFatherName FatherName,eod.DesNameNew,";
                sql10 = sql10 + " egd.EMotherName,egd.EMobileNo,Convert(varchar(11),eod.RegistrationDate,106) as RegistrationDate  from EmpployeeOfficialDetails eod ";
                sql10 = sql10 + " inner join EmpGeneralDetail egd on eod.Empid=egd.Empid and eod.EmpId=egd.EmpId where eod.Withdrwal is null ";
                sql10 = sql10 + " and eod.EmpId='" + empId.Trim() + "' and egd.BranchCode=" + Session["BranchCode"].ToString() + " and eod.BranchCode=" + Session["BranchCode"].ToString() + "";
                Grd.DataSource = _oo.GridFill(sql10);
                Grd.DataBind();
                DataSet ds;
                ds = _oo.GridFill(_sql);

                if (Grd.Rows.Count > 0)
                {
                    grdshow.Visible = true;
                }
                else
                {
                    Campus camp = new Campus(); camp.msgbox(lnkShow, msgbox, "Sorry, No record(s) found!", "A");
                    grdshow.Visible = false;
                }

                if (Grd.Rows.Count > 0)
                {
                    newsrdis.Visible = true;
                    TextBox1.Text = "";
                    GridView1.DataSource = null;
                    GridView1.Visible = false;
                }
                else
                {
                    newsrdis.Visible = false;
                    TextBox1.Text = "";
                    GridView1.DataSource = null;
                    GridView1.Visible = false;
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Invalid Employee ID!", "A");
                }

            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            ViewState["CheckRefresh"] = Session["CheckRefresh"];
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            string empId = Request.Form[hfEmployeeId.UniqueID];
            if (empId == string.Empty)
            {
                empId = TxtEnter.Text.Trim();
            }

            string msg = "";
            string sql1 = "select * from EmpployeeOfficialDetails  ";
            sql1 = sql1 + " where  EmpId='" + empId.Trim() + "' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            if (_oo.Duplicate(sql1) == false)
            {
                if (TextBox1.Text != "")
                {
                    List<SqlParameter> param = new List<SqlParameter>();
                    param.Add(new SqlParameter("@Old_EmpId", empId.Trim()));
                    param.Add(new SqlParameter("@New_EmpId", TextBox1.Text.Trim()));
                    param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString().Trim()));


                    SqlParameter para = new SqlParameter("@MSG", "");
                    para.Direction = ParameterDirection.Output;
                    para.Size = 0x100;
                    param.Add(para);

                    msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("USP_ChangeEmpId", param);

                    if (msg.Trim().ToUpper() == "S")
                    {
                        FetchNewEmpId(TextBox1.Text.Trim());
                        Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Employee ID updated successfully", "S");
                        hfEmployeeId.Value = "";
                        TextBox1.Text = "";
                    }
                    if (msg.Trim().ToUpper() == "D")
                    {
                        Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Sorry, This Employee ID Already Exist!", "A");
                    }
                }
                else
                {
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Please fill Employee ID!", "A");
                }
            }
            else
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Sorry, This Employee ID Already Exist!", "A");

            }
        }

        public void FetchNewEmpId(string empId)
        {
                _sql = "Select eod.EmpId EmpId,eod.Ecode Ecode,egd.EFirstName+' '+egd.EMiddleName+' '+egd.ELastName as EmpName,egd.EFatherName FatherName,eod.DesNameNew,";
                _sql = _sql + " egd.EMotherName,egd.EMobileNo,Convert(varchar(11),eod.RegistrationDate,106) as RegistrationDate  from EmpployeeOfficialDetails eod ";
                _sql = _sql + " inner join EmpGeneralDetail egd on eod.Empid=egd.Empid and eod.EmpId=egd.EmpId where eod.Withdrwal is null ";
                _sql = _sql + " and eod.EmpId='" + empId.Trim() + "' and egd.BranchCode=" + Session["BranchCode"].ToString() + " and eod.BranchCode=" + Session["BranchCode"].ToString() + "";

                GridView1.DataSource = _oo.GridFill(_sql);
                GridView1.DataBind();
                GridView1.Visible = true;

                //Submit.Visible = false;
        }

        protected void TxtEnter_TextChanged(object sender, EventArgs e)
        {
            Show();
        }

        public override void Dispose()
        {
            _con.Dispose();
            _con1.Dispose();
            _oo.Dispose();
        }

        //protected void TextBox1_TextChanged(object sender, EventArgs e)
        //{
        //    TextBox1.Text = TextBox1.Text.Replace(" ", "");
        //    Submit.Visible = true;
        //}
    }
}