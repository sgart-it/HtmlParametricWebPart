<%@ Page Language="C#" MasterPageFile="~/_layouts/pickerdialog.master"  %>

<asp:Content ID="Content1" contentplaceholderid="PlaceHolderDialogHeaderPageTitle" runat="server">
		Editor
</asp:Content>
<asp:Content ID="Content5" contentplaceholderid="PlaceHolderSiteName" runat="server">
		Sgart.it Html Parametric
</asp:Content>
<asp:Content ID="Content3" contentplaceholderid="PlaceHolderDialogTitleInTitleArea" runat="server">
		Editor
</asp:Content>
<asp:Content ID="Content6" contentplaceholderid="PlaceHolderHelpLink" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderId="PlaceHolderAdditionalPageHead" runat="server">
<script language="javascript" type="text/javascript">

    var targetElement=null;
    var txtEditor = null;
    
    _spBodyOnLoadFunctionNames.push("init");

    function init() {

        var btnId="<%= Master.FindControl("PlaceHolderDialogButtonSection").FindControl("btnOk").ClientID %>";
        //$("input[id*='btnOk']").click(save);
        var btn = document.getElementById(btnId);
        btn.onclick = function () {
            targetElement.value = txtEditor.value;
            window.close();
        };

        targetElement = opener.document.getElementById(getQueryString('ctrlId'));
        txtEditor = document.getElementById("txtEditor");
        txtEditor.value = targetElement.value;
    }

    function getQueryString(queryString) {
        hu = window.location.search.substring(1);
        gy = hu.split("&");
        for (i = 0; i < gy.length; i++) {
            ft = gy[i].split("=");
            if (ft[0] == queryString) {
                return ft[1];
            }
        }
    }

    function allowTab() {
        if (event != null) {
            if (event.srcElement) {
                if (event.srcElement.value) {
                    if (event.keyCode == 9) {
                        // tab character               
                        if (document.selection != null) {
                            document.selection.createRange().text = '\t';
                            event.returnValue = false;
                        }
                        else {
                            event.srcElement.value += '\t';
                            return false;
                        }
                    }
                }
            }

        }
    }

</script>
</asp:Content>
<asp:Content ID="Content4" contentplaceholderid="PlaceHolderDialogBodySection" runat="server">
    <table style="width:100%;height:100%;">
        <tr>
            <td>Insert tag: <select id="sgartTag" onchange="insertTag(this)">
                <option ></option>
<option value='{s:id}'>Site ID: {s:id}</option>
<option value='{s:name}'>Site name: {s:name}</option>
<option value='{s:title}'>Site title: {s:title}</option>
<option value='{s:description}'>Site description: {s:description}</option>
<option value='{s:authenticationmode}'>Site authentication mode: {s:authenticationmode}</option>
<option value='{s:lcid}'>Site Lcid: {s:lcid}</option>
<option value='{s:localename}'>Site locale name {s:localename}</option>
<option value='{s:localelcid}'>Site locale lcid {s:localelcid}</option>
<option value='{s:folderurl}'>Folder url: {s:folderurl}</option>
<option value='{s:folderurlfull}'>Folder url full: {s:folderurlfull}</option>
<option value='{s:weburl}'>Site url: {s:weburl}</option>
<option value='{s:weburlfull}'>Site url full: {s:weburlfull}</option>
<option value='{s:layoutsurl}'>Layouts url: {s:layoutsurl}</option>
<option value='{s:layoutsurlfull}'>Layouts url full: {s:layoutsurlfull}</option>
<option value='{s:siteurl}'>Site collection url: {s:siteurl}</option>
<option value='{s:siteurlfull}'>Site collection url full: {s:siteurlfull}</option>
<option value='{s:urlscheme}'>Url scheme: {s:urlscheme}</option>
<option value='{s:zone}'>Zone: {s:zone}</option>
<option value='{s:urlhost}'>Host: {s:urlhost}</option>
<option value='{s:clientname}'>Client name: {s:clientname}</option>
<option value='{s:clientip}'>Client IP: {s:clientip}</option>
<option value='{s:servername}'>Server name: {s:servername}</option>
<option value='{s:serverip}'>Server IP: {s:serverip}</option>
<option value='{s:osversion}'>OS versione: {s:osversion}</option>
<option value='{s:date}'>Date: {s:date}</option>
<option value='{s:time}'>Time: {s:time}</option>
<option value='{s:datetime}'>Date and time: {s:datetime}</option>
<option value='{s:username}'>User name: {s:username}</option>
<option value='{s:userloginfull}'>User login full: {s:userloginfull}</option>
<option value='{s:userlogin}'>User login: {s:userlogin}</option>
<option value='{s:userdomain}'>User domain: {s:userdomain}</option>
<option value='{s:useremail}'>User email: {s:useremail}</option>
<option value='{s:issiteadmin}'>User IsSiteAdmin {s:issiteadmin}</option>
<option value='{s:ismemberofgroup:groupname}'>User IsMemberOfGroup: {s:ismemberofgroup:groupname}</option>
<option value='{sq:test}'>Query string parameter: {sq:parameterName}</option>
<option value='{sl:id:documents}'>List ID: {sl:id:listNameOrUrl}</option>
<option value='{sl:title:documents}'>List title: {sl:title:listNameOrUrl}</option>
<option value='{sl:url:documents}'>List url: {sl:url:listNameOrUrl}</option>
<option value='{sl:viewschema:documents}'>List schema: {sl:viewschema:listNameOrUrl}</option>
<option value='{sl:viewschemaurl:documents}'>List schema link: {sl:viewschemaurl:listNameOrUrl}</option>
<option value='{sp:param1}'>Parameter 1: {sp:param1}</option>
<option value='{sp:param2}'>Parameter 2: {sp:param2}</option>
<option value='{sp:param3}'>Parameter 3: {sp:param3}</option>
<option value='{sp:param4}'>Parameter 4: {sp:param4}</option>
<option value='{sp:user1}'>User parameter 1: {sp:user1}</option>
<option value='{sp:user2}'>User parameter 2: {sp:user2}</option>
<option value='{sp:user3}'>User parameter 3: {sp:user3}</option>
                    </select></td>
        </tr>
        <tr>
            <td style="height:99%;"><textarea id="txtEditor" name="txtEditor" cols="30" rows="26" style="width:100%;height:100%;" onkeydown="allowTab()"></textarea></td>
        </tr>
    </table>

<script language="javascript" type="text/javascript">
function insertTag(obj){
    var i = obj.selectedIndex;
    var val = obj.options[i].value;
    
    var txt = document.getElementById("txtEditor");
    if (document.selection) {
        txt.focus();
        var sel = document.selection.createRange();
        sel.text = val;
    } else if (txt.selectionStart || txt.selectionStart == '0') {
        var startPos = txt.selectionStart;
        var endPos = txt.selectionEnd;
        txt.value = txt.value.substring(0, startPos)
            + val + txt.value.substring(endPos, txt.value.length);
    } else {
        txt.value += val;
    }
    obj.selectedIndex = -1;
}        
</script>
    
</asp:Content>
