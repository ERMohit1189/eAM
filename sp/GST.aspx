<%@ Page Title="Assignment | eAM&#174;" Language="C#" MasterPageFile="~/sp/sp_root-manager.master" AutoEventWireup="true" 
CodeFile="GST.aspx.cs" Inherits="sp_Circular" %>

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
                                 <h4>New Registration</h4>
                                 <hr>
                                 <p class="mand-text">indicates mandatory fields</p>
                                 <div class="row">
                                    <div class="col-xs-12">
                                       <fieldset class="d-flex gap-12">
                                           <div class="d-flex gap-8 align-center">
                                                <input name="radioregis" checked="checked" type="radio">
                                                <label class="mb-0" for="radionew">New Registration</label>
                                           </div>
                                            <div class="d-flex gap-8 align-center">
                                                 <input name="radioregis" type="radio">
                                                  <label class="mb-0" for="radiotrn"><a href="#">Temporary Reference Number (TRN) </a></label>
                                            </div>
                                     
                                       </fieldset>
                                    </div>
                                 </div>
                                 <!---->
                                 <!---->
                                 <form action="" method="post">                   
                                    <fieldset>
                                       <div class="row margin-bottom-10">
                                          <div class="col-xs-12">
                                             <label class="reg m-cir">I am a</label>
                                             <select class="input-validation-error form-control" data-val="true" data-val-required="User profile is required" id="UserType" name="UserType">
                                                <option selected="selected" value="">Select</option>
                                                <option value="Taxpayer">Taxpayer</option>
                                                <option value="GST_practitioner">GST Practitioner</option>
                                             </select>
                                             <span class="err"><span class="text-danger" data-valmsg-for="UserType" data-valmsg-replace="true">User profile is required</span></span>
                                             <!---->
                                             <!---->
                                          </div>
                                       </div>
                                       <div class="row margin-bottom-10">
                                          <div class="col-xs-12">
                                             <label class="reg m-cir">State / UT</label>
                                             <select class="input-validation-error form-control" data-val="true" data-val-range="State or Union Territory is required" data-val-range-max="32767" data-val-range-min="1" id="State" name="State" onchange="fillDropDownDistrict($(this).val())">
                                                <option selected="selected" value="0">Select</option>
                                                <option value="31">Andaman and Nicobar Islands</option>
                                                <option value="1">Andhra Pradesh</option>
                                                <option value="2">Arunachal Pradesh</option>
                                                <option value="3">Assam</option>
                                                <option value="4">Bihar</option>
                                                <option value="32">Chandigarh</option>
                                                <option value="5">Chhattisgarh</option>
                                                <option value="33">Dadra and Nagar Haveli</option>
                                                <option value="34">Daman and Diu</option>
                                                <option value="35">Delhi</option>
                                                <option value="6">Goa</option>
                                                <option value="7">Gujarat</option>
                                                <option value="8">Haryana</option>
                                                <option value="9">Himachal Pradesh</option>
                                                <option value="10">Jammu and Kashmir</option>
                                                <option value="11">Jharkhand</option>
                                                <option value="12">Karnataka</option>
                                                <option value="13">Kerala</option>
                                                <option value="36">Lakshadweep</option>
                                                <option value="14">Madhya Pradesh</option>
                                                <option value="15">Maharashtra</option>
                                                <option value="16">Manipur</option>
                                                <option value="17">Meghalaya</option>
                                                <option value="18">Mizoram</option>
                                                <option value="19">Nagaland</option>
                                                <option value="20">Orissa</option>
                                                <option value="37">Pondicherry</option>
                                                <option value="21">Punjab</option>
                                                <option value="22">Rajasthan</option>
                                                <option value="23">Sikkim</option>
                                                <option value="24">Tamil Nadu</option>
                                                <option value="30">Telangana</option>
                                                <option value="25">Tripura</option>
                                                <option value="27">Uttar Pradesh</option>
                                                <option value="26">Uttarakhand</option>
                                                <option value="28">West Bengal</option>
                                             </select>
                                             <span class="err"><span class="text-danger" data-valmsg-for="State" data-valmsg-replace="true">State or Union Territory is required</span></span>
                                        
                                          </div>
                                       </div>
                                       <div class="row margin-bottom-10">
                                          <div class="col-xs-12">
                                             <label class="reg m-cir">District</label>
                                             <select class="input-validation-error form-control" data-val="true" data-val-required="District is required" id="District" name="District">
                                                   <option value="0">Select</option>
                                                   <option value="597">Agra</option>
                                                   <option value="598">Aligarh</option>
                                                   <option value="599">Allahabad</option>
                                                   <option value="600">Ambedkar Nagar</option>
                                                   <option value="601">Amethi</option>
                                                   <option value="602">Amroha</option>
                                                   <option value="603">Auraiya</option>
                                                   <option value="604">Azamgarh</option>
                                                   <option value="605">Baghpat</option>
                                                   <option value="606">Bahraich</option>
                                                   <option value="607">Ballia</option>
                                                   <option value="608">Balrampur</option>
                                                   <option value="609">Banda</option>
                                                   <option value="610">Barabanki</option>
                                                   <option value="611">Bareilly</option>
                                                   <option value="612">Basti</option>
                                                   <option value="613">Bijnor</option>
                                                   <option value="614">Budaun</option>
                                                   <option value="615">Bulandshahr</option>
                                                   <option value="616">Chandauli</option>
                                                   <option value="617">Chitrakoot</option>
                                                   <option value="618">Deoria</option>
                                                   <option value="619">Etah</option>
                                                   <option value="620">Etawah</option>
                                                   <option value="621">Faizabad</option>
                                                   <option value="622">Farrukhabad</option>
                                                   <option value="623">Fatehpur</option>
                                                   <option value="624">Firozabad</option>
                                                   <option value="625">Gautam Buddha Nagar</option>
                                                   <option value="626">Ghaziabad</option>
                                                   <option value="627">Ghazipur</option>
                                                   <option value="628">Gonda</option>
                                                   <option value="629">Gorakhpur</option>
                                                   <option value="630">Hamirpur</option>
                                                   <option value="631">Hardoi</option>
                                                   <option value="632">Hathras (Mahamaya Nagar)</option>
                                                   <option value="633">Jalaun</option>
                                                   <option value="634">Jaunpur</option>
                                                   <option value="635">Jhansi</option>
                                                   <option value="636">Jyotiba Phule Nagar</option>
                                                   <option value="637">Kannauj</option>
                                                   <option value="638">Kanpur Dehat (Ramabai Nagar)</option>
                                                   <option value="639">Kanpur Nagar</option>
                                                   <option value="640">Kanshiram Nagar</option>
                                                   <option value="641">Kaushambi</option>
                                                   <option value="642">Kheri</option>
                                                   <option value="643">Kushinagar</option>
                                                   <option value="644">Lalitpur</option>
                                                   <option value="645">Lucknow</option>
                                                   <option value="646">Maharajganj</option>
                                                   <option value="647">Mahoba</option>
                                                   <option value="648">Mainpuri</option>
                                                   <option value="649">Mathura</option>
                                                   <option value="650">Mau</option>
                                                   <option value="651">Meerut</option>
                                                   <option value="652">Mirzapur</option>
                                                   <option value="653">Moradabad</option>
                                                   <option value="654">Muzaffarnagar</option>
                                                   <option value="655">Panchsheel Nagar district (Hapur)</option>
                                                   <option value="656">Pilibhit</option>
                                                   <option value="657">Pratapgarh</option>
                                                   <option value="658">Raebareli</option>
                                                   <option value="659">Rampur</option>
                                                   <option value="660">Saharanpur</option>
                                                   <option value="661">Sant Kabir Nagar</option>
                                                   <option value="662">Sant Ravidas Nagar</option>
                                                   <option value="663">Shahjahanpur</option>
                                                   <option value="664">Shamli</option>
                                                   <option value="665">Shravasti</option>
                                                   <option value="666">Siddharthnagar</option>
                                                   <option value="667">Sitapur</option>
                                                   <option value="668">Sonbhadra</option>
                                                   <option value="669">Sultanpur</option>
                                                   <option value="670">Unnao</option>
                                                   <option value="671">Varanasi</option>
                                                </select>
                                             <span class="err"><span class="text-danger" data-valmsg-for="District" data-valmsg-replace="true">District is required</span></span>
                                          </div>
                                       </div>
                                       <div class="row margin-bottom-10">
                                          <div class="col-xs-12">
                                             <label for="bnm" class="reg m-cir">
                                                Legal Name of the Business (As mentioned in PAN)
                                             </label>
                                             <!---->
                                             <input class="input-validation-error form-control" data-val="true" data-val-required="Legal name of the business is required" id="LegalName" maxlength="50" name="LegalName" placeholder="Enter Legal Name of Business" type="text" value="">
                                             <span class="err"><span class="text-danger" data-valmsg-for="LegalName" data-valmsg-replace="true">Legal name of the business is required</span></span>
                                             <!---->
                                             <!---->
                                          </div>
                                       </div>
                                       <!---->
                                       <div class="row margin-bottom-10" data-ng-if="(plogin.applnType !== 'APLNR')">
                                          <!---->
                                          <div class="col-xs-12">
                                             <label class="reg m-cir">Permanent Account Number (PAN)</label>
                                             <div class="has-feedback" data-ng-class="{'has-error': form_submitted &amp;&amp; preg.pan_card.$invalid, 'has-success': form_submitted &amp;&amp; preg.pan_card.$valid}">
                                                <input class="input-validation-error form-control" data-val="true" data-val-length="PAN must be of 10 characters" data-val-length-max="10" data-val-length-min="10" data-val-required="Permanent Account Number (PAN) is required" id="PAN" maxlength="10" name="PAN" placeholder="Enter Permanent Account Number(PAN)" style="text-transform:uppercase;" type="text" value="">
                                                <span class="err"><span class="text-danger" data-valmsg-for="PAN" data-valmsg-replace="true">Permanent Account Number (PAN) is required</span> </span>
                                             </div>  
                                             <i class="fa fa-info-circle"></i>
                                             <span data-ng-bind="trans.HLP_APPLY_PAN_LINK1">If you don't have PAN, Click</span>
                                          </div>
                                       </div>
                                       <!---->
                                       <!---->
                                       <div class="row">
                                          <div class="col-xs-12">
                                             <label class="reg m-cir" for="email"><span>Email Address</span></label>
                                             <div class="input-group">
                                                <span class="input-group-addon" id="ba2"><i class="fa fa-envelope"></i></span>
                                                <input class="input-validation-error form-control" data-val="true" data-val-email="Enter a Valid Email ID" data-val-required="Email Address is required" id="EmailId" maxlength="50" name="EmailId" placeholder="Enter Email Address" type="text" value="">
                                                <br>
                                             </div>
                                             <span class="err"><span class="text-danger">Email Address is required</span> </span>
                                             <span class="help-block"><i class="fa fa-info-circle"></i><span>OTP will be sent to this Email Address</span></span>
                                          </div>
                                       </div>
                                       <div class="row">
                                          <div class="col-xs-12">
                                             <label class="reg m-cir" for="mobile"><span>Mobile Number</span></label>
                                             <div class="input-group">
                                                <span class="input-group-addon" id="ba">+91</span>
                                                <input class="input-validation-error form-control" data-val="true" data-val-number="The field MobileNumber must be a number." data-val-range="Enter a Valid Mobile Number" data-val-range-max="9999999999" data-val-range-min="4000000000" data-val-required="Mobile number is required" id="MobileNumber" maxlength="10" name="MobileNumber" placeholder="Enter Mobile Number" type="text" value="0">
                                                <br>
                                             </div>
                                             <span class="err"><span class="text-danger" data-valmsg-for="MobileNumber" data-valmsg-replace="true">Enter a Valid Mobile Number</span></span>
                                             <sp an="" class="help-block"><i class="fa fa-info-circle"></i> <span data-ng-bind="trans.HLP_OTP_MOBILE_HELP">Separate OTP will be sent to this mobile number</span>
                                             </sp>
                                          </div>
                                       </div>
                                       <!---->
                                       <div class="row" data-ng-if="preg.$dirty" id="captchaID" style="display: none;">
                                          <div class="col-xs-12">
                                             <label class="reg m-cir">Type the characters you see in the image below</label>
                                             <div class="row">
                                                <div class="col-sm-12 col-xs-12">
                                                   <div data-captcha="" data-captcha-object="captchaObj">
                                                      <!---->
                                                      <table id="captchaTable">
                                                         <tbody>
                                                            <tr>
                                                               <th rowspan="2">
                                                                  <img id="imgCaptcha" src="../Captcha/189824.png">
                                                               </th>
                                                               <th>
                                                                  <button type="button" ng-click="play()" ng-disabled="playingCap"><i class="fa fa-volume-up"></i></button>
                                                               </th>
                                                            </tr>
                                                            <tr>
                                                               <td>
                                                                  <button type="button" ng-click="refreshCaptcha()" onclick="captchaChange();" ng-disabled="playingCap"><i class="fa fa-refresh"></i></button>
                                                               </td>
                                                            </tr>
                                                         </tbody>
                                                      </table>
                                                      <p class="err ng-hide" ng-show="captchaErr" data-ng-bind="captchaErr"></p>
                                                   </div>
                                                   <!---->
                                                   <!---->
                                                </div>
                                             </div>
                                             <input autocomplete="off" class="input-validation-error form-control" data-val="true" data-val-length="Captcha must be of 6 characters" data-val-length-max="6" data-val-length-min="6" data-val-required="Captcha number is required" id="NR_Captcha" maxlength="6" name="NR_Captcha" placeholder="Enter character displayed in the Captcha Image" type="text" value="">
                                             <span class="err"><span class="text-danger">Captcha number is required</span></span>
                                             <span>&nbsp;</span>
                                          </div>
                                       </div>
                                       <!---->
                                    </fieldset>
                                    <!---->
                                    <div class="row" style="margin-bottom:20px;">
                                       <div class="col-xs-12">
                                          <a href="/sp/GST-otp.aspx" class="btn-blue btn-block" style="color:#FFF !important;">Proceed</a>
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
                   
                    <!--***********registration SECTION end***************-->

                    

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

