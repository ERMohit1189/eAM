using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

public partial class DatewiseCollectionServerAll : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable TDataHead = new DataTable();
        string table = "";
        string Date = Request.Form["Date"].ToString().Trim();
        string PaymentMode = Request.Form["PaymentMode"].ToString().Trim();
        string Status = Request.Form["Status"].ToString().Trim();
        string SessionName = Request.Form["SessionName"].ToString().Trim();
        string BranchCode = Request.Form["BranchCode"].ToString().Trim();
        string isExclude = Request.Form["isExclude"].ToString().Trim();
        string user = Request.Form["user"].ToString().Trim();
        string extravar = "";
        using (SqlConnection conn = new SqlConnection())
        {
            extravar= "<input type='hidden' id='hdncnts' value='0' />";

            table += extravar;
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
                if (user!="")
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
                    table = "";
                    extravar = "<input type='hidden' id='hdncnts' value='5' />";
                    table += extravar;
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
                    DateTime dts = DateTime.Parse(Date);
                    DayOfWeek dow = dts.DayOfWeek;
                    string str = dow.ToString();

                    table += "<div class=' table-responsive  table-responsive2 '>";

                    table += "<table id='abc' class='table td-th-2'>";
                    table += "<tr><td><div style='margin-top: 10px;' class='divHeader'></div></td></tr><tr class='text-center'>";
                    table += "<td><span>Daily Collection Report | </span><span>" + Date + " (" + str + ")</span></td></tr><tr>";
                    table += "<td id='DivGrid'>";

                    double Gtotal1 = 0; double Gtotal = 0; int tdcount = 0;

                    //Response.Write("<div  style='margin-top: 10px;' class='divHeader'></div>");
                    table += "<table class='table p-table-bordered table-bordered' style='border-collapse:collapse;'><tbody>";
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

                            table += "<tr>";
                            table += "<td>" + (i + 1) + "</td><td>" + dt.Rows[i]["ReceiptNo"].ToString() + "</td><td>" + dt.Rows[i]["Status"].ToString() + "</td><td>" + dt.Rows[i]["Mode"].ToString() + "</td>";

                            string Months = "";
                            if (ds.Tables[3].Rows.Count > 0)
                            {
                                Months = Months + " " + ds.Tables[3].Rows[0]["MonthNames"].ToString()+", ";
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
                                string[] ss = dt.Rows[i]["SrNo"].ToString().Split(new string[] {"_" }, StringSplitOptions.None);
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
                                table += "<td class='text-right'>" + (dt.Rows[i]["Status"].ToString() == "Paid"? double.Parse(ds.Tables[2].Rows[0]["Paid"].ToString()).ToString("0.00"):"0.00") + "</td>";
                                if (dt.Rows[i]["Status"].ToString()=="Paid")
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
                    table += "</td></tr></table></div>";
                }
            }

        }
        Response.Write(table);
    }

}