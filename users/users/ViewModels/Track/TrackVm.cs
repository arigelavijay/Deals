using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using users.ViewModels.Cart;

namespace users.ViewModels.Track
{
    public class TrackVm
    {
        public int id { get; set; }
        public string orderId { get; set; }
        public float grandTotal { get; set; }
        public float subTotal { get; set; }
        public string orderStatus { get; set; }
        public string promoCode { get; set; }
        public float promoDiscount { get; set; }
        public DateTime createdOn { get; set; }
        public List<ItemVm> ItemVm { get; set; }
        public int shippingId { get; set; }

        public TrackVm()
        {
            this.ItemVm = new List<ItemVm>();
        }        
    }

    public static class TrackOrder
    {
        public enum TrackEnum
        {
            Pending = 1,
            OrderPacked = 2,
            ReadyToShipment = 3,
            ShippedToDeliveryCenter = 4,
            NotApplicable = 5
        }

        public static string getOrderStatusDescription(this TrackEnum orderStatus)
        {
            switch (orderStatus)
            {
                case TrackEnum.Pending:
                    return "Pending";
                case TrackEnum.OrderPacked:
                    return "Order Packed";
                case TrackEnum.ReadyToShipment:
                    return "Ready To Shipment";
                case TrackEnum.ShippedToDeliveryCenter:
                    return "Shipped To Delivery Center";
                case TrackEnum.NotApplicable:
                    return "Not Applicable";
                default:
                    return null;
            }
        }
    }

    

    
}