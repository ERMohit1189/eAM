using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

public partial class sp_userControl_classTeacherProfile : System.Web.UI.UserControl
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
                //ignored
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
        ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("GetClassTeacherProfile_Proc", param);

        if (ds != null)
        {
            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                lblCTNAME.Text = dt.Rows[0][0].ToString();
            //   ctImage.ImageUrl = dt.Rows[0][1] == DBNull.Value ? "~/img/user-pic/user-pic.jpg" : "../"+dt.Rows[0][1].ToString();
               if(dt.Rows[0][1].ToString().Trim() != "")
                {
                    var subPath = dt.Rows[0][1].ToString().Trim();
                    var exists = System.IO.File.Exists(Server.MapPath(subPath));
                    if (!exists)
                    {
                        ctImage.ImageUrl = "~/img/user-pic/user-pic.jpg";
                    }
                    else
                    {
                        ctImage.ImageUrl = "../" + dt.Rows[0][1].ToString().Trim();

                    }
                }
               else
                {
                    ctImage.ImageUrl = "~/img/user-pic/user-pic.jpg";
                }

                if (dt.Rows[0]["ismobilenoshow"].ToString().Trim() != "" && dt.Rows[0]["ismobilenoshow"].ToString().Trim() != "0" && dt.Rows[0]["ismobilenoshow"].ToString().Trim() != "False")
                {
                    moshow.Visible = true;
                    lblCTCONTACT.Text = dt.Rows[0][2].ToString();
                }
                if (dt.Rows[0]["isemailshow"].ToString().Trim() != "" && dt.Rows[0]["isemailshow"].ToString().Trim() != "0" && dt.Rows[0]["isemailshow"].ToString().Trim() != "False")
                {
                    emailshow.Visible = true;
                    lblCTEMAIL.Text = dt.Rows[0][3].ToString();
                }
            }
        }
    }
}