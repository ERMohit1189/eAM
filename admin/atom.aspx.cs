using System;

public partial class admin_payu : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Campus camp = new Campus(); camp.LoadLoader(loader);
        if(!IsPostBack)
        {
            GetPaymentGateway();
        }
    }

    protected void lnkSubmit_Click(object sender, EventArgs e)
    {
        SetPaymentGateway();
    }

    public void SetPaymentGateway()
    {
        
    }

    public void GetPaymentGateway()
    {
       
    }
}