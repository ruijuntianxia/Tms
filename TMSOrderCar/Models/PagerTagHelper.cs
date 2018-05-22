using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMSOrderCar.Models
{
    #region 分页扩展PageExtend

    /// <summary>
    /// 分页Option属性
    /// </summary>
    public class MoPagerOption
    {

        /// <summary>
        /// 当前页 必传
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// 总条数 必传
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// 分页记录数（每页条数 默认每页15条）
        /// </summary>

        public int PageSize { get; set; }


        /// <summary>
        /// 路由地址 （格式：/controller/action）默认自动获取
        /// </summary>
        public string RouteUrl { get; set; }

        /// <summary>
        /// 样式  默认 bootstrap 样式1
        /// </summary>
        public int StyleNum { get; set; }

        //不是必要的  根据页面是否有查询条件适当添加修改
        /// <summary>
        /// Select 查询条件 名字
        /// </summary>
        public string SelectName { get; set; }

        /// <summary>
        /// Select 查询条件值
        /// </summary>
        public string SelectValue { get; set; }

        /// <summary>
        /// Text 查询条件 名字
        /// </summary>
        public string TextName { get; set; }
        /// <summary>
        /// Text 查询条件值
        /// </summary>
        public string TextValue { get; set; }
    }
    

   
    public class PagerTagHelper:TagHelper
    {
        public MoPagerOption PagerOption { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {

            output.TagName = "div";

            if (PagerOption.PageSize <= 0) { PagerOption.PageSize = 15; }
            if (PagerOption.CurrentPage <= 0) { PagerOption.CurrentPage = 1; }
            if (PagerOption.Total <= 0) { return; }

            //总页数
            var totalPage = PagerOption.Total / PagerOption.PageSize + (PagerOption.Total % PagerOption.PageSize > 0 ? 1 : 0);
            if (totalPage <= 0) { return; }
            //当前路由地址
            if (string.IsNullOrEmpty(PagerOption.RouteUrl))
            {

                //PagerOption.RouteUrl = helper.ViewContext.HttpContext.Request.RawUrl;
                if (!string.IsNullOrEmpty(PagerOption.RouteUrl))
                {

                    var lastIndex = PagerOption.RouteUrl.LastIndexOf("/");
                    PagerOption.RouteUrl = PagerOption.RouteUrl.Substring(0, lastIndex);
                }
            }
            PagerOption.RouteUrl = PagerOption.RouteUrl.TrimEnd('/');

            //是否具有查询条件
            var urlHeader = string.IsNullOrWhiteSpace(PagerOption.SelectName) ? "" : ("?" + PagerOption.SelectName + "=" + PagerOption.SelectValue + "&" + (string.IsNullOrWhiteSpace(PagerOption.TextName) ? "" : PagerOption.TextName + "=" + PagerOption.TextValue));



            //构造分页样式
            var sbPage = new StringBuilder(string.Empty);
            switch (PagerOption.StyleNum)
            {
                case 2:
                    {
                        break;
                    }
                default:
                    {
                        #region 默认样式

                        sbPage.Append("<nav>");
                        sbPage.Append("  <ul class=\"pagination\">");
                        sbPage.AppendFormat("       <li><a href=\"{0}/{1}\" aria-label=\"Previous\"><span aria-hidden=\"true\">&laquo;</span></a></li>",
                                                PagerOption.RouteUrl,
                                                PagerOption.CurrentPage - 1 <= 0 ? 1 : PagerOption.CurrentPage - 1);

                        for (int i = 1; i <= totalPage; i++)
                        {

                            sbPage.AppendFormat("       <li {1}><a href=\"{2}/{0}{3}\">{0}</a></li>",
                                i,
                                i == PagerOption.CurrentPage ? "class=\"active\"" : "",
                                PagerOption.RouteUrl, string.IsNullOrWhiteSpace(urlHeader) ? "" : urlHeader);

                        }

                        sbPage.Append("       <li>");
                        sbPage.AppendFormat("         <a href=\"{0}/{1}\" aria-label=\"Next\">",
                                            PagerOption.RouteUrl,
                                            PagerOption.CurrentPage + 1 > totalPage ? PagerOption.CurrentPage : PagerOption.CurrentPage + 1);
                        sbPage.Append("               <span aria-hidden=\"true\">&raquo;</span>");
                        sbPage.Append("         </a>");
                        sbPage.Append("       </li>");
                        sbPage.Append("   </ul>");
                        sbPage.Append("</nav>");
                        #endregion
                    }
                    break;
            }

            output.Content.SetHtmlContent(sbPage.ToString());
            //output.TagMode = TagMode.SelfClosing;
            //return base.ProcessAsync(context, output);
        }

    }
    #endregion
}
