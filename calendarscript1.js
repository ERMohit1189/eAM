var currentUpdateEvent;
var addStartDate;
var addEndDate;
var globalAllDay;

function selectDate(start, end, allDay) {

    $('#addDialog').dialog('open');


    $("#addEventStartDate").text("" + start.toLocaleString());
    $("#addEventEndDate").text("" + end.toLocaleString());


    addStartDate = start;
    addEndDate = end;
    globalAllDay = allDay;
}

$(document).ready(function () {

    var date = new Date();
    var d = date.getDate();
    var m = date.getMonth();
    var y = date.getFullYear();

    var calendar = $('#calendar').fullCalendar({
        theme: true,
        header: {
            left: 'prev,next today',
            center: 'title',
            right: 'month,agendaWeek,agendaDay'
        },
        eventClick: "",
        selectable: true,
        selectHelper: true,
        select: selectDate,
        editable: false,
        events: "JsonResponse1.ashx",
        eventDrop: "",
        eventResize: "",      
        eventRender: function (event, element) {
            var elements = document.querySelectorAll('.fc-event');
            for (var i = 0; i < elements.length; i++) {
                
                var innerelement = elements[i].querySelector('.fc-event-inner')

                var left = $(elements[i]).css('position');
                var index = $(elements[i]).css('z-index');
                var left = $(elements[i]).css('left');
                var width = $(elements[i]).css('width');
                var top = $(elements[i]).css('top');
                if ($(innerelement).css('background-color') == 'rgba(0, 0, 0, 0)') {
                    $(innerelement).css('background-color', '#CCC');
                }
                var colours = $(innerelement).css('background-color');              
                $(elements[i]).css("cssText", "border-color: " + colours + " !important;top: " + top + ";position: absolute; z-index: " + index + ";width: " + width + ";    left: " + left + ";");
                $(innerelement).css("visibility", "hidden");
            }
            element.qtip({
                content: event.description,
                position: { corner: { tooltip: 'bottomLeft', target: 'topLeft' } },
                style: {
                    border: {
                        width: 1,
                        radius: 3,
                        color: event.color.includes(",") ? "#CCC" : event.color
                    },
                    padding: 10,
                    textAlign: 'center',
                    position: 'relative',
                    width: 100,
                    tip: true, // Give it a speech bubble tip with automatic corner detection
                    //name: 'cream', // Style it according to the preset 'cream' style
                    //backgroungcolor:"#FFF"
                }

            });
            
        }
    
    });

    var fcheaderright = document.querySelector('.fc-header-right');
    fcheaderright.style.display = "none";

    var fcmonthyear = document.querySelector('.fc-header-center');
    fcmonthyear.style.textAlign = "right";
});

