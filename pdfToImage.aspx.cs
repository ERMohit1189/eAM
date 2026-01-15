using iDiTect.Converter;
using System;
using System.Drawing.Imaging;
using System.IO;
using System.Drawing;
public partial class pdfToImage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Convert();
    }
    public void Convert()
    {
        string str_uploadpath = Server.MapPath("/uploads/pdf/eAM_Manual.pdf");
        PdfToImageConverter converter = new PdfToImageConverter();
        converter.Load(File.ReadAllBytes(str_uploadpath));
        converter.DPI = 96;
        for (int i = 0; i < converter.PageCount; i++)
        {
            Image pageImage = converter.PageToImage(i);
            pageImage.Save(i.ToString() + ".jpg", ImageFormat.Jpeg);
        }

    }
}