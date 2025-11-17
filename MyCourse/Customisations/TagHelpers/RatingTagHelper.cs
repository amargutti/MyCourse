using Microsoft.AspNetCore.Razor.TagHelpers;

namespace MyCourse.Customisations.TagHelpers
{
    public class RatingTagHelper : TagHelper
    {
        public double Value { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            for (int i = 1; i <= 5; i++)
            {
                if (Value >= i)
                {
                    output.Content.AppendHtml("<i class=\"bi bi-star-fill text-warning\"></i>");
                }
                else if (Value > i - 1)
                {
                    output.Content.AppendHtml("<i class=\"bi bi-star-half text-warning\"></i>");
                }
                else
                {
                    output.Content.AppendHtml("<i class=\"bi bi-star\"></i>");
                }
            }
        }
    }
}
