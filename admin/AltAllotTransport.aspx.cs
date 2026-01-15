using System;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class admin_AltDeleteSrno : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    { con = oo.dbGet_connection();

    if (!IsPostBack)
    {

        sql = "select SG.Id, SC.SectionName,SO.Card,SO.Medium as Medium,CM.ClassName,convert(nvarchar,So.DateOfAdmiission,106) as DateOfAdmiission ,SO.SectionId,Sf.FatherName,SF.MotherName,SG.FirstName,SG.MiddleName,SG.LastName,so.StEnRCode as StEnRCode,sg.srno  as srno,case  when so.TransportRequired='Yes' then 'Yes' else 'No' end as TransportRequired,so.wayamount as wayamount from StudentGenaralDetail SG ";
        sql = sql + "    left join StudentFamilyDetails SF on SG.srno=SF.srno";
        sql = sql + "   left join StudentOfficialDetails SO on SG.srno=SO.srno";
        sql = sql + "   left join ClassMaster CM on SO.AdmissionForClassId=CM.Id";
        sql = sql + "   left join SectionMaster SC on SO.SectionId=SC.Id where";
        sql = sql + " TransportRequired='Yes' and so.SessionName='2014-2015' and sg.SessionName='2014-2015' and sf.SessionName='2014-2015' and cm.SessionName='2014-2015' and sc.SessionName='2014-2015' and so.Withdrwal is null";

        Grd.DataSource = oo.GridFill(sql);
        Grd.DataBind();

        if (Grd.Rows.Count == 0)
        {
            oo.MessageBox("Sorry, No Record(s) Found", this.Page);
        }
    }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {

       
            if (Grd.Rows.Count > 0)
            {
                for (int i = 0; i < Grd.Rows.Count; i++)
                {
                    Label Label1 = (Label)Grd.Rows[i].FindControl("Label1");
                    Label Label18 = (Label)Grd.Rows[i].FindControl("Label18");
                    sql = "Update StudentOfficialDetails set TransportRequired='Yes' where srno='" + Label1.Text + "' and sessionname='" + Session["SessionName"] + "'";
                    SqlCommand cmd = new SqlCommand(sql,con);
                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        oo.MessageBoxforUpdatePanel("Updated successfully.", LinkButton1);
                    }
                    catch (SqlException) { }
                }
            }
       
    }
}