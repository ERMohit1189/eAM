//$('[data-toggle="tooltip"]').each(function () {
//    $(this).data('bs.tooltip').options.placement = 'right';
//});
//$(function () {
//    $('[data-toggle="tooltip"]').tooltip({
//        placement: 'top'
//    });
//});
function currentDate() {
    var datepicker = document.querySelectorAll('.datepicker-normal');
    var date = new Date();
    for (var i = 0; i < datepicker.length; i++) {
        datepicker[i].value = date.format("dd-MMM-yyyy");
    }
}

function decimalOrNumeric(tis) {
    var $this = $(tis);
    $this.val($this.val().replace(/[^\d.]/g, ''));
    var amount = ($(tis).val() == "" ? "0.00" : $(tis).val());
    $(tis).val(amount);
}
function decimalOrNumeric2(tis) {
    var $this = $(tis);
    $this.val($this.val().replace(/[^\d.]/g, ''));
    var amount = ($(tis).val() == "" ? "" : $(tis).val());
    $(tis).val(amount);
}
function checksFileSizeandFileType(fileupload, size, filetype, imageClass) {
    var img = document.querySelector('.' + imageClass);
    if (fileupload.files.length > 0) {
        var filename = fileupload.files[0].name;
        var filesize = fileupload.files[0].size;
        if (filesize <= size) {
            var extSplit = filename.split('.');
            var extReverse = extSplit.reverse();
            var ext = extReverse[0];
            var splitfileext = filetype.split('|');
            var flag = false;

            for (var i = 0; i < splitfileext.length; i++) {
                if (ext == splitfileext[i]) {
                    flag = true;
                    break;
                }
            }
            if (flag == false) {
                alert('Only ' + filetype + ' files are allowed!');
                fileupload.value = "";
            }
        }
        else {
            alert('File size should not more than ' + (size / 1000) + ' Kb');
            fileupload.value = "";
        }
       
        var reader = new FileReader();
        reader.onloadend = function () {
            img.src = reader.result;
        }
        if (fileupload.files[0]) {
            reader.readAsDataURL(fileupload.files[0]);
        }
    }
}


function checksFileSizeandFileTypeinupdatePanel(fileupload, size, filetype, imageClass, hiddenfield) {
    var img = document.querySelector('.' + imageClass);
    if (fileupload.files.length > 0) {
        var filename = fileupload.files[0].name;
        var filesize = fileupload.files[0].size;
        if (filesize <= size) {
            var extSplit = filename.split('.');
            var extReverse = extSplit.reverse();
            var ext = extReverse[0];
            var splitfileext = filetype.split('|');
            var flag = false;

            for (var i = 0; i < splitfileext.length; i++) {
                if (ext == splitfileext[i]) {
                    flag = true;
                    break;
                }

            }
            if (flag == false) {
                alert('Only ' + filetype + ' files are allowed!');
                fileupload.value = "";
            }
        }
        else {
            alert('File size should not more than ' + (size / 1000) + ' Kb');
            fileupload.value = "";
        }

        var reader = new FileReader();
        reader.onloadend = function () {
            img.src = reader.result;
            var base64url = reader.result.split(',')
            document.getElementById(hiddenfield).value = base64url[base64url.length - 1];
        }
        if (fileupload.files[0]) {
            reader.readAsDataURL(fileupload.files[0]);
        }
        else {
            img.src = "";
        }
    }
    else {
        img.src = "";
    }

}

function checksFileSizeandFileTypeinupdatePanel_witin_Repeater(fileupload, size, filetype, hiddenfield1, hiddenfield2) {
    var item = fileupload.id.split('_');
    var itemIndex = item[item.length - 1];
    var ext = "";
    if (fileupload.files.length > 0) {
        var filename = fileupload.files[0].name;
        var filesize = fileupload.files[0].size;
        if (filesize <= size) {
            var extSplit = filename.split('.');
            var extReverse = extSplit.reverse();
            ext = extReverse[0];
            var splitfileext = filetype.split('|');
            var flag = false;

            for (var i = 0; i < splitfileext.length; i++) {
                if (ext == splitfileext[i]) {
                    flag = true;
                    break;
                }

            }
            if (flag == false) {
                alert('Only ' + filetype + ' files are allowed!');
                fileupload.value = "";
            }
        }
        else {
            alert('File size should not more than ' + (size / 1000) + ' Kb');
            fileupload.value = "";
        }

        var reader = new FileReader();
        reader.onloadend = function () {
            var base64url = reader.result.split(',')
            document.getElementById(hiddenfield1 + "_" + itemIndex).value = base64url[base64url.length - 1];
            document.getElementById(hiddenfield2 + "_" + itemIndex).value = "."+ext;
            
        }
        if (fileupload.files[0]) {
            reader.readAsDataURL(fileupload.files[0]);
        }
        else {
            img.src = "";
        }
    }
    else {
        img.src = "";
    }

}

