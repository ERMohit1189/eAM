<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="ap_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8" />
    <title>Admission Portal | eAM ®</title>
    <meta http-equiv="content-type" content="text/html;charset=UTF-8" />

    <!-- Set the viewport width to device width for mobile -->
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <!-- Fav and touch icons -->
    <link rel="apple-touch-icon-precomposed" sizes="114x114" href="<%# ResolveUrl("~/img/ico/apple-touch-icon-114-precomposed.png") %>" />
    <link rel="apple-touch-icon-precomposed" sizes="72x72" href="<%# ResolveUrl("~/img/ico/apple-touch-icon-72-precomposed.png") %>" />
    <link rel="apple-touch-icon-precomposed" href="<%# ResolveUrl("~/img/ico/apple-touch-icon-57-precomposed.png") %>" />
    <link rel="shortcut icon" href="<%# ResolveUrl("~/img/ico/favicon.png") %>">
    <script src="<%# ResolveUrl("~/js/MyScript.js") %>"></script>

    <!-- CSS -->

    <!-- Bootstrap & FontAwesome & Entypo CSS -->
    <link href="<%# ResolveUrl("~/css/bootstrap.min.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%# ResolveUrl("~/css/font-awesome.min.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%# ResolveUrl("~/css/font-entypo.css") %>" rel="stylesheet" type="text/css" />

    <!-- Fonts CSS -->
    <link href="<%# ResolveUrl("~/css/fonts.css") %>" rel="stylesheet" type="text/css" />

    <!-- Plugin CSS -->
    <link href="<%# ResolveUrl("~/plugins/jquery-ui/jquery-ui.custom.min.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%# ResolveUrl("~/plugins/prettyPhoto-plugin/css/prettyPhoto.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%# ResolveUrl("~/plugins/isotope/css/isotope.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%# ResolveUrl("~/plugins/pnotify/css/jquery.pnotify.css") %>" media="screen" rel="stylesheet" type="text/css" />
    <link href="<%# ResolveUrl("~/plugins/google-code-prettify/prettify.css") %>" rel="stylesheet" type="text/css" />


    <link href="<%# ResolveUrl("~/plugins/mCustomScrollbar/jquery.mCustomScrollbar.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%# ResolveUrl("~/plugins/tagsInput/jquery.tagsinput.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%# ResolveUrl("~/plugins/bootstrap-switch/bootstrap-switch.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%# ResolveUrl("~/plugins/daterangepicker/daterangepicker-bs3.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%# ResolveUrl("~/plugins/bootstrap-timepicker/bootstrap-timepicker.min.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%# ResolveUrl("~/plugins/colorpicker/css/colorpicker.css") %>" rel="stylesheet" type="text/css" />

    <link href="<%# ResolveUrl("~/css/summernote/summernote.css") %>" rel="stylesheet" type="text/css" />
    <!-- Specific CSS -->
    <link href="<%# ResolveUrl("~/plugins/jquery-file-upload/css/jquery.fileupload.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%# ResolveUrl("~/plugins/jquery-file-upload/css/jquery.fileupload-ui.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%# ResolveUrl("~/plugins/bootstrap-wysiwyg/css/bootstrap-wysihtml5-0.0.2.css") %>" rel="stylesheet" type="text/css" />

    <!-- Specific CSS -->
    <link href="<%# ResolveUrl("~/plugins/fullcalendar/fullcalendar.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%# ResolveUrl("~/plugins/fullcalendar/fullcalendar.print.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%# ResolveUrl("~/plugins/introjs/css/introjs.min.css") %>" rel="stylesheet" type="text/css" />
    <!-- Specific CSS -->

    <!-- Theme CSS -->
    <link href="<%# ResolveUrl("~/css/theme.min.css") %>" rel="stylesheet" type="text/css" />

    <link href="<%# ResolveUrl("~/css/chrome.css") %>" rel="stylesheet" type="text/chrome" />
    <!-- chrome only css -->



    <!-- Responsive CSS -->
    <link href="<%# ResolveUrl("~/css/theme-responsive.min.css") %>" rel="stylesheet" type="text/css" />

    <link href="<%# ResolveUrl("~/css/summernote/summernote.css") %>" rel="stylesheet" />


    <!-- for specific page in style css -->

    <!-- for specific page responsive in style css -->


    <!-- Custom CSS -->
    <link href="<%# ResolveUrl("~/custom/custom.css") %>" rel="stylesheet" type="text/css" />

    <link href="<%# ResolveUrl("~/Smoke/smoke.css") %>" rel="stylesheet" />
    <script src="<%# ResolveUrl("~/Smoke/smoke.min.js") %>"></script>
    <script src="<%# ResolveUrl("~/Smoke/smoke.js") %>"></script>

    <!-- Head SCRIPTS -->
    <script type="text/javascript" src="<%# ResolveUrl("~/js/modernizr.js") %>"></script>
    <script type="text/javascript" src="<%# ResolveUrl("~/js/mobile-detect.min.js") %>"></script>
    <script type="text/javascript" src="<%# ResolveUrl("~/js/mobile-detect-modernizr.js") %>"></script>
    <script type="text/javascript" src="<%# ResolveUrl("~/js/modernizr.js") %>"></script>
    <script type="text/javascript" src="<%# ResolveUrl("~/js/MyScript.js") %>"></script>
    <script>
        function activeonLoad() {
            //Get browser url
            var url = window.location.href;

            var classActive = document.querySelectorAll(".activeit");
            //Get all active class <a>
            for (var i = 0; i < classActive.length; i++) {
                // match active class url with browser url
                if (classActive[i].href === url) {
                    classActive[i].style.background = "rgb(73, 122, 151)";
                }
            }
        }
    </script>
    <script>
        function popuphide(myModal) {
            document.getElementById(myModal).style.display = "none";
            return false;
        }
    </script>
    <script>
        var specialKeys = new Array();
        specialKeys.push(8); //Backspace
        function IsNumeric(e) {
            var keyCode = e.which ? e.which : e.keyCode;
            var ret = ((keyCode >= 48 && keyCode <= 57) || specialKeys.indexOf(keyCode) !== -1);

            return ret;
        }

        function ClearTextboxes() {
            document.getElementById('ContentPlaceHolderMainBox_txtregmobile').value = '';
            document.getElementById('ContentPlaceHolderMainBox_txtregpassword').value = '';
        }
    </script>
    <style>
        .form-group {
            margin-right: -30px !important;
            margin-left: -30px !important;
            margin-bottom: 8px !important;
        }
    </style>
    <link href="<%# ResolveUrl("~/css/animate.css") %>" rel="stylesheet" />
