using Library.Web.Entities;
using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Library.Web.HtmlHelpers
{
    public static class MultiselectHelper
    {
        public static MvcHtmlString MultiselectFor(this HtmlHelper<Book> htmlHelper, Expression<Func<Book, PublicationHouse>> expression, object htmlAttributes)
        {

        }
    }
}