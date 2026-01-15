<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="PeriodAllotment.aspx.cs" Inherits="admin_AllotPeriod" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
   <style type="text/css">
.modalPopup
{
background-color: #696969;
filter: alpha(opacity=40);
opacity: 0.7;
xindex:-1;
}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">

<script type="text/javascript">

    var prm = Sys.WebForms.PageRequestManager.getInstance();
    //Raised before processing of an asynchronous postback starts and the postback request is sent to the server.
    prm.add_beginRequest(BeginRequestHandler);
    // Raised after an asynchronous postback is finished and control has been returned to the browser.
    prm.add_endRequest(EndRequestHandler);
    function BeginRequestHandler(sender, args) {
        //Shows the modal popup - the update progress
        var popup1 = args.get_postBackElement();
        if (popup1 != null) {
            var popup = $find('<%= UpdateProgress1_ModalPopupExtender.ClientID %>');
            if (popup != null) {
                popup.show();
            }
        }
    }

    function EndRequestHandler(sender, args) {
        //Hide the modal popup - the update progress
        var popup = $find('<%= UpdateProgress1_ModalPopupExtender.ClientID %>');
        if (popup != null) {
            popup.hide();
        }
    }
</script>
<div aling="center" id="show" runat="server">
<table>
      <tr align="center">
      <td>
         <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                <ProgressTemplate>
                 <span style="color:White">
                     <asp:Label ID="Label2" runat="server" Text="Please Wait....."></asp:Label></span>
                   <br />
                <asp:Image ID="Image1" runat="server" AlternateText="Processing" ImageUrl="~/SuperAdmin/images/waiting.gif" />
                </ProgressTemplate>
            </asp:UpdateProgress>
            <asp:ModalPopupExtender ID="UpdateProgress1_ModalPopupExtender" runat="server" BackgroundCssClass="modalPopup" DynamicServicePath=""
                Enabled="True" PopupControlID="UpdateProgress1" TargetControlID="UpdateProgress1">
            </asp:ModalPopupExtender>
      </td>
      </tr>
</table>
   
</div>
    <table class="table">
 <tr>
  <td>
  Select <span class="vd_red">*</span>
  </td>
  <td>
      <asp:DropDownList ID="drpEnter" runat="server" CssClass="textbox" Width="200">
          <asp:ListItem>EmpId</asp:ListItem>
          <asp:ListItem>EmpCode</asp:ListItem>
      </asp:DropDownList> 
  </td>
  <td>
  Enter <span class="vd_red">*</span>
  </td>
  <td>
      <asp:TextBox ID="txtEnter" runat="server"  CssClass="textbox" Width="180" onKeyUp="" ></asp:TextBox> 
  </td>
   <td>
       <asp:LinkButton ID="lnkShow" runat="server" CssClass="button" onclick="lnkShow_Click">Show</asp:LinkButton>
  </td>
  </tr>
</table>
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>--%>
 
<div runat="server" id="div1">
    
