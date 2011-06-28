using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Web.UI;
using System.Web.UI.WebControls.WebParts;

using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.WebControls;
using Microsoft.SharePoint.WebPartPages;
using System.Text.RegularExpressions;

namespace SgartIt.Sp
{
  [Guid("8d010a5f-a1c2-4580-b092-f5d48b3ce351")]
  public class HtmlParametric : System.Web.UI.WebControls.WebParts.WebPart
  {
    private string htmlBody;
    private string param1;
    private string param2;
    private string param3;
    private string param4;
    private string paramUser1;
    private string paramUser2;
    private string paramUser3;

    protected LiteralControl lc;

    SPWeb web;
    DateTime dtNow;
    SPUser user;

    public HtmlParametric()
    {
      this.ExportMode = WebPartExportMode.All;
    }
    #region Public properties
    //edited by owners
    [WebBrowsable(false)]
    [Personalizable(PersonalizationScope.Shared)]
    [WebPartStorage(Storage.Shared)]
    public string HtmlBody
    {
      get { return this.htmlBody; }
      set { this.htmlBody = value; }
    }

    [WebBrowsable(false)]
    [Personalizable(PersonalizationScope.Shared)]
    [WebPartStorage(Storage.Shared)]
    public string Param1
    {
      get { return this.param1; }
      set { this.param1 = value; }
    }

    [WebBrowsable(false)]
    [Personalizable(PersonalizationScope.Shared)]
    [WebPartStorage(Storage.Shared)]
    public string Param2
    {
      get { return this.param2; }
      set { this.param2 = value; }
    }

    [WebBrowsable(false)]
    [Personalizable(PersonalizationScope.Shared)]
    [WebPartStorage(Storage.Shared)]
    public string Param3
    {
      get { return this.param3; }
      set { this.param3 = value; }
    }

    [WebBrowsable(false)]
    [Personalizable(PersonalizationScope.Shared)]
    [WebPartStorage(Storage.Shared)]
    public string Param4
    {
      get { return this.param4; }
      set { this.param4 = value; }
    }

    //edited my members
    [WebBrowsable(false)]
    [Personalizable(PersonalizationScope.User)]
    [WebPartStorage( Storage.Personal)]
    public string ParamUser1
    {
      get { return this.paramUser1; }
      set { this.paramUser1 = value; }
    }

    [WebBrowsable(false)]
    [Personalizable(PersonalizationScope.User)]
    [WebPartStorage(Storage.Personal)]
    public string ParamUser2
    {
      get { return this.paramUser2; }
      set { this.paramUser2 = value; }
    }

    [WebBrowsable(false)]
    [Personalizable(PersonalizationScope.User)]
    [WebPartStorage(Storage.Personal)]
    public string ParamUser3
    {
      get { return this.paramUser3; }
      set { this.paramUser3 = value; }
    }
    #endregion

    public override EditorPartCollection CreateEditorParts()
    {
      List<EditorPart> parts = new List<EditorPart>();
      if (WebPartManager.Personalization.Scope == PersonalizationScope.Shared)
      {
        HtmlParametricEditor editor = new HtmlParametricEditor();
        editor.ID = string.Format("{0}_{1}", this.ID, Helper.KEY);
        parts.Add(editor);
      }
      HtmlParametricEditorUser editorUser = new HtmlParametricEditorUser();
      editorUser.ID = string.Format("{0}_{1}User", this.ID, Helper.KEY);
      parts.Add(editorUser);

      return new EditorPartCollection(base.CreateEditorParts(), parts); ;
    }

