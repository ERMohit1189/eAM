using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI.WebControls;

namespace _1
{
    public partial class AdminMasterUpdate : System.Web.UI.Page
    {
        private SqlConnection _con;
        private readonly Campus _oo;
        private string _sql = String.Empty;
        private static string _sess = String.Empty;

        public AdminMasterUpdate()
        {
            _con = new SqlConnection();
            _oo = new Campus();
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            _con = _oo.dbGet_connection();
            if ((string)Session["LoginName"] == "" || (string)Session["BranchCode"] == "" || (string)Session["SessionName"] == "" || Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
            {
                Response.Redirect("default.aspx");
            }
            Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file

            if (!IsPostBack)
            {
                _con = _oo.dbGet_connection();
           
                // ReSharper disable once RedundantAssignment
                int yy1 , yy2 = 0; 
                // ReSharper disable once PossibleNullReferenceException
                yy1 = Convert.ToInt32(Session["SessionName"].ToString().Substring(0, 4));
                yy1 = yy1 + 1;
                yy2 = Convert.ToInt32(Session["SessionName"].ToString().Substring(5, 4));
                yy2 = yy2 + 1;
                _sess = yy1 + "-" + yy2;
                CheckTableDataOnPageLoad();
            
            }

        }

        public void CheckTableDataOnPageLoad()
        {
            Tuple<CheckBox, string> t1 = new Tuple<CheckBox, string>(Chk1, "ClassMaster");
            Tuple<CheckBox, string> t2 = new Tuple<CheckBox, string>(Chk2, "SectionMaster");
            Tuple<CheckBox, string> t3 = new Tuple<CheckBox, string>(Chk3, "FeeGroupMaster");
            Tuple<CheckBox, string> t4 = new Tuple<CheckBox, string>(Chk4, "MonthMaster");
            Tuple<CheckBox, string> t5 = new Tuple<CheckBox, string>(Chk5, "FeeMaster");
            Tuple<CheckBox, string> t6 = new Tuple<CheckBox, string>(Chk6, "FeeAllotedForClassWise");
            Tuple<CheckBox, string> t7 = new Tuple<CheckBox, string>(Chk7, "HouseMaster");
            Tuple<CheckBox, string> t8 = new Tuple<CheckBox, string>(Chk8, "dt_CreateDocumentName");
            Tuple<CheckBox, string> t9 = new Tuple<CheckBox, string>(Chk9, "dt_CreateStaffDocumentName");
            Tuple<CheckBox, string> t10 = new Tuple<CheckBox, string>(Chk10, "VehicleMaster");
            Tuple<CheckBox, string> t11 = new Tuple<CheckBox, string>(Chk11, "VehicleDetails");
            Tuple<CheckBox, string> t12 = new Tuple<CheckBox, string>(Chk12, "VehicleRouteMaster");
            Tuple<CheckBox, string> t13 = new Tuple<CheckBox, string>(Chk13, "VehiclePickupLocationMaster");
            Tuple<CheckBox, string> t14 = new Tuple<CheckBox, string>(Chk14, "VehicleDropLocationMaster");
            Tuple<CheckBox, string> t15 = new Tuple<CheckBox, string>(Chk15, "LocationWiseVehicleAmount");
            Tuple<CheckBox, string> t16 = new Tuple<CheckBox, string>(Chk16, "GroupMaster");
            Tuple<CheckBox, string> t17 = new Tuple<CheckBox, string>(Chk17, "RangeBasisFineMaster");

            List<Tuple<CheckBox, string>> chklist = new List<Tuple<CheckBox, string>>();
            chklist.Add(t1); chklist.Add(t2); chklist.Add(t3); chklist.Add(t4); chklist.Add(t5); chklist.Add(t6);
            chklist.Add(t7); chklist.Add(t8); chklist.Add(t9);
            chklist.Add(t10); chklist.Add(t11); chklist.Add(t12); chklist.Add(t13);
            chklist.Add(t14); chklist.Add(t15); chklist.Add(t16); chklist.Add(t17);
            foreach (Tuple<CheckBox, string> t in chklist)
            {
                _sql = "select * from " + t.Item2 + " where SessionName='" + _sess + "'";
                if (ColourValidation(t.Item1, _oo.Duplicate(_sql)))
                {
                    t.Item1.Checked = false;
                }
                else
                {
                    t.Item1.Checked = true;
                }
            }
            CheckRedindication();
        }

        public bool ColourValidation(CheckBox chk, bool flag)
        {
            chk.BorderStyle = BorderStyle.Solid;
            chk.BorderWidth = 1;
            if (flag)
            {
                chk.BackColor = chk.BorderColor = Color.Red;
                chk.Checked = false;
            }
            else
            {
                chk.BackColor = chk.BorderColor = Color.Green;
                chk.Checked = true;
            }

            return flag;
        }

        public Boolean Duplicate(string qry)
        {
            int co = 0;
            Boolean flag = false;
            _con.Close();
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            try
            {
                cmd.CommandText = qry;
                cmd.Connection = _con;
                _con.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    int ro = dr.FieldCount;
                    if (ro > 1)
                    {
                        if (dr[1] != DBNull.Value)
                        {
                            co++;
                        }
                    }
                    else
                    {
                        co++;
                    }
                }
                _con.Close();
            }
            catch (SqlException)
            {
                _con.Close();
            }

            if (co >= 1)
            {
                flag = true;
            }
            return flag;
        }

