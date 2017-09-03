using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Newtonsoft.Json;

using users.ViewModels.Track;
using users.ViewModels.Cart;
using users.Models;
using users.Extensions;
using users.Common;

using Newtonsoft.Json.Linq;

namespace users.Controllers
{
    [RoutePrefix("api/track")]
    public class TrackController : ApiBase
    {
        [HttpGet]
        [Route("orders/{userId}")]
        public HttpResponseMessage orders(int userId, int currentPage, int limit)
        {
            var response = new HttpResponseMessage();            
            try
            {
                using (var dbCntx = new dbEntity())
                {
                    var _orders = dbCntx.orders
                                        .Where(x => x.userId == userId)
                                        .Select(userSelect.FuncOrdersSelect); 

                    var orders = new List<TrackVm>();
                    orders = _orders.OrderByDescending(x => x.createdOn).Skip(currentPage).Take(limit).ToList<TrackVm>();

                    orders.ForEach(x =>
                    {
                        var orderId = x.id;
                        x.ItemVm = (from a in dbCntx.orderitems
                                            join b in dbCntx.deals
                                            on a.dealId equals b.dealId
                                            where a.orderId == orderId
                                            select new ItemVm
                                            {
                                                dealId = a.dealId,
                                                name = b.name,
                                                bannerId = b.bannerId,
                                                locationId = b.locationId,
                                                vendorId = b.vendorId,
                                                image = b.image,
                                                unitPrice = b.unitPrice,
                                                discount = b.discount,
                                                sellingPrice = b.sellingPrice,
                                                count = b.count,
                                                sold = b.sold.Value,
                                                quantity = a.quantity
                                            }).ToList<ItemVm>();
                    });

                    /*
                    for (var i = 0; i < orders.Count; i++)
                    {
                        var orderId = orders[i].id;
                        orders[i].ItemVm = (from a in dbCntx.orderitems
                                            join b in dbCntx.deals
                                            on a.dealId equals b.dealId
                                            where a.orderId == orderId
                                            select new ItemVm
                                            {
                                                dealId = a.dealId,
                                                name = b.name,
                                                bannerId = b.bannerId,
                                                locationId = b.locationId,
                                                vendorId = b.vendorId,
                                                image = b.image,
                                                unitPrice = b.unitPrice,
                                                discount = b.discount,
                                                sellingPrice = b.sellingPrice,
                                                count = b.count,
                                                sold = b.sold.Value,
                                                quantity = a.quantity
                                            }).ToList<ItemVm>();
                    }*/

                                        

                    PageVm result = new PageVm() {                        
                        data = orders,
                        total = _orders.Count()                        
                    };

                    response.Content = new StringContent(JsonConvert.SerializeObject(result));
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