<asp:GridView ID="Grd" runat="server" AutoGenerateColumns="false" CssClass="Grid" Width="100%">
      <Columns>
       <asp:TemplateField HeaderText="#">
      <ItemTemplate>
          <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
      </ItemTemplate>
      </asp:TemplateField>
       <asp:TemplateField HeaderText="Emp Code">
      <ItemTemplate>
          <asp:Label ID="lblEcode" runat="server" Text='<%# Bind("Ecode") %>'></asp:Label>
      </ItemTemplate>
      </asp:TemplateField>
       <asp:TemplateField HeaderText="Emp Id">
      <ItemTemplate>
          <asp:Label ID="lblEmpId" runat="server" Text='<%# Bind("EmpId") %>'></asp:Label>
      </ItemTemplate>
      </asp:TemplateField>
       <asp:TemplateField HeaderText="Name">
      <ItemTemplate>
          <asp:Label ID="lblEmpName" runat="server" Text='<%# Bind("EmpName") %>'></asp:Label>
      </ItemTemplate>
      </asp:TemplateField>
       <asp:TemplateField HeaderText="Father Name">
      <ItemTemplate>
          <asp:Label ID="lblFName" runat="server" Text='<%# Bind("FatherName") %>'></asp:Label>
      </ItemTemplate>
      </asp:TemplateField>
        <asp:TemplateField HeaderText="Designation">
      <ItemTemplate>
          <asp:Label ID="lblDesi" runat="server" Text='<%# Bind("Designation") %>'></asp:Label>
      </ItemTemplate>
      </asp:TemplateField>
      </Columns>
      </asp:GridView>
      <table class="table">
        <tr>
        <td>
        <asp:Label ID="Label3" runat="server" Text="Select Medium : "></asp:Label>
        </td>
            <td>
                
               <asp:DropDownList ID="drpMedium" runat="server" AutoPostBack="true" CssClass="textbox"></asp:DropDownList>
                </td>
            </tr>
            <tr>
              <td>
        <asp:Label ID="Label4" runat="server" Text="Select Type : "></asp:Label>
        </td>
            <td>
                <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal" 
                    onselectedindexchanged="RadioButtonList1_SelectedIndexChanged" AutoPostBack="True">
                    <asp:ListItem>For Boys</asp:ListItem>
                    <asp:ListItem>For Girls</asp:ListItem>
                    <asp:ListItem>Combined</asp:ListItem>
                </asp:RadioButtonList>
                </td>
            </tr>
            
      </table>
      <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" CssClass="Grid" Width="100%" >
    <Columns>
    <asp:TemplateField HeaderText="">
     <HeaderTemplate>
       
              <asp:CheckBox ID="ChkAll" runat="server" OnCheckedChanged="ChkAll_OnCheckedChanged" AutoPostBack="true" />
       
    </HeaderTemplate>
    <ItemTemplate>
      
                    <asp:CheckBox ID="Chk" runat="server" OnCheckedChanged="Chk_OnCheckedChanged" AutoPostBack="true"  />
         
    </ItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField HeaderText="Period">
    <%--<HeaderTemplate>
        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
    </HeaderTemplate>--%>
    <ItemTemplate>
     
                <asp:Label ID="lblPeriodName" runat="server" Text='<%# Bind("PeriodName") %>'></asp:Label>
      
    </ItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField HeaderText="MONDAY">
     <%--  <HeaderTemplate>
        <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
    </HeaderTemplate>--%>
    <ItemTemplate>
      
                <asp:DropDownList ID="DropDownList7" Enabled="false" runat="server" CssClass="textbox" OnSelectedIndexChanged="DropDownList7_SelectedIndexChanged" AutoPostBack="True">
                       </asp:DropDownList>
  <asp:DropDownList ID="DropDownList13" runat="server" Enabled="false" CssClass="textbox" OnSelectedIndexChanged="DropDownList13_SelectedIndexChanged" AutoPostBack="True">
        </asp:DropDownList>
       
       
               <asp:DropDownList ID="DropDownList1" Enabled="false" runat="server" CssClass="textbox" >
        </asp:DropDownList> 
      
       
    </ItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField HeaderText="TUESDAY">
    <%--  <HeaderTemplate>
        <asp:Label ID="Label4" runat="server" Text="" ></asp:Label>
    </HeaderTemplate>--%>
    <ItemTemplate>
      
                  <asp:DropDownList ID="DropDownList8" Enabled="false" runat="server" CssClass="textbox" OnSelectedIndexChanged="DropDownList8_SelectedIndexChanged" AutoPostBack="True">
                     </asp:DropDownList>
                           <asp:DropDownList ID="DropDownList14" Enabled="false"  runat="server" CssClass="textbox" OnSelectedIndexChanged="DropDownList14_SelectedIndexChanged" AutoPostBack="True">
        </asp:DropDownList>
                         <asp:DropDownList ID="DropDownList2" Enabled="false" runat="server" CssClass="textbox" >
        </asp:DropDownList>
                    
       
    </ItemTemplate>
   
    </asp:TemplateField>
    <asp:TemplateField HeaderText="WEDNESDAY">
      <%--<HeaderTemplate>
        <asp:Label ID="Label5" runat="server" Text=""></asp:Label>
    </HeaderTemplate>--%>
    <ItemTemplate>
       
              <asp:DropDownList ID="DropDownList9" Enabled="false" runat="server" CssClass="textbox" OnSelectedIndexChanged="DropDownList9_SelectedIndexChanged" AutoPostBack="True">
               </asp:DropDownList>
                       <asp:DropDownList ID="DropDownList15" Enabled="false"  runat="server" CssClass="textbox" OnSelectedIndexChanged="DropDownList15_SelectedIndexChanged" AutoPostBack="True">
        </asp:DropDownList>
       
                    <asp:DropDownList ID="DropDownList3" Enabled="false" runat="server" CssClass="textbox">
        </asp:DropDownList>
       
    </ItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField HeaderText="THURSDAY">
     <%--  <HeaderTemplate>
        <asp:Label ID="Label6" runat="server" Text=""></asp:Label>
    </HeaderTemplate>--%>
    <ItemTemplate>
      
            <asp:DropDownList ID="DropDownList10" Enabled="false" runat="server" CssClass="textbox" OnSelectedIndexChanged="DropDownList10_SelectedIndexChanged" AutoPostBack="True">
        </asp:DropDownList>
                         <asp:DropDownList ID="DropDownList16" Enabled="false"  runat="server" CssClass="textbox" OnSelectedIndexChanged="DropDownList16_SelectedIndexChanged" AutoPostBack="True">
        </asp:DropDownList>
        
                   <asp:DropDownList ID="DropDownList4" Enabled="false" runat="server" CssClass="textbox">
        </asp:DropDownList>
       
    </ItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField HeaderText="FRIDAY">
      <%-- <HeaderTemplate>
        <asp:Label ID="Label7" runat="server" Text=""></asp:Label>
    </HeaderTemplate>--%>
    <ItemTemplate>
      
                 <asp:DropDownList ID="DropDownList11" Enabled="false" runat="server" CssClass="textbox" OnSelectedIndexChanged="DropDownList11_SelectedIndexChanged" AutoPostBack="True">
        </asp:DropDownList>
                  <asp:DropDownList ID="DropDownList17" Enabled="false"  runat="server" CssClass="textbox" OnSelectedIndexChanged="DropDownList17_SelectedIndexChanged" AutoPostBack="True">
        </asp:DropDownList>
                 <asp:DropDownList ID="DropDownList5" Enabled="false" runat="server" CssClass="textbox">
        </asp:DropDownList>
       
        
    </ItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField HeaderText="SATURDAY">
      <%-- <HeaderTemplate>
        <asp:Label ID="Label8" runat="server" Text=""></asp:Label>
    </HeaderTemplate>--%>
    <ItemTemplate>
      
                         <asp:DropDownList ID="DropDownList12" Enabled="false" runat="server" CssClass="textbox" OnSelectedIndexChanged="DropDownList12_SelectedIndexChanged" AutoPostBack="True">
        </asp:DropDownList>
                     <asp:DropDownList ID="DropDownList18" Enabled="false"  runat="server" CssClass="textbox" OnSelectedIndexChanged="DropDownList18_SelectedIndexChanged" AutoPostBack="True">
        </asp:DropDownList>
                 <asp:DropDownList ID="DropDownList6" Enabled="false" runat="server" CssClass="textbox">
        </asp:DropDownList>
     
       
    </ItemTemplate>
    </asp:TemplateField>
    </Columns>
    </asp:GridView>
         <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false" CssClass="Grid" Width="100%" 
         >
    <Columns>
    <asp:TemplateField HeaderText="Period">
    <%--<HeaderTemplate>
        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
    </HeaderTemplate>--%>
    <ItemTemplate>
     
                <asp:Label ID="lblPeriodName" runat="server" Text='<%# Bind("PeriodName") %>'></asp:Label>
                <asp:Label ID="lblPeriodTime" runat="server" Text='<%# Bind("PeriodTime") %>'></asp:Label>
    </ItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField HeaderText="MONDAY">
     <%--  <HeaderTemplate>
        <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
    </HeaderTemplate>--%>
    <ItemTemplate>
      
        <asp:Label ID="Label2" runat="server" ></asp:Label>
        <asp:Label ID="Label5" runat="server" ></asp:Label>
        <asp:Label ID="Label16" runat="server"></asp:Label>
        <asp:Label ID="Label22" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="Label28" runat="server" Visible="false"></asp:Label>
    </ItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField HeaderText="TUESDAY">
    <%--  <HeaderTemplate>
        <asp:Label ID="Label4" runat="server" Text="" ></asp:Label>
    </HeaderTemplate>--%>
    <ItemTemplate>
      
        <asp:Label ID="Label6" runat="server"></asp:Label>
        <asp:Label ID="Label7" runat="server" ></asp:Label>
        <asp:Label ID="Label17" runat="server"></asp:Label>
        <asp:Label ID="Label23" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="Label29" runat="server" Visible="false"></asp:Label>
    </ItemTemplate>
   
    </asp:TemplateField>
    <asp:TemplateField HeaderText="WEDNESDAY">
      <%--<HeaderTemplate>
        <asp:Label ID="Label5" runat="server" Text=""></asp:Label>
    </HeaderTemplate>--%>
    <ItemTemplate>
       
        <asp:Label ID="Label8" runat="server"></asp:Label>
        <asp:Label ID="Label9" runat="server" ></asp:Label>
        <asp:Label ID="Label18" runat="server"></asp:Label>
        <asp:Label ID="Label24" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="Label30" runat="server" Visible="false" ></asp:Label>
    </ItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField HeaderText="THURSDAY">
     <%--  <HeaderTemplate>
        <asp:Label ID="Label6" runat="server" Text=""></asp:Label>
    </HeaderTemplate>--%>
    <ItemTemplate>
      
        <asp:Label ID="Label10" runat="server" ></asp:Label>
        <asp:Label ID="Label11" runat="server" ></asp:Label>
        <asp:Label ID="Label19" runat="server"></asp:Label>
        <asp:Label ID="Label25" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="Label31" runat="server" Visible="false"></asp:Label>
    </ItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField HeaderText="FRIDAY">
      <%-- <HeaderTemplate>
        <asp:Label ID="Label7" runat="server" Text=""></asp:Label>
    </HeaderTemplate>--%>
    <ItemTemplate>
      
        <asp:Label ID="Label12" runat="server" ></asp:Label>
        <asp:Label ID="Label13" runat="server" ></asp:Label>
        <asp:Label ID="Label20" runat="server"></asp:Label>
        <asp:Label ID="Label26" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="Label32" runat="server" Visible="false"></asp:Label>
        
    </ItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField HeaderText="SATURDAY">
      <%-- <HeaderTemplate>
        <asp:Label ID="Label8" runat="server" Text=""></asp:Label>
    </HeaderTemplate>--%>
    <ItemTemplate>
      
        <asp:Label ID="Label14" runat="server" ></asp:Label>
        <asp:Label ID="Label15" runat="server" ></asp:Label>
        <asp:Label ID="Label21" runat="server" ></asp:Label>
        <asp:Label ID="Label27" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="Label33" runat="server" Visible="false" ></asp:Label>
       
    </ItemTemplate>
    </asp:TemplateField>
    </Columns>
    </asp:GridView>
    <table class="table" width="100%">
    <tr align="center">
    <td>
        <asp:LinkButton ID="lnkSubmit" runat="server" CssClass="button" onclick="lnkSubmit_Click" CausesValidation="False">Submit</asp:LinkButton>
    </td>
    </tr>
    </table>
    <asp:GridView ID="Grd1" runat="server" CssClass="Grid" Width="100%" AutoGenerateColumns="false" 
            onprerender="Grd1_PreRender">
    <Columns>
       <asp:TemplateField HeaderText="#">
      <ItemTemplate>
          <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
      </ItemTemplate>
      </asp:TemplateField>
    <%--   <asp:TemplateField HeaderText="Emp Code">
      <ItemTemplate>
          <asp:Label ID="lblEcode1" runat="server" Text='<%# Eval("Ecode") %>'></asp:Label>
      </ItemTemplate>
      </asp:TemplateField>--%>
       <asp:TemplateField HeaderText="Emp Id">
      <ItemTemplate>
          <asp:Label ID="lblEmpId1" runat="server" Text='<%# Eval("EmpId") %>'></asp:Label>
      </ItemTemplate>
      </asp:TemplateField>
       <asp:TemplateField HeaderText="Name">
      <ItemTemplate>
          <asp:Label ID="lblEmpName1" runat="server" Text='<%# Bind("EmpName") %>'></asp:Label>
      </ItemTemplate>
      </asp:TemplateField>
      
      
       <asp:TemplateField HeaderText="Medium">
      <ItemTemplate>
          <asp:Label ID="lblMedium" runat="server" Text='<%# Bind("Medium") %>'></asp:Label>
      </ItemTemplate>
      </asp:TemplateField>
     
        <asp:TemplateField HeaderText="Type" Visible="false">
      <ItemTemplate>
          <asp:Label ID="lblType" runat="server" Text='<%# Bind("Type") %>'></asp:Label>
      </ItemTemplate>
      </asp:TemplateField>
        <asp:TemplateField HeaderText="Period">
      <ItemTemplate>
          <asp:Label ID="lblPeriod" runat="server" Text='<%# Bind("PeriodName") %>'></asp:Label>
      </ItemTemplate>
      </asp:TemplateField>
       <asp:TemplateField HeaderText="Time">
      <ItemTemplate>
          <asp:Label ID="lblTime" runat="server" Text='<%# Bind("Time") %>'></asp:Label>
      </ItemTemplate>
      </asp:TemplateField>
        <asp:TemplateField HeaderText="ClassName">
      <ItemTemplate>
          <asp:Label ID="lblClassName" runat="server" Text='<%# Bind("ClassName") %>'></asp:Label>
      </ItemTemplate>
      </asp:TemplateField> 
        <asp:TemplateField HeaderText="SectionName">
      <ItemTemplate>
          <asp:Label ID="lblSectionName" runat="server" Text='<%# Bind("SectionName") %>'></asp:Label>
      </ItemTemplate>
      </asp:TemplateField>    
        <asp:TemplateField HeaderText="Subject Name">
      <ItemTemplate>
          <asp:Label ID="lblSubjectName" runat="server" Text='<%# Bind("SubjectName") %>'></asp:Label>
      </ItemTemplate>
      </asp:TemplateField> 
      
       
       <asp:TemplateField HeaderText="Day">
      <ItemTemplate>
          <asp:Label ID="lblDays" runat="server"></asp:Label>
      </ItemTemplate>
      </asp:TemplateField>
       <asp:TemplateField HeaderText="Duration">                   
      <ItemTemplate>
          <asp:Label ID="lblDuration" runat="server" Text='<%# Bind("Duration") %>'></asp:Label>
      </ItemTemplate>
      </asp:TemplateField>
       
      </Columns>
    <RowStyle VerticalAlign="Top" />
    </asp:GridView>
    <div style="overflow: auto; width: 1px; height: 1px">
        <asp:Panel ID="Panel2" runat="server" CssClass="popup">
            <table width="100%">
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <h4>
                            Do you really want to allot continuasly more than three period?
                         <%--   <br />
                            Do not disturbed dgain!.
                            <asp:RadioButtonList ID="RadioButtonList2" runat="server">
                            <asp:ListItem>Yes</asp:ListItem>
                            <asp:ListItem>No</asp:ListItem>
                            </asp:RadioButtonList>--%>
                            <asp:Label ID="lblvalue" runat="server" Visible="False"></asp:Label>
                            <asp:Button ID="Button7" runat="server" Style="display: none" />
                        </h4>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Button ID="btnYes" runat="server" CssClass="button" Text="Yes" />
                        &nbsp;
                        <asp:Button ID="btnNo" runat="server" OnClick="btnNo_Click" CausesValidation="False" CssClass="button" Text="No" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server" DynamicServicePath="" Enabled="True" TargetControlID="Button7"
            PopupControlID="Panel2" CancelControlID="btnYes" BackgroundCssClass="popup_bg" BehaviorID="Panel2_ModalPopupExtender_Close">
        </asp:ModalPopupExtender>
    </div>
      
    
    
    
    
    </div>
  <%--  </ContentTemplate>
    </asp:UpdatePanel>  --%>
</asp:Content>

