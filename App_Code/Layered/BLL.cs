using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


/// <summary>
/// Summary description for BLL
/// </summary>
public partial class BLL : UserControl
{
    private DataTable _dt;
    private string _sql = "";

    public string branchCode()
    {
        string branchCode = "0";
        try
        {
            branchCode = HttpContext.Current.Session["BranchCode"].ToString();
        }
        catch (Exception)
        {
        }
        return branchCode;
    }

    public string SessionName()
    {
        string SessionName = "-1";
        try
        {


            if (HttpContext.Current.Session["SessionName"] != null)
            {
                SessionName = HttpContext.Current.Session["SessionName"].ToString();
            }
        }
        catch (Exception)
        {
        }
        return SessionName;
    }

    public string LoadControls(string path, Control parentId)
    {
        var msg = "";
        try
        {
            var UC = LoadControl(path);
            parentId.Controls.Add(UC);
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }

        return msg;
    }

    public DataSet GetStudentDetails(string registervalue, string sessionName, string branchcode)
    {
        var param = new List<SqlParameter>
        {
            new SqlParameter("@registervalue", registervalue),
            new SqlParameter("@sessionName", sessionName),
            new SqlParameter("@branchcode", branchcode)
        };


        return DLL.objDll.Sp_SelectRecord_usingExecuteDataset("USP_GetStudentDetails", param);
    }

    public DataSet GetInstallment(string registervalue, string sessionName, string branchcode)
    {
        var param = new List<SqlParameter>
        {
            new SqlParameter("@registervalue", registervalue),
            new SqlParameter("@sessionName", sessionName),
            new SqlParameter("@branchcode", branchcode)
        };


        return DLL.objDll.Sp_SelectRecord_usingExecuteDataset("USP_GetInstallment", param);
    }

    public DataSet GetTransportInstallment(string registervalue, string sessionName, string branchcode)
    {
        var param = new List<SqlParameter>
        {
            new SqlParameter("@registervalue", registervalue),
            new SqlParameter("@sessionName", sessionName),
            new SqlParameter("@branchcode", branchcode)
        };


        return DLL.objDll.Sp_SelectRecord_usingExecuteDataset("USP_GetInstallment_Trnsport", param);
    }

    public DataSet GetHostelInstallment(string registervalue, string sessionName, string branchcode)
    {
        var param = new List<SqlParameter>
        {
            new SqlParameter("@SrNoOrEmpId", registervalue),
            new SqlParameter("@SessionName", sessionName),
            new SqlParameter("@BranchCode", branchcode),
            new SqlParameter("@Action", "Select")
        };


        return DLL.objDll.Sp_SelectRecord_usingExecuteDataset("HostelFeeAllotmentForCondidateProc", param);
    }
    public DataSet GetHostelInstallmentFee(string registervalue, string sessionName, string branchcode)
    {
        var param = new List<SqlParameter>
        {
            new SqlParameter("@SrNoOrEmpId", registervalue),
            new SqlParameter("@SessionName", sessionName),
            new SqlParameter("@BranchCode", branchcode),
            new SqlParameter("@Action", "SelectInstallment")
        };


        return DLL.objDll.Sp_SelectRecord_usingExecuteDataset("HostelFeeAllotmentForCondidateProc", param);
    }


    public string LoadFeeWd(Control parentId)
    {
        var msg = "";
        try
        {
            const string path = "~/admin/usercontrol/widgets/fee.ascx";
            var UC = LoadControl(path);
            parentId.Controls.Add(UC);
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }

        return msg;
    }
    public string LoadBirthDayAndAnniversary(Control parentId)
    {
        var msg = "";
        try
        {
            const string path = "~/admin/usercontrol/widgets/BirthDayAndAnniversary.ascx";
            var UC = LoadControl(path);
            parentId.Controls.Add(UC);
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }

        return msg;
    }

    public string LoadAttWd(Control parentId)
    {
        var msg = "";
        try
        {
            const string path = "~/admin/usercontrol/widgets/attendance.ascx";
            var UC = LoadControl(path);
            parentId.Controls.Add(UC);
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }

        return msg;
    }

    public string LoadBulletinWd(Control parentId)
    {
        var msg = "";
        try
        {
            const string path = "~/admin/usercontrol/widgets/bulletin.ascx";
            var UC = LoadControl(path);
            parentId.Controls.Add(UC);
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }

        return msg;
    }

    public string LoadPlannerWd(Control parentId)
    {
        var msg = "";
        try
        {
            const string path = "~/admin/usercontrol/widgets/planner.ascx";
            var UC = LoadControl(path);
            parentId.Controls.Add(UC);
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }

        return msg;
    }

    public string LoadStfAttWd(Control parentId)
    {
        var msg = "";
        try
        {
            const string path = "~/admin/usercontrol/widgets/staffattendance.ascx";
            var UC = LoadControl(path);
            parentId.Controls.Add(UC);
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }

        return msg;
    }

    public string LoadAccountsWd(Control parentId)
    {
        var msg = "";
        try
        {
            const string path = "~/admin/usercontrol/widgets/accounts.ascx";
            var UC = LoadControl(path);
            parentId.Controls.Add(UC);
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }

        return msg;
    }

