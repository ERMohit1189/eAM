<%@ Page Title="Assignment | eAM&#174;" Language="C#" MasterPageFile="~/sp/sp_root-manager.master" AutoEventWireup="true" 
CodeFile="landing.aspx.cs" Inherits="sp_Circular" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" Runat="Server">
 
   
    <style>
      
        #landing .main-header {
            background: #0B1E59;
            color: #fff;
            padding:2px 3px;
                border-top-left-radius: 8px;
                    border-top-right-radius: 8px;
        }
        #landing header #landing .site-title {
    font-size: 2.3em;
    display: inline-flex;
    font-family: Helvetica, Bold;
    margin-left: 13px;
    position: relative;
    top: -15px;
}
.branding {
    padding: 8px;
}
#landing header {
    background-color: #0B1E59;
    width: 100%;
    margin-left: -1px;
}
#landing .branding {
    padding-top: 10px;
}
#landing header .subtitle {
    font-size: 2rem;
    font-family: Helvetica, Bold;
    text-align: left;
    color: #FFFFFF;
    opacity: 1;
}
       #landing  .main-header .logo {
            vertical-align: middle;
            margin-top: -10px;
            margin-left: 13px;
            width: 62px;
            height: 72px;
        }
       #landing  .main-header .site-title {
            display: inline-block;
            margin-left: 15px;
            vertical-align: middle;
        }
       #landing  .main-header .site-title a {
            color: #fff;
            text-decoration: none;
        }
        #landing .main-header .site-title .subtitle {
            font-size: 14px;
            color: #ddd;
        }
        #landing .navbar-custom {
            background: #2c4e86;
            border: none;
            border-radius: 0;
        }
       #landing  .navbar-custom .navbar-brand,
        #landing .navbar-custom .navbar-nav > li > a {
            color: #fff;
            font-weight: bold;
        }
        #landing .navbar-custom .navbar-nav > li > a:hover {
            background: #0056b3;
            color: #fff;
        }
       #landing  .section {
            padding: 30px;
            border-radius: 10px;
            box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
            text-align: center;
            margin-bottom: 30px;
        }
       #landing  #gst {
            background: #ffe4b5;
        }
       #landing  #itr {
            background: #98fb98;
        }
      #landing   #epf {
            background: #add8e6;
        }
      #landing   #esic {
            background: #dda0dd;
        }
    
      #landing   header .mlinks {
    display: inline;
    float: right;
    text-align: right;
    margin: 0;
    padding: 16px 0;
    font-size: 12px;
    margin-top: 12px;
}
#landing header .skip {
    background: #051547;
    color: #fff;
    text-align: right;
    padding: 5px 0px 0px;
}
#landing .list-inline {
    padding-left: 0;
    list-style: none;
    margin-left: 0;
    float: right;
    padding-top: 10px;
}
#landing .btn-primary {
    color: #fff;
    background-color: #428bca !important;
    border-color: #357ebd;
}
#landing .nav li a {
    padding: 16px 15px;
}
#landing header .skip {
    background: #051547;
    color: #fff;
    text-align: right;
}
#landing header .skip a {
    color: #fff;
}
#landing header .skip a:link, header .skip a:visited {
    text-decoration: none !important;
}
#landing header .mlinks > li {
    /* border-right: 1px solid white; */
    min-height: 22px;
    vertical-align: top;
    padding: 0 10px;
}
#landing header .button {
    border: 1px solid #FFFFFF;
    padding: 8px 15px;
    margin-left: -10px;
    background: #FFFFFF 0% 0% no-repeat padding-box;
    color: #2C4E86 !important;
    text-transform: uppercase;
    text-align: center;
    display: inline-flex;
    font-size: 1em;
    font: normal normal bold 14px / 16px Helvetica;
    border-radius: 4px;
    opacity: 1;
    text-decoration: none;
}
.justify-content-between{justify-content:space-between !important;}
.item-center{align-items:center}
.flex-wrap{flex-wrap:wrap  !important;}
    </style>
         <div id="loader" runat="server"></div>
            <asp:UpdatePanel ID="tyu" runat="server">
                <ContentTemplate>
                 <div id="landing" class="panel widget light-widget" style="margin:12px 12px; min-height:700px;background: radial-gradient(circle, #000000 10%, transparent 10%) 0 0 / 10px 10px, #ffffff; padding:10px; color: #000;">
                    <div>
                        <div class="container">
                            <div class="row">
                                <div class="col-xs-12">
                                    <h1 class="text-center text-black" style="color:#000000; font-weight:700; text-align:center !important; padding:20px 0px;">
                                        This Site is for Education Purpose only
                                    </h1>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="container">
                        <div class="row">
                            <div class="col-md-3">
                                <a href="/sp/GST-home.aspx" style="color:#FFF !important;">
                                    <div class="section" style="background: url('../img/gst-banner.png') no-repeat center center; object-fit:cover; padding:12px; color: #fff;min-height:330px;display:flex; justify-content: center;align-items: center;">
                                        <div>
                                            <h2 style="text-shadow: -2px 0px 8px rgb(0 0 0); font-size:24px; line-height:28px;">GST Filing Services</h2>
                                            <p style="text-shadow: -2px 0px 8px rgb(0 0 0); font-size:15px; line-height:22px;">We provide comprehensive GST filing services to ensure your compliance 
                                            with government regulations. Simplify your GST returns with our expert assistance.</p>
                                        <a href="/sp/GST-home.aspx" class="btn btn-primary">Learn More</a></div>
                                    </div>
                                </a>
                            </div>

                            <div class="col-md-3">
                                <a href="#" style="color:#FFF !important;">
                                    <div class="section" style="background: url('../img/gst-banner.png') no-repeat center center; object-fit:cover; padding:12px; color: #fff;min-height:330px;display:flex; justify-content: center;align-items: center;">
                                        <div>
                                             <h2 style="text-shadow: -2px 0px 8px rgb(0 0 0); font-size:24px; line-height:28px;">Income Tax Return Filing</h2>
                                            <p style="text-shadow: -2px 0px 8px rgb(0 0 0); font-size:15px; line-height:22px;">Get your income tax returns filed accurately and on time 
                                            with our reliable tax professionals. Save time and avoid penalties with our services.</p>
                                                <a href="#" class="btn btn-primary">Learn More</a>
                                          </div>
                                    </div>
                                 </a>
                            </div>

                            <div class="col-md-3">
                                <a href="#" style="color:#FFF !important;">
                                   <div class="section" style="background: url('../img/gst-banner.png') no-repeat center center; object-fit:cover; padding:12px; color: #fff;min-height:330px;display:flex; justify-content: center;align-items: center;">
                                        <div>
                                            <h2 style="text-shadow: -2px 0px 8px rgb(0 0 0); font-size:24px; line-height:28px;">EPF Management Services</h2>
                                            <p style="text-shadow: -2px 0px 8px rgb(0 0 0); font-size:15px; line-height:22px;">Our EPF management services help businesses manage employee 
                                            provident funds efficiently, ensuring compliance with EPFO guidelines.</p>
                                            <a href="#" class="btn btn-primary">Learn More</a>
                                         </div>
                                    </div>
                                </a>
                            </div>

                            <div class="col-md-3">
                                <a href="#" style="color:#FFF !important;">
                                   <div class="section" style="background: url('../img/gst-banner.png') no-repeat center center; object-fit:cover; padding:12px; color: #fff;min-height:330px;display:flex; justify-content: center;align-items: center;">
                                        <div>
                                            <h2 style="text-shadow: -2px 0px 8px rgb(0 0 0); font-size:24px; line-height:28px;">ESIC Services</h2>
                                            <p style="text-shadow: -2px 0px 8px rgb(0 0 0); font-size:15px; line-height:22px;">We assist businesses in managing ESIC compliance, 
                                                ensuring timely contributions.</p>
                                            <a href="#" class="btn btn-primary">Learn More</a>
                                        </div>
                                    </div>
                                </a>
                            </div>
                        </div>
                    </div>
                </div>



        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

