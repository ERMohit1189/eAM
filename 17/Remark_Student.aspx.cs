using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;

// ReSharper disable once CheckNamespace
// ReSharper disable once InconsistentNaming
public partial class SuperAdmin_Customized_Message : Page
{
    SqlConnection _con;
    private readonly Campus _oo;
    string _sql, _smsResponse = string.Empty;

    public SuperAdmin_Customized_Message()
    {
        _con = new SqlConnection();
        _oo = new Campus();
    }
    protected void Page_PreInIt(object sender, EventArgs e)
    {
        if (Session["Logintype"] == null) { Response.Redirect("~/default.aspx"); }
        // ReSharper disable once PossibleNullReferenceException
        if (Session["Logintype"].ToString() == "Admin")
        {
            MasterPageFile = "~/Master/admin_root-manager.master";
        }
        else if (Session["Logintype"].ToString() == "Staff")
        {
            MasterPageFile = "~/Staff/staff_root-manager.master";
        }
        else if (Session["Logintype"].ToString() == "Guardian")
        {
            MasterPageFile = "~/sp/sp_root-manager.master";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        _con = _oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);
        if (!IsPostBack)
        {
            var isInternetConnectionAvailable = NetworkInterface.GetIsNetworkAvailable();
            if (isInternetConnectionAvailable == false)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "alert", "alertmsg()", true);
            }
            txtDate.Text = BAL.objBal.CurrentDate("yyyy MMM dd");

            //LoadClass();

            //// ReSharper disable once PossibleNullReferenceException
            //BLL.BLLInstance.loadSection(drpSection, Session["SessionName"].ToString(), drpClassCourse.SelectedValue);
            //BLL.BLLInstance.loadBranch(drpBranch, Session["SessionName"].ToString(), drpClassCourse.SelectedValue);


