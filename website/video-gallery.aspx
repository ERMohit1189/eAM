<%@ Page Title="" Language="C#" MasterPageFile="~/website/MasterPage.master" AutoEventWireup="true" CodeFile="video-gallery.aspx.cs" Inherits="website_video_gallery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:HiddenField ID="HiddenField1" runat="server" />
     <div id="page-main-content" class="main-content  col-md-9 col-lg-9 sb-r">

							<div class="main-content-inner">
										
									<div class="content-main">
										  <div class="region region-content">
    <div id="block-system-main" class="block block-system no-title">
  <div class="block-inner clearfix">


            <div class="panel panel-danger">
          <div class="panel-heading">VIDEO GALLERY</div>
          
           <div class="row">
                 

            

        <div class="col-lg-12 col-md-12 col-sm-12  ">
       
        
        
                <div class="pane-content">
        <div class="view view-gallery view-id-gallery view-display-id-gallery_v2 gallery-grid view-dom-id-d857a7b6595952743ad6f08ca03945bd">

      <div class="view-content">
      <div >

          <asp:Repeater ID="Repeater1" runat="server">
              <ItemTemplate>
                  <div class=" margin-bottom-30">
                      <div class="col-lg-6 col-md-6 col-sm-6  mar-bottom">
                          <div class="gallery-large img-box2 ">
                              <div class="breadcrumb" style="margin-bottom:0px">
                                <asp:Label ID="Label1" runat="server" CssClass="text-info"></asp:Label>
                              </div>
                              <div class="video-media video-responsive">
                                  <asp:Label ID="lblId" runat="server" Text='<%# Eval("Id") %>' Visible="false"></asp:Label>
                                  <br />
                                  
                                  <iframe runat="server" id="iframe1" width="500" height="300" frameborder="0" webkitallowfullscreen mozallowfullscreen allowfullscreen></iframe>
                              </div>
                          </div>
                      </div>
                  </div>
              </ItemTemplate>
          </asp:Repeater>
           
         
               
         
            </div>
    </div>      
       </div>
    </div> 
</div>
       </div>
    </div>  

    
  </div>
</div>	
</div>
</div>	
</div>
</div>
</asp:Content>

