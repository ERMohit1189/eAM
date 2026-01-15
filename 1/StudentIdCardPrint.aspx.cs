using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
//using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
//using System.Drawing.Imaging;


namespace _1
{
    public partial class StudentIdCardPrint : Page
    {
        private readonly Campus _oo = new Campus();
        private string _sql = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
            {
                Response.Redirect("default.aspx");
            }
            _oo.dbGet_connection();
            Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file
            //BLL.BLLInstance.LoadHeader("Receipt", header1);
            if (!IsPostBack)
            {
                LoadClass();
                drpSection.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
                DropBranch.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
                DropSrno.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
                _oo.AddDateMonthYearDropDown(DDYear2, DDMonth2, DDDate2);
                _oo.FindCurrentDateandSetinDropDown(DDYear2, DDMonth2, DDDate2);

                _oo.AddDateMonthYearDropDown(DDYearTo2, DDMonthTo2, DDDateTo2);
                _oo.FindCurrentDateandSetinDropDown(DDYearTo2, DDMonthTo2, DDDateTo2);
                string gtDate = "select top 1 case when convert(int, format(DateOfAdmiission,'dd'))<10 then convert(int, format(DateOfAdmiission,'dd')) else format(DateOfAdmiission,'dd') end dd, format(DateOfAdmiission, 'MMM')MMM, format(DateOfAdmiission, 'yyyy')yyyy from StudentOfficialDetails where SessionName = '" + Session["SessionName"] + "' and BranchCode = '" + Session["BranchCode"] + "' order by convert(date, DateOfAdmiission) asc";
                DDDate2.SelectedValue = _oo.ReturnTag(gtDate, "dd").ToString();
                DDMonth2.SelectedValue = _oo.ReturnTag(gtDate, "MMM").ToString();
                DDYear2.SelectedValue = _oo.ReturnTag(gtDate, "yyyy").ToString();
            }
            Panel1.Visible = false;
        }

