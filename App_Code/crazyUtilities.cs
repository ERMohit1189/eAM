using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using AjaxControlToolkit;

/// <summary>
/// Summary description for crazyUtilities
/// </summary>

namespace Utilities
{
    public class crazyUtilities
    {

        public bool RegisterScript(System.Web.UI.Page newPage, string AlertMessage)
        {
            ScriptManager.RegisterClientScriptBlock(newPage,GetType(), "AlertMessage", "alert('" + AlertMessage + "');", true);
            return true;
        }


        public int Clear_Controls(HtmlForm parent)
        {
            int count = 0;
            foreach (Control c in  parent.Controls)
            {
                foreach (Control cc in c.Controls)
                {
                    if (cc.GetType() == typeof(HtmlForm))
                    {
                        foreach (Control x in cc.Controls)
                        {
                            if (x.GetType() == typeof(ContentPlaceHolder))
                            {
                                foreach (Control xx in x.Controls)
                                {
                                    if (xx.GetType() == typeof(TextBox))
                                    {
                                        ((TextBox)xx).Text = string.Empty;
                                        count = count + 1;
                                    }
                                    if (xx.GetType() == typeof(DropDownList))
                                    {
                                        ((DropDownList)xx).SelectedIndex = 0;
                                        count = count + 1;
                                    }
                                }
                            }
                            else
                            {
                                if (x.GetType() == typeof(TextBox))
                                {
                                    ((TextBox)x).Text = string.Empty;
                                    count = count + 1;
                                }
                                if (x.GetType() == typeof(DropDownList))
                                {
                                    ((DropDownList)x).SelectedIndex = 0;
                                    count = count + 1;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (cc.GetType() == typeof(TextBox))
                        {
                            ((TextBox)cc).Text = string.Empty;
                            count = count + 1;
                        }
                        if (cc.GetType() == typeof(DropDownList))
                        {
                            ((DropDownList)cc).SelectedIndex = 0;
                            count = count + 1;
                        }
                    }

                }
            }
            return count;
        }

        public int Clear_Controls(System.Web.UI.Page parent)
        {
            int count = 0;
            foreach (Control c in parent.Controls)
            {
                foreach (Control cc in c.Controls)
                {
                    if (cc.GetType() == typeof(HtmlForm))
                    {
                        foreach (Control x in cc.Controls)
                        {
                            if (x.GetType() == typeof(ContentPlaceHolder))
                            {
                                foreach (Control xx in x.Controls)
                                {
                                    if (xx.GetType() == typeof(TextBox))
                                    {
                                        ((TextBox)xx).Text = string.Empty;
                                        count = count + 1;
                                    }
                                    if (xx.GetType() == typeof(DropDownList))
                                    {
                                        ((DropDownList)xx).SelectedIndex = 0;
                                        count = count + 1;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return count;
        }


        public int setDefault_Controls(System.Web.UI.Page parent)
        {
            int count = 0;
            foreach (Control c in parent.Controls)
            {
                foreach (Control cc in c.Controls)
                {
                    if (cc.GetType() == typeof(HtmlForm))
                    {
                        foreach (Control x in cc.Controls)
                        {
                            if (x.GetType() == typeof(ContentPlaceHolder))
                            {
                                foreach (Control xx in x.Controls)
                                {
                                    if (xx.GetType() == typeof(TextBox))
                                    {
                                       if (((TextBox)xx).Text =="")
                                       {
                                           ((TextBox)xx).Text = "Not Specified";

                                        }
                                        count = count + 1;
                                    }
                                    if (xx.GetType() == typeof(DropDownList))
                                    {
                                        ((DropDownList)xx).SelectedIndex = 0;
                                        count = count + 1;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return count;
        }
    }
}