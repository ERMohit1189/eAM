using c4SmsNew;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace _1
{
    public partial class AdminAdmissionEnquiry : System.Web.UI.Page
    {
        private SqlConnection _con;
        private readonly Campus _oo;
        private string _sql, _ss = string.Empty;
        public AdminAdmissionEnquiry()
        {
            _con = new SqlConnection();
            _oo = new Campus();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
            {
                Response.Redirect("default.aspx");
            }
            _con = _oo.dbGet_connection();
            Campus camp = new Campus(); camp.LoadLoader(loader);
            if (!IsPostBack)
            {
                try
                {
                    CheckValueAddDeleteUpdate();
                }
                catch (Exception)
                {
                }
                txtEnquirydate.Text = Convert.ToDateTime(BAL.objBal.CurrentDate()).ToString("dd-MMM-yyyy");
                txtFromDate.Text = Convert.ToDateTime(BAL.objBal.CurrentDate()).ToString("dd-MMM-yyyy");
                txtToDate.Text = Convert.ToDateTime(BAL.objBal.CurrentDate()).ToString("dd-MMM-yyyy");
                _oo.AddDateMonthYearDropDown(drpYYPanel, drpMMPanel, drpDDPanel);
                _oo.FindCurrentDateandSetinDropDown(drpYYPanel, drpMMPanel, drpDDPanel);
                LoadCountry();
                LoadState();
                LoadCity();
                LoadRepeater();
                CheckListfill();
                CheckListfill1();
                LoadClass();
                LoadSession();
                ChangeRefrence();
            }
        }
        protected void LnkView_Click(object sender, EventArgs e)
        {
            LoadRepeater();
        }
        public void LoadRepeater()
        {
            _sql = "select ad.EnquiryNo,ad.Id,Cm.CountryName,CS.CityName,Sm.StateName, convert(nvarchar,Ad.Date,106) as Date ,  ad.MobileNo,Ad.AdmissionClass, FatherName,Ad.Name+' '+Ad.MiddleName+' '+Ad.LastName as Name ,Ad.ContactNo ,Ad.EMail ,Ad.Address, format(AD.recorddate, 'dd-MMM-yyyy hh:mm:ss tt') RecordedDate, AD.LoginName, Ad.Status  from AdmissionEnquiry AD";
            _sql = _sql + " left join CountryMaster CM on AD.CountryId=CM.Id";
            _sql = _sql + " left join StateMaster SM on AD.StateId=SM.Id";
            _sql = _sql + " left join CityMaster CS on AD.CityId=CS.Id ";
            _sql = _sql + " where Ad.AdmissionClass=" + ( drpClassForView.SelectedItem == null || drpClassForView.SelectedItem.Text == "<--All-->" ? "Ad.AdmissionClass" : ("'"+drpClassForView.SelectedItem.Text+"'")) + " AND ad.BranchCode=" + Session["BranchCode"] + "";
            _sql = _sql + " and convert(date, AD.Date) between convert(date, '"+txtFromDate.Text.Trim()+ "') and  convert(date, '" + txtToDate.Text.Trim() + "') order by CONVERT(int, (SUBSTRING(Ad.EnquiryNo, 14, len(Ad.EnquiryNo)))) desc";
            Repeater1.DataSource = _oo.GridFill(_sql);
            Repeater1.DataBind();
            if (Repeater1.Items.Count > 0)
            {
                string ss = "select convert(nvarchar,GETDATE(),106) currDate";
                string curdate = _oo.ReturnTag(ss, "currDate");
                divGrid.Visible = true;
                for (int i = 0; i < Repeater1.Items.Count; i++)
                {
                    Label Status = (Label)Repeater1.Items[i].FindControl("Status");
                    Label date = (Label)Repeater1.Items[i].FindControl("date");
                    Label lblLoginName = (Label)Repeater1.Items[i].FindControl("lblLoginName");
                    LinkButton LinkButton1 = (LinkButton)Repeater1.Items[i].FindControl("LinkButton1");
                    if (lblLoginName.Text==Session["LoginName"].ToString() && date.Text.ToString()==curdate && Status.Text== "Pending")
                    {
                        LinkButton1.Text = "<i class='fa fa-pencil'></i>";
                        LinkButton1.Enabled = true;
                    }
                    else
                    {
                        LinkButton1.Text = "<i class='fa fa-lock'></i>";
                        LinkButton1.Enabled = false;
                    }
                }
            }
            else
            {
                divGrid.Visible = false;
            }  
        }
        public void LoadSession()
        {
            _sql = "Select Convert(varchar(8),Datepart(Year,FromDate))+'-'+Convert(varchar(8),Datepart(Year,ToDate)) as Sessionfor from";
            _sql = _sql + " SessionMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            //_sql = _sql + " Union";
            //_sql = _sql + " Select Convert(varchar(8),(Datepart(Year,FromDate)+1))+'-'+Convert(varchar(8),(Datepart(Year,ToDate)+1)) as Sessionfor from ";
            //_sql = _sql + " SessionMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";

            _oo.FillDropDownWithOutSelect(_sql, drpSession, "Sessionfor");
            _oo.FillDropDownWithOutSelect(_sql, drpSessionPanel, "Sessionfor");
            drpSession.Items.Insert(0, new ListItem("<-- Select Session -->", "<-- Select Session -->"));
            drpSessionPanel.Items.Insert(0, new ListItem("<-- Select Session -->", "<-- Select Session -->"));

            drpSession.SelectedIndex = drpSession.Items.Count - 1;
        }
        private void LoadClass()
        {
            _sql = "Select Id,ClassName from ClassMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " order by Cidorder";
            _oo.FillDropDown_withValue(_sql, drpClass, "ClassName", "Id");
            _oo.FillDropDown_withValue(_sql, drpClassPanel, "ClassName", "Id");
            _oo.FillDropDown_withValue(_sql, drpClassForView, "ClassName", "Id");
            drpClass.Items.Insert(0, new ListItem("<--Select-->", "0"));
            drpClassPanel.Items.Insert(0, new ListItem("<--Select-->", "0"));
            drpClassForView.Items.Insert(0, new ListItem("<--All-->", "0"));
        }
        private void LoadCountry()
        {
            _sql = "Select CountryName,id from countryMaster";
            BAL.objBal.FillDropDown_withValue(_sql, drCountry, "CountryName", "id");
            BAL.objBal.FillDropDown_withValue(_sql, drpCountryPanel, "CountryName", "id");
            try
            {
                using (BLL objBll = new BLL())
                {
                    objBll.loadDefaultvalue("Country", drCountry);
                }
            }
            catch
            {
                // ignored
            }
        }
        private void LoadState()
        {
            _sql = "Select StateName,Id from StateMaster where CountryId='" + drCountry.SelectedValue + "'";
            BAL.objBal.FillDropDown_withValue(_sql, drstate, "StateName", "id");
            BAL.objBal.FillDropDown_withValue(_sql, drpStatePanel, "StateName", "id");
            try
            {
                using (BLL objBll = new BLL())
                {
                    objBll.loadDefaultvalue("State", drstate);
                }
            }
            catch
            {
                // ignored
            }
        }
        private void LoadCity()
        {
            _sql = "Select CityName,id from CityMaster where StateId='" + drstate.SelectedValue + "'";
            BAL.objBal.FillDropDown_withValue(_sql, drcity, "CityName", "id");
            BAL.objBal.FillDropDown_withValue(_sql, drpCityPanel, "CityName", "id");
            try
            {
                using (BLL objBll = new BLL())
                {
                    objBll.loadDefaultvalue("City", drcity);
                }
            }
            catch
            {
                // ignored
            }
        }
        public void CheckListfill()
        {
            drpreferenceaden.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
            _sql = "select Id,Name from knowAboutUs order By Id";
            using (SqlCommand cmdFill = new SqlCommand(_sql, _con))
            {
                _con.Open();
                using (SqlDataReader sdr = cmdFill.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        ListItem item = new ListItem
                        {
                            Text = sdr["Name"].ToString(),
                            Value = sdr["Name"].ToString()
                        };
                        drpreferenceaden.Items.Add(item);
                    }
                }
                _con.Close();
            }
        }
        public void CheckListfill1()
        {
            drpReferencePanel.Items.Insert(0, new ListItem("<--Select Reference-->", "<--Select Reference-->"));
            _sql = "select Id,Name from knowAboutUs order By Id";
            using (SqlCommand cmdFill = new SqlCommand(_sql, _con))
            {
                _con.Open();
                using (SqlDataReader sdr = cmdFill.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        ListItem item = new ListItem
                        {
                            Text = sdr["Name"].ToString(),
                            Value = sdr["Name"].ToString()
                        };
                        drpReferencePanel.Items.Add(item);
                    }
                }
                _con.Close();
            }
        }
        protected void drCountry_SelectedIndexChanged(object sender, EventArgs e)
        {

            string cCode;
            _sql = "select Id from CountryMaster where CountryName='" + drCountry.SelectedItem + "'";

            cCode = _oo.ReturnTag(_sql, "id");
            _sql = "Select StateName from StateMaster where Countryid=" + cCode;

            _oo.FillDropDown(_sql, drstate, "StateName");
        }
        protected void drpEnyear_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        protected void drpenmonth_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        protected void drpYYPanel_SelectedIndexChanged(object sender, EventArgs e)
        {
            _oo.YearDropDown(drpYYPanel, drpMMPanel, drpDDPanel);
        }
        protected void drpMMPanel_SelectedIndexChanged(object sender, EventArgs e)
        {
            _oo.MonthDropDown(drpYYPanel, drpMMPanel, drpDDPanel);
        }
        protected void drstate_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cc;
            _sql = "select Id from StateMaster where StateName='" + drstate.SelectedItem + "'";
            cc = _oo.ReturnTag(_sql, "id");
            _sql = "Select CityName from CityMaster where StateId=" + cc;
            _oo.FillDropDown(_sql, drcity, "CityName");
        }
        protected void drcity_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            if (txtNamePanel.ReadOnly == false)
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "AdmissionEnquiryUpdateProc";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = _con;
                    cmd.Parameters.AddWithValue("@CountryId", drpCountryPanel.SelectedValue);
                    cmd.Parameters.AddWithValue("@StateId", drpStatePanel.SelectedValue);
                    cmd.Parameters.AddWithValue("@CityId", drpCityPanel.SelectedValue);
                    cmd.Parameters.AddWithValue("@Id", lblID.Text);
                    cmd.Parameters.AddWithValue("@AdmissionClass", drpClassPanel.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Name", txtNamePanel.Text);
                    cmd.Parameters.AddWithValue("@MiddleName", txtNamePanel0.Text);
                    cmd.Parameters.AddWithValue("@LastName", txtNamePanel1.Text);
                    cmd.Parameters.AddWithValue("@ContactNo", txtContactNoPanel.Text);
                    cmd.Parameters.AddWithValue("@MobileNo", txtMobileNoPanel.Text);
                    cmd.Parameters.AddWithValue("@EMail", txtEmailPanel.Text);
                    cmd.Parameters.AddWithValue("@Address", txtAddressPanel.Text);
                    cmd.Parameters.AddWithValue("@Reference", drpReferencePanel.Text);
                    cmd.Parameters.AddWithValue("@Forr", drpForPanel.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@RelationShip", drpRelationshipPanel.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Remark", txtRemarkPanel.Text);
                    cmd.Parameters.AddWithValue("@FatherName", txtFatherName0.Text);
                    cmd.Parameters.AddWithValue("@Gender", drpgender0.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@EnquiredPerson", txtEnquiredPerson0.Text);
                    cmd.Parameters.AddWithValue("@refrencevalue", "");
                    cmd.Parameters.AddWithValue("@refrencename", txtRefrenceNamePanel.Text);
                    cmd.Parameters.AddWithValue("@Assigenedto", Session["LoginName"].ToString());
                    cmd.Parameters.AddWithValue("@SessionFor", drpSessionPanel.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@AboutInstitution", drpReferencePanel.SelectedValue);
                    try
                    {
                        _con.Open();
                        cmd.ExecuteNonQuery();
                        _con.Close();
                        Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Updated successfully.", "S");
                        _oo.ClearControls(Page);
                        LoadRepeater();
                    }
                    catch (SqlException ee)
                    {
                    }
                }
            }
            else
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Sorry, unable to update!", "W");
            }
        }
        protected void Button4_Click(object sender, EventArgs e)
        {
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            _sql = "Delete from AdmissionEnquiry where Id=" + lblvalue.Text;

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

                    LoadRepeater();
                }
                catch (SqlException ee)
                {
                    throw new Exception("some reason to rethrow", ee);
                }
            }
        }
        protected void Button8_Click(object sender, EventArgs e)
        {
        }
        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            var chk = (LinkButton)sender;
            var lblId = (Label)chk.NamingContainer.FindControl("lblid");
            var ss = lblId.Text;

            lblvalue.Text = ss;
            Panel2_ModalPopupExtender.Show();
        }
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            try
            {
                var chk = (LinkButton)sender;
                var lblId = (Label)chk.NamingContainer.FindControl("lblid");
                var ss = lblId.Text;
                lblID.Text = ss;
                _sql = "select ROW_NUMBER() OVER (ORDER BY ad.Id desc) AS SrNo,ad.Id,ad.CityId, ad.FatherName,ad.Gender,ad.EnquiredPerson,ltrim(Cm.CountryName) as Countryname,CS.CityName as CityName,Sm.StateName as StateName,ltrim(Ad.AdmissionClass) as AdmissionClass ,convert(nvarchar,Ad.Date,106) as Date ,Ad.Name  as name,Ad.MiddleName,Ad.Lastname ,Ad.ContactNo as ContactNo ,Ad.MobileNo as MobileNo,Ad.AboutInstitution,Ad.EMail  as Email,Ad.Address as Address ,ad.Reference as Reference,";
                _sql = _sql + " left(convert(nvarchar,Date,106),2) as DD,Right(left(convert(nvarchar,Date,106),6),3) as MM , RIGHT(convert(nvarchar,Date,106),4) as YY,";
                _sql = _sql + " ad.Forr as Forr,ad.RelationShip as RelationShip ,ad.remark as Remark,ad.EnquiryNo as EnquiryNo,refrencevalue,refrencename,Assigenedto,SessionFor, cm.ID CountryId, sm.id StateId   from AdmissionEnquiry AD";
                _sql = _sql + "   left join CountryMaster CM on AD.CountryId=CM.Id";
                _sql = _sql + " left join StateMaster SM on AD.StateId=SM.Id";
                _sql = _sql + " left join CityMaster CS on AD.CityId=CS.Id ";
                _sql = _sql + " where ad.Id=" + ss;
                try
                {
                    drpClassPanel.SelectedValue = drpClassPanel.Items.FindByText(_oo.ReturnTag(_sql, "AdmissionClass")).Value;
                }
                catch (Exception)
                {
                }
                drpYYPanel.Text = _oo.ReturnTag(_sql, "YY");
                drpMMPanel.Text = _oo.ReturnTag(_sql, "MM");
                var t = "";
                if (_oo.ReturnTag(_sql, "DD").Substring(0, 1) == "0")
                {
                    t = _oo.ReturnTag(_sql, "DD").Substring(1, 1);
                }
                else
                {
                    t = _oo.ReturnTag(_sql, "DD");
                }
                drpDDPanel.Text = t;
                txtNamePanel.Text = _oo.ReturnTag(_sql, "Name");
                txtNamePanel0.Text = _oo.ReturnTag(_sql, "MiddleName");
                txtNamePanel1.Text = _oo.ReturnTag(_sql, "LastName");
                txtContactNoPanel.Text = _oo.ReturnTag(_sql, "ContactNo");
                txtMobileNoPanel.Text = _oo.ReturnTag(_sql, "MobileNo");
                txtEmailPanel.Text = _oo.ReturnTag(_sql, "Email");
                txtAddressPanel.Text = _oo.ReturnTag(_sql, "Address");
                txtRemarkPanel.Text = _oo.ReturnTag(_sql, "Remark");
                drpCountryPanel.SelectedValue = _oo.ReturnTag(_sql, "CountryId");
                drpStatePanel.SelectedValue = _oo.ReturnTag(_sql, "StateId");
                try
                {
                    drpCityPanel.SelectedValue = _oo.ReturnTag(_sql, "CityId");
                }
                catch (Exception)
                {
                    drpCityPanel.SelectedValue = _oo.ReturnTag(_sql, "CityId");
                }
                drpReferencePanel.SelectedValue = _oo.ReturnTag(_sql, "Reference");
                drpForPanel.Text = _oo.ReturnTag(_sql, "Forr");
                drpRelationshipPanel.Text = _oo.ReturnTag(_sql, "RelationShip");
                txtRemarkPanel.Text = _oo.ReturnTag(_sql, "Remark");
                Label9.Text = _oo.ReturnTag(_sql, "EnquiryNo");
                txtFatherName0.Text = _oo.ReturnTag(_sql, "FatherName");
                drpgender0.Text = _oo.ReturnTag(_sql, "Gender");
                txtEnquiredPerson0.Text = _oo.ReturnTag(_sql, "EnquiredPerson");

                txtRefrenceNamePanel.Text = _oo.ReturnTag(_sql, "refrencename");
                txtVarValuePanel.Text = _oo.ReturnTag(_sql, "refrencevalue");
                drpSessionPanel.SelectedValue = drpSessionPanel.Items.FindByText(_oo.ReturnTag(_sql, "SessionFor")).Value;

                drpClassPanel.Enabled = true;
                drpYYPanel.Enabled = true;
                drpMMPanel.Enabled = true;
                drpDDPanel.Enabled = true;
                txtNamePanel.Enabled = true;
                txtNamePanel0.Enabled = true;
                txtNamePanel1.Enabled = true;
                txtContactNoPanel.Enabled = true;
                txtMobileNoPanel.Enabled = true;
                txtEmailPanel.Enabled = true;
                txtAddressPanel.Enabled = true;
                txtRemarkPanel.Enabled = true;
                drpCountryPanel.Enabled = true;
                drpStatePanel.Enabled = true;
                drpCityPanel.Enabled = true;
                drpReferencePanel.Enabled = true;
                drpForPanel.Enabled = true;
                drpRelationshipPanel.Enabled = true;
                txtRemarkPanel.Enabled = true;
                Label9.Enabled = true;
                txtFatherName0.Enabled = true;
                drpgender0.Enabled = true;
                txtEnquiredPerson0.Enabled = true;
                txtRefrenceNamePanel.Enabled = true;
                txtVarValuePanel.Enabled = true;
                drpSessionPanel.Enabled = true;
                Button3.Visible = true;
            }
            catch
            {
                // ignored
            }
            Panel1_ModalPopupExtender.Show();


        }
        protected void drpCountryPanel_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cCode;
            _sql = "select Id from CountryMaster where CountryName='" + drCountry.SelectedItem + "'";

            cCode = _oo.ReturnTag(_sql, "id");
            _sql = "Select StateName from StateMaster where Countryid=" + cCode;

            _oo.FillDropDown(_sql, drstate, "StateName");
        }
        protected void drpStatePanel_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cc ;
            _sql = "select Id from StateMaster where StateName='" + drstate.SelectedItem + "'";
            cc = _oo.ReturnTag(_sql, "id");
            _sql = "Select CityName from CityMaster where StateId=" + cc;
            _oo.FillDropDown(_sql, drcity, "CityName");
        }
        protected void drpadm_SelectedIndexChanged(object sender, EventArgs e)
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
            _sql = _sql + " left join LoginType Lt on LTb.LoginTypeId=Lt.LoginTypeId where LT.LoginTypeName='" + Session["Logintype"] + "' and LTb.LoginName='" + Session["LoginName"] + "' and LTb.BranchId=" + Session["BranchCode"] + "";
            // ReSharper disable once UnusedVariable
            var a = Convert.ToInt32(_oo.ReturnTag(_sql, "add1"));
            // ReSharper disable once UnusedVariable
            var u = Convert.ToInt32(_oo.ReturnTag(_sql, "update1"));
            // ReSharper disable once UnusedVariable
            var d = Convert.ToInt32(_oo.ReturnTag(_sql, "delete1"));

            //PermissionGrant(a, d, u, (LinkButton)LinkButton1, btnDelete, Button3);
        }
        public string IdGeneration(string x)
        {
            var xx = "";
            switch (x.Length)
            {
                case 1:
                    xx = "00000" + x;
                    break;
                case 2:
                    xx = "0000" + x;
                    break;
                case 3:
                    xx = "000" + x;
                    break;
                case 4:
                    xx = "00" + x;
                    break;
                case 5:
                    xx = xx + "0" + x;
                    break;
                default:
                    xx = x;
                    break;
            }
            return "AE/" + Session["SessionName"] + "/" + xx;
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            _sql = "select *from AdmissionEnquiry where Name='" + txtnamead.Text + "' and ContactNo='" + txtcontAdm.Text + "' and AdmissionClass=" + drpClass.SelectedItem + "'";
            _sql = _sql + "  and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";

            if (_oo.Duplicate(_sql))
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Duplicate Entry!", "A");
            }
            else
            {
                _sql = "select Max(Id)+1 as Id  from AdmissionEnquiry ";
                _ss = _oo.ReturnTag(_sql, "Id");
                _ss = IdGeneration(_ss == "" ? "1" : _ss);

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "AdmissionEnquiryProc";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = _con;
                    cmd.Parameters.AddWithValue("@CountryId", drCountry.SelectedValue);
                    cmd.Parameters.AddWithValue("@StateId", drstate.SelectedValue);
                    cmd.Parameters.AddWithValue("@CityId", drcity.SelectedValue);
                    cmd.Parameters.AddWithValue("@Date", txtEnquirydate.Text.Trim());
                    cmd.Parameters.AddWithValue("@AdmissionClass", drpClass.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Name", txtnamead.Text);
                    cmd.Parameters.AddWithValue("@MiddleName", txtnamead0.Text);
                    cmd.Parameters.AddWithValue("@LastName", txtnamead1.Text);
                    cmd.Parameters.AddWithValue("@ContactNo", txtcontAdm.Text);
                    cmd.Parameters.AddWithValue("@MobileNo", txtmobAdmiss.Text);
                    cmd.Parameters.AddWithValue("@EMail", txtemaAdmiss.Text);
                    cmd.Parameters.AddWithValue("@Address", txtAddressAdmiss.Text);
                    cmd.Parameters.AddWithValue("@Reference", drpreferenceaden.SelectedValue);
                    cmd.Parameters.AddWithValue("@Forr", drpforade.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@RelationShip", drrelat.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                    cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                    cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
                    cmd.Parameters.AddWithValue("@AboutInstitution", drpreferenceaden.SelectedValue);
                    cmd.Parameters.AddWithValue("@Remark", TextBox1.Text);
                    cmd.Parameters.AddWithValue("@EnquiryNo", _ss);
                    cmd.Parameters.AddWithValue("@FatherName", txtFatherName.Text);
                    cmd.Parameters.AddWithValue("@Gender", drpgender.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@EnquiredPerson", txtEnquiredPerson.Text);
                    cmd.Parameters.AddWithValue("@refrencevalue", "");
                    cmd.Parameters.AddWithValue("@refrencename", txtName.Text);
                    cmd.Parameters.AddWithValue("@Assigenedto", Session["LoginName"].ToString());
                    cmd.Parameters.AddWithValue("@Status", "Pending");
                    cmd.Parameters.AddWithValue("@SessionFor", drpSession.SelectedItem.ToString());
                    try
                    {
                        _con.Open();
                        cmd.ExecuteNonQuery();
                        _con.Close();
                        Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Submitted successfully.", "S");
                        SendFeesSms(txtcontAdm.Text.Trim(), _ss.ToString(), txtEnquiredPerson.Text.Trim());
                        _oo.ClearControls(Page);
                        txtEnquirydate.Text = Convert.ToDateTime(BAL.objBal.CurrentDate()).ToString("dd-MMM-yyyy");
                        txtFromDate.Text = Convert.ToDateTime(BAL.objBal.CurrentDate()).ToString("dd-MMM-yyyy");
                        txtToDate.Text = Convert.ToDateTime(BAL.objBal.CurrentDate()).ToString("dd-MMM-yyyy");
                        LoadGrid();
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
        }
        public void SendFeesSms(string fmobileNo, string RefNo, string VistorName)
        {
            string ddd = "select ShortCode from BranchTab where BranchId=" + Session["BranchCode"] + "";
            _sql = "Select HitValue from SMSActivateDeactivate where BranchCode=" + Session["BranchCode"] + "";
            string ph = "select top(1) Phone from CollegeMaster where BranchCode=" + Session["BranchCode"] + "";
            if (_oo.ReturnTag(_sql, "HitValue") != "")
            {
                if (_oo.ReturnTag(_sql, "HitValue") == "true")
                {
                    SMSAdapterNew sadpNew = new SMSAdapterNew();
                    var mess = "Dear Mr./Ms. "+ VistorName + ", Thank you for visiting at " + _oo.ReturnTag(ddd, "ShortCode") + ". Your enquiry Ref. No. " + RefNo + ". For more details contact us at "+ _oo.ReturnTag(ph, "Phone") + ".";
                    if (fmobileNo != "")
                    {
                        _sql = "Select SmsSent From SmsEmailMaster where Id='28' ";
                        if (_oo.ReturnTag(_sql, "SmsSent").Trim() == "true")
                        {
                            sadpNew.Send(mess, fmobileNo);
                        }
                    }
                }
            }
        }
        public void LoadGrid()
        {
            LoadRepeater();
        }
        public void LoadRefrences()
        {
            drpreferenceaden.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
            drpreferenceaden.Items.Insert(1, new ListItem("Alumni", "Alumni"));
            drpreferenceaden.Items.Insert(2, new ListItem("Campaign", "Campaign"));
            drpreferenceaden.Items.Insert(3, new ListItem("Digital Media", "Digital Media"));
            drpreferenceaden.Items.Insert(4, new ListItem("Google", "Google"));
            drpreferenceaden.Items.Insert(5, new ListItem("Hoarding", "Hoarding"));
            drpreferenceaden.Items.Insert(6, new ListItem("Magazine", "Magazine"));
            drpreferenceaden.Items.Insert(7, new ListItem("Newspaper", "Newspaper"));
            drpreferenceaden.Items.Insert(8, new ListItem("Other", "Other"));
            drpreferenceaden.Items.Insert(9, new ListItem("Pamphlet", "Pamphlet"));
            drpreferenceaden.Items.Insert(10, new ListItem("Parents", "Parents"));
            drpreferenceaden.Items.Insert(11, new ListItem("Radio", "Radio"));
            drpreferenceaden.Items.Insert(12, new ListItem("Staff", "Staff"));
            drpreferenceaden.Items.Insert(13, new ListItem("Student", "Student"));
            drpreferenceaden.Items.Insert(14, new ListItem("Television", "Television"));
            drpreferenceaden.Items.Insert(15, new ListItem("Walk in", "Walk in"));
            drpreferenceaden.Items.Insert(16, new ListItem("Website", "Website"));
            drpreferenceaden.Items.Insert(17, new ListItem("Word of Mouth", "Word of Mouth"));
            drpReferencePanel.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
            drpReferencePanel.Items.Insert(1, new ListItem("Alumni", "Alumni"));
            drpReferencePanel.Items.Insert(2, new ListItem("Campaign", "Campaign"));
            drpReferencePanel.Items.Insert(3, new ListItem("Digital Media", "Digital Media"));
            drpReferencePanel.Items.Insert(4, new ListItem("Google", "Google"));
            drpReferencePanel.Items.Insert(5, new ListItem("Hoarding", "Hoarding"));
            drpReferencePanel.Items.Insert(6, new ListItem("Magazine", "Magazine"));
            drpReferencePanel.Items.Insert(7, new ListItem("Newspaper", "Newspaper"));
            drpReferencePanel.Items.Insert(8, new ListItem("Other", "Other"));
            drpReferencePanel.Items.Insert(9, new ListItem("Pamphlet", "Pamphlet"));
            drpReferencePanel.Items.Insert(10, new ListItem("Parents", "Parents"));
            drpReferencePanel.Items.Insert(11, new ListItem("Radio", "Radio"));
            drpReferencePanel.Items.Insert(12, new ListItem("Staff", "Staff"));
            drpReferencePanel.Items.Insert(13, new ListItem("Student", "Student"));
            drpReferencePanel.Items.Insert(14, new ListItem("Television", "Television"));
            drpReferencePanel.Items.Insert(15, new ListItem("Walk in", "Walk in"));
            drpReferencePanel.Items.Insert(16, new ListItem("Website", "Website"));
            drpReferencePanel.Items.Insert(17, new ListItem("Word of Mouth", "Word of Mouth"));
        }
        protected void drpreferenceaden_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeRefrence();
        }
        public void ChangeRefrence()
        {
            txtName.Text = "";
            txtVarValue.Text = "";
            lblref.Text = "Enter Name";
            if (drpreferenceaden.SelectedIndex == 6)
            {
                lblref.Text = "Enter Website";
            }
        }
        protected void txtVarValue_TextChanged(object sender, EventArgs e)
        {
            txtName.Text = txtVarValue.Text;
        }
        protected void txtVarValuePanel_TextChanged(object sender, EventArgs e)
        {
            Panel1_ModalPopupExtender.Show();
            txtRefrenceNamePanel.Text = txtVarValuePanel.Text;
            
        }
        protected void drpReferencePanel_SelectedIndexChanged(object sender, EventArgs e)
        {
            Panel1_ModalPopupExtender.Show();
            txtRefrenceNamePanel.Text = "";
            txtVarValuePanel.Text = "";
        }
        public override void Dispose()
        {
            _con.Dispose();
            _oo.Dispose();
        }



        protected void LinkViewFull_Click(object sender, EventArgs e)
        {
            try
            {
                var chk = (LinkButton)sender;
                var lblId = (Label)chk.NamingContainer.FindControl("lblid");
                var ss = lblId.Text;
                lblID.Text = ss;
                _sql = "select ROW_NUMBER() OVER (ORDER BY ad.Id desc) AS SrNo,ad.Id,ad.CityId, ad.FatherName,ad.Gender,ad.EnquiredPerson,ltrim(Cm.CountryName) as Countryname,CS.CityName as CityName,Sm.StateName as StateName,ltrim(Ad.AdmissionClass) as AdmissionClass ,convert(nvarchar,Ad.Date,106) as Date ,Ad.Name  as name,Ad.MiddleName,Ad.Lastname ,Ad.ContactNo as ContactNo ,Ad.MobileNo as MobileNo,Ad.AboutInstitution,Ad.EMail  as Email,Ad.Address as Address ,ad.Reference as Reference,";
                _sql += " left(convert(nvarchar,Date,106),2) as DD,Right(left(convert(nvarchar,Date,106),6),3) as MM , RIGHT(convert(nvarchar,Date,106),4) as YY,";
                _sql += " ad.Forr as Forr,ad.RelationShip as RelationShip ,ad.remark as Remark,ad.EnquiryNo as EnquiryNo,refrencevalue,refrencename,Assigenedto,SessionFor, cm.ID CountryId, sm.id StateId   from AdmissionEnquiry AD";
                _sql += "   left join CountryMaster CM on AD.CountryId=CM.Id";
                _sql += " left join StateMaster SM on AD.StateId=SM.Id";
                _sql += " left join CityMaster CS on AD.CityId=CS.Id ";
                _sql += " where ad.Id=" + ss;
                try
                {
                    drpClassPanel.SelectedValue = drpClassPanel.Items.FindByText(_oo.ReturnTag(_sql, "AdmissionClass")).Value;
                }
                catch (Exception)
                {
                }
                drpYYPanel.Text = _oo.ReturnTag(_sql, "YY");
                drpMMPanel.Text = _oo.ReturnTag(_sql, "MM");
                var t = "";
                if (_oo.ReturnTag(_sql, "DD").Substring(0, 1) == "0")
                {
                    t = _oo.ReturnTag(_sql, "DD").Substring(1, 1);
                }
                else
                {
                    t = _oo.ReturnTag(_sql, "DD");
                }
                drpDDPanel.Text = t;
                txtNamePanel.Text = _oo.ReturnTag(_sql, "Name");
                txtNamePanel0.Text = _oo.ReturnTag(_sql, "MiddleName");
                txtNamePanel1.Text = _oo.ReturnTag(_sql, "LastName");
                txtContactNoPanel.Text = _oo.ReturnTag(_sql, "ContactNo");
                txtMobileNoPanel.Text = _oo.ReturnTag(_sql, "MobileNo");
                txtEmailPanel.Text = _oo.ReturnTag(_sql, "Email");
                txtAddressPanel.Text = _oo.ReturnTag(_sql, "Address");
                txtRemarkPanel.Text = _oo.ReturnTag(_sql, "Remark");
                drpCountryPanel.SelectedValue = _oo.ReturnTag(_sql, "CountryId");
                drpStatePanel.SelectedValue = _oo.ReturnTag(_sql, "StateId");
                try
                {
                    drpCityPanel.SelectedValue = _oo.ReturnTag(_sql, "CityId");
                }
                catch (Exception)
                {
                    drpCityPanel.SelectedValue = _oo.ReturnTag(_sql, "CityId");
                }
                drpReferencePanel.SelectedValue = _oo.ReturnTag(_sql, "Reference");
                drpForPanel.Text = _oo.ReturnTag(_sql, "Forr");
                drpRelationshipPanel.Text = _oo.ReturnTag(_sql, "RelationShip");
                txtRemarkPanel.Text = _oo.ReturnTag(_sql, "Remark");
                Label9.Text = _oo.ReturnTag(_sql, "EnquiryNo");
                txtFatherName0.Text = _oo.ReturnTag(_sql, "FatherName");
                drpgender0.Text = _oo.ReturnTag(_sql, "Gender");
                txtEnquiredPerson0.Text = _oo.ReturnTag(_sql, "EnquiredPerson");

                txtRefrenceNamePanel.Text = _oo.ReturnTag(_sql, "refrencename");
                txtVarValuePanel.Text = _oo.ReturnTag(_sql, "refrencevalue");
                drpSessionPanel.SelectedValue = drpSessionPanel.Items.FindByText(_oo.ReturnTag(_sql, "SessionFor")).Value;



                drpClassPanel.Enabled = false;
                drpYYPanel.Enabled = false;
                drpMMPanel.Enabled = false;
                drpDDPanel.Enabled = false;
                txtNamePanel.Enabled = false;
                txtNamePanel0.Enabled = false;
                txtNamePanel1.Enabled = false;
                txtContactNoPanel.Enabled = false;
                txtMobileNoPanel.Enabled = false;
                txtEmailPanel.Enabled = false;
                txtAddressPanel.Enabled = false;
                txtRemarkPanel.Enabled = false;
                drpCountryPanel.Enabled = false;
                drpStatePanel.Enabled = false;
                drpCityPanel.Enabled = false;
                drpReferencePanel.Enabled = false;
                drpForPanel.Enabled = false;
                drpRelationshipPanel.Enabled = false;
                txtRemarkPanel.Enabled = false;
                Label9.Enabled = false;
                txtFatherName0.Enabled = false;
                drpgender0.Enabled = false;
                txtEnquiredPerson0.Enabled = false;
                txtRefrenceNamePanel.Enabled = false;
                txtVarValuePanel.Enabled = false;
                drpSessionPanel.Enabled = false;
                Button3.Visible = false;
            }
            catch
            {
                // ignored
            }
            Panel1_ModalPopupExtender.Show();

        }

    }
}