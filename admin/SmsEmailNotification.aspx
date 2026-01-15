<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="SmsEmailNotification.aspx.cs" Inherits="SmsEmailNotification" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <style>
        .chk input[type=checkbox] {
            width: 15px !important;
            cursor:pointer;
        }
        .chk input[type=checkbox]:before {
  position: relative;
    display: block;
    width: 17px;
    height: 17px;
    /* border: 1px solid #808080; */
    content: "";
    background: #FFF;
}

.chk input[type=checkbox]:after {
  position: relative;
    display: block;
    le: 0;
    left: 1px;
    top: -16px;
    width: 16px;
    height: 16px;
    border-width: 1px;
    border-style: solid;
    border-color: #B3B3B3;
    content: "";
    background-image: linear-gradient(135deg, #FFF 0%, #FFF 100%);
    background-repeat: no-repeat;
    background-position: center;
}

.chk input[type=checkbox]:checked:after {
  background-image: url('data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAcAAAAHCAQAAABuW59YAAAACXBIWXMAAAsTAAALEwEAmpwYAAAAIGNIUk0AAHolAACAgwAA+f8AAIDpAAB1MAAA6mAAADqYAAAXb5JfxUYAAAB2SURBVHjaAGkAlv8A3QDyAP0A/QD+Dam3W+kCAAD8APYAAgTVZaZCGwwA5wr0AvcA+Dh+7UX/x24AqK3Wg/8nt6w4/5q71wAAVP9g/7rTXf9n/+9N+AAAtpJa/zf/S//DhP8H/wAA4gzWj2P4lsf0JP0A/wADAHB0Ngka6UmKAAAAAElFTkSuQmCC'), linear-gradient(135deg, #B1B6BE 0%, #FFF 100%);
}

.chk input[type=checkbox]:disabled:after {
  -webkit-filter: opacity(0.4);
}

.chk input[type=checkbox]:not(:disabled):checked:hover:after {
  background-image: url('data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAcAAAAHCAQAAABuW59YAAAACXBIWXMAAAsTAAALEwEAmpwYAAAAIGNIUk0AAHolAACAgwAA+f8AAIDpAAB1MAAA6mAAADqYAAAXb5JfxUYAAAB2SURBVHjaAGkAlv8A3QDyAP0A/QD+Dam3W+kCAAD8APYAAgTVZaZCGwwA5wr0AvcA+Dh+7UX/x24AqK3Wg/8nt6w4/5q71wAAVP9g/7rTXf9n/+9N+AAAtpJa/zf/S//DhP8H/wAA4gzWj2P4lsf0JP0A/wADAHB0Ngka6UmKAAAAAElFTkSuQmCC'), linear-gradient(135deg, #8BB0C2 0%, #FFF 100%);
}

.chk input[type=checkbox]:not(:disabled):hover:after {
  background-image: linear-gradient(135deg, #8BB0C2 0%, #FFF 100%);
  border-color: #85A9BB #92C2DA #92C2DA #85A9BB;
}

.chk input[type=checkbox]:not(:disabled):hover:before {
  border-color: #3D7591;
}

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>

    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-lg-12 no-padding">
                                    <div class="col-lg-12 no-padding">
                                        <div class=" table-responsive  table-responsive2">
                                            <asp:GridView ID="Grd" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-hover no-bm no-head-border table-bordered text-center">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="#" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblsrno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="40px" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Title" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTitle" runat="server" Text='<%# Bind("SmsTitle") %>'></asp:Label>
                                                            <asp:Label ID="lblId" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SMS Alert" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:CheckBox runat="server" ID="chkSmsSent" CssClass="chk" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="250px" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Email Alert" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:CheckBox runat="server" ID="chkEmailSent" CssClass="chk" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="250px" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <div class="col-lg-12 no-padding text-center">
                                        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="button form-control-blue" OnClick="LinkButton1_Click">Update</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 76px;"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