function checksFileSizeandFileTypeinupdatePanel_fordoc(fileupload, filetype, hiddenfield1, hiddenfield2) {
    var ext = "";
    if (fileupload.files.length > 0) {
        var filename = fileupload.files[0].name;
        var filesize = fileupload.files[0].size;
        //if (filesize <= size) {
            var extSplit = filename.split('.');
            var extReverse = extSplit.reverse();
            ext = extReverse[0];
            var splitfileext = filetype.split('|');
            var flag = false;

            for (var i = 0; i < splitfileext.length; i++) {
                if (ext == splitfileext[i]) {
                    flag = true;
                    break;
                }

            }
            if (flag == false) {
                alert('Only ' + filetype + ' files are allowed!');
                fileupload.value = "";
            }
        //}
        //else {
        //    alert('File size should not more than ' + (size / 1000) + ' Kb');
        //    fileupload.value = "";
        //}

        var reader = new FileReader();
        reader.onloadend = function () {
            var base64url = reader.result.split(',')
            document.getElementById(hiddenfield1).value = base64url[base64url.length - 1];
            document.getElementById(hiddenfield2).value = "." + ext;

        }
        if (fileupload.files[0]) {
            reader.readAsDataURL(fileupload.files[0]);
        }
        else {
            img.src = "";
        }
    }
    else {
        img.src = "";
    }

}

function CheckDecimalValue(e, TextBox, Textbox1) {
    var index = TextBox.value.toLowerCase().indexOf(String.fromCharCode(e.keyCode).toLowerCase());
    var values = TextBox.value;
    var amount = ""; 
    if ((e.keyCode == 190)) {        
        var checkDot = 0;
        for (var i = 0; i < values.length; i++) {
            if (values[i] == '.') {
                checkDot = checkDot + 1;
            }
            if (checkDot <= 1) {
                amount = amount + values[i];
            }
        }
        TextBox.value = amount;
        
    }
    else {
        if (e.keyCode >= 65 && e.keyCode <= 90) {
            for (var i = 0; i < values.length; i++) {
                if (i != index) {
                    amount = amount + values[i];
                }
            }
            TextBox.value = amount;
        }
        else if (e.keyCode >= 106 && e.keyCode <= 109) {
            for (var i = 0; i < values.length; i++) {
                if (i != index) {
                    amount = amount + values[i];
                }
            }
            TextBox.value = amount;
        }
        else if (e.keyCode == 111) {
            for (var i = 0; i < values.length; i++) {
                if (i != index) {
                    amount = amount + values[i];
                }
            }
            TextBox.value = amount;
        }
        else if (e.keyCode >= 186 && e.keyCode <= 189) {
            for (var i = 0; i < values.length; i++) {
                if (i != index) {
                    amount = amount + values[i];
                }
            }
            TextBox.value = amount;
        }
        else if (e.keyCode >= 191 && e.keyCode <= 192) {
            for (var i = 0; i < values.length; i++) {
                if (i != index) {
                    amount = amount + values[i];
                }
            }
            TextBox.value = amount;
        }
        else if (e.keyCode >= 219 && e.keyCode <= 222) {
            for (var i = 0; i < values.length; i++) {
                if (i != index) {
                    amount = amount + values[i];
                }
            }
            TextBox.value = amount;
        }
        else {
            //Check Textbox1 Parameter is null or not
            if (Textbox1 != null) {
                //Copy first Textbox value in Textbox1
                document.querySelector(Textbox1).value = TextBox.value;
            }          
        }
    }
    
}

function SetPercentage(e, TextBox, perTextbox, mmTextbox) {

    var index = TextBox.value.toLowerCase().indexOf(String.fromCharCode(e.keyCode).toLowerCase());
    var values = TextBox.value;
    var strings = TextBox.id.split('_');
    var rowIndex = strings[strings.length-1];
    var amount = "";
    if ((e.keyCode == 190)) {
        var checkDot = 0;
        for (var i = 0; i < values.length; i++) {
            if (values[i] == '.') {
                checkDot = checkDot + 1;
            }
            if (checkDot <= 1) {
                amount = amount + values[i];
            }
        }
        TextBox.value = amount;

    }
    else {
        if (e.keyCode >= 65 && e.keyCode <= 90) {
            for (var i = 0; i < values.length; i++) {
                if (i != index) {
                    amount = amount + values[i];
                }
            }
            TextBox.value = amount;
        }
        else if (e.keyCode >= 106 && e.keyCode <= 109) {
            for (var i = 0; i < values.length; i++) {
                if (i != index) {
                    amount = amount + values[i];
                }
            }
            TextBox.value = amount;
        }
        else if (e.keyCode == 111) {
            for (var i = 0; i < values.length; i++) {
                if (i != index) {
                    amount = amount + values[i];
                }
            }
            TextBox.value = amount;
        }
        else if (e.keyCode >= 186 && e.keyCode <= 189) {
            for (var i = 0; i < values.length; i++) {
                if (i != index) {
                    amount = amount + values[i];
                }
            }
            TextBox.value = amount;
        }
        else if (e.keyCode >= 191 && e.keyCode <= 192) {
            for (var i = 0; i < values.length; i++) {
                if (i != index) {
                    amount = amount + values[i];
                }
            }
            TextBox.value = amount;
        }
        else if (e.keyCode >= 219 && e.keyCode <= 222) {
            for (var i = 0; i < values.length; i++) {
                if (i != index) {
                    amount = amount + values[i];
                }
            }
            TextBox.value = amount;
        }
        else {
            //Check Textbox1 Parameter is null or not
            if (perTextbox != null) {
                //Copy first Textbox value in Textbox1
                if (TextBox.value != "" && document.querySelector(mmTextbox + "_" + rowIndex).value != "") {
                    if (parseFloat(TextBox.value) <= parseFloat(document.querySelector(mmTextbox + "_" + rowIndex).value)) {
                        document.querySelector(perTextbox + "_" + rowIndex).value = (parseFloat(TextBox.value) * 100) / parseFloat(document.querySelector(mmTextbox + "_" + rowIndex).value);
                    }
                    else
                    {
                        document.querySelector(perTextbox + "_" + rowIndex).value = "";
                    }
                }
                else
                {
                    document.querySelector(perTextbox + "_" + rowIndex).value = "";
                }
            }
        }
    }

}

