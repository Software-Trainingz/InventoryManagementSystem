using InventoryManagementSystem.Core.Domain.Common;
using System.Security.Cryptography.X509Certificates;

namespace InventoryManagementSystem.Core.Domain.Entities
{
    public class Order :BaseEntity<int>
    {
        public DateTime OrderDate { get; set; }

        public string ? PaymentMethod { get; set; }

        public required string  Status { get; set; }
        public decimal TotalAmount => OrderLines.Sum(ol => ol.LineTotal);

        // RelationShip

        public int CashierId { get; set; }

        public required Cashier ProcessedBy { get; set; }


        public required ICollection<OrderLine> OrderLines { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

    }
}