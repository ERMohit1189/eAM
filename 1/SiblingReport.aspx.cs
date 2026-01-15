using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _1
{
    public partial class AdminListofallsiblings : Page
    {
        private readonly Campus _oo;
        private string _sql = String.Empty;
        public AdminListofallsiblings()
        {
            _oo = new Campus();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
            {
                Response.Redirect("default.aspx");
            }
            Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file


            if (!IsPostBack)
            {
                BLL obj = new BLL();
                obj.LoadHeader("Receipt", header1);
                LoadClass();
                Row1.Visible = false;
                Row2.Visible = false;
                AllSiblings.Visible = false;
                StudentWiseNO.Visible = false;
                AllSiblings.Visible = true;
            }
        }

        public void LoadClass()
        {
            _sql = "Select ClassName,Id from ClassMaster where Sessionname='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " order by CidOrder";
            _oo.FillDropDown_withValue(_sql, drpClass, "ClassName", "Id");
            drpClass.Items.Insert(0, new ListItem("All", "All"));
        }

        public void DisplayAllStudentrecord()
        {
            if (drpClass.SelectedIndex == 0)
            {
                _sql = "Select distinct ClassName+' '+Case When IsDisplay=1 then BranchName else '' end+' '+SectionName class, asr.CombineClassName,sr.Id,sr.GroupId,SectionName,Card,Medium as Medium,ClassName,";
                _sql = _sql + "    convert(nvarchar,DateOfAdmiission,106) as DateOfAdmiission,FatherName,MotherName,";
                _sql = _sql + "    Name,asr.StEnRCode as StEnRCode,asr.srno  as srno,";
                _sql = _sql + "    IsNULL(TransportRequired,'No') as TransportRequired,UPPER(sr.SiblingRelation) SiblingRelation,format(sr.RecordDate,'dd-MMM-yyyy hh:mm:ss tt') reDate, sr.LoginName ";
                _sql = _sql + "    from (Select Distinct GroupId, asr.BranchCode, asr.CombineClassName,asr.SessionName from AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") as asr";
                _sql = _sql + "    inner join SiblingRecord sr on sr.Srno=asr.SrNo and asr.BranchCode=sr.BranchCode and asr.SessionName=sr.SessionName ) as T2";
                _sql = _sql + "    inner join SiblingRecord sr on T2.GroupId=sr.GroupId and T2.BranchCode=sr.BranchCode and T2.SessionName=sr.SessionName";
                _sql = _sql + "    inner join AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") as asr on sr.Srno=asr.SrNo";
                _sql = _sql + "    and sr.Withdrwal is null and sr.BranchCode=" + Session["BranchCode"] + "";
                _sql = _sql + "    Order by GroupId Desc";

                GrdAll.DataSource = _oo.GridFill(_sql);
                GrdAll.DataBind();
            }
            else
            {
                _sql = "Select distinct ClassName+' '+Case When IsDisplay=1 then BranchName else '' end+' '+SectionName class, asr.CombineClassName,sr.Id,sr.GroupId,SectionName,Card,Medium as Medium,ClassName,";
                _sql = _sql + "    convert(nvarchar,DateOfAdmiission,106) as DateOfAdmiission,FatherName,MotherName,";
                _sql = _sql + "    Name,asr.StEnRCode as StEnRCode,asr.srno  as srno,";
                _sql = _sql + "    IsNULL(TransportRequired,'No') as TransportRequired,UPPER(sr.SiblingRelation) SiblingRelation,format(sr.RecordDate,'dd-MMM-yyyy hh:mm:ss tt') reDate, sr.LoginName ";
                _sql = _sql + "    from (Select Distinct GroupId, asr.BranchCode, asr.CombineClassName,asr.SessionName from AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") as asr";
                _sql = _sql + "    inner join SiblingRecord sr on sr.Srno=asr.SrNo and asr.BranchCode=sr.BranchCode and asr.SessionName=sr.SessionName and asr.ClassName='" + drpClass.SelectedItem.Text + "') as T2";
                _sql = _sql + "    inner join SiblingRecord sr on T2.GroupId=sr.GroupId  and T2.BranchCode=sr.BranchCode and T2.SessionName=sr.SessionName";
                _sql = _sql + "    inner join AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") as asr on sr.Srno=asr.SrNo";
                _sql = _sql + "    and sr.Withdrwal is null and sr.BranchCode=" + Session["BranchCode"] + "";
                _sql = _sql + "    Order by GroupId Desc";

                GrdAll.DataSource = _oo.GridFill(_sql);
                GrdAll.DataBind();
            }

            if (GrdAll.Rows.Count > 0)
            {
                header.Visible = true;
                title.Visible = true;
                divExportBtn.Visible = true;
                
            }
            else
            {
                header.Visible = false;
                title.Visible = false;
                divExportBtn.Visible = false;
            }
        }

        public void MergeRows(GridView gridView)
        {
            for (int rowIndex = gridView.Rows.Count - 2; rowIndex >= 0; rowIndex--)
            {
                GridViewRow row = gridView.Rows[rowIndex];
                GridViewRow previousRow = gridView.Rows[rowIndex + 1];
                for (int i = 0; i < row.Cells.Count - 6; i++)
                {
                    if (((Label)(row.Cells[i].Controls[1])).Text == ((Label)(previousRow.Cells[i].Controls[1])).Text)
                    {
                        row.Cells[i].RowSpan = previousRow.Cells[i].RowSpan < 2 ? 2 : previousRow.Cells[i].RowSpan + 1;
                        previousRow.Cells[i].Visible = false;
                    }
                }
            }
            for (int i = 0; i < gridView.Rows.Count; i++)
            {
                int span = gridView.Rows[i].Cells[1].RowSpan;
                gridView.Rows[i].Cells[1].Text = (span - 1).ToString();
            }
        }
        protected void GridView2_PreRender(object sender, EventArgs e)
        {
            MergeRows(GridView2);
        }
        protected void GrdAll_PreRender(object sender, EventArgs e)
        {
            MergeRows(GrdAll);
        }
        protected void ImageButton1_Click(object sender, EventArgs e)
        {
            BLL obj = new BLL();
            obj.LoadHeader("Receipt", header1);
            _oo.ExportToWord(Response, "ListofallrelatedSiblings.doc", divExport);
        }
        protected void ImageButton2_Click(object sender, EventArgs e)
        {
            BLL obj = new BLL();
            obj.LoadHeader("Receipt", header1);
            _oo.ExportDivToExcel(Response, "ListofallrelatedSiblings.xls", divExport);
        }

        protected void ImageButton3_Click(object sender, EventArgs e)
        {
            BLL obj = new BLL();
            obj.LoadHeader("Receipt", header1);
            _oo.ExporttoPdf(Response, "ListofallrelatedSiblings", divExport);
        }
        protected void ImageButton4_Click(object sender, EventArgs e)
        {
            PrintHelper_New.ctrl = abc;
            if (GrdAll.Rows.Count > 0)
            {
                GrdAll.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            ClientScript.RegisterStartupScript(GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            BLL obj = new BLL();
            obj.LoadHeader("Receipt", header1);
            Row1.Visible = false;
            Row2.Visible = false;
            header.Visible = false;
            title.Visible = false;
            if (RadioButtonList1.SelectedIndex == 0)
            {
                AllSiblings.Visible = false;
                StudentWiseNO.Visible = true;
                Label8.Text = txtStudentEnter.Text + " Sibling Records";
            }
            else
            {
                if (RadioButtonList1.SelectedIndex == 1)
                {
                    StudentWiseNO.Visible = false;
                    AllSiblings.Visible = true;
                    Label8.Text = drpClass.SelectedItem.Text + " Sibling Report";

                }
            }
        }
        protected void lnkShow_Click(object sender, EventArgs e)
        {
            BLL obj = new BLL();
            obj.LoadHeader("Receipt", header1);
            if (RadioButtonList1.SelectedIndex == 0)
            {
                Row1.Visible = false;
                Row2.Visible = true;
                DisplaySingleStudentrecord();
            }
            else
            {
                if (RadioButtonList1.SelectedIndex == 1)
                {
                    Row2.Visible = false;
                    Row1.Visible = true;
                    DisplayAllStudentrecord();

                }
            }
        }

        

        public void DisplaySingleStudentrecord()
        {
            var studentId = Request.Form[hfStudentId.UniqueID];
            if (string.IsNullOrEmpty(studentId))
            {
                studentId = txtStudentEnter.Text.Trim();
            }

            _sql = "Select GroupId from SiblingRecord where Srno='" + studentId + "'";
            string groupId = _oo.ReturnTag(_sql, "GroupId");
            _sql = "select   CombineClassName,GroupId,Card,Medium, convert(nvarchar, DateOfAdmiission, 106) as DateOfAdmiission ,SectionId,FatherName, Name, asr.StEnRCode,asr.srno as srno,UPPER(sr.SiblingRelation) SiblingRelation,format(sr.RecordDate,'dd-MMM-yyyy hh:mm:ss tt') reDate, sr.LoginName from AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") asr inner join SiblingRecord sr on sr.Srno = asr.SrNo and sr.BranchCode = asr.BranchCode   ";
            _sql = _sql + "   where  sr.GroupId='" + groupId + "'  and sr.Withdrwal is null";
            GridView2.DataSource = _oo.GridFill(_sql);
            GridView2.DataBind();
            

            if (GridView2.Rows.Count > 0)
            {
                header.Visible = true;
                title.Visible = true;
                divExportBtn.Visible = true;
            }
            else
            {
                header.Visible = false;
                title.Visible = false;
                divExportBtn.Visible = false;
            }

        }



        protected void txtStudentEnter_TextChanged(object sender, EventArgs e)
        {
            BLL obj = new BLL();
            obj.LoadHeader("Receipt", header1);
            if (RadioButtonList1.SelectedIndex == 0)
            {
                Row1.Visible = false;
                Row2.Visible = true;
                //loadStudentGrid();
                //loadfee();
                DisplaySingleStudentrecord();
            }
            else
            {
                if (RadioButtonList1.SelectedIndex == 1)
                {
                    Row2.Visible = false;
                    Row1.Visible = true;
                    DisplayAllStudentrecord();

                }
            }
        }

        public override void Dispose()
        {
            _oo.Dispose();
            _oo.Dispose();
        }
    }
}