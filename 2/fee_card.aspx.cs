using System;

namespace _2
{
    public partial class FeeCard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BLL.BLLInstance.LoadHeader("Report", header);
        }
    }
}
