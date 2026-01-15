using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI;
namespace _2
{
    public partial class ProvisionalFormPrint : Page
    {
        string _sql = "";
        
        protected void Page_Load(object sender, EventArgs e)
        {
            BLL.BLLInstance.LoadHeader("Report", header);
            if (!IsPostBack)
            {
                

                if (string.IsNullOrEmpty(Request.QueryString["print"]))
                {
                    if ((string)Session["LoginName"] == "" || (string)Session["BranchCode"] == "" || (string)Session["SessionName"] == "" ||
                        Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
                    {
                        Response.Redirect("ProvisinalAdmissionForm.aspx");
                    }
                }
                if (!string.IsNullOrEmpty(Request.QueryString["print"]))
                {
                    string[] dobData = Request.QueryString["print"].ToString().Split(new string[] { "$" }, StringSplitOptions.None);
                    StudentRecord(dobData[0].ToString(), dobData[1].ToString());
                    
                }
            }
        }


        private void StudentRecord(string Srno, string session)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = BAL.objBal.dbGet_connection().ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "ProvisionalFormProc";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@sessionName", session.Trim());
                    cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString().Trim());
                    cmd.Parameters.AddWithValue("@SrNo", Srno);
                    cmd.Parameters.AddWithValue("@Action", "select");
                    SqlDataAdapter das = new SqlDataAdapter(cmd);
                    DataTable dtlist = new DataTable();
                    das.Fill(dtlist);
                    cmd.Parameters.Clear();

                    if (dtlist.Rows.Count > 0)
                    {
                        lblSrno.Text = dtlist.Rows[0]["srno"].ToString();
                        lblClass.Text = dtlist.Rows[0]["AdmissionClass"].ToString();
                        lblSessionFor.Text = dtlist.Rows[0]["WhichSessionName"].ToString();
                        lblStudentName.Text = dtlist.Rows[0]["Name"].ToString();
                        if (dtlist.Rows[0]["Gender"].ToString().ToUpper() == "MALE")
                        {
                            male.Attributes.Add("class", "icon-checkmark");
                            female.Attributes.Add("class", "sex-check-box-blank");
                            Anyother.Attributes.Add("class", "sex-check-box-blank");
                        }
                        else if (dtlist.Rows[0]["Gender"].ToString().ToUpper() == "FEMALE")
                        {
                            male.Attributes.Add("class", "sex-check-box-blank");
                            female.Attributes.Add("class", "icon-checkmark");
                            Anyother.Attributes.Add("class", "sex-check-box-blank");
                        }
                        else
                        {
                            Anyother.Attributes.Add("class", "icon-checkmark");
                        }
                        if (dtlist.Rows[0]["dob"].ToString() != "")
                        {
                            string[] dobData = dtlist.Rows[0]["dob"].ToString().Split(new string[] { " " }, StringSplitOptions.None);
                            dd.Text = dobData[0];
                            mm.Text = dobData[1];
                            yy.Text = dobData[2];
                            inword.Text = dtlist.Rows[0]["dobNew"].ToString();
                        }
                        mother.Text = dtlist.Rows[0]["MotherName"].ToString();
                        father.Text = dtlist.Rows[0]["FatherName"].ToString();
                        motherQuli.Text = dtlist.Rows[0]["MotherQualificationNew"].ToString();
                        fatherQuli.Text = dtlist.Rows[0]["FatherQualificationNew"].ToString();
                        mothermobile.Text = dtlist.Rows[0]["MotherContactNo"].ToString();
                        fathermobile.Text = dtlist.Rows[0]["FatherContactNo"].ToString();
                        MotherOcu.Text = dtlist.Rows[0]["MotherOccupation"].ToString();
                        fatherOcu.Text = dtlist.Rows[0]["FatherOccupation"].ToString();
                        motherincome.Text = dtlist.Rows[0]["MotherIncomeMonthly"].ToString();
                        fatherincome.Text = dtlist.Rows[0]["FatherIncomeMonthly"].ToString();
                        category.Text = dtlist.Rows[0]["Category"].ToString();
                        aadhar.Text = dtlist.Rows[0]["AadharNo"].ToString();
                        guardianContact.Text = dtlist.Rows[0]["FamilyContactNo"].ToString();
                        photo.ImageUrl = dtlist.Rows[0]["PhotoPath"].ToString() != string.Empty ? dtlist.Rows[0]["PhotoPath"] + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture) : "../img/user-pic/user-pic.jpg" + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture);
                    }
                }
            }
        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            PrintHelper_New.ctrl = abc;
            ClientScript.RegisterStartupScript(GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
        }
        protected void lnkback_Click(object sender, EventArgs e)
        {
            lnkback.PostBackUrl = "~/2/ProvisinalAdmissionForm.aspx";
        }
    }
}