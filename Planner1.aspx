<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Planner1.aspx.cs" Inherits="Calendar" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Event Calendar Using J-Query</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
   
    <link href="fullcalendar/cupertino/jquery-ui-1.7.3.custom.css" rel="stylesheet" type="text/css" />
    <link href="fullcalendar/fullcalendar1.css" rel="stylesheet" type="text/css" />
    <script src="fullcalendar/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>

    <script src="fullcalendar/jquery/jquery-ui-1.7.3.custom.min.js" type="text/javascript"></script>

    <script src="fullcalendar/jquery/jquery.qtip-1.0.0-rc3.min.js" type="text/javascript"></script>

    <script src="fullcalendar/fullcalendar1.min.js" type="text/javascript"></script>

    <script src="calendarscript1.js" type="text/javascript"></script>
    
    <script src="fullcalendar/jquery/jquery-ui-timepicker-addon-0.6.2.min.js" type="text/javascript"></script>
    <style type='text/css'>
        body
        {
            margin-top: 40px;
            text-align: center;
            font-size: 14px;
            font-family: "Lucida Grande" ,Helvetica,Arial,Verdana,sans-serif;
        }
        #calendar
        {
            width: 900px;
            margin: 0 auto;
        }
        /* css for timepicker */
        .ui-timepicker-div dl
        {
            text-align: left;
        }
        .ui-timepicker-div dl dt
        {
            height: 25px;
        }
        .ui-timepicker-div dl dd
        {
            margin: -25px 0 10px 65px;
        }
        .style1
        {
            width: 100%;
        }
        
        /* table fields alignment*/
        .alignRight
        {
        	text-align:right;
        	padding-right:10px;
        	padding-bottom:10px;
        }
        .alignLeft
        {
        	text-align:left;
        	padding-bottom:10px;
        }
        .fc-event-skin {
            visibility:visible !important;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
   <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    
    <div id="calendar">
    </div>
    <div runat="server" id="jsonDiv"></div>
    <input type="hidden" id="hdClient" runat="server" />
    </form>
</body>
</html>
