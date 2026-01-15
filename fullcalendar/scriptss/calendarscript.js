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
        events: "../JsonResponse.ashx",
        eventDrop: "",
        eventResize: "",      
        eventRender: function (event, element) {

        element.qtip({
                content: event.description,
                position: { corner: { tooltip: 'bottomLeft', target: 'topLeft' } },
                style: {
                    border: {
                        width: 1,
                        radius: 3,
                        color: event.color

                    },
                    padding: 10,
                    textAlign: 'center',
                    position: 'relative',
                    width: 100,
                    left: -10,
                    tip: true, // Give it a speech bubble tip with automatic corner detection
                    //name: 'cream' // Style it according to the preset 'cream' style
                }

            });
        }

    });
});



