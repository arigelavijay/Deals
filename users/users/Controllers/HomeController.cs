using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Data.Objects;
using MySql.Data.Entity;

using Newtonsoft.Json;

using users.Models;
using users.ViewModels.Home;
using users.Extensions;
using users.Common;

namespace users.Controllers
{
    [RoutePrefix("api/home")]
    public class HomeController : ApiBase
    {
        [HttpGet]
        [ResponseType(typeof(GridVm))]
        [Route("deals/{locationId}")]
        public IHttpActionResult GetDeals(int locationId)
        {
            Func<deal, bool> FuncDealsWhere = delegate(deal d)
            {
                return (d.locationId == locationId &&
                       d.isActive == true &&
                       d.count > d.sold &&
                       d.startsOn <= DateTime.UtcNow.IndianTime() &&
                       d.endsOn >= DateTime.UtcNow.IndianTime());
            };

            var DefaultcatConfig = new categoryconfig() { sequence = 1 };

            var response = new HttpResponseMessage();
            try
            {
                using (var dbCntx = new dbEntity())
                {
                    var deals = dbCntx.deals
                                    .Where(FuncDealsWhere)
                                    .Select(userSelect.FuncDealsSelect).ToList<DealsVm>();

                    var catList = deals.Where(x => x.bannerId > 5)
                                    .Join(dbCntx.categoryconfigs,
                                        a => a.categoryId,
                                        b => b.categoryId,
                                        (a, b) => new { A = a, B = b })                                    
                                    .GroupBy(x => new { x.A.categoryId, x.B.sequence })
                                    .OrderByDescending(x => x.FirstOrDefault().B.sequence)
                                    .Select(x => x.FirstOrDefault().A.categoryId)
                                    .ToList<int>();                   

                    
                    var dealAvgRatings = dbCntx.userreviews
                                            .GroupBy(x => x.dealId)
                                            .Select(x => new {
                                                dealId = x.FirstOrDefault().dealId,
                                                avgRating = x.Average(y => y.rating)
                                            }).ToList();

                    for (var i = 0; i < deals.Count; i++)
                    {
                        deals[i].avgRating = dealAvgRatings.Where(x => x.dealId == deals[i].dealId).Select(x => x.avgRating).FirstOrDefault();
                    }

                    var grid = new GridVm();
                    grid.main = deals.Where(x => x.bannerId == 1).Select(x => x).FirstOrDefault<DealsVm>();
                    grid.other = deals.Where(x => x.bannerId == 2 ||
                                                  x.bannerId == 3 || 
                                                  x.bannerId == 4 || 
                                                  x.bannerId == 5).Select(x => x).ToList<DealsVm>();
                    Remaining remList;
                    
                    for (var i = 0; i < catList.Count; i++)
                    {
                        remList = new Remaining();

                        var tempCatId = catList[i];
                        remList.RemainingList = deals.Where(x => x.categoryId == tempCatId && x.bannerId > 5).ToList<DealsVm>();
                        remList.categoryId = catList[i];
                        remList.categoryName = dbCntx.categories
                                                    .Where(x => x.id == tempCatId)
                                                    .Select(x => x.name)
                                                    .FirstOrDefault<string>();

                        grid.Remaining.Add(remList);
                    }                   
                    
                    return Json<GridVm>(grid);
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
        }

        
        [HttpGet]
        [Route("guestid")]
        public HttpResponseMessage guestid()
        {
            var response = new HttpResponseMessage();
            try
            {
                using (var dbCntx = new dbEntity())
                {
                    var dt = DateTime.UtcNow.IndianTime();
                    var guestid = Convert.ToInt32(dt.ToString("MM") + "" +
                                       dt.ToString("dd") + "" +
                                       dt.ToString("yy") + "" +
                                       dt.ToString("hh") + "" +
                                       dt.ToString("mm"));
                    var flag = true;
                    while (flag) {
                        var isvalid = dbCntx.guestids
                                        .Where(x => x.guest_id == guestid)
                                        .Select(x => x)
                                        .FirstOrDefault<guestid>();

                        if (isvalid != null)
                        {
                            guestid++;
                        }
                        else
                        {
                            flag = false;

                            var guestObj = new guestid {
                                guest_id = guestid
                            };
                            dbCntx.guestids.Add(guestObj);
                            dbCntx.SaveChanges();
                        }
                    }

                    response.Content = new StringContent(JsonConvert.SerializeObject(new {
                        guest_id = guestid
                    }));
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
         
    }
}
