using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

public partial class sp_userControl_studentProfile : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                GetStudentAndTeacherProfile();
            }
            catch (Exception)
            {
            }
        }
    }

    private void GetStudentAndTeacherProfile()
    {
        List<SqlParameter> param = new List<SqlParameter>();
        param.Add(new SqlParameter("@Srno", Session["Srno"].ToString()));
        param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));

        DataSet ds = new DataSet();
        ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("GetStudentProfile_Proc", param);

        if (ds != null)
        {
            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = BAL.objBal.dbGet_connection().ConnectionString;
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "USP_StudentsPhotoReport";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@sessionName", Session["SessionName"].ToString().Trim());
                        cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString().Trim());
                        cmd.Parameters.AddWithValue("@SrNo", Session["Srno"].ToString().Trim());
                        cmd.Parameters.AddWithValue("@action", "details");
                        SqlDataAdapter das = new SqlDataAdapter(cmd);
                        DataSet dsPhoto = new DataSet();
                        das.Fill(dsPhoto);
                        cmd.Parameters.Clear();

                        if (dsPhoto.Tables[0].Rows.Count > 0)
                        {

                            string path = dsPhoto.Tables[0].Rows[0]["PhotoPath"].ToString() != string.Empty ? "../" + dsPhoto.Tables[0].Rows[0]["PhotoPath"] + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture) : "~/img/user-pic/user-pic.jpg" + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture);
                            if (path != "" && path != null)
                            {
                                Session["ImageUrl"] = path.Replace("..", "~").ToString();
                            }
                            else
                            {
                                path = "~/img/user-pic/user-pic.jpg";
                            }
                            //if (!File.Exists(path))
                            //{

                            //}
                            //else
                            //{

                            //}
                            stImage.ImageUrl = path;
                            stImage2.ImageUrl = path;
                        }
                    }
                }

                lblStName.Text = dt.Rows[0][0].ToString();
                lblStName2.Text = lblStName.Text;


                lblClass.Text = dt.Rows[0][2].ToString();
                lblClass2.Text = lblClass.Text;

                lblSection.Text = dt.Rows[0][3].ToString();
                lblSection2.Text = lblSection.Text;

                lblBranch.Text = dt.Rows[0][4].ToString();
                lblBranch2.Text = lblBranch2.Text;

                lblStream.Text = dt.Rows[0][5].ToString();
                lblStream2.Text = lblStream.Text;

                lblSessionName.Text = dt.Rows[0][6].ToString();
                lblSrno.Text = dt.Rows[0][7].ToString();
                lblSrno2.Text = lblSrno.Text;

                lblDOB.Text = dt.Rows[0][8].ToString();
                lblFaContactNo.Text = dt.Rows[0][9].ToString();
                lblFaEmail.Text = dt.Rows[0][10].ToString();
                lblSTAddress.Text = dt.Rows[0][11].ToString();
                lblFather.Text = dt.Rows[0][12].ToString();
                lblMother.Text = dt.Rows[0][13].ToString();
            }
        }
    }
}