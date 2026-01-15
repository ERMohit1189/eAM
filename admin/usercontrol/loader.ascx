<%@ Control Language="C#" AutoEventWireup="true" CodeFile="loader.ascx.cs" Inherits="admin.usercontrol.AdminUsercontrolLoader" %>
<style type="text/css">
    .modalPopup {
        background-color: #0c0101;
        filter: alpha(opacity=40);
        opacity: 0.7;
/* ReSharper disable once CssNotResolved */
        xindex: -1;
    }
</style>
<%-- Start Script --%>
<script type="text/javascript">
    //
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    //Raised before processing of an asynchronous postback starts and the postback request is sent to the server.
    prm.add_beginRequest(BeginRequestHandler);
    // Raised after an asynchronous postback is finished and control has been returned to the browser.
    prm.add_endRequest(EndRequestHandler);
    function BeginRequestHandler() {
        //Shows the modal popup - the update progress

// ReSharper disable once PossiblyUnassignedProperty
        var popup = window.$find('<%= UpdateProgress1_ModalPopupExtender.ClientID %>');
        if (popup != null) {
            popup.show();

        }
    }

    function EndRequestHandler() {
        //Hide the modal popup - the update progress
// ReSharper disable once PossiblyUnassignedProperty
        var popup = window.$find('<%= UpdateProgress1_ModalPopupExtender.ClientID %>');
        if (popup != null) {
            popup.hide();
        }
    }
</script>
<%-- End Script --%>

<style>
    .bubblingG {
	text-align: center;
	width:78px;
	height:49px;
	margin: auto;
    }

    .bubblingG span {
        display: inline-block;
	vertical-align: middle;
	width: 10px;
	height: 10px;
	margin: 24px auto;
	background: rgb(0,0,0);
        -o-border-radius: 49px;
		-ms-border-radius: 49px;
		-webkit-border-radius: 49px;
		-moz-border-radius: 49px;
        border-radius: 49px;
        -o-animation: bubblingG 1.5s infinite alternate;
		-ms-animation: bubblingG 1.5s infinite alternate;
		-webkit-animation: bubblingG 1.5s infinite alternate;
		-moz-animation: bubblingG 1.5s infinite alternate;
        animation: bubblingG 1.5s infinite alternate;
    }

    #bubblingG_1 {
        -o-animation-delay: 0;
		-ms-animation-delay: 0;
		-webkit-animation-delay: 0;
		-moz-animation-delay: 0;
        animation-delay: 0;
    }

    #bubblingG_2 {
        -o-animation-delay: 0.45s;
		-ms-animation-delay: 0.45s;
		-webkit-animation-delay: 0.45s;
		-moz-animation-delay: 0.45s;
        animation-delay: 0.45s;
    }

    #bubblingG_3 {
        -o-animation-delay: 0.9s;
		-ms-animation-delay: 0.9s;
		-webkit-animation-delay: 0.9s;
		-moz-animation-delay: 0.9s;
        animation-delay: 0.9s;
    }



    @keyframes bubblingG {
	0% {
		width: 10px;
		height: 10px;
		background-color:rgb(0,0,0);
		transform: translateY(0);
	}

	100% {
		width: 23px;
		height: 23px;
		background-color:rgb(255,255,255);
		transform: translateY(-20px);
	}
}

@-o-keyframes bubblingG {
	0% {
		width: 10px;
		height: 10px;
		background-color:rgb(0,0,0);
		-o-transform: translateY(0);
	}

	100% {
		width: 23px;
		height: 23px;
		background-color:rgb(255,255,255);
		-o-transform: translateY(-20px);
	}
}

@-ms-keyframes bubblingG {
	0% {
		width: 10px;
		height: 10px;
		background-color:rgb(0,0,0);
		-ms-transform: translateY(0);
	}

	100% {
		width: 23px;
		height: 23px;
		background-color:rgb(255,255,255);
		-ms-transform: translateY(-20px);
	}
}

@-webkit-keyframes bubblingG {
	0% {
		width: 10px;
		height: 10px;
		background-color:rgb(0,0,0);
		-webkit-transform: translateY(0);
	}

	100% {
		width: 23px;
		height: 23px;
		background-color:rgb(255,255,255);
		-webkit-transform: translateY(-20px);
	}
}

@-moz-keyframes bubblingG {
	0% {
		width: 10px;
		height: 10px;
		background-color:rgb(0,0,0);
		-moz-transform: translateY(0);
	}

	100% {
		width: 23px;
		height: 23px;
		background-color:rgb(255,255,255);
		-moz-transform: translateY(-20px);
	}
}
   
</style>

<%-- Start Progress Panel --%>
<div aling="center" id="show" runat="server">
    <table>
        <tr style="align-items: center">
            <td>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                    <ProgressTemplate>
                        <table>
                            <tr>
                                <td>
                                    <%-- <asp:Image ID="Image1" runat="server" AlternateText="Processing" ImageUrl="~/images/ajax-loader.gif" />--%>
                                    <%--<span class="vd_white" style="font-size:150px;">
                                        <i class="fa fa-spinner fa-spin"></i>
                                   </span>--%>
                                    <div class="bubblingG">
                                        <span id="bubblingG_1"></span>
                                        <span id="bubblingG_2"></span>
                                        <span id="bubblingG_3"></span>
                                    </div>
                                </td>
                            </tr>
                        </table>

                    </ProgressTemplate>
                </asp:UpdateProgress>
                <%-- ReSharper disable once Asp.InvalidControlType --%>
                <%-- ReSharper disable once Asp.InvalidControlType --%>
                <%-- ReSharper disable once Asp.InvalidControlType --%>
                <%-- ReSharper disable Asp.InvalidControlType --%>
                <ajaxToolkit:ModalPopupExtender ID="UpdateProgress1_ModalPopupExtender" runat="server" BackgroundCssClass="modalPopup" DynamicServicePath=""
                    Enabled="True" PopupControlID="UpdateProgress1" TargetControlID="UpdateProgress1">
                    <%-- ReSharper restore Asp.InvalidControlType --%>
                </ajaxToolkit:ModalPopupExtender>
            </td>
        </tr>
    </table>
</div>
<%-- End Progress Panel --%>