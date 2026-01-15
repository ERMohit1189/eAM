using System;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;

public partial class admin_AddMobileNo : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LoginName"] == null)
        {
            Response.Redirect("default.aspx");
        }
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);
        if(!IsPostBack)
        {
            txtMoNo.Focus();
            getdata();
        }
    }
    
    public void getdata()
    {
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "USP_VISITORName";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@QueryFor", "S");
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            if(dt.Rows.Count>0)
            {
                repeatermember.DataSource = dt;
                repeatermember.DataBind();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Label VisitorName = (Label)repeatermember.Items[i].FindControl("Label2");
                    LinkButton btnedit = (LinkButton)repeatermember.Items[i].FindControl("btnedit");
                    LinkButton lnkDelete = (LinkButton)repeatermember.Items[i].FindControl("lnkDelete");
                    sql = "select id from Visitors where WhomMeet='" + VisitorName.Text.Trim() + "' and BranchCode=" + Session["BranchCode"] + "";
                    if (oo.Duplicate(sql))
                    {
                        btnedit.Text = "<i class='fa fa-lock'></i>";
                        btnedit.Enabled = false;
                        lnkDelete.Text = "<i class='fa fa-lock'></i>";
                        lnkDelete.Enabled = false;
                    }
                }
            }
        }
        catch (SqlException ee) { throw ee; }
        finally { if (con.State == ConnectionState.Open) { con.Close(); } }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        try
        {
            sendsinglemesg();
        }
        catch (SqlException ee) { throw ee; }
    }

    public void sendsinglemesg()
    {
        if (txtMoNo.Text != "")
        {
            sql = "SELECT Name  FROM VISITORName where Name='" + txtMoNo.Text + "'";
            if (oo.Duplicate(sql))
            {
                oo.ClearControls(this.Page);
                oo.MessageBox("", this.Page);
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Name already exist!", "W");
            }
            else
            {
                SqlCommand cmd1 = new SqlCommand();
                cmd1.CommandText = "USP_VISITORName";
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Connection = con;
                cmd1.Parameters.AddWithValue("@QueryFor", "I");
                cmd1.Parameters.AddWithValue("@Name", txtMoNo.Text.ToUpper());
                cmd1.Parameters.AddWithValue("@Remark", txtremark.Text.ToUpper());
                try
                {
                    con.Open();
                    int n1 = cmd1.ExecuteNonQuery();
                    if (n1 > 0)
                    {
                        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "submitted successfully.", "S");
                        oo.ClearControls(this.Page);
                        oo.MessageBox("", this.Page);
                        getdata();
                    }
                    con.Close();
                }
                catch (SqlException ee) { throw ee; }
                finally { if (con.State == ConnectionState.Open) { con.Close(); } }
            }
        }
        else if (txtMoNo.Text == "")
        {
            try
            {
                string base64std = hfFile.Value;
                string fileExtention = hdfilefileExtention.Value;

                string filePath = "";
                string fileName = "";

                if (base64std != string.Empty)
                {
                    filePath = @"../uploads/UploadExcel/";

                    DateTime date = System.DateTime.Now;

                    string time = date.ToString("dd_MMM_yyyy_HH_mm_ss");

                    fileName = String.Format("AddName" + "{0}" + fileExtention, time);

                    using (FileStream fs = new FileStream(Server.MapPath((filePath + fileName)), FileMode.Create))
                    {
                        using (BinaryWriter bw = new BinaryWriter(fs))
                        {
                            byte[] data = Convert.FromBase64String(base64std);
                            bw.Write(data);
                            bw.Close();
                        }
                    }
                }

                if ((filePath + fileName) != string.Empty)
                {
                   var dt2= BLL.BLLInstance.ReadExcel(Server.MapPath((filePath + fileName)), fileExtention, LinkButton1, "Add_Name$");

                    File.Delete(Server.MapPath((filePath + fileName)));
                    DataTable newTable = dt2.DefaultView.ToTable(false, "F1");

                    SqlCommand cmd1 = new SqlCommand();
                    cmd1.CommandText = "USP_CHECKNAME";
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Connection = con;
                    cmd1.Parameters.AddWithValue("@CHKNAME_TBL", newTable);
                    
                    try
                    {
                        con.Open();
                        int n1 = cmd1.ExecuteNonQuery();
                        if (n1 > 0)
                        {
                            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "submitted successfully.", "S");
                            oo.ClearControls(this.Page);
                            oo.MessageBox("", this.Page);
                            getdata();
                        }
                        con.Close();
                    }
                    catch (SqlException ee) { throw ee; }
                    finally { if (con.State == ConnectionState.Open) { con.Close(); } }
                }
            }
            catch (Exception ex)
            {
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, ex.Message, "W");
            }
        }
        else { Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Enter Nmae or Upload Excel File.", "W"); }
    }

    
    
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("lbldeleteeditid");
        string ss = lblId.Text;

        lblvalue.Text = ss.ToString();
        Panel2_ModalPopupExtender.Show();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "USP_VISITORName";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;

        cmd.Parameters.AddWithValue("@QueryFor", "D");
        cmd.Parameters.AddWithValue("@ID", lblvalue.Text);

        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Deleted successfully.", "W");
            oo.ClearControls(this.Page);
            oo.MessageBox("", this.Page);
            getdata();
        }
        catch (SqlException ee) { throw ee; }
        finally { if (con.State == ConnectionState.Open) { con.Close(); } }
    }
    protected void btnedit_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        var currentrow = (RepeaterItem)lnk.NamingContainer;
        Label lblId = (Label)currentrow.FindControl("lbldeleteeditid");
        string ss = lblId.Text;
        lblID.Text = ss;
        
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "USP_VISITORName";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
        cmd.Parameters.AddWithValue("@QueryFor", "SU");
        cmd.Parameters.AddWithValue("Id", lblId.Text);
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        DataTable dt1 = new DataTable();
        da.Fill(dt1);
        if (dt1.Rows.Count > 0)
        {
            TextBox1.Text = dt1.Rows[0]["Name"].ToString().ToUpper();
            TextBox2.Text = dt1.Rows[0]["Remark"].ToString().ToUpper();
            Panel1_ModalPopupExtender.Show();
        }
    }

    protected void btnupdate_Click(object sender, EventArgs e)
    {
        sql = "SELECT Name  FROM VISITORName where Name='" + TextBox1.Text + "'";
        if (oo.Duplicate(sql))
        {
            oo.ClearControls(this.Page);
            oo.MessageBox("", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Name already exist!", "W");
        }
        else
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "USP_VISITORName";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@QueryFor", "U");
            cmd.Parameters.AddWithValue("@Id", lblID.Text);
            cmd.Parameters.AddWithValue("@Name", TextBox1.Text.ToUpper());
            cmd.Parameters.AddWithValue("@Remark", TextBox2.Text.ToUpper());
            cmd.Connection = con;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                getdata();
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully.", "U");
                oo.ClearControls(this.Page);
                oo.MessageBox("", this.Page);
            }
            catch (SqlException ee) { throw ee; }
        }
    }

    //public DataTable ReadExcel(string fileName, string fileExt, Control ctrl, string sheetName)
    //{
    //    string conn = string.Empty;
    //    DataTable dtexcel = new DataTable();
    //    if (fileExt.CompareTo(".xls") == 0)
    //        conn = @"provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties='Excel 8.0;HRD=Yes;IMEX=1';"; //for below excel 2007  
    //    else
    //        conn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties='Excel 12.0;HDR=NO';"; //for above excel 2007  
    //    using (OleDbConnection con = new OleDbConnection(conn))
    //    {
    //        try
    //        {
    //            //Get the name of First Sheet
    //            //con.Open();
    //            //DataTable dtExcelSchema;
    //            //dtExcelSchema = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
    //            //string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
    //            //con.Close();

    //            con.Open();
    //            //Read Data from First Sheet
    //            OleDbDataAdapter oleAdpt = new OleDbDataAdapter("select * from [" + sheetName + "] ", con); //here we read data from sheet1  
    //            oleAdpt.Fill(dtexcel); //fill excel data into dataTable  
    //            con.Close();
    //        }
    //        catch (Exception ex)
    //        {
    //            ScriptManager.RegisterClientScriptBlock(ctrl, GetType(), "alert", "window.alert('" + ex.Message + "')", true);
    //        }
    //    }
    //    return dtexcel;
    //}

}