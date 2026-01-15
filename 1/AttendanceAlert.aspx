<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="AttendanceAlert.aspx.cs" Inherits="_1_AttendanceAlert" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <script src="../js/angularjs/angular.js"></script>

    <script>
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
            var hide = document.getElementById(divmsgboxid);
            hide.className = 'msgbox ' + background + ' animated  fadeInLeft';
            hide.innerHTML = '<i class=fa' + icon + 'aria-hidden=true></i> ' + msg;
            jscript();
            function disable() {
                var hide = document.getElementById(divmsgboxid); if (hide.innerHTML !== '') {
                    hide.className = 'msgbox msgbox-bx-n-z-n ' + background + 'animated fadeInRight-dn';
                    setTimeout(clear, 5000);
                }
            }

            function clear() {
                var hide = document.getElementById(divmsgboxid);
                hide.className = ''; hide.innerHTML = '';
            }

            function jscript() { setTimeout(disable, 10000); }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">


    <div class="vd_content-section clearfix">
        <div class="row">
            <div class="col-sm-12 mgbt-xs-20">
                <div class="panel widget light-widget panel-bd-top">
                    <div class="panel-body">
                        <div ng-app="myapp" ng-controller="mycontroller">
                            <div class="col-sm-12  no-padding">
                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                    <label class="control-label">Attendance Value&nbsp;<span class="vd_red">* </span></label>
                                    <div class="">
                                        <select ng-model="selectAttendancevalue" ng-options="value for value in attendancevalue" ng-change="FillList()"></select>
                                        <div class="text-box-msg">
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                    <label class="control-label">Date&nbsp;<span class="vd_red">* </span></label>
                                    <div class="">
                                        <asp:TextBox ID="TextBox1" runat="server" CssClass="datepicker-normal"
                                            ng-model="fromdate" ng-change="FillList();"></asp:TextBox>
                                        <div class="text-box-msg">
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                    <label class="control-label">&nbsp;<span class="vd_red"> </span></label>
                                    <div class="">
                                        <input type="button" class="button" value="View" ng-click="Click()" ng-disabled="btndisabled" />
                                        <div class="text-box-msg">
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-sm-12  ">
                                <div class=" table-responsive  table-responsive2">
                                    <table class="table table-striped table-hover no-bm no-head-border table-bordered pro-table table-text-center table-header-group">
                                        <thead>
                                            <tr>
                                                <th>#</th>
                                                <th>S.R. No.</th>
                                                <th>Student's Name</th>
                                                <th>Father's Name</th>
                                                <th>Mobile No.</th>
                                                <th>SMS</th>
                                                <th>
                                                    <input type="checkbox" ng-model="master" ng-init="master=true">
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr ng-repeat="student in StudentsList | orderBy: 'studentname'">
                                                <td>{{$index+1}}</td>
                                                <td>{{student.srno}}</td>
                                                <td>{{student.StudentName}}</td>
                                                <td>{{student.FatherName}}</td>
                                                <td ng-model="contactno">{{student.MobileNo}}</td>
                                                <td>
                                                    <textarea rows="2" id="{{$index}}" ng-bind="'Dear, Guardian your ward ' +
                                    student.StudentName +' ('+
                                    student.srno +') '+ 'is ' +
                                    student.AttendanceName + ' on ' +
                                    student.AttendanceDate"></textarea>
                                                </td>
                                                <td>
                                                    <input type="checkbox" id="{{student.srno}}" ng-checked="master"></td>
                                            </tr>
                                        </tbody>
                                    </table>

                                    <div class="col-sm-12  mgbt-xs-15 no-padding">
                                        <input type="button" class="button" ng-click="SendSMS(StudentsList);" value="Send" />
                                        <div id="divmsg" style="left: 75px"></div>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>

    <script>
        var app = angular.module("myapp", []);
        app.controller("mycontroller", function ($scope, $http) {
            var date = new Date().format('yyyy MMM dd');
            $scope.fromdate = date;
            $scope.attendancevalue = ['All', 'P', 'A', 'Lt'];
            $scope.selectAttendancevalue = $scope.attendancevalue[0];
            $scope.Click = function () {
                $scope.btndisabled = true;
                var datetexbox = document.getElementById("ContentPlaceHolder1_ContentPlaceHolderMainBox_TextBox1");
                $scope.fromdate = datetexbox.value;
                $scope.FillList();
            };
            $scope.SendSMS = function (StdList) {

                for (var i = 0; i < StdList.length; i++) {
                    var chk = document.getElementById($scope.StudentsList[i]["srno"]);
                    var areatextbox = document.getElementById(i);
                    if (chk.checked) {
                  
                        var httpreq = {
                            method: 'POST',
                            url:'AttendanceAlert.aspx/SendSMS',
                            headers: {
                                'content-type': 'application/json; charset=utf-8',
                                'dataType': 'json'
                            },
                            data: { msg: areatextbox.value, contactno: $scope.StudentsList[i]["MobileNo"] }

                        };
                        $http(httpreq)
                        .success(function (response) {
                     
                            if (response.d == "PDA")
                            {
                                msgboxnew('divmsg',"Sorry, SMS Panel is Deactivated!", 'A');
                            }
                            else
                            {
                                msgboxnew('divmsg', "Message Send Successfully.", 'S');
                            }
                        });
                    }
                };
            };
            $scope.FillList = function () {
                if ($scope.fromdate != null) {
                    $scope.studentname = 'StudentName';
                    var httpreq = {
                        method: 'POST',
                        url: 'AttendanceAlert.aspx/GetStudentAttendance',
                        headers: {
                            'Content-Type': 'application/json; charset=utf-8',
                            'dataType': 'json'
                        },
                        data: { FromDate: $scope.fromdate, AttendanceValue: $scope.selectAttendancevalue }
                    }

                    $http(httpreq)
                        .success(function (response) {
                            $scope.StudentsList = response.d;
                        })
                };
                $scope.btndisabled = false;
            }
            $scope.FillList();
        });

    </script>

</asp:Content>