    protected override void CreateChildControls()
    {
      string s = htmlBody;
      if (string.IsNullOrEmpty(s))
      {
        s = string.Format(@"
Html Parametric by 
<a href=""http://www.sgart.it?prg=HtmlParametric"" target=""_blank"">Sgart.it</a>
<br />
<a id=""HtmlParametricWebPart_OpenToolPane_{0}"" href=""#"" onclick=""javascript:MSOTlPn_ShowToolPane2('Edit','{1}');"">Open the tool pane</a>
to configure this Web Part.", this.ClientID, this.ID);
      }
      else
      {
        try
        {
          web = SPContext.Current.Web;
          dtNow = DateTime.Now;
          user = web.CurrentUser;

          s = ParseTag(s);
        }
        catch (Exception ex)
        {
          s = string.Format("### Error: {0}", ex);
        }
      }

      lc = new LiteralControl(s);
      this.Controls.Add(lc);

      base.CreateChildControls();
    }

    private string ParseTag(string s)
    {
      Regex re = new Regex(@"\{(?<tag>s[a-z]*):(?<value>[a-z0-9]+)(:(?<option>.*))?\}"
        , RegexOptions.Compiled | RegexOptions.ExplicitCapture
        | RegexOptions.IgnoreCase | RegexOptions.Multiline);
      string result = re.Replace(s, delegate(Match m)
      {
        string tag = m.Groups["tag"].Value.ToLower();
        string val = m.Groups["value"].Value.ToLower();
        string opt = m.Groups["option"].Value;
        if (tag == "s")
        {
          //site or general
          return GetWebProperty(val, opt);
        }
        else if (tag == "sl")
        {
          //list
          return GetListProperty(val, opt);
        }
        else if (tag == "sp")
        {
          //list
          return GetParameters(val, opt);
        }
        else if (tag == "sq")
        {
          // query string
          return this.Context.Request.QueryString[val];
        }
        else
        {
          return string.Format("### unknow tag: {0} ###", tag);
        }
      });
      return result;
    }

    private string GetParameters(string val, string opt)
    {
      switch (val)
      {
        case "param1":
        case "parameter1":
        case "p1":
          return Param1;
        case "param2":
        case "parameter2":
        case "p2":
          return Param2;
        case "param3":
        case "parameter3":
        case "p3":
          return Param3;
        case "param4":
        case "parameter4":
        case "p4":
          return Param4;
        case "user1":
        case "u1":
          return ParamUser1;
        case "user2":
        case "u2":
          return ParamUser2;
        case "user3":
        case "u3":
          return ParamUser3;
        default:
          return string.Format("### unknow parameter: {0} ###", val);
      }
    }

    private string GetListProperty(string val, string opt)
    {
      try
      {
        using (SPSite site = new SPSite(web.Url))
        {
          site.CatchAccessDeniedException = false;
          using (SPWeb webList = site.OpenWeb(web.ServerRelativeUrl))
          {
            SPList list = null;
            if (opt.Contains("/"))
            {
              string url = web.ServerRelativeUrl + opt;
              list = webList.GetList(opt);
            }
            else
            {
              list = webList.Lists[opt];
            }

            switch (val)
            {
              case "title":
                return list.Title;
              case "url":
                return list.RootFolder.ServerRelativeUrl;
              case "guid":
              case "id":
                return list.ID.ToString("B").ToUpper();
              case "viewschema":
              case "viewschemaurl":
                string u = string.Format(
                  "{0}/_vti_bin/owssvr.dll?Cmd=ExportList&List={1}"
                  , web.ServerRelativeUrl == "/" ? "" : web.ServerRelativeUrl
                  , list.ID.ToString("B").ToUpper());
                if (val == "viewschemaurl")
                {
                  return u;
                }
                else
                {
                  return string.Format("<a href=\"{0}\" target=\"_blank\">{1}</a>"
                    , u, list.Title);
                }
              default:
                return string.Format("### unknow value: {0} ###", val);
            }
          }
        }
      }
      catch
      {
        return string.Format("### List {0} not found ###", opt);
      }
    
    }

    private string GetWebProperty(string val, string opt)
    {
      string[] tmp;

      switch (val)
      {
        case "id":
        case "guid":
          return web.ID.ToString("B").ToUpper();
        case "name":
          return web.Name;
        case "title":
          return web.Title;
        case "description":
          return web.Description;
        case "authenticationmode":
          return web.AuthenticationMode.ToString();
        case "localename":
          return web.Locale.Name;
        case "localelcid":
          return web.Locale.LCID.ToString();
        case "lcid":
          return web.Language.ToString();
        case "zone":
          return web.Site.Zone.ToString();

        case "folderurl":
          return System.IO.Path.GetDirectoryName(this.Context.Request.Url.AbsolutePath).Replace("\\", "/");
        case "folderurlfull":
          return SPUtility.GetUrlDirectory(this.Context.Request.Url.ToString());
        case "weburl":
          return web.ServerRelativeUrl;
        case "weburlfull":
          return web.Url;
        case "siteurl":
          return web.Site.ServerRelativeUrl;
        case "siteurlfull":
          return web.Site.Url;
        case "urlscheme":
          return this.Context.Request.Url.Scheme;
        case "urlhost":
          return this.Context.Request.Url.Host;
        case "layoutsurl":
          return (web.ServerRelativeUrl + "/_layouts").Replace("//", "/");
        case "layoutsurlfull":
          return web.Url + "/_layouts";

        case "clientname":
        case "clientnamefull":
          try
          {
            string pcname = System.Net.Dns.GetHostEntry(Context.Request.ServerVariables["remote_addr"]).HostName;
            if (val == "clientname")
            {
              tmp = pcname.Split(new Char[] { '.' });
              return tmp[0];
            }
            else
            {
              return pcname;
            }
          }
          catch { }
          return "?";

        case "clientip":
          try
          {
            return Context.Request.ServerVariables["remote_addr"];
          }
          catch {
            return "?";
          }

        case "servername":
          return Environment.MachineName;

        case "serverip":
          return Context.Request.ServerVariables["local_addr"];

        case "osversion":
          return Environment.OSVersion.VersionString;

        case "date":
          return dtNow.Date.ToShortDateString();
        case "time":
          return dtNow.ToShortTimeString();
        case "datetime":
          return dtNow.ToString();

        case "username":
          return user.Name;
        case "useremail":
        case "usermail":
          return user.Email;
        case "userloginfull":
          return user.LoginName;
        case "userlogin":
        case "userdomain":
          tmp = user.LoginName.Split(new char[] { '\\', '|' });
          if (tmp.Length >= 2)
          {
            if (val == "userlogin")
            {
              return tmp[1];
            }
            else
            {
              return tmp[0];
            }
          }
          else
          {
            return "??";
          }
        case "issiteadmin":
          return web.UserIsSiteAdmin.ToString();
        case "ismemberofgroup":
          try
          {
            SPGroup grp = web.Groups[opt];
            return web.IsCurrentUserMemberOfGroup(grp.ID).ToString();
          }
          catch
          {
            return false.ToString();
          }

        default:
          return string.Format("### unknow value: {0} ###", val);
      }
    }

    protected override void Render(HtmlTextWriter writer)
    {
      writer.Write("<!-- begin: SgartIt.Sp.HtmlParametric -->");
      lc.RenderControl(writer);
      writer.Write("<!-- end: SgartIt.Sp.HtmlParametric -->");
    }

  }
}
