using InventoryManagementSystem.Domain.Common;
using System;
using System.Collections.Generic;

namespace InventoryManagementSystem.Domain.Entities
{
    public class Order : BaseAuditableEntity<int>
    {
        public DateTime OrderDate { get; set; }
        public decimal Subtotal { get; set; }
        public decimal DeliveryFee { get; set; }
        public decimal Total { get; set; }
        public string PaymentMethod { get; set; }
        public string Status { get; set; }
        public string PaymentIntentId { get; set; }

        // Customer information
        public string BuyerEmail { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // Address information
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }

        // Delivery information
        public TimeSpan EstimatedDeliveryTime { get; set; }
        public string SpecialInstructions { get; set; }

        // Relationships (with proper nullability)
     
        public int? CashierId { get; set; } // Made nullable
        public virtual Cashier Cashier { get; set; }

        public int? RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }

        public int? CustomerId { get; set; } // Kept required
        public virtual Customer Customer { get; set; }

        // Order items
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();

        //public virtual DeliveryMethod DeliveryMethod { get; set; }
        //public int ? DeliveryMethodId { get; set; }

        // Talabat integration
        public string TalabatOrderId { get; set; }
        public string TalabatOrderStatus { get; set; }
        public bool IsSyncedWithTalabat { get; set; }
    }
}