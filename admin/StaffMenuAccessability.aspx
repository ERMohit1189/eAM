<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="StaffMenuAccessability.aspx.cs" Inherits="SuperAdmin_menu_accessability" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <script type="text/javascript">
        function OnTreeClick(evt) {
            var src = window.event !== window.undefined ? window.event.srcElement : evt.target;
            var isChkBoxClick = (src.tagName.toLowerCase() === "input" && src.type === "checkbox");
            if (isChkBoxClick) {
                var parentTable = GetParentByTagName("table", src);
                var nxtSibling = parentTable.nextSibling;
                //check if nxt sibling is not null & is an element node
                if (nxtSibling && nxtSibling.nodeType === 1) {
                    //if node has children    
                    if (nxtSibling.tagName.toLowerCase() === "div") {
                        //check or uncheck children at all levels
                        CheckUncheckChildren(parentTable.nextSibling, src.checked);
                    }
                }
                //check or uncheck parents at all levels
                //CheckUncheckParents(src, src.checked);
            }
        }

        function CheckUncheckChildren(childContainer, check) {
            var childChkBoxes = childContainer.getElementsByTagName("input");
            var childChkBoxCount = childChkBoxes.length;
            for (var i = 0; i < childChkBoxCount; i++) {
                childChkBoxes[i].checked = check;
            }
        }

        function CheckUncheckParents(srcChild, check) {
            var parentDiv = GetParentByTagName("div", srcChild);
            var parentNodeTable = parentDiv.previousSibling;
            if (parentNodeTable) {
                var checkUncheckSwitch;
                //checkbox checked 
                if (check) {
                    var isAllSiblingsChecked = AreAllSiblingsChecked(srcChild);
                    if (isAllSiblingsChecked)
                        checkUncheckSwitch = true;
                    else
                        return; //do not need to check parent if any(one or more) child not checked
                }
                else //checkbox unchecked
                {
                    checkUncheckSwitch = false;
                }

                var inpElemsInParentTable = parentNodeTable.getElementsByTagName("input");
                if (inpElemsInParentTable.length > 0) {
                    var parentNodeChkBox = inpElemsInParentTable[0];
                    parentNodeChkBox.checked = checkUncheckSwitch;
                    //do the same recursively
                    CheckUncheckParents(parentNodeChkBox, checkUncheckSwitch);
                }
            }
        }

        function AreAllSiblingsChecked(chkBox) {
            var parentDiv = GetParentByTagName("div", chkBox);
            var childCount = parentDiv.childNodes.length;
            for (var i = 0; i < childCount; i++) {
                if (parentDiv.childNodes[i].nodeType === 1) {
                    //check if the child node is an element node
                    if (parentDiv.childNodes[i].tagName.toLowerCase() === "table") {
                        var prevChkBox = parentDiv.childNodes[i].getElementsByTagName("input")[0];
                        //if any of sibling nodes are not checked, return false
                        if (!prevChkBox.checked) {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        //utility function to get the container of an element by tagname
        function GetParentByTagName(parentTagName, childElementObj) {
            var parent = childElementObj.parentNode;
            while (parent.tagName.toLowerCase() !== parentTagName.toLowerCase()) {
                parent = parent.parentNode;
            }
            return parent;
        }
    </script>
    <style>
        input[type=checkbox] {
            margin-right: 5px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12 no-padding">
                                    <div class="col-sm-3 mgbt-xs-15 hide">
                                        <label class="control-label">Select Type</label>
                                        <asp:DropDownList ID="ddltype" runat="server" AutoPostBack="True" CssClass="form-control-blue" OnSelectedIndexChanged="ddltype_SelectedIndexChanged">
                                            <asp:ListItem Value="">Select</asp:ListItem>
                                            <asp:ListItem Value="3" Selected="True">Staff</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-3 mgbt-xs-15">
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <label class="control-label">Select User&nbsp;<span class="vd_red">*</span></label>
                                                <asp:DropDownList ID="ddlUser" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlUser_SelectedIndexChanged" CssClass="form-control-blue">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    
                                    <div class="col-sm-3  btn-a-devices-1-p4-p2 mgbt-xs-15" runat="server" id="div4" visible="false">
                                        <asp:LinkButton ID="Button1" runat="server" OnClick="Button1_Click" CssClass="button">Submit</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 74px !important;"></div>
                                    </div>
                                    <div class="col-sm-12" runat="server" id="div2" visible="false">
                                        <label class="control-label">Select desired modules</label>
                                    </div>


                                    <div class="col-sm-4  mgbt-xs-15" runat="server" id="div3" visible="false">
                                        <asp:TreeView ID="TreeView1" runat="server" ForeColor="Black" class="check-title-set"
                                            ShowCheckBoxes="All" ShowLines="True">
                                        </asp:TreeView>
                                    </div>

                                    

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