function enableControl(parentControl, controltoValidate, control1, control2, control3, control4, control5, control6, control7, control8, control9, control10, control11) {
    var row = controltoValidate.parentNode.parentNode;
    var rowIndex = row.rowIndex - 1;
    if (controltoValidate.value != "") {

      document.querySelector(parentControl + "_" + control1 + "_" + rowIndex).disabled = false;
      document.querySelector(parentControl + "_" + control2 + "_" + rowIndex).disabled = false;
      document.querySelector(parentControl + "_" + control3 + "_" + rowIndex).disabled = false;
      document.querySelector(parentControl + "_" + control4 + "_" + rowIndex).disabled = false;
      document.querySelector(parentControl + "_" + control5 + "_" + rowIndex).disabled = false;
      document.querySelector(parentControl + "_" + control6 + "_" + rowIndex).disabled = false;
      document.querySelector(parentControl + "_" + control7 + "_" + rowIndex).disabled = false;  
      document.querySelector(parentControl + "_" + control8 + "_" + rowIndex).disabled = false;
      document.querySelector(parentControl + "_" + control9 + "_" + rowIndex).disabled = false;
      document.querySelector(parentControl + "_" + control10 + "_" + rowIndex).disabled = false;
      document.querySelector(parentControl + "_" + control11 + "_" + rowIndex).disabled = false;
      
    }
    else {

        document.querySelector(parentControl + "_" + control1 + "_" + rowIndex).disabled = true;
        document.querySelector(parentControl + "_" + control2 + "_" + rowIndex).disabled = true;
        document.querySelector(parentControl + "_" + control3 + "_" + rowIndex).disabled = true;
        document.querySelector(parentControl + "_" + control4 + "_" + rowIndex).disabled = true;
        document.querySelector(parentControl + "_" + control5 + "_" + rowIndex).disabled = true;
        document.querySelector(parentControl + "_" + control6 + "_" + rowIndex).disabled = true;
        document.querySelector(parentControl + "_" + control7 + "_" + rowIndex).disabled = true;
        document.querySelector(parentControl + "_" + control8 + "_" + rowIndex).disabled = true;
        document.querySelector(parentControl + "_" + control9 + "_" + rowIndex).disabled = true;
        document.querySelector(parentControl + "_" + control10 + "_" + rowIndex).disabled = true;
        document.querySelector(parentControl + "_" + control11 + "_" + rowIndex).disabled = true;
     
    }
}

function enableControlNew(parent, controltoValidate, classvalue)
{
    var parentdiv = document.getElementById(parent);
    var control = parentdiv.querySelectorAll(classvalue);
    //alert(control.length);
    if (controltoValidate.value != "") {
        for (var i = 1; i < control.length; i++) {
            
            control[i].disabled = false;
        }
    }
    else {
        for (var i = 1; i < control.length; i++) {
            control[i].disabled = true;
        }
    }
}

function AddDecimalValue(e, TextBox, putTextbox1, AddwidthTextbox2) {
    var index = TextBox.value.toLowerCase().indexOf(String.fromCharCode(e.keyCode).toLowerCase());
    var values = TextBox.value;
    var amount = "";
    if ((e.keyCode == 190)) {
        var checkDot = 0;
        for (var i = 0; i < values.length; i++) {
            if (values[i] == '.') {
                checkDot = checkDot + 1;
            }
            if (checkDot <= 1) {
                amount = amount + values[i];
            }
        }
        TextBox.value = amount;

    }
    else {
        if (e.keyCode >= 65 && e.keyCode <= 90) {
            for (var i = 0; i < values.length; i++) {
                if (i != index) {
                    amount = amount + values[i];
                }
            }
            TextBox.value = amount;
        }
        else if (e.keyCode >= 106 && e.keyCode <= 109) {
            for (var i = 0; i < values.length; i++) {
                if (i != index) {
                    amount = amount + values[i];
                }
            }
            TextBox.value = amount;
        }
        else if (e.keyCode == 111) {
            for (var i = 0; i < values.length; i++) {
                if (i != index) {
                    amount = amount + values[i];
                }
            }
            TextBox.value = amount;
        }
        else if (e.keyCode >= 186 && e.keyCode <= 189) {
            for (var i = 0; i < values.length; i++) {
                if (i != index) {
                    amount = amount + values[i];
                }
            }
            TextBox.value = amount;
        }
        else if (e.keyCode >= 191 && e.keyCode <= 192) {
            for (var i = 0; i < values.length; i++) {
                if (i != index) {
                    amount = amount + values[i];
                }
            }
            TextBox.value = amount;
        }
        else if (e.keyCode >= 219 && e.keyCode <= 222) {
            for (var i = 0; i < values.length; i++) {
                if (i != index) {
                    amount = amount + values[i];
                }
            }
            TextBox.value = amount;
        }
        else {
            //Check Textbox1 Parameter is null or not
            if (putTextbox1 != null) {
                //Copy first Textbox value in Textbox1
                if (TextBox.value != "" && document.querySelector(AddwidthTextbox2).value!="") {
                    document.querySelector(putTextbox1).value = parseFloat(document.querySelector(AddwidthTextbox2).value) + parseFloat(TextBox.value);
                }
                else if (TextBox.value == "" && document.querySelector(AddwidthTextbox2).value != "") {
                    document.querySelector(putTextbox1).value = document.querySelector(AddwidthTextbox2).value;
                }
                else if (TextBox.value != "" && document.querySelector(AddwidthTextbox2).value == "") {
                    document.querySelector(putTextbox1).value = TextBox.value;
                }
                else {
                    document.querySelector(putTextbox1).value = 0;
                }
            }
        }
    }

}

