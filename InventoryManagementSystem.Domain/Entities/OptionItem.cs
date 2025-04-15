namespace InventoryManagementSystem.Domain.Entities
{
    public class OptionItem
    {
        public int ItemId { get; set; }
        public int OptionId { get; set; }
        public virtual ProductOption Option { get; set; }
        public string Name { get; set; }
        public decimal AdditionalPrice { get; set; }
        public bool IsDefault { get; set; }

        // خصائص الربط مع طلبات
        public int? TalabatOptionItemId { get; set; }
    }

}