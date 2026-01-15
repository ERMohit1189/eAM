<%@ Page Title="" Language="C#" MasterPageFile="~/blank.master" AutoEventWireup="true" CodeFile="404.aspx.cs" Inherits="_404" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainBox" Runat="Server">

    <!-- Header Ends --> 
<div class="content">
  <div class="container"> 
    
    <!-- Middle Content Start -->
    
    <div class="vd_content-wrapper">
      <div class="vd_container">
        <div class="vd_content clearfix">
          <div class="vd_content-section clearfix">
            <div class="vd_register-page">
              <div class="heading clearfix">
                <div class="logo">
                  <h2 ><img src="img/logo.png" alt="logo"></h2>
                </div>
              </div>
              <%--<div class="panel widget">--%>
                <div class="panel-body" style="box-shadow:none;">
                  <%--<div class="login-icon"> <i class="fa fa-cog"></i> </div>--%>
                  <h1 class="font-semibold text-center" style="font-size:52px">404 Not Found</h1>
                  <form class="form-horizontal" action="#" role="form">
                    <div class="form-group">
                      <div class="col-md-12">
                        <h4 class="text-center mgbt-xs-20">Your requested page could not be found or it is currently unavailable.</h4>
                        <p class="text-center"> Please <a href='<%= ResolveClientUrl("default.aspx") %>'>click here</a> to go back to Dashboard.</p>
                        
                      </div>
                    </div>
                    
                  </form>
                </div>
             <%-- </div>--%>
              <!-- Panel Widget -->
              </div>
            <!-- vd_login-page --> 
            
          </div>
          <!-- .vd_content-section --> 
          
        </div>
        <!-- .vd_content --> 
      </div>
      <!-- .vd_container --> 
    </div>
    <!-- .vd_content-wrapper --> 
    
    <!-- Middle Content End --> 
    
  </div>
  <!-- .container --> 
</div>
<!-- .content -->

</asp:Content>