function CreateBatch(e, TextBox, putTextbox1, AddwidthTextbox2) {
    var index = TextBox.value.toLowerCase().indexOf(String.fromCharCode(e.keyCode).toLowerCase());
    var values = TextBox.value;
    var amount = "";
    if ((e.keyCode == 190)) {
        var checkDot = 0;
        for (var i = 0; i < values.length; i++) {
            if (values[i] == '.') {
                checkDot = checkDot + 1;
            }
            if (checkDot <= 1) {
                amount = amount + values[i];
            }
        }
        TextBox.value = amount;

    }
    else {
        if (e.keyCode >= 65 && e.keyCode <= 90) {
            for (var i = 0; i < values.length; i++) {
                if (i != index) {
                    amount = amount + values[i];
                }
            }
            TextBox.value = amount;
        }
        else if (e.keyCode >= 106 && e.keyCode <= 109) {
            for (var i = 0; i < values.length; i++) {
                if (i != index) {
                    amount = amount + values[i];
                }
            }
            TextBox.value = amount;
        }
        else if (e.keyCode == 111) {
            for (var i = 0; i < values.length; i++) {
                if (i != index) {
                    amount = amount + values[i];
                }
            }
            TextBox.value = amount;
        }
        else if (e.keyCode >= 186 && e.keyCode <= 189) {
            for (var i = 0; i < values.length; i++) {
                if (i != index) {
                    amount = amount + values[i];
                }
            }
            TextBox.value = amount;
        }
        else if (e.keyCode >= 191 && e.keyCode <= 192) {
            for (var i = 0; i < values.length; i++) {
                if (i != index) {
                    amount = amount + values[i];
                }
            }
            TextBox.value = amount;
        }
        else if (e.keyCode >= 219 && e.keyCode <= 222) {
            for (var i = 0; i < values.length; i++) {
                if (i != index) {
                    amount = amount + values[i];
                }
            }
            TextBox.value = amount;
        }
        else {
            //Check Textbox1 Parameter is null or not
            if (putTextbox1 != null) {
                //Copy first Textbox value in Textbox1
                if (TextBox.value != "" && document.querySelector(AddwidthTextbox2).value != "") {
                    document.querySelector(putTextbox1).value = document.querySelector(AddwidthTextbox2).value + "-" + (parseFloat(document.querySelector(AddwidthTextbox2).value) + parseFloat(TextBox.value));
                }
                else {
                    document.querySelector(putTextbox1).value = "";
                }
            }
        }
    }

}

function CheckIntegerValueonKeyUp(e, TextBox, Textbox1) {
    var index = TextBox.value.toLowerCase().indexOf(String.fromCharCode(e.keyCode).toLowerCase());
    var values = TextBox.value;
    var amount = "";

    if (e.keyCode >= 65 && e.keyCode <= 90) {
        for (var i = 0; i < values.length; i++) {
            if (i != index) {
                amount = amount + values[i];
            }
        }
        TextBox.value = amount;
    }
    else if (e.keyCode >= 106 && e.keyCode <= 109) {
        for (var i = 0; i < values.length; i++) {
            if (i != index) {
                amount = amount + values[i];
            }
        }
        TextBox.value = amount;
    }
    else if (e.keyCode == 111) {
        for (var i = 0; i < values.length; i++) {
            if (i != index) {
                amount = amount + values[i];
            }
        }
        TextBox.value = amount;
    }
    else if (e.keyCode >= 186 && e.keyCode <= 189) {
        for (var i = 0; i < values.length; i++) {
            if (i != index) {
                amount = amount + values[i];
            }
        }
        TextBox.value = amount;
    }
    else if (e.keyCode >= 191 && e.keyCode <= 192) {
        for (var i = 0; i < values.length; i++) {
            if (i != index) {
                amount = amount + values[i];
            }
        }
        TextBox.value = amount;
    }
    else if (e.keyCode >= 219 && e.keyCode <= 222) {
        for (var i = 0; i < values.length; i++) {
            if (i != index) {
                amount = amount + values[i];
            }
        }
        TextBox.value = amount;
    }
    else if (e.keyCode == 190 || e.keyCode == 110) {
        for (var i = 0; i < values.length; i++) {
            if (values[i] != '.') {
                amount = amount + values[i];
            }
        }
        TextBox.value = amount;
    }
    else {
        //Check Textbox1 Parameter is null or not
        if (Textbox1 != null) {
            //Copy first Textbox value in Textbox1
            document.querySelector(Textbox1).value = TextBox.value;
        }
    }

}

function CheckIntegerValueonBlur(TextBox, maxvalue) {
    if (TextBox.value == "") {
        TextBox.value = maxvalue;
    }
}

function IsValidEmail(email) {
    var expr = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
    return expr.test(email);
};
function ValidateEmails(Textbox) {
    //Check Textbox Parameter is null or not
    if (Textbox.value.length > 0 && Textbox != null) {
        var email = Textbox.value;
        if (!IsValidEmail(email)) {
            alert("Invalid email address.");
            Textbox.value = "";
            Textbox.focus();
        }
    }
}



