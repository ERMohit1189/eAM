using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;
using System.IO;

public partial class Privacy_Policy : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        sql = "select CollegeName, Phone, Email,  (CollegeAdd1 + ' ' + (case when CollegeAdd2 <> '' then CollegeAdd2 + ', ' else '' end) + (case when CityName<> '' then CityName+', ' else '' end)+StateName) Address1 from CollegeMaster inner join CityMaster on CityMaster.Id = CollegeMaster.CityId inner join StateMaster on StateMaster.Id = CollegeMaster.StateId";
        if (oo.Duplicate(sql))
        {
            instituteName.Text = oo.ReturnTag(sql, "collegeName");
        }
    }
}