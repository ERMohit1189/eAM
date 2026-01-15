using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.IO;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;

public partial class Subjectwisereport : Page
{
    private SqlConnection _con;
    private readonly Campus _oo;
    private string _sql, _sql1 = String.Empty;
    public Subjectwisereport()
    {
        _con = new SqlConnection();
        _oo = new Campus();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Session["LoginName"] as string))
        {
            Response.Redirect("~/default.aspx");
        }
        
        _con = _oo.dbGet_connection();
        _oo.LoadLoader(loader);
        Campus camp = new Campus(); camp.LoadLoader(loader);
        if (!IsPostBack)
        {
            loadTerm();
            LoadData();
        }
    }
    private void loadTerm()
    {
        _sql = "select Id, TermName from OT_TermMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " order by id asc";
        _oo.FillDropDown_withValue(_sql, ddlTerm, "TermName", "Id");
        ddlTerm.Items.Insert(0, new ListItem("<--All-->", ""));
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        
        LoadData();
    }
    protected void LoadData()
    {
        _sql = "select ClassId from AllStudentRecord_UDF('" + Session["SessionName"] + "', " + Session["BranchCode"] + ") where SrNo='" + Session["LoginName"].ToString() + "'";
        string classid = _oo.ReturnTag(_sql, "ClassId");
        _sql = "select id, TermName from OT_TermMaster where SessionName='" + Session["SessionName"] + "' and Branchcode=" + Session["BranchCode"] + " and Id=case when '" + ddlTerm.SelectedIndex+ "'='0' then id else '" + ddlTerm.SelectedValue + "' end ";
        _sql = _sql+" and id in (select distinct Termid from OT_ExamAnswerResult where classid=" + classid + " and SrNO='" + Session["LoginName"].ToString() + "' and SessionName='" + Session["SessionName"] + "' and Branchcode=" + Session["BranchCode"] + ")";

        var dt = _oo.Fetchdata(_sql);
        rpter.DataSource = dt;
        rpter.DataBind();
        if (rpter.Items.Count>0)
        {
            for (int i = 0; i < rpter.Items.Count; i++)
            {
                Chart Chart1 = (Chart)rpter.Items[i].FindControl("Chart1");
                Label TermId = (Label)rpter.Items[i].FindControl("lblid");
                GridView Grd = (GridView)rpter.Items[i].FindControl("Grd");
                _sql = "select id, Paper from OT_PaperMaster where SubjectId in(select SubjectId from OT_ExamMaster where TermId=" + TermId.Text + " and classid="+ classid + " and getdate() between ResultShow and ResultHide and SessionName='" + Session["SessionName"] + "' and Branchcode=" + Session["BranchCode"] + ")  and classid=" + classid + "  and SessionName='" + Session["SessionName"] + "' and Branchcode=" + Session["BranchCode"] + "";
                Grd.DataSource = _oo.GridFill(_sql);
                Grd.DataBind();
                string subjects = "";
                string obtainds = "";
                if (Grd.Rows.Count>0)
                {
                    for (int j = 0; j < Grd.Rows.Count; j++)
                    {
                        Label Subject = (Label)Grd.Rows[j].FindControl("Label2");
                        Label subjectId = (Label)Grd.Rows[j].FindControl("SubjectId");
                        Label lblMax = (Label)Grd.Rows[j].FindControl("lblMax");
                        _sql = "select isnull(sum(MaxMarks),0)MaxMarks from OT_AnswerMaster where PaperId=" + subjectId.Text + " and SessionName='" + Session["SessionName"] + "' and Branchcode=" + Session["BranchCode"] + " and classid=" + classid + "";
                        lblMax.Text = _oo.ReturnTag(_sql, "MaxMarks");
                        Label lblObtaind = (Label)Grd.Rows[j].FindControl("lblObtaind");
                        _sql = "select isnull(sum(ObtaindMarks),0)ObtaindMarks from OT_ExamAnswerResult where PaperId=" + subjectId.Text + "  and classid=" + classid + " and SrNO='" + Session["LoginName"].ToString() + "' and SessionName='" + Session["SessionName"] + "' and Branchcode=" + Session["BranchCode"] + "";
                        lblObtaind.Text = _oo.ReturnTag(_sql, "ObtaindMarks");
                        if (j == (Grd.Rows.Count - 1))
                        {
                            subjects = subjects + Subject.Text;
                            obtainds = obtainds + lblObtaind.Text;
                        }
                        else
                        {
                            subjects = subjects + Subject.Text + ",";
                            obtainds = obtainds + lblObtaind.Text + ",";
                        }
                        
                    }

                    double[] yValues = Array.ConvertAll(obtainds.Split(','), double.Parse);
                    string[] xValues = subjects.ToUpper().Split(new string[] { "," }, StringSplitOptions.None);

                    //double[] yValues = {5,7,8,3,9,11,22 };
                    //string[] xValues = { "dsadsa ASASASS", "sad DSGFDSFDSFDSFD", "dsad SADFDFDSDFDS", "dsad SAFDSFDS", "sad DSFDSFDSF", "dsad DSFDSF", "dsad  DSFDF", };

                    Chart1.Series["Default"].Points.DataBindXY(xValues, yValues);
                    Label lblTerm = (Label)rpter.Items[i].FindControl("lblTerm");
                    //Chart1.Series["Default"].Points.FindMaxByValue("5", yValues.Length);

                    Chart1.Titles["Title1"].Text = ("Subject wise Marks Report ("+ lblTerm.Text+")").ToUpper();


                    


                    Chart1.Series["Default"].ChartType = SeriesChartType.Column;

                    Chart1.Series["Default"]["PieLabelStyle"] = "Disabled";

                    Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = false;

                    Chart1.Legends[0].Enabled = false;
                    string[] colours = { "#1FAE66", "#F89C2C", "#E91E63", "#DA4448", "#23709E", "#9C27B0", "#795548", "#9e9e9e", "#ffeb3b" };
                    int sts = 0;
                    for (int k = 0; k < Grd.Rows.Count; k++)
                    {
                        Label lblMax = (Label)Grd.Rows[0].FindControl("lblMax");
                        Label lblMax2 = (Label)Grd.Rows[k].FindControl("lblMax");
                        if (k > 0 && double.Parse(lblMax.Text) != double.Parse(lblMax2.Text))
                        {
                            sts = sts + 1;
                        }
                        Chart1.Series["Default"].Points[k].Color = ColorTranslator.FromHtml(colours[k]);
                    }

                    if (sts > 0)
                    {
                        Chart1.Visible = false;
                    }
                }
                
            }
        }
    }
    public override void Dispose()
    {
        _con.Dispose();
        _oo.Dispose();
    }

    
}