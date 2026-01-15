using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using c4SmsNew;

namespace admin
{
    public partial class AdminManageResponse : System.Web.UI.Page
    {
        private string txtno;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    List<SqlParameter> param = new List<SqlParameter>();
                    if (Request.QueryString["txnid"] != null)
                    {
                        txtno = Request.QueryString["txnid"];

                        param.Add(new SqlParameter("@txtno", txtno));

                        DataSet ds = new DataSet();

                        ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("GetStatusAdmissionOnline_Proc", param);
                        var recMobile = String.Empty;
                        if (ds != null)
                        {
                            DataTable dt;
                            dt = ds.Tables[0];
                            if (dt.Rows.Count > 0)
                            {
                                var recid = dt.Rows[0]["RecieptNo"].ToString();
                                var recStatus = dt.Rows[0]["Status"].ToString();
                                recMobile = dt.Rows[0]["Mobile"].ToString();
                                if (recStatus == "Paid")
                                {
                                    try
                                    {
                                        Response.Redirect("../2/pafReciept.aspx?print=1&rid=" + recid + "",false);
                                    }
                                    catch (Exception)
                                    {
                                        // ignored
                                    }
                                }
                               //else if (recStatus == "Cancelled" && recMop == "Online")
                               else if (recStatus == "Cancelled")
                                {

                                    try
                                    {
                                        Response.Redirect("~/ap/Admission_Details.aspx?txtno=" + recMobile + "",false);
                                    }
                                    catch (Exception)
                                    {
                                        // ignored
                                    }
                                }
                                else if (recStatus == "")
                                {

                                    try
                                    {
                                        Response.Redirect("~/ap/Admission_Details.aspx?txtno=" + recMobile + "", false);
                                    }
                                    catch (Exception)
                                    {
                                        // ignored
                                    }
                                }
                            }
                        }
                        else
                        {
                            try
                            {
                                Response.Redirect("~/ap/Admission_Details.aspx?txtno=" + recMobile + "", false);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }
                        }
                    }
                }
                catch (ThreadAbortException )
                {
                    // ignored
                }
                catch(Exception)
                {
                    Response.Redirect("../ap/default.aspx");
                }

            }
        }
        
    }
}