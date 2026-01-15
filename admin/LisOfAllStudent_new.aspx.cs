using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;

public partial class admin_LisOfAllStudent : Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        drpSession.Focus();

        con = oo.dbGet_connection();
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        Campus camp = new Campus(); camp.LoadLoader(loader);
        BLL.BLLInstance.LoadHeader("Report", header1);
        if (!IsPostBack)
        {
          

            loadSession();
            loadCourse();
            loadClass();
            drpSection.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
            drpBranch.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
            drpStream.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
            loadMedium();
            loadCategory();

            loadBoard();

            

            abc.Visible = false;
            swipeText();

            
        }
        listdisplay.Visible = false;
    }

    public void TextTrnsform()
    {
        object value;
        List<SqlParameter> param = new List<SqlParameter>();
        param.Add(new SqlParameter("@isDo", "Select"));
        param.Add(new SqlParameter("@value", ""));
        param.Add(new SqlParameter("@SessionName", ""));
        param.Add(new SqlParameter("@LoginName", ""));
        SqlParameter para = new SqlParameter("@Msg", "");
        para.Direction = ParameterDirection.Output;
        para.Size = 0x10;
        param.Add(para);
        value = DLL.objDll.Sp_SelectRecord_usingExecuteScalar("SetandGet_texttransformdata", param);
        ScriptManager.RegisterClientScriptBlock(this.Page, GetType(), "textTransform", "finalsubmit('" + value + "')", true);
    }

    protected void loadBoard()
    {
        sql = "Select BoardName from BoardMaster";
        oo.FillDropDown(sql, drpBoard, "BoardName");

    }

    protected void loadCourse()
    {
        sql = "Select CourseName,Id from CourseMaster where SessionName='" + drpSession.SelectedItem.Text.ToString() + "'";
        oo.FillDropDown_withValue(sql, drpCourse, "CourseName", "Id");
        drpCourse.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));

    }

    protected void loadSession()
    {
        sql = "select SessionName from SessionMaster";
        sql = sql + " where BranchCode=" + Session["BranchCode"].ToString() + "";
        oo.FillDropDown(sql, drpSession, "SessionName");

        drpSession.Text = Session["SessionName"].ToString();
    }

    protected void loadClass()
    {
        sql = "Select ClassName,Id from ClassMaster";
        sql = sql + "  where (Course='" + drpCourse.SelectedValue.ToString() + "' or Course is null) and SessionName='" + drpSession.SelectedItem.Text.ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        sql = sql + " order by CIDOrder";
        oo.FillDropDown_withValue(sql, drpClass, "ClassName","Id");
        drpClass.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
    }

    protected void loadBranch()
    {
        sql = "Select BranchName,Id from BranchMaster where ClassId=" + drpClass.SelectedValue.ToString();
        sql = sql + " and SessionName='" + drpSession.SelectedItem.Text.ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

        oo.FillDropDown_withValue(sql, drpBranch, "BranchName", "Id");

        drpBranch.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
        
    }

    private void loadStream()
    {
        sql = "Select Stream,Id from StreamMaster where SessionName='" + Session["SessionName"].ToString() + "' and ClassId='" + drpClass.SelectedValue.ToString() + "' and BranchId='" + drpBranch.SelectedValue.ToString() + "'";
        oo.FillDropDown_withValue_withSelect(sql, drpStream, "Stream", "Id");
    }

    protected void loadSection()
    {
        sql = "Select SectionName from SectionMaster where ClassNameId=" + drpClass.SelectedValue.ToString();
        sql = sql + " and SessionName='" + drpSession.SelectedItem.Text.ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

        oo.FillDropDown(sql, drpSection, "SectionName");
        if (drpSection.Items.Count <= 0)
        {
            drpSection.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
        }
    }

    protected void loadMedium()
    {
        sql = "select Medium from MediumMaster where SessionName='" + drpSession.SelectedItem.Text.ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        oo.FillDropDown(sql, drpMedium, "Medium");
    }

    protected void loadCategory()
    {
        sql = "Select CasteName from CasteMaster";
        oo.FillDropDown(sql, drpCategory, "CasteName");
    }

    protected void drpClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadBranch();
        loadSection();
    }
   
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        loadgrid();
        TextTrnsform();
    }

    protected void loadgrid()
    {
        try
        {
            if (drpSession.SelectedIndex != 0)
            {
                Label15.Text = "( for " + drpSession.SelectedItem.Text.Trim() + "";
            }
            if (drpCourse.SelectedIndex != 0)
            {
                Label15.Text = Label15.Text + ", " + drpCourse.SelectedItem.Text.Trim() + "";
            }
            if (drpClass.SelectedIndex != 0)
            {
                Label15.Text = Label15.Text + ", " + drpClass.SelectedItem.Text.Trim() + "";
            }
            if (drpSection.SelectedIndex != 0)
            {
                Label15.Text = Label15.Text + ", " + drpSection.SelectedItem.Text.Trim() + "";
            }
            if (drpBranch.SelectedIndex != 0)
            {
                Label15.Text = Label15.Text + ", " + drpBranch.SelectedItem.Text.Trim() + "";
            }
            if (drpStream.SelectedIndex != 0)
            {
                Label15.Text = Label15.Text + ", " + drpStream.SelectedItem.Text.Trim() + "";
            }
            if (drpMedium.SelectedIndex != 0)
            {
                Label15.Text = Label15.Text + ", " + drpMedium.SelectedItem.Text.Trim() + "";
            }
            if (drpCategory.SelectedIndex != 0)
            {
                Label15.Text = Label15.Text + ", " + drpCategory.SelectedItem.Text.Trim() + "";
            }
            if (drpFeegroup.SelectedIndex != 0)
            {
                Label15.Text = Label15.Text + ", " + drpFeegroup.SelectedItem.Text.Trim() + "";
            }
            if (drpType.SelectedIndex != 0)
            {
                Label15.Text = Label15.Text + ", " + drpType.SelectedItem.Text.Trim() + "";
            }
            if (drpBoard.SelectedIndex != 0)
            {
                Label15.Text = Label15.Text + ", " + drpBoard.SelectedItem.Text.Trim() + "";
            }
            Label15.Text = Label15.Text + ")";
            Label16.Text = drpStatus.SelectedItem.Text;
            Label17.Text = RadioButtonList1.SelectedItem.Text;
            lblPrintDate.Text = DateTime.Today.ToString("dd MMM yyyy");
            listdisplay.Visible = true;

            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@SessionName", drpSession.SelectedItem.Text.ToString()));
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
            param.Add(new SqlParameter("@FeeGroup", drpFeegroup.SelectedItem.Text));
            param.Add(new SqlParameter("@CourseName", drpCourse.SelectedItem.Text));
            param.Add(new SqlParameter("@ClassName", drpClass.SelectedItem.Text));
            param.Add(new SqlParameter("@SectionName", drpSection.SelectedItem.Text));
            param.Add(new SqlParameter("@BranchName", drpBranch.SelectedItem.Text));
            param.Add(new SqlParameter("@Stream", drpStream.SelectedItem.Text));
            param.Add(new SqlParameter("@TypeOFAdmision", drpType.SelectedItem.Text));
            param.Add(new SqlParameter("@Medium", drpMedium.SelectedItem.Text));
            param.Add(new SqlParameter("@Withdrwal", drpStatus.SelectedValue.ToString()));
            param.Add(new SqlParameter("@Category", drpCategory.SelectedItem.Text));
            param.Add(new SqlParameter("@Board", drpBoard.SelectedItem.Text.ToString()));
            param.Add(new SqlParameter("@Gender", RadioButtonList1.SelectedItem.Text.ToUpper()));
            param.Add(new SqlParameter("@DisplayOrder", RadioButtonList2.SelectedValue.ToString()));

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            ds = new DLL().Sp_SelectRecord_usingExecuteDataset("Get_AllStudentRecord", param);
            if (ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];


                if (dt.Rows.Count > 0)
                {
                    string n = "<table id='example' class='table table-striped text-center table-hover no-head-border table-bordered pro-table table-header-group ' cellspacing='0' width='100%'> <thead> <tr><th>#</th> <th>S.R.No.</th> <th>Name</th> <th>Course</th> <th>Class</th> <th>Section</th> <th>Branch</th> <th>Stream</th> <th>Optional Subjects</th> <th>Medium</th> <th>DOB</th> <th>Father&#39;s Name</th> <th>Father&#39;s Contact No.</th> <th>Father&#39;s Occupation</th> <th>Father&#39;s Income</th> <th>Mother&#39;s Name</th> <th>Mother&#39;s Contact No.</th> <th>Mother&#39;s Occupation</th> <th>Mother&#39;s Income</th> <th>Guardian Name</th> <th>Guardian Contact No.</th>  <th>Permanent Address</th> <th>Present Address</th> <th>Date of Admission</th> <th>Categoty</th> <th>Fee Group</th> <th>Type</th> <th>Status</th> <th>Gender</th> <th>Blood Group</th> <th>Religion</th> <th>Board/ University</th> <th>House</th> <th>Hostel Facility</th> <th>Library Facility</th> <th>Transport Facility</th> <th>Student&#39;s Photo</th> <th>Father&#39;s Photo</th> <th>Mother&#39;s Photo</th> </tr> </thead><tbody>";
                    int nn;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        nn = i+1;
                        n += "<tr><td>" + nn + "</td>";
                        n += "<td>" + dt.Rows[i]["S.R.No."] + "</td>";
                        n += "<td>" + dt.Rows[i]["Name"] + "</td>";
                        n += "<td>" + dt.Rows[i]["Course"] + "</td>";
                        n += "<td>" + dt.Rows[i]["Class"] + "</td>";
                        n += "<td>" + dt.Rows[i]["Section"] + "</td>";
                        n += "<td>" + dt.Rows[i]["Branch"] + "</td>";
                        n += "<td>" + dt.Rows[i]["Stream"] + "</td>";
                        n += "<td>" + dt.Rows[i]["Optional Subjects"] + "</td>";
                        n += "<td>" + dt.Rows[i]["Medium"] + "</td>";
                        n += "<td>" + dt.Rows[i]["DOB"] + "</td>";
                        n += "<td>" + dt.Rows[i]["Father's Name"] + "</td>";
                        n += "<td>" + dt.Rows[i]["Father's Contact No."] + "</td>";
                        n += "<td>" + dt.Rows[i]["Father's Occupation"] + "</td>";
                        n += "<td>" + dt.Rows[i]["Father's Income"] + "</td>";
                        n += "<td>" + dt.Rows[i]["Mother's Name"] + "</td>";
                        n += "<td>" + dt.Rows[i]["Mother's Contact No."] + "</td>";
                        n += "<td>" + dt.Rows[i]["Mother's Occupation"] + "</td>";
                        n += "<td>" + dt.Rows[i]["Mother's Income"] + "</td>";
                        n += "<td>" + dt.Rows[i]["Guardian Name"] + "</td>";
                        n += "<td>" + dt.Rows[i]["Guardian Contact No."] + "</td>";
                        n += "<td>" + dt.Rows[i]["Permanent Address"] + "</td>";
                        n += "<td>" + dt.Rows[i]["Present Address"] + "</td>";
                        n += "<td>" + dt.Rows[i]["Date of Admission"] + "</td>";
                        n += "<td>" + dt.Rows[i]["Categoty"] + "</td>";
                        n += "<td>" + dt.Rows[i]["Fee Group"] + "</td>";
                        n += "<td>" + dt.Rows[i]["Type"] + "</td>";
                        n += "<td>" + dt.Rows[i]["Status"] + "</td>";
                        n += "<td>" + dt.Rows[i]["Gender"] + "</td>";
                        n += "<td>" + dt.Rows[i]["Blood Group"] + "</td>";
                        n += "<td>" + dt.Rows[i]["Regigion"] + "</td>";
                        n += "<td>" + dt.Rows[i]["Board"] + "</td>";
                        n += "<td>" + dt.Rows[i]["House"] + "</td>";
                        n += "<td>" + dt.Rows[i]["Hostel Facility"] + "</td>";
                        n += "<td>" + dt.Rows[i]["Library Facility"] + "</td>";
                        n += "<td>" + dt.Rows[i]["Transport Facility"] + "</td>";
                        n += "<td>" + dt.Rows[i]["Student's Photo"] + "</td>";
                        n += "<td>" + dt.Rows[i]["Father's Photo"] + "</td>";
                        //n += "<td>" + dt.Rows[i][""] + "</td>";
                        n += "<td>" + dt.Rows[i]["Mother's Photo"] + "</td></tr>";
                    }
                    ltrshow.Text = n + "</tbody></table>";
                }
                else
                {

                }



                swipeDtColumnText(dt);

                GridView1.DataSource = dt;
                GridView1.DataBind();


                if (GridView1.Rows.Count > 0)
                {
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                abc.Visible = true;
                hideColoumns();

                if (CheckBoxList1.Items[7].Selected)
                {
                    for (int i = 0; i < GridView1.Rows.Count; i++)
                    {
                        string srno = GridView1.Rows[i].Cells[1].Text;
                        string optionalsubjects = "";
                        param = new List<SqlParameter>();
                        param.Add(new SqlParameter("@SessionName", drpSession.SelectedItem.Text.ToString()));
                        param.Add(new SqlParameter("@SRNO", srno));
                        optionalsubjects = Convert.ToString(DLL.objDll.Sp_SelectRecord_usingExecuteScalar("OPTIONALSUBJECTLISTPROC", param));

                        GridView1.Rows[i].Cells[9].Text = optionalsubjects;
                    }
                }
                TextTrnsform();


            }
            else
            {
                abc.Visible = false;
                oo.MessageBoxforUpdatePanel("Sorry, No Record&apos;s found", this.Page);
            }
        }
        catch (Exception )
        {  }
    }

    protected void swipeText()
    {
        for (int i = 0; i < CheckBoxList1.Items.Count; i++)
        {
            string text = CheckBoxList1.Items[i].Text;
            sql = "Select replace from DefaultText where replacewith='" + text + "'";
            if(oo.ReturnTag(sql, "replace")!="")
            {
                CheckBoxList1.Items[i].Text = oo.ReturnTag(sql, "replace");
            }
        }
    }

    protected void swipeDtColumnText(DataTable dt)
    {
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            string text = dt.Columns[i].ColumnName;
            sql = "Select replace from DefaultText where replacewith='" + text + "'";
            if (oo.ReturnTag(sql, "replace") != "")
            {
                dt.Columns[i].ColumnName = oo.ReturnTag(sql, "replace");
            }
        }
    }

    protected void hideColoumns()
    {
        for (int i = 1; i < GridView1.HeaderRow.Cells.Count; i++)
        {
            GridView1.HeaderRow.Cells[i].Visible = false;
        }
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            for (int j = 1; j < GridView1.Rows[i].Cells.Count; j++)
            {
                GridView1.Rows[i].Cells[j].Visible = false;
            }
        }
        for (int j = 0; j < CheckBoxList1.Items.Count; j++)
        {
            for (int i = 1; i < GridView1.HeaderRow.Cells.Count; i++)
            {
                if (CheckBoxList1.Items[j].Selected == true)
                {
                    if (GridView1.HeaderRow.Cells[i].Text.ToUpper().Replace("&#39;", "") == CheckBoxList1.Items[j].Text.ToUpper().Replace("'", ""))
                    {
                        GridView1.HeaderRow.Cells[i].Visible = true;
                        for (int k = 0; k < GridView1.Rows.Count; k++)
                        {
                            GridView1.Rows[k].Cells[i].Visible = true;
                        }
                    }
                }
            }
        }
    }

    

    protected void ImageButton3_Click(object sender, EventArgs e)
    {
        loadImages();
        oo.ExporttolandscapePdf(Response, "ListOfStudents", gdv1);
    }
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        loadImages();
        oo.ExportTolandscapeWord(Response, "ListOfStudents", gdv);
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        loadImages();
        GridView1.Style.Add("text-transform", "uppercase");
        oo.ExportDivToExcelWithFormatting(Response, "ListOfStudents.xls", gdv, Server.MapPath("~/Admin/css/style.css"));
    }

    protected void CheckTextTrnsformation()
    {
        object value;
        List<SqlParameter> param = new List<SqlParameter>();
        param.Add(new SqlParameter("@isDo", "Select"));
        param.Add(new SqlParameter("@value", ""));
        param.Add(new SqlParameter("@SessionName", ""));
        param.Add(new SqlParameter("@LoginName", ""));
        SqlParameter para = new SqlParameter("@Msg", "");
        para.Direction = ParameterDirection.Output;
        para.Size = 0x10;
        param.Add(para);
        value = DLL.objDll.Sp_SelectRecord_usingExecuteScalar("SetandGet_texttransformdata", param);
        if (value != DBNull.Value)
        {
            switch ((string)value)
            {
                case "U":
                    GridView1.Style.Add("text-transform", "uppercase");
                    break;
                case "L":
                    GridView1.Style.Add("text-transform", "lowercase");
                    break;
                case "C":
                    GridView1.Style.Add("text-transform", "capitalize");
                    break;
                default:
                    GridView1.Style.Add("text-transform", "none");
                    break;
            }
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //CheckTextTrnsformation();


        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            for (int j = 0; j < CheckBoxList1.Items.Count; j++)
            {
                for (int i = 1; i < GridView1.HeaderRow.Cells.Count; i++)
                {
                    if (CheckBoxList1.Items[j].Selected == true)
                    {
                        if (CheckBoxList1.Items[j].Text.ToUpper().Replace("'", "") == "Students Photo".ToUpper() ||
                            CheckBoxList1.Items[j].Text.ToUpper().Replace("'", "") == "Fathers Photo".ToUpper() ||
                            CheckBoxList1.Items[j].Text.ToUpper().Replace("'", "") == "Mothers Photo".ToUpper())
                        {
                            if (GridView1.HeaderRow.Cells[i].Text.ToUpper().Replace("&#39;", "") == CheckBoxList1.Items[j].Text.ToUpper().Replace("'", ""))
                            {
                                if (e.Row.Cells[i].Text != "&nbsp;")
                                {
                                    Image img = new Image();
                                    img.Height = 50;
                                    img.Width = 50;
                                    img.ImageUrl = oo.GetImageUrl(ResolveClientUrl(e.Row.Cells[i].Text), Request.Url.AbsoluteUri.Split('/'));
                                    e.Row.Cells[i].Controls.Add(img);
                                }
                            }
                        }
                    }
                }
            }

            TextTrnsform();
        }
    }

    protected void loadImages()
    {
        for (int j = 0; j < CheckBoxList1.Items.Count; j++)
        {
            for (int i = 1; i < GridView1.HeaderRow.Cells.Count; i++)
            {
                if (CheckBoxList1.Items[j].Selected == true)
                {
                    if (CheckBoxList1.Items[j].Text.ToUpper().Replace("'", "") == "Students Photo".ToUpper() || 
                        CheckBoxList1.Items[j].Text.ToUpper().Replace("'", "") == "Fathers Photo".ToUpper() || 
                        CheckBoxList1.Items[j].Text.ToUpper().Replace("'", "") == "Mothers Photo".ToUpper())
                    {
                        if (GridView1.HeaderRow.Cells[i].Text.ToUpper().Replace("&#39;", "") == CheckBoxList1.Items[j].Text.ToUpper().Replace("'", ""))
                        {
                            for (int k = 0; k < GridView1.Rows.Count; k++)
                            {
                                if (GridView1.Rows[k].Cells[i].Text != "&nbsp;")
                                {
                                    Image img = new Image();
                                    img.Height = 50;
                                    img.Width = 50;
                                    img.ImageUrl = oo.GetImageUrl(ResolveClientUrl(GridView1.Rows[k].Cells[i].Text), Request.Url.AbsoluteUri.Split('/'));
                                    GridView1.Rows[k].Cells[i].Controls.Add(img);
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        loadImages();
        
        if (GridView1.Rows.Count > 0)
        {
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            TextTrnsform();
        }
        PrintHelper_New.ctrl = abc;
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "onclick", "var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}", true);
    
        loadgrid();
       
    }
    protected void drpCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadClass();
    }
    protected void drpSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadCourse();
    }
    protected void drpBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadStream();
    }
}