        public void CheckRedindication()
        {
            if (Chk1.BackColor == Color.Red)
            {
                ChkAll1.Enabled = false;
                ChkAll1.Checked = false;
            }
            if (Chk7.BackColor == Color.Red)
            {
                ChkAll2.Enabled = false;
                ChkAll2.Checked = false;
            }
            if (Chk9.BackColor == Color.Red)
            {
                ChkAll3.Enabled = false;
                ChkAll3.Checked = false;
            }
            if (Chk3.BackColor == Color.Red)
            {
                ChkAll4.Enabled = false;
                ChkAll4.Checked = false;
            }
            if (Chk10.BackColor == Color.Red)
            {
                ChkAll5.Enabled = false;
                ChkAll5.Checked = false;
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            _sql = "select * from SessionMaster where sessionName='" + _sess + "'and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            if (_oo.Duplicate(_sql))
            {
                _sql = "select * from ClassMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                if (_oo.Duplicate(_sql)!=true)
                {
                    //oo.MessageBoxforUpdatePanel("Sorry, Master(s) already updated!", this.Page);
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Sorry, Master(s) already updated!", "A");

                }
                else
                {
                    try
                    {
                        ClassMaster();
                        SectionMaster();
                        FeeGroupMaster();
                        MonthMaster();
                        FeeMaster();
                        FeeAllotedForClassWise();
                        HouseMaster();
                        StudentDocumentTypeMaster();
                        StaffDocumentTypeMaster();
                        VehicleMaster();
                        VehicleDetailsMaster();
                        VehicleRouteMaster();
                        VehiclePickupLocationMaster();
                        VehicleDropLocationMaster();
                        LocationWiseVehicleAmountMaster();
                        Group_update();
                        RangeBasisFineMaster();
                        //DailyBasisFineMaster();
                        CheckTableDataOnPageLoad();
                        //oo.MessageBox("Master(s) updated successfully.", this.Page);
                        Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Master(s) updated successfully..", "S");

                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                }
            }
            else
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Please Create a new Session!", "A");

                //oo.MessageBox("Please Create a new Session!", this.Page);               
            }
       
        }

        #region All Master Update Methodes

        public void ClassMaster()
        {
            if (Chk1.Checked)
            {
                _sql = "Select *from ClassMaster where SessionName='" + _sess + "' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
           
                string qry , pp , sql1 , ClassNa;
                _sql = "select max(Id)+1 as ID from ClassMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                // ReSharper disable once UnusedVariable
                int id  = Convert.ToInt32(_oo.ReturnTag(_sql, "ID"));

                _sql = "select ID,ClassName from ClassMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                _con.Open();
                SqlDataAdapter da = new SqlDataAdapter(_sql, _con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                try
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        pp = dt.Rows[i]["ID"].ToString();
                        ClassNa = dt.Rows[i]["ClassName"].ToString();
                        //id = id + 1;
                        sql1 = "select * from ClassMaster where ClassName='" + ClassNa + "' and SessionName='" + _sess + "' and BranchCode=" + Session["BranchCode"] + "";
                        if (Duplicate(sql1) == false)
                        {
                            qry = " insert into ClassMaster(Id,ClassName,RoomNo,Location,Remark,BranchCode,LoginName,SessionName,ClassCode,RecordDate,CIDOrder)";
                            qry = qry + " select id,ClassName,RoomNo,Location,Remark,BranchCode,LoginName,'" + _sess + "',ClassCode,getdate(),CIDOrder from ClassMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and Id=" + pp;
                            _oo.ProcedureDatabase(qry);
                        }
                    }
                    _con.Close();
                }
                catch (SqlException)
                {
                    _con.Close();
                }
            }
          
        }

