using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;

public partial class FeePaymentFailed : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Session.Abandon();
        Session.Clear();
    }

}


   