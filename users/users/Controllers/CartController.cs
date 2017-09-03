using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Newtonsoft.Json;

using users.Models;
using users.ViewModels.Cart;
using users.Common;
using users.Extensions;

using System.Threading.Tasks;

namespace users.Controllers
{
    [RoutePrefix("api/cart")]
    public class CartController : ApiBase
    {
        [HttpGet]
        [Route("{userId}/{isGuest}")]
        public HttpResponseMessage getitems(int userId, bool isGuest)
        {
            var response = new HttpResponseMessage();
            var CartVm = new CartVm();

            Func<DealUserCarts, bool> FuncFilterWhere = delegate(DealUserCarts obj)
            {
                return obj.usercart.userId == userId &&
                       obj.usercart.isActive == true &&
                       ((obj.deal.endsOn <= DateTime.UtcNow.IndianTime()) || obj.deal.sold >= obj.deal.count);

            };
           
            try
            {
                using (var dbCntx = new dbEntity())
                {
                    var expiredItems = dbCntx.deals
                                        .Join(dbCntx.usercarts,
                                            a => a.dealId,
                                            b => b.dealId,
                                            (a, b) => new DealUserCarts { deal = a, usercart = b })
                                        .Where(FuncFilterWhere)
                                        .Select(x => x.usercart)
                                        .ToList<usercart>();
                    
                    expiredItems.ForEach(x =>
                    {
                        x.isActive = false;
                    });
                    
                    dbCntx.SaveChanges();

                    CartVm = GetCartData(userId, isGuest, dbCntx);

                    response.Content = new StringContent(JsonConvert.SerializeObject(new {
                        CartItems = CartVm,
                        hasShipping = CartVm.ItemVm
                                        .Where(x => x.hasShipping == true)
                                        .Select(x => x)
                                        .Count() > 0
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
        
        [HttpDelete]
        [Route("delete")]
        public HttpResponseMessage delete(int userId, bool isGuest, int dealId)
        {
            var response = new HttpResponseMessage();
            try
            {
                using (var dbCntx = new dbEntity())
                {
                    var cartItem = dbCntx.usercarts
                                        .Where(x => 
                                            x.userId == userId && 
                                            x.isguest == isGuest && 
                                            x.dealId == dealId)
                                        .Select(x => x)
                                        .FirstOrDefault<usercart>();

                    dbCntx.usercarts.Remove(cartItem);
                    dbCntx.SaveChanges();

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

        [HttpPost]
        [Route("update/{userId}")]
        public HttpResponseMessage update(List<UpdateVm> Update, int userId)
        {
            var response = new HttpResponseMessage();
            try
            {
                using (var dbCntx = new dbEntity())
                {
                    var cartItems = dbCntx.usercarts
                                        .Where(x => x.userId == userId &&
                                                    x.isActive == true)
                                        .Select(x => x)
                                        .ToList<usercart>();

                    for (var i = 0; i < cartItems.Count; i++)
                    {
                        var item = Update.Where(x => x.dealId == cartItems[i].dealId &&
                                                     x.locationId == cartItems[i].locationId &&
                                                     x.vendorId == cartItems[i].vendorId &&
                                                     x.bannerId == cartItems[i].bannerId).FirstOrDefault<UpdateVm>();

                        cartItems[i].quantity = item.quantity;
                    }
                    dbCntx.SaveChanges();

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
