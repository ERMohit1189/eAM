using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System;
using System.Globalization;

public partial class ConveyanceAllotment : Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";
    string[] InstallmentMonth;
    public string srno = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file
    }
    protected void lnkView_Click(object sender, EventArgs e)
    {
        string studentId = Request.Form[hfStudentId.UniqueID];
        if (studentId == null || studentId == string.Empty)
        {
            studentId = txtSearch.Text.Trim();
        }
        
        BindData(studentId);
    }
    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {
        string studentId = Request.Form[hfStudentId.UniqueID];
        if (studentId == null || studentId == string.Empty)
        {
            studentId = txtSearch.Text.Trim();
        }
        BindData(studentId);
    }
    protected void loadInitialGrid(string card, string classname, GridView grdAllotment)
    {
        sql = "select MonthName, mm.MonthId from MonthMaster mm";
        sql = sql + " inner join FeeGroupMaster fgm on (Case when ISNUMERIC(CardType)=1 THEN fgm.Id Else fgm.FeeGroupName End)=mm.CardType and fgm.SessionName=mm.SessionName";
        sql = sql + " inner join ClassMaster cm on cm.Id=mm.ClassId and cm.sessionName=mm.SessionName";
        sql = sql + " where fgm.FeeGroupName='" + card + "' and cm.BranchCode=" + Session["BranchCode"].ToString() + " and fgm.BranchCode=" + Session["BranchCode"].ToString() + " and mm.BranchCode=" + Session["BranchCode"].ToString() + " and cm.ClassName='" + classname + "'  and mm.SessionName='" + Session["SessionName"].ToString() + "' ";
        sql = sql + " and mm.BranchCode=" + Session["BranchCode"].ToString() + " or monthid=0 order by MonthId";

        grdAllotment.DataSource = oo.GridFill(sql);
        grdAllotment.DataBind();
    }
    protected void loadVehicle()
    {
        sql = "SELECT Id, VehicleNo+ ' ( '+ Driver+' )' VehicleNo FROM VehicleDetails where BranchCode=" + Session["BranchCode"].ToString() + " and id in (select distinct VehicleId from locationMapping where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + ") and Status=1  ORDER by VehicleNo";
        for (int i = 0; i < grdAllotment.Rows.Count; i++)
        {
            DropDownList ddlvcl = (DropDownList)grdAllotment.Rows[i].FindControl("ddlVehicle");
            oo.FillDropDown_withValue(sql, ddlvcl, "VehicleNo", "Id");
            ddlvcl.Items.Insert(0, new ListItem("<--Vehicle-->", "0"));
        }
        DropDownList ddlVehicleH = (DropDownList)grdAllotment.HeaderRow.FindControl("ddlVehicleH");
        oo.FillDropDown_withValue(sql, ddlVehicleH, "VehicleNo", "Id");
        ddlVehicleH.Items.Insert(0, new ListItem("<--Vehicle-->", "0"));
    }
    protected void loadLocation(string ddlVehicleHVal,int index)
    {
        sql = "SELECT locationId, locationName+ ' ( '+ CONVERT(nvarchar(max), distanceInKm)+' )' location FROM LocationMaster where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and locationId in (select distinct locationId from locationMapping where VehicleId=" + ddlVehicleHVal + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + ") ORDER by locationId asc";
        DropDownList ddlloc = (DropDownList)grdAllotment.Rows[index].FindControl("ddlLocation");
        oo.FillDropDown_withValue(sql, ddlloc, "location", "locationId");
        ddlloc.Items.Insert(0, new ListItem("<--Location-->", "0"));
        DropDownList ddlLocationH = (DropDownList)grdAllotment.HeaderRow.FindControl("ddlLocationH");
        oo.FillDropDown_withValue(sql, ddlLocationH, "location", "Id");
        ddlLocationH.Items.Insert(0, new ListItem("<--Location-->", "0"));

    }
    protected void GetInfoOfStudent()
    {
        loadVehicle();
        //loadLocation();
        if (studentInfoGrid.Rows.Count > 0)
        {
            Label srno = (Label)studentInfoGrid.Rows[0].FindControl("Label6");
            Label StEnRCode = (Label)studentInfoGrid.Rows[0].FindControl("Label12");
            sql = "Select *,LoginName+' ('+Format(RecordDate,'dd-MMM-yyyy hh:mm:ss tt')+' )' LoginNameWithDate from StudentVehicleAllotment where SrNo='" + srno.Text + "' and BranchCode=" + Session["BranchCode"].ToString() + " and SessionName='" + Session["SessionName"].ToString() + "'";
            DataTable dt;
            dt = oo.Fetchdata(sql);
            int Vcount = 0;
            int Lcount = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < grdAllotment.Rows.Count; j++)
                {
                    Label InstallmentId = (Label)grdAllotment.Rows[j].FindControl("InstallmentId");
                    Label lblLoginName = (Label)grdAllotment.Rows[j].FindControl("lblLoginName");
                    DropDownList ddlVehicle = (DropDownList)grdAllotment.Rows[j].FindControl("ddlVehicle");
                    DropDownList ddlLocation = (DropDownList)grdAllotment.Rows[j].FindControl("ddlLocation");
                    CheckBox chkPickupL = (CheckBox)grdAllotment.Rows[j].FindControl("chkPickupL");
                    CheckBox chkDropL = (CheckBox)grdAllotment.Rows[j].FindControl("chkDropL");
                    TextBox lblTotalAmount = (TextBox)grdAllotment.Rows[j].FindControl("lblTotalAmount");

                    lblLoginName.Text = dt.Rows[i]["LoginNameWithDate"].ToString();

                    if (InstallmentId.Text == "198" && dt.Rows[i]["InstallmentId"].ToString() == "198")
                    {
                        string ss = dt.Rows[i]["InstallmentId"].ToString();
                    }
                    if (InstallmentId.Text == dt.Rows[i]["InstallmentId"].ToString())
                    {
                        ddlVehicle.SelectedValue = dt.Rows[i]["VehicleId"].ToString();
                        ddlLocation.Items.Clear();
                        string ss2 = "SELECT locationId, locationName+ ' ( '+ CONVERT(nvarchar(max), distanceInKm)+' )' location FROM LocationMaster where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and locationId in (select distinct locationId from locationMapping where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + ") ORDER by locationId asc";
                        oo.FillDropDown_withValue(ss2, ddlLocation, "location", "locationId");
                        ddlLocation.Items.Insert(0, new ListItem("<--Location-->", "0"));
                        ddlLocation.SelectedValue = dt.Rows[i]["LocationId"].ToString();

                        chkPickupL.Checked = (dt.Rows[i]["PickupStatus"].ToString() == "True" ? true : false);
                        chkDropL.Checked = (dt.Rows[i]["DropStatus"].ToString() == "True" ? true : false);
                        lblTotalAmount.Text = dt.Rows[i]["amount"].ToString();
                        if (chkPickupL.Checked)
                        {
                            Vcount = Vcount + 1;
                        }
                        if (chkDropL.Checked)
                        {
                            Lcount = Lcount + 1;
                        }
                        string pp = "Select count(*) cnt from CompositFeeDeposit where SrNo='" + srno.Text.Trim() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and SessionName='" + Session["SessionName"].ToString() + "' and receiptStatus = 'Paid' and InstallmentId = " + InstallmentId.Text + " and FeeHeadId in (select id from FeeHeadMaster where BranchCode = " + Session["BranchCode"].ToString() + " and FeeType = 'Transport Fee')";
                        if (oo.ReturnTag(pp, "cnt") != "0" && oo.ReturnTag(pp, "cnt") != "")
                        {
                            GridViewRow trh = (GridViewRow)grdAllotment.HeaderRow;
                            var thcount = trh.Cells.Count;
                            for (int k = 0; k < thcount; k++)
                            {
                                trh.BackColor = System.Drawing.Color.Red;
                                if (k == 2)
                                {
                                }
                                    // trh.Enabled = false;
                                }
                           
                           
                            GridViewRow tr = (GridViewRow)grdAllotment.Rows[j];
                            chkPickupL.Enabled = false;
                            chkDropL.Enabled = false;
                            var TDCount = tr.Cells.Count;
                            for (int m = 0; m < TDCount; m++)
                            {
                                tr.Cells[m].BackColor = System.Drawing.Color.Red;
                                if(m==2)
                                {
                                    DropDownList ddlVehicleH = (DropDownList)tr.Cells[m].FindControl("ddlVehicleH");
                                    DropDownList ddlLocationH = (DropDownList)tr.Cells[m].FindControl("ddlLocationH");
                                    DropDownList ddlVehicle1 = (DropDownList)tr.Cells[m].FindControl("ddlVehicle");
                                    DropDownList ddlLocation1 = (DropDownList)tr.Cells[m].FindControl("ddlLocation");
                                 
                                    if (ddlVehicleH != null)
                                    {
                                        ddlVehicleH.Enabled = true;
                                    }
                                    if (ddlLocationH != null)
                                    {
                                        ddlLocationH.Enabled = true;
                                    }
                                    if (ddlVehicle1 != null)
                                    {
                                        ddlVehicle1.Enabled = true;
                                    }
                                    if (ddlLocation1 != null)
                                    {
                                        ddlLocation1.Enabled = true;
                                    }
                                  
                                }
                                //else if(m==3)
                                //{
                                //    CheckBox chkDropH1 = (CheckBox)tr.Cells[m].FindControl("chkPickupH");
                                //    CheckBox chkDropL1 = (CheckBox)tr.Cells[m].FindControl("chkPickupH");
                                //    if (chkDropH1 != null)
                                //    {
                                //        chkDropH1.Enabled = true;
                                //    }
                                //    if (chkDropL1 != null)
                                //    {
                                //        chkDropL1.Enabled = true;
                                //    }
                                //}
                                //else if (m == 4)
                                //{
                                //    CheckBox chkDropH1 = (CheckBox)tr.Cells[m].FindControl("chkDropH");
                                //    CheckBox chkDropL1 = (CheckBox)tr.Cells[m].FindControl("chkDropL");
                                //    if (chkDropH1 != null)
                                //    {
                                //        chkDropH1.Enabled = true;
                                //    }
                                //    if (chkDropL1 != null)
                                //    {
                                //        chkDropL1.Enabled = true;
                                //    }
                                //}
                                else if (m == 5)
                                {
                                    TextBox txttotal = (TextBox)tr.Cells[m].FindControl("lblTotalAmount");
                                    if (txttotal != null)
                                    {
                                        txttotal.Enabled = true;
                                    }
                                   
                                }
                                else
                                {
                                    tr.Cells[m].Enabled = false;
                                }

                            }
                            tr.BackColor = System.Drawing.Color.Red;
                            tr.Enabled = false;
                        }
                    }
                }
            }

            double Gtotal = 0;
            for (int i = 0; i < grdAllotment.Rows.Count; i++)
            {
                double total = 0;
                TextBox lbltotal = (TextBox)grdAllotment.Rows[i].FindControl("lblTotalAmount");
                double.TryParse(lbltotal.Text.Trim(), out total);
                Gtotal = Gtotal + total;
            }
            Label lblGTotalAmount = (Label)grdAllotment.FooterRow.FindControl("lblGTotalAmount");
            lblGTotalAmount.Text = Gtotal.ToString("0.00");
            if (grdAllotment.Rows.Count == Vcount)
            {
                CheckBox chkPickupH = (CheckBox)grdAllotment.HeaderRow.FindControl("chkPickupH");
                chkPickupH.Checked = true;
            }
            if (grdAllotment.Rows.Count == Lcount)
            {
                CheckBox chkDropH = (CheckBox)grdAllotment.HeaderRow.FindControl("chkDropH");
                chkDropH.Checked = true;
            }
        }
    }
    protected void ddlVehicleH_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlVehicleH = (DropDownList)grdAllotment.HeaderRow.FindControl("ddlVehicleH");
        DropDownList ddlLocationH = (DropDownList)grdAllotment.HeaderRow.FindControl("ddlLocationH");
        for (int i = 0; i < grdAllotment.Rows.Count; i++)
        {
            CheckBox chkPickupL = (CheckBox)grdAllotment.Rows[i].FindControl("chkPickupL");
            CheckBox chkDropL = (CheckBox)grdAllotment.Rows[i].FindControl("chkDropL");
            if (chkPickupL.Enabled == true)
            {
                DropDownList ddlVehicle = (DropDownList)grdAllotment.Rows[i].FindControl("ddlVehicle");
                DropDownList ddlLocation = (DropDownList)grdAllotment.Rows[i].FindControl("ddlLocation");

                TextBox lblTotalAmount = (TextBox)grdAllotment.Rows[i].FindControl("lblTotalAmount");
                ddlVehicle.SelectedValue = ddlVehicleH.SelectedValue;
                if (ddlVehicleH.SelectedIndex == 0)
                {

                    chkPickupL.Checked = false;
                    chkDropL.Checked = false;
                    lblTotalAmount.Text = "";
                    ddlLocation.Items.Clear();
                    ddlLocation.Items.Insert(0, new ListItem("<--Location-->", "0"));

                }
                else
                {

                    loadLocation(ddlVehicleH.SelectedValue,i);

                }
            }
        }
        if (ddlVehicleH.SelectedIndex == 0)
        {
            ddlLocationH.Items.Clear();
            ddlLocationH.Items.Insert(0, new ListItem("<--Location-->", "0"));
        }
        else
        {
            DropDownList ddlLocationHs = (DropDownList)grdAllotment.HeaderRow.FindControl("ddlLocationH");
            ddlLocationHs.Items.Clear();
            oo.FillDropDown_withValue(sql, ddlLocationHs, "location", "locationId");
            ddlLocationHs.Items.Insert(0, new ListItem("<--Location-->", "0"));
        }
        Label lblGTotalAmount = (Label)grdAllotment.FooterRow.FindControl("lblGTotalAmount");
        lblGTotalAmount.Text = "";
    }
    protected void ddlVehicle_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList chk = (DropDownList)sender;

        DropDownList ddlVehicle = (DropDownList)chk.NamingContainer.FindControl("ddlVehicle");
        DropDownList ddlLocation = (DropDownList)chk.NamingContainer.FindControl("ddlLocation");
        CheckBox chkPickupL = (CheckBox)chk.NamingContainer.FindControl("chkPickupL");
        CheckBox chkDropL = (CheckBox)chk.NamingContainer.FindControl("chkDropL");
        TextBox lblTotalAmount = (TextBox)chk.NamingContainer.FindControl("lblTotalAmount");


        chkPickupL.Checked = false;
        chkDropL.Checked = false;
        lblTotalAmount.Text = "";
        double Gtotal = 0;
        if (ddlVehicle.SelectedIndex > 0)
        {
            sql = "SELECT locationId, locationName+ ' ( '+ CONVERT(nvarchar(max), distanceInKm)+' )' location FROM LocationMaster where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and locationId in (select distinct locationId from locationMapping where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + ") ORDER by locationId asc";
            for (int i = 0; i < grdAllotment.Rows.Count; i++)
            {
                ddlLocation.Items.Clear();
                oo.FillDropDown_withValue(sql, ddlLocation, "location", "locationId");
                ddlLocation.Items.Insert(0, new ListItem("<--Location-->", "0"));
            }
        }
        else
        {
            ddlLocation.Items.Clear();
            ddlLocation.Items.Insert(0, new ListItem("<--Location-->", "0"));
        }
        for (int i = 0; i < grdAllotment.Rows.Count; i++)
        {
            double total = 0;
            TextBox lbltotal = (TextBox)grdAllotment.Rows[i].FindControl("lblTotalAmount");
            double.TryParse(lbltotal.Text.Trim(), out total);
            Gtotal = Gtotal + total;
        }
        Label lblGTotalAmount = (Label)grdAllotment.FooterRow.FindControl("lblGTotalAmount");
        lblGTotalAmount.Text = Gtotal.ToString("0.00");
    }
    protected void ddlLocationH_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlLocationH = (DropDownList)grdAllotment.HeaderRow.FindControl("ddlLocationH");
        CheckBox chkPickupH = (CheckBox)grdAllotment.HeaderRow.FindControl("chkPickupH");
        CheckBox chkDropH = (CheckBox)grdAllotment.HeaderRow.FindControl("chkDropH");
        chkPickupH.Checked = false;
        chkDropH.Checked = false;
        for (int i = 0; i < grdAllotment.Rows.Count; i++)
        {
            CheckBox chkPickupL = (CheckBox)grdAllotment.Rows[i].FindControl("chkPickupL");
            CheckBox chkDropL = (CheckBox)grdAllotment.Rows[i].FindControl("chkDropL");
            if (chkPickupL.Enabled == true)
            {
                DropDownList ddlLocation = (DropDownList)grdAllotment.Rows[i].FindControl("ddlLocation");
                ddlLocation.SelectedValue = ddlLocationH.SelectedValue;
                TextBox lblTotalAmount = (TextBox)grdAllotment.Rows[i].FindControl("lblTotalAmount");
                chkPickupL.Checked = false;
                chkDropL.Checked = false;
                lblTotalAmount.Text = "";
            }          

        }
        Label lblGTotalAmount = (Label)grdAllotment.FooterRow.FindControl("lblGTotalAmount");
        lblGTotalAmount.Text = "";
    }
    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList chk = (DropDownList)sender;
        DropDownList ddlLocation = (DropDownList)chk.NamingContainer.FindControl("ddlLocation");
        CheckBox chkPickupL = (CheckBox)chk.NamingContainer.FindControl("chkPickupL");
        CheckBox chkDropL = (CheckBox)chk.NamingContainer.FindControl("chkDropL");
        TextBox lblTotalAmount = (TextBox)chk.NamingContainer.FindControl("lblTotalAmount");
        CheckBox chkPickupH = (CheckBox)grdAllotment.HeaderRow.FindControl("chkPickupH");
        CheckBox chkDropH = (CheckBox)grdAllotment.HeaderRow.FindControl("chkDropH");
        chkPickupH.Checked = false;
        chkDropH.Checked = false;
        chkPickupL.Checked = false;
        chkDropL.Checked = false;
        lblTotalAmount.Text = "";
        double Gtotal = 0;
        for (int i = 0; i < grdAllotment.Rows.Count; i++)
        {
            double total = 0;
            TextBox lbltotal = (TextBox)grdAllotment.Rows[i].FindControl("lblTotalAmount");
            double.TryParse(lbltotal.Text.Trim(), out total);
            Gtotal = Gtotal + total;
        }
        Label lblGTotalAmount = (Label)grdAllotment.FooterRow.FindControl("lblGTotalAmount");
        lblGTotalAmount.Text = Gtotal.ToString("0.00");
    }
    protected void chkPickupH_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkPickupH = (CheckBox)grdAllotment.HeaderRow.FindControl("chkPickupH");
        double Gtotal = 0;
        for (int i = 0; i < grdAllotment.Rows.Count; i++)
        {
            DropDownList ddlVehicle = (DropDownList)grdAllotment.Rows[i].FindControl("ddlVehicle");
            DropDownList ddlLocation = (DropDownList)grdAllotment.Rows[i].FindControl("ddlLocation");
            CheckBox chkPickupL = (CheckBox)grdAllotment.Rows[i].FindControl("chkPickupL");
            CheckBox chkDropL = (CheckBox)grdAllotment.Rows[i].FindControl("chkDropL");
            TextBox lblTotalAmount = (TextBox)grdAllotment.Rows[i].FindControl("lblTotalAmount");
            if (chkPickupL.Enabled == true)
            {
                if (chkPickupH.Checked)
                {
                    chkPickupL.Checked = true;
                }
                else
                {
                    chkPickupL.Checked = false;
                }
                if (ddlVehicle.SelectedIndex > 0 && ddlLocation.SelectedIndex > 0)
                {
                    string ss = "select oneWayAmt, twoWayAmt from LocationMaster where SessionName='" + Session["SessionName"] + "' and  BranchCode=" + Session["BranchCode"] + " and LocationId=" + ddlLocation.SelectedValue + "  and locationId in (select distinct locationId from locationMapping where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + ")";
                    double oneWayAmt = 0; double twoWayAmt = 0; double totalAmt = 0;
                    double.TryParse(oo.ReturnTag(ss, "oneWayAmt"), out oneWayAmt);
                    double.TryParse(oo.ReturnTag(ss, "twoWayAmt"), out twoWayAmt);
                    if (chkPickupL.Checked && chkDropL.Checked)
                    {
                        totalAmt = twoWayAmt;
                    }
                    if ((chkPickupL.Checked && !chkDropL.Checked) || (!chkPickupL.Checked && chkDropL.Checked))
                    {
                        totalAmt = oneWayAmt;
                    }
                    lblTotalAmount.Text = totalAmt.ToString("0.00");

                    double total = 0;
                    double.TryParse(lblTotalAmount.Text.Trim(), out total);
                    Gtotal = Gtotal + total;

                }
            }
        }
        Label lblGTotalAmount = (Label)grdAllotment.FooterRow.FindControl("lblGTotalAmount");
        lblGTotalAmount.Text = Gtotal.ToString("0.00");

    }
    protected void chkPickupL_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = (CheckBox)sender;
        DropDownList ddlVehicle = (DropDownList)chk.NamingContainer.FindControl("ddlVehicle");
        DropDownList ddlLocation = (DropDownList)chk.NamingContainer.FindControl("ddlLocation");
        CheckBox chkPickupL = (CheckBox)chk.NamingContainer.FindControl("chkPickupL");
        CheckBox chkDropL = (CheckBox)chk.NamingContainer.FindControl("chkDropL");
        CheckBox chkPickupH = (CheckBox)grdAllotment.HeaderRow.FindControl("chkPickupH");

        TextBox lblTotalAmount = (TextBox)chk.NamingContainer.FindControl("lblTotalAmount");
        if (ddlVehicle.SelectedIndex > 0 && ddlLocation.SelectedIndex > 0)
        {
            string ss = "select oneWayAmt, twoWayAmt from LocationMaster where SessionName='" + Session["SessionName"] + "' and  BranchCode=" + Session["BranchCode"] + " and LocationId=" + ddlLocation.SelectedValue + " and locationId in (select distinct locationId from locationMapping where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + ")";
            double oneWayAmt = 0; double twoWayAmt = 0; double totalAmt = 0;
            double.TryParse(oo.ReturnTag(ss, "oneWayAmt"), out oneWayAmt);
            double.TryParse(oo.ReturnTag(ss, "twoWayAmt"), out twoWayAmt);
            if (chkPickupL.Checked && chkDropL.Checked)
            {

                totalAmt = twoWayAmt;
            }
            if ((chkPickupL.Checked && !chkDropL.Checked) || (!chkPickupL.Checked && chkDropL.Checked))
            {
                totalAmt = oneWayAmt;
            }
            lblTotalAmount.Text = totalAmt.ToString("0.00");
            double Gtotal = 0; int chkPcount = 0;
            for (int i = 0; i < grdAllotment.Rows.Count; i++)
            {
                double total = 0;
                TextBox lbltotal = (TextBox)grdAllotment.Rows[i].FindControl("lblTotalAmount");
                double.TryParse(lbltotal.Text.Trim(), out total);
                Gtotal = Gtotal + total;
                CheckBox chkPickupLs = (CheckBox)grdAllotment.Rows[i].FindControl("chkPickupL");
                if (chkPickupLs.Checked)
                {
                    chkPcount = chkPcount + 1;
                }
            }
            if (chkPcount == grdAllotment.Rows.Count)
            {
                chkPickupH.Checked = true;
            }
            else
            {
                chkPickupH.Checked = false;
            }

            Label lblGTotalAmount = (Label)grdAllotment.FooterRow.FindControl("lblGTotalAmount");
            lblGTotalAmount.Text = Gtotal.ToString("0.00");
        }
    }
    protected void chkDropH_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkDropH = (CheckBox)grdAllotment.HeaderRow.FindControl("chkDropH");
        double Gtotal = 0;
        for (int i = 0; i < grdAllotment.Rows.Count; i++)
        {
            DropDownList ddlVehicle = (DropDownList)grdAllotment.Rows[i].FindControl("ddlVehicle");
            DropDownList ddlLocation = (DropDownList)grdAllotment.Rows[i].FindControl("ddlLocation");
            CheckBox chkPickupL = (CheckBox)grdAllotment.Rows[i].FindControl("chkPickupL");
            CheckBox chkDropL = (CheckBox)grdAllotment.Rows[i].FindControl("chkDropL");
            TextBox lblTotalAmount = (TextBox)grdAllotment.Rows[i].FindControl("lblTotalAmount");
            if (chkDropL.Enabled == true)
            {
                if (chkDropH.Checked)
                {
                    chkDropL.Checked = true;
                }
                else
                {
                    chkDropL.Checked = false;
                }

                if (ddlVehicle.SelectedIndex > 0 && ddlLocation.SelectedIndex > 0)
                {
                    string ss = "select oneWayAmt, twoWayAmt from LocationMaster where SessionName='" + Session["SessionName"] + "' and  BranchCode=" + Session["BranchCode"] + " and LocationId=" + ddlLocation.SelectedValue + " and locationId in (select distinct locationId from locationMapping where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + ")";
                    double oneWayAmt = 0; double twoWayAmt = 0; double totalAmt = 0;
                    double.TryParse(oo.ReturnTag(ss, "oneWayAmt"), out oneWayAmt);
                    double.TryParse(oo.ReturnTag(ss, "twoWayAmt"), out twoWayAmt);
                    if (chkPickupL.Checked && chkDropL.Checked)
                    {
                        totalAmt = twoWayAmt;
                    }
                    if ((chkPickupL.Checked && !chkDropL.Checked) || (!chkPickupL.Checked && chkDropL.Checked))
                    {
                        totalAmt = oneWayAmt;
                    }
                    lblTotalAmount.Text = totalAmt.ToString("0.00");
                    double total = 0;
                    double.TryParse(lblTotalAmount.Text.Trim(), out total);
                    Gtotal = Gtotal + total;
                }
            }
        }
        Label lblGTotalAmount = (Label)grdAllotment.FooterRow.FindControl("lblGTotalAmount");
        lblGTotalAmount.Text = Gtotal.ToString("0.00");
    }
    protected void chkDropL_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = (CheckBox)sender;
        DropDownList ddlVehicle = (DropDownList)chk.NamingContainer.FindControl("ddlVehicle");
        DropDownList ddlLocation = (DropDownList)chk.NamingContainer.FindControl("ddlLocation");
        CheckBox chkPickupL = (CheckBox)chk.NamingContainer.FindControl("chkPickupL");
        CheckBox chkDropL = (CheckBox)chk.NamingContainer.FindControl("chkDropL");
        TextBox lblTotalAmount = (TextBox)chk.NamingContainer.FindControl("lblTotalAmount");
        CheckBox chkDropH = (CheckBox)grdAllotment.HeaderRow.FindControl("chkDropH");
        if (ddlVehicle.SelectedIndex > 0 && ddlLocation.SelectedIndex > 0)
        {
            string ss = "select oneWayAmt, twoWayAmt from LocationMaster where SessionName='" + Session["SessionName"] + "' and  BranchCode=" + Session["BranchCode"] + " and LocationId=" + ddlLocation.SelectedValue + " and locationId in (select distinct locationId from locationMapping where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + ")";
            double oneWayAmt = 0; double twoWayAmt = 0; double totalAmt = 0;
            double.TryParse(oo.ReturnTag(ss, "oneWayAmt"), out oneWayAmt);
            double.TryParse(oo.ReturnTag(ss, "twoWayAmt"), out twoWayAmt);
            if (chkPickupL.Checked && chkDropL.Checked)
            {
                totalAmt = twoWayAmt;
            }
            if ((chkPickupL.Checked && !chkDropL.Checked) || (!chkPickupL.Checked && chkDropL.Checked))
            {
                totalAmt = oneWayAmt;
            }
            lblTotalAmount.Text = totalAmt.ToString("0.00");
            double Gtotal = 0; int chkDcount = 0;
            for (int i = 0; i < grdAllotment.Rows.Count; i++)
            {
                double total = 0;
                TextBox lbltotal = (TextBox)grdAllotment.Rows[i].FindControl("lblTotalAmount");
                double.TryParse(lbltotal.Text.Trim(), out total);
                Gtotal = Gtotal + total;
                CheckBox chkDropLs = (CheckBox)grdAllotment.Rows[i].FindControl("chkDropL");
                if (chkDropLs.Checked)
                {
                    chkDcount = chkDcount + 1;
                }
            }
            if (chkDcount == grdAllotment.Rows.Count)
            {
                chkDropH.Checked = true;
            }
            else
            {
                chkDropH.Checked = false;
            }
            Label lblGTotalAmount = (Label)grdAllotment.FooterRow.FindControl("lblGTotalAmount");
            lblGTotalAmount.Text = Gtotal.ToString("0.00");
        }
    }
    public void BindData(string srno)
    {
        string _sql = "Select * from StudentOfficialDetails where blocked='Yes' and srno='" + srno + "' and SessionName='" + Session["SessionName"] + "' and branchcode=" + Session["BranchCode"] + "";
        var sql1 = "Select Promotion,MODForFeeDeposit  from StudentOfficialDetails where srno='" + srno + "' and SessionName='" + Session["SessionName"] + "' and branchcode=" + Session["BranchCode"] + "";
        var _sql2 = "select isnull(Withdrwal, '') Withdrwal from StudentOfficialDetails where srno='" + srno + "' and SessionName='" + Session["SessionName"] + "' and branchcode=" + Session["BranchCode"] + "";
        var ds = BLL.BLLInstance.GetStudentDetails(srno, Session["SessionName"].ToString(), Session["BranchCode"].ToString());
        studentInfoGrid.DataSource = ds;
        studentInfoGrid.DataBind();

        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = BAL.objBal.dbGet_connection().ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "USP_StudentsPhotoReport";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@sessionName", Session["SessionName"].ToString().Trim());
                    cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString().Trim());
                    cmd.Parameters.AddWithValue("@SrNo", srno.ToString().Trim());

                    cmd.Parameters.AddWithValue("@action", "details");
                    SqlDataAdapter das = new SqlDataAdapter(cmd);
                    DataSet dsPhoto = new DataSet();
                    das.Fill(dsPhoto);
                    cmd.Parameters.Clear();

                    if (dsPhoto.Tables[0].Rows.Count > 0)
                    {
                        divStudent.Visible = true;
                        img.ImageUrl = dsPhoto.Tables[0].Rows[0]["PhotoPath"].ToString() != string.Empty ? dsPhoto.Tables[0].Rows[0]["PhotoPath"] + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture) : "../img/user-pic/user-pic.jpg" + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture);
                        studentImg.NavigateUrl = dsPhoto.Tables[0].Rows[0]["PhotoPath"].ToString() != string.Empty ? dsPhoto.Tables[0].Rows[0]["PhotoPath"] + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture) : "../img/user-pic/user-pic.jpg" + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture);
                        hylinkmoredetails.NavigateUrl = "../11/StudentRegView.aspx?print=1&id=" + ds.Tables[0].Rows[0]["stenrcode"];
                        string ss = "select count(*) cnt from CompositFeeDeposit where SrNo='" + srno + "' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and receiptStatus='Paid'";
                    }
                }
            }
        }

        if (studentInfoGrid.Rows.Count == 0)
        { Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, No Record(s) found!", "A"); detailsDiv.Visible = false; }
        else
        {
            Load();
            detailsDiv.Visible = true;

            for (int i = 0; i < grdAllotment.Rows.Count; i++)
            {
                DropDownList ddlloc = (DropDownList)grdAllotment.Rows[i].FindControl("ddlLocation");
                ddlloc.Items.Insert(0, new ListItem("<--Location-->", "0"));
            }
            DropDownList ddlLocationHs = (DropDownList)grdAllotment.HeaderRow.FindControl("ddlLocationH");
            ddlLocationHs.Items.Insert(0, new ListItem("<--Location-->", "0"));
        }
    }
    protected void Load()
    {
        Label lblsrno = (Label)studentInfoGrid.Rows[0].FindControl("Label6");
        srno = lblsrno.Text;
        try
        {
            sql = "Select Card,ClassId,classname,BranchId,CardId from";
            sql = sql + " AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "'," + Session["BranchCode"].ToString() + ")";
            sql = sql + " where SrNo='" + lblsrno.Text + "'";
            string card = oo.ReturnTag(sql, "Card");

            string classname = oo.ReturnTag(sql, "classname");
            loadInitialGrid(card, classname, grdAllotment);
            GetInfoOfStudent();
        }
        catch (Exception ex)
        {
        }

    }
    protected void Linksubmit_Click(object sender, EventArgs e)
    {
        try
        {
            Label srno = (Label)studentInfoGrid.Rows[0].FindControl("Label6");
            int y = 0; int yy = 0;
            for (int i = 0; i < grdAllotment.Rows.Count; i++)
            {
                Label Insttalment = (Label)grdAllotment.Rows[i].FindControl("Insttalment");
                Label InstallmentId = (Label)grdAllotment.Rows[i].FindControl("InstallmentId");
                DropDownList ddlVehicle = (DropDownList)grdAllotment.Rows[i].FindControl("ddlVehicle");
                DropDownList ddlLocation = (DropDownList)grdAllotment.Rows[i].FindControl("ddlLocation");
                CheckBox chkPickupL = (CheckBox)grdAllotment.Rows[i].FindControl("chkPickupL");
                CheckBox chkDropL = (CheckBox)grdAllotment.Rows[i].FindControl("chkDropL");
                TextBox lblTotalAmount = (TextBox)grdAllotment.Rows[i].FindControl("lblTotalAmount");
                if (ddlVehicle.SelectedIndex > 0 && ddlLocation.SelectedIndex > 0 && (chkPickupL.Checked || chkDropL.Checked))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "StudentVehicleAllotmentProc";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@SrNo", srno.Text);
                    cmd.Parameters.AddWithValue("@VehicleId", ddlVehicle.SelectedValue);
                    cmd.Parameters.AddWithValue("@LocationId", ddlLocation.SelectedValue);
                    cmd.Parameters.AddWithValue("@InstallmentId", InstallmentId.Text);
                    cmd.Parameters.AddWithValue("@PickupStatus", (chkPickupL.Checked ? "1" : "0"));
                    cmd.Parameters.AddWithValue("@DropStatus", (chkDropL.Checked ? "1" : "0"));
                    cmd.Parameters.AddWithValue("@amount", lblTotalAmount.Text.Trim());
                    cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                    cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                    cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                    cmd.Parameters.AddWithValue("@Action", "insert");
                    yy = yy + 1;
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    con.Close();

                }
                else
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "StudentVehicleAllotmentProc";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@SrNo", srno.Text);
                    cmd.Parameters.AddWithValue("@InstallmentId", InstallmentId.Text);
                    cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                    cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                    cmd.Parameters.AddWithValue("@Action", "delete");
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    con.Close();
                }
            }
            if (yy > 0)
            {
                Campus camp = new Campus(); camp.msgbox(this.Page, Div2, "Submitted successfully.", "S");
                BindData(srno.Text);
            }
            else
            {
                Campus camp = new Campus(); camp.msgbox(this.Page, Div2, "Not Submitted!", "A");
            }
        }
        catch (Exception ex)
        {
        }
    }
}



