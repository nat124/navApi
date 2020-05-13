using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class StockTransaction
    {
        public int Id { get; set; }
        public int? ProductVariantOptionId { get; set; }
        public int Quantity { get; set; }
        public int TransactionTypeId { get; set; }
        public DateTime TransactionDate { get; set; }
        public bool IsActive { get; set; }
        public int ProductVariantDetailId { get; set; }
        public virtual TransactionType TransactionType { get; set; }
        public virtual ProductVariantOption ProductVariantOption { get; set; }

    }

    public class TransactionType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<StockTransaction> StockTransactions { get; set; }
    }
}
