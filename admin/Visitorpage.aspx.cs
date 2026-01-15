using System;
using System.Linq;
using System.Web.UI;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using c4SmsNew;

// ReSharper disable once InconsistentNaming
// ReSharper disable once CheckNamespace
// ReSharper disable once IdentifierTypo
public partial class admin_Visitorpage : Page
{
    readonly Campus _clsCam;
    SqlConnection _con;
    readonly Campus _oo;

    private string _sql;
#pragma warning disable 169
    private string _ss;
#pragma warning restore 169
    private string _msg;
    // ReSharper disable once InconsistentNaming
#pragma warning disable 414
    private string SQL;
#pragma warning restore 414
    private string _editid;
    private string _mess = String.Empty;
#pragma warning restore 169

    // ReSharper disable once IdentifierTypo
    public admin_Visitorpage()
    {
        _clsCam = new Campus();
        _con = new SqlConnection();
        _oo = new Campus();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((string) Session["LoginName"] == "")
        {
            Response.Redirect("default.aspx");
        }
        Session.LCID = 2057;
        _con = _oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);
        if (!IsPostBack)
        {
            try
            {
                if (Request.QueryString.Keys.Count > 0)
                {
                     _editid = Request.QueryString["print"];
                    Editgetdata(_editid);
                }
                else
                { txtReligion.Focus(); }
            }
            catch (Exception ex)
            {
                throw new Exception("some reason to rethrow", ex);
            }
        }
    }

    // ReSharper disable once IdentifierTypo
    public void Editgetdata(string edit)
    {
        var cmd = new SqlCommand();
        using (var da = new SqlDataAdapter())
        {
            try
            {
                cmd.CommandText = "USP_Visitors";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = _con;
                cmd.Parameters.AddWithValue("@QueryFor", "EDITS");
                cmd.Parameters.AddWithValue("@Id", edit);
                da.SelectCommand = cmd;
                var dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    // ReSharper disable once IdentifierTypo
                    var nameno = dt.Rows[0]["VisitorName"].ToString();

                    // ReSharper disable once IdentifierTypo
                    var splitname = nameno.Split('(');

                    if (splitname.Count() == 1)
                    {
                        txtReligion.Text = splitname[0];
                    }
                    else
                    {
                        txtReligion.Text = splitname[0];
                        txtnopurson.Text = splitname[1].Replace("+", "").Replace(")", "");
                    }

                    RadioButtonList1g.SelectedValue = dt.Rows[0]["Gender"].ToString();
                    txtmobileno.Text = dt.Rows[0]["ContactNo"].ToString();
                    emailidtxt.Text = dt.Rows[0]["EmailID"].ToString();
                    subjectvisittxt.Text = dt.Rows[0]["SubjectVisit"].ToString();
                    txtSearch.Text = dt.Rows[0]["WhomMeet"].ToString();
                    addresstxt.Text = dt.Rows[0]["Address"].ToString();
                    Avatar.ImageUrl = dt.Rows[0]["PhotoPath"].ToString();
                    txtpassno.Text = dt.Rows[0]["passno"].ToString();
                }
            }
            catch (Exception ex)
            { throw new Exception("some reason to rethrow", ex); }
        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        try
        {

            if (Request.QueryString.Keys.Count > 0)
            {
                SQL = "EDIT";
                ValidationEdit(LinkButton1);
                LinkButton1.Text = "Update";
            }
            else
            {
                SQL = "I";
                Validation(LinkButton1);
            }

        }
        catch (Exception ex)
        { Campus camp = new Campus(); camp.msgbox(Page, msgbox, ex.Message, "W"); }
    }

    // ReSharper disable once IdentifierTypo
    private void Validation(Control cntrl)
    {
        _msg = "";

        if (txtReligion.Text.Trim() == "" && _msg == "")
        {
            _msg = "Enter Name !";
            txtReligion.Focus();
        }
        if (txtmobileno.Text.Trim() == "" && _msg == "")
        {
            _msg = "Enter Mobile Number !";
            txtmobileno.Focus();
        }
        if (subjectvisittxt.Text.Trim() == string.Empty && _msg == "")
        {
            _msg = "Enter Subject/Purpose of visit !";
            subjectvisittxt.Focus();
        }
        if (txtSearch.Text.Trim() == string.Empty && _msg == "")
        {
            _msg = "Enter Whom to meet !";
            subjectvisittxt.Focus();
        }

        if (_msg != string.Empty)
        {
            ShowMsg(_msg, "A");
        }
        else
        {
            SetBookDay(cntrl); 
        }
    }
    // ReSharper disable once IdentifierTypo
    private void ValidationEdit(Control cntrl1)
    {
        EditVisitor(cntrl1);
    }
    // ReSharper disable once InconsistentNaming
    // ReSharper disable once ParameterHidesMember
    private void ShowMsg(string MSG, string type)
    {

        Campus camp = new Campus(); camp.msgbox(Page, msgbox, BLL.BLLInstance.FetchMSG(MSG), type);
    }

    // ReSharper disable once IdentifierTypo
    // ReSharper disable once UnusedParameter.Local
      private void SetBookDay(Control cntrl)
        {
            lblMsg.Text = "";

        using (var cmd = new SqlCommand())
        {
        cmd.CommandText = "USP_Visitors";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = _con;
        cmd.Parameters.AddWithValue("@QueryFor", "I");
        // ReSharper disable once IdentifierTypo
        string nameno;
        if (txtnopurson.Text.Trim() == "")
        {
            nameno = txtReligion.Text.Trim().Replace("'", "").ToUpper();
        }
        else
        {
            nameno = txtReligion.Text.Trim().Replace("'", "").ToUpper() + " (+" + txtnopurson.Text.Trim() + ")";
        }
        cmd.Parameters.AddWithValue("@VisitorName", nameno);
        cmd.Parameters.AddWithValue("@Gender", RadioButtonList1g.SelectedValue);
        cmd.Parameters.AddWithValue("@ContactNo", txtmobileno.Text.Trim().Replace("'", ""));
        cmd.Parameters.AddWithValue("@EmailID", emailidtxt.Text.Trim().Replace("'", "").ToUpper());
        cmd.Parameters.AddWithValue("@SubjectVisit", subjectvisittxt.Text.Trim().Replace("'", "").ToUpper());

        if (hfStudentId.Value.Trim() == txtSearch.Text.Trim())
        {
            cmd.Parameters.AddWithValue("@WhomMeet", txtSearch.Text.Trim().Replace("'", "").ToUpper());
            cmd.Parameters.AddWithValue("@passno", txtpassno.Text.Trim().Replace("'", "").ToUpper());
            cmd.Parameters.AddWithValue("@Address", addresstxt.Text.Trim().Replace("'", "").ToUpper());
            cmd.Parameters.AddWithValue("@SignofOfficer", null);
            cmd.Parameters.AddWithValue("@SingofSecurity", null);

            // ReSharper disable once RedundantAssignment
            // ReSharper disable once InconsistentNaming
            var FileName = String.Empty;
            // ReSharper disable once RedundantAssignment
            // ReSharper disable once InconsistentNaming
            var FilePath = String.Empty;
            // ReSharper disable once RedundantAssignment
            // ReSharper disable once InconsistentNaming
            var base64std = hdPhoto.Value;
            if (!string.IsNullOrEmpty(hdPhoto.Value))
            {
                FilePath = "~/uploads/VisitorPhoto/";
                if (!Directory.Exists(Server.MapPath(FilePath)))
                {
                    Directory.Exists(Server.MapPath(FilePath));
                }
                var rn = new Random();
                // ReSharper disable once StringLiteralTypo
                FileName = "V_ID" + "_" + DateTime.Now.ToString("dd_mm_yyyy") + "_" + rn.Next(12345, 98765) + ".jpg";
                // ReSharper disable once UseStringInterpolation
                FilePath = string.Format("~/uploads/VisitorPhoto/{0}", FileName);
                using (FileStream fs = new FileStream(Server.MapPath(FilePath), FileMode.Create))
                {
                    using (BinaryWriter bw = new BinaryWriter(fs))
                    {
                        var data = Convert.FromBase64String(base64std);
                        bw.Write(data);
                        bw.Close();
                    }
                }

                cmd.Parameters.AddWithValue("@PhotoPath", FilePath);
                cmd.Parameters.AddWithValue("@PhotoName", FileName);

                cmd.Parameters.Add("@msg", SqlDbType.VarChar, 50);
                cmd.Parameters["@msg"].Direction = ParameterDirection.Output;

                try
                {
                    _con.Open();
                    var n = cmd.ExecuteNonQuery();

                    if (n > 0)
                    {
                        // ReSharper disable once IdentifierTypo
                        var printid = (string)cmd.Parameters["@msg"].Value;
                        _oo.ClearControls(Page);
                        _oo.MessageBox("", Page);
                        using (var sda = new SqlDataAdapter("SELECT MOBILENO  FROM VISITORMONO", _con))
                        {
                            // ReSharper disable once IdentifierTypo
                            var sadpNew = new SMSAdapterNew();
                            _sql = "Select SmsSent From SmsEmailMaster where Id='27' ";
                            if (_oo.ReturnTag(_sql, "SmsSent").Trim() == "true")
                            {
                                _sql = "SELECT * from   Visitors where id='" + printid.Trim() + "' ";

                                var name1 = _oo.ReturnTag(_sql, "VisitorName").Trim();
                                var name11 = _oo.ReturnTag(_sql, "WhomMeet").Trim();
                                var name111 = _oo.ReturnTag(_sql, "id").Trim();

                                _mess = name1.Trim() + " is here to meet " + name11.Trim() + ".\n" + "Gate Pass No.- " + name111.Trim();

                                var dt = new DataTable();
                                sda.Fill(dt);
                                if (dt.Rows.Count > 0)
                                {
                                    for (int ii = 0; ii < dt.Rows.Count; ii++)
                                    {
                                        // ReSharper disable once IdentifierTypo
                                        var fmobileNo = dt.Rows[ii]["MOBILENO"].ToString();
                                        if (fmobileNo != "")
                                        {
                                            try
                                            {
                                                sadpNew.Send(_mess, fmobileNo, "");
                                            }
                                            catch
                                            {
                                                // ignored
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        ScriptManager.RegisterClientScriptBlock(Page, GetType(), "onclick", "var winpop=window.open('Visitorpageprint.aspx?print=1&empcode=" + printid.Trim() + "','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}", true);
                        txtReligion.Text = txtmobileno.Text = emailidtxt.Text = subjectvisittxt.Text = txtSearch.Text = addresstxt.Text = "";
                    }
                    _con.Close();
                    hdPhoto.Value = "";
                }
                catch (SqlException ee) { throw new Exception("some reason to rethrow", ee); }
                finally { if (_con.State == ConnectionState.Open) { _con.Close(); } }
            }
            else
            {
                lblMsg.Text = "Please capture visitor's image!";
            }
        }
        else
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Please Select Correct Name Whom to meet", "W");
        }
    }
}
    // ReSharper disable once UnusedParameter.Local
    // ReSharper disable once IdentifierTypo
    private void EditVisitor(Control cntrl1)
    {
            _editid = Request.QueryString["print"];
        using (var cmd1 = new SqlCommand())
        {
            cmd1.CommandText = "USP_Visitors";
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Connection = _con;
            cmd1.Parameters.AddWithValue("@QueryFor", "EDIT");
            cmd1.Parameters.AddWithValue("@Id", _editid);
            string nameno;
            if (txtnopurson.Text.Trim() == "")
            {
                nameno = txtReligion.Text.Trim().Replace("'", "").ToUpper();
            }
            else
            {
                nameno = txtReligion.Text.Trim().Replace("'", "").ToUpper() + " (+" + txtnopurson.Text.Trim() + ")";
            }
            cmd1.Parameters.AddWithValue("@VisitorName", nameno);
            cmd1.Parameters.AddWithValue("@Gender", RadioButtonList1g.SelectedValue);
            cmd1.Parameters.AddWithValue("@ContactNo", txtmobileno.Text.Trim().Replace("'", ""));
            cmd1.Parameters.AddWithValue("@EmailID", emailidtxt.Text.Trim().Replace("'", "").ToUpper());
            cmd1.Parameters.AddWithValue("@SubjectVisit", subjectvisittxt.Text.Trim().Replace("'", "").ToUpper());
            //if (hfStudentId.Value.ToString() == txtSearch.Text.ToString().Trim())
            //{
            cmd1.Parameters.AddWithValue("@passno", txtpassno.Text.Trim().Replace("'", "").ToUpper());
            cmd1.Parameters.AddWithValue("@WhomMeet", txtSearch.Text.Trim().Replace("'", "").ToUpper());
            cmd1.Parameters.AddWithValue("@Address", addresstxt.Text.Trim().Replace("'", "").ToUpper());

            try
            {
                _con.Open();
                var n1 = cmd1.ExecuteNonQuery();
                if (n1 > 0)
                {
                    Response.Redirect("VisitorOut.aspx", false);
                }
                _con.Close();
                hdPhoto.Value = "";
            }
            catch (SqlException ee) { throw new Exception("some reason to rethrow", ee); }
            finally { if (_con.State == ConnectionState.Open) { _con.Close(); } }
        }
        //}
        //else
        //{
        //    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please Select Correct Name Whom to meet", "W");
        //}
    }

    public override void Dispose()
    {
        _clsCam.Dispose();
        _con.Dispose();
    }
}