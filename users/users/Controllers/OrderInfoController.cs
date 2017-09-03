using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Newtonsoft.Json;

using users.Models;
using users.ViewModels.Cart;
using users.ViewModels.Track;
using users.Extensions;
using users.ViewModels.Address;

namespace users.Controllers
{
    [RoutePrefix("api/orderinfo")]
    public class OrderInfoController : ApiController
    {
        [HttpGet]
        [Route("{userId}/{orderId}")]
        public HttpResponseMessage orderInfo(int userId, int orderId)
        {
            var response = new HttpResponseMessage();

            try
            {
                using (var dbCntx = new dbEntity())
                {
                    var orders = dbCntx.orders
                                    .Where(x => x.userId == userId && x.id == orderId)
                                    .Select(userSelect.FuncOrdersSelect)
                                    .FirstOrDefault<TrackVm>();

                    orders.ItemVm = (from a in dbCntx.orderitems
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

                    var address = (from a in dbCntx.shippingaddresses
                                   join b in dbCntx.states
                                   on a.state equals b.id
                                   where a.id == orders.shippingId && a.userId == userId
                                   select new ShowAddressVm
                                   {
                                       id = a.id,
                                       name = a.name,
                                       pincode = a.pincode,
                                       address = a.address,
                                       landmark = a.landmark,
                                       city = a.city,
                                       state = b.name,
                                       phone = a.phone
                                   }).FirstOrDefault<ShowAddressVm>();


                    response.Content = new StringContent(JsonConvert.SerializeObject(new {
                        orders = orders,
                        address = address
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