function ValidateEmail(Textbox, copyTotexTbox) {
    //Check Textbox Parameter is null or not
    if (Textbox.value.length > 0 && Textbox!=null) {
        var email = Textbox.value;
        if (!IsValidEmail(email)) {
            alert("Invalid email address.");
            Textbox.value = "";
            Textbox.focus();
        }
        else {
            //Check copyTotexTbox Parameter is null or not
            if (copyTotexTbox != null) {
                //Copy first Textbox value in copyTotexTbox
                document.querySelector(copyTotexTbox).value = Textbox.value;
            }
        }
    }
}
function CopyTextBox(Textbox, copyTotexTbox) {
        document.querySelector(copyTotexTbox).value = Textbox.value;
}

function CopyTextBoxNew(Textbox, copyTotexTbox, copyToThreeCharTextbox) {
    // Pure value copy
    document.querySelector(copyTotexTbox).value = Textbox.value;

    // Sirf first 3 characters copy
    document.querySelector(copyToThreeCharTextbox).value = Textbox.value.substring(0, 3);
}
function CopyText(Textbox, copyTotexTbox, drpCheck, checkdrpValue) {
    //Check drpCheck Parameter is null or not
    if (drpCheck != null) {
        var dropdown = document.querySelector(drpCheck);
        if (dropdown.options[dropdown.selectedIndex].value == checkdrpValue) {
            //Copy first Textbox value in copyTotexTbox
            document.querySelector(copyTotexTbox).value = Textbox.value;
        }
    }
    else {
        //Copy first Textbox value in copyTotexTbox
        document.querySelector(copyTotexTbox).value = Textbox.value;
    }
}

function CheckUncheck(parentcontrol, checkbox)
{
    var div = document.getElementById(parentcontrol);
    var inputList = div.getElementsByTagName('input');
    //The First element is the Header Checkbox
    var headerCheckBox = inputList[0];
 
    if (headerCheckBox == checkbox) {
   
        for (var i = 0; i < inputList.length; i++) {
            //Based on all or none checkboxes
            //are checked check/uncheck Header Checkbox
            if (inputList[i].type === 'checkbox' && inputList[i] != headerCheckBox && inputList[i].style.backgroundColor != "#Red") {
                inputList[i].checked = headerCheckBox.checked;
            }
        }
    }
    else {

        for (var i = 0; i < inputList.length; i++) {
            //Based on all or none checkboxes
            //are checked check/uncheck Header Checkbox
            var checked = true;
            if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {
                if (!inputList[i].checked) {
                    checked = false;
                    break;
                }
            }
        }
        headerCheckBox.checked = checked;
    }
}


function ChecktenDigitMobileNumber(inputtxt)  
{
    var phoneno = /^\d{10}$/;
    if (inputtxt.value.match(phoneno) && inputtxt != null)
    {
        inputtxt.style.border = "1px solid #D5D5D5";
        return true;  
    }  
    else  
    {
        if (inputtxt.value != "")
        {
       
            inputtxt.style.border = "1px solid Red";
            inputtxt.value = "";
            inputtxt.focus();
            return false;
        }
    }  
}
function ChecktwellebDigitMobileNumber(inputtxt) {
    var phoneno = /^\d{12}$/;
    if (inputtxt.value.match(phoneno) && inputtxt != null) {
        inputtxt.style.border = "1px solid #D5D5D5";
        return true;
    }
    else {
        if (inputtxt.value != "") {

            inputtxt.style.border = "1px solid Red";
            inputtxt.value = "";
            inputtxt.focus();
            return false;
        }
    }
}
function CheckDigitMobileNumber(inputtxt) {
    var phoneno = /^\d+$/;
    if (inputtxt.value.match(phoneno) && inputtxt != null) {
        return true;
    }
    else {
        if (inputtxt.value != "") {
            alert("Please enter only numbers and maximum 5 digits!");
            inputtxt.value = "";
            inputtxt.focus();
            return false;
        }
    }
}
         
function ChecktenDigitNumber(inputtxt) {
    var phoneno = /^\d+$/;
    if (inputtxt.value.match(phoneno) && inputtxt != null) {
        inputtxt.style.border = "1px solid #D5D5D5";
        return true;
    }
    else {
        if (inputtxt.value != "") {
            inputtxt.style.border = "1px solid Red";
            inputtxt.value = "";
            inputtxt.focus();
            return false;
        }
    }
}


function CheckDecimalNumber(inputtxt) {
    var exp = /^(\+|-)?(\d*\.?\d*)$/;
    if (inputtxt.value.match(exp) && inputtxt != null) {
        inputtxt.style.border = "1px solid #D5D5D5";
        return true;
    }
    else {
        if (inputtxt.value != "") {
            inputtxt.style.border = "1px solid Red";
            alert("Please enter only numbers!");
            inputtxt.value = "0.00";
            inputtxt.focus();
            return false;
        }
    }
}

function CreateShortName(textBox,txtShortName)
{
    var shortname = "";
    var value = textBox.value.split(' ');
    for (var i = 0; i < value.length; i++) {
        if (value == "") {
            if (value[i].toUpperCase() != "AND") {
                if (value[i].charAt(0) != "&") {
                    shortname = value[i].charAt(0);
                }
            }
        }
        else {
            if (value[i].toUpperCase() != "AND") {
                if (value[i].charAt(0) != "&") {
                    shortname = shortname + value[i].charAt(0);
                }
            }
        }
    }
    document.querySelector(txtShortName).value = shortname.toUpperCase();
    
}

function CopyString(parentControl, fromControl, toControl) {

    document.getElementById(parentControl + toControl).value = fromControl.value;
}

