using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using c4SmsNew;
using System.Web.Services;
using System.Globalization;


// ReSharper disable once InconsistentNaming
// ReSharper disable once CheckNamespace
public partial class admin_GetPass : Page
{
    private SqlConnection _con = new SqlConnection();
    private SqlCommand _cmd;
    private readonly Campus _oo = new Campus();
    private string _sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        _con = _oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file
        BLL.BLLInstance.LoadHeader("Report", header);  //in cs file
        BLL.BLLInstance.LoadHeader("Report", header1);  //in cs file
        if (!IsPostBack)
        {
          //  Loadadat();
            //if (Request.InputStream.Length > 0)
            //{
            //    using (StreamReader reader = new StreamReader(Request.InputStream))
            //    {
            //        string hexString = Server.UrlEncode(reader.ReadToEnd());
            //        string imageName = DateTime.Now.ToString("dd-MM-yy hh-mm-ss");
            //        string imagePath = string.Format("~/Uploads/Captures/{0}.png", imageName);
            //        File.WriteAllBytes(Server.MapPath(imagePath), ConvertHexToBytes(hexString));
            //        Session["CapturedImage"] = ResolveUrl(imagePath);
            //    }
            //}
        }
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        Showrecord();
        if (Grd1.Items.Count > 0)
        {
            div1.Attributes["class"] = "show";
            Div2.Attributes["class"] = "hide";
        }
        else
        {
            div1.Attributes["class"] = "hide";
        }

        
        
    }
  
    public void Showrecord()
    {
        var studentId = Request.Form[hfStudentId.UniqueID];
        if (studentId == string.Empty)
        {
            studentId = TxtEnter.Text.Trim();
        }
        _sql = "Select id,SrNo,StEnRCode,Name as StudentName,FatherName,ClassName,combineClassName,SectionName,Medium,Card,Convert(varchar(11),DateOfAdmiission) as DateOfAdmiission,CourseName,BranchName,FamilyContactNo,PhotoPath";
        _sql = _sql + " from AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") asr where srno='" + studentId + "' and Withdrwal is null";

        Grd1.DataSource = _oo.GridFill(_sql);
        Grd1.DataBind();

        if (Grd1.Items.Count > 0)
        {
                var img = (Image)Grd1.Items[0].FindControl("img");
                var studentImg = (HyperLink) Grd1.Items[0].FindControl("studentImg");
                var hylinkmoredetails = (HyperLink) Grd1.Items[0].FindControl("hylinkmoredetails");
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
                    cmd.Parameters.AddWithValue("@SrNo", studentId.ToString().Trim());
                    cmd.Parameters.AddWithValue("@action", "details");
                    SqlDataAdapter das = new SqlDataAdapter(cmd);
                    DataSet dsPhoto = new DataSet();
                    das.Fill(dsPhoto);
                    cmd.Parameters.Clear();

                    if (dsPhoto.Tables[0].Rows.Count > 0)
                    {
                        Session["studentImageurl"] = dsPhoto.Tables[0].Rows[0]["PhotoPath"].ToString() != string.Empty ? dsPhoto.Tables[0].Rows[0]["PhotoPath"] + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture) : "../img/user-pic/user-pic.jpg" + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture);
                        //hpstudentphoto.Value= dsPhoto.Tables[0].Rows[0]["PhotoPath"].ToString() != string.Empty ? dsPhoto.Tables[0].Rows[0]["PhotoPath"] + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture) : "../img/user-pic/user-pic.jpg" + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture);
                        //imgstphoto.ImageUrl = dsPhoto.Tables[0].Rows[0]["PhotoPath"].ToString() != string.Empty ? dsPhoto.Tables[0].Rows[0]["PhotoPath"] + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture) : "../img/user-pic/user-pic.jpg" + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture);
                        img.ImageUrl = dsPhoto.Tables[0].Rows[0]["PhotoPath"].ToString() != string.Empty ? dsPhoto.Tables[0].Rows[0]["PhotoPath"] + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture) : "../img/user-pic/user-pic.jpg" + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture);
                        studentImg.NavigateUrl = dsPhoto.Tables[0].Rows[0]["PhotoPath"].ToString() != string.Empty ? dsPhoto.Tables[0].Rows[0]["PhotoPath"] + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture) : "../img/user-pic/user-pic.jpg" + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture);
                        hylinkmoredetails.NavigateUrl = "../11/StudentRegView.aspx?print=1&id=" + _oo.ReturnTag(_sql, "StEnRCode");
                    }
                }
            }
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "captureImage();", true);
        }
        if (Grd1.Items.Count == 0)
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Invalid S.R.No.", "A");   
        }
    }

    protected void lnkSubmit_Click(object sender, EventArgs e)
    {
        if (Grd1.Items.Count <= 0) return;
        var label12 = (Label)Grd1.Items[0].FindControl("Label12");
        var label13 = (Label)Grd1.Items[0].FindControl("Label13");

        _cmd = new SqlCommand
        {
            CommandText = "GatePassDataProc",
            CommandType = CommandType.StoredProcedure,
            Connection = _con
        };
        _cmd.Parameters.AddWithValue("@SrNo", label12.Text.Trim());
        _cmd.Parameters.AddWithValue("@StEnRCode", label13.Text.Trim());
        _cmd.Parameters.AddWithValue("@GuardianName", txtGuardianname.Text.Trim());

        var guardianPhotoPath = "";
        var guardianPhotoName = "";
        var base64G = hdPhoto.Value;
        if (base64G != string.Empty)
        {
            guardianPhotoPath = @"../Uploads/GatePass/GuardianPhoto/";
            string date = DateTime.Now.ToString("yyyy_MM_dd");
            guardianPhotoName = "G" + label12.Text.Trim().Replace("/", "_") + "_" + date + ".jpg";
            guardianPhotoPath = guardianPhotoPath + guardianPhotoName;
            using (FileStream fs = new FileStream(Server.MapPath(guardianPhotoPath), FileMode.Create))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    byte[] data = Convert.FromBase64String(base64G);
                    bw.Write(data);
                    bw.Close();
                }
            }
        }
        else
        {
           
        }

        _cmd.Parameters.AddWithValue("@GuardianPhotoPath", guardianPhotoPath);
        _cmd.Parameters.AddWithValue("@GuardianPhotoName", guardianPhotoName);
        _cmd.Parameters.AddWithValue("@Relation", txtRelation.Text.Trim());
        _cmd.Parameters.AddWithValue("@GuardianContact", txtContactno.Text.Trim());
        _cmd.Parameters.AddWithValue("@Reason", txtReason.Text.Trim());

        string studentPhotoPath;
        var base64Std = hpstudentphoto.Value;
        if (base64Std != string.Empty)
        {
            studentPhotoPath = @"../Uploads/GatePass/StudentPhoto/";
            string date = DateTime.Now.ToString("yyyy_MM_dd");
            var studentPhotoName = label12.Text.Trim().Replace("/", "_") + "_" + date + ".jpg";
            studentPhotoPath = studentPhotoPath + studentPhotoName;
            using (FileStream fs = new FileStream(Server.MapPath((studentPhotoPath)), FileMode.Create))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    byte[] data = Convert.FromBase64String(base64Std);
                    bw.Write(data);
                    bw.Close();
                }
            }
            _cmd.Parameters.AddWithValue("@StudentPhotoPath", studentPhotoPath);
            _cmd.Parameters.AddWithValue("@SPhoto", "Yes");
        }
        else
        {
            if (!string.Equals((string)Session["studentImageurl"], string.Empty, StringComparison.Ordinal))
            {
                studentPhotoPath = Session["studentImageurl"].ToString();
                _cmd.Parameters.AddWithValue("@StudentPhotoPath", studentPhotoPath);
                _cmd.Parameters.AddWithValue("@SPhoto", "Yes");
            }
            else
            {
                _cmd.Parameters.AddWithValue("@SPhoto", "No");
            }
        }

        _cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
        _cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
        _cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"]);
        _con.Open();
        try
        {
            var count = _cmd.ExecuteNonQuery();
            _con.Close();
            if (count == 1)
            {
                Loadadat();
                _sql = "select FamilyContactNo from StudentFamilyDetails  where Srno='" + label12.Text.Trim() + "' and BranchCode=" + Session["BranchCode"] + "  and SessionName='" + Session["SessionName"] + "'";
                var conta = _oo.ReturnTag(_sql, "FamilyContactNo");
                SendFeesSms(conta, label12.Text.Trim(), txtRelation.Text, txtGuardianname.Text, txtReason.Text);
                
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Submitted successfully.", "S");
                var image4 = (Image)Repeater1.Items[0].FindControl("Image4");
                var lblidmax = (Label)Repeater1.Items[0].FindControl("Label25");
                if (image4.ImageUrl != "")
                {
                    Session["Id"] = lblidmax.Text;
                    Response.Redirect("PrintGetPass.aspx?print=1", false);
                    //ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('PrintGetPass.aspx?print=1');", true);
                }
                else
                {
                    Div2.Attributes["class"] = "hide";
                }
            }
            else
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Sorry, Record Not successfully!", "W");
                _con.Close();
            }
        }
        catch (Exception ex)
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, ex.Message, "W");
        }
    }

    public void SendFeesSms(string fmobileNo, string srno, string relation, string guardionname, string reasion)
    {
        _sql = "Select HitValue from SMSActivateDeactivate where BranchCode=" + Session["BranchCode"] + "";
        if (_oo.ReturnTag(_sql, "HitValue") == "") return;
        if (_oo.ReturnTag(_sql, "HitValue") != "true") return;
        var sadpNew = new SMSAdapterNew();

        _sql = "Select (FirstName+' '+MiddleName+' '+LastName) as StudentName from StudentGenaralDetail  where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        _sql = _sql + "    and  Srno='" + srno + "'";

        var mess = "Dear Guardian, " + _oo.ReturnTag(_sql, "StudentName") + "(S.R. No.-" + srno + ") has been sent back to home with " + relation + "" + ' ' + "" + guardionname + " due to " + reasion + "";

        if (fmobileNo == "") return;
        _sql = "Select SmsSent From SmsEmailMaster Where Id='23'";
        if (_oo.ReturnTag(_sql, "SmsSent").Trim() == "true")
        {
            sadpNew.Send(mess, fmobileNo, "23");                  
        }
    }

    public void Loadadat()
    {
        _sql = "Select Top 1 gpd.Id as MaxId,gpd.SrNo,asr.Name as StudentName,asr.FatherName,ClassName, combineclassname,SectionName,gpd.LoginName,";
        _sql = _sql + " FamilyContactNo,Reason,GuardianName,Relation,GuardionContact,gpd.RecordDate as date,ISNULL(gpd.StudentPhotoPath,ISNULL(asr.PhotoPath,'')) as StudentPhotopath,gpd.GuardianPhotoPath";
        _sql = _sql + " from AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") asr";
        _sql = _sql + " inner join GatePassData gpd on gpd.SrNo=asr.SrNo  and gpd.BranchCode=" + Session["BranchCode"] + "";
        _sql = _sql + " order by gpd.id desc";
        Repeater1.DataSource = _oo.GridFill(_sql);
        Repeater1.DataBind();
        Repeater2.DataSource = _oo.GridFill(_sql);
        Repeater2.DataBind();
       
    }

   protected void lnkPrint_Click(object sender, EventArgs e)
   {
       PrintHelper_New.ctrl = abc2;
       ScriptManager.RegisterClientScriptBlock(Page, GetType(), "", "var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}", true);
       
   }

    // ReSharper disable once UnusedMember.Local
    private static byte[] ConvertHexToBytes(string hex)
    {
        byte[] bytes = new byte[hex.Length / 2];
        for (int i = 0; i < hex.Length; i += 2)
        {
            bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
        }
        return bytes;
    }

    [WebMethod(EnableSession = true)]
    public static string GetCapturedImage()
    {
        string url = HttpContext.Current.Session["CapturedImage"].ToString();
        HttpContext.Current.Session["CapturedImage"] = null;
        return url;
    }
    
    protected void TxtEnter_TextChanged(object sender, EventArgs e)
    {
        Showrecord();
        if (Grd1.Items.Count > 0)
        {
            
            div1.Attributes["class"] = "show";
            Div2.Attributes["class"] = "hide";
        }
        else
        {
            div1.Attributes["class"] = "hide";
        }
    }
}