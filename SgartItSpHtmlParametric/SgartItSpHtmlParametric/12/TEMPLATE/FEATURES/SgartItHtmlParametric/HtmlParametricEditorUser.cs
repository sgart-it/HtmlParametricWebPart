using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;

namespace SgartIt.Sp
{
  public class HtmlParametricEditorUser 
    : System.Web.UI.WebControls.WebParts.EditorPart
  {
    protected TextBox txtParamUser1;
    protected TextBox txtParamUser2;
    protected TextBox txtParamUser3;

    public HtmlParametricEditorUser()
    {
      this.Title = "Sgart.it - Html Parametric - User ";
    }

    protected override void CreateChildControls()
    {
      txtParamUser1 = new TextBox();
      txtParamUser1.ID = "txtParamUser1";
      txtParamUser1.CssClass = "UserInput";
      txtParamUser1.Style.Add("width", "176px");
      this.Controls.Add(txtParamUser1);

      txtParamUser2 = new TextBox();
      txtParamUser2.ID = "txtParamUser2";
      txtParamUser2.CssClass = "UserInput";
      txtParamUser2.Style.Add("width", "176px");
      this.Controls.Add(txtParamUser2);

      txtParamUser3 = new TextBox();
      txtParamUser3.ID = "txtParamUser3";
      txtParamUser3.CssClass = "UserInput";
      txtParamUser3.Style.Add("width", "176px");
      this.Controls.Add(txtParamUser3);
      
      base.CreateChildControls();
    }

    public override bool ApplyChanges()
    {
      this.EnsureChildControls();
      HtmlParametric wp = (HtmlParametric)this.WebPartToEdit;
      wp.ParamUser1 = txtParamUser1.Text;
      wp.ParamUser2 = txtParamUser2.Text;
      wp.ParamUser3 = txtParamUser3.Text;
      return true;
    }

    public override void SyncChanges()
    {
      this.EnsureChildControls();
      HtmlParametric wp = (HtmlParametric)this.WebPartToEdit;
      txtParamUser1.Text = wp.ParamUser1;
      txtParamUser2.Text = wp.ParamUser2;
      txtParamUser3.Text = wp.ParamUser3;
    }

    protected override void Render(System.Web.UI.HtmlTextWriter writer)
    {
      writer.Write("<div class=\"ms-ToolPartSpacing\"></div>");
      writer.Write("<div class=\"ms-TPBody\">");

      writer.Write("<div class=\"UserSectionHead\">User parameter 1</div>");
      writer.Write("<div class=\"UserSectionBody\">");
      txtParamUser1.RenderControl(writer);
      writer.Write("</div>");

      writer.Write("<div class=\"UserSectionHead\">User parameter 2</div>");
      writer.Write("<div class=\"UserSectionBody\">");
      txtParamUser2.RenderControl(writer);
      writer.Write("</div>");

      writer.Write("<div class=\"UserSectionHead\">User parameter 3</div>");
      writer.Write("<div class=\"UserSectionBody\">");
      txtParamUser3.RenderControl(writer);
      writer.Write("</div>");

      writer.Write("</div>");
    }
  }
}
