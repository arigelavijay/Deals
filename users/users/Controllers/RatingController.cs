using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using users.Models;
using users.Extensions;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace users.Controllers
{
    #region
    /*
     Note: Only registered user can only rate a deal
    */
    #endregion

    [RoutePrefix("api/rating")]
    public class RatingController : ApiController
    {
        [Route("{userId}/{dealId}/{rating}"), HttpPost]
        public HttpResponseMessage rating(int userId, int dealId, int rating)
        {
            var res = new HttpResponseMessage();
            try
            {
                using (var dbCntx = new dbEntity())
                {
                    var userRating = new userrating {
                        userId = userId,
                        dealId = dealId,
                        rating = rating,
                        dateCreated = DateTime.UtcNow.IndianTime(),
                        isActive = true
                    };

                    dbCntx.userratings.Add(userRating);

                    dbCntx.SaveChanges();
                    var avgRating = dbCntx.userratings
                                        .Where(x => x.dealId == dealId)
                                        .GroupBy(x => x.dealId)
                                        .Average(x => x.Average(y => y.rating));

                    

                    JObject obj = JObject.FromObject(new {
                        msg = "Success",
                        avg = avgRating
                    });

                    res.Content = new StringContent(JsonConvert.SerializeObject(obj));
                    res.StatusCode = HttpStatusCode.OK;
                }
            }
            catch (HttpResponseException ex)
            {
                throw new ApiDataException((int)ex.Response.StatusCode, ex.Message, ex.Response.StatusCode);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(500, ex.Message, HttpStatusCode.BadRequest);
            }
            return res;
        }
    }
}
