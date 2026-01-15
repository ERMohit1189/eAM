using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
public partial class PrintfeeCardServerNew : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string session = Request.Form["session"].ToString().Trim();
        string SrNo = Request.Form["SrNo"].ToString().Trim();
        string Section = Request.Form["Section"].ToString().Trim();
        string ClassId = Request.Form["ClassId"].ToString().Trim();
        string BranchCode = Request.Form["BranchCode"].ToString().Trim();
        string BranchId = Request.Form["BranchId"].ToString().Trim();
        string ClassName = Request.Form["ClassName"].ToString().Trim();
        string Remark = Request.Form["Remark"].ToString().Trim();
        string status = Request.Form["status"].ToString().Trim();
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = BAL.objBal.dbGet_connection().ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = "sp_FeeCard";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@sessionName", session.Trim());
                if (SrNo.Trim() != "")
                { cmd.Parameters.AddWithValue("@SrNo", SrNo.Trim()); }
                cmd.Parameters.AddWithValue("@branchid", BranchId.Trim());
                cmd.Parameters.AddWithValue("@SectionName", Section.Trim());
                cmd.Parameters.AddWithValue("@ClassId", ClassId.Trim());
                cmd.Parameters.AddWithValue("@branchCode", BranchCode.Trim());
                if (status.Trim() != "")
                { cmd.Parameters.AddWithValue("@status", status.Trim()); }
                cmd.Parameters.AddWithValue("@action", "student");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmd.Parameters.Clear();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        cmd.CommandText = "sp_FeeCard";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ClassId", ClassId.Trim());
                        cmd.Parameters.AddWithValue("@sessionName", session.Trim());
                        cmd.Parameters.AddWithValue("@AdmissionType", dt.Rows[i]["TypeOFAdmision"].ToString().Trim());
                        cmd.Parameters.AddWithValue("@CardType", dt.Rows[i]["CardId"].ToString().Trim());
                        cmd.Parameters.AddWithValue("@templetId", "2");
                        cmd.Parameters.AddWithValue("@branchCode", BranchCode.Trim());
                        cmd.Parameters.AddWithValue("@action", "details");
                        SqlDataAdapter das = new SqlDataAdapter(cmd);
                        DataSet dts = new DataSet();
                        das.Fill(dts);
                        cmd.Parameters.Clear();
                        try
                        {
                            if (dts.Tables[2].Rows.Count > 0)
                            {
                                if (dts.Tables[2].Rows[0]["templetId"].ToString() == "2")
                                {
                                    string[] img = dts.Tables[0].Rows[0]["collegeLogo"].ToString().Split(new string[] { "/" }, StringSplitOptions.None);
                                    int count = img.Length - 1;
                                    string logo = "";
                                    if (dts.Tables[0].Rows[0]["collegeLogo"].ToString() != "")
                                    {
                                        logo = "<img src=../uploads/CollegeLogo/" + img[count] + " style='max-width: 70px !important; max-height:60px !important;margin-bottom: 2px;'>";
                                    }
                                    else
                                    {
                                        logo = "<img src='../uploads/CollegeLogo/DefaultCollegeLogo.png' style='max-width: 70px !important; max-height:60px !important;margin-bottom: 2px;'";
                                    }
                                    
                                    Response.Write("<div class='col-sm-12' style='page-break-after: always; border: 2px solid; border-radius:20px; margin-right: 10px; padding-bottom:5px;'>");
                                    Response.Write("<div class='term1 col-sm-12' style=' margin-top:5px; padding:0 !important; border-bottom: 2px solid;'>");
                                    Response.Write("<table class='table mp-table p-table-bordered table-bordered text-uppercase' style='margin-bottom: 0px; width:100%;border:0px !important; margin-top: 0px !important;'><tbody>");
                                    Response.Write("<tr><td style='text-align:center; font-size: 11px !important; border:0px !important; font-weight:bold; width:10%;max-height:70px;'>" + logo + "</td>");
                                    Response.Write("<td style='text-align:center; font-size: 11px !important; border:0px !important; font-weight:bold; width:90%; max-height:70px;'>");
                                    Response.Write("<table style='width:100%;'>");
                                    Response.Write("<tr><td style='text-align:center; font-size: 25px !important; border:0px !important; font-weight:bold;  line-height: 25px !important;'>" + dts.Tables[0].Rows[0]["collegename"].ToString() + "</td></tr>");
                                    Response.Write("<tr><td style='text-align:center; font-size: 11px !important; border:0px !important; font-weight:bold; '><div style='width: 100%; margin: 0 auto;'>" + dts.Tables[0].Rows[0]["address2"].ToString() + "<div></td></tr>");
                                    Response.Write("<tr><td style='text-align:center; font-size: 11px !important; border:0px !important; font-weight:bold; '>Contact No: " + dts.Tables[0].Rows[0]["phone"].ToString() + "</td></tr>");
                                    Response.Write("</table>");
                                    Response.Write("</td>");


                                    Response.Write("</tbody></table>");
                                    Response.Write("</div>");
                                    Response.Write("<div class='col-sm-12'><h2 style='text-align:center; font-size: 18px !important; margin-top: 5px;margin-bottom: 5px;'><b>Fee Card (" + session.ToString() + ")</b></h2></div>");
                                    Response.Write("<table class='table mp-table p-table-bordered table-bordered text-uppercase' style='margin-bottom: 5px; margin - top: 10px; width:100%; border: 0;'><tbody>");
                                    Response.Write("<tr>");
                                    Response.Write("<td style='text-align:Left; border:0px !important; font-size: 11px !important;  width:50%; '><b>Student's Name :</b><p style='border-bottom:1px solid #000; float: right; width: 65%;margin: 0 0 0px !important;'>" + dt.Rows[i]["StudentName"].ToString() + "</p></td>");
                                    Response.Write("<td style='text-align:Left; border:0px !important; font-size: 11px !important;  width:50%;'><b>Sr. No. :</b><p style='border-bottom:1px solid #000; float: right; width: 74%;margin: 0 0 0px !important;'>" + dt.Rows[i]["admissionNo"].ToString() + "</p></td>");
                                    Response.Write("</tr>");

                                    Response.Write("<tr>");
                                    Response.Write("<td style='text-align:Left; border:0px !important; font-size: 11px !important;  width:50%;'><b>Father's Name :</b><p style='border-bottom:1px solid #000;  float: right; width: 65%;margin: 0 0 0px !important;'>" + dt.Rows[i]["FatherName"].ToString() + "</p></td>");
                                    Response.Write("<td style='text-align:Left; border:0px !important; font-size: 11px !important;  width:50%; '><b>Class :</b><p style='border-bottom:1px solid #000; float: right; width: 74%;margin: 0 0 0px !important;'>" + dt.Rows[i]["class_section"].ToString() + "</p></td>");

                                    Response.Write("</tr>");

                                    Response.Write("<tr>");
                                    Response.Write("<td style='text-align:Left; border:0px !important; font-size: 11px !important;  width:50%;'><b>Mother's Name :</b><p style='border-bottom:1px solid #000; float: right; width: 65%;margin: 0 0 0px !important;'>" + dt.Rows[i]["MotherName"].ToString() + "</p></td>");
                                    Response.Write("<td style='text-align:Left; border:0px !important; font-size: 11px !important;  width:50%;'><b>Mob. No. :</b><p style='border-bottom:1px solid #000;  float: right; width: 74%;margin: 0 0 0px !important;'>" + dt.Rows[i]["MobileNumber"].ToString() + "</p></td>");
                                    Response.Write("</tr>");
                                    Response.Write("<tr>");
                                    Response.Write("<td colspan='2' style='text-align:Left; border:0px !important; font-size: 11px !important; width:50%;'><b>Address :</b><p style='border-bottom:1px solid #000; float: right; width: 83%;margin: 0 0 0px !important;'>" + dt.Rows[i]["StPerAddress2"].ToString() + "</p></td>");
                                    Response.Write("</tr>");
                                    Response.Write("</tbody></table>");
                                    string hideClass1 = "hide";
                                    string hideClass = "hide";
                                    Campus oo = new Campus();
                                    string sql = "select count(*)cnt from StudentVehicleAllotment where SessionName='" + session.Trim() + "' and  BranchCode=" + BranchCode.Trim() + " and SrNo='" + dt.Rows[i]["admissionNo"].ToString().Trim() + "'";
                                    if (oo.ReturnTag(sql, "cnt") != "0" && oo.ReturnTag(sql, "cnt") != "")
                                    {
                                        hideClass1 = "";
                                    }
                                    sql = "select count(*)cnt from HostelFeeAllotmentForCondidate where SessionName='" + session.Trim() + "' and CardId=" + dt.Rows[i]["CardId"].ToString().Trim() + " and BranchCode=" + BranchCode.Trim() + " and SrNoOrEmpId='" + dt.Rows[i]["admissionNo"].ToString().Trim() + "'";
                                    if (oo.ReturnTag(sql, "cnt") != "0" && oo.ReturnTag(sql, "cnt") != "")
                                    {
                                        hideClass = "";
                                    }
                                    
                                    Response.Write("<div class='col-sm-12' style='padding: 0;'><h2 style='text-align:left; font-size: 11px !important; margin: 5px 0; float: left;' class='" + hideClass1 + "'><b>Transport Facility :</b> &nbsp;Yes</h2><h2 style='text-align:left; font-size: 11px !important; margin: 5px 0; float:right;' class='" + hideClass + "'><b>Hostel Facility :</b> &nbsp;Yes</h2></div>");
                                    Response.Write("<table class='table mp-table p-table-bordered table-bordered text-uppercase' style='margin-bottom: 5px; width:100%;'><thead>");
                                    Response.Write("<tr><th style='text-align:center; font-size: 11px !important; width: 4%;'>#</th><th style='text-align:left; font-size: 11px !important; width: 21%;'>Installments</th><th  style='text-align:center; font-size: 11px !important; width: 17%;'>Amount Received</th><th  style='text-align:center; font-size: 11px !important; width: 17%;'>Receipt No.</th><th  style='text-align:center; font-size: 11px !important; width: 18%;'>Date</th><th  style='text-align:center; font-size: 11px !important; width: 10%;'>Fine</th><th  style='text-align:center; font-size: 11px !important; width: 13%;'>Sign.</th></tr>");
                                    Response.Write("</thead>");
                                    Response.Write("</tbody>");
                                    for (int m = 0; m < dts.Tables[1].Rows.Count; m++)
                                    {
                                        Response.Write("<tr><td style='text-align:center; font-size: 11px !important;'>" + (m + 1) + "</td><td style='text-align:left; font-size: 11px !important;padding: 0px 5px 0px 5px !important;'>" + dts.Tables[1].Rows[m]["InstallmentName"].ToString() + "</td><td></td><td></td><td></td><td></td><td></td></tr>");
                                    }
                                    Response.Write("</tbody></table>");
                                    Response.Write("</div>");
                                    
                                    Response.Write("</div>");

                                    Response.Write("<div class='col-sm-12' style='page-break-after: always; border: 2px solid; border-radius:20px; margin-right: 10px;margin-top: 5px;'>");
                                    Response.Write("<div class='col-sm-12'><h2 style='text-align:center; font-size: 18px  !important; margin-bottom: 5px;margin-top: 5px;'><b>Fee Structure (" + session.ToString() + ")&nbsp;for Class " + dt.Rows[i]["class_section"].ToString() + "</b></h2></div>");
                                    Response.Write("<table class='table mp-table p-table-bordered table-bordered text-uppercase' style='margin-bottom: 5px; width:100%;font-size: 25px !important;'><tbody>");
                                    double GTotal = 0;
                                    if (dts.Tables[1].Rows.Count > 0)
                                    {
                                        Response.Write("<tr><th style='text-align:center; font-size: 13px !important;padding: 0px 5px 0px 5px!important;'>#</th><th style='text-align:Left; font-size: 11px !important;padding: 0px 5px 0px 5px!important;'>Installments</th><th style='text-align:right; font-size: 11px !important;padding: 0px 5px 0px 0px!important;'>Amount</th></tr>");
                                        int srno = 0;
                                        for (int k = 0; k < dts.Tables[1].Rows.Count; k++)
                                        {
                                            cmd.CommandText = "sp_FeeCard";
                                            cmd.CommandType = CommandType.StoredProcedure;
                                            cmd.Parameters.AddWithValue("@sessionName", session.Trim());
                                            cmd.Parameters.AddWithValue("@ClassId", ClassId.ToString());
                                            cmd.Parameters.AddWithValue("@AdmissionType", dt.Rows[i]["TypeOFAdmision"].ToString());
                                            cmd.Parameters.AddWithValue("@CardType", dt.Rows[i]["CardId"].ToString());
                                            cmd.Parameters.AddWithValue("@monthid", dts.Tables[1].Rows[k]["MonthId"].ToString());
                                            cmd.Parameters.AddWithValue("@branchid", dt.Rows[i]["branchid"].ToString().Trim());
                                            cmd.Parameters.AddWithValue("@SrNo", dt.Rows[i]["admissionNo"].ToString().Trim());
                                            cmd.Parameters.AddWithValue("@branchCode", BranchCode.Trim());
                                            cmd.Parameters.AddWithValue("@action", "FeePayment");
                                            SqlDataAdapter da2 = new SqlDataAdapter(cmd);
                                            DataTable dt2 = new DataTable();
                                            da2.Fill(dt2);
                                            cmd.Parameters.Clear();
                                            if (dt2.Rows.Count > 0)
                                            {
                                                srno = srno + 1;
                                                string amount = ""; double total = 0;
                                                if (dt2.Rows.Count > 1)
                                                {
                                                    for (int j = 0; j < dt2.Rows.Count; j++)
                                                    {
                                                        amount = amount + ((amount == "" ? "" : " + ") + dt2.Rows[j]["FeePayment"].ToString() + " (" + dt2.Rows[j]["FeeType"].ToString() + ") ");
                                                        total = total + double.Parse((dt2.Rows[j]["FeePayment"].ToString() == "" ? "0" : dt2.Rows[j]["FeePayment"].ToString()));
                                                    }
                                                    Response.Write("<tr><td style='text-align:center; font-size: 11px !important;'>" + srno + "</td><td style='text-align:Left; font-size: 11px !important; padding: 0px 5px 0px 5px !important;'>" + dts.Tables[1].Rows[k]["InstallmentName"].ToString() + "</td><td style='text-align:right; font-size: 11px !important; padding-left: 5px !important;'>" + amount.ToString() + " = " + total.ToString("0.00") + "</td></tr>");
                                                }
                                                if (dt2.Rows.Count == 1)
                                                {
                                                    amount = dt2.Rows[0]["FeePayment"].ToString() + " (" + dt2.Rows[0]["FeeType"].ToString() + ") ";
                                                    total = double.Parse((dt2.Rows[0]["FeePayment"].ToString() == "" ? "0" : dt2.Rows[0]["FeePayment"].ToString()));
                                                    Response.Write("<tr><td style='text-align:center; font-size: 11px !important;'>" + srno + "</td><td style='text-align:Left; font-size: 11px !important; padding:  0px 5px 0px 5px  !important;'>" + dts.Tables[1].Rows[k]["InstallmentName"].ToString() + "</td><td style='text-align:right; font-size: 11px !important; padding-left: 5px !important;'>" + amount.ToString() + " = " + total.ToString("0.00") + "</td></tr>");
                                                }
                                                GTotal = GTotal + total;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Fee Installments not found.')", true);
                                    }
                                    Response.Write("<tr><td style='text-align:right; font-size: 11px !important; ' colspan='2'><b>Total</b> </td><td style='text-align:right; font-size: 11px !important; '><b>= " + GTotal.ToString("0.00") + "</b></td></tr>");
                                    Response.Write("</tbody></table>");

                                    Response.Write("<div class='col-sm-12 text-center'><h2 style='text-transform: uppercase;border-radius: 50%;width: 223px; font-size: 18px !important; font-weight:bold; margin: 0 auto; margin-top: 5px !important;'><b>Instructions</b></h2></div>");
                                    if (dts.Tables[2].Rows.Count > 0)
                                    {
                                        Response.Write("<div class='col-sm-12 text-left' style='padding:0px; font-size: 13px !important;line-height: 18px !important;'>" + dts.Tables[2].Rows[0]["Instructions"].ToString() + "</div>");
                                    }
                                    Response.Write("<div class='col-sm-12 text-left' style='"+(Remark!=""? "border-top: 1px solid;" : "") +" font-size: 11px !important; line-height: 22px;'>" + Remark.ToString() + "</div>");
                                    Response.Write("</div>");
                                }
                                else
                                {
                                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Fee Card instructions not found.')", true);
                                    break;
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Fee Card instructions not found.')", true);
                            }
                        }
                        catch (SqlException ex)
                        { throw ex; }
                    }
                }
            }
        }
    }
}