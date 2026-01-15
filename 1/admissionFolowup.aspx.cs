using c4SmsNew;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace _1
{
    public partial class admissionFolowup : System.Web.UI.Page
    {
        private SqlConnection _con;
        private readonly Campus _oo;
        private string _sql, _ss = string.Empty;
        public admissionFolowup()
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
                
            }
        }
        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            loadDetails();
        }
        protected void Button11_Click(object sender, EventArgs e)
        {

            loadDetails();
        }
        protected void loadDetails()
        {
            if (TextBox1.Text != "")
            {
                TextBox1.Text = TextBox1.Text.Replace(" ", string.Empty);
            }
            if (TextBox1.Text == "")
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgView, "Please enter Enquiry No.!", "A");
                return;
            }
            string _sql2 = "select format(date, 'dd-MMM-yyyy')EnqDate, EnquiryNo, (name+' '+MiddleName+' '+LastName) StudentName, FatherName, ContactNo, EnquiredPerson as vistorName, AdmissionClass, Status from AdmissionEnquiry where EnquiryNo='" + TextBox1.Text.Trim() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            if (!_oo.Duplicate(_sql2))
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgView, "Invalid Enquiry No.!", "A");
                divTools.Visible = false;
                divGrid.Visible = false;
                divDetails.Visible = false;
                Grd.DataSource = null;
                Grd.DataBind();
                return;
            }
            else
            {
                divGrid.Visible = true;
                divDetails.Visible = true;
                Grd.DataSource = _oo.Fetchdata(_sql2);
                Grd.DataBind();
                LoadRepeater();
                if (_oo.ReturnTag(_sql2, "Status").ToLower() != "registered")
                {
                    divTools.Visible = true;
                }
                else
                {
                    divTools.Visible = false;
                }
            }
        }
        public void LoadRepeater()
        {
            _sql = "select id, EnquiryNo, Desctiption, format(date, 'dd-MMM-yyyy hh:mm:ss tt')FallowDate,LoginName, Status,BranchCode from AdmissionFollowup ";
            _sql = _sql + " where  EnquiryNo='" + TextBox1.Text.Trim() + "' and BranchCode=" + Session["BranchCode"].ToString() + " order by Id desc";
            Repeater1.DataSource = _oo.GridFill(_sql);
            Repeater1.DataBind();
            if (Repeater1.Items.Count > 0)
            {
                divGrid.Visible = true;
            }
            else
            {
                divGrid.Visible = false;
            }
        }

        protected void lnkSubmit_Click(object sender, EventArgs e)
        {
            Label LabelStatus = (Label)Grd.Rows[0].FindControl("LabelStatus");
            Label LabelEnquiryNo = (Label)Grd.Rows[0].FindControl("LabelEnquiryNo");
            if (LabelStatus.Text.ToLower() != "registered")
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "AdmissionFollowupProc";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = _con;
                    cmd.Parameters.AddWithValue("@EnquiryNo", LabelEnquiryNo.Text);
                    cmd.Parameters.AddWithValue("@Desctiption", txtDescription.Text.Trim());
                    cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"]);
                    if (LabelStatus.Text == "Form Issued")
                    {
                        cmd.Parameters.AddWithValue("@Status", "Form Issued");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Status", ddlStatus.SelectedValue.Trim());
                    }
                    cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                    cmd.Parameters.AddWithValue("@Action", "insert");
                    try
                    {
                        _con.Open();
                        cmd.ExecuteNonQuery();
                        _con.Close();
                        txtDescription.Text = "";
                        Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Submitted successfully.", "S");
                        LoadRepeater();
                        loadDetails();
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
            else
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Student registered already!", "A");
            }
        }
        public override void Dispose()
        {
            _con.Dispose();
            _oo.Dispose();
        }
    }
}