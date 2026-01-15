using System;
using System.Web.UI;
using System.Web.UI.WebControls;

//public class DynamicTemplate : ITemplate
//{
//    private ListItemType templateType;
//    private string columnName;

//    public DynamicTemplate(ListItemType type, string colname)
//    {
//        templateType = type;
//        columnName = colname;
//    }

//    public void InstantiateIn(Control container)
//    {
//        if (templateType == ListItemType.Item || templateType == ListItemType.AlternatingItem)
//        {
//            Label lbl = new Label();
//            lbl.DataBinding += new EventHandler(this.BindData);
//            container.Controls.Add(lbl);
//        }
//    }

//    private void BindData(object sender, EventArgs e)
//    {
//        Label lbl = (Label)sender;
//        GridViewRow container = (GridViewRow)lbl.NamingContainer;
//        object dataValue = DataBinder.Eval(container.DataItem, columnName);

//        lbl.Text = dataValue != null ? dataValue.ToString() : string.Empty;
//    }
//}

public class DynamicTemplate : ITemplate
{
    private ListItemType templateType;
    private string columnName;

    public DynamicTemplate(ListItemType type, string colname)
    {
        templateType = type;
        columnName = colname;
    }

    public void InstantiateIn(Control container)
    {
        if (templateType == ListItemType.Item || templateType == ListItemType.AlternatingItem)
        {
            Label lbl = new Label
            {
                ID = "lblAttendance" // ✅ Add ID so we can find it later in RowDataBound
            };
            lbl.DataBinding += new EventHandler(this.BindData);
            container.Controls.Add(lbl);
        }
    }

    private void BindData(object sender, EventArgs e)
    {
        Label lbl = (Label)sender;
        GridViewRow container = (GridViewRow)lbl.NamingContainer;
        object dataValue = DataBinder.Eval(container.DataItem, columnName);

        lbl.Text = dataValue != null ? dataValue.ToString() : string.Empty;
    }
}

