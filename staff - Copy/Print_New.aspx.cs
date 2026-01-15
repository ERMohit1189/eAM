using System;

public partial class Print : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        PrintHelper_New.PrintWebControl(string.Empty);
    }

}