function printDiv(heding, tblname) {
    var hd1 = $('div[Id*="header"]').html();
    var hd2 = $('div[Id*="gdv"] span[Id*="heading"]').html();
    var hd3 = $('div[Id*="gdv"] span[Id*="lblRegister"]').html();
    //alert(hd1 + '-' + hd2 + '-' + hd3);
    //alert(hd1);
    //alert(heding+'-'+ tblname);
    //var monthName = $('select[Id*="ddlMonth"]').find(":selected").text();
    //var yearName = $('select[Id*="ddlYear"]').find(":selected").text();

    var tds = $('table[Id*="' + tblname +'"] tbody').children('tr').children('th').length;
    //alert(tds);
    var tfHdr = [], tfBody = [];
    $('table[Id*="' + tblname+'"] tbody tr th').each(function () {
        tfHdr.push($(this).html());
    });

    $('table[Id*="' + tblname+'"] tbody tr td').each(function () {
        tfBody.push($(this).html());
    });

    //alert(tfHdr); alert(tfBody);

    /*===================================================*/

    var tutionDiv = '<b>' + heding+'</b><hr><table class="table table-bordered"><thead><tr>';
    for (var i = 0; i < tfHdr.length; i++) {
        tutionDiv += '<th>' + tfHdr[i] + '</th>';
    }
    tutionDiv += '</tr></thead><tbody>';
    var j = 0;
    for (var i = 0; i < tfBody.length; i = i + tds) {
        tutionDiv += '<tr>';
        for (var j = 0; j < tds; j++) {
            tutionDiv += '<td>' + tfBody[i + j] + '</td>';
        }
        tutionDiv += '</tr>';
    }
    tutionDiv += '</tbody></table>';
    //alert(tutionDiv);
    /*===================================================*/
    var newWin = window.open('', 'Print-Window');
    newWin.document.open();
    newWin.document.write('<html><head><title>' + heding + ' </title><link rel="stylesheet" href="../css/bootstrap.min.css">' +
        '<style>hr { margin-top: 2px; margin-bottom: 2px;}.table{font-size: 10px;} .table>tbody>tr>td, .table>tbody>tr>th, .table>tfoot>tr>td, .table>tfoot>tr>th, .table>thead>tr>td, .table>thead>tr>th {'
        + 'padding: 2px;} h1, h3{margin:0!important;}  @media print {.col-lg-12 { text-align:center; } .col-lg-6 { width: 50%;float:left;font-size:12px;} } </style> </head><body onload="window.print()"><div class="ontainer"><div class="row">'
        //+ '<div class="col-lg-12"><b>Student Details </b><hr>' + studentDiv + '</div>'
        + '<div class="col-lg-12 text-center"><b>' + hd1 + '</b></div></div>'
        + '<div class="row"><div class="col-lg-12 text-center"><b>' + hd2 + '</b></div></div>'
        + '<div class="col-lg-12 text-center"><b>' + hd3 + '</b></div></div>'
        + '<div class="row"><div class="col-lg-12 text-center">' + tutionDiv + '</div>'
        //+ '<div class="col-lg-6">' + (transportDiv.length < 60 ? '' : transportDiv) + '<div>'
        + '<div><div></body></html>');

    newWin.document.close();

    //setTimeout(function () { newWin.close(); }, 5);
}