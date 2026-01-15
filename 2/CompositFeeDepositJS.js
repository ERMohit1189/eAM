function TotalPayable(tis) {
    if (isNaN(parseFloat($(tis).val()))) {
        $(tis).val('');
    }
    else if (parseFloat($(tis).val()) == 0) {
        $(tis).val('');
    }
    var maivval1 = parseFloat($(tis).val() == "" ? "0" : $(tis).val());
    var count = 0;
    $(".mainRow").each(function () {
        var headRowid = $(this).attr('id');
        //$("#" + headRowid).find('td:eq(3) input[type=text]').val('');
        $("#" + headRowid).find('td:eq(5) input[type=text]').val('');
        if ($('.hdnDiscountText').html() == "1") {
            $("#" + headRowid).find('td:eq(3) input[type=text]').attr('readonly', 'readonly');
        }
        if ($('.hdnPaidText').html() == "1") {
            $("#" + headRowid).find('td:eq(5) input[type=text]').attr('readonly', 'readonly');
        }
        $("#" + headRowid).find('td:eq(8) textarea').attr('readonly', 'readonly');
        $("#" + headRowid).find('td:eq(1)  input[type=checkbox]').prop('checked', false);

        var payable_h = 0; var due_h = 0; var thisval = 0;
        var lblfee_h = parseFloat($("#" + headRowid).find('td:eq(2) span:eq(0)').html());
        var disc = $("#ContentPlaceHolder1_ContentPlaceHolderMainBox_rptFeeStructure_txtInstallmentdiscount_" + count).val();
        var lbldisc = parseFloat($("#ContentPlaceHolder1_ContentPlaceHolderMainBox_rptFeeStructure_lblInstallmentDiscount_" + count).html());
        var lbldiscount_h = parseFloat((lbldisc == '' || lbldisc == undefined) ? 0 : lbldisc) +parseFloat((disc == '' || disc == undefined) ? 0 : disc);
        var lblTotal_h = (lblfee_h.toFixed(2) - lbldiscount_h.toFixed(2)).toFixed(2);
        var lblpaid_h = parseFloat($("#" + headRowid).find('td:eq(5) span:eq(0)').html());
        var actualDue = parseFloat($("#" + headRowid).find('td:eq(6) span:eq(1)').html());
        payable_h = (lblTotal_h - lblpaid_h).toFixed(2);
        if (actualDue > 0) {
            var txttotal_h = 0;
            var boxValues = parseFloat($("#ContentPlaceHolder1_ContentPlaceHolderMainBox_txtTotalPaid").val());
            $("#ContentPlaceHolder1_ContentPlaceHolderMainBox_txtTotalPaid").val(boxValues.toFixed(2));
            $("#ContentPlaceHolder1_ContentPlaceHolderMainBox_hdnTotalPaid").val(boxValues.toFixed(2));
            if (maivval1 >= payable_h) {
                $("#" + headRowid).find('td:eq(6) span:eq(0)').html('0.00');
                $("#" + headRowid).find('td:eq(5) input[type=text]').val(payable_h);
                maivval1 = maivval1 - payable_h;
                txttotal_h = payable_h;
                $("#" + headRowid).find('td:eq(1)  input[type=checkbox]').prop('checked', true);
                if ($('.hdnDiscountText').html() == "1") {
                    $("#" + headRowid).find('td:eq(3) input[type=text]').removeAttr('readonly');
                }
                if ($('.hdnPaidText').html() == "1") {
                    $("#" + headRowid).find('td:eq(5) input[type=text]').removeAttr('readonly');
                }
                $("#" + headRowid).find('td:eq(8) textarea').removeAttr('readonly');
            }
            else if (maivval1 < payable_h && maivval1 > 0) {
                $("#" + headRowid).find('td:eq(6) span:eq(0)').html((payable_h - maivval1).toFixed(2));
                $("#" + headRowid).find('td:eq(5) input[type=text]').val(maivval1.toFixed(2));
                txttotal_h = maivval1;
                maivval1 = 0;
                $("#" + headRowid).find('td:eq(1)  input[type=checkbox]').prop('checked', true);
                if ($('.hdnDiscountText').html() == "1") {
                    $("#" + headRowid).find('td:eq(3) input[type=text]').removeAttr('readonly');
                }
                if ($('.hdnPaidText').html() == "1") {
                    $("#" + headRowid).find('td:eq(5) input[type=text]').removeAttr('readonly');
                }
                $("#" + headRowid).find('td:eq(8) textarea').removeAttr('readonly');
            }
            else {
                $("#" + headRowid).find('td:eq(6) span:eq(0)').html(payable_h);
                $("#" + headRowid).find('td:eq(5) input[type=text]').val('');
                $("#" + headRowid).find('td:eq(1)  input[type=checkbox]').prop('checked', false);
                if ($('.hdnDiscountText').html() == "1") {
                    $("#" + headRowid).find('td:eq(3) input[type=text]').attr('readonly');
                }
                if ($('.hdnPaidText').html() == "1") {
                    $("#" + headRowid).find('td:eq(5) input[type=text]').attr('readonly');
                }
                txttotal_h = 0;
                maivval1 = 0;
            }
            var childRowid = "instalDeails_" + $(this).closest('tr').attr('id').split("_")[1];
            var headTable_ids = "headTable_" + $(this).closest('tr').attr('id').split("_")[1];
            var len = $("#" + headTable_ids + " tbody tr").length;
            if (txttotal_h > 0) {
                var finetype = 0; var totalDuef = 0;
                for (var j = 0; j < len; j++) {
                    var ApplyFine = $("#" + headTable_ids + " tbody tr:eq(" + j + ")").find('td:eq(0) span:eq(2)').html();
                    var lblPayablef = parseFloat($("#" + headTable_ids + " tbody tr:eq(" + j + ")").find('td:eq(4) span:eq(0)').html());
                    var lblPaidf = parseFloat($("#" + headTable_ids + " tbody tr:eq(" + j + ")").find('td:eq(5) span:eq(0)').html());
                    if (ApplyFine == "ApplyFine") {

                        var duh = (lblPayablef - lblPaidf).toFixed(2);
                        if (duh > 0) {
                            finetype = finetype + 1;
                            totalDuef = parseFloat(totalDuef) + parseFloat(duh);
                        }
                    }
                }
                if (totalDuef > 0 && txttotal_h >= totalDuef && finetype > 0) {
                    for (var j = 0; j < len; j++) {
                        var isFineApply = $("#" + headTable_ids + " tbody tr:eq(" + j + ")").find('td:eq(0) span:eq(2)').html();
                        if (isFineApply == "ApplyFine") {
                            var lblPayablefh = parseFloat($("#" + headTable_ids + " tbody tr:eq(" + j + ")").find('td:eq(4) span:eq(0)').html());
                            var lblPaidfh = parseFloat($("#" + headTable_ids + " tbody tr:eq(" + j + ")").find('td:eq(5) span:eq(0)').html());
                            var tDuefh = (lblPayablefh - lblPaidfh).toFixed(2);
                            $("#" + headTable_ids + " tbody tr:eq(" + j + ")").find('td:eq(0)  input[type=checkbox]').prop('checked', true);
                            $("#" + headTable_ids + " tbody tr:eq(" + j + ")").find('td:eq(3) input[type=text]').removeAttr('readonly');
                            $("#" + headTable_ids + " tbody tr:eq(" + j + ")").find('td:eq(5) input[type=text]').attr('readonly', 'readonly');
                            $("#" + headTable_ids + " tbody tr:eq(" + j + ")").find('td:eq(5) input[type=text]').val(tDuefh);
                            $("#" + headTable_ids + " tbody tr:eq(" + j + ")").find('td:eq(6) span:eq(0)').html("0.00")
                            txttotal_h = txttotal_h - tDuefh;
                            len = (len - finetype);
                        }
                    }
                }

                for (var i = 0; i < len; i++) {
                    var lblActualDue = parseFloat($("#" + headTable_ids + " tbody tr:eq(" + i + ")").find('td:eq(6) span:eq(1)').html());
                    if (lblActualDue > 0) {
                        var isApplyFine = $("#" + headTable_ids + " tbody tr:eq(" + i + ")").find('td:eq(0) span:eq(2)').html();
                        if (isApplyFine != "ApplyFine") {
                            var lblPayable = parseFloat($("#" + headTable_ids + " tbody tr:eq(" + i + ")").find('td:eq(4) span:eq(0)').html());
                            var lblPaid = parseFloat($("#" + headTable_ids + " tbody tr:eq(" + i + ")").find('td:eq(5) span:eq(0)').html());
                            if (txttotal_h >= (lblPayable - lblPaid) && txttotal_h > 0) {
                                var totalDue1 = (lblPayable - lblPaid).toFixed(2);
                                $('#' + headTable_ids + ' tbody tr:eq(' + i + ')').find('td:eq(5) input[type=text]').val(totalDue1);
                                txttotal_h = txttotal_h - (lblPayable - lblPaid);
                                $('#' + headTable_ids + ' tbody tr:eq(' + i + ')').find('td:eq(6) span:eq(0)').html("0.00");
                                $('#' + headTable_ids + ' tbody tr:eq(' + i + ')').find('td:eq(0)  input[type=checkbox]').prop('checked', true);
                                if ($('.hdnDiscountText').html() == "1") {
                                    $('#' + headTable_ids + ' tbody tr:eq(' + i + ')').find('td:eq(3) input[type=text]').removeAttr('readonly');
                                }
                                if ($('.hdnPaidText').html() == "1") {
                                    $('#' + headTable_ids + ' tbody tr:eq(' + i + ')').find('td:eq(5) input[type=text]').removeAttr('readonly');
                                }
                                $('#' + headTable_ids + ' tbody tr:eq(' + i + ')').find('td:eq(7) textarea').removeAttr('readonly');
                            }
                            else if (txttotal_h < (lblPayable - lblPaid) && txttotal_h > 0) {
                                var totalDue2 = (lblPayable - lblPaid).toFixed(2);
                                $('#' + headTable_ids + ' tbody tr:eq(' + i + ')').find('td:eq(5) input[type=text]').val(txttotal_h);
                                $('#' + headTable_ids + ' tbody tr:eq(' + i + ')').find('td:eq(6) span:eq(0)').html((totalDue2 - txttotal_h).toFixed(2));
                                txttotal_h = 0;
                                $('#' + headTable_ids + ' tbody tr:eq(' + i + ')').find('td:eq(0)  input[type=checkbox]').prop('checked', true);
                                if ($('.hdnDiscountText').html() == "1") {
                                    $('#' + headTable_ids + ' tbody tr:eq(' + i + ')').find('td:eq(3) input[type=text]').removeAttr('readonly');
                                }
                                if ($('.hdnPaidText').html() == "1") {
                                    $('#' + headTable_ids + ' tbody tr:eq(' + i + ')').find('td:eq(5) input[type=text]').removeAttr('readonly');
                                }
                                $('#' + headTable_ids + ' tbody tr:eq(' + i + ')').find('td:eq(7) textarea').removeAttr('readonly');
                            }
                            else if (txttotal_h == 0) {
                                txttotal_h = 0;
                                var totalDue3 = (lblPayable - lblPaid).toFixed(2);
                                $('#' + headTable_ids + ' tbody tr:eq(' + i + ')').find('td:eq(5) input[type=text]').val('');
                                $('#' + headTable_ids + ' tbody tr:eq(' + i + ')').find('td:eq(6) span:eq(0)').html(totalDue3);
                                $('#' + headTable_ids + ' tbody tr:eq(' + i + ')').find('td:eq(0)  input[type=checkbox]').prop('checked', false);
                                if ($('.hdnDiscountText').html() == "1") {
                                    $('#' + headTable_ids + ' tbody tr:eq(' + i + ')').find('td:eq(3) input[type=text]').attr('readonly', 'readonly');
                                }
                                if ($('.hdnPaidText').html() == "1") {
                                    $('#' + headTable_ids + ' tbody tr:eq(' + i + ')').find('td:eq(5) input[type=text]').attr('readonly', 'readonly');
                                }
                                $('#' + headTable_ids + ' tbody tr:eq(' + i + ')').find('td:eq(7) textarea').attr('readonly', 'readonly');
                            }
                        }
                    }
                }

            }
            else {
                for (var j = 0; j < len; j++) {
                    var lblfee = parseFloat($('#' + headTable_ids + ' tbody tr:eq(' + j + ')').find('td:eq(2) span:eq(0)').html());
                    var lbldisc = parseFloat($('#' + headTable_ids + ' tbody tr:eq(' + j + ')').find('td:eq(3) span:eq(0)').html());
                    var lblPayablefh = (lblfee - lbldisc).toFixed(2);
                    $('#' + headTable_ids + ' tbody tr:eq(' + j + ')').find('td:eq(4) span:eq(0)').html(lblPayablefh);
                    var lblPaidfh = parseFloat($('#' + headTable_ids + ' tbody tr:eq(' + j + ')').find('td:eq(5) span:eq(0)').html());
                    var tDuefh = (lblPayablefh - lblPaidfh).toFixed(2);
                    $('#' + headTable_ids + ' tbody tr:eq(' + j + ')').find('td:eq(6) span:eq(0)').html(tDuefh);
                    $('#' + headTable_ids + ' tbody tr:eq(' + j + ')').find('td:eq(5) input[type=text]').val('');
                    $('#' + headTable_ids + ' tbody tr:eq(' + j + ')').find('td:eq(0)  input[type=checkbox]').prop('checked', false);
                    if ($('.hdnDiscountText').html() == "1") {
                        $('#' + headTable_ids + ' tbody tr:eq(' + j + ')').find('td:eq(3) input[type=text]').attr('readonly', 'readonly');
                    }
                    if ($('.hdnPaidText').html() == "1") {
                        $('#' + headTable_ids + ' tbody tr:eq(' + j + ')').find('td:eq(5) input[type=text]').attr('readonly', 'readonly');
                    }
                    $('#' + headTable_ids + ' tbody tr:eq(' + j + ')').find('td:eq(7) textarea').attr('readonly', 'readonly');
                }
            }
        }
        if (isNaN(parseFloat($(tis).val()))) {
            $(tis).val('0.00');
        }
        count += 1;
    });
}