        public void SectionMaster()
        {
            if (Chk2.Checked)
            {
                string qry , pp, sql1, classId , sectionName ;
                _sql = "select max(Id)+1 as ID from SectionMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                int id  = Convert.ToInt32(_oo.ReturnTag(_sql, "ID"));


                _sql = "select ID,SectionName,ClassNameId from SectionMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                _con.Open();
                SqlDataAdapter da = new SqlDataAdapter(_sql, _con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                try
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        pp = dt.Rows[i]["ID"].ToString();
                        sectionName = dt.Rows[i]["SectionName"].ToString();
                        classId = dt.Rows[i]["ClassNameId"].ToString();

                        sql1 = "select * from SectionMaster where  SessionName='" + _sess + "' and SectionName='" + sectionName + "' and BranchCode=" + Session["BranchCode"] + " and ClassNameId=" + classId;
                        if (Duplicate(sql1) == false)
                        {

                            qry = " insert into SectionMaster(Id,ClassNameId,SectionName,RoomNo,Location,Remark,BranchCode,LoginName,SessionName,SectionCode,RecordDate)";
                            qry = qry + " select " + id + ",ClassNameId,SectionName,RoomNo,Location,Remark,BranchCode,LoginName,'" + _sess + "',SectionCode,getdate() from SectionMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and Id=" + pp;
                            id = id + 1;
                            _oo.ProcedureDatabase(qry);
                        }
                    }
                    _con.Close();
                }
                catch (SqlException)
                {
                    _con.Close();
                }
            }

        }

