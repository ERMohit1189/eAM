using System;
using System.Web.UI;
using System.Data;
using System.Data.SqlClient;

public partial class RulesForTimeTable : Page
{
    string sql = "", _sql = "", Sql = "";
    Campus _oo = new Campus();
    private SqlConnection _con;
    DataTable dt;
    protected void Page_Load(object sender, EventArgs e)
    {
        _con = new SqlConnection();
        _con = _oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);
        if (!IsPostBack)
        {
            LoadDesignations();
            
        }
    }

   
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int stschecked = 0; string chkdata = "";
        for (int j = 0; j < chkCategory.Items.Count; j++)
        {
            if (chkCategory.Items[j].Selected)
            {
                stschecked = stschecked + 1;
                if (stschecked > 1)
                {
                    chkdata = chkdata + ","+chkCategory.Items[j].Value.ToString();
                }
                else
                {
                    chkdata = chkdata + chkCategory.Items[j].Value.ToString();
                }
                
            }
        }
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = "TTRulesOfTimeTableProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = _con;
            cmd.Parameters.AddWithValue("@DesignationAllowed", chkdata.ToString());
            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@Action", "insert");
            try
            {
                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Submitted successfully.", "S");
                LoadDesignations();
            }
            catch (Exception ex)
            {
            }
        }
    }

    protected void LoadDesignations()
    {
        sql = "select EmpDesName, EmpDesId from EmpDesMaster where BranchCode=" + Session["BranchCode"] + "  order by EmpDesId asc";
        var dt=_oo.Fetchdata(sql);
        chkCategory.DataSource = dt;
        chkCategory.DataTextField = "EmpDesName";
        chkCategory.DataValueField = "EmpDesId";
        chkCategory.DataBind();
        sql = "select DesignationAllowed from TTRulesOfTimeTable where BranchCode=" + Session["BranchCode"] + "";
        string data=_oo.ReturnTag(sql, "DesignationAllowed");
        if (data!="")
        {
            string[] rows = data.Split(new string[] { ","}, StringSplitOptions.None);
            for (int i = 0; i < rows.Length; i++)
            {
                for (int j = 0; j < chkCategory.Items.Count; j++)
                {
                    if (chkCategory.Items[j].Value == rows[i])
                    {
                        chkCategory.Items[j].Selected = true;
                    }
                }
            }
        }
        
    }

    
}