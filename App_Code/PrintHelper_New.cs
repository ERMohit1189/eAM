using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
using System.Web.SessionState;

/// <summary>
/// Summary description for PrintHelper_New
/// </summary>
public class PrintHelper_New
{
    public static Control ctrl;
    public static string FontSize;
	public PrintHelper_New()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static void PrintWebControl(Control ctrl)
    {
        PrintWebControl(string.Empty);
    }

    public static void PrintWebControl(string Script)
    {

        StringWriter stringWrite = new StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);

        if (ctrl is WebControl)
        {
            Unit w = new Unit(100, UnitType.Percentage);
            ((WebControl)ctrl).Width = w;
        }

        Page pg = new Page();
             
        pg.EnableEventValidation = false;
        if (Script != string.Empty)
        {
            pg.ClientScript.RegisterStartupScript(pg.GetType(), "PrintJavaScript", Script);
        }
        HtmlForm frm = new HtmlForm();
        
        pg.Controls.Add(frm);
        pg.Theme = "blue";
        frm.Attributes.Add("runat", "server");
        frm.Controls.Add(ctrl);
        pg.DesignerInitialize();

        pg.RenderControl(htmlWrite);
        string strHTML = stringWrite.ToString();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.Write(strHTML);

        

        HttpContext.Current.Response.Write("<link href=../css/print/print-theme.min.css rel=stylesheet type=text/css  />");
        HttpContext.Current.Response.Write("<link href=../css/print/print-bootstrap.min.css rel=stylesheet type=text/css   />");
        HttpContext.Current.Response.Write("<link href=../css/font-entypo.css rel=stylesheet type=text/css />");
        HttpContext.Current.Response.Write("<link href=../css/font-awesome.min.css rel=stylesheet type=text/css />");
        HttpContext.Current.Response.Write("<link href=../css/fonts.css rel=stylesheet type=text/css />");
        if (FontSize != string.Empty)
        {
            HttpContext.Current.Response.Write("<script>var fontsize='" + FontSize + "'; var customtext = document.querySelectorAll('.customtext');for (var i = 0; i < customtext.length; i++) {customtext[i].style.fontSize = fontsize;}</script>");
        }
        HttpContext.Current.Response.Write("<script>window.print();</script>");
        HttpContext.Current.Response.End();

    }
}