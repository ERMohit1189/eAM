<%@ Page Title="" Language="C#" MasterPageFile="~/website/MasterPage.master" AutoEventWireup="true" CodeFile="JobApply.aspx.cs" Inherits="website_JobApply" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div role="main" class="main main-page" style="margin:30px 0px;">
<%--End Left Side--%>

		<div id="content" class="content content-full">
			
			<div class="container">
               
      <div class="panel panel-danger">
          <div class="panel-heading">CAREER APPLY</div>
        <div class="row">

         <div  class="main-content  col-md-12 col-lg-12 my-mar-top2">
          
            

    <div class="col-lg-8 col-md-8  ">						
         <div id="msgbox" runat="server" style="left: 147px !important;"></div>
       <div class=" myjustify">
          <div class="contact-form">
               <asp:TextBox ID="TextBox1" placeholder="Full Name" size="60" Class="my-txt-box" runat="server"></asp:TextBox>
               <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1"
                    Display="Dynamic" ErrorMessage="Please fill your name." CssClass="text-danger" SetFocusOnError="True" ValidationGroup="a"></asp:RequiredFieldValidator>
          
               <asp:TextBox ID="TextBox2" placeholder="Contact No." size="60" Class="my-txt-box" runat="server"></asp:TextBox>
               <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="TextBox2"
                    Display="Dynamic" ErrorMessage="Please enter 10 digit mobile No." CssClass="text-danger" ValidationExpression="\d{10}" SetFocusOnError="true" ValidationGroup="a"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox2"
                    Display="Dynamic" ErrorMessage="Please enter your mobile No." CssClass="text-danger" SetFocusOnError="True" ValidationGroup="a"></asp:RequiredFieldValidator>
           

               <asp:TextBox ID="TextBox3" placeholder="Your E-mail ID" size="60" Class="my-txt-box" runat="server"></asp:TextBox>
               <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBox3"
                    Display="Dynamic" ErrorMessage="Please fill your correct e-mail." CssClass="text-danger" SetFocusOnError="True" ValidationGroup="a"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TextBox3"
                    Display="Dynamic" ErrorMessage="Please fill correct e-mail." CssClass="text-danger" SetFocusOnError="True"
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="a"></asp:RegularExpressionValidator>
           

              <asp:TextBox ID="TextBox4" placeholder="Confirm E-mail ID" size="60" Class="my-txt-box" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TextBox4"
                    Display="Dynamic" ErrorMessage="Confirm e-mail." CssClass="text-danger" SetFocusOnError="True" ValidationGroup="a"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="TextBox3"
                    ControlToValidate="TextBox4" Display="Dynamic" CssClass="text-danger" ErrorMessage="E-mail are not same."
                    SetFocusOnError="True" ValueToCompare="a"></asp:CompareValidator>
            


               <asp:DropDownList ID="DropDownList1" Class="my-dropdown" runat="server">
                 <asp:ListItem Text="From Website" Value="From Website"></asp:ListItem>
                 <asp:ListItem Text="TV Ads" Value="TV Ads"></asp:ListItem>
                 <asp:ListItem Text="Outdoor Advertising" Value="Outdoor Advertisin"></asp:ListItem>
                 <asp:ListItem Text="Other" Value="Other"></asp:ListItem>
               </asp:DropDownList>

               

               <asp:DropDownList ID="DropDownList2" Class="my-dropdown" runat="server">
                 <asp:ListItem Text="Lecturer" Value="Lecturer"></asp:ListItem>
                 <asp:ListItem Text="Teachers" Value="Teachers"></asp:ListItem>
                 <asp:ListItem Text="Other" Value="Other"></asp:ListItem>
               </asp:DropDownList>

              <asp:TextBox ID="TextBox6" TextMode="MultiLine" placeholder="Something About You" Class="my-dropdown txt-ped"  rows="5" runat="server"></asp:TextBox>

                  <div>
                      <div class="my-div-box"><h4><small> Attach your CV PDF & DOC Format</small></h4></div>
                      <div class=" my-div-box"> 
                          <asp:FileUpload ID="FileUpload1" Class="my-txt-box txt-box"  runat="server" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="FileUpload1"
                    Display="Dynamic" ErrorMessage="C.V File Format is Invalid" CssClass="text-danger" ValidationExpression="^.+(.pdf|.PDF|.DOC|.doc|.docx|.DOCX)$" ValidationGroup="a"></asp:RegularExpressionValidator>
            
                      </div>
                   </div>
             <div > 
                 <asp:Button ID="Button1" runat="server" Text="Send Detail" 
                     class="webform-submit button-primary btn-primary btn form-submit " onclick="Button1_Click"  ValidationGroup="a" /></div>

                </div>
           </div>
          </div>
        

                
       <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6 lihide ">						
        

            &#13;
     <div data-ride="carousel" class="carousel slide carousel-shortcode" id="carousel-1238675029">&#13;
        <div class="carousel-inner">&#13;
           
&#13;
    <div class="item active">&#13;
      <img src="sites/default/files/career/career-1.jpg" alt="" />
	 <div class="carousel-caption"></div>&#13;
    </div>&#13;
   
&#13;
    <div class="item ">&#13;
      <img src="sites/default/files/career/career-2.jpg" alt="" /><div class="carousel-caption"></div>&#13;
    </div>&#13;
   
    &#13;
    <div class="item ">&#13;
      <img src="sites/default/files/career/career-3.jpg" alt="" /><div class="carousel-caption"></div>&#13;
    </div>&#13;
   
&#13;
        </div>&#13;
        <a data-slide="prev" href="#carousel-1238675029" class="left carousel-control"></a>&#13;
        <a data-slide="next" href="#carousel-1238675029" class="right carousel-control"></a>&#13;
      </div>  &#13;
  </div>  
  
</div>
    </div>
   
    </div>  
  </div>
</div>
    </div>
</asp:Content>

