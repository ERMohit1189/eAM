using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _2
{
    public partial class AdminAdmissionFormFeeMaster : Page
    {
        private SqlConnection _con;
        private SqlCommand _cmd;
        private readonly Campus _oo;
        private string _sql = String.Empty;

        public AdminAdmissionFormFeeMaster()
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

            if (!IsPostBack)
            {
                Loadclass();
                drpBranch.Items.Insert(0, "<--Select-->");
                Loadgrid();
            }
        }
        protected void drpAdmissionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Loadgrid();
        }
        protected void Loadgrid()
        {
            _sql = "Select affm.Id,cm.ClassName,Amount, AdminssionType,affm.Gender, FeeHead,Concession,ISNULL(BranchName,'Other') Branch from AdmissionFormFeeMaster affm inner join ClassMaster cm on cm.Id=affm.Classid left join BranchMaster bm on bm.Id=affm.BranchId and bm.SessionName=affm.SessionName where cm.SessionName='" + Session["SessionName"] + "' and bm.BranchCode=" + Session["BranchCode"] + " and affm.BranchCode=" + Session["BranchCode"] + " and cm.BranchCode=" + Session["BranchCode"] + " and affm.SessionName='" + Session["SessionName"] + "'";
            if (drpAdmissionType.SelectedIndex!=0)
            {
                _sql = _sql+ " and affm.AdminssionType='" + drpAdmissionType.SelectedValue + "'";
            }
            if (drpClass.SelectedIndex != 0)
            {
                _sql = _sql + " and affm.classid=" + drpClass.SelectedValue + "";
            }
            if (drpBranch.SelectedIndex != 0)
            {
                _sql = _sql+ " and affm.Branchid=" + drpBranch.SelectedValue + "";
            }
            if (ddlGender.SelectedIndex != 0)
            {
                _sql = _sql + " and isnull(affm.Gender,'')=case when isnull(affm.Gender,'')='' then isnull(affm.Gender,'') else '" + ddlGender.SelectedValue + "' end";
            }
            GridView1.DataSource = _oo.GridFill(_sql);
            GridView1.DataBind();
        }

        protected void Loadclass()
        {
            _sql = "Select ClassName,Id From ClassMaster where SessionName='"+Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " Order by CidOrder";
            _oo.FillDropDown_withValue(_sql, drpClass, "ClassName","Id");
            _oo.FillDropDown_withValue(_sql, DropDownList2, "ClassName", "Id");
            drpClass.Items.Insert(0, "<--Select-->");
        }
        protected void lnkSubmit_Click(object sender, EventArgs e)
        {
            if (drpClass.SelectedIndex != 0)
            {
                _sql = "Select Gender from AdmissionFormFeeMaster where ClassId='" + drpClass.SelectedValue + "' and BranchId='" + drpBranch.SelectedValue + "' and Gender=" + ddlGender.SelectedValue + " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                if (_oo.Duplicate(_sql) == false)
                {
                    Save();
                    Loadgrid();
                }
                else
                {
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Duplicate record!", "A");   
                }
            }
            else
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Select class!", "A");
            }
        }

        protected void Save()
        {
            int sts = 0;
            Campus camp = new Campus();
            if (drpAdmissionType.SelectedValue == "")
            {
                for (int i = 0; i < 3; i++)
                {
                    if (ddlGender.SelectedValue == "")
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            _cmd = new SqlCommand();
                            _cmd.CommandText = "AdmissionFormFeeMasterProc";
                            _cmd.CommandType = CommandType.StoredProcedure;
                            _cmd.Connection = _con;
                            _cmd.Parameters.AddWithValue("@Type", "Insert");
                            _cmd.Parameters.AddWithValue("@Id", "");
                            if (i == 0)
                            {
                                _cmd.Parameters.AddWithValue("@AdminssionType", "New");
                            }
                            if (i == 1)
                            {
                                _cmd.Parameters.AddWithValue("@AdminssionType", "Old");
                            }
                            if (i == 2)
                            {
                                _cmd.Parameters.AddWithValue("@AdminssionType", "New (Provisional)");
                            }
                            _cmd.Parameters.AddWithValue("@FeeHead", txtFeeHead.Text);
                            _cmd.Parameters.AddWithValue("@classid", drpClass.SelectedValue);
                            if (j == 0)
                            {
                                _cmd.Parameters.AddWithValue("@Gender", "Male");
                            }
                            if (j == 1)
                            {
                                _cmd.Parameters.AddWithValue("@Gender", "Female");
                            }
                            if (j == 2)
                            {
                                _cmd.Parameters.AddWithValue("@Gender", "Transgender");
                            }
                            _cmd.Parameters.AddWithValue("@Amount", txtAmount.Text);
                            _cmd.Parameters.AddWithValue("@Concession", txtConcession.Text == "" ? "0" : txtConcession.Text);
                            _cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                            _cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                            _cmd.Parameters.AddWithValue("@Branchid", drpBranch.SelectedValue);
                            _con.Open();
                            _cmd.ExecuteNonQuery();
                            _con.Close();
                            sts = sts + 1;
                        }
                    }
                    else
                    {
                        _cmd = new SqlCommand();
                        _cmd.CommandText = "AdmissionFormFeeMasterProc";
                        _cmd.CommandType = CommandType.StoredProcedure;
                        _cmd.Connection = _con;
                        _cmd.Parameters.AddWithValue("@Type", "Insert");
                        _cmd.Parameters.AddWithValue("@Id", "");
                        if (i == 0)
                        {
                            _cmd.Parameters.AddWithValue("@AdminssionType", "New");
                        }
                        if (i == 1)
                        {
                            _cmd.Parameters.AddWithValue("@AdminssionType", "Old");
                        }
                        if (i == 2)
                        {
                            _cmd.Parameters.AddWithValue("@AdminssionType", "New (Provisional)");
                        }
                        _cmd.Parameters.AddWithValue("@FeeHead", txtFeeHead.Text);
                        _cmd.Parameters.AddWithValue("@classid", drpClass.SelectedValue);
                        _cmd.Parameters.AddWithValue("@Gender", ddlGender.SelectedValue);
                        _cmd.Parameters.AddWithValue("@Amount", txtAmount.Text);
                        _cmd.Parameters.AddWithValue("@Concession", txtConcession.Text == "" ? "0" : txtConcession.Text);
                        _cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                        _cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                        _cmd.Parameters.AddWithValue("@Branchid", drpBranch.SelectedValue);
                        _con.Open();
                        _cmd.ExecuteNonQuery();
                        _con.Close();
                        sts = sts + 1;
                    }
                }

            }
            else
            {
                if (ddlGender.SelectedValue == "")
                {
                    for (int j = 0; j < 3; j++)
                    {
                        _cmd = new SqlCommand();
                        _cmd.CommandText = "AdmissionFormFeeMasterProc";
                        _cmd.CommandType = CommandType.StoredProcedure;
                        _cmd.Connection = _con;
                        _cmd.Parameters.AddWithValue("@Type", "Insert");
                        _cmd.Parameters.AddWithValue("@Id", "");
                        _cmd.Parameters.AddWithValue("@AdminssionType", drpAdmissionType.SelectedValue);
                        _cmd.Parameters.AddWithValue("@classid", drpClass.SelectedValue);
                        if (j == 0)
                        {
                            _cmd.Parameters.AddWithValue("@Gender", "Male");
                        }
                        if (j == 1)
                        {
                            _cmd.Parameters.AddWithValue("@Gender", "Female");
                        }
                        if (j == 2)
                        {
                            _cmd.Parameters.AddWithValue("@Gender", "Transgender");
                        }
                        _cmd.Parameters.AddWithValue("@FeeHead", txtFeeHead.Text);
                        _cmd.Parameters.AddWithValue("@Amount", txtAmount.Text);
                        _cmd.Parameters.AddWithValue("@Concession", txtConcession.Text == "" ? "0" : txtConcession.Text);
                        _cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                        _cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                        _cmd.Parameters.AddWithValue("@Branchid", drpBranch.SelectedValue);
                        _con.Open();
                        _cmd.ExecuteNonQuery();
                        _con.Close();
                        sts = sts + 1;
                    }
                }
                else
                {
                    _cmd = new SqlCommand();
                    _cmd.CommandText = "AdmissionFormFeeMasterProc";
                    _cmd.CommandType = CommandType.StoredProcedure;
                    _cmd.Connection = _con;
                    _cmd.Parameters.AddWithValue("@Type", "Insert");
                    _cmd.Parameters.AddWithValue("@Id", "");
                    _cmd.Parameters.AddWithValue("@AdminssionType", drpAdmissionType.SelectedValue);
                    _cmd.Parameters.AddWithValue("@classid", drpClass.SelectedValue);
                    _cmd.Parameters.AddWithValue("@Gender", ddlGender.SelectedValue);
                    _cmd.Parameters.AddWithValue("@FeeHead", txtFeeHead.Text);
                    _cmd.Parameters.AddWithValue("@Amount", txtAmount.Text);
                    _cmd.Parameters.AddWithValue("@Concession", txtConcession.Text == "" ? "0" : txtConcession.Text);
                    _cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                    _cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                    _cmd.Parameters.AddWithValue("@Branchid", drpBranch.SelectedValue);
                    _con.Open();
                    _cmd.ExecuteNonQuery();
                    _con.Close();
                    sts = sts + 1;
                }
                txtAmount.Text = "";
            }
            if (sts > 0)
            {
                camp.msgbox(Page, msgbox, "Submitted successfully", "S");
                Loadgrid();
            }
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            LinkButton chk = (LinkButton)sender;
            Label lnk = (Label)chk.NamingContainer.FindControl("Label36");
            lblId.Text = lnk.Text;
            _sql = "Select Id,ClassId,Amount,Concession, Feehead from AdmissionFormFeeMaster where Id='" + lblId.Text + "' and SessionName='" + Session["SessionName"] + "' and BranchCode="+Session["BranchCode"]+"";
            DropDownList2.SelectedValue = _oo.ReturnTag(_sql, "ClassId");
            txtFeehead0.Text = _oo.ReturnTag(_sql, "Feehead");
            txtAmount0.Text = _oo.ReturnTag(_sql, "Amount");
            txtConcession0.Text = _oo.ReturnTag(_sql, "Concession");
            Panel1_ModalPopupExtender.Show();
        }
        protected void lnkUpdate_Click(object sender, EventArgs e)
        {
        
            Update();
            Loadgrid();  
      
        }
        protected void Update()
        {
            _cmd = new SqlCommand();
            _cmd.CommandText = "AdmissionFormFeeMasterProc";
            _cmd.CommandType = CommandType.StoredProcedure;
            _cmd.Connection = _con;
            _cmd.Parameters.AddWithValue("@Type", "Update");
            _cmd.Parameters.AddWithValue("@Id", lblId.Text);
            _cmd.Parameters.AddWithValue("@classid", "");
            _cmd.Parameters.AddWithValue("@Amount", txtAmount0.Text);
            _cmd.Parameters.AddWithValue("@FeeHead", txtFeehead0.Text);
            _cmd.Parameters.AddWithValue("@Concession", txtConcession0.Text == "" ? "0" : txtConcession0.Text);
            _cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
            _cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());

            _con.Open();
            _cmd.ExecuteNonQuery();
            _con.Close();
            Loadgrid();
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Updated successfully.", "S");       


        }
        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            Button8.Focus();
            LinkButton chk = (LinkButton)sender;
            // ReSharper disable once LocalVariableHidesMember
            Label lblId = (Label)chk.NamingContainer.FindControl("Label37");

            lblvalue.Text = lblId.Text;
            Panel2_ModalPopupExtender.Show();
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            _sql = "Delete From AdmissionFormFeeMaster where Id='"+lblvalue.Text+ "' and BranchCode=" + Session["BranchCode"] + "";
            _cmd = new SqlCommand(_sql, _con);
            _con.Open();
            _cmd.ExecuteNonQuery();
            _con.Close();
            Loadgrid();
            //oo.MessageBoxforUpdatePanel("Record deleted successfully", btnDelete);
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Deleted successfully.", "S");       

        }

        protected void drpClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadBranch();
            Loadgrid();
        }

        private void LoadBranch()
        {
            _sql = "Select BranchName,Id from BranchMaster where Classid='" + drpClass.SelectedValue + "' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            BAL.objBal.FillDropDown_withValue(_sql, drpBranch, "BranchName", "Id");
            drpBranch.Items.Insert(0, new ListItem("<-- Select-->", "<-- Select-->"));
        }

        public override void Dispose()
        {
            _con.Dispose();
            _oo.Dispose();
        }

        
    }
}