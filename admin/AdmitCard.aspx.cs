using System;
using System.Web.UI.WebControls;

public partial class AdmitCard : System.Web.UI.Page
{
    Campus oo = new Campus();
    string sql = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        Campus camp = new Campus(); camp.LoadLoader(loader); 
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        if (!IsPostBack)
        {
            loadclass();
            loadsection();
            loadgrid();
        }
    }


    protected void drpClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadsection();
        loadgrid();
    }

    public void loadgrid()
    {
        sql = "select SG.Id, UPPER(SC.SectionName) SectionName,UPPER(SO.Card) Card,UPPER(SO.Medium) as Medium,UPPER(CM.ClassName) ClassName,convert(nvarchar,So.DateOfAdmiission,106) as DateOfAdmiission ,SO.SectionId,UPPER(Sf.FatherName) FatherName,UPPER(SF.MotherName) MotherName,(UPPER(SG.FirstName)+' '+UPPER(SG.MiddleName)+' '+UPPER(SG.LastName)) as Name,sg.StEnRCode as StEnRCode,sg.srno  as srno,case  when so.TransportRequired='Yes' then 'Yes' else 'No' end as TransportRequired,so.wayamount as wayamount from StudentGenaralDetail SG ";
        sql = sql + "    left join StudentFamilyDetails SF on SG.srno=SF.srno";
        sql = sql + "   left join StudentOfficialDetails SO on SG.srno=SO.srno";
        sql = sql + "   left join ClassMaster CM on SO.AdmissionForClassId=CM.Id";
        sql = sql + "   left join SectionMaster SC on SO.SectionId=SC.Id";
        sql = sql + "    where sg.SessionName='" + Session["SessionName"].ToString() + "' and ";
        sql = sql + "     so.SessionName='" + Session["SessionName"].ToString() + "' and sf.SessionName='" + Session["SessionName"].ToString() + "' and cm.SessionName='" + Session["SessionName"].ToString() + "'";
        sql = sql + "    and SC.SessionName='" + Session["SessionName"].ToString() + "'  and";
        sql = sql + " sg.BranchCode=" + Session["BranchCode"].ToString() + "";
        sql = sql + "   and SO.Withdrwal is null and SO.AdmissionForClassId='" + drpClass.SelectedValue.ToString() + "' and Sc.SectionName='" + drpsection.SelectedItem.ToString() + "' order by SG.FirstName Asc";

        GridView1.DataSource = oo.GridFill(sql);
        GridView1.DataBind();
    }

    public void loadclass()
    {
        sql = "Select Id,ClassName,CidOrder from ClassMaster Where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " Order by CIDOrder";
        oo.FillDropDown_withValue(sql, drpClass, "ClassName", "Id");
    }

    public void loadsection()
    {
        sql = "Select SectionName,Id from SectionMaster where ClassNameId='" + drpClass.SelectedValue.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        oo.FillDropDown_withValue(sql, drpsection, "SectionName", "Id");
        drpsection.Items.Insert(0, "<--Select-->");
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        Label srno = (Label)lnk.NamingContainer.FindControl("Label2");

        //LinkButton lnk = (LinkButton)sender;
        //GridViewRow currentrow = (GridViewRow)lnk.NamingContainer;

        //Label srno = (Label)currentrow.FindControl("Label2");
        Session["srno"] = srno.Text;
        Session["ClassId"] = drpClass.SelectedValue.ToString();
        Session["SectionName"] = drpsection.SelectedItem.Text;
        Session["Eval"] = drpExamination.SelectedItem.Text;
        Response.Write("<script>");
        Response.Write("window.open('GenrateAdmitCard.aspx?print=1','_blank')");
        Response.Write("</script>");

    }
    protected void drpsection_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadgrid();
    }
}