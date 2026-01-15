using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Data;
using System.Data.SqlClient;

public partial class Administrator_Settexttransformation : Page
{
    Campus oo = new Campus();
    DLL dllobj = new DLL();
    Message msgobj = new Message();

    protected void Page_Load(object sender, EventArgs e)
    {
        Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file

        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        if (!IsPostBack)
        {
            loadData();
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        string msg;
        List<SqlParameter> param = new List<SqlParameter>();
        param.Add(new SqlParameter("@isDo", "Insert_Update"));
        param.Add(new SqlParameter("@value", RadioButtonList1.SelectedValue.ToString()));
        param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"]));
        param.Add(new SqlParameter("@LoginName", Session["LoginName"].ToString()));
        SqlParameter para = new SqlParameter("@Msg", "");
        para.Direction = ParameterDirection.Output;
        para.Size = 0x10;
        param.Add(para);
        try
        {
            msg = dllobj.Sp_Insert_Update_Delete_usingExecuteNonQuery("SetandGet_texttransformdata", param);
          

        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        msgobj.Noofnoncleartxt = 0;
        //oo.MessageBoxforUpdatePanel(msgobj.MessageType(msg,LinkButton1,msgobj), LinkButton1);
        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, msgobj.MessageType(msg, LinkButton1, msgobj), "S");       

        loadData();
    }
    protected void loadData()
    {
        object value;
        List<SqlParameter> param = new List<SqlParameter>();
        param.Add(new SqlParameter("@isDo", "Select"));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"]));
        SqlParameter para = new SqlParameter("@Msg", "");
        para.Direction = ParameterDirection.Output;
        para.Size = 0x10;
        param.Add(para);
        value=dllobj.Sp_SelectRecord_usingExecuteScalar("SetandGet_texttransformdata", param);
        if (value != DBNull.Value)
        {
            switch ((string)value)
            {
                case "U":
                    RadioButtonList1.SelectedIndex = 0;
                    break;
                case "L":
                    RadioButtonList1.SelectedIndex = 1;
                    break;
                case "C":
                    RadioButtonList1.SelectedIndex = 2;
                    break;
                case "T":
                    RadioButtonList1.SelectedIndex = 3;
                    break;
                default:
                    RadioButtonList1.SelectedIndex = 3;
                    break;
            }
        }
    }
}