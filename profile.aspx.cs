using System;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_profile : System.Web.UI.Page
{
    string sql = "";
    string branchcode = "", value = "", table = "", logintypeid = "0";
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null || Session["Logintype"] == null || Session["LoginId"]==null)
        {
            Response.Redirect("default.aspx");
        }
        //Campus camp = new Campus(); camp.LoadLoader(loader);
        if (!IsPostBack)
        {
            loadCountry(drpCountry);
            loadState(drpState, drpCountry);
            loadCity(drpCity, drpState);

            if (Session["Logintype"].ToString().Trim() == "Admin")
            {
                table = "NewAdminInformation";
                branchcode = "and nai.BranchCode=" + Session["BranchCode"].ToString() + "";
            }
            else if (Session["Logintype"].ToString().Trim() == "Staff")
            {
                branchcode = "";
                table = "GetAllStaffRecords_UDF(" + Session["BranchCode"].ToString() + ")";
            }

            sql = "Select LoginName,Pass from LoginTab where LoginId='" + Session["LoginId"].ToString() + "' and (IsActive=1 or IsActive is null)";
            string loginname = BAL.objBal.ReturnTag(sql, "LoginName");
            string pass = BAL.objBal.ReturnTag(sql, "Pass");

            sql = "Select Name,FatherName,ContactNo,Email,Address,sm.id stateid,cm.id cityid,com.id countryid,PhotoPath,DisplayName from " + table + " nai";
            sql = sql + " inner join StateMaster sm on sm.StateName=nai.State";
            sql = sql + " inner join CountryMaster com on com.Id=sm.CountryId";
            sql = sql + " inner join CityMaster cm on cm.CityName=nai.City and sm.Id=cm.StateId";
            sql = sql + " where UserId='" + loginname + "' and Password='" + pass + "' " + branchcode + "";

            txtName.Text = BAL.objBal.ReturnTag(sql, "Name");
            txtDisplay.Text = BAL.objBal.ReturnTag(sql, "DisplayName");
            txtFathersName.Text = BAL.objBal.ReturnTag(sql, "FatherName");
            txtContactNo.Text = BAL.objBal.ReturnTag(sql, "ContactNo");
            txtEmail.Text = BAL.objBal.ReturnTag(sql, "Email");
            txtAddress.Text = BAL.objBal.ReturnTag(sql, "Address");
            if (BAL.objBal.ReturnTag(sql, "PhotoPath") != string.Empty)
            {
                Session["photopath"] = imgAvatars.ImageUrl = BAL.objBal.ReturnTag(sql, "PhotoPath");
            }
            else
            {
                imgAvatars.ImageUrl = "~/img/user-pic/student-pic.png";
            }
            try
            {
                drpCountry.SelectedValue = BAL.objBal.ReturnTag(sql, "countryid");
                string sql1 = sql;
                loadState(drpState, drpCountry);
                drpState.SelectedValue = BAL.objBal.ReturnTag(sql1, "stateid");
                loadCity(drpCity, drpState);
                drpCity.SelectedValue = BAL.objBal.ReturnTag(sql1, "cityid");
            }
            catch
            {
            }
        }
    }

    private void loadCountry(DropDownList drp)
    {
        sql = "Select CountryName,Id from CountryMaster";
        BAL.objBal.FillDropDown_withValue(sql, drp, "CountryName", "id");
    }

    private void loadState(DropDownList drp, DropDownList drpValue)
    {
        sql = "Select StateName,Id from StateMaster where CountryId='" + drpValue.SelectedValue.ToString() + "'";
        BAL.objBal.FillDropDown_withValue(sql, drp, "StateName", "id");
    }

    private void loadCity(DropDownList drp, DropDownList drpValue)
    {
        sql = "Select CityName,id from CityMaster where StateId='" + drpValue.SelectedValue.ToString() + "'";
        BAL.objBal.FillDropDown_withValue(sql, drp, "CityName", "id");
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        string matchingcolumn = "";
        string photopath = "~/Uploads/AdminPhoto/";
        string password = "";

        sql = "Select LoginName,Pass from LoginTab where LoginId='" + Session["LoginId"].ToString() + "' and (IsActive=1 or IsActive is null)";
        string loginname = BAL.objBal.ReturnTag(sql, "LoginName");
        string pass = BAL.objBal.ReturnTag(sql, "Pass");

        if (Session["Logintype"].ToString().Trim() == "Admin")
        {
            table = "NewAdminInformation";
            matchingcolumn = "UserId";
            photopath = "~/Uploads/AdminPhoto/";
            password = "and Password='" + pass + "'";
        }
        else if (Session["Logintype"].ToString().Trim() == "Staff")
        {
            table = "EmpGeneralDetail";
            matchingcolumn = "ECode";
            photopath = "~/Uploads/StaffPhoto/";
        }

       
        int count = 0;
        string msg = "Sorry, Photo not updated successfully!";

        string filePath = photopath;
        string fileName = "";

        string base64std = hdUserPic.Value;
        if (base64std != string.Empty)
        {
            fileName = loginname + ".jpg";

            filePath = filePath + fileName;

            using (FileStream fs = new FileStream(Server.MapPath(filePath), FileMode.Create))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    byte[] data = Convert.FromBase64String(base64std);
                    bw.Write(data);
                    bw.Close();
                }
            }

            try
            {
                sql = "Update " + table + " Set PhotoPath='" + filePath + "' , PhotoName='" + fileName + "'";
                sql = sql + " where " + matchingcolumn + "='" + loginname + "' " + password + "";
                count = BAL.objBal.Insert_Update_Delete1(sql);

                Session["ImageUrl"] = filePath;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }            

            if (count > 0)
            {
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Photo uploaded Successfully.", "S");

                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "window.location='" + BLL.GetSiteRoot() + "' + '/profile.aspx';", true);
            }
            else
            {
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, msg, "W");
            }
        }
        else
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Photo uploaded Successfully.", "S");
        }

        //if (avatarUpload.HasFile)
        //{
        //    //string photoname = avatarUpload.FileName;
        //    //try
        //    //{
        //    //    string getextention = Path.GetExtension(avatarUpload.FileName);
        //    //    avatarUpload.SaveAs(Server.MapPath(@photopath + loginname + getextention));

        //    //    sql = "Update " + table + " Set PhotoPath='" + photopath + loginname + getextention + "' , PhotoName='" + loginname + getextention + "'";
        //    //    sql = sql + " where " + matchingcolumn + "='" + loginname + "' " + password + "";
        //    //    count = BAL.objBal.Insert_Update_Delete1(sql);

        //    //    Session["ImageUrl"] = photopath + loginname + getextention;
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    msg = ex.Message;
        //    //}

           
        //}
        //else
        //{
        //    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Photo uploaded Successfully.", "S");
        //}
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        string matchingcolumn = "";
        string password = "";

        sql = "Select LoginName,Pass from LoginTab where LoginId='" + Session["LoginId"].ToString() + "' and (IsActive=1 or IsActive is null)";
        string loginname = BAL.objBal.ReturnTag(sql, "LoginName");
        string pass = BAL.objBal.ReturnTag(sql, "Pass");

        if (Session["Logintype"].ToString().Trim() == "Admin")
        {
            table = "NewAdminInformation";
            matchingcolumn = "UserId";
            password = "and Password='" + pass + "'";
        }
        else if (Session["Logintype"].ToString().Trim() == "Staff")
        {
            table = "EmpGeneralDetail";
            matchingcolumn = "ECode";
        }


        int count = 0;
        string msg = "Sorry, Photo not deleted successfully!";
        try
        {
            string path = Session["photopath"].ToString();
            Microsoft.VisualBasic.FileIO.FileSystem.DeleteFile(Server.MapPath(path), Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs, Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin);

            sql = "Update " + table + " Set PhotoPath='" + string.Empty + "' , PhotoName='" + string.Empty + "'";
            sql = sql + " where " + matchingcolumn + "='" + loginname + "' " + password + "";
            count = BAL.objBal.Insert_Update_Delete1(sql);

            Session["ImageUrl"] = "~/img/user-pic/user-pic.jpg";
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }

        if (count > 0)
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Photo deleted Successfully.", "S");

            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "window.location='" + Request.ApplicationPath + "/profile.aspx';", true);
        }
        else
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, msg, "W");
        }
        
    }
}