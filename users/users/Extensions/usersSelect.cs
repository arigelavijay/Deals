using System;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Web;
using users.Models;
using users.ViewModels.DealInfo;
using users.ViewModels.Home;
using users.ViewModels.Track;

namespace users.Extensions
{
    public static class userSelect
    {
        public static Func<deal, dealInfoVm> DealInfoSelect = delegate(deal d)
        {
            return new dealInfoVm
            {
                dealId = d.dealId,
                vendorId = d.vendorId,
                bannerId = d.bannerId,
                locationId = d.locationId,
                name = d.name,
                description = d.description,
                image = d.image,
                startsOn = d.startsOn,
                endsOn = d.endsOn,
                leftDays = (d.endsOn - DateTime.UtcNow.IndianTime()).Days,
                sold = d.sold.Value,
                unitPrice = d.unitPrice,
                discount = d.discount,
                sellingPrice = d.sellingPrice,
                hoursLeft = (d.endsOn - DateTime.UtcNow.IndianTime()).TotalHours
            };
        };

        public static Func<deal, DealsVm> FuncDealsSelect = delegate(deal d)
        {
            return new DealsVm
            {
                dealId = d.dealId,
                bannerId = d.bannerId,
                vendorId = d.vendorId,
                image = d.image,
                name = d.name,
                remaining = d.count - d.sold,
                startsOn = d.startsOn,
                createdOn = d.createdOn,
                unitPrice = d.unitPrice,
                discount = d.discount,
                sellingPrice = d.sellingPrice,
                hoursLeft = (d.endsOn - DateTime.UtcNow.IndianTime()).TotalHours,
                categoryId = d.categoryId.Value
            };
        };

        public static Func<object, DealsVm> FuncDealsSelect2 = delegate(object d)
        {
            //IGrouping<System.Reflection.MemberTypes, System.Reflection.MemberInfo> group =
            //                                                            typeof(String).GetMembers().
            //                                                            GroupBy(member => member.MemberType).
            //                                                            First();
            var deal = d.GetType();
            return new DealsVm {
                
            };
        };

        
        public static DealsVm FuncDealsSelect3<TSource, TKey>
            (this IEnumerable<TSource> source)
        {
            return new DealsVm {
            
            };
        }        

        public static Func<order, TrackVm> FuncOrdersSelect = delegate(order o)
        {
            return new TrackVm
            {
                orderId = o.orderId,
                grandTotal = o.grandTotal,
                subTotal = o.subTotal,
                createdOn = o.createdOn,
                id = o.id,
                orderStatus = TrackOrder.getOrderStatusDescription((TrackOrder.TrackEnum)Enum.Parse(typeof(TrackOrder.TrackEnum), o.orderStatus.ToString())),
                promoCode = o.promoCode,
                promoDiscount = o.promoDiscount.Value,
                shippingId = o.shippingId
            };
        };

        [EdmFunction("deals2Model", "DiffHours")]
        public static int DiffHours(DateTime toDt, DateTime fromDt)
        {
            throw new NotSupportedException("Direct calls are not supported.");
        }

        #region
        /*
         <Function Name="DiffHours" ReturnType="Edm.Int32">
          <Parameter Name="toDt" Type="Edm.DateTime"></Parameter>
          <Parameter Name="fromDt" Type="Edm.DateTime"></Parameter>
          <DefiningExpression>
            SELECT TIMESTAMPDIFF(Hour, fromDt, toDt)
          </DefiningExpression>
        </Function>
         */
        #endregion
    }
}