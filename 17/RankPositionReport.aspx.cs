using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;
using c4SmsNew;
using System.Net.Mail;
using System.Collections.Generic;

public partial class RankPositionReport : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";
    DataTable dt;
    protected void Page_PreInIt(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null) { Response.Redirect("~/default.aspx"); }
        if (Session["Logintype"].ToString() == "Admin")
        {
            this.MasterPageFile = "~/Master/admin_root-manager.master";
        }
        else if (Session["Logintype"].ToString() == "Staff")
        {
            this.MasterPageFile = "~/Staff/staff_root-manager.master";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);
        BLL.BLLInstance.LoadHeader("Report", header1);
        if (!IsPostBack)
        {
            LoadClass();
            DrpAttenSection.Items.Insert(0, new ListItem("<--Select-->", ""));
            drpBranch.Items.Insert(0, new ListItem("<--Select-->", ""));
            drpStream.Items.Insert(0, new ListItem("<--Select-->", ""));
        }
    }
    protected void LoadBranch()
    {
        sql = "Select BranchName,Id from BranchMaster where ClassId=" + DrpAtteClass.SelectedValue;
        sql +=  " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"] + "";
        oo.FillDropDown_withValue(sql, drpBranch, "BranchName", "Id");
        drpBranch.Items.Insert(0, new ListItem("<--Select-->", ""));
    }

    private void LoadStream()
    {
        sql = "Select Stream,Id from StreamMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"].ToString() + " and ClassId='" + DrpAtteClass.SelectedValue + "' and BranchId='" + drpBranch.SelectedValue + "'";
        oo.FillDropDown_withValue_withSelect(sql, drpStream, "Stream", "Id");
        if (drpStream.Items.Count <= 0)
        {
            drpStream.Items.Insert(0, new ListItem("<--Select-->", ""));
        }
    }
    protected void LoadClass()
    {
        sql = "Select ClassName,Id from ClassMaster where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"] + "";
        sql +=  " order by CIDOrder";
        oo.FillDropDown_withValue(sql, DrpAtteClass, "ClassName", "Id");
        DrpAtteClass.Items.Insert(0, new ListItem("<--Select-->", ""));
    }
    protected void DrpAtteClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        BLL.BLLInstance.loadSection(DrpAttenSection, Session["SessionName"].ToString(), DrpAtteClass.SelectedValue);
        LoadBranch();
    }
    protected void drpBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadStream();
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        if (DrpAtteClass.SelectedItem.Text == "")
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please Select Class!", "A");
        }
        else
        {
            sql +=  " select asr.SrNo, asr.ClassId, asr.CombineClassName, asr.ClassName, asr.Name, asr.FatherName, asr.SessionName, asr.TypeOFAdmision, asr.DateOfAdmiission, asr.StreamId, asr.BranchId ";
            sql +=  " from AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "', " + Session["BranchCode"].ToString() + ") asr where asr.ClassId = " + DrpAtteClass.SelectedValue.Trim() + " and asr.SectionName ='" + DrpAttenSection.SelectedItem.Text.Trim() + "' ";
            if (DrpAtteClass.SelectedIndex == 15 || DrpAtteClass.SelectedIndex == 16)
            {
                sql +=  " and BranchId "+drpBranch.SelectedValue+ "  and StreamId " + drpStream.SelectedValue + " ";
            }
            sql +=  " and Withdrwal is null and Promotion is null ";

            var ds = oo.GridFill(sql);
            if (ds!=null && ds.Tables.Count > 0)
            {
                Grd.DataSource = ds;
                Grd.DataBind();
                ImageButton1.Visible = true;
                ImageButton2.Visible = true;
                ImageButton3.Visible = true;
                ImageButton4.Visible = true;
                abc.Visible = true;
                for (int i = 0; i < Grd.Rows.Count; i++)
                {
                    Label lblsrno = (Label)Grd.Rows[i].FindControl("lblsrno");
                    Label T1Rank = (Label)Grd.Rows[i].FindControl("T1Rank");
                    Label T2Rank = (Label)Grd.Rows[i].FindControl("T2Rank");
                    Label TotalRank = (Label)Grd.Rows[i].FindControl("TotalRank");
                    Label T1Position = (Label)Grd.Rows[i].FindControl("T1Position");
                    Label T2Position = (Label)Grd.Rows[i].FindControl("T2Position");
                    Label TotalPosition = (Label)Grd.Rows[i].FindControl("TotalPosition");
                    using (SqlConnection conn = new SqlConnection())
                    {
                        conn.ConnectionString = BAL.objBal.dbGet_connection().ConnectionString;
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.Connection = conn;
                            if (DrpAtteClass.SelectedIndex>0 || DrpAtteClass.SelectedIndex <=3)
                            {
                                cmd.CommandText = "calculateRank_NurtoPrep_2021";
                            }
                            if (DrpAtteClass.SelectedIndex >= 4 && DrpAtteClass.SelectedIndex <= 8)
                            {
                                cmd.CommandText = "calculateRank_ItoV_2021";
                            }
                            if (DrpAtteClass.SelectedIndex >= 9 && DrpAtteClass.SelectedIndex <= 11)
                            {
                                cmd.CommandText = "calculateRank_VItoVIII_2021";
                            }
                            if (DrpAtteClass.SelectedIndex == 13 || DrpAtteClass.SelectedIndex == 14)
                            {
                                cmd.CommandText = "calculateRank_IXtoX_2021";
                            }
                            if (DrpAtteClass.SelectedIndex == 15)
                            {
                                cmd.CommandText = "calculateRank_XI_2021";
                            }
                            if (DrpAtteClass.SelectedIndex == 16)
                            {
                                cmd.CommandText = "calculateRank_XII_2021";
                            }
                            cmd.CommandType = CommandType.StoredProcedure;

                            if (DrpAtteClass.SelectedIndex == 15 || DrpAtteClass.SelectedIndex == 16)
                            {
                                cmd.Parameters.AddWithValue("@BranchId", drpBranch.SelectedValue.Trim());
                                cmd.Parameters.AddWithValue("@StreamId", drpStream.SelectedValue.Trim());
                            }
                            if (DrpAtteClass.SelectedIndex == 13 || DrpAtteClass.SelectedIndex == 14)
                            {
                                cmd.Parameters.AddWithValue("@ClassName", DrpAtteClass.SelectedItem.Text.Trim()); 
                            }
                            cmd.Parameters.AddWithValue("@sessionName", Session["SessionName"].ToString());
                            cmd.Parameters.AddWithValue("@SrNo", lblsrno.Text.Trim());
                            cmd.Parameters.AddWithValue("@SectionName", DrpAttenSection.SelectedItem.Text.Trim());
                            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"]);
                            cmd.Parameters.AddWithValue("@ClassId", DrpAtteClass.SelectedValue.Trim());

                            SqlDataAdapter daRank = new SqlDataAdapter(cmd);
                            DataSet dsRank = new DataSet();
                            daRank.Fill(dsRank);
                            cmd.Parameters.Clear();
                            if (dsRank!=null)
                            {
                                T1Rank.Text = (dsRank.Tables[0].Rows.Count > 0 ? dsRank.Tables[0].Rows[0]["ranks"].ToString() : "-").ToString();
                                T1Position.Text = (dsRank.Tables[1].Rows.Count > 0 ? dsRank.Tables[1].Rows[0]["position"].ToString().Trim() : "-").ToString();
                                T2Rank.Text = (dsRank.Tables[2].Rows.Count > 0 ? dsRank.Tables[2].Rows[0]["ranks"].ToString().Trim() : "-").ToString();
                                T2Position.Text = (dsRank.Tables[3].Rows.Count > 0 ? dsRank.Tables[3].Rows[0]["position"].ToString().Trim() : "-").ToString();
                                TotalRank.Text = (dsRank.Tables[4].Rows.Count > 0 ? dsRank.Tables[4].Rows[0]["ranks"].ToString().Trim() : "-").ToString();
                                TotalPosition.Text = (dsRank.Tables[5].Rows.Count > 0 ? dsRank.Tables[5].Rows[0]["position"].ToString().Trim() : "-").ToString();
                            }
                        }
                    }
                }
            }
            else
            {
                Grd.DataSource = null;
                Grd.DataBind();
                ImageButton1.Visible = false;
                ImageButton2.Visible = false;
                ImageButton3.Visible = false;
                ImageButton4.Visible = false;
                abc.Visible = false;

            }

            
            
            
        }
    }

    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        oo.ExportToWord(Response, "StudentAttendanceTillDate.doc", gdv1);
    }

    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        oo.ExportToExcel("StudentAttendanceTillDate.xls", Grd);
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {

    }

    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = abc;
        if (Grd.Rows.Count > 0)
        {
            Grd.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        ScriptManager.RegisterClientScriptBlock(ImageButton4, GetType(), "onclick", "var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}", true);
    }

}

