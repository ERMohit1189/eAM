using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class DateWiseCollection : Page
{
    Campus oo = new Campus();
    protected void Page_PreInIt(object sender, EventArgs e)
    {
        if (Session["Logintype"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (Session["Logintype"].ToString() == "SuperAdmin")
        {
            MasterPageFile = "~/50/sadminRootManager.master";
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Campus camp = new Campus(); camp.LoadLoader(loader);
        BLL.BLLInstance.LoadHeader("Report", header);
        if (!IsPostBack)
        {
            dayName.Text = DateTime.Now.ToString("dddd");
            oo.AddDateMonthYearDropDown(DDYear, DDMonth, DDDate);
            oo.FindCurrentDateandSetinDropDown(DDYear, DDMonth, DDDate);
            string sql = "Select BranchId, BranchName from Branchtab";
            var dt = oo.Fetchdata(sql);
            ddlBranch.DataSource = dt;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "BranchId";
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, new ListItem("<--Select Branch-->", ""));
            if (Session["LoginType"].ToString() == "Admin")
            {
                ddlBranch.SelectedValue = Session["BranchCode"].ToString();
                string sqls = "select SessionName from SessionMaster where BranchCode=" + ddlBranch.SelectedValue + "";
                var dt2 = oo.Fetchdata(sqls);
                DrpSessionName.DataSource = dt2;
                DrpSessionName.DataTextField = "SessionName";
                DrpSessionName.DataValueField = "SessionName";
                DrpSessionName.DataBind();
                DrpSessionName.Items.Insert(0, new ListItem("<--Select Session-->", ""));
                DrpSessionName.SelectedIndex = (DrpSessionName.Items.Count - 1);
                
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "removeClassses();", true);
            }
            if (Session["LoginType"].ToString() == "Admin")
            {
                divBranch.Visible = false;
                divSession.Visible = false;
                DrpSessionName.SelectedValue = Session["SessionName"].ToString();
            }
            if (Session["Logintype"].ToString() == "SuperAdmin")
            {
                divBranch.Visible = true;
                divSession.Visible = true;
            }
            DrpSessionName.Items.Insert(0, new ListItem("<--Select-->", ""));
            loadUser();
        }
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadUser();
        if (ddlBranch.SelectedIndex == 0)
        {
            DrpSessionName.Items.Clear();
            DrpSessionName.Items.Insert(0, new ListItem("<--Select Session-->", ""));
            return;
        }
        string sql = "select SessionName from SessionMaster where BranchCode=" + ddlBranch.SelectedValue + "";
        var dt2 = oo.Fetchdata(sql);
        DrpSessionName.DataSource = dt2;
        DrpSessionName.DataTextField = "SessionName";
        DrpSessionName.DataValueField = "SessionName";
        DrpSessionName.DataBind();
        DrpSessionName.Items.Insert(0, new ListItem("<--Select Session-->", ""));
        DrpSessionName.SelectedIndex = (DrpSessionName.Items.Count - 1);
        if (Session["LoginType"].ToString() == "Admin")
        {
            DrpSessionName.SelectedValue = Session["SessionName"].ToString();
        }
    }
    
    protected void loadUser()
    {
        string sql = "";
        if (Session["LoginType"].ToString() == "Admin")
        {
            sql = "Select UserId From NewAdminInformation where BranchCode=" + Session["BranchCode"] + "";
        }
        else
        {
            sql = "Select UserId From NewAdminInformation where BranchCode=" + ddlBranch.SelectedValue + "";
        }
        oo.FillDropDownWithOutSelect(sql, DropDownList1, "UserId");
        DropDownList1.Items.Insert(0, new ListItem("All", ""));
    }
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        oo.ExportTolandscapeWord(Response, "AditionalFeeReport", gdv1);
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        oo.ExportDivToExcelWithFormatting(Response, "AditionalFeeReport.xls", gdv1, Server.MapPath("~/Admin/css/style.css"));
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {
        oo.ExporttolandscapePdf(Response, "AditionalFeeReport", gdv1);
    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = gdv1;
        ClientScript.RegisterStartupScript(GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }
    protected void DDYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.YearDropDown(DDYear, DDMonth, DDDate);
    }
    protected void DDMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.MonthDropDown(DDYear, DDMonth, DDDate);

    }
    protected void DDDate_SelectedIndexChanged(object sender, EventArgs e)
    {
        DateTime Date = DateTime.Parse(DDDate.SelectedItem.Text + "-" + DDMonth.SelectedItem.Text + "-" + DDYear.SelectedItem.Text);
        dayName.Text = Date.ToString("dddd");
    }
    protected void loaddata()
    {
        DataTable TDataHead = new DataTable();
        string table = "";
        string day = "";
        if (int.Parse(DDDate.SelectedItem.Text) < 10)
        {
            day = "0" + DDDate.SelectedItem.Text;
        }
        else
        {
            day = DDDate.SelectedItem.Text;
        }
        string Date = day + "-" + DDMonth.SelectedItem.Text + "-" + DDYear.SelectedItem.Text;
        string PaymentMode = DdlpaymentMode.SelectedValue;
        string Status = drpStatus.SelectedValue;
        string SessionName = DrpSessionName.SelectedValue;
        string BranchCode = ddlBranch.SelectedValue;
        string isExclude = chkExclude.Checked?"1":"0";
        string user = DropDownList1.SelectedValue;
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = BAL.objBal.dbGet_connection().ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = "sp_DatewiseCollection";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DepositDate", Date.Trim());
                if (PaymentMode != "All")
                {
                    cmd.Parameters.AddWithValue("@PaymentMode", PaymentMode.Trim());
                }
                if (Status != "All")
                {
                    cmd.Parameters.AddWithValue("@Status", Status.Trim());
                }
                cmd.Parameters.AddWithValue("@SessionName", SessionName.Trim());
                cmd.Parameters.AddWithValue("@BranchCode", BranchCode.Trim());
                if (user != "")
                {
                    cmd.Parameters.AddWithValue("@LoginName", user.Trim());
                }
                cmd.Parameters.AddWithValue("@Action", "students");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmd.Parameters.Clear();
                if (dt.Rows.Count > 0)
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        cmd.CommandText = "sp_DatewiseCollection";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@SrNo", dt.Rows[i]["SrNo"].ToString().Replace("_adm", ""));
                        cmd.Parameters.AddWithValue("@DepositDate", Date.Trim());
                        if (PaymentMode != "All")
                        {
                            cmd.Parameters.AddWithValue("@PaymentMode", PaymentMode.Trim());
                        }
                        if (Status != "All")
                        {
                            cmd.Parameters.AddWithValue("@Status", Status.Trim());
                        }
                        cmd.Parameters.AddWithValue("@ReceiptNo", dt.Rows[i]["ReceiptNo"].ToString());
                        cmd.Parameters.AddWithValue("@SessionName", SessionName.Trim());
                        cmd.Parameters.AddWithValue("@BranchCode", BranchCode.Trim());
                        cmd.Parameters.AddWithValue("@IsExcludeOtherFee", isExclude.Trim());
                        if (user != "")
                        {
                            cmd.Parameters.AddWithValue("@LoginName", user.Trim());
                        }
                        cmd.Parameters.AddWithValue("@action", "details");
                        SqlDataAdapter das = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        das.Fill(ds);
                        cmd.Parameters.Clear();
                        if (ds.Tables[1].Rows.Count > 0)
                        {

                            if (TDataHead == null || TDataHead.Columns.Count == 0)
                            {
                                for (int j = 0; j < ds.Tables[1].Rows.Count; j++)
                                {
                                    TDataHead.Columns.Add(ds.Tables[1].Rows[j]["HeadName"].ToString(), typeof(string));
                                }
                            }
                            else
                            {
                                for (int j = 0; j < ds.Tables[1].Rows.Count; j++)
                                {
                                    int exits = 0;
                                    for (int k = 0; k < TDataHead.Columns.Count; k++)
                                    {
                                        if (ds.Tables[1].Rows[j]["HeadName"].ToString() == TDataHead.Columns[k].ToString())
                                        {
                                            exits = 1;
                                        }
                                    }
                                    if (exits == 0)
                                    {
                                        TDataHead.Columns.Add(ds.Tables[1].Rows[j]["HeadName"].ToString(), typeof(string));
                                    }
                                }
                            }
                        }
                    }


                    table += "<div class=' table-responsive  table-responsive2 '>";



                    double Gtotal1 = 0; double Gtotal = 0; int tdcount = 0;

                    table += "<table class='table table-striped table-hover no-bm no-head-border table-bordered pro-table table-header-group'><tbody>";
                    table += "<tr align='center' valign='middle'>";
                    table += "<th>#</th><th>Receipt No.</th><th>Status</th><th>Mode</th><th>Installment</th><th>S.R. No.</th><th>Student's Name</th><th>Class</th>";
                    tdcount = tdcount + 8;
                    for (int i = 0; i < TDataHead.Columns.Count; i++)
                    {
                        tdcount = tdcount + 1;
                        table += "<th>" + TDataHead.Columns[i].ToString() + "</th>";
                    }
                    table += "<th>Fine</th><th>Discount</th><th>Total Fee</th><th>Total Deposit</th>";
                    table += "</tr>";
                    tdcount = tdcount + 4;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        cmd.CommandText = "sp_DatewiseCollection";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@SrNo", dt.Rows[i]["SrNo"].ToString().Replace("_adm", ""));
                        cmd.Parameters.AddWithValue("@ReceiptNo", dt.Rows[i]["ReceiptNo"].ToString());
                        cmd.Parameters.AddWithValue("@DepositDate", Date.Trim());
                        if (PaymentMode != "All")
                        {
                            cmd.Parameters.AddWithValue("@PaymentMode", PaymentMode.Trim());
                        }
                        if (Status != "All")
                        {
                            cmd.Parameters.AddWithValue("@Status", Status.Trim());
                        }
                        cmd.Parameters.AddWithValue("@SessionName", SessionName.Trim());
                        cmd.Parameters.AddWithValue("@BranchCode", BranchCode.Trim());
                        cmd.Parameters.AddWithValue("@IsExcludeOtherFee", isExclude.Trim());
                        if (user != "")
                        {
                            cmd.Parameters.AddWithValue("@LoginName", user.Trim());
                        }
                        cmd.Parameters.AddWithValue("@action", "details");
                        SqlDataAdapter das = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        das.Fill(ds);
                        cmd.Parameters.Clear();
                        for (int s = 0; s < TDataHead.Rows.Count; s++)
                        {
                            TDataHead.Rows[s].Delete();
                        }

                        if (ds.Tables[1].Rows.Count > 0)
                        {
                            DataRow dtrow = TDataHead.NewRow();
                            for (int k = 0; k < TDataHead.Columns.Count; k++)
                            {
                                int exits = 0;
                                for (int j = 0; j < ds.Tables[1].Rows.Count; j++)
                                {

                                    if (ds.Tables[1].Rows[j]["HeadName"].ToString() == TDataHead.Columns[k].ToString())
                                    {
                                        dtrow[TDataHead.Columns[k].ToString()] = ds.Tables[1].Rows[j]["Amount"].ToString();
                                        exits = 1;
                                    }
                                }
                                if (exits == 0)
                                {
                                    dtrow[TDataHead.Columns[k].ToString()] = "";
                                }
                            }
                            TDataHead.Rows.Add(dtrow);
                        }
                        double total = 0;
                        if (TDataHead.Rows.Count > 0)
                        {
                            div1.Visible = true;
                            gdv1.Visible = true;
                            heading.Text = "Date Wise Collection of " + Date + " ("+ dayName.Text + ")";
                            string str = "";
                            if (drpStatus.SelectedIndex != 0)
                            {
                                str = str + " Status : " + drpStatus.SelectedItem.Text;
                            }
                            if (DdlpaymentMode.SelectedIndex != 0 && drpStatus.SelectedIndex != 0)
                            {
                                str = str + " | Mode : " + DdlpaymentMode.SelectedItem.Text;
                            }
                            if (DdlpaymentMode.SelectedIndex != 0 && drpStatus.SelectedIndex == 0)
                            {
                                str = str + " Mode : " + DdlpaymentMode.SelectedItem.Text;
                            }

                            if (DropDownList1.SelectedIndex != 0 && DdlpaymentMode.SelectedIndex != 0)
                            {
                                str = str + " | User : " + DropDownList1.SelectedItem.Text;
                            }
                            if (DropDownList1.SelectedIndex != 0 && DdlpaymentMode.SelectedIndex == 0)
                            {
                                    str = str + " User : " + DropDownList1.SelectedItem.Text;
                            }
                            if (DdlpaymentMode.SelectedIndex != 0 || drpStatus.SelectedIndex != 0 || DropDownList1.SelectedIndex != 0)
                            {
                                if (chkExclude.Checked)
                                {
                                    str = str + " | Without Other fee";
                                }
                                
                            }
                            else
                            {
                                if (chkExclude.Checked)
                                {
                                    str = str + "  Without Other fee";
                                }
                                
                            }
                            
                            lblRegister.Text = str;
                            table += "<tr>";
                            table += "<td>" + (i + 1) + "</td><td>" + dt.Rows[i]["ReceiptNo"].ToString() + "</td><td>" + dt.Rows[i]["Status"].ToString() + "</td><td>" + dt.Rows[i]["Mode"].ToString() + "</td>";

                            string Months = "";
                            if (ds.Tables[3].Rows.Count > 0 && ds.Tables[0].Rows.Count>0)
                            {
                                Months = Months + " " + ds.Tables[3].Rows[0]["MonthNames"].ToString() + ", ";
                            }
                            if (ds.Tables[3].Rows.Count > 0 && ds.Tables[0].Rows.Count == 0)
                            {
                                Months = Months + " " + ds.Tables[3].Rows[0]["MonthNames"].ToString() + " ";
                            }
                            for (int p = 0; p < ds.Tables[0].Rows.Count; p++)
                            {
                                if (ds.Tables[0].Rows.Count > 1)
                                {
                                    if (p == 0)
                                    {
                                        Months = Months + ds.Tables[0].Rows[p]["MonthNames"].ToString();
                                    }
                                    if (p == (ds.Tables[0].Rows.Count - 1))
                                    {
                                        Months = Months + " - " + ds.Tables[0].Rows[p]["MonthNames"].ToString();
                                    }
                                }
                                else
                                {
                                    Months = Months + ds.Tables[0].Rows[p]["MonthNames"].ToString();
                                }
                            }

                            string sst = "";
                            try
                            {
                                string[] ss = dt.Rows[i]["SrNo"].ToString().Split(new string[] { "_" }, StringSplitOptions.None);
                                sst = ss[1];
                            }
                            catch (Exception)
                            {
                            }

                            string srno = dt.Rows[i]["SrNo"].ToString();
                            if (sst == "adm")
                            {
                                srno = "";
                            }
                            table += "<td>" + Months + "</td>";
                            table += "<td>" + srno + "</td><td>" + dt.Rows[i]["Name"].ToString() + "</td><td>" + dt.Rows[i]["CombineClassName"].ToString() + "</td>";

                            for (int m = 0; m < TDataHead.Columns.Count; m++)
                            {

                                table += "<td class='text-right'>" + TDataHead.Rows[0][TDataHead.Columns[m].ToString()].ToString() + "</td>";
                                total = total + double.Parse(TDataHead.Rows[0][TDataHead.Columns[m].ToString()].ToString() == "" ? "0" : TDataHead.Rows[0][TDataHead.Columns[m].ToString()].ToString());
                            }

                            if (ds.Tables[2].Rows.Count > 0)
                            {
                                table += "<td class='text-right'>" + ds.Tables[2].Rows[0]["Fine"].ToString() + "</td>";
                                table += "<td class='text-right'>" + ds.Tables[2].Rows[0]["Discount"].ToString() + "</td>";
                                total = total + double.Parse(ds.Tables[2].Rows[0]["Fine"].ToString() == "" ? "0" : ds.Tables[2].Rows[0]["Fine"].ToString());
                                total = total - double.Parse(ds.Tables[2].Rows[0]["Discount"].ToString() == "" ? "0" : ds.Tables[2].Rows[0]["Discount"].ToString());
                                table += "<td class='text-right'>" + total.ToString("0.00") + "</td>";
                                table += "<td class='text-right'>" + (dt.Rows[i]["Status"].ToString() == "Paid" ? double.Parse(ds.Tables[2].Rows[0]["Paid"].ToString()).ToString("0.00") : "0.00") + "</td>";
                                if (dt.Rows[i]["Status"].ToString() == "Paid")
                                {
                                    Gtotal = Gtotal + double.Parse(ds.Tables[2].Rows[0]["Paid"].ToString());
                                }
                                Gtotal1 = Gtotal1 + total;
                            }
                            else
                            {
                                table += "<td></td><td></td><td></td>";
                            }
                            table += "</tr>";
                        }//--
                    }
                    table += "<tr><td colspan='" + (tdcount - 3) + "'></td><td class='text-right'><b>Total</b></td><td class='text-right'><b>" + Gtotal1.ToString("0.00") + "</b></td><td class='text-right'><b>" + Gtotal.ToString("0.00") + "</b></td></tr>";
                    table += "</tbody></table>";
                }
                else
                {
                    div1.Visible = false;
                    gdv1.Visible = false;
                }
            }

        }
        divExport.InnerHtml = table;
    }



    protected void btnView_Click(object sender, EventArgs e)
    {
        loaddata();
    }
}