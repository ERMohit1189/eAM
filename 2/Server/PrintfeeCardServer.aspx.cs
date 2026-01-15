using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web.UI;
using System.Windows.Forms;

public partial class PrintfeeCardServer : Page
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
        string feemedium = Request.Form["feemedium"].ToString().Trim();
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
                cmd.Parameters.AddWithValue("@FeeMedium", feemedium.ToString());
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmd.Parameters.Clear();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        PictureBox imageControl = new PictureBox();
                        Zen.Barcode.Code128BarcodeDraw barcode = Zen.Barcode.BarcodeDrawFactory.Code128WithChecksum;
                        var image = barcode.Draw(dt.Rows[i]["admissionNo"].ToString().Trim(), 50);
                        using (var graphics = Graphics.FromImage(image))
                        using (var font = new Font("Consolas", 12)) // Any font you want
                        using (var brush = new SolidBrush(Color.White))
                        using (var format = new StringFormat() { LineAlignment = StringAlignment.Near }) // To align text above the specified point
                        {
                            // Print a string at the left bottom corner of image
                            graphics.DrawString(dt.Rows[i]["admissionNo"].ToString().Trim(), font, brush, 0, image.Height, format);
                        }

                        imageControl.Image = image;
                        System.IO.MemoryStream ms = new MemoryStream();
                        image.Save(ms, ImageFormat.Jpeg);
                        byte[] byteImage = ms.ToArray();
                        var SigBase64 = Convert.ToBase64String(byteImage);

                        cmd.CommandText = "sp_FeeCard";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ClassId", ClassId.Trim());
                        cmd.Parameters.AddWithValue("@sessionName", session.Trim());
                        cmd.Parameters.AddWithValue("@AdmissionType", dt.Rows[i]["TypeOFAdmision"].ToString().Trim());
                        cmd.Parameters.AddWithValue("@CardType", dt.Rows[i]["CardId"].ToString().Trim());
                        cmd.Parameters.AddWithValue("@templetId", "1");
                        cmd.Parameters.AddWithValue("@branchCode", BranchCode.Trim());
                        cmd.Parameters.AddWithValue("@action", "details");
                        cmd.Parameters.AddWithValue("@FeeMedium", feemedium.ToString());
                        SqlDataAdapter das = new SqlDataAdapter(cmd);
                        DataSet dts = new DataSet();
                        das.Fill(dts);
                        cmd.Parameters.Clear();
                        try
                        {
                            if (dts.Tables[2].Rows.Count > 0)
                            {
                               
                                if (dts.Tables[2].Rows[0]["templetId"].ToString() == "1")
                                {
                                    Response.Write("<div id='break_div' class='term1' style='width: 100%;height:758px;display:flex; gap:22px; page-break-after: always; margin-top:3px;margin-bottom:3px; padding:10px !important;'>");
                                    Response.Write("<div class='col-sm-6' style='border: 2px solid;  padding-left:8px; padding-right:8px;height: 758px !important;'>");
                                    Response.Write("<div class='col-sm-12' style='padding: 0;'><h2 style='text-align:center; font-size: 20px !important; margin-top: 15px;padding: 0;'><b>Fee Structure (" + session.ToString() + ")</b> for Class " + dt.Rows[i]["class_section"].ToString() + "</h2></div>");
                                    Response.Write("<table class='table mp-table p-table-bordered table-bordered text-uppercase' style='margin-bottom: 5px; width:100%;font-size: 25px !important;'><tbody>");
                                    double GTotal = 0;
                                    if (dts.Tables[1].Rows.Count > 0)
                                    {
                                        Response.Write("<tr><th style='text-align:center; font-size: 13px !important;padding: 5px 5px 5px 5px!important;'>#</th><th style='text-align:Left; font-size: 14px !important;padding: 10px 5px 10px 5px!important;'>Installments</th><th style='text-align:right; font-size: 14px !important;padding: 10px 5px 10px 0px!important;'>Amount</th></tr>");
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
                                            cmd.Parameters.AddWithValue("@FeeMedium", feemedium.ToString());
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
                                                    Response.Write("<tr><td style='text-align:center; font-size: 12px !important;'>" + srno + "</td><td style='text-align:Left; font-size: 12px !important; padding: 7px 5px 7px 5px !important;'>" + dts.Tables[1].Rows[k]["InstallmentName"].ToString() + "</td><td style='text-align:right; font-size: 12px !important; padding: 7px 5px 7px 5px!important; !important;'>" + amount.ToString() + " = " + total.ToString("0.00") + "</td></tr>");
                                                }
                                                if (dt2.Rows.Count == 1)
                                                {
                                                    amount = dt2.Rows[0]["FeePayment"].ToString() + " (" + dt2.Rows[0]["FeeType"].ToString() + ") ";
                                                    total = double.Parse((dt2.Rows[0]["FeePayment"].ToString() == "" ? "0" : dt2.Rows[0]["FeePayment"].ToString()));
                                                    Response.Write("<tr><td style='text-align:center; font-size: 12px !important;'>" + srno + "</td><td style='text-align:Left; font-size: 12px !important; padding:  7px 5px 7px 5px  !important;'>" + dts.Tables[1].Rows[k]["InstallmentName"].ToString() + "</td><td style='text-align:right; font-size: 12px !important; padding: 7px 5px 7px 5px!important;'>" + amount.ToString() + " = " + total.ToString("0.00") + "</td></tr>");
                                                }
                                                GTotal = GTotal + total;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Fee Installments not found.')", true);
                                    }
                                    Response.Write("<tr><td style='text-align:right; font-size: 15px !important; padding: 5px !important;' colspan='2'><b>Total</b> </td><td style='text-align:right; font-size: 15px !important; padding: 5px !important;'><b>= " + GTotal.ToString("0.00") + "</b></td></tr>");
                                    Response.Write("</tbody></table>");
                                    Response.Write("</div>");
                                    Response.Write("<div class='col-sm-6' style='border: 2px solid;padding-left:8px; padding-right:8px;height: 758px  !important; vertical-align: top !important;'>");
                                    Response.Write("<table class='table mp-table p-table-bordered table-bordered text-uppercase' style='margin-bottom: 5px; width:100%;border:0px !important; margin-top: 3px !important;'><tbody>");
                                    Response.Write("<tr><td style='text-align:center; font-size: 32px !important; border:0px !important; font-weight:bold; padding-top: 5px !important; line-height: 35px !important;'>" + dts.Tables[0].Rows[0]["collegename"].ToString() + "</td></tr>");
                                    Response.Write("<tr><td style='text-align:center; font-size: 15px !important; border:0px !important; font-weight:bold; padding-top: 5px !important;'><i>Affiliated by " + dts.Tables[0].Rows[0]["affiliatedto"].ToString() + "</i></td></tr>");
                                    Response.Write("<tr><td style='text-align:center; font-size: 15px !important; border:0px !important; font-weight:bold; padding-top: 5px !important;'><div style='width: 300px; margin: 0 auto;'>" + dts.Tables[0].Rows[0]["address2"].ToString() + "<div></td></tr>");
                                    Response.Write("<tr><td style='text-align:center; font-size: 15px !important; border:0px !important; font-weight:bold; padding-top: 5px !important;'>Phone No.: " + dts.Tables[0].Rows[0]["phone"].ToString() + "</td></tr>");
                                    string[] img = dts.Tables[0].Rows[0]["collegeLogo"].ToString().Split(new string[] { "/" }, StringSplitOptions.None);
                                    int count = img.Length - 1;
                                    string logo = "";
                                    if (dts.Tables[0].Rows[0]["collegeLogo"].ToString() != "")
                                    {
                                        logo = "<img src=../uploads/CollegeLogo/" + img[count] + " style='max-height: 100px!important;'>";
                                    }
                                    else
                                    {
                                        logo = "<img src='../uploads/CollegeLogo/DefaultCollegeLogo.png' style='max-height: 100px!important;'";
                                    }
                                    Response.Write("<tr><td style='text-align:center; font-size: 15px !important; border:0px !important; font-weight:bold;padding-top: 0px !important;'>"+ logo + "</td></tr>");
                                    Response.Write("<tr><td style='text-align:center; font-size: 15px !important; border:0px !important; font-weight:bold; padding-top: 5px !important;'>FEE CARD (SESSION " + session.ToString() + ")</td></tr>");
                                    //Response.Write("<tr><td style='text-align:center; font-size: 15px !important; border:0px !important; font-weight:bold; padding-bottom: 40px !important;'><div id='Qr" + i + "'></div><span id='QrData" + i + "'>" + dt.Rows[i]["admissionNo"].ToString() + "</span><span class='spnCount'>" + dt.Rows.Count + "</span></td></tr>");

                                    Response.Write("<tr><td style='vertical-align: top !important;'>");
                                    Response.Write("<table class='table mp-table p-table-bordered table-bordered text-uppercase' style='margin-bottom: 0px; margin - top: 10px; width:100%; border: 0;'><tbody>");
                                    Response.Write("<tr><td style='text-align:Left; border:0px !important; font-size: 14px !important; padding-bottom: 5px !important; padding-top: 15px !important;'><b>Student's Name :</b><p style='border-bottom:1px solid #000; float: right; width: 70%;'>" + dt.Rows[i]["StudentName"].ToString() + "</p></td></tr>");
                                    Response.Write("<tr><td style='text-align:Left; border:0px !important; font-size: 14px !important; padding-bottom: 5px !important;'><b>Class :</b><p style='border-bottom:1px solid #000; float: right; width: 87%;'><span id='classsecton'>" + dt.Rows[i]["CombineClassName"].ToString() + "</span>&nbsp;&nbsp;&nbsp;&nbsp;<b>S.R. No. :&nbsp;</b><span>" + dt.Rows[i]["admissionNo"].ToString() + "</span></p></td></tr>");
                                    Response.Write("<tr><td style='text-align:Left; border:0px !important; font-size: 14px !important; padding-bottom: 5px !important;'><b>Father's Name :</b><p style='border-bottom:1px solid #000;  float: right; width: 71%;'>" + dt.Rows[i]["FatherName"].ToString() + "</p></td></tr>");
                                    Response.Write("<tr><td style='text-align:Left; border:0px !important; font-size: 14px !important; padding-bottom: 5px !important;'><b>Mother's Name :</b><p style='border-bottom:1px solid #000; float: right; width: 69%;'>" + dt.Rows[i]["MotherName"].ToString() + "</p></td></tr>");
                                    Response.Write("<tr><td style='text-align:Left; border:0px !important; font-size: 14px !important; padding-bottom: 5px !important;'><b>Mobile No. :</b><p style='border-bottom:1px solid #000;  float: right; width: 75%;'>" + dt.Rows[i]["MobileNumber"].ToString() + "</p></td></tr>");
                                    Response.Write("<tr><td style='text-align:Left; border:0px !important; font-size: 14px !important;'><b>Address :</b><p style='border-bottom:1px solid #000; float: right; width: 82%;'>" + dt.Rows[i]["StPerAddress"].ToString() + "</p></td></tr>");
                                    Response.Write("</tbody></table><br/>");
                                    Response.Write("</td></tr>");
                                    Response.Write("<tr><td style='text-align:center !important;padding: 16px !important;'><img src='data:image/png;base64," + SigBase64 + "' style='height: 50px!important;' /></td></tr>");
                                    Response.Write("</tbody></table>");
                                    Response.Write("</div>");
                                    Response.Write("</div>");
                                    string hideClass = "hide";
                                    Campus oo = new Campus();
                                    string sql = "select count(*)cnt from HostelFeeAllotmentForCondidate where SessionName='" + session.Trim() + "' and CardId=" + dt.Rows[i]["CardId"].ToString().Trim() + " and BranchCode=" + BranchCode.Trim() + " and SrNoOrEmpId='" + dt.Rows[i]["admissionNo"].ToString().Trim() + "'";
                                    if (oo.ReturnTag(sql, "cnt")!="0" && oo.ReturnTag(sql, "cnt") != "")
                                    {
                                        hideClass = "";
                                    }
                                    Response.Write("<div id='break_div' class='term1 col-sm-12' style='width:100%;height:758px; display:flex; page-break-after: always; margin-top:3px;margin-bottom:3px; padding:10px !important;'>");
                                    Response.Write("<div class='col-sm-6' style='border: 2px solid; margin-right: 22px;padding-left:8px; padding-right:8px;height: 758px !important;'>");
                                   
                                    Response.Write("<table class='table mp-table p-table-bordered table-bordered text-uppercase' style='margin-bottom:8px;margin-top:8px; width:100%;'><thead>");
                                    Response.Write("<tr><th style='text-align:center; font-size: 12px !important; width: 4%;'>#</th><th style='text-align:left; font-size: 14px !important;padding:3px!important; width: 21%;'>Installments</th><th  style='text-align:center; font-size: 14px !important;padding: 5px!important; width: 15%;'>Amount<br/>Received</th><th  style='text-align:center; font-size: 14px !important;padding: 5px!important; width: 17%;'>Receipt No & Date </th><th  style='text-align:center; font-size: 14px !important;padding: 5px!important; width: 18%;'>Bal.</th><th  style='text-align:center; font-size: 14px !important;padding: 5px!important; width: 12%;'>Receipt No & Date </th><th  style='text-align:center; font-size: 14px !important;padding: 5px!important; width: 13%;'>Sign</th></tr>");
                                    Response.Write("</thead>");
                                    Response.Write("</tbody>");
                                    for (int m = 0; m < dts.Tables[1].Rows.Count; m++)
                                    {
                                        Response.Write("<tr><td style='text-align:center; font-size: 12px !important;'>" + (m + 1) + "</td><td style='text-align:left; font-size: 12px !important; padding:  7px 5px 7px 5px  !important;'>" + dts.Tables[1].Rows[m]["InstallmentName"].ToString() + "</td><td></td><td></td><td></td><td></td><td></td></tr>");
                                    }
                                    Response.Write("</tbody></table>");
                                    Response.Write("</div>");
                                    Response.Write("<div class='col-sm-6' style='border: 2px solid;padding-left:8px; padding-right:8px;height: 758px !important;'>");
                                    Response.Write("<div class='col-sm-12 text-center'><h2 style='text-transform: uppercase;border-radius: 50%;width: 223px; font-size: 20px !important; font-weight:bold; margin: 0 auto; margin-top: 10px !important; box-shadow:0px 3px 0px 2px #444242 !important; border: 1px solid; padding:10px;'>Instructions</h2></div>");
                                    if (dts.Tables[2].Rows.Count > 0)
                                    {
                                        Response.Write("<div class='col-sm-12 text-left' style='padding-top:12px; font-size: 13px !important; height: 579px !important; line-height: 18px !important;'>" + dts.Tables[2].Rows[0]["Instructions"].ToString() + "</div>");
                                    }
                                    Response.Write("<div class='col-sm-12 text-left' style='height:70px;border-top: 1px solid; font-size: 14px !important; line-height: 22px;'>" + Remark.ToString() + "</div>");
                                    Response.Write("</div>");
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