using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SetAmountForPickupDropPoint : Page
{
    SqlConnection con = new SqlConnection();
    SqlCommand cmd;
    Campus obj = new Campus();
    string sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((string)Session["LoginName"] == "" || (string)Session["BranchCode"] == "" || (string)Session["SessionName"] == "" || Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        con = obj.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);
        if (!IsPostBack)
        {
            if (HiddenField1.Value != "1")
            {
                table1.Attributes.Add("style", "Display:none;");
            }
            loadClass();
            loadFeeGroup();
            loadRoute();
            loadVehicle();
            loadVehicleNo();
           
        }
    }
    protected void loadRoute()
    {
        sql = "Select RouteName,Id from VehicleRouteMaster where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"] + "";
        obj.FillDropDown_withValue(sql, DrpRouteName, "RouteName", "Id");
        obj.FillDropDown_withValue(sql, drpRouteNamePanel, "RouteName", "Id");
        DrpRouteName.Items.Insert(0, "<--Select-->");
        drpRouteNamePanel.Items.Insert(0, "<--Select-->");
    }
    protected void loadVehicle()
    {
        sql = "Select VehicleType,Id from VehicleMaster";
        sql = sql + " where  SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        obj.FillDropDown_withValue(sql, DrpVehicleType, "VehicleType", "Id");
        obj.FillDropDown_withValue(sql, drpVehicleTypePanel, "VehicleType", "Id");
        DrpVehicleType.Items.Insert(0, "<--Select-->");
        drpVehicleTypePanel.Items.Insert(0, "<--Select-->");
    }
    protected void loadVehicleNo()
    {
        sql = "Select VehicleNo,Id from VehicleDetails where VehicleType='" + DrpVehicleType.SelectedItem.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "'";
        obj.FillDropDown_withValue(sql, Drpvehicleno, "VehicleNo", "Id");
        obj.FillDropDown_withValue(sql, drpVehicleNoPanel, "VehicleNo", "Id");
        Drpvehicleno.Items.Insert(0, "<--Select-->");
        drpVehicleNoPanel.Items.Insert(0, "<--Select-->");
    }
    protected void loadLocation()
    {
        if (rbl1.SelectedIndex == 0)
        {
            sql = "Select PickupPointName,PickupPointId from VehiclePickupLocationMaster where RouteId='" + DrpRouteName.SelectedValue.ToString() + "' and VehicleTypeId='" + DrpVehicleType.SelectedValue.ToString() + "'";
            sql = sql + " and VehicleNoid='" + Drpvehicleno.SelectedValue.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            obj.FillDropDown_withValue(sql, Drplocation1, "PickupPointName", "PickupPointId");
            obj.FillDropDown_withValue(sql, Drplocation2, "PickupPointName", "PickupPointId");
            Drplocation1.Items.Insert(0, "<--Select-->");
            Drplocation2.Items.Insert(0, "<--Select-->");
        }
        if(rbl1.SelectedIndex == 1)
        {
            sql = "Select DropPointName,DropPointId from VehicleDropLocationMaster where RouteId='" + DrpRouteName.SelectedValue.ToString() + "' and VehicleTypeId='" + DrpVehicleType.SelectedValue.ToString() + "'";
            sql = sql + " and VehicleNoid='" + Drpvehicleno.SelectedValue.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            obj.FillDropDown_withValue(sql, Drplocation1, "DropPointName", "DropPointId");
            obj.FillDropDown_withValue(sql, Drplocation2, "DropPointName", "DropPointId");
            Drplocation1.Items.Insert(0, "<--Select-->");
            Drplocation2.Items.Insert(0, "<--Select-->");
        }
    }
    protected void loadInsttalment(string feegroup, string classname, string mod)
    {
        sql = "select MonthName from MonthMaster mm";
        sql = sql + " inner join FeeGroupMaster fgm on (Case when ISNUMERIC(CardType)=1 THEN fgm.Id Else fgm.FeeGroupName End)=mm.CardType and fgm.SessionName=mm.SessionName";
        sql = sql + " inner join ClassMaster cm on cm.Id=mm.ClassId and cm.SessionName=mm.SessionName";
        sql = sql + " where fgm.FeeGroupName='" + feegroup + "' and cm.ClassName='" + classname + "' and MOD='" + mod + "'  and mm.SessionName='" + Session["SessionName"].ToString() + "' ";
        sql = sql + " and mm.BranchCode=" + Session["BranchCode"].ToString() + " and cm.BranchCode=" + Session["BranchCode"] + " and fgm.BranchCode=" + Session["BranchCode"] + " or monthid=0 order by MonthId";

        GridView1.DataSource = BAL.objBal.GridFill(sql);
        GridView1.DataBind();
    }

    
    protected void rbl1_SelectedIndexChanged(object sender, EventArgs e)
    {
        //table1.Visible = string.IsNullOrEmpty(rbl1.SelectedValue) ? false : true;
        loadLocation();
        Label1.Text = rbl1.SelectedIndex == 0 ? "Pickup " : "Drop ";
        Label2.Text = rbl1.SelectedIndex == 0 ? "Pickup " : "Drop ";
        GridView2.DataSource = null;
        GridView2.DataBind();
        GridView3.DataSource = null;
        GridView3.DataBind();
    }
    protected void DrpRouteName_SelectedIndexChanged(object sender, EventArgs e)
    {
        table1.Attributes.Add("style", "Display:block;");
        loadVehicle();
    }
    protected void DrpVehicleType_SelectedIndexChanged(object sender, EventArgs e)
    {
        table1.Attributes.Add("style", "Display:block;");
        loadVehicleNo();
    }
    protected void Drpvehicleno_SelectedIndexChanged(object sender, EventArgs e)
    {
        table1.Attributes.Add("style", "Display:block;");
        loadLocation();
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        table1.Attributes.Add("style", "Display:block;");
        insert();        
    }
    private void insert()
    {
        try
        {
            for (int i = Drplocation1.SelectedIndex; i <= Drplocation2.SelectedIndex; i++)
            {
                for (int j = 0; j < GridView1.Rows.Count; j++)
                {
                    Label lblInsttalment = (Label)GridView1.Rows[j].FindControl("lblInsttalment");
                    TextBox txtAmount = (TextBox)GridView1.Rows[j].FindControl("txtAmount");


                    sql = "Select *from LocationWiseVehicleAmount where VehicleId='" + Drpvehicleno.SelectedValue + "'";
                    sql = sql + " and VehicleTypeId='" + DrpVehicleType.SelectedValue + "' and LocationCode='" + Drplocation1.Items[i].Value + "'";
                    sql = sql + " and Insttalment='" + lblInsttalment.Text + "' and Classid='" + DrpClass.SelectedValue.ToString() + "' and BranchId='" + drpBranch.SelectedValue.ToString() + "' and FeeGroupId='" + drpFeeGroup.SelectedValue.ToString() + "' and Mop='I' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"].ToString()+ "' and Way='"+rdoWay.SelectedValue+"'";
                    if (obj.Duplicate(sql) == false)
                    {

                        cmd = new SqlCommand();
                        cmd.CommandText = "LocationWiseVehicleAmountProc";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@Type", "Insert");
                        cmd.Parameters.AddWithValue("@Id", "");
                        cmd.Parameters.AddWithValue("@RouteId", DrpRouteName.SelectedValue.ToString());
                        cmd.Parameters.AddWithValue("@VehicleId", Drpvehicleno.SelectedValue.ToString());
                        cmd.Parameters.AddWithValue("@VehicleTypeId", DrpVehicleType.SelectedValue.ToString());
                        cmd.Parameters.AddWithValue("@LocationCode", Drplocation1.Items[i].Value.ToString());
                        cmd.Parameters.AddWithValue("@Insttalment", lblInsttalment.Text);
                        cmd.Parameters.AddWithValue("@Way", rdoWay.SelectedValue);
                        if (txtAmount.Text != "")
                        {
                            cmd.Parameters.AddWithValue("@Amount", txtAmount.Text.Trim());
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Amount", "0");
                        }
                        cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                        cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                        cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());

                        cmd.Parameters.AddWithValue("@Classid", DrpClass.SelectedValue.ToString());
                        cmd.Parameters.AddWithValue("@BranchId", drpBranch.SelectedValue.ToString());
                        cmd.Parameters.AddWithValue("@FeeGroupId", drpFeeGroup.SelectedValue.ToString());

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        loadgrid();
                        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted Successfully.", "S");
                        //obj.MessageBoxforUpdatePanel("Submitted successfully!", LinkButton1);
                    }
                    else
                    {
                        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Duplicate entry!", "A");
                    }
                }
            }
            obj.ClearControls(ac1);
        }
        catch (Exception ex)
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, ex.Message, "W");
            //obj.MessageBoxforUpdatePanel(ex.Message, LinkButton1);
        }
    
    }
    protected void Delete_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            TextBox txt = (TextBox)GridView1.Rows[i].FindControl("txtAmount");
            txt.Text = "";
        }
    }
    protected void loadgrid()
    {
       
        if (rbl1.SelectedIndex == 0)
        {
            sql = "Select lwva.classid, lwva.Id,RouteName,PickupPointName,vm.VehicleType,vd.VehicleNo,Amount,Insttalment,DisplayOrder, lwva.LocationCode";
            sql = sql + " from LocationWiseVehicleAmount lwva ";
            sql = sql + " inner join VehicleRouteMaster vrm on vrm.Id=lwva.RouteId";
            sql = sql + " inner join VehiclePickupLocationMaster vplm on vplm.PickupPointId=lwva.LocationCode";
            sql = sql + " inner join VehicleMaster vm on vm.Id=vplm.VehicleTypeId";
            sql = sql + " inner join VehicleDetails vd on vd.VehicleType=vm.VehicleType and vd.Id=vplm.VehicleNoId";
            sql = sql + " where (lwva.ClassId='" + DrpClass.SelectedValue.ToString() + "' or lwva.Classid is null) and (lwva.BranchId='" + drpBranch.SelectedValue.ToString() + "' or lwva.BranchId is null) and vplm.SessionName='" + Session["SessionName"].ToString() + "' and vrm.SessionName='" + Session["SessionName"].ToString() + "'  ";
            sql = sql + " and lwva.SessionName='" + Session["SessionName"].ToString() + "' and lwva.BranchCode=" + Session["BranchCode"] + " and lwva.LocationCode='" + Drplocation1.SelectedValue + "' and lwva.way='" + rdoWay.SelectedValue + "' order by DisplayOrder Asc";
            
            GridView3.DataSource = null;
            GridView3.DataBind();
            GridView2.DataSource = obj.GridFill(sql);
            GridView2.DataBind();
            for (int i = 0; i < GridView2.Rows.Count; i++)
            {
                Label lblClassIds = (Label)GridView2.Rows[i].FindControl("lblClassId1s");
                sql = "select distinct ClassId from VW_AllStudentRecord where SrNo in (select distinct SrNo from StudentVehicleAllotment where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + ") and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and ClassId=" + lblClassIds.Text + "";
                if (obj.Duplicate(sql))
                {
                    LinkButton LinkButton2 = (LinkButton)GridView2.Rows[i].FindControl("LinkButton2");
                    LinkButton LinkButton3 = (LinkButton)GridView2.Rows[i].FindControl("LinkButton3");
                    LinkButton2.Text = "<i class='fa fa-lock'></i>";
                    LinkButton3.Text = "<i class='fa fa-lock'></i>";
                    LinkButton2.Enabled = false;
                    LinkButton3.Enabled = false;
                }
            }
        }
        else if (rbl1.SelectedIndex == 1)
        {
            sql = "Select lwva.classid, lwva.Id,RouteName,DropPointName,vm.VehicleType,vd.VehicleNo,Amount,Insttalment,DisplayOrder, lwva.LocationCode";
            sql = sql + " from LocationWiseVehicleAmount lwva ";
            sql = sql + " inner join VehicleRouteMaster vrm on vrm.Id=lwva.RouteId";
            sql = sql + " inner join VehicleDropLocationMaster vdlm on vdlm.DropPointId=lwva.LocationCode";
            sql = sql + " inner join VehicleMaster vm on vm.Id=vdlm.VehicleTypeId";
            sql = sql + " inner join VehicleDetails vd on vd.VehicleType=vm.VehicleType and vd.Id=vdlm.VehicleNoId";
            sql = sql + " where (lwva.ClassId='" + DrpClass.SelectedValue.ToString() + "' or lwva.Classid is null) and (lwva.BranchId='" + drpBranch.SelectedValue.ToString() + "' or lwva.BranchId is null) and vdlm.SessionName='" + Session["SessionName"].ToString() + "' and vrm.SessionName='" + Session["SessionName"].ToString() + "'  ";
            sql = sql + " and lwva.SessionName='" + Session["SessionName"].ToString() + "' and lwva.BranchCode=" + Session["BranchCode"] + " and lwva.LocationCode='" + Drplocation1.SelectedValue + "' and lwva.way='"+rdoWay.SelectedValue+"' order by DisplayOrder Asc";

            GridView2.DataSource = null;
            GridView2.DataBind();
            GridView3.DataSource = obj.GridFill(sql);
            GridView3.DataBind();
            for (int i = 0; i < GridView3.Rows.Count; i++)
            {
                Label lblClassIds = (Label)GridView3.Rows[i].FindControl("lblClassId2s");
                sql = "select distinct ClassId from VW_AllStudentRecord where SrNo in (select distinct SrNo from StudentVehicleAllotment where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + ") and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and ClassId=" + lblClassIds.Text + "";
                if (obj.Duplicate(sql))
                {
                    LinkButton LinkButton2 = (LinkButton)GridView3.Rows[i].FindControl("LinkButton4");
                    LinkButton LinkButton3 = (LinkButton)GridView3.Rows[i].FindControl("LinkButton5");
                    LinkButton2.Text = "<i class='fa fa-lock'></i>";
                    LinkButton3.Text = "<i class='fa fa-lock'></i>";
                    LinkButton2.Enabled = false;
                    LinkButton3.Enabled = false;
                }
            }
        }
        
    }  
    protected void Drplocation1_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadgrid();
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("Label36");
        lblValue.Text = lblId.Text;

        sql = "Select RouteId,VehicleTypeId,Vehicleid,Amount,Insttalment  from LocationWiseVehicleAmount where Id='" + lblValue.Text + "' and way='" + rdoWay.SelectedValue + "' and BranchCode=" + Session["BranchCode"] + "";
        drpRouteNamePanel.SelectedValue = obj.ReturnTag(sql, "RouteId");
        drpVehicleTypePanel.SelectedValue = obj.ReturnTag(sql, "VehicleTypeId");
        drpVehicleNoPanel.SelectedValue = obj.ReturnTag(sql, "Vehicleid");
        lblInsttalmentPanel.Text = obj.ReturnTag(sql, "Insttalment");   
        txtAmountPanel.Text = obj.ReturnTag(sql, "Amount");
        loactionPanel();
        sql = "Select RouteId,VehicleTypeId,Vehicleid,Amount,Insttalment,LocationCode  from LocationWiseVehicleAmount where Id='" + lblValue.Text + "' and way='" + rdoWay.SelectedValue + "' and BranchCode=" + Session["BranchCode"] + "";
        drpLocationPanel.SelectedValue = obj.ReturnTag(sql, "LocationCode");
       
        Panel1_ModalPopupExtender.Show();
    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblId2 = (Label)chk.NamingContainer.FindControl("Label37");
        lblid.Text = lblId2.Text;
        Panel2_ModalPopupExtender.Show();
        LinkButton9.Focus();
    }
    protected void LinkButton8_Click(object sender, EventArgs e)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "LocationWiseVehicleAmountProc";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
        cmd.Parameters.AddWithValue("@Type", "Delete");
        cmd.Parameters.AddWithValue("@Id", lblid.Text);
        cmd.Parameters.AddWithValue("@RouteId", "");
        cmd.Parameters.AddWithValue("@VehicleId", "");
        cmd.Parameters.AddWithValue("@VehicleTypeId", "");
        cmd.Parameters.AddWithValue("@LocationCode", "");
        cmd.Parameters.AddWithValue("@Insttalment", "");
        cmd.Parameters.AddWithValue("@Amount", "");
        cmd.Parameters.AddWithValue("@SessionName", "");
        cmd.Parameters.AddWithValue("@BranchCode", "");
        cmd.Parameters.AddWithValue("@LoginName", "");
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        loadgrid();
        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Deleted successfully!", "S");
        //obj.MessageBoxforUpdatePanel("Deleted successfully!", LinkButton8);
    }
    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblId3 = (Label)chk.NamingContainer.FindControl("Label38");
        lblValue.Text = lblId3.Text;

        sql = "Select RouteId,VehicleTypeId,Vehicleid,Amount,Insttalment  from LocationWiseVehicleAmount where Id='" + lblValue.Text + "' and BranchCode=" + Session["BranchCode"] + "";
        drpRouteNamePanel.SelectedValue = obj.ReturnTag(sql, "RouteId");
        drpVehicleTypePanel.SelectedValue = obj.ReturnTag(sql, "VehicleTypeId");
        drpVehicleNoPanel.SelectedValue = obj.ReturnTag(sql, "Vehicleid");
        lblInsttalmentPanel.Text = obj.ReturnTag(sql, "Insttalment");
        txtAmountPanel.Text = obj.ReturnTag(sql, "Amount");
        loactionPanel();
        sql = "Select RouteId,VehicleTypeId,Vehicleid,Amount,Insttalment,LocationCode  from LocationWiseVehicleAmount where Id='" + lblValue.Text + "' and BranchCode=" + Session["BranchCode"] + "";
        drpLocationPanel.SelectedValue = obj.ReturnTag(sql, "LocationCode");

        Panel1_ModalPopupExtender.Show();
    }

    protected void LinkButton5_Click(object sender, EventArgs e)
    { }

    protected void drpVehicleNoPanel_SelectedIndexChanged(object sender, EventArgs e)
    {
        //table1.Attributes.Add("style", "Display:block;");
        loactionPanel();
    }
    private void loactionPanel()
    {
        if (rbl1.SelectedIndex == 0)
        {
            sql = "Select PickupPointName,PickupPointId from VehiclePickupLocationMaster where RouteId='" + drpRouteNamePanel.SelectedValue.ToString() + "' and VehicleTypeId='" + drpVehicleTypePanel.SelectedValue.ToString() + "'";
            sql = sql + " and VehicleNoid='" + drpVehicleNoPanel.SelectedValue.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            obj.FillDropDown_withValue(sql, drpLocationPanel, "PickupPointName", "PickupPointId");
        }
        if (rbl1.SelectedIndex == 1)
        {
            sql = "Select DropPointName,DropPointId from VehicleDropLocationMaster where RouteId='" + drpRouteNamePanel.SelectedValue.ToString() + "' and VehicleTypeId='" + drpVehicleTypePanel.SelectedValue.ToString() + "'";
            sql = sql + " and VehicleNoid='" + drpVehicleNoPanel.SelectedValue.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            obj.FillDropDown_withValue(sql, drpLocationPanel, "DropPointName", "DropPointId");
        }
    }
    protected void LinkButton6_Click(object sender, EventArgs e)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "LocationWiseVehicleAmountProc";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
        cmd.Parameters.AddWithValue("@Type", "Update");
        cmd.Parameters.AddWithValue("@Id", lblValue.Text);
        cmd.Parameters.AddWithValue("@RouteId", drpRouteNamePanel.SelectedValue.ToString());
        cmd.Parameters.AddWithValue("@VehicleId", drpVehicleNoPanel.SelectedValue.ToString());
        cmd.Parameters.AddWithValue("@VehicleTypeId", drpVehicleTypePanel.SelectedValue.ToString());
        cmd.Parameters.AddWithValue("@LocationCode", drpLocationPanel.SelectedValue.ToString());
        cmd.Parameters.AddWithValue("@Insttalment", lblInsttalmentPanel.Text);
        if (txtAmountPanel.Text != "")
        {
            cmd.Parameters.AddWithValue("@Amount", txtAmountPanel.Text.Trim());
        }
        else
        {
            cmd.Parameters.AddWithValue("@Amount", "0");
        }
        cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
        cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
        cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        loadgrid();
        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully!", "S");
        //obj.MessageBoxforUpdatePanel("Updated successfully!", LinkButton1);
    }
    protected void Drplocation2_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadInsttalment(drpFeeGroup.SelectedItem.Text.Trim(), DrpClass.SelectedItem.Text.ToString(), "I");
    }
    protected void DrpClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadBranch();
        loadgrid();
    }

    private void loadClass()
    {
        sql = "Select Id,ClassName from ClassMaster";
        sql = sql + " where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and CIDOrder !=0 ";
        BAL.objBal.FillDropDown_withValue(sql, DrpClass, "ClassName", "Id");
        DrpClass.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
    }

    private void loadFeeGroup()
    {
        sql = "Select FeeGroupName,id from FeeGroupMaster where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        BAL.objBal.FillDropDown_withValue(sql, drpFeeGroup, "FeeGroupName","Id");
        drpFeeGroup.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
    }

    private void loadBranch()
    {
        sql = "Select BranchName,Id from BranchMaster";
        sql = sql + " where (ClassId='" + DrpClass.SelectedValue.ToString() + "' or ClassId is NULL) and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

        BAL.objBal.FillDropDown_withValue(sql, drpBranch, "BranchName", "Id");
        drpBranch.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
    }
    protected void drpBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadgrid();
    }
}