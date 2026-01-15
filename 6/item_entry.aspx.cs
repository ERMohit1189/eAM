using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _6
{
    public partial class ItemEntry : Page
    {
        private SqlConnection _con;
        readonly Campus _oo;
        private string _sql = "";
        public ItemEntry()
        {
            _con = new SqlConnection();
            _oo = new Campus();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
            {
                Response.Redirect("default.aspx");
            }
            _con = _oo.dbGet_connection();
            Campus camp = new Campus(); camp.LoadLoader(loader);
            if (!IsPostBack)
            {
                try
                {
                    CheckValueAddDeleteUpdate();
                }
                catch (Exception){}
                LnkUpdate.Visible = false;
                _oo.AddDateMonthYearDropDown(drpLiYY, drpLiMM, drpLiDD);
                _oo.AddDateMonthYearDropDown(drpPublYY, drpPublMM, drpPublDD);
                _oo.AddDateMonthYearDropDown(drpBillYY, drpBillMM, drpBillDD);
                _oo.FindCurrentDateandSetinDropDown(drpLiYY, drpLiMM, drpLiDD);
                _oo.FindCurrentDateandSetinDropDown(drpPublYY, drpPublMM, drpPublDD);
                _oo.FindCurrentDateandSetinDropDown(drpBillYY, drpBillMM, drpBillDD);

                _sql = "Select CategoryName,Id from ItemCategoryMaster where BranchCode = " + Session["BranchCode"] + "";
                _oo.FillDropDown_withValue_withSelect(_sql, drpCategory, "CategoryName", "Id");

                _sql = "Select SubCategoryName,Id from ItemSubCategoryMaster where BranchCode = " + Session["BranchCode"] + "";
                _oo.FillDropDown_withValue_withSelect(_sql, DrpSubCategory, "CategoryName", "Id");

                _sql = "Select SubjectName,Id from SubjectTopicLibraryMaster where BranchCode = " + Session["BranchCode"] + "";
                _oo.FillDropDown_withValue_withSelect(_sql, drpSubject, "SubjectName", "Id");

                _sql = "Select CategoryName,Id from ItemLanguageMaster where BranchCode = " + Session["BranchCode"] + "";
                _oo.FillDropDown_withValue_withSelect(_sql, drpLanguage, "CategoryName", "Id");

                _sql = "Select PublisherName,Id from PublisherInfoEntry where BranchCode = " + Session["BranchCode"] + "";
                _oo.FillDropDown_withValue_withSelect(_sql, drpPublisher, "PublisherName", "Id");

                _sql = "Select SupplierName,Id from SupplierInfoEntry where BranchCode = " + Session["BranchCode"] + "";
                _oo.FillDropDown_withValue_withSelect(_sql, drpSupplier, "SupplierName", "Id");

                _oo.ReadOnlyControls(Page);
                txtAccessionNo.ReadOnly = false;

                txtAccessionNo.Attributes.Add("onkeypress", "button_click(this,'" + LinkButton2.ClientID + "')");

                txtAccessionNo.Focus();
                txtNoOfItem.Text = "1";
            }
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
           
            var vv = txtAccessionNo.Text;
            ItemAdd(vv.ToString());
        }
        public void ItemAdd(string acno)
        {
            using (var cmd = new SqlCommand())
            {
                if (drpCategory.SelectedItem.ToString() == "<--Select-->" || drpLanguage.SelectedItem.ToString() == "<--Select-->" || drpPublisher.SelectedItem.ToString() == "<--Select-->" || DrpSubCategory.SelectedItem.ToString() == "<--Select-->" || drpSubject.SelectedItem.ToString() == "<--Select-->" || drpSupplier.SelectedItem.ToString() == "<--Select-->")
                {
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Please Select Condition", "A");
                }
                else
                {
                    cmd.CommandText = "LibraryItemEntryProc";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = _con;
                    cmd.Parameters.AddWithValue("@AccessionNo", acno.Trim());
                    cmd.Parameters.AddWithValue("@Title", txtTitle.Text.Trim());
                    var date = drpLiYY.SelectedItem + "/" + drpLiMM.SelectedItem + "/" + drpLiDD.SelectedItem;

                    cmd.Parameters.AddWithValue("@LibraryEntryDate", date.Trim());
                    cmd.Parameters.AddWithValue("@Supplier", drpSupplier.SelectedValue);
                    cmd.Parameters.AddWithValue("@Publisher", drpPublisher.SelectedValue);
                    cmd.Parameters.AddWithValue("@NoOfItem", "1");
                    cmd.Parameters.AddWithValue("@ClassName", txtClass.Text.Trim());
                    cmd.Parameters.AddWithValue("@Language", drpLanguage.SelectedValue);
                    cmd.Parameters.AddWithValue("@BillNo", txtBillNo.Text.Trim());
                    var date1 = drpBillYY.SelectedItem + "/" + drpBillMM.SelectedItem + "/" + drpBillDD.SelectedItem;
                    cmd.Parameters.AddWithValue("@BiilDate", date1);
                    var date2 = drpPublYY.SelectedItem + "/" + drpPublMM.SelectedItem + "/" + drpPublDD.SelectedItem;
                    cmd.Parameters.AddWithValue("@PublicationYear", date2.Trim());

                    cmd.Parameters.AddWithValue("@SubjectTopic", drpSubject.SelectedValue);
                    cmd.Parameters.AddWithValue("@Category", drpCategory.SelectedValue);
                    cmd.Parameters.AddWithValue("@SubCategory", DrpSubCategory.SelectedValue);
                    cmd.Parameters.AddWithValue("@Author1", txtAuthor1.Text.Trim());
                    cmd.Parameters.AddWithValue("@Author2", txtAuthor2.Text.Trim());
                    cmd.Parameters.AddWithValue("@Author3", txtAuthor3.Text.Trim());

                    cmd.Parameters.AddWithValue("@Keyword1", txtkeyword1.Text.Trim());
                    cmd.Parameters.AddWithValue("@Keyword2", txtkeyword2.Text.Trim());
                    cmd.Parameters.AddWithValue("@Keyword3", txtKeyword3.Text.Trim());
                    cmd.Parameters.AddWithValue("@Edition", txtedition.Text.Trim());
                    cmd.Parameters.AddWithValue("@Source", txtSorce.Text.Trim());
                    cmd.Parameters.AddWithValue("@Location", txtLocation.Text.Trim());
                    cmd.Parameters.AddWithValue("@Editor", txteditor.Text.Trim());
                    cmd.Parameters.AddWithValue("@ISBNISSN", txtIsbnIssn.Text.Trim());
                    cmd.Parameters.AddWithValue("@Pages", txtpages.Text.Trim());
                    cmd.Parameters.AddWithValue("@Translator", txttranslator.Text.Trim());
                    cmd.Parameters.AddWithValue("@Size", txtSize.Text.Trim());
                    cmd.Parameters.AddWithValue("@Illustrator", txtIllustrator.Text.Trim());
                    cmd.Parameters.AddWithValue("@Compiler", txtCampiler.Text.Trim());
                    cmd.Parameters.AddWithValue("@Price", txtPrice.Text.Trim());
                    cmd.Parameters.AddWithValue("@SavedBy", txtSavedBy.Text.Trim());
                    
                    
                    var base64Std = hfLibImage.Value;
                    if (base64Std != string.Empty)
                    {
                        var filePath = @"../Uploads/libBookImages/";
                        var fileName = txtTitle.Text.Trim() + ".jpg";

                        using (FileStream fs = new FileStream(Server.MapPath((filePath + fileName)), FileMode.Create))
                        {
                            using (BinaryWriter bw = new BinaryWriter(fs))
                            {
                                var data = Convert.FromBase64String(base64Std);
                                bw.Write(data);
                                bw.Close();
                            }
                        }

                        cmd.Parameters.AddWithValue("@Image", (filePath + fileName));
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Image", "../img/book-pic.jpg");
                    }

                    cmd.Parameters.AddWithValue("@Remark", Txtremark.Text.Trim());
                    cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                    cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                    cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
                    try
                    {
                        _con.Open();
                        var flag = cmd.ExecuteNonQuery();
                        _con.Close();

                        if (flag > 0)
                        {
                            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Submitted Successfully.", "S");
                            DisplayItem();
                            Clear();
                        }
                        else
                        {
                            Campus camp = new Campus(); camp.msgbox(LinkButton1, msgbox, "Sorry,Record not Submitted!", "W");
                        }
                    }
                    catch (SqlException ex2)
                    {
                        Campus camp = new Campus(); camp.msgbox(LnkUpdate, msgbox, ex2.Message, "W");
                        _con.Close();
                    }
                    catch (Exception ex)
                    {
                        Campus camp = new Campus(); camp.msgbox(LnkUpdate, msgbox, ex.Message, "W");
                        _con.Close();
                    }
                }
            }
        }
        protected void drpLiYY_SelectedIndexChanged(object sender, EventArgs e)
        {
            _oo.YearDropDown(drpLiYY, drpLiMM, drpLiDD);

        }
        protected void drpLiMM_SelectedIndexChanged(object sender, EventArgs e)
        {
            _oo.MonthDropDown(drpLiYY, drpLiMM, drpLiDD);
        }
        protected void drpLiDD_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void drpPublYY_SelectedIndexChanged(object sender, EventArgs e)
        {
            _oo.YearDropDown(drpPublYY, drpPublMM, drpPublDD);
        }
        protected void drpPublMM_SelectedIndexChanged(object sender, EventArgs e)
        {
            _oo.MonthDropDown(drpPublYY, drpPublMM, drpPublDD);
        }
        protected void drpPublDD_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void drpBillYY_SelectedIndexChanged(object sender, EventArgs e)
        {
            _oo.YearDropDown(drpBillYY, drpBillMM, drpBillDD);
        }
        protected void drpBillMM_SelectedIndexChanged(object sender, EventArgs e)
        {
            _oo.YearDropDown(drpBillYY, drpBillMM, drpBillDD);
        }
        public void DisplayItem()
        {
            _sql = "Select Id,AccessionNo  ,GroupId  ,Title  ,Supplier  ,Publisher  ,NoOfItem  ,Language  ,";
            _sql = _sql + "  left(convert(nvarchar,LibraryEntryDate,106),2) as DD,Right(left(convert(nvarchar,LibraryEntryDate,106),6),3) as MM , RIGHT(convert(nvarchar,LibraryEntryDate,106),4) as YY,";
            _sql = _sql + "  left(convert(nvarchar,BiilDate,106),2) as DD1,Right(left(convert(nvarchar,BiilDate,106),6),3) as MM1 , RIGHT(convert(nvarchar,BiilDate,106),4) as YY1,";
            _sql = _sql + "  left(convert(nvarchar,PublicationYear,106),2) as DD2,Right(left(convert(nvarchar,PublicationYear,106),6),3) as MM2 , RIGHT(convert(nvarchar,PublicationYear,106),4) as YY2,";


            _sql = _sql + "    BillNo  ,SubjectTopic  ,   Category  ,SubCategory  ,Author1  ,Author2  ,Author3  ,";
            _sql = _sql + "  Keyword1  ,Keyword2  ,Keyword3  ,Edition  ,Source  ,Location  ,Editor  ,ISBNISSN  ,Pages  ,Translator  ,Size  ,";
            _sql = _sql + "     Illustrator  ,   Compiler  ,Price  ,SavedBy  ,Image  ,Remark  ,LoginName  ,SessionName  ,BranchCode,RecordDate,ClassName from LibraryItemEntry where AccessionNo='" + txtAccessionNo.Text + "' and BranchCode = " + Session["BranchCode"] + "";
            txtAccessionNo.Text = _oo.ReturnTag(_sql, "AccessionNo");
            txtAuthor1.Text = _oo.ReturnTag(_sql, "Author1");
            txtAuthor2.Text = _oo.ReturnTag(_sql, "Author2");
            txtAuthor3.Text = _oo.ReturnTag(_sql, "Author3");
            txtBillNo.Text = _oo.ReturnTag(_sql, "BillNo");
            txtCampiler.Text = _oo.ReturnTag(_sql, "Compiler");
            txtedition.Text = _oo.ReturnTag(_sql, "Edition");
            txteditor.Text = _oo.ReturnTag(_sql, "Editor");
            txtIllustrator.Text = _oo.ReturnTag(_sql, "Illustrator");
            txtIsbnIssn.Text = _oo.ReturnTag(_sql, "ISBNISSN");
            txtkeyword1.Text = _oo.ReturnTag(_sql, "Keyword1");
            txtkeyword2.Text = _oo.ReturnTag(_sql, "Keyword2");
            txtKeyword3.Text = _oo.ReturnTag(_sql, "Keyword3");
            txtLocation.Text = _oo.ReturnTag(_sql, "Location");
            txtNoOfItem.Text = "1";
            txtpages.Text = _oo.ReturnTag(_sql, "Pages");
            txtPrice.Text = _oo.ReturnTag(_sql, "Price");
            Txtremark.Text = _oo.ReturnTag(_sql, "Remark");
            txtSavedBy.Text = _oo.ReturnTag(_sql, "SavedBy");
            txtSize.Text = _oo.ReturnTag(_sql, "Size");
            txtSorce.Text = _oo.ReturnTag(_sql, "Source");
            txtTitle.Text = _oo.ReturnTag(_sql, "Title");
            string sessionname = _oo.ReturnTag(_sql, "SessionName");
            string sql1 = _sql;
            
            _sql = sql1;
            try
            {
                txtClass.Text = _oo.ReturnTag(_sql, "ClassName");
            }
            catch
            {
                // ignored
            }
            try
            {
                drpLanguage.Text = _oo.ReturnTag(_sql, "Language");
            }
            catch (Exception)
            {
                // ignored
            }
            try
            {
                drpCategory.Text = _oo.ReturnTag(_sql, "Category");
            }
            catch (Exception)
            {
                // ignored
            }
            try
            {
                drpPublisher.Text = _oo.ReturnTag(_sql, "Publisher");
            }
            catch (Exception)
            {
                // ignored
            }
            try
            {
                drpSupplier.Text = _oo.ReturnTag(_sql, "Supplier");
            }
            catch (Exception)
            {
                // ignored
            }
            try
            {
                DrpSubCategory.Text = _oo.ReturnTag(_sql, "SubCategory");
            }
            catch (Exception)
            {
                // ignored
            }
            try
            {
                drpSubject.Text = _oo.ReturnTag(_sql, "SubjectTopic");
            }
            catch (Exception)
            {
                // ignored
            }
            try
            {
                drpBillDD.Text = _oo.ReturnTag(_sql, "DD1");
            }
            catch (Exception)
            {
                // ignored
            }
            try
            {
                drpBillMM.Text = _oo.ReturnTag(_sql, "MM1");
            }
            catch (Exception)
            {
                // ignored
            }
            try
            {
                drpBillYY.Text = _oo.ReturnTag(_sql, "YY1");
            }
            catch (Exception)
            {
                // ignored
            }
            try
            {
                drpPublDD.Text = _oo.ReturnTag(_sql, "DD2");
            }
            catch (Exception)
            {
                // ignored
            }
            try
            {
                drpPublMM.Text = _oo.ReturnTag(_sql, "MM2");
            }
            catch (Exception)
            {
                // ignored
            }
            try
            {

                drpPublYY.Text = _oo.ReturnTag(_sql, "YY2");
            }
            catch (Exception)
            {
                // ignored
            }
            try
            {
                drpLiDD.Text = _oo.ReturnTag(_sql, "DD");
            }
            catch (Exception)
            {
                // ignored
            }
            try
            {

                drpLiMM.Text = _oo.ReturnTag(_sql, "MM");
            }
            catch (Exception)
            {
                // ignored
            }
            try
            {
                drpLiYY.Text = _oo.ReturnTag(_sql, "YY");
            }
            catch (Exception)
            {
                // ignored
            }
            txttranslator.Text = _oo.ReturnTag(_sql, "Translator");

            Image1.ImageUrl = _oo.ReturnTag(_sql, "Image");
        }
        protected void DrpSubCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            //sql = "Select SubCategoryName from ItemSubCategoryMaster where CategoryName ='" + drpCategory.SelectedItem.ToString() + "'";
            //oo.FillDropDown(sql, DrpSubCategory, "SubCategoryName");



        }
        protected void drpCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            _sql = "Select SubCategoryName,Id from ItemSubCategoryMaster where CategoryName ='" + drpCategory.SelectedValue + "' and BranchCode = " + Session["BranchCode"] + "";
            _oo.FillDropDown_withValue_withSelect(_sql, DrpSubCategory, "SubCategoryName", "Id");

            if (drpCategory.SelectedItem.Text.ToUpper() == "BOOKS" || drpCategory.SelectedItem.Text.ToUpper() == "BOOK")
            {
                Label1.Text = "ISBN";
            }
            else
            {
                Label1.Text = "ISSN";
            }

            drpCategory.Focus();
        }
        //public string GroupIdFind()
        //{
        //    _sql = "select GroupId  from LibraryItemEntry where AccessionNo='" + txtAccessionNo.Text + "' and BranchCode = " + Session["BranchCode"] + "";
        //    var gid = _oo.ReturnTag(_sql, "GroupId");
        //    return gid;
        //}
        protected void LnkUpdate_Click(object sender, EventArgs e)
        {
            using (var cmd = new SqlCommand())
            {
                cmd.CommandText = "LibraryItemEntryUpdateProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = _con;
                cmd.Parameters.AddWithValue("@ClassName", txtClass.Text.Trim());
                cmd.Parameters.AddWithValue("@AccessionNo", txtAccessionNo.Text.Trim());
                //var ggId = GroupIdFind();
                //cmd.Parameters.AddWithValue("@GroupId", ggId);
                cmd.Parameters.AddWithValue("@Title", txtTitle.Text);
                var date = drpLiYY.SelectedItem + "/" + drpLiMM.SelectedItem + "/" + drpLiDD.SelectedItem;
                cmd.Parameters.AddWithValue("@LibraryEntryDate", date);
                cmd.Parameters.AddWithValue("@Supplier", drpSupplier.SelectedValue);
                cmd.Parameters.AddWithValue("@Publisher", drpPublisher.SelectedValue);
                cmd.Parameters.AddWithValue("@NoOfItem", "1");
                cmd.Parameters.AddWithValue("@Language", drpLanguage.SelectedValue);
                cmd.Parameters.AddWithValue("@BillNo", txtBillNo.Text);
                var date1 = "";
                date1 = drpBillYY.SelectedItem + "/" + drpBillMM.SelectedItem + "/" + drpBillDD.SelectedItem;
                cmd.Parameters.AddWithValue("@BiilDate", date1);
                var date2 = "";
                date2 = drpPublYY.SelectedItem + "/" + drpPublMM.SelectedItem + "/" + drpPublDD.SelectedItem;
                cmd.Parameters.AddWithValue("@PublicationYear", date2.Trim());
                cmd.Parameters.AddWithValue("@SubjectTopic", drpSubject.SelectedValue);
                cmd.Parameters.AddWithValue("@Category", drpCategory.SelectedValue);
                cmd.Parameters.AddWithValue("@SubCategory", DrpSubCategory.SelectedValue);
                cmd.Parameters.AddWithValue("@Author1", txtAuthor1.Text.Trim());
                cmd.Parameters.AddWithValue("@Author2", txtAuthor2.Text.Trim());
                cmd.Parameters.AddWithValue("@Author3", txtAuthor3.Text.Trim());
                cmd.Parameters.AddWithValue("@Keyword1", txtkeyword1.Text.Trim());
                cmd.Parameters.AddWithValue("@Keyword2", txtkeyword2.Text.Trim());
                cmd.Parameters.AddWithValue("@Keyword3", txtKeyword3.Text.Trim());
                cmd.Parameters.AddWithValue("@Edition", txtedition.Text.Trim());
                cmd.Parameters.AddWithValue("@Source", txtSorce.Text.Trim());
                cmd.Parameters.AddWithValue("@Location", txtLocation.Text.Trim());
                cmd.Parameters.AddWithValue("@Editor", txteditor.Text.Trim());
                cmd.Parameters.AddWithValue("@ISBNISSN", txtIsbnIssn.Text.Trim());
                cmd.Parameters.AddWithValue("@Pages", txtpages.Text.Trim());
                cmd.Parameters.AddWithValue("@Translator", txttranslator.Text.Trim());
                cmd.Parameters.AddWithValue("@Size", txtSize.Text.Trim());
                cmd.Parameters.AddWithValue("@Illustrator", txtIllustrator.Text.Trim());
                cmd.Parameters.AddWithValue("@Compiler", txtCampiler.Text.Trim());
                cmd.Parameters.AddWithValue("@Price", txtPrice.Text.Trim());
                cmd.Parameters.AddWithValue("@SavedBy", txtSavedBy.Text.Trim());
                var base64Std = hfLibImage.Value;
                if (base64Std != string.Empty)
                {
                    var filePath = @"../Uploads/libBookImages/";
                    var fileName = txtTitle.Text.Trim() + ".jpg";

                    using (FileStream fs = new FileStream(Server.MapPath((filePath + fileName)), FileMode.Create))
                    {
                        using (BinaryWriter bw = new BinaryWriter(fs))
                        {
                            var data = Convert.FromBase64String(base64Std);
                            bw.Write(data);
                            bw.Close();
                        }
                    }

                    cmd.Parameters.AddWithValue("@Image", (filePath + fileName));
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Image", "../img/book-pic.jpg");
                }

                cmd.Parameters.AddWithValue("@Remark", Txtremark.Text.Trim());
                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);

                cmd.Parameters.AddWithValue("@updateTypeNa", value: CheckBox1.Checked == true ? "A" : "S");
                try
                {
                    var flag = 0;
                    _con.Open();
                    flag = cmd.ExecuteNonQuery();
                    _con.Close();

                    if (flag > 0)
                    {
                        Campus camp = new Campus(); camp.msgbox(LnkUpdate, msgbox, "Updated successfully.", "S");
                        Clear();
                    }
                }
                catch (SqlException ex2)
                {
                    _con.Close();
                }
                catch (Exception ex) {
                    _con.Close();
                }
            }
        }
        protected void lnkCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }
        public void Clear()
        {
            LinkButton1.Visible = true;
            LnkUpdate.Visible = false;
            _oo.ClearControls(Page);
            txtNoOfItem.Text = "1";
            _oo.UnReadOnlyControls(Page);
            _sql = "Select CategoryName,Id from ItemCategoryMaster where BranchCode = " + Session["BranchCode"] + "";
            _oo.FillDropDown_withValue_withSelect(_sql, drpCategory, "CategoryName", "Id");


            _sql = "Select SubCategoryName,Id from ItemSubCategoryMaster where BranchCode = " + Session["BranchCode"] + "";
            _oo.FillDropDown_withValue_withSelect(_sql, DrpSubCategory, "CategoryName", "Id");

            _sql = "Select SubjectName,Id from SubjectTopicLibraryMaster where BranchCode = " + Session["BranchCode"] + "";
            _oo.FillDropDown_withValue_withSelect(_sql, drpSubject, "SubjectName", "Id");

            _sql = "Select CategoryName,Id from ItemLanguageMaster where BranchCode = " + Session["BranchCode"] + "";
            _oo.FillDropDown_withValue_withSelect(_sql, drpLanguage, "CategoryName", "Id");


            _sql = "Select PublisherName,Id from PublisherInfoEntry where BranchCode = " + Session["BranchCode"] + "";
            _oo.FillDropDown_withValue_withSelect(_sql, drpPublisher, "PublisherName", "Id");



            _sql = "Select SupplierName,Id from SupplierInfoEntry where BranchCode = " + Session["BranchCode"] + "";
            _oo.FillDropDown_withValue_withSelect(_sql, drpSupplier, "SupplierName", "Id");


            Image1.ImageUrl = null;

        }
        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            DisplayItem();
            LinkButton1.Visible = false;
            LnkUpdate.Visible = true;
            _oo.UnReadOnlyControls(Page);
            txtAccessionNo.ReadOnly = true;
            txtTitle.Focus();
        }
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            _sql = "Select AccessionNo from LibraryItemEntry where AccessionNo='" + txtAccessionNo.Text + "' and BranchCode = " + Session["BranchCode"] + "  and isnull(DeleteBookYesno, '')<> 'Yes' ";
            if (_oo.Duplicate(_sql))
            {
                Panel2_ModalPopupExtender.Show();
                btnDelete.Focus();
            }
            else
            {
                _sql = "Select AccessionNo from LibraryItemEntry where AccessionNo='" + txtAccessionNo.Text + "' and BranchCode = " + Session["BranchCode"] + "  and isnull(DeleteBookYesno, '')= 'Yes' ";
                if (_oo.Duplicate(_sql))
                {
                    Panel3_ModalPopupExtender.Show();
                    LinkButton5.Focus();
                }
                else
                {
                    
                    LinkButton1.Visible = true;
                    LnkUpdate.Visible = false;
                    _oo.UnReadOnlyControls(Page);
                    txtTitle.Focus();
                }
            }
        }
        protected void Button8_Click(object sender, EventArgs e)
        {
            LinkButton1.Visible = true;
            LnkUpdate.Visible = false;
            _oo.ClearControls(Page);
            _oo.UnReadOnlyControls(Page);
        }
        protected void Button9_Click(object sender, EventArgs e)
        {
            LinkButton1.Visible = true;
            LnkUpdate.Visible = false;
            _oo.ClearControls(Page);
            txtNoOfItem.Text = "1";
            _oo.UnReadOnlyControls(Page);
            _sql = "Select CategoryName,id from ItemCategoryMaster Where BranchCode = " + Session["BranchCode"] + "";
            _oo.FillDropDown_withValue_withSelect(_sql, drpCategory, "CategoryName", "id");

            _sql = "Select SubCategoryName,id from ItemSubCategoryMaster Where BranchCode = " + Session["BranchCode"] + "";
            _oo.FillDropDown_withValue_withSelect(_sql, DrpSubCategory, "CategoryName", "id");

            _sql = "Select SubjectName,id from SubjectTopicLibraryMaster Where BranchCode = " + Session["BranchCode"] + "";
            _oo.FillDropDown_withValue_withSelect(_sql, drpSubject, "SubjectName", "id");

            _sql = "Select CategoryName,id from ItemLanguageMaster Where BranchCode = " + Session["BranchCode"] + "";
            _oo.FillDropDown_withValue_withSelect(_sql, drpLanguage, "CategoryName", "id");

            _sql = "Select PublisherName,id from PublisherInfoEntry Where BranchCode = " + Session["BranchCode"] + "";
            _oo.FillDropDown_withValue_withSelect(_sql, drpPublisher, "PublisherName", "id");

            _sql = "Select SupplierName,id from SupplierInfoEntry Where BranchCode = " + Session["BranchCode"] + "";
            _oo.FillDropDown_withValue_withSelect(_sql, drpSupplier, "SupplierName", "id");
            Image1.ImageUrl = null;
        }
        public void PermissionGrant(int add1, int delete1, int update1, LinkButton ladd, LinkButton ldelete, LinkButton lUpdate)
        {
            ladd.Enabled = add1 == 1;
            ldelete.Enabled = delete1 == 1;
            lUpdate.Enabled = update1 == 1;
        }
        public void CheckValueAddDeleteUpdate()
        {
            _sql = " select LoginId,LoginName,Pass,SessionId,BranchId,LT.LoginTypeName,ltb.add1 as add1,ltb.delete1 as delete1,ltb.update1 as update1 from LoginTab LTb";
            _sql = _sql + " left join LoginType Lt on LTb.LoginTypeId=Lt.LoginTypeId where LT.LoginTypeName='" + Session["Logintype"] + "' and LTb.LoginName='" + Session["LoginName"] + "' and LTb.BranchId = " + Session["BranchCode"] + "";
            var a = Convert.ToInt32(_oo.ReturnTag(_sql, "add1"));
            var u = Convert.ToInt32(_oo.ReturnTag(_sql, "update1"));
            var d = Convert.ToInt32(_oo.ReturnTag(_sql, "delete1"));

            // ReSharper disable once RedundantCast
            PermissionGrant(a, d, u, (LinkButton)LinkButton1, btnDelete, LnkUpdate);
        }
        public override void Dispose()
        {
            _con.Dispose();
            _oo.Dispose();
        }
        protected void txtAccessionNo_TextChanged(object sender, EventArgs e)
        {
            _sql = "Select AccessionNo from LibraryItemEntry where AccessionNo='" + txtAccessionNo.Text + "' and BranchCode = " + Session["BranchCode"] + "  and isnull(DeleteBookYesno, '')<> 'Yes' ";
            if (_oo.Duplicate(_sql))
            {
                Panel2_ModalPopupExtender.Show();
                btnDelete.Focus();
            }
            else
            {
                _sql = "Select AccessionNo from LibraryItemEntry where AccessionNo='" + txtAccessionNo.Text + "' and BranchCode = " + Session["BranchCode"] + "  and isnull(DeleteBookYesno, '')= 'Yes' ";
                if (_oo.Duplicate(_sql))
                {
                    Panel3_ModalPopupExtender.Show();
                    LinkButton5.Focus();
                }
                else
                {
                    
                    LinkButton1.Visible = true;
                    LnkUpdate.Visible = false;
                    _oo.UnReadOnlyControls(Page);
                    txtTitle.Focus();
                }
            }
        }
        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            LinkButton1.Visible = true;
            LnkUpdate.Visible = false;
            _oo.ClearControls(Page);
            txtNoOfItem.Text = "1";
            _oo.UnReadOnlyControls(Page);
            _sql = "Select CategoryName,id from ItemCategoryMaster Where BranchCode = " + Session["BranchCode"] + "";
            _oo.FillDropDown_withValue_withSelect(_sql, drpCategory, "CategoryName", "id");

            _sql = "Select SubCategoryName,id from ItemSubCategoryMaster Where BranchCode = " + Session["BranchCode"] + "";
            _oo.FillDropDown_withValue_withSelect(_sql, DrpSubCategory, "CategoryName", "id");

            _sql = "Select SubjectName,id from SubjectTopicLibraryMaster Where BranchCode = " + Session["BranchCode"] + "";
            _oo.FillDropDown_withValue_withSelect(_sql, drpSubject, "SubjectName", "id");

            _sql = "Select CategoryName,id from ItemLanguageMaster Where BranchCode = " + Session["BranchCode"] + "";
            _oo.FillDropDown_withValue_withSelect(_sql, drpLanguage, "CategoryName", "id");

            _sql = "Select PublisherName,id from PublisherInfoEntry Where BranchCode = " + Session["BranchCode"] + "";
            _oo.FillDropDown_withValue_withSelect(_sql, drpPublisher, "PublisherName", "id");

            _sql = "Select SupplierName,id from SupplierInfoEntry Where BranchCode = " + Session["BranchCode"] + "";
            _oo.FillDropDown_withValue_withSelect(_sql, drpSupplier, "SupplierName", "id");
            Image1.ImageUrl = null;
        }
    }
}
