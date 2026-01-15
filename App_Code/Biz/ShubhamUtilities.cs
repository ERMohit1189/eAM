using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using AjaxControlToolkit;

/// <summary>
/// Summary description for ShubhamUtilities
/// Created By :Shubham Saxena
/// </summary>
namespace Utilities
{
    
    public class ShubhamUtilities
    {
        public ShubhamUtilities()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public bool MessageBox(System.Web.UI.Page newPage, string AlertMessage)
        {


            ScriptManager.RegisterClientScriptBlock(newPage, GetType(), "AlertMessage", "alert('" + AlertMessage + "');", true);
            return true;
        }
      


        /// <summary>
        /// Developed By:Shubham Saxena
        /// Reset All Textboxes and DropdownList exist in current Form.
        /// </summary>
        /// <param name="parent">
        /// Form
        /// </param>
        /// <returns></returns>
        public int Reset(HtmlForm parent)
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
        /// <summary>
        /// Developed By:Shubham Saxena
        /// Reset All Textboxes and DropdownList exist in current Update Panel.
        /// </summary>
        /// <param name="parent">
        /// UpdatePanel
        /// </param>
        /// <returns></returns>
        public int Reset(UpdatePanel parent)
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
       
    }
}
