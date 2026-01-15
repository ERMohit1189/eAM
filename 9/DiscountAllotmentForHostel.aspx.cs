using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;

public partial class admin_DiscountAllotmentForHostel : System.Web.UI.Page
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
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader); 

        //con.ConnectionString = ConfigurationManager.ConnectionStrings["Menu"].ConnectionString;
        if (!IsPostBack)
        {

            GrdDiscountDetails.Visible = false;
            Grd.Visible = false;
            GridDisplay();
            Panel3.Visible = false;
        }


    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {

        LinkButton chk = (LinkButton)sender;
        string ss = chk.Text;
        lblID.Text = ss;
        sql = "select DiscountId,DiscountName,DiscountValue,DiscountType,SessionName,BranchCode,SrNo,StEnRCode,RecordDate,Remark from DiscountMasterHostel";
        sql = sql + "  where DiscountId=" + ss;
        sql = sql + "  and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        txtDiscNamePanel.Text = oo.ReturnTag(sql, "DiscountName");
        if (oo.ReturnTag(sql, "DiscountType") == "Percentage")
        {
            RdoAmountTypePanel.Items[0].Selected = true;
            RdoAmountTypePanel.Items[1].Selected = false;
        }
        else
        {
            RdoAmountTypePanel.Items[0].Selected = false;
            RdoAmountTypePanel.Items[1].Selected = true;
        }

        txtDiscountValuePanel.Text = oo.ReturnTag(sql, "DiscountValue");
        txtRemarkPanel.Text = oo.ReturnTag(sql, "Remark");
        Panel1_ModalPopupExtender.Show();
    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        Button8.Focus();
        LinkButton chk = (LinkButton)sender;
        string ss = chk.Text;
        lblvalue.Text = ss.ToString();
        Panel2_ModalPopupExtender.Show();
    }
    public void GridDisplay()
    {
        try
        {


            //Label lblClass = (Label)Grd.Rows[0].FindControl("Label5");



            sql = "select ROW_NUMBER() OVER (ORDER BY dm.DiscountId ASC) AS SNo, dm.DiscountId,dm.DiscountName,dm.DiscountValue,dm.DiscountType,dm.SessionName,dm.BranchCode,dm.SrNo,dm.StEnRCode,dm.Remark, ";
            sql = sql + "  sg.FirstName+' '+sg.MiddleName+' '+sg.LastName as Name ,cm.ClassName as Class,sm.SectionName as Section  from DiscountMasterHostel dm  ";
            sql = sql + "  left join StudentGenaralDetail sg on dm.SrNo=sg.SrNo  ";
            sql = sql + "  left join StudentOfficialDetails sd on sd.SrNo=sg.SrNo  ";
            sql = sql + " left join ClassMaster cm on sd.AdmissionForClassId=cm.Id  ";
            sql = sql + "  left join SectionMaster sm on sd.SectionId=sm.Id  ";
            sql = sql + "  where sg.SessionName='" + Session["SessionName"].ToString() + "' and sg.BranchCode=" + Session["BranchCode"].ToString() + "";
            sql = sql + "   and cm.SessionName='" + Session["SessionName"].ToString() + "'   and cm.BranchCode=" + Session["BranchCode"].ToString() + "  and sm.SessionName='" + Session["SessionName"].ToString() + "'";
            sql = sql + "  and sd.SessionName='" + Session["SessionName"].ToString() + "' and sd.BranchCode=" + Session["BranchCode"].ToString() + "";
            sql = sql + "  and dm.SessionName='" + Session["SessionName"].ToString() + "' and dm.BranchCode=" + Session["BranchCode"].ToString() + "";
            //sql = sql + "  and cm.ClassName='" + lblClass.Text + "'";



            //sql = "select ROW_NUMBER() OVER (ORDER BY DiscountId ASC) AS SNo, DiscountId,DiscountName,DiscountValue,DiscountType,SessionName,BranchCode,SrNo,StEnRCode,RecordDate,Remark from DiscountMaster ";
            GrdDiscountDetails.DataSource = oo.GridFill(sql);
            GrdDiscountDetails.DataBind();
            if (GrdDiscountDetails.Rows.Count > 0)
            {
                GrdDiscountDetails.Visible = true;
                Panel3.Visible = true;

            }
            else
            {
                GrdDiscountDetails.Visible = false;
                Panel3.Visible = false;
                //oo.MessageBox("Sorry! No Record Found.", this.Page);
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry! No Record Found.", "A");       

            }
        }
        catch (Exception) { }
    }
    protected void DrpEnter_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        sql = "delete from DiscountMasterHostel where DiscountId=" + lblvalue.Text;
        sql = sql + "  and sessionName='" + Session["SessionName"].ToString()+ "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        oo.ProcedureDatabase(sql);
        //oo.MessageBox("Deleted successfully.", this.Page);
        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Deleted successfully.", "S");       

        GridDisplay();
    }
    protected void Button8_Click(object sender, EventArgs e)
    {

    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "DiscountMasterHostelUpdateProc";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
        //@DiscountName,@DiscountValue,@DiscountType,@SessionName,@BranchCode,@SrNo,@StEnRCode
        cmd.Parameters.AddWithValue("@DiscountName", txtDiscNamePanel.Text.Trim());
        cmd.Parameters.AddWithValue("@DiscountValue", txtDiscountValuePanel.Text.Trim());
        cmd.Parameters.AddWithValue("@DiscountType", RdoAmountTypePanel.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Remark", txtRemarkPanel.Text);
        cmd.Parameters.AddWithValue("@DiscountId", lblID.Text);
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            GridDisplay();
            oo.ClearControls(this.Page);
            //oo.MessageBox("Updated successfully.", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully.", "S");       

        }
        catch (Exception) { }
    }
    protected void Button4_Click(object sender, EventArgs e)
    {

    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {

        sql = "select DiscountName from DiscountMasterHostel  where " + DrpEnter.SelectedValue.ToString() + "=" + "'" + TxtEnter.Text.Trim() + "'";
        sql = sql + "  and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        if (oo.Duplicate(sql))
        {
            //oo.MessageBox("Duplicate Entry, Could't assign discount to this Student!", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Duplicate Entry, Could't assign discount to this Student!", "A");       

            lblError.Text = "Duplicate Entry, Could't assign discount to this Student!";
        }
        else
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "DiscountMasterHostelProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                //@DiscountName,@DiscountValue,@DiscountType,@SessionName,@BranchCode,@SrNo,@StEnRCode
                cmd.Parameters.AddWithValue("@DiscountName", txtDiscName.Text.Trim());
                cmd.Parameters.AddWithValue("@DiscountValue", txtDiscValue.Text.Trim());
                cmd.Parameters.AddWithValue("@DiscountType", RdoDiscType.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                sql = "select srno,stenrcode from StudentOfficialDetails where " + DrpEnter.SelectedValue.ToString() + "=" + "'" + TxtEnter.Text.Trim() + "'";
                cmd.Parameters.AddWithValue("@SrNo", TxtEnter.Text);
                cmd.Parameters.AddWithValue("@StEnRCode", oo.ReturnTag(sql, "stenrcode"));
                cmd.Parameters.AddWithValue("@Remark", txtRemark.Text);
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    GridDisplay();
                    oo.ClearControls(this.Page);
                    //oo.MessageBox("Submitted successfully.", this.Page);
                    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");       

                }
                catch (Exception) { }
            }

            catch (Exception) { }
        }
    }
    protected void LinkButton7_Click(object sender, EventArgs e)
    {
        sql = "select SG.Id, SC.SectionName,SO.Card,SO.Medium as Medium,CM.ClassName,convert(nvarchar,So.DateOfAdmiission,106) as DateOfAdmiission ,SO.SectionId,Sf.FatherName,SF.MotherName,SG.FirstName,SG.MiddleName,SG.LastName,sg.StEnRCode as StEnRCode,sg.srno  as srno,case  when so.TransportRequired='Yes' then 'Yes' else 'No' end as TransportRequired,so.wayamount as wayamount from StudentGenaralDetail SG ";
        sql = sql + "    left join StudentFamilyDetails SF on SG.StEnRCode=SF.StEnRCode";
        sql = sql + "   left join StudentOfficialDetails SO on SG.StEnRCode=SO.StEnRCode";
        sql = sql + "   left join ClassMaster CM on SO.AdmissionForClassId=CM.Id";
        sql = sql + "   left join SectionMaster SC on SO.SectionId=SC.Id";
        sql = sql + "    where  SG." + DrpEnter.SelectedValue.ToString() + "=" + "'" + TxtEnter.Text.Trim() + "'";
        sql = sql + "  and sg.SessionName='" + Session["SessionName"].ToString() + "' and sg.BranchCode=" + Session["BranchCode"].ToString() + "";
        sql = sql + "   and Sf.SessionName='" + Session["SessionName"].ToString() + "' and sc.BranchCode=" + Session["BranchCode"].ToString() + " and so.BranchCode=" + Session["BranchCode"].ToString() + "  and SO.SessionName='" + Session["SessionName"].ToString() + "'";
        sql = sql + "   and cm.SessionName='" + Session["SessionName"].ToString() + "'  and cm.BranchCode=" + Session["BranchCode"].ToString() + " and sf.BranchCode=" + Session["BranchCode"].ToString() + "   and Sc.SessionName='" + Session["SessionName"].ToString() + "'";
        sql = sql + "   and SO.Withdrwal is null";
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
            Panel3.Visible = false;
            //oo.MessageBox("Sorry! No Record Found.", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry! No Record Found.", "A");       

        }
    }
    protected void TxtEnter_TextChanged(object sender, EventArgs e)
    {

    }
}