    public string LoadHeader(string templateFor, Control header)
    {
        var msg = "";
        string path;
        if (templateFor != string.Empty)
        {
            path = "~/admin/usercontrol/" + templateFor + ".ascx";
        }
        else
        {
            path = "~/admin/usercontrol/RHT1.ascx";
        }
        var UC = LoadControl(path);
        try
        {
            var param = new List<SqlParameter> { new SqlParameter("@TemplateFor", templateFor), new SqlParameter("@BranchCode", branchCode()) };
            var ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("GetHeaderTemplateProc", param);
            if (ds.Tables.Count > 0)
            {
                _dt = new DataTable();
                _dt = ds.Tables[0];
                if (_dt.Rows.Count > 0)
                {
                    UC = hideControl(UC, "ctrl1", _dt.Rows[0][0].ToString() != "False");

                    UC = hideControl(UC, "ctrl2", _dt.Rows[0][1].ToString() != "False");

                    UC = hideControl(UC, "ctrl3", _dt.Rows[0][2].ToString() != "False");

                    UC = hideControl(UC, "ctrl4", _dt.Rows[0][3].ToString() != "False");

                    UC = hideControl(UC, "ctrl5", _dt.Rows[0][4].ToString() != "False");

                    UC = hideControl(UC, "ctrl6", _dt.Rows[0][5].ToString() != "False");

                    UC = hideControl(UC, "ctrl7", _dt.Rows[0][6].ToString() != "False");

                    UC = hideControl(UC, "ctrl8", _dt.Rows[0][7].ToString() != "False");

                    UC = hideControl(UC, "ctrl9", _dt.Rows[0][8].ToString() != "False");

                    UC = hideControl(UC, "ctrl10", _dt.Rows[0][9].ToString() != "False");

                    UC = hideControl(UC, "ctrl11", _dt.Rows[0][10].ToString() != "False");

                    UC = hideControl(UC, "ctrl12", _dt.Rows[0][11].ToString() != "False");

                    if (_dt.Rows[0][12].ToString() == "True")
                    {
                        UC = hideControl(UC, "ctrl13", true);
                        UC = hideControl(UC, "ctrl14", false);

                    }
                    else
                    {
                        UC = hideControl(UC, "ctrl13", false);
                        UC = hideControl(UC, "ctrl1", false);
                        hideControl(UC, "ctrlmain");
                        UC = hideControl(UC, "ctrl14", _dt.Rows[0][0].ToString() != "False");
                    }

                    UC = hideControl(UC, "ctrl15", _dt.Rows[0][13].ToString() != "False");
                    UC = hideControl(UC, "ctrl16", _dt.Rows[0][14].ToString() != "False");

                    UC = hideControl(UC, "ctrl17", _dt.Rows[0][13].ToString() != "False" && _dt.Rows[0][14].ToString() != "False");

                    header.Controls.Add(UC);
                }
                else
                {
                    header.Controls.Add(UC);
                }
            }
            else
            {
                header.Controls.Add(UC);
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return msg;
    }
    public string LoadHeader2(string templateFor, Control header, string branchcode)
    {
        var msg = "";
        string path;
        if (templateFor != string.Empty)
        {
            path = "~/admin/usercontrol/" + templateFor + ".ascx";
        }
        else
        {
            path = "~/admin/usercontrol/RHT1.ascx";
        }
        var UC = LoadControl(path);
        try
        {
            var param = new List<SqlParameter> { new SqlParameter("@TemplateFor", templateFor), new SqlParameter("@BranchCode", branchcode) };
            var ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("GetHeaderTemplateProc", param);
            if (ds.Tables.Count > 0)
            {
                _dt = new DataTable();
                _dt = ds.Tables[0];
                if (_dt.Rows.Count > 0)
                {
                    UC = hideControl(UC, "ctrl1", _dt.Rows[0][0].ToString() != "False");

                    UC = hideControl(UC, "ctrl2", _dt.Rows[0][1].ToString() != "False");

                    UC = hideControl(UC, "ctrl3", _dt.Rows[0][2].ToString() != "False");

                    UC = hideControl(UC, "ctrl4", _dt.Rows[0][3].ToString() != "False");

                    UC = hideControl(UC, "ctrl5", _dt.Rows[0][4].ToString() != "False");

                    UC = hideControl(UC, "ctrl6", _dt.Rows[0][5].ToString() != "False");

                    UC = hideControl(UC, "ctrl7", _dt.Rows[0][6].ToString() != "False");

                    UC = hideControl(UC, "ctrl8", _dt.Rows[0][7].ToString() != "False");

                    UC = hideControl(UC, "ctrl9", _dt.Rows[0][8].ToString() != "False");

                    UC = hideControl(UC, "ctrl10", _dt.Rows[0][9].ToString() != "False");

                    UC = hideControl(UC, "ctrl11", _dt.Rows[0][10].ToString() != "False");

                    UC = hideControl(UC, "ctrl12", _dt.Rows[0][11].ToString() != "False");

                    if (_dt.Rows[0][12].ToString() == "True")
                    {
                        UC = hideControl(UC, "ctrl13", true);
                        UC = hideControl(UC, "ctrl14", false);

                    }
                    else
                    {
                        UC = hideControl(UC, "ctrl13", false);
                        UC = hideControl(UC, "ctrl1", false);
                        hideControl(UC, "ctrlmain");
                        UC = hideControl(UC, "ctrl14", _dt.Rows[0][0].ToString() != "False");
                    }

                    UC = hideControl(UC, "ctrl15", _dt.Rows[0][13].ToString() != "False");


                    header.Controls.Add(UC);
                }
                else
                {
                    header.Controls.Add(UC);
                }
            }
            else
            {
                header.Controls.Add(UC);
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return msg;
    }
    public string LoadReportCardHeader(string templateFor, Control header)
    {
        var msg = "";
        string path;
        if (templateFor != string.Empty)
        {
            path = "~/admin/usercontrol/" + templateFor + ".ascx";
        }
        else
        {
            path = "~/admin/usercontrol/RHT1.ascx";
        }
        var UC = LoadControl(path);
        try
        {
            var param = new List<SqlParameter> {
                new SqlParameter("@TemplateFor", templateFor),
                new SqlParameter("@BranchCode", branchCode())
        };
            var ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("GetHeaderTemplateProc", param);
            if (ds.Tables.Count > 0)
            {
                _dt = new DataTable();
                _dt = ds.Tables[0];
                if (_dt.Rows.Count > 0)
                {
                    if (_dt.Rows[0][0].ToString() == "False")
                    {
                        UC = hideControl(UC, "ctrl1", false);
                    }
                    else
                    {
                        UC = hideControl(UC, "ctrl1", true);
                    }

                    if (_dt.Rows[0][1].ToString() == "False")
                    {
                        UC = VisibilControl(UC, "ctrl2", "hidden");
                    }
                    else
                    {
                        UC = VisibilControl(UC, "ctrl2", "visible");
                    }

                    if (_dt.Rows[0][2].ToString() == "False")
                    {
                        UC = VisibilControl(UC, "ctrl3", "hidden");
                    }
                    else
                    {
                        UC = VisibilControl(UC, "ctrl3", "visible");
                    }

                    if (_dt.Rows[0][3].ToString() == "False")
                    {
                        UC = hideControl(UC, "ctrl4", false);
                    }
                    else
                    {
                        UC = hideControl(UC, "ctrl4", true);
                    }

                    if (_dt.Rows[0][4].ToString() == "False")
                    {
                        UC = hideControl(UC, "ctrl5", false);
                    }
                    else
                    {
                        UC = hideControl(UC, "ctrl5", true);
                    }

                    if (_dt.Rows[0][5].ToString() == "False")
                    {
                        UC = VisibilControl(UC, "ctrl6", "hidden");
                    }
                    else
                    {
                        UC = VisibilControl(UC, "ctrl6", "visible");
                    }

                    if (_dt.Rows[0][6].ToString() == "False")
                    {
                        UC = hideControl(UC, "ctrl7", false);
                    }
                    else
                    {
                        UC = hideControl(UC, "ctrl7", true);
                    }

                    if (_dt.Rows[0][7].ToString() == "False")
                    {
                        UC = hideControl(UC, "ctrl8", false);
                    }
                    else
                    {
                        UC = hideControl(UC, "ctrl8", true);
                    }

                    if (_dt.Rows[0][8].ToString() == "False")
                    {
                        UC = hideControl(UC, "ctrl9", false);
                    }
                    else
                    {
                        UC = hideControl(UC, "ctrl9", true);
                    }

                    if (_dt.Rows[0][9].ToString() == "False")
                    {
                        UC = VisibilControl(UC, "ctrl10", "hidden");
                    }
                    else
                    {
                        UC = VisibilControl(UC, "ctrl10", "visible");
                    }

                    if (_dt.Rows[0][10].ToString() == "False")
                    {
                        UC = hideControl(UC, "ctrl11", false);
                    }
                    else
                    {
                        UC = hideControl(UC, "ctrl11", true);
                    }

                    if (_dt.Rows[0][11].ToString() == "False")
                    {
                        UC = hideControl(UC, "ctrl12", false);
                    }
                    else
                    {
                        UC = hideControl(UC, "ctrl12", true);
                    }

                    if (_dt.Rows[0][12].ToString() == "True")
                    {
                        UC = hideControl(UC, "ctrl13", true);
                        UC = hideControl(UC, "ctrl14", false);
                    }
                    else
                    {
                        UC = hideControl(UC, "ctrl13", false);
                        UC = hideControl(UC, "ctrl1", false);
                        hideControl(UC, "ctrlmain");
                        if (_dt.Rows[0][0].ToString() == "False")
                        {
                            UC = hideControl(UC, "ctrl14", false);
                        }
                        else
                        {
                            UC = hideControl(UC, "ctrl14", true);
                        }
                    }

                    if (_dt.Rows[0][13].ToString() == "False")
                    {
                        UC = hideControl(UC, "ctrl15", false);
                    }
                    else
                    {
                        UC = hideControl(UC, "ctrl15", true);
                    }

                    UC = hideControl(UC, "ctrl16", _dt.Rows[0][14].ToString() != "False");

                    UC = hideControl(UC, "ctrl17", _dt.Rows[0][13].ToString() != "False" && _dt.Rows[0][14].ToString() != "False");

                    header.Controls.Add(UC);
                }
                else
                {
                    header.Controls.Add(UC);
                }
            }
            else
            {
                header.Controls.Add(UC);
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return msg;
    }

    public string LoadCertificateHeader(Control header)
    {
        var msg = "";
        var path = "~/admin/usercontrol/Certificate.ascx";

        var UC = LoadControl(path);
        try
        {
            var param = new List<SqlParameter>();
            param.Add(new SqlParameter("@TemplateFor", "Certificate"));
            param.Add(new SqlParameter("@BranchCode", branchCode()));
            var ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("GetHeaderTemplateProc", param);
            if (ds.Tables.Count > 0)
            {
                _dt = new DataTable();
                _dt = ds.Tables[0];
                if (_dt.Rows.Count > 0)
                {
                    if (_dt.Rows[0][0].ToString() == "False")
                    {
                        UC = hideControl(UC, "ctrl1", false);
                    }
                    else
                    {
                        UC = hideControl(UC, "ctrl1", true);
                    }

                    if (_dt.Rows[0][1].ToString() == "False")
                    {
                        UC = hideControl(UC, "ctrl2", false);
                    }
                    else
                    {
                        UC = hideControl(UC, "ctrl2", true);
                    }

                    if (_dt.Rows[0][2].ToString() == "False")
                    {
                        UC = hideControl(UC, "ctrl3", false);
                    }
                    else
                    {
                        UC = hideControl(UC, "ctrl3", true);
                    }

                    if (_dt.Rows[0][3].ToString() == "False")
                    {
                        UC = hideControl(UC, "ctrl4", false);
                    }
                    else
                    {
                        UC = hideControl(UC, "ctrl4", true);
                    }

                    if (_dt.Rows[0][4].ToString() == "False")
                    {
                        UC = hideControl(UC, "ctrl5", false);
                    }
                    else
                    {
                        UC = hideControl(UC, "ctrl5", true);
                    }

                    if (_dt.Rows[0][5].ToString() == "False")
                    {
                        UC = hideControl(UC, "ctrl6", false);
                    }
                    else
                    {
                        UC = hideControl(UC, "ctrl6", true);
                    }

                    if (_dt.Rows[0][6].ToString() == "False")
                    {
                        UC = hideControl(UC, "ctrl7", false);
                    }
                    else
                    {
                        UC = hideControl(UC, "ctrl7", true);
                    }

                    if (_dt.Rows[0][7].ToString() == "False")
                    {
                        UC = hideControl(UC, "ctrl8", false);
                    }
                    else
                    {
                        UC = hideControl(UC, "ctrl8", true);
                    }

                    if (_dt.Rows[0][8].ToString() == "False")
                    {
                        UC = hideControl(UC, "ctrl9", false);
                    }
                    else
                    {
                        UC = hideControl(UC, "ctrl9", true);
                    }

                    if (_dt.Rows[0][9].ToString() == "False")
                    {
                        UC = hideControl(UC, "ctrl10", false);
                    }
                    else
                    {
                        UC = hideControl(UC, "ctrl10", true);
                    }

                    if (_dt.Rows[0][10].ToString() == "False")
                    {
                        UC = hideControl(UC, "ctrl11", false);
                    }
                    else
                    {
                        UC = hideControl(UC, "ctrl11", true);
                    }

                    if (_dt.Rows[0][11].ToString() == "False")
                    {
                        UC = hideControl(UC, "ctrl12", false);
                    }
                    else
                    {
                        UC = hideControl(UC, "ctrl12", true);
                    }
                    header.Controls.Add(UC);
                }
                else
                {
                    header.Controls.Add(UC);
                }
            }
            else
            {
                header.Controls.Add(UC);
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return msg;
    }

    public string LoadReportHeader(Control header)
    {
        var msg = "";
        var path = "~/admin/usercontrol/Report.ascx";

        var UC = LoadControl(path);
        try
        {
            var param = new List<SqlParameter>();
            param.Add(new SqlParameter("@TemplateFor", "Report"));
            param.Add(new SqlParameter("@BranchCode", branchCode()));
            var ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("GetHeaderTemplateProc", param);
            if (ds.Tables.Count > 0)
            {
                _dt = new DataTable();
                _dt = ds.Tables[0];
                if (_dt.Rows.Count > 0)
                {
                    if (_dt.Rows[0][0].ToString() == "False")
                    {
                        UC = hideControl(UC, "ctrl1", false);
                    }
                    else
                    {
                        UC = hideControl(UC, "ctrl1", true);
                    }

                    if (_dt.Rows[0][1].ToString() == "False")
                    {
                        UC = hideControl(UC, "ctrl2", false);
                    }
                    else
                    {
                        UC = hideControl(UC, "ctrl2", true);
                    }

                    if (_dt.Rows[0][2].ToString() == "False")
                    {
                        UC = hideControl(UC, "ctrl3", false);
                    }
                    else
                    {
                        UC = hideControl(UC, "ctrl3", true);
                    }

                    if (_dt.Rows[0][3].ToString() == "False")
                    {
                        UC = hideControl(UC, "ctrl4", false);
                    }
                    else
                    {
                        UC = hideControl(UC, "ctrl4", true);
                    }

                    if (_dt.Rows[0][4].ToString() == "False")
                    {
                        UC = hideControl(UC, "ctrl5", false);
                    }
                    else
                    {
                        UC = hideControl(UC, "ctrl5", true);
                    }

                    if (_dt.Rows[0][5].ToString() == "False")
                    {
                        UC = hideControl(UC, "ctrl6", false);
                    }
                    else
                    {
                        UC = hideControl(UC, "ctrl6", true);
                    }

                    if (_dt.Rows[0][6].ToString() == "False")
                    {
                        UC = hideControl(UC, "ctrl7", false);
                    }
                    else
                    {
                        UC = hideControl(UC, "ctrl7", true);
                    }

                    if (_dt.Rows[0][7].ToString() == "False")
                    {
                        UC = hideControl(UC, "ctrl8", false);
                    }
                    else
                    {
                        UC = hideControl(UC, "ctrl8", true);
                    }

                    if (_dt.Rows[0][8].ToString() == "False")
                    {
                        UC = hideControl(UC, "ctrl9", false);
                    }
                    else
                    {
                        UC = hideControl(UC, "ctrl9", true);
                    }

                    if (_dt.Rows[0][9].ToString() == "False")
                    {
                        UC = hideControl(UC, "ctrl10", false);
                    }
                    else
                    {
                        UC = hideControl(UC, "ctrl10", true);
                    }

                    if (_dt.Rows[0][10].ToString() == "False")
                    {
                        UC = hideControl(UC, "ctrl11", false);
                    }
                    else
                    {
                        UC = hideControl(UC, "ctrl11", true);
                    }

                    if (_dt.Rows[0][11].ToString() == "False")
                    {
                        UC = hideControl(UC, "ctrl12", false);
                    }
                    else
                    {
                        UC = hideControl(UC, "ctrl12", true);
                    }
                    header.Controls.Add(UC);
                }
                else
                {
                    header.Controls.Add(UC);
                }
            }
            else
            {
                header.Controls.Add(UC);
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return msg;
    }

    public string LoadReceiptHeader(Control header)
    {
        var msg = "";
        var path = "~/admin/usercontrol/Receipt.ascx";

        var UC = LoadControl(path);
        try
        {
            var param = new List<SqlParameter>();
            param.Add(new SqlParameter("@TemplateFor", "Receipt"));
            param.Add(new SqlParameter("@BranchCode", branchCode()));
            var ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("GetHeaderTemplateProc", param);
            if (ds.Tables.Count > 0)
            {
                _dt = new DataTable();
                _dt = ds.Tables[0];
                if (_dt.Rows.Count > 0)
                {
                    if (_dt.Rows[0][0].ToString() == "False")
                    {
                        UC = hideControl(UC, "ctrl1", false);
                    }
                    else
                    {
                        UC = hideControl(UC, "ctrl1", true);
                    }

                    if (_dt.Rows[0][1].ToString() == "False")
                    {
                        UC = hideControl(UC, "ctrl2", false);
                    }
                    else
                    {
                        UC = hideControl(UC, "ctrl2", true);
                    }

                    if (_dt.Rows[0][2].ToString() == "False")
                    {
                        UC = hideControl(UC, "ctrl3", false);
                    }
                    else
                    {
                        UC = hideControl(UC, "ctrl3", true);
                    }

                    if (_dt.Rows[0][3].ToString() == "False")
                    {
                        UC = hideControl(UC, "ctrl4", false);
                    }
                    else
                    {
                        UC = hideControl(UC, "ctrl4", true);
                    }

                    if (_dt.Rows[0][4].ToString() == "False")
                    {
                        UC = hideControl(UC, "ctrl5", false);
                    }
                    else
                    {
                        UC = hideControl(UC, "ctrl5", true);
                    }

                    if (_dt.Rows[0][5].ToString() == "False")
                    {
                        UC = hideControl(UC, "ctrl6", false);
                    }
                    else
                    {
                        UC = hideControl(UC, "ctrl6", true);
                    }

                    if (_dt.Rows[0][6].ToString() == "False")
                    {
                        UC = hideControl(UC, "ctrl7", false);
                    }
                    else
                    {
                        UC = hideControl(UC, "ctrl7", true);
                    }

                    if (_dt.Rows[0][7].ToString() == "False")
                    {
                        UC = hideControl(UC, "ctrl8", false);
                    }
                    else
                    {
                        UC = hideControl(UC, "ctrl8", true);
                    }

                    if (_dt.Rows[0][8].ToString() == "False")
                    {
                        UC = hideControl(UC, "ctrl9", false);
                    }
                    else
                    {
                        UC = hideControl(UC, "ctrl9", true);
                    }

                    if (_dt.Rows[0][9].ToString() == "False")
                    {
                        UC = hideControl(UC, "ctrl10", false);
                    }
                    else
                    {
                        UC = hideControl(UC, "ctrl10", true);
                    }

                    if (_dt.Rows[0][10].ToString() == "False")
                    {
                        UC = hideControl(UC, "ctrl11", false);
                    }
                    else
                    {
                        UC = hideControl(UC, "ctrl11", true);
                    }

                    if (_dt.Rows[0][11].ToString() == "False")
                    {
                        UC = hideControl(UC, "ctrl12", false);
                    }
                    else
                    {
                        UC = hideControl(UC, "ctrl12", true);
                    }
                    header.Controls.Add(UC);
                }
                else
                {
                    header.Controls.Add(UC);
                }
            }
            else
            {
                header.Controls.Add(UC);
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return msg;
    }

    public Control hideControl(Control parent, string id, bool isdisplay)
    {
        foreach (Control _ChildControl in parent.Controls)
        {
            if (_ChildControl.ID == id)
            {
                _ChildControl.Visible = isdisplay;
            }
            if (_ChildControl.ID == "ctrlmain")
            {
                hideControl(_ChildControl, id, isdisplay);
            }
        }

        return parent;
    }

    public Control VisibilControl(Control parent, string id, string isdisplay)
    {
        foreach (Control _ChildControl in parent.Controls)
        {
            if (_ChildControl.ID == id)
            {
                ((HtmlGenericControl)_ChildControl).Style.Add("visibility", isdisplay);
            }
            if (_ChildControl.ID == "ctrlmain")
            {
                VisibilControl(_ChildControl, id, isdisplay);
            }
        }

        return parent;
    }

    public void hideControl(Control parent, string id)
    {
        foreach (Control _ChildControl in parent.Controls)
        {
            if (_ChildControl.ID == "ctrlmain")
            {
                ((HtmlGenericControl)_ChildControl).Attributes.Add("class", "text-center col-lg-8 col-md-8 col-xs-8 col-sm-8 no-padding ");
            }
        }
    }



    public static string GetSiteRoot()
    {
        var port = System.Web.HttpContext.Current.Request.ServerVariables["SERVER_PORT"];
        if (port == null || port == "80" || port == "443")
            port = "";
        else
            port = ":" + port;

        var protocol = System.Web.HttpContext.Current.Request.ServerVariables["SERVER_PORT_SECURE"];
        if (protocol == null || protocol == "0")
            protocol = "http://";
        else
            protocol = "https://";

        var sOut = protocol + System.Web.HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + port + System.Web.HttpContext.Current.Request.ApplicationPath;

        if (sOut.EndsWith("/"))
        {
            sOut = sOut.Substring(0, sOut.Length - 1);
        }

        return sOut;
    }

    public void CountControls(ControlCollection controls, System.Web.UI.DataVisualization.Charting.Chart chartControl)
    {
        var tbCount = 0;
        var tbUnCount = 0;
        var cbCount = 0;
        var filled = "";
        var Unfilled = "";

        foreach (Control wc in controls)
        {
            if (wc is TextBox)
            {
                if (((TextBox)wc).Enabled == true)
                {
                    if (((TextBox)wc).Text != "")
                    {
                        tbCount++;
                    }
                    else
                    {
                        tbUnCount++;
                    }
                }
            }
            else if (wc is CheckBox)
            {
                cbCount++;
            }

        }


        filled = "Complete " + tbCount.ToString();
        Unfilled = "UnComplete " + tbUnCount.ToString();
        double[] yValues = { tbCount, tbUnCount };
        string[] xValues = { filled, Unfilled };
        chartControl.Series["Series1"].Points.DataBindXY(xValues, yValues);

    }

    public void ClearControls(Control parent, System.Web.UI.DataVisualization.Charting.Chart chartControl)
    {
        var tbCount = 0;
        var tbUnCount = 0;
        //var cbCount = 0;
        var filled = "";
        var Unfilled = "";

        foreach (Control _ChildControl in parent.Controls)
        {
            //if ((_ChildControl.Controls.Count > 0))
            //{
            //    ClearControls(_ChildControl, chartControl);
            //}
            if (_ChildControl is TextBox)
            {
                if (((TextBox)_ChildControl).Text != "")
                {
                    tbCount++;
                }
                else
                {
                    tbUnCount++;
                }
            }
            else if (_ChildControl is CheckBox)
            {
                ((CheckBox)_ChildControl).Checked = false;
            }


        }
        filled = "Complete " + tbCount.ToString();
        Unfilled = "UnComplete " + tbUnCount.ToString();
        double[] yValues = { tbCount, tbUnCount };
        string[] xValues = { filled, Unfilled };
        chartControl.Series["Series1"].Points.DataBindXY(xValues, yValues);



    }

    public string loadDefaultvalue(string value, Control ctrl)
    {
        var objBal = new BAL.Set_DefaultSelectedValue();
        var msg = "";
        try
        {
            objBal.defaultvalueof = value;
            objBal.BranchCode = int.Parse(branchCode());

            if (ctrl is DropDownList)
            {
                ((DropDownList)ctrl).SelectedValue = (string)DAL.objDal.Get_SetDefaultValue(objBal).Item2;
                msg = (string)DAL.objDal.Get_SetDefaultValue(objBal).Item1;
            }
            if (ctrl is TextBox)
            {
                ((TextBox)ctrl).Text = (string)DAL.objDal.Get_SetDefaultValue(objBal).Item2;
                msg = (string)DAL.objDal.Get_SetDefaultValue(objBal).Item2;
            }

        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }

        return msg;
    }

    public void setDefaultSelectedIndex(Control ctrl, int value)
    {
        try
        {
            if (ctrl is DropDownList)
            {
                if (value > 0)
                {
                    ((DropDownList)ctrl).SelectedIndex = value;
                }
                else
                {
                    ((DropDownList)ctrl).SelectedIndex = 0;
                }
            }
            else if (ctrl is RadioButtonList)
            {
                if (value > 0)
                {
                    foreach (ListItem li in ((RadioButtonList)ctrl).Items)
                    {
                        li.Selected = false;
                    }
                }
                else
                {
                    ((RadioButtonList)ctrl).SelectedIndex = value;
                }
            }
            else if (ctrl is CheckBoxList)
            {
                if (value > 0)
                {
                    foreach (ListItem li in ((CheckBoxList)ctrl).Items)
                    {
                        li.Selected = false;
                    }
                }
                else
                {
                    ((CheckBoxList)ctrl).SelectedIndex = value;
                }
            }

        }
        catch (Exception)
        {

        }
    }

    public void loadLoginUsersList(DropDownList drpUserList, string loginTypeId)
    {
        _sql = "Select *from(select LoginName,LoginId from LoginTab where LoginTypeId=@LoginTypeId and (IsActive is NULL or IsActive=1) and BranchId=" + branchCode() + " ";
        _sql = _sql + " Union ";
        _sql = _sql + " Select '<--Select-->' LoginName,'-1' LoginId) T1";
        if (loginTypeId == "3")
        {
            _sql = _sql + " order by CASE WHEN ISNUMERIC(Right(LoginName,3))=1 THEN Right(LoginName,3) ELSE 0 END";
        }
        else
        {
            _sql = _sql + " order by LoginName";
        }
        var param = new List<SqlParameter>();
        param.Add(new SqlParameter("@LoginTypeId", loginTypeId));
        drpUserList.DataSource = DLL.objDll.SelectRecord_usingExecuteDataset(_sql, param);
        drpUserList.DataTextField = "LoginName";
        drpUserList.DataValueField = "LoginId";
        drpUserList.DataBind();
    }

    public void loadCourse(DropDownList drpCourse, string sessionName)
    {
        _sql = "Select CourseName,Id from CourseMaster Where SessionName=@SessionName and BranchCode=@BranchCode";
        _sql = _sql + " Union ";
        _sql = _sql + " Select '<--Select-->','-1' order by Id";

        var param = new List<SqlParameter>();
        param.Add(new SqlParameter("@SessionName", sessionName));
        param.Add(new SqlParameter("@BranchCode", branchCode()));
        drpCourse.DataSource = DLL.objDll.SelectRecord_usingExecuteDataset(_sql, param);
        drpCourse.DataTextField = "CourseName";
        drpCourse.DataValueField = "Id";
        drpCourse.DataBind();
    }
    public void loadCourseWithoutSession(DropDownList drpCourse, string sessionName)
    {
        _sql = "Select CourseName,Id from CourseMaster Where  BranchCode=@BranchCode";
        _sql = _sql + " Union ";
        _sql = _sql + " Select '<--Select-->','-1' order by Id";

        var param = new List<SqlParameter>();
        param.Add(new SqlParameter("@SessionName", sessionName));
        param.Add(new SqlParameter("@BranchCode", branchCode()));
        drpCourse.DataSource = DLL.objDll.SelectRecord_usingExecuteDataset(_sql, param);
        drpCourse.DataTextField = "CourseName";
        drpCourse.DataValueField = "Id";
        drpCourse.DataBind();
    }

    public void loadClassUseingCourse(DropDownList drpClass, string courseid, string sessionName)
    {
        _sql = "Select ClassName,Id from ClassMaster Where SessionName=@SessionName and Course=@Courseid  and BranchCode=@BranchCode";
        _sql = _sql + " Union ";
        _sql = _sql + " Select '<--Select-->','-1' order by Id";

        var param = new List<SqlParameter>();
        param.Add(new SqlParameter("@SessionName", sessionName));
        param.Add(new SqlParameter("@Courseid", courseid));
        param.Add(new SqlParameter("@BranchCode", branchCode()));
        drpClass.DataSource = DLL.objDll.SelectRecord_usingExecuteDataset(_sql, param);
        drpClass.DataTextField = "ClassName";
        drpClass.DataValueField = "Id";
        drpClass.DataBind();
    }

    public void loadClass(Control control, string sessionName)
    {
        _sql = "Select ClassName,Id from ClassMaster Where SessionName=@SessionName and BranchCode=@BranchCode";
        _sql = _sql + " Union ";
        _sql = _sql + " Select '<--Select-->','-1' order by Id";

        var list = control as DropDownList;
        if (list != null)
        {
            var param = new List<SqlParameter> { new SqlParameter("@SessionName", sessionName), new SqlParameter("@BranchCode", branchCode()) };
            list.DataSource = DLL.objDll.SelectRecord_usingExecuteDataset(_sql, param);
            list.DataTextField = "ClassName";
            list.DataValueField = "Id";
            list.DataBind();
        }

        else if (control is RadioButtonList)
        {
            var param = new List<SqlParameter> { new SqlParameter("@SessionName", sessionName), new SqlParameter("@BranchCode", branchCode()) };
            ((RadioButtonList)control).DataSource = DLL.objDll.SelectRecord_usingExecuteDataset(_sql, param);
            ((RadioButtonList)control).DataTextField = "ClassName";
            ((RadioButtonList)control).DataValueField = "Id";
            ((RadioButtonList)control).DataBind();
        }
        else if (control is CheckBoxList)
        {
            var param = new List<SqlParameter> { new SqlParameter("@SessionName", sessionName), new SqlParameter("@BranchCode", branchCode()) };
            ((CheckBoxList)control).DataSource = DLL.objDll.SelectRecord_usingExecuteDataset(_sql, param);
            ((CheckBoxList)control).DataTextField = "ClassName";
            ((CheckBoxList)control).DataValueField = "Id";
            ((CheckBoxList)control).DataBind();
        }
    }
    public void loadBatch(Control control, string sessionName)
    {
        _sql = "Select BatchName,ID from BatchMst_tb Where SessionName=@SessionName and BranchCode=@BranchCode";
        _sql = _sql + " Union ";
        _sql = _sql + " Select '<--Select-->','-1' order by Id";

        var list = control as DropDownList;
        if (list != null)
        {
            var param = new List<SqlParameter> { new SqlParameter("@SessionName", sessionName), new SqlParameter("@BranchCode", branchCode()) };
            list.DataSource = DLL.objDll.SelectRecord_usingExecuteDataset(_sql, param);
            list.DataTextField = "BatchName";
            list.DataValueField = "ID";
            list.DataBind();
        }

        else if (control is RadioButtonList)
        {
            var param = new List<SqlParameter> { new SqlParameter("@SessionName", sessionName), new SqlParameter("@BranchCode", branchCode()) };
            ((RadioButtonList)control).DataSource = DLL.objDll.SelectRecord_usingExecuteDataset(_sql, param);
            ((RadioButtonList)control).DataTextField = "BatchName";
            ((RadioButtonList)control).DataValueField = "ID";
            ((RadioButtonList)control).DataBind();
        }
        else if (control is CheckBoxList)
        {
            var param = new List<SqlParameter> { new SqlParameter("@SessionName", sessionName), new SqlParameter("@BranchCode", branchCode()) };
            ((CheckBoxList)control).DataSource = DLL.objDll.SelectRecord_usingExecuteDataset(_sql, param);
            ((CheckBoxList)control).DataTextField = "BatchName";
            ((CheckBoxList)control).DataValueField = "ID";
            ((CheckBoxList)control).DataBind();
        }
    }
    public void loadClass2(Control control, string sessionName, string branchCode)
    {
        _sql = "Select ClassName,Id from ClassMaster Where SessionName=@SessionName and BranchCode=@BranchCode";
        _sql = _sql + " Union ";
        _sql = _sql + " Select '<--Select-->','-1' order by Id";

        var list = control as DropDownList;
        if (list != null)
        {
            var param = new List<SqlParameter> { new SqlParameter("@SessionName", sessionName), new SqlParameter("@BranchCode", branchCode) };
            list.DataSource = DLL.objDll.SelectRecord_usingExecuteDataset(_sql, param);
            list.DataTextField = "ClassName";
            list.DataValueField = "Id";
            list.DataBind();
        }
        else if (control is RadioButtonList)
        {
            var param = new List<SqlParameter> { new SqlParameter("@SessionName", sessionName), new SqlParameter("@BranchCode", branchCode) };
            ((RadioButtonList)control).DataSource = DLL.objDll.SelectRecord_usingExecuteDataset(_sql, param);
            ((RadioButtonList)control).DataTextField = "ClassName";
            ((RadioButtonList)control).DataValueField = "Id";
            ((RadioButtonList)control).DataBind();
        }
        else if (control is CheckBoxList)
        {
            var param = new List<SqlParameter> { new SqlParameter("@SessionName", sessionName), new SqlParameter("@BranchCode", branchCode) };
            ((CheckBoxList)control).DataSource = DLL.objDll.SelectRecord_usingExecuteDataset(_sql, param);
            ((CheckBoxList)control).DataTextField = "ClassName";
            ((CheckBoxList)control).DataValueField = "Id";
            ((CheckBoxList)control).DataBind();
        }
    }
    public void loadDoctype(Control control)
    {
        _sql = "select distinct Id, DocumentType  from dt_CreateDocumentName where BranchCode=" + branchCode() + "";

        var list = control as DropDownList;
        if (list != null)
        {
            list.DataSource = DLL.objDll.SelectRecord_usingExecuteDataset(_sql);
            list.DataTextField = "DocumentType";
            list.DataValueField = "Id";
            list.DataBind();
        }
        else if (control is RadioButtonList)
        {
            ((RadioButtonList)control).DataSource = DLL.objDll.SelectRecord_usingExecuteDataset(_sql);
            ((RadioButtonList)control).DataTextField = "DocumentType";
            ((RadioButtonList)control).DataValueField = "Id";
            ((RadioButtonList)control).DataBind();
        }
        else if (control is CheckBoxList)
        {
            ((CheckBoxList)control).DataSource = DLL.objDll.SelectRecord_usingExecuteDataset(_sql);
            ((CheckBoxList)control).DataTextField = "DocumentType";
            ((CheckBoxList)control).DataValueField = "Id";
            ((CheckBoxList)control).DataBind();

        }
    }
    public void LoadClasswithoutselect(Control control, string sessionName)
    {
        _sql = "Select ClassName,Id from ClassMaster Where SessionName=@SessionName and BranchCode=@BranchCode order by CIDORDER";

        var list = control as DropDownList;
        if (list != null)
        {
            var param = new List<SqlParameter> { new SqlParameter("@SessionName", sessionName), new SqlParameter("@BranchCode", branchCode()) };
            list.DataSource = DLL.objDll.SelectRecord_usingExecuteDataset(_sql, param);
            list.DataTextField = "ClassName";
            list.DataValueField = "Id";
            list.DataBind();
        }
        else if (control is RadioButtonList)
        {
            var param = new List<SqlParameter> { new SqlParameter("@SessionName", sessionName), new SqlParameter("@BranchCode", branchCode()) };
            ((RadioButtonList)control).DataSource = DLL.objDll.SelectRecord_usingExecuteDataset(_sql, param);
            ((RadioButtonList)control).DataTextField = "ClassName";
            ((RadioButtonList)control).DataValueField = "Id";
            ((RadioButtonList)control).DataBind();
        }
        else if (control is CheckBoxList)
        {
            var param = new List<SqlParameter> { new SqlParameter("@SessionName", sessionName), new SqlParameter("@BranchCode", branchCode()) };
            ((CheckBoxList)control).DataSource = DLL.objDll.SelectRecord_usingExecuteDataset(_sql, param);
            ((CheckBoxList)control).DataTextField = "ClassName";
            ((CheckBoxList)control).DataValueField = "Id";
            ((CheckBoxList)control).DataBind();
        }
    }

    public DataSet getClasseswithValue(string sessionName)
    {
        _sql = "Select ClassName,Id from ClassMaster Where SessionName=@SessionName and BranchCode=@BranchCode order by CIDORDER";

        var param = new List<SqlParameter> { new SqlParameter("@SessionName", sessionName), new SqlParameter("@BranchCode", branchCode()) };
        return DLL.objDll.SelectRecord_usingExecuteDataset(_sql, param);
    }

    public void loadBranch(DropDownList drpBranch, string sessionName, string classid)
    {
        _sql = "Select BranchName,Id from BranchMaster Where ClassId=@ClassId and SessionName=@SessionName and BranchCode=@BranchCode";
        _sql = _sql + " Union ";
        _sql = _sql + " Select '<--Select-->','-1' order by Id";

        var param = new List<SqlParameter>();
        param.Add(new SqlParameter("@SessionName", sessionName));
        param.Add(new SqlParameter("@ClassId", classid));
        param.Add(new SqlParameter("@BranchCode", branchCode()));
        drpBranch.DataSource = DLL.objDll.SelectRecord_usingExecuteDataset(_sql, param);
        drpBranch.DataTextField = "BranchName";
        drpBranch.DataValueField = "Id";
        drpBranch.DataBind();
    }
    public void loadBranch2(DropDownList drpBranch, string sessionName, string classid, string branchCode)
    {
        _sql = "Select BranchName,Id from BranchMaster Where ClassId=@ClassId and SessionName=@SessionName and BranchCode=@BranchCode";
        _sql = _sql + " Union ";
        _sql = _sql + " Select '<--Select-->','-1' order by Id";

        var param = new List<SqlParameter>();
        param.Add(new SqlParameter("@SessionName", sessionName));
        param.Add(new SqlParameter("@ClassId", classid));
        param.Add(new SqlParameter("@BranchCode", branchCode));
        drpBranch.DataSource = DLL.objDll.SelectRecord_usingExecuteDataset(_sql, param);
        drpBranch.DataTextField = "BranchName";
        drpBranch.DataValueField = "Id";
        drpBranch.DataBind();
    }
    public void loadAllBranch(DropDownList drpBranch)
    {
        _sql = "Select BranchId,BranchName from BranchTab";
        _sql = _sql + " Union ";
        _sql = _sql + " Select '0','All' order by BranchId";

        var param = new List<SqlParameter>();

        drpBranch.DataSource = DLL.objDll.SelectRecord_usingExecuteDataset(_sql);
        drpBranch.DataTextField = "BranchName";
        drpBranch.DataValueField = "BranchId";
        drpBranch.DataBind();
    }

    public void loadClasswithSectionandBranch(DropDownList drpClass, string sessionName)
    {
        var param = new List<SqlParameter>();
        param.Add(new SqlParameter("@SessionName", sessionName));
        param.Add(new SqlParameter("@BranchCode", branchCode()));
        drpClass.DataSource = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("USP_GetClasswithSectionandBranch", param);
        drpClass.DataTextField = "ClassName";
        drpClass.DataValueField = "ClassId";
        drpClass.DataBind();
    }

    public void loadShiftForStudent(DropDownList drpShift, string sessionName)
    {
        drpShift.DataSource = new DAL().GetStdShiftMaster(-1, sessionName);
        drpShift.DataTextField = "ShiftName";
        drpShift.DataValueField = "Id";
        drpShift.DataBind();
    }

    public void loadStream(DropDownList drpStream, string classid, string branchid, string sessionName)
    {
        _sql = "Select Stream,Id from StreamMaster where ClassId=@ClassId and BranchId=@BranchId and SessionName=@SessionName and BranchCode=@BranchCode";
        _sql = _sql + " Union ";
        _sql = _sql + " Select '<--Select-->','-1' order by Id";

        var param = new List<SqlParameter>();
        param.Add(new SqlParameter("@SessionName", sessionName));
        param.Add(new SqlParameter("@ClassId", classid));
        param.Add(new SqlParameter("@BranchId", branchid));
        param.Add(new SqlParameter("@BranchCode", branchCode()));
        drpStream.DataSource = DLL.objDll.SelectRecord_usingExecuteDataset(_sql, param);
        drpStream.DataTextField = "Stream";
        drpStream.DataValueField = "Id";
        drpStream.DataBind();
    }

    public void loadSection(DropDownList drpSection, string sessionName, string classid)
    {
        _sql = "Select SectionName,Id from SectionMaster Where ClassNameId=@ClassId and SessionName=@SessionName and BranchCode=@BranchCode";
        _sql = _sql + " Union ";
        _sql = _sql + " Select '<--Select-->','-1' order by Id";

        var param = new List<SqlParameter>();
        param.Add(new SqlParameter("@SessionName", sessionName));
        param.Add(new SqlParameter("@ClassId", classid));
        param.Add(new SqlParameter("@BranchCode", branchCode()));
        drpSection.DataSource = DLL.objDll.SelectRecord_usingExecuteDataset(_sql, param);
        drpSection.DataTextField = "SectionName";
        drpSection.DataValueField = "Id";
        drpSection.DataBind();
    }
    public void loadSection2(DropDownList drpSection, string sessionName, string classid, string branchCode)
    {
        _sql = "Select SectionName,Id from SectionMaster Where ClassNameId=@ClassId and SessionName=@SessionName and BranchCode=@BranchCode";
        _sql = _sql + " Union ";
        _sql = _sql + " Select '<--Select-->','-1' order by Id";

        var param = new List<SqlParameter>();
        param.Add(new SqlParameter("@SessionName", sessionName));
        param.Add(new SqlParameter("@ClassId", classid));
        param.Add(new SqlParameter("@BranchCode", branchCode));
        drpSection.DataSource = DLL.objDll.SelectRecord_usingExecuteDataset(_sql, param);
        drpSection.DataTextField = "SectionName";
        drpSection.DataValueField = "Id";
        drpSection.DataBind();
    }

    public void loadFeeGroup(DropDownList drpFeeGroup, string sessionName)
    {
        drpFeeGroup.Items.Clear();
        _sql = "Select FeeGroupName,Id from FeeGroupMaster Where SessionName=@SessionName and BranchCode=@BranchCode";
        _sql = _sql + " Union ";
        _sql = _sql + " Select '<--Select-->','-1' order by Id";

        var param = new List<SqlParameter>();
        param.Add(new SqlParameter("@SessionName", sessionName));
        param.Add(new SqlParameter("@BranchCode", branchCode()));
        drpFeeGroup.DataSource = DLL.objDll.SelectRecord_usingExecuteDataset(_sql, param);
        drpFeeGroup.DataTextField = "FeeGroupName";
        drpFeeGroup.DataValueField = "Id";
        drpFeeGroup.DataBind();
    }

    public void loadMedium(DropDownList drpMedium, string sessionName)
    {
        _sql = "Select Medium,Id from MediumMaster Where BranchCode=@BranchCode";
        _sql = _sql + " Union ";
        _sql = _sql + " Select '<--Select-->','-1' order by Id";

        var param = new List<SqlParameter>();
        param.Add(new SqlParameter("@SessionName", sessionName));
        param.Add(new SqlParameter("@BranchCode", branchCode()));
        drpMedium.DataSource = DLL.objDll.SelectRecord_usingExecuteDataset(_sql, param);
        drpMedium.DataTextField = "Medium";
        drpMedium.DataValueField = "Id";
        drpMedium.DataBind();
    }

    public void loadInsttalment(DropDownList drpInsttalment, string classId, string sessionName)
    {
        _sql = "Select MonthName,MonthId Id from MonthMaster where SessionName=@SessionName and (ClassId=@ClassId or ClassId is null) and BranchCode=@BranchCode";
        _sql = _sql + " Union ";
        _sql = _sql + " Select '<--Select-->','-1' order by Id";

        var param = new List<SqlParameter>();
        param.Add(new SqlParameter("@SessionName", sessionName));
        param.Add(new SqlParameter("@ClassId", classId));
        param.Add(new SqlParameter("@BranchCode", branchCode()));
        drpInsttalment.DataSource = DLL.objDll.SelectRecord_usingExecuteDataset(_sql, param);
        drpInsttalment.DataTextField = "MonthName";
        drpInsttalment.DataValueField = "Id";
        drpInsttalment.DataBind();
    }

    public void LoadSrnoInDropDownList(DropDownList ddlsrno, string classname, string sectionname, string branchname, string sessionname, string branchcode)
    {
        DataTable dt = null;

        List<SqlParameter> param = new List<SqlParameter>();
        param.Add(new SqlParameter("@ClassName", classname));
        param.Add(new SqlParameter("@SectionName", sectionname));
        param.Add(new SqlParameter("@BranchName", branchname));
        param.Add(new SqlParameter("@SessionName", sessionname));
        param.Add(new SqlParameter("@BranchCode", branchcode));

        DataSet ds = new DataSet();

        ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("Get_AllStudentRecord", param);

        if (ds != null)
        {
            DataView dv = new DataView(ds.Tables[0]);
            dt = dv.ToTable(true, "S.R.No.", "SRNOWITHNAME");

            dt.Columns["S.R.No."].ColumnName = "srno";
        }

        ddlsrno.DataSource = dt;
        ddlsrno.DataTextField = "SRNOWITHNAME";
        ddlsrno.DataValueField = "srno";
        ddlsrno.DataBind();
    }

    public void loadEval(DropDownList drpEval, string classid, string sessionname)
    {
        _sql = "Select GroupId from dt_ClassGroupMaster where SessionName='" + sessionname + "' and ClassId='" + classid + "' and BranchCode=" + branchCode() + "";
        var groupid = BAL.objBal.ReturnTag(_sql, "GroupId");
        var dt = new DataTable();
        dt.Columns.Add("Eval");
        dt.Rows.Clear();
        var dr = dt.NewRow();
        if (groupid == "G1")
        {
            dr["Eval"] = "MAY/JULY";
            dt.Rows.Add(dr);
            dr = dt.NewRow();

            dr["Eval"] = "AUG";
            dt.Rows.Add(dr);
            dr = dt.NewRow();

            dr["Eval"] = "SEPT.";
            dt.Rows.Add(dr);
            dr = dt.NewRow();

            dr["Eval"] = "DEC";
            dt.Rows.Add(dr);
            dr = dt.NewRow();

            dr["Eval"] = "JAN";
            dt.Rows.Add(dr);
            dr = dt.NewRow();

            dr["Eval"] = "FEB";
            dt.Rows.Add(dr);
        }
        else if (groupid == "G2")
        {
            dr["Eval"] = "MAY/JULY";
            dt.Rows.Add(dr);
            dr = dt.NewRow();

            dr["Eval"] = "AUG";
            dt.Rows.Add(dr);
            dr = dt.NewRow();

            dr["Eval"] = "SEPT.";
            dt.Rows.Add(dr);
            dr = dt.NewRow();

            dr["Eval"] = "DEC";
            dt.Rows.Add(dr);
            dr = dt.NewRow();

            dr["Eval"] = "JAN";
            dt.Rows.Add(dr);
            dr = dt.NewRow();

            dr["Eval"] = "FEB";
            dt.Rows.Add(dr);
        }
        else if (groupid == "G3")
        {
            dr["Eval"] = "EVALUATION1";
            dt.Rows.Add(dr);
            dr = dt.NewRow();

            dr["Eval"] = "EVALUATION2";
            dt.Rows.Add(dr);
            dr = dt.NewRow();

            dr["Eval"] = "EVALUATION3";
            dt.Rows.Add(dr);
        }
        else if (groupid == "G4")
        {
            dr["Eval"] = "FA1";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["Eval"] = "FA2";
            dt.Rows.Add(dr);
            dr = dt.NewRow();

            dr["Eval"] = "SA1";
            dt.Rows.Add(dr);
            dr = dt.NewRow();

            dr["Eval"] = "FA3";
            dt.Rows.Add(dr);
            dr = dt.NewRow();

            dr["Eval"] = "FA4";
            dt.Rows.Add(dr);
            dr = dt.NewRow();

            dr["Eval"] = "SA2";
            dt.Rows.Add(dr);
        }
        else if (groupid == "G5")
        {
            dr["Eval"] = "FA1";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["Eval"] = "FA2";
            dt.Rows.Add(dr);
            dr = dt.NewRow();

            dr["Eval"] = "SA1";
            dt.Rows.Add(dr);
            dr = dt.NewRow();

            dr["Eval"] = "FA3";
            dt.Rows.Add(dr);
            dr = dt.NewRow();

            dr["Eval"] = "FA4";
            dt.Rows.Add(dr);
            dr = dt.NewRow();

            dr["Eval"] = "SA2";
            dt.Rows.Add(dr);
        }
        else if (groupid == "G6")
        {
            dr["Eval"] = "FA1";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["Eval"] = "FA2";
            dt.Rows.Add(dr);
            dr = dt.NewRow();

            dr["Eval"] = "SA1";
            dt.Rows.Add(dr);
            dr = dt.NewRow();

            dr["Eval"] = "FA3";
            dt.Rows.Add(dr);
            dr = dt.NewRow();

            dr["Eval"] = "FA4";
            dt.Rows.Add(dr);
            dr = dt.NewRow();

            dr["Eval"] = "SA2";
            dt.Rows.Add(dr);
        }
        else if (groupid == "G7")
        {
            dr["Eval"] = "FA1";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["Eval"] = "FA2";
            dt.Rows.Add(dr);
            dr = dt.NewRow();

            dr["Eval"] = "SA1";
            dt.Rows.Add(dr);
            dr = dt.NewRow();

            dr["Eval"] = "FA3";
            dt.Rows.Add(dr);
            dr = dt.NewRow();

            dr["Eval"] = "FA4";
            dt.Rows.Add(dr);
            dr = dt.NewRow();

            dr["Eval"] = "SA2";
            dt.Rows.Add(dr);
        }
        else
        {
            BAL.objBal.MessageBoxforUpdatePanel("Please, Add Class in their related Group!", drpEval);
        }

        if (dt.Rows.Count > 0)
        {
            drpEval.DataSource = dt;
            drpEval.DataTextField = "Eval";
            drpEval.DataValueField = "Eval";
            drpEval.DataBind();
        }
    }




    public bool checkUrl(string AbsoluteUri, string loginid, string logintype, string isPrintPage, string checkRedirectpath)
    {
        if (isPrintPage == null)
        {
            isPrintPage = string.Empty;
        }

        if (checkRedirectpath == null)
        {
            checkRedirectpath = string.Empty;
        }

        var getpagename = AbsoluteUri.Split('/');

        Int16 istrue = 0;
        Int16 count = 0;

        foreach (var str in getpagename)
        {
            if (str.ToUpper() == "COMMON")
            {
                istrue = 1;
                break;
            }
            else
            {
                count++;
            }
        }

        var url = "";

        if (isPrintPage != string.Empty)
        {
            return true;
        }
        else
        {
            if (istrue == 1)
            {
                if (checkRedirectpath == string.Empty)
                {
                    for (int i = count; i < getpagename.Length; i++)
                    {
                        if (url == string.Empty)
                        {
                            url = getpagename[i];
                        }
                        else
                        {
                            url = url + "/" + getpagename[i];
                        }
                    }
                }
                else
                {
                    for (int i = count; i < getpagename.Length - 1; i++)
                    {
                        if (url == string.Empty)
                        {
                            url = getpagename[i];
                        }
                        else
                        {
                            url = url + "/" + getpagename[i];
                        }
                    }

                    url = url + "/" + checkRedirectpath + ".aspx";
                }

                return checkUrlPermissionbypassingDirecturl(url, loginid, logintype);
            }
            else
            {
                if (checkRedirectpath == string.Empty)
                {
                    url = getpagename[getpagename.Length - 1].ToString();

                    return checkUrlPermission(url, loginid, logintype);
                }
                else
                {
                    url = checkRedirectpath + ".aspx";

                    return checkUrlPermission(url, loginid, logintype);
                }

            }
        }
    }

    public bool checkUrlPermission(string url, string loginid, string logintype)
    {

        var param = new List<SqlParameter>();

        param.Add(new SqlParameter("@URL", url));
        param.Add(new SqlParameter("@Loginid", loginid));
        param.Add(new SqlParameter("@Logintype", logintype));

        var isPermission = DLL.objDll.Sp_SelectRecord_usingExecuteScalar("checkUrlAuthorizationProc", param);

        if (isPermission != null)
        {
            if (isPermission.ToString() != string.Empty)
            {
                if (isPermission.ToString() == "Y")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }
        else
        {
            return false;
        }


    }

    public bool checkUrlPermissionbypassingDirecturl(string url, string loginid, string logintype)
    {


        var param = new List<SqlParameter>();

        param.Add(new SqlParameter("@URL", url));
        param.Add(new SqlParameter("@Loginid", loginid));
        param.Add(new SqlParameter("@Logintype", logintype));

        var isPermission = DLL.objDll.Sp_SelectRecord_usingExecuteScalar("checkUrlAuthorizationProc", param);

        if (isPermission != null)
        {
            if (isPermission.ToString() != string.Empty)
            {
                if (isPermission.ToString() == "Y")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }
        else
        {
            return false;
        }
    }
    public DataTable ReadExcel(string fileName, string fileExt, Control ctrl, string sheetName)
    {
        try
        {
            var conn = string.Empty;
            var dtexcel = new DataTable();
            if (fileExt.CompareTo(".xls") == 0)
            {
                conn = @"provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties='Excel 8.0;HRD=Yes;IMEX=1';"; //for below excel 2007 
            }
            else
            {
                conn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties='Excel 12.0;HDR=NO';"; //for above excel 2007 
            }
            using (var con = new OleDbConnection(conn))
            {
                try
                {
                    OleDbCommand cmd = new OleDbCommand(@"select * from [" + sheetName + "] where F1<>'Id'", con);
                    //OleDbCommand cmd = new OleDbCommand(@"select * from [" + sheetName + "]", con);
                    OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                    adapter.Fill(dtexcel);
                    con.Close();
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(ctrl, GetType(), "alert", "window.alert('" + ex.Message + "')", true);
                }
            }
            return dtexcel;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public DataTable ReadExcel(string fileName, string fileExt, Control ctrl)
    {
        var conn = string.Empty;
        var dtexcel = new DataTable();
        if (fileExt.CompareTo(".xls") == 0)
            conn = @"provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties='Excel 8.0;HRD=Yes;IMEX=1';"; //for below excel 2007  
        else
            conn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties='Excel 12.0;HDR=NO';"; //for above excel 2007  
        using (var con = new OleDbConnection(conn))
        {
            try
            {
                //Get the name of First Sheet
                con.Open();
                DataTable dtExcelSchema;
                dtExcelSchema = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                var SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                con.Close();

                con.Open();
                //Read Data from First Sheet
                var oleAdpt = new OleDbDataAdapter("select * from [" + SheetName + "]", con); //here we read data from sheet1  
                oleAdpt.Fill(dtexcel); //fill excel data into dataTable  
                con.Close();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(ctrl, GetType(), "alert", "window.alert('" + ex.Message + "')", true);
            }
        }
        return dtexcel;
    }

    public DataSet loadChequeBounceFineAmount(string srno)
    {
        var param = new List<SqlParameter>();
        param.Add(new SqlParameter("@Srno", srno));

        var fineAmount = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("Get_ChequeBounceFineAmount_Proc", param);

        return fineAmount;
    }

    public string loadCurrentSessionStartDate(string currentsession)
    {
        _sql = "Select ISNULL(FromDate,'') FromDate from SessionMaster where SessionName=@SessionName and BranchCode=@BranchCode";

        var param = new List<SqlParameter>();
        param.Add(new SqlParameter("@SessionName", currentsession));
        param.Add(new SqlParameter("@BranchCode", branchCode()));

        return DLL.objDll.Sp_SelectRecord_usingExecuteReader(_sql, param);

    }

    public string loadCurrentSessionEndDate(string currentsession)
    {
        _sql = "Select ISNULL(ToDate,'') ToDate from SessionMaster where SessionName=@SessionName and BranchCode=@BranchCode";

        var param = new List<SqlParameter>();
        param.Add(new SqlParameter("@SessionName", currentsession));
        param.Add(new SqlParameter("@BranchCode", branchCode()));
        return DLL.objDll.Sp_SelectRecord_usingExecuteReader(_sql, param);
    }

    public void loadFeeHead(DropDownList drpFeeHead, string feeheadcategoryid, string sessionName)
    {
        _sql = "Select FeeName FeeHead,FeeId Id from FeeMaster where SessionName=@SessionName and BranchCode=@BranchCode";
        _sql = _sql + " Union ";
        _sql = _sql + " Select '<--Select-->','-1' order by Id";

        var param = new List<SqlParameter>();
        param.Add(new SqlParameter("@SessionName", sessionName));
        param.Add(new SqlParameter("@BranchCode", branchCode()));
        drpFeeHead.DataSource = DLL.objDll.SelectRecord_usingExecuteDataset(_sql, param);
        drpFeeHead.DataTextField = "FeeHead";
        drpFeeHead.DataValueField = "Id";
        drpFeeHead.DataBind();
    }

    public void loadCtegory(DropDownList drpClass)
    {
        _sql = "Select CasteName,CasteId Id from CasteMaster where BranchCode=" + branchCode() + " and SessionName=" + Session["SessionName"] + "";
        _sql = _sql + " Union ";
        _sql = _sql + " Select '<--Select-->','-1' order by Id";

        var param = new List<SqlParameter>();
        drpClass.DataSource = DLL.objDll.SelectRecord_usingExecuteDataset(_sql, param);
        drpClass.DataTextField = "CasteName";
        drpClass.DataValueField = "Id";
        drpClass.DataBind();
    }

    public void loadFeeHeadCategory(DropDownList drpFeeHeadCategory, string sessionname)
    {
        _sql = "Select Distinct FeeHeadCategory,Id from FeeHeadCategoryMaster where SessionName='" + sessionname + "' BranchCode=" + branchCode() + "";
        _sql = _sql + " Union ";
        _sql = _sql + " Select '<--Select-->','-1' order by Id";

        var param = new List<SqlParameter>();
        drpFeeHeadCategory.DataSource = DLL.objDll.SelectRecord_usingExecuteDataset(_sql, param);
        drpFeeHeadCategory.DataTextField = "FeeHeadCategory";
        drpFeeHeadCategory.DataValueField = "Id";
        drpFeeHeadCategory.DataBind();
    }
}