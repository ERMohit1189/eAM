using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;
public partial class Period_master : Page
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
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        con = oo.dbGet_connection();
        BLL.BLLInstance.LoadHeader("Report", header);
        //con.ConnectionString = ConfigurationManager.ConnectionStrings["Menu"].ConnectionString;
        if (!IsPostBack)
        {
            sql = "Select ClassName from ClassMaster";
            sql = sql + " where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            sql = sql + "  order by Id";
            oo.FillDropDownWithOutSelect(sql, drpClass, "ClassName");


            string ss = "";
            sql = "select Id from ClassMaster where  ClassName='" + drpClass.SelectedItem.ToString() + "'";
            sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            ss = oo.ReturnTag(sql, "Id");
            sql = "Select SectionName from SectionMaster where ClassNameId=" + ss;
            sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            oo.FillDropDown(sql, drpsection, "SectionName");


            ImageButton1.Visible = false;
            ImageButton2.Visible = false;
            ImageButton3.Visible = false;
            ImageButton4.Visible = false;
            header.Visible = false;

        }




    }
    protected void txtremark_TextChanged(object sender, EventArgs e)
    {

    }
    protected void drpClass_SelectedIndexChanged(object sender, EventArgs e)
   {
       string ss = "";
       sql = "select Id from ClassMaster where  ClassName='" + drpClass.SelectedItem.ToString() + "'";
       sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
       ss = oo.ReturnTag(sql, "Id");
       sql = "Select SectionName from SectionMaster where ClassNameId=" + ss;
       sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
       oo.FillDropDown(sql, drpsection, "SectionName");




    //    sql = "select ROW_NUMBER() OVER (ORDER BY Id ASC) AS SrNo, Id, FacultyName,FacultyEmpId,SubjectAlloted,Class,Section,Medium,grp from TeacherSubjectAllotment";

    //    sql = sql + "  where Class='" + drpClass.SelectedItem.ToString() + "' and Section='" + drpsection.SelectedItem.ToString() + "' and  Medium='" + drpmedium.SelectedItem.ToString() + "' and grp='" + drpgroup.SelectedItem.ToString() + "' and Season='" + RadioButtonList1.SelectedItem.ToString() + "'";
    //    sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

    //    GridView1.DataSource = oo.GridFill(sql);
    //    GridView1.DataBind();
    //    LinkButton4.Visible = true;


    }

   

   
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        string sql1 = "";

        sql = "select ROW_NUMBER() OVER (ORDER BY Id ASC) AS SrNo, Id, FacultyName,FacultyEmpId,Subjectname as SubjectAlloted,Class,Section,Medium,grp ,totiming,fromtiming,Modeofsubject,Period,Season,days,PaperTypes  from Period_Master";
        sql = sql + "  where Class='" + drpClass.SelectedItem.ToString() + "' and Section='" + drpsection.SelectedItem.ToString() + "' and  Medium='" + drpmedium.SelectedItem.ToString() + "' and grp='" + drpgroup.SelectedItem.ToString() + "' and Season='" + RadioButtonList1.SelectedItem.ToString() + "'";
        sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and FacultyEmpId!='None'";

      
            GridView2.DataSource = oo.GridFill(sql);
            GridView2.DataBind();
            header.Visible = true;
            ImageButton1.Visible = true;
            ImageButton2.Visible = true;
            ImageButton3.Visible = true;
            ImageButton4.Visible = true;
            if (GridView2.Rows.Count == 0)
            {
                oo.MessageBox("Sorry, No Record(s) found!", this.Page);
                header.Visible = false;
                ImageButton1.Visible = false;
                ImageButton2.Visible = false;
                ImageButton3.Visible = false;
                ImageButton4.Visible = false;
            }
            else
            {
                sql1 = "Select  ROW_NUMBER() OVER (ORDER BY Id ASC) AS SrNo,Id,Period,fromtiming,totiming from Period_Master";
                sql1 = sql1 + " where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
                sql1 = sql1 + "  and Class='" + drpClass.SelectedItem.ToString() + "' and Section='" + drpsection.SelectedItem.ToString() + "' and  Medium='" + drpmedium.SelectedItem.ToString() + "' and grp='" + drpgroup.SelectedItem.ToString() + "' and Season='" + RadioButtonList1.SelectedItem.ToString() + "' and Period ='lunch Time'";

                try
                {
                    Label Label36 = (Label)GridView2.FooterRow.FindControl("Label33");
                    Label36.Text = oo.ReturnTag(sql1, "fromtiming");
                    Label Label37 = (Label)GridView2.FooterRow.FindControl("Label34");
                    Label37.Text = oo.ReturnTag(sql1, "totiming");



                }
                catch (Exception) { }

            }



    }
    protected void drpmedium_SelectedIndexChanged(object sender, EventArgs e)
    {


        //sql = "select ROW_NUMBER() OVER (ORDER BY Id ASC) AS SrNo, Id, FacultyName,FacultyEmpId,SubjectAlloted,Class,Section,Medium,grp from TeacherSubjectAllotment";

        //sql = sql + "  where Class='" + drpClass.SelectedItem.ToString() + "' and Section='" + drpsection.SelectedItem.ToString() + "' and  Medium='" + drpmedium.SelectedItem.ToString() + "' and grp='" + drpgroup.SelectedItem.ToString() + "' and Season='"+RadioButtonList1.SelectedItem.ToString()+"'";
        //sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

        //GridView1.DataSource = oo.GridFill(sql);
        //GridView1.DataBind();
        //LinkButton4.Visible = true;





    }
    protected void drpgroup_SelectedIndexChanged(object sender, EventArgs e)
    {

        //sql = "select ROW_NUMBER() OVER (ORDER BY Id ASC) AS SrNo, Id, FacultyName,FacultyEmpId,SubjectAlloted,Class,Section,Medium,grp from TeacherSubjectAllotment";

        //sql = sql + "  where Class='" + drpClass.SelectedItem.ToString() + "' and Section='" + drpsection.SelectedItem.ToString() + "' and  Medium='" + drpmedium.SelectedItem.ToString() + "' and grp='" + drpgroup.SelectedItem.ToString() + "' and Season='" + RadioButtonList1.SelectedItem.ToString() + "'";
        //sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

        //GridView1.DataSource = oo.GridFill(sql);
        //GridView1.DataBind();
        //LinkButton4.Visible = true;

    }
    protected void drpsection_SelectedIndexChanged(object sender, EventArgs e)
    {
        //sql = "select ROW_NUMBER() OVER (ORDER BY Id ASC) AS SrNo, Id, FacultyName,FacultyEmpId,SubjectAlloted,Class,Section,Medium,grp from TeacherSubjectAllotment";

        //sql = sql + "  where Class='" + drpClass.SelectedItem.ToString() + "' and Section='" + drpsection.SelectedItem.ToString() + "' and  Medium='" + drpmedium.SelectedItem.ToString() + "' and grp='" + drpgroup.SelectedItem.ToString() + "' and Season='" + RadioButtonList1.SelectedItem.ToString() + "'";
        //sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

        //GridView1.DataSource = oo.GridFill(sql);
        //GridView1.DataBind();
        //LinkButton4.Visible = true;

    }
    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void LinkButton4_Click(object sender, EventArgs e)
    {
      

        PrintHelper_New.ctrl = abc;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");



    }


    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {

        //     LinkButton chk = (LinkButton)sender;
        //    string ss = chk.Text;
        //    lblID.Text = ss;
        //    //DropDownList3.Visible = true ;
        //    //Label8.Visible = true ;
        //    // sql = "Select  distinct ROW_NUMBER() OVER (ORDER BY p.ProductId ASC) AS  [ProductId] ,po.id as ID, pc.ProductCategoryName as[ProductCategoryName], p.ProductName as [ProductName], Pm.ProductTypeName as [ProductTypeName],PO.ProductModelName as [ProductModelName] from Productcategorymaster pc left join ProductName p on p.ProductId=pc.ProductId  left join ProductTypeMaster PM on p.ProductId=PM.ProductId left join ProductModelMaster PO on p.ProductId=PO.ProductId ";
        //    sql = "Select  ROW_NUMBER() OVER (ORDER BY Id ASC) AS SrNo,Id,Period,fromtiming,totiming from Period_Master where Id=" + ss;
        //  //  sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        //   // DropDownList DropDownList3 = (DropDownList)GridView1.FindControl("DropDownList3");
        //    DropDownList3.Text = oo.ReturnTag(sql, "Period").Trim();
        //    TextBox6.Text = oo.ReturnTag(sql, "fromtiming").Trim();
        //    TextBox5.Text = oo.ReturnTag(sql, "totiming").Trim();

        //    Panel1_ModalPopupExtender.Show();
        //    DropDownList3.Visible = true;
        //    Label8.Visible = true;
        //}
    }
   protected void Button3_Click(object sender, EventArgs e)
  {



//            SqlCommand cmd = new SqlCommand();
//            cmd.CommandText = "Period_MasterupdateProc";
//            cmd.CommandType = CommandType.StoredProcedure;
//            cmd.Connection = con;
//            string dd = "";

//            cmd.Parameters.AddWithValue("@id", lblID.Text);

//            if (lbllunch.Text != "")
//            {
//                cmd.Parameters.AddWithValue("@Period", lbllunch.Text );
//                lbllunch.Text = "";
//            }
//            else
//            {
//                cmd.Parameters.AddWithValue("@Period", DropDownList3.SelectedItem.ToString());
//            }
        
//            cmd.Parameters.AddWithValue("@fromtiming", TextBox6.Text.ToString());
//            cmd.Parameters.AddWithValue("@totiming", TextBox5.Text.ToString());



//            try
//            {
//                con.Open();
//                cmd.ExecuteNonQuery();
//                con.Close();
//                oo.MessageBox("Updated successfully.", this.Page);
               

//                //sql = "Select ROW_NUMBER() OVER (ORDER BY Id ASC) AS SrNo, Id, Medium,Grp,Class,Section,Season from Period_Master where Section ='" + drpsection.SelectedItem.ToString() + "' and Medium='" + drpmedium.SelectedItem.ToString() + "' and Grp='" + drpgroup.SelectedItem.ToString() + "' and Class='" + drpClass.SelectedItem.ToString() + "' and Season='" + RadioButtonList1.SelectedItem.ToString() + "'";
//                //sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

//                //sql = "select ROW_NUMBER() OVER (ORDER BY Id ASC) AS SrNo, Id, FacultyName,FacultyEmpId,Subjectname as SubjectAlloted,Class,Section,Medium,grp ,totiming,fromtiming,Modeofsubject,Period,Season  from Period_Master";
//                //sql = sql + "  where Class='" + drpClass.SelectedItem.ToString() + "' and Section='" + drpsection.SelectedItem.ToString() + "' and  Medium='" + drpmedium.SelectedItem.ToString() + "' and grp='" + drpgroup.SelectedItem.ToString() + "' and Season='" + RadioButtonList1.SelectedItem.ToString() + "'";
//                //sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and FacultyEmpId!='None'";
//                //GridView1.DataSource = oo.GridFill(sql);
//                //GridView1.DataBind();
//                CheckAllotedUpdate();
//                LinkButton4.Visible = true;


//            }
//            catch (Exception) { con.Close(); }



//    }
   }
       protected void Button4_Click(object sender, EventArgs e)
    {

    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {

    }
    protected void Button8_Click(object sender, EventArgs e)
    {

    }
 
protected void  DropDownList3_SelectedIndexChanged1(object sender, EventArgs e)
{

}
protected void LinkButton6_Click(object sender, EventArgs e)
{

    //LinkButton chk = (LinkButton)sender;
    //string ss = chk.Text;
    //lblID.Text = ss;
    //lbllunch.Text = "Lunch Time";
    //// sql = "Select  distinct ROW_NUMBER() OVER (ORDER BY p.ProductId ASC) AS  [ProductId] ,po.id as ID, pc.ProductCategoryName as[ProductCategoryName], p.ProductName as [ProductName], Pm.ProductTypeName as [ProductTypeName],PO.ProductModelName as [ProductModelName] from Productcategorymaster pc left join ProductName p on p.ProductId=pc.ProductId  left join ProductTypeMaster PM on p.ProductId=PM.ProductId left join ProductModelMaster PO on p.ProductId=PO.ProductId ";
    //sql = "Select  ROW_NUMBER() OVER (ORDER BY Id ASC) AS SrNo,Id,Period,fromtiming,totiming from Period_Master where Id=" + ss;
    //sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

    //DropDownList3.Visible = false;
    //Label8.Visible = false;
    //TextBox6.Text = oo.ReturnTag(sql, "fromtiming");
    //TextBox5.Text = oo.ReturnTag(sql, "totiming");

    //Panel1_ModalPopupExtender.Show();
}
protected void DropDownList3_SelectedIndexChanged2(object sender, EventArgs e)
{

}
protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
{

}
protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
{
    oo.ExportToWord(Response, "AllGeneralEnquiryReport.doc", gdv);
}
protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
{
    oo.ExportToExcel("AllGeneralEnquiryReport.xls", GridView2);
}
protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
{
    PrintHelper_New.ctrl = abc;
    ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
}
}

            