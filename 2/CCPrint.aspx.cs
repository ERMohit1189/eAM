using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace _2
{
    public partial class AdminCCPrint : Page
    {
        string sql = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string)Session["LoginName"] == "" || (string)Session["BranchCode"] == "" || (string)Session["SessionName"] == "" || Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
            {
                Response.Redirect("default.aspx");
            }
            BLL.BLLInstance.LoadCertificateHeader(header);
            if (!IsPostBack)
            {
                loadCC();
                setMargin();
            }
        }

        private void loadCC()
        {
            lblRefno.Text = Session["RecieptNo"].ToString();
            Label1.Text = lblRefno.Text;
            sql = "Select asr.SrNo, asr.Name StudentName,asr.FatherName,Class,passfail,year,sex,Convert(nvarchar(11),ISNULL(CCissuedate,RecordDate),106) CCissuedate,asr.InstituteRollNo RollNumber,asr.Dob from CCCollection cc inner join AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "','" + Session["BranchCode"].ToString() + "') asr on asr.SrNo=cc.srno and cc.BranchCode=asr.BranchCode where cc.RecieptNo='" + Session["RecieptNo"] + "' and cc.BranchCode=" + Session["BranchCode"] + " and cc.Cancel is null";
            lblstudentname.Text = BAL.objBal.ReturnTag(sql, "StudentName") + " (" + BAL.objBal.ReturnTag(sql, "srno") + ")";
            lblfathername.Text = BAL.objBal.ReturnTag(sql, "FatherName");
            lblclassname.Text = BAL.objBal.ReturnTag(sql, "Class");
            lblstatus.Text = BAL.objBal.ReturnTag(sql, "passfail").ToUpper();
            if (lblstatus.Text == "essentialrepeat".ToUpper())
            {
                lblstatus.Text = "Essential Repeat".ToUpper();
            }
            lblyear.Text = BAL.objBal.ReturnTag(sql, "year");
            lblDate.Text = BAL.objBal.ReturnTag(sql, "CCissuedate");



            if (BAL.objBal.ReturnTag(sql, "RollNumber") == "" || BAL.objBal.ReturnTag(sql, "RollNumber") == "0")
            {
                lblRollNotxt.Visible = false;
            }
            else
            {
                lbl_rollnumber.Text = BAL.objBal.ReturnTag(sql, "RollNumber");
                lblRollNotxt.Visible = true;
            }


            lbldob.Text = BAL.objBal.ReturnTag(sql, "Dob");
            string genders = BAL.objBal.ReturnTag(sql, "sex").Trim().ToLower();
            if (genders == "female")
            {
                lblSonordauter.Text = "daughter";
                lblheshe.Text = "She";
                lblheshe1.Text = "She";
                lblheher.Text = "her";
                lblrecordsdob.Text = "her";
            }
            else if (genders == "male")
            {
                lblSonordauter.Text = "son";
                lblheshe.Text = "He";
                lblheshe1.Text = "He";
                lblheher.Text = "his";
                lblrecordsdob.Text = "his";
            }
            else
            {
                lblSonordauter.Text = "son/daughter";
                lblheshe.Text = "He/She";
                lblheher.Text = "his/her";
                lblheshe1.Text = "He/She";
                lblrecordsdob.Text = "his/her";
            }

        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            divExport2.Attributes.Add("class", "col-sm-12 no-padding print-row ");
            PrintHelper_New.ctrl = divExport;
            ScriptManager.RegisterClientScriptBlock(LinkButton1, GetType(), "onclick", "var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}", true);
        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            divExport2.Attributes.Add("class", "col-sm-12 no-padding print-row  hearder-marg");
            PrintHelper_New.ctrl = divExport2;
            ScriptManager.RegisterClientScriptBlock(LinkButton3, GetType(), "onclick", "var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}", true);
        }

        public Tuple<string, string, string, string, string> getDocSetting()
        {
            string mrgTop = "0px", mrgBott = "0px", mrgLeft = "0px", mrgRight = "0px";
            string font = "12px";

            try
            {
                DataSet ds = new DataSet();
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@QueryFor", "S"));
                param.Add(new SqlParameter("@DocCategory", "Certificate"));
                ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("DocSetting_Proc", param);
                if (ds != null)
                {
                    DataTable dt = new DataTable();
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        //drpFontsize.SelectedValue = dt.Rows[0]["FontSize"].ToString();
                        font = dt.Rows[0]["FontSize"] + "px";

                        mrgTop = (Convert.ToDouble(dt.Rows[0]["MarginTop"].ToString()) * 96) + "px";
                        //margin. = mrgTop;
                        mrgBott = (Convert.ToDouble(dt.Rows[0]["MarginBottom"].ToString()) * 96) + "px";
                        //margin.Add(mrgBott);
                        mrgLeft = (Convert.ToDouble(dt.Rows[0]["MarginLeft"].ToString()) * 96) + "px";
                        //margin.Add(mrgLeft);
                        mrgRight = (Convert.ToDouble(dt.Rows[0]["MarginRight"].ToString()) * 96) + "px";
                        //margin.Add(mrgRight);
                    }
                }
            }
            catch
            {
                // ignored
            }

            return new Tuple<string, string, string, string, string>(mrgTop, mrgRight, mrgBott, mrgLeft, font);
        }

        public void setMargin()
        {
            divExport2.Style.Add("margin-top", getDocSetting().Item1);
            divExport2.Style.Add("margin-right", getDocSetting().Item2);
            divExport2.Style.Add("margin-bottom", getDocSetting().Item3);
            divExport2.Style.Add("margin-left", getDocSetting().Item4);
            ScriptManager.RegisterStartupScript(Page, GetType(), "font", "setFont('" + getDocSetting().Item5 + "');", true);
        }
    }
}