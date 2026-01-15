using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _7
{
    public partial class AdminPaymentHistory : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var obj = new BAL.clsVendorQuotation();
            if(!IsPostBack)
            {
                Int32 v04Id;

                Int32.TryParse(Request.QueryString["V04ID"].Trim(),out v04Id);

                // ReSharper disable once RedundantAssignment
                var dt = new DataTable();

                obj.V01ID = Convert.ToInt32(Default.All);
                obj.V04V05ID = v04Id;
                obj.IsQuotation = (Int32)Default.All;
                obj.VendorID = string.Empty;
                obj.H06ID = (Int32)Default.All;
                obj.IsActive = (Int32)Default.All;
                obj.IsQuotation = (Int32)Default.All;

                dt = DAL.DALInstance.GetPaymentHistory(obj);

                if (dt != null && dt.Rows.Count > 0)
                {
                    dt = BLL.BLLInstance.GetSerialNo(ref dt, "SrNo");
                    gvPaymentHistory.Visible = true;
                    gvPaymentHistory.DataSource = dt;
                }
                else
                {
                    gvPaymentHistory.DataSource = null;
                    //  BLL.BLLInstance.ShowMSG("No Record Found !");
                    // msgboxshow(Page, msgbox, "No Record Found!", "A");
             
                    const string msg = "Sorry, No record(s) found!";
                    ScriptManager.RegisterStartupScript(Page, GetType(), "alert", "msgboxnew('" + msgbox.ClientID + "','" + msg + "','W');", true);
                }

                gvPaymentHistory.DataBind();
            }
        }

        bool _isHideDetail = true;
        protected void gvPaymentHistory_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[5].Text.Trim() == "Cash")
                    // ReSharper disable once PossibleNullReferenceException
                    (e.Row.FindControl("lbtnDetail") as LinkButton).Visible = false;

                // ReSharper disable once RedundantBoolCompare
                if (_isHideDetail == true)
                {
                    if (e.Row.Cells[5].Text.Trim() != "Cash")
                        _isHideDetail = false;
                }
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                // ReSharper disable once RedundantBoolCompare
                if (_isHideDetail == true)
                {
                    gvPaymentHistory.Columns[7].Visible = false;
                }
            }
        }

        protected void lbtnDetail_Click(object sender, EventArgs e)
        {     
            // ReSharper disable once PossibleNullReferenceException
            var row = (GridViewRow)(sender as LinkButton).Parent.Parent;
            var path = ((Label)row.FindControl("lblDetail")).Text.Trim();
            ScriptManager.RegisterStartupScript(this, Page.GetType(), "onclick", "javascript:openDetail('PaymentDetail.aspx?H04ID=" + path + "', 'Detail');", true);
        }


    }
}