</head>
<body runat="server" id="pages" class="full-layout no-nav-left no-nav-right nav-top-fixed background-login responsive remove-navbar login-layout clearfix" data-active="pages" data-smooth-scrolling="1" style="background-image: url(../Uploads/LoginWallpaper/eAM_Default_bg.png); background-size: 100%;">
    <form id="form2" runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <div class="vd_body">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div class="vd_body">
                        <div class="content">
                            <div class="container">
                                <div class="">
                                    <div class="vd_container">
                                        <div class="vd_content clearfix">
                                            <div class=" clearfix">

                                                <div class="vd_login-page">

                                                    <div class="panel widget">

                                                        <div class="panel-body" style="border-radius: 10px; padding: 10px 20px;">
                                                            <div class="login-img entypo-icon">
                                                                <div class="main-logo-center">
                                                                    <img src="../img/logo.png" alt="eAM logo" />
                                                                </div>
                                                            </div>

                                                            <div class="form-horizontal">
                                                                <div class="col-md-12">
                                                                    <div class="form-group">
                                                                        <div class="col-md-12">
                                                                            <div class="vd_input-wrapper">
                                                                                <span class="menu-icon"><i class="fa fa-map-marker"></i></span>
                                                                                <asp:DropDownList ID="DrpBranchName" runat="server" CssClass="select-box form-control-blue" AutoPostBack="true" OnSelectedIndexChanged="DrpBranchName_SelectedIndexChanged">
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    
                                                                </div>
                                                                <div class="col-md-12 text-center mgbt-xs-5" runat="server" id="divmsg" visible="false">
                                                                    <div id="msgbox" class="alert alert-danger" runat="server" style="left: -10px !important; width: 300px; margin-top: 10px; padding: 5px !important;"></div>
                                                                </div>
                                                                <div class="col-md-12 text-center mgbt-xs-5" runat="server" id="divRegister" visible="false">
                                                                    <asp:LinkButton ID="linkRegister" runat="server"  style="text-decoration: underline; color: #23709e !important;" OnClick="linkRegister_Click" OnClientClick="ValidateTextBox('.validatetxt');return validationReturn(this);">Click here to register</asp:LinkButton>
                                                                </div>
                                                                <div class="col-md-12 text-center mgbt-xs-5" runat="server" id="divLogin" visible="false">
                                                                    <asp:Label ID="dd" runat="server">Already have an account?&nbsp;<asp:LinkButton ID="linkLogin" runat="server"  style="text-decoration: underline; color: #23709e !important;" OnClick="linkLogin_Click" OnClientClick="ValidateTextBox('.validatetxt');return validationReturn(this);">Click here to Sign In</asp:LinkButton></asp:Label>
                                                                </div>
                                                                <div class="col-md-12 text-center mgbt-xs-5">
                                                                    <asp:Label ID="Label1" runat="server">For help and support&nbsp;<asp:HyperLink ID="LinkButton1" runat="server"  style="text-decoration: underline; color: #23709e !important;" NavigateUrl="~/ap/Contact_Us.aspx">Contact Us</asp:HyperLink></asp:Label>
                                                                </div>
                                                                <div class="col-md-12 text-center mgbt-xs-5">
                                                                </div>
                                                                
                                                                <div class="col-md-12 text-center mgbt-xs-5">
                                                                    <p class="login-footer-title vd_black-new" runat="server" id="lblCompanyName" visible="false" style="margin-bottom: 0px !important; color: #333  !important; border-top: 1px solid #333;">
                                                                        <asp:Label ID="Label2" runat="server"></asp:Label>
                                                                    </p>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>
                                                <!-- .vd_content-section -->

                                            </div>
                                            <!-- .vd_content -->
                                        </div>
                                        <!-- .vd_container -->
                                    </div>

                                    

                                </div>
                                <!-- .container -->
                            </div>
                            <!-- .content -->
                        </div>
                        </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>

    <!-- .vd_body END  -->
    <a id="back-top" href="../#" data-action="backtop" class="vd_back-top visible"><i class="fa  fa-angle-up"></i></a>
    <script type="text/javascript" src='<%= ResolveClientUrl("~/js/jquery.js") %>'></script>

    <script type="text/javascript" src='<%= ResolveClientUrl("~/js/bootstrap.min.js") %>'></script>
    <script type="text/javascript" src='<%= ResolveClientUrl("~/plugins/jquery-ui/jquery-ui.custom.min.js") %>'></script>
    <script type="text/javascript" src='<%= ResolveClientUrl("~/plugins/jquery-ui-touch-punch/jquery.ui.touch-punch.min.js") %>'></script>

    <script type="text/javascript" src='<%= ResolveClientUrl("~/js/caroufredsel.js") %>'></script>
    <script type="text/javascript" src='<%= ResolveUrl("~/js/plugins.js") %>'></script>

    <script type="text/javascript" src='<%= ResolveUrl("~/plugins/breakpoints/breakpoints.js") %>'></script>
    <script type="text/javascript" src='<%= ResolveUrl("~/plugins/dataTables/jquery.dataTables.min.js") %>'></script>
    <script type="text/javascript" src='<%= ResolveUrl("~/plugins/prettyPhoto-plugin/js/jquery.prettyPhoto.js") %>'></script>

    <script type="text/javascript" src='<%= ResolveUrl("~/plugins/mCustomScrollbar/jquery.mCustomScrollbar.concat.min.js") %>'></script>
    <script type="text/javascript" src='<%= ResolveUrl("~/plugins/tagsInput/jquery.tagsinput.min.js") %>'></script>
    <script type="text/javascript" src='<%= ResolveUrl("~/plugins/bootstrap-switch/bootstrap-switch.min.js") %>'></script>
    <script type="text/javascript" src='<%= ResolveUrl("~/plugins/blockUI/jquery.blockUI.js") %>'></script>
    <script type="text/javascript" src='<%= ResolveUrl("~/plugins/pnotify/js/jquery.pnotify.min.js") %>'></script>

    <script type="text/javascript" src='<%= ResolveUrl("~/js/theme.js") %>'></script>
    <script type="text/javascript" src='<%= ResolveUrl("~/custom/custom.js") %>'></script>

    <!-- Specific Page Scripts Put Here -->
    <%--<script type="text/javascript" src='<%= ResolveUrl("~plugins/tagsInput/jquery.tagsinput.min.js') %>'></script>--%>

    <script type="text/javascript" src='<%= ResolveUrl("~/plugins/bootstrap-timepicker/bootstrap-timepicker.min.js") %>'></script>
    <script type="text/javascript" src='<%= ResolveUrl("~/plugins/daterangepicker/moment.min.js") %>'></script>
    <script type="text/javascript" src='<%= ResolveUrl("~/plugins/daterangepicker/daterangepicker.js") %>'></script>
    <script type="text/javascript" src='<%= ResolveUrl("~/plugins/colorpicker/colorpicker.js") %>'></script>
    <script type="text/javascript" src='<%= ResolveUrl("~/plugins/ckeditor/ckeditor.js") %>'></script>
    <script type="text/javascript" src='<%= ResolveUrl("~/plugins/ckeditor/adapters/jquery.js") %>'></script>
    <script type="text/javascript" src='<%= ResolveUrl("~/js/bootstrap-datepicker.js") %>'></script>
    <script type="text/javascript" src='<%= ResolveUrl("~/plugins/bootstrap-wysiwyg/js/wysihtml5-0.3.0.min.js") %>'></script>
    <script type="text/javascript" src='<%= ResolveUrl("~/plugins/bootstrap-wysiwyg/js/bootstrap-wysihtml5-0.0.2.js") %>'></script>
    <script type="text/javascript" src='<%= ResolveUrl("~/plugins/introjs/js/intro.min.js") %>'></script>


    <script type="text/javascript">
        function renderCalendar() {

            if (!jQuery().fullCalendar) {
                return;
            }

            var date = new Date();
            // ReSharper disable once UnusedLocals
            var d = date.getDate();
            // ReSharper disable once UnusedLocals
            var m = date.getMonth();
            // ReSharper disable once UnusedLocals
            var y = date.getFullYear();


            var h = {
                left: 'title, prev,next',
                center: '',
                right: 'today,month,agendaWeek,agendaDay'
            };


            function initDragObject(element) {
                // create an Event Object (http://arshaw.com/fullcalendar/docs/event_data/Event_Object/)
                // it doesn't need to have a start or end
                var eventObject = {
                    title: $.trim(element.text()) // use the element's text as the event title
                };
                // store the Event Object in the DOM element so we can get to it later
                element.data('eventObject', eventObject);
                // make the event draggable using jQuery UI
                element.draggable({
                    zIndex: 999,
                    revert: true, // will cause the event to go back to its
                    revertDuration: 0 //  original position after the drag
                });
            }

            var addEvent = function (title) {
                title = title.length === 0 ? "Untitled Event" : title;
                var html = $('<div class="external-event btn vd_btn vd_bg-blue btn-xs mgr-10 mgbt-xs-10" style="" role="button">' + title + '</div>');
                jQuery('#events').append(html);
                initDragObject(html);
            };

            /* News Widget */


            $('#external-events div.external-event').each(function () {
                initDragObject($(this));
            });


            $('#event-add').unbind('click').click(function () {
                var title = $('#event-title').val();
                addEvent(title);
            });

            $('#event-title').keypress(function (event) {
                if (event.keyCode === 13) {
                    event.preventDefault();
                    $('#event-add').click();
                }
            });

            //predefined events
            $('#events').html("");
            addEvent("Event 1");
            addEvent("Event 2");
            addEvent("Event 3");
            addEvent("Event 4");
            addEvent("Event 5");
            addEvent("Event 6");

            $('#fullcalendar').html("");
            $('#fullcalendar').fullCalendar({
                header: h,
                editable: true,
                droppable: true, // this allows things to be dropped onto the calendar !!!
                drop: function (date, allDay) { // this function is called when something is dropped

                    // retrieve the dropped element's stored Event Object
                    var originalEventObject = $(this).data('eventObject');
                    // we need to copy it, so that multiple events don't have a reference to the same object
                    var copiedEventObject = $.extend({}, originalEventObject);

                    // assign it the date that was reported
                    copiedEventObject.start = date;
                    copiedEventObject.allDay = allDay;
                    copiedEventObject.className = $(this).attr("data-class");

                    // render the event on the calendar
                    // the last `true` argument determines if the event "sticks" (http://arshaw.com/fullcalendar/docs/event_rendering/renderEvent/)
                    $('#fullcalendar').fullCalendar('renderEvent', copiedEventObject, true);

                    // is the "remove after drop" checkbox checked?
                    if ($('#drop-remove').is(':checked')) {
                        // if so, remove the element from the "Draggable Events" list
                        $(this).remove();
                    }
                }
            });

        }
        window.onload = function () {

            renderCalendar();



            //var siteMapPath = document.getElementById('ContentPlaceHolder1_SiteMapPath1');
            //var span = siteMapPath.getElementsByTagName('span');
            ////var lblTitel = document.getElementById('ContentPlaceHolder1_lblTitel');

            //document.title = span[span.length - 1].innerText + " |  eAM™";

            //$(".datepicker_normal").datepicker({ dateFormat: 'dd M yy' });
            //// ReSharper disable once WrongExpressionStatement
            //"use strict";
            //$('#goto-menu a').click(function (e) {
            //    e.preventDefault();
            //    scrollTo($(this).attr('href'), -80);
            //});



            $('#input-autocomplete').tagsInput({
                width: 'auto',
                autocomplete_url: 'templates/files/fake_json_endpoint.html' // jquery ui autocomplete requires a json endpoint

            });


            var availableTags = [
                "ActionScript",
                "AppleScript",
                "Asp",
                "BASIC",
                "C",
                "C++",
                "Clojure",
                "COBOL",
                "ColdFusion",
                "Erlang",
                "Fortran",
                "Groovy",
                "Haskell",
                "Java",
                "JavaScript",
                "Lisp",
                "Perl",
                "PHP",
                "Python",
                "Ruby",
                "Scala",
                "Scheme"
            ];

            $("#normal-autocomplete").autocomplete({
                source: availableTags
            });



            $.widget("custom.catcomplete", $.ui.autocomplete, {
                _renderMenu: function (ul, items) {
                    var that = this,
                        currentCategory = "";
                    $.each(items, function (index, item) {
                        if (item.category !== currentCategory) {
                            ul.append("<li class='ui-autocomplete-category'>" + item.category + "</li>");
                            currentCategory = item.category;
                        }
                        that._renderItemData(ul, item);
                    });
                }
            });

            var data = [
                { label: "anders", category: "" },
                { label: "andreas", category: "" },
                { label: "antal", category: "" },
                { label: "annhhx10", category: "Products" },
                { label: "annk K12", category: "Products" },
                { label: "annttop C13", category: "Products" },
                { label: "anders andersson", category: "People" },
                { label: "andreas andersson", category: "People" },
                { label: "andreas johnson", category: "People" }
            ];
            $("#category-autocomplete").catcomplete({
                delay: 0,
                source: data
            });


            // ReSharper disable once InconsistentNaming
            var data_image = [
                { label: "anders", desc: "Lorem ipsum doler sit amet.", icon: "img/avatar/avatar.jpg" },
                { label: "andreas", desc: "Lorem ipsum doler sit amet.", icon: "img/avatar/avatar-2.jpg" },
                { label: "antal", desc: "Lorem ipsum doler sit amet.", icon: "img/avatar/avatar-3.jpg" },
                { label: "annhhx10", desc: "Lorem ipsum doler sit amet.", icon: "img/avatar/avatar-4.jpg" },
                { label: "annk K12", desc: "Lorem ipsum doler sit amet.", icon: "img/avatar/avatar-5.jpg" },
                { label: "annttop C13", desc: "Lorem ipsum doler sit amet.", icon: "img/avatar/avatar-6.jpg" },
                { label: "anders andersson", desc: "Lorem ipsum doler sit amet.", icon: "img/avatar/avatar-7.jpg" },
                { label: "andreas andersson", desc: "Lorem ipsum doler sit amet.", icon: "img/avatar/avatar-8.jpg" },
                { label: "andreas johnson", desc: "Lorem ipsum doler sit amet.", icon: "img/avatar/avatar-9.jpg" }
            ];
            $("#image-autocomplete").autocomplete({
                minLength: 0,
                source: data_image,
                focus: function (event, ui) {
                    $("#image-autocomplete").val(ui.item.label);
                    return false;
                }
            })
                .data("ui-autocomplete")._renderItem = function (ul, item) {
                    return $("<li>")
                        .append("<a href='javascript:void(0)'><span class='menu-icon'><img src='" + item.icon + "' alt='" + item.icon + "'></span><span class='menu-text'>" + item.label + "<span class='menu-info'>" + item.desc + "</span></span></a>")
                        .appendTo(ul);
                };


            /* Multiple Values */
            function split(val) {
                return val.split(/,\s*/);
            }
            function extractLast(term) {
                return split(term).pop();
            }
            $("#multiple-autocomplete")
                // don't navigate away from the field on tab when selecting an item
                .bind("keydown", function (event) {
                    if (event.keyCode === $.ui.keyCode.TAB &&
                        $(this).data("ui-autocomplete").menu.active) {
                        event.preventDefault();
                    }
                })
                .autocomplete({
                    minLength: 0,
                    source: function (request, response) {
                        // delegate back to autocomplete, but extract the last term
                        response($.ui.autocomplete.filter(
                            availableTags, extractLast(request.term)));
                    },
                    focus: function () {
                        // prevent value inserted on focus
                        return false;
                    },
                    select: function (event, ui) {
                        var terms = split(this.value);
                        // remove the current input
                        terms.pop();
                        // add the selected item
                        terms.push(ui.item.value);
                        // add placeholder to get the comma-and-space at the end
                        terms.push("");
                        this.value = terms.join(", ");
                        return false;
                    }
                });


            $("#datepicker-multiple").datepicker({
                numberOfMonths: 3,
                showButtonPanel: true,
                dateFormat: 'dd M yy'
            });
            $("#datepicker-from").datepicker({
                defaultDate: "+1w",
                dateFormat: 'dd M yy',
                changeMonth: true,
                numberOfMonths: 3,
                onClose: function (selectedDate) {
                    $("#to").datepicker("option", "minDate", selectedDate);
                }
            });
            $("#datepicker-to").datepicker({
                defaultDate: "+1w",
                dateFormat: 'dd M yy',
                changeMonth: true,
                numberOfMonths: 3,
                onClose: function (selectedDate) {
                    $("#from").datepicker("option", "maxDate", selectedDate);
                }
            });
            $("#datepicker-icon").datepicker({ dateFormat: 'dd M yy' });
            // ReSharper disable once UnusedParameter
            $('[data-datepicker]').click(function (e) {
                var data = $(this).data('datepicker');
                $(data).focus();
            });
            $("#datepicker-restrict").datepicker({ minDate: -20, maxDate: "+1M +10D" });
            $("#datepicker-widget").datepicker();


            $('#datepicker-daterangepicker').daterangepicker(
                {
                    ranges: {
                        'Today': [window.moment(), window.moment()],
                        'Yesterday': [window.moment().subtract('days', 1), window.moment().subtract('days', 1)],
                        'Last 7 Days': [window.moment().subtract('days', 6), window.moment()],
                        'Last 30 Days': [window.moment().subtract('days', 29), window.moment()],
                        'This Month': [window.moment().startOf('month'), window.moment().endOf('month')],
                        'Last Month': [window.moment().subtract('month', 1).startOf('month'), window.moment().subtract('month', 1).endOf('month')]
                    },
                    startDate: window.moment().subtract('days', 29),
                    endDate: window.moment()
                },
                function (start, end) {
                    $('#reportrange span').html(start.format('MMMM D, YYYY') + ' - ' + end.format('MMMM D, YYYY'));
                }
            );

            $('#datepicker-datetime').daterangepicker({ timePicker: true, timePickerIncrement: 30, format: 'MM/DD/YYYY h:mm A' });

            $('#timepicker-default').timepicker();
            $('#timepicker-full').timepicker({
                minuteStep: 1,
                template: false,
                showSeconds: true,
                showMeridian: false
            });

            $('#colorpicker-hex').ColorPicker({
                color: '#ff00ff',
                onSubmit: function (hsb, hex, rgb, el) {
                    $(el).val(hex);
                    $(el).ColorPickerHide();
                },
                onBeforeShow: function () {
                    $(this).ColorPickerSetColor(this.value);
                },
                // ReSharper disable once UnusedParameter
                onChange: function (hsb, hex, rgb) {
                    $('#colorpicker-hex').val('#' + hex);
                    $('#colorpicker-hex').siblings().css({ 'color': '#' + hex });
                }
            })
                .bind('keyup', function () {
                    $(this).ColorPickerSetColor(this.value);
                    // ReSharper disable once UnusedParameter
                }).siblings().click(function (e) {
                    $(this).siblings().click();
                });




            $('.colorpicker-rgba').ColorPicker({
                color: '#ff00ff',
                onSubmit: function (hsb, hex, rgb, el) {
                    $(el).val(hex);
                    $(el).ColorPickerHide();
                },
                onBeforeShow: function () {
                    $(this).ColorPickerSetColor(this.value);
                },
                onChange: function (hsb, hex, rgb) {
                    $('.colorpicker-rgba').val('rgb(' + rgb['r'] + ',' + rgb['g'] + ',' + rgb['b'] + ')');
                    $('.colorpicker-rgba').siblings().css({ 'color': 'rgb(' + rgb['r'] + ',' + rgb['g'] + ',' + rgb['b'] + ')' });
                }
            })
                .bind('keyup', function () {
                    $(this).ColorPickerSetColor(this.value);
                    // ReSharper disable once UnusedParameter
                }).siblings().click(function (e) {
                    $(this).siblings().click();
                });

            //CKEDITOR.replace( $('[data-rel^="ckeditor"]') );
            $('[data-rel^="ckeditor"]').ckeditor();





        };
    </script>


    <!-- The Load Image plugin is included for the preview images and image resizing functionality -->
    <%--<script src='<%= ResolveUrl("~/blueimp.github.io/JavaScript-Load-Image/js/load-image.min.html"></script>--%>
    <!-- The Canvas to Blob plugin is included for image resizing functionality -->
    <%-- <script src="../blueimp.github.io/JavaScript-Canvas-to-Blob/js/canvas-to-blob.min.js"></script>--%>
    <!-- The Iframe Transport is required for browsers without support for XHR file uploads -->
    <script src='<%= ResolveUrl("~/plugins/jquery-file-upload/js/jquery.iframe-transport.js") %>'></script>
    <!-- The basic File Upload plugin -->
    <script src='<%= ResolveUrl("~/plugins/jquery-file-upload/js/jquery.fileupload.js") %>'></script>
    <!-- The File Upload processing plugin -->
    <script src='<%= ResolveUrl("~/plugins/jquery-file-upload/js/jquery.fileupload-process.js") %>'></script>
    <!-- The File Upload image preview & resize plugin -->
    <script src='<%= ResolveUrl("~/plugins/jquery-file-upload/js/jquery.fileupload-image.js") %>'></script>
    <!-- The File Upload audio preview plugin -->
    <script src='<%= ResolveUrl("~/plugins/jquery-file-upload/js/jquery.fileupload-audio.js") %>'></script>
    <!-- The File Upload video preview plugin -->
    <script src='<%= ResolveUrl("~/plugins/jquery-file-upload/js/jquery.fileupload-video.js") %>'></script>
    <!-- The File Upload validation plugin -->
    <script src='<%= ResolveUrl("~/plugins/jquery-file-upload/js/jquery.fileupload-validate.js") %>'></script>

    <script src='<%= ResolveUrl("~/plugins/summernote/summernote.js") %>'></script>
    <style type='text/css'>
        #calendar {
            margin-top: 0;
            text-align: center;
            font-size: 14px;
            font-family: "Lucida Grande",Helvetica,Arial,Verdana,sans-serif;
            width: 350px;
            margin: 0 auto;
        }
        /* css for timepicker */
        .ui-timepicker-div dl {
            text-align: left;
        }

            .ui-timepicker-div dl dt {
                height: 25px;
            }

            .ui-timepicker-div dl dd {
                margin: -25px 0 10px 65px;
            }

        .style1 {
            width: 100%;
        }

        /* table fields alignment*/
        .alignRight {
            text-align: right;
            padding-right: 10px;
            padding-bottom: 10px;
        }

        .alignLeft {
            text-align: left;
            padding-bottom: 10px;
        }
    </style>


</body>
</html>
