<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="CoscholasticAllotmentFor_VItoX.aspx.cs" Inherits="staff_CoscholasticAllotmentFor_VItoX" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
<table class="table" width="100%">
    <tr>
        <td>
            <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" >
                <asp:ListItem Selected="True">Class wise</asp:ListItem>
                <asp:ListItem Enabled="false">Student wise</asp:ListItem>
            </asp:RadioButtonList>
        </td>
    </tr>
      <tr>
      <td>
        Select Class: <span class="vd_red">*</span>
      </td>
        <td>
            <asp:DropDownList ID="drpClass" runat="server" AutoPostBack="True" 
                onselectedindexchanged="drpclass_SelectedIndexChanged" CssClass="textbox">
            </asp:DropDownList>
      </td>
      <td>
        Select Section:  <span class="vd_red">*</span>
      </td>
        <td>
            <asp:DropDownList ID="drpsection" runat="server" AutoPostBack="True" 
                onselectedindexchanged="drpsection_SelectedIndexChanged" CssClass="textbox">
            </asp:DropDownList>
      </td>
      </tr>
      <tr>
      <td>
          Medium:<span class="vd_red">*</span>
      </td>
      <td>
            <asp:DropDownList ID="drpMedium" runat="server" CssClass="textbox" AutoPostBack="True" 
                onselectedindexchanged="drpMedium_SelectedIndexChanged">
            </asp:DropDownList>
      </td>
      <td>
          Part Id:<span class="vd_red">*</span>
      </td>
      <td>
            <asp:DropDownList ID="drpPart" runat="server" CssClass="textbox" AutoPostBack="True" 
                onselectedindexchanged="drpdrpPart_SelectedIndexChanged">
                <asp:ListItem Value="9">Part 3(A)</asp:ListItem>
                <asp:ListItem Value="10">Part 3(B)</asp:ListItem>
                <asp:ListItem Value="3">Part 3(C)</asp:ListItem>
                <asp:ListItem Value="12">Part 3(D)</asp:ListItem>
            </asp:DropDownList>
      </td>     
      </tr>
    <tr>
        <td>Coscholastic Group:<span class="vd_red">*</span>
        </td>
        <td>
            <asp:DropDownList ID="drpCoscholasticGroup" runat="server" CssClass="textbox" AutoPostBack="True"
                OnSelectedIndexChanged="drpCoscholastic_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
    </tr>


       
      </table>
    <asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow"></asp:CheckBoxList>
     <table width="100%">
     <tr align="right">
     <td>
         <asp:LinkButton ID="lnkEditAll" runat="server" CssClass="button" Visible="false" onclick="lnkEditAll_Click">EditAll</asp:LinkButton>
         &nbsp;&nbsp;
         <asp:LinkButton ID="lnkSubmit" runat="server" CssClass="button" onclick="lnkSubmit_Click">Submit</asp:LinkButton>
     </td>
     </tr>
     </table>
      <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" CssClass="Grid" Width="100%">
    <Columns>
    <asp:TemplateField HeaderText="#">
    <ItemTemplate>
        <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
    </ItemTemplate>
    </asp:TemplateField>

     <asp:TemplateField HeaderText="S.R. No.">
    <ItemTemplate>
        <asp:Label ID="Label2" runat="server" Text='<%# Bind("srno") %>'></asp:Label>
    </ItemTemplate>
    </asp:TemplateField>

    <asp:TemplateField HeaderText="Name">
    <ItemTemplate>
        <asp:Label ID="Label3" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
    </ItemTemplate>
    </asp:TemplateField>

    <asp:TemplateField HeaderText="Father Name">
    <ItemTemplate>
        <asp:Label ID="Label4" runat="server" Text='<%# Bind("FatherName") %>'></asp:Label>
    </ItemTemplate>
    </asp:TemplateField>

    <asp:TemplateField HeaderText="">
    <HeaderTemplate>
        <asp:CheckBox ID="ChkAll" runat="server" OnCheckedChanged="ChkAll_CheckedChanged" AutoPostBack="true" />
    </HeaderTemplate>
    <ItemTemplate>
        <asp:CheckBox ID="Chk" runat="server" />
    </ItemTemplate>
    <ItemStyle HorizontalAlign="Center" />
    </asp:TemplateField>

    <asp:TemplateField HeaderText="Edit">
    <ItemTemplate>
        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" Width="16px" Height="16px" CssClass="edit" Font-Size="0px"></asp:LinkButton>
    </ItemTemplate>
    <ItemStyle HorizontalAlign="Center" />
    </asp:TemplateField>

    </Columns>
    </asp:GridView>
</asp:Content>

