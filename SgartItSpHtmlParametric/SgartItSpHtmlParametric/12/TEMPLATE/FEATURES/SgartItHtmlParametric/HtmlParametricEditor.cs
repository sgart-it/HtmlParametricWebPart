using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;

namespace SgartIt.Sp
{
  public class HtmlParametricEditor 
    : System.Web.UI.WebControls.WebParts.EditorPart
  {
    protected TextBox txtHtmlBody;
    protected TextBox txtParam1;
    protected TextBox txtParam2;
    protected TextBox txtParam3;
    protected TextBox txtParam4;

    public HtmlParametricEditor()
    {
      this.Title = "Sgart.it - Html Parametric";
    }

    protected override void OnLoad(System.EventArgs e)
    {
      this.EnsureChildControls();
      string url = SPContext.Current.Web.ServerRelativeUrl;
      //script
      StringBuilder sb = new StringBuilder(1000);
      sb.AppendFormat(@"
function sgartItWP2007HtmlParametricEditor(ctrlId) {{
  var p = window.open('{0}/_layouts/SgartItHtmlParametric/Editor.aspx?ctrlId=' + ctrlId,'SgartItHtmlParametric','resizable=1,height=550,width=700');
  p.focus();
}}", url == "/" ? "" : url);
      this.Page.ClientScript.RegisterClientScriptBlock(typeof(HtmlParametricEditor)
        , "SgartItHtmlParametricEditor", sb.ToString(), true);

      base.OnLoad(e);
    }

    protected override void CreateChildControls()
    {
      txtHtmlBody = new TextBox();
      txtHtmlBody.ID = "txtHtmlBody";
      txtHtmlBody.TextMode = TextBoxMode.MultiLine;
      txtHtmlBody.Rows = 5;
      txtHtmlBody.Style.Add("width", "176px");
      this.Controls.Add(txtHtmlBody);

      txtParam1 = new TextBox();
      txtParam1.ID = "txtParam1";
      txtParam1.CssClass = "UserInput";
      txtParam1.Style.Add("width", "176px");
      this.Controls.Add(txtParam1);

      txtParam2 = new TextBox();
      txtParam2.ID = "txtParam2";
      txtParam2.CssClass = "UserInput";
      txtParam2.Style.Add("width", "176px");
      this.Controls.Add(txtParam2);

      txtParam3 = new TextBox();
      txtParam3.ID = "txtParam3";
      txtParam3.CssClass = "UserInput";
      txtParam3.Style.Add("width", "176px");
      this.Controls.Add(txtParam3);

      txtParam4 = new TextBox();
      txtParam4.ID = "txtParam4";
      txtParam4.CssClass = "UserInput";
      txtParam4.Style.Add("width", "176px");
      this.Controls.Add(txtParam4);

      base.CreateChildControls();
    }

    public override bool ApplyChanges()
    {
      this.EnsureChildControls();
      HtmlParametric wp = (HtmlParametric)this.WebPartToEdit;
      wp.HtmlBody = txtHtmlBody.Text;
      wp.Param1 = txtParam1.Text;
      wp.Param2 = txtParam2.Text;
      wp.Param3 = txtParam3.Text;
      wp.Param4 = txtParam4.Text;
      return true;
    }

    public override void SyncChanges()
    {
      this.EnsureChildControls();
      HtmlParametric wp = (HtmlParametric)this.WebPartToEdit;
      txtHtmlBody.Text = wp.HtmlBody;
      txtParam1.Text = wp.Param1;
      txtParam2.Text = wp.Param2;
      txtParam3.Text = wp.Param3;
      txtParam4.Text = wp.Param4;
    }

    protected override void Render(System.Web.UI.HtmlTextWriter writer)
    {
      writer.Write("<div class=\"ms-ToolPartSpacing\"></div>");
      writer.Write("<div class=\"ms-TPBody\">");

      writer.Write("<div class=\"UserSectionHead\">Html&#160;");
      writer.Write(" ( <a href=\"javascript:sgartItWP2007HtmlParametricEditor('");
      writer.Write(txtHtmlBody.ClientID);
      writer.Write("')\">Open in popup</a> )");
      writer.Write("</div>");
      writer.Write("<div class=\"UserSectionBody\">");
      txtHtmlBody.RenderControl(writer);
      writer.Write("</div>");

      writer.Write("<div style=\"width: 100%\" class=\"userdottedline\"></div>");

      writer.Write("<div class=\"UserSectionHead\">Parameter 1</div>");
      writer.Write("<div class=\"UserSectionBody\">");
      txtParam1.RenderControl(writer);
      writer.Write("</div>");

      writer.Write("<div class=\"UserSectionHead\">Parameter 2</div>");
      writer.Write("<div class=\"UserSectionBody\">");
      txtParam2.RenderControl(writer);
      writer.Write("</div>");

      writer.Write("<div class=\"UserSectionHead\">Parameter 3</div>");
      writer.Write("<div class=\"UserSectionBody\">");
      txtParam3.RenderControl(writer);
      writer.Write("</div>");

      writer.Write("<div class=\"UserSectionHead\">Parameter 4</div>");
      writer.Write("<div class=\"UserSectionBody\">");
      txtParam4.RenderControl(writer);
      writer.Write("</div>");

      //writer.Write("<div style=\"width: 100%\" class=\"userdottedline\"></div>");
      writer.Write("</div>");
    }
  }
}
