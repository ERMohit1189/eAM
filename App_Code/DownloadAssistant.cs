using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections;

/// <summary>
/// A class that helps to build download buttons/links on the fly
/// </summary>
static public class DownloadAssistant
{
    static public void DownloadFile(string filePath)
    {
        try
        {
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.IO.Path.GetFileName(filePath));
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.WriteFile(filePath);
            HttpContext.Current.Response.End();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //overloaded method as the Event handler for Button and LinkButton
    static private void DownloadFile(object sender, CommandEventArgs e)
    {
        DownloadFile(e.CommandArgument.ToString());
    }
    //create a download link using a linkbutton
    static public void WireUpDownloadLink(LinkButton linkButton, string linkText, string fileFullPath)
    {
        try
        {
            //set properties
            linkButton.Text = linkText;
            linkButton.CommandArgument = fileFullPath;
            //wire event
            linkButton.Command += new CommandEventHandler(DownloadFile);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    //create a download using a button
    static public void WireUpDownloadButton(Button button, string buttonText, string fileFullPath)
    {
        try
        {
            //set properties
            button.Text = buttonText;
            button.CommandArgument = fileFullPath;
            //wire event
            button.Command += new CommandEventHandler(DownloadFile);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    //Create a group of LinkButtons and place them in a panel control for downloads
    static public void CreateFileDownloadLinks(Panel pnlDownloadContainer, ArrayList linkText_FilePathPairs, string linkSeparator)
    {
        try
        {
            //loop through the linkTextFilePathPairs to create LinkButtons and add them to panel
            int i=0, Count=linkText_FilePathPairs.Count;
            foreach (ListItem itm in linkText_FilePathPairs)
            {
                //declare a LinkButton
                LinkButton lnk = new LinkButton();
                //set properties
                lnk.Text = itm.Text;
                lnk.CommandArgument = itm.Value;//file path
                //wire event
                lnk.Command += new CommandEventHandler(DownloadFile);
                //add the created control to container
                pnlDownloadContainer.Controls.Add(lnk);
                if (Count > 1 && i<Count-1)//add a separator at the end
                {
                    pnlDownloadContainer.Controls.Add(new System.Web.UI.LiteralControl(linkSeparator));
                }
                ++i;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    //create a group of Buttons and place them in a panel control for downloads
    static public void CreateFileDownloadButtons(Panel pnlDownloadContainer, ArrayList buttonText_FilePathPairs, string buttonSeparator)
    {
        try
        {
            //loop through the buttonText_FilePathPairs to create LinkButtons and add them to panel
            int i = 0, Count = buttonText_FilePathPairs.Count;
            foreach (ListItem itm in buttonText_FilePathPairs)
            {
                //declare a button
                Button btn = new Button();
                //set properties
                btn.Text = itm.Text;
                btn.CommandArgument = itm.Value;//file path
                //wire event
                btn.Command += new CommandEventHandler(DownloadFile);
                //add the created control to container
                pnlDownloadContainer.Controls.Add(btn);
                if (Count > 1 && i < Count - 1)//add a separator at the end
                {
                    pnlDownloadContainer.Controls.Add(new System.Web.UI.LiteralControl(buttonSeparator));
                }
                ++i;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
