<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WebHook.aspx.cs" Inherits="WebHook" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="js/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $.ajax({
                type: "GET",
                url: "https://www.payumoney.com/payment/payment/chkMerchantTxnStatus?merchantKey=xbY94EBE&merchantTransactionIds=1412f26a0d004ddef776",
                contentType: "application/json",
                dataType: "json",
                headers: {
                    "Authorization": "oyatwOMyDejvk7NRfdBqltJTjvpc+99pQY/TJKOYS7g="
                },
                success: function (data) {
                    alert(data);
                    // do something!
                    console.log(data);
                }
            });
        //$.ajax({
        //    type: "POST",
        //    url: "https://www.payumoney.com/payment/payment/chkMerchantTxnStatus?merchantKey=xbY94EBE&merchantTransactionIds=1412f26a0d004ddef776&Authorization=oyatwOMyDejvk7NRfdBqltJTjvpc+99pQY/TJKOYS7g=&Hcache-control:no-cache",
        //    //url: "https://www.payumoney.com/payment/payment/chkMerchantTxnStatus?merchantKey=xbY94EBE&merchantTransactionIds=1412f26a0d004ddef776",
        //    contentType: "application/json",
        //    //data: { merchantKey: "xbY94EBE", merchantTransactionIds: "1412f26a0d004ddef776"},
        //    success: function (result) {
        //        if (result != null) {
        //            var data = result.d;
        //            alert("v");
        //            //$('#empCode').text(data.EmployeeCode);
        //            //$('#empName').text(data.EmployeeName);
        //        }
        //    },
        //    error: function (err) {
        //        alert(err.statusText);
        //    }
        //});
    });
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
