namespace InventoryManagementSystem.Domain.Entities
{
    public class ProductOption
    {
        public int OptionId { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsRequired { get; set; }
        public int MinSelections { get; set; }
        public int MaxSelections { get; set; }
        public virtual ICollection<OptionItem> Items { get; set; } = new HashSet<OptionItem>();

        // خصائص الربط مع طلبات
        public int? TalabatOptionId { get; set; }
    }

}