        public void FeeGroupMaster()
        {
            if (Chk3.Checked)
            {
                string qry, pp , sql1 , feeGroupNa;
                _sql = "select max(Id)+1 as ID from FeeGroupMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                int id  = Convert.ToInt32(_oo.ReturnTag(_sql, "ID"));


                _sql = "select ID,FeeGroupName from FeeGroupMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                _con.Open();
                SqlDataAdapter da = new SqlDataAdapter(_sql, _con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                try
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        pp = dt.Rows[i]["ID"].ToString();
                        feeGroupNa = dt.Rows[i]["FeeGroupName"].ToString();

                        sql1 = "select * from FeeGroupMaster where FeeGroupName='" + feeGroupNa + "' and SessionName='" + _sess + "' and BranchCode=" + Session["BranchCode"] + "";
                        if (Duplicate(sql1) == false)
                        {

                            qry = " insert into FeeGroupMaster(Id,FeeGroupName,Remark,SessionName ,BranchCode ,LoginName ,RecordDate)";
                            qry = qry + " select " + id + ",FeeGroupName,Remark,'" + _sess + "' ,BranchCode ,LoginName ,getdate() from FeeGroupMaster where SessionName='" + Session["SessionName"] + "' and Id=" + pp;
                            id = id + 1;
                            _oo.ProcedureDatabase(qry);
                        }
                    }
                    _con.Close();
                }
                catch (SqlException)
                {
                    _con.Close();
                }
            }
        }

        public void MonthMaster()
        {
            if (Chk4.Checked)
            {
                string qry , pp , sql1 , monthName, cardtype ;
                _sql = "select max(MonthId)+1 as ID from MonthMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                int id = Convert.ToInt32(_oo.ReturnTag(_sql, "ID"));


                _sql = "select MonthId,MonthName,CardType from MonthMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                _con.Open();
                SqlDataAdapter da = new SqlDataAdapter(_sql, _con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                try
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        pp = dt.Rows[i]["MonthId"].ToString();
                        monthName = dt.Rows[i]["MonthName"].ToString();
                        cardtype = dt.Rows[i]["CardType"].ToString();
                        sql1 = "select * from MonthMaster where MonthName='" + monthName + "' and SessionName='" + _sess + "' and BranchCode=" + Session["BranchCode"] + " and (CardType='" + cardtype + "' or CardType is null)";
                        if (Duplicate(sql1) == false)
                        {

                            qry = " insert into MonthMaster(MonthId,MonthName,MonthRemark ,SessionName ,BranchCode,LoginName,RecordDate,CardType,DueMonth)";
                            qry = qry + " select " + id + ",MonthName,MonthRemark ,'" + _sess + "' ,BranchCode,LoginName,getdate(),CardType,DueMonth from MonthMaster where SessionName='" + Session["SessionName"] + "' and MonthId=" + pp;
                            id = id + 1;
                            _oo.ProcedureDatabase(qry);
                        }
                    }
                    _con.Close();
                }
                catch (SqlException)
                {
                    _con.Close();
                }
            }
        }

        public void FeeMaster()
        {
            if (Chk5.Checked)
            {
                string qry, pp, sql1 , feeName , admissionType, medium;
                _sql = "select max(FeeId)+1 as ID from FeeMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                int id = Convert.ToInt32(_oo.ReturnTag(_sql, "ID"));
                _sql = "select FeeId,FeeName,Medium,AdmissionType from FeeMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                _con.Open();
                SqlDataAdapter da = new SqlDataAdapter(_sql, _con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                try
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        pp = dt.Rows[i]["FeeId"].ToString();
                        feeName = dt.Rows[i]["FeeName"].ToString();
                        medium = dt.Rows[i]["Medium"].ToString();
                        admissionType = dt.Rows[i]["AdmissionType"].ToString();
                        sql1 = "select * from FeeMaster where FeeName='" + feeName + "' and SessionName='" + _sess + "' and AdmissionType='" + admissionType + "' and Medium='" + medium + "' and BranchCode=" + Session["BranchCode"] + "";
                        if (Duplicate(sql1) == false)
                        {

                            qry = " insert into FeeMaster(FeeId,FeeName,FeeCategory ,Remark ,SessionName ,BranchCode,LoginName,RecordDate,Medium,AdmissionType,NoOfMonths)";
                            qry = qry + " select " + id + ",FeeName,FeeCategory ,Remark ,'" + _sess + "' ,BranchCode,LoginName,getdate(),Medium,AdmissionType,NoOfMonths from FeeMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and FeeId=" + pp;
                            id = id + 1;
                            _oo.ProcedureDatabase(qry);
                        }
                    }
                    _con.Close();
                }
                catch (SqlException)
                {
                    _con.Close();
                }
            }
        }

        public void FeeAllotedForClassWise()
        {
            if (Chk6.Checked)
            {
                // ReSharper disable once NotAccessedVariable
                string qry , pp , sql1 , card , admissionType , medium , feeType , month , feeName , classNa ;
                _sql = "select max(Id)+1 as ID from FeeAllotedForClassWise where SessionName='" + Session["SessionName"] + "'";
                int id  = Convert.ToInt32(_oo.ReturnTag(_sql, "ID"));


                _sql = "select Id,CardType,AdmissionType,Medium,FeeType,Month,FeeName,Class from FeeAllotedForClassWise where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                _con.Open();
                SqlDataAdapter da = new SqlDataAdapter(_sql, _con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                try
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        pp = dt.Rows[i]["Id"].ToString();
                        card = dt.Rows[i]["CardType"].ToString();
                        admissionType = dt.Rows[i]["AdmissionType"].ToString();
                        medium = dt.Rows[i]["Medium"].ToString();
                        // ReSharper disable once RedundantAssignment
                        feeType = dt.Rows[i]["FeeType"].ToString();
                        month = dt.Rows[i]["Month"].ToString();
                        feeName = dt.Rows[i]["FeeName"].ToString();
                        classNa = dt.Rows[i]["Class"].ToString();
                        sql1 = "select * from FeeAllotedForClassWise where FeeName='" + feeName + "' and SessionName='" + _sess + "' and AdmissionType='" + admissionType + "' and Medium='" + medium + "'  and Month='" + month + "' and CardType='" + card + "' and Class='" + classNa + "'";
                        if (Duplicate(sql1) == false)
                        {

                            qry = " insert into FeeAllotedForClassWise(Id,Month,FeeParticular,Class,FeeType,FeePayment,Remark,   BranchCode ,LoginName,SessionName,RecordDate, cardtype,AdmissionType,FeeName,Medium)";
                            qry = qry + " select " + id + ",Month,FeeParticular,Class,FeeType,FeePayment,Remark,   BranchCode ,LoginName,'" + _sess + "',getdate(), cardtype,AdmissionType,FeeName,Medium from FeeAllotedForClassWise where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and Id=" + pp;
                            id = id + 1;
                            _oo.ProcedureDatabase(qry);
                        }
                    }
                    _con.Close();
                }
                catch (SqlException)
                {
                    _con.Close();
                }
            }
        }

        public void HouseMaster()
        {
            if (Chk7.Checked)
            {
                string qry, pp , sql1, classNa;
                _sql = "select max(Id) as ID from HouseMaster";
                int id = Convert.ToInt32(_oo.ReturnTag(_sql, "ID"));
                _sql = "select ID,HouseName from HouseMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " ";
                _con.Open();
                SqlDataAdapter da = new SqlDataAdapter(_sql, _con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                try
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        pp = dt.Rows[i]["ID"].ToString();
                        classNa = dt.Rows[i]["HouseName"].ToString();
                        //id = id + 1;
                        sql1 = "select * from HouseMaster where HouseName='" + classNa + "' and SessionName='" + _sess + "'";
                        if (Duplicate(sql1) == false)
                        {

                            qry = " insert into HouseMaster(Id,HouseName,Color,Remark,SessionName,BranchCode,LoginName,RecordDate)";
                            qry = qry + " select '" + id + "',HouseName,Color,Remark,'" + _sess + "',BranchCode,LoginName,GetDate() from HouseMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and Id=" + pp;
                            id = id + 1;
                            _oo.ProcedureDatabase(qry);
                        }
                    }
                    _con.Close();
                }
                catch (SqlException)
                {
                    _con.Close();
                }
            }
        }

        public void StudentDocumentTypeMaster()
        {
            if (Chk8.Checked)
            {
                #region Update Student DocumentType Name
                _sql = "Insert into dt_CreateDocumentName([Id],[DocumentType],[ClassId],[SessionName],[BranchCode],[LoginName],[RecordDate])";
                _sql = _sql + " Select [Id],[DocumentType],[ClassId],'" + _sess + "',[BranchCode],'" + Session["LoginName"] + "',GETDATE() from dt_CreateDocumentName where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                _oo.ProcedureDatabase(_sql);
                #endregion

            }

        }

        public void StaffDocumentTypeMaster()
        {
            if (Chk9.Checked)
            {
                #region Update Staff DocumentType Name
                _sql = "Insert into dt_CreateStaffDocumentName([Id],[DocumentType],[SessionName],[BranchCode],[LoginName],[RecordDate])";
                _sql = _sql + " Select [Id],[DocumentType],'" + _sess + "',[BranchCode],'" + Session["LoginName"] + "',GETDATE() from dt_CreateStaffDocumentName where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                _oo.ProcedureDatabase(_sql);
                #endregion
            }

        }

        public void VehicleMaster()
        {
            if (Chk10.Checked)
            {
                #region Update VehicleMaster
                _sql = "Insert into VehicleMaster([Id],[VehicleType],[Remark],[SesionName],[BranchCode],[SessionName],[LoginName],[RecordDate] )";
                _sql = _sql + " Select [Id],[VehicleType],[Remark],[SesionName],[BranchCode],'" + _sess + "','" + Session["LoginName"] + "',GETDATE() from VehicleMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                _oo.ProcedureDatabase(_sql);
                #endregion

            }
        }

        public void VehicleDetailsMaster()
        {
            if (Chk11.Checked)
            {
                #region Update VehicleDetails
                _sql = "Insert into VehicleDetails([Id],[VehicleType],[VehicleNo],[VehicleModel],[BodyType],[SeatCapacity],[RegistrationNo],[VehicleMade],[FuelType],[EngineNo],[ChasisNo],";
                _sql = _sql + " [VehicleCC],[MFG],[VehicleRemark],[OwnerName],[OwnerContactNo],[PermitNo],[ValidUpto],[OwnerAddress],[OwnerRemark],[SessionName],[BranchCode],[LoginName],[RecordDate],[Driver])";
                _sql = _sql + " Select [Id],[VehicleType],[VehicleNo],[VehicleModel],[BodyType],[SeatCapacity],[RegistrationNo],[VehicleMade],[FuelType],[EngineNo],[ChasisNo],";
                _sql = _sql + " [VehicleCC],[MFG],[VehicleRemark],[OwnerName],[OwnerContactNo],[PermitNo],[ValidUpto],[OwnerAddress],[OwnerRemark],'" + _sess + "',[BranchCode],'" + Session["LoginName"] + "',GETDATE(),[Driver] from VehicleDetails where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                _oo.ProcedureDatabase(_sql);
                #endregion 
            }
        }

        public void VehicleRouteMaster()
        {
            if (Chk12.Checked)
            {
                #region Update VehicleRouteMaster
                _sql = "Insert into VehicleRouteMaster(Id,RouteName,Remark,SessionName,BranchCode,LoginName,RecordDate)";
                _sql = _sql + " Select Id,RouteName,Remark,'" + _sess + "',BranchCode,'" + Session["LoginName"] + "',GETDATE() from VehicleRouteMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                _oo.ProcedureDatabase(_sql);
                #endregion 
            }
        }

        public void VehiclePickupLocationMaster()
        {
            if (Chk13.Checked)
            {
                #region Update VehiclePickupLocationMaster
                _sql = "Insert into VehiclePickupLocationMaster([Id],[RouteId],[VehicleTypeId],[VehicleNoid],[PickupPointName],[PickupDistance],[ArrivalTime],";
                _sql = _sql + " [DisplayOrder],[PickupPointId],[Remark],[SessionName],[LoginName],[BranchCode],[RecordDate],[DepartureTime])";
                _sql = _sql + " Select [Id],[RouteId],[VehicleTypeId],[VehicleNoid],[PickupPointName],[PickupDistance],[ArrivalTime],[DisplayOrder],[PickupPointId],";
                _sql = _sql + " [Remark],'" + _sess + "','" + Session["LoginName"] + "',[BranchCode],Getdate(),[DepartureTime] from VehiclePickupLocationMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                _oo.ProcedureDatabase(_sql);
                #endregion 
            }
        }

        public void VehicleDropLocationMaster()
        {
            if (Chk14.Checked)
            { 
                #region Update VehicleDropLocationMaster
                _sql = "Insert into VehicleDropLocationMaster([Id],[RouteId],[VehicleTypeId],[VehicleNoid],[DropPointName],[DropDistance],[ArrivalTime],";
                _sql = _sql + " [DisplayOrder],[DropPointId],[Remark],[SessionName],[LoginName],[BranchCode],[RecordDate],[DepartureTime])";
                _sql = _sql + " Select [Id],[RouteId],[VehicleTypeId],[VehicleNoid],[DropPointName],[DropDistance],[ArrivalTime],[DisplayOrder],[DropPointId],";
                _sql = _sql + " [Remark],'" + _sess + "','" + Session["LoginName"] + "',[BranchCode],Getdate(),[DepartureTime] from VehicleDropLocationMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                _oo.ProcedureDatabase(_sql);
                #endregion
            }
        }

        public void LocationWiseVehicleAmountMaster()
        {
            if (Chk15.Checked)
            { 
                #region Update LocationWiseVehicleAmount
                _sql = "Insert into LocationWiseVehicleAmount([Id] ,[RouteId],[VehicleId] ,[VehicleTypeId] ,[LocationCode],[Insttalment] ,";
                _sql = _sql + " [Amount] ,[SessionName] ,[BranchCode] ,[LoginName] ,[RecordDate] )";
                _sql = _sql + " Select [Id] ,[RouteId],[VehicleId] ,[VehicleTypeId] ,[LocationCode],[Insttalment] ,";
                _sql = _sql + " [Amount] ,'" + _sess + "' ,[BranchCode] ,'" + Session["LoginName"] + "' ,Getdate() from LocationWiseVehicleAmount where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                _oo.ProcedureDatabase(_sql);
                #endregion
            }
        }
   
        public void Group_update()
        {
            if (Chk16.Checked)
            {
                string qry , pp, sql1 ;
                _sql = "select max(Id) as ID from GroupMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                int id;
                id = Convert.ToInt32(_oo.ReturnTag(_sql, "ID"));


                _sql = "select Id,GroupName from GroupMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                _con.Open();
                SqlDataAdapter da = new SqlDataAdapter(_sql, _con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                try
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        //id = id + 1;
                        pp = dt.Rows[i]["Id"].ToString();
                        var groupName = dt.Rows[i]["GroupName"].ToString();
                        sql1 = "select * from GroupMaster where SessionName='" + _sess + "' and GroupName='" + groupName + "' and BranchCode=" + Session["BranchCode"] + " and ";
                        if (Duplicate(sql1) == false)
                        {
                            //id = id + 1;                   
                            // oo.MessageBox("Hello", this.Page);
                            qry = " insert into GroupMaster(Id,GroupName,Remark,SessionName,BranchCode,LoginName,RecordDate)";
                            qry = qry + " select " + id + ",GroupName,Remark ,'" + _sess + "' ,BranchCode,LoginName,Getdate() from GroupMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and Id='" + pp + "'";
                            id = id + 1;
                            _oo.ProcedureDatabase(qry);
                        }
                    }
                    _con.Close();
                }
                catch (SqlException)
                {
                    _con.Close();
                }

            }
        }

        public void RangeBasisFineMaster()
        {
            if (Chk17.Checked)
            {
                string qry , pp, sql1 , cate;
                try
                {
                    _sql = "select max(Id)+1 as ID from RangeBasisFineMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                    int id;
                    id = Convert.ToInt32(_oo.ReturnTag(_sql, "ID"));


                    _sql = "select Id,FineCategory from RangeBasisFineMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                    _con.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(_sql, _con))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        try
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                pp = dt.Rows[i]["Id"].ToString();
                                cate = dt.Rows[i]["FineCategory"].ToString();
                                sql1 = "select * from RangeBasisFineMaster where FineCategory='" + cate + "' and SessionName='" + _sess + "' and BranchCode=" + Session["BranchCode"] + "";
                                if (Duplicate(sql1) == false)
                                {
                                    qry = " insert into RangeBasisFineMaster(Id,FromDate,ToDate, AmountPerDay ,FineCategory ,BranchCode ,LoginName ,SessionName ,RecordDate,MonthAmount)";
                                    qry = qry + " select " + id + ",FromDate,ToDate, AmountPerDay ,FineCategory ,BranchCode ,LoginName ,'" + _sess + "' ,GetDate(),MonthAmount from RangeBasisFineMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and Id=" + pp;
                                    id = id + 1;
                                    _oo.ProcedureDatabase(qry);
                                }
                            }
                            _con.Close();
                        }
                        catch (SqlException)
                        {
                            _con.Close();
                        }
                    }
                }
                catch (Exception)
                {
                    // ignored
                }
            }
        }

        public override void Dispose()
        {
            _con.Dispose();
            _oo.Dispose();
        }

        //public void DailyBasisFineMaster()
        //{
        //    string Sess = "", qry = "", pp = "", sql1 = "", cate = "";
        //    sql = "select max(Id)+1 as ID from DailyBasisFineMaster where SessionName='" + Session["SessionName"].ToString() + "'";
        //    int id = 0;
        //    if (oo.ReturnTag(sql, "ID").ToString() != "")
        //    {
        //        id = Convert.ToInt32(oo.ReturnTag(sql, "ID"));
        //    }
        //    else
        //    {
        //        id = 0;
        //    }


        //    sql = "select Id,FineCategory from DailyBasisFineMaster where SessionName='" + Session["SessionName"].ToString() + "'";
        //    con.Open();
        //    SqlDataAdapter da = new SqlDataAdapter(sql, con);
        //    DataTable dt = new DataTable();
        //    da.Fill(dt);
        //    try
        //    {

        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            pp =dt.Rows[i]["Id"].ToString();
        //            cate = dt.Rows[i]["FineCategory"].ToString();
        //            sql1 = "select * from DailyBasisFineMaster where FineCategory='" + cate + "' and SessionName='" + Sess + "'";
        //            if (Duplicate(sql1) == false)
        //            {

        //                qry = " insert into DailyBasisFineMaster(Id,FromDate, Amount ,DailyIncrement ,FineCategory ,BranchCode ,LoginName ,SessionName ,RecordDate,MonthAmount)";
        //                qry = qry + " select " + id + ",FromDate, Amount ,DailyIncrement ,FineCategory ,BranchCode ,LoginName ,'"+Sess+"' ,getdate(),MonthAmount from DailyBasisFineMaster where SessionName='" + Session["SessionName"].ToString() + "' and Id=" + pp;
        //                id = id + 1;
        //                oo.ProcedureDatabase(qry);
        //            }
        //        }
        //        con.Close();
        //    }
        //    catch (SqlException)
        //    {
        //        con.Close();
        //    }
        //} 
        #endregion
    }
}