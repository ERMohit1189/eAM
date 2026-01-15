using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;


public partial class vehicle_details : Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";
    string permitno = "N/A";
    string contactno = "N/A";
    string Address = "N/A";

    string permitno1 = "N/A";
    string contactno1 = "N/A";
    string Address1 = "N/A";


    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
            
        }
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file
        txtRegistrationExpiry.Attributes["value"] = txtRegistrationExpiry.Text;
        if (txtRegistrationExpiry.Text == "")
        {
            txtRegistrationExpiry.Text = DateTime.Now.ToString("dd-MMM-yyyy");
        }
        if (txtInsuranceExpiry.Text == "")
        {
            txtInsuranceExpiry.Text = DateTime.Now.ToString("dd-MMM-yyyy");
        }
        if (txtPollutionExpiry.Text == "")
        {
            txtPollutionExpiry.Text = DateTime.Now.ToString("dd-MMM-yyyy");
        }
        if (!IsPostBack)
        {
            

            sql = "Select VehicleType from VehicleMaster";
            sql = sql + " where BranchCode=" + Session["BranchCode"].ToString() + "";
            oo.FillDropDown(sql, DrpVehicleType, "VehicleType");
            oo.FillDropDown(sql, drpvehicletype1, "VehicleType");

            sql = "Select Agency from DealerMaster";
            sql = sql + " where BranchCode=" + Session["BranchCode"].ToString() + "";
            oo.FillDropDown(sql, drpagency, "Agency");
            oo.FillDropDown(sql, drpagency1, "Agency");

            sql = "Select FuelType from FuelMaster";
            sql = sql + " where BranchCode=" + Session["BranchCode"].ToString() + "";
            oo.FillDropDown_withValue(sql, drpfuletype, "FuelType", "FuelType");
            oo.FillDropDown_withValue(sql, drpfuletype1, "FuelType", "FuelType");

            Load();
        }
    }
    protected void Load()
    {
        sql = "Select  ROW_NUMBER() OVER (ORDER BY id ASC) AS SrNo,Id,VehicleType, VehicleNo, VehicleModel, BodyType, SeatCapacity, RegistrationNo, RegistrationExpiry,(Case when Status=1 then 'Active' else 'In Active' end)Status, ";
        sql = sql + "  VehicleMade, FuelType, EngineNo, ChasisNo, VehicleCC, MFG, VehicleRemark, OwnerName, OwnerContactNo, PermitNo, RegExpiryNotifyBefore, InsuranceNotifyBefore, PollutionNotifyBefore, ";
        sql = sql + "  ValidUpto, OwnerAddress, OwnerRemark, SessionName, BranchCode, LoginName,RecordDate,Driver, driverContact, InsuranceNo, InsuranceExpiry, PollutionReceiptNo, PollutionExpiry from VehicleDetails ";
        sql = sql + " where  BranchCode=" + Session["BranchCode"].ToString() + "";
        GridView1.DataSource = oo.GridFill(sql);
        GridView1.DataBind();
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            Label Label36 = (Label)GridView1.Rows[i].FindControl("Label36");
            sql = "Select  VehicleId from locationMapping where VehicleId='" + Label36.Text.Trim() + "' and BranchCode=" + Session["BranchCode"] + "";
            if (oo.Duplicate(sql))
            {
                LinkButton LinkButton3 = (LinkButton)GridView1.Rows[i].FindControl("LinkButton3");
                LinkButton LinkButton4 = (LinkButton)GridView1.Rows[i].FindControl("LinkButton4");
                //LinkButton3.Text = "<i class='fa fa-lock'></i>";
                LinkButton4.Text = "<i class='fa fa-lock'></i>";
                //LinkButton3.Enabled = false;
                LinkButton4.Enabled = false;
            }
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        if (DrpVehicleType.SelectedValue == "")
        {
            //oo.MessageBox("Please Select Vehicle Type!", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please Select Vehicle Type!", "A");
            DrpVehicleType.Focus();
            return;
        }
        else if (txtRegistrationNo.Text.Trim() == "")
        {
            //oo.MessageBox("Please Select Agency!", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please Enter Registration No.!", "A");
            txtRegistrationNo.Focus();
            return;
        }
        //else if (hdnRegistrationExpiry.Value == "")
        //{
        //    //oo.MessageBox("Please Select Agency!", this.Page);
        //    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please Enter Registration Expiry!", "A");
        //    txtRegistrationExpiry.Focus();
        //    return;
        //}
        else if (txtRegExpiryNotifyBefore.Text.Trim() == "")
        {
            //oo.MessageBox("Please Select Agency!", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please Enter Registration Expiry notify before day(s)!", "A");
            txtRegExpiryNotifyBefore.Focus();
            return;
        }
        else if (txtNo.Text.Trim() == "")
        {
            //oo.MessageBox("Please Select Agency!", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please Enter Vehicle No.!", "A");
            txtNo.Focus();
            return;
        }
        else if (drpfuletype.SelectedValue == "")
        {
            //oo.MessageBox("Please Select Fule Type!", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please Select Fule Type!", "A");
            drpfuletype.Focus();
            return;
        }
        else if (txtMFG.Text.Trim() == "")
        {
            //oo.MessageBox("Please Select Agency!", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please Enter MFG!", "A");
            txtMFG.Focus();
            return;
        }
        else if (drpagency.SelectedValue == "")
        {
            //oo.MessageBox("Please Select Agency!", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please Select Agency!", "A");
            drpagency.Focus();
            return;
        }

        else if (txtdriver.Text.Trim() == "")
        {
            //oo.MessageBox("Please Select Agency!", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please Enter Driver Name!", "A");
            txtdriver.Focus();
            return;
        }
        else if (txtdriverContact.Text.Trim() == "")
        {
            //oo.MessageBox("Please Select Agency!", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please Enter driver contact no.!", "A");
            txtdriverContact.Focus();
            return;
        }
        else if (txtdriverContact.Text.Trim().Length != 10)
        {
            //oo.MessageBox("Please Select Agency!", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please Enter valid driver contact no.!", "A");
            txtdriverContact.Focus();
            return;
        }
        else if (txtInsuranceNo.Text.Trim() == "")
        {
            //oo.MessageBox("Please Select Agency!", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please Enter Insurance No!", "A");
            txtInsuranceNo.Focus();
            return;
        }
        //else if (hdnInsuranceExpiry.Value == "")
        //{
        //    //oo.MessageBox("Please Select Agency!", this.Page);
        //    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please Enter Insurance Expiry!", "A");
        //    txtInsuranceExpiry.Focus();
        //    return;
        //}
        else if (txtInsuranceNotifyBefore.Text.Trim() == "")
        {
            //oo.MessageBox("Please Select Agency!", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please Enter Insurance Expiry notify before day(s)!", "A");
            txtInsuranceNotifyBefore.Focus();
            return;
        }
        else if (txtPollutionReceiptNo.Text.Trim() == "")
        {
            //oo.MessageBox("Please Select Agency!", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please Enter Pollution Receipt No!", "A");
            txtPollutionReceiptNo.Focus();
            return;
        }
        //else if (hdnPollutionExpiry.Value == "")
        //{
        //    //oo.MessageBox("Please Select Agency!", this.Page);
        //    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please Enter Pollution Expiry!", "A");
        //    txtPollutionExpiry.Focus();
        //    return;
        //}
        else if (txtPollutionNotifyBefore.Text.Trim() == "")
        {
            //oo.MessageBox("Please Select Agency!", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please Enter Pollution Expiry notify before day(s)!", "A");
            txtPollutionNotifyBefore.Focus();
            return;
        }
        else
        {
            sql = "Select vehicleNo from VehicleDetails where vehicleNo='" + txtNo.Text.Trim() + "'";
            sql = sql + " and BranchCode=" + Session["BranchCode"].ToString() + "";
            if (oo.Duplicate(sql))
            {
                //oo.MessageBox("This VehicleNo already Registered!", this.Page);
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "This VehicleNo already Registered!", "A");       

            }
            else
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "VehicleDetailsProce";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                cmd.Parameters.AddWithValue("@VehicleType", DrpVehicleType.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@VehicleNo", txtNo.Text.Trim().ToString());
                cmd.Parameters.AddWithValue("@VehicleModel", txtModel.Text.Trim().ToString());
                cmd.Parameters.AddWithValue("@BodyType", txtBodyType.Text.Trim().ToString());
                cmd.Parameters.AddWithValue("@SeatCapacity", txtSeatCapacity.Text.Trim().ToString());
                cmd.Parameters.AddWithValue("@RegistrationNo", txtRegistrationNo.Text.Trim().ToString());
                cmd.Parameters.AddWithValue("@RegistrationExpiry", hdnRegistrationExpiry.Value.ToString());
                cmd.Parameters.AddWithValue("@VehicleMade", txtMade.Text.Trim().ToString());
                cmd.Parameters.AddWithValue("@FuelType", drpfuletype.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@EngineNo", txtEngineNo.Text.Trim().ToString());
                cmd.Parameters.AddWithValue("@ChasisNo", txtChasis.Text.Trim().ToString());
                cmd.Parameters.AddWithValue("@VehicleCC", txtVehicleCC.Text.Trim().ToString());
                cmd.Parameters.AddWithValue("@MFG", txtMFG.Text.Trim().ToString());
                cmd.Parameters.AddWithValue("@VehicleRemark", txtVehicleRemark.Text.Trim().ToString());
                cmd.Parameters.AddWithValue("@OwnerName", drpagency.Text.Trim().ToString());
                cmd.Parameters.AddWithValue("@OwnerContactNo", contactno.Trim());
                cmd.Parameters.AddWithValue("@PermitNo", permitno.Trim());
                cmd.Parameters.AddWithValue("@DriverName", txtdriver.Text.Trim().ToString());
                cmd.Parameters.AddWithValue("@driverContact", txtdriverContact.Text.Trim().ToString());
                cmd.Parameters.AddWithValue("@InsuranceNo", txtInsuranceNo.Text.Trim().ToString());
                cmd.Parameters.AddWithValue("@InsuranceExpiry", hdnInsuranceExpiry.Value.ToString());
                cmd.Parameters.AddWithValue("@PollutionReceiptNo", txtPollutionReceiptNo.Text.Trim().ToString());
                cmd.Parameters.AddWithValue("@PollutionExpiry", hdnPollutionExpiry.Value.ToString());

                cmd.Parameters.AddWithValue("@RegExpiryNotifyBefore", txtRegExpiryNotifyBefore.Text.Trim().ToString());
                cmd.Parameters.AddWithValue("@InsuranceNotifyBefore", txtInsuranceNotifyBefore.Text.Trim().ToString());
                cmd.Parameters.AddWithValue("@PollutionNotifyBefore", txtPollutionNotifyBefore.Text.Trim().ToString());

                String Date = DateTime.Today.ToString("yyyy/MM/dd");
               
                cmd.Parameters.AddWithValue("@ValidUpto", Date.Trim());
                cmd.Parameters.AddWithValue("@OwnerAddress", Address.Trim());
                cmd.Parameters.AddWithValue("@OwnerRemark", "N/A");

                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
                cmd.Parameters.AddWithValue("@Status", DropDownList1.SelectedValue);
                try
                {

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    //oo.MessageBox("Submitted successfully.", this.Page);
                    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");       

                    oo.ClearControls(this.Page);
                    Load();

                }
                catch (Exception ex)
                {
                    //oo.MessageBox("Not Submitted successfully!", this.Page);
                    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Not Submitted successfully!", "A");       

                }
            }
        }
     
    }


    protected void LinkButton4_Click(object sender, EventArgs e)
    {

    }
    protected void DrpYY_SelectedIndexChanged(object sender, EventArgs e)
    {
        //oo.YearDropDown(DrpYY, DrpMM, DrpDD);
    }
    protected void DrpMM_SelectedIndexChanged(object sender, EventArgs e)
    {
        //oo.MonthDropDown(DrpYY0, DrpMM0, DrpDD0);
    }
    protected void DrpDD_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void DrpYY0_SelectedIndexChanged(object sender, EventArgs e)
    {
        //oo.YearDropDown(DrpYY0, DrpMM0, DrpDD0);
    }
    protected void DrpMM0_SelectedIndexChanged(object sender, EventArgs e)
    {
        //oo.YearDropDown(DrpYY0, DrpMM0, DrpDD0);
    }
    protected void DrpDD0_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Button8_Click(object sender, EventArgs e)
    {

    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        sql = "Delete from VehicleDetails where id=" + lblvalue.Text.Trim();

        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = sql;
        cmd.CommandType = CommandType.Text;
        cmd.Connection = con;

        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            //oo.MessageBox("Deleted successfully.", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Deleted successfully.", "A");

            Load();
        }
        catch (SqlException) { }
    }
    protected void LinkButton5_Click(object sender, EventArgs e)
    {
        //if (drpvehicletype1.SelectedIndex == 0)
        //{
        //    //oo.MessageBox("Please Select Vehicle Type!", this.Page);
        //    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox2, "Please Select Vehicle Type!", "A");
        //    drpvehicletype1.Focus();
        //    Panel1_ModalPopupExtender.Show();
        //    return;
        //}
        if (txtRegistrationNo0.Text.Trim() == "")
        {
            //oo.MessageBox("Please Select Agency!", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox2, "Please Enter Registration No.!", "A");
            txtRegistrationNo0.Focus();
            Panel1_ModalPopupExtender.Show();
            return;
        }
        else if (hdnRegistrationExpiry1.Value == "")
        {
            //oo.MessageBox("Please Select Agency!", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox2, "Please Enter Registration Expiry!", "A");
            txtRegistrationExpiry1.Focus();
            Panel1_ModalPopupExtender.Show();
            return;
        }
        else if (txtRegExpiryNotifyBefore1.Text.Trim() == "")
        {
            //oo.MessageBox("Please Select Agency!", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox2, "Please Enter Registration Expiry notify before day(s)!", "A");
            txtRegExpiryNotifyBefore1.Focus();
            Panel1_ModalPopupExtender.Show();
            return;
        }
        else if (txtNo0.Text.Trim() == "")
        {
            //oo.MessageBox("Please Select Agency!", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox2, "Please Enter Vehicle No.!", "A");
            txtNo0.Focus();
            Panel1_ModalPopupExtender.Show();
            return;
        }
        //else if (drpfuletype1.SelectedIndex == 0)
        //{
        //    //oo.MessageBox("Please Select Fule Type!", this.Page);
        //    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox2, "Please Select Fule Type!", "A");
        //    drpfuletype1.Focus();
        //    Panel1_ModalPopupExtender.Show();
        //    return;
        //}
        else if (txtMFG0.Text.Trim() == "")
        {
            //oo.MessageBox("Please Select Agency!", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox2, "Please Enter MFG!", "A");
            txtMFG0.Focus();
            Panel1_ModalPopupExtender.Show();
            return;
        }
        //else if (drpagency1.SelectedIndex == 0)
        //{
        //    //oo.MessageBox("Please Select Agency!", this.Page);
        //    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox2, "Please Select Agency!", "A");
        //    drpagency1.Focus();
        //    Panel1_ModalPopupExtender.Show();
        //    return;
        //}

        else if (txtdriverName1.Text.Trim() == "")
        {
            //oo.MessageBox("Please Select Agency!", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox2, "Please Enter Driver Name!", "A");
            txtdriverName1.Focus();
            Panel1_ModalPopupExtender.Show();
            return;
        }
        else if (txtdriverContact1.Text.Trim() == "")
        {
            //oo.MessageBox("Please Select Agency!", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox2, "Please Enter driver contact no.!", "A");
            txtdriverContact1.Focus();
            Panel1_ModalPopupExtender.Show();
            return;
        }
        else if (txtdriverContact1.Text.Trim().Length != 10)
        {
            //oo.MessageBox("Please Select Agency!", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox2, "Please Enter valid driver contact no.!", "A");
            txtdriverContact1.Focus();
            Panel1_ModalPopupExtender.Show();
            return;
        }
        else if (txtInsuranceNo1.Text.Trim() == "")
        {
            //oo.MessageBox("Please Select Agency!", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox2, "Please Enter Insurance No!", "A");
            txtInsuranceNo1.Focus();
            Panel1_ModalPopupExtender.Show();
            return;
        }
        else if (hdnInsuranceExpiry1.Value == "")
        {
            //oo.MessageBox("Please Select Agency!", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox2, "Please Enter Insurance Expiry!", "A");
            txtInsuranceExpiry1.Focus();
            Panel1_ModalPopupExtender.Show();
            return;
        }
        else if (txtInsuranceNotifyBefore1.Text.Trim() == "")
        {
            //oo.MessageBox("Please Select Agency!", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox2, "Please Enter Insurance Expiry notify before day(s)!", "A");
            txtInsuranceNotifyBefore1.Focus();
            Panel1_ModalPopupExtender.Show();
            return;
        }
        else if (txtPollutionReceiptNo1.Text.Trim() == "")
        {
            //oo.MessageBox("Please Select Agency!", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox2, "Please Enter Pollution Receipt No!", "A");
            drpvehicletype1.Focus();
            Panel1_ModalPopupExtender.Show();
            return;
        }
        else if (hdnPollutionExpiry1.Value == "")
        {
            //oo.MessageBox("Please Select Agency!", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox2, "Please Enter Pollution Expiry!", "A");
            txtPollutionExpiry1.Focus();
            Panel1_ModalPopupExtender.Show();
            return;
        }
        else if (txtPollutionNotifyBefore1.Text.Trim() == "")
        {
            //oo.MessageBox("Please Select Agency!", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox2, "Please Enter Pollution Expiry notify before day(s)!", "A");
            txtPollutionNotifyBefore1.Focus();
            Panel1_ModalPopupExtender.Show();
            return;
        }
        else
        {
            sql = "Select vehicleNo from VehicleDetails where vehicleNo='" + txtNo0.Text.Trim() + "' and BranchCode=" + Session["BranchCode"].ToString() + " except ";
            sql = sql + " Select vehicleNo from VehicleDetails where Id='" + lblID.Text.Trim() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            if (oo.Duplicate(sql))
            {
                //oo.MessageBox("This VehicleNo already Registered!", this.Page);
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox2, "This VehicleNo already Registered!", "W");

            }
            else
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "VehicleDetailsUpdateProce";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                cmd.Parameters.AddWithValue("@id", lblID.Text.Trim());
                cmd.Parameters.AddWithValue("@VehicleType", drpvehicletype1.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@VehicleNo", txtNo0.Text.Trim().ToString());
                cmd.Parameters.AddWithValue("@VehicleModel", txtModel0.Text.Trim().ToString());
                cmd.Parameters.AddWithValue("@BodyType", txtBodyType0.Text.Trim().ToString());
                cmd.Parameters.AddWithValue("@SeatCapacity", txtSeatCapacity0.Text.Trim().ToString());
                cmd.Parameters.AddWithValue("@RegistrationNo", txtRegistrationNo0.Text.Trim().ToString());
                cmd.Parameters.AddWithValue("@RegistrationExpiry", hdnRegistrationExpiry1.Value.ToString());
                cmd.Parameters.AddWithValue("@VehicleMade", txtMade0.Text.Trim().ToString());
                cmd.Parameters.AddWithValue("@FuelType", drpfuletype1.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@EngineNo", txtEngineNo0.Text.Trim().ToString());
                cmd.Parameters.AddWithValue("@ChasisNo", txtChasis0.Text.Trim().ToString());
                cmd.Parameters.AddWithValue("@VehicleCC", txtVehicleCC0.Text.Trim().ToString());
                cmd.Parameters.AddWithValue("@MFG", txtMFG0.Text.Trim().ToString());
                cmd.Parameters.AddWithValue("@VehicleRemark", txtremark1.Text.Trim());
                cmd.Parameters.AddWithValue("@OwnerName", drpagency1.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@OwnerContactNo", contactno1);
                cmd.Parameters.AddWithValue("@PermitNo", permitno1);
                cmd.Parameters.AddWithValue("@DriverName", txtdriverName1.Text.Trim().ToString());
                cmd.Parameters.AddWithValue("@driverContact", txtdriverContact1.Text.Trim().ToString());
                cmd.Parameters.AddWithValue("@InsuranceNo", txtInsuranceNo1.Text.Trim().ToString());
                cmd.Parameters.AddWithValue("@InsuranceExpiry", hdnInsuranceExpiry1.Value.ToString());
                cmd.Parameters.AddWithValue("@PollutionReceiptNo", txtPollutionReceiptNo1.Text.Trim().ToString());
                cmd.Parameters.AddWithValue("@PollutionExpiry", hdnPollutionExpiry1.Value.ToString());
                cmd.Parameters.AddWithValue("@RegExpiryNotifyBefore", txtRegExpiryNotifyBefore1.Text.Trim().ToString());
                cmd.Parameters.AddWithValue("@InsuranceNotifyBefore", txtInsuranceNotifyBefore1.Text.Trim().ToString());
                cmd.Parameters.AddWithValue("@PollutionNotifyBefore", txtPollutionNotifyBefore1.Text.Trim().ToString());

                String Date0 = DateTime.Today.ToString("yyyy/MM/dd");

                cmd.Parameters.AddWithValue("@ValidUpto", Date0);
                cmd.Parameters.AddWithValue("@OwnerAddress", Address1);
                cmd.Parameters.AddWithValue("@OwnerRemark", txtremark1.Text.Trim());

                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());//Session["Login"].ToString());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
                cmd.Parameters.AddWithValue("@Status", DropDownList2.SelectedValue);


                try
                {

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    //oo.MessageBox("Updated successfully.", this.Page);
                    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully.", "S");

                    oo.ClearControls(this.Page);
                    Load();

                }
                catch (Exception) { }
            }
        }
    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblId2 = (Label)chk.NamingContainer.FindControl("Label36");

        string ss = lblId2.Text.Trim();
        lblID.Text = ss;
        sql = "Select  ROW_NUMBER() OVER (ORDER BY id ASC) AS SrNo,Id,VehicleType, VehicleNo, VehicleModel, BodyType, SeatCapacity, RegistrationNo, RegistrationExpiry,Status, ";
        sql = sql + "  VehicleMade, FuelType, EngineNo, ChasisNo, VehicleCC, MFG, VehicleRemark, OwnerName, OwnerContactNo, PermitNo, RegExpiryNotifyBefore, InsuranceNotifyBefore, PollutionNotifyBefore, ";
        sql = sql + "  left(convert(nvarchar,ValidUpto,106),2) as DD,Right(left(convert(nvarchar,ValidUpto,106),6),3) as MM , RIGHT(convert(nvarchar,ValidUpto,106),4) as YY,";
        
        sql = sql + "   OwnerAddress, OwnerRemark, SessionName, BranchCode, LoginName,RecordDate, Driver, driverContact, InsuranceNo, InsuranceExpiry, PollutionReceiptNo, PollutionExpiry from VehicleDetails ";
        sql = sql + " where Id=" + ss;
        sql = sql + "  and BranchCode=" + Session["BranchCode"].ToString() + "";
  
        drpvehicletype1.Text= oo.ReturnTag(sql, "VehicleType");
        txtNo0.Text= oo.ReturnTag(sql, "VehicleNo");
        txtModel0.Text= oo.ReturnTag(sql, "VehicleModel");
        txtBodyType0.Text= oo.ReturnTag(sql, "BodyType");
        txtSeatCapacity0.Text= oo.ReturnTag(sql, "SeatCapacity");
        txtRegistrationNo0.Text= oo.ReturnTag(sql, "RegistrationNo");
        txtRegistrationExpiry1.Text= oo.ReturnTag(sql, "RegistrationExpiry");
        hdnRegistrationExpiry1.Value= oo.ReturnTag(sql, "RegistrationExpiry");
        txtMade0.Text= oo.ReturnTag(sql, "VehicleMade");
        drpfuletype1.Text= oo.ReturnTag(sql, "FuelType");
        txtEngineNo0.Text= oo.ReturnTag(sql, "EngineNo");
        txtChasis0.Text= oo.ReturnTag(sql, "ChasisNo");
        txtVehicleCC0.Text= oo.ReturnTag(sql, "VehicleCC");
        txtMFG0.Text= oo.ReturnTag(sql, "MFG");
        txtremark1.Text = oo.ReturnTag(sql, "VehicleRemark");
        drpagency1.Text= oo.ReturnTag(sql, "OwnerName");
        contactno1 = oo.ReturnTag(sql, "OwnerContactNo");
        permitno1 = oo.ReturnTag(sql, "PermitNo");
        Address1 = oo.ReturnTag(sql, "OwnerAddress");
        txtdriverName1.Text = oo.ReturnTag(sql, "Driver");
        txtdriverContact1.Text = oo.ReturnTag(sql, "driverContact");
        txtInsuranceNo1.Text = oo.ReturnTag(sql, "InsuranceNo");
        txtInsuranceExpiry1.Text = oo.ReturnTag(sql, "InsuranceExpiry");
        hdnInsuranceExpiry1.Value = oo.ReturnTag(sql, "InsuranceExpiry");
        txtPollutionReceiptNo1.Text = oo.ReturnTag(sql, "PollutionReceiptNo");
        txtPollutionExpiry1.Text = oo.ReturnTag(sql, "PollutionExpiry");
        hdnPollutionExpiry1.Value = oo.ReturnTag(sql, "PollutionExpiry");
        txtRegExpiryNotifyBefore1.Text = oo.ReturnTag(sql, "RegExpiryNotifyBefore");
        txtInsuranceNotifyBefore1.Text = oo.ReturnTag(sql, "InsuranceNotifyBefore");
        txtPollutionNotifyBefore1.Text = oo.ReturnTag(sql, "PollutionNotifyBefore");

        DropDownList2.SelectedValue = oo.ReturnTag(sql, "Status");
        Panel1_ModalPopupExtender.Show();



    }
    protected void LinkButton4_Click1(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblId3 = (Label)chk.NamingContainer.FindControl("Label37");
        string ss = lblId3.Text;
        lblvalue.Text = ss.ToString();
        Panel2_ModalPopupExtender.Show();
        Button8.Focus();
    }
    protected void DrpVehicleType_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void drpagency_SelectedIndexChanged(object sender, EventArgs e)
    {
        sql = "Select Permitno,Contactno,Address from DealerMaster where Agency='" + drpagency.SelectedItem.ToString() + "'";
        sql = sql + " and BranchCode=" + Session["BranchCode"].ToString() + "";
        SqlDataReader dr;
        dr=oo.fetch_data(sql);
        if (dr.Read())
        {
            permitno = dr["Permitno"].ToString();
            contactno = dr["Contactno"].ToString();
            Address = dr["Address"].ToString();
        }


    }
    protected void txtRegistrationNo_TextChanged(object sender, EventArgs e)
    {
       txtNo.Text=txtRegistrationNo.Text.Trim();
    }
    
}

