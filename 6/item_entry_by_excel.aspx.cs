using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_libraryItemEntryByExcel : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    readonly Campus oo = new Campus();
    string sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Campus camp = new Campus(); camp.LoadLoader(loader);
        if (!IsPostBack)
        {
            truncatetable();
        }
    }

    public void truncatetable()
    {
        try
        {
            sql = "delete from TempAccessionNo Where BranchCode = " + Session["BranchCode"] + "";
            BAL.objBal.Insert_Update_Delete1(sql);
        }
        catch
        { }
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
        }
    }

    protected void lnkShow_Click(object sender, EventArgs e)
    {
        gvUploadLibItem.DataSource = null;
        gvUploadLibItem.DataBind();
        PublishershowExcel();
    }

    private void PublishershowExcel()
    {
        try
        {
            string base64std1 = hfFile.Value;
            string fileExtention1 = hdfilefileExtention.Value;

            string filePath1 = "";
            string fileName1 = "";

            if (base64std1 != string.Empty)
            {
                filePath1 = @"../uploads/UploadExcel/";

                DateTime date = DateTime.Now;

                string time = date.ToString("HH_mm_ss");

                fileName1 = String.Format("libitemlist" + "{0}" + fileExtention1, time);

                using (FileStream fs = new FileStream(Server.MapPath((filePath1 + fileName1)), FileMode.Create))
                {
                    using (BinaryWriter bw = new BinaryWriter(fs))
                    {
                        byte[] data = Convert.FromBase64String(base64std1);
                        bw.Write(data);
                        bw.Close();
                    }
                }
            }

            if ((filePath1 + fileName1) != string.Empty)
            {
                string sheetname = "Item_Entry";

                var dt2 = ReadExcel(Server.MapPath((filePath1 + fileName1)), fileExtention1, lnkShow, sheetname + "$");

                File.Delete(Server.MapPath((filePath1 + fileName1)));
                if (dt2.Rows.Count==0)
                {
                    lnkSubmit.Visible = false;
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Please select valid excel file!", "A");
                    gvUploadLibItem.DataSource = null;
                    gvUploadLibItem.DataBind();
                    return;
                }
                if (dt2.Rows.Count >1000)
                {
                    lnkSubmit.Visible = false;
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Please enter maximum 1000 records at a time!", "A");
                    gvUploadLibItem.DataSource = null;
                    gvUploadLibItem.DataBind();
                    return;
                }
                gvUploadLibItem.DataSource = dt2;
                gvUploadLibItem.DataBind();
                
            }
            if (gvUploadLibItem.Rows.Count > 0)
            {
                
                hfFile.Value = "";
                hdfilefileExtention.Value = "";
                if (checkValues())
                {
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Data verified successfully.", "S");
                    
                    lnkSubmit.Visible = true;
                }
                else
                {
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Mandatory columns should have their values!", "A");
                    lnkSubmit.Visible = false;
                }
            }
            else
            {
                lnkSubmit.Visible = false;
                hfFile.Value = "";
                hdfilefileExtention.Value = "";
            }
        }
        catch (Exception ex)
        {
        }

    }
    public DataTable ReadExcel(string fileName, string fileExt, Control ctrl, string sheetName)
    {
        try
        {
            var conn = string.Empty;
            var dtexcel = new DataTable();
            if (fileExt.CompareTo(".xls") == 0)
            {
                conn = @"provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties='Excel 8.0;HRD=Yes;IMEX=1';"; //for below excel 2007 
            }
            else
            {
                conn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties='Excel 12.0;HDR=NO';"; //for above excel 2007 
            }
            using (var con = new OleDbConnection(conn))
            {
                try
                {
                    OleDbCommand cmd = new OleDbCommand(@"select * from [" + sheetName + "] where F3<>'Title*'", con);
                    //OleDbCommand cmd = new OleDbCommand(@"select * from [" + sheetName + "]", con);
                    OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                    adapter.Fill(dtexcel);
                    con.Close();
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(ctrl, GetType(), "alert", "window.alert('" + ex.Message + "')", true);
                }
            }
            return dtexcel;
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void gvUploadLibItem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

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
        }
    }

    protected void lnkSubmit_Click(object sender, EventArgs e)
    {
        string msg = "";
        string vv="";
        int duplicate=0;
        int success = 0;
        if (checkValues())
        {
            for (int i = 0; i < gvUploadLibItem.Rows.Count; i++)
            {
                Label lblAccessionNo = (Label)gvUploadLibItem.Rows[i].FindControl("lblAccessionNo");
                vv = lblAccessionNo.Text.Trim();
                msg = SaveData(gvUploadLibItem, i, vv.ToString());
                if (msg.Trim().ToLower()=="s")
                {
                    success = success + 1;
                }
                if (msg.Trim().ToLower() == "du")
                {
                    duplicate = duplicate + 1;
                }
            }
            if (success>0 && duplicate==0)
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Record(s) submitted successfully.", "S");
            }
            else if (duplicate > 0 && (success>0 || success==0))
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Duplicate record(s) found!", "A");
            }

            gvUploadLibItem.DataSource = null;
            gvUploadLibItem.DataBind();
            lnkSubmit.Visible = false;
            hfFile.Value = "";
            hdfilefileExtention.Value = "";
        }
        else
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Mandatory columns should have their values!", "A");
        }
    }
    private string SaveData(GridView gvUploadLibItem, int i, string accno)
    {
        string msg = "";
        List<SqlParameter> param = new List<SqlParameter>();
        Label lblId = (Label)gvUploadLibItem.Rows[i].FindControl("lblId");
        Label lblTitle = (Label)gvUploadLibItem.Rows[i].FindControl("lblTitle");
        Label lblClass = (Label)gvUploadLibItem.Rows[i].FindControl("lblClass");
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
        Label lblLocation = (Label)gvUploadLibItem.Rows[i].FindControl("lblLocation");
        DropDownList drpSupplier = (DropDownList)gvUploadLibItem.Rows[i].FindControl("drpSupplier");
        DropDownList drpPublisher = (DropDownList)gvUploadLibItem.Rows[i].FindControl("drpPublisher");
        DropDownList drpLanguage = (DropDownList)gvUploadLibItem.Rows[i].FindControl("drpLanguage");
        DropDownList drpSubjectTopic = (DropDownList)gvUploadLibItem.Rows[i].FindControl("drpSubjectTopic");
        DropDownList drpCategory = (DropDownList)gvUploadLibItem.Rows[i].FindControl("drpCategory");
        DropDownList drpSubCategory = (DropDownList)gvUploadLibItem.Rows[i].FindControl("drpSubCategory");
        param.Add(new SqlParameter("@id", lblId.Text.Trim()));
        param.Add(new SqlParameter("@AccessionNo", accno));
        param.Add(new SqlParameter("@Title", lblTitle.Text.ToString()));
        string LibraryEntryDate = "";
        if (lblLibraryEntryDate.Text == "")
        {
            LibraryEntryDate = DateTime.Now.ToString("dd-MMM-yyyy");
        }
        else
        {
            LibraryEntryDate=DateTime.Parse(lblLibraryEntryDate.Text).ToString("dd-MMM-yyyy");
        }
        param.Add(new SqlParameter("@LibraryEntryDate", LibraryEntryDate.Trim()));
        param.Add(new SqlParameter("@Supplier", drpSupplier.SelectedValue.ToString()));
        param.Add(new SqlParameter("@Publisher", drpPublisher.SelectedValue.ToString()));
        param.Add(new SqlParameter("@NoOfItem", "1"));
        param.Add(new SqlParameter("@ClassName", lblClass.Text.Trim()));
        param.Add(new SqlParameter("@Language", drpLanguage.SelectedValue.ToString()));
        param.Add(new SqlParameter("@BillNo", lblBillNo.Text.Trim().ToString()));
        param.Add(new SqlParameter("@BiilDate", lblBiilDate.Text.Trim()));
        param.Add(new SqlParameter("@PublicationYear", lblPublicationYear.Text.Trim()));
        param.Add(new SqlParameter("@SubjectTopic", drpSubjectTopic.SelectedValue.ToString()));
        param.Add(new SqlParameter("@Category", drpCategory.SelectedValue.ToString()));
        param.Add(new SqlParameter("@SubCategory", drpSubCategory.SelectedValue.ToString()));
        param.Add(new SqlParameter("@Author1", lblAuthor1.Text.Trim().ToString()));
        param.Add(new SqlParameter("@Author2", lblAuthor2.Text.Trim().ToString()));
        param.Add(new SqlParameter("@Keyword1", lblKeyword1.Text.Trim().ToString()));
        param.Add(new SqlParameter("@Edition", lblEdition.Text.Trim().ToString()));
        param.Add(new SqlParameter("@Editor", lblEditor.Text.Trim().ToString()));
        param.Add(new SqlParameter("@ISBNISSN", lblISBNISSN.Text.Trim().ToString()));
        param.Add(new SqlParameter("@Pages", lblPages.Text.Trim().ToString()));
        param.Add(new SqlParameter("@Price", lblPrice.Text.Trim().ToString()));
        param.Add(new SqlParameter("@Location", lblLocation.Text.Trim().ToString()));
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

    public bool checkValues()
    {
        int flagNain = 0;
        
        for (int i = 0; i < gvUploadLibItem.Rows.Count; i++)
        {
            int flag = 0;
            Label lblLibraryEntryDate = (Label)gvUploadLibItem.Rows[i].FindControl("lblLibraryEntryDate");
            if (lblLibraryEntryDate.Text == "")
            {
                var col = gvUploadLibItem.Rows[i].Cells[3];
                col.BorderColor = System.Drawing.Color.Red;
                flag = flag + 1;
                flagNain = flagNain + 1;
                col.Text = "Error";

            }
            
            Label lblAccessionNo = (Label)gvUploadLibItem.Rows[i].FindControl("lblAccessionNo");
            if (lblAccessionNo.Text == string.Empty)
            {
                var col = gvUploadLibItem.Rows[i].Cells[1];
                col.BorderColor = System.Drawing.Color.Red;
                flag = flag + 1;
                flagNain = flagNain + 1;
                col.Text = "Error";
            }
            Label lblTitle = (Label)gvUploadLibItem.Rows[i].FindControl("lblTitle");
            if (lblTitle.Text == string.Empty)
            {
                var col = gvUploadLibItem.Rows[i].Cells[2];
                col.BorderColor = System.Drawing.Color.Red;
                flag = flag + 1;
                flagNain = flagNain + 1;
                col.Text = "Error";
            }

           

            Label LibraryEntryDate = (Label)gvUploadLibItem.Rows[i].FindControl("lblLibraryEntryDate");
            if (LibraryEntryDate.Text == string.Empty)
            {
                var col = gvUploadLibItem.Rows[i].Cells[3];
                col.BorderColor = System.Drawing.Color.Red;
                flag = flag + 1;
                flagNain = flagNain + 1;
                col.Text = "Error";
            }

            Label lblAuthor1 = (Label)gvUploadLibItem.Rows[i].FindControl("lblAuthor1");
            if (lblAuthor1.Text == string.Empty)
            {
                var col = gvUploadLibItem.Rows[i].Cells[14];
                col.BorderColor = System.Drawing.Color.Red;
                flag = flag + 1;
                flagNain = flagNain + 1;
                col.Text = "Error";
            }

            Label lblKeyword1 = (Label)gvUploadLibItem.Rows[i].FindControl("lblKeyword1");
            if (lblKeyword1.Text == string.Empty)
            {
                var col = gvUploadLibItem.Rows[i].Cells[16];
                col.BorderColor = System.Drawing.Color.Red;
                flag = flag + 1;
                flagNain = flagNain + 1;
                col.Text = "Error";
            }

            DropDownList drpLanguage = (DropDownList)gvUploadLibItem.Rows[i].FindControl("drpLanguage");
            if (drpLanguage.SelectedIndex == 0)
            {
                var col = gvUploadLibItem.Rows[i].Cells[7];
                col.BorderColor = System.Drawing.Color.Red;
                flag = flag + 1;
                flagNain = flagNain + 1;
                col.Text = "Error";
            }


            DropDownList drpSupplier = (DropDownList)gvUploadLibItem.Rows[i].FindControl("drpSupplier");
            if (drpSupplier.SelectedIndex == 0)
            {
                var col = gvUploadLibItem.Rows[i].Cells[4];
                col.BorderColor = System.Drawing.Color.Red;
                flag = flag + 1;
                flagNain = flagNain + 1;
                col.Text = "Error";
            }

            DropDownList drpPublisher = (DropDownList)gvUploadLibItem.Rows[i].FindControl("drpPublisher");
            if (drpPublisher.SelectedIndex == 0)
            {
                var col = gvUploadLibItem.Rows[i].Cells[5];
                col.BorderColor = System.Drawing.Color.Red;
                flag = flag + 1;
                flagNain = flagNain + 1;
                col.Text = "Error";
            }

            DropDownList drpSubjectTopic = (DropDownList)gvUploadLibItem.Rows[i].FindControl("drpSubjectTopic");
            if (drpSubjectTopic.SelectedIndex == 0)
            {
                var col = gvUploadLibItem.Rows[i].Cells[11];
                col.BorderColor = System.Drawing.Color.Red;
                flag = flag + 1;
                flagNain = flagNain + 1;
                col.Text = "Error";
            }

            DropDownList drpCategory = (DropDownList)gvUploadLibItem.Rows[i].FindControl("drpCategory");
            if (drpCategory.SelectedIndex == 0)
            {
                var col = gvUploadLibItem.Rows[i].Cells[12];
                col.BorderColor = System.Drawing.Color.Red;
                flag = flag + 1;
                flagNain = flagNain + 1;
                col.Text = "Error";
            }

            DropDownList drpSubCategory = (DropDownList)gvUploadLibItem.Rows[i].FindControl("drpSubCategory");
            if (drpSubCategory.SelectedIndex == 0)
            {
                var col = gvUploadLibItem.Rows[i].Cells[13];
                col.BorderColor = System.Drawing.Color.Red;
                flag = flag + 1;
                flagNain = flagNain + 1;
                col.Text = "Error";
            }

            if (flag >0)
            {
                //gvUploadLibItem.Rows[i].CssClass = "red";
            }
        }
        if (flagNain > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
        
    }

    protected void lnkDownloadExcel_Click(object sender, EventArgs e)
    {
        Response.ContentType = ContentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName("~/uploads/Excel/Lib/lib_item_entry_by_Excel.xlsx"));
        Response.WriteFile(Server.MapPath("~/uploads/Excel/Lib/lib_item_entry_by_Excel.xlsx"));
        Response.End();
    }
    

    

    
}