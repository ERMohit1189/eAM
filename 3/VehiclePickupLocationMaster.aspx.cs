using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;


public partial class admin_VehiclePickupLocationMaster : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    SqlCommand cmd;
    Campus oo = new Campus();
    string sql = "";
    public static string BranchCode = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((string)Session["LoginName"] == "" || (string)Session["BranchCode"] == "" || (string)Session["SessionName"] == "" || Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        con = oo.dbGet_connection();
        if (!IsPostBack)
        {
            loadRoute();          
            loadVehicle();
            Drpvehicleno.Items.Insert(0, "<--Select-->");
            loadgrid();
            BranchCode = Session["BranchCode"].ToString();
        }
    }

    #region Method for loaddropdown
    protected void loadVehicle()
    {
        sql = "Select VehicleType,Id from VehicleMaster";
        sql = sql + " where  SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        oo.FillDropDown_withValue(sql, DrpVehicleType, "VehicleType", "Id");
        DrpVehicleType.Items.Insert(0, "<--Select-->");
    }
    protected void loadVehicle0()
    {
        sql = "Select VehicleType,Id from VehicleMaster";
        sql = sql + " where  SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        oo.FillDropDown_withValue(sql, DrpVehicleType0, "VehicleType", "Id");
        DrpVehicleType0.Items.Insert(0, "<--Select-->");
    }
    protected void loadVehicleNo()
    {
        sql = "Select VehicleNo,Id from VehicleDetails where VehicleType='" + DrpVehicleType.SelectedItem.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        oo.FillDropDown_withValue(sql, Drpvehicleno, "VehicleNo", "Id");
        Drpvehicleno.Items.Insert(0, "<--Select-->");
    }
    protected void loadVehicleNo0()
    {
        sql = "Select VehicleNo,Id from VehicleDetails where VehicleType='" + DrpVehicleType0.SelectedItem.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        oo.FillDropDown_withValue(sql, Drpvehicleno0, "VehicleNo", "Id");
        Drpvehicleno0.Items.Insert(0, "<--Select-->");
    }
    protected void loadRoute()
    {
        sql = "Select RouteName,Id from VehicleRouteMaster where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        oo.FillDropDown_withValue(sql, DrpRouteName, "RouteName", "Id");
        oo.FillDropDown_withValue(sql, DrpRouteName0, "RouteName", "Id");
        DrpRouteName.Items.Insert(0, "<--Select-->");
        DrpRouteName0.Items.Insert(0, "<--Select-->");
    }
    #endregion

    #region Method for bindgrid
    protected void loadgrid()
    {
        sql = "Select vlm.Id,RouteName,PickupPointName, PickupPointId,PickupDistance,ArrivalTime,DepartureTime,DisplayOrder,vm.VehicleType,vd.VehicleNo";
        sql = sql + " from VehiclePickupLocationMaster vlm inner join VehicleRouteMaster vrm on vrm.Id=vlm.RouteId";
        sql = sql + " inner join VehicleMaster vm on vm.Id=vlm.VehicleTypeId";
        sql = sql + " inner join VehicleDetails vd on vd.VehicleType=vm.VehicleType and vd.Id=vlm.VehicleNoId";
        sql = sql + " where vlm.SessionName='" + Session["SessionName"].ToString() + "' and vrm.SessionName='" + Session["SessionName"].ToString() + "'";
        sql = sql + " and vm.SessionName='" + Session["SessionName"].ToString() + "' and vm.BranchCode=" + Session["BranchCode"].ToString() + " and vd.BranchCode=" + Session["BranchCode"].ToString() + " and vrm.BranchCode=" + Session["BranchCode"].ToString() + " and vd.SessionName='" + Session["SessionName"].ToString() + "'";
        if (DrpRouteName.SelectedIndex != 0)
        {
            sql = sql + " and RouteId='" + DrpRouteName.SelectedValue.ToString() + "'";
        }
        sql = sql + " order by DisplayOrder Asc";
        GridView1.DataSource = oo.GridFill(sql);
        GridView1.DataBind();
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            Label PickupPointId = (Label)GridView1.Rows[i].FindControl("PickupPointId");
            sql = "select LocationCode from LocationWiseVehicleAmount where LocationCode='" + PickupPointId.Text + "' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            if (oo.Duplicate(sql))
            {
                LinkButton LinkButton2 = (LinkButton)GridView1.Rows[i].FindControl("LinkButton2");
                LinkButton LinkButton3 = (LinkButton)GridView1.Rows[i].FindControl("LinkButton3");
                LinkButton2.Text = "<i class='fa fa-lock'></i>";
                LinkButton3.Text = "<i class='fa fa-lock'></i>";
                LinkButton2.Enabled = false;
                LinkButton3.Enabled = false;
            }
        }
    }
    #endregion

    #region Method for (Insert,Update,delete)
    protected void Insert()
    {
        for (int j = 0; j < GridView2.Rows.Count; j++)
        {
            TextBox txtPickupName = (TextBox)GridView2.Rows[j].FindControl("txtPickupName");
            if (txtPickupName.Text.Trim()=="")
            {
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please, Enter Pickup Stoppage Name!", "A");
                return;
            }
        }
        for (int i = 0; i < GridView2.Rows.Count; i++)
        {
            TextBox txtPickupName = (TextBox)GridView2.Rows[i].FindControl("txtPickupName");
            TextBox txtPickDistance = (TextBox)GridView2.Rows[i].FindControl("txtDistance");
            TextBox txtPickDisplayOrder = (TextBox)GridView2.Rows[i].FindControl("txtDisplayOrder");
            Label lblStopageCode = (Label)GridView2.Rows[i].FindControl("lblStopageCode");
          
            #region arrivalTime
            TextBox txtAHH = (TextBox)GridView2.Rows[i].FindControl("txtAHH");
            TextBox txtAMM = (TextBox)GridView2.Rows[i].FindControl("txtAMM");
            string arrivalTime = "00:00";
            if (txtAHH.Text.Trim() == "" && txtAMM.Text.Trim() == "")
            {
                arrivalTime = "00:00";
            }
            else if (txtAHH.Text.Trim() != "" && txtAMM.Text.Trim() == "")
            {
                arrivalTime = txtAHH.Text.Trim() + ":00";
            }
            else if (txtAHH.Text.Trim() == "" && txtAMM.Text.Trim() != "")
            {
                arrivalTime = "00:" + txtAMM.Text.Trim();
            }
            else
            {
                arrivalTime = txtAHH.Text.Trim() + ":" + txtAMM.Text.Trim();
            }
            #endregion
            #region departureTime
            TextBox txtDHH = (TextBox)GridView2.Rows[i].FindControl("txtDHH");
            TextBox txtDMM = (TextBox)GridView2.Rows[i].FindControl("txtDMM");
            string departureTime = "00:00";

            if (txtDHH.Text.Trim() == "" && txtDMM.Text.Trim() == "")
            {
                departureTime = "00:00";
            }
            else if (txtDHH.Text.Trim() != "" && txtDMM.Text.Trim() == "")
            {
                departureTime = txtDHH.Text.Trim() + ":00";
            }
            else if (txtDHH.Text.Trim() == "" && txtDMM.Text.Trim() != "")
            {
                departureTime = "00:" + txtDMM.Text.Trim();
            }
            else
            {
                departureTime = txtDHH.Text.Trim() + ":" + txtDMM.Text.Trim();
            }
            #endregion

            #region SP VehiclePickupLocationMaster_Proc (INSERT)
            cmd = new SqlCommand();
            cmd.CommandText = "VehiclePickupLocationMaster_Proc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@Type", "Insert");
            cmd.Parameters.AddWithValue("@Id", "");
            cmd.Parameters.AddWithValue("@RouteId", DrpRouteName.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@VehicleTypeId", DrpVehicleType.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@VehicleNoid", Drpvehicleno.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@PickupPointName", txtPickupName.Text.Trim());
            cmd.Parameters.AddWithValue("@PickupDistance", txtPickDistance.Text.Trim());
            cmd.Parameters.AddWithValue("@ArrivalTime", arrivalTime);
            cmd.Parameters.AddWithValue("@DepartureTime", departureTime);
            cmd.Parameters.AddWithValue("@DisplayOrder", txtPickDisplayOrder.Text.Trim());
            cmd.Parameters.AddWithValue("@PickupPointId", lblStopageCode.Text.Trim());
            cmd.Parameters.AddWithValue("@Remark", txtremark.Text.Trim());
            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            //oo.MessageBoxforUpdatePanel("Submitted successfully.", LinkButton1);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");

            #endregion
        }
        clear();
        loadgrid();
    }
    protected void Update()
    {
        #region arrivalTime
        string arrivalTime = "00:00";
        if (txtAHH0.Text.Trim() == "" && txtAMM0.Text.Trim() == "")
        {
            arrivalTime = "00:00";
        }
        else if (txtAHH0.Text.Trim() != "" && txtAMM0.Text.Trim() == "")
        {
            arrivalTime = txtAHH0.Text.Trim() + ":00";
        }
        else if (txtAHH0.Text.Trim() == "" && txtAMM0.Text.Trim() != "")
        {
            arrivalTime = "00:" + txtAMM0.Text.Trim();
        }
        else
        {
            arrivalTime = txtAHH0.Text.Trim() + ":" + txtAMM0.Text.Trim();
        }
        #endregion

        #region departureTime
        string departureTime = "00:00";

        if (txtDHH0.Text.Trim() == "" && txtDMM0.Text.Trim() == "")
        {
            departureTime = "00:00";
        }
        else if (txtDHH0.Text.Trim() != "" && txtDMM0.Text.Trim() == "")
        {
            departureTime = txtDHH0.Text.Trim() + ":00";
        }
        else if (txtDHH0.Text.Trim() == "" && txtDMM0.Text.Trim() != "")
        {
            departureTime = "00:" + txtDMM0.Text.Trim();
        }
        else
        {
            departureTime = txtDHH0.Text.Trim() + ":" + txtDMM0.Text.Trim();
        }
        #endregion

        #region StoreProcedure VehiclePickupLocationMaster_Proc (UPDATE)
        cmd = new SqlCommand();
        cmd.CommandText = "VehiclePickupLocationMaster_Proc";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
        cmd.Parameters.AddWithValue("@Type", "Update");
        cmd.Parameters.AddWithValue("@Id", lblID.Text.Trim());
        cmd.Parameters.AddWithValue("@RouteId", DrpRouteName0.SelectedValue.ToString());
        cmd.Parameters.AddWithValue("@VehicleTypeId", DrpVehicleType0.SelectedValue.ToString());
        cmd.Parameters.AddWithValue("@VehicleNoid", Drpvehicleno0.SelectedValue.ToString());
        cmd.Parameters.AddWithValue("@PickupPointName", txtPickupStopageName.Text.Trim());
        cmd.Parameters.AddWithValue("@PickupDistance", txtPickupDistance.Text.Trim());                
        cmd.Parameters.AddWithValue("@ArrivalTime", arrivalTime);
        cmd.Parameters.AddWithValue("@DepartureTime", departureTime);
        cmd.Parameters.AddWithValue("@DisplayOrder", txtDisplayOrder.Text.Trim());
        cmd.Parameters.AddWithValue("@PickupPointId", txtremark.Text.Trim());
        cmd.Parameters.AddWithValue("@Remark", txtremark.Text.Trim());
        cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
        cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
        cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        loadgrid();
        //oo.MessageBoxforUpdatePanel("Updated successfully.", LinkButton4);
        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully.", "S");

        #endregion
    }
    protected void Delete()
    {
        #region StoreProcedure VehiclePickupLocationMaster_Proc (DELETE)
        cmd = new SqlCommand();
        cmd.CommandText = "VehiclePickupLocationMaster_Proc";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
        cmd.Parameters.AddWithValue("@Type", "Delete");
        cmd.Parameters.AddWithValue("@Id", lblvalue.Text.Trim());
        cmd.Parameters.AddWithValue("@RouteId", "");
        cmd.Parameters.AddWithValue("@VehicleTypeId", "");
        cmd.Parameters.AddWithValue("@VehicleNoid", "");
        cmd.Parameters.AddWithValue("@PickupPointName", "");
        cmd.Parameters.AddWithValue("@PickupDistance", "");
        cmd.Parameters.AddWithValue("@ArrivalTime", "");
        cmd.Parameters.AddWithValue("@DepartureTime", "");
        cmd.Parameters.AddWithValue("@DisplayOrder", "");
        cmd.Parameters.AddWithValue("@PickupPointId", "");
        cmd.Parameters.AddWithValue("@Remark", "");
        cmd.Parameters.AddWithValue("@LoginName", "");
        cmd.Parameters.AddWithValue("@BranchCode", "");
        cmd.Parameters.AddWithValue("@SessionName", "");
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        txtPickupStopage.Text = "";
        GridView2.DataSource = null;
        GridView2.DataBind();
        loadgrid();
        //oo.MessageBoxforUpdatePanel("Deleted successfully.", btnDelete);
        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Deleted successfully.", "S");

        #endregion
    }
    #endregion

    #region Method for checkDuplicay and clear control
    public bool checkDuplicateLocationName()
    {
        bool flag = false;
        for (int i = 0; i < GridView2.Rows.Count; i++)
        {
            TextBox txtPickupName = (TextBox)GridView2.Rows[i].FindControl("txtPickupName");
            sql = "Select *from VehiclePickupLocationMaster where ";
            sql = sql + " RouteId='" + DrpRouteName.SelectedValue.ToString() + "'";
            sql = sql + " and VehicleTypeId='" + DrpVehicleType.SelectedValue.ToString() + "'";
            sql = sql + " and VehicleNoid='" + Drpvehicleno.SelectedValue.ToString() + "' ";
            sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "'";
            sql = sql + " and PickupPointName='" + txtPickupName.Text.Trim() + "'";
            if (oo.Duplicate(sql))
            {
                txtPickupName.Focus();
                txtPickupName.BorderColor = System.Drawing.Color.Maroon;
                flag = true;
            }
        }
        return flag;
    }
    protected void clear()
    {
        txtPickupStopage.Text = "";
        GridView2.DataSource = null;
        GridView2.DataBind();
    }
    #endregion

    #region All LinkButton Click Event
    protected void LinkButton1_Click(object sender, EventArgs e)
    {

        if (DrpVehicleType.SelectedIndex != 0 && Drpvehicleno.SelectedIndex != 0)
        {
            if (!checkDuplicateLocationName())
            {
                try
                {
                    Insert();
                }
                catch (Exception ex)
                {
                    oo.MessageBoxforUpdatePanel(ex.Message, LinkButton1);
                }
            }
            else
            {
                //oo.MessageBoxforUpdatePanel("Duplicate Entry!", LinkButton1);
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Duplicate Entry!", "A");

            }
        }
        else
        {
            //oo.MessageBoxforUpdatePanel("Please Select VehicleType or VehicleNo.!", LinkButton1);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please Select VehicleType or VehicleNo.!", "A");

        }

    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("Label36");
        lblID.Text = lblId.Text;

        sql = "Select RouteId,PickupPointName,PickupDistance,Datepart(HH,ArrivalTime) as AHH,Datepart(MI,ArrivalTime) as AMM,DisplayOrder, ";
        sql = sql + " Datepart(HH,DepartureTime) as DHH,Datepart(MI,DepartureTime) as DMM";
        sql = sql + " from VehiclePickupLocationMaster where Id='" + lblID.Text + "' and BranchCode=" + Session["BranchCode"].ToString() + " and SessionName='" + Session["SessionName"].ToString() + "'";
        try
        {
            DrpRouteName0.SelectedValue = oo.ReturnTag(sql, "RouteId");
        }
        catch
        {
            DrpRouteName0.SelectedIndex = 0;
        }
        txtPickupStopageName.Text = oo.ReturnTag(sql, "PickupPointName");
        txtPickupDistance.Text = oo.ReturnTag(sql, "PickupDistance");

        #region Arrival Time
        if (oo.ReturnTag(sql, "AHH").Length == 1)
        {
            txtAHH0.Text = "0" + oo.ReturnTag(sql, "AHH");
        }
        else
        {
            txtAHH0.Text = oo.ReturnTag(sql, "AHH");
        }
        if (oo.ReturnTag(sql, "AMM").Length == 1)
        {
            txtAMM0.Text = "0" + oo.ReturnTag(sql, "AMM");
        }
        else
        {
            txtAMM0.Text = oo.ReturnTag(sql, "AMM");
        }
        #endregion

        #region Departure Time
        if (oo.ReturnTag(sql, "DHH").Length == 1)
        {
            txtDHH0.Text = "0" + oo.ReturnTag(sql, "DHH");
        }
        else
        {
            txtDHH0.Text = oo.ReturnTag(sql, "DHH");
        }
        if (oo.ReturnTag(sql, "DMM").Length == 1)
        {
            txtDMM0.Text = "0" + oo.ReturnTag(sql, "DMM");
        }
        else
        {
            txtDMM0.Text = oo.ReturnTag(sql, "DMM");
        }
        #endregion

        txtDisplayOrder.Text = oo.ReturnTag(sql, "DisplayOrder");
        loadVehicle0();
        sql = "Select VehicleTypeId from VehiclePickupLocationMaster where Id='" + lblID.Text + "' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        DrpVehicleType0.SelectedValue = oo.ReturnTag(sql, "VehicleTypeId");
        loadVehicleNo0();
        sql = "Select VehicleNoid from VehiclePickupLocationMaster where Id='" + lblID.Text + "' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        Drpvehicleno0.SelectedValue = oo.ReturnTag(sql, "VehicleNoid");

        Panel1_ModalPopupExtender.Show();

    }
    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        sql = "Select *from VehiclePickupLocationMaster where ";
        sql = sql + " Id<>'" + lblID.Text.Trim() + "' and RouteId='" + DrpRouteName.SelectedValue.ToString() + "'";
        sql = sql + " and VehicleTypeId='" + DrpVehicleType.SelectedValue.ToString() + "'";
        sql = sql + " and VehicleNoid='" + Drpvehicleno.SelectedValue.ToString() + "' ";
        sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "'";
        sql = sql + " and PickupPointName='" + txtPickupStopageName.Text.Trim() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        if (oo.Duplicate(sql) == false)
        {
            if (DrpVehicleType0.SelectedIndex != 0 && Drpvehicleno0.SelectedIndex != 0 && txtPickupStopageName.Text != "")
            {
                Update();
            }
            else
            {
                //oo.MessageBoxforUpdatePanel("Please Select VehicleType or VehicleNo.!", LinkButton1);
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please Select VehicleType, VehicleNo & Pickup Stoppage Name.!", "A");

            }
        }
        else
        {
            //oo.MessageBoxforUpdatePanel("Source and Destination are already exist!", LinkButton1);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Source and Destination are already exist!", "A");

        }
    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("Label37");
        lblvalue.Text = lblId.Text;
        Button8.Focus();
        Panel2_ModalPopupExtender.Show();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        Delete();
    }
    
    #endregion     

    #region All Dropdown Changed event
    protected void DrpRouteName_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadgrid();
        bindStopageGrid();
        createId();
    }
    protected void DrpVehicleType_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadVehicleNo();
        bindStopageGrid();
        createId();
    }
    protected void DrpRouteName0_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadVehicle0();
        Panel1_ModalPopupExtender.Show();
    }
    protected void DrpVehicleType0_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadVehicleNo0();
        Panel1_ModalPopupExtender.Show();
    }
    protected void Drpvehicleno_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindStopageGrid();
        createId();
    }
    #endregion
  
    #region Method for setPickupStopage and bindpickup grid and All txtChanged Event
    protected void txtPickupName_TextChanged(object sender, EventArgs e)
    {
        var txt = (TextBox)sender;
        GridViewRow currentrow = (GridViewRow)txt.NamingContainer;
        int count1 = 0;
        for (int i = 0; i < GridView2.Rows.Count; i++)
        {
            TextBox findtxtvalue = (TextBox)GridView2.Rows[i].FindControl("txtPickupName");
            if (findtxtvalue.Text.ToUpper() == txt.Text.ToUpper())
            {
                count1 = count1 + 1;
                if (count1 > 1)
                {
                    txt.Text = "";
                    txt.Focus();
                    break;
                }
            }

        }
        if (count1 <= 1)
        {
            TextBox focustext = (TextBox)GridView2.Rows[currentrow.RowIndex].FindControl("txtAHH");
            focustext.Focus();
        }
    }
    protected void txtPickupStopage_TextChanged(object sender, EventArgs e)
    {
        bindStopageGrid();
    }
    public void bindStopageGrid()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("sr");
        dt.Columns.Add("srno");
        bool flag;
        int stopage;
        flag = int.TryParse(txtPickupStopage.Text.Trim(), out stopage);
        if (flag)
        {
            sql = "Select Max(DisplayOrder) as DisplayOrder from VehiclePickupLocationMaster where ";
            sql = sql + " RouteId='" + DrpRouteName.SelectedValue.ToString() + "'";
            sql = sql + " and VehicleTypeId='" + DrpVehicleType.SelectedValue.ToString() + "'";
            sql = sql + " and VehicleNoid='" + Drpvehicleno.SelectedValue.ToString() + "' ";
            sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            if (!string.IsNullOrEmpty(oo.ReturnTag(sql, "DisplayOrder")))
            {
                int j = Convert.ToInt16(oo.ReturnTag(sql, "DisplayOrder"));
                for (int i = j + 1; i <= stopage + j; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["sr"] = (i - j).ToString();
                    dr["srno"] = i.ToString();
                    dt.Rows.Add(dr);
                }
            }
            else
            {
                for (int i = 1; i <= stopage; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["sr"] = i.ToString();
                    dr["srno"] = i.ToString();
                    dt.Rows.Add(dr);
                }
            }
        }
        GridView2.DataSource = dt;
        GridView2.DataBind();
        createId();
    }
    public void createId()
    {
        if (GridView2.Rows.Count > 0)
        {
            for (int i = 0; i < GridView2.Rows.Count; i++)
            {
                TextBox txtDisplayOrder = (TextBox)GridView2.Rows[i].FindControl("txtDisplayOrder");
                Label lblStopageCode = (Label)GridView2.Rows[i].FindControl("lblStopageCode");
                sql = "Select DatePart(DD,Getdate()) as DD,DatePart(month,Getdate()) as MM,DatePart(year,Getdate()) as Year,Replace(Convert(nvarchar(50),Convert(time(0),Getdate())),':','') as Time";
                lblStopageCode.Text = "P" + DrpRouteName.SelectedValue + DrpVehicleType.SelectedValue + Drpvehicleno.SelectedValue + txtDisplayOrder.Text + oo.ReturnTag(sql, "DD") + oo.ReturnTag(sql, "MM") + oo.ReturnTag(sql, "Year") + oo.ReturnTag(sql, "Time");
            }
        }
    }   
    #endregion
       
    #region WebMethod
    [WebMethod] 
    public static void updateGridViewReorder(int DisplayOrder, int id)
    {
        Campus oo1=new Campus();
        string sql = "";
        SqlConnection con = new SqlConnection();
        con=oo1.dbGet_connection();
        //variable Validation for sql injection
        bool flag1, flag2;
        flag1 = int.TryParse(DisplayOrder.ToString(), out DisplayOrder);
        flag2 = int.TryParse(id.ToString(), out id);
        if (flag1 && flag2)
        {
            sql = "Update VehiclePickupLocationMaster set DisplayOrder='" + DisplayOrder + "' where id='" + id + "' and BranchCode=" + BranchCode + "";
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }  
    //public Control GetPostBackControl(Page page, Control ctrl)
    //{
    //    Control control = null;

    //    string ctrlname = page.Request.Params.Get("__EVENTTARGET");
    //    if (ctrlname != null && ctrlname != string.Empty)
    //    {
    //        control = page.FindControl(ctrlname);
    //    }
    //    else
    //    {
    //        foreach (string ctl in page.Request.Form)
    //        {
    //            Control c = page.FindControl(ctl);
    //            if (c is System.Web.UI.WebControls.Button)
    //            {
    //                control = c;
    //                break;
    //            }
    //        }
    //    }
    //    return control;
    //}
    #endregion


}