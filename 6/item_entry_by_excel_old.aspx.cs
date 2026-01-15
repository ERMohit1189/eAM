using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;

public partial class admin_libraryItemEntryByExcel : System.Web.UI.Page
{
    string sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Campus camp = new Campus(); camp.LoadLoader(loader);
        if (!IsPostBack)
        {
        }
    }

    public void loadItemCategory(DropDownList drpCategory, string categoryName)
    {
        sql = "Select CategoryName,Id from ItemCategoryMaster Where BranchCode = " + Session["BranchCode"] + "";
        BAL.objBal.FillDropDown_withValue_withSelect(sql, drpCategory, "CategoryName", "Id");

        try
        {
            drpCategory.SelectedValue = drpCategory.Items.Cast<ListItem>().FirstOrDefault(i => i.Text.Equals(categoryName, StringComparison.InvariantCultureIgnoreCase)).Value;
        }
        catch
        {
            drpCategory.SelectedValue = drpCategory.Items.Cast<ListItem>().FirstOrDefault(i => i.Text.Equals("other", StringComparison.InvariantCultureIgnoreCase)).Value;
        }
    }

    public void loadItemSubCategory(DropDownList DrpSubCategory, string subCategoryName)
    {
        sql = "Select SubCategoryName,Id from ItemSubCategoryMaster Where BranchCode = " + Session["BranchCode"] + "";
        BAL.objBal.FillDropDown_withValue_withSelect(sql, DrpSubCategory, "CategoryName", "Id");

        try
        {
            DrpSubCategory.SelectedValue = DrpSubCategory.Items.Cast<ListItem>().FirstOrDefault(i => i.Text.Equals(subCategoryName, StringComparison.InvariantCultureIgnoreCase)).Value;
        }
        catch
        {
            DrpSubCategory.SelectedValue = DrpSubCategory.Items.Cast<ListItem>().FirstOrDefault(i => i.Text.Equals("other", StringComparison.InvariantCultureIgnoreCase)).Value;
        }
    }

    public void loadSubjectTopicLibrary(DropDownList drpSubject, string subjectName)
    {
        sql = "Select SubjectName,Id from SubjectTopicLibraryMaster Where BranchCode = " + Session["BranchCode"] + "";
        BAL.objBal.FillDropDown_withValue_withSelect(sql, drpSubject, "SubjectName", "Id");

        try
        {
            drpSubject.SelectedValue = drpSubject.Items.Cast<ListItem>().FirstOrDefault(i => i.Text.Equals(subjectName, StringComparison.InvariantCultureIgnoreCase)).Value;
        }
        catch
        {
            drpSubject.SelectedValue = drpSubject.Items.Cast<ListItem>().FirstOrDefault(i => i.Text.Equals("other", StringComparison.InvariantCultureIgnoreCase)).Value;
        }
    }

    public void loadItemLanguage(DropDownList drpLanguage, string languageName)
    {
        sql = "Select CategoryName,Id from ItemLanguageMaster Where BranchCode = " + Session["BranchCode"] + "";
        BAL.objBal.FillDropDown_withValue_withSelect(sql, drpLanguage, "CategoryName", "Id");

        try
        {
            drpLanguage.SelectedValue = drpLanguage.Items.Cast<ListItem>().FirstOrDefault(i => i.Text.Equals(languageName, StringComparison.InvariantCultureIgnoreCase)).Value;
        }
        catch
        {
            drpLanguage.SelectedValue = drpLanguage.Items.Cast<ListItem>().FirstOrDefault(i => i.Text.Equals("other", StringComparison.InvariantCultureIgnoreCase)).Value;
        }
    }

    public void loadPublisher(DropDownList drpPublisher, string publisherName)
    {
        sql = "Select PublisherName,Id from PublisherInfoEntry Where BranchCode = " + Session["BranchCode"] + "";
        BAL.objBal.FillDropDown_withValue_withSelect(sql, drpPublisher, "PublisherName", "Id");

        try
        {
            drpPublisher.SelectedValue = drpPublisher.Items.Cast<ListItem>().FirstOrDefault(i => i.Text.Equals(publisherName, StringComparison.InvariantCultureIgnoreCase)).Value;
        }
        catch
        {
            drpPublisher.SelectedValue = drpPublisher.Items.Cast<ListItem>().FirstOrDefault(i => i.Text.Equals("other", StringComparison.InvariantCultureIgnoreCase)).Value;
        }
    }

    public void loadSupplier(DropDownList drpSupplier, string supplierName)
    {
        sql = "Select SupplierName,Id from SupplierInfoEntry Where BranchCode = " + Session["BranchCode"] + "";
        BAL.objBal.FillDropDown_withValue_withSelect(sql, drpSupplier, "SupplierName", "Id");

        try
        {
            drpSupplier.SelectedValue = drpSupplier.Items.Cast<ListItem>().FirstOrDefault(i => i.Text.Equals(supplierName, StringComparison.InvariantCultureIgnoreCase)).Value;
        }
        catch
        {
            drpSupplier.SelectedValue = drpSupplier.Items.Cast<ListItem>().FirstOrDefault(i => i.Text.Equals("other", StringComparison.InvariantCultureIgnoreCase)).Value;
        }
    }

    protected void lnkShow_Click(object sender, EventArgs e)
    {
        showExcel();
    }

    private void showExcel()
    {
        try
        {
            string base64std = hfFile.Value;
            string fileExtention = hdfilefileExtention.Value;

            string filePath = "";
            string fileName = "";

            if (base64std != string.Empty)
            {
                filePath = @"../uploads/UploadExcel/";

                DateTime date = DateTime.Now;

                string time = date.ToString("HH_mm_ss");

                fileName = String.Format("libitemlist" + "{0}" + fileExtention, time);

                using (FileStream fs = new FileStream(Server.MapPath((filePath + fileName)), FileMode.Create))
                {
                    using (BinaryWriter bw = new BinaryWriter(fs))
                    {
                        byte[] data = Convert.FromBase64String(base64std);
                        bw.Write(data);
                        bw.Close();
                    }
                }
            }

            if ((filePath + fileName) != string.Empty)
            {
                BLL.BLLInstance.ReadExcel(Server.MapPath((filePath + fileName)), fileExtention, lnkShow, "Item_Entry$");

                File.Delete(Server.MapPath((filePath + fileName)));

                //for (int i = 0; i < dt2.Rows.Count; i++)
                //{
                //    string isDuplicate = "";
                //    List<SqlParameter> param = new List<SqlParameter>();

                //    string accno = dt2.Rows[i]["F2"].ToString();
                //    param.Add(new SqlParameter("@AccessionNo", accno));

                //    SqlParameter para = new SqlParameter("@Msg", "");
                //    para.Direction = ParameterDirection.Output;
                //    param.Add(para);

                //    isDuplicate = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("USP_checkDuplicateAccessionNo", param);

                //    if (isDuplicate == "D")
                //    {
                //        //dt2.Rows.RemoveAt(i);
                //        i--;
                //    }
                //}

                gvUploadLibItem.DataSource = null;// dt2;
                gvUploadLibItem.DataBind();

                if (gvUploadLibItem.Rows.Count == 0)
                {
                    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "All AccessionNo. are already exists!", "A");
                }
            }
        }
        catch(Exception ex)
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, ex.Message, "W");
        }
       
    }

    protected void gvUploadLibItem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //string isDuplicate = "";
            //List<SqlParameter> param = new List<SqlParameter>();

            //Label lblAccessionNo = (Label)e.Row.FindControl("lblAccessionNo");

            //param.Add(new SqlParameter("@AccessionNo", lblAccessionNo.Text.Trim()));

            //SqlParameter para = new SqlParameter("@Msg", "");
            //para.Direction = ParameterDirection.Output;
            //param.Add(para);

            //isDuplicate = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("USP_checkDuplicateAccessionNo", param);

            //if (isDuplicate == "D")
            //{
            //    e.Row.Visible = false;
            //}

            //if (e.Row.Visible == true)
            //{
                DropDownList drpSupplier = (DropDownList)e.Row.FindControl("drpSupplier");
                HiddenField hfSupplier = (HiddenField)e.Row.FindControl("hfSupplier");

                DropDownList drpPublisher = (DropDownList)e.Row.FindControl("drpPublisher");
                HiddenField hfPublisher = (HiddenField)e.Row.FindControl("hfPublisher");

                DropDownList drpLanguage = (DropDownList)e.Row.FindControl("drpLanguage");
                HiddenField hfLanguage = (HiddenField)e.Row.FindControl("hfLanguage");


                DropDownList drpSubjectTopic = (DropDownList)e.Row.FindControl("drpSubjectTopic");
                HiddenField hfSubjectTopic = (HiddenField)e.Row.FindControl("hfSubjectTopic");


                DropDownList drpCategory = (DropDownList)e.Row.FindControl("drpCategory");
                HiddenField hfCategory = (HiddenField)e.Row.FindControl("hfCategory");


                DropDownList drpSubCategory = (DropDownList)e.Row.FindControl("drpSubCategory");
                HiddenField hfSubCategory = (HiddenField)e.Row.FindControl("hfSubCategory");


                loadSupplier(drpSupplier, hfSupplier.Value);
                loadPublisher(drpPublisher, hfPublisher.Value);
                loadItemLanguage(drpLanguage, hfLanguage.Value);
                loadSubjectTopicLibrary(drpSubjectTopic, hfSubjectTopic.Value);
                loadItemCategory(drpCategory, hfCategory.Value);
                loadItemSubCategory(drpSubCategory, hfSubCategory.Value);
            //}
        }
    }

    protected void lnkSubmit_Click(object sender, EventArgs e)
    {
        string msg = "";
        string ss = "";
        int qty = 0;
        int vv;

        for (int i = 0; i < gvUploadLibItem.Rows.Count; i++)
        {
            if (checkValues(i))
            {
                Label lblNoOfItem = (Label)gvUploadLibItem.Rows[i].FindControl("lblNoOfItem");
                Label lblAccessionNo = (Label)gvUploadLibItem.Rows[i].FindControl("lblAccessionNo");
                qty = Convert.ToInt32(lblNoOfItem.Text);
                for (int j = 0; j <= qty - 1; j++)
                {
                    vv = Convert.ToInt32(lblAccessionNo.Text);
                    ss = (vv + j).ToString();
                    msg = SaveData(gvUploadLibItem, i, ss);
                }
            }
        }

        if (msg.Trim() == "S")
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Record, Submitted successfully.", "S");
        }
        else
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, msg, "A");
        }
    }

    private string SaveData(GridView gvUploadLibItem, int i, string accno)
    {
        string msg = "";


        List<SqlParameter> param = new List<SqlParameter>();

        Label lblTitle = (Label)gvUploadLibItem.Rows[i].FindControl("lblTitle");
        Label lblNoOfItem = (Label)gvUploadLibItem.Rows[i].FindControl("lblNoOfItem");
        Label lblLibraryEntryDate = (Label)gvUploadLibItem.Rows[i].FindControl("lblLibraryEntryDate");
        Label lblBillNo = (Label)gvUploadLibItem.Rows[i].FindControl("lblBillNo");
        Label lblBiilDate = (Label)gvUploadLibItem.Rows[i].FindControl("lblBiilDate");
        Label lblPublicationYear = (Label)gvUploadLibItem.Rows[i].FindControl("lblPublicationYear");
        Label lblAuthor1 = (Label)gvUploadLibItem.Rows[i].FindControl("lblAuthor1");
        Label lblAuthor2 = (Label)gvUploadLibItem.Rows[i].FindControl("lblAuthor2");
        Label lblKeyword1 = (Label)gvUploadLibItem.Rows[i].FindControl("lblKeyword1");
        Label lblEdition = (Label)gvUploadLibItem.Rows[i].FindControl("lblEdition");
        Label lblEditor = (Label)gvUploadLibItem.Rows[i].FindControl("lblEditor");
        Label lblISBNISSN = (Label)gvUploadLibItem.Rows[i].FindControl("lblISBNISSN");
        Label lblPages = (Label)gvUploadLibItem.Rows[i].FindControl("lblPages");
        Label lblPrice = (Label)gvUploadLibItem.Rows[i].FindControl("lblPrice");


        DropDownList drpSupplier = (DropDownList)gvUploadLibItem.Rows[i].FindControl("drpSupplier");
        DropDownList drpPublisher = (DropDownList)gvUploadLibItem.Rows[i].FindControl("drpPublisher");
        DropDownList drpLanguage = (DropDownList)gvUploadLibItem.Rows[i].FindControl("drpLanguage");
        DropDownList drpSubjectTopic = (DropDownList)gvUploadLibItem.Rows[i].FindControl("drpSubjectTopic");
        DropDownList drpCategory = (DropDownList)gvUploadLibItem.Rows[i].FindControl("drpCategory");
        DropDownList drpSubCategory = (DropDownList)gvUploadLibItem.Rows[i].FindControl("drpSubCategory");

        param.Add(new SqlParameter("@AccessionNo", accno));
        param.Add(new SqlParameter("@Title", lblTitle.Text.ToString()));
        param.Add(new SqlParameter("@LibraryEntryDate", lblLibraryEntryDate.Text));
        param.Add(new SqlParameter("@Supplier", drpSupplier.SelectedValue.ToString()));
        param.Add(new SqlParameter("@Publisher", drpPublisher.SelectedValue.ToString()));
        param.Add(new SqlParameter("@NoOfItem", lblNoOfItem.Text.Trim()));
        param.Add(new SqlParameter("@Language", drpLanguage.SelectedValue.ToString()));
        param.Add(new SqlParameter("@BillNo", lblBillNo.Text.ToString()));
        param.Add(new SqlParameter("@BiilDate", lblBiilDate.Text.Trim()));
        param.Add(new SqlParameter("@PublicationYear", lblPublicationYear.Text.Trim()));


        param.Add(new SqlParameter("@SubjectTopic", drpSubjectTopic.SelectedValue.ToString()));
        param.Add(new SqlParameter("@Category", drpCategory.SelectedValue.ToString()));
        param.Add(new SqlParameter("@SubCategory", drpSubCategory.SelectedValue.ToString()));
        param.Add(new SqlParameter("@Author1", lblAuthor1.Text.ToString()));
        param.Add(new SqlParameter("@Author2", lblAuthor2.Text.ToString()));

        param.Add(new SqlParameter("@Keyword1", lblKeyword1.Text.ToString()));

        param.Add(new SqlParameter("@Edition", lblEdition.Text.ToString()));
        param.Add(new SqlParameter("@Editor", lblEditor.Text.ToString()));
        param.Add(new SqlParameter("@ISBNISSN", lblISBNISSN.Text.ToString()));
        param.Add(new SqlParameter("@Pages", lblPages.Text.ToString()));

        param.Add(new SqlParameter("@Price", lblPrice.Text.ToString()));
        param.Add(new SqlParameter("@LoginName", Session["LoginName"].ToString()));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
        param.Add(new SqlParameter("@SessionName", Session["SessionName"]));

        SqlParameter para = new SqlParameter("@Msg", "");
        para.Direction = ParameterDirection.Output;
        para.Size = 0x100;
        param.Add(para);

        msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("itemEntrybyExcel_proc", param);

        return msg;
    }

    public bool checkValues(int i)
    {
        bool flag = true;

        Label lblAccessionNo = (Label)gvUploadLibItem.Rows[i].FindControl("lblAccessionNo");
        if (lblAccessionNo.Text == string.Empty)
        {
            flag = false;
        }
        Label lblTitle = (Label)gvUploadLibItem.Rows[i].FindControl("lblTitle");
        if (lblTitle.Text == string.Empty)
        {
            flag = false;
        }

        Label lblNoOfItem = (Label)gvUploadLibItem.Rows[i].FindControl("lblNoOfItem");
        if (lblNoOfItem.Text == string.Empty)
        {
            flag = false;
        }

        Label LibraryEntryDate = (Label)gvUploadLibItem.Rows[i].FindControl("lblLibraryEntryDate");
        if (LibraryEntryDate.Text == string.Empty)
        {
            flag = false;
        }

        Label lblAuthor1 = (Label)gvUploadLibItem.Rows[i].FindControl("lblAuthor1");
        if (lblAuthor1.Text == string.Empty)
        {
            flag = false;
        }

        Label lblKeyword1 = (Label)gvUploadLibItem.Rows[i].FindControl("lblKeyword1");
        if (lblKeyword1.Text == string.Empty)
        {
            flag = false;
        }

        DropDownList drpLanguage = (DropDownList)gvUploadLibItem.Rows[i].FindControl("drpLanguage");
        if (drpLanguage.SelectedIndex == 0)
        {
            flag = false;
        }


        DropDownList drpSupplier = (DropDownList)gvUploadLibItem.Rows[i].FindControl("drpSupplier");
        if (drpSupplier.SelectedIndex == 0)
        {
            flag = false;
        }

        DropDownList drpPublisher = (DropDownList)gvUploadLibItem.Rows[i].FindControl("drpPublisher");
        if (drpPublisher.SelectedIndex == 0)
        {
            flag = false;
        }

        DropDownList drpSubjectTopic = (DropDownList)gvUploadLibItem.Rows[i].FindControl("drpSubjectTopic");
        if (drpSubjectTopic.SelectedIndex == 0)
        {
            flag = false;
        }

        DropDownList drpCategory = (DropDownList)gvUploadLibItem.Rows[i].FindControl("drpCategory");
        if (drpCategory.SelectedIndex == 0)
        {
            flag = false;
        }

        DropDownList drpSubCategory = (DropDownList)gvUploadLibItem.Rows[i].FindControl("drpSubCategory");
        if (drpSubCategory.SelectedIndex == 0)
        {
            flag = false;
        }

        if (flag == false)
        {
            gvUploadLibItem.Rows[i].CssClass = "red";
        }

        return flag;
    }

    protected void lnkDownloadExcel_Click(object sender, EventArgs e)
    {
        Response.ContentType = ContentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName("~/uploads/Excel/Lib/lib_item_entry_by_Excel.xlsx"));
        Response.WriteFile(Server.MapPath("~/uploads/Excel/Lib/lib_item_entry_by_Excel.xlsx"));
        Response.End();
    }

    protected void lnkReset_Click(object sender, EventArgs e)
    {
        Response.Redirect("item_entry_by_excel.aspx");
    }
}