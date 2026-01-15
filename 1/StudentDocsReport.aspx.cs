using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminStudentDocsReport : Page
{
    string sql = "";
    Campus oo = new Campus();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        Campus camp = new Campus(); camp.LoadLoader(loader);
        BLL.BLLInstance.LoadHeader("Report", header);
        if (!IsPostBack)
        {
            sql = "Select id, ClassName from ClassMaster";
            sql +=  "  where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            sql +=  "  order by Id";
            oo.FillDropDown_withValue(sql, DrpClass, "ClassName", "id");
            DrpClass.Items.Insert(0, new ListItem("<--Select-->", ""));
            drpSection.Items.Insert(0, new ListItem("<--Select-->", ""));
            LoadDocType();
        }

    }
    protected void DrpClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        drpSection.Items.Clear();
        string sql = "select id, SectionName from SectionMaster ";
        sql +=  "  where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and ClassNameId=" + DrpClass.SelectedValue + "";
        sql +=  "  order by Id";
        oo.FillDropDown_withValue(sql, drpSection, "SectionName", "id");
        drpSection.Items.Insert(0, new ListItem("<--Select-->", ""));
    }

    protected void LoadDocType()
    {
        BLL.BLLInstance.loadDoctype(chk_doc_type);
        foreach (ListItem li in chk_doc_type.Items)
        {
            li.Selected = true;
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
    }

    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        oo.ExportTolandscapeWord(Response, "DocumentReport", gdv1);
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        oo.ExportDivToExcelWithFormatting(Response, "DocumentReport.xls", gdv1, Server.MapPath("~/Admin/css/style.css"));
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {
        oo.ExporttolandscapePdf(Response, "DocumentReport", gdv1);
    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = gdv1;
        ClientScript.RegisterStartupScript(GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }


    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        string drpClassId = DrpClass.SelectedValue.ToString();
        string drpClassName = DrpClass.SelectedItem.Text.ToString();
        string SectionName = drpSection.SelectedItem.Text.ToString();
        string AdmissioncType = drpAdmissioncType.SelectedItem.Text.ToString();
        string DocType = drpDocType.SelectedValue;
        string Hardcopy = ""; string Softcopy = "";
        if (DocType == "Both") { Hardcopy = "1"; Softcopy = "1"; }
        else if (DocType == "Hard Copy") { Hardcopy = "1"; Softcopy = "0"; }
        else if (DocType == "Soft Copy") { Hardcopy = "0"; Softcopy = "1"; }
        else { Hardcopy = ""; Softcopy = ""; }

        string BranchCode = Session["BranchCode"].ToString();
        string session = Session["SessionName"].ToString();
        var chksName = ""; var chksNamedata = ""; var count = 0;
        for (int i = 0; i < chk_doc_type.Items.Count; i++)
        {
            if (chk_doc_type.Items[i].Selected)
            {
                if ((i + 1) == chk_doc_type.Items.Count)
                {
                    chksName += chk_doc_type.Items[i].Text;
                    chksNamedata += chk_doc_type.Items[i].Value + "#" + chk_doc_type.Items[i].Text;
                }
                else
                {
                    chksName += chk_doc_type.Items[i].Text + "#";
                    chksNamedata += chk_doc_type.Items[i].Value + "#" + chk_doc_type.Items[i].Text + "##";
                }
            }

            count = count + 1;
        }
        string[] chkNameLength = chksNamedata.Split(new string[] { "##" }, StringSplitOptions.None);

        DataTable dt = new DataTable();
        dt.Columns.Add("id", typeof(int));
        dt.Columns.Add("DocumentType", typeof(string));
        DataRow dr;

        for (int q = 0; q < chkNameLength.Length-1; q++)
        {
            string[] chksValueLength = chkNameLength[q].Split(new string[] { "#" }, StringSplitOptions.None);
            dr = dt.NewRow();
            dr["id"] = chksValueLength[0];
            dr["DocumentType"] = chksValueLength[1];
            dt.Rows.Add(dr);

        }

        string data = "";

        var listOf = ddllistOf.SelectedValue.ToString();
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = BAL.objBal.dbGet_connection().ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = "Get_StudentDocsRecord_new";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@sessionName", session.Trim());
                cmd.Parameters.AddWithValue("@BranchCode", BranchCode.Trim());
                if (drpClassName != "")
                { cmd.Parameters.AddWithValue("@ClassName", drpClassName.Trim()); }
                if (SectionName != "")
                { cmd.Parameters.AddWithValue("@SectionName", SectionName.Trim()); }
                cmd.Parameters.AddWithValue("@Hardcopy", Hardcopy.Trim());
                cmd.Parameters.AddWithValue("@Softcopy", Softcopy.Trim());
                if (AdmissioncType != "All")
                { cmd.Parameters.AddWithValue("@TypeOFAdmision", AdmissioncType.Trim()); }
                cmd.Parameters.AddWithValue("@docTypes", chksName);
                cmd.Parameters.AddWithValue("@SubmitTypes", listOf);
                cmd.Parameters.AddWithValue("@QueryFor", "Student");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                cmd.Parameters.Clear();
                try
                {
                    conn.Open();

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gdv1.Visible = true;
                        icons.Visible = true;
                        data += "<table id='example' class='table no-bm  table-striped table-hover no-head-border table-bordered'>";
                        data += "<thead>";
                        data += "<tr><td class='text-center' colspan='" + (5 + dt.Rows.Count) + "'><b>Document Report</b></td></tr>";
                        data += "<tr>";
                        data += "<th align = 'center' valign = 'middle' scope = 'col' style = 'width: 40px; '>#</th> <th scope='col'>S.R. No.</th> <th scope='col'>Student's Name</th> <th scope='col'>Father's Name</th> <th scope='col'>Class</th>";
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            data += "<th  scope='col'>" + ds.Tables[0].Rows[i]["DocumentType"].ToString() + "</th>";

                        }
                        data += "</tr></thead>";
                        data += "<tbody>";

                        for (int j = 0; j < ds.Tables[1].Rows.Count; j++)
                        {
                            data += "<tr>";
                            data += "<td>" + (j + 1) + "</td>";
                            data += "<td>" + ds.Tables[1].Rows[j]["Srno"].ToString() + "</td>";
                            data += "<td>" + ds.Tables[1].Rows[j]["Name"].ToString() + "</td>";
                            data += "<td>" + ds.Tables[1].Rows[j]["FatherName"].ToString() + "</td>";
                            data += "<td>" + ds.Tables[1].Rows[j]["ClassName"].ToString() + " " + (SectionName.ToString() != "" ? "(" + SectionName.ToString() + ")" : "") + "</td>";

                            cmd.Connection = conn;
                            cmd.CommandText = "Get_StudentDocsRecord_new";
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@sessionName", session.Trim());
                            cmd.Parameters.AddWithValue("@Srno", ds.Tables[1].Rows[j]["Srno"].ToString());
                            cmd.Parameters.AddWithValue("@BranchCode", BranchCode.Trim());
                            cmd.Parameters.AddWithValue("@docTypes", chksName);
                            cmd.Parameters.AddWithValue("@QueryFor", "Doc");
                            SqlDataAdapter das = new SqlDataAdapter(cmd);
                            DataTable dts = new DataTable();
                            das.Fill(dts);
                            cmd.Parameters.Clear();

                            if (dts.Rows.Count > 0)
                            {

                                for (int k = 0; k < dts.Rows.Count; k++)
                                {
                                    string Softcopys = ""; string Hardcopys = "";
                                    if (dts.Rows[k]["Softcopy"].ToString() == "0")
                                    {
                                        Softcopys = "[S]<i class='text-false-color txt-bold' style='font-size:15px'>X</i>";
                                    }
                                    else
                                    {
                                        Softcopys = "[S]<i class='text-true-color txt-bold' style='font-size:15px'>✔</i>";
                                    }
                                    if (dts.Rows[k]["Hardcopy"].ToString() == "0")
                                    {
                                        Hardcopys = "[H]<i class='text-false-color txt-bold' style='font-size:15px'>X</i>";
                                    }
                                    else
                                    {
                                        Hardcopys = "[H]<i class='text-true-color txt-bold' style='font-size:15px'>✔</i>";
                                    }
                                    if (Hardcopy == "1" && Softcopy == "1")
                                    {
                                        data += "<td>" + Softcopys + "&nbsp;|&nbsp;" + Hardcopys + "</td>";

                                    }
                                    if (Hardcopy != "1" && Softcopy == "1")
                                    {
                                        data += "<td>" + Softcopys + "</td>";

                                    }
                                    if (Hardcopy == "1" && Softcopy != "1")
                                    {
                                        data += "<td>" + Hardcopys + "</td>";

                                    }
                                }
                            }

                            data += "</tr>";
                        }
                        data += "</tbody>";
                        data += "</table>";
                    }
                    else
                    {
                        gdv1.Visible = false;
                        icons.Visible = false;
                        data += "";
                    }
                    divExport.InnerHtml = data;
                    conn.Close();
                }
                catch (SqlException ex)
                { throw ex; }
            }
        }
    }
}