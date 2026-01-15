using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace _1
{
    public partial class AdminSessionMaster : System.Web.UI.Page
    {
        private SqlConnection _con;
        private readonly Campus _oo;
        string _sql = string.Empty;
        public AdminSessionMaster()
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
            txtSesion.Focus();


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

                _oo.AddDateMonthYearDropDown(DrpYear, DrpMonth, DrpDate);
                FindCurrentDateandSetinDropDown();
                _oo.AddDateMonthYearDropDown(DrpYear1, DrpMonth1, DrpDate1);
                FindCurrentDateandSetinDropDown1();

                _oo.AddDateMonthYearDropDown(DrpYYFromPanel, DrpFromMMPanel, DrpFromDDPanel);
                _oo.FindCurrentDateandSetinDropDown(DrpYYFromPanel, DrpFromMMPanel, DrpFromDDPanel);

                _oo.AddDateMonthYearDropDown(DrpYYToPanel, DrpPaneltoMM, DrpToPanelDD);
                _oo.FindCurrentDateandSetinDropDown(DrpYYToPanel, DrpPaneltoMM, DrpToPanelDD);

                string sql = "Select BranchId, BranchName from Branchtab";
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
                }
                dataload();
                Getsessionyear();
            }
        }
        protected void dataload()
        {
            _sql = "Select ROW_NUMBER() OVER (ORDER BY SessionId ASC) AS SrNo, SessionId,SessionName,convert(nvarchar,FromDate,106) as FromDate,convert(nvarchar,ToDate,106) as ToDate  from SessionMaster where BranchCode=" + ddlBranch.SelectedValue + "";
            Grd.DataSource = _oo.GridFill(_sql);
            Grd.DataBind();
            for (int i = 0; i < Grd.Rows.Count; i++)
            {
                Label Label1 = (Label)Grd.Rows[i].FindControl("Label1");
                LinkButton LinkButton2 = (LinkButton)Grd.Rows[i].FindControl("LinkButton2");
                LinkButton LinkButton3 = (LinkButton)Grd.Rows[i].FindControl("LinkButton3");
                _sql = "select (select count(*) from NewAdminInformation where BranchCode=" + ddlBranch.SelectedValue + " and SessionName='" + Label1.Text + "')+ ";
                _sql = _sql + " (select count(*) from StudentOfficialDetails where BranchCode = " + ddlBranch.SelectedValue + " and SessionName = '" + Label1.Text + "') + ";
                _sql = _sql + " (select count(*) from ClassMaster where BranchCode = " + ddlBranch.SelectedValue + " and SessionName = '" + Label1.Text + "') counts";
                if (_oo.ReturnTag(_sql, "counts") != "0")
                {
                    //LinkButton2.Text = "<i class='fa fa-lock'></i>";
                    //LinkButton2.Enabled = false;
                    LinkButton3.Text = "<i class='fa fa-lock'></i>";
                    LinkButton3.Enabled = false;
                }
            }
        }
        protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataload();

        }
        private void Getsessionyear()
        {
            try
            {
                if (DrpYear.SelectedItem.Text != DrpYear1.SelectedItem.Text)
                {

                    if (Convert.ToInt32(DrpYear.SelectedItem.Text) <= Convert.ToInt32(DrpYear1.SelectedItem.Text))
                    {
                        txtSesion.Text = DrpYear.SelectedItem.Text + "-" + DrpYear1.SelectedItem.Text;
                    }
                    else
                    {
                        txtSesion.Text = DrpYear.SelectedItem.Text;
                    }
                }
                else

                {
                    txtSesion.Text = DrpYear.SelectedItem.Text;
                }
            }
            catch
            {
                // ignored
            }
        }

        private void Getsessionyearpanel()
        {
            try
            {
                if (DrpYYFromPanel.SelectedItem.Text != DrpYYToPanel.SelectedItem.Text)
                {

                    if (Convert.ToInt32(DrpYYFromPanel.SelectedItem.Text) <= Convert.ToInt32(DrpYYToPanel.SelectedItem.Text))
                    {
                        txtSessionNamePanel.Text = DrpYYFromPanel.SelectedItem.Text + "-" + DrpYYToPanel.SelectedItem.Text;
                    }
                    else
                    {
                        txtSessionNamePanel.Text = DrpYYFromPanel.SelectedItem.Text;
                    }
                }
                else

                {
                    txtSessionNamePanel.Text = DrpYYFromPanel.SelectedItem.Text;
                }
            }
            catch
            {
                // ignored
            }
        }

        protected void DrpYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            _oo.YearDropDown(DrpYear, DrpMonth, DrpDate);
            Getsessionyear();
        }
        public void FindCurrentDateandSetinDropDown()
        {
            string dd , mm , yy ;
            dd = _oo.ReturnTag("Select day(getdate()) as DateDD", "DateDD");
            mm = _oo.ReturnTag("Select Month(getdate())as MonthMM", "MonthMM");
            yy = _oo.ReturnTag("Select Year(getdate()) as YearYY ", "YearYY");

            DrpYear.Text = yy;
            switch (mm)
            {
                case "1":
                    DrpMonth.Text = "Jan";
                    break;
                case "2":
                    DrpMonth.Text = "Feb";
                    break;
                case "3":
                    DrpMonth.Text = "Mar";
                    break;
                case "4":
                    DrpMonth.Text = "Apr";
                    break;
                case "5":
                    DrpMonth.Text = "May";
                    break;
                case "6":
                    DrpMonth.Text = "Jun";
                    break;
                case "7":
                    DrpMonth.Text = "Jul";
                    break;
                case "8":
                    DrpMonth.Text = "Aug";
                    break;
                case "9":
                    DrpMonth.Text = "Sep";
                    break;
                case "10":
                    DrpMonth.Text = "Oct";
                    break;
                case "11":
                    DrpMonth.Text = "Nov";
                    break;
                case "12":
                    DrpMonth.Text = "Dec";
                    break;
            }


            DrpDate.Text = dd;
        }
        public void FindCurrentDateandSetinDropDown1()
        {
            string dd, mm, yy ;
            dd = _oo.ReturnTag("Select day(getdate()) as DateDD", "DateDD");
            mm = _oo.ReturnTag("Select Month(getdate())as MonthMM", "MonthMM");
            yy = _oo.ReturnTag("Select Year(getdate()) as YearYY ", "YearYY");

            DrpYear1.Text = yy;
            switch (mm)
            {
                case "1":
                    DrpMonth1.Text = "Jan";
                    break;
                case "2":
                    DrpMonth1.Text = "Feb";
                    break;
                case "3":
                    DrpMonth1.Text = "Mar";
                    break;
                case "4":
                    DrpMonth1.Text = "Apr";
                    break;
                case "5":
                    DrpMonth.Text = "May";
                    break;
                case "6":
                    DrpMonth1.Text = "Jun";
                    break;
                case "7":
                    DrpMonth1.Text = "Jul";
                    break;
                case "8":
                    DrpMonth1.Text = "Aug";
                    break;
                case "9":
                    DrpMonth1.Text = "Sep";
                    break;
                case "10":
                    DrpMonth1.Text = "Oct";
                    break;
                case "11":
                    DrpMonth1.Text = "Nov";
                    break;
                case "12":
                    DrpMonth1.Text = "Dec";
                    break;
            }


            DrpDate1.Text = dd;
        }
        protected void DrpMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            _oo.MonthDropDown(DrpYear, DrpMonth, DrpDate);
        }
        protected void DrpYear1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _oo.YearDropDown(DrpYear1, DrpMonth1, DrpDate1);
            Getsessionyear();
        }
        protected void DrpMonth1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _oo.MonthDropDown(DrpYear1, DrpMonth1, DrpDate1);
        }
        protected void LinkButton_Click(object sender, EventArgs e)
        {
            // ReSharper disable once RedundantAssignment
            var ss = "";
            // ReSharper disable once RedundantAssignment
            var ss1 = "";
            var yy = 0;
            var yy1 = 0;

            try
            {
                ss = txtSesion.Text.Substring(0, 4);
                ss1 = txtSesion.Text.Substring(5, 4);

                yy = Convert.ToInt32(ss);
                yy1 = Convert.ToInt32(ss1);
            }
            catch (Exception)
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Invalid Session Name!", "A");
            }
            if (yy < yy1)
            {

                var date = DrpYear.SelectedItem + "/" + DrpMonth.SelectedItem + "/" + DrpDate.SelectedItem;
                var date1 = DrpYear1.SelectedItem + "/" + DrpMonth1.SelectedItem + "/" + DrpDate1.SelectedItem;
                _sql = " Select  SessionId,SessionName  from SessionMaster   where SessionName='" + txtSesion.Text + "' and BranchCode=" + ddlBranch.SelectedValue + "";
                if (_oo.Duplicate(_sql))
                {
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Session Name already alloted!", "A");
                    return;
                }
                _sql = " Select  SessionId  from SessionMaster where (convert(date, '" + date + "') between convert(date, fromdate) and convert(date, todate) or convert(date, '" + date1 + "') between convert(date, fromdate) and convert(date, todate)) and BranchCode=" + ddlBranch.SelectedValue + "";
                if (_oo.Duplicate(_sql))
                {
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Session Name already alloted between entered date(s)!", "A");
                    return;
                }
                else
                {
                    using (var cmd = new SqlCommand())
                    {
                        cmd.CommandText = "SessionProc";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = _con;
                        //SessionName nvarchar(50),@FromDate smalldatetime,@ToDate smalldatetime,@Remark nvarchar(500),@BranchCode nvarchar(50),@LoginName nvarchar(50))
                        cmd.Parameters.AddWithValue("@SessionName", txtSesion.Text);
                        
                        cmd.Parameters.AddWithValue("@FromDate", date);
                        
                        cmd.Parameters.AddWithValue("@ToDate", date1);
                        cmd.Parameters.AddWithValue("@Remark", txtremark.Text);
                        cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());//Session["Login"].ToString());
                        cmd.Parameters.AddWithValue("@BranchCode", ddlBranch.SelectedValue.ToString());
                        {
                            _con.Open();
                            cmd.ExecuteNonQuery();
                            _con.Close();
                            //oo.MessageBox("Submitted successfully.", this.Page);
                            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Submitted successfully.", "S");
                            _oo.ClearControls(Page);
                            FindCurrentDateandSetinDropDown();
                            FindCurrentDateandSetinDropDown1();
                            dataload();
                            Getsessionyear();
                        }
                    }

                }
            }
            else
            {
                //oo.MessageBox("Invalid Session Name!", this.Page);
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Invalid Session Name!", "A");
            }
        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            var fromdate  = DrpYYFromPanel.SelectedItem + "/" + DrpFromMMPanel.SelectedItem + "/" + DrpFromDDPanel.SelectedItem;
            var todate = DrpYYToPanel.SelectedItem + "/" + DrpPaneltoMM.SelectedItem + "/" + DrpToPanelDD.SelectedItem;
            _sql = " Select  SessionId  from SessionMaster where (convert(date, '" + fromdate + "') between convert(date, fromdate) and convert(date, todate) or convert(date, '" + todate + "') between convert(date, fromdate) and convert(date, todate)) and BranchCode=" + ddlBranch.SelectedValue + " and SessionId<>"+lblID.Text.Trim()+"";
            if (_oo.Duplicate(_sql))
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Session Name already alloted between entered date(s)!", "A");
                return;
            }
            using (var cmd = new SqlCommand())
            {
                cmd.CommandText = "SessionMasterUpdateProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SessionId", lblID.Text);
                cmd.Parameters.AddWithValue("@SessionName", txtSessionNamePanel.Text);
                cmd.Parameters.AddWithValue("@FromDate", fromdate);
                cmd.Parameters.AddWithValue("@ToDate", todate);
                cmd.Connection = _con;
                {
                    _con.Open();
                    cmd.ExecuteNonQuery();
                    _con.Close();
                    //oo.MessageBox("Updated successfully.", this.Page);
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Updated successfully.", "S");
                    dataload();
                }
            }
        }
        protected void Button4_Click(object sender, EventArgs e)
        {

        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            _sql = "Delete from SessionMaster where SessionId=" + lblvalue.Text+ " and BranchCode=" + ddlBranch.SelectedValue + "";

            using (var cmd = new SqlCommand())
            {
                cmd.CommandText = _sql;
                cmd.CommandType = CommandType.Text;
                cmd.Connection = _con;
                {
                    _con.Open();
                    cmd.ExecuteNonQuery();
                    _con.Close();
                    //oo.MessageBox("Deleted successfully.", this.Page);
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Deleted successfully.", "S");
                    dataload();
                }
            }
        }
        protected void Button8_Click(object sender, EventArgs e)
        {

        }
        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            Button8.Focus();
            var chk = (LinkButton)sender;
            var lblId = (Label)chk.NamingContainer.FindControl("Label37");
            var ss = lblId.Text;
            lblvalue.Text = ss;
            Panel2_ModalPopupExtender.Show();

        }
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            var chk = (LinkButton)sender;
            var lblId = (Label)chk.NamingContainer.FindControl("Label36");
            var LinkButton3 = (LinkButton)chk.NamingContainer.FindControl("LinkButton3");
            
            var ss = lblId.Text;
            lblID.Text = ss;

            // sql = "Select  distinct ROW_NUMBER() OVER (ORDER BY p.ProductId ASC) AS  [ProductId] ,po.id as ID, pc.ProductCategoryName as[ProductCategoryName], p.ProductName as [ProductName], Pm.ProductTypeName as [ProductTypeName],PO.ProductModelName as [ProductModelName] from Productcategorymaster pc left join ProductName p on p.ProductId=pc.ProductId  left join ProductTypeMaster PM on p.ProductId=PM.ProductId left join ProductModelMaster PO on p.ProductId=PO.ProductId ";        
            _sql = "Select ROW_NUMBER() OVER (ORDER BY SessionId ASC) AS SrNo, SessionId,SessionName,convert(nvarchar,FromDate,106) as FromDate,convert(nvarchar,ToDate,106) as ToDate,Remark , ";
            _sql = _sql + " left(convert(nvarchar,FromDate,106),2) as FDD,Right(left(convert(nvarchar,FromDate,106),6),3) as FMM , RIGHT(convert(nvarchar,FromDate,106),4) as FYY,";
            _sql = _sql + " left(convert(nvarchar,ToDate,106),2) as DDT,Right(left(convert(nvarchar,ToDate,106),6),3) as MMT , RIGHT(convert(nvarchar,ToDate,106),4) as YYT";
            _sql = _sql + "  from SessionMaster";
            _sql = _sql + " where SessionId=" + ss+ " and BranchCode=" + ddlBranch.SelectedValue + "";
            string tempdtFrom = "01-" + _oo.ReturnTag(_sql, "FMM") + "-" + _oo.ReturnTag(_sql, "FYY");
            int totalDaysF = int.Parse(_oo.ReturnTag("select DAY(EOMONTH(convert(datetime,'" + tempdtFrom + "',103))) totalDays", "totalDays"));
            for (int i = 1; i <= totalDaysF; i++)
            {
                DrpFromDDPanel.Items.Add(i.ToString());
            }
            string tempdtTo = "01-" + _oo.ReturnTag(_sql, "mmt") + "-" + _oo.ReturnTag(_sql, "yyT");
            int totalDaysT = int.Parse(_oo.ReturnTag("select DAY(EOMONTH(convert(datetime,'"+ tempdtTo + "',103))) totalDays", "totalDays"));
            for (int i = 1; i <= totalDaysT; i++)
            {
                DrpToPanelDD.Items.Add(i.ToString());
            }



            txtremark0.Text = _oo.ReturnTag(_sql, "Remark");
            txtSessionNamePanel.Text = _oo.ReturnTag(_sql, "SessionName");
            DrpFromMMPanel.Text = _oo.ReturnTag(_sql, "FMM");

            // ReSharper disable once RedundantAssignment
            var t = "";
            if (_oo.ReturnTag(_sql, "FDD").Substring(0, 1) == "0")
            {
                t = _oo.ReturnTag(_sql, "FDD").Substring(1, 1);
            }
            else
            {
                t = _oo.ReturnTag(_sql, "FDD");
            }


            DrpFromDDPanel.Text = t;
            DrpYYFromPanel.Text = _oo.ReturnTag(_sql, "FYY");
            {
                DrpYYToPanel.Text = _oo.ReturnTag(_sql, "yyT");
            }


            if (_oo.ReturnTag(_sql, "DDt").Substring(0, 1) == "0")
            {
                t = _oo.ReturnTag(_sql, "DDt").Substring(1, 1);
            }
            else
            {
                t = _oo.ReturnTag(_sql, "DDt");
            }
        
            DrpToPanelDD.Text = t;
            DrpPaneltoMM.Text = _oo.ReturnTag(_sql, "mmt");

            Panel1_ModalPopupExtender.Show();
        }
        protected void DrpYYFromPanel_SelectedIndexChanged(object sender, EventArgs e)
        {
            _oo.YearDropDown(DrpYYFromPanel, DrpFromMMPanel, DrpFromDDPanel);
            Getsessionyearpanel();
            Panel1_ModalPopupExtender.Show();
        }
        protected void DrpFromMMPanel_SelectedIndexChanged(object sender, EventArgs e)
        {
            _oo.MonthDropDown(DrpYYFromPanel, DrpFromMMPanel, DrpFromDDPanel);
        }
        protected void DrpFromDDPanel_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void DrpYYToPanel_SelectedIndexChanged(object sender, EventArgs e)
        {
            _oo.YearDropDown(DrpYYToPanel, DrpPaneltoMM, DrpToPanelDD);
            Getsessionyearpanel();
            Panel1_ModalPopupExtender.Show();
        }
        protected void DrpPaneltoMM_SelectedIndexChanged(object sender, EventArgs e)
        {
            _oo.MonthDropDown(DrpYYToPanel, DrpPaneltoMM, DrpToPanelDD);
        }
        protected void DrpToPanelDD_SelectedIndexChanged(object sender, EventArgs e)
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
            _sql = _sql + " left join LoginType Lt on LTb.LoginTypeId=Lt.LoginTypeId where LT.LoginTypeName='" + Session["Logintype"] + "' and LTb.LoginName='" + Session["LoginName"] + "' and BranchId=" + ddlBranch.SelectedValue + "";
            int a, u, d;
            a = Convert.ToInt32(_oo.ReturnTag(_sql, "add1"));
            u = Convert.ToInt32(_oo.ReturnTag(_sql, "update1"));
            d = Convert.ToInt32(_oo.ReturnTag(_sql, "delete1"));

            // ReSharper disable once RedundantCast
            PermissionGrant(a, d, u, (LinkButton)LinkButton, btnDelete, Button3);
        }

        public override void Dispose()
        {
            _con.Dispose();
            _oo.Dispose();
        }
    }
}