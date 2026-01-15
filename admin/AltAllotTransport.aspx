<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="AltAllotTransport.aspx.cs" Inherits="admin_AltDeleteSrno" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">

<script type = "text/javascript">
    function Confirm() {
        var confirm_value = document.createElement("INPUT");
        confirm_value.type = "hidden";
        confirm_value.name = "confirm_value";
        if (confirm("Do you want to delete record?")) {
            confirm_value.value = "Yes";
        } else {
            confirm_value.value = "No";
        }
        document.forms[0].appendChild(confirm_value);
    }
    </script>

  <table width="100%">
  <tr align="right">
  <td>
  <asp:LinkButton ID="LinkButton1" runat="server" CssClass="button" onclick="LinkButton1_Click">Update All Records</asp:LinkButton>
  </td>
  </tr>
  </table>

   <asp:GridView ID="Grd" runat="server" AutoGenerateColumns="False" CssClass="Grid" Height="16px" Width="100%">
        <AlternatingRowStyle CssClass="alt" />
         <Columns>
         <asp:TemplateField HeaderText="#">
                <ItemTemplate>
                    <asp:Label ID="Label8" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="50px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="S.R. No.">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("srno") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="50px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Enrollment No.">
                <ItemTemplate>
                    <asp:Label ID="Label18" runat="server" Text='<%# Bind("StEnRCode") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Name">
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("FirstName") %>'></asp:Label>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("MiddleName") %>'></asp:Label>
                    <asp:Label ID="Label23" runat="server" Text='<%# Bind("LastName") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Father's Name">
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("FatherName") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Class">
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("ClassName") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="85px" />
                <ItemStyle Width="50px" HorizontalAlign="Left" VerticalAlign="Top" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Section">
                <ItemTemplate>
                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("SectionName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Medium">
                <ItemTemplate>
                    <asp:Label ID="Label27" runat="server" Text='<%# Bind("Medium") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Admission Date ">
                <ItemTemplate>
                    <asp:Label ID="Label7" runat="server" Text='<%# Bind("DateOfAdmiission") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Transport">
                <ItemTemplate>
                    <asp:Label ID="Label30" runat="server" Text='<%# Bind("TransportRequired") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:TemplateField>
        </Columns>
        
    </asp:GridView>
    
</asp:Content>

