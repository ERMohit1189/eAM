using System;
using System.Data;
using System.Web.UI;

namespace _7
{
    public partial class AdminPaymentDetail : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var obj = new BAL.clsDayBook();
            if (!IsPostBack)
            {
                Int32 h04Id;

                Int32.TryParse(Request.QueryString["H04ID"].Trim(), out h04Id);

                // ReSharper disable once RedundantAssignment
                var dt = new DataTable();

                obj.H04ID = h04Id;
                obj.H03ID = -1;
                obj.M06ID = -1;

                dt = DAL.DALInstance.GetPaymentDetail(obj);

                if (dt != null && dt.Rows.Count > 0)
                {
                    dt = BLL.BLLInstance.GetSerialNo(ref dt, "SrNo");
                    gvPaymentHistory.Visible = true;
                    gvPaymentHistory.DataSource = dt;
                }
                else
                {
                    gvPaymentHistory.DataSource = null;
                    const string msg = "Sorry, No record(s) found!";
                    ScriptManager.RegisterStartupScript(Page, GetType(), "alert", "msgboxnew('" + msgbox.ClientID + "','" + msg + "','W');", true);
                }

                gvPaymentHistory.DataBind();
            }
        }
    }
}