        private void LoadClass()
        {
            BLL.BLLInstance.loadClass(DrpClass, Session["SessionName"].ToString());
        }
        private void LoadSection()
        {
            _sql = "select id, SectionName from SectionMaster where ClassNameId='" + DrpClass.SelectedValue + "'";
            _sql += "  and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            _oo.FillDropDown_withValue(_sql, drpSection, "SectionName", "Id");
        }
        private void LoadBranch()
        {
            _sql = "Select BranchName,Id from BranchMaster";
            _sql += " where (ClassId='" + DrpClass.SelectedValue + "' or ClassId is NULL) and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";

            _oo.FillDropDown_withValue(_sql, DropBranch, "BranchName", "Id");
            DropBranch.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
        }
        private void LoadSrNo()
        {
            _sql = " select (name+' '+CombineClassName+' '+SrNo)names, SrNo from AllStudentRecord_UDF('" + Session["SessionName"] + "', " + Session["BranchCode"] + ") ";
            _sql += " where ClassId='" + DrpClass.SelectedValue + "' and SectionID='" + drpSection.SelectedValue + "' and BranchId='" + DropBranch.SelectedValue + "'";

            if (drpStatus.SelectedValue != "")
            {
                _sql += "   and isnull(Withdrwal,'') = case when isnull('" + drpStatus.SelectedValue + "','')='B' or isnull('" + drpStatus.SelectedValue + "','')='' then isnull(Withdrwal,'') else case when isnull('" + drpStatus.SelectedValue + "','')='A' then '' else 'W' end end";
                _sql += " and isnull(blocked,'') = case when isnull('" + drpStatus.SelectedValue + "','')= 'W' or isnull('" + drpStatus.SelectedValue + "','')= '' then isnull(blocked,'') else case when isnull('" + drpStatus.SelectedValue + "','')= 'A' then '' else 'yes' end end ";
            }
            _oo.FillDropDown_withValue(_sql, DropSrno, "names", "SrNo");
            DropSrno.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
        }
        protected void DrpClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSection();
            LoadBranch();
        }
        protected void drpStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSrNo();
        }
        protected void drpSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadBranch();
        }
        protected void DropBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSrNo();
        }
        protected void LinkButton1_Click1(object sender, EventArgs e)
        {
            ShowDetails();
        }
        public void ShowDetails()
        {
            Panel1.Visible = true;
            rptICard.DataSource = null;
            rptICard.DataBind();
            rptICardLandscape.DataSource = null;
            rptICardLandscape.DataBind();
            _sql = " select SrNo, Name,BloodGroup, CombineClassName,(Select Isnull(PrincpalSign,'') from CollegeMaster where BranchCode=" + Session["BranchCode"] + ") as PrincpalSign, FatherContactNo, FatherName, Gender,MotherName, StLocalAddress+', '+StLocalCityName as Laddress, PhotoPath, case when isnull(PhotoPath, '')='' then '../img/user-pic/user-pic.jpg' else PhotoPath end PhotoPaths from AllStudentRecord_UDF('" + Session["SessionName"] + "', " + Session["BranchCode"] + ") ";
            _sql += " where ClassId='" + DrpClass.SelectedValue + "' and SectionID= case when '" + drpSection.SelectedValue + "'='<--Select-->' then SectionID else '" + drpSection.SelectedValue + "' end and BranchId=case when '" + DropBranch.SelectedValue + "'='<--Select-->' then BranchId else '" + DropBranch.SelectedValue + "' end and SrNo=case when '" + DropSrno.SelectedValue + "'='<--Select-->' then SrNo else '" + DropSrno.SelectedValue + "' end ";
            if (drpStatus.SelectedValue != "")
            {
                _sql += "   and isnull(Withdrwal,'') = case when isnull('" + drpStatus.SelectedValue + "','')='B' or isnull('" + drpStatus.SelectedValue + "','')='' then isnull(Withdrwal,'') else case when isnull('" + drpStatus.SelectedValue + "','')='A' then '' else 'W' end end";
                _sql += " and isnull(blocked,'') = case when isnull('" + drpStatus.SelectedValue + "','')= 'W' or isnull('" + drpStatus.SelectedValue + "','')= '' then isnull(blocked,'') else case when isnull('" + drpStatus.SelectedValue + "','')= 'A' then '' else 'yes' end end ";
            }
            var fromDate = DDYear2.SelectedItem + " " + DDMonth2.SelectedItem + " " + DDDate2.SelectedItem;
            var toDate = DDYearTo2.SelectedItem + " " + DDMonthTo2.SelectedItem + " " + DDDateTo2.SelectedItem;
            _sql += "and convert(date, isnull(DateOfAdmiission, getdate()))      between convert(Date, '" + fromDate + "') and convert(Date, '" + toDate + "')  ";
            if (RadioButtonList2.SelectedValue == "Name")
            {
                _sql += " order by Name asc";
            }
            if (RadioButtonList2.SelectedValue == "Id")
            {
                _sql += " ORDER BY isnull([Id], 0) asc";
            }
            if (RadioButtonList2.SelectedValue == "InstituteRollNo")
            {
                _sql += " ORDER BY ClassId, isnull(InstituteRollNo, 0) asc";
            }
            if (RadioButtonList2.SelectedValue == "doa")
            {
                _sql += " ORDER BY convert(date, DateOfAdmiission) asc";
            }

            var ds = _oo.GridFill(_sql);
            if (ds != null)
            {
                UpdatePanel11.Visible = true;
                if (ddlLayout.SelectedIndex == 0)
                {
                    abc.Visible = true;
                    Div1.Visible = false;

                    rptICard.DataSource = _oo.GridFill(_sql);
                    rptICard.DataBind();
                    for (int i = 0; i < rptICard.Items.Count; i++)
                    {
                        Image Image1 = (Image)rptICard.Items[i].FindControl("Image1");
                        Image Image2 = (Image)rptICard.Items[i].FindControl("Image2");
                        Label Label16 = (Label)rptICard.Items[i].FindControl("Label16");
                        Label lblsessions = (Label)rptICard.Items[i].FindControl("lblsessions");
                        lblsessions.Text = Session["SessionName"].ToString();

                        _sql = "select top(1) PrincpalSign, ClassTeacherSign from CollegeMaster where BranchCode=" + Session["BranchCode"] + "";
                        if (_oo.ReturnTag(_sql, "PrincpalSign").ToString() == "")
                        {
                            Image2.Visible = false;
                        }
                        else
                        {
                            Image2.Visible = false;
                            Image2.ImageUrl = _oo.ReturnTag(_sql, "PrincpalSign").ToString() + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture);
                        }
                        Label barCodes = (Label)rptICard.Items[i].FindControl("Label16");
                        System.Windows.Forms.PictureBox imageControl = new System.Windows.Forms.PictureBox();
                        Zen.Barcode.Code128BarcodeDraw barcode = Zen.Barcode.BarcodeDrawFactory.Code128WithChecksum;
                        var image = barcode.Draw(barCodes.Text, 50);
                        using (var graphics = System.Drawing.Graphics.FromImage(image))
                        using (var font = new System.Drawing.Font("Consolas", 12)) // Any font you want
                        using (var brush = new System.Drawing.SolidBrush(System.Drawing.Color.White))
                        using (var format = new System.Drawing.StringFormat() { LineAlignment = System.Drawing.StringAlignment.Near }) // To align text above the specified point
                        {
                            // Print a string at the left bottom corner of image
                            graphics.DrawString(barCodes.Text, font, brush, 0, image.Height, format);
                        }

                        imageControl.Image = image;
                        System.IO.MemoryStream ms = new MemoryStream();
                        image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        byte[] byteImage = ms.ToArray();
                        var SigBase64 = Convert.ToBase64String(byteImage);
                        var imageBar = "data:image/png;base64," + SigBase64;
                        Image imgs = (Image)rptICard.Items[i].FindControl("imgs");
                        imgs.ImageUrl = imageBar;


                        using (SqlConnection conn = new SqlConnection())
                        {
                            conn.ConnectionString = BAL.objBal.dbGet_connection().ConnectionString;
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                cmd.Connection = conn;
                                cmd.CommandText = "USP_StudentsPhotoReport";
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@sessionName", Session["SessionName"].ToString().Trim());
                                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString().Trim());
                                cmd.Parameters.AddWithValue("@SrNo", Label16.Text.Trim());
                                cmd.Parameters.AddWithValue("@action", "details");
                                SqlDataAdapter das = new SqlDataAdapter(cmd);
                                DataSet dsPhoto = new DataSet();
                                das.Fill(dsPhoto);
                                cmd.Parameters.Clear();

                                if (dsPhoto.Tables[0].Rows.Count > 0)
                                {
                                    Image1.ImageUrl = dsPhoto.Tables[0].Rows[0]["PhotoPath"].ToString() != string.Empty ? dsPhoto.Tables[0].Rows[0]["PhotoPath"] + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture) : "../img/user-pic/user-pic.jpg" + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture);
                                }
                            }
                        }


                    }
                }
                else
                {
                    abc.Visible = false;
                    Div1.Visible = true;
                    rptICardLandscape.DataSource = _oo.GridFill(_sql);
                    rptICardLandscape.DataBind();

                    for (int i = 0; i < rptICardLandscape.Items.Count; i++)
                    {
                        Image Image1 = (Image)rptICardLandscape.Items[i].FindControl("Image1");
                        Image Image2 = (Image)rptICardLandscape.Items[i].FindControl("Image2");
                        Label Label16 = (Label)rptICardLandscape.Items[i].FindControl("Label16");
                        Label lblsession = (Label)rptICardLandscape.Items[i].FindControl("lblsession");
                        lblsession.Text = Session["SessionName"].ToString();
                        _sql = "select top(1) PrincpalSign, ClassTeacherSign from CollegeMaster where BranchCode=" + Session["BranchCode"] + "";
                        if (_oo.ReturnTag(_sql, "PrincpalSign").ToString() == "")
                        {
                            Image2.Visible = false;
                        }
                        else
                        {
                            Image2.Visible = false;
                            Image2.ImageUrl = _oo.ReturnTag(_sql, "PrincpalSign").ToString() + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture);
                        }

                        Label barCodes = (Label)rptICardLandscape.Items[i].FindControl("Label16");
                        System.Windows.Forms.PictureBox imageControl = new System.Windows.Forms.PictureBox();
                        Zen.Barcode.Code128BarcodeDraw barcode = Zen.Barcode.BarcodeDrawFactory.Code128WithChecksum;
                        var image = barcode.Draw(barCodes.Text, 50);
                        using (var graphics = System.Drawing.Graphics.FromImage(image))
                        using (var font = new System.Drawing.Font("Consolas", 12)) // Any font you want
                        using (var brush = new System.Drawing.SolidBrush(System.Drawing.Color.White))
                        using (var format = new System.Drawing.StringFormat() { LineAlignment = System.Drawing.StringAlignment.Near }) // To align text above the specified point
                        {
                            // Print a string at the left bottom corner of image
                            graphics.DrawString(barCodes.Text, font, brush, 0, image.Height, format);
                        }

                        imageControl.Image = image;
                        System.IO.MemoryStream ms = new MemoryStream();
                        image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        byte[] byteImage = ms.ToArray();
                        var SigBase64 = Convert.ToBase64String(byteImage);
                        var imageBar = "data:image/png;base64," + SigBase64;
                        Image imgs = (Image)rptICardLandscape.Items[i].FindControl("imgs");
                        imgs.ImageUrl = imageBar;

                        using (SqlConnection conn = new SqlConnection())
                        {
                            conn.ConnectionString = BAL.objBal.dbGet_connection().ConnectionString;
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                cmd.Connection = conn;
                                cmd.CommandText = "USP_StudentsPhotoReport";
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@sessionName", Session["SessionName"].ToString().Trim());
                                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString().Trim());
                                cmd.Parameters.AddWithValue("@SrNo", Label16.Text.Trim());
                                cmd.Parameters.AddWithValue("@action", "details");
                                SqlDataAdapter das = new SqlDataAdapter(cmd);
                                DataSet dsPhoto = new DataSet();
                                das.Fill(dsPhoto);
                                cmd.Parameters.Clear();

                                if (dsPhoto.Tables[0].Rows.Count > 0)
                                {
                                    Image1.ImageUrl = dsPhoto.Tables[0].Rows[0]["PhotoPath"].ToString() != string.Empty ? dsPhoto.Tables[0].Rows[0]["PhotoPath"] + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture) : "../img/user-pic/user-pic.jpg" + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture);
                                }
                            }
                        }


                    }
                }
            }
            else
            {
                rptICard.DataSource = null;
                rptICard.DataBind();
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Sorry, No Record(s) found!", "A");
            }
        }

        protected void ImageButton1_Click(object sender, EventArgs e)
        {
            _oo.ExportToWord(Response, "StudentIdCard.doc", gdv);
        }

        protected void ImageButton4_Click(object sender, EventArgs e)
        {
            if (ddlLayout.SelectedIndex == 0)
            {
                PrintHelper_New.ctrl = abc;

            }
            else
            {
                PrintHelper_New.ctrl = Div1;

            }
            ClientScript.RegisterStartupScript(GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
        }

        public void ExportToWord(HttpResponse hh, string fname, HtmlGenericControl dd)
        {
            hh.Clear();
            hh.Buffer = true;
            hh.AddHeader("content-disposition", "attachment;filename=" + fname + ".doc");
            hh.Charset = "";
            hh.Cache.SetCacheability(HttpCacheability.NoCache);
            hh.ContentType = "application/vnd.ms-word";
            var stringWrite = new System.IO.StringWriter();
            var htmlWrite = new HtmlTextWriter(stringWrite);
            dd.RenderControl(htmlWrite);
            hh.Output.Write(stringWrite.ToString());
            hh.Flush();
            hh.End();
        }

        protected void reportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reportType.SelectedIndex == 0)
            {
                Response.Redirect("StudentIdCard.aspx");
            }
            if (reportType.SelectedIndex == 1)
            {
                Response.Redirect("StudentIdCardNarmal.aspx");
            }
        }
        protected void DDYear2_SelectedIndexChanged(object sender, EventArgs e)
        {
            _oo.YearDropDown(DDYear2, DDMonth2, DDDate2);
        }

        protected void DDMonth2_SelectedIndexChanged(object sender, EventArgs e)
        {
            _oo.MonthDropDown(DDYear2, DDMonth2, DDDate2);
        }

        protected void DDDate2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void DDYearTo2_SelectedIndexChanged(object sender, EventArgs e)
        {
            _oo.YearDropDown(DDYear2, DDMonth2, DDDate2);
        }

        protected void DDMonthTo2_SelectedIndexChanged(object sender, EventArgs e)
        {
            _oo.MonthDropDown(DDYear2, DDMonth2, DDDate2);
        }

        protected void DDDateTo2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}