using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_BatchMaster : System.Web.UI.Page
{
    string _sql = "";
    private SqlConnection _con;
    Campus oo = new Campus();
    SqlConnection con = new SqlConnection();
    protected void Page_PreInIt(object sender, EventArgs e)
    {
        if (Session["Logintype"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        _con = new SqlConnection();
        _con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);
        con = oo.dbGet_connection();
        if (!IsPostBack)
        {
            LoadReport();
        }
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("lblLessonID");
        Label lblLessonName = (Label)chk.NamingContainer.FindControl("lblClassID");
        lblIDEdit.Text = lblId.Text;
        txtlessonEdit.Text = lblLessonName.Text;
        Panel1_ModalPopupExtender.Show();
    }


    protected void LoadReport()
    {
        try
        {
            if (Session["SessionName"] == null || Session["BranchCode"] == null)
            {
                Campus camp = new Campus();
                camp.msgbox(Page, msgbox, "Session Expired. Please login again.", "E");
                return;
            }

            List<SqlParameter> param = new List<SqlParameter>
        {
            new SqlParameter("@BatchName", DBNull.Value), // If needed, replace with actual value
            new SqlParameter("@SessionName", Session["SessionName"].ToString()),
            new SqlParameter("@BranchCode", Convert.ToInt32(Session["BranchCode"])),
            new SqlParameter("@Action", "Select")
        };
            DataSet ds = new DLL().Sp_SelectRecord_usingExecuteDataset("[BatchMasterProc]", param);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    Grd.DataSource = dt;
                    Grd.DataBind();
                }
                else
                {
                    Grd.DataSource = null;
                    Grd.DataBind();
                }
            }
            else
            {
                Grd.DataSource = null;
                Grd.DataBind();
            }
        }
        catch (Exception ex)
        {
            Campus camp = new Campus();
            camp.msgbox(Page, msgbox, "Error: " + ex.Message, "E");
        }
    }

   

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        int flg = 0;
        string message = "";
        SqlCommand cmd = new SqlCommand
        {
            CommandText = "BatchMasterProc",
            CommandType = CommandType.StoredProcedure,
            Connection = con
        };
      
        cmd.Parameters.AddWithValue("@BatchName", txtlesson.Text.Trim());
        cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
        cmd.Parameters.AddWithValue("@BranchCode", Convert.ToInt32(Session["BranchCode"]));
        cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
        cmd.Parameters.AddWithValue("@Action", "Insert");
        try
        {
            con.Open();
            object result = cmd.ExecuteScalar();
            con.Close();
            if (result != null)
            {
                message = result.ToString();
                flg = message == "Saved Successfully" ? 1 : 0;
            }

        }
        catch (Exception ex)
        {
            con.Close();
            message = "Error: " + ex.Message;
        }
        if (flg > 0)
        {
            LoadReport();
            Campus camp = new Campus();
            camp.msgbox(Page, msgbox, message, "S");
        }
        else
        {
            Campus camp = new Campus();
            camp.msgbox(Page, msgbox, message, "E");
        }


    }

  



    protected void LinkButton3_Click(object sender, EventArgs e)
    {

        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("lblLessonID");
        lblValue.Text = lblId.Text;
        mpeDelete.Show();
    }

    protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadReport();
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        int flg = 0;
        string message = "";
        SqlCommand cmd = new SqlCommand
        {
            CommandText = "BatchMasterProc",
            CommandType = CommandType.StoredProcedure,
            Connection = con
        };
        cmd.Parameters.AddWithValue("@Id", Convert.ToInt32(lblIDEdit.Text));
        cmd.Parameters.AddWithValue("@BatchName", txtlessonEdit.Text.Trim());
        cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
        cmd.Parameters.AddWithValue("@BranchCode", Convert.ToInt32(Session["BranchCode"]));
        cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
        cmd.Parameters.AddWithValue("@Action", "Update");
        try
        {
            con.Open();
            object result = cmd.ExecuteScalar();
            con.Close();
            if (result != null)
            {
                message = result.ToString();
                flg = message == "Update Successfully" ? 1 : 0;
            }

        }
        catch (Exception ex)
        {
            con.Close();
            message = "Error: " + ex.Message;
        }
        if (flg > 0)
        {
            LoadReport();
            Campus camp = new Campus();
            camp.msgbox(Page, msgbox, message, "S");
        }
        else
        {
            Campus camp = new Campus();
            camp.msgbox(Page, msgbox, message, "E");
        }
    }

    protected void btnNo_Click(object sender, EventArgs e)
    {

    }

    protected void btnYes_Click(object sender, EventArgs e)
    {
        _sql = "Delete from BatchMst_tb where ID=" + lblValue.Text + "  and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "'";

        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = _sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = _con;
            try
            {
                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Deleted successfully.", "S");
                LoadReport();

            }
            catch (Exception ex)
            {
            }
        }
    }

   

   
}