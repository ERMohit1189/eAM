using System;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _2
{
    public partial class AdminDayWiseCollection : Page
    {
        private SqlConnection con;
        private SqlConnection con1;
        private readonly Campus oo;
        private string sql;

        public AdminDayWiseCollection()
        {
            con = new SqlConnection();
            con1 = new SqlConnection();
            oo = new Campus();
            sql = string.Empty;
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
            {
                Response.Redirect("default.aspx");
            }

            con = oo.dbGet_connection();
            con1 = oo.dbGet_connection();
            Campus camp = new Campus(); camp.LoadLoader(loader);
            BLL.BLLInstance.LoadHeader("Report",header);
            if (!IsPostBack)
            {
                Panel1.Visible = false;
                abc.Visible = false;
            }

        }

        public void GridDisplay()
        {
            int i = 0;
            sql = "select ROW_NUMBER() OVER (ORDER BY Id ASC) AS SrNo,Id, className from ClassMaster  where id!=0  and SessionName='" + Session["SessionName"] + "' order by CIDOrder";
            GrdDisplay.DataSource = oo.GridFill(sql);
            GrdDisplay.DataBind();

            Panel1.Visible = true;
            ImageButton1.Visible = true;
            ImageButton2.Visible = true;
            ImageButton3.Visible = true;
            ImageButton4.Visible = true;

            Label5.Text = DropDownList1.SelectedItem.ToString();
            abc.Visible = true;
            for (i = 0; i <= GrdDisplay.Rows.Count - 1; i++)
            {
                Label Label2 = (Label)GrdDisplay.Rows[i].FindControl("Label2");

                FeesDepositSum(Label2.Text.Trim(), i);

            }
            GrandTotal();

            try
            {
                Perday1Sum();
                Perday2Sum();
                Perday3Sum();
                Perday4Sum();
                Perday5Sum();
                Perday6Sum();
                Perday7Sum();
                Perday8Sum();
                Perday9Sum();
                Perday10Sum();
                Perday11Sum();
                Perday12Sum();
                Perday13Sum();
                Perday14Sum();
                Perday15Sum();
                Perday16Sum();
                Perday17Sum();
                Perday18Sum();
                Perday19Sum();
                Perday20Sum();
                Perday21Sum();
                Perday22Sum();
                Perday23Sum();
                Perday24Sum();
                Perday25Sum();
                Perday26Sum();
                Perday27Sum();
                Perday28Sum();
                Perday29Sum();
                Perday30Sum();
                Perday31Sum();
            }
            catch (Exception)
            {
                // ignored
            }

            //Perday Sum Close
        }

        public void FeesDepositSum(string ClassName, int x)
        {

            Label lbl1 = (Label)GrdDisplay.Rows[x].FindControl("lbl1");
            Label lbl2 = (Label)GrdDisplay.Rows[x].FindControl("lbl2");
            Label lbl3 = (Label)GrdDisplay.Rows[x].FindControl("lbl3");
            Label lbl4 = (Label)GrdDisplay.Rows[x].FindControl("lbl4");
            Label lbl5 = (Label)GrdDisplay.Rows[x].FindControl("lbl5");
            Label lbl6 = (Label)GrdDisplay.Rows[x].FindControl("lbl6");
            Label lbl7 = (Label)GrdDisplay.Rows[x].FindControl("lbl7");
            Label lbl8 = (Label)GrdDisplay.Rows[x].FindControl("lbl8");
            Label lbl9 = (Label)GrdDisplay.Rows[x].FindControl("lbl9");
            Label lbl10 = (Label)GrdDisplay.Rows[x].FindControl("lbl10");
            Label lbl11 = (Label)GrdDisplay.Rows[x].FindControl("lbl11");
            Label lbl12 = (Label)GrdDisplay.Rows[x].FindControl("lbl12");
            Label lbl13 = (Label)GrdDisplay.Rows[x].FindControl("lbl13");
            Label lbl14 = (Label)GrdDisplay.Rows[x].FindControl("lbl14");
            Label lbl15 = (Label)GrdDisplay.Rows[x].FindControl("lbl15");
            Label lbl16 = (Label)GrdDisplay.Rows[x].FindControl("lbl16");
            Label lbl17 = (Label)GrdDisplay.Rows[x].FindControl("lbl17");
            Label lbl18 = (Label)GrdDisplay.Rows[x].FindControl("lbl18");
            Label lbl19 = (Label)GrdDisplay.Rows[x].FindControl("lbl19");
            Label lbl20 = (Label)GrdDisplay.Rows[x].FindControl("lbl20");
            Label lbl21 = (Label)GrdDisplay.Rows[x].FindControl("lbl21");
            Label lbl22 = (Label)GrdDisplay.Rows[x].FindControl("lbl22");
            Label lbl23 = (Label)GrdDisplay.Rows[x].FindControl("lbl23");
            Label lbl24 = (Label)GrdDisplay.Rows[x].FindControl("lbl24");
            Label lbl25 = (Label)GrdDisplay.Rows[x].FindControl("lbl25");
            Label lbl26 = (Label)GrdDisplay.Rows[x].FindControl("lbl26");
            Label lbl27 = (Label)GrdDisplay.Rows[x].FindControl("lbl27");
            Label lbl28 = (Label)GrdDisplay.Rows[x].FindControl("lbl28");
            Label lbl29 = (Label)GrdDisplay.Rows[x].FindControl("lbl29");
            Label lbl30 = (Label)GrdDisplay.Rows[x].FindControl("lbl30");
            Label lbl31 = (Label)GrdDisplay.Rows[x].FindControl("lbl31");
            Label lblTotalAmt = (Label)GrdDisplay.Rows[x].FindControl("lblTotalAmt");
            Label lblRegisterAmt = (Label)GrdDisplay.Rows[x].FindControl("lblRegisterAmt");


            lbl1.Text = PerDayDepositFees(1, DropDownList1.SelectedValue, ClassName);
            lbl2.Text = PerDayDepositFees(2, DropDownList1.SelectedValue, ClassName);
            lbl3.Text = PerDayDepositFees(3, DropDownList1.SelectedValue, ClassName);
            lbl4.Text = PerDayDepositFees(4, DropDownList1.SelectedValue, ClassName);
            lbl5.Text = PerDayDepositFees(5, DropDownList1.SelectedValue, ClassName);
            lbl6.Text = PerDayDepositFees(6, DropDownList1.SelectedValue, ClassName);
            lbl7.Text = PerDayDepositFees(7, DropDownList1.SelectedValue, ClassName);
            lbl8.Text = PerDayDepositFees(8, DropDownList1.SelectedValue, ClassName);
            lbl9.Text = PerDayDepositFees(9, DropDownList1.SelectedValue, ClassName);
            lbl10.Text = PerDayDepositFees(10, DropDownList1.SelectedValue, ClassName);
            lbl11.Text = PerDayDepositFees(11, DropDownList1.SelectedValue, ClassName);
            lbl12.Text = PerDayDepositFees(12, DropDownList1.SelectedValue, ClassName);
            lbl13.Text = PerDayDepositFees(13, DropDownList1.SelectedValue, ClassName);
            lbl14.Text = PerDayDepositFees(14, DropDownList1.SelectedValue, ClassName);
            lbl15.Text = PerDayDepositFees(15, DropDownList1.SelectedValue, ClassName);
            lbl16.Text = PerDayDepositFees(16, DropDownList1.SelectedValue, ClassName);
            lbl17.Text = PerDayDepositFees(17, DropDownList1.SelectedValue, ClassName);
            lbl18.Text = PerDayDepositFees(18, DropDownList1.SelectedValue, ClassName);
            lbl19.Text = PerDayDepositFees(19, DropDownList1.SelectedValue, ClassName);
            lbl20.Text = PerDayDepositFees(20, DropDownList1.SelectedValue, ClassName);
            lbl21.Text = PerDayDepositFees(21, DropDownList1.SelectedValue, ClassName);
            lbl22.Text = PerDayDepositFees(22, DropDownList1.SelectedValue, ClassName);
            lbl23.Text = PerDayDepositFees(23, DropDownList1.SelectedValue, ClassName);
            lbl24.Text = PerDayDepositFees(24, DropDownList1.SelectedValue, ClassName);
            lbl25.Text = PerDayDepositFees(25, DropDownList1.SelectedValue, ClassName);
            lbl26.Text = PerDayDepositFees(26, DropDownList1.SelectedValue, ClassName);
            lbl27.Text = PerDayDepositFees(27, DropDownList1.SelectedValue, ClassName);
            lbl28.Text = PerDayDepositFees(28, DropDownList1.SelectedValue, ClassName);
            lbl29.Text = PerDayDepositFees(29, DropDownList1.SelectedValue, ClassName);
            lbl30.Text = PerDayDepositFees(30, DropDownList1.SelectedValue, ClassName);
            lbl31.Text = PerDayDepositFees(31, DropDownList1.SelectedValue, ClassName);

            double sum = 0;

            try
            {
                sum = sum + Convert.ToDouble(lbl1.Text);
            }
            catch (Exception)
            {
                // ignored
            }

            try
            {
                sum = sum + Convert.ToDouble(lbl2.Text);
            }
            catch (Exception)
            {
                // ignored
            }

            try
            {
                sum = sum + Convert.ToDouble(lbl3.Text);
            }
            catch (Exception)
            {
                // ignored
            }

            try
            {
                sum = sum + Convert.ToDouble(lbl4.Text);
            }
            catch (Exception)
            {
                // ignored
            }

            try
            {
                sum = sum + Convert.ToDouble(lbl5.Text);
            }
            catch (Exception)
            {
                // ignored
            }

            try
            {
                sum = sum + Convert.ToDouble(lbl6.Text);
            }
            catch (Exception)
            {
                // ignored
            }
            try
            {
                sum = sum + Convert.ToDouble(lbl7.Text);
            }
            catch (Exception)
            {
                // ignored
            }
            try
            {
                sum = sum + Convert.ToDouble(lbl8.Text);
            }
            catch (Exception)
            {
                // ignored
            }
            try
            {
                sum = sum + Convert.ToDouble(lbl9.Text);
            }
            catch (Exception)
            {
                // ignored
            }
            try
            {
                sum = sum + Convert.ToDouble(lbl10.Text);
            }
            catch (Exception)
            {
                // ignored
            }
            try
            {
                sum = sum + Convert.ToDouble(lbl11.Text);
            }
            catch (Exception)
            {
                // ignored
            }
            try
            {
                sum = sum + Convert.ToDouble(lbl12.Text);
            }
            catch (Exception)
            {
                // ignored
            }
            try
            {
                sum = sum + Convert.ToDouble(lbl13.Text);
            }
            catch (Exception)
            {
                // ignored
            }
            try
            {
                sum = sum + Convert.ToDouble(lbl14.Text);
            }
            catch (Exception)
            {
                // ignored
            }
            try
            {
                sum = sum + Convert.ToDouble(lbl15.Text);
            }
            catch (Exception)
            {
                // ignored
            }
            try
            {
                sum = sum + Convert.ToDouble(lbl16.Text);
            }
            catch (Exception)
            {
                // ignored
            }
            try
            {
                sum = sum + Convert.ToDouble(lbl17.Text);
            }
            catch (Exception)
            {
                // ignored
            }
            try
            {
                sum = sum + Convert.ToDouble(lbl18.Text);
            }
            catch (Exception)
            {
                // ignored
            }
            try
            {
                sum = sum + Convert.ToDouble(lbl19.Text);
            }
            catch (Exception)
            {
                // ignored
            }
            try
            {
                sum = sum + Convert.ToDouble(lbl20.Text);
            }
            catch (Exception)
            {
                // ignored
            }
            try
            {
                sum = sum + Convert.ToDouble(lbl21.Text);
            }
            catch (Exception)
            {
                // ignored
            }
            try
            {
                sum = sum + Convert.ToDouble(lbl22.Text);
            }
            catch (Exception)
            {
                // ignored
            }
            try
            {
                sum = sum + Convert.ToDouble(lbl23.Text);
            }
            catch (Exception)
            {
                // ignored
            }
            try
            {
                sum = sum + Convert.ToDouble(lbl24.Text);
            }
            catch (Exception)
            {
                // ignored
            }
            try
            {
                sum = sum + Convert.ToDouble(lbl25.Text);
            }
            catch (Exception)
            {
                // ignored
            }
            try
            {
                sum = sum + Convert.ToDouble(lbl26.Text);
            }
            catch (Exception)
            {
                // ignored
            }
            try
            {
                sum = sum + Convert.ToDouble(lbl27.Text);
            }
            catch (Exception)
            {
                // ignored
            }
            try
            {
                sum = sum + Convert.ToDouble(lbl28.Text);
            }
            catch (Exception)
            {
                // ignored
            }
            try
            {
                sum = sum + Convert.ToDouble(lbl29.Text);
            }
            catch (Exception)
            {
                // ignored
            }
            try
            {
                sum = sum + Convert.ToDouble(lbl30.Text);
            }
            catch (Exception)
            {
                // ignored
            }
            try
            {
                sum = sum + Convert.ToDouble(lbl31.Text);
            }
            catch (Exception)
            {
                // ignored
            }
            lblTotalAmt.Text = sum.ToString(CultureInfo.InvariantCulture);
        }


        public void Perday1Sum()
        {
            int i;
            double sum = 0;
            for (i = 0; i <= GrdDisplay.Rows.Count - 1; i++)
            {
                Label lbl1 = (Label)GrdDisplay.Rows[i].FindControl("lbl1");
                try
                {
                    sum = sum + Convert.ToDouble(lbl1.Text.Trim());
                }
                catch (Exception)
                {
                    // ignored
                }
            }
            Label lbl1stCollection = (Label)GrdDisplay.FooterRow.FindControl("lbl1stCollection");
            lbl1stCollection.Text = sum.ToString(CultureInfo.InvariantCulture);
        }

        public void Perday2Sum()
        {
            int i;
            double sum = 0;
            for (i = 0; i <= GrdDisplay.Rows.Count - 1; i++)
            {
                Label lbl2 = (Label)GrdDisplay.Rows[i].FindControl("lbl2");
                try
                {
                    sum = sum + Convert.ToDouble(lbl2.Text.Trim());
                }
                catch (Exception)
                {
                    // ignored
                }
            }
            Label lbl2ndCollection = (Label)GrdDisplay.FooterRow.FindControl("lbl2ndCollection");
            lbl2ndCollection.Text = sum.ToString(CultureInfo.InvariantCulture);
        }



        public void Perday3Sum()
        {
            int i;
            double sum = 0;
            for (i = 0; i <= GrdDisplay.Rows.Count - 1; i++)
            {
                Label lbl3 = (Label)GrdDisplay.Rows[i].FindControl("lbl3");
                try
                {
                    sum = sum + Convert.ToDouble(lbl3.Text.Trim());
                }
                catch (Exception)
                {
                    // ignored
                }
            }
            Label lbl3rdCollection = (Label)GrdDisplay.FooterRow.FindControl("lbl3rdCollection");
            lbl3rdCollection.Text = sum.ToString(CultureInfo.InvariantCulture);
        }

        public void Perday4Sum()
        {
            int i;
            double sum = 0;
            for (i = 0; i <= GrdDisplay.Rows.Count - 1; i++)
            {
                Label lbl4 = (Label)GrdDisplay.Rows[i].FindControl("lbl4");
                try
                {
                    sum = sum + Convert.ToDouble(lbl4.Text.Trim());
                }
                catch (Exception)
                {
                    // ignored
                }
            }
            Label lbl4thCollection = (Label)GrdDisplay.FooterRow.FindControl("lbl4thCollection");
            lbl4thCollection.Text = sum.ToString(CultureInfo.InvariantCulture);
        }
        public void Perday5Sum()
        {
            int i;
            double sum = 0;
            for (i = 0; i <= GrdDisplay.Rows.Count - 1; i++)
            {
                Label lbl5 = (Label)GrdDisplay.Rows[i].FindControl("lbl5");
                try
                {
                    sum = sum + Convert.ToDouble(lbl5.Text.Trim());
                }
                catch (Exception)
                {
                    // ignored
                }
            }
            Label lbl5thCollection = (Label)GrdDisplay.FooterRow.FindControl("lbl5thCollection");
            lbl5thCollection.Text = sum.ToString(CultureInfo.InvariantCulture);
        }
        public void Perday6Sum()
        {
            int i;
            double sum = 0;
            for (i = 0; i <= GrdDisplay.Rows.Count - 1; i++)
            {
                Label lbl6 = (Label)GrdDisplay.Rows[i].FindControl("lbl6");
                try
                {
                    sum = sum + Convert.ToDouble(lbl6.Text.Trim());
                }
                catch (Exception)
                {
                    // ignored
                }
            }
            Label lbl6thCollection = (Label)GrdDisplay.FooterRow.FindControl("lbl6thCollection");
            lbl6thCollection.Text = sum.ToString(CultureInfo.InvariantCulture);
        }
        public void Perday7Sum()
        {
            int i;
            double sum = 0;
            for (i = 0; i <= GrdDisplay.Rows.Count - 1; i++)
            {
                Label lbl7 = (Label)GrdDisplay.Rows[i].FindControl("lbl7");
                try
                {
                    sum = sum + Convert.ToDouble(lbl7.Text.Trim());
                }
                catch (Exception)
                {
                    // ignored
                }
            }
            Label lbl7thCollection = (Label)GrdDisplay.FooterRow.FindControl("lbl7thCollection");
            lbl7thCollection.Text = sum.ToString(CultureInfo.InvariantCulture);
        }
        public void Perday8Sum()
        {
            int i;
            double sum = 0;
            for (i = 0; i <= GrdDisplay.Rows.Count - 1; i++)
            {
                Label lbl8 = (Label)GrdDisplay.Rows[i].FindControl("lbl8");
                try
                {
                    sum = sum + Convert.ToDouble(lbl8.Text.Trim());
                }
                catch (Exception)
                {
                    // ignored
                }
            }
            Label lbl8thCollection = (Label)GrdDisplay.FooterRow.FindControl("lbl8thCollection");
            lbl8thCollection.Text = sum.ToString(CultureInfo.InvariantCulture);
        }

        public void Perday9Sum()
        {
            int i;
            double sum = 0;
            for (i = 0; i <= GrdDisplay.Rows.Count - 1; i++)
            {
                Label lbl9 = (Label)GrdDisplay.Rows[i].FindControl("lbl9");
                try
                {
                    sum = sum + Convert.ToDouble(lbl9.Text.Trim());
                }
                catch (Exception)
                {
                    // ignored
                }
            }
            Label lbl9thCollection = (Label)GrdDisplay.FooterRow.FindControl("lbl9thCollection");
            lbl9thCollection.Text = sum.ToString(CultureInfo.InvariantCulture);
        }

        public void Perday10Sum()
        {
            int i;
            double sum = 0;
            for (i = 0; i <= GrdDisplay.Rows.Count - 1; i++)
            {
                Label lbl10 = (Label)GrdDisplay.Rows[i].FindControl("lbl10");
                try
                {
                    sum = sum + Convert.ToDouble(lbl10.Text.Trim());
                }
                catch (Exception)
                {
                    // ignored
                }
            }
            Label lbl10thCollection = (Label)GrdDisplay.FooterRow.FindControl("lbl10thCollection");
            lbl10thCollection.Text = sum.ToString(CultureInfo.InvariantCulture);
        }

        public void Perday11Sum()
        {
            int i;
            double sum = 0;
            for (i = 0; i <= GrdDisplay.Rows.Count - 1; i++)
            {
                Label lbl11 = (Label)GrdDisplay.Rows[i].FindControl("lbl11");
                try
                {
                    sum = sum + Convert.ToDouble(lbl11.Text.Trim());
                }
                catch (Exception)
                {
                    // ignored
                }
            }
            Label lbl11thCollection = (Label)GrdDisplay.FooterRow.FindControl("lbl11thCollection");
            lbl11thCollection.Text = sum.ToString(CultureInfo.InvariantCulture);
        }

        public void Perday12Sum()
        {
            int i;
            double sum = 0;
            for (i = 0; i <= GrdDisplay.Rows.Count - 1; i++)
            {
                Label lbl12 = (Label)GrdDisplay.Rows[i].FindControl("lbl12");
                try
                {
                    sum = sum + Convert.ToDouble(lbl12.Text.Trim());
                }
                catch (Exception)
                {
                    // ignored
                }
            }
            Label lbl12thCollection = (Label)GrdDisplay.FooterRow.FindControl("lbl12thCollection");
            lbl12thCollection.Text = sum.ToString(CultureInfo.InvariantCulture);
        }

        public void Perday13Sum()
        {
            int i;
            double sum = 0;
            for (i = 0; i <= GrdDisplay.Rows.Count - 1; i++)
            {
                Label lbl13 = (Label)GrdDisplay.Rows[i].FindControl("lbl13");
                try
                {
                    sum = sum + Convert.ToDouble(lbl13.Text.Trim());
                }
                catch (Exception)
                {
                    // ignored
                }
            }
            Label lbl13thCollection = (Label)GrdDisplay.FooterRow.FindControl("lbl13thCollection");
            lbl13thCollection.Text = sum.ToString(CultureInfo.InvariantCulture);
        }

        public void Perday14Sum()
        {
            int i;
            double sum = 0;
            for (i = 0; i <= GrdDisplay.Rows.Count - 1; i++)
            {
                Label lbl14 = (Label)GrdDisplay.Rows[i].FindControl("lbl14");
                try
                {
                    sum = sum + Convert.ToDouble(lbl14.Text.Trim());
                }
                catch (Exception)
                {
                    // ignored
                }
            }
            Label lbl14thCollection = (Label)GrdDisplay.FooterRow.FindControl("lbl14thCollection");
            lbl14thCollection.Text = sum.ToString(CultureInfo.InvariantCulture);
        }

        public void Perday15Sum()
        {
            int i;
            double sum = 0;
            for (i = 0; i <= GrdDisplay.Rows.Count - 1; i++)
            {
                Label lbl15 = (Label)GrdDisplay.Rows[i].FindControl("lbl15");
                try
                {
                    sum = sum + Convert.ToDouble(lbl15.Text.Trim());
                }
                catch (Exception)
                {
                    // ignored
                }
            }
            Label lbl15thCollection = (Label)GrdDisplay.FooterRow.FindControl("lbl15thCollection");
            lbl15thCollection.Text = sum.ToString(CultureInfo.InvariantCulture);
        }

        public void Perday16Sum()
        {
            int i;
            double sum = 0;
            for (i = 0; i <= GrdDisplay.Rows.Count - 1; i++)
            {
                Label lbl16 = (Label)GrdDisplay.Rows[i].FindControl("lbl16");
                try
                {
                    sum = sum + Convert.ToDouble(lbl16.Text.Trim());
                }
                catch (Exception)
                {
                    // ignored
                }
            }
            Label lbl16thCollection = (Label)GrdDisplay.FooterRow.FindControl("lbl16thCollection");
            lbl16thCollection.Text = sum.ToString(CultureInfo.InvariantCulture);
        }
        public void Perday17Sum()
        {
            int i;
            double sum = 0;
            for (i = 0; i <= GrdDisplay.Rows.Count - 1; i++)
            {
                Label lbl17 = (Label)GrdDisplay.Rows[i].FindControl("lbl17");
                try
                {
                    sum = sum + Convert.ToDouble(lbl17.Text.Trim());
                }
                catch (Exception)
                {
                    // ignored
                }
            }
            Label lbl17thCollection = (Label)GrdDisplay.FooterRow.FindControl("lbl17thCollection");
            lbl17thCollection.Text = sum.ToString(CultureInfo.InvariantCulture);
        }
        public void Perday18Sum()
        {
            int i;
            double sum = 0;
            for (i = 0; i <= GrdDisplay.Rows.Count - 1; i++)
            {
                Label lbl18 = (Label)GrdDisplay.Rows[i].FindControl("lbl18");
                try
                {
                    sum = sum + Convert.ToDouble(lbl18.Text.Trim());
                }
                catch (Exception)
                {
                    // ignored
                }
            }
            Label lbl18thCollection = (Label)GrdDisplay.FooterRow.FindControl("lbl18thCollection");
            lbl18thCollection.Text = sum.ToString(CultureInfo.InvariantCulture);
        }
        public void Perday19Sum()
        {
            int i;
            double sum = 0;
            for (i = 0; i <= GrdDisplay.Rows.Count - 1; i++)
            {
                Label lbl19 = (Label)GrdDisplay.Rows[i].FindControl("lbl19");
                try
                {
                    sum = sum + Convert.ToDouble(lbl19.Text.Trim());
                }
                catch (Exception)
                {
                    // ignored
                }
            }
            Label lbl19thCollection = (Label)GrdDisplay.FooterRow.FindControl("lbl19thCollection");
            lbl19thCollection.Text = sum.ToString(CultureInfo.InvariantCulture);
        }
        public void Perday20Sum()
        {
            int i;
            double sum = 0;
            for (i = 0; i <= GrdDisplay.Rows.Count - 1; i++)
            {
                Label lbl20 = (Label)GrdDisplay.Rows[i].FindControl("lbl20");
                try
                {
                    sum = sum + Convert.ToDouble(lbl20.Text.Trim());
                }
                catch (Exception)
                {
                    // ignored
                }
            }
            Label lbl20thCollection = (Label)GrdDisplay.FooterRow.FindControl("lbl20thCollection");
            lbl20thCollection.Text = sum.ToString(CultureInfo.InvariantCulture);
        }
        public void Perday21Sum()
        {
            int i;
            double sum = 0;
            for (i = 0; i <= GrdDisplay.Rows.Count - 1; i++)
            {
                Label lbl21 = (Label)GrdDisplay.Rows[i].FindControl("lbl21");
                try
                {
                    sum = sum + Convert.ToDouble(lbl21.Text.Trim());
                }
                catch (Exception)
                {
                    // ignored
                }
            }
            Label lbl21thCollection = (Label)GrdDisplay.FooterRow.FindControl("lbl21thCollection");
            lbl21thCollection.Text = sum.ToString(CultureInfo.InvariantCulture);
        }
        public void Perday22Sum()
        {
            int i;
            double sum = 0;
            for (i = 0; i <= GrdDisplay.Rows.Count - 1; i++)
            {
                Label lbl22 = (Label)GrdDisplay.Rows[i].FindControl("lbl22");
                try
                {
                    sum = sum + Convert.ToDouble(lbl22.Text.Trim());
                }
                catch (Exception)
                {
                    // ignored
                }
            }
            Label lbl22thCollection = (Label)GrdDisplay.FooterRow.FindControl("lbl22thCollection");
            lbl22thCollection.Text = sum.ToString(CultureInfo.InvariantCulture);
        }
        public void Perday23Sum()
        {
            int i;
            double sum = 0;
            for (i = 0; i <= GrdDisplay.Rows.Count - 1; i++)
            {
                Label lbl23 = (Label)GrdDisplay.Rows[i].FindControl("lbl23");
                try
                {
                    sum = sum + Convert.ToDouble(lbl23.Text.Trim());
                }
                catch (Exception)
                {
                    // ignored
                }
            }
            Label lbl23thCollection = (Label)GrdDisplay.FooterRow.FindControl("lbl23thCollection");
            lbl23thCollection.Text = sum.ToString(CultureInfo.InvariantCulture);
        }
        public void Perday24Sum()
        {
            int i;
            double sum = 0;
            for (i = 0; i <= GrdDisplay.Rows.Count - 1; i++)
            {
                Label lbl24 = (Label)GrdDisplay.Rows[i].FindControl("lbl24");
                try
                {
                    sum = sum + Convert.ToDouble(lbl24.Text.Trim());
                }
                catch (Exception)
                {
                    // ignored
                }
            }
            Label lbl24thCollection = (Label)GrdDisplay.FooterRow.FindControl("lbl24thCollection");
            lbl24thCollection.Text = sum.ToString(CultureInfo.InvariantCulture);
        }
        public void Perday25Sum()
        {
            int i;
            double sum = 0;
            for (i = 0; i <= GrdDisplay.Rows.Count - 1; i++)
            {
                Label lbl25 = (Label)GrdDisplay.Rows[i].FindControl("lbl25");
                try
                {
                    sum = sum + Convert.ToDouble(lbl25.Text.Trim());
                }
                catch (Exception)
                {
                    // ignored
                }
            }
            Label lbl25thCollection = (Label)GrdDisplay.FooterRow.FindControl("lbl25thCollection");
            lbl25thCollection.Text = sum.ToString(CultureInfo.InvariantCulture);
        }
        public void Perday26Sum()
        {
            int i;
            double sum = 0;
            for (i = 0; i <= GrdDisplay.Rows.Count - 1; i++)
            {
                Label lbl26 = (Label)GrdDisplay.Rows[i].FindControl("lbl26");
                try
                {
                    sum = sum + Convert.ToDouble(lbl26.Text.Trim());
                }
                catch (Exception)
                {
                    // ignored
                }
            }
            Label lbl26thCollection = (Label)GrdDisplay.FooterRow.FindControl("lbl26thCollection");
            lbl26thCollection.Text = sum.ToString(CultureInfo.InvariantCulture);
        }
        public void Perday27Sum()
        {
            int i;
            double sum = 0;
            for (i = 0; i <= GrdDisplay.Rows.Count - 1; i++)
            {
                Label lbl27 = (Label)GrdDisplay.Rows[i].FindControl("lbl27");
                try
                {
                    sum = sum + Convert.ToDouble(lbl27.Text.Trim());
                }
                catch (Exception)
                {
                    // ignored
                }
            }
            Label lbl27thCollection = (Label)GrdDisplay.FooterRow.FindControl("lbl27thCollection");
            lbl27thCollection.Text = sum.ToString(CultureInfo.InvariantCulture);
        }

        public void Perday28Sum()
        {
            int i;
            double sum = 0;
            for (i = 0; i <= GrdDisplay.Rows.Count - 1; i++)
            {
                Label lbl28 = (Label)GrdDisplay.Rows[i].FindControl("lbl28");
                try
                {
                    sum = sum + Convert.ToDouble(lbl28.Text.Trim());
                }
                catch (Exception)
                {
                    // ignored
                }
            }
            Label lbl28thCollection = (Label)GrdDisplay.FooterRow.FindControl("lbl28thCollection");
            lbl28thCollection.Text = sum.ToString(CultureInfo.InvariantCulture);
        }
        public void Perday29Sum()
        {
            int i;
            double sum = 0;
            for (i = 0; i <= GrdDisplay.Rows.Count - 1; i++)
            {
                Label lbl29 = (Label)GrdDisplay.Rows[i].FindControl("lbl29");
                try
                {
                    sum = sum + Convert.ToDouble(lbl29.Text.Trim());
                }
                catch (Exception)
                {
                    // ignored
                }
            }
            Label lbl29thCollection = (Label)GrdDisplay.FooterRow.FindControl("lbl29thCollection");
            lbl29thCollection.Text = sum.ToString(CultureInfo.InvariantCulture);
        }
        public void Perday30Sum()
        {
            int i;
            double sum = 0;
            for (i = 0; i <= GrdDisplay.Rows.Count - 1; i++)
            {
                Label lbl30 = (Label)GrdDisplay.Rows[i].FindControl("lbl30");
                try
                {
                    sum = sum + Convert.ToDouble(lbl30.Text.Trim());
                }
                catch (Exception)
                {
                    // ignored
                }
            }
            Label lbl30thCollection = (Label)GrdDisplay.FooterRow.FindControl("lbl30thCollection");
            lbl30thCollection.Text = sum.ToString(CultureInfo.InvariantCulture);
        }
        public void Perday31Sum()
        {
            int i;
            double sum = 0;
            for (i = 0; i <= GrdDisplay.Rows.Count - 1; i++)
            {
                Label lbl31 = (Label)GrdDisplay.Rows[i].FindControl("lbl31");
                try
                {
                    sum = sum + Convert.ToDouble(lbl31.Text.Trim());
                }
                catch (Exception)
                {
                    // ignored
                }
            }
            Label lbl31thCollection = (Label)GrdDisplay.FooterRow.FindControl("lbl31thCollection");
            lbl31thCollection.Text = sum.ToString(CultureInfo.InvariantCulture);
        }


        public string PerDayDepositFees(int day, string month, string ClassName)
        {
            string dd = "";
            string sql = "Select DATEPART(YEAR,FromDate) Year1,DATEPART(YEAR,ToDate) Year2 from SessionMaster where SessionName='" + Session["SessionName"] + "'";
            string year1 = oo.ReturnTag(sql, "Year1");
            string year2 = oo.ReturnTag(sql, "Year2");
            string year = DropDownList1.SelectedIndex == 0 || DropDownList1.SelectedIndex == 1 || DropDownList1.SelectedIndex == 2 ? year2 : year1;
            sql = "  select CONVERT(numeric,sum(RecievedAmount)) as RecievedAmount from FeeDeposite where Status='Paid' and DAY(FeeDepositeDate)=" + day + " and Month(FeeDepositeDate)=" + month + " and Year(FeeDepositeDate)=" + year + " and Class='" + ClassName.Trim() + "' and SessionName='" + Session["SessionName"] + "' and cancel is null";
       
            if (RadioButtonList2.SelectedIndex != 0)
            {
                sql = sql + " and MoP='" + RadioButtonList2.SelectedItem + "'";
            }
            try
            {
                dd = oo.ReturnTag(sql, "RecievedAmount");
            }
            catch (Exception) { dd = ""; }

            return dd;
        }

        public void GrandTotal()
        {
            int i;
            double GrandSum = 0;

            if (GrdDisplay.Rows.Count >= 0)
            {
                for (i = 0; i <= GrdDisplay.Rows.Count - 1; i++)
                {
                    Label lblTotalAmt = (Label)GrdDisplay.Rows[i].FindControl("lblTotalAmt");
                    GrandSum = GrandSum + Convert.ToDouble(lblTotalAmt.Text);
                }

                Label lblGrand = (Label)GrdDisplay.FooterRow.FindControl("lblGrandTotal");
                lblGrand.Text = GrandSum.ToString(CultureInfo.InvariantCulture);
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //GridDisplay();
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            PrintHelper_New.ctrl = abc;
            ClientScript.RegisterStartupScript(GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
        }
        protected void ImageButton1_Click(object sender, EventArgs e)
        {
            oo.ExportToWord(Response, "MonthWisePerDayFeeCollectionReport.doc", gdv);
        }
        protected void ImageButton2_Click(object sender, EventArgs e)
        {
            oo.ExportToExcel("MonthWisePerDayFeeCollectionReport.xls", GrdDisplay);
        }
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            GridDisplay();
       
            //lblRegister.Text = DropDownList1.SelectedItem.ToString() + " Register";
        }



        protected void ImageButton4_Click(object sender, EventArgs e)
        {
            PrintHelper_New.ctrl = abc;
            ClientScript.RegisterStartupScript(GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
        }




        protected void RadioButtonList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            abc.Visible = false;
        }
        protected void ImageButton3_Click(object sender, EventArgs e)
        {

        }

        public override void Dispose()
        {
            con.Dispose();
            con1.Dispose();
            oo.Dispose();
        }
    }
}