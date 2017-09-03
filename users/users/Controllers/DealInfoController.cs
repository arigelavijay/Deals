using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using users.Models;
using users.ViewModels.DealInfo;
using users.Extensions;

using Newtonsoft.Json;


namespace users.Controllers
{
    [RoutePrefix("api/dealinfo")]
    public class DealInfoController : ApiController
    {
        [HttpGet]
        [Route("{id}/{userId}/{isGuest}")]
        public HttpResponseMessage Info(int id, int userId, bool isGuest)
        {
            var response = new HttpResponseMessage();
            try
            {
                using (var dbCntx = new dbEntity())
                {
                    var dealInfo = dbCntx.deals
                                       .Where(x => 
                                           x.dealId == id)
                                       .Select(userSelect.DealInfoSelect)
                                       .FirstOrDefault<dealInfoVm>();

                    var Query = dbCntx.userreviews
                                    .Where(x => x.dealId == id);

                    int? ratingCount = Query.Count();

                    double? rating = 0;
                    bool isRated = false, isReviewed = false;
                    if (ratingCount > 0)
                    {
                        rating = Query
                                    .GroupBy(x => x.dealId)
                                    .Average(x => x.Average(y => y.rating));
                    }

                    if (!isGuest)
                    {
                        isRated = Query
                                    .Where(x => x.userId == userId)
                                    .Select(x => x)
                                    .Count() > 0;

                        isReviewed = Query
                                       .Where(x => x.userId == userId && x.dealId == id)
                                       .Select(x => x)
                                       .Count() > 0;
                                        
                    }

                    dealInfo.rating = rating.Value;
                    dealInfo.isRated = isRated;
                    dealInfo.isReviewed = isReviewed;
                    
                    if (dealInfo != null)
                    {
                        response.Content = new StringContent(JsonConvert.SerializeObject(dealInfo));
                        response.StatusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        response.Content = new StringContent("Not deal found");
                        response.StatusCode = HttpStatusCode.NotFound;
                    }
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

        [HttpPost]
        [Route("buy/{userId}/{isGuest}")]
        public HttpResponseMessage buy(int userId, bool isGuest, dealInfoVm dInfo)
        {
            var response = new HttpResponseMessage();            
            try
            {
                if (ModelState.IsValid)
                {
                    using (var dbCntx = new dbEntity())
                    {
                        var obj = dbCntx.deals.Where(x => x.dealId == dInfo.dealId &&
                                                           x.bannerId == dInfo.bannerId &&
                                                           x.locationId == dInfo.locationId &&
                                                           x.vendorId == dInfo.vendorId)
                                              .Select(x => x)
                                              .FirstOrDefault<deal>();

                        if (obj != null && obj.sold <= obj.count && obj.endsOn >= DateTime.UtcNow.IndianTime())
                        {
                            var item = dbCntx.usercarts
                                            .Where(x => 
                                                x.dealId == dInfo.dealId &&
                                                x.userId == userId && 
                                                x.isguest == isGuest && 
                                                x.isActive == true)
                                            .Select(x => x)
                                            .FirstOrDefault<usercart>();                            

                            if (item != null)
                            {
                                if ((item.quantity + 1) <= (obj.count - obj.sold))
                                {
                                    item.quantity = item.quantity + 1;
                                    response.Content = new StringContent(JsonConvert.SerializeObject(new
                                    {
                                        message = "Success",
                                        cartId = item.Id
                                    }));
                                }
                                else
                                {
                                    response.Content = new StringContent(JsonConvert.SerializeObject(new
                                    {
                                        message = "Out Of Stock"
                                    }));
                                }
                            }
                            else
                            {
                                var userCart = new usercart
                                {
                                    userId = userId,
                                    dealId = obj.dealId,
                                    locationId = obj.locationId,
                                    bannerId = obj.bannerId,
                                    vendorId = obj.vendorId,
                                    quantity = 1,
                                    dateCreated = DateTime.UtcNow.IndianTime(),
                                    dateModified = DateTime.UtcNow.IndianTime(),
                                    isActive = true,
                                    isguest = Convert.ToBoolean(isGuest)
                                };

                                dbCntx.usercarts.Add(userCart);

                                response.Content = new StringContent(JsonConvert.SerializeObject(new
                                {
                                    message = "Success",
                                    cartId = userCart.Id
                                }));
                            }

                            dbCntx.SaveChanges();
                            response.StatusCode = HttpStatusCode.OK;
                        }
                        else
                        {
                            response.Content = new StringContent("Deal not found.");
                            response.StatusCode = HttpStatusCode.NotFound;
                        }
                    }
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
    }
}
