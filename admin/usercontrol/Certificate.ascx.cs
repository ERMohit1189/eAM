using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

public partial class admin_Certificate : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        List<SqlParameter> param = new List<SqlParameter>();
        param.Add(new SqlParameter("@Id", 1));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"]));
        DataSet ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("InstituteMasterProc", param);
        if (ds.Tables.Count > 0)
        {
            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {

                lblInstitute.Text = dt.Rows[0]["collegename"].ToString();
                lblAddress.Text = dt.Rows[0]["address"].ToString();
                lblBranchandCity.Text = dt.Rows[0]["branchandcityname"].ToString();
                lblCity.Text = dt.Rows[0]["cityname"].ToString();
                lblContactnoandemail.Text = dt.Rows[0]["phone"].ToString();
                lblContactnoandemail1.Text = dt.Rows[0]["email"].ToString();
                lblPhoneNo.Text = dt.Rows[0]["phone"].ToString();
                lblEmail.Text = dt.Rows[0]["email"].ToString();
                lblWebsite.Text = dt.Rows[0]["website"].ToString();
                //hylWebsite.NavigateUrl = "http://" + lblWebsite.Text;
                lblWebsiteandEmail.Text = dt.Rows[0]["email"].ToString();
                lblWebsiteandEmail1.Text = dt.Rows[0]["website"].ToString();
                //hylWebsiteandEmail1.NavigateUrl = "http://" + lblWebsiteandEmail1.Text;
                lblAffilation.Text = dt.Rows[0]["affiliatedto"].ToString();
                lblSlogan.Text = dt.Rows[0]["slogan"].ToString();
                if (dt.Rows[0]["collegeLogo"].ToString() == "")
                {
                    Image1.ImageUrl = "~/uploads/CollegeLogo/DefaultCollegeLogo.png";
                }
                else
                {
                    Image1.ImageUrl = dt.Rows[0]["collegeLogo"].ToString() + "?a=" + DateTime.Now.ToString("ddMMMyyyyhhmmss");//BAL.objBal.GetImageUrl(ResolveClientUrl(dt.Rows[0]["collegeLogo"].ToString()), Request.Url.AbsoluteUri.Split('/'));
                    Image3.ImageUrl = dt.Rows[0]["collegeLogo"].ToString() + "?a=" + DateTime.Now.ToString("ddMMMyyyyhhmmss"); //BAL.objBal.GetImageUrl(ResolveClientUrl(dt.Rows[0]["collegeLogo"].ToString()), Request.Url.AbsoluteUri.Split('/'));
                }
                lblAffilationNo.Text = dt.Rows[0]["AffiliationNo"].ToString();
                lblSchoolNo.Text = dt.Rows[0]["SchoolNo"].ToString();
                string boarduniversitylogo = dt.Rows[0]["boarduniversitylogo"].ToString().Replace("~/", "../../");
                if (dt.Rows[0]["boarduniversitylogo"].ToString() == "")
                {
                    Image2.ImageUrl = "~/img/cbse-logo.png";
                }
                else
                {
                    Image2.ImageUrl = boarduniversitylogo;
                }
            }
        }
    }
}
