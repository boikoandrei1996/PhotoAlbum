using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace WebApplicationPL.Infrastructure
{
    using Models;
    public static class HtmlHelpers
    {

        #region UploadImageInput Html view
        /*     
        <div class="input-group image-preview">
            <span class="input-group-btn">
                <label class="btn btn-default image-preview-input">
                    <span class="glyphicon glyphicon-folder-open"></span>
                    <span class="image-preview-input-title">Browse</span>
                    <input type="file" accept=".png, .jpg, .jpeg, .gif" name="avatar" id="avatar" />
                </label>
                <button type="button" class="btn btn-default image-preview-clear" style="display:none;">
                    <span class="glyphicon glyphicon-remove"></span> Clear
                </button>
            </span>                        
            <input type="text" class="form-control image-preview-filename" disabled="disabled">
        </div>
        */
        #endregion
        public static MvcHtmlString UploadImageInput(this HtmlHelper html, string nameInput, string idInput)
        {
            TagBuilder tagSpan1 = new TagBuilder("span");
            tagSpan1.AddCssClass("glyphicon-folder-open");
            tagSpan1.AddCssClass("glyphicon");

            TagBuilder tagSpan2 = new TagBuilder("span");
            tagSpan2.AddCssClass("image-preview-input-title");
            tagSpan2.SetInnerText("Browse");

            TagBuilder tagInput1 = new TagBuilder("input");
            tagInput1.MergeAttribute("type", "file");
            tagInput1.MergeAttribute("accept", ".png, .jpg, .jpeg, .gif");
            tagInput1.MergeAttribute("name", nameInput);
            tagInput1.GenerateId(idInput);            

            TagBuilder tagSpan3 = new TagBuilder("span");
            tagSpan3.AddCssClass("glyphicon-remove");
            tagSpan3.AddCssClass("glyphicon");
            
            TagBuilder tagSpan4 = new TagBuilder("span");
            tagSpan4.SetInnerText("Clear");

            TagBuilder tagInput2 = new TagBuilder("input");
            tagInput2.MergeAttribute("type", "text");
            tagInput2.MergeAttribute("disabled", "disabled");
            tagInput2.AddCssClass("image-preview-filename");
            tagInput2.AddCssClass("form-control");

            TagBuilder tagLabel = new TagBuilder("label");
            tagLabel.AddCssClass("image-preview-input");
            tagLabel.AddCssClass("btn-default");
            tagLabel.AddCssClass("btn");
            tagLabel.InnerHtml += tagSpan1.ToString();
            tagLabel.InnerHtml += tagSpan2.ToString();
            tagLabel.InnerHtml += tagInput1.ToString();

            TagBuilder tagButton = new TagBuilder("button");
            tagButton.MergeAttribute("type", "button");
            tagButton.MergeAttribute("style", "display:none;");
            tagButton.AddCssClass("image-preview-clear");
            tagButton.AddCssClass("btn-default");
            tagButton.AddCssClass("btn");
            tagButton.InnerHtml += tagSpan3.ToString();
            tagButton.InnerHtml += tagSpan4.ToString();

            TagBuilder tagSpan5 = new TagBuilder("span");
            tagSpan5.AddCssClass("input-group-btn");
            tagSpan5.InnerHtml += tagLabel.ToString();
            tagSpan5.InnerHtml += tagButton.ToString();

            TagBuilder tagDiv = new TagBuilder("div");
            tagDiv.AddCssClass("image-preview");
            tagDiv.AddCssClass("input-group");
            tagDiv.InnerHtml += tagSpan5.ToString();
            tagDiv.InnerHtml += tagInput2.ToString();

            return new MvcHtmlString(tagDiv.ToString());
        }


        #region PageLinks Html view
        /*
        <ul class="pagination">
            <li class="active"><a href = "" > 1 </a></li>
            <li><a href = "" > 1 </ a ></ li > 
             ...
        </ul>
        */
        #endregion
        public static MvcHtmlString PageLinks(this HtmlHelper html, PageInfoViewModel pageInfo, Func<int, string> pageUrl)
        {
            TagBuilder tagUl = new TagBuilder("ul");
            tagUl.AddCssClass("pagination");

            StringBuilder listItems = new StringBuilder();
            for (int i = 1; i <= pageInfo.TotalPages; i++)
            {
                TagBuilder tagLink = new TagBuilder("a");
                tagLink.MergeAttribute("href", pageUrl(i));
                tagLink.InnerHtml = i.ToString();

                TagBuilder tagLi = new TagBuilder("li");

                if (i == pageInfo.CurrentPage)
                    tagLi.AddCssClass("active");

                tagLi.InnerHtml += tagLink.ToString();

                listItems.Append(tagLi.ToString());
            }

            tagUl.InnerHtml += listItems.ToString();

            return new MvcHtmlString(tagUl.ToString());
        }


        /*public static MvcHtmlString PageLinks(this AjaxHelper ajax, PageInfoViewModel pageInfo, 
                                                Func<int, string> pageUrl, string updateTargetId, string loadingElementId)
        {
            StringBuilder listItems = new StringBuilder();

            TagBuilder tagUl = new TagBuilder("ul");
            tagUl.AddCssClass("pagination");
            for (int i = 1; i <= pageInfo.TotalPages; i++)
            {
                AjaxOptions ajaxOptions = new AjaxOptions
                {
                    UpdateTargetId = updateTargetId,
                    LoadingElementDuration = 10,
                    LoadingElementId = loadingElementId,
                    OnSuccess = "ChangeActivePageLink",
                    Url = pageUrl(i)
                };

                TagBuilder tagLink = new TagBuilder("a");
                tagLink.MergeAttributes(ajaxOptions.ToUnobtrusiveHtmlAttributes());
                tagLink.InnerHtml = i.ToString();

                TagBuilder tagLi = new TagBuilder("li");
                if (i == pageInfo.CurrentPage)
                    tagLi.AddCssClass("active");
                tagLi.InnerHtml += tagLink.ToString();

                listItems.Append(tagLi.ToString());
            }

            tagUl.InnerHtml += listItems.ToString();

            return new MvcHtmlString(tagUl.ToString());
        }*/
    }
}