function HideControl(mainControlid, values, controlid1, controlid2, controlid3, controlid4) {
    
    if (controlid1 != null) {
        var control1 = document.getElementById(controlid1);
        if (mainControlid.value == values) {
            control1.style.visibility = "hidden";
        }
        else {
            control1.style.visibility = "visible";
        }
    }

    if (controlid2 != null) {
        var control2 = document.getElementById(controlid2);
        if (mainControlid.value == values) {
            control2.style.visibility = "hidden";
        }
        else {
            control2.style.visibility = "visible";
        }
    }

    if (controlid3 != null) {
        var control3 = document.getElementById(controlid3);
        if (mainControlid.value == values) {
            control3.style.visibility = "hidden";
        }
        else {
            control3.style.visibility = "visible";
        }
    }

    if (controlid4 != null) {
        var control4 = document.getElementById(controlid4);
        if (mainControlid.value == values) {
            control4.style.visibility = "hidden";
        }
        else {
            control4.style.visibility = "visible";
        }
    }
    
    

}

function ShowControl(mainControlid, values, controlid1) {

    if (controlid1 != null) {
        var control1 = document.getElementById(controlid1);
        if (mainControlid.value == values) {
            control1.style.visibility = "visible";           
        }
        else {
            control1.style.visibility = "hidden";
        }
    }
}

function ShowhidehtmlControl(mainControl, controlid1, controlid2, value1, value2)
{
    if (mainControl != null) {
        var control1 = document.getElementById(controlid1);
        var control2 = document.getElementById(controlid2);
        var AspRadio_ListItem = mainControl.getElementsByTagName('input');
        for (var i = 0; i < AspRadio_ListItem.length; i++) {
            if (AspRadio_ListItem[i].checked)
                if (AspRadio_ListItem[i].value == value1) {
                    control1.style.display = "block";
                    control2.style.display = "none";
                    break;
                }
                else if (AspRadio_ListItem[i].value == value2) {
                    control2.style.display = "block";
                    control1.style.display = "none";
                    break;
                }
        }
    }
}

function SelectDropDownValue(mainControlid,ParentId, values,onTrue,onFalse, controlid1) {

    if (controlid1 != null) {
        var control1 = document.getElementById(ParentId+controlid1);
        if (mainControlid.value == values) {
            control1.value = onTrue;
        }
        else {
            control1.value = onFalse;
        }
    }
}

function validationFordiscountValue(mainControlid, values) {

    if (mainControlid != null) {
        var flag;
        flag = mainControlid.value > 0 ? mainControlid.value > values ? "max" : "" : "min"
        if (flag=="max") {
            alert("Value should exist b/w 1 to " + values);
            mainControlid.value = values;
            mainControlid.focus();
        }
        else if (flag == "min") {
            alert("Value should exist b/w 1 to " + values);
            mainControlid.value = 1;
            mainControlid.focus();
        }
    }
}

function setFocus(Control) {
    document.getElementById(Control).focus();
}

function CopyGridHeaderText(textbox,ControlId) {
    try {
        var GridView1 = document.getElementById(ControlId);
        var amount = textbox.value;
        for (var i = 1; i < GridView1.rows.length; i++) {
            GridView1.rows[i].cells[1].getElementsByTagName("input")[0].value = amount;
        }

    }
    catch (err) {
        alert(err.message);
    }
}

function CopyRepeaterHeaderText(textbox, ControlId) {
    try {
        var amount = textbox.value;
        var Repeater1 = document.getElementById(ControlId);
        var Repeater_item = Repeater1.getElementsByTagName('tr');
        for (var i = 1; i < Repeater_item.length; i++) {
            var gg = Repeater_item[i].getElementsByTagName('td')[1];
            gg.getElementsByTagName('input')[0].value = amount;
        }

    }
    catch (err) {
        alert(err.message);
    }
}

function ValidatorUpdateDisplayss(val) {
    if (val.isvalid) {
        alert(val.isvalid);
        document.getElementById(val.controltovalidate).style.border = '1px solid #CCC';
    }
    else {
        alert(val.isvalid);
        document.getElementById(val.controltovalidate).style.border = '1px solid red';
    }
}
var globalflag = true;
var globalflag1 = true;
var globalflag2 = true;
var globalflag3 = true;
var globalflag4 = true;
var globalflag5 = true;

function ValidateDropdown(drpclass) {
    var localflag = true;
    var dropDown = document.querySelectorAll(drpclass)
    for (var i = 0; i < dropDown.length; i++)
    {
        if (dropDown[i].selectedIndex == 0)
        {
           
            dropDown[i].style.border = '1px solid red';
            dropDown[i].focus()
            localflag = false;
        }
        else {
            dropDown[i].style.border = '1px solid #CCC';
            dropDown[i].focus();
        }
    }
  
    globalflag1 = localflag;

    return globalflag1;
}

function ValidateRepeater(control) {
    var localflag = true;
    var Repeater = document.getElementById(control);
    var Repeater_row = Repeater.getElementsByTagName('tr');
    for (var i = 1; i < Repeater_row.length; i++) {
        if (Repeater_row[i].getElementsByTagName('td')[1].getElementsByTagName('input')[0].value == "") {
            Repeater_row[i].getElementsByTagName('td')[1].getElementsByTagName('input')[0].style.border = '1px solid red';
            localflag = false;
        }
        else {
            Repeater_row[i].getElementsByTagName('td')[1].getElementsByTagName('input')[0].style.border = '1px solid #CCC';
        }
    }
    if (globalflag != false) {
        globalflag = localflag;
    }
    return globalflag;
}

