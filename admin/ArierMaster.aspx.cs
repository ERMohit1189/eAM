using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;

public partial class admin_ArierMaster : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        //con.ConnectionString = ConfigurationManager.ConnectionStrings["Menu"].ConnectionString;
        con = oo.dbGet_connection();
        if (!IsPostBack)
        {
            sql = "select SessionName from SessionMaster";
            oo.FillDropDown(sql, drpSession, "SessionName");
            oo.FillDropDown(sql, drpSessionPanel, "SessionName");

            oo.AddDateMonthYearDropDown(drpYY, drpMM, drpDD);
            oo.AddDateMonthYearDropDown(drpYYP, drpMMP, drpDDP);

            oo.FindCurrentDateandSetinDropDown(drpYY, drpMM, drpDD);
            oo.FindCurrentDateandSetinDropDown(drpYYP, drpMMP, drpDDP);

            DisplayGrid(); Panel3.Visible = false ;

        }
    }
    protected void txtCaste_TextChanged(object sender, EventArgs e)
    {

    }
    protected void LinkButton7_Click(object sender, EventArgs e)
    {
        sql = "select SG.Id, SC.SectionName,SO.Card,SO.Medium as Medium,CM.ClassName,convert(nvarchar,So.DateOfAdmiission,106) as DateOfAdmiission ,SO.SectionId,Sf.FatherName,SF.MotherName,SG.FirstName,SG.MiddleName,SG.LastName,sg.StEnRCode as StEnRCode,sg.srno  as srno,case  when so.TransportRequired='Yes' then 'Yes' else 'No' end as TransportRequired,so.wayamount as wayamount from StudentGenaralDetail SG ";
        sql = sql + "    left join StudentFamilyDetails SF on SG.StEnRCode=SF.StEnRCode";
        sql = sql + "   left join StudentOfficialDetails SO on SG.StEnRCode=SO.StEnRCode";
        sql = sql + "   left join ClassMaster CM on SO.AdmissionForClassId=CM.Id";
        sql = sql + "   left join SectionMaster SC on SO.SectionId=SC.Id";
        sql = sql + "    where  SG." + DrpEnter.SelectedValue.ToString() + "=" + "'" + TxtEnter.Text.Trim() + "'";
        sql = sql + " and sg.SessionName='" + Session["SessionName"].ToString() + "' and sg.BranchCode=" + Session["BranchCode"].ToString() + "";
        sql = sql + "   and SO.Withdrwal is null";
        sql = sql + "  and so.SessionName='" + Session["SessionName"].ToString() + "' and cm.SessionName='" + Session["SessionName"].ToString() + "'  and sc.SessionName='" + Session["SessionName"].ToString() + "'";
        Grd.DataSource = oo.GridFill(sql);
        Grd.DataBind();
        if (Grd.Rows.Count > 0)
        {
            Grd.Visible = true;
            Panel3.Visible = true;

        }
        else
        {
            Grd.Visible = false;
           oo.MessageBox("Record(s) not found", this.Page);
           Panel3.Visible = false;


        }
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        string ss = chk.Text;
        lblID.Text = ss.ToString();
        sql = "select ArrierId,Srno,StEnRCode,ArrearAmt,ArrierSession,Remark,RecordDate,BranchCode,LoginName,SessionName,left(convert(nvarchar,ArrierDate,106),2) as DD,Right(left(convert(nvarchar,ArrierDate,106),6),3) as MM , RIGHT(convert(nvarchar,ArrierDate,106),4) as YY ";        
        sql=sql+" from ArrierMast  where ArrierId="+lblID.Text;
        txtArearAmtPanel.Text = oo.ReturnTag(sql, "ArrearAmt");
        drpSessionPanel.Text = oo.ReturnTag(sql, "ArrierSession");
        txtRemarkPanel.Text = oo.ReturnTag(sql, "Remark");
        drpDDP.Text =oo.ReadDD( oo.ReturnTag(sql, "DD"));
        drpMMP.Text = oo.ReturnTag(sql, "MM");
        drpYYP.Text = oo.ReturnTag(sql, "YY");

        Panel1_ModalPopupExtender.Show();


    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        string ss = chk.Text;
        lblvalue.Text = ss.ToString();
        Panel2_ModalPopupExtender.Show();

    }
    protected void DrpEnter_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void TxtEnter_TextChanged(object sender, EventArgs e)
    {

    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        string dd="";

        //sql = "select * from ArrierMast where " + DrpEnter.SelectedValue.ToString() + "=" + "'" + TxtEnter.Text.Trim() + "'";
        //sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        //if (oo.Duplicate(sql))
        //{
        //    oo.MessageBox("Duplicate Entry!", this.Page);
        //}
        //else 
        if (drpSession.SelectedItem.ToString() == "<--Select-->")
        {
            oo.MessageBox("Please <--Select--> the Session!", this.Page);
        }
        else
        {


            dd = drpYY.SelectedItem.ToString() + "/" + drpMM.SelectedItem.ToString() + "/" + drpDD.SelectedItem.ToString();
            //@Srno,@RegNo,@ArrearAmt,@ArrierSession,@Remark,GETDATE(),@BranchCode,@LoginName,@SessionName
            sql = "select srno,stenrcode from StudentOfficialDetails where " + DrpEnter.SelectedValue.ToString() + "=" + "'" + TxtEnter.Text.Trim() + "'";
            sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "ArrierMastProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Srno", TxtEnter.Text);
            cmd.Parameters.AddWithValue("@RegNo", oo.ReturnTag(sql, "stenrcode"));
            cmd.Parameters.AddWithValue("@ArrearAmt", txtArrearAmt.Text);
            cmd.Parameters.AddWithValue("@ArrierSession", drpSession.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@Remark", txtRemark.Text);
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
            cmd.Parameters.AddWithValue("@ArrierDate", dd);
            cmd.Connection = con;

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                oo.MessageBox("Submitted successfully.", this.Page);
                DisplayGrid();
            }
            catch (Exception) { }

        }
    }

    public void DisplayGrid()
    {
        sql = "     select ROW_NUMBER() OVER (ORDER BY Am.ArrierId ASC) AS SNo, Am.ArrierId,AM.ArrearAmt,am.ArrierSession,am.SrNo,am.StEnRCode,am.Remark, ";
        sql = sql + "  sg.FirstName+' '+sg.MiddleName+' '+sg.LastName as Name ,cm.ClassName as Class,sm.SectionName as Section,convert(nvarchar,ArrierDate,106)as ArrierDate  from ArrierMast Am  ";
        sql = sql + "  left join StudentGenaralDetail sg on am.SrNo=sg.SrNo  ";
        sql = sql + "  left join StudentOfficialDetails sd on sd.SrNo=sg.SrNo  ";
        sql = sql + " left join ClassMaster cm on sd.AdmissionForClassId=cm.Id  ";
        sql = sql + "  left join SectionMaster sm on sd.SectionId=sm.Id  ";
        sql = sql + " where sg.SessionName='" + Session["SessionName"].ToString() + "' and sg.BranchCode=" + Session["BranchCode"].ToString() + "";
        sql = sql + "  and sd.SessionName='" + Session["SessionName"].ToString() + "'  and cm.SessionName='" + Session["SessionName"].ToString() + "'  and sm.SessionName='" + Session["SessionName"].ToString() + "'";
        GrdDiscountDetails.DataSource = oo.GridFill(sql);
        GrdDiscountDetails.DataBind();

    }


    protected void btnDelete_Click(object sender, EventArgs e)
    {
        sql = "delete from ArrierMast where ArrierId=" + lblvalue.Text;
        sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        oo.ProcedureDatabase(sql);
        oo.MessageBox("Deleted successfully.!", this.Page);
        DisplayGrid();
    }
    protected void Button8_Click(object sender, EventArgs e)
    {

    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        string dd = "";

        dd = drpYYP.SelectedItem.ToString() + "/" + drpMMP.SelectedItem.ToString() + "/" + drpDDP.SelectedItem.ToString();
        //@RegNo nvarchar(50),@ArrearAmt numeric(18,2),@ArrierSession nvarchar(50),@Remark
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "ArrierMastUpdateProc";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ArrierId", lblID.Text);      
        cmd.Parameters.AddWithValue("@ArrearAmt", txtArearAmtPanel.Text);
        cmd.Parameters.AddWithValue("@ArrierSession", drpSessionPanel.SelectedItem.ToString());
        cmd.Parameters.AddWithValue("@Remark", txtRemarkPanel.Text);
        cmd.Parameters.AddWithValue("@ArrierDate", dd);
        cmd.Connection = con;
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            oo.MessageBox("Updated successfully.!", this.Page);
            DisplayGrid();
        }
        catch (Exception) { }


    }
    protected void Button4_Click(object sender, EventArgs e)
    {

    }
}