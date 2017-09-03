using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using users.Models;
using users.ViewModels.Review;
using users.Extensions;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web.Http.Description;


namespace users.Controllers
{
    [RoutePrefix("api/review")]
    public class ReviewController : ApiController
    {
        [Route("insert")]
        [HttpPost]
        public HttpResponseMessage Insert(ReviewVm review)
        {
            var response = new HttpResponseMessage();
            try
            {
                using (var dbCntx = new dbEntity())
                {
                    var userReview = new userreview() {
                        userId = review.userId,
                        dealId = review.dealId,
                        title = review.title,
                        review = review.review,
                        rating = review.rating,
                        name = review.name,
                        dateCreated = DateTime.UtcNow.IndianTime(),
                        isActive = true
                    };
                    dbCntx.userreviews.Add(userReview);                    
                                            
                    /*
                    var userRating = new userrating() {
                        userId = review.userId,
                        dealId = review.dealId,
                        rating = review.rating,
                        dateCreated = DateTime.UtcNow.IndianTime(),
                        isActive = true
                    };
                    dbCntx.userratings.Add(userRating);*/
                    dbCntx.SaveChanges();

                    var dealAvgRatings = dbCntx.userreviews
                                            .Where(x => x.dealId == review.dealId)
                                            .GroupBy(x => x.dealId)
                                            .Average(x => x.Average(y => y.rating));


                    JObject obj = JObject.FromObject(new {
                        msg = "Success",
                        rating = dealAvgRatings
                    });

                    response.Content = new StringContent(JsonConvert.SerializeObject(obj));
                    response.StatusCode = HttpStatusCode.OK;
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
            return response;
        }

        [HttpGet]
        [Route("{dealId}")]
        public HttpResponseMessage reivews(int dealId, int skip, int limit)
        {
            var response = new HttpResponseMessage();
            try
            {
                using (var dbCntx = new dbEntity())
                {
                    var Query = dbCntx.userreviews
                                    .Where(x => x.dealId == dealId);

                    var _reviews = Query
                                    .OrderByDescending(x => x.dateCreated)
                                    .Skip(skip)
                                    .Take(limit)
                                    .Select(x => new
                                        {
                                            title = x.title,
                                            review = x.review,
                                            rating = x.rating,
                                            name = x.name
                                        })
                                    .ToList();

                    JObject JObj = JObject.FromObject(new {
                        reviews = _reviews,
                        total = Query.Count()
                    });

                    response.Content = new StringContent(JsonConvert.SerializeObject(JObj));
                    response.StatusCode = HttpStatusCode.OK;
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
            return response;
        }

        /*
        [HttpGet]
        [ResponseType(typeof(MyApiClass))]
        private IHttpActionResult MyApiMethod()
        {
            var Obj = new MyApiClass();
            return Ok<MyApiClass>(Obj);
        }*/
    }
    /*
    public class MyApiClass
    {

    }*/
}
