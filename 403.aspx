<%@ Page Title="" Language="C#" MasterPageFile="~/blank.master" AutoEventWireup="true" CodeFile="403.aspx.cs" Inherits="_403" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">

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
                  <h1 class="font-semibold text-center" style="font-size:52px">403 Forbidden</h1>

                    
                    <div class="form-group">
                      <div class="col-md-12">
                        <h4 class="text-center mgbt-xs-20">You are not authorized to access this Page.</h4>
                        <p class="text-center"> Please <a href='<%= ResolveClientUrl("default.aspx") %>'>click here</a> to Log Out or let us know by sending an email to support@eam.co.in</p>
                        
                      </div>
                    </div>
                    
                 
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

