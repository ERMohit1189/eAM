using System;
using System.Web.UI;
using System.Data;
public partial class admin_PaymentDetail : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Int32 H04ID;

            Int32.TryParse(Request.QueryString["H04ID"].ToString().Trim(), out H04ID);

            DataTable dt = new DataTable();

            BAL.clsDayBook obj = new BAL.clsDayBook();
            obj.H04ID = H04ID;
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
                BLL.BLLInstance.ShowMSG("No Record Found !");
            }

            gvPaymentHistory.DataBind();
        }
    }
}