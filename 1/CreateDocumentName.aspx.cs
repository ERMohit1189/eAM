using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _1
{
    public partial class AdminFrmCreateDocumentName : Page
    {
        private readonly BAL.Set_DocumentName _obj = new BAL.Set_DocumentName();
        private readonly BAL.textBoxList _textBoxList = new BAL.textBoxList();
        //BAL.Get_Class obj1 = new BAL.Get_Class();
        private DataTable _dt;
        private readonly Campus _oo;
        private readonly Message _message = new Message();
        private SqlConnection _con;
        public AdminFrmCreateDocumentName()
        {
            _con = new SqlConnection();
            _oo = new Campus();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string) Session["LoginName"] == "" || (string) Session["BranchCode"] == "" || (string) Session["SessionName"] == "" || Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
            {
                Response.Redirect("default.aspx");
            }
            _con = _oo.dbGet_connection();
            Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file

            if (!IsPostBack)
            {
                Loadclass(Page);
                Get_DocumentName(Page);
            
            }
        }
        public void Loadclass(Control ctrl)
        {
            //string msg;
            //obj1.SessionName=Session["SessionName"].ToString();
            //obj1.BranchCode = Session["BranchCode"].ToString();
            //try
            //{
            //    List<DropDownList> drplist=new List<DropDownList>();
            //    drplist.Add(drpFromClass);
            //    drplist.Add(drpToClass);
            //    msg=new BLL().Get_ClassWithValue(drplist, obj1);
            //}
            //catch (Exception ex)
            //{
            //    msg=ex.Message;
            
            //}
            //oo.MessageBoxforUpdatePanel(msg, ctrl);

            //string sql = "Select ClassName,Id from ClassMaster where SessionName='"+Session["SessionName"].ToString()+"' and BranchCode='"+Session["BranchCode"].ToString()+"'";
            //oo.FillDropDown_withValue(sql, drpFromClass, "ClassName", "Id");
            //drpFromClass.Items.Insert(0,new ListItem("<--Select-->", "-1"));
            //oo.FillDropDown_withValue(sql, drpToClass, "ClassName", "Id");
            //drpToClass.Items.Insert(0,new ListItem("<--Select-->", "-1"));
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtDocumentName.Text.Trim() != string.Empty)
            {
                Set_DocumentName(btnSubmit);
                Get_DocumentName(btnSubmit);
            }
            else
            {
                _textBoxList.Noofnoncleartxt = 0;
                //oo.MessageBoxforUpdatePanel(message.MessageType("E", table1, textBoxList), );
                Campus camp = new Campus(); camp.msgbox(btnSubmit, msgbox, _message.MessageType("E", table1, _textBoxList), "A");
            }
        }
        protected void Set_DocumentName(Control ctrl)
        {
            string msg;
            try
            {
                _obj.Type = "I";
                _obj.Id = 0;
                _obj.DocumentType = txtDocumentName.Text.Trim();
                //obj.ClassId = Convert.ToInt16(drpFromClass.Items[i].Value.ToString());
                //obj.ClassName = "";
                _obj.Session = Session["SessionName"].ToString();
                _obj.BranchCode = Convert.ToInt16(Session["BranchCode"].ToString());
                _obj.LoginName = Session["LoginName"].ToString();
                msg = new DAL().Set_DocumentName(_obj);
                Campus camp = new Campus(); camp.msgbox(btnSubmit, msgbox, _message.MessageType(msg, table1, _textBoxList), "S");
            
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                Campus camp = new Campus(); camp.msgbox(btnSubmit, msgbox, _message.MessageType(msg, table1, _textBoxList), "W");
            }
            //oo.MessageBoxforUpdatePanel(, );
        }
        protected void Get_DocumentName(Control ctrl)
        {
            string ss = "select * from dt_CreateDocumentName where BranchCode=" + Session["BranchCode"] + "";
            grdDetails.DataSource = _oo.Fetchdata(ss);
            grdDetails.DataBind();
            if (grdDetails.Rows.Count>0)
            {
                for (int i = 0; i < grdDetails.Rows.Count; i++)
                {
                    Label lblid = (Label)grdDetails.Rows[i].FindControl("Label37");
                    LinkButton lnkDelete = (LinkButton)grdDetails.Rows[i].FindControl("lnkDelete");
                    string ss1 = "select id from StudentDocs where BranchCode=" + Session["BranchCode"] + " and DocId="+ lblid.Text.Trim() + "";
                    if (_oo.Duplicate(ss1))
                    {
                        lnkDelete.Enabled = false;
                        lnkDelete.Text = "<i class='fa fa-lock'></i>";
                    }

                }
            }
        }
        protected void Update_DocumentName(Control ctrl)
        {
            string msg;
            try
            {
                _obj.Type = "U";
                _obj.Id = Convert.ToInt16(Session["Id"].ToString());
                _obj.DocumentType = txtDocumentTypePanel.Text.Trim();
                //obj.ClassId = 0;
                //obj.ClassName = Session["ClassName"].ToString();
                _obj.Session = Session["SessionName"].ToString();
                _obj.BranchCode = Convert.ToInt16(Session["BranchCode"].ToString());
                _obj.LoginName = Session["LoginName"].ToString();
                msg = new DAL().Set_DocumentName(_obj);
                Campus camp = new Campus(); camp.msgbox(btnSubmit, msgbox, _message.MessageType(msg, table1, _textBoxList), "S");

            }
            catch (Exception ex)
            {
                msg = ex.Message;
                Campus camp = new Campus(); camp.msgbox(btnSubmit, msgbox, _message.MessageType(msg, table1, _textBoxList), "W");
            }

            //oo.MessageBoxforUpdatePanel(, btnSubmit);
      
            //Session["Id"] = "";
            //Session["ClassName"] = "";
        }
        //protected void drpFromClass_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    msg = "";
        //    try
        //    {
        //        Get_DocumentName(drpFromClass);
        //    }
        //    catch (Exception ex)
        //    {
        //        msg = ex.Message;
            
        //    }
        //    textBoxList.Noofnoncleartxt = 0;
        //    if (msg != string.Empty)
        //    {
        //        oo.MessageBoxforUpdatePanel(message.MessageType(msg, drpFromClass, textBoxList), drpFromClass);
        //    }

        //}
        protected void lnkEdit_Click(object sender, EventArgs e)
        {   
            LinkButton lnk = (LinkButton)sender;
            var row = (GridViewRow)lnk.NamingContainer;
            //Label className = (Label)row.FindControl("lblClassName");
            Label lblId = (Label)row.FindControl("Label36");
            Session["Id"] = lblId.Text;
            try
            {
                Label lblDocument = (Label)row.FindControl("lblDocument");
                txtDocumentTypePanel.Text = lblDocument.Text;
                Panel1_ModalPopupExtender.Show();
            }
            catch (Exception ex)
            {
                _textBoxList.Noofnoncleartxt = 0;
                //oo.MessageBoxforUpdatePanel(, );
                Campus camp = new Campus(); camp.msgbox(lnkEdit, msgbox, _message.MessageType(ex.Message, lnkEdit, _textBoxList), "W");
            }
        }
        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            lnkNo.Focus();
            LinkButton chk = (LinkButton)sender;
            Label lblId = (Label)chk.NamingContainer.FindControl("Label37");
            Session["Id"] = lblId.Text;
            Panel2_ModalPopupExtender.Show();
        }
        protected void lnkYes_Click(object sender, EventArgs e)
        {
            Delete_DocumentName(lnkDelete);
            Get_DocumentName(lnkUpdate);
        }
        protected void Delete_DocumentName(Control ctrl)
        {
            string msg;
            try
            {
                _obj.Type = "D";
                _obj.Id = Convert.ToInt16(Session["Id"].ToString());
                _obj.DocumentType = "";
                _obj.ClassId = 0;
                _obj.ClassName = "";
                _obj.Session = Session["SessionName"].ToString();
                _obj.BranchCode = Convert.ToInt16(Session["BranchCode"].ToString());
                _obj.LoginName = "";
                msg = new DAL().Set_DocumentName(_obj);
                Campus camp = new Campus(); camp.msgbox(btnSubmit, msgbox, _message.MessageType(msg, table1, _textBoxList), "S");

            }
            catch (Exception ex)
            {
                msg = ex.Message;
                Campus camp = new Campus(); camp.msgbox(btnSubmit, msgbox, _message.MessageType(msg, table1, _textBoxList), "W");
            }

            //oo.MessageBoxforUpdatePanel(, );

            Session["Id"] = "";
        }
        protected void lnkUpdate_Click(object sender, EventArgs e)
        {
            Update_DocumentName(lnkUpdate);
            Get_DocumentName(lnkUpdate);
        }

        public override void Dispose()
        {
            _oo.Dispose();
        }
    }
}