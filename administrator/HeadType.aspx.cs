using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class admin_HeadType : Page
{
    public string MSG = "", SQL = "";
    public static int H01ID = 0;
    public DataTable dt = new DataTable();

    protected void Page_preinit(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("../default.aspx");
        }
        
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetHeadType();
        }
    }

    private void Reset()
    {
        txtHeadType.Text = string.Empty;
        txtHeadType0.Text = string.Empty;
        txtHeadTypeCode.Text = string.Empty;
        txtHeadTypeCode0.Text = string.Empty;
    }

    private void Validation(Control cntrl)
    {
        MSG = "";

        if (txtHeadType.Text.Trim() == string.Empty)
        {
            MSG += "Enter Head Type !" + "\\n";
        }

        if (MSG != string.Empty)
        {
            new Campus().MessageBoxforUpdatePanel(MSG, cntrl);
        }
        else
        {
            SetHeadType(cntrl);
        }
    }

    private void SetHeadType(Control cntrl)
    {
        BAL.clsHeadType obj = new BAL.clsHeadType();

        obj.HeadCode = txtHeadTypeCode.Text.Trim();
        obj.HeadType = txtHeadType.Text.Trim();
        obj.IsActive = Convert.ToInt32(rblIsActive.SelectedValue) == 1 ? 1 : 0;
        obj.HeadMode = rblHeadMode.SelectedValue;

        obj.SQL = SQL;

        MSG = new DAL().SetHeadType(obj);
        if (MSG == "")
        {
            MSG = SQL;
            Reset();
        }

        btnInsert.Visible = true;
        GetHeadType();

        BLL.BLLInstance.ShowMSG(MSG);
    }

    private void GetHeadType()
    {
        BAL.clsHeadType obj = new BAL.clsHeadType();

        obj.H01ID = -1;
        obj.HeadType = "";
        obj.IsActive = -1;
        obj.HeadMode = "-1";
        obj.SessionName = Session["SessionName"].ToString();

        dt = null;
        dt = new DAL().GetHeadType(obj);

        if (dt != null && dt.Rows.Count > 0)
        {
            dt = new BLL().GetSerialNo(ref dt, "SrNo");
            gvHeadType.DataSource = dt;
        }
        else
        {
            gvHeadType.DataSource = null;
        }
        gvHeadType.DataBind();
    }

    protected void btnInsert_Click(object sender, EventArgs e)
    {
        SQL = "I";
        Validation(btnInsert);
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        Reset();
    }

    protected void lbtnEdit_Click(object sender, EventArgs e)
    {
        GridViewRow Row = (GridViewRow)(sender as LinkButton).Parent.Parent;
        LinkButton btn = (LinkButton)sender;
        H01ID = Convert.ToInt16(btn.Text);
        int i = Row.RowIndex;

        txtHeadType0.Text = gvHeadType.Rows[i].Cells[1].Text.Trim();
        txtHeadTypeCode0.Text = gvHeadType.Rows[i].Cells[2].Text.Trim();
        rblHeadMode0.Items.FindByText(gvHeadType.Rows[i].Cells[3].Text.Trim()).Selected = true;
        rblIsActive0.Items.FindByText(((Label)(gvHeadType.Rows[i].FindControl("IsActive"))).Text).Selected = true;

        Panel1_ModalPopupExtender.Show();
    } 

    protected void lbtnDelete_Click(object sender, EventArgs e)
    {
        LinkButton lbtn = (LinkButton)sender;
        H01ID = Convert.ToInt16(lbtn.Text);
        mpeDelete.Show();
    }

    protected void btnYes_Click(object sender, EventArgs e)
    {
        DeleteRecord(btnYes);
        GetHeadType();
    }

    public void DeleteRecord(Control cntrl)
    {
        SQL = "D";
        BAL.clsHeadType obj =new BAL.clsHeadType();
        obj.SQL = SQL;
        obj.H01ID = H01ID;
        MSG = new DAL().SetHeadType(obj);

        if (MSG == string.Empty)
        {
            BLL.BLLInstance.ShowMSG(SQL);
        }
        else
         BLL.BLLInstance.ShowMSG(MSG);
    }

    protected void btnNo_Click(object sender, EventArgs e)
    {

    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        MSG = "";
        SQL = "U";

        if (txtHeadType0.Text.Trim() == string.Empty)
        {
            MSG += "Enter Head Type !" + "\\n";
        }

        if (!string.IsNullOrEmpty(MSG))
        {
            BLL.BLLInstance.ShowMSG(MSG);
        }
        else
        {
            BAL.clsHeadType obj = new BAL.clsHeadType();
            obj.SQL = SQL;

            obj.H01ID = H01ID;
            obj.HeadType = txtHeadType0.Text.Trim();
            obj.HeadMode = rblHeadMode0.SelectedValue;
            obj.HeadCode = txtHeadTypeCode0.Text.Trim(); 
            obj.IsActive = Convert.ToInt32(rblIsActive0.SelectedValue);

            MSG = new DAL().SetHeadType(obj);
            
            if (MSG == "")
            {
                MSG = SQL;
                Reset();
            }

            GetHeadType();

            BLL.BLLInstance.ShowMSG(MSG);
        }
    }

    protected void rblIsExam_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}