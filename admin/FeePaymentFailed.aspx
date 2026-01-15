<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FeePaymentFailed.aspx.cs" Inherits="FeePaymentFailed" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Payment Failed | eAM&reg;</title>
 <link href="//maxcdn.bootstrapcdn.com/bootstrap/3.3.0/css/bootstrap.min.css" rel="stylesheet" />
<script src="//maxcdn.bootstrapcdn.com/bootstrap/3.3.0/js/bootstrap.min.js"></script>
<script src="//code.jquery.com/jquery-1.11.1.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="container">
                <div class="row text-center">
                    <div class="col-sm-6 col-sm-offset-3">
                        <br>
                        <br>
                        <h2 style="color: #ff0000">Ohh!</h2>
                        <h3>Payment has been failed.</h3>
                        <p style="font-size: 20px; color: #5C5C5C;">
                            Dear user payment has been failed due to reason of cancel or any technical problem.
                        </p>
                        <a href="../Default.aspx" class="btn btn-success">Go Back to Login</a>
                        <br>
                        <br>
                    </div>

                </div>
            </div>
        </div>
    </form>
</body>
</html>