function validationReturn(btn) {

    if (globalflag1 == true && globalflag2 == true && globalflag3 == true && globalflag4 == true && globalflag5==true) {
        globalflag = true;
    }
    else {
        globalflag = false;
    }
    if (globalflag == true) {
        desableButton(btn);
    }
    //alert(globalflag);
    return globalflag;
}

function desableButton(btnSubmit) {
    if (btnSubmit != null) {
        btnSubmit.style.display = "none";
    }
}

function visibleFalseTableColumn(columnId, isVisible) {
    try
    {
        var rd = isVisible.getElementsByTagName("input");
        var col = document.getElementById(columnId);
        for (var i = 0; i < rd.length; i++) {
            if (rd[i].checked) {
                if (rd[i].value == "Yes") {
                    col.style.display = "block";
                }
                else {
                    col.style.display = "none";
                }
            }

        }
    }
    catch (err) {
        //alert(err.message);
    }
}

function visibleFalseTableColumn1(columnId, isVisible) {
    try {
        var rd = document.getElementById(isVisible).getElementsByTagName("input");
        var col = document.getElementById(columnId);
        for (var i = 0; i < rd.length; i++) {
            if (rd[i].checked) {
                if (rd[i].value == "Yes") {
                    col.style.display = "block";
                }
                else {
                    col.style.display = "none";
                }
            }

        }
    }
    catch (err) {
        //alert(err.message);
    }
}

function ValidateTextBox(txtBox) {
    var localflag = true;
    var count = -1;
    var textBox = document.querySelectorAll(txtBox)
    for (var i = 0; i < textBox.length; i++) {
        if (textBox[i].value == '') {
            textBox[i].style.border = '1px solid red';
            localflag = false;
            if (count == -1) {
                textBox[i].focus();
                count = i;
            }
        }
        else {
            textBox[i].style.border = '1px solid #CCC';
        }
    }
    globalflag2 = localflag;
    return globalflag2;
}

function ValidateTextBoxUsingId(txtBox) {
    var localflag = true;
    var textBox = document.getElementById(txtBox)

    if (textBox.value == '') {
        textBox.style.border = '1px solid red';
        textBox.focus();
        localflag = false;
    }
    else {
        textBox.style.border = '1px solid #CCC';
    }

    if (globalflag == true) {
        globalflag = localflag;
    }
    return globalflag;
}

function EmptyTextBox(listoftxtbox) {
    var listoftxtBox = listoftxtbox.split(',');
    //alert(listoftxtBox);
    for (var i = 0; i < listoftxtBox.length; i++) {
        document.getElementById(listoftxtBox[i]).value = "";
    }
}

function ValidateRadiobuttonList(rblist) {
    try{
        var rablist = document.querySelectorAll(rblist)
        var flag = [];
        var flag1 = true;
        for (var i = 0; i < rablist.length; i++) {
            var localflag = false;
            var inputitem = rablist[i].getElementsByTagName("input");
            for (var j = 0; j < inputitem.length; j++) {
     
                if (inputitem[j].checked == true) {
                    localflag = true;
                    break;
                }
            }
            flag[i] = localflag;
            if (localflag == false) {
                rablist[i].className = "vd_radio vd_radio_red radio-success validaterb";
                         
            }
            else {
                rablist[i].className = "vd_radio radio-success validaterb";               
            }
        }
        for (var i = 0; i < flag.length; i++)
        {
            if (flag[i] == false) {
                flag1 = false;
            }
        }
        globalflag3 = flag1;
    }
    catch(e)
    {
        alert(e.message);
    }
    return globalflag3;
}

function ValidateCheckBoxList(cblist) {
    try {
        var cblist = document.querySelectorAll(cblist)
        var flag = [];
        var flag1 = true;
        for (var i = 0; i < cblist.length; i++) {
            var localflag = false;
            var inputitem = cblist[i].getElementsByTagName("input");
            for (var j = 0; j < inputitem.length; j++) {

                if (inputitem[j].checked == true) {
                    localflag = true;
                    break;
                }
            }
            flag[i] = localflag;
            if (localflag == false) {
                cblist[i].className = "vd_checkbox vd_checkbox_red checkbox-success validatecb";

            }
            else {
                cblist[i].className = "vd_checkbox checkbox-success validatecb";
            }
        }
        for (var i = 0; i < flag.length; i++) {
            if (flag[i] == false) {
                flag1 = false;
            }
        }
        globalflag4 = flag1;
    }
    catch (e) {
        alert(e.message);
    }
    return globalflag4;
}

function onlyAlphabets(e, t) {
    try {
        if (window.event) {
            var charCode = window.event.keyCode;
        }
        else if (e) {
            var charCode = e.which;
        }
        else { return true; }
        if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123))
            return true;
        else
            return false;
    }
    catch (err) {
        alert(err.Description);
    }
}

function button_click(objTextBox, objBtnID) {
    if (window.event.keyCode == 13) {
        document.getElementById(objBtnID).focus();
        document.getElementById(objBtnID).click();
    }
}

function SetCursorToTextEndandSepratebyComma(textControlID) {
    var text = document.getElementById(textControlID);
    if (text.value.charAt(text.value.length-1) != ',') {
        text.value = text.value + ",";
    }
    text.focus();
    text.selectionStart = text.selectionEnd = text.value.length;
}

function SetCursorToTextEnd(textControlID) {
    var text = document.getElementById(textControlID);
    text.focus();
    text.selectionStart = text.selectionEnd = text.value.length;
}

