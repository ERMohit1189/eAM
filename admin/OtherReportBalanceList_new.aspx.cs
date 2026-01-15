using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;


public partial class admin_OtherReportBalanceList : Page
{
    SqlConnection con = new SqlConnection();
    SqlConnection con1 = new SqlConnection();
    Campus oo = new Campus();
    string sql = string.Empty;


    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        con = oo.dbGet_connection();
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        if (!IsPostBack)
        {
            Drp_fill();
            //Display_Other_collectiobBalanceList();
        }

    }


    public void Drp_fill()
    {
        sql = "select ClassName,Id from ClassMaster where SessionName='" + Session["SessionName"].ToString() + "' order By CIDOrder";
        oo.FillDropDown_withValue(sql,drpClass,"ClassName","Id");
        drpClass.Items.Insert(0,"<--Select-->");
        drpClass.Items.Insert(1, "Select ALL");
    }


    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        if (Grd.Rows.Count > 0)
        {
            oo.ExportToWord(Response, "AdmisionFormCollection.doc", divExport);
        }
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        if (Grd.Rows.Count > 0)
        {
            oo.ExportToExcel("AdmisionFormCollection.xls", Grd);
        }
    }
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
    {
        PrintHelper_New.ctrl = abc;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }

    public void Display_Other_collectiobBalanceList_allClass()
    {
        double sum = 0;
        int i;
        sql = "select SO.SrNo  from StudentOfficialDetails SO left join ClassMaster CM on SO.AdmissionForClassId=CM.Id where SO.SessionName='" + Session["SessionName"].ToString() + "' and SO.Withdrwal is Null except  ";
        sql = sql + "select oc.Srno  from Other_fee_Collection oc where oc.SessionName='" + Session["SessionName"].ToString() + "' Order by so.SrNo"; 
        Grd.DataSource = oo.GridFill(sql);
        Grd.DataBind();

        for (i = 0; i <= Grd.Rows.Count - 1; i++)
        {
            Label lblSrNo = (Label)Grd.Rows[i].FindControl("lblSrNo");


            sql = "select sf.FatherContactNo as FatherContactNo,SG.Id, SC.SectionName,SO.Card,SO.Medium as Medium,CM.ClassName,convert(nvarchar,So.DateOfAdmiission,106) as DateOfAdmiission ,SO.SectionId,Sf.FatherName,SF.MotherName,SG.FirstName,SG.MiddleName,SG.LastName,sg.StEnRCode as StEnRCode,sg.srno  as srno,case  when so.TransportRequired='Yes' then 'Yes' else 'No' end as TransportRequired,so.wayamount as wayamount from StudentGenaralDetail SG";
            sql = sql + "    left join StudentFamilyDetails SF on SG.StEnRCode=SF.StEnRCode";
            sql = sql + "   left join StudentOfficialDetails SO on SG.StEnRCode=SO.StEnRCode";
            sql = sql + "   left join ClassMaster CM on SO.AdmissionForClassId=CM.Id";
            sql = sql + "   left join SectionMaster SC on SO.SectionId=SC.Id";
            sql = sql + "    where  So.srno =" + "'" + lblSrNo.Text.Trim() + "'";
            sql = sql + " and sg.SessionName='" + Session["SessionName"].ToString() + "' and ";
            sql = sql + "     so.SessionName='" + Session["SessionName"].ToString() + "' and sf.SessionName='" + Session["SessionName"].ToString() + "' and cm.SessionName='" + Session["SessionName"].ToString() + "'";
            sql = sql + "    and SC.SessionName='" + Session["SessionName"].ToString() + "'  and";
            sql = sql + " sg.BranchCode=" + Session["BranchCode"].ToString() + "";
            sql = sql + "   and SO.Withdrwal is null";

            Label lblStudnetName = (Label)Grd.Rows[i].FindControl("lblStudnetName");
            Label lblFName = (Label)Grd.Rows[i].FindControl("lblFName");
            Label lblClass = (Label)Grd.Rows[i].FindControl("lblClass");
            Label lblSection = (Label)Grd.Rows[i].FindControl("lblSection");
            Label lblMobileno = (Label)Grd.Rows[i].FindControl("lblMobileno");
            Label lblSno = (Label)Grd.Rows[i].FindControl("lblSno");

            lblStudnetName.Text = oo.ReturnTag(sql, "FirstName") + " " + oo.ReturnTag(sql, "MiddleName") + " " + oo.ReturnTag(sql, "LastName");
            lblFName.Text = oo.ReturnTag(sql, "FatherName");
            lblClass.Text = oo.ReturnTag(sql, "ClassName");
            lblSection.Text = oo.ReturnTag(sql, "SectionName");
            lblMobileno.Text = oo.ReturnTag(sql, "FatherContactNo");
            lblSno.Text = (i + 1).ToString();
            sum = sum + 500;

            //if (Grd.Rows[i].Cells[1].Text == "")
            //{
            //    Grd.Rows[i].Dispose();
            //}

        }
        if (Grd.Rows.Count > 0)
        {
            Label lblBalanceAmtTotal = (Label)Grd.FooterRow.FindControl("lblBalanceAmtTotal");
            lblBalanceAmtTotal.Text = "Total Balance" + sum.ToString();
            lblTitle.Text = "Other Fees Balance Report";
            lblDate.Text = oo.CurrentDate();
        }
    }



    public void Display_Other_collectiobBalanceList()
    {
        double sum = 0;
        int i;
        sql = "select SrNo  from StudentOfficialDetails where SessionName='" + Session["SessionName"].ToString() + "' and AdmissionForClassId='" + drpClass.SelectedValue + "' and Withdrwal is Null except  ";
        sql = sql + "select Srno  from Other_fee_Collection where SessionName='" + Session["SessionName"].ToString() + "'"; 

        Grd.DataSource = oo.GridFill(sql);
        Grd.DataBind();

        for (i = 0; i <= Grd.Rows.Count - 1; i++)
        {
            Label lblSrNo = (Label)Grd.Rows[i].FindControl("lblSrNo");


            sql = "select sf.FatherContactNo as FatherContactNo,SG.Id, SC.SectionName,SO.Card,SO.Medium as Medium,CM.ClassName,convert(nvarchar,So.DateOfAdmiission,106) as DateOfAdmiission ,SO.SectionId,Sf.FatherName,SF.MotherName,SG.FirstName,SG.MiddleName,SG.LastName,sg.StEnRCode as StEnRCode,sg.srno  as srno,case  when so.TransportRequired='Yes' then 'Yes' else 'No' end as TransportRequired,so.wayamount as wayamount from StudentGenaralDetail SG";
            sql = sql + "    left join StudentFamilyDetails SF on SG.StEnRCode=SF.StEnRCode";
            sql = sql + "   left join StudentOfficialDetails SO on SG.StEnRCode=SO.StEnRCode";
            sql = sql + "   left join ClassMaster CM on SO.AdmissionForClassId=CM.Id";
            sql = sql + "   left join SectionMaster SC on SO.SectionId=SC.Id";
            sql = sql + "    where  So.srno =" + "'" + lblSrNo.Text.Trim() + "'";
            sql = sql + " and sg.SessionName='" + Session["SessionName"].ToString() + "' and ";
            sql = sql + "     so.SessionName='" + Session["SessionName"].ToString() + "' and sf.SessionName='" + Session["SessionName"].ToString() + "' and cm.SessionName='" + Session["SessionName"].ToString() + "'";
            sql = sql + "    and SC.SessionName='" + Session["SessionName"].ToString() + "'  and";
            sql = sql + " sg.BranchCode=" + Session["BranchCode"].ToString() + "";
            sql = sql + "   and SO.Withdrwal is null";

            Label lblStudnetName = (Label)Grd.Rows[i].FindControl("lblStudnetName");
            Label lblFName = (Label)Grd.Rows[i].FindControl("lblFName");
            Label lblClass = (Label)Grd.Rows[i].FindControl("lblClass");
            Label lblSection = (Label)Grd.Rows[i].FindControl("lblSection");
            Label lblMobileno = (Label)Grd.Rows[i].FindControl("lblMobileno");
            Label lblSno = (Label)Grd.Rows[i].FindControl("lblSno");

            lblStudnetName.Text = oo.ReturnTag(sql, "FirstName") + " " + oo.ReturnTag(sql, "MiddleName") + " " + oo.ReturnTag(sql, "LastName");
            lblFName.Text = oo.ReturnTag(sql, "FatherName");
            lblClass.Text = oo.ReturnTag(sql, "ClassName");
            lblSection.Text = oo.ReturnTag(sql, "SectionName");
            lblMobileno.Text = oo.ReturnTag(sql, "FatherContactNo");
            lblSno.Text = (i + 1).ToString();
            sum = sum + 500;
            //if (Grd.Rows[i].Cells[1].Text == "")
            //{
            //    oo.MessageBox(Grd.Rows[i].Cells[1].Text,this.Page);
            //    Grd.Rows[i].Visible = false;
            //}

        }
        if (Grd.Rows.Count > 0)
        {
            Label lblBalanceAmtTotal = (Label)Grd.FooterRow.FindControl("lblBalanceAmtTotal");
            lblBalanceAmtTotal.Text = "Total Balance" + sum.ToString();
            lblTitle.Text = "Other Fees Balance Report";
            lblDate.Text = oo.CurrentDate();
        }
    }
    //protected void drpClass_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if(drpClass.SelectedItem.Text!="<--Select-->")
    //    {
    //        Display_Other_collectiobBalanceList();
    //    }
    //    else{
    //    oo.MessageBox("Please Select The Class",this.Page);
    //    }
    //}
   
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        if (drpClass.SelectedItem.Text == "<--Select-->")
        {
            oo.MessageBox("Please Select The Class", this.Page);

        }
        else if (drpClass.SelectedItem.Text == "Select ALL")
        {
            Display_Other_collectiobBalanceList_allClass();
        }
        else
        {
            Display_Other_collectiobBalanceList();
        }
    }
}