using System.Data;
using System.Data.SqlClient;
using System;


public partial class _Default : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    
    
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        Session.LCID = 2057;

        con = oo.dbGet_connection();
      //  Campus camp = new Campus(); camp.LoadLoader(loader);
        //if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        //{
        //    Response.Redirect("default.aspx");
        //}
      //  Campus camp = new Campus(); camp.LoadLoader(loader);

        if (!IsPostBack)
        {
            getslider();
            getnews();
            gettestimonial();
            getfeaturedvideos();
        }
    }

    public void getslider()
    {
        try
        {
            if (con.State == ConnectionState.Closed) { con.Open(); }
            DataTable dtslider = new DataTable();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "USP_HomeSlider";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@QueryFor", "S");
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(dtslider);
            if (dtslider.Rows.Count > 0)
            {
                slidershow.Visible = true;
                repeaterslider.DataSource = dtslider;
                repeaterslider.DataBind();
            }
            else
            { slidershow.Visible = false; }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (con.State == ConnectionState.Open) { con.Close(); }
        }
    }

    public void getnews()
    {
        try
        {
            DataTable dtnews = new DataTable();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "USP_NoticeInformation";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@QueryFor", "NS");
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(dtnews);
            if (dtnews.Rows.Count > 0)
            {
                news.Visible = true;
                repeaternews.DataSource = dtnews;
                repeaternews.DataBind();
            }
            else { news.Visible = false; }
            DataTable dtnews1 = new DataTable();
            SqlCommand cmd1 = new SqlCommand();
            cmd1.CommandText = "USP_NoticeInformation";
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Connection = con;
            cmd1.Parameters.AddWithValue("@QueryFor", "N1");
            SqlDataAdapter da1 = new SqlDataAdapter();
            da1.SelectCommand = cmd1;
            da1.Fill(dtnews1);
            if (dtnews1.Rows.Count > 0)
            {
                news.Visible = true;
                repeaternews1.DataSource = dtnews1;
                repeaternews1.DataBind();
            }
            else { news.Visible = false; }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        { }
    }

    public void gettestimonial()
    {
        try
        {
            DataTable dtnews1 = new DataTable();
            SqlCommand cmd1 = new SqlCommand();
            cmd1.CommandText = "USP_TestimonialInformatio";
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Connection = con;
            cmd1.Parameters.AddWithValue("@QueryFor", "T1");
            SqlDataAdapter da1 = new SqlDataAdapter();
            da1.SelectCommand = cmd1;
            da1.Fill(dtnews1);
            if (dtnews1.Rows.Count > 0)
            {
                testimonial.Visible = true;
                reptestimonial1.DataSource = dtnews1;
                reptestimonial1.DataBind();
            }
            else { testimonial.Visible = false; }

            DataTable dtnews = new DataTable();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "USP_TestimonialInformatio";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@QueryFor", "TS");
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(dtnews);
            if (dtnews.Rows.Count > 0)
            {
                testimonial.Visible = true;
                reptestimonial.DataSource = dtnews;
                reptestimonial.DataBind();
            }
            else { testimonial.Visible = false; }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        { }
    }

    public void getfeaturedvideos()
    {
        try
        {
            DataTable dtfeature = new DataTable();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "USP_HomePageVideo";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@QueryFor", "SF");
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(dtfeature);
            if (dtfeature.Rows.Count > 0)
            {
                featuredshowvideo.Visible = true;
                int FCount = dtfeature.Rows.Count;
                if (FCount == 3)
                {
                    repeaterfeaturevideo1.Visible = true;
                    repeaterfeaturevideo1.DataSource = dtfeature;
                    repeaterfeaturevideo1.DataBind();
                }
                else if (FCount == 2)
                {
                    repeaterfeaturevideo2.Visible = true;
                    repeaterfeaturevideo2.DataSource = dtfeature;
                    repeaterfeaturevideo2.DataBind();
                }
                else if (FCount == 1)
                {
                    repeaterfeaturevideo3.Visible = true;
                    repeaterfeaturevideo3.DataSource = dtfeature;
                    repeaterfeaturevideo3.DataBind();
                }
            }
            else { featuredshowvideo.Visible = false; }

            
            //if (dtfeature.Rows.Count > 0)
            //{
                
            //    featuredshowvideo.Visible = true;
            //    string featuredvideos = String.Empty;
            //    for (int f = 0; f < dtfeature.Rows.Count; f++)
            //    {
            //        int fcount = dtfeature.Rows.Count;
            //        if (fcount == 3)
            //        {
            //            ltrfeatured.Visible = true;
            //            featuredvideos += "<div class='col-md-4 col-sm-6  res-pb-xs-30'> <div class='single-blog'>" + dtfeature.Rows[f]["YouTubeIFrameURL"] + " </div> </div>";
            //        }
            //        else if (fcount == 2)
            //        {
            //            ltrfeatured1.Visible = true;
            //            featuredvideos += " <div class='col-md-6 col-sm-6  res-pb-xs-30'> <div class='single-blog'>  "+dtfeature.Rows[f]["YouTubeIFrameURL"]+" </div> </div>";
            //        }
            //        else if (fcount == 1)
            //        {
            //            ltrfeatured2.Visible = true;
            //            featuredvideos += " <div class='col-md-12 col-sm-12  res-pb-xs-30'> <div class='single-blog'>  " + dtfeature.Rows[f]["YouTubeIFrameURL"] + " </div> </div>";
            //        }
            //    }
            //    ltrfeatured.Text = featuredvideos;
            //    ltrfeatured1.Text = featuredvideos;
            //    ltrfeatured2.Text = featuredvideos;
            //}
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        { }
    }


}