            LoadClass(drpClassCourse);
            BAL.objBal.fillSelectvalue(drpSection, "<--Select-->", "-1");
            BAL.objBal.fillSelectvalue(drpBranch, "<--Select-->", "-1");
        }
    }

    //public void LoadClass()
    //{
    //    _sql = "Select ClassName,Id from ClassMaster where SessionName='" + Session["SessionName"] + "' Order by CIDOrder ";
    //    _oo.FillDropDown_withValue(_sql, drpClassCourse, "ClassName","Id");
    //    drpClassCourse.Items.Insert(0, "<--Select-->");
    //}

    private void LoadClass(DropDownList drpclass)
    {
        if (Session["logintype"].ToString() == "Admin")
        {
            BLL.BLLInstance.loadClass(drpClassCourse, Session["SessionName"].ToString());
        }
        else
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
            param.Add(new SqlParameter("@EmpCode", Session["LoginName"].ToString()));
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));

            drpclass.DataSource = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("GetClassTeacherClassName_Proc", param);
            drpclass.DataTextField = "ClassName";
            drpclass.DataValueField = "Id";
            drpclass.DataBind();
            drpclass.Items.Insert(0, new ListItem("<--Select-->", "0"));

        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        if (Grd.Rows.Count > 0)
        {
            //_sql = "Select HitValue from SMSActivateDeactivate where BranchCode=" + Session["BranchCode"] + "";
            //if (_oo.ReturnTag(_sql, "HitValue") != "")
            //{
            //    if (_oo.ReturnTag(_sql, "HitValue") == "true")
            //    {
            //        _sql = "Select SmsSent From SmsEmailMaster where Id='22' ";
            //        if (_oo.ReturnTag(_sql, "SmsSent").Trim() == "true")
            //        {
                        var contact = "";
                        foreach (GridViewRow gvr in Grd.Rows)
                        {
                            var chk = (CheckBox)gvr.FindControl("Chk");
                            // ReSharper disable once IdentifierTypo
                            var fmobileNo = (Label)gvr.FindControl("Label30");
                            // ReSharper disable once IdentifierTypo
                            var srnoid = (Label)gvr.FindControl("Label1");
                            if (chk.Checked)
                            {
                                Reamarkinsert(srnoid.Text);
                                if (txtMessage.Text.Trim() != "")
                                {
                                    if (contact == "")
                                    {
                                        if (fmobileNo.Text.Length == 10)
                                        {
                                            contact = fmobileNo.Text;
                                        }
                                    }
                                    else
                                    {
                                        if (fmobileNo.Text.Length == 10)
                                        {
                                            contact = contact + "," + fmobileNo.Text;
                                        }
                                    }
                                }
                            }
                        }
                        if (contact != "")
                        {
                            var bb = NetworkInterface.GetIsNetworkAvailable();

                            if (bb == false)
                            {
                                Campus camp = new Campus(); camp.msgbox(Page, msg1, "Internet connections are not available", "A");   
                            }
                            else
                            {
                                var str = SendFeesSms(contact);

                                var noHtml = Regex.Replace(str, @"<[^>]+>|&nbsp;", "").Trim();

                                // ReSharper disable once IdentifierTypo
                                var noHtmlNormalised = Regex.Replace(noHtml, @"\s{2,}", " ");

                                double value;

                                noHtmlNormalised = noHtmlNormalised.Replace("S.", "");

                                noHtmlNormalised = noHtmlNormalised.Replace("Job Id:", "");

                                noHtmlNormalised = noHtmlNormalised.Replace(" ", "");

                                noHtmlNormalised = noHtmlNormalised.Split(',').Length > 2 ? noHtmlNormalised.Split(',')[2] : noHtmlNormalised;

                                var flag = double.TryParse(noHtmlNormalised.Trim(), out value);

                                if (flag)
                                {
                                    Campus camp = new Campus(); camp.msgbox(Page, msg1, "Submitted successfully", "S");
                                    //_oo.ClearControls(Page);
                                    //ddlcategory.ClearSelection();
                                    //drpClassCourse.ClearSelection();
                                }
                                else
                                {
                                    Campus camp = new Campus(); camp.msgbox(Page, msg1, noHtmlNormalised, "W");
                                }
                            }
                        }
                        //        }
                        //        else
                        //        {
                        //            // ReSharper disable once StringLiteralTypo
                        //            Campus camp = new Campus(); camp.msgbox(Page, msg1, "SMS sending not allowed!", "A"); 
                        //        }

                        //    }
                        //    else
                        //    {
                        //        // ReSharper disable once StringLiteralTypo
                        //        Campus camp = new Campus(); camp.msgbox(Page, msg1, "SMS sending not allowed!", "A"); 
                        //    }
                        //}
                        //else
                        //{
                        //    // ReSharper disable once StringLiteralTypo
                        //    Campus camp = new Campus(); camp.msgbox(Page, msg1, "SMS sending not allowed!", "A"); 
                        //}
                    }
                }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {

    }

    // ReSharper disable once IdentifierTypo
    public string SendFeesSms(string fmobileNo)
    {
        // ReSharper disable once IdentifierTypo
       // var sadpNew = new SMSAdapterNew();
        //// ReSharper disable once JoinDeclarationAndInitializer
        string mess;
        mess = txtMessage.Text.Trim();
        if (mess != "")
        {
            //  _smsResponse = sadpNew.Send(mess, fmobileNo);
            _smsResponse = ""+fmobileNo;
        }
        txtMessage.Text = "";
        //txtremark.Text = "";
        return _smsResponse;       
    }

    private void Reamarkinsert(string srNo)
    {
        try
        {
            using (var cmd = new SqlCommand())
            {
                if (_con.State == ConnectionState.Closed)
                {
                    _con.Open();
                }
                cmd.CommandText = "USP_RemarkStudent";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = _con;
                cmd.Parameters.AddWithValue("@QueryFor", "I");
                cmd.Parameters.AddWithValue("@SrNo", srNo);
                cmd.Parameters.AddWithValue("@LoginUserID", Session["LoginName"].ToString());
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                cmd.Parameters.AddWithValue("@Category", ddlcategory.SelectedValue);
                cmd.Parameters.AddWithValue("@Remark", txtremark.Text.Trim());
                cmd.Parameters.AddWithValue("@MessageBody", txtMessage.Text.Trim());
                cmd.Parameters.AddWithValue("@RecordDate", Convert.ToDateTime(txtDate.Text.Trim()));
                cmd.ExecuteNonQuery();
                Campus camp = new Campus(); camp.msgbox(Page, msg1, "Submitted successfully.", "S");
            }
        }
        catch (Exception ex)
        {
        }
        finally
        {
            if (_con.State == ConnectionState.Open) { _con.Close(); }
        }
    }
    private void LoadSection(DropDownList drpsection, DropDownList drpclass)
    {
        if (Session["Logintype"].ToString() == "Admin")
        {
            BLL.BLLInstance.loadSection(drpsection, Session["SessionName"].ToString(), drpclass.SelectedValue);
        }
        else
        {
            _sql = "Select SectionName,sm.Id from ClassTeacherMaster T1";
            _sql +=  " inner join SectionMaster sm on sm.Id=T1.SectionId and sm.SessionName=t1.SessionName";
            _sql +=  " where EmpCode='" + Session["LoginName"] + "' and IsClassTeacher=1 and T1.SessionName='" + Session["SessionName"] + "'";
            _sql +=  " and t1.Classid=" + drpclass.SelectedValue + " and sm.BranchCode=" + Session["BranchCode"] + " and T1.BranchCode=" + Session["BranchCode"] + "";
            BAL.objBal.FillDropDown_withValue(_sql, drpsection, "SectionName", "Id");
        }
    }

    private void LoadBranch(DropDownList drpbranch, DropDownList drpclass)
    {
        if (Session["Logintype"].ToString() == "Admin")
        {
            BLL.BLLInstance.loadBranch(drpbranch, Session["SessionName"].ToString(), drpclass.SelectedValue);
        }
        else
        {
            _sql = "Select BranchName,bm.Id from ClassTeacherMaster T1";
            _sql +=  "   inner join BranchMaster bm on bm.Id=T1.BranchId and bm.SessionName=t1.SessionName";
            _sql +=  "   where EmpCode='" + Session["LoginName"] + "' and IsClassTeacher=1 and";
            _sql +=  "   T1.SessionName='" + Session["SessionName"] + "' and bm.BranchCode=" + Session["BranchCode"] + " and T1.BranchCode=" + Session["BranchCode"] + " and T1.Classid='" + drpclass.SelectedValue + "'";
            BAL.objBal.FillDropDown_withValue(_sql, drpbranch, "BranchName", "Id");
        }
    }
    protected void ChkAll_CheckedChanged(object sender, EventArgs e)
    {
        // ReSharper disable once IdentifierTypo
        var chkall = (CheckBox)Grd.HeaderRow.FindControl("ChkAll");
        if (chkall.Checked)
        {
            foreach (GridViewRow gvr in Grd.Rows)
            {
                var chk = (CheckBox)gvr.FindControl("Chk");
                chk.Checked = true;
            }
        }
        else
        {
            foreach (GridViewRow gvr in Grd.Rows)
            {
                var chk = (CheckBox)gvr.FindControl("Chk");
                chk.Checked = false;
            }
        }
    }
    protected void drpClassCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        //BLL.BLLInstance.loadSection(drpSection, Session["SessionName"].ToString(), drpClassCourse.SelectedValue);
        //BLL.BLLInstance.loadBranch(drpBranch, Session["SessionName"].ToString(), drpClassCourse.SelectedValue);  
        LoadSection(drpSection, drpClassCourse);
        LoadBranch(drpBranch, drpClassCourse);
        LoadGrid(drpClassCourse);
    }

    public void LoadGrid(Control ctrl)
    {
        _sql = "Select *from AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") where Withdrwal is null";
        if (drpClassCourse.SelectedIndex != 0)
        {
            if (Session["logintype"].ToString() == "Admin")
            {
                _sql +=  " and ClassName='" + drpClassCourse.SelectedItem + "'";
                if (drpSection.SelectedValue != "-1")
                {
                    _sql +=  " and SectionName='" + drpSection.SelectedItem + "'";
                }
                if (drpBranch.SelectedValue != "-1")
                {
                    _sql +=  " and BranchName='" + drpBranch.SelectedItem + "'";
                }
            }
            else
            {
                _sql +=  " and ClassName='" + drpClassCourse.SelectedItem + "'";
                _sql +=  " and SectionName='" + drpSection.SelectedItem + "'";
                _sql +=  " and BranchName='" + drpBranch.SelectedItem + "'";
            }
           
        }
        _sql +=  "  order by CIDOrder,Name";

        Grd.DataSource = _oo.GridFill(_sql);
        Grd.DataBind();

        if (Grd.Rows.Count == 0)
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Sorry, No Record found!", "A"); 
        }
    }
    protected void drpSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadGrid(drpSection);
    }
    protected void drpBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadGrid(drpBranch);
    }

    public override void Dispose()
    {
        _con.Dispose();
        _oo.Dispose();
    }
}