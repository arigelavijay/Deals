using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Http;

using users.Models;
using users.ViewModels.Cart;
using users.Extensions;
using System.IO;
using System.Text;

namespace users.Common
{
    public class ApiBase : ApiController
    {
        public CartVm GetCartData(int userId, bool isGuest, dbEntity dbCntx)
        {
            try
            {
                var CartVm = new CartVm();
                var items = (from a in dbCntx.usercarts
                             join b in dbCntx.banners
                             on a.bannerId equals b.id
                             join c in dbCntx.locations
                             on a.locationId equals c.locationId
                             join d in dbCntx.vendors
                             on a.vendorId equals d.id
                             join e in dbCntx.deals
                             on a.dealId equals e.dealId
                             where a.userId == userId &&
                                   a.isActive == true &&
                                   a.isguest == isGuest
                             select new ItemVm
                             {
                                 dealId = e.dealId,
                                 name = e.name,
                                 bannerId = a.bannerId,
                                 locationId = a.locationId,
                                 vendorId = a.vendorId,
                                 image = e.image,
                                 unitPrice = e.unitPrice,
                                 discount = e.discount,
                                 sellingPrice = e.sellingPrice,
                                 quantity = a.quantity,
                                 count = e.count,
                                 sold = e.sold.Value,
                                 endsOn = e.endsOn,
                                 hasShipping = e.hasShipping.Value,
                                 categoryId = e.categoryId.Value
                             }).ToList<ItemVm>();

                CartVm.ItemVm = items.Where(x => x.endsOn >= DateTime.UtcNow.IndianTime())
                                     .Select(x => x)
                                     .ToList<ItemVm>();

                var tax = 1450;
                CartVm.SubTotal = CartVm.ItemVm.Sum(x => x.quantity * x.sellingPrice);
                CartVm.GrandTotal = CartVm.SubTotal + tax;
                CartVm.Tax = 0;
                if (CartVm.ItemVm.Count > 0) {
                    CartVm.Tax = 1450;
                }
                

                return CartVm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public float GetPromoDiscount(float subTotal, int promoValue)
        {
            try
            {
                return ((subTotal / 100) * promoValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CheckForItemAvailable(dbEntity dbCntx)
        {
            try
            {
                var userCartItems = (from a in dbCntx.usercarts
                                     join b in dbCntx.deals
                                     on a.dealId equals b.dealId
                                     select a).ToList<usercart>();

                var tempList = new List<int>();
                for (var i = 0; i < userCartItems.Count; i++)
                {
                    var dealItem = dbCntx.deals
                                        .Where(x => x.dealId == userCartItems[i].dealId)
                                        .Select(x => x)
                                        .FirstOrDefault<deal>();

                    if (dealItem.sold >= dealItem.count)
                    {
                        tempList.Add(dealItem.dealId);
                    }
                }

                tempList.ForEach(x =>
                {
                    userCartItems.Where(y => y.dealId == x).FirstOrDefault<usercart>().isActive = false;
                });

                dbCntx.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static T Deserialize<T>(string json)
        {
            try
            {
                T obj = Activator.CreateInstance<T>();
                MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(json));
                System.Runtime.Serialization.Json.DataContractJsonSerializer serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(obj.GetType());
                obj = (T)serializer.ReadObject(ms);
                ms.Close();
                ms.Dispose();
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string Serialize<T>(T obj)
        {
            try
            {
                System.Runtime.Serialization.Json.DataContractJsonSerializer serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(obj.GetType());
                MemoryStream ms = new MemoryStream();
                serializer.WriteObject(ms, obj);
                string retVal = Encoding.Default.GetString(ms.ToArray());
                ms.Dispose();
                return retVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}