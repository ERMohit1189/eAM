using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

/// <summary>
/// Summary description for MyTemplate
/// </summary>
public class MyTemplate : ITemplate
{
    private string colname;
    public MyTemplate(string colname)
    {
        this.colname = colname;
    }
    public void InstantiateIn(Control container)
    {
        LiteralControl l = new LiteralControl();
        l.DataBinding += new EventHandler(this.OnDataBinding);
        container.Controls.Add(l);
    }
    public void OnDataBinding(object sender, EventArgs e)
    {
        LiteralControl l = (LiteralControl)sender;
        GridViewRow container = (GridViewRow)l.NamingContainer;
        l.Text = ((DataRowView)container.DataItem)[colname].ToString();
    }
}