function button_click_onblur(objTextBox, objBtnID) {
    document.getElementById(objBtnID).focus();
    document.getElementById(objBtnID).click();
}

function button_click_ontabpress(objTextBox, objBtnID) {
    if (window.event.keyCode == 9) {
        document.getElementById(objBtnID).focus();
        document.getElementById(objBtnID).click();
    }

}
//$('[data-toggle^="tooltip"]').tooltip();
//function tooltip() {
//    $('[data-toggle^="tooltip"]').addClass('tooltip1');
//}

function datetime() {
   
    $(".datepicker-normal").datepicker({ dateFormat: 'dd-M-yy', changeYear: true, changeMonth: true, yearRange: '-50:+10' });
    if ($('.datepicker-normal').val() == "" && !$('.datepicker-normal').hasClass("dateblank")) {
        $('.datepicker-normal').datepicker('setDate', new Date());
    }
    else {
        $(".datepicker-normal").datepicker({ dateFormat: 'dd-M-yy', changeYear: true, changeMonth: true, yearRange: '-50:+10' });
    }
    if ($('.datepicker-normal').hasClass("currDate")) {
        $('.currDate').datepicker('setDate', new Date());
    }
}
function datetimenew() {
    $(".datepicker-normal").datepicker({ dateFormat: 'dd-M-yy', changeYear: true, changeMonth: true, yearRange: '-50:+10' });
}
function prettyphoto() {
    $('a[data-rel^="prettyPhoto"]').each(function () {
        $(this).attr('rel', $(this).data('rel'));
    });
    $("a[rel^='prettyPhoto']").prettyPhoto({ theme: 'light_square' });
}

function txtAreaHtml() {
  
    var uiSummernote = function () {
        /* Extended summernote editor */
        if ($(".summernote").length > 0) {
            $(".summernote").summernote({
                height: 250,
                codemirror: {
                    mode: 'text/html',
                    htmlMode: true,
                    lineNumbers: true,
                    theme: 'default'
                }
            });
        }
        /* END Extended summernote editor */

        /* Lite summernote editor */
        if ($(".summernote_lite").length > 0) {

            $(".summernote_lite").on("focus", function () {

                $(".summernote_lite").summernote({
                    height: 100, focus: true,
                    toolbar: [
                        ["style", ["bold", "italic", "underline", "clear"]],
                        ["insert", ["link", "picture", "video"]]
                    ]
                });
            });
        }
        /* END Lite summernote editor */
       
        /* Email summernote editor */
       
        if ($(".summernote_email").length > 0) {

            $(".summernote_email").summernote({
                height: 400, focus: true,
                toolbar: [
                    ['style', ['bold', 'italic', 'underline', 'clear']],
                    ['font', ['strikethrough']],
                    ['fontsize', ['fontsize']],
                    ['color', ['color']],
                    ['para', ['ul', 'ol', 'paragraph']],
                    ['height', ['height']],
                    ["insert", ["link"]]
                ]
            });

        }
        /* END Email summernote editor */

    }// END Summernote 

    uiSummernote();
}


function scrollbar() {
    $('html.no-touch [data-rel^="scroll"]').mCustomScrollbar({
        set_height: function () { $(this).css('max-height', $(this).data('scrollheight')); return $(this).data('scrollheight'); },
        mouseWheel: "auto",
        autoDraggerLength: true,
        autoHideScrollbar: true,
        advanced: {
            updateOnBrowserResize: true,
            updateOnContentResize: true
        }, // removed extra commas 
    });
    $('html.touch [data-rel^="scroll"]').css({
        'height': function () { return ($(this).data('scrollheight')) },
        'max-height': function () { return ($(this).data('scrollheight')) },
        'overflow-y': 'scroll',
    });

}

function minimize() {
    $('[data-action^="minimize"]').click(function () {
        if ($(this).hasClass('active')) {
            $(this).removeClass('active');
            $(this).closest(".widget").children('.minimize').slideDown('slow');
        } else {
            $(this).addClass('active');
            $(this).closest(".widget").children('.minimize').slideUp('slow');
        }
    });


   
function msgboxnew(divmsgboxid, msg, msgsymbol) {
            var divmsgbox = document.getElementById(divmsgboxid);
            divmsgbox.InnerHtml = "";
            var background = "";
            var icon = "";
            switch (msgsymbol) {
            case "S":
                background = "vd_bg-green";
                icon = "fa-check";
                break;
            case "U":
                background = "vd_bg-green";
                icon = "fa-check";

                break;
            case "A":
                background = "vd_bg-yellow";
                icon = "fa-exclamation-triangle";
                break;
            case "W":
                background = "vd_bg-red";
                icon = "fa-times";
                break;
            default:
                divmsgbox.InnerHtml = "";
                break;
            }

            enable(divmsgboxid, background, icon, msg);
        }

    function enable(divmsgboxid, background, icon, msg) {
        var hide = document.getElementById(divmsgboxid); hide.className = 'msgbox ' + background + ' animated  fadeInLeft'; hide.innerHTML = '<i class=fa' + icon + 'aria-hidden=true></i> ' + msg;
        function disable() {
            var hide = document.getElementById(divmsgboxid); if (hide.innerHTML !== '') {
                hide.className = 'msgbox msgbox-bx-n-z-n ' + background + 'animated fadeInRight-dn';
                setTimeout(clear, 5000);
            }
        } function clear() {
            var hide = document.getElementById(divmsgboxid);
            hide.className = ''; hide.innerHTML = '';
        } function jscript() { setTimeout(disable, 10000); }
    }
   


}












              