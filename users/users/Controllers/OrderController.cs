using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using users.Models;
using users.ViewModels.Cart;
using users.Common;
using users.Extensions;

namespace users.Controllers
{
    [RoutePrefix("api/order")]
    public class OrderController : ApiBase
    {
        [HttpGet]
        [Route("applypromo/{userId}/{promoCode}")]
        public HttpResponseMessage applypromo(int userId, string promoCode)
        {
            Func<promocode, bool> FuncPromoWhere = delegate(promocode promo)
            {
                return promo.promoCode1 == promoCode && promo.expiresOn >= DateTime.UtcNow.IndianTime();
            };
            
            var response = new HttpResponseMessage();            
           
            var CartVm = new CartVm();
            try
            {
                using (var dbCntx = new dbEntity())
                {
                    var promoObj = dbCntx.promocodes
                                    .Where(FuncPromoWhere)
                                    .Select(x => x)
                                    .FirstOrDefault<promocode>();

                    var pAmount = 0.0F;
                    CartVm = GetCartData(userId, false, dbCntx);
                    string msg = "";
                    string Status = "";
                    if (promoObj != null)
                    {
                        if (promoObj.categoryId == -1)// -1 indicates promo applicable for all categories
                        {
                            pAmount = CalculateDiscount(ref CartVm, promoObj);
                            Status = "Success";
                        }
                        else
                        {
                            /* applying promocode to particular category only start */

                            var categoryMatchedItems = CartVm.ItemVm
                                                            .Where(x => x.categoryId == promoObj.categoryId)
                                                            .Select(x => x)
                                                            .ToList<ItemVm>();

                            if (categoryMatchedItems.Count > 0)
                            {
                                var catName = dbCntx.categories
                                               .Where(x => x.id == promoObj.categoryId)
                                               .Select(x => x.name)
                                               .FirstOrDefault<string>();

                                msg = "This offer is available for only " + catName + " items.";


                                var gCategorySubTotal = categoryMatchedItems.Sum(x => x.quantity * x.sellingPrice);
                                if (promoObj.isPercentage)
                                {
                                    pAmount = GetPromoDiscount(gCategorySubTotal, promoObj.promoValue);
                                    CartVm.GrandTotal = CartVm.GrandTotal - pAmount;
                                }
                                else
                                {
                                    pAmount = promoObj.promoValue;
                                    CartVm.GrandTotal = CartVm.GrandTotal - pAmount;
                                }
                                Status = "Success";
                            }
                            else
                            {
                                Status = "Failed";
                                msg = "No applicable product in your cart";
                            }
                            /* applying promocode to particular category only end */
                        }

                        response.Content = new StringContent(JsonConvert.SerializeObject(new
                        {
                            cartData = CartVm,
                            status = Status,
                            msg = msg,
                            pAmount = pAmount
                        }));

                        response.StatusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        response.Content = new StringContent(JsonConvert.SerializeObject(new {
                            cartData = CartVm,
                            status = "Failed",
                            msg = "Invalid Promo code"
                        }));
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

        public float CalculateDiscount(ref CartVm CartVm, promocode promoObj)
        {
            var pAmount = 0.0F;
            try
            {
                if (promoObj.isPercentage)
                {
                    pAmount = GetPromoDiscount(CartVm.SubTotal, promoObj.promoValue);
                    CartVm.GrandTotal = CartVm.GrandTotal - pAmount;
                }
                else
                {
                    pAmount = promoObj.promoValue;
                    CartVm.GrandTotal = CartVm.GrandTotal - pAmount;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pAmount;
        }

        [HttpPost]
        [Route("confirm/{userId}/{promoCode}")]
        public HttpResponseMessage confirm(int userId, string promoCode)
        {
            Func<promocode, bool> FuncPromoWhere = delegate(promocode promo)
            {
                return promo.promoCode1 == promoCode && promo.expiresOn >= DateTime.UtcNow.IndianTime();
            };

            var response = new HttpResponseMessage();
            try
            {
                using (var dbCntx = new dbEntity())
                {
                    var itemsVm = dbCntx.usercarts
                                     .Join(dbCntx.banners,
                                        a => a.bannerId,
                                        b => b.id,
                                        (a, b) => new { A = a, B = b })
                                     .Join(dbCntx.locations,
                                        ab => ab.A.locationId,
                                        c => c.locationId,
                                        (ab, c) => new { AB = ab, C = c })
                                     .Join(dbCntx.vendors,
                                        abc => abc.AB.A.vendorId,
                                        d => d.id,
                                        (abc, d) => new { ABC = abc, D = d })
                                     .Join(dbCntx.deals,
                                        abcd => abcd.ABC.AB.A.dealId,
                                        e => e.dealId,
                                        (abcd, e) => new { ABCD = abcd, E = e })
                                     .Where(x => x.ABCD.ABC.AB.A.userId == userId &&
                                                 x.ABCD.ABC.AB.A.isActive == true &&
                                                 x.ABCD.ABC.AB.A.isguest == false)
                                     .Select(x => new ItemVm {
                                            dealId = x.E.dealId,
                                            name = x.E.name,
                                            bannerId = x.ABCD.ABC.AB.B.id,
                                            locationId = x.ABCD.ABC.C.locationId,
                                            vendorId = x.ABCD.D.id,
                                            image = x.E.image,
                                            unitPrice = x.E.unitPrice,
                                            discount = x.E.discount,
                                            sellingPrice = x.E.sellingPrice,
                                            quantity = x.ABCD.ABC.AB.A.quantity,
                                            count = x.E.count,
                                            sold = x.E.sold.Value,
                                            hasShipping = x.E.hasShipping.Value })
                                     .ToList<ItemVm>();
                    

                    var totalDeals = dbCntx.deals
                                       .Join(dbCntx.usercarts,
                                                a => a.dealId,
                                                b => b.dealId,
                                                (a, b) => new { A = a, B = b })
                                       .Where(x => x.B.userId == userId &&
                                                   x.B.isActive == true)
                                       .Select(x => x.A)
                                       .ToList<deal>();


                    var subTotal = itemsVm.Sum(x => x.quantity * x.sellingPrice);
                    var grandTotal = subTotal + 1450;

                    var pAmount = 0.0F;
                    var hasShipping = itemsVm.Where(x => x.hasShipping == true)
                                        .Select(x => x).Count() > 0;                    

                    if (true)
                    {
                        var finalAddress = dbCntx.addresses
                                                .Where(x => x.userId == userId && x.isDefault == true)
                                                .Select(x => x)
                                                .FirstOrDefault<address>();

                        var shippingAddress = new shippingaddress
                        {
                            userId = userId,
                            name = finalAddress.name,
                            pincode = finalAddress.pincode,
                            address = finalAddress.address1,
                            landmark = finalAddress.landmark,
                            city = finalAddress.city,
                            state = finalAddress.state,
                            phone = finalAddress.phone,
                            createdOn = DateTime.UtcNow.IndianTime()
                        };
                        dbCntx.shippingaddresses.Add(shippingAddress);
                    }

                    var order = new order
                    {
                        orderId = GetOrderNo(dbCntx),
                        userId = userId,
                        orderStatus = hasShipping ? 1 : 5,
                        subTotal = subTotal,
                        grandTotal = grandTotal,
                        createdOn = DateTime.UtcNow.IndianTime(),
                        isActive = true,
                        promoDiscount = pAmount
                    };
                    /* Promo Code Start */
                    var promoObj = dbCntx.promocodes
                                    .Where(FuncPromoWhere)
                                    .Select(x => x)
                                    .FirstOrDefault<promocode>();

                    
                    if (promoObj != null)
                    {
                        if (promoObj.isPercentage)
                        {
                            pAmount = GetPromoDiscount(order.subTotal, promoObj.promoValue);
                            order.grandTotal = order.grandTotal - pAmount;                            
                        }
                        else
                        {
                            pAmount = promoObj.promoValue;
                            order.grandTotal = order.grandTotal - pAmount;
                        }
                        order.promoCode = promoCode;
                        order.promoDiscount = pAmount;
                    }

                    /* Promo Code End */

                    dbCntx.orders.Add(order);

                    for (var i = 0; i < itemsVm.Count; i++)
                    {
                        var order_Item = new orderitem
                        {
                            dealId = itemsVm[i].dealId,
                            quantity = itemsVm[i].quantity,
                            unitPrice = itemsVm[i].unitPrice,
                            discount = itemsVm[i].discount,
                            sellingPrice = itemsVm[i].sellingPrice,
                            subTotal = itemsVm[i].quantity * itemsVm[i].sellingPrice,
                            createdOn = DateTime.UtcNow.IndianTime(),
                            isActive = true
                        };

                        dbCntx.orderitems.Add(order_Item);

                        var dealInfo = totalDeals
                                        .Where(x => x.dealId == itemsVm[i].dealId)
                                        .FirstOrDefault<deal>();

                        dealInfo.sold = dealInfo.sold + itemsVm[i].quantity;

                        if (dealInfo.sold > dealInfo.count) {
                            response.StatusCode = HttpStatusCode.OK;
                            response.Content = new StringContent(JsonConvert.SerializeObject(new {
                                status = "Failed",
                                msg = "Some of the items in cart are not available."
                            }));

                            return response;
                        }
                    }

                    dbCntx.usercarts
                        .Where(x => x.userId == userId && x.isActive == true)
                        .ToList<usercart>()
                        .ForEach(x => x.isActive = false);
                    

                    dbCntx.SaveChanges();

                    response.StatusCode = HttpStatusCode.OK;
                    response.Content = new StringContent(JsonConvert.SerializeObject(new { orderId = order.orderId }));
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

        public string GetOrderNo(dbEntity dbCntx)
        {
            try
            {
                var maxCount = dbCntx.GetCurrentId().ToList()[0].Value;
                return "DE-" + maxCount.ToString("D6");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
