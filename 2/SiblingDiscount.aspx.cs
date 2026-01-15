using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace _2
{
    public partial class SiblingDiscount : System.Web.UI.Page
    {
        SqlConnection _con = new SqlConnection();
        Campus _oo = new Campus();
        string sql = "";
        string _sql = "";
        string _sql1 ="";
        
        protected void Page_Load(object sender, EventArgs e)
        {
            _con = _oo.dbGet_connection();
            Campus camp = new Campus(); camp.LoadLoader(loader);
            if (!IsPostBack)
            {
                loadFeeGroup(drpFeeGroup, Session["SessionName"].ToString());
                loadClass();
                setDefaultDate();
                ddlFeeGroup.Items.Insert(0, new ListItem("<--Select-->", "-1"));
                ddlInstallmentId.Items.Insert(0, new ListItem("<--Select-->", "-1"));
                ddlFeeHeadId.Items.Insert(0, new ListItem("<--Select-->", "-1"));
                loadData();

            }
        }

        public void getInstallmentNo(CheckBoxList chkInstallment, string classId="-1")
        {
            //_sql1 = "select * from MonthMaster where SessionName='"+ Session["SessionName"].ToString() + "' and ClassId="+ (classId == "-1" ? chkClassList.SelectedValue : classId) + " and CardType="+ drpFeeGroup.SelectedValue + "";
            //chkInstallment.DataSource = _oo.GridFill(_sql1);
            //chkInstallment.DataValueField = "MonthId";
            //chkInstallment.DataTextField = "MonthName";
            //chkInstallment.DataBind();
        }
        private void loadClass()
        {
            ddlClassMain.DataSource = BLL.BLLInstance.getClasseswithValue(Session["SessionName"].ToString());
            ddlClassMain.DataTextField = "ClassName";
            ddlClassMain.DataValueField = "Id";
            ddlClassMain.DataBind();
            ddlClassMain.Items.Insert(0, new ListItem("<--Select-->", ""));


            ddlClass.DataSource = BLL.BLLInstance.getClasseswithValue(Session["SessionName"].ToString());
            ddlClass.DataTextField = "ClassName";
            ddlClass.DataValueField = "Id";
            ddlClass.DataBind();
            ddlClass.Items.Insert(0, new ListItem("<--Select-->", "-1"));
            LoadGrid();


        }
        private void LoadGrid()
        {
            _sql1 = "select * from MonthMaster where ClassId =" + ddlClassMain.SelectedValue + " and CardType =" + drpFeeGroup.SelectedValue + " and sessionname='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            Grd.DataSource = _oo.GridFill(_sql1);
            Grd.DataBind();
            if (Grd.Rows.Count > 0)
            {
                lnkSubmit.Visible = true;
                for (int i = 0; i < Grd.Rows.Count; i++)
                {
                    Label installmentId = (Label)Grd.Rows[i].FindControl("installmentId");
                    CheckBoxList chkFeeHead = (CheckBoxList)Grd.Rows[i].FindControl("chkFeeHead");
                    loadFeeHead(Session["SessionName"].ToString(), chkFeeHead, ddlClassMain.SelectedValue, installmentId.Text);
                }
            }
        }
        private void setDefaultDate()
        {
            bool flag = false;
            DateTime fromdate = new DateTime();
            flag = DateTime.TryParse(BLL.BLLInstance.loadCurrentSessionStartDate(Session["SessionName"].ToString()), out fromdate);
            if (flag)
            {
                FromDate4.Text = fromdate.ToString("dd-MMM-yyyy");
            }

            DateTime todate = new DateTime();
            flag = DateTime.TryParse(BLL.BLLInstance.loadCurrentSessionEndDate(Session["SessionName"].ToString()), out todate);
            if (flag)
            {
                ToDate4.Text = todate.ToString("dd-MMM-yyyy");
            }

        }

        private void loadFeeHead(string sessionName, CheckBoxList chkFeeHead, string classid, string installmentId)
        {
            _sql1 = "select FeeHead, Id from FeeHeadMaster where id in (select FeeHeadId from FeeAllotedForClassWise where MonthId=" + installmentId + " and Classid=" + classid + " and SessionName='" + sessionName + "' and BranchCode=" + Session["BranchCode"] + " and CardType=" + drpFeeGroup.SelectedValue + ") and BranchCode=" + Session["BranchCode"] + "";
            chkFeeHead.DataSource = _oo.GridFill(_sql1);
            chkFeeHead.DataTextField = "FeeHead";
            chkFeeHead.DataValueField = "Id";
            chkFeeHead.DataBind();
        }
        private void loadFeeGroup(DropDownList drpFeeGroup, string sessionName)
        {
            BLL.BLLInstance.loadFeeGroup(drpFeeGroup, sessionName);
            drpFeeGroup.Items.RemoveAt(0);
        }

        private string setFromDate(string FromDate)
        {
            bool flag = false;
            DateTime fromdate = new DateTime();

            if (FromDate == "")
            {
                flag = DateTime.TryParse(BLL.BLLInstance.loadCurrentSessionStartDate(Session["SessionName"].ToString()), out fromdate);
                if (flag)
                {
                    FromDate = fromdate.ToString("dd-MMM-yyyy");
                }
            }
            else
            {
                flag = DateTime.TryParse(FromDate, out fromdate);
                if (flag)
                {
                    FromDate = fromdate.ToString("dd-MMM-yyyy");
                }
            }

            return FromDate;
        }

        private string setToDate(string ToDate)
        {
            bool flag = false;
            DateTime todate = new DateTime();

            if (ToDate == "")
            {
                flag = DateTime.TryParse(BLL.BLLInstance.loadCurrentSessionEndDate(Session["SessionName"].ToString()), out todate);
                if (flag)
                {
                    ToDate = todate.ToString("dd-MMM-yyyy");
                }
            }
            else
            {
                flag = DateTime.TryParse(ToDate, out todate);
                if (flag)
                {
                    ToDate = todate.ToString("dd-MMM-yyyy");
                }
            }

            return ToDate;
        }
        private void loadData(string id="")
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@SiblingCategory", ddlSiblingCategory.SelectedValue));
            if (ddlClass.SelectedIndex!=0)
            {
                param.Add(new SqlParameter("@ClassId", ddlClass.SelectedValue));
            }
            if (id!="")
            {
                param.Add(new SqlParameter("@Id", id));
            }
            if (ddlFeeGroup.SelectedIndex !=0)
            {
                param.Add(new SqlParameter("@FeeGroup", ddlFeeGroup.SelectedValue));
            }
            if (ddlInstallmentId.SelectedIndex != 0)
            {
                param.Add(new SqlParameter("@InstallmentId", ddlInstallmentId.SelectedValue));
            }
            if (ddlFeeHeadId.SelectedIndex != 0)
            {
                param.Add(new SqlParameter("@FeeHeadId", ddlFeeHeadId.SelectedValue));
            }
            param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
            param.Add(new SqlParameter("@Action", "Select"));

            DataSet ds = new DataSet();
            ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("SiblingDiscountProc", param);
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
        
        
        #region onSubmit
        protected void lnkSubmit_Click(object sender, EventArgs e)
        {
            _sql = "select count(*) cnt from SiblingDiscount where SessionName='"+ Session["SessionName"].ToString() + "' and BranchCode="+ Session["BranchCode"].ToString() + "";
            if (_oo.ReturnTag(_sql, "cnt")!="0")
            {
                _sql = "select distinct SiblingType from SiblingDiscount where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
                if (_oo.ReturnTag(_sql, "SiblingType")!= drpSiblingType.SelectedValue.ToString())
                {
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "You have already saved data for " + _oo.ReturnTag(_sql, "SiblingType").ToUpper()+ " sibling, so please select " + _oo.ReturnTag(_sql, "SiblingType").ToUpper() + ".", "A");
                }
            }
            bool chkFeeHeadStatus = false; bool txtValue4Status = false;
            for (int i = 0; i < Grd.Rows.Count; i++)
            {
                Label installmentId = (Label)Grd.Rows[i].FindControl("installmentId");
                CheckBoxList chkFeeHead = (CheckBoxList)Grd.Rows[i].FindControl("chkFeeHead");
                TextBox txtValue4 = (TextBox)Grd.Rows[i].FindControl("txtValue4");
                DropDownList drpMode4 = (DropDownList)Grd.Rows[i].FindControl("drpMode4");
                
                for (int k = 0; k < chkFeeHead.Items.Count; k++)
                {
                    if (chkFeeHead.Items[k].Selected)
                    {
                        chkFeeHeadStatus = true;
                        if (txtValue4.Text != "")
                        {
                            insert(ddlClassMain.SelectedValue, drpFeeGroup.SelectedValue, txtCap4.Text, FromDate4.Text, ToDate4.Text, installmentId.Text, chkFeeHead.Items[k].Value, txtValue4.Text, drpMode4.SelectedValue);
                            txtValue4Status = true;
                        }
                    }
                }
            }
            if (chkFeeHeadStatus == true && txtValue4Status == false)
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Please enter discount value in selected head", "A");
            }
            else if (chkFeeHeadStatus == false && txtValue4Status == false)
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Please select head and enter discount value", "A");
            }
            else
            {
                LoadGrid();
                loadData("");
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Submitted successfully.", "S");
            }
            
        }
        private void insert(string ClassId, string FeeGroup, string Caption, string DateFrom, string DateTo, string InstallmentId, string FeeHeadId, string Discount, string DiscountMode)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SiblingDiscountProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = _con;

            cmd.Parameters.AddWithValue("@SiblingCategory", drpSiblingCategory.SelectedValue);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@FeeGroup", FeeGroup);
            cmd.Parameters.AddWithValue("@Caption", Caption);
            cmd.Parameters.AddWithValue("@DateFrom", DateFrom);
            cmd.Parameters.AddWithValue("@DateTo", DateTo);
            cmd.Parameters.AddWithValue("@InstallmentId", InstallmentId);
            cmd.Parameters.AddWithValue("@FeeHeadId", FeeHeadId);
            cmd.Parameters.AddWithValue("@Discount", Discount);
            cmd.Parameters.AddWithValue("@DiscountMode", DiscountMode);
            cmd.Parameters.AddWithValue("@SiblingType", drpSiblingType.SelectedValue);
            if (drpSiblingType.SelectedValue == "One")
            {
                cmd.Parameters.AddWithValue("@AplicableFor", drpApplyononesibling.SelectedValue);
            }
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
            cmd.Parameters.AddWithValue("@Action", "Insert");
            try
            {
                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }
            catch (Exception ex) { }
        }
        #endregion

        #region onUpdate
        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            Label lblId = (Label)lnk.NamingContainer.FindControl("lblIdDelete");
            Label LabelDateFrom = (Label)lnk.NamingContainer.FindControl("LabelDateFrom");
            Label LabelDateTo = (Label)lnk.NamingContainer.FindControl("LabelDateTo");
            Label LabelDiscount = (Label)lnk.NamingContainer.FindControl("LabelDiscount");
            Label LabelDiscountMode = (Label)lnk.NamingContainer.FindControl("LabelDiscountMode");
            lblFlnkUpdate.Text = lblId.Text;
            FromDate4Panel.Text = LabelDateFrom.Text;
            ToDate4Panel.Text = LabelDateTo.Text;
            txtValue4Panel.Text = LabelDiscount.Text;
            drpMode4Panel.SelectedValue = LabelDiscountMode.Text;

            Panel1_ModalPopupExtender.Show();
        }
        protected void lnkUpdate_Click(object sender, EventArgs e)
        {
            update(lblFlnkUpdate.Text.Trim(), FromDate4Panel.Text.Trim(), ToDate4Panel.Text.Trim(), txtValue4Panel.Text.Trim(), drpMode4Panel.SelectedValue);
        }
        private void update(string Id, string DateFrom, string DateTo, string Discount, string DiscountMode)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SiblingDiscountProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = _con;

            cmd.Parameters.AddWithValue("@Id", Id);
            cmd.Parameters.AddWithValue("@DateFrom", DateFrom);
            cmd.Parameters.AddWithValue("@DateTo", DateTo);
            cmd.Parameters.AddWithValue("@Discount", Discount);
            cmd.Parameters.AddWithValue("@DiscountMode", DiscountMode);
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
            cmd.Parameters.AddWithValue("@Action", "Update");
            try
            {
                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
                loadData(Id);
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Updated successfully", "S");
            }
            catch (Exception) { }
        }
        #endregion

        #region onDelete
        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            Label lblIdDelete = (Label)lnk.NamingContainer.FindControl("lblIdDelete");
            lblvalue.Text = lblIdDelete.Text;
            Panel2_ModalPopupExtender.Show();
        }
        protected void lnkYes_Click(object sender, EventArgs e)
        {
            delete(lblvalue.Text);
        }
        private void delete(string Id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SiblingDiscountProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = _con;

            cmd.Parameters.AddWithValue("@Id", Id);
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@Action", "Delete");
            try
            {
                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
                loadData("");
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Deleted successfully.", "S");
            }
            catch (Exception) { }
        }
        #endregion

        #region onSelectChange
        protected void drpMode4Panel_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtValue4Panel.Text = "";
            Panel1_ModalPopupExtender.Show();
        }
        
        protected void ddlClassMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGrid();
        }


        protected void drpFeeGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGrid();
        }
       
        
        #endregion

        protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            BLL.BLLInstance.loadFeeGroup(ddlFeeGroup, Session["SessionName"].ToString());
        }

        protected void ddlFeeGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlInstallmentId.Items.Clear();
            _sql1 = "select * from MonthMaster where SessionName='" + Session["SessionName"].ToString() + "' and ClassId=" + ddlClass.SelectedValue + " and CardType=" + drpFeeGroup.SelectedValue + "";
            ddlInstallmentId.DataSource = _oo.GridFill(_sql1);
            ddlInstallmentId.DataValueField = "MonthId";
            ddlInstallmentId.DataTextField = "MonthName";
            ddlInstallmentId.DataBind();
            ddlInstallmentId.Items.Insert(0, new ListItem("<--Select-->", "-1"));

            ddlFeeHeadId.Items.Clear();
            _sql1 = "select FeeHead, Id from FeeHeadMaster where id in (select FeeHeadId from FeeAllotedForClassWise where MonthId=case when " + ddlInstallmentId.SelectedValue + "='-1' then MonthId else " + ddlInstallmentId.SelectedValue + " end and Classid=" + ddlClass.SelectedValue + " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and CardType=" + ddlFeeGroup.SelectedValue + ") and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            ddlFeeHeadId.DataSource = _oo.GridFill(_sql1);
            ddlFeeHeadId.DataTextField = "FeeHead";
            ddlFeeHeadId.DataValueField = "Id";
            ddlFeeHeadId.DataBind();
            ddlFeeHeadId.Items.Insert(0, new ListItem("<--Select-->", "-1"));
        }

        protected void LinkView_Click(object sender, EventArgs e)
        {
            loadData("");
        }

        protected void drpMode4_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList chk = (DropDownList)sender;
            TextBox txtValue4 = (TextBox)chk.NamingContainer.FindControl("txtValue4");
            txtValue4.Text = "";
        }

        protected void chkAllDelete_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkAllDelete = (CheckBox)GridView1.HeaderRow.FindControl("chkAllDelete");
            if (chkAllDelete.Checked)
            {
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    CheckBox chkdelete = (CheckBox)GridView1.Rows[i].FindControl("chkdelete");
                    chkdelete.Checked = true;
                }
            }
            else
            {
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    CheckBox chkdelete = (CheckBox)GridView1.Rows[i].FindControl("chkdelete");
                    chkdelete.Checked = false;
                }
            }
        }

        protected void chkdelete_CheckedChanged(object sender, EventArgs e)
        {
            int cnt = 0;
            CheckBox chkAllDelete = (CheckBox)GridView1.HeaderRow.FindControl("chkAllDelete");
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                CheckBox chkdelete = (CheckBox)GridView1.Rows[i].FindControl("chkdelete");
                if (chkdelete.Checked)
                {
                    cnt++;
                }
            }

            if (cnt != GridView1.Rows.Count)
            {
                chkAllDelete.Checked = false;
            }
            else
            {
                chkAllDelete.Checked = true;
            }
        }

        protected void lnkDeleteSelected_Click(object sender, EventArgs e)
        {
            Panel3_ModalPopupExtender.Show();
        }
        protected void lnkDeleteAllYes_Click(object sender, EventArgs e)
        {
            int cnt = 0;
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                CheckBox chkdelete = (CheckBox)GridView1.Rows[i].FindControl("chkdelete");
                Label Id = (Label)GridView1.Rows[i].FindControl("lblIdDelete");

                if (chkdelete.Checked)
                {
                    cnt++;
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "SiblingDiscountProc";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = _con;

                    cmd.Parameters.AddWithValue("@Id", Id.Text);
                    cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
                    cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                    cmd.Parameters.AddWithValue("@Action", "Delete");
                    try
                    {
                        _con.Open();
                        cmd.ExecuteNonQuery();
                        _con.Close();


                    }
                    catch (Exception ex) { }
                }
            }
            if (cnt == 0)
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Please select atleast one!", "A");
            }
            else
            {
                loadData("");
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Deleted successfully.", "S");
            }
        }
        protected void drpSiblingType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpSiblingType.SelectedValue == "One")
            {
                ApplicableClass.Visible = true;
            }
            else
            {
                ApplicableClass.Visible = false;
            }
        }

        protected void drpSiblingCategory_OnTextChanged(object sender, EventArgs e)
        {
            switch (drpSiblingCategory.SelectedIndex)
            {
                case 0:
                    txtCap4.Text = "One Sibling Discount";
                    break;
                case 1:
                    txtCap4.Text = "Two Sibling Discount";
                    break;
                case 2:
                    txtCap4.Text = "Three Sibling Discount";
                    break;
                case 3:
                    txtCap4.Text = "Four Sibling Discount";
                    break;
                default:
                    txtCap4.Text = "Five Sibling Discount";
                    break;
            }
        }
    }
}