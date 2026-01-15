using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;

public partial class admin_Dealer_master : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        if ((string)Session["LoginName"] == "" || (string)Session["BranchCode"] == "" || (string)Session["SessionName"] == "" || Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }

        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file

        if (!IsPostBack)
        {

            sql = "select ltrim(rtrim(CountryName)) as CountryName, id from CountryMaster ";
            oo.FillDropDown_withValue(sql, DropCountry, "CountryName", "id");
            oo.FillDropDown_withValue(sql, DropCountry0, "CountryName", "id");
            using (var objBll = new BLL())
            {
                try
                {
                    objBll.loadDefaultvalue("Country", DropCountry);
                    objBll.loadDefaultvalue("Country", DropCountry0);
                }
                catch
                {
                    // ignored
                }
            }

            DropState.Items.Clear();
            sql = "Select count(*) cnt from StateMaster where countryId='" + DropCountry.SelectedValue + "'";
            if (oo.ReturnTag(sql, "cnt") == "0")
            {
                DropState.Items.Add(new ListItem("Other", "0"));
            }
            else
            {
                sql = "Select StateName,Id from StateMaster where countryId='" + DropCountry.SelectedValue + "'";
                oo.FillDropDown_withValue(sql, DropState, "StateName", "id");
                oo.FillDropDown_withValue(sql, DropState0, "StateName", "id");
                using (var objBll = new BLL())
                {
                    try
                    {
                        objBll.loadDefaultvalue("State", DropState);
                        objBll.loadDefaultvalue("State", DropState0);
                    }
                    catch
                    {
                        // ignored
                    }
                }
            }
            DropCity.Items.Clear();
            sql = "Select count(*) cnt from CityMaster where StateId='" + DropState.SelectedValue + "'";
            if (oo.ReturnTag(sql, "cnt") == "0")
            {
                DropCity.Items.Add(new ListItem("Other", "0"));
            }
            else
            {
                sql = "Select CityName,id from CityMaster where StateId='" + DropState.SelectedValue + "'";
                BAL.objBal.FillDropDown_withValue(sql, DropCity, "CityName", "id");
                BAL.objBal.FillDropDown_withValue(sql, DropCity0, "CityName", "id");
                using (var objBll = new BLL())
                {
                    try
                    {
                        objBll.loadDefaultvalue("City", DropCity);
                        objBll.loadDefaultvalue("City", DropCity0);
                    }
                    catch
                    {
                        // ignored
                    }
                }
            }
            load();




        }
    }
    protected void load()
    {
        sql = "Select  ROW_NUMBER() OVER (ORDER BY id ASC) AS SrNo,Id,Agency  ,Country  ,State  ,City  ,Email,PhoneNo,";
        sql = sql + "      Address  ,Owner  ,";
        sql = sql + "   ContactPerson  ,ContactNo  ,SessionName  ,BranchCode ,LoginName,RecordDate,permitno,Address1 from DealerMaster";
        sql = sql + "  where   BranchCode=" + Session["BranchCode"].ToString() + "";
        GridView1.DataSource = oo.GridFill(sql);
        GridView1.DataBind();
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            Label Label2 = (Label)GridView1.Rows[i].FindControl("Label2");
            sql = "Select  OwnerName from VehicleDetails where OwnerName='" + Label2.Text.Trim() + "' and BranchCode=" + Session["BranchCode"] + "";
            if (oo.Duplicate(sql))
            {
               // LinkButton LinkButton2 = (LinkButton)GridView1.Rows[i].FindControl("LinkButton2");
                LinkButton LinkButton3 = (LinkButton)GridView1.Rows[i].FindControl("LinkButton3");
               // LinkButton2.Text = "<i class='fa fa-lock'></i>";
                LinkButton3.Text = "<i class='fa fa-lock'></i>";
                //LinkButton2.Enabled = false;
                LinkButton3.Enabled = false;
            }
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {      
        if (DropCity.SelectedIndex == 0 && DropCity.Items.Count>1)
        {
            //oo.MessageBox("Please Select Your City!", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please Select Your City!", "A");       

        }
        else
        {
            sql = "Select Agency from DealerMaster where Agency='" + txtAgency.Text.Trim() + "'";
            sql = sql + " and BranchCode=" + Session["BranchCode"].ToString() + "";
            if (oo.Duplicate(sql))
            {
                //oo.MessageBox("This Agency already Registered!", this.Page);
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "This Agency already Registered!", "A");       

            }
            else
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "[DealerMasterProc]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                cmd.Parameters.AddWithValue("@Agency", txtAgency.Text.Trim().ToString());
                cmd.Parameters.AddWithValue("@Country", DropCountry.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@State", DropState.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@City", DropCity.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim().ToString());
                cmd.Parameters.AddWithValue("@Address", txtAddress1.Text.Trim().ToString());
                cmd.Parameters.AddWithValue("@Owner", txtOwner.Text.Trim().ToString());
                cmd.Parameters.AddWithValue("@PhoneNo", txtphoneno.Text.Trim().ToString());
                cmd.Parameters.AddWithValue("@ContactPerson", txtContactPerson.Text.Trim().ToString());
                cmd.Parameters.AddWithValue("@ContactNo", txtContactNo.Text.Trim().ToString());
                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
                cmd.Parameters.AddWithValue("@Permitno", txtpermitno.Text.Trim().ToString());
                cmd.Parameters.AddWithValue("@Address1", txtAddress2.Text.Trim().ToString());


                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    //oo.MessageBox("Submitted successfully.", this.Page);
                    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");
                    load();

                }
                catch (Exception) { }
            }

        }
        }
    protected void DropState_SelectedIndexChanged(object sender, EventArgs e)
    {
        string CCode = "";
        sql = "select Id from StateMaster where StateName='" + DropState.SelectedItem.ToString() + "'";

        CCode = oo.ReturnTag(sql, "id");
        sql = "Select CityName from CityMaster where StateId=" + CCode;

        oo.FillDropDown(sql, DropCity, "CityName");
    }
    protected void DropCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        string CCode = "";
        sql = "select Id from CountryMaster where CountryName='" + DropCountry.SelectedItem.ToString() + "'";

        CCode = oo.ReturnTag(sql, "id");
        sql = "Select StateName from StateMaster where CountryId=" + CCode;

        oo.FillDropDown(sql, DropState, "StateName");
    }
    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        if (DropCity0.SelectedIndex == 0)
        {
            //oo.MessageBox("Please Select Your City!", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please Select Your City!", "A");

        }
        else
        {
            sql = "Select Agency from DelearMaster where Agency='" + txtAgency.Text.Trim().ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " except ";
            sql = sql + " Select Agency from DelearMaster where Id='" + lblID.Text.Trim() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            if (oo.Duplicate(sql))
            {
                //oo.MessageBox("This Agency already Registered!", this.Page);
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "This Dealer already Registered!", "A");

            }
            else
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "[DealerMasterUpdateProc]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@id", lblID.Text);
                cmd.Parameters.AddWithValue("@Agency", txtAgency0.Text.Trim().ToString());
                cmd.Parameters.AddWithValue("@Country", DropCountry0.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@State", DropState0.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@City", DropCity0.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@PhoneNo", txtphoneNo0.Text.Trim().ToString());
                cmd.Parameters.AddWithValue("@Email", txtEmail0.Text.Trim().ToString());
                cmd.Parameters.AddWithValue("@Address", txtAddress0.Text.Trim().ToString());
                cmd.Parameters.AddWithValue("@Owner", txtOwner0.Text.Trim().ToString());
                cmd.Parameters.AddWithValue("@ContactPerson", txtContactPerson0.Text.Trim().ToString());
                cmd.Parameters.AddWithValue("@ContactNo", txtContactNo0.Text.Trim().ToString());
                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());//Session["Login"].ToString());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
                cmd.Parameters.AddWithValue("@permitno", txtperminno1.Text.Trim().ToString());
                cmd.Parameters.AddWithValue("@Address1", txtAddress1_0.Text.Trim().ToString());

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully.", "S");
                    load();

                }
                catch (Exception)
                {
                    //oo.MessageBox("Not Updated successfully.", this.Page);
                    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Not Updated successfully.", "W");

                }
            }
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        sql = "Delete from DealerMaster where BranchCode=" + Session["BranchCode"].ToString() + " and id=" + lblvalue.Text.Trim();

        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = sql;
        cmd.CommandType = CommandType.Text;
        cmd.Connection = con;

        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            oo.MessageBox("Deleted successfully.", this.Page);
            load();
        }
        catch (SqlException) { }
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("Label36");
     
        string ss = lblId.Text;
        lblID.Text = ss;
        sql = "Select  ROW_NUMBER() OVER (ORDER BY id ASC) AS SrNo,Id,Agency  ,Country  ,State  ,City  ,PhoneNo ,Email,";
        sql = sql + "      Address  ,Owner  ,";
        sql = sql + "   ContactPerson  ,ContactNo  ,SessionName  ,BranchCode ,LoginName,RecordDate,permitno,Address1 from DealerMaster";
        sql = sql + " where Id=" + ss+ " and BranchCode=" + Session["BranchCode"].ToString() + "";
        //  sql = sql + " and  SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

        txtAgency0.Text = oo.ReturnTag(sql, "Agency");
        //DropCountry0.Text = oo.ReturnTag(sql, "Country");
        //DropState0.Text = oo.ReturnTag(sql, "State");
        //DropCity0.Text = oo.ReturnTag(sql, "City");
        txtphoneNo0.Text = oo.ReturnTag(sql, "PhoneNo");
        txtEmail0.Text = oo.ReturnTag(sql, "Email");
        txtAddress0.Text = oo.ReturnTag(sql, "Address");
        txtOwner0.Text = oo.ReturnTag(sql, "Owner");
        txtContactPerson0.Text = oo.ReturnTag(sql, "ContactPerson");
        txtContactNo0.Text = oo.ReturnTag(sql, "ContactNo");
        txtperminno1.Text = oo.ReturnTag(sql, "permitno");
        txtAddress1_0.Text = oo.ReturnTag(sql, "Address1");
        Panel1_ModalPopupExtender.Show();
    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("Label37");

        string ss = lblId.Text;
        lblvalue.Text = ss.ToString();
        Panel3_ModalPopupExtender.Show();
        Button8.Focus();
    }
    protected void Button8_Click(object sender, EventArgs e)
    {

    }
    protected void LinkButton5_Click(object sender, EventArgs e)
    {
      
    }
    protected void DropCountry0_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void DropState0_SelectedIndexChanged(object sender, EventArgs e)
    {
        //string CCode = "";
        //sql = "select Id from StateMaster where StateName='" + DropState0.SelectedItem.ToString() + "'";

        //CCode = oo.ReturnTag(sql, "id");
        //sql = "Select CityName from CityMaster where StateId=" + CCode;

        //oo.FillDropDown(sql, DropCity0, "CityName");
    }
    protected void DropCity0_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void DropCity_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }
    protected void DropState0_TextChanged(object sender, EventArgs e)
    {
        Panel1_ModalPopupExtender.Show();
    }
    protected void DropState_TextChanged(object sender, EventArgs e)
    {

        string CCode = "";
        sql = "select Id from StateMaster where StateName='" + DropState.SelectedItem.ToString() + "'";

        CCode = oo.ReturnTag(sql, "id");
        sql = "Select CityName from CityMaster where StateId=" + CCode;

        oo.FillDropDown(sql, DropCity, "CityName");
    }
}

