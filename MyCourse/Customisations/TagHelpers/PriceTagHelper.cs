using Microsoft.AspNetCore.Razor.TagHelpers;
using MyCourse.Models.ValueObjects;

namespace MyCourse.Customisations.TagHelpers
{
    public class PriceTagHelper : TagHelper
    {
        public Money FullPrice { get; set; }
        public Money CurrenPrice { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Content.AppendHtml($"{CurrenPrice}");

            if(!CurrenPrice.Equals(FullPrice))
            {
                output.Content.AppendHtml($"<br /> <s class=\"text-secondary\">{FullPrice}</s>");
            }
        }
    }
}
