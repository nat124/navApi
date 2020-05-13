using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestCore
{
    public class StockTransactionModel
    {
        public int Id { get; set; }
        public int ProductVariantOptionId { get; set; }
        public int Quantity { get; set; }
        public int TransactionTypeId { get; set; }
        public DateTime TransactionDate { get; set; }
        public bool IsActive { get; set; }

        public virtual TransactionTypeModel TransactionType { get; set; }
        //public virtual ProductVariantOptionModel ProductVariantOption { get; set; }

    }

    public class TransactionTypeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

       // public virtual ICollection<StockTransaction> StockTransactions { get; set; }
    }
}