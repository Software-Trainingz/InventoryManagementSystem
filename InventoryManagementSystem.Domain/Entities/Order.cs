using InventoryManagementSystem.Domain.Common;
using System.Security.Cryptography.X509Certificates;

namespace InventoryManagementSystem.Domain.Entities
{
    public class Order
    {
        // الخصائص الأصلية
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public float TotalAmount { get; set; }
        public string PaymentMethod { get; set; }
        public string Status { get; set; }

        // الخصائص الجديدة للتكامل مع طلبات
        public string BuyerEmail { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
        public int? DeliveryMethodId { get; set; }
        public virtual DeliveryMethod DeliveryMethod { get; set; }
        public decimal Subtotal { get; set; }
        public decimal DeliveryFee { get; set; }
        public decimal Total { get; set; }
        public string PaymentIntentId { get; set; }
        public DateTime EstimatedDeliveryTime { get; set; }
        public int? RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }
        public string SpecialInstructions { get; set; }
        public virtual ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();

        // خصائص الربط مع طلبات
        public string TalabatOrderId { get; set; }
        public string TalabatOrderStatus { get; set; }
        public bool IsSyncedWithTalabat { get; set; }
    }
}