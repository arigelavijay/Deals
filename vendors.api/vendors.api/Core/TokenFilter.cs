using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

using System.Data.Objects;

using vendors.api.Models;

namespace vendors.api.Core
{
    public class TokenFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            var TOKEN = HttpContext.Current.Request.Headers["AUTH_TOKEN"];
            var USERID = Convert.ToInt32(HttpContext.Current.Request.Headers["USER_ID"]);
            if (string.IsNullOrWhiteSpace(TOKEN))
            {
                filterContext.Response = new HttpResponseMessage
                {
                    Content = new StringContent("AUTH_TOKEN is missing.."),
                    StatusCode = HttpStatusCode.Unauthorized
                };
            }
            else
            {
                using (var dbCntx = new dbEntity())
                {
                    var AUTH_OBJ = (from row in dbCntx.authentication_token
                                    where row.userid == USERID && row.token == TOKEN 
                                    orderby row.expiretime descending
                                    select row).FirstOrDefault<authentication_token>();

                    if (AUTH_OBJ.expiretime > DateTime.UtcNow.IndianTime())
                    {
                        AUTH_OBJ.expiretime = DateTime.UtcNow.IndianTime().AddMinutes(20);
                        dbCntx.SaveChanges();
                    }
                    else
                    {
                        filterContext.Response = new HttpResponseMessage
                        {
                            Content = new StringContent("AUTH_TOKEN expired.."),
                            StatusCode = HttpStatusCode.Unauthorized
                        };
                    }
                }
            }
        }
    }
}