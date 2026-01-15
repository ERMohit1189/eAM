using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;

public partial class uploadFeeStructure : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";
    DataTable dt;
    protected void Page_PreInIt(object sender, EventArgs e)
    {
        if (Session["Logintype"] == null) { Response.Redirect("~/default.aspx"); }
        if (Session["Logintype"].ToString() == "Admin")
        {
            this.MasterPageFile = "~/Master/admin_root-manager.master";
        }
        else if (Session["Logintype"].ToString() == "Staff")
        {
            this.MasterPageFile = "~/Staff/staff_root-manager.master";
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Campus camp = new Campus(); camp.LoadLoader(loader);
        con = oo.dbGet_connection();
        if (!IsPostBack)
        {
            loadGrid();
        }
    }

    protected void lnkSubmit_Click(object sender, EventArgs e)
    {
        if (hfSllabusFile.Value != string.Empty && hfSllabusFile.Value != null)
        {
            string filePath = "";
            string fileName = "";

            string base64std = hfSllabusFile.Value;
            if (base64std != string.Empty)
            {
                fileName = "FeeStructure_" + DateTime.Now.ToString("dd-MM-yy_hhmmss") + hfSllabusFileext.Value.ToString();
                filePath = string.Format("~/Uploads/Docs/pdf/{0}", fileName);

                using (FileStream fs = new FileStream(Server.MapPath(filePath), FileMode.Create))
                {
                    using (BinaryWriter bw = new BinaryWriter(fs))
                    {
                        byte[] data = Convert.FromBase64String(base64std);
                        bw.Write(data);
                        bw.Close();
                    }
                }
                string msg = "";
                string sql1 = "select count(*) cnt from feeStructure";
                if (oo.ReturnTag(sql1, "cnt") == "0")
                {
                    msg = "Submitted";
                    sql = "insert into feeStructure (PDFurl, PDFName)values('" + filePath + "', '" + fileName + "')";
                }
                else
                {
                    msg = "Updated";
                    sql = "update feeStructure set PDFurl='" + filePath + "', PDFName='" + fileName + "'";
                }

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, msg+" successfully.", "S");
                    loadGrid();
                }
                catch (SqlException ee) { oo.MessageBox(ee.Message.ToString(), this.Page); }

            }

        }
        else
        {
            lblErrormsg.Text = "Please, Select file!";
        }
    }

    private void loadGrid()
    {
        sql = "select * from feeStructure";

        grdDocList.DataSource = oo.Fetchdata(sql);
        grdDocList.DataBind();
    }

    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        Label lblid = (Label)lnk.NamingContainer.FindControl("lblDelete");
        lblvalue.Text = lblid.Text;
        Panel2_ModalPopupExtender.Show();
        lnkNo.Focus();
    }
    protected void lnkYes_Click(object sender, EventArgs e)
    {
        sql = "delete from feeStructure where id=" + lblvalue.Text.Trim() + "";
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = sql;
        cmd.CommandType = CommandType.Text;
        cmd.Connection = con;
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Deleted successfully.", "S");
            loadGrid();
        }
        catch (SqlException ee) { oo.MessageBox(ee.Message.ToString(), this.Page); }
    }
}