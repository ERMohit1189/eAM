<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdmissionFeePaymentFailed.aspx.cs" Inherits="AdmissionFeePaymentFailed" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>eAM</title>
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
                        <h2 style="color: #ff0000">Ohh! Sorry..</h2>
                        <h3>Payment has been faild.</h3>
                        <p style="font-size: 20px; color: #5C5C5C;">
                            Dear user payment has been faild due to reason of cancel or any technical problem.
                        </p>
                            <a href="../2/paf.aspx" class="btn btn-success">Go Back to Admission Form</a>
                        <br>
                        <br>
                    </div>

                </div>
            </div>
        </div>
    </form>
</body>
</html>
