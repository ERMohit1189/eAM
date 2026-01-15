using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;
using System.IO;

public partial class admin_database_backup : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = string.Empty;

    protected void Page_PreInIt(object sender, EventArgs e)
    {
        if (Session["Logintype"] == null) { Response.Redirect("~/default.aspx"); }
        if (Session["Logintype"].ToString() == "Administrator")
        {
            this.MasterPageFile = "~/Administrator/administrato_root-manager.master";
        }
        if (Session["Logintype"].ToString() == "SuperAdmin")
        {
            this.MasterPageFile = "~/50/sadminRootManager.master";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LoginName"] == null)
        {
            Response.Redirect("default.aspx");
        }
        Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file

        con = oo.dbGet_connection();
        if (!IsPostBack)
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
            {
                DropDownList1.Items.Add(drive.Name);
            }
           
        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        string sql1 = "";
        string databackupFNa = "";
        sql1 = "Select top(1) BackupNameStart from BackupNameStart";
        if (oo.Duplicate(sql1))
        {
            databackupFNa = oo.ReturnTag(sql1, "BackupNameStart");
        }
        else
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please Initialize database name!.", "A");
            return;
        }
        if (oo.Duplicate(sql1))
        {
            sql = "select LEFT(convert(nvarchar,GETDATE(),106),2)as DD ,Right(left(convert(nvarchar,GETDATE(),106),6),3) as Mo,Right(convert(nvarchar,GETDATE(),106),4) as YY ,LEFT(convert(nvarchar,GETDATE(),108),2) as hh,Right(LEFT(convert(nvarchar,GETDATE(),108),5),2) as mm,Right(convert(nvarchar,GETDATE(),108),2) as ss";
            databackupFNa = databackupFNa + oo.ReturnTag(sql, "DD");
            databackupFNa = databackupFNa + oo.ReturnTag(sql, "Mo");
            databackupFNa = databackupFNa + oo.ReturnTag(sql, "YY");
            databackupFNa = databackupFNa + oo.ReturnTag(sql, "hh");
            databackupFNa = databackupFNa + oo.ReturnTag(sql, "MM");
            databackupFNa = databackupFNa + oo.ReturnTag(sql, "ss");
            try
            {
                if (!Directory.Exists(DropDownList1.SelectedItem.Text + @"\\eambackup"))
                {
                    Directory.CreateDirectory(DropDownList1.SelectedItem.Text + @"\\eambackup");
                }
                sql = "BACKUP DATABASE [" + con.Database + "] TO  DISK = N'" + DropDownList1.SelectedItem.Text + "\\eambackup\\eAMdb" + databackupFNa + ".bak' WITH NOFORMAT, NOINIT,  NAME = N'eAMdb-Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Database Backup process completed successfully.", "S");
                }
                catch (SqlException) { }
            }
            catch (Exception) { }
        }
    }
}






