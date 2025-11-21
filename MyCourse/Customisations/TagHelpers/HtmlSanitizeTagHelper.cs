using AngleSharp.Html.Dom;
using Ganss.Xss;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace MyCourse.Customisations.TagHelpers
{
    [HtmlTargetElement(Attributes = "html-sanitize")]
    public class HtmlSanitizeTagHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            //Ottengo il contenuto del tag
            TagHelperContent tagHelperContent = await output.GetChildContentAsync(NullHtmlEncoder.Default);
            string content = tagHelperContent.GetContent(NullHtmlEncoder.Default);

            //Eseguo la sanitizzazione tramite paccehtto nuGet HtmlSanitizer
            var sanitizer = CreateSanitizer();
            content = sanitizer.Sanitize(content);

            //Reimpostiamo il contenuto del tag
            output.Content.SetHtmlContent(content);
        }

        private static HtmlSanitizer CreateSanitizer()
        {
            var sanitizer = new HtmlSanitizer();

            //Tag Consentiti
            sanitizer.AllowedTags.Clear();
            sanitizer.AllowedTags.Add("b");
            sanitizer.AllowedTags.Add("i");
            sanitizer.AllowedTags.Add("p");
            sanitizer.AllowedTags.Add("br");
            sanitizer.AllowedTags.Add("ul");
            sanitizer.AllowedTags.Add("ol");
            sanitizer.AllowedTags.Add("li");
            sanitizer.AllowedTags.Add("iframe");

            //Attributi consentiti
            sanitizer.AllowedAttributes.Clear();
            sanitizer.AllowedAttributes.Add("src");

            //Stili Consentiti
            sanitizer.AllowedCssProperties.Clear();

            sanitizer.FilterUrl += FilterUrl;
            sanitizer.PostProcessNode += ProcessIFrame;

            return sanitizer;

        }

        private static void ProcessIFrame(object? sender, PostProcessNodeEventArgs e)
        {
            var iframe = e.Node as IHtmlInlineFrameElement;
            if (iframe == null) {
                return;
            }

            var container = e.Document.CreateElement("span");
            container.ClassName = "video-container";
            container.AppendChild(iframe.Clone(true));
            e.ReplacementNodes.Add(container);
        }

        private static void FilterUrl(object? sender, FilterUrlEventArgs e)
        {
            if(!e.OriginalUrl.StartsWith("//www.youtube.com/") && !e.OriginalUrl.StartsWith("https://www.youtube.com/"))
            {
                e.SanitizedUrl = null;
            }
        }
    }
}
