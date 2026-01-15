using System;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_FeeChart : Page
{
    string sql = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file

        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        BLL.BLLInstance.LoadHeader("Report", header);
        if(!IsPostBack)
        {
            BLL.BLLInstance.loadFeeGroup(drpFeeGroup, Session["SessionName"].ToString());
            BLL.BLLInstance.loadMedium(drpMedium, Session["SessionName"].ToString());
        }
    }

    private void SetHeader()
    {
        Label lblheader = (Label)rptClasswithBranch.Controls[0].FindControl("lblheader");
        Label lblAdmitionType = (Label)rptClasswithBranch.Controls[0].FindControl("lblAdmitionType");
        Label lblmedium = (Label)rptClasswithBranch.Controls[0].FindControl("lblmedium");
 
        lblheader.Text = "Fee Structure of Session " + Session["SessionName"].ToString();
        lblAdmitionType.Text =(drpAdmissionType.SelectedIndex==0?"All": drpAdmissionType.SelectedItem.Text.Trim());
        lblmedium.Text = drpMedium.SelectedItem.Text.Trim();
    }

    private void loadClass()
    {
        sql = "Select ClassName+' '+Case When IsDisplay=1 Then BranchName else '' end Class,ClassName,cm.Id as Classid,bm.Id as BranchId from ClassMaster cm";
        sql = sql + " inner join BranchMaster bm on bm.Classid=cm.Id and cm.SessionName=bm.SessionName and cm.BranchCode=bm.BranchCode where cm.SessionName='" + Session["SessionName"].ToString()+ "' and cm.BranchCode=" + Session["BranchCode"].ToString() + "  order by cm.CIDOrder asc";

        rptClasswithBranch.DataSource = BAL.objBal.GridFill(sql);
        rptClasswithBranch.DataBind();
        if (rptClasswithBranch.Items.Count > 0)
        {
            divEx.Visible = true;
            divExport.Visible = true;
        }
        else
        {
            divEx.Visible = false;
            divExport.Visible = false;
        }
    }

    private void loadInsttalment()
    {
        for (int i = 0; i < rptClasswithBranch.Items.Count; i++)
        {
            Repeater rptInsttalment = (Repeater)rptClasswithBranch.Items[i].FindControl("rptInsttalment");
            Label lblClassid = (Label)rptClasswithBranch.Items[i].FindControl("lblclassid");
            Label lblclassname = (Label)rptClasswithBranch.Items[i].FindControl("lblclassname");
            Label lblbranchid = (Label)rptClasswithBranch.Items[i].FindControl("lblbranchid");


            sql = "Select * from MonthMaster where ClassId='" + lblClassid.Text + "' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and (CardType='" + drpFeeGroup.SelectedValue.ToString() + "' or CardType='" + drpFeeGroup.SelectedItem.Text.ToString() + "') order by monthid";
            rptInsttalment.DataSource = BAL.objBal.GridFill(sql);
            rptInsttalment.DataBind();

            loadFeeHeadWithAmount(rptInsttalment, lblclassname.Text, lblClassid.Text, lblbranchid.Text, drpAdmissionType.SelectedItem.Text.Trim());

            Label lblGrandTotalFee = (Label)rptInsttalment.Controls[rptInsttalment.Controls.Count - 1].Controls[0].FindControl("lblGrandTotalFee");
            
            sql = "Select Sum(FeeAmount) FeePayment from FeeAllotedForClassWise where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " ";
            sql = sql + " and Branchid='" + lblbranchid.Text + "' and Medium='" + drpMedium.SelectedItem.Text.Trim() + "'  and AdmissionType= case when '" + drpAdmissionType.SelectedItem.Text.Trim() + "'='<--All-->' then AdmissionType else '" + drpAdmissionType.SelectedItem.Text.Trim() + "' end and CardType='" + drpFeeGroup.SelectedValue.ToString() + "'";

            lblGrandTotalFee.Text = "Grand Total: " + (BAL.objBal.ReturnTag(sql, "FeePayment") == "" ? "0.00" : BAL.objBal.ReturnTag(sql, "FeePayment"));
        }
    }

    private void loadFeeHeadWithAmount(Repeater rptInsttalment,string classname,string classid,string branchid,string admissiontype)
    {
       
        for (int i = 0; i < rptInsttalment.Items.Count; i++)
        {
            Repeater rptFeeHeadWithAmount = (Repeater)rptInsttalment.Items[i].FindControl("rptFeeHeadWithAmount");
            Label lblMonthName = (Label)rptInsttalment.Items[i].FindControl("lblMonthName");
            Label Monthid = (Label)rptInsttalment.Items[i].FindControl("Monthid");


            sql = "Select fh.FeeHead FeeType,cws.FeeAmount FeePayment, cws.AdmissionType from FeeAllotedForClassWise cws inner join FeeHeadMaster fh on cws.FeeHeadid=fh.id and cws.BranchCode=fh.BranchCode where cws.SessionName='" + Session["SessionName"].ToString() + "' and cws.BranchCode=" + Session["BranchCode"].ToString() + " and cws.ClassId=" + classid + "";
            sql = sql + " and cws.Branchid='" + branchid + "' and cws.Monthid='" + Monthid.Text + "' and cws.AdmissionType= case when '" + admissiontype + "'='<--All-->' then cws.AdmissionType else '" + admissiontype + "' end and cws.Medium='" + drpMedium.SelectedItem.Text.Trim() + "' and cws.CardType='" + drpFeeGroup.SelectedValue.ToString() + "' order by AdmissionType asc";
            rptFeeHeadWithAmount.DataSource = BAL.objBal.GridFill(sql);
            rptFeeHeadWithAmount.DataBind();
            Label lblTotalFee = (Label)rptFeeHeadWithAmount.Controls[rptFeeHeadWithAmount.Controls.Count - 1].Controls[0].FindControl("lblTotalFee");
       
            sql = "Select Sum(FeeAmount) FeePayment from FeeAllotedForClassWise where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and ClassId=" + classid + "";
            sql = sql + " and Branchid='" + branchid + "' and Monthid='" + Monthid.Text + "' and AdmissionType= case when '" + admissiontype + "'='<--All-->' then AdmissionType else '" + admissiontype + "' end and Medium='" + drpMedium.SelectedItem.Text.Trim() + "' and CardType='" + drpFeeGroup.SelectedValue.ToString() + "'";

            lblTotalFee.Text = "Total: "+(BAL.objBal.ReturnTag(sql, "FeePayment")==""?"0.00": BAL.objBal.ReturnTag(sql, "FeePayment"));
            
        }

       
    }

    protected void lnkShow_Click(object sender, EventArgs e)
    {
        loadClass();
        loadInsttalment();
        SetHeader();
    }

    protected void ImageButton1_Click(object sender, EventArgs e)
    {     
        BAL.objBal.ExportTolandscapeWord(Response, "FeeStructure_" + Session["SessionName"].ToString(), divExport);       
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        BAL.objBal.ExportDivToExcelWithFormatting(Response, "FeeStructure_" + Session["SessionName"].ToString(), divExport);        
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {
        BAL.objBal.ExporttolandscapePdf(Response, "FeeStructure_" + Session["SessionName"].ToString(), divExport);
    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = divExport;
        ScriptManager.RegisterClientScriptBlock(ImageButton4,this.GetType(), "onclick", "var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}",true);
    }
}