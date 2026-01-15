using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace _1
{
    public partial class AdminSubjectwiseAttendance : System.Web.UI.Page
    {
       private SqlConnection _con;
        private readonly Campus _oo;
        private string _sql = "";
        public AdminSubjectwiseAttendance()
        {
            _con = new SqlConnection();
            _oo = new Campus();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            _con = _oo.dbGet_connection();
            Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file


            if ((string) Session["LoginName"] == "" || (string)Session["BranchCode"] == "" || (string)Session["SessionName"] == "" || Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
            {
                Response.Redirect("default.aspx");
            }
            if (!IsPostBack)
            {
                _sql = "Select ClassName,Id from ClassMaster";
                _sql = _sql + " where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                _sql = _sql + "  order by CIDOrder";
                _oo.FillDropDown_withValue(_sql, DrpAtteClass, "ClassName","Id");
                //////sql = "Select SectionName from SectionMaster";
                //////sql = sql + " where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
                //////oo.FillDropDown(sql, DrpAttenSection, "SectionName");

                //sql = "select Id from ClassMaster where  ClassName='" + DrpAtteClass.SelectedItem.ToString() + "'";
                //sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
                //CCode = oo.ReturnTag(sql, "id");
                //sql = "Select SectionName from SectionMaster where ClassNameId=" + CCode;
                //sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

                // oo.FillDropDown(sql, DrpAttenSection, "SectionName");
                _oo.AddDateMonthYearDropDown(DrpSaal, DrpMahina, DrpDin);
                _oo.FindCurrentDateandSetinDropDown(DrpSaal, DrpMahina, DrpDin);
                DrpAttenSection.Items.Clear();
                DropDownList2.Items.Clear();
                DrpAttenSection.Items.Insert(0, "<--Select-->");
                DropDownList2.Items.Insert(0, "<--Select-->");
            }
        }
        protected void DrpSaal_SelectedIndexChanged(object sender, EventArgs e)
        {
            _oo.YearDropDown(DrpSaal, DrpMahina, DrpDin);
        }
        protected void DrpMahina_SelectedIndexChanged(object sender, EventArgs e)
        {
            _oo.MonthDropDown(DrpSaal, DrpMahina, DrpDin);
        }

        

        protected void btnShow_Click(object sender, EventArgs e)
        {
            //oo.MessageBox(DrpAtteClass.SelectedIndex.ToString(), this.Page);
            if (DrpAtteClass.SelectedItem.Text != "<--Select-->")
            {
                //lblmess.Text = "Insert";
                if (DrpAttenSection.SelectedItem.Text != "<--Select-->")
                {
                    if (DropDownList2.SelectedItem.Text != "<--Select-->")
                    {
                        string dateVal = DrpSaal.SelectedItem.Text + "/" + DrpMahina.SelectedItem.Text + "/" + DrpDin.SelectedItem.Text;
                        _sql = "select Row_Number() over(Order By AttendanceSubjectwise.Id) as Id,AttendanceSubjectwise.Id as Ids,AttendanceSubjectwise.SrNo,StudentGenaralDetail.StEnRCode,AttendanceSubjectwise.AttendanceValue,StudentGenaralDetail.FirstName+' '+StudentGenaralDetail.MiddleName+' '+StudentGenaralDetail.LastName as StudentName,StudentFamilyDetails.FatherName from AttendanceSubjectwise  inner join StudentGenaralDetail on StudentGenaralDetail.SrNo=AttendanceSubjectwise.SrNo inner join StudentFamilyDetails on StudentFamilyDetails.SrNo=AttendanceSubjectwise.SrNo  where AttendanceSubjectwise.ClassName='" + DrpAtteClass.SelectedValue + "' and AttendanceSubjectwise.SectionName='" + DrpAttenSection.SelectedValue + "' and AttendanceSubjectwise.SubjectId='" + DropDownList2.SelectedValue + "' and AttendanceSubjectwise.AttendanceDate='" + dateVal + "' and AttendanceSubjectwise.SessionName='" + Session["SessionName"] + "' and AttendanceSubjectwise.BranchCode=" + Session["BranchCode"] + " and StudentGenaralDetail.SessionName='" + Session["SessionName"] + "' and StudentGenaralDetail.BranchCode=" + Session["BranchCode"] + " and StudentFamilyDetails.SessionName='" + Session["SessionName"] + "'"; 
                        Grd.DataSource = _oo.GridFill(_sql);
                        Grd.DataBind();
                        for (int a = 0; a < Grd.Rows.Count; a++)//State Wise
                        {
                            DropDownList drp = (DropDownList)Grd.Rows[a].FindControl("DropDownList1");
                            Label label11 = (Label)Grd.Rows[a].FindControl("Label11");
                            //sql = "select AttendanceValue from AttendanceSubjectwise where Id='" + Label11.Text + "' and SessionName='" + Session["SessionName"].ToString() + "'";
                            //Drp.Text = oo.ReturnTag(sql, "AttendanceValue").ToString();
                            _sql = "select AbbreviationName from  AttendanceAbbreviationMaster";
                            //  sql = sql + " where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
                            _oo.FillDropDownWithOutSelect(_sql, drp, "AbbreviationName");
                            _sql = "select AttendanceValue from AttendanceSubjectwise where Id='" + label11.Text + "' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";


                            if (_oo.ReturnTag(_sql, "AttendanceValue") == "A         ")
                            {
                                drp.SelectedIndex = drp.Items.IndexOf(drp.Items.FindByText("A"));
                            }
                            else if (_oo.ReturnTag(_sql, "AttendanceValue") == "P         ")
                            {
                                drp.SelectedIndex = drp.Items.IndexOf(drp.Items.FindByText("P"));
                            }
                            else
                            {
                                drp.SelectedIndex = drp.Items.IndexOf(drp.Items.FindByText("L"));
                            }            
                    
                            //Drp.Items.Insert(0, "<--Select-->");                      

                        }                   
                    }
                    else
                    {
                        //oo.MessageBox("Please Select Subject First!", this.Page);
                        Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Please Select Subject First!", "A");

                    }
                }
                else
                {
                    //oo.MessageBox("Please Select Section First!", this.Page);
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Please Select Section First!", "A");

                }
            }
            else
            {
                //oo.MessageBox("Please Select Class First!", this.Page);
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Please Select Class First!", "A");

            }

        }
        protected void DrpAtteClass_SelectedIndexChanged(object sender, EventArgs e)
        {
       
            //sql = "select Id from ClassMaster where  ClassName='" + DrpAtteClass.SelectedItem.ToString() + "'";
            //sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

            //CCode = oo.ReturnTag(sql, "id");
            _sql = "Select SectionName,Id from SectionMaster where ClassNameId=" + DrpAtteClass.SelectedValue;
            _sql = _sql + " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";

            _oo.FillDropDown_withValue(_sql, DrpAttenSection, "SectionName", "Id");
            DrpAttenSection.Items.Insert(0, "<--Select-->");
            //DrpAttenSection.SelectedIndex = 0;
        }
        protected void DrpAttenSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            //oo.MessageBox(DrpAtteClass.SelectedValue + "/" + DrpAttenSection.SelectedValue, this.Page);
            _sql = "select SubjectName,Id from Subject_names where classid='" + DrpAtteClass.SelectedValue + "' and sectionName='" + DrpAttenSection.SelectedValue + "' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            _oo.FillDropDown_withValue(_sql, DropDownList2, "SubjectName", "Id");
            DropDownList2.Items.Insert(0, "<--Select-->");
        }
        protected void Button1_Click(object sender, EventArgs e)
        { 
            if (DrpAtteClass.SelectedItem.Text != "<--Select-->")
            {
                //lblmess.Text = "Insert";
                if (DrpAttenSection.SelectedItem.Text != "<--Select-->")
                {
                    if (DropDownList2.SelectedItem.Text != "<--Select-->")
                    {
                        DailyAttendanceRadio();
                        _sql = "";
                        Grd.DataSource = _oo.GridFill(_sql);
                        Grd.DataBind();
                    }
                    else
                    {
                        //oo.MessageBox("Please Select Subject First!", this.Page);
                        Campus camp = new Campus(); camp.msgbox(Page, msgbox1, "Please Select Subject First!", "A");

                    }
                }
                else
                {
                    //oo.MessageBox("Please Select Section First!", this.Page);
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox1, "Please Select Section First!", "A");

                }
            }
            else
            {
                //oo.MessageBox("Please Select Class First!", this.Page);
                Campus camp = new Campus(); camp.msgbox(Page, msgbox1, "Please Select Class First!", "A");

            }
        }
                
        public void DailyAttendanceRadio()
        {
            // ReSharper disable once LocalVariableHidesMember
            // ReSharper disable once RedundantAssignment
            string dd = "";
            if (DrpDin.SelectedItem.ToString().Length == 1)
            {
                dd = "0" + DrpDin.SelectedItem;
            }
            else
            {
                dd = DrpDin.SelectedItem.ToString();
            }


            _sql = "select count(*) as Counter from AttendanceSubjectwise where ClassName='" + DrpAtteClass.SelectedValue + "' and SectionName='" + DrpAttenSection.SelectedValue + "' and subjectid='" + DropDownList2.SelectedValue + "' and convert(nvarchar,AttendanceDate,106)='" + dd + " " + DrpMahina.SelectedItem + " " + DrpSaal.SelectedItem + "' and sessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            
            bool flag = false;
            for (int a = 0; a < Grd.Rows.Count; a++)//State Wise
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "AttendanceDetails_subjectwise_UpdateProc";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = _con;
                    // ReSharper disable once UnusedVariable
                    Label lEnroll = (Label)Grd.Rows[a].Cells[0].FindControl("Label1");
                    // ReSharper disable once UnusedVariable
                    Label Lsrno = (Label)Grd.Rows[a].Cells[1].FindControl("Label2");
                    // ReSharper disable once UnusedVariable
                    Label firstname = (Label)Grd.Rows[a].Cells[2].FindControl("Label3");
                    Label label11 = (Label)Grd.Rows[a].FindControl("Label11");
                    // Label Lastname = (Label)Grd.Rows[a].Cells[2].FindControl("Label9");
                    DropDownList drp1 = (DropDownList)Grd.Rows[a].Cells[3].FindControl("DropDownList1");
                    String date  = DrpSaal.SelectedItem + "/" + DrpMahina.SelectedItem + "/" + DrpDin.SelectedItem;
                    cmd.Parameters.AddWithValue("@AttendanceDate", date);
                    
                    cmd.Parameters.AddWithValue("@AttendanceValue", drp1.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Id", label11.Text);
                    
                    try
                    {

                        _con.Open();
                        cmd.ExecuteNonQuery();
                        _con.Close();
                        flag = true;
                        _oo.ClearControls(Page);


                        if (drp1.SelectedItem.ToString() == "A")
                        {
                           
                        }
                    }

                    catch (Exception)
                    {
                        // ignored
                    }
                }
                //}

            }
            if (flag)
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox1, "Updated successfully.", "S");
            }
        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public override void Dispose()
        {
            _con.Dispose();
            _oo.Dispose();
        }
    }
}