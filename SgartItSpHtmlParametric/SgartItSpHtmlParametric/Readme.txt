Copile project
execute makesolution.bat
execute setup.exe


OS versione: {s:osversion}
serverName: {s:servername}
ServerIP: {s:serverip}
ClientName: {s:clientname}
ClientNameFull: {s:clientnamefull}
ClientIP: {s:clientip}

Title: {s:title}
Description: {s:description}
Lcid: {s:lcid}
WebUrl: {s:weburl}
WebUrlFull: {s:weburlfull}
LayoutsUrl: {s:layoutsurl}
LayoutsUrlFull: {s:layoutsurlfull}
SiteUrl: {s:siteurl}
SiteUrlFull: {s:siteurlfull}
Scheme: {s:urlscheme}
Host: {s:urlhost}
Date: {s:date}
Time: {s:time}
DateTime: {s:datetime}
UserName: {s:username}
UserLoginFull: {s:userloginfull}
UserLogin: {s:userlogin}
UserDomain: {s:userdomain}
QS: {sq:test}
List title: {sl:title:documents}
List url: {sl:url:documents}
List schema: {sl:viewschema:documents}
List schema: {sl:viewschemaurl:documents}
List ID: {sl:id:documents}
Parameters: {sp:param1} - {sp:param2} - {sp:param3} - {sp:param4}
User Parameters: {sp:user1} - {sp:user2} - {sp:user3}


<object id="sgartMovie" classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000" 
  codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,40,0" 
  width="{sp:param1}" height="{sp:param2}" > 
  <param name="movie" value="{sq:movie}" /> 
  <param name="quality" value="high" /> 
  <param name="bgcolor" value="#ffffff" /> 
  <embed name="sgartMovie" width="{sp:param1}" height="{sp:param2}" 
    src="{sq:movie}" quality="high" bgcolor="#ffffff"
	type="application/x-shockwave-flash" 
    pluginspage="http://www.macromedia.com/go/getflashplayer"> 
  </embed> 
</object> 



<h3 id="secure">?</h3>
<script language="javascript">
var objSecure = document.getElementById("secure");
if ("{s:urlscheme}" == "https") {
  objSecure.innerText = "Secure protocol https";
} else{
  objSecure.innerText = "Normal protocol http";
}
</script>

 
