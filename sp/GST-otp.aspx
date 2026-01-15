<%@ Page Title="Assignment | eAM&#174;" Language="C#" MasterPageFile="~/sp/sp_root-manager.master" AutoEventWireup="true" 
CodeFile="GST-otp.aspx.cs" Inherits="sp_Circular" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" Runat="Server">
    <link rel="stylesheet" type="text/css" href="../css/gst.css" />
    
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
.navbar-default .navbar-nav>li>a {
    color: #ffffff;
}
.navbar-default .navbar-nav>li>a:hover {
    color: #ffffff;
}
.form-gst.container {
    max-width: 780px;
}
    </style>
         <div id="loader" runat="server"></div>
            <asp:UpdatePanel ID="tyu" runat="server">
                <ContentTemplate>
                     <div class="panel widget light-widget" style="margin:12px 12px;">
                        <!--***********registration SECTION start***************-->
                           <div id="landing">   
                                   
                                    <div class="container">
                                        <div class="row">
                                            <div class="col-xs-12">
                                                <h1 class="text-center text-black" style="color:#000000; font-weight:700; text-align:center !important; padding:20px 0px;">
                                                    This Site is for Education Purpose only
                                                </h1>
                                            </div>
                                        </div>
                                    </div>
                                   
                                  <div class="main-header">
                                      <div class="skip">
                                          <div class="container">
                                              <div class="row">                                                   
                                                    <div class="col-md-12">
                                                        <ul class="skip list-inline">
                                                            <li><a tabindex="-1" class="accessible" href="javascript:void(0)">Skip to Main Content</a></li>
                                                            <li class="high-low"><i class="fa fa-adjust"></i></li>
                                                            <li class="fresize f-up">A<sup>+</sup></li>
                                                            <li class="fresize f-down">A<sup>-</sup></li>
                                                        </ul>
                                                    </div>
                                                </div>
                                          </div>
                                      </div>
                                      <div class="container">
                                          <div class="row branding">
                                              <div class="col-xs-12">
                                                  <div class="d-flex justify-content-between flex-wrap">
                                                      <div class="d-flex gap-12 item-center">
                                                          <img src="../img/Emblem_of_India-white.svg" class="logo" alt="">
                                 
                                                          <h1 class="site-title">
                                                              Goods and Services Tax<br>
                                                              <span class="subtitle">Government of India, States and Union Territories</span>
                                     
                                                          </h1>
                                                      </div>
                                                   </div>
                                              </div>
                                          </div>
                                      </div>
                                  </div>
                                <nav class="navbar navbar-default collapsed" style="top: auto;">
                                   <div class="container nav-color">
                                      <div id="main" class="navbar-collapse collapse" aria-expanded="false">
                                         <ul class="nav navbar-nav">
                                            <li>
                                               <a href="/sp/GST-home.aspx">Home</a>
                                            </li>
                                            <li class="dropdown" data-ng-class="{'active': servers.NAV_COMPONENT == 'services'}">
                                               <a data-ng-href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-expanded="false">Services <span class="caret"></span></a>
                                               <ul class="dropdown-menu smenu" role="menu">
                                                  <li class="has-sub">
                                                     <a data-ng-show="udata &amp;&amp; udata.role == 'login'" data-ng-href="{{servers.GST_SERVICES_R1_URL}}/services/auth/quicklinks/registration">Registration</a>
                                                     <ul class="isubmenu serv">
                                                        <li>
                                                           <a href="/sp/GST.aspx">New Registration</a>
                                                        </li>
                                                        <li>
                                                           <a data-ng-show="udata &amp;&amp; udata.role == 'panLogin'" data-ng-href="{{servers.GST_SERVICES_R1_URL}}/services/auth/appstatus">Track Application Status</a>
                                                        </li>
                                                     </ul>
                                                  </li>
                                                  <li class="has-sub">
                                                     <a data-ng-show="udata &amp;&amp; udata.role == 'login'" data-ng-href="{{servers.GST_SERVICES_R1_URL}}/services/auth/quicklinks/payments">Ledgers</a>
                                                     <ul class="isubmenu pay" data-ng-class="{'post': udata &amp;&amp; udata.role == 'login'}">
                                                     </ul>
                                                  </li>
                                                  <li class="has-sub">
                                                     <a data-ng-show="udata &amp;&amp; (udata.role == 'login'||udata.role == 'panLogin')" data-ng-href="{{servers.GST_SERVICES_R1_URL}}/services/auth/quicklinks/userservices">Returns</a>
                                                     <ul class="isubmenu oth" data-ng-class="{'services': !udata,'oth': udata &amp;&amp; udata.role == 'login'}">
                                                        <li>
                                                           <a href="/sp/GST-home.aspx">Returns dashboard</a>
                                                        </li>
                                                        <li>
                                                           <a data-ng-href="{{servers.GST_SERVICES_R1_URL}}/services/searchtp">View e-Filed Returns</a>
                                                        </li>
                                                        <li>
                                                           <a data-ng-href="{{servers.GST_SERVICES_R1_URL}}/services/listoftaxpayer/">Track Return Status</a>
                                                        </li>
                                                     </ul>
                                                  </li>
                                                  <li class="has-sub">
                                                     <a data-ng-show="udata &amp;&amp; (udata.role == 'login'||udata.role == 'panLogin')" data-ng-href="{{servers.GST_SERVICES_R1_URL}}/services/auth/quicklinks/userservices">Payments</a>
                                                  </li>
                                                  <li class="has-sub">
                                                     <a data-ng-show="udata &amp;&amp; (udata.role == 'login'||udata.role == 'panLogin')" data-ng-href="{{servers.GST_SERVICES_R1_URL}}/services/auth/quicklinks/userservices">User Services</a>
                                                     <ul class="isubmenu oth5" data-ng-class="{'services': !udata,'oth': udata &amp;&amp; udata.role == 'login'}">
                                                        <li data-ng-show="udata &amp;&amp; (udata.role == 'login' || udata.role == 'panLogin')">
                                                           <a data-ng-href="{{servers.GST_SERVICES_R1_URL}}/services/auth/holiday">Holiday List</a>
                                                        </li>
                                                        <li data-ng-show="!udata">
                                                           <a data-ng-href="{{servers.GST_SERVICES_R1_URL}}/services/searchtp">Search Taxpayer</a>
                                                        </li>
                                                        <li>
                                                           <a data-ng-href="{{servers.GST_SERVICES_R1_URL}}/services/listoftaxpayer/">Search Taxpayer Opted In / Out of Composition</a>
                                                        </li>
                                                        <li>
                                                           <a data-ng-href="{{servers.GST_SERVICES_R1_URL}}/services/officeaddress/">Search Office Addresses</a>
                                                        </li>
                                                     </ul>
                                                  </li>
                                               </ul>
                                            </li>
                                            <li class="dropdown" data-ng-class="{'active': servers.NAV_COMPONENT == 'services'}">
                                               <a data-ng-href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-expanded="false">GST Law </a>
                                            </li>
                                            <li class="has-sub" data-ng-class="{'active': servers.NAV_COMPONENT == 'downloads'}" ng-hide="udata &amp;&amp; udata.role == 'login'">
                                               <a data-ng-href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-expanded="false">Downloads <span class="caret"></span>
                                               </a>
                                               <ul class="dropdown-menu smenu" role="menu">
                                                  <li class="has-sub">
                                                     <a>Offline Utilities</a>
                                                     <ul class="isubmenu down">
                                                        <li class="has-sub">
                                                           <a href="/GST/Services/OfflineTool">Returns Offline Utility</a>
                                                        </li>
                                                        <li class="has-sub">
                                                           <a href="/GST/Services/GSTR3BOfflineUtility">GSTR3B Offline Utility</a>
                                                        </li>
                                                        <li class="has-sub">
                                                           <a title="GSTR 4 Offlinr Tool" href="/GST/Utilities/GSTR_4_Offline_Utility.zip">GSTR 4 Offline Tool</a>
                                                        </li>
                                                     </ul>
                                                  </li>
                                               </ul>
                                            </li>
                                            <li class="dropdown">
                                               <a data-ng-href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-expanded="false">Search Taxpayer </a>
                                            </li>
                                            <li class="dropdown" data-ng-class="{'active': servers.NAV_COMPONENT == 'services'}">
                                               <a data-ng-href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-expanded="false">Help </a>
                                            </li>
                                            <li class="dropdown" data-ng-class="{'active': servers.NAV_COMPONENT == 'services'}">
                                               <a class="dropdown-toggle" href="/GST/Utility/GSTeWayBillSystem">e-Way Bill System</a>
                                            </li>
                                            <li class="mnav" data-ng-show="!udata"><a data-ng-href="{{servers.GST_SERVICES_R1_URL}}/services/login">Login</a></li>
                                            <li class="mnav" data-ng-show="udata"><a data-ng-href="{{servers.GST_SERVICES_R1_URL}}/services/auth/myprofile">MyProfile</a></li>
                                            <li class="mnav" data-ng-show="udata"><a data-ng-href="{{servers.GST_SERVICES_R1_URL}}/services/logout">Logout</a></li>
                                         </ul>
                                      </div>
                                   </div>
                                </nav>
                            </div>

   
       
                        <div class="form-gst container" style="margin-top:20px;">       
                            <div class="card-form">         
                          
                               <form action="" method="post">                                
                                   <h4 data-ng-bind="trans.HEAD_VERIFY_OTP">Verify OTP</h4>
                                  <hr>
                                  <p class="mand-text">indicates mandatory fields</p>
                                  <fieldset>
                                     <div class="row">
                                        <div class="form-group col-xs-12">                         
                                           <div class="row">
                                                 <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">                                 
                                                    <label class="reg m-cir" for="mobile_otp">Mobile OTP</label>
                                                    <div class="form-group has-feedback">  
                                                       <input autocomplet="off" class="form-control" data-val="true" data-val-length="OTP must be 6 digits" data-val-length-max="6" data-val-length-min="6" data-val-range="OTP must be 6 digits" data-val-range-max="999999" data-val-range-min="100000" data-val-required="Enter a valid OTP" id="NR_MobileNumberOTP" maxlength="6" name="NR_MobileNumberOTP" type="text" value="">
                                                       <span class="err"><span class="field-validation-valid" data-valmsg-for="NR_EmailOTP" data-valmsg-replace="true"></span> </span>
                                                          <span class="help-block">
                                                             <i class="fa fa-info-circle"></i><span>Enter OTP sent to your mobile number</span>
                                                          </span>
                                                    </div>
                                                 </div>
                                 
                                                 <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                    <label class="reg m-cir">Email OTP</label>
                                                    <div class="form-group has-feedback">                                      
                                                       <input autocomplet="off" class="form-control" id="NR_EmailOTP" maxlength="6" name="NR_EmailOTP" type="text" value="">
                                                       <span class="err"><span class="field-validation-valid" data-valmsg-for="NR_EmailOTP" data-valmsg-replace="true"></span> </span>
                                       
                                                       <span class="help-block">
                                                             <p><i class="fa fa-info-circle"></i><span>Enter OTP sent to your Email Address</span></p>
                                                             <p><i class="fa fa-info-circle"></i><span>Please check the junk/spam folder in case you do not get email.</span></p>
                                                       </span>
                                                    </div>
                                                 </div>                                
                                           </div>
                                           <p><a href="#" onclick="alert('OTP is Resent')">Need OTP to be resent? Click here</a> </p>                          
                                        </div>
                                     </div>
                               </fieldset>               
                               <div class="row">
                                     <div class="col-xs-12 text-right">
                                        <a class="btn btn-default" href="/sp/GST.aspx">Back</a>
                                        <a  href="/sp/verify.aspx" class="btn btn-blue text-white" style="color:#FFFFFF;">Proceed</a>
                                     </div>
                               </div>
                            </form>
                         </div>
                       </div>
                          <!-- **************footer****************-->
                             <footer class="bg-dark">
                               <div class="f1 menuList">
                                  <div class="container">
                                     <div class="row">
                                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-12 no-mobile">
                                           <a class="fhead" href="#">About GST</a>
                                           <ul>
                                              <li><a href="javascript:void(0);">GST Council Structure</a></li>
                                              <li><a href="javascript:void(0);">GST History</a></li>
                                           </ul>
                                        </div>
                                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-12 no-mobile">
                                           <a class="fhead" href="javascript:void(0);">Website Policies</a>
                                           <ul>
                                              <li><a href="javascript:void(0);">Website Policy</a></li>
                                              <li><a href="javascript:void(0);">Terms and Conditions</a></li>
                                              <li><a href="javascript:void(0);">Hyperlink Policy</a></li>
                                              <li><a href="javascript:void(0);">Disclaimer</a></li>
                                           </ul>
                                        </div>
                                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-12 no-mobile">
                                           <a class="fhead" href="#">Related Sites</a>
                                           <ul>
                                              <li><a href="javascript:void(0);">Central Board 
                                                 of Indirect Taxes and Customs <i class="fa fa-external-link-square"></i></a>
                                              </li>
                                              <li><a href="javascript:void(0);">State
                                                 Tax Websites</a>
                                              </li>
                                              <li><a data-popup="true" href="#">National
                                                 Portal <i class="fa fa-external-link-square"></i></a>
                                              </li>
                                           </ul>
                                        </div>
                                        <div class="col-lg-3 col-md-3 col-sm-2 col-xs-12 help no-mobile">
                                           <a class="fhead" href="#">Help and Taxpayer Facilities</a>
                                           <ul>
                                              <li> <a href="#">System Requirements</a></li>
                                              <li> <a href="#">GST Knowledge Portal</a></li>
                                              <li> <a href="#">GST Media <i class="fa fa-external-link-square"></i></a></li>
 
                                              <li class="fhead" >Site Map</a></li>
                                              <!---->
                                              <li><a href="javascript:void(0);">Grievance Nodal Officers  </a></li>
                                              <li><a href="javascript:void(0);">Free Accounting and Billing Services <i class="fa fa-external-link-square"></i></a></li>
                                              <li><a href="javascript:void(0);">GST Suvidha Providers <i class="fa fa-external-link-square"></i></a></li>
                                           </ul>
                                        </div>
                                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12 cont no-mobile scl">
                                           <a class="fhead" href="#">Contact Us</a>
                                           <ul>
                                              <li>
                                                 <span class="contact">Help Desk Number: <br>1800-103-4786</span>
                                              </li>                        
                                              <li>
                                                 <span class="contact">Log/Track Your Issue:<br></span><a href="#" title="Grievance Redressal Portal for GST">Grievance Redressal Portal for GST <i class="fa fa-external-link-square"></i></a>
                                              </li>
                                              <li class="social">
                                                 <a data-popup="true" href="#" title="Facebook""><span><img src="../img/Facebook.svg"></span></a>
                                                 <a data-popup="true" href="#" title="Youtube"><span><img src="../img/Youtube.svg"></span></a>
                                                 <a data-popup="true" href="#" title="Twitter"><span><img src="../img/X.svg"></span></a>
                                                 <a data-popup="true" href="#" title="Linkedin"><span><img src="../img/Linkedin.svg"></span></a>
                                              </li>
                                              <!---->
                                           </ul>
                                        </div>
                                     </div>
                                  </div>
                               </div>
                               <div class="f2">
                                  <div class="container">
                                     <div class="row">
                                        <div class="col-xs-12">
                                           <p>©&nbsp;2025&nbsp;Goods and Services Tax Network</p>
                                           <p>Site Last Updated on 06-01-2025</p>
                                           <p>Designed &amp; Developed by GSTN</p>
                                        </div>
                                     </div>
                                  </div>
                               </div>
                               <div class="f3">
                                  <div class="container">
                                     <div class="row">
                                        <div class="col-xs-12">
                                           <p class="site">Site best viewed at 1024 x 768 resolution in Microsoft Edge, Google Chrome 49+, Firefox 45+ and Safari 6+</p>
                                        </div>
                                     </div>
                                  </div>
                               </div>
                               <style>
                                  .disabled{
                                  cursor: not-allowed !important;
                                  opacity: 0.6;
                                  }
                                  .menuList .disabled:active {
                                  pointer-events: none;
                                  }
                                  .menuList .disabledanchor {
                                  cursor: not-allowed;
                                  pointer-events: none;
                                  }
                                  footer.bg-dark {
                                         position: relative;
                                         padding-top: 0px;
                                         bottom: 0;
                                         left: 0;
                                         right: 0;
                                         background: #14315D;
                                     }
                               </style>  
                            </footer>

                              <!-- **************footer end****************-->
                    </div>
            
                     <!--***********OTP SECTION end***************-->

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

