using MvcPL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using MvcPL.Controllers;

namespace MvcPL.Infrastructure.Helpers
{
    public static class RatingHelpers
    {
        public static MvcHtmlString Ratings(this HtmlHelper helper, VotingViewModel vote)
        {
            TagBuilder spanTag = new TagBuilder("span");
            spanTag.AddCssClass("rating");
            spanTag.MergeAttribute("id", string.Format("span{0}", vote.PhotoId));
            spanTag.MergeAttribute("rating", string.Format("{0}", vote.Rating));
            spanTag.MergeAttribute("photo", string.Format("{0}", vote.PhotoId));
            spanTag.MergeAttribute("title", "Click to vote!");

            for (int i = 1; i <= 5; i++)
            {
                TagBuilder imgTag = new TagBuilder("img");
                imgTag.AddCssClass("star");
                imgTag.MergeAttribute("alt", "star");
                imgTag.MergeAttribute("width", "20");
                imgTag.MergeAttribute("height", "20");
                imgTag.MergeAttribute("value", string.Format("{0}", i));
                imgTag.MergeAttribute("onclick", "clickStar(this);");
                if (i <= vote.Rating)
                    imgTag.MergeAttribute("src", "/Content/images/star.png");
                else
                    imgTag.MergeAttribute("src", "/Content/images/starOff.png");

                spanTag.InnerHtml += imgTag.ToString();
            }

            return new MvcHtmlString(spanTag.ToString());